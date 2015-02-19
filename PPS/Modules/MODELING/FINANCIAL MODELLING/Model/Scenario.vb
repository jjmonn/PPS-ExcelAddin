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
' Last modified: 18/02/2015


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
    Protected Friend ScenarioDGV As New vDataGridView
    Private FModellingAccount As FModellingAccount
    Protected Friend Outputchart As New Chart
    Protected Friend ExportedChart As New Chart
    Protected Friend DetailedDGV As New vDataGridView
    Private scenarioTV As TreeView

    ' Variables
    Protected Friend scenario_id As String
    Protected Friend data_dic As Dictionary(Of String, Double())
    Private periods_array As Int32()
    Private charts_periods As New List(Of Int32)
    Private constraints_text_box_editor As New TextBoxEditor()
    Protected Friend dividend_formula_option As Int32 = -1
    Protected Friend scenarioDGV_id_row_index_Dictionary As New Dictionary(Of String, Int32)
    Private current_constraint_cell As GridCell = Nothing
    Private Outputs_name_id_dic As Hashtable
    Private is_updating_value As Boolean
    Private manual_edition As Boolean

    ' Display
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER
    Protected Friend Const PERCENT_FORMAT As String = "{0:P0}"


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_id As String, _
                             ByRef inputTV As TreeView, _
                             ByRef periods_list As Int32(), _
                             ByRef outputs_dic As Hashtable, _
                             ByRef RC_Menu As ContextMenuStrip, _
                             ByRef input_FModellingAccount As FModellingAccount, _
                             ByRef FModeling_constraint_name_id_dic As Hashtable)

        scenario_id = input_id
        scenarioTV = inputTV
        Outputs_name_id_dic = outputs_dic
        periods_array = periods_list
        FModellingAccount = input_FModellingAccount
        ScenarioDGV.ContextMenuStrip = RC_Menu
        BuildScenarioDGV(FModeling_constraint_name_id_dic)
        BuildOutputsChart(Outputchart)
        AddHandler ScenarioDGV.CellBeginEdit, AddressOf ScenarioDGV_CellBeginEdit
   
    End Sub

    Protected Friend Sub BuildScenarioDGV(ByRef FModeling_constraint_name_id_dic As Hashtable)

        ScenarioDGV.RowsHierarchy.Visible = False
        '   ScenarioDGV.AllowCopyPaste = True
        constraints_text_box_editor.ActivationFlags = EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL

        ' Init Columns
        ScenarioDGV.ColumnsHierarchy.Items.Add("")
        Dim col As HierarchyItem = ScenarioDGV.ColumnsHierarchy.Items.Add("Chart")
        col.CellsTextAlignment = Drawing.ContentAlignment.MiddleCenter
        For Each period In periods_array
            Dim column = ScenarioDGV.ColumnsHierarchy.Items.Add(Format(DateTime.FromOADate(period), "yyyy"))
            column.CellsEditor = constraints_text_box_editor
            column.TextAlignment = Drawing.ContentAlignment.MiddleCenter
            column.CellsTextAlignment = DataGridViewContentAlignment.MiddleRight
        Next

        ' Init Rows
        InitializeConstraints(FModeling_constraint_name_id_dic)
        For Each f_account_id In Outputs_name_id_dic.Values
            If scenarioDGV_id_row_index_Dictionary.ContainsKey(f_account_id) = False Then AddRow(f_account_id)
        Next
        ScenarioDGV.BackColor = Drawing.Color.White
        DataGridViewsUtil.InitDisplayVDataGridView(ScenarioDGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(ScenarioDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        ScenarioDGV.ColumnsHierarchy.AutoStretchColumns = True
        ScenarioDGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        ScenarioDGV.Refresh()
        AddHandler ScenarioDGV.CellValueChanging, AddressOf scenarioDGV_CellValueChanging
        AddHandler ScenarioDGV.CellMouseClick, AddressOf scenarioDGV_CellMouseClick

    End Sub

    Private Sub InitializeConstraints(ByRef FModeling_constraint_name_id_dic As Hashtable)

        For Each constraint_id As String In FModeling_constraint_name_id_dic.Values
            AddRow(constraint_id)
        Next

    End Sub

    Private Sub AddRow(ByRef f_account_id As String)

        Dim row As HierarchyItem = ScenarioDGV.RowsHierarchy.Items.Add(f_account_id)
        Dim f_account_name As String = FModellingAccount.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_NAME_VARIABLE)
        ScenarioDGV.CellsArea.SetCellValue(row, ScenarioDGV.ColumnsHierarchy.Items(0), f_account_name)
        row.CellsFormatString = FModellingAccount.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_FORMAT_VARIABLE)
        If scenarioDGV_id_row_index_Dictionary.ContainsKey(f_account_id) = False Then scenarioDGV_id_row_index_Dictionary.Add(f_account_id, row.ItemIndex)
        Dim checkBoxEditor As New CheckBoxEditor
        ScenarioDGV.CellsArea.SetCellEditor(row, ScenarioDGV.ColumnsHierarchy.Items(1), checkBoxEditor)
        AddHandler checkBoxEditor.CheckedChanged, AddressOf OutputDGV_Checkedchanged

    End Sub

    Protected Friend Sub BuildOutputsChart(ByRef chart As Chart)

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


#Region "Interface"

    Protected Friend Sub FillOutputs(ByRef input_data_dic As Dictionary(Of String, Double()))

        data_dic = input_data_dic
        For Each id In Outputs_name_id_dic.Values
            For j As Int32 = 2 To ScenarioDGV.ColumnsHierarchy.Items.Count - 1
                Dim row As HierarchyItem = ScenarioDGV.RowsHierarchy.Items(scenarioDGV_id_row_index_Dictionary(id))
                Dim value = data_dic(id)(j - 2)
                If row.CellsFormatString = PRCT_FORMAT_STRING Then value = value / 100
                ScenarioDGV.CellsArea.SetCellValue(row, ScenarioDGV.ColumnsHierarchy.Items(j), value)
            Next
        Next
        ScenarioDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        ScenarioDGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()
        BindChartSeries()
        FillDetailedDGV()

    End Sub

    Protected Friend Sub AddConstraint(ByRef id As String, _
                                       ByRef f_account_name As String,
                                       Optional ByRef default_value As Double = 0)

        Dim f_account_id As String = Outputs_name_id_dic(f_account_name)
        Dim row As HierarchyItem = ScenarioDGV.RowsHierarchy.Items(scenarioDGV_id_row_index_Dictionary(f_account_id))
        FormatRow(row, True)

        For j = 2 To ScenarioDGV.ColumnsHierarchy.Items.Count - 1
            ScenarioDGV.CellsArea.SetCellValue(row, ScenarioDGV.ColumnsHierarchy.Items(j), default_value)
        Next
        ScenarioDGV.Refresh()

    End Sub

    Protected Friend Function DeleteDGVActiveConstraint() As String

        If Not current_constraint_cell Is Nothing Then
            Dim constraint_id = current_constraint_cell.RowItem.Caption
            If current_constraint_cell.RowItem.ItemIndex <= 2 Then Return ""
            Format(current_constraint_cell.RowItem, False)
            Return constraint_id
        End If
        Return ""

    End Function

    Protected Friend Function DeleteConstraint(ByRef constraint_id) As Boolean

        Dim row As HierarchyItem = ScenarioDGV.RowsHierarchy.Items(scenarioDGV_id_row_index_Dictionary(constraint_id))
        If current_constraint_cell.RowItem.ItemIndex <= 2 Then Return False
        FormatRow(row, False)
        Return True

    End Function

    Protected Friend Sub Delete()

        Me.Finalize()

    End Sub

    Protected Friend Sub AddSerieToChart(ByRef f_account_id As String)

        Dim serie_ht As Hashtable = FModellingAccount.GetSeriHT(f_account_id)
        serie_ht.Add(REPORTS_SERIE_WIDTH_VAR, 25)
        ChartsUtilities.AddSerieToChart(Outputchart, serie_ht, serie_ht(FINANCIAL_MODELLING_SERIE_CHART_VARIABLE))
        Outputchart.Series(FModellingAccount.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_NAME_VARIABLE)).Points.DataBindXY(charts_periods, data_dic(f_account_id))

    End Sub

    Protected Friend Sub CopyValueRight()

        DataGridViewsUtil.CopyValueRight(ScenarioDGV, current_constraint_cell)
        ScenarioDGV.Refresh()

    End Sub

#End Region


#Region "Detailed DGV"

    Protected Friend Function GetDetailedDGV(ByRef f_accounts_id_list As List(Of String)) As vDataGridView

        DetailedDGV.RowsHierarchy.Visible = False
        DetailedDGV.BackColor = Drawing.Color.White
        'DetailedDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DetailedDGV.ColumnsHierarchy.AutoStretchColumns = True
        DetailedDGV.ColumnsHierarchy.Items.Add("")
        For Each period In periods_array
            Dim column As HierarchyItem = DetailedDGV.ColumnsHierarchy.Items.Add(Format(DateTime.FromOADate(period), "yyyy"))
            column.TextAlignment = Drawing.ContentAlignment.MiddleCenter
            column.CellsTextAlignment = DataGridViewContentAlignment.MiddleRight
        Next
        DataGridViewsUtil.InitDisplayVDataGridView(DetailedDGV, DGV_THEME)
        DataGridViewsUtil.DGVSetHiearchyFontSize(DetailedDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)

        For Each f_account_id As String In f_accounts_id_list
            Dim row As HierarchyItem = detailedDGV.RowsHierarchy.Items.Add(f_account_id)
            detailedDGV.CellsArea.SetCellValue(row, detailedDGV.ColumnsHierarchy.Items(0), FModellingAccount.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_NAME_VARIABLE))
            row.CellsFormatString = FModellingAccount.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_FORMAT_VARIABLE)
            FormatDetailedDGVRow(row, FModellingAccount.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_TYPE_VARIABLE))
        Next
        FillDetailedDGV()
        Return detailedDGV

    End Function

    Protected Friend Sub FillDetailedDGV()

        For Each row As HierarchyItem In DetailedDGV.RowsHierarchy.Items
            For j As Int32 = 1 To DetailedDGV.ColumnsHierarchy.Items.Count - 1
                Dim value = data_dic(row.Caption)(j - 1)
                If row.CellsFormatString = PRCT_FORMAT_STRING Then value = value / 100
                DetailedDGV.CellsArea.SetCellValue(row, DetailedDGV.ColumnsHierarchy.Items(j), value)
            Next
        Next
        DetailedDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DetailedDGV.ColumnsHierarchy.ResizeColumnsToFitGridWidth()

    End Sub

    Private Sub FormatDetailedDGVRow(ByRef row As HierarchyItem, _
                                     ByRef type As String)

        Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(row.DataGridView.VIBlendTheme).GridCellStyle
        Select Case type
            Case FINANCIAL_MODELLING_INPUT_TYPE : CStyle.TextColor = System.Drawing.Color.Blue
            Case FINANCIAL_MODELLING_OUTPUT_TYPE : CStyle.TextColor = System.Drawing.Color.DarkBlue
            Case FINANCIAL_MODELLING_EXPORT_TYPE, FINANCIAL_MODELLING_FORMULA_TYPE : CStyle.TextColor = System.Drawing.Color.Purple
            Case FINANCIAL_MODELLING_CONSTRAINT_TYPE : CStyle.TextColor = System.Drawing.Color.Red
        End Select
        row.CellsStyle = CStyle

    End Sub

#End Region


#Region "Events"

    Private Sub ScenarioDGV_CellBeginEdit(sender As Object, e As EventArgs)

        manual_edition = True

    End Sub

    Private Sub scenarioDGV_CellValueChanging(sender As Object, args As CellValueChangingEventArgs)

        If manual_edition = True AndAlso is_updating_value = False Then
            Select Case args.Cell.ColumnItem.ItemIndex
                Case Is > 1
                    If args.NewValue = Nothing Then Exit Sub
                    If Not IsNumeric(args.NewValue) Then
                        args.Cancel = True
                    Else
                        Dim f_account_id As String = args.Cell.RowItem.Caption
                        If FModellingAccount.ReadFModellingAccount(f_account_id, FINANCIAL_MODELLING_FORMAT_VARIABLE) = PERCENT_FORMAT Then
                            is_updating_value = True
                            args.Cell.Value = args.NewValue / 100
                            args.Cell.RowItem.CellsFormatString = PERCENT_FORMAT
                            args.Cancel = True
                            is_updating_value = False
                        End If
                    End If
            End Select
            manual_edition = False
        End If

    End Sub

    Private Sub scenarioDGV_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        current_constraint_cell = args.Cell

    End Sub

    Private Sub OutputDGV_Checkedchanged(sender As Object, e As EventArgs)

        BindChartSeries()

    End Sub

#End Region


#Region "Utilities"

    Private Sub FormatRow(ByRef row As HierarchyItem, ByRef is_input As Boolean)

        Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(ScenarioDGV.VIBlendTheme).GridCellStyle
        If is_input Then
            CStyle.TextColor = System.Drawing.Color.Red
        Else
            CStyle.TextColor = System.Drawing.Color.Black
        End If
        row.CellsStyle = CStyle

    End Sub

    Private Sub BindChartSeries()

        Outputchart.Series.Clear()
        For Each row As HierarchyItem In ScenarioDGV.RowsHierarchy.Items
            Dim cb As CheckBoxEditor = ScenarioDGV.CellsArea.GetCellEditor(row, ScenarioDGV.ColumnsHierarchy.Items(1))
            Dim checkBox As vCheckBox = TryCast(cb.Control, vCheckBox)
            If checkBox.Checked = True Then
                AddSerieToChart(row.Caption)
            End If
        Next
        DuplicateChart()

    End Sub

    Protected Friend Sub DuplicateChart()

        Dim myStream As System.IO.MemoryStream = New System.IO.MemoryStream()
        Outputchart.Serializer.Save(myStream)
        ExportedChart.Serializer.Load(myStream)

    End Sub

#End Region



End Class
