' Currency.vb : CRUD model for currencies table
'
'
'
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



Friend Class Currency


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Variables
    Friend object_is_alive As Boolean


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        SRV = New ModelServer
        Dim i As Int32 = 0
        Dim q_result = SRV.openRst(GlobalVariables.database & "." & CURRENCIES_TABLE_NAME, ModelServer.STATIC_CURSOR)
        While q_result = False AndAlso i < 10
            q_result = SRV.openRst(GlobalVariables.database & "." & CURRENCIES_TABLE_NAME, ModelServer.STATIC_CURSOR)
            i = i + 1
        End While
        object_is_alive = q_result
        RST = SRV.rst

    End Sub


#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateCurrency(ByRef currency_id As String)

        RST.AddNew(CURRENCIES_KEY_VARIABLE, currency_id)

    End Sub

    Protected Friend Function ReadCurrencies() As List(Of String)

        Dim tmp_list As New List(Of String)
        RST.Filter = ""
        RST.MoveFirst()
        While RST.EOF = False
            tmp_list.Add(RST.Fields(CURRENCIES_KEY_VARIABLE).Value)
            RST.MoveNext()
        End While
        Return tmp_list

    End Function

    Protected Friend Sub DeleteCurrency(ByRef currency_id As String)

        RST.Filter = CURRENCIES_KEY_VARIABLE + "='" + currency_id + "'"
        If RST.EOF = False Then RST.Delete()
        RST.Filter = ""

    End Sub

    Protected Friend Sub UpdateModel()

        RST.UpdateBatch()

    End Sub

#End Region


#Region "Utilities"

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region


End Class
