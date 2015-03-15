' ControllingUIAggregationComputer.vb
'
'
' To do
'
'
'
'
' Author: Julien Monnereau
' Last modified: 10/03/2015

Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class ControllingUIModel


#Region "Instance Variables"

    ' Objects
    Private AggregationComputer As GenericAggregationDLL3Computing
    Private ESB As EntitiesSelectionBuilderClass
    Private Categories As TreeNode
    Private PBar As ProgressBarControl

    ' Variables
    Protected Friend model_data_dict As New Hashtable
    Private versions_dict As Dictionary(Of String, Hashtable)


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_ESB As EntitiesSelectionBuilderClass)

        AggregationComputer = New GenericAggregationDLL3Computing(GlobalVariables.GlobalDBDownloader)
        ESB = input_ESB

    End Sub

#End Region


#Region "Interface"

    Protected Friend Function BuildGeneralDataHT(ByRef entity_node As TreeNode, _
                                                 ByRef versions_id_array As String(), _
                                                 ByRef versions_dic As Dictionary(Of String, Hashtable), _
                                                 ByRef categories_array As String(), _
                                                 ByRef adjustments_filters_list As List(Of String))

        For Each category_id As String In categories_array

            ' the model config depends on categories -> changes selection  !!!
            ' use ESB for categories break down ?
            ' play on the entities selection
            ' maybe review the entities selection filter process on categories -> should use relational DB
            ' what if a category filter is already applied ?
            ' -> must be additive with categories filter

            AggregationComputer.init_computer_complete_mode(entity_node)
            VersionsLoop(category_id, _
                         entity_node, _
                         versions_id_array, _
                         adjustments_filters_list)
        Next

    End Function


#End Region


#Region "Computations loops"

    ' Loop through versions and Launch computations/ storage
    Private Sub VersionsLoop(ByRef category_id As String, _
                             ByRef entity_node As TreeNode, _
                             ByRef versions_array As String(), _
                             ByRef adjustments_filter_list As List(Of String))

        Dim versionIndex As Int32 = 0
        Dim Years_Versions_HT As New Hashtable
        For Each Version_id As String In versions_array
            compute(entity_node, _
                    versions_dict(Version_id)(VERSIONS_TIME_CONFIG_VARIABLE), _
                    Years_Versions_HT, _
                    Version_id, _
                    versions_dict(Version_id)(VERSIONS_RATES_VERSION_ID_VAR), _
                    adjustments_filter_list)

            versionIndex = versionIndex + 1
        Next
        model_data_dict.Add(category_id, Years_Versions_HT)
    
    End Sub

    ' Compute and Stores the (versions)(Entities)(Accounts)(Perios) HT
    Private Sub compute(ByRef entity_node As TreeNode, _
                        ByRef time_config As String, _
                        ByRef versions_HT As Hashtable, _
                        ByRef version_id As String, _
                        ByRef currency As String, _
                        ByRef adjustments_filter_list As List(Of String))

        Dim start_period As Int32 = versions_dict(version_id)(VERSIONS_START_PERIOD_VAR)
        Dim nb_periods As Int32 = versions_dict(version_id)(VERSIONS_NB_PERIODS_VAR)
        Dim rates_version_id As String = versions_dict(version_id)(VERSIONS_RATES_VERSION_ID_VAR)

        AggregationComputer.compute_selection_complete(version_id, _
                                                     versions_dict(version_id)(VERSIONS_TIME_CONFIG_VARIABLE), _
                                                     rates_version_id, _
                                                     versions_dict(version_id)(Version.PERIOD_LIST), _
                                                     currency, _
                                                     start_period, _
                                                     nb_periods, _
                                                     PBar, _
                                                     ESB.StrSqlQuery, _
                                                     adjustments_filter_list)

        AggregationComputer.LoadOutputMatrix(PBar)
        If time_config = YEARLY_TIME_CONFIGURATION Then
            versions_HT.Add(version_id, _
                            GetModelCurrentDataDictionary(AggregationComputer.complete_data_dictionary, _
                                                          versions_dict(version_id)(Version.PERIOD_LIST)))
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


#End Region


#Region "Utilities"

    Protected Friend Sub ClearDataDictionaries()

        data_dict.Clear()
       
    End Sub

    ' Below: those could go in the Aggregation Computer !

    ' Returns (entities)(accounts)(years)
    Private Function GetModelCurrentDataDictionary(ByRef data_dict As Dictionary(Of String, Double()), _
                                                   ByRef periods_list As List(Of Int32)) As Hashtable

        Dim tmp_HT As New Hashtable
        For Each entity_id As String In data_dict.Keys
            Dim Entity_HT As New Hashtable
            Dim account_index As Int32 = 0
            For Each account_id As String In AggregationComputer.get_model_accounts_list
                Dim Account_HT As New Hashtable
                For Each period As Int32 In periods_list
                    Account_HT.Add(period, data_dict(entity_id)(account_index * periods_list.IndexOf(period)))
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
                    years_HT.Add(0, years_data_dict(entity_id)(account_index * year_index))

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


End Class
