Imports VIBlend.WinForms.Controls
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports CRUD

' CUI2LeftPane.vb
'
' Control for entities and other selections
'
'
'
'
'
' Created on : 15/08/2015
' Last modified: 14/01/2016


Public Class CUI2LeftPane


#Region "Instance variables"

    ' Objects
    Private ExpandSelectionPaneBT As New vButton
    Friend entitiesTV As New vTreeView
    Friend entitiesFiltersTV As New vTreeView
    Friend adjustmentsTV As New vTreeView
    Friend adjustmentsFiltersTV As New vTreeView
    Friend m_clientsTV As New vTreeView
    Friend clientsFiltersTV As New vTreeView
    Friend productsTV As New vTreeView
    Friend productsFiltersTV As New vTreeView
    Friend m_employeesFiltersTV As vTreeView
    Friend versionsTV As New vTreeView
    Friend periodsTV As New vTreeView
    Friend currenciesCLB As New vRadioListBox
    Private m_periodSelection As PeriodRangeSelectionControl

    ' Variables
    Private selectionPaneExpandedFlag As Boolean
    Private SPDistance As Single = 250

    ' Constants
    Private ENTITIES_FILTERS_STR As String = Local.GetValue("general.entities_filters")
    Private CLIENTS_STR As String = Local.GetValue("general.clients")
    Private CLIENTS_FILTERS_STR As String = Local.GetValue("general.clients_filters")
    Private PRODUCTS_STR As String = Local.GetValue("general.products")
    Private PRODUCTS_FILTERS_STR As String = Local.GetValue("general.products_filters")
    Private ADJUSTMENTS_STR As String = Local.GetValue("general.adjustments")
    Private ADJUSTMENTS_FILTERS_STR As String = Local.GetValue("general.adjustments_filters")
    Private EMPLOYEES_FILTERS_STR As String = Local.GetValue("general.employees_filters")
    Private PERIODS_STR As String = Local.GetValue("general.periods")
    Private VERSIONS_STR As String = Local.GetValue("general.versions")
    Private CURRENCIES_STR As String = Local.GetValue("general.currencies")
    Private Const PERIOD_SELECTION_HEIGHT As Single = 147

#End Region


#Region "Initialize"

    ' init tv here and belong here ?! priority high

    Public Sub New(ByRef p_process As CRUD.Account.AccountProcess)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SplitContainer.Panel1.Controls.Add(entitiesTV)
        entitiesTV.Dock = DockStyle.Fill
        entitiesTV.CheckBoxes = True
        entitiesTV.TriStateMode = True

        LoadTvs(p_process)
        InitCurrenciesCLB()
        InitSelectionCB(p_process)
        InitButton()
        MultilangueSetup()
        m_selectionTableLayout.BackColor = Drawing.Color.White

        If p_process = Account.AccountProcess.RH Then
            InitPeriodRangeSelection()
        End If

    End Sub

    Private Sub LoadTvs(ByRef p_process As Account.AccountProcess)

        GlobalVariables.AxisElems.LoadEntitiesTV(entitiesTV)
        GlobalVariables.AxisElems.LoadHierarchyAxisTree(AxisType.Client, m_clientsTV) 'LoadAxisTreeOnlyFirstHierarchyLevel
        GlobalVariables.AxisElems.LoadAxisTree(AxisType.Product, productsTV)
        GlobalVariables.AxisElems.LoadAxisTree(AxisType.Adjustment, adjustmentsTV)

        AxisFilterManager.LoadFvTv(entitiesFiltersTV, GlobalEnums.AnalysisAxis.ENTITIES)
        AxisFilterManager.LoadFvTv(clientsFiltersTV, GlobalEnums.AnalysisAxis.CLIENTS)
        AxisFilterManager.LoadFvTv(productsFiltersTV, GlobalEnums.AnalysisAxis.PRODUCTS)
        AxisFilterManager.LoadFvTv(adjustmentsFiltersTV, GlobalEnums.AnalysisAxis.ADJUSTMENTS)

        VTreeViewUtil.CheckStateAllNodes(entitiesTV, CheckState.Checked)
        VTreeViewUtil.CheckStateAllNodes(m_clientsTV, CheckState.Checked)
        VTreeViewUtil.CheckStateAllNodes(productsTV, CheckState.Checked)
        VTreeViewUtil.CheckStateAllNodes(adjustmentsTV, CheckState.Checked)
        VTreeViewUtil.CheckStateAllNodes(entitiesFiltersTV, CheckState.Checked)
        VTreeViewUtil.CheckStateAllNodes(clientsFiltersTV, CheckState.Checked)
        VTreeViewUtil.CheckStateAllNodes(productsFiltersTV, CheckState.Checked)
        VTreeViewUtil.CheckStateAllNodes(adjustmentsFiltersTV, CheckState.Checked)

        entitiesFiltersTV.TriStateMode = True
        clientsFiltersTV.TriStateMode = True
        productsFiltersTV.TriStateMode = True
        adjustmentsFiltersTV.TriStateMode = True

        GlobalVariables.Versions.LoadVersionsTV(versionsTV)

        VTreeViewUtil.InitTVFormat(entitiesTV)
        VTreeViewUtil.InitTVFormat(m_clientsTV)
        VTreeViewUtil.InitTVFormat(productsTV)
        VTreeViewUtil.InitTVFormat(adjustmentsTV)
        VTreeViewUtil.InitTVFormat(versionsTV)
        VTreeViewUtil.InitTVFormat(entitiesFiltersTV)
        VTreeViewUtil.InitTVFormat(clientsFiltersTV)
        VTreeViewUtil.InitTVFormat(productsFiltersTV)
        VTreeViewUtil.InitTVFormat(periodsTV)
        VTreeViewUtil.SeEntitiesTVImageIndexes(entitiesTV)

        InitTV(entitiesFiltersTV)
        InitTV(m_clientsTV)
        InitTV(clientsFiltersTV)
        InitTV(productsTV)
        InitTV(productsFiltersTV)
        InitTV(adjustmentsTV)
        InitTV(adjustmentsFiltersTV)
        InitTV(versionsTV)
        InitTV(periodsTV)

        m_clientsTV.ContextMenuStrip = m_rightClickMenu
        productsTV.ContextMenuStrip = m_rightClickMenu
        versionsTV.ContextMenuStrip = m_rightClickMenu
        entitiesFiltersTV.ContextMenuStrip = m_rightClickMenu
        clientsFiltersTV.ContextMenuStrip = m_rightClickMenu
        productsFiltersTV.ContextMenuStrip = m_rightClickMenu
        versionsTV.ImageList = m_versionsTreeviewImageList
        entitiesTV.ImageList = EntitiesTVImageList

        If p_process = Account.AccountProcess.RH Then
            m_employeesFiltersTV = New vTreeView
            VTreeViewUtil.InitTVFormat(m_employeesFiltersTV)
            AxisFilterManager.LoadFvTv(m_employeesFiltersTV, GlobalEnums.AnalysisAxis.EMPLOYEES)
            VTreeViewUtil.CheckStateAllNodes(m_employeesFiltersTV, CheckState.Checked)
            m_employeesFiltersTV.TriStateMode = True
            InitTV(m_employeesFiltersTV)
            m_employeesFiltersTV.ContextMenuStrip = m_rightClickMenu
        End If

    End Sub

    Private Sub InitSelectionCB(ByRef p_process As Account.AccountProcess)

        SelectionCB.Items.Add(ENTITIES_FILTERS_STR)
        SelectionCB.Items.Add(CLIENTS_STR)
        SelectionCB.Items.Add(CLIENTS_FILTERS_STR)
        SelectionCB.Items.Add(PRODUCTS_STR)
        SelectionCB.Items.Add(PRODUCTS_FILTERS_STR)
        If p_process = Account.AccountProcess.RH Then
            SelectionCB.Items.Add(EMPLOYEES_FILTERS_STR)
        End If
        SelectionCB.Items.Add(ADJUSTMENTS_STR)
        SelectionCB.Items.Add(ADJUSTMENTS_FILTERS_STR)
        SelectionCB.Items.Add(PERIODS_STR)
        SelectionCB.Items.Add(VERSIONS_STR)
        If p_process = Account.AccountProcess.FINANCIAL Then
            SelectionCB.Items.Add(CURRENCIES_STR)
        End If

    End Sub

    Private Sub InitTV(ByRef p_treeview As vTreeView)

        m_selectionTableLayout.Controls.Add(p_treeview, 0, 1)
        p_treeview.Dock = DockStyle.Fill
        p_treeview.CheckBoxes = True
        p_treeview.Visible = False

    End Sub

    Private Sub InitCurrenciesCLB()

        For Each currency As Currency In GlobalVariables.Currencies.GetDictionary().Values
            If currency.InUse = True Then
                Dim li As New ListItem
                li.Value = currency.Id
                li.Text = currency.Name
                currenciesCLB.Items.Add(li)
                If li.Value = My.Settings.currentCurrency Then
                    li.IsChecked = True
                    currenciesCLB.SelectedItem = li
                End If
            End If
        Next

        m_selectionTableLayout.Controls.Add(currenciesCLB)
        currenciesCLB.Dock = DockStyle.Fill
        currenciesCLB.CheckOnClick = True
        currenciesCLB.SelectionMode = SelectionMode.One
        currenciesCLB.Visible = False

    End Sub

    Private Sub InitButton()

        ExpandSelectionPaneBT.Width = 19
        ExpandSelectionPaneBT.Height = 19
        ExpandSelectionPaneBT.ImageList = ExpansionImageList
        ExpandSelectionPaneBT.ImageIndex = 0
        ExpandSelectionPaneBT.Text = ""
        ExpandSelectionPaneBT.PaintBorder = False
        ExpandSelectionPaneBT.FlatStyle = FlatStyle.Flat
        ExpandSelectionPaneBT.Visible = False
        AddHandler ExpandSelectionPaneBT.Click, AddressOf ExpandSelectionBT_Click
        SplitContainer.Panel2.Controls.Add(ExpandSelectionPaneBT)

    End Sub

    Private Sub InitPeriodRangeSelection()

        m_selectionTableLayout.RowStyles.Add(New RowStyle(SizeType.Absolute, PERIOD_SELECTION_HEIGHT))
        m_selectionTableLayout.RowCount += 1
        m_periodSelection = New PeriodRangeSelectionControl(My.Settings.version_id)
        m_selectionTableLayout.Controls.Add(m_periodSelection, 0, m_selectionTableLayout.RowCount - 1)
        m_periodSelection.Dock = DockStyle.Fill
        m_periodSelection.BackColor = Drawing.Color.White
        m_periodSelection.BorderStyle = Windows.Forms.BorderStyle.FixedSingle

    End Sub

    Private Sub MultilangueSetup()

        Me.SelectionCB.Text = Local.GetValue("CUI.selection")
        Me.m_entitySelectionLabel.Text = Local.GetValue("CUI.entities_selection")
        Me.SelectAllToolStripMenuItem.Text = Local.GetValue("CUI.select_all")
        Me.UnselectAllToolStripMenuItem.Text = Local.GetValue("CUI.unselect_all")

    End Sub

#End Region


#Region "Interface"

    Delegate Sub SetupPeriodsTVDoneAttemp_Delegate(ByRef periodsNodes As vTreeNode)
    Friend Sub SetupPeriodsTV(ByRef periodsNodes As vTreeNode)

        If InvokeRequired Then
            Dim MyDelegate As New SetupPeriodsTVDoneAttemp_Delegate(AddressOf SetupPeriodsTV)
            Me.Invoke(MyDelegate, New Object() {periodsNodes})
        Else
            periodsTV.Nodes.Clear()
            For Each node As vTreeNode In periodsNodes.Nodes
                VTreeViewUtil.CopySubNodes(node, periodsTV)
            Next
        End If

    End Sub

    Friend Function GetRHPeriodSelection() As List(Of Int32)

        If m_periodSelection IsNot Nothing Then
            Return m_periodSelection.GetPeriodList
        Else
            Return Nothing
        End If

    End Function

#End Region


#Region "Events and Call Backs"

    Private Sub SelectionCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles SelectionCB.SelectedValueChanged

        HideAllTVs()
        Select Case SelectionCB.SelectedItem.Text
            Case ENTITIES_FILTERS_STR : entitiesFiltersTV.Visible = True
            Case CLIENTS_STR : m_clientsTV.Visible = True
            Case CLIENTS_FILTERS_STR : clientsFiltersTV.Visible = True
            Case PRODUCTS_STR : productsTV.Visible = True
            Case PRODUCTS_FILTERS_STR : productsFiltersTV.Visible = True
            Case EMPLOYEES_FILTERS_STR : m_employeesFiltersTV.Visible = True
            Case ADJUSTMENTS_STR : adjustmentsTV.Visible = True
            Case ADJUSTMENTS_FILTERS_STR : adjustmentsFiltersTV.Visible = True
            Case PERIODS_STR : periodsTV.Visible = True
            Case VERSIONS_STR : versionsTV.Visible = True
            Case CURRENCIES_STR : currenciesCLB.Visible = True
        End Select

    End Sub

    Private Sub CollapseSelectionBT_Click(sender As Object, e As EventArgs) Handles CollapseSelectionBT.Click

        CollapseSelectionPane()

    End Sub

    Private Sub ExpandSelectionBT_Click(sender As Object, e As EventArgs)

        ExpandSelectionPane()

    End Sub

    Private Sub SelectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelectAllToolStripMenuItem.Click
        Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
        Dim l_treeview As vTreeView = CType(cms.SourceControl, vTreeView)
        VTreeViewUtil.CheckStateAllNodes(l_treeview, CheckState.Checked)
    End Sub

    Private Sub UnselectAllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UnselectAllToolStripMenuItem.Click
        Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
        Dim l_treeview As vTreeView = CType(cms.SourceControl, vTreeView)
        VTreeViewUtil.CheckStateAllNodes(l_treeview, CheckState.Unchecked)
    End Sub

#End Region


#Region "Utilities"

    Private Sub ExpandSelectionPane()

        SplitContainer.SplitterDistance = SPDistance
        ExpandSelectionPaneBT.Visible = False
        m_selectionTableLayout.Visible = True

    End Sub

    Private Sub CollapseSelectionPane()

        SPDistance = SplitContainer.SplitterDistance
        SplitContainer.SplitterDistance = SplitContainer.Height
        m_selectionTableLayout.Visible = False
        ExpandSelectionPaneBT.Visible = True

    End Sub

    Private Sub HideAllTVs()

        Me.m_clientsTV.Visible = False
        Me.clientsFiltersTV.Visible = False
        Me.productsTV.Visible = False
        Me.productsFiltersTV.Visible = False
        Me.adjustmentsTV.Visible = False
        Me.adjustmentsFiltersTV.Visible = False
        If Me.m_employeesFiltersTV IsNot Nothing Then Me.m_employeesFiltersTV.Visible = False
        Me.versionsTV.Visible = False
        Me.periodsTV.Visible = False
        If Me.currenciesCLB IsNot Nothing Then Me.currenciesCLB.Visible = False

    End Sub

#End Region


End Class
