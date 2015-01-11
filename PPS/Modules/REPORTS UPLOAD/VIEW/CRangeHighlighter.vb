' SelectionHighlightModule.vb
'
' Draw a rectangle around the input ranges
' Keep track of drawed shapes
' Delete shapes
'
' To do:
'       - 
'
'
' Last modified: 05/08/2014
' Author: Julien Monnereau
'


Imports Microsoft.Office.Interop
Imports Microsoft.Office.Core
Imports System.Collections.Generic
Imports System.Drawing


Friend Class CRangeHighlighter


#Region "Instance Variables"

    ' Objects
    Friend WS As Excel.Worksheet

    ' Variables
    Private rectangleList As New List(Of String)
    Private shapeIndex As Integer = 0

    ' Constants

    Private Const LINE_WEIGHT As Double = 1
    Private SEL_COLOR As Integer = RGB(0, 255, 255)
    Private GREEN_COLOR As Integer = RGB(153, 255, 102)
    Private RED_COLOR As Integer = RGB(255, 153, 153)
    Private Const RECT_NAME_PREFIX As String = "rect"



#End Region


#Region "Initialize"


    Public Sub New(ByRef inputWS As Excel.Worksheet)
        ws = inputWS
    End Sub


#End Region


#Region "Interface"

    ' Highlight Range in green
    Friend Sub FillCellInGreen(ByRef cellAddress As String)
        WS.Range(cellAddress).Interior.Color = GREEN_COLOR
    End Sub

    ' Draw bright blue Border on Range
    Friend Sub FillCellInred(ByRef cellAddress As String)
        WS.Range(cellAddress).Interior.Color = RED_COLOR
    End Sub


    ' Highlight param range Border in Blue
    Friend Sub DrawBlueBorderAroundRange(ByRef inputRange As Excel.Range)

        Dim left As Double = inputRange.Cells(1, 1).left
        Dim top As Double = inputRange.Cells(1, 1).top
        Dim width As Double = inputRange.Width
        Dim height As Double = inputRange.Height

        WS.Shapes.AddShape(MsoAutoShapeType.msoShapeRectangle, left, top, width, height).Name = RECT_NAME_PREFIX & shapeIndex
        With WS.Shapes.Range({RECT_NAME_PREFIX & shapeIndex})
            .Fill.Visible = False
            .Line.Weight = LINE_WEIGHT
            .Line.ForeColor.RGB = SEL_COLOR
            .LockAspectRatio = MsoTriState.msoTrue
            .Shadow.Type = MsoShadowType.msoShadow20
            .Shadow.Visible = MsoTriState.msoTrue
        End With

        rectangleList.Add(RECT_NAME_PREFIX & shapeIndex)
        shapeIndex = shapeIndex + 1

    End Sub

    ' Delete blue borders
    Friend Sub DeleteBlueBorders()

        For Each shapeName As String In rectangleList
            WS.Shapes.Range({shapeName}).Delete()
        Next
        rectangleList.Clear()

    End Sub


#End Region


   
   


 


End Class
