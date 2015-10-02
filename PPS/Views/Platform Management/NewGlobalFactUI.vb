' NewRatesVersionUI.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 24/08/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls


Friend Class NewGlobalFactUI


#Region "Instance Variables"

    Private m_controller As GlobalFactController

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As GlobalFactController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller

    End Sub

#End Region


#Region "Call Backs"

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs) Handles ValidateBT.Click

        Dim name As String = NameTB.Text
        If Len(name) < NAMES_MAX_LENGTH AndAlso Len(name) > 0 Then
            If m_controller.IsUsedName(name) Then MsgBox("This name is already used for another fact or account") Else _
                m_controller.CreateFact(name)
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


    Private Sub NewGlobalFactUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()

    End Sub


#End Region

End Class