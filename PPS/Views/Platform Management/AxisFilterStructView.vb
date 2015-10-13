' EntitySelectionForUsersMGT.vb
'
' Entities Treeview for the selection and holds the key | names dictionary
'
'
' To do:
'
'
'
' Known bugs :
'
' Last modified: 13/10/2015
' Author: Julien Monnereau


Imports VIBlend.WinForms.DataGridView
Imports System.Drawing
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls
Imports System.Collections


Class AxisFilterStructView

#Region "Instances"
    Private m_newFilterUI As NewFilterUI
    Private m_controller As AxisFiltersController
    Private m_filtersTV As vTreeView
    Private m_axisId As Int32
#End Region

#Region "Initialize"

    Public Sub New(ByRef p_axisFiltersTV As vTreeView, ByRef p_axisId As Int32, ByRef p_controller As AxisFiltersController, ByRef p_filtersNode As vTreeNode)

        ' This call is required by the designer.
        InitializeComponent()
        m_axisId = p_axisId
        m_filtersTV = p_axisFiltersTV
        VTreeViewUtil.InitTVFormat(p_axisFiltersTV)
        VPanel2.Content.Controls.Add(p_axisFiltersTV)
        p_axisFiltersTV.Dock = DockStyle.Fill
        m_controller = p_controller
        m_newFilterUI = New NewFilterUI(p_controller, p_filtersNode)
        m_filtersTV.ContextMenuStrip = m_structureTreeviewRightClickMenu

    End Sub

#End Region

#Region "Events"

    Delegate Sub DeleteFilter_Delegate(ByRef p_filterId As Int32)
    Friend Sub DeleteFilter(ByRef p_filterId As Int32)
        If InvokeRequired Then
            Dim MyDelegate As New DeleteFilter_Delegate(AddressOf DeleteFilter)
            Me.Invoke(MyDelegate, New Object() {p_filterId})
            Exit Sub
        End If
        DeleteNode(m_filtersTV.Nodes, p_filterId)
        m_filtersTV.Refresh()
    End Sub

    Delegate Sub SetFilter_Delegate(ByRef p_filter As Hashtable)
    Friend Sub SetFilter(ByRef p_filter As Hashtable)
        If InvokeRequired Then
            Dim MyDelegate As New SetFilter_Delegate(AddressOf SetFilter)
            Me.Invoke(MyDelegate, New Object() {p_filter})
            Exit Sub
        End If
        Dim targetNode As vTreeNode = FindNode(m_filtersTV.Nodes, p_filter(ID_VARIABLE))

        If targetNode Is Nothing Then
            targetNode = New vTreeNode
            targetNode.Value = CInt(p_filter(ID_VARIABLE))
            AddNode(m_filtersTV.Nodes, p_filter(PARENT_ID_VARIABLE), targetNode)
        End If
        targetNode.Text = p_filter(NAME_VARIABLE)
    End Sub

    Private Function FindNode(ByRef p_nodeList As vTreeNodeCollection, ByRef p_value As Int32) As vTreeNode
        For Each node As vTreeNode In p_nodeList
            If CInt(node.Value) = p_value Then Return node
            For Each child In node.Nodes
                If CInt(child.Value) = p_value Then Return child
                Dim result As vTreeNode = FindNode(child.Nodes, p_value)
                If Not result Is Nothing Then Return result
            Next
        Next
        Return Nothing
    End Function

    Private Sub AddNode(ByRef p_nodeList As vTreeNodeCollection, ByRef p_parent As Int32, ByRef p_newNode As vTreeNode)
        If (p_parent = 0) Then
            p_nodeList.Add(p_newNode)
            Exit Sub
        End If
        For Each node As vTreeNode In p_nodeList
            If node.Value = p_parent Then
                node.Nodes.Add(p_newNode)
                Exit Sub
            End If
            AddNode(node.Nodes, p_parent, p_newNode)
        Next
    End Sub

    Private Sub DeleteNode(ByRef p_node As vTreeNodeCollection, ByRef p_value As Int32)
        For Each node As vTreeNode In p_node
            If CInt(node.Value) = p_value Then
                node.Remove()
                Exit Sub
            End If

            DeleteNode(node.Nodes, p_value)
        Next
    End Sub

    Delegate Sub UpdateFiltersTV_Delegate()
    Friend Sub UpdateFiltersTV()
        If InvokeRequired Then
            Dim MyDelegate As New UpdateFiltersTV_Delegate(AddressOf UpdateFiltersTV)
            Me.Invoke(MyDelegate, New Object() {})
            Exit Sub
        End If
        GlobalVariables.Filters.LoadFiltersTV(m_filtersTV, m_axisId)
        m_filtersTV.Refresh()
    End Sub

    Private Sub AxisFilterStructView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub DeleteBT_Click(sender As Object, e As EventArgs) Handles DeleteBT.Click, m_deleteButton.Click

        ' to be validated 
        ' priority normal 
        ' !!!
        If Not m_filtersTV.SelectedNode Is Nothing Then
            Dim current_node As vTreeNode = m_filtersTV.SelectedNode
            If m_controller.IsFilter(m_filtersTV.SelectedNode.Value) Then
                ' Delete Category
                Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category: " + Chr(13) + current_node.Text + Chr(13) + "Do you confirm?", _
                                                         "Category deletion confirmation", _
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    m_controller.DeleteFilter(current_node.Value)
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

    Private Sub RenameMenuBT_Click(sender As Object, e As EventArgs) Handles m_renameButton.Click
        If Not m_filtersTV.SelectedNode Is Nothing Then
            Dim current_node As vTreeNode = m_filtersTV.SelectedNode
            Dim name = InputBox("Please enter the new Category Name:")
            If name <> "" Then
                If m_controller.IsAllowedFilterName(name) Then
                    m_controller.UpdateFilter(current_node.Value, NAME_VARIABLE, name)
                Else
                    MsgBox("This name is already used or contains forbiden characters.")
                End If
            End If
        Else
            MsgBox("A Category must be selected in order ot Add a Value.")
        End If
    End Sub

    Private Sub CreateFilterBT_Click(sender As Object, e As EventArgs) Handles AddBT.Click, m_createButton.Click

        Dim currentNode As vTreeNode = m_filtersTV.SelectedNode
        If m_filtersTV.SelectedNode Is Nothing Then
            MsgBox("Please select a Category.")
        Else
            Dim newFilterName As String = InputBox("Enter the name of the New Category", "New Category")
            If newFilterName <> "" Then
                If GlobalVariables.Filters.IsNameValid(newFilterName) = True Then
                    m_controller.CreateFilter(newFilterName, currentNode.Value, False)
                Else
                    MsgBox("This name is already in use.")
                End If
            End If
        End If
    End Sub

#End Region

End Class