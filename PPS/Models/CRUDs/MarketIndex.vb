' MarketIndex.vb
'
' CRUD Model for market indexes table
'
'
' Author: Julien Monnereau
' Last modified: 05/02/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports ADODB


Friend Class MarketIndex


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
        object_is_alive = SRV.OpenRst(GlobalVariables.database & "." & MARKET_INDEXES_TABLE, ModelServer.STATIC_CURSOR)
        RST = SRV.rst

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub Createindex(ByRef index_id As String)

        RST.AddNew(MARKET_INDEXES_ID_VARIABLE, index_id)

    End Sub

    Protected Friend Function Readindexes() As List(Of String)

        Dim tmp_list As New List(Of String)
        RST.Filter = ""
        RST.MoveFirst()
        While RST.EOF = False
            tmp_list.Add(RST.Fields(MARKET_INDEXES_ID_VARIABLE).Value)
            RST.MoveNext()
        End While
        Return tmp_list

    End Function

    Protected Friend Sub Deleteindex(ByRef index_id As String)

        RST.Filter = MARKET_INDEXES_ID_VARIABLE + "='" + index_id + "'"
        If RST.EOF = False Then RST.Delete()
        RST.Filter = ""

    End Sub

#End Region


#Region "Utilities"

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region


End Class
