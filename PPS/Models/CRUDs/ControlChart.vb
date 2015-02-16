' ControlChart.vb
'
' controlcharts table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 05/01/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class ControlChart


#Region "Instance Variables"

    ' Objects
    Private SRV As New ModelServer
    Private RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean
    Private Const NB_CONNECTIONS_TRIALS = 10

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        Dim i As Int32 = 0
        Dim q_result = SRV.openRst(CONFIG_DATABASE & "." & CONTROL_CHART_TABLE, ModelServer.DYNAMIC_CURSOR)
        While q_result = False AndAlso i < NB_CONNECTIONS_TRIALS
            q_result = SRV.openRst(CONFIG_DATABASE & "." & CONTROL_CHART_TABLE, ModelServer.DYNAMIC_CURSOR)
            i = i + 1
        End While
        SRV.rst.Sort = ITEMS_POSITIONS
        RST = SRV.rst
        object_is_alive = q_result

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateControlChart(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadControlChart(ByRef controlchart_id As String, ByRef field As String) As Object

        RST.Filter = CONTROL_CHART_ID_VARIABLE + "='" + controlchart_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function GetSerieHT(ByRef controlchart_id As String) As Hashtable

        Dim ht As New Hashtable
        RST.Filter = CONTROL_CHART_ID_VARIABLE + "='" + controlchart_id + "'"
        If RST.EOF Then Return Nothing
        ht.Add(CONTROL_CHART_NAME_VARIABLE, RST.Fields(CONTROL_CHART_NAME_VARIABLE).Value)
        ht.Add(CONTROL_CHART_TYPE_VARIABLE, RST.Fields(CONTROL_CHART_TYPE_VARIABLE).Value)
        ht.Add(CONTROL_CHART_COLOR_VARIABLE, RST.Fields(CONTROL_CHART_COLOR_VARIABLE).Value)
        ht.Add(CONTROL_CHART_ACCOUNT_ID_VARIABLE, RST.Fields(CONTROL_CHART_ACCOUNT_ID_VARIABLE).Value)
        ht.Add(CONTROL_CHART_PALETTE_VARIABLE, RST.Fields(CONTROL_CHART_PALETTE_VARIABLE).Value)
        Return ht

    End Function

    Protected Friend Sub UpdateControlChart(ByRef controlchart_id As String, _
                                  ByRef field As String, _
                                  ByVal value As Object)

        RST.Filter = CONTROL_CHART_ID_VARIABLE + "='" + controlchart_id + "'"
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

    Protected Friend Sub DeleteControlChart(ByRef controlchart_id As String)

        RST.Filter = CONTROL_CHART_ID_VARIABLE + "='" + controlchart_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub LoadControlChartsTree(ByRef TV As TreeView)

        Dim srv As New ModelServer
        Dim q_result As Boolean = srv.openRst(CONFIG_DATABASE & "." & CONTROL_CHART_TABLE, ModelServer.FWD_CURSOR)
        If q_result = True Then

            Dim currentNode, ParentNode() As TreeNode
            TV.Nodes.Clear()
            srv.rst.Sort = ITEMS_POSITIONS

            If srv.rst.RecordCount > 0 Then
                srv.rst.MoveFirst()

                Do While srv.rst.EOF = False

                    If IsDBNull(srv.rst.Fields(CONTROL_CHART_PARENT_ID_VARIABLE).Value) Then
                        currentNode = TV.Nodes.Add(Trim(srv.rst.Fields(CONTROL_CHART_ID_VARIABLE).Value), _
                                                   Trim(srv.rst.Fields(CONTROL_CHART_NAME_VARIABLE).Value), 0, 0)
                    Else

                        ParentNode = TV.Nodes.Find(Trim(srv.rst.Fields(CONTROL_CHART_PARENT_ID_VARIABLE).Value), True)
                        currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(CONTROL_CHART_ID_VARIABLE).Value), _
                                                                  Trim(srv.rst.Fields(CONTROL_CHART_NAME_VARIABLE).Value), 1, 1)
                    End If
                    currentNode.EnsureVisible()
                    srv.rst.MoveNext()
                Loop
            End If
            srv.rst.Close()
        End If

    End Sub

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region


End Class
