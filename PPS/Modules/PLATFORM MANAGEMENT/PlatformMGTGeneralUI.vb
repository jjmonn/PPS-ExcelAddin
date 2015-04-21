' PlatformMGTGeneralUI.vb 
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 21/04/2015


Imports System.ComponentModel


Friend Class PlatformMGTGeneralUI


#Region "Instance Variables"

    ' Objects
    Private current_controller As Object
    Private CP As CircularProgressUI

    ' Variables
    Private controller_index As Int32

#End Region


#Region "Main Menu Call Backs"

    Private Sub FinancialsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinancialsToolStripMenuItem.Click

        displayControl(0)

    End Sub

    Private Sub AdjustmentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustmentsToolStripMenuItem.Click

        displayControl(1)

    End Sub

    Private Sub OrganizationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrganizationToolStripMenuItem.Click

         displayControl(2)

    End Sub

    Private Sub OrganizationCategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrganizationCategoriesToolStripMenuItem.Click

        displayControl(3)

    End Sub

    Private Sub ClientsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClientsToolStripMenuItem.Click

        displayControl(4)

    End Sub

    Private Sub ClientsCategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClientsCategoriesToolStripMenuItem.Click

        displayControl(5)

    End Sub

    Private Sub ProductsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsToolStripMenuItem.Click

        displayControl(6)

    End Sub

    Private Sub ProductsCategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsCategoriesToolStripMenuItem.Click

        displayControl(7)

    End Sub

    Private Sub VersionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionsToolStripMenuItem.Click

        displayControl(8)

    End Sub

    Private Sub CurrenciesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CurrenciesToolStripMenuItem.Click

        displayControl(9)

    End Sub

    Private Sub UsersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsersToolStripMenuItem.Click

        displayControl(10)

    End Sub

#End Region


#Region "Background Worker Methods"

    Delegate Sub EndSubmission_Delegate()

    Private Sub BackgroundWork(sender As Object, e As DoWorkEventArgs) Handles BCDWorker.DoWork

        If Not current_controller Is Nothing Then
            current_controller.close()
            current_controller = Nothing
        End If

        Select Case controller_index
            Case 0 : current_controller = New AccountsController()
            Case 1 ' current_controller = New adjustments(Panel1)
            Case 2 : current_controller = New EntitiesController()
            Case 3 'current_controller = New entitiescategoriesController(Panel1)
            Case 4 'current_controller = New clientsController(Panel1)
            Case 5 'current_controller = New clientscategoriesController(Panel1)
            Case 6 : current_controller = New ProductsController()
            Case 7 'current_controller = New productscategoriesController(Panel1)
            Case 8 : current_controller = New DataVersionsController()
            Case 9 : current_controller = New ExchangeRatesController()
            Case 10 'current_controller = New UsersController(Panel1)
        End Select

    End Sub

    Private Sub BGW_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles BCDWorker.RunWorkerCompleted

        EndSubmission_ThreadSafe()

    End Sub

    Private Sub EndSubmission_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New EndSubmission_Delegate(AddressOf EndSubmission_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})

            If Panel1.Controls.Count > 0 Then Panel1.Controls(0).Dispose()
            current_controller.addControlToPanel(Panel1)
            CP.Dispose()
            CP.Close()
        Else

            If Panel1.Controls.Count > 0 Then Panel1.Controls(0).Dispose()
            current_controller.addControlToPanel(Panel1)
            CP.Close()
            CP.Dispose()
        End If

    End Sub

#End Region


#Region "Utilities"

    Private Sub displayControl(ByRef index As Int32)

        controller_index = index
        CP = New CircularProgressUI(Drawing.Color.Purple, "")
        CP.Show()
        BCDWorker.RunWorkerAsync()

    End Sub

#End Region


End Class