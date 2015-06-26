' DatabaseDataDownloader.vb
' 
'  Manage data tables downloads
'
'
' To do:
'       - Adapt Aggregated queries and single aggregation to filter on clients, products and adjustments
'           -> process to be decided
'
'
' Known bugs:
'       - 
'
' Author: Julien Monnereau
' Last modified: 25/06/2015
'


Imports System.Linq
Imports System.Data
Imports System.Collections.Generic
Imports System.Collections


Friend Class DataBaseDataDownloader


#Region "Instance Variables"

    ' Objects
    Private srv As New ModelServer

    ' Variables
    Friend stored_inputs_list As New List(Of String)
    Friend currentRatesVersionCode As String = ""

    ' Filter Process

    '  Initial Lists
    Private initial_clients_id_list As List(Of String)
    Private initial_products_id_list As List(Of String)
    Private initial_adjustments_id_list As List(Of String)

    '  Filters Lists
    Private entities_id_filter_list As List(Of String)
    Private clients_id_filter_list As List(Of String)
    Private products_id_filter_list As List(Of String)
    Private adjustments_id_filter_list As List(Of String)

    '  Flags
    Private entities_filter_flag As Boolean
    Private clients_filter_flag As Boolean
    Private products_filter_flag As Boolean
    Private adjustments_filter_flag As Boolean

    ' Inputs Arrays and Dictionaries
    '   Dll Entities Aggregation Process
    Friend accounts_ID_hash As New Dictionary(Of String, String())
    Friend periods_ID_hash As New Dictionary(Of String, Integer())
    Friend values_ID_hash As New Dictionary(Of String, Double())

    '   Aggregation Queries Process
    Friend AccKeysArray() As String
    Friend PeriodArray() As Integer
    Friend ValuesArray() As Double

    ' Constants
    Private Const MONETARY_TYPE_CODE As String = "MO"


#End Region


#Region "Initialize"

    Protected Friend Sub New()

        initial_clients_id_list = ClientsMapping.GetclientsIDList()
        initial_products_id_list = ProductsMapping.GetproductsIDList()
        initial_adjustments_id_list = AdjustmentsMapping.GetAdjustmentsIDsList(ANALYSIS_AXIS_ID_VAR)

        ' below ?!!
        clients_id_filter_list = initial_clients_id_list
        products_id_filter_list = initial_products_id_list
        adjustments_id_filter_list = initial_adjustments_id_list


    End Sub

#End Region


#Region "Aggregated Queries"

    ' Purpose of the process: PPSBI Formula ?

    ' the inputs arrays should be passed as params ! 
    Friend Function GetAggregatedConvertedInputs(ByRef entities_id_list As List(Of String), _
                                                 ByRef version_id As String, _
                                                 ByRef destination_currency As String, _
                                                 Optional ByRef clients_id_list As List(Of String) = Nothing, _
                                                 Optional ByRef products_id_list As List(Of String) = Nothing, _
                                                 Optional ByRef adjustments_id_list As List(Of String) = Nothing) As Boolean

        Dim sql_where_clause As String = GetWhereClause(entities_id_list, _
                                                        clients_id_list, _
                                                        products_id_list, _
                                                        adjustments_id_list)

        If DataAggregationQuery(version_id, sql_where_clause) = True _
          AndAlso entities_id_list.Count > 0 Then
            ConvertInputsArrays(version_id, entities_id_list.ToArray(), destination_currency)
            srv.rst.Close()
            Return True
        Else
            srv.rst.Close()
            Return False
        End If

    End Function

    Private Function DataAggregationQuery(ByRef version_id As String, _
                                          ByRef sql_where_clause As String) As Boolean

        Dim strSQL As String = "SELECT " & "D." & DATA_PERIOD_VARIABLE & "," _
                             & "D." & DATA_ACCOUNT_ID_VARIABLE & "," _
                             & "SUM(" & DATA_VALUE_VARIABLE & ") AS " & DATA_VALUE_VARIABLE & "," _
                             & "A." & ENTITIES_CURRENCY_VARIABLE _
                             & " FROM " & GlobalVariables.database & "." & version_id & " D" & ", " & GlobalVariables.database & "." & ENTITIES_TABLE & " A" _
                             & " WHERE " & "D." & DATA_ENTITY_ID_VARIABLE & "=" & "A." & ENTITIES_ID_VARIABLE
                            
        If sql_where_clause <> "" Then strSQL = strSQL & " AND " & sql_where_clause

        strSQL = strSQL & " GROUP BY " & "D." & DATA_PERIOD_VARIABLE & "," & _
                                         "D." & DATA_ACCOUNT_ID_VARIABLE & "," & _
                                         "A." & ENTITIES_CURRENCY_VARIABLE

        srv.openRstSQL(strSQL, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = True Then Return False Else Return True

    End Function

    Private Sub ConvertInputsArrays(ByRef version_id As String, _
                                    ByRef entities_id_List() As String, _
                                    ByRef destination_currency As String)

        Dim typesDict As Hashtable = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_TYPE_VARIABLE)
        Dim conversion_flagDict As Hashtable = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_CONVERSION_FLAG_VARIABLE)
        Dim account_id As String
        Dim value As Double
        Dim rate_type, currency_token, currency_ As String
        Dim values_dict As New Dictionary(Of Int32, Dictionary(Of String, List(Of Double)))
        Dim accounts_list As New List(Of String)
        Dim periods_list As New List(Of Int32)

        Dim exchange_rates_dictionary As Dictionary(Of String, Dictionary(Of Int32, Dictionary(Of String, Double))) _
        = GetExchangeRatesDictionary(version_id, destination_currency, entities_id_List)

        Do While srv.rst.EOF = False AndAlso srv.rst.BOF = False
            account_id = srv.rst.Fields(DATA_ACCOUNT_ID_VARIABLE).Value
            value = srv.rst.Fields(DATA_VALUE_VARIABLE).Value
            currency_ = srv.rst.Fields(ENTITIES_CURRENCY_VARIABLE).Value

            If currency_ <> destination_currency AndAlso typesDict(account_id) = MONETARY_ACCOUNT_TYPE Then
                If conversion_flagDict(account_id) = FLUX_CONVERSION Then rate_type = ExchangeRate.AVERAGE_RATE Else rate_type = ExchangeRate.CLOSING_RATE
                currency_token = currency_ & CURRENCIES_SEPARATOR & destination_currency
                value = value * exchange_rates_dictionary(currency_token)(srv.rst.Fields(DATA_PERIOD_VARIABLE).Value)(rate_type)
            End If
            AddValueToAggregatedConvertedValuesHash(values_dict, _
                                                    accounts_list, _
                                                    periods_list, _
                                                    account_id, _
                                                    srv.rst.Fields(DATA_PERIOD_VARIABLE).Value, _
                                                    value)
            srv.rst.MoveNext()
        Loop
        BuildAggregatedInputsArrays(values_dict, _
                                    accounts_list, _
                                    periods_list)

    End Sub

    Private Sub AddValueToAggregatedConvertedValuesHash(ByRef values_dict As Dictionary(Of Int32, Dictionary(Of String, List(Of Double))), _
                                                        ByRef accounts_list As List(Of String), _
                                                        ByRef periods_list As List(Of Int32), _
                                                        ByRef account_id As String, _
                                                        ByVal period_ As Int32, _
                                                        ByRef value As Double)

        If accounts_list.Contains(account_id) = False Then accounts_list.Add(account_id)
        If periods_list.Contains(period_) = False Then periods_list.Add(period_)
        If values_dict.ContainsKey(period_) Then
            If values_dict(period_).ContainsKey(account_id) = False Then
                Dim values_list As New List(Of Double)
                values_dict(period_).Add(account_id, values_list)
            End If
        Else
            Dim values_list As New List(Of Double)
            Dim accounts_dic As New Dictionary(Of String, List(Of Double))
            accounts_dic.Add(account_id, values_list)
            values_dict.Add(period_, accounts_dic)
        End If
        values_dict(period_)(account_id).Add(value)

    End Sub

    Private Sub BuildAggregatedInputsArrays(ByRef values_dict As Dictionary(Of Int32, Dictionary(Of String, List(Of Double))), _
                                            ByRef accounts_list As List(Of String), _
                                            ByRef periods_list As List(Of Int32))

        Dim nb_records As Int32 = (accounts_list.Count) * (periods_list.Count)
        Dim i As Int32 = 0
        ReDim PeriodArray(nb_records)
        ReDim AccKeysArray(nb_records)
        ReDim ValuesArray(nb_records)

        For Each period_ As Int32 In periods_list
            For Each account_id As String In accounts_list
                Try
                    ValuesArray(i) = values_dict(period_)(account_id).Sum
                    PeriodArray(i) = period_
                    AccKeysArray(i) = account_id
                    i = i + 1
                Catch ex As Exception
                End Try
            Next
        Next
        ReDim Preserve PeriodArray(i - 1)
        ReDim Preserve AccKeysArray(i - 1)
        ReDim Preserve ValuesArray(i - 1)

    End Sub

#End Region


#Region "Dll Entities Aggregation data hash build"

    Friend Function BuildDataRSTForEntityLoop(ByRef data_view_name As String) As Boolean

        Dim strSQL As String = "SELECT " + "D." + DATA_PERIOD_VARIABLE + "," _
                             + "D." + DATA_ACCOUNT_ID_VARIABLE + "," _
                             + "SUM(" + DATA_VALUE_VARIABLE + ") AS value," _
                             + DATA_ENTITY_ID_VARIABLE _
                             + " FROM " + GlobalVariables.database + "." + data_view_name + " D" + ", " + GlobalVariables.database + "." + ENTITIES_TABLE + " A" _
                             + " WHERE " + DATA_ENTITY_ID_VARIABLE + "=" + ENTITIES_ID_VARIABLE

        Dim additional_where_clause As String = GetAdditionnalWhereClauseFromFilters()
        If additional_where_clause <> "" Then strSQL = strSQL & " AND " & additional_where_clause

        Dim str_sql_group As String = " GROUP BY " + DATA_PERIOD_VARIABLE + "," + _
                                                     DATA_ACCOUNT_ID_VARIABLE + "," + _
                                                     DATA_ENTITY_ID_VARIABLE
        strSQL = strSQL + str_sql_group

        srv.openRstSQL(strSQL, ModelServer.FWD_CURSOR)
        If srv.rst.BOF = True Or srv.rst.EOF = True Then
            srv.rst.Close()
            Return False
        Else
            Return True
        End If

    End Function

    Private Function StoreEntityData(ByRef entity_id As String)

        srv.rst.Filter = DATA_ENTITY_ID_VARIABLE & "='" & entity_id & "'"
        If srv.rst.BOF = True Or srv.rst.EOF = True Then
            srv.rst.Filter = ""
            Return False
        Else
            Dim data_array(,) As Object
            data_array = srv.rst.GetRows
            Dim tmp_periods(UBound(data_array, 2)) As Integer
            Dim tmp_acc(UBound(data_array, 2)) As String
            Dim tmp_values(UBound(data_array, 2)) As Double

            For i As Integer = 0 To UBound(data_array, 2)
                tmp_periods(i) = data_array(0, i)
                tmp_acc(i) = data_array(1, i)
                tmp_values(i) = data_array(2, i)
            Next

            periods_ID_hash.Add(entity_id, tmp_periods)
            accounts_ID_hash.Add(entity_id, tmp_acc)
            values_ID_hash.Add(entity_id, tmp_values)
            stored_inputs_list.Add(entity_id)

            srv.rst.Filter = ""
            Return True
        End If

    End Function

    Friend Function FilterOnEntityID(ByRef entityID As String) As Boolean

        srv.rst.Filter = DATA_ENTITY_ID_VARIABLE & "='" & entityID & "'"
        If srv.rst.BOF = True Or srv.rst.EOF = True Then
            srv.rst.Filter = ""
            Return False
        Else
            Dim data_array(,) As Object
            data_array = srv.rst.GetRows
            BuildOutputsArrays(data_array)
            srv.rst.Filter = ""
            Return True
        End If

    End Function

#End Region


#Region "Single Entity Queries"

    Protected Friend Function GetEntityInputsNonConverted(ByRef entityKey As String, _
                                                          ByRef ViewName As String, _
                                                          Optional ByRef clients_id_list As List(Of String) = Nothing, _
                                                          Optional ByRef products_id_list As List(Of String) = Nothing, _
                                                          Optional ByRef adjustments_id_list As List(Of String) = Nothing) As Boolean

        Dim strSql As String = "SELECT " & DATA_PERIOD_VARIABLE & ", " _
                             & DATA_ACCOUNT_ID_VARIABLE & ", " _
                             & DATA_VALUE_VARIABLE _
                             & " FROM " & GlobalVariables.database & "." & ViewName _
                             & " WHERE " & DATA_ENTITY_ID_VARIABLE & "='" & entityKey & "'"

        Dim additional_where_clause As String = GetWhereClause(, clients_id_list, _
                                                               products_id_list, _
                                                               adjustments_id_list)
        If additional_where_clause <> "" Then strSql = strSql & " AND " & additional_where_clause

        If srv.openRstSQL(strSql, ModelServer.FWD_CURSOR) = False Then Return False
        Dim tmpArray(,) As Object = Nothing
        If srv.rst.EOF = False Then tmpArray = srv.rst.GetRows()
        Try
            srv.rst.Close()
        Catch ex As Exception
        End Try
        If Not tmpArray Is Nothing Then
            BuildOutputsArrays(tmpArray)
            Return True
        Else
            Erase PeriodArray
            Erase AccKeysArray
            Erase ValuesArray
            Return False
        End If
        
    End Function

    Private Sub BuildOutputsArrays(ByRef data_array(,) As Object)

        RedimOutputsArrays(UBound(data_array, 2))
        For i As Integer = 0 To UBound(data_array, 2)
            PeriodArray(i) = data_array(0, i)
            AccKeysArray(i) = data_array(1, i)
            ValuesArray(i) = data_array(2, i)
        Next

    End Sub

    Private Sub RedimOutputsArrays(ByVal upper_bound As Int32)

        ReDim AccKeysArray(upper_bound)
        ReDim PeriodArray(upper_bound)
        ReDim ValuesArray(upper_bound)

    End Sub

#End Region


#Region "Adjustments Queries"

    ' Return AdjustmentsDict (account_id)(entity_id)(adjustment_id)(period) -> value
    ' Add optional filter param on adjustments_id
    Protected Friend Function GetAdjustments(ByRef version_id As String, _
                                             ByRef entitiesIDList As String(), _
                                             ByRef destination_currency As String, _
                                             Optional ByRef adjustments_id_list As List(Of String) = Nothing)

        Dim adjustments_dic As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Int32, Double))))
        If AdjustmentsQuery(version_id, entitiesIDList, adjustments_id_list) = True Then

            ConvertAdjustments(version_id, _
                               destination_currency, _
                               adjustments_dic, _
                               entitiesIDList)
        End If
        Try
            srv.rst.Close()
        Catch ex As Exception
        End Try
        Return adjustments_dic

    End Function

    Private Function AdjustmentsQuery(ByRef version_id As String, _
                                      ByRef entitiesIDList As String(), _
                                      ByRef adjustments_id_list As List(Of String)) As Boolean

        Dim entities_ids As String = "'" + Join(entitiesIDList, "','") + "'"

        Dim strSQL As String = "SELECT D." & DATA_PERIOD_VARIABLE & "," _
                             & " D." & DATA_ACCOUNT_ID_VARIABLE & "," _
                             & " D." & DATA_VALUE_VARIABLE & "," _
                             & " D." & DATA_ENTITY_ID_VARIABLE & "," _
                             & " D." & DATA_ADJUSTMENT_ID_VARIABLE & "," _
                             & " A." & ENTITIES_CURRENCY_VARIABLE _
                             & " FROM " & GlobalVariables.database & "." & version_id & " D" & ", " & GlobalVariables.database + "." & ENTITIES_TABLE + " A" _
                             & " WHERE " & "D." & DATA_ENTITY_ID_VARIABLE & "=" & "A." & ENTITIES_ID_VARIABLE _
                             & " AND " & DATA_ENTITY_ID_VARIABLE & " IN " & "(" & entities_ids & ")" _
                             & " AND " & DATA_CLIENT_ID_VARIABLE & " ='aaa'" _
                             & " AND " & DATA_PRODUCT_ID_VARIABLE & " ='aaa'"

        ' STUB  !!!!!!! attention à modifier
        If Not adjustments_id_list Is Nothing Then
            Dim adjustments_selection As String = "'" + Join(adjustments_id_list.ToArray(), "','") + "'"
            strSQL = strSQL + " AND " + DATA_ADJUSTMENT_ID_VARIABLE + " IN (" + adjustments_selection + ")"
        End If

        Return srv.openRstSQL(strSQL, ModelServer.FWD_CURSOR)

    End Function

    Private Sub ConvertAdjustments(ByRef version_id As String, _
                                   ByRef destination_currency As String, _
                                   ByRef adjustments_dic As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Int32, Double)))), _
                                   ByRef entities_id_List As String())

        ' AdjustmentsDict (account_id)(entity_id)(adjustment_id)(period) -> value
        Dim typesDict As Hashtable = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_TYPE_VARIABLE)
        Dim conversion_flagDict As Hashtable = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_CONVERSION_FLAG_VARIABLE)
        Dim account_id As String
        Dim value As Double
        Dim rate_type, currency_token, currency_ As String

        Dim exchange_rates_dictionary As Dictionary(Of String, Dictionary(Of Int32, Dictionary(Of String, Double))) _
        = GetExchangeRatesDictionary(version_id, destination_currency, entities_id_List)

        Do While srv.rst.EOF = False AndAlso srv.rst.BOF = False
            account_id = srv.rst.Fields(DATA_ACCOUNT_ID_VARIABLE).Value
            value = srv.rst.Fields(DATA_VALUE_VARIABLE).Value
            currency_ = srv.rst.Fields(ENTITIES_CURRENCY_VARIABLE).Value

            If currency_ <> destination_currency AndAlso typesDict(account_id) = MONETARY_ACCOUNT_TYPE Then

                If conversion_flagDict(account_id) = FLUX_CONVERSION Then rate_type = ExchangeRate.AVERAGE_RATE Else rate_type = ExchangeRate.CLOSING_RATE
                currency_token = currency_ & CURRENCIES_SEPARATOR & destination_currency
                value = value * exchange_rates_dictionary(currency_token)(srv.rst.Fields(DATA_PERIOD_VARIABLE).Value)(rate_type)

            End If
            AddAdjustmentToDictionary(adjustments_dic, _
                               srv.rst.Fields(DATA_ENTITY_ID_VARIABLE).Value, _
                               account_id, _
                               srv.rst.Fields(DATA_PERIOD_VARIABLE).Value, _
                               srv.rst.Fields(DATA_ADJUSTMENT_ID_VARIABLE).Value, _
                               value)
            srv.rst.MoveNext()
        Loop

    End Sub

    Private Sub AddAdjustmentToDictionary(ByRef adjustments_dic As Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Int32, Double)))), _
                                          ByVal entity_id As String, _
                                          ByVal account_id As String, _
                                          ByVal period As Int32, _
                                          ByVal adjustment_id As String, _
                                          ByVal value As Double)

        If adjustments_dic.Keys.Contains(account_id) Then
            If adjustments_dic(account_id).Keys.Contains(entity_id) Then
                If adjustments_dic(account_id)(entity_id).Keys.Contains(adjustment_id) Then
                    If adjustments_dic(account_id)(entity_id)(adjustment_id).Keys.Contains(period) = True Then
                        MsgBox("PPS error 10")
                    End If
                Else
                    Dim tmp_periods_dic As New Dictionary(Of Int32, Double)
                    adjustments_dic(account_id)(entity_id).Add(adjustment_id, tmp_periods_dic)
                End If
            Else
                Dim tmp_periods_dic As New Dictionary(Of Int32, Double)
                Dim tmp_adjts_dic As New Dictionary(Of String, Dictionary(Of Int32, Double))
                tmp_adjts_dic.Add(adjustment_id, tmp_periods_dic)
                adjustments_dic(account_id).Add(entity_id, tmp_adjts_dic)
            End If
        Else
            Dim tmp_period_dic As New Dictionary(Of Int32, Double)
            Dim tmp_adjts_dict As New Dictionary(Of String, Dictionary(Of Int32, Double))
            tmp_adjts_dict.Add(adjustment_id, tmp_period_dic)
            Dim tmp_entity_dict As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of Int32, Double)))
            tmp_entity_dict.Add(entity_id, tmp_adjts_dict)
            adjustments_dic.Add(account_id, tmp_entity_dict)
        End If
        adjustments_dic(account_id)(entity_id)(adjustment_id).Add(period, value)

    End Sub

#End Region


#Region "Single Value Query"

    ' Below: is it safe ? !!
    Protected Friend Shared Function GetSingleValue(ByRef version_id As String, _
                                                    ByRef entity_id As String, _
                                                    ByRef account_id As String, _
                                                    ByRef period As Integer, _
                                                    Optional ByRef client_id As String = "", _
                                                    Optional ByRef product_id As String = "", _
                                                    Optional ByRef adjustment_id As String = "") As Double

        Dim srv As New ModelServer
        srv.OpenRst(GlobalVariables.database & "." & version_id, ModelServer.FWD_CURSOR)

        Dim str_filter As String = DATA_ACCOUNT_ID_VARIABLE & "='" & account_id & "' AND " & _
                                   DATA_ENTITY_ID_VARIABLE & "='" & entity_id & "' AND " & _
                                   DATA_PERIOD_VARIABLE & "=" & period

        If client_id <> "" Then str_filter = str_filter & " AND " & DATA_CLIENT_ID_VARIABLE & "='" & client_id & "'"
        If product_id <> "" Then str_filter = str_filter & " AND " & DATA_PRODUCT_ID_VARIABLE & "='" & client_id & "'"
        If adjustment_id <> "" Then str_filter = str_filter & " AND " & DATA_ADJUSTMENT_ID_VARIABLE & "='" & client_id & "'"

        srv.rst.Filter = str_filter

        If srv.rst.EOF = True Or srv.rst.BOF = True Then
            srv.CloseRst()
            Return 0
        End If
        Dim value = srv.rst.Fields(DATA_VALUE_VARIABLE).Value
        srv.CloseRst()
        Return value

    End Function

#End Region


#Region "DB Filters"

#Region "Filters Reinitialization"

    ' Reinitialize filters lists so that all values are present
    Protected Friend Sub InitializeFilterLists(ByRef input_entities_id_list As List(Of String), _
                                               Optional ByRef input_clients_id_list As List(Of String) = Nothing, _
                                               Optional ByRef input_products_id_list As List(Of String) = Nothing, _
                                               Optional ByRef input_adjustments_id_list As List(Of String) = Nothing)

        ResetEntitiesFilter(input_entities_id_list)
        ResetclientsFilter(input_clients_id_list)
        ResetproductsFilter(input_products_id_list)
        ResetadjustmentsFilter(input_adjustments_id_list)

    End Sub

    Protected Friend Sub ResetEntitiesFilter(ByRef input_entities_id_list As List(Of String))

        entities_id_filter_list = input_entities_id_list
        entities_filter_flag = False

    End Sub

    Protected Friend Sub ResetclientsFilter(ByRef input_clients_id_list As List(Of String))

        If input_clients_id_list Is Nothing Then
            clients_id_filter_list = initial_clients_id_list
            clients_filter_flag = False
        Else
            clients_id_filter_list = input_clients_id_list
            clients_filter_flag = True
        End If


    End Sub

    Protected Friend Sub ResetproductsFilter(ByRef input_products_id_list As List(Of String))

        If input_products_id_list Is Nothing Then
            products_id_filter_list = initial_products_id_list
            products_filter_flag = False
        Else
            products_id_filter_list = input_products_id_list
            products_filter_flag = True
        End If


    End Sub

    Protected Friend Sub ResetadjustmentsFilter(ByRef input_adjustments_id_list As List(Of String))

        If input_adjustments_id_list Is Nothing Then
            adjustments_id_filter_list = initial_adjustments_id_list
            adjustments_filter_flag = False
        Else
            adjustments_id_filter_list = input_adjustments_id_list
            adjustments_filter_flag = True
        End If

    End Sub

#End Region

#Region "Filters Additive Methods"

    Protected Friend Sub UpdateEntitiesFilter(ByRef entities_id_short_list As List(Of String))

        entities_id_filter_list = Utilities_Functions.GetShortList(entities_id_filter_list, entities_id_short_list)
        entities_filter_flag = True

    End Sub

    Protected Friend Sub UpdateClientsFilter(ByRef clients_id_short_list As List(Of String))

        clients_id_filter_list = Utilities_Functions.GetShortList(clients_id_filter_list, clients_id_short_list)
        clients_filter_flag = True

    End Sub

    Protected Friend Sub UpdateProductsFilter(ByRef products_id_short_list As List(Of String))

        products_id_filter_list = Utilities_Functions.GetShortList(products_id_filter_list, products_id_short_list)
        products_filter_flag = True

    End Sub

    Protected Friend Sub UpdateAdjustmentsFilter(ByRef adjustments_id_short_list As List(Of String))

        adjustments_id_filter_list = Utilities_Functions.GetShortList(adjustments_id_filter_list, adjustments_id_short_list)
        adjustments_filter_flag = True

    End Sub

#End Region

    Private Function GetAdditionnalWhereClauseFromFilters() As String

        Dim str_SQL As String = ""
        Dim entities_sql_filter As String = DATA_ENTITY_ID_VARIABLE & " IN ('" + Join(entities_id_filter_list.ToArray, "','") + "')"
        Dim clients_sql_filter As String = DATA_CLIENT_ID_VARIABLE & " IN ('" + Join(clients_id_filter_list.ToArray, "','") + "')"
        Dim products_sql_filter As String = DATA_PRODUCT_ID_VARIABLE & " IN ('" + Join(products_id_filter_list.ToArray, "','") + "')"
        Dim adjustments_sql_filter As String = DATA_ADJUSTMENT_ID_VARIABLE & " IN ('" + Join(adjustments_id_filter_list.ToArray, "','") + "')"

        If entities_filter_flag = True Then str_SQL = str_SQL & " AND " & entities_sql_filter
        If clients_filter_flag = True Then str_SQL = str_SQL & " AND " & clients_sql_filter
        If products_filter_flag = True Then str_SQL = str_SQL & " AND " & products_sql_filter
        If adjustments_filter_flag = True Then str_SQL = str_SQL & " AND " & adjustments_sql_filter

        If str_SQL = "" Then
            Return str_SQL
        Else
            Return Right(str_SQL, Len(str_SQL) - Len(" AND "))
        End If

    End Function

    ' below -> cache quid !
    Private Function GetWhereClause(Optional ByRef input_entities_id_list As List(Of String) = Nothing, _
                                    Optional ByRef input_clients_id_list As List(Of String) = Nothing, _
                                    Optional ByRef input_products_id_list As List(Of String) = Nothing, _
                                    Optional ByRef input_adjustments_id_list As List(Of String) = Nothing) As String

        Dim str_SQL As String = ""
        If Not input_entities_id_list Is Nothing _
        AndAlso input_entities_id_list.Count > 0 Then str_SQL = str_SQL & " AND " & DATA_ENTITY_ID_VARIABLE & " IN ('" + Join(input_entities_id_list.ToArray, "','") + "')"

        If Not input_clients_id_list Is Nothing _
        AndAlso input_clients_id_list.Count > 0 Then str_SQL = str_SQL & " AND " & DATA_CLIENT_ID_VARIABLE & " IN ('" + Join(input_clients_id_list.ToArray, "','") + "')"

        If Not input_products_id_list Is Nothing _
        AndAlso input_products_id_list.Count > 0 Then str_SQL = str_SQL & " AND " & DATA_PRODUCT_ID_VARIABLE & " IN ('" + Join(input_products_id_list.ToArray, "','") + "')"

        If Not input_adjustments_id_list Is Nothing _
        AndAlso input_adjustments_id_list.Count > 0 Then str_SQL = str_SQL & " AND " & DATA_ADJUSTMENT_ID_VARIABLE & " IN ('" + Join(input_adjustments_id_list.ToArray, "','") + "')"

        If str_SQL = "" Then
            Return str_SQL
        Else
            Return Right(str_SQL, Len(str_SQL) - Len(" AND "))
        End If

    End Function



#End Region


#Region "Utilities"

    ' Exchange Rates: (currency_token)(period)(rate_type)
    Protected Friend Shared Function GetExchangeRatesDictionary(ByRef version_id As String, _
                                                                ByRef destination_currency As String, _
                                                                ByRef entities_id_List As String()) _
                                                                As Dictionary(Of String, Dictionary(Of Int32, Dictionary(Of String, Double)))

        Dim rates_dic As New Dictionary(Of String, Dictionary(Of Int32, Dictionary(Of String, Double)))
        Dim versions As New Version
        Dim rates_version_id As String = versions.ReadVersion(version_id, VERSIONS_RATES_VERSION_ID_VAR)
        Dim ExchangeRates As New ExchangeRate(rates_version_id)
        Dim reverse_flag As Boolean = False
        Dim start_period As Int32 = versions.ReadVersion(version_id, VERSIONS_START_PERIOD_VAR)
        Dim nb_periods As Int32 = versions.ReadVersion(version_id, VERSIONS_NB_PERIODS_VAR)
        Dim time_configuration As String = versions.ReadVersion(version_id, VERSIONS_TIME_CONFIG_VARIABLE)
        versions.Close()

        Dim currencies_list As List(Of String) = GetUniqueCurrencies(entities_id_List)
        For Each original_currency As String In currencies_list

            Dim currencies_token As String = original_currency & CURRENCIES_SEPARATOR & destination_currency
            reverse_flag = False
            If destination_currency <> MAIN_CURRENCY Then reverse_flag = True

            ' !! Attention stub car besoin d'une fonction supp qui créé les taux 
            ' -> NOK/USD par exemple (en passant par la main currency) -> à voir car il y a le reverse token

            If original_currency <> destination_currency Then _
            rates_dic.Add(currencies_token, ExchangeRates.BuildExchangeRatesDictionary(time_configuration, _
                                                                                       start_period, _
                                                                                       nb_periods, _
                                                                                       currencies_token, _
                                                                                       reverse_flag))
        Next
        Return rates_dic

    End Function

    Friend Sub ClearDatasDictionaries()

        accounts_ID_hash.Clear()
        periods_ID_hash.Clear()
        values_ID_hash.Clear()
        stored_inputs_list.Clear()

    End Sub

    Protected Friend Shared Function GetUniqueCurrencies(ByRef entities_id_list As String()) As List(Of String)

        Dim unique_currencies As New List(Of String)
        Dim entities_id_currencies_dic As Hashtable = EntitiesMapping.GetEntitiesDictionary(ENTITIES_ID_VARIABLE, ENTITIES_CURRENCY_VARIABLE)
        For Each entity_id In entities_id_list
            If unique_currencies.Contains(entities_id_currencies_dic(entity_id)) = False Then _
                unique_currencies.Add(entities_id_currencies_dic(entity_id))
        Next
        Return unique_currencies

    End Function

    Protected Friend Sub CloseRST()

        srv.rst.Close()

    End Sub

    Protected Overrides Sub finalize()

        If srv.rst.State = 1 Then srv.rst.Close()
        MyBase.Finalize()

    End Sub

#End Region


End Class
