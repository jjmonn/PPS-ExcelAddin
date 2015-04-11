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
' Last modified: 09/04/2015


Imports System.Collections.Generic
Imports System.Collections
Imports System.Linq


Friend Class Period


#Region "Instance Variables"

    Protected Friend Const NB_MONTHS As Int32 = 12

#End Region


#Region "Initialize"

    Friend Sub New()


    End Sub

#End Region


#Region "Interface"

    ' Provide monthly periods
    ' Reference year: year used for creating end of months dates
    ' PeriodMinusOneOption: true->include month-1 | false-> do not include
    Protected Friend Shared Function GetMonthlyPeriodsList(ByRef start_period As Int32, _
                                                           ByRef nb_periods As Int32, _
                                                           ByRef PeriodMinusOneOption As Boolean) As List(Of Integer)

        Dim nbDaysinMonth As Int32
        Dim tmpList As New List(Of Integer)

        For j As Int32 = start_period To start_period + nb_periods - 1
            If PeriodMinusOneOption = True AndAlso j = start_period Then tmpList.Add(Int(CDbl(DateSerial(j - 1, 12, 31).ToOADate())))
            For i = 1 To NB_MONTHS
                nbDaysinMonth = DateTime.DaysInMonth(j, i)
                tmpList.Add(Int(CDbl(DateSerial(j, i, nbDaysinMonth).ToOADate())))
            Next
        Next
        Return tmpList

    End Function

    Protected Friend Shared Function GetMonthsPeriodsInOneYear(ByRef year As Int32, _
                                                               ByRef PeriodMinusOneOption As Boolean) As List(Of Integer)

        Dim nbDaysinMonth As Int32
        Dim tmpList As New List(Of Integer)
        If PeriodMinusOneOption = True Then tmpList.Add(Int(CDbl(DateSerial(year - 1, 12, 31).ToOADate())))
        For i = 1 To NB_MONTHS
            nbDaysinMonth = DateTime.DaysInMonth(year, i)
            tmpList.Add(Int(CDbl(DateSerial(year, i, nbDaysinMonth).ToOADate())))
        Next
        Return tmpList

    End Function

    Protected Friend Shared Function GetYearlyPeriodList(ByRef start_period As Int32, _
                                                         ByRef nb_periods As Int32) As List(Of Int32)

        Dim list As New List(Of Int32)
        Dim nbDaysinMonth As Int32 = DateTime.DaysInMonth(start_period, NB_MONTHS)
        For j As Int32 = 0 To nb_periods - 1
            list.Add(Int(CDbl(DateSerial(start_period + j, NB_MONTHS, nbDaysinMonth).ToOADate())))
        Next
        Return list

    End Function

    Protected Friend Shared Function GetGlobalPeriodsDictionary(ByRef yearly_periods_list As List(Of Int32)) _
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

    Protected Friend Shared Function GetGlobalPeriodsDictionary(ByRef start_period As Int32, _
                                                                ByRef nb_periods As Int32) As Dictionary(Of Integer, List(Of Int32))

        Return GetGlobalPeriodsDictionary(GetYearlyPeriodList(start_period, nb_periods))

    End Function

    Protected Friend Shared Function GetLastMonthOfYear(ByRef year_value As Int32) As List(Of Int32)

        Dim tmp_list As New List(Of Int32)
        tmp_list.Add(Int(CDbl(DateSerial(year_value, 12, 31).ToOADate())))
        Return tmp_list

    End Function

#End Region


#Region "Utilities"

    Protected Friend Shared Function GetYearIDFromYearValue(ByRef year_value) As Int32

        Return Int(CDbl(DateSerial(year_value, 12, 31).ToOADate()))

    End Function


#End Region


End Class
