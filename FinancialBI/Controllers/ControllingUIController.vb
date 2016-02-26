' FinancialUIController.vb
'
' Controlling User iterface: Compurte/ Display data and consolidated data
'
' To do:
'      - Keep track of hidden hierarchy items and provide right click option to hide/ unhide 
'      - Option accounts/entities hierarchy display -> priority normal  !
'
' Known bugs:
'
'
' Author: Julien Monnereau
' Last modified: 14/01/2016


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq
Imports VIBlend.Utilities
Imports VIBlend.WinForms.Controls
Imports CRUD
Imports FinancialBI.GlobalEnums



Friend Class ControllingUIController


#Region "Instance Variable"

    ' Objects
    Private computer As New Computer
    Private m_view As ControllingUI_2
    Friend m_entityNode As vTreeNode
    Private m_vCompNode As TreeNode
    Private m_computingCache As New ComputingCache(True)

    ' Variables
    Private m_currenciesNameIdDict As Dictionary(Of String, Int32)
    Private m_accountsIdShortlist As List(Of Int32)
    Private m_rowsHierarchyNode As New vTreeNode
    Private m_columnsHierarchyNode As New vTreeNode
    Private m_DisplayAxisHt As New Hashtable

    Private m_filtersAndAxisDict As New SafeDictionary(Of String, List(Of Int32))
    Private m_dataMap As Dictionary(Of String, Double)
    Private m_filters_dict As New SafeDictionary(Of String, Int32)
    Private m_filtersNodes As New vTreeNode
    Private m_VersionsTV As New vTreeView
    Friend m_versionsDict As New SafeDictionary(Of Int32, String)
    Friend m_initDisplayFlag As Boolean = False
    Friend m_computedFlag As Boolean = False
    Friend m_isComputingFlag As Boolean
    Friend m_isDisplayingFlag As Boolean = False
    Private m_chartsView As CUI2Visualization
    Private m_chartsViewContainer As New CUI2VisualizationContainer

    ' Virtual binding
    Private itemsDimensionsDict As Dictionary(Of HierarchyItem, Hashtable)
    Friend cellsUpdateNeeded As Boolean = True

    ' constants
    Private Const BASE_ALPHA As Single = 190

#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_inputView As Object)

        m_view = p_inputView
        LoadSpecialFiltersValuesNode()

        m_chartsView = New CUI2Visualization(Me)
        m_chartsViewContainer.Controls.Add(m_chartsView)
        m_chartsView.Dock = DockStyle.Fill

        AddHandler computer.ComputationAnswered, AddressOf AfterCompute

    End Sub

    Private Sub LoadSpecialFiltersValuesNode()

        ' Load Filters Nodes
        For Each axis As AxisType In GlobalVariables.Filters.GetDictionary().Keys
            For Each filter As Filter In GlobalVariables.Filters.GetDictionary(axis).Values
                Dim filterNodeName As String = computer.FILTERS_DECOMPOSITION_IDENTIFIER & filter.Id
                Dim filterNodeText As String = filter.Name
                Dim filterNode As vTreeNode = VTreeViewUtil.AddNode(filterNodeName, filterNodeText, m_filtersNodes)

                Dim filterValuesDict = GlobalVariables.FiltersValues.GetDictionary(filter.Id)
                For Each filterValue As FilterValue In filterValuesDict.Values
                    VTreeViewUtil.AddNode(filterValue.Id, filterValue.Name, filterNode)
                Next

            Next
        Next

        LoadAxisNodes(AxisType.Client, AnalysisAxis.CLIENTS, ControllingUI_2.CLIENTS_CODE)
        LoadAxisNodes(AxisType.Product, AnalysisAxis.PRODUCTS, ControllingUI_2.PRODUCTS_CODE)
        LoadAxisNodes(AxisType.Adjustment, AnalysisAxis.ADJUSTMENTS, ControllingUI_2.ADJUSTMENT_CODE)
        LoadAxisNodes(AxisType.Employee, AnalysisAxis.EMPLOYEES, ControllingUI_2.EMPLOYEE_CODE)

    End Sub

    Private Sub LoadAxisNodes(ByRef p_axisType As AxisType, _
                              ByRef p_analysisAxis As AnalysisAxis, _
                              ByRef p_axisCode As String)

        Dim l_AxisNode As vTreeNode = VTreeViewUtil.AddNode(computer.AXIS_DECOMPOSITION_IDENTIFIER & p_analysisAxis, _
                                                            p_axisCode, _
                                                            m_filtersNodes)
        For Each l_elem In GlobalVariables.AxisElems.GetDictionary(p_axisType).Values
            VTreeViewUtil.AddNode(l_elem.Id, l_elem.Name, l_AxisNode)
        Next

    End Sub

#End Region


#Region "Computing"

    Private Function HasMinimumDimensions() As String

        Select Case m_view.m_process
            Case Account.AccountProcess.FINANCIAL
                If Not m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS) Then
                    If Not m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS) Then
                        If Not m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS) Then
                            Return Local.GetValue("CUI.msg_specify_dimension_periods1")
                        End If
                    End If
                End If
                If Not m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS) Then
                    Return Local.GetValue("CUI.msg_specify_dimension_account")
                End If
                Return ""

            Case Account.AccountProcess.RH
                If Not m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.WEEKS) Then
                    If m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.DAYS) Then
                        If m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YDAYS) Then
                            Return Local.GetValue("CUI.msg_specify_dimension_periods2")
                        End If
                    End If
                End If
                'If Not m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES) Then
                '    If Not m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS) Then
                '        Return Local.GetValue("CUI.msg_specify_dimension_client_or_entities")
                '    End If
                'End If
                Return ""

            Case Else
                System.Diagnostics.Debug.WriteLine("CUI.Compute() -> ControllingUIController.HasMinimumDimensions() - undeifined process: " & m_view.m_process)
        End Select
        Return ""

    End Function

    Friend Sub Compute(ByRef p_versionIDs() As Int32, _
                       ByRef p_inputEntityNode As vTreeNode, _
                       Optional ByRef p_useCache As Boolean = False)

        If (m_isComputingFlag = True Or m_isDisplayingFlag = True) Then Exit Sub

        Dim l_minimumDimensionsErrorMessage As String = HasMinimumDimensions()
        If l_minimumDimensionsErrorMessage <> "" Then
            MsgBox(Local.GetValue("CUI.msg_specify_dimensions1") & Chr(13) & l_minimumDimensionsErrorMessage)
            Exit Sub
        End If
        m_view.SetComputeButtonState(False)
        m_isComputingFlag = True
        m_isDisplayingFlag = True
        m_computedFlag = False

        If Not m_dataMap Is Nothing Then m_dataMap.Clear()
        m_dataMap = Nothing
        m_entityNode = p_inputEntityNode
        ' View.ClearDGVs()
        m_rowsHierarchyNode.Nodes.Clear()
        m_columnsHierarchyNode.Nodes.Clear()

        ' Versions init
        m_versionsDict.Clear()
        For Each version_Id As Int32 In p_versionIDs
            Dim versionNode As vTreeNode = VTreeViewUtil.FindNode(m_view.m_leftPaneControl.versionsTV, version_Id)
            m_versionsDict.Add(versionNode.Value, versionNode.Text)
        Next

        If m_versionsDict.Count > 1 _
        AndAlso m_view.m_rightPaneControl.DimensionsListContainsItem(computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS) = False Then
            m_view.m_rightPaneControl.AddItemToColumnsHierarchy(ControllingUI_2.VERSIONS_CODE, _
                                                                computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS)
        End If

        m_initDisplayFlag = False
        For Each item In m_view.m_rightPaneControl.rowsDisplayList.Items
            VTreeViewUtil.AddNode(item.Value, item.Text, m_rowsHierarchyNode)
        Next
        For Each item In m_view.m_rightPaneControl.columnsDisplayList.Items
            VTreeViewUtil.AddNode(item.Value, item.Text, m_columnsHierarchyNode)
        Next

        ' Filters and axis Filters Build
        Dim filters As Dictionary(Of Int32, List(Of Int32)) = GetFilters()
        Dim axisFilters As Dictionary(Of Int32, List(Of Int32)) = GetAxisFilters()

        ' Decomposition Hierarchy build
        Dim computingHierarchyList As New List(Of String)
        IncrementComputingHierarchy(m_rowsHierarchyNode, computingHierarchyList)
        IncrementComputingHierarchy(m_columnsHierarchyNode, computingHierarchyList)

        ' Currency Setup
        If m_view.m_leftPaneControl.currenciesCLB.SelectedItem Is Nothing Then
            MsgBox(Local.GetValue("CUI.msg_currency_selection"))
            Exit Sub
        End If
        Dim currencyId As Int32 = CInt(m_view.m_leftPaneControl.currenciesCLB.SelectedItem.Value)

        ' Computing order
        Dim l_mustCompute As Boolean = True
        If p_useCache = True _
         AndAlso m_computingCache.MustCompute(m_entityNode.Value, _
                                              currencyId, _
                                              p_versionIDs, _
                                              filters, _
                                              axisFilters, _
                                              computingHierarchyList) = False Then
            l_mustCompute = False
        End If

        If l_mustCompute = True Then

            If computingHierarchyList.Count = 0 Then computingHierarchyList = Nothing

            computer.CMSG_COMPUTE_REQUEST(p_versionIDs, _
                                          {CInt(m_entityNode.Value)}.ToList, _
                                          m_view.m_process, _
                                          currencyId, _
                                          filters, _
                                          axisFilters, _
                                          computingHierarchyList, _
                                          m_view.GetPeriodsList(), _
                                          False)
        End If

        ' Cache registering
        m_computingCache.cacheEntityID = CInt(m_entityNode.Value)
        m_computingCache.cacheCurrencyId = currencyId
        m_computingCache.cacheVersions = p_versionIDs
        m_computingCache.cacheComputingHierarchyList = computingHierarchyList
        m_computingCache.cacheFilters = filters
        m_computingCache.cacheAxisFilters = axisFilters

        ' Redraw hierarchy Items
        ' Do not launch InitDisplay() if dimensions did not change
        InitDisplay()
        FillUIHeader()
        If m_initDisplayFlag = False Then
            m_view.LaunchCircularProgress()
        End If
        m_isDisplayingFlag = False

    End Sub

    Private Sub AfterCompute()

        While m_initDisplayFlag = False
        End While
        m_dataMap = computer.GetData()
        If m_view.m_process = Account.AccountProcess.RH Then m_view.DeleteNonUsedClientsRH()
        m_view.FormatDGV_ThreadSafe()
        m_view.TerminateCircularProgress()

        UpdateCUICharts()

        m_computedFlag = True
        m_view.SetComputeButtonState(True)
        m_isComputingFlag = False

    End Sub

    Private Sub IncrementComputingHierarchy(ByRef hierarchyNode As vTreeNode, _
                                            ByRef computingHierarchyList As List(Of String))

        For Each node As vTreeNode In hierarchyNode.Nodes
            If node.Value <> computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS _
            AndAlso node.Value <> computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES _
            AndAlso node.Value <> computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS _
            AndAlso node.Value <> computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS _
            AndAlso node.Value <> computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS _
            AndAlso node.Value <> computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS _
            AndAlso node.Value <> computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.WEEKS _
            AndAlso node.Value <> computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.DAYS Then
                computingHierarchyList.Add(node.Value)
            End If
        Next

    End Sub

#End Region


#Region "Display Initialization"

    Private Sub InitDisplay()

        m_filters_dict.Clear()
        itemsDimensionsDict = New SafeDictionary(Of HierarchyItem, Hashtable)
        FillHierarchy(m_rowsHierarchyNode)
        FillHierarchy(m_columnsHierarchyNode)
        m_view.m_progressBar.Value = 0
        m_view.m_progressBar.Refresh()

        For Each tab_ As VIBlend.WinForms.Controls.vTabPage In m_view.DGVsControlTab.TabPages
            Dim l_DataGridView As vDataGridView = tab_.Controls(0)
            RemoveHandler l_DataGridView.CellValueNeeded, AddressOf DGVs_CellValueNeeded
            l_DataGridView.Clear()

            '     If m_view.m_process = Account.AccountProcess.FINANCIAL Then
            m_accountsIdShortlist = VTreeViewUtil.GetNodesIds(VTreeViewUtil.FindNode(m_view.m_accountsTreeview, tab_.Name))
            m_accountsIdShortlist.Remove(tab_.Name)
            '     End If

            ' Display_axis_values Initialization 
            m_DisplayAxisHt.Clear()
            Select Case m_view.m_process
                Case Account.AccountProcess.FINANCIAL : m_DisplayAxisHt(GlobalEnums.DataMapAxis.ACCOUNTS) = 0
                Case Account.AccountProcess.RH : m_DisplayAxisHt(GlobalEnums.DataMapAxis.ACCOUNTS) = 0 '  PDC account id
            End Select
            m_DisplayAxisHt(GlobalEnums.DataMapAxis.PERIODS) = ""
            m_DisplayAxisHt(GlobalEnums.DataMapAxis.FILTERS) = "0"
            m_DisplayAxisHt(GlobalEnums.DataMapAxis.ENTITIES) = CInt(m_entityNode.Value)
            If m_versionsDict.Keys.Count = 1 Then
                m_DisplayAxisHt(GlobalEnums.DataMapAxis.VERSIONS) = CInt(m_versionsDict.Keys(0))
            Else
                m_DisplayAxisHt(GlobalEnums.DataMapAxis.VERSIONS) = 0
            End If

            ' keep track of dimension id / column ? priority normal !!!
            ' /////////////////////////////// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            l_DataGridView.RowsHierarchy.Clear()
            l_DataGridView.ColumnsHierarchy.Clear()
            If m_columnsHierarchyNode.Nodes.Count > 0 Then CreateColumn(l_DataGridView, m_columnsHierarchyNode.Nodes(0))
            If m_rowsHierarchyNode.Nodes.Count > 0 Then CreateRow(l_DataGridView, m_rowsHierarchyNode.Nodes(0))
            l_DataGridView.ColumnsHierarchy.AutoStretchColumns = True
            AddHandler l_DataGridView.CellValueNeeded, AddressOf DGVs_CellValueNeeded
            m_view.m_progressBar.Value = (tab_.TabIndex - 2) / m_view.DGVsControlTab.TabPages.Count * 100
            m_view.m_progressBar.Refresh()
        Next
        m_initDisplayFlag = True

    End Sub

    Private Sub FillHierarchy(ByRef p_hierarchyNode As vTreeNode)

        Select Case m_view.m_process
            Case Account.AccountProcess.FINANCIAL : FinancialHierarchyListPeriodsTreatment(p_hierarchyNode)
            Case Account.AccountProcess.RH : PDCHierarchyListPeriodsTreatment(p_hierarchyNode)
        End Select

        For Each node As vTreeNode In p_hierarchyNode.Nodes
            Select Case node.Value
                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                    For Each l_entityNode As vTreeNode In m_entityNode.Nodes
                        VTreeViewUtil.CopySubNodes(l_entityNode, node)
                    Next

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                    ' Accounts hierarchy if not first item in hierarchy !!!
                    ' !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    ' priority high
                    ' set an option or set a rule !! 
                    If True Then
                        For Each l_accountNode As vTreeNode In m_view.m_accountsTreeview.Nodes
                            For Each l_subAccountNode As vTreeNode In l_accountNode.Nodes
                                VTreeViewUtil.CopySubNodes(l_subAccountNode, node)
                            Next
                        Next
                    Else
                        For Each account_node As vTreeNode In m_view.m_accountsTreeview.GetNodes
                            VTreeViewUtil.AddNode(account_node.Value, account_node.Text, node)
                        Next
                    End If

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS
                    For Each l_clientsNode As vTreeNode In m_view.m_leftPaneControl.m_clientsTV.Nodes
                        VTreeViewUtil.CopySubNodes(l_clientsNode, node)
                    Next

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                    For Each l_version_id In m_versionsDict.Keys
                        VTreeViewUtil.AddNode(l_version_id, m_versionsDict(l_version_id), node)
                    Next
                    If m_versionsDict.Count = 2 Then
                        VTreeViewUtil.AddNode(m_versionsDict.Keys(0) & computer.TOKEN_SEPARATOR & m_versionsDict.Keys(1), _
                                              m_versionsDict.Values(0) & computer.TOKEN_SEPARATOR & m_versionsDict.Values(1), _
                                              node)
                    Else
                        m_vCompNode = Nothing
                    End If

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    Dim l_yearsPeriodList = GlobalVariables.Versions.GetYears(m_versionsDict)
                    If m_view.m_process = Account.AccountProcess.RH Then
                        l_yearsPeriodList = Period.FilterPeriodList(l_yearsPeriodList.ToArray, m_view.PeriodsShortList)
                    End If
                    For Each l_yearId As Int32 In l_yearsPeriodList
                        VTreeViewUtil.AddNode(computer.YEAR_PERIOD_IDENTIFIER & l_yearId, Format(Date.FromOADate(l_yearId), "yyyy"), node)
                    Next
                    m_view.m_leftPaneControl.SetupPeriodsTV(node)

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    Dim l_monthsPeriods = GlobalVariables.Versions.GetMonths(m_versionsDict)
                    If m_view.m_process = Account.AccountProcess.RH Then
                        l_monthsPeriods = Period.FilterPeriodList(l_monthsPeriods.ToArray, m_view.PeriodsShortList)
                    End If
                    For Each l_monthId As Int32 In l_monthsPeriods
                        VTreeViewUtil.AddNode(computer.MONTH_PERIOD_IDENTIFIER & l_monthId, Format(Date.FromOADate(l_monthId), "MMM yyyy"), node)
                    Next
                    m_view.m_leftPaneControl.SetupPeriodsTV(node)

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS
                    Dim l_periodsDict = GlobalVariables.Versions.GetPeriodsDictionary(m_versionsDict)

                    Dim l_yearsPeriodList = l_periodsDict.Keys.ToArray
                    If m_view.m_process = Account.AccountProcess.RH Then
                        l_yearsPeriodList = Period.FilterPeriodList(l_yearsPeriodList, m_view.PeriodsShortList).ToArray
                    End If
                    
                    For Each l_yearId As Int32 In l_periodsDict.Keys
                        Dim l_yearNode As vTreeNode = VTreeViewUtil.AddNode(computer.YEAR_PERIOD_IDENTIFIER & l_yearId, _
                                                                          Format(Date.FromOADate(l_yearId), "yyyy"), _
                                                                          node)

                        Dim l_monthsPeriods = l_periodsDict(l_yearId)
                        If m_view.m_process = Account.AccountProcess.RH Then
                            l_monthsPeriods = Period.FilterPeriodList(l_monthsPeriods.ToArray, m_view.PeriodsShortList)
                        End If
                        For Each l_monthId As Int32 In l_monthsPeriods
                            VTreeViewUtil.AddNode(computer.MONTH_PERIOD_IDENTIFIER & l_monthId, Format(Date.FromOADate(l_monthId), "MMM yyyy"), l_yearNode)
                        Next
                    Next
                    m_view.m_leftPaneControl.SetupPeriodsTV(node)

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.WEEKS
                    Dim l_weekPeriods = GlobalVariables.Versions.GetWeeks(m_versionsDict)
                    If m_view.m_process = Account.AccountProcess.RH Then
                        l_weekPeriods = Period.FilterPeriodList(l_weekPeriods.ToArray, m_view.PeriodsShortList)
                    End If

                    For Each l_weekId As Int32 In l_weekPeriods
                        VTreeViewUtil.AddNode(computer.WEEK_PERIOD_IDENTIFIER & l_weekId, Local.GetValue("general.week") & " " & Period.GetWeekNumberFromDateId(l_weekId) & ", " & Year(Date.FromOADate(l_weekId)), node)
                    Next
                    m_view.m_leftPaneControl.SetupPeriodsTV(node)

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.DAYS
                    Dim l_daysPeriods = GlobalVariables.Versions.GetDays(m_versionsDict)
                    If m_view.m_process = Account.AccountProcess.RH Then
                        l_daysPeriods = Period.FilterPeriodList(l_daysPeriods.ToArray, m_view.PeriodsShortList)
                    End If

                    For Each l_dayId As Int32 In l_daysPeriods
                        VTreeViewUtil.AddNode(computer.DAY_PERIOD_IDENTIFIER & l_dayId, Format(Date.FromOADate(l_dayId), "MMMM dd, yyyy"), node)
                    Next
                    m_view.m_leftPaneControl.SetupPeriodsTV(node)

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YDAYS
                    '     Dim l_periodsDict = GlobalVariables.Versions.GetPeriodsDictionary(m_versionsDict)

                    Dim l_weekPeriods = GlobalVariables.Versions.GetWeeks(m_versionsDict)
                    If m_view.m_process = Account.AccountProcess.RH Then
                        l_weekPeriods = Period.FilterPeriodList(l_weekPeriods.ToArray, m_view.PeriodsShortList)
                    End If
                    For Each l_weekId As Int32 In l_weekPeriods
                        Dim l_weekNode As vTreeNode = VTreeViewUtil.AddNode(computer.WEEK_PERIOD_IDENTIFIER & l_weekId, _
                                                                           Local.GetValue("general.week") & " " & Period.GetWeekNumberFromDateId(l_weekId) & ", " & Year(Date.FromOADate(l_weekId)), _
                                                                            node)

                        Dim l_daysPeriods = Period.GetDaysIdListInWeek(l_weekId)
                        '  If m_view.m_process = Account.AccountProcess.RH Then l_daysPeriods = m_view.FilterPeriodList(l_daysPeriods.ToArray)
                        For Each l_dayId As Int32 In l_daysPeriods
                            VTreeViewUtil.AddNode(computer.DAY_PERIOD_IDENTIFIER & l_dayId, Format(Date.FromOADate(l_dayId), "MMMM dd, yyyy"), l_weekNode)
                        Next
                    Next
                    m_view.m_leftPaneControl.SetupPeriodsTV(node)

                Case Else
                    For Each value_node As vTreeNode In VTreeViewUtil.FindNode(m_filtersNodes, node.Value).Nodes
                        VTreeViewUtil.AddNode(value_node.Value, value_node.Text, node)
                    Next

            End Select
        Next

    End Sub

    Private Sub FinancialHierarchyListPeriodsTreatment(ByRef hierarchyNode As vTreeNode)

        ' Build period related hierarchy nodes
        Dim periodsNodes As New vTreeNode
        For Each node As vTreeNode In hierarchyNode.Nodes
            Select Case node.Value
                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    VTreeViewUtil.AddNode(1, 1, periodsNodes)
                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    VTreeViewUtil.AddNode(2, 2, periodsNodes)
            End Select
        Next

        ' Analyze present periods dimensions and order
        If periodsNodes.Nodes.Count = 2 Then
            Select Case periodsNodes.Nodes(0).Value & periodsNodes.Nodes(1).Value
                Case "12"
                    ' if years before months => only keep months but with special code
                    Dim monthsNode As vTreeNode = VTreeViewUtil.FindNode(hierarchyNode, computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS)
                    monthsNode.Value = computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS
                    hierarchyNode.Nodes.Remove(VTreeViewUtil.FindNode(hierarchyNode, computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS))
                Case "21"
                    ' if years after months => delete years
                    hierarchyNode.Nodes.Remove(VTreeViewUtil.FindNode(hierarchyNode, computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS))
            End Select
        End If

    End Sub

    Private Sub PDCHierarchyListPeriodsTreatment(ByRef p_hierarchyNode As vTreeNode)

        ' Build period related hierarchy nodes
        Dim l_periodsNodes As New vTreeNode
        For Each l_node As vTreeNode In p_hierarchyNode.Nodes
            Select Case l_node.Value
                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.WEEKS
                    VTreeViewUtil.AddNode(1, 1, l_periodsNodes)
                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.DAYS
                    VTreeViewUtil.AddNode(2, 2, l_periodsNodes)
            End Select
        Next

        ' Analyze present periods dimensions and order
        If l_periodsNodes.Nodes.Count = 2 Then
            Select Case l_periodsNodes.Nodes(0).Value & l_periodsNodes.Nodes(1).Value
                Case "12"
                    ' if weeks before days => only keep days but with special code
                    Dim l_daysNode As vTreeNode = VTreeViewUtil.FindNode(p_hierarchyNode, computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.DAYS)
                    l_daysNode.Value = computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YDAYS
                    p_hierarchyNode.Nodes.Remove(VTreeViewUtil.FindNode(p_hierarchyNode, computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.WEEKS))
                Case "21"
                    ' if weeks after days => delete weeks
                    p_hierarchyNode.Nodes.Remove(VTreeViewUtil.FindNode(p_hierarchyNode, computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.WEEKS))
            End Select
        End If

    End Sub

    Private Sub CreateColumn(ByRef dgv As vDataGridView, _
                             ByRef dimensionNode As vTreeNode, _
                             Optional ByRef valueNode As vTreeNode = Nothing, _
                             Optional ByRef column As HierarchyItem = Nothing)

        Dim subColumn As HierarchyItem
        If valueNode Is Nothing Then
            If dimensionNode.Nodes.Count > 0 Then
                ' Loop through values
                For Each subNode In dimensionNode.Nodes
                    CreateColumn(dgv, dimensionNode, subNode, column)
                Next
            ElseIf Not dimensionNode.NextSiblingNode Is Nothing Then
                CreateColumn(dgv, dimensionNode.NextNode, , column)
            End If
        Else
            'Set current value for current display axis
            If SetDisplayAxisValue(dimensionNode, valueNode) = True Then

                If column Is Nothing Then
                    subColumn = dgv.ColumnsHierarchy.Items.Add(valueNode.Text)
                Else
                    subColumn = column.Items.Add(valueNode.Text)
                End If
                subColumn.ItemValue = 0
                HideHiearchyItemIfVComp(subColumn, _
                                        dimensionNode, _
                                        valueNode)

                ' Style => will go in utilities !!! priority normal
                ' ------------------------------------------------------------------------------
                m_view.FormatDGVItem(subColumn)
                subColumn.TextAlignment = Drawing.ContentAlignment.MiddleCenter
                RegisterHierarchyItemDimensions(subColumn)
                ' ------------------------------------------------------------------------------

                ' Dig one level deeper if any
                If Not dimensionNode.NextSiblingNode Is Nothing Then

                    ' Test: Loop only if dimension is not account and account_formulatype = title
                    If dimensionNode.Value = computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS Then
                        Dim account As Account = GlobalVariables.Accounts.GetValue(CUInt(valueNode.Value))
                        If account Is Nothing Then Exit Sub
                        If account.FormulaType = account.FormulaTypes.TITLE Then
                            ' Case account formula type title
                        Else
                            CreateColumn(dgv, dimensionNode.NextNode, , subColumn)
                        End If
                    Else
                        CreateColumn(dgv, dimensionNode.NextNode, , subColumn)
                    End If
                End If

                ' Loop through children if any
                For Each subNode In valueNode.Nodes
                    CreateColumn(dgv, dimensionNode, subNode, subColumn)
                Next

                LevelDimensionFilterOrAxis(dimensionNode)
            End If
        End If

    End Sub

    Private Sub CreateRow(ByRef dgv As vDataGridView, _
                          ByRef dimensionNode As vTreeNode, _
                          Optional ByRef valueNode As vTreeNode = Nothing, _
                          Optional ByRef row As HierarchyItem = Nothing)

        Dim subRow As HierarchyItem
        If valueNode Is Nothing Then
            If dimensionNode.Nodes.Count > 0 Then
                ' Loop through values
                For Each subNode In dimensionNode.Nodes
                    CreateRow(dgv, dimensionNode, subNode, row)
                Next
            ElseIf Not dimensionNode.NextSiblingNode Is Nothing Then
                CreateRow(dgv, dimensionNode.NextNode, , row)
            End If
        Else
            'Set current value for current display axis
            If SetDisplayAxisValue(dimensionNode, valueNode) = True Then

                If row Is Nothing Then
                    subRow = dgv.RowsHierarchy.Items.Add(valueNode.Text)
                Else
                    subRow = row.Items.Add(valueNode.Text)
                End If
                subRow.ItemValue = m_DisplayAxisHt(GlobalEnums.DataMapAxis.ACCOUNTS)
                subRow.CellsDataSource = GridCellDataSource.Virtual
                m_view.FormatDGVItem(subRow)
                HideHiearchyItemIfVComp(subRow, _
                                        dimensionNode, _
                                        valueNode)
                RegisterHierarchyItemDimensions(subRow)

                ' Dig one level deeper if any
                If Not dimensionNode.NextSiblingNode Is Nothing Then

                    ' Test: Loop only if dimension is not account and account_formulatype = title
                    If dimensionNode.Value = computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS Then
                        Dim account As Account = GlobalVariables.Accounts.GetValue(CUInt(valueNode.Value))
                        If account Is Nothing Then Exit Sub
                        If account.FormulaType = account.FormulaTypes.TITLE Then
                            ' Case account formula type title
                        Else
                            CreateRow(dgv, dimensionNode.NextNode, , subRow)
                        End If
                    Else
                        CreateRow(dgv, dimensionNode.NextNode, , subRow)
                    End If
                End If

                ' Loop through children if any
                For Each subNode In valueNode.Nodes
                    CreateRow(dgv, dimensionNode, subNode, subRow)
                Next

                LevelDimensionFilterOrAxis(dimensionNode)
            End If
        End If

    End Sub

    Private Function SetDisplayAxisValue(ByRef dimensionNode As vTreeNode, _
                                         ByRef valueNode As vTreeNode) As Boolean

        Select Case dimensionNode.Value
            Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                ' In case display_axis is accounts we should only display the accounts belonging to the current accounts tab
                If m_accountsIdShortlist.Contains(CInt(valueNode.Value)) = True Then
                    m_DisplayAxisHt(GlobalEnums.DataMapAxis.ACCOUNTS) = CInt(valueNode.Value)
                Else
                    Return False
                End If

            Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                m_DisplayAxisHt(GlobalEnums.DataMapAxis.ENTITIES) = CInt(valueNode.Value)

            Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                m_DisplayAxisHt(GlobalEnums.DataMapAxis.VERSIONS) = valueNode.Value

            Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, _
                 computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS, _
                 computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS, _
                computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.WEEKS, _
                computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.DAYS, _
                computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YDAYS
                m_DisplayAxisHt(GlobalEnums.DataMapAxis.PERIODS) = valueNode.Value

            Case Else
                ' Filters (clients, products, adjustments, oter filters)
                ' In case display_axis is filters we just add the filter_value_id to the filters_values_id_list
                ' Possible values are analysis_axis except entities
                m_filters_dict(dimensionNode.Value) = valueNode.Value

        End Select
        Return True

    End Function

    Private Sub LevelDimensionFilterOrAxis(ByRef dimensionNode As vTreeNode)

        Dim findNode As vTreeNode = VTreeViewUtil.FindNode(m_filtersNodes, dimensionNode.Value)
        If Not findNode Is Nothing Then
            m_filters_dict.Remove(dimensionNode.Value)                 ' remove filter from filters dic
        Else
            Select Case dimensionNode.Value
                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                    m_DisplayAxisHt(GlobalEnums.DataMapAxis.ENTITIES) = CInt(m_entityNode.Value)

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                    m_DisplayAxisHt(GlobalEnums.DataMapAxis.VERSIONS) = 0

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                    m_DisplayAxisHt(GlobalEnums.DataMapAxis.ACCOUNTS) = 0

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    m_DisplayAxisHt(GlobalEnums.DataMapAxis.PERIODS) = ""

                Case computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    m_DisplayAxisHt(GlobalEnums.DataMapAxis.PERIODS) = ""

            End Select
        End If

    End Sub

    Private Sub RegisterHierarchyItemDimensions(ByRef item As HierarchyItem)

        itemsDimensionsDict.Add(item, New Hashtable)
        m_DisplayAxisHt(GlobalEnums.DataMapAxis.FILTERS) = computer.GetFiltersToken(m_filters_dict)
        For Each key In m_DisplayAxisHt.Keys
            itemsDimensionsDict(item)(key) = m_DisplayAxisHt(key)
        Next

    End Sub

#End Region


#Region "Events"

    Friend Sub DGVs_CellValueNeeded(ByVal sender As Object, ByVal args As CellValueNeededEventArgs)

        ' priority high -> no update if alraedy displayed !!!! 
        '---------------------------------------------------------
        If Not m_dataMap Is Nothing  Then

            Dim l_accountId As UInt32 = 0
            Dim l_entityId As Int32 = 0
            Dim l_periodId As String = ""
            Dim l_versionId As String = 0
            Dim l_filterId As String = "0"

            SetCellsItems(args.RowItem, _
                          args.ColumnItem, _
                          l_entityId, _
                          l_accountId, _
                          l_periodId, _
                          l_versionId, _
                          l_filterId)

            Dim l_token As String = computer.TOKEN_SEPARATOR & _
                                  l_filterId & computer.TOKEN_SEPARATOR & _
                                  l_entityId & computer.TOKEN_SEPARATOR & _
                                  l_accountId & computer.TOKEN_SEPARATOR & _
                                  l_periodId
            If l_versionId.Contains(computer.TOKEN_SEPARATOR) Then
                SetVersionsComparisonCellsValues(l_versionId, l_token, args)
            Else
                If m_dataMap.ContainsKey(l_versionId & l_token) Then
                    ' The data CONTAINS the token
                    args.CellValue = m_dataMap(l_versionId & l_token)
                    If Double.IsNaN(args.CellValue) Then args.CellValue = "-"
                    'If My.Settings.controllingUIResizeTofitGrid = True Then
                    '    ' args.ColumnItem.DataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
                    '    ' args.ColumnItem.AutoResize(AutoResizeMode.FIT_ALL)
                    'End If
                Else
                    ' The data DOES NOT CONTAIN the token
                    args.CellValue = ""
                    '  DeleteRowsIfNeededCaseTokenNotFound(args, l_accountId)
                End If
            End If
        End If

    End Sub

    Private Sub SetVersionsComparisonCellsValues(ByRef p_versionid As String, _
                                                 ByRef p_token As String, _
                                                 ByVal args As CellValueNeededEventArgs)

        Dim v1 As Int32 = GetFirstVersionId(p_versionid)
        Dim v2 As Int32 = GetSecondVersionId(p_versionid)

        If m_dataMap.ContainsKey(v1 & p_token) _
        AndAlso m_dataMap.ContainsKey(v2 & p_token) Then
            Dim l_deltaValue = m_dataMap(v1 & p_token) - m_dataMap(v2 & p_token)
            args.CellValue = l_deltaValue
            If Double.IsNaN(args.CellValue) Then
                args.CellValue = "-"
            Else
                Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(args.RowItem.DataGridView.VIBlendTheme).GridCellStyle
                If l_deltaValue > 0 Then
                    ' CStyle.FillStyle = New FillStyleSolid(System.Drawing.Color.Green)
                    CStyle.TextColor = System.Drawing.Color.Green
                Else
                    CStyle.TextColor = Drawing.Color.Red
                    '  CStyle.FillStyle = New FillStyleSolid(System.Drawing.Color.Red)
                End If
                args.RowItem.DataGridView.CellsArea.SetCellDrawStyle(args.RowItem, args.ColumnItem, CStyle)
            End If
            If My.Settings.controllingUIResizeTofitGrid = True Then
                args.ColumnItem.DataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
                ' args.ColumnItem.AutoResize(AutoResizeMode.FIT_ALL)
            End If
        Else
            args.CellValue = ""
        End If

    End Sub

    Private Sub DeleteRowsIfNeededCaseTokenNotFound(ByRef args As CellValueNeededEventArgs, _
                                                    ByRef p_accountid As Int32)

        Dim l_account As Account = GlobalVariables.Accounts.GetValue(p_accountid)
        If Not l_account Is Nothing Then
            If (l_account.FormulaType <> Account.FormulaTypes.TITLE) Then
                args.CellValue = "-"

                ' Attention à commenter -> Ou bien flexibilité rows/columns
                ' *******************************************************
                If IsEmptyRow(args.RowItem) Then
                    args.RowItem.Delete()
                End If
                ' *******************************************************

            End If
        End If

    End Sub

    Friend Sub SetCellsItems(ByVal p_row As HierarchyItem, _
                             ByVal p_column As HierarchyItem, _
                             ByRef p_entityId As Int32, _
                             ByRef p_accountId As Int32, _
                             ByRef p_periodId As String, _
                             ByRef p_versionId As String, _
                             ByRef p_filterId As String)

        Dim items() As HierarchyItem = {p_row, p_column}
        For Each item As HierarchyItem In items
            '  If itemsDimensionsDict(item).Count = 0 then Exit sub
            Dim ht As Hashtable = itemsDimensionsDict(item)
            For Each dimension In ht.Keys
                Dim value = ht(dimension)
                If Not TypeOf (dimension) Is Char Then
                    Select Case dimension
                        Case GlobalEnums.DataMapAxis.ACCOUNTS
                            If value <> 0 Then p_accountId = value
                        Case GlobalEnums.DataMapAxis.ENTITIES
                            If value <> 0 Then
                                Select Case p_entityId
                                    Case 0
                                        p_entityId = value
                                    Case Else
                                        If p_entityId = CInt(m_entityNode.Value) Then p_entityId = value
                                End Select
                            End If
                        Case GlobalEnums.DataMapAxis.PERIODS
                            If value <> "" Then p_periodId = value
                        Case GlobalEnums.DataMapAxis.VERSIONS
                            If value <> "0" Then p_versionId = value
                        Case GlobalEnums.DataMapAxis.FILTERS
                            If value <> "0" Then p_filterId = value
                    End Select
                End If
            Next
        Next


    End Sub

#Region "Utilities"

    Private Function IsEmptyRow(ByRef p_row As HierarchyItem) As Boolean
        For Each item In p_row.Cells
            If item.Value <> "" Then Return (False)
        Next
        Return (True)
    End Function

#End Region

#End Region


#Region "Versions Comparison and Periods Display"

    Private Sub HideHiearchyItemIfVComp(ByRef item As HierarchyItem, _
                                       ByRef dimensionNode As vTreeNode, _
                                       ByRef valueNode As vTreeNode)

        If dimensionNode.Value = computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS _
         AndAlso valueNode.Value.Contains(computer.TOKEN_SEPARATOR) Then
            item.Hidden = True
        End If

    End Sub

    Friend Sub VersionsCompDisplay(ByVal display As Boolean)

        For Each tab_ As VIBlend.WinForms.Controls.vTabPage In m_view.DGVsControlTab.TabPages
            Dim DGV As vDataGridView = tab_.Controls(0)
            For Each row As HierarchyItem In DGV.RowsHierarchy.Items
                DisplayVCompHierarchy(row, display)
            Next
            For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
                DisplayVCompHierarchy(column, display)
            Next
        Next

    End Sub

    Private Sub DisplayVCompHierarchy(ByRef item As HierarchyItem, _
                                     ByRef display As Boolean)

        If item.Caption.Contains(computer.TOKEN_SEPARATOR) Then
            item.Hidden = display
        Else
            For Each subItem As HierarchyItem In item.Items
                DisplayVCompHierarchy(subItem, display)
            Next
        End If

    End Sub

    Friend Sub ReverseVersionsComparison()

        If m_vCompNode Is Nothing Then Exit Sub
        Dim v1 As String = GetFirstVersionId(m_vCompNode.Name)
        Dim v2 As String = GetSecondVersionId(m_vCompNode.Name)
        m_vCompNode.Name = v2 & computer.TOKEN_SEPARATOR & v1

        v1 = GetFirstVersionId(m_vCompNode.Text)
        v2 = GetSecondVersionId(m_vCompNode.Text)
        m_vCompNode.Text = v2 & computer.TOKEN_SEPARATOR & v1

        ' Then launch rows and column hierarchies redrawing 
        '   => priority normal !!!


    End Sub

    Friend Sub PeriodsSelectionFilter(ByRef periodsSelectionDict As Dictionary(Of String, Boolean))

        For Each tab_ As vTabPage In m_view.DGVsControlTab.TabPages
            Dim DGV As vDataGridView = tab_.Controls(0)

            For Each col As HierarchyItem In DGV.ColumnsHierarchy.Items
                FilterSubHierarchyItems(col, periodsSelectionDict)
            Next
            For Each row As HierarchyItem In DGV.RowsHierarchy.Items
                FilterSubHierarchyItems(row, periodsSelectionDict)
            Next
        Next

    End Sub

    Private Sub FilterSubHierarchyItems(ByRef item As HierarchyItem, _
                                        ByRef periodsSelectionDict As Dictionary(Of String, Boolean))

        If periodsSelectionDict.Keys.Contains(item.Caption) Then
            If periodsSelectionDict(item.Caption) = False Then
                item.Hidden = True
            Else
                item.Hidden = False
            End If
        End If
        For Each subItem As HierarchyItem In item.Items
            FilterSubHierarchyItems(subItem, periodsSelectionDict)
        Next

    End Sub

#End Region


#Region "Filters Reading"

    Private Function GetAxisFilters() As Dictionary(Of Int32, List(Of Int32))

        Dim axisFilters As New SafeDictionary(Of Int32, List(Of Int32))

        AddAxisFilterFromTV(m_view.m_leftPaneControl.entitiesTV, GlobalEnums.AnalysisAxis.ENTITIES, axisFilters)
        AddAxisFilterFromTV(m_view.m_leftPaneControl.m_clientsTV, GlobalEnums.AnalysisAxis.CLIENTS, axisFilters)
        AddAxisFilterFromTV(m_view.m_leftPaneControl.productsTV, GlobalEnums.AnalysisAxis.PRODUCTS, axisFilters)
        AddAxisFilterFromTV(m_view.m_leftPaneControl.adjustmentsTV, GlobalEnums.AnalysisAxis.ADJUSTMENTS, axisFilters)

        If axisFilters.Count = 0 Then
            Return Nothing
        Else
            Return axisFilters
        End If

    End Function

    Private Sub AddAxisFilterFromTV(ByRef TV As vTreeView, _
                                    ByRef axisId As Int32, _
                                    ByRef axisFilters As Dictionary(Of Int32, List(Of Int32)))

        If FiltersReader.IsAxisFiltered(TV) Then
            axisFilters.Add(axisId, New List(Of Int32))
            For Each axisValueId As Int32 In VTreeViewUtil.GetCheckedNodesIds(TV)
                axisFilters(axisId).Add(axisValueId)
            Next
        End If

    End Sub

    Private Function GetFilters() As Dictionary(Of Int32, List(Of Int32))

        Dim filters As New SafeDictionary(Of Int32, List(Of Int32))

        AddFiltersFromTV(m_view.m_leftPaneControl.entitiesFiltersTV, _
                         GlobalEnums.AnalysisAxis.ENTITIES, _
                         filters)
        AddFiltersFromTV(m_view.m_leftPaneControl.clientsFiltersTV, _
                         GlobalEnums.AnalysisAxis.CLIENTS, _
                         filters)
        AddFiltersFromTV(m_view.m_leftPaneControl.productsFiltersTV, _
                         GlobalEnums.AnalysisAxis.PRODUCTS, _
                         filters)
        AddFiltersFromTV(m_view.m_leftPaneControl.adjustmentsFiltersTV, _
                         GlobalEnums.AnalysisAxis.ADJUSTMENTS, _
                         filters)
        If m_view.m_process = Account.AccountProcess.RH Then
            AddFiltersFromTV(m_view.m_leftPaneControl.m_employeesFiltersTV, _
                            GlobalEnums.AnalysisAxis.EMPLOYEES, _
                            filters)
        End If

        If filters.Count = 0 Then
            Return Nothing
        Else
            Return filters
        End If

    End Function

    Private Sub AddFiltersFromTV(ByRef TV As vTreeView, _
                                 ByRef axis_id As Int32, _
                                 ByRef filtersDict As Dictionary(Of Int32, List(Of Int32)))

        Dim tmpDict As Dictionary(Of Int32, List(Of Int32)) = FiltersReader.GetFiltersValuesDict(TV, axis_id)
        For Each filterId As Int32 In tmpDict.Keys
            filtersDict.Add(filterId, tmpDict(filterId))
        Next

    End Sub


#End Region


#Region "Charts Interface"

    Friend Sub ShowCharts()

        m_chartsViewContainer.Show()

    End Sub

    Friend Sub UpdateCUICharts()

        m_chartsView.ClearCharts_ThreadSafe()
        Dim xAxisValues As String() = GetSerieXValues()

        Select Case m_versionsDict.Count
            Case 1
                FillChartsSeries(CInt(m_versionsDict.Keys(0)), "", xAxisValues, BASE_ALPHA)
            Case Else
                Dim alpha As Single = BASE_ALPHA
                For Each versionId As Int32 In m_versionsDict.Keys
                    FillChartsSeries(versionId, m_versionsDict(versionId), xAxisValues, alpha)
                    alpha -= 50
                    If alpha < 60 Then Exit Select ' define as main alpha priority normal
                Next
        End Select

    End Sub

    Private Sub FillChartsSeries(ByVal p_versionId As Int32, _
                                 ByVal p_versionName As String, _
                                 ByRef p_xAxisValues As String(), _
                                 ByVal p_alpha As Single)

        FillSerie(My.Settings.chart1Serie1AccountId, 0, p_versionId, p_versionName, p_xAxisValues, My.Settings.chart1Serie1Color, My.Settings.chart1Serie1Type, p_alpha)
        FillSerie(My.Settings.chart1Serie2AccountId, 0, p_versionId, p_versionName, p_xAxisValues, My.Settings.chart1Serie2Color, My.Settings.chart1Serie2Type, p_alpha)
        FillSerie(My.Settings.chart2Serie1AccountId, 1, p_versionId, p_versionName, p_xAxisValues, My.Settings.chart2Serie1Color, My.Settings.chart2Serie1Type, p_alpha)
        FillSerie(My.Settings.chart2Serie2AccountId, 1, p_versionId, p_versionName, p_xAxisValues, My.Settings.chart2Serie2Color, My.Settings.chart2Serie2Type, p_alpha)
        FillSerie(My.Settings.chart3Serie1AccountId, 2, p_versionId, p_versionName, p_xAxisValues, My.Settings.chart3Serie1Color, My.Settings.chart3Serie1Type, p_alpha)
        FillSerie(My.Settings.chart3Serie2AccountId, 2, p_versionId, p_versionName, p_xAxisValues, My.Settings.chart3Serie2Color, My.Settings.chart3Serie2Type, p_alpha)
        FillSerie(My.Settings.chart4Serie1AccountId, 3, p_versionId, p_versionName, p_xAxisValues, My.Settings.chart4Serie1Color, My.Settings.chart4Serie1Type, p_alpha)
        FillSerie(My.Settings.chart4Serie2AccountId, 3, p_versionId, p_versionName, p_xAxisValues, My.Settings.chart4Serie2Color, My.Settings.chart4Serie2Type, p_alpha)

    End Sub

    Private Sub FillSerie(ByRef p_accountId As Int32, _
                          ByRef p_chartIndex As UInt16, _
                          ByRef p_versionId As Int32, _
                          ByRef p_versionName As String, _
                          ByRef p_xAxisValues As String(), _
                          ByRef p_serieColor As System.Drawing.Color, _
                          ByRef p_serieType As Int32, _
                          ByRef p_alpha As Single)
        Dim l_account As Account = GlobalVariables.Accounts.GetValue(p_accountId)

        If Not l_account Is Nothing Then
            Dim serieName = l_account.Name & p_versionName
            m_chartsView.BindData_ThreadSafe(p_chartIndex, serieName, p_xAxisValues, BuildSerieYValues(p_accountId, p_versionId))
            m_chartsView.FormatSerie_ThreadSafe(p_chartIndex, p_serieColor, p_serieType, serieName, p_alpha)
        End If

    End Sub

    Private Function BuildSerieYValues(ByRef p_accountId As Int32, _
                                       ByRef p_versionId As Int32) As Double()

        Dim nbPeriods As Int32 = m_view.m_leftPaneControl.periodsTV.Nodes.Count - 1
        Dim yValues(nbPeriods) As Double
        Dim entityId As Int32 = m_entityNode.Value
        Dim filterId As String = "0"
        Dim periodId As String = ""
        Dim token As String

        Dim i As Int32
        For Each periodNode As vTreeNode In m_view.m_leftPaneControl.periodsTV.Nodes
            periodId = periodNode.Value
            token = p_versionId & computer.TOKEN_SEPARATOR & _
                    filterId & computer.TOKEN_SEPARATOR & _
                    entityId & computer.TOKEN_SEPARATOR & _
                    p_accountId & computer.TOKEN_SEPARATOR & _
                    periodId
            If m_dataMap.ContainsKey(token) _
            AndAlso Not Double.IsNaN(m_dataMap(token)) _
            AndAlso Not Double.IsInfinity(m_dataMap(token)) Then
                yValues(i) = m_dataMap(token)
                i += 1
            End If
        Next
        Return yValues

    End Function

    Private Function GetSerieXValues() As String()

        Dim nbPeriods As Int32 = m_view.m_leftPaneControl.periodsTV.Nodes.Count - 1
        Dim xValues(nbPeriods) As String
        Dim i As Int32 = 0
        For Each node As vTreeNode In m_view.m_leftPaneControl.periodsTV.Nodes
            xValues(i) = node.Text
            i += 1
        Next
        Return xValues

    End Function

    Friend Sub LaunchMainUIRefreshFromCharts()

        m_view.RefreshData()

    End Sub

#End Region


#Region "Utilities"

    Private Sub FillUIHeader()

        m_view.EntityTB.Text = m_entityNode.Text
        m_view.CurrencyTB.Text = m_view.m_leftPaneControl.currenciesCLB.SelectedItem.Text
        m_view.VersionTB.Text = String.Join(" ; ", m_versionsDict.Values)

        m_chartsView.EntityTB.Text = m_entityNode.Text
        m_chartsView.CurrencyTB.Text = m_view.m_leftPaneControl.currenciesCLB.SelectedItem.Text
        m_chartsView.VersionTB.Text = String.Join(" ; ", m_versionsDict.Values)

    End Sub

    Friend Sub DropOnExcel(ByRef p_copyOnlyExpanded As Boolean)

        On Error Resume Next
        If Not m_entityNode Is Nothing Then
            Dim destination As Microsoft.Office.Interop.Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS(m_entityNode.Text, _
                                                                                           {"Entity", "Version", "Currency"}, _
                                                                                           {m_entityNode.Text, m_view.VersionTB.Text, m_view.CurrencyTB.Text})
            Dim i As Int32 = 1
            For Each tab_ As VIBlend.WinForms.Controls.vTabPage In m_view.DGVsControlTab.TabPages
                Dim DGV As VIBlend.WinForms.DataGridView.vDataGridView = tab_.Controls(0)
                DataGridViewsUtil.CopyDGVToExcelGeneric(DGV, destination, i, p_copyOnlyExpanded)
            Next
            destination.Worksheet.Columns.AutoFit()
            destination.Worksheet.Outline.ShowLevels(RowLevels:=1)
        End If
        GlobalVariables.APPS.ActiveWindow.Activate()

    End Sub

    Private Function GetFirstVersionId(ByRef versionId As String) As Object

        Dim sepIndex = versionId.IndexOf(computer.TOKEN_SEPARATOR)
        Return Left(versionId, sepIndex)

    End Function

    Private Function GetSecondVersionId(ByRef versionId As String) As Object

        Dim sepIndex = versionId.IndexOf(computer.TOKEN_SEPARATOR)
        Return Right(versionId, Len(versionId) - sepIndex - 1)

    End Function

    Friend Function GetAccountTypeFromId(ByRef p_accountId As UInt32) As Account.AccountType

        Dim l_account As Account = GlobalVariables.Accounts.GetValue(p_accountId)
        If Not l_account Is Nothing Then
            Return l_account.Type
        End If
        Return 0

    End Function

    Friend Function GetAccountFormatFromId(ByRef p_accountId As UInt32) As String

        Dim l_account As Account = GlobalVariables.Accounts.GetValue(p_accountId)
        If Not l_account Is Nothing Then
            Return l_account.FormatId
        End If
        Return ""

    End Function

    Private Function GetNumberOfRows() As Int32

        Dim nbRows As Int32 = 1
        For Each node In m_rowsHierarchyNode.Nodes
            nbRows *= node.Nodes.Count
        Next
        Return nbRows

    End Function

#End Region



End Class
