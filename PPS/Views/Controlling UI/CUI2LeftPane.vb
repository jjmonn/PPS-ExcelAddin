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
' Last modified: 07/11/2015


Public Class CUI2LeftPane


#Region "Instance variables"

    ' Objects
    Private ExpandSelectionPaneBT As New vButton
    Friend entitiesTV As New vTreeView
    Friend entitiesFiltersTV As New vTreeView
    Friend adjustmentsTV As New vTreeView
    Friend adjustmentsFiltersTV As New vTreeView
    Friend clientsTV As New vTreeView
    Friend clientsFiltersTV As New vTreeView
    Friend productsTV As New vTreeView
    Friend productsFiltersTV As New vTreeView
    Friend versionsTV As New vTreeView
    Friend periodsTV As New vTreeView
    Friend currenciesCLB As New vRadioListBox

    ' Variables
    Private selectionPaneExpandedFlag As Boolean
    Private SPDistance As Single = 250

    ' Constants
    Private Const ENTITIES_FILTERS_STR As String = "Entities Categories"
    Private Const CLIENTS_STR As String = "Clients"
    Private Const CLIENTS_FILTERS_STR As String = "Clients Categories"
    Private Const PRODUCTS_STR As String = "Products"
    Private Const PRODUCTS_FILTERS_STR As String = "Products Categories"
    Private Const ADJUSTMENTS_STR As String = "Adjustments "
    Private Const ADJUSTMENTS_FILTERS_STR As String = "Adjustments Categories"
    Private Const PERIODS_STR As String = "Periods"
    Private Const VERSIONS_STR As String = "Versions"
    Private Const CURRENCIES_STR As String = "Currencies"

#End Region


#Region "Initialize"

    ' init tv here and belong here ?! priority high

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        SplitContainer.Panel1.Controls.Add(entitiesTV)
        entitiesTV.Dock = DockStyle.Fill
        entitiesTV.CheckBoxes = True

        LoadTvs()
        InitCurrenciesCLB()
        InitSelectionCB()
        InitButton()
        MultilangueSetup()

    End Sub

    Private Sub LoadTvs()

        GlobalVariables.AxisElems.LoadEntitiesTV(entitiesTV)
        GlobalVariables.AxisElems.LoadAxisTree(AxisType.Client, clientsTV)
        GlobalVariables.AxisElems.LoadAxisTree(AxisType.Product, productsTV)
        GlobalVariables.AxisElems.LoadAxisTree(AxisType.Adjustment, adjustmentsTV)

        AxisFilterManager.LoadFvTv(entitiesFiltersTV, GlobalEnums.AnalysisAxis.ENTITIES)
        AxisFilterManager.LoadFvTv(clientsFiltersTV, GlobalEnums.AnalysisAxis.CLIENTS)
        AxisFilterManager.LoadFvTv(productsFiltersTV, GlobalEnums.AnalysisAxis.PRODUCTS)
        AxisFilterManager.LoadFvTv(adjustmentsFiltersTV, GlobalEnums.AnalysisAxis.ADJUSTMENTS)

        VTreeViewUtil.CheckStateAllNodes(entitiesTV, True)
        VTreeViewUtil.CheckStateAllNodes(clientsTV, True)
        VTreeViewUtil.CheckStateAllNodes(productsTV, True)
        VTreeViewUtil.CheckStateAllNodes(adjustmentsTV, True)
        VTreeViewUtil.CheckStateAllNodes(entitiesFiltersTV, True)
        VTreeViewUtil.CheckStateAllNodes(clientsFiltersTV, True)
        VTreeViewUtil.CheckStateAllNodes(productsFiltersTV, True)
        VTreeViewUtil.CheckStateAllNodes(adjustmentsFiltersTV, True)

        entitiesFiltersTV.TriStateMode = True
        clientsFiltersTV.TriStateMode = True
        productsFiltersTV.TriStateMode = True
        adjustmentsFiltersTV.TriStateMode = True

        GlobalVariables.Versions.LoadVersionsTV(versionsTV)

        VTreeViewUtil.InitTVFormat(entitiesTV)
        VTreeViewUtil.InitTVFormat(clientsTV)
        VTreeViewUtil.InitTVFormat(productsTV)
        VTreeViewUtil.InitTVFormat(adjustmentsTV)
        VTreeViewUtil.InitTVFormat(versionsTV)
        VTreeViewUtil.InitTVFormat(entitiesFiltersTV)
        VTreeViewUtil.InitTVFormat(clientsFiltersTV)
        VTreeViewUtil.InitTVFormat(productsFiltersTV)
        VTreeViewUtil.InitTVFormat(periodsTV)
        VTreeViewUtil.SeEntitiesTVImageIndexes(entitiesTV)

        InitTV(entitiesFiltersTV)
        InitTV(clientsTV)
        InitTV(clientsFiltersTV)
        InitTV(productsTV)
        InitTV(productsFiltersTV)
        InitTV(adjustmentsTV)
        InitTV(adjustmentsFiltersTV)
        InitTV(versionsTV)
        InitTV(periodsTV)

        versionsTV.ImageList = VersionsIL
        entitiesTV.ImageList = EntitiesTVImageList

    End Sub

    Private Sub InitSelectionCB()

        SelectionCB.Items.Add(ENTITIES_FILTERS_STR)
        SelectionCB.Items.Add(CLIENTS_STR)
        SelectionCB.Items.Add(CLIENTS_FILTERS_STR)
        SelectionCB.Items.Add(PRODUCTS_STR)
        SelectionCB.Items.Add(PRODUCTS_FILTERS_STR)
        SelectionCB.Items.Add(ADJUSTMENTS_STR)
        SelectionCB.Items.Add(ADJUSTMENTS_FILTERS_STR)
        SelectionCB.Items.Add(PERIODS_STR)
        SelectionCB.Items.Add(VERSIONS_STR)
        SelectionCB.Items.Add(CURRENCIES_STR)

    End Sub

    Private Sub InitTV(ByRef TV As vTreeView)

        SelectionTVTableLayout.Controls.Add(TV, 0, 1)
        TV.Dock = DockStyle.Fill
        TV.CheckBoxes = True
        TV.Visible = False

    End Sub

    Private Sub InitCurrenciesCLB()

        For Each currency As Currency In GlobalVariables.Currencies.GetDictionary().Values
            Dim li As New ListItem
            li.Value = currency.Id
            li.Text = currency.Name
            currenciesCLB.Items.Add(li)
            If li.Value = My.Settings.currentCurrency Then
                li.IsChecked = True
                currenciesCLB.SelectedItem = li
            End If
        Next

        SelectionTVTableLayout.Controls.Add(currenciesCLB)
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

    Private Sub MultilangueSetup()

        Me.SelectionCB.Text = Local.GetValue("CUI.selection")
        Me.m_entitySelectionLabel.Text = Local.GetValue("CUI.entities_selection")


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


#End Region


#Region "Events and Call Backs"

    Private Sub SelectionCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles SelectionCB.SelectedValueChanged

        HideAllTVs()
        Select Case SelectionCB.SelectedItem.Text
            Case ENTITIES_FILTERS_STR : entitiesFiltersTV.Visible = True
            Case CLIENTS_STR : clientsTV.Visible = True
            Case CLIENTS_FILTERS_STR : clientsFiltersTV.Visible = True
            Case PRODUCTS_STR : productsTV.Visible = True
            Case PRODUCTS_FILTERS_STR : productsFiltersTV.Visible = True
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

#End Region


#Region "Utilities"

    Private Sub ExpandSelectionPane()

        SplitContainer.SplitterDistance = SPDistance
        ExpandSelectionPaneBT.Visible = False
        SelectionTVTableLayout.Visible = True

    End Sub

    Private Sub CollapseSelectionPane()

        SPDistance = SplitContainer.SplitterDistance
        SplitContainer.SplitterDistance = SplitContainer.Height
        SelectionTVTableLayout.Visible = False
        ExpandSelectionPaneBT.Visible = True

    End Sub

    Private Sub HideAllTVs()

        Me.clientsTV.Visible = False
        Me.clientsFiltersTV.Visible = False
        Me.productsTV.Visible = False
        Me.productsFiltersTV.Visible = False
        Me.adjustmentsTV.Visible = False
        Me.adjustmentsFiltersTV.Visible = False
        Me.versionsTV.Visible = False
        Me.periodsTV.Visible = False
        Me.currenciesCLB.Visible = False

    End Sub

#End Region



End Class
