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
    Private Fact As New Facts
    Private Dataset As ModelDataSet
    Private DataModificationsTracker As DataModificationsTracking
    Private Model As AcquisitionModel
    Private SubmissionWSController As SubmissionWSController
    Friend associatedWorksheet As Excel.Worksheet
    Friend wsComboboxMenuItem As ADXRibbonItem
  
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

        AddHandler Model.AfterInputsDownloaded, AddressOf AfterDataBaseInputsDowloaded
        AddHandler Model.AfterOutputsComputed, AddressOf AfterOutputsComputed
        AddHandler Fact.AfterUpdate, AddressOf AfterCommit

    End Sub

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

    Friend Sub DataSubmission()

        If Dataset.GlobalOrientationFlag <> ORIENTATION_ERROR_FLAG Then
            errorsList.Clear()
            Submit()
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
        ' priority normal => V2

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

        ' Update Computer order
        Model.ComputeCalculatedItems(entityName)   

    End Sub

    ' Worksheet display update
    Private Sub AfterOutputsComputed(ByRef entityName As String)

        isUpdating = True
        GlobalVariables.APPS.ScreenUpdating = False
        SubmissionWSController.updateCalculatedItemsOnWS(entityName)
        GlobalVariables.APPS.ScreenUpdating = True
        GlobalVariables.APPS.Interactive = True
        isUpdating = False

    End Sub


#End Region


#Region "Data Submission"

    Private Sub Submit()

        Dim factsList As New List(Of Hashtable)
        Dim cellsAddresses As List(Of String) = DataModificationsTracker.GetModificationsListCopy
        For Each cellAddress In cellsAddresses
            ' Implies type of cell checked before -> only double -> check if we can enter anything else !!!
            Dim ht As New Hashtable
            ht(ENTITY_ID_VARIABLE) = Dataset.EntitiesNameKeyDictionary(Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.ENTITY_ITEM))
            ht(ACCOUNT_ID_VARIABLE) = Dataset.AccountsNameKeyDictionary(Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.ACCOUNT_ITEM))
            ht(PERIOD_VARIABLE) = Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.PERIOD_ITEM)
            ht(VERSION_ID_VARIABLE) = Model.current_version_id
            ht(CLIENT_ID_VARIABLE) = GlobalVariables.ClientsIDDropDown.SelectedItemId
            ht(PRODUCT_ID_VARIABLE) = GlobalVariables.ProductsIDDropDown.SelectedItemId
            ht(ADJUSTMENT_ID_VARIABLE) = GlobalVariables.AdjustmentIDDropDown.SelectedItemId
            ht(VALUE_VARIABLE) = GlobalVariables.APPS.ActiveSheet.range(cellAddress).value

            factsList.Add(ht)
        Next
        Fact.CMSG_UPDATE_FACT_LIST(factsList, cellsAddresses)

    End Sub

    Private Sub AfterCommit(ByRef status As Boolean, ByRef commitResults As Dictionary(Of String, Boolean))

        If status = True Then
             For Each cellAddress In commitResults.Keys
                If commitResults(cellAddress) = True Then
                    DataModificationsTracker.UnregisterSingleModification(cellAddress)
                Else
                    errorsList.Add("Error during upload of Entity: " & Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.ENTITY_ITEM) _
                                    & " Account: " & Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.ACCOUNT_ITEM) _
                                    & " Period: " & Dataset.CellsAddressItemsDictionary(cellAddress)(ModelDataSet.PERIOD_ITEM) _
                                    & " Value: " & GlobalVariables.APPS.ActiveSheet.range(cellAddress).value)
                End If
             Next
            If errorsList.Count = 0 Then
                uploadState = True
                GlobalVariables.SubmissionStatusButton.Image = 1
            Else
                uploadState = False
                GlobalVariables.SubmissionStatusButton.Image = 2
            End If
        Else
            MsgBox("Commit failed. Check network connection and try again." & Chr(13) & "If the error persists, please contact your administrator or the Financial BI team.")
        End If

    End Sub


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
