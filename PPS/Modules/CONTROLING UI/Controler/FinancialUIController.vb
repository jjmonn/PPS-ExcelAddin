' FinancialUIController.vb
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
' Last modified: 07/08/2015


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
    Private rows_hierarchy_node As New TreeNode
    Private columns_hierarchy_node As New TreeNode
    Private display_axis_ht As New Hashtable
    Private filtersAndAxisDict As New Dictionary(Of String, List(Of Int32))
    Private dataMap As Hashtable
    Private filters_dict As New Dictionary(Of String, Int32)
    Private filtersNodes As New TreeNode
    Private VersionsTV As New TreeView
    Friend versionsDict As New Dictionary(Of Int32, String)
    Friend initDisplayFlag As Boolean = False

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

    Friend Sub Compute(ByRef rowsHierarchyNodesList As List(Of TreeNode), _
                       ByRef columnsHierarchyNodesList As List(Of TreeNode))

        initDisplayFlag = False
        For Each node As TreeNode In View.display_control.rows_display_tv.Nodes
            rowsHierarchyNodesList.Add(node)
        Next
        For Each node As TreeNode In View.display_control.columns_display_tv.Nodes
            columnsHierarchyNodesList.Add(node)
        Next

        ' ------------------------------------------
        ' Filters and axis Filters Build
        Dim filters As New Dictionary(Of Int32, List(Of Int32))
        ' Use Generic Selection Builder
        Dim axisFilters As New Dictionary(Of Int32, List(Of Int32))
        ' Use tv checkboxes
        '   -> to be implemented priority high !!
        filters = Nothing
        axisFilters = Nothing
        ' ------------------------------------------

        ' Decomposition Hierarchy build
        Dim computingHierarchyList As New List(Of String)
        IncrementComputingHierarchy(rowsHierarchyNodesList, computingHierarchyList)
        IncrementComputingHierarchy(columnsHierarchyNodesList, computingHierarchyList)
        If computingHierarchyList.Count = 0 Then computingHierarchyList = Nothing

        ' Currency setting
        ' STUBS !!!!!!!!!!!!! priority high
        Dim currencyId As Int32 = 3 ' currenciesNameIdDict(View.CurrenciesCLB.SelectedItem)

        ' Versions init
        Dim versionID() As Int32 = TreeViewsUtilities.GetCheckedNodesID(View.versionsTV).ToArray
        versionsDict.Clear()
        For Each version_Id As Int32 In versionID
            Dim versionNode As TreeNode = View.versionsTV.Nodes.Find(version_Id, True)(0)
            versionsDict.Add(versionNode.Name, versionNode.Text)
        Next

        ' Computing order
        Computer.CMSG_COMPUTE_REQUEST(versionID, _
                                      CInt(EntityNode.Name), _
                                      currencyId, _
                                      filters, _
                                      axisFilters, _
                                      computingHierarchyList)

        ' -> fill UI header not here !! priority normal
        '  FillUIHeader()

    End Sub

    Private Sub AfterCompute()

        While initDisplayFlag = False
        End While
        View.ComputingBCGWorker.CancelAsync()
        dataMap = Computer.GetData()
        '     View.CP.Dispose()
        ' View.CP = New CircularProgressUI(System.Drawing.Color.Purple, "Displaying")
        'View.CP.Show()
        '  View.DisplayBCGWorker.RunWorkerAsync()
        'For Each tab_ As TabPage In View.TabControl1.TabPages
        '    tab_.Controls(0).Refresh()
        'Next

    End Sub

    Private Sub IncrementComputingHierarchy(ByRef hierarchyNodesList As List(Of TreeNode), _
                                                ByRef computingHierarchyList As List(Of String))

        For Each node As TreeNode In hierarchyNodesList
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

    Friend Sub InitDisplay(ByRef rowsHierarchyNodeList As List(Of TreeNode), _
                           ByRef columnsHierarchyNodeList As List(Of TreeNode))

        filters_dict = New Dictionary(Of String, Int32)
        rows_hierarchy_node.Nodes.Clear()
        columns_hierarchy_node.Nodes.Clear()
        itemsDimensionsDict.Clear()
        FillHierarchy(rows_hierarchy_node, rowsHierarchyNodeList)
        FillHierarchy(columns_hierarchy_node, columnsHierarchyNodeList)

    End Sub

    ' rows head to be updated -> i.e. accounts/ entities => accounts line contains head entity !!!
    ' priority high 

    ' Generic Rows/ Columns hierarchy node initialization
    Private Sub FillHierarchy(ByRef node As TreeNode, _
                              ByRef list As List(Of TreeNode))

        node.Nodes.Clear()
        For Each item_node In list
            Dim axis_node As TreeNode = node.Nodes.Add(item_node.Name, item_node.Text)
            Select Case item_node.Name
                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES
                    For Each Entity_node In EntityNode.Nodes
                        Dim sub_node As TreeNode = axis_node.Nodes.Add(Entity_node.Name, Entity_node.Text)
                        CopySubNodes(Entity_node, sub_node)
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS
                    ' Accounts hierarchy if not first item in hierarchy !!!
                    ' !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    ' priority high
                    For Each account_node In TreeViewsUtilities.GetNodesList(View.accountsTV)
                        axis_node.Nodes.Add(account_node.Name, account_node.Text)
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS
                    ' versions comparisons implementation !!
                    ' priority high
                    For Each version_id In versionsDict.Keys
                        axis_node.Nodes.Add(version_id, versionsDict(version_id))
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                    For Each yearId As Int32 In GlobalVariables.Versions.GetYears(versionsDict)
                        axis_node.Nodes.Add(Computer.YEAR_PERIOD_IDENTIFIER & yearId, Format(Date.FromOADate(yearId), "yyyy"))
                    Next

                Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
                    For Each monthId As Int32 In GlobalVariables.Versions.GetMonths(versionsDict)
                        axis_node.Nodes.Add(Computer.YEAR_PERIOD_IDENTIFIER & monthId, Format(Date.FromOADate(monthId), "MMM yyyy"))
                    Next

                Case Else
                    For Each value_node In filtersNodes.Nodes.Find(item_node.Name, True)(0).Nodes
                        axis_node.Nodes.Add(value_node.name, value_node.text)
                    Next

            End Select
        Next

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
        CreateColumn(DGV, columns_hierarchy_node.Nodes(0))
        CreateRow(DGV, rows_hierarchy_node.Nodes(0))
        'DGV.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        'DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)

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
                RegisterHierarchyItemDimensions(subRow)
                subRow.CellsDataSource = GridCellDataSource.Virtual
                subRow.CellsFormatString = "{0:N}"

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

            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS
                display_axis_ht(GlobalEnums.DataMapAxis.PERIODS) = valueNode.Name

            Case Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS
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
        Dim filterId As String = ""

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

        Try
            args.CellValue = dataMap(versionId) _
                             (filterId) _
                             (entityId) _
                             (accountId) _
                             (periodId)
        Catch ex As Exception
            args.CellValue = ""
        End Try

    End Sub

#End Region


#Region "Util"

    Private Sub CopySubNodes(ByRef or_node As TreeNode, _
                             ByRef des_node As TreeNode)

        For Each node In or_node.Nodes
            Dim new_node As TreeNode = des_node.Nodes.Add(node.name, node.text)
            CopySubNodes(node, new_node)
        Next

    End Sub

#End Region


#Region "Utilities"

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

#End Region


End Class
