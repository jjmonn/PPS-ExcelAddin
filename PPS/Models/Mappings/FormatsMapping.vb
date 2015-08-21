'' FormatsMappingTransactions.vb
''
'' Module managing the transactions with the formats table in DB 
''
''
'' To do:
''
'' Known bugs:
''
''
''Last modified: 02/12/2014
'' Author: Julien Monnereau


'Imports System.Collections.Generic
'Imports System.Collections

'Friend Class FormatsMapping

'    ' Return a dictionary of formats
'    Friend Shared Function GetFormatTable(ByRef formatDestinationCode As String) As Dictionary(Of String, Dictionary(Of String, Object))

'        Dim formatList As New Dictionary(Of String, Dictionary(Of String, Object))
'        Dim srv As New ModelServer
'        srv.openRst(GlobalVariables.database + "." + FORMATS_TABLE_NAME, ModelServer.FWD_CURSOR)
'        srv.rst.Filter = FORMAT_DESTINATION_VARIABLE + "='" + formatDestinationCode + "'"

'        If srv.rst.EOF = False And srv.rst.BOF = False Then

'            Do While srv.rst.EOF = False
'                Dim tempDict As New Dictionary(Of String, Object)

'                tempDict.Add(FORMAT_TEXT_COLOR_VARIABLE, srv.rst.Fields(FORMAT_TEXT_COLOR_VARIABLE).Value)
'                tempDict.Add(FORMAT_BOLD_VARIABLE, srv.rst.Fields(FORMAT_BOLD_VARIABLE).Value)
'                tempDict.Add(FORMAT_ITALIC_VARIABLE, srv.rst.Fields(FORMAT_ITALIC_VARIABLE).Value)
'                tempDict.Add(FORMAT_BORDER_VARIABLE, srv.rst.Fields(FORMAT_BORDER_VARIABLE).Value)
'                tempDict.Add(FORMAT_BCKGD_VARIABLE, srv.rst.Fields(FORMAT_BCKGD_VARIABLE).Value)
'                tempDict.Add(FORMAT_NAME_VARIABLE, srv.rst.Fields(FORMAT_NAME_VARIABLE).Value)
'                tempDict.Add(FORMAT_ICON_VARIABLE, srv.rst.Fields(FORMAT_ICON_VARIABLE).Value)
'                tempDict.Add(FORMAT_INDENT_VARIABLE, srv.rst.Fields(FORMAT_INDENT_VARIABLE).Value)
'                tempDict.Add(FORMAT_UP_BORDER_VARIABLE, srv.rst.Fields(FORMAT_UP_BORDER_VARIABLE).Value)
'                tempDict.Add(FORMAT_BOTTOM_BORDER_VARIABLE, srv.rst.Fields(FORMAT_BOTTOM_BORDER_VARIABLE).Value)

'                formatList.Add(srv.rst.Fields(FORMAT_CODE_VARIABLE).Value, tempDict)
'                srv.rst.MoveNext()
'            Loop
'        End If

'        srv.rst.Close()
'        Return formatList

'    End Function

'    ' Returns the format code from name
'    Protected Friend Shared Function GetFormatsDictionary(ByRef Key As String, _
'                                                          ByRef Value As String, _
'                                                          Optional ByRef destination As String = REPORT_FORMAT_CODE) As Hashtable

'        Dim tmpHT As New Hashtable
'        Dim srv As New ModelServer
'        srv.OpenRst(GlobalVariables.database + "." + FORMATS_TABLE_NAME, ModelServer.FWD_CURSOR)
'        srv.rst.Filter = FORMAT_DESTINATION_VARIABLE + "='" + destination + "'"
'        If srv.rst.EOF = False And srv.rst.BOF = False Then

'            srv.rst.MoveFirst()
'            Do While srv.rst.EOF = False
'                tmpHT.Add(srv.rst.Fields(Key).Value, srv.rst.Fields(Value).Value)
'                srv.rst.MoveNext()
'            Loop
'        End If

'        srv.rst.Close()
'        Return tmpHT

'    End Function



'End Class
