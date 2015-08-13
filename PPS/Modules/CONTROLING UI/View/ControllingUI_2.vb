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
' Last modified: 13/08/2015


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
Imports PPSFBIControls
Imports System.ComponentModel
Imports VIBlend.WinForms.Controls


Friend Class ControllingUI_2


#Region "Instance Variables"

#Region "Objects"

    Private Controller As FinancialUIController
    Friend DGVUTIL As New DataGridViewsUtil
    '  Friend PBar As New ProgressBarControl
    Friend display_control As DisplayControl
    Private leftSplitContainer As SplitContainer
    Private rightSplitContainer As SplitContainer
    Private Accounts As New Account
    Friend CP As CircularProgressUI

#End Region

#Region "Variables"

    Private right_clicked_node As TreeNode
    Private current_DGV_cell As GridCell
    Private rows_list_dic As New Dictionary(Of String, List(Of HierarchyItem))
    Private columns_list_dic As New Dictionary(Of String, List(Of HierarchyItem))
    Private row_index As Int32
    Private column_index As Int32


#End Region

#Region "Treeviews"

    Friend accountsTV As New TreeView
    Friend entitiesTV As New TreeView
    Friend entitiesFiltersTV As New TreeView
    Friend adjustmentsTV As New TreeView
    Friend adjustmentsFiltersTV As New TreeView
    Friend clientsTV As New TreeView
    Friend clientsFiltersTV As New TreeView
    Friend productsTV As New TreeView
    Friend productsFiltersTV As New TreeView
    Private periodsCLB As New CheckedListBox
    Friend versionsTV As New TreeView
    Friend CurrenciesCLB As New CheckedListBox

    Private SP1Distance As Single = 230
    Private SP2Distance As Single = 900

#End Region

#Region "Flags"

    Private isUpdatingPeriodsCheckList As Boolean
    Private isVersionComparisonDisplayed As Boolean
    Private IsUpdatingChildrenCategory As Boolean

    Private EntitiesFlag As Boolean
    Private EntitiesCategoriesFlag As Boolean
    Private ClientsFlag As Boolean
    Private ClientsCategoriesFlag As Boolean
    Private productsFlag As Boolean
    Private productsCategoriesFlag As Boolean

    Private adjustments_flag As Boolean
    Private CurrenciesFlag As Boolean
    Private VersionsFlag As Boolean
    Private PeriodsFlag As Boolean
    Private displayControlFlag As Boolean = True

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
        Controller = New FinancialUIController(Me)
        LoadTrees()

        periodsCLB.Dock = DockStyle.Fill
        periodsCLB.CheckOnClick = True

        ' Init TabControl
        For Each node As TreeNode In accountsTV.Nodes
            Dim newTab As New vTabPage
            newTab.Text = node.Text
            newTab.Name = node.Name
            tabControl1.TabPages.Add(newTab)
        Next
        InitializeChartsTab()
        DimensionsDisplayPaneSetup()
        TVTableLayout.Controls.Add(periodsCLB, 0, 1)
        HideAllMenusItems()
        CollapseSP1Pane1()
        DisplayFirstTreeOnly()          ' TV Table Layout

        If versionsTV.Nodes.Find(My.Settings.version_id, True).Length > 0 Then
            versionsTV.Nodes.Find(My.Settings.version_id, True)(0).Checked = True
        End If

    End Sub

    Private Sub LoadTrees()

        ' controller actions !!!!!! priority high

        GlobalVariables.Entities.LoadEntitiesTV(entitiesTV)
        GlobalVariables.Clients.LoadClientsTree(clientsTV)
        GlobalVariables.Products.LoadProductsTree(productsTV)
        GlobalVariables.Accounts.LoadAccountsTV(accountsTV)
        GlobalVariables.Adjustments.LoadAdjustmentsTree(adjustmentsTV)

        AxisFilter.LoadFvTv(entitiesFiltersTV, GlobalEnums.AnalysisAxis.ENTITIES)
        AxisFilter.LoadFvTv(clientsFiltersTV, GlobalEnums.AnalysisAxis.CLIENTS)
        AxisFilter.LoadFvTv(productsFiltersTV, GlobalEnums.AnalysisAxis.PRODUCTS)
        AxisFilter.LoadFvTv(adjustmentsFiltersTV, GlobalEnums.AnalysisAxis.ADJUSTMENTS)

        GlobalVariables.Versions.LoadVersionsTV(versionsTV)

        TVSetup(entitiesTV, 1, EntitiesTVImageList)
        TVSetup(clientsTV, 1)
        TVSetup(productsTV, 1)
        TVSetup(adjustmentsTV, 1)
        TVSetup(versionsTV, 1, VersionsIL)
        TVSetup(entitiesFiltersTV, 2, CategoriesIL)
        TVSetup(clientsFiltersTV, 2, CategoriesIL)
        TVSetup(productsFiltersTV, 2, CategoriesIL)

        TreeViewsUtilities.set_TV_basics_icon_index(entitiesTV)
        LoadCurrencies()

        ' Add TVs events for categories clients / products !!
        AddHandler entitiesTV.NodeMouseDoubleClick, AddressOf EntitiesTV_NodeMouseDoubleClick
        AddHandler entitiesTV.KeyDown, AddressOf EntitiesTV_KeyDown
        AddHandler entitiesTV.AfterCheck, AddressOf entitiesTV_AfterCheck
        AddHandler entitiesTV.NodeMouseClick, AddressOf EntitiesTV_NodeMouseClick
        AddHandler entitiesFiltersTV.AfterCheck, AddressOf entitiesCategory_AfterCheck
        AddHandler clientsFiltersTV.AfterCheck, AddressOf clientsCategory_AfterCheck
        AddHandler productsFiltersTV.AfterCheck, AddressOf productsCategory_AfterCheck
        AddHandler CurrenciesCLB.ItemCheck, AddressOf currenciesCLB_ItemCheck
        AddHandler periodsCLB.ItemCheck, AddressOf periodsCLB_ItemCheck

        entitiesTV.ContextMenuStrip = entitiesRightClickMenu
        adjustmentsTV.ContextMenuStrip = AdjustmentsRCM
        periodsCLB.ContextMenuStrip = periodsRightClickMenu

    End Sub

    Private Sub TVSetup(ByRef TV As TreeView, _
                        ByRef row_index As Int32, _
                        Optional ByRef image_list As ImageList = Nothing)

        TVTableLayout.Controls.Add(TV, 0, row_index)
        TV.Dock = DockStyle.Fill
        TV.CheckBoxes = True
        If Not image_list Is Nothing Then TV.ImageList = image_list

    End Sub

    Private Sub LoadCurrencies()

        TVTableLayout.Controls.Add(CurrenciesCLB, 0, 1)
        CurrenciesCLB.Dock = DockStyle.Fill
        CurrenciesCLB.CheckOnClick = True
        CurrenciesCLB.SelectionMode = SelectionMode.One

        Dim currenciesList As New List(Of UInt32)
        ' STUB !!!!!!!!!!
        ' GlobalVariables.Currencies.currencies_hash.Keys
        currenciesList.Add(1) '
        currenciesList.Add(2)
        currenciesList.Add(3)
        ' -------------------------- priotiy high !!!!!!!

        For Each currency_ As String In currenciesList
            CurrenciesCLB.Items.Add(currency_, False)
        Next
        CurrenciesCLB.SetItemChecked(CurrenciesCLB.FindString(My.Settings.mainCurrency), True)
        CurrenciesCLB.SelectedItem = My.Settings.mainCurrency

    End Sub

    Private Sub DataMiningUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Me.Controls.Add(PBar)                           ' Progress Bar
        'PBar.Left = (Me.Width - PBar.Width) / 2         ' Progress Bar
        'PBar.Top = (Me.Height - PBar.Height) / 2        ' Progress Bar

        For Each tab_ As vTabPage In tabControl1.TabPages

            tabControl1.SelectedTab = tab_
            tab_.ImageIndex = 0

            Dim DGV As New vDataGridView
            DataGridViewsUtil.InitDisplayVDataGridView(DGV, DGV_THEME)
            DGV.Name = tab_.Name
            DGV.Dock = DockStyle.Fill
            DGV.Left = INNER_MARGIN
            DGV.Top = INNER_MARGIN
            DGV.BackColor = SystemColors.Control
            DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
            DGV.ContextMenuStrip = DGVsRCM
            AddHandler DGV.CellMouseClick, AddressOf DGV_CellMouseClick
            AddHandler DGV.HierarchyItemExpanded, AddressOf Controller.DGV_Hierarchy_Expanded
            'AddHandler DGV.HierarchyItemCollapsed
            tab_.Controls.Add(DGV)

        Next

        If Not IsNothing(TabControl1.TabPages(0)) Then
            TabControl1.SelectedTab = TabControl1.TabPages(0)
        End If
        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub InitializeChartsTab()




    End Sub

    Private Sub DimensionsDisplayPaneSetup()

        ' This initialization should go into controller ?!!
        ' priority high
        Dim analysis_axis_tv As New vTreeView
        VTreeViewUtil.InitTVFormat(analysis_axis_tv)
         VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS, ACCOUNTS_CODE, analysis_axis_tv)

        ' Entities Analysis Axis and Categories Nodes
        Dim entities_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES, _
                                                               ENTITIES_CODE, _
                                                               analysis_axis_tv)

        For Each entity_node As TreeNode In entitiesFiltersTV.Nodes
            VTreeViewUtil.AddNode(Computer.FILTERS_DECOMPOSITION_IDENTIFIER & entity_node.Name, _
                                  entity_node.Text, _
                                  entities_node)
        Next

        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, YEARS_CODE, analysis_axis_tv)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS, MONTHS_CODE, analysis_axis_tv)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS, VERSIONS_CODE, analysis_axis_tv)

        ' Clients Analysis Axis and Categories Nodes
        Dim clientsNode As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS, CLIENTS_CODE, analysis_axis_tv)
        For Each client_category_node As TreeNode In clientsFiltersTV.Nodes
            VTreeViewUtil.AddNode(Computer.FILTERS_DECOMPOSITION_IDENTIFIER & client_category_node.Name, _
                                  client_category_node.Text, _
                                  clientsNode)
          Next

        ' Products Analysis Axis and Categories Nodes
        Dim products_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.PRODUCTS,
                                                               PRODUCTS_CODE, _
                                                               analysis_axis_tv)

        For Each product_category_node As TreeNode In productsFiltersTV.Nodes
            VTreeViewUtil.AddNode(Computer.FILTERS_DECOMPOSITION_IDENTIFIER & product_category_node.Name, _
                                  product_category_node.Text, _
                                  products_node)
        Next

        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER _
                              & GlobalEnums.AnalysisAxis.ADJUSTMENTS, _
                              ADJUSTMENT_CODE, analysis_axis_tv)
      
        display_control = New DisplayControl(analysis_axis_tv)
        SplitContainer2.Panel2.Controls.Add(display_control)
        display_control.Dock = DockStyle.Fill

        AddHandler display_control.RefreshOrder, AddressOf RefreshFromRightPane
        AddHandler display_control.HideControl, AddressOf HideRightPane

    End Sub


#End Region


#Region "Interface"

    Private Sub RefreshData(ByRef entityNode As TreeNode, _
                            Optional ByRef useCache As Boolean = False)

        ' check that 1 version is selected at least 

        ' + attention => it seems that we can select folder versions !! 
        ' priority high !!! 

        Controller.EntityNode = entityNode ' inverser to be put in controller  !! priority normal 
        ' Computing
        Controller.Compute()

    End Sub


#End Region


#Region "Formatting"

    Private Sub FormatVIEWDataDisplay()

        ' to be reimplemented priority high !!!!!!

        'DGVUTIL.FormatDGVs(TabControl1, CurrencyTB.Text)
        'If Not IsNothing(TabControl1.TabPages(0)) Then TabControl1.SelectedTab = TabControl1.TabPages(0)
        'Me.Update()

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


#Region "Events"

#Region "Entities TreeView"

    Private Sub EntitiesTV_NodeMouseDoubleClick(sender As Object, e As TreeNodeMouseClickEventArgs)


    End Sub

    Private Sub EntitiesTV_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            If Not entitiesTV.SelectedNode Is Nothing Then RefreshData(entitiesTV.SelectedNode)
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

    Private Sub entitiesCategory_AfterCheck(sender As Object, e As TreeViewEventArgs)

        If IsUpdatingChildrenCategory = False Then
            updateChildrenCheckedState(e.Node)
            Controller.EntitiesCategoriesUpdate()
        End If

    End Sub

    Private Sub clientsCategory_AfterCheck(sender As Object, e As TreeViewEventArgs)

        If IsUpdatingChildrenCategory = False Then
            updateChildrenCheckedState(e.Node)
            Controller.ClientsCategoriesUpdate()
        End If

    End Sub

    Private Sub productsCategory_AfterCheck(sender As Object, e As TreeViewEventArgs)

        If IsUpdatingChildrenCategory = False Then
            updateChildrenCheckedState(e.Node)
            Controller.ProductsCategoriesUpdate()
        End If

    End Sub

    Private Sub updateChildrenCheckedState(ByRef node As TreeNode)

        If node.Parent Is Nothing Then
            Dim state As Boolean = node.Checked
            IsUpdatingChildrenCategory = True
            For Each child_node As TreeNode In node.Nodes
                child_node.Checked = state
            Next
            IsUpdatingChildrenCategory = False
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

    Private Sub DGV_CellMouseClick(sender As Object, e As CellMouseEventArgs)

        current_DGV_cell = e.Cell

    End Sub

    'Private Sub displayControlDisplayTVs_KeyDown(sender As Object, e As KeyEventArgs)

    '    Select Case e.KeyCode
    '        Case Keys.Delete : sender.SelectedNode.Remove()

    '        Case Keys.Up
    '            If e.Control Then
    '                TreeViewsUtilities.MoveNodeUp(sender.SelectedNode)
    '            End If
    '        Case Keys.Down
    '            If e.Control Then
    '                TreeViewsUtilities.MoveNodeDown(sender.SelectedNode)
    '            End If
    '    End Select

    'End Sub

#End Region


#Region "Menus and Display Management"

#Region "Main Menu Calls Backs"

#Region "Home Menu"

    Private Sub EntitiesMenuBTClick(sender As Object, e As EventArgs) Handles EntitiesMenuBT1.Click, SelectionToolStripMenuItem.Click

        If EntitiesFlag = False Then
            entitiesTV.Select()
            ExpandSP1Pane1()
            Dim show_entities_categories As Boolean = EntitiesCategoriesFlag
            HideAllMenusItems()

            entitiesTV.Visible = True
            EntitiesFlag = True

            If show_entities_categories = True Then
                entitiesFiltersTV.Visible = True
                EntitiesCategoriesFlag = True
                DisplayTwoTrees()
                TVTableLayout.SetRow(entitiesTV, 2)
            Else
                TVTableLayout.SetRow(entitiesTV, 1)
            End If
        Else
            If TVTableLayout.GetRow(entitiesTV) = 1 Then
                CollapseSP1Pane1()
            Else
                DisplayFirstTreeOnly()
            End If
            entitiesTV.Visible = False
            EntitiesFlag = False
        End If

    End Sub

    Private Sub DisplayMenuBTClick(sender As Object, e As EventArgs) Handles DisplayBT.Click, displayLabel.Click, _
                                                                             DisplayBT2.Click, DisplayLabel2.Click, _
                                                                             DisplayBT3.Click, DisplayLabel3.Click

        If displayControlFlag = False Then
            ExpandSP2Pane2()
            displayControlFlag = True
        Else
            CollapseSP2Pane2()
            displayControlFlag = False
        End If

    End Sub

#Region "Selection"

    Private Sub entitiesCategoriesMenuClick(sender As Object, e As EventArgs) Handles entitiesCategoriesMenuBT.Click

        If EntitiesCategoriesFlag = False Then
            ExpandSP1Pane1()
            Dim show_entities As Boolean = EntitiesFlag
            HideAllMenusItems()
            entitiesFiltersTV.Visible = True
            EntitiesCategoriesFlag = True

            If show_entities = True Then
                entitiesTV.Visible = True
                EntitiesFlag = True
                DisplayTwoTrees()
                TVTableLayout.SetRow(entitiesFiltersTV, 2)
            Else
                TVTableLayout.SetRow(entitiesFiltersTV, 1)
            End If
        Else
            If TVTableLayout.GetRow(entitiesFiltersTV) = 1 Then
                CollapseSP1Pane1()
            Else
                DisplayFirstTreeOnly()
            End If
            entitiesFiltersTV.Visible = False
            EntitiesCategoriesFlag = False
        End If

    End Sub

    Private Sub ClientsMenuBT_Click(sender As Object, e As EventArgs) Handles ClientsSelectionToolStripMenuItem.Click

        If ClientsFlag = False Then
            clientsTV.Select()
            ExpandSP1Pane1()
            Dim show_clients_categories As Boolean = ClientsCategoriesFlag
            HideAllMenusItems()

            clientsTV.Visible = True
            ClientsFlag = True

            If show_clients_categories = True Then
                clientsFiltersTV.Visible = True
                ClientsCategoriesFlag = True
                DisplayTwoTrees()
                TVTableLayout.SetRow(clientsTV, 2)
            Else
                TVTableLayout.SetRow(clientsTV, 1)
            End If
        Else
            If TVTableLayout.GetRow(clientsTV) = 1 Then
                CollapseSP1Pane1()
            Else
                DisplayFirstTreeOnly()
            End If
            clientsTV.Visible = False
            ClientsFlag = False
        End If

    End Sub

    Private Sub clientsCategoriesMenuBT_Click(sender As Object, e As EventArgs) Handles clientsCategoriesMenuBT.Click

        If ClientsCategoriesFlag = False Then
            ExpandSP1Pane1()
            Dim show_clients As Boolean = ClientsFlag
            HideAllMenusItems()
            clientsFiltersTV.Visible = True
            ClientsCategoriesFlag = True

            If show_clients = True Then
                clientsTV.Visible = True
                ClientsFlag = True
                DisplayTwoTrees()
                TVTableLayout.SetRow(clientsFiltersTV, 2)
            Else
                TVTableLayout.SetRow(clientsFiltersTV, 1)
            End If
        Else
            If TVTableLayout.GetRow(clientsFiltersTV) = 1 Then
                CollapseSP1Pane1()
            Else
                DisplayFirstTreeOnly()
            End If
            clientsFiltersTV.Visible = False
            ClientsCategoriesFlag = False
        End If

    End Sub

    Private Sub ProductsMenuBT_Click(sender As Object, e As EventArgs) Handles ProductsFilterToolStripMenuItem.Click

        If productsFlag = False Then
            productsTV.Select()
            ExpandSP1Pane1()
            Dim show_products_categories As Boolean = productsCategoriesFlag
            HideAllMenusItems()

            productsTV.Visible = True
            productsFlag = True

            If show_products_categories = True Then
                productsFiltersTV.Visible = True
                productsCategoriesFlag = True
                DisplayTwoTrees()
                TVTableLayout.SetRow(productsTV, 2)
            Else
                TVTableLayout.SetRow(productsTV, 1)
            End If
        Else
            If TVTableLayout.GetRow(productsTV) = 1 Then
                CollapseSP1Pane1()
            Else
                DisplayFirstTreeOnly()
            End If
            productsTV.Visible = False
            productsFlag = False
        End If

    End Sub

    Private Sub productsCategoriesMenuBT_Click(sender As Object, e As EventArgs) Handles productsCategoriesMenuBT.Click

        If productsCategoriesFlag = False Then
            ExpandSP1Pane1()
            Dim show_products As Boolean = productsFlag
            HideAllMenusItems()
            productsFiltersTV.Visible = True
            productsCategoriesFlag = True

            If show_products = True Then
                productsTV.Visible = True
                productsFlag = True
                DisplayTwoTrees()
                TVTableLayout.SetRow(productsFiltersTV, 2)
            Else
                TVTableLayout.SetRow(productsFiltersTV, 1)
            End If
        Else
            If TVTableLayout.GetRow(productsFiltersTV) = 1 Then
                CollapseSP1Pane1()
            Else
                DisplayFirstTreeOnly()
            End If
            productsFiltersTV.Visible = False
            productsCategoriesFlag = False
        End If

    End Sub

    Private Sub AdjustmentsMenuBT_Click(sender As Object, e As EventArgs) Handles AdjustmentsMenuBT.Click

        If adjustments_flag = False Then
            ExpandSP1Pane1()
            HideAllMenusItems()
            adjustmentsTV.Visible = True
            adjustments_flag = True
        Else
            CollapseSP1Pane1()
            adjustmentsTV.Visible = False
            adjustments_flag = False
        End If

    End Sub

    Private Sub CurrenciesMenuClick(sender As Object, e As EventArgs) Handles CurrenciesMenuBT.Click

        If CurrenciesFlag = False Then
            ExpandSP1Pane1()
            HideAllMenusItems()
            CurrenciesCLB.Visible = True
            CurrenciesFlag = True
            DisplayTwoTrees()

        Else
            CollapseSP1Pane1()
            CurrenciesCLB.Visible = False
            CurrenciesFlag = False
            DisplayFirstTreeOnly()
        End If

    End Sub

    Private Sub PeriodsMenuClick(sender As Object, e As EventArgs) Handles PeriodsMenuBT.Click

        If PeriodsFlag = False Then
            ExpandSP1Pane1()
            HideAllMenusItems()
            periodsCLB.Visible = True
            PeriodsFlag = True
        Else
            CollapseSP1Pane1()
            periodsCLB.Visible = False
            PeriodsFlag = False
        End If

    End Sub

    Private Sub VersionsMenuClick(sender As Object, e As EventArgs) Handles VersionsMenuBT.Click

        If VersionsFlag = False Then
            ExpandSP1Pane1()
            HideAllMenusItems()
            versionsTV.Visible = True
            VersionsFlag = True
        Else
            CollapseSP1Pane1()
            versionsTV.Visible = False
            VersionsFlag = False
        End If

    End Sub

#End Region

    Private Sub Refresh_Click(sender As Object, e As EventArgs) Handles RefreshMenuBT.Click, RefreshMenuBT2.Click

        If Not entitiesTV.SelectedNode Is Nothing Then
            RefreshData(entitiesTV.SelectedNode)
        Else
            MsgBox("An Entity level must be selected in order to refresh " + Chr(13) + Chr(13) + _
                   " Please select an entity")
        End If

    End Sub

#End Region

#Region "Business Control"

    Private Sub AddVersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles versionComparisonBT.Click

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

    Private Sub SwitchVersionsOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles switchVersionsBT.Click

        Controller.ReverseVersionsComparison()

    End Sub

    Private Sub RemoveVersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles deleteComparisonBT.Click

        If isVersionComparisonDisplayed Then
            For Each tab_ As vTabPage In tabControl1.TabPages
                Dim DGV As vDataGridView = tab_.Controls(0)
                DGVUTIL.RemoveVersionsComparison(DGV)
            Next
            isVersionComparisonDisplayed = False
        End If

    End Sub

#End Region

#Region "Excel"

    Private Sub DropDrillDown_Click(sender As Object, e As EventArgs) Handles SendBreakDownBT.Click

        Controller.dropOnExcel()

    End Sub

#End Region

#End Region


#Region "Right Click Menus"

    Private Sub compute_complete_Click(sender As Object, e As EventArgs) Handles compute_complete.Click

        RefreshData(right_clicked_node)

    End Sub

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

        For Each tab_ As vTabPage In tabControl1.TabPages
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

    Private Sub ControllingUI_2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing


    End Sub

#End Region


#Region "Left Pane Expansion/ Collapse"

    Private Sub CollapseSP2Pane2()

        SP2Distance = SplitContainer2.SplitterDistance
        SplitContainer2.SplitterDistance = SplitContainer2.Width
        SplitContainer2.Panel2.Hide()

    End Sub

    Private Sub ExpandSP2Pane2()

        SplitContainer2.SplitterDistance = SP2Distance
        SplitContainer2.Panel2.Show()

    End Sub

    Private Sub CollapseSP1Pane1()

        SplitContainer1.SplitterDistance = 0
        SplitContainer1.Panel1.Hide()

    End Sub

    Private Sub ExpandSP1Pane1()

        SplitContainer1.SplitterDistance = SP1Distance
        SplitContainer1.Panel1.Show()

    End Sub

    Private Sub HideAllMenusItems()

        DisplayFirstTreeOnly()

        entitiesTV.Visible = False
        entitiesFiltersTV.Visible = False
        clientsTV.Visible = False
        clientsFiltersTV.Visible = False
        productsTV.Visible = False
        productsFiltersTV.Visible = False
        adjustmentsTV.Visible = False
        CurrenciesCLB.Visible = False
        periodsCLB.Visible = False
        versionsTV.Visible = False

        EntitiesFlag = False
        EntitiesCategoriesFlag = False
        ClientsFlag = False
        ClientsCategoriesFlag = False
        productsFlag = False
        productsCategoriesFlag = False
        adjustments_flag = False
        CurrenciesFlag = False
        PeriodsFlag = False
        VersionsFlag = False

    End Sub

    Private Sub DisplayFirstTreeOnly()

        TVTableLayout.RowStyles.Item(1).SizeType = SizeType.Percent
        TVTableLayout.RowStyles.Item(2).SizeType = SizeType.Percent
        TVTableLayout.RowStyles.Item(1).Height = 100
        TVTableLayout.RowStyles.Item(2).Height = 0

    End Sub

    Private Sub DisplayTwoTrees()

        TVTableLayout.RowStyles.Item(0).SizeType = SizeType.Percent
        TVTableLayout.RowStyles.Item(1).SizeType = SizeType.Percent
        TVTableLayout.RowStyles.Item(0).Height = 50
        TVTableLayout.RowStyles.Item(1).Height = 50

    End Sub

#End Region


#Region "Dimension Right Pane Call Backs"

    Private Sub HideRightPane()

        CollapseSP2Pane2()

    End Sub

    Private Sub RefreshFromRightPane()

        If Not Controller.EntityNode Is Nothing Then
            RefreshData(Controller.EntityNode, True)
        Else
            If Not entitiesTV.SelectedNode Is Nothing Then
                RefreshData(entitiesTV.SelectedNode, True)
            Else
                MsgBox("Please select an Entity to refresh")
            End If
        End If
       

    End Sub

#End Region


#End Region


#Region "Background Workers"

    Delegate Sub AfterDisplayAttemp_Delegate()

    Friend Sub AfterDisplayAttemp_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New AfterDisplayAttemp_Delegate(AddressOf AfterDisplayAttemp_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            CP.Dispose()
            For Each tab_ As vTabPage In tabControl1.TabPages
                Dim dgv As vDataGridView = tab_.Controls(0)
                dgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
                dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
                On Error Resume Next
                dgv.Refresh()
            Next

        End If

    End Sub

#End Region



End Class
