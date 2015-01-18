'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 06/01/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections


Friend Class SubmissionsControlsController


#Region "Instance Variables"

    ' Objects
    Private SubmissionControl As New SubmissionControlModel
    Private View As SubmissionsControlUI
    Private Computer As CControlingMODEL
    Private ControlCharts As New ControlChart
    Private VersionsMGT As New CVersionsForControlingUIs
    Private EntitiesTV As New TreeView
    Private ChartsTV As New TreeView

    ' Variables
    Private data_dictionaries As New Dictionary(Of String, Dictionary(Of String, Double()))
    Private charts_dic As New Dictionary(Of String, Chart)
    Private periods_list As List(Of Int32)
    Private version_id As String
    Private successfull_controls_dic As New Dictionary(Of String, List(Of String))
    Private entities_id_list As List(Of String)
    Private charts_periods As New List(Of Int32)


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        View.ChartsTableLayoutPanel.Visible = False
        Entity.LoadEntitiesTree(EntitiesTV)
        ControlChart.LoadControlChartsTree(ChartsTV)
        entities_id_list = cTreeViews_Functions.GetNodesKeysList(EntitiesTV)
        InitializeChartsDictionary()
        View = New SubmissionsControlUI(Me, EntitiesTV, charts_dic)
        version_id = GLOBALCurrentVersionCode
        Computer = New CControlingMODEL(VersionsMGT.PERIODSMGT.yearlyPeriodList)
        View.ChartsTableLayoutPanel.Visible = True
        View.Show()

    End Sub

    Private Sub InitializeChartsDictionary()

        For Each chart_node As TreeNode In ChartsTV.Nodes
            charts_dic.Add(chart_node.Name, ChartsUtilities.CreateChart(chart_node.Text, ControlCharts.ReadControlChart(chart_node.Name, CONTROL_CHART_PALETTE_VARIABLE)))
            For Each serie_node As TreeNode In chart_node.Nodes
                Dim serieHT As Hashtable = ControlCharts.GetSerieHT(serie_node.Name)
                If Not IsDBNull(serieHT(CONTROL_CHART_ACCOUNT_ID_VARIABLE)) Then ChartsUtilities.AddSerieToChart(charts_dic(chart_node.Name), serieHT)
            Next
        Next

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub ControlSubmissions()

        ComputeEntities()
        LaunchControls()

        charts_periods.clear()
        For Each period In periods_list
            charts_periods.Add(Format(DateTime.FromOADate(period), "yyyy"))
        Next

    End Sub

    Protected Friend Sub DisplayEntityControls(ByRef entity_id As String)

        View.UpdateControlDGV(successfull_controls_dic(entity_id))
       
        For Each chart_node In ChartsTV.Nodes
            For Each serie_node In chart_node.nodes
                Dim serieHT As Hashtable = ControlCharts.GetSerieHT(serie_node.Name)
                If Not IsDBNull(serieHT(CONTROL_CHART_ACCOUNT_ID_VARIABLE)) Then
                    charts_dic(chart_node.name).Series(serieHT(CONTROL_CHART_NAME_VARIABLE)).Points.DataBindXY(charts_periods, data_dictionaries(entity_id)(serieHT(CONTROL_CHART_ACCOUNT_ID_VARIABLE)))
                End If
            Next
        Next

        View.EntityTB.Text = EntitiesTV.Nodes.Find(entity_id, True)(0).Text
        View.VersionTB.Text = Version_label_Sub_Ribbon.Text
        View.CurrencyTB.Text = MAIN_CURRENCY

    End Sub

#End Region


#Region "Model Commands"

    Private Sub ComputeEntities()

        If Not EntitiesTV.Nodes(0) Is Nothing Then
            Computer.init_computer_complete_mode(EntitiesTV.Nodes(0))
            InitializePBar()
            periods_list = VersionsMGT.GetPeriodList(version_id)
            Computer.compute_selection_complete(version_id, _
                                                View.PBar, _
                                                VersionsMGT.versionsCodeTimeSetUpDict(version_id)(VERSIONS_TIME_CONFIG_VARIABLE), _
                                                GLOBALCurrentRatesVersionCode, _
                                                periods_list, _
                                                MAIN_CURRENCY)

            BuildDataDictionaries()
            View.PBar.AddProgress()
        End If
        View.PBar.EndProgress()
        View.PBar.Visible = False

    End Sub

    Private Sub BuildDataDictionaries()

        data_dictionaries.Clear()
        For Each entity_id In entities_id_list
            Dim tmp_dic As New Dictionary(Of String, Double())
            Dim tmp_data_array = Computer.GetEntityArray(entity_id)
            Dim i As Int32 = 0
            For Each account_id In Computer.get_model_accounts_list
                Dim account_array(periods_list.Count - 1) As Double
                For j = 0 To periods_list.Count - 1
                    account_array(j) = tmp_data_array(i)
                    i = i + 1
                Next
                tmp_dic.Add(account_id, account_array)
            Next
            data_dictionaries.Add(entity_id, tmp_dic)
        Next

    End Sub

    Private Sub LaunchControls()

        successfull_controls_dic.Clear()
        For Each entity_id In entities_id_list
            successfull_controls_dic.Add(entity_id, SubmissionControl.CheckSubmission(periods_list, data_dictionaries(entity_id)))
        Next

        Dim nodes_icon_dic As New Dictionary(Of String, Int32)
        Dim nb_controls As Int32 = SubmissionControl.controls_dic.Keys.Count
        For Each entity_id In entities_id_list
            If successfull_controls_dic(entity_id).Count = nb_controls Then
                nodes_icon_dic.Add(entity_id, 1)
            Else
                nodes_icon_dic.Add(entity_id, 0)
            End If
        Next
        Entity.LoadEntitiesTree(EntitiesTV, nodes_icon_dic)
        EntitiesTV.CollapseAll()
        EntitiesTV.Refresh()

    End Sub

#End Region


#Region "Utilities"

    Private Sub InitializePBar()

        Dim LoadingBarMax As Integer = Computer.inputs_entities_list.Count + 5
        View.PBar.Launch(1, LoadingBarMax)
        '    View.PBar.Visible = True

    End Sub






#End Region


End Class
