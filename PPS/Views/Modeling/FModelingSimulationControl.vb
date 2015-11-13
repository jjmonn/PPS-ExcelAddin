' FModelingSimulationControl.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 11/05/2015


Friend Class FModelingSimulationControl


#Region "Instance Variables"

    ' Objects
    Private controller As FModelingSimulationController
    Private constraint_input_UI As NewConstraintUI


#End Region


#Region "Initialize"

    Friend Sub New(ByRef FModelingsimulationController As FModelingSimulationController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        controller = FModelingsimulationController


    End Sub

#End Region


#Region "Interface"

    Friend Sub addConstraint(ByRef f_account_name As String, _
                             ByRef default_value As Double)

        controller.AddConstraint(f_account_name, default_value)

    End Sub


#End Region


#Region "Call Backs"

    Private Sub refreshBT_Click(sender As Object, e As EventArgs) Handles refreshBT.Click

        controller.ComputeScenario()

    End Sub

    Friend Sub AddConstraintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddConstraintToolStripMenuItem.Click

        constraint_input_UI = New NewConstraintUI(Me, controller.getPossiblesConstraintsNameList())
        constraint_input_UI.Show()

    End Sub

    Private Sub DeleteConstraintToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteConstraintToolStripMenuItem.Click

        controller.DeleteDGVActiveConstraint()

    End Sub

    Private Sub CopyValueRightBT_Click(sender As Object, e As EventArgs) Handles CopyValueRightBT.Click

        controller.CopyValueRight()

    End Sub

    '   SimulationsController.ShowFAccountsConfig() -> legacy: look at what should be displayed


#End Region


End Class
