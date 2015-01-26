' CRefreshGetDataBatch.vb
' 
' Aim: Refresh PPSBI formulas and reports on the current worksheet 
'   -> Refreshes Formulas OR Report !      
'
'
' To do: 
'        - 
'        - Careful: as soon as change occurs the GENERICDCGLobalInstance must be cleared
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
' Last modified date: 19/01/2015


Imports Microsoft.Office.Interop
Imports System.Collections.Generic



Friend Class CRefreshGetDataBatch


#Region "Instance Variables"

    Private WS As Excel.Worksheet
    Private FormulasRangesCollection As New Dictionary(Of Excel.Range, String)
    Private cellsByEntityDictionary As Dictionary(Of String, List(Of Excel.Range))

    Private Const UDF_FORMULA_NAME = UDF_FORMULA_GET_DATA_NAME

#End Region


#Region "Interface"

    Public Sub RefreshWorksheet()

        GENERICDCGLobalInstance.ReinitializeGenericDataDLL3Computer()
        WS = APPS.ActiveSheet
        If findGetDataFormulaCells() Then
            evaluateFormulas()
        Else
            RefreshReport()
        End If
    End Sub

#End Region


#Region "Refresh PPSBI Formulas"

    ' Loop throught worksheet and add ranges to formulaDictionary if "PPSBI" formula found
    Private Function findGetDataFormulaCells() As Boolean

        FormulasRangesCollection.Clear()
        Dim c As Excel.Range
        Dim firstAddress As String
        With WS.Cells
            c = .Find(UDF_FORMULA_NAME, WS.Cells(1, 1), , )

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

    ' Loop through formulas and evaluate
    Private Sub evaluateFormulas()

        For Each cell As Excel.Range In FormulasRangesCollection.Keys
            cell.Formula = FormulasRangesCollection.Item(cell)
        Next

    End Sub


#End Region


#Region "Refresh Report"

    Friend Shared Sub RefreshReport(Optional ByRef currency As String = "", _
                                    Optional ByVal adjustment_id As String = "")

        If currency = "" Then currency = "EUR"
        ' currency stub !-> if currency not provided dataset should identify the currency - if no currency found -> ask user
        Dim DS As New CModelDataSet(APPS.ActiveSheet)
        DS.SnapshotWS()
        DS.getOrientations()
        If DS.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG Then DS.RefreshAll(adjustment_id)

    End Sub


#End Region


End Class

