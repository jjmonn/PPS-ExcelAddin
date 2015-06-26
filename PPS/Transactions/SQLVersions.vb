' VersioningQueriesSupport.vb
'
' Manages the SQL queries related to versions tables
' 
'  to do:
'   - lock/ unlock process
'   
'
' known bugs:
'
'
' Last modified: 16/03/2015
' Author: Julien Monnereau


Imports System.Collections.Generic


Friend Class SQLVersions


#Region "Instance Variables"

    ' Variables
    Private srv As New ModelServer

#End Region


    Protected Friend Function CreateNewVersionTable(ByRef version_id As String) As Boolean

        If srv.sqlQuery("CREATE TABLE " & GlobalVariables.database & "." & version_id & " (" & _
                      DATA_ACCOUNT_ID_VARIABLE & " CHAR(" & ACCOUNTS_TOKEN_SIZE & ") NOT NULL," & _
                      DATA_ENTITY_ID_VARIABLE & " CHAR(" & ENTITIES_TOKEN_SIZE & ") NOT NULL," & _
                      DATA_PERIOD_VARIABLE & " INT NOT NULL," & _
                      DATA_VALUE_VARIABLE & " DOUBLE NOT NULL, " & _
                      DATA_ADJUSTMENT_ID_VARIABLE & " CHAR(" & ANALYSIS_AXIS_TOKEN_SIZE & ") NOT NULL DEFAULT ''," & _
                      DATA_CLIENT_ID_VARIABLE & " CHAR(" & ANALYSIS_AXIS_TOKEN_SIZE & ") NOT NULL DEFAULT ''," & _
                      DATA_PRODUCT_ID_VARIABLE & " CHAR(" & ANALYSIS_AXIS_TOKEN_SIZE & ") NOT NULL DEFAULT ''," & _
                      " PRIMARY KEY (" & DATA_ENTITY_ID_VARIABLE & "," & _
                                         DATA_ACCOUNT_ID_VARIABLE & "," & _
                                         DATA_PERIOD_VARIABLE & "," & _
                                         DATA_ADJUSTMENT_ID_VARIABLE & "," & _
                                         DATA_CLIENT_ID_VARIABLE & "," & _
                                         DATA_PRODUCT_ID_VARIABLE _
                                         & "))") Then

            Return CreateTrigger(version_id)
        Else
            Return False
        End If

    End Function

    Protected Friend Function CreateNewVersionTableFrom(ByRef new_version_id As String, ByRef old_version_id As String) As Boolean

        If srv.sqlQuery("CREATE TABLE " + GlobalVariables.database + "." + new_version_id + " AS (SELECT * FROM " + GlobalVariables.database + "." + old_version_id + ")") Then

            If srv.sqlQuery("ALTER TABLE " & GlobalVariables.database + "." + new_version_id & _
                            " ADD CONSTRAINT pk_ID PRIMARY KEY (" & DATA_ENTITY_ID_VARIABLE & "," & _
                                                                    DATA_ACCOUNT_ID_VARIABLE & "," & _
                                                                    DATA_PERIOD_VARIABLE & _
                                                                    DATA_ADJUSTMENT_ID_VARIABLE & "," & _
                                                                    DATA_CLIENT_ID_VARIABLE & "," & _
                                                                    DATA_PRODUCT_ID_VARIABLE _
                                                                    & "))") Then

                Return CreateTrigger(new_version_id)
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    Protected Friend Function DeleteVersionQueries(ByRef version_id As String) As Boolean

        Return srv.sqlQuery("DROP TABLE " + GlobalVariables.database + "." + version_id)

    End Function

    Friend Sub LockDataTable(ByRef dataTableName As String)


    End Sub

    Friend Sub UnlockDataTable(ByRef dataTableName As String)


    End Sub

    Private Function CreateTrigger(ByRef version_id As String) As Boolean

        Dim str As String = "CREATE DEFINER=`" & GlobalVariables.Current_User_ID & "`@`%` TRIGGER `" & GlobalVariables.database & "`.`" & version_id & "_after_insert` AFTER INSERT ON `" & _
                            GlobalVariables.database & "`.`" & version_id & "`" & _
                            "FOR EACH ROW BEGIN " & _
                            "INSERT INTO `" & GlobalVariables.database & "`.`" & LOG_TABLE_NAME & "`(" & _
                                LOG_USER_ID_VARIABLE & "," & _
                                LOG_VERSION_ID_VARIABLE & "," & _
                                DATA_ACCOUNT_ID_VARIABLE & "," & _
                                DATA_ENTITY_ID_VARIABLE & "," & _
                                DATA_PERIOD_VARIABLE & "," & _
                                DATA_VALUE_VARIABLE & "," & _
                                DATA_ADJUSTMENT_ID_VARIABLE & "," & _
                                DATA_CLIENT_ID_VARIABLE & "," & _
                                DATA_PRODUCT_ID_VARIABLE _
                            & ") " & _
                            "VALUES(SUBSTRING_INDEX(USER(),'@',1),'" & _
                            version_id & "'" & _
                                ", NEW." & DATA_ACCOUNT_ID_VARIABLE & _
                                ", NEW." & DATA_ENTITY_ID_VARIABLE & _
                                ", NEW." & DATA_PERIOD_VARIABLE & _
                                ", NEW." & DATA_VALUE_VARIABLE & _
                                ", NEW." & DATA_ADJUSTMENT_ID_VARIABLE & _
                                ", NEW." & DATA_CLIENT_ID_VARIABLE & _
                                ", NEW." & DATA_PRODUCT_ID_VARIABLE _
                            & ");" & _
                            "END;"

        Return srv.sqlQuery(str)

    End Function

    '' Accounts Foreign Key
    'Dim strSql As String = " ALTER TABLE " + versionTableName _
    '                       + "ADD CONSTRAINT " + DATA_ACCOUNT_ID_FK_VARIABLE _
    '                       + " FOREIGN KEY(" + DATA_ACCOUNT_ID_VARIABLE + ")" _
    '                       + " REFERENCES " + GlobalVariables.database + "." + ACCOUNTS_TABLE + "(" + ACCOUNT_ID_VARIABLE + ")"

    'srv.sqlQuery(strSql)


    '' Entities Foreign Key
    'strSql = " ALTER TABLE " + versionTableName _
    '         + "ADD CONSTRAINT " + DATA_ENTITY_ID_FK_VARIABLE _
    '         + " FOREIGN KEY(" + DATA_ENTITY_ID_VARIABLE + ")" _
    '         + " REFERENCES " + ASSETS_TABLE + "(" + ENTITIES_ID_VARIABLE + ")"

    'srv.sqlQuery(strSql)


End Class
