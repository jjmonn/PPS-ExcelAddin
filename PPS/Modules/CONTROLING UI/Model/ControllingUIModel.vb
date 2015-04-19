' ControllingUIAggregationComputer.vb
'
'
' To do
'
'
'
'
' Author: Julien Monnereau
' Last modified: 17/04/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq

Friend Class ControllingUIModel


#Region "Instance Variables"

    ' Objects
    Private AggregationComputer As GenericAggregationDLL3Computing
    Private PBar As ProgressBarControl

    ' Variables
    Protected Friend DataDictionary As New Dictionary(Of Object, Object)
    Protected Friend categories_id_code_dict As New Dictionary(Of String, String)
    Private hierarchies_node As New TreeNode
    Private account_index As Int32
    Protected Friend categories_values_dict As New Dictionary(Of String, Dictionary(Of String, String))
    Private complete_period_list As List(Of Int32)
    Private years_aggregation_period_list As List(Of Int32)

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_pBar As ProgressBarControl)

        AggregationComputer = New GenericAggregationDLL3Computing(GlobalVariables.GlobalDBDownloader)
        PBar = input_pBar
        InitializeCategoriesIDsCodeDict()

    End Sub

    Private Sub InitializeCategoriesIDsCodeDict()

        categories_id_code_dict = CategoriesMapping.GetCategoriesIDCodeMapping()
        categories_id_code_dict.Add(ControllingUI2Controller.CLIENTS_CODE, ControllingUI2Controller.CLIENTS_CODE)
        categories_id_code_dict.Add(ControllingUI2Controller.PRODUCTS_CODE, ControllingUI2Controller.PRODUCTS_CODE)
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

                    Case ControllingUI2Controller.PRODUCTS_CODE
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

            Dim periods_dict As Dictionary(Of Int32, List(Of Int32)) = versions_dict(Version_id)(Version.PERIOD_DICT)
            filters_dict(ControllingUI2Controller.VERSIONS_CODE) = Version_id
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
            years_aggregation_period_list = periods_dict.Keys.ToList
            complete_period_list = versions_dict(Version_id)(Version.PERIOD_LIST)

            filters_dict(ControllingUI2Controller.ENTITIES_CODE) = categories_values_dict(ControllingUI2Controller.ENTITIES_CODE).Keys(0)
            DataDictionaryFill(DataDictionary, _
                               hierarchies_node.Nodes(0), _
                               filters_dict, _
                               versions_dict(Version_id)(Version.PERIOD_DICT), _
                               versions_dict(Version_id)(VERSIONS_TIME_CONFIG_VARIABLE))
            PBar.AddProgress(2)
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
                                   ByRef periods_dict As Dictionary(Of Int32, List(Of Int32)), _
                                   ByRef time_config As String, _
                                   Optional ByRef period_id As Int32 = 0)

        If hierarchies_node.NextNode Is Nothing Then

            Select Case hierarchies_node.Name
                Case ControllingUI2Controller.ACCOUNTS_CODE
                    account_index = 0
                    For Each account_id As String In AggregationComputer.get_model_accounts_list
                        filters_dict(ControllingUI2Controller.ACCOUNTS_CODE) = account_id
                        current_data_dictionary(account_id) = GetValue(filters_dict, period_id, time_config)
                        account_index = account_index + 1
                    Next

                Case ControllingUI2Controller.ENTITIES_CODE
                    For Each entity_id As String In categories_values_dict(ControllingUI2Controller.ENTITIES_CODE).Keys
                        Utilities_Functions.AddOrSetValueToDict(filters_dict, ControllingUI2Controller.ENTITIES_CODE, entity_id)
                        current_data_dictionary(entity_id) = GetValue(filters_dict, period_id, time_config)
                    Next

                Case ControllingUI2Controller.YEARS_CODE
                    For Each year_id As Int32 In categories_values_dict(ControllingUI2Controller.YEARS_CODE).Keys
                        filters_dict(ControllingUI2Controller.YEARS_CODE) = year_id
                        period_id = year_id
                        current_data_dictionary(year_id) = GetValue(filters_dict, period_id, time_config)
                    Next

                Case ControllingUI2Controller.MONTHS_CODE
                    ' Loop with month = 0 (years aggregation)
                    period_id = filters_dict(ControllingUI2Controller.YEARS_CODE)
                    filters_dict(ControllingUI2Controller.MONTHS_CODE) = ControllingUI2Controller.ZERO_PERIOD_CODE
                    current_data_dictionary(ControllingUI2Controller.ZERO_PERIOD_CODE) = GetValue(filters_dict, period_id, time_config)

                    ' Months Loop 
                    If categories_values_dict.ContainsKey(filters_dict(ControllingUI2Controller.YEARS_CODE)) Then
                        For Each month_id As Int32 In categories_values_dict(filters_dict(ControllingUI2Controller.YEARS_CODE)).Keys
                            period_id = month_id
                            filters_dict(ControllingUI2Controller.MONTHS_CODE) = month_id
                            current_data_dictionary(month_id) = GetValue(filters_dict, period_id, time_config)
                        Next
                    End If

                Case Else
                    current_data_dictionary(filters_dict(hierarchies_node.Name)) = GetValue(filters_dict, period_id, time_config)
            End Select

        Else
            Select Case hierarchies_node.Name
                Case ControllingUI2Controller.ACCOUNTS_CODE

                    account_index = 0
                    For Each account_id As String In AggregationComputer.get_model_accounts_list

                        filters_dict(ControllingUI2Controller.ACCOUNTS_CODE) = account_id

                        DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, account_id), _
                                           hierarchies_node.NextNode, _
                                           filters_dict, _
                                           periods_dict, _
                                           time_config, _
                                           period_id)
                        account_index = account_index + 1
                    Next

                Case ControllingUI2Controller.ENTITIES_CODE

                    For Each entity_id As String In categories_values_dict(ControllingUI2Controller.ENTITIES_CODE).Keys
                        filters_dict(ControllingUI2Controller.ENTITIES_CODE) = entity_id
                        DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, entity_id), _
                                           hierarchies_node.NextNode, _
                                           filters_dict, _
                                           periods_dict, _
                                           time_config, _
                                           period_id)
                    Next

                Case ControllingUI2Controller.YEARS_CODE
                    For Each year_id As Int32 In categories_values_dict(ControllingUI2Controller.YEARS_CODE).Keys
                        period_id = year_id
                        filters_dict(ControllingUI2Controller.YEARS_CODE) = year_id
                        filters_dict(ControllingUI2Controller.MONTHS_CODE) = ControllingUI2Controller.ZERO_PERIOD_CODE
                        DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, year_id), _
                                           hierarchies_node.NextNode, _
                                           filters_dict, _
                                           periods_dict, _
                                           time_config, _
                                           period_id)
                    Next

                Case ControllingUI2Controller.MONTHS_CODE
                    ' Loop with month = 0 (years aggregation)
                    period_id = filters_dict(ControllingUI2Controller.YEARS_CODE)
                    filters_dict(ControllingUI2Controller.MONTHS_CODE) = ControllingUI2Controller.ZERO_PERIOD_CODE
                    DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, ControllingUI2Controller.ZERO_PERIOD_CODE), _
                                       hierarchies_node.NextNode, _
                                       filters_dict, _
                                       periods_dict, _
                                       time_config, _
                                       period_id)
                    ' Months Loop 
                    If categories_values_dict.ContainsKey(filters_dict(ControllingUI2Controller.YEARS_CODE)) Then
                        For Each month_id As Int32 In categories_values_dict(filters_dict(ControllingUI2Controller.YEARS_CODE)).Keys
                            period_id = month_id
                            filters_dict(ControllingUI2Controller.MONTHS_CODE) = month_id
                            DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, month_id), _
                                               hierarchies_node.NextNode, _
                                               filters_dict, _
                                               periods_dict, _
                                               time_config, _
                                               period_id)
                        Next
                    End If

                Case Else
                    DataDictionaryFill(SetOrAddHTToHT(current_data_dictionary, filters_dict(hierarchies_node.Name)), _
                                       hierarchies_node.NextNode, _
                                       filters_dict, _
                                       periods_dict, _
                                       time_config, _
                                       period_id)
            End Select
        End If

    End Sub

    Private Function GetValue(ByRef filters_dict As Dictionary(Of String, String), _
                              ByRef period_id As Int32, _
                              ByRef time_config As String) As Double

        On Error GoTo error_handler
        If time_config = MONTHLY_TIME_CONFIGURATION _
        AndAlso filters_dict(ControllingUI2Controller.MONTHS_CODE) = ControllingUI2Controller.ZERO_PERIOD_CODE Then
            If years_aggregation_period_list.Contains(period_id) Then
                Dim data_index As Int32 = ((account_index * years_aggregation_period_list.Count) + years_aggregation_period_list.IndexOf(period_id))
                Return AggregationComputer.years_aggregation_data_dictionary(filters_dict(ControllingUI2Controller.ENTITIES_CODE))(data_index)
            Else
                Return 0
            End If          
        Else
            Dim data_index As Int32 = ((account_index * complete_period_list.Count) + complete_period_list.IndexOf(period_id))
            Return AggregationComputer.complete_data_dictionary(filters_dict(ControllingUI2Controller.ENTITIES_CODE))(data_index)
        End If

error_handler:
        ' debug to be deleted !
        'If period_id = -1 Then MsgBox("Careful period_id = -1")
        Return 0

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

    Protected Friend Sub CloseModel()

        AggregationComputer.delete_model()

    End Sub

#End Region


End Class
