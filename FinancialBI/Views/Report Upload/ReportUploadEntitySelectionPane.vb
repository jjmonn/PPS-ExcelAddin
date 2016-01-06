' ReportUploadEntitySelectionPane.vb
' 
' Task pane for Entity Input Report Selection
'
' Author: Julien Monnereau
' Last modified: 06/01/2016


Imports System.Runtime.InteropServices
Imports AddinExpress.XL
Imports System.Collections
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports VIBlend.WinForms.Controls



Public Class ReportUploadEntitySelectionPane

#Region "Instance Variables"

    Private m_inputReportCreationController As New InputReportsBuildingController

#End Region

#Region "Initialize"

    Public Sub New()
        MyBase.New()

        InitializeComponent()
        VTreeViewUtil.InitTVFormat(m_entitiesTV)
        m_entitiesTV.ImageList = EntitiesTVImageList
        PeriodsRangeSetup()
      
    End Sub

    Private Sub MultilangueSetup()

        Me.Text = Local.GetValue("upload.input_sidepane_title")
        m_entitySelectionLabel.Text = Local.GetValue("upload.entity_selection")
        m_accountSelectionLabel.Text = Local.GetValue("upload.accounts_selection")
        m_periodsSelectionLabel.Text = Local.GetValue("upload.periods_selection")
        m_startDateLabel.Text = Local.GetValue("submissionsFollowUp.start_date")
        m_endDateLabel.Text = Local.GetValue("submissionsFollowUp.end_date")
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
                m_accountSelectionComboBox.SelectedItem = AddItemToCombobox(l_rhAccounts(0).Id, l_rhAccounts(0).Name)
                m_accountSelectionComboBox.Enabled = False

            Case Else
                For Each l_account As CRUD.Account In l_rhAccounts
                    AddItemToCombobox(l_account.Id, l_account.Name)
                Next
                m_accountSelectionComboBox.SelectedItem = m_accountSelectionComboBox.Items(0)

        End Select

    End Sub

    Private Sub InitializePeriodsSelection()


    End Sub

    Private Sub PeriodsRangeSetup()

        m_startDate.FormatValue = "MM-dd-yy"
        m_endDate.FormatValue = "MM-dd-yy"

        Dim l_todaysDate As Date = Today.Date
        m_startDate.Text = l_todaysDate.AddDays(-Period.m_nbDaysInWeek)
        m_endDate.Text = l_todaysDate.AddDays(Period.m_nbDaysInWeek * 3)

        GeneralUtilities.FillWeekTextBox(m_startWeekTB, m_startDate.Text)
        GeneralUtilities.FillWeekTextBox(m_endWeekTB, m_endDate.Text)

    End Sub

#End Region

#Region "Interface"

    ' Validate
    Private Sub ValidateInputSelection()
        If Not m_entitiesTV.SelectedNode Is Nothing AndAlso m_entitiesTV.SelectedNode.Nodes.Count = 0 Then
              Select My.Settings.processId
                Case CRUD.Account.AccountProcess.FINANCIAL
                    m_inputReportCreationController.InputReportPaneCallBack_ReportCreation(My.Settings.processId, Nothing)
                Case CRUD.Account.AccountProcess.RH
                    Dim l_weeksIdList As List(Of Int32) = Period.GetWeeksPeriodListFromPeriodsRange(m_startDate.Text, m_endDate.Text)
                    Dim l_periods = Period.GetDaysPeriodsListFromWeeksId(l_weeksIdList)
                    m_inputReportCreationController.InputReportPaneCallBack_ReportCreation(My.Settings.processId, l_periods)
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

#Region "Datepickers Events"

    Private Sub m_startDate_ValueChanged(sender As Object, e As EventArgs) Handles m_startDate.ValueChanged
        GeneralUtilities.FillWeekTextBox(m_startWeekTB, m_startDate.DateTimeEditor.Value.Value)
    End Sub

    Private Sub m_endDate_ValueChanged(sender As Object, e As EventArgs) Handles m_endDate.ValueChanged
        GeneralUtilities.FillWeekTextBox(m_endWeekTB, m_endDate.DateTimeEditor.Value.Value)
    End Sub

#End Region

#End Region

#Region "Utilities"

    Private Function AddItemToCombobox(ByRef p_id As String, ByRef p_name As String) As ListItem

        Dim l_item As New ListItem
        l_item.Value = p_id
        l_item.Text = p_name
        m_accountSelectionComboBox.Items.Add(l_item)
        Return l_item

    End Function

    Private Sub PeriodsControlDisplay(ByRef p_visible As Boolean)

        m_startDate.Visible = p_visible
        m_endDate.Visible = p_visible
        m_startDateLabel.Visible = p_visible
        m_endDateLabel.Visible = p_visible
        m_startWeekTB.Visible = p_visible
        m_endWeekTB.Visible = p_visible
   
    End Sub

#End Region

End Class
