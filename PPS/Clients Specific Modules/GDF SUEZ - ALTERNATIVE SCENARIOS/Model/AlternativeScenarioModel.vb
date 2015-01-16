' AlternativeScenariosModel.vb
'
'
' To do: 
'       -
'
'
'
' Author:Julien Monnereau
' Last modified:16/01/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections


Friend Class AlternativeScenarioModel

#Region "Instance Variables"

    ' Objects
    Private BaseComputer As CControlingMODEL
    Private VersionsMGT As New CVersionsForControlingUIs

    ' Variables
    Private market_indexes_list As List(Of String)
    Private indexes_list As List(Of String)
    Private entities_attributes_dictionary As New Dictionary(Of String, Hashtable)
    Private sensitivities_dictionary As New Dictionary(Of String, Hashtable)
    Private volumes As Double()
    Private base_revenues As Double()

    ' Data Dictionaries
    Private SensisResultsDict As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double())))
    Private current_conso_data_dic As New Dictionary(Of String, Dictionary(Of String, Double()))
    Private new_scenario_aggregates As New Dictionary(Of String, Double())

    ' Current Config
    Protected Friend periods_list As List(Of Int32)
    Protected Friend time_configuration As String
    Private entities_id_list As List(Of String)


#End Region


    Protected Friend Sub New()

        BaseComputer = New CControlingMODEL(VersionsMGT.PERIODSMGT.yearlyPeriodList)
        indexes_list = MarketIndexesMapping.GetMarketIndexesList()
        entities_attributes_dictionary = GDFSUEZEntitiesAttributes.GetEntitiesAttributes()
        sensitivities_dictionary = GDFSUEZSensitivitiesMapping.GetSensitivitiesDictionary()


    End Sub


#Region "Interface"

    
    Protected Friend Sub ComputeEntity(ByRef version_id As String, _
                                       ByRef entity_node As TreeNode, _
                                       ByRef PBar As ProgressBarControl)

        ' InitializePBar()-> controller
        BaseComputer.init_computer_complete_mode(entity_node)
        periods_list = VersionsMGT.GetPeriodList(version_id)
        time_configuration = VersionsMGT.versionsCodeTimeSetUpDict(version_id)(VERSIONS_TIME_CONFIG_VARIABLE)

        BaseComputer.compute_selection_complete(version_id, _
                                                PBar, _
                                                time_configuration, _
                                                GLOBALCurrentRatesVersionCode, _
                                                periods_list, _
                                                MAIN_CURRENCY)

        entities_id_list = cTreeViews_Functions.GetNoChildrenNodesList(cTreeViews_Functions.GetNodesKeysList(entity_node), entity_node.TreeView)
        BuildDataDic(entity_node)
        '  register version id and entity id in controller (before)
        ' end pbar in controller

    End Sub

    Protected Friend Sub ComputeSensitivities(ByRef market_prices_version_id As String)


        SensisResultsDict.Clear()
        For Each sensitivity_id In sensitivities_dictionary.Keys
            Dim PSDLL As New PSDLLL_Interface(indexes_list.ToArray, _
                                              entities_id_list.ToArray, _
                                              GetFormulas(sensitivities_dictionary(sensitivity_id)(GDF_SENSITIVITIES_FORMULA_NAME_VAR)).ToArray, _
                                              periods_list.Count)

            For Each index In market_indexes_list
                Dim index_prices As Double() = MarketPricesMapping.GetIndexMarketPricesFlatArray(index, _
                                                                                                 market_prices_version_id, _
                                                                                                 periods_list.ToArray, _
                                                                                                 time_configuration)
                PSDLL.ResgisterIndexMarketPrices(index_prices, index)
            Next
            BuildVolumesAndBaseRevenuesFlatArrays(sensitivity_id)
            PSDLL.Compute(volumes, base_revenues, GetTaxRatesFlatArray())
            SensisResultsDict.Add(sensitivity_id, PSDLL.GetResultsDict())
        Next

    End Sub

    Protected Friend Sub AggregateSensis()


    End Sub

    Protected Friend Sub AggregateNewScenario()


        ' new = base + sensi aggreg()
        '  new_scenario_aggregates


        ' stub data visualization prototype




    End Sub

#End Region


#Region "Base Scenario Computations"

    Private Sub BuildDataDic(ByRef entity_node As TreeNode)

        current_conso_data_dic.Clear()
        For Each entity_id In cTreeViews_Functions.GetNodesKeysList(entity_node)
            Dim tmp_dict As New Dictionary(Of String, Double())
            Dim tmp_data_array = BaseComputer.GetEntityArray(entity_id)

            Dim i As Int32 = 0
            For Each account_id In BaseComputer.get_model_accounts_list
                Dim account_array(periods_list.Count - 1) As Double
                For j = 0 To periods_list.Count - 1
                    account_array(j) = tmp_data_array(i)
                    i = i + 1
                Next
                tmp_dict.Add(account_id, account_array)
            Next
            current_conso_data_dic.Add(entity_id, tmp_dict)
        Next
        BaseComputer.clear_complete_data_dictionary()
        ' Erase array in dll3 ?

    End Sub

    Private Sub BuildVolumesAndBaseRevenuesFlatArrays(ByRef sensitivity_id As String)

        ReDim volumes(entities_id_list.Count * periods_list.Count)
        ReDim base_revenues(entities_id_list.Count * periods_list.Count)

        Dim volume_account_id = sensitivities_dictionary(sensitivity_id)(GDF_SENSITIVITIES_VOLUMES_ACCOUNT_ID_VAR)
        Dim revenues_account_id = sensitivities_dictionary(sensitivity_id)(GDF_SENSITIVITIES_REVENUES_ACCOUNT_ID_VAR)
        Dim initial_unit = sensitivities_dictionary(sensitivity_id)(GDF_SENSITIVITIES_INITIAL_UNIT_VAR)
        Dim destination_unit = sensitivities_dictionary(sensitivity_id)(GDF_SENSITIVITIES_DEST_UNIT_VAR)
        Dim conversion_rate As Double = 1

        If initial_unit <> destination_unit Then OperationalUnitConversionMapping.GetConversionRate(initial_unit & CURRENCIES_SEPARATOR & destination_unit, conversion_rate)

        Dim index As Int32 = 0
        For Each entity_id In entities_id_list
            For j As Int32 = 0 To periods_list.Count - 1
                base_revenues(index) = current_conso_data_dic(entity_id)(revenues_account_id)(j)
                volumes(index) = current_conso_data_dic(entity_id)(volume_account_id)(j) * conversion_rate
                index = index + 1
            Next
        Next

    End Sub


#End Region


#Region "Sensitivities Computation"

    Private Function GetFormulas(ByRef formula_id As String) As List(Of String)

        Dim formulas As New List(Of String)
        For Each entity_id In entities_id_list
            formulas.Add(entities_attributes_dictionary(entity_id)(formula_id))
        Next
        Return formulas

    End Function

    Private Function GetTaxRatesFlatArray() As Double()

        Dim tax_rates_flat_array(entities_id_list.Count * periods_list.Count) As Double
        Dim index As Int32 = 0
        For Each entity_id In entities_id_list
            For Each period In periods_list
                Dim tax_rate As Double = entities_attributes_dictionary(entity_id)(GDF_ENTITIES_AS_TAX_RATE_VAR)
                tax_rates_flat_array(index) = tax_rate
                index = index + 1
            Next
        Next

        Return tax_rates_flat_array

    End Function



#End Region





End Class
