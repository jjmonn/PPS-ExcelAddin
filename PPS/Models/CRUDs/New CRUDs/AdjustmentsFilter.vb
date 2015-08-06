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
' Last modified: 05/08/2015



Friend Class AdjustmentFilter

#Region "Instance variables"


    ' Variables
    Friend state_flag As Boolean
    Friend server_response_flag As Boolean
    Friend adjustmentsFilters_list As New List(Of Hashtable)
    Private request_id As Dictionary(Of UInt32, Boolean)
    Private fIdFvDict As New Dictionary(Of String, UInt32)

    ' Events
    Public Event ObjectInitialized()
    Public Event AdjustmentFilterCreationEvent(ByRef attributes As Hashtable)
    Public Shared Event AdjustmentFilterRead(ByRef attributes As Hashtable)
    Public Event AdjustmentFilterUpdateEvent()
    Public Event AdjustmentFilterDeleteEvent(ByRef id As UInt32)

#End Region


#Region "Init"

    Friend Sub New()

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
                adjustmentsFilters_list.Add(tmp_ht)
                fIdFvDict.Add(tmp_ht(ADJUSTMENT_ID_VARIABLE) & tmp_ht(FILTER_ID_VARIABLE), tmp_ht(FILTER_VALUE_ID_VARIABLE))
            Next
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_LIST_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_LIST_ADJUSTMENT_FILTER_ANSWER)
            server_response_flag = True
            RaiseEvent ObjectInitialized()
        Else
            server_response_flag = False
        End If

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
            Dim tmp_ht As New Hashtable
            GetAdjustmentFilterHTFromPacket(packet, tmp_ht)
            adjustmentsFilters_list.Add(tmp_ht)
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_CREATE_ADJUSTMENT_FILTER_ANSWER)
            RaiseEvent AdjustmentFilterCreationEvent(tmp_ht)
        Else

        End If

    End Sub

    Friend Shared Sub CMSG_READ_ADJUSTMENT_FILTER(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_READ_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_CREATE_ADJUSTMENTS_FILTER, UShort))
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Shared Sub SMSG_READ_ADJUSTMENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAdjustmentFilterHTFromPacket(packet, ht)
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_CREATE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_READ_ADJUSTMENT_FILTER_ANSWER)
            RaiseEvent AdjustmentFilterRead(ht)
        Else

        End If

    End Sub

    Friend Sub CMSG_UPDATE_ADJUSTMENT_FILTER(ByRef id As UInt32, _
                                  ByRef updated_var As String, _
                                  ByRef new_value As String)

        Dim tmp_ht As Hashtable = adjustmentsFilters_list(id).Clone ' check clone !!!!
        tmp_ht(updated_var) = new_value

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ADJUSTMENTS_FILTER, UShort))
        WriteAdjustmentFilterPacket(packet, tmp_ht)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Friend Sub CMSG_UPDATE_ADJUSTMENT_FILTER(ByRef attributes As Hashtable)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_UPDATE_ADJUSTMENTS_FILTER, UShort))
        WriteAdjustmentFilterPacket(packet, attributes)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER(packet As ByteBuffer)

        If packet.ReadInt32() = 0 Then
            Dim ht As New Hashtable
            GetAdjustmentFilterHTFromPacket(packet, ht)
            adjustmentsFilters_list(ht(ID_VARIABLE)) = ht
            NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_UPDATE_ADJUSTMENT_FILTER_ANSWER)
            RaiseEvent AdjustmentFilterUpdateEvent()
        Else

        End If

    End Sub

    Friend Sub CMSG_DELETE_ADJUSTMENT_FILTER(ByRef id As UInt32)

        NetworkManager.GetInstance().SetCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER)
        Dim packet As New ByteBuffer(CType(ClientMessage.CMSG_DELETE_ADJUSTMENTS_FILTER, UShort))
        packet.WriteUint32(id)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Private Sub SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER()

        '   If packet.ReadInt32() = 0 Then
        Dim id As UInt32
        ' get id from request_id ?
        NetworkManager.GetInstance().RemoveCallback(ServerMessage.SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER, AddressOf SMSG_DELETE_ADJUSTMENT_FILTER_ANSWER)
        RaiseEvent AdjustmentFilterDeleteEvent(id)

    End Sub

#End Region


#Region "Mappings"

    Friend Function GetFilteredAdjustmentIDs(ByRef filter_id As UInt32, _
                                         ByRef filter_value_id As UInt32) As List(Of UInt32)

        Dim adjustments_list As New List(Of UInt32)
        For Each ht In adjustmentsFilters_list
            If ht(FILTER_ID_VARIABLE) = filter_id _
            AndAlso ht(FILTER_VALUE_ID_VARIABLE) = filter_value_id Then
                adjustments_list.Add(ht(ADJUSTMENT_ID_VARIABLE))
            End If
        Next
        Return adjustments_list

    End Function

    Friend Function GetFilterValue(ByRef adjustment_id As UInt32, _
                                   ByRef filter_id As UInt32)

        Return fIdFvDict(adjustment_id & filter_id)

    End Function


#End Region


#Region "Utilities"

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



End Class
