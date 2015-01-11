' CredentialLevel.vb
'
' credential_levels table CRUD model
'
' To do: 
'
'
'
'
' Author: Julien Monnereau
' Last modified: 05/01/2015


Imports ADODB
Imports System.Collections


Friend Class CredentialLevel


#Region "Instance Variables"

    ' Objects
    Private SRV As ModelServer
    Friend RST As Recordset

    ' Constants
    Friend object_is_alive As Boolean
    Private Const NB_CONNECTIONS_TRIALS = 10

#End Region


#Region "Initialize"

    Public Sub New()

        SRV = New ModelServer
        Dim i As Int32 = 0
        Dim q_result = SRV.openRst(CONFIG_DATABASE & "." & CREDENTIALS_ID_TABLE, ModelServer.DYNAMIC_CURSOR)
        While q_result = False AndAlso i < NB_CONNECTIONS_TRIALS
            q_result = SRV.openRst(CONFIG_DATABASE & "." & CREDENTIALS_ID_TABLE, ModelServer.DYNAMIC_CURSOR)
            i = i + 1
        End While
        RST = SRV.rst
        object_is_alive = q_result

    End Sub

#End Region


#Region "CRUD Interface"

    Protected Friend Sub CreateCredential_level(ByRef entity_id As String, _
                                      ByRef credential_level As Int32)

        RST.AddNew()
        RST.Fields(CREDENTIALS_ASSETID_VARIABLE).Value = entity_id
        RST.Fields(CREDENTIALS_ID_VARIABLE).Value = credential_level
        RST.Update()

    End Sub

    Protected Friend Function ReadCredential_level(ByRef entity_id As String) As Int32

        RST.Filter = CREDENTIALS_ASSETID_VARIABLE + "='" + entity_id + "'"
        If RST.EOF Then Return -1
        Return RST.Fields(CREDENTIALS_ID_VARIABLE).Value

    End Function

    Protected Friend Sub UpdateCredential_level(ByRef entity_id As String, _
                                      ByRef credential_level As Int32)

        RST.Filter = CREDENTIALS_ASSETID_VARIABLE + "='" + entity_id + "'"
        If RST.EOF = False AndAlso RST.BOF = False Then
            If RST.Fields(CREDENTIALS_ID_VARIABLE).Value <> credential_level Then
                RST.Fields(CREDENTIALS_ID_VARIABLE).Value = credential_level
                RST.Update()
            End If
        End If

    End Sub

    Protected Friend Sub DeleteCredential_level(ByRef entity_id As String)

        RST.Filter = CREDENTIALS_ASSETID_VARIABLE + "='" + entity_id + "'"
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
