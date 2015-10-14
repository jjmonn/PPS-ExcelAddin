' AnalalysisCategoriesControl.vb
'
'
'
' Author: Julien Monnereau
' Last modified: 05/09/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.Controls


Friend Class AxisFiltersView


#Region "Instance Variables"

    ' Objects
    Private Controller As AxisFiltersController
    Private FiltersFiltersValuesTV As New vTreeView
    Private filtersNode As vTreeNode
    Private axisId As Int32

    ' Variables
    Private CP As CircularProgressUI

#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_controller As AxisFiltersController, _
                   ByRef p_filtersNode As vTreeNode, _
                   ByRef p_axidId As Int32, _
                   ByRef p_filtersFiltersValuesTV As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = p_controller
        FiltersFiltersValuesTV = p_filtersFiltersValuesTV
        axisId = p_axidId
        filtersNode = p_filtersNode
        TableLayoutPanel1.Controls.Add(FiltersFiltersValuesTV, 0, 1)
        FiltersFiltersValuesTV.Dock = DockStyle.Fill
        FiltersFiltersValuesTV.ImageList = ImageList1
        FiltersFiltersValuesTV.ContextMenuStrip = RCM_TV
        VTreeViewUtil.InitTVFormat(FiltersFiltersValuesTV)
        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then
            CategoriesToolStripMenuItem.Enabled = False
            RenameRCM.Enabled = False
            DeleteRCM.Enabled = False
            AddValueRCM.Enabled = False
            EditStructureToolStripMenuItem.Enabled = False
        End If

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
            For Each node As vTreeNode In FiltersFiltersValuesTV.Nodes
                node.Value = "filterId" & node.Value
            Next
            TreeViewsUtilities.ResumeExpansionsLevel(FiltersFiltersValuesTV, TVExpansionTemp)
            FiltersFiltersValuesTV.Refresh()
        End If

    End Sub

    Delegate Sub SetFilter_Delegate(ByRef p_ht As Hashtable)
    Friend Sub SetFilter(ByRef p_ht As Hashtable)
        If InvokeRequired Then
            Dim MyDelegate As New SetFilter_Delegate(AddressOf SetFilter)
            Me.Invoke(MyDelegate, New Object() {p_ht})
        Else
            Dim newNode As New vTreeNode

            newNode.Text = p_ht(NAME_VARIABLE)
            newNode.Value = p_ht(ID_VARIABLE)
            If p_ht(PARENT_ID_VARIABLE) = 0 Then
                newNode.Value = "filterId" & newNode.Value
                FiltersFiltersValuesTV.Nodes.Add(newNode)
            Else
                Dim parentNode = GetNode(FiltersFiltersValuesTV.Nodes, p_ht(PARENT_ID_VARIABLE))

                If Not parentNode Is Nothing Then parentNode.Nodes.Add(newNode)
            End If
        End If
    End Sub

    Private Function GetNode(ByRef p_parentNode As vTreeNodeCollection, ByRef p_id As Int32) As vTreeNode
        For Each node As vTreeNode In p_parentNode
            If node.Value = p_id Then Return node

            Dim result = GetNode(node.Nodes, p_id)
            If Not result Is Nothing Then Return result
        Next
        Return Nothing
    End Function

#End Region


#Region "Call backs"

    Private Sub CreateFilterValueBT_Click_1(sender As Object, e As EventArgs) Handles AddValueRCM.Click

        Dim filterId, parentFilterValueId As Int32
        Dim currentNode As vTreeNode = FiltersFiltersValuesTV.SelectedNode
        If FiltersFiltersValuesTV.SelectedNode Is Nothing Then
            MsgBox("Please select a Category or a Value.")
        Else

            ' Check if node is filter Node
            If currentNode.Value.IndexOf(AxisFiltersController.m_FilterTag) > -1 Then
                filterId = Strings.Right(currentNode.Value, Len(currentNode.Value) - Len(AxisFiltersController.m_FilterTag))
                parentFilterValueId = 0
                GoTo NewFilterValue
            End If

            If currentNode.Nodes.Count > 0 Then
                filterId = GlobalVariables.FiltersValues.filtervalues_hash(CInt(currentNode.Nodes(0).Value))(FILTER_ID_VARIABLE)
            Else
                Dim parentFilterId As Int32 = GlobalVariables.FiltersValues.filtervalues_hash(CInt(currentNode.Value))(FILTER_ID_VARIABLE)
                filterId = GlobalVariables.Filters.GetFilterChild(parentFilterId)
                If (filterId = -1) Then
                    MsgBox("A value cannot be added under " & currentNode.Text & " because there is no deeper category.")
                    Exit Sub
                End If
            End If
            parentFilterValueId = CInt(currentNode.Value)
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
            Dim current_node As vTreeNode = FiltersFiltersValuesTV.SelectedNode
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
            Dim current_node As vTreeNode = FiltersFiltersValuesTV.SelectedNode
            If current_node.Value.IndexOf(AxisFiltersController.m_FilterTag) > -1 Then

                Dim filterId As Int32 = Strings.Right(current_node.Value, Len(current_node.Value) - Len(AxisFiltersController.m_FilterTag))
                ' Delete Category
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category: " + Chr(13) + current_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Category deletion confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    Controller.DeleteFilter(filterId)
                End If
            Else
                ' Delete Category Value
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category value: " + Chr(13) + current_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Category value deletion confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    Controller.DeleteFilterValue(current_node.Value)
                End If
            End If
        End If

    End Sub

#End Region


#Region "Events"

    Private Sub TV_AfterSelect(sender As Object, e As vTreeViewEventArgs)

        FiltersFiltersValuesTV.SelectedNode = e.Node

    End Sub

    Private Sub FiltersFiltersValuesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteBT_Click(sender, e)
            Case Keys.Up
                If e.Control Then
                    If Not FiltersFiltersValuesTV.SelectedNode Is Nothing Then
                        VTreeViewUtil.MoveNodeUp(FiltersFiltersValuesTV.SelectedNode)
                    End If
                End If
            Case Keys.Down
                If e.Control Then
                    If Not FiltersFiltersValuesTV.SelectedNode Is Nothing Then
                        VTreeViewUtil.MoveNodeDown(FiltersFiltersValuesTV.SelectedNode)
                    End If
                End If
        End Select

    End Sub

    Private Sub ExpandAllBT_Click(sender As Object, e As EventArgs) Handles ExpandAllBT.Click
        For Each node As vTreeNode In FiltersFiltersValuesTV.Nodes
            node.Expand()
        Next
    End Sub

#End Region

    Private Sub CollapseAllBT_Click(sender As Object, e As EventArgs) Handles CollapseAllBT.Click
        For Each node As vTreeNode In FiltersFiltersValuesTV.Nodes
            node.Collapse(False)
        Next
    End Sub

    Private Sub EditStructureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditStructureToolStripMenuItem.Click
        Controller.ShowEditStructure()
    End Sub

End Class
