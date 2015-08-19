Imports System.Collections
Imports System.Collections.Generic


' FilterValue2.vb
'
' CRUD for filtervalues table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 23/07/2015
' Last modified: 17/08/2015



Friend Class FilterValue

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend filtervalues_hash As New Hashtable
    Private request_id As Dictionary(Of Int32, Boolean)

    ' Events
    Public Event ObjectInitialized()
    Public Event FilterValueCreationEvent(ByRef attributes As Hashtable)
    Public Shared Event FilterValueRead(ByRef attributes As Hashtable)
    Public Event FilterValueUpdateEvent()
    Public Event FilterValueDeleteEvent(ByRef id As Int32)


#End Region


#Region "Init"

    Friend Sub New()

        LoadFiltervaluesTable()

    End Sub

    Friend Sub LoadFiltervaluesTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_FILTER_VALUE_ANSWER, AddressOf SMSG_LIST_FILTER_VALUE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_FILTER_VALUE, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_FILTER_VALUE_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetFilterValueHTFromPacket(packet, tmp_ht)
                filtervalues_hash(CInt(tmp_ht(ID_VARIABLE))) = tmp_ht
            Next
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_FILTER_VALUE_ANSWER, AddressOf SMSG_LIST_FILTER_VALUE_ANSWER)
            server_response_flag = True
            RaiseEvent ObjectInitialized()
        Else
            server_response_flag = False
        End If

    End Sub

#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_FILTER_VALUE(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_FILTERS_VALUE_ANSWER, AddressOf SMSG_CREATE_FILTER_VALUE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_FILTER_VALUE, UShort))
        WriteFilterValuePacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_FILTER_VALUE_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MsgBox(packet.ReadString())
            Dim tmp_ht As New Hashtable
            GetFilterValueHTFromPacket(packet, tmp_ht)
            filtervalues_hash(tmp_ht(ID_VARIABLE)) = tmp_ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_FILTERS_VALUE_ANSWER, AddressOf SMSG_CREATE_FILTER_VALUE_ANSWER)
            RaiseEvent FilterValueCreationEvent(tmp_ht)
        Else

        End If

    End Sub

    Friend Shared Sub CMSG_READ_FILTER_VALUE(ByRef id As Int32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_FILTERS_VALUE_ANSWER, AddressOf SMSG_READ_FILTER_VALUE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_FILTER_VALUE, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_FILTER_VALUE_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetFilterValueHTFromPacket(packet, ht)
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_FILTERS_VALUE_ANSWER, AddressOf SMSG_READ_FILTER_VALUE_ANSWER)
            RaiseEvent FilterValueRead(ht)
        Else

        End If

    End Sub

    Friend Sub UpdateBatch(ByRef updates As List(Of Object()))

        ' to be implemented !!!! priority normal

        request_id.Clear()
        For Each update As Object() In updates


        Next


    End Sub

    Friend Sub CMSG_UPDATE_FILTER_VALUE(ByRef id As Int32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = filtervalues_hash(id).clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_FILTERS_VALUE_ANSWER, AddressOf SMSG_UPDATE_FILTER_VALUE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_FILTER_VALUE, UShort))
        WriteFilterValuePacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_FILTER_VALUE(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_FILTERS_VALUE_ANSWER, AddressOf SMSG_UPDATE_FILTER_VALUE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_FILTER_VALUE, UShort))
        WriteFilterValuePacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_FILTER_VALUE_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetFilterValueHTFromPacket(packet, ht)
            filtervalues_hash(ht(ID_VARIABLE)) = ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_FILTERS_VALUE_ANSWER, AddressOf SMSG_UPDATE_FILTER_VALUE_ANSWER)
            RaiseEvent FilterValueUpdateEvent()
        Else

        End If

    End Sub

    Friend Sub CMSG_DELETE_FILTER_VALUE(ByRef id As Int32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_FILTERS_VALUE_ANSWER, AddressOf SMSG_DELETE_FILTER_VALUE_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_FILTER_VALUE, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_FILTER_VALUE_ANSWER()

        '   If packet.ReadInt32() = 0 Then

        Dim id As UInt32 ' read packet ?
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_FILTERS_VALUE_ANSWER, AddressOf SMSG_DELETE_FILTER_VALUE_ANSWER)
        RaiseEvent FilterValueDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetFiltervaluesList(ByRef variable As String) As List(Of Object)

        Dim tmp_list As New List(Of Object)
        For Each id In filtervalues_hash.Keys
            tmp_list.Add(filtervalues_hash(id)(variable))
        Next
        Return tmp_list

    End Function

    Friend Function GetFiltervaluesList(ByRef filter_id As Int32, _
                                        ByRef variable As String) As List(Of Object)

        Dim tmp_list As New List(Of Object)
        Dim selection() As String = {}

        For Each id In filtervalues_hash.Keys
            If filtervalues_hash(id)(FILTER_ID_VARIABLE) = filter_id Then tmp_list.Add(filtervalues_hash(id)(variable))
        Next
        Return tmp_list

    End Function

    Friend Sub GetMostNestedFilterValuesDict(ByRef filter_id As Int32, _
                                             ByRef filter_value_id As Int32, _
                                             ByRef result_dict As Dictionary(Of Int32, List(Of Int32)))

        Dim children_filter_value_id As Dictionary(Of Int32, Int32) = GetFilterValueIDChildren(filter_value_id)
        If children_filter_value_id.Count > 0 Then
            For Each id As Int32 In children_filter_value_id.Keys
                GetMostNestedFilterValuesDict(children_filter_value_id(id), _
                                              ID_VARIABLE, _
                                              result_dict)
            Next
        Else
            If result_dict.ContainsKey(filter_id) = False Then
                Dim tmp_list As New List(Of Int32)
                result_dict.Add(filter_id, tmp_list)
            End If
            result_dict(filter_id).Add(filter_value_id)
        End If

    End Sub

    Friend Function GetFiltervaluesDictionary(ByRef filter_id As Int32, _
                                              ByRef Key As String, _
                                              ByRef Value As String) As Hashtable

        Dim tmpHT As New Hashtable
        For Each id In filtervalues_hash.Keys
            If filtervalues_hash(id)(FILTER_ID_VARIABLE) = filter_id Then tmpHT(CInt(filtervalues_hash(id)(Key))) = filtervalues_hash(id)(Value)
        Next
        Return tmpHT

    End Function

    ' load an index like hash based on filter_id/ entity_id au démarrage
    ' priority normal !! 

#End Region


#Region "Utilities"

    Friend Shared Sub GetFilterValueHTFromPacket(ByRef packet As ByteBuffer, ByRef filtervalue_ht As Hashtable)

        filtervalue_ht(NAME_VARIABLE) = packet.ReadString()
        filtervalue_ht(ID_VARIABLE) = packet.ReadUint32()
        filtervalue_ht(FILTER_ID_VARIABLE) = packet.ReadUint32()
        filtervalue_ht(PARENT_FILTER_VALUE_ID_VARIABLE) = packet.ReadUint32()
        filtervalue_ht(ITEMS_POSITIONS) = packet.ReadUint32()

    End Sub

    Private Sub WriteFilterValuePacket(ByRef packet As ByteBuffer, ByRef attributes As Hashtable)

        packet.WriteString(attributes(NAME_VARIABLE))
        If attributes.ContainsKey(ID_VARIABLE) Then packet.WriteUint32(attributes(ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_ID_VARIABLE))
        packet.WriteUint32(attributes(PARENT_FILTER_VALUE_ID_VARIABLE))
        packet.WriteUint32(attributes(ITEMS_POSITIONS))

    End Sub

    Private Function GetFilterValueIDChildren(ByRef filter_value_id As Int32) As Dictionary(Of Int32, Int32)

        Dim children_filters_values_id As New Dictionary(Of Int32, Int32)
        For Each id As Int32 In filtervalues_hash.Keys
            If filtervalues_hash(id)(PARENT_FILTER_VALUE_ID_VARIABLE) = filter_value_id Then
                children_filters_values_id.Add(id, filtervalues_hash(id)(FILTER_ID_VARIABLE))
            End If
        Next
        Return children_filters_values_id

    End Function

    Friend Function IsNameAvailable(ByRef name As String) As Boolean

        If GetFiltervaluesList(NAME_VARIABLE).Contains(name) = True Then Return False
        Return True

    End Function

#End Region





End Class
