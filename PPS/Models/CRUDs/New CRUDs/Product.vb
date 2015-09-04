Imports System.Collections
Imports System.Collections.Generic


' Product2.vb
'
' CRUD for products table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 24/07/2015
' Last modified: 04/09/2015



Friend Class Product : Inherits SuperAxisCRUD


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean = False
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Shadows Event ObjectInitialized()


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_PRODUCT_ANSWER, AddressOf SMSG_READ_PRODUCT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_PRODUCT_ANSWER, AddressOf SMSG_DELETE_PRODUCT_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_PRODUCT_ANSWER, AddressOf SMSG_LIST_PRODUCT_ANSWER)

    End Sub

    Private Sub SMSG_LIST_PRODUCT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetAxisHTFromPacket(packet, tmp_ht)
                axis_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
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

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_PRODUCT_ANSWER, AddressOf SMSG_CREATE_PRODUCT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_PRODUCT, UShort))
        WriteAxisPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_PRODUCT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MyBase.OnCreate(True, packet.ReadUint32())
        Else
            MyBase.OnCreate(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_PRODUCT_ANSWER, AddressOf SMSG_CREATE_PRODUCT_ANSWER)

    End Sub

    Friend Sub CMSG_READ_PRODUCT(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_PRODUCT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_PRODUCT_ANSWER(packet As ByteBuffer)

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

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_PRODUCT_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_PRODUCT, UShort))
        WriteAxisPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_PRODUCT_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MyBase.OnUpdate(True, packet.ReadUint32())
        Else
            MyBase.OnUpdate(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_PRODUCT_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_ANSWER)

    End Sub

    Friend Overrides Sub CMSG_DELETE_AXIS(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_PRODUCT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_PRODUCT_ANSWER(packet As ByteBuffer)

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

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_PRODUCT_ANSWER, AddressOf SMSG_LIST_PRODUCT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_PRODUCT_ANSWER, AddressOf SMSG_READ_PRODUCT_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_PRODUCT_ANSWER, AddressOf SMSG_DELETE_PRODUCT_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
