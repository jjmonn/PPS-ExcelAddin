﻿Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD

Public Class AxedCRUDManager(Of T As {AxedCRUDEntity, NamedCRUDEntity}) : Inherits CRUDManager

#Region "Instance variables"
    Protected m_CRUDDic As New SortedDictionary(Of AxisType, MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity))
#End Region

#Region "Init"
    Public Sub New()
        Clear()
    End Sub

    Private Sub Clear()
        m_CRUDDic.Clear()
        For Each l_axis In System.Enum.GetValues(GetType(AxisType))
            m_CRUDDic(l_axis) = New MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity)
        Next
    End Sub
#End Region

#Region "CRUD"

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_filter As T = Build(packet)

                m_CRUDDic(tmp_filter.Axis).Set(tmp_filter.Id, tmp_filter.Name, tmp_filter)
            Next
            state_flag = True
            RaiseObjectInitializedEvent()
        Else
            state_flag = False
        End If

    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim tmp_filter = Filter.BuildFilter(packet)

            m_CRUDDic(tmp_filter.Axis).Set(tmp_filter.Id, tmp_filter.Name, tmp_filter)
            RaiseReadEvent(packet.GetError(), tmp_filter)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)

        Dim id As UInt32 = packet.ReadUint32()
        If packet.GetError() = ErrorMessage.SUCCESS Then

            For Each axis In m_CRUDDic.Keys
                m_CRUDDic(axis).Remove(id)
            Next
            RaiseDeleteEvent(packet.GetError(), id)
        Else
            RaiseDeleteEvent(packet.GetError(), id)
        End If

    End Sub

#End Region

#Region "Mapping"

    Public Overloads Function GetValue(ByVal p_axis As AxisType, ByVal p_id As UInt32) As CRUDEntity
        Return m_CRUDDic(p_axis)(p_id)
    End Function

    Public Overloads Function GetValue(ByVal p_axis As AxisType, ByVal p_id As Int32) As CRUDEntity
        Return m_CRUDDic(p_axis)(CUInt(p_id))
    End Function

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        For Each axis In m_CRUDDic.Values
            Dim l_value As CRUDEntity = axis(p_id)

            If Not l_value Is Nothing Then Return l_value
        Next
        Return Nothing
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Friend Function GetDictionary(ByVal p_axis As AxisType) As MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity)
        Return m_CRUDDic(p_axis)
    End Function

    Friend Function GetDictionary() As SortedDictionary(Of AxisType, MultiIndexDictionary(Of UInt32, String, NamedCRUDEntity))
        Return m_CRUDDic
    End Function

    ' Utilities Methods
    Friend Function GetValueId(ByVal p_axis As AxisType, ByRef name As String) As UInt32

        Dim axisValue As AxisElem = m_CRUDDic(p_axis)(name)

        If axisValue Is Nothing Then Return 0
        Return axisValue.Id

    End Function

    Friend Function GetValueName(ByVal p_axis As AxisType, ByRef p_id As UInt32) As String

        Dim axisValue As AxisElem = m_CRUDDic(p_axis)(p_id)

        If axisValue Is Nothing Then Return ""
        Return axisValue.Name

    End Function

#End Region

End Class
