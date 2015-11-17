' Scenario.vb
'
' 
' 
' To do:
'      - 
'      - 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 27/05/2015


Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.Utilities
Imports VIBlend.WinForms.Controls


Friend Class Scenario


#Region "Instance Variable"

    ' objects
    Friend constraintsDGV As New vDataGridView
    Friend generalDGV As New vDataGridView
    Friend Outputchart As New Chart
    Private fAccountsNodes As TreeNode
    Private constraints_text_box_editor As New TextBoxEditor()

    ' Variables
    Friend data_dic As Dictionary(Of String, Double())
    Private periods_array As Int32()
    Private charts_periods As New List(Of Int32)
    Friend generalDGV_rows_id_item_dict As New Dictionary(Of String, HierarchyItem)
    Friend constraints_DGV_rows_id_item_dict As New Dictionary(Of String, HierarchyItem)
    Private current_constraint_cell As GridCell = Nothing
    Private is_updating_value As Boolean
    Private manual_edition As Boolean

    ' Display
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
    Friend Const PERCENT_FORMAT As String = "{0:P0}"


#End Region


#Region "Initialize"

    Friend Sub New(ByRef periods_list As Int32(), _
                   ByRef fAccountsNodes As TreeNode, _
                   ByRef constraints_id_list As List(Of String))

        Me.periods_array = periods_list
        Me.fAccountsNodes = fAccountsNodes
        buildConstraintsDGV(constraints_id_list)
        BuildGeneralDGV()
        BuildOutputsChart(Outputchart)

        AddHandler generalDGV.CellBeginEdit, AddressOf ConstraintDGV_CellBeginEdit

    End Sub

    Friend Sub BuildOutputsChart(ByRef chart As Chart)

        Dim ChartArea1 As New ChartArea
        Dim ChartArea2 As New ChartArea
        chart.ChartAreas.Add(ChartArea1)
        chart.ChartAreas.Add(ChartArea2)

        ChartArea1.AxisY.LabelAutoFitMaxFontSize = 8
        ChartArea2.AxisY.LabelAutoFitMaxFontSize = 8
        ChartArea1.AxisX.LabelAutoFitMaxFontSize = 8
        ChartArea2.AxisX.LabelAutoFitMaxFontSize = 8

        chart.Palette = ChartColorPalette.Berry
        ChartArea1.AxisX.MajorGrid.Enabled = False
        ChartArea2.AxisX.MajorGrid.Enabled = False

        Dim legend1 As New Legend
        chart.Legends.Add(legend1)
        chart.Legends(0).Docking = Docking.Bottom

        ChartArea1.Position.X = 0
        ChartArea1.Position.Y = 0
        ChartArea1.Position.Width = 50
        ChartArea1.Position.Height = 80

        ChartArea2.Position.X = 50
        ChartArea2.Position.Y = 0
        ChartArea2.Position.Width = 50
        ChartArea2.Position.Height = 80

        charts_periods.Clear()
        For Each period In periods_array
            charts_periods.Add(Format(DateTime.FromOADate(period), "yyyy"))
        Next

    End Sub

#End Region


#Region "General DGV"

    Private Sub BuildGeneralDGV()

        ' Init Columns
        generalDGV.ColumnsHierarchy.Items.Add("")
        Dim col As HierarchyItem = generalDGV.ColumnsHierarchy.Items.Add("Chart")
        col.CellsTextAlignment = Drawing.ContentAlignment.MiddleCenter
        For Each period In periods_array
            Dim column = generalDGV.ColumnsHierarchy.Items.Add(Format(DateTime.FromOADate(period), "yyyy"))
            '  column.CellsEditor = constraints_text_box_editor
            column.TextAlignment = Drawing.ContentAlignment.MiddleCenter
            column.CellsTextAlignment = DataGridViewContentAlignment.MiddleRight
        Next

        ' Init Rows
        For Each node In fAccountsNodes.Nodes
            AddGeneralRow(node)
        Next
        formatDGV(generalDGV)

    End Sub

    Private Sub AddGeneralRow(ByRef f_accounts_node As TreeNode, _
                              Optional ByRef parent_item As HierarchyItem = Nothing)

        ' Row Creation
        Dim row As HierarchyItem
        If parent_item Is Nothing Then
            row = generalDGV.RowsHierarchy.Items.Add(f_accounts_node.Name)
        Else
            row = parent_item.Items.Add(f_accounts_node.Name)
        End If

        ' Fill Accounts name Cell
        generalDGV.CellsArea.SetCellValue(row, generalDGV.ColumnsHierarchy.Items(0), f_accounts_node.Text)
        ' Format Row
        '       row.CellsFormatString = FModellingAccounts.ReadFModellingAccount(f_accounts_node.Name, FINANCIAL_MODELLING_FORMAT_VARIABLE)
        ' Register Row in General Rows Dictionary (id -> Item)
        generalDGV_rows_id_item_dict(f_accounts_node.Name) = row

        ' Add Check box Editor (For disply serie on chart option)
        Dim checkBoxEditor As New CheckBoxEditor
        generalDGV.CellsArea.SetCellEditor(row, generalDGV.ColumnsHierarchy.Items(1), checkBoxEditor)
        AddHandler checkBoxEditor.CheckedChanged, AddressOf GeneralDGV_Checkedchanged

        ' Fill Sub Rows if any
        For Each sub_node In f_accounts_node.Nodes
            AddGeneralRow(sub_node, row)
        Next

    End Sub

#End Region


#Region "Constraints DGV"

    Private Sub buildConstraintsDGV(ByRef constraints_id_list As List(Of String))

        constraintsDGV.RowsHierarchy.Visible = False
        constraintsDGV.AllowCopyPaste = True
        constraints_text_box_editor.ActivationFlags = EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL

        ' Init Columns
        constraintsDGV.ColumnsHierarchy.Items.Add("")
        For Each period In periods_array
            Dim column = constraintsDGV.ColumnsHierarchy.Items.Add(Format(DateTime.FromOADate(period), "yyyy"))
            column.CellsEditor = constraints_text_box_editor
            column.TextAlignment = Drawing.ContentAlignment.MiddleCenter
            column.CellsTextAlignment = DataGridViewContentAlignment.MiddleRight
        Next

        ' Init Rows
        For Each constraint_id As String In constraints_id_list
            addConstraintRow(constraint_id)
        Next
        formatDGV(constraintsDGV)
        AddHandler constraintsDGV.CellValueChanging, AddressOf constraintsDGV_CellValueChanging
        AddHandler constraintsDGV.CellBeginEdit, AddressOf ConstraintDGV_CellBeginEdit
        AddHandler constraintsDGV.CellMouseClick, AddressOf constraintsDGV_CellMouseClick

    End Sub

    Friend Sub addConstraintRow(ByRef f_account_id As String, _
                                Optional ByRef default_value As Double = 0)

        ' Add constraint row
        Dim row As HierarchyItem = constraintsDGV.RowsHierarchy.Items.Add(f_account_id)
        ' Fill 1st column = Constraint name
        Dim f_account_name As String = fAccountsNodes.Nodes.Find(f_account_id, True)(0).Text
        constraintsDGV.CellsArea.SetCellValue(row, constraintsDGV.ColumnsHierarchy.Items(0), f_account_name)
        ' Format row
        '     row.CellsFormatString = FModellingAccounts.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_FORMAT_VARIABLE)
        ' Register Row in Rows Dictionary
        constraints_DGV_rows_id_item_dict(f_account_id) = row
        ' Fill with default value
        For j = 1 To constraintsDGV.ColumnsHierarchy.Items.Count - 1
            constraintsDGV.CellsArea.SetCellValue(row, constraintsDGV.ColumnsHierarchy.Items(j), default_value)
        Next

        Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(constraintsDGV.VIBlendTheme).GridCellStyle
        CStyle.TextColor = System.Drawing.Color.CadetBlue
        row.CellsStyle = CStyle

        constraintsDGV.Refresh()

    End Sub

#End Region


#Region "Interface"

    Friend Sub fillGeneralDGV(ByRef input_data_dic As Dictionary(Of String, Double()), _
                              ByRef fAccountsIdsList As List(Of Int32))

        data_dic = input_data_dic
        For Each f_account_id As String In fAccountsIdsList
            Dim row As HierarchyItem = generalDGV_rows_id_item_dict(f_account_id)
            For j As Int32 = 2 To generalDGV.ColumnsHierarchy.Items.Count - 1
                Dim value = data_dic(f_account_id)(j - 2)
                If row.CellsFormatString = PRCT_FORMAT_STRING Then value = value / 100
                generalDGV.CellsArea.SetCellValue(row, generalDGV.ColumnsHierarchy.Items(j), value)
            Next
        Next
        generalDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        generalDGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        generalDGV.Refresh()
        generalDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        BindChartSeries()

    End Sub

    Friend Function DeleteDGVActiveConstraint() As String

        If Not current_constraint_cell Is Nothing Then
            Dim constraint_id = current_constraint_cell.RowItem.Caption
            If current_constraint_cell.RowItem.ItemIndex <= 2 Then Return ""
            constraints_DGV_rows_id_item_dict(constraint_id).Delete()
            constraintsDGV.Refresh()
            constraintsDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
            constraints_DGV_rows_id_item_dict(constraint_id).Delete()
            Return constraint_id
        End If
        Return "na"

    End Function

    Friend Sub AddSerieToChart(ByRef f_account_id As String)

        'Dim serie_ht As Hashtable = FModellingAccounts.GetSeriHT(f_account_id)
        'serie_ht.Add(REPORTS_SERIE_WIDTH_VAR, 25)
        'ChartsUtilities.AddSerieToChart(Outputchart, serie_ht, serie_ht(FINANCIAL_MODELLING_SERIE_CHART_VARIABLE))
        'Outputchart.Series(FModellingAccounts.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_NAME_VARIABLE)).Points.DataBindXY(charts_periods, data_dic(f_account_id))

    End Sub

    Friend Sub CopyValueRight()

        DataGridViewsUtil.CopyValueRight(constraintsDGV, current_constraint_cell)
        generalDGV.Refresh()

    End Sub

#End Region


#Region "Events"

    Private Sub ConstraintDGV_CellBeginEdit(sender As Object, e As EventArgs)

        manual_edition = True

    End Sub

    Private Sub constraintsDGV_CellValueChanging(sender As Object, args As CellValueChangingEventArgs)

        If manual_edition = True AndAlso is_updating_value = False Then
            Select Case args.Cell.ColumnItem.ItemIndex
                Case Is > 1
                    If args.NewValue = Nothing Then Exit Sub
                    If Not IsNumeric(args.NewValue) Then
                        args.Cancel = True
                    Else
                        Dim f_account_id As String = args.Cell.RowItem.Caption
                        'If FModellingAccounts.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_FORMAT_VARIABLE) = PERCENT_FORMAT Then
                        '    is_updating_value = True
                        '    args.Cell.Value = args.NewValue / 100
                        '    args.Cell.RowItem.CellsFormatString = PERCENT_FORMAT
                        '    args.Cancel = True
                        '    is_updating_value = False
                        'End If
                    End If
            End Select
            manual_edition = False
        End If
      

    End Sub

    Private Sub constraintsDGV_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        current_constraint_cell = args.Cell

    End Sub

    Private Sub GeneralDGV_Checkedchanged(sender As Object, e As EventArgs)

        BindChartSeries()

    End Sub

#End Region


#Region "Utilities"

    Private Sub formatDGV(ByRef DGV As vDataGridView)

        DGV.BackColor = Drawing.Color.White
        DataGridViewsUtil.InitDisplayVDataGridView(DGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        DGV.ColumnsHierarchy.AutoStretchColumns = True
        DGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        DGV.Refresh()

    End Sub

    Private Sub BindChartSeries()

        Outputchart.Series.Clear()
        For Each row As HierarchyItem In generalDGV.RowsHierarchy.Items
            For Each sub_row As HierarchyItem In row.Items
                Dim cb As CheckBoxEditor = generalDGV.CellsArea.GetCellEditor(sub_row, generalDGV.ColumnsHierarchy.Items(1))
                Dim checkBox As vCheckBox = TryCast(cb.Control, vCheckBox)
                If checkBox.Checked = True Then
                    AddSerieToChart(sub_row.Caption)
                End If
            Next
        Next

    End Sub

    Friend Sub Delete()

        Me.Finalize()

    End Sub

#End Region



End Class
