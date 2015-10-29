﻿Imports System.Collections
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections.Generic
Imports System.Windows.Forms


Public Class CUI2Visualization

#Region "Instance variables"

    Private m_controller As ControllingUIController
    Private m_chartTab As Chart()
    Private m_chartIndexes As New Dictionary(Of Chart, Int32)

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_controller As ControllingUIController)

        InitializeComponent()
        m_controller = p_controller
        m_chartTab = {Chart1, Chart2, Chart3, Chart4}

        m_chartIndexes.Add(Chart1, 1)
        m_chartIndexes.Add(Chart2, 2)
        m_chartIndexes.Add(Chart3, 3)
        m_chartIndexes.Add(Chart4, 4)

        Chart1.ContextMenuStrip = m_chartsRightClickMenu
        Chart2.ContextMenuStrip = m_chartsRightClickMenu
        Chart3.ContextMenuStrip = m_chartsRightClickMenu
        Chart4.ContextMenuStrip = m_chartsRightClickMenu

        ' STUB !! -> chart format en settings  (i.e. {0:P0})
        ChartsUtilities.InitializeChartDisplay(Chart1, My.Settings.chart1Title)
        ChartsUtilities.InitializeChartDisplay(Chart2, My.Settings.chart2Title)
        ChartsUtilities.InitializeChartDisplay(Chart3, My.Settings.chart3Title)
        ChartsUtilities.InitializeChartDisplay(Chart4, My.Settings.chart4Title, "%", "", , False, "{0:P0}")

    End Sub

#End Region

#Region "Interface"

    Delegate Sub ClearCharts_Delegate()
    Friend Sub ClearCharts_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New ClearCharts_Delegate(AddressOf ClearCharts_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            Chart1.Series.Clear()
            Chart2.Series.Clear()
            Chart3.Series.Clear()
            Chart4.Series.Clear()
        End If

    End Sub

    Delegate Sub BindData_Delegate(ByRef p_chartNb As UInt32, _
                                   ByRef p_serieName As String, _
                                   ByRef p_dataX As IEnumerable, _
                                   ByRef p_dataY As IEnumerable, _
                                    ByRef p_axisType As AxisType)
    Friend Sub BindData_ThreadSafe(ByRef p_chartNb As UInt32, _
                                   ByRef p_serieName As String, _
                                   ByRef p_dataX As IEnumerable, _
                                   ByRef p_dataY As IEnumerable, _
                                   Optional ByRef p_axisType As AxisType = AxisType.Primary)

        If InvokeRequired Then
            Dim MyDelegate As New BindData_Delegate(AddressOf BindData_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {p_chartNb, p_serieName, p_dataX, p_dataY, p_axisType})
        Else
            If p_chartNb > m_chartTab.Length Then Exit Sub

            ChartsUtilities.BindSerieToChart(m_chartTab(p_chartNb), p_serieName, p_axisType, p_dataX, p_dataY)

        End If

    End Sub


    ' STUB Formatting for demos
    Delegate Sub StubDemosFormatting_Delegate()
    Friend Sub StubDemosFormatting_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New StubDemosFormatting_Delegate(AddressOf StubDemosFormatting_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            If My.Settings.chart1Serie1AccountId <> 0 Then ChartsUtilities.FormatSerie(Chart1.Series(GlobalVariables.Accounts.GetValueName(My.Settings.chart1Serie1AccountId)), My.Settings.chart1Serie1Color, My.Settings.chart1Serie1Type)
            If My.Settings.chart1Serie2AccountId <> 0 Then ChartsUtilities.FormatSerie(Chart1.Series(GlobalVariables.Accounts.GetValueName(My.Settings.chart1Serie2AccountId)), My.Settings.chart1Serie2Color, My.Settings.chart1Serie2Type)
            If My.Settings.chart2Serie1AccountId <> 0 Then ChartsUtilities.FormatSerie(Chart2.Series(GlobalVariables.Accounts.GetValueName(My.Settings.chart2Serie1AccountId)), My.Settings.chart2Serie1Color, My.Settings.chart2Serie1Type, , 22)
            If My.Settings.chart2Serie2AccountId <> 0 Then ChartsUtilities.FormatSerie(Chart2.Series(GlobalVariables.Accounts.GetValueName(My.Settings.chart2Serie2AccountId)), My.Settings.chart2Serie2Color, My.Settings.chart2Serie2Type)
            If My.Settings.chart3Serie1AccountId <> 0 Then ChartsUtilities.FormatSerie(Chart3.Series(GlobalVariables.Accounts.GetValueName(My.Settings.chart3Serie1AccountId)), My.Settings.chart3Serie1Color, My.Settings.chart3Serie1Type, , 22)
            If My.Settings.chart3Serie2AccountId <> 0 Then ChartsUtilities.FormatSerie(Chart3.Series(GlobalVariables.Accounts.GetValueName(My.Settings.chart3Serie2AccountId)), My.Settings.chart3Serie2Color, My.Settings.chart3Serie2Type, 100)
            If My.Settings.chart4Serie1AccountId <> 0 Then ChartsUtilities.FormatSerie(Chart4.Series(GlobalVariables.Accounts.GetValueName(My.Settings.chart4Serie1AccountId)), My.Settings.chart4Serie1Color, My.Settings.chart4Serie1Type, 100)
            If My.Settings.chart4Serie1AccountId <> 0 Then ChartsUtilities.FormatSerie(Chart4.Series(GlobalVariables.Accounts.GetValueName(My.Settings.chart4Serie1AccountId)), My.Settings.chart4Serie2Color, My.Settings.chart4Serie2Type, 100)
        End If

    End Sub



    Delegate Sub RefreshData_Delegate(ByRef p_serieName As String, _
                                       ByRef p_dataX As IEnumerable, _
                                       ByRef p_dataY As IEnumerable)
    Friend Sub RefreshData_ThreadSafe(ByRef p_serieName As String, _
                                       ByRef p_dataX As IEnumerable, _
                                       ByRef p_dataY As IEnumerable)

        If InvokeRequired Then
            Dim MyDelegate As New RefreshData_Delegate(AddressOf RefreshData_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {p_serieName, p_dataX, p_dataY})
        Else
            For Each chart As Chart In m_chartTab
                If Not chart.Series(p_serieName) Is Nothing Then _
                    chart.Series(p_serieName).Points.DataBindXY(p_dataX, p_dataY)
            Next
        End If

    End Sub

#End Region

#Region "Call Backs"

    Private Sub m_editSerieButton_Click(sender As Object, e As EventArgs)

        ' follow mouse location on chart
        ' on click -> hit test -> return the element

        ' need to have a UI to edit the series
        ' where should we store that ?
        Dim chart As Chart = GetChartFromContextMenu(sender)
        Dim HTR As HitTestResult
        Dim dataPoint As DataPoint
        '   HTR = chart.HitTest(e.x, e.y)
        If HTR.ChartElementType = ChartElementType.DataPoint Then
            Dim serie As Series = HTR.Series
            dataPoint = serie.Points(HTR.PointIndex)
        End If

    End Sub

    Private Sub m_editChartButton_Click(sender As Object, e As EventArgs) Handles m_editChartButton.Click

        Dim chart As Chart = GetChartFromContextMenu(sender)
        Dim chartEditionUI As New CUI2VisualisationChartsSettings(Chart, m_chartIndexes(Chart))
        chartEditionUI.Show()

    End Sub

    Private Sub m_exportOnExcel_Click(sender As Object, e As EventArgs) Handles m_dropChartOnExcelButton.Click

        ' validation message
        Dim chart As Chart = GetChartFromContextMenu(sender)
        ChartsUtilities.ExportChartExcel(chart, GlobalVariables.APPS.ActiveSheet)

    End Sub

    Private Sub m_refreshButton_Click(sender As Object, e As EventArgs) Handles m_refreshButton.Click

        m_controller.LaunchMainUIRefreshFromCharts()

    End Sub


#End Region

#Region "Events"

    ' listen to mouse move over charts and 
    '   - display data pont value
    '   - highligh hovered serie


#End Region


    Private Function GetChartFromContextMenu(ByRef sender As Object) As Chart

        Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
        Return cms.SourceControl

    End Function


  
End Class
