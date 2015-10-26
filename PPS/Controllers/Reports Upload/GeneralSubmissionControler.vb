' CGeneralReportSubmissionControler.vb
' 
' Manages the whole submission process:
'       - Dataset                       ok
'       - SubmissionInterface           ok
'       - DatasetRibbon                 ok
'       - DataModificationsTracking     ok
'       - StatusReportInterface         ok
'       - DataHighlighter               ok
'       - Acquisition Interface         ok        
'
'
' To do:  
'       - changement de version -> changement du display (cf. periods <>)!!
'
'       - case where status = false -> modified to true...!!
'       - Dans le cas où le snapshotStatus est false -> si une action le fait passer en true ->enable buttons
'       -                                            >> actions = modifications sur les inputs -> dans addin
'       - CellChanged -> no update for 2 orientations cases yet (account flag =1)
'       - Choix d'un asset dans le cas où seul Entityflag = 0
'       - Manage the flags checks after Dataset returned (can use read only array of valid global orientation flag)
'       -  flag indicating GRSControler is associated to the WS and no flag errors
'       -> manages all flag cases -> display errors and/or what to do in case of not okd 
'       - must allow entity selection button only if entity flag = 1 at Dataset level
'       - Update Single Value -> check for period on BS inputs necessary
'       - entity selection on flag error = only entity=0
'
'
' Known bugs:
'       - 
'
'
'
' Author: Julien Monnereau
' Last modified: 19/09/2015


Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports VIBlend.WinForms.DataGridView
Imports System.Linq
Imports AddinExpress.MSO
Imports System.Collections
Imports System.ComponentModel
Imports System.Collections.Generic
Imports Microsoft.Office.Core


Friend Class GeneralSubmissionControler


#Region "Instance Variables"

    ' Objects
    Private m_addin As AddinModule
    Private m_fact As New Facts
    Private m_dataset As ModelDataSet
    Private m_dataModificationsTracker As DataModificationsTracking
    Private m_acquisitionModel As AcquisitionModel
    Private m_submissionWSController As SubmissionWSController
    Friend m_associatedWorksheet As Excel.Worksheet
    Friend m_worksheetsComboboxMenuItem As ADXRibbonItem
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

    Friend Sub New(ByRef inputWSCB As ADXRibbonItem, _
                   ByRef inputAddIn As AddinModule)

        m_addin = inputAddIn
        m_worksheetsComboboxMenuItem = inputWSCB
        m_associatedWorksheet = GlobalVariables.APPS.ActiveSheet

        m_dataset = New ModelDataSet(m_associatedWorksheet)
        m_acquisitionModel = New AcquisitionModel(m_dataset)
        m_dataModificationsTracker = New DataModificationsTracking(m_dataset)
   
        AddHandler m_acquisitionModel.AfterInputsDownloaded, AddressOf AfterDataBaseInputsDowloaded
        AddHandler m_acquisitionModel.AfterOutputsComputed, AddressOf AfterOutputsComputed
        AddHandler m_fact.AfterUpdate, AddressOf AfterCommit

    End Sub

    ' Display the entity name and currency in the ribbons Corresponding edit boxes
    Friend Sub FillInEntityAndCurrencyTB(ByRef p_entityName As String)

        m_addin.CurrentEntityTB.Text = p_entityName
        m_entityName = p_entityName
        Dim entityId As Int32 = GlobalVariables.Entities.GetEntityId(p_entityName)
        If entityId <> 0 Then
            Dim currencyId As Int32 = GlobalVariables.Entities.entities_hash(entityId)(ENTITIES_CURRENCY_VARIABLE)
            m_addin.EntCurrTB.Text = GlobalVariables.Currencies.currencies_hash(currencyId)(NAME_VARIABLE)
        End If

    End Sub


#End Region


#Region "Ribbon Interface"

    Friend Sub UpdateRibbon()

        If m_dataset.m_EntityFlag = 1 Then FillInEntityAndCurrencyTB(m_dataset.m_entitiesAddressValuesDictionary.ElementAt(0).Value)
        ' Check that the Dataset is still the same ?

    End Sub

    Friend Sub DataSubmission()

        If m_dataset.m_globalOrientationFlag <> ORIENTATION_ERROR_FLAG Then
            m_errorMessagesUI.ClearBox()
            Submit()
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

        Dim entityCellAddress As String = m_dataset.m_entitiesAddressValuesDictionary.ElementAt(0).Key
        m_associatedWorksheet.Range(entityCellAddress).Value2 = entityname
        m_dataset.m_entitiesAddressValuesDictionary(entityCellAddress) = entityname
        FillInEntityAndCurrencyTB(entityname)

    End Sub

    Friend Sub DisplayUploadStatusAndErrorsUI()

        m_errorMessagesUI.Show()

    End Sub

    Friend Sub CloseInstance()

        m_dataModificationsTracker.TakeOffFormats()
        If Not m_dataset Is Nothing Then m_dataset = Nothing
        If Not m_dataModificationsTracker Is Nothing Then m_dataModificationsTracker = Nothing
        If Not m_acquisitionModel Is Nothing Then m_acquisitionModel = Nothing

        On Error Resume Next
        RemoveHandler m_associatedWorksheet.Change, AddressOf m_submissionWSController.Worksheet_Change
        RemoveHandler m_associatedWorksheet.BeforeRightClick, AddressOf m_submissionWSController.Worksheet_BeforeRightClick
        RemoveHandler m_associatedWorksheet.SelectionChange, AddressOf m_submissionWSController.Worksheet_SelectionChange
        If Not m_submissionWSController Is Nothing Then m_submissionWSController = Nothing
        '  associatedWorksheet.Unprotect()

    End Sub

    Friend Sub RangeEdition()

        Dim RNGEDITION As New ManualRangesSelectionUI(Me, m_dataset)
        RNGEDITION.Show()

    End Sub

    Friend Function RefreshSnapshot(ByRef update_inputs As Boolean) As Boolean

        m_isReportReadyFlag = False
        ' Unformat if necessary 
        If m_dataModificationsTracker.m_rangeHighlighter.FormattedCellsDictionnarySize > 0 Then
            m_dataModificationsTracker.m_rangeHighlighter.RevertToOriginalColors()
            m_itemsHighlightFlag = False
        End If

        If m_dataset.WsScreenshot() = True Then
            m_dataset.SnapshotWS()
            m_dataset.getOrientations()

            ' needed ,??????!!!!!!!! priority normal
            m_dataModificationsTracker.InitializeDataSetRegion()
            m_dataModificationsTracker.InitializeOutputsRegion()
            
            If m_dataset.m_globalOrientationFlag <> ORIENTATION_ERROR_FLAG _
            AndAlso m_dataset.m_entitiesAddressValuesDictionary.Count > 0 Then

                '  Dataset.getDataSet()
                m_snapshotSuccessFlag = True
                FillInEntityAndCurrencyTB(m_dataset.m_entitiesAddressValuesDictionary.ElementAt(0).Value)
                m_submissionWSController = New SubmissionWSController(Me, m_dataset, m_acquisitionModel, m_dataModificationsTracker)
                HighlightItemsAndDataRegions()
                UpdateAfterAnalysisAxisChanged(GlobalVariables.ClientsIDDropDown.SelectedItemId, _
                                               GlobalVariables.ProductsIDDropDown.SelectedItemId, _
                                               GlobalVariables.AdjustmentIDDropDown.SelectedItemId, _
                                               update_inputs)
                ' Associate worksheet if not already associated 
                If m_submissionWSController.m_excelWorksheet Is Nothing Then
                    m_submissionWSController.AssociateWS(m_associatedWorksheet)
                ElseIf Not m_submissionWSController.m_excelWorksheet.Name Is m_associatedWorksheet Then
                    m_submissionWSController.AssociateWS(m_associatedWorksheet)
                End If
            Else
                SnapshotError()
                Return False
            End If
            m_addin.modifySubmissionControlsStatus(m_snapshotSuccessFlag)
        Else
            Return False
        End If
        Return True

    End Function

    Friend Sub UpdateAfterAnalysisAxisChanged(ByVal client_id As String, _
                                              ByVal product_id As String, _
                                              ByVal adjustment_id As String, _
                                              Optional ByRef update_inputs_from_DB As Boolean = False)

        m_isUpdating = True
        m_mustUpdateExcelWorksheetFromDataBase = update_inputs_from_DB
        m_dataModificationsTracker.DiscardModifications()
        ' PB: Ceci ne marche que pour le cas orientation "AcDa"  !!! 
        ' option: select case on orientation 
        ' (possibility to download inputs from multiple entities => function ready in model)
        ' priority normal => V2

        m_acquisitionModel.downloadDBInputs(m_entityName, _
                               client_id, _
                               product_id, _
                               adjustment_id)

    End Sub

#End Region


#Region "Excel-DGV Controllers Interface"

    Friend Sub UpdateModelFromExcelUpdate(ByRef entityName As String, _
                                          ByRef accountName As String, _
                                            ByRef periodInt As Integer, _
                                            ByVal value As Double, _
                                            ByRef cellAddress As String)

        m_acquisitionModel.ValuesDictionariesUpdate(entityName, accountName, periodInt, value)
        m_dataModificationsTracker.RegisterModification(cellAddress)

    End Sub

    Friend Sub updateInputs()

        m_isUpdating = True
        m_submissionWSController.updateInputsOnWS()
        GlobalVariables.APPS.ScreenUpdating = True
        GlobalVariables.APPS.ScreenUpdating = False
        'isUpdating = False

    End Sub

    Friend Sub UpdateCalculatedItems(ByRef entityName As String)

        m_isUpdating = True
        GlobalVariables.APPS.Interactive = False
        m_acquisitionModel.ComputeCalculatedItems(entityName)

    End Sub

#End Region


#Region "Model Events Listening"

    Private Sub AfterDataBaseInputsDowloaded()

        On Error GoTo errorHandler
        m_dataset.RegisterDimensionsToCellDictionary()
        m_dataModificationsTracker.HighlightsFPIOutputPart()
        If m_mustUpdateExcelWorksheetFromDataBase = True Then
            updateInputs()
        End If
        m_dataset.RegisterDataSetCellsValues()
        m_dataModificationsTracker.IdentifyDifferencesBtwDataSetAndDB(m_acquisitionModel.dataBaseInputsDictionary)
        UpdateCalculatedItems(m_entityName)
        m_isUpdating = False
        ' Update DGV in acquisitionInterface !!

errorHandler:
        Exit Sub

    End Sub

    Private Sub AfterOutputsComputed(ByRef entityName As String)

        If entityName = "" Then
            MsgBox("An error occured in the computation of this report. You may have not been granted access to this version or entity.Please contact your administrator.")
            GlobalVariables.APPS.ScreenUpdating = True
            GlobalVariables.APPS.Interactive = True
            m_isUpdating = False
            Exit Sub
        End If

        On Error Resume Next
        m_isUpdating = True
        GlobalVariables.APPS.Interactive = False
        GlobalVariables.APPS.ScreenUpdating = False
        m_submissionWSController.updateCalculatedItemsOnWS(entityName)
        GlobalVariables.APPS.ScreenUpdating = True
        GlobalVariables.APPS.Interactive = True
        m_isUpdating = False

    End Sub


#End Region


#Region "Data Submission"

    Private Sub Submit()

        Dim factsList As New List(Of Hashtable)
        Dim cellsAddresses As List(Of String) = m_dataModificationsTracker.GetModificationsListCopy
        For Each cellAddress In cellsAddresses
            ' Implies type of cell checked before -> only double -> check if we can enter anything else !!!
            Dim ht As New Hashtable()
            ht(ENTITY_ID_VARIABLE) = m_dataset.m_entitiesNameIdDictionary(m_dataset.m_datasetCellDimensionsDictionary(cellAddress).m_entityName)
            ht(ACCOUNT_ID_VARIABLE) = m_dataset.m_accountsNameIdDictionary(m_dataset.m_datasetCellDimensionsDictionary(cellAddress).m_accountName)
            ht(PERIOD_VARIABLE) = m_dataset.m_datasetCellDimensionsDictionary(cellAddress).m_period
            ht(VERSION_ID_VARIABLE) = m_acquisitionModel.current_version_id
            ht(CLIENT_ID_VARIABLE) = GlobalVariables.ClientsIDDropDown.SelectedItemId
            ht(PRODUCT_ID_VARIABLE) = GlobalVariables.ProductsIDDropDown.SelectedItemId
            ht(ADJUSTMENT_ID_VARIABLE) = GlobalVariables.AdjustmentIDDropDown.SelectedItemId
            ht(VALUE_VARIABLE) = GlobalVariables.APPS.ActiveSheet.range(cellAddress).value

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

        Return m_acquisitionModel.currentPeriodList

    End Function

    Friend Function GetTimeConfig() As String

        Return GlobalVariables.Versions.versions_hash(m_acquisitionModel.current_version_id)(VERSIONS_TIME_CONFIG_VARIABLE)

    End Function

    Private Sub SnapshotError()

        If m_dataset.m_EntityFlag = 0 AndAlso _
        m_dataset.m_accountFlag <> 0 AndAlso _
        m_dataset.m_dateFlag <> 0 Then
            ' Initialize AcVPe or PeVAc + necessary to choose an Entity
            ' Need a call from entity selection (which will launch initialize and addHandler, if cancel suppr GRS)
            ' !! priority normal !
        Else
            Dim errorStr As String = IdentifyFlagsErrors()
            MsgBox("The Snapshot was unsuccessful because the following dimensions not found on the Worksheet: " + Chr(13) + _
                   errorStr)
        End If
        m_snapshotSuccessFlag = False

    End Sub

    Private Function IdentifyFlagsErrors() As String

        Dim tmpStr As String = ""
        If m_dataset.m_EntityFlag = 0 Then tmpStr = "  - Entities" + Chr(13)
        If m_dataset.m_dateFlag = 0 Then tmpStr = tmpStr & "  - Periods" + Chr(13)
        If m_dataset.m_accountFlag = 0 Then tmpStr = tmpStr & "  - Accounts"
        Return tmpStr

    End Function

#End Region


End Class
