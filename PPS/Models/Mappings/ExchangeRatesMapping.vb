' CExchangeRatesMapping.vb
'
' Produces a dictionary of exchange rates
'
'
'
' To do: 
'       - 
'
'
'
' Known bugs:
'        - 
'
'
'
' Author: Julien Monnereau
' Last modified: 08/02/2015


Imports System.Collections
Imports System.Collections.Generic


Friend Class ExchangeRatesMapping


    Protected Friend Shared Function GetFXDictionary(ByRef Key As String, _
                                            ByRef Value As String, _
                                            ByRef version As String) As Hashtable

        Dim tmpHT As New Hashtable
        Dim srv As New ModelServer
        srv.OpenRst(CONFIG_DATABASE & "." & EXCHANGE_RATES_TABLE_NAME, ModelServer.FWD_CURSOR)
        srv.rst.Filter = EX_RATES_RATE_VERSION & "='" & version & "'"

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpHT.Add(srv.rst.Fields(Key).Value, srv.rst.Fields(Value).Value)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

    Protected Friend Shared Sub FillRatesLists(ByVal currency_token As String, _
                                               ByRef rates_version As String, _
                                               ByRef reverse_flag As Int32, _
                                               ByRef periods_list As List(Of Int32), _
                                               ByRef rates As List(Of Double), _
                                               ByRef start_period As Int32, _
                                               ByRef nb_periods As Int32)

        Dim srv As New ModelServer
        If reverse_flag = 1 Then reverse_token(currency_token)
        Dim str_SQL As String = "SELECT * FROM " & CONFIG_DATABASE & "." & EXCHANGE_RATES_TABLE_NAME & _
                                " WHERE " & EX_RATES_CURRENCY_VARIABLE & "='" & currency_token & "'" & " AND " & _
                                  EX_RATES_RATE_VERSION & "='" & rates_version & "'"

        srv.openRstSQL(str_SQL, ModelServer.FWD_CURSOR)
        For year_period As Int32 = start_period To start_period + nb_periods - 1
            Dim months_list As List(Of Integer) = Period.GetMonthsPeriodsInOneYear(year_period, 0)
            For Each period In months_list
                periods_list.Add(period)
                srv.rst.Filter = EX_RATES_PERIOD_VARIABLE & "=" & period
                If srv.rst.EOF = False And srv.rst.BOF = False Then
                    If reverse_flag = 1 Then
                        rates.Add(1 / srv.rst.Fields(EX_RATES_RATE_VARIABLE).Value)
                    Else
                        rates.Add(srv.rst.Fields(EX_RATES_RATE_VARIABLE).Value)
                    End If
                Else
                    rates.Add(1)
                End If
            Next
        Next
        srv.rst.Close()
        srv = Nothing

    End Sub


#Region "Utilities"

    Public Shared Sub reverse_token(ByRef token As String)

        Dim left_side = Left(token, CURRENCIES_TOKEN_SIZE)
        Dim right_side = Right(token, CURRENCIES_TOKEN_SIZE)
        token = right_side + CURRENCIES_SEPARATOR + left_side

    End Sub

#End Region



End Class
