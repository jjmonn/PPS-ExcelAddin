Imports System.Collections
Imports System.Collections.Generic


' ProductFilter.vb
'
' CRUD for productsFilters table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 23/07/2015
' Last modified: 04/09/2015



Friend Class ProductsFilter : Inherits SuperAxisFilterCRUD


#Region "Instance variables"

    ' Events
    Public Event ObjectInitialized()
    Public Event CreationEvent(ByRef status As Boolean, ByRef product_id As Int32, ByRef filter_id As Int32, filter_value_id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef product_id As Int32, filter_id As Int32)

#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_PRODUCT_FILTER_ANSWER, AddressOf SMSG_READ_PRODUCT_FILTER_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_PRODUCT_FILTER_ANSWER, AddressOf SMSG_LIST_PRODUCT_FILTER_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_DELETE_PRODUCT_FILTER_ANSWER)

    End Sub

    Private Sub SMSG_LIST_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetAxisFilterHTFromPacket(packet, tmp_ht)
                AddRecordToaxisFiltersHash(tmp_ht(AXIS_ID_VARIABLE), _
                                                tmp_ht(FILTER_ID_VARIABLE), _
                                                tmp_ht(FILTER_VALUE_ID_VARIABLE))
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_PRODUCT_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_CREATE_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_PRODUCTS_FILTER, UShort))
        WriteAxisFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim product_id As Int32 = packet.ReadUint32()
            Dim filter_id As Int32 = packet.ReadUint32()
            Dim filter_value_id As Int32 = packet.ReadUint32()
            RaiseEvent CreationEvent(True, product_id, filter_id, filter_value_id)
        Else
            RaiseEvent CreationEvent(False, 0, 0, 0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_CREATE_PRODUCT_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_READ_PRODUCT_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_PRODUCTS_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAxisFilterHTFromPacket(packet, ht)
            AddRecordToaxisFiltersHash(ht(AXIS_ID_VARIABLE), _
                                           ht(FILTER_ID_VARIABLE), _
                                           ht(FILTER_VALUE_ID_VARIABLE))
            MyBase.OnRead(True, ht)
        Else
            MyBase.OnRead(False, Nothing)
        End If

    End Sub

    Friend Overrides Sub CMSG_UPDATE_AXIS_FILTER(ByRef productId As Int32, _
                                                 ByRef filterId As Int32, _
                                                 ByRef filterValueId As Int32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_PRODUCTS_FILTER, UShort))
        packet.WriteUint32(productId)
        packet.WriteUint32(filterId)
        packet.WriteUint32(filterValueId)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim product_id As Int32 = packet.ReadUint32()
            Dim filter_id As Int32 = packet.ReadUint32()
            Dim filter_value_id As Int32 = packet.ReadUint32()
            MyBase.OnUpdate(True, product_id, filter_id, filter_value_id)
        Else
            MyBase.OnUpdate(False, 0, 0, 0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_PRODUCT_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_PRODUCTS_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim productId As UInt32 = packet.ReadInt32
            Dim filterId As UInt32 = packet.ReadInt32
            RemoveRecordFromFiltersHash(productId, filterId)
            RaiseEvent DeleteEvent(True, productId, filterId)
        Else
            RaiseEvent DeleteEvent(False, 0, 0)
        End If

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_PRODUCT_FILTER_ANSWER, AddressOf SMSG_LIST_PRODUCT_FILTER_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_PRODUCT_FILTER_ANSWER, AddressOf SMSG_READ_PRODUCT_FILTER_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_DELETE_PRODUCT_FILTER_ANSWER)
        MyBase.Finalize()

    End Sub



End Class
