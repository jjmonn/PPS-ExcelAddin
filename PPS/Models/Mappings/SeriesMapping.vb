' Mapping for Series tables
'
'
'
' Author: Julien Monnereau
' Last modified: 28/12/2014


Imports System.Collections.Generic


Friend Class SeriesMapping


    Protected Friend Shared Function GetSerieColorsList() As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(GlobalVariables.database + "." + SERIE_COLORS_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(SERIE_COLORS_ID_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        Return tmpList

    End Function

    Protected Friend Shared Function GetSerieTypesList() As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(GlobalVariables.database + "." + SERIE_TYPE_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(SERIE_TYPE_ID_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        Return tmpList

    End Function


End Class
