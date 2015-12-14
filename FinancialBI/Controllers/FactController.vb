Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Drawing
Imports CRUD


Friend Class FactController

#Region "Instance Variables"

    ' Objects
    Private m_onSuccess As Action(Of List(Of Fact))
    Private m_onError As Action

#End Region


#Region "Initialize"

    Protected Friend Sub New()


    End Sub

#End Region


#Region "Interface"

    Friend Sub GetFact(ByRef p_accountId As UInt32, _
                       ByRef p_productId As UInt32, _
                       ByRef p_versionId As UInt32, _
                       ByRef p_startPeriod As UInt32, _
                       ByRef p_endPeriod As UInt32,
                       ByRef p_onSuccess As Action(Of List(Of Fact)), _
                       Optional ByRef p_onError As Action = Nothing)

        m_onSuccess = p_onSuccess
        m_onError = p_onError
        AddHandler Facts.Read, AddressOf ReadEvent

        Dim l_factsVersion As Version = GlobalVariables.Versions.GetValue(p_versionId)
        Facts.CMSG_GET_FACT(p_accountId, p_productId, p_versionId, p_startPeriod, p_endPeriod)

    End Sub

#End Region


#Region "Callback"

    Private Sub ReadEvent(p_status As Boolean, p_fact As List(Of Fact))
        If p_status = True Then m_onSuccess(p_fact)
        If p_status = False Then If Not m_onError Is Nothing Then m_onError()
    End Sub

#End Region


End Class
