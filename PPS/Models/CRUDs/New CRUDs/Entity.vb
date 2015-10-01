Imports System.Collections
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
' Last modified: 23/09/2015



Friend Class Entity


#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend entities_hash As New Hashtable
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event CreationEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef id As Int32)
    Public Event UpdateListEvent(ByRef status As Boolean, ByRef updateResults As Dictionary(Of Int32, Boolean))
    Public Event DeleteEvent(ByRef status As Boolean, ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ENTITY_ANSWER, AddressOf SMSG_READ_ENTITY_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ENTITY_ANSWER, AddressOf SMSG_DELETE_ENTITY_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ENTITY_ANSWER, AddressOf SMSG_LIST_ENTITY_ANSWER)

        state_flag = False

    End Sub

    Private Sub SMSG_LIST_ENTITY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetEntityHTFromPacket(packet, tmp_ht)
                entities_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            state_flag = True
            RaiseEvent ObjectInitialized()
        Else
            state_flag = False
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

        If packet.GetError() = 0 Then
            RaiseEvent CreationEvent(True, packet.ReadUint32())
        Else
            RaiseEvent CreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ENTITY_ANSWER, AddressOf SMSG_CREATE_ENTITY_ANSWER)

    End Sub

    Friend Sub CMSG_READ_ENTITY(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_ENTITY, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_ENTITY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim ht As New Hashtable
            GetEntityHTFromPacket(packet, ht)
            entities_hash(CInt(ht(ID_VARIABLE))) = ht
            RaiseEvent Read(True, ht)
        Else
            RaiseEvent Read(False, Nothing)
        End If

    End Sub

    Friend Sub CMSG_UPDATE_ENTITY(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ENTITY_ANSWER, AddressOf SMSG_UPDATE_ENTITY_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ENTITY, UShort))
        WriteEntityPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ENTITY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            RaiseEvent UpdateEvent(True, packet.ReadUint32())
        Else
            RaiseEvent UpdateEvent(False, packet.ReadUint32())
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ENTITY_ANSWER, AddressOf SMSG_UPDATE_ENTITY_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_ENTITY_LIST(ByRef p_entities As Hashtable)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ENTITY_LIST_ANSWER, AddressOf SMSG_UPDATE_ENTITY_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ENTITY_LIST, UShort))

        packet.WriteInt32(p_entities.Count())
        For Each attributes As Hashtable In p_entities.Values
            packet.WriteUint8(CRUDAction.UPDATE)
            WriteEntityPacket(packet, attributes)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_UPDATE_ENTITY_LIST_ANSWER(packet As ByteBuffer)

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

            RaiseEvent UpdateListEvent(False, resultList)
        Else
            RaiseEvent UpdateListEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ENTITY_LIST_ANSWER, AddressOf SMSG_UPDATE_ENTITY_LIST_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_ENTITY(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ENTITY, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ENTITY_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim id As UInt32 = packet.ReadInt32
            entities_hash.Remove(CInt(id))
            RaiseEvent DeleteEvent(True, id)
        Else
            RaiseEvent DeleteEvent(False, 0)
        End If

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

    Friend Function GetEntityId(ByRef name As String) As Int32

        For Each id As Int32 In entities_hash.Keys
            If name = entities_hash(id)(NAME_VARIABLE) Then Return id
        Next
        Return 0

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
        packet.WriteBool(attributes(ENTITIES_ALLOW_EDITION_VARIABLE))

    End Sub

    Friend Sub LoadEntitiesTV(ByRef TV As Windows.Forms.TreeView)

        TreeViewsUtilities.LoadTreeview(TV, entities_hash)

    End Sub

    Friend Sub LoadEntitiesTV(ByRef TV As VIBlend.WinForms.Controls.vTreeView)

        VTreeViewUtil.LoadTreeview(TV, entities_hash)

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


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_ENTITY_ANSWER, AddressOf SMSG_READ_ENTITY_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ENTITY_ANSWER, AddressOf SMSG_DELETE_ENTITY_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ENTITY_ANSWER, AddressOf SMSG_LIST_ENTITY_ANSWER)
        MyBase.Finalize()

    End Sub


End Class
