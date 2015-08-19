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
        srv.openRst(GlobalVariables.database + "." + RATES_VERSIONS_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = RATES_IS_FOLDER_VARIABLE + "= 0"

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


    Protected Friend Shared Function GetRatesVersionDictionary(ByRef Key As String, _
                                                               ByRef Value As String) As Dictionary(Of String, String)

        Dim srv As New ModelServer
        Dim tmpHT As New Dictionary(Of String, String)
        srv.OpenRst(GlobalVariables.database + "." + RATES_VERSIONS_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = RATES_IS_FOLDER_VARIABLE + "= 0"

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
