' Mapping for Series tables
'
'
'
' Author: Julien Monnereau
' Last modified: 29/12/2014


Imports System.Collections.Generic


Friend Class PalettesMapping


    Protected Friend Shared Function GetPalettesList() As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(CONFIG_DATABASE + "." + PALETTES_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(PALETTES_ID_VARIABLES).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        Return tmpList

    End Function


End Class
