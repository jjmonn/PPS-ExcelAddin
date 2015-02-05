' MarketPrice.vb : CRUD model for market_index_prices table
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 05/02/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports ADODB


Friend Class MarketPrice


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Variables
    Friend current_version As String
    Friend modified_flag As Boolean
    Friend object_is_alive As Boolean

  
#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef market_price_version_id As String)

        SRV = New ModelServer
        Dim str_sql = "SELECT * FROM " & CONFIG_DATABASE & "." & MARKET_INDEXES_PRICES_TABLE & _
                     " WHERE " & MARKET_INDEXES_PRICES_VERSION_VAR & "='" & market_price_version_id & "'"
        Dim i As Int32 = 0
        Dim q_result = SRV.openRstSQL(str_sql, ModelServer.FWD_CURSOR)  '
        object_is_alive = q_result
        RST = SRV.rst
        current_version = market_price_version_id

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateMarketPrice(ByRef id As String, _
                                           ByRef period As Integer, _
                                           ByRef version As String, _
                                           ByRef value As Double)

        RST.AddNew()
        RST(MARKET_INDEXES_PRICES_ID_VAR).Value = id
        RST(MARKET_INDEXES_PRICES_PERIOD_VAR).Value = period
        RST(MARKET_INDEXES_PRICES_VERSION_VAR).Value = version
        RST(MARKET_INDEXES_PRICES_VALUE_VAR).Value = value
        RST.Update()

    End Sub

    Protected Friend Function ReadMarketPrice(ByRef MarketPrice_id As String, _
                                              ByRef period As Int32) As Object

        RST.Filter = MARKET_INDEXES_PRICES_ID_VAR & "='" & MarketPrice_id & "' AND " & _
                   MARKET_INDEXES_PRICES_PERIOD_VAR & "=" & period
        If RST.EOF Then Return Nothing
        Return RST.Fields(MARKET_INDEXES_PRICES_VALUE_VAR).Value

    End Function

    Protected Friend Sub UpdateMarketPrice(ByRef MarketPrice_id As String, _
                                           ByRef period As Int32, _
                                           ByVal value As Object)

        RST.Filter = MARKET_INDEXES_PRICES_ID_VAR & "='" & MarketPrice_id & "' AND " & _
                     MARKET_INDEXES_PRICES_PERIOD_VAR & "=" & period
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(MARKET_INDEXES_PRICES_VALUE_VAR).Value <> value Then
                RST.Fields(MARKET_INDEXES_PRICES_VALUE_VAR).Value = value
                modified_flag = True
            End If
        Else
            CreateMarketPrice(MarketPrice_id, period, current_version, value)
        End If

    End Sub

    Protected Friend Sub DeleteMarketPrice(ByRef MarketPrice_id As String, ByRef period As Int32)

        RST.Filter = MARKET_INDEXES_PRICES_ID_VAR & "='" & MarketPrice_id & "' AND " & _
                     MARKET_INDEXES_PRICES_PERIOD_VAR & "=" & period
        If RST.EOF = False Then
            RST.Delete()
            modified_flag = True
        End If

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Function DeleteAllMarketPrices(ByRef version_id As String) As Boolean

        Dim srv As New ModelServer
        Dim str_sql As String = "DELETE FROM " & CONFIG_DATABASE & "." & MARKET_INDEXES_PRICES_TABLE & _
                                " WHERE " & MARKET_INDEXES_PRICES_VERSION_VAR & "='" & version_id & "'"
        Dim i As Int32 = 0
        Dim q_result = srv.sqlQuery(str_sql)
        Return q_result
        srv = Nothing

    End Function

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub

  
#End Region



End Class
