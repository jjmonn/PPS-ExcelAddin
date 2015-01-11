' CProgressBar.vb
'
'
'
'
' To do:
'       - 
'
'
'
' Known Bugs:
'       - position of the progress bar to the left not ok
'       - not transparent
'
'
' Author: Julien Monnereau
' Last modified: 22/08/2014


Imports H4xCode
Imports System.Drawing


Public Class ProgressBarControl


#Region "Instance Variables"

    'Objects
    Friend mProgressBar As New GaugeThisBar

    ' Variables
    Friend barProgress As Double
    Private LoadingBarIncrementValue As Double

    ' Constants
    Public Const PROGRESS_BAR_MAX_VALUE As Integer = 100
    Private Const PROGRESS_BAR_WIDTH As Integer = 225
    Private Const PROGRESS_BAR_HEIGHT As Integer = 17
    Private Const V_MARGIN As Int32 = 10
    Private Const H_MARGIN As Int32 = 10
    Private PROGRESS_BAR_COLOR_1 As Color = Color.LavenderBlush
    Private PROGRESS_BAR_COLOR_2 As Color = Color.Purple

#End Region


#Region "Initialize"

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        'Me.Width = PROGRESS_BAR_WIDTH + H_MARGIN
        'Me.Height = PROGRESS_BAR_HEIGHT + V_MARGIN
        Me.Controls.Add(mProgressBar)

        mProgressBar.Dock = Windows.Forms.DockStyle.Fill
        'mProgressBar.Left = (Me.Width - mProgressBar.Width) / 2
        'mProgressBar.Top = (Me.Height - mProgressBar.Height) / 2
        'mProgressBar.Width = PROGRESS_BAR_WIDTH
        'mProgressBar.Height = PROGRESS_BAR_HEIGHT
        mProgressBar.GradientColor1 = PROGRESS_BAR_COLOR_1
        mProgressBar.GradientColor2 = PROGRESS_BAR_COLOR_2
        mProgressBar.Value = 0
        mProgressBar.MaxValue = PROGRESS_BAR_MAX_VALUE
        Me.Hide()

    End Sub


#End Region


#Region "Interface"

    Friend Sub Launch(ByRef incrementalProgress As Double, Optional ByRef maxValue As Integer = 0)

        If maxValue <> 0 Then mProgressBar.MaxValue = maxValue
        barProgress = incrementalProgress
        mProgressBar.Value = 0
        Me.Show()
        Me.BringToFront()

    End Sub

    Friend Sub AddProgress(Optional ByRef progress As Double = 0)

        If progress = 0 Then progress = barProgress
        LoadingBarIncrementValue = mProgressBar.Value + progress
        If LoadingBarIncrementValue > mProgressBar.MaxValue Then
            mProgressBar.Value = 99
        End If
        mProgressBar.Value = LoadingBarIncrementValue
        mProgressBar.Update()

    End Sub

    Friend Sub EndProgress()

        mProgressBar.Value = mProgressBar.MaxValue
        Me.Hide()

    End Sub


#End Region


End Class
