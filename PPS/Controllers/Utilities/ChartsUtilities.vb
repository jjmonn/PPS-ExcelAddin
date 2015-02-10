' ChartsUtilities.vb
'
' Utilities for creating Charts and add series
'
' To do:
'       - Charts export serie color fix !
'
'
' Author: Julien Monnereau
' Last modified: 08/02/2015


Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices


Friend Class ChartsUtilities


#Region "Instance Variables"

    Friend Const LABELS_MAX_FONT_SIZE As Single = 8
    Friend Const VALUES_LABELS_FONT_SIZE As Single = 7
    Friend Const DEFAULT_CHART_PALETTE = ChartColorPalette.Berry
    Friend Const CHART_TITLE_FONT_SIZE As Single = 9
    Friend Const DEFAULT_LABELS_FORMAT As String = "{0:N0}"

#End Region


#Region "Charts and Series Creation"

    Protected Friend Shared Function CreateChart(ByRef reportHT As Hashtable) As Chart

        Dim new_chart As New Chart
        Dim ChartArea1 As New ChartArea
        new_chart.ChartAreas.Add(ChartArea1)

        ChartArea1.AxisY.LabelAutoFitMaxFontSize = LABELS_MAX_FONT_SIZE
        If reportHT.ContainsKey(REPORTS_AXIS1_NAME_VAR) _
        AndAlso Not IsDBNull(reportHT(REPORTS_AXIS1_NAME_VAR)) Then
            ChartArea1.AxisY.Title = reportHT(REPORTS_AXIS1_NAME_VAR)
            ChartArea1.AxisY.TextOrientation = TextOrientation.Rotated90
        End If
        If reportHT.ContainsKey(REPORTS_AXIS1_NAME_VAR) _
        AndAlso Not IsDBNull(reportHT(REPORTS_AXIS2_NAME_VAR)) Then
            ChartArea1.AxisY2.Title = reportHT(REPORTS_AXIS2_NAME_VAR)
            ChartArea1.AxisY2.TextOrientation = TextOrientation.Rotated90
        End If
        ChartArea1.AxisX.LabelAutoFitMaxFontSize = LABELS_MAX_FONT_SIZE
        ChartArea1.AxisX.MajorGrid.Enabled = False

        If reportHT.ContainsKey(REPORTS_PALETTE_VAR) Then SetChartPalette(new_chart, reportHT(REPORTS_PALETTE_VAR))

        Dim legend1 As New Legend
        new_chart.Legends.Add(legend1)
        new_chart.Legends(0).Docking = Docking.Bottom
        new_chart.Legends(0).IsDockedInsideChartArea = False
        new_chart.Legends(0).TableStyle = LegendTableStyle.Wide
        new_chart.Legends(0).Alignment = System.Drawing.StringAlignment.Center
        new_chart.Legends(0).AutoFitMinFontSize = VALUES_LABELS_FONT_SIZE

        new_chart.Titles.Add(reportHT(REPORTS_NAME_VAR))
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

        If serieHT.ContainsKey(CONTROL_CHART_TYPE_VARIABLE) AndAlso _
        Not IsDBNull(serieHT(CONTROL_CHART_TYPE_VARIABLE)) Then
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

        If serieHT.ContainsKey(CONTROL_CHART_COLOR_VARIABLE) AndAlso _
        Not IsDBNull(serieHT(CONTROL_CHART_COLOR_VARIABLE)) Then
            new_serie.Color = System.Drawing.Color.FromArgb(serieHT(CONTROL_CHART_COLOR_VARIABLE))
        End If

        If serieHT.ContainsKey(REPORTS_DISPLAY_VALUES_VAR) AndAlso _
        Not IsDBNull(serieHT(REPORTS_DISPLAY_VALUES_VAR)) Then
            If serieHT(REPORTS_DISPLAY_VALUES_VAR) = 1 Then
                new_serie.IsValueShownAsLabel = True
                new_serie.LabelBackColor = Drawing.Color.White
                new_serie.LabelBorderColor = new_serie.Color
                new_serie.LabelBorderWidth = 1
                new_serie.LabelFormat = DEFAULT_LABELS_FORMAT
                Dim label_font As System.Drawing.Font = New System.Drawing.Font(new_serie.Font.FontFamily, VALUES_LABELS_FONT_SIZE, Drawing.FontStyle.Regular)
                new_serie.Font = label_font
            End If
        End If

        If serieHT.ContainsKey(REPORTS_SERIE_WIDTH_VAR) AndAlso _
        Not IsDBNull(serieHT(REPORTS_SERIE_WIDTH_VAR)) Then
            new_serie.CustomProperties = "PixelPointWidth = " & serieHT(REPORTS_SERIE_WIDTH_VAR)
        End If

        If serieHT.ContainsKey(REPORTS_SERIE_AXIS_VAR) AndAlso _
        Not IsDBNull(serieHT(REPORTS_SERIE_AXIS_VAR)) Then
            Select Case serieHT(REPORTS_SERIE_AXIS_VAR)
                Case "Primary" : new_serie.YAxisType = AxisType.Primary
                Case "Secondary" : new_serie.YAxisType = AxisType.Secondary
            End Select
        End If

    End Sub

    Protected Friend Shared Sub EqualizeChartsYAxis1(ByRef chart1 As Chart, _
                                                     ByRef chart2 As Chart)


        If chart1.ChartAreas(0).AxisY.Maximum > chart2.ChartAreas(0).AxisY.Maximum Then
            chart2.ChartAreas(0).AxisY.Maximum = chart1.ChartAreas(0).AxisY.Maximum
        Else
            chart1.ChartAreas(0).AxisY.Maximum = chart2.ChartAreas(0).AxisY.Maximum
        End If

    End Sub

    
#End Region


#Region "Position Adjustment"

    Protected Friend Shared Sub AdjustChartPosition(ByRef chart As Chart)

        chart.ChartAreas(0).Position = New ElementPosition(10, 10, 80, 80)
        chart.Legends(0).Position = New ElementPosition(0, 90, 100, 10)

    End Sub

#End Region


#Region "Excel Export"

    Protected Friend Shared Sub ExportChartExcel(ByRef input_chart As Chart, _
                                                 ByRef ws As Excel.Worksheet, _
                                                 Optional ByRef i As Int32 = 2, _
                                                 Optional ByRef j As Int32 = 2)

        Dim chartObjs As Excel.ChartObjects = ws.ChartObjects(Type.Missing)     ' (ChartObjects) -> cast
        Dim chartObj As Excel.ChartObject = chartObjs.Add(100, 20, 300, 300)
        Dim xlChart As Excel.Chart = chartObj.Chart
        xlChart.HasTitle = True
        xlChart.ChartTitle.Caption = input_chart.Titles(0).Text
        xlChart.ChartTitle.Font.Size = input_chart.Titles(0).Font.Size
        xlChart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionBottom

        For Each serie As Series In input_chart.Series
            j = 2
            ws.Cells(i, 1).value = serie.Name
            For Each point As DataPoint In serie.Points
                ws.Cells(i, j).value = point.YValues(0)
                ws.Cells(1, j).value = point.XValue
                j = j + 1
            Next
            Dim xValues As Excel.Range = ws.Range(ws.Cells(1, 2), ws.Cells(1, j - 1))
            Dim values As Excel.Range = ws.Range(ws.Cells(i, 2), ws.Cells(i, j - 1))
            Dim SeriesCollection As Excel.SeriesCollection = xlChart.SeriesCollection()
            Dim series1 As Excel.Series = SeriesCollection.NewSeries()
            series1.XValues = xValues
            series1.Values = values
            series1.Name = serie.Name
            series1.format.fill.visible = True
            series1.format.fill.Solid()
            series1.format.fill.Transparency = 0
            series1.Format.Fill.ForeColor.rgb = System.Drawing.Color.Red.ToArgb.ToString
            ' serie.Color.ToArgb

            Select Case serie.ChartType
                Case SeriesChartType.Area : series1.ChartType = Excel.XlChartType.xlArea
                Case SeriesChartType.Bar : series1.ChartType = Excel.XlChartType.xlBarClustered
                Case SeriesChartType.Column : series1.ChartType = Excel.XlChartType.xlColumnClustered
                Case SeriesChartType.Line : series1.ChartType = Excel.XlChartType.xlLine
                Case SeriesChartType.Spline : series1.ChartType = Excel.XlChartType.xlLine
                Case SeriesChartType.StackedColumn : series1.ChartType = Excel.XlChartType.xlColumnStacked
                Case SeriesChartType.StackedColumn100 : series1.ChartType = Excel.XlChartType.xlColumnStacked100
            End Select
            i = i + 1
        Next

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub SetChartPalette(ByRef chart As Chart, _
                                                      Optional ByRef palette As Object = Nothing)

        If IsDBNull(palette) Or palette Is Nothing Then
            chart.Palette = DEFAULT_CHART_PALETTE
        Else
            Select Case palette
                Case "Berry" : chart.Palette = ChartColorPalette.Berry
                Case "Bright" : chart.Palette = ChartColorPalette.Bright
                Case "BrightPastel" : chart.Palette = ChartColorPalette.BrightPastel
                Case "Chocolate" : chart.Palette = ChartColorPalette.Chocolate
                Case "EarthTones" : chart.Palette = ChartColorPalette.EarthTones
                Case "Excel" : chart.Palette = ChartColorPalette.Excel
                Case "Fire" : chart.Palette = ChartColorPalette.Fire
                Case "GrayScale" : chart.Palette = ChartColorPalette.Grayscale
                Case "Light" : chart.Palette = ChartColorPalette.Light
                Case "Pastel" : chart.Palette = ChartColorPalette.Pastel
                Case "SeaGreen" : chart.Palette = ChartColorPalette.SeaGreen
                Case "SemiTransparent" : chart.Palette = ChartColorPalette.SemiTransparent
            End Select
        End If

    End Sub

#End Region


End Class
