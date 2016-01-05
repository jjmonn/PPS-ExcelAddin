Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD

Class AxedCRUDManager(Of T As {AxedCRUDEntity, NamedCRUDEntity}) : Inherits CRUDManager

#Region "Instance variables"
    Protected m_CRUDDic As New SortedDictionary(Of AxisType, MultiIndexDictionary(Of UInt32, String, T))
#End Region

#Region "Init"
    Public Sub New()
        Clear()
    End Sub

    Private Sub Clear()
        m_CRUDDic.Clear()
        For Each l_axis In System.Enum.GetValues(GetType(AxisType))
            m_CRUDDic(l_axis) = New MultiIndexDictionary(Of UInt32, String, T)
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
            Dim tmp_filter As T = Build(packet)

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

    Public Overloads Function GetValue(ByVal p_axis As AxisType, ByVal p_name As String) As T
        If m_CRUDDic.ContainsKey(p_axis) = False Then Return Nothing
        Return m_CRUDDic(p_axis)(p_name)
    End Function

    Public Overloads Function GetValue(ByVal p_axis As AxisType, ByVal p_id As UInt32) As T
        If m_CRUDDic.ContainsKey(p_axis) = False Then Return Nothing
        Return m_CRUDDic(p_axis)(p_id)
    End Function

    Public Overloads Function GetValue(ByVal p_axis As AxisType, ByVal p_id As Int32) As T
        Return GetValue(p_axis, CUInt(p_id))
    End Function

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        For Each axis In m_CRUDDic.Values
            If axis.ContainsKey(p_id) = False Then Continue For

            Return axis(p_id)
        Next
        Return Nothing
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Friend Function GetDictionary(ByVal p_axis As AxisType) As MultiIndexDictionary(Of UInt32, String, T)
        If m_CRUDDic.ContainsKey(p_axis) = False Then Return Nothing
        Return m_CRUDDic(p_axis)
    End Function

    Friend Function GetDictionary(ByVal p_axis As AxisType, _
                                  ByVal p_axisParent As UInt32) As MultiIndexDictionary(Of UInt32, String, T)
        If m_CRUDDic.ContainsKey(p_axis) = False Then Return Nothing
        Dim l_multiIndexDict As New MultiIndexDictionary(Of UInt32, String, T)

        For Each l_axisElem As T In m_CRUDDic(p_axis).Values
            Dim l_axisParent As AxisParent = GlobalVariables.AxisParents.GetValue(l_axisElem.Id)
            If l_axisParent Is Nothing Then Continue For
            If l_axisParent.ParentId = p_axisParent Then
                l_multiIndexDict.Set(l_axisElem.Id, l_axisElem.Name, l_axisElem)
            End If
        Next
        Return l_multiIndexDict

    End Function

    Friend Function GetDictionary() As SortedDictionary(Of AxisType, MultiIndexDictionary(Of UInt32, String, T))
        Return m_CRUDDic
    End Function

    ' Utilities Methods
    Friend Function GetValueId(ByVal p_axis As AxisType, ByRef name As String) As UInt32

        Dim axisValue As T = GetValue(p_axis, name)

        If axisValue Is Nothing Then Return 0
        Return axisValue.Id

    End Function

    Friend Function GetValueName(ByVal p_axis As AxisType, ByRef p_id As UInt32) As String

        Dim axisValue As T = GetValue(p_axis, p_id)

        If axisValue Is Nothing Then Return ""
        Return axisValue.Name

    End Function

#End Region

End Class
