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
' Last modified: 13/10/2015


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
Imports ProgressControls
Imports CRUD


Friend Class ControllingUI_2


#Region "Instance Variables"

#Region "Objects"

    Private m_controller As ControllingUIController
    Friend DGVUTIL As New DataGridViewsUtil
    Friend rightPane_Control As CUI2RightPane
    Friend leftPane_control As CUI2LeftPane
    Private leftSplitContainer As SplitContainer
    Private rightSplitContainer As SplitContainer
    Private m_circularProgress As New ProgressIndicator
    Private leftPaneExpandBT As vButton
    Private rightPaneExpandBT As vButton
    Friend BackgroundWorker1 As New BackgroundWorker
    Private m_logController As New LogController
    Private m_logView As LogView

#End Region

#Region "Variables"

    Private current_DGV_cell As GridCell
    Private rows_list_dic As New Dictionary(Of String, List(Of HierarchyItem))
    Private columns_list_dic As New Dictionary(Of String, List(Of HierarchyItem))
    Private row_index As Int32
    Private column_index As Int32
    Friend accountsTV As New vTreeView
    Private SP1Distance As Single = 230
    Private SP2Distance As Single = 900
    '   Private m_formatsDictionary As New Dictionary(Of int32, Formats.FinancialBIFormat)
    Private m_currentEntityNode As vTreeNode


#End Region

#Region "Data Grid Views Items and Cells Formats"

    Private hierarchyItemNormalStyle As HierarchyItemStyle
    Private hierarchyItemSelectedStyle As HierarchyItemStyle
    Private hierarchyItemDisabledStyle As HierarchyItemStyle
    Private hierarchyImportantItemNormalStyle As HierarchyItemStyle
    Private hierarchyImportantItemSelectedStyle As HierarchyItemStyle
    Private hierarchyImportantItemDisabledStyle As HierarchyItemStyle
    Private hierarchyTitleItemNormalStyle As HierarchyItemStyle
    Private hierarchyTitleItemSelectedStyle As HierarchyItemStyle
    Private hierarchyTitleItemDisabledStyle As HierarchyItemStyle
    Private hierarchyDetailItemNormalStyle As HierarchyItemStyle
    Private hierarchyDetailItemSelectedStyle As HierarchyItemStyle
    Private hierarchyDetailItemDisabledStyle As HierarchyItemStyle

    Private GridCellStyleNormal As GridCellStyle

#End Region

#Region "Flags"

    Private m_isUpdatingPeriodsCheckList As Boolean
    Private m_isVersionComparisonDisplayed As Boolean
    ' Private m_IsUpdatingChildrenCategory As Boolean
    ' Private m_displayControlFlag As Boolean
    ' Private m_leftPaneExpandedFlag As Boolean

#End Region

#Region "Constants"

    Private Const MARGIN_SIZE As Double = 25
    Private Const INNER_MARGIN As Integer = 0
    Private Const EXCEL_SHEET_NAME_MAX_LENGHT = 31
    Private Const DGV_THEME As Int32 = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
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
        RightPaneSetup()
        m_controller = New ControllingUIController(Me)
        GlobalVariables.Accounts.LoadAccountsTV(accountsTV)
        SetupProgressUIs()

        ' Init TabControl
        For Each node As vTreeNode In accountsTV.Nodes
            Dim newTab As New vTabPage
            newTab.Text = node.Text
            newTab.Name = node.Value
            DGVsControlTab.TabPages.Add(newTab)
        Next
        InitItemsFormat()
        MultilangueSetup()

        ' Refreshing Background Worker
        BackgroundWorker1.WorkerSupportsCancellation = True
        AddHandler BackgroundWorker1.DoWork, AddressOf BackgroundWorker1_DoWork
        AddHandler BackgroundWorker1.RunWorkerCompleted, AddressOf backgroundWorker1_RunWorkerCompleted

        ' Accounts Events
        AddHandler GlobalVariables.Accounts.Read, AddressOf AccountUpdateFromServer
        AddHandler GlobalVariables.Accounts.DeleteEvent, AddressOf AccountDeleteFromServer

    End Sub

    Private Sub SetupProgressUIs()

        ' Progress Bar
        m_progressBar.Visible = False

        ' Progress Indicator (circular)
        m_circularProgress.CircleColor = Drawing.Color.Purple
        m_circularProgress.NumberOfCircles = 12
        m_circularProgress.NumberOfVisibleCircles = 8
        m_circularProgress.AnimationSpeed = 75
        m_circularProgress.CircleSize = 0.7
        m_circularProgress.Width = 79
        m_circularProgress.Height = 79
        SplitContainer1.Panel2.Controls.Add(m_circularProgress)
        m_circularProgress.Visible = False

    End Sub

    Private Sub LeftPaneSetup()

        leftPane_control = New CUI2LeftPane
        Me.SplitContainer1.Panel1.Controls.Add(leftPane_control)
        leftPane_control.Dock = DockStyle.Fill

        leftPaneExpandBT = New vButton
        leftPaneExpandBT.Width = 19
        leftPaneExpandBT.Height = 19
        leftPaneExpandBT.ImageList = ExpansionImageList
        leftPaneExpandBT.ImageIndex = 0
        leftPaneExpandBT.Text = ""
        leftPaneExpandBT.FlatStyle = FlatStyle.Flat
        leftPaneExpandBT.FlatAppearance.BorderSize = 0
        leftPaneExpandBT.PaintBorder = False
        leftPaneExpandBT.ImageAlign = Drawing.ContentAlignment.MiddleCenter
        leftPaneExpandBT.Visible = False
        Me.SplitContainer1.Panel1.Controls.Add(leftPaneExpandBT)

        AddHandler leftPane_control.entitiesTV.KeyDown, AddressOf EntitiesTV_KeyDown
        AddHandler leftPane_control.periodsTV.NodeChecked, AddressOf periodsTV_ItemCheck
        AddHandler leftPane_control.PanelCollapseBT.Click, AddressOf CollapseSP1Pane1
        AddHandler leftPaneExpandBT.Click, AddressOf ExpandSP1Pane1

        leftPane_control.entitiesTV.ContextMenuStrip = EntitiesRCMenu
        leftPane_control.adjustmentsTV.ContextMenuStrip = AdjustmentsRCMenu
        leftPane_control.periodsTV.ContextMenuStrip = PeriodsRCMenu
        leftPane_control.SelectionCB.DropDownWidth = leftPane_control.SelectionCB.Width
        leftPane_control.SelectionCB.DropDownHeight = leftPane_control.SelectionCB.ItemHeight * 12


        Dim vNode As vTreeNode = VTreeViewUtil.FindNode(leftPane_control.versionsTV, My.Settings.version_id)
        If Not vNode Is Nothing Then
            vNode.Checked = Windows.Forms.CheckState.Checked
        End If


    End Sub

    Private Sub RightPaneSetup()

        Dim entitiesFiltersNode As New TreeNode
        Dim clientsFiltersNode As New TreeNode
        Dim productsFiltersNode As New TreeNode
        Dim adjustmentsFiltersNode As New TreeNode

        GlobalVariables.Filters.LoadFiltersNode(entitiesFiltersNode, GlobalEnums.AnalysisAxis.ENTITIES)
        GlobalVariables.Filters.LoadFiltersNode(clientsFiltersNode, GlobalEnums.AnalysisAxis.CLIENTS)
        GlobalVariables.Filters.LoadFiltersNode(productsFiltersNode, GlobalEnums.AnalysisAxis.PRODUCTS)
        GlobalVariables.Filters.LoadFiltersNode(adjustmentsFiltersNode, GlobalEnums.AnalysisAxis.ADJUSTMENTS)

        rightPane_Control = New CUI2RightPane(entitiesFiltersNode, _
                                              clientsFiltersNode, _
                                              productsFiltersNode, _
                                              adjustmentsFiltersNode)

        SplitContainer2.Panel2.Controls.Add(rightPane_Control)
        rightPane_Control.Dock = DockStyle.Fill

        rightPane_Control.CollapseRightPaneBT.ImageList = ExpansionImageList
        rightPane_Control.CollapseRightPaneBT.ImageIndex = 1

        rightPaneExpandBT = New vButton
        SplitContainer2.Panel2.Controls.Add(rightPaneExpandBT)
        rightPaneExpandBT.Width = 19
        rightPaneExpandBT.Height = 19
        rightPaneExpandBT.ImageList = ExpansionImageList
        rightPaneExpandBT.Margin = New Padding(3, 5, 3, 3)
        rightPaneExpandBT.ImageIndex = 0
        rightPaneExpandBT.Text = ""
        rightPaneExpandBT.FlatStyle = FlatStyle.Flat
        rightPaneExpandBT.PaintBorder = False
        ' rightPaneExpandBT.ImageAlign = Drawing.ContentAlignment.MiddleCenter
        rightPaneExpandBT.Visible = False

        AddHandler rightPane_Control.UpdateBT.Click, AddressOf RefreshFromRightPane
        AddHandler rightPane_Control.CollapseRightPaneBT.Click, AddressOf CollapseSP2Pane2
        AddHandler rightPaneExpandBT.Click, AddressOf ExpandSP2Pane2

    End Sub

    Private Sub DataMiningUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        GridCellStyleNormal = GridTheme.GetDefaultTheme(DGV_THEME).GridCellStyle
        GridCellStyleNormal.Font = New System.Drawing.Font(GridCellStyleNormal.Font.FontFamily, My.Settings.dgvFontSize)

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
            DGV.RowsHierarchy.AllowResize = False
            tab_.Controls.Add(DGV)
            AddHandler DGV.CellMouseClick, AddressOf DGV_CellMouseClick
        Next

        If Not IsNothing(DGVsControlTab.TabPages(0)) Then
            DGVsControlTab.SelectedTab = DGVsControlTab.TabPages(0)
        End If
        Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub MultilangueSetup()

        Me.RefreshRightClick.Text = Local.GetValue("CUI.refresh")
        Me.SelectAllToolStripMenuItem.Text = Local.GetValue("CUI.select_all")
        Me.UnselectAllToolStripMenuItem.Text = Local.GetValue("CUI.unselect_all")
        Me.ExpandAllRightClick.Text = Local.GetValue("CUI.expand_all")
        Me.CollapseAllRightClick.Text = Local.GetValue("CUI.collapse_all")
        Me.LogRightClick.Text = Local.GetValue("CUI.log")
        Me.DGVFormatsButton.Text = Local.GetValue("CUI.display_options")
        Me.ColumnsAutoSize.Text = Local.GetValue("CUI.adjust_columns_size")
        Me.ColumnsAutoFitBT.Text = Local.GetValue("CUI.automatic_columns_adjustment")
        Me.SelectAllToolStripMenuItem1.Text = Local.GetValue("CUI.select_all")
        Me.UnselectAllToolStripMenuItem1.Text = Local.GetValue("CUI.unselect_all")

        ' labels
        Me.m_currencyLabel.Text = Local.GetValue("CUI.currency")
        Me.m_versionLabel.Text = Local.GetValue("CUI.version")
        Me.m_entityLabel.Text = Local.GetValue("CUI.entity")

        Me.MainMenu.Text = Local.GetValue("CUI.main_menu")
        Me.RefreshToolStripMenuItem.Text = Local.GetValue("CUI.refresh")
        Me.ExcelToolStripMenuItem.ToolTipText = Local.GetValue("CUI.drop_on_excel_tooltip")
        Me.ExcelToolStripMenuItem.Text = Local.GetValue("CUI.drop_on_excel")
        Me.DropOnExcelToolStripMenuItem.Text = Local.GetValue("CUI.drop_on_excel")
        Me.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Text = Local.GetValue("CUI.drop_on_excel_visible_part")
        Me.BusinessControlToolStripMenuItem.Text = Local.GetValue("CUI.performance_review")
        Me.BusinessControlToolStripMenuItem.ToolTipText = Local.GetValue("CUI.performance_review_tooltip")
        Me.VersionsComparisonToolStripMenuItem.Text = Local.GetValue("CUI.display_versions_comparison")
        Me.SwitchVersionsToolStripMenuItem.Text = Local.GetValue("CUI.switch_versions")
        Me.HideVersionsComparisonToolStripMenuItem.Text = Local.GetValue("CUI.take_off_comparison")
        Me.RefreshToolStripMenuItem.ToolTipText = Local.GetValue("CUI.refresh_tooltip")
        Me.ChartBT.Text = Local.GetValue("CUI.charts")
        Me.Text = Local.GetValue("CUI.financials")

    End Sub

#End Region


#Region "Interface"

    Friend Sub RefreshData(Optional ByRef useCache As Boolean = False)

        If m_controller.m_isComputingFlag = True Then
            Exit Sub
        End If
        '   m_progressBar.Left = (SplitContainer1.Panel2.Width - m_progressBar.Width) / 2
        '   m_progressBar.Top = (SplitContainer1.Panel2.Height - m_progressBar.Height) / 2
        DGVsControlTab.Visible = False
        m_progressBar.Visible = True
        m_progressBar.Enabled = True
        m_progressBar.Show()
        BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub RefreshFromRightPane()

        If Not m_controller.m_entityNode Is Nothing Then
            m_currentEntityNode = m_controller.m_entityNode
            RefreshData(True)
        Else
            If Not leftPane_control.entitiesTV.SelectedNode Is Nothing Then
                m_currentEntityNode = leftPane_control.entitiesTV.SelectedNode
                RefreshData(True)
            Else
                RefreshData(True)
            End If
        End If

    End Sub

    Friend Sub FormatDGVItem(ByRef item As HierarchyItem)

        Dim currencyId As UInt32 = leftPane_control.currenciesCLB.SelectedItem.Value
        Dim l_currency As Currency = GlobalVariables.Currencies.GetValue(currencyId)
        If l_currency Is Nothing Then Exit Sub

        item.CellsStyle = GridCellStyleNormal
        item.CellsTextAlignment = System.Drawing.ContentAlignment.MiddleRight

        If item.IsRowsHierarchyItem Then
            ' Account's Type formatting
            Dim typeId As Int32 = m_controller.GetAccountTypeFromId(item.ItemValue)
            If typeId <> 0 Then
                Select Case typeId
                    Case Account.AccountType.MONETARY : item.CellsFormatString = "{0:" & l_currency.Symbol & "#,##0;(" & l_currency.Symbol & "#,##0)}" ' m_currenciesSymbol_dict(currencyId) & "#,##0.00;(" & m_currenciesSymbol_dict(currencyId) & "#,##0.00)"
                    Case Account.AccountType.PERCENTAGE : item.CellsFormatString = "{0:P}" '"0.00%"        ' put this in a table ?
                    Case Account.AccountType.NUMBER : item.CellsFormatString = "{0:N2}" '"#,##0.00"
                    Case Account.AccountType.DATE_ : item.CellsFormatString = "{0:yyyy/MMMM/dd}"  '"d-mmm-yy" ' d-mmm-yy
                    Case Else : item.CellsFormatString = "{0:N}"
                End Select
            End If

            ' Account's Format
            Dim formatId As String = m_controller.GetAccountFormatFromId(item.ItemValue)
            If formatId <> "" Then
                Select Case formatId
                    Case "t"
                        item.HierarchyItemStyleNormal = hierarchyTitleItemNormalStyle
                        item.HierarchyItemStyleSelected = hierarchyTitleItemSelectedStyle
                        item.HierarchyItemStyleDisabled = hierarchyTitleItemDisabledStyle
                    Case "i"
                        item.HierarchyItemStyleNormal = hierarchyImportantItemNormalStyle
                        item.HierarchyItemStyleSelected = hierarchyImportantItemSelectedStyle
                        item.HierarchyItemStyleDisabled = hierarchyImportantItemDisabledStyle
                    Case "n"
                        item.HierarchyItemStyleNormal = hierarchyItemNormalStyle
                        item.HierarchyItemStyleSelected = hierarchyItemSelectedStyle
                        item.HierarchyItemStyleDisabled = hierarchyItemDisabledStyle
                    Case "d"
                        item.HierarchyItemStyleNormal = hierarchyDetailItemNormalStyle
                        item.HierarchyItemStyleSelected = hierarchyDetailItemSelectedStyle
                        item.HierarchyItemStyleDisabled = hierarchyDetailItemDisabledStyle
                End Select
            End If
        Else
            item.HierarchyItemStyleNormal = hierarchyItemNormalStyle
            item.HierarchyItemStyleSelected = hierarchyItemSelectedStyle
            item.HierarchyItemStyleDisabled = hierarchyItemDisabledStyle
        End If

        If item.IsColumnsHierarchyItem _
       AndAlso item.Caption.Length > 20 Then
            item.TextWrap = True
        End If

    End Sub

#End Region


#Region "Events"

    Private Sub AccountUpdateFromServer(ByRef status As Boolean, ByRef p_account As Account)
        ReloadAccountsTV_ThreadSafe()
    End Sub

    Private Sub AccountDeleteFromServer(ByRef status As Boolean, ByRef id As UInt32)
        ReloadAccountsTV_ThreadSafe()
    End Sub

    Private Sub EntitiesTV_KeyDown(sender As Object, e As KeyEventArgs)

        ' to be managed !! -> goes into left pane ? priority normal
        If e.KeyCode = Keys.Enter Then
            If Not leftPane_control.entitiesTV.SelectedNode Is Nothing Then
                m_currentEntityNode = leftPane_control.entitiesTV.SelectedNode
                RefreshData()
            Else
                m_currentEntityNode = Nothing
                RefreshData()
            End If
        End If

    End Sub

    ' Periods filter when unchecked
    Private Sub periodsTV_ItemCheck(sender As Object, e As vTreeViewEventArgs)

        If m_isUpdatingPeriodsCheckList = False Then
            Dim periodSelectionDict As New Dictionary(Of String, Boolean)

            For Each node As vTreeNode In leftPane_control.periodsTV.GetNodes
                If node.Checked = CheckState.Checked Then
                    periodSelectionDict.Add(node.Text, True)
                Else
                    periodSelectionDict.Add(node.Text, False)
                End If
            Next
            m_controller.PeriodsSelectionFilter(periodSelectionDict)
        End If

    End Sub

    Private Sub DGV_CellMouseClick(sender As Object, e As CellMouseEventArgs)

        current_DGV_cell = e.Cell

    End Sub

    Private Sub tabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DGVsControlTab.SelectedIndexChanged

        m_controller.cellsUpdateNeeded = True

    End Sub

#End Region


#Region "Calls Backs"

#Region "Data Grid View Righ Click Menu Calls backs"

    Private Sub Compute_Click(sender As Object, e As EventArgs) Handles RefreshRightClick.Click

        m_currentEntityNode = leftPane_control.entitiesTV.SelectedNode
        RefreshData()

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

    Private Sub LogRightClick_Click(sender As Object, e As EventArgs) Handles LogRightClick.Click

        If Not current_DGV_cell Is Nothing Then
            Dim accountId As Int32 = 0
            Dim entityId As Int32 = 0
            Dim periodId As String = ""
            Dim versionId As String = 0
            Dim filterId As String = "0"

            m_controller.SetCellsItems(current_DGV_cell.RowItem, _
                                       current_DGV_cell.ColumnItem, _
                                       entityId, _
                                       accountId, _
                                       periodId, _
                                       versionId, _
                                       filterId)

            Dim l_account As Account = GlobalVariables.Accounts.GetValue(accountId)
            If l_account Is Nothing Then Exit Sub
            If l_account.FormulaType = Account.FormulaTypes.HARD_VALUE_INPUT _
            Or l_account.FormulaType = Account.FormulaTypes.FIRST_PERIOD_INPUT Then

                Dim logsHashTable As New Action(Of List(Of Hashtable))(AddressOf DisplayLog_ThreadSafe)
                m_logController.GetFactLog(accountId, _
                                           entityId, _
                                           Strings.Right(periodId, Len(periodId) - 1), _
                                           versionId,
                                           logsHashTable)

                Dim l_entity As AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Entities, entityId)
                If l_entity Is Nothing Then Exit Sub
                If l_entity.AllowEdition = True Then

                End If
                m_logView = New LogView(False, _
                                        l_entity.Name, _
                                        l_account.Name)
            Else
                MsgBox("Selected entity is not editable. Log is only available on editable entities.")
            End If
        Else
            MsgBox("Selected account is not an input. Log is only available on inputs accounts.")
        End If

    End Sub

    Private Sub FormatsRCMBT_Click(sender As Object, e As EventArgs) Handles DGVFormatsButton.Click

        ' to be reimplemented => formats = settings

    End Sub


#End Region


#Region "Main Menu Calls Backs"

    Private Sub VersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VersionsComparisonToolStripMenuItem.Click

        If m_isVersionComparisonDisplayed = True Then
            m_controller.VersionsCompDisplay(False)
            m_isVersionComparisonDisplayed = False
        Else
            If m_controller.m_versionsDict.Count = 2 Then
                m_controller.VersionsCompDisplay(True)
                m_isVersionComparisonDisplayed = True
            Else
                MsgBox(Local.GetValue("CUI.2_versions_alert"))
            End If
        End If

    End Sub

    Private Sub SwitchVersionsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SwitchVersionsToolStripMenuItem.Click
        m_controller.ReverseVersionsComparison()
    End Sub

    Private Sub HideVersionsComparisonToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideVersionsComparisonToolStripMenuItem.Click

        If m_isVersionComparisonDisplayed Then
            For Each tab_ As vTabPage In DGVsControlTab.TabPages
                Dim DGV As vDataGridView = tab_.Controls(0)
                DGVUTIL.RemoveVersionsComparison(DGV)
            Next
            m_isVersionComparisonDisplayed = False
        End If

    End Sub

    Private Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DropOnExcelToolStripMenuItem.Click
        m_controller.DropOnExcel(False)
    End Sub

    Private Sub DropOnlyTheVisibleItemsOnExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Click
        m_controller.DropOnExcel(True)
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshToolStripMenuItem.Click

        If leftPane_control.entitiesTV.SelectedNode Is Nothing Then
            m_currentEntityNode = Nothing
            RefreshData()
        Else
            m_currentEntityNode = leftPane_control.entitiesTV.SelectedNode
            RefreshData()
        End If

    End Sub

    Private Sub ChartsBT_Click(sender As Object, e As EventArgs) Handles ChartBT.Click

        m_controller.ShowCharts()

    End Sub

#End Region


#Region "Periods Checked List Box Call backs"

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click

        m_isUpdatingPeriodsCheckList = True
        VTreeViewUtil.CheckStateAllNodes(leftPane_control.periodsTV, CheckState.Checked)
        m_isUpdatingPeriodsCheckList = False
        periodsTV_ItemCheck(sender, New vTreeViewEventArgs(leftPane_control.periodsTV.SelectedNode, vTreeViewAction.Unknown))

    End Sub

    Private Sub UnselectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnselectAllToolStripMenuItem.Click

        m_isUpdatingPeriodsCheckList = True
        VTreeViewUtil.CheckStateAllNodes(leftPane_control.periodsTV, CheckState.Unchecked)
        m_isUpdatingPeriodsCheckList = False
        periodsTV_ItemCheck(sender, New vTreeViewEventArgs(leftPane_control.periodsTV.SelectedNode, vTreeViewAction.Unknown))


    End Sub

#End Region


#Region "Right Click Menu"

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

    Private Sub CollapseAllRightClick_Click(sender As Object, e As EventArgs) Handles CollapseAllRightClick.Click
        Dim dgv As vDataGridView = DGVsControlTab.SelectedTab.Controls(0)
        dgv.RowsHierarchy.CollapseAllItems()
    End Sub

    Private Sub ExpandAllRightClick_Click(sender As Object, e As EventArgs) Handles ExpandAllRightClick.Click
        Dim dgv As vDataGridView = DGVsControlTab.SelectedTab.Controls(0)
        dgv.RowsHierarchy.ExpandAllItems()
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

    Delegate Sub SetComputeButtonState_Delegate(ByRef p_state As Boolean)
    Friend Sub SetComputeButtonState(ByRef p_state As Boolean)
        If InvokeRequired Then
            Dim MyDelegate As New SetComputeButtonState_Delegate(AddressOf SetComputeButtonState)
            Me.Invoke(MyDelegate, New Object() {p_state})
        Else
            RefreshToolStripMenuItem.Enabled = p_state
            rightPane_Control.UpdateBT.Enabled = p_state
            If Not RefreshToolStripMenuItem.GetCurrentParent() Is Nothing Then RefreshToolStripMenuItem.GetCurrentParent().Refresh()
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
        RefreshBackGround()
    End Sub

    Delegate Sub RefreshBackGround_Delegate()
    Private Sub RefreshBackGround()

        If InvokeRequired Then
            Dim MyDelegate As New RefreshBackGround_Delegate(AddressOf RefreshBackGround)
            Me.Invoke(MyDelegate, New Object() {})
        Else

            Dim versionsIds As New List(Of Int32)
            For Each versionId In VTreeViewUtil.GetCheckedNodesIds(leftPane_control.versionsTV)
                Dim version As Version = GlobalVariables.Versions.GetValue(versionId)
                If version Is Nothing Then Continue For
                If version.IsFolder = False Then
                    versionsIds.Add(versionId)
                End If
            Next

            If m_currentEntityNode Is Nothing Then
                If leftPane_control.entitiesTV.Nodes.Count > 0 Then
                    m_currentEntityNode = leftPane_control.entitiesTV.Nodes(0)
                    If versionsIds.Count > 0 Then
                        ' Launch Computation
                        Try
                            m_controller.Compute(versionsIds.ToArray, m_currentEntityNode)
                        Catch ex As OutOfMemoryException
                            System.Diagnostics.Debug.WriteLine(ex.Message)
                            MsgBox(Local.GetValue("CUI.request_too_complex"))
                            'AfterWorkDoneAttemp_ThreadSafe()
                        End Try
                    Else
                        MsgBox(Local.GetValue("CUI.need_one_version"))
                    End If
                Else
                    MsgBox(Local.GetValue("CUI.no_entity_selected"))
                    Exit Sub
                End If
            Else
                If versionsIds.Count > 0 Then
                    ' Launch Computation
                    Try
                        m_controller.Compute(versionsIds.ToArray, m_currentEntityNode)
                    Catch ex As Exception
                        System.Diagnostics.Debug.WriteLine(ex.Message)
                        MsgBox(Local.GetValue("CUI.request_too_complex"))
                        '   AfterWorkDoneAttemp_ThreadSafe()
                    End Try
                Else
                    MsgBox(Local.GetValue("CUI.need_one_version"))
                End If
            End If

        End If

    End Sub

    Private Sub backgroundWorker1_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)

        AfterWorkDoneAttemp_ThreadSafe(e)

    End Sub

    Delegate Sub AfterWorkDoneAttemp_Delegate(e As RunWorkerCompletedEventArgs)
    Friend Sub AfterWorkDoneAttemp_ThreadSafe(e As RunWorkerCompletedEventArgs)

        If InvokeRequired Then
            Dim MyDelegate As New AfterWorkDoneAttemp_Delegate(AddressOf AfterWorkDoneAttemp_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {e})
        Else
            If Not m_progressBar Is Nothing Then
                m_progressBar.Visible = False
            End If
        End If

    End Sub

    Delegate Sub LaunchCircularProgress_Delegate()
    Friend Sub LaunchCircularProgress()

        If InvokeRequired Then
            Dim MyDelegate As New LaunchCircularProgress_Delegate(AddressOf LaunchCircularProgress)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            m_circularProgress.Left = (SplitContainer1.Panel2.Width - m_circularProgress.Width) / 2
            m_circularProgress.Top = (SplitContainer1.Panel2.Height - m_circularProgress.Height) / 2
            m_circularProgress.Start()
            m_circularProgress.Visible = True
        End If

    End Sub

    Delegate Sub TerminateCircularProgress_Delegate()
    Friend Sub TerminateCircularProgress()

        If InvokeRequired Then
            Dim MyDelegate As New TerminateCircularProgress_Delegate(AddressOf TerminateCircularProgress)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            m_circularProgress.Stop()
            m_circularProgress.Visible = False
            DGVsControlTab.Visible = True
        End If

    End Sub

    Delegate Sub DGVFormattingAttemp_Delegate()
    Friend Sub FormatDGV_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New DGVFormattingAttemp_Delegate(AddressOf FormatDGV_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            Dim dgvFormatter As New DataGridViewsUtil
            For Each tab_ As vTabPage In DGVsControlTab.TabPages
                If tab_.Controls.Count <= 0 Then Continue For
                Dim dgv As vDataGridView = tab_.Controls(0)
                dgv.Select()
                dgv.GroupingDefaultHeaderTextVisible = True
                dgv.BackColor = Color.White
                dgv.GridLinesDisplayMode = GridLinesDisplayMode.DISPLAY_NONE
                dgv.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
                dgv.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
                '    dgv.RowsHierarchy.AllowResize = False
                dgv.RowsHierarchy.CompactStyleRenderingEnabled = True
                dgv.ColumnsHierarchy.AutoStretchColumns = True
                dgv.ColumnsHierarchy.ExpandAllItems()

                ' attention !!! test
                '    dgvFormatter.FormatDGVs(dgv, 34)

                dgv.Update()
                dgv.Refresh()
            Next
        End If

    End Sub

    Delegate Sub DisplayLogAttemp_Delegate(p_logValuesHt As List(Of Hashtable))
    Private Sub DisplayLog_ThreadSafe(p_logValuesHt As List(Of Hashtable))

        If InvokeRequired Then
            Dim MyDelegate As New DisplayLogAttemp_Delegate(AddressOf DisplayLog_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {p_logValuesHt})
        Else
            m_logView.DisplayLogValues(p_logValuesHt)
        End If

    End Sub

    Delegate Sub ReloadAccountsTV_Delegate()
    Friend Sub ReloadAccountsTV_ThreadSafe()

        If Me.accountsTV.InvokeRequired Then
            Dim MyDelegate As New ReloadAccountsTV_Delegate(AddressOf ReloadAccountsTV_ThreadSafe)
            Me.accountsTV.Invoke(MyDelegate, New Object() {})
        Else
            GlobalVariables.Accounts.LoadAccountsTV(accountsTV)
        End If

    End Sub

#End Region


#Region "Formatting"

    Private Sub InitItemsFormat()

        hierarchyItemNormalStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleNormal
        hierarchyItemSelectedStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleSelected
        hierarchyItemDisabledStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleDisabled
        hierarchyItemNormalStyle.Font = New System.Drawing.Font(hierarchyItemNormalStyle.Font.FontFamily, My.Settings.dgvFontSize)
        hierarchyItemSelectedStyle.Font = New System.Drawing.Font(hierarchyItemSelectedStyle.Font.FontFamily, My.Settings.dgvFontSize)
        hierarchyItemDisabledStyle.Font = New System.Drawing.Font(hierarchyItemDisabledStyle.Font.FontFamily, My.Settings.dgvFontSize)

        hierarchyImportantItemNormalStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleNormal
        hierarchyImportantItemSelectedStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleSelected
        hierarchyImportantItemDisabledStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleDisabled
        hierarchyImportantItemNormalStyle.Font = New System.Drawing.Font(hierarchyImportantItemNormalStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Bold)
        hierarchyImportantItemSelectedStyle.Font = New System.Drawing.Font(hierarchyImportantItemSelectedStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Bold)
        hierarchyImportantItemDisabledStyle.Font = New System.Drawing.Font(hierarchyImportantItemDisabledStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Bold)

        hierarchyTitleItemNormalStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleNormal
        hierarchyTitleItemSelectedStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleSelected
        hierarchyTitleItemDisabledStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleDisabled
        hierarchyTitleItemNormalStyle.Font = New System.Drawing.Font(hierarchyTitleItemNormalStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Bold)
        hierarchyTitleItemSelectedStyle.Font = New System.Drawing.Font(hierarchyTitleItemSelectedStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Bold)
        hierarchyTitleItemDisabledStyle.Font = New System.Drawing.Font(hierarchyTitleItemDisabledStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Bold)

        hierarchyDetailItemNormalStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleNormal
        hierarchyDetailItemSelectedStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleSelected
        hierarchyDetailItemDisabledStyle = GridTheme.GetDefaultTheme(DGV_THEME).HierarchyItemStyleDisabled
        hierarchyDetailItemNormalStyle.Font = New System.Drawing.Font(hierarchyDetailItemNormalStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Italic)
        hierarchyDetailItemSelectedStyle.Font = New System.Drawing.Font(hierarchyDetailItemSelectedStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Italic)
        hierarchyDetailItemDisabledStyle.Font = New System.Drawing.Font(hierarchyDetailItemDisabledStyle.Font.FontFamily, My.Settings.dgvFontSize, FontStyle.Italic)

    End Sub


#End Region


  
End Class
