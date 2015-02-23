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
'Last modified: 19/01/2015
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Collections


Friend Class VersionsMapping


#Region "List provider"

    Protected Friend Shared Function GetVersionsList(item As String) As List(Of String)

        Dim srv As New ModelServer
        Dim tmpList As New List(Of String)
        srv.OpenRst(CONFIG_DATABASE + "." + VERSIONS_TABLE, _
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
    Protected Friend Shared Function GetVersionsHashTable(ByRef keys As String, ByRef values As String) As Hashtable

        Dim srv As New ModelServer
        Dim tmpHT As New Hashtable
        srv.OpenRst(CONFIG_DATABASE + "." + VERSIONS_TABLE, _
                    ModelServer.FWD_CURSOR)
        srv.rst.Filter = VERSIONS_IS_FOLDER_VARIABLE + "= 0"
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

    Protected Friend Shared Function GetVersionTimeConfiguration(ByRef versionkey As String) As Hashtable

        Dim srv As New ModelServer
        Dim tmpHT As New Hashtable
        srv.OpenRst(CONFIG_DATABASE + "." + VERSIONS_TABLE, _
                    ModelServer.FWD_CURSOR)
        srv.rst.Filter = VERSIONS_CODE_VARIABLE + "='" + versionkey + "'"
        If srv.rst.EOF = False And srv.rst.BOF = False Then
            tmpHT.Add(VERSIONS_TIME_CONFIG_VARIABLE, srv.rst.Fields(VERSIONS_TIME_CONFIG_VARIABLE).Value)
            tmpHT.Add(VERSIONS_START_PERIOD_VAR, srv.rst.Fields(VERSIONS_START_PERIOD_VAR).Value)
            tmpHT.Add(VERSIONS_NB_PERIODS_VAR, srv.rst.Fields(VERSIONS_NB_PERIODS_VAR).Value)
            srv.rst.MoveNext()
        Else
            Return Nothing
        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

    Protected Friend Shared Function GetVersionsTimeConfiguration() As Dictionary(Of String, Hashtable)

        Dim srv As New ModelServer
        Dim tmpDict As New Dictionary(Of String, Hashtable)
        srv.OpenRst(CONFIG_DATABASE + "." + VERSIONS_TABLE, _
                    ModelServer.FWD_CURSOR)
        srv.rst.Filter = ""

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False

                Dim tmpHT As New Hashtable
                tmpHT.Add(VERSIONS_TIME_CONFIG_VARIABLE, srv.rst.Fields(VERSIONS_TIME_CONFIG_VARIABLE).Value)
                tmpHT.Add(VERSIONS_START_PERIOD_VAR, srv.rst.Fields(VERSIONS_START_PERIOD_VAR).Value)
                tmpHT.Add(VERSIONS_NB_PERIODS_VAR, srv.rst.Fields(VERSIONS_NB_PERIODS_VAR).Value)
                tmpDict.Add(srv.rst.Fields(VERSIONS_CODE_VARIABLE).Value, tmpHT)
                srv.rst.MoveNext()
            Loop
        Else
            Return Nothing
        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpDict

    End Function

#End Region


    Protected Friend Shared Function GetVersionsIDFromName(ByRef version_name As String) As String

        Dim srv As New ModelServer
        Dim version_id As String
        srv.OpenRst(CONFIG_DATABASE + "." + VERSIONS_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = VERSIONS_NAME_VARIABLE & "='" & version_name & "'"
        If srv.rst.EOF = False And srv.rst.BOF = False Then
            version_id = srv.rst.Fields(RATES_VERSIONS_ID_VARIABLE).Value
            srv.rst.Close()
            Return version_id
        Else
            srv.rst.Close()
            Return ""
        End If

    End Function

End Class
