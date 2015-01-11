Imports Microsoft.Office.Interop

Module Write_Functions


    Public Sub WriteAccountsFromTreeView(TV As Windows.Forms.TreeView, FormulaHT As Collections.Hashtable, _
                                          ActiveCell As Excel.Range)

        '---------------------------------------------------------------------------------------
        ' Loop through root nodes and launch write to Worksheet
        '---------------------------------------------------------------------------------------
        Dim Line As Integer = 1
        Dim IndentLevel As Integer = 0

        For Each Node As Windows.Forms.TreeNode In TV.Nodes
            IndentLevel = 0
            WriteAccount(Node, FormulaHT, ActiveCell, Line, IndentLevel)
        Next

        ActiveCell.Worksheet.Columns(ActiveCell.Column).autofit()
        ' Period writing ?

    End Sub

    Private Sub WriteAccount(Node As Windows.Forms.TreeNode, FormulaHT As Collections.Hashtable, _
                              ByRef RNG As Excel.Range, ByRef Line As Integer, ByRef IndentLevel As Integer)

        '---------------------------------------------------------------------------------------
        ' Actual Range writing
        '---------------------------------------------------------------------------------------
        Line = Line + 1

        With RNG.Cells(Line, 1)
            .indentlevel = IndentLevel
            .value2 = Node.Text
        End With

        Select Case FormulaHT(Node.Name)(0)                         ' Defining row Format

            Case FORMULA_TYPE_TITLE
                With RNG.Rows(Line)
                    .Font.Bold = True
                    .Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
                    .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
                End With
            Case FORMULA_TYPE_SUM_OF_CHILDREN
                'With RNG.Rows(Line)
                '    .Font.Bold = True
                'End With
            Case FORMULA_TYPE_HARD_VALUE
                With RNG.Rows(Line)
                    .font.italic = True
                End With
            Case FORMULA_TYPE_FORMULA
                With RNG.Rows(Line)
                    .font.bold = True
                    .Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
                    .Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
                End With
            Case FORMULA_TYPE_BALANCE_SHEET
                ' 
            Case FORMULA_TYPE_OTHER_LINE
                '
        End Select

        If Node.Nodes.Count > 0 Then
            IndentLevel = IndentLevel + 1
            For Each Child As Windows.Forms.TreeNode In Node.Nodes
                WriteAccount(Child, FormulaHT, RNG, Line, IndentLevel)
            Next
            IndentLevel = IndentLevel - 1
        End If

    End Sub



End Module
