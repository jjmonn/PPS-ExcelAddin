Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Friend Class GroupManager : Inherits NamedCRUDManager(Of NamedHierarchyCRUDEntity)

    Enum Permission
        NONE = 0
        ADMIN = 1
        USER = 2
    End Enum

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_GROUP
        ReadCMSG = ClientMessage.CMSG_READ_GROUP
        UpdateCMSG = ClientMessage.CMSG_UPDATE_GROUP
        DeleteCMSG = ClientMessage.CMSG_DELETE_GROUP
        ListCMSG = ClientMessage.CMSG_LIST_GROUPS

        CreateSMSG = ServerMessage.SMSG_CREATE_GROUP_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_GROUP_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_GROUP_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_GROUP_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_GROUPS

        Build = AddressOf Group.BuildGroup

        InitCallbacks()

        List()

    End Sub

    <Obsolete("Not implemented", True)> _
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub UpdateList(ByRef p_crudList As List(Of CRUDEntity))
    End Sub

#End Region

#Region "Utilities"

    Friend Sub LoadGroupTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)
        VTreeViewUtil.LoadTreeview(TV, m_CRUDDic)
    End Sub

    Friend Function GroupIsAdmin(ByRef p_groupId As UInt32) As Boolean
        Dim l_group As Group = GetValue(p_groupId)

        If l_group Is Nothing Then Return False
        Return l_group.Rights And Permission.ADMIN
    End Function

#End Region


End Class
