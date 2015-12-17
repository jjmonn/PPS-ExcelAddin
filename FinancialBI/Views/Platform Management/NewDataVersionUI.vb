'
'
'
'
'
'
'
'
'
' Author: Julien Monnereau
' Last modified: 11/12/2015


Imports System.Windows.Forms
Imports System.Collections
Imports VIBlend.WinForms.Controls
Imports CRUD
Imports System.Collections.Generic

Friend Class NewDataVersionUI

#Region "Instance Variables"

    ' Objects
    Private m_controller As DataVersionsController

    ' Variables
    Private m_originVersionNode As vTreeNode
    Private m_parentNode As vTreeNode
    Private m_timeConfigDayItem As New ListItem()
    Private m_timeConfigWeekItem As New ListItem()
    Private m_timeConfigMonthItem As New ListItem()
    Private m_timeConfigYearItem As New ListItem()


#End Region

#Region "Initialize"

    Friend Sub New(ByRef input_controller As DataVersionsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = input_controller
        GlobalVariables.Versions.LoadVersionsTV(m_parentVersionsTreeviewBox.TreeView)
        VTreeViewUtil.InitTVFormat(m_parentVersionsTreeviewBox.TreeView)
        InitializeComboboxes()
        m_parentVersionsTreeviewBox.Enabled = False
        MultilangueSetup()

    End Sub

    Private Sub MultilangueSetup()
        m_versionNameLabel.Text = Local.GetValue("facts_versions.version_name")
        m_copyCheckBox.Text = Local.GetValue("facts_versions.copy_from")
        m_periodConfigLabel.Text = Local.GetValue("facts_versions.period_config")
        m_startingPeriodLabel.Text = Local.GetValue("facts_versions.starting_period")
        m_nbPeriodsLabel.Text = Local.GetValue("facts_versions.nb_periods")
        m_rateVersionLabel.Text = Local.GetValue("facts_versions.exchange_rates_version")
        m_factVersionLabel.Text = Local.GetValue("facts_versions.global_facts_version")
        CancelBT.Text = Local.GetValue("general.cancel")
        CreateVersionBT.Text = Local.GetValue("general.create")
        Me.Text = Local.GetValue("facts_versions.version_new")
    End Sub

    Private Sub InitializeComboboxes()

        m_timeConfigDayItem.Text = Local.GetValue("period.timeconfig.day")
        m_timeConfigDayItem.Value = TimeConfig.DAYS
        m_timeConfigCB.Items.Add(m_timeConfigDayItem)

        m_timeConfigMonthItem.Text = Local.GetValue("period.timeconfig.month")
        m_timeConfigMonthItem.Value = TimeConfig.MONTHS
        m_timeConfigCB.Items.Add(m_timeConfigMonthItem)

        m_timeConfigYearItem.Text = Local.GetValue("period.timeconfig.year")
        m_timeConfigYearItem.Value = TimeConfig.YEARS
        m_timeConfigCB.Items.Add(m_timeConfigYearItem)

        m_startingPeriodDatePicker.Value = Today
        NbPeriodsNUD.Increment = 1

        VTreeViewUtil.LoadTreeview(m_exchangeRatesVersionVTreeviewbox.TreeView, GlobalVariables.RatesVersions.GetDictionary())
        VTreeViewUtil.LoadTreeview(m_factsVersionVTreeviewbox.TreeView, GlobalVariables.GlobalFactsVersions.GetDictionary())
        VTreeViewUtil.InitTVFormat(m_exchangeRatesVersionVTreeviewbox.TreeView)
        VTreeViewUtil.InitTVFormat(m_factsVersionVTreeviewbox.TreeView)

    End Sub

#End Region

#Region "Interface"

    Friend Sub PreFill(Optional ByRef input_parent_node As vTreeNode = Nothing)

        On Error Resume Next
        m_parentNode = input_parent_node
        m_originVersionNode = Nothing
        m_parentVersionsTreeviewBox.TreeView.SelectedNode = Nothing
        m_parentVersionsTreeviewBox.Text = ""
        NameTB.Text = ""
        NameTB.Select()

    End Sub

    Delegate Sub AfterRead_Delegate(ByRef p_version As Version)
    Friend Sub AfterRead(ByRef p_version As Version)

        If Me.m_parentVersionsTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New AfterRead_Delegate(AddressOf AfterRead)
            Me.m_parentVersionsTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {p_version})
        Else
            On Error GoTo errorHandler
            Dim l_versionNode As vTreeNode = VTreeViewUtil.FindNode(m_parentVersionsTreeviewBox.TreeView, p_version.Id)
            If l_versionNode Is Nothing Then
                If p_version.ParentId <> 0 Then
                    Dim parentNode As VIBlend.WinForms.Controls.vTreeNode = Nothing
                    parentNode = VTreeViewUtil.FindNode(m_parentVersionsTreeviewBox.TreeView, p_version.ParentId)
                    VTreeViewUtil.AddNode(p_version.Id, p_version.Name, parentNode)
                Else
                    VTreeViewUtil.AddNode(p_version.Id, p_version.Name, m_parentVersionsTreeviewBox.TreeView)
                End If
            Else
                l_versionNode.Text = p_version.Name
                l_versionNode.ImageIndex = p_version.Image
            End If
        End If

errorHandler:
        Exit Sub

    End Sub

    Delegate Sub AfterDelete_Delegate(ByRef id As UInt32)
    Friend Sub AfterDelete(ByRef id As UInt32)

        If Me.m_parentVersionsTreeviewBox.TreeView.InvokeRequired Then
            Dim MyDelegate As New AfterDelete_Delegate(AddressOf AfterDelete)
            Me.m_parentVersionsTreeviewBox.TreeView.Invoke(MyDelegate, New Object() {id})
        Else
            On Error GoTo errorHandler
            Dim node As vTreeNode = VTreeViewUtil.FindNode(m_parentVersionsTreeviewBox.TreeView, id)
            If Not node Is Nothing Then
                node.Remove()
            End If
        End If
errorHandler:
        Exit Sub

    End Sub

#End Region

#Region "Call Backs"

    Private Sub CreateBT_Click(sender As Object, e As EventArgs) Handles CreateVersionBT.Click

        Dim l_name As String = NameTB.Text
        If IsFormValid(l_name) = True Then
            Dim l_newVersion As New Version

            l_newVersion.Name = l_name
            l_newVersion.CreatedAt = Format(Now, "short Date")
            l_newVersion.IsFolder = False
            l_newVersion.Locked = False
            l_newVersion.LockDate = "NA"
            l_newVersion.TimeConfiguration = m_timeConfigCB.SelectedItem.Value

            Dim l_startDate As Date = m_startingPeriodDatePicker.Value
            l_newVersion.StartPeriod = GetLastDayOfPeriod(l_newVersion.TimeConfiguration, l_startDate.ToOADate)

            l_newVersion.NbPeriod = NbPeriodsNUD.Text
            l_newVersion.RateVersionId = m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value
            l_newVersion.GlobalFactVersionId = m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value()
            If Not m_parentNode Is Nothing Then l_newVersion.ParentId = m_parentNode.Value

            If m_copyCheckBox.Checked = True AndAlso _
            m_originVersionNode IsNot Nothing AndAlso _
            m_controller.IsFolder(m_originVersionNode.Value) = False Then
                Dim id As UInt32 = m_originVersionNode.Value

                m_controller.CopyVersion(id, l_newVersion)
            Else
                m_controller.CreateVersion(l_newVersion)
            End If
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()
        m_controller.ShowVersionsMGT()

    End Sub

#End Region

#Region "Events"

    Private Sub m_copyCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles m_copyCheckBox.CheckedChanged

        m_parentVersionsTreeviewBox.Enabled = m_copyCheckBox.Checked
        m_startingPeriodDatePicker.Enabled = Not m_copyCheckBox.Checked
        m_timeConfigCB.Enabled = Not m_copyCheckBox.Checked

    End Sub

    Private Sub m_versionsTreeviewBox_Click(sender As Object, e As EventArgs) Handles m_parentVersionsTreeviewBox.TextChanged

        m_originVersionNode = Nothing
        If m_copyCheckBox.Checked = True AndAlso m_parentVersionsTreeviewBox.TreeView.SelectedNode IsNot Nothing AndAlso _
        m_controller.IsFolder(m_parentVersionsTreeviewBox.TreeView.SelectedNode.Value) = False Then

            Dim version As Version = GlobalVariables.Versions.GetValue(CUInt(m_parentVersionsTreeviewBox.TreeView.SelectedNode.Value))
            If version Is Nothing Then Exit Sub

            m_originVersionNode = m_parentVersionsTreeviewBox.TreeView.SelectedNode
            Select Case version.TimeConfiguration
                Case TimeConfig.YEARS : m_timeConfigCB.SelectedItem = m_timeConfigYearItem
                Case TimeConfig.MONTHS : m_timeConfigCB.SelectedItem = m_timeConfigMonthItem
                Case TimeConfig.DAYS : m_timeConfigCB.SelectedItem = m_timeConfigDayItem
            End Select

            m_startingPeriodDatePicker.Value = Date.FromOADate(version.StartPeriod)
            NbPeriodsNUD.Value = version.NbPeriod

        End If

    End Sub

    Private Sub TimeConfigCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles m_timeConfigCB.SelectedValueChanged

        If m_timeConfigCB.SelectedItem Is Nothing Then Exit Sub
        Select Case m_timeConfigCB.SelectedItem.Value
            Case CRUD.TimeConfig.YEARS
                m_nbPeriodsLabel.Text = Local.GetValue("facts_versions.nb_years")
                m_startingPeriodDatePicker.FormatValue = "yyyy"
            Case CRUD.TimeConfig.MONTHS
                m_nbPeriodsLabel.Text = Local.GetValue("facts_versions.nb_months")
                m_startingPeriodDatePicker.FormatValue = "MMM. yyyy"
            Case CRUD.TimeConfig.DAYS
                m_nbPeriodsLabel.Text = Local.GetValue("facts_versions.nb_days")
                m_startingPeriodDatePicker.FormatValue = "dd MMM. yyyy"
        End Select
        m_startingPeriodDatePicker.Refresh()
    End Sub

#End Region

#Region "Utilities"

    Private Function IsFormValid(ByRef name As String) As Boolean

        If m_controller.IsNameValid(name) = False Then
            ' Pourquoi ne pas ajouter le check duplicate names ici ?! priority normal
            MsgBox("The name is not valid. The name must not exceed " & NAMES_MAX_LENGTH & " characters.")
            Return False
        End If

        If m_timeConfigCB.Text = "" Then
            MsgBox(Local.GetValue("facts_versions.msg_config_selection"))
            Return False
        End If

        If m_startingPeriodDatePicker.Text = "" _
        Or IsDate(m_startingPeriodDatePicker.Value) = False Then
            MsgBox(Local.GetValue("facts_versions.msg_starting_period"))
            Return False
        End If

        ' Check exchange rates and global facts selection
        If m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode IsNot Nothing Then
            If m_controller.IsRatesVersionValid(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
                MsgBox(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_cannot_use_exchange_rates_folder"))
                Return False
            End If
        Else
            MsgBox(Local.GetValue("facts_versions.msg_select_rates_version"))
        End If

        If m_factsVersionVTreeviewbox.TreeView.SelectedNode IsNot Nothing Then
            If m_controller.IsFactsVersionValid(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
                MsgBox(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_cannot_use_global_fact_folder"))
                Return False
            End If
        Else
            MsgBox(Local.GetValue("facts_versions.msg_select_global_facts_version"))
        End If

        ' Check exchange rates and global facts validity
        Select Case m_timeConfigCB.SelectedValue

            Case CRUD.TimeConfig.YEARS, CRUD.TimeConfig.MONTHS
                Dim l_startDate As Date = m_startingPeriodDatePicker.Value
                Dim l_startPeriodCheck = GetLastDayOfPeriod(m_timeConfigCB.SelectedValue, l_startDate.ToOADate)

                If m_controller.IsRatesVersionCompatibleWithPeriods(l_startPeriodCheck, NbPeriodsNUD.Value, m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
                    MsgBox(Local.GetValue("facts_versions.msg_rates_version_mismatch"))
                    Return False
                End If

                If m_controller.IsFactVersionCompatibleWithPeriods(l_startPeriodCheck, NbPeriodsNUD.Value, m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
                    MsgBox(Local.GetValue("facts_versions.msg_fact_version_mismatch"))
                    Return False
                End If

            Case CRUD.TimeConfig.DAYS
                ' not checked so far

        End Select
        Return True

    End Function

    Private Sub NewDataVersionUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()
        m_controller.ShowVersionsMGT()

    End Sub

    Private Function GetLastDayOfPeriod(ByRef p_timeConfig As TimeConfig, _
                                        ByRef p_startPeriod As Int32) As Int32

        Dim l_startDate As Date = m_startingPeriodDatePicker.Value
        Select Case p_timeConfig
            Case TimeConfig.YEARS : Return Period.GetYearIdFromPeriodId(l_startDate.ToOADate)
            Case TimeConfig.MONTHS : Return Period.GetMonthIdFromPeriodId(l_startDate.ToOADate)
            Case TimeConfig.WEEK : Return Period.GetWeekIdFromPeriodId(l_startDate.ToOADate)
            Case TimeConfig.DAYS : Return l_startDate.ToOADate()
        End Select
        System.Diagnostics.Debug.WriteLine("NewDataVersionUI - GetLastDayOfPeriod() - undefined timeconfig: " & p_timeConfig)
        Return 0

    End Function

#End Region

End Class