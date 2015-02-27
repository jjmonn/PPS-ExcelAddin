' PrivilegesManagement.vb
' 
' Create users
' Grants and revokes privileges for the users 
'
'
' To do:
'       - test connection and usage with user created
'       - test usage and create other users with DBM credentials    
'       - RETURN success boolean for queries ?
'
' Known bugs:
'
'
' Last modified: 05/01/2015
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Windows.Forms


Friend Class SQLPrivileges


#Region "Instance Variables"

    ' Objects
    Private srv As New ModelServer

    ' Variables
    Private readTablesDictionary As New Dictionary(Of String, List(Of String))
    Private writeTablesDictionary As New Dictionary(Of String, List(Of String))
    Private dataTablesList As List(Of String)

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Dim credentialsList As List(Of String) = CredentialsTypesMapping.GetCredentialsTypesList(CREDENTIALS_DESCRIPTION_VARIABLE)
        For Each credentialType As String In credentialsList

            Dim tmpReadList As List(Of String) = ConfigPrivilegesMapping.GetFilteredTablesNamesList(CONFIG_READ_TABLES, credentialType)
            Dim tmpWriteList As List(Of String) = ConfigPrivilegesMapping.GetFilteredTablesNamesList(CONFIG_WRITE_TABLES, credentialType)
            readTablesDictionary.Add(credentialType, tmpReadList)
            writeTablesDictionary.Add(credentialType, tmpWriteList)

        Next
        dataTablesList = VersionsMapping.GetVersionsList(VERSIONS_CODE_VARIABLE)

    End Sub

#End Region


#Region "GRANT PRIVILEGES"

    ' Grant privileges
    Protected Friend Sub GrantPrivilegesToUser(ByVal user_id As String, _
                                               ByRef entity_id As String, _
                                               ByRef credentialType As String, _
                                               ByRef credential_level As Int32)

        Dim userID = "'" + user_id + "'@'%'"

        ' CONFIG DATABASE Read
        For Each table As String In readTablesDictionary(credentialType)
            ExecuteUserQuery("GRANT SELECT ON " + CONFIG_DATABASE + "." + table + " TO " + userID, credentialType)
        Next

        ' Grant Write privileges in CONFIG DATABASE
        For Each table As String In writeTablesDictionary(credentialType)
            ExecuteUserQuery("GRANT UPDATE, INSERT, DELETE ON " + CONFIG_DATABASE + "." + table + " TO " + userID, credentialType)
        Next

        ' Write on DATA tables (DATA DATABASE) 
        For Each table As String In dataTablesList
            ExecuteUserQuery("GRANT DELETE, UPDATE, INSERT ON " + DATA_DATABASE + "." + table + " TO " + userID, credentialType)
        Next

        ' ACCESS Tables
        ExecuteUserQuery("GRANT SELECT ON " + ACCESS_DATABASE + "." + CONFIG_READ_TABLES + " TO " + userID, credentialType)

        ' Credential Levels
        ' ExecuteUserQuery("GRANT SELECT (" & CREDENTIALS_ID_VARIABLE & ") ON " + CONFIG_DATABASE + "." + CREDENTIALS_ID_TABLE + " TO " + userID, credentialType)

        ' Read on Authorized Views
        GrantViewsAuthorizedPrivileges(user_id, entity_id, credentialType)

        ' Read/ Write on datalog
        ExecuteUserQuery("GRANT SELECT, INSERT, UPDATE ON " + DATA_DATABASE + "." + LOG_TABLE_NAME + " TO " + userID, credentialType)

        If credentialType = DATA_BASE_MANAGER_CREDENTIAL_TYPE Then GrantDataBaseManagerPrivileges(userID, credential_level)

    End Sub

    ' Grant DataBase Manager privileges
    Private Sub GrantDataBaseManagerPrivileges(ByRef userID As String, ByRef credential_level As Int32)

        ' Access.ConfigTableWrite
        srv.sqlQuery("GRANT SELECT ON " + ACCESS_DATABASE + "." + CONFIG_WRITE_TABLES + " TO " + userID + " WITH GRANT OPTION")

        ' Data Database
        srv.sqlQuery("GRANT CREATE, SELECT, INSERT, UPDATE, ALTER, DELETE, CREATE VIEW, LOCK TABLES, TRIGGER ON " + DATA_DATABASE + ".*" + " TO " + userID + " WITH GRANT OPTION")
        'For Each table As String In dataTablesList
        '    srv.sqlQuery("GRANT CREATE VIEW, SELECT, INSERT, UPDATE, DELETE, ALTER ON " + DATA_DATABASE + "." + table + " TO " + userID + " WITH GRANT OPTION")
        'Next

        ' Views Database
        srv.sqlQuery("GRANT EXECUTE, CREATE VIEW, SHOW VIEW, CREATE, SELECT, DROP ON " + VIEWS_DATABASE + ".*" + " TO " + userID + " WITH GRANT OPTION")

        ' Entities Table
        srv.sqlQuery("GRANT CREATE VIEW, SELECT, INSERT, UPDATE, DELETE, ALTER ON " + LEGAL_ENTITIES_DATABASE + "." + ENTITIES_TABLE + " TO " + userID + " WITH GRANT OPTION")

        srv.sqlQuery("GRANT CREATE USER ON *.* TO " + userID + " WITH GRANT OPTION")

    End Sub

    Private Sub GrantPrivilegesOnDataTable(ByRef version_id As String, _
                                            ByVal user_id As String, _
                                            ByRef credentialType As String)

        user_id = "'" + user_id + "'@'%'"
        ExecuteUserQuery("GRANT DELETE, UPDATE, INSERT ON " + DATA_DATABASE + "." + version_id + " TO " + user_id, credentialType)
        'If credentialType = DATA_BASE_MANAGER_CREDENTIAL_TYPE Then _
        'srv.sqlQuery("GRANT CREATE VIEW, SELECT, INSERT, UPDATE, DELETE, ALTER ON " + DATA_DATABASE + "." + version_id + " TO " + user_id + " WITH GRANT OPTION")


    End Sub


    ' Query execution for users (Adds With grant option if Data Base Manager)
    Private Sub ExecuteUserQuery(ByRef sqlQuery As String, ByRef credentialType As String)

        If credentialType = DATA_BASE_MANAGER_CREDENTIAL_TYPE Then sqlQuery = sqlQuery + " WITH GRANT OPTION"
        srv.sqlQuery(sqlQuery)

    End Sub


#End Region


#Region "Views Privileges"

    ' Loop also for all lower credentials
    Protected Friend Sub GrantViewsAuthorizedPrivileges(ByVal user_id As String, _
                                                        ByRef entity_id As String, _
                                                        ByRef credential_type As String)

        Dim nodes() As TreeNode
        Dim credentialsTV As New TreeView
        Entity.LoadEntitiesCredentialTree(credentialsTV)
        nodes = credentialsTV.Nodes.Find(entity_id, True)
        If nodes.Length > 0 Then
            Dim entity_node = nodes(0)
            Dim credentials_list = TreeViewsUtilities.GetNodesTextsList(entity_node)
            credentials_list = TreeViewsUtilities.GetUniqueList(credentials_list)

            user_id = "'" + user_id + "'@'%'"
            For Each credential_level In credentials_list
                For Each table As String In dataTablesList
                    Dim view_name = table & credential_level
                    ExecuteUserQuery("GRANT SELECT, SHOW VIEW ON " & VIEWS_DATABASE & "." & view_name & " TO " & user_id, credential_type)
                Next
                ExecuteUserQuery("GRANT SELECT, SHOW VIEW ON " & VIEWS_DATABASE & "." & ENTITIES_TABLE & credential_level & " TO " & user_id, credential_type)
            Next
        End If

    End Sub

    'Private Sub GrantDBManagerRightsOnViews(ByRef user_id As String, _
    '                                        ByRef credentials_list As List(Of String))

    '    ' Grant Drop on Views
    '    For Each credential_level In credentials_list
    '        For Each table As String In dataTablesList
    '            Dim view_name = table & credential_level
    '            srv.sqlQuery("GRANT CREATE VIEW, CREATE, DROP ON " & VIEWS_DATABASE & "." & view_name & " TO " & user_id & " WITH GRANT OPTION")
    '        Next
    '        srv.sqlQuery("GRANT CREATE VIEW, CREATE, DROP ON " & _
    '                     VIEWS_DATABASE & "." & ENTITIES_TABLE & credential_level & " TO " & user_id & " WITH GRANT OPTION")
    '    Next

    'End Sub

#End Region


#Region "Revoke Privileges"

    Protected Friend Sub RevokeAllPrivileges(ByVal user_id)

        user_id = "'" + user_id + "'@'%'"
        srv.sqlQuery("REVOKE ALL PRIVILEGES, GRANT OPTION FROM " & user_id)

    End Sub

    ' not currently used 
    Protected Friend Sub RevokePrivilegesOnViews(ByVal user_id As String, _
                                                 ByRef credential_level As Int32)

        user_id = "'" + user_id + "'@'%'"
        For Each table As String In dataTablesList
            Dim view_name = table & credential_level
            srv.sqlQuery("REVOKE SELECT ON " & VIEWS_DATABASE & "." & view_name & " FROM " & user_id)
        Next
        srv.sqlQuery("REVOKE SELECT ON " & VIEWS_DATABASE & "." & ENTITIES_TABLE & credential_level & " FROM " & user_id)

    End Sub


#End Region


#Region "All Users Privileges"

    Protected Friend Sub GrantAllUsersPrivilegesOnNewDataTable(ByRef version_id As String)

        Dim users_dict = UsersMapping.GetUsersTable()
        For Each user_id In users_dict.Keys
            GrantPrivilegesOnDataTable(version_id, _
                                       user_id, _
                                       users_dict(user_id)(USERS_CREDENTIAL_TYPE_VARIABLE))
        Next

    End Sub

    Protected Friend Sub GrantAllUsersPrivilegesOnAuthorizedDataViews()

        Dim users_dict = UsersMapping.GetUsersTable()
        dataTablesList = VersionsMapping.GetVersionsList(VERSIONS_CODE_VARIABLE)
        For Each user_id In users_dict.Keys
            For Each version_id In dataTablesList
                GrantViewsAuthorizedPrivileges(user_id, _
                                               users_dict(user_id)(USERS_ENTITY_ID_VARIABLE), _
                                               users_dict(user_id)(USERS_CREDENTIAL_TYPE_VARIABLE))
            Next
        Next

    End Sub

 
#End Region


#Region "CREATE/ DROP USER"

    Friend Sub CreateUser(ByVal userID As String, ByVal userPWd As String)

        userPWd = userPWd + SNOW_KEY
        srv.sqlQuery("CREATE USER '" + userID + "'@'%' IDENTIFIED BY '" + userPWd + "'")

    End Sub

    Friend Sub DropUser(ByRef userID As String)

        Dim srv As New ModelServer
        srv.sqlQuery("DROP USER '" + userID + "'")

    End Sub

    Friend Sub ChangePassword(ByRef userID As String, ByVal userPWd As String)

        userPWd = userPWd + SNOW_KEY
        srv.sqlQuery("SET PASSWORD FOR '" + userID + "'@'%' = PASSWORD('" + userPWd + "')")

    End Sub

    Friend Sub ChangeCurrentUserPwd(ByVal pwd As String)

        pwd = pwd + SNOW_KEY
        srv.sqlQuery("SET PASSWORD = PASSWORD('" + pwd + "')")

    End Sub

#End Region



End Class
