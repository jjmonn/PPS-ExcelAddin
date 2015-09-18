Imports System.Collections
Imports System.Collections.Generic

Friend Class GroupAllowedEntity


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend groupAllowedEntityDic As New Dictionary(Of Int32, List(Of Int32))
    Public Event UpdateEvent(ByRef status As Boolean, ByRef groupId As Int32, ByRef entityId As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef groupId As Int32, ByRef entityId As Int32)
    Public Event CreateEvent(ByRef status As Boolean, ByRef groupId As Int32, ByRef entityId As Int32)
    Public Event ReadEvent(ByRef status As Boolean, ByRef groupId As Int32, ByRef entityId As Int32)

    ' Events
    Public Shadows Event ObjectInitialized(ByRef status As Boolean)


#End Region

#Region "Init"

    Friend Sub New()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_GROUP_ENTITIES_ANSWER, AddressOf SMSG_LIST_GROUP_ENTITIES_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_GET_GROUP_ENTITIES_ANSWER, AddressOf SMSG_GET_GROUP_ENTITIES_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_GROUP_ENTITY, AddressOf SMSG_READ_GROUP_ENTITY)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DEL_GROUP_ENTITY_ANSWER, AddressOf SMSG_DEL_GROUP_ENTITY_ANSWER)
        state_flag = False

    End Sub

    Private Sub SMSG_LIST_GROUP_ENTITIES_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                GetGroupAllowedEntityHTFromPacket(packet, groupAllowedEntityDic)
            Next
            RaiseEvent ObjectInitialized(True)
            state_flag = True
        Else
            RaiseEvent ObjectInitialized(False)
            state_flag = False
        End If

    End Sub

#End Region

#Region "Utilities"

    Private Shared Function GetGroupAllowedEntityHTFromPacket(ByRef packet As ByteBuffer, ByRef ht As Dictionary(Of Int32, List(Of Int32))) As Int32


        Dim list As New List(Of Int32)
        Dim groupId As Int32 = packet.ReadInt32()

        For j As Int32 = 1 To packet.ReadInt32()
            list.Add(packet.ReadInt32())
        Next
        If (ht.ContainsKey(groupId) = True) Then
            ht(groupId) = list
        Else
            ht.Add(groupId, list)
        End If
        Return (groupId)
    End Function

#End Region

#Region "Management"

    Friend Sub CMSG_GET_GROUP_ENTITIES(ByRef groupId As Int32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_GET_GROUP_ENTITIES, UShort))
        packet.WriteInt32(groupId)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_GET_GROUP_ENTITIES_ANSWER(packet As ByteBuffer)

        If (packet.GetError() <> 0) Then
            RaiseEvent UpdateEvent(False, packet.ReadUint32(), packet.ReadUint32())
        End If
        RaiseEvent UpdateEvent(True, packet.ReadUint32(), packet.ReadUint32())

    End Sub

    Friend Sub CMSG_ADD_GROUP_ENTITY(ByRef groupId As Int32, ByRef entityId As Int32)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_ADD_GROUP_ENTITY, UShort))
        packet.WriteInt32(groupId)
        packet.WriteInt32(entityId)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_ADD_GROUP_ENTITY_ANSWER(packet As ByteBuffer)
        RaiseEvent CreateEvent(packet.GetError() = 0, packet.ReadUint32(), packet.ReadUint32())
    End Sub

    Private Sub SMSG_READ_GROUP_ENTITY(packet As ByteBuffer)
        Dim groupId = packet.ReadInt32()
        Dim entityId = packet.ReadInt32()

        If (groupAllowedEntityDic(groupId).Contains(entityId) = False) Then groupAllowedEntityDic(groupId).Add(entityId)
        RaiseEvent ReadEvent(packet.GetError() = 0, groupId, entityId)
    End Sub

    Friend Sub CMSG_DEL_GROUP_ENTITY(ByRef groupId As Int32, ByRef entityId As Int32)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DEL_GROUP_ENTITY, UShort))
        packet.WriteInt32(groupId)
        packet.WriteInt32(entityId)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_DEL_GROUP_ENTITY_ANSWER(packet As ByteBuffer)
        Dim groupId = packet.ReadInt32()
        Dim entityId = packet.ReadInt32()

        If (groupAllowedEntityDic(groupId).Contains(entityId) = True) Then groupAllowedEntityDic(groupId).Remove(entityId)
        RaiseEvent DeleteEvent(packet.GetError() = 0, groupId, entityId)
    End Sub

#End Region

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_GROUP_ENTITIES_ANSWER, AddressOf SMSG_LIST_GROUP_ENTITIES_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_GET_GROUP_ENTITIES_ANSWER, AddressOf SMSG_GET_GROUP_ENTITIES_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DEL_GROUP_ENTITY_ANSWER, AddressOf SMSG_DEL_GROUP_ENTITY_ANSWER)

        MyBase.Finalize()

    End Sub


End Class
