' rate_version.vb
'
' CRUD for rate_versions table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 24/08/2015
' Last modified: 01/09/2015


Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Public Class RatesVersionManager : Inherits NamedCRUDManager(Of NamedHierarchyCRUDEntity)

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_RATE_VERSION
        ReadCMSG = ClientMessage.CMSG_READ_RATE_VERSION
        UpdateCMSG = ClientMessage.CMSG_UPDATE_RATE_VERSION
        UpdateListCMSG = ClientMessage.CMSG_CRUD_RATE_VERSION_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_RATE_VERSION
        ListCMSG = ClientMessage.CMSG_LIST_RATE_VERSION

        CreateSMSG = ServerMessage.SMSG_CREATE_RATE_VERSION_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_RATE_VERSION_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_RATE_VERSION_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_RATE_VERSION_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_RATE_VERSION_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_RATE_VERSION_ANSWER

        Build = AddressOf ExchangeRateVersion.BuildExchangeRateVersion

        InitCallbacks()

    End Sub

#End Region

#Region "Utilities"

    Friend Sub LoadRateVersionsTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, m_CRUDDic)

    End Sub

    Friend Sub LoadRateVersionsTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, m_CRUDDic)

    End Sub

    Friend Function GetMonthsList(ByRef versionId As UInt32) As List(Of Int32)

        Dim l_version As ExchangeRateVersion = GetValue(versionId)
        If l_version Is Nothing Then Return New List(Of Int32)

        Return Period.GetMonthlyPeriodsList(l_version.StartPeriod, l_version.NbPeriod, True)

    End Function


#End Region

End Class
