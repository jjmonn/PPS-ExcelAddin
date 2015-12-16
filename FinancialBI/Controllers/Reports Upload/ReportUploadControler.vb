' ReportUploadControler.vb
' 
'
' Author: Julien Monnereau
' Last modified: 14/12/2015


Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports VIBlend.WinForms.DataGridView
Imports System.Linq
Imports AddinExpress.MSO
Imports System.Collections
Imports System.ComponentModel
Imports System.Collections.Generic
Imports Microsoft.Office.Core
Imports CRUD

Friend Class ReportUploadControler

#Region "Instance Variables"

    ' Objects
    Private m_addin As AddinModule
    Private m_fact As New Facts
    Private m_dataset As ModelDataSet
    Private m_dataModificationsTracker As DataModificationsTracking
    Private m_acquisitionModel As AcquisitionModel
    Private m_factsStorage As New FactsStorage
    Private Shared m_reportUploadWorksheetEventHandler As ReportUploadWorksheetsEventHandler
    Friend m_associatedWorksheet As Excel.Worksheet
    Private m_errorMessagesUI As New StatusReportInterfaceUI

    ' Variables
    Private m_entityName As String
    Friend m_snapshotSuccessFlag As Boolean
    Friend m_autoCommitFlag As Boolean
    Private m_itemsHighlightFlag As Boolean
    Private m_uploadTimeStamp As Date
    Private m_uploadState As Boolean
    Private m_originalValue As Double
    Friend m_isUpdating As Boolean
    Private m_mustUpdateExcelWorksheetFromDataBase As Boolean
    Friend m_isReportReadyFlag As Boolean

#End Region

#Region "Initialize"

    Friend Sub New(ByRef inputAddIn As AddinModule)

        m_reportUploadWorksheetEventHandler = New ReportUploadWorksheetsEventHandler()
        m_addin = inputAddIn
        m_associatedWorksheet = GlobalVariables.APPS.ActiveSheet

        m_dataset = New ModelDataSet(m_associatedWorksheet)
        m_acquisitionModel = New AcquisitionModel(m_dataset)
        m_dataModificationsTracker = New DataModificationsTracking(m_dataset)

        AddHandler m_acquisitionModel.m_afterInputsDownloaded, AddressOf AfterDataBaseInputsDowloaded
        AddHandler m_acquisitionModel.m_afterOutputsComputed, AddressOf AfterOutputsComputed
        AddHandler m_fact.AfterUpdate, AddressOf AfterCommit
        AddHandler m_factsStorage.FactsDownloaded, AddressOf AfterRHFactsDownload

    End Sub

    ' Display the entity name and currency in the ribbons Corresponding edit boxes
    Friend Sub FillInFinancialSubmissionRibbonEntityAndCurrencyTB(ByRef p_entityName As String)

        m_addin.CurrentEntityTB.Text = p_entityName
        m_entityName = p_entityName
        Dim entity As EntityCurrency = GlobalVariables.EntityCurrencies.GetValue(p_entityName)
        If Not entity Is Nothing Then
            Dim currency As Currency = GlobalVariables.Currencies.GetValue(entity.CurrencyId)

            If currency Is Nothing Then Exit Sub
            m_addin.EntCurrTB.Text = currency.Name
        End If

    End Sub

#End Region

#Region "Ribbon Interface"

    Friend Sub UpdateRibbon()

        If m_dataset.m_EntityFlag = 1 Then FillInFinancialSubmissionRibbonEntityAndCurrencyTB(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).ElementAt(0).Value)
        ' Check that the Dataset is still the same ?

    End Sub

    Friend Sub DataSubmission()

        If m_dataset.m_globalOrientationFlag <> ModelDataSet.Orientations.ORIENTATION_ERROR Then
            m_errorMessagesUI.ClearBox()
            Select Case m_dataset.m_processFlag
                Case Account.AccountProcess.FINANCIAL : SubmitFinancialProcess()
                Case Account.AccountProcess.RH : SubmitPDCProcess()
            End Select
        Else
            GlobalVariables.SubmissionStatusButton.Image = 2
            m_errorMessagesUI.AddError("The worksheet recognition was not set up properly.")
            MsgBox("PPS Error tracking -> which flag error")
            m_uploadState = False
        End If

    End Sub

    Friend Sub HighlightItemsAndDataRegions()

        If m_itemsHighlightFlag = False Then
            m_dataModificationsTracker.HighlightItemsAndDataRanges()
            m_itemsHighlightFlag = True
        Else
            m_dataModificationsTracker.UnHighlightItemsAndDataRanges()
            m_itemsHighlightFlag = False
        End If

    End Sub

    Friend Sub ChangeCurrentEntity(ByRef entityname As String)

        Dim entityCellAddress As String = m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).ElementAt(0).Key
        m_associatedWorksheet.Range(entityCellAddress).Value2 = entityname
        m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY)(entityCellAddress) = entityname
        FillInFinancialSubmissionRibbonEntityAndCurrencyTB(entityname)

    End Sub

    Friend Sub DisplayUploadStatusAndErrorsUI()

        m_errorMessagesUI.Show()

    End Sub

    Friend Sub CloseInstance()

        '    On Error Resume Next
        m_dataModificationsTracker.TakeOffFormatsAndEnableConditionalFormatting()
        If Not m_dataset Is Nothing Then m_dataset = Nothing
        If Not m_dataModificationsTracker Is Nothing Then m_dataModificationsTracker = Nothing
        If Not m_acquisitionModel Is Nothing Then m_acquisitionModel = Nothing

        '   GlobalVariables.APPS.CellDragAndDrop = True
        RemoveHandler m_associatedWorksheet.Change, AddressOf m_reportUploadWorksheetEventHandler.Worksheet_Change
        RemoveHandler m_associatedWorksheet.BeforeRightClick, AddressOf m_reportUploadWorksheetEventHandler.Worksheet_BeforeRightClick
        RemoveHandler m_associatedWorksheet.SelectionChange, AddressOf m_reportUploadWorksheetEventHandler.Worksheet_SelectionChange
        If Not m_reportUploadWorksheetEventHandler Is Nothing Then m_reportUploadWorksheetEventHandler = Nothing
        'GlobalVariables.APPS.Interactive = True
        'GlobalVariables.APPS.ScreenUpdating = True

    End Sub

    Friend Sub RangeEdition()

        Dim RNGEDITION As New ManualRangesSelectionUI(Me, m_dataset)
        RNGEDITION.Show()

    End Sub

    ' (Financial process)
    Friend Sub UpdateAfterAnalysisAxisChanged(ByVal p_client_id As String, _
                                              ByVal p_product_id As String, _
                                              ByVal p_adjustment_id As String, _
                                              Optional ByRef p_update_inputs_from_DB As Boolean = False)

        m_isUpdating = True
        m_mustUpdateExcelWorksheetFromDataBase = p_update_inputs_from_DB
        m_dataModificationsTracker.DiscardModifications()

        If p_update_inputs_from_DB = True Then
            m_dataModificationsTracker.InitializeDataSetRegion()
            m_dataModificationsTracker.InitializeOutputsRegion()
        End If

        GlobalVariables.APPS.Interactive = True
        Select Case m_dataset.m_globalOrientationFlag
            Case ModelDataSet.Orientations.ACCOUNTS_PERIODS, _
                 ModelDataSet.Orientations.PERIODS_ACCOUNTS

                m_acquisitionModel.DownloadInputsFromServer({m_entityName}.ToList, _
                                                            p_client_id, _
                                                            p_product_id, _
                                                            p_adjustment_id)

            Case ModelDataSet.Orientations.ACCOUNTS_ENTITIES, _
                 ModelDataSet.Orientations.ENTITIES_ACCOUNTS, _
                 ModelDataSet.Orientations.ENTITIES_PERIODS, _
                 ModelDataSet.Orientations.PERIODS_ENTITIES

                Dim l_entitiesNamesList = m_dataset.m_dimensionsValueAddressDict(ModelDataSet.Dimension.ENTITY).Keys.ToList
                m_acquisitionModel.DownloadInputsFromServer(l_entitiesNamesList, _
                                                            p_client_id, _
                                                            p_product_id, _
                                                            p_adjustment_id)

        End Select

    End Sub

#End Region

#Region "Snapshot Interface"

    Friend Function RefreshSnapshot(ByRef p_updateInputsFlag As Boolean) As Boolean

        m_isReportReadyFlag = False
        ' Unformat if necessary 
        If m_dataModificationsTracker Is Nothing Then Return False
        If m_dataModificationsTracker.m_rangeHighlighter.FormattedCellsDictionnarySize > 0 Then
            m_dataModificationsTracker.m_rangeHighlighter.RevertToOriginalColors()
            m_itemsHighlightFlag = False
        End If

        If m_dataset.WsScreenshot() = True Then
            m_dataset.SnapshotWS()
            m_dataset.GetOrientations()
            m_dataModificationsTracker.InitializeDataSetRegion()
            m_dataModificationsTracker.InitializeOutputsRegion()
            Select Case m_dataset.m_processFlag
                Case Account.AccountProcess.FINANCIAL : Return FinancialProcessSnapshotRefreshment(p_updateInputsFlag)
                Case Account.AccountProcess.RH : Return PDCProcessSnapshotRefreshment()
            End Select
        End If
        Return False

    End Function

    Private Function FinancialProcessSnapshotRefreshment(ByRef p_updateInputsFlag As Boolean) As Boolean

        If m_dataset.m_globalOrientationFlag <> ModelDataSet.Orientations.ORIENTATION_ERROR _
        AndAlso m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).Count > 0 Then

            m_snapshotSuccessFlag = True
            FillInFinancialSubmissionRibbonEntityAndCurrencyTB(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).ElementAt(0).Value)
            HighlightItemsAndDataRegions()
            m_addin.SelectFinancialDropDownSubmissionButtons(m_dataset.AxisElemIdentify(AxisType.Client), _
                                                    m_dataset.AxisElemIdentify(AxisType.Product), _
                                                    m_dataset.AxisElemIdentify(AxisType.Adjustment))
            UpdateAfterAnalysisAxisChanged(GlobalVariables.ClientsIDDropDown.SelectedItemId, _
                                           GlobalVariables.ProductsIDDropDown.SelectedItemId, _
                                           GlobalVariables.AdjustmentIDDropDown.SelectedItemId, _
                                           p_updateInputsFlag)

            ' Associate worksheet if not already associated 
            If m_reportUploadWorksheetEventHandler.m_excelWorksheet Is Nothing Then
                m_reportUploadWorksheetEventHandler.AssociateSubmissionWSController(Me, m_dataset, m_acquisitionModel, m_dataModificationsTracker, m_associatedWorksheet)
            ElseIf Not m_reportUploadWorksheetEventHandler.m_excelWorksheet.Name Is m_associatedWorksheet Then
                m_reportUploadWorksheetEventHandler.AssociateSubmissionWSController(Me, m_dataset, m_acquisitionModel, m_dataModificationsTracker, m_associatedWorksheet)
            End If
        Else
            SnapshotErrorGeneration(Account.AccountProcess.FINANCIAL)
            Return False
        End If
        m_addin.ModifySubmissionControlsStatus(m_snapshotSuccessFlag)
        Return True

    End Function

    Private Function PDCProcessSnapshotRefreshment() As Boolean

        If m_dataset.m_globalOrientationFlag <> ModelDataSet.Orientations.ORIENTATION_ERROR Then
            If m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).Count = 0 Then
                MsgBox(Local.GetValue("upload.msg_entity_not_found"))
            Else
                m_addin.SetPDCSubmissionRibbonEntityAndAccountName(m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ENTITY).ElementAt(0).Value, _
                                                         m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT).ElementAt(0).Value)
            End If
            m_snapshotSuccessFlag = True
            HighlightItemsAndDataRegions()
            ' Associate worksheet if not already associated 
            If m_reportUploadWorksheetEventHandler.m_excelWorksheet Is Nothing Then
                m_reportUploadWorksheetEventHandler.AssociateSubmissionWSController(Me, m_dataset, m_acquisitionModel, m_dataModificationsTracker, m_associatedWorksheet)
            ElseIf Not m_reportUploadWorksheetEventHandler.m_excelWorksheet.Name Is m_associatedWorksheet Then
                m_reportUploadWorksheetEventHandler.AssociateSubmissionWSController(Me, m_dataset, m_acquisitionModel, m_dataModificationsTracker, m_associatedWorksheet)
            End If

            m_dataset.RegisterDimensionsToCellDictionary()

            ' RH Cloud data loading
            m_factsStorage.LoadRHFacts({m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT).ElementAt(0).Value}.ToList, _
                                       m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PRODUCT).Values.ToList, _
                                       m_dataset.m_currentVersionId, _
                                       m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD).Values.Min, _
                                       m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.PERIOD).Values.Max)

            Return True
        Else
            SnapshotErrorGeneration(Account.AccountProcess.RH)
            m_addin.ModifySubmissionControlsStatus(m_snapshotSuccessFlag)
            GlobalVariables.APPS.Interactive = True
            GlobalVariables.APPS.ScreenUpdating = True
            m_isUpdating = False
            Return False
        End If


    End Function

    Friend Function GetProcess() As Account.AccountProcess
        Return m_dataset.m_processFlag
    End Function

#End Region

#Region "Excel Controllers Interface"

    ' Différencier les process Financial et PDC -> pas de calculs
    Friend Sub UpdateModelFromExcelUpdate(ByRef p_entityName As String, _
                                          ByRef p_accountName As String, _
                                          ByRef p_period As Integer, _
                                          ByVal p_value As Double, _
                                          ByRef p_cellAddress As String)

        m_acquisitionModel.ValuesDictionariesUpdate(p_entityName, p_accountName, p_period, p_value)
        RegisterModification(p_cellAddress)

    End Sub

    Friend Sub RegisterModification(ByRef p_cellAddress As String)
        m_dataModificationsTracker.RegisterModification(p_cellAddress)
    End Sub

    Friend Sub updateInputs()

        m_isUpdating = True
        GlobalVariables.APPS.ScreenUpdating = False
        GlobalVariables.APPS.Interactive = False
        m_reportUploadWorksheetEventHandler.UpdateInputsOnWS()
        GlobalVariables.APPS.ScreenUpdating = True
        GlobalVariables.APPS.ScreenUpdating = False
        'isUpdating = False

    End Sub

    Friend Sub UpdateCalculatedItems()

        m_isUpdating = True
        GlobalVariables.APPS.Interactive = True
        m_acquisitionModel.ComputeCalculatedItems()

    End Sub

#End Region

#Region "Model Events Listening"

    Private Sub AfterDataBaseInputsDowloaded(ByRef p_status As Boolean)

        If p_status = False Then
            m_addin.ClearSubmissionMode(Me)
            Exit Sub
        End If

        On Error GoTo errorHandler
        GlobalVariables.APPS.Interactive = False
        m_dataset.RegisterDimensionsToCellDictionary()
        m_dataModificationsTracker.HighlightsFPIOutputPart()
        If m_mustUpdateExcelWorksheetFromDataBase = True Then
            updateInputs()
        End If
        m_dataset.RegisterDataSetCellsValues()

        m_dataModificationsTracker.IdentifyFinancialDifferencesBtwDataSetAndDB(m_acquisitionModel.m_databaseInputsDictionary)

        ' update calculated items only if several accounts
        Select Case m_dataset.m_globalOrientationFlag
            Case ModelDataSet.Orientations.ACCOUNTS_PERIODS, _
                 ModelDataSet.Orientations.PERIODS_ACCOUNTS, _
                 ModelDataSet.Orientations.ACCOUNTS_ENTITIES, _
                 ModelDataSet.Orientations.ENTITIES_ACCOUNTS
                UpdateCalculatedItems()
        End Select
        m_isUpdating = False
        ' Update DGV in acquisitionInterface 
        GlobalVariables.APPS.Interactive = True

errorHandler:
        GlobalVariables.APPS.Interactive = True
        GlobalVariables.APPS.ScreenUpdating = True
        m_isUpdating = False
        Exit Sub

    End Sub

    Private Sub AfterRHFactsDownload(ByRef p_status As Boolean)

        If p_status = True Then
            ' Initial differencies identifications
            Dim l_RHaccountName As String = m_dataset.m_dimensionsAddressValueDict(ModelDataSet.Dimension.ACCOUNT).ElementAt(0).Value
            m_dataModificationsTracker.IdentifyRHDifferencesBtwDataSetAndDB(l_RHaccountName, m_factsStorage.m_FactsDict(l_RHaccountName))
        End If
        m_addin.ModifySubmissionControlsStatus(m_snapshotSuccessFlag)
        GlobalVariables.APPS.Interactive = True
        GlobalVariables.APPS.ScreenUpdating = True
        m_isUpdating = False

    End Sub

    Private Sub AfterOutputsComputed(ByRef p_entitiesName() As String)

        If p_entitiesName Is Nothing Then
            MsgBox(Local.GetValue("upload.msg_report_error"))
            m_isUpdating = False
            m_addin.ClearSubmissionMode(Me)
            Exit Sub
        End If

        On Error Resume Next
        SyncLock (m_reportUploadWorksheetEventHandler)
            m_isUpdating = True
            GlobalVariables.APPS.Interactive = False
            GlobalVariables.APPS.ScreenUpdating = False
            m_reportUploadWorksheetEventHandler.UpdateCalculatedItemsOnWS(p_entitiesName)
            GlobalVariables.APPS.ScreenUpdating = True
            GlobalVariables.APPS.Interactive = True
            m_isUpdating = False
        End SyncLock

    End Sub

#End Region

#Region "Data Submission"

    Private Sub SubmitFinancialProcess()

        Dim factsList As New List(Of Hashtable)
        Dim cellsAddresses As List(Of String) = m_dataModificationsTracker.GetModificationsListCopy
        For Each cellAddress In cellsAddresses
            ' Implies type of cell checked before -> only double -> check if we can enter anything else !!!
            Dim ht As New Hashtable()

            ht(ENTITY_ID_VARIABLE) = GlobalVariables.AxisElems.GetValueId(AxisType.Entities, m_dataset.m_datasetCellDimensionsDictionary(cellAddress).m_entityName)
            ht(ACCOUNT_ID_VARIABLE) = GlobalVariables.Accounts.GetValueId(m_dataset.m_datasetCellDimensionsDictionary(cellAddress).m_accountName)
            ht(PERIOD_VARIABLE) = m_dataset.m_datasetCellDimensionsDictionary(cellAddress).m_period
            ht(VERSION_ID_VARIABLE) = m_acquisitionModel.m_currentVersionId
            ht(CLIENT_ID_VARIABLE) = GlobalVariables.ClientsIDDropDown.SelectedItemId
            ht(PRODUCT_ID_VARIABLE) = GlobalVariables.ProductsIDDropDown.SelectedItemId
            ht(ADJUSTMENT_ID_VARIABLE) = GlobalVariables.AdjustmentIDDropDown.SelectedItemId
            ht(VALUE_VARIABLE) = GlobalVariables.APPS.ActiveSheet.range(cellAddress).value

            factsList.Add(ht)
        Next
        m_fact.CMSG_UPDATE_FACT_LIST(factsList, cellsAddresses)

    End Sub

    Private Sub SubmitPDCProcess()

        Dim factsList As New List(Of Hashtable)
        Dim cellsAddresses As List(Of String) = m_dataModificationsTracker.GetModificationsListCopy
        For Each cellAddress In cellsAddresses
            Dim ht As New Hashtable()

            ' attention à faire :
            '   -> create client if it doesn't exists (server side ?)
            '   -> check again that client is valid and a string

            ht(ENTITY_ID_VARIABLE) = 50 ' stub !!! 
            ht(ACCOUNT_ID_VARIABLE) = GlobalVariables.Accounts.GetValueId(m_dataset.m_datasetCellDimensionsDictionary(cellAddress).m_accountName)
            ht(PERIOD_VARIABLE) = m_dataset.m_datasetCellDimensionsDictionary(cellAddress).m_period
            ht(VERSION_ID_VARIABLE) = m_dataset.m_currentVersionId

            ' quid : clients creation

            ht(CLIENT_ID_VARIABLE) = 8 'GlobalVariables.APPS.ActiveSheet.range(cellAddress).value
            ht(PRODUCT_ID_VARIABLE) = GlobalVariables.AxisElems.GetValue(AxisType.Product, m_dataset.m_datasetCellDimensionsDictionary(cellAddress).m_product).Id
            ht(ADJUSTMENT_ID_VARIABLE) = CUInt(CRUD.AxisType.Adjustment)
            ht(VALUE_VARIABLE) = 1      ' * % working time of the consultant

            factsList.Add(ht)
        Next
        m_fact.CMSG_UPDATE_FACT_LIST(factsList, cellsAddresses)

    End Sub

    Private Sub AfterCommit(ByRef status As Boolean, ByRef commitResults As Dictionary(Of String, ErrorMessage))

        If status = True Then
            Dim HasError As Boolean = False
            m_errorMessagesUI.AfterCommit(m_dataset, commitResults)
            For Each result In commitResults
                If commitResults(result.Key) = ErrorMessage.SUCCESS Then
                    m_dataModificationsTracker.UnregisterSingleModification(result.Key)
                Else
                    HasError = True
                End If
            Next
            If HasError = False Then
                m_uploadState = True
                GlobalVariables.SubmissionStatusButton.Image = 1
            Else
                m_uploadState = False
                GlobalVariables.SubmissionStatusButton.Image = 2
            End If
        Else
            MsgBox("Commit failed. Check network connection and try again." & Chr(13) & "If the error persists, please contact your administrator or the Financial BI team.")
        End If

    End Sub

#End Region

#Region "Utilities"

    Friend Function GetPeriodsList() As Int32()
        Return m_acquisitionModel.m_currentPeriodList
    End Function

    Private Sub SnapshotErrorGeneration(ByRef p_process As Account.AccountProcess)

        Dim l_errorStr As String
        Select Case p_process
            Case Account.AccountProcess.FINANCIAL : l_errorStr = IdentifyFinancialSnapshotFlagsErrors()
            Case Account.AccountProcess.RH : l_errorStr = IdentifyPDCSnapshotFlagsErrors()
        End Select
        MsgBox(Local.GetValue("upload.msg_snapshot_error") & Chr(13) & l_errorStr)
        m_snapshotSuccessFlag = False

    End Sub

    Private Function IdentifyFinancialSnapshotFlagsErrors() As String

        Dim tmpStr As String = ""
        If m_dataset.m_entityFlag = ModelDataSet.SnapshotResult.ZERO Then tmpStr = "  - " & Local.GetValue("general.entities") & Chr(13)
        If m_dataset.m_periodFlag = ModelDataSet.SnapshotResult.ZERO Then tmpStr = tmpStr & "  - " & Local.GetValue("general.periods") & Chr(13)
        If m_dataset.m_accountFlag = ModelDataSet.SnapshotResult.ZERO Then tmpStr = tmpStr & "  - " & Local.GetValue("general.accounts")
        If m_dataset.m_entitiesOrientationFlag = ModelDataSet.Alignment.UNCLEAR Then tmpStr = "  - " & Local.GetValue("upload.msg_entities_orientation_unclear") & Chr(13)
        If m_dataset.m_periodsOrientationFlag = ModelDataSet.Alignment.UNCLEAR Then tmpStr = tmpStr & "  - " & Local.GetValue("upload.msg_periods_orientation_unclear") & Chr(13)
        If m_dataset.m_accountsOrientationFlag = ModelDataSet.Alignment.UNCLEAR Then tmpStr = tmpStr & "  - " & Local.GetValue("upload.msg_accounts_orientation_unclear")
        Return tmpStr

    End Function

    Private Function IdentifyPDCSnapshotFlagsErrors() As String

        Dim tmpStr As String = ""
        If m_dataset.m_productFlag = ModelDataSet.SnapshotResult.ZERO Then tmpStr = tmpStr & "  - " & Local.GetValue("general.products") & Chr(13)
        If m_dataset.m_entityFlag = ModelDataSet.SnapshotResult.ZERO Then tmpStr = "  - " & Local.GetValue("general.entities") & Chr(13)
        If m_dataset.m_periodFlag = ModelDataSet.SnapshotResult.ZERO Then tmpStr = tmpStr & "  - " & Local.GetValue("general.periods") & Chr(13)
        If m_dataset.m_periodsOrientationFlag = ModelDataSet.Alignment.UNCLEAR Then tmpStr = tmpStr & "  - " & Local.GetValue("upload.msg_periods_orientation_unclear") & Chr(13)
        If m_dataset.m_productsOrientationFlag = ModelDataSet.Alignment.UNCLEAR Then tmpStr = tmpStr & "  - " & Local.GetValue("upload.msg_products_orientation_unclear") & Chr(13)
        Return tmpStr

    End Function

    Friend Function IsCurrentController() As Boolean
        Return m_addin.IsCurrentReportUploadController(Me)
    End Function

    Friend Sub ActivateReportUploadController()
        m_addin.ActivateReportUploadController(Me)
    End Sub

#End Region


End Class
