' CEntitiesDeletion.vb
'
' Manages the deletion of entities in data tables
'
' to do:
'       - Error tracking ?
'       - Could be added elsewhere
'
'
' Known Bugs:
'
'
' Auhtor: Julien Monnereau
' Last modified: 10/12/2014


Imports System.Collections.Generic


Friend Class SQLEntities


#Region "Instance Variables"

    'Objects
    Private CredentialsController As New CredentialsController
    Private srv As New ModelServer
    Private dataTablesList As List(Of String)


#End Region


#Region "Initialize"

    Friend Sub New()

        dataTablesList = VersionsMapping.GetVersionsList(VERSIONS_CODE_VARIABLE)

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub DeleteEntityFromPlatform(ByRef entityKey As String)

        DeleteEntityFromDataTables(entityKey)
        CredentialsController.DeleteCredentialKey(entityKey)

    End Sub

    Protected Friend Function CreateNewEntitiesVariable(ByRef variable_id As String) As Boolean

        Dim column_values_length As Int32 = CATEGORIES_TOKEN_SIZE + Len(NON_ATTRIBUTED_SUFIX)
        Return srv.sqlQuery("ALTER TABLE " + LEGAL_ENTITIES_DATABASE + "." + ENTITIES_TABLE + _
                            " ADD COLUMN " & variable_id & " VARCHAR(" & column_values_length & ") DEFAULT '" & variable_id & NON_ATTRIBUTED_SUFIX & "'")

    End Function

    Protected Friend Function DeleteEntitiesVariable(ByRef variable_id) As Boolean

        Return srv.sqlQuery("ALTER TABLE " + LEGAL_ENTITIES_DATABASE + "." + ENTITIES_TABLE + _
                            " DROP COLUMN " & variable_id)

    End Function

#End Region


#Region "Utilities"

    Private Sub DeleteEntityFromDataTables(ByRef entity_id As String)

        ' replace by a sub in a class managing all data databases transactions ?

        For Each Version As String In dataTablesList
            srv.sqlQuery("DELETE FROM " & DATA_DATABASE & "." & Version & _
                         " WHERE " & DATA_ENTITY_ID_VARIABLE & "='" & entity_id & "'")
        Next

    End Sub

    Protected Friend Function ReplaceEntitiesCategoryValue(ByRef category_id As String, _
                                                       ByRef origin_value As String) As Boolean

        Dim new_value = category_id & NON_ATTRIBUTED_SUFIX
        Return srv.sqlQuery("UPDATE " & LEGAL_ENTITIES_DATABASE & "." + ENTITIES_TABLE & _
                            " SET " & category_id & "='" & new_value & "'" & _
                            " WHERE " & category_id & "='" & origin_value & "'")

    End Function

    Protected Friend Function SetNewCategoryDefaultValue(ByRef category_id As String) As Boolean

        Dim new_value = category_id & NON_ATTRIBUTED_SUFIX
        Return srv.sqlQuery("UPDATE " & LEGAL_ENTITIES_DATABASE & "." + ENTITIES_TABLE & _
                            " SET " & category_id & "='" & new_value & "'")

    End Function


#End Region








End Class
