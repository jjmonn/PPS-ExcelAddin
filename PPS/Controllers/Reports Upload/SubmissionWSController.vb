' cExcelSubmissionWorksheetController.vb
'
' Controls worksheet connected to GeneralSubmissionController -> events / update cells ... etc
'
' To do: 
'       - 
'       - 
'
' Known bugs: 
'       - 
'
' Author: Julien Monnereau
' Last modified: 18/09/2015


Imports System.Collections.Generic
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Core
Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Linq


Friend Class SubmissionWSController


#Region "Instance Variables"

    ' Objects
    Private m_dataSet As ModelDataSet
    Private m_acquisitionModel As AcquisitionModel
    Private m_dataModificationsTracker As DataModificationsTracking
    Private m_generalSubmissionController As GeneralSubmissionControler


    ' Variables
    Private m_disableWSChangeFlag As Boolean
    Friend m_excelWorksheet As Excel.Worksheet

    ' const 
    Private MAX_NB_ROWS As UInt16 = 16384

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputGeneralSubmissionController As GeneralSubmissionControler, _
                   ByRef inputDataSet As ModelDataSet, _
                   ByRef inputAcquisitionModel As AcquisitionModel, _
                   ByRef inputDataModificationTracker As DataModificationsTracking)

        m_generalSubmissionController = inputGeneralSubmissionController
        m_dataSet = inputDataSet
        m_acquisitionModel = inputAcquisitionModel
        m_dataModificationsTracker = inputDataModificationTracker

    End Sub

    Friend Sub AssociateWS(ByRef p_excelWorksheet As Excel.Worksheet)

        AddHandler p_excelWorksheet.Change, AddressOf Worksheet_Change
        AddHandler p_excelWorksheet.BeforeRightClick, AddressOf Worksheet_BeforeRightClick
        '      AddHandler p_excelWorksheet.SelectionChange
        Me.m_excelWorksheet = p_excelWorksheet


        ' AddHandler thisworkbook.SheetSelectionChange, AddressOf Worksheet_SelectionChange
        'WS.Protect(DrawingObjects:=False, _
        '           Contents:=True, _
        '           Scenarios:= _
        '           False, _
        '           AllowFormattingCells:=True, _
        '           AllowFormattingColumns:=True, _
        '           AllowFormattingRows:=True, _
        '           AllowInsertingHyperlinks:=True, _
        '           AllowSorting:= _
        '           True, _
        '           AllowFiltering:=True, _
        '           AllowUsingPivotTables:=True)

    End Sub

#End Region


#Region "Update Worksheet"

    Friend Sub UpdateCalculatedItemsOnWS(ByRef entityName As String)

        Dim entityId As Int32 = CInt(m_acquisitionModel.entitiesNameIdDict(entityName))
        For Each accountName As String In m_acquisitionModel.outputsList
            For Each period As Int32 In m_acquisitionModel.currentPeriodList
                If m_acquisitionModel.accountsNamesFormulaTypeDict(accountName) = GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT Then
                    If period <> m_acquisitionModel.currentPeriodList(0) Then
                        SetDatsetCellValue(entityId, entityName, accountName, period)
                    End If
                Else
                    SetDatsetCellValue(entityId, entityName, accountName, period)
                End If
            Next
        Next

    End Sub

    Private Sub SetDatsetCellValue(ByRef p_entityId As Int32, _
                                   ByRef p_entityName As String, _
                                   ByRef p_accountName As String, _
                                   ByRef p_period As Int32)

        Dim tuple_ As New Tuple(Of String, String, String)(p_entityName, p_accountName, p_period)
        If m_dataSet.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
            Dim value = m_acquisitionModel.GetCalculatedValue(p_entityId, _
                                                            m_dataSet.m_accountsNameIdDictionary(p_accountName), _
                                                            m_acquisitionModel.periodsIdentifyer & p_period)
            If Double.IsNaN(value) Then value = 0

            m_dataSet.m_datasetCellsDictionary(tuple_).Value2 = value
        End If

    End Sub

    Friend Sub UpdateInputsOnWS()

        Dim value As Double
        For Each entityName As String In m_dataSet.m_entitiesAddressValuesDictionary.Values
            For Each accountName As String In m_dataSet.m_inputsAccountsList
                For Each period As Int32 In m_acquisitionModel.currentPeriodList
                    If m_acquisitionModel.dataBaseInputsDictionary(entityName)(accountName).ContainsKey(m_acquisitionModel.periodsIdentifyer & period) = True Then
                        value = m_acquisitionModel.dataBaseInputsDictionary(entityName)(accountName)(m_acquisitionModel.periodsIdentifyer & period)
                    Else
                        value = 0
                    End If
                    If Double.IsNaN(value) Then value = 0
                    Dim tuple_ As New Tuple(Of String, String, String)(entityName, accountName, period)
                    m_dataSet.m_datasetCellsDictionary(tuple_).Value2 = value
                Next
            Next
        Next

    End Sub


#End Region


#Region "Events"

    ' Listen to changes in associated worksheet
    Friend Sub Worksheet_Change(ByVal p_target As Excel.Range)

        ' If row or column insertion then exit and relaunch dataset snapshot
        If p_target.Count > MAX_NB_ROWS Then
            m_generalSubmissionController.RefreshSnapshot(False)
            Exit Sub
        End If

        Dim modelUpdateFlag As Boolean = False
        Dim dependents_cells As Excel.Range = Nothing
        Dim entityName As String
        If m_generalSubmissionController.m_isUpdating = False AndAlso m_disableWSChangeFlag = False Then

            For Each cell As Excel.Range In p_target.Cells

                Dim intersect = GlobalVariables.APPS.Intersect(cell, m_dataModificationsTracker.m_dataSetRegion)
                If Not intersect Is Nothing Then

                    entityName = m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_entityName
                    If IsNumeric(cell.Value) Then
                        If m_acquisitionModel.CheckIfBSCalculatedItem(m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
                                                                    m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_period) = False Then

                            ' Cell modification registration
                            modelUpdateFlag = True
                            m_generalSubmissionController.UpdateModelFromExcelUpdate(entityName, _
                                                                                   m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
                                                                                   m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_period, _
                                                                                   cell.Value2, _
                                                                                   cell.Address)

                            ' Register modification in dependant cells
                            On Error Resume Next
                            dependents_cells = cell.Dependents
                            If Not dependents_cells Is Nothing Then
                                For Each dependant_cell As Excel.Range In dependents_cells
                                    intersect = GlobalVariables.APPS.Intersect(dependant_cell, m_dataModificationsTracker.m_dataSetRegion)
                                    If Not intersect Is Nothing Then
                                        m_generalSubmissionController.UpdateModelFromExcelUpdate(m_dataSet.m_datasetCellDimensionsDictionary(dependents_cells.Address).m_entityName, _
                                                                                               m_dataSet.m_datasetCellDimensionsDictionary(dependents_cells.Address).m_accountName, _
                                                                                               m_dataSet.m_datasetCellDimensionsDictionary(dependents_cells.Address).m_period, _
                                                                                               dependant_cell.Value2, _
                                                                                               dependant_cell.Address)
                                    End If
                                Next
                            End If
                            If m_generalSubmissionController.m_autoCommitFlag = True Then m_generalSubmissionController.DataSubmission()

                        End If
                    Else
                        ' Put back the former value in case invalid input has been given (eg. string, ...)
                        m_disableWSChangeFlag = True
                        cell.Value = m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_value
                        m_disableWSChangeFlag = False
                    End If
                Else
                    ' Put back the real output value in case the output has been overwritten
                    On Error Resume Next
                    entityName = m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_entityName
                    Dim intersectOutput = GlobalVariables.APPS.Intersect(cell, m_dataModificationsTracker.m_outputsRegion)
                    If Not intersectOutput Is Nothing Then
                        m_disableWSChangeFlag = True
                        SetDatsetCellValue(m_dataSet.m_entitiesNameIdDictionary(entityName), _
                                           entityName, _
                                           m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
                                           m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_period)
                        m_disableWSChangeFlag = False
                    End If
                End If
            Next
            If modelUpdateFlag = True Then
                GlobalVariables.APPS.Interactive = False
                m_generalSubmissionController.UpdateCalculatedItems(entityName)
            End If
        End If

    End Sub

    Friend Sub Worksheet_BeforeRightClick(ByVal Target As Range, ByRef Cancel As Boolean)

        Dim newButton As CommandBarButton
        On Error Resume Next
        GlobalVariables.APPS.CommandBars("Cell").Controls("Account Details").Delete()
        newButton = GlobalVariables.APPS.CommandBars("Cell").Controls.Add(Temporary:=True)
        newButton.Caption = "Account Details"
        newButton.Style = MsoButtonStyle.msoButtonCaption
        ' rajouter image -> icone PPS
        AddHandler newButton.Click, AddressOf ShowAccountDetailsRightClick
        On Error GoTo 0

    End Sub

    Public Sub ShowAccountDetailsRightClick(ctrl As CommandBarButton, ByRef cancelDefault As Boolean)

        MsgBox("account(s detail")
        ' to be implemented priority normal -> display account's formula

    End Sub

    Friend Sub Worksheet_SelectionChange(ByVal Sh As Object, ByVal Target As Excel.Range)

        ' doea not work yet : error in addin express Get Thisworkbook function
        ' priority normal 
        Dim ws As Excel.Worksheet = CType(Sh, Excel.Worksheet)
        MsgBox("worksheet changed: " & ws.Name)

        ' if ok 
        '  -> change active GRS in addin ()
        '  -> update ribbon
        ' Entity
        ' Currency 
        ' Product
        ' Client

    End Sub

#End Region



End Class
