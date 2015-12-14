Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Drawing


Friend Class LogController


#Region "Instance Variables"

    ' Objects
    Private m_onSuccess As Action(Of List(Of Hashtable))
    Private m_onError As Action

#End Region


#Region "Initialize"

    Protected Friend Sub New()


    End Sub

#End Region


#Region "Interface"

    Friend Sub GetFactLog(ByRef p_accountId As Int32, _
                          ByRef p_entityId As Int32, _
                          ByRef p_period As Int32, _
                          ByRef p_versionId As Int32, _
                          ByRef p_onSuccess As Action(Of List(Of Hashtable)), _
                          Optional ByRef p_onError As Action = Nothing)

        m_onSuccess = p_onSuccess
        m_onError = p_onError
        AddHandler FactLog.Read, AddressOf ReadEvent
        Dim ht As New Hashtable()

        ht.Add(ACCOUNT_ID_VARIABLE, p_accountId)
        ht.Add(ENTITY_ID_VARIABLE, p_entityId)
        ht.Add(PERIOD_VARIABLE, p_period)
        ht.Add(VERSION_ID_VARIABLE, p_versionId)
        FactLog.CMSG_GET_FACT_LOG(ht)

    End Sub

#End Region


#Region "Callback"

    Private Sub ReadEvent(p_status As Boolean, p_factLogList As List(Of Hashtable))
        If p_status = True Then m_onSuccess(p_factLogList)
        If p_status = False Then If Not m_onError Is Nothing Then m_onError()
    End Sub

#End Region


End Class
