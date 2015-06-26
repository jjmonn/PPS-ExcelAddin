' Control.vb
'
' Controls table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 27/01/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class Adjustment


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean
   
#End Region


#Region "Initialize"

    Protected Friend Sub New()

        SRV = New ModelServer
        Dim q_result = SRV.OpenRst(GlobalVariables.database & "." & ADJUSTMENTS_TABLE, ModelServer.DYNAMIC_CURSOR)
        RST = SRV.rst
        object_is_alive = q_result

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateAdjustment(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadAdjustment(ByRef Adjustment_id As String, ByRef field As String) As Object

        RST.Filter = ANALYSIS_AXIS_ID_VAR + "='" + Adjustment_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Sub UpdateAdjustment(ByRef Adjustment_id As String, ByRef hash As Hashtable)

        RST.Filter = ANALYSIS_AXIS_ID_VAR + "='" + Adjustment_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In hash.Keys
                If RST.Fields(Attribute).Value <> hash(Attribute) Then
                    RST.Fields(Attribute).Value = hash(Attribute)
                    RST.Update()
                End If
            Next
        End If

    End Sub

    Protected Friend Sub UpdateAdjustment(ByRef Adjustment_id As String, _
                                          ByRef field As String, _
                                          ByVal value As Object)

        RST.Filter = ANALYSIS_AXIS_ID_VAR + "='" + Adjustment_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteAdjustment(ByRef Adjustment_id As String)

        RST.Filter = ANALYSIS_AXIS_ID_VAR + "='" + Adjustment_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub

    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub LoadAdjustmentsTree(ByRef TV As TreeView)

        Dim srv As New ModelServer
        Dim q_result As Boolean
        q_result = srv.OpenRst(GlobalVariables.database & "." & ADJUSTMENTS_TABLE, ModelServer.FWD_CURSOR)
        If q_result = True Then
            TV.Nodes.Clear()
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                Dim node As TreeNode = TV.Nodes.Add(Trim(srv.rst.Fields(ANALYSIS_AXIS_ID_VAR).Value), _
                                                    Trim(srv.rst.Fields(ANALYSIS_AXIS_NAME_VAR).Value), 0, 0)
                node.Checked = True
                srv.rst.MoveNext()
            Loop
            End If
            srv.rst.Close()
       
    End Sub


#End Region


End Class
