﻿Imports System.Collections
Imports System.Collections.Generic


' Filter.vb
'
' CRUD for filters table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 22/07/2015
' Last modified: 22/07/2015



Friend Class Filter

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend filters_hash As New Hashtable
    Private request_id As Dictionary(Of UInt32, Boolean)


    ' Constants
     Private FORBIDEN_CHARS = {","}

    ' Events
    Public Event FilterCreationEvent(ByRef attributes As Hashtable)
    Public Shared Event FilterRead(ByRef attributes As Hashtable)
    Public Event FilterUpdateEvent()
    Public Event FilterDeleteEvent(ByRef id As UInt32)


#End Region


#Region "Init"

    Friend Sub New()

        LoadFiltersTable()
        Dim time_stamp = Timer
        Do
            If Timer - time_stamp > GlobalVariables.timeOut Then
                state_flag = False
                Exit Do
            End If
        Loop While server_response_flag = True
        state_flag = True

    End Sub

    Friend Sub LoadFiltersTable()

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_LIST_FILTER_ANSWER, AddressOf SMSG_LIST_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_LIST_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_FILTER_ANSWER(packet As ByteBuffer)

        For i As Int32 = 0 To packet.ReadInt32()
            Dim tmp_ht As New Hashtable
            GetFilterHTFromPacket(packet, tmp_ht)
            filters_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        Next
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_LIST_FILTER_ANSWER, AddressOf SMSG_LIST_FILTER_ANSWER)
        server_response_flag = True

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_CREATE_FILTER_ANSWER, AddressOf SMSG_CREATE_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_CREATE_FILTER, UShort))
        WriteFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_FILTER_ANSWER(packet As ByteBuffer)

        MsgBox(packet.ReadString())
        Dim tmp_ht As New Hashtable
        GetFilterHTFromPacket(packet, tmp_ht)
        filters_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_CREATE_FILTER_ANSWER, AddressOf SMSG_CREATE_FILTER_ANSWER)
        RaiseEvent FilterCreationEvent(tmp_ht)

    End Sub

    Friend Shared Sub CMSG_READ_FILTER(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_READ_FILTER_ANSWER, AddressOf SMSG_READ_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_CREATE_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_FILTER_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetFilterHTFromPacket(packet, ht)
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_CREATE_FILTER_ANSWER, AddressOf SMSG_READ_FILTER_ANSWER)
        RaiseEvent FilterRead(ht)

    End Sub

    Friend Sub UpdateBatch(ByRef updates As List(Of Object()))

        ' to be implemented !!!! priority normal

        request_id.Clear()
        For Each update As Object() In updates


        Next


    End Sub

    Friend Sub CMSG_UPDATE_FILTER(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = filters_hash(id).clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_FILTER_ANSWER, AddressOf SMSG_UPDATE_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_UPDATE_FILTER, UShort))
        WriteFilterPacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_FILTER_ANSWER, AddressOf SMSG_UPDATE_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_UPDATE_FILTER, UShort))
        WriteFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_FILTER_ANSWER(packet As ByteBuffer)

        Dim ht As New Hashtable
        GetFilterHTFromPacket(packet, ht)
        filters_hash(ht(ID_VARIABLE)) = ht
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_UPDATE_FILTER_ANSWER, AddressOf SMSG_UPDATE_FILTER_ANSWER)
        RaiseEvent FilterUpdateEvent()

    End Sub

    Friend Sub CMSG_DELETE_FILTER(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(GlobalEnums.ServerMessage.SMSG_DELETE_FILTER_ANSWER, AddressOf SMSG_DELETE_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(GlobalEnums.ClientMessage.CMSG_DELETE_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_FILTER_ANSWER()

        Dim id As UInt32
        NetworkManager.GetInstance().RemoveCallback(GlobalEnums.ServerMessage.SMSG_DELETE_FILTER_ANSWER, AddressOf SMSG_DELETE_FILTER_ANSWER)
        RaiseEvent FilterDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetFiltersList(ByRef axis_id As UInt32, _
                                   ByRef variable As String)

        Dim tmp_list As New List(Of String)
        Dim selection() As String = {}

        For Each id In filters_hash.Keys
            If filters_hash(id)(FILTER_AXIS_ID_VARIABLE) = axis_id Then tmp_list.Add(filters_hash(id)(variable))
        Next
        Return tmp_list

    End Function

    Friend Function GetFiltersList(ByRef variable As String)

        Dim tmp_list As New List(Of String)
        Dim selection() As String = {}

        For Each id In filters_hash.Keys
            tmp_list.Add(filters_hash(id)(variable))
        Next
        Return tmp_list

    End Function

    Friend Function GetFiltersDictionary(ByRef axis_id As UInt32, _
                                         ByRef Key As String,
                                         ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In filters_hash.Keys
            If filters_hash(id)(FILTER_AXIS_ID_VARIABLE) = axis_id Then tmpHT(filters_hash(id)(Key)) = filters_hash(id)(Value)
        Next
        Return tmpHT

    End Function

#End Region


#Region "Utilities"

    Friend Shared Sub GetFilterHTFromPacket(ByRef packet As ByteBuffer, ByRef filter_ht As Hashtable)

        filter_ht(ID_VARIABLE) = packet.ReadInt32()
        filter_ht(PARENT_ID_VARIABLE) = packet.ReadInt32()
        filter_ht(FILTER_AXIS_ID_VARIABLE) = packet.ReadString()
        filter_ht(NAME_VARIABLE) = packet.ReadString()
        filter_ht(ITEMS_POSITIONS) = packet.ReadInt32()
        filter_ht(FILTER_IS_PARENT_VARIABLE) = packet.ReadInt32()

    End Sub

    Private Sub WriteFilterPacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteInt32(attributes(ID_VARIABLE))
        packet.WriteInt32(attributes(PARENT_ID_VARIABLE))
        packet.WriteInt32(attributes(FILTER_AXIS_ID_VARIABLE))
        packet.WriteString(attributes(NAME_VARIABLE))
        packet.WriteInt32(attributes(ITEMS_POSITIONS))
        packet.WriteInt32(attributes(FILTER_IS_PARENT_VARIABLE))

    End Sub

    Friend Sub LoadFiltersNode(ByRef node As Windows.Forms.TreeNode, _
                             ByRef axis_id As UInt32)

        Dim tmp_ht As New Hashtable
        For Each id As UInt32 In filters_hash.Keys
            If filters_hash(id)(FILTER_AXIS_ID_VARIABLE) = axis_id Then
                tmp_ht(id) = filters_hash(id)
            End If
        Next
        TreeViewsUtilities.LoadTreeview(node, tmp_ht)

    End Sub

    Friend Sub LoadFiltersTV(ByRef TV As Windows.Forms.TreeView, _
                            ByRef axis_id As UInt32)

        Dim tmp_ht As New Hashtable
        For Each id As UInt32 In filters_hash.Keys
            If filters_hash(id)(FILTER_AXIS_ID_VARIABLE) = axis_id Then
                tmp_ht(id) = filters_hash(id)
            End If
        Next
        TreeViewsUtilities.LoadTreeview(TV, tmp_ht)

    End Sub


    ' should load treenode instead ? => filters tv not displayed - priority normal (function to be implemented) !
    Friend Function IsNameValid(ByRef name As String) As Boolean

        If name.Length > NAMES_MAX_LENGTH Then Return False
        For Each char_ In FORBIDEN_CHARS
            If name.Contains(char_) Then Return False
        Next
        If name = "" Then Return False
        If GetFiltersList(NAME_VARIABLE).contains(name) Then Return False
        Return True

    End Function

#End Region


End Class
