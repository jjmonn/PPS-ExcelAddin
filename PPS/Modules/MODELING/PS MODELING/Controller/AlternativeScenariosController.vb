' AlternativeScenariosController.vb
'
'
' To do: 
'       -
'
'
'
' Author:Julien Monnereau
' Last modified: 17/07/2015

Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Linq


Friend Class AlternativeScenariosController


#Region "Instance Variables"

    ' Objects
    Private Model As AlternativeScenarioModel
    Private InputsController As ASInputsController
    Private View As AlternativeScenariosUI
    Private GDFSUEZASExports As New GDFSUEZASExport

    ' Variables
    Private accounts_name_id_dict As Hashtable
    Private items As String() = {AlternativeScenarioModel.INCREMENTAL_TAX, PSDLLL_Interface.INCREMENTAL_REVENUES}


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Model = New AlternativeScenarioModel(Me)
        InputsController = New ASInputsController(Me)
        View = New AlternativeScenariosUI(Me, InputsController, Model.sensitivities_dictionary, GetExportsDictionary, GetAccountsComboBox)
        InputsController.InitializeView(View)
        accounts_name_id_dict = globalvariables.accounts.GetAccountsDictionary(NAME_VARIABLE, ID_VARIABLE)
        View.Show()

    End Sub

    Private Function GetExportsDictionary() As Dictionary(Of String, Dictionary(Of String, String))

        Dim export_dict As New Dictionary(Of String, Dictionary(Of String, String))
        Dim accounts_id_name_dic = globalvariables.accounts.GetAccountsDictionary(ID_VARIABLE, NAME_VARIABLE)

        For Each sensitivity_id In Model.sensitivities_dictionary.Keys
            Dim tmp_dict As New Dictionary(Of String, String)
            For Each item In items
                tmp_dict.Add(item, accounts_id_name_dic(GDFSUEZASExports.ReadExport(item, sensitivity_id)))
            Next
            export_dict.Add(sensitivity_id, tmp_dict)
        Next
        Return export_dict

    End Function

    Private Function GetAccountsComboBox() As ComboBoxEditor

        Dim tmpCB As New ComboBoxEditor
        For Each account_id As String In GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS, NAME_VARIABLE)
            tmpCB.Items.Add(account_id)
        Next
        Return tmpCB

    End Function

#End Region


#Region "Interface"

    Protected Friend Sub ComputeAlternativeScenario()

        If InputsController.ValidateInputsSelection = True Then

            InitializePBar(TreeViewsUtilities.GetNoChildrenNodesList(TreeViewsUtilities.GetNodesKeysList(InputsController.EntitiesTV), InputsController.EntitiesTV), _
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

    Protected Friend Sub UpdateExportMapping(ByRef sensitivity_id As String, _
                                             ByRef item As String, _
                                             ByRef account_name As String)

        GDFSUEZASExports.UpdateExport(item, sensitivity_id, accounts_name_id_dict(account_name))

    End Sub

    Protected Friend Sub ScenarioReinjection(Optional ByRef adjustment_id As String = "")

        InitializePBarForExport()
        If adjustment_id = "" Then adjustment_id = DEFAULT_ANALYSIS_AXIS_ID
        Model.ASReinjection(items, GDFSUEZASExports, InputsController.current_version_id, adjustment_id)
        View.PBar.EndProgress()

    End Sub

    Protected Friend Sub AddProgress()

        View.PBar.AddProgress()

    End Sub

#End Region


#Region "Main Display Functions"

    Private Sub DisplayAlternativeScenario(ByRef new_scenario_data As Dictionary(Of String, Double()), _
                                           ByRef period_list As List(Of UInt32), _
                                           ByRef time_configuration As String)

        View.ClearMainPanel()
        Dim accounts_id_name_dic As Hashtable = GlobalVariables.Accounts.GetAccountsDictionary(ID_VARIABLE, NAME_VARIABLE)
        Dim reports_settings_dic As Dictionary(Of String, Hashtable) = Report.GetReportsSettingsDictionary()
        Dim entity_id = InputsController.current_entity_node.Name
        Dim ReportsTV As New TreeView
        Dim charts_periods = GetChartsPeriods(period_list, time_configuration)
        Report.LoadReportsTV(ReportsTV)

        For Each report_node As TreeNode In ReportsTV.Nodes
            If reports_settings_dic(report_node.Name)(REPORTS_TYPE_VAR) = CHART_REPORT_TYPE Then
                DisplayCharts(entity_id, report_node, new_scenario_data, reports_settings_dic, charts_periods)
            Else
                DisplayDGVs(entity_id, report_node, new_scenario_data, reports_settings_dic, time_configuration, period_list)
            End If
        Next

    End Sub

    Private Sub DisplayCharts(ByRef entity_id As String, _
                              ByRef report_node As TreeNode, _
                              ByRef new_scenario_data As Dictionary(Of String, Double()), _
                              ByRef reports_settings_dic As Dictionary(Of String, Hashtable), _
                              ByRef charts_periods As List(Of Int32))

        reports_settings_dic(report_node.Name)(REPORTS_NAME_VAR) = reports_settings_dic(report_node.Name)(REPORTS_NAME_VAR) & " Base Scenario"
        reports_settings_dic(report_node.Name)(REPORTS_NAME_VAR) = reports_settings_dic(report_node.Name)(REPORTS_NAME_VAR) & " New Scenario"
        Dim base_chart As Chart = ChartsUtilities.CreateChart(reports_settings_dic(report_node.Name))
        Dim new_chart As Chart = ChartsUtilities.CreateChart(reports_settings_dic(report_node.Name))
        For Each serie_node As TreeNode In report_node.Nodes
            ChartsUtilities.AddSerieToChart(base_chart, reports_settings_dic(serie_node.Name))
            ChartsUtilities.AddSerieToChart(new_chart, reports_settings_dic(serie_node.Name))
            If Not IsDBNull(reports_settings_dic(serie_node.Name)(REPORTS_ACCOUNT_ID)) Then
                Dim serie_account_id = reports_settings_dic(serie_node.Name)(REPORTS_ACCOUNT_ID)
                base_chart.Series(reports_settings_dic(serie_node.Name)(REPORTS_NAME_VAR)).Points.DataBindXY(charts_periods, Model.current_conso_data_dic(entity_id)(serie_account_id))
                new_chart.Series(reports_settings_dic(serie_node.Name)(REPORTS_NAME_VAR)).Points.DataBindXY(charts_periods, new_scenario_data(serie_account_id))
            End If
        Next
        View.AddCharts(base_chart, new_chart) ' , new_chart, reports_settings_dic(report_node.Name)(REPORTS_NAME_VAR))
        ChartsUtilities.EqualizeChartsYAxis1(base_chart, new_chart)
        ChartsUtilities.AdjustChartPosition(base_chart)
        ChartsUtilities.AdjustChartPosition(new_chart)
        AddHandler base_chart.MouseDown, AddressOf View.Reports_MouseClick
        AddHandler new_chart.MouseDown, AddressOf View.Reports_MouseClick

    End Sub

    Private Sub DisplayDGVs(ByRef entity_id As String, _
                            ByRef report_node As TreeNode, _
                            ByRef new_scenario_data As Dictionary(Of String, Double()), _
                            ByRef reports_settings_dic As Dictionary(Of String, Hashtable), _
                            ByRef time_configuration As String, _
                            ByRef period_list As List(Of UInt32))

        Dim DGV As vDataGridView = DataGridViewsUtil.CreateASDGVReport(period_list, time_configuration)
        For Each serie_node As TreeNode In report_node.Nodes
            If Not IsDBNull(reports_settings_dic(serie_node.Name)(REPORTS_ACCOUNT_ID)) Then
                Dim serie_account_id = reports_settings_dic(serie_node.Name)(REPORTS_ACCOUNT_ID)
                Dim serie_name = reports_settings_dic(serie_node.Name)(REPORTS_NAME_VAR)
                DataGridViewsUtil.AddSerieToBasicDGVReport(DGV.RowsHierarchy.Items(0), serie_name, Model.current_conso_data_dic(entity_id)(serie_account_id))
                DataGridViewsUtil.AddSerieToBasicDGVReport(DGV.RowsHierarchy.Items(1), serie_name, new_scenario_data(serie_account_id))
            End If
        Next
        DataGridViewsUtil.FormatBasicDGV(DGV)
        View.AddDGV(DGV) 'reports_settings_dic(report_node.Name)(REPORTS_NAME_VAR))
        AddHandler DGV.MouseDown, AddressOf View.Reports_MouseClick

    End Sub


#End Region


#Region "Utilities"

    Protected Friend Sub InitializePBar(ByRef input_entities As List(Of UInt32), _
                                        ByRef nb_sensitivities As Int32)

        Dim LoadingBarMax As Integer = input_entities.Count + (nb_sensitivities) * 2 + 5
        View.PBar.Launch(1, LoadingBarMax)

    End Sub

    Private Sub InitializePBarForExport()

        Dim LoadingBarMax As Integer = Model.sensitivities_dictionary.Count _
                                     * Model.SensisResultsDict(Model.sensitivities_dictionary.ElementAt(0).Key).Keys.Count _
                                     * Model.SensisResultsDict(Model.sensitivities_dictionary.ElementAt(0).Key).ElementAt(0).Value.Keys.Count + 2
        View.PBar.Launch(1, LoadingBarMax)

    End Sub

    Protected Friend Function GetChartsPeriods(ByRef period_list As List(Of UInt32), _
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
