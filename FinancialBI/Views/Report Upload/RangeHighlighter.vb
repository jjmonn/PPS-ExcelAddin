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
' Last modified: 17/09/2015
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
    Private original_cells_format As New SafeDictionary(Of String, Hashtable)
    Friend inputCellsAddresses As New List(Of String)
    Friend outputCellsAddresses As New List(Of String)

    ' Constants
    Private SEL_COLOR As Integer = RGB(0, 255, 255)
    Private GREEN_BKG_COLOR As Int32 = RGB(102, 199, 150)
    Private GREEN_TXT_COLOR As Int32 = RGB(255, 255, 255) 'RGB(74, 144, 108)
    Private RED_BKG_COLOR As Int32 = RGB(223, 106, 120)
    Private RED_TXT_COLOR As Int32 = RGB(255, 255, 255) 'RGB(161, 76, 87)
    Private Const RECT_NAME_PREFIX As String = "rect"

    Private Const BKG_COLOR As String = "cell_bkg_color"
    Private Const TXT_COLOR As String = "cell_txt_color"
 
#End Region


#Region "Initialize"

    Public Sub New(ByRef inputWS As Excel.Worksheet)
        WS = inputWS
    End Sub

#End Region


#Region "Interface"

    Friend Sub ColorRangeGreen(ByRef cellAddress As String)

        '     If original_cells_format.ContainsKey(cellAddress) = False Then SaveCellFormat(WS.Range(cellAddress))
        WS.Range(cellAddress).Interior.Color = GREEN_BKG_COLOR
        WS.Range(cellAddress).Font.Color = GREEN_TXT_COLOR

    End Sub

    Friend Sub ColorRangeRed(ByRef cellAddress As String)

        '    If original_cells_format.ContainsKey(cellAddress) = False Then SaveCellFormat(WS.Range(cellAddress))
        WS.Range(cellAddress).Interior.Color = RED_BKG_COLOR
        WS.Range(cellAddress).Font.Color = RED_TXT_COLOR

    End Sub

    Friend Sub ColorRangeInputBlue(ByRef cellAddress As String)

        If original_cells_format.ContainsKey(cellAddress) = False Then SaveCellFormat(WS.Range(cellAddress))
        WS.Range(cellAddress).Interior.Color = My.Settings.snapshotInputsBackColor
        WS.Range(cellAddress).Font.Color = My.Settings.snapshotInputsTextColor
        If inputCellsAddresses.Contains(cellAddress) = False Then inputCellsAddresses.Add(cellAddress)

    End Sub

    Friend Sub ColorOutputRange(ByRef output_range)

        For Each c As Excel.Range In output_range
            If original_cells_format.ContainsKey(c.Address) = False Then SaveCellFormat(c)
            c.Interior.Color = My.Settings.snapshotOutputsBackColor
            c.Font.Color = My.Settings.snapshotOutputsTextColor
            'c.Borders(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlDash
            'c.Borders(Excel.XlBordersIndex.xlEdgeTop).Color = System.Drawing.Color.LightBlue
            'c.Borders(Excel.XlBordersIndex.xlEdgeTop).TintAndShade = 0.799981688894314
            'c.Borders(Excel.XlBordersIndex.xlEdgeTop).Weight = Excel.XlBorderWeight.xlHairline
            'c.Borders(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlDash
            'c.Borders(Excel.XlBordersIndex.xlEdgeBottom).Color = System.Drawing.Color.LightBlue
            'c.Borders(Excel.XlBordersIndex.xlEdgeBottom).TintAndShade = 0.799981688894314
            'c.Borders(Excel.XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlHairline

            If outputCellsAddresses.Contains(c.Address) = False Then outputCellsAddresses.Add(c.Address)
        Next

    End Sub

    Friend Sub HighlightInputRange(ByRef input_range As Excel.Range)

        For Each cell As Excel.Range In input_range
            ColorRangeInputBlue(cell.Address)
        Next

    End Sub

    Friend Sub RevertToOriginalColors()

        On Error GoTo errorHandler
        GlobalVariables.APPS.ScreenUpdating = False
        For Each cell_address As String In original_cells_format.Keys
            Dim c As Excel.Range = WS.Range(cell_address)
            c.Interior.Color = original_cells_format(cell_address)(BKG_COLOR)
            c.Font.Color = original_cells_format(cell_address)(TXT_COLOR)
        Next
        GlobalVariables.APPS.ScreenUpdating = True
        original_cells_format.Clear()

errorHandler:
        On Error Resume Next
        GlobalVariables.APPS.ScreenUpdating = True
        original_cells_format.Clear()
        Exit Sub

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
        original_cells_format.Add(c.Address, ht)

    End Sub

#End Region


End Class
