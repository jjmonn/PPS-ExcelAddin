' Created by: Nathanaël Landais
' Last modified: 02/12/2015


Imports System.Collections
Imports System.Collections.Generic
Imports CRUD

Friend Class EntityDistributionManager : Inherits CRUDManager

#Region "Instance variables"

    Private m_entityDistributionDic As New SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, UInt32, EntityDistribution))
    ' sorted by entity id then can be access by id (primary key) or account_id (secondary key)

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_ENTITY_DISTRIBUTION
        ReadCMSG = ClientMessage.CMSG_READ_ENTITY_DISTRIBUTION
        UpdateCMSG = ClientMessage.CMSG_UPDATE_ENTITY_DISTRIBUTION
        ListCMSG = ClientMessage.CMSG_LIST_ENTITY_DISTRIBUTION

        ReadSMSG = ServerMessage.SMSG_READ_ENTITY_DISTRIBUTION_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_ENTITY_DISTRIBUTION_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_ENTITY_DISTRIBUTION_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_ENTITY_DISTRIBUTION_ANSWER
        CreateSMSG = ServerMessage.SMSG_CREATE_ENTITY_DISTRIBUTION_ANSWER

        Build = AddressOf EntityDistribution.BuildEntityDistribution

        InitCallbacks()

    End Sub

#End Region

#Region "CRUD"

#Region "Disabled"

    <Obsolete("Not implemented", True)> _
    <System.ComponentModel.EditorBrowsable(ComponentModel.EditorBrowsableState.Never)>
    Friend Overrides Sub Delete(ByRef p_id As UInt32)
    End Sub

#End Region

    Protected Overrides Sub ListAnswer(packet As ByteBuffer)
        If packet.GetError() = ErrorMessage.SUCCESS Then
            Dim count As UInt32 = packet.ReadUint32()
            For i As Int32 = 1 To count
                Dim l_entityDistribution As EntityDistribution = Build(packet)

                If m_entityDistributionDic.ContainsKey(l_entityDistribution.AccountId) = False Then
                    m_entityDistributionDic(l_entityDistribution.AccountId) = New MultiIndexDictionary(Of UInt32, UInt32, EntityDistribution)
                End If
                m_entityDistributionDic(l_entityDistribution.AccountId).Set(l_entityDistribution.Id, l_entityDistribution.EntityId, l_entityDistribution)
            Next
            RaiseObjectInitializedEvent()
            state_flag = True
        Else
            RaiseObjectInitializedEvent()
            state_flag = False
        End If

    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)
        Dim l_entityDistribution As EntityDistribution = Build(packet)

        If m_entityDistributionDic.ContainsKey(l_entityDistribution.AccountId) = False Then
            m_entityDistributionDic(l_entityDistribution.AccountId) = New MultiIndexDictionary(Of UInt32, UInt32, EntityDistribution)
        End If
        m_entityDistributionDic(l_entityDistribution.AccountId).Set(l_entityDistribution.Id, l_entityDistribution.EntityId, l_entityDistribution)
        RaiseReadEvent(packet.GetError(), l_entityDistribution)
    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim Id As UInt32 = packet.ReadUint32()

        For Each l_elem In m_entityDistributionDic.Values
            If l_elem.ContainsKey(Id) Then
                l_elem.RemovePrimary(Id)
                Exit For
            End If
        Next
        RaiseDeleteEvent(packet.GetError() = 0, Id)
    End Sub

#End Region

#Region "Mapping"

    Public Overrides Function GetValue(ByVal p_id As UInt32) As CRUDEntity
        For Each l_elem In m_entityDistributionDic.Values
            If l_elem.ContainsKey(p_id) Then
                Return l_elem.PrimaryKeyItem(p_id)
            End If
        Next
        Return Nothing
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Public Overloads Function GetValue(ByVal p_entityId As UInt32, ByVal p_accountId As UInt32) As EntityDistribution
        If m_entityDistributionDic.ContainsKey(p_accountId) = False Then Return Nothing
        Dim l_elem = m_entityDistributionDic(p_accountId)

        Return l_elem.SecondaryKeyItem(p_entityId)
    End Function

    Public Function GetDictionary() As SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, UInt32, EntityDistribution))
        Return m_entityDistributionDic
    End Function

    Public Function GetDictionary(ByVal p_accountId As UInt32) As MultiIndexDictionary(Of UInt32, UInt32, EntityDistribution)
        If m_entityDistributionDic.ContainsKey(p_accountId) = False Then Return Nothing
        Return m_entityDistributionDic(p_accountId)
    End Function

#End Region

End Class
