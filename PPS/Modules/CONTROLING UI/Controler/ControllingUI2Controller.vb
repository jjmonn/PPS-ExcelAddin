' CControlingCONTROLER.vb
'
' Model for Controlling User iterfaces: display consolidated data
'
' To do:
'      
'      - Implement Clients and Products Categories TV after check (bottom of this page) !! 
'
'
' Known bugs:
'
'
' Author: Julien Monnereau
' Last modified: 03/05/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq


Friend Class ControllingUI2Controller


#Region "Instance Variable"

    ' Objects
    Private Model As ControllingUIModel
    Private ESB As New ESB
    Private View As ControllingUI_2
    Protected Friend Entity_node As TreeNode

    ' Variables
    Protected Friend categories_values_dict As New Dictionary(Of String, Dictionary(Of String, String))
    Private TVsDict As New Dictionary(Of String, TreeView)
    Private versions_dict As Dictionary(Of String, Hashtable)

    ' Const
    Friend Const ACCOUNTS_CODE As String = "accounts"
    Friend Const YEARS_CODE As String = "years"
    Friend Const MONTHS_CODE As String = "months"
    Friend Const VERSIONS_CODE As String = "versions"
    Friend Const ENTITIES_CODE As String = "Entities"
    Friend Const CLIENTS_CODE As String = "Clients"
    Friend Const PRODUCTS_CODE As String = "Products"
    Friend Const ADJUSTMENT_CODE As String = "Adjustments"
    Friend Const ENTITY_CATEGORY_CODE As String = "entitiescategories"
    Friend Const CLIENT_CATEGORY_CODE As String = "clientscategories"
    Friend Const PRODUCT_CATEGORY_CODE As String = "productscategories"
    Friend Const TOTAL_CODE As String = "total"
    Friend Const ZERO_PERIOD_CODE As Int32 = 0

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
        Model = New ControllingUIModel(View.PBar)
        TVsDict.Add(ENTITIES_CODE, View.entitiesTV)
        TVsDict.Add(CLIENTS_CODE, View.clientsTV)
        TVsDict.Add(PRODUCTS_CODE, View.productsTV)
        TVsDict.Add(ADJUSTMENT_CODE, View.adjustmentsTV)
        TVsDict.Add(ENTITY_CATEGORY_CODE, View.entities_categoriesTV)
        TVsDict.Add(CLIENT_CATEGORY_CODE, View.clients_categoriesTV)
        TVsDict.Add(PRODUCT_CATEGORY_CODE, View.products_categoriesTV)

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub RefreshData(ByRef input_entity_node As TreeNode)

        Dim rows_display_nodes As New TreeNode
        rows_display_nodes.Nodes.Add(ACCOUNTS_CODE, ACCOUNTS_CODE)
        For Each node As TreeNode In View.display_control.rows_display_tv.Nodes
            rows_display_nodes.Nodes.Add(node.Name, node.Text)
        Next
        Dim rows_hierarchy_nodes As TreeNode = BuildRowsHierarchy(rows_display_nodes)
        Entity_node = input_entity_node

        Dim periods_node As TreeNode
        Dim columns_display_node As New TreeNode
        BuildColumnsHierarchy(columns_display_node, periods_node)

        CategoriesValuesDictionaryBuild(periods_node, _
                                        columns_display_node, _
                                        rows_display_nodes, _
                                        rows_hierarchy_nodes)

        Pbar_init_complete()
        Model.InitializeDataDictionary(categories_values_dict, rows_display_nodes, columns_display_node)
        View.CreateDGVHierarchies(columns_display_node, rows_display_nodes)

        GetDataDictionary(rows_hierarchy_nodes, View.CurrenciesCLB.SelectedItem)

        FillUIHeader()
        View.DisplayData(Model.DataDictionary, _
                         columns_display_node, _
                         rows_display_nodes)

    End Sub

    Protected Friend Sub dropOnExcel()

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

    Private Function BuildRowsHierarchy(ByRef rows_display_nodes As TreeNode) As TreeNode

        Dim rows_hierarchy_nodes As New TreeNode
        For Each node As TreeNode In rows_display_nodes.Nodes
            If node.Name <> ControllingUI2Controller.ACCOUNTS_CODE _
            AndAlso node.Name <> ControllingUI2Controller.ENTITIES_CODE Then
                rows_hierarchy_nodes.Nodes.Add(node.Name, node.Name)
            End If
        Next
        Return rows_hierarchy_nodes

    End Function

    Private Sub BuildColumnsHierarchy(ByRef columns_display_node As TreeNode, _
                                      ByRef periods_node As TreeNode)

        Dim versions As New Version
        versions_dict = versions.GetVersionsDictionary(TreeViewsUtilities.GetCheckedNodesID(View.versionsTV))
        versions.Close()

        Dim time_config As String = Version.IdentifyVersionsComparison(versions_dict)

        columns_display_node.Nodes.Add(YEARS_CODE, YEARS_CODE)
        Select Case time_config
            Case Version.YEARLY_VERSIONS_COMPARISON, Version.YEARLY_MONTHLY_VERSIONS_COMPARISON
                columns_display_node.Nodes.Add(VERSIONS_CODE, VERSIONS_CODE)

            Case MONTHLY_TIME_CONFIGURATION
                columns_display_node.Nodes.Add(MONTHS_CODE, MONTHS_CODE)

            Case Version.MONTHLY_VERSIONS_COMPARISON
                columns_display_node.Nodes.Add(MONTHS_CODE, MONTHS_CODE)
                columns_display_node.Nodes.Add(VERSIONS_CODE, VERSIONS_CODE)

        End Select
        periods_node = versions.GetPeriodsNode(versions_dict, time_config)

    End Sub

    Private Sub CategoriesValuesDictionaryBuild(ByRef periods_node As TreeNode, _
                                                ByRef columns_hierarchy_nodes As TreeNode, _
                                                ByRef rows_display_node As TreeNode, _
                                                ByRef rows_hierarchy_nodes As TreeNode)

        categories_values_dict.Clear()
        
        ' Entities
        Dim entities_dic As New Dictionary(Of String, String)
        entities_dic.Add(Entity_node.Name, Entity_node.Text)

        If rows_display_node.Nodes.Find(ENTITIES_CODE, True).Count > 0 Then
            Dim entities_node_list As List(Of TreeNode) = TreeViewsUtilities.GetNodesList(Entity_node)
            For Each node As TreeNode In entities_node_list
                entities_dic.Add(node.Name, node.Text)
            Next
        End If
        categories_values_dict.Add(ENTITIES_CODE, entities_dic)

        ' Versions 
        Dim tmp_dic As New Dictionary(Of String, String)
        For Each version_id As String In versions_dict.Keys
            tmp_dic.Add(version_id, versions_dict(version_id)(VERSIONS_NAME_VARIABLE))
        Next
        categories_values_dict.Add(VERSIONS_CODE, tmp_dic)

        ' Periods
        Dim year_dic As New Dictionary(Of String, String)
        For Each year_node As TreeNode In periods_node.Nodes
            year_dic.Add(year_node.Name, year_node.Text)
            Dim tmp_node As TreeNode = columns_hierarchy_nodes.Nodes.Find(YEARS_CODE, True)(0).Nodes.Add(year_node.Name, year_node.Text)
            Dim months_dic As New Dictionary(Of String, String)
            For Each month_node As TreeNode In year_node.Nodes
                months_dic.Add(month_node.Name, month_node.Text)
                tmp_node.Nodes.Add(month_node.Name, month_node.Text)
            Next
            If months_dic.Count > 0 Then categories_values_dict.Add(year_node.Name, months_dic)
        Next
        categories_values_dict.Add(YEARS_CODE, year_dic)

        ' Rows Dictionary Codes
        For Each node As TreeNode In rows_hierarchy_nodes.Nodes
            Dim tmp_values_dic As New Dictionary(Of String, String)

            Select Case Model.categories_id_code_dict(node.Name)
                Case ControllingUI2Controller.CLIENTS_CODE, _
                     ControllingUI2Controller.PRODUCTS_CODE, _
                     ControllingUI2Controller.ADJUSTMENT_CODE

                    For Each value_node As TreeNode In TVsDict(node.Name).Nodes
                        If value_node.Checked = True Then tmp_values_dic.Add(value_node.Name, value_node.Text)
                    Next

                Case ControllingUI2Controller.ENTITY_CATEGORY_CODE, _
                     ControllingUI2Controller.CLIENT_CATEGORY_CODE, _
                     ControllingUI2Controller.PRODUCT_CATEGORY_CODE

                    For Each value_node As TreeNode In TVsDict(Model.categories_id_code_dict(node.Name)).Nodes.Find(node.Name, True)(0).Nodes
                        If value_node.Checked = True Then tmp_values_dic.Add(value_node.Name, value_node.Text)
                    Next

            End Select
            categories_values_dict.Add(node.Name, tmp_values_dic)
        Next

    End Sub

   #End Region


#Region "Model Computations Management"

    Private Sub GetDataDictionary(ByRef rows_hierarchy_node As TreeNode, _
                                  ByRef destination_currency As String)

        Dim filters_dict As New Dictionary(Of String, String)
        If rows_hierarchy_node.Nodes.Count > 0 Then
            RowsInnerLoop(rows_hierarchy_node.Nodes(0), filters_dict, destination_currency)
        Else
            compute(filters_dict, destination_currency)
        End If

    End Sub

    Private Sub RowsInnerLoop(ByRef hierarchy_node As TreeNode, _
                              ByVal filters_dict As Dictionary(Of String, String), _
                              ByRef destination_currency As String)

        ' Total value Loop 
        FilterValueLoop(TOTAL_CODE, hierarchy_node, filters_dict, destination_currency)

        ' Loop through all filters value
        For Each filter_value As String In categories_values_dict(hierarchy_node.Name).Keys
            FilterValueLoop(filter_value, hierarchy_node, filters_dict, destination_currency)
        Next
        ' Remove current level filter
        filters_dict(hierarchy_node.Name) = TOTAL_CODE

    End Sub

    Private Sub FilterValueLoop(ByRef filter_value As String, _
                                ByRef hierarchy_node As TreeNode, _
                                ByVal filters_dict As Dictionary(Of String, String), _
                                ByRef destination_currency As String)

        filters_dict(hierarchy_node.Name) = filter_value
        If Not hierarchy_node.NextNode Is Nothing Then
            ' Goes one hierarchy level deeper
            RowsInnerLoop(hierarchy_node.NextNode, filters_dict, destination_currency)
        Else
            ' No deeper hierarchy: Apply filter and Compute
            compute(filters_dict, destination_currency)
        End If

    End Sub

    Private Sub compute(ByVal filters_dict As Dictionary(Of String, String), _
                        ByRef destination_currency As String)

        Model.IntializeComputer(Entity_node, _
                                TreeViewsUtilities.GetCheckedNodesID(TVsDict(ControllingUI2Controller.CLIENTS_CODE)), _
                                TreeViewsUtilities.GetCheckedNodesID(TVsDict(ControllingUI2Controller.PRODUCTS_CODE)))
        Model.SetDBFilters(filters_dict)
        Model.ComputeAndFillDD(Utilities_Functions.GetDictionaryCopy(filters_dict), _
                               categories_values_dict(VERSIONS_CODE).Keys.ToList, _
                               versions_dict, _
                               destination_currency)

    End Sub

#End Region


#Region "Utilities"

    Private Sub Pbar_init_complete()

        Dim load_max As Int32 = 0
        For Each key As String In categories_values_dict.Keys
            load_max = load_max + 1 + categories_values_dict(key).Count
        Next

        '(Model.inputs_entities_list.Count + 5) * versions_id_array.Length + 2
        View.PBar.Launch(1, load_max * 2)
        View.PBar.Visible = True

    End Sub

    Friend Sub EntitiesCategoriesUpdate()

        ESB.BuildCategoriesFilterFromTreeview(View.entities_categoriesTV)
        Dim expansionDict = TreeViewsUtilities.SaveNodesExpansionsLevel(View.entitiesTV)
        Dim checkedList = TreeViewsUtilities.SaveCheckedStates(View.entitiesTV)
        Entity.LoadEntitiesTree(View.entitiesTV, ESB.StrSqlQueryForEntitiesUploadFunctions)
        TreeViewsUtilities.ResumeExpansionsLevel(View.entitiesTV, expansionDict)
        TreeViewsUtilities.ResumeCheckedStates(View.entitiesTV, checkedList)    ' a list of nodes filtered can be displayed

    End Sub

    Protected Friend Sub ClientsCategoriesUpdate()

        View.clientsTV.Nodes.Clear()
        Client.LoadClientsTree(View.clientsTV, GenericSelectionBuilder.getFilteredList(View.clients_categoriesTV, CLIENTS_TABLE))

    End Sub

    Protected Friend Sub ProductsCategoriesUpdate()

        View.productsTV.Nodes.Clear()
        Product.LoadProductsTree(View.productsTV, GenericSelectionBuilder.getFilteredList(View.products_categoriesTV, PRODUCTS_TABLE))

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
        Dim versions_ids(categories_values_dict(VERSIONS_CODE).Keys.Count) As String
        Dim i = 0
        For Each id As String In categories_values_dict(VERSIONS_CODE).Keys
            versions_ids(i) = versions_dict(id)(VERSIONS_NAME_VARIABLE)
            i = i + 1
        Next
        ' fill textbox
        View.VersionTB.Text = String.Join(" ; ", versions_ids)
        View.VersionTB2.Text = View.VersionTB.Text
        View.VersionTB3.Text = View.VersionTB.Text

    End Sub

    Friend Sub close_model()

        Model.CloseModel()

    End Sub

    Private Function GetAdjustmentsFilter() As List(Of String)

        Dim tmp_list As List(Of String) = TreeViewsUtilities.GetCheckedNodesID(View.adjustmentsTV)
        If tmp_list.Count <> View.adjustmentsTV.Nodes.Count Then
            Return tmp_list
        Else
            Return Nothing
        End If

    End Function

#End Region


End Class
