' ConfigPrivilegesMapping.vb
' 
' Provides Lists and dictionaries for the ACF_Access Tables:
'                                                           - Config_Tables_Read
'                                                           - Config_Tables_Write
'
'   To do:
'       - only CRUD may be necessary !!
'
'
'   Know bugs:
'
'
' Last modified: 02/12/2014
' Author: Julien Monnereau


Imports System.Collections.Generic


Public Class ConfigPrivilegesMapping

 

#Region "List Provider"


    ' Return a filetered list 
    ' param 1:Name of the table (Write or Read)
    ' Param 2: Credential type filter (User, DBM,...)
    Public Shared Function GetFilteredTablesNamesList(ByRef tableName As String, _
                                                      ByRef credentialTypeFilter As String) As List(Of String)

        Dim srv As New ModelServer
        Dim tmpList As New List(Of String)
        srv.openRst(ACCESS_DATABASE + "." + tableName, ModelServer.FWD_CURSOR)
        srv.rst.Filter = CONFIG_ACCESS_CREDENTIAL_TYPE_VARIABLE + " ='" + credentialTypeFilter + "'"
        If srv.rst.EOF = False And srv.rst.BOF = False Then

            Do While srv.rst.EOF = False
                tmpList.Add(srv.rst.Fields(CONFIG_ACCESS_TABLE_NAME_VARIABLE).Value)
                srv.rst.MoveNext()
            Loop

        End If

        srv.rst.Close()
        Return tmpList

    End Function


#End Region




End Class
