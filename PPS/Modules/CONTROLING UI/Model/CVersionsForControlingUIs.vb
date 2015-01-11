' CVersionsForControlingUIs.vb
'
' Build versions lists, identify timeSetups and versions comparison configuration
' 
'
' To do:
'       -
'       -
'
'
' Known Bugs: 
'       - 
'
'
' Author: Julien Monnereau
' Last Modified: 25/08/2014

Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Collections


Public Class CVersionsForControlingUIs


#Region "Instance Variables"

    ' Objects
    Friend PERIODSMGT As New Periods

    ' Variables
    Friend versionsCodeTimeSetUpDict As Dictionary(Of String, Hashtable)
    Friend VersionsCodeArray() As String
    Friend VersionsNameArray() As String
    Friend versionsComparisonFlag As Int32 = -1

    ' Constants
    Friend Const YEARLY_VERSIONS_COMPARISON As Int32 = 0
    Friend Const MONTHLY_VERSIONS_COMPARISON As Int32 = 1
    Friend Const YEARLY_MONTHLY_VERSIONS_COMPARISON As Int32 = 2


#End Region


#Region "Initialize"

    Friend Sub New()

        versionsCodeTimeSetUpDict = VersionsMapping.GetVersionsTimeConfiguration()

    End Sub

#End Region


#Region "Interface"

    Friend Sub InitializeVersionsArray(ByRef versionsNodesList As List(Of TreeNode),
                                       ByRef periodsList As List(Of Integer))

        versionsComparisonFlag = -1
        If Not periodsList Is Nothing Then periodsList = Nothing
        If versionsNodesList.Count = 0 Then
            VersionsCodeArray = {GLOBALCurrentVersionCode}
            VersionsNameArray = {Version_Label.Caption}
            periodsList = GetPeriodList(VersionsCodeArray(0))
        Else
            ReDim VersionsCodeArray(versionsNodesList.Count - 1)
            ReDim VersionsNameArray(versionsNodesList.Count - 1)
            Dim i As Int32
            For Each Version As TreeNode In versionsNodesList
                If Version.Nodes.Count = 0 Then
                    VersionsCodeArray(i) = Version.Name
                    VersionsNameArray(i) = Version.Text
                    i = i + 1
                End If
            Next
            ReDim Preserve VersionsCodeArray(i - 1)
            ReDim Preserve VersionsNameArray(i - 1)
            IdentifyVersionsComparison()
            periodsList = GetSeveralVersionsPeriodsList()
        End If

    End Sub

    ' Return a list of Integer Periods corresponding to the param version code
    Friend Function GetPeriodList(ByRef versionCode) As List(Of Integer)

        Select Case versionsCodeTimeSetUpDict(versionCode)(VERSIONS_TIME_CONFIG_VARIABLE)
            Case YEARLY_TIME_CONFIGURATION : Return PERIODSMGT.yearlyPeriodList
            Case MONTHLY_TIME_CONFIGURATION : Return Periods.GetMonthlyPeriodsList(versionsCodeTimeSetUpDict(versionCode)(VERSIONS_REFERENCE_YEAR_VARIABLE), True)
            Case Else
                ' PPS Error tracking
                Return Nothing
        End Select

    End Function

    ' Return a list of Dates corresponding to the param version code
    Friend Function GetPeriodsDatesList(ByRef versionCode) As List(Of Date)

        Dim periodsList As List(Of Integer) = GetPeriodList(versionCode)
        Dim datesList As New List(Of Date)

        For Each period In periodsList
            datesList.Add(DateTime.FromOADate(period))
        Next

        Return datesList

    End Function


#End Region


#Region "Utilities"


    Private Sub IdentifyVersionsComparison()

        Dim timeSetup As String = versionsCodeTimeSetUpDict(VersionsCodeArray(0))(VERSIONS_TIME_CONFIG_VARIABLE)
        For i = 0 To VersionsCodeArray.Length - 1
            If timeSetup <> versionsCodeTimeSetUpDict(VersionsCodeArray(i))(VERSIONS_TIME_CONFIG_VARIABLE) Then
                versionsComparisonFlag = YEARLY_MONTHLY_VERSIONS_COMPARISON
                Exit Sub
            End If
        Next

        If timeSetup = MONTHLY_TIME_CONFIGURATION Then
            versionsComparisonFlag = MONTHLY_VERSIONS_COMPARISON
        Else
            versionsComparisonFlag = YEARLY_VERSIONS_COMPARISON
        End If

    End Sub

    Private Function GetSeveralVersionsPeriodsList() As List(Of Integer)

        Select Case versionsComparisonFlag
            Case YEARLY_VERSIONS_COMPARISON : Return PERIODSMGT.yearlyPeriodList

            Case MONTHLY_VERSIONS_COMPARISON
                Dim referenceYears As New List(Of Int32)
                For Each versionCode In VersionsCodeArray
                    Dim refYear As Int32 = versionsCodeTimeSetUpDict(versionCode)(VERSIONS_REFERENCE_YEAR_VARIABLE)
                    If Not referenceYears.Contains(refYear) Then referenceYears.Add(refYear)
                Next
                If referenceYears.Count = 1 Then
                    Return Periods.GetMonthlyPeriodsList(referenceYears(0), True)
                Else
                    Dim aggregatedPeriodsList As New List(Of Integer)
                    For Each refYear In referenceYears
                        Dim currentPeriodsList As List(Of Integer) = Periods.GetMonthlyPeriodsList(refYear, True)
                        For Each period As Integer In currentPeriodsList
                            If Not aggregatedPeriodsList.Contains(period) Then aggregatedPeriodsList.Add(period)
                        Next
                    Next
                    Return aggregatedPeriodsList
                End If

            Case YEARLY_MONTHLY_VERSIONS_COMPARISON : Return PERIODSMGT.yearlyPeriodList
            Case Else
                ' PPS Error tracking
                Return Nothing
        End Select


    End Function


#End Region



End Class
