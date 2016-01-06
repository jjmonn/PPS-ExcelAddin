Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Drawing


Friend Class AxisElemLogController

#Region "Instance Variables"

    ' Objects
    Private m_onSuccess As Action(Of List(Of CRUD.AxisElemLog))
    Private m_onError As Action

#End Region

#Region "Interface"

    Friend Sub GetAxisElemLog(ByRef p_axisType As CRUD.AxisType, _
                          ByRef p_startTimestamp As UInt64, _
                          ByRef p_endTimestamp As UInt64, _
                          ByRef p_onSuccess As Action(Of List(Of CRUD.AxisElemLog)), _
                          Optional ByRef p_onError As Action = Nothing)

        m_onSuccess = p_onSuccess
        m_onError = p_onError
        AddHandler AxisElemLogManager.Read, AddressOf ReadEvent
        AxisElemLogManager.CMSG_GET_AXIS_ELEM_LOG(p_axisType, p_startTimestamp, p_endTimestamp)

    End Sub

#End Region

#Region "Callback"

    Private Sub ReadEvent(p_status As Boolean, p_axisElemLogList As List(Of CRUD.AxisElemLog))
        If p_status = True Then m_onSuccess(p_axisElemLogList)
        If p_status = False Then If Not m_onError Is Nothing Then m_onError()
    End Sub

#End Region

End Class
