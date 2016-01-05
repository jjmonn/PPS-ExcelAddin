' Period.vb
'
' Return periods list according to the version parameter (month or years)
'
'
' Auhtor: Julien Monnereau
' Last modified: 11/12/2015


Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq
Imports System.Globalization


Friend Class Period


#Region "Instance Variables"

    Friend Const m_nbMonthInYear As Int16 = 12
    Friend Const m_nbDaysInWeek As Int16 = 7
    Friend Const m_nbWeekdsInYear As Int16 = 52
    Friend Const m_nbDaysInYear As Int16 = 365

#End Region

#Region "Years interface"

    Friend Shared Function GetYearsList(ByVal p_startPeriod As UInt32, _
                                        ByVal p_nbPeriod As UInt16, _
                                        ByVal p_timeConfig As CRUD.TimeConfig) As Int32()

        Select Case p_timeConfig
            Case CRUD.TimeConfig.YEARS : Return GetYearsListFromYearlyConfiguration(p_startPeriod, p_nbPeriod).ToArray
            Case CRUD.TimeConfig.MONTHS : Return GetYearsListFromMonthlyConfiguration(p_startPeriod, p_nbPeriod).ToArray
            Case CRUD.TimeConfig.WEEK : Return GetYearsListFromWeeklyConfiguration(p_startPeriod, p_nbPeriod).ToArray
            Case CRUD.TimeConfig.DAYS : Return GetYearsListFromDailyConfiguration(p_startPeriod, p_nbPeriod).ToArray
            Case Else
                System.Diagnostics.Debug.WriteLine("Period.vb - Get YearsList() - Undefined time configuration")
                Return Nothing
        End Select
        Return Nothing

    End Function

    Private Shared Function GetYearsListFromYearlyConfiguration(ByVal p_startPeriod As Int32, ByVal p_nbPeriods As Int32) As List(Of Int32)

        Dim l_yearsIdList As New List(Of Int32)
        l_yearsIdList.Add(p_startPeriod)
        Dim l_year__ As Double = p_startPeriod / 365.25 + 1900

        For i As Int16 = 1 To p_nbPeriods - 1
            If (DateTime.IsLeapYear(l_year__)) Then
                p_startPeriod += 366
            Else
                p_startPeriod += 365
            End If
            l_yearsIdList.Add(p_startPeriod)
            l_year__ += 1
        Next
        Return l_yearsIdList

    End Function

    Private Shared Function GetYearsListFromMonthlyConfiguration(ByVal p_startPeriod As Int32, ByVal p_nbPeriods As Int32) As List(Of Int32)

        Dim l_yearsIdList As New List(Of Int32)
        Dim l_yearId As Int32
        For Each l_monthId As Int32 In GetMonthsList(p_startPeriod, p_nbPeriods, CRUD.TimeConfig.MONTHS)
            l_yearId = GetYearIdFromMonthID(l_monthId)
            If l_yearsIdList.Contains(l_yearId) = False Then
                l_yearsIdList.Add(l_yearId)
            End If
        Next
        Return l_yearsIdList

    End Function

    Private Shared Function GetYearsListFromWeeklyConfiguration(ByVal p_startPeriod As Int32, ByVal p_nbPeriods As Int32) As List(Of Int32)

        Dim l_yearsIdList As New List(Of Int32)
        Dim yearId As Int32
        For Each l_weekId As Int32 In GetWeeksList(p_startPeriod, p_nbPeriods, CRUD.TimeConfig.WEEK)
            yearId = Year(Date.FromOADate(l_weekId))
            If l_yearsIdList.Contains(yearId) = False Then
                l_yearsIdList.Add(yearId)
            End If
        Next
        Return l_yearsIdList

    End Function

    Private Shared Function GetYearsListFromDailyConfiguration(ByVal p_startPeriod As Int32, ByVal p_nbPeriods As Int32) As List(Of Int32)

        Dim l_yearsIdList As New List(Of Int32)
        Dim yearId As Int32
        For Each l_dayId As Int32 In GetDaysList(p_startPeriod, p_nbPeriods)
            yearId = Year(Date.FromOADate(l_dayId))
            If l_yearsIdList.Contains(yearId) = False Then
                l_yearsIdList.Add(yearId)
            End If
        Next
        Return l_yearsIdList

    End Function

#End Region

#Region "Months interface"

    Friend Shared Function GetMonthsList(ByVal p_startPeriod As UInt32, _
                                         ByVal p_nbPeriod As UInt16, _
                                         ByRef p_timeConfiguration As CRUD.TimeConfig) As Int32()

        Select Case p_timeConfiguration
            Case CRUD.TimeConfig.MONTHS : Return GetMonthsListFromMonthlyConfiguration(p_startPeriod, p_nbPeriod).ToArray
            Case CRUD.TimeConfig.WEEK : Return GetMonthsListFromWeeklyConfiguration(p_startPeriod, p_nbPeriod).ToArray
            Case CRUD.TimeConfig.DAYS : Return GetMonthsListFromDailyConfiguration(p_startPeriod, p_nbPeriod).ToArray
            Case CRUD.TimeConfig.YEARS
                System.Diagnostics.Debug.WriteLine("Period.vb - Get GetMonthsList() - yearly time configuration asked (years must not be broke down in months because not values on months if yearly config).")
                Return Nothing
            Case Else
                System.Diagnostics.Debug.WriteLine("Period.vb - Get GetMonthsList() - Undefined time configuration")
                Return Nothing
        End Select

    End Function

    Private Shared Function GetMonthsListFromMonthlyConfiguration(ByVal p_startPeriod As UInt32, ByVal p_nbPeriod As UInt16) As Int32()

        Dim l_periodsList As New List(Of Int32)
        l_periodsList.Add(p_startPeriod)
        If (p_nbPeriod <= 1) Then Return l_periodsList.ToArray
        Dim l_monthList() As UInt16 = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}

        Dim l_currentYear As Double = Year(Date.FromOADate(p_startPeriod))
        Dim l_currentMonth As UInt16 = Month(Date.FromOADate(p_startPeriod))

        For i As UInt16 = 1 To p_nbPeriod - 1
            p_startPeriod += l_monthList(l_currentMonth Mod (12))
            If (l_currentMonth = 1 AndAlso DateTime.IsLeapYear(l_currentYear)) Then    ' february of a leap year
                p_startPeriod += 1
            End If
            l_periodsList.Add(p_startPeriod)
            If (l_currentMonth = 12) Then
                l_currentYear += 1
                l_currentMonth = 0
            End If
            l_currentMonth += 1
        Next

        Return l_periodsList.ToArray

    End Function

    Private Shared Function GetMonthsListFromWeeklyConfiguration(ByVal p_startPeriod As UInt32, ByVal p_nbPeriods As UInt16) As List(Of Int32)

        Dim l_monthsIdList As New List(Of Int32)
        Dim l_monthId As Int32
        For Each l_weekId As Int32 In GetWeeksList(p_startPeriod, p_nbPeriods, CRUD.TimeConfig.WEEK)
            l_monthId = GetMonthIdFromPeriodId(l_weekId)
            If l_monthsIdList.Contains(l_monthId) = False Then
                l_monthsIdList.Add(l_monthId)
            End If
        Next
        Return l_monthsIdList

    End Function

    Private Shared Function GetMonthsListFromDailyConfiguration(ByVal p_startPeriod As UInt32, ByVal p_nbPeriods As UInt16) As List(Of Int32)

        Dim l_monthsIdList As New List(Of Int32)
        Dim l_monthId As Int32
        For Each l_dayId As Int32 In GetDaysList(p_startPeriod, p_nbPeriods)
            l_monthId = GetMonthIdFromPeriodId(l_dayId)
            If l_monthsIdList.Contains(l_monthId) = False Then
                l_monthsIdList.Add(l_monthId)
            End If
        Next
        Return l_monthsIdList

    End Function

    ' yearId: "31/12/N" au format INT
    Friend Shared Function GetMonthsIdsInYear(ByRef p_yearId As UInt32) As Int32()

        Dim l_monthsIdList(11) As Int32
        Dim l_monthId As Int32
        Dim l_year As Int32 = Year(Date.FromOADate(p_yearId))
        Dim l_monthList() As UInt16 = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        For l_monthIndex = 0 To 11
            l_monthId = DateSerial(l_year, l_monthIndex + 1, l_monthList(l_monthIndex)).ToOADate
            ' Add 1 day if february of a leap year
            If l_monthIndex = 1 AndAlso DateTime.IsLeapYear(l_year) Then
                l_monthId += 1
            End If
            l_monthsIdList(l_monthIndex) = l_monthId
        Next
        Return l_monthsIdList.ToArray

        ' dessous : méthode Nathanaël, à expliquer 

        'Dim nbDaysinMonth As Int32
        '' Note JM: below not ok -> 
        'Dim tmpMonthList As Int32() = GetMonthsList(p_startPeriod, p_nbPeriod, CRUD.TimeConfig.MONTHS)
        'Dim year_ As Int32 = Year(Date.FromOADate(p_yearId))
        'Dim nbMonth As Int32

        '' Note JM: à expliquer
        'If Year(Date.FromOADate(tmpMonthList(tmpMonthList.Length - 1))) < year_ Then nbMonth = 0
        'If Year(Date.FromOADate(tmpMonthList(tmpMonthList.Length - 1))) > year_ Then nbMonth = 12
        'If Year(Date.FromOADate(tmpMonthList(tmpMonthList.Length - 1))) = year_ Then nbMonth = Month(Date.FromOADate(tmpMonthList(tmpMonthList.Length - 1)))

        'Dim monthsIds(nbMonth) As Int32

        'For i = 1 To nbMonth
        '    nbDaysinMonth = DateTime.DaysInMonth(year_, i)
        '    monthsIds(i - 1) = (Int(CDbl(DateSerial(year_, i, nbDaysinMonth).ToOADate())))
        'Next
        'Return monthsIds

    End Function

#End Region

#Region "Weeks interface"

    Friend Shared Function GetWeeksList(ByVal p_startPeriod As UInt32, _
                                        ByVal p_nbPeriod As UInt16, _
                                        ByVal p_timeConfig As UInt16) As Int32()

        Select Case p_timeConfig
            Case CRUD.TimeConfig.WEEK : Return GetWeekIDListFromWeeklyConfig(p_startPeriod, p_nbPeriod).ToArray
            Case CRUD.TimeConfig.DAYS : Return GetWeekIDListFromDailyConfig(p_startPeriod, p_nbPeriod).ToArray
            Case CRUD.TimeConfig.MONTHS
                System.Diagnostics.Debug.WriteLine("Period.vb - GetWeeksList() - Monthly config version asked")
                Return {}
            Case CRUD.TimeConfig.YEARS
                System.Diagnostics.Debug.WriteLine("Period.vb - GetWeeksList() - Yearly config version asked")
                Return {}
        End Select
        Return {}

    End Function

    Private Shared Function GetWeekIDListFromWeeklyConfig(ByVal p_startPeriod As UInt32, ByVal p_nbPeriod As UInt16) As List(Of Int32)

        Dim l_weeksList As New List(Of Int32)
        Dim l_periodId As Int32 = p_startPeriod

        ' Check which day a week starts !!
        For i = 0 To p_nbPeriod - 1
            l_weeksList.Add(l_periodId)
            l_periodId += m_nbDaysInWeek
        Next
        Return l_weeksList

    End Function

    Private Shared Function GetWeekIDListFromDailyConfig(ByVal p_startPeriod As UInt32, ByVal p_nbPeriod As UInt16) As List(Of Int32)

        Dim l_weeksList As New List(Of Int32)
        Dim l_periodId As Int32 = p_startPeriod
        Dim l_currentWeekId As Int32
        For Each l_dayId As Int32 In GetDaysList(p_startPeriod, p_nbPeriod)
            l_currentWeekId = GetWeekIdFromPeriodId(l_dayId)
            If l_weeksList.Contains(l_currentWeekId) = False Then l_weeksList.Add(l_currentWeekId)
        Next
        Return l_weeksList

    End Function

    Friend Shared Function GetFirstDayOfWeekId(ByRef p_dayId As Int32) As Int32

        ' Uses localized settings for the first day of the week.
        If p_dayId = 0 Then
            Return 0
        End If
        ' to be checked !!!!!
        Dim l_weekId As Int32 = p_dayId - Weekday(Date.FromOADate(p_dayId), vbUseSystem)
        Return l_weekId

    End Function

#End Region

#Region "Days interface"
    ' To be checked 
    Friend Shared Function GetDaysList(ByVal p_startDayId As Int32, p_nbDays As Int32) As Int32()

        Dim l_daysList As New List(Of Int32)
        For i = 0 To p_nbDays - 1
            l_daysList.Add(p_startDayId)
            p_startDayId += 1
        Next
        Return l_daysList.ToArray

    End Function

    Friend Shared Function GetDaysIdListInWeek(ByRef p_weekId As Int32) As List(Of Int32)

        Dim l_daysIdList As New List(Of Int32)
        Dim l_dayId As Int32 = GetFirstDayOfWeekId(p_weekId)
        For i = 1 To 7
            l_daysIdList.Add(l_dayId)
            l_dayId += 1
        Next
        Return l_daysIdList
        ' to be checked !!
    End Function

#End Region


#Region "Utilities"

    Friend Shared Function GetYearIDFromYearValue(ByRef p_yearValue) As Int32

        Return Int(CDbl(DateSerial(p_yearValue, 12, 31).ToOADate()))

    End Function

    Friend Shared Function GetYearValueFromYearID(ByRef p_yearId As Int32) As Int32
        Return Year(Date.FromOADate(CDbl(p_yearId)))
    End Function

    Friend Shared Function GetYearIdFromPeriodId(ByRef p_periodId As Int32) As Int32
        Return GetYearIdFromMonthID(GetMonthIdFromPeriodId(p_periodId))
    End Function

    Friend Shared Function GetYearIdFromMonthID(ByRef p_monthId As Int32) As Int32

        Dim year_ As Int32 = Year(Date.FromOADate(p_monthId))
        Return Int(CDbl(DateSerial(year_, 12, 31).ToOADate()))

    End Function


    Friend Shared Function GetWeekIdFromPeriodId(ByRef p_dayId As Int32) As Int32

        ' Uses localized settings for the first day of the week.
        If p_dayId = 0 Then
            Return 0
        End If
        Dim l_weekId As Int32 = p_dayId - Weekday(Date.FromOADate(p_dayId), vbUseSystem) + 7
        Return l_weekId

    End Function

    ' Return the last day of month
    Friend Shared Function GetMonthIdFromPeriodId(ByRef p_periodId As Int32) As Int32

        Dim l_monthId As Int32
        Dim l_year As Int32 = Date.FromOADate(p_periodId).Year
        Dim l_monthList() As UInt16 = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        Dim l_monthnb As Int16 = Date.FromOADate(p_periodId).Month - 1
        l_monthId = DateSerial(l_year, Date.FromOADate(p_periodId).Month, l_monthList(l_monthnb)).ToOADate

        ' Add 1 day if february of a leap year
        If l_monthnb = 1 AndAlso DateTime.IsLeapYear(l_year) Then
            l_monthId += 1
        End If
        Return l_monthId

    End Function

    ' Compliant with ISO 860
    Friend Shared Function GetWeekNumberFromDateId(ByRef p_periodId As Int32) As Int16

        ' Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
        ' be the same week# as whatever Thursday, Friday or Saturday are,  and we always get those right
        Dim l_time As DateTime = Date.FromOADate(p_periodId)
        Dim l_day As DayOfWeek = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(l_time)
        If l_day >= DayOfWeek.Monday AndAlso l_day <= DayOfWeek.Wednesday Then
            l_time = l_time.AddDays(3)
        End If

        ' Return the week of our adjusted day
        Return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(l_time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday)

    End Function

   

#End Region


End Class
