''
''
''
''
'' Author: Julien Monnereau
'' Last modified: 05/02/2015


'Imports System.Collections.Generic
'Imports System.Collections


'Friend Class pps_colorsMapping


'    Friend Shared Function GetColorsList() As List(Of Hashtable)

'        Dim tmpList As New List(Of Hashtable)
'        Dim srv As New ModelServer
'        srv.OpenRst(GlobalVariables.database + "." + PPS_COLORS_TABLE, ModelServer.FWD_CURSOR)
'        If srv.rst.EOF = False And srv.rst.BOF = False Then
'            srv.rst.MoveFirst()
'            Do While srv.rst.EOF = False
'                Dim ht As New Hashtable
'                ht.Add(PPS_COLORS_RED_VAR, srv.rst.Fields(PPS_COLORS_RED_VAR).Value)
'                ht.Add(PPS_COLORS_GREEN_VAR, srv.rst.Fields(PPS_COLORS_GREEN_VAR).Value)
'                ht.Add(PPS_COLORS_BLUE_VAR, srv.rst.Fields(PPS_COLORS_BLUE_VAR).Value)
'                tmpList.Add(ht)
'                srv.rst.MoveNext()
'            Loop
'        End If
'        srv.rst.Close()
'        Return tmpList

'    End Function


'End Class
