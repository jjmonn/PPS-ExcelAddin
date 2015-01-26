' cCredentials.vb
'
' Sets the user credential from DB
'
'
' to do:
'   
'
'
' Known bugs:
'
'
' Auhtor: Julien Monnereau
' Last modified: 09/12/2014


Imports System.Collections.Generic



Friend Class SQLCredentials


    Protected Friend Shared Function SetGLOBALUserCredential() As Boolean

        Dim srv As New ModelServer
        Dim strSql As String = "SELECT " + USERS_CREDENTIAL_LEVEL_VARIABLE + " FROM " + CONFIG_DATABASE + "." + USERS_TABLE _
                             + " WHERE " + USERS_ID_VARIABLE + "='" + Current_User_ID + "'"
        srv.openRstSQL(strSql, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False Then
            User_Credential = srv.rst.Fields(USERS_CREDENTIAL_LEVEL_VARIABLE).Value
            srv.rst.Close()
        Else
            MsgBox("The current user account: " + Current_User_ID + " is not registered." + Chr(13) + Chr(13) + _
                   "Please contact your Database manager." + Chr(13) + "(Error PP007)")
        End If
        If User_Credential <> -1 Then
            SetEntitiesViewGlobalVariable()
            Return True
        End If
        Return False

    End Function

    Protected Friend Shared Sub SetEntitiesViewGlobalVariable()

        Entities_View = ENTITIES_TABLE & User_Credential

    End Sub

    Protected Friend Shared Sub UnvalidateCredentialLevelInUsers(ByRef credential_level As Int32)

        Dim usersDic = UsersMapping.GetUsersDictionary(USERS_ID_VARIABLE, USERS_CREDENTIAL_LEVEL_VARIABLE)
        For Each user_id In usersDic.Keys
            If usersDic(user_id) = credential_level Then
                SetUserCredentialLevelToUnvalid(user_id)
            End If
        Next

    End Sub

    Protected Friend Shared Sub SetUserCredentialLevelToUnvalid(ByRef user_id As String)

        Dim srv As New ModelServer
        srv.sqlQuery("UPDATE " & CONFIG_DATABASE + "." + USERS_TABLE & _
                     " SET " & USERS_CREDENTIAL_LEVEL_VARIABLE & "=" & -1 & "," & _
                     USERS_ENTITY_ID_VARIABLE & "=''" & _
                     " WHERE " & USERS_ID_VARIABLE & "='" & user_id & "'")

    End Sub


End Class
