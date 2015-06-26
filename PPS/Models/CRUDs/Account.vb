﻿' Account.vb
' 
' CRUD model for table accounts
' 
' To do:
'       - Delete accounts: delete references of the account in other formulas
'     
'
' Known bugs: 
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 05/01/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports ADODB


Friend Class Account


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Private RST As Recordset

    ' Constants
    Private ACCOUNTS_TABLE_ADDRESS As String = GlobalVariables.database + "." + ACCOUNTS_TABLE
    Friend object_is_alive As Boolean

#End Region


#Region "Initialize"

    Protected Friend Sub New()

        SRV = New ModelServer
        Dim i As Int32 = 0
        Dim q_result = SRV.openRst(ACCOUNTS_TABLE_ADDRESS, ModelServer.STATIC_CURSOR)
        While q_result = False AndAlso i < 10
            q_result = SRV.openRst(ACCOUNTS_TABLE_ADDRESS, ModelServer.STATIC_CURSOR)
            i = i + 1
        End While
        RST = SRV.rst
        object_is_alive = q_result

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateAccount(ByRef newAccountAttributes As Hashtable)

        Dim fieldsArray(newAccountAttributes.Count - 1) As Object
        Dim valuesArray(newAccountAttributes.Count - 1) As Object
        newAccountAttributes.Keys.CopyTo(fieldsArray, 0)
        newAccountAttributes.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadAccount(ByRef accountKey As String, ByRef field As String) As Object

        RST.Filter = ACCOUNT_ID_VARIABLE + "='" + accountKey + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Sub UpdateAccount(ByRef accountKey As String, ByRef accountAttributes As Hashtable)

        RST.Filter = ACCOUNT_ID_VARIABLE + "='" + accountKey + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            For Each Attribute In accountAttributes.Keys
                If RST.Fields(Attribute).Value <> accountAttributes(Attribute) Then
                    RST.Fields(Attribute).Value = accountAttributes(Attribute)
                    RST.Update()
                End If
            Next
        End If

    End Sub

    Protected Friend Sub UpdateAccount(ByRef accountKey As String, _
                             ByRef field As String, _
                             ByVal value As Object)

        RST.Filter = ACCOUNT_ID_VARIABLE + "='" + accountKey + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteAccount(ByRef accountKey As String)

        RST.Filter = ACCOUNT_ID_VARIABLE + "='" + accountKey + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub


#End Region


#Region "Utilities"

    Friend Shared Sub LoadAccountsTree(ByRef TV As TreeView)

        Dim srv As New ModelServer
        If srv.openRst(GlobalVariables.database + "." + ACCOUNTS_TABLE, ModelServer.FWD_CURSOR) Then
            srv.rst.Sort = ITEMS_POSITIONS
            TreeViewsUtilities.LoadAccountsTree(TV, srv.rst)
            srv.rst.Close()
        End If

    End Sub

    Protected Friend Sub UpdatePositionsDictionary()

        SRV.sqlQuery("CALL " & GlobalVariables.database & "." & UPDATE_ACCOUNTS_POSITIONS_SP)

    End Sub

    Protected Friend Sub Close()

        On Error Resume Next
        RST.Close()
        Me.finalize()

    End Sub

    Protected Overrides Sub finalize()

        Try
            RST.Close()
        Catch ex As Exception
        End Try
        MyBase.Finalize()

    End Sub

#End Region


End Class
