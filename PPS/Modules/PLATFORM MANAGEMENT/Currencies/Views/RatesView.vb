' RatesView.vb
'
' Manages the display of the exchange rates DGV in the CurrenciesManagementUI
' Manages the display of the rates chart
'
' To do: 
'       - Auto scale chart
'       - Chart axis display options
'       - Implement hierarchy item expanded -> row items column resize
'       
' Known bugs:
'       - rates DGV rows item are not wide enough
'       - Edit rate error (not double) -> should exit edition and do nothing 
'
'
' Author: Julien Monnereau
' Last modified: 08/02/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.DataVisualization.Charting


Friend Class RatesView


#Region "Instance variables"

    ' Objects
    Private DGV As vDataGridView
    Private CONTROLLER As CExchangeRatesCONTROLER
    Private chart As New Chart

    ' Variables
    Friend rowsKeyItemDictionary As New Dictionary(Of Integer, HierarchyItem)
    Friend columnsKeyItemDictionary As New Dictionary(Of String, HierarchyItem)
    Friend rowIDKeyDictionary As New Dictionary(Of String, Integer)
    Friend columnIDKeyDictionary As New Dictionary(Of String, String)
    Private is_filling_cells As Boolean
    Private colors_palette As List(Of Hashtable)
    Private charts_periods As New List(Of String)
    Private is_copying_value_down As Boolean = False

    ' Constants
    Private DGV_ITEMS_FONT_SIZE = 8
    Private DGV_CELLS_FONT_SIZE = 8
    Private LINES_WIDTH As Single = 3

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputDGV As vDataGridView, ByRef input_chart As Chart)

        DGV = inputDGV
        chart = input_chart
        DGV.AllowCopyPaste = True
        DGV.RowsHierarchy.CompactStyleRenderingEnabled = False
        colors_palette = pps_colorsMapping.GetColorsList()
        AddHandler DGV.CellValueChanging, AddressOf rates_DGV_CellValueChanging

    End Sub

    Friend Sub AttributeController(ByRef inputController As CExchangeRatesCONTROLER)

        CONTROLLER = inputController

    End Sub

#End Region


#Region "DGV Init and Fill"

#Region "Initialize DGV Display"

    Friend Sub InitializeDGV(ByRef currenciesList As List(Of String), _
                             ByRef globalPeriodsDictionary As Dictionary(Of Int32, Integer()))


        ClearDictionaries()
        DGV.ColumnsHierarchy.Clear()
        DGV.RowsHierarchy.Clear()
        InitColumns(currenciesList)
        InitRows(globalPeriodsDictionary)
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, DGV_ITEMS_FONT_SIZE, DGV_CELLS_FONT_SIZE)
        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.Refresh()

    End Sub

    Private Sub InitColumns(ByRef currenciesList As List(Of String))

        For Each curr As String In currenciesList
            If curr <> MAIN_CURRENCY Then AddColumn(curr)
        Next

    End Sub

    Private Sub AddColumn(ByRef curr As String)

        Dim col As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(curr + "/" + MAIN_CURRENCY)
        columnIDKeyDictionary.Add(col.GetUniqueID, curr)
        columnsKeyItemDictionary.Add(curr, col)

    End Sub

    Private Sub InitRows(ByRef globalPeriodsDictionary As Dictionary(Of Int32, Integer()))

        charts_periods.Clear()
        For Each yearAsInteger As Integer In globalPeriodsDictionary.Keys
            Dim row As HierarchyItem = DGV.RowsHierarchy.Items.Add(Year(Date.FromOADate(yearAsInteger)))
            For Each monthDateInteger As Integer In globalPeriodsDictionary(yearAsInteger)
                Dim period As Date = Date.FromOADate(monthDateInteger)
                AddSubRow(row, Format(period, "MMM yyyy"), monthDateInteger)
                charts_periods.Add(Format(period, "MMM yyyy"))
            Next
        Next

    End Sub

    Private Function AddRowItem(ByRef dateStr As String, _
                                ByRef DateInt As Integer) _
                                As HierarchyItem

        Dim row As HierarchyItem = DGV.RowsHierarchy.Items.Add(dateStr)
        rowIDKeyDictionary.Add(row.GetUniqueID, DateInt)
        rowsKeyItemDictionary.Add(DateInt, row)
        Return row

    End Function

    Private Sub AddSubRow(ByRef item As HierarchyItem, _
                          ByRef dateStr As String, _
                          ByRef DateInt As Integer)

        Dim row As HierarchyItem = item.Items.Add(dateStr)
        rowIDKeyDictionary.Add(row.GetUniqueID, DateInt)
        rowsKeyItemDictionary.Add(DateInt, row)
        Dim currencyTextBoxEditor As New TextBoxEditor()
        currencyTextBoxEditor.ActivationFlags = EditorActivationFlags.KEY_PRESS_ENTER
        currencyTextBoxEditor.ActivationFlags = EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL
        row.CellsEditor = currencyTextBoxEditor

    End Sub

#End Region

    Friend Sub DisplayRatesVersionValuesinDGV(ByRef versionExchangeRatesDictionary As Dictionary(Of String, Hashtable))

        FillInGridData(versionExchangeRatesDictionary)
        DGV.Refresh()

    End Sub

    Private Sub FillInGridData(ByRef exchangeRatesDictionary As Dictionary(Of String, Hashtable))

        is_filling_cells = True
        Dim curr As String
        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            curr = columnIDKeyDictionary(column.GetUniqueID)
            If exchangeRatesDictionary.ContainsKey(curr) Then
                For Each row As HierarchyItem In DGV.RowsHierarchy.Items
                    For Each subRow As HierarchyItem In row.Items
                        FillInGridCell(exchangeRatesDictionary, subRow, _
                                       curr, column)
                    Next
                Next
                DisplayPriceCurve(curr, GetValuesFromPeriodsHT(exchangeRatesDictionary(curr)), column.ItemIndex)
            End If
        Next
        is_filling_cells = False

    End Sub

    Private Sub FillInGridCell(ByRef exchangeRatesDictionary As Dictionary(Of String, Hashtable), _
                               ByRef row As HierarchyItem, _
                               ByRef curr As String, _
                               ByRef column As HierarchyItem)

        Dim value As Double
        Dim period As Integer = rowIDKeyDictionary(row.GetUniqueID)

        If exchangeRatesDictionary(curr).ContainsKey(period) Then
            value = exchangeRatesDictionary(curr)(period)
        Else
            value = 1
        End If
        DGV.CellsArea.SetCellValue(row, column, value)


    End Sub

    Friend Sub UpdateCell(ByRef curr As String, ByRef period As Integer, ByRef value As Double)

        DGV.CellsArea.SetCellValue(rowsKeyItemDictionary(period), columnsKeyItemDictionary(curr), value)

    End Sub

#End Region


#Region "Chart Display"

    Friend Sub DisplayPriceCurve(ByRef curr As String, _
                                 ByRef values As Double(), _
                                 ByRef color_index As Int32)

        Dim new_serie As New Series(curr)
        chart.Series.Add(curr)
        new_serie.ChartArea = "ChartArea1"
        chart.Series(curr).ChartType = SeriesChartType.Line
        chart.Series(curr).Color = Color.FromArgb(colors_palette(color_index)(PPS_COLORS_RED_VAR), colors_palette(color_index)(PPS_COLORS_GREEN_VAR), colors_palette(color_index)(PPS_COLORS_BLUE_VAR))
        chart.Series(curr).BorderWidth = LINES_WIDTH
        chart.Series(curr).Points.DataBindXY(charts_periods, values)

    End Sub

    Private Sub UpdateSerie(ByRef curr As String, _
                            ByRef column_curr As Int32)

        Dim values(charts_periods.Count - 1) As Double
        Dim i As Int32 = 0
        For Each row As HierarchyItem In DGV.RowsHierarchy.Items
            For Each sub_row As HierarchyItem In row.Items
                values(i) = DGV.CellsArea.GetCellValue(sub_row, DGV.ColumnsHierarchy.Items(column_curr))
                i = i + 1
            Next
        Next
        chart.Series(curr).Points.DataBindXY(charts_periods, values)
        chart.Update()

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub CopyRateValueDown()

        Dim value As Double = DGV.CellsArea.SelectedCells(0).Value
        Dim column As HierarchyItem = DGV.CellsArea.SelectedCells(0).ColumnItem
        Dim row As HierarchyItem = DGV.CellsArea.SelectedCells(0).RowItem
        is_copying_value_down = True
        If Not row.ParentItem Is Nothing Then
            CopyValueIntoCellsBelow(row.ParentItem, row.ItemIndex + 1, column, value)
            For i = row.ParentItem.ItemIndex + 1 To DGV.RowsHierarchy.Items.Count - 1
                CopyValueIntoCellsBelow(DGV.RowsHierarchy.Items(i), 0, column, value)
            Next
        End If
        is_copying_value_down = False

    End Sub

#End Region


#Region "DGV Events"

    Private Sub rates_DGV_CellValueChanging(sender As Object, args As CellValueChangingEventArgs)

        If is_filling_cells = False Then
            If Not IsNumeric(args.NewValue) Then
                args.Cancel = True
            Else
                Dim curr As String = columnIDKeyDictionary(args.Cell.ColumnItem.GetUniqueID)
                Dim period As String = rowIDKeyDictionary(args.Cell.RowItem.GetUniqueID)
                CONTROLLER.UpdateRate(curr, period, args.NewValue)
                If is_copying_value_down = False Then UpdateSerie(curr, args.Cell.ColumnItem.ItemIndex)
            End If
        End If

    End Sub

#End Region


#Region "Utilities"

    Private Sub ClearDictionaries()

        rowsKeyItemDictionary.Clear()
        columnsKeyItemDictionary.Clear()
        rowIDKeyDictionary.Clear()
        columnIDKeyDictionary.Clear()
        DGV.RowsHierarchy.Clear()
        DGV.ColumnsHierarchy.Clear()

    End Sub

    Private Sub CopyValueIntoCellsBelow(ByRef parent_row As HierarchyItem, _
                                        ByRef start_index As Int32, _
                                        ByRef column As HierarchyItem, _
                                        ByRef value As Double)

        For i As Int32 = start_index To parent_row.Items.Count - 1
            DGV.CellsArea.SetCellValue(parent_row.Items(i), column, value)
        Next

    End Sub

    Private Function GetValuesFromPeriodsHT(ByRef ht As Hashtable) As Double()

        Dim tmp_array(charts_periods.Count - 1) As Double
        Dim i As Int32 = 0
        For Each row As HierarchyItem In DGV.RowsHierarchy.Items
            For Each sub_row As HierarchyItem In row.Items
                tmp_array(i) = ht(rowIDKeyDictionary(sub_row.GetUniqueID))
                i = i + 1
            Next
        Next
        Return tmp_array

    End Function

#End Region


End Class
