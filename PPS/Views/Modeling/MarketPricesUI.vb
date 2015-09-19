'' MarketPricesUI.vb
''
''
''
''
'' Author: Julien Monnereau
'' Last modified: 05/02/2015


'Imports System.Collections.Generic
'Imports VIBlend.WinForms.DataGridView
'Imports System.Collections
'Imports System.Windows.Forms
'Imports System.Windows.Forms.DataVisualization.Charting


'Friend Class MarketPricesUI


'#Region "Instance Variables"

'    ' Objects
'    Friend Controller As MarketPricesController
'    Friend marketPricesView As MarketPricesView
'    Private chart As Chart

'    ' Variables
'    Private mainMenuFlag As Boolean
'    Private indexesMenuFlag As Boolean
'    Private VersionsMenuFlag As Boolean = True
'    Private right_clicked_node As TreeNode

'    ' Menu
'    Private version_splitter_distance As Double = 240
'    Private chart_splitter_distance As Double = 500
'    Private isVersionDisplayed As Boolean = False
'    Private isChartDisplayed As Boolean = True

'#End Region


'#Region "Initialize"

'    Protected Friend Sub New(ByRef MarketPricesController As MarketPricesController)

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.
'        Controller = MarketPricesController
'        Dim ht As New Hashtable
'        ht.Add(name_variable, "Market Prices")
'        chart = ChartsUtilities.CreateChart(ht)
'        SplitContainer2.Panel2.Controls.Add(chart)
'        chart.Dock = DockStyle.Fill
'        chart.BorderlineColor = Drawing.Color.Gray
'        chart.BorderlineWidth = 1
'        marketPricesView = New MarketPricesView(prices_DGV, chart, Controller)

'    End Sub

'    Private Sub MarketPricesUILoad(sender As Object, e As EventArgs) Handles MyBase.Load

'        Me.WindowState = FormWindowState.Maximized
'        prices_DGV.AllowCopyPaste = True
'        prices_DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
'        prices_DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
'        prices_DGV.Refresh()
'        prices_DGV.Select()
'        prices_DGV.RowsHierarchy.ExpandAllItems()

'    End Sub

'#End Region


'#Region "Call Backs"

'#Region "Main Menu Call backs"

'    Private Sub VersionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles versions_button.Click

'        If isVersionDisplayed = True Then
'            CollapseVersionPane()
'            isVersionDisplayed = False
'        Else
'            ExpandVersionPane()
'            isVersionDisplayed = True
'        End If
'    End Sub

'    Private Sub chart_display_Click(sender As Object, e As EventArgs) Handles chart_button.Click

'        If isChartDisplayed = True Then
'            CollapseChartPane()
'            isChartDisplayed = False
'        Else
'            ExpandChartPane()
'            isChartDisplayed = True
'        End If

'    End Sub

'    Private Sub ImportFromExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFromExcelToolStripMenuItem.Click

'        Controller.ImportPricessFromExcel()

'    End Sub

'#End Region

'#Region "Indexes Call backs"

'    Private Sub NewIndexBT_Click(sender As Object, e As EventArgs) Handles AddNewIndexToolStripMenuItem.Click, _
'                                                                           AddIndexToolStripMenuItem.Click
'        Controller.AddNewIndex()

'    End Sub

'    Private Sub DeleteIndexBT_Click(sender As Object, e As EventArgs) Handles DeleteIndexToolStripMenuItem1.Click

'        Dim index As String = ""
'        If Not prices_DGV.CellsArea.SelectedCells(0).ColumnItem Is Nothing Then index = prices_DGV.CellsArea.SelectedCells(0).ColumnItem.Caption
'        If index <> "" Then
'            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the Index " + Chr(13) + Chr(13) + _
'                                                     index + Chr(13) + Chr(13), _
'                                                     "Index deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
'            If confirm = DialogResult.Yes Then
'                Controller.DeleteIndex(index)
'            End If
'        Else
'            MsgBox("No Index selected")
'        End If

'    End Sub


'#End Region

'#Region "Versions Right Click Menu"

'    Private Sub select_version_Click(sender As Object, e As EventArgs) Handles select_version.Click

'        If right_clicked_node.SelectedImageIndex = 0 Then Controller.ChangeVersion(right_clicked_node.Name)

'    End Sub

'    Private Sub AddFolderRCM_Click(sender As Object, e As EventArgs) Handles AddFolderRCM.Click

'        If Not right_clicked_node Is Nothing Then
'            If right_clicked_node.SelectedImageIndex = 1 Then
'                Dim name As String = InputBox("Please enter a name for the new Folder")
'                If Len(name) < 50 Then Controller.CreateVersion(name, 1, , , right_clicked_node) Else MsgBox("The name cannot exceed 50 characters")
'            Else
'                MsgBox("A folder can only be added to another folder")
'            End If
'        Else
'            Dim name As String = InputBox("Please enter a name for the new Folder")
'            If Len(name) < 50 Then Controller.CreateVersion(name, 1) Else MsgBox("The name cannot exceed 50 characters")
'        End If

'    End Sub

'    Private Sub AddPricesVersionRCM_Click(sender As Object, e As EventArgs) Handles AddVersionRCM.Click

'        If Not right_clicked_node Is Nothing Then
'            If Controller.IsFolderVersion(right_clicked_node.Name) = True Then
'                Controller.ShowNewPricesVersion(right_clicked_node)
'            Else
'                MsgBox("A Version can only be added under a folder")
'            End If
'        Else
'            Controller.ShowNewPricesVersion()
'        End If

'    End Sub

'    Private Sub DeleteVersionBT_Click(sender As Object, e As EventArgs) Handles DeleteVersionRCM.Click

'        If Not right_clicked_node Is Nothing Then
'            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the version " + Chr(13) + Chr(13) + _
'                                                      right_clicked_node.Text + Chr(13) + Chr(13) + _
'                                                      "This version and all sub versions will be deleted, do you confirm?" + Chr(13) + Chr(13), _
'                                                      "Version deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
'            If confirm = DialogResult.Yes Then
'                Controller.DeleteVersionsOrFolder(right_clicked_node)
'            End If
'        End If

'    End Sub

'#End Region

'#Region "DGV Right Click Menu"

'    Private Sub expand_periods_Click(sender As Object, e As EventArgs) Handles expand_periods.Click

'        prices_DGV.RowsHierarchy.ExpandAllItems()

'    End Sub

'    Private Sub collapse_periods_Click(sender As Object, e As EventArgs) Handles collapse_periods.Click

'        prices_DGV.RowsHierarchy.CollapseAllItems()

'    End Sub

'    Private Sub CopyValueDownToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyPriceDown.Click

'        marketPricesView.CopyPriceValueDown()

'    End Sub

'#End Region

'#End Region


'#Region "Events"

'    Private Sub index_version_MouseClick(sender As Object, e As MouseEventArgs) Handles index_version_TB.MouseClick

'        VersionsToolStripMenuItem_Click(sender, e)  ' Display Versions pane

'    End Sub

'#Region "Versions TV Events"

'    Private Sub versionsTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles versionsTV.NodeMouseDoubleClick

'        If e.Node.SelectedImageIndex = 0 Then Controller.ChangeVersion(e.Node.Name)

'    End Sub

'    Private Sub versionsTV_KeyPress(sender As Object, e As KeyPressEventArgs) Handles versionsTV.KeyPress

'        If e.KeyChar = Chr(13) AndAlso Not versionsTV.SelectedNode Is Nothing _
'        AndAlso versionsTV.SelectedNode.SelectedImageIndex = 0 Then Controller.ChangeVersion(versionsTV.SelectedNode.Name)

'        If e.KeyChar = Chr(10) Then DeleteVersionBT_Click(sender, e)

'    End Sub

'    Private Sub versionsTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles versionsTV.NodeMouseClick

'        If e.Button = Windows.Forms.MouseButtons.Right Then right_clicked_node = e.Node Else right_clicked_node = Nothing

'    End Sub

'#End Region

'#End Region


'#Region "Menu Utilities"

'    Private Sub CollapseVersionPane()

'        SplitContainer1.SplitterDistance = 0
'        SplitContainer1.Panel1.Hide()

'    End Sub

'    Private Sub ExpandVersionPane()

'        SplitContainer1.SplitterDistance = version_splitter_distance
'        SplitContainer1.Panel1.Show()

'    End Sub

'    Private Sub CollapseChartPane()

'        SplitContainer2.SplitterDistance = SplitContainer2.Height
'        SplitContainer2.Panel2.Hide()

'    End Sub

'    Private Sub ExpandChartPane()

'        SplitContainer2.SplitterDistance = chart_splitter_distance
'        SplitContainer2.Panel2.Show()

'    End Sub

'#End Region


'End Class