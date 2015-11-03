﻿' CAcquisitionModel.vb
'
' Upload database inputs, computes - serve calculations for DGV and Excel submission processes
'
' To do:
'       - !! pour l'instant complètement dépendant du dataset -> can t be used stand alone
'
'       - 2nd update sub (for ENT/PER orientation)
'       - Implementation of calculated items and DBInputs for AcEn and EnAc configs
'       - Default -> 3rd dimension = entities, when resetting dimensions we should rename DGVs with the 3rd dimension name 
'       - Format -> according to items (maybe should go into display) - > simple loop
'       - always same orientation as dataset ?
'       - Computations should go in separate class or display !
'       - format -> not always currency + currency is variable
'
' Known bugs:
'       - > shouldn't be activated for En|Pe or Pe|En orientations
'
'
' Author: Julien Monnereau
' Last modified: 01/09/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports VIBlend.Utilities
Imports System.Drawing
Imports Microsoft.Office.Interop
Imports System.Linq
Imports CRUD


Friend Class AcquisitionModel


#Region "Instance Variables"

    ' Objects
    Private Computer As New Computer
    Private SingleComputer As New ComputerInputEntity
    Private dataset As ModelDataSet

    ' Variables
    '(entity_name)(account_name)(period_token) => values
    Friend dataBaseInputsDictionary As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double)))
    Private computationDataMap As New Dictionary(Of Int32, Dictionary(Of Int32, Dictionary(Of String, Double)))
    Friend currentPeriodDict As Dictionary(Of Int32, List(Of Int32))
    Friend currentPeriodList() As Int32
    Friend outputsList As List(Of Account)
    Friend accountsTV As New TreeView
    Friend current_version_id As Int32
    Friend periodsIdentifyer As String

    ' Dll computation related
    Private accKeysArray() As Int32
    Private periodsArray() As Int32
    Private valuesArray() As Double

    ' Events
    Public Event AfterInputsDownloaded()
    Public Event AfterOutputsComputed(ByRef entityName As String)

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputDataSet As ModelDataSet)

        GlobalVariables.Accounts.LoadAccountsTV(accountsTV)
        dataset = inputDataSet

        outputsList = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS)

        AddHandler Computer.ComputationAnswered, AddressOf AfterInputsComputation
        AddHandler SingleComputer.ComputationAnswered, AddressOf AfterOuptutsComputed

    End Sub

#End Region


#Region "Inputs Download"

    Friend Sub downloadDBInputs(ByRef entitiesList As List(Of String), _
                                ByRef client_id As String, _
                                ByRef product_id As String, _
                                ByVal adjustment_id As String)

        ' caution !! priority high => if used duplication of peirods setup / versions in 
        ' !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        For Each entityName As String In entitiesList
            downloadDBInputs(entityName, _
                             client_id, _
                             product_id, _
                             adjustment_id)
        Next

    End Sub

    Friend Sub DownloadDBInputs(ByRef entityName As String, _
                                ByRef client_id As Int32, _
                                ByRef product_id As Int32, _
                                ByRef adjustment_id As Int32)

        Dim entity As EntityCurrency = GlobalVariables.EntityCurrencies.GetValue(entityName)
        Dim version As Version = GlobalVariables.Versions.GetValue(My.Settings.version_id)
        If entity Is Nothing OrElse version Is Nothing Then Exit Sub

        current_version_id = My.Settings.version_id
        currentPeriodDict = GlobalVariables.Versions.GetPeriodsDictionary(current_version_id)
        currentPeriodList = GlobalVariables.Versions.GetPeriodsList(current_version_id)
        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS : periodsIdentifyer = Computer.YEAR_PERIOD_IDENTIFIER
            Case CRUD.TimeConfig.MONTHS : periodsIdentifyer = Computer.MONTH_PERIOD_IDENTIFIER
        End Select

        ' Axis filters creation
        Dim axisFilters As New Dictionary(Of Int32, List(Of Int32))
        axisFilters.Add(GlobalEnums.AnalysisAxis.CLIENTS, New List(Of Int32))
        axisFilters.Add(GlobalEnums.AnalysisAxis.PRODUCTS, New List(Of Int32))
        axisFilters.Add(GlobalEnums.AnalysisAxis.ADJUSTMENTS, New List(Of Int32))
        axisFilters(GlobalEnums.AnalysisAxis.CLIENTS).Add(client_id)
        axisFilters(GlobalEnums.AnalysisAxis.PRODUCTS).Add(product_id)
        axisFilters(GlobalEnums.AnalysisAxis.ADJUSTMENTS).Add(adjustment_id)


        ' Computation order
        Computer.CMSG_COMPUTE_REQUEST({current_version_id}, _
                                      entity.Id, _
                                      entity.CurrencyId, _
                                      Nothing, _
                                      axisFilters, _
                                      Nothing)

    End Sub

    Private Sub AfterInputsComputation(ByRef entityId As Int32, ByRef status As ErrorMessage, ByRef requestId As Int32)

        If status = ErrorMessage.SUCCESS Then
            Dim entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, entityId)
            If entity Is Nothing Then Exit Sub

            If dataBaseInputsDictionary.ContainsKey(entity.Name) Then
                dataBaseInputsDictionary(entity.Name) = LoadDataMapIntoInputsDict(entityId, entity.Name, Computer.GetData)
            Else
                dataBaseInputsDictionary.Add(entity.Name, LoadDataMapIntoInputsDict(entityId, entity.Name, Computer.GetData))
            End If
            RaiseEvent AfterInputsDownloaded()
            ' Quid priority high -> do not raise event if all entities have not been downloaded !
        Else
            ' ? priority normal
        End If

    End Sub

    Private Function LoadDataMapIntoInputsDict(ByRef entityId As Int32, _
                                               ByRef entityName As String, _
                                               ByRef dataMap As Dictionary(Of String, Double)) As Dictionary(Of String, Dictionary(Of String, Double))

        Dim dataMapToken As New String("")
        Dim dataDict As New Dictionary(Of String, Dictionary(Of String, Double))
        Dim fixed_left_token As String = current_version_id & _
                                         Computer.TOKEN_SEPARATOR & _
                                         "0" & _
                                         Computer.TOKEN_SEPARATOR & _
                                         entityId

        ' select case input/ FPI
        For Each accountId As Int32 In TreeViewsUtilities.GetNodesKeysList(accountsTV)

            Dim l_account As Account = GlobalVariables.Accounts.GetValue(accountId)

            If l_account Is Nothing Then Continue For
            Select Case l_account.FormulaType

                Case Account.FormulaTypes.HARD_VALUE_INPUT

                    Dim accountName As String = l_account.Name
                    dataDict.Add(accountName, New Dictionary(Of String, Double))

                    ' Years
                    For Each yearId As Int32 In currentPeriodDict.Keys
                        dataMapToken = fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                        accountId & Computer.TOKEN_SEPARATOR & _
                                       Computer.YEAR_PERIOD_IDENTIFIER & yearId

                        If dataMap.ContainsKey(dataMapToken) Then
                            dataDict(accountName).Add(Computer.YEAR_PERIOD_IDENTIFIER & yearId, dataMap(dataMapToken))
                        Else
                            ' ? priority high
                            System.Diagnostics.Debug.WriteLine("DB inputs build: token not found, token = " & dataMapToken)
                        End If

                        ' Months
                        For Each monthId As Int32 In currentPeriodDict(yearId)
                            dataMapToken = fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                           accountId & Computer.TOKEN_SEPARATOR & _
                                           Computer.MONTH_PERIOD_IDENTIFIER & monthId

                            If dataMap.ContainsKey(dataMapToken) Then
                                dataDict(accountName).Add(Computer.MONTH_PERIOD_IDENTIFIER & monthId, dataMap(dataMapToken))
                            Else
                                System.Diagnostics.Debug.WriteLine("DB inputs build: token not found, token = " & dataMapToken)
                                ' ? priority high
                            End If
                        Next
                    Next

                Case Account.FormulaTypes.FIRST_PERIOD_INPUT

                    Dim accountName As String = l_account.Name
                    dataDict.Add(accountName, New Dictionary(Of String, Double))
                    Dim periodToken As String = ""

                    Select Case periodsIdentifyer
                        Case Computer.YEAR_PERIOD_IDENTIFIER
                            periodToken = Computer.YEAR_PERIOD_IDENTIFIER & currentPeriodDict.ElementAt(0).Key
                            dataMapToken = fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                                              accountId & Computer.TOKEN_SEPARATOR & _
                                                                periodToken

                        Case Computer.MONTH_PERIOD_IDENTIFIER
                            periodToken = Computer.MONTH_PERIOD_IDENTIFIER & currentPeriodDict.ElementAt(0).Key
                            dataMapToken = fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                                              accountId & Computer.TOKEN_SEPARATOR & _
                                                              Computer.YEAR_PERIOD_IDENTIFIER & currentPeriodDict.ElementAt(0).Value(0)
                    End Select
                    If dataMap.ContainsKey(dataMapToken) Then
                        dataDict(accountName).Add(periodToken, dataMap(dataMapToken))
                    Else
                        ' ? priority high
                        System.Diagnostics.Debug.WriteLine("DB inputs build: token not found, token = " & dataMapToken)
                    End If
            End Select
        Next
        Return dataDict

    End Function

    'Private Sub LoaddataBaseInputsDictionary(ByRef entityName As String)

    '    If Not dataBaseInputsDictionary.ContainsKey(entityName) Then
    '        Dim tmpDict As New Dictionary(Of Int32, Dictionary(Of String, Double))
    '        dataBaseInputsDictionary.Add(entityName, tmpDict)
    '    Else
    '        dataBaseInputsDictionary(entityName).Clear()
    '    End If


    'End Sub

#End Region


#Region "Outputs Computations"

    ' Launch Single Computation
    Friend Sub ComputeCalculatedItems(ByRef entityName As String)

        Dim entity As EntityCurrency = GlobalVariables.EntityCurrencies.GetValue(entityName)
        If entity Is Nothing Then Exit Sub
        BuildInputsArrays(entityName)
        SingleComputer.CMSG_SOURCED_COMPUTE(current_version_id, _
                                            entity.Id, _
                                            entity.CurrencyId, _
                                            accKeysArray, _
                                            periodsArray, _
                                            valuesArray)

    End Sub

    Private Sub AfterOuptutsComputed(ByRef entityId As Int32, ByRef status As Boolean)

        If computationDataMap.ContainsKey(entityId) Then
            computationDataMap(entityId) = SingleComputer.GetDataMap
        Else
            computationDataMap.Add(entityId, SingleComputer.GetDataMap)
        End If

        Dim l_entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, entityId)
        If l_entity Is Nothing Then Exit Sub
        If Not l_entity Is Nothing Then
            RaiseEvent AfterOutputsComputed(l_entity.Name)
        Else
            RaiseEvent AfterOutputsComputed("")
        End If

    End Sub

    ' Build datasource arrays (accKeys, periods, values)
    Private Sub BuildInputsArrays(ByRef p_entityName As String)

        Dim i As Integer
        ReDim accKeysArray(dataset.m_inputsAccountsList.Count * currentPeriodList.Length)
        ReDim periodsArray(dataset.m_inputsAccountsList.Count * currentPeriodList.Length)
        ReDim valuesArray(dataset.m_inputsAccountsList.Count * currentPeriodList.Length) 'legnth periods to be checked

        For Each inputAccount As Account In dataset.m_inputsAccountsList
            For Each period As Int32 In currentPeriodList

                accKeysArray(i) = inputAccount.Id
                periodsArray(i) = period

                Dim tuple_ As New Tuple(Of String, String, String)(p_entityName, inputAccount.Name, CStr(period))
                If dataset.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                    valuesArray(i) = dataset.m_datasetCellsDictionary(tuple_).Value2
                ElseIf dataBaseInputsDictionary(p_entityName).ContainsKey(inputAccount.Name) _
                AndAlso dataBaseInputsDictionary(p_entityName)(inputAccount.Name).ContainsKey(Trim(CStr(period))) Then
                    valuesArray(i) = dataBaseInputsDictionary(p_entityName)(inputAccount.Name)(Trim(CStr(period)))
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


#Region "Interface"

    Friend Function GetCalculatedValue(ByRef entityId As Int32, _
                                       ByRef accountId As Int32, _
                                       ByRef Period_token As String) As Double

        ' dataMap: [account_id][period_token] => value
        ' Manage case where value not found ? priority normal
        On Error GoTo ReturnError
        Return computationDataMap(entityId)(accountId)(Period_token)

ReturnError:

        'If computationDataMap.ContainsKey(entityId) = True Then
        '    If computationDataMap(entityId).ContainsKey(accountId) = True Then
        '        If computationDataMap(entityId)(accountId).ContainsKey(Period_token) = True Then
        '            Return 
        '        Else
        '            Return 0
        '        End If
        '    Else
        '        Return 0
        '    End If
        'Else
        '    Return 0
        'End If

    End Function

    Friend Sub ValuesDictionariesUpdate(ByRef p_entityName As String, _
                                        ByRef p_accountName As String, _
                                        ByRef p_period As String, _
                                        ByVal p_value As Double)

        ' -> should go back in dataset or controller !! no dataset here
        Dim tuple_ As New Tuple(Of String, String, String)(p_entityName, p_accountName, p_period)
        If dataset.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
            Dim cell As Excel.Range = dataset.m_datasetCellsDictionary(tuple_)
            Dim datasetCell As ModelDataSet.DataSetCellDimensions = dataset.m_datasetCellDimensionsDictionary(cell.Address)
            datasetCell.m_value = p_value
        End If
        If dataBaseInputsDictionary(p_entityName)(p_accountName).ContainsKey(p_period) Then
            dataBaseInputsDictionary(p_entityName)(p_accountName)(p_period) = p_value
        Else
            dataBaseInputsDictionary(p_entityName)(p_accountName).Add(p_period, p_value)
        End If

    End Sub

#End Region


#Region "Checks"

    Friend Function CheckIfFPICalculatedItem(ByRef accountName As String, ByRef period As Integer) As Boolean

        Dim l_account As Account = GlobalVariables.Accounts.GetValue(accountName)

        If l_account Is Nothing Then Return False
        If l_account.FormulaType = Account.FormulaTypes.FIRST_PERIOD_INPUT _
        AndAlso Not period = CInt(CDbl(dataset.m_periodsDatesList(0).ToOADate())) Then
            Return True
        Else
            Return False
        End If

    End Function


#End Region


End Class
