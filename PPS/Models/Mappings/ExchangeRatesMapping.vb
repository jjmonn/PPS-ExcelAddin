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
' Last modified: 02/12/2014


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

    ' Convention: all periods rates has been previously filled in currencies management
    Protected Friend Shared Sub FillRatesLists(ByVal currency_token As String, _
                                               ByRef rates_version As String, _
                                               ByRef time_config As String, _
                                               ByRef reverse_flag As Int32, _
                                               ByRef periods_list As List(Of Int32), _
                                               ByRef rates As List(Of Double), _
                                               Optional ByRef ref_period As Int32 = 0)

        Dim srv As New ModelServer
        If reverse_flag = 1 Then reverse_token(currency_token)
        srv.openRstSQL("SELECT * FROM " & CONFIG_DATABASE & "." & EXCHANGE_RATES_TABLE_NAME & _
                       " WHERE " & EX_RATES_CURRENCY_VARIABLE & "='" & currency_token & "'" & " AND " & _
                         EX_RATES_RATE_VERSION & "='" & rates_version & "'", _
                         ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            If time_config = YEARLY_TIME_CONFIGURATION Then
                srv.rst.MoveFirst()
                Do While srv.rst.EOF = False
                    periods_list.Add(srv.rst.Fields(EX_RATES_PERIOD_VARIABLE).Value)
                    If reverse_flag = 1 Then
                        rates.Add(1 / srv.rst.Fields(EX_RATES_RATE_VARIABLE).Value)
                    Else
                        rates.Add(srv.rst.Fields(EX_RATES_RATE_VARIABLE).Value)
                    End If
                    srv.rst.MoveNext()
                Loop
            Else
                Dim months_list As List(Of Integer) = Period.GetMonthsPeriodsInOneYear(ref_period, 1)
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
            End If
        End If
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
