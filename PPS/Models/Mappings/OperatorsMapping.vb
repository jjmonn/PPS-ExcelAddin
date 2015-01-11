'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 26/12/2014


Imports System.Collections
Imports System.Collections.Generic


Friend Class OperatorsMapping


    Protected Friend Shared Function GetOperatorsDictionary(ByRef Key As String, ByRef Value As String) As Dictionary(Of String, String)

        Dim srv As New ModelServer
        Dim tmpHT As New Dictionary(Of String, String)
        srv.openRst(CONFIG_DATABASE + "." + OPERATORS_TABLE, ModelServer.FWD_CURSOR)

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




End Class
