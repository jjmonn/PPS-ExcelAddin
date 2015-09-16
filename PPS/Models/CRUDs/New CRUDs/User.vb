Imports System.Collections
Imports System.Collections.Generic

Friend Class User


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend userHT As New Hashtable
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event ReadEvent(ByRef status As Boolean, ByRef attributes As Hashtable)

    ' Events
    Public Shadows Event ObjectInitialized(ByRef status As Boolean)


#End Region

#Region "Init"

    Friend Sub New()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_USERS, AddressOf SMSG_LIST_USERS)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_USER_ANSWER, AddressOf SMSG_READ_USER_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_USER_ANSWER, AddressOf SMSG_DELETE_USER_ANSWER)
        state_flag = False

    End Sub

    Private Sub SMSG_LIST_USERS(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetUserHTFromPacket(packet, tmp_ht)
                userHT(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
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

    Private Shared Sub GetUserHTFromPacket(ByRef packet As ByteBuffer, ByRef user_ht As Hashtable)

        user_ht(ID_VARIABLE) = packet.ReadInt32()
        user_ht(NAME_VARIABLE) = packet.ReadString()
        user_ht(GROUP_ID_VARIABLE) = packet.ReadUint32()
        user_ht(PASSWORD_VARIABLE) = "*****"

    End Sub

#End Region

#Region "CRUD"

    Friend Sub CMSG_CREATE_USER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_USER_ANSWER, AddressOf SMSG_CREATE_USER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_USER, UShort))
        packet.WriteUint32(attributes(GROUP_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteString(attributes(PASSWORD_VARIABLE))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_USER_ANSWER(packet As ByteBuffer)

        RaiseEvent CreationEvent(packet.GetError() = 0, packet.ReadUint32())
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_USER_ANSWER, AddressOf SMSG_CREATE_USER_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_USER(ByRef attributes As Hashtable)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_USER_ANSWER, AddressOf SMSG_UPDATE_USER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_USER, UShort))
        packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteInt32(attributes(GROUP_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteString(attributes(PASSWORD_VARIABLE))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_UPDATE_USER_ANSWER(packet As ByteBuffer)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_USER_ANSWER, AddressOf SMSG_UPDATE_USER_ANSWER)
        RaiseEvent UpdateEvent(packet.GetError() = 0, packet.ReadUint32())
    End Sub

    Friend Sub CMSG_DELETE_USER(ByRef id As Int32)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_USER, UShort))
        packet.WriteInt32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_DELETE_USER_ANSWER(packet As ByteBuffer)
        RaiseEvent DeleteEvent(packet.GetError() = 0, packet.ReadUint32())
    End Sub

    Friend Sub CMSG_READ_USER(ByRef id As Int32)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_USER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_READ_USER_ANSWER(packet As ByteBuffer)
        If (packet.GetError() <> 0) Then
            RaiseEvent ReadEvent(False, Nothing)
            Exit Sub
        End If

        Dim ht As New Hashtable
        GetUserHTFromPacket(packet, ht)
        userHT(CInt(ht(ID_VARIABLE))) = ht
        RaiseEvent ReadEvent(True, ht)
    End Sub

#End Region

    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_USERS, AddressOf SMSG_LIST_USERS)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_USER_ANSWER, AddressOf SMSG_READ_USER_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_USER_ANSWER, AddressOf SMSG_DELETE_USER_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
