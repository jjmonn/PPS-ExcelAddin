' ConnectionUI.vb
'
' Connection interface: allows to input id and password and try to connect 
'
'
' To do: 
'       - Ideally resume action after closing connectUI
'       - Reimplement properly
'
'
' known bugs:
'
' 
' Author: Julien Monnereau
' Last modified: 18/08/2015


Imports ProgressControls


Friend Class ConnectionUI


#Region "Instance Variables"

    ' Objects
    Private ADDIN As AddinModule
    Private CP As CircularProgressUI
    Private isConnecting As Boolean
   
#End Region


#Region "Initialize"

    Public Sub New(ByRef inputAddIn As AddinModule)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ADDIN = inputAddIn
        BackgroundWorker1.WorkerReportsProgress = True
        BackgroundWorker1.WorkerSupportsCancellation = True

        userNameTextBox.Text = My.Settings.user
        passwordTextBox.Select()

    End Sub


#End Region


#Region "Call Backs"

    'Private Sub ConnectionBT_Click(sender As Object, e As EventArgs)

    '    If isConnecting = False Then
    '        CP = New CircularProgressUI(Drawing.Color.Purple, "Connecting and Initializing")
    '        Me.Hide()
    '        CP.Show()
    '        BackgroundWorker1.RunWorkerAsync()
    '    End If

    'End Sub

    Private Sub CloseBT_Click(sender As Object, e As EventArgs) Handles CloseBT.Click

        Me.Dispose()
        Me.Close()

    End Sub


#End Region


#Region "Background Worker 1"

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        ' to be manage priority normal (copy code from Task pane)

        'Dim start_time As Date
        'Dim secs As Single

        'AddHandler connectionFunction.ConnectionFailedEvent, AddressOf ConnectionFailedMethod

        'isConnecting = True
        'If connectionFunction.NetworkConnection(My.Settings.serverIp, _
        '                                        My.Settings.port_number, _
        '                                        id, _
        '                                        pwd) = True Then

        '    start_time = Now
        '    Do While connectionFunction.globalInitFlag = False
        '        secs = DateDiff("s", start_time, Now)
        '        If secs > 6 Then Exit Do
        '    Loop
        '    MsgBox("Initialization did not success. Please try again.")
        'Else
        '    MsgBox("The server did not respond")
        'End If


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
            isConnecting = False
            CP.Dispose()
            Me.Dispose()
            Me.Close()
        End If

    End Sub

#End Region



End Class