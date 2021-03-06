' ReportUploadEntitySelectionPane.vb
' 
' Task pane for Entity Input Report Selection
'
' Author: Julien Monnereau
' Last modified: 14/01/2016


Imports System.Runtime.InteropServices
Imports AddinExpress.XL
Imports System.Collections
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports VIBlend.WinForms.Controls



Public Class ReportUploadEntitySelectionPane

#Region "Instance Variables"

    Private m_inputReportCreationController As New InputReportsBuildingController
    Private m_periodsRangeSelection As PeriodRangeSelectionControl

#End Region

#Region "Initialize"

    Public Sub New()
        MyBase.New()

        InitializeComponent()
        VTreeViewUtil.InitTVFormat(m_entitiesTV)
        m_entitiesTV.ImageList = EntitiesTVImageList
      
    End Sub

    Private Sub MultilangueSetup()

        Me.Text = Local.GetValue("upload.input_sidepane_title")
        m_entitySelectionLabel.Text = Local.GetValue("upload.entity_selection")
        m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection")
        m_periodsSelectionLabel.Text = Local.GetValue("upload.periods_selection")
        m_validateButton.Text = Local.GetValue("general.validate")

    End Sub

    ' Init TV and combo boxes
    Friend Sub InitializeSelectionChoices(ByRef AddinInstance As AddinModule)

        m_entitiesTV.Nodes.Clear()
        m_accountSelectionComboBox.Items.Clear()
        GlobalVariables.AxisElems.LoadEntitiesTV(m_entitiesTV)
        m_entitiesTV.CollapseAll()

        InitializePeriodsSelection()

        Dim l_accountsItemVisible As Boolean = False
        If My.Settings.processId = CRUD.Account.AccountProcess.RH Then
            InitializeRHComboBoxChoices()
            l_accountsItemVisible = True
        End If
        m_accountSelectionComboBox.Visible = l_accountsItemVisible
        m_accountSelectionLabel.Visible = l_accountsItemVisible

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

    Private Sub InitializePeriodsSelection()


    End Sub

    Private Sub InitPeriodsRangeSelection()

        m_periodsSelectionPanel.Controls.Clear()
        m_periodsRangeSelection = New PeriodRangeSelectionControl(My.Settings.version_id)
        m_periodsSelectionPanel.Controls.Add(m_periodsRangeSelection)
        m_periodsSelectionPanel.Dock = DockStyle.Fill

    End Sub

#End Region

#Region "Interface"

    ' Validate
    Private Sub ValidateInputSelection()
        If Not m_entitiesTV.SelectedNode Is Nothing AndAlso m_entitiesTV.SelectedNode.Nodes.Count = 0 Then
              Select My.Settings.processId
                Case CRUD.Account.AccountProcess.FINANCIAL
                    m_inputReportCreationController.InputReportPaneCallBack_ReportCreation(My.Settings.processId, Nothing, "")
                Case CRUD.Account.AccountProcess.RH
                    Dim l_periods = m_periodsRangeSelection.GetPeriodList()
                    If m_accountSelectionComboBox.Text <> "" Then
                        m_inputReportCreationController.InputReportPaneCallBack_ReportCreation(My.Settings.processId, l_periods, m_accountSelectionComboBox.Text)
                    Else
                        MsgBox(Local.GetValue("upload.msg_no_account_selected"))
                    End If
            End Select
      
        End If
    End Sub


#End Region

#Region "Events"

    Private Sub EntitiesTV_NodeMouseDoubleClick(sender As Object, e As MouseEventArgs) Handles m_entitiesTV.MouseDoubleClick
        Dim l_node As vTreeNode = VTreeViewUtil.GetNodeAtPosition(m_entitiesTV, e.Location)
        If l_node IsNot Nothing Then
            m_entitiesTV.SelectedNode = l_node
            ValidateInputSelection()
        End If
    End Sub

    Private Sub EntitiesTV_KeyPress(sender As Object, e As Windows.Forms.KeyPressEventArgs) Handles m_entitiesTV.KeyPress
        If e.KeyChar = Chr(13) Then
            ValidateInputSelection()
        End If
    End Sub

    Private Sub m_validateButton_Click(sender As Object, e As EventArgs) Handles m_validateButton.Click
        ValidateInputSelection()
    End Sub

    Private Sub ADXExcelTaskPane1_ADXBeforeTaskPaneShow(sender As Object, e As ADXBeforeTaskPaneShowEventArgs) Handles MyBase.ADXBeforeTaskPaneShow
        Me.Visible = GlobalVariables.InputSelectionTaskPaneVisible
    End Sub

    Private Sub CInputSelectionPane_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
    End Sub

    Private Sub CInputSelectionPane_ADXAfterTaskPaneShow(sender As Object, e As ADXAfterTaskPaneShowEventArgs) Handles MyBase.ADXAfterTaskPaneShow

        m_entitiesTV.Select()
        If m_entitiesTV.Nodes.Count > 0 Then m_entitiesTV.SelectedNode = m_entitiesTV.Nodes(0)
        MultilangueSetup()

        Select Case My.Settings.processId
            Case CRUD.Account.AccountProcess.FINANCIAL : PeriodsControlDisplay(False)
            Case CRUD.Account.AccountProcess.RH
                PeriodsControlDisplay(True)
        End Select


    End Sub

#End Region

#Region "Utilities"

    Private Sub PeriodsControlDisplay(ByRef p_visible As Boolean)
    If Not m_periodsRangeSelection Is Nothing Then m_periodsRangeSelection.Visible = p_visible
  End Sub

#End Region

End Class
