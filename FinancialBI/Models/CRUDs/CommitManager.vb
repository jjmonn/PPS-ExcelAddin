' CommitManager.vb
'
'
'
' Created by: Julien Monnereau
' Created on: 04/01/2016
' Last modified: 04/01/2016


Imports System.Collections
Imports System.Collections.Generic
Imports CRUD


Class CommitManager : Inherits CRUDManager

#Region "Instance variables"

    Private m_CommitsDic As New SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, UInt32, Commit))
    ' Sorted by entity id then can be accessed by id (primary key) or period id (secondary key)

#End Region

#Region "Init"

    Friend Sub New()

        CreateCMSG = ClientMessage.CMSG_CREATE_COMMIT
        ReadCMSG = ClientMessage.CMSG_READ_COMMIT
        UpdateCMSG = ClientMessage.CMSG_UPDATE_COMMIT
        ListCMSG = ClientMessage.CMSG_LIST_COMMIT

        ReadSMSG = ServerMessage.SMSG_READ_COMMIT_ANSWER
        UpdateSMSG = ServerMessage.SMSG_UPDATE_COMMIT_ANSWER
        ListSMSG = ServerMessage.SMSG_LIST_COMMIT_ANSWER
        DeleteSMSG = ServerMessage.SMSG_DELETE_COMMIT_ANSWER
        CreateSMSG = ServerMessage.SMSG_CREATE_COMMIT_ANSWER

        Build = AddressOf Commit.BuildCommit

        InitCallbacks()

    End Sub

#End Region

#Region "Interface"

    Friend Sub Initialize()

        List()

    End Sub

    Friend Sub UpdateCommitStatus(ByRef p_entityId As UInt32, ByRef p_period As UInt32, ByRef p_value As Byte)

        Dim l_Commit As Commit = GetValue(p_entityId, p_period)
        If l_Commit Is Nothing Then
            l_Commit = New Commit()
            l_Commit.Period = p_period
            l_Commit.EntityId = p_entityId
            l_Commit.Value = p_value
            Create(l_Commit)
        Else
            l_Commit.Value = p_value
            Update(l_Commit)
        End If

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
                Dim l_Commit As Commit = Build(packet)

                If m_CommitsDic.ContainsKey(l_Commit.EntityId) = False Then
                    m_CommitsDic(l_Commit.EntityId) = New MultiIndexDictionary(Of UInt32, UInt32, Commit)
                End If
                m_CommitsDic(l_Commit.EntityId).Set(l_Commit.Id, l_Commit.Period, l_Commit)
            Next
            RaiseObjectInitializedEvent()
            state_flag = True
        Else
            RaiseObjectInitializedEvent()
            state_flag = False
        End If

    End Sub

    Protected Overrides Sub ReadAnswer(packet As ByteBuffer)
        Dim l_Commit As Commit = Build(packet)

        If m_CommitsDic.ContainsKey(l_Commit.EntityId) = False Then
            m_CommitsDic(l_Commit.EntityId) = New MultiIndexDictionary(Of UInt32, UInt32, Commit)
        End If
        m_CommitsDic(l_Commit.EntityId).Set(l_Commit.Id, l_Commit.Period, l_Commit)
        RaiseReadEvent(packet.GetError(), l_Commit)
    End Sub

    Protected Overrides Sub DeleteAnswer(packet As ByteBuffer)
        Dim Id As UInt32 = packet.ReadUint32()

        For Each l_elem In m_CommitsDic.Values
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
        For Each l_elem In m_CommitsDic.Values
            If l_elem.ContainsKey(p_id) Then
                Return l_elem.PrimaryKeyItem(p_id)
            End If
        Next
        Return Nothing
    End Function

    Public Overrides Function GetValue(ByVal p_id As Int32) As CRUDEntity
        Return GetValue(CUInt(p_id))
    End Function

    Public Overloads Function GetValue(ByVal p_entityId As UInt32, ByVal p_period As UInt32) As Commit
        If m_CommitsDic.ContainsKey(p_entityId) = False Then Return Nothing
        Dim l_elem = m_CommitsDic(p_entityId)

        Return l_elem.SecondaryKeyItem(p_period)
    End Function

    Public Function GetDictionary() As SortedDictionary(Of UInt32, MultiIndexDictionary(Of UInt32, UInt32, Commit))
        Return m_CommitsDic
    End Function

    Public Function GetDictionary(ByVal p_entityId As UInt32) As MultiIndexDictionary(Of UInt32, UInt32, Commit)
        If m_CommitsDic.ContainsKey(p_entityId) = False Then Return Nothing
        Return m_CommitsDic(p_entityId)
    End Function

#End Region

End Class
