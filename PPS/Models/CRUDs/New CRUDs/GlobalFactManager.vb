Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports CRUD


Friend Class GlobalFactManager : Inherits NamedCRUDManager

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_GLOBAL_FACT
        ReadCMSG = ClientMessage.CMSG_READ_GLOBAL_FACT
        UpdateCMSG = ClientMessage.CMSG_UPDATE_GLOBAL_FACT
        UpdateListCMSG = ClientMessage.CMSG_UPDATE_GLOBAL_FACT_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_GLOBAL_FACT
        ListCMSG = ClientMessage.CMSG_LIST_GLOBAL_FACT

        CreateSMSG = ServerMessage.SMSG_CREATE_GLOBAL_FACT_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_GLOBAL_FACT_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_GLOBAL_FACT_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_UPDATE_GLOBAL_FACT_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_GLOBAL_FACT_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_GLOBAL_FACT_ANSWER

        Build = AddressOf GlobalFact.BuildGlobalFact

        InitCallbacks()

    End Sub

#End Region

#Region "Utilities"

    Friend Function GetMonthsList(ByRef p_versionId As Int32) As List(Of Int32)

        Dim periodList As New List(Of Int32)
        Dim version As GlobalFactVersion = GlobalVariables.GlobalFactsVersions.GetValue(p_versionId)
        If version Is Nothing Then Return periodList

        For Each monthId As Int32 In Period.GetMonthsList(version.StartPeriod, version.NbPeriod)
            periodList.Add(monthId)
        Next
        Return periodList

    End Function

    Friend Sub LoadGlobalFactsTV(ByRef p_tv As TreeView)
        TreeViewsUtilities.LoadTreeview(p_tv, m_CRUDDic)
    End Sub

#End Region

End Class
