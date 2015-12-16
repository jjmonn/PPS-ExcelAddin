Imports System.IO
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Windows.Forms.DataVisualization.Charting



Friend Class HTTP_test

    ' Private entities_tv As New TreeView
    ' Private DBDOWNLOADER As New DataBaseDataDownloader
    ' Private CCOMPUTERINT As New DLL3_Interface


    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        TextBox1.Text = "http://localhost:3000/testhttp/EBITDA.json" '(xml)


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Dim URI As String = TextBox1.Text
        'Dim httpRequest As New WinHttp.WinHttpRequest
        'httpRequest.Open("get", URI)
        'httpRequest.Send()

        '' responseTB.Text = httpRequest.ResponseStream
        '' responseTB.Text = "body: " & httpRequest.ResponseBody
        'MsgBox(httpRequest.ResponseText)

        'Dim JSONString As String = "[{'id':'1','account_name':'a'},{'id':'2','account_name':'b'}]"

        'Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(httpRequest.ResponseText)

        'Dim account_name1 = j(0)("account_name")
        'For i = 1 To j.length - 1
        '    GlobalVariables.apps.ActiveSheet.cells(i, 1).value = j(i)("account_name")

        'Next

        'j("status")

    End Sub


    Private Sub build_years_months_array(years_months_dict As Dictionary(Of Int32, Int32()), _
                                         periods_array() As Int32)

        'For Each period_ As Integer In periods_array
        '    Dim currentYear As Int32 = Year(DateTime.FromOADate(period_))
        '    Dim months_list As List(Of Int32) = Period.GetMonthsPeriodsInOneYear(currentYear, False)
        '    years_months_dict.Add(period_, months_list.ToArray)
        'Next

    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        'Dim entities As String() = {"a", "b", "c"}
        'Dim formulas As String() = {"NBP*0.5", "brent*1.2", "TTF"}
        'Dim nb_entities As Int32 = 3
        'Dim nb_periods As Int32 = 4

        'Dim nbp_prices As Double() = {10, 12, 15, 11}
        'Dim brent_prices As Double() = {82, 83, 82, 85}
        'Dim TTF_prices As Double() = {5, 5.2, 5.1, 5.3}
        'Dim index_list As String() = {"NBP", "brent", "TTF"}

        'Dim volumes As Double() = {1, 1.1, 1.2, 1.2, 2, 2, 2, 2, 4, 3, 2, 1}
        'Dim base_revenues As Double() = {100, 101, 102, 103, 2000, 2000, 2000, 2000, 400, 300, 200, 100}
        'Dim tax_rates As Double() = {0.3, 0.3, 0.3, 0.3, 0.25, 0.25, 0.25, 0.25, 0.5, 0.5, 0.5, 0.5}

        'Dim PSDLL As New PSDLLL_Interface(index_list, entities, formulas, nb_periods)
        'PSDLL.ResgisterIndexMarketPrices(nbp_prices, "NBP")
        'PSDLL.ResgisterIndexMarketPrices(brent_prices, "brent")
        'PSDLL.ResgisterIndexMarketPrices(TTF_prices, "TTF")
        'PSDLL.Compute(volumes, base_revenues, tax_rates)

        'Dim results = PSDLL.GetResultsDict()
        
    End Sub



    Private Sub DrawChart(ByRef data_dic)

        Dim x As Integer() = {2013, 2014, 2015, 2016, 2017}
        Dim chart1 As New Chart
        Dim ChartArea1 As New ChartArea
        Dim ChartArea2 As New ChartArea
        chart1.ChartAreas.Add(ChartArea1)
        chart1.ChartAreas.Add(ChartArea2)

        ChartArea1.AxisY.LabelAutoFitMaxFontSize = 8
        ChartArea2.AxisY.LabelAutoFitMaxFontSize = 8
        ChartArea1.AxisX.LabelAutoFitMaxFontSize = 8
        ChartArea2.AxisX.LabelAutoFitMaxFontSize = 8

        chart1.Palette = ChartColorPalette.Berry
        ChartArea1.AxisX.MajorGrid.Enabled = False

        Dim legend1 As New Legend
        chart1.Legends.Add(legend1)

        Dim dividends_serie As New Series("Dividends")
        Dim cash_serie As New Series("Cash")
        Dim payout As New Series("Payout")
        Dim doe As New Series("D/E")

        chart1.Series.Add(dividends_serie)
        chart1.Series.Add(cash_serie)
        chart1.Series.Add(payout)
        chart1.Series.Add(doe)

        dividends_serie.ChartArea = "ChartArea1"
        cash_serie.ChartArea = "ChartArea1"
        payout.ChartArea = "ChartArea2"
        doe.ChartArea = "ChartArea2"

        chart1.Series("Dividends").Points.DataBindXY(x, data_dic("div"))
        chart1.Series("Cash").Points.DataBindXY(x, data_dic("cash"))
        chart1.Series("Payout").Points.DataBindXY(x, data_dic("payout"))
        chart1.Series("D/E").Points.DataBindXY(x, data_dic("doe"))

        For Each serie In chart1.Series
            serie.CustomProperties = "DrawingStyle=cylinder"
        Next

        Panel1.Controls.Add(chart1)
        chart1.Dock = DockStyle.Fill

        legend1.Position.X = 88
        legend1.Position.Y = 0
        legend1.Position.Width = 15

        ChartArea1.Position.X = 0
        ChartArea1.Position.Y = 0
        ChartArea1.Position.Width = 43
        ChartArea1.Position.Height = 100

        ChartArea2.Position.X = 43
        ChartArea2.Position.Y = 0
        ChartArea2.Position.Width = 43
        ChartArea2.Position.Height = 100

    End Sub



End Class