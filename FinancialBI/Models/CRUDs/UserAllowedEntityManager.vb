Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Class UserAllowedEntityManager : Inherits CRUDManager

#Region "Instance variables"

    Private m_userAllowedEntityDic As New SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, UInt32, UserAllowedEntity))

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_ADD_USER_ENTITY
        DeleteCMSG = ClientMessage.CMSG_DEL_USER_ENTITY
        ListCMSG = ClientMessage.CMSG_LIST_USER_ENTITIES

        CreateSMSG = ServerMessage.SMSG_ADD_USER_ENTITY_ANSWER
        ReadSMSG = ServerMessage.SMSG_READ_USER_ENTITY
        DeleteSMSG = ServerMessage.SMSG_DEL_USER_ENTITY_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_USER_ENTITIES_ANSWER

        Build = AddressOf UserAllowedEntity.BuildUserAllowedEntity

        InitCallbacks()

    End Sub

#End Region

#Region "CRUD"

#Region "Disabled"

    <Obsolete("Not implemented", True)> _
<System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub Delete(ByRef p_id As UInt32)
    End Sub

    <Obsolete("Not implemented", True)> _
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub Update(ByRef p_crud As CRUDEntity)
    End Sub

    <Obsolete("Not implemented", True)> _
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub UpdateList(ByRef p_crudList As List(Of CRUDEntity))
    End Sub
#End Region

    Friend Overloads Sub Delete(ByRef p_id As UInt32, ByRef p_userId As UInt32, ByRef p_entityId As UInt32)

        Dim packet As New ByteBuffer(CType(DeleteCMSG, UShort))
        packet.WriteUint32(p_id)
        packet.WriteUint32(p_userId)
        packet.WriteUint32(p_entityId)
        packet.Release()
        NetworkManager.GetInstance().Send(packet)

    End Sub

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)
        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim count As UInt32 = packet.ReadUint32()
            For i As Int32 = 1 To count
                Dim l_allowedEntity As UserAllowedEntity = Build(packet)

                If m_userAllowedEntityDic.ContainsKey(l_allowedEntity.UserId) = False Then
                    m_userAllowedEntityDic(l_allowedEntity.UserId) = New MultiIndexDictionary(Of UInt32, UInt32, UserAllowedEntity)
                End If
                m_userAllowedEntityDic(l_allowedEntity.UserId).Set(l_allowedEntity.Id, l_allowedEntity.EntityId, l_allowedEntity)
            Next
            RaiseObjectInitializedEvent()
            state_flag = True
        Else
            RaiseObjectInitializedEvent()
            state_flag = False
        End If

    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)
        Dim l_allowedEntity As UserAllowedEntity = Build(packet)

        If m_userAllowedEntityDic.ContainsKey(l_allowedEntity.UserId) = False Then
            m_userAllowedEntityDic(l_allowedEntity.UserId) = New MultiIndexDictionary(Of UInt32, UInt32, UserAllowedEntity)
        End If
        m_userAllowedEntityDic(l_allowedEntity.UserId).Set(l_allowedEntity.Id, l_allowedEntity.EntityId, l_allowedEntity)
        RaiseReadEvent(packet.GetError(), l_allowedEntity)
    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim Id As UInt32 = packet.ReadUint32()

        For Each l_user In m_userAllowedEntityDic.Values
            If l_user.ContainsKey(Id) Then
                l_user.RemovePrimary(Id)
                Exit For
            End If
        Next
        RaiseDeleteEvent(packet.GetError(), Id)
    End Sub

#End Region

#Region "Mapping"

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        For Each l_user In m_userAllowedEntityDic.Values
            If l_user.ContainsKey(p_id) Then
                Return l_user.PrimaryKeyItem(p_id)
            End If
        Next
        Return Nothing
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Public Overloads Function GetValue(ByVal p_userId As UInt32, ByVal p_entityId As UInt32) As UserAllowedEntity
        If m_userAllowedEntityDic.ContainsKey(p_userId) = False Then Return Nothing
        Dim l_user = m_userAllowedEntityDic(p_userId)

        Return l_user.SecondaryKeyItem(p_entityId)
    End Function

    Public Function GetDictionary() As SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, UInt32, UserAllowedEntity))
        Return m_userAllowedEntityDic
    End Function

    Public Function GetDictionary(ByVal p_userId As UInt32) As MultiIndexDictionary(Of UInt32, UInt32, UserAllowedEntity)
        If m_userAllowedEntityDic.ContainsKey(p_userId) = False Then Return Nothing
        Return m_userAllowedEntityDic(p_userId)
    End Function

#End Region

End Class
