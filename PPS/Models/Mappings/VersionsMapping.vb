' VersioningMapping.vb
'
' Creates dictionaries for Versions Table
'
'
' To do : 
'
'
' Know bugs :
' 
'
'Last modified: 29/11/2014
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Collections


Public Class VersionsMapping


#Region "List provider"

    Public Shared Function GetVersionsList(item As String) As List(Of String)

        Dim srv As New ModelServer
        Dim tmpList As New List(Of String)
        srv.openRst(CONFIG_DATABASE + "." + VERSIONS_TABLE, _
                    ModelServer.FWD_CURSOR)
        srv.rst.Filter = VERSIONS_IS_FOLDER_VARIABLE + "= 0"

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

#End Region


#Region "Dictionary Provider"

    ' returns a dictionary of keys: param1 | Values: param2
    Public Shared Function GetVersionsHashTable(ByRef keys As String, ByRef values As String) As Hashtable

        Dim srv As New ModelServer
        Dim tmpHT As New Hashtable
        srv.openRst(CONFIG_DATABASE + "." + VERSIONS_TABLE, _
                    ModelServer.FWD_CURSOR)
        srv.rst.filter = VERSIONS_IS_FOLDER_VARIABLE + "= 0"
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                'If Not tmpHT.ContainsKey(srv.rst.Fields(Key).Value) Then
                tmpHT.Add(srv.rst.Fields(keys).Value, srv.rst.Fields(values).Value)
                srv.rst.MoveNext()
                ' End If
            Loop

        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

    Public Shared Function GetVersionTimeConfiguration(ByRef versionkey As String) As Hashtable

        Dim srv As New ModelServer
        Dim tmpHT As New Hashtable
        srv.openRst(CONFIG_DATABASE + "." + VERSIONS_TABLE, _
                    ModelServer.FWD_CURSOR)
        srv.rst.filter = VERSIONS_CODE_VARIABLE + "='" + versionkey + "'"
        If srv.rst.EOF = False And srv.rst.BOF = False Then
            tmpHT.Add(VERSIONS_TIME_CONFIG_VARIABLE, srv.rst.Fields(VERSIONS_TIME_CONFIG_VARIABLE).Value)
            tmpHT.Add(VERSIONS_REFERENCE_YEAR_VARIABLE, srv.rst.Fields(VERSIONS_REFERENCE_YEAR_VARIABLE).Value)
            srv.rst.MoveNext()
        Else
            Return Nothing
        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

    Public Shared Function GetVersionsTimeConfiguration() As Dictionary(Of String, Hashtable)

        Dim srv As New ModelServer
        Dim tmpDict As New Dictionary(Of String, Hashtable)
        srv.openRst(CONFIG_DATABASE + "." + VERSIONS_TABLE, _
                    ModelServer.FWD_CURSOR)
        srv.rst.filter = ""

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False

                Dim tmpHT As New Hashtable
                tmpHT.Add(VERSIONS_TIME_CONFIG_VARIABLE, srv.rst.Fields(VERSIONS_TIME_CONFIG_VARIABLE).Value)
                tmpHT.Add(VERSIONS_REFERENCE_YEAR_VARIABLE, srv.rst.Fields(VERSIONS_REFERENCE_YEAR_VARIABLE).Value)
                tmpDict.Add(srv.rst.Fields(VERSIONS_CODE_VARIABLE).Value, tmpHT)
                srv.rst.MoveNext()
            Loop
        Else
            Return Nothing
        End If
        srv.rst.close()
        srv = Nothing
        Return tmpDict

    End Function

#End Region


End Class
