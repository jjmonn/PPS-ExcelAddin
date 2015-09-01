' NewConstraintUI.vb
'
' UI allowing to add a new constraint
'
'
'
' Author: Julien Monnereau
' Last modified: 11/05/2015


Imports System.Collections


Friend Class NewConstraintUI


#Region "Instance Variables"

    Private ParentView As FModelingSimulationControl
    Private scenario_id As String

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_UI As FModelingSimulationControl, _
                             ByRef possible_constraints_name_list As Generic.List(Of String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ParentView = input_UI
        For Each constraint As String In possible_constraints_name_list
            ConstraintsComboBox.Items.Add(constraint)
        Next
        Me.TopMost = True
        Me.Top = (ParentView.Height + Me.Height) / 2
        Me.Left = (ParentView.Width + Me.Width) / 2


    End Sub

#End Region


#Region "Call Back"

    Private Sub ValidateButton_Click(sender As Object, e As EventArgs) Handles ValidateButton.Click

        Dim default_value As Double = 0
        If IsNumeric(DefaultValueTB.Text) Then default_value = DefaultValueTB.Text
        ParentView.addConstraint(ConstraintsComboBox.SelectedItem, _
                                 default_value)
        Me.Dispose()

    End Sub

#End Region


    
End Class