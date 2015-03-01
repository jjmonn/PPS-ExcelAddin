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
' Last modified: 26/02/2015


Imports ProgressControls


Friend Class ConnectionUI


#Region "Instance Variables"

    ' Objects
    Private ADDIN As AddinModule
    Private CP As CircularProgressUI

    ' Variables
    Private isCredentialValid As Boolean

#End Region


#Region "Initialize"

    Public Sub New(ByRef inputAddIn As AddinModule)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ADDIN = inputAddIn
        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        BackgroundWorker1.WorkerReportsProgress = True

        AddHandler mainPanel.Paint, AddressOf Panel1_Paint
        AddHandler mainPanel.MouseMove, AddressOf panel1_MouseMove
        AddHandler mainPanel.MouseDown, AddressOf form_MouseDown
        AddHandler mainPanel.MouseUp, AddressOf form_MouseUp
        IDTB.Text = My.Settings.user
        PWDTB.Select()

    End Sub


#End Region


#Region "Call Backs"

    Private Sub ConnectionBT_Click(sender As Object, e As EventArgs) Handles ConnectionBT.Click

        CP = New CircularProgressUI(Drawing.Color.Purple, "Connecting and Initializing")
        Me.Hide()
        CP.Show()
        BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub CloseBT_Click(sender As Object, e As EventArgs) Handles CloseBT.Click
        Me.Dispose()
        Me.Close()
    End Sub


#End Region


#Region "Background Worker 1"

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        ConnectioN = OpenConnection(IDTB.Text, PWDTB.Text)
        If Not ConnectioN Is Nothing Then
            If SQLCredentials.SetGLOBALUserCredential() Then
                GlobalVariables.GlobalDBDownloader = New DataBaseDataDownloader
                GlobalVariables.GlobalDll3Interface = New DLL3_Interface
                GlobalVariables.GenericGlobalSingleEntityComputer = New GenericSingleEntityDLL3Computer(GlobalVariables.GlobalDBDownloader, _
                                                                                                 GlobalVariables.GlobalDll3Interface)
                GlobalVariables.GenericGlobalAggregationComputer = New GenericAggregationDLL3Computing(GlobalVariables.GlobalDBDownloader, _
                                                                                                       GlobalVariables.GlobalDll3Interface)
                isCredentialValid = True
            Else
                isCredentialValid = False
                Exit Sub
            End If
        End If

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
            If Not ConnectioN Is Nothing Then
                If isCredentialValid = True Then
                    If My.Settings.user <> IDTB.Text Then My.Settings.user = IDTB.Text
                    GlobalVariables.Connection_Toggle_Button.Image = 1
                    GlobalVariables.Connection_Toggle_Button.Caption = "Connected"

                    If VersionsMapping.IsVersionValid(My.Settings.version_id) Then
                        ADDIN.SetVersion(My.Settings.version_id)
                    Else
                        ADDIN.LaunchVersionSelection()
                    End If
                  
                    AdjustmentsMapping.LoadAdjustmentsIDDD()
                    CP.Dispose()
                    Me.Dispose()
                    Me.Close()
                Else
                    CP.Close()
                    MsgBox("Your credentials have been revoked. Please contact your Administrator.")
                    ConnectioN.Close()
                End If
            Else
                CP.Close()
                MsgBox("Connection failed")
            End If

        End If

    End Sub

#End Region



End Class