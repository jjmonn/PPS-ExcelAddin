Imports VIBlend.WinForms.Controls

Public Class PDCClientSelectionUI

#Region "Instance variables"

#End Region

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        GlobalVariables.AxisElems.LoadHierarchyAxisTree(CRUD.AxisType.Client, m_clientsTreeview)
        m_clientsTreeview.ExpandAll()
        VTreeViewUtil.InitTVFormat(m_clientsTreeview)

        m_validateButton.Text = Local.GetValue("general.validate")

    End Sub

    Friend Sub ClientSelectionMouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles m_clientsTreeview.MouseDoubleClick

        Dim l_node As vTreeNode = VTreeViewUtil.GetNodeAtPosition(m_clientsTreeview, e.Location)
        If l_node IsNot Nothing Then
            GlobalVariables.APPS.ActiveCell.Value2 = l_node.Text
            Me.Close()
        End If

    End Sub

    Friend Sub ClientSelectionKeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles m_clientsTreeview.KeyPress

        If e.KeyChar = Chr(13) _
        AndAlso m_clientsTreeview.SelectedNode IsNot Nothing Then
            GlobalVariables.APPS.ActiveCell.Value2 = m_clientsTreeview.SelectedNode.Text
            Me.Close()
        End If

    End Sub
    
    Private Sub PDCClientSelectionUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Left = GlobalVariables.APPS.ActiveCell.Left
        Me.Top = GlobalVariables.APPS.ActiveCell.Top

    End Sub

    Private Sub m_validateButton_Click(sender As Object, e As EventArgs) Handles m_validateButton.Click
        If m_clientsTreeview.SelectedNode IsNot Nothing Then
            GlobalVariables.APPS.ActiveCell.Value2 = m_clientsTreeview.SelectedNode.Text
            Me.Close()
        End If
    End Sub


End Class