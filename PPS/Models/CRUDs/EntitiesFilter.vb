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
' Last modified: 23/07/2015



Friend Class EntitiesFilter

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend entitiesFilters_list As New List(Of Hashtable)
    Private fIdFvDict As New Dictionary(Of String, UInt32)
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event EntityFilterCreationEvent(ByRef attributes As Hashtable)

    Public Shared Event EntityFilterRead(ByRef attributes As Hashtable)
    Public Event EntityFilterUpdateEvent()
    Public Event EntityFilterDeleteEvent(ByRef id As UInt32)

#End Region


#Region "Init"

    Friend Sub New()

        LoadEntityFiltersTable()
        'Dim time_stamp = Timer
        'Do
        '    If Timer - time_stamp > GlobalVariables.timeOut Then
        '        state_flag = False
        '        Exit Do
        '    End If
        'Loop While server_response_flag = True
        'state_flag = True

    End Sub

    Friend Sub LoadEntityFiltersTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ENTITY_FILTER_ANSWER, AddressOf SMSG_LIST_ENTITY_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_ENTITY_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_ENTITY_FILTER_ANSWER(packet As ByteBuffer)

        For i As Int32 = 0 To packet.ReadInt32()
            Dim tmp_ht As New Hashtable
            GetEntityFilterHTFromPacket(packet, tmp_ht)
            entitiesFilters_list(tmp_ht(ID_VARIABLE)) = tmp_ht
            fIdFvDict.Add(tmp_ht(ENTITY_ID_VARIABLE) & tmp_ht(FILTER_ID_VARIABLE), tmp_ht(FILTER_VALUE_ID_VARIABLE))
        Next
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ENTITY_FILTER_ANSWER, AddressOf SMSG_LIST_ENTITY_FILTER_ANSWER)
        server_response_flag = True

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

        MsgBox(packet.ReadString())
        Dim tmp_ht As New Hashtable
        GetEntityFilterHTFromPacket(packet, tmp_ht)
        entitiesFilters_list.Add(tmp_ht)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ENTITY_FILTER_ANSWER, AddressOf SMSG_CREATE_ENTITY_FILTER_ANSWER)
        RaiseEvent EntityFilterCreationEvent(tmp_ht)

    End Sub

    Friend Shared Sub CMSG_READ_ENTITY_FILTER(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ENTITY_FILTER_ANSWER, AddressOf SMSG_READ_ENTITY_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ENTITY_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_ENTITY_FILTER_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetEntityFilterHTFromPacket(packet, ht)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ENTITY_FILTER_ANSWER, AddressOf SMSG_READ_ENTITY_FILTER_ANSWER)
        RaiseEvent EntityFilterRead(ht)

    End Sub

    Friend Sub CMSG_UPDATE_ENTITY_FILTER(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = entitiesFilters_list(id).clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ENTITY_FILTER_ANSWER, AddressOf SMSG_UPDATE_ENTITY_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ENTITY_FILTER, UShort))
        WriteEntityFilterPacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_ENTITY_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ENTITY_FILTER_ANSWER, AddressOf SMSG_UPDATE_ENTITY_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ENTITY_FILTER, UShort))
        WriteEntityFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ENTITY_FILTER_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetEntityFilterHTFromPacket(packet, ht)
        entitiesFilters_list(ht(ID_VARIABLE)) = ht
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ENTITY_FILTER_ANSWER, AddressOf SMSG_UPDATE_ENTITY_FILTER_ANSWER)
        RaiseEvent EntityFilterUpdateEvent()

    End Sub

    Friend Sub CMSG_DELETE_ENTITY_FILTER(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ENTITY_FILTER_ANSWER, AddressOf SMSG_DELETE_ENTITY_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ENTITY_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ENTITY_FILTER_ANSWER()

        Dim id As UInt32
        ' get id from request_id ?
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ENTITY_FILTER_ANSWER, AddressOf SMSG_DELETE_ENTITY_FILTER_ANSWER)
        RaiseEvent EntityFilterDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetEntitiesFiltersDictionary()


    End Function

    Friend Function GetFilteredEntityIDs(ByRef filter_id As UInt32, _
                                         ByRef filter_value_id As UInt32) As List(Of UInt32)

        Dim entities_list As New List(Of UInt32)
        For Each ht In entitiesFilters_list
            If ht(FILTER_ID_VARIABLE) = filter_id _
            AndAlso ht(FILTER_VALUE_ID_VARIABLE) = filter_value_id Then
                entities_list.Add(ht(ENTITY_ID_VARIABLE))
            End If
        Next
        Return entities_list

    End Function

    Friend Function GetFilterValue(ByRef entity_id As UInt32, _
                                   ByRef filter_id As UInt32)

        Return fIdFvDict(entity_id & filter_id)

    End Function


#End Region


#Region "Utilities"

    Friend Shared Sub GetEntityFilterHTFromPacket(ByRef packet As ByteBuffer, _
                                                  ByRef entityFilter_ht As Hashtable)

        entityFilter_ht(ENTITY_ID_VARIABLE) = packet.ReadInt32()
        entityFilter_ht(FILTER_ID_VARIABLE) = packet.ReadInt32()
        entityFilter_ht(FILTER_VALUE_ID_VARIABLE) = packet.ReadInt32()

    End Sub

    Private Sub WriteEntityFilterPacket(ByRef packet As ByteBuffer, _
                                        ByRef attributes As Hashtable)

        packet.WriteInt32(attributes(ENTITY_ID_VARIABLE))
        packet.WriteInt32(attributes(FILTER_ID_VARIABLE))
        packet.WriteInt32(attributes(FILTER_VALUE_ID_VARIABLE))

    End Sub


#End Region





End Class
