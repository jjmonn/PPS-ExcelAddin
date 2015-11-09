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
' Last modified: 09/11/2015


Imports System.Windows.Forms
Imports System.Collections
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class NewDataVersionUI


#Region "Instance Variables"

    ' Objects
    Private m_controller As DataVersionsController

    ' Variables
    Private m_originVersionNode As vTreeNode
    Private m_parentNode As vTreeNode
    Private Const NB_YEARS_AVAILABLE As Int32 = 5

#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_controller As DataVersionsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = input_controller
        StartingPeriodNUD.Value = Year(Now)
        InitializeComboboxes()
        MultilanguageSetup()

    End Sub

    Private Sub InitializeComboboxes()

        TimeConfigCB.Items.Add(MONTHLY_TIME_CONFIGURATION)
        TimeConfigCB.Items.Add(YEARLY_TIME_CONFIGURATION)

        Dim current_year As Int32 = Year(Now)
        StartingPeriodNUD.Value = current_year - 1
        StartingPeriodNUD.Increment = 1
        NbPeriodsNUD.Increment = 1

        GlobalVariables.Versions.LoadVersionsTV(m_versionsTreeviewBox.TreeView)
        VTreeViewUtil.InitTVFormat(m_versionsTreeviewBox.TreeView)

        VTreeViewUtil.LoadTreeview(m_exchangeRatesVersionVTreeviewbox.TreeView, GlobalVariables.RatesVersions.GetDictionary())
        VTreeViewUtil.LoadTreeview(m_factsVersionVTreeviewbox.TreeView, GlobalVariables.GlobalFactsVersions.GetDictionary())

    End Sub

    Private Sub MultilanguageSetup()

        Me.m_factsVersionLabel.Text = Local.GetValue("facts_versions.fact_version")
        Me.m_ratesVersionLabel.Text = Local.GetValue("facts_versions.exchange_rates_version")
        Me.m_numberOfYearsLabel.Text = Local.GetValue("facts_versions.nb_years")
        Me.m_startingYearLabel.Text = Local.GetValue("facts_versions.starting_period")
        Me.m_periodConfigLabel.Text = Local.GetValue("facts_versions.period_config")
        Me.m_nameLabel.Text = Local.GetValue("facts_versions.version_name")
        Me.CancelBT.Text = Local.GetValue("general.cancel")
        Me.CreateVersionBT.Text = Local.GetValue("general.create")
        Me.Text = Local.GetValue("facts_versions.version_new")

    End Sub


#End Region


#Region "Interface"

    Friend Sub PreFill(Optional ByRef input_parent_node As vTreeNode = Nothing)

        m_parentNode = input_parent_node
        m_originVersionNode = Nothing
        m_versionsTreeviewBox.TreeView.SelectedNode = Nothing
        m_versionsTreeviewBox.Text = ""
        NameTB.Select()

    End Sub

#End Region


#Region "Call Backs"

    Private Sub CreateBT_Click(sender As Object, e As EventArgs) Handles CreateVersionBT.Click

        Dim name As String = NameTB.Text
        If IsFormValid(name) = True Then
            Dim newVersion As New Version

            newVersion.Name = name
            newVersion.CreatedAt = Format(Now, "short Date")
            newVersion.IsFolder = False
            newVersion.Locked = False
            newVersion.LockDate = "NA"
            Dim l_timeConfig As TimeConfig
            If TimeConfigCB.Text = MONTHLY_TIME_CONFIGURATION Then l_timeConfig = TimeConfig.MONTHS Else l_timeConfig = TimeConfig.YEARS
            newVersion.TimeConfiguration = l_timeConfig
            newVersion.StartPeriod = DateSerial(StartingPeriodNUD.Value, 12, 31).ToOADate()
            newVersion.NbPeriod = NbPeriodsNUD.Text
            newVersion.RateVersionId = m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value
            newVersion.GlobalFactVersionId = m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value()
            If Not m_parentNode Is Nothing Then newVersion.ParentId = m_parentNode.Value

            If m_copyCheckBox.Checked = True AndAlso _
            m_versionsTreeviewBox.TreeView.SelectedNode IsNot Nothing AndAlso _
            m_controller.IsFolder(m_originVersionNode.Value) = False Then
                Dim id As UInt32 = m_originVersionNode.Value
                Dim version As Version = GlobalVariables.Versions.GetValue(id)
                If version Is Nothing Then Exit Sub

                newVersion.TimeConfiguration = version.TimeConfiguration
                newVersion.StartPeriod = version.StartPeriod
                newVersion.NbPeriod = version.NbPeriod
                m_controller.CreateVersion(newVersion, m_originVersionNode.Value)
            Else
                m_controller.CreateVersion(newVersion)
            End If
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()
        m_controller.ShowVersionsMGT()

    End Sub

#End Region


#Region "Events"

  

    Private Sub m_copyCheckbox_CheckedChanged(sender As Object, e As EventArgs)

        If m_copyCheckBox.Checked = True AndAlso m_versionsTreeviewBox.TreeView.SelectedNode IsNot Nothing AndAlso _
        m_controller.IsFolder(m_originVersionNode.Value) = False Then

            Dim version As Version = GlobalVariables.Versions.GetValue(m_originVersionNode.Value)
            If version Is Nothing Then Exit Sub

            TimeConfigCB.SelectedText = version.TimeConfiguration
            StartingPeriodNUD.Value = Year(Date.FromOADate(version.StartPeriod))
            NbPeriodsNUD.Value = version.NbPeriod

        End If

    End Sub

#End Region


#Region "Utilities"

    Private Function IsFormValid(ByRef name As String) As Boolean

        If m_controller.IsNameValid(name) = False Then
            ' Pourquoi ne pas ajouter le check duplicate names ici ?! priority normal
            MsgBox("The name is not valid. The name must not exceed " & NAMES_MAX_LENGTH & " characters.")
            Return False
        End If

        If TimeConfigCB.Text = "" Then
            MsgBox(Local.GetValue("facts_versions.msg_config_selection"))
            Return False
        End If

        If StartingPeriodNUD.Text = "" Then
            MsgBox(Local.GetValue("facts_versions.msg_starting_period"))
            Return False
        End If

        If m_controller.IsRatesVersionValid(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_cannot_use_exchange_rates_folder"))
            Return False
        End If

        If m_controller.IsFactsVersionValid(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_cannot_use_global_fact_folder"))
            Return False
        End If

        If m_controller.IsRatesVersionCompatibleWithPeriods(DateSerial(StartingPeriodNUD.Value, 12, 31).ToOADate(), NbPeriodsNUD.Value, m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox(Local.GetValue("facts_versions.msg_rates_version_mismatch"))
            Return False
        End If

        If m_controller.IsFactVersionCompatibleWithPeriods(DateSerial(StartingPeriodNUD.Value, 12, 31).ToOADate(), NbPeriodsNUD.Value, m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox(Local.GetValue("facts_versions.msg_fact_version_mismatch"))
            Return False
        End If

        Return True

    End Function

    Private Sub NewDataVersionUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()
        m_controller.ShowVersionsMGT()

    End Sub

   

#End Region

End Class