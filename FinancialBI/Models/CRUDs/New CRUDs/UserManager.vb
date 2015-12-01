﻿Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Friend Class UserManager : Inherits NamedCRUDManager(Of NamedCRUDEntity)

#Region "Instance variables"

    Friend currentUserName As String

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_USER
        ReadCMSG = ClientMessage.CMSG_READ_USER
        UpdateCMSG = ClientMessage.CMSG_UPDATE_USER
        DeleteCMSG = ClientMessage.CMSG_DELETE_USER
        ListCMSG = ClientMessage.CMSG_LIST_USERS

        CreateSMSG = ServerMessage.SMSG_CREATE_USER_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_USER_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_USER_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_USER_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_USERS

        Build = AddressOf User.BuildUser

        InitCallbacks()

    End Sub

    <Obsolete("Not implemented", True)> _
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub UpdateList(ByRef p_crudList As List(Of CRUDEntity))
    End Sub

#End Region

#Region "Utilities"

    Friend Function GetCurrentUser() As User
        Return m_CRUDDic(currentUserName)
    End Function

    Friend Function CurrentUserIsAdmin() As Boolean
        Dim l_user As User = GetCurrentUser()
        If l_user Is Nothing Then Return False

        Return GlobalVariables.Groups.GroupIsAdmin(l_user.GroupId)
    End Function

#End Region

End Class