Imports System.Collections
Imports System.Collections.Generic


' AdjustmentFilter.vb
'
' CRUD for adjustmentsFilters table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 23/07/2015
' Last modified: 26/08/2015



Friend Class AdjustmentFilter


#Region "Instance variables"

    ' Variables
    Friend state_flag As Boolean
    Friend adjustmentsFiltersHash As New Dictionary(Of Int32, Dictionary(Of Int32, Int32))

    ' Events
    Public Event ObjectInitialized(ByRef status As Boolean)
    Public Event AdjustmentFilterCreationEvent(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event AdjustmentFilterDeleteEvent(ByRef status As Boolean, ByRef adjustmentId As UInt32, ByRef filterId As UInt32)
    Public Event AdjustmentFilterUpdateEvent(ByRef status As Boolean, ByRef attributes As Hashtable)


#End Region


#Region "Init"

    Friend Sub New()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_FILTER_ANSWER)
        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER)
        state_flag = False
        LoadAdjustmentFiltersTable()

    End Sub

    Friend Sub LoadAdjustmentFiltersTable()

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_LIST_ADJUSTMENTS_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_LIST_ADJUSTMENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As New Hashtable
                GetAdjustmentFilterHTFromPacket(packet, tmp_ht)
    
                AddRecordToAdjustmentsFiltersHash(tmp_ht(ADJUSTMENT_ID_VARIABLE), _
                                                tmp_ht(FILTER_ID_VARIABLE), _
                                                tmp_ht(FILTER_VALUE_ID_VARIABLE))
            Next
            state_flag = True
            RaiseEvent ObjectInitialized(True)
        Else
            state_flag = False
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_FILTER_ANSWER)

    End Sub


#End Region


#Region "CRUD"

    Friend Sub CMSG_CREATE_ADJUSTMENT_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ADJUSTMENTS_FILTER, UShort))
        WriteAdjustmentFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_CREATE_ADJUSTMENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            MsgBox(packet.ReadString())
            Dim ht As New Hashtable
            GetAdjustmentFilterHTFromPacket(packet, ht)
            RaiseEvent AdjustmentFilterCreationEvent(True, ht)
        Else
            RaiseEvent AdjustmentFilterCreationEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_READ_ADJUSTMENT_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_READ_ADJUSTMENTS_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_READ_ADJUSTMENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAdjustmentFilterHTFromPacket(packet, ht)
            AddRecordToAdjustmentsFiltersHash(ht(ADJUSTMENT_ID_VARIABLE), _
                                              ht(FILTER_ID_VARIABLE), _
                                              ht(FILTER_VALUE_ID_VARIABLE))
        Else
        End If

    End Sub

    Friend Sub CMSG_UPDATE_ADJUSTMENT_FILTER(ByRef adjustmentId As Int32, _
                                             ByRef filterId As Int32, _
                                             ByRef filterValueId As Int32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ADJUSTMENTS_FILTER, UShort))
        packet.WriteUint32(adjustmentId)
        packet.WriteUint32(filterId)
        packet.WriteUint32(filterValueId)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub


    Private Sub SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAdjustmentFilterHTFromPacket(packet, ht)
            RaiseEvent AdjustmentFilterUpdateEvent(True, ht)
        Else
            RaiseEvent AdjustmentFilterUpdateEvent(False, Nothing)
        End If
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER)

    End Sub

    Friend Sub CMSG_DELETE_ADJUSTMENT_FILTER(ByRef id As UInt32)

        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ADJUSTMENTS_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim adjustmentId As UInt32 = packet.ReadInt32
            Dim filterId As UInt32 = packet.ReadInt32
            RemoveRecordFromFiltersHash(adjustmentId, filterId)
            RaiseEvent AdjustmentFilterDeleteEvent(True, adjustmentId, filterId)
        Else
            RaiseEvent AdjustmentFilterDeleteEvent(False, 0, 0)
        End If

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetFilteredAdjustmentIDs(ByRef filter_id As UInt32, _
                                         ByRef filter_value_id As UInt32) As List(Of UInt32)

        Dim adjustments_list As New List(Of UInt32)
        For Each adjustmentId As Int32 In adjustmentsFiltersHash.Keys
            If adjustmentsFiltersHash(adjustmentId)(filter_id) = filter_value_id Then
                adjustments_list.Add(adjustmentId)
            End If
        Next
        Return adjustments_list

    End Function


#End Region


#Region "Utilities"

    Private Sub AddRecordToAdjustmentsFiltersHash(ByRef adjustmentId As Int32, _
                                              ByRef filterId As Int32, _
                                              ByRef filterValueId As Int32)

        If adjustmentsFiltersHash.ContainsKey(adjustmentId) Then
            If adjustmentsFiltersHash(adjustmentId).ContainsKey(filterId) Then
                adjustmentsFiltersHash(adjustmentId)(filterId) = filterValueId
            Else
                adjustmentsFiltersHash(adjustmentId).Add(filterId, filterValueId)
            End If
        Else
            adjustmentsFiltersHash.Add(adjustmentId, New Dictionary(Of Int32, Int32))
            adjustmentsFiltersHash(adjustmentId).Add(filterId, filterValueId)
        End If

    End Sub

    Private Sub RemoveRecordFromFiltersHash(ByRef adjustmentId As Int32, _
                                           ByRef filterId As Int32)

        On Error Resume Next
        adjustmentsFiltersHash(adjustmentId).Remove(filterId)

    End Sub

    Friend Shared Sub GetAdjustmentFilterHTFromPacket(ByRef packet As ByteBuffer, _
                                                  ByRef adjustmentFilter_ht As Hashtable)

        adjustmentFilter_ht(ADJUSTMENT_ID_VARIABLE) = packet.ReadUint32()
        adjustmentFilter_ht(FILTER_ID_VARIABLE) = packet.ReadUint32()
        adjustmentFilter_ht(FILTER_VALUE_ID_VARIABLE) = packet.ReadUint32()

    End Sub

    Private Sub WriteAdjustmentFilterPacket(ByRef packet As ByteBuffer, _
                                            ByRef attributes As Hashtable)

        packet.WriteUint32(attributes(ADJUSTMENT_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_VALUE_ID_VARIABLE))

    End Sub


#End Region


    Protected Overrides Sub finalize()

        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_READ_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_FILTER_ANSWER)
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER)
        MyBase.Finalize()

    End Sub



End Class
