' SQLLog.vb
'
' SQL queries related to datalog table
'
'
'
' Author: Julien Monnereau
' Last modified: 18/01/2015


Imports System.Collections.Generic
Imports System.Collections


Friend Class SQLLog


    Protected Friend Shared Function GetValueHistory(ByRef entity_id As String, _
                                                     ByRef version_id As String, _
                                                     ByRef account_id As String, _
                                                     ByRef period As Int32) As List(Of Hashtable)

        Dim srv As New ModelServer
        Dim str As String = "SELECT " & LOG_TIME_VARIABLE & "," & _
                                        LOG_USER_ID_VARIABLE & "," & _
                                        LOG_VALUE_VARIABLE & _
                                        LOG_ADJUSTMENT_ID_VARIABLE & _
                            " FROM " & DATA_DATABASE & "." & LOG_TABLE_NAME & _
                            " WHERE " & LOG_ENTITY_ID_VARIABLE & "='" & entity_id & "'" & _
                            " AND " & LOG_VERSION_ID_VARIABLE & "='" & version_id & "'" & _
                            " AND " & LOG_ACCOUNT_ID_VARIABLE & "='" & account_id & "'" & _
                            " AND " & LOG_PERIOD_VARIABLE & "=" & period
                         
        srv.openRstSQL(str, ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False AndAlso srv.rst.BOF = False Then
            Dim result_list As New List(Of Hashtable)
            srv.rst.MoveFirst()
            While srv.rst.EOF = False
                Dim ht As New Hashtable
                ht.Add(LOG_TIME_VARIABLE, srv.rst.Fields(LOG_TIME_VARIABLE).Value)
                ht.Add(LOG_USER_ID_VARIABLE, srv.rst.Fields(LOG_USER_ID_VARIABLE).Value)
                ht.Add(LOG_VALUE_VARIABLE, srv.rst.Fields(LOG_VALUE_VARIABLE).Value)
                ht.Add(LOG_ADJUSTMENT_ID_VARIABLE, srv.rst.Fields(LOG_ADJUSTMENT_ID_VARIABLE).Value)
                result_list.Add(ht)
                srv.rst.MoveNext()
            End While
            srv.CloseRst()
            Return result_list
        Else
            srv.CloseRst()
            Return Nothing
        End If

    End Function



End Class
