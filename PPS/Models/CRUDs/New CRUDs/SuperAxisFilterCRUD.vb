Imports System.Collections
Imports System.Collections.Generic


Public MustInherit Class SuperAxisFilterCRUD

    ' Events
    Public Event Read(ByRef status As Boolean, ByRef attributes As Hashtable)
    Public Event UpdateEvent(ByRef status As Boolean, ByRef axis_id As Int32, ByRef filter_id As Int32, ByRef filter_value_id As Int32)

    Protected Sub OnRead(ByRef status As Boolean, ByRef attributes As Hashtable)
        RaiseEvent Read(status, attributes)
    End Sub

    Protected Sub OnUpdate(ByRef status As Boolean, ByRef axis_id As Int32, ByRef filter_id As Int32, ByRef filter_value_id As Int32)
        RaiseEvent UpdateEvent(status, axis_id, filter_id, filter_value_id)
    End Sub


    ' Variables
    Friend state_flag As Boolean
    Friend axisFiltersHash As New Dictionary(Of Int32, Dictionary(Of Int32, Int32))

    ' CRUD Methods
    Friend MustOverride Sub CMSG_UPDATE_AXIS_FILTER(ByRef axisId As Int32, _
                                                    ByRef filterId As Int32, _
                                                    ByRef filterValueId As Int32)

    ' Mapping Methods
    Friend Function GetFilteredAxisIDs(ByRef filter_id As UInt32, _
                                       ByRef filter_value_id As UInt32) As List(Of UInt32)

        Dim axis_list As New List(Of UInt32)
        For Each axisId As Int32 In axisFiltersHash.Keys
            If axisFiltersHash(axisId)(filter_id) = filter_value_id Then
                axis_list.Add(axisId)
            End If
        Next
        Return axis_list

    End Function

    Friend Function GetFilterValueId(ByRef filterId As Int32, _
                                     ByRef axisValueId As Int32) As Int32

        Dim mostNestedFilterId = GlobalVariables.Filters.GetMostNestedFilterId(filterId)
        Dim mostNestedFilterValueId = axisFiltersHash(axisValueId)(mostNestedFilterId)
        Return GlobalVariables.FiltersValues.GetFilterValueId(mostNestedFilterValueId, filterId)

    End Function

    ' Utilities Functions
    Protected Sub AddRecordToaxisFiltersHash(ByRef axisId As Int32, _
                                           ByRef filterId As Int32, _
                                           ByRef filterValueId As Int32)

        If axisFiltersHash.ContainsKey(axisId) Then
            If axisFiltersHash(axisId).ContainsKey(filterId) Then
                axisFiltersHash(axisId)(filterId) = filterValueId
            Else
                axisFiltersHash(axisId).Add(filterId, filterValueId)
            End If
        Else
            axisFiltersHash.Add(axisId, New Dictionary(Of Int32, Int32))
            axisFiltersHash(axisId).Add(filterId, filterValueId)
        End If

    End Sub

    Protected Sub RemoveRecordFromFiltersHash(ByRef axisId As Int32, _
                                            ByRef filterId As Int32)

        On Error Resume Next
        axisFiltersHash(CInt(axisId)).Remove(CInt(filterId))

    End Sub

    Friend Shared Sub GetAxisFilterHTFromPacket(ByRef packet As ByteBuffer, _
                                                ByRef axisFilter_ht As Hashtable)

        axisFilter_ht(AXIS_ID_VARIABLE) = packet.ReadUint32()
        axisFilter_ht(FILTER_ID_VARIABLE) = packet.ReadUint32()
        axisFilter_ht(FILTER_VALUE_ID_VARIABLE) = packet.ReadUint32()

    End Sub

    Protected Sub WriteAxisFilterPacket(ByRef packet As ByteBuffer, _
                                        ByRef attributes As Hashtable)

        packet.WriteUint32(attributes(AXIS_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_ID_VARIABLE))
        packet.WriteUint32(attributes(FILTER_VALUE_ID_VARIABLE))

    End Sub



End Class
