' FinancialModellingAccountsMapping.vb
'
' Manages the transaction with financial_modelling_accounts table in DB. 
'
' To do : 
'
'
' Know bugs :
' 
'
'Last modified: 23/12/2014
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Collections


Friend Class FModelingAccountsMapping


#Region "Lists"

    Protected Friend Shared Function GetFModellingAccountsList(ByRef key As String, _
                                                               Optional ByRef type As String = "") As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv = New ModelServer
        srv.openRst(CONFIG_DATABASE + "." + FINANCIAL_MODELLING_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Sort = ITEMS_POSITIONS
        If type <> "" Then srv.rst.Filter = FINANCIAL_MODELLING_TYPE_VARIABLE + "='" & type & "'"

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(key).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        srv = Nothing
        Return tmpList

    End Function


#End Region


#Region "Dictionaries"

    Protected Friend Shared Function GetFModellingAccountsDict(ByRef key As String, _
                                                               ByRef value As String, _
                                                               Optional ByRef type As String = "") As Hashtable

        Dim tmpHT As New Hashtable
        Dim srv = New ModelServer
        srv.openRst(CONFIG_DATABASE + "." + FINANCIAL_MODELLING_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Sort = ITEMS_POSITIONS
        If type <> "" Then srv.rst.Filter = FINANCIAL_MODELLING_TYPE_VARIABLE + "='" & type & "'"

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpHT.Add(srv.rst.Fields(key).Value, srv.rst.Fields(value).Value)
                srv.rst.MoveNext()
            Loop
        End If
        Return tmpHT

    End Function


#End Region







End Class
