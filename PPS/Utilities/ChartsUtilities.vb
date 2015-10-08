' ChartsUtilities.vb
'
' Utilities for creating Charts and add series
'
' To do:
'       - Charts export serie color fix !
'
'
' Author: Julien Monnereau
' Last modified: 01/09/2015


Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.Drawing


Friend Class ChartsUtilities


#Region "Instance Variables"

    Friend Const LABELS_MAX_FONT_SIZE As Single = 10
    Friend Const LABELS_MIN_FONT_SIZE As Single = 8
    Friend Const VALUES_LABELS_FONT_SIZE As Single = 8
    Friend Const DEFAULT_CHART_PALETTE = ChartColorPalette.Berry
    Friend Const CHART_TITLE_FONT_SIZE As Single = 12
    Friend Const DEFAULT_LABELS_FORMAT As String = "{0:N0}"
    Friend Const CHARTS_X_AXIS_ANGLE As Int32 = -45



#End Region


#Region "Charts and Series Creation"

    ' Obsolete! (only used in financial engineering -to be reviewed)
    Friend Shared Function CreateChart(ByRef reportHT As Hashtable) As Chart

        Dim new_chart As New Chart
        Dim ChartArea1 As New ChartArea
        new_chart.ChartAreas.Add(ChartArea1)

        ' Axis
        ChartArea1.AxisX.LabelStyle.Angle = CHARTS_X_AXIS_ANGLE
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

        ' Grid Lines
        ChartArea1.AxisX.LabelAutoFitMaxFontSize = LABELS_MAX_FONT_SIZE
        ChartArea1.AxisX.MajorGrid.Enabled = False
        ChartArea1.AxisY.MajorGrid.LineColor = Drawing.Color.LightGray
        ChartArea1.AxisY2.MajorGrid.Enabled = False

        ' Colors Palette
        If reportHT.ContainsKey(REPORTS_PALETTE_VAR) Then SetChartPalette(new_chart, reportHT(REPORTS_PALETTE_VAR))

        ' Legend
        Dim legend1 As New Legend
        new_chart.Legends.Add(legend1)
        new_chart.Legends(0).Docking = Docking.Bottom
        new_chart.Legends(0).IsDockedInsideChartArea = False
        new_chart.Legends(0).TableStyle = LegendTableStyle.Wide
        new_chart.Legends(0).Alignment = System.Drawing.StringAlignment.Center
        ' new_chart.Legends(0).AutoFitMinFontSize = VALUES_LABELS_FONT_SIZE
        new_chart.Legends(0).Font = New Drawing.Font("calibri", 10)

        ' Title
        new_chart.Titles.Add(reportHT(NAME_VARIABLE))
        new_chart.Titles(0).Font = New Drawing.Font("calibri", CHART_TITLE_FONT_SIZE, Drawing.FontStyle.Bold)

        ' Borders
        new_chart.BorderlineWidth = 1
        new_chart.BorderlineColor = Drawing.Color.Gray
        Return new_chart

    End Function

    ' Obsolete! (only used in financial engineering -to be reviewed)
    Friend Shared Sub AddSerieToChart(ByRef chart As Chart, _
                                               ByRef serieHT As Hashtable, _
                                               Optional ByRef chart_area As String = "ChartArea1")

        Dim new_serie As New Series(serieHT(CONTROL_CHART_NAME_VARIABLE))
        chart.Series.Add(new_serie)
        new_serie.ChartArea = chart_area

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


    Friend Shared Sub BindSerieToChart(ByRef p_chart As Chart, _
                                    ByRef p_name As String, _
                                    ByRef p_axisType As AxisType, _
                                    ByRef p_dataX As IEnumerable, _
                                    ByRef p_dataY As IEnumerable, _
                                    Optional ByRef chart_area As String = "ChartArea1")

        Dim new_serie As New Series(p_name)
        p_chart.Series.Add(new_serie)
        new_serie.ChartArea = chart_area
        new_serie.YAxisType = p_axisType
        new_serie.Points.DataBindXY(p_dataX, p_dataY)

    End Sub

    Friend Shared Sub FormatSerie(ByRef p_serie As Series, _
                                   ByRef p_color As System.Drawing.Color, _
                                   ByRef p_chartType As SeriesChartType, _
                                         Optional ByRef p_alphaColor As Integer = -1, _
                                   Optional ByRef p_seriePointsWidth As Single = 0)

        If p_serie Is Nothing Then Exit Sub
        p_serie.Color = p_color
        If p_alphaColor > -1 Then
            p_serie.Color = Color.FromArgb(p_alphaColor, p_color.R, p_color.G, p_color.B)
        End If
        p_serie.ChartType = p_chartType
        If p_seriePointsWidth > 0 Then
            p_serie.CustomProperties = "PixelPointWidth = " & p_seriePointsWidth
        End If

    End Sub

    Friend Shared Sub FormatSerieLabel(ByRef p_serie As Series, _
                                       ByRef p_labelsColor As System.Drawing.Color, _
                                       ByRef p_labelsBackColor As System.Drawing.Color, _
                                       ByRef p_labelsBordersWidth As Single, _
                                       ByRef p_labelsBorderColor As System.Drawing.Color, _
                                       Optional ByRef p_labelsFontSize As Single = VALUES_LABELS_FONT_SIZE)

        If p_serie Is Nothing Then Exit Sub
        p_serie.IsValueShownAsLabel = True
        p_serie.LabelBackColor = p_labelsBackColor
        p_serie.LabelBorderColor = p_labelsColor
        p_serie.LabelBorderWidth = 1
        p_serie.LabelFormat = DEFAULT_LABELS_FORMAT
        Dim label_font As System.Drawing.Font = New System.Drawing.Font("calibri", p_labelsFontSize)
        p_serie.Font = label_font

    End Sub

    Friend Shared Sub InitializeChartDisplay(ByRef p_chart As Chart, _
                                          Optional ByRef p_title As String = "", _
                                          Optional ByRef p_yAxisName As String = "", _
                                          Optional ByRef p_yAxis2Name As String = "",
                                          Optional ByRef p_colorPalette As Integer = -1, _
                                          Optional ByRef p_border As Boolean = False, _
                                          Optional ByRef p_YAxisFormat As String = DEFAULT_LABELS_FORMAT)

        Dim chartArea1 As ChartArea = p_chart.ChartAreas("ChartArea1")
        chartArea1.IsSameFontSizeForAllAxes = True

        ' Axis
        chartArea1.AxisY.TitleFont = New Drawing.Font("calibri", VALUES_LABELS_FONT_SIZE)
        chartArea1.AxisX.TitleFont = New Drawing.Font("calibri", VALUES_LABELS_FONT_SIZE)
        chartArea1.AxisX.LabelStyle.Angle = CHARTS_X_AXIS_ANGLE
        chartArea1.AxisY.LabelAutoFitMaxFontSize = LABELS_MAX_FONT_SIZE
        chartArea1.AxisY.LabelAutoFitMinFontSize = LABELS_MIN_FONT_SIZE
        chartArea1.AxisX.LabelAutoFitMaxFontSize = LABELS_MAX_FONT_SIZE
        chartArea1.AxisX.LabelAutoFitMinFontSize = LABELS_MIN_FONT_SIZE

        If p_yAxisName <> "" Then
            chartArea1.AxisY.Title = p_yAxisName
            chartArea1.AxisY.TextOrientation = TextOrientation.Rotated90
        End If
        chartArea1.AxisY.LabelStyle.Format = p_YAxisFormat

        If p_yAxis2Name <> "" Then
            chartArea1.AxisY2.Title = p_yAxis2Name
            chartArea1.AxisY2.TextOrientation = TextOrientation.Rotated90
        End If

        ' Grid Lines
        chartArea1.AxisX.MajorGrid.Enabled = False
        chartArea1.AxisY.MajorGrid.LineColor = Drawing.Color.LightGray
        chartArea1.AxisY2.MajorGrid.Enabled = False

        ' Colors Palette
        If p_colorPalette > -1 Then
            p_chart.Palette = p_colorPalette
        End If

        ' Legend
        Dim legend1 As New Legend
        p_chart.Legends.Add(legend1)
        p_chart.Legends(0).Docking = Docking.Bottom
        p_chart.Legends(0).IsDockedInsideChartArea = False
        p_chart.Legends(0).TableStyle = LegendTableStyle.Auto
        p_chart.Legends(0).Alignment = System.Drawing.StringAlignment.Center
        p_chart.Legends(0).Font = New Drawing.Font("calibri", VALUES_LABELS_FONT_SIZE)
        p_chart.Legends(0).AutoFitMinFontSize = LABELS_MIN_FONT_SIZE
        p_chart.Legends(0).MaximumAutoSize = LABELS_MAX_FONT_SIZE
        p_chart.Legends(0).Font = New Drawing.Font("calibri", 10)

        ' Title
        If p_title <> "" Then
            p_chart.Titles.Add(p_title)
            p_chart.Titles(0).Font = New Drawing.Font("calibri", CHART_TITLE_FONT_SIZE, Drawing.FontStyle.Bold)
        End If

        ' Borders
        If p_border = True Then
            p_chart.BorderlineWidth = 1
            p_chart.BorderlineColor = Drawing.Color.Gray
        End If

    End Sub

    Friend Shared Sub EqualizeChartsYAxis1(ByRef chart1 As Chart, _
                                           ByRef chart2 As Chart)

        If chart1.ChartAreas(0).AxisY.Maximum > chart2.ChartAreas(0).AxisY.Maximum Then
            chart2.ChartAreas(0).AxisY.Maximum = chart1.ChartAreas(0).AxisY.Maximum
        Else
            chart1.ChartAreas(0).AxisY.Maximum = chart2.ChartAreas(0).AxisY.Maximum
        End If

    End Sub


#End Region


#Region "Position Adjustment"

    Friend Shared Sub AdjustChartPosition(ByRef chart As Chart)

        chart.ChartAreas(0).Position = New ElementPosition(0, 10, 100, 80)
        chart.Legends(0).Position = New ElementPosition(0, 90, 100, 10)

    End Sub

#End Region


#Region "Excel Export"

    ' drop values as option ? priority normal
    Friend Shared Sub ExportChartExcel(ByRef input_chart As Chart, _
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
            Dim c As System.Drawing.Color = System.Drawing.Color.FromArgb(serie.Color.ToArgb())
            '  Dim rgb_color As System.Drawing.Color = Excel.ColorFormat
            series1.Format.Fill.forecolor.rgb = RGB(c.R, c.G, c.B) 'System.Drawing.Color.FromArgb(serie.Color.ToArgb())

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

    Friend Shared Sub SetChartPalette(ByRef chart As Chart, _
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

    Friend Shared Sub SetY1AxisMaxValue(ByRef chart As Chart, _
                                                 ByRef max_y As Double)

        chart.ChartAreas(0).AxisY.Maximum = max_y

    End Sub

    Friend Shared Sub SetY2AxisMaxValue(ByRef chart As Chart, _
                                                 ByRef max_y As Double)

        chart.ChartAreas(0).AxisY2.Maximum = max_y

    End Sub

    Friend Sub DuplicateChart()

        Dim myStream As System.IO.MemoryStream = New System.IO.MemoryStream()
        'Outputchart.Serializer.Save(myStream)
        'ExportedChart.Serializer.Load(myStream)

    End Sub

#End Region


End Class
