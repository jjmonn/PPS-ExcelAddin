' EntitiesControl.vb
'
' to be reviewed for proper disctinction between DGV and control !!
'
'
'
' Known bugs:
'
'
'
' Last modified: 04/09/2015
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.DataGridView.Filters
Imports VIBlend.Utilities
Imports System.Drawing
Imports System.ComponentModel
Imports Microsoft.Office.Interop


Friend Class EntitiesControl


#Region "Instance Variables"

    ' Objects
    Private Controller As EntitiesController
    Private entitiesTV As TreeView
    Private entitiesFilterTV As TreeView
    Private entitiesFilterValuesTV As TreeView
    Friend DGV As New vDataGridView
    Private CP As CircularProgressUI

    ' Variables
    Friend columnsVariableItemDictionary As New Dictionary(Of String, HierarchyItem)
    Friend rows_id_item_dic As New Dictionary(Of Int32, HierarchyItem)
    Private DGVArray(,) As String
    Private filterGroup As New FilterGroup(Of String)()
    Friend currentRowItem As HierarchyItem
    Friend isFillingDGV As Boolean

    ' Constants
    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER
    Friend Const CURRENCY_COLUMN_NAME As String = "Currency"
    Friend Const NAME_COLUMN_NAME As String = "Name"
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 7


#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_controller As EntitiesController, _
                   ByRef p_entitiesTV As TreeView, _
                   ByRef p_entitiesFilterValuesTV As TreeView, _
                   ByRef p_entitiesFiltersTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = p_controller
        entitiesTV = p_entitiesTV
        entitiesFilterTV = p_entitiesFiltersTV
        entitiesFilterValuesTV = p_entitiesFilterValuesTV

        initFilters()
        InitializeDGVDisplay()
        DGVColumnsInitialize()
        DGVRowsInitialize(entitiesTV)
        fillDGV()

        Me.TableLayoutPanel1.Controls.Add(DGV, 0, 1)
        DGV.Dock = DockStyle.Fill
        DGV.ContextMenuStrip = RCM_TGV

        AddHandler DGV.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler DGV.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick
        AddHandler DGV.CellValueChanged, AddressOf dataGridView_CellValueChanged
        AddHandler DGV.KeyDown, AddressOf DGV_KeyDown

    End Sub

  
#End Region


#Region "Interface"

    Delegate Sub UpdateEntity_Delegate(ByRef ht As Hashtable)
    Friend Sub UpdateEntity(ByRef ht As Hashtable)

        If InvokeRequired Then
            Dim MyDelegate As New UpdateEntity_Delegate(AddressOf UpdateEntity)
            Me.Invoke(MyDelegate, New Object() {ht})
        Else
            isFillingDGV = True
            FillRow(ht(ID_VARIABLE), ht)
            isFillingDGV = False
        End If

    End Sub

    Delegate Sub DeleteEntity_Delegate(ByRef id As Int32)
    Friend Sub DeleteEntity(ByRef id As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New DeleteEntity_Delegate(AddressOf DeleteEntity)
            Me.Invoke(MyDelegate, New Object() {id})
        Else
            rows_id_item_dic(id).Delete()
            rows_id_item_dic.Remove(id)
            DGV.Refresh()
        End If

    End Sub

#End Region


#Region "Calls Back"

#Region "DGV Right Click Menu"

    Private Sub cop_down_bt_Click(sender As Object, e As EventArgs) Handles copy_down_bt.Click

        CopyValueDown()
        DGV.Refresh()

    End Sub

    Private Sub drop_to_excel_bt_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        DropInExcel()

    End Sub

    Private Sub CreateEntityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateEntityToolStripMenuItem.Click

        AddEntity_cmd_Click(sender, e)

    End Sub

    Private Sub DeleteEntityToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteEntityToolStripMenuItem2.Click

        DeleteEntity_cmd_Click(sender, e)

    End Sub

#End Region

#Region "Main menu"

    Private Sub AddEntity_cmd_Click(sender As Object, e As EventArgs) Handles CreateEntityToolStripMenuItem.Click

        Me.Hide()
        Controller.ShowNewEntityUI()

    End Sub

    Private Sub DeleteEntity_cmd_Click(sender As Object, e As EventArgs) Handles DeleteEntityToolStripMenuItem.Click

        If Not currentRowItem Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the entity " + Chr(13) + Chr(13) + _
                                                    DGV.CellsArea.GetCellValue(currentRowItem, DGV.ColumnsHierarchy.Items(0)) + Chr(13) + Chr(13) + _
                                                     "This entity and all sub entities will be deleted, do you confirm?" + Chr(13) + Chr(13), _
                                                     "Entity deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Dim entity_id As String = Controller.GetEntityId(DGV.CellsArea.GetCellValue(currentRowItem, DGV.ColumnsHierarchy.Items(0)))
                Controller.DeleteEntity(entity_id)
            End If
        Else
            MsgBox("An Entity must be selected in order to be deleted")
        End If
        currentRowItem = Nothing

    End Sub

    Private Sub dropInExcel_cmd_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        DropInExcel()

    End Sub

#End Region

#End Region


#Region "DGV"

    Private Sub InitializeDGVDisplay()

        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.ColumnsHierarchy.AllowResize = True
        DGV.RowsHierarchy.AllowDragDrop = True
        DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        DGV.AllowDragDropIndication = True
        DGV.AllowCopyPaste = True
        DGV.FilterDisplayMode = FilterDisplayMode.Custom
        DGV.VIBlendTheme = DGV_VI_BLEND_STYLE
        DGV.BackColor = SystemColors.Control
        'DGV.ImageList = EntitiesIL

    End Sub

    Private Sub fillDGV()

        isFillingDGV = True
        For Each entity_id In GlobalVariables.Entities.entities_hash.Keys
            FillRow(entity_id, GlobalVariables.Entities.entities_hash(entity_id))
        Next
        isFillingDGV = False
        updateDGVFormat()

    End Sub


#Region "Columns Initialization"

    Private Sub DGVColumnsInitialize()

        DGV.ColumnsHierarchy.Clear()
        columnsVariableItemDictionary.Clear()

        ' Entities Name Column
        Dim nameColumn As HierarchyItem = DGV.ColumnsHierarchy.Items.Add("Entity")
        nameColumn.ItemValue = NAME_VARIABLE

        Dim nameTextBox As New TextBoxEditor()
        nameTextBox.ActivationFlags = EditorActivationFlags.KEY_PRESS_ENTER
        nameColumn.CellsEditor = nameTextBox
        columnsVariableItemDictionary.Add(NAME_VARIABLE, nameColumn)

        ' Entities Currency Column
        Dim currencyColumn As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(CURRENCY_COLUMN_NAME)
        columnsVariableItemDictionary.Add(ENTITIES_CURRENCY_VARIABLE, currencyColumn)
        currencyColumn.ItemValue = ENTITIES_CURRENCY_VARIABLE
        InitCurrenciesComboBox()
        currencyColumn.AllowFiltering = True
        ' CreateFilter(col1)

        For Each rootNode As TreeNode In entitiesFilterTV.Nodes
            CreateSubFilters(rootNode)
            '   CreateFilter(col)
        Next

    End Sub

    Private Sub CreateSubFilters(ByRef node As TreeNode)

        Dim col As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(node.Text)

        columnsVariableItemDictionary.Add(node.Name, col)
        col.ItemValue = node.Name
        col.AllowFiltering = True

        For Each childNode As TreeNode In node.Nodes
            CreateSubFilters(childNode)
        Next

    End Sub

    'Private Sub CreateFilter(ByRef column As HierarchyItem)

    '    Dim filter As New HierarchyItemFilter()
    '    filter.Item = column
    '    filter.Filter = filterGroup
    '    DGV.RowsHierarchy.Filters.Add(filter)

    'End Sub

    Private Sub initFilters()

        Dim stringFilter1 As New StringFilter()
        stringFilter1.ComparisonOperator = StringFilterComparisonOperator.NOT_EMPTY
        stringFilter1.Value = ""

        Dim stringFilter2 As New StringFilter()
        stringFilter1.ComparisonOperator = StringFilterComparisonOperator.CONTAINS
        stringFilter1.Value = "a"

        FilterGroup.AddFilter(FilterOperator.AND, stringFilter1)
        FilterGroup.AddFilter(FilterOperator.AND, stringFilter2)

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
        Dim row As HierarchyItem = CreateRow(node.Name, parent_row)
        For Each child_node In node.Nodes
            addRow(child_node, row)
        Next
        isFillingDGV = False

    End Sub

    Private Function CreateRow(ByRef entityId As Int32, _
                               Optional ByRef parentRow As HierarchyItem = Nothing) As HierarchyItem

        Dim row As HierarchyItem
        If parentRow Is Nothing Then
            row = DGV.RowsHierarchy.Items.Add("")
        Else
            row = parentRow.Items.Add("")
        End If
        row.ItemValue = entityId
        FormatRow(row, entityId)
        Return row

    End Function

    Private Sub FormatRow(ByRef row As HierarchyItem, ByRef row_id As Int32)

        If rows_id_item_dic.ContainsKey(row_id) Then
            rows_id_item_dic(row_id) = row
        Else
            rows_id_item_dic.Add(row_id, row)
        End If
        row.TextAlignment = ContentAlignment.MiddleLeft

    End Sub

    Private Sub InitCurrenciesComboBox()

        Dim comboBox As New ComboBoxEditor()
        comboBox.DropDownList = True
        comboBox.DropDownHeight = comboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
        comboBox.DropDownWidth = CB_WIDTH
        For Each currencyId As Int32 In GlobalVariables.Currencies.currencies_hash.Keys
            comboBox.Items.Add(GlobalVariables.Currencies.currencies_hash(currencyId)(NAME_VARIABLE))
        Next

        columnsVariableItemDictionary(ENTITIES_CURRENCY_VARIABLE).CellsEditor = comboBox
        AddHandler comboBox.EditBase.TextBox.KeyDown, AddressOf comboTextBox_KeyDown

    End Sub

#End Region


#Region "DGV Filling"

    Friend Sub FillRow(ByVal entity_id As Int32, _
                       ByVal entity_ht As Hashtable)

        Dim rowItem As HierarchyItem
        If rows_id_item_dic.ContainsKey(entity_id) Then
            rowItem = rows_id_item_dic(entity_id)
        Else
            Dim parentEntityId As Int32 = entity_ht(PARENT_ID_VARIABLE)
            If parentEntityId = 0 Then
                rowItem = CreateRow(entity_id)
            Else
                rowItem = CreateRow(entity_id, rows_id_item_dic(parentEntityId))
            End If
        End If
        Dim column As HierarchyItem = columnsVariableItemDictionary(ENTITIES_CURRENCY_VARIABLE)
        DGV.CellsArea.SetCellValue(rowItem, columnsVariableItemDictionary(NAME_VARIABLE), entity_ht(NAME_VARIABLE))
        DGV.CellsArea.SetCellValue(rowItem, column, GlobalVariables.Currencies.currencies_hash(CInt(entity_ht(ENTITIES_CURRENCY_VARIABLE)))(NAME_VARIABLE))
        For Each filterNode As TreeNode In entitiesFilterTV.Nodes
            FillSubFilters(filterNode, entity_id, rowItem)
        Next
        If entity_ht(ENTITIES_ALLOW_EDITION_VARIABLE) = 0 Then rowItem.ImageIndex = 0 Else rowItem.ImageIndex = 1

    End Sub

    Private Sub FillSubFilters(ByRef filterNode As TreeNode, _
                               ByRef entity_id As Int32, _
                               ByRef rowItem As HierarchyItem)

        Dim combobox As New ComboBoxEditor
        combobox.DropDownList = True
        Dim columnItem As HierarchyItem = columnsVariableItemDictionary(filterNode.Name)
        Dim filterValueId = GlobalVariables.EntitiesFilters.GetFilterValueId(CInt(filterNode.Name), entity_id)
        Dim filter_value_name = GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(NAME_VARIABLE)
        DGV.CellsArea.SetCellValue(rowItem, columnItem, filter_value_name)

        ' Filters Choices Setup
        If filterNode.Parent Is Nothing Then
            ' Root Filter
            Dim valuesNames = GlobalVariables.FiltersValues.GetFiltervaluesList(filterNode.Name, NAME_VARIABLE)
            For Each valueName As String In valuesNames
                combobox.Items.Add(valueName)
            Next
        Else
            ' Child Filter
            Dim parentFilterFilterValueId As Int32 = GlobalVariables.EntitiesFilters.GetFilterValueId(filterNode.Parent.Name, entity_id)     ' Child Filter Id
            Dim filterValuesIds = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds({parentFilterFilterValueId})
            For Each Id As Int32 In filterValuesIds
                combobox.Items.Add(GlobalVariables.FiltersValues.filtervalues_hash(Id)(NAME_VARIABLE))
            Next
        End If

        ' Add ComboBoxEditor to Cell
        DGV.CellsArea.SetCellEditor(rowItem, columnItem, combobox)

        ' Recursive if Filters Children exist
        For Each childFilterNode As TreeNode In filterNode.Nodes
            FillSubFilters(childFilterNode, entity_id, rowItem)
        Next

    End Sub

    Private Sub updateDGVFormat()

        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, My.Settings.dgvFontSize, My.Settings.dgvFontSize)
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.Refresh()

    End Sub

#End Region


#Region "DGV Updates"

    ' Update Parents and Children Filters Cells or comboboxes after a filter cell has been edited
    Friend Sub UpdateEntitiesFiltersAfterEdition(ByRef entityId As Int32, _
                                                 ByRef filterId As Int32, _
                                                 ByRef filterValueId As Int32)

        isFillingDGV = True
        Dim filterNode As TreeNode = entitiesFilterTV.Nodes.Find(filterId, True)(0)

        ' Update parent filters recursively
        UpdateParentFiltersValues(rows_id_item_dic(entityId), _
                                 entityId, _
                                 filterNode, _
                                 filterValueId)

        ' Update children filters comboboxes recursively
        For Each childFilterNode As TreeNode In filterNode.Nodes
            UpdateChildrenFiltersComboBoxes(rows_id_item_dic(entityId), _
                                            childFilterNode, _
                                            {filterValueId})
        Next
        isFillingDGV = False

    End Sub

    Private Sub UpdateParentFiltersValues(ByRef row As HierarchyItem, _
                                          ByRef entityId As Int32, _
                                          ByRef filterNode As TreeNode,
                                          ByRef filterValueId As Int32)

        If Not filterNode.Parent Is Nothing Then
            Dim parentFilterNode As TreeNode = filterNode.Parent
            Dim column As HierarchyItem = columnsVariableItemDictionary(parentFilterNode.Name)
            Dim parentFilterValueId As Int32 = GlobalVariables.FiltersValues.filtervalues_hash(filterValueId)(PARENT_FILTER_VALUE_ID_VARIABLE)
            Dim filtervaluename = GlobalVariables.FiltersValues.filtervalues_hash(parentFilterValueId)(NAME_VARIABLE)
            DGV.CellsArea.SetCellValue(row, column, filtervaluename)

            ' Recursively update parent filters
            UpdateParentFiltersValues(row, _
                                      entityId, _
                                      parentFilterNode, _
                                      parentFilterValueId)
        End If

    End Sub

    Private Sub UpdateChildrenFiltersComboBoxes(ByRef row As HierarchyItem, _
                                                ByRef filterNode As TreeNode,
                                                ByRef parentFilterValueIds() As Int32)

        Dim column As HierarchyItem = columnsVariableItemDictionary(filterNode.Name)
        Dim comboBox As New ComboBoxEditor

        Dim filterValuesIds As Int32() = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds(parentFilterValueIds)
        For Each Id As Int32 In filterValuesIds
            comboBox.Items.Add(GlobalVariables.FiltersValues.filtervalues_hash(Id)(NAME_VARIABLE))
        Next

        ' Set Cell value to nothing
        DGV.CellsArea.SetCellValue(row, column, Nothing)

        ' Add ComboBoxEditor to Cell
        DGV.CellsArea.SetCellEditor(row, column, comboBox)

        ' Recursivly update children comboboxes
        For Each childFilterNode As TreeNode In filterNode.Nodes
            UpdateChildrenFiltersComboBoxes(row, _
                                            childFilterNode, _
                                            filterValuesIds)
        Next

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

    Private Sub DGV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteEntity_cmd_Click(sender, e)
        End Select

    End Sub

    Private Sub dataGridView_CellValueChanged(sender As Object, args As CellEventArgs)

        If isFillingDGV = False Then
            If (Not args.Cell.Value Is Nothing AndAlso args.Cell.Value <> "") Then
                Dim entityId As Int32 = args.Cell.RowItem.ItemValue


                Select Case args.Cell.ColumnItem.ItemValue

                    Case ENTITIES_CURRENCY_VARIABLE
                        Dim currencyId As Int32 = GlobalVariables.Currencies.GetCurrencyId(args.Cell.Value)
                        If (currencyId = 0) Then
                            MsgBox("Currency " & args.Cell.Value & " not found.")
                            Exit Sub
                        Else
                            Controller.UpdateEntity(entityId, _
                                                    args.Cell.ColumnItem.ItemValue, _
                                                    currencyId)
                        End If

                    Case NAME_VARIABLE
                        Dim newEntityName As String = args.Cell.Value
                        Controller.UpdateEntity(entityId, _
                                                args.Cell.ColumnItem.ItemValue, _
                                                newEntityName)
                    Case Else
                        Dim filterValueName As String = args.Cell.Value
                        Dim filterValueId As Int32 = GlobalVariables.FiltersValues.GetFilterValueId(filterValueName)
                        If filterValueId = 0 Then
                            MsgBox("The Filter Value Name " & filterValueName & " could not be found.")
                            Exit Sub
                        End If
                        Dim filterId As Int32 = args.Cell.ColumnItem.ItemValue
                        UpdateEntitiesFiltersAfterEdition(entityId, filterId, filterValueId)
                        Controller.UpdateFilterValue(entityId, filterId, filterValueId)
                End Select

            End If
        End If

    End Sub

#End Region

#End Region


#Region "Utilities"

    Friend Function getCurrentRowItem() As HierarchyItem

        Return currentRowItem

    End Function

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

#End Region




End Class
