' MarketPricesMapping.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 16/01/2015


Imports System.Collections.Generic


Friend Class MarketPricesMapping


    Protected Friend Shared Function GetIndexMarketPricesFlatArray(ByRef index_id As String, _
                                                                   ByRef version_id As String, _
                                                                   ByRef period_array As Int32(), _
                                                                   ByRef time_configuration As String) As Double()

        Dim srv As New ModelServer
        Dim prices_array(period_array.Length) As Double
        srv.openRstSQL("SELECT * FROM " & CONFIG_DATABASE & "." & MARKET_INDEXES_PRICES_TABLE & _
                       " WHERE " & MARKET_INDEXES_PRICES_ID_VAR & "='" & index_id & "'" & " AND " & _
                       MARKET_INDEXES_PRICES_VERSION_VAR & "='" & version_id & "'", _
                       ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            If time_configuration = YEARLY_TIME_CONFIGURATION Then
                For j = 0 To period_array.Length - 1

                    Dim tmp_list As New List(Of Double)
                    Dim months_list As List(Of Integer) = Periods.GetMonthlyPeriodsList(period_array(j), 0)
                    For Each month_ In months_list
                        srv.rst.Filter = EX_TABLE_PERIOD_VARIABLE & "=" & month_
                        tmp_list.Add(srv.rst.Fields(MARKET_INDEXES_PRICES_VALUE_VAR).Value)
                    Next
                    prices_array(j) = ComputeAverage(tmp_list)

                Next
            Else
                For j = 0 To period_array.Length - 1
                    srv.rst.Filter = EX_TABLE_PERIOD_VARIABLE & "=" & period_array(j)
                    prices_array(j) = srv.rst.Fields(MARKET_INDEXES_PRICES_VALUE_VAR).Value
                Next
            End If
        End If
        srv.rst.Close()
        srv = Nothing
        Return prices_array

    End Function



#Region "Utilities"

    Public Shared Function ComputeAverage(ByRef values As List(Of Double)) As Double

        Dim sum As Double
        For Each value In values
            sum = sum + value
        Next
        Return sum / values.Count

    End Function


#End Region

End Class
