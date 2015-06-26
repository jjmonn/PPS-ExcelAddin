﻿' Control.vb
'
' Controls table CRUD model
'
' To do: 
'  We could have only one AnalysisAxis CRUD Model
'
'
'
' Author: Julien Monnereau
' Last modified: 27/07/2015


Imports ADODB
Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic


Friend Class Client


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
        object_is_alive = SRV.OpenRst(GlobalVariables.database & "." & CLIENTS_TABLE, ModelServer.DYNAMIC_CURSOR)
        RST = SRV.rst

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateClient(ByRef hash As Hashtable)

        Dim fieldsArray(hash.Count - 1) As Object
        Dim valuesArray(hash.Count - 1) As Object
        hash.Keys.CopyTo(fieldsArray, 0)
        hash.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadClient(ByRef Client_id As String, ByRef field As String) As Object

        RST.Filter = ANALYSIS_AXIS_ID_VAR + "='" + Client_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function GetRecord(ByRef client_id As String, ByRef categoriesTV As TreeView) As Hashtable

        RST.Filter = ANALYSIS_AXIS_ID_VAR + "='" + client_id + "'"
        If RST.EOF Then Return Nothing
        Dim hash As New Hashtable
        hash.Add(ANALYSIS_AXIS_NAME_VAR, RST.Fields(ANALYSIS_AXIS_NAME_VAR).Value)
        For Each category_node In categoriesTV.Nodes
            hash.Add(category_node.name, RST.Fields(category_node.name).Value)
        Next
        Return hash

    End Function

    Protected Friend Sub UpdateClient(ByRef Client_id As String, ByRef hash As Hashtable)

        RST.Filter = ANALYSIS_AXIS_ID_VAR + "='" + Client_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In hash.Keys
                If RST.Fields(Attribute).Value <> hash(Attribute) Then
                    RST.Fields(Attribute).Value = hash(Attribute)
                    RST.Update()
                End If
            Next
        End If

    End Sub

    Protected Friend Sub UpdateClient(ByRef Client_id As String, _
                                          ByRef field As String, _
                                          ByVal value As Object)

        RST.Filter = ANALYSIS_AXIS_ID_VAR + "='" + Client_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteClient(ByRef Client_id As String)

        RST.Filter = ANALYSIS_AXIS_ID_VAR + "='" + Client_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub

    Protected Overrides Sub finalize()

        On Error Resume Next
        RST.Close()
        MyBase.Finalize()

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Shared Sub LoadClientsTree(ByRef TV As TreeView)

        Dim srv As New ModelServer
        Dim q_result As Boolean
        q_result = srv.OpenRst(GlobalVariables.database & "." & ClientS_TABLE, ModelServer.FWD_CURSOR)
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

    Protected Friend Shared Sub LoadClientsTree(ByRef TV As TreeView, ByRef filter_list As List(Of String))

        Dim srv As New ModelServer
        Dim q_result As Boolean
        q_result = srv.OpenRst(GlobalVariables.database & "." & CLIENTS_TABLE, ModelServer.FWD_CURSOR)
        If q_result = True Then
            TV.Nodes.Clear()
            srv.rst.MoveFirst()
            Do While srv.rst.EOF = False
                If filter_list.Contains(srv.rst.Fields(ANALYSIS_AXIS_ID_VAR).Value) Then
                    Dim node As TreeNode = TV.Nodes.Add(Trim(srv.rst.Fields(ANALYSIS_AXIS_ID_VAR).Value), _
                                                    Trim(srv.rst.Fields(ANALYSIS_AXIS_NAME_VAR).Value), 0, 0)
                    node.Checked = True
                End If
                srv.rst.MoveNext()
            Loop
        End If
        srv.rst.Close()

    End Sub

    Protected Friend Function getNewId() As String

        Dim id As String = TreeViewsUtilities.IssueNewToken(ANALYSIS_AXIS_TOKEN_SIZE)
        Do While Not ReadClient(id, ANALYSIS_AXIS_ID_VAR) Is Nothing
            id = TreeViewsUtilities.IssueNewToken(ANALYSIS_AXIS_TOKEN_SIZE)
        Loop
        Return id

    End Function

    Protected Friend Function isNameValid(ByRef name As String) As Boolean

        If name = "" Then Return False
        SRV.rst.Filter = ANALYSIS_AXIS_NAME_VAR + "='" + name + "'"
        If SRV.rst.EOF = False Then Return False
        Return True

    End Function

    Protected Friend Sub closeRST()

        On Error Resume Next
        RST.Close()

    End Sub

#End Region


#Region "Categories Utilities"

    Protected Friend Shared Function CreateNewClientsVariable(ByRef variable_id As String) As Boolean

        Dim tmp_srv As New ModelServer
        Dim column_values_length As Int32 = CATEGORIES_TOKEN_SIZE + Len(NON_ATTRIBUTED_SUFIX)
        Return tmp_srv.sqlQuery("ALTER TABLE " + GlobalVariables.database + "." + CLIENTS_TABLE + _
                                " ADD COLUMN " & variable_id & " VARCHAR(" & column_values_length & ") DEFAULT '" & variable_id & NON_ATTRIBUTED_SUFIX & "'")

    End Function

    Protected Friend Shared Function DeleteClientsVariable(ByRef variable_id) As Boolean

        Dim tmp_srv As New ModelServer
        Return tmp_srv.sqlQuery("ALTER TABLE " + GlobalVariables.database + "." + CLIENTS_TABLE + _
                                " DROP COLUMN " & variable_id)

    End Function

    Protected Friend Shared Function ReplaceClientsCategoryValue(ByRef category_id As String, _
                                                                 ByRef origin_value As String) As Boolean

        Dim tmp_srv As New ModelServer
        Dim new_value = category_id & NON_ATTRIBUTED_SUFIX
        Return tmp_srv.sqlQuery("UPDATE " & GlobalVariables.database + "." + CLIENTS_TABLE & _
                                " SET " & category_id & "='" & new_value & "'" & _
                                " WHERE " & category_id & "='" & origin_value & "'")

    End Function

#End Region


End Class
