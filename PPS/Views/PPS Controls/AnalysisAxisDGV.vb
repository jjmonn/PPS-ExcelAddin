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
    Friend isFillingDGV As Boolean

    ' Constants
    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.OFFICE2010SILVER
    Protected Friend DGV_FONT_SIZE As Single = 8
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 7


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_categoriesTV As TreeView, _
                             ByRef values_dict As Dictionary(Of String, Hashtable))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        categoriesTV = input_categoriesTV
        InitializeDGVDisplay()
        columnsInit()
        fillDGV(values_dict)

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


#Region "Interface"

    Private Sub columnsInit()

        DGV.ColumnsHierarchy.Items.Add("Name")

        For Each rootNode As TreeNode In categoriesTV.Nodes
            Dim col As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(rootNode.Text)
            columnsIDItemDict.Add(rootNode.Name, col)
            columnsCaptionIDDict.Add(rootNode.Text, rootNode.Name)
            initComboBox(col, rootNode)
        Next

    End Sub

    Private Sub initComboBox(ByRef columnHierarchyItem As HierarchyItem, _
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

    Private Sub fillDGV(ByRef values_dict As Dictionary(Of String, Hashtable))

        isFillingDGV = True
        Dim column As HierarchyItem
        Dim category_value As String
        For Each value_id In values_dict.Keys
            Dim rowItem = DGV.RowsHierarchy.Items.Add(value_id)
            rowItem.TextAlignment = Drawing.ContentAlignment.MiddleLeft
            column = DGV.ColumnsHierarchy.Items(0)
            DGV.CellsArea.SetCellValue(rowItem, column, values_dict(value_id)(ANALYSIS_AXIS_NAME_VAR))

            For Each root_category_node As TreeNode In categoriesTV.Nodes
                column = columnsIDItemDict(root_category_node.Name)
                category_value = categoriesTV.Nodes.Find((values_dict(value_id)(root_category_node.Name)), True)(0).Text
                DGV.CellsArea.SetCellValue(rowItem, column, category_value)
            Next

        Next
        isFillingDGV = False
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, DGV_FONT_SIZE, DGV_FONT_SIZE)
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
