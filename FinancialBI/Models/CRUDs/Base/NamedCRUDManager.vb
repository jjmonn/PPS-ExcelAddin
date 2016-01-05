Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports CRUD

Class NamedCRUDManager(Of T As {NamedCRUDEntity}) : Inherits CRUDManager

#Region "Instance variables"
    Protected m_CRUDDic As New MultiIndexDictionary(Of UInt32, String, T)
#End Region

#Region "CRUD"

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)
        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_CRUDDic.Clear()
            Dim nb_accounts = packet.ReadInt32()
            For i As Int32 = 1 To nb_accounts
                Dim tmp_crud As NamedCRUDEntity = Build(packet)

                m_CRUDDic.Set(tmp_crud.Id, tmp_crud.Name, tmp_crud)
            Next

            state_flag = True
            RaiseObjectInitializedEvent()
        Else
            state_flag = False
        End If
    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)
        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim tmp_crud As NamedCRUDEntity = MyBase.Build(packet)

            m_CRUDDic.Set(tmp_crud.Id, tmp_crud.Name, tmp_crud)
            RaiseReadEvent(packet.GetError(), tmp_crud)
        Else
            RaiseReadEvent(packet.GetError(), Nothing)
        End If
    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim id As UInt32 = packet.ReadUint32()
        If packet.GetError() = ErrorMessage.SUCCESS Then
            m_CRUDDic.Remove(id)
        End If
        RaiseDeleteEvent(packet.GetError(), id)
    End Sub

#End Region

#Region "Mapping"

    Public Function GetValueId(ByRef p_name As String) As UInt32

        If m_CRUDDic(p_name) Is Nothing Then Return 0
        Return m_CRUDDic(p_name).Id

    End Function

    Public Function GetValueName(ByRef p_id As UInt32) As String

        If m_CRUDDic(p_id) Is Nothing Then Return ""
        Return m_CRUDDic(p_id).Name

    End Function

    Public Overloads Function GetValue(ByVal p_name As String) As T
        If p_name Is Nothing Then Return Nothing
        Return m_CRUDDic(p_name)
    End Function

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        Return m_CRUDDic(p_id)
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return m_CRUDDic(CUInt(p_id))
    End Function

    Public Function GetDictionary() As MultiIndexDictionary(Of UInt32, String, T)
        Return m_CRUDDic
    End Function

#End Region

End Class
