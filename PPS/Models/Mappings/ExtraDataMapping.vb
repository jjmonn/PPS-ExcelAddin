' CExtraDataMapping.vb
'
'
' To do:
'       - Desinged for starting period and ending period only so cfar
'       - 
'
'
' Known Bugs
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 19/01/2015


Imports System.Collections


Public Class ExtraDataMapping



#Region "Ivars"


#End Region


    '' Provides EP and SP
    'Friend Shared Function GetStartAndEndingPeriod() As Hashtable

    '    Dim srv As New ModelServer
    '    srv.openRst(EXTRA_DATA_TABLE_ADDRESS, ModelServer.FWD_CURSOR)
    '    Dim variablesArray() As String = {STARTING_PERIOD_KEY, ENDING_PERIOD_KEY}
    '    Dim tmpHT As New Hashtable

    '    For Each variable In variablesArray
    '        srv.rst.Filter = EXTRA_DATA_KEY_VARIABLE + "='" + variable + "'"
    '        If srv.rst.EOF = False AndAlso srv.rst.BOF = False Then
    '            tmpHT.Add(variable, srv.rst.Fields(EXTRA_DATA_VALUE_VARIABLE).Value)
    '        End If
    '    Next
    '    srv.rst.Close()
    '    srv = Nothing
    '    Return tmpHT

    'End Function


    Friend Shared Function GetValue(ByRef key As String) As Object

        Dim srv As New ModelServer
        srv.openRstSQL("SELECT * FROM " & GlobalVariables.database + "." & EXTRA_DATA_TABLE_NAME & " WHERE " & EXTRA_DATA_KEY_VARIABLE & "='" & key & "'", _
                        ModelServer.FWD_CURSOR)
        If srv.rst.EOF = False And srv.rst.BOF = False Then
            Return srv.rst.Fields(EXTRA_DATA_VALUE_VARIABLE).Value
        Else
            Return Nothing
        End If

    End Function




End Class
