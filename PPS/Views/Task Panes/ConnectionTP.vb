Imports System.Runtime.InteropServices
Imports AddinExpress.XL



Public Class ConnectionTP

#Region "Instance Variables"

    ' Objects
    Private Addin As AddinModule
    Private CP As ProgressControls.ProgressIndicator


#End Region


#Region "Initialization"

    Public Sub New()
        MyBase.New()

        InitializeComponent()
        
    End Sub

    Public Sub Init(ByRef addin As AddinModule)

        Me.Addin = addin

    End Sub

#End Region


#Region "Call Backs"

    Private Sub ConnectionBT_Click(sender As Object, e As EventArgs) Handles ConnectionBT.Click

        CP = New ProgressControls.ProgressIndicator
        CP.CircleColor = Drawing.Color.Purple
        CP.NumberOfCircles = 12
        CP.NumberOfVisibleCircles = 8
        CP.AnimationSpeed = 75
        CP.CircleSize = 0.7
        CPPanel.Controls.Add(CP)
        CP.Width = 79
        CP.Height = 79
        '  CP.Dock = Windows.Forms.DockStyle.Fill
        CP.Start()
        CP.Show()

        BackgroundWorker1.RunWorkerAsync()

    End Sub

#End Region


#Region "Background Worker 1"

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        'ConnectionsFunctions.Connection(Addin, _
        '                                IDTB.Text, _
        '                                PWDTB.Text)
        ConnectionsFunctions.NetworkConnection(My.Settings.serverIp, _
                                               My.Settings.port_number)



    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        AfterConnectionAttemp_ThreadSafe()

    End Sub

    Delegate Sub AfterConnectionAttemp_Delegate()

    Private Sub AfterConnectionAttemp_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New AfterConnectionAttemp_Delegate(AddressOf AfterConnectionAttemp_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            CP.Dispose()
            PWDTB.Clear()
            Me.Hide()
        End If

    End Sub

#End Region


#Region "Form show and close events"

    Private Sub ConnectionTP_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

        IDTB.Text = My.Settings.user

    End Sub

    Private Sub ConnectionPane_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow

        Me.Visible = GlobalVariables.ConnectionPaneVisible

    End Sub

    Private Sub ConnectionPane_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If Not CP Is Nothing Then CP.Dispose()
        PWDTB.Clear()
        CPPanel.Controls.Clear()
        e.Cancel = True

    End Sub

#End Region

   
  
End Class
