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
        Dim q_result = SRV.OpenRst(CONFIG_DATABASE & "." & table_name, ModelServer.DYNAMIC_CURSOR)
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

        RST.Filter = REPORTS_ID_VAR + "='" + report_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function GetSerieHT(ByRef controlchart_id As String) As Hashtable

        Dim ht As New Hashtable
        RST.Filter = CONTROL_CHART_ID_VARIABLE + "='" + controlchart_id + "'"
        If RST.EOF Then Return Nothing
        ht.Add(REPORTS_PARENT_ID_VAR, SRV.rst.Fields(REPORTS_PARENT_ID_VAR).Value)
        ht.Add(REPORTS_NAME_VAR, SRV.rst.Fields(REPORTS_NAME_VAR).Value)
        ht.Add(REPORTS_ACCOUNT_ID, SRV.rst.Fields(REPORTS_ACCOUNT_ID).Value)
        ht.Add(REPORTS_TYPE_VAR, SRV.rst.Fields(REPORTS_TYPE_VAR).Value)
        ht.Add(REPORTS_COLOR_VAR, SRV.rst.Fields(REPORTS_COLOR_VAR).Value)
        ht.Add(REPORTS_PALETTE_VAR, SRV.rst.Fields(REPORTS_PALETTE_VAR).Value)
        ht.Add(REPORTS_AXIS1_NAME_VAR, SRV.rst.Fields(REPORTS_AXIS1_NAME_VAR).Value)
        ht.Add(REPORTS_AXIS2_NAME_VAR, SRV.rst.Fields(REPORTS_AXIS2_NAME_VAR).Value)
        ht.Add(REPORTS_SERIE_AXIS_VAR, SRV.rst.Fields(REPORTS_SERIE_AXIS_VAR).Value)
        ht.Add(REPORTS_SERIE_UNIT_VAR, SRV.rst.Fields(REPORTS_SERIE_UNIT_VAR).Value)
        ht.Add(REPORTS_DISPLAY_VALUES_VAR, SRV.rst.Fields(REPORTS_DISPLAY_VALUES_VAR).Value)
        ht.Add(REPORTS_SERIE_WIDTH_VAR, SRV.rst.Fields(REPORTS_SERIE_WIDTH_VAR).Value)
        Return ht

    End Function

    Protected Friend Sub UpdateReport(ByRef report_id As String, _
                                      ByRef field As String, _
                                      ByVal value As Object)

        RST.Filter = REPORTS_ID_VAR + "='" + report_id + "'"
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

        RST.Filter = REPORTS_ID_VAR + "='" + report_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub LoadReportsTV(ByRef TV As TreeView)

        Dim srv As New ModelServer
        Dim q_result As Boolean = srv.OpenRst(CONFIG_DATABASE & "." & GDF_AS_REPORTS_TABLE, ModelServer.FWD_CURSOR)
        If q_result = True Then

            Dim currentNode, ParentNode() As TreeNode
            Dim image_index As Int32
            TV.Nodes.Clear()
            srv.rst.Sort = ITEMS_POSITIONS

            If srv.rst.RecordCount > 0 Then
                srv.rst.MoveFirst()
                Do While srv.rst.EOF = False
                    If IsDBNull(srv.rst.Fields(REPORTS_PARENT_ID_VAR).Value) Then
                        If srv.rst.Fields(REPORTS_TYPE_VAR).Value = TABLE_REPORT_TYPE Then image_index = 2 Else image_index = 0
                        currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(REPORTS_ID_VAR).Value), _
                                                   Trim(srv.rst.Fields(REPORTS_NAME_VAR).Value), image_index, image_index)
                    Else
                        ParentNode = TV.Nodes.Find(Trim(srv.rst.Fields(REPORTS_PARENT_ID_VAR).Value), True)
                        If ParentNode(0).ImageIndex = 0 Then image_index = 1 Else image_index = 3
                        currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(REPORTS_ID_VAR).Value), _
                                                                  Trim(srv.rst.Fields(REPORTS_NAME_VAR).Value), image_index, image_index)
                    End If
                    currentNode.EnsureVisible()
                    srv.rst.MoveNext()
                Loop
            End If
            srv.rst.Close()
        End If

    End Sub

    Protected Friend Shared Function GetReportsSettingsDictionary(Optional ByRef srv As ModelServer = Nothing) As Dictionary(Of String, Hashtable)

        If SRV Is Nothing Then SRV = New ModelServer
        Dim reports_dict As New Dictionary(Of String, Hashtable)
        SRV.OpenRst(CONFIG_DATABASE + "." + GDF_AS_REPORTS_TABLE, ModelServer.FWD_CURSOR)

        If SRV.rst.EOF = False And SRV.rst.BOF = False Then
            SRV.rst.MoveFirst()
            Do While SRV.rst.EOF = False

                Dim ht As New Hashtable
                ht.Add(REPORTS_PARENT_ID_VAR, SRV.rst.Fields(REPORTS_PARENT_ID_VAR).Value)
                ht.Add(REPORTS_NAME_VAR, SRV.rst.Fields(REPORTS_NAME_VAR).Value)
                ht.Add(REPORTS_ACCOUNT_ID, SRV.rst.Fields(REPORTS_ACCOUNT_ID).Value)
                ht.Add(REPORTS_TYPE_VAR, SRV.rst.Fields(REPORTS_TYPE_VAR).Value)
                ht.Add(REPORTS_COLOR_VAR, SRV.rst.Fields(REPORTS_COLOR_VAR).Value)
                ht.Add(REPORTS_PALETTE_VAR, SRV.rst.Fields(REPORTS_PALETTE_VAR).Value)
                ht.Add(REPORTS_AXIS1_NAME_VAR, srv.rst.Fields(REPORTS_AXIS1_NAME_VAR).Value)
                ht.Add(REPORTS_AXIS2_NAME_VAR, srv.rst.Fields(REPORTS_AXIS2_NAME_VAR).Value)
                ht.Add(REPORTS_SERIE_AXIS_VAR, SRV.rst.Fields(REPORTS_SERIE_AXIS_VAR).Value)
                ht.Add(REPORTS_SERIE_UNIT_VAR, SRV.rst.Fields(REPORTS_SERIE_UNIT_VAR).Value)
                ht.Add(REPORTS_DISPLAY_VALUES_VAR, SRV.rst.Fields(REPORTS_DISPLAY_VALUES_VAR).Value)
                ht.Add(REPORTS_SERIE_WIDTH_VAR, SRV.rst.Fields(REPORTS_SERIE_WIDTH_VAR).Value)

                reports_dict.Add(SRV.rst.Fields(REPORTS_ID_VAR).Value, ht)
                SRV.rst.MoveNext()
            Loop
        End If
        SRV.rst.Close()
        SRV = Nothing
        Return reports_dict

    End Function

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region




End Class
