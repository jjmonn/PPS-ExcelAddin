' NewRatesVersionUI.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 24/08/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls


Friend Class NewGlobalFactVersionUI


#Region "Instance Variables"

    Private m_controller As GlobalFactController
    Friend m_parentId As Int32

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As GlobalFactController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        StartPeriodNUD.Value = Year(Now)

    End Sub

#End Region


#Region "Call Backs"

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs) Handles ValidateBT.Click

        Dim name As String = NameTB.Text
        If Len(name) < NAMES_MAX_LENGTH AndAlso Len(name) > 0 Then
            m_controller.CreateVersion(m_parentId, _
                                         name, 0, _
                                         StartPeriodNUD.Value, _
                                         NBPeriodsNUD.Value)
            Me.Hide()
        Else
            MsgBox("The Name cannot exceed " & NAMES_MAX_LENGTH & " characters")
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()

    End Sub


#End Region


#Region "Events"


    Private Sub NewGlobalFactVersionUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()

    End Sub


#End Region



    Private Sub NewGlobalFactVersionUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class