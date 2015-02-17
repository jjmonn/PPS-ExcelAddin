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
' Last modified: 17/02/2015


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
        Dim q_result = SRV.openRst(CONFIG_DATABASE + "." + FINANCIAL_MODELLING_TABLE, ModelServer.DYNAMIC_CURSOR)
        While q_result = False AndAlso i < 10
            q_result = SRV.openRst(CONFIG_DATABASE + "." + FINANCIAL_MODELLING_TABLE, ModelServer.DYNAMIC_CURSOR)
            i = i + 1
        End While
        RST = SRV.rst
        RST.Sort = ITEMS_POSITIONS
        object_is_alive = q_result

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
        While RST.EOF = False
            ht.Add(FINANCIAL_MODELLING_NAME_VARIABLE, RST.Fields(FINANCIAL_MODELLING_NAME_VARIABLE).Value)
            ht.Add(FINANCIAL_MODELLING_SERIE_COLOR_VARIABLE, RST.Fields(FINANCIAL_MODELLING_SERIE_COLOR_VARIABLE).Value)
            ht.Add(FINANCIAL_MODELLING_SERIE_TYPE_ENTITY_VARIABLE, RST.Fields(FINANCIAL_MODELLING_SERIE_TYPE_ENTITY_VARIABLE).Value)
            ht.Add(FINANCIAL_MODELLING_SERIE_CHART_VARIABLE, RST.Fields(FINANCIAL_MODELLING_SERIE_CHART_VARIABLE).Value)
        End While
        Return ht

    End Function

    Protected Friend Sub UpdateFModellingAccount(ByRef fmodelling_account_id As String, _
                                       ByRef field As String, _
                                       ByRef value As String)

        RST.Filter = FINANCIAL_MODELLING_ID_VARIABLE + "='" + fmodelling_account_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(field).Value <> value Then
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


    Protected Overrides Sub finalize()

        RST.Close()
        MyBase.Finalize()

    End Sub



End Class
