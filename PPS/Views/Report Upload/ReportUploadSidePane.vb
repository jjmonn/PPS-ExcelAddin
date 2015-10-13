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
        If GlobalVariables.Accounts.m_accountsHash.ContainsKey(p_accountId) Then
            m_accountTextBox.Text = GlobalVariables.Accounts.m_accountsHash(p_accountId)(NAME_VARIABLE)
            Dim formulaTypeId As Int32 = GlobalVariables.Accounts.m_accountsHash(p_accountId)(ACCOUNT_FORMULA_TYPE_VARIABLE)
            Select Case formulaTypeId
                Case GlobalEnums.FormulaTypes.TITLE
                    m_accountTypeTextBox.Text = "Title"
                Case GlobalEnums.FormulaTypes.HARD_VALUE_INPUT
                    m_accountTypeTextBox.Text = "Input"
                Case GlobalEnums.FormulaTypes.FORMULA
                    m_accountTypeTextBox.Text = "Formula"
                Case GlobalEnums.FormulaTypes.FIRST_PERIOD_INPUT
                    m_accountTypeTextBox.Text = "First period input"
                Case GlobalEnums.FormulaTypes.AGGREGATION_OF_SUB_ACCOUNTS
                    m_accountTypeTextBox.Text = "Aggregation of sub Accounts"
            End Select
            m_formulaTextBox.Text = m_formulaTranslator.GetHumanFormulaFromDB(GlobalVariables.Accounts.m_accountsHash(p_accountId)(ACCOUNT_FORMULA_VARIABLE))
            m_descriptionTextBox.Text = GlobalVariables.Accounts.m_accountsHash(p_accountId)(ACCOUNT_DESCRIPTION_VARIABLE)
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
