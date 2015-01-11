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
' Last modified: 20/10/2014



Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports System.Collections
Imports System.Drawing



Public Class CExcelFormatting


#Region "Instance Variables"

    ' Variables
    Friend InputFormatsDictionary As Dictionary(Of String, Dictionary(Of String, Object))
    Private ReportFormatsDictionary As Dictionary(Of String, Dictionary(Of String, Object))
    Friend AccountsNameFormatDictionary As Hashtable
    Private accountsNamesFTypesDictionary As Hashtable


#End Region


#Region "Initialize"

    Public Sub New()

        InputFormatsDictionary = FormatsMapping.GetFormatTable(INPUT_FORMAT_CODE)
        ReportFormatsDictionary = FormatsMapping.GetFormatTable(REPORT_FORMAT_CODE)
        AccountsNameFormatDictionary = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_FORMAT_VARIABLE)
        accountsNamesFTypesDictionary = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)
       
    End Sub

#End Region

#Region "Interface"

    ' Identify the current range and 
    ' Param: REPORT_FORMAT_CODE or INPUT_FORMAT_CODE
    Friend Sub FormatExcelRange(ByRef WS As Excel.Worksheet, _
                                ByRef format As String, _
                                Optional ByRef startingDate As Date = Nothing)

        Dim tmpRange As Excel.Range
        tmpRange = WS.Range(WS.Cells(1, 1), Utilities_Functions.GetRealLastCell(WS))
        Dim DGVUTIL As New DataGridViewsUtil
        FormatExcelRangeAs(tmpRange, format, startingDate)

    End Sub

#End Region


#Region "Formatting"

    Friend Sub FormatExcelRangeAs(ByRef inputRange As Excel.Range, _
                                  ByRef reportFormat As String, _
                                  Optional ByRef startingDate As Date = Nothing)

        Dim currentFormatsDictionary As Dictionary(Of String, Dictionary(Of String, Object))
        Select Case reportFormat
            Case REPORT_FORMAT_CODE : currentFormatsDictionary = ReportFormatsDictionary
            Case INPUT_FORMAT_CODE : currentFormatsDictionary = InputFormatsDictionary
        End Select

        Dim formatCode As String
        For Each row As Excel.Range In inputRange.Rows

            Dim accValue As String = row.Cells(1, 1).value2
            If Not accValue Is Nothing Then
                If AccountsNameFormatDictionary.ContainsKey(accValue) Then
                    formatCode = AccountsNameFormatDictionary.Item(accValue)
                    With row

                        .Font.Color = Color.FromArgb(currentFormatsDictionary.Item(formatCode).Item(FORMAT_TEXT_COLOR_VARIABLE))
                        .Columns(1).IndentLevel = currentFormatsDictionary.Item(formatCode).Item(FORMAT_INDENT_VARIABLE)
                        .Interior.Color = Color.FromArgb(currentFormatsDictionary.Item(formatCode).Item(FORMAT_BCKGD_VARIABLE))
                        If currentFormatsDictionary.Item(formatCode).Item(FORMAT_BOLD_VARIABLE) = 1 Then .Font.Bold = True
                        If currentFormatsDictionary.Item(formatCode).Item(FORMAT_ITALIC_VARIABLE) = 1 Then .Font.Italic = True
                        If currentFormatsDictionary.Item(formatCode).Item(FORMAT_BORDER_VARIABLE) = 1 Then
                            .Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = Color.FromArgb(currentFormatsDictionary.Item(formatCode).Item(FORMAT_BOTTOM_BORDER_VARIABLE))
                            .Borders(Excel.XlBordersIndex.xlEdgeTop).Color = Color.FromArgb(currentFormatsDictionary.Item(formatCode).Item(FORMAT_UP_BORDER_VARIABLE))
                        End If
                        If currentFormatsDictionary.Item(formatCode).Item(FORMAT_BOTTOM_BORDER_VARIABLE) <> "" Then
                            .Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = Drawing.Color.FromArgb(currentFormatsDictionary.Item(formatCode).Item(FORMAT_BOTTOM_BORDER_VARIABLE))
                        End If
                        If currentFormatsDictionary.Item(formatCode).Item(FORMAT_UP_BORDER_VARIABLE) <> "" Then
                            .Borders(Excel.XlBordersIndex.xlEdgeTop).Color = Drawing.Color.FromArgb(currentFormatsDictionary.Item(formatCode).Item(FORMAT_UP_BORDER_VARIABLE))
                        End If

                    End With
                End If
            End If
        Next

        If reportFormat = INPUT_FORMAT_CODE Then ApplyBackGroundColorToInputCells(inputRange, startingDate)

        'inputRange.Columns(1).AutoFit()
        inputRange.NumberFormat = "#,##0"
        inputRange.Rows(1).NumberFormat = "Short date"

    End Sub

    Friend Sub ApplyBackGroundColorToInputCells(ByRef inputRange As Excel.Range,
                                                Optional ByRef startingDate As Date = Nothing)

        Dim fType As String
        Dim StartingDateColumn As Int32
        If IsDate(startingDate) Then StartingDateColumn = GetStaringPeriodColumn(inputRange, startingDate)

        For Each row As Excel.Range In inputRange.Rows

            Dim subRange As Excel.Range = row.Offset(0, 1).Resize(row.Rows.Count, row.Columns.Count - 1)
            Dim accValue As String = row.Cells(1, 1).value2

            If Not accValue Is Nothing Then
                If accountsNamesFTypesDictionary.ContainsKey(accValue) Then
                    fType = accountsNamesFTypesDictionary(accValue)
                    If fType = INPUT_ACCOUNT_FORMULA_TYPE Then
                        subRange.Interior.Color = Color.FromArgb(INPUT_COLOR)

                    ElseIf IsDate(startingDate) _
                    AndAlso fType = BALANCE_SHEET_ACCOUNT_FORMULA_TYPE Then
                        subRange.Cells(1, StartingDateColumn - 1).Interior.Color = Color.FromArgb(INPUT_COLOR)
                        End If
                End If
            End If
        Next

    End Sub

#End Region


#Region "Utilities"

    Private Function DatesLineIdentification(ByRef inputRange As Excel.Range) As Int32

        For Each cell In inputRange
            If IsDate(cell.value) Then Return cell.row
        Next
        Return -1

    End Function

    Private Function GetStaringPeriodColumn(ByRef inputRange As Excel.Range, _
                                            ByRef startingDate As Date) As Int32

        Dim datesLine As Int32 = DatesLineIdentification(inputRange)
        On Error Resume Next
        If datesLine <> -1 Then
            For i = 1 To inputRange.Columns.Count
                Dim dateIfDateType = Nothing
                dateIfDateType = CDate(inputRange.Cells(datesLine, i).value)
                If Not dateIfDateType Is Nothing Then If dateIfDateType = startingDate Then Return i
             Next
        End If
        Return -1
        
    End Function
   

#End Region


End Class
