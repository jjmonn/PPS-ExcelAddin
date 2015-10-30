Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

' FilterValue2.vb
'
' CRUD for filtervalues table - relation with c++ server
'
'
'
'
' Author: Julien Monnereau
' Created: 23/07/2015
' Last modified: 01/09/2015

Friend Class FilterValueManager : Inherits CRUDManager

#Region "Instance variables"

    Private m_filterValuesDic As New SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, String, FilterValue))

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_FILTER_VALUE
        ReadCMSG = ClientMessage.CMSG_READ_FILTER_VALUE
        UpdateCMSG = ClientMessage.CMSG_UPDATE_FILTER_VALUE
        UpdateListCMSG = ClientMessage.CMSG_UPDATE_FILTER_VALUE_LIST
        DeleteCMSG = ClientMessage.CMSG_DELETE_FILTER_VALUE
        ListCMSG = ClientMessage.CMSG_LIST_FILTER_VALUE

        CreateSMSG = ServerMessage.SMSG_CREATE_FILTERS_VALUE_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_FILTERS_VALUE_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_FILTERS_VALUE_ANSWER
        UpdateListSMSG = ServerMessage.SMSG_CRUD_FILTER_VALUE_LIST_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_FILTERS_VALUE_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_FILTER_VALUE_ANSWER

        Build = AddressOf FilterValue.BuildFilterValue

        InitCallbacks()

    End Sub

#End Region

#Region "CRUD"

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_filterValuesDic.Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_ht As FilterValue = Build(packet)

                If m_filterValuesDic.ContainsKey(tmp_ht.FilterId) = False Then m_filterValuesDic(tmp_ht.FilterId) = New MultiIndexDictionary(Of UInteger, String, FilterValue)
                m_filterValuesDic(tmp_ht.FilterId).Set(tmp_ht.Id, tmp_ht.Name, tmp_ht)
            Next
            state_flag = True
            RaiseObjectInitializedEvent()
        Else
            state_flag = False
        End If

    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim tmp_ht As FilterValue = Build(packet)

            If m_filterValuesDic.ContainsKey(tmp_ht.FilterId) = False Then m_filterValuesDic(tmp_ht.FilterId) = New MultiIndexDictionary(Of UInteger, String, FilterValue)
            m_filterValuesDic(tmp_ht.FilterId).Set(tmp_ht.Id, tmp_ht.Name, tmp_ht)
            RaiseReadEvent(packet.GetError(), tmp_ht)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim id As UInt32 = packet.ReadInt32()

        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_filterValuesDic.Remove(CInt(id))
        End If
        RaiseDeleteEvent(packet.GetError(), id)

    End Sub

#End Region

#Region "Mappings"

    Friend Sub GetMostNestedFilterValuesDict(ByRef p_filterId As Int32, _
                                             ByRef p_filterValueId As Int32, _
                                             ByRef p_resultDic As Dictionary(Of Int32, List(Of Int32)))

        Dim filterId As Dictionary(Of Int32, Int32) = GetChildrenFilterValueIdsFilterIdDic(p_filterValueId)
        If filterId.Count > 0 Then
            For Each filterValueId As Int32 In filterId.Keys
                GetMostNestedFilterValuesDict(filterId(filterValueId), _
                                              filterValueId, _
                                              p_resultDic)
            Next
        Else
            If p_resultDic.ContainsKey(p_filterId) = False Then
                Dim tmp_list As New List(Of Int32)
                p_resultDic.Add(p_filterId, tmp_list)
            End If
            p_resultDic(p_filterId).Add(p_filterValueId)
        End If

    End Sub

    Friend Function GetDictionary(ByRef filter_id As Int32) As MultiIndexDictionary(Of UInt32, String, FilterValue)

        If m_filterValuesDic.ContainsKey(filter_id) = False Then Return Nothing
        Return m_filterValuesDic(filter_id)

    End Function

    Friend Function GetDictionary() As SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, String, FilterValue))

        Return m_filterValuesDic

    End Function

    Friend Function GetValueId(ByRef name As String) As UInt32

        Dim filterValue As FilterValue = GetValue(name)

        If filterValue Is Nothing Then Return 0
        Return filterValue.Id

    End Function

    Friend Function GetValueName(ByRef p_id As UInt32) As String

        Dim filterValue As FilterValue = GetValue(p_id)

        If filterValue Is Nothing Then Return ""
        Return filterValue.Name

    End Function

    Public Overloads Function GetValue(ByRef name As String) As FilterValue

        For Each filterValueSet As MultiIndexDictionary(Of UInt32, String, FilterValue) In m_filterValuesDic.Values
            Dim filterValue As FilterValue = filterValueSet(name)

            If Not filterValue Is Nothing Then Return filterValue
        Next
        Return Nothing

    End Function

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity

        For Each filterValueSet As MultiIndexDictionary(Of UInt32, String, FilterValue) In m_filterValuesDic.Values
            Dim filterValue As FilterValue = filterValueSet(p_id)

            If Not filterValue Is Nothing Then Return filterValue
        Next
        Return Nothing

    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Public Overloads Function GetValue(ByRef p_filterId As UInt32, ByRef p_id As UInt32) As FilterValue

        If m_filterValuesDic.ContainsKey(p_filterId) = False Then Return Nothing
        Return m_filterValuesDic(p_filterId)(p_id)

    End Function

    Friend Function GetFilterValueIdsFromParentFilterValueIds(ByRef parentFilterValuesIds() As Int32) As Int32()

        Dim filterValuesIdsList As New List(Of Int32)

        For Each parentFilterValueId As Int32 In parentFilterValuesIds
            For Each filterValueSet As MultiIndexDictionary(Of UInt32, String, FilterValue) In m_filterValuesDic.Values
                For Each filterValue As FilterValue In filterValueSet.Values
                    If filterValue.ParentId = parentFilterValueId Then
                        filterValuesIdsList.Add(filterValue.Id)
                    End If
                Next
            Next
        Next
        Return filterValuesIdsList.ToArray

    End Function

#End Region

#Region "Utilities"

    Friend Function GetChildrenFilterValueIdsFilterIdDic(ByRef parent_filter_value_id As Int32) As Dictionary(Of Int32, Int32)

        Dim children_filters_values_id As New Dictionary(Of Int32, Int32)
        For Each filterValueSet As MultiIndexDictionary(Of UInt32, String, FilterValue) In m_filterValuesDic.Values
            For Each filterValue As FilterValue In filterValueSet.Values
                If filterValue.ParentId = parent_filter_value_id Then
                    children_filters_values_id.Add(filterValue.Id, filterValue.FilterId)
                End If
            Next
        Next
        Return children_filters_values_id

    End Function

    Friend Function GetFilterValueId(ByVal mostNestedFilterValueId As UInt32, _
                                     ByRef filterId As UInt32) As UInt32

        Dim filterValue As FilterValue = GetValue(mostNestedFilterValueId)
        If filterValue Is Nothing Then
            Return 0
        End If
        If filterValue.FilterId = filterId Then
            Return mostNestedFilterValueId
        Else
            mostNestedFilterValueId = filterValue.ParentId
            Return GetFilterValueId(mostNestedFilterValueId, filterId)
        End If

    End Function

    Friend Function IsNameAvailable(ByRef name As String) As Boolean

        If GetValue(name) Is Nothing Then Return False
        Return True

    End Function

#End Region

End Class
