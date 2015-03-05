' Cc_ComputerInterface.vb
' 
' Hold a DLL computer, compute from data/account/period arrays, take back data from computer
' Complementary with a data/account/periods array builder
'
' To do: 
'       - Destroy Model when excel exits !!
'       - Attention les périodes sont réinitialisées à chaque entity aggregation + non enregistré !!!
'
' Known Bugs:
'       -
'
'
' Last modified: 21/01/2015
' Author: Julien Monnereau


Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports System.Linq
Imports System.Collections.Generic
Imports System.Collections


Friend Class DLL3_Interface


#Region " Instance Variables"

    ' Objects
    Private objptr As Integer

    ' Variables
    Friend currentSingleEntityKey As String
    Friend dll3TimeSetup As String                  ' MONTHLY_TIME_CONFIGURATION or YEARLY_TIME_CONFIGURATION
    Private all_entities_ids As List(Of String)
    Friend accounts_array() As String
    Friend convertor_currencies_token_list As New List(Of String)
    Friend entities_currencies As New List(Of String)
    Protected Friend current_start_period As Int32 = 0
    Protected Friend current_nb_periods As Int32 = 0
    Private isModelAlive As Boolean = False

#End Region


#Region "DLL3 Functions"

#Region "Common"

    <DllImport("DLL3.dll")>
    Private Shared Function CreateDll3() As Integer
    End Function

    <DllImport("DLL3.dll")>
    Private Shared Sub DestroyDll3(ByVal objptr As Integer)
    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Function initModelDll3(ByVal objptr As Integer, _
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef formulasTable() As String, _
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef formulaCodesTable() As Integer, _
                                          tablesLenght As Integer, _
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef accKeys() As String, _
                                          accNb As Integer, _
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef accParentKeys() As String, _
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef accFrmTypes() As String, _
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef accFormulas() As String,
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef accConversionFlags() As String,
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef recomputed_accounts_ids() As String,
                                         nb_recomputed_accounts As Integer) As Integer
    End Function

    <DllImport("DLL3.dll")>
    Private Shared Sub initPeriodsDll3(ByVal objptr As Integer, _
                                       <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef periodsArray() As Integer, _
                                       nbPeriods As Integer)
    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Function CheckParserFormulaDll3(ByVal objptr As Integer, _
                                                   <MarshalAs(UnmanagedType.BStr)> ByVal formula As String) As Int32
    End Function

#End Region

#Region "CC and CCube calculations"

    <DllImport("DLL3.dll")>
    Private Shared Sub initOutputDll3(ByVal objptr As Integer, nbEntities As Integer, option_ As Integer)
    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub computeEntityDll3(ByVal objptr As Integer, _
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef dataAccKeys() As String, _
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef dataPeriods() As Integer, _
                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> ByRef dataValues() As Double, _
                                         nbRecords As Integer, _
                                         <MarshalAs(UnmanagedType.BStr)> ByVal currency As String, storage_option As Integer)
    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub RegisterEmptyEntityDLL3(ByVal objptr As Integer, _
                                              <MarshalAs(UnmanagedType.BStr)> ByVal currency As String)
    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Function ReturnDataArrayDLL3(<MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_VARIANT)> _
                                                <System.Runtime.InteropServices.Out()> ByRef Output(,,) As Object, _
                                                 ByVal objptr As Integer) As Integer
    End Function

    <DllImport("DLL3.dll")>
    Private Shared Function ReturnValueFromModelDLL3(ByVal objptr As Integer, _
                                                     <MarshalAs(UnmanagedType.BStr)> ByVal AccountKey As String, _
                                                     ByVal Period As Integer, _
                                                     <MarshalAs(UnmanagedType.BStr)> ByVal Currency As String) As Double
    End Function

#End Region

#Region "Entities Aggregation"

    <DllImport("DLL3.dll")>
    Private Shared Sub initializeCurrencyConvertorDll3(ByVal objptr As Integer, _
                                                       <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef inputPeriodsArray() As Integer, _
                                                       ByVal nb_periods As Integer)


    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub AddPeriodYearCurrencyConvertorDll3(ByVal objptr As Integer, _
                                                      <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef monthlyPeriodsArray() As Integer)
    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub AddYearlyMatrixCurrencyDll3(ByVal objptr As Integer, _
                                                          <MarshalAs(UnmanagedType.BStr)> ByVal currency_token As String, _
                                                          <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef periods() As Integer, _
                                                           <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> ByRef rates() As Double, _
                                                           nbRecords As Integer)

    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub AddMonthlyMatrixCurrencyDll3(ByVal objptr As Integer, _
                                                    <MarshalAs(UnmanagedType.BStr)> ByVal currency_token As String, _
                                                    <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> ByRef rates() As Double)

    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub InitializeEntitiesAggregationDll3(ByVal objptr As Integer, _
                                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef entities_array() As String, _
                                                         <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef entities_currencies() As String, _
                                                         ByVal nb_entities As Integer)

    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub SetUpBeforeComputeDll3(ByVal objptr As Integer, _
                                              <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef inputPeriodsArray() As Integer, _
                                              ByVal nb_periods As Integer, _
                                              <MarshalAs(UnmanagedType.BStr)> ByVal main_currency As String, _
                                              <MarshalAs(UnmanagedType.BStr)> ByVal time_config As String, _
                                              <MarshalAs(UnmanagedType.BStr)> ByVal rates_version As String, _
                                              ByVal ref_period As Integer)
    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub AddEntityToEntitiesAggregationHierarchyDll3(ByVal objptr As Integer, _
                                                                   <MarshalAs(UnmanagedType.BStr)> ByVal entity_id As String, _
                                                                   <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef children_array() As String, _
                                                                   ByVal nb_children As Integer)
    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub ComputeInputDll3(ByVal objptr As Integer, _
                                      <MarshalAs(UnmanagedType.BStr)> ByVal entity_id As String, _
                                      <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_BSTR)> ByRef accKeys() As String, _
                                      <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_INT)> ByRef periods() As Integer, _
                                      <MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> ByRef values() As Double, _
                                      nbRecords As Integer)

    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Sub ComputeHierarchyInAggregationDll3(ByVal objptr As Integer)
    End Sub

    <DllImport("DLL3.dll")>
    Private Shared Function ReturnAggregationMatrixDll3(<MarshalAs(UnmanagedType.SafeArray, SafeArraySubType:=VarEnum.VT_R8)> _
                                                        <System.Runtime.InteropServices.Out()> ByRef Output() As Double, _
                                                        ByVal objptr As Integer, _
                                                        <MarshalAs(UnmanagedType.BStr)> ByVal entity_id As String) As Integer

    End Function


#End Region

#End Region


#Region "Initialize"

    ' DataSourceBuilder initialization -> Model init
    Protected Friend Sub New()

        objptr = CreateDll3()
        isModelAlive = True
        Dim formulaTypes() As String = FormulaTypesMapping.GetModelFormulaTypesKeys().ToArray        ' {"HV", "F", "SOAC", "BS", "WC"}
        Dim formulaCodes() As Integer = FormulaTypesMapping.GetModelFormulaTypesIntCodes.ToArray     ' { 0  ,  1 ,    2  ,  3  ,  4}

        Dim accountsTableDictionary As Dictionary(Of String, String()) = AccountsMapping.GetAccountsTableArrays()
        FormulaTypesCheckAndRepair(accountsTableDictionary)
        Dim recomputed_accounts As List(Of String) = AccountsMapping.GetRecomputedAccountsIDsList()
        Dim dumbInt As Integer = initModelDll3(objptr, formulaTypes, formulaCodes, formulaCodes.GetLength(0), _
                                               accountsTableDictionary.Item(ACCOUNT_ID_VARIABLE), _
                                               accountsTableDictionary.Item(ACCOUNT_ID_VARIABLE).GetLength(0), _
                                               accountsTableDictionary.Item(ACCOUNT_PARENT_ID_VARIABLE),
                                               accountsTableDictionary.Item(ACCOUNT_FORMULA_TYPE_VARIABLE),
                                               accountsTableDictionary.Item(ACCOUNT_FORMULA_VARIABLE), _
                                               accountsTableDictionary.Item(ACCOUNT_CONVERSION_FLAG_VARIABLE), _
                                               recomputed_accounts.ToArray, recomputed_accounts.Count)

        accounts_array = accountsTableDictionary.Item(ACCOUNT_ID_VARIABLE)
        accountsTableDictionary.Clear()
        dll3TimeSetup = ""

    End Sub

    Friend Sub InitDllPeriods(ByRef periodsList As List(Of Integer), _
                              ByRef timePeriodsSetUp As String)

        Dim periodsSetupArray As Integer() = periodsList.ToArray
        Dim nbPeriods As Int32 = periodsList.Count
        initPeriodsDll3(objptr, periodsSetupArray, nbPeriods)
        dll3TimeSetup = timePeriodsSetUp

    End Sub

    Friend Sub InitDllCurrencyConvertorPeriods(ByVal periods_list As List(Of Int32), _
                                               ByRef time_config As String, _
                                               ByRef start_period As Int32, _
                                               ByRef nb_periods As Int32)

        Dim years_periods_list As List(Of Int32)
        Select Case time_config
            Case MONTHLY_TIME_CONFIGURATION
                years_periods_list = New List(Of Int32)
                For i = 0 To nb_periods - 1
                    years_periods_list.Add(start_period + i)
                Next
            Case YEARLY_TIME_CONFIGURATION
                years_periods_list = periods_list
        End Select

        initializeCurrencyConvertorDll3(objptr, years_periods_list.ToArray, years_periods_list.Count)
        Dim years_months_dict As Dictionary(Of Int32, Int32()) = Period.GetGlobalPeriodsDictionary(years_periods_list)

        For Each period In years_periods_list
            AddPeriodYearCurrencyConvertorDll3(objptr, years_months_dict(period))
        Next
        convertor_currencies_token_list.Clear()
        current_start_period = years_periods_list(0)
        current_nb_periods = years_periods_list.Count

    End Sub

    ' Currency_token: rates_version + curr1 + "/" + dest_curr + ref_period
    Protected Friend Sub AddYearlyCurrenciesRatesToConvertor(ByVal currency_token As String, ByRef periods_array() As Int32, _
                                                   ByRef rates_array() As Double, ByRef nb_records As Int32)

        AddYearlyMatrixCurrencyDll3(objptr, currency_token, periods_array, rates_array, nb_records)
        convertor_currencies_token_list.Add(currency_token)

    End Sub

    ' Rates_array must be ordered by month and includes all months
    ' Currency_token: rates_version + curr1 + "/" + dest_curr + time_config + ref_period
    Friend Sub AddMonthlyCurrenciesRatesToConvertor(ByVal currency_token As String, ByRef rates_array() As Double)

        AddMonthlyMatrixCurrencyDll3(objptr, currency_token, rates_array)
        convertor_currencies_token_list.Add(currency_token)

    End Sub

    Friend Sub destroy_dll()

        If isModelAlive = True Then
            DestroyDll3(objptr)
            isModelAlive = False
        End If

    End Sub

#End Region


#Region "CC Interface"


    ' Param1: NbEntities -> size the DLL output Array
    ' Param2: OutputMode -> 0 = display | 1 = DataBase (cube)
    Friend Sub InitializeDLLOutput(ByRef nbEntities As Integer, ByRef OutputMode As Int32)

        initOutputDll3(objptr, nbEntities, OutputMode)

    End Sub

    ' Init and compute a single entity
    Friend Sub ComputeSingleEntity(ByRef entityKey As String, _
                                   ByRef accKeysArray() As String, _
                                   ByRef periodsArray() As Integer, _
                                   ByRef valuesArray() As Double)

        initOutputDll3(objptr, 1, 0)    ' Init Ouptut with 1 entity, output option 0 (display)
        ComputeEntity(accKeysArray, _
                      periodsArray, _
                      valuesArray, _
                      0)
        currentSingleEntityKey = entityKey

    End Sub

    Friend Sub ComputeEntity(ByRef accKeysArray() As String, _
                             ByRef periodsArray() As Integer, _
                             ByRef valuesArray() As Double, _
                             ByVal storage_option As Int32)

        '!! take off currency in this function in dll3
        computeEntityDll3(objptr, _
                          accKeysArray, _
                          periodsArray, _
                          valuesArray, _
                          valuesArray.GetLength(0), _
                          "", _
                          storage_option)

    End Sub

    Friend Sub RegisterEmptyEntity()

        RegisterEmptyEntityDLL3(objptr, "")

    End Sub

    ' Fills the param array with DLL current computed data
    Friend Sub GetDataArray(ByRef inputDataArray(,,) As Object)

        Dim Result As Integer
        Result = ReturnDataArrayDLL3(inputDataArray, objptr)

    End Sub

    ' Retrieve specific Account|Period|Currency from current DLL Model instance
    Public Function GetDataFromComputer(ByRef accountKey As String, _
                                        ByRef periodIndex As Integer) As Object

        Try
            Return ReturnValueFromModelDLL3(objptr, _
                                            accountKey, _
                                            periodIndex, _
                                            "")

        Catch ex As Exception
        End Try

    End Function


#End Region


#Region "Entities Aggregation Interface"

    Friend Function InitializeEntitiesAggregation(ByRef input_entity_node As TreeNode) As List(Of String)

        all_entities_ids = TreeViewsUtilities.GetNodesKeysList(input_entity_node)
        TreeViewsUtilities.FilterSelectedNodes(input_entity_node, all_entities_ids)
        FillInEntitiesCurrencies(all_entities_ids, entities_currencies)

        InitializeEntitiesAggregationDll3(objptr, _
                                          all_entities_ids.ToArray,
                                          entities_currencies.ToArray, _
                                          all_entities_ids.Count)

        For Each entity_id In all_entities_ids
            Dim node As TreeNode
            Dim children_array() As String
            If entity_id = input_entity_node.Name Then
                node = input_entity_node
            Else
                node = input_entity_node.Nodes.Find(entity_id, True)(0)
            End If
            children_array = TreeViewsUtilities.GetNodeChildrenIDsStringArray(node, True)
            AddEntityToEntitiesAggregationHierarchyDll3(objptr, entity_id, children_array, children_array.Count)
        Next

        Return all_entities_ids

    End Function

    Friend Sub SetUpEABeforeCompute(ByRef periodsList As List(Of Integer), _
                                    ByRef destination_currency_arg As String, _
                                    ByRef time_config As String,
                                    ByRef rates_version As String, _
                                    Optional ByRef ref_period As Int32 = 0)

        SetUpBeforeComputeDll3(objptr, periodsList.ToArray, periodsList.Count, destination_currency_arg, _
                               time_config, rates_version, ref_period)

        dll3TimeSetup = time_config

    End Sub

    Friend Sub ComputeInputEntity(ByVal entity_id As String, _
                                  ByRef accKeysArray() As String, _
                                  ByRef periodsArray() As Integer, _
                                  ByRef valuesArray() As Double)

        ComputeInputDll3(objptr, _
                       entity_id, _
                       accKeysArray, _
                       periodsArray, _
                       valuesArray, _
                       valuesArray.GetLength(0))


    End Sub

    Protected Friend Sub ComputeAggregation()

        ComputeHierarchyInAggregationDll3(objptr)

    End Sub

    Protected Friend Function GetEntityDataArray(ByRef entity_id As String) As Double()

        Dim tmpDataArray() As Double
        ReturnAggregationMatrixDll3(tmpDataArray, objptr, entity_id)
        Return tmpDataArray

    End Function

    Friend Function GetOutputMatrix() As Dictionary(Of String, Double())

        Dim tmpDict As New Dictionary(Of String, Double())
        For Each entity_id In all_entities_ids
            Dim tmpDataArray() As Double
            ReturnAggregationMatrixDll3(tmpDataArray, objptr, entity_id)
            tmpDict.Add(entity_id, tmpDataArray)
        Next
        Return tmpDict

    End Function


#End Region


#Region "Checks"

    ' FormulaTypesCheckAndRepair checks for accounts which formula type needs a formula and don't have a formula
    ' If such account if found then the FType is set to hard value input
    Private Sub FormulaTypesCheckAndRepair(ByRef accountsTableDictionary As Dictionary(Of String, String()))

        Dim formulasFTypesList As List(Of String) = FormulaTypesMapping.GetFTypesKeysNeedingFormula

        For i = 0 To accountsTableDictionary(ACCOUNT_FORMULA_TYPE_VARIABLE).GetLength(0) - 1
            If formulasFTypesList.Contains(accountsTableDictionary(ACCOUNT_FORMULA_TYPE_VARIABLE)(i)) Then
                If accountsTableDictionary(ACCOUNT_FORMULA_VARIABLE)(i) = "" Then
                    accountsTableDictionary(ACCOUNT_FORMULA_TYPE_VARIABLE)(i) = HARD_VALUE_F_TYPE_CODE
                End If
            End If
        Next


    End Sub


#End Region


#Region "Utilities"

    Private Sub FillInEntitiesCurrencies(ByRef entities_list As List(Of String), _
                                         ByRef entities_currencies As List(Of String))

        entities_currencies.Clear()
        Dim entitites_currencies_map = EntitiesMapping.GetEntitiesDictionary(ASSETS_TREE_ID_VARIABLE, ASSETS_CURRENCY_VARIABLE)
        For Each entity In entities_list
            entities_currencies.Add(entitites_currencies_map(entity))
        Next

    End Sub

    Friend Function CheckParserFormula(ByRef formula_str As String) As Boolean

        If CheckParserFormulaDll3(objptr, formula_str) = 1 Then Return True Else Return False

    End Function

#End Region


    Protected Overrides Sub finalize()

        If isModelAlive = True Then
            DestroyDll3(objptr)
            isModelAlive = False
        End If

    End Sub

End Class
