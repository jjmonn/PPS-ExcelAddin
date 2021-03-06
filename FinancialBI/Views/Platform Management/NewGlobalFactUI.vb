﻿' NewRatesVersionUI.vb
'
'
'
'
' Author: Julien Monnereau
' Last modified: 24/08/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.Controls


Friend Class NewGlobalFactUI


#Region "Instance Variables"

    Private m_controller As GlobalFactController

#End Region


#Region "Initialize"

    Friend Sub New(ByRef p_controller As GlobalFactController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = p_controller
        MultilanguageSetup()

    End Sub

    Private Sub MultilanguageSetup()

        Me.m_nameLabel.Text = Local.GetValue("general.name")
        Me.CancelBT.Text = Local.GetValue("general.cancel")
        Me.ValidateBT.Text = Local.GetValue("general.create")
        Me.Text = Local.GetValue("global_facts.new_global_fact")



    End Sub

#End Region


#Region "Call Backs"

    Private Sub ValidateBT_Click(sender As Object, e As EventArgs) Handles ValidateBT.Click

        Dim name As String = NameTB.Text
        If Len(name) < NAMES_MAX_LENGTH AndAlso Len(name) > 0 Then
            If m_controller.IsUsedName(name) Then MsgBox(Local.GetValue("global_facts.msg_name_already_taken")) Else _
                m_controller.CreateFact(name)
            Me.Hide()
        Else
            MsgBox(Local.GetValue("general.msg_name_exceed1") & NAMES_MAX_LENGTH & Local.GetValue("general.msg_name_exceed1"))
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()

    End Sub


#End Region


#Region "Events"


    Private Sub NewGlobalFactUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()

    End Sub


#End Region

End Class