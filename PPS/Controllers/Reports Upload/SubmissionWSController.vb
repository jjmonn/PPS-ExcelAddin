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
    Private DataSet As ModelDataSet
    Private AcquisitionModel As AcquisitionModel
    Private DataModificationsTracker As DataModificationsTracking
    Private GeneralSubmissionController As GeneralSubmissionControler


    ' Variables
    Private disableWSChange As Boolean
    Friend ws As Excel.Worksheet

    ' const 
    Private MAX_NB_ROWS As UInt16 = 16384

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputGeneralSubmissionController As GeneralSubmissionControler, _
                   ByRef inputDataSet As ModelDataSet, _
                   ByRef inputAcquisitionModel As AcquisitionModel, _
                   ByRef inputDataModificationTracker As DataModificationsTracking)

        GeneralSubmissionController = inputGeneralSubmissionController
        DataSet = inputDataSet
        AcquisitionModel = inputAcquisitionModel
        DataModificationsTracker = inputDataModificationTracker

    End Sub

    Friend Sub AssociateWS(ByRef WS As Excel.Worksheet)

        AddHandler WS.Change, AddressOf Worksheet_Change
        AddHandler WS.BeforeRightClick, AddressOf Worksheet_BeforeRightClick
        Me.ws = WS

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

    Friend Sub updateCalculatedItemsOnWS(ByRef entityName As String)

        Dim entityId As Int32 = CInt(AcquisitionModel.entitiesNameIdDict(entityName))
        For Each accountName As String In DataSet.OutputsAccountsAddressvaluesDictionary.Values
            For Each period As Int32 In AcquisitionModel.currentPeriodList
                SetDatsetCellValue(entityId, entityName, accountName, period)
            Next
        Next

    End Sub

    Private Sub SetDatsetCellValue(ByRef p_entityId As Int32, _
                                   ByRef p_entityName As String, _
                                   ByRef p_accountName As String, _
                                   ByRef p_period As Int32)

        Dim tuple_ As New Tuple(Of String, String, String)(p_entityName, p_accountName, p_period)
        If DataSet.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
            Dim value = AcquisitionModel.GetCalculatedValue(p_entityId, _
                                                            DataSet.m_accountsNameIdDictionary(p_accountName), _
                                                            AcquisitionModel.periodsIdentifyer & p_period)
            If Double.IsNaN(value) Then value = 0

            DataSet.m_datasetCellsDictionary(tuple_).Value2 = value
        End If

    End Sub

    Friend Sub updateInputsOnWS()

        Dim value As Double
        For Each entityName As String In DataSet.EntitiesAddressValuesDictionary.Values
            For Each accountName As String In DataSet.m_inputsAccountsList
                For Each period As Int32 In AcquisitionModel.currentPeriodList
                    If AcquisitionModel.dataBaseInputsDictionary(entityName)(accountName).ContainsKey(AcquisitionModel.periodsIdentifyer & period) = True Then
                        value = AcquisitionModel.dataBaseInputsDictionary(entityName)(accountName)(AcquisitionModel.periodsIdentifyer & period)
                    Else
                        value = 0
                    End If
                    If Double.IsNaN(value) Then value = 0
                    Dim tuple_ As New Tuple(Of String, String, String)(entityName, accountName, period)
                    DataSet.m_datasetCellsDictionary(tuple_).Value2 = value
                Next
            Next
        Next

    End Sub

 
#End Region


#Region "Events"

    ' Listen to changes in associated worksheet
    Friend Sub Worksheet_Change(ByVal Target As Excel.Range)

        ' If row or column insertion then exit and relaunch dataset snapshot
        If Target.Count > MAX_NB_ROWS Then
            GeneralSubmissionController.RefreshSnapshot(False)
            Exit Sub
        End If

        Dim modelUpdateFlag As Boolean = False
        Dim dependents_cells As Excel.Range = Nothing
        Dim entityName As String
        If GeneralSubmissionController.isUpdating = False AndAlso disableWSChange = False Then

            For Each cell As Excel.Range In Target.Cells

                entityName = DataSet.m_datasetCellDimensionsDictionary(cell.Address).m_entityName
                Dim intersect = GlobalVariables.APPS.Intersect(cell, DataModificationsTracker.dataSetRegion)
                If Not intersect Is Nothing Then

                    If IsNumeric(cell.Value) Then
                        If AcquisitionModel.CheckIfBSCalculatedItem(DataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
                                                                    DataSet.m_datasetCellDimensionsDictionary(cell.Address).m_period) = False Then

                            ' Cell modification registration
                            modelUpdateFlag = True
                            GeneralSubmissionController.UpdateModelFromExcelUpdate(entityName, _
                                                                                   DataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
                                                                                   DataSet.m_datasetCellDimensionsDictionary(cell.Address).m_period, _
                                                                                   cell.Value2, _
                                                                                   cell.Address)

                            ' Register modification in dependant cells
                            On Error Resume Next
                            dependents_cells = cell.Dependents
                            If Not dependents_cells Is Nothing Then
                                For Each dependant_cell As Excel.Range In dependents_cells
                                    intersect = GlobalVariables.APPS.Intersect(dependant_cell, DataModificationsTracker.dataSetRegion)
                                    If Not intersect Is Nothing Then
                                        GeneralSubmissionController.UpdateModelFromExcelUpdate(DataSet.m_datasetCellDimensionsDictionary(dependents_cells.Address).m_entityName, _
                                                                                               DataSet.m_datasetCellDimensionsDictionary(dependents_cells.Address).m_accountName, _
                                                                                               DataSet.m_datasetCellDimensionsDictionary(dependents_cells.Address).m_period, _
                                                                                               dependant_cell.Value2, _
                                                                                               dependant_cell.Address)
                                    End If
                                Next
                            End If
                            If GeneralSubmissionController.autoCommitFlag = True Then GeneralSubmissionController.DataSubmission()

                        End If
                    Else
                        ' Put back the former value in case invalid input has been given (eg. string, ...)
                        disableWSChange = True
                        cell.Value = DataSet.m_datasetCellDimensionsDictionary(cell).m_value
                        disableWSChange = False
                    End If
                Else
                    ' Put back the real output value in case the output has been overwritten
                    On Error Resume Next
                    Dim intersectOutput = GlobalVariables.APPS.Intersect(cell, DataModificationsTracker.outputsRegion)
                    If Not intersectOutput Is Nothing Then
                        disableWSChange = True
                        SetDatsetCellValue(DataSet.m_entitiesNameIdDictionary(entityName), _
                                           entityName, _
                                           DataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
                                           DataSet.m_datasetCellDimensionsDictionary(cell.Address).m_period)
                        disableWSChange = False
                    End If
                End If
            Next
            If modelUpdateFlag = True Then
                GlobalVariables.APPS.Interactive = False
                GeneralSubmissionController.UpdateCalculatedItems(entityName)
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
