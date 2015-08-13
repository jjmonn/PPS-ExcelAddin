﻿Imports System.Collections
Imports System.Collections.Generic


' Entity2.vb
'
' CRUD for entities table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 21/07/2015
' Last modified: 03/08/2015



Friend Class Entity

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend entities_hash As New Hashtable
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event EntityCreationEvent(ByRef attributes As Hashtable)
    Public Shared Event EntityRead(ByRef attributes As Hashtable)
    Public Event EntityUpdateEvent()
    Public Event EntityDeleteEvent(ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        LoadEntitiesTable()

    End Sub

    Friend Sub LoadEntitiesTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ENTITY_ANSWER, AddressOf SMSG_LIST_ENTITY_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_ENTITY, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_ENTITY_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetEntityHTFromPacket(packet, tmp_ht)
                entities_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ENTITY_ANSWER, AddressOf SMSG_LIST_ENTITY_ANSWER)
            server_response_flag = True
            RaiseEvent ObjectInitialized()
        Else
            server_response_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_ENTITY(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_ENTITY_ANSWER, AddressOf SMSG_CREATE_ENTITY_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ENTITY, UShort))
        WriteEntityPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_ENTITY_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MsgBox(packet.ReadString())
            Dim tmp_ht As New Hashtable
            GetEntityHTFromPacket(packet, tmp_ht)
            entities_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ENTITY_ANSWER, AddressOf SMSG_CREATE_ENTITY_ANSWER)
            RaiseEvent EntityCreationEvent(tmp_ht)
        Else
            ' priority high
            ' raise vent ->
            ' implement error message from server
        End If

    End Sub

    Friend Shared Sub CMSG_READ_ENTITY(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ENTITY_ANSWER, AddressOf SMSG_READ_ENTITY_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ENTITY, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_ENTITY_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetEntityHTFromPacket(packet, ht)
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ENTITY_ANSWER, AddressOf SMSG_READ_ENTITY_ANSWER)
            RaiseEvent EntityRead(ht)
        Else
            ' priority high
            ' raise vent -> set request failed in event
        End If

    End Sub

    Friend Sub UpdateBatch(ByRef updates As List(Of Object()))

        ' to be implemented !!!! priority normal

        request_id.Clear()
        For Each update As Object() In updates


        Next


    End Sub

    Friend Sub CMSG_UPDATE_ENTITY(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = entities_hash(id).clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ENTITY_ANSWER, AddressOf SMSG_UPDATE_ENTITY_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ENTITY, UShort))
        WriteEntityPacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_ENTITY(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ENTITY_ANSWER, AddressOf SMSG_UPDATE_ENTITY_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ENTITY, UShort))
        WriteEntityPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ENTITY_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetEntityHTFromPacket(packet, ht)
            entities_hash(ht(ID_VARIABLE)) = ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ENTITY_ANSWER, AddressOf SMSG_UPDATE_ENTITY_ANSWER)
            RaiseEvent EntityUpdateEvent()
        Else
            '
            '
        End If

    End Sub

    Friend Sub CMSG_DELETE_ENTITY(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ENTITY_ANSWER, AddressOf SMSG_DELETE_ENTITY_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ENTITY, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ENTITY_ANSWER()

        'If packet.ReadInt32() = 0 Then

        'Else

        'End If
        Dim id As UInt32
        ' get id from request_id ?
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ENTITY_ANSWER, AddressOf SMSG_DELETE_ENTITY_ANSWER)
        RaiseEvent EntityDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetEntitiesList(ByRef variable As String)

        Dim tmp_list As New List(Of String)
        Dim selection() As String = {}

        For Each id In entities_hash.Keys
            tmp_list.Add(entities_hash(id)(variable))
        Next
        Return tmp_list

    End Function

    Friend Function GetEntitiesDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In entities_hash.Keys
            tmpHT(entities_hash(id)(Key)) = entities_hash(id)(Value)
        Next
        Return tmpHT

    End Function

#End Region


#Region "Utilities"

    Friend Shared Sub GetEntityHTFromPacket(ByRef packet As ByteBuffer, ByRef entity_ht As Hashtable)

        entity_ht(ID_VARIABLE) = packet.ReadUint32()
        entity_ht(PARENT_ID_VARIABLE) = packet.ReadUint32()
        entity_ht(ENTITIES_CURRENCY_VARIABLE) = packet.ReadUint32()
        entity_ht(NAME_VARIABLE) = packet.ReadString()
        entity_ht(ITEMS_POSITIONS) = packet.ReadUint32()
        entity_ht(ENTITIES_ALLOW_EDITION_VARIABLE) = packet.ReadBool()
        If entity_ht(ENTITIES_ALLOW_EDITION_VARIABLE) = True Then
            entity_ht(IMAGE_VARIABLE) = 1
        Else
            entity_ht(IMAGE_VARIABLE) = 0
        End If

    End Sub

    Private Sub WriteEntityPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteUint32(attributes(ID_VARIABLE))
        packet.WriteUint32(attributes(PARENT_ID_VARIABLE))
        packet.WriteUint32(attributes(ENTITIES_CURRENCY_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteUint32(attributes(ITEMS_POSITIONS))
        packet.WriteUint8(attributes(ENTITIES_ALLOW_EDITION_VARIABLE))

    End Sub

    Friend Sub LoadEntitiesTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, entities_hash)

    End Sub

    Friend Sub LoadEntitiesTV(ByRef TV As Windows.Forms.TreeView, _
                            ByRef nodes_icon_dic As Dictionary(Of UInt32, Int32))

        Dim tmp_ht As New Hashtable
        tmp_ht = entities_hash
        For Each id As UInt32 In nodes_icon_dic.Keys
            tmp_ht(id)(IMAGE_VARIABLE) = nodes_icon_dic(id)
        Next
        TreeViewsUtilities.LoadTreeview(TV, tmp_ht)

    End Sub

    Friend Sub LoadEntitiesTVWithFilters(ByRef TV As Windows.Forms.TreeView, _
                                         ByRef axisFilteredValues As List(Of UInt32))


        Dim tmp_ht As New Hashtable
        For Each id As UInt32 In entities_hash.Keys
            If axisFilteredValues.Contains(id) Then
                tmp_ht(id) = entities_hash(id)
            End If
        Next
        TreeViewsUtilities.LoadTreeview(TV, tmp_ht)

    End Sub


#End Region



End Class