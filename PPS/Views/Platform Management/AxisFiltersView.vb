' AnalalysisCategoriesControl.vb
'
'
'
' Author: Julien Monnereau
' Last modified: 05/09/2015


Imports System.Windows.Forms
Imports System.Collections.Generic


Friend Class AxisFiltersView


#Region "Instance Variables"

    ' Objects
    Private Controller As AxisFiltersController
    Private NewFilterUI As NewFilterUI
    Private FiltersFiltersValuesTV As New TreeView
    Private filtersNode As TreeNode
    Private axisId As Int32

    ' Variables
    Private CP As CircularProgressUI

#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_controller As AxisFiltersController, _
                   ByRef p_filtersNode As TreeNode, _
                   ByRef p_axidId As Int32, _
                   ByRef p_filtersFiltersValuesTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = p_controller
        Me.NewFilterUI = New NewFilterUI(p_controller, p_filtersNode)
        FiltersFiltersValuesTV = p_filtersFiltersValuesTV
        axisId = p_axidId
        filtersNode = p_filtersNode
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


#Region "Interface"

    Delegate Sub UpdateFiltersValuesTV_Delegate()
    Friend Sub UpdateFiltersValuesTV()

        If InvokeRequired Then
            Dim MyDelegate As New UpdateFiltersValuesTV_Delegate(AddressOf UpdateFiltersValuesTV)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            Dim TVExpansionTemp As Dictionary(Of String, Boolean) = TreeViewsUtilities.SaveNodesExpansionsLevel(FiltersFiltersValuesTV)
            AxisFilter.LoadFvTv(FiltersFiltersValuesTV, filtersNode, axisId)
            If FiltersFiltersValuesTV.Nodes.Count > 0 Then
                FiltersFiltersValuesTV.Nodes(0).Name = "filterId" & FiltersFiltersValuesTV.Nodes(0).Name
            End If
            TreeViewsUtilities.ResumeExpansionsLevel(FiltersFiltersValuesTV, TVExpansionTemp)
            FiltersFiltersValuesTV.Refresh()
        End If

    End Sub

#End Region


#Region "Call backs"

    Private Sub CreateFilterBT_Click(sender As Object, e As EventArgs) Handles NewCategoryMenuBT.Click, CreateCategoryRCM.Click

        Me.NewFilterUI.Show()

    End Sub

    Private Sub CreateFilterValueBT_Click_1(sender As Object, e As EventArgs) Handles AddValueRCM.Click

        Dim filterId, parentFilterValueId As Int32
        Dim currentNode As TreeNode = FiltersFiltersValuesTV.SelectedNode
        If FiltersFiltersValuesTV.SelectedNode Is Nothing Then
            MsgBox("Please select a Category or a Value.")
        Else
            If Controller.IsFilter(currentNode.Name) Then
                filterId = CInt(currentNode.Name)
                parentFilterValueId = 0
                GoTo NewFilterValue
            End If

            If currentNode.Nodes.Count > 0 Then
                filterId = GlobalVariables.FiltersValues.filtervalues_hash(CInt(currentNode.Nodes(0).Name))(FILTER_ID_VARIABLE)
                parentFilterValueId = CInt(currentNode.Name)
            Else
                filterId = GlobalVariables.FiltersValues.filtervalues_hash(CInt(currentNode.Name))(FILTER_ID_VARIABLE)
                If Controller.IsFilter(currentNode.Parent.Name) = True Then
                    parentFilterValueId = 0
                Else
                    parentFilterValueId = CInt(currentNode.Parent.Name)
                End If
            End If
        End If

NewFilterValue:
        Dim newFilterValueName As String = InputBox("Enter the name of the New Category Value.", "New Category Value")
        If newFilterValueName <> "" Then
            If GlobalVariables.Filters.IsNameValid(newFilterValueName) = True Then
                Controller.CreateFilterValue(newFilterValueName, _
                                             filterId, _
                                             parentFilterValueId)
            Else
                MsgBox("This name is already in use.")
            End If
        End If

    End Sub

    Private Sub RenameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameMenuBT.Click, RenameRCM.Click

        If Not FiltersFiltersValuesTV.SelectedNode Is Nothing Then
            Dim current_node As TreeNode = FiltersFiltersValuesTV.SelectedNode
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
            MsgBox("A Category must be selected in order ot Add a Value.")
        End If

    End Sub

    Private Sub DeleteBT_Click(sender As Object, e As EventArgs) Handles DeleteMenuBT.Click, DeleteRCM.Click

        ' to be validated 
        ' priority normal 
        ' !!!
        If Not FiltersFiltersValuesTV.SelectedNode Is Nothing Then
            Dim current_node As TreeNode = FiltersFiltersValuesTV.SelectedNode
            If Controller.IsFilter(FiltersFiltersValuesTV.SelectedNode.Name) Then
                ' Delete Category
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category: " + Chr(13) + current_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Category deletion confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    Controller.DeleteFilter(current_node.Name)
                End If
            Else
                ' Delete Category Value
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category value: " + Chr(13) + current_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Category value deletion confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    Controller.DeleteFilterValue(current_node.Name)
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




End Class
