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
        MultilangueSetup()

    End Sub

    Private Sub MultilangueSetup()

        Me.m_nameLabel.Text = Local.GetValue("general.name")
        Me.m_startingPeriodLabel.Text = Local.GetValue("facts_versions.starting_period")
        Me.m_numberPeriodsLabel.Text = Local.GetValue("facts_versions.nb_years")
        Me.CancelBT.Text = Local.GetValue("general.cancel")
        Me.ValidateBT.Text = Local.GetValue("general.create")
        Me.Text = Local.GetValue("global_facts.new_version")

    End Sub


#End Region


#Region "Call Backs"

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs) Handles ValidateBT.Click

        Dim name As String = NameTB.Text
        If Len(name) < NAMES_MAX_LENGTH AndAlso Len(name) > 0 Then
            m_controller.CreateVersion(m_parentId, _
                                         name, 0, _
                                         StartPeriodNUD.Value, _
                                         m_nb_years.Value * 12)
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


End Class