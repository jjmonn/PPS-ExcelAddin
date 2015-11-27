' UsersControl.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 09/11/2015


Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.DataGridView.Filters
Imports VIBlend.Utilities
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.Controls
Imports System.Windows.Forms
Imports CRUD

Friend Class GroupsControl

#Region "Instance variables"

    ' Objects
    Private m_controller As GroupController
    Private m_groupTreeView As VIBlend.WinForms.Controls.vTreeView
    Private m_currentNode As VIBlend.WinForms.Controls.vTreeNode
    Private m_userController As UsersController
    Private m_entitiesTV As New vTreeView
    Private m_isLoading As Boolean
#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_controller As GroupController)

        InitializeComponent()

        m_controller = p_controller
        m_groupTreeView = m_controller.GetGroupTV()
        Me.GroupsPanel.Controls.Add(m_groupTreeView)
        m_groupTreeView.Dock = DockStyle.Fill
        AddHandler m_groupTreeView.MouseClick, AddressOf groupTreeView_Click

        GlobalVariables.AxisElems.LoadEntitiesTV(m_entitiesTV)
        Me.EntitiesPanel.Controls.Add(m_entitiesTV)
        EntitiesPanel.Enabled = False
        m_entitiesTV.Dock = DockStyle.Fill
        m_entitiesTV.CheckBoxes = True

        InitializeFormats()
       
        AddHandler GlobalVariables.GroupAllowedEntities.Read, AddressOf GroupAllowedEntities_READ
        AddHandler GlobalVariables.GroupAllowedEntities.DeleteEvent, AddressOf GroupAllowedEntities_DELETE
        AddHandler m_entitiesTV.NodeChecked, AddressOf entitiesTreeView_Check

    End Sub

    Private Sub InitializeFormats()

        VTreeViewUtil.InitTVFormat(m_groupTreeView)
        VTreeViewUtil.InitTVFormat(m_entitiesTV)

    End Sub

    Friend Sub AddUserControl(ByRef p_userController As UsersController)
        m_userController = p_userController
        Me.UsersPanel.Controls.Add(m_userController.GetView())
        m_userController.GetView().Dock = DockStyle.Fill
        m_userController.ApplyFilter(0)
    End Sub

    Private Sub LoadCurrent()
        m_isLoading = True
        If (m_groupTreeView.SelectedNode Is Nothing) Then EntitiesPanel.Enabled = False Else EntitiesPanel.Enabled = True
        If m_currentNode Is Nothing Then
            If Not m_userController Is Nothing Then
                m_userController.RemoveFilter()
            End If
            Exit Sub
        End If
        If Not m_userController Is Nothing Then m_userController.ApplyFilter(CInt(m_currentNode.Value))
        CheckEntityTree(m_entitiesTV.Nodes, False)
        m_entitiesTV.Refresh()
        m_isLoading = False
    End Sub

    Private Function SetCaseStatus(ByRef p_case As vTreeNode, ByRef p_parentChecked As Boolean) As Boolean
        p_case.ShowCheckBox = True
        If (p_parentChecked = True) Then
            p_case.ShowCheckBox = False
            Return True
        End If
        If (m_controller.IsAllowedEntity(m_currentNode.Value, p_case.Value)) Then
            p_case.Checked = CheckState.Checked
            Return True
        End If
        p_case.Checked = CheckState.Unchecked
        Return False
    End Function

    Private Sub CheckEntityTree(ByRef p_node As vTreeNodeCollection, ByRef p_parentChecked As Boolean)
        For Each entityBox In p_node
            Dim checkedParent = SetCaseStatus(entityBox, p_parentChecked)
            For Each child In entityBox.Nodes
                Dim checkedChild = SetCaseStatus(child, checkedParent)
                CheckEntityTree(child.Nodes, checkedChild)
            Next
        Next
    End Sub

#End Region

#Region "UI Events"
    Private Sub groupTreeView_Click(sender As Object, e As MouseEventArgs)

        m_currentNode = VTreeViewUtil.GetNodeAtPosition(m_groupTreeView, e.Location)
        LoadCurrent()

    End Sub

    Private Sub entitiesTreeView_Check(sender As Object, e As vTreeViewEventArgs)
        If m_isLoading = True Then Exit Sub
        On Error Resume Next
        If e.Node.Checked = CheckState.Checked Then
            m_controller.AddAllowedEntity(m_currentNode.Value, e.Node.Value)
        Else
            m_controller.RemoveAllowedEntity(m_currentNode.Value, e.Node.Value)
        End If
    End Sub

#End Region

#Region "CRUD Events"

    Delegate Sub GroupAllowedEntities_READ_Delegate(ByRef p_status As ErrorMessage, ByRef p_groupAllowedEntity As GroupAllowedEntity)
    Private Sub GroupAllowedEntities_READ(ByRef p_status As ErrorMessage, ByRef p_groupAllowedEntity As GroupAllowedEntity)
        If InvokeRequired Then
            Dim MyDelegate As New GroupAllowedEntities_READ_Delegate(AddressOf GroupAllowedEntities_READ)
            Me.Invoke(MyDelegate, New Object() {p_status, p_groupAllowedEntity})
        Else
            If (Not m_currentNode Is Nothing AndAlso p_groupAllowedEntity.GroupId = m_currentNode.Value) Then
                LoadCurrent()
            End If
        End If
    End Sub

    Delegate Sub GroupAllowedEntities_DELETE_Delegate(ByRef p_status As ErrorMessage, ByRef p_id As UInt32)
    Private Sub GroupAllowedEntities_DELETE(ByRef p_status As ErrorMessage, ByRef p_id As UInt32)
        If InvokeRequired Then
            Dim MyDelegate As New GroupAllowedEntities_DELETE_Delegate(AddressOf GroupAllowedEntities_DELETE)
            Me.Invoke(MyDelegate, New Object() {p_status, p_id})
        Else
            Dim l_allowedEntity As GroupAllowedEntity = GlobalVariables.GroupAllowedEntities.GetValue(p_id)

            If (Not l_allowedEntity Is Nothing AndAlso Not m_currentNode Is Nothing AndAlso l_allowedEntity.GroupId = m_currentNode.Value) Then
                LoadCurrent()
            End If
        End If
    End Sub

#End Region

End Class
