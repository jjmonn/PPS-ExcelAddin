' CGeneralReportSubmissionControler.vb
' 
' Manages the whole submission process:
'       - DataSet                       ok
'       - SubmissionInterface           ok
'       - DataSetRibbon                 ok
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
'       - Starting Progress bar
'       - CellChanged -> no update for 2 orientations cases yet (account flag =1)
'       - Choix d'un asset dans le cas où seul Entityflag = 0
'       - Manage the flags checks after dataset returned (can use read only array of valid global orientation flag)
'       -  flag indicating GRSControler is associated to the WS and no flag errors
'       -> manages all flag cases -> display errors and/or what to do in case of not okd 
'       - must allow entity selection button only if entity flag = 1 at dataset level
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
' Last modified: 19/01/2015


Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports VIBlend.WinForms.DataGridView
Imports System.Linq
Imports AddinExpress.MSO
Imports System.Collections
Imports System.ComponentModel
Imports System.Collections.Generic
Imports Microsoft.Office.Core


Friend Class CGeneralReportSubmissionControler


#Region "Instance Variables"

#Region "Objects"

    Private ADDIN As AddinModule
    Private DATASET As CModelDataSet
    Private DATAMODTRACKER As CDataModificationsTracking
    Private DBUploader As DataBaseDataUploader
    Private DBDownloader As New DataBaseDataDownloader
    Private CCOMPUTERINT As DLL3_Interface
    Private Model As CAcquisitionModel
    Private EXCELWSCONTROLLER As cExcelSubmissionWorksheetController
    Private ACQUICONTROLLER As cAcquisitionUIController
    Friend associatedWorksheet As Excel.Worksheet
    Friend wsComboboxMenuItem As ADXRibbonItem
    Friend PBar As PBarUI
    Private BCKGW As New BackgroundWorker

#End Region

#Region "Variables"

    Friend snapshotSuccess As Boolean
    Friend autoCommitFlag As Boolean
    Private itemsHighlightFlag As Boolean
    Private errorsList As New List(Of String)
    Private uploadTimeStamp As Date
    Private uploadState As Boolean
    Private originalValue As Double
    Friend isUpdating As Boolean


#End Region

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef inputWSCB As ADXRibbonItem, _
                   ByRef inputAddIn As AddinModule)

        ADDIN = inputAddIn
        wsComboboxMenuItem = inputWSCB
        associatedWorksheet = APPS.ActiveSheet

        DATASET = New CModelDataSet(associatedWorksheet)
        DBUploader = New DataBaseDataUploader()
        DBUploader.SetUpDictionaries(DATASET.EntitiesNameKeyDictionary, _
                                     DATASET.AccountsNameKeyDictionary)
        CCOMPUTERINT = New DLL3_Interface
        DATAMODTRACKER = New CDataModificationsTracking(DATASET)
        Model = New CAcquisitionModel(DATASET, DBDownloader, CCOMPUTERINT)

        BCKGW.WorkerReportsProgress = True
        AddHandler BCKGW.DoWork, AddressOf BCKGW_DoWork
        AddHandler BCKGW.RunWorkerCompleted, AddressOf BCKGW_RunWorkerCompleted
        AddHandler BCKGW.ProgressChanged, AddressOf BCKGW_ProgressChanged

    End Sub

    ' Snapshot WS and initializes ACQMODEL accordingly
    Friend Sub LaunchDataSetSnapshotAndAssociateModel()

        If Not DATASET.GlobalScreenShot Is Nothing Then
            DATASET.SnapshotWS()
            DATASET.getOrientations()
            ' DATASET.RefreshAll -> possible evolution (take off refreshdatabatch in addin and set excel formatting here)
            DATAMODTRACKER.InitializeDataSetRegion()
            DATAMODTRACKER.InitializeOutputsRegion()
            ACQUICONTROLLER = New cAcquisitionUIController(DATASET, Me, Model)

            If DATASET.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG Then
                snapshotSuccess = True
                InitializeAcquisitionModelAndSetUpDisplay()
                EXCELWSCONTROLLER = New cExcelSubmissionWorksheetController(Me, DATASET, Model, DATAMODTRACKER)
                EXCELWSCONTROLLER.AssociateWS(associatedWorksheet)
            Else
                If DATASET.pAssetFlag = 0 AndAlso _
                   DATASET.pAccountFlag <> 0 AndAlso _
                   DATASET.pDateFlag <> 0 Then
                    ' Initialize AcVPe or PeVAc + necessary to choose an Entity
                    ' Need a call from entity selection (which will launch initialize and addHandler, if cancel suppr GRS)
                    snapshotSuccess = False
                Else
                    Dim errorStr As String = IdentifyFlagsErrors()
                    MsgBox("The Snapshot was unsuccessful because the following dimensions not found on the Worksheet: " + Chr(13) + _
                           errorStr)
                    ADDIN.modifySubmissionControlsStatus(False)
                    snapshotSuccess = False
                End If
            End If
        Else
            MsgBox("Empty Worksheet")
        End If

    End Sub

    ' General Initialize and Set up display AcqUI and Ribbon
    Private Sub InitializeAcquisitionModelAndSetUpDisplay()

        DATASET.getDataSet()
        ACQUICONTROLLER.InitializeACQUIDGV()
        LaunchInitialDBDifferencesLookUp()
        ' ACQUICONTROLLER.ShowAcquisitionUI()
        If snapshotSuccess = False Then
            snapshotSuccess = True
            ADDIN.GetRibbonControlEnabled(True)
        End If

    End Sub

    ' Display the entity name and currency in the ribbons Corresponding edit boxes
    Friend Sub FillInEntityAndCurrencyTB(ByRef entity As String)

        ADDIN.CurrentEntityTB.Text = entity
        Dim entitiesNameCurrDic As Hashtable = EntitiesMapping.GetEntitiesDictionary(ASSETS_NAME_VARIABLE, ASSETS_CURRENCY_VARIABLE)
        ADDIN.EntCurrTB.Text = entitiesNameCurrDic(entity)
        ACQUICONTROLLER.UpdateCurrentEntityAndCurrencyOnACQUI(entity, entitiesNameCurrDic(entity), "??")

    End Sub

    Friend Sub LaunchInitialDBDifferencesLookUp()

        DATAMODTRACKER.IdentifyDifferencesBtwDataSetAndDB(Model.DBInputsDictionary)

    End Sub


#End Region


#Region "Ribbon Interface"

    Friend Sub ActivateControler()

        UpdateRibbonTextBoxes()
        ' Check that the dataSet is still the same ?

    End Sub

    Friend Sub UpdateRibbonTextBoxes()

        If DATASET.pAssetFlag = 1 Then FillInEntityAndCurrencyTB(DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value)

    End Sub

    Friend Sub Submit()

        If DATASET.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG Then
            ' DBUploader.UpdateDataBase(DATASET.dataSetDictionary, _
            '                           CInt(DATASET.periodsDatesList(0).ToOADate, _
            '                           DATASET.currentVersionCode)
            ' ACQMODEL.FillSubmittedCellsInGreen()
            uploadTimeStamp = Now
            errorsList.Clear()
            PBar = New PBarUI
            PBar.ProgressBarControl1.Launch(1, DATAMODTRACKER.modifiedCellsList.Count())
            PBar.Show()
            BCKGW.RunWorkerAsync()

        Else
            SubmissionStatusButton.Image = 2
            errorsList.Add("The worksheet recognition was not set up properly.")
            MsgBox("PPS Error tracking -> which flag error")
            uploadState = False
        End If

    End Sub

    Friend Sub HighlightItemsAndDataRegions()

        If itemsHighlightFlag = False Then
            DATAMODTRACKER.HighlightItemsAndDataRanges()
            itemsHighlightFlag = True
        Else
            DATAMODTRACKER.UnHighlightItemsAndDataRanges()
            itemsHighlightFlag = False
        End If

    End Sub

    Friend Sub ChangeCurrentEntity(ByRef entityname As String)

        Dim entityCellAddress As String = DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Key
        associatedWorksheet.Range(entityCellAddress).Value2 = entityname
        DATASET.EntitiesAddressValuesDictionary(entityCellAddress) = entityname
        FillInEntityAndCurrencyTB(entityname)

    End Sub

    Friend Sub DisplayUploadStatusAndErrorsUI()

        Dim UploadStatusUI As New UploadingHistoryUI(uploadState, _
                                                     uploadTimeStamp, _
                                                     errorsList)
        UploadStatusUI.Show()

    End Sub

    Friend Sub CloseInstance()

        If Not ACQUICONTROLLER Is Nothing Then
            ACQUICONTROLLER.ShutDown()
            ACQUICONTROLLER = Nothing
        End If
        If Not DATASET Is Nothing Then DATASET = Nothing
        If Not DATAMODTRACKER Is Nothing Then DATAMODTRACKER = Nothing
        If Not CCOMPUTERINT Is Nothing Then CCOMPUTERINT = Nothing
        If Not Model Is Nothing Then Model = Nothing
        Try
            RemoveHandler associatedWorksheet.Change, AddressOf EXCELWSCONTROLLER.Worksheet_Change
        Catch ex As Exception
            MsgBox("GRS could not take off WS event")
        End Try
        Try
            RemoveHandler associatedWorksheet.BeforeRightClick, AddressOf EXCELWSCONTROLLER.Worksheet_BeforeRightClick
        Catch ex As Exception
            MsgBox("GRS could not take off before click event")
        End Try

        If Not EXCELWSCONTROLLER Is Nothing Then EXCELWSCONTROLLER = Nothing
        Try
            associatedWorksheet.Unprotect()
        Catch ex As Exception
        End Try

    End Sub

    Friend Sub RangeEdition()

        Dim RNGEDITION As New ManualRangesSelectionUI(Me, DATASET)
        HideACQUI()
        RNGEDITION.Show()

    End Sub

    Protected Friend Sub UpdateGRSAfterAdjustmentIdChanged(ByVal adjustment_id As String)

        isUpdating = True
        DATASET.RefreshAll(adjustment_id)
        InitializeAcquisitionModelAndSetUpDisplay()
        Model.DownloadDBInputs(ADDIN.CurrentEntityTB.Text, adjustment_id)
        UpdateModel(ADDIN.CurrentEntityTB.Text)
        isUpdating = False

    End Sub

#End Region


#Region "Excel-DGV Controllers Interface"

    Friend Sub UpdateDGVFromExcelUpdate(ByRef entityName As String, _
                                        ByRef accountName As String, _
                                        ByRef periodInt As Integer, _
                                        ByVal value As Double, _
                                        ByRef cellAddress As String)


        ACQUICONTROLLER.SetDGVCellValue(entityName, accountName, periodInt, value)
        Model.ValuesDictionariesUpdate(entityName, accountName, periodInt, value)
        DATAMODTRACKER.RegisterModification(cellAddress)


    End Sub

    Friend Sub UpdateExcelFromDGVUpdate(ByRef entityName As String, _
                                        ByRef accountName As String, _
                                        ByRef periodInt As Integer, _
                                        ByVal value As Double)

        Dim cell As Excel.Range = EXCELWSCONTROLLER.UpdateExcelWS(entityName, accountName, periodInt, value)
        Model.ValuesDictionariesUpdate(entityName, accountName, periodInt, value)
        If Not cell Is Nothing Then DATAMODTRACKER.RegisterModification(cell.Address)

    End Sub

    Friend Sub UpdateModel(ByRef entityName As String)

        isUpdating = True
        Model.ComputeCalculatedItems(entityName)
        EXCELWSCONTROLLER.UpdateCalculatedItemsOnWS(entityName)
        ACQUICONTROLLER.UpdateCalculatedItemsOnDGV(entityName)
        isUpdating = False

    End Sub


#End Region


#Region "Data Set And Display Update"

    Friend Sub UpdateDataSetRegions()

        DATASET.SnapshotWS()
        DATASET.getOrientations()
        If DATASET.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG Then
            InitializeAcquisitionModelAndSetUpDisplay()
        End If

    End Sub

    Friend Sub UpdateDataSet()

        DATASET.getOrientations()
        If DATASET.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG Then
            InitializeAcquisitionModelAndSetUpDisplay()
        End If

    End Sub

#End Region


#Region "Submission Background Worker"

    ' Implies type of cell checked before -> only double
    Private Sub BCKGW_DoWork(sender As Object, e As ComponentModel.DoWorkEventArgs)

        DBUploader.mStartingPeriod = CInt(DATASET.periodsDatesList(0).ToOADate)
        Dim tmpList As New List(Of String)
        For Each cellAddress In DATAMODTRACKER.GetModificationsListCopy
            If DBUploader.CheckAndUpdateSingleValueFromNames(DATASET.CellsAddressItemsDictionary(cellAddress)(CModelDataSet.ENTITY_ITEM), _
                                                             DATASET.CellsAddressItemsDictionary(cellAddress)(CModelDataSet.ACCOUNT_ITEM), _
                                                             DATASET.CellsAddressItemsDictionary(cellAddress)(CModelDataSet.PERIOD_ITEM), _
                                                             APPS.ActiveSheet.range(cellAddress).value, _
                                                             DATASET.currentVersionCode, _
                                                             AdjustmentIDDropDown.SelectedItemId) = False Then

                errorsList.Add("Error during upload of Entity: " & DATASET.CellsAddressItemsDictionary(cellAddress)(CModelDataSet.ENTITY_ITEM) _
                               & " Account: " & DATASET.CellsAddressItemsDictionary(cellAddress)(CModelDataSet.ACCOUNT_ITEM) _
                               & " Period: " & DATASET.CellsAddressItemsDictionary(cellAddress)(CModelDataSet.PERIOD_ITEM) _
                               & " Value: " & APPS.ActiveSheet.range(cellAddress).value)
            Else
                DATAMODTRACKER.UnregisterSingleModification(cellAddress)
            End If
            BCKGW.ReportProgress(1)
        Next

    End Sub

    Private Sub BCKGW_ProgressChanged(sender As Object, e As ComponentModel.ProgressChangedEventArgs)

        PBar.ProgressBarControl1.AddProgress()

    End Sub

    Private Sub BCKGW_RunWorkerCompleted(sender As Object, e As ComponentModel.RunWorkerCompletedEventArgs)

        PBar.ProgressBarControl1.EndProgress()
        PBar.Close()
        If errorsList.Count = 0 Then
            uploadState = True
            SubmissionStatusButton.Image = 1
        Else
            uploadState = False
            SubmissionStatusButton.Image = 2
        End If

    End Sub

    ' Used only in the case where total cModelDataSet.dataSet is sent to be updated by DBUploader
    Public Sub UpdateUploadResult(ByRef uploadSuccessfull As Boolean)

        DBUploader.PBar.ProgressBarControl1.EndProgress()
        DBUploader.PBar.Close()
        If uploadSuccessfull = True Then
            DATAMODTRACKER.UnregisterModifications()
            SubmissionStatusButton.Image = 1
        Else
            SubmissionStatusButton.Image = 2
        End If
        ' -> should track error if not successfull (display details by clicking on red button upload)

    End Sub


#End Region


#Region "Utilities"

    Private Function IdentifyFlagsErrors() As String

        Dim tmpStr As String = ""
        If DATASET.pAssetFlag = 0 Then tmpStr = "  - Entities" + Chr(13)
        If DATASET.pDateFlag = 0 Then tmpStr = "  - Periods" + Chr(13)
        If DATASET.pAccountFlag = 0 Then tmpStr = "  - Accounts"
        Return tmpStr

    End Function

    Friend Sub HideACQUI()

        ACQUICONTROLLER.HideAcquisitionUI()

    End Sub

    Friend Sub ShowACQUI()

        ACQUICONTROLLER.ShowAcquisitionUI()

    End Sub

    Protected Friend Function GetPeriodsList() As List(Of Int32)

        Return Model.currentPeriodlist

    End Function

    Protected Friend Function GetTimeConfig() As String

        Return Model.versionsTimeConfigDict(Model.mCurrentVersionCode)

    End Function

#End Region


End Class
