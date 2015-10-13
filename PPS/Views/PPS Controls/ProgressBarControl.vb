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
    Friend m_progressBar As New GaugeThisBar

    ' Variables
    Friend m_incrementalProgress As Double

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
        Me.Controls.Add(m_progressBar)
        m_progressBar.Dock = Windows.Forms.DockStyle.Fill
        m_progressBar.GradientColor1 = PROGRESS_BAR_COLOR_1
        m_progressBar.GradientColor2 = PROGRESS_BAR_COLOR_2
        m_progressBar.Value = 0
        m_progressBar.MaxValue = PROGRESS_BAR_MAX_VALUE
        Me.Hide()

    End Sub


#End Region


#Region "Interface"

    Friend Sub Launch(ByRef p_incrementalProgress As Double, Optional ByRef p_maxValue As Integer = 0)

        If p_maxValue <> 0 Then m_progressBar.MaxValue = p_maxValue
        m_incrementalProgress = p_incrementalProgress
        m_progressBar.Value = 0
        Me.Show()
        Me.BringToFront()

    End Sub

    Friend Sub AddProgress(Optional ByRef p_progress As Double = 0)

        If p_progress = 0 Then p_progress = m_incrementalProgress
        Dim incrementalValue As Double = m_progressBar.Value + p_progress
        If incrementalValue > m_progressBar.MaxValue Then
            m_progressBar.Value = 99
        End If
        m_progressBar.Value = incrementalValue
        m_progressBar.Update()

    End Sub

    Friend Sub EndProgress()

        m_progressBar.Value = m_progressBar.MaxValue
        Me.Hide()

    End Sub


#End Region


End Class
