' ControllingUIAggregationComputer.vb
'
'
' To do
'
'
'
'
' Author: Julien Monnereau
' Last modified: 04/04/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class ControllingUIModel


#Region "Instance Variables"

    ' Objects
    Private AggregationComputer As GenericAggregationDLL3Computing
    Private ESB As ESB
    Private PBar As ProgressBarControl

    ' Variables
    Protected Friend DataDictionary As New Dictionary(Of Object, Object)
    Protected Friend categories_id_code_dict As New Dictionary(Of String, String)
    Private hierarchies_node As New TreeNode
    Private account_index As Int32
    Private year_index As Int32
    Private month_index As Int32
    Private total_years_count As Int32
    Protected Friend categories_values_dict As New Dictionary(Of String, Dictionary(Of String, String))


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_ESB As ESB)

        AggregationComputer = New GenericAggregationDLL3Computing(GlobalVariables.GlobalDBDownloader)
        ESB = input_ESB
        InitializeCategoriesIDsCodeDict()

    End Sub

    Private Sub InitializeCategoriesIDsCodeDict()

        categories_id_code_dict = CategoriesMapping.GetCategoriesIDCodeMapping()
        categories_id_code_dict.Add(ControllingUI2Controller.CLIENTS_CODE, ControllingUI2Controller.CLIENTS_CODE)
        categories_id_code_dict.Add(ControllingUI2Controller.PRODUCT_CODE, ControllingUI2Controller.PRODUCT_CODE)
        categories_id_code_dict.Add(ControllingUI2Controller.ADJUSTMENT_CODE, ControllingUI2Controller.ADJUSTMENT_CODE)

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub InitializeDataDictionary(ByRef input_categories_values_dict As Dictionary(Of String, Dictionary(Of String, String)), _
                                                  ByRef rows_display_node As TreeNode, _
                                                  ByRef columns_display_node As TreeNode)

        categories_values_dict = input_categories_values_dict
        DataDictionary.Clear()
        BuildHierarchiesNodes(rows_display_node, columns_display_node)

    End Sub

    Protected Friend Sub IntializeComputerAndFilter(ByRef entity_node As TreeNode)

        AggregationComputer.init_computer_complete_mode(entity_node)
        AggregationComputer.ReinitializeFiltersList()
    
    End Sub

    Protected Friend Sub SetDBFilters(ByRef filters_dict As Dictionary(Of String, String))

        For Each filter_code As String In filters_dict.Keys
            If filters_dict(filter_code) <> ControllingUI2Controller.TOTAL_CODE Then

                Dim str_filter_SQL As String = filter_code & "='" & filters_dict(filter_code) & "'"

                Select Case categories_id_code_dict(filter_code)
                    Case ControllingUI2Controller.CLIENTS_CODE
                        Dim clients_id_filter_list As New List(Of String)
                        clients_id_filter_list.Add(filters_dict(filter_code))
                        AggregationComputer.UpdateclientsFilters(clients_id_filter_list)

                    Case ControllingUI2Controller.PRODUCT_CODE
                        Dim products_id_filter_list As New List(Of String)
                        products_id_filter_list.Add(filters_dict(filter_code))
                        AggregationComputer.UpdateproductsFilters(products_id_filter_list)

                    Case ControllingUI2Controller.ADJUSTMENT_CODE
                        Dim adjustments_id_filter_list As New List(Of String)
                        adjustments_id_filter_list.Add(filters_dict(filter_code))
                        AggregationComputer.UpdateadjustmentsFilters(adjustments_id_filter_list)

                    Case ControllingUI2Controller.ENTITY_CATEGORY_CODE
                        Dim entities_id_filter_list As List(Of String) = SQLFilterListsGenerators.GetEntitiesFilterList(str_filter_SQL)
                        AggregationComputer.UpdateEntitiesFilters(entities_id_filter_list)

                    Case ControllingUI2Controller.CLIENT_CATEGORY_CODE
                        Dim clients_id_filter_list As List(Of String) = SQLFilterListsGenerators.GetAnalysisAxisFilterList(CLIENTS_TABLE, str_filter_SQL)
                        AggregationComputer.UpdateclientsFilters(clients_id_filter_list)

                    Case ControllingUI2Controller.PRODUCT_CATEGORY_CODE
                        Dim products_id_filter_list As List(Of String) = SQLFilterListsGenerators.GetAnalysisAxisFilterList(PRODUCTS_TABLE, str_filter_SQL)
                        AggregationComputer.UpdateproductsFilters(products_id_filter_list)

                End Select
            End If
        Next

    End Sub

    Protected Friend Sub ComputeAndFillDD(ByVal filters_dict As Dictionary(Of String, String), _
                                          ByRef versions_id_list As List(Of String), _
                                          ByRef versions_dict As Dictionary(Of String, Hashtable), _
                                          ByRef destination_currency As String)

        For Each Version_id As String In versions_id_list

            filters_dict(ControllingUI2Controller.VERSIONS_CODE) = Version_id
            total_years_count = versions_dict(Version_id)(Version.PERIOD_DICT).keys.count
            AggregationComputer.compute_selection_complete(Version_id, _
                                                           versions_dict(Version_id)(VERSIONS_TIME_CONFIG_VARIABLE), _
                                                           versions_dict(Version_id)(VERSIONS_RATES_VERSION_ID_VAR), _
                                                           versions_dict(Version_id)(Version.PERIOD_LIST), _
                                                           destination_currency, _
                                                           versions_dict(Version_id)(VERSIONS_START_PERIOD_VAR), _
                                                           versions_dict(Version_id)(VERSIONS_NB_PERIODS_VAR), _
                                                           PBar)

            AggregationComputer.LoadOutputMatrix(PBar)
            If versions_dict(Version_id)(VERSIONS_TIME_CONFIG_VARIABLE) = MONTHLY_TIME_CONFIGURATION Then
                AggregationComputer.ComputeMonthlyPeriodsAggregations(Version_id, _
                                                                      destination_currency, _
                                                                      versions_dict(Version_id)(VERSIONS_RATES_VERSION_ID_VAR), _
                                                                      versions_dict(Version_id)(VERSIONS_START_PERIOD_VAR), _
                                                                      versions_dict(Version_id)(VERSIONS_NB_PERIODS_VAR), _
                                                                      versions_dict(Version_id)(Version.PERIOD_DICT))

            End If
            DataDictionaryFill(DataDictionary, _
                               hierarchies_node.Nodes(0), _
                               filters_dict, _
                               versions_dict(Version_id)(Version.PERIOD_DICT))
        Next

    End Sub



#End Region


#Region "Data Dictionary Filling Loops"

    ' Prerequisites: 
    '   - Hierarchies_node -> rows_hierarchy + columns_hierarchy
    '   - DataDictionary has already been initialized (all values to 0)
    Private Sub DataDictionaryFill(ByRef current_data_dictionary As Dictionary(Of Object, Object), _
                                   ByRef hierarchies_node As TreeNode, _
                                   ByRef filters_dict As Dictionary(Of String, String), _
                                   ByRef periods_dict As Dictionary(Of Int32, Int32()))

        If hierarchies_node.NextNode Is Nothing Then

            Select Case hierarchies_node.Name
                Case ControllingUI2Controller.ACCOUNTS_CODE
                    account_index = 0
                    For Each account_id As String In AggregationComputer.get_model_accounts_list
                        Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.ACCOUNTS_CODE, account_id)
                        SetOrAddValueToHT(current_data_dictionary, account_id, GetValue(filters_dict))
                        account_index = account_index + 1
                    Next

                Case ControllingUI2Controller.ENTITIES_CODE
                    For Each entity_id As String In AggregationComputer.complete_data_dictionary.Keys
                        Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.ENTITIES_CODE, entity_id)
                        SetOrAddValueToHT(current_data_dictionary, entity_id, GetValue(filters_dict))
                    Next

                Case ControllingUI2Controller.YEARS_CODE
                    year_index = 0
                    For Each year_id As Int32 In periods_dict.Keys
                        Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.YEARS_CODE, year_id)
                        SetOrAddValueToHT(current_data_dictionary, year_id, GetValue(filters_dict))
                        year_index = year_index + 1
                    Next

                Case ControllingUI2Controller.MONTHS_CODE
                    ' Loop with month = 0 (years aggregation)
                    Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.MONTHS_CODE, ControllingUI2Controller.ZERO_PERIOD_CODE)
                    SetOrAddValueToHT(current_data_dictionary, _
                                      ControllingUI2Controller.ZERO_PERIOD_CODE, _
                                      GetValue(filters_dict))

                    ' Months Loop 
                    month_index = 0
                    For Each month_id As Int32 In periods_dict(filters_dict(ControllingUI2Controller.YEARS_CODE))
                        Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.MONTHS_CODE, month_id)
                        SetOrAddValueToHT(current_data_dictionary, month_id, GetValue(filters_dict))
                        month_index = month_index + 1
                    Next

                Case Else
                    SetOrAddValueToHT(current_data_dictionary, filters_dict(hierarchies_node.Name), GetValue(filters_dict))

            End Select

        Else
            Select Case hierarchies_node.Name
                Case ControllingUI2Controller.ACCOUNTS_CODE

                    account_index = 0
                    For Each account_id As String In AggregationComputer.get_model_accounts_list
                        Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.ACCOUNTS_CODE, account_id)

                        DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, account_id), _
                                           hierarchies_node.NextNode, _
                                           filters_dict, _
                                           periods_dict)
                        account_index = account_index + 1
                    Next

                Case ControllingUI2Controller.ENTITIES_CODE

                    For Each entity_id As String In AggregationComputer.complete_data_dictionary.Keys
                        Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.ENTITIES_CODE, entity_id)

                        DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, entity_id), _
                                           hierarchies_node.NextNode, _
                                           filters_dict, _
                                           periods_dict)
                    Next

                Case ControllingUI2Controller.YEARS_CODE
                    year_index = 0
                    For Each year_id As Int32 In periods_dict.Keys

                        Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.YEARS_CODE, year_id)
                        DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, year_id), _
                                           hierarchies_node.NextNode, _
                                           filters_dict, _
                                           periods_dict)
                        year_index = year_index + 1
                    Next

                Case ControllingUI2Controller.MONTHS_CODE
                    ' Loop with month = 0 (years aggregation)
                    Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.MONTHS_CODE, ControllingUI2Controller.ZERO_PERIOD_CODE)
                    DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, ControllingUI2Controller.ZERO_PERIOD_CODE), _
                                       hierarchies_node.NextNode, _
                                       filters_dict, _
                                       periods_dict)
                    ' Months Loop 
                    month_index = 0
                    For Each month_id As Int32 In periods_dict(filters_dict(ControllingUI2Controller.YEARS_CODE))
                        Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.MONTHS_CODE, month_id)
                        DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, month_id), _
                                           hierarchies_node.NextNode, _
                                           filters_dict, _
                                           periods_dict)
                        month_index = month_index + 1
                    Next

                Case Else
                    DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, filters_dict(hierarchies_node.Name)), _
                                       hierarchies_node.NextNode, _
                                       filters_dict, _
                                       periods_dict)
            End Select
        End If

    End Sub

    Private Function GetValue(ByRef filters_dict As Dictionary(Of String, String)) As Double

        If filters_dict.ContainsKey(ControllingUI2Controller.MONTHS_CODE) Then
            If filters_dict.ContainsKey(ControllingUI2Controller.MONTHS_CODE) = ControllingUI2Controller.ZERO_PERIOD_CODE Then
                Dim data_index As Int32 = ((account_index * total_years_count) + year_index)
                Return AggregationComputer.years_aggregation_data_dictionary(filters_dict(ControllingUI2Controller.ENTITIES_CODE))(data_index)
            Else
                Dim data_index As Int32 = ((account_index * total_years_count * NB_MONTHS) + month_index)
                Return AggregationComputer.complete_data_dictionary(filters_dict(ControllingUI2Controller.ENTITIES_CODE))(data_index)
            End If
        Else
            Dim data_index As Int32 = ((account_index * total_years_count) + year_index)
            Return AggregationComputer.complete_data_dictionary(filters_dict(ControllingUI2Controller.ENTITIES_CODE))(data_index)
        End If

    End Function

#End Region


#Region "Utilities"

    Private Sub BuildHierarchiesNodes(ByRef rows_display_nodes As TreeNode, _
                                      ByRef columns_display_nodes As TreeNode)

        hierarchies_node.Nodes.Clear()
        For Each node As TreeNode In rows_display_nodes.Nodes
            hierarchies_node.Nodes.Add(node.Name, node.Text)
        Next
        For Each node As TreeNode In columns_display_nodes.Nodes
            hierarchies_node.Nodes.Add(node.Name, node.Text)
        Next

    End Sub

    Private Function SetOrAddHTToHT(ByRef HT As Dictionary(Of Object, Object), ByRef key As String) As Dictionary(Of Object, Object)

        If HT.ContainsKey(key) Then
            Return HT(key)
        Else
            Dim tmp_ht As New Dictionary(Of Object, Object)
            HT.Add(key, tmp_ht)
            Return HT(key)
        End If

    End Function

    Private Sub SetOrAddValueToHT(ByRef HT As Dictionary(Of Object, Object), _
                                  ByRef key As Object, _
                                  ByRef value As Double)

        If HT.ContainsKey(key) Then
            HT(key) = value
        Else
            HT.Add(key, Nothing)
            HT(key) = value
        End If

    End Sub

    Protected Friend Sub CloseModel()

        AggregationComputer.delete_model()

    End Sub

#End Region


End Class
