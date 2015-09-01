'' GDFSUEZASExport.vb
'' 
'' CRUD model for table gdf_as_export
'' 
'' To do:
''     
''
''
'' Author: Julien Monnereau
'' Last modified: 25/01/2015


'Imports System.Windows.Forms
'Imports System.Collections.Generic
'Imports System.Collections
'Imports ADODB



'Friend Class AlternativeScenarioExport


'#Region "Instance Variables"

'    ' Objects
'    Private SRV As ModelServer
'    Friend RST As Recordset

'    ' Constants
'    Friend object_is_alive As Boolean

'#End Region


'#Region "Initialize"

'    Protected Friend Sub New()

'        SRV = New ModelServer
'        Dim q_result = SRV.OpenRst(GlobalVariables.database + "." + GDF_AS_EXPORTS_TABLE, ModelServer.DYNAMIC_CURSOR)
'        RST = SRV.rst
'        object_is_alive = q_result

'    End Sub

'#End Region


'#Region "CRUD Interface"

'    Protected Friend Sub CreateExport(ByRef newExportAttributes As Hashtable)

'        Dim fieldsArray(newExportAttributes.Count - 1) As Object
'        Dim valuesArray(newExportAttributes.Count - 1) As Object
'        newExportAttributes.Keys.CopyTo(fieldsArray, 0)
'        newExportAttributes.Values.CopyTo(valuesArray, 0)
'        RST.AddNew(fieldsArray, valuesArray)

'    End Sub

'    Protected Friend Function ReadExport(ByVal item As String, _
'                                         ByVal sensitivity_id As String) As Object

'        RST.Filter = GDF_AS_EXPORTS_ITEM_VAR & "='" & item & "' AND " & _
'                     GDF_AS_EXPORTS_SENSI_VAR & "='" & sensitivity_id & "'"
'        If RST.EOF Then Return Nothing
'        Return RST.Fields(GDF_AS_EXPORTS_ACCOUNT_ID_VAR).Value

'    End Function

'    Protected Friend Sub UpdateExport(ByVal item As String, _
'                                      ByVal sensitivity_id As String, _
'                                      ByRef value As String)

'        RST.Filter = GDF_AS_EXPORTS_ITEM_VAR & "='" & item & "' AND" & _
'                     GDF_AS_EXPORTS_SENSI_VAR & "='" & sensitivity_id & "'"
'        If RST.EOF = False AndAlso RST.BOF = False Then
'            If RST.Fields(GDF_AS_EXPORTS_ACCOUNT_ID_VAR).Value <> value Then
'                RST.Fields(GDF_AS_EXPORTS_ACCOUNT_ID_VAR).Value = value
'                RST.Update()
'            End If
'        End If

'    End Sub

'    Protected Friend Sub DeleteExport(ByVal item As String, _
'                                      ByVal sensitivity_id As String)

'        RST.Filter = GDF_AS_EXPORTS_ITEM_VAR & "='" & item & "' AND" & _
'                    GDF_AS_EXPORTS_SENSI_VAR & "='" & sensitivity_id & "'"
'        If RST.EOF = False Then
'            RST.Delete()
'            RST.Update()
'        End If

'    End Sub

'#End Region

'#Region "Utilities"


'    Protected Overrides Sub finalize()

'        RST.Close()
'        MyBase.Finalize()

'    End Sub

'#End Region


'End Class
