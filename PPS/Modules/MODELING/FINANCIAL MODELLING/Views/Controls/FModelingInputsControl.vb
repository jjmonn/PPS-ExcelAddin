' FModelingInputsControl.vb
'
' View managing inputs for the Financial Modeling
'
'
'
'
' Author: Julien Monnereau
' Last modified: 27/05/2015


Imports System.Windows.Forms
Imports VIBlend.WinForms.DataGridView



Friend Class FModelingInputsControl


#Region "Instance Variables"

    ' Objects
    Private controller As FModelingInputsController
    Friend PBar As New ProgressBarControl

    ' Variables
    Private VersionsTV As TreeView
    Private EntitiesTV As TreeView

#End Region


#Region "Initialization"

    Friend Sub New(ByRef controller As FModelingInputsController)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.controller = controller
        Me.Controls.Add(PBar)                           ' Progress Bar
        PBar.Left = (Me.Width - PBar.Width) / 2         ' Progress Bar
        PBar.Top = (Me.Height - PBar.Height) / 2        ' Progress Bar

    End Sub

    Friend Sub AddInputsTabElement(ByRef input_entitiesTV As TreeView, _
                                   ByRef input_versionsTV As TreeView)

        EntitiesTVPanel.Controls.Add(input_entitiesTV)
        VersionsTVpanel.Controls.Add(input_versionsTV)
        input_entitiesTV.Dock = DockStyle.Fill
        input_versionsTV.Dock = DockStyle.Fill

        VersionsTV = input_versionsTV
        EntitiesTV = input_entitiesTV
        TreeViewsUtilities.CheckAllNodes(EntitiesTV)
        EntitiesTV.ImageList = EntitiesTVImageList
        VersionsTV.ImageList = VersionsTVIcons
        EntitiesTV.CollapseAll()
        VersionsTV.CollapseAll()

        AddHandler VersionsTV.AfterSelect, AddressOf VersionsTV_AfterSelect
        AddHandler EntitiesTV.AfterSelect, AddressOf EntitiesTV_AfterSelect
        AddHandler VersionsTV.KeyDown, AddressOf versionsTV_KeyDown
        AddHandler EntitiesTV.KeyDown, AddressOf entitiesTV_KeyDown

    End Sub

#End Region


#Region "Call Backs"

    Private Sub LaunchConsoBT_Click(sender As Object, e As EventArgs) Handles LaunchConsoBT.Click

        If Not EntitiesTV.SelectedNode Is Nothing Then
            If Not VersionsTV.SelectedNode Is Nothing Then
                If controller.IsVersionValid(VersionsTV.SelectedNode.Name) = False Then
                    MsgBox("The Version currently selected is a folder. Please select a Version.")
                    Exit Sub
                End If
                controller.ComputeEntity(VersionsTV.SelectedNode, EntitiesTV.SelectedNode)
                ValidateInputs()
            Else
                MsgBox("A Version must be selected.")
            End If
        Else
            MsgBox("An entity must be selected.")
        End If

    End Sub

    Private Sub ValidateInputs()

        If Not controller.periods_list Is Nothing Then
            If controller.IsMappingComplete() = False Then
                Dim confirm As Integer = MessageBox.Show("All the inputs are not mapped. If you validate the Inputs now some of them will be initialized to zero. Do you want to validate?", _
                                                         "Inputs Validation", _
                                                           MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)
                If confirm = DialogResult.Yes Then
                    controller.sendInputsToSimulationController()
                End If
            Else
                controller.sendInputsToSimulationController()
            End If
        Else
            MsgBox("A consolidation must be launched first.")
        End If
    End Sub

#End Region


#Region "Events"

    Private Sub VersionsTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        If controller.IsVersionValid(e.Node.Name) Then VersionTB.Text = e.Node.Text

    End Sub

    Private Sub EntitiesTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        EntityTB.Text = e.Node.Text

    End Sub

    Private Sub versionsTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter
                EntitiesTV.Select()
                If Not EntitiesTV.Nodes(0) Is Nothing Then EntitiesTV.SelectedNode = EntitiesTV.Nodes(0)
        End Select

    End Sub

    Private Sub entitiesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Enter : LaunchConsoBT.Select()
        End Select

    End Sub

#End Region


#Region "Utilities"



#End Region




   
End Class
