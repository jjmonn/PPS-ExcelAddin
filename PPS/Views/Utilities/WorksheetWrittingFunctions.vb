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


Friend Class WorksheetWrittingFunctions


    Private Const EXCEL_SHEET_NAME_MAX_LENGHT = 30


#Region "Input Report Creation"

    Protected Friend Shared Sub InsertInputReportOnWS(ByVal destinationcell As Excel.Range, _
                                                      ByRef periodList As List(Of Date), _
                                                      ByRef timeConfig As String)

        Dim accountsTV As New TreeView
        Dim WS As Excel.Worksheet = destinationcell.Worksheet
        GlobalVariables.Accounts.LoadAccountsTV(accountsTV)
        WriteAccountsFromTreeView(accountsTV, destinationcell, periodList)
        accountsTV.Dispose()

    End Sub

#End Region


#Region "Write on Worksheet"

    ' Write array to the specified destination range
    Public Shared Sub WriteArray(arrayToWrite(,) As Object, Destination As Excel.Range)

        Destination.Resize(UBound(arrayToWrite, 1) + 1, UBound(arrayToWrite, 2) + 1).Value = arrayToWrite

    End Sub

    Public Shared Sub WriteAccountsFromTreeView(ByRef TV As Windows.Forms.TreeView, _
                                                ByVal destinationCell As Excel.Range, _
                                                Optional ByRef periodDatesList As List(Of Date) = Nothing)

        Dim IndentLevel As Integer = 0
        For Each Node As TreeNode In TV.Nodes
            IndentLevel = 0
            destinationCell = destinationCell.Offset(1, 0)
            destinationCell.Value2 = Node.Text

            If Not periodDatesList Is Nothing Then
                Dim i As Int32 = 0
                For Each period As Date In periodDatesList
                    destinationCell.Offset(0, 1 + i).Value2 = Format(period, "Short Date")
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

    ' param+ = timeSetup for periods formatting
    Public Shared Sub WriteAccount(ByRef Node As TreeNode, _
                                   ByRef destinationCell As Excel.Range, _
                                   ByRef IndentLevel As Integer)

        With destinationCell
            .IndentLevel = IndentLevel
            .Value2 = Node.Text
        End With
        destinationCell = destinationCell.Offset(1, 0)

        For Each Child As TreeNode In Node.Nodes
            IndentLevel = IndentLevel + 1
            WriteAccount(Child, destinationCell, IndentLevel)
            IndentLevel = IndentLevel - 1
        Next

    End Sub

#End Region


#Region "Worksheets Add/ Delete"

    Friend Shared Function CreateReceptionWS(ByRef wsName As String, _
                                            ByRef header_names_array As String(), _
                                            ByRef header_values_array As String()) As Excel.Range

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
        destination.Offset(i + 1, 0).Value = "Date"
        destination.Offset(i + 1, 1).Value = Today

        destination = destination.Offset(i + 2, 0)
        Return destination

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
