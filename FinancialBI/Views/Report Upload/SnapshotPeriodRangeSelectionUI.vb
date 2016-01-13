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


#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_addin As AddinModule)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_addin = p_addin
        MultilanguageSetup()
        PeriodsRangeSetup()

    End Sub

    Private Sub MultilanguageSetup()

        Me.Text = Local.GetValue("upload.periods_selection")
        m_startDateLabel.Text = Local.GetValue("submissionsFollowUp.start_date")
        m_endDateLabel.Text = Local.GetValue("submissionsFollowUp.end_date")
        m_validateButton.Text = Local.GetValue("general.validate")

    End Sub

    Private Sub PeriodsRangeSetup()

        m_startDate.FormatValue = "MM-dd-yy"
        m_endDate.FormatValue = "MM-dd-yy"

        Dim l_todaysDate As Date = Today.Date
        m_startDate.Text = l_todaysDate.AddDays(-Period.m_nbDaysInWeek)
        m_endDate.Text = l_todaysDate.AddDays(Period.m_nbDaysInWeek * 3)

        m_startDate.DateTimeEditor.Value = m_startDate.Text
        m_endDate.DateTimeEditor.Value = m_endDate.Text

        GeneralUtilities.FillWeekTextBox(m_startWeekTB, m_startDate.Text)
        GeneralUtilities.FillWeekTextBox(m_endWeekTB, m_endDate.Text)

    End Sub

#End Region


#Region "Datepickers Events"

    Private Sub m_startDate_ValueChanged(sender As Object, e As EventArgs) Handles m_startDate.ValueChanged
        GeneralUtilities.FillWeekTextBox(m_startWeekTB, m_startDate.DateTimeEditor.Value.Value)
    End Sub

    Private Sub m_endDate_ValueChanged(sender As Object, e As EventArgs) Handles m_endDate.ValueChanged
        GeneralUtilities.FillWeekTextBox(m_endWeekTB, m_endDate.DateTimeEditor.Value.Value)
    End Sub

#End Region


#Region "Call backs"

    Private Sub m_validateButton_Click(sender As Object, e As EventArgs) Handles m_validateButton.Click
        Dim l_weeksIdList As List(Of Int32) = Period.GetWeeksPeriodListFromPeriodsRange(m_startDate.DateTimeEditor.Value.Value, _
                                                                                        m_endDate.DateTimeEditor.Value.Value)
        Dim l_periods = Period.GetDaysPeriodsListFromWeeksId(l_weeksIdList)
        Me.Hide()
        m_addin.AssociateReportUploadControler(False, l_periods)
        Me.Close()
    End Sub

#End Region

End Class