' SQLFilterListsGenerators
' 
' Issue list of filters for Analysis Axis (Entities, Clients, Products) from SQL WHERE 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 17/03/2015


Imports System.Collections.Generic


Friend Class SQLFilterListsGenerators

    Protected Friend Shared Function GetEntitiesFilterList(ByRef str_sql_filter As String) As List(Of String)

        Dim srv As New ModelServer
        Dim tmp_list As New List(Of String)
        srv.OpenRst(GlobalVariables.database & "." & ENTITIES_TABLE, ModelServer.FWD_CURSOR)
        srv.rst.Filter = str_sql_filter
        While srv.rst.EOF = False
            tmp_list.Add(srv.rst.Fields(ENTITIES_ID_VARIABLE).Value)
            srv.rst.MoveNext()
        End While
        Return tmp_list

    End Function

    Protected Friend Shared Function GetAnalysisAxisFilterList(ByRef table_name As String, _
                                                               ByRef str_sql_filter As String) As List(Of String)

        Dim srv As New ModelServer
        Dim tmp_list As New List(Of String)
        Dim strSQL As String = "SELECT " + ANALYSIS_AXIS_ID_VAR + _
                               " FROM " + GlobalVariables.database & "." & table_name

        If str_sql_filter <> "" Then strSQL = strSQL + " WHERE " + str_sql_filter

        srv.openRstSQL(strSQL, ModelServer.FWD_CURSOR)
        While srv.rst.EOF = False
            tmp_list.Add(srv.rst.Fields(ANALYSIS_AXIS_ID_VAR).Value)
            srv.rst.MoveNext()
        End While
        Return tmp_list

    End Function



End Class
