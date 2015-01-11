Imports System.Web.Script.Serialization
Imports System.IO
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Windows.Forms.DataVisualization.Charting



Public Class HTTP_test

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

        Dim URI As String = TextBox1.Text
        Dim httpRequest As New WinHttp.WinHttpRequest
        httpRequest.Open("get", URI)
        httpRequest.Send()

        ' responseTB.Text = httpRequest.ResponseStream
        ' responseTB.Text = "body: " & httpRequest.ResponseBody
        MsgBox(httpRequest.ResponseText)

        Dim JSONString As String = "[{'id':'1','account_name':'a'},{'id':'2','account_name':'b'}]"

        Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(httpRequest.ResponseText)

        Dim account_name1 = j(0)("account_name")
        For i = 1 To j.length - 1
            APPS.ActiveSheet.cells(i, 1).value = j(i)("account_name")

        Next

        'j("status")

    End Sub


    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    '    Dim t0 = DateTime.UtcNow

    '    Dim VERSION_TIME_SETUP As String = YEARLY_TIME_CONFIGURATION
    '    Dim periods_list As New List(Of Integer)
    '    Dim periods_array() As Integer = {41639, 42004, 42369, 42735, 43100, 43465, _
    '                                      43830, 44196, 44561}
    '    For Each period In periods_array
    '        periods_list.Add(period)
    '    Next
    '    EntitiesModel.LoadAssetTree(entities_tv)

    '    ' init ent aggregator
    '    Dim entities_id_list = CCOMPUTERINT.InitializeEntitiesAggregation(periods_list,
    '                                                                      entities_tv.Nodes.Find(".10", True)(0),
    '                                                                      MAIN_CURRENCY)
    '    Dim t1 = DateTime.UtcNow
    '    ' Init convertor periods and convertor periods inject rates
    '    Dim years_months_dict As New Dictionary(Of Int32, Int32())
    '    build_years_months_array(years_months_dict, periods_array)
    '    For Each year_period In years_months_dict.Keys
    '        CCOMPUTERINT.InitDllCurrencyConvertorPeriods(years_months_dict(year_period))
    '    Next

    '    Dim currency_token As String = "USD/EUR"
    '    Dim ratesList As New List(Of Double)
    '    Dim ratesPeriodsList As New List(Of Int32)
    '    EXCHMAPP.FillRatesLists(currency_token, "FIR", ratesPeriodsList, ratesList)

    '    CCOMPUTERINT.AddCurrenciesRatesToConvertor(currency_token, ratesPeriodsList.ToArray(), ratesList.ToArray(), ratesList.Count)

    '    Dim t2 = DateTime.UtcNow

    '    ' compute inputs
    '    Dim inputs_entities_list = cTreeViews_Functions.GetNoChildrenNodesList(entities_id_list, entities_tv)

    '    If DBDOWNLOADER.BuildDataRSTForEntityLoop(inputs_entities_list.ToArray, "VOCNU") Then
    '        For Each entity_id In inputs_entities_list
    '            If DBDOWNLOADER.FilterOnEntityID(entity_id) Then
    '                CCOMPUTERINT.ComputeInputEntity(entity_id, _
    '                                                DBDOWNLOADER.AccKeysArray, _
    '                                                DBDOWNLOADER.PeriodArray, _
    '                                                DBDOWNLOADER.ValuesArray)
    '            End If
    '        Next
    '        DBDOWNLOADER.CloseRST()
    '    End If

    '    'If DBDOWNLOADER.build_data_hash(inputs_entities_list.ToArray, "VOCNU") Then

    '    '    For Each entity_id In DBDOWNLOADER.stored_inputs_list
    '    '        CCOMPUTERINT.ComputeInputEntity(entity_id, _
    '    '                                        DBDOWNLOADER.accounts_ID_hash(entity_id), _
    '    '                                        DBDOWNLOADER.periods_ID_hash(entity_id), _
    '    '                                        DBDOWNLOADER.values_ID_hash(entity_id))
    '    '    Next
    '    '    DBDOWNLOADER.ClearDatasDictionaries()
    '    'End If
    '    Dim t3 = DateTime.UtcNow
    '    Dim elapsed = t1 - t0
    '    ' MsgBox(elapsed.Milliseconds)

    '    Dim data_dictionary As Dictionary(Of String, Double()) = CCOMPUTERINT.GetOutputMatrix()
    '    Dim t4 = DateTime.UtcNow

    '    Dim ellapsed_init = t1 - t0
    '    Dim ellapsed_currencies = t2 - t1
    '    Dim ellapsed_inputs = t3 - t2
    '    Dim ellapsed_parents = t4 - t1

    '    MsgBox("initialization: " & ellapsed_init.Milliseconds & Chr(13) & _
    '           "init currency convertor: " & ellapsed_currencies.Milliseconds & Chr(13) & _
    '           "inputs computation: " & ellapsed_inputs.Milliseconds & Chr(13) & _
    '            "parents computation: " & ellapsed_parents.Milliseconds & Chr(13))


    '    Dim entities_keys As New List(Of String)
    '    For Each key In data_dictionary.Keys
    '        entities_keys.Add(key)
    '    Next
    '    Dim cube_form As New CubeView(periods_list, entities_keys, CCOMPUTERINT.accounts_array)
    '    cube_form.fill_in(data_dictionary)
    '    cube_form.Show()
    '    '    CCOMPUTERINT.ClearDllAggregation()

    'End Sub


    ' Utilities


    Private Sub build_years_months_array(years_months_dict As Dictionary(Of Int32, Int32()), _
                                         periods_array() As Int32)

        For Each period As Integer In periods_array
            Dim currentYear As Int32 = Year(DateTime.FromOADate(period))
            Dim months_list As List(Of Int32) = Periods.GetMonthlyPeriodsList(currentYear, False)
            years_months_dict.Add(period, months_list.ToArray)
        Next

    End Sub



    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim FDLLInt As New FDLL_Interface

        Dim nb_periods = 5
        Dim nb_inputs = 6

        Dim accounts_list As String() = {"orn",
                                        "odiv",
                                        "ordebt",
                                        "oequ",
                                        "ocapex",
                                        "ocash",
                                        "icod",
                                        "iccap",
                                        "itrate",
                                        "irdebt",
                                        "icash",
                                        "ipdtfi",
                                        "irn",
                                        "rn",
                                        "div",
                                        "rdebt",
                                        "ndebt",
                                        "equ",
                                        "doe",
                                        "cash",
                                        "payout"}

        Dim inputs_names As String() = {"orn",
                                        "odiv",
                                        "ordebt",
                                        "oequ",
                                        "ocapex",
                                        "ocash"}

        Dim inputs_array As Double(,) = _
        {{1000, 1050, 1100, 1200, 1200},
         {0, 0, 0, 0, 0},
         {100, 101, 102, 103, 104},
         {1000, 2050, 3150, 4350, 5550},
         {0, 0, 0, 0, 0},
         {800, 800, 800, 800, 800}}

        Dim inputs_list(nb_periods * nb_inputs - 1) As String
        Dim inputs_periods(nb_periods * nb_inputs - 1) As Int32
        Dim inputs_values(nb_periods * nb_inputs - 1) As Double

        Dim index As Int32 = 0
        For i = 0 To nb_inputs - 1
            Dim account = inputs_names(i)
            For j = 0 To nb_periods - 1
                inputs_list(index) = account
                inputs_periods(index) = j
                inputs_values(index) = inputs_array(i, j)
                index = index + 1
            Next
        Next

        Dim constraints_list As String() = {"doe", "doe", "doe", "doe",
                                            "icod", "icod", "icod", "icod", "icod",
                                            "itrate", "itrate", "itrate", "itrate", "itrate",
                                            "iccap", "iccap", "iccap", "iccap", "iccap"}

        Dim constraints_periods As Int32() = {1, 2, 3, 4,
                                              0, 1, 2, 3, 4,
                                              0, 1, 2, 3, 4,
                                              0, 1, 2, 3, 4}

        Dim constraints_targets As Double() = {60, 60, 60, 60,
                                               -2, -2, -2, -2, -2,
                                               1, 1, 1, 1, 1,
                                               0, 0, 0, 0, 0}

        Dim comp_result = FDLLInt.Compute(inputs_list, inputs_periods, inputs_values, _
                                            constraints_list, constraints_periods, constraints_targets, _
                                            nb_periods, 0)

        'FDLLInt.DestroyDll()
        If comp_result = 1 Then
            Dim data_dic = FDLLInt.GetOutputMatrix(accounts_list)
            DrawChart(data_dic)
        Else
            MsgBox("The solver could not find any solution. Please check your model assumptions and constraints.")
        End If

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