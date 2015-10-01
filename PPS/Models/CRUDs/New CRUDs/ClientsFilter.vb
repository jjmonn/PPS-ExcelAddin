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
' Last modified: 04/09/2015



Friend Class ClientsFilter : Inherits SuperAxisFilterCRUD


#Region "Instance variables"

    ' Events
    Public Event ObjectInitialized()
    Public Shadows Event CreationEvent(ByRef status As Boolean, ByRef client_id As Int32, ByRef filter_id As Int32, filter_value_id As Int32)
    Public Shadows Event DeleteEvent(ByRef status As Boolean, ByRef client_id As Int32, filter_id As Int32)

#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_CLIENT_FILTER_ANSWER, AddressOf SMSG_LIST_CLIENT_FILTER_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_CLIENT_FILTER_ANSWER, AddressOf SMSG_READ_CLIENT_FILTER_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_CLIENT_FILTER_ANSWER, AddressOf SMSG_DELETE_CLIENT_FILTER_ANSWER)
        state_flag = False

    End Sub

    Private Sub SMSG_LIST_CLIENT_FILTER_ANSWER(packet As ByteBuffer)

        axisFiltersHash.Clear()
        If packet.GetError() = 0 Then
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

    Friend Sub CMSG_CREATE_CLIENT_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_CLIENT_FILTER_ANSWER, AddressOf SMSG_CREATE_CLIENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_CLIENTS_FILTER, UShort))
        WriteAxisFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_CLIENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
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

        If packet.GetError() = 0 Then
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

    Friend Overrides Sub CMSG_UPDATE_AXIS_FILTER(ByRef clientId As Int32, _
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

    Private Sub SMSG_UPDATE_CLIENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim client_id As Int32 = packet.ReadUint32()
            Dim filter_id As Int32 = packet.ReadUint32()
            Dim filter_value_id As Int32 = packet.ReadUint32()
            MyBase.OnUpdate(True, client_id, filter_id, filter_value_id)
        Else
            MyBase.OnUpdate(False, 0, 0, 0)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_CLIENT_FILTER_ANSWER, AddressOf SMSG_UPDATE_CLIENT_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_UPDATE_AXIS_FILTER_LIST(ByRef p_filters As Hashtable)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_CLIENT_FILTER_LIST_ANSWER, AddressOf SMSG_UPDATE_CLIENT_FILTER_LIST_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_CLIENT_FILTER_LIST, UShort))

        packet.WriteInt32(p_filters.Count())
        For Each attributes As Hashtable In p_filters.Values
            packet.WriteUint8(CRUDAction.UPDATE)
            WriteAxisFilterPacket(packet, attributes)
        Next
        packet.Release()
        NetworkManager.GetInstance().Send(packet)
    End Sub

    Private Sub SMSG_UPDATE_CLIENT_FILTER_LIST_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim resultList As New List(Of Boolean)
            Dim nbResult As Int32 = packet.ReadInt32()

            For i As Int32 = 1 To nbResult
                resultList.Add(packet.ReadBool())
                packet.ReadString()
            Next

            MyBase.OnUpdateList(True, resultList)
        Else
            MyBase.OnUpdateList(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_CLIENT_FILTER_LIST_ANSWER, AddressOf SMSG_UPDATE_CLIENT_FILTER_LIST_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_CLIENT_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_CLIENTS_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_CLIENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.GetError() = 0 Then
            Dim clientId As UInt32 = packet.ReadInt32
            Dim filterId As UInt32 = packet.ReadInt32
            RemoveRecordFromFiltersHash(clientId, filterId)
            RaiseEvent DeleteEvent(True, clientId, filterId)
        Else
            RaiseEvent DeleteEvent(False, 0, 0)
        End If

    End Sub

#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_CLIENT_FILTER_ANSWER, AddressOf SMSG_LIST_CLIENT_FILTER_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_CLIENT_FILTER_ANSWER, AddressOf SMSG_READ_CLIENT_FILTER_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_CLIENT_FILTER_ANSWER, AddressOf SMSG_DELETE_CLIENT_FILTER_ANSWER)
        MyBase.Finalize()

    End Sub



End Class
