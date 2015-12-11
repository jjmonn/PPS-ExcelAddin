Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Public Class GlobalFactVersionManager : Inherits NamedCRUDManager(Of NamedHierarchyCRUDEntity)

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_GLOBAL_FACT_VERSION
        ReadCMSG = ClientMessage.CMSG_READ_GLOBAL_FACT_VERSION
        UpdateCMSG = ClientMessage.CMSG_UPDATE_GLOBAL_FACT_VERSION
        UpdateListCMSG = ClientMessage.CMSG_CRUD_GLOBAL_FACT_VERSION_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_GLOBAL_FACT_VERSION
        ListCMSG = ClientMessage.CMSG_LIST_GLOBAL_FACT_VERSION

        CreateSMSG = ServerMessage.SMSG_CREATE_GLOBAL_FACT_VERSION_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_GLOBAL_FACT_VERSION_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_GLOBAL_FACT_VERSION_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_GLOBAL_FACT_VERSION_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_GLOBAL_FACT_VERSION_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_GLOBAL_FACT_VERSION_ANSWER

        Build = AddressOf GlobalFactVersion.BuildGlobalFactVersion

        InitCallbacks()
    End Sub

#End Region

#Region "Utilities"

    Friend Function GetMonthsList(ByRef versionId As UInt32) As List(Of Int32)

        Dim periodList As New List(Of Int32)
        Dim version As GlobalFactVersion = m_CRUDDic(versionId)
        If version Is Nothing Then Return periodList

        Dim monthList As Int32() = Period.GetMonthsList(version.StartPeriod, version.NbPeriod, CRUD.TimeConfig.MONTHS)
        For Each monthId As Int32 In monthList
            periodList.Add(monthId)
        Next
        Return periodList

    End Function

    Friend Sub LoadVersionsTV(ByRef p_treeview As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(p_treeview, m_CRUDDic)
        VTreeViewUtil.LoadTreeviewIcons(p_treeview, m_CRUDDic)

    End Sub

#End Region

End Class
