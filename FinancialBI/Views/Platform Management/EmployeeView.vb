Imports VIBlend.WinForms.Controls
Imports System.Windows.Forms

Friend Class EmployeeView
    Inherits AxisView


#Region "Instance variables"

    Private m_entitiesTreeview As vTreeView
    Private m_AxisOwnerId As UInt32

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_controller As AxisController, _
                   ByRef p_axisFilterValuesTV As vTreeView, _
                   ByRef p_axisFiltersTV As vTreeView)

        MyBase.New(p_controller, p_axisFilterValuesTV, p_axisFiltersTV)
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
            m_AxisOwnerId = m_entitiesTreeview.SelectedNode.Value
        End If

    End Sub


#End Region

#Region "Overrides"

    Friend Overrides Sub LoadInstanceVariables()
        If Not IsHandleCreated Then Exit Sub
        If InvokeRequired Then
            Dim MyDelegate As New LoadInstanceVariables_Delegate(AddressOf LoadInstanceVariables)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            m_controller.LoadInstanceVariables(m_AxisOwnerId)
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

        If m_AxisOwnerId <> 0 Then
            FillDGV((m_controller.GetAxisDictionary(m_AxisOwnerId)))
        End If

    End Sub

    Protected Overrides Sub AddAxisElem_cmd_Click(sender As Object, e As EventArgs)

        If m_AxisOwnerId <> 0 Then
            Dim l_entity As CRUD.AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Entities, m_AxisOwnerId)
            If l_entity Is Nothing Then Exit Sub

            If l_entity.AllowEdition = True Then
                m_controller.ShowNewAxisElemUI(m_AxisOwnerId)
            Else
                MsgBox(Local.GetValue("axis.msg_entity_axis_owner_not_allowed"))
            End If
        Else
            MsgBox(Local.GetValue("axis.msg_entity_axis_owner_selection"))
        End If

    End Sub

#End Region

    Private Sub DisplayEmployeesBelongingToAxisOwner(Optional ByRef p_AxisOwnerId As UInt32 = 0)

        If p_AxisOwnerId <> 0 Then
            ' m_controller.AxisOwnerId = p_AxisOwnerId
            m_AxisOwnerId = p_AxisOwnerId
        Else
            If m_entitiesTreeview.SelectedNode IsNot Nothing Then
                ' m_controller.AxisOwnerId = m_entitiesTreeview.SelectedNode.Value
                m_AxisOwnerId = m_entitiesTreeview.SelectedNode.Value
            Else
                Exit Sub
            End If
        End If
        LoadInstanceVariables()
        DGVColumnsInitialize()
        DGVRowsInitialize()
        FillDGV()

    End Sub

#Region "Events"

    Private Sub EntitiesTreeview_KeyPress(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            DisplayEmployeesBelongingToAxisOwner()
        End If

    End Sub

    Private Sub EntitiesTreeview_MouseDoubleClick(sender As Object, e As MouseEventArgs)

        Dim l_node = VTreeViewUtil.GetNodeAtPosition(m_entitiesTreeview, e.Location)
        If l_node IsNot Nothing Then DisplayEmployeesBelongingToAxisOwner(l_node.Value)

    End Sub

#End Region



End Class
