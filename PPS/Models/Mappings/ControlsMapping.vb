'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 26/12/2014


Imports System.Collections
Imports System.Collections.Generic


Friend Class ControlsMapping


    Protected Friend Shared Function GetControlsDictionary(ByRef Key As String, ByRef Value As String) As Dictionary(Of String, String)

        Dim srv As New ModelServer
        Dim tmpHT As New Dictionary(Of String, String)
        srv.openRst(GlobalVariables.database + "." + CONTROLS_TABLE, ModelServer.FWD_CURSOR)

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

    Protected Friend Shared Function GetControlsList(ByRef Field As String) As List(Of String)

        Dim srv As New ModelServer
        Dim tmpList As New List(Of String)
        srv.openRst(GlobalVariables.database + "." + CONTROLS_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(Field).Value)
                srv.rst.MoveNext()
            Loop

        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpList

    End Function


End Class
