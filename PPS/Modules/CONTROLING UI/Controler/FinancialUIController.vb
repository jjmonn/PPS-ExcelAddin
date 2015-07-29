' CControlingCONTROLER.vb
'
' Model for Controlling User iterfaces: display consolidated data
'
' To do:
'      
'   
'
' Known bugs:
'
'
' Author: Julien Monnereau
' Last modified: 28/07/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq


Friend Class FinancialUIController


#Region "Instance Variable"

    ' Objects
    Private Computer As New Computer
    Private View As ControllingUI_2
    Friend Entity_node As TreeNode


    ' Variables
    Private versions_dict As Dictionary(Of String, Hashtable)
    Private currenciesNameIdDict As Dictionary(Of String, UInt32)


#End Region


#Region "Vizualization"

    ' -----------------------------------------------------------------------  
    ' Hierarchy node does not include ACCOUNTS/ ENTITIES/ VERSIONS Codes
    ' Example:  
    '
    ' Hierarchy Node example:
    ' |    |
    ' |    - Geographic Region (id)
    ' |    - Clients Analaysis Axis (id)
    ' |    
    ' ----------------------------------------------------------------------- 

    ' ----------------------------------------------------------------------- 
    ' Example:
    '
    ' - Hierarchy Display Node
    ' |    |
    ' |    - Accounts
    ' |    |
    ' |    - Regions
    ' |    |
    ' |    - Entities
    ' |    |
    ' |    - Clients
    ' | 
    ' ----------------------------------------------------------------------- 

    ' Categories_values_dict
    ' = {Category1 => {value1_id => name; value2_id => name} ; Category2 => {value1_id => name; value2_id => name} }

    ' ----------------------------------------------------------------------- 
    ' Corresponding Data Dictionaries: (DataHT) (non comprehensive list)
    ' 
    ' (total)(total)(versions)(Entities)(Accounts)(Periods)
    ' (total)(Client X)(versions)(Entities)(Accounts)(Periods)
    ' (total)(Client Y)(versions)(Entities)(Accounts)(Periods)
    ' (Region A)(total)(versions)(Entities)(Accounts)(Periods)
    ' (Region A)(Client X)(versions)(Entities)(Accounts)(Periods)
    ' (Region A)(Client Y)(versions)(Entities)(Accounts)(Periods)
    ' ...
    ' ----------------------------------------------------------------------- 


#End Region


#Region "Initialization"

    Friend Sub New(ByRef inputView As Object)

        View = inputView
        AddHandler Computer.ComputationAnswered, AddressOf AfterCompute

    End Sub

#End Region


#Region "Interface"

    Friend Sub Compute(ByRef rowsHierarchyNodesList As List(Of TreeNode), _
                       ByRef columnsHierarchyNodesList As List(Of TreeNode))

        For Each node As TreeNode In View.display_control.rows_display_tv.Nodes
            rowsHierarchyNodesList.Add(node)
        Next
        For Each node As TreeNode In View.display_control.columns_display_tv.Nodes
            columnsHierarchyNodesList.Add(node)
        Next

        ' Filters and axis Filters Build
        Dim filters As New Dictionary(Of UInt32, List(Of UInt32))
        Dim axisFilters As New Dictionary(Of UInt32, List(Of UInt32))
        ' ------------------------------------------
        '   -> to be implemented priority high !!
        ' ------------------------------------------


        ' Decomposition Hierarchy build
        Dim computingHierarchyList As New List(Of String)
        IncrementComputingHierarchy(rowsHierarchyNodesList, computingHierarchyList)
        IncrementComputingHierarchy(columnsHierarchyNodesList, computingHierarchyList)
        If computingHierarchyList.Count = 0 Then computingHierarchyList = Nothing

        ' Currency setting
        Dim currencyId As UInt32 = currenciesNameIdDict(View.CurrenciesCLB.SelectedItem)

        ' Computing order
        Computer.CMSG_COMPUTE_REQUEST(TreeViewsUtilities.GetCheckedNodesID(View.versionsTV).ToArray, _
                                      CInt(Entity_node.Name), _
                                      currencyId, _
                                      filters, _
                                      axisFilters, _
                                      computingHierarchyList)

        ' -> fill UI header not here !! priority normal
        '  FillUIHeader()

    End Sub

    Private Sub AfterCompute()

        View.ComputingBCGWorker.CancelAsync()

    End Sub

    Friend Function GetDataMap() As Hashtable

        Return Computer.GetData()

    End Function


    Friend Sub dropOnExcel()

        ' Maybe issue if nothing in the DGV ? !
        If Not Entity_node.Text Is Nothing Then
            Dim destination As Microsoft.Office.Interop.Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS(Entity_node.Text, _
                                                                                           {"Entity", "Version", "Currency"}, _
                                                                                           {Entity_node.Text, View.VersionTB.Text, View.CurrencyTB.Text})
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


#Region "Hierarchies Configuration - Values Dictionaries Initialization"

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

    ' below -> à garder pour la construction du tv filters values
    Private Sub BuildColumnsHierarchy(ByRef columns_display_node As TreeNode, _
                                      ByRef periods_node As TreeNode)


        'Dim versions As New Version
        'versions_dict = versions.GetVersionsDictionary(TreeViewsUtilities.GetCheckedNodesID(View.versionsTV))
        'versions.Close()

        'Dim time_config As String = Version.IdentifyVersionsComparison(versions_dict)

        'columns_display_node.Nodes.Add(YEARS_CODE, YEARS_CODE)
        'Select Case time_config
        '    Case Version.YEARLY_VERSIONS_COMPARISON, Version.YEARLY_MONTHLY_VERSIONS_COMPARISON
        '        columns_display_node.Nodes.Add(VERSIONS_CODE, VERSIONS_CODE)

        '    Case MONTHLY_TIME_CONFIGURATION
        '        columns_display_node.Nodes.Add(MONTHS_CODE, MONTHS_CODE)

        '    Case Version.MONTHLY_VERSIONS_COMPARISON
        '        columns_display_node.Nodes.Add(MONTHS_CODE, MONTHS_CODE)
        '        columns_display_node.Nodes.Add(VERSIONS_CODE, VERSIONS_CODE)

        'End Select
        'periods_node = versions.GetPeriodsNode(versions_dict, time_config)

    End Sub

    Private Sub CategoriesValuesDictionaryBuild(ByRef periods_node As TreeNode, _
                                                ByRef columns_hierarchy_nodes As TreeNode, _
                                                ByRef rows_display_node As TreeNode, _
                                                ByRef rows_hierarchy_nodes As TreeNode)

        ' Really usefull ??!! priority high
        ' see how this interacts with new data_map


        'categories_values_dict.Clear()

        '' Entities
        'Dim entities_dic As New Dictionary(Of String, String)
        'entities_dic.Add(Entity_node.Name, Entity_node.Text)

        'If rows_display_node.Nodes.Find(ENTITIES_CODE, True).Count > 0 Then
        '    Dim entities_node_list As List(Of TreeNode) = TreeViewsUtilities.GetNodesList(Entity_node)
        '    For Each node As TreeNode In entities_node_list
        '        entities_dic.Add(node.Name, node.Text)
        '    Next
        'End If
        'categories_values_dict.Add(ENTITIES_CODE, entities_dic)

        '' Versions 
        'Dim tmp_dic As New Dictionary(Of String, String)
        'For Each version_id As String In versions_dict.Keys
        '    tmp_dic.Add(version_id, versions_dict(version_id)(NAME_VARIABLE))
        'Next
        'categories_values_dict.Add(VERSIONS_CODE, tmp_dic)

        '' Periods
        'Dim year_dic As New Dictionary(Of String, String)
        'For Each year_node As TreeNode In periods_node.Nodes
        '    year_dic.Add(year_node.Name, year_node.Text)
        '    Dim tmp_node As TreeNode = columns_hierarchy_nodes.Nodes.Find(YEARS_CODE, True)(0).Nodes.Add(year_node.Name, year_node.Text)
        '    Dim months_dic As New Dictionary(Of String, String)
        '    For Each month_node As TreeNode In year_node.Nodes
        '        months_dic.Add(month_node.Name, month_node.Text)
        '        tmp_node.Nodes.Add(month_node.Name, month_node.Text)
        '    Next
        '    If months_dic.Count > 0 Then categories_values_dict.Add(year_node.Name, months_dic)
        'Next
        'categories_values_dict.Add(YEARS_CODE, year_dic)

        '' Rows Dictionary Codes
        'For Each node As TreeNode In rows_hierarchy_nodes.Nodes
        '    Dim tmp_values_dic As New Dictionary(Of String, String)

        '    Select Case Model.categories_id_code_dict(node.Name)
        '        Case ControllingUI2Controller.CLIENTS_CODE, _
        '             ControllingUI2Controller.PRODUCTS_CODE, _
        '             ControllingUI2Controller.ADJUSTMENT_CODE

        '            For Each value_node As TreeNode In TVsDict(node.Name).Nodes
        '                If value_node.Checked = True Then tmp_values_dic.Add(value_node.Name, value_node.Text)
        '            Next

        '        Case ControllingUI2Controller.ENTITY_CATEGORY_CODE, _
        '             ControllingUI2Controller.CLIENT_CATEGORY_CODE, _
        '             ControllingUI2Controller.PRODUCT_CATEGORY_CODE

        '            For Each value_node As TreeNode In TVsDict(Model.categories_id_code_dict(node.Name)).Nodes.Find(node.Name, True)(0).Nodes
        '                If value_node.Checked = True Then tmp_values_dic.Add(value_node.Name, value_node.Text)
        '            Next

        '    End Select
        '    categories_values_dict.Add(node.Name, tmp_values_dic)
        'Next

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

        View.PBar.EndProgress()

        ' Entities Textbox
        View.EntityTB.Text = Entity_node.Text
        View.EntityTB2.Text = Entity_node.Text
        View.EntityTB3.Text = Entity_node.Text

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

    Private Function GetAdjustmentsFilter() As List(Of UInt32)

        Dim tmp_list As List(Of UInt32) = TreeViewsUtilities.GetCheckedNodesID(View.adjustmentsTV)
        If tmp_list.Count <> View.adjustmentsTV.Nodes.Count Then
            Return tmp_list
        Else
            Return Nothing
        End If

    End Function

#End Region


End Class
