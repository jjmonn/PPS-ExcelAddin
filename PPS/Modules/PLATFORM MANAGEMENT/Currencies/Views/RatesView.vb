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
' Last modified: 21/01/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections.Generic
Imports System.Collections
Imports ZedGraph
Imports System.Drawing
Imports System.Windows.Forms


Friend Class RatesView


#Region "Instance variables"

    ' Objects
    Private DGV As vDataGridView
    Private CONTROLLER As CExchangeRatesCONTROLER

    ' Variables
    Friend rowsKeyItemDictionary As New Dictionary(Of Integer, HierarchyItem)
    Friend columnsKeyItemDictionary As New Dictionary(Of String, HierarchyItem)
    Friend rowIDKeyDictionary As New Dictionary(Of String, Integer)
    Friend columnIDKeyDictionary As New Dictionary(Of String, String)
    Friend chartSeriesDictionary As New Dictionary(Of String, PointPairList)
    Private myPane As GraphPane
    Private is_filling_cells As Boolean

    ' Flags
    Private chartSerieDisplayed As Boolean
    Private chartDisplayedCurrency As String

    ' Constants
    Private DGV_ITEMS_FONT_SIZE = 8
    Private DGV_CELLS_FONT_SIZE = 8

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputDGV As vDataGridView)

        DGV = inputDGV

        With DGV
            .AllowCopyPaste = True
            .RowsHierarchy.CompactStyleRenderingEnabled = False
            AddHandler .CellValueChanging, AddressOf rates_DGV_CellValueChanging
        End With

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

        For Each yearAsInteger As Integer In globalPeriodsDictionary.Keys
            Dim row As HierarchyItem = DGV.RowsHierarchy.Items.Add(Year(Date.FromOADate(yearAsInteger)))
            For Each monthDateInteger As Integer In globalPeriodsDictionary(yearAsInteger)
                Dim period As Date = Date.FromOADate(monthDateInteger)
                AddSubRow(row, Format(period, "MMM yyyy"), monthDateInteger)
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
                        FillInGridCell(exchangeRatesDictionary, _
                                       subRow, _
                                       curr, _
                                       column)
                    Next
                Next
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

    Friend Sub DisplayCurrencyCurve(ByRef currencyKey As String)

        ReLoadRatesSerie(currencyKey)
        myPane.Title.Text = currencyKey + "/" + MAIN_CURRENCY + " rates curve"
        myPane.YAxis.Title.Text = currencyKey + "/" + MAIN_CURRENCY

        chartDisplayedCurrency = currencyKey

    End Sub

    ' Reload a rates serie
    Private Sub ReLoadRatesSerie(ByRef curr As String)

        Dim col As HierarchyItem = columnsKeyItemDictionary(curr)
        Dim tmpList = New PointPairList()
        For Each row As HierarchyItem In DGV.RowsHierarchy.Items

            Dim value As Double = DGV.CellsArea.GetCellValue(row, col)
            Dim x As Double = CDbl(New XDate(CDbl(row.Caption)))
            tmpList.Add(x, value)

        Next
        chartSeriesDictionary(curr) = tmpList

    End Sub


#End Region


#Region "Interface"

    Protected Friend Sub CopyRateValueDown()

        Dim value As Double = DGV.CellsArea.SelectedCells(0).Value
        Dim column As HierarchyItem = DGV.CellsArea.SelectedCells(0).ColumnItem
        Dim row As HierarchyItem = DGV.CellsArea.SelectedCells(0).RowItem

        If Not row.ParentItem Is Nothing Then
            CopyValueIntoCellsBelow(row.ParentItem, row.ItemIndex + 1, column, value)
            For i = row.ParentItem.ItemIndex + 1 To DGV.RowsHierarchy.Items.Count - 1
                CopyValueIntoCellsBelow(DGV.RowsHierarchy.Items(i), 0, column, value)
            Next
        End If

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

#End Region


End Class
