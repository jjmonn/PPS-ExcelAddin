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
' Last modified: 25/06/2015


Imports System.Collections.Generic


Friend Class SQLEntities


#Region "Instance Variables"

    'Objects
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
        '  CredentialsController.DeleteCredentialKey(entityKey)

    End Sub

  
#End Region


#Region "Utilities"

    Private Sub DeleteEntityFromDataTables(ByRef entity_id As String)

        ' replace by a sub in a class managing all data databases transactions ?

        For Each Version As String In dataTablesList
            srv.sqlQuery("DELETE FROM " & GlobalVariables.database & "." & Version & _
                         " WHERE " & DATA_ENTITY_ID_VARIABLE & "='" & entity_id & "'")
        Next

    End Sub

    Protected Friend Function ReplaceEntitiesCategoryValue(ByRef category_id As String, _
                                                       ByRef origin_value As String) As Boolean

        ' Implement on server before delete 
        MsgBox("implement category replacement on server")

        'Dim new_value = category_id & NON_ATTRIBUTED_SUFIX
        'Return srv.sqlQuery("UPDATE " & GlobalVariables.database & "." + ENTITIES_TABLE & _
        '                    " SET " & category_id & "='" & new_value & "'" & _
        '                    " WHERE " & category_id & "='" & origin_value & "'")

    End Function

    Protected Friend Function SetNewCategoryDefaultValue(ByRef category_id As String) As Boolean

        MsgBox("implement category replacement on server")
        'Dim new_value = category_id & NON_ATTRIBUTED_SUFIX
        'Return srv.sqlQuery("UPDATE " & GlobalVariables.database & "." + ENTITIES_TABLE & _
        '                    " SET " & category_id & "='" & new_value & "'")

    End Function


#End Region








End Class
