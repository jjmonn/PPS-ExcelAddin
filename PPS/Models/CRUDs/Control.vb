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
' Last modified: 05/01/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class Control


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean
    Private Const NB_CONNECTIONS_TRIALS = 10

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        SRV = New ModelServer
        Dim i As Int32 = 0
        Dim q_result = SRV.openRst(CONFIG_DATABASE & "." & CONTROLS_TABLE, ModelServer.DYNAMIC_CURSOR)
        While q_result = False AndAlso i < NB_CONNECTIONS_TRIALS
            q_result = SRV.openRst(CONFIG_DATABASE & "." & CONTROLS_TABLE, ModelServer.DYNAMIC_CURSOR)
            i = i + 1
        End While
        RST = SRV.rst
        object_is_alive = q_result

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateControl(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadControl(ByRef control_id As String, ByRef field As String) As Object

        RST.Filter = CONTROL_ID_VARIABLE + "='" + control_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function ReadAll() As Dictionary(Of String, Hashtable)

        RST.Filter = ""
        Dim dic As New Dictionary(Of String, Hashtable)
        While RST.EOF = False
            Dim HT As New Hashtable
            HT.Add(CONTROL_ID_VARIABLE, RST(CONTROL_ID_VARIABLE).Value)
            HT.Add(CONTROL_NAME_VARIABLE, RST(CONTROL_NAME_VARIABLE).Value)
            HT.Add(CONTROL_ITEM1_VARIABLE, RST(CONTROL_ITEM1_VARIABLE).Value)
            HT.Add(CONTROL_ITEM2_VARIABLE, RST(CONTROL_ITEM2_VARIABLE).Value)
            HT.Add(CONTROL_OPERATOR_ID_VARIABLE, RST(CONTROL_OPERATOR_ID_VARIABLE).Value)
            HT.Add(CONTROL_PERIOD_OPTION_VARIABLE, RST(CONTROL_PERIOD_OPTION_VARIABLE).Value)
            dic.Add(RST(CONTROL_ID_VARIABLE).Value, HT)
            RST.MoveNext()
        End While
        Return dic

    End Function

    Protected Friend Sub UpdateControl(ByRef control_id As String, ByRef hash As Hashtable)

        RST.Filter = CONTROL_ID_VARIABLE + "='" + control_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In hash.Keys
                If RST.Fields(Attribute).Value <> hash(Attribute) Then
                    RST.Fields(Attribute).Value = hash(Attribute)
                    RST.Update()
                End If
            Next
        End If

    End Sub

    Protected Friend Sub UpdateControl(ByRef control_id As String, _
                              ByRef field As String, _
                              ByVal value As Object)

        RST.Filter = CONTROL_ID_VARIABLE + "='" + control_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteControl(ByRef control_id As String)

        RST.Filter = CONTROL_ID_VARIABLE + "='" + control_id + "'"
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



End Class
