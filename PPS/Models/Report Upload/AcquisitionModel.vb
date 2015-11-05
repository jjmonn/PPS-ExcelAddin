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
    Private m_entitiesNamesInputsList As New List(Of String)
    Private m_entitiesNameOutputList As New List(Of String)
    Private m_entitiesIdOutputList As New List(Of Int32)

    ' Dll computation related
    Private m_entitiesIdInputsAccounts As New Dictionary(Of Int32, Int32())
    Private m_entitiesIdInputsPeriods As New Dictionary(Of Int32, Int32())
    Private m_entitiesIdInputsValues As New Dictionary(Of Int32, Double())

    Private m_inputsDownloadFailureFlag As Boolean = False
    Private m_outputsComputationFailureFlag As Boolean = False

    ' Events
    Public Event m_afterInputsDownloaded(ByRef p_status As Boolean)
    Public Event m_afterOutputsComputed(ByRef p_entitiesName() As String)

#End Region


#Region "Initialize"

    Friend Sub New(ByRef inputDataSet As ModelDataSet)

        GlobalVariables.Accounts.LoadAccountsTV(m_accountsTV)
        m_dataSet = inputDataSet

        m_outputsList = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_OUTPUTS)

        AddHandler m_computer.ComputationAnswered, AddressOf AfterInputsComputation
        AddHandler m_singleComputer.ComputationAnswered, AddressOf AfterOutputsComputed

    End Sub

#End Region


#Region "Inputs Download"

    Friend Sub DownloadInputsFromServer(ByRef p_entitiesList As List(Of String), _
                                        ByRef p_client_id As String, _
                                        ByRef p_product_id As String, _
                                        ByVal p_adjustment_id As String)

        m_inputsDownloadFailureFlag = False
        Dim l_version As Version = GlobalVariables.Versions.GetValue(My.Settings.version_id)
        If l_version Is Nothing Then
            MsgBox("There was an issue with the selected version.")
            m_inputsDownloadFailureFlag = True
            Exit Sub
        End If

        m_currentVersionId = My.Settings.version_id
        m_currentPeriodDict = GlobalVariables.Versions.GetPeriodsDictionary(m_currentVersionId)
        m_currentPeriodList = GlobalVariables.Versions.GetPeriodsList(m_currentVersionId)
        Select Case l_version.TimeConfiguration
            Case CRUD.TimeConfig.YEARS : m_periodsIdentifyer = Computer.YEAR_PERIOD_IDENTIFIER
            Case CRUD.TimeConfig.MONTHS : m_periodsIdentifyer = Computer.MONTH_PERIOD_IDENTIFIER
        End Select

        ' Entities List creation
        m_entitiesNamesInputsList = p_entitiesList
        Dim l_entitiesId As New List(Of Int32)
        For Each l_entityName As String In m_entitiesNamesInputsList
            Dim l_entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, l_entityName)
            On Error Resume Next
            l_entitiesId.Add(l_entity.Id)
        Next

        ' Axis filters creation
        Dim l_axisFilters As New Dictionary(Of Int32, List(Of Int32))
        l_axisFilters.Add(GlobalEnums.AnalysisAxis.CLIENTS, New List(Of Int32))
        l_axisFilters.Add(GlobalEnums.AnalysisAxis.PRODUCTS, New List(Of Int32))
        l_axisFilters.Add(GlobalEnums.AnalysisAxis.ADJUSTMENTS, New List(Of Int32))
        l_axisFilters(GlobalEnums.AnalysisAxis.CLIENTS).Add(p_client_id)
        l_axisFilters(GlobalEnums.AnalysisAxis.PRODUCTS).Add(p_product_id)
        l_axisFilters(GlobalEnums.AnalysisAxis.ADJUSTMENTS).Add(p_adjustment_id)

        ' Actual Computation
        m_computer.CMSG_COMPUTE_REQUEST({m_currentVersionId}, _
                                        l_entitiesId, _
                                        0, _
                                        Nothing, _
                                        l_axisFilters, _
                                        Nothing)

    End Sub

    Private Sub AfterInputsComputation(ByRef p_entityId As Int32, _
                                       ByRef p_status As ErrorMessage, _
                                       ByRef p_requestId As Int32)

        If m_inputsDownloadFailureFlag = False Then
            If p_status = ErrorMessage.SUCCESS Then
                For Each l_entityName As String In m_entitiesNamesInputsList
                    Dim l_entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, l_entityName)
                    If Not l_entity Is Nothing Then
                        If m_databaseInputsDictionary.ContainsKey(l_entity.Name) Then
                            m_databaseInputsDictionary(l_entity.Name) = LoadDataMapIntoInputsDict(p_entityId, l_entity.Name, m_computer.GetData)
                        Else
                            m_databaseInputsDictionary.Add(l_entity.Name, LoadDataMapIntoInputsDict(p_entityId, l_entity.Name, m_computer.GetData))
                        End If
                    End If
                Next
                ' End of inputs entities to be computed
                RaiseEvent m_afterInputsDownloaded(True)
            Else
                MsgBox(Local.GetValue("upload.msg_entity_not_computed1"))
                m_inputsDownloadFailureFlag = True
                RaiseEvent m_afterInputsDownloaded(False)
            End If
        End If

    End Sub

    Private Function LoadDataMapIntoInputsDict(ByRef p_entityId As Int32, _
                                               ByRef p_entityName As String, _
                                               ByRef p_dataMap As Dictionary(Of String, Double)) As Dictionary(Of String, Dictionary(Of String, Double))

        Dim l_dataMapToken As New String("")
        Dim l_dataDict As New Dictionary(Of String, Dictionary(Of String, Double))
        Dim l_fixed_left_token As String = m_currentVersionId & _
                                         Computer.TOKEN_SEPARATOR & _
                                         "0" & _
                                         Computer.TOKEN_SEPARATOR & _
                                         p_entityId

        ' select case input/ FPI
        For Each l_accountId As Int32 In TreeViewsUtilities.GetNodesKeysList(m_accountsTV)

            Dim l_account As Account = GlobalVariables.Accounts.GetValue(l_accountId)
            If l_account Is Nothing Then Continue For

            Select Case l_account.FormulaType

                Case Account.FormulaTypes.HARD_VALUE_INPUT

                    Dim accountName As String = l_account.Name
                    l_dataDict.Add(accountName, New Dictionary(Of String, Double))

                    ' Years
                    For Each l_yearId As Int32 In m_currentPeriodDict.Keys
                        l_dataMapToken = l_fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                        l_accountId & Computer.TOKEN_SEPARATOR & _
                                       Computer.YEAR_PERIOD_IDENTIFIER & l_yearId

                        If p_dataMap.ContainsKey(l_dataMapToken) Then
                            l_dataDict(accountName).Add(Computer.YEAR_PERIOD_IDENTIFIER & l_yearId, p_dataMap(l_dataMapToken))
                        Else
                            '  priority high
                            System.Diagnostics.Debug.WriteLine("DB inputs build: token not found, token = " & l_dataMapToken)
                        End If

                        ' Months
                        For Each l_monthId As Int32 In m_currentPeriodDict(l_yearId)
                            l_dataMapToken = l_fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                           l_accountId & Computer.TOKEN_SEPARATOR & _
                                           Computer.MONTH_PERIOD_IDENTIFIER & l_monthId

                            If p_dataMap.ContainsKey(l_dataMapToken) Then
                                l_dataDict(accountName).Add(Computer.MONTH_PERIOD_IDENTIFIER & l_monthId, p_dataMap(l_dataMapToken))
                            Else
                                System.Diagnostics.Debug.WriteLine("DB inputs build: token not found, token = " & l_dataMapToken)
                                ' ? priority high
                            End If
                        Next
                    Next

                Case Account.FormulaTypes.FIRST_PERIOD_INPUT

                    Dim accountName As String = l_account.Name
                    l_dataDict.Add(accountName, New Dictionary(Of String, Double))
                    Dim l_periodToken As String = ""

                    Select Case m_periodsIdentifyer
                        Case Computer.YEAR_PERIOD_IDENTIFIER
                            l_periodToken = Computer.YEAR_PERIOD_IDENTIFIER & m_currentPeriodDict.ElementAt(0).Key
                            l_dataMapToken = l_fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                             l_accountId & Computer.TOKEN_SEPARATOR & _
                                             l_periodToken

                        Case Computer.MONTH_PERIOD_IDENTIFIER
                            l_periodToken = Computer.MONTH_PERIOD_IDENTIFIER & m_currentPeriodDict.ElementAt(0).Key
                            l_dataMapToken = l_fixed_left_token & Computer.TOKEN_SEPARATOR & _
                                             l_accountId & Computer.TOKEN_SEPARATOR & _
                                             Computer.YEAR_PERIOD_IDENTIFIER & m_currentPeriodDict.ElementAt(0).Value(0)
                    End Select
                    If p_dataMap.ContainsKey(l_dataMapToken) Then
                        l_dataDict(accountName).Add(l_periodToken, p_dataMap(l_dataMapToken))
                    Else
                        '  priority high
                        System.Diagnostics.Debug.WriteLine("DB inputs build: token not found, token : " & l_dataMapToken)
                    End If
            End Select
        Next
        Return l_dataDict

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
    Friend Sub ComputeCalculatedItems(Optional ByRef p_entityName As String = "")

        Dim l_entitiesNames() As String
        If p_entityName = "" Then
            l_entitiesNames = m_entitiesNamesInputsList.ToArray
        Else
            l_entitiesNames = {p_entityName}
        End If

        m_outputsComputationFailureFlag = False
        m_entitiesNameOutputList = l_entitiesNames.ToList
        m_entitiesIdOutputList.Clear()
        For Each l_entityName As String In m_entitiesNameOutputList
            Dim l_entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, l_entityName)
            If Not l_entity Is Nothing Then
                m_entitiesIdOutputList.Add(l_entity.Id)
            End If
        Next

        BuildInputsArrays(m_entitiesNameOutputList)
        m_singleComputer.CMSG_SOURCED_COMPUTE(m_currentVersionId, _
                                              m_entitiesIdOutputList, _
                                              m_entitiesIdInputsAccounts, _
                                              m_entitiesIdInputsPeriods, _
                                              m_entitiesIdInputsValues)

    End Sub

    Private Sub AfterOutputsComputed(ByRef p_status As Boolean)

        If p_status = True Then
            For Each l_entityId As Int32 In m_entitiesIdOutputList
                If m_computationDataMap.ContainsKey(l_entityId) Then
                    m_computationDataMap(l_entityId) = m_singleComputer.GetDataMap(l_entityId)
                Else
                    m_computationDataMap.Add(l_entityId, m_singleComputer.GetDataMap(l_entityId))
                End If
                m_singleComputer.RemoveEntityDataFromDataMap(l_entityId)
            Next
            RaiseEvent m_afterOutputsComputed(m_entitiesNameOutputList.ToArray)
        Else
            RaiseEvent m_afterOutputsComputed(Nothing)
        End If

    End Sub

    ' Build datasource arrays (accKeys, periods, values)
    Private Sub BuildInputsArrays(ByRef p_entitiesNames As List(Of String))

        m_entitiesIdInputsAccounts.Clear()
        m_entitiesIdInputsPeriods.Clear()
        m_entitiesIdInputsValues.Clear()

        For Each l_entityName As String In p_entitiesnames

            Dim l_entity As AxisElem = GlobalVariables.AxisElems.GetValue(AxisType.Entities, l_entityName)
            If Not l_entity Is Nothing Then
                Dim l_entityId As Int32 = l_entity.Id

                ReDim m_entitiesIdInputsAccounts(l_entityId)(m_dataSet.m_inputsAccountsList.Count * m_currentPeriodList.Length)
                ReDim m_entitiesIdInputsPeriods(l_entityId)(m_dataSet.m_inputsAccountsList.Count * m_currentPeriodList.Length)
                ReDim m_entitiesIdInputsValues(l_entityId)(m_dataSet.m_inputsAccountsList.Count * m_currentPeriodList.Length)

                Dim i As Integer = 0
                For Each inputAccount As Account In m_dataSet.m_inputsAccountsList
                    For Each period As Int32 In m_currentPeriodList

                        m_entitiesIdInputsAccounts(l_entityId)(i) = inputAccount.Id
                        m_entitiesIdInputsPeriods(l_entityId)(i) = period

                        Dim tuple_ As New Tuple(Of String, String, String)(l_entityName, inputAccount.Name, CStr(period))
                        If m_dataSet.m_datasetCellsDictionary.ContainsKey(tuple_) = True Then
                            m_entitiesIdInputsValues(l_entityId)(i) = m_dataSet.m_datasetCellsDictionary(tuple_).Value2
                        ElseIf m_databaseInputsDictionary(l_entityName).ContainsKey(inputAccount.Name) _
                        AndAlso m_databaseInputsDictionary(l_entityName)(inputAccount.Name).ContainsKey(Trim(CStr(period))) Then
                            m_entitiesIdInputsValues(l_entityId)(i) = m_databaseInputsDictionary(l_entityName)(inputAccount.Name)(Trim(CStr(period)))
                        Else
                            m_entitiesIdInputsValues(l_entityId)(i) = 0
                        End If
                        i = i + 1
                    Next
                Next
                ReDim Preserve m_entitiesIdInputsAccounts(l_entityId)(i - 1)
                ReDim Preserve m_entitiesIdInputsPeriods(l_entityId)(i - 1)
                ReDim Preserve m_entitiesIdInputsValues(l_entityId)(i - 1)
            End If
        Next

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
        System.Diagnostics.Debug.WriteLine("Acquisition model returned error: the value was not in the m_computationDataMap.")
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
