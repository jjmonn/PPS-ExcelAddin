' ExcelEntitiesReportFormatting.vb
'
' Manages format for the entities TGV Drop in excel
'
'
' To do: 
'       - 
'
'
'
' Known bugs:
'       - 
'
'
' Auhtor: Julien Monnereau
' Last modified: 20/07/2014


Imports Microsoft.Office.Interop
Imports System.Drawing



Public Class ExcelEntitiesReportFormatting

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




End Class
