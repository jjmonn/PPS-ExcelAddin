' ControlsControl.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 17/07/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.Utilities
Imports System.Windows.Forms


Friend Class ControlsControl


#Region "Instance Variables"

    ' Objects
    Private Controller As ControlsController
    Private ChartsController As ChartsControlsController
    Protected Friend DGV As New vDataGridView
    Private ChartsTV As TreeView

    ' Variables
    Private accounts_name_id_dic As Hashtable
    Private accounts_id_name_dic As Hashtable
    Private accounts_list As List(Of String)

#Region "Numeric Controls Variables"

    Private Item1Editor As New ComboBoxEditor
    Private Item2Editor As New ComboBoxEditor
    Private OperatorEditor As New ComboBoxEditor
    Private OptionEditor As New ComboBoxEditor

    ' Variables
    Private operators_symbol_id_dic As Dictionary(Of String, String)
    Private period_options_name_id_dic As Dictionary(Of String, String)
    Private operators_id_symbol_dic As Dictionary(Of String, String)
    Private period_options_id_name_dic As Dictionary(Of String, String)
    Private isFillingDGV As Boolean
    Private current_row As HierarchyItem

    ' Display
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 25
    Private Const FONT_SIZE_ROW_ITEMS As Int32 = 8
    Private Const DGV_THEME As VIBLEND_THEME = VIBLEND_THEME.VISTABLUE

#End Region

#Region "Charts Controls"

    Private current_chart_node As TreeNode
    Private current_serie_id As String = ""
    Private current_chart_id As String = ""
    Private isDisplayingSerie As Boolean

#End Region

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_controller As ControlsController, _
                            ByRef input_chartsController As ChartsControlsController, _
                            ByRef input_accounts_name_id_dic As Hashtable, _
                            ByRef input_operators_symbol_id_dic As Dictionary(Of String, String), _
                            ByRef input_period_options_name_id_dic As Dictionary(Of String, String))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        accounts_name_id_dic = input_accounts_name_id_dic
        operators_symbol_id_dic = input_operators_symbol_id_dic
        period_options_name_id_dic = input_period_options_name_id_dic
        ChartsController = input_chartsController

        accounts_id_name_dic = GlobalVariables.Accounts.GetAccountsDictionary(ID_VARIABLE, NAME_VARIABLE)
        operators_id_symbol_dic = OperatorsMapping.GetOperatorsDictionary(OPERATOR_ID_VARIABLE, OPERATOR_SYMBOL_VARIABLE)
        period_options_id_name_dic = ControlOptionsMapping.GetControlOptionsDictionary(CONTROL_OPTION_ID_VARIABLE, CONTROL_OPTION_NAME_VARIABLE)

        InitializeNumericControlCBs()
        InitializeChartsCBs()
        InitializeDGV()
        ControlsDGVPanel.Controls.Add(DGV)
        DGV.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Private Sub InitializeNumericControlCBs()

        accounts_list = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_ALL, NAME_VARIABLE)
        For Each account_name In accounts_list
            Item1Editor.Items.Add(account_name)
            Item2Editor.Items.Add(account_name)
        Next

        For Each operator_symbol In operators_symbol_id_dic.Keys
            OperatorEditor.Items.Add(operator_symbol)
        Next

        For Each period_option_name In period_options_name_id_dic.Keys
            OptionEditor.Items.Add(period_option_name)
        Next

        Item1Editor.DropDownHeight = Item1Editor.ItemHeight * CB_NB_ITEMS_DISPLAYED
        Item2Editor.DropDownHeight = Item2Editor.ItemHeight * CB_NB_ITEMS_DISPLAYED
        OperatorEditor.DropDownHeight = OperatorEditor.ItemHeight * OperatorEditor.Items.Count
        OptionEditor.DropDownHeight = OptionEditor.ItemHeight * (OptionEditor.Items.Count + 1)

    End Sub

    Private Sub InitializeChartsCBs()

        Dim palettes_list As List(Of String) = PalettesMapping.GetPalettesList
        For Each palette In palettes_list
            PalettesCB.Items.Add(palette)
        Next

        For Each account_name In accounts_list
            SerieAccountIDCB.Items.Add(account_name)
        Next

        For Each serie_type In SeriesMapping.GetSerieTypesList
            SerieTypeCB.Items.Add(serie_type)
        Next

        PalettesCB.Items.Add("")
        SerieAccountIDCB.Items.Add("")
        SerieTypeCB.Items.Add("")

    End Sub

    Private Sub InitializeDGV()

        DGV.RowsHierarchy.Visible = False
        Dim c0 = DGV.ColumnsHierarchy.Items.Add("Control")
        Dim c1 = DGV.ColumnsHierarchy.Items.Add("Item 1")
        Dim c2 = DGV.ColumnsHierarchy.Items.Add("Operator")
        Dim c3 = DGV.ColumnsHierarchy.Items.Add("Item 2")
        Dim c4 = DGV.ColumnsHierarchy.Items.Add("Option")

        Dim nameEditor As New TextBoxEditor
        c0.CellsEditor = nameEditor
        c1.CellsEditor = Item1Editor
        c2.CellsEditor = OperatorEditor
        c3.CellsEditor = Item2Editor
        c4.CellsEditor = OptionEditor

        DataGridViewsUtil.InitDisplayVDataGridView(DGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, FONT_SIZE_ROW_ITEMS, FONT_SIZE_ROW_ITEMS)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.BackColor = System.Drawing.SystemColors.Control
        AddHandler DGV.CellValueChanging, AddressOf DGV_CellValueChanging
        AddHandler DGV.CellMouseClick, AddressOf DGV_CellMouseClick

    End Sub

    Protected Friend Sub closeControl()

    End Sub

#End Region


#Region "Interface"

#Region "Numeric Controls"

    Protected Friend Sub AddRow(ByVal HT As Hashtable)

        Dim row = DGV.RowsHierarchy.Items.Add(HT(CONTROL_ID_VARIABLE))
        isFillingDGV = True
        DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(0), HT(CONTROL_NAME_VARIABLE))

        ' Item 1
        If IsNumeric(HT(CONTROL_ITEM1_VARIABLE)) Then
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(1), HT(CONTROL_ITEM1_VARIABLE))
        Else
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(1), accounts_id_name_dic(HT(CONTROL_ITEM1_VARIABLE)))
        End If

        ' Item 2
        If IsNumeric(HT(CONTROL_ITEM2_VARIABLE)) Then
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(3), HT(CONTROL_ITEM2_VARIABLE))
        Else
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(3), accounts_id_name_dic(HT(CONTROL_ITEM2_VARIABLE)))
        End If

        DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(2), operators_id_symbol_dic(HT(CONTROL_OPERATOR_ID_VARIABLE)))
        DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(4), period_options_id_name_dic(HT(CONTROL_PERIOD_OPTION_VARIABLE)))
        isFillingDGV = False

    End Sub

#End Region

#Region "Charts Controls"

    Protected Friend Sub DisplayChartsInit(ByRef input_chartsTV As TreeView)

        ChartsTV = input_chartsTV
        ChartsTV.ImageList = ChartsTVImageList
        ChartsTVPanel.Controls.Add(ChartsTV)
        ChartsTV.Dock = DockStyle.Fill
        ChartsTV.ContextMenuStrip = ChartsTVRCM
        AddHandler ChartsTV.AfterSelect, AddressOf ChartsTV_AfterSelect
        AddHandler ChartsTV.NodeMouseClick, AddressOf ChartsTV_node_mouse_click
        AddHandler ChartsTV.KeyDown, AddressOf ChartsTV_KeyDown

    End Sub

#End Region

#End Region


#Region "Call Backs"

#Region "Numeric Controls"

    Private Sub AddControlBT_Click(sender As Object, e As EventArgs) Handles AddControlBT.Click

        Controller.DisplayNewUI()

    End Sub

    Private Sub DeleteControlBT_Click(sender As Object, e As EventArgs) Handles DeleteControlBT.Click

        If Not current_row Is Nothing Then
            Controller.DeleteControl(current_row.Caption)
            current_row.Delete()
            current_row = Nothing
            DGV.Refresh()
            DGV.Select()
        Else
            MsgBox("A control must be selected.")
        End If

    End Sub

#End Region

#Region "Charts Controls"

    Private Sub NewChartBT_Click(sender As Object, e As EventArgs) Handles NewChartRCM.Click, NewChartBT.Click

        Dim name = InputBox("Please enter the Name of the New Chart: ")
        If name <> "" Then ChartsController.CreateControlChart(name)

    End Sub

    Private Sub NewSerieBT_Click(sender As Object, e As EventArgs) Handles NewSerieRCM.Click, NewSerieBT.Click

        If Not current_chart_node Is Nothing Then
            Dim name As String = InputBox("new Serie Name: ")
            If name <> "" Then
                If current_chart_node.Parent Is Nothing Then
                    ChartsController.CreateSerie(current_chart_node, name)
                    current_chart_node.ExpandAll()
                Else
                    ChartsController.CreateSerie(current_chart_node.Parent, name)
                    current_chart_node.Parent.ExpandAll()
                End If
            End If
        Else
            MsgBox("A Chart must be selected to add a new Serie.")
        End If

    End Sub

    Private Sub DeleteChartsBT_Click(sender As Object, e As EventArgs) Handles DeleteRCM.Click, DeleteChartsBT.Click

        If Not current_chart_node Is Nothing Then
            If current_chart_node.Parent Is Nothing Then
                If current_chart_id = current_chart_node.Name Then ClearChartOptions()
                ChartsController.DeleteChart(current_chart_node)
            Else
                If current_serie_id = current_chart_node.Name Then ClearSerieDisplay()
                ChartsController.DeleteSerie(current_chart_node)
            End If
        Else
            MsgBox("A Chart or a Serie must be selected.")
        End If

    End Sub

    Private Sub RenameRCBT_Click(sender As Object, e As EventArgs) Handles RenameRCBT.Click

        If Not current_chart_node Is Nothing Then
            Dim name = InputBox("New Name: ")
            If name <> "" Then
                ChartsController.UpdateName(current_chart_node, name)
                If current_chart_id = current_chart_node.Name Then ChartNameTB.Text = name Else SerieNameTB.Text = name
            End If
        Else
            MsgBox("A Chart or a Serie must be selected.")
        End If

    End Sub

#End Region

#End Region


#Region "Events"

#Region "Numeric Controls"

    Private Sub DGV_CellValueChanging(sender As Object, args As CellValueChangingEventArgs)

        If isFillingDGV = False Then
            Select Case args.Cell.ColumnItem.ItemIndex
                Case 0
                    If Controller.IsNameValid(args.NewValue) Then
                        Controller.UpdateName(args.Cell.RowItem.Caption, args.NewValue)
                    Else
                        args.Cancel = True
                    End If
                Case 1
                    If accounts_name_id_dic.ContainsKey(args.NewValue) Then
                        Controller.UpdateItem1(args.Cell.RowItem.Caption, accounts_name_id_dic(args.NewValue))
                    Else
                        args.Cancel = True
                    End If
                Case 2
                    If operators_symbol_id_dic.ContainsKey(args.NewValue) Then
                        Controller.UpdateOperator(args.Cell.RowItem.Caption, operators_symbol_id_dic(args.NewValue))
                    Else
                        args.Cancel = True
                    End If
                Case 3
                    If accounts_name_id_dic.ContainsKey(args.NewValue) Then
                        Controller.UpdateItem2(args.Cell.RowItem.Caption, accounts_name_id_dic(args.NewValue))
                    Else
                        args.Cancel = True
                    End If
                Case 4

            End Select
        End If

    End Sub

    Private Sub DGV_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        current_row = args.Cell.RowItem

    End Sub

#End Region

#Region "Charts Controls"

    Private Sub SerieAccountIDCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles SerieAccountIDCB.SelectedValueChanged

        If current_serie_id <> "" AndAlso isDisplayingSerie = False Then ChartsController.UpdateSerieAccountID(current_serie_id, accounts_name_id_dic(SerieAccountIDCB.Text))

    End Sub

    Private Sub ColorBT_Click(sender As Object, e As EventArgs) Handles ColorBT.Click

        If current_serie_id <> "" Then
            If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                ColorBT.BackColor = ColorDialog1.Color
                ChartsController.UpdateSerieColor(current_serie_id, ColorDialog1.Color.ToArgb)
            End If
        End If

    End Sub

    Private Sub SerieTypeTB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SerieTypeCB.SelectedIndexChanged

        If current_serie_id <> "" AndAlso isDisplayingSerie = False Then ChartsController.UpdateSerieType(current_serie_id, SerieTypeCB.Text)

    End Sub

    Private Sub PalettesCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PalettesCB.SelectedIndexChanged

        If current_chart_id <> "" AndAlso isDisplayingSerie = False Then ChartsController.UpdateChartPalette(current_chart_id, PalettesCB.Text)

    End Sub

#Region "Charts TV"

    Private Sub ChartsTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter : DisplayDataIfSerie()
            Case Keys.Delete : DeleteChartsBT_Click(sender, e)
        End Select

    End Sub

    Private Sub ChartsTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        current_chart_node = e.Node
        DisplayDataIfSerie()

    End Sub

    Private Sub ChartsTV_node_mouse_click(sender As Object, e As TreeNodeMouseClickEventArgs)

        current_chart_node = e.Node
        DisplayDataIfSerie()

    End Sub

#End Region

#End Region

#End Region


#Region "Utilities"

#Region "Charts Controls"

    Private Sub DisplayDataIfSerie()

        If Not current_chart_node.Parent Is Nothing Then
            current_serie_id = current_chart_node.Name
            DisplaySerieData(current_serie_id)
            ClearChartOptions()
        Else
            DisplayChartOptions(current_chart_node.Name)
            ClearSerieDisplay()
        End If

    End Sub

    Private Sub DisplaySerieData(ByRef serie_id As String)

        If serie_id <> "" Then
            isDisplayingSerie = True
            Dim ht As Hashtable = ChartsController.GetSerieHT(serie_id)
            ChartNameTB.Text = current_chart_node.Parent.Text
            SerieNameTB.Text = current_chart_node.Text
            If Not IsDBNull(ht(CONTROL_CHART_TYPE_VARIABLE)) Then SerieTypeCB.Text = ht(CONTROL_CHART_TYPE_VARIABLE) Else SerieTypeCB.Text = ""
            If Not IsDBNull(ht(CONTROL_CHART_COLOR_VARIABLE)) Then ColorBT.BackColor = System.Drawing.Color.FromArgb(ht(CONTROL_CHART_COLOR_VARIABLE)) Else ColorBT.BackColor = Drawing.Color.White
            If Not IsDBNull(ht(CONTROL_CHART_ACCOUNT_ID_VARIABLE)) Then SerieAccountIDCB.SelectedItem = accounts_id_name_dic(ht(CONTROL_CHART_ACCOUNT_ID_VARIABLE)) Else SerieAccountIDCB.Text = ""
            isDisplayingSerie = False
        End If

    End Sub

    Private Sub ClearSerieDisplay()

        isDisplayingSerie = True
        SerieNameTB.Text = ""
        SerieTypeCB.Text = ""
        ColorBT.BackColor = Drawing.Color.White
        SerieAccountIDCB.Text = ""
        isDisplayingSerie = False
        current_serie_id = ""

    End Sub

    Private Sub DisplayChartOptions(ByRef chart_id As String)

        current_chart_id = chart_id
        Dim palette = ChartsController.GetChartPalette(chart_id)
        isDisplayingSerie = True
        If Not IsDBNull(palette) Then PalettesCB.SelectedItem = palette Else PalettesCB.SelectedItem = ""
        isDisplayingSerie = False

    End Sub

    Private Sub ClearChartOptions()

        isDisplayingSerie = True
        PalettesCB.SelectedItem = ""
        isDisplayingSerie = False
        current_chart_id = ""

    End Sub

#End Region


#End Region



End Class
