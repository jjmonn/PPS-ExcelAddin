' AnalysisAxisDGV.vb
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 20/04/2015


Imports VIBlend.WinForms.DataGridView
Imports VIBlend.Utilities
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections



Friend Class AnalysisAxisDGV


#Region "Instance variables"

    ' Objects
    Protected Friend DGV As New vDataGridView

    ' Variables
    Private categoriesTV As TreeView
    Private columnsIDItemDict As New Dictionary(Of String, HierarchyItem)
    Protected Friend columnsCaptionIDDict As New Dictionary(Of String, String)
    Protected Friend isFillingDGV As Boolean
    Protected Friend current_row As HierarchyItem = Nothing

    ' Constants
    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER
      Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 7


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_categoriesTV As TreeView, _
                             ByRef values_dict As Hashtable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        categoriesTV = input_categoriesTV
        InitializeDGVDisplay()
        columnsInit()
        fillDGV(values_dict)

        AddHandler DGV.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick
        AddHandler DGV.CellMouseClick, AddressOf dataGridView_CellMouseClick

    End Sub

    Private Sub InitializeDGVDisplay()

        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.ColumnsHierarchy.AllowResize = True
        DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
        DGV.RowsHierarchy.Visible = False
        DGV.AllowDragDropIndication = True
        DGV.AllowCopyPaste = True
        DGV.FilterDisplayMode = FilterDisplayMode.Custom
        DGV.VIBlendTheme = DGV_VI_BLEND_STYLE
        DGV.BackColor = Drawing.SystemColors.Control

    End Sub

#End Region


#Region "DGV init and Fill"

    Private Sub columnsInit()

        Dim nameColumn As HierarchyItem = DGV.ColumnsHierarchy.Items.Add("Name")
        Dim nameEditor As New TextBoxEditor
        nameColumn.CellsEditor = nameEditor
        AddHandler nameEditor.KeyDown, AddressOf comboTextBox_KeyDown

        For Each rootNode As TreeNode In categoriesTV.Nodes
            Dim comboBox As New ComboBoxEditor()
            Dim col As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(rootNode.Text)
            col.CellsEditor = comboBox
            columnsIDItemDict.Add(rootNode.Name, col)
            columnsCaptionIDDict.Add(rootNode.Text, rootNode.Name)
            initComboBox(col, comboBox, rootNode)
            AddHandler comboBox.EditBase.TextBox.KeyDown, AddressOf comboTextBox_KeyDown
        Next

    End Sub

    Private Sub initComboBox(ByRef columnHierarchyItem As HierarchyItem, _
                             ByRef comboBox As ComboBoxEditor, _
                             ByRef inputNode As TreeNode)

        ComboBox.DropDownHeight = ComboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
        ComboBox.DropDownWidth = CB_WIDTH

        Dim tmpDictionary As New Dictionary(Of String, String)
        For Each node As TreeNode In inputNode.Nodes
            ComboBox.Items.Add(node.Text)
            tmpDictionary.Add(node.Text, node.Name)
        Next

    End Sub

    Private Sub fillDGV(ByRef values_dict As Hashtable)

        isFillingDGV = True
        For Each value_id In values_dict.Keys
            addRow(value_id, values_dict(value_id))
        Next
        isFillingDGV = False
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, My.Settings.dgvFontSize, My.Settings.dgvFontSize)
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.Refresh()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub addRow(ByRef item_id As String, _
                                ByVal itemHT As Hashtable)

        Dim rowItem = DGV.RowsHierarchy.Items.Add(item_id)
        rowItem.TextAlignment = Drawing.ContentAlignment.MiddleLeft
        Dim column As HierarchyItem = DGV.ColumnsHierarchy.Items(0)
        DGV.CellsArea.SetCellValue(rowItem, column, itemHT(NAME_VARIABLE))

        For Each root_category_node As TreeNode In categoriesTV.Nodes
            column = columnsIDItemDict(root_category_node.Name)
            Dim category_value As String = categoriesTV.Nodes.Find((itemHT(root_category_node.Name)), True)(0).Text
            DGV.CellsArea.SetCellValue(rowItem, column, category_value)
        Next

    End Sub

#End Region


#Region "DGV Events"

    Private Sub dataGridView_HierarchyItemMouseClick(sender As Object, args As HierarchyItemMouseEventArgs)

        current_row = args.HierarchyItem

    End Sub

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        current_row = args.Cell.RowItem

    End Sub

    Private Sub comboTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter : DGV.CloseEditor(True)
            Case Keys.Escape : DGV.CloseEditor(False)
        End Select

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Sub copyValueDown()

        If Not DGV.CellsArea.SelectedCells(0) Is Nothing Then
            Dim row As HierarchyItem = DGV.CellsArea.SelectedCells(0).RowItem
            Dim column As HierarchyItem = DGV.CellsArea.SelectedCells(0).ColumnItem
            Dim value As String = DGV.CellsArea.SelectedCells(0).Value

            row = row.ItemBelow
            While Not row Is Nothing
                DGV.CellsArea.SetCellValue(row, column, value)
                row = row.ItemBelow
            End While
            DGV.Refresh()
        End If

    End Sub

    Protected Friend Sub dropToExcel()

        ' to be implemented -> use DGV copytoexcel generic method


    End Sub


#End Region


End Class
