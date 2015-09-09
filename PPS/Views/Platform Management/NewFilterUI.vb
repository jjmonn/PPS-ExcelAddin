Imports System.Windows.Forms

' NewFilter.vb
'
'
'
'
'
' Author: Julien Monnereau
' Created: 07/09/2015
' Last modified: 07/09/2015


Public Class NewFilterUI


#Region "Instance Variables"

    Private filtersNode As TreeNode
    Private Controller As AxisFiltersController


#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As AxisFiltersController, _
                   ByRef p_filtersNode As TreeNode)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        filtersNode = p_filtersNode
        Controller = p_controller

    End Sub

    Private Sub NewFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        VTreeViewUtil.LoadParentEntitiesTreeviewBox(ParentFilterTreeBox, filtersNode)

    End Sub

#End Region


#Region "Call Backs"

    Private Sub CreateButton_Click(sender As Object, e As EventArgs) Handles CreateButton.Click

        If NameTextBox.Text <> "" Then
            If ParentFilterTreeBox.TreeView.SelectedNode Is Nothing Then
                Controller.CreateFilter(NameTextBox.Text, _
                                        0)
            Else
                Controller.CreateFilter(NameTextBox.Text, _
                                        ParentFilterTreeBox.TreeView.SelectedNode.Value)
            End If
            Me.Hide()
        Else
            MsgBox("Please enter a name for the new filter.")
        End If

    End Sub

    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click

        Me.Hide()

    End Sub

    Private Sub NewFilter_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()

    End Sub

#End Region




End Class