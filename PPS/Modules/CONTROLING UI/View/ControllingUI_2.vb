' ControlingUI_2.vb
' 
'  
' To do: 
'       - Adjustments TV display !
'
' Known Bugs:
'     
'
'
' Author: Julien Monnereau
' Last modified: 20/08/2015


Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports System.Drawing
Imports System.IO
Imports System.Data
Imports System.Linq
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.DataGridView.Filters
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.ComponentModel
Imports VIBlend.WinForms.Controls
Imports VIBlend.Utilities


Friend Class ControllingUI_2


#Region "Instance Variables"

#Region "Objects"

    Private Controller As FinancialUIController
    Friend DGVUTIL As New DataGridViewsUtil
    Friend rightPane_Control As CUI2RightPane
    Friend leftPane_control As CUI2LeftPane
    Private leftSplitContainer As SplitContainer
    Private rightSplitContainer As SplitContainer
    Private Accounts As New Account
    Friend CP As CircularProgressUI
    Private leftPaneExpandBT As vButton
    Private rightPaneExpandBT As vButton

#End Region

#Region "Variables"

    Private right_clicked_node As vTreeNode
    Private current_DGV_cell As GridCell
    Private rows_list_dic As New Dictionary(Of String, List(Of HierarchyItem))
    Private columns_list_dic As New Dictionary(Of String, List(Of HierarchyItem))
    Private row_index As Int32
    Private column_index As Int32
    Friend accountsTV As New vTreeView

    Private hierarchyItemNormalStyle As HierarchyItemStyle
    Private hierarchyItemSelectedStyle As HierarchyItemStyle
    Private hierarchyItemDisabledStyle As HierarchyItemStyle
    Private CEStyle As GridCellStyle

    Private SP1Distance As Single = 230
    Private SP2Distance As Single = 900

#End Region

#Region "Flags"

    Private isUpdatingPeriodsCheckList As Boolean
    Private isVersionComparisonDisplayed As Boolean
    Private IsUpdatingChildrenCategory As Boolean
    Private displayControlFlag As Boolean
    Private leftPaneExpandedFlag As Boolean

#End Region

#Region "Constants"

    Private Const MARGIN_SIZE As Double = 25
    Private Const INNER_MARGIN As Integer = 0
    Private Const EXCEL_SHEET_NAME_MAX_LENGHT = 31
    Private Const DGV_THEME As Int32 = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
    Friend Const TOP_LEFT_CHART_POSITION As String = "tl"
    Friend Const TOP_RIGHT_CHART_POSITION As String = "tr"
    Friend Const BOTTOM_LEFT_CHART_POSITION As String = "bl"
    Friend Const BOTTOM_RIGHT_CHART_POSITION As String = "br"

    Friend Const ACCOUNTS_CODE As String = "Accounts"
    Friend Const YEARS_CODE As String = "Years"
    Friend Const MONTHS_CODE As String = "Months"
    Friend Const VERSIONS_CODE As String = "Versions"
    Friend Const ENTITIES_CODE As String = "Entities"
    Friend Const CLIENTS_CODE As String = "Clients"
    Friend Const PRODUCTS_CODE As String = "Products"
    Friend Const ADJUSTMENT_CODE As String = "Adjustments"

#End Region

#End Region


#Region "Initialization"

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        LeftPaneSetup()
        Controller = New FinancialUIController(Me)
        GlobalVariables.Accounts.LoadAccountsTV(accountsTV)

        ' Init TabControl
        For Each node As vTreeNode In accountsTV.Nodes
            Dim newTab As New vTabPage
            newTab.Text = node.Text
            newTab.Name = node.Value
            DGVsControlTab.TabPages.Add(newTab)
        Next
        InitializeChartsTab()
        DimensionsDisplayPaneSetup()

        hierarchyItemNormalStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleNormal
        hierarchyItemSelectedStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleNormal
        hierarchyItemDisabledStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleNormal

        CEStyle = GridTheme.GetDefaultTheme(DGV_THEME).GridCellStyle


    End Sub

    Private Sub LeftPaneSetup()

        leftPane_control = New CUI2LeftPane
        Me.SplitContainer1.Panel1.Controls.Add(leftPane_control)
        leftPane_control.Dock = DockStyle.Fill

        leftPaneExpandBT = New vButton
        leftPaneExpandBT.Width = 19
        leftPaneExpandBT.Height = 19
        leftPaneExpandBT.ImageList = ExpansionImageList
        leftPaneExpandBT.ImageIndex = 2
        leftPaneExpandBT.Text = ""
        leftPaneExpandBT.FlatStyle = FlatStyle.Flat
        leftPaneExpandBT.FlatAppearance.BorderSize = 0
        leftPaneExpandBT.PaintBorder = False
        leftPaneExpandBT.ImageAlign = Drawing.ContentAlignment.MiddleCenter
        leftPaneExpandBT.Visible = False
        Me.SplitContainer1.Panel1.Controls.Add(leftPaneExpandBT)

        AddHandler leftPane_control.entitiesTV.KeyDown, AddressOf EntitiesTV_KeyDown
        AddHandler leftPane_control.entitiesTV.MouseClick, AddressOf EntitiesTV_NodeMouseClick
        AddHandler leftPane_control.periodsTV.NodeChecked, AddressOf periodsTV_ItemCheck
        AddHandler leftPane_control.PanelCollapseBT.Click, AddressOf CollapseSP1Pane1
        AddHandler leftPaneExpandBT.Click, AddressOf ExpandSP1Pane1

        leftPane_control.entitiesTV.ContextMenuStrip = EntitiesRCMenu
        leftPane_control.adjustmentsTV.ContextMenuStrip = AdjustmentsRCMenu
        leftPane_control.periodsTV.ContextMenuStrip = PeriodsRCMenu

        Dim vNode As vTreeNode = VTreeViewUtil.FindNode(leftPane_control.versionsTV, My.Settings.version_id)
        If Not vNode Is Nothing Then
            vNode.Checked = Windows.Forms.CheckState.Checked
        End If


    End Sub

    Private Sub DataMiningUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        For Each tab_ As vTabPage In DGVsControlTab.TabPages
            DGVsControlTab.SelectedTab = tab_
            Dim DGV As New vDataGridView
            DGV.VIBlendTheme = DGV_THEME
            DGV.Name = tab_.Name
            DGV.Dock = DockStyle.Fill
            DGV.Left = INNER_MARGIN
            DGV.Top = INNER_MARGIN
            DGV.BackColor = SystemColors.Control
            DGV.ContextMenuStrip = DataGridViewsRCMenu
            tab_.Controls.Add(DGV)
            AddHandler DGV.CellMouseClick, AddressOf DGV_CellMouseClick
            CEStyle.Font = New System.Drawing.Font(DGV.Font.FontFamily, My.Settings.dgvFontSize)
        Next

        If Not IsNothing(DGVsControlTab.TabPages(0)) Then
            DGVsControlTab.SelectedTab = DGVsControlTab.TabPages(0)
        End If
        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub InitializeChartsTab()




    End Sub

    Private Sub DimensionsDisplayPaneSetup()

        ' This initialization should go into right pane !!!!!!!!!!
        ' ///////////////////////////////////////////
        ' priority high !!! 
        Dim analysis_axis_tv As New vTreeView
        VTreeViewUtil.InitTVFormat(analysis_axis_tv)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS, ACCOUNTS_CODE, analysis_axis_tv)

        ' Entities Analysis Axis and Categories Nodes
        Dim entities_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES, _
                                                               ENTITIES_CODE, _
                                                               analysis_axis_tv)

        For Each entity_node As vTreeNode In leftPane_control.entitiesFiltersTV.Nodes
            VTreeViewUtil.AddNode(Computer.FILTERS_DECOMPOSITION_IDENTIFIER & entity_node.Value, _
                                  entity_node.Text, _
                                  entities_node)
        Next

        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, YEARS_CODE, analysis_axis_tv)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS, MONTHS_CODE, analysis_axis_tv)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS, VERSIONS_CODE, analysis_axis_tv)

        ' Clients Analysis Axis and Categories Nodes
        Dim clientsNode As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS, CLIENTS_CODE, analysis_axis_tv)
        For Each client_category_node As vTreeNode In leftPane_control.clientsFiltersTV.Nodes
            VTreeViewUtil.AddNode(Computer.FILTERS_DECOMPOSITION_IDENTIFIER & client_category_node.Value, _
                                  client_category_node.Text, _
                                  clientsNode)
        Next

        ' Products Analysis Axis and Categories Nodes
        Dim products_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.PRODUCTS,
                                                               PRODUCTS_CODE, _
                                                               analysis_axis_tv)

        For Each product_category_node As vTreeNode In leftPane_control.productsFiltersTV.Nodes
            VTreeViewUtil.AddNode(Computer.FILTERS_DECOMPOSITION_IDENTIFIER & product_category_node.Value, _
                                  product_category_node.Text, _
                                  products_node)
        Next

        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER _
                              & GlobalEnums.AnalysisAxis.ADJUSTMENTS, _
                              ADJUSTMENT_CODE, analysis_axis_tv)

        rightPane_Control = New CUI2RightPane(analysis_axis_tv)
        SplitContainer2.Panel2.Controls.Add(rightPane_Control)
        rightPane_Control.Dock = DockStyle.Fill

        rightPane_Control.CollapseRightPaneBT.ImageList = ExpansionImageList
        rightPane_Control.CollapseRightPaneBT.ImageKey = "expand_right"

        AddHandler rightPane_Control.UpdateBT.Click, AddressOf RefreshFromRightPane
        AddHandler rightPane_Control.CollapseRightPaneBT.Click, AddressOf CollapseSP2Pane2

        ' go into 

        rightPaneExpandBT = New vButton
        rightPaneExpandBT.Width = 19
        rightPaneExpandBT.Height = 19
        rightPaneExpandBT.ImageList = ExpansionImageList
        rightPaneExpandBT.Margin = New Padding(3, 5, 3, 3)
        rightPaneExpandBT.ImageKey = "expand_left"
        rightPaneExpandBT.Text = ""
        rightPaneExpandBT.FlatStyle = FlatStyle.Flat
        rightPaneExpandBT.PaintBorder = False
        ' rightPaneExpandBT.ImageAlign = Drawing.ContentAlignment.MiddleCenter
        rightPaneExpandBT.Visible = False
        Me.SplitContainer2.Panel2.Controls.Add(rightPaneExpandBT)
        AddHandler rightPaneExpandBT.Click, AddressOf ExpandSP2Pane2

    End Sub

#End Region


#Region "Interface"

    Private Sub RefreshData(Optional ByRef entityNode As vTreeNode = Nothing, _
                            Optional ByRef useCache As Boolean = False)

        Dim versionsIds As New List(Of Int32)
        For Each versionId In VTreeViewUtil.GetCheckedNodesIds(leftPane_control.versionsTV)
            If GlobalVariables.Versions.versions_hash(versionId)(IS_FOLDER_VARIABLE) = False Then
                versionsIds.Add(versionId)
            End If
        Next

        If entityNode Is Nothing Then
            If leftPane_control.entitiesTV.Nodes.Count > 0 Then
                entityNode = leftPane_control.entitiesTV.Nodes(0)
            Else
                MsgBox("No Entity set up.")
                Exit Sub
            End If
        Else
            If versionsIds.Count > 0 Then
                Controller.Compute(versionsIds.ToArray, entityNode)
            Else
                MsgBox("At least one version must be selected.")
            End If
        End If

    End Sub

    Private Sub RefreshFromRightPane()

        If Not Controller.EntityNode Is Nothing Then
            RefreshData(Controller.EntityNode, True)
        Else
            If Not leftPane_control.entitiesTV.SelectedNode Is Nothing Then
                RefreshData(leftPane_control.entitiesTV.SelectedNode, True)
            Else
                MsgBox("Please select an Entity to refresh")
            End If
        End If

    End Sub

    Friend Sub FormatDGVItem(ByRef item As HierarchyItem)

        item.HierarchyItemStyleNormal = hierarchyItemNormalStyle
        item.HierarchyItemStyleSelected = hierarchyItemSelectedStyle
        item.HierarchyItemStyleDisabled = hierarchyItemDisabledStyle

        item.CellsStyle = CEStyle
        item.CellsTextAlignment = System.Drawing.ContentAlignment.MiddleRight

        ' Manage cells formats according to account type, currencies and units
        'priority normal

        If item.IsColumnsHierarchyItem _
       AndAlso item.Caption.Length > 20 Then
            item.TextWrap = True
        End If

    End Sub

#End Region


#Region "Events"


    'Private Sub VTreeView1_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles VTreeView1.KeyDown

    'End Sub

    Private Sub EntitiesTV_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            If Not leftPane_control.entitiesTV.SelectedNode Is Nothing Then
                RefreshData(leftPane_control.entitiesTV.SelectedNode)
            Else
                RefreshData()
            End If
        End If

    End Sub

    Private Sub EntitiesTV_NodeMouseClick(sender As Object, e As MouseEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Right Then
            right_clicked_node = leftPane_control.entitiesTV.HitTest(e.Location)
        End If

    End Sub

    ' Periods filter when unchecked
    Private Sub periodsTV_ItemCheck(sender As Object, e As vTreeViewEventArgs)

        If isUpdatingPeriodsCheckList = False Then
            Dim periodSelectionDict As New Dictionary(Of String, Boolean)

            For Each node As vTreeNode In leftPane_control.periodsTV.GetNodes
                If node.Checked = CheckState.Checked Then
                    periodSelectionDict.Add(node.Text, True)
                Else
                    periodSelectionDict.Add(node.Text, False)
                End If
            Next
            Controller.PeriodsSelectionFilter(periodSelectionDict)
        End If

    End Sub

    Private Sub DGV_CellMouseClick(sender As Object, e As CellMouseEventArgs)

        current_DGV_cell = e.Cell

    End Sub

    Private Sub tabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DGVsControlTab.SelectedIndexChanged

        Controller.cellsUpdateNeeded = True

    End Sub

#End Region


#Region "Calls Backs"

#Region "Data Grid View Righ Click Menu Calls backs"

    Private Sub compute_Click(sender As Object, e As EventArgs) Handles RefreshRightClick.Click

        RefreshData(right_clicked_node)

    End Sub


    Private Sub ColumnsAutoFitBT_Click(sender As Object, e As EventArgs) Handles ColumnsAutoFitBT.Click

        My.Settings.controllingUIResizeTofitGrid = ColumnsAutoFitBT.Checked
        My.Settings.Save()

    End Sub

    Private Sub ColumnsAutoSize_Click(sender As Object, e As EventArgs) Handles ColumnsAutoSize.Click

        Dim dgv As vDataGridView = DGVsControlTab.SelectedTab.Controls(0)
        dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        dgv.Refresh()
        dgv.Select()

    End Sub

    Private Sub DisplayDataTrackingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayDataTrackingToolStripMenuItem.Click

        If Not current_DGV_cell Is Nothing Then
            ' to be implemented -> quid -> find cell's account, entity, period, version from nothing...
        End If

    End Sub

    Private Sub FormatsRCMBT_Click(sender As Object, e As EventArgs) Handles DGVFormatsButton.Click

        Dim formatsController As New FormatsController

    End Sub


#End Region

#Region "Main Menu Calls Backs"

    Private Sub VersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionsComparisonToolStripMenuItem.Click

        If isVersionComparisonDisplayed = True Then
            Controller.VersionsCompDisplay(False)
            isVersionComparisonDisplayed = False
        Else
            If Controller.versionsDict.Count = 2 Then
                Controller.VersionsCompDisplay(True)
                isVersionComparisonDisplayed = True
            Else
                MsgBox("Two versions must be selected in order to display the comparison.")
            End If
        End If

    End Sub

    Private Sub SwitchVersionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SwitchVersionsToolStripMenuItem.Click
        Controller.ReverseVersionsComparison()
    End Sub

    Private Sub HideVersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideVersionsComparisonToolStripMenuItem.Click

        If isVersionComparisonDisplayed Then
            For Each tab_ As vTabPage In DGVsControlTab.TabPages
                Dim DGV As vDataGridView = tab_.Controls(0)
                DGVUTIL.RemoveVersionsComparison(DGV)
            Next
            isVersionComparisonDisplayed = False
        End If

    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Controller.dropOnExcel()
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click

        If leftPane_control.entitiesTV.SelectedNode Is Nothing Then
            RefreshData()
        Else
            RefreshData(leftPane_control.entitiesTV.SelectedNode)
        End If

    End Sub


#End Region

#Region "Periods Checked List Box Call backs"

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click

        isUpdatingPeriodsCheckList = True
        VTreeViewUtil.CheckStateAllNodes(leftPane_control.periodsTV, True)
        isUpdatingPeriodsCheckList = False
        periodsTV_ItemCheck(sender, New vTreeViewEventArgs(leftPane_control.periodsTV.SelectedNode, vTreeViewAction.Unknown))

    End Sub

    Private Sub UnselectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnselectAllToolStripMenuItem.Click

        isUpdatingPeriodsCheckList = True
        VTreeViewUtil.CheckStateAllNodes(leftPane_control.periodsTV, False)
        isUpdatingPeriodsCheckList = False
        periodsTV_ItemCheck(sender, New vTreeViewEventArgs(leftPane_control.periodsTV.SelectedNode, vTreeViewAction.Unknown))


    End Sub

#End Region


#Region "Adjustments RCM"

    Private Sub SelectAllToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem1.Click

        SetAdjustmentsSelection(True)

    End Sub

    Private Sub UnselectAllToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles UnselectAllToolStripMenuItem1.Click

        SetAdjustmentsSelection(False)

    End Sub

    Private Sub SetAdjustmentsSelection(ByRef state As Boolean)

        For Each node As vTreeNode In leftPane_control.adjustmentsTV.Nodes
            node.Checked = state
        Next

    End Sub


#End Region


#Region "Charts Utilities"

    ' Manages the charts configuration
    ' -> Test sur un per user config !
    '    


    Protected Friend Sub DrawChart(ByRef chart_position As String, _
                                   ByRef ChartHT As Hashtable, _
                                   ByRef SeriesHT_Dic As Dictionary(Of String, Hashtable), _
                                   ByRef SeriesData_Dic As Dictionary(Of String, Double()))

        Dim chart As Chart = ChartsUtilities.CreateChart(ChartHT)

        ' create chart
        ' add series
        ' bind series


        InsertChart(chart, chart_position)

    End Sub

    Private Sub InsertChart(ByRef chart As Chart, _
                            ByRef chart_position As String)

        Select Case chart_position
            Case TOP_LEFT_CHART_POSITION
                leftSplitContainer.Panel1.Controls.Clear()
                leftSplitContainer.Panel1.Controls.Add(chart)
            Case TOP_RIGHT_CHART_POSITION
                rightSplitContainer.Panel1.Controls.Clear()
                rightSplitContainer.Panel1.Controls.Add(chart)
            Case BOTTOM_LEFT_CHART_POSITION
                leftSplitContainer.Panel2.Controls.Clear()
                leftSplitContainer.Panel2.Controls.Add(chart)
            Case BOTTOM_RIGHT_CHART_POSITION
                rightSplitContainer.Panel2.Controls.Clear()
                rightSplitContainer.Panel2.Controls.Add(chart)
        End Select

    End Sub


#End Region


#Region "Left Pane Expansion/ Collapse"

    Private Sub ExpandSP2Pane2()

        SplitContainer2.SplitterDistance = SP2Distance
        rightPane_Control.Visible = True
        rightPaneExpandBT.Visible = False

    End Sub

    Private Sub CollapseSP2Pane2()

        SP2Distance = SplitContainer2.SplitterDistance
        SplitContainer2.SplitterDistance = SplitContainer2.Width - 27
        rightPane_Control.Visible = False
        rightPaneExpandBT.Visible = True

    End Sub

    Private Sub CollapseSP1Pane1()

        SplitContainer1.SplitterDistance = 25
        leftPane_control.Visible = False
        leftPaneExpandBT.Visible = True

    End Sub

    Private Sub ExpandSP1Pane1()

        SplitContainer1.SplitterDistance = SP1Distance
        SplitContainer1.Panel1.Show()
        leftPane_control.Visible = True
        leftPaneExpandBT.Visible = False

    End Sub


#End Region


#End Region


#Region "ThreadSafe"

    Delegate Sub AfterDisplayAttemp_Delegate()

    Friend Sub AfterDisplayAttemp_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New AfterDisplayAttemp_Delegate(AddressOf AfterDisplayAttemp_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            CP.Dispose()
            For Each tab_ As vTabPage In DGVsControlTab.TabPages
                Dim dgv As vDataGridView = tab_.Controls(0)
                dgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
                dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
                On Error Resume Next
                dgv.Select()
                dgv.Refresh()
                dgv.Update()
                dgv.ColumnsHierarchy.ExpandAllItems()
            Next

        End If

    End Sub

    Delegate Sub DGVFormattingAttemp_Delegate()

    Friend Sub FormatDGV_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New DGVFormattingAttemp_Delegate(AddressOf FormatDGV_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            For Each tab_ As vTabPage In DGVsControlTab.TabPages
                Dim dgv As vDataGridView = tab_.Controls(0)
                dgv.GroupingDefaultHeaderTextVisible = True
                dgv.BackColor = Color.White
                dgv.GridLinesDisplayMode = GridLinesDisplayMode.DISPLAY_NONE
                dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
                dgv.RowsHierarchy.CompactStyleRenderingEnabled = True
                dgv.Refresh()
            Next
        End If

    End Sub


#End Region




   
End Class
