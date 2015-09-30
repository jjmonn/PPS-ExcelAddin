Imports System.Collections
Imports System.Collections.Generic


' Client.vb
'
' CRUD for clients table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 23/07/2015
' Last modified: 04/09/2015



Friend Class Client : Inherits SuperAxisCRUD


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Shadows Event ObjectInitialized()
    'Public Shadows Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    'Public Shadows Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    'Public Shadows Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    'Public Shadows Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_CLIENT_ANSWER, AddressOf SMSG_READ_CLIENT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_CLIENT_ANSWER, AddressOf SMSG_DELETE_CLIENT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_CLIENT_ANSWER, AddressOf SMSG_LIST_CLIENT_ANSWER)
        state_flag = False

    End Sub

    Private Sub SMSG_LIST_CLIENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetAxisHTFromPacket(packet, tmp_ht)
                Axis_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Overrides Sub CMSG_CREATE_AXIS(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_CLIENT_ANSWER, AddressOf SMSG_CREATE_CLIENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_CLIENT, UShort))
        WriteAxisPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_CLIENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            MyBase.OnCreate(True, packet.ReadUint32())
        Else
            MyBase.OnCreate(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_CLIENT_ANSWER, AddressOf SMSG_CREATE_CLIENT_ANSWER)

    End Sub

    Friend Sub CMSG_READ_CLIENT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_CLIENT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_CLIENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim ht As New Hashtable
            GetAxisHTFromPacket(packet, ht)
            Axis_hash(CInt(ht(ID_VARIABLE))) = ht
            MyBase.OnRead(True, ht)
        Else
            MyBase.OnRead(False, Nothing)
        End If

    End Sub

    Friend Overrides Sub CMSG_UPDATE_AXIS(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_CLIENT_ANSWER, AddressOf SMSG_UPDATE_CLIENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_CLIENT, UShort))
        WriteAxisPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_CLIENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            MyBase.OnUpdate(True, packet.ReadUint32())
        Else
            MyBase.OnUpdate(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_CLIENT_ANSWER, AddressOf SMSG_UPDATE_CLIENT_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_AXIS_LIST(ByRef p_clients As Hashtable)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_CLIENT_LIST_ANSWER, AddressOf SMSG_UPDATE_CLIENT_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_CLIENT_LIST, UShort))

        packet.WriteInt32(p_clients.Count())
        For Each attributes As Hashtable In p_clients.Values
            WriteAxisPacket(packet, attributes)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_UPDATE_CLIENT_LIST_ANSWER(packet As ByteBuffer)

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

            MyBase.OnUpdateList(True, resultList)
        Else
            MyBase.OnUpdateList(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_CLIENT_LIST_ANSWER, AddressOf SMSG_UPDATE_CLIENT_LIST_ANSWER)

    End Sub

    Friend Overrides Sub CMSG_DELETE_AXIS(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_CLIENT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_CLIENT_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            Axis_hash.Remove(CInt(id))
            MyBase.OnDelete(True, id)
        Else
            MyBase.OnDelete(False, 0)
        End If

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_CLIENT_ANSWER, AddressOf SMSG_READ_CLIENT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_CLIENT_ANSWER, AddressOf SMSG_DELETE_CLIENT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_CLIENT_ANSWER, AddressOf SMSG_LIST_CLIENT_ANSWER)
        MyBase.Finalize()

    End Sub



End Class
