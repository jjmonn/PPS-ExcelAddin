Imports System.Collections.Generic

Public Class PeriodRangeSelectionControl

#Region "New"

    Friend Sub New(ByRef p_versionId As UInt32)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        MultilanguageSetup()
        PeriodsRangeSetup(p_versionId)

        AddHandler m_startDate.ValueChanged, AddressOf m_startDate_ValueChanged
        AddHandler m_endDate.ValueChanged, AddressOf m_endDate_ValueChanged

    End Sub

    Private Sub MultilanguageSetup()

        m_startDateLabel.Text = Local.GetValue("submissionsFollowUp.start_date")
        m_endDateLabel.Text = Local.GetValue("submissionsFollowUp.end_date")

    End Sub

    Private Sub PeriodsRangeSetup(ByRef p_versionId As UInt32)

        m_startDate.FormatValue = "MM-dd-yy"
        m_endDate.FormatValue = "MM-dd-yy"

        Dim l_periods As Int32() = GlobalVariables.Versions.GetPeriodsList(p_versionId)
        If l_periods Is Nothing OrElse l_periods.Length = 0 Then Exit Sub

        m_startDate.MinDate = Date.FromOADate(l_periods(0))
        m_startDate.MaxDate = Date.FromOADate(l_periods(l_periods.Length - 1))
        m_endDate.MinDate = Date.FromOADate(l_periods(0))
        m_endDate.MaxDate = Date.FromOADate(l_periods(l_periods.Length - 1))

        Dim l_startDate As Date = Today.Date
        l_startDate = l_startDate.AddDays(-Period.m_nbDaysInWeek)

        If l_startDate >= m_startDate.MinDate _
        AndAlso l_startDate <= m_startDate.MaxDate Then
            m_startDate.Text = l_startDate
        Else
            m_startDate.Text = m_startDate.MinDate
        End If

        Dim l_endDate As Date = l_startDate.AddDays(Period.m_nbDaysInWeek * 3)
        If l_endDate >= m_endDate.MinDate _
        AndAlso l_endDate <= m_endDate.MaxDate Then
            m_endDate.Text = l_endDate
        Else
            m_endDate.Text = m_endDate.MaxDate
        End If

        m_startDate.DateTimeEditor.Value = m_startDate.Text
        m_endDate.DateTimeEditor.Value = m_endDate.Text

        GeneralUtilities.FillWeekTextBox(m_startWeekTB, m_startDate.Text)
        GeneralUtilities.FillWeekTextBox(m_endWeekTB, m_endDate.Text)

    End Sub

#End Region

#Region "Interface"

    Friend Function GetPeriodList() As List(Of Int32)

        'Dim l_weeksIdList As List(Of Int32) = Period.GetWeeksPeriodListFromPeriodsRange(m_startDate.DateTimeEditor.Value.Value, _
        '                                                                                m_endDate.DateTimeEditor.Value.Value)
        'Return Period.GetDaysPeriodsListFromWeeksId(l_weeksIdList)
        Dim l_periodsRange As New List(Of Int32)
        Dim l_date As Date = m_startDate.DateTimeEditor.Value.Value
        Do While l_date <= m_endDate.DateTimeEditor.Value.Value
            l_periodsRange.Add(l_date.ToOADate)
            l_date = l_date.AddDays(1)
        Loop
        Return l_periodsRange

    End Function

    Friend Sub ReinitializePeriodsRange(ByRef p_versionId As UInt32)
        PeriodsRangeSetup(p_versionId)
    End Sub

#End Region

#Region "Events"

    Private Sub m_startDate_ValueChanged(sender As Object, e As EventArgs)
        GeneralUtilities.FillWeekTextBox(m_startWeekTB, m_startDate.DateTimeEditor.Value.Value)
    End Sub

    Private Sub m_endDate_ValueChanged(sender As Object, e As EventArgs)
        GeneralUtilities.FillWeekTextBox(m_endWeekTB, m_endDate.DateTimeEditor.Value.Value)
    End Sub

#End Region

End Class
