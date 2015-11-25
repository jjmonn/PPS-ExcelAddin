' NewRatesVersionUI.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 09/11/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls


Friend Class NewRatesVersionUI


#Region "Instance Variables"

    Private m_controller As ExchangeRatesController
    Friend m_parentId As Int32

#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_controller As ExchangeRatesController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = input_controller
        StartPeriodNUD.Value = Year(Now)
        MultilanguageSetup()

        m_circularProgress.Visible = False
        AddHandler m_creationBackgroundWorker.DoWork, AddressOf CreationBackgroundWorker_DoWork
  
    End Sub

    Private Sub MultilanguageSetup()

        Me.m_nameLabel.Text = Local.GetValue("general.name")
        Me.m_startingYearLabel.Text = Local.GetValue("facts_versions.starting_period")
        Me.m_numberOfYearsLabel.Text = Local.GetValue("facts_versions.nb_years")
        Me.CancelBT.Text = Local.GetValue("general.cancel")
        Me.ValidateBT.Text = Local.GetValue("general.create")
        Me.Text = Local.GetValue("new_rates_version")

    End Sub

#End Region


#Region "Call Backs"

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs) Handles ValidateBT.Click

        Dim l_name As String = NameTB.Text
        If Len(l_name) < NAMES_MAX_LENGTH Then

            m_circularProgress.Visible = True
            m_circularProgress.Enabled = True
            m_circularProgress.Start()
            m_creationBackgroundWorker.RunWorkerAsync()

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


#Region "Background Workers"


    Private Sub CreationBackgroundWorker_DoWork()

        m_controller.CreateVersion(m_parentId, _
                                   NameTB.Text, _
                                   0, _
                                   StartPeriodNUD.Value,
                                   NBPeriodsNUD.Value * 12)


    End Sub

    Friend Delegate Sub CreationBackgroundWorker_AfterWork_ThreadSafe()
    Friend Sub CreationBackgroundWorker_AfterWork()

        If Me.InvokeRequired Then
            Dim MyDelegate As New CreationBackgroundWorker_AfterWork_ThreadSafe(AddressOf CreationBackgroundWorker_AfterWork)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            m_circularProgress.Stop()
            m_circularProgress.Visible = False
            m_circularProgress.Enabled = False
            Me.Visible = False
        End If

    End Sub


#End Region



End Class