﻿' clientsMapping.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 25/06/2015


Imports System.Collections
Imports System.Collections.Generic




Friend Class ClientsMapping


    Friend Shared Function GetClientsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim srv As New ModelServer
        Dim tmpHT As New Hashtable
        srv.OpenRst(GlobalVariables.database + "." + CLIENTS_TABLE, ModelServer.FWD_CURSOR)
        Do While srv.rst.EOF = False
            tmpHT.Add(srv.rst.Fields(Key).Value, srv.rst.Fields(Value).Value)
            srv.rst.MoveNext()
        Loop
        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

    Friend Shared Function GetclientsIDList() As List(Of String)

        Dim ids_list As New List(Of String)
        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database + "." + CLIENTS_TABLE, ModelServer.FWD_CURSOR)
        Do While srv.rst.EOF = False
            ids_list.Add(srv.rst.Fields(ANALYSIS_AXIS_ID_VAR).Value)
            srv.rst.MoveNext()
        Loop
        srv.rst.Close()
        Return ids_list

    End Function

End Class
