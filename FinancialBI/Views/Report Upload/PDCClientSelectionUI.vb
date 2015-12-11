Imports VIBlend.WinForms.Controls

Public Class PDCClientSelectionUI

#Region "Instance variables"

    Friend m_clientsTreeview As New vTreeView

#End Region

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        GlobalVariables.AxisElems.LoadAxisTree(CRUD.AxisType.Client, m_clientsTreeview)
        Me.Controls.Add(m_clientsTreeview)
        VTreeViewUtil.InitTVFormat(m_clientsTreeview)
        m_clientsTreeview.Dock = Windows.Forms.DockStyle.Fill
        m_clientsTreeview.ExpandAll()

        AddHandler m_clientsTreeview.KeyPress, AddressOf ClientSelectionKeyPress
        AddHandler m_clientsTreeview.MouseDoubleClick, AddressOf ClientSelectionMouseDoubleClick

    End Sub

    Friend Sub ClientSelectionMouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs)

        If m_clientsTreeview.SelectedNode IsNot Nothing Then
            GlobalVariables.APPS.ActiveCell.Value2 = m_clientsTreeview.SelectedNode.Text
            Me.Close()
        End If

    End Sub

    Friend Sub ClientSelectionKeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs)

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
End Class