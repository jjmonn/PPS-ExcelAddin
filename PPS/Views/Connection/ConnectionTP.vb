' ConnectionTP.vb
'
' Connection/ Authentication Task Pane
'
'
'
' Author: Julien Monnereau
' Last modified: 19/08/2015

Imports System.Runtime.InteropServices
Imports AddinExpress.XL


Public Class ConnectionTP


#Region "Instance Variables"

    ' Objects
    Private Addin As AddinModule
    Private CP As ProgressControls.ProgressIndicator
    Private isConnecting As Boolean
    Private id As String
    Private pwd As String
    Private connectionFunction As New ConnectionsFunctions
    Private connectionFailed As Boolean

#End Region


#Region "Initialization"

    Public Sub New()

        MyBase.New()
        InitializeComponent()
        CancelBT.Visible = False
        CP = New ProgressControls.ProgressIndicator
        CP.CircleColor = Drawing.Color.Blue
        CP.NumberOfCircles = 12
        CP.NumberOfVisibleCircles = 8
        CP.AnimationSpeed = 75
        CP.CircleSize = 0.7
        CPPanel.Controls.Add(CP)
        CP.Dock = Windows.Forms.DockStyle.Fill
        CP.Width = 79
        CP.Height = 79
        CP.Visible = False

        SetupMultilangue()

    End Sub

  Public Sub Init(ByRef addin As AddinModule)

    Me.Addin = addin

  End Sub

    Private Sub SetupMultilangue()

        Me.m_userLabel.Text = Local.GetValue("connection.user_id")
        Me.m_passwordLabel.Text = Local.GetValue("connection.password")
        Me.ConnectionBT.Text = Local.GetValue("connection.connection")
        Me.CancelBT.Text = Local.GetValue("general.cancel")
        Me.Text = Local.GetValue("connection.connection")


    End Sub

#End Region


#Region "Call Backs"

    Private Sub ConnectionBT_Click(sender As Object, e As EventArgs) Handles ConnectionBT.Click, ConnectionBT.KeyDown

        BackgroundWorker1 = New ComponentModel.BackgroundWorker
        AddHandler BackgroundWorker1.DoWork, AddressOf BackgroundWorker1_DoWork
        AddHandler BackgroundWorker1.RunWorkerCompleted, AddressOf AfterConnectionAttemp_ThreadSafe
        BackgroundWorker1.WorkerSupportsCancellation = True

        If isConnecting = False Then
            ConnectionBT.Visible = False
            id = Me.userNameTextBox.Text
            pwd = Me.passwordTextBox.Text
            CancelBT.Visible = True
            CP.Visible = True
            CP.Enabled = True
            CP.Start()
            CP.Show()
            BackgroundWorker1.RunWorkerAsync()
        End If

    End Sub

  Private Sub IDTB_TextChanged(sender As Object, e As EventArgs) Handles userNameTextBox.TextChanged

    My.Settings.user = userNameTextBox.Text
    My.Settings.Save()

  End Sub


#End Region


#Region "Background Worker 1"

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs)
        isConnecting = True
        AddHandler connectionFunction.ConnectionFailedEvent, AddressOf ConnectionFailedMethod
        ConnectionsFunctions.Connect(connectionFunction, connectionFailed, id, pwd)
    End Sub

    Private Sub ConnectionFailedMethod() Handles CancelBT.Click
        connectionFailed = True
        BackgroundWorker1.CancelAsync()
        AfterConnectionAttemp_ThreadSafe()
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs)
        AfterConnectionAttemp_ThreadSafe()
    End Sub

    Delegate Sub AfterConnectionAttemp_Delegate()
    Private Sub AfterConnectionAttemp_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New AfterConnectionAttemp_Delegate(AddressOf AfterConnectionAttemp_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            isConnecting = False
            CP.Stop()
            CancelBT.Visible = False
            CP.Visible = CP.Enabled = False
            ConnectionBT.Visible = True
            id = ""
            pwd = ""
            AddinModule.DisplayConnectionStatus(connectionFailed = False)
            BackgroundWorker1 = Nothing
            passwordTextBox.Text = ""
            Me.Hide()
        End If

    End Sub

#End Region


#Region "Form show and close events"

    Private Sub ConnectionTP_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        userNameTextBox.Text = My.Settings.user
    End Sub

    Private Sub ConnectionPane_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow
        Me.Visible = GlobalVariables.ConnectionPaneVisible
    End Sub

    Private Sub ConnectionPane_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If Not CP Is Nothing Then CP.Stop()
        passwordTextBox.Text = ""
        CPPanel.Controls.Clear()
        e.Cancel = True

    End Sub

#End Region




End Class
