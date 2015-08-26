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

        InitializeCBs()
        Panel1.Controls.Add(versionsTV)
        versionsTV.Dock = DockStyle.Fill
        AddHandler versionsTV.MouseClick, AddressOf versionsTV_NodeMouseClick
        AddHandler versionsTV.KeyDown, AddressOf versionsTV_KeyDown

    End Sub

    Private Sub InitializeCBs()

        TimeConfigCB.Items.Add(MONTHLY_TIME_CONFIGURATION)
        TimeConfigCB.Items.Add(YEARLY_TIME_CONFIGURATION)

        Dim current_year As Int32 = Year(Now)
        StartingPeriodNUD.Value = current_year - 1
        StartingPeriodNUD.Increment = 1
        NbPeriodsNUD.Increment = 1

        For Each name_ In Controller.rates_versions_name_id_dic.Keys
            RatesVersionCB.Items.Add(name_)
        Next

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
            Dim hash As New Hashtable
            hash.Add(NAME_VARIABLE, name)
            hash.Add(VERSIONS_CREATION_DATE_VARIABLE, Format(Now, "short Date"))
            hash.Add(IS_FOLDER_VARIABLE, 0)
            hash.Add(VERSIONS_LOCKED_VARIABLE, 0)
            hash.Add(VERSIONS_TIME_CONFIG_VARIABLE, TimeConfigCB.Text)
            hash.Add(VERSIONS_START_PERIOD_VAR, StartingPeriodNUD.Text)
            hash.Add(VERSIONS_NB_PERIODS_VAR, NbPeriodsNUD.Text)
            hash.Add(VERSIONS_RATES_VERSION_ID_VAR, Controller.rates_versions_name_id_dic(RatesVersionCB.Text))
            If Not parent_node Is Nothing Then hash.Add(PARENT_ID_VARIABLE, parent_node.Value)

            If CreateCopyBT.Checked = True AndAlso _
            ReferenceTB.Text <> "" AndAlso _
            reference_node.Text = ReferenceTB.Text AndAlso
            Controller.IsFolder(reference_node.Value) = False Then
                Dim id As UInt32 = reference_node.Value
                hash(VERSIONS_TIME_CONFIG_VARIABLE) = GlobalVariables.Versions.versions_hash(id)(VERSIONS_TIME_CONFIG_VARIABLE)
                hash(VERSIONS_START_PERIOD_VAR) = GlobalVariables.Versions.versions_hash(id)(VERSIONS_START_PERIOD_VAR)
                hash(VERSIONS_NB_PERIODS_VAR) = GlobalVariables.Versions.versions_hash(id)(VERSIONS_NB_PERIODS_VAR)
                Controller.CreateVersion(hash, reference_node.Value)
            Else
                Controller.CreateVersion(hash)
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

    Private Sub versionsTV_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs)

        Select Case e.Node.Nodes.Count
            Case 0
                ReferenceTB.Text = e.Node.Text
                reference_node = versionsTV.HitTest(e.Location)
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

            TimeConfigCB.SelectedText = GlobalVariables.Versions.versions_hash(reference_node.Value)(VERSIONS_TIME_CONFIG_VARIABLE)
            StartingPeriodNUD.Value = GlobalVariables.Versions.versions_hash(reference_node.Value)(VERSIONS_START_PERIOD_VAR)
            NbPeriodsNUD = GlobalVariables.Versions.versions_hash(reference_node.Value)(VERSIONS_NB_PERIODS_VAR)

        End If

    End Sub

#End Region


#Region "Utilities"

    Private Function IsFormValid(ByRef name As String) As Boolean

        If Controller.IsNameValid(name) Then
            If TimeConfigCB.Text <> "" Then
                If TimeConfigCB.Text = MONTHLY_TIME_CONFIGURATION Then
                    If StartingPeriodNUD.Text <> "" Then
                        If Controller.IsRatesVersionValid(StartingPeriodNUD.Value, NbPeriodsNUD.Value, Controller.rates_versions_name_id_dic(RatesVersionCB.Text)) = True Then
                            Return True
                        Else
                            MsgBox("This Exchange Rates Version is not compatible with the Periods Configuration.")
                        End If
                    Else
                        MsgBox("The Reference Year must be selected for monthly Time Configuration Versions.")
                        Return False
                    End If
                Else
                    Return True
                End If
            Else
                MsgBox("The Time Configuration must be selected.")
                Return False
            End If
        Else
            MsgBox("The name is not valid. The name must not exceed " & VERSIONS_TOKEN_SIZE & " characters.")
            Return False
        End If


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