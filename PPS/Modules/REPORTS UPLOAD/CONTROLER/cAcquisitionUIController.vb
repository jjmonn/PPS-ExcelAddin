' cAcquisitionUIController.vb
'
' Manages interactions bewteen cAcquisitionUI and cAcquisitionUIModel
'
' To do: 
'       - set up DGV name = 3rd dimension !! 
'       - before validating edition need to check if cell is HV or BS 1st year
'       '- if grs autocommit flag = true -> update !
'
' Known bugs: 
'       -
'
' Author: Julien Monnereau
' Last modified: 25/01/2015


Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports System.Linq


Friend Class cAcquisitionUIController


#Region "Instance Variables"

    ' Objects
    Private GRS As CGeneralReportSubmissionControler
    Private ACQUI As AcquisitionUI
    Private ACQUMODEL As CAcquisitionModel
    Private DATASET As CModelDataSet

    ' Variables
    Private isStandAlone As Boolean
    Friend currentDGVOrientationFlag As String
    Friend currentCurrency As String
    Friend currentEntity As String

    ' DGV Dictionaries
    Friend rowsKeyItemDictionary As New Dictionary(Of String, HierarchyItem)
    Friend columnsKeyItemDictionary As New Dictionary(Of String, HierarchyItem)
    Friend editorKeyHierarchyDictionary As New Dictionary(Of String, HierarchyItem)
    Friend periodsItemIDPeriodCodeDict As New Dictionary(Of String, Integer)


#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputDataSet As CModelDataSet, _
                   Optional ByRef inputGRS As CGeneralReportSubmissionControler = Nothing, _
                   Optional ByRef inputAcquModel As CAcquisitionModel = Nothing)

        DATASET = inputDataSet
        ACQUI = New AcquisitionUI(DATASET, Me)
        If Not inputAcquModel Is Nothing Then ACQUMODEL = inputAcquModel
        If Not inputGRS Is Nothing Then GRS = inputGRS

    End Sub

#End Region


#Region "Interface"

    Friend Sub ShowAcquisitionUI()

        ACQUI.Show()

    End Sub

    Friend Sub HideAcquisitionUI()

        ACQUI.Hide()

    End Sub

    Friend Sub InitializeACQUIDGV()

        ACQUI.DGV = New vDataGridView
        DataGridViewsUtil.DGVSetHiearchyFontSize(ACQUI.DGV, AcquisitionUI.DGV_FONT_SIZE, AcquisitionUI.DGV_FONT_SIZE)

        Select Case DATASET.GlobalOrientationFlag
            Case CModelDataSet.DATASET_ACCOUNTS_PERIODS_OR
                ACQUI.DGV.Name = DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVAcPe(DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value)
                GRS.FillInEntityAndCurrencyTB(DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value)

            Case CModelDataSet.DATASET_PERIODS_ACCOUNTS_OR
                ACQUI.DGV.Name = DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVPeAc(DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value)
                GRS.FillInEntityAndCurrencyTB(DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value)

            Case CModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR
                ACQUI.DGV.Name = DATASET.periodsAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVAcEn(DATASET.periodsAddressValuesDictionary.ElementAt(0).Value)

            Case CModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR
                ACQUI.DGV.Name = DATASET.periodsAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVEnAc(DATASET.periodsAddressValuesDictionary.ElementAt(0).Value)

            Case CModelDataSet.DATASET_PERIODS_ENTITIES_OR
                ACQUI.DGV.Name = DATASET.AccountsAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVpPeEn(DATASET.AccountsAddressValuesDictionary.ElementAt(0).Value)

            Case CModelDataSet.DATASET_ENTITIES_PERIODS_OR
                ACQUI.DGV.Name = DATASET.AccountsAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVEnPe(DATASET.AccountsAddressValuesDictionary.ElementAt(0).Value)

        End Select
        ACQUI.InitDGVDisplay()

    End Sub

    Friend Sub UpdateCurrentEntityAndCurrencyOnACQUI(ByRef entityName As String, ByRef currency As String, ByRef version As String)

        ACQUI.CurrencyTB.Text = currency
        ACQUI.entityTB.Text = entityName
        ACQUI.VersionTB.Text = version

    End Sub

#End Region


#Region "Hierarchies Initialization"

#Region "Accounts and Periods"

    ' Initializes a DGV with accounts in rows and period in column for a given entity
    Friend Sub ConfigDGVAcPe(ByRef entity As String)

        ClearDGVandDictionaries()
        ACQUMODEL.DownloadDBInputs(entity, GlobalVariables.AdjustmentIDDropDown.SelectedItemId)
        ACQUI.LoadAccountsToHierarchy(ACQUI.DGV.RowsHierarchy, entity, ACQUMODEL.accountsTV)
        ACQUI.LoadPeriodsToHierarchy(ACQUI.DGV.ColumnsHierarchy, entity, GetRandomAccount(entity), ACQUMODEL.currentPeriodlist, ACQUMODEL.versionsTimeConfigDict(ACQUMODEL.mCurrentVersionCode))

        For Each account In ACQUI.DGV.RowsHierarchy.Items
            FillInSubItemAcPe(account, ACQUI.DGV.ColumnsHierarchy, entity, CModelDataSet.DATASET_ACCOUNTS_PERIODS_OR)
        Next
        currentDGVOrientationFlag = CModelDataSet.DATASET_ACCOUNTS_PERIODS_OR

        ACQUMODEL.ComputeCalculatedItems(entity)
        For Each account In ACQUI.DGV.RowsHierarchy.Items
            FillInCalculatedSubItemsAcPe(account, ACQUI.DGV.ColumnsHierarchy, entity, CModelDataSet.DATASET_ACCOUNTS_PERIODS_OR)
        Next

    End Sub

    ' Set up Periods in rows, accounts in columns configurations 
    Friend Sub ConfigDGVPeAc(ByRef entity As String)

        ClearDGVandDictionaries()
        ACQUI.LoadAccountsToHierarchy(ACQUI.DGV.ColumnsHierarchy, entity, ACQUMODEL.accountsTV)
        ACQUI.LoadPeriodsToHierarchy(ACQUI.DGV.RowsHierarchy, entity, GetRandomAccount(entity), ACQUMODEL.currentPeriodlist, ACQUMODEL.versionsTimeConfigDict(ACQUMODEL.mCurrentVersionCode))
        ACQUMODEL.DownloadDBInputs(entity, GlobalVariables.AdjustmentIDDropDown.SelectedItemId)

        For Each account In ACQUI.DGV.ColumnsHierarchy.Items
            FillInSubItemAcPe(account, ACQUI.DGV.RowsHierarchy, entity, CModelDataSet.DATASET_PERIODS_ACCOUNTS_OR)
        Next
        currentDGVOrientationFlag = CModelDataSet.DATASET_PERIODS_ACCOUNTS_OR

        ACQUMODEL.ComputeCalculatedItems(entity)
        For Each account In ACQUI.DGV.ColumnsHierarchy.Items
            FillInCalculatedSubItemsAcPe(account, ACQUI.DGV.RowsHierarchy, entity, CModelDataSet.DATASET_PERIODS_ACCOUNTS_OR)
        Next

    End Sub

#End Region


#Region "Accounts and Entities"

    ' Set up Accounts in Rows, Entities in Columns configuration
    Friend Sub ConfigDGVAcEn(ByRef period As String)

        ACQUI.DGV.Clear()
        ACQUI.LoadEntitiesToHierarchy(ACQUI.DGV.ColumnsHierarchy)
        Dim randomEntity As String = ACQUI.DGV.ColumnsHierarchy.Items(0).Caption
        ACQUI.LoadAccountsToHierarchy(ACQUI.DGV.RowsHierarchy, randomEntity, ACQUMODEL.accountsTV)
        For Each account In ACQUI.DGV.RowsHierarchy.Items
            FillInSubItemAcEn(account, ACQUI.DGV.ColumnsHierarchy, period, CModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR)
        Next
        currentDGVOrientationFlag = CModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR

    End Sub

    ' Set up Entities in Rows, accounts in Columns configuration
    Friend Sub ConfigDGVEnAc(ByRef period As String)

        ACQUI.DGV.Clear()
        ACQUI.LoadEntitiesToHierarchy(ACQUI.DGV.RowsHierarchy)
        Dim randomEntity As String = ACQUI.DGV.RowsHierarchy.Items(0).Caption
        ACQUI.LoadAccountsToHierarchy(ACQUI.DGV.ColumnsHierarchy, randomEntity, ACQUMODEL.accountsTV)
        For Each account In ACQUI.DGV.ColumnsHierarchy.Items
            FillInSubItemAcEn(account, ACQUI.DGV.RowsHierarchy, period, CModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR)
        Next

        currentDGVOrientationFlag = CModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR

    End Sub


#End Region


#Region "Entities and Periods"

    ' Set up Entities in Rows, Periods in column configuration
    Friend Sub ConfigDGVEnPe(ByRef account As String)

        ACQUI.DGV.Clear()
        ACQUI.LoadEntitiesToHierarchy(ACQUI.DGV.RowsHierarchy)
        Dim currentEntity As String = ACQUI.DGV.RowsHierarchy.Items(0).Caption
        ACQUI.LoadPeriodsToHierarchy(ACQUI.DGV.ColumnsHierarchy, currentEntity, account, ACQUMODEL.currentPeriodlist, ACQUMODEL.versionsTimeConfigDict(ACQUMODEL.mCurrentVersionCode))

        ' -> download DBInputs -> for all entities 
        Dim entitiesList As New List(Of String)
        For Each item In DATASET.EntitiesAddressValuesDictionary.Values
            entitiesList.Add(item)
        Next
        ' -> we should keep this list ?
        ACQUMODEL.DownloadDBInputs(entitiesList, GlobalVariables.AdjustmentIDDropDown.SelectedItemId)
        FillInSubItemEnPe(ACQUI.DGV.RowsHierarchy, ACQUI.DGV.ColumnsHierarchy, account, CModelDataSet.DATASET_ENTITIES_PERIODS_OR)
        currentDGVOrientationFlag = CModelDataSet.DATASET_ENTITIES_PERIODS_OR

    End Sub

    ' Set up Periods in Rows, Entities in column configuration
    Friend Sub ConfigDGVpPeEn(ByRef account As String)

        ACQUI.DGV.Clear()
        ACQUI.LoadEntitiesToHierarchy(ACQUI.DGV.ColumnsHierarchy)
        Dim currentEntity As String = ACQUI.DGV.ColumnsHierarchy.Items(0).Caption
        ACQUI.LoadPeriodsToHierarchy(ACQUI.DGV.RowsHierarchy, currentEntity, account, ACQUMODEL.currentPeriodlist, ACQUMODEL.versionsTimeConfigDict(ACQUMODEL.mCurrentVersionCode))

        ' -> download DBInputs -> for all entities 
        Dim entitiesList As New List(Of String)
        For Each item In DATASET.EntitiesAddressValuesDictionary.Values
            entitiesList.Add(item)
        Next
        ' -> we should keep this list ?
        ACQUMODEL.DownloadDBInputs(entitiesList, GlobalVariables.AdjustmentIDDropDown.SelectedItemId)
        FillInSubItemEnPe(ACQUI.DGV.ColumnsHierarchy, ACQUI.DGV.RowsHierarchy, account, CModelDataSet.DATASET_PERIODS_ENTITIES_OR)
        currentDGVOrientationFlag = CModelDataSet.DATASET_PERIODS_ENTITIES_OR

    End Sub



#End Region

#End Region


#Region "DGV Fill in"

    ' Recursively fills in DGV - Configuration Accounts and periods
    Private Sub FillInSubItemAcPe(ByRef account As HierarchyItem, _
                                  ByRef periodHierarchy As Hierarchy, _
                                  ByRef currentEntity As String,
                                  ByRef configCode As String)

        Dim value As Double
        Dim periodAsInt As Integer
        For Each period In periodHierarchy.Items
            periodAsInt = periodsItemIDPeriodCodeDict(period.GetUniqueID)
            If DATASET.dataSetDictionary(currentEntity).ContainsKey(account.Caption) Then
                value = DATASET.dataSetDictionary(currentEntity)(account.Caption)(periodAsInt)
            ElseIf ACQUMODEL.DBInputsDictionary(currentEntity).ContainsKey(account.Caption) AndAlso _
                   ACQUMODEL.DBInputsDictionary(currentEntity)(account.Caption).ContainsKey(periodAsInt) Then
                value = ACQUMODEL.DBInputsDictionary(currentEntity)(account.Caption)(periodAsInt)
            Else
                value = Nothing
            End If
            Select Case configCode
                Case CModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : ACQUI.DGV.CellsArea.SetCellValue(account, period, value)
                Case CModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : ACQUI.DGV.CellsArea.SetCellValue(period, account, value)
            End Select
        Next
        For Each subRow As HierarchyItem In account.Items
            FillInSubItemAcPe(subRow, periodHierarchy, currentEntity, configCode)
        Next

    End Sub

    ' Recursively fills in DGV - Configuration Accounts and entities
    Private Sub FillInSubItemAcEn(ByRef account As HierarchyItem, _
                                  ByRef entityHierarchy As Hierarchy, _
                                  ByRef period As String, _
                                  ByRef configCode As String)

        Dim value As Double
        For Each entity In entityHierarchy.Items
            If DATASET.dataSetDictionary(entity.Caption).ContainsKey(account.Caption) Then
                value = DATASET.dataSetDictionary(entity.Caption)(account.Caption)(period)
            Else
                value = 0
            End If
            Select Case configCode
                Case CModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : account.DataGridView.CellsArea.SetCellValue(account, entity, value)
                Case CModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : account.DataGridView.CellsArea.SetCellValue(entity, account, value)
            End Select
        Next
        For Each subRow As HierarchyItem In account.Items
            FillInSubItemAcEn(subRow, entityHierarchy, period, configCode)
        Next

    End Sub

    ' Fills in DGV - Configuration Entities and Periods
    Private Sub FillInSubItemEnPe(ByRef entitiesHierarchy As Hierarchy, _
                                  ByRef periodsHierarchy As Hierarchy, _
                                  ByRef account As String, _
                                  ByRef configCode As String)

        Dim value As Double
        For Each entity In entitiesHierarchy.Items
            For Each period In periodsHierarchy.Items
                If DATASET.dataSetDictionary(entity.Caption).ContainsKey(account) Then
                    value = DATASET.dataSetDictionary(entity.Caption)(account)(periodsItemIDPeriodCodeDict(period.GetUniqueID))
                Else
                    value = 0
                End If
                Select Case configCode
                    Case CModelDataSet.DATASET_ENTITIES_PERIODS_OR : ACQUI.DGV.CellsArea.SetCellValue(entity, period, value)
                    Case CModelDataSet.DATASET_PERIODS_ENTITIES_OR : ACQUI.DGV.CellsArea.SetCellValue(period, entity, value)
                End Select
            Next
        Next

    End Sub

    ' Recursively fills in the CALCULATED ITEM of the DGV - Configuration Accounts and periods
    Private Sub FillInCalculatedSubItemsAcPe(ByRef account As HierarchyItem, _
                                             ByRef periodHierarchy As Hierarchy, _
                                             ByRef currentEntity As String,
                                             ByRef configCode As String)

        Dim value As Double
        Dim accKey As String

        If ACQUMODEL.outputsList.Contains(account.Caption) Then
            accKey = DATASET.AccountsNameKeyDictionary(account.Caption)
            For Each period In periodHierarchy.Items
                value = ACQUMODEL.GetCalculatedValue(accKey, periodsItemIDPeriodCodeDict(period.GetUniqueID))
                Select Case configCode
                    Case CModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : account.DataGridView.CellsArea.SetCellValue(account, period, value)
                    Case CModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : account.DataGridView.CellsArea.SetCellValue(period, account, value)
                End Select
            Next
        End If
        For Each subRow As HierarchyItem In account.Items
            FillInCalculatedSubItemsAcPe(subRow, periodHierarchy, currentEntity, configCode)
        Next

    End Sub


#End Region


#Region "Updates"

    Friend Sub SetDGVCellValue(ByRef entity As String, _
                               ByRef account As String, _
                               ByRef period As String, _
                               ByVal value As Double)

        ACQUI.GetDGVCell(entity, account, period).Value = value

    End Sub

    Friend Sub UpdateCalculatedItemsOnDGV(ByRef entityName As String)

        Dim accountHierarchy, secondHierarchy As Hierarchy
        ' the select case below assumes that the ACQMODEL.DGV has the same orientation as the DATASET...!!
        Select Case DATASET.GlobalOrientationFlag

            Case CModelDataSet.DATASET_ACCOUNTS_PERIODS_OR, CModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR
                accountHierarchy = ACQUI.DGV.RowsHierarchy
                secondHierarchy = ACQUI.DGV.ColumnsHierarchy

            Case CModelDataSet.DATASET_PERIODS_ACCOUNTS_OR, CModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR
                accountHierarchy = ACQUI.DGV.ColumnsHierarchy
                secondHierarchy = ACQUI.DGV.RowsHierarchy

            Case CModelDataSet.DATASET_PERIODS_ENTITIES_OR, CModelDataSet.DATASET_ENTITIES_PERIODS_OR
                Exit Sub

        End Select

        For Each accountItem As HierarchyItem In accountHierarchy.Items
            FillInCalculatedSubItemsAcPe(accountItem, secondHierarchy, entityName, currentDGVOrientationFlag)
        Next

    End Sub

    Friend Sub sendUpdateToGRS(ByRef entityName As String, _
                               ByRef accountName As String, _
                               ByRef periodInt As String, _
                               ByVal value As Double)

        GRS.UpdateExcelFromDGVUpdate(entityName, accountName, periodInt, value)
        GRS.UpdateModel(entityName)

    End Sub

#End Region


#Region "Utilities"

    ' returns a random account in the dataset dictionary
    Private Function GetRandomAccount(ByRef entity As String) As String

        Dim e As Dictionary(Of String, Dictionary(Of String, Double)).Enumerator = DATASET.dataSetDictionary(entity).GetEnumerator
        e.MoveNext()
        Return e.Current.Key

    End Function

    Friend Sub ShutDown()

        ACQUI.Close()
        ACQUI.Dispose()

    End Sub

    Friend Function IsGRSUpdating() As Boolean

        Return GRS.isUpdating

    End Function

    Friend Sub ClearDGVandDictionaries()

        ACQUI.DGV.Clear()
        rowsKeyItemDictionary.Clear()
        columnsKeyItemDictionary.Clear()
        editorKeyHierarchyDictionary.Clear()
        periodsItemIDPeriodCodeDict.Clear()

    End Sub

    Protected Friend Function GetPeriodsList() As List(Of Int32)

        Return GRS.GetPeriodsList

    End Function

    Protected Friend Function GetTimeConfig()

        Return GRS.GetTimeConfig

    End Function

#End Region


End Class
