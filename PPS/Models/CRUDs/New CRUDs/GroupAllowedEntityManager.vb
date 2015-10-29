Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Friend Class GroupAllowedEntityManager : Inherits CRUDManager

#Region "Instance variables"

    Private m_groupAllowedEntityDic As New SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, UInt32, GroupAllowedEntity))

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_ADD_GROUP_ENTITY
        DeleteCMSG = ClientMessage.CMSG_DEL_GROUP_ENTITY
        ListCMSG = ClientMessage.CMSG_LIST_GROUP_ENTITIES

        CreateSMSG = ServerMessage.SMSG_ADD_GROUP_ENTITY_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_GROUP_ENTITY
        DeleteSMSG = ServerMessage.SMSG_DEL_GROUP_ENTITY_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_GROUP_ENTITIES_ANSWER

        Build = AddressOf GroupAllowedEntity.BuildGroupAllowedEntity

        InitCallbacks()

    End Sub

#End Region

#Region "CRUD"

#Region "Disabled"
    <Obsolete("Not implemented", True)> _
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub Update(ByRef p_crud As CRUDEntity)
    End Sub

    <Obsolete("Not implemented", True)> _
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub UpdateList(ByRef p_crudList As List(Of CRUDEntity))
    End Sub
#End Region

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)
        If packet.GetError() = ErrorMessage.SUCCESS Then
            For i As Int32 = 1 To packet.ReadInt32()
                Dim l_allowedEntity As GroupAllowedEntity = Build(packet)

                If m_groupAllowedEntityDic.ContainsKey(l_allowedEntity.GroupId) = False Then
                    m_groupAllowedEntityDic(l_allowedEntity.GroupId) = New MultiIndexDictionary(Of UInt32, UInt32, GroupAllowedEntity)
                End If
                m_groupAllowedEntityDic(l_allowedEntity.GroupId).Set(l_allowedEntity.Id, l_allowedEntity.EntityId, l_allowedEntity)
            Next
            RaiseObjectInitializedEvent()
            state_flag = True
        Else
            RaiseObjectInitializedEvent()
            state_flag = False
        End If

    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)
        Dim l_allowedEntity As GroupAllowedEntity = Build(packet)

        If m_groupAllowedEntityDic.ContainsKey(l_allowedEntity.GroupId) = False Then
            m_groupAllowedEntityDic(l_allowedEntity.GroupId) = New MultiIndexDictionary(Of UInt32, UInt32, GroupAllowedEntity)
        End If
        m_groupAllowedEntityDic(l_allowedEntity.GroupId).Set(l_allowedEntity.Id, l_allowedEntity.EntityId, l_allowedEntity)
        RaiseReadEvent(packet.GetError(), l_allowedEntity)
    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim Id As UInt32 = packet.ReadUint32()

        For Each l_group In m_groupAllowedEntityDic.Values
            If l_group.ContainsKey(Id) Then
                l_group.RemovePrimary(Id)
                Exit For
            End If
        Next
        RaiseDeleteEvent(packet.GetError() = 0, Id)
    End Sub

#End Region

#Region "Mapping"

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        For Each l_group In m_groupAllowedEntityDic.Values
            If l_group.ContainsKey(p_id) Then
                Return l_group.PrimaryKeyItem(p_id)
            End If
        Next
        Return Nothing
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Public Overloads Function GetValue(ByVal p_groupId As UInt32, ByVal p_entityId As UInt32) As GroupAllowedEntity
        Dim l_group = m_groupAllowedEntityDic(p_groupId)
        If l_group Is Nothing Then Return Nothing

        Return l_group.SecondaryKeyItem(p_entityId)
    End Function

    Public Function GetDictionary() As SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, UInt32, GroupAllowedEntity))
        Return m_groupAllowedEntityDic
    End Function

    Public Function GetDictionary(ByVal p_groupId As UInt32) As MultiIndexDictionary(Of UInt32, UInt32, GroupAllowedEntity)
        Return m_groupAllowedEntityDic(p_groupId)
    End Function

#End Region

End Class
