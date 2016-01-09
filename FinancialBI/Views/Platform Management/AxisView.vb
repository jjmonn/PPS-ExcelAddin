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

Friend Class AxisView

#Region "Instance Variables"

    ' Objects
    Protected m_controller As AxisController
    Protected m_axisFilterTreeview As vTreeView
    Protected m_axisFilterValuesTreeview As vTreeView
    Friend m_axisDataGridView As New vDataGridView
    Private m_currenciesComboBox As New ComboBoxEditor()
    Private m_rightMgr As New RightManager
    Friend m_rowsIdsItemDic As New SafeDictionary(Of Int32, HierarchyItem)

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

    Protected Sub New()
        InitializeComponent()
    End Sub

    Friend Sub New(ByRef p_controller As AxisController, _
                   ByRef p_axisFilterValuesTV As vTreeView, _
                   ByRef p_axisFiltersTV As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        m_axisFilterTreeview = p_axisFiltersTV
        m_axisFilterValuesTreeview = p_axisFilterValuesTV

        LoadInstanceVariables()
        initFilters()
        LoadControls()
        If m_controller.GetAxisType() = AxisType.Entities Then InitCurrenciesComboBox()
        InitializeDGVDisplay()
        DGVColumnsInitialize()
        DGVRowsInitialize()
        FillDGV()
        m_axisDataGridView.RowsHierarchy.ExpandAllItems()

        AddHandler m_axisDataGridView.CellMouseClick, AddressOf dataGridView_CellMouseClick
        AddHandler m_axisDataGridView.MouseDown, AddressOf DataGridViewRightClick
        AddHandler m_axisDataGridView.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick
        AddHandler m_axisDataGridView.CellValueChanged, AddressOf dataGridView_CellValueChanged
        AddHandler m_axisDataGridView.KeyDown, AddressOf DGV_KeyDown
        DefineUIPermissions()
        DesactivateUnallowed()
        MultilanguageSetup()

    End Sub

    Protected Overridable Sub LoadControls()

        Me.TableLayoutPanel1.Controls.Add(m_axisDataGridView, 0, 1)
        m_axisDataGridView.Dock = DockStyle.Fill

    End Sub

    Private Sub DefineUIPermissions()
        m_rightMgr(RenameAxisElemButton) = Group.Permission.EDIT_AXIS
        m_rightMgr(CreateAxisElemToolStripMenuItem) = Group.Permission.CREATE_AXIS
        m_rightMgr(DeleteAxisElemToolStripMenuItem) = Group.Permission.DELETE_AXIS
        m_rightMgr(DeleteAxisElemToolStripMenuItem2) = Group.Permission.DELETE_AXIS
        m_rightMgr(CreateANewAxisElemToolStripMenuItem) = Group.Permission.CREATE_AXIS
        m_rightMgr(copy_down_bt) = Group.Permission.EDIT_AXIS
    End Sub

    Private Sub DesactivateUnallowed()
        m_rightMgr.Enable(GlobalVariables.Users.GetCurrentUserRights())
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

        Me.RenameAxisElemButton.Text = Local.GetValue("general.rename")
        Me.CreateAxisElemToolStripMenuItem.Text = Local.GetValue("general.create")
        Me.DeleteAxisElemToolStripMenuItem2.Text = Local.GetValue("general.delete")
        Me.copy_down_bt.Text = Local.GetValue("general.copy_down")
        Me.drop_to_excel_bt.Text = Local.GetValue("general.drop_on_excel")
        Me.AutoResizeColumnsButton.Text = Local.GetValue("general.auto_resize_columns")
        Me.ExpandAllBT.Text = Local.GetValue("general.expand_all")
        Me.CollapseAllBT.Text = Local.GetValue("general.collapse_all")
        Me.CreateANewAxisElemToolStripMenuItem.Text = Local.GetValue("general.create")
        Me.DeleteAxisElemToolStripMenuItem.Text = Local.GetValue("general.delete")
        Me.SendEntitiesHierarchyToExcelToolStripMenuItem.Text = Local.GetValue("general.drop_on_excel")
        Me.m_axisEditionButton.Text = Local.GetValue("general.edition")

    End Sub

#End Region

#Region "Rows Initialization"

    Protected Sub DGVRowsInitialize()

        m_isFillingDGV = True
        m_axisDataGridView.RowsHierarchy.Clear()
        m_rowsIdsItemDic.Clear()
        m_isFillingDGV = False

    End Sub

#End Region

#Region "Interface"

    Delegate Sub LoadInstanceVariables_Delegate()
    Friend Overridable Sub LoadInstanceVariables()
        If m_axisDataGridView.InvokeRequired Then
            Dim MyDelegate As New LoadInstanceVariables_Delegate(AddressOf LoadInstanceVariables)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            m_controller.LoadInstanceVariables()
        End If
    End Sub

    Delegate Sub UpdateAxisElem_Delegate(ByRef p_entity As AxisElem)
    Friend Sub UpdateAxisElem(ByRef p_entity As AxisElem)

        If m_axisDataGridView.InvokeRequired Then
            Dim MyDelegate As New UpdateAxisElem_Delegate(AddressOf UpdateAxisElem)
            Me.Invoke(MyDelegate, New Object() {p_entity})
        Else
            m_isFillingDGV = True
            FillRow(p_entity.Id, p_entity)
            UpdateDGVFormat()
            m_isFillingDGV = False
        End If

    End Sub

    Delegate Sub DeleteAxisElem_Delegate(ByRef p_id As Int32)
    Friend Sub DeleteAxisElem(ByRef p_id As Int32)

        If m_axisDataGridView.InvokeRequired Then
            Dim MyDelegate As New DeleteAxisElem_Delegate(AddressOf DeleteAxisElem)
            Me.Invoke(MyDelegate, New Object() {p_id})
        Else
            Dim row As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.RowsHierarchy, p_id)
            If row IsNot Nothing Then row.Delete()
            m_axisDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
            m_axisDataGridView.Refresh()
        End If

    End Sub

#End Region

#Region "Calls Back"

#Region "DGV Right Click Menu"

    Private Sub DataGridViewRightClick(sender As Object, e As MouseEventArgs)

        If (e.Button <> MouseButtons.Right) Then Exit Sub
        Dim target As HierarchyItem = m_axisDataGridView.RowsHierarchy.HitTest(e.Location)
        If target IsNot Nothing Then
            m_currentRowItem = DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.RowsHierarchy, target.ItemValue)
        Else
            Dim target2 As GridCell = m_axisDataGridView.CellsArea.HitTest(e.Location)
            If target2 Is Nothing Then Exit Sub
            m_currentRowItem = target2.RowItem
        End If
        m_axisRightClickMenu.Visible = True
        m_axisRightClickMenu.Bounds = New Rectangle(MousePosition, New Size(m_axisRightClickMenu.Width, m_axisRightClickMenu.Height))

    End Sub

    Private Sub RenameAxisElemButton_Click(sender As Object, e As EventArgs) Handles RenameAxisElemButton.Click

        If m_currentRowItem Is Nothing Then
            MsgBox(Local.GetValue("axis_edition.msg_selection" & m_controller.GetAxisType))
            Exit Sub
        End If
        Dim newAxisElemName As String = InputBox(Local.GetValue("axis_edition.msg_name" & m_controller.GetAxisType), , m_currentRowItem.Caption)
        If newAxisElemName <> "" Then
            m_controller.UpdateAxisElemName(m_currentRowItem.ItemValue, newAxisElemName)
        End If

    End Sub

    Private Sub cop_down_bt_Click(sender As Object, e As EventArgs) Handles copy_down_bt.Click

        CopyValueDown()
        m_axisDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        m_axisDataGridView.Refresh()

    End Sub

    Private Sub drop_to_excel_bt_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        DropInExcel()

    End Sub

    Private Sub CreateAxisElemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateAxisElemToolStripMenuItem.Click
        AddAxisElem_cmd_Click(sender, e)
    End Sub

    Private Sub CreateANewAxisElemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateANewAxisElemToolStripMenuItem.Click
        AddAxisElem_cmd_Click(sender, e)
    End Sub

    Private Sub DeleteAxisElemToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteAxisElemToolStripMenuItem2.Click

        DeleteAxisElem_cmd_Click(sender, e)

    End Sub

    Private Sub AutoResizeColumnsButton_Click(sender As Object, e As EventArgs) Handles AutoResizeColumnsButton.Click
        m_axisDataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
    End Sub

#End Region

#Region "Main menu"

    Private Sub AddAxisElem_cmd_Click(sender As Object, e As EventArgs) Handles CreateAxisElemToolStripMenuItem.Click

        Me.Hide()
        m_controller.ShowNewAxisElemUI()

    End Sub

    Private Sub DeleteAxisElem_cmd_Click(sender As Object, e As EventArgs) Handles DeleteAxisElemToolStripMenuItem.Click

        Dim l_axisType = m_controller.GetAxisType
        If Not m_currentRowItem Is Nothing Then
            Dim confirm As Integer = MessageBox.Show(Local.GetValue("axis_edition.msg_delete1" & l_axisType) + Chr(13) + Chr(13) + _
                                                    m_currentRowItem.Caption + Chr(13) + Chr(13) + _
                                                    Local.GetValue("axis_edition.msg_delete2" & l_axisType) + Chr(13) + Chr(13), _
                                                    Local.GetValue("axis_edition.title_delete_confirmation" & l_axisType), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Dim entity_id As String = m_currentRowItem.ItemValue
                m_controller.DeleteAxisElem(entity_id)
            End If
        Else
            MsgBox(Local.GetValue("axis_edition.msg_selection" & l_axisType))
        End If
        m_currentRowItem = Nothing

    End Sub

    Private Sub dropInExcel_cmd_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click, SendEntitiesHierarchyToExcelToolStripMenuItem.Click

        DropInExcel()

    End Sub

    Private Sub ExpandAllBT_Click(sender As Object, e As EventArgs) Handles ExpandAllBT.Click
        m_axisDataGridView.RowsHierarchy.ExpandAllItems()
    End Sub

    Private Sub CollapseAllBT_Click(sender As Object, e As EventArgs) Handles CollapseAllBT.Click
        m_axisDataGridView.RowsHierarchy.CollapseAllItems()
    End Sub

#End Region

#End Region

#Region "DGV"

    Private Sub InitializeDGVDisplay()

        m_axisDataGridView.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        m_axisDataGridView.ColumnsHierarchy.AutoStretchColumns = True
        m_axisDataGridView.ColumnsHierarchy.AllowResize = True
        ' DGV.RowsHierarchy.AllowDragDrop = True
        m_axisDataGridView.RowsHierarchy.CompactStyleRenderingEnabled = True
        m_axisDataGridView.AllowDragDropIndication = True
        m_axisDataGridView.AllowCopyPaste = True
        m_axisDataGridView.FilterDisplayMode = FilterDisplayMode.Custom
        m_axisDataGridView.VIBlendTheme = DGV_VI_BLEND_STYLE
        m_axisDataGridView.BackColor = SystemColors.Control
        'DGV.ImageList = AxisIL
        m_axisDataGridView.RowsHierarchy.AllowDragDrop = True
        m_axisDataGridView.AllowDragDropIndication = True

    End Sub

    Protected Overridable Sub FillDGV()

        FillDGV(m_controller.GetAxisDictionary())

    End Sub

    Protected Sub fillDGV(ByRef axisHT As MultiIndexDictionary(Of UInt32, String, AxisElem))

        m_isFillingDGV = True
        For Each axisValue In axisHT.SortedValues
            FillRow(axisValue.Id, axisValue)
        Next
        m_isFillingDGV = False
        updateDGVFormat()

    End Sub

    Protected Overridable Sub CreateAxisOrder()

        Dim axisName As String = InputBox(Local.GetValue("axis.msg_enter_name"), Local.GetValue("axis.msg_axis_creation"))
        If axisName <> "" Then
            m_controller.CreateAxisElem(axisName)
        End If

    End Sub

#Region "Columns Initialization"

    Protected Sub DGVColumnsInitialize()

        m_axisDataGridView.ColumnsHierarchy.Clear()

        If m_controller.GetAxisType() = AxisType.Entities Then
            ' Entities Currency Column
            Dim currencyColumn As HierarchyItem = m_axisDataGridView.ColumnsHierarchy.Items.Add(CURRENCY_COLUMN_NAME)
            currencyColumn.ItemValue = ENTITIES_CURRENCY_VARIABLE
            currencyColumn.AllowFiltering = True
            currencyColumn.Width = COLUMNS_WIDTH
        End If

        For Each rootNode As vTreeNode In m_axisFilterTreeview.Nodes
            CreateSubFilters(rootNode)
            '   CreateFilter(col)
        Next

    End Sub

    Private Sub CreateSubFilters(ByRef node As vTreeNode)

        Dim col As HierarchyItem = m_axisDataGridView.ColumnsHierarchy.Items.Add(node.Text)
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

    Friend Sub FillRow(ByVal p_axisElemId As Int32, _
                       ByVal p_axisElem As AxisElem)

        Dim rowItem As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.RowsHierarchy, p_axisElemId)
        If rowItem Is Nothing Then
            Dim parentAxisElemId As Int32 = p_axisElem.ParentId
            If parentAxisElemId = 0 Then
                rowItem = DataGridViewsUtil.CreateRow(m_axisDataGridView, p_axisElemId, p_axisElem.Name)
            Else
                Dim parentRow As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.RowsHierarchy, parentAxisElemId)
                If (parentRow Is Nothing) Then Exit Sub
                rowItem = DataGridViewsUtil.CreateRow(m_axisDataGridView, p_axisElemId, p_axisElem.Name, parentRow)
            End If
        End If
        rowItem.Caption = p_axisElem.Name
        Dim column As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.ColumnsHierarchy, ENTITIES_CURRENCY_VARIABLE)
        If p_axisElem.AllowEdition = True Then

            If m_controller.GetAxisType() = AxisType.Entities Then
                If GlobalVariables.Users.CurrentUserHasRight(Group.Permission.EDIT_AXIS) Then
                    m_axisDataGridView.CellsArea.SetCellEditor(rowItem, column, m_currenciesComboBox)
                End If
                Dim entityCurrency As EntityCurrency = GlobalVariables.EntityCurrencies.GetValue(p_axisElemId)
                If entityCurrency Is Nothing Then Exit Sub
                Dim currency As Currency = GlobalVariables.Currencies.GetValue(CInt(entityCurrency.CurrencyId))
                If currency Is Nothing Then Exit Sub

                m_axisDataGridView.CellsArea.SetCellValue(rowItem, column, currency.Name)
            End If

            For Each filterNode As vTreeNode In m_axisFilterTreeview.Nodes
                FillSubFilters(filterNode, p_axisElemId, rowItem)
            Next
        End If
        If p_axisElem.AllowEdition = False Then rowItem.ImageIndex = 0 Else rowItem.ImageIndex = 1

    End Sub

    Private Sub FillSubFilters(ByRef p_filterNode As vTreeNode, _
                               ByRef p_axisElemId As Int32, _
                               ByRef p_rowItem As HierarchyItem)

        Dim combobox As New ComboBoxEditor
        combobox.DropDownList = True
        Dim columnItem As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.ColumnsHierarchy, p_filterNode.Value)
        Dim filterValueId As UInt32 = GlobalVariables.AxisFilters.GetFilterValueId(m_controller.GetAxisType(), CInt(p_filterNode.Value), p_axisElemId)
        Dim filter_value_name As String = ""
        If filterValueId <> 0 Then
            filter_value_name = GlobalVariables.FiltersValues.GetValueName(filterValueId)
        End If

        m_axisDataGridView.CellsArea.SetCellValue(p_rowItem, columnItem, filter_value_name)

        ' Filters Choices Setup
        If p_filterNode.Parent Is Nothing Then
            ' Root Filter
            If Not GlobalVariables.FiltersValues.GetDictionary(p_filterNode.Value) Is Nothing Then
                For Each value As FilterValue In GlobalVariables.FiltersValues.GetDictionary(p_filterNode.Value).Values
                    combobox.Items.Add(value.Name)
                Next
            End If
        Else
            ' Child Filter
            Dim parentFilterFilterValueId As Int32 = GlobalVariables.AxisFilters.GetFilterValueId(m_controller.GetAxisType(), p_filterNode.Parent.Value, p_axisElemId)     ' Child Filter Id
            Dim filterValuesIds = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds({parentFilterFilterValueId})
            For Each Id As UInt32 In filterValuesIds
                combobox.Items.Add(GlobalVariables.FiltersValues.GetValueName(Id))
            Next
        End If

        ' Add ComboBoxEditor to Cell
        If GlobalVariables.Users.CurrentUserHasRight(Group.Permission.EDIT_AXIS) Then m_axisDataGridView.CellsArea.SetCellEditor(p_rowItem, columnItem, combobox)

        ' Recursive if Filters Children exist
        If p_filterNode.Nodes Is Nothing Then Exit Sub
        For Each childFilterNode As vTreeNode In p_filterNode.Nodes
            FillSubFilters(childFilterNode, p_axisElemId, p_rowItem)
        Next

    End Sub

    Private Sub UpdateDGVFormat()

        DataGridViewsUtil.DGVSetHiearchyFontSize(m_axisDataGridView, My.Settings.dgvFontSize, My.Settings.dgvFontSize)
        DataGridViewsUtil.FormatDGVRowsHierarchy(m_axisDataGridView)
        m_axisDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        m_axisDataGridView.Refresh()

    End Sub

#End Region


#Region "DGV Updates"

    ' Update Parents and Children Filters Cells or comboboxes after a filter cell has been edited
    Friend Sub UpdateEntitiesFiltersAfterEdition(ByRef axisElemId As Int32, _
                                                 ByRef filterId As Int32, _
                                                 ByRef filterValueId As Int32)

        m_isFillingDGV = True
        Dim filterNode As vTreeNode = VTreeViewUtil.FindNode(m_axisFilterTreeview, filterId)

        If filterNode Is Nothing Then Exit Sub
        ' Update parent filters recursively
        UpdateParentFiltersValues(DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.RowsHierarchy, axisElemId), _
                                 axisElemId, _
                                 filterNode, _
                                 filterValueId)

        ' Update children filters comboboxes recursively
        For Each childFilterNode As vTreeNode In filterNode.Nodes
            UpdateChildrenFiltersComboBoxes(DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.RowsHierarchy, axisElemId), _
                                            childFilterNode, _
                                            {filterValueId})
        Next
        m_axisDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        m_axisDataGridView.Refresh()
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
            Dim column As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.ColumnsHierarchy, parentFilterNode.Value)
            Dim parentFilterValue As FilterValue = GlobalVariables.FiltersValues.GetValue(filterValue.ParentId)
            If parentFilterValue Is Nothing Then Exit Sub

            m_axisDataGridView.CellsArea.SetCellValue(row, column, parentFilterValue.Name)

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

        Dim column As HierarchyItem = DataGridViewsUtil.GetHierarchyItemFromId(m_axisDataGridView.ColumnsHierarchy, filterNode.Value)
        Dim comboBox As New ComboBoxEditor

        Dim filterValuesIds As Int32() = GlobalVariables.FiltersValues.GetFilterValueIdsFromParentFilterValueIds(parentFilterValueIds)
        For Each Id As UInt32 In filterValuesIds
            comboBox.Items.Add(GlobalVariables.FiltersValues.GetValueName(Id))
        Next

        ' Set Cell value to nothing
        m_axisDataGridView.CellsArea.SetCellValue(row, column, Nothing)

        ' Add ComboBoxEditor to Cell
        If GlobalVariables.Users.CurrentUserHasRight(Group.Permission.EDIT_AXIS) Then m_axisDataGridView.CellsArea.SetCellEditor(row, column, comboBox)

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
                m_axisDataGridView.CloseEditor(True)
            Else
                m_axisDataGridView.CloseEditor(False)
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
            Case Keys.Delete : DeleteAxisElem_cmd_Click(sender, e)
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
                            MsgBox(Local.GetValue("axis_edition.msg_currency_not_found1") & args.Cell.Value & Local.GetValue("axis_edition.msg_currency_not_found2"))
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

        If Not m_axisDataGridView.CellsArea.SelectedCells(0) Is Nothing Then
            Dim row As HierarchyItem = m_axisDataGridView.CellsArea.SelectedCells(0).RowItem
            Dim column As HierarchyItem = m_axisDataGridView.CellsArea.SelectedCells(0).ColumnItem
            Dim value As String = m_axisDataGridView.CellsArea.SelectedCells(0).Value
            If row.Items.Count > 0 Then SetValueToChildrenItems(row, column, value) Else SetValueToSibbling(row, column, value)
            m_axisDataGridView.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
            m_axisDataGridView.Refresh()
            m_axisDataGridView.Select()
        End If

    End Sub

    Private Sub SetValueToChildrenItems(ByRef rowItem_ As HierarchyItem, ByRef columnItem_ As HierarchyItem, ByRef value As String)

        For Each childItem As HierarchyItem In rowItem_.Items
            m_axisDataGridView.CellsArea.SetCellValue(childItem, columnItem_, value)
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
                    m_axisDataGridView.CellsArea.SetCellValue(currentItem, columnItem_, value)
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
        For Each item As HierarchyItem In m_axisDataGridView.RowsHierarchy.Items
            DGVRowsCount(item, nbRows)
        Next

        Dim nbcols As Int32 = m_axisDataGridView.ColumnsHierarchy.Items.Count
        ReDim m_DGVArray(nbRows + 1, nbcols + 2)
        Dim i, j As Int32

        m_DGVArray(i, 0) = "AxisElem's Name"
        m_DGVArray(i, 1) = "AxisElem's Affiliate's Name"
        For j = 0 To nbcols - 1
            m_DGVArray(i, j + 2) = m_axisDataGridView.ColumnsHierarchy.Items(j).Caption
        Next

        For Each row In m_axisDataGridView.RowsHierarchy.Items
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

        For Each column As HierarchyItem In m_axisDataGridView.ColumnsHierarchy.Items
            m_DGVArray(rowIndex, j + 2) = m_axisDataGridView.CellsArea.GetCellValue(inputRow, column)
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