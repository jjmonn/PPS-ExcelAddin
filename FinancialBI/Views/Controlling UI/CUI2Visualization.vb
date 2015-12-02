Imports System.Collections
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Drawing


Public Class CUI2Visualization

#Region "Instance variables"

    Private m_controller As ControllingUIController
    Private m_chartTab As Chart()
    Private m_chartIndexes As New SafeDictionary(Of Chart, Int32)

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

    Delegate Sub FormatSerie_Delegate(ByRef p_chartIndex As UInt32, _
                                      ByRef p_serieColor As Color, _
                                      ByRef p_serieType As Int32, _
                                      ByRef p_serieName As String, _
                                      ByRef p_alpha As Single)
    Friend Sub FormatSerie_ThreadSafe(ByRef p_chartIndex As UInt32, _
                                      ByRef p_serieColor As Color, _
                                      ByRef p_serieType As Int32, _
                                      ByRef p_serieName As String, _
                                      ByRef p_alpha As Single)

        If InvokeRequired Then
            Dim MyDelegate As New FormatSerie_Delegate(AddressOf FormatSerie_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {p_chartIndex, p_serieColor, p_serieType, p_serieName, p_alpha})
        Else
            ChartsUtilities.FormatSerie(m_chartTab(p_chartIndex).Series(p_serieName), p_serieColor, p_serieType, p_alpha)
        End If

    End Sub


    'Delegate Sub FormatCharts_Delegate(ByRef p_alpha As Single)
    'Friend Sub FormatCharts_ThreadSafe(ByRef p_alpha As Single)

    '    If InvokeRequired Then
    '        Dim MyDelegate As New FormatCharts_Delegate(AddressOf FormatCharts_ThreadSafe)
    '        Me.Invoke(MyDelegate, New Object() {p_alpha})
    '    Else
    '        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(My.Settings.chart1Serie1AccountId) <> 0 Then ChartsUtilities.FormatSerie(Chart1.Series(GlobalVariables.Accounts.m_accountsHash(My.Settings.chart1Serie1AccountId)(NAME_VARIABLE)), My.Settings.chart1Serie1Color, My.Settings.chart1Serie1Type, p_alpha)
    '        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(My.Settings.chart1Serie2AccountId) <> 0 Then ChartsUtilities.FormatSerie(Chart1.Series(GlobalVariables.Accounts.m_accountsHash(My.Settings.chart1Serie2AccountId)(NAME_VARIABLE)), My.Settings.chart1Serie2Color, My.Settings.chart1Serie2Type, p_alpha)
    '        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(My.Settings.chart2Serie1AccountId) <> 0 Then ChartsUtilities.FormatSerie(Chart2.Series(GlobalVariables.Accounts.m_accountsHash(My.Settings.chart2Serie1AccountId)(NAME_VARIABLE)), My.Settings.chart2Serie1Color, My.Settings.chart2Serie1Type, p_alpha, 22)
    '        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(My.Settings.chart2Serie2AccountId) <> 0 Then ChartsUtilities.FormatSerie(Chart2.Series(GlobalVariables.Accounts.m_accountsHash(My.Settings.chart2Serie2AccountId)(NAME_VARIABLE)), My.Settings.chart2Serie2Color, My.Settings.chart2Serie2Type, p_alpha)
    '        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(My.Settings.chart3Serie1AccountId) <> 0 Then ChartsUtilities.FormatSerie(Chart3.Series(GlobalVariables.Accounts.m_accountsHash(My.Settings.chart3Serie1AccountId)(NAME_VARIABLE)), My.Settings.chart3Serie1Color, My.Settings.chart3Serie1Type, p_alpha, 22)
    '        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(My.Settings.chart3Serie2AccountId) <> 0 Then ChartsUtilities.FormatSerie(Chart3.Series(GlobalVariables.Accounts.m_accountsHash(My.Settings.chart3Serie2AccountId)(NAME_VARIABLE)), My.Settings.chart3Serie2Color, My.Settings.chart3Serie2Type, p_alpha)
    '        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(My.Settings.chart4Serie1AccountId) <> 0 Then ChartsUtilities.FormatSerie(Chart4.Series(GlobalVariables.Accounts.m_accountsHash(My.Settings.chart4Serie1AccountId)(NAME_VARIABLE)), My.Settings.chart4Serie1Color, My.Settings.chart4Serie1Type, p_alpha)
    '        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(My.Settings.chart4Serie1AccountId) <> 0 Then ChartsUtilities.FormatSerie(Chart4.Series(GlobalVariables.Accounts.m_accountsHash(My.Settings.chart4Serie1AccountId)(NAME_VARIABLE)), My.Settings.chart4Serie2Color, My.Settings.chart4Serie2Type, p_alpha)
    '    End If

    'End Sub

    'Delegate Sub RefreshData_Delegate(ByRef p_serieName As String, _
    '                                   ByRef p_dataX As IEnumerable, _
    '                                   ByRef p_dataY As IEnumerable)
    'Friend Sub RefreshData_ThreadSafe(ByRef p_serieName As String, _
    '                                   ByRef p_dataX As IEnumerable, _
    '                                   ByRef p_dataY As IEnumerable)

    '    If InvokeRequired Then
    '        Dim MyDelegate As New RefreshData_Delegate(AddressOf RefreshData_ThreadSafe)
    '        Me.Invoke(MyDelegate, New Object() {p_serieName, p_dataX, p_dataY})
    '    Else
    '        For Each chart As Chart In m_chartTab
    '            If Not chart.Series(p_serieName) Is Nothing Then _
    '                chart.Series(p_serieName).Points.DataBindXY(p_dataX, p_dataY)
    '        Next
    '    End If

    'End Sub

#End Region

#Region "Call Backs"

    Private Sub m_editSerieButton_Click(sender As Object, e As EventArgs)

        ' follow mouse location on chart
        ' on click -> hit test -> return the element

        ' need to have a UI to edit the series
        ' where should we store that ?
        Dim chart As Chart = GetChartFromContextMenu(sender)
        Dim HTR As New HitTestResult
        Dim dataPoint As DataPoint
        '   HTR = chart.HitTest(e.x, e.y)
        If HTR.ChartElementType = ChartElementType.DataPoint Then
            Dim serie As Series = HTR.Series
            dataPoint = serie.Points(HTR.PointIndex)
        End If

    End Sub

    Private Sub m_editChartButton_Click(sender As Object, e As EventArgs) Handles m_editChartButton.Click

        Dim chart As Chart = GetChartFromContextMenu(sender)
        Dim chartEditionUI As New CUI2VisualisationChartsSettings(chart, m_chartIndexes(chart))
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
