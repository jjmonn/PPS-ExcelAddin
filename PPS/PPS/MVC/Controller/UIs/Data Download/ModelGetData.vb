Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices


Public Class ModelGetData

    Private DB As ModelDatabase
    Public MAPP As ModelMapping
    Public DataSource As Data.DataTable
    Private periodList() As Integer


    Public Sub New()

        MAPP = New ModelMapping
        buildPeriodList()
        DB = New ModelDatabase
        DB.periodList = periodList

    End Sub

    Private Sub buildPeriodList()

        '--------------------------------------------------------------------------
        ' Build an array holding the list of periods from opening to closing
        '--------------------------------------------------------------------------
        Dim startPeriod As Integer = MAPP.getOpeningPeriod
        Dim endPeriod As Integer = MAPP.getLastPeriod

        ReDim periodList(endPeriod - startPeriod)
        For i = 0 To endPeriod - startPeriod
            periodList(i) = startPeriod + i
        Next

    End Sub

    Public Sub computeMultipleAsset(affiliatesSelection() As String)

        Dim dataView As New DataViewUI

        If IsNothing(GLOBALMC) Then
            GLOBALMC = New ModelComputer

        End If
                If GLOBALMC.WS.Range(MODEL_BUILT_CELL_FLAG).Value2 = MODEL_BUILT_FLAG_TRUE Then
        Else
            GLOBALMC = New ModelComputer
            ModelingApp = New Excel.Application
        End If

        ' For each asset (depending on the download selection way)
        DB.CreateAggregDataHT(affiliatesSelection)
        For Each account As String In DB.dataHT.Keys
            For Each period As Integer In DB.periodList
                GLOBALMC.ModelDictionary.Item(account).Item(period).Value = DB.dataHT.Item(account).Item(period)
            Next
        Next
        GLOBALMC.modelUpdate()

        ' DataSource filling
        buildDataSourceHeaders()
        'Dim valuesArray(,) As Object = GLOBALMC.WS.Range(GLOBALMC.startCell, GLOBALMC.endCell).Value

        'Dim test() As Object = apps.WorksheetFunction.Index(valuesArray, 1, 0)
        For i = GLOBALMC.startCell.Row To GLOBALMC.endCell.Row
            Dim temparray(GLOBALMC.endCell.Column - 1) As Object
            For j = GLOBALMC.startCell.Column To GLOBALMC.endCell.Column
                temparray(j - 1) = GLOBALMC.WS.Cells(i, j).value
            Next j
            DataSource.LoadDataRow(temparray, True)
        Next i
        ' Next Asset


    End Sub

    Private Sub buildDataSourceHeaders()

        '------------------------------------------------------------------------
        ' Get the datatable headers ("Accounts" and periods)
        '------------------------------------------------------------------------
        DataSource = New Data.DataTable
        DataSource.Columns.Add("Account", GetType(String))
        For Each period As Integer In periodList
            DataSource.Columns.Add(period, GetType(Double))
        Next

    End Sub



    Public Function PPSKeys(entityKey As String, accountKey As String, period As Integer, currency As String) As Double

        '----------------------------------------------------------------------------------------------
        ' Return
        ' inputs: 
        '
        '----------------------------------------------------------------------------------------------
        Dim affiliateList() As String = {entityKey}

        DB.CreateAggregDataHT(affiliateList)

        If IsNothing(GLOBALMC) Then GLOBALMC = New ModelComputer
        If GLOBALMC.WS.Range(MODEL_BUILT_CELL_FLAG).Value2 = MODEL_BUILT_FLAG_TRUE Then
        Else
            GLOBALMC = New ModelComputer
        End If

        For Each item As String In DB.dataHT.Keys
            For Each timePeriod As Integer In DB.periodList
                GLOBALMC.ModelDictionary.Item(item).Item(timePeriod).Value2 = DB.dataHT.Item(item).Item(timePeriod)
            Next
        Next
        GLOBALMC.modelUpdate()
        Try
            PPSKeys = GLOBALMC.ModelDictionary.Item(accountKey).Item(period).Value2
        Catch
        End Try

    End Function


End Class
