' CAcquisitionModel.vb
'
' Upload database inputs, computes - serve calculations for DGV and Excel submission processes
'
' To do:
'       - !! pour l'instant complètement dépendant du DATASET -> can t be used stand alone
'
'       - 2nd update sub (for ENT/PER orientation)
'       - Implementation of calculated items and DBInputs for AcEn and EnAc configs
'       - Default -> 3rd dimension = entities, when resetting dimensions we should rename DGVs with the 3rd dimension name 
'       - Format -> according to items (maybe should go into display) - > simple loop
'       - always same orientation as DATASET ?
'       - Computations should go in separate class or display !
'       - format -> not always currency + currency is variable
'
' Known bugs:
'       - > shouldn't be activated for En|Pe or Pe|En orientations
'
'
' Author: Julien Monnereau
' Last modified: 25/06/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.Utilities
Imports System.Drawing
Imports Microsoft.Office.Interop


Friend Class AcquisitionModel


#Region "Instance Variables"

    ' Objects
    Private DATASET As ModelDataSet
    Private CCOMPUTERINT As DLL3_Interface
    Private DBDownloader As DataBaseDataDownloader

    ' Variables
    Friend DBInputsDictionary As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double)))    '(entities)(accounts)(periods) -> values
    Friend currentPeriodlist As New List(Of Integer)
    Friend outputsList As List(Of String)
    Protected Friend versionsTimeConfigDict As Hashtable
    Friend accountsTV As New TreeView
    Private accountsNamesFormulaTypeDict As Hashtable

    Protected Friend current_version_id As String
    
    ' Dll computation related
    Private accKeysArray() As String
    Private periodsArray() As Integer
    Private valuesArray() As Double

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputDataSet As ModelDataSet, _
                   ByRef inputDBDownloader As DataBaseDataDownloader, _
                   ByRef inputComputerInt As DLL3_Interface)

        DBDownloader = inputDBDownloader
        Account.LoadAccountsTree(accountsTV)
        DATASET = inputDataSet
        CCOMPUTERINT = inputComputerInt

        versionsTimeConfigDict = VersionsMapping.GetVersionsHashTable(VERSIONS_CODE_VARIABLE, VERSIONS_TIME_CONFIG_VARIABLE)
        accountsNamesFormulaTypeDict = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)
        outputsList = AccountsMapping.GetAccountsNamesList(AccountsMapping.LOOKUP_OUTPUTS)

    End Sub

#End Region


#Region "Interface"

    Friend Sub DownloadDBInputs(ByRef entityName As String, _
                                ByRef client_id As String, _
                                ByRef product_id As String, _
                                ByRef adjustment_id As String)

        ' -> we shouldn't use dataset  in the model !
        '
        Dim entityKey As String = DATASET.EntitiesNameKeyDictionary(entityName)
        current_version_id = GlobalVariables.GLOBALCurrentVersionCode

        Dim Versions As New Version
        currentPeriodlist = Versions.GetPeriodList(current_version_id)

        Dim viewName = current_version_id
        DBDownloader.GetEntityInputsNonConverted(entityKey, _
                                                 viewName, _
                                                 Utilities_Functions.getStringsList({client_id}), _
                                                 Utilities_Functions.getStringsList({product_id}), _
                                                 Utilities_Functions.getStringsList({adjustment_id}))
        LoadDBInputsDictionary(entityName)

    End Sub

    Friend Sub downloadDBInputs(ByRef entitiesList As List(Of String), _
                                ByRef client_id As String, _
                                ByRef product_id As String, _
                                ByVal adjustment_id As String)

        For Each entityName As String In entitiesList
            DownloadDBInputs(entityName, _
                             client_id, _
                             product_id, _
                             adjustment_id)
        Next

    End Sub

    Friend Function GetCalculatedValue(ByRef accKey As String, _
                                       ByRef PeriodInt As Integer) As Double

        Return CCOMPUTERINT.GetDataFromComputer(accKey, PeriodInt)

    End Function

    Friend Sub ValuesDictionariesUpdate(ByRef entityName As String, _
                                        ByRef accountName As String, _
                                        ByRef periodInt As String, _
                                        ByVal value As Double)

        ' -> should go back in dataset or controller !! no dataset here
        If DATASET.dataSetDictionary(entityName).ContainsKey(accountName) Then
            DATASET.dataSetDictionary(entityName)(accountName)(periodInt) = value
        End If
        If DBInputsDictionary(entityName)(accountName).ContainsKey(periodInt) Then
            DBInputsDictionary(entityName)(accountName)(periodInt) = value
        Else
            DBInputsDictionary(entityName)(accountName).Add(periodInt, value)
        End If

    End Sub

#End Region


#Region "Computation"

    Private Sub LoadDBInputsDictionary(ByRef entityName As String)

        If Not DBInputsDictionary.ContainsKey(entityName) Then
            Dim tmpDict As New Dictionary(Of String, Dictionary(Of String, Double))
            DBInputsDictionary.Add(entityName, tmpDict)
        Else
            DBInputsDictionary(entityName).Clear()
        End If

        If Not DBDownloader.AccKeysArray Is Nothing Then
            For i As Int32 = 0 To DBDownloader.AccKeysArray.Length - 1
                Dim accountName As String = accountsTV.Nodes.Find(DBDownloader.AccKeysArray(i), True)(0).Text

                If Not DBInputsDictionary(entityName).ContainsKey(accountName) Then
                    Dim tmpDict As New Dictionary(Of String, Double)
                    DBInputsDictionary(entityName).Add(accountName, tmpDict)
                End If
                DBInputsDictionary(entityName)(accountName).Add(CStr(DBDownloader.PeriodArray(i)), DBDownloader.ValuesArray(i))
            Next
        End If
        FillInWithZeroValuesItemsNotPresentInDB(entityName)

    End Sub

    ' Complete DBInputsDcitionary with null values where data not in data base
    Private Sub FillInWithZeroValuesItemsNotPresentInDB(ByRef entity As String)

        For Each account As String In DATASET.inputsAccountsList
            If DBInputsDictionary(entity).ContainsKey(account) Then
                For Each period As String In currentPeriodlist
                    If Not DBInputsDictionary(entity)(account).ContainsKey(period) Then
                        DBInputsDictionary(entity)(account).Add(period, 0)
                    End If
                Next
            Else
                Dim tmpDic As New Dictionary(Of String, Double)
                For Each period As String In currentPeriodlist
                    tmpDic.Add(period, 0)
                Next
                DBInputsDictionary(entity).Add(account, tmpDic)
            End If
        Next

    End Sub

    ' Launch dll computation - Reinit periods if periods configuration is different
    Friend Sub ComputeCalculatedItems(ByRef entity As String)

        Dim entityKey As String = DATASET.EntitiesNameKeyDictionary(entity)
        BuildInputsArrays(entity)

        Dim currentVersionTimeConfig As String = versionsTimeConfigDict(current_version_id)
        If CCOMPUTERINT.dll3TimeSetup <> currentVersionTimeConfig Then
            Dim Versions As New Version
            currentPeriodlist = Versions.GetPeriodList(current_version_id)
            Versions.Close()
            CCOMPUTERINT.InitDllPeriods(currentPeriodlist, currentVersionTimeConfig)
        End If

        CCOMPUTERINT.ComputeSingleEntity(entityKey, accKeysArray, periodsArray, valuesArray)

    End Sub

    ' Build datasource arrays (accKeys, periods, values)
    Private Sub BuildInputsArrays(ByRef entity)

        Dim i As Integer
        ReDim accKeysArray(DATASET.inputsAccountsList.Count * currentPeriodlist.Count + 1)
        ReDim periodsArray(DATASET.inputsAccountsList.Count * currentPeriodlist.Count + 1)
        ReDim valuesArray(DATASET.inputsAccountsList.Count * currentPeriodlist.Count + 1)

        For Each inputAccount As String In DATASET.inputsAccountsList
            For Each period In currentPeriodlist

                accKeysArray(i) = DATASET.AccountsNameKeyDictionary(inputAccount)
                periodsArray(i) = period

                If DATASET.dataSetDictionary.ContainsKey(entity) _
                AndAlso DATASET.dataSetDictionary(entity).ContainsKey(inputAccount) _
                AndAlso DATASET.dataSetDictionary(entity)(inputAccount).ContainsKey(Trim(CStr(period))) Then
                    valuesArray(i) = DATASET.dataSetDictionary(entity)(inputAccount)(Trim(CStr(period)))
                ElseIf DBInputsDictionary(entity).ContainsKey(inputAccount) _
                AndAlso DBInputsDictionary(entity)(inputAccount).ContainsKey(Trim(CStr(period))) Then
                    valuesArray(i) = DBInputsDictionary(entity)(inputAccount)(Trim(CStr(period)))
                Else
                    valuesArray(i) = 0
                End If
                i = i + 1
            Next
        Next

        ReDim Preserve accKeysArray(i - 1)
        ReDim Preserve periodsArray(i - 1)
        ReDim Preserve valuesArray(i - 1)

    End Sub

#End Region


#Region "Checks"

    Friend Function CheckIfBSCalculatedItem(ByRef accountName As String, ByRef period As Integer) As Boolean

        If accountsNamesFormulaTypeDict(accountName) = BALANCE_SHEET_ACCOUNT_FORMULA_TYPE _
        AndAlso Not period = CInt(CDbl(DATASET.periodsDatesList(0).ToOADate())) Then
            Return True
        Else
            Return False
        End If

    End Function


#End Region


End Class
