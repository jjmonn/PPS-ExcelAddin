' SQLDataViews.vb
'
' SQL queries for Data tables Views creation
'
'
'
'
'
' author: Julien Monnereau
' Last modified: 18/01/2015


Imports System.Collections.Generic

Friend Class SQLDataViews



    ' process to be reviewed => credentials + views reconstructions
    ' Set up view creation SQL query
    Protected Friend Function ViewCreationQuery(ByRef dataTableName As String, _
                                                ByRef viewName As String, _
                                                ByRef credential_levels_list As List(Of String)) _
                                                As Boolean

        Dim srv As New ModelServer
        Dim SqlCondition As String = DefineDataWhereClause(credential_levels_list)
        Dim SqlQuery As String = "CREATE OR REPLACE VIEW " + VIEWS_DATABASE + "." + viewName + " AS " + _
                                 "SELECT " + _
                                  DATA_ACCOUNT_ID_VARIABLE + "," + _
                                  DATA_ENTITY_ID_VARIABLE + "," + _
                                  DATA_PERIOD_VARIABLE + "," + _
                                  DATA_VALUE_VARIABLE + "," + _
                                  DATA_CLIENT_ID_VARIABLE + "," + _
                                  DATA_PRODUCT_ID_VARIABLE + "," + _
                                  DATA_ADJUSTMENT_ID_VARIABLE + _
                                 " FROM " + DATA_DATABASE + "." + dataTableName + " D," + _
                                  LEGAL_ENTITIES_DATABASE + "." + ENTITIES_TABLE + " A" + _
                                 " WHERE A." + ENTITIES_ID_VARIABLE + "= D." + DATA_ENTITY_ID_VARIABLE

        If SqlCondition <> "" Then SqlQuery = SqlQuery + " AND " + SqlCondition
        Return srv.sqlQuery(SqlQuery)


    End Function

    ' Set up the where clause for the DATA view by creating WHERE CLAUSE on SELECT the credential levels
    Private Function DefineDataWhereClause(credential_levels_list As List(Of String)) As String

        If credential_levels_list.Count > 0 Then
            Dim CredentialLevelsString As String = "'" + Join(credential_levels_list.ToArray, "','") + "'"
            Return "A." + ENTITIES_CREDENTIAL_ID_VARIABLE + " IN (" + CredentialLevelsString + ")"
        Else
            Return ""
        End If

    End Function

   



End Class
