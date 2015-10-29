' EntitySelectionForUsersMGT.vb
'
' Entities Treeview for the selection and holds the key | names dictionary
'
'
' To do: Key down event to delete filters
'
'
'
' Known bugs :
'
'
' Last modified: 15/10/2015
' Author: Julien Monnereau


Imports VIBlend.WinForms.DataGridView
Imports System.Drawing
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls
Imports System.Collections
Imports CRUD

Class AxisFilterStructView

#Region "Instances"

    Private m_newFilterUI As NewFilterUI
    Private m_controller As AxisFiltersController
    Private m_filtersTV As vTreeView
    Private m_axisId As Int32

#End Region


#Region "Initialize"

    Public Sub New(ByRef p_axisFiltersTV As vTreeView, _
                   ByRef p_axisId As Int32, _
                   ByRef p_controller As AxisFiltersController, _
                   ByRef p_filtersNode As vTreeNode)

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

        AddHandler m_filtersTV.KeyDown, AddressOf FiltersTV_KeyDown

    End Sub

#End Region


#Region "Thread Safe Interface"

    Delegate Sub DeleteFilter_Delegate(ByRef p_filterId As Int32)
    Friend Sub DeleteFilter(ByRef p_filterId As Int32)
        If InvokeRequired Then
            Dim MyDelegate As New DeleteFilter_Delegate(AddressOf DeleteFilter)
            Me.Invoke(MyDelegate, New Object() {p_filterId})
        Else
            Dim nodeToBeRemoved As vTreeNode = VTreeViewUtil.FindNode(m_filtersTV, p_filterId)
            If IsNothing(nodeToBeRemoved) = False Then nodeToBeRemoved.Remove()
            m_filtersTV.Refresh()
        End If

    End Sub

    Delegate Sub SetFilter_Delegate(ByRef p_filter As Filter)
    Friend Sub SetFilter(ByRef p_filter As Filter)

        If InvokeRequired Then
            Dim MyDelegate As New SetFilter_Delegate(AddressOf SetFilter)
            Me.Invoke(MyDelegate, New Object() {p_filter})
        Else
            Dim targetNode As vTreeNode = VTreeViewUtil.FindNode(m_filtersTV, p_filter.Id)
            If targetNode Is Nothing Then
                If p_filter.ParentId <> 0 Then
                    Dim parentNode As vTreeNode = VTreeViewUtil.FindNode(m_filtersTV, p_filter.ParentId)
                    VTreeViewUtil.AddNode(p_filter.Id, _
                                          p_filter.Name, _
                                          parentNode)
                Else
                    VTreeViewUtil.AddNode(p_filter.Id, _
                                          p_filter.Name, _
                                          m_filtersTV)
                End If
            Else
                targetNode.Text = p_filter.Name
            End If
        End If

    End Sub

#End Region


#Region "Call Backs"

    Private Sub DeleteBT_Click(sender As Object, e As EventArgs) Handles DeleteBT.Click, m_deleteButton.Click

        If Not m_filtersTV.SelectedNode Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Category: " + Chr(13) + m_filtersTV.SelectedNode.Text + Chr(13) + "Do you confirm?", _
                                                     "Category deletion confirmation", _
                                                     MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                m_controller.DeleteFilter(m_filtersTV.SelectedNode.Value)
            End If
        End If

    End Sub

    Private Sub RenameMenuBT_Click(sender As Object, e As EventArgs) Handles m_renameButton.Click
        If Not m_filtersTV.SelectedNode Is Nothing Then
            Dim current_node As vTreeNode = m_filtersTV.SelectedNode
            Dim name = InputBox("Please enter the new Category Name:")
            If name <> "" Then
                If m_controller.IsAllowedFilterName(name) Then
                    m_controller.UpdateFilterName(current_node.Value, name)
                Else
                    MsgBox("This name is already used or contains forbiden characters.")
                End If
            End If
        Else
            MsgBox("A Category must be selected in order ot Add a Value.")
        End If
    End Sub

    Private Sub CreateFilterBTUnderSelectedNode_Click(sender As Object, e As EventArgs) Handles m_createCategoryUnderCurrentCategoryButton.Click

        Dim currentNode As vTreeNode = m_filtersTV.SelectedNode
        Dim parentId As Int32
        If currentNode Is Nothing Then
            MsgBox("A Category must be selected.")
            Exit Sub
        Else
            parentId = currentNode.Value
        End If
        Dim newFilterName As String = InputBox("Enter the name of the New Category", "New Category")
        If newFilterName <> "" Then
            If GlobalVariables.Filters.IsNameValid(newFilterName) = True Then
                m_controller.CreateFilter(newFilterName, parentId, False)
            Else
                MsgBox("This name is already in use.")
            End If
        End If
    End Sub

    Private Sub CreateFilterBT_Click(sender As Object, e As EventArgs) Handles AddBT.Click

        Dim parentId As Int32= 0
        Dim newFilterName As String = InputBox("Enter the name of the New Category", "New Category")
        If newFilterName <> "" Then
            If GlobalVariables.Filters.IsNameValid(newFilterName) = True Then
                m_controller.CreateFilter(newFilterName, parentId, False)
            Else
                MsgBox("This name is already in use.")
            End If
        End If
    End Sub

#End Region


#Region "Events"

    Private Sub FiltersTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteBT_Click(sender, e)


                ' Below -> to be reviewed because it crashes sometimes !!! check priority normal
                'Case Keys.Up
                '    If e.Control Then
                '        If Not m_filtersTV.SelectedNode Is Nothing Then
                '            VTreeViewUtil.MoveNodeUp(m_filtersTV.SelectedNode)
                '        End If
                '    End If
                'Case Keys.Down
                '    If e.Control Then
                '        If Not m_filtersTV.SelectedNode Is Nothing Then
                '            VTreeViewUtil.MoveNodeDown(m_filtersTV.SelectedNode)
                '        End If
                '    End If
        End Select

    End Sub

    Private Sub AxisFilterStructView_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

#End Region


    Private Sub AxisFilterStructView_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class