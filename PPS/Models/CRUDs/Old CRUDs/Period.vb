' CPeriods.vb
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
        If timeConfig = GlobalEnums.TimeConfig.MONTHS Then
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
    Friend Shared Function GetMonthsIdsInYear(ByRef yearId As UInt32) As Int32()

        Dim nbDaysinMonth As Int32
        Dim monthsIds(12) As Int32
        Dim year_ As Int32 = Year(Date.FromOADate(yearId))
        For i = 1 To 12
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

    ' startPeriod becomes periodId ! (carefull changes everywhere)
    ' we want 
    Friend Shared Function GetYearsDict(ByRef startPeriodId As Int32, _
                                        ByRef nbPeriods As Int32) As Dictionary(Of Int32, Date)

        ' use function from server nath
        '    -> build periods_id
        '    -> years/ months period identifyer ?
        '       => in that case periods ID should be strings
        '       => and we can have tha same function with time_config as argument
        '       => and periods should be stored as string => changes DB schema


    End Function

    Friend Shared Function GetGlobalPeriodsDictionary(ByRef yearly_periods_list As List(Of Int32)) _
                                                      As Dictionary(Of Int32, List(Of Int32))

        Dim tmp_dic As New Dictionary(Of Int32, List(Of Int32))

        ' Add Period-1 (December of Year -1)
        Dim year_minus_one_id As Integer = GetYearIDFromYearValue(Year(Date.FromOADate(yearly_periods_list.ElementAt(0))) - 1)
        tmp_dic.Add(year_minus_one_id, GetLastMonthOfYear(Year(Date.FromOADate(year_minus_one_id))))

        ' Add all Years and their corresponding month dictionary
        For Each year_id In yearly_periods_list
            tmp_dic.Add(year_id, GetMonthsPeriodsInOneYear(Year(Date.FromOADate(year_id)), 0))
        Next
        Return tmp_dic

    End Function


    Friend Shared Function GetLastMonthOfYear(ByRef year_value As Int32) As List(Of Int32)

        Dim tmp_list As New List(Of Int32)
        tmp_list.Add(Int(CDbl(DateSerial(year_value, 12, 31).ToOADate())))
        Return tmp_list

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
