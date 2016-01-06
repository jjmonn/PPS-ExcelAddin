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
    Friend m_rightPaneControl As CUI2RightPane
    Friend m_leftPaneControl As CUI2LeftPane
    Private m_leftSplitContainer As SplitContainer
    Private m_rightSplitContainer As SplitContainer
    Private m_circularProgress As New ProgressIndicator
    Private m_leftPaneExpandBT As vButton
    Private m_rightPaneExpandBT As vButton
    Friend m_BackgroundWorker1 As New BackgroundWorker
    Private m_logController As New FactLogController
    Private m_logView As LogView

#End Region

#Region "Variables"

    Private m_currentDGVCell As GridCell
    Private m_rowsListDic As New SafeDictionary(Of String, List(Of HierarchyItem))
    Private m_columnsListDic As New SafeDictionary(Of String, List(Of HierarchyItem))
    Private m_rowIndex As Int32
    Private m_columnIndex As Int32
    Friend m_accountsTreeview As New vTreeView
    Private m_SplitContainer1Distance As Single = 230
    Private m_SplitContainer2Distance As Single = 900
    Friend m_process As Account.AccountProcess
    Private m_currentEntityNode As vTreeNode
    Private m_periodsList As List(Of Int32)

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

    Friend Shared ACCOUNTS_CODE As String = Local.GetValue("general.accounts")
    Friend Shared YEARS_CODE As String = Local.GetValue("general.years")
    Friend Shared MONTHS_CODE As String = Local.GetValue("general.months")
    Friend Shared WEEKS_CODE As String = Local.GetValue("general.weeks")
    Friend Shared DAYS_CODE As String = Local.GetValue("general.days")
    Friend Shared VERSIONS_CODE As String = Local.GetValue("general.versions")
    Friend Shared ENTITIES_CODE As String = Local.GetValue("general.entities")
    Friend Shared CLIENTS_CODE As String = Local.GetValue("general.clients")
    Friend Shared PRODUCTS_CODE As String = Local.GetValue("general.products")
    Friend Shared ADJUSTMENT_CODE As String = Local.GetValue("general.adjustments")
    Friend Shared EMPLOYEE_CODE As String = Local.GetValue("general.employees")


#End Region

#End Region


#Region "Initialization"

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call. 
        m_process = My.Settings.processId
        LeftPaneSetup()
        RightPaneSetup()
        m_controller = New ControllingUIController(Me)
        SetupProgressUIs()

        Select Case m_process
            Case Account.AccountProcess.FINANCIAL : GlobalVariables.Accounts.LoadAccountsTV(m_accountsTreeview)
            Case Account.AccountProcess.RH : GlobalVariables.Accounts.LoadRHAccountsTV(m_accountsTreeview)
        End Select

        ' Init TabControl
        For Each node As vTreeNode In m_accountsTreeview.Nodes
            Dim newTab As New vTabPage
            newTab.Text = node.Text
            newTab.Name = node.Value
            DGVsControlTab.TabPages.Add(newTab)
        Next
        ' Accounts Events
        AddHandler GlobalVariables.Accounts.Read, AddressOf AccountUpdateFromServer
        AddHandler GlobalVariables.Accounts.DeleteEvent, AddressOf AccountDeleteFromServer

        InitItemsFormat()
        MultilangueSetup()

        ' Refreshing Background Worker
        m_BackgroundWorker1.WorkerSupportsCancellation = True
        AddHandler m_BackgroundWorker1.DoWork, AddressOf BackgroundWorker1_DoWork
        AddHandler m_BackgroundWorker1.RunWorkerCompleted, AddressOf backgroundWorker1_RunWorkerCompleted

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

        m_leftPaneControl = New CUI2LeftPane(m_process)
        Me.SplitContainer1.Panel1.Controls.Add(m_leftPaneControl)
        m_leftPaneControl.Dock = DockStyle.Fill

        m_leftPaneExpandBT = New vButton
        m_leftPaneExpandBT.Width = 19
        m_leftPaneExpandBT.Height = 19
        m_leftPaneExpandBT.ImageList = ExpansionImageList
        m_leftPaneExpandBT.ImageIndex = 0
        m_leftPaneExpandBT.Text = ""
        m_leftPaneExpandBT.FlatStyle = FlatStyle.Flat
        m_leftPaneExpandBT.FlatAppearance.BorderSize = 0
        m_leftPaneExpandBT.PaintBorder = False
        m_leftPaneExpandBT.ImageAlign = Drawing.ContentAlignment.MiddleCenter
        m_leftPaneExpandBT.Visible = False
        Me.SplitContainer1.Panel1.Controls.Add(m_leftPaneExpandBT)

        AddHandler m_leftPaneControl.entitiesTV.KeyDown, AddressOf EntitiesTV_KeyDown
        AddHandler m_leftPaneControl.periodsTV.NodeChecked, AddressOf periodsTV_ItemCheck
        AddHandler m_leftPaneControl.PanelCollapseBT.Click, AddressOf CollapseSP1Pane1
        AddHandler m_leftPaneExpandBT.Click, AddressOf ExpandSP1Pane1

        m_leftPaneControl.entitiesTV.ContextMenuStrip = EntitiesRCMenu
        m_leftPaneControl.adjustmentsTV.ContextMenuStrip = AdjustmentsRCMenu
        m_leftPaneControl.periodsTV.ContextMenuStrip = PeriodsRCMenu
        m_leftPaneControl.SelectionCB.DropDownWidth = m_leftPaneControl.SelectionCB.Width
        m_leftPaneControl.SelectionCB.DropDownHeight = m_leftPaneControl.SelectionCB.ItemHeight * 12


        Dim vNode As vTreeNode = VTreeViewUtil.FindNode(m_leftPaneControl.versionsTV, My.Settings.version_id)
        If Not vNode Is Nothing Then
            vNode.Checked = Windows.Forms.CheckState.Checked
        End If


    End Sub

    Private Sub RightPaneSetup()

        Dim l_entitiesFiltersNode As New TreeNode
        Dim l_clientsFiltersNode As New TreeNode
        Dim l_productsFiltersNode As New TreeNode
        Dim l_adjustmentsFiltersNode As New TreeNode
        Dim l_employeesFiltersNode As New TreeNode

        GlobalVariables.Filters.LoadFiltersNode(l_entitiesFiltersNode, GlobalEnums.AnalysisAxis.ENTITIES)
        GlobalVariables.Filters.LoadFiltersNode(l_clientsFiltersNode, GlobalEnums.AnalysisAxis.CLIENTS)
        GlobalVariables.Filters.LoadFiltersNode(l_productsFiltersNode, GlobalEnums.AnalysisAxis.PRODUCTS)
        GlobalVariables.Filters.LoadFiltersNode(l_adjustmentsFiltersNode, GlobalEnums.AnalysisAxis.ADJUSTMENTS)
        GlobalVariables.Filters.LoadFiltersNode(l_employeesFiltersNode, GlobalEnums.AnalysisAxis.EMPLOYEES)

        m_rightPaneControl = New CUI2RightPane(m_process, _
                                              l_entitiesFiltersNode, _
                                              l_clientsFiltersNode, _
                                              l_productsFiltersNode, _
                                              l_adjustmentsFiltersNode, _
                                              l_employeesFiltersNode)

        SplitContainer2.Panel2.Controls.Add(m_rightPaneControl)
        m_rightPaneControl.Dock = DockStyle.Fill

        m_rightPaneControl.CollapseRightPaneBT.ImageList = ExpansionImageList
        m_rightPaneControl.CollapseRightPaneBT.ImageIndex = 1

        m_rightPaneExpandBT = New vButton
        SplitContainer2.Panel2.Controls.Add(m_rightPaneExpandBT)
        m_rightPaneExpandBT.Width = 19
        m_rightPaneExpandBT.Height = 19
        m_rightPaneExpandBT.ImageList = ExpansionImageList
        m_rightPaneExpandBT.Margin = New Padding(3, 5, 3, 3)
        m_rightPaneExpandBT.ImageIndex = 0
        m_rightPaneExpandBT.Text = ""
        m_rightPaneExpandBT.FlatStyle = FlatStyle.Flat
        m_rightPaneExpandBT.PaintBorder = False
        ' rightPaneExpandBT.ImageAlign = Drawing.ContentAlignment.MiddleCenter
        m_rightPaneExpandBT.Visible = False

        AddHandler m_rightPaneControl.UpdateBT.Click, AddressOf RefreshFromRightPane
        AddHandler m_rightPaneControl.CollapseRightPaneBT.Click, AddressOf CollapseSP2Pane2
        AddHandler m_rightPaneExpandBT.Click, AddressOf ExpandSP2Pane2

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

        If m_process = Account.AccountProcess.RH Then
            m_periodsList = m_leftPaneControl.GetRHPeriodSelection
        Else
            m_periodsList = Nothing
        End If

        '   m_progressBar.Left = (SplitContainer1.Panel2.Width - m_progressBar.Width) / 2
        '   m_progressBar.Top = (SplitContainer1.Panel2.Height - m_progressBar.Height) / 2
        DGVsControlTab.Visible = False
        m_progressBar.Visible = True
        m_progressBar.Enabled = True
        m_progressBar.Show()
        m_BackgroundWorker1.RunWorkerAsync()

    End Sub

    Private Sub RefreshFromRightPane()

        If Not m_controller.m_entityNode Is Nothing Then
            m_currentEntityNode = m_controller.m_entityNode
            RefreshData(True)
        Else
            If Not m_leftPaneControl.entitiesTV.SelectedNode Is Nothing Then
                m_currentEntityNode = m_leftPaneControl.entitiesTV.SelectedNode
                RefreshData(True)
            Else
                RefreshData(True)
            End If
        End If

    End Sub

    Friend Sub FormatDGVItem(ByRef item As HierarchyItem)

        Dim currencyId As UInt32 = m_leftPaneControl.currenciesCLB.SelectedItem.Value
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

    Friend Function GetPeriodsList() As Int32()
        If m_periodsList Is Nothing Then
            Return Nothing
        Else
            Return m_periodsList.ToArray
        End If
    End Function

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
            If Not m_leftPaneControl.entitiesTV.SelectedNode Is Nothing Then
                m_currentEntityNode = m_leftPaneControl.entitiesTV.SelectedNode
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
            Dim periodSelectionDict As New SafeDictionary(Of String, Boolean)

            For Each node As vTreeNode In m_leftPaneControl.periodsTV.GetNodes
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

        m_currentDGVCell = e.Cell

    End Sub

    Private Sub tabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DGVsControlTab.SelectedIndexChanged

        m_controller.cellsUpdateNeeded = True

    End Sub

#End Region


#Region "Calls Backs"

#Region "Data Grid View Righ Click Menu Calls backs"

    Private Sub Compute_Click(sender As Object, e As EventArgs) Handles RefreshRightClick.Click

        m_currentEntityNode = m_leftPaneControl.entitiesTV.SelectedNode
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

        If Not m_currentDGVCell Is Nothing Then
            Dim accountId As Int32 = 0
            Dim entityId As Int32 = 0
            Dim periodId As String = ""
            Dim versionId As String = 0
            Dim filterId As String = "0"

            m_controller.SetCellsItems(m_currentDGVCell.RowItem, _
                                       m_currentDGVCell.ColumnItem, _
                                       entityId, _
                                       accountId, _
                                       periodId, _
                                       versionId, _
                                       filterId)

            Dim l_account As Account = GlobalVariables.Accounts.GetValue(accountId)
            If l_account Is Nothing Then Exit Sub
            If l_account.FormulaType = Account.FormulaTypes.HARD_VALUE_INPUT _
            Or l_account.FormulaType = Account.FormulaTypes.FIRST_PERIOD_INPUT Then

                Dim logsHashTable As New Action(Of List(Of FactLog))(AddressOf DisplayLog_ThreadSafe)
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

        If m_leftPaneControl.entitiesTV.SelectedNode Is Nothing Then
            m_currentEntityNode = Nothing
            RefreshData()
        Else
            m_currentEntityNode = m_leftPaneControl.entitiesTV.SelectedNode
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
        VTreeViewUtil.CheckStateAllNodes(m_leftPaneControl.periodsTV, CheckState.Checked)
        m_isUpdatingPeriodsCheckList = False
        periodsTV_ItemCheck(sender, New vTreeViewEventArgs(m_leftPaneControl.periodsTV.SelectedNode, vTreeViewAction.Unknown))

    End Sub

    Private Sub UnselectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnselectAllToolStripMenuItem.Click

        m_isUpdatingPeriodsCheckList = True
        VTreeViewUtil.CheckStateAllNodes(m_leftPaneControl.periodsTV, CheckState.Unchecked)
        m_isUpdatingPeriodsCheckList = False
        periodsTV_ItemCheck(sender, New vTreeViewEventArgs(m_leftPaneControl.periodsTV.SelectedNode, vTreeViewAction.Unknown))


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

        For Each node As vTreeNode In m_leftPaneControl.adjustmentsTV.Nodes
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
                m_leftSplitContainer.Panel1.Controls.Clear()
                m_leftSplitContainer.Panel1.Controls.Add(chart)
            Case TOP_RIGHT_CHART_POSITION
                m_rightSplitContainer.Panel1.Controls.Clear()
                m_rightSplitContainer.Panel1.Controls.Add(chart)
            Case BOTTOM_LEFT_CHART_POSITION
                m_leftSplitContainer.Panel2.Controls.Clear()
                m_leftSplitContainer.Panel2.Controls.Add(chart)
            Case BOTTOM_RIGHT_CHART_POSITION
                m_rightSplitContainer.Panel2.Controls.Clear()
                m_rightSplitContainer.Panel2.Controls.Add(chart)
        End Select

    End Sub


#End Region


#Region "Left Pane Expansion/ Collapse"

    Private Sub ExpandSP2Pane2()

        SplitContainer2.SplitterDistance = m_SplitContainer2Distance
        m_rightPaneControl.Visible = True
        m_rightPaneExpandBT.Visible = False

    End Sub

    Private Sub CollapseSP2Pane2()

        m_SplitContainer2Distance = SplitContainer2.SplitterDistance
        SplitContainer2.SplitterDistance = SplitContainer2.Width - 27
        m_rightPaneControl.Visible = False
        m_rightPaneExpandBT.Visible = True

    End Sub

    Private Sub CollapseSP1Pane1()

        SplitContainer1.SplitterDistance = 25
        m_leftPaneControl.Visible = False
        m_leftPaneExpandBT.Visible = True

    End Sub

    Private Sub ExpandSP1Pane1()

        SplitContainer1.SplitterDistance = m_SplitContainer1Distance
        SplitContainer1.Panel1.Show()
        m_leftPaneControl.Visible = True
        m_leftPaneExpandBT.Visible = False

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
            m_rightPaneControl.UpdateBT.Enabled = p_state
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
            For Each versionId In VTreeViewUtil.GetCheckedNodesIds(m_leftPaneControl.versionsTV)
                Dim version As Version = GlobalVariables.Versions.GetValue(versionId)
                If version Is Nothing Then Continue For
                If version.IsFolder = False Then
                    versionsIds.Add(versionId)
                End If
            Next

            If m_currentEntityNode Is Nothing Then
                If m_leftPaneControl.entitiesTV.Nodes.Count > 0 Then
                    m_currentEntityNode = m_leftPaneControl.entitiesTV.Nodes(0)
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

    Delegate Sub DisplayLogAttemp_Delegate(p_logValuesHt As List(Of FactLog))
    Private Sub DisplayLog_ThreadSafe(p_logValuesHt As List(Of FactLog))

        If InvokeRequired Then
            Dim MyDelegate As New DisplayLogAttemp_Delegate(AddressOf DisplayLog_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {p_logValuesHt})
        Else
            m_logView.DisplayLogValues(p_logValuesHt)
        End If

    End Sub

    Delegate Sub ReloadAccountsTV_Delegate()
    Friend Sub ReloadAccountsTV_ThreadSafe()

        If Me.m_accountsTreeview.InvokeRequired Then
            Dim MyDelegate As New ReloadAccountsTV_Delegate(AddressOf ReloadAccountsTV_ThreadSafe)
            Me.m_accountsTreeview.Invoke(MyDelegate, New Object() {})
        Else
            GlobalVariables.Accounts.LoadAccountsTV(m_accountsTreeview)
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


#Region "Utilities"

    Friend Function FilterPeriodList(ByRef p_periods As Int32()) As List(Of Int32)

        Dim l_periods As New List(Of Int32)
        For Each l_periodId In p_periods
            If l_periodId >= m_periodsList(0) Then
                If l_periodId <= m_periodsList(m_periodsList.Count - 1) Then
                    l_periods.Add(l_periodId)
                Else
                    Return l_periods
                End If
            End If
        Next
        Return l_periods

    End Function

#End Region

End Class
