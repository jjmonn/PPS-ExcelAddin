' SnapshotPeriodRangeSelectionUI.vb
'
' Allows users to select the period range on which the snapshot will be based. 
'
'
' To do : factor controls with upload period range selection
'
' Create by: Julien Monnereau
' Created on: 06/01/2016
' Last modified: 06/01/2016

Imports System.Collections.Generic


Public Class SnapshotPeriodRangeSelectionUI

#Region "Instance variables"

    Private m_addin As AddinModule
    Private m_periodSelectionControl As PeriodRangeSelectionControl

#End Region

#Region "Initialize"

    Friend Sub New(ByRef p_addin As AddinModule, _
                   ByRef p_versionId As UInt32)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_addin = p_addin
        InitializeRHComboBoxChoices()
        MultilanguageSetup()
        m_periodSelectionControl = New PeriodRangeSelectionControl(p_versionId)
        m_periodSelectionPanel.Controls.Add(m_periodSelectionControl)
        m_periodSelectionControl.Dock = Windows.Forms.DockStyle.Fill

    End Sub

    Private Sub MultilanguageSetup()

        Me.Text = Local.GetValue("upload.periods_selection")
        m_validateButton.Text = Local.GetValue("general.validate")
        m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection")

    End Sub

    Private Sub InitializeRHComboBoxChoices()

        Dim l_rhAccounts As List(Of CRUD.Account) = GlobalVariables.Accounts.GetAccountsList(GlobalEnums.AccountsLookupOptions.LOOKUP_INPUTS, CRUD.Account.AccountProcess.RH)
        Select Case l_rhAccounts.Count
            Case 0
            Case 1
                m_accountSelectionComboBox.SelectedItem = GeneralUtilities.AddItemToCombobox(m_accountSelectionComboBox, l_rhAccounts(0).Id, l_rhAccounts(0).Name)
                m_accountSelectionComboBox.Enabled = False

            Case Else
                For Each l_account As CRUD.Account In l_rhAccounts
                    GeneralUtilities.AddItemToCombobox(m_accountSelectionComboBox, l_account.Id, l_account.Name)
                Next
                m_accountSelectionComboBox.SelectedItem = m_accountSelectionComboBox.Items(0)

        End Select

    End Sub
  
#End Region

#Region "Call backs"

    Private Sub m_validateButton_Click(sender As Object, e As EventArgs) Handles m_validateButton.Click

        Dim l_periods = m_periodSelectionControl.GetPeriodList()
        If m_accountSelectionComboBox.Text <> "" Then
            Me.Hide()
            m_addin.AssociateReportUploadControler(False, l_periods, m_accountSelectionComboBox.Text)
            Me.Close()
        Else
            MsgBox(Local.GetValue("upload.msg_no_account_selected"))
        End If
        

    End Sub

#End Region

End Class