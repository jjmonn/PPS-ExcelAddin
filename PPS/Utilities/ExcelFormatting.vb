﻿' CExcelFormatting.vb
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
' Last modified: 17/07/2015


Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports System.Collections
Imports System.Drawing



Friend Class ExcelFormatting


    ' Identify the current range and 
    ' Param: REPORT_FORMAT_CODE or INPUT_FORMAT_CODE
    Friend Shared Sub FormatExcelRange(ByRef first_range_cell As Excel.Range, _
                                        ByRef format As String, _
                                        ByRef currency As String, _
                                        Optional ByRef startingDate As Date = Nothing)

        Dim ws As Excel.Worksheet = first_range_cell.Worksheet
        Dim tmpRange As Excel.Range
        tmpRange = ws.Range(first_range_cell, Utilities_Functions.GetRealLastCell(ws))
        FormatExcelRangeAs(tmpRange, format, currency, startingDate)

    End Sub

    Friend Shared Sub FormatExcelRangeAs(ByRef inputRange As Excel.Range, _
                                                 ByRef reportFormat As String, _
                                                 ByRef currency As String, _
                                                 Optional ByRef startingDate As Date = Nothing)

        Dim AccountsNameFormatDictionary As Hashtable = globalvariables.accounts.GetAccountsDictionary(NAME_VARIABLE, ACCOUNT_FORMAT_VARIABLE)
        Dim AccountsNameTypeDict As Hashtable = globalvariables.accounts.GetAccountsDictionary(NAME_VARIABLE, ACCOUNT_TYPE_VARIABLE)
        Dim currentFormatsDictionary As Dictionary(Of String, Dictionary(Of String, Object))
        Dim currencies_symbol_dict As Hashtable = GlobalVariables.Currencies.GetCurrenciesDict(ID_VARIABLE, CURRENCY_SYMBOL_VARIABLE)

        ' priority normal => implement format CRUD
        'Select Case reportFormat
        '    Case REPORT_FORMAT_CODE : currentFormatsDictionary = FormatsMapping.GetFormatTable(REPORT_FORMAT_CODE)
        '    Case INPUT_FORMAT_CODE : currentFormatsDictionary = FormatsMapping.GetFormatTable(INPUT_FORMAT_CODE)
        'End Select

        Dim formatCode As String
        For Each row As Excel.Range In inputRange.Rows

            Dim accValue As String = row.Cells(1, 1).value2
            If Not accValue Is Nothing Then
                If AccountsNameFormatDictionary.ContainsKey(accValue) Then
                    formatCode = AccountsNameFormatDictionary(accValue)

                    ' Colors
                    SetRangeColors(row, _
                                   currentFormatsDictionary(formatCode)(FORMAT_TEXT_COLOR_VARIABLE), _
                                   currentFormatsDictionary(formatCode)(FORMAT_BCKGD_VARIABLE))

                    ' Format
                    row.Columns(1).IndentLevel = currentFormatsDictionary(formatCode)(FORMAT_INDENT_VARIABLE)
                    If currentFormatsDictionary(formatCode)(FORMAT_BOLD_VARIABLE) = 1 Then row.Font.Bold = True
                    If currentFormatsDictionary(formatCode)(FORMAT_ITALIC_VARIABLE) = 1 Then row.Font.Italic = True

                    Select Case AccountsNameTypeDict(accValue)
                        Case "MO" : row.Cells.NumberFormat = currencies_symbol_dict(currency) & "#,##0.00;(" & currencies_symbol_dict(currency) & "#,##0.00)"
                        Case "RA" : row.Cells.NumberFormat = "0.00%"        ' put this in a table ?
                        Case "OP" : row.Cells.NumberFormat = "#,##0.00"        ' further evolution set unit ?
                        Case "NU" : row.Cells.NumberFormat = "#,##0"
                        Case "DA" : row.Cells.NumberFormat = "d-mmm-yy" ' d-mmm-yy
                        Case Else : row.Cells.NumberFormat = "#,##0.00"
                    End Select

                    ' Borders
                    If currentFormatsDictionary(formatCode)(FORMAT_BORDER_VARIABLE) = 1 Then
                        'row.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = Color.FromArgb(currentFormatsDictionary(formatCode)(FORMAT_BOTTOM_BORDER_VARIABLE))
                        'row.Borders(Excel.XlBordersIndex.xlEdgeTop).Color = Color.FromArgb(currentFormatsDictionary(formatCode)(FORMAT_UP_BORDER_VARIABLE))
                        If Not IsDBNull(currentFormatsDictionary.Item(formatCode).Item(FORMAT_BOTTOM_BORDER_VARIABLE)) Then
                            row.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = Drawing.Color.FromArgb(currentFormatsDictionary(formatCode)(FORMAT_BOTTOM_BORDER_VARIABLE))
                        End If
                        If Not IsDBNull(currentFormatsDictionary.Item(formatCode).Item(FORMAT_UP_BORDER_VARIABLE)) Then
                            row.Borders(Excel.XlBordersIndex.xlEdgeTop).Color = Drawing.Color.FromArgb(currentFormatsDictionary(formatCode)(FORMAT_UP_BORDER_VARIABLE))
                        End If
                    End If
                End If
            End If
        Next
        ' !! inconsistence dans les formats -> à revoir !!!
        If reportFormat = INPUT_FORMAT_CODE Then ApplyBackGroundColorToInputCells(inputRange, _
                                                                                  currentFormatsDictionary.Item(HV_FORMAT_CODE).Item(FORMAT_TEXT_COLOR_VARIABLE), _
                                                                                  currentFormatsDictionary.Item(HV_FORMAT_CODE).Item(FORMAT_BCKGD_VARIABLE), _
                                                                                  startingDate)
        inputRange.Columns.AutoFit()

    End Sub

    Friend Shared Sub ApplyBackGroundColorToInputCells(ByRef inputRange As Excel.Range,
                                                        ByRef text_color As Object, _
                                                        ByRef bckgd_color As Object, _
                                                        Optional ByRef startingDate As Date = Nothing)

        Dim fType As String
        Dim StartingDateColumn As Int32
        Dim accountsNamesFTypesDictionary As Hashtable = GlobalVariables.Accounts.GetAccountsDictionary(NAME_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)

        '    If IsDate(startingDate) Then StartingDateColumn = GetStaringPeriodColumn(inputRange, startingDate)

        For Each row As Excel.Range In inputRange.Rows

            '      Dim subRange As Excel.Range = row.Offset(0, 1).Resize(1, inputRange.Columns.Count - 1)
            Dim accValue As String = row.Cells(1, 1).value2

            If Not accValue Is Nothing Then
                If accountsNamesFTypesDictionary.ContainsKey(accValue) Then
                    Select Case accountsNamesFTypesDictionary(accValue)

                        Case GlobalEnums.FormulaTypes.HARD_VALUE_INPUT : SetRangeColors(row, text_color, bckgd_color)
                        Case GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT : SetRangeColors(row.Cells(1, 2), text_color, bckgd_color)

                    End Select
                End If
            End If
        Next

    End Sub

    ' Format an excel range as an 
    Public Shared Sub FormatEntitiesReport(ByRef area As Excel.Range)

        area.Font.Color = Color.Black
        Dim subArea As Excel.Range
        For j = 1 To area.Columns.Count - 1
            Dim cell1 As Excel.Range = area.Cells(1, j)
            Dim cell2 As Excel.Range = area.Cells(1, j).Offset(area.Rows.Count - 1, 0)
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

    Protected Shared Function DatesLineIdentification(ByRef inputRange As Excel.Range) As Int32

        For Each cell In inputRange
            If IsDate(cell.value) Then Return cell.row
        Next
        Return -1

    End Function

    Protected Shared Function GetStaringPeriodColumn(ByRef inputRange As Excel.Range, _
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

    Protected Friend Shared Sub SetRangeColors(ByRef r As Excel.Range, _
                                               ByRef text_color As Object, _
                                               ByRef bckdg_color As Object)

        If Not IsDBNull(text_color) Then
            r.Font.Color = Drawing.Color.FromArgb(text_color)
        End If
        If Not IsDBNull(bckdg_color) Then
            r.Interior.Color = Drawing.Color.FromArgb(bckdg_color)
        End If

    End Sub

#End Region


End Class