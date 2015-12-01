﻿' cExcelSubmissionWorksheetController.vb
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
' Last modified: 15/10/2015


Imports System.Collections.Generic
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Core
Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Linq
Imports CRUD


Friend Class SubmissionWSController


#Region "Instance Variables"

    ' Objects
    Private m_dataSet As ModelDataSet
    Private m_acquisitionModel As AcquisitionModel
    Private m_dataModificationsTracker As DataModificationsTracking
    Private m_generalSubmissionController As GeneralSubmissionControler
    Private m_logController As New LogController
    Private m_logView As LogView

    ' Variables
    Private m_disableWSChangeFlag As Boolean
    Friend m_excelWorksheet As Excel.Worksheet
    Private m_currentCellAddress As String

    ' const 
    Private MAX_NB_ROWS As UInt16 = 16384

#End Region


#Region "Initialize"

    Friend Sub AssociateSubmissionWSController(ByRef p_generalSubmissionController As GeneralSubmissionControler, _
                                               ByRef p_dataSet As ModelDataSet, _
                                               ByRef p_acquisitionModel As AcquisitionModel, _
                                               ByRef p_dataModificationTracker As DataModificationsTracking, _
                                               ByRef p_excelWorksheet As Excel.Worksheet)

        m_generalSubmissionController = p_generalSubmissionController
        m_dataSet = p_dataSet
        m_acquisitionModel = p_acquisitionModel
        m_dataModificationsTracker = p_dataModificationTracker
        m_logView = New LogView(True)

        GlobalVariables.APPS.CellDragAndDrop = False
        AddHandler p_excelWorksheet.Change, AddressOf Worksheet_Change
        AddHandler p_excelWorksheet.BeforeRightClick, AddressOf Worksheet_BeforeRightClick
        AddHandler p_excelWorksheet.SelectionChange, AddressOf Worksheet_SelectionChange
        m_excelWorksheet = p_excelWorksheet
      
    End Sub

#End Region


#Region "Update Worksheet"

    Friend Sub UpdateCalculatedItemsOnWS(ByRef p_entitiesName() As String)

        For Each l_entityName As String In p_entitiesName
            Dim l_entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, l_entityName)
            If Not l_entity Is Nothing Then
                For Each l_account As Account In m_acquisitionModel.m_outputsList
                    For Each l_period As Int32 In m_acquisitionModel.m_currentPeriodList
                        If Not l_account Is Nothing Then
                            If l_account.FormulaType = Account.FormulaTypes.FIRST_PERIOD_INPUT Then
                                If l_period <> m_acquisitionModel.m_currentPeriodList(0) Then
                                    SetDatsetCellValue(l_entity.Id, l_entityName, l_account.Name, l_period)
                                End If
                            Else
                                SetDatsetCellValue(l_entity.Id, l_entity.Name, l_account.Name, l_period)
                            End If
                        End If
                    Next
                Next
            End If
        Next

    End Sub

    Private Sub SetDatsetCellValue(ByRef p_entityId As Int32, _
                                   ByRef p_entityName As String, _
                                   ByRef p_accountName As String, _
                                   ByRef p_period As Int32)

        Dim tuple_ As New Tuple(Of String, String, String)(p_entityName, p_accountName, p_period)
        If m_dataSet.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
            Dim l_account = GlobalVariables.Accounts.GetValue(p_accountName)

            If l_account Is Nothing Then Exit Sub
            Dim value = m_acquisitionModel.GetCalculatedValue(p_entityId, _
                                                            l_account.Id, _
                                                            m_acquisitionModel.m_periodsIdentifyer & p_period)
            If Double.IsNaN(value) Then value = 0

            m_dataSet.m_datasetCellsDictionary(tuple_).Value2 = value
        End If

    End Sub

    Friend Sub UpdateInputsOnWS()

        Dim value As Double
        For Each entityName As String In m_dataSet.m_entitiesAddressValuesDictionary.Values
            For Each l_account As Account In m_dataSet.m_inputsAccountsList
                For Each period As Int32 In m_acquisitionModel.m_currentPeriodList
                    If m_acquisitionModel.m_databaseInputsDictionary(entityName)(l_account.Name).ContainsKey(m_acquisitionModel.m_periodsIdentifyer & period) = True Then
                        value = m_acquisitionModel.m_databaseInputsDictionary(entityName)(l_account.Name)(m_acquisitionModel.m_periodsIdentifyer & period)
                    Else
                        value = 0
                    End If
                    If Double.IsNaN(value) Then value = 0
                    Dim tuple_ As New Tuple(Of String, String, String)(entityName, l_account.Name, period)
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
        Dim entityName As New String("")
        If m_generalSubmissionController.m_isUpdating = False AndAlso m_disableWSChangeFlag = False Then

            For Each cell As Excel.Range In p_target.Cells
                If CellBelongsToDimensionsDefinition(cell.Address) Then Continue For

                Dim intersect = GlobalVariables.APPS.Intersect(cell, m_dataModificationsTracker.m_dataSetRegion)
                If Not intersect Is Nothing Then

                    entityName = m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_entityName
                    If IsNumeric(cell.Value) Then
                        If m_acquisitionModel.CheckIfFPICalculatedItem(m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
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
                        Else
                            ' First period input formula type : output period
                            m_disableWSChangeFlag = True
                            SetDatsetCellValue(GlobalVariables.AxisElems.GetValueId(AxisType.Entities, entityName), _
                                               entityName, _
                                               m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
                                               m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_period)
                            m_disableWSChangeFlag = False
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
                        SetDatsetCellValue(GlobalVariables.AxisElems.GetValueId(AxisType.Entities, entityName), _
                                           entityName, _
                                           m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
                                           m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_period)
                        m_disableWSChangeFlag = False
                    End If
                End If
            Next
            If modelUpdateFlag = True Then
                GlobalVariables.APPS.Interactive = False
                m_generalSubmissionController.UpdateCalculatedItems()
            End If
        End If

    End Sub

    Friend Sub Worksheet_BeforeRightClick(ByVal Target As Range, ByRef Cancel As Boolean)

        Dim logButton As CommandBarButton
        On Error Resume Next
        GlobalVariables.APPS.CommandBars("Cell").Controls("Financial Bi Data Log").Delete()
        logButton = GlobalVariables.APPS.CommandBars("Cell").Controls.Add(Temporary:=True)
        logButton.Caption = "Financial Bi Data Log"
        logButton.Style = MsoButtonStyle.msoButtonCaption
        '   logButton.Picture = My.Resources.fbi_dark_blue_icon
        m_currentCellAddress = Target.Address
        AddHandler logButton.Click, AddressOf DisplayLogButton_Click
        On Error GoTo 0

    End Sub

    Private Sub DisplayLogButton_Click(ctrl As CommandBarButton, ByRef cancelDefault As Boolean)

        Dim entityId As Int32 = 0
        Dim periodId As String = ""
        Dim versionId As String = My.Settings.version_id
        Dim l_account As Account

        If m_dataSet.m_datasetCellDimensionsDictionary.ContainsKey(m_currentCellAddress) Then
            Dim datasetCellStruct As ModelDataSet.DataSetCellDimensions = m_dataSet.m_datasetCellDimensionsDictionary(m_currentCellAddress)
            l_account = GlobalVariables.Accounts.GetValue(datasetCellStruct.m_accountName)
            entityId = GlobalVariables.AxisElems.GetValueId(AxisType.Entities, datasetCellStruct.m_entityName)
            periodId = datasetCellStruct.m_period

            If l_account Is Nothing Then Exit Sub
            If l_account.Id = 0 Or entityId = 0 Then
                MsgBox("Invalid Account or Entity. Unable to display log. Contact your administratyor or the Financial BI team if the error persists.")
                Exit Sub
            End If

            If l_account.FormulaType = Account.FormulaTypes.HARD_VALUE_INPUT _
            Or l_account.FormulaType = Account.FormulaTypes.FIRST_PERIOD_INPUT Then
                ' we must add the check for entity = input when report upload is adapted to all orientations
                ' Priority normal

                Dim logsHashTable As New Action(Of List(Of Hashtable))(AddressOf DisplayLogUI)
                m_logController.GetFactLog(l_account.Id, _
                                           entityId, _
                                           periodId, _
                                           versionId,
                                           logsHashTable)

                m_logView.SetEntityAndAccountNames(datasetCellStruct.m_entityName, _
                                                   datasetCellStruct.m_accountName)
                m_logView.Show()
            Else
                MsgBox("The selected cell correspond to a computed Account or Entity. Log is only available for inputs.")
            End If
        Else
            MsgBox("The log can only be displayed for values cells.")
        End If

    End Sub

    Private Sub DisplayLogUI(p_logValuesHt As List(Of Hashtable))

        m_logView.DisplayLogValues(p_logValuesHt)
        ' ui position = right click location ?

    End Sub

    Friend Sub Worksheet_SelectionChange(ByVal Target As Excel.Range)

        ' Side pane display
        Dim address As String = Strings.Replace(Target.Address, "$", "")
        If m_dataSet.m_datasetCellDimensionsDictionary.ContainsKey(Target.Address) Then
            Dim datasetCellStruct As ModelDataSet.DataSetCellDimensions = m_dataSet.m_datasetCellDimensionsDictionary(Target.Address)
            GlobalVariables.s_reportUploadSidePane.DisplayAccountDetails(GlobalVariables.Accounts.GetValueId(datasetCellStruct.m_accountName))
        ElseIf m_dataSet.m_accountsAddressValuesDictionary.ContainsKey(address) Then
            GlobalVariables.s_reportUploadSidePane.DisplayAccountDetails(GlobalVariables.Accounts.GetValueId(m_dataSet.m_accountsAddressValuesDictionary(address)))
        ElseIf m_dataSet.m_outputsAccountsAddressvaluesDictionary.ContainsKey(address) Then
            GlobalVariables.s_reportUploadSidePane.DisplayAccountDetails(GlobalVariables.Accounts.GetValueId(m_dataSet.m_outputsAccountsAddressvaluesDictionary(address)))
        Else
            GlobalVariables.s_reportUploadSidePane.DisplayEmptyTextBoxes()
        End If

        ' Associate the right GRS Controller if the worksheet is different
        If m_generalSubmissionController.IsCurrentController() = False Then
            m_generalSubmissionController.ActivateGRSController()
        End If

    End Sub


#End Region


#Region "Utilities"

    Private Function CellBelongsToDimensionsDefinition(ByVal p_address As String) As Boolean

        p_address = Replace(p_address, "$", "")
        If m_dataSet.m_accountsAddressValuesDictionary.ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_accountsAddressValuesDictionary(p_address)
            m_disableWSChangeFlag = False
            Return True
        ElseIf m_dataSet.m_periodsAddressValuesDictionary.ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_periodsAddressValuesDictionary(p_address)
            m_disableWSChangeFlag = False
            Return True
        ElseIf m_dataSet.m_entitiesAddressValuesDictionary.ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_entitiesAddressValuesDictionary(p_address)
            m_disableWSChangeFlag = False
            Return True
        End If
        Return False

    End Function

#End Region

End Class
