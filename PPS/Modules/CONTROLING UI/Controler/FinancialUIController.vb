﻿' FinancialUIController.vb
'
' Controlling User iterface: Compurte/ Display data and consolidated data
'
' To do:
'      - 
'   
'
' Known bugs:
'
'
' Author: Julien Monnereau
' Last modified: 11/08/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq


Friend Class FinancialUIController


#Region "Instance Variable"

    ' Objects
    Private Computer As New Computer
    Private View As ControllingUI_2
    Friend EntityNode As TreeNode

    ' Variables
    Private currenciesNameIdDict As Dictionary(Of String, Int32)
    Private accounts_id_shortlist As List(Of Int32)
    Private rowsHierarchyNode As New TreeNode
    Private columnsHierarchyNode As New TreeNode
    Private display_axis_ht As New Hashtable
    Private filtersAndAxisDict As New Dictionary(Of String, List(Of Int32))
    Private dataMap As Dictionary(Of String, Double)
    Private filters_dict As New Dictionary(Of String, Int32)
    Private filtersNodes As New TreeNode
    Private VersionsTV As New TreeView
    Friend versionsDict As New Dictionary(Of Int32, String)
    Friend initDisplayFlag As Boolean = False

    ' Cache
    Private cacheEntityID As Int32
    Private cacheCurrencyId As Int32
    Private cacheVersions() As Int32
    Private cacheComputingHierarchyList As List(Of String)
    Private cacheFilters As Dictionary(Of Int32, List(Of Int32))
    Private cacheAxisFilters As Dictionary(Of Int32, List(Of Int32))

    ' Virtual binding
    Private itemsDimensionsDict As New Dictionary(Of HierarchyItem, Hashtable)


#End Region


#Region "Initialization"

    Friend Sub New(ByRef inputView As Object)

        View = inputView
        LoadSpecialFiltersValuesNode()
        AddHandler Computer.ComputationAnswered, AddressOf AfterCompute

    End Sub

    Private Sub LoadSpecialFiltersValuesNode()

        ' Load Filters Nodes
        For Each filterId As Int32 In GlobalVariables.Filters.filters_hash.Keys
            Dim filterNodeName As String = Computer.FILTERS_DECOMPOSITION_IDENTIFIER & filterId
            Dim filterNodeText As String = GlobalVariables.Filters.filters_hash(filterId)(NAME_VARIABLE)
            Dim filterNode As TreeNode = filtersNodes.Nodes.Add(filterNodeName, filterNodeText)

            Dim filterValuesDict As Hashtable = GlobalVariables.FiltersValues.GetFiltervaluesDictionary(filterId, _
                                                                                                        ID_VARIABLE, _
                                                                                                        NAME_VARIABLE)
            For Each filterValueId As UInt32 In filterValuesDict.Keys
                filterNode.Nodes.Add(filterValueId, filterValuesDict(filterValueId))
            Next
        Next

        ' Load Clients Nodes
        Dim clientsNode = filtersNodes.Nodes.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS, ControllingUI_2.CLIENTS_CODE)
        For Each clientId As Int32 In GlobalVariables.Clients.clients_hash.Keys
            clientsNode.Nodes.Add(clientId, GlobalVariables.Clients.clients_hash(clientId)(NAME_VARIABLE))
        Next

        ' Load Products Nodes
        Dim productsNode = filtersNodes.Nodes.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.PRODUCTS, ControllingUI_2.PRODUCTS_CODE)
        For Each productId As Int32 In GlobalVariables.Products.products_hash.Keys
            productsNode.Nodes.Add(productId, GlobalVariables.Products.products_hash(productId)(NAME_VARIABLE))
        Next

        ' Load Adjustment Nodes
        Dim adjustmentsNode = filtersNodes.Nodes.Add(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ADJUSTMENTS, ControllingUI_2.ADJUSTMENT_CODE)
        For Each adjustmentId As Int32 In GlobalVariables.Adjustments.adjustments_hash.Keys
            adjustmentsNode.Nodes.Add(adjustmentId, GlobalVariables.Adjustments.adjustments_hash(adjustmentId)(NAME_VARIABLE))
        Next

    End Sub

#End Region


#Region "Computing"

    Friend Sub Compute(Optional ByRef useCache As Boolean = False)

        rowsHierarchyNode.Nodes.Clear()
        columnsHierarchyNode.Nodes.Clear()

        initDisplayFlag = False
        For Each node As TreeNode In View.display_control.rows_display_tv.Nodes
            rowsHierarchyNode.Nodes.Add(node.Name, node.Text)
        Next
        For Each node As TreeNode In View.display_control.columns_display_tv.Nodes
            columnsHierarchyNode.Nodes.Add(node.Name, node.Text)
        Next

        ' ------------------------------------------
        ' Filters and axis Filters Build
        Dim filters As New Dictionary(Of Int32, List(Of Int32))
        ' Use Generic Selection Builder
        Dim axisFilters As New Dictionary(Of Int32, List(Of Int32))
        ' Use tv checkboxes
        '   -> to be implemented priority high !!
        ' ------------------------------------------

        ' Decomposition Hierarchy build
        Dim computingHierarchyList As New List(Of String)
        IncrementComputingHierarchy(rowsHierarchyNode, computingHierarchyList)
        IncrementComputingHierarchy(columnsHierarchyNode, computingHierarchyList)
 
        ' => computing hierarchy = cache system implementation !!!!
        '     Priority high !!!
        '

        ' Currency setting
        ' STUBS !!!!!!!!!!!!! priority high
        Dim currencyId As Int32 = 3 ' currenciesNameIdDict(View.CurrenciesCLB.SelectedItem)

        ' Versions init
        Dim versionIDs() As Int32 = TreeViewsUtilities.GetCheckedNodesID(View.versionsTV).ToArray
        versionsDict.Clear()
        For Each version_Id As Int32 In versionIDs
            Dim versionNode As TreeNode = View.versionsTV.Nodes.Find(version_Id, True)(0)
            versionsDict.Add(versionNode.Name, versionNode.Text)
        Next

        ' Computing order
        Dim mustCompute As Boolean = True
        If useCache = True AndAlso CheckCache(currencyId, _
                                              versionIDs, _
                                              computingHierarchyList, _
                                              filters, _
                                              axisFilters) = True Then mustCompute = False
        If mustCompute = True Then


            If computingHierarchyList.Count = 0 Then computingHierarchyList = Nothing
            filters = Nothing
            axisFilters = Nothing
            ' STUB -> priority HIGH !!!!!!!!

            Computer.CMSG_COMPUTE_REQUEST(versionIDs, _
                                    CInt(EntityNode.Name), _
                                    currencyId, _
                                    filters, _
                                    axisFilters, _
                                    computingHierarchyList)
        End If

        ' Cache registering
        cacheEntityID = CInt(EntityNode.Name)
        cacheCurrencyId = currencyId
        cacheVersions = versionIDs
        cacheComputingHierarchyList = computingHierarchyList
        cacheFilters = filters
        cacheAxisFilters = axisFilters

        ' Create rows and columns
        InitDisplay()
        For Each tab_ As TabPage In View.TabControl1.TabPages
            CreateRowsAndColumns(tab_.Controls(0), tab_.Name)
        Next
        initDisplayFlag = True

        ' Launch waiting CP
        View.CP = New CircularProgressUI(System.Drawing.Color.Blue, "Computing")
        View.CP.Show()

        ' -> fill UI header not here !! priority normal
        '  FillUIHeader()

    End Sub

    Private Function CheckCache(ByRef currencyId As Int32, _
                                ByRef versionIds() As Int32, _
                                ByRef computingHierarchyList As List(Of String), _
                                ByRef filters As Dictionary(Of Int32, List(Of Int32)), _
                                ByRef axisFilters As Dictionary(Of Int32, List(Of Int32))) As Boolean

        ' entityId => included in current scope
        If View.entitiesTV.Nodes.Find(cacheEntityID, True)(0).Nodes.Find(EntityNode.Name, True).Length = 0 Then Return False
        If cacheCurrencyId <> currencyId Then Return False
        For Each versionId In versionIds
            If cacheVersions.Contains(versionId) = False Then Return False
        Next

        ' decomposition dimensions
        For Each dimensionId As String In computingHierarchyList
            If cacheComputingHierarchyList.Contains(dimensionId) = False Then Return False
        Next

        ' filters / axis filters
        If DictsCompare(filters, cacheFilters) = False Then Return False
        If DictsCompare(axisFilters, cacheAxisFilters) = False Then Return False

        Return True

    End Function

    Private Sub AfterCompute()

        While initDisplayFlag = False
        End While
        dataMap = Computer.GetData()
        View.AfterDisplayAttemp_ThreadSafe()

    End Sub

    Private Sub IncrementComputingHierarchy(ByRef hierarchyNode As TreeNode, _
                                            ByRef computingHierarchyList As List(Of String))

        For Each node As TreeNode In hierarchyNode.Nodes
            If node.Name <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS _
            AndAlso node.Name <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES _
            AndAlso node.Name <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS _
            AndAlso node.Name <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS _
            AndAlso node.Name <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS _
            AndAlso node.Name <> Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS Then
                computingHierarchyList.Add(node.Name)
            End If
        Next

    End Sub

#End Region


#Region "Display Initialization"

    Private Sub InitDisplay()

        filters_dict = New Dictionary(Of String, Int32)
        itemsDimensionsDict.Clear()
        FillHierarchy(rowsHierarchyNode)
        FillHierarchy(columnsHierarchyNode)

    End Sub

    ' rows head to be updated -> i.e. accounts/ entities => accounts line contains head entity !!!
    ' priority high 

    ' Generic Rows/ Columns hierarchy node initialization
    Private Sub FillHierarchy(ByRef hierarchyNode As TreeNode)

        ' analyse list: entities/ accounts => which one is the first/ absent
        ' priority high

        HierarchyListPeriodsTreatment(hierarchyNode)

        For Each node In hierarchyNode.Nodes
            Select Case node.name
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                    For Each Entity_node In EntityNode.Nodes
                        CopySubNodes(Entity_node, node)
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                    ' Accounts hierarchy if not first item in hierarchy !!!
                    ' !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    ' priority high
                    ' set an option or set a rule !! 
                    If True Then
                        For Each accountNode As TreeNode In View.accountsTV.Nodes
                            For Each subAccountNode In accountNode.Nodes
                                CopySubNodes(subAccountNode, node)
                            Next
                        Next
                    Else
                        For Each account_node In TreeViewsUtilities.GetNodesList(View.accountsTV)
                            node.Nodes.Add(account_node.Name, account_node.Text)
                        Next
                    End If

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                    ' versions comparisons implementation !!
                    ' priority high
                    For Each version_id In versionsDict.Keys
                        node.Nodes.Add(version_id, versionsDict(version_id))
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    For Each yearId As Int32 In GlobalVariables.Versions.GetYears(versionsDict)
                        node.Nodes.Add(Computer.YEAR_PERIOD_IDENTIFIER & yearId, Format(Date.FromOADate(yearId), "yyyy"))
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    Dim monthsDict = GlobalVariables.Versions.GetMonths(versionsDict)
                    For Each yearId As Int32 In monthsDict.Keys
                        For Each monthId As Int32 In monthsDict(yearId)
                            node.Nodes.Add(Computer.MONTH_PERIOD_IDENTIFIER & monthId, Format(Date.FromOADate(monthId), "MMM yyyy"))
                        Next
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS
                    Dim monthsDict = GlobalVariables.Versions.GetMonths(versionsDict)
                    For Each yearId As Int32 In monthsDict.Keys
                        Dim yearNode As TreeNode = node.Nodes.Add(Computer.YEAR_PERIOD_IDENTIFIER & yearId, Format(Date.FromOADate(yearId), "yyyy"))
                        For Each monthId As Int32 In monthsDict(yearId)
                            yearNode.Nodes.Add(Computer.MONTH_PERIOD_IDENTIFIER & monthId, Format(Date.FromOADate(monthId), "MMM yyyy"))
                        Next
                    Next


                Case Else
                    For Each value_node In filtersNodes.Nodes.Find(node.name, True)(0).Nodes
                        node.Nodes.Add(value_node.name, value_node.text)
                    Next

            End Select
        Next

    End Sub

    Private Sub HierarchyListPeriodsTreatment(ByRef hierarchyNode As TreeNode)

        ' Build period related hierarchy nodes
        Dim periodsNodes As New TreeNode
        For Each node As TreeNode In hierarchyNode.nodes
            Select Case node.Name
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    periodsNodes.Nodes.Add(1, 1)
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    periodsNodes.Nodes.Add(2, 2)
            End Select
        Next

        ' Analyze present periods dimensions and order
        If periodsNodes.Nodes.Count = 2 Then
            Select Case periodsNodes.Nodes(0).Name & periodsNodes.Nodes(1).Name
                Case "12"
                    ' if years before months => only keep months but with special code
                    Dim monthsNode As TreeNode = hierarchyNode.Nodes.Find(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS, True)(0)
                    monthsNode.name = Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS
                    hierarchyNode.Nodes.Find(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, True)(0).Remove()
                Case "21"
                    ' if years after months => delete years
                    hierarchyNode.Nodes.Find(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, True)(0).Remove()
            End Select
        End If

    End Sub

    Friend Sub CreateRowsAndColumns(ByRef DGV As vDataGridView, _
                                    ByRef tab_account_id As Int32)

        AddHandler DGV.CellValueNeeded, AddressOf DGVs_CellValueNeeded

        accounts_id_shortlist = TreeViewsUtilities.GetNodesKeysList(View.accountsTV.Nodes.Find(tab_account_id, True)(0))
        accounts_id_shortlist.Remove(tab_account_id)

        ' Display_axis_values Initialization 
        display_axis_ht.Clear()
        display_axis_ht(GlobalEnums.DataMapAxis.ACCOUNTS) = 0
        display_axis_ht(GlobalEnums.DataMapAxis.PERIODS) = ""
        display_axis_ht(GlobalEnums.DataMapAxis.FILTERS) = "0"
        display_axis_ht(GlobalEnums.DataMapAxis.ENTITIES) = CInt(EntityNode.Name)
        If versionsDict.Keys.Count = 1 Then
            display_axis_ht(GlobalEnums.DataMapAxis.VERSIONS) = CInt(versionsDict.Keys(0))
        End If
        ' Case multiple versions !!!! priority high
        '  -> filters = 0
        ' priority high !!!!

        ' Bien vérifier le display des filters values = TOTAL ("0")

        ' keep track of dimension id / column ? priority normal !!!
        ' keep track of version hierarchy item !!! priority high !!!!
        ' /////////////////////////////// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        '
        DGV.RowsHierarchy.Clear()
        DGV.ColumnsHierarchy.Clear()
        CreateColumn(DGV, columnsHierarchyNode.Nodes(0))
        CreateRow(DGV, rowsHierarchyNode.Nodes(0))


    End Sub

    Private Sub CreateColumn(ByRef dgv As vDataGridView, _
                             ByRef dimensionNode As TreeNode, _
                             Optional ByRef valueNode As TreeNode = Nothing, _
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
                subColumn.CellsDataSource = GridCellDataSource.Virtual
                subColumn.CellsFormatString = "{0:N}"
                subColumn.CellsTextAlignment = Drawing.ContentAlignment.MiddleRight
                RegisterHierarchyItemDimensions(subColumn)

                ' Dig one level deeper if any
                If Not dimensionNode.NextNode Is Nothing Then
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
                          ByRef dimensionNode As TreeNode, _
                          Optional ByRef valueNode As TreeNode = Nothing, _
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
                '     subRow.CellsDataSource = GridCellDataSource.Virtual
                'For Each column As HierarchyItem In dgv.ColumnsHierarchy.Items
                '    SetHierarchyVirtualDataSource(subRow, column)
                'Next
                RegisterHierarchyItemDimensions(subRow)

                ' Dig one level deeper if any
                If Not dimensionNode.NextNode Is Nothing Then
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

    Private Function SetDisplayAxisValue(ByRef dimensionNode As TreeNode, _
                                         ByRef valueNode As TreeNode) As Boolean

        Select Case dimensionNode.Name
            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                ' In case display_axis is accounts we should only display the accounts belonging to the current accounts tab
                If accounts_id_shortlist.Contains(CInt(valueNode.Name)) = True Then
                    display_axis_ht(GlobalEnums.DataMapAxis.ACCOUNTS) = CInt(valueNode.Name)
                Else
                    Return False
                End If

            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                display_axis_ht(GlobalEnums.DataMapAxis.ENTITIES) = CInt(valueNode.Name)

            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                display_axis_ht(GlobalEnums.DataMapAxis.VERSIONS) = CInt(valueNode.Name)

            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, _
                 Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS, _
                 Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YMONTHS
                display_axis_ht(GlobalEnums.DataMapAxis.PERIODS) = valueNode.Name

            Case Else
                ' Filters (clients, products, adjustments, oter filters)
                ' In case display_axis is filters we just add the filter_value_id to the filters_values_id_list
                ' Possible values are analysis_axis except entities
                filters_dict.Add(dimensionNode.Name, valueNode.Name)

        End Select
        Return True

    End Function

    Private Sub LevelDimensionFilterOrAxis(ByRef dimensionNode As TreeNode)

        If filtersNodes.Nodes.Find(dimensionNode.Name, True).Length > 0 Then
            filters_dict.Remove(dimensionNode.Name)                 ' remove filter from filters dic
        Else
            Select Case dimensionNode.Name
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                    display_axis_ht(GlobalEnums.DataMapAxis.ENTITIES) = CInt(EntityNode.Name)

                    ' must manage other cases ?! 
                    ' priority high
                    ' tests output !!
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                    display_axis_ht(GlobalEnums.DataMapAxis.ACCOUNTS) = 0

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    ' manage => priority high

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    ' manage => priority high

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

    Private Sub DGVs_CellValueNeeded(ByVal sender As Object, ByVal args As CellValueNeededEventArgs)

        Dim accountId As Int32 = 0
        Dim entityId As Int32 = 0
        Dim periodId As String = ""
        Dim versionId As Int32 = 0
        Dim filterId As String = "0"

        Dim items() As HierarchyItem = {args.RowItem, args.ColumnItem}
        For Each item As HierarchyItem In items
            Dim ht As Hashtable = itemsDimensionsDict(item)
            For Each dimension In ht.Keys
                Dim value = ht(dimension)
                Select Case dimension
                    Case GlobalEnums.DataMapAxis.ACCOUNTS
                        If value <> 0 Then accountId = value
                    Case GlobalEnums.DataMapAxis.ENTITIES
                        If value <> 0 Then
                            Select Case entityId
                                Case 0
                                    entityId = value
                                Case Else
                                    If entityId = CInt(EntityNode.Name) Then entityId = value
                            End Select
                        End If
                    Case GlobalEnums.DataMapAxis.PERIODS
                        If value <> "" Then periodId = value
                    Case GlobalEnums.DataMapAxis.VERSIONS
                        If value <> 0 Then versionId = value
                    Case GlobalEnums.DataMapAxis.FILTERS
                        If value <> "0" Then filterId = value
                End Select
            Next
        Next

        If dataMap.ContainsKey(versionId & _
                               filterId & _
                               entityId & _
                               accountId & _
                               periodId) Then

            args.CellValue = dataMap(versionId & _
                                     filterId & _
                                     entityId & _
                                     accountId & _
                                     periodId)
        Else
            args.CellValue = ""
        End If

        '   System.Diagnostics.Debug.WriteLine("cell value given, row: " & args.RowItem.Caption & " column: " & args.ColumnItem.Caption)

    End Sub

#End Region


#Region "Utilities"


    Private Sub CopySubNodes(ByRef or_node As TreeNode, _
                             ByRef des_node As TreeNode)

        Dim subNode As TreeNode = des_node.Nodes.Add(or_node.Name, or_node.Text)
        For Each node In or_node.Nodes
            CopySubNodes(node, subNode)
        Next

    End Sub

    'Private Sub SetHierarchyVirtualDataSource(ByRef row As HierarchyItem, _
    '                                          ByRef column As HierarchyItem)

    '    row.DataGridView.CellsArea.SetCellDataSource(row, column, GridCellDataSource.Virtual)
    '    For Each subColumn As HierarchyItem In column.Items
    '        SetHierarchyVirtualDataSource(row, subColumn)
    '    Next

    'End Sub


    ' below => to be deleted 
    ' simplification 
    ' priority high !!! 
    Friend Sub EntitiesCategoriesUpdate()

        Dim expansionDict = TreeViewsUtilities.SaveNodesExpansionsLevel(View.entitiesTV)
        Dim checkedList = TreeViewsUtilities.SaveCheckedStates(View.entitiesTV)
        Dim filtersDict As List(Of UInt32) = GenericSelectionBuilder.GetAxisFilteredValuesList(View.entitiesFiltersTV, _
                                                                                               GlobalEnums.AnalysisAxis.ENTITIES)
        GlobalVariables.Entities.LoadEntitiesTVWithFilters(View.entitiesTV, filtersDict)
        TreeViewsUtilities.ResumeExpansionsLevel(View.entitiesTV, expansionDict)
        TreeViewsUtilities.ResumeCheckedStates(View.entitiesTV, checkedList)    ' a list of nodes filtered can be displayed

    End Sub

    Friend Sub ClientsCategoriesUpdate()

        GlobalVariables.Clients.LoadClientsTree(View.clientsTV, _
                                                GenericSelectionBuilder.GetAxisFilteredValuesList(View.clientsFiltersTV, _
                                                                                                  GlobalEnums.AnalysisAxis.CLIENTS))

    End Sub

    Friend Sub ProductsCategoriesUpdate()

        GlobalVariables.Products.LoadProductsTree(View.productsTV, _
                                                  GenericSelectionBuilder.GetAxisFilteredValuesList(View.productsFiltersTV, _
                                                                                                    GlobalEnums.AnalysisAxis.PRODUCTS))

    End Sub

    Friend Sub AdjustmentsCategoriesUpdate()

        GlobalVariables.Adjustments.LoadAdjustmentsTree(View.adjustmentsTV, _
                                                        GenericSelectionBuilder.GetAxisFilteredValuesList(View.adjustmentsFiltersTV, _
                                                                                                    GlobalEnums.AnalysisAxis.ADJUSTMENTS))

    End Sub

    Private Sub FillUIHeader()

        ' View.PBar.EndProgress()

        ' Entities Textbox
        View.EntityTB.Text = EntityNode.Text
        View.EntityTB2.Text = EntityNode.Text
        View.EntityTB3.Text = EntityNode.Text

        ' Currencies textbox
        View.CurrencyTB.Text = View.CurrenciesCLB.CheckedItems(0)
        View.CurrencyTB2.Text = View.CurrencyTB.Text
        View.CurrencyTB3.Text = View.CurrencyTB.Text

        ' Versions Textbox
        ' Get versions name from ids
        'Dim versions_ids(categories_values_dict(VERSIONS_CODE).Keys.Count) As String
        'Dim i = 0
        'For Each id As String In categories_values_dict(VERSIONS_CODE).Keys
        '    versions_ids(i) = versions_dict(id)(NAME_VARIABLE)
        '    i = i + 1
        'Next
        ' fill textbox
        '      View.VersionTB.Text = String.Join(" ; ", versions_ids)
        View.VersionTB2.Text = View.VersionTB.Text
        View.VersionTB3.Text = View.VersionTB.Text

    End Sub

    'Private Function GetAdjustmentsFilter() As List(Of UInt32)

    '    Dim tmp_list As List(Of UInt32) = TreeViewsUtilities.GetCheckedNodesID(View.adjustmentsTV)
    '    If tmp_list.Count <> View.adjustmentsTV.Nodes.Count Then
    '        Return tmp_list
    '    Else
    '        Return Nothing
    '    End If

    'End Function

    Friend Sub dropOnExcel()

        ' should be elsewhere !!!! 
        ' priority normal
        ' Maybe issue if nothing in the DGV ? !
        If Not EntityNode.Text Is Nothing Then
            Dim destination As Microsoft.Office.Interop.Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS(EntityNode.Text, _
                                                                                           {"Entity", "Version", "Currency"}, _
                                                                                           {EntityNode.Text, View.VersionTB.Text, View.CurrencyTB.Text})
            Dim i As Int32 = 1
            For Each tab_ As TabPage In View.TabControl1.TabPages
                Dim DGV As VIBlend.WinForms.DataGridView.vDataGridView = tab_.Controls(0)
                DataGridViewsUtil.CopyDGVToExcelGeneric(DGV, destination, i)
            Next
            destination.Worksheet.Columns.AutoFit()
            destination.Worksheet.Outline.ShowLevels(RowLevels:=1)
        End If

    End Sub

    Private Function DictsCompare(ByRef dict1 As Dictionary(Of Int32, List(Of Int32)), _
                                  ByRef dict2 As Dictionary(Of Int32, List(Of Int32))) As Boolean


        For Each filterId As Int32 In dict1.Keys
            If dict2.ContainsKey(filterId) = False Then Return False
            For Each filterValueId As Int32 In dict1(filterId)
                If dict2(filterId).Contains(filterValueId) = False Then Return False
            Next
        Next
        Return True

    End Function

#End Region


End Class
