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
' Last modified: 23/07/2015



Friend Class ProductsFilter

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend productsFilters_list As New List(Of Hashtable)
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ProductFilterCreationEvent(ByRef attributes As Hashtable)
    Public Shared Event ProductFilterRead(ByRef attributes As Hashtable)
    Public Event ProductFilterUpdateEvent()
    Public Event ProductFilterDeleteEvent(ByRef id As UInt32)

#End Region


#Region "Init"

    Friend Sub New()

        LoadProductFiltersTable()
        Dim time_stamp = Timer
        Do
            If Timer - time_stamp > GlobalVariables.timeOut Then
                state_flag = False
                Exit Do
            End If
        Loop While server_response_flag = True
        state_flag = True

    End Sub

    Friend Sub LoadProductFiltersTable()

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_LIST_PRODUCT_FILTER_ANSWER, AddressOf SMSG_LIST_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_LIST_PRODUCTS_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        For i As Int32 = 0 To packet.ReadInt32()
            Dim tmp_ht As New Hashtable
            GetProductFilterHTFromPacket(packet, tmp_ht)
            productsFilters_list(tmp_ht(ID_VARIABLE)) = tmp_ht
        Next
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_LIST_PRODUCT_FILTER_ANSWER, AddressOf SMSG_LIST_PRODUCT_FILTER_ANSWER)
        server_response_flag = True

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_PRODUCT_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_CREATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_CREATE_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_CREATE_PRODUCTS_FILTER, UShort))
        WriteProductFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        MsgBox(packet.ReadString())
        Dim tmp_ht As New Hashtable
        GetProductFilterHTFromPacket(packet, tmp_ht)
        productsFilters_list.Add(tmp_ht)
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_CREATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_CREATE_PRODUCT_FILTER_ANSWER)
        RaiseEvent ProductFilterCreationEvent(tmp_ht)

    End Sub

    Friend Shared Sub CMSG_READ_PRODUCT_FILTER(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_READ_PRODUCT_FILTER_ANSWER, AddressOf SMSG_READ_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_CREATE_PRODUCTS_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetProductFilterHTFromPacket(packet, ht)
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_CREATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_READ_PRODUCT_FILTER_ANSWER)
        RaiseEvent ProductFilterRead(ht)

    End Sub

    Friend Sub CMSG_UPDATE_PRODUCT_FILTER(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = productsFilters_list(id).clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_UPDATE_PRODUCTS_FILTER, UShort))
        WriteProductFilterPacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_PRODUCT_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_UPDATE_PRODUCTS_FILTER, UShort))
        WriteProductFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_PRODUCT_FILTER_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetProductFilterHTFromPacket(packet, ht)
        productsFilters_list(ht(ID_VARIABLE)) = ht
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_UPDATE_PRODUCT_FILTER_ANSWER)
        RaiseEvent ProductFilterUpdateEvent()

    End Sub

    Friend Sub CMSG_DELETE_PRODUCT_FILTER(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_DELETE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_DELETE_PRODUCT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_DELETE_PRODUCTS_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_PRODUCT_FILTER_ANSWER()

        Dim id As UInt32
        ' get id from request_id ?
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_DELETE_PRODUCT_FILTER_ANSWER, AddressOf SMSG_DELETE_PRODUCT_FILTER_ANSWER)
        RaiseEvent ProductFilterDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetFilteredProductIDs(ByRef filter_id As UInt32, _
                                         ByRef filter_value_id As UInt32) As List(Of UInt32)

        Dim products_list As New List(Of UInt32)
        For Each ht In productsFilters_list
            If ht(FILTER_ID_VARIABLE) = filter_id _
            AndAlso ht(FILTER_VALUE_ID_VARIABLE) = filter_value_id Then
                products_list.Add(ht(PRODUCT_ID_VARIABLE))
            End If
        Next
        Return products_list

    End Function


#End Region


#Region "Utilities"

    Friend Shared Sub GetProductFilterHTFromPacket(ByRef packet As ByteBuffer, _
                                                  ByRef productFilter_ht As Hashtable)

        productFilter_ht(PRODUCT_ID_VARIABLE) = packet.ReadInt32()
        productFilter_ht(FILTER_ID_VARIABLE) = packet.ReadInt32()
        productFilter_ht(FILTER_VALUE_ID_VARIABLE) = packet.ReadInt32()

    End Sub

    Private Sub WriteProductFilterPacket(ByRef packet As ByteBuffer, _
                                        ByRef attributes As Hashtable)

        packet.WriteInt32(attributes(PRODUCT_ID_VARIABLE))
        packet.WriteInt32(attributes(FILTER_ID_VARIABLE))
        packet.WriteInt32(attributes(FILTER_VALUE_ID_VARIABLE))

    End Sub


#End Region



End Class
