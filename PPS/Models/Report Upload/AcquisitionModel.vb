' CAcquisitionModel.vb
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
    Private m_computer As New Computer
    Private m_singleComputer As New ComputerInputEntity
    Private m_dataSet As ModelDataSet

    ' Variables
    '(entity_name)(account_name)(period_token) => values
    Friend m_databaseInputsDictionary As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Double)))
    Private m_computationDataMap As New Dictionary(Of Int32, Dictionary(Of Int32, Dictionary(Of String, Double)))
    Friend m_currentPeriodDict As Dictionary(Of Int32, List(Of Int32))
    Friend m_currentPeriodList() As Int32
    Friend m_outputsList As List(Of Account)
    Friend m_accountsTV As New TreeView
    Friend m_currentVersionId As Int32
    Friend m_periodsIdentifyer As String

    ' Dll computation related
    Private m_accKeysArray() As Int32
    Private m_periodsArray() As Int32
    Private m_valuesArray() As Double

    ' Events
    Public Event m_afterInputsDownloaded()
    Public Event m_afterOutputsComputed(ByRef entityName As String)

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputDataSet As ModelDataSet)

        GlobalVariables.Accounts.LoadAccountsTV(m_accountsTV)
        m_dataSet = inputDataSet

        m_outputsList = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS)

        AddHandler m_computer.ComputationAnswered, AddressOf AfterInputsComputation
        AddHandler m_singleComputer.ComputationAnswered, AddressOf AfterOuptutsComputed

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

        Dim entity As Entity = GlobalVariables.Entities.GetValue(entityName)
        Dim version As Version = GlobalVariables.Versions.GetValue(My.Settings.version_id)
        If entity Is Nothing OrElse version Is Nothing Then Exit Sub

        m_currentVersionId = My.Settings.version_id
        m_currentPeriodDict = GlobalVariables.Versions.GetPeriodsDictionary(m_currentVersionId)
        m_currentPeriodList = GlobalVariables.Versions.GetPeriodsList(m_currentVersionId)
        Select Case version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS : m_periodsIdentifyer = Computer.YEAR_PERIOD_IDENTIFIER
            Case CRUD.TimeConfig.MONTHS : m_periodsIdentifyer = Computer.MONTH_PERIOD_IDENTIFIER
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
        m_computer.CMSG_COMPUTE_REQUEST({m_currentVersionId}, _
                                      entity.Id, _
                                      entity.CurrencyId, _
                                      Nothing, _
                                      axisFilters, _
                                      Nothing)

    End Sub

    Private Sub AfterInputsComputation(ByRef entityId As Int32, ByRef status As ErrorMessage, ByRef requestId As Int32)

        If status = ErrorMessage.SUCCESS Then
            Dim entity As Entity = GlobalVariables.Entities.GetValue(entityId)
            If entity Is Nothing Then Exit Sub

            If m_databaseInputsDictionary.ContainsKey(entity.Name) Then
                m_databaseInputsDictionary(entity.Name) = LoadDataMapIntoInputsDict(entityId, entity.Name, m_computer.GetData)
            Else
                m_databaseInputsDictionary.Add(entity.Name, LoadDataMapIntoInputsDict(entityId, entity.Name, m_computer.GetData))
            End If
            RaiseEvent m_afterInputsDownloaded()
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
        Dim fixed_left_token As String = m_currentVersionId & _
                                         Computer.TOKEN_SEPARATOR & _
                                         "0" & _
                                         Computer.TOKEN_SEPARATOR & _
                                         entityId

        ' select case input/ FPI
        For Each accountId As Int32 In TreeViewsUtilities.GetNodesKeysList(m_accountsTV)

            Dim l_account As Account = GlobalVariables.Accounts.GetValue(accountId)

            If l_account Is Nothing Then Continue For
            Select Case l_account.FormulaType

                Case Account.FormulaTypes.HARD_VALUE_INPUT

                    Dim accountName As String = l_account.Name
                    dataDict.Add(accountName, New Dictionary(Of String, Double))

                    ' Years
                    For Each yearId As Int32 In m_currentPeriodDict.Keys
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
                        For Each monthId As Int32 In m_currentPeriodDict(yearId)
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

                    Select Case m_periodsIdentifyer
                        Case Computer.YEAR_PERIOD_IDENTIFIER
                            periodToken = Computer.YEAR_PERIOD_IDENTIFIER & m_currentPeriodDict.ElementAt(0).Key
                            dataMapToken = fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                                              accountId & Computer.TOKEN_SEPARATOR & _
                                                                periodToken

                        Case Computer.MONTH_PERIOD_IDENTIFIER
                            periodToken = Computer.MONTH_PERIOD_IDENTIFIER & m_currentPeriodDict.ElementAt(0).Key
                            dataMapToken = fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                                              accountId & Computer.TOKEN_SEPARATOR & _
                                                              Computer.YEAR_PERIOD_IDENTIFIER & m_currentPeriodDict.ElementAt(0).Value(0)
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

        Dim entity As Entity = GlobalVariables.Entities.GetValue(entityName)
        If entity Is Nothing Then Exit Sub
        BuildInputsArrays(entity.Name)
        m_singleComputer.CMSG_SOURCED_COMPUTE(m_currentVersionId, _
                                            entity.Id, _
                                            entity.CurrencyId, _
                                            m_accKeysArray, _
                                            m_periodsArray, _
                                            m_valuesArray)

    End Sub

    Private Sub AfterOuptutsComputed(ByRef entityId As Int32, ByRef status As Boolean)

        If m_computationDataMap.ContainsKey(entityId) Then
            m_computationDataMap(entityId) = m_singleComputer.GetDataMap
        Else
            m_computationDataMap.Add(entityId, m_singleComputer.GetDataMap)
        End If

        Dim l_entity As Entity = GlobalVariables.Entities.GetValue(entityId)
        If l_entity Is Nothing Then Exit Sub
        If Not l_entity Is Nothing Then
            RaiseEvent m_afterOutputsComputed(l_entity.Name)
        Else
            RaiseEvent m_afterOutputsComputed("")
        End If

    End Sub

    ' Build datasource arrays (accKeys, periods, values)
    Private Sub BuildInputsArrays(ByRef p_entityName As String)

        Dim i As Integer
        ReDim m_accKeysArray(m_dataSet.m_inputsAccountsList.Count * m_currentPeriodList.Length)
        ReDim m_periodsArray(m_dataSet.m_inputsAccountsList.Count * m_currentPeriodList.Length)
        ReDim m_valuesArray(m_dataSet.m_inputsAccountsList.Count * m_currentPeriodList.Length) 'legnth periods to be checked

        For Each inputAccount As Account In m_dataSet.m_inputsAccountsList
            For Each period As Int32 In m_currentPeriodList

                m_accKeysArray(i) = inputAccount.Id
                m_periodsArray(i) = period

                Dim tuple_ As New Tuple(Of String, String, String)(p_entityName, inputAccount.Name, CStr(period))
                If m_dataSet.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                    m_valuesArray(i) = m_dataSet.m_datasetCellsDictionary(tuple_).Value2
                ElseIf m_databaseInputsDictionary(p_entityName).ContainsKey(inputAccount.Name) _
                AndAlso m_databaseInputsDictionary(p_entityName)(inputAccount.Name).ContainsKey(Trim(CStr(period))) Then
                    m_valuesArray(i) = m_databaseInputsDictionary(p_entityName)(inputAccount.Name)(Trim(CStr(period)))
                Else
                    m_valuesArray(i) = 0
                End If
                i = i + 1
            Next
        Next

        ReDim Preserve m_accKeysArray(i - 1)
        ReDim Preserve m_periodsArray(i - 1)
        ReDim Preserve m_valuesArray(i - 1)

    End Sub


#End Region


#Region "Interface"

    Friend Function GetCalculatedValue(ByRef entityId As Int32, _
                                       ByRef accountId As Int32, _
                                       ByRef Period_token As String) As Double

        ' dataMap: [account_id][period_token] => value
        ' Manage case where value not found ? priority normal
        On Error GoTo ReturnError
        Return m_computationDataMap(entityId)(accountId)(Period_token)

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
        If m_dataSet.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
            Dim cell As Excel.Range = m_dataSet.m_datasetCellsDictionary(tuple_)
            Dim datasetCell As ModelDataSet.DataSetCellDimensions = m_dataSet.m_datasetCellDimensionsDictionary(cell.Address)
            datasetCell.m_value = p_value
        End If
        If m_databaseInputsDictionary(p_entityName)(p_accountName).ContainsKey(p_period) Then
            m_databaseInputsDictionary(p_entityName)(p_accountName)(p_period) = p_value
        Else
            m_databaseInputsDictionary(p_entityName)(p_accountName).Add(p_period, p_value)
        End If

    End Sub

#End Region


#Region "Checks"

    Friend Function CheckIfFPICalculatedItem(ByRef accountName As String, ByRef period As Integer) As Boolean

        Dim l_account As Account = GlobalVariables.Accounts.GetValue(accountName)

        If l_account Is Nothing Then Return False
        If l_account.FormulaType = Account.FormulaTypes.FIRST_PERIOD_INPUT _
        AndAlso Not period = CInt(CDbl(m_dataSet.m_periodsDatesList(0).ToOADate())) Then
            Return True
        Else
            Return False
        End If

    End Function


#End Region


End Class
