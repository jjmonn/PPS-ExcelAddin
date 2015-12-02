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



Friend Class WorksheetRefreshController


#Region "Instance Variables"

    '   Private WS As Excel.Worksheet
    Private cellsByEntityDictionary As Dictionary(Of String, List(Of Excel.Range))
    Private Const UDF_FORMULA_NAME = UDF_FORMULA_GET_DATA_NAME

#End Region


#Region "Interface"

    Friend Sub RefreshWorksheet(ByRef p_addin As AddinModule, _
                                Optional ByRef rng As Excel.Range = Nothing)

        GlobalVariables.g_mustResetCache = True
        Dim l_formulasRangesCollection As New SafeDictionary(Of Excel.Range, String)

        If rng Is Nothing Then
            Dim ws As Excel.Worksheet = GlobalVariables.APPS.ActiveSheet
            Dim lastCell As Excel.Range = GeneralUtilities.GetRealLastCell(ws)

            If (lastCell Is Nothing) Then
                MsgBox("Nothing to refresh")
                Exit Sub
            Else
                rng = ws.Range(ws.Cells(1, 1), lastCell)
            End If
        End If

        If p_addin.IsCurrentGeneralSubmissionController = True Then
            Dim l_refreshFromDataBaseFlag As Boolean
            Dim confirm1 = Windows.Forms.MessageBox.Show("Do you want to download the inputs from the cloud?", "", _
                                                         Windows.Forms.MessageBoxButtons.YesNoCancel, _
                                                         Windows.Forms.MessageBoxIcon.Question)
            If confirm1 = Windows.Forms.DialogResult.Yes Then
                l_refreshFromDataBaseFlag = True
            End If
            If p_addin.RefreshGeneralSubmissionControllerSnapshot(l_refreshFromDataBaseFlag) = False Then
                GoTo RefreshFormulasOnWorksheet
            End If
        Else
            GoTo RefreshFormulasOnWorksheet
        End If
        Exit Sub

RefreshFormulasOnWorksheet:
        If findGetDataFormulaCells(l_formulasRangesCollection, rng) Then
            EvaluateFormulas(l_formulasRangesCollection)
        End If



    End Sub

#End Region


#Region "PPSBI Formulas"

    ' Loop throught worksheet and add ranges to formulaDictionary if "PPSBI" formula found
    Friend Shared Function findGetDataFormulaCells(ByRef FormulasRangesCollection As Dictionary(Of Excel.Range, String), _
                                                             ByRef rng As Excel.Range) As Boolean

        Dim c As Excel.Range
        Dim firstAddress As String

        If rng.Count = 1 Then
            FormulasRangesCollection.Add(rng, rng.Formula)
            rng.Value2 = REFRESH_WAITING_TEXT
        Else
            With rng.Cells
                c = .Find(UDF_FORMULA_NAME, rng.Cells(1, 1), , , Excel.XlSearchOrder.xlByRows, Excel.XlSearchDirection.xlNext)

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
        End If

        If FormulasRangesCollection.Count > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub EvaluateFormulas(ByRef FormulasRangesCollection As Dictionary(Of Excel.Range, String))

        ' dans l'idéal extraire la liste des entities_id to be computed pour avoir le highest hierarchy level to be computed
        For Each cell As Excel.Range In FormulasRangesCollection.Keys
            GlobalVariables.APPS.ActiveSheet.range(cell.Address).formula = FormulasRangesCollection.Item(cell)
            ' cell.Formula = FormulasRangesCollection.Item(cell)
        Next

    End Sub

    Friend Shared Sub BreakLinks()

        Dim FormulasRangesCollection As New SafeDictionary(Of Excel.Range, String)
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

    ' My.Settings.mainCurrency
    Friend Shared Sub RefreshReport(Optional ByRef currency As String = "", _
                                    Optional ByVal adjustment_id As String = "aaa")

        ' stub !!! priority high
        ' currency stub !-> if currency not provided dataset should identify the currency - if no currency found -> ask user

        ' decision to be made here !!!


        'Dim DS As New ModelDataSet(GlobalVariables.APPS.ActiveSheet)
        'DS.SnapshotWS()
        'DS.getOrientations()
        ' If DS.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG Then DS.RefreshAll(adjustment_id)

    End Sub


#End Region


End Class

