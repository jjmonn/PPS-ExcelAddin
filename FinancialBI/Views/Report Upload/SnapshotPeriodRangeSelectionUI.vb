Imports System.Collections.Generic

' SnapshotPeriodRangeSelectionUI.vb
'
' Allows users to select the period range on which the snapshot will be based. 
'
'
' To do : factor controls with upload period range selection
'
' Create by: Julien Monnereau
' Created on: 06/01/2016
' Last modified: 06/01/2016



Public Class SnapshotPeriodRangeSelectionUI


#Region "Instance variables"

    Private m_addin As AddinModule
    Private m_periodSelectionControl As PeriodRangeSelectionControl

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_addin As AddinModule, _
                   ByRef p_versionId As UInt32)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_addin = p_addin
        MultilanguageSetup()
        m_periodSelectionControl = New PeriodRangeSelectionControl(p_versionId)
        m_periodSelectionPanel.Controls.Add(m_periodSelectionControl)
        m_periodSelectionControl.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Private Sub MultilanguageSetup()

        Me.Text = Local.GetValue("upload.periods_selection")
        m_validateButton.Text = Local.GetValue("general.validate")

    End Sub

  
#End Region


#Region "Call backs"

    Private Sub m_validateButton_Click(sender As Object, e As EventArgs) Handles m_validateButton.Click
        Dim l_weeksIdList As List(Of Int32) = Period.GetWeeksPeriodListFromPeriodsRange(m_periodSelectionControl.m_startDate.DateTimeEditor.Value.Value, _
                                                                                        m_periodSelectionControl.m_endDate.DateTimeEditor.Value.Value)
        Dim l_periods = Period.GetDaysPeriodsListFromWeeksId(l_weeksIdList)
        Me.Hide()
        m_addin.AssociateReportUploadControler(False, l_periods)
        Me.Close()
    End Sub

#End Region

End Class