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
' Last modified: 16/09/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq
Imports VIBlend.Utilities
Imports VIBlend.WinForms.Controls

Friend Class ControllingUIController


#Region "Instance Variable"

    ' Objects
    Private Computer As New Computer
    Private View As ControllingUI_2
    Friend EntityNode As vTreeNode
    Private vCompNode As TreeNode
    Private computingCache As New ComputingCache(True)

    ' Variables
    Private currenciesNameIdDict As Dictionary(Of String, Int32)
    Private accounts_id_shortlist As List(Of Int32)
    Private rowsHierarchyNode As New vTreeNode
    Private columnsHierarchyNode As New vTreeNode
    Private display_axis_ht As New Hashtable
    Private filtersAndAxisDict As New Dictionary(Of String, List(Of Int32))
    Private dataMap As Dictionary(Of String, Double)
    Private filters_dict As New Dictionary(Of String, Int32)
    Private filtersNodes As New vTreeNode
    Private VersionsTV As New vTreeView
    Friend versionsDict As New Dictionary(Of Int32, String)
    Friend initDisplayFlag As Boolean = False
    Friend computedFlag As Boolean = False
    Friend isComputingFlag As Boolean
    Private m_chartsView As CUI2Visualization
    Private m_chartsViewContainer As New CUI2VisualizationContainer

    ' Virtual binding
    Private itemsDimensionsDict As Dictionary(Of HierarchyItem, Hashtable)
    Friend cellsUpdateNeeded As Boolean = True

#End Region


#Region "Initialization"

    Friend Sub New(ByRef inputView As Object)

        View = inputView
        LoadSpecialFiltersValuesNode()

        m_chartsView = New CUI2Visualization(Me)
        m_chartsViewContainer.Controls.Add(m_chartsView)
        m_chartsView.Dock = DockStyle.Fill
    
        AddHandler Computer.ComputationAnswered, AddressOf AfterCompute

    End Sub

    Private Sub LoadSpecialFiltersValuesNode()

        ' Load Filters Nodes
        For Each filterId As Int32 In GlobalVariables.Filters.filters_hash.Keys
            Dim filterNodeName As String = Computer.FILTERS_DECOMPOSITION_IDENTIFIER & filterId
            Dim filterNodeText As String = GlobalVariables.Filters.filters_hash(filterId)(NAME_VARIABLE)
            Dim filterNode As vTreeNode = VTreeViewUtil.AddNode(filterNodeName, filterNodeText, filtersNodes)

            Dim filterValuesDict As Hashtable = GlobalVariables.FiltersValues.GetFiltervaluesDictionary(filterId, _
                                                                                                        ID_VARIABLE, _
                                                                                                        NAME_VARIABLE)
            For Each filterValueId As Int32 In filterValuesDict.Keys
                VTreeViewUtil.AddNode(filterValueId, filterValuesDict(filterValueId), filterNode)
            Next

        Next

        ' Load Clients Nodes
        Dim clientsNode As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS, _
                                                             ControllingUI_2.CLIENTS_CODE, _
                                                             filtersNodes)
        For Each clientId As Int32 In GlobalVariables.Clients.Axis_hash.Keys
            VTreeViewUtil.AddNode(clientId, GlobalVariables.Clients.Axis_hash(clientId)(NAME_VARIABLE), clientsNode)
        Next

        ' Load Products Nodes
        Dim productsNode As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.PRODUCTS, _
                                                              ControllingUI_2.PRODUCTS_CODE, _
                                                              filtersNodes)
        For Each productId As Int32 In GlobalVariables.Products.Axis_hash.Keys
            VTreeViewUtil.AddNode(productId, GlobalVariables.Products.Axis_hash(productId)(NAME_VARIABLE), productsNode)
        Next

        ' Load Adjustment Nodes
        Dim adjustmentsNode As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ADJUSTMENTS, _
                                                                 ControllingUI_2.ADJUSTMENT_CODE, _
                                                                 filtersNodes)
        For Each adjustmentId As Int32 In GlobalVariables.Adjustments.Axis_hash.Keys
            VTreeViewUtil.AddNode(adjustmentId, GlobalVariables.Adjustments.Axis_hash(adjustmentId)(NAME_VARIABLE), adjustmentsNode)
        Next

    End Sub

#End Region


#Region "Computing"

    Friend Sub Compute(ByRef versionIDs() As Int32, _
                       ByRef inputEntityNode As vTreeNode, _
                       Optional ByRef useCache As Boolean = False)

        If (isComputingFlag = True) Then Exit Sub
        View.SetComputeButtonState(False)
        isComputingFlag = True
        computedFlag = False

        If Not dataMap Is Nothing Then dataMap.Clear()
        dataMap = Nothing
        EntityNode = inputEntityNode
        ' View.ClearDGVs()
        rowsHierarchyNode.Nodes.Clear()
        columnsHierarchyNode.Nodes.Clear()

        ' Versions init
        versionsDict.Clear()
        For Each version_Id As Int32 In versionIDs
            Dim versionNode As vTreeNode = VTreeViewUtil.FindNode(View.leftPane_control.versionsTV, version_Id)
            versionsDict.Add(versionNode.Value, versionNode.Text)
        Next

        If versionsDict.Count > 1 _
        AndAlso View.rightPane_Control.DimensionsListContainsItem(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS) = False Then
            View.rightPane_Control.AddItemToColumnsHierarchy(ControllingUI_2.VERSIONS_CODE, _
                                                           Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS)
        End If

        initDisplayFlag = False
        For Each item In View.rightPane_Control.rowsDisplayList.Items
            VTreeViewUtil.AddNode(item.Value, item.Text, rowsHierarchyNode)
        Next
        For Each item In View.rightPane_Control.columnsDisplayList.Items
            VTreeViewUtil.AddNode(item.Value, item.Text, columnsHierarchyNode)
        Next

        ' Filters and axis Filters Build
        Dim filters As Dictionary(Of Int32, List(Of Int32)) = GetFilters()
        Dim axisFilters As Dictionary(Of Int32, List(Of Int32)) = GetAxisFilters()

        ' Decomposition Hierarchy build
        Dim computingHierarchyList As New List(Of String)
        IncrementComputingHierarchy(rowsHierarchyNode, computingHierarchyList)
        IncrementComputingHierarchy(columnsHierarchyNode, computingHierarchyList)

        ' Currency Setup
        If View.leftPane_control.currenciesCLB.SelectedItem Is Nothing Then
            MsgBox("Please select a Currency.")
            Exit Sub
        End If
        Dim currencyId As Int32 = CInt(View.leftPane_control.currenciesCLB.SelectedItem.Value)

        ' Computing order
        Dim mustCompute As Boolean = True
        If useCache = True AndAlso computingCache.MustCompute(EntityNode.Value, _
                                                             currencyId, _
                                                              versionIDs, _
                                                              filters, _
                                                              axisFilters, _
                                                              computingHierarchyList) = False Then mustCompute = False
        If mustCompute = True Then

            If computingHierarchyList.Count = 0 Then computingHierarchyList = Nothing
            Computer.CMSG_COMPUTE_REQUEST(versionIDs, _
                                          CInt(EntityNode.Value), _
                                          currencyId, _
                                          filters, _
                                          axisFilters, _
                                          computingHierarchyList)
        End If

        ' Cache registering
        computingCache.cacheEntityID = CInt(EntityNode.Value)
        computingCache.cacheCurrencyId = currencyId
        computingCache.cacheVersions = versionIDs
        computingCache.cacheComputingHierarchyList = computingHierarchyList
        computingCache.cacheFilters = filters
        computingCache.cacheAxisFilters = axisFilters

        ' Redraw hierarchy Items
        InitDisplay()
        FillUIHeader()

    End Sub

    Private Sub AfterCompute()

        While initDisplayFlag = False
        End While
        View.FormatDGV_ThreadSafe()
        dataMap = Computer.GetData()
        StubFillingChart()
        computedFlag = True
        View.TerminateCircularProgress()
        isComputingFlag = False
        View.SetComputeButtonState(True)

    End Sub

    Private Sub IncrementComputingHierarchy(ByRef hierarchyNode As vTreeNode, _
                                            ByRef computingHierarchyList As List(Of String))

        For Each node As vTreeNode In hierarchyNode.Nodes
            If node.Value <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS _
            AndAlso node.Value <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES _
            AndAlso node.Value <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS _
            AndAlso node.Value <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS _
            AndAlso node.Value <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS _
            AndAlso node.Value <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS Then
                computingHierarchyList.Add(node.Value)
            End If
        Next

    End Sub

#End Region


#Region "Display Initialization"

    Private Sub InitDisplay()

        filters_dict.Clear()
        itemsDimensionsDict = New Dictionary(Of HierarchyItem, Hashtable)
        FillHierarchy(rowsHierarchyNode)
        FillHierarchy(columnsHierarchyNode)

        For Each tab_ As VIBlend.WinForms.Controls.vTabPage In View.DGVsControlTab.TabPages
            '  View.DGVsControlTab.SelectedTab = tab_
            Dim DGV As vDataGridView = tab_.Controls(0)
            RemoveHandler DGV.CellValueNeeded, AddressOf DGVs_CellValueNeeded
            DGV.Clear()
            accounts_id_shortlist = VTreeViewUtil.GetNodesIds(VTreeViewUtil.FindNode(View.accountsTV, tab_.Name))
            accounts_id_shortlist.Remove(tab_.Name)

            ' Display_axis_values Initialization 
            display_axis_ht.Clear()
            display_axis_ht(GlobalEnums.DataMapAxis.ACCOUNTS) = 0
            display_axis_ht(GlobalEnums.DataMapAxis.PERIODS) = ""
            display_axis_ht(GlobalEnums.DataMapAxis.FILTERS) = "0"
            display_axis_ht(GlobalEnums.DataMapAxis.ENTITIES) = CInt(EntityNode.Value)
            If versionsDict.Keys.Count = 1 Then
                display_axis_ht(GlobalEnums.DataMapAxis.VERSIONS) = CInt(versionsDict.Keys(0))
            Else
                display_axis_ht(GlobalEnums.DataMapAxis.VERSIONS) = 0
            End If

            ' keep track of dimension id / column ? priority normal !!!
            ' /////////////////////////////// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            DGV.RowsHierarchy.Clear()
            DGV.ColumnsHierarchy.Clear()
            If columnsHierarchyNode.Nodes.Count > 0 Then CreateColumn(DGV, columnsHierarchyNode.Nodes(0))
            If rowsHierarchyNode.Nodes.Count > 0 Then CreateRow(DGV, rowsHierarchyNode.Nodes(0))
            DGV.ColumnsHierarchy.AutoStretchColumns = True
            AddHandler DGV.CellValueNeeded, AddressOf DGVs_CellValueNeeded
        Next
        initDisplayFlag = True

    End Sub

    Private Sub FillHierarchy(ByRef hierarchyNode As vTreeNode)

        HierarchyListPeriodsTreatment(hierarchyNode)

        For Each node As vTreeNode In hierarchyNode.Nodes
            Select Case node.Value
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                    For Each Entity_node As vTreeNode In EntityNode.Nodes
                        VTreeViewUtil.CopySubNodes(Entity_node, node)
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                    ' Accounts hierarchy if not first item in hierarchy !!!
                    ' !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    ' priority high
                    ' set an option or set a rule !! 
                    If True Then
                        For Each accountNode As vTreeNode In View.accountsTV.Nodes
                            For Each subAccountNode As vTreeNode In accountNode.Nodes
                                VTreeViewUtil.CopySubNodes(subAccountNode, node)
                            Next
                        Next
                    Else
                        For Each account_node As vTreeNode In View.accountsTV.GetNodes
                            VTreeViewUtil.AddNode(account_node.Value, account_node.Text, node)
                        Next
                    End If

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                    For Each version_id In versionsDict.Keys
                        VTreeViewUtil.AddNode(version_id, versionsDict(version_id), node)
                    Next
                    If versionsDict.Count = 2 Then
                        VTreeViewUtil.AddNode(versionsDict.Keys(0) & Computer.TOKEN_SEPARATOR & versionsDict.Keys(1), _
                                              versionsDict.Values(0) & Computer.TOKEN_SEPARATOR & versionsDict.Values(1), _
                                              node)
                    Else
                        vCompNode = Nothing
                    End If

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    For Each yearId As Int32 In GlobalVariables.Versions.GetYears(versionsDict)
                        VTreeViewUtil.AddNode(Computer.YEAR_PERIOD_IDENTIFIER & yearId, Format(Date.FromOADate(yearId), "yyyy"), node)
                    Next
                    View.leftPane_control.SetupPeriodsTV(node)

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    For Each monthId As Int32 In GlobalVariables.Versions.GetMonths(versionsDict)
                        VTreeViewUtil.AddNode(Computer.MONTH_PERIOD_IDENTIFIER & monthId, Format(Date.FromOADate(monthId), "MMM yyyy"), node)
                    Next
                    View.leftPane_control.SetupPeriodsTV(node)

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS
                    Dim periodsDict = GlobalVariables.Versions.GetPeriodsDictionary(versionsDict)
                    For Each yearId As Int32 In periodsDict.Keys
                        Dim yearNode As vTreeNode = VTreeViewUtil.AddNode(Computer.YEAR_PERIOD_IDENTIFIER & yearId, _
                                                                          Format(Date.FromOADate(yearId), "yyyy"), _
                                                                          node)
                        For Each monthId As Int32 In periodsDict(yearId)
                            VTreeViewUtil.AddNode(Computer.MONTH_PERIOD_IDENTIFIER & monthId, Format(Date.FromOADate(monthId), "MMM yyyy"), yearNode)
                        Next
                    Next
                    View.leftPane_control.SetupPeriodsTV(node)

                Case Else
                    For Each value_node As vTreeNode In VTreeViewUtil.FindNode(filtersNodes, node.Value).Nodes
                        VTreeViewUtil.AddNode(value_node.Value, value_node.Text, node)
                    Next

            End Select
        Next

    End Sub

    Private Sub HierarchyListPeriodsTreatment(ByRef hierarchyNode As vTreeNode)

        ' Build period related hierarchy nodes
        Dim periodsNodes As New vTreeNode
        For Each node As vTreeNode In hierarchyNode.Nodes
            Select Case node.Value
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    VTreeViewUtil.AddNode(1, 1, periodsNodes)
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    VTreeViewUtil.AddNode(2, 2, periodsNodes)
            End Select
        Next

        ' Analyze present periods dimensions and order
        If periodsNodes.Nodes.Count = 2 Then
            Select Case periodsNodes.Nodes(0).Value & periodsNodes.Nodes(1).Value
                Case "12"
                    ' if years before months => only keep months but with special code
                    Dim monthsNode As vTreeNode = VTreeViewUtil.FindNode(hierarchyNode, Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS)
                    monthsNode.Value = Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS
                    hierarchyNode.Nodes.Remove(VTreeViewUtil.FindNode(hierarchyNode, Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS))
                Case "21"
                    ' if years after months => delete years
                    hierarchyNode.Nodes.Remove(VTreeViewUtil.FindNode(hierarchyNode, Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS))
            End Select
        End If

    End Sub

    Private Sub CreateColumn(ByRef dgv As vDataGridView, _
                             ByRef dimensionNode As vTreeNode, _
                             Optional ByRef valueNode As vTreeNode = Nothing, _
                             Optional ByRef column As HierarchyItem = Nothing)

        Dim subColumn As HierarchyItem
        If valueNode Is Nothing Then
            subColumn = column
            ' Loop through values
            For Each subNode In dimensionNode.Nodes
                CreateColumn(dgv, dimensionNode, subNode, subColumn)
            Next
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
                View.FormatDGVItem(subColumn)
                subColumn.TextAlignment = Drawing.ContentAlignment.MiddleCenter
                RegisterHierarchyItemDimensions(subColumn)
                ' ------------------------------------------------------------------------------

                ' Dig one level deeper if any
                If Not dimensionNode.NextSiblingNode Is Nothing Then
                    CreateColumn(dgv, dimensionNode.NextNode, , subColumn)
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
            ' Loop through values
            For Each subNode In dimensionNode.Nodes
                CreateRow(dgv, dimensionNode, subNode, row)
            Next
        Else
            'Set current value for current display axis
            If SetDisplayAxisValue(dimensionNode, valueNode) = True Then

                If row Is Nothing Then
                    subRow = dgv.RowsHierarchy.Items.Add(valueNode.Text)
                Else
                    subRow = row.Items.Add(valueNode.Text)
                End If
                subRow.ItemValue = display_axis_ht(GlobalEnums.DataMapAxis.ACCOUNTS)
                subRow.CellsDataSource = GridCellDataSource.Virtual
                View.FormatDGVItem(subRow)
                HideHiearchyItemIfVComp(subRow, _
                                        dimensionNode, _
                                        valueNode)
                RegisterHierarchyItemDimensions(subRow)

                ' Dig one level deeper if any
                If Not dimensionNode.NextSiblingNode Is Nothing Then
                    CreateRow(dgv, dimensionNode.NextNode, , subRow)
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
            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                ' In case display_axis is accounts we should only display the accounts belonging to the current accounts tab
                If accounts_id_shortlist.Contains(CInt(valueNode.Value)) = True Then
                    display_axis_ht(GlobalEnums.DataMapAxis.ACCOUNTS) = CInt(valueNode.Value)
                Else
                    Return False
                End If

            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                display_axis_ht(GlobalEnums.DataMapAxis.ENTITIES) = CInt(valueNode.Value)

            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                display_axis_ht(GlobalEnums.DataMapAxis.VERSIONS) = valueNode.Value

            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, _
                 Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS, _
                 Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS
                display_axis_ht(GlobalEnums.DataMapAxis.PERIODS) = valueNode.Value

            Case Else
                ' Filters (clients, products, adjustments, oter filters)
                ' In case display_axis is filters we just add the filter_value_id to the filters_values_id_list
                ' Possible values are analysis_axis except entities
                filters_dict(dimensionNode.Value) = valueNode.Value

        End Select
        Return True

    End Function

    Private Sub LevelDimensionFilterOrAxis(ByRef dimensionNode As vTreeNode)

        Dim findNode As vTreeNode = VTreeViewUtil.FindNode(filtersNodes, dimensionNode.Value)
        If Not findNode Is Nothing Then
            filters_dict.Remove(dimensionNode.Value)                 ' remove filter from filters dic
        Else
            Select Case dimensionNode.Value
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                    display_axis_ht(GlobalEnums.DataMapAxis.ENTITIES) = CInt(EntityNode.Value)

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                    display_axis_ht(GlobalEnums.DataMapAxis.VERSIONS) = 0

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                    display_axis_ht(GlobalEnums.DataMapAxis.ACCOUNTS) = 0

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    display_axis_ht(GlobalEnums.DataMapAxis.PERIODS) = ""

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    display_axis_ht(GlobalEnums.DataMapAxis.PERIODS) = ""

            End Select
        End If

    End Sub

    Private Sub RegisterHierarchyItemDimensions(ByRef item As HierarchyItem)

        itemsDimensionsDict.Add(item, New Hashtable)
        display_axis_ht(GlobalEnums.DataMapAxis.FILTERS) = Computer.GetFiltersToken(filters_dict)
        For Each key In display_axis_ht.Keys
            itemsDimensionsDict(item)(key) = display_axis_ht(key)
        Next

    End Sub

#End Region


#Region "Events"

    Friend Sub DGVs_CellValueNeeded(ByVal sender As Object, ByVal args As CellValueNeededEventArgs)

        ' priority high -> no update if alraedy displayed !!!! 
        '---------------------------------------------------------
        If Not dataMap Is Nothing Then

            Dim accountId As Int32 = 0
            Dim entityId As Int32 = 0
            Dim periodId As String = ""
            Dim versionId As String = 0
            Dim filterId As String = "0"

            Dim items() As HierarchyItem = {args.RowItem, args.ColumnItem}
            For Each item As HierarchyItem In items
                '  If itemsDimensionsDict(item).Count = 0 then Exit sub
                Dim ht As Hashtable = itemsDimensionsDict(item)
                For Each dimension In ht.Keys
                    Dim value = ht(dimension)
                    If Not TypeOf (dimension) Is Char Then
                        Select Case dimension
                            Case GlobalEnums.DataMapAxis.ACCOUNTS
                                If value <> 0 Then accountId = value
                            Case GlobalEnums.DataMapAxis.ENTITIES
                                If value <> 0 Then
                                    Select Case entityId
                                        Case 0
                                            entityId = value
                                        Case Else
                                            If entityId = CInt(EntityNode.Value) Then entityId = value
                                    End Select
                                End If
                            Case GlobalEnums.DataMapAxis.PERIODS
                                If value <> "" Then periodId = value
                            Case GlobalEnums.DataMapAxis.VERSIONS
                                If value <> "0" Then versionId = value
                            Case GlobalEnums.DataMapAxis.FILTERS
                                If value <> "0" Then filterId = value
                        End Select
                    End If
                Next
            Next

            Dim token As String = Computer.TOKEN_SEPARATOR & _
                                  filterId & Computer.TOKEN_SEPARATOR & _
                                  entityId & Computer.TOKEN_SEPARATOR & _
                                  accountId & Computer.TOKEN_SEPARATOR & _
                                  periodId
            If versionId.Contains(Computer.TOKEN_SEPARATOR) Then
                Dim v1 As Int32 = GetFirstVersionId(versionId)
                Dim v2 As Int32 = GetSecondVersionId(versionId)

                If dataMap.ContainsKey(v1 & token) _
                AndAlso dataMap.ContainsKey(v2 & token) Then
                    args.CellValue = dataMap(v1 & token) - dataMap(v2 & token)
                    If Double.IsNaN(args.CellValue) Then args.CellValue = "-"
                    If My.Settings.controllingUIResizeTofitGrid = True Then
                        args.ColumnItem.DataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
                        ' args.ColumnItem.AutoResize(AutoResizeMode.FIT_ALL)
                    End If
                Else
                    args.CellValue = ""
                End If
            Else
                If dataMap.ContainsKey(versionId & token) Then
                    args.CellValue = dataMap(versionId & token)
                    If Double.IsNaN(args.CellValue) Then args.CellValue = "-"
                    If My.Settings.controllingUIResizeTofitGrid = True Then
                        args.ColumnItem.DataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
                        ' args.ColumnItem.AutoResize(AutoResizeMode.FIT_ALL)
                    End If
                Else
                    args.CellValue = ""
                    If (GlobalVariables.Accounts.m_accountsHash(accountId)(ACCOUNT_FORMULA_TYPE_VARIABLE) <> 5) Then
                        args.RowItem.ParentItem.Items.Remove(args.RowItem)
                    End If
                End If
            End If
        End If

    End Sub

#End Region


#Region "Versions Comparison and Periods Display"

    Private Sub HideHiearchyItemIfVComp(ByRef item As HierarchyItem, _
                                       ByRef dimensionNode As vTreeNode, _
                                       ByRef valueNode As vTreeNode)

        If dimensionNode.Value = Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS _
         AndAlso valueNode.Value.Contains(Computer.TOKEN_SEPARATOR) Then
            item.Hidden = True
        End If

    End Sub

    Friend Sub VersionsCompDisplay(ByVal display As Boolean)

        For Each tab_ As VIBlend.WinForms.Controls.vTabPage In View.DGVsControlTab.TabPages
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

        If item.Caption.Contains(Computer.TOKEN_SEPARATOR) Then
            item.Hidden = display
        Else
            For Each subItem As HierarchyItem In item.Items
                DisplayVCompHierarchy(subItem, display)
            Next
        End If

    End Sub

    Friend Sub ReverseVersionsComparison()

        If vCompNode Is Nothing Then Exit Sub
        Dim v1 As String = GetFirstVersionId(vCompNode.Name)
        Dim v2 As String = GetSecondVersionId(vCompNode.Name)
        vCompNode.Name = v2 & Computer.TOKEN_SEPARATOR & v1

        v1 = GetFirstVersionId(vCompNode.Text)
        v2 = GetSecondVersionId(vCompNode.Text)
        vCompNode.Text = v2 & Computer.TOKEN_SEPARATOR & v1

        ' Then launch rows and column hierarchies redrawing 
        '   => priority normal !!!


    End Sub

    Friend Sub PeriodsSelectionFilter(ByRef periodsSelectionDict As Dictionary(Of String, Boolean))

        For Each tab_ As vTabPage In View.DGVsControlTab.TabPages
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

        Dim axisFilters As New Dictionary(Of Int32, List(Of Int32))

        AddAxisFilterFromTV(View.leftPane_control.entitiesTV, GlobalEnums.AnalysisAxis.ENTITIES, axisFilters)
        AddAxisFilterFromTV(View.leftPane_control.clientsTV, GlobalEnums.AnalysisAxis.CLIENTS, axisFilters)
        AddAxisFilterFromTV(View.leftPane_control.productsTV, GlobalEnums.AnalysisAxis.PRODUCTS, axisFilters)
        AddAxisFilterFromTV(View.leftPane_control.adjustmentsTV, GlobalEnums.AnalysisAxis.ADJUSTMENTS, axisFilters)

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

        Dim filters As New Dictionary(Of Int32, List(Of Int32))

        AddFiltersFromTV(View.leftPane_control.entitiesFiltersTV, _
                         GlobalEnums.AnalysisAxis.ENTITIES, _
                         filters)
        AddFiltersFromTV(View.leftPane_control.clientsFiltersTV, _
                         GlobalEnums.AnalysisAxis.CLIENTS, _
                         filters)
        AddFiltersFromTV(View.leftPane_control.productsFiltersTV, _
                         GlobalEnums.AnalysisAxis.PRODUCTS, _
                         filters)
        AddFiltersFromTV(View.leftPane_control.adjustmentsFiltersTV, _
                         GlobalEnums.AnalysisAxis.ADJUSTMENTS, _
                         filters)

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

    Friend Sub StubFillingChart()

        m_chartsView.ClearCharts_ThreadSafe()
        Dim xAxisValues As String() = GetSerieXValues()
        m_chartsView.BindData_ThreadSafe(0, "Chiffre d'affaires", xAxisValues, BuildSerieYValues(2))
        m_chartsView.BindData_ThreadSafe(0, "EBIDTA", xAxisValues, BuildSerieYValues(9))
        m_chartsView.BindData_ThreadSafe(1, "Investissements", xAxisValues, BuildSerieYValues(89))
        m_chartsView.BindData_ThreadSafe(2, "Cash-Flow", xAxisValues, BuildSerieYValues(349))
        m_chartsView.BindData_ThreadSafe(2, "Trésorerie", xAxisValues, BuildSerieYValues(41))
        m_chartsView.BindData_ThreadSafe(3, "Levier Financier (D/E) %", xAxisValues, BuildSerieYValues(358))
        m_chartsView.BindData_ThreadSafe(3, "Rentabilité (ROCE) %", xAxisValues, BuildSerieYValues(353))

        m_chartsView.StubDemosFormatting_ThreadSafe()

    End Sub

    Private Function BuildSerieYValues(ByRef p_accountId As Int32) As Double()

        Dim nbPeriods As Int32 = View.leftPane_control.periodsTV.Nodes.Count - 1
        Dim yValues(nbPeriods) As Double
        Dim entityId As Int32 = EntityNode.Value
        Dim versionId As Int32 = CInt(versionsDict.Keys(0))
        Dim filterId As String = "0"
        Dim periodId As String = ""
        Dim token As String

        Dim i As Int32
        For Each periodNode As vTreeNode In View.leftPane_control.periodsTV.Nodes
            periodId = periodNode.Value
            token = versionId & Computer.TOKEN_SEPARATOR & _
                    filterId & Computer.TOKEN_SEPARATOR & _
                    entityId & Computer.TOKEN_SEPARATOR & _
                    p_accountId & Computer.TOKEN_SEPARATOR & _
                    periodId
            If dataMap.ContainsKey(token) _
            AndAlso Not Double.IsNaN(dataMap(token)) Then
                yValues(i) = dataMap(token)
                i += 1
            End If
        Next
        Return yValues

    End Function

    Private Function GetSerieXValues() As String()

        Dim nbPeriods As Int32 = View.leftPane_control.periodsTV.Nodes.Count - 1
        Dim xValues(nbPeriods) As String
        Dim i As Int32 = 0
        For Each node As vTreeNode In View.leftPane_control.periodsTV.Nodes
            xValues(i) = node.Text
            i += 1
        Next
        Return xValues

    End Function


#End Region


#Region "Utilities"

    Private Sub FillUIHeader()

        View.EntityTB.Text = EntityNode.Text
        View.CurrencyTB.Text = View.leftPane_control.currenciesCLB.SelectedItem.Text
        View.VersionTB.Text = String.Join(" ; ", versionsDict.Values)

        m_chartsView.EntityTB.Text = EntityNode.Text
        m_chartsView.CurrencyTB.Text = View.leftPane_control.currenciesCLB.SelectedItem.Text
        m_chartsView.VersionTB.Text = String.Join(" ; ", versionsDict.Values)

    End Sub

    Friend Sub dropOnExcel()

        ' should be elsewhere !!!! 
        ' priority normal
        ' Maybe issue if nothing in the DGV ? !
        ' reimplement ??  priority normal
        On Error Resume Next
        If Not EntityNode Is Nothing Then
            Dim destination As Microsoft.Office.Interop.Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS(EntityNode.Text, _
                                                                                           {"Entity", "Version", "Currency"}, _
                                                                                           {EntityNode.Text, View.VersionTB.Text, View.CurrencyTB.Text})
            Dim i As Int32 = 1
            For Each tab_ As VIBlend.WinForms.Controls.vTabPage In View.DGVsControlTab.TabPages
                Dim DGV As VIBlend.WinForms.DataGridView.vDataGridView = tab_.Controls(0)
                DataGridViewsUtil.CopyDGVToExcelGeneric(DGV, destination, i)
            Next
            destination.Worksheet.Columns.AutoFit()
            destination.Worksheet.Outline.ShowLevels(RowLevels:=1)
        End If

    End Sub

    Private Function GetFirstVersionId(ByRef versionId As String) As Object

        Dim sepIndex = versionId.IndexOf(Computer.TOKEN_SEPARATOR)
        Return Left(versionId, sepIndex)

    End Function

    Private Function GetSecondVersionId(ByRef versionId As String) As Object

        Dim sepIndex = versionId.IndexOf(Computer.TOKEN_SEPARATOR)
        Return Right(versionId, Len(versionId) - sepIndex - 1)

    End Function

    Friend Function GetAccountTypeFromId(ByRef p_accountId As Int32) As Int32

        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(p_accountId) Then
            Return GlobalVariables.Accounts.m_accountsHash(p_accountId)(ACCOUNT_TYPE_VARIABLE)
        End If
        Return 0

    End Function

    Friend Function GetAccountFormatFromId(ByRef p_accountId As Int32) As String

        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(p_accountId) Then
            Return GlobalVariables.Accounts.m_accountsHash(p_accountId)(ACCOUNT_FORMAT_VARIABLE)
        End If
        Return ""

    End Function

#End Region



End Class
