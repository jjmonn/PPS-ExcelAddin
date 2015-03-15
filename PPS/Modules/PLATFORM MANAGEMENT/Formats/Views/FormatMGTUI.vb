' FormatMGTUI.vb
'
'
'   Evolution: Manage INP 
'
'
' Author: Julien Monnereau
' Last modified: 03/03/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports VIBlend.Utilities
Imports VIBlend.WinForms.Controls


Friend Class FormatMGTUI


#Region "Instance Variables"

    ' Objects
    Private Controller As FormatsController
    Protected Friend DGV As New vDataGridView

    ' Variables
    Friend formats_name_id_dic As Hashtable
    Friend rows_id_item_dic As New Dictionary(Of String, HierarchyItem)
    Friend isFillingDGV As Boolean
    Private current_cell As GridCell

    ' Constants
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_controller As FormatsController, _
                             ByRef input_formats_name_id_dic As Hashtable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        formats_name_id_dic = input_formats_name_id_dic

        InitializeDGVDisplay()
        Me.Controls.Add(DGV)
        DGV.Dock = DockStyle.Fill

        AddHandler DGV.CellBeginEdit, AddressOf dataGridView_CellBeginEdit
        '     AddHandler DGV.CellValueChanging, AddressOf dataGridView_CellValueChanging
        ' AddHandler DGV.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler DGV.CellMouseEnter, AddressOf dataGridView_CellMouseEnter

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

        DGV.ColumnsHierarchy.Items.Add("Format Preview")
        Dim col1 As HierarchyItem = DGV.ColumnsHierarchy.Items.Add("Text Color")
        Dim col2 As HierarchyItem = DGV.ColumnsHierarchy.Items.Add("Background Color")
        DGV.ColumnsHierarchy.Items.Add("Bold")
        DGV.ColumnsHierarchy.Items.Add("Italic")
        DGV.ColumnsHierarchy.Items.Add("Border")

        col1.CellsEditor = TBEditor
        col2.CellsEditor = TBEditor

        For j = 1 To DGV.ColumnsHierarchy.Items.Count - 1
            DGV.ColumnsHierarchy.Items(j).TextAlignment = Drawing.ContentAlignment.MiddleCenter
            DGV.ColumnsHierarchy.Items(j).CellsTextAlignment = Drawing.ContentAlignment.MiddleCenter
        Next

    End Sub

    Private Sub InitializeDGVRows()

        isFillingDGV = True
        Dim checkBoxEditor As New CheckBoxEditor
        For Each format_name As String In formats_name_id_dic.Keys

            Dim format_id As String = formats_name_id_dic(format_name)
            Dim row As HierarchyItem = DGV.RowsHierarchy.Items.Add(format_id)
            rows_id_item_dic.Add(format_id, row)

            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(0), format_name)
            DGV.CellsArea.SetCellEditor(row, DGV.ColumnsHierarchy.Items(3), checkBoxEditor)
            DGV.CellsArea.SetCellEditor(row, DGV.ColumnsHierarchy.Items(4), checkBoxEditor)
            DGV.CellsArea.SetCellEditor(row, DGV.ColumnsHierarchy.Items(5), checkBoxEditor)

        Next
        AddHandler checkBoxEditor.CheckedChanged, AddressOf dataGridView_Checkedchanged
        isFillingDGV = False

    End Sub

    Private Sub EntitiesManagementUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)

    End Sub

    Protected Friend Sub FillDGV(ByRef formats_HT As Dictionary(Of String, Hashtable))

        isFillingDGV = True
        For Each format_id In formats_HT.Keys
            Dim row As HierarchyItem = rows_id_item_dic(format_id)
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(1), "")
            DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(2), "")
            If Not IsDBNull(formats_HT(format_id)(FORMAT_TEXT_COLOR_VARIABLE)) Then DataGridViewsUtil.SetCellFillColor(row.Cells(1), formats_HT(format_id)(FORMAT_TEXT_COLOR_VARIABLE))
            If Not IsDBNull(formats_HT(format_id)(FORMAT_BCKGD_VARIABLE)) Then DataGridViewsUtil.SetCellFillColor(row.Cells(2), formats_HT(format_id)(FORMAT_BCKGD_VARIABLE))

            If formats_HT(format_id)(FORMAT_BOLD_VARIABLE) = 1 Then DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(3), True)
            If formats_HT(format_id)(FORMAT_ITALIC_VARIABLE) = 1 Then DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(4), True)
            If formats_HT(format_id)(FORMAT_BORDER_VARIABLE) = 1 Then DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(5), True)

            FormatPreview(format_id, formats_HT(format_id))
        Next
        isFillingDGV = False
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        DGV.Refresh()

    End Sub

#End Region


#Region "Events"

    Private Sub dataGridView_CellMouseEnter(ByVal sender As Object, ByVal args As CellEventArgs)

        current_cell = args.Cell

    End Sub

    Private Sub dataGridView_CellBeginEdit(sender As Object, e As CellEventArgs)

        If isFillingDGV = False Then
            Select Case e.Cell.ColumnItem.ItemIndex
                Case 1
                    If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                        DataGridViewsUtil.SetCellFillColor(e.Cell, ColorDialog1.Color.ToArgb)
                        Controller.UpdateTextColor(e.Cell.RowItem.Caption, ColorDialog1.Color.ToArgb)
                        DGV.CloseEditor(False)
                    End If
                Case 2
                    If ColorDialog1.ShowDialog <> Windows.Forms.DialogResult.Cancel Then
                        DataGridViewsUtil.SetCellFillColor(e.Cell, ColorDialog1.Color.ToArgb)
                        Controller.UpdateBckgdColor(e.Cell.RowItem.Caption, ColorDialog1.Color.ToArgb)
                        DGV.CloseEditor(False)
                    End If
            End Select
        End If

    End Sub

    Private Sub dataGridView_Checkedchanged(sender As Object, e As EventArgs)

        Select Case current_cell.ColumnItem.ItemIndex
            Case 3
                Dim checkBox As vCheckBox = TryCast(DGV.CellsArea.GetCellEditor(current_cell.RowItem, DGV.ColumnsHierarchy.Items(3)).Control, vCheckBox)
                Controller.UpdateBold(current_cell.RowItem.Caption, Convert.ToInt32(checkBox.Checked))
            Case 4
                Dim checkBox As vCheckBox = TryCast(DGV.CellsArea.GetCellEditor(current_cell.RowItem, DGV.ColumnsHierarchy.Items(4)).Control, vCheckBox)
                Controller.UpdateItalic(current_cell.RowItem.Caption, Convert.ToInt32(checkBox.Checked))
            Case 5
                Dim checkBox As vCheckBox = TryCast(DGV.CellsArea.GetCellEditor(current_cell.RowItem, DGV.ColumnsHierarchy.Items(5)).Control, vCheckBox)
                Controller.UpdateBorder(current_cell.RowItem.Caption, Convert.ToInt32(checkBox.Checked))

        End Select

    End Sub


#End Region


#Region "Utilities"

    Protected Friend Sub FormatPreview(ByRef format_id As String, ByVal format_HT As Hashtable)

        Dim row As HierarchyItem = rows_id_item_dic(format_id)
        Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(DGV.VIBlendTheme).GridCellStyle
        If Not IsDBNull(format_HT(FORMAT_TEXT_COLOR_VARIABLE)) Then CStyle.TextColor = System.Drawing.Color.FromArgb(format_HT(FORMAT_TEXT_COLOR_VARIABLE))
        If Not IsDBNull(format_HT(FORMAT_BCKGD_VARIABLE)) Then CStyle.FillStyle = New FillStyleSolid(System.Drawing.Color.FromArgb(format_HT(FORMAT_BCKGD_VARIABLE)))
        If format_HT(FORMAT_BOLD_VARIABLE) = 1 Then CStyle.Font = New System.Drawing.Font(CStyle.Font.FontFamily, DGV_CELLS_FONT_SIZE, FontStyle.Bold)
        If format_HT(FORMAT_ITALIC_VARIABLE) = 1 Then CStyle.Font = New System.Drawing.Font(CStyle.Font.FontFamily, DGV_CELLS_FONT_SIZE, FontStyle.Italic)
        DGV.CellsArea.SetCellDrawStyle(row, DGV.ColumnsHierarchy.Items(0), CStyle)

    End Sub

    Private Sub FormatMGTUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Controller.Close()

    End Sub

#End Region



End Class