Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD

Friend Class FModelingAccountManager : Inherits NamedCRUDManager

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_FMODELING_ACCOUNT
        ReadCMSG = ClientMessage.CMSG_READ_FMODELING_ACCOUNT
        UpdateCMSG = ClientMessage.CMSG_UPDATE_FMODELING_ACCOUNT
        UpdateListCMSG = ClientMessage.CMSG_UPDATE_FMODELING_ACCOUNT
        DeleteCMSG = ClientMessage.CMSG_DELETE_FMODELING_ACCOUNT
        ListCMSG = ClientMessage.CMSG_LIST_FMODELING_ACCOUNT

        CreateSMSG = ServerMessage.SMSG_CREATE_FMODELING_ACCOUNT_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_FMODELING_ACCOUNT_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_FMODELING_ACCOUNT_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_UPDATE_FMODELING_ACCOUNT_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_FMODELING_ACCOUNT_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_FMODELING_ACCOUNT_ANSWER

        Build = AddressOf Filter.BuildFilter

        InitCallbacks()

    End Sub

#End Region

#Region "Utilities"

    Friend Sub LoadTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, m_CRUDDic)

    End Sub

    Friend Sub LoadTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, m_CRUDDic)

    End Sub

#End Region

End Class
