Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Drawing


Friend Class CommitLogController

#Region "Instance Variables"

    ' Objects
    Private m_onSuccess As Action(Of List(Of CRUD.CommitLog))
    Private m_onError As Action

#End Region

#Region "Interface"

    Friend Sub GetCommitLog(ByRef p_entityId As Int32, _
                          ByRef p_period As Int32, _
                          ByRef p_onSuccess As Action(Of List(Of CRUD.CommitLog)), _
                          Optional ByRef p_onError As Action = Nothing)

        m_onSuccess = p_onSuccess
        m_onError = p_onError
        AddHandler CommitLogManager.Read, AddressOf ReadEvent
        CommitLogManager.CMSG_GET_COMMIT_LOG(p_entityId, p_period)

    End Sub

#End Region

#Region "Callback"

    Private Sub ReadEvent(p_status As Boolean, p_commitLogList As List(Of CRUD.CommitLog))
        If p_status = True Then m_onSuccess(p_commitLogList)
        If p_status = False Then If Not m_onError Is Nothing Then m_onError()
    End Sub

#End Region

End Class
