' CredentialMappingClass.vb
'
' Provides lists and dictionary relative to credentials table
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


Public Class CredentialsTypesMapping



#Region "List provider"

    Public Shared Function GetCredentialsTypesList(item As String) As List(Of String)

        Dim tmpList As New List(Of String)
        Dim srv As New ModelServer
        srv.openRst(CONFIG_DATABASE + "." + CREDENTIALS_TYPES_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(item).Value)
                srv.rst.MoveNext()
            Loop
        End If

        srv.rst.Close()
        Return tmpList

    End Function


#End Region










End Class
