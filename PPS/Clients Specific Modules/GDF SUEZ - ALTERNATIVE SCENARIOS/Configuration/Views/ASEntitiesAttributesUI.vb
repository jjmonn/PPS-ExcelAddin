' 
'
'
'
'
' Last modified: 10/02/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.DataGridView.Filters
Imports VIBlend.Utilities
Imports System.Drawing


Friend Class ASEntitiesAttributesUI


#Region "Instance Variables"

    ' Objects
    Private Controller As ASEntitiesAttributesController
    Private entitiesTV As TreeView

    ' Variables
    Friend entities_name_id_dic As Hashtable
    Friend rows_id_item_dic As New Dictionary(Of String, HierarchyItem)
    Friend isFillingDGV As Boolean
    Friend current_cell As GridCell

    ' Constants
    Private Const DGV_FONT_SIZE As Single = 8
    Private Const CONSO_ENTITIES_ROW_THEME As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010BLUE
    Private Const INPUT_ENTITIES_ROW_THEME As VIBLEND_THEME = VIBLEND_THEME.METROBLUE
    Private columns_names As String() = {"Entity", "Gas Formula", "Liquids Formula", "Tax Rate"}
    Private columns_ids As String() = {"", GDF_ENTITIES_AS_GAS_FORMULA_VAR, GDF_ENTITIES_AS_LIQUID_FORMULA_VAR, GDF_ENTITIES_AS_TAX_RATE_VAR}

#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef input_controller As ASEntitiesAttributesController, _
                             ByRef input_entitites_name_key_dic As Hashtable, _
                             ByRef input_entities_TV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        entitiesTV = input_entities_TV
        entities_name_id_dic = input_entitites_name_key_dic
        InitializeDGVDisplay()
        Me.WindowState = FormWindowState.Maximized
        AddHandler entitiesDGV.CellValueChanging, AddressOf dataGridView_CellValueChanging
        AddHandler entitiesDGV.CellMouseClick, AddressOf dataGridView_CellMouseClick


    End Sub

    Private Sub InitializeDGVDisplay()

        InitializeDGVColumns()
        InitializeDGVRows()
        entitiesDGV.ColumnsHierarchy.AutoStretchColumns = True
        entitiesDGV.ColumnsHierarchy.AllowResize = True
        entitiesDGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        entitiesDGV.AllowCopyPaste = True
        entitiesDGV.VIBlendTheme = CONSO_ENTITIES_ROW_THEME
        entitiesDGV.BackColor = SystemColors.Control

    End Sub

    Private Sub EntitiesManagementUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        entitiesDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)

    End Sub

#End Region


#Region "DGV"

#Region "Init"

    Private Sub InitializeDGVColumns()

        For Each column_name In columns_names
            entitiesDGV.ColumnsHierarchy.Items.Add(column_name)
        Next
        For j = 1 To columns_names.Length - 1
            Dim currencyTextBoxEditor As New TextBoxEditor()
            currencyTextBoxEditor.ActivationFlags = EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL
            entitiesDGV.ColumnsHierarchy.Items(j).CellsEditor = currencyTextBoxEditor
        Next

    End Sub

    Private Sub InitializeDGVRows()

        isFillingDGV = True
        For Each node In entitiesTV.Nodes
            Dim row As HierarchyItem = entitiesDGV.RowsHierarchy.Items.Add("")
            entitiesDGV.CellsArea.SetCellValue(row, entitiesDGV.ColumnsHierarchy.Items(0), node.text)
            FormatRow(row, node)
            addChildrenRows(node, row)
        Next
        isFillingDGV = False

    End Sub

    Private Sub addChildrenRows(ByRef node As TreeNode, ByRef row As HierarchyItem)

        For Each child_node In node.Nodes
            Dim sub_row As HierarchyItem = row.Items.Add("")
            entitiesDGV.CellsArea.SetCellValue(sub_row, entitiesDGV.ColumnsHierarchy.Items(0), child_node.Text)
            FormatRow(sub_row, child_node)
            addChildrenRows(child_node, sub_row)
        Next

    End Sub

    Private Sub FormatRow(ByRef row As HierarchyItem, _
                          ByRef node As TreeNode)

        rows_id_item_dic.Add(node.Name, row)
        row.TextAlignment = ContentAlignment.MiddleLeft
        Dim style_n, style_s As HierarchyItemStyle

        If node.Nodes.Count > 0 Then
            style_n = GridTheme.GetDefaultTheme(CONSO_ENTITIES_ROW_THEME).HierarchyItemStyleNormal
            style_s = GridTheme.GetDefaultTheme(CONSO_ENTITIES_ROW_THEME).HierarchyItemStyleSelected
            Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(CONSO_ENTITIES_ROW_THEME).GridCellStyle
            CStyle.FillStyle = style_n.FillStyle
            row.CellsStyle = CStyle
        Else
            style_n = GridTheme.GetDefaultTheme(INPUT_ENTITIES_ROW_THEME).HierarchyItemStyleNormal
            style_s = GridTheme.GetDefaultTheme(INPUT_ENTITIES_ROW_THEME).HierarchyItemStyleSelected
        End If
        row.HierarchyItemStyleNormal = style_n
        row.HierarchyItemStyleSelected = style_s

    End Sub

#End Region

    Protected Friend Sub FillDGV(ByRef entities_dict As Dictionary(Of String, Hashtable))

        isFillingDGV = True
        Dim columnItem As HierarchyItem
        For Each entity_id In entities_dict.Keys
            Dim rowItem = rows_id_item_dic(entity_id)
            For column_index = 0 To entities_dict(entity_id).Count - 1
                columnItem = entitiesDGV.ColumnsHierarchy.Items(column_index + 1)
                entitiesDGV.CellsArea.SetCellValue(rowItem, columnItem, entities_dict(entity_id)(columns_ids(column_index + 1)))
            Next
            ' If entities_dict(entity_id)(ENTITIES_ALLOW_EDITION_VARIABLE) = 0 Then rowItem.ImageIndex = 0 
        Next
        isFillingDGV = False
        DataGridViewsUtil.DGVSetHiearchyFontSize(entitiesDGV, DGV_FONT_SIZE, DGV_FONT_SIZE)
        DataGridViewsUtil.FormatDGVRowsHierarchy(entitiesDGV)
        entitiesDGV.Refresh()

    End Sub

#End Region


#Region "Calls Back"

    Private Sub CopyDownBT_Click(sender As Object, e As EventArgs)

        entitiesDGV.Refresh()

    End Sub

    Private Sub dropInExcel_cmd_Click(sender As Object, e As EventArgs) Handles SendEntitiesHierarchyToExcelToolStripMenuItem.Click


    End Sub


#Region "DGV Right Click Menu"

    Private Sub cop_down_bt_Click(sender As Object, e As EventArgs) Handles copy_down_bt.Click

        '        EntitiesDGVMGT.CopyValueDown()

    End Sub

    Private Sub drop_to_excel_bt_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        ' EntitiesDGVMGT.DropInExcel()

    End Sub

#End Region


#End Region


#Region "DGV Events"

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        current_cell = args.Cell
        '        currentRowItem = args.Cell.RowItem

    End Sub

    Friend Sub comboTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Escape Then
            If e.KeyCode = Keys.Enter Then
                entitiesDGV.CloseEditor(True)
            Else
                entitiesDGV.CloseEditor(False)
            End If
        End If

    End Sub

    Private Sub dataGridView_CellValueChanging(sender As Object, args As CellValueChangingEventArgs)

        If isFillingDGV = False Then
            Dim variable_id As String = columns_ids(args.Cell.ColumnItem.ItemIndex)
            Dim entity_id As String = entities_name_id_dic(entitiesDGV.CellsArea.GetCellValue(args.Cell.RowItem, entitiesDGV.ColumnsHierarchy.Items(0)))
            Select Case variable_id
                Case GDF_ENTITIES_AS_GAS_FORMULA_VAR
                    If Controller.UpdateGasFormula(entity_id, args.NewValue) = False Then args.Cancel = True

                Case GDF_ENTITIES_AS_LIQUID_FORMULA_VAR
                    If Controller.UpdateLiquidsFormula(entity_id, args.NewValue) = False Then args.Cancel = True

                Case GDF_ENTITIES_AS_TAX_RATE_VAR
                    Dim value As Object = args.NewValue
                    If IsNumeric(Replace(value, ".", ",")) Then value = Replace(value, ".", ",")
                    If IsNumeric(value) Then
                        Controller.UpdateTaxrate(entity_id, value)
                    Else
                        args.Cancel = True
                    End If
            End Select

        End If

    End Sub


#End Region



End Class