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
' Last modified: 25/08/2015
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Collections
Imports System.Drawing
Imports VIBlend.WinForms.Controls

'Imports VIBlend.WinForms.Controls



Friend Class VersionsControl


#Region "Instance variables"

    ' Objects
    Private Controller As DataVersionsController
    Private VersionsTV As VIBlend.WinForms.Controls.vTreeView
    Private CP As CircularProgressUI

    ' Variables
    Private current_node As VIBlend.WinForms.Controls.vTreeNode
    Private creationFlag As String
    Private isDisplaying As Boolean

#End Region


#Region "Initialization"

    Friend Sub New(ByRef input_controller As DataVersionsController, _
                   ByRef input_versionsTV As VIBlend.WinForms.Controls.vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        VersionsTV = input_versionsTV
        VersionsTV.ImageList = VersionsTVIcons
        VersionsTVPanel.Controls.Add(VersionsTV)
        VersionsTV.Dock = DockStyle.Fill
        VersionsTV.AllowDrop = True
        VersionsTV.ContextMenuStrip = RCM_TV

        AddHandler VersionsTV.AfterSelect, AddressOf VersionsTV_AfterSelect
        AddHandler VersionsTV.KeyDown, AddressOf CategoriesTV_KeyDown
        AddHandler VersionsTV.MouseClick, AddressOf tv_mouse_click
        AddHandler GlobalVariables.Versions.Read, AddressOf versionReadEvent

        ' implement tv drag and drop -> common for all

        '   AddHandler VersionsTV.drag, AddressOf versionsTV_ItemDrag
        AddHandler VersionsTV.DragEnter, AddressOf versionsTV_DragEnter
        AddHandler VersionsTV.DragOver, AddressOf versionsTV_DragOver
        AddHandler VersionsTV.DragDrop, AddressOf versionsTV_DragDrop

        AddHandler m_exchangeRatesVersionVTreeviewbox.TreeView.AfterSelect, AddressOf ExchangeRatesVTreebox_TextChanged
        AddHandler m_factsVersionVTreeviewbox.TreeView.AfterSelect, AddressOf FactsRatesVTreebox_TextChanged

        VTreeViewUtil.LoadTreeview(m_exchangeRatesVersionVTreeviewbox.TreeView, GlobalVariables.RatesVersions.rate_versions_hash)
        VTreeViewUtil.LoadTreeview(m_factsVersionVTreeviewbox.TreeView, GlobalVariables.GlobalFactsVersions.globalFact_versions_hash)

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

    Friend Sub closeControl()

        Controller.SendNewPositionsToModel()

    End Sub


#End Region


#Region "Interface"

    Private Sub Display(ByRef inputNode As VIBlend.WinForms.Controls.vTreeNode)

        Dim startPeriod As String = ""
        If GlobalVariables.Versions.versions_hash(CInt(inputNode.Value))(IS_FOLDER_VARIABLE) = True Then
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
            Dim versionId As Int32 = CInt(inputNode.Value)
            NameTB.Text = inputNode.Text
            CreationTB.Text = GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_CREATION_DATE_VARIABLE)
            TimeConfigTB.Text = GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE)

            If GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_TIME_CONFIG_VARIABLE) = GlobalEnums.TimeConfig.YEARS Then
                startPeriod = Format(Date.FromOADate(GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_START_PERIOD_VAR)), "yyyy")
            Else
                startPeriod = Format(Date.FromOADate(GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_START_PERIOD_VAR)), "MMM yyyy")
            End If
            StartPeriodTB.Text = startPeriod
            StartPeriodTB.ValueMember = GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_START_PERIOD_VAR)

            NBPeriodsTB.Text = GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_NB_PERIODS_VAR)

            ' Exchange Rates verions node activation
            Dim activeExchangeRatesNode As vTreeNode = VTreeViewUtil.FindNode(m_exchangeRatesVersionVTreeviewbox.TreeView, GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_RATES_VERSION_ID_VAR))
            If Not activeExchangeRatesNode Is Nothing Then
                m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode = activeExchangeRatesNode
                m_exchangeRatesVersionVTreeviewbox.Text = m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Text
            End If

            ' Facts Rates verions node activation
            Dim activeFactsRatesNode As vTreeNode = VTreeViewUtil.FindNode(m_factsVersionVTreeviewbox.TreeView, GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_GLOBAL_FACT_VERSION_ID))
            If Not activeFactsRatesNode Is Nothing Then
                m_factsVersionVTreeviewbox.TreeView.SelectedNode = activeFactsRatesNode
                m_factsVersionVTreeviewbox.Text = m_factsVersionVTreeviewbox.TreeView.SelectedNode.Text
            End If

            If GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_LOCKED_VARIABLE) = True Then
                lockedCB.Checked = True
                LockedDateT.Text = GlobalVariables.Versions.versions_hash(versionId)(VERSIONS_LOCKED_DATE_VARIABLE)
            Else
                lockedCB.Checked = False
                LockedDateT.Text = Local.GetValue("facts_versions.version_not_locked")
            End If
        End If

    End Sub

    Private Sub DeleteVersion()

        If Not current_node Is Nothing Then
            Dim confirm As Integer = MessageBox.Show(Local.GetValue("versions.msg_delete1") + Chr(13) + Chr(13) + _
                                                     current_node.Text + Chr(13) + Chr(13) + _
                                                     Local.GetValue("versions.msg_delete2") + Chr(13) + Chr(13), _
                                                     Local.GetValue("versions.title_delete_confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Controller.DeleteVersions(current_node)
                MsgBox(Local.GetValue("facts_versions.msg_delete_successful"))
            End If
        End If

    End Sub

    Private Sub RenameVersionInTV(ByRef p_id As UInt32, ByRef p_name As String)

        For Each node In VersionsTV.Nodes
            If node.Value = p_id Then node.Text = p_name
        Next

    End Sub

#End Region


#Region "Events"

    Delegate Sub versionReadEvent_Delegate(ByRef p_status As Boolean, ByRef p_attributes As Hashtable)
    Private Sub versionReadEvent(ByRef p_status As Boolean, ByRef p_attributes As Hashtable)
        If InvokeRequired Then
            Dim MyDelegate As New versionReadEvent_Delegate(AddressOf versionReadEvent)
            Me.Invoke(MyDelegate, New Object() {p_status, p_attributes})
        Else
            RenameVersionInTV(p_attributes(ID_VARIABLE), p_attributes(NAME_VARIABLE))
            If p_attributes(ID_VARIABLE) = current_node.Value Then Display(current_node)
        End If
    End Sub

    Delegate Sub AfterDelete_Delegate(ByRef status As Boolean, ByRef id As UInt32)
    Friend Sub AfterDelete(ByRef status As Boolean, ByRef id As UInt32)

        If InvokeRequired Then
            Dim MyDelegate As New AfterDelete_Delegate(AddressOf AfterDelete)
            Me.Invoke(MyDelegate, New Object() {status, id})
        Else
            Dim node As VIBlend.WinForms.Controls.vTreeNode = VTreeViewUtil.FindNode(VersionsTV, id)
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
            current_node = e.Node
            isDisplaying = True
            Display(current_node)
            isDisplaying = False
            DesactivateUnallowed()
        End If

    End Sub

    Private Sub tv_mouse_click(sender As Object, e As MouseEventArgs)

        current_node = VersionsTV.HitTest(e.Location)

    End Sub

    Private Sub CategoriesTV_KeyDown(sender As Object, e As KeyEventArgs)

        ' priority high -> keydown sur le TV à ctacher !!
        Select Case e.KeyCode
            Case Keys.Delete : delete_bt_Click(sender, e)
            Case Keys.Up
                If e.Control Then
                    If Not current_node Is Nothing Then
                        VTreeViewUtil.MoveNodeUp(current_node)
                    End If
                End If
            Case Keys.Down
                If e.Control Then
                    If Not current_node Is Nothing Then
                        VTreeViewUtil.MoveNodeDown(current_node)
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
            Controller.UpdateParent(dropNode.Name, DBNull.Value)
        ElseIf Controller.IsFolder(targetNode.Name) = True Then
            dropNode.Remove()                                               'Remove the drop node from its current location
            targetNode.Nodes.Add(dropNode)
            Controller.UpdateParent(dropNode.Name, targetNode.Name)
        End If
        dropNode.EnsureVisible()
        selectedTreeview.SelectedNode = dropNode

    End Sub

#End Region


#End Region


#Region "Versions Attributes Modifications"

    Private Sub lockedCB_CheckedChanged(sender As Object, e As EventArgs) Handles lockedCB.CheckedChanged

        ' only if not folder ?
        If Not VersionsTV.SelectedNode Is Nothing AndAlso isDisplaying = False Then
            Select Case lockedCB.Checked
                Case True : Controller.LockVersion(VersionsTV.SelectedNode.Value)
                Case False : Controller.UnlockVersion(VersionsTV.SelectedNode.Value)
            End Select
        End If

    End Sub

    Private Sub ExchangeRatesVTreebox_TextChanged(sender As Object, e As EventArgs)

        If Not VersionsTV.SelectedNode Is Nothing AndAlso isDisplaying = False Then
            Dim version_id As Int32 = VersionsTV.SelectedNode.Value
            Dim rates_version_id As Int32 = m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Value

            ' Not folder Control
            If Controller.IsRatesVersionValid(rates_version_id) = False Then
                MsgBox(m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_is_folder"))
                GoTo RevertToFormerValue
            End If

            ' Time configuration control
            If Controller.IsRatesVersionCompatibleWithPeriods(StartPeriodTB.ValueMember, NBPeriodsTB.Text, rates_version_id) Then
                Controller.UpdateRatesVersion_id(version_id, rates_version_id)
            Else
                MsgBox(Local.GetValue("facts_versions.msg_rates_version_mismatch"))
                GoTo RevertToFormerValue
            End If
        End If
        Exit Sub


RevertToFormerValue:
        isDisplaying = True
        m_exchangeRatesVersionVTreeviewbox.TreeView.SelectedNode = VTreeViewUtil.FindNode(m_exchangeRatesVersionVTreeviewbox.TreeView, GlobalVariables.Versions.versions_hash(CInt(current_node.Value))(VERSIONS_RATES_VERSION_ID_VAR))
        isDisplaying = False

    End Sub

    Private Sub FactsRatesVTreebox_TextChanged(sender As Object, e As EventArgs)

        If Not VersionsTV.SelectedNode Is Nothing AndAlso isDisplaying = False Then
            Dim version_id As Int32 = VersionsTV.SelectedNode.Value
            Dim fact_version_id As Int32 = m_factsVersionVTreeviewbox.TreeView.SelectedNode.Value

            ' Not folder Control
            If Controller.IsFactsVersionValid(fact_version_id) = False Then
                MsgBox(m_factsVersionVTreeviewbox.TreeView.SelectedNode.Text & Local.GetValue("facts_versions.msg_is_folder"))
                GoTo RevertToFormerValue
            End If

            ' Time configuration control
            If Controller.IsFactVersionCompatibleWithPeriods(StartPeriodTB.ValueMember, NBPeriodsTB.Text, fact_version_id) Then
                Controller.UpdateFactVersion_id(version_id, fact_version_id)
            Else
                MsgBox(Local.GetValue("facts_versions.msg_fact_version_mismatch"))
                GoTo RevertToFormerValue
            End If
        End If
        Exit Sub

RevertToFormerValue:
        isDisplaying = True
        m_factsVersionVTreeviewbox.TreeView.SelectedNode = VTreeViewUtil.FindNode(m_factsVersionVTreeviewbox.TreeView, GlobalVariables.Versions.versions_hash(CInt(current_node.Value))(VERSIONS_GLOBAL_FACT_VERSION_ID))
        isDisplaying = False

    End Sub

#End Region


#Region "Right Click Menu"

    Private Sub new_version_bt_Click(sender As Object, e As EventArgs) Handles new_version_bt.Click, NewVersionMenuBT.Click

        If Not current_node Is Nothing Then
            If Controller.IsFolder(current_node.Value) Then
                Controller.ShowNewVersionUI(current_node)
            Else
                Controller.ShowNewVersionUI()
            End If
        Else
            Controller.ShowNewVersionUI()
        End If

    End Sub

    Private Sub new_folder_bt_Click(sender As Object, e As EventArgs) Handles new_folder_bt.Click, NewFolderMenuBT.Click

        Dim name = InputBox(Local.GetValue("facts_versions.msg_folder_name"))
        Dim ht As New Hashtable
        If name <> "" Then
            If Controller.IsNameValid(name) = True Then
                ht(NAME_VARIABLE) = name
                If Not current_node Is Nothing Then
                    ht.Add(PARENT_ID_VARIABLE, current_node.Value)
                End If
                Controller.CreateVersion(ht)
            Else
                MsgBox(Local.GetValue("facts_versions.invalid_name"))
            End If
        End If

    End Sub

    Private Sub delete_bt_Click(sender As Object, e As EventArgs) Handles delete_bt.Click, DeleteVersionMenuBT.Click

        DeleteVersion()

    End Sub

    Private Sub rename_bt_Click(sender As Object, e As EventArgs) Handles rename_bt.Click, RenameMenuBT.Click

        If Not current_node Is Nothing Then
            Dim name As String = InputBox(Local.GetValue("facts_versions.msg_new_version_name"))
            If name <> "" Then
                If Controller.IsNameValid(name) Then
                    Controller.UpdateName(current_node.Value, name)
                    current_node.Text = name
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
