' AnalalysisCategoriesControl.vb
'
'
'
' Author: Julien Monnereau
' Last modified: 05/09/2015


Imports System.Windows.Forms
Imports System.Collections.Generic


Friend Class AxisFiltersControl


#Region "Instance Variables"

    ' Objects
    Private Controller As AxisFiltersController
    Private FiltersFiltersValuesTV As New TreeView

    ' Variables
    Private CP As CircularProgressUI

#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_controller As AxisFiltersController, _
                   ByRef p_filtersFiltersValuesTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = p_controller
        FiltersFiltersValuesTV = p_filtersFiltersValuesTV
        TableLayoutPanel1.Controls.Add(FiltersFiltersValuesTV, 0, 1)
        FiltersFiltersValuesTV.Dock = DockStyle.Fill
        FiltersFiltersValuesTV.ImageList = ImageList1
        FiltersFiltersValuesTV.ContextMenuStrip = RCM_TV

        AddHandler FiltersFiltersValuesTV.KeyDown, AddressOf FiltersFiltersValuesTV_KeyDown
        AddHandler FiltersFiltersValuesTV.AfterSelect, AddressOf TV_AfterSelect

    End Sub

    Friend Sub closeControl()

        Controller.SendNewPositionsToModel()

    End Sub

#End Region


#Region "Call backs"

    Private Sub CreateCategoryBT_Click(sender As Object, e As EventArgs) Handles NewCategoryMenuBT.Click, CreateCategoryRCM.Click

        Dim name = InputBox("Please enter the name of the New Category :")
        If Controller.CreateFilter(name) = False Then
            MsgBox("This name is already used or contains forbiden characters.")
        End If

    End Sub

    Private Sub AddCategoryValueBT_Click_1(sender As Object, e As EventArgs) Handles AddValueRCM.Click, NewCategoryMenuBT.Click

        ' reimplement: priority normal:
        '
        ' if node has already children 
        '   -> add a child
        ' else
        '   -> add filter (ask for filter name) -> does not produce node addition
        '   -> enchainer sur demande première valeur ?


        'If Not FiltersFiltersValuesTV.SelectedNode Is Nothing Then
        '    Dim current_node As TreeNode = FiltersFiltersValuesTV.SelectedNode
        '    If Controller.IsCategory(FiltersFiltersValuesTV.SelectedNode.Name) = False Then current_node = current_node.Parent

        '    Dim name = InputBox("Please enter the new Category Value Name:")
        '    If Controller.CreateCategoryValue(name, current_node) = False Then
        '        MsgBox("This name is already used or contains forbiden characters. The length must not exceed 48 characters.")
        '    End If
        'Else
        '    MsgBox("A Category must be selected in order ot Add a Value.")
        'End If

    End Sub

    Private Sub RenameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameMenuBT.Click, RenameRCM.Click

        If Not FiltersFiltersValuesTV.SelectedNode Is Nothing Then

            Dim current_node As TreeNode = FiltersFiltersValuesTV.SelectedNode
            If IsNonAttributedValue(current_node) = False Then
                Dim name = InputBox("Please enter the new Category Value Name:")
                If name <> "" Then

                    ' reimplement => update simply
                    ' the server manage validity
                    ' priority normal
                    'If Controller.RenameFilterValue(current_node.Name, name) Then
                    '    current_node.Text = name
                    'Else
                    '    MsgBox("This name is already used or contains forbiden characters.")
                    'End If
                End If
            Else
                MsgBox("Cannot rename a NA value.")
            End If
        Else
            MsgBox("A Category must be selected in order ot Add a Value.")
        End If

    End Sub

    Private Sub DeleteBT_Click(sender As Object, e As EventArgs) Handles DeleteMenuBT.Click, DeleteRCM.Click

        ' to be validated 
        ' priority normal 
        ' !!!
        If Not FiltersFiltersValuesTV.SelectedNode Is Nothing Then
            Dim current_node As TreeNode = FiltersFiltersValuesTV.SelectedNode
            If Controller.Isfilter(FiltersFiltersValuesTV.SelectedNode.Name) Then
                ' Delete Category
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category: " + Chr(13) + current_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Category deletion confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    Controller.DeleteFilter(current_node)
                End If
            Else
                ' Delete Category Value
                If IsNonAttributedValue(current_node) = False Then
                    Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category value: " + Chr(13) + current_node.Text + Chr(13) + "Do you confirm?", _
                                                             "Category value deletion confirmation", _
                                                             MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                    If confirm = DialogResult.Yes Then
                        Controller.DeleteFilterValue(current_node)
                    End If
                Else
                    MsgBox("Cannot delete a NA Value.")
                End If
            End If
        End If

    End Sub

#End Region


#Region "Events"

    Private Sub TV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        FiltersFiltersValuesTV.SelectedNode = e.Node

    End Sub

    Private Sub FiltersFiltersValuesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteBT_Click(sender, e)
            Case Keys.Up
                If e.Control Then
                    If Not FiltersFiltersValuesTV.SelectedNode Is Nothing Then
                        TreeViewsUtilities.MoveNodeUp(FiltersFiltersValuesTV.SelectedNode)
                    End If
                End If
            Case Keys.Down
                If e.Control Then
                    If Not FiltersFiltersValuesTV.SelectedNode Is Nothing Then
                        TreeViewsUtilities.MoveNodeDown(FiltersFiltersValuesTV.SelectedNode)
                    End If
                End If
        End Select

    End Sub

#End Region


#Region "Utilities"

    Private Function IsNonAttributedValue(ByRef node As TreeNode) As Boolean

        If node.Name = node.Parent.Name & NON_ATTRIBUTED_SUFIX Then Return True Else Return False

    End Function


#End Region




End Class
