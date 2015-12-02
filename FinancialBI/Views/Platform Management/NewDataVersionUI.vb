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
' Last modified: 24/10/2015


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
    Private Const NB_YEARS_AVAILABLE As Int32 = 5
    Private m_monthDictionary As New SafeDictionary(Of String, ListItem)
    Private m_timeConfigDictionary As New SafeDictionary(Of String, ListItem)

#End Region

#Region "Initialize"

    Friend Sub New(ByRef input_controller As DataVersionsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = input_controller
        StartingPeriodNUD.Value = Year(Now)
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
        m_startingMonthLabel.Text = Local.GetValue("facts_versions.starting_month")
        m_nbYearLabel.Text = Local.GetValue("facts_versions.nb_years")
        m_rateVersionLabel.Text = Local.GetValue("facts_versions.exchange_rates_version")
        m_factVersionLabel.Text = Local.GetValue("facts_versions.global_facts_version")
        CancelBT.Text = Local.GetValue("general.cancel")
        CreateVersionBT.Text = Local.GetValue("general.create")
        Me.Text = Local.GetValue("facts_versions.version_new")
    End Sub

    Private Sub InitializeComboboxes()

        m_timeConfigDictionary.Clear()

        Dim timeConfigMonth As New ListItem()
        timeConfigMonth.Text = Local.GetValue("period.timeconfig.month")
        timeConfigMonth.Value = TimeConfig.MONTHS

        Dim timeConfigYear As New ListItem()
        timeConfigYear.Text = Local.GetValue("period.timeconfig.year")
        timeConfigYear.Value = TimeConfig.YEARS

        m_timeConfigDictionary.Add(timeConfigYear.Text, TimeConfigCB.Items.Add(timeConfigYear))
        m_timeConfigDictionary.Add(timeConfigMonth.Text, TimeConfigCB.Items.Add(timeConfigMonth))

        Dim current_year As Int32 = Year(Now)
        StartingPeriodNUD.Value = current_year - 1
        StartingPeriodNUD.Increment = 1
        NbPeriodsNUD.Increment = 1

        m_monthDictionary.Clear()
        For i As Int16 = 1 To 12
            Dim monthItem As New ListItem()

            monthItem.Value = i
            monthItem.Text = Local.GetValue("period.month" + i.ToString())
            m_monthDictionary.Add(monthItem.Text, m_startingMonthCombobox.Items.Add(monthItem))
        Next

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

        Dim name As String = NameTB.Text
        If IsFormValid(name) = True Then
            Dim newVersion As New Version

            newVersion.Name = name
            newVersion.CreatedAt = Format(Now, "short Date")
            newVersion.IsFolder = False
            newVersion.Locked = False
            newVersion.LockDate = "NA"
            newVersion.TimeConfiguration = TimeConfigCB.SelectedItem.Value
            Dim l_startingMonth As UInt32 = m_startingMonthCombobox.SelectedItem.Value
            newVersion.StartPeriod = DateSerial(StartingPeriodNUD.Value, l_startingMonth, 31).ToOADate()
            newVersion.NbPeriod = NbPeriodsNUD.Text
            newVersion.RateVersionId = m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value
            newVersion.GlobalFactVersionId = m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value()
            If Not m_parentNode Is Nothing Then newVersion.ParentId = m_parentNode.Value

            If m_copyCheckBox.Checked = True AndAlso _
            m_originVersionNode IsNot Nothing AndAlso _
            m_controller.IsFolder(m_originVersionNode.Value) = False Then
                Dim id As UInt32 = m_originVersionNode.Value

                m_controller.CopyVersion(id, newVersion)
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

    Private Sub m_copyCheckbox_CheckedChanged(sender As Object, e As EventArgs) Handles m_copyCheckBox.CheckedChanged

        m_parentVersionsTreeviewBox.Enabled = m_copyCheckBox.Checked
        StartingPeriodNUD.Enabled = Not m_copyCheckBox.Checked
        TimeConfigCB.Enabled = Not m_copyCheckBox.Checked

    End Sub

    Private Sub m_versionsTreeviewBox_Click(sender As Object, e As EventArgs) Handles m_parentVersionsTreeviewBox.TextChanged
        m_originVersionNode = Nothing
        If m_copyCheckBox.Checked = True AndAlso m_parentVersionsTreeviewBox.TreeView.SelectedNode IsNot Nothing AndAlso _
        m_controller.IsFolder(m_parentVersionsTreeviewBox.TreeView.SelectedNode.Value) = False Then

            Dim version As Version = GlobalVariables.Versions.GetValue(CUInt(m_parentVersionsTreeviewBox.TreeView.SelectedNode.Value))
            If version Is Nothing Then Exit Sub

            m_originVersionNode = m_parentVersionsTreeviewBox.TreeView.SelectedNode
            TimeConfigCB.SelectedItem = m_timeConfigDictionary(
                If(version.TimeConfiguration = TimeConfig.MONTHS, Local.GetValue("period.timeconfig.month"), Local.GetValue("period.timeconfig.year")))
            StartingPeriodNUD.Value = Year(Date.FromOADate(version.StartPeriod))
            NbPeriodsNUD.Value = version.NbPeriod
            Dim selectedMonth As Integer = Month(Date.FromOADate(version.StartPeriod))
            Dim selectedString As String = Local.GetValue("period.month" + selectedMonth.ToString())
            Dim selectedItem As ListItem = m_monthDictionary(selectedString)
            m_startingMonthCombobox.SelectedItem = selectedItem

        End If
    End Sub

    Private Sub TimeConfigCB_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TimeConfigCB.SelectedIndexChanged
        If TimeConfigCB.SelectedItem Is Nothing Then Exit Sub
        m_startingMonthCombobox.Enabled = (TimeConfigCB.SelectedItem.Value = TimeConfig.MONTHS)

        If m_startingMonthCombobox.Enabled = False Then
            Dim monthString As String = Local.GetValue("period.month12")

            If m_monthDictionary.ContainsKey(monthString) Then
                m_startingMonthCombobox.SelectedItem = m_monthDictionary(monthString)
            End If
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

        If m_startingMonthCombobox.SelectedItem Is Nothing Then
            MsgBox(Local.GetValue("facts_versions.msg_need_starting_month"))
            Return False
        End If
        If m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode Is Nothing OrElse _
            m_controller.IsRatesVersionValid(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_cannot_use_exchange_rates_folder"))
            Return False
        End If

        If m_factsVersionVTreeviewbox.TreeView.SelectedNode Is Nothing OrElse _
            m_controller.IsFactsVersionValid(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_cannot_use_global_fact_folder"))
            Return False
        End If

        If m_controller.IsRatesVersionCompatibleWithPeriods(DateSerial(StartingPeriodNUD.Value, m_startingMonthCombobox.SelectedItem.Value, 31).ToOADate(), NbPeriodsNUD.Value, m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox(Local.GetValue("facts_versions.msg_rates_version_mismatch"))
            Return False
        End If

        If m_controller.IsFactVersionCompatibleWithPeriods(DateSerial(StartingPeriodNUD.Value, m_startingMonthCombobox.SelectedItem.Value, 31).ToOADate(), NbPeriodsNUD.Value, m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
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