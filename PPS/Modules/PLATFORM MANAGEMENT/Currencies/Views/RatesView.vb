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
'       - display to be checked -> modification des noms des colones
'
'       - rates DGV rows item are not wide enough
'       - Edit rate error (not double) -> should exit edition and do nothing 
'
'
' Author: Julien Monnereau
' Last modified: 24/08/2015


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
    Private controller As ExchangeRatesController
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

    Friend Sub AttributeController(ByRef inputController As ExchangeRatesController)

        controller = inputController

    End Sub

#End Region


#Region "DGV Init and Fill"

#Region "Initialize DGV Display"

    Friend Sub InitializeDGV(ByRef currenciesList As List(Of UInt32), _
                             ByRef monthsIdList As List(Of Int32))


        ClearDictionaries()
        DGV.ColumnsHierarchy.Clear()
        DGV.RowsHierarchy.Clear()
        InitColumns(currenciesList)
        InitRows(monthsIdList)
        DataGridViewsUtil.DGVSetHiearchyFontSize(DGV, My.Settings.tablesFontSize, My.Settings.tablesFontSize)
        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
        DGV.BackColor = Color.White
        DGV.Refresh()

    End Sub

    Private Sub InitColumns(ByRef currenciesList As List(Of UInt32))

        For Each currencyId As UInt32 In currenciesList
            If currencyId <> My.Settings.mainCurrency Then AddColumn(currencyId)
        Next

    End Sub

    Private Sub AddColumn(ByRef currencyId As UInt32)

        Dim col As HierarchyItem = DGV.ColumnsHierarchy.Items.Add(GlobalVariables.Currencies.currencies_hash(My.Settings.mainCurrency)(NAME_VARIABLE) & "/" & currencyId)
        columnIDKeyDictionary.Add(col.GetUniqueID, currencyId)
        columnsKeyItemDictionary.Add(currencyId, col)

    End Sub

    Private Sub InitRows(ByRef monthsIdList As List(Of Int32))

        charts_periods.Clear()
        For Each monthId As Int32 In monthsIdList
            Dim period As Date = Date.FromOADate(monthId)
            Dim row As HierarchyItem = DGV.RowsHierarchy.Items.Add(Format(period, "MMM yyyy"))
            rowIDKeyDictionary.Add(row.GetUniqueID, monthId)
            rowsKeyItemDictionary.Add(monthId, row)
            Dim currencyTextBoxEditor As New TextBoxEditor()
            currencyTextBoxEditor.ActivationFlags = EditorActivationFlags.KEY_PRESS_ENTER
            currencyTextBoxEditor.ActivationFlags = EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL
            row.CellsEditor = currencyTextBoxEditor
            charts_periods.Add(Format(period, "MMM yyyy"))
        Next

    End Sub

#End Region

    Friend Sub DisplayRatesVersionValuesinDGV(ByRef exchangeRatesDict As Dictionary(Of String, Double))

        is_filling_cells = True
        Dim currencyId As String
        For Each column As HierarchyItem In DGV.ColumnsHierarchy.Items
            currencyId = columnIDKeyDictionary(column.GetUniqueID)

            For Each row As HierarchyItem In DGV.RowsHierarchy.Items
                Dim period As Integer = rowIDKeyDictionary(row.GetUniqueID)

                Dim rateToken As String = currencyId & Computer.TOKEN_SEPARATOR & _
                                          controller.currentRatesVersionId & Computer.TOKEN_SEPARATOR & _
                                          period

                If exchangeRatesDict.ContainsKey(rateToken) Then
                    DGV.CellsArea.SetCellValue(row, column, exchangeRatesDict(rateToken))
                Else
                    DGV.CellsArea.SetCellValue(row, column, 0)
                End If

            Next
            '    DisplayPriceCurve(currencyId, GetValuesFromPeriodsHT(ExchangeRatesDictionary(rateToken)), column.ItemIndex)
            ' reimplmement charting priority low
        Next
        is_filling_cells = False
        DGV.Refresh()

    End Sub

    Friend Sub UpdateCell(ByRef currencyId As Int32, _
                          ByRef period As Int32, _
                          ByRef value As Double)

        DGV.CellsArea.SetCellValue(rowsKeyItemDictionary(period), _
                                   columnsKeyItemDictionary(currencyId), _
                                   value)

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
                controller.UpdateRate(curr, period, args.NewValue)
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
        chart.Series.Clear()

    End Sub

    Private Sub CopyValueIntoCellsBelow(ByRef parent_row As HierarchyItem, _
                                        ByRef start_index As Int32, _
                                        ByRef column As HierarchyItem, _
                                        ByRef value As Double)

        For i As Int32 = start_index To parent_row.Items.Count - 1
            DGV.CellsArea.SetCellValue(parent_row.Items(i), column, value)
        Next

    End Sub

  

#End Region


End Class
