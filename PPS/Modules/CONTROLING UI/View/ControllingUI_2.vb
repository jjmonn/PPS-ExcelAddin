' Class: ControlingUI_2.vb
' 
'     Visualize data
'
' To do: 
'       - Reimplement Versions comparison !!
' 
' 
'
'
' Known Bugs:
'     
'
'
' Author: Julien Monnereau
' Last modified: 05/04/2015


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


Friend Class ControllingUI_2


#Region "Instance Variables"

#Region "Objects"

    Private Controller As ControllingUI2Controller
    Friend DGVUTIL As New DataGridViewsUtil
    Private DROPTOEXCELController As CControlingDropOnExcel
    Friend PBar As New ProgressBarControl
    Private leftSplitContainer As SplitContainer
    Private rightSplitContainer As SplitContainer

    ' Ribbons Menus
    Private DisplayMenu As New DisplayMenu
    Private ExcelMenu As New ExcelMenu
    Private BusinessControlMenu As New BusinessControlMenu

#End Region

#Region "Variables"

    ' Friend tabsNodesDictionary As New Dictionary(Of String, TreeNode)
    ' Friend vDGVAccountsKeyLineNbDic As New Dictionary(Of Int32, Dictionary(Of String, Int32))
    Private EntitiesCategoriesSelectionList As List(Of String)
    '  Private periodsColumnIndexDictionary As Dictionary(Of Integer, Int32)
    Private AccountsTokenNamesDict As Hashtable
    Protected Friend AccountsKeysTabNbDict As Hashtable
    Private isUpdatingPeriodsCheckList As Boolean
    Private isVersionComparisonDisplayed As Boolean
    Private right_clicked_node As TreeNode
    Private current_DGV_cell As GridCell
    ' Private adjustments_lines_list As New List(Of HierarchyItem)
    Private IsUpdatingChildrenCategory As Boolean = False

    Private current_column As HierarchyItem
    Private current_row_caption As String
    Private rows_list_dic As New Dictionary(Of String, List(Of HierarchyItem))
    Private columns_list_dic As New Dictionary(Of String, List(Of HierarchyItem))
    Private row_index As Int32
    Private column_index As Int32

#End Region

#Region "Treeviews"

    Protected Friend accountsTV As New TreeView
    Protected Friend entitiesTV As New TreeView
    Protected Friend entities_categoriesTV As New TreeView
    Protected Friend adjustmentsTV As New TreeView
    Protected Friend clientsTV As New TreeView
    Protected Friend clients_categoriesTV As New TreeView
    Protected Friend productsTV As New TreeView
    Protected Friend products_categoriesTV As New TreeView
    Private periodsCLB As New CheckedListBox
    Friend versionsTV As New TreeView
    Private DropMenu As New TableLayoutPanel
    Friend CurrenciesCLB As New CheckedListBox

    Private EntitiesFlag As Boolean
    Private CategoriesFlag As Boolean
    Private adjustments_flag As Boolean
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
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
    Protected Friend Const TOP_LEFT_CHART_POSITION As String = "tl"
    Protected Friend Const TOP_RIGHT_CHART_POSITION As String = "tr"
    Protected Friend Const BOTTOM_LEFT_CHART_POSITION As String = "bl"
    Protected Friend Const BOTTOM_RIGHT_CHART_POSITION As String = "br"


#End Region

#End Region


#Region "Initialization"

    Protected Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        AccountsTokenNamesDict = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_NAME_VARIABLE)
        AccountsKeysTabNbDict = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_TAB_VARIABLE)
        Controller = New ControllingUI2Controller(Me)
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
        InitializeChartsTab()
        CollapsePane1()
        InitializeTabMenus()

    End Sub

    Private Sub LoadTrees()

        Entity.LoadEntitiesTree(entitiesTV)
        Client.LoadClientsTree(clientsTV)
        Product.LoadProductsTree(productsTV)
        Account.LoadAccountsTree(accountsTV)
        Category.LoadCategoryCodeTV(entities_categoriesTV, ControllingUI2Controller.ENTITIES_CODE)
        Category.LoadCategoryCodeTV(clients_categoriesTV, ControllingUI2Controller.CLIENTS_CODE)
        Category.LoadCategoryCodeTV(products_categoriesTV, ControllingUI2Controller.PRODUCT_CODE)
        Adjustment.LoadAdjustmentsTree(adjustmentsTV)
        Version.LoadVersionsTree(versionsTV)

        TVSetup(entitiesTV, 0, EntitiesTVImageList)
        TVSetup(clientsTV, 0)
        TVSetup(productsTV, 0)
        TVSetup(adjustmentsTV, 0)
        TVSetup(versionsTV, 0, VersionsIL)
        TVSetup(entities_categoriesTV, 1, categoriesIL)
        TVSetup(clients_categoriesTV, 1, categoriesIL)
        TVSetup(products_categoriesTV, 1, categoriesIL)

        TreeViewsUtilities.set_TV_basics_icon_index(entitiesTV)
        LoadCurrencies()


        ' Add TVs events for categories clients / products !!
        AddHandler entitiesTV.NodeMouseDoubleClick, AddressOf EntitiesTV_NodeMouseDoubleClick
        AddHandler entitiesTV.KeyDown, AddressOf EntitiesTV_KeyDown
        AddHandler entitiesTV.AfterCheck, AddressOf entitiesTV_AfterCheck
        AddHandler entitiesTV.NodeMouseClick, AddressOf EntitiesTV_NodeMouseClick
        AddHandler entities_categoriesTV.AfterCheck, AddressOf Category_AfterCheck
        AddHandler CurrenciesCLB.ItemCheck, AddressOf currenciesCLB_ItemCheck
        AddHandler periodsCLB.ItemCheck, AddressOf periodsCLB_ItemCheck

        entitiesTV.ContextMenuStrip = entitiesRightClickMenu
        adjustmentsTV.ContextMenuStrip = AdjustmentsRCM
        periodsCLB.ContextMenuStrip = periodsRightClickMenu

        DisplayFirstTreeOnly()          ' TV Table Layout

    End Sub

    Private Sub TVSetup(ByRef TV As TreeView, _
                        ByRef row_index As Int32, _
                        Optional ByRef image_list As ImageList = Nothing)

        TVTableLayout.Controls.Add(entitiesTV, 0, row_index)
        TV.Dock = DockStyle.Fill
        TV.CheckBoxes = True
        If Not image_list Is Nothing Then TV.ImageList = image_list

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
        CurrenciesCLB.SelectedItem = MAIN_CURRENCY

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

    Private Sub InitializeChartsTab()



    End Sub

    Private Sub InitializeTabMenus()

        ' Display Menu Buttons Handlers
        AddHandler DisplayMenu.EntitiesBT.Click, AddressOf EntitiesMenuClick
        AddHandler DisplayMenu.EntitiesLabel.Click, AddressOf EntitiesMenuClick
        AddHandler DisplayMenu.CategoriesBT.Click, AddressOf CategoriesMenuClick
        AddHandler DisplayMenu.CategoriesLabel.Click, AddressOf CategoriesMenuClick
        AddHandler DisplayMenu.AdjustmentBT.Click, AddressOf AdjustmentsMenuClick
        AddHandler DisplayMenu.AdjustmentLabel.Click, AddressOf AdjustmentsMenuClick
        AddHandler DisplayMenu.CurrenciesBT.Click, AddressOf CurrenciesMenuClick
        AddHandler DisplayMenu.CurrenciesLabel.Click, AddressOf CurrenciesMenuClick
        AddHandler DisplayMenu.PeriodsBT.Click, AddressOf PeriodsMenuClick
        AddHandler DisplayMenu.PeriodsLabel.Click, AddressOf PeriodsMenuClick
        AddHandler DisplayMenu.VersionsBT.Click, AddressOf VersionsMenuClick
        AddHandler DisplayMenu.VersionsLabel.Click, AddressOf VersionsMenuClick

        ' Excel Menu Buttons Handlers
        AddHandler ExcelMenu.ConsoExportBT.Click, AddressOf SendCurrentEntity_Click
        AddHandler ExcelMenu.ConsoExportlabel.Click, AddressOf SendCurrentEntity_Click
        AddHandler ExcelMenu.DrillDownExportBT.Click, AddressOf DropDrillDown_Click
        AddHandler ExcelMenu.DrillDownExportlabel.Click, AddressOf DropDrillDown_Click

        ' Business Control Buttons Handlers
        AddHandler BusinessControlMenu.VersionsComparisonBT.Click, AddressOf AddVersionsComparisonToolStripMenuItem_Click
        AddHandler BusinessControlMenu.VersionsComparisonLabel.Click, AddressOf AddVersionsComparisonToolStripMenuItem_Click
        AddHandler BusinessControlMenu.SwitchVersionsBT.Click, AddressOf SwitchVersionsOrderToolStripMenuItem_Click
        AddHandler BusinessControlMenu.SwitchVersionsLabel.Click, AddressOf SwitchVersionsOrderToolStripMenuItem_Click
        AddHandler BusinessControlMenu.DeleteComparisonBT.Click, AddressOf RemoveVersionsComparisonToolStripMenuItem_Click
        AddHandler BusinessControlMenu.DeleteComparisonLabel.Click, AddressOf RemoveVersionsComparisonToolStripMenuItem_Click

        DisplayDisplayMenu()

    End Sub

#End Region


#Region "DGV Hierarchies Creation"

    Protected Friend Sub CreateDGVHierarchies(ByRef columns_hierarchy_nodes As TreeNode, _
                                              ByRef rows_display_node As TreeNode)

        rows_list_dic.Clear()
        columns_list_dic.Clear()
        For Each tab_account_node As TreeNode In accountsTV.Nodes

            Dim DGV As vDataGridView = TabControl1.TabPages.Item(tab_account_node.Index).Controls(0)
            DGV.RowsHierarchy.Clear()
            DGV.ColumnsHierarchy.Clear()
            Dim rows_list As New List(Of HierarchyItem)
            Dim columns_list As New List(Of HierarchyItem)
            rows_list_dic.Add(tab_account_node.Name, rows_list)
            columns_list_dic.Add(tab_account_node.Name, columns_list)

            ' Rows creation (Loop through accounts 1st (config))
            For Each account_node As TreeNode In TreeViewsUtilities.GetNodesList(tab_account_node)

                ' Create row
                Dim row As HierarchyItem = DGV.RowsHierarchy.Items.Add(account_node.Text)
                rows_list_dic(tab_account_node.Name).Add(row)

                ' Dive one Display level if any
                If Not rows_display_node.Nodes(0).NextNode Is Nothing Then
                    Select Case rows_display_node.Nodes(0).NextNode.Name
                        Case ControllingUI2Controller.ENTITIES_CODE : EntitiesRowsCreationLoopByRef(DGV, _
                                                                                                    Controller.Entity_node, _
                                                                                                    tab_account_node, _
                                                                                                    rows_display_node.Nodes(0).NextNode, _
                                                                                                    rows_list_dic(tab_account_node.Name), _
                                                                                                    row)
                        Case Else : RowsCreationLoopByRef(DGV, _
                                                          tab_account_node, _
                                                          rows_display_node.Nodes(0).NextNode, _
                                                          rows_list_dic(tab_account_node.Name), _
                                                          row)
                    End Select
                End If
            Next

            ' Columns Creation
            If columns_hierarchy_nodes.Nodes(0).Name = ControllingUI2Controller.YEARS_CODE Then
                PeriodsColumnsCreationLoop(DGV, columns_hierarchy_nodes.Nodes(0), columns_list)
            Else
                ColumnsCreationLoop(DGV, columns_hierarchy_nodes.Nodes(0), columns_list)
            End If
        Next

    End Sub

#Region "Columns"

    Private Sub ColumnsCreationLoop(ByRef DGV As vDataGridView, _
                                    ByRef column_node As TreeNode, _
                                    ByRef columns_list As List(Of HierarchyItem), _
                                    Optional ByRef parent_column As HierarchyItem = Nothing)

        For Each filter_value As String In Controller.categories_values_dict(column_node.Name).Keys

            ' Create Column
            Dim column As HierarchyItem
            If parent_column Is Nothing Then
                column = DGV.ColumnsHierarchy.Items.Add(Controller.categories_values_dict(column_node.Name)(filter_value))
            Else
                column = parent_column.Items.Add(Controller.categories_values_dict(column_node.Name)(filter_value))
            End If
            columns_list.Add(column)

            ' Dive one Column hierarchy Level if any
            If Not column_node.NextNode Is Nothing Then
                If column_node.NextNode.Name = ControllingUI2Controller.YEARS_CODE Then
                    PeriodsColumnsCreationLoop(DGV, column_node.NextNode, columns_list, column)
                Else
                    ColumnsCreationLoop(DGV, column_node.NextNode, columns_list, column)
                End If
            End If
        Next


    End Sub

    Private Sub PeriodsColumnsCreationLoop(ByRef DGV As vDataGridView, _
                                           ByVal column_node As TreeNode, _
                                           ByRef columns_list As List(Of HierarchyItem), _
                                           Optional ByRef parent_column As HierarchyItem = Nothing)

        Dim periods_node As TreeNode = column_node
        For Each year_node As TreeNode In periods_node.Nodes

            ' Create Year Column
            Dim year_column As HierarchyItem
            If parent_column Is Nothing Then
                year_column = DGV.ColumnsHierarchy.Items.Add(year_node.Text)
            Else
                year_column = parent_column.Items.Add(year_node.Text)
            End If
            columns_list.Add(year_column)

            If Not column_node.NextNode Is Nothing AndAlso column_node.NextNode.Name = ControllingUI2Controller.MONTHS_CODE Then column_node = column_node.NextNode

            If year_node.Nodes.Count > 0 Then

                ' Iterate through Months
                For Each month_node As TreeNode In year_node.Nodes
                    Dim month_column = year_column.Items.Add(month_node.Text)
                    columns_list.Add(month_column)
                    If Not column_node.NextNode Is Nothing Then ColumnsCreationLoop(DGV, column_node.NextNode, columns_list, month_column)
                Next
            Else
                If Not column_node.NextNode Is Nothing Then ColumnsCreationLoop(DGV, column_node.NextNode, columns_list, year_column)
            End If
        Next

    End Sub

#End Region

#Region "Rows"

    ' modifier entities loop
    ' + accounts = always first hierarchy

    Private Sub RowsCreationLoopByRef(ByRef DGV As vDataGridView, _
                                      ByRef tab_account_node As TreeNode, _
                                      ByRef display_node As TreeNode, _
                                      ByRef rows_list As List(Of HierarchyItem), _
                                      Optional ByRef parent_row As HierarchyItem = Nothing)

        ' Loop through the filter values of the category
        For Each filter_value As String In Controller.categories_values_dict(display_node.Name).Keys

            ' Create row
            Dim row As HierarchyItem = parent_row.Items.Add(Controller.categories_values_dict(display_node.Name)(filter_value))
            rows_list.Add(row)

            ' Dive one Display level if any
            If Not display_node.NextNode Is Nothing Then
                Select Case display_node.NextNode.Name
                    Case ControllingUI2Controller.ENTITIES_CODE : EntitiesRowsCreationLoopByRef(DGV, Controller.Entity_node, tab_account_node, display_node.NextNode, rows_list, row)
                    Case Else : RowsCreationLoopByRef(DGV, tab_account_node, display_node.NextNode, rows_list, row)
                End Select
            End If
        Next

    End Sub

    Private Sub EntitiesRowsCreationLoopByRef(ByRef DGV As vDataGridView, _
                                              ByRef entity_node As TreeNode, _
                                               ByRef tab_account_node As TreeNode, _
                                               ByRef display_node As TreeNode, _
                                               ByRef rows_list As List(Of HierarchyItem), _
                                               ByRef parent_row As HierarchyItem)

        If entity_node.Name = Controller.Entity_node.Name Then
            ' Dive one Display level if any
            If Not display_node.NextNode Is Nothing Then
                RowsCreationLoopByRef(DGV, tab_account_node, display_node.NextNode, rows_list, parent_row)
            End If
        End If

        For Each current_entity_node In entity_node.Nodes

            ' Create row
            Dim row As HierarchyItem = parent_row.Items.Add(current_entity_node.Text)
            rows_list.Add(row)

            ' Dive one Display level if any
            If Not display_node.NextNode Is Nothing Then
                RowsCreationLoopByRef(DGV, tab_account_node, display_node.NextNode, rows_list, row)
            End If

            ' Loop through children
            If current_entity_node.nodes.count > 0 Then
                EntitiesRowsCreationLoopByRef(DGV, current_entity_node, tab_account_node, display_node, rows_list, row)
            End If
        Next

    End Sub

#End Region


#End Region


#Region "DGV Fill Functions"

    Protected Friend Sub DisplayData(ByRef DataDictionary As Dictionary(Of Object, Object), _
                                     ByRef columns_hirerarchy_nodes As TreeNode, _
                                     ByRef rows_display_hierarchy As TreeNode)

        For Each tab_account_node As TreeNode In accountsTV.Nodes
            ' debug to be deleted
            '  System.Diagnostics.Debug.Write(Chr(13) & "tab: " & tab_account_node.Text)
            Dim DGV As vDataGridView = TabControl1.TabPages.Item(tab_account_node.Index).Controls(0)
            Dim accounts_list As List(Of String) = TreeViewsUtilities.GetNodesKeysList(tab_account_node)
            accounts_list.Remove(tab_account_node.Name)

            row_index = 0
            For Each account_id As String In accounts_list
                ' debug to be deleted
                'System.Diagnostics.Debug.Write(Chr(13) & "account id: " & account_id)

                ' --------------------------------------------------
                ' stub for perf test -> "T" accounts ignored !!!!!
                ' --------------------------------------------------
                If DataDictionary.ContainsKey(account_id) Then
                    RowsFillingLoop(rows_display_hierarchy.Nodes(0), _
                                    columns_hirerarchy_nodes, _
                                    DataDictionary(account_id), _
                                    rows_list_dic(tab_account_node.Name), _
                                    columns_list_dic(tab_account_node.Name))
                End If
            Next
        Next
        FormatVIEWDataDisplay()
        InitializeMenuPeriodsCB()

    End Sub

    ' Si trop lent faire un test avec DD en IVAR !!
    Private Sub RowsFillingLoop(ByRef rows_display_node As TreeNode, _
                                ByRef columns_display_node As TreeNode, _
                                ByRef DataDictionary As Dictionary(Of Object, Object), _
                                ByRef rows_list As List(Of HierarchyItem), _
                                ByRef column_list As List(Of HierarchyItem))

        If rows_display_node.NextNode Is Nothing Then
            ' debug to be deleted
            ' System.Diagnostics.Debug.Write(Chr(13) & "Columns Loop; row= " & current_row.Caption & " display node: " & rows_display_node.Name)

            ColumnsLoop(columns_display_node.Nodes(0), _
                        DataDictionary, _
                        rows_list(row_index), _
                        column_list)
            row_index = row_index + 1
        Else
            For Each key In DataDictionary.Keys.ToList
                RowsFillingLoop(rows_display_node.NextNode, _
                                columns_display_node, _
                                DataDictionary(key), _
                                rows_list, _
                                column_list)
            Next
        End If

    End Sub

    Private Sub ColumnsLoop(ByRef column_display_node As TreeNode, _
                            ByRef DataDictionary As Dictionary(Of Object, Object), _
                            ByRef row As HierarchyItem, _
                            ByRef column_list As List(Of HierarchyItem))

        column_index = 0
        If column_display_node.NextNode Is Nothing Then
            For Each key In DataDictionary.Keys.ToList
                row.DataGridView.CellsArea.SetCellValue(row, current_column, DataDictionary(key))
                current_column = column_list(column_index)
                column_index = column_index + 1
            Next
        Else
            For Each key As String In DataDictionary.Keys.ToList
                ColumnsLoop(column_display_node.NextNode, _
                            DataDictionary(key), _
                            row, _
                            column_list)
            Next
        End If

    End Sub


#Region "Formatting"

    Private Sub FormatVIEWDataDisplay()

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

#End Region


#Region "Events"

#Region "Entities TreeView"

    Private Sub EntitiesTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)


    End Sub

    Private Sub EntitiesTV_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            If Not entitiesTV.SelectedNode Is Nothing Then Controller.STUBCompute(entitiesTV.SelectedNode)
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

    Private Sub Category_AfterCheck(sender As Object, e As TreeViewEventArgs)

        If IsUpdatingChildrenCategory = False Then
            If e.Node.Parent Is Nothing Then
                Dim state As Boolean = e.Node.Checked
                IsUpdatingChildrenCategory = True
                For Each child_node As TreeNode In e.Node.Nodes
                    child_node.Checked = state
                Next
                IsUpdatingChildrenCategory = False
            End If
            Controller.EntitiesCategoriesUpdate()
        End If

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

        DROPTOEXCELController.SendTabToExcel(TabControl1.SelectedTab.Controls(0), _
                                             VersionTB.Text, _
                                            CurrencyTB.Text)

    End Sub

    Private Sub DGV_CellMouseClick(sender As Object, e As CellMouseEventArgs)

        current_DGV_cell = e.Cell

    End Sub


#End Region


#Region "Main Menu Calls Backs"

#Region "Buttons"

    Private Sub SelectionBT_Click(sender As Object, e As EventArgs) Handles SelectionMBT.Click

        DisplayDisplayMenu()

    End Sub

    Private Sub ExcelBT_Click(sender As Object, e As EventArgs) Handles ExcelMBT.Click

        DisplayExcelMenu()

    End Sub

    Private Sub ControllingBT_Click(sender As Object, e As EventArgs) Handles BusinessControlMBT.Click

        DisplayBusinessControlMenu()

    End Sub

    Private Sub RefreshBT_Click(sender As Object, e As EventArgs) Handles RefreshBT.Click

        Refresh_Click(sender, e)

    End Sub

#End Region

#Region "Main Menus"

    Private Sub EntitiesMenuClick(sender As Object, e As EventArgs)

        If EntitiesFlag = False Then
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
            If TVTableLayout.GetRow(entitiesTV) = 0 Then
                CollapsePane1()
            Else
                DisplayFirstTreeOnly()
            End If
            entitiesTV.Visible = False
            EntitiesFlag = False
        End If

    End Sub

    Private Sub CategoriesMenuClick(sender As Object, e As EventArgs)

        If CategoriesFlag = False Then
            ExpandPane1()
            HideAllMenusItemExceptEntities()
            entities_categoriesTV.Visible = True
            CategoriesFlag = True
            If EntitiesFlag = True Then
                DisplayTwoTrees()
                TVTableLayout.SetRow(entities_categoriesTV, 1)
            Else
                TVTableLayout.SetRow(entities_categoriesTV, 0)
            End If
        Else
            If TVTableLayout.GetRow(entities_categoriesTV) = 0 Then
                CollapsePane1()
            Else
                DisplayFirstTreeOnly()
            End If
            entities_categoriesTV.Visible = False
            CategoriesFlag = False
        End If

    End Sub

    Private Sub AdjustmentsMenuClick(sender As Object, e As EventArgs)

        If adjustments_flag = False Then
            ExpandPane1()
            HideAllMenuItems()
            adjustmentsTV.Visible = True
            adjustments_flag = True
            '          DisplayTwoTrees()
        Else
            CollapsePane1()
            adjustmentsTV.Visible = False
            adjustments_flag = False
            '     DisplayFirstTreeOnly()
        End If

    End Sub

    Private Sub CurrenciesMenuClick(sender As Object, e As EventArgs)

        If CurrenciesFlag = False Then
            ExpandPane1()
            HideAllMenuItems()
            CurrenciesCLB.Visible = True
            CurrenciesFlag = True
            DisplayTwoTrees()

        Else
            CollapsePane1()
            CurrenciesCLB.Visible = False
            CurrenciesFlag = False
            DisplayFirstTreeOnly()
        End If

    End Sub

    Private Sub PeriodsMenuClick(sender As Object, e As EventArgs)

        If PeriodsFlag = False Then
            ExpandPane1()
            HideAllMenuItems()
            periodsCLB.Visible = True
            PeriodsFlag = True
        Else
            CollapsePane1()
            periodsCLB.Visible = False
            PeriodsFlag = False
        End If

    End Sub

    Private Sub VersionsMenuClick(sender As Object, e As EventArgs)

        If VersionsFlag = False Then
            ExpandPane1()
            HideAllMenuItems()
            versionsTV.Visible = True
            VersionsFlag = True
        Else
            CollapsePane1()
            versionsTV.Visible = False
            VersionsFlag = False
        End If

    End Sub

    Private Sub SendCurrentEntity_Click(sender As Object, e As EventArgs)

        DROPTOEXCELController.SendCurrentEntityToExcel(VersionTB.Text, _
                                                       CurrencyTB.Text)

    End Sub

    Private Sub DropDrillDown_Click(sender As Object, e As EventArgs)

        ' Maybe issue if nothing in the DGV ? 
        DROPTOEXCELController.SendDrillDownToExcel(VersionTB.Text, _
                                                   CurrencyTB.Text)

    End Sub

    Private Sub Refresh_Click(sender As Object, e As EventArgs)

        If Not Controller.Entity_node Is Nothing Then
            Controller.STUBCompute(entitiesTV.Nodes.Find(Controller.Entity_node.Name, True)(0))
        ElseIf Not entitiesTV.SelectedNode Is Nothing Then
            Controller.STUBCompute(entitiesTV.SelectedNode)
        Else
            MsgBox("An Entity level must be selected in order to refresh " + Chr(13) + Chr(13) + _
                   " Please select an entity")
        End If

    End Sub

    Private Sub AddVersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs)

        If isVersionComparisonDisplayed = True Then
            MsgBox("The Versions Comparison is already displayed")
        Else

            ' to be reimplemented -> launch request to controller
            ' dependant on version position in columns hierarchy

            'If Controller.versions_id_array.Length = 2 Then
            '    For Each tab_ As TabPage In TabControl1.TabPages
            '        Dim DGV As vDataGridView = tab_.Controls(0)
            '        DGVUTIL.AddVersionComparison(DGV)
            '    Next
            '    TabControl1.TabPages(0).Select()
            '    isVersionComparisonDisplayed = True
            'Else
            '    MsgBox("Two versions must be selected in order to display the comparison." + Chr(13) + _
            '           "Refresh must be applied after versions selection.")
            'End If
        End If

    End Sub

    Private Sub SwitchVersionsOrderToolStripMenuItem_Click(sender As Object, e As EventArgs)

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

    Private Sub RemoveVersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs)

        If isVersionComparisonDisplayed Then
            For Each tab_ As TabPage In TabControl1.TabPages
                Dim DGV As vDataGridView = tab_.Controls(0)
                DGVUTIL.RemoveVersionsComparison(DGV)
            Next
            isVersionComparisonDisplayed = False
        End If

    End Sub

#End Region

#Region "Menus Ribbon Display"

    Private Sub DisplayDisplayMenu()

        UncheckAllButtons()
        SelectionMBT.Checked = CheckState.Checked
        RibbonsPanel.Controls.Clear()
        RibbonsPanel.Controls.Add(DisplayMenu)
        DisplayMenu.Margin = New Padding(0, 0, 0, 0)

    End Sub

    Private Sub DisplayExcelMenu()

        UncheckAllButtons()
        ExcelMBT.Checked = CheckState.Checked
        RibbonsPanel.Controls.Clear()
        RibbonsPanel.Controls.Add(ExcelMenu)
        ExcelMenu.Margin = New Padding(0, 0, 0, 0)

    End Sub

    Private Sub DisplayBusinessControlMenu()

        UncheckAllButtons()
        BusinessControlMBT.Checked = CheckState.Checked
        RibbonsPanel.Controls.Clear()
        RibbonsPanel.Controls.Add(BusinessControlMenu)
        BusinessControlMenu.Margin = New Padding(0, 0, 0, 0)

    End Sub

    Private Sub UncheckAllButtons()

        SelectionMBT.Checked = CheckState.Unchecked
        BusinessControlMBT.Checked = CheckState.Unchecked
        ExcelMBT.Checked = CheckState.Unchecked

    End Sub

#End Region

#End Region


#Region "Right Click Menus"

#Region "EntitiesTV Right Click Menu Call Backs"

    Private Sub compute_complete_Click(sender As Object, e As EventArgs) Handles compute_complete.Click

        Controller.STUBCompute(right_clicked_node)

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

    Private Sub FormatsRCMBT_Click(sender As Object, e As EventArgs) Handles FormatsRCMBT.Click

        Dim formatsController As New FormatsController

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

        For Each node As TreeNode In adjustmentsTV.Nodes
            node.Checked = state
        Next

    End Sub


#End Region

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
        entities_categoriesTV.Visible = False
        CategoriesFlag = False
        adjustmentsTV.Visible = False
        adjustments_flag = False

    End Sub

    Private Sub HideAllMenuItemsExceptCategories()

        HideAllMenusItemsCore()
        entities_categoriesTV.Visible = False
        CategoriesFlag = False

    End Sub

    Private Sub HideAllMenusItemExceptEntities()

        HideAllMenusItemsCore()
        entitiesTV.Visible = False
        EntitiesFlag = False

    End Sub

    Private Sub HideAllMenusItemsCore()

        DisplayFirstTreeOnly()
        adjustmentsTV.Visible = False
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
