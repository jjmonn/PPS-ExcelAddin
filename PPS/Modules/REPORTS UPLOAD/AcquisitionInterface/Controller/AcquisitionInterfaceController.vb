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


Friend Class AcquisitionInterfaceController


#Region "Instance Variables"

    ' Objects
    Private ACQUI As AcquisitionInterface
    Private ACQUMODEL As AcquisitionModel
    Private DATASET As ModelDataSet

    ' Variables
    Private isStandAlone As Boolean
    Friend currentDGVOrientationFlag As String
    Friend currentCurrency As String
    Friend currentEntityName As String  ' caution to be initialized !!! 

    ' DGV Dictionaries
    Friend rowsKeyItemDictionary As New Dictionary(Of String, HierarchyItem)
    Friend columnsKeyItemDictionary As New Dictionary(Of String, HierarchyItem)
    Friend editorKeyHierarchyDictionary As New Dictionary(Of String, HierarchyItem)
    Friend periodsItemIDPeriodCodeDict As New Dictionary(Of String, Integer)


#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputDataSet As ModelDataSet, _
                   Optional ByRef inputAcquModel As AcquisitionModel = Nothing)

        DATASET = inputDataSet
        ACQUI = New AcquisitionInterface(DATASET, Me)
        If Not inputAcquModel Is Nothing Then ACQUMODEL = inputAcquModel
        
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
        DataGridViewsUtil.DGVSetHiearchyFontSize(ACQUI.DGV, My.Settings.dgvFontSize, My.Settings.dgvFontSize)

        Select Case DATASET.GlobalOrientationFlag
            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR
                ACQUI.DGV.Name = DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVAcPe(DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value)
           
            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR
                ACQUI.DGV.Name = DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVPeAc(DATASET.EntitiesAddressValuesDictionary.ElementAt(0).Value)
               
            Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR
                ACQUI.DGV.Name = DATASET.periodsAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVAcEn(DATASET.periodsAddressValuesDictionary.ElementAt(0).Value)

            Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR
                ACQUI.DGV.Name = DATASET.periodsAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVEnAc(DATASET.periodsAddressValuesDictionary.ElementAt(0).Value)

            Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR
                ACQUI.DGV.Name = DATASET.AccountsAddressValuesDictionary.ElementAt(0).Value
                ConfigDGVpPeEn(DATASET.AccountsAddressValuesDictionary.ElementAt(0).Value)

            Case ModelDataSet.DATASET_ENTITIES_PERIODS_OR
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
    Friend Sub ConfigDGVAcPe(ByRef entityName As String)

        ClearDGVandDictionaries()
        ACQUMODEL.downloadDBInputs(entityName, _
                                   GlobalVariables.ClientsIDDropDown.SelectedItemId, _
                                   GlobalVariables.ProductsIDDropDown.SelectedItemId, _
                                   GlobalVariables.AdjustmentIDDropDown.SelectedItemId)
        ACQUI.LoadAccountsToHierarchy(ACQUI.DGV.RowsHierarchy, entityName, ACQUMODEL.accountsTV)
        ACQUI.LoadPeriodsToHierarchy(ACQUI.DGV.ColumnsHierarchy, _
                                     entityName, _
                                     GetRandomAccount(entityName), _
                                     ACQUMODEL.currentPeriodList, _
                                     GlobalVariables.Versions.versions_hash(ACQUMODEL.current_version_id)(VERSIONS_TIME_CONFIG_VARIABLE))

        For Each account In ACQUI.DGV.RowsHierarchy.Items
            FillInSubItemAcPe(account, ACQUI.DGV.ColumnsHierarchy, entityName, ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR)
        Next
        currentDGVOrientationFlag = ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR

        ACQUMODEL.ComputeCalculatedItems(entityName)
        For Each account In ACQUI.DGV.RowsHierarchy.Items
            FillInCalculatedSubItemsAcPe(account, ACQUI.DGV.ColumnsHierarchy, entityName, ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR)
        Next

    End Sub

    ' Set up Periods in rows, accounts in columns configurations 
    Friend Sub ConfigDGVPeAc(ByRef entityName As String)

        ClearDGVandDictionaries()
        ACQUI.LoadAccountsToHierarchy(ACQUI.DGV.ColumnsHierarchy, entityName, ACQUMODEL.accountsTV)
        ACQUI.LoadPeriodsToHierarchy(ACQUI.DGV.RowsHierarchy, entityName, GetRandomAccount(entityName), ACQUMODEL.currentPeriodList, GlobalVariables.Versions.versions_hash(ACQUMODEL.current_version_id)(VERSIONS_TIME_CONFIG_VARIABLE))
        ACQUMODEL.downloadDBInputs(entityName, _
                                   GlobalVariables.ClientsIDDropDown.SelectedItemId, _
                                   GlobalVariables.ProductsIDDropDown.SelectedItemId, _
                                   GlobalVariables.AdjustmentIDDropDown.SelectedItemId)

        For Each account In ACQUI.DGV.ColumnsHierarchy.Items
            FillInSubItemAcPe(account, ACQUI.DGV.RowsHierarchy, entityName, ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR)
        Next
        currentDGVOrientationFlag = ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR

        ACQUMODEL.ComputeCalculatedItems(entityName)
        For Each account In ACQUI.DGV.ColumnsHierarchy.Items
            FillInCalculatedSubItemsAcPe(account, ACQUI.DGV.RowsHierarchy, entityName, ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR)
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
            FillInSubItemAcEn(account, ACQUI.DGV.ColumnsHierarchy, period, ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR)
        Next
        currentDGVOrientationFlag = ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR

    End Sub

    ' Set up Entities in Rows, accounts in Columns configuration
    Friend Sub ConfigDGVEnAc(ByRef period As String)

        ACQUI.DGV.Clear()
        ACQUI.LoadEntitiesToHierarchy(ACQUI.DGV.RowsHierarchy)
        Dim randomEntity As String = ACQUI.DGV.RowsHierarchy.Items(0).Caption
        ACQUI.LoadAccountsToHierarchy(ACQUI.DGV.ColumnsHierarchy, randomEntity, ACQUMODEL.accountsTV)
        For Each account In ACQUI.DGV.ColumnsHierarchy.Items
            FillInSubItemAcEn(account, ACQUI.DGV.RowsHierarchy, period, ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR)
        Next

        currentDGVOrientationFlag = ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR

    End Sub


#End Region


#Region "Entities and Periods"

    ' Set up Entities in Rows, Periods in column configuration
    Friend Sub ConfigDGVEnPe(ByRef account As String)

        ACQUI.DGV.Clear()
        ACQUI.LoadEntitiesToHierarchy(ACQUI.DGV.RowsHierarchy)
        Dim currentEntity As String = ACQUI.DGV.RowsHierarchy.Items(0).Caption
        ACQUI.LoadPeriodsToHierarchy(ACQUI.DGV.ColumnsHierarchy, currentEntity, account, ACQUMODEL.currentPeriodlist, GlobalVariables.Versions.versions_hash(ACQUMODEL.current_version_id)(VERSIONS_TIME_CONFIG_VARIABLE))

        ' -> download DBInputs -> for all entities 
        Dim entitiesList As New List(Of String)
        For Each item In DATASET.EntitiesAddressValuesDictionary.Values
            entitiesList.Add(item)
        Next
        ' -> we should keep this list ?
        ACQUMODEL.DownloadDBInputs(entitiesList, _
                                   GlobalVariables.ClientsIDDropDown.SelectedItemId, _
                                   GlobalVariables.ProductsIDDropDown.SelectedItemId, _
                                   GlobalVariables.AdjustmentIDDropDown.SelectedItemId)
        FillInSubItemEnPe(ACQUI.DGV.RowsHierarchy, ACQUI.DGV.ColumnsHierarchy, account, ModelDataSet.DATASET_ENTITIES_PERIODS_OR)
        currentDGVOrientationFlag = ModelDataSet.DATASET_ENTITIES_PERIODS_OR

    End Sub

    ' Set up Periods in Rows, Entities in column configuration
    Friend Sub ConfigDGVpPeEn(ByRef account As String)

        ACQUI.DGV.Clear()
        ACQUI.LoadEntitiesToHierarchy(ACQUI.DGV.ColumnsHierarchy)
        Dim currentEntity As String = ACQUI.DGV.ColumnsHierarchy.Items(0).Caption
        ACQUI.LoadPeriodsToHierarchy(ACQUI.DGV.RowsHierarchy, currentEntity, account, ACQUMODEL.currentPeriodlist, GlobalVariables.Versions.versions_hash(ACQUMODEL.current_version_id)(VERSIONS_TIME_CONFIG_VARIABLE))

        ' -> download DBInputs -> for all entities 
        Dim entitiesList As New List(Of String)
        For Each item In DATASET.EntitiesAddressValuesDictionary.Values
            entitiesList.Add(item)
        Next
        ' -> we should keep this list ?
        ACQUMODEL.DownloadDBInputs(entitiesList, _
                                   GlobalVariables.ClientsIDDropDown.SelectedItemId, _
                                   GlobalVariables.ProductsIDDropDown.SelectedItemId, _
                                   GlobalVariables.AdjustmentIDDropDown.SelectedItemId)
        FillInSubItemEnPe(ACQUI.DGV.ColumnsHierarchy, ACQUI.DGV.RowsHierarchy, account, ModelDataSet.DATASET_PERIODS_ENTITIES_OR)
        currentDGVOrientationFlag = ModelDataSet.DATASET_PERIODS_ENTITIES_OR

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
            ElseIf ACQUMODEL.dataBaseInputsDictionary(currentEntity).ContainsKey(account.Caption) AndAlso _
                   ACQUMODEL.dataBaseInputsDictionary(currentEntity)(account.Caption).ContainsKey(periodAsInt) Then
                value = ACQUMODEL.dataBaseInputsDictionary(currentEntity)(account.Caption)(periodAsInt)
            Else
                value = Nothing
            End If
            Select Case configCode
                Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : ACQUI.DGV.CellsArea.SetCellValue(account, period, value)
                Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : ACQUI.DGV.CellsArea.SetCellValue(period, account, value)
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
                Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR : account.DataGridView.CellsArea.SetCellValue(account, entity, value)
                Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR : account.DataGridView.CellsArea.SetCellValue(entity, account, value)
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
                    Case ModelDataSet.DATASET_ENTITIES_PERIODS_OR : ACQUI.DGV.CellsArea.SetCellValue(entity, period, value)
                    Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR : ACQUI.DGV.CellsArea.SetCellValue(period, entity, value)
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
                value = ACQUMODEL.GetCalculatedValue(currentEntityName, accKey, periodsItemIDPeriodCodeDict(period.GetUniqueID))
                Select Case configCode
                    Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR : account.DataGridView.CellsArea.SetCellValue(account, period, value)
                    Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR : account.DataGridView.CellsArea.SetCellValue(period, account, value)
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

            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR, ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR
                accountHierarchy = ACQUI.DGV.RowsHierarchy
                secondHierarchy = ACQUI.DGV.ColumnsHierarchy

            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR, ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR
                accountHierarchy = ACQUI.DGV.ColumnsHierarchy
                secondHierarchy = ACQUI.DGV.RowsHierarchy

            Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR, ModelDataSet.DATASET_ENTITIES_PERIODS_OR
                Exit Sub

        End Select

        For Each accountItem As HierarchyItem In accountHierarchy.Items
            FillInCalculatedSubItemsAcPe(accountItem, secondHierarchy, entityName, currentDGVOrientationFlag)
        Next

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

    Friend Sub ClearDGVandDictionaries()

        ACQUI.DGV.Clear()
        rowsKeyItemDictionary.Clear()
        columnsKeyItemDictionary.Clear()
        editorKeyHierarchyDictionary.Clear()
        periodsItemIDPeriodCodeDict.Clear()

    End Sub

#End Region


End Class
