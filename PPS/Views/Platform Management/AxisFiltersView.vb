' AnalalysisCategoriesControl.vb
'
'
'
' Author: Julien Monnereau
' Last modified: 14/10/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class AxisFiltersView


#Region "Instance Variables"

    ' Variables
    Private m_controller As AxisFiltersController
    Friend m_filtersFiltersValuesTV As New vTreeView
    Private m_filtersNode As vTreeNode
    Private m_axisId As Int32

#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_controller As AxisFiltersController, _
                   ByRef p_filtersNode As vTreeNode, _
                   ByRef p_axidId As Int32)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        m_axisId = p_axidId
        m_filtersNode = p_filtersNode
        AxisFilterManager.LoadFvTv(m_filtersFiltersValuesTV, m_filtersNode, m_axisId)

        m_tableLayoutPanel.Controls.Add(m_filtersFiltersValuesTV, 0, 1)
        m_filtersFiltersValuesTV.Dock = DockStyle.Fill
        m_filtersFiltersValuesTV.ImageList = ImageList1
        m_filtersFiltersValuesTV.ContextMenuStrip = RCM_TV
        VTreeViewUtil.InitTVFormat(m_filtersFiltersValuesTV)

        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then
            CategoriesToolStripMenuItem.Enabled = False
            RenameRCM.Enabled = False
            DeleteRCM.Enabled = False
            AddValueRCM.Enabled = False
            EditStructureToolStripMenuItem.Enabled = False
        End If

        AddHandler m_filtersFiltersValuesTV.KeyDown, AddressOf FiltersFiltersValuesTV_KeyDown
        AddHandler m_filtersFiltersValuesTV.AfterSelect, AddressOf TV_AfterSelect

    End Sub

    Friend Sub closeControl()

        m_controller.SendNewPositionsToModel()

    End Sub

#End Region


#Region "Interface"


    Delegate Sub UpdateFiltersValuesTV_Delegate()
    Friend Sub UpdateFiltersValuesTV()

        If InvokeRequired Then
            Dim MyDelegate As New UpdateFiltersValuesTV_Delegate(AddressOf UpdateFiltersValuesTV)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            Dim TVExpansionTemp As Dictionary(Of String, Boolean) = VTreeViewUtil.SaveNodesExpansionsLevel(m_filtersFiltersValuesTV)
            AxisFilterManager.LoadFvTv(m_filtersFiltersValuesTV, m_filtersNode, m_axisId)
            VTreeViewUtil.ResumeExpansionsLevel(m_filtersFiltersValuesTV, TVExpansionTemp)
            m_filtersFiltersValuesTV.Refresh()
        End If

    End Sub

    Delegate Sub SetFilter_Delegate(ByRef p_ht As Filter)
    Friend Sub SetFilter(ByRef p_ht As Filter)
        If InvokeRequired Then
            Dim MyDelegate As New SetFilter_Delegate(AddressOf SetFilter)
            Me.Invoke(MyDelegate, New Object() {p_ht})
        Else
            Dim newNode As New vTreeNode

            newNode.Text = p_ht.Name
            newNode.Value = p_ht.Id
            If p_ht.ParentId = 0 Then
                m_filtersFiltersValuesTV.Nodes.Add(newNode)
            Else
                Dim parentNode = GetNode(m_filtersFiltersValuesTV.Nodes, p_ht.ParentId)

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
        Dim currentNode As vTreeNode = m_filtersFiltersValuesTV.SelectedNode
        If m_filtersFiltersValuesTV.SelectedNode Is Nothing Then
            MsgBox("Please select a Category or a Value.")
            Exit Sub
        Else

            ' Check if node is filter Node
            If currentNode.Parent Is Nothing Then
                filterId = currentNode.Value
                parentFilterValueId = 0
                GoTo NewFilterValue
            End If

            If currentNode.Nodes.Count > 0 Then
                Dim filterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(CUInt(currentNode.Nodes(0).Value))
                If filterValue Is Nothing Then Exit Sub
                filterId = filterValue.FilterId
            Else
                Dim filterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(CUInt(currentNode.Nodes(0).Value))
                If filterValue Is Nothing Then Exit Sub
                Dim parentFilterId As Int32 = filterValue.ParentId
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
                m_controller.CreateFilterValue(newFilterValueName, _
                                             filterId, _
                                             parentFilterValueId)
            Else
                MsgBox("This name is already in use.")
            End If
        End If

    End Sub

    Private Sub RenameToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RenameMenuBT.Click, RenameRCM.Click

        If Not m_filtersFiltersValuesTV.SelectedNode Is Nothing Then
            Dim current_node As vTreeNode = m_filtersFiltersValuesTV.SelectedNode
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

        If Not m_filtersFiltersValuesTV.SelectedNode Is Nothing Then
            Dim current_node As vTreeNode = m_filtersFiltersValuesTV.SelectedNode
            If current_node.Parent Is Nothing Then

                Dim filterId As UInt32 = current_node.Value
                ' Delete Category
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category: " + Chr(13) + current_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Category deletion confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    m_controller.DeleteFilter(filterId)
                End If
            Else
                ' Delete Category Value
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category value: " + Chr(13) + current_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Category value deletion confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    m_controller.DeleteFilterValue(current_node.Value)
                End If
            End If
        End If

    End Sub

    Private Sub CollapseAllBT_Click(sender As Object, e As EventArgs) Handles CollapseAllBT.Click
        For Each node As vTreeNode In m_filtersFiltersValuesTV.Nodes
            node.Collapse(False)
        Next
    End Sub

    Private Sub EditStructureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditStructureToolStripMenuItem.Click
        m_controller.ShowEditStructure()
    End Sub


#End Region


#Region "Events"

    Private Sub TV_AfterSelect(sender As Object, e As vTreeViewEventArgs)

        m_filtersFiltersValuesTV.SelectedNode = e.Node

    End Sub

    Private Sub FiltersFiltersValuesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteBT_Click(sender, e)

                ' Below -> to be reviewed because it crashes sometimes !!! check priority normal
                'Case Keys.Up
                '    If e.Control Then
                '        If Not m_filtersFiltersValuesTV.SelectedNode Is Nothing Then
                '            VTreeViewUtil.MoveNodeUp(m_filtersFiltersValuesTV.SelectedNode)
                '        End If
                '    End If
                'Case Keys.Down
                '    If e.Control Then
                '        If Not m_filtersFiltersValuesTV.SelectedNode Is Nothing Then
                '            VTreeViewUtil.MoveNodeDown(m_filtersFiltersValuesTV.SelectedNode)
                '        End If
                '    End If
        End Select

    End Sub

    Private Sub ExpandAllBT_Click(sender As Object, e As EventArgs) Handles ExpandAllBT.Click
        For Each node As vTreeNode In m_filtersFiltersValuesTV.Nodes
            node.Expand()
        Next
    End Sub

#End Region

  
End Class
