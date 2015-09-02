Imports System.Collections
Imports System.Collections.Generic


' EntityFilter.vb
'
' CRUD for entitiesFilters table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 23/07/2015
' Last modified: 02/09/2015



Friend Class EntitiesFilter


#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend entitiesFiltersHash As New Dictionary(Of Int32, Dictionary(Of Int32, Int32))
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef entity_id As Int32, ByRef filter_id As Int32, filter_value_id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef entity_id As Int32, ByRef filter_id As Int32, filter_value_id As Int32)
    Public Event DeleteEvent(ByRef status As Boolean, ByRef entity_id As Int32, filter_id As Int32)

#End Region


#Region "Init"

  Friend Sub New()

    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ENTITY_FILTER_ANSWER, AddressOf SMSG_LIST_ENTITY_FILTER_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ENTITY_FILTER_ANSWER, AddressOf SMSG_READ_ENTITY_FILTER_ANSWER)
    NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ENTITY_FILTER_ANSWER, AddressOf SMSG_DELETE_ENTITY_FILTER_ANSWER)
    state_flag = False

  End Sub

    Private Sub SMSG_LIST_ENTITY_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim nbRecords As UInt32 = packet.ReadInt32()
            For i As Int32 = 1 To nbRecords
                Dim tmp_ht As New Hashtable
                GetEntityFilterHTFromPacket(packet, tmp_ht)

                AddRecordToEntitiesFiltersHash(tmp_ht(ENTITY_ID_VARIABLE), _
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

    Friend Sub CMSG_CREATE_ENTITY_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_ENTITY_FILTER_ANSWER, AddressOf SMSG_CREATE_ENTITY_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ENTITY_FILTER, UShort))
        WriteEntityFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_ENTITY_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim entity_id As Int32 = packet.ReadUint32()
            Dim filter_id As Int32 = packet.ReadUint32()
            Dim filter_value_id As Int32 = packet.ReadUint32()
            RaiseEvent CreationEvent(True, entity_id, filter_id, filter_value_id)
        Else
            RaiseEvent CreationEvent(False, 0, 0, 0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ENTITY_FILTER_ANSWER, AddressOf SMSG_CREATE_ENTITY_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_READ_ENTITY_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_ENTITY_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_ENTITY_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetEntityFilterHTFromPacket(packet, ht)
            AddRecordToEntitiesFiltersHash(ht(ENTITY_ID_VARIABLE), _
                                           ht(FILTER_ID_VARIABLE), _
                                           ht(FILTER_VALUE_ID_VARIABLE))
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_entity_FILTER(ByRef entityId As Int32, _
                                             ByRef filterId As Int32, _
                                             ByRef filterValueId As Int32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ENTITY_FILTER_ANSWER, AddressOf SMSG_UPDATE_ENTITY_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ENTITY_FILTER, UShort))
        packet.WriteUint32(entityId)
        packet.WriteUint32(filterId)
        packet.WriteUint32(filterValueId)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ENTITY_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim entity_id As Int32 = packet.ReadUint32()
            Dim filter_id As Int32 = packet.ReadUint32()
            Dim filter_value_id As Int32 = packet.ReadUint32()
            RaiseEvent UpdateEvent(True, entity_id, filter_id, filter_value_id)
        Else
            RaiseEvent UpdateEvent(False, 0, 0, 0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ENTITY_FILTER_ANSWER, AddressOf SMSG_UPDATE_ENTITY_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_ENTITY_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ENTITY_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ENTITY_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim entityId As UInt32 = packet.ReadInt32
            Dim filterId As UInt32 = packet.ReadInt32
            RemoveRecordFromFiltersHash(entityId, filterId)
            RaiseEvent DeleteEvent(True, entityId, filterId)
        Else
            RaiseEvent DeleteEvent(False, 0, 0)
        End If

    End Sub

#End Region


#Region "Mappings"


    Friend Function GetFilteredEntityIDs(ByRef filter_id As UInt32, _
                                         ByRef filter_value_id As UInt32) As List(Of UInt32)

        Dim entities_list As New List(Of UInt32)
        For Each entityId As Int32 In entitiesFiltersHash.Keys
            If entitiesFiltersHash(entityId)(filter_id) = filter_value_id Then
                entities_list.Add(entityId)
            End If
        Next
        Return entities_list

    End Function


#End Region


#Region "Utilities"

    Private Sub AddRecordToEntitiesFiltersHash(ByRef entityId As Int32, _
                                                ByRef filterId As Int32, _
                                                ByRef filterValueId As Int32)

        If entitiesFiltersHash.ContainsKey(entityId) Then
            If entitiesFiltersHash(entityId).ContainsKey(filterId) Then
                entitiesFiltersHash(entityId)(filterId) = filterValueId
            Else
                entitiesFiltersHash(entityId).Add(filterId, filterValueId)
            End If
        Else
            entitiesFiltersHash.Add(entityId, New Dictionary(Of Int32, Int32))
            entitiesFiltersHash(entityId).Add(filterId, filterValueId)
        End If

    End Sub

    Private Sub RemoveRecordFromFiltersHash(ByRef entityId As Int32, _
                                           ByRef filterId As Int32)

        On Error Resume Next
        entitiesFiltersHash(CInt(entityId)).Remove(CInt(filterId))

    End Sub

    Friend Shared Sub GetEntityFilterHTFromPacket(ByRef packet As ByteBuffer, _
                                                  ByRef entityFilter_ht As Hashtable)

        entityFilter_ht(ENTITY_ID_VARIABLE) = packet.ReadUint32()
        entityFilter_ht(FILTER_ID_VARIABLE) = packet.ReadUint32()
        entityFilter_ht(FILTER_VALUE_ID_VARIABLE) = packet.ReadUint32()

    End Sub

    Private Sub WriteEntityFilterPacket(ByRef packet As ByteBuffer, _
                                        ByRef attributes As Hashtable)

        packet.WriteUint32(attributes(ENTITY_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_VALUE_ID_VARIABLE))

    End Sub


#End Region


  Protected Overrides Sub finalize()

    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ENTITY_FILTER_ANSWER, AddressOf SMSG_LIST_ENTITY_FILTER_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_ENTITY_FILTER_ANSWER, AddressOf SMSG_READ_ENTITY_FILTER_ANSWER)
    NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ENTITY_FILTER_ANSWER, AddressOf SMSG_DELETE_ENTITY_FILTER_ANSWER)
    MyBase.Finalize()

  End Sub



End Class
