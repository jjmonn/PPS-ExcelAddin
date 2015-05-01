' PlatformMGTGeneralUI.vb 
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 23/04/2015


Imports System.ComponentModel
Imports System.Threading.Tasks


Friend Class PlatformMGTGeneralUI


#Region "Instance Variables"

    ' Objects
    Private current_controller As Object
 
    ' Variables
    Private controller_index As Int32

#End Region


#Region "Main Menu Call Backs"

    Private Sub FinancialsAndOperationalItemsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FinancialsAndOperationalItemsToolStripMenuItem.Click

        closeCurrentControl(0)

    End Sub

    Private Sub AdjustmentsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdjustmentsToolStripMenuItem.Click

        closeCurrentControl(1)

    End Sub

    Private Sub OrganizationToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles OrganizationToolStripMenuItem1.Click

        closeCurrentControl(2)

    End Sub

    Private Sub OrganizationCategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OrganizationCategoriesToolStripMenuItem.Click

        closeCurrentControl(3)

    End Sub

    Private Sub ClientsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ClientsToolStripMenuItem1.Click

        closeCurrentControl(4)

    End Sub

    Private Sub ClientsCategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClientsCategoriesToolStripMenuItem.Click

        closeCurrentControl(5)

    End Sub

    Private Sub ProductsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ProductsToolStripMenuItem1.Click

        closeCurrentControl(6)

    End Sub

    Private Sub ProductsCategoriesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProductsCategoriesToolStripMenuItem.Click

        closeCurrentControl(7)

    End Sub

    Private Sub VersionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionsToolStripMenuItem.Click

        closeCurrentControl(8)

    End Sub

    Private Sub CurrenciesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CurrenciesToolStripMenuItem.Click

        closeCurrentControl(9)

    End Sub

    Private Sub UsersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UsersToolStripMenuItem.Click

        closeCurrentControl(10)

    End Sub

#End Region


#Region "Controls Display"

    Private Sub closeCurrentControl(ByVal index As Int32)

        controller_index = index
        If Not current_controller Is Nothing Then
            current_controller.close()
        Else
            displayControl()
        End If

    End Sub

    Protected Friend Sub displayControl()

        If Not current_controller Is Nothing Then current_controller = Nothing

        Select Case controller_index
            Case 0 : current_controller = New AccountsController()
            Case 1 ' current_controller = New adjustments(Panel1)
            Case 2 : current_controller = New EntitiesController()
            Case 3 : current_controller = New EntitiesCategoriesController()
            Case 4 : current_controller = New ClientsController()
            Case 5 : current_controller = New ClientsCategoriesController()
            Case 6 : current_controller = New ProductsController()
            Case 7 : current_controller = New productsCategoriesController()
            Case 8 : current_controller = New DataVersionsController()
            Case 9 : current_controller = New ExchangeRatesController()
            Case 10 'current_controller = New UsersController(Panel1)

                ' controls and Charts to be added !!!
        End Select
        If Panel1.Controls.Count > 0 Then Panel1.Controls(0).Dispose()
        current_controller.addControlToPanel(Panel1, Me)

    End Sub


#End Region


    Private Sub PlatformMGTGeneralUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        If Not current_controller Is Nothing Then
            '  e.Cancel = True
            current_controller.close()
        End If

    End Sub

End Class