Imports VIBlend.WinForms.Controls
Imports System.Windows.Forms

Friend Class EmployeeView
    Inherits AxisView


#Region "Instance variables"

    Private m_entitiesTreeview As vTreeView

#End Region


    Friend Sub New(ByRef p_controller As AxisController, _
                   ByRef p_axisTV As vTreeView, _
                   ByRef p_axisFilterValuesTV As vTreeView, _
                   ByRef p_axisFiltersTV As vTreeView)

        MyBase.New(p_controller, p_axisTV, p_axisFilterValuesTV, p_axisFiltersTV)
        VTreeViewUtil.InitTVFormat(m_entitiesTreeview)
        '     m_entitiesTreeview.ImageList = 
        GlobalVariables.AxisElems.LoadEntitiesTV(m_entitiesTreeview)

    End Sub

    Protected Overrides Sub LoadControls()

        Dim l_splitContainer As New vSplitContainer
        Me.TableLayoutPanel1.Controls.Add(l_splitContainer, 0, 1)
        l_splitContainer.Dock = DockStyle.Fill

        m_entitiesTreeview = New vTreeView
        l_splitContainer.Panel1.Controls.Add(m_entitiesTreeview)
        l_splitContainer.Panel2.Controls.Add(m_axisDataGridView)
        m_entitiesTreeview.Dock = DockStyle.Fill
        m_axisDataGridView.Dock = DockStyle.Fill

    End Sub

End Class
