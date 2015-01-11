' UsersMapping.vb
'
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 07/12/2014


Imports System.Collections.Generic
Imports System.Collections


Friend Class UsersMapping


    ' returns a dictionary of keys: param1 | Values: param2
    Protected Friend Shared Function GetUsersDictionary(ByRef keys As String, ByRef values As String) As Hashtable

        Dim srv As New ModelServer
        Dim tmpHT As New Hashtable
        srv.openRst(CONFIG_DATABASE + "." + USERS_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = USERS_IS_FOLDER_VARIABLE + "= 0"
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

  
    Protected Friend Shared Function GetUsersTable() As Dictionary(Of String, Hashtable)

        Dim dict As New Dictionary(Of String, Hashtable)
        Dim srv As New ModelServer
        srv.openRst(CONFIG_DATABASE + "." + USERS_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = USERS_IS_FOLDER_VARIABLE + "= 0"
        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False

                Dim tmpHT As New Hashtable
                tmpHT.Add(USERS_ENTITY_ID_VARIABLE, srv.rst.Fields(USERS_ENTITY_ID_VARIABLE).Value)
                tmpHT.Add(USERS_CREDENTIAL_LEVEL_VARIABLE, srv.rst.Fields(USERS_CREDENTIAL_LEVEL_VARIABLE).Value)
                tmpHT.Add(USERS_CREDENTIAL_TYPE_VARIABLE, srv.rst.Fields(USERS_CREDENTIAL_TYPE_VARIABLE).Value)
                dict.Add(srv.rst.Fields(USERS_ID_VARIABLE).Value, tmpHT)
                srv.rst.MoveNext()

            Loop
        End If
        srv.rst.Close()
        srv = Nothing
        Return dict



    End Function




End Class
