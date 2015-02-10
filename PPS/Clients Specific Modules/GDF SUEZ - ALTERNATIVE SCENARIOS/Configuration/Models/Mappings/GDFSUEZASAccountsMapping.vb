' GDFSUEZASAccountsMapping.vb
'
' Provides information on GDF SUEZ Alternative Scenarios Accounts Table
' This table provides the accounts to be computed and displayed after the sensitivities have been computed
' Each new account = Base Account + Incr Item
'
'
' Known bugs
'
' Author: Julien Monnereau
' Last modified: 16/01/2015 


Imports System.Collections.Generic
Imports System.Collections


Friend Class GDFSUEZASAccountsMapping


    Protected Friend Shared Function GetAlternativeScenarioAccounts() As Dictionary(Of String, String)

        Dim dict As New Dictionary(Of String, String)
        Dim srv As New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + GDF_AS_ACCOUNTS_TABLES, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                dict.Add(srv.rst.Fields(GDF_AS_ACCOUNT_ID_VAR).Value, srv.rst.Fields(GDF_AS_ITEM_VAR).Value)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        Return dict

    End Function

    Protected Friend Shared Function GetAlternativeScenarioAccountsList() As List(Of String)

        Dim tmp_list As New List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(CONFIG_DATABASE + "." + GDF_AS_ACCOUNTS_TABLES, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmp_list.Add(srv.rst.Fields(GDF_AS_ACCOUNT_ID_VAR).Value)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        Return tmp_list

    End Function

End Class
