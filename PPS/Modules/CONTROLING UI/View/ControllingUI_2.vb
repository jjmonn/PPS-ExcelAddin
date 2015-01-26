' Class: ControlingUI_2.vb
' 
'     Visualize data
'
' To do: 
'
'   1. reloading entitiesTV afetr categories selection -> same expansions levels + same selection (checkboxes) + msg box if changed
'
'   -> a priori le columns dictionary ne sert à rien !!
'   - Drop to excel -> adapt method (drill down) -> 
'   - Load DLL computer too slow -> check what is going on
'   - categories selection -> select all child nodes when parent select (and the same for deselection)
'    
'   - Zoom
'   - check if possible security breach if Controller access VIEW treeviews...
'   - delete columns index dictionary ?
'   - Dispatch monthly vs. yearly version -> frozen - Will be implemented if necessary
'
' Known Bugs:
'     
'
'
' Author: Julien Monnereau
' Last modified: 25/01/2015


Imports System.Windows.Forms
Imports Microsoft.Office.Interop
Imports System.Drawing
Imports System.IO
Imports System.Data
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.DataGridView.Filters


Friend Class ControllingUI_2


#Region "Instance Variables"

#Region "Objects"

    Private Controller As ControlingUI2Controller
    Friend DGVUTIL As New DataGridViewsUtil
    Private DROPTOEXCELController As CControlingDropOnExcel
    Friend PBar As New ProgressBarControl
    Friend accountsTV As New TreeView

#End Region

#Region "Variables"

    ' Friend tabsNodesDictionary As New Dictionary(Of String, TreeNode)
    Friend vDGVAccountsKeyLineNbDic As New Dictionary(Of Int32, Dictionary(Of String, Int32))
    Private EntitiesCategoriesSelectionList As List(Of String)
    Private periodsColumnIndexDictionary As Dictionary(Of Integer, Int32)
    Private AccountsTokenNamesDict As Hashtable
    Private AccountsKeysTabNbDict As Hashtable
    Private isUpdatingPeriodsCheckList As Boolean
    Private isVersionComparisonDisplayed As Boolean
    Private right_clicked_node As TreeNode
    Private current_DGV_cell As GridCell
    Private adjustments_lines_list As New List(Of HierarchyItem)

#End Region

#Region "Menu"

    Friend entitiesTV As New TreeView
    Friend categoriesTV As New TreeView
    Private periodsCLB As New CheckedListBox
    Friend versionsTV As New TreeView
    Private DropMenu As New TableLayoutPanel
    Friend CurrenciesCLB As New CheckedListBox

    Private EntitiesFlag As Boolean
    Private CategoriesFlag As Boolean
    Private CurrenciesFlag As Boolean
    Private VersionsFlag As Boolean
    Private PeriodsFlag As Boolean
    Private DropMenuFlag As Boolean
    Private tmpSplitterDistance As Double = 230

#End Region

#Region "Constants"

    Private Const MARGIN_SIZE As Double = 25
    Private Const INNER_MARGIN As Integer = 0
    Private Const EXCEL_SHEET_NAME_MAX_LENGHT = 31
    Friend Const DGV_FONT_SIZE As Single = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010BLUE

#End Region

#End Region


#Region "Initialization"

    Protected Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        AccountsTokenNamesDict = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_NAME_VARIABLE)
        AccountsKeysTabNbDict = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_TAB_VARIABLE)
        Controller = New ControlingUI2Controller(Me)
        DROPTOEXCELController = New CControlingDropOnExcel(Me, Controller)
        LoadTrees()
        HideAllMenuItems()

        periodsCLB.Dock = DockStyle.Fill
        periodsCLB.CheckOnClick = True

        ' Init TabControl
        For Each node As TreeNode In accountsTV.Nodes
            TabControl1.TabPages.Add(node.Text, node.Text)
            '   tabsNodesDictionary.Add(node.Text, node)
        Next
        CollapsePane1()

    End Sub

    Private Sub LoadTrees()

        TVTableLayout.Controls.Add(entitiesTV, 0, 0)
        TVTableLayout.Controls.Add(categoriesTV, 0, 1)
        TVTableLayout.Controls.Add(CurrenciesCLB, 0, 0)
        TVTableLayout.Controls.Add(periodsCLB, 0, 0)
        TVTableLayout.Controls.Add(versionsTV, 0, 0)

        DisplayFirstTreeOnly()          ' TV Table Layout
        versionsTV.ImageList = VersionsIL
        categoriesTV.ImageList = categoriesIL
        entitiesTV.ImageList = EntitiesTVImageList

        Version.LoadVersionsTree(versionsTV)
        Entity.LoadEntitiesTree(entitiesTV)
        cTreeViews_Functions.set_TV_basics_icon_index(entitiesTV)
        Account.LoadAccountsTree(accountsTV)
        Category.LoadCategoriesTree(categoriesTV)
        LoadCurrencies()

        entitiesTV.Dock = DockStyle.Fill
        categoriesTV.Dock = DockStyle.Fill
        categoriesTV.CheckBoxes = True
        categoriesTV.ExpandAll()
        versionsTV.Dock = DockStyle.Fill
        versionsTV.CheckBoxes = True
        versionsTV.ExpandAll()
        entitiesTV.CollapseAll()
        entitiesTV.CheckBoxes = True

        AddHandler entitiesTV.NodeMouseDoubleClick, AddressOf EntitiesTV_NodeMouseDoubleClick
        AddHandler entitiesTV.KeyDown, AddressOf EntitiesTV_KeyDown
        AddHandler entitiesTV.AfterCheck, AddressOf entitiesTV_AfterCheck
        AddHandler entitiesTV.NodeMouseClick, AddressOf EntitiesTV_NodeMouseClick
        AddHandler categoriesTV.AfterCheck, AddressOf Category_AfterCheck
        AddHandler CurrenciesCLB.ItemCheck, AddressOf currenciesCLB_ItemCheck
        AddHandler periodsCLB.ItemCheck, AddressOf periodsCLB_ItemCheck
        entitiesTV.ContextMenuStrip = entitiesRightClickMenu
        entitiesTV.Nodes(0).Checked = True
        periodsCLB.ContextMenuStrip = periodsRightClickMenu

    End Sub

    Private Sub LoadCurrencies()

        CurrenciesCLB.Dock = DockStyle.Fill
        CurrenciesCLB.CheckOnClick = True
        CurrenciesCLB.SelectionMode = SelectionMode.One

        Dim currenciesList As List(Of String)
        currenciesList = CurrenciesMapping.getCurrenciesList(CURRENCIES_KEY_VARIABLE)
        For Each currency_ As String In currenciesList
            CurrenciesCLB.Items.Add(currency_, False)
        Next
        CurrenciesCLB.SetItemChecked(CurrenciesCLB.FindString(MAIN_CURRENCY), True)

    End Sub

    Private Sub DataMiningUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Controls.Add(PBar)                           ' Progress Bar
        PBar.Left = (Me.Width - PBar.Width) / 2         ' Progress Bar
        PBar.Top = (Me.Height - PBar.Height) / 2        ' Progress Bar

        For Each tab_ As TabPage In TabControl1.TabPages

            TabControl1.SelectedTab = tab_
            tab_.ImageIndex = 0

            Dim DGV As New vDataGridView
            DataGridViewsUtil.InitDisplayVDataGridView(DGV, DGV_THEME)
            DGV.Name = tab_.Name
            DGV.ImageList = MenuIcons
            DGV.Dock = DockStyle.Fill
            DGV.Left = INNER_MARGIN
            DGV.Top = INNER_MARGIN
            DGV.BackColor = SystemColors.Control
            DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
            DGV.ContextMenuStrip = DGVsRCM
            AddHandler DGV.CellMouseClick, AddressOf DGV_CellMouseClick
            tab_.Controls.Add(DGV)

        Next

        If Not IsNothing(TabControl1.TabPages(0)) Then
            TabControl1.SelectedTab = TabControl1.TabPages(0)
        End If
        Me.WindowState = FormWindowState.Maximized

    End Sub


#End Region


#Region "DGVs Display Functions"

    Friend Sub VIEWInitialization(ByRef periodsList As List(Of Integer), _
                                  ByRef versionsNamesArray() As String, _
                                  ByRef versionComparisonFlag As String, _
                                  ByRef versionTimeConfig As String, _
                                  Optional ByRef entitiesArray() As String = Nothing, _
                                  Optional ByRef entity_node As TreeNode = Nothing)

        ' Add the possibility to have several rates_version 
        ' Columns
        Dim timeConfig As String
        Select Case versionComparisonFlag
            Case Version.YEARLY_VERSIONS_COMPARISON, Version.YEARLY_MONTHLY_VERSIONS_COMPARISON : timeConfig = YEARLY_TIME_CONFIGURATION
            Case Version.MONTHLY_VERSIONS_COMPARISON : timeConfig = MONTHLY_TIME_CONFIGURATION
            Case Else : timeConfig = versionTimeConfig
        End Select
        For Each tab_ As TabPage In TabControl1.TabPages
            If versionsNamesArray.Length > 1 Then
                periodsColumnIndexDictionary = DataGridViewsUtil.CreateDGVColumns(tab_.Controls(0), periodsList.ToArray, versionsNamesArray, timeConfig)
            Else
                periodsColumnIndexDictionary = DataGridViewsUtil.CreateDGVColumns(tab_.Controls(0), periodsList.ToArray, timeConfig, True)
            End If
        Next

        ' Rows
        vDGVAccountsKeyLineNbDic.Clear()
        Dim i As Integer = 0
        For Each tab_ As TabPage In TabControl1.TabPages
            vDGVAccountsKeyLineNbDic.Add(i, DGVUTIL.CreatesVDataGridViewRowsHierarchy(tab_.Controls(0), i, accountsTV, , entity_node))
            i = i + 1
        Next

        ' Menu Periods Combo Box
        InitializeMenuPeriodsCB()
        isVersionComparisonDisplayed = False

    End Sub

    Friend Sub FormatVIEWDataDisplay()

        DGVUTIL.FormatDGVs(TabControl1)
        If Not IsNothing(TabControl1.TabPages(0)) Then TabControl1.SelectedTab = TabControl1.TabPages(0)
        Me.Update()

    End Sub

    Private Sub InitializeMenuPeriodsCB()

        periodsCLB.Items.Clear()
        Dim dgv As vDataGridView = TabControl1.TabPages(0).Controls(0)
        Dim i As Int32
        isUpdatingPeriodsCheckList = True
        For Each item As HierarchyItem In dgv.ColumnsHierarchy.Items
            periodsCLB.Items.Add(item.Caption)
            periodsCLB.SetItemChecked(i, True)
            i = i + 1
        Next
        isUpdatingPeriodsCheckList = False

    End Sub

#End Region


#Region "Dispatch"

    Friend Sub Dispatch_complete(ByRef entity_node As TreeNode, _
                                  ByRef data_hash As Dictionary(Of String, Double()), _
                                  ByRef period_list As List(Of Integer), _
                                  ByRef accounts_array() As String, _
                                  Optional ByRef VersionIndex As Int32 = -1)

        Dim i As Int32 = 0
        Dim column As HierarchyItem
        For Each account_id In accounts_array
            Dim tab_index = AccountsKeysTabNbDict(account_id)
            Dim dgv As vDataGridView = TabControl1.TabPages(tab_index).controls(0)
            Dim account_row = dgv.RowsHierarchy.Items(vDGVAccountsKeyLineNbDic(tab_index)(account_id))

            Dim period_index As Int32 = 0
            For Each period In period_list
                If VersionIndex = -1 Then
                    column = dgv.ColumnsHierarchy.Items(period_index)
                Else
                    column = dgv.ColumnsHierarchy.Items(period_index).Items(VersionIndex)
                End If
                Dim value = data_hash(entity_node.Name)(i)
                dgv.CellsArea.SetCellValue(account_row, column, value)
                ' Entities loop
                Dim entity_index As Int32 = 0
                For Each child_entity_node In entity_node.Nodes
                    fill_in_entities_children(dgv, data_hash, child_entity_node, entity_index, i, account_row, column)
                    entity_index = entity_index + 1
                Next
                i = i + 1
                period_index = period_index + 1
            Next
        Next

    End Sub

    ' Recursively fills the entity hierarchy in the DGV
    Private Sub fill_in_entities_children(ByRef dgv As vDataGridView, _
                                          ByRef data_hash As Dictionary(Of String, Double()), _
                                          ByRef entity_node As TreeNode,
                                          ByVal entity_index As Int32, _
                                          ByVal i As Int32, _
                                          ByRef parent_row As HierarchyItem, _
                                          ByRef column As HierarchyItem)

        Dim value = data_hash(entity_node.Name)(i)
        Dim row = parent_row.Items(entity_index)
        dgv.CellsArea.SetCellValue(row, column, value)
        Dim child_entity_index As Int32 = 0
        For Each child_node In entity_node.Nodes
            fill_in_entities_children(dgv, data_hash, child_node, child_entity_index, i, row, column)
            child_entity_index = child_entity_index + 1
        Next


    End Sub

    ' Dispatch monthly vs. yearly version -> frozen - Will be implemented if necessary

#End Region


#Region "Adjustments Display"

    Protected Friend Sub DisplayAdjustments(ByRef adjustments_dict As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Int32, Double)))), _
                                            Optional ByRef version_index As Int32 = -1)

        Dim account_row As HierarchyItem
        Dim entity_row As HierarchyItem
        Dim period_column As HierarchyItem
        Dim DGV As vDataGridView
        Dim tab_index As Int32
        Dim adjustments_id_names As Dictionary(Of String, String) = AdjustmentsMapping.GetAdjustmentsDictionary(ADJUSTMENTS_ID_VAR, ADJUSTMENTS_NAME_VAR)

        For Each account_id In adjustments_dict.Keys
            tab_index = AccountsKeysTabNbDict(account_id)
            DGV = TabControl1.TabPages(tab_index).Controls(0)
            account_row = DGV.RowsHierarchy.Items(vDGVAccountsKeyLineNbDic(tab_index)(account_id))

            For Each entity_id In adjustments_dict(account_id).Keys
                entity_row = GetEntityRow(account_row, cTreeViews_Functions.GetNodePath(entitiesTV.Nodes.Find(entity_id, True)(0), Controller.CurrentEntityKey))

                For Each adjustment_id In adjustments_dict(account_id)(entity_id).Keys
                    Dim adjt_row As HierarchyItem = entity_row.Items.Add(adjustments_id_names(adjustment_id))
                    DataGridViewsUtil.FormatAdjustmentRow(adjt_row)
                    adjustments_lines_list.Add(adjt_row)

                    For Each Period In adjustments_dict(account_id)(entity_id)(adjustment_id).Keys
                        period_column = DGV.ColumnsHierarchy.Items(periodsColumnIndexDictionary(Period))
                        If version_index > -1 Then period_column = period_column.Items(version_index)
                        DGV.CellsArea.SetCellValue(adjt_row, period_column, adjustments_dict(account_id)(entity_id)(adjustment_id)(Period))                        
                    Next
                Next
            Next
        Next

    End Sub


#End Region


#Region "Events"

#Region "Entities TreeView"

    Private Sub EntitiesTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)


    End Sub

    Private Sub EntitiesTV_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            If Not entitiesTV.SelectedNode Is Nothing Then Controller.compute_entity_complete(entitiesTV.SelectedNode)
        End If

    End Sub

    Private Sub entitiesTV_AfterCheck(ByVal sender As Object, ByVal e As TreeViewEventArgs)

        For Each node As TreeNode In e.Node.Nodes
            node.Checked = e.Node.Checked
        Next

    End Sub

    Private Sub EntitiesTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        If e.Button = Windows.Forms.MouseButtons.Right Then right_clicked_node = e.Node

    End Sub


#End Region

    ' Category after Select
    Private Sub Category_AfterCheck(sender As Object, e As TreeViewEventArgs)

        ' apply selectedvalue to children
        ' loop through children applys same selection as parent !!
        Controller.CategoriesUpdate()

    End Sub

    ' CurrenciesCLB check item event handler
    Private Sub currenciesCLB_ItemCheck(sender As Object, e As ItemCheckEventArgs)

        For i As Int32 = 0 To CurrenciesCLB.Items.Count - 1
            If i <> e.Index Then CurrenciesCLB.SetItemChecked(i, False)
        Next

    End Sub

    ' Periods filter when unchecked
    Private Sub periodsCLB_ItemCheck(sender As Object, e As ItemCheckEventArgs)

        If isUpdatingPeriodsCheckList = False Then
            Dim selectedList As New List(Of Int32)
            For i = 0 To periodsCLB.Items.Count - 1
                If i = e.Index Then
                    If e.NewValue = CheckState.Checked Then selectedList.Add(i)
                Else
                    If periodsCLB.GetItemCheckState(i) = CheckState.Checked Then selectedList.Add(i)
                End If
            Next
            DisplaySelectedPeriods(selectedList)
        End If

    End Sub

    ' Double click on tab event
    Private Sub TabControl1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TabControl1.DoubleClick

        DROPTOEXCELController.SendTabToExcel(TabControl1.SelectedTab.Controls(0))

    End Sub

    Private Sub DGV_CellMouseClick(sender As Object, e As CellMouseEventArgs)

        current_DGV_cell = e.Cell

    End Sub


#End Region


#Region "Main Menu Calls Backs"

    Private Sub EntitiesMenuClick(sender As Object, e As EventArgs) Handles EntitiesToolStripMenuItem.Click

        If EntitiesFlag = False Then
            EntitiesToolStripMenuItem.Checked = True
            entitiesTV.Select()
            ExpandPane1()
            HideAllMenuItemsExceptCategories()
            entitiesTV.Visible = True
            EntitiesFlag = True
            If CategoriesFlag = True Then
                DisplayTwoTrees()
                TVTableLayout.SetRow(entitiesTV, 1)
            Else
                TVTableLayout.SetRow(entitiesTV, 0)
            End If
        Else
            EntitiesToolStripMenuItem.Checked = False
            If TVTableLayout.GetRow(entitiesTV) = 0 Then
                CollapsePane1()
            Else
                DisplayFirstTreeOnly()
            End If
            entitiesTV.Visible = False
            EntitiesFlag = False
        End If

    End Sub

    Private Sub CategoriesMenuClick(sender As Object, e As EventArgs) Handles CategoriesToolStripMenuItem.Click

        If CategoriesFlag = False Then
            CategoriesToolStripMenuItem.CheckState = CheckState.Checked
            ExpandPane1()
            HideAllMenusItemExceptEntities()
            categoriesTV.Visible = True
            CategoriesFlag = True
            If EntitiesFlag = True Then
                DisplayTwoTrees()
                TVTableLayout.SetRow(categoriesTV, 1)
            Else
                TVTableLayout.SetRow(categoriesTV, 0)
            End If
        Else
            CategoriesToolStripMenuItem.CheckState = CheckState.Unchecked
            If TVTableLayout.GetRow(categoriesTV) = 0 Then
                CollapsePane1()
            Else
                DisplayFirstTreeOnly()
            End If
            categoriesTV.Visible = False
            CategoriesFlag = False
        End If

    End Sub

    Private Sub CurrenciesMenuClick(sender As Object, e As EventArgs) Handles CurrenciesToolStripMenuItem.Click

        If CurrenciesFlag = False Then
            CurrenciesToolStripMenuItem.CheckState = CheckState.Checked
            ExpandPane1()
            HideAllMenuItems()
            CurrenciesCLB.Visible = True
            CurrenciesFlag = True
            DisplayTwoTrees()

        Else
            CurrenciesToolStripMenuItem.CheckState = CheckState.Unchecked
            CollapsePane1()
            CurrenciesCLB.Visible = False
            CurrenciesFlag = False
            DisplayFirstTreeOnly()
        End If

    End Sub

    Private Sub PeriodsMenuClick(sender As Object, e As EventArgs) Handles PeriodsToolStripMenuItem.Click

        If PeriodsFlag = False Then
            PeriodsToolStripMenuItem.CheckState = CheckState.Checked
            ExpandPane1()
            HideAllMenuItems()
            periodsCLB.Visible = True
            PeriodsFlag = True
        Else
            PeriodsToolStripMenuItem.CheckState = CheckState.Unchecked
            CollapsePane1()
            periodsCLB.Visible = False
            PeriodsFlag = False
        End If

    End Sub

    Private Sub VersionsMenuClick(sender As Object, e As EventArgs) Handles VersionsToolStripMenuItem.Click

        If VersionsFlag = False Then
            VersionsToolStripMenuItem.CheckState = CheckState.Checked
            ExpandPane1()
            HideAllMenuItems()
            versionsTV.Visible = True
            VersionsFlag = True
        Else
            VersionsToolStripMenuItem.CheckState = CheckState.Unchecked
            CollapsePane1()
            versionsTV.Visible = False
            VersionsFlag = False
        End If

    End Sub

    Private Sub SendCurrentEntity_Click(sender As Object, e As EventArgs) Handles DropCurrentEntToExcelToolStripMenuItem.Click

        DROPTOEXCELController.SendCurrentEntityToExcel()

    End Sub

    Private Sub DropDrillDown_Click(sender As Object, e As EventArgs) Handles DropDrillDownToExcelToolStripMenuItem.Click

        DROPTOEXCELController.SendDropDownToExcel()

    End Sub

    Private Sub Refresh_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click

        If Controller.CurrentEntityKey <> "" Then
            Controller.compute_entity_complete(entitiesTV.Nodes.Find(Controller.CurrentEntityKey, True)(0))
        ElseIf Not entitiesTV.SelectedNode Is Nothing Then
            Controller.compute_entity_complete(entitiesTV.SelectedNode)
        Else
            MsgBox("An Entity level must be selected in order to refresh " + Chr(13) + Chr(13) + _
                   " Please select an entity")
        End If

    End Sub

    Private Sub AddVersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddVersionsComparisonToolStripMenuItem.Click

        If isVersionComparisonDisplayed = True Then
            MsgBox("The Versions Comparison is already displayed")
        Else
            If Controller.versions_id_array.Length = 2 Then
                For Each tab_ As TabPage In TabControl1.TabPages
                    Dim DGV As vDataGridView = tab_.Controls(0)
                    DGVUTIL.AddVersionComparison(DGV)
                Next
                TabControl1.TabPages(0).Select()
                isVersionComparisonDisplayed = True
            Else
                MsgBox("Two versions must be selected in order to display the comparison." + Chr(13) + _
                       "Refresh must be applied after versions selection.")
            End If
        End If

    End Sub

    Private Sub SwitchVersionsOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SwitchVersionsOrderToolStripMenuItem.Click

        Dim displayComparison As Boolean
        If isVersionComparisonDisplayed = True Then
            RemoveVersionsComparisonToolStripMenuItem_Click(sender, e)
            displayComparison = True
        End If
        For Each tab_ As TabPage In TabControl1.TabPages
            Dim DGV As vDataGridView = tab_.Controls(0)
            DGVUTIL.SwitchVersionsOrderWithoutComparison(DGV)
        Next
        If displayComparison = True Then AddVersionsComparisonToolStripMenuItem_Click(sender, e)

    End Sub

    Private Sub RemoveVersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveVersionsComparisonToolStripMenuItem.Click

        If isVersionComparisonDisplayed Then
            For Each tab_ As TabPage In TabControl1.TabPages
                Dim DGV As vDataGridView = tab_.Controls(0)
                DGVUTIL.RemoveVersionsComparison(DGV)
            Next
            isVersionComparisonDisplayed = False
        End If

    End Sub


#End Region


#Region "Right Click Menus"

#Region "EntitiesTV Right Click Menu Call Backs"

    Private Sub compute_complete_Click(sender As Object, e As EventArgs) Handles compute_complete.Click

        Controller.compute_entity_complete(right_clicked_node)

    End Sub

#End Region

#Region "Periods Right Click Menu"

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click

        SetPeriodsSelection(True)

    End Sub

    Private Sub UnselectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnselectAllToolStripMenuItem.Click

        SetPeriodsSelection(False)

    End Sub

    Private Sub SetPeriodsSelection(ByRef state As Boolean)

        isUpdatingPeriodsCheckList = True
        For i = 0 To periodsCLB.Items.Count - 2
            periodsCLB.SetItemChecked(i, state)
        Next
        isUpdatingPeriodsCheckList = False
        periodsCLB.SetItemChecked(periodsCLB.Items.Count - 1, state)

    End Sub


#End Region

#Region "DGVs RCM"

    Private Sub DisplayDataTrackingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DisplayDataTrackingToolStripMenuItem.Click

        If Not current_DGV_cell Is Nothing Then
            ' to be implemented -> quid -> find cell's account, entity, period, version from nothing...
        End If

    End Sub

    Private Sub DisplayAdjustmensRCM_Click(sender As Object, e As EventArgs) Handles DisplayAdjustmensRCM.Click

        Controller.LoadAdjustments()

    End Sub

#End Region

#End Region


#Region "Utilities"

    Private Sub DisplaySelectedPeriods(ByRef selectionList As List(Of Int32))

        For Each tab_ As TabPage In TabControl1.TabPages
            Dim DGV As vDataGridView = tab_.Controls(0)

            For Each col As HierarchyItem In DGV.ColumnsHierarchy.Items
                If selectionList.Contains(col.ItemIndex) Then
                    col.Hidden = False
                Else
                    col.Hidden = True
                End If
            Next
        Next

    End Sub

#Region "Menu Utilities"

    Private Sub CollapsePane1()

        SplitContainer1.SplitterDistance = 0
        SplitContainer1.Panel1.Hide()

    End Sub

    Private Sub ExpandPane1()

        SplitContainer1.SplitterDistance = tmpSplitterDistance
        SplitContainer1.Panel1.Show()

    End Sub

    Private Sub HideAllMenuItems()

        HideAllMenusItemsCore()
        entitiesTV.Visible = False
        EntitiesFlag = False
        categoriesTV.Visible = False
        CategoriesFlag = False

    End Sub

    Private Sub HideAllMenuItemsExceptCategories()

        HideAllMenusItemsCore()
        entitiesTV.Visible = False
        EntitiesFlag = False

    End Sub

    Private Sub HideAllMenusItemExceptEntities()

        HideAllMenusItemsCore()
        categoriesTV.Visible = False
        CategoriesFlag = False

    End Sub

    Private Sub HideAllMenusItemsCore()

        DisplayFirstTreeOnly()
        CurrenciesCLB.Visible = False
        periodsCLB.Visible = False
        versionsTV.Visible = False
        DropMenu.Visible = False
        CurrenciesFlag = False
        PeriodsFlag = False
        VersionsFlag = False
        DropMenuFlag = False

    End Sub

    Private Sub DisplayFirstTreeOnly()

        TVTableLayout.RowStyles.Item(0).SizeType = SizeType.Percent
        TVTableLayout.RowStyles.Item(1).SizeType = SizeType.Percent
        TVTableLayout.RowStyles.Item(0).Height = 100
        TVTableLayout.RowStyles.Item(1).Height = 0

    End Sub

    Private Sub DisplayTwoTrees()

        TVTableLayout.RowStyles.Item(0).SizeType = SizeType.Percent
        TVTableLayout.RowStyles.Item(1).SizeType = SizeType.Percent
        TVTableLayout.RowStyles.Item(0).Height = 50
        TVTableLayout.RowStyles.Item(1).Height = 50

    End Sub


#End Region

    Private Function GetEntityRow(ByRef row As HierarchyItem, _
                                  ByRef path_list As List(Of Int32)) As HierarchyItem

        Dim entity_row As HierarchyItem = row
        For Each index In path_list
            entity_row = entity_row.Items(index)
        Next
        Return entity_row

    End Function


    Private Sub ControllingUI_2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Controller.close_model()

    End Sub

#End Region



End Class
