Imports System.Collections
Imports System.Collections.Generic



Public Class GlobalFactVersion


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean = False
    Friend globalFact_versions_hash As New Hashtable

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)
    Public Event UpdateListEvent(ByRef status As Boolean, ByRef resultList As Dictionary(Of Int32, Boolean))

#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_READ_GLOBAL_FACT_VERSION_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_DELETE_GLOBAL_FACT_VERSION_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_LIST_GLOBAL_FACT_VERSION_ANSWER)

    End Sub

    Private Sub SMSG_LIST_GLOBAL_FACT_VERSION_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim nb_globalFact_versions = packet.ReadInt32()
            For i As Int32 = 1 To nb_globalFact_versions
                Dim tmp_ht As New Hashtable
                GetGlobalFactVersionHTFromPacket(packet, tmp_ht)
                globalFact_versions_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_GLOBAL_FACT_VERSION(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_CREATE_GLOBAL_FACT_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_GLOBAL_FACT_VERSION, UShort))
        WriteGlobalFactVersionPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_GLOBAL_FACT_VERSION_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            RaiseEvent CreationEvent(True, packet.ReadUint32())
        Else
            RaiseEvent CreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_CREATE_GLOBAL_FACT_VERSION_ANSWER)

    End Sub

    Friend Sub CMSG_READ_GLOBAL_FACT_VERSION(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_GLOBAL_FACT_VERSION, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_GLOBAL_FACT_VERSION_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim ht As New Hashtable
            GetGlobalFactVersionHTFromPacket(packet, ht)
            globalFact_versions_hash(CInt(ht(ID_VARIABLE))) = ht
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_GLOBAL_FACT_VERSION(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_UPDATE_GLOBAL_FACT_VERSION_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_GLOBAL_FACT_VERSION, UShort))
        WriteGlobalFactVersionPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_GLOBAL_FACT_VERSION_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            RaiseEvent UpdateEvent(True, packet.ReadUint32())
        Else
            RaiseEvent UpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_UPDATE_GLOBAL_FACT_VERSION_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_GLOBAL_FACT_VERSION_LIST(ByRef p_currencies As Hashtable)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_GLOBAL_FACT_VERSION_LIST_ANSWER, AddressOf SMSG_UPDATE_GLOBAL_FACT_VERSION_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_GLOBAL_FACT_VERSION_LIST, UShort))

        packet.WriteInt32(p_currencies.Count())
        For Each attributes As Hashtable In p_currencies.Values
            WriteGlobalFactVersionPacket(packet, attributes)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_UPDATE_GLOBAL_FACT_VERSION_LIST_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim resultList As New Dictionary(Of Int32, Boolean)
            Dim nbResult As Int32 = packet.ReadInt32()

            For i As Int32 = 1 To nbResult
                Dim id As Int32 = packet.ReadInt32()
                If (resultList.ContainsKey(id)) Then
                    resultList(id) = packet.ReadBool()
                Else
                    resultList.Add(id, packet.ReadBool)
                End If
                packet.ReadString()
            Next

            RaiseEvent UpdateListEvent(False, resultList)
        Else
            RaiseEvent UpdateListEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_GLOBAL_FACT_VERSION_LIST_ANSWER, AddressOf SMSG_UPDATE_GLOBAL_FACT_VERSION_LIST_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_GLOBAL_FACT_VERSION(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_GLOBAL_FACT_VERSION, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_GLOBAL_FACT_VERSION_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim id As UInt32 = packet.ReadUint32
            globalFact_versions_hash.Remove(CInt(id))
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If

    End Sub

#End Region

#Region "Utilities"

    Friend Shared Sub GetGlobalFactVersionHTFromPacket(ByRef packet As ByteBuffer, ByRef globalFact_version_ht As Hashtable)

        globalFact_version_ht(ID_VARIABLE) = packet.ReadUint32()
        globalFact_version_ht(PARENT_ID_VARIABLE) = packet.ReadUint32()
        globalFact_version_ht(NAME_VARIABLE) = packet.ReadString()
        globalFact_version_ht(IS_FOLDER_VARIABLE) = packet.ReadBool()
        globalFact_version_ht(ITEMS_POSITIONS) = packet.ReadUint32()
        globalFact_version_ht(VERSIONS_START_PERIOD_VAR) = packet.ReadUint32()
        globalFact_version_ht(VERSIONS_NB_PERIODS_VAR) = packet.ReadUint16()
        If globalFact_version_ht(IS_FOLDER_VARIABLE) = True Then
            globalFact_version_ht(IMAGE_VARIABLE) = 1
        Else
            globalFact_version_ht(IMAGE_VARIABLE) = 0
        End If

    End Sub

    Private Sub WriteGlobalFactVersionPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteUint32(attributes(ID_VARIABLE))
        packet.WriteUint32(attributes(PARENT_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteBool(attributes(IS_FOLDER_VARIABLE))
        packet.WriteUint32(attributes(ITEMS_POSITIONS))
        packet.WriteUint32(attributes(VERSIONS_START_PERIOD_VAR))
        packet.WriteUint16(attributes(VERSIONS_NB_PERIODS_VAR))

    End Sub

    Friend Sub LoadGlobalFactVersionsTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, globalFact_versions_hash)

    End Sub

    Friend Sub LoadGlobalFactVersionsTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, globalFact_versions_hash)

    End Sub

    Friend Function GetGlobalFactVersionsIDFromName(ByRef globalFact_versionName As String) As Int32

        For Each globalFact_versionId In globalFact_versions_hash.Keys
            If globalFact_versions_hash(globalFact_versionId)(NAME_VARIABLE) = globalFact_versionName Then
                Return globalFact_versionId
            End If
        Next
        Return 0

    End Function

    Friend Function GetMonthsList(ByRef versionId As Int32) As List(Of Int32)

        Dim periodList As New List(Of Int32)
        For Each monthId As Int32 In Period.GetMonthsList(globalFact_versions_hash(versionId)(VERSIONS_START_PERIOD_VAR), _
                                      globalFact_versions_hash(versionId)(VERSIONS_NB_PERIODS_VAR))
            periodList.Add(monthId)
        Next
        Return periodList

    End Function

    Friend Sub LoadVersionsTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, globalFact_versions_hash)

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_LIST_GLOBAL_FACT_VERSION_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_READ_GLOBAL_FACT_VERSION_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_GLOBAL_FACT_VERSION_ANSWER, AddressOf SMSG_DELETE_GLOBAL_FACT_VERSION_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
