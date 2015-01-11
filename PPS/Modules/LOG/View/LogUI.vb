' LogUI.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 08/01/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Drawing


Friend Class LogUI

#Region "Instance Variables"

    ' Objects
    Private Controller As LogController
    Private EntitiesTV As New TreeView
    Private AccountsTV As New TreeView
    Private DGVUtil As New DataGridViewsUtil

    ' Variables
    Protected Friend FinancialDGV As New vDataGridView
    Private period_list As List(Of Int32)
    Private current_version_id As String
    Protected Friend rows_item_account_id_dic As New Dictionary(Of HierarchyItem, String)
    Private current_DGV_cell As GridCell
    Private current_entity_node As TreeNode

    ' Display
    Friend Const DGV_FONT_SIZE As Single = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
    Private Const INNER_MARGIN As Integer = 0


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_controller As LogController, _
                             ByRef input_entitiesTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.WindowState = FormWindowState.Maximized
        Controller = input_controller
        EntitiesTV = input_entitiesTV
        EntitiesTV.ImageList = EntitiesTVImageList
        EntitiesTVPanel.Controls.Add(EntitiesTV)
        EntitiesTV.Dock = DockStyle.Fill
        EntitiesTV.ContextMenuStrip = EntitiesTVRCM
        EntitiesTV.CollapseAll()
        AddHandler EntitiesTV.KeyDown, AddressOf EntitiesTV_KeyDown
        AddHandler EntitiesTV.NodeMouseClick, AddressOf EntitiesTV_NodeMouseClick

        Account.LoadAccountsTree(AccountsTV)
        InitializeDGV()

    End Sub

    Private Sub InitializeDGV()

        Dim index As Int32 = 0
        For Each node In AccountsTV.Nodes
            DGVTabControl.TabPages.Add(node.name, node.Text)
            Dim tab_ As TabPage = DGVTabControl.TabPages.Item(index)

            Dim DGV As New vDataGridView
            DataGridViewsUtil.InitDisplayVDataGridView(DGV, DGV_THEME)

            DGV.BackColor = SystemColors.Control
            DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
            DGV.Left = INNER_MARGIN
            DGV.Top = INNER_MARGIN
            DGV.ContextMenuStrip = DGVsRCM
            AddHandler DGV.CellMouseClick, AddressOf DGV_CellMouseClick
            AddHandler DGV.CellMouseDoubleClick, AddressOf DGV_CellMouseDoubleClick

            tab_.Controls.Add(DGV)
            DGV.Dock = DockStyle.Fill

            For Each sub_node In node.Nodes
                CreateAccountsStructure(sub_node, DGV)
            Next
            DataGridViewsUtil.FormatDGVFirstColumn(DGV)
            index = index + 1
        Next

    End Sub

    Private Sub CreateAccountsStructure(ByRef input_node As TreeNode, _
                                        ByRef DGV As vDataGridView, _
                                        Optional ByRef row As HierarchyItem = Nothing)

        Dim sub_row As HierarchyItem
        If Not row Is Nothing Then
            sub_row = row.Items.Add(input_node.Text)
        Else
            sub_row = DGV.RowsHierarchy.Items.Add(input_node.Text)
        End If
        rows_item_account_id_dic.Add(sub_row, input_node.Name)

        For Each sub_node As TreeNode In input_node.Nodes
            CreateAccountsStructure(sub_node, DGV, sub_row)
        Next

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub InitializeDGVColumns(ByRef version_id As String,
                                              ByRef input_period_list As List(Of Int32), _
                                              ByRef time_config As String)

        period_list = input_period_list
        current_version_id = version_id
        For Each tab_ As TabPage In DGVTabControl.TabPages
            Dim dgv As vDataGridView = tab_.Controls(0)
            DGVUtil.CreateDGVColumns(dgv, period_list.ToArray, time_config)
            dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
            dgv.ColumnsHierarchy.AutoStretchColumns = True
        Next

        DGVUtil.FormatDGVs(DGVTabControl)
        If Not IsNothing(TabControl1.TabPages(0)) Then TabControl1.SelectedTab = TabControl1.TabPages(0)
        Me.Update()

    End Sub

    Protected Friend Sub DisplayCurrentAttributes(ByRef currency As String)

        CurrencyTB.Text = currency
        EntityTB.Text = current_entity_node.Text
        VersionTB.Text = Version_label_Sub_Ribbon.Text


    End Sub


#End Region


#Region "Call Backs"

    Private Sub DisplayDataTrackingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayDataTrackingToolStripMenuItem.Click

        If Not current_DGV_cell Is Nothing AndAlso Controller.current_entity_id <> "" Then

            Dim period_index As Int32 = current_DGV_cell.ColumnItem.ItemIndex
            Dim account_id As String = rows_item_account_id_dic(current_DGV_cell.RowItem)
            If Controller.IsAccountInput(account_id, period_index) = True Then
                Controller.DisplayDataHistory(account_id, period_list(period_index), current_DGV_cell)
            End If

        End If

    End Sub

    Private Sub DisplayEntityFinancialsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayEntityFinancialsToolStripMenuItem.Click

        If Not current_entity_node Is Nothing Then
            LaunchComputationIfInput()
        End If

    End Sub

    Private Sub RefreshBT_Click(sender As Object, e As EventArgs) Handles RefreshBT.Click

        Controller.ChangeVersionID()

    End Sub

#End Region


#Region "Events"

    Private Sub DGV_CellMouseClick(sender As Object, e As CellMouseEventArgs)

        current_DGV_cell = e.Cell

    End Sub

    Private Sub DGV_CellMouseDoubleClick(sender As Object, e As CellMouseEventArgs)

        current_DGV_cell = e.Cell
        DisplayDataTrackingToolStripMenuItem_Click(sender, e)

    End Sub

    Private Sub EntitiesTV_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            If Not EntitiesTV.SelectedNode Is Nothing Then
                current_entity_node = EntitiesTV.SelectedNode
                LaunchComputationIfInput()
            End If
        End If

    End Sub

    Private Sub EntitiesTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Right Then
            current_entity_node = e.Node
        End If

    End Sub

#End Region


#Region "Utilities"

    Private Sub LaunchComputationIfInput()

        If current_entity_node.Nodes.Count = 0 Then
            Controller.LaunchComputation(current_entity_node.Name)
        Else
            MsgBox("Log can only be displayed on Inputs Entities.")
        End If

    End Sub


#End Region

   
   
End Class