' SQLDataViews.vb
'
' SQL queries for Data tables Views creation
'
'
'
'
'
' author: Julien Monnereau
' Last modified: 03/12/2014


Imports System.Collections.Generic

Friend Class SQLDataViews


    ' set up view creation SQL query
    Protected Friend Function ViewCreationQuery(ByRef dataTableName As String, _
                                                ByRef viewName As String, _
                                                ByRef credential_levels_list As List(Of String)) _
                                                As Boolean

        Dim srv As New ModelServer
        Dim SqlCondition As String = DefineDataWhereClause(credential_levels_list)
        Dim SqlQuery As String = "CREATE OR REPLACE VIEW " + VIEWS_DATABASE + "." + viewName + " AS " + _
                                 "SELECT " + _
                                  DATA_ACCOUNT_ID_VARIABLE + "," + _
                                  DATA_ASSET_ID_VARIABLE + "," + _
                                  DATA_PERIOD_VARIABLE + "," + _
                                  DATA_VALUE_VARIABLE + _
                                 " FROM " + DATA_DATABASE + "." + dataTableName + " D," + _
                                  LEGAL_ENTITIES_DATABASE + "." + ENTITIES_TABLE + " A" + _
                                 " WHERE A." + ASSETS_TREE_ID_VARIABLE + "= D." + DATA_ASSET_ID_VARIABLE

        If SqlCondition <> "" Then SqlQuery = SqlQuery + " AND " + SqlCondition
        Return srv.sqlQuery(SqlQuery)


    End Function

    Protected Friend Function DeleteViews() As Boolean



    End Function

    ' Set up the where clause for the DATA view by creating WHERE CLAUSE on SELECT the credential levels
    Private Function DefineDataWhereClause(credential_levels_list As List(Of String)) As String

        If credential_levels_list.Count > 0 Then
            Dim CredentialLevelsString As String = "'" + Join(credential_levels_list.ToArray, "','") + "'"
            Return "A." + ASSETS_CREDENTIAL_ID_VARIABLE + " IN (" + CredentialLevelsString + ")"
        Else
            Return ""
        End If

    End Function

   



End Class
