' ExchangeRate.vb : CRUD model for exchange_rates table
'
'
' - check if possible to add records on a "select" query ! 
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 05/01/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports ADODB


Friend Class ExchangeRate


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Variables
    Friend current_version As String
    Friend modified_flag As Boolean
    Friend object_is_alive As Boolean

    ' Const
    Public Shared NB_QUERIES_TRY As Int32 = 10

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef rates_version_id As String)

        SRV = New ModelServer
        Dim str_sql = "SELECT * FROM " & CONFIG_DATABASE & "." & EXCHANGE_RATES_TABLE_NAME & _
                              " WHERE " & EX_TABLE_RATE_VERSION & "='" & rates_version_id & "'"
        Dim i As Int32 = 0
        Dim q_result = SRV.openRstSQL(str_sql, ModelServer.FWD_CURSOR)  '
        While q_result = False AndAlso i < NB_QUERIES_TRY
            q_result = SRV.openRstSQL(str_sql, ModelServer.FWD_CURSOR)
            i = i + 1
        End While
        object_is_alive = q_result
        RST = SRV.rst
        current_version = rates_version_id

    End Sub


#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateExchangeRate(ByRef curr As String, _
                                  ByRef period As Integer, _
                                  ByRef version As String, _
                                  ByRef value As Double)

        RST.AddNew()
        Dim curr_token As String = curr & "/" & MAIN_CURRENCY
        RST(EX_TABLE_CURRENCY_VARIABLE).Value = curr_token
        RST(EX_TABLE_PERIOD_VARIABLE).Value = period
        RST(EX_TABLE_RATE_VARIABLE).Value = 1
        RST(EX_TABLE_RATE_ID_VARIABLE).Value = curr_token & period
        RST(EX_TABLE_RATE_VERSION).Value = version
        RST.Update()

    End Sub

    Protected Friend Function ReadRate(ByRef rate_id As String, ByRef field As String) As Object

        RST.Filter = EX_TABLE_RATE_ID_VARIABLE + "='" + rate_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Sub UpdateRate(ByRef rate_id As String, ByRef rateAttributes As Hashtable)

        RST.Filter = EX_TABLE_RATE_ID_VARIABLE + "='" + rate_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In rateAttributes.Keys
                If RST.Fields(Attribute).Value <> rateAttributes(Attribute) Then RST.Fields(Attribute).Value = rateAttributes(Attribute)
            Next
            modified_flag = True
        End If

    End Sub

    Protected Friend Sub UpdateRate(ByRef rate_id As String, _
                          ByRef field As String, _
                          ByVal value As Object)

        RST.Filter = EX_TABLE_RATE_ID_VARIABLE + "='" + rate_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                modified_flag = True
            End If
        End If

    End Sub

    Protected Friend Sub DeleteRate(ByRef rate_id As String)

        RST.Filter = EX_TABLE_RATE_ID_VARIABLE + "='" + rate_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            modified_flag = True
        End If

    End Sub

    Protected Friend Sub UpdateModel()

        RST.UpdateBatch()
        modified_flag = False

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Function DeleteAllRates(ByRef version_id As String) As Boolean

        Dim srv As New ModelServer
        Dim str_sql As String = "DELETE FROM " & CONFIG_DATABASE & "." & EXCHANGE_RATES_TABLE_NAME & _
                                " WHERE " & EX_TABLE_RATE_VERSION & "='" & version_id & "'"
        Dim i As Int32 = 0
        Dim q_result = srv.sqlQuery(str_sql)
        While q_result = False AndAlso i < NB_QUERIES_TRY
            q_result = srv.sqlQuery(str_sql)
            i = i + 1
        End While
        Return q_result
        srv = Nothing

    End Function

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region



End Class
