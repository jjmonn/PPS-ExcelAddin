' CategoriesManagementUI.vb
'
' 
' To do:
'       - 
'
'
' Known bugs:
'       -
'
'
' Last modified: 10/12/2014
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms


Friend Class CategoriesManagementUI


#Region "Instance Variables"

    ' Objects
    Private Controller As CategoriesController
    Private CategoriesTV As New TreeView
    Private current_node As TreeNode

    ' Variables


    ' Constants
    Private Const POSITION_STEP As Double = 0.0000001

#End Region


#Region "Initialization"

    Public Sub New(ByRef input_controller As CategoriesController, _
                   ByRef input_categories_tv As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        CategoriesTV = input_categories_tv
        Panel1.Controls.Add(CategoriesTV)
        CategoriesTV.Dock = DockStyle.Fill
        CategoriesTV.ImageList = ImageList1
        CategoriesTV.ContextMenuStrip = RCM_TV
        AddHandler CategoriesTV.NodeMouseClick, AddressOf tv_node_mouse_click
        AddHandler CategoriesTV.KeyDown, AddressOf CategoriesTV_KeyDown
        AddHandler CategoriesTV.AfterSelect, AddressOf TV_AfterSelect

    End Sub

#End Region


#Region "Call backs"

    Private Sub CreateCategory()

        Dim name = InputBox("Please enter the name of the New Category :")
        If Controller.CreateCategory(name) = False Then
            MsgBox("This name is already used or contains forbiden characters.")
        End If

    End Sub

    Private Sub CreateCategoryValue(ByRef parent_node As TreeNode)

        If Controller.IsCategory(parent_node.Name) = False Then parent_node = parent_node.Parent
        Dim name = InputBox("Please enter the new Category Value Name:")
        If Controller.CreateCategoryValue(name, parent_node) = False Then
            MsgBox("This name is already used or contains forbiden characters. The length must not exceed 48 characters.")
        End If

    End Sub

    Private Sub DeleteCategory(ByRef node As TreeNode)

        Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category: " + Chr(13) + node.Text + Chr(13) + "Do you confirm?", _
                                                 "Category deletion confirmation", _
                                                 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

        If confirm = DialogResult.Yes Then
            Controller.DeleteCategory(node)
        End If

    End Sub

    Private Sub DeleteCategoryValue(ByRef node As TreeNode)

        If IsNonAttributedValue(node) = False Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category value: " + Chr(13) + node.Text + Chr(13) + "Do you confirm?", _
                                                     "Category value deletion confirmation", _
                                                     MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)

            If confirm = DialogResult.Yes Then
                Controller.DeleteCategoryValue(node)
            End If
        Else
            MsgBox("Cannot delete a NA Value.")
        End If

    End Sub

    Private Sub RenameCategoryValue(ByRef node As TreeNode)

        If IsNonAttributedValue(node) = False Then
            Dim name = InputBox("Please enter the new Category Value Name:")
            If name <> "" Then
                If Controller.RenameCategoryValue(node.Name, name) Then
                    node.Text = name
                Else
                    MsgBox("This name is already used or contains forbiden characters.")
                End If
            End If
        Else
            MsgBox("Cannot rename a NA value.")
        End If

    End Sub

#End Region


#Region "Right Click Menu"

    Private Sub AddCategoryValueBT_Click_1(sender As Object, e As EventArgs) Handles AddCategoryValueBT.Click

        If Not current_node Is Nothing Then
            CreateCategoryValue(current_node)
        Else
            MsgBox("A Category must be selected in order ot Add a Value.")
        End If

    End Sub

    Private Sub CreateCategoryBT_Click(sender As Object, e As EventArgs) Handles CreateCategoryBT.Click

        CreateCategory()

    End Sub

    Private Sub RenameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameToolStripMenuItem.Click

        If Not current_node Is Nothing Then
            RenameCategoryValue(current_node)
        Else
            MsgBox("A Category must be selected in order ot Add a Value.")
        End If

    End Sub

    Private Sub DeleteBT_Click(sender As Object, e As EventArgs) Handles DeleteBT.Click

        If Not current_node Is Nothing Then
            If Controller.IsCategory(current_node.Name) Then
                DeleteCategory(current_node)
            Else
                DeleteCategoryValue(current_node)
            End If
        End If

    End Sub

#End Region


#Region "Events"

    Private Sub tv_node_mouse_click(sender As Object, e As TreeNodeMouseClickEventArgs)

        current_node = e.Node

    End Sub

    Private Sub TV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        current_node = e.Node

    End Sub

    Private Sub CategoriesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteBT_Click(sender, e)
            Case Keys.Up
                If e.Control Then
                    If Not current_node Is Nothing Then
                        TreeViewsUtilities.MoveNodeUp(current_node)
                        Controller.positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(CategoriesTV)
                    End If
                End If
            Case Keys.Down
                If e.Control Then
                    If Not current_node Is Nothing Then
                        TreeViewsUtilities.MoveNodeDown(current_node)
                        Controller.positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(CategoriesTV)
                    End If
                End If
        End Select

    End Sub

#Region "Move nodes up and down into hierarchy Procedure"


    Private Sub ResumeAccountTree()

        Dim expansionDic As Dictionary(Of String, Boolean) = TreeViewsUtilities.SaveNodesExpansionsLevel(CategoriesTV)
        Controller.SendNewPositionsToModel()
        Category.LoadCategoriesTree(CategoriesTV)
        TreeViewsUtilities.ResumeExpansionsLevel(CategoriesTV, expansionDic)

    End Sub

#End Region

    Private Sub Categories_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Controller.positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(CategoriesTV)
        Controller.SendNewPositionsToModel()

    End Sub

#End Region


#Region "Utilities"

    Private Function IsNonAttributedValue(ByRef node As TreeNode) As Boolean

        If node.Name = node.Parent.Name & NON_ATTRIBUTED_SUFIX Then Return True Else Return False

    End Function


#End Region


End Class