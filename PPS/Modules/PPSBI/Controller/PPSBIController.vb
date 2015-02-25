' PPSBIController.vb
' 
' - Currently PPSBI user defined function call back
' - Manage the checks, launch computing and return formula result
'
' To do:
'      - Simplification : loop through optional parameters
'      - Version and currency dllcomputerinstance current checks to add when those params are operationals
'      - Must be able to identify dates more globally -> regex for example     

'  Known bugs:
'      
'
' Author: Julien Monnereau
' Last modified: 25/02/2015
'


Imports Microsoft.Office.Interop
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms


Friend Class PPSBIController


#Region "Instance Variables"

    ' Objects
    Private ESB As EntitiesSelectionBuilderClass

    ' Variables
    Private AccountsNameKeyDictionary As Hashtable
    Private EntitiesNameKeyDictionary As Hashtable
    Private CategoriesNameKeyDictionary As Hashtable
    Private AccountsFTypesDictionary As Hashtable
    Private Adjustments_name_id_dic As Dictionary(Of String, String)
    Private emptyCellFlag As Boolean
    Friend filterList As New List(Of String)
    Private aggregation_computed_accounts_types As New List(Of String)

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        ESB = New EntitiesSelectionBuilderClass
        AccountsFTypesDictionary = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_FORMULA_TYPE_VARIABLE)
        AccountsNameKeyDictionary = AccountsMapping.GetAccountsDictionary(ACCOUNT_NAME_VARIABLE, ACCOUNT_ID_VARIABLE)
        EntitiesNameKeyDictionary = EntitiesMapping.GetEntitiesDictionary(ASSETS_NAME_VARIABLE, ASSETS_TREE_ID_VARIABLE)
        CategoriesNameKeyDictionary = CategoriesMapping.GetCategoriesDictionary(CATEGORY_NAME_VARIABLE, CATEGORY_ID_VARIABLE)
        Adjustments_name_id_dic = AdjustmentsMapping.GetAdjustmentsDictionary(ADJUSTMENTS_NAME_VAR, ADJUSTMENTS_ID_VAR)
        emptyCellFlag = False
        aggregation_computed_accounts_types.Add(FORMULA_ACCOUNT_FORMULA_TYPE)
        aggregation_computed_accounts_types.Add(FORMULA_TYPE_BALANCE_SHEET)
        aggregation_computed_accounts_types.Add(FORMULA_TYPE_SUM_OF_CHILDREN)

    End Sub

#End Region


#Region "Interface"

    ' Period input: date as integer 
    Protected Friend Function getDataCallBack(ByRef entity As String, _
                                            ByRef account As Object, _
                                            ByRef period As Object, _
                                            ByRef currency As Object, _
                                            ByRef version As Object, _
                                            Optional ByRef adjustment_filter As Object = Nothing, _
                                            Optional ByRef filtersArray As Object() = Nothing) As Object

        Dim entityString, entity_id, account_id, accountString, periodString, currencyString, version_id, adjustment_string, adjustment_id, error_message As String
        emptyCellFlag = False
        entityString = ReturnValueFromRange(entity)
        accountString = ReturnValueFromRange(account)
        periodString = ReturnValueFromRange(period)
        currencyString = ReturnValueFromRange(currency)
        adjustment_string = ReturnValueFromRange(adjustment_filter)

        filterList.Clear()
        For Each filter_ In filtersArray
            If Not filter_ Is Nothing Then AddFilterValueToFiltersList(filter_)
        Next

        If CheckParameters(accountString, account_id, _
                           entityString, entity_id, _
                           adjustment_string, adjustment_id, _
                           version, version_id, _
                           error_message) _
                           = False Then Return error_message

        ESB.BuildCategoriesFilterFromFilterList(filterList)
        Dim entity_node As TreeNode = ESB.EntitiesTV.Nodes.Find(entity_id, True)(0)

        If aggregation_computed_accounts_types.Contains(AccountsFTypesDictionary(account_id)) _
        AndAlso entity_node.Nodes.Count > 0 Then
            Return ComputeViaAggregationComputer(entity_id, _
                                                 entity_node, _
                                                 account_id, _
                                                 version_id, _
                                                 currencyString, _
                                                 period, _
                                                 adjustment_id)
        Else
            Return ComputeViaSingleEntityComputer(entity_id, _
                                                  account_id, _
                                                  version_id, _
                                                  currencyString, _
                                                  period, _
                                                  adjustment_id)
        End If

    End Function

    Private Function ComputeViaSingleEntityComputer(ByRef entity_id As String, _
                                                    ByRef account_id As String, _
                                                    ByRef version_id As String, _
                                                    ByRef currency As String, _
                                                    ByRef period As Object, _
                                                    ByRef adjustment_id As String) As Object

        Dim period_int As Int32
        If GlobalVariables.GenericGlobalSingleEntityComputer.current_entity_id <> entity_id _
        Or GlobalVariables.GenericGlobalSingleEntityComputer.currentStrSqlQuery <> ESB.StrSqlQuery _
        Or GlobalVariables.GenericGlobalSingleEntityComputer.current_version_id <> version_id _
        Or GlobalVariables.GenericGlobalSingleEntityComputer.currentCurrency <> currency _
        Or GlobalVariables.GenericGlobalSingleEntityComputer.current_adjusmtent_id <> adjustment_id Then

            GlobalVariables.GenericGlobalSingleEntityComputer.ComputeAggregatedEntity(entity_id, _
                                                                                      version_id, _
                                                                                      currency, _
                                                                                      adjustment_id, _
                                                                                      ESB.StrSqlQuery)
        End If

        If CheckDate(period, period_int, GlobalVariables.GenericGlobalSingleEntityComputer.period_list) = False Then
            Return "Invalid Period or Period format"
        End If

        Try
            Return GlobalVariables.GenericGlobalSingleEntityComputer.GetDataFromDLL3Computer(account_id, period_int)
        Catch ex As Exception
            Return "Invalid parameters"
        End Try

    End Function

    Private Function ComputeViaAggregationComputer(ByRef entity_id As String, _
                                                   ByRef entity_node As TreeNode, _
                                                   ByRef account_id As String, _
                                                   ByRef version_id As String, _
                                                   ByRef currency As String, _
                                                   ByRef period As Object, _
                                                   ByRef adjustment_id As String) As Object

        Dim period_int As Int32
        If GlobalVariables.GenericGlobalAggregationComputer.IsEntityAlreadyComputed(entity_id) = False _
        Or GlobalVariables.GenericGlobalAggregationComputer.current_version_id <> version_id _
        Or GlobalVariables.GenericGlobalAggregationComputer.current_currency <> currency _
        Or GlobalVariables.GenericGlobalAggregationComputer.current_sql_filter_query <> ESB.StrSqlQuery _
        Or GlobalVariables.GenericGlobalAggregationComputer.current_adjustment_id <> adjustment_id Then

            Dim Versions As New Version
            Dim nb_periods, start_period As Int32
            Dim periods_list As List(Of Int32)
            Dim time_configuration As String
            Dim rates_version_id As String = Versions.ReadVersion(version_id, VERSIONS_RATES_VERSION_ID_VAR)
            Dim adjustments_id_list As List(Of String)
            If adjustment_id <> "" Then
                adjustments_id_list = New List(Of String)
                adjustments_id_list.Add(adjustment_id)
            Else
                adjustments_id_list = Nothing
            End If
            GlobalVariables.GenericGlobalAggregationComputer.init_computer_complete_mode(entity_node)
            periods_list = Versions.GetPeriodList(version_id)
            time_configuration = Versions.ReadVersion(version_id, VERSIONS_TIME_CONFIG_VARIABLE)
            nb_periods = Versions.ReadVersion(version_id, VERSIONS_NB_PERIODS_VAR)
            start_period = Versions.ReadVersion(version_id, VERSIONS_START_PERIOD_VAR)
            Versions.Close()

            GlobalVariables.GenericGlobalAggregationComputer.compute_selection_complete(version_id, _
                                                                                        time_configuration, _
                                                                                        rates_version_id, _
                                                                                        periods_list, _
                                                                                        currency, _
                                                                                        start_period, _
                                                                                        nb_periods, , _
                                                                                        ESB.StrSqlQuery, _
                                                                                        adjustments_id_list)
            GlobalVariables.GenericGlobalAggregationComputer.LoadOutputMatrix()
        End If

        If CheckDate(period, period_int, GlobalVariables.GenericGlobalAggregationComputer.periods_list) = False Then
            Return "Invalid Period or Period format"
        End If
        Return GlobalVariables.GenericGlobalAggregationComputer.GetValueFromComputer(entity_id, _
                                                                                     account_id, _
                                                                                     period_int)
    End Function

#End Region


#Region "Checks"

    Private Function CheckParameters(ByRef accountstring As String, _
                                     ByRef account_id As String, _
                                     ByRef entityString As String, _
                                     ByRef entity_id As String, _
                                     ByRef adjustmentstring As String, _
                                     ByRef adjustment_id As String, _
                                     ByRef version As Object, _
                                     ByRef version_id As String, _
                                     ByRef error_message As String) As Boolean

        If AccountsNameKeyDictionary.ContainsKey(accountstring) Then
            account_id = AccountsNameKeyDictionary.Item(accountstring)
        Else
            error_message = "Invalid Account"
            Return False
        End If

        If EntitiesNameKeyDictionary.ContainsKey(entityString) Then
            entity_id = EntitiesNameKeyDictionary.Item(entityString)
        Else
            error_message = "Invalid Entity"
            Return False
        End If

        If adjustmentstring <> "" Then
            If Adjustments_name_id_dic.ContainsKey(adjustmentstring) Then
                adjustment_id = Adjustments_name_id_dic(adjustmentstring)
            Else
                error_message = "Invalid Adjustment"
                Return False
            End If
        Else
            adjustment_id = ""
        End If

        If emptyCellFlag = True Then
            error_message = "Missing Parameter"
            Return False
        End If

        If CheckVersion(version, version_id) = False Then
            error_message = "Invalid version"
            Return False
        End If
        Return True

    End Function

    Private Function CheckVersion(ByRef version_name As Object, _
                               ByRef version_id As String) As String

        If Not version_name Is Nothing Then
            Dim versionString As String = ReturnValueFromRange(version_name)
            version_id = VersionsMapping.GetVersionsIDFromName(versionString)
            If version_id <> "" Then Return True Else Return False
        Else
            version_id = GlobalVariables.GLOBALCurrentVersionCode
            Return True
        End If

    End Function

    Private Function CheckDate(ByRef input_period_object As Object, _
                               ByRef periodInteger As Integer, _
                               ByRef periodslist As List(Of Int32)) As Boolean

        Dim periodstr As String = ReturnValueFromRange(input_period_object)
        If IsDate(periodstr) Then
            Dim periodAsDate As Date = CDate(periodstr)
            If periodslist.Contains(periodAsDate.ToOADate) Then
                periodInteger = periodAsDate.ToOADate
                Return True
            Else
                Return False
            End If
        Else
            If periodslist.Contains(periodstr) Then
                periodInteger = periodstr
                Return True
            Else
                Select Case GlobalVariables.GenericGlobalSingleEntityComputer.time_config
                    Case MONTHLY_TIME_CONFIGURATION
                        For Each period As Integer In periodslist
                            If Month(DateTime.FromOADate(period)) = periodstr Then
                                periodInteger = period
                                periodstr = DateTime.FromOADate(period)
                                Return True
                            End If
                        Next
                    Case YEARLY_TIME_CONFIGURATION
                        If IsNumeric(periodstr) Then
                            For Each period As Integer In periodslist
                                If Year(DateTime.FromOADate(period)) = periodstr Then
                                    periodInteger = period
                                    Return True
                                End If
                            Next
                        End If
                End Select
            End If

        End If
        Return False

    End Function

#End Region


#Region "Utilities"

    Private Function ReturnValueFromRange(ByRef input As Object) As Object

        If input Is Nothing Then Return ""
        If TypeOf (input) Is Excel.Range Then
            Dim rng As Excel.Range = CType(input, Excel.Range)
            If rng.Value2 Is Nothing Then emptyCellFlag = True
            Return rng.Value2
        Else
            Return input
        End If

    End Function

    Private Sub AddFilterValueToFiltersList(ByRef filter As Object)

        Dim filterValue As String = ReturnValueFromRange(filter)
        If filterValue.Contains(",") = True Then
            For Each value As String In filterValue.Split(PPSBI_FORMULA_CATEGORIES_SEPARATOR)
                If CategoriesNameKeyDictionary.ContainsKey(value) Then filterList.Add(CategoriesNameKeyDictionary.Item(value))
            Next
        Else
            If CategoriesNameKeyDictionary.ContainsKey(filterValue) Then filterList.Add(CategoriesNameKeyDictionary.Item(filterValue))
        End If

    End Sub

#End Region


End Class
