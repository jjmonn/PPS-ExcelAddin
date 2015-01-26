' cDatabaseDataDownloader.vb
' 
'  Manage data tables downloads
'
'
' To do:
'
'      
' Known bugs:
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 24/01/2015
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
    Friend accounts_ID_hash As New Dictionary(Of String, String())
    Friend periods_ID_hash As New Dictionary(Of String, Integer())
    Friend values_ID_hash As New Dictionary(Of String, Double())

    Friend AccKeysArray() As String
    Friend PeriodArray() As Integer
    Friend ValuesArray() As Double
    Friend currentRatesVersionCode As String = ""

    ' Constants
    Private Const MONETARY_TYPE_CODE As String = "MO"


#End Region


#Region "Aggregations Queries"


    '     Create Assets Aggregated DataArrays NOT CONVERTED
    '    Param1: EntitiesIDList (array of string holding the tokens of entities to compute)
    '   Param2: ViewName to query on
    '  Param3: Entities short list
    Friend Function getAggregateOutputsArraysNonConverted(ByRef entitiesIDList() As String, _
                                                          ByRef ViewName As String, _
                                                          Optional ByRef strSqlAdditionalClause As String = "") As Boolean

        If BuildDataRSTWithoutCurrencies(entitiesIDList, ViewName, strSqlAdditionalClause) Then
            Dim data_array(,) As Object
            data_array = srv.rst.GetRows
            BuildOutputsArrays(data_array)
            srv.rst.Close()
            Return True
        Else
            Return False
        End If

    End Function

    Friend Function BuildDataRSTWithoutCurrencies(ByRef entitiesIDList() As String, _
                                                  ByRef ViewName As String, _
                                                  Optional ByRef strSqlAdditionalClause As String = "") As Boolean


        Dim strSQL As String
        Dim keysSelection As String = "'" + Join(entitiesIDList, "','") + "'"

        If strSqlAdditionalClause <> "" Then         ' Case filter on specific entities

            strSQL = "SELECT " + "D." + DATA_PERIOD_VARIABLE + "," _
                       + "D." + DATA_ACCOUNT_ID_VARIABLE + "," _
                       + "SUM(" + DATA_VALUE_VARIABLE + ")" + "," _
                       + DATA_ASSET_ID_VARIABLE _
                       + " FROM " + VIEWS_DATABASE + "." + ViewName + " D" + ", " + VIEWS_DATABASE + "." + Entities_View + " A" _
                       + " WHERE " + "D." + DATA_ASSET_ID_VARIABLE + "=" + "A." + ASSETS_TREE_ID_VARIABLE _
                       + " AND " + "A." + ASSETS_TREE_ID_VARIABLE + " IN " + "(" + keysSelection + ")" _
                       + " AND " + strSqlAdditionalClause _
                       + " GROUP BY " + "D." + DATA_PERIOD_VARIABLE + ", " _
                       + "D." + DATA_ACCOUNT_ID_VARIABLE

        Else                                            ' Case no entities filter
            strSQL = "SELECT " + "D." + DATA_PERIOD_VARIABLE + ", " _
                       + "D." + DATA_ACCOUNT_ID_VARIABLE + ", " _
                       + "SUM(" + DATA_VALUE_VARIABLE + ")" + "," _
                       + DATA_ASSET_ID_VARIABLE _
                       + " FROM " + VIEWS_DATABASE + "." + ViewName + " D" + ", " + VIEWS_DATABASE + "." + Entities_View + " A" _
                       + " WHERE " + "D." + DATA_ASSET_ID_VARIABLE + "=" + "A." + ASSETS_TREE_ID_VARIABLE _
                       + " AND " + "A." + ASSETS_TREE_ID_VARIABLE + " IN " + "(" + keysSelection + ")" _
                       + " GROUP BY " + "D." + DATA_PERIOD_VARIABLE + ", " _
                       + "D." + DATA_ACCOUNT_ID_VARIABLE
        End If

        srv.openRstSQL(strSQL, ModelServer.FWD_CURSOR)
        If srv.rst.BOF = True Or srv.rst.EOF = True Then
            srv.rst.Close()
            Return False
        Else
            Return True
        End If

    End Function

#End Region


#Region "Dll Entities Aggregation data hash build"


    Friend Function build_data_hash(ByRef entitiesIDList() As String, _
                                    ByRef ViewName As String, _
                                    Optional ByRef strSqlAdditionalClause As String = "") As Boolean

        ClearDatasDictionaries()
        If BuildDataRSTForEntityLoop(entitiesIDList, ViewName, strSqlAdditionalClause) Then
            For Each entity_id In entitiesIDList
                StoreEntityData(entity_id)
            Next
            srv.rst.Close()
            Return True
        Else
            Return False
        End If

    End Function

    Friend Function BuildDataRSTForEntityLoop(ByRef entitiesIDList() As String, _
                                             ByRef ViewName As String, _
                                             Optional ByRef strSqlAdditionalClause As String = "") As Boolean

        Dim keysSelection As String = "'" + Join(entitiesIDList, "','") + "'"

        Dim strSQL As String = "SELECT " + "D." + DATA_PERIOD_VARIABLE + "," _
                             + "D." + DATA_ACCOUNT_ID_VARIABLE + "," _
                             + "SUM(" + DATA_VALUE_VARIABLE + ") AS value," _
                             + DATA_ASSET_ID_VARIABLE _
                             + " FROM " + VIEWS_DATABASE + "." + ViewName + " D" + ", " + VIEWS_DATABASE + "." + Entities_View + " A" _
                             + " WHERE " + "D." + DATA_ASSET_ID_VARIABLE + "=" + "A." + ASSETS_TREE_ID_VARIABLE _
                             + " AND " + "A." + ASSETS_TREE_ID_VARIABLE + " IN " + "(" + keysSelection + ")"

        If strSqlAdditionalClause <> "" Then strSQL = strSQL + " AND " + strSqlAdditionalClause ' Case filter on specific entities
        Dim str_sql_group As String = " GROUP BY " + DATA_PERIOD_VARIABLE + "," + _
                                                     DATA_ACCOUNT_ID_VARIABLE + "," + _
                                                     DATA_ASSET_ID_VARIABLE
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

        srv.rst.Filter = DATA_ASSET_ID_VARIABLE & "='" & entity_id & "'"
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

        srv.rst.Filter = DATA_ASSET_ID_VARIABLE & "='" & entityID & "'"
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

    ' aggregated query -> filter on adjustment_id should be send within str_sql_query (optional)
    ' need an additional function retreiving only adjustments id values to display in DGVs



#End Region


#Region "Single Entity Queries"

    Protected Friend Function GetEntityInputsNonConverted(ByRef entityKey As String, _
                                                ByRef ViewName As String, _
                                                Optional ByVal adjustment_id As String = "") As Boolean

        Dim strSql As String = "SELECT " + DATA_PERIOD_VARIABLE + ", " _
                             + DATA_ACCOUNT_ID_VARIABLE + ", " _
                             + DATA_VALUE_VARIABLE _
                             + " FROM " + VIEWS_DATABASE + "." + ViewName _
                             + " WHERE " + DATA_ASSET_ID_VARIABLE + "='" + entityKey + "'"

        If adjustment_id <> "" Then strSql = strSql + " AND " + DATA_ADJUSTMENT_ID_VARIABLE + "='" + adjustment_id + "'"

        srv.openRstSQL(strSql, ModelServer.FWD_CURSOR)
        If srv.rst.BOF = True Or srv.rst.EOF = True Then
            srv.rst.Close()
            Return False
        Else
            Dim tmpArray(,) As Object = srv.rst.GetRows()
            srv.rst.Close()
            BuildOutputsArrays(tmpArray)
            Return True
        End If


    End Function

#End Region


#Region "Adjustments Queries"

    ' Return AdjustmentsDict (account_id)(entity_id)(adjustment_id)(period) -> value
    Protected Friend Function GetAdjustments(ByRef version_id As String, _
                                             ByRef entitiesIDList As String(), _
                                             ByRef destination_currency As String)

        Dim adjustments_dic As New Dictionary(Of String, Dictionary(Of String, Dictionary(Of String, Dictionary(Of Int32, Double))))
        If AdjustmentsQuery(version_id, entitiesIDList) = True Then

            ConvertAdjustments(version_id, _
                               destination_currency, _
                               adjustments_dic, _
                               entitiesIDList)
        End If
        Return adjustments_dic

    End Function

    Private Function AdjustmentsQuery(ByRef version_id As String, _
                                      ByRef entitiesIDList As String()) As Boolean

        Dim entities_ids As String = "'" + Join(entitiesIDList, "','") + "'"

        Dim strSQL As String = "SELECT D." & DATA_PERIOD_VARIABLE & "," _
                             & " D." & DATA_ACCOUNT_ID_VARIABLE & "," _
                             & " D." & DATA_VALUE_VARIABLE & "," _
                             & " D." & DATA_ASSET_ID_VARIABLE & "," _
                             & " D." & DATA_ADJUSTMENT_ID_VARIABLE & "," _
                             & " A." & ASSETS_CURRENCY_VARIABLE _
                             & " FROM " & VIEWS_DATABASE & "." & version_id & User_Credential & " D" & ", " & VIEWS_DATABASE + "." & Entities_View + " A" _
                             & " WHERE " & "D." & DATA_ASSET_ID_VARIABLE & "=" & "A." & ASSETS_TREE_ID_VARIABLE _
                             & " AND " & DATA_ASSET_ID_VARIABLE & " IN " & "(" & entities_ids & ")"

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
            currency_ = srv.rst.Fields(ASSETS_CURRENCY_VARIABLE).Value

            If currency_ <> destination_currency AndAlso typesDict(account_id) = MONETARY_ACCOUNT_TYPE Then

                If conversion_flagDict(account_id) = FLUX_CONVERSION Then rate_type = ExchangeRate.AVERAGE_RATE Else rate_type = ExchangeRate.CLOSING_RATE
                currency_token = currency_ & CURRENCIES_SEPARATOR & destination_currency
                value = value * exchange_rates_dictionary(currency_token)(srv.rst.Fields(DATA_PERIOD_VARIABLE).Value)(rate_type)

            End If
            AddAdjustmentToDictionary(adjustments_dic, _
                               srv.rst.Fields(DATA_ASSET_ID_VARIABLE).Value, _
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

    ' Excahnge Rates: (currency_token)(period)(rate_type)
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
            ' -> NOK/USD par exemple (en passant par la main currency)

            If original_currency <> destination_currency Then _
            rates_dic.Add(currencies_token, ExchangeRates.BuildExchangeRatesDictionary(time_configuration, _
                                                                                       start_period, _
                                                                                       nb_periods, _
                                                                                       currencies_token, _
                                                                                       reverse_flag))
        Next
        Return rates_dic

    End Function


#End Region


#Region "Single Value Query"

    ' Below: is it safe ? !!
    Protected Friend Shared Function GetSingleValue(ByRef version_id As String, _
                                                    ByRef entity_id As String, _
                                                    ByRef account_id As String, _
                                                    ByRef period As Integer, _
                                                    Optional ByRef adjustment_id As String = "") As Double

        Dim srv As New ModelServer
        srv.OpenRst(VIEWS_DATABASE & "." & version_id & User_Credential, ModelServer.FWD_CURSOR)

        Dim str_filter As String = DATA_ACCOUNT_ID_VARIABLE & "='" & account_id & "' AND " & _
                                   DATA_ASSET_ID_VARIABLE & "='" & entity_id & "' AND " & _
                                   DATA_PERIOD_VARIABLE & "=" & period

        If adjustment_id <> "" Then str_filter = str_filter + " AND " + DATA_ADJUSTMENT_ID_VARIABLE + "='" + adjustment_id + "'"
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


#Region "Utilities"

    Friend Sub ClearDatasDictionaries()

        accounts_ID_hash.Clear()
        periods_ID_hash.Clear()
        values_ID_hash.Clear()
        stored_inputs_list.Clear()

    End Sub

    Private Sub BuildOutputsArrays(ByRef data_array(,) As Object)

        ReDim AccKeysArray(UBound(data_array, 2))
        ReDim PeriodArray(UBound(data_array, 2))
        ReDim ValuesArray(UBound(data_array, 2))

        For i As Integer = 0 To UBound(data_array, 2)
            PeriodArray(i) = data_array(0, i)
            AccKeysArray(i) = data_array(1, i)
            ValuesArray(i) = data_array(2, i)
        Next

    End Sub

    Protected Friend Shared Function GetUniqueCurrencies(ByRef entities_id_list As String()) As List(Of String)

        Dim unique_currencies As New List(Of String)
        Dim entities_id_currencies_dic As Hashtable = EntitiesMapping.GetEntitiesDictionary(ASSETS_TREE_ID_VARIABLE, ASSETS_CURRENCY_VARIABLE)
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
