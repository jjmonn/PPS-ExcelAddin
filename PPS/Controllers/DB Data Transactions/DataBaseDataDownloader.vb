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
' Last modified: 02/12/2014
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
    Private typesDict As Hashtable

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


#Region "Initialize"

    Friend Sub New()

        typesDict = AccountsMapping.GetAccountsDictionary(ACCOUNT_ID_VARIABLE, ACCOUNT_TYPE_VARIABLE)

    End Sub


#End Region


    '#Region "Aggregations Queries"


    '    ' Create Assets Aggregated DataArrays NOT CONVERTED
    '    ' Param1: EntitiesIDList (array of string holding the tokens of entities to compute)
    '    ' Param2: ViewName to query on
    '    ' Param3: Entities short list
    '    'Friend Function getAggregateOutputsArraysNonConverted(ByRef entitiesIDList() As String, _
    '    '                                                      ByRef ViewName As String, _
    '    '                                                      Optional ByRef strSqlAdditionalClause As String = "") As Boolean

    '    '    If BuildDataRSTWithoutCurrencies(entitiesIDList, ViewName, strSqlAdditionalClause) Then
    '    '        Dim data_array(,) As Object
    '    '        data_array = srv.rst.GetRows
    '    '        BuildOutputsArrays(data_array)
    '    '        srv.rst.Close()
    '    '        Return True
    '    '    Else
    '    '        Return False
    '    '    End If

    '    'End Function

    '    'Friend Function BuildDataRSTWithoutCurrencies(ByRef entitiesIDList() As String, _
    '    '                                              ByRef ViewName As String, _
    '    '                                              Optional ByRef strSqlAdditionalClause As String = "") As Boolean


    '    '    Dim strSQL As String
    '    '    Dim keysSelection As String = "'" + Join(entitiesIDList, "','") + "'"

    '    '    If strSqlAdditionalClause <> "" Then         ' Case filter on specific entities

    '    '        strSQL = "SELECT " + "D." + DATA_PERIOD_VARIABLE + "," _
    '    '                   + "D." + DATA_ACCOUNT_ID_VARIABLE + "," _
    '    '                   + "SUM(" + DATA_VALUE_VARIABLE + ")" + "," _
    '    '                   + DATA_ASSET_ID_VARIABLE _
    '    '                   + " FROM " + VIEWS_DATABASE + "." + ViewName + " D" + ", " + VIEWS_DATABASE + "." + Entities_View + " A" _
    '    '                   + " WHERE " + "D." + DATA_ASSET_ID_VARIABLE + "=" + "A." + ASSETS_TREE_ID_VARIABLE _
    '    '                   + " AND " + "A." + ASSETS_TREE_ID_VARIABLE + " IN " + "(" + keysSelection + ")" _
    '    '                   + " AND " + strSqlAdditionalClause _
    '    '                   + " GROUP BY " + "D." + DATA_PERIOD_VARIABLE + ", " _
    '    '                   + "D." + DATA_ACCOUNT_ID_VARIABLE

    '    '    Else                                            ' Case no entities filter
    '    '        strSQL = "SELECT " + "D." + DATA_PERIOD_VARIABLE + ", " _
    '    '                   + "D." + DATA_ACCOUNT_ID_VARIABLE + ", " _
    '    '                   + "SUM(" + DATA_VALUE_VARIABLE + ")" + "," _
    '    '                   + DATA_ASSET_ID_VARIABLE _
    '    '                   + " FROM " + VIEWS_DATABASE + "." + ViewName + " D" + ", " + VIEWS_DATABASE + "." + Entities_View + " A" _
    '    '                   + " WHERE " + "D." + DATA_ASSET_ID_VARIABLE + "=" + "A." + ASSETS_TREE_ID_VARIABLE _
    '    '                   + " AND " + "A." + ASSETS_TREE_ID_VARIABLE + " IN " + "(" + keysSelection + ")" _
    '    '                   + " GROUP BY " + "D." + DATA_PERIOD_VARIABLE + ", " _
    '    '                   + "D." + DATA_ACCOUNT_ID_VARIABLE
    '    '    End If

    '    '    srv.openRstSQL(strSQL, ModelServer.FWD_CURSOR)
    '    '    If srv.rst.BOF = True Or srv.rst.EOF = True Then
    '    '        srv.rst.Close()
    '    '        Return False
    '    '    Else
    '    '        Return True
    '    '    End If

    '    'End Function

    '#End Region


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


        Dim strSQL As String
        Dim keysSelection As String = "'" + Join(entitiesIDList, "','") + "'"

        If strSqlAdditionalClause <> "" Then         ' Case filter on specific entities

            strSQL = "SELECT " + "D." + DATA_PERIOD_VARIABLE + "," _
                       + "D." + DATA_ACCOUNT_ID_VARIABLE + "," _
                       + DATA_VALUE_VARIABLE + "," _
                       + DATA_ASSET_ID_VARIABLE _
                       + " FROM " + VIEWS_DATABASE + "." + ViewName + " D" + ", " + VIEWS_DATABASE + "." + Entities_View + " A" _
                       + " WHERE " + "D." + DATA_ASSET_ID_VARIABLE + "=" + "A." + ASSETS_TREE_ID_VARIABLE _
                       + " AND " + "A." + ASSETS_TREE_ID_VARIABLE + " IN " + "(" + keysSelection + ")" _
                       + " AND " + strSqlAdditionalClause

        Else                                            ' Case no entities filter
            strSQL = "SELECT " + "D." + DATA_PERIOD_VARIABLE + ", " _
                       + "D." + DATA_ACCOUNT_ID_VARIABLE + ", " _
                       + DATA_VALUE_VARIABLE + "," _
                       + DATA_ASSET_ID_VARIABLE _
                       + " FROM " + VIEWS_DATABASE + "." + ViewName + " D" + ", " + VIEWS_DATABASE + "." + Entities_View + " A" _
                       + " WHERE " + "D." + DATA_ASSET_ID_VARIABLE + "=" + "A." + ASSETS_TREE_ID_VARIABLE _
                       + " AND " + "A." + ASSETS_TREE_ID_VARIABLE + " IN " + "(" + keysSelection + ")"
        End If

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

#End Region


#Region "Single Entity Queries"

    Public Function GetEntityInputsNonConverted(ByRef entityKey As String, _
                                                ByRef ViewName As String) As Boolean

        Dim strSql As String = "SELECT " + DATA_PERIOD_VARIABLE + ", " _
                             + DATA_ACCOUNT_ID_VARIABLE + ", " _
                             + DATA_VALUE_VARIABLE _
                             + " FROM " + VIEWS_DATABASE + "." + ViewName _
                             + " WHERE " + DATA_ASSET_ID_VARIABLE + "='" + entityKey + "'"


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


#Region "Single Value Query"

    ' Below: is it safe ? !!
    Protected Friend Shared Function GetSingleValue(ByRef version_id As String, _
                                                    ByRef entity_id As String, _
                                                    ByRef account_id As String, _
                                                    ByRef period As Integer) As Double

        Dim srv As New ModelServer
        srv.openRst(VIEWS_DATABASE & "." & version_id & User_Credential, ModelServer.FWD_CURSOR)
        srv.rst.Filter = DATA_ACCOUNT_ID_VARIABLE & "='" & account_id & "' AND " & _
                         DATA_ASSET_ID_VARIABLE & "='" & entity_id & "' AND " & _
                         DATA_PERIOD_VARIABLE & "=" & period & ""
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

    Friend Sub CloseRST()

        srv.rst.Close()

    End Sub

#End Region


End Class
