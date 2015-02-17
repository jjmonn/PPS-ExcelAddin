' NewConstraintUI.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 16/02/2015

Imports System.Collections


Friend Class NewConstraintUI


#Region "Instance Variables"

    Private FModelingUI As FModelingUI
    Private scenario_id As String

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_UI As FModelingUI, _
                             ByRef Outputs_name_id_dic As Hashtable, _
                             ByRef input_scenario_id As String)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        FModelingUI = input_UI
        scenario_id = input_scenario_id
        For Each constraint As String In Outputs_name_id_dic.Keys
            ConstraintsComboBox.Items.Add(constraint)
        Next
        Me.TopMost = True
        Me.Top = (FModelingUI.Height + Me.Height) / 2
        Me.Left = (FModelingUI.Width + Me.Width) / 2


    End Sub

#End Region


#Region "Call Back"

    Private Sub ValidateButton_Click(sender As Object, e As EventArgs) Handles ValidateButton.Click

        Dim default_value As Double = 0
        If IsNumeric(DefaultValueTB.Text) Then default_value = DefaultValueTB.Text
        FModelingUI.AddConstraint(ConstraintsComboBox.SelectedItem, _
                                  scenario_id, _
                                  default_value)

        Me.Dispose()

    End Sub

#End Region


    
End Class