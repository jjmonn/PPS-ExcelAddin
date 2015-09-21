' PPSBI_UI.VB
' 
' User interface for the construction of PPSBI functions
'
' 
' Last modified: 10/09/2015 
' Author: Julien Monnereau


Imports Microsoft.Office.Interop
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.WinForms.Controls



Friend Class PPSBI_UI


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

        GlobalVariables.Entities.LoadEntitiesTV(EntityTreeBox.TreeView)
        GlobalVariables.Accounts.LoadAccountsTV(AccountTreeBox.TreeView)
        GlobalVariables.Versions.LoadVersionsTV(VersionTreeBox.TreeView)
        GlobalVariables.Clients.LoadAxisTree(ClientsTreeviewBox.TreeView)
        GlobalVariables.Products.LoadAxisTree(ProductsTreeviewBox.TreeView)
        GlobalVariables.Adjustments.LoadAxisTree(AdjustmentsTreeviewBox.TreeView)

        EntityTreeBox.DropDownWidth = EntityTreeBox.Width
        AccountTreeBox.DropDownWidth = AccountTreeBox.Width
        VersionTreeBox.DropDownWidth = VersionTreeBox.Width
        CategoriesFiltersTreebox.DropDownWidth = CategoriesFiltersTreebox.Width
        PeriodTreeBox.DropDownWidth = PeriodTreeBox.Width
        ClientsTreeviewBox.DropDownWidth = ClientsTreeviewBox.Width
        ProductsTreeviewBox.DropDownWidth = ProductsTreeviewBox.Width
        AdjustmentsTreeviewBox.DropDownWidth = AdjustmentsTreeviewBox.Width

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

        VersionTreeBox.TreeView.ImageList = VersionsTVIcons

    End Sub

    Private Sub LoadAxisNodes(ByRef axisId As Int32, _
                              ByRef axisName As String)

        Dim axisNode As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.AddNode(axisId, axisName, CategoriesFiltersTreebox.TreeView)
        AxisFilter.LoadFvTv(axisNode, axisId)

    End Sub

    Private Sub InitCurrenciesCheckedListBox()

        Dim currenciesList As New Collections.Generic.List(Of UInt32)
        For Each currencyId As Int32 In GlobalVariables.Currencies.currencies_hash.Keys
            Dim li As New VIBlend.WinForms.Controls.ListItem
            li.Value = currencyId
            li.Text = GlobalVariables.Currencies.currencies_hash(currencyId)(NAME_VARIABLE)
            CurrenciesComboBox.Items.Add(li)
            If li.Value = GlobalVariables.Currencies.mainCurrency Then
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
        Select Case GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE)

            Case GlobalEnums.TimeConfig.YEARS
                For Each yearId As Int32 In GlobalVariables.Versions.GetPeriodsList(versionId)
                    VTreeViewUtil.AddNode(yearId, Format(Date.FromOADate(yearId), "yyyy"), PeriodTreeBox.TreeView)
                Next

            Case GlobalEnums.TimeConfig.MONTHS
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
            MsgBox("Please select an Entity.")
            Return False
        End If

        If AccountTreeBox.TreeView.SelectedNode Is Nothing Then
            MsgBox("Please select an Account.")
            Return False
        End If

        If VersionTreeBox.TreeView.SelectedNode Is Nothing _
         Or GlobalVariables.Versions.IsVersionValid(VersionTreeBox.TreeView.SelectedNode.Value) = False Then
            MsgBox("Invalid Version (Folder) or no Version Selected.")
            Return False
        End If

        If PeriodTreeBox.TreeView.SelectedNode Is Nothing Then
            MsgBox("Please select a Period (A version must be selected in order to select the period).")
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