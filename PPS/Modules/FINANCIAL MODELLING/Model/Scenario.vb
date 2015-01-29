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
' Last modified: 25/12/2014


Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.Utilities


Friend Class Scenario


#Region "Instance Variable"

    ' objects
    Friend InputsDGV As New vDataGridView
    Friend OutputDGV As New vDataGridView
    Private FModellingAccount As FModellingAccount
    Friend Outputchart As New Chart
    Private scenarioTV As TreeView

    ' Variables
    Protected Friend scenario_id As String
    Protected Friend data_dic As Dictionary(Of String, Double())
    Private periods_array As Int32()
    Private charts_periods As New List(Of Int32)
    Private ConstraintsComboBox As New ComboBoxEditor()
    Private constraints_text_box_editor As New TextBoxEditor()
    Friend dividend_formula_option As Int32 = -1
    Private OutputsDGV_id_rows_Dictionary As New Dictionary(Of String, HierarchyItem)
    Private inputsDGV_id_rows_Dictionary As New Dictionary(Of String, HierarchyItem)
    Private current_constraint_row As HierarchyItem = Nothing
    Private Outputs_name_id_dic As Hashtable

    ' Display
    Private Const CB_WIDTH As Double = 20
    Private Const CB_NB_ITEMS_DISPLAYED As Int32 = 7
    Private DGV_CELLS_FONT_SIZE = 8
    Private Const DGV_THEME = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER


#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef input_id As String, _
                             ByRef inputTV As TreeView, _
                             ByRef periods_list As Int32(), _
                             ByRef outputs_dic As Hashtable, _
                             ByRef RC_Menu As ContextMenuStrip, _
                             ByRef input_FModellingAccount As FModellingAccount)

        scenario_id = input_id
        scenarioTV = inputTV
        Outputs_name_id_dic = outputs_dic
        InitializeConstraintsCB()
        periods_array = periods_list
        FModellingAccount = input_FModellingAccount
        ' set up the style for items and cells (font size) to be applied on DGVs !!

        InputsDGV.ContextMenuStrip = RC_Menu
        BuildInputsDGV(periods_list)
        BuildOutputsDGV(periods_list)
        BuildOutputsChart()

    End Sub

    Private Sub InitializeConstraintsCB()

        ConstraintsComboBox.DropDownHeight = ConstraintsComboBox.ItemHeight * CB_NB_ITEMS_DISPLAYED
        ConstraintsComboBox.DropDownWidth = CB_WIDTH
        For Each constraint As String In Outputs_name_id_dic.Keys
            ConstraintsComboBox.Items.Add(constraint)
        Next

    End Sub

    Private Sub BuildInputsDGV(ByRef periods_list As Int32())

        InputsDGV.RowsHierarchy.Visible = False
        InputsDGV.AllowCopyPaste = True

        constraints_text_box_editor.ActivationFlags = EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL

        ' Init Columns
        Dim item_column = InputsDGV.ColumnsHierarchy.Items.Add("Constraint")
        item_column.CellsEditor = ConstraintsComboBox

        For Each period In periods_list
            Dim column = InputsDGV.ColumnsHierarchy.Items.Add(Format(DateTime.FromOADate(period), "yyyy"))
            column.CellsEditor = constraints_text_box_editor
        Next

        DataGridViewsUtil.InitDisplayVDataGridView(InputsDGV, DGV_THEME)
        ' DataGridViewsUtil.DGVSetHiearchyFontSize(InputsDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        InputsDGV.ColumnsHierarchy.AutoStretchColumns = True
        AddHandler InputsDGV.CellValueChanging, AddressOf inputsDGV_CellValueChanging
        AddHandler InputsDGV.CellMouseClick, AddressOf inputDGV_CellMouseClick
        InputsDGV.BackColor = System.Drawing.SystemColors.Control

    End Sub

    Private Sub BuildOutputsDGV(ByRef periods_list As Int32())

        ' inputs in dark blue ?
        ' Init Columns
        For Each period In periods_list
            Dim column = OutputDGV.ColumnsHierarchy.Items.Add(Format(DateTime.FromOADate(period), "yyyy"))
            column.CellsTextAlignment = DataGridViewContentAlignment.MiddleRight
        Next

        ' Init Rows
        For Each output_id In Outputs_name_id_dic.Keys
            Dim row = OutputDGV.RowsHierarchy.Items.Add(output_id)
            row.CellsFormatString = FModellingAccount.ReadFModellingAccount(Outputs_name_id_dic(output_id), FINANCIAL_MODELLING_FORMAT_VARIABLE)
            OutputsDGV_id_rows_Dictionary.Add(Outputs_name_id_dic(output_id), row)
        Next
        OutputDGV.ColumnsHierarchy.AutoStretchColumns = True
        DataGridViewsUtil.InitDisplayVDataGridView(OutputDGV, DGV_THEME)
        DataGridViewsUtil.EqualizeColumnsAndRowsHierarchyWidth(OutputDGV)
        DataGridViewsUtil.FormatDGVRowsHierarchy(OutputDGV)
        DataGridViewsUtil.DGVSetHiearchyFontSize(OutputDGV, DGV_CELLS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        OutputDGV.BackColor = System.Drawing.SystemColors.Control

    End Sub

    Private Sub BuildOutputsChart()

        ' Series display settings to go in a chart Util class
        Dim ChartArea1 As New ChartArea
        Dim ChartArea2 As New ChartArea
        Outputchart.ChartAreas.Add(ChartArea1)
        Outputchart.ChartAreas.Add(ChartArea2)

        ChartArea1.AxisY.LabelAutoFitMaxFontSize = 8
        ChartArea2.AxisY.LabelAutoFitMaxFontSize = 8
        ChartArea1.AxisX.LabelAutoFitMaxFontSize = 8
        ChartArea2.AxisX.LabelAutoFitMaxFontSize = 8

        Outputchart.Palette = ChartColorPalette.Berry
        ChartArea1.AxisX.MajorGrid.Enabled = False

        Dim legend1 As New Legend
        Outputchart.Legends.Add(legend1)
        Outputchart.Legends(0).Docking = Docking.Bottom


        Dim dividends_serie As New Series("Dividends")
        Dim cash_serie As New Series("Cash")
        cash_serie.ChartType = SeriesChartType.Line
        Dim payout As New Series("Payout")
        Dim doe As New Series("D/E")

        Outputchart.Series.Add(dividends_serie)
        Outputchart.Series.Add(cash_serie)
        Outputchart.Series.Add(payout)
        Outputchart.Series.Add(doe)

        dividends_serie.ChartArea = "ChartArea1"
        cash_serie.ChartArea = "ChartArea1"
        payout.ChartArea = "ChartArea2"
        doe.ChartArea = "ChartArea2"

        ChartArea1.Position.X = 0
        ChartArea1.Position.Y = 0
        ChartArea1.Position.Width = 50
        ChartArea1.Position.Height = 80

        ChartArea2.Position.X = 50
        ChartArea2.Position.Y = 0
        ChartArea2.Position.Width = 50
        ChartArea2.Position.Height = 80

        For Each serie In Outputchart.Series
            serie.CustomProperties = "DrawingStyle=cylinder"
        Next

        For Each period In periods_array
            charts_periods.Add(Format(DateTime.FromOADate(period), "yyyy"))
        Next

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub FillOutputs(ByRef input_data_dic As Dictionary(Of String, Double()))

        data_dic = input_data_dic
        For Each id In Outputs_name_id_dic.Values
            For j As Int32 = 0 To OutputDGV.ColumnsHierarchy.Items.Count - 1
                Dim row As HierarchyItem = OutputsDGV_id_rows_Dictionary(id)
                Dim value = data_dic(id)(j)
                If row.CellsFormatString = PRCT_FORMAT_STRING Then value = value / 100
                OutputDGV.CellsArea.SetCellValue(row, OutputDGV.ColumnsHierarchy.Items(j), value)
            Next
        Next

        ' Bind chart series
        Outputchart.Series("Dividends").Points.DataBindXY(charts_periods, data_dic("div"))
        Outputchart.Series("Cash").Points.DataBindXY(charts_periods, data_dic("cash"))
        Outputchart.Series("Payout").Points.DataBindXY(charts_periods, data_dic("payout"))
        Outputchart.Series("D/E").Points.DataBindXY(charts_periods, data_dic("doe"))


    End Sub

    Protected Friend Sub AddConstraint(ByRef id As String, _
                                       Optional ByRef name As String = "",
                                       Optional ByRef set_default_value As Boolean = False, _
                                       Optional ByRef default_value As Double = Nothing)

        Dim new_row = InputsDGV.RowsHierarchy.Items.Add(id)
        Dim CStyle As GridCellStyle = GridTheme.GetDefaultTheme(InputsDGV.VIBlendTheme).GridCellStyle
        CStyle.TextColor = System.Drawing.Color.Blue
        new_row.CellsStyle = CStyle

            If name <> "" Then InputsDGV.CellsArea.SetCellValue(new_row, InputsDGV.ColumnsHierarchy.Items(0), name)
            If set_default_value = True Then
                For j = 1 To InputsDGV.ColumnsHierarchy.Items.Count - 1
                    InputsDGV.CellsArea.SetCellValue(new_row, InputsDGV.ColumnsHierarchy.Items(j), default_value)
                Next
            End If
            inputsDGV_id_rows_Dictionary.Add(id, new_row)
            new_row.Selected = True

    End Sub

    Protected Friend Function DeleteDGVActiveConstraint() As String

        If Not current_constraint_row Is Nothing Then
            Dim constraint_id = current_constraint_row.Caption
            If current_constraint_row.ItemIndex <= 2 Then Return ""
            current_constraint_row.Delete()
            Return constraint_id
        End If
        Return ""

    End Function

    Protected Friend Function DeleteConstraint(ByRef constraint_id) As Boolean

        Dim row As HierarchyItem = inputsDGV_id_rows_Dictionary(constraint_id)
        If current_constraint_row.ItemIndex <= 2 Then Return False
        row.Delete()
        inputsDGV_id_rows_Dictionary.Remove(constraint_id)
        Return True

    End Function

    Protected Friend Sub Delete()

        Me.Finalize()

    End Sub

#End Region


#Region "Events"

    Private Sub inputsDGV_CellValueChanging(sender As Object, args As CellValueChangingEventArgs)

        Select Case args.Cell.ColumnItem.ItemIndex
            Case 0
                scenarioTV.Nodes.Find(args.Cell.RowItem.Caption, True)(0).Text = args.NewValue

                ' maybe check if the constraint is in the list !!
            Case Else
                If Not IsNumeric(args.NewValue) Then
                    args.Cancel = True
                End If
        End Select


    End Sub

    Private Sub inputDGV_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        current_constraint_row = args.Cell.RowItem

    End Sub

#End Region




End Class
