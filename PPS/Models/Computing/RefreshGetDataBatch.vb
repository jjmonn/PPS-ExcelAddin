' RefreshGetDataBatch.vb
' 
' Aim: Refresh PPSBI formulas and reports on the current worksheet 
'   -> Refreshes Formulas OR Report !      
'
'
' To do: 
'        - 
'        - Careful: as soon as change occurs the GlobalVariables.GenericGlobalSingleEntityComputer must be cleared
'               ' -> accounts mapping change -> new instance
'        - Report refresh -> identification of reports to be implemented
'        - so far refreshes either formulas or report, a further choice could be implemented
'        - !!! if input refresh all ???!!! or message and choice to the user
'        - currency STUB !! for refresh all -> DataSet should look for a currency or ask the user
'
' Known Bugs:
'
'
' Author: Julien Monnereau
' Last modified date: 24/02/2015


Imports Microsoft.Office.Interop
Imports System.Collections.Generic



Friend Class RefreshGetDataBatch


#Region "Instance Variables"

    '   Private WS As Excel.Worksheet
    Private cellsByEntityDictionary As Dictionary(Of String, List(Of Excel.Range))

    Private Const UDF_FORMULA_NAME = UDF_FORMULA_GET_DATA_NAME

#End Region


#Region "Interface"

    Protected Friend Sub RefreshWorksheet()

        Dim FormulasRangesCollection As New Dictionary(Of Excel.Range, String)
        Dim WS As Excel.Worksheet = GlobalVariables.APPS.ActiveSheet
        GlobalVariables.GenericGlobalSingleEntityComputer.ReinitializeGenericDataDLL3Computer()
        GlobalVariables.GenericGlobalAggregationComputer.ReinitializeComputerCache()
        If findGetDataFormulaCells(FormulasRangesCollection, WS) Then
            evaluateFormulas(FormulasRangesCollection)
        Else
            RefreshReport()
        End If
    End Sub

#End Region


#Region "PPSBI Formulas"

    ' Loop throught worksheet and add ranges to formulaDictionary if "PPSBI" formula found
    Protected Friend Shared Function findGetDataFormulaCells(ByRef FormulasRangesCollection As Dictionary(Of Excel.Range, String), _
                                                            ByRef ws As Excel.Worksheet) As Boolean

        Dim c As Excel.Range
        Dim firstAddress As String
        With ws.Cells
            c = .Find(UDF_FORMULA_NAME, ws.Cells(1, 1), , )

            If Not c Is Nothing Then
                firstAddress = c.Address
                Do
                    FormulasRangesCollection.Add(c, c.Formula)
                    c.Value2 = REFRESH_WAITING_TEXT
                    c = .FindNext(c)
                    If IsNothing(c) Then Exit Do
                Loop While Not c Is Nothing And c.Address <> firstAddress
            End If
        End With

        If FormulasRangesCollection.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub evaluateFormulas(ByRef FormulasRangesCollection As Dictionary(Of Excel.Range, String))

        ' dans l'idéal extraire la liste des entities_id to be computed pour avoir le highest hierarchy level to be computed
        For Each cell As Excel.Range In FormulasRangesCollection.Keys
            cell.Formula = FormulasRangesCollection.Item(cell)
        Next

    End Sub

    Protected Friend Shared Sub BreakLinks()

        Dim FormulasRangesCollection As New Dictionary(Of Excel.Range, String)
        Dim WS As Excel.Worksheet = GlobalVariables.APPS.ActiveSheet
        Dim c As Excel.Range
        Dim firstAddress As String

        With WS.Cells
            c = .Find(UDF_FORMULA_NAME, WS.Cells(1, 1), , )

            If Not c Is Nothing Then
                firstAddress = c.Address
                Do
                    Dim value As Object = c.Value2
                    c.Formula = value
                    c = .FindNext(c)
                    If IsNothing(c) Then Exit Do
                Loop While Not c Is Nothing And c.Address <> firstAddress
            End If
        End With


    End Sub

#End Region


#Region "Refresh Report"

    Friend Shared Sub RefreshReport(Optional ByRef currency As String = "", _
                                    Optional ByVal adjustment_id As String = "")

        If currency = "" Then currency = "EUR"
        ' currency stub !-> if currency not provided dataset should identify the currency - if no currency found -> ask user
        Dim DS As New CModelDataSet(GlobalVariables.apps.ActiveSheet)
        DS.SnapshotWS()
        DS.getOrientations()
        If DS.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG Then DS.RefreshAll(adjustment_id)

    End Sub


#End Region


End Class

