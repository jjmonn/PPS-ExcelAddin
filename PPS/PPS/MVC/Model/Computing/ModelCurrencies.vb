Public Class ModelCurrencies

    Private rst As ADODB.Recordset
    Private srv As ModelServer
    Private ratesRST As ADODB.Recordset

    Public Sub New()
        rst = New ADODB.Recordset
        srv = New ModelServer
    End Sub

    Public Sub UploadRates(Currencies As String, PeriodsArray() As Integer, RatesArray() As Double)

        '------------------------------------------------------------------
        ' Rates upload: Save the rates to the database
        '------------------------------------------------------------------
        Dim i As Integer
        Dim Fields() As Object
        Dim Values As Object
        Dim criteria As String

        Fields = {EX_TABLE_CURRENCY_VARIABLE, EX_TABLE_PERIOD_VARIABLE, _
                  EX_TABLE_RATE_VARIABLE, EX_TABLE_RATE_ID_VARIABLE}

        rst = srv.openRst(EXCHANGE_RATES_TABLE_NAME)
        For i = LBound(PeriodsArray) To UBound(PeriodsArray)

            Values = {Currencies, PeriodsArray(i), _
                      RatesArray(i), Currencies & PeriodsArray(i)}

            criteria = EX_TABLE_RATE_ID_VARIABLE & "=" & "'" & Values(3) & "'"
            rst.Find(criteria, , , 1)

            If rst.EOF = True Or rst.BOF = True Then                                ' If record NOT FOUND
                rst.AddNew(Fields, Values)                                          ' Add records to database
                rst.Update()
            Else
                If rst.Fields(EX_TABLE_RATE_VARIABLE).Value <> Values(2) Then
                    rst.Fields(EX_TABLE_RATE_VARIABLE).Value = Values(2)
                    rst.Update()
                End If
            End If
        Next i
        MsgBox("Upload successful")

    End Sub

    Public Sub RatesLoad()

        '----------------------------------------------------------
        ' Sub: Rates Load
        '----------------------------------------------------------
        ratesRST = srv.openRst(EXCHANGE_RATES_TABLE_NAME)

    End Sub

    Public Function Convert(Value As Double, OriginalCurr As String, _
                            DestinationCurr As String, Period As Integer) As Double

        '----------------------------------------------------------
        ' Convert the input value to the output currency
        '----------------------------------------------------------
        Dim criteria As String
        Dim Currencies As String

        Currencies = OriginalCurr & CURRENCIES_SEPARATOR & DestinationCurr
        criteria = EX_TABLE_RATE_ID_VARIABLE & "=" & "'" & Currencies & Period & "'"
        ratesRST.Find(criteria, , , 1)

        If ratesRST.EOF Or ratesRST.BOF Then

            Currencies = DestinationCurr & CURRENCIES_SEPARATOR & OriginalCurr
            criteria = EX_TABLE_RATE_ID_VARIABLE & "=" & "'" & Currencies & Period & "'"
            ratesRST.Find(criteria, , , 1)

            If ratesRST.EOF Or ratesRST.BOF Then
                MsgBox("Key not in mapping, check modeldatabase. Error PP004")                ' Item not found
                Convert = Nothing
                Exit Function
            End If
            Convert = Value / ratesRST.Fields(EX_TABLE_RATE_VARIABLE).Value
        Else
            Convert = Value * ratesRST.Fields(EX_TABLE_RATE_VARIABLE).Value
        End If

    End Function

End Class
