' ReportUploadSidePane.vb
'
' Displays information on accounts
'
'
' Author: Julien Monnereau
' Created: 13/10/2015
' Last modified: 13/10/2015


Imports System.Runtime.InteropServices
Imports AddinExpress.XL
Imports CRUD

Public Class ReportUploadSidePane

    Private m_formulaTranslator As FormulasTranslations


    Public Sub New()
        MyBase.New()
        InitializeComponent()
    End Sub

    Private Sub ADXExcelTaskPane1_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow
        Me.Visible = GlobalVariables.s_reportUploadSidePaneVisible
        m_formulaTranslator = New FormulasTranslations()
    End Sub

    Friend Sub DisplayAccountDetails(ByRef p_accountId As Int32)
        Dim l_account = GlobalVariables.Accounts.GetAccount(p_accountId)

        If Not l_account Is Nothing Then
            m_accountTextBox.Text = l_account.Name
            Dim formulaTypeId As Account.FormulaTypes = l_account.FormulaType
            Select Case formulaTypeId
                Case Account.FormulaTypes.TITLE
                    m_accountTypeTextBox.Text = "Title"
                Case Account.FormulaTypes.HARD_VALUE_INPUT
                    m_accountTypeTextBox.Text = "Input"
                Case Account.FormulaTypes.FORMULA
                    m_accountTypeTextBox.Text = "Formula"
                Case Account.FormulaTypes.FIRST_PERIOD_INPUT
                    m_accountTypeTextBox.Text = "First period input"
                Case Account.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS
                    m_accountTypeTextBox.Text = "Aggregation of sub Accounts"
            End Select
            m_formulaTextBox.Text = m_formulaTranslator.GetHumanFormulaFromDB(l_account.Formula)
            m_descriptionTextBox.Text = l_account.Formula
        Else
            DisplayEmptyTextBoxes()
        End If
    End Sub

    Friend Sub DisplayEmptyTextBoxes()
        m_accountTextBox.Text = ""
        m_formulaTextBox.Text = ""
        m_descriptionTextBox.Text = ""
        m_accountTypeTextBox.Text = ""
    End Sub

    Private Sub ReportUploadSidePane_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DisplayEmptyTextBoxes()
        m_formulaTranslator = Nothing
        e.Cancel = True
        Me.Visible = False
    End Sub

End Class
