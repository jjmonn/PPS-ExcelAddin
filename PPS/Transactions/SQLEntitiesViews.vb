' SQLEntitiesViews.vb
'
' SQL queries for Entities table Views creation
'
'
'
'
'
' author: Julien Monnereau
' Last modified: 03/12/2014


Imports System.Collections.Generic


Friend Class SQLEntitiesViews


    ' Create a new ENTITIES TABLE view
    'Protected Friend Function CreateEntitiesView(ByRef credential_level As Int32, _
    '                                             ByRef credential_levels_list As List(Of String)) _
    '                                             As Boolean

    '    Dim srv As New ModelServer
    '    Dim viewName As String = ENTITIES_TABLE & credential_level
    '    Dim SqlCondition As String = DefineEntitiesWhereClause(credential_levels_list)
    '    Dim SqlQuery As String = "CREATE OR REPLACE VIEW " + GlobalVariables.database + "." + viewName + " AS " + _
    '                             "SELECT * " + _
    '                             "FROM " + GlobalVariables.database + "." + ENTITIES_TABLE

    '    If SqlCondition <> "" Then SqlQuery = SqlQuery + " WHERE " + SqlCondition
    '    Return srv.sqlQuery(SqlQuery)

    'End Function

    ' Set up the where clause for the ENTITIES view by creating WHERE CLAUSE on SELECT the credential levels
    'Private Function DefineEntitiesWhereClause(credential_levels_list As List(Of String)) As String

    '    If credential_levels_list.Count > 0 Then
    '        Dim CredentialLevelsString As String = "'" + Join(credential_levels_list.ToArray, "','") + "'"
    '        Return ENTITIES_CREDENTIAL_ID_VARIABLE + " IN (" + CredentialLevelsString + ")"
    '    Else
    '        Return ""
    '    End If

    'End Function



#Region "Utilities"

    Protected Friend Sub DeleteViewQuery(ByRef viewName As String)

        Dim srv As New ModelServer
        srv.sqlQuery("DROP VIEW " + GlobalVariables.database + "." + viewName)
   
    End Sub


#End Region


End Class
