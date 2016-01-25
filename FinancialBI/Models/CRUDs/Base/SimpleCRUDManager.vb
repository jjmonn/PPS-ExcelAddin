Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class SimpleCRUDManager : Inherits CRUDManager

#Region "Instance variables"

    ' Variables
    Private m_dic As New SortedDictionary(Of UInt32, CRUDEntity)
    Private request_id As Dictionary(Of UInt32, Boolean)

#End Region

#Region "CRUD"

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_dic.Clear()
            For i As Int32 = 1 To packet.ReadInt32()
                Dim tmp_axis = Build(packet)

                m_dic.Add(tmp_axis.Id, tmp_axis)
            Next
            state_flag = True
            RaiseObjectInitializedEvent()
        Else
            state_flag = False
            RaiseObjectInitializedEvent()
        End If

    End Sub

    ' add read => must listen in case somebody is already editing
    ' or block any CRUD editing when someone is already editing ?!
    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)

        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim value = Build(packet)

            If m_dic.ContainsKey(value.Id) Then
                m_dic(value.Id) = value
            Else
                m_dic.Add(value.Id, value)
            End If
            RaiseReadEvent(packet.GetError(), value)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If

    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim id As UInt32 = packet.ReadUint32()
        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_dic.Remove(id)
        End If
        RaiseDeleteEvent(packet.GetError(), id)
    End Sub

#End Region

#Region "Mapping"

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        If m_dic.ContainsKey(p_id) = False Then Return Nothing
        Return m_dic(p_id)
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

#End Region

End Class
