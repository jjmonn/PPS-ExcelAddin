'Report.vb
'
' gdfsuez_as_reports table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 08/02/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class Report


#Region "Instance Variables"

    ' Objects
    Private SRV As New ModelServer
    Private RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef table_name As String)

        Dim i As Int32 = 0
        Dim q_result = SRV.OpenRst(GlobalVariables.database & "." & table_name, ModelServer.DYNAMIC_CURSOR)
        SRV.rst.Sort = ITEMS_POSITIONS
        RST = SRV.rst
        object_is_alive = q_result

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateReport(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadReport(ByRef report_id As String, ByRef field As String) As Object

        RST.Filter = id_variable + "='" + report_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function GetSerieHT(ByRef controlchart_id As String) As Hashtable

        Dim ht As New Hashtable
        RST.Filter = CONTROL_CHART_ID_VARIABLE + "='" + controlchart_id + "'"
        If RST.EOF Then Return Nothing
        ht.Add(parent_id_variable, SRV.rst.Fields(parent_id_variable).Value)
        ht.Add(name_variable, SRV.rst.Fields(name_variable).Value)
        ht.Add(account_id_variable, SRV.rst.Fields(account_id_variable).Value)
        'ht.Add(REPORTS_TYPE_VAR, SRV.rst.Fields(REPORTS_TYPE_VAR).Value)
        'ht.Add(REPORTS_COLOR_VAR, SRV.rst.Fields(REPORTS_COLOR_VAR).Value)
        'ht.Add(REPORTS_PALETTE_VAR, SRV.rst.Fields(REPORTS_PALETTE_VAR).Value)
        'ht.Add(REPORTS_AXIS1_NAME_VAR, SRV.rst.Fields(REPORTS_AXIS1_NAME_VAR).Value)
        'ht.Add(REPORTS_AXIS2_NAME_VAR, SRV.rst.Fields(REPORTS_AXIS2_NAME_VAR).Value)
        'ht.Add(REPORTS_SERIE_AXIS_VAR, SRV.rst.Fields(REPORTS_SERIE_AXIS_VAR).Value)
        'ht.Add(REPORTS_SERIE_UNIT_VAR, SRV.rst.Fields(REPORTS_SERIE_UNIT_VAR).Value)
        'ht.Add(REPORTS_DISPLAY_VALUES_VAR, SRV.rst.Fields(REPORTS_DISPLAY_VALUES_VAR).Value)
        'ht.Add(REPORTS_SERIE_WIDTH_VAR, SRV.rst.Fields(REPORTS_SERIE_WIDTH_VAR).Value)
        Return ht

    End Function

    Protected Friend Sub UpdateReport(ByRef report_id As String, _
                                      ByRef field As String, _
                                      ByVal value As Object)

        RST.Filter = id_variable + "='" + report_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If IsDBNull(RST.Fields(field).Value) Then
                If Not IsNumeric(value) AndAlso value <> "" Then
                    RST.Fields(field).Value = value
                    RST.Update()
                ElseIf IsNumeric(value) Then
                    RST.Fields(field).Value = value
                    RST.Update()
                End If
            ElseIf RST.Fields(field).Value <> value Then
                If Not IsNumeric(value) AndAlso value = "" Then
                    RST.Fields(field).Value = DBNull.Value
                Else
                    RST.Fields(field).Value = value
                End If
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteReport(ByRef report_id As String)

        RST.Filter = id_variable + "='" + report_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub LoadReportsTV(ByRef TV As TreeView)

        'To be reimplemented -> new crud priority low 

    End Sub

    Protected Friend Shared Function GetReportsSettingsDictionary(Optional ByRef srv As ModelServer = Nothing) As Dictionary(Of String, Hashtable)

        ' To be reimplemented -> priotiy low


        'If SRV Is Nothing Then SRV = New ModelServer
        'Dim reports_dict As New Dictionary(Of String, Hashtable)
        'SRV.OpenRst(GlobalVariables.database + "." + GDF_AS_REPORTS_TABLE, ModelServer.FWD_CURSOR)

        'If SRV.rst.EOF = False And SRV.rst.BOF = False Then
        '    SRV.rst.MoveFirst()
        '    Do While SRV.rst.EOF = False

        '        Dim ht As New Hashtable
        '        ht.Add(parent_id_variable, SRV.rst.Fields(parent_id_variable).Value)
        '        ht.Add(name_variable, SRV.rst.Fields(name_variable).Value)
        '        ht.Add(account_id_variable, SRV.rst.Fields(account_id_variable).Value)
        '        ht.Add(REPORTS_TYPE_VAR, SRV.rst.Fields(REPORTS_TYPE_VAR).Value)
        '        ht.Add(REPORTS_COLOR_VAR, SRV.rst.Fields(REPORTS_COLOR_VAR).Value)
        '        ht.Add(REPORTS_PALETTE_VAR, SRV.rst.Fields(REPORTS_PALETTE_VAR).Value)
        '        ht.Add(REPORTS_AXIS1_NAME_VAR, srv.rst.Fields(REPORTS_AXIS1_NAME_VAR).Value)
        '        ht.Add(REPORTS_AXIS2_NAME_VAR, srv.rst.Fields(REPORTS_AXIS2_NAME_VAR).Value)
        '        ht.Add(REPORTS_SERIE_AXIS_VAR, SRV.rst.Fields(REPORTS_SERIE_AXIS_VAR).Value)
        '        ht.Add(REPORTS_SERIE_UNIT_VAR, SRV.rst.Fields(REPORTS_SERIE_UNIT_VAR).Value)
        '        ht.Add(REPORTS_DISPLAY_VALUES_VAR, SRV.rst.Fields(REPORTS_DISPLAY_VALUES_VAR).Value)
        '        ht.Add(REPORTS_SERIE_WIDTH_VAR, SRV.rst.Fields(REPORTS_SERIE_WIDTH_VAR).Value)

        '        reports_dict.Add(SRV.rst.Fields(id_variable).Value, ht)
        '        SRV.rst.MoveNext()
        '    Loop
        'End If
        'SRV.rst.Close()
        'SRV = Nothing
        'Return reports_dict

    End Function

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region




End Class
