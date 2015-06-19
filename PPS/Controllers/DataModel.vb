' cDataModel.vb
'
' Manages the identification of Entities or accounts in Data Tables
'
' To do: 
'
'
'
' Known bugs:
'
' Author: Julien Monnereau
' Last modified: 12/10/2014


Imports System.Collections.Generic
Imports System.Collections


Friend Class DataModel


#Region "Instance Variables"

    ' Objects
    Private srv As New ModelServer

    ' Variables
    Private dataVersionsKeyNameDictionary As Hashtable


#End Region


#Region "Initialize"

    Public Sub New()

        dataVersionsKeyNameDictionary = VersionsMapping.GetVersionsHashTable(VERSIONS_CODE_VARIABLE, VERSIONS_NAME_VARIABLE)

    End Sub

#End Region


#Region "Data Tables Items identify and Deletion"

    Friend Function checkForItemInDataTables(ByRef itemKey As String, ByRef itemVariable As String) As List(Of String)

        Dim positiveVersions As New List(Of String)
        For Each dataTable In dataVersionsKeyNameDictionary.Keys
            srv.openRstSQL("SELECT 1 FROM " + DATA_DATABASE + "." + dataTable + _
                           " WHERE " + itemVariable + "='" + itemKey + "'", ModelServer.FWD_CURSOR)
            If srv.rst.EOF = False Then positiveVersions.Add(dataVersionsKeyNameDictionary(dataTable))
            srv.rst.Close()
        Next
        Return positiveVersions

    End Function

    Friend Function deleteRowsWithItemKeys(ByRef itemKey As String, ByRef itemVariable As String) As Boolean

        Dim errorList As New List(Of String)
        For Each dataTable In dataVersionsKeyNameDictionary.Keys
            Try
                srv.sqlQuery("DELETE FROM " + DATA_DATABASE + "." + dataTable + _
                             " WHERE " + itemVariable + "='" + itemKey + "'")
            Catch ex As Exception
                errorList.Add(dataTable)
                MsgBox("An erorr occurred in the process of account's data deletion for version " + Chr(13) + _
                       dataVersionsKeyNameDictionary(dataTable) + Chr(13) + Chr(13) + _
                       ex.Message)
            End Try
        Next
        If errorList.Count > 0 Then Return False
        Return True

    End Function


#End Region



End Class
