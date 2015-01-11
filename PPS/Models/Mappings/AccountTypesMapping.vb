' AccountTypesMapping.vb
'
' Mapping for account_types table
'
'
' To do:
'
' Known bugs:
'
'
'Last modified: 29/12/2014
' Author: Julien Monnereau


Imports System.Collections.Generic


Friend Class AccountTypesMapping

    Protected Friend Shared Function GetAccountTypesDic(ByRef Key As String, _
                                                        ByRef Value As String) As Dictionary(Of String, String)

        Dim tmpHT As New Dictionary(Of String, String)
        Dim srv As New ModelServer
        srv.openRst(CONFIG_DATABASE + "." + ACCOUNT_TYPE_TABLE, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpHT.Add(srv.rst.Fields(Key).Value, srv.rst.Fields(Value).Value)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        Return tmpHT

    End Function


End Class
