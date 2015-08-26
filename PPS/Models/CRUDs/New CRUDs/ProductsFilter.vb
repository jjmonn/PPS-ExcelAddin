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
' Last modified: 26/08/2015



Friend Class ProductsFilter

#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean = False
    Friend productsFiltersHash As New Dictionary(Of Int32, Dictionary(Of Int32, Int32))
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event ProductFilterCreationEvent(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event ProductFilterUpdateEvent(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event ProductFilterDeleteEvent(ByRef status As Boolean, ByRef productId As UInt32, ByRef filterId As UInt32)

#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_PRODUCT_FILTER_ANSWER, AddressOf SMSG_READ_PRODUCT_FILTER_ANSWER)

        LoadProductFiltersTable()

    End Sub

    Friend Sub LoadProductFiltersTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_PRODUCT_FILTER_ANSWER, AddressOf SMSG_LIST_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_PRODUCTS_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetProductFilterHTFromPacket(packet, tmp_ht)

                AddRecordToproductsFiltersHash(tmp_ht(PRODUCT_ID_VARIABLE), _
                                                tmp_ht(FILTER_ID_VARIABLE), _
                                                tmp_ht(FILTER_VALUE_ID_VARIABLE))
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_PRODUCT_FILTER_ANSWER, AddressOf SMSG_LIST_PRODUCT_FILTER_ANSWER)

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_PRODUCT_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_CREATE_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_PRODUCTS_FILTER, UShort))
        WriteProductFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim tmp_ht As New Hashtable
            GetProductFilterHTFromPacket(packet, tmp_ht)
            RaiseEvent ProductFilterCreationEvent(True, tmp_ht)
        Else
            RaiseEvent ProductFilterCreationEvent(False, Nothing)
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
            GetProductFilterHTFromPacket(packet, ht)
            AddRecordToproductsFiltersHash(ht(PRODUCT_ID_VARIABLE), _
                                           ht(FILTER_ID_VARIABLE), _
                                           ht(FILTER_VALUE_ID_VARIABLE))
        Else
        End If

    End Sub

    Friend Sub CMSG_UPDATE_PRODUCT_FILTER(ByRef productId As Int32, _
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
            Dim ht As New Hashtable
            GetProductFilterHTFromPacket(packet, ht)
            RaiseEvent ProductFilterUpdateEvent(True, ht)
        Else
            RaiseEvent ProductFilterUpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_PRODUCT_FILTER(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_DELETE_PRODUCT_FILTER_ANSWER)
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
            RaiseEvent ProductFilterDeleteEvent(True, productId, filterId)
        Else
            RaiseEvent ProductFilterDeleteEvent(False, 0, 0)
        End If

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetFilteredProductIDs(ByRef filter_id As UInt32, _
                                         ByRef filter_value_id As UInt32) As List(Of UInt32)

        Dim products_list As New List(Of UInt32)
        For Each productId As Int32 In productsFiltersHash.Keys
            If productsFiltersHash(productId)(filter_id) = filter_value_id Then
                products_list.Add(productId)
            End If
        Next
        Return products_list

    End Function

#End Region


#Region "Utilities"

    Private Sub AddRecordToproductsFiltersHash(ByRef productId As Int32, _
                                            ByRef filterId As Int32, _
                                            ByRef filterValueId As Int32)

        If productsFiltersHash.ContainsKey(productId) Then
            If productsFiltersHash(productId).ContainsKey(filterId) Then
                productsFiltersHash(productId)(filterId) = filterValueId
            Else
                productsFiltersHash(productId).Add(filterId, filterValueId)
            End If
        Else
            productsFiltersHash.Add(productId, New Dictionary(Of Int32, Int32))
            productsFiltersHash(productId).Add(filterId, filterValueId)
        End If

    End Sub

    Private Sub RemoveRecordFromFiltersHash(ByRef productId As Int32, _
                                            ByRef filterId As Int32)

        On Error Resume Next
        productsFiltersHash(productId).Remove(filterId)

    End Sub

    Friend Shared Sub GetProductFilterHTFromPacket(ByRef packet As ByteBuffer, _
                                                  ByRef productFilter_ht As Hashtable)

        productFilter_ht(PRODUCT_ID_VARIABLE) = packet.ReadUint32()
        productFilter_ht(FILTER_ID_VARIABLE) = packet.ReadUint32()
        productFilter_ht(FILTER_VALUE_ID_VARIABLE) = packet.ReadUint32()

    End Sub

    Private Sub WriteProductFilterPacket(ByRef packet As ByteBuffer, _
                                        ByRef attributes As Hashtable)

        packet.WriteUint32(attributes(PRODUCT_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_VALUE_ID_VARIABLE))

    End Sub


#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_PRODUCT_FILTER_ANSWER, AddressOf SMSG_READ_PRODUCT_FILTER_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_DELETE_PRODUCT_FILTER_ANSWER)
        MyBase.Finalize()

    End Sub



End Class
