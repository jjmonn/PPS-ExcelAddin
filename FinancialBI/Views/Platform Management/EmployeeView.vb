Imports VIBlend.WinForms.Controls
Imports System.Windows.Forms

Friend Class EmployeeView
    Inherits AxisView


#Region "Instance variables"

    Private m_entitiesTreeview As vTreeView
    Private m_axisParentId As UInt32


#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_controller As AxisController, _
                   ByRef p_axisTV As vTreeView, _
                   ByRef p_axisFilterValuesTV As vTreeView, _
                   ByRef p_axisFiltersTV As vTreeView)

        MyBase.New(p_controller, p_axisTV, p_axisFilterValuesTV, p_axisFiltersTV)
        VTreeViewUtil.InitTVFormat(m_entitiesTreeview)
        m_entitiesTreeview.ImageList = VTreeViewUtil.GetEntitiesImageList()
        GlobalVariables.AxisElems.LoadEntitiesTV(m_entitiesTreeview)
        SetupEntitySelection()

        AddHandler m_entitiesTreeview.KeyDown, AddressOf EntitiesTreeview_KeyPress
        AddHandler m_entitiesTreeview.MouseDoubleClick, AddressOf EntitiesTreeview_MouseDoubleClick

    End Sub

    Private Sub SetupEntitySelection()

        If m_entitiesTreeview.Nodes.Count > 0 Then
            m_entitiesTreeview.SelectedNode = m_entitiesTreeview.Nodes(0)
            m_axisParentId = m_entitiesTreeview.SelectedNode.Value
        End If

    End Sub


#End Region

#Region "Overrides"

    Friend Overrides Sub LoadInstanceVariables()
        If InvokeRequired Then
            Dim MyDelegate As New LoadInstanceVariables_Delegate(AddressOf LoadInstanceVariables)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            If m_axisParentId <> 0 Then
                m_controller.LoadInstanceVariables(m_axisParentId)
            End If
        End If
    End Sub

    Protected Overrides Sub LoadControls()

        Dim l_splitContainer As New vSplitContainer
        l_splitContainer.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        Me.TableLayoutPanel1.Controls.Add(l_splitContainer, 0, 1)
        l_splitContainer.Dock = DockStyle.Fill
        l_splitContainer.SplitterSize = 2
        l_splitContainer.SplitterDistance = 50

        m_entitiesTreeview = New vTreeView
        l_splitContainer.Panel1.Controls.Add(m_entitiesTreeview)
        l_splitContainer.Panel2.Controls.Add(m_axisDataGridView)
        m_entitiesTreeview.Dock = DockStyle.Fill
        m_axisDataGridView.Dock = DockStyle.Fill

    End Sub

    Protected Overrides Sub FillDGV()

        If m_axisParentId <> 0 Then
            FillDGV((m_controller.GetAxisDictionary(m_axisParentId)))
        End If

    End Sub

    Protected Overrides Sub CreateAxisOrder()

        If m_axisParentId <> 0  Then
            Dim l_entity As CRUD.AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Entities, m_axisParentId)
            If l_entity Is Nothing Then Exit Sub

            If l_entity.AllowEdition = True Then

                Dim axisName As String = InputBox(Local.GetValue("axis.msg_enter_name"), Local.GetValue("axis.msg_axis_creation"))
                If axisName <> "" Then
                    m_controller.CreateAxis(axisName, m_axisParentId)
                End If
            Else
                MsgBox(Local.GetValue("axis.msg_entity_axis_parent_not_allowed"))
            End If
        Else
            MsgBox(Local.GetValue("axis.msg_entity_axis_parent_selection"))
        End If

    End Sub

#End Region

    Private Sub DisplayEmployeesBelongingToAxisParent(Optional ByRef p_axisParentId As UInt32 = 0)

        If p_axisParentId <> 0 Then
            '        m_controller.AxisParentId = p_axisParentId
            m_axisParentId = p_axisParentId
        Else
            If m_entitiesTreeview.SelectedNode IsNot Nothing Then
                '               m_controller.AxisParentId = m_entitiesTreeview.SelectedNode.Value
                m_axisParentId = m_entitiesTreeview.SelectedNode.Value
            Else
                Exit Sub
            End If
        End If
        LoadInstanceVariables()
        DGVColumnsInitialize()
        DGVRowsInitialize(m_axisTreeview)
        FillDGV()

    End Sub

#Region "Events"

    Private Sub EntitiesTreeview_KeyPress(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            DisplayEmployeesBelongingToAxisParent()
        End If

    End Sub

    Private Sub EntitiesTreeview_MouseDoubleClick(sender As Object, e As MouseEventArgs)

        Dim l_node = VTreeViewUtil.GetNodeAtPosition(m_entitiesTreeview, e.Location)
        If l_node IsNot Nothing Then DisplayEmployeesBelongingToAxisParent(l_node.Value)

    End Sub

#End Region



End Class
