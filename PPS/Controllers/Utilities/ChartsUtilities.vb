Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices

' ChartsUtilities.vb
'
' Utilities for creating Charts and add series
'
'
' Author: Julien Monnereau
' Last modified: 25/01/2015


Friend Class ChartsUtilities


#Region "Instance Variables"

    Friend Const LABELS_MAX_FONT_SIZE As Single = 8
    Friend Const VALUES_LABELS_FONT_SIZE As Single = 7
    Friend Const DEFAULT_CHART_PALETTE = ChartColorPalette.Berry
    Friend Const CHART_TITLE_FONT_SIZE As Single = 9
    Friend Const DEFAULT_LABELS_FORMAT As String = "{0:N0}"

#End Region


#Region "Charts and Series Creation"

    Protected Friend Shared Function CreateChart(ByRef title As String, _
                                                 Optional ByRef palette As Object = Nothing) As Chart

        Dim new_chart As New Chart
        Dim ChartArea1 As New ChartArea
        new_chart.ChartAreas.Add(ChartArea1)

        ChartArea1.AxisY.LabelAutoFitMaxFontSize = LABELS_MAX_FONT_SIZE
        ChartArea1.AxisX.LabelAutoFitMaxFontSize = LABELS_MAX_FONT_SIZE
        ' set legend min font size
        ChartArea1.AxisX.MajorGrid.Enabled = False

        If IsDBNull(palette) Then
            new_chart.Palette = DEFAULT_CHART_PALETTE
        Else
            Select Case palette
                ' use rgb
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
        new_chart.Legends(0).AutoFitMinFontSize = VALUES_LABELS_FONT_SIZE

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
                Case "black" : new_serie.Color = Drawing.Color.Black
                Case "Khaki" : new_serie.Color = Drawing.Color.Khaki
                Case "DarkKhaki" : new_serie.Color = Drawing.Color.DarkKhaki
            End Select
        End If

        If serieHT.ContainsKey(GDF_AS_REPORTS_DISPLAY_VALUES_VAR) AndAlso _
        Not IsDBNull(serieHT(GDF_AS_REPORTS_DISPLAY_VALUES_VAR)) Then
            If serieHT(GDF_AS_REPORTS_DISPLAY_VALUES_VAR) = 1 Then
                new_serie.IsValueShownAsLabel = True
                new_serie.LabelBackColor = Drawing.Color.White
                new_serie.LabelBorderColor = new_serie.Color
                new_serie.LabelBorderWidth = 1
                new_serie.LabelFormat = DEFAULT_LABELS_FORMAT
                Dim label_font As System.Drawing.Font = New System.Drawing.Font(new_serie.Font.FontFamily, VALUES_LABELS_FONT_SIZE, Drawing.FontStyle.Regular)
                new_serie.Font = label_font
            End If
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

        ' specific to PriceModeling
        ' to be reviewed
        chart.ChartAreas(0).Position = New ElementPosition(10, 10, 80, 80)
        chart.Legends(0).Position = New ElementPosition(0, 90, 100, 10)

    End Sub

#End Region

#Region "Excel"

    Protected Friend Shared Sub CopyChartToExcel(ByRef input_chart As Chart)

        ' Dim dataSourceCells As Excel.Range = Nothing
        Dim chartObjects As Excel.ChartObjects = APPS.ActiveSheet.ChartObjects
        Dim chartObject As Excel.ChartObject = Nothing
        Dim newChart As Excel.Chart = Nothing

        '  dataSourceCells = activeSheet.Range["A1", "G6"] 
        chartObject = chartObjects.Add(100, 100, 350, 300)  ' (Excel.ChartObject)
        newChart = chartObject.Chart
        ' copy series and settings one by one ?

        newChart.SetSourceData(input_chart.DataSource)    ' dataSourceCells
        newChart.ChartType = Excel.XlChartType.xlLineMarkers

        If Not chartObjects Is Nothing Then Marshal.ReleaseComObject(chartObjects)
        If Not chartObject Is Nothing Then Marshal.ReleaseComObject(chartObject)
        If Not newChart Is Nothing Then Marshal.ReleaseComObject(newChart)


    End Sub



#End Region

End Class
