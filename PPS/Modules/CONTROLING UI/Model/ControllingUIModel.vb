' ControllingUIAggregationComputer.vb
'
'
' To do
'
'
'
'
' Author: Julien Monnereau
' Last modified: 24/03/2015

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
    Protected Friend categories_id_code_dict As New Dictionary(Of String, String)

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

    Protected Friend Sub IntializeComputerAndFilter(ByRef entity_node As TreeNode)

        AggregationComputer.init_computer_complete_mode(entity_node)
        AggregationComputer.ReinitializeFiltersList()

    End Sub

    Protected Friend Sub SetDBFilters(ByRef filters_dict As Dictionary(Of String, String))

        For Each filter_code As String In filters_dict.Keys
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
        Next

    End Sub

    Protected Friend Function GetDataHT(ByRef versions_id_list As List(Of String), _
                                        ByRef versions_dict As Dictionary(Of String, Hashtable), _
                                        ByRef destination_currency As String) As Hashtable

        Dim DataHT As New Hashtable
        For Each Version_id As String In versions_id_list
            compute(DataHT, _
                    Version_id, _
                    versions_dict(Version_id)(VERSIONS_TIME_CONFIG_VARIABLE), _
                    versions_dict(Version_id)(VERSIONS_START_PERIOD_VAR), _
                    versions_dict(Version_id)(VERSIONS_NB_PERIODS_VAR), _
                    versions_dict(Version_id)(VERSIONS_RATES_VERSION_ID_VAR), _
                    versions_dict(Version_id)(Version.PERIOD_LIST), _
                    destination_currency)
        Next
        Return DataHT

    End Function

#End Region


#Region "Computations loops"

    ' Compute and Stores the (versions)(Entities)(Accounts)(Perios) HT
    Private Sub compute(ByRef versions_HT As Hashtable, _
                        ByRef version_id As String, _
                        ByRef time_config As String, _
                        ByRef start_period As Int32, _
                        ByRef nb_periods As Int32, _
                        ByRef rates_version_id As String, _
                        ByRef period_list As List(Of Int32), _
                        ByRef currency As String)

        AggregationComputer.compute_selection_complete(version_id, _
                                                     time_config, _
                                                     rates_version_id, _
                                                     period_list, _
                                                     currency, _
                                                     start_period, _
                                                     nb_periods, _
                                                     PBar)

        AggregationComputer.LoadOutputMatrix(PBar)
        If time_config = YEARLY_TIME_CONFIGURATION Then
            versions_HT.Add(version_id, _
                            GetModelCurrentDataDictionary(AggregationComputer.complete_data_dictionary, _
                                                          period_list))
        Else
            Dim global_periods_dic As Dictionary(Of Int32, Int32())
            Dim years_data_dic As Dictionary(Of String, Double()) = AggregationComputer.ComputeMonthlyPeriodsAggregations(version_id, _
                                                                                                                            currency, _
                                                                                                                            rates_version_id, _
                                                                                                                            start_period, _
                                                                                                                            nb_periods, _
                                                                                                                            global_periods_dic)
            versions_HT.Add(version_id, _
                            GetModelCurrentDataDictionary_monthly(AggregationComputer.complete_data_dictionary, _
                                                                  years_data_dic, _
                                                                  global_periods_dic))

        End If

    End Sub

    ' Below: those could go in the Aggregation Computer ! 
    ' Would imply to reimplement all GenericAggregationDLL3Computing dependant processes

    ' Returns (entities)(accounts)(years)(total)
    Private Function GetModelCurrentDataDictionary(ByRef data_dict As Dictionary(Of String, Double()), _
                                                   ByRef periods_list As List(Of Int32)) As Hashtable

        Dim tmp_HT As New Hashtable
        For Each entity_id As String In data_dict.Keys
            Dim Entity_HT As New Hashtable
            Dim account_index As Int32 = 0
            For Each account_id As String In AggregationComputer.get_model_accounts_list
                Dim Account_HT As New Hashtable
                For Each period As Int32 In periods_list
                    Dim years_HT As New Hashtable
                    Dim data_index As Int32 = (account_index * periods_list.Count) + periods_list.IndexOf(period)
                    years_HT.Add(ControllingUI2Controller.ZERO_PERIOD_CODE, data_dict(entity_id)(data_index))
                    Account_HT.Add(period, years_HT)
                Next
                Entity_HT.Add(account_id, Account_HT)
                account_index = account_index + 1
            Next
            tmp_HT.Add(entity_id, Entity_HT)
        Next
        Return tmp_HT

    End Function

    ' Returns (entities)(accounts)(years)(months)
    Private Function GetModelCurrentDataDictionary_monthly(ByRef months_data_dict As Dictionary(Of String, Double()), _
                                                           ByRef years_data_dict As Dictionary(Of String, Double()), _
                                                           ByRef global_periods_list As Dictionary(Of Int32, Int32())) As Hashtable

        Dim tmp_HT As New Hashtable
        For Each entity_id As String In years_data_dict.Keys
            Dim Entity_HT As New Hashtable
            Dim account_index As Int32 = 0
            For Each account_id As String In AggregationComputer.get_model_accounts_list

                Dim Account_HT As New Hashtable
                Dim year_index As Int32 = 0
                Dim month_index As Int32 = 0
                For Each year_period As Int32 In global_periods_list.Keys
                    Dim years_HT As New Hashtable
                    years_HT.Add(ControllingUI2Controller.ZERO_PERIOD_CODE, years_data_dict(entity_id)(account_index * year_index))

                    For Each month_period As Int32 In global_periods_list(year_period)
                        years_HT.Add(month_period, months_data_dict(entity_id)(account_index * month_index))
                        month_index = month_index + 1
                    Next
                    Account_HT.Add(year_period, years_HT)
                    year_index = year_index + 1

                Next
                Entity_HT.Add(account_id, Account_HT)
                account_index = account_index + 1
            Next
            tmp_HT.Add(entity_id, Entity_HT)
        Next
        Return tmp_HT

    End Function

#End Region


#Region "Utilities"

    Protected Friend Sub CloseModel()

        AggregationComputer.delete_model()

    End Sub

#End Region


End Class
