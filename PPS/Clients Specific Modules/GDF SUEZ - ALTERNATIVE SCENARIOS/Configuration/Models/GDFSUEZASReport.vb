' GDFSUEZASReport.vb
'
' gdfsuez_as_reports table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 17/01/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class GDFSUEZASReport

#Region "Instance Variables"

    ' Objects
    Private SRV As New ModelServer
    Private RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Dim i As Int32 = 0
        Dim q_result = SRV.OpenRst(CONFIG_DATABASE & "." & GDF_AS_REPORTS_TABLE, ModelServer.DYNAMIC_CURSOR)
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

        RST.Filter = GDF_AS_REPORTS_ID_VAR + "='" + report_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Sub UpdateReport(ByRef report_id As String, _
                                      ByRef field As String, _
                                      ByVal value As Object)

        RST.Filter = GDF_AS_REPORTS_ID_VAR + "='" + report_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If IsDBNull(RST.Fields(field).Value) AndAlso value <> "" Then
                RST.Fields(field).Value = value
                RST.Update()
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

        RST.Filter = GDF_AS_REPORTS_ID_VAR + "='" + report_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub LoadAlternativeScenarioReportsTV(ByRef TV As TreeView)

        Dim srv As New ModelServer
        Dim q_result As Boolean = srv.OpenRst(CONFIG_DATABASE & "." & GDF_AS_REPORTS_TABLE, ModelServer.FWD_CURSOR)
        If q_result = True Then

            Dim currentNode, ParentNode() As TreeNode
            TV.Nodes.Clear()
            srv.rst.Sort = ITEMS_POSITIONS

            If srv.rst.RecordCount > 0 Then
                srv.rst.MoveFirst()

                Do While srv.rst.EOF = False

                    If IsDBNull(srv.rst.Fields(GDF_AS_REPORTS_PARENT_ID_VAR).Value) Then
                        currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(GDF_AS_REPORTS_ID_VAR).Value), _
                                                   Trim(srv.rst.Fields(GDF_AS_REPORTS_NAME_VAR).Value), 0, 0)
                    Else

                        ParentNode = TV.Nodes.Find(Trim(srv.rst.Fields(GDF_AS_REPORTS_PARENT_ID_VAR).Value), True)
                        currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(GDF_AS_REPORTS_ID_VAR).Value), _
                                                                  Trim(srv.rst.Fields(GDF_AS_REPORTS_NAME_VAR).Value), 1, 1)
                    End If
                    currentNode.EnsureVisible()
                    srv.rst.MoveNext()
                Loop
            End If
            srv.rst.Close()
        End If

    End Sub

    Protected Friend Shared Function GetReportsSettingsDictionary() As Dictionary(Of String, Hashtable)

        Dim srv As New ModelServer
        Dim reports_dict As New Dictionary(Of String, Hashtable)
        srv.OpenRst(CONFIG_DATABASE + "." + GDF_AS_REPORTS_TABLE, ModelServer.FWD_CURSOR)

        If srv.rst.EOF = False And srv.rst.BOF = False Then
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False

                Dim ht As New Hashtable
                ht.Add(GDF_AS_REPORTS_PARENT_ID_VAR, srv.rst.Fields(GDF_AS_REPORTS_PARENT_ID_VAR).Value)
                ht.Add(GDF_AS_REPORTS_NAME_VAR, srv.rst.Fields(GDF_AS_REPORTS_NAME_VAR).Value)
                ht.Add(GDF_AS_REPORTS_ACCOUNT_ID, srv.rst.Fields(GDF_AS_REPORTS_ACCOUNT_ID).Value)
                ht.Add(GDF_AS_REPORTS_TYPE_VAR, srv.rst.Fields(GDF_AS_REPORTS_TYPE_VAR).Value)
                ht.Add(GDF_AS_REPORTS_COLOR_VAR, srv.rst.Fields(GDF_AS_REPORTS_COLOR_VAR).Value)
                ht.Add(GDF_AS_REPORTS_PALETTE_VAR, srv.rst.Fields(GDF_AS_REPORTS_PALETTE_VAR).Value)
                ht.Add(GDF_AS_REPORTS_AXIS2_NAME_VAR, srv.rst.Fields(GDF_AS_REPORTS_AXIS2_NAME_VAR).Value)
                ht.Add(GDF_AS_REPORTS_SERIE_AXIS_VAR, srv.rst.Fields(GDF_AS_REPORTS_SERIE_AXIS_VAR).Value)
                ht.Add(GDF_AS_REPORTS_SERIE_UNIT_VAR, srv.rst.Fields(GDF_AS_REPORTS_SERIE_UNIT_VAR).Value)
                ht.Add(GDF_AS_REPORTS_DISPLAY_VALUES_VAR, srv.rst.Fields(GDF_AS_REPORTS_DISPLAY_VALUES_VAR).Value)

                reports_dict.Add(srv.rst.Fields(GDF_AS_REPORTS_ID_VAR).Value, ht)
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()
        srv = Nothing
        Return reports_dict

    End Function

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region




End Class
