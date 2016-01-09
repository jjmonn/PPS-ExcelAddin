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
' Last modified: 09/01/2016


Imports System.Collections.Generic
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Core
Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Linq
Imports CRUD


Friend Class ReportUploadWorksheetsEventHandler

#Region "Instance Variables"

    ' Objects
    Private m_dataSet As ModelDataSet
    Private m_acquisitionModel As AcquisitionModel
    Private m_factsStorage As New FactsStorage
    Private m_dataModificationsTracker As DataModificationsTracking
    Private m_reportUploadController As ReportUploadControler
    Private m_logController As New FactLogController
    Private m_logView As LogView
    Private m_clientSelectionUI As PDCClientSelectionUI
    Private m_periodIdentifier As Char

    ' Variables
    Private m_disableWSChangeFlag As Boolean
    Friend m_excelWorksheet As Excel.Worksheet
    Private m_currentCellAddress As String

    ' const 
    Private MAX_NB_ROWS As UInt16 = 16384



#End Region

#Region "Initialize"

    Friend Sub AssociateSubmissionWSController(ByRef p_generalSubmissionController As ReportUploadControler, _
                                               ByRef p_dataSet As ModelDataSet, _
                                               ByRef p_acquisitionModel As AcquisitionModel, _
                                                ByRef p_factsStorage As FactsStorage, _
                                               ByRef p_dataModificationTracker As DataModificationsTracking, _
                                               ByRef p_excelWorksheet As Excel.Worksheet)

        m_reportUploadController = p_generalSubmissionController
        m_dataSet = p_dataSet
        m_acquisitionModel = p_acquisitionModel
        m_factsStorage = p_factsStorage
        m_dataModificationsTracker = p_dataModificationTracker
        m_logView = New LogView(True)

           Select m_dataSet.m_processFlag
            Case Account.AccountProcess.RH
                Dim version As Version = GlobalVariables.Versions.GetValue(m_dataSet.m_currentVersionId)
                If version Is Nothing Then Exit Sub

                Select Case version.TimeConfiguration
                    Case CRUD.TimeConfig.YEARS : m_periodIdentifier = Computer.YEAR_PERIOD_IDENTIFIER
                    Case CRUD.TimeConfig.MONTHS : m_periodIdentifier = Computer.MONTH_PERIOD_IDENTIFIER
                    Case CRUD.TimeConfig.DAYS : m_periodIdentifier = Computer.DAY_PERIOD_IDENTIFIER
                End Select
        End Select

        GlobalVariables.APPS.CellDragAndDrop = False
        AddHandler p_excelWorksheet.Change, AddressOf Worksheet_Change
        AddHandler p_excelWorksheet.BeforeRightClick, AddressOf Worksheet_BeforeRightClick
        AddHandler p_excelWorksheet.SelectionChange, AddressOf Worksheet_SelectionChange
        m_excelWorksheet = p_excelWorksheet

    End Sub

#End Region

#Region "Update Worksheet"

    Friend Sub UpdateFinancialCalculatedItemsOnWS(ByRef p_entitiesName() As String)

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

        Dim tuple_ As New Tuple(Of String, String, String, String)(p_entityName, p_accountName, "", p_period)
        ' tuple -> entity, account, product, period
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

    Friend Sub UpdateFinancialInputsOnWorsheet()

        Dim l_value As Double
        For Each l_entityName As String In m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).Values
            For Each l_account As Account In m_dataSet.m_inputsAccountsList
                For Each l_period As Int32 In m_acquisitionModel.m_currentPeriodList
                    If m_acquisitionModel.m_databaseInputsDictionary(l_entityName)(l_account.Name).ContainsKey(m_acquisitionModel.m_periodsIdentifyer & l_period) = True Then
                        l_value = m_acquisitionModel.m_databaseInputsDictionary(l_entityName)(l_account.Name)(m_acquisitionModel.m_periodsIdentifyer & l_period)
                    Else
                        l_value = 0
                    End If
                    If Double.IsNaN(l_value) Then l_value = 0
                    ' tuple -> entity, account, employee, period
                    Dim l_tuple As New Tuple(Of String, String, String, String)(l_entityName, l_account.Name, "", l_period)
                    m_dataSet.m_datasetCellsDictionary(l_tuple).Value2 = l_value
                Next
            Next
        Next

    End Sub

    Friend Sub UpdateRHInputsOnWorsheet()

        Dim l_value As String
        On Error Resume Next
        Dim l_entityName As String = m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).Values(0)
        Dim l_accountName As String = m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT).Values(0)

        For Each l_employeeName As String In m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.EMPLOYEE).Values
            For Each l_period As Int32 In m_dataSet.m_dimensionsValueAddressDict(ModelDataSet.Dimension.PERIOD).Keys

                Dim l_fact As CRUD.Fact = m_factsStorage.GetRHFact(l_accountName, l_employeeName, m_periodIdentifier & l_period)
                If l_fact Is Nothing Then
                    l_value = ""
                Else
                    Dim l_client As AxisElem = GlobalVariables.AxisElems.GetValue(CRUD.AxisType.Client, l_fact.ClientId)
                    If l_client Is Nothing Then Continue For
                    l_value = l_client.Name
                End If

                ' tuple -> entity, account, employee, period
                Dim l_tuple As New Tuple(Of String, String, String, String)(l_entityName, l_accountName, l_employeeName, l_period)
                m_dataSet.m_datasetCellsDictionary(l_tuple).Value2 = l_value
            Next
        Next

    End Sub

#End Region

#Region "Events"

#Region "Worksheet change"

    ' Listen to changes in associated worksheet
    Friend Sub Worksheet_Change(ByVal p_target As Excel.Range)
        Select Case m_dataSet.m_processFlag
            Case Account.AccountProcess.FINANCIAL : WorksheetChangeFinancial(p_target)
            Case Account.AccountProcess.RH : WorksheetChangePDC(p_target)
        End Select
    End Sub

    Private Sub WorksheetChangeFinancial(ByVal p_target As Excel.Range)

        ' If row or column insertion then exit and relaunch dataset snapshot
        If p_target.Count > MAX_NB_ROWS Then
            m_reportUploadController.RefreshSnapshot(False)
            Exit Sub
        End If

        Dim modelUpdateFlag As Boolean = False
        Dim dependents_cells As Excel.Range = Nothing
        Dim entityName As New String("")
        If m_reportUploadController.m_isUpdating = False AndAlso m_disableWSChangeFlag = False Then

            For Each cell As Excel.Range In p_target.Cells
                If CellBelongsToFinancialDimensionsDefinition(cell.Address) Then Continue For

                Dim intersect = GlobalVariables.APPS.Intersect(cell, m_dataModificationsTracker.m_dataSetRegion)
                If Not intersect Is Nothing Then

                    entityName = m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_entityName
                    If IsNumeric(cell.Value) Then
                        If m_acquisitionModel.CheckIfFPICalculatedItem(m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_accountName, _
                                                                    m_dataSet.m_datasetCellDimensionsDictionary(cell.Address).m_period) = False Then

                            ' Cell modification registration
                            modelUpdateFlag = True
                            m_reportUploadController.UpdateModelFromExcelUpdate(entityName, _
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
                                        m_reportUploadController.UpdateModelFromExcelUpdate(m_dataSet.m_datasetCellDimensionsDictionary(dependents_cells.Address).m_entityName, _
                                                                                               m_dataSet.m_datasetCellDimensionsDictionary(dependents_cells.Address).m_accountName, _
                                                                                               m_dataSet.m_datasetCellDimensionsDictionary(dependents_cells.Address).m_period, _
                                                                                               dependant_cell.Value2, _
                                                                                               dependant_cell.Address)
                                    End If
                                Next
                            End If
                            If m_reportUploadController.m_autoCommitFlag = True Then m_reportUploadController.DataSubmission()
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
                m_reportUploadController.UpdateCalculatedItems()
            End If
        End If

    End Sub

    Private Sub WorksheetChangePDC(ByVal p_target As Excel.Range)

        ' If row or column insertion then exit and relaunch dataset snapshot
        If p_target.Count > MAX_NB_ROWS Then
            m_reportUploadController.RefreshSnapshot(False)
            Exit Sub
        End If

        Dim l_modelUpdateFlag As Boolean = False
        If m_reportUploadController.m_isUpdating = False AndAlso m_disableWSChangeFlag = False Then

            For Each l_cell As Excel.Range In p_target.Cells
                If CellBelongsToPDCDimensionsDefinition(l_cell.Address) Then Continue For

                Dim l_intersect = GlobalVariables.APPS.Intersect(l_cell, m_dataModificationsTracker.m_dataSetRegion)
                If Not l_intersect Is Nothing Then
                    ' Modifications registering
                    RHCellsModificationTreatment(l_cell, l_modelUpdateFlag)
                    ' Auto commit if needed
                    If m_reportUploadController.m_autoCommitFlag = True Then m_reportUploadController.DataSubmission()
                Else
                    ' Put back the former value in case invalid input has been given (eg. double, ...)
                    m_disableWSChangeFlag = True
                    l_cell.Value = m_dataSet.m_datasetCellDimensionsDictionary(l_cell.Address).m_client
                    m_disableWSChangeFlag = False
                End If
            Next
        End If

    End Sub

    Private Sub RHCellsModificationTreatment(ByRef p_cell As Excel.Range, _
                                             ByRef p_modelUpdateFlag As Boolean)

        On Error Resume Next
        SyncLock (m_dataSet)
            Dim l_cellDimensions = m_dataSet.m_datasetCellDimensionsDictionary(p_cell.Address)
            Dim l_entityName As String = l_cellDimensions.m_entityName

            If VarType(p_cell.Value) = VariantType.String _
            Or p_cell.Value2 = "" Then

                ' Register modification if needed
                RHRegisterModificationIfDifferentValue(p_cell, l_cellDimensions, p_modelUpdateFlag)

                ' Register modification in dependant cells
                Dim l_dependantCells = p_cell.Dependents
                If Not l_dependantCells Is Nothing Then
                    For Each l_dependantCell In l_dependantCells.Cells
                        Dim l_dependantIntersect = GlobalVariables.APPS.Intersect(l_dependantCell, m_dataModificationsTracker.m_dataSetRegion)
                        If Not l_dependantIntersect Is Nothing Then
                            RHCellsModificationTreatment(l_dependantCell, p_modelUpdateFlag)
                        End If
                    Next
                End If
            End If
        End SyncLock

    End Sub

#End Region

    Friend Sub Worksheet_BeforeRightClick(ByVal Target As Range, ByRef Cancel As Boolean)

        Select Case m_dataSet.m_processFlag
            Case Account.AccountProcess.FINANCIAL : WorksheetChangeFinancial(Target)
                Dim logButton As CommandBarButton
                On Error Resume Next
                GlobalVariables.APPS.CommandBars("Cell").Controls(Local.GetValue("upload.financialBILog")).Delete()
                logButton = GlobalVariables.APPS.CommandBars("Cell").Controls.Add(Temporary:=True)
                logButton.Caption = Local.GetValue("upload.financialBILog")
                logButton.Style = MsoButtonStyle.msoButtonCaption
                '   logButton.Picture = My.Resources.fbi_dark_blue_icon
                m_currentCellAddress = Target.Address
                AddHandler logButton.Click, AddressOf DisplayLogButton_Click
                On Error GoTo 0

            Case Account.AccountProcess.RH : WorksheetChangePDC(Target)
                Dim l_clientSelectionButton As CommandBarButton
                On Error Resume Next
                GlobalVariables.APPS.CommandBars("Cell").Controls(Local.GetValue("upload.clientSelectionButtonText")).Delete()
                l_clientSelectionButton = GlobalVariables.APPS.CommandBars("Cell").Controls.Add(Temporary:=True)
                l_clientSelectionButton.Caption = Local.GetValue("upload.clientSelectionButtonText")
                l_clientSelectionButton.Style = MsoButtonStyle.msoButtonCaption
                '   logButton.Picture = My.Resources.fbi_dark_blue_icon
                m_currentCellAddress = Target.Address
                AddHandler l_clientSelectionButton.Click, AddressOf DisplayClientSelection_Click
                On Error GoTo 0

        End Select

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

                Dim logsHashTable As New Action(Of List(Of CRUD.FactLog))(AddressOf DisplayLogUI)
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

    Private Sub DisplayLogUI(p_logValuesHt As List(Of FactLog))

        m_logView.DisplayLogValues(p_logValuesHt)
        ' ui position = right click location ?

    End Sub

    Friend Sub Worksheet_SelectionChange(ByVal Target As Excel.Range)

        ' Side pane display
        Dim address As String = Strings.Replace(Target.Address, "$", "")
        If m_dataSet.m_datasetCellDimensionsDictionary.ContainsKey(Target.Address) Then
            Dim datasetCellStruct As ModelDataSet.DataSetCellDimensions = m_dataSet.m_datasetCellDimensionsDictionary(Target.Address)
            GlobalVariables.s_reportUploadSidePane.DisplayAccountDetails(GlobalVariables.Accounts.GetValueId(datasetCellStruct.m_accountName))
        ElseIf m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT).ContainsKey(address) Then
            GlobalVariables.s_reportUploadSidePane.DisplayAccountDetails(GlobalVariables.Accounts.GetValueId(m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT)(address)))
        ElseIf m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.OUTPUTACCOUNT).ContainsKey(address) Then
            GlobalVariables.s_reportUploadSidePane.DisplayAccountDetails(GlobalVariables.Accounts.GetValueId(m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.OUTPUTACCOUNT)(address)))
        Else
            GlobalVariables.s_reportUploadSidePane.DisplayEmptyTextBoxes()
        End If

        ' Associate the right GRS Controller if the worksheet is different
        If m_reportUploadController.IsCurrentController() = False Then
            m_reportUploadController.ActivateReportUploadController()
        End If

    End Sub

#End Region

#Region "PDC Clients Selection Events"

    Private Sub DisplayClientSelection_Click(ctrl As CommandBarButton, ByRef cancelDefault As Boolean)

        m_clientSelectionUI = New PDCClientSelectionUI()
        m_clientSelectionUI.Show()

    End Sub

#End Region

#Region "Utilities"

    Private Function CellBelongsToFinancialDimensionsDefinition(ByVal p_address As String) As Boolean

        p_address = Replace(p_address, "$", "")
        If m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT).ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT)(p_address)
            m_disableWSChangeFlag = False
            Return True
        ElseIf m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD).ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD)(p_address)
            m_disableWSChangeFlag = False
            Return True
        ElseIf m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY)(p_address)
            m_disableWSChangeFlag = False
            Return True
        End If
        Return False

    End Function

    Private Function CellBelongsToPDCDimensionsDefinition(ByVal p_address As String) As Boolean

        p_address = Replace(p_address, "$", "")
        If m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.EMPLOYEE).ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.EMPLOYEE)(p_address)
            m_disableWSChangeFlag = False
            Return True
        ElseIf m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD).ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD)(p_address)
            m_disableWSChangeFlag = False
            Return True
        ElseIf m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY)(p_address)
            m_disableWSChangeFlag = False
            Return True
        ElseIf m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT).ContainsKey(p_address) Then
            m_disableWSChangeFlag = True
            m_excelWorksheet.Range(p_address).Value = m_dataSet.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT)(p_address)
            m_disableWSChangeFlag = False
            Return True
        End If
        Return False

    End Function

    Private Sub RHRegisterModificationIfDifferentValue(ByRef p_cell As Excel.Range, _
                                                       ByRef p_cellDimensions As ModelDataSet.DataSetCellDimensions, _
                                                       ByRef p_updateFlag As Boolean)


        ' si la cellule est vide register !!!!!!!!!!

        Dim l_fact = m_factsStorage.GetRHFact(p_cellDimensions.m_accountName, _
                                              p_cellDimensions.m_employee, _
                                              m_periodIdentifier & p_cellDimensions.m_period)
        If l_fact Is Nothing Then
            If p_cell.Value2 <> "" Then
                p_updateFlag = True
                m_reportUploadController.RegisterModification(p_cell.Address)
                Exit Sub
            End If
        End If

        Dim l_client As AxisElem = GlobalVariables.AxisElems.GetValue(l_fact.ClientId)
        If l_client Is Nothing Then
            p_updateFlag = True
            m_reportUploadController.RegisterModification(p_cell.Address)
            Exit Sub
        End If

        Dim l_caseInsensitiveWSClientName = LCase(p_cell.Value2)
        Dim l_caseInsensitiveDBClientName = LCase(l_client.Name)
        If l_caseInsensitiveWSClientName <> l_caseInsensitiveDBClientName Then
            p_updateFlag = True
            m_reportUploadController.RegisterModification(p_cell.Address)
        Else
            Exit Sub
        End If

    End Sub

#End Region

End Class

