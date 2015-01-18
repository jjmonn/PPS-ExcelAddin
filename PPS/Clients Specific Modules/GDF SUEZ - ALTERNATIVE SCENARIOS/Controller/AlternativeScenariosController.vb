Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Windows.Forms.DataVisualization.Charting

' AlternativeScenariosController.vb
'
'
' To do: 
'       -
'
'
'
' Author:Julien Monnereau
' Last modified:17/01/2015



Friend Class AlternativeScenariosController


#Region "Instance Variables"

    ' Objects
    Private Model As AlternativeScenarioModel
    Private InputsController As ASInputsController
    Private View As AlternativeScenariosUI

    ' Variables



#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Model = New AlternativeScenarioModel(Me)
        InputsController = New ASInputsController(Me)
        View = New AlternativeScenariosUI(Me, InputsController, Model.sensitivities_dictionary)
        InputsController.InitializeView(View)
        View.Show()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub ComputeAlternativeScenario()

        If InputsController.ValidateInputsSelection = True Then

            InitializePBar(cTreeViews_Functions.GetNoChildrenNodesList(cTreeViews_Functions.GetNodesKeysList(InputsController.EntitiesTV), InputsController.EntitiesTV), _
                           Model.sensitivities_dictionary.Keys.Count)
            Model.ComputeEntity(InputsController.current_version_id, InputsController.current_entity_node, View.PBar)
            View.PBar.AddProgress(2)

            Model.ComputeSensitivities(InputsController.current_market_prices_version_id, View.PBar)
            View.PBar.AddProgress(1)

            Model.AggregateSensis(InputsController.current_entity_node)
            View.PBar.AddProgress(1)

            Dim alternative_scenario_accounts_dict As Dictionary(Of String, String) = GDFSUEZASAccountsMapping.GetAlternativeScenarioAccounts()
            Dim new_scenario_data As Dictionary(Of String, Double()) = Model.AggregateNewScenario(InputsController.current_entity_node.Name, alternative_scenario_accounts_dict)
            DisplayAlternativeScenario(new_scenario_data, Model.periods_list, Model.time_configuration)

            View.DisplaySensibilitiesTabs(Model.SensisResultsDict, Model.periods_list, Model.time_configuration, InputsController.current_entity_node)
            View.PBar.EndProgress()
            View.MainTabControl.SelectedTab = View.MainTabControl.TabPages(1)

        End If

    End Sub


#End Region


#Region "Main Display Functions"

    Private Sub DisplayAlternativeScenario(ByRef new_scenario_data As Dictionary(Of String, Double()), _
                                           ByRef period_list As List(Of Int32), _
                                           ByRef time_configuration As String)

        ' split this function !!!! -> separate charts and tables ?
        View.ClearMainPanel()
        Dim accounts_id_name_dic As Hashtable = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_NAME_VARIABLE)
        Dim reports_settings_dic As Dictionary(Of String, Hashtable) = GDFSUEZASReport.GetReportsSettingsDictionary()
        Dim entity_id = InputsController.current_entity_node.Name
        Dim ReportsTV As New TreeView
        Dim charts_periods = GetChartsPeriods(period_list, time_configuration)
        GDFSUEZASReport.LoadAlternativeScenarioReportsTV(ReportsTV)

        For Each report_node As TreeNode In ReportsTV.Nodes

            If reports_settings_dic(report_node.Name)(GDF_AS_REPORTS_TYPE_VAR) = GDF_CHART_REPORT_TYPE Then
                Dim base_chart As Chart = ChartsUtilities.CreateChart(reports_settings_dic(report_node.Name)(GDF_AS_REPORTS_NAME_VAR), reports_settings_dic(report_node.Name)(GDF_AS_REPORTS_PALETTE_VAR))
                Dim new_chart As Chart = ChartsUtilities.CreateChart(reports_settings_dic(report_node.Name)(GDF_AS_REPORTS_NAME_VAR), reports_settings_dic(report_node.Name)(GDF_AS_REPORTS_PALETTE_VAR))
                For Each serie_node As TreeNode In report_node.Nodes
                    ChartsUtilities.AddSerieToChart(base_chart, reports_settings_dic(serie_node.Name))
                    ChartsUtilities.AddSerieToChart(new_chart, reports_settings_dic(serie_node.Name))
                    If Not IsDBNull(reports_settings_dic(serie_node.Name)(GDF_AS_REPORTS_ACCOUNT_ID)) Then
                        Dim serie_account_id = reports_settings_dic(serie_node.Name)(GDF_AS_REPORTS_ACCOUNT_ID)
                        base_chart.Series(reports_settings_dic(serie_node.Name)(GDF_AS_REPORTS_NAME_VAR)).Points.DataBindXY(charts_periods, Model.current_conso_data_dic(entity_id)(serie_account_id))
                        new_chart.Series(reports_settings_dic(serie_node.Name)(GDF_AS_REPORTS_NAME_VAR)).Points.DataBindXY(charts_periods, new_scenario_data(serie_account_id))
                    End If
                Next
                View.AddReports(base_chart, new_chart, reports_settings_dic(report_node.Name)(GDF_AS_REPORTS_NAME_VAR))
                ChartsUtilities.EqualizeChartsYAxis1(base_chart, new_chart)
                AddHandler base_chart.MouseDown, AddressOf View.Reports_MouseClick
                AddHandler new_chart.MouseDown, AddressOf View.Reports_MouseClick
            Else
                Dim base_DGV As vDataGridView = DataGridViewsUtil.CreateBasicDGVReport(period_list, time_configuration)
                Dim new_DGV As vDataGridView = DataGridViewsUtil.CreateBasicDGVReport(period_list, time_configuration)
                For Each serie_node As TreeNode In report_node.Nodes
                    If Not IsDBNull(reports_settings_dic(serie_node.Name)(GDF_AS_REPORTS_ACCOUNT_ID)) Then
                        Dim serie_account_id = reports_settings_dic(serie_node.Name)(GDF_AS_REPORTS_ACCOUNT_ID)
                        Dim serie_name = reports_settings_dic(serie_node.Name)(GDF_AS_REPORTS_NAME_VAR)
                        DataGridViewsUtil.AddSerieToBasicDGVReport(base_DGV, serie_name, Model.current_conso_data_dic(entity_id)(serie_account_id))
                        DataGridViewsUtil.AddSerieToBasicDGVReport(new_DGV, serie_name, new_scenario_data(serie_account_id))
                    End If
                Next
                DataGridViewsUtil.FormatBasicDGV(base_DGV)
                DataGridViewsUtil.FormatBasicDGV(new_DGV)
                View.AddReports(base_DGV, new_DGV, reports_settings_dic(report_node.Name)(GDF_AS_REPORTS_NAME_VAR))
                AddHandler base_DGV.MouseDown, AddressOf View.Reports_MouseClick
                AddHandler new_DGV.MouseDown, AddressOf View.Reports_MouseClick
            End If
        Next

    End Sub



#End Region


#Region "Utilities"

    Protected Friend Sub InitializePBar(ByRef input_entities As List(Of String), _
                                        ByRef nb_sensitivities As Int32)

        Dim LoadingBarMax As Integer = input_entities.Count + (nb_sensitivities) * 2 + 5
        View.PBar.Launch(1, LoadingBarMax)

    End Sub

    Protected Friend Function GetChartsPeriods(ByRef period_list As List(Of Int32), _
                                               ByRef time_config As String)

        Dim charts_periods
        Select Case time_config
            Case MONTHLY_TIME_CONFIGURATION
                charts_periods = New List(Of String)
                For Each period In period_list
                    charts_periods.Add(Month(DateTime.FromOADate(period)))
                Next
            Case YEARLY_TIME_CONFIGURATION
                charts_periods = New List(Of Int32)
                For Each period In period_list
                    charts_periods.Add(Format(DateTime.FromOADate(period), "yyyy"))
                Next
        End Select
        Return charts_periods

    End Function



#End Region


End Class
