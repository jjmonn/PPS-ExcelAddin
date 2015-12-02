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
' Last modified: 09/11/2015
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
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class EntitiesView


#Region "Instance Variables"

    ' Objects
    Private m_controller As EntitiesController
    Private m_entitiesTreeview As vTreeView
    Private m_entitiesFilterTreeview As vTreeView
    Private m_entitiesFilterValuesTreeview As vTreeView
    Friend m_entitiesDataGridView As New vDataGridView
    Private m_currenciesComboBox As New ComboBoxEditor()

    ' Variables
    Private m_DGVArray(,) As String
    Private m_filterGroup As New FilterGroup(Of String)()
    Friend m_currentRowItem As HierarchyItem
    Friend m_isFillingDGV As Boolean

    ' Constants
    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER
    Friend Const CURRENCY_COLUMN_NAME As String = "Currency"
    Friend Const NAME_COLUMN_NAME As String = "Name"
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 7
    Private Const COLUMNS_WIDTH As Single = 150


#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_controller As EntitiesController, _
                   ByRef p_entitiesTV As vTreeView, _
                   ByRef p_entitiesFilterValuesTV As vTreeView, _
                   ByRef p_entitiesFiltersTV As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        m_entitiesTreeview = p_entitiesTV
        m_entitiesFilterTreeview = p_entitiesFiltersTV
        m_entitiesFilterValuesTreeview = p_entitiesFilterValuesTV

        initFilters()
        InitCurrenciesComboBox()
        InitializeDGVDisplay()
        DGVColumnsInitialize()
        DataGridViewsUtil.DGVRowsInitialize(m_entitiesDataGridView, m_entitiesTreeview)
        fillDGV()
        m_entitiesDataGridView.RowsHierarchy.ExpandAllItems()

        Me.TableLayoutPanel1.Controls.Add(m_entitiesDataGridView, 0, 1)
        m_entitiesDataGridView.Dock = DockStyle.Fill
       
        AddHandler m_entitiesDataGridView.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler m_entitiesDataGridView.MouseDown, AddressOf DataGridViewRightClick
        AddHandler m_entitiesDataGridView.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick
        AddHandler m_entitiesDataGridView.CellValueChanged, AddressOf dataGridView_CellValueChanged
        AddHandler m_entitiesDataGridView.KeyDown, AddressOf DGV_KeyDown
        DesactivateUnallowed()
        MultilanguageSetup()

    End Sub

    Private Sub DesactivateUnallowed()
        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then
            RenameEntityButton.Enabled = False
            CreateEntityToolStripMenuItem.Enabled = False
            DeleteEntityToolStripMenuItem.Enabled = False
            DeleteEntityToolStripMenuItem2.Enabled = False
            CreateANewEntityToolStripMenuItem.Enabled = False
            copy_down_bt.Enabled = False
        End If
    End Sub

    Private Sub InitCurrenciesComboBox()

        m_currenciesComboBox.DropDownList = True
        m_currenciesComboBox.DropDownHeight = m_currenciesComboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
        m_currenciesComboBox.DropDownWidth = CB_WIDTH
        For Each currency As Currency In GlobalVariables.Currencies.GetDictionary().Values
            If currency.InUse = False Then Continue For
            m_currenciesComboBox.Items.Add(currency.Name)
        Next
        AddHandler m_currenciesComboBox.EditBase.TextBox.KeyDown, AddressOf comboTextBox_KeyDown

    End Sub

    Private Sub MultilanguageSetup()

        Me.RenameEntityButton.Text = Local.GetValue("general.rename")
        Me.CreateEntityToolStripMenuItem.Text = Local.GetValue("general.create")
        Me.DeleteEntityToolStripMenuItem2.Text = Local.GetValue("general.delete")
        Me.copy_down_bt.Text = Local.GetValue("general.copy_down")
        Me.drop_to_excel_bt.Text = Local.GetValue("general.drop_on_excel")
        Me.AutoResizeColumnsButton.Text = Local.GetValue("general.auto_resize_columns")
        Me.ExpandAllBT.Text = Local.GetValue("general.expand_all")
        Me.CollapseAllBT.Text = Local.GetValue("general.collapse_all")
        Me.EditToolStripMenuItem.Text = Local.GetValue("general.entities")
        Me.CreateANewEntityToolStripMenuItem.Text = Local.GetValue("general.create")
        Me.DeleteEntityToolStripMenuItem.Text = Local.GetValue("general.delete")
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("general.drop_on_excel")

    End Sub

#End Region


#Region "Interface"

    Friend Sub LoadInstanceVariables_Safe()
        Try
            Dim MyDelegate As New LoadInstanceVariables_Delegate(AddressOf LoadInstanceVariables)
            Me.Invoke(MyDelegate, New Object() {})
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine(ex.Message)
        End Try
    End Sub

    Delegate Sub LoadInstanceVariables_Delegate()
    Friend Sub LoadInstanceVariables()
        If InvokeRequired Then
            Dim MyDelegate As New LoadInstanceVariables_Delegate(AddressOf LoadInstanceVariables)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            m_controller.LoadInstanceVariables()
        End If
    End Sub

    Delegate Sub UpdateEntity_Delegate(ByRef ht As AxisElem)
    Friend Sub UpdateEntity(ByRef ht As AxisElem)

        If InvokeRequired Then
            Dim MyDelegate As New UpdateEntity_Delegate(AddressOf UpdateEntity)
            Me.Invoke(MyDelegate, New Object() {ht})
        Else
            m_isFillingDGV = True
            FillRow(ht.Id, ht)
            UpdateDGVFormat()
            m_isFillingDGV = False
        End If

    End Sub

    Delegate Sub DeleteEntity_Delegate(ByRef id As Int32)
    Friend Sub DeleteEntity(ByRef id As Int32)

        If InvokeRequired Then
            Dim MyDelegate As New DeleteEntity_Delegate(AddressOf DeleteEntity)
            Me.Invoke(MyDelegate, New Object() {id})
        Else
            Dim row As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.RowsHierarchy, id)
            If row IsNot Nothing Then row.Delete()
            m_entitiesDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
            m_entitiesDataGridView.Refresh()
        End If

    End Sub

#End Region


#Region "Calls Back"

#Region "DGV Right Click Menu"

    Private Sub DataGridViewRightClick(sender As Object, e As MouseEventArgs)

        If (e.Button <> MouseButtons.Right) Then Exit Sub
        Dim target As HierarchyItem = m_entitiesDataGridView.RowsHierarchy.HitTest(e.Location)
        If target IsNot Nothing Then
            m_currentRowItem = DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.RowsHierarchy, target.ItemValue)
        Else
            Dim target2 As GridCell = m_entitiesDataGridView.CellsArea.HitTest(e.Location)
            If target2 Is Nothing Then Exit Sub
            m_currentRowItem = target2.RowItem
        End If
        m_entitiesRightClickMenu.Visible = True
        m_entitiesRightClickMenu.Bounds = New Rectangle(MousePosition, New Size(m_entitiesRightClickMenu.Width, m_entitiesRightClickMenu.Height))

    End Sub

    Private Sub RenameEntityButton_Click(sender As Object, e As EventArgs) Handles RenameEntityButton.Click

        If m_currentRowItem Is Nothing Then
            MsgBox(Local.GetValue("entities_edition.msg_selection"))
            Exit Sub
        End If
        Dim newEntityName As String = InputBox(Local.GetValue("entities_edition.msg_entity_name"), , m_currentRowItem.Caption)
        If newEntityName <> "" Then
            m_controller.UpdateEntityName(m_currentRowItem.ItemValue, newEntityName)
        End If

    End Sub

    Private Sub cop_down_bt_Click(sender As Object, e As EventArgs) Handles copy_down_bt.Click

        CopyValueDown()
        m_entitiesDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        m_entitiesDataGridView.Refresh()

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

    Private Sub AutoResizeColumnsButton_Click(sender As Object, e As EventArgs) Handles AutoResizeColumnsButton.Click
        m_entitiesDataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
    End Sub

#End Region

#Region "Main menu"

    Private Sub AddEntity_cmd_Click(sender As Object, e As EventArgs) Handles CreateEntityToolStripMenuItem.Click

        Me.Hide()
        m_controller.ShowNewEntityUI()

    End Sub

    Private Sub DeleteEntity_cmd_Click(sender As Object, e As EventArgs) Handles DeleteEntityToolStripMenuItem.Click

        If Not m_currentRowItem Is Nothing Then
            Dim confirm As Integer = MessageBox.Show(Local.GetValue("entities_edition.msg_delete1") + Chr(13) + Chr(13) + _
                                                    m_currentRowItem.Caption + Chr(13) + Chr(13) + _
                                                    Local.GetValue("entities_edition.msg_delete2") + Chr(13) + Chr(13), _
                                                    Local.GetValue("entities_edition.title_delete_confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Dim entity_id As String = m_currentRowItem.ItemValue
                m_controller.DeleteEntity(entity_id)
            End If
        Else
            MsgBox(Local.GetValue("entities_edition.msg_selection"))
        End If
        m_currentRowItem = Nothing

    End Sub

    Private Sub dropInExcel_cmd_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click, SendEntitiesHierarchyToExcelToolStripMenuItem.Click

        DropInExcel()

    End Sub

    Private Sub ExpandAllBT_Click(sender As Object, e As EventArgs) Handles ExpandAllBT.Click
        m_entitiesDataGridView.RowsHierarchy.ExpandAllItems()
    End Sub

    Private Sub CollapseAllBT_Click(sender As Object, e As EventArgs) Handles CollapseAllBT.Click
        m_entitiesDataGridView.RowsHierarchy.CollapseAllItems()
    End Sub

#End Region

#End Region


#Region "DGV"

    Private Sub InitializeDGVDisplay()

        m_entitiesDataGridView.ColumnsHierarchy.AllowResize = True
        m_entitiesDataGridView.RowsHierarchy.AllowDragDrop = True
        m_entitiesDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = True
        m_entitiesDataGridView.AllowDragDropIndication = True
        m_entitiesDataGridView.AllowCopyPaste = True
        m_entitiesDataGridView.FilterDisplayMode = FilterDisplayMode.Custom
        m_entitiesDataGridView.VIBlendTheme = DGV_VI_BLEND_STYLE
        m_entitiesDataGridView.BackColor = SystemColors.Control
        m_entitiesDataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        'DGV.ImageList = EntitiesIL

    End Sub

    Private Sub fillDGV()

        m_isFillingDGV = True
        For Each entity As AxisElem In GlobalVariables.AxisElems.GetDictionary(AxisType.Entities).Values
            FillRow(entity.Id, entity)
        Next
        m_isFillingDGV = False
        UpdateDGVFormat()

    End Sub


#Region "Columns Initialization"

    Private Sub DGVColumnsInitialize()

        m_entitiesDataGridView.ColumnsHierarchy.Clear()

        ' Entities Currency Column
        Dim currencyColumn As HierarchyItem = m_entitiesDataGridView.ColumnsHierarchy.Items.Add(CURRENCY_COLUMN_NAME)
        currencyColumn.ItemValue = ENTITIES_CURRENCY_VARIABLE
        currencyColumn.AllowFiltering = True
        currencyColumn.Width = COLUMNS_WIDTH
        ' CreateFilter(col1)

        For Each rootNode As vTreeNode In m_entitiesFilterTreeview.Nodes
            CreateSubFilters(rootNode)
            '   CreateFilter(col)
        Next

    End Sub

    Private Sub CreateSubFilters(ByRef node As vTreeNode)

        Dim col As HierarchyItem = m_entitiesDataGridView.ColumnsHierarchy.Items.Add(node.Text)
        col.ItemValue = node.Value
        col.AllowFiltering = True
        col.Width = COLUMNS_WIDTH
        For Each childNode As vTreeNode In node.Nodes
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

        m_filterGroup.AddFilter(FilterOperator.AND, stringFilter1)
        m_filterGroup.AddFilter(FilterOperator.AND, stringFilter2)

    End Sub

#End Region


#Region "DGV Filling"

    Friend Sub FillRow(ByVal p_entityId As Int32, _
                       ByVal p_entityHashtable As AxisElem)

        Dim rowItem As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.RowsHierarchy, p_entityId)
        If rowItem Is Nothing Then
            Dim parentEntityId As Int32 = p_entityHashtable.ParentId
            m_isFillingDGV = True
            If parentEntityId = 0 Then
                rowItem = DataGridViewsUtil.CreateRow(m_entitiesDataGridView, p_entityId, p_entityHashtable.Name)
            Else
                Dim parentRow As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.RowsHierarchy, parentEntityId)
                rowItem = DataGridViewsUtil.CreateRow(m_entitiesDataGridView, p_entityId, p_entityHashtable.Name, parentRow)
            End If
            m_isFillingDGV = False
        End If
        rowItem.Caption = p_entityHashtable.Name
        Dim column As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.ColumnsHierarchy, ENTITIES_CURRENCY_VARIABLE)
        If p_entityHashtable.AllowEdition = True Then
            If GlobalVariables.Users.CurrentUserIsAdmin() Then m_entitiesDataGridView.CellsArea.SetCellEditor(rowItem, column, m_currenciesComboBox)
            Dim entityCurrency As EntityCurrency = GlobalVariables.EntityCurrencies.GetValue(p_entityId)
            If entityCurrency Is Nothing Then Exit Sub
            Dim currency As Currency = GlobalVariables.Currencies.GetValue(CInt(entityCurrency.CurrencyId))
            If currency Is Nothing Then Exit Sub

            m_entitiesDataGridView.CellsArea.SetCellValue(rowItem, column, currency.Name)

            For Each filterNode As vTreeNode In m_entitiesFilterTreeview.Nodes
                FillSubFilters(filterNode, p_entityId, rowItem)
            Next
        End If
        If p_entityHashtable.AllowEdition = False Then rowItem.ImageIndex = 0 Else rowItem.ImageIndex = 1

    End Sub

    Private Sub FillSubFilters(ByRef filterNode As vTreeNode, _
                               ByRef entity_id As Int32, _
                               ByRef rowItem As HierarchyItem)

        Dim combobox As New ComboBoxEditor
        combobox.DropDownList = True
        Dim columnItem As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.ColumnsHierarchy, filterNode.Value)
        Dim filterValueId As UInt32 = GlobalVariables.AxisFilters.GetFilterValueId(AxisType.Entities, CInt(filterNode.Value), entity_id)
        Dim filter_value_name As String = ""
        If filterValueId <> 0 Then
            filter_value_name = GlobalVariables.FiltersValues.GetValueName(filterValueId)
        End If

        m_entitiesDataGridView.CellsArea.SetCellValue(rowItem, columnItem, filter_value_name)

        ' Filters Choices Setup
        If filterNode.Parent Is Nothing Then
            ' Root Filter
            If Not GlobalVariables.FiltersValues.GetDictionary(filterNode.Value) Is Nothing Then
                For Each value As FilterValue In GlobalVariables.FiltersValues.GetDictionary(filterNode.Value).Values
                    combobox.Items.Add(value.Name)
                Next
            End If
        Else
            ' Child Filter
            Dim parentFilterFilterValueId As Int32 = GlobalVariables.AxisFilters.GetFilterValueId(AxisType.Entities, filterNode.Parent.Value, entity_id)     ' Child Filter Id
            Dim filterValuesIds = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds({parentFilterFilterValueId})
            For Each Id As UInt32 In filterValuesIds
                combobox.Items.Add(GlobalVariables.FiltersValues.GetValueName(Id))
            Next
        End If

        ' Add ComboBoxEditor to Cell
        If GlobalVariables.Users.CurrentUserIsAdmin() Then m_entitiesDataGridView.CellsArea.SetCellEditor(rowItem, columnItem, combobox)

        ' Recursive if Filters Children exist
        If filterNode.Nodes Is Nothing Then Exit Sub
        For Each childFilterNode As vTreeNode In filterNode.Nodes
            FillSubFilters(childFilterNode, entity_id, rowItem)
        Next

    End Sub

    Private Sub UpdateDGVFormat()

        DataGridViewsUtil.DGVSetHiearchyFontSize(m_entitiesDataGridView, My.Settings.dgvFontSize, My.Settings.dgvFontSize)
        DataGridViewsUtil.FormatDGVRowsHierarchy(m_entitiesDataGridView)
        m_entitiesDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        m_entitiesDataGridView.Refresh()

    End Sub

#End Region


#Region "DGV Updates"

    ' Update Parents and Children Filters Cells or comboboxes after a filter cell has been edited
    Friend Sub UpdateEntitiesFiltersAfterEdition(ByRef entityId As Int32, _
                                                 ByRef filterId As Int32, _
                                                 ByRef filterValueId As Int32)

        m_isFillingDGV = True
        Dim filterNode As vTreeNode = VTreeViewUtil.FindNode(m_entitiesFilterTreeview, filterId)

        If filterNode Is Nothing Then Exit Sub
        ' Update parent filters recursively
        UpdateParentFiltersValues(DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.RowsHierarchy, entityId), _
                                 entityId, _
                                 filterNode, _
                                 filterValueId)

        ' Update children filters comboboxes recursively
        For Each childFilterNode As vTreeNode In filterNode.Nodes
            UpdateChildrenFiltersComboBoxes(DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.RowsHierarchy, entityId), _
                                            childFilterNode, _
                                            {filterValueId})
        Next
        m_entitiesDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        m_entitiesDataGridView.Refresh()
        m_isFillingDGV = False

    End Sub

    Private Sub UpdateParentFiltersValues(ByRef row As HierarchyItem, _
                                          ByRef entityId As Int32, _
                                          ByRef filterNode As vTreeNode,
                                          ByRef filterValueId As UInt32)

        Dim filterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(filterValueId)
        If filterValue Is Nothing Then Exit Sub

        If Not filterNode.Parent Is Nothing Then
            Dim parentFilterNode As vTreeNode = filterNode.Parent
            Dim column As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.ColumnsHierarchy, parentFilterNode.Value)
            Dim parentFilterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(filterValue.ParentId)
            If parentFilterValue Is Nothing Then Exit Sub

            m_entitiesDataGridView.CellsArea.SetCellValue(row, column, parentFilterValue.Name)

            ' Recursively update parent filters
            UpdateParentFiltersValues(row, _
                                      entityId, _
                                      parentFilterNode, _
                                      parentFilterValue.Id)
        End If

    End Sub

    Private Sub UpdateChildrenFiltersComboBoxes(ByRef row As HierarchyItem, _
                                                ByRef filterNode As vTreeNode,
                                                ByRef parentFilterValueIds() As Int32)

        Dim column As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_entitiesDataGridView.ColumnsHierarchy, filterNode.Value)
        Dim comboBox As New ComboBoxEditor

        Dim filterValuesIds As Int32() = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds(parentFilterValueIds)
        For Each Id As UInt32 In filterValuesIds
            comboBox.Items.Add(GlobalVariables.FiltersValues.GetValueName(Id))
        Next

        ' Set Cell value to nothing
        m_entitiesDataGridView.CellsArea.SetCellValue(row, column, Nothing)

        ' Add ComboBoxEditor to Cell
        If GlobalVariables.Users.CurrentUserIsAdmin() Then m_entitiesDataGridView.CellsArea.SetCellEditor(row, column, comboBox)

        ' Recursivly update children comboboxes
        For Each childFilterNode As vTreeNode In filterNode.Nodes
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
                m_entitiesDataGridView.CloseEditor(True)
            Else
                m_entitiesDataGridView.CloseEditor(False)
            End If
        End If

    End Sub

    Private Sub dataGridView_HierarchyItemMouseClick(sender As Object, args As HierarchyItemMouseEventArgs)

        m_currentRowItem = args.HierarchyItem

    End Sub

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        m_currentRowItem = args.Cell.RowItem

    End Sub

    Private Sub DGV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteEntity_cmd_Click(sender, e)
        End Select

    End Sub

    Private Sub dataGridView_CellValueChanged(sender As Object, args As CellEventArgs)

        If m_isFillingDGV = False Then
            If (Not args.Cell.Value Is Nothing AndAlso args.Cell.Value <> "") Then
                Dim entityId As Int32 = args.Cell.RowItem.ItemValue


                Select Case args.Cell.ColumnItem.ItemValue

                    Case ENTITIES_CURRENCY_VARIABLE
                        Dim currencyId As Int32 = GlobalVariables.Currencies.GetValueId(CStr(args.Cell.Value))
                        If (currencyId = 0) Then
                            MsgBox(Local.GetValue("entities_edition.msg_currency_not_found1") & args.Cell.Value & Local.GetValue("entities_edition.msg_currency_not_found2"))
                            Exit Sub
                        Else
                            m_controller.UpdateEntityCurrency(entityId, currencyId)
                        End If

                    Case Else
                        Dim filterValueName As String = args.Cell.Value
                        Dim filterValueId As Int32 = GlobalVariables.FiltersValues.GetValueId(filterValueName)
                        If filterValueId = 0 Then
                            MsgBox(Local.GetValue("axis.msg_filter_value_not_found"))
                            Exit Sub
                        End If
                        Dim filterId As Int32 = args.Cell.ColumnItem.ItemValue
                        UpdateEntitiesFiltersAfterEdition(entityId, filterId, filterValueId)
                        m_controller.UpdateAxisFilter(entityId, filterId, filterValueId)
                End Select

            End If
        End If

    End Sub

#End Region


#End Region


#Region "Utilities"

    Friend Function getCurrentRowItem() As HierarchyItem

        Return m_currentRowItem

    End Function

#Region "Copy Down"

    Friend Sub CopyValueDown()

        If Not m_entitiesDataGridView.CellsArea.SelectedCells(0) Is Nothing Then
            Dim row As HierarchyItem = m_entitiesDataGridView.CellsArea.SelectedCells(0).RowItem
            Dim column As HierarchyItem = m_entitiesDataGridView.CellsArea.SelectedCells(0).ColumnItem
            Dim value As String = m_entitiesDataGridView.CellsArea.SelectedCells(0).Value
            If row.Items.Count > 0 Then SetValueToChildrenItems(row, column, value) Else SetValueToSibbling(row, column, value)
            m_entitiesDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
            m_entitiesDataGridView.Refresh()
            m_entitiesDataGridView.Select()
        End If

    End Sub

    Private Sub SetValueToChildrenItems(ByRef rowItem_ As HierarchyItem, ByRef columnItem_ As HierarchyItem, ByRef value As String)

        For Each childItem As HierarchyItem In rowItem_.Items
            m_entitiesDataGridView.CellsArea.SetCellValue(childItem, columnItem_, value)
            If childItem.Items.Count > 0 Then SetValueToChildrenItems(childItem, columnItem_, value)
        Next

    End Sub

    Private Sub SetValueToSibbling(ByRef rowItem_ As HierarchyItem, ByRef columnItem_ As HierarchyItem, ByRef value As String)

        On Error GoTo errorHandler
        Dim parent As HierarchyItem = rowItem_.ParentItem
        Dim currentItem = rowItem_
        While Not currentItem.ItemBelow Is Nothing

            currentItem = currentItem.ItemBelow
            If currentItem.ParentItem.GetUniqueID = parent.GetUniqueID Then

                If currentItem.Items.Count > 0 Then
                    SetValueToChildrenItems(currentItem, columnItem_, value)
                Else
                    m_entitiesDataGridView.CellsArea.SetCellValue(currentItem, columnItem_, value)
                End If
            End If

        End While

errorHandler:
        Exit Sub

    End Sub

#End Region

#Region "Excel Drop"

    ' Drop the current TGV in a new worksheet
    Friend Sub DropInExcel()

        Dim cell As Excel.Range = WorksheetWrittingFunctions.CreateReceptionWS("Entities", _
                                                                                {"Entities Hierarchy"}, _
                                                                                {""})
        Dim nbRows As Int32
        For Each item As HierarchyItem In m_entitiesDataGridView.RowsHierarchy.Items
            DGVRowsCount(item, nbRows)
        Next

        Dim nbcols As Int32 = m_entitiesDataGridView.ColumnsHierarchy.Items.Count
        ReDim m_DGVArray(nbRows + 1, nbcols + 2)
        Dim i, j As Int32

        m_DGVArray(i, 0) = "Entity's Name"
        m_DGVArray(i, 1) = "Entity's Affiliate's Name"
        For j = 0 To nbcols - 1
            m_DGVArray(i, j + 2) = m_entitiesDataGridView.ColumnsHierarchy.Items(j).Caption
        Next

        For Each row In m_entitiesDataGridView.RowsHierarchy.Items
            fillInDGVArrayRow(row, 1)
        Next

        WorksheetWrittingFunctions.WriteArray(m_DGVArray, cell)
        ExcelFormatting.FormatEntitiesReport(GlobalVariables.APPS.ActiveSheet.range(cell, cell.Offset(UBound(m_DGVArray, 1), UBound(m_DGVArray, 2))))

    End Sub

    ' Recursively fill in row array
    Private Sub fillInDGVArrayRow(ByRef inputRow As HierarchyItem, ByRef rowIndex As Int32)

        Dim j As Int32

        m_DGVArray(rowIndex, 0) = inputRow.Caption
        If Not inputRow.ParentItem Is Nothing Then
            m_DGVArray(rowIndex, 1) = inputRow.ParentItem.Caption
        Else
            m_DGVArray(rowIndex, 1) = ""
        End If

        For Each column As HierarchyItem In m_entitiesDataGridView.ColumnsHierarchy.Items
            m_DGVArray(rowIndex, j + 2) = m_entitiesDataGridView.CellsArea.GetCellValue(inputRow, column)
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
