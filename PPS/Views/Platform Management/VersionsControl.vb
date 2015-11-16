' VersioningManagementUI.vb
'
' Allow the user to manage the versions: add, copy, create/ delete..
' Displays versions attributes
'
' to do : 
'       - copy version folder selection process to be reviewed
'       - drag and drop ?
'       - Merge the two "Delete" Buttons ?
'       - old version process selection (think about the possibility to select in another way)
'       - all or nothing needed in the creation / deletion of the versions
'       - all versions must be in a folder
'
'
' Known bugs:
'       
'
' Last modified: 11/11/2015
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Collections
Imports System.Drawing
Imports VIBlend.WinForms.Controls
Imports CRUD
'Imports VIBlend.WinForms.Controls



Friend Class VersionsControl


#Region "Instance variables"

    ' Objects
    Private m_controller As DataVersionsController
    Private m_versionsTV As VIBlend.WinForms.Controls.vTreeView
    Private m_circularProgress As CircularProgressUI

    ' Variables
    Private m_currentNode As VIBlend.WinForms.Controls.vTreeNode
    Private m_creationFlag As String
    Private m_isDisplaying As Boolean

#End Region


#Region "Initialization"

    Friend Sub New(ByRef input_controller As DataVersionsController, _
                   ByRef input_versionsTV As VIBlend.WinForms.Controls.vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_controller = input_controller
        m_versionsTV = input_versionsTV
        m_versionsTV.ImageList = m_versionsTreeviewImageList
        VersionsTVPanel.Controls.Add(m_versionsTV)
        m_versionsTV.Dock = DockStyle.Fill
        m_versionsTV.AllowDrop = True
        m_versionsTV.ContextMenuStrip = RCM_TV

        AddHandler m_versionsTV.AfterSelect, AddressOf VersionsTV_AfterSelect
        AddHandler m_versionsTV.KeyDown, AddressOf CategoriesTV_KeyDown
        AddHandler m_versionsTV.MouseClick, AddressOf tv_mouse_click

        ' implement tv drag and drop -> common for all

        '   AddHandler VersionsTV.drag, AddressOf versionsTV_ItemDrag
        AddHandler m_versionsTV.DragEnter, AddressOf versionsTV_DragEnter
        AddHandler m_versionsTV.DragOver, AddressOf versionsTV_DragOver
        AddHandler m_versionsTV.DragDrop, AddressOf versionsTV_DragDrop

        AddHandler m_exchangeRatesVersionVTreeviewbox.TreeView.AfterSelect, AddressOf ExchangeRatesVTreebox_TextChanged
        AddHandler m_factsVersionVTreeviewbox.TreeView.AfterSelect, AddressOf FactsRatesVTreebox_TextChanged

        VTreeViewUtil.LoadTreeview(m_exchangeRatesVersionVTreeviewbox.TreeView, GlobalVariables.RatesVersions.GetDictionary())
        VTreeViewUtil.LoadTreeview(m_factsVersionVTreeviewbox.TreeView, GlobalVariables.GlobalFactsVersions.GetDictionary())

        m_exchangeRatesVersionVTreeviewbox.Text = ""
        m_factsVersionVTreeviewbox.Text = ""

        DesactivateUnallowed()

    End Sub

    Private Sub DesactivateUnallowed()
        If Not GlobalVariables.Users.CurrentUserIsAdmin() Then
            lockedCB.Enabled = False
            m_exchangeRatesVersionVTreeviewbox.Enabled = False
            m_factsVersionVTreeviewbox.Enabled = False
            new_folder_bt.Enabled = False
            new_version_bt.Enabled = False
            rename_bt.Enabled = False
            delete_bt.Enabled = False
            NewVersionMenuBT.Enabled = False
            NewFolderMenuBT.Enabled = False
            DeleteVersionMenuBT.Enabled = False
            RenameMenuBT.Enabled = False
        End If
    End Sub

    Private Sub MultilanguageSetup()

        Me.new_version_bt.Text = Local.GetValue("versions.add_version")
        Me.new_folder_bt.Text = Local.GetValue("versions.add_folder")
        Me.rename_bt.Text = Local.GetValue("general.rename")
        Me.delete_bt.Text = Local.GetValue("general.delete")
        Me.m_globalFactsVersionLabel.Text = Local.GetValue("facts_versions.global_facts_version")
        Me.m_exchangeRatesVersionLabel.Text = Local.GetValue("facts_versions.exchange_rates_version")
        Me.m_numberOfYearsLabel.Text = Local.GetValue("facts_versions.nb_years")
        Me.m_startingPeriodLabel.Text = Local.GetValue("facts_versions.starting_period")
        Me.m_periodConfigLabel.Text = Local.GetValue("facts_versions.period_config")
        Me.m_nameLabel.Text = Local.GetValue("facts_versions.version_name")
        Me.m_lockedLabel.Text = Local.GetValue("facts_versions.version_locked")
        Me.m_lockedDateLabel.Text = Local.GetValue("facts_versions.locked_date")
        Me.m_creationDateLabel.Text = Local.GetValue("facts_versions.creation_date")
        Me.VersionsToolStripMenuItem.Text = Local.GetValue("general.versions")
        Me.NewVersionMenuBT.Text = Local.GetValue("versions.add_version")
        Me.NewFolderMenuBT.Text = Local.GetValue("versions.add_folder")
        Me.DeleteVersionMenuBT.Text = Local.GetValue("general.delete")
        Me.RenameMenuBT.Text = Local.GetValue("general.rename")

    End Sub

#End Region


#Region "Interface"

    Private Sub Display(ByRef inputNode As VIBlend.WinForms.Controls.vTreeNode)

        Dim startPeriod As String = ""

        If m_controller.IsFolder(CUInt(inputNode.Value)) Then
            NameTB.Text = ""
            CreationTB.Text = ""
            lockedCB.Checked = False
            LockedDateT.Text = ""
            TimeConfigTB.Text = ""
            StartPeriodTB.Text = ""
            NBPeriodsTB.Text = ""
            m_exchangeRatesVersionVTreeviewbox.Text = ""
            m_factsVersionVTreeviewbox.Text = ""
        Else
            Dim version As Version = m_controller.GetVersion(CUInt(inputNode.Value))
            If version Is Nothing Then Exit Sub

            NameTB.Text = inputNode.Text
            CreationTB.Text = version.CreatedAt
            TimeConfigTB.Text = version.TimeConfiguration

            If version.TimeConfiguration = CRUD.TimeConfig.YEARS Then
                startPeriod = Format(Date.FromOADate(version.StartPeriod), "yyyy")
            Else
                startPeriod = Format(Date.FromOADate(version.StartPeriod), "MMM yyyy")
            End If
            StartPeriodTB.Text = startPeriod
            StartPeriodTB.ValueMember = version.StartPeriod

            NBPeriodsTB.Text = version.NbPeriod

            ' Exchange Rates verions node activation
            Dim activeExchangeRatesNode As vTreeNode = VTreeViewUtil.FindNode(m_exchangeRatesVersionVTreeviewbox.TreeView, version.RateVersionId)
            If Not activeExchangeRatesNode Is Nothing Then
                m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode = activeExchangeRatesNode
                m_exchangeRatesVersionVTreeviewbox.Text = m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Text
            End If

            ' Facts Rates verions node activation
            Dim activeFactsRatesNode As vTreeNode = VTreeViewUtil.FindNode(m_factsVersionVTreeviewbox.TreeView, version.GlobalFactVersionId)
            If Not activeFactsRatesNode Is Nothing Then
                m_factsVersionVTreeviewbox.TreeView.SelectedNode = activeFactsRatesNode
                m_factsVersionVTreeviewbox.Text = m_factsVersionVTreeviewbox.TreeView.SelectedNode.Text
            End If

            If version.Locked Then
                lockedCB.Checked = True
                LockedDateT.Text = version.LockDate
            Else
                lockedCB.Checked = False
                LockedDateT.Text = Local.GetValue("facts_versions.version_not_locked")
            End If
        End If

    End Sub

    Private Sub DeleteVersion()

        If Not m_currentNode Is Nothing Then
            Dim confirm As Integer = MessageBox.Show(Local.GetValue("versions.msg_delete1") + Chr(13) + Chr(13) + _
                                                     m_currentNode.Text + Chr(13) + Chr(13) + _
                                                     Local.GetValue("versions.msg_delete2") + Chr(13) + Chr(13), _
                                                     Local.GetValue("versions.title_delete_confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                m_controller.DeleteVersions(m_currentNode)
                MsgBox(Local.GetValue("facts_versions.msg_delete_successful"))
            End If
        End If

    End Sub

#End Region


#Region "Events"

    Delegate Sub AfterRead_Delegate(ByRef p_version As Version)
    Friend Sub AfterRead(ByRef p_version As Version)

        If Me.m_versionsTV.InvokeRequired Then
            Dim MyDelegate As New AfterRead_Delegate(AddressOf AfterRead)
            Me.m_versionsTV.Invoke(MyDelegate, New Object() {p_version})
        Else
                Dim l_versionNode As vTreeNode = VTreeViewUtil.FindNode(m_versionsTV, p_version.Id)
                If l_versionNode Is Nothing Then
                If p_version.ParentId <> 0 Then
                    Dim parentNode As vTreeNode = VTreeViewUtil.FindNode(m_versionsTV, p_version.ParentId)
                    If parentNode IsNot Nothing Then
                        VTreeViewUtil.AddNode(p_version.Id, p_version.Name, parentNode, p_version.Image)
                    End If
                Else
                    VTreeViewUtil.AddNode(p_version.Id, p_version.Name, m_versionsTV, p_version.Image)
                End If
                Else
                    l_versionNode.Text = p_version.Name
                    l_versionNode.ImageIndex = p_version.Image
                End If
                If m_currentNode IsNot Nothing AndAlso p_version.Id = m_currentNode.Value Then Display(m_currentNode)
            End If
    End Sub

    Delegate Sub AfterDelete_Delegate(ByRef id As UInt32)
    Friend Sub AfterDelete(ByRef id As UInt32)

        If Me.m_versionsTV.InvokeRequired Then
            Dim MyDelegate As New AfterDelete_Delegate(AddressOf AfterDelete)
            Me.m_versionsTV.Invoke(MyDelegate, New Object() {id})
        Else
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.FindNode(m_versionsTV, id)
            If Not node Is Nothing Then
                node.Remove()
            End If
        End If
    End Sub

#Region "TV Events"

    Delegate Sub VersionsTV_AfterSelect_Delegate(sender As Object, e As VIBlend.WinForms.Controls.vTreeViewEventArgs)
    Private Sub VersionsTV_AfterSelect(sender As Object, e As VIBlend.WinForms.Controls.vTreeViewEventArgs)

        If InvokeRequired Then
            Dim MyDelegate As New VersionsTV_AfterSelect_Delegate(AddressOf VersionsTV_AfterSelect)
            Me.Invoke(MyDelegate, New Object() {sender, e})
        Else
            m_currentNode = e.Node
            m_isDisplaying = True
            Display(m_currentNode)
            m_isDisplaying = False
            DesactivateUnallowed()
        End If

    End Sub

    Private Sub tv_mouse_click(sender As Object, e As MouseEventArgs)

        m_currentNode = VTreeViewUtil.GetNodeAtPosition(m_versionsTV, e.Location)

    End Sub

    Private Sub CategoriesTV_KeyDown(sender As Object, e As KeyEventArgs)

        ' priority high -> keydown sur le TV à ctacher !!
        Select Case e.KeyCode
            Case Keys.Delete : delete_bt_Click(sender, e)
            Case Keys.Up
                If e.Control Then
                    If Not m_currentNode Is Nothing Then
                        VTreeViewUtil.MoveNodeUp(m_currentNode)
                    End If
                End If
            Case Keys.Down
                If e.Control Then
                    If Not m_currentNode Is Nothing Then
                        VTreeViewUtil.MoveNodeDown(m_currentNode)
                    End If
                End If
        End Select

    End Sub


#End Region

#Region "Nodes Drag and Drop Procedure"

    Private Sub versionsTV_ItemDrag(sender As Object, e As ItemDragEventArgs)

        DoDragDrop(e.Item, DragDropEffects.Move)

    End Sub

    Private Sub versionsTV_DragEnter(sender As Object, e As DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub versionsTV_DragOver(sender As Object, e As Windows.Forms.DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        Dim selectedTreeview As Windows.Forms.TreeView = CType(sender, TreeView)
        Dim pt As Drawing.Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
        Dim targetNode As TreeNode = selectedTreeview.GetNodeAt(pt)

        'See if the targetNode is currently selected, if so no need to validate again
        If Not (selectedTreeview.SelectedNode Is targetNode) Then       'Select the node currently under the cursor
            selectedTreeview.SelectedNode = targetNode

            'Check that the selected node is not the dropNode and also that it is not a child of the dropNode and therefore an invalid target
            Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

            Do Until targetNode Is Nothing
                If targetNode Is dropNode Then
                    e.Effect = DragDropEffects.None
                    Exit Sub
                End If
                targetNode = targetNode.Parent
            Loop
        End If
        'Currently selected node is a suitable target
        ' If Controller.IsFolder(targetNode.Name) = True Then 
        e.Effect = DragDropEffects.Move ' here check that the process only allow to drop on folders!

    End Sub

    Private Sub versionsTV_DragDrop(sender As Object, e As DragEventArgs)

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        Dim selectedTreeview As TreeView = CType(sender, TreeView)

        'Get the TreeNode being dragged
        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        'The target node should be selected from the DragOver event
        Dim targetNode As TreeNode = selectedTreeview.SelectedNode
        If targetNode Is Nothing Then
            dropNode.Remove()                                               'Remove the drop node from its current location
            selectedTreeview.Nodes.Add(dropNode)
            m_controller.UpdateParent(dropNode.Name, DBNull.Value)
        ElseIf m_controller.IsFolder(targetNode.Name) = True Then
            dropNode.Remove()                                               'Remove the drop node from its current location
            targetNode.Nodes.Add(dropNode)
            m_controller.UpdateParent(dropNode.Name, targetNode.Name)
        End If
        dropNode.EnsureVisible()
        selectedTreeview.SelectedNode = dropNode

    End Sub

#End Region


#End Region


#Region "Versions Attributes Modifications"

    Private Sub lockedCB_CheckedChanged(sender As Object, e As EventArgs) Handles lockedCB.CheckedChanged

        ' only if not folder ?
        If Not m_versionsTV.SelectedNode Is Nothing AndAlso m_isDisplaying = False Then
            Select Case lockedCB.Checked
                Case True : m_controller.LockVersion(m_versionsTV.SelectedNode.Value)
                Case False : m_controller.UnlockVersion(m_versionsTV.SelectedNode.Value)
            End Select
        End If

    End Sub

    Private Sub ExchangeRatesVTreebox_TextChanged(sender As Object, e As EventArgs)

        If Not m_versionsTV.SelectedNode Is Nothing AndAlso m_isDisplaying = False Then
            Dim version_id As Int32 = m_versionsTV.SelectedNode.Value
            Dim rates_version_id As Int32 = m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value

            ' Not folder Control
            If m_controller.IsRatesVersionValid(rates_version_id) = False Then
                MsgBox(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_is_folder"))
                GoTo RevertToFormerValue
            End If

            ' Time configuration control
            If m_controller.IsRatesVersionCompatibleWithPeriods(StartPeriodTB.ValueMember, NBPeriodsTB.Text, rates_version_id) Then
                m_controller.UpdateRatesVersion_id(version_id, rates_version_id)
            Else
                MsgBox(Local.GetValue("facts_versions.msg_rates_version_mismatch"))
                GoTo RevertToFormerValue
            End If
        End If
        Exit Sub


RevertToFormerValue:
        m_isDisplaying = True
        Dim version As Version = m_controller.GetVersion(CUInt(m_currentNode.Value))

        If Not version Is Nothing Then
            m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode = VTreeViewUtil.FindNode(m_exchangeRatesVersionVTreeviewbox.TreeView, version.RateVersionId)
        End If
        m_isDisplaying = False

    End Sub

    Private Sub FactsRatesVTreebox_TextChanged(sender As Object, e As EventArgs)

        If Not m_versionsTV.SelectedNode Is Nothing AndAlso m_isDisplaying = False Then
            Dim version_id As Int32 = m_versionsTV.SelectedNode.Value
            Dim fact_version_id As Int32 = m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value

            ' Not folder Control
            If m_controller.IsFactsVersionValid(fact_version_id) = False Then
                MsgBox(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_is_folder"))
                GoTo RevertToFormerValue
            End If

            ' Time configuration control
            If m_controller.IsFactVersionCompatibleWithPeriods(StartPeriodTB.ValueMember, NBPeriodsTB.Text, fact_version_id) Then
                m_controller.UpdateFactVersion_id(version_id, fact_version_id)
            Else
                MsgBox(Local.GetValue("facts_versions.msg_fact_version_mismatch"))
                GoTo RevertToFormerValue
            End If
        End If
        Exit Sub

RevertToFormerValue:
        m_isDisplaying = True
        Dim version As Version = m_controller.GetVersion(CUInt(m_currentNode.Value))

        If Not version Is Nothing Then
            m_factsVersionVTreeviewbox.TreeView.SelectedNode = VTreeViewUtil.FindNode(m_factsVersionVTreeviewbox.TreeView, version.GlobalFactVersionId)
        End If
        m_isDisplaying = False

    End Sub

#End Region


#Region "Right Click Menu"

    Private Sub new_version_bt_Click(sender As Object, e As EventArgs) Handles new_version_bt.Click, NewVersionMenuBT.Click

        If Not m_currentNode Is Nothing Then
            If m_controller.IsFolder(m_currentNode.Value) Then
                m_controller.ShowNewVersionUI(m_currentNode)
            Else
                m_controller.ShowNewVersionUI()
            End If
        Else
            m_controller.ShowNewVersionUI()
        End If

    End Sub

    Private Sub new_folder_bt_Click(sender As Object, e As EventArgs) Handles new_folder_bt.Click, NewFolderMenuBT.Click

        Dim name = InputBox(Local.GetValue("facts_versions.msg_folder_name"))
        Dim version As New Version
        If name <> "" Then
            If m_controller.IsNameValid(name) = True Then

                version.Name = name
                version.IsFolder = True
                If Not m_currentNode Is Nothing Then
                    version.ParentId = m_currentNode.Value
                End If
                m_controller.CreateVersion(version)
            Else
                MsgBox(Local.GetValue("facts_versions.invalid_name"))
            End If
        End If

    End Sub

    Private Sub delete_bt_Click(sender As Object, e As EventArgs) Handles delete_bt.Click, DeleteVersionMenuBT.Click

        DeleteVersion()

    End Sub

    Private Sub rename_bt_Click(sender As Object, e As EventArgs) Handles rename_bt.Click, RenameMenuBT.Click

        If Not m_currentNode Is Nothing Then
            Dim name As String = InputBox(Local.GetValue("facts_versions.msg_new_version_name"))
            If name <> "" Then
                If m_controller.IsNameValid(name) Then
                    m_controller.UpdateName(m_currentNode.Value, name)
                    m_currentNode.Text = name
                Else
                    MsgBox(Local.GetValue("facts_versions.msg_invalid_name"))
                End If
            End If
        Else
            MsgBox(Local.GetValue("facts_versions.msg_select_version_or_folder"))
        End If

    End Sub


#Region "Main Menu Call Backs"

    Private Sub AddFolderBT_Click(sender As Object, e As EventArgs)

        new_folder_bt_Click(sender, e)

    End Sub

    Private Sub AddVersionBT_Click(sender As Object, e As EventArgs)

        new_version_bt_Click(sender, e)

    End Sub

    Private Sub DeleteVersionBT_Click(sender As Object, e As EventArgs)

        delete_bt_Click(sender, e)

    End Sub

#End Region


#End Region


End Class
