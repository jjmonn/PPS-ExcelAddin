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
' Last modified: 28/08/2015


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
    Private ADDIN As AddinModule
    Private Dataset As ModelDataSet
    Private DataModificationsTracker As DataModificationsTracking
    Private Model As AcquisitionModel
    Private SubmissionWSController As SubmissionWSController
    Friend associatedWorksheet As Excel.Worksheet
    Friend wsComboboxMenuItem As ADXRibbonItem
    Friend PBar As PBarUI
    Private BCKGW As New BackgroundWorker

    ' Variables
    Private current_entity_name As String
    Friend snapshotSuccess As Boolean
    Friend autoCommitFlag As Boolean
    Private itemsHighlightFlag As Boolean
    Private errorsList As New List(Of String)
    Private uploadTimeStamp As Date
    Private uploadState As Boolean
    Private originalValue As Double
    Friend isUpdating As Boolean
    Private mustUpdateExcelWorksheetFromDataBase As Boolean

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputWSCB As ADXRibbonItem, _
                   ByRef inputAddIn As AddinModule)

        ADDIN = inputAddIn
        wsComboboxMenuItem = inputWSCB
        associatedWorksheet = GlobalVariables.APPS.ActiveSheet

        Dataset = New ModelDataSet(associatedWorksheet)
        Model = New AcquisitionModel(Dataset)
        DataModificationsTracker = New DataModificationsTracking(Dataset)

        BCKGW.WorkerReportsProgress = True
        AddHandler Model.AfterInputsDownloaded, AddressOf AfterDataBaseInputsDowloaded
        AddHandler Model.AfterOutputsComputed, AddressOf AfterOutputsComputed
        AddHandler BCKGW.DoWork, AddressOf BCKGW_DoWork
        AddHandler BCKGW.RunWorkerCompleted, AddressOf BCKGW_RunWorkerCompleted
        AddHandler BCKGW.ProgressChanged, AddressOf BCKGW_ProgressChanged

    End Sub

    '' Snapshot WS and initializes ACQMODEL accordingly
    'Friend Sub LaunchDatasetSnapshotAndAssociateModel(ByRef update_inputs As Boolean)

    '    If Not Dataset.GlobalScreenShot Is Nothing Then
    '        Dataset.SnapshotWS()
    '        Dataset.getOrientations()
    '        ' Dataset.RefreshAll -> possible evolution (take off refreshdatabatch in addin and set excel formatting here)
    '        DataModificationsTracker.InitializeDataSetRegion()
    '        DataModificationsTracker.InitializeOutputsRegion()

    '        If Dataset.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG _
    '        AndAlso Dataset.EntitiesAddressValuesDictionary.Count > 0 Then

    '            ' Attention crash si entity not identified !!! priority high

    '            snapshotSuccess = True
    '            FillInEntityAndCurrencyTB(Dataset.EntitiesAddressValuesDictionary.ElementAt(0).Value)
    '            SubmissionWSController = New SubmissionWSController(Me, Dataset, Model, DataModificationsTracker)
    '            HighlightItemsAndDataRegions()
    '            UpdateAfterAnalysisAxisChanged(GlobalVariables.ClientsIDDropDown.SelectedItemId, _
    '                                           GlobalVariables.ProductsIDDropDown.SelectedItemId, _
    '                                           GlobalVariables.AdjustmentIDDropDown.SelectedItemId, _
    '                                           update_inputs)
    '            SubmissionWSController.AssociateWS(associatedWorksheet)
    '        Else
    '            SnapshotError()
    '        End If
    '        ADDIN.modifySubmissionControlsStatus(snapshotSuccess)
    '    Else
    '        MsgBox("Empty Worksheet")
    '    End If

    'End Sub

    '' General Initialize and Set up display Ribbon
    'Private Sub RefreshDataSetAndSetUpDisplay()

    '    DataModificationsTracker.IdentifyDifferencesBtwDataSetAndDB(Model.dataBaseInputsDictionary)
    '    If snapshotSuccess = False Then
    '        snapshotSuccess = True
    '        ADDIN.GetRibbonControlEnabled(True)
    '    End If

    'End Sub


    ' Display the entity name and currency in the ribbons Corresponding edit boxes
    Friend Sub FillInEntityAndCurrencyTB(ByRef entity As String)

        ADDIN.CurrentEntityTB.Text = entity
        Dim entitiesNameCurrDic As Hashtable = GlobalVariables.Entities.GetEntitiesDictionary(NAME_VARIABLE, ENTITIES_CURRENCY_VARIABLE)
        ADDIN.EntCurrTB.Text = entitiesNameCurrDic(entity)
        current_entity_name = entity

    End Sub

    
#End Region


#Region "Ribbon Interface"

    Friend Sub UpdateRibbon()

        If Dataset.pAssetFlag = 1 Then FillInEntityAndCurrencyTB(Dataset.EntitiesAddressValuesDictionary.ElementAt(0).Value)
        ' Check that the Dataset is still the same ?

    End Sub

    Friend Sub Submit()

        If Dataset.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG Then
            errorsList.Clear()
            PBar = New PBarUI
            PBar.ProgressBarControl1.Launch(1, DataModificationsTracker.modifiedCellsList.Count())
            PBar.Show()
            BCKGW.RunWorkerAsync()
        Else
            GlobalVariables.SubmissionStatusButton.Image = 2
            errorsList.Add("The worksheet recognition was not set up properly.")
            MsgBox("PPS Error tracking -> which flag error")
            uploadState = False
        End If

    End Sub

    Friend Sub HighlightItemsAndDataRegions()

        If itemsHighlightFlag = False Then
            DataModificationsTracker.HighlightItemsAndDataRanges()
            itemsHighlightFlag = True
        Else
            DataModificationsTracker.UnHighlightItemsAndDataRanges()
            itemsHighlightFlag = False
        End If

    End Sub

    Friend Sub ChangeCurrentEntity(ByRef entityname As String)

        Dim entityCellAddress As String = Dataset.EntitiesAddressValuesDictionary.ElementAt(0).Key
        associatedWorksheet.Range(entityCellAddress).Value2 = entityname
        Dataset.EntitiesAddressValuesDictionary(entityCellAddress) = entityname
        FillInEntityAndCurrencyTB(entityname)

    End Sub

    Friend Sub DisplayUploadStatusAndErrorsUI()

        Dim UploadStatusUI As New UploadingHistoryUI(uploadState, _
                                                     uploadTimeStamp, _
                                                     errorsList)
        UploadStatusUI.Show()

    End Sub

    Friend Sub CloseInstance()

        DataModificationsTracker.TakeOffFormats()
        If Not Dataset Is Nothing Then Dataset = Nothing
        If Not DataModificationsTracker Is Nothing Then DataModificationsTracker = Nothing
        If Not Model Is Nothing Then Model = Nothing
       
        On Error Resume Next
        RemoveHandler associatedWorksheet.Change, AddressOf SubmissionWSController.Worksheet_Change
        RemoveHandler associatedWorksheet.BeforeRightClick, AddressOf SubmissionWSController.Worksheet_BeforeRightClick
        If Not SubmissionWSController Is Nothing Then SubmissionWSController = Nothing
        '  associatedWorksheet.Unprotect()

    End Sub

    Friend Sub RangeEdition()

        Dim RNGEDITION As New ManualRangesSelectionUI(Me, Dataset)
        RNGEDITION.Show()

    End Sub

    Friend Sub RefreshSnapshot(ByRef update_inputs As Boolean)

        ' Unformat if necessary 
        If DataModificationsTracker.RangeHighlighter.FormattedCellsDictionnarySize > 0 Then
            DataModificationsTracker.RangeHighlighter.RevertToOriginalColors()
            itemsHighlightFlag = False
        End If

        If Dataset.WsScreenshot() = True Then
            Dataset.SnapshotWS()
            Dataset.getOrientations()
            DataModificationsTracker.InitializeDataSetRegion()
            DataModificationsTracker.InitializeOutputsRegion()

            If Dataset.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG _
            AndAlso Dataset.EntitiesAddressValuesDictionary.Count > 0 Then

                snapshotSuccess = True
                FillInEntityAndCurrencyTB(Dataset.EntitiesAddressValuesDictionary.ElementAt(0).Value)
                SubmissionWSController = New SubmissionWSController(Me, Dataset, Model, DataModificationsTracker)
                HighlightItemsAndDataRegions()
                UpdateAfterAnalysisAxisChanged(GlobalVariables.ClientsIDDropDown.SelectedItemId, _
                                               GlobalVariables.ProductsIDDropDown.SelectedItemId, _
                                               GlobalVariables.AdjustmentIDDropDown.SelectedItemId, _
                                               update_inputs)
                ' Associate worksheet if not already associated 
                If SubmissionWSController.ws Is Nothing Then
                    SubmissionWSController.AssociateWS(associatedWorksheet)
                ElseIf Not SubmissionWSController.ws.Name Is associatedWorksheet Then
                    SubmissionWSController.AssociateWS(associatedWorksheet)
                End If
            Else
                SnapshotError()
            End If
            ADDIN.modifySubmissionControlsStatus(snapshotSuccess)
        End If

    End Sub

    Friend Sub UpdateAfterAnalysisAxisChanged(ByVal client_id As String, _
                                              ByVal product_id As String, _
                                              ByVal adjustment_id As String, _
                                              Optional ByRef update_inputs_from_DB As Boolean = False)

        isUpdating = True
        mustUpdateExcelWorksheetFromDataBase = update_inputs_from_DB
        DataModificationsTracker.DiscardModifications()
        ' PB: Ceci ne marche que pour le cas orientation "AcDa"  !!! 
        ' option: select case on orientation 
        ' (possibility to download inputs from multiple entities => function ready in model)
        ' priority normal

        Model.downloadDBInputs(current_entity_name, _
                               client_id, _
                               product_id, _
                               adjustment_id)

    End Sub

    Private Sub AfterDataBaseInputsDowloaded()

        If mustUpdateExcelWorksheetFromDataBase = True Then
            updateInputs()
        End If
        Dataset.getDataSet()
        DataModificationsTracker.IdentifyDifferencesBtwDataSetAndDB(Model.dataBaseInputsDictionary)
        UpdateCalculatedItems(current_entity_name)
        isUpdating = False

    End Sub

#End Region


#Region "Excel-DGV Controllers Interface"

    Friend Sub UpdateModelFromExcelUpdate(ByRef entityName As String, _
                                          ByRef accountName As String, _
                                            ByRef periodInt As Integer, _
                                            ByVal value As Double, _
                                            ByRef cellAddress As String)

        Model.ValuesDictionariesUpdate(entityName, accountName, periodInt, value)
        DataModificationsTracker.RegisterModification(cellAddress)

    End Sub

    Friend Sub updateInputs()

        isUpdating = True
        SubmissionWSController.updateInputsOnWS()
        ' Update DGV in acquisitionInterface !!
        isUpdating = False

    End Sub

    Friend Sub UpdateCalculatedItems(ByRef entityName As String)

        Model.ComputeCalculatedItems(entityName)

    End Sub

    Private Sub AfterOutputsComputed(ByRef entityName As String)

        isUpdating = True
        SubmissionWSController.updateCalculatedItemsOnWS(entityName)
        isUpdating = False

    End Sub

#End Region


#Region "Submission Background Worker"

    ' Implies type of cell checked before -> only double
    Private Sub BCKGW_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs)

        ' to be reimplemented with Facts CRUD !! 

        'DBUploader.mStartingPeriod = CInt(Dataset.periodsDatesList(0).ToOADate)
        'Dim tmpList As New List(Of String)
        'For Each cellAddress In DataModificationsTracker.GetModificationsListCopy
        '    If DBUploader.CheckAndUpdateSingleValueFromNames(Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.ENTITY_ITEM), _
        '                                                     Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.ACCOUNT_ITEM), _
        '                                                     Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.PERIOD_ITEM), _
        '                                                     GlobalVariables.APPS.ActiveSheet.range(cellAddress).value, _
        '                                                     Dataset.currentVersionCode, _
        '                                                     GlobalVariables.ClientsIDDropDown.SelectedItemId, _
        '                                                     GlobalVariables.ProductsIDDropDown.SelectedItemId, _
        '                                                     GlobalVariables.AdjustmentIDDropDown.SelectedItemId) = False Then

        '        errorsList.Add("Error during upload of Entity: " & Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.ENTITY_ITEM) _
        '                       & " Account: " & Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.ACCOUNT_ITEM) _
        '                       & " Period: " & Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.PERIOD_ITEM) _
        '                       & " Value: " & GlobalVariables.APPS.ActiveSheet.range(cellAddress).value)
        '    Else
        '        DataModificationsTracker.UnregisterSingleModification(cellAddress)
        '    End If
        '    BCKGW.ReportProgress(1)
        'Next

    End Sub

    Private Sub BCKGW_ProgressChanged(sender As Object, e As ComponentModel.ProgressChangedEventArgs)

        PBar.ProgressBarControl1.AddProgress()

    End Sub

    Private Sub BCKGW_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs)

        PBar.ProgressBarControl1.EndProgress()
        PBar.Close()
        If errorsList.Count = 0 Then
            uploadState = True
            GlobalVariables.SubmissionStatusButton.Image = 1
        Else
            uploadState = False
            GlobalVariables.SubmissionStatusButton.Image = 2
        End If

    End Sub

    ' Used only in the case where total cModelDataset.Dataset is sent to be updated by DBUploader
    'Friend Sub UpdateUploadResult(ByRef uploadSuccessfull As Boolean)

    '    DBUploader.PBar.ProgressBarControl1.EndProgress()
    '    DBUploader.PBar.Close()
    '    If uploadSuccessfull = True Then
    '        DataModificationsTracker.UnregisterModifications()
    '        GlobalVariables.SubmissionStatusButton.Image = 1
    '    Else
    '        GlobalVariables.SubmissionStatusButton.Image = 2
    '    End If
    '    ' -> should track error if not successfull (display details by clicking on red button upload)

    'End Sub


#End Region


#Region "Utilities"

    Friend Function GetPeriodsList() As Int32()

        Return Model.currentPeriodList

    End Function

    Friend Function GetTimeConfig() As String

        Return GlobalVariables.Versions.versions_hash(Model.current_version_id)(VERSIONS_TIME_CONFIG_VARIABLE)

    End Function

    Private Sub SnapshotError()

        If Dataset.pAssetFlag = 0 AndAlso _
        Dataset.pAccountFlag <> 0 AndAlso _
        Dataset.pDateFlag <> 0 Then
            ' Initialize AcVPe or PeVAc + necessary to choose an Entity
            ' Need a call from entity selection (which will launch initialize and addHandler, if cancel suppr GRS)
            ' !! priority normal !
        Else
            Dim errorStr As String = IdentifyFlagsErrors()
            MsgBox("The Snapshot was unsuccessful because the following dimensions not found on the Worksheet: " + Chr(13) + _
                   errorStr)
        End If
        snapshotSuccess = False

    End Sub

    Private Function IdentifyFlagsErrors() As String

        Dim tmpStr As String = ""
        If Dataset.pAssetFlag = 0 Then tmpStr = "  - Entities" + Chr(13)
        If Dataset.pDateFlag = 0 Then tmpStr = tmpStr & "  - Periods" + Chr(13)
        If Dataset.pAccountFlag = 0 Then tmpStr = tmpStr & "  - Accounts"
        Return tmpStr

    End Function



#End Region


End Class
