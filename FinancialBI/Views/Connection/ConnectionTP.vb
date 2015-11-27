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
    Private m_addin As AddinModule
    ' Private m_circularProgress As New ProgressControls.ProgressIndicator
    Private m_isConnecting As Boolean
    Private m_id As String
    Private m_password As String
    Private m_connectionFunction As New ConnectionsFunctions
    Private m_connectionFailed As Boolean

#End Region


#Region "Initialization"

    Public Sub New()

        MyBase.New()
        InitializeComponent()
        m_cancelButton.Visible = False
        SetupMultilangue()

    End Sub

    Public Sub Init(ByRef addin As AddinModule)

        Me.m_addin = addin

    End Sub

    Private Sub SetupMultilangue()

        Me.m_userLabel.Text = Local.GetValue("connection.user_id")
        Me.m_passwordLabel.Text = Local.GetValue("connection.password")
        Me.ConnectionBT.Text = Local.GetValue("connection.connection")
        Me.m_cancelButton.Text = Local.GetValue("general.cancel")
        Me.Text = Local.GetValue("connection.connection")

    End Sub

#End Region


#Region "Call Backs"

    Private Sub ConnectionBT_Click(sender As Object, e As EventArgs) Handles ConnectionBT.Click, ConnectionBT.KeyDown

        BackgroundWorker1 = New ComponentModel.BackgroundWorker
        AddHandler BackgroundWorker1.DoWork, AddressOf BackgroundWorker1_DoWork
        AddHandler BackgroundWorker1.RunWorkerCompleted, AddressOf AfterConnectionAttemp_ThreadSafe
        BackgroundWorker1.WorkerSupportsCancellation = True

        If m_isConnecting = False Then
            ConnectionBT.Visible = False
            m_id = Me.userNameTextBox.Text
            m_password = Me.passwordTextBox.Text

            m_circularProgress2.Start()
            m_cancelButton.Visible = True
            m_circularProgress2.Visible = True
            m_circularProgress2.Enabled = True
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
        m_isConnecting = True
        AddHandler m_connectionFunction.ConnectionFailedEvent, AddressOf ConnectionFailedMethod
        ConnectionsFunctions.Connect(m_connectionFunction, m_connectionFailed, m_id, m_password)
    End Sub

    Private Sub ConnectionFailedMethod() Handles m_cancelButton.Click
        m_connectionFailed = True
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
            m_isConnecting = False
            m_circularProgress2.Stop()
            m_cancelButton.Visible = False
            m_circularProgress2.Visible = m_circularProgress2.Enabled = False
            ConnectionBT.Visible = True
            m_id = ""
            m_password = ""
            AddinModule.DisplayConnectionStatus(m_connectionFailed = False)
            BackgroundWorker1 = Nothing
            passwordTextBox.Text = ""
            Me.Hide()
        End If

    End Sub

#End Region


#Region "Form show and close events"

    Private Sub ConnectionTP_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        userNameTextBox.Text = My.Settings.user
        m_circularProgress2.Visible = False
        m_circularProgress2.Enabled = False
    End Sub

    Private Sub ConnectionPane_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow
        Me.Visible = GlobalVariables.ConnectionPaneVisible
    End Sub

    Private Sub ConnectionPane_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If Not m_circularProgress2 Is Nothing Then m_circularProgress2.Stop()
        passwordTextBox.Text = ""
        e.Cancel = True

    End Sub

#End Region


End Class
