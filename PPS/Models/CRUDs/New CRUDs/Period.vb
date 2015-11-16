﻿' CPeriods.vb
'
' Return periods list according to the version parameter (month or years)
'
'
'
' To Do:
'       - Careful: Yealy periods have now to be stored as YEAR INTEGER !! (cf Exchange rates)
'       - //future evolution -> option to include n-2 in the periods (currently n-1 always included)
'       - 
'
' Known Bugs
'
'
'
' Auhtor: Julien Monnereau
' Last modified: 21/08/2015


Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq


Friend Class Period


#Region "Instance Variables"

    Friend Const NB_MONTHS As Int32 = 12



#End Region


#Region "Interface 2"

    Friend Shared Function GetYearsList(ByVal startPeriod As UInt32, _
                                         ByVal nbPeriod As UInt16, _
                                         ByVal timeConfig As UInt16) As Int32()

        Dim periodsList As New List(Of Int32)
        If timeConfig = CRUD.TimeConfig.MONTHS Then
            Dim yearId As Int32
            For Each monthId As Int32 In GetMonthsList(startPeriod, nbPeriod)
                yearId = GetYearIdFromMonthID(monthId)
                If periodsList.Contains(yearId) = False Then
                    periodsList.Add(yearId)
                End If
            Next
        Else
            periodsList.Add(startPeriod)

            Dim year__ As Double = startPeriod / 365.25 + 1900

            For i As Int16 = 1 To nbPeriod - 1
                If (DateTime.IsLeapYear(year__)) Then
                    startPeriod += 366
                Else
                    startPeriod += 365
                End If
                periodsList.Add(startPeriod)
                year__ += 1
            Next
        End If
        Return periodsList.ToArray

    End Function

    Friend Shared Function GetMonthsList(ByVal startPeriod As UInt32, _
                                         ByVal nbPeriod As UInt16) As Int32()

        Dim periodsList As New List(Of Int32)
        periodsList.Add(startPeriod)
        If (nbPeriod <= 1) Then Return periodsList.ToArray
        Dim monthList() As UInt16 = {31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}
        'Dim year As Double = CDbl(startPeriod) / 365.25 + 1900
        'Dim Month As UInt16 = year - Math.Truncate(year) + 1

        Dim currentYear As Double = Year(Date.FromOADate(startPeriod))
        Dim currentMonth As UInt16 = Month(Date.FromOADate(startPeriod))

        For i As UInt16 = 1 To nbPeriod - 1
            startPeriod += monthList(currentMonth Mod (12))
            If (currentMonth = 1 AndAlso DateTime.IsLeapYear(currentYear)) Then    ' february of a leap year
                startPeriod += 1
            End If
            periodsList.Add(startPeriod)
            If (currentMonth = 12) Then
                currentYear += 1
                currentMonth = 0
            End If
            currentMonth += 1
        Next

        Return periodsList.ToArray

    End Function

    ' yearId: "31/12/N" au format INT
    Friend Shared Function GetMonthsIdsInYear(ByRef yearId As UInt32, ByRef p_startPeriod As UInt32, ByRef p_nbPeriod As UInt16) As Int32()

        Dim nbDaysinMonth As Int32
        Dim tmpMonthList As Int32() = GetMonthsList(p_startPeriod, p_nbPeriod)
        Dim year_ As Int32 = Year(Date.FromOADate(yearId))
        Dim nbMonth As Int32

        If Year(Date.FromOADate(tmpMonthList(tmpMonthList.Length - 1))) < year_ Then nbMonth = 0
        If Year(Date.FromOADate(tmpMonthList(tmpMonthList.Length - 1))) > year_ Then nbMonth = 12
        If Year(Date.FromOADate(tmpMonthList(tmpMonthList.Length - 1))) = year_ Then nbMonth = Month(Date.FromOADate(tmpMonthList(tmpMonthList.Length - 1)))

        Dim monthsIds(nbMonth) As Int32

        For i = 1 To nbMonth
            nbDaysinMonth = DateTime.DaysInMonth(year_, i)
            monthsIds(i - 1) = (Int(CDbl(DateSerial(year_, i, nbDaysinMonth).ToOADate())))
        Next
        Return monthsIds

    End Function

#End Region


#Region "Interface"

    ' Provide monthly periods
    ' Reference year: year used for creating end of months dates
    ' PeriodMinusOneOption: true->include month-1 | false-> do not include
    Friend Shared Function GetMonthlyPeriodsList(ByRef start_period As Int32, _
                                                ByRef nb_periods As Int32, _
                                                ByRef PeriodMinusOneOption As Boolean) As List(Of Integer)

        Dim nbDaysinMonth As Int32
        Dim tmpList As New List(Of Integer)
        Dim currentPeriod = start_period + 1

        For i As Int32 = 1 To nb_periods
            Dim currentDate = DateTime.FromOADate(currentPeriod) 'miss february
            nbDaysinMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month)
            tmpList.Add(currentPeriod - 1)
            currentPeriod += nbDaysinMonth
        Next
        Return tmpList

    End Function

    Friend Shared Function GetMonthsPeriodsInOneYear(ByRef year As Int32, _
                                                     ByRef PeriodMinusOneOption As Boolean) As List(Of Integer)

        Dim nbDaysinMonth As Int32
        Dim tmpList As New List(Of Integer)
        tmpList.Add(Int(CDbl(DateSerial(year, 1, 0).ToOADate())))
        For i = 2 To NB_MONTHS
            nbDaysinMonth = DateTime.DaysInMonth(year, i)
            tmpList.Add(Int(CDbl(DateSerial(year, i - 1, nbDaysinMonth).ToOADate())))
        Next
        Return tmpList

    End Function

#End Region


#Region "Utilities"

    Friend Shared Function GetYearIDFromYearValue(ByRef year_value) As Int32

        Return Int(CDbl(DateSerial(year_value, 12, 31).ToOADate()))

    End Function

    Friend Shared Function GetYearValueFromYearID(ByRef yearId As Int32) As Int32

        Return Year(Date.FromOADate(CDbl(yearId)))

    End Function

    Friend Shared Function GetYearIdFromMonthID(ByRef monthId As Int32) As Int32

        Dim year_ As Int32 = Year(Date.FromOADate(monthId))
        Return Int(CDbl(DateSerial(year_, 12, 31).ToOADate()))

    End Function

#End Region


End Class