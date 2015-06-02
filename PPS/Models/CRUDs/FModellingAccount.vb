' FModellingAccount.vb
' 
' CRUD model for table fmodelling_acccounts
' 
' To do:
'     
'
' Known bugs: 
'       - 
'
'
' Author: Julien Monnereau
' Last modified: 11/05/2015


Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.Collections
Imports ADODB



Friend Class FModellingAccount


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
        Dim i As Int32 = 0
        object_is_alive = SRV.OpenRst(CONFIG_DATABASE + "." + FINANCIAL_MODELLING_TABLE, ModelServer.DYNAMIC_CURSOR)
        RST = SRV.rst
        RST.Sort = ITEMS_POSITIONS

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateFModellingAccount(ByRef newAccountAttributes As Hashtable)

        Dim fieldsArray(newAccountAttributes.Count - 1) As Object
        Dim valuesArray(newAccountAttributes.Count - 1) As Object
        newAccountAttributes.Keys.CopyTo(fieldsArray, 0)
        newAccountAttributes.Values.CopyTo(valuesArray, 0)
        RST.AddNew(fieldsArray, valuesArray)

    End Sub

    Protected Friend Function ReadFModellingAccount(ByVal fmodelling_account_id As String, ByRef field As String) As Object

        RST.Filter = FINANCIAL_MODELLING_ID_VARIABLE + "='" + fmodelling_account_id + "'"
        If RST.EOF Then Return Nothing
        Return RST.Fields(field).Value

    End Function

    Protected Friend Function GetSeriHT(ByRef fmodelling_account_id As String) As Hashtable

        Dim ht As New Hashtable
        RST.Filter = FINANCIAL_MODELLING_ID_VARIABLE + "='" + fmodelling_account_id + "'"
        If RST.EOF Then Return Nothing
        ht.Add(FINANCIAL_MODELLING_NAME_VARIABLE, RST.Fields(FINANCIAL_MODELLING_NAME_VARIABLE).Value)
        ht.Add(CONTROL_CHART_COLOR_VARIABLE, RST.Fields(FINANCIAL_MODELLING_SERIE_COLOR_VARIABLE).Value)
        ht.Add(CONTROL_CHART_TYPE_VARIABLE, RST.Fields(FINANCIAL_MODELLING_SERIE_TYPE_VARIABLE).Value)
        ht.Add(FINANCIAL_MODELLING_SERIE_CHART_VARIABLE, RST.Fields(FINANCIAL_MODELLING_SERIE_CHART_VARIABLE).Value)
        Return ht

    End Function

    Protected Friend Sub UpdateFModellingAccount(ByRef fmodelling_account_id As String, _
                                       ByRef field As String, _
                                       ByVal value As String)

        RST.Filter = FINANCIAL_MODELLING_ID_VARIABLE + "='" + fmodelling_account_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If IsDBNull(RST.Fields(field).Value) Then
                If value <> "" Then
                    RST.Fields(field).Value = value
                    RST.Update()
                End If
            ElseIf RST.Fields(field).Value <> value Then
                RST.Fields(field).Value = value
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteFModellingAccount(ByRef fmodelling_account_id As String)

        RST.Filter = FINANCIAL_MODELLING_ID_VARIABLE + "='" + fmodelling_account_id + "'"
        If RST.EOF = False Then
            RST.Delete()
            RST.Update()
        End If

    End Sub

#End Region


#Region "Utilities"

    Friend Shared Function getFAccountsNode() As TreeNode

        Dim f_accounts_nodes As New TreeNode
        Dim srv As New ModelServer
        If srv.OpenRst(CONFIG_DATABASE + "." + FINANCIAL_MODELLING_TABLE, ModelServer.DYNAMIC_CURSOR) Then

            Dim currentNode, ParentNode() As TreeNode
            srv.rst.Sort = ITEMS_POSITIONS

            Do While srv.rst.EOF = False
                If IsDBNull(srv.rst.Fields(FINANCIAL_MODELLING_PARENT_ID_VARIABLE).Value) Then
                    currentNode = f_accounts_nodes.Nodes.Add(Trim(srv.rst.Fields(FINANCIAL_MODELLING_ID_VARIABLE).Value), _
                                                                Trim(srv.rst.Fields(FINANCIAL_MODELLING_NAME_VARIABLE).Value))
                Else
                    ParentNode = f_accounts_nodes.Nodes.Find(Trim(srv.rst.Fields(FINANCIAL_MODELLING_PARENT_ID_VARIABLE).Value), True)
                    currentNode = ParentNode(0).Nodes.Add(Trim(srv.rst.Fields(FINANCIAL_MODELLING_ID_VARIABLE).Value), _
                                                            Trim(srv.rst.Fields(FINANCIAL_MODELLING_NAME_VARIABLE).Value))
                End If
                srv.rst.MoveNext()
            Loop
            srv.rst.Close()
        End If
        Return f_accounts_nodes

    End Function

    Protected Overrides Sub finalize()

        On Error Resume Next
        RST.Close()
        MyBase.Finalize()

    End Sub


#End Region
   


End Class
