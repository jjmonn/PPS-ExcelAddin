﻿' EntitiesTGV.vb
'
'
' To do: Should inherit from analysisDGV and override init functions !
'
'
' Known bugs: 
'       - dgv row filling crashes if entity has "" value for a category
'           -> will be an issue in implementing no categories value for conso entities
'
'
' Last modified: 01/09/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports VIBlend.Utilities
Imports System.Drawing
Imports System.Collections
Imports Microsoft.Office.Interop
Imports VIBlend.WinForms.DataGridView.Filters
Imports System.Linq



Friend Class EntitiesDGV


#Region "Instance Variables"

    ' Objects
    Friend DGV As New vDataGridView

    ' Variables
    Private entitiesFilterTV As TreeView
    Private categoriesNameKeyDic As Hashtable
    Friend columnsDictionary As New Dictionary(Of String, HierarchyItem)
    Friend columnsCaptionID As New Dictionary(Of String, String)
    Friend rows_id_item_dic As New Dictionary(Of Int32, HierarchyItem)

    Private DGVArray(,) As String
    Private filterGroup As New FilterGroup(Of String)()
    Friend currentRowItem As HierarchyItem
    Friend isFillingDGV As Boolean

    ' Constants
    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER
    Friend Const CURRENCY_COLUMN_NAME As String = "Currency"
    Private Const NAME_COLUMN_NAME As String = "Name"
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 7

#End Region


#Region "Initialize"

    Friend Sub New(ByRef entitiesTV As TreeView, _
                    ByRef input_entitiesFilterTV As TreeView, _
                    ByRef input_categoriesKeyNameDic As Hashtable)

        entitiesFilterTV = input_entitiesFilterTV

        initFilters()
        InitializeDGVDisplay()
        DGVColumnsInitialize()
        DGVRowsInitialize(entitiesTV)
        fillDGV()

        AddHandler DGV.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler DGV.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick

    End Sub

    Private Sub InitializeDGVDisplay()

        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.ColumnsHierarchy.AllowResize = True
        'DGV.RowsHierarchy.AllowDragDrop = True
        DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        DGV.AllowDragDropIndication = True
        DGV.AllowCopyPaste = True
        DGV.FilterDisplayMode = FilterDisplayMode.Custom
        DGV.VIBlendTheme = DGV_VI_BLEND_STYLE
        DGV.BackColor = SystemColors.Control
        'DGV.ImageList = EntitiesIL

    End Sub

#Region "Columns Initialization"

    Private Sub DGVColumnsInitialize()

        DGV.ColumnsHierarchy.Clear()
        columnsDictionary.Clear()

        DGV.ColumnsHierarchy.Items.Add("Entity")
        Dim col1 As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(CURRENCY_COLUMN_NAME)
        columnsDictionary.Add(ENTITIES_CURRENCY_VARIABLE, col1)
        columnsCaptionID.Add(CURRENCY_COLUMN_NAME, ENTITIES_CURRENCY_VARIABLE)
        InitCurrenciesCB()
        col1.AllowFiltering = True
        ' CreateFilter(col1)

        For Each rootNode As TreeNode In entitiesFilterTV.Nodes
            CreateSubFilters(rootNode)
            '   CreateFilter(col)
        Next

    End Sub

    Private Sub CreateSubFilters(ByRef node As TreeNode)
        Dim col As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(node.Text)
        columnsDictionary.Add(node.Name, col)
        columnsCaptionID.Add(node.Text, node.Name)
        InitComboBox(col, node)
        col.AllowFiltering = True
        For Each childNode As TreeNode In node.Nodes
            CreateSubFilters(childNode)
        Next
    End Sub

    Private Sub CreateFilter(ByRef column As HierarchyItem)

        Dim filter As New HierarchyItemFilter()
        filter.Item = column
        filter.Filter = filterGroup
        DGV.RowsHierarchy.Filters.Add(filter)

    End Sub

    Private Sub initFilters()

        Dim stringFilter1 As New StringFilter()
        stringFilter1.ComparisonOperator = StringFilterComparisonOperator.NOT_EMPTY
        stringFilter1.Value = ""

        Dim stringFilter2 As New StringFilter()
        stringFilter1.ComparisonOperator = StringFilterComparisonOperator.CONTAINS
        stringFilter1.Value = "a"

        filterGroup.AddFilter(FilterOperator.AND, stringFilter1)
        filterGroup.AddFilter(FilterOperator.AND, stringFilter2)

    End Sub

#End Region

#Region "Rows Initialization"

    Private Sub DGVRowsInitialize(ByRef entities_tv As TreeView)

        DGV.RowsHierarchy.Clear()
        rows_id_item_dic.Clear()
        For Each node In entities_tv.Nodes
            addRow(node)
        Next

    End Sub

    Friend Sub addRow(ByRef node As TreeNode, _
                      Optional ByRef parent_row As HierarchyItem = Nothing)

        isFillingDGV = True
        Dim row As HierarchyItem
        If parent_row Is Nothing Then
            row = DGV.RowsHierarchy.Items.Add("")
        Else
            row = parent_row.Items.Add("")
        End If
        DGV.CellsArea.SetCellValue(row, DGV.ColumnsHierarchy.Items(0), node.Text)
        FormatRow(row, CInt(node.Name))
        For Each child_node In node.Nodes
            addRow(child_node, row)
        Next
        isFillingDGV = False

    End Sub

    Private Sub FormatRow(ByRef row As HierarchyItem, ByRef row_id As Int32)

        rows_id_item_dic.Add(row_id, row)
        row.TextAlignment = ContentAlignment.MiddleLeft

    End Sub

#End Region

#Region "ComboBoxes Utilities"

    Private Sub InitComboBox(ByRef columnHierarchyItem As HierarchyItem, _
                             ByRef inputNode As TreeNode)


        Dim comboBox As New ComboBoxEditor()
        comboBox.DropDownHeight = comboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
        comboBox.DropDownWidth = CB_WIDTH

        Dim tmpDictionary As New Dictionary(Of String, String)
        For Each node As TreeNode In inputNode.Nodes
            comboBox.Items.Add(node.Text)
            tmpDictionary.Add(node.Text, node.Name)
        Next

        columnHierarchyItem.CellsEditor = comboBox
        AddHandler comboBox.EditBase.TextBox.KeyDown, AddressOf comboTextBox_KeyDown

    End Sub

    Private Sub InitCurrenciesCB()

        Dim comboBox As New ComboBoxEditor()
        comboBox.DropDownHeight = comboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
        comboBox.DropDownWidth = CB_WIDTH
        For Each currency_ As Int32 In GlobalVariables.Currencies.currencies_hash.Keys
            comboBox.Items.Add(currency_)
        Next

        columnsDictionary(ENTITIES_CURRENCY_VARIABLE).CellsEditor = comboBox
        AddHandler comboBox.EditBase.TextBox.KeyDown, AddressOf comboTextBox_KeyDown

    End Sub

#End Region

    Private Sub fillDGV()

        isFillingDGV = True
        For Each entity_id In GlobalVariables.Entities.entities_hash.Keys
            fillRow(entity_id, GlobalVariables.Entities.entities_hash(entity_id))
        Next
        isFillingDGV = False
        updateDGVFormat()

    End Sub

    Friend Sub fillRow(ByVal entity_id As Int32, _
                       ByVal entity_ht As Hashtable)
        Dim rowItem = rows_id_item_dic(entity_id)
        Dim column As HierarchyItem = columnsDictionary(ENTITIES_CURRENCY_VARIABLE)
        DGV.CellsArea.SetCellValue(rowItem, column, GlobalVariables.Currencies.currencies_hash(CInt(entity_ht(ENTITIES_CURRENCY_VARIABLE)))(NAME_VARIABLE))
        For Each root_category_node As TreeNode In entitiesFilterTV.Nodes
            FillSubFilters(root_category_node, entity_id, rowItem)
        Next
        If entity_ht(ENTITIES_ALLOW_EDITION_VARIABLE) = 0 Then rowItem.ImageIndex = 0 Else rowItem.ImageIndex = 1

    End Sub

    Private Sub FillSubFilters(ByRef node As TreeNode, ByRef entity_id As Int32, ByRef rowItem As HierarchyItem)
        Dim filter_value_name As String
        Dim column As HierarchyItem = columnsDictionary(node.Name)
        Dim filter_id As Int32 = CInt(node.Name)
        Dim mostNestedFilterId = GlobalVariables.Filters.GetMostNestedFilterId(CInt(node.Name))
        Dim mostNestedFilterValueId = GlobalVariables.EntitiesFilters.entitiesFiltersHash(entity_id)(mostNestedFilterId)
        Dim filterValueId = GlobalVariables.FiltersValues.GetFilterValueId(mostNestedFilterValueId, filter_id)
        filter_value_name = GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(NAME_VARIABLE)
        DGV.CellsArea.SetCellValue(rowItem, column, filter_value_name)

        For Each childNode As TreeNode In node.Nodes
            FillSubFilters(childNode, entity_id, rowItem)
        Next
    End Sub

    Private Sub updateDGVFormat()

        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, My.Settings.dgvFontSize, My.Settings.dgvFontSize)
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.Refresh()

    End Sub

#End Region


#Region "DGV Events"

    Friend Sub comboTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.Escape Then
            If e.KeyCode = Keys.Enter Then
                DGV.CloseEditor(True)
            Else
                DGV.CloseEditor(False)
            End If
        End If

    End Sub

    Private Sub dataGridView_HierarchyItemMouseClick(sender As Object, args As HierarchyItemMouseEventArgs)

        currentRowItem = args.HierarchyItem

    End Sub

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        currentRowItem = args.Cell.RowItem

    End Sub

#End Region


#Region "Copy Down"

    Friend Sub CopyValueDown()

        If Not DGV.CellsArea.SelectedCells(0) Is Nothing Then
            Dim row As HierarchyItem = DGV.CellsArea.SelectedCells(0).RowItem
            Dim column As HierarchyItem = DGV.CellsArea.SelectedCells(0).ColumnItem
            Dim value As String = DGV.CellsArea.SelectedCells(0).Value
            If row.Items.Count > 0 Then SetValueToChildrenItems(row, column, value) Else SetValueToSibbling(row, column, value)
            DGV.Refresh()
            DGV.Select()
        End If

    End Sub

    Private Sub SetValueToChildrenItems(ByRef rowItem_ As HierarchyItem, ByRef columnItem_ As HierarchyItem, ByRef value As String)

        For Each childItem As HierarchyItem In rowItem_.Items
            DGV.CellsArea.SetCellValue(childItem, columnItem_, value)
            If childItem.Items.Count > 0 Then SetValueToChildrenItems(childItem, columnItem_, value)
        Next

    End Sub

    Private Sub SetValueToSibbling(ByRef rowItem_ As HierarchyItem, ByRef columnItem_ As HierarchyItem, ByRef value As String)

        Dim parent As HierarchyItem = rowItem_.ParentItem
        Dim currentItem = rowItem_
        While Not currentItem.ItemBelow Is Nothing

            currentItem = currentItem.ItemBelow
            If currentItem.ParentItem.GetUniqueID = parent.GetUniqueID Then
                DGV.CellsArea.SetCellValue(currentItem, columnItem_, value)
                If currentItem.Items.Count > 0 Then SetValueToChildrenItems(currentItem, columnItem_, value)
            End If

        End While

    End Sub


#End Region


#Region "Excel Drop"

    ' Drop the current TGV in a new worksheet
    Friend Sub DropInExcel()

        Dim cell As Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS("Entities", _
                                                                                {"Entities Hierarchy"}, _
                                                                                {""})
        Dim nbRows As Int32
        For Each item As HierarchyItem In DGV.RowsHierarchy.Items
            DGVRowsCount(item, nbRows)
        Next

        Dim nbcols As Int32 = DGV.ColumnsHierarchy.Items.Count
        ReDim DGVArray(nbRows + 1, nbcols + 2)
        Dim i, j As Int32

        DGVArray(i, 0) = "Entity's Name"
        DGVArray(i, 1) = "Entity's Affiliate's Name"
        For j = 0 To nbcols - 1
            DGVArray(i, j + 2) = DGV.ColumnsHierarchy.Items(j).Caption
        Next

        For Each row In DGV.RowsHierarchy.Items
            fillInDGVArrayRow(row, 1)
        Next

        WorksheetWrittingFunctions.WriteArray(DGVArray, cell)
        ExcelFormatting.FormatEntitiesReport(GlobalVariables.APPS.ActiveSheet.range(cell, cell.Offset(UBound(DGVArray, 1), UBound(DGVArray, 2))))

    End Sub

    ' Recursively fill in row array
    Private Sub fillInDGVArrayRow(ByRef inputRow As HierarchyItem, ByRef rowIndex As Int32)

        Dim j As Int32

        DGVArray(rowIndex, 0) = DGV.CellsArea.GetCellValue(inputRow, DGV.ColumnsHierarchy.Items(0))
        If Not inputRow.ParentItem Is Nothing Then
            DGVArray(rowIndex, 1) = DGV.CellsArea.GetCellValue(inputRow.ParentItem, DGV.ColumnsHierarchy.Items(0))
        Else
            DGVArray(rowIndex, 1) = ""
        End If

        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            DGVArray(rowIndex, j + 2) = DGV.CellsArea.GetCellValue(inputRow, column)
            j = j + 1
        Next
        rowIndex = rowIndex + 1

        For Each childRow As HierarchyItem In inputRow.Items
            fillInDGVArrayRow(childRow, rowIndex)
        Next


    End Sub

    Private Sub DGVRowsCount(ByRef item As HierarchyItem, ByRef nbrows As Int32)

        ' -> maybe use tv total nodes count
        nbrows = nbrows + item.Items.Count
        For Each row As HierarchyItem In item.Items
            DGVRowsCount(row, nbrows)
        Next

    End Sub


#End Region


End Class