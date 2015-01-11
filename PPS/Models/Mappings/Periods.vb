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
' Last modified: 27/11/2014


Imports System.Collections.Generic
Imports System.Collections


Public Class Periods


#Region "Variables"

    ' Variables
    Friend yearlyPeriodList As New List(Of Integer)
    Friend starting_period As Int32
    Friend ending_period As Int32

    ' Const
    Public Const NB_MONTHS As Int32 = 12


#End Region


#Region "Initialize"

    Friend Sub New()

        Dim startAndEndPeriodsYearlySetup As Hashtable = ExtraDataMapping.GetStartAndEndingPeriod
        starting_period = startAndEndPeriodsYearlySetup(STARTING_PERIOD_KEY)
        ending_period = startAndEndPeriodsYearlySetup(ENDING_PERIOD_KEY)

        yearlyPeriodList.Add(Int(DateSerial(starting_period - 1, 12, 31).ToOADate()))
        For i As Int32 = starting_period To ending_period
            yearlyPeriodList.Add(Int(DateSerial(i, 12, 31).ToOADate()))
        Next

    End Sub

#End Region


#Region "Interface"

    ' Provide monthly periods
    ' Reference year: year used for creating end of months dates
    ' PeriodMinusOneOption: true->include month-1 | false-> do not include
    Public Shared Function GetMonthlyPeriodsList(ByRef referenceYear As Int32, _
                                                 ByRef PeriodMinusOneOption As Boolean) As List(Of Integer)

        Dim nbDaysinMonth As Int32
        Dim tmpList As New List(Of Integer)

        If PeriodMinusOneOption = True Then tmpList.Add(Int(CDbl(DateSerial(referenceYear - 1, 12, 31).ToOADate())))
        For i = 1 To NB_MONTHS
            nbDaysinMonth = DateTime.DaysInMonth(referenceYear, i)
            tmpList.Add(Int(CDbl(DateSerial(referenceYear, i, nbDaysinMonth).ToOADate())))
        Next
        Return tmpList

    End Function

    Friend Function GetGlobalPeriodsDictionary() As Dictionary(Of Integer, Integer())

        Dim tmp_dic As New Dictionary(Of Integer, Integer())
        For Each period In yearlyPeriodList
            tmp_dic.Add(period, GetMonthlyPeriodsList(Year(Date.FromOADate(period)), 0).ToArray)
        Next
        Return tmp_dic

    End Function

#End Region




End Class
