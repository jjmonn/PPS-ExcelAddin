Imports System.Collections.Generic

Public Class PeriodRangeSelectionControl

#Region "New"

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        PeriodsRangeSetup()

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


#Region "Interface"

    Friend Function GetPeriodList() As List(Of Int32)
        Dim l_weeksIdList As List(Of Int32) = Period.GetWeeksPeriodListFromPeriodsRange(m_startDate.DateTimeEditor.Value.Value, _
                                                                                        m_endDate.DateTimeEditor.Value.Value)
        Return Period.GetDaysPeriodsListFromWeeksId(l_weeksIdList)
    End Function

#End Region

#Region "Events"

    Private Sub m_startDate_ValueChanged(sender As Object, e As EventArgs) Handles m_startDate.ValueChanged
        GeneralUtilities.FillWeekTextBox(m_startWeekTB, m_startDate.DateTimeEditor.Value.Value)
    End Sub

    Private Sub m_endDate_ValueChanged(sender As Object, e As EventArgs) Handles m_endDate.ValueChanged
        GeneralUtilities.FillWeekTextBox(m_endWeekTB, m_endDate.DateTimeEditor.Value.Value)
    End Sub

#End Region

End Class
