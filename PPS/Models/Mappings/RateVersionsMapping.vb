' CFXRatesVersionMapping.VB
'
' provides lists and dictionaries for the rates versioning table 
' -> Remplacé par RateVersion.vb ??!!
'
'
' To do:
'       - 
'       - 
'
'
' Known bugs:
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 02/12/2014


Imports System.Collections.Generic



Public Class RateVersionsMapping


    ' Return a list of 
    Public Shared Function GetRatesVersionsList(item As String) As List(Of String)

        Dim srv As New ModelServer
        Dim tmpList As New List(Of String)
        srv.openRst(CONFIG_DATABASE + "." + RATES_VERSIONS_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = RATES_VERSIONS_IS_FOLDER_VARIABLE + "= 0"

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(item).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        srv = Nothing
        Return tmpList

    End Function


End Class
