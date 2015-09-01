' NewRatesVersionUI.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 24/08/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls


Friend Class NewRatesVersionUI


#Region "Instance Variables"

    Private Controller As ExchangeRatesController
    Protected Friend parent_node As vTreeNode = Nothing

#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_controller As ExchangeRatesController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        StartPeriodNUD.Value = Year(Now)

    End Sub

#End Region


#Region "Call Backs"

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs) Handles ValidateBT.Click

        Dim name As String = NameTB.Text
        If Len(name) < NAMES_MAX_LENGTH Then
            Controller.CreateVersion(name, 0, _
                                     StartPeriodNUD.Value, _
                                     NBPeriodsNUD.Value, _
                                     parent_node.Value)
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


    Private Sub NewRatesVersionUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()

    End Sub


#End Region



End Class