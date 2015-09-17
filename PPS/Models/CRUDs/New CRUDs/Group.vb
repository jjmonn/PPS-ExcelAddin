Imports System.Collections
Imports System.Collections.Generic

Friend Class Group


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend groupDic As New Dictionary(Of Int32, Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event ReadEvent(ByRef status As Boolean, ByRef id As Hashtable)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)

    ' Events
    Public Shadows Event ObjectInitialized(ByRef status As Boolean)


#End Region

#Region "Init"

    Friend Sub New()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_GROUPS, AddressOf SMSG_LIST_GROUPS)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_GROUP_ANSWER, AddressOf SMSG_READ_GROUP_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_GROUP_ANSWER, AddressOf SMSG_DELETE_GROUP_ANSWER)
        state_flag = False

    End Sub

    Private Sub SMSG_LIST_GROUPS(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetGroupHTFromPacket(packet, tmp_ht)
                groupDic(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
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

    Private Shared Sub GetGroupHTFromPacket(ByRef packet As ByteBuffer, ByRef group_ht As Hashtable)

        group_ht(ID_VARIABLE) = packet.ReadInt32()
        group_ht(PARENT_ID_VARIABLE) = packet.ReadUint32()
        group_ht(NAME_VARIABLE) = packet.ReadString()
        group_ht(RIGHTS_VARIABLE) = packet.ReadUint64()

    End Sub

#End Region

#Region "Management"

    Friend Sub CMSG_CREATE_GROUP(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_GROUP_ANSWER, AddressOf SMSG_CREATE_GROUP_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_GROUP, UShort))
        packet.WriteUint32(attributes(PARENT_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteUint64(attributes(RIGHTS_VARIABLE))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_GROUP_ANSWER(packet As ByteBuffer)

        RaiseEvent CreationEvent(packet.GetError() = 0, packet.ReadUint32())
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_GROUP_ANSWER, AddressOf SMSG_CREATE_GROUP_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_GROUP(ByRef attributes As Hashtable)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_GROUP_ANSWER, AddressOf SMSG_UPDATE_GROUP_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_GROUP, UShort))
        packet.WriteInt32(attributes(GROUP_ID_VARIABLE))
        packet.WriteInt32(attributes(PARENT_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteUint64(attributes(RIGHTS_VARIABLE))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_UPDATE_GROUP_ANSWER(packet As ByteBuffer)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_GROUP_ANSWER, AddressOf SMSG_UPDATE_GROUP_ANSWER)
        RaiseEvent UpdateEvent(packet.GetError() = 0, packet.ReadUint32())
    End Sub

    Friend Sub CMSG_DELETE_GROUP(ByRef id As Int32)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_GROUP, UShort))
        packet.WriteInt32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_DELETE_GROUP_ANSWER(packet As ByteBuffer)
        RaiseEvent UpdateEvent(packet.GetError() = 0, packet.ReadUint32())
    End Sub

    Friend Sub CMSG_READ_GROUP(ByRef id As Int32)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_GROUP, UShort))
        packet.WriteInt32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_READ_GROUP_ANSWER(packet As ByteBuffer)
        If (packet.GetError() <> 0) Then
            RaiseEvent ReadEvent(False, Nothing)
            Exit Sub
        End If

        Dim ht As New Hashtable
        GetGroupHTFromPacket(packet, ht)
        groupDic(CInt(ht(ID_VARIABLE))) = ht
        RaiseEvent ReadEvent(True, ht)
    End Sub

#End Region

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_GROUPS, AddressOf SMSG_LIST_GROUPS)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_GROUP_ANSWER, AddressOf SMSG_READ_GROUP_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_GROUP_ANSWER, AddressOf SMSG_DELETE_GROUP_ANSWER)

        MyBase.Finalize()

    End Sub


End Class
