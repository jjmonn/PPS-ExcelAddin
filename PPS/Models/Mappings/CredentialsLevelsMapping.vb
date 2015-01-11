' CredentialKeysMappingClass.vb
'
' Provides lists and dictionary relative to credentials AssetID and CredentialKeys
'
'
'
' To do:
'
'
' 
' Known bugs:
'
'
'
' Author: Julien Monnereau
' Last modified: 02/12/2014


Imports System.Collections
Imports System.Collections.Generic


Public Class CredentialsLevelsMapping



#Region "Dictionary provider"

    ' returns a dictionary of keys: param1 | Values: param2
    Public Shared Function GetCredentialKeysDictionaries(ByRef keys As String, ByRef values As String) As Dictionary(Of String, String)

        Dim tmpHT As New Dictionary(Of String, String)
        Dim srv As New ModelServer
        srv.openRst(CONFIG_DATABASE + "." + CREDENTIALS_ID_TABLE, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpHT.Add(srv.rst.Fields(keys).Value, srv.rst.Fields(values).Value)
                srv.rst.MoveNext()
            Loop

        End If
        srv.rst.Close()
        srv = Nothing
        Return tmpHT

    End Function

#End Region


#Region "List provider"

    ' Return a list of 
    Protected Friend Shared Function GetCredentialsList(item As String) As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(CONFIG_DATABASE + "." + CREDENTIALS_ID_TABLE, ModelServer.FWD_CURSOR)

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

    Protected Friend Shared Function GetCredentialsLevelsList() As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRstSQL("SELECT " & CREDENTIALS_ID_VARIABLE & " FROM " & CONFIG_DATABASE + "." + CREDENTIALS_ID_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(CREDENTIALS_ID_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        srv = Nothing
        Return tmpList

    End Function


#End Region


#Region "Others"

    ' Return the max credentialKey
    Public Shared Function GetMaxCredentialKey() As Int32

        Dim result As Int32
        Dim srv As New ModelServer

        Dim strSql As String = "SELECT MAX(" + CREDENTIALS_ID_VARIABLE + ") FROM " + CONFIG_DATABASE + "." + CREDENTIALS_ID_TABLE
        srv.openRstSQL(strSql, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then
            result = srv.rst.Fields("MAX(" + CREDENTIALS_ID_VARIABLE + ")").Value
            srv.rst.Close()
            Return result
        Else
            srv.rst.Close()
            Return Nothing
        End If

    End Function


#End Region



End Class
