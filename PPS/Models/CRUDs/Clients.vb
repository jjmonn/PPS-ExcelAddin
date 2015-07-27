﻿Imports System.Collections
Imports System.Collections.Generic


' Client.vb
'
' CRUD for clients table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 23/07/2015
' Last modified: 24/07/2015



Friend Class Client

#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend clients_hash As New Hashtable
    Private request_id As Dictionary(Of UInt32, Boolean)

    ' Events
    Public Event ClientCreationEvent(ByRef attributes As Hashtable)
    Public Shared Event ClientRead(ByRef attributes As Hashtable)
    Public Event ClientUpdateEvent(ByRef attributes As Hashtable)
    Public Event ClientDeleteEvent(ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        LoadClientsTable()
        Dim time_stamp = Timer
        Do
            If Timer - time_stamp > GlobalVariables.timeOut Then
                state_flag = False
                Exit Do
            End If
        Loop While server_response_flag = True
        state_flag = True

    End Sub

    Friend Sub LoadClientsTable()

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_LIST_CLIENT_ANSWER, AddressOf SMSG_LIST_CLIENT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_LIST_CLIENT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_CLIENT_ANSWER(packet As ByteBuffer)

        For i As Int32 = 0 To packet.ReadInt32()
            Dim tmp_ht As New Hashtable
            GetClientHTFromPacket(packet, tmp_ht)
            clients_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        Next
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_LIST_CLIENT_ANSWER, AddressOf SMSG_LIST_CLIENT_ANSWER)
        server_response_flag = True

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_CLIENT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_CREATE_CLIENT_ANSWER, AddressOf SMSG_CREATE_CLIENT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_CREATE_CLIENT, UShort))
        WriteClientPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_CLIENT_ANSWER(packet As ByteBuffer)

        MsgBox(packet.ReadString())
        Dim tmp_ht As New Hashtable
        GetClientHTFromPacket(packet, tmp_ht)
        clients_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_CREATE_CLIENT_ANSWER, AddressOf SMSG_CREATE_CLIENT_ANSWER)
        RaiseEvent ClientCreationEvent(tmp_ht)

    End Sub

    Friend Shared Sub CMSG_READ_CLIENT(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_READ_CLIENT_ANSWER, AddressOf SMSG_READ_CLIENT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_CREATE_CLIENT, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_CLIENT_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetClientHTFromPacket(packet, ht)
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_CREATE_CLIENT_ANSWER, AddressOf SMSG_READ_CLIENT_ANSWER)
        RaiseEvent ClientRead(ht)

    End Sub

    Friend Sub UpdateBatch(ByRef updates As List(Of Object()))

        ' to be implemented !!!! priority normal

        request_id.Clear()
        For Each update As Object() In updates


        Next


    End Sub

    Friend Sub CMSG_UPDATE_CLIENT(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = clients_hash(id).clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_CLIENT_ANSWER, AddressOf SMSG_UPDATE_CLIENT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_UPDATE_CLIENT, UShort))
        WriteClientPacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_CLIENT(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_CLIENT_ANSWER, AddressOf SMSG_UPDATE_CLIENT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_UPDATE_CLIENT, UShort))
        WriteClientPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_CLIENT_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetClientHTFromPacket(packet, ht)
        clients_hash(ht(ID_VARIABLE)) = ht
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_CLIENT_ANSWER, AddressOf SMSG_UPDATE_CLIENT_ANSWER)
        RaiseEvent ClientUpdateEvent(ht)

    End Sub

    Friend Sub CMSG_DELETE_CLIENT(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_DELETE_CLIENT_ANSWER, AddressOf SMSG_DELETE_CLIENT_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_DELETE_CLIENT, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_CLIENT_ANSWER()

        Dim id As UInt32
        ' get id from request_id ?
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_DELETE_CLIENT_ANSWER, AddressOf SMSG_DELETE_CLIENT_ANSWER)
        RaiseEvent ClientDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetClientsNameList() As List(Of String)

        Dim tmp_list As New List(Of String)
        For Each id In clients_hash.Keys
            tmp_list.Add(clients_hash(id)(NAME_VARIABLE))
        Next
        Return tmp_list

    End Function

    Friend Function GetClientsDictionary(ByRef Key As String, ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In clients_hash.Keys
            tmpHT(clients_hash(id)(Key)) = clients_hash(id)(Value)
        Next
        Return tmpHT

    End Function

#End Region


#Region "Utilities"

    Friend Shared Sub GetClientHTFromPacket(ByRef packet As ByteBuffer, ByRef client_ht As Hashtable)

        client_ht(ID_VARIABLE) = packet.ReadInt32()
        client_ht(NAME_VARIABLE) = packet.ReadString()

    End Sub

    Private Sub WriteClientPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))

    End Sub

    Friend Sub LoadClientsTree(ByRef TV As Windows.Forms.TreeView)

        TV.Nodes.Clear()
        For Each id As UInt32 In clients_hash.Keys
            Dim node As Windows.Forms.TreeNode = TV.Nodes.Add(CStr(id), _
                                                              clients_hash(id)(NAME_VARIABLE), _
                                                              0, 0)
            node.Checked = True
        Next

    End Sub

    Friend Sub LoadClientsTree(ByRef TV As Windows.Forms.TreeView, _
                               ByRef filter_list As List(Of UInt32))

        TV.Nodes.Clear()
        For Each id As UInt32 In clients_hash.Keys
            Dim node As Windows.Forms.TreeNode = TV.Nodes.Add(CStr(id), _
                                                              clients_hash(id)(NAME_VARIABLE), _
                                                              0, 0)
            node.Checked = True
        Next

    End Sub

    Friend Function IsNameValid(ByRef name As String) As Boolean

        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ As Char In AXIS_NAME_FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        If name = "" Then Return False
        If GetClientsNameList(NAME_VARIABLE).Contains(name) Then Return False
        Return True

    End Function

#End Region





End Class
