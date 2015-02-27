' PPSBI_UI.VB
' 
' User interface for the construction of PPSBI functions
'
' To do:
'       >> highest hiearachy conso level or stay in aggregation queries with fx conversion in vb !!!!!)
'
'
'       - Check if already a formula when opening: in this case fill TB with current values
'       - Add possibility to design lines and columns off data
'     
' Quid: do the categories have sub categories...?
'
'
' Last modified: 25/02/2015 
' Author: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections



Friend Class PPSBI_UI


#Region "Instance Variables"

#Region "Objects"

    Private entitiesTV As New TreeView
    Private accountsTV As New TreeView
    Private versionsTV As New TreeView
    Private categoriesTV As New TreeView

#End Region

#Region "Variables"

    Private versions_name_id As Hashtable
    Private currentSelection As String
    Private destination As Excel.Range
    Private periodsStrIntDictionary As New Dictionary(Of String, Integer)
    Private categoriesFilterStr As String
    Private isRightSideExpanded As Boolean = False
    Private expandedControlWidth As Int32
    Private categoriesTabControlWidth As Int32

#End Region

#Region "Constants"

    Private Const ENTITIES_SELECTION As String = "entSel"
    Private Const ACCOUNTS_SELECTION As String = "accSel"
    Private Const VERSIONS_SELECTION As String = "verSel"
    Private Const NON_EXPANDED_CONTROL_WIDTH As Integer = 600
    Private Const EXPANDED_CONTROL_HEIGHT As Integer = 620
    Private Const NON_EXPANDED_CONTROL_HEIGHT As Integer = 470
    Private Const EXPANSION_CONTROL_MARGIN As Integer = 30
    Private Const EXPANDED_IMAGE_INDEX As Int32 = 1
    Private Const COLLAPSED_IMAGE_INDEX As Int32 = 0
    Private Const AVERAGE_LETTER_SIZE As Int32 = 10

#End Region

#End Region


#Region "Initialization"

    Protected Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        destination = GlobalVariables.apps.ActiveCell

        Dim currencies As List(Of String) = CurrenciesMapping.getCurrenciesList(CURRENCIES_KEY_VARIABLE)
        For Each currency_ As String In currencies
            CurrencyCB.Items.Add(currency_)
        Next

        CurrencyCB.SelectedItem = MAIN_CURRENCY
        TreeviewsInitialization()
        InitializeAdjustmentsCB()
        versionsTB.Text = versionsTV.Nodes.Find(GlobalVariables.GLOBALCurrentVersionCode, True)(0).Text
        InitializeTimePeriodsSelection(GlobalVariables.GLOBALCurrentVersionCode)
        CategoriesControlTabInitialization()

    End Sub

    Private Sub InitializeTimePeriodsSelection(ByRef versionCode As String)

        periodsStrIntDictionary.Clear()
        PeriodCB.Items.Clear()
        Dim Versions As New Version
        ' may be improved -> anyway module to reimplement to efficiently compute what is needed!
      
        Dim strFormat As String = ""
        Select Case Versions.ReadVersion(versionCode, VERSIONS_TIME_CONFIG_VARIABLE)
            Case MONTHLY_TIME_CONFIGURATION : strFormat = "MMM yyyy"
            Case YEARLY_TIME_CONFIGURATION : strFormat = "yyyy"
        End Select

        For Each period As Integer In Versions.GetPeriodList(versionCode)
            PeriodCB.Items.Add(Format(DateTime.FromOADate(period), strFormat))
            periodsStrIntDictionary.Add(Format(DateTime.FromOADate(period), strFormat), period)
        Next
        PeriodCB.SelectedIndex = Nothing

    End Sub

    Private Sub TreeviewsInitialization()

        ' Entities TreeView
        entitiesTV.ImageList = EntitiesTVImageList
        Entity.LoadEntitiesTree(entitiesTV)
        TreeViewsUtilities.set_TV_basics_icon_index(entitiesTV)
        entitiesTV.CollapseAll()
        entitiesTV.Dock = DockStyle.Fill
        AddHandler entitiesTV.KeyDown, AddressOf entitiesTV_KeyDown
        AddHandler entitiesTV.NodeMouseDoubleClick, AddressOf entitiesTV_NodeMouseClick

        ' Accounts
        accountsTV.ImageList = accountsIL
        Account.LoadAccountsTree(accountsTV)
        accountsTV.CollapseAll()
        accountsTV.Dock = DockStyle.Fill
        AddHandler accountsTV.KeyDown, AddressOf accountsTV_KeyDown
        AddHandler accountsTV.NodeMouseDoubleClick, AddressOf accountsTV_NodeMouseClick

        ' Versions
        versionsTV.ImageList = VersionsTVIcons
        Version.LoadVersionsTree(versionsTV)
        versions_name_id = VersionsMapping.GetVersionsHashTable(VERSIONS_NAME_VARIABLE, RATES_VERSIONS_ID_VARIABLE)
        versionsTV.CollapseAll()
        versionsTV.Dock = DockStyle.Fill
        AddHandler versionsTV.KeyDown, AddressOf versionsTV_KeyDown
        AddHandler versionsTV.NodeMouseDoubleClick, AddressOf versionsTV_NodeMouseClick

        'Categories
        Category.LoadCategoriesTree(categoriesTV)

        Panel1.Controls.Add(entitiesTV)
        Panel2.Controls.Add(accountsTV)
        Panel3.Controls.Add(versionsTV)

    End Sub

    Private Sub InitializeAdjustmentsCB()

        AdjustmentCB.Items.Add("")
        For Each adjustment_name As String In AdjustmentsMapping.GetAdjustmentsIDsList(ADJUSTMENTS_NAME_VAR)
            AdjustmentCB.Items.Add(adjustment_name)
        Next

    End Sub

    Private Sub CategoriesControlTabInitialization()

        For Each node As TreeNode In categoriesTV.Nodes

            categoriesTabControlWidth = categoriesTabControlWidth + node.Text.Length * AVERAGE_LETTER_SIZE
            CategoriesTVsTabControl.TabPages.Add(node.Name, node.Text)
            Dim catTV As New TreeView
            catTV.CheckBoxes = True
            catTV.Dock = DockStyle.Fill
            AddHandler catTV.AfterCheck, AddressOf CategoriesTVs_AfterCheck

            For Each subNode As TreeNode In node.Nodes
                catTV.Nodes.Add(subNode.Name, subNode.Text)
            Next
            CategoriesTVsTabControl.TabPages(CategoriesTVsTabControl.TabPages.IndexOfKey(node.Name)).Controls.Add(catTV)

        Next
        expandedControlWidth = NON_EXPANDED_CONTROL_WIDTH + categoriesTabControlWidth + EXPANSION_CONTROL_MARGIN
        categoriesSelectionGroupBox.Width = categoriesTabControlWidth + 2
        
    End Sub

    Private Sub PPSBI_UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        MainTVsSelectionGB.Hide()
        Panel1.Hide()
        Panel2.Hide()
        Panel3.Hide()
        Me.Width = NON_EXPANDED_CONTROL_WIDTH

    End Sub

#End Region


#Region "Actions"

    Private Sub validate_cmd_Click(sender As Object, e As EventArgs) Handles validate_cmd.Click

        ' check before sending ?
        ' check that one thing has been selected at least for everything !!

        Dim periodStr As String = (Format(DateTime.FromOADate(periodsStrIntDictionary(PeriodCB.Text)), "Short date"))
        Dim strFormula = "=" + UDF_FORMULA_GET_DATA_NAME + "(" + Chr(34) + entitiesTB.Text + Chr(34) + "," _
                                                                + Chr(34) + accountsTB.Text + Chr(34) + "," _
                                                                + Chr(34) + periodStr + Chr(34) + "," _
                                                                + Chr(34) + CurrencyCB.Text + Chr(34) + "," _
                                                                + Chr(34) + versionsTB.Text + Chr(34)
        If AdjustmentCB.Text <> "" Then
            strFormula = strFormula + "," + Chr(34) + AdjustmentCB.Text + Chr(34)
        Else
            strFormula = strFormula + "," + AdjustmentCB.Text
        End If

        If categoriesFilterStr <> "" Then
            strFormula = strFormula + "," + Chr(34) + categoriesFilterStr + Chr(34) + ")"
        Else
            strFormula = strFormula + ")"
        End If
        destination.Formula = strFormula
        Me.Close()

    End Sub

#Region "Main Selections Validation"

    Private Sub ValidateEntitySelection(ByRef entityName As String)

        entitiesTB.Text = entityName
        ShowCategoriesSelection()
        CollapseRightPane()
        accountsTB.Select()

    End Sub

    Private Sub ValidateAccountSelection(ByRef accountName As String)

        accountsTB.Text = accountName
        ShowCategoriesSelection()
        CollapseRightPane()
   
    End Sub

    Private Sub ValidateVersionSelection(ByRef versionCode As String, ByRef versionName As String)

        versionsTB.Text = versionName
        InitializeTimePeriodsSelection(versionCode)
        ShowCategoriesSelection()
        CollapseRightPane()
        PeriodCB.Select()

    End Sub

#End Region


#End Region


#Region "Events"

#Region "Text Boxes Events"

    Private Sub Entity_TB_Enter(sender As Object, e As EventArgs) Handles entitiesTB.Enter

        ShowEntitiesSelectionTree()
        entitiesTV.Select()
        If Not entitiesTV.Nodes(0) Is Nothing Then entitiesTV.SelectedNode = entitiesTV.Nodes(0)

    End Sub

    Private Sub accountsTB_Enter(sender As Object, e As EventArgs) Handles accountsTB.Enter

        ShowAccountsSelectionTree()
        accountsTV.Select()
        If Not accountsTV.Nodes(0) Is Nothing Then accountsTV.SelectedNode = accountsTV.Nodes(0)

    End Sub

    Private Sub versionsTB_Enter(sender As Object, e As EventArgs) Handles versionsTB.Enter

        ShowVersionsSelectionTree()
        versionsTV.Select()
        If Not versionsTV.Nodes(0) Is Nothing Then versionsTV.SelectedNode = versionsTV.Nodes(0)

    End Sub

#End Region

#Region "SelectionButtons Events"

    Private Sub entitiesClick(sender As Object, e As EventArgs) Handles entityLabel.Click, selectEntity.Click
        ShowEntitiesSelectionTree()
    End Sub

    Private Sub accountsClick(sender As Object, e As EventArgs) Handles accountLabel.Click, accountSelect.Click
        ShowAccountsSelectionTree()
    End Sub

    Private Sub VersionSelectionBT_Click(sender As Object, e As EventArgs) Handles VersionSelectionBT.Click, versionLabel.Click
        ShowVersionsSelectionTree()
    End Sub

#End Region

#Region "Categories Filters Events"

    Private Sub CategoriesTVs_AfterCheck(sender As Object, e As TreeViewEventArgs)

        FilterStringSelectionBuilding()
      
    End Sub

#End Region


#Region "Entities, Accounts and Version Treeviews Call backs"

#Region "Key Downs"

    Private Sub entitiesTV_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter _
        AndAlso Not entitiesTV.SelectedNode Is Nothing Then ValidateEntitySelection(entitiesTV.SelectedNode.Text)

    End Sub

    Private Sub accountsTV_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter _
        AndAlso Not accountsTV.SelectedNode Is Nothing Then ValidateAccountSelection(accountsTV.SelectedNode.Text)

    End Sub

    Private Sub versionsTV_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter _
        AndAlso Not versionsTV.SelectedNode Is Nothing _
        AndAlso versions_name_id.ContainsValue(versionsTV.SelectedNode.Name) Then _
            ValidateVersionSelection(versionsTV.SelectedNode.Name, versionsTV.SelectedNode.Text)

    End Sub

#End Region

#Region "Nodes Double Clicks"

    Private Sub entitiesTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        If Not entitiesTV.SelectedNode Is Nothing Then
            Select Case entitiesTV.SelectedNode.Nodes.Count
                Case 0 : ValidateEntitySelection(entitiesTV.SelectedNode.Text)
                    ' Case Else : If entitiesTV.SelectedNode.IsExpanded = True Then ValidateEntitySelection(entitiesTV.SelectedNode.Text)
            End Select
        End If

    End Sub

    Private Sub accountsTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        If Not accountsTV.SelectedNode Is Nothing Then
            Select Case accountsTV.SelectedNode.Nodes.Count
                Case 0 : ValidateAccountSelection(accountsTV.SelectedNode.Text)
                    'Case Else : If accountsTV.SelectedNode.IsExpanded = True Then ValidateAccountSelection(accountsTV.SelectedNode.Text)
            End Select
        End If

    End Sub

    Private Sub versionsTV_NodeMouseClick(sender As Object, e As Windows.Forms.TreeNodeMouseClickEventArgs)

        If Not versionsTV.SelectedNode Is Nothing _
        AndAlso versions_name_id.ContainsValue(versionsTV.SelectedNode.Name) Then
            Select Case versionsTV.SelectedNode.Nodes.Count
                Case 0 : ValidateVersionSelection(versionsTV.SelectedNode.Name, versionsTV.SelectedNode.Text)
                    'Case Else : If versionsTV.SelectedNode.IsExpanded = True Then ValidateVersionSelection(versionsTV.SelectedNode.Name, versionsTV.SelectedNode.Text)
            End Select
        End If

    End Sub

#End Region

#End Region


#End Region


#Region "Menus Management"

#Region "Right Side Display/ Hide"

    Private Sub rightSideEpxansionBT_Click(sender As Object, e As EventArgs) Handles rightSideEpxansionBT.Click

        If isRightSideExpanded = True Then
            CollapseRightPane()
        Else
            ExpandRightPane()
        End If

    End Sub

    Private Sub ExpandRightPane()

        Me.Width = expandedControlWidth
        Me.Height = EXPANDED_CONTROL_HEIGHT
        rightSideEpxansionBT.ImageIndex = EXPANDED_IMAGE_INDEX
        isRightSideExpanded = True

    End Sub

    Private Sub CollapseRightPane()

        Me.Width = NON_EXPANDED_CONTROL_WIDTH
        Me.Height = NON_EXPANDED_CONTROL_HEIGHT
        rightSideEpxansionBT.ImageIndex = COLLAPSED_IMAGE_INDEX
        isRightSideExpanded = False

    End Sub

#End Region

#Region "Main Selections Menu (entities/accounts/versions)"

#Region "Main Selection Group Box Menu (validate/ cancel"

    Private Sub selectionCancelBT_Click(sender As Object, e As EventArgs) Handles selectionCancelBT.Click
        ShowCategoriesSelection()
    End Sub

    Private Sub SelectionValidateBT_Click(sender As Object, e As EventArgs) Handles SelectionValidateBT.Click

        Select Case currentSelection
            Case ENTITIES_SELECTION : If Not entitiesTV.SelectedNode Is Nothing Then ValidateEntitySelection(entitiesTV.SelectedNode.Text)
            Case ACCOUNTS_SELECTION : If Not accountsTV.SelectedNode Is Nothing Then ValidateAccountSelection(accountsTV.SelectedNode.Text)
            Case VERSIONS_SELECTION
                If Not versionsTV.SelectedNode Is Nothing _
                AndAlso versions_name_id.ContainsValue(versionsTV.SelectedNode.Name) Then
                    ValidateVersionSelection(versionsTV.SelectedNode.Name, versionsTV.SelectedNode.Text)
                End If
        End Select

    End Sub

#End Region

#Region "Specific Trees Display"

    Private Sub ShowEntitiesSelectionTree()

        If isRightSideExpanded = False Then ExpandRightPane()
        Panel1.Show()
        currentSelection = ENTITIES_SELECTION
        ShowGB2()

    End Sub

    Private Sub ShowAccountsSelectionTree()

        If isRightSideExpanded = False Then ExpandRightPane()
        Panel2.Show()
        currentSelection = ACCOUNTS_SELECTION
        showGB2()

    End Sub

    Private Sub ShowVersionsSelectionTree()

        If isRightSideExpanded = False Then ExpandRightPane()
        Panel3.Show()
        currentSelection = VERSIONS_SELECTION
        showGB2()

    End Sub

#End Region


#End Region

#End Region


#Region "Utilities"

    Private Sub ShowCategoriesSelection()

        MainTVsSelectionGB.Hide()
        categoriesSelectionGroupBox.Show()

    End Sub

    Private Sub ShowGB2()

        categoriesSelectionGroupBox.Hide()
        MainTVsSelectionGB.Show()

    End Sub

    Private Sub FilterStringSelectionBuilding()

        categoriesFilterStr = ""
        For Each tab_ As TabPage In CategoriesTVsTabControl.TabPages
            Dim tmpTV As TreeView = tab_.Controls(0)
            For Each node As TreeNode In tmpTV.Nodes
                If node.Checked = True Then categoriesFilterStr = categoriesFilterStr + PPSBI_FORMULA_CATEGORIES_SEPARATOR + node.Text
            Next
        Next
        If categoriesFilterStr <> "" Then categoriesFilterStr = Strings.Right(categoriesFilterStr, Len(categoriesFilterStr) - 1)

    End Sub

#End Region



End Class