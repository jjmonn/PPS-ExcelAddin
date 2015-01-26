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
' Last modified: 20/01/2015


Imports System.Collections.Generic
Imports System.Collections


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
    Shared Function GetMonthlyPeriodsList(ByRef start_period As Int32, _
                                                    ByRef nb_periods As Int32, _
                                                    ByRef PeriodMinusOneOption As Boolean) As List(Of Integer)

        Dim nbDaysinMonth As Int32
        Dim tmpList As New List(Of Integer)

        For j As Int32 = start_period To start_period + nb_periods
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
                                                                As Dictionary(Of Integer, Integer())

        Dim tmp_dic As New Dictionary(Of Integer, Integer())
        For Each period In yearly_periods_list
            tmp_dic.Add(period, GetMonthsPeriodsInOneYear(Year(Date.FromOADate(period)), 0).ToArray)
        Next
        Return tmp_dic

    End Function


#End Region




End Class
