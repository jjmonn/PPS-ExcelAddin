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
        srv.openRstSQL(VIEWS_DATABASE & GlobalVariables.Entities_View, ModelServer.FWD_CURSOR)
        srv.rst.Filter = str_sql_filter
        While srv.rst.EOF = False
            tmp_list.Add(srv.rst.Fields(ENTITIES_ID_VARIABLE).Value)
        End While
        Return tmp_list

    End Function

    Protected Friend Shared Function GetAnalysisAxisFilterList(ByRef table_name As String, _
                                                               ByRef str_sql_filter As String) As List(Of String)

        Dim srv As New ModelServer
        Dim tmp_list As New List(Of String)
        srv.openRstSQL(table_name, ModelServer.FWD_CURSOR)
        srv.rst.Filter = str_sql_filter
        While srv.rst.EOF = False
            tmp_list.Add(srv.rst.Fields(ANALYSIS_AXIS_ID_VAR).Value)
        End While
        Return tmp_list

    End Function



End Class
