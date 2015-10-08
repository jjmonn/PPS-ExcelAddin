Imports System.Collections
Imports System.Windows.Forms.DataVisualization.Charting


Public Class CUI2Visualization

#Region "Instance variables"

    Private m_controller As ControllingUIController
    Private m_chartTab As Chart()

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_controller As ControllingUIController)

        InitializeComponent()
        m_controller = p_controller
        m_chartTab = {Chart1, Chart2, Chart3, Chart4}

        Chart1.ContextMenu = m_chartsRightClickMenu
        Chart2.ContextMenu = m_chartsRightClickMenu
        Chart3.ContextMenu = m_chartsRightClickMenu
        Chart4.ContextMenu = m_chartsRightClickMenu

        ' STUB !!
        ChartsUtilities.InitializeChartDisplay(Chart1, "Résultat Opérationnel (m€)")
        ChartsUtilities.InitializeChartDisplay(Chart2, "Investissements (m€)")
        ChartsUtilities.InitializeChartDisplay(Chart3, "Cash Flow (€)")
        ChartsUtilities.InitializeChartDisplay(Chart4, "Ratios Financiers (%)", "%", "", , False, "{0:P0}")

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
            ChartsUtilities.FormatSerie(Chart1.Series("Chiffre d'affaires"), System.Drawing.Color.DeepSkyBlue, SeriesChartType.Column)
            ChartsUtilities.FormatSerie(Chart1.Series("EBIDTA"), System.Drawing.Color.Cyan, SeriesChartType.Column)
            ChartsUtilities.FormatSerie(Chart2.Series("Investissements"), System.Drawing.Color.DarkGray, SeriesChartType.Column, , 22)
            ChartsUtilities.FormatSerie(Chart3.Series("Cash-Flow"), System.Drawing.Color.Red, SeriesChartType.Line)
            ChartsUtilities.FormatSerie(Chart3.Series("Trésorerie"), System.Drawing.Color.LightSlateGray, SeriesChartType.Column, , 22)
            ChartsUtilities.FormatSerie(Chart4.Series("Levier Financier (D/E) %"), System.Drawing.Color.LightYellow, SeriesChartType.Area, 100)
            ChartsUtilities.FormatSerie(Chart4.Series("Rentabilité (ROCE) %"), System.Drawing.Color.LightBlue, SeriesChartType.Area, 100)
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

    Private Sub m_addSerieButton_Click(sender As Object, e As EventArgs) Handles m_addSerieButton.Click

        Dim chart As Chart = sender ' ?
   

    End Sub

    Private Sub m_removeSerieButton_Click(sender As Object, e As EventArgs) Handles m_removeSerieButton.Click

    End Sub

    Private Sub m_editSerieButton_Click(sender As Object, e As EventArgs) Handles m_editSerieButton.Click

        ' follow mouse location on chart
        ' on click -> hit test -> return the element

        ' need to have a UI to edit the series
        ' where should we store that ?
        Dim chart As Chart = sender
        Dim HTR As HitTestResult
        Dim dataPoint As DataPoint
        '   HTR = chart.HitTest(e.x, e.y)
        If HTR.ChartElementType = ChartElementType.DataPoint Then
            Dim serie As Series = HTR.Series
            dataPoint = serie.Points(HTR.PointIndex)
        End If

    End Sub

    Private Sub m_editChartButton_Click(sender As Object, e As EventArgs) Handles m_editChartButton.Click

        ' change font 
        ' change title

    End Sub

    Private Sub m_exportOnExcel_Click(sender As Object, e As EventArgs) Handles m_exportOnExcel.Click

        ' validation message
        ChartsUtilities.ExportChartExcel(sender, GlobalVariables.APPS.ActiveSheet)

    End Sub

#End Region

#Region "Events"

    ' listen to mouse move over charts and 
    '   - display data pont value
    '   - highligh hovered serie


#End Region



End Class
