' FBIFunctionUI.VB
' 
' User interface for the construction of PPSBI functions
'
' 
' Last modified: 11/11/2015 
' Author: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class FBIFunctionUI


#Region "Instance Variables"



#End Region


#Region "Initialize"

    Friend Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        LoadTreeviews()
        InitCurrenciesCheckedListBox()
        PreSelection()
        CategoriesFiltersTreebox.TreeView.EnableMultipleSelection = True
        ClientsTreeviewBox.TreeView.EnableMultipleSelection = True
        ProductsTreeviewBox.TreeView.EnableMultipleSelection = True
        AdjustmentsTreeviewBox.TreeView.EnableMultipleSelection = True


        Dim fSep As Char = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator

    End Sub

    Private Sub LoadTreeviews()

        GlobalVariables.AxisElems.LoadEntitiesTV(EntityTreeBox.TreeView)
        GlobalVariables.Accounts.LoadAccountsTV(AccountTreeBox.TreeView)
        GlobalVariables.Versions.LoadVersionsTV(VersionTreeBox.TreeView)
        GlobalVariables.AxisElems.LoadAxisTree(AxisType.Client, ClientsTreeviewBox.TreeView)
        GlobalVariables.AxisElems.LoadAxisTree(AxisType.Product, ProductsTreeviewBox.TreeView)
        GlobalVariables.AxisElems.LoadAxisTree(AxisType.Adjustment, AdjustmentsTreeviewBox.TreeView)

        EntityTreeBox.DropDownWidth = EntityTreeBox.Width
        AccountTreeBox.DropDownWidth = AccountTreeBox.Width
        VersionTreeBox.DropDownWidth = VersionTreeBox.Width
        CategoriesFiltersTreebox.DropDownWidth = CategoriesFiltersTreebox.Width
        PeriodTreeBox.DropDownWidth = PeriodTreeBox.Width
        ClientsTreeviewBox.DropDownWidth = ClientsTreeviewBox.Width
        ProductsTreeviewBox.DropDownWidth = ProductsTreeviewBox.Width
        AdjustmentsTreeviewBox.DropDownWidth = AdjustmentsTreeviewBox.Width

        LoadAxisNodes(GlobalEnums.AnalysisAxis.ENTITIES, "Entities Filters")
        LoadAxisNodes(GlobalEnums.AnalysisAxis.CLIENTS, "Clients Filters")
        LoadAxisNodes(GlobalEnums.AnalysisAxis.PRODUCTS, "Products Filters")
        LoadAxisNodes(GlobalEnums.AnalysisAxis.ADJUSTMENTS, "Adjustments Filters")

        VTreeViewUtil.InitTVFormat(EntityTreeBox.TreeView)
        VTreeViewUtil.InitTVFormat(AccountTreeBox.TreeView)
        VTreeViewUtil.InitTVFormat(VersionTreeBox.TreeView)
        VTreeViewUtil.InitTVFormat(CategoriesFiltersTreebox.TreeView)
        VTreeViewUtil.InitTVFormat(ClientsTreeviewBox.TreeView)
        VTreeViewUtil.InitTVFormat(ProductsTreeviewBox.TreeView)
        VTreeViewUtil.InitTVFormat(AdjustmentsTreeviewBox.TreeView)

        VersionTreeBox.TreeView.ImageList = m_versionsTreeviewImageList

    End Sub

    Private Sub LoadAxisNodes(ByRef axisId As Int32, _
                              ByRef axisName As String)

        Dim axisNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(axisId, axisName, CategoriesFiltersTreebox.TreeView)
        AxisFilterManager.LoadFvTv(axisNode, axisId)

    End Sub

    Private Sub InitCurrenciesCheckedListBox()

        Dim currenciesList As New Collections.Generic.List(Of UInt32)
        For Each currency As Currency In GlobalVariables.Currencies.GetDictionary().Values
            Dim li As New VIBlend.WinForms.Controls.ListItem
            li.Value = currency.Id
            li.Text = currency.Name
            CurrenciesComboBox.Items.Add(li)
            If li.Value = My.Settings.currentCurrency Then
                CurrenciesComboBox.SelectedItem = li
            End If
        Next

    End Sub

    Private Sub PreSelection()

        On Error Resume Next
        If GlobalVariables.Versions.IsVersionValid(My.Settings.version_id) Then
            Dim versionNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.FindNode(VersionTreeBox.TreeView, My.Settings.version_id)
            VersionTreeBox.TreeView.SelectedNode = versionNode
            InitPeriodsTReeview(versionNode.Value)
        End If

    End Sub

    Private Sub InitPeriodsTReeview(ByRef versionId As Int32)

        PeriodTreeBox.TreeView.Nodes.Clear()
        Dim version As Version = GlobalVariables.Versions.GetValue(versionId)
        If version Is Nothing Then Exit Sub

        Select Case version.TimeConfiguration

            Case CRUD.TimeConfig.YEARS
                For Each yearId As Int32 In GlobalVariables.Versions.GetPeriodsList(versionId)
                    VTreeViewUtil.AddNode(yearId, Format(Date.FromOADate(yearId), "yyyy"), PeriodTreeBox.TreeView)
                Next

            Case CRUD.TimeConfig.MONTHS
                Dim periodsDict = GlobalVariables.Versions.GetPeriodsDictionary(versionId)
                For Each yearId As Int32 In periodsDict.Keys
                    Dim yearNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(Computer.YEAR_PERIOD_IDENTIFIER & yearId, _
                                                                                                Format(Date.FromOADate(yearId), "yyyy"), _
                                                                                                PeriodTreeBox.TreeView)
                    For Each monthId As Int32 In periodsDict(yearId)
                        VTreeViewUtil.AddNode(monthId, Format(Date.FromOADate(monthId), "MMM yyyy"), yearNode)
                    Next
                Next

        End Select
        VTreeViewUtil.InitTVFormat(PeriodTreeBox.TreeView)

    End Sub

    Private Sub MultiLanguageSetup()

        Me.validate_cmd.Text = Local.GetValue("ppsbi.insert_formula")
        Me.m_categoryFilterLabel.Text = Local.GetValue("ppsbi.categories_filter")
        Me.m_productFilterLabel.Text = Local.GetValue("ppsbi.products_filter")
        Me.m_clientFilterLabel.Text = Local.GetValue("ppsbi.clients_filter")
        Me.m_versionLabel.Text = Local.GetValue("general.version")
        Me.m_currencyLabel.Text = Local.GetValue("general.currency")
        Me.m_entityLabel.Text = Local.GetValue("general.entity")
        Me.m_accountLabel.Text = Local.GetValue("general.account")
        Me.m_periodLabel.Text = Local.GetValue("general.period")
        Me.m_adjustmentFilterLabel.Text = Local.GetValue("ppsbi.adjustments_filter")
        Me.Text = Local.GetValue("ppsbi.title")

    End Sub

#End Region


#Region "Interface"

    Private Sub BuildFormula()

        If CheckFormValidity() = True Then
            Dim periodStr As String = (Format(DateTime.FromOADate(PeriodTreeBox.TreeView.SelectedNode.Value), "Short date"))
            Dim clientsFiltersStr As String = BuildStrFromTreeView(ClientsTreeviewBox.TreeView)
            Dim productsFiltersStr As String = BuildStrFromTreeView(ProductsTreeviewBox.TreeView)
            Dim adjustmentsFiltersStr As String = BuildStrFromTreeView(AdjustmentsTreeviewBox.TreeView)
            Dim categoriesFiltersStr As String = BuildStrFromTreeView(CategoriesFiltersTreebox.TreeView)

            Dim strFormula = "=" + UDF_FORMULA_GET_DATA_NAME + "(" & Chr(34) & EntityTreeBox.TreeView.SelectedNode.Text & Chr(34) & "," _
                                                                   & Chr(34) & AccountTreeBox.TreeView.SelectedNode.Text & Chr(34) & "," _
                                                                   & Chr(34) & periodStr & Chr(34) & "," _
                                                                   & Chr(34) & CurrenciesComboBox.SelectedItem.Text & Chr(34) & "," _
                                                                   & Chr(34) & VersionTreeBox.TreeView.SelectedNode.Text & Chr(34) & "," _
                                                                   & Chr(34) & clientsFiltersStr & Chr(34) & "," _
                                                                   & Chr(34) & productsFiltersStr & Chr(34) & "," _
                                                                   & Chr(34) & adjustmentsFiltersStr & Chr(34) & "," _
                                                                   & Chr(34) & categoriesFiltersStr & Chr(34) & ")"

            GlobalVariables.APPS.ActiveCell.Formula = strFormula
        End If

    End Sub


#End Region


#Region "Events"

    Private Sub validate_cmd_Click(sender As Object, e As EventArgs) Handles validate_cmd.Click

        BuildFormula()

    End Sub

    Private Sub VersionTreeBox_Leave(sender As Object, e As EventArgs) Handles VersionTreeBox.Leave

        Dim versionNode As VIBlend.WinForms.Controls.vTreeNode = VersionTreeBox.TreeView.SelectedNode
        If versionNode Is Nothing Then Exit Sub
        If GlobalVariables.Versions.IsVersionValid(versionNode.Value) Then
            InitPeriodsTReeview(versionNode.Value)
        End If

    End Sub

#End Region


#Region "Checks"

    Private Function CheckFormValidity() As Boolean

        If EntityTreeBox.TreeView.SelectedNode Is Nothing Then
            MsgBox(Local.GetValue("ppsbi.entity_selection"))
            Return False
        End If

        If AccountTreeBox.TreeView.SelectedNode Is Nothing Then
            MsgBox(Local.GetValue("ppsbi.account_selection"))
            Return False
        End If

        If VersionTreeBox.TreeView.SelectedNode Is Nothing _
         Or GlobalVariables.Versions.IsVersionValid(VersionTreeBox.TreeView.SelectedNode.Value) = False Then
            MsgBox(Local.GetValue("ppsbi.invalid_version"))
            Return False
        End If

        If PeriodTreeBox.TreeView.SelectedNode Is Nothing Then
            MsgBox(Local.GetValue("ppsbi.msg_period_selection"))
            Return False
        End If

        Return True

    End Function

#End Region


#Region "Utilities"

    Private Function BuildStrFromTreeView(ByRef TV As vTreeView) As String

        Dim filterStr As String = ""
        For Each selectedNode As vTreeNode In TV.SelectedNodes
            filterStr &= selectedNode.Text & ","
        Next
        If filterStr = "" Then Return ""
        Return Strings.Left(filterStr, Len(filterStr) - 1)

    End Function

#End Region




End Class