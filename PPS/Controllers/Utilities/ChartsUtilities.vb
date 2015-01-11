Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections

' ChartsUtilities.vb
'
' Utilities for creating Charts and add series
'
'
' Author: Julien Monnereau
' Last modified: 29/12/2014


Friend Class ChartsUtilities


#Region "Instance Variables"

    Friend Const LABELS_MAX_FONT_SIZE As Single = 8
    Friend Const DEFAULT_CHART_PALETTE = ChartColorPalette.Berry
    Friend Const CHART_TITLE_FONT_SIZE As Single = 9

#End Region


#Region "Interface"

    Protected Friend Shared Function CreateChart(ByRef title As String, ByRef palette As Object) As Chart

        Dim new_chart As New Chart
        Dim ChartArea1 As New ChartArea
        new_chart.ChartAreas.Add(ChartArea1)

        ChartArea1.AxisY.LabelAutoFitMaxFontSize = LABELS_MAX_FONT_SIZE
        ChartArea1.AxisX.LabelAutoFitMaxFontSize = LABELS_MAX_FONT_SIZE
        ChartArea1.AxisX.MajorGrid.Enabled = False

        If IsDBNull(palette) Then
            new_chart.Palette = DEFAULT_CHART_PALETTE
        Else
            Select Case palette
                Case "Berry" : new_chart.Palette = ChartColorPalette.Berry
                Case "Bright" : new_chart.Palette = ChartColorPalette.Bright
                Case "BrightPastel" : new_chart.Palette = ChartColorPalette.BrightPastel
                Case "Chocolate" : new_chart.Palette = ChartColorPalette.Chocolate
                Case "EarthTones" : new_chart.Palette = ChartColorPalette.EarthTones
                Case "Excel" : new_chart.Palette = ChartColorPalette.Excel
                Case "Fire" : new_chart.Palette = ChartColorPalette.Fire
                Case "GrayScale" : new_chart.Palette = ChartColorPalette.Grayscale
                Case "Light" : new_chart.Palette = ChartColorPalette.Light
                Case "Pastel" : new_chart.Palette = ChartColorPalette.Pastel
                Case "SeaGreen" : new_chart.Palette = ChartColorPalette.SeaGreen
                Case "SemiTransparent" : new_chart.Palette = ChartColorPalette.SemiTransparent
            End Select
        End If

        Dim legend1 As New Legend
        new_chart.Legends.Add(legend1)
        new_chart.Legends(0).Docking = Docking.Bottom
        new_chart.Legends(0).IsDockedInsideChartArea = False
        new_chart.Legends(0).TableStyle = LegendTableStyle.Wide
        new_chart.Legends(0).Alignment = System.Drawing.StringAlignment.Center


        new_chart.Titles.Add(title)
        new_chart.Titles(0).Font = New Drawing.Font("Arial", CHART_TITLE_FONT_SIZE, Drawing.FontStyle.Bold)
        new_chart.BorderlineWidth = 1
        new_chart.BorderlineColor = Drawing.Color.Gray

        Return new_chart

    End Function

    Protected Friend Shared Sub AddSerieToChart(ByRef chart As Chart, _
                                                ByRef serieHT As Hashtable)

        Dim new_serie As New Series(serieHT(CONTROL_CHART_NAME_VARIABLE))
        chart.Series.Add(new_serie)
        new_serie.ChartArea = "ChartArea1"

        If Not IsDBNull(serieHT(CONTROL_CHART_TYPE_VARIABLE)) Then
            Select Case serieHT(CONTROL_CHART_TYPE_VARIABLE)
                Case "Area" : new_serie.ChartType = SeriesChartType.Area
                Case "Bar" : new_serie.ChartType = SeriesChartType.Bar
                Case "Column" : new_serie.ChartType = SeriesChartType.Column
                Case "Line" : new_serie.ChartType = SeriesChartType.Line
                Case "Spline" : new_serie.ChartType = SeriesChartType.Spline
                Case "StackedColumn" : new_serie.ChartType = SeriesChartType.StackedColumn
                Case "StackedColumn100" : new_serie.ChartType = SeriesChartType.StackedColumn100
            End Select
        End If

        If Not IsDBNull(serieHT(CONTROL_CHART_COLOR_VARIABLE)) Then
            Select Case serieHT(CONTROL_CHART_COLOR_VARIABLE)
                Case "blue" : new_serie.Color = Drawing.Color.Blue
                Case "green" : new_serie.Color = Drawing.Color.Green
                Case "yellow" : new_serie.Color = Drawing.Color.Yellow
                Case "red" : new_serie.Color = Drawing.Color.Red
                Case "purple" : new_serie.Color = Drawing.Color.Purple
                Case "gray" : new_serie.Color = Drawing.Color.Gray
            End Select

        End If

    End Sub


#End Region

End Class
