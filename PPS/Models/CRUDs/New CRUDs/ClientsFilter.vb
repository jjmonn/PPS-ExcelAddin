Imports System.Collections
Imports System.Collections.Generic


' ClientFilter.vb
'
' CRUD for clientsFilters table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 23/07/2015
' Last modified: 02/09/2015



Friend Class ClientsFilter


#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend clientsFiltersHash As New Dictionary(Of Int32, Dictionary(Of Int32, Int32))
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef client_id As Int32, ByRef filter_id As Int32, filter_value_id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef client_id As Int32, ByRef filter_id As Int32, filter_value_id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef client_id As Int32, filter_id As Int32)

#End Region


#Region "Init"

  Friend Sub New()

    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_CLIENT_FILTER_ANSWER, AddressOf SMSG_LIST_CLIENT_FILTER_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_CLIENT_FILTER_ANSWER, AddressOf SMSG_READ_CLIENT_FILTER_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_CLIENT_FILTER_ANSWER, AddressOf SMSG_DELETE_CLIENT_FILTER_ANSWER)
    state_flag = False

  End Sub

    Private Sub SMSG_LIST_CLIENT_FILTER_ANSWER(packet As ByteBuffer)

        clientsFiltersHash.Clear()
        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetClientFilterHTFromPacket(packet, tmp_ht)
                AddRecordToClientsFiltersHash(tmp_ht(CLIENT_ID_VARIABLE), _
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

    Friend Sub CMSG_CREATE_CLIENT_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_CLIENT_FILTER_ANSWER, AddressOf SMSG_CREATE_CLIENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_CLIENTS_FILTER, UShort))
        WriteClientFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_CLIENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim client_id As Int32 = packet.ReadUint32()
            Dim filter_id As Int32 = packet.ReadUint32()
            Dim filter_value_id As Int32 = packet.ReadUint32()
            RaiseEvent CreationEvent(True, client_id, filter_id, filter_value_id)
        Else
            RaiseEvent CreationEvent(False, 0, 0, 0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_CLIENT_FILTER_ANSWER, AddressOf SMSG_CREATE_CLIENT_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_READ_CLIENT_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_CLIENTS_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_CLIENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetClientFilterHTFromPacket(packet, ht)
            AddRecordToClientsFiltersHash(ht(CLIENT_ID_VARIABLE), _
                                          ht(FILTER_ID_VARIABLE), _
                                          ht(FILTER_VALUE_ID_VARIABLE))
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_client_FILTER(ByRef clientId As Int32, _
                                               ByRef filterId As Int32, _
                                               ByRef filterValueId As Int32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_CLIENT_FILTER_ANSWER, AddressOf SMSG_UPDATE_CLIENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_CLIENTS_FILTER, UShort))
        packet.WriteUint32(clientId)
        packet.WriteUint32(filterId)
        packet.WriteUint32(filterValueId)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_CLIENT_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_CLIENT_FILTER_ANSWER, AddressOf SMSG_UPDATE_CLIENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_CLIENTS_FILTER, UShort))
        WriteClientFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_CLIENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim client_id As Int32 = packet.ReadUint32()
            Dim filter_id As Int32 = packet.ReadUint32()
            Dim filter_value_id As Int32 = packet.ReadUint32()
            RaiseEvent UpdateEvent(True, client_id, filter_id, filter_value_id)
        Else
            RaiseEvent UpdateEvent(False, 0, 0, 0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_CLIENT_FILTER_ANSWER, AddressOf SMSG_UPDATE_CLIENT_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_CLIENT_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_CLIENTS_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_CLIENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim clientId As UInt32 = packet.ReadInt32
            Dim filterId As UInt32 = packet.ReadInt32
            RemoveRecordFromFiltersHash(clientId, filterId)
            RaiseEvent DeleteEvent(True, clientId, filterId)
        Else
            RaiseEvent DeleteEvent(False, 0, 0)
        End If

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetFilteredClientIDs(ByRef filter_id As UInt32, _
                                         ByRef filter_value_id As UInt32) As List(Of UInt32)

        Dim clients_list As New List(Of UInt32)
        For Each clientId As Int32 In clientsFiltersHash.Keys
            If clientsFiltersHash(clientId)(filter_id) = filter_value_id Then
                clients_list.Add(clientId)
            End If
        Next
        Return clients_list

    End Function


#End Region


#Region "Utilities"

    Private Sub AddRecordToClientsFiltersHash(ByRef clientId As Int32, _
                                              ByRef filterId As Int32, _
                                              ByRef filterValueId As Int32)

        If clientsFiltersHash.ContainsKey(clientId) Then
            If clientsFiltersHash(clientId).ContainsKey(filterId) Then
                clientsFiltersHash(clientId)(filterId) = filterValueId
            Else
                clientsFiltersHash(clientId).Add(filterId, filterValueId)
            End If
        Else
            clientsFiltersHash.Add(clientId, New Dictionary(Of Int32, Int32))
            clientsFiltersHash(clientId).Add(filterId, filterValueId)
        End If

    End Sub

    Private Sub RemoveRecordFromFiltersHash(ByRef clientId As Int32, _
                                            ByRef filterId As Int32)

        On Error Resume Next
        clientsFiltersHash(clientId).Remove(filterId)

    End Sub

    Friend Shared Sub GetClientFilterHTFromPacket(ByRef packet As ByteBuffer, _
                                                  ByRef clientFilter_ht As Hashtable)

        clientFilter_ht(CLIENT_ID_VARIABLE) = packet.ReadUint32()
        clientFilter_ht(FILTER_ID_VARIABLE) = packet.ReadUint32()
        clientFilter_ht(FILTER_VALUE_ID_VARIABLE) = packet.ReadUint32()

    End Sub

    Private Sub WriteClientFilterPacket(ByRef packet As ByteBuffer, _
                                        ByRef attributes As Hashtable)

        packet.WriteUint32(attributes(CLIENT_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_VALUE_ID_VARIABLE))

    End Sub

#End Region


  Protected Overrides Sub finalize()

    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_CLIENT_FILTER_ANSWER, AddressOf SMSG_LIST_CLIENT_FILTER_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_CLIENT_FILTER_ANSWER, AddressOf SMSG_READ_CLIENT_FILTER_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_CLIENT_FILTER_ANSWER, AddressOf SMSG_DELETE_CLIENT_FILTER_ANSWER)
    MyBase.Finalize()

  End Sub



End Class
