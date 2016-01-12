' CWorksheetFunction.vb
'
' Handles excel worksheets related
'
'
' To do:
'       - put all excel ws related functions here...
'       - > periods format must be function of timeSetUp
'
'
' Known Bugs:
'       -
'       -
'
'
'
' Author: Julien Monnereau
' Last modified: 28/04/2015


Imports Microsoft.Office.Interop
Imports System.Collections.Generic
Imports System.Collections
Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls


Friend Class WorksheetWrittingFunctions


    Private Const EXCEL_SHEET_NAME_MAX_LENGHT = 30


#Region "Input Report Creation"

  
#End Region


#Region "Write on Worksheet"

    ' Write array to the specified destination range
    Public Shared Sub WriteArray(arrayToWrite(,) As Object, Destination As Excel.Range)

        Destination.Resize(UBound(arrayToWrite, 1) + 1, UBound(arrayToWrite, 2) + 1).Value2 = arrayToWrite

    End Sub

    Public Shared Sub WriteAccountsFromTreeView(ByRef TV As Windows.Forms.TreeView, _
                                                ByVal destinationCell As Excel.Range, _
                                                Optional ByRef periodDatesList As Int32() = Nothing)

        Dim IndentLevel As Integer = 0
        For Each Node As TreeNode In TV.Nodes
            IndentLevel = 0
            destinationCell = destinationCell.Offset(1, 0)
            destinationCell.Value = Node.Text

            If Not periodDatesList Is Nothing Then
                Dim i As Int32 = 0
                For Each period As UInt32 In periodDatesList
                    destinationCell.Offset(0, 1 + i).Value = Format(Date.FromOADate(period), "Short Date")
                    'destinationCell.Offset(0, 1 + i).NumberFormat = "yyyy"
                    i = i + 1
                Next
            End If
            destinationCell = destinationCell.Offset(1, 0)

            For Each childNode In Node.Nodes
                WriteAccount(childNode, destinationCell, IndentLevel)
            Next
        Next
        destinationCell.Worksheet.Columns(destinationCell.Column).autofit()

    End Sub

    Friend Shared Sub WriteListOnExcel(ByVal p_range As Excel.Range, ByRef p_list As List(Of String))

        For Each l_value As String In p_list
            p_range = p_range.Offset(1, 0)
            p_range.Value2 = l_value
        Next

    End Sub

    Friend Shared Sub WritePeriodsOnWorksheet(ByVal p_range As Excel.Range, _
                                              ByRef p_periods As Int32(), _
                                              ByRef p_timeConfig As CRUD.TimeConfig)
        Dim i As Int32 = 0
        For Each period As UInt32 In p_periods
            ' need to adjust period format according to local settings ?
            Dim l_cultue = My.Application.Culture
            p_range.Offset(0, 1 + i).Value = Format(Date.FromOADate(period), "MM/dd/yyyy") ' "Short Date")
            i = i + 1
        Next

    End Sub

    Public Shared Sub WriteAccount(ByRef Node As TreeNode, _
                                   ByRef destinationCell As Excel.Range, _
                                   ByRef IndentLevel As Integer)

        With destinationCell
            .IndentLevel = IndentLevel
            .Value = Node.Text
        End With
        destinationCell = destinationCell.Offset(1, 0)

        For Each Child As TreeNode In Node.Nodes
            IndentLevel = IndentLevel + 1
            WriteAccount(Child, destinationCell, IndentLevel)
            IndentLevel = IndentLevel - 1
        Next

    End Sub

    Public Shared Sub WriteAccountsFromTreeView(ByRef p_treeview As vTreeView, _
                                             ByVal p_destinationCell As Microsoft.Office.Interop.Excel.Range, _
                                             Optional ByRef p_periodDatesList As Int32() = Nothing)

        Dim IndentLevel As Integer = 0
        For Each Node As vTreeNode In p_treeview.GetNodes
            IndentLevel = 0
            p_destinationCell = p_destinationCell.Offset(1, 0)
            p_destinationCell.Value = Node.Text

            If Not p_periodDatesList Is Nothing Then
                Dim i As Int32 = 0
                For Each period As UInt32 In p_periodDatesList
                    p_destinationCell.Offset(0, 1 + i).Value = Format(Date.FromOADate(period), "Short Date")
                    'p_destinationCell.Offset(0, 1 + i).NumberFormat = "yyyy"
                    i = i + 1
                Next
            End If
            p_destinationCell = p_destinationCell.Offset(1, 0)

            For Each childNode In Node.Nodes
                WriteAccount(childNode, p_destinationCell, IndentLevel)
            Next
        Next
        p_destinationCell.Worksheet.Columns(p_destinationCell.Column).autofit()

    End Sub

    Public Shared Sub WriteAccount(ByRef p_node As vTreeNode, _
                                  ByRef p_destinationCell As Microsoft.Office.Interop.Excel.Range, _
                                  ByRef p_indentLevel As Integer)

        With p_destinationCell
            .IndentLevel = p_indentLevel
            .Value = p_node.Text
        End With
        p_destinationCell = p_destinationCell.Offset(1, 0)

        For Each Child As vTreeNode In p_node.Nodes
            p_indentLevel = p_indentLevel + 1
            WriteAccount(Child, p_destinationCell, p_indentLevel)
            p_indentLevel = p_indentLevel - 1
        Next

    End Sub


#End Region


#Region "Worksheets Add/ Delete"

    Friend Shared Function CreateReceptionWS(ByRef wsName As String, _
                                            ByRef header_names_array As String(), _
                                            ByRef header_values_array As String()) As Excel.Range

        On Error GoTo ReturnError
        Dim WS As Excel.Worksheet = CType(GlobalVariables.APPS.Worksheets.Add(), Excel.Worksheet)
        If Len(wsName) < EXCEL_SHEET_NAME_MAX_LENGHT _
        AndAlso CheckIfWorkbookContainsWorksheetName(GlobalVariables.APPS.ActiveWorkbook, wsName) = False Then
            WS.Name = wsName
        Else
            wsName = Left(wsName, EXCEL_SHEET_NAME_MAX_LENGHT)
            If CheckIfWorkbookContainsWorksheetName(GlobalVariables.APPS.ActiveWorkbook, wsName) = False Then
                WS.Name = wsName
            End If
        End If

        GlobalVariables.APPS.ActiveWindow.DisplayGridlines = False
        Dim destination As Excel.Range = WS.Cells(1, 1)

        Dim i As Int32 = 0
        For Each item In header_names_array
            destination.Offset(i + 1, 0).Value = header_names_array(i)
            destination.Offset(i + 1, 1).Value = header_values_array(i)
            i = i + 1
        Next
        destination.Offset(i + 1, 0).Value = Local.GetValue("upload.report_as_of") & CStr(Format(Today, "D"))

        destination = destination.Offset(i + 2, 0)
        Return destination

ReturnError:
        Return Nothing

    End Function

    Friend Shared Function CheckIfWorkbookContainsWorksheetName(ByRef WB As Excel.Workbook, WSName As String) As Boolean

        For Each WS As Excel.Worksheet In WB.Worksheets
            If WS.Name = WSName Then
                Return True
            End If
        Next
        Return False

    End Function


#End Region



End Class
