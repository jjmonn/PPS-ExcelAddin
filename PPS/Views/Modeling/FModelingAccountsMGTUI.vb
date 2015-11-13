' FModelingAccountsMGTUI.vb
'
'
'
'
'
' Auhtor: Julien Monnereau
' Last modified: 17/02/2015


Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.Utilities
Imports System.Drawing
Imports System.Windows.Forms


Friend Class FModelingAccountsMGTUI



#Region "Instance Variables"

    ' Objects
    Private Controller As FModellingAccountsController
    Private DGV As New vDataGridView

    ' Variables
    Friend f_accounts_name_id_dic As Hashtable
    Friend rows_id_item_dic As New Dictionary(Of String, HierarchyItem)
    Friend isFillingDGV As Boolean
    Private current_serie_id As String

    ' Constants
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER

#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef input_controller As FModellingAccountsController, _
                             ByRef input_accounts_name_id_dic As Hashtable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        f_accounts_name_id_dic = input_accounts_name_id_dic

        InitializeDGVDisplay()
        Me.Controls.Add(DGV)
        DGV.Dock = DockStyle.Fill

        AddHandler DGV.CellBeginEdit, AddressOf dataGridView_CellBeginEdit
        AddHandler DGV.CellMouseClick, AddressOf dataGridView_CellMouseClick

    End Sub

    Private Sub InitializeDGVDisplay()

        InitializeDGVColumns()
        InitializeDGVRows()
        DGV.RowsHierarchy.Visible = False
        DGV.ColumnsHierarchy.AllowResize = True
        DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        DGV.AllowCopyPaste = True
        DGV.VIBlendTheme = DGV_THEME
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        DGV.BackColor = Color.White

    End Sub

    Private Sub InitializeDGVColumns()

        Dim TBEditor As New TextBoxEditor
        Dim TypesCBEditor As New ComboBoxEditor
        Dim ChartsCBEditor As New ComboBoxEditor
        InitializeEditors(TypesCBEditor, ChartsCBEditor)

        Dim col0 As HierarchyItem = DGV.ColumnsHierarchy.Items.Add("")
        Dim col1 As HierarchyItem = DGV.ColumnsHierarchy.Items.Add("Color")
        Dim col2 As HierarchyItem = DGV.ColumnsHierarchy.Items.Add("Type")
        Dim col3 As HierarchyItem = DGV.ColumnsHierarchy.Items.Add("Chart")

        col1.CellsEditor = TBEditor
        col2.CellsEditor = TypesCBEditor
        col3.CellsEditor = ChartsCBEditor

        For j = 1 To DGV.ColumnsHierarchy.Items.Count - 1
            DGV.ColumnsHierarchy.Items(j).TextAlignment = Drawing.ContentAlignment.MiddleCenter
            DGV.ColumnsHierarchy.Items(j).CellsTextAlignment = Drawing.ContentAlignment.MiddleRight
        Next

    End Sub

    Private Sub InitializeDGVRows()

        isFillingDGV = True
        For Each f_account_name As String In f_accounts_name_id_dic.Keys
            Dim f_account_id As String = f_accounts_name_id_dic(f_account_name)
            Dim row As HierarchyItem = DGV.RowsHierarchy.Items.Add(f_account_id)
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(0), f_account_name)
            rows_id_item_dic.Add(f_account_id, row)
        Next
        isFillingDGV = False

    End Sub

    Private Sub InitializeEditors(ByRef TypesCBEditor As ComboBoxEditor, _
                                  ByRef ChartsCBEditor As ComboBoxEditor)

        TypesCBEditor.Items.Add("Area")
        TypesCBEditor.Items.Add("Bar")
        TypesCBEditor.Items.Add("Column")
        TypesCBEditor.Items.Add("Line")
        TypesCBEditor.Items.Add("Spline")
        TypesCBEditor.Items.Add("StackedColumn")

        ChartsCBEditor.Items.Add("Right Chart")
        ChartsCBEditor.Items.Add("Left Chart")

    End Sub

    Private Sub EntitiesManagementUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub FillDGV(ByRef f_accounts_seriesHT As Dictionary(Of String, Hashtable))

        isFillingDGV = True
        For Each f_account_id In f_accounts_seriesHT.Keys
            Dim row As HierarchyItem = rows_id_item_dic(f_account_id)
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(1), "")
            If Not IsDBNull(f_accounts_seriesHT(f_account_id)(CONTROL_CHART_COLOR_VARIABLE)) Then DataGridViewsUtil.SetCellFillColor(row.Cells(1), f_accounts_seriesHT(f_account_id)(CONTROL_CHART_COLOR_VARIABLE))
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(2), f_accounts_seriesHT(f_account_id)(CONTROL_CHART_TYPE_VARIABLE))
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(3), f_accounts_seriesHT(f_account_id)(FINANCIAL_MODELLING_SERIE_CHART_VARIABLE))
        Next
        isFillingDGV = False
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        DGV.Refresh()

    End Sub

#End Region


#Region "DGV Events"

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        current_serie_id = args.Cell.RowItem.Caption

    End Sub

    Private Sub dataGridView_CellBeginEdit(sender As Object, e As CellEventArgs)

        If e.Cell.ColumnItem.ItemIndex = 1 Then
            If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                DataGridViewsUtil.SetCellFillColor(e.Cell, ColorDialog1.Color.ToArgb)
                Controller.UpdateSerieColor(current_serie_id, ColorDialog1.Color.ToArgb)
                DGV.CloseEditor(False)
            End If
        End If

    End Sub

#End Region


#Region "Utilities"

    Private Sub FModelingAccountsMGTUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Me.Visible = False
        e.Cancel = True

    End Sub

#End Region



End Class