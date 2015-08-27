' SelectionHighlightModule.vb
'
' Draw a rectangle around the input ranges
' Keep track of drawed shapes
' Delete shapes
'
' To do:
'       - priority high: FPI -> only first period highlighted !! 
'
'
' Last modified: 13/07/2015
' Author: Julien Monnereau
'


Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Collections


Friend Class RangeHighlighter


#Region "Instance Variables"

    ' Objects
    Friend WS As Excel.Worksheet

    ' Variables
    Private original_cells_format As New Dictionary(Of String, Hashtable)

    ' Constants
    Private Const LINE_WEIGHT As Double = 1
    Private SEL_COLOR As Integer = RGB(0, 255, 255)
    Private INPUT_BLUE_BKG_COLOR As Int32 = RGB(41, 182, 216)
    Private INPUT_BLUE_TXT_COLOR As Int32 = RGB(255, 255, 255) 'RGB(30, 131, 156)
    Private OUTPUT_BKG_COLOR As Int32 = RGB(255, 255, 255)
    Private OUTPUT_TXT_COLOR As Int32 = RGB(30, 131, 156)
    Private OUTPUT_LINE_COLOR As Int32 = RGB(197, 217, 241)
    Private GREEN_BKG_COLOR As Int32 = RGB(102, 199, 150)
    Private GREEN_TXT_COLOR As Int32 = RGB(255, 255, 255) 'RGB(74, 144, 108)
    Private RED_BKG_COLOR As Int32 = RGB(223, 106, 120)
    Private RED_TXT_COLOR As Int32 = RGB(255, 255, 255) 'RGB(161, 76, 87)
    Private Const RECT_NAME_PREFIX As String = "rect"

    Private Const BKG_COLOR As String = "cell_bkg_color"
    Private Const TXT_COLOR As String = "cell_txt_color"
    Private Const BOTTOM_EDGE_LINE_COLOR As String = "bottom_edge_line_color"
    Private Const TOP_EDGE_LINE_COLOR As String = "top_edge_line_color"
    Private Const BOTTOM_EDGE_LINE_STYLE As String = "bottom_edge_line_style"
    Private Const TOP_EDGE_LINE_STYLE As String = "top_edge_line_style"
    Private Const BOTTOM_EDGE_TINT_SHADE As String = "bottom_edge_tint_shade"
    Private Const TOP_EDGE_TINT_SHADE As String = "top_edge_tint_shade"
    Private Const BOTTOM_EDGE_WEIGHT As String = "bottom_edge_weight"
    Private Const TOP_EDGE_WEIGHT As String = "top_edge_weight"

#End Region


#Region "Initialize"


    Public Sub New(ByRef inputWS As Excel.Worksheet)
        WS = inputWS
    End Sub


#End Region


#Region "Interface"

    Friend Sub ColorRangeGreen(ByRef cellAddress As String)

        If original_cells_format.ContainsKey(cellAddress) = False Then SaveCellFormat(WS.Range(cellAddress))
        WS.Range(cellAddress).Interior.Color = GREEN_BKG_COLOR
        WS.Range(cellAddress).Font.Color = GREEN_TXT_COLOR

    End Sub

    Friend Sub ColorRangeRed(ByRef cellAddress As String)

        If original_cells_format.ContainsKey(cellAddress) = False Then SaveCellFormat(WS.Range(cellAddress))
        WS.Range(cellAddress).Interior.Color = RED_BKG_COLOR
        WS.Range(cellAddress).Font.Color = RED_TXT_COLOR

    End Sub

    Friend Sub ColorRangeInputBlue(ByRef cellAddress As String)

        If original_cells_format.ContainsKey(cellAddress) = False Then SaveCellFormat(WS.Range(cellAddress))
        WS.Range(cellAddress).Interior.Color = INPUT_BLUE_BKG_COLOR
        WS.Range(cellAddress).Font.Color = INPUT_BLUE_TXT_COLOR

    End Sub

    Friend Sub ColorOutputRange(ByRef output_range)

        For Each c As Excel.Range In output_range
            If original_cells_format.ContainsKey(c.Address) = False Then SaveCellFormat(c)
            c.Interior.Color = OUTPUT_BKG_COLOR
            c.Font.Color = OUTPUT_TXT_COLOR
            c.Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlDash
            c.Borders(Excel.XlBordersIndex.xlEdgeTop).Color = System.Drawing.Color.LightBlue
            c.Borders(Excel.XlBordersIndex.xlEdgeTop).TintAndShade = 0.799981688894314
            c.Borders(Excel.XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlHairline
            c.Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlDash
            c.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = System.Drawing.Color.LightBlue
            c.Borders(Excel.XlBordersIndex.xlEdgeBottom).TintAndShade = 0.799981688894314
            c.Borders(Excel.XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlHairline
        Next

    End Sub

    Friend Sub HighlightInputRange(ByRef input_range As Excel.Range)

        For Each cell As Excel.Range In input_range
            ColorRangeInputBlue(cell.Address)
        Next

    End Sub

    Friend Sub RevertToOriginalColors()

        GlobalVariables.APPS.ScreenUpdating = False
        For Each cell_address As String In original_cells_format.Keys
            Dim c As Excel.Range = WS.Range(cell_address)
            c.Interior.Color = original_cells_format(cell_address)(BKG_COLOR)
            c.Font.Color = original_cells_format(cell_address)(TXT_COLOR)

            ' Top border
            If (original_cells_format(cell_address)(TOP_EDGE_LINE_COLOR) <> 0) Then
                c.Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = original_cells_format(cell_address)(TOP_EDGE_LINE_STYLE)
                c.Borders(Excel.XlBordersIndex.xlEdgeTop).Color = original_cells_format(cell_address)(TOP_EDGE_LINE_COLOR)
                c.Borders(Excel.XlBordersIndex.xlEdgeTop).TintAndShade = original_cells_format(cell_address)(TOP_EDGE_TINT_SHADE)
                c.Borders(Excel.XlBordersIndex.xlEdgeTop).Weight = original_cells_format(cell_address)(TOP_EDGE_WEIGHT)
            Else
                c.Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlLineStyleNone
            End If

            ' Bottom border
            If (original_cells_format(cell_address)(BOTTOM_EDGE_LINE_COLOR) <> 0) Then
                c.Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = original_cells_format(cell_address)(BOTTOM_EDGE_LINE_STYLE)
                c.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = original_cells_format(cell_address)(BOTTOM_EDGE_LINE_COLOR)
                c.Borders(Excel.XlBordersIndex.xlEdgeBottom).TintAndShade = original_cells_format(cell_address)(BOTTOM_EDGE_TINT_SHADE)
                c.Borders(Excel.XlBordersIndex.xlEdgeBottom).Weight = original_cells_format(cell_address)(BOTTOM_EDGE_WEIGHT)
            Else
                c.Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = original_cells_format(cell_address)(BOTTOM_EDGE_LINE_STYLE)
            End If
        Next
        GlobalVariables.APPS.ScreenUpdating = True
        original_cells_format.Clear()

    End Sub

    Friend Function FormattedCellsDictionnarySize() As UInt16

        Return original_cells_format.Count

    End Function

#End Region


#Region "Utilities"

    Private Sub SaveCellFormat(ByRef c As Excel.Range, Optional log As Boolean = False)

        Dim ht As New Hashtable
        ht(BKG_COLOR) = c.Interior.Color
        ht(TXT_COLOR) = c.Font.Color

        ht(TOP_EDGE_LINE_STYLE) = c.Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle
        ht(TOP_EDGE_LINE_COLOR) = c.Borders(Excel.XlBordersIndex.xlEdgeTop).Color
        ht(TOP_EDGE_TINT_SHADE) = c.Borders(Excel.XlBordersIndex.xlEdgeTop).TintAndShade
        ht(TOP_EDGE_WEIGHT) = c.Borders(Excel.XlBordersIndex.xlEdgeTop).Weight
        ht(BOTTOM_EDGE_LINE_STYLE) = c.Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle
        ht(BOTTOM_EDGE_LINE_COLOR) = c.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color
        ht(BOTTOM_EDGE_TINT_SHADE) = c.Borders(Excel.XlBordersIndex.xlEdgeBottom).TintAndShade
        ht(BOTTOM_EDGE_WEIGHT) = c.Borders(Excel.XlBordersIndex.xlEdgeBottom).Weight

        original_cells_format.Add(c.Address, ht)

    End Sub

#End Region


End Class
