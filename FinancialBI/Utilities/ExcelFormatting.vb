' CExcelFormatting.vb
'
' Manages Formats of an Excel WS 
'
'
' To do:
'       - Dates formatting ?
'       - Cases where accounts are not in column 1 ... !
'
'
' Known bugs:
'       -
'
' 
' Author: Julien Monnereau
' Last modified: 04/11/2015


Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports System.Collections
Imports System.Drawing
Imports CRUD

Friend Class ExcelFormatting


    ' Identify the current range and 
    ' Param: REPORT_FORMAT_CODE or INPUT_FORMAT_CODE
    Friend Shared Sub FormatExcelRange(ByRef first_range_cell As Excel.Range, _
                                        ByRef currency As Int32, _
                                        Optional ByRef startingDate As Date = Nothing)

        Dim ws As Excel.Worksheet = first_range_cell.Worksheet
        Dim l_Range As Excel.Range
        Dim l_lastCell As Excel.Range = GeneralUtilities.GetRealLastCell(ws)
        If l_lastCell IsNot Nothing Then
            l_Range = ws.Range(first_range_cell, l_lastCell)
            FormatExcelRangeAs(l_Range, currency, startingDate)
        Else
            'MsgBox() ?
        End If

    End Sub

    Friend Shared Sub FormatExcelRangeAs(ByRef inputRange As Excel.Range, _
                                         ByRef currency As UInt32, _
                                         Optional ByRef startingDate As Date = Nothing)

        Dim l_currency As Currency = GlobalVariables.Currencies.GetValue(currency)
        If l_currency Is Nothing Then Exit Sub
        Dim formatsDictionary As New SafeDictionary(Of String, Formats.FinancialBIFormat)
        formatsDictionary.Add("t", Formats.GetFormat("t"))
        formatsDictionary.Add("i", Formats.GetFormat("i"))
        formatsDictionary.Add("n", Formats.GetFormat("n"))
        formatsDictionary.Add("d", Formats.GetFormat("d"))

        Dim formatCode As String
        For Each row As Excel.Range In inputRange.Rows

            Dim accValue As String = row.Cells(1, 1).value2
            If Not accValue Is Nothing Then
                Dim l_account As Account = GlobalVariables.Accounts.GetValue(accValue)
                If Not l_account Is Nothing Then
                    formatCode = l_account.FormatId

                    ' Colors
                    row.Interior.Color = formatsDictionary(formatCode).backColor
                    row.Font.Color = formatsDictionary(formatCode).textColor

                    ' Format
                    row.Columns(1).IndentLevel = formatsDictionary(formatCode).indent
                    If formatsDictionary(formatCode).isBold = True Then row.Font.Bold = True
                    If formatsDictionary(formatCode).isItalic = True Then row.Font.Italic = True

                    Select Case l_account.Type
                        Case Account.AccountType.MONETARY : row.Cells.NumberFormat = "[$" & l_currency.Symbol & "]#,##0.00;([$" & l_currency.Symbol & "]#,##0.00)"
                        Case Account.AccountType.PERCENTAGE : row.Cells.NumberFormat = "0.00%"        ' put this in a table ?
                        Case Account.AccountType.NUMBER : row.Cells.NumberFormat = "#,##0.00"        ' further evolution set unit ?
                        Case Account.AccountType.DATE_ : row.Cells.NumberFormat = "d-mmm-yy" ' d-mmm-yy
                        Case Else : row.Cells.NumberFormat = "#,##0.00"
                    End Select

                    ' Borders
                    If formatsDictionary(formatCode).bordersPresent = True Then
                        row.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = formatsDictionary(formatCode).bordersColor
                        row.Borders(Excel.XlBordersIndex.xlEdgeTop).Color = formatsDictionary(formatCode).bordersColor
                    End If
                End If
            End If
        Next
        inputRange.Columns.AutoFit()

    End Sub

    Public Shared Sub FormatEntitiesReport(ByRef area As Excel.Range)

        area.Font.Color = Color.Black
        Dim subArea As Excel.Range
        For j = 1 To area.Columns.Count - 1
            Dim cell1 As Excel.Range = area.Cells(1, j).Offset(-3, 0)
            Dim cell2 As Excel.Range = area.Cells(1, j).Offset(area.Rows.Count - 4, 0)
            subArea = area.Range(cell1, cell2)
            subArea.Borders(Excel.XlBordersIndex.xlEdgeRight).Color = Color.Black
            subArea.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = Color.Black
            subArea.Borders(Excel.XlBordersIndex.xlEdgeLeft).Color = Color.Black
            subArea.Borders(Excel.XlBordersIndex.xlEdgeTop).Color = Color.Black
            area.EntireColumn(j).autofit()
            subArea.Cells(1, 1).Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = Color.Black
        Next

    End Sub


#Region "Utilities"

    Shared Function DatesLineIdentification(ByRef inputRange As Excel.Range) As Int32

        For Each cell In inputRange
            If IsDate(cell.value) Then Return cell.row
        Next
        Return -1

    End Function

    Shared Function GetStaringPeriodColumn(ByRef inputRange As Excel.Range, _
                                            ByRef startingDate As Date) As Int32

        Dim datesLine As Int32 = DatesLineIdentification(inputRange)
        On Error Resume Next
        If datesLine <> -1 Then
            'For i = 1 To inputRange.Columns.Count
            '    Dim test = inputRange.Cells(datesLine, i).value
            '    Dim test2 = Date.FromOADate(CDbl(inputRange.Cells(datesLine, i).value))
            '    If IsDate(inputRange.Cells(datesLine, i).value) _
            '    AndAlso inputRange.Cells(datesLine, i).value = startingDate Then Return i
            'Next

            ' fin a way to find the date stub !!!!
        End If
        Return 2

    End Function

   
#End Region


End Class
