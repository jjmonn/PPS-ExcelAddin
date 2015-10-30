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
' Last modified: 25/08/2015


Imports System.Windows.Forms
Imports System.Collections
Imports VIBlend.WinForms.Controls
Imports CRUD

Friend Class NewDataVersionUI


#Region "Instance Variables"

    ' Objects
    Private Controller As DataVersionsController

    ' Variables
    Private versionsTV As New vTreeView
    Private isFormExpanded As Boolean
    Private reference_node As vTreeNode
    Private parent_node As vTreeNode

    ' Expansion Display Constants
    Private Const COLLAPSED_WIDTH As Int32 = 660
    Private Const EXPANDED_WIDTH As Int32 = 1120
    Private Const COLLAPSED_HEIGHT As Int32 = 390
    Private Const EXPANDED_HEIGHT As Int32 = 510
    Private Const NB_YEARS_AVAILABLE As Int32 = 5

#End Region


#Region "Initialize"

    Friend Sub New(ByRef input_controller As DataVersionsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        GlobalVariables.Versions.LoadVersionsTV(versionsTV)
        StartingPeriodNUD.Value = Year(Now)

        InitializeCBs()
        Panel1.Controls.Add(versionsTV)
        versionsTV.Dock = DockStyle.Fill
        AddHandler versionsTV.NodeMouseDown, AddressOf versionsTV_NodeMouseClick
        AddHandler versionsTV.KeyDown, AddressOf versionsTV_KeyDown

    End Sub

    Private Sub InitializeCBs()

        TimeConfigCB.Items.Add(MONTHLY_TIME_CONFIGURATION)
        TimeConfigCB.Items.Add(YEARLY_TIME_CONFIGURATION)

        Dim current_year As Int32 = Year(Now)
        StartingPeriodNUD.Value = current_year - 1
        StartingPeriodNUD.Increment = 1
        NbPeriodsNUD.Increment = 1

        VTreeViewUtil.LoadTreeview(m_exchangeRatesVersionVTreeviewbox.TreeView, GlobalVariables.RatesVersions.GetDictionary())
        VTreeViewUtil.LoadTreeview(m_factsVersionVTreeviewbox.TreeView, GlobalVariables.GlobalFactsVersions.GetDictionary())

    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub PreFill(Optional ByRef input_parent_node As vTreeNode = Nothing)

        parent_node = input_parent_node
        reference_node = Nothing
        ReferenceTB.Text = ""
        NameTB.Select()
        HideVersionsTV()

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
            If Not parent_node Is Nothing Then newVersion.ParentId = parent_node.Value

            If CreateCopyBT.Checked = True AndAlso _
            ReferenceTB.Text <> "" AndAlso _
            reference_node.Text = ReferenceTB.Text AndAlso
            Controller.IsFolder(reference_node.Value) = False Then
                Dim id As UInt32 = reference_node.Value
                Dim version As Version = GlobalVariables.Versions.GetValue(id)
                If version Is Nothing Then Exit Sub

                newVersion.TimeConfiguration = version.TimeConfiguration
                newVersion.StartPeriod = version.StartPeriod
                newVersion.NbPeriod = version.NbPeriod
                Controller.CreateVersion(newVersion, reference_node.Value)
            Else
                Controller.CreateVersion(newVersion)
            End If
        End If

    End Sub

    Private Sub CancelBT_Click(sender As Object, e As EventArgs) Handles CancelBT.Click

        Me.Hide()
        Controller.ShowVersionsMGT()

    End Sub

#End Region


#Region "Events"

    Private Sub ReferenceTB_Enter(sender As Object, e As EventArgs) Handles ReferenceTB.Enter

        DisplayVersionsTV()

    End Sub

    Private Sub versionsTV_NodeMouseClick(sender As Object, e As vTreeViewMouseEventArgs)

        Select Case e.Node.Nodes.Count
            Case 0
                ReferenceTB.Text = e.Node.Text
                reference_node = versionsTV.HitTest(e.MouseEventArgs.Location)
                HideVersionsTV()
        End Select

    End Sub

    Private Sub versionsTV_KeyDown(sender As Object, e As KeyEventArgs)

        If e.KeyCode = Keys.Enter _
        AndAlso Not versionsTV.SelectedNode Is Nothing Then
            ReferenceTB.Text = versionsTV.SelectedNode.Text
            reference_node = versionsTV.SelectedNode
            HideVersionsTV()
        End If

    End Sub

    Private Sub CreateCopyBT_CheckedChanged(sender As Object, e As EventArgs) Handles CreateCopyBT.CheckedChanged

        If CreateCopyBT.Checked = True AndAlso ReferenceTB.Text <> "" AndAlso _
        reference_node.Text = ReferenceTB.Text AndAlso
        Controller.IsFolder(reference_node.Value) = False Then

            Dim version As Version = GlobalVariables.Versions.GetValue(reference_node.Value)
            If version Is Nothing Then Exit Sub

            TimeConfigCB.SelectedText = version.TimeConfiguration
            StartingPeriodNUD.Value = Year(Date.FromOADate(version.StartPeriod))
            NbPeriodsNUD.Value = version.NbPeriod

        End If

    End Sub

#End Region


#Region "Utilities"

    Private Function IsFormValid(ByRef name As String) As Boolean

        If Controller.IsNameValid(name) = False Then
            ' Pourquoi ne pas ajouter le check duplicate names ici ?! priority normal
            MsgBox("The name is not valid. The name must not exceed " & NAMES_MAX_LENGTH & " characters.")
            Return False
        End If

        If TimeConfigCB.Text = "" Then
            MsgBox("The Time Configuration must be selected.")
            Return False
        End If

        If StartingPeriodNUD.Text = "" Then
            MsgBox("The Reference Year must be selected for monthly Time Configuration Versions.")
            Return False
        End If

        If Controller.IsRatesVersionValid(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Text & " is a folder and connot be set as the Exchange Rates version.")
            Return False
        End If

        If Controller.IsFactsVersionValid(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Text & "is a folder and cannot be set as the Economic Indicators version.")
            Return False
        End If

        If Controller.IsRatesVersionCompatibleWithPeriods(DateSerial(StartingPeriodNUD.Value, 12, 31).ToOADate(), NbPeriodsNUD.Value, m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox("This Exchange Rates Version is not compatible with the Periods Configuration.")
            Return False
        End If

        If Controller.IsFactVersionCompatibleWithPeriods(DateSerial(StartingPeriodNUD.Value, 12, 31).ToOADate(), NbPeriodsNUD.Value, m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value) = False Then
            MsgBox("This Fact Version is not compatible with the Periods Configuration.")
            Return False
        End If

        Return True

    End Function

    Private Sub NewDataVersionUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        e.Cancel = True
        Me.Hide()
        Controller.ShowVersionsMGT()

    End Sub

    Private Sub DisplayVersionsTV()

        Me.Width = EXPANDED_WIDTH
        Me.Height = EXPANDED_HEIGHT
        isFormExpanded = True

    End Sub

    Protected Friend Sub HideVersionsTV()

        Me.Width = COLLAPSED_WIDTH
        Me.Height = COLLAPSED_HEIGHT
        isFormExpanded = False

    End Sub

#End Region

End Class