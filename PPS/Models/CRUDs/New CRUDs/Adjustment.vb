Imports System.Collections
Imports System.Collections.Generic


' Adjustment2.vb
'
' CRUD for adjustments table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 24/07/2015
' Last modified: 04/09/2015



Friend Class Adjustment : Inherits SuperAxisCRUD


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
  
    ' Events
    Public Shadows Event ObjectInitialized(ByRef status As Boolean)
   

#End Region


#Region "Init"

    Friend Sub New()
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ADJUSTMENT_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_ANSWER)
        state_flag = False

    End Sub

    Private Sub SMSG_LIST_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetAxisHTFromPacket(packet, tmp_ht)
                axis_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            RaiseEvent ObjectInitialized(True)
            state_flag = True
        Else
            RaiseEvent ObjectInitialized(False)
            state_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Overrides Sub CMSG_CREATE_AXIS(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ADJUSTMENT, UShort))
        WriteAxisPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MyBase.OnCreate(True, packet.ReadUint32())
        Else
            MyBase.OnCreate(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_ANSWER)

    End Sub

    Friend Shared Sub CMSG_READ_ADJUSTMENT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_ADJUSTMENT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAxisHTFromPacket(packet, ht)
            axis_hash(CInt(ht(ID_VARIABLE))) = ht
            MyBase.OnRead(True, ht)
        Else
            MyBase.OnRead(False, Nothing)
        End If

    End Sub

    Friend Overrides Sub CMSG_UPDATE_AXIS(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ADJUSTMENT, UShort))
        WriteAxisPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MyBase.OnUpdate(True, packet.ReadUint32())
        Else
            MyBase.OnUpdate(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_ANSWER)

    End Sub

    Friend Overrides Sub CMSG_DELETE_AXIS(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ADJUSTMENT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ADJUSTMENT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            axis_hash.Remove(CInt(id))
            MyBase.OnDelete(True, id)
        Else
            MyBase.OnDelete(False, 0)
        End If

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_ADJUSTMENT_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
