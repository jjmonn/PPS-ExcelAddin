﻿' VersioningManagementUI.vb
'
' Allow the user to manage the versions: add, copy, create/ delete..
' Displays versions attributes
'
' to do : 
'       - new folder -> check position on tree
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
' Last modified: 20/01/2015
' Author: Julien Monnereau


Imports System.Collections.Generic
Imports System.Windows.Forms
Imports System.Collections
Imports System.Drawing



Friend Class VersioningManagementUI


#Region "Instance variables"

    ' Objects
    Private Controller As DataVersionsController
    Private VersionsTV As TreeView

    ' Variables
    Private current_node As TreeNode
    Private creationFlag As String
    Private isDisplaying As Boolean

    ' const
    Private Const POSITION_STEP As Double = 0.0000001


#End Region


#Region "Initialization"

    Friend Sub New(ByRef input_controller As DataVersionsController, _
                   ByRef input_versionsTV As TreeView)

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
        AddHandler VersionsTV.NodeMouseClick, AddressOf tv_node_mouse_click
        AddHandler VersionsTV.ItemDrag, AddressOf versionsTV_ItemDrag
        AddHandler VersionsTV.DragEnter, AddressOf versionsTV_DragEnter
        AddHandler VersionsTV.DragOver, AddressOf versionsTV_DragOver
        AddHandler VersionsTV.DragDrop, AddressOf versionsTV_DragDrop

        For Each name_ In Controller.rates_versions_name_id_dic.Keys
            RatesVersionCB.Items.Add(name_)
        Next

    End Sub

    Private Sub VersionsMGTUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.WindowState = FormWindowState.Maximized

    End Sub

#End Region


#Region "Interface"

    Private Sub Display(ByRef inputNode As TreeNode)

        Dim record = Controller.GetRecord(inputNode.Name)
        If record(VERSIONS_IS_FOLDER_VARIABLE) = 1 Then
            NameTB.Text = ""
            CreationTB.Text = ""
            lockedCB.Checked = False
            LockedDateT.Text = ""
            TimeConfigTB.Text = ""
            StartPeriodTB.Text = ""
            NBPeriodsTB.Text = ""
            RatesVersionCB.Text = ""
        Else
            NameTB.Text = inputNode.Text
            CreationTB.Text = record(VERSIONS_CREATION_DATE_VARIABLE)
            TimeConfigTB.Text = record(VERSIONS_TIME_CONFIG_VARIABLE)
            StartPeriodTB.Text = record(VERSIONS_START_PERIOD_VAR)
            NBPeriodsTB.Text = record(VERSIONS_NB_PERIODS_VAR)
            RatesVersionCB.SelectedItem = Controller.rates_versions_id_name_dic(record(VERSIONS_RATES_VERSION_ID_VAR))
            If record(VERSIONS_LOCKED_VARIABLE) = 1 Then
                lockedCB.Checked = True
                LockedDateT.Text = record(VERSIONS_LOCKED_DATE_VARIABLE)
            Else
                lockedCB.Checked = False
                LockedDateT.Text = "Version not locked"
            End If
        End If

    End Sub

    Private Sub DeleteVersion()

        If Not current_node Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("You are about to delete the version named: " + Chr(13) + Chr(13) + _
                                                     current_node.Text + Chr(13) + Chr(13) + _
                                                     "Do you confirm?" + Chr(13) + Chr(13), _
                                                     "Version deleting confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Controller.DeleteVersions(current_node)
                MsgBox("The version has successfully been deleted")
            End If
        End If

    End Sub

#End Region


#Region "Events"

#Region "TV Events"

    Private Sub VersionsTV_AfterSelect(sender As Object, e As TreeViewEventArgs)

        current_node = e.Node
        isDisplaying = True
        Display(current_node)
        isDisplaying = False

    End Sub

    Private Sub tv_node_mouse_click(sender As Object, e As TreeNodeMouseClickEventArgs)

        current_node = e.Node

    End Sub

    Private Sub CategoriesTV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : delete_bt_Click(sender, e)
            Case Keys.Up
                If e.Control Then
                    If Not current_node Is Nothing Then MoveNodeUp()
                End If
            Case Keys.Down
                If e.Control Then
                    If Not current_node Is Nothing Then MoveNodeDown()
                End If
        End Select

    End Sub

#Region "Move nodes up and down into hierarchy Procedure"

    Private Sub MoveNodeUp()

        If Not current_node.PrevNode Is Nothing Then
            Dim currentKey As String = current_node.Name
            Me.Hide()
            Dim currentPosition As Double = Controller.positions_dictionary(current_node.PrevNode.Name) - 1 + POSITION_STEP
            TreeViewsUtilities.UpdateChildrenPosition(current_node, currentPosition, Controller.positions_dictionary)
            ResumeVersionsTree()
            Me.Show()
            VersionsTV.SelectedNode = versionstv.Nodes.Find(currentKey, True)(0)
        End If

    End Sub

    Private Sub MoveNodeDown()

        If Not current_node.NextNode Is Nothing Then
            Dim currentKey As String = current_node.Name
            Dim currentPosition As Object = Controller.positions_dictionary(current_node.NextNode.Name) _
                                            + TreeViewsUtilities.GetNodeAllChildrenCount(current_node.NextNode) + POSITION_STEP
            TreeViewsUtilities.UpdateChildrenPosition(current_node, currentPosition, Controller.positions_dictionary)
            Me.Hide()
            ResumeVersionsTree()
            Me.Show()
            versionstv.SelectedNode = versionstv.Nodes.Find(currentKey, True)(0)
        End If

    End Sub

    Private Sub ResumeVersionsTree()

        Dim expansionDic As Dictionary(Of String, Boolean) = TreeViewsUtilities.SaveNodesExpansionsLevel(versionstv)
        Controller.SendNewPositionsToModel()
        Category.LoadCategoriesTree(versionstv)
        TreeViewsUtilities.ResumeExpansionsLevel(versionstv, expansionDic)

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
        If Controller.IsFolder(targetNode.Name) = True Then e.Effect = DragDropEffects.Move ' here check that the process only allow to drop on folders!

    End Sub

    Private Sub versionsTV_DragDrop(sender As Object, e As DragEventArgs)

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        Dim selectedTreeview As TreeView = CType(sender, TreeView)

        'Get the TreeNode being dragged
        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        'The target node should be selected from the DragOver event
        Dim targetNode As TreeNode = selectedTreeview.SelectedNode
        If Controller.IsFolder(targetNode.Name) = True Then

            dropNode.Remove()                                               'Remove the drop node from its current location

            If targetNode Is Nothing Then
                selectedTreeview.Nodes.Add(dropNode)
            Else
                targetNode.Nodes.Add(dropNode)
            End If

            dropNode.EnsureVisible()                                        ' Ensure the newley created node is visible to the user and 
            selectedTreeview.SelectedNode = dropNode
            Controller.UpdateParent(dropNode.Name, targetNode.Name)

            Dim currentPosition = Controller.positions_dictionary(targetNode.Name) + targetNode.Nodes.Count + POSITION_STEP
            TreeViewsUtilities.UpdateChildrenPosition(dropNode, currentPosition, Controller.positions_dictionary)
            Controller.SendNewPositionsToModel()
        End If

    End Sub

#End Region


#End Region

#Region "Versions Attributes Modifications"

    Private Sub lockedCB_CheckedChanged(sender As Object, e As EventArgs) Handles lockedCB.CheckedChanged

        ' only if not folder ?
        If Not VersionsTV.SelectedNode Is Nothing AndAlso isDisplaying = False Then
            Select Case lockedCB.Checked
                Case True : Controller.LockVersion(VersionsTV.SelectedNode.Name)
                Case False : Controller.UnlockVersion(VersionsTV.SelectedNode.Name)
            End Select
        End If

    End Sub

    Private Sub RatesVersionCB_SelectedValueChanged(sender As Object, e As EventArgs) Handles RatesVersionCB.SelectedValueChanged

        If Not VersionsTV.SelectedNode Is Nothing AndAlso isDisplaying = False Then
            Dim version_id As String = VersionsTV.SelectedNode.Name
            Dim rates_version_id As String = Controller.rates_versions_name_id_dic(RatesVersionCB.Text)
            If Controller.IsRatesVersionValid(StartPeriodTB.Text, NBPeriodsTB.Text, rates_version_id) Then
                Controller.UpdateRatesVersion_id(version_id, rates_version_id)
            Else
                MsgBox("This Exchange Rates Version is not compatible with the Periods Configuration.")
            End If
        End If

    End Sub

#End Region

#End Region


#Region "Right Click Menu"

    Private Sub new_version_bt_Click(sender As Object, e As EventArgs) Handles new_version_bt.Click

        If Not current_node Is Nothing Then
            If Controller.IsFolder(current_node.Name) Then
                Controller.ShowNewVersionUI(current_node)
            Else
                Controller.ShowNewVersionUI()
            End If
        Else
            Controller.ShowNewVersionUI()
        End If

    End Sub

    Private Sub new_folder_bt_Click(sender As Object, e As EventArgs) Handles new_folder_bt.Click

        Dim name = InputBox("Please enter the new Folder Name")
        If name <> "" Then
            If Controller.IsNameValid(name) = True Then
                If Not current_node Is Nothing Then
                    Controller.CreateFolder(name, current_node)
                Else
                    Controller.CreateFolder(name)
                End If
            Else
                MsgBox("Invalid Name.")
            End If
        End If

    End Sub

    Private Sub delete_bt_Click(sender As Object, e As EventArgs) Handles delete_bt.Click

        DeleteVersion()

    End Sub

    Private Sub rename_bt_Click(sender As Object, e As EventArgs) Handles rename_bt.Click

        If Not current_node Is Nothing Then
            Dim name As String = InputBox("Enter the new Name: ")
            If name <> "" Then
                If Controller.IsNameValid(name) Then
                    Controller.UpdateName(current_node.Name, name)
                    current_node.Text = name
                Else
                    MsgBox("This Name is not valid. Either it already exists or contains forbidden characters. Please Try with another Name.")
                End If
            End If
        Else
            MsgBox("Please select a Version or a Folder.")
        End If

    End Sub


#Region "Main Menu Call Backs"

    Private Sub AddFolderBT_Click(sender As Object, e As EventArgs) Handles AddFolderBT.Click

        new_folder_bt_Click(sender, e)

    End Sub

    Private Sub AddVersionBT_Click(sender As Object, e As EventArgs) Handles AddVersionBT.Click

        new_version_bt_Click(sender, e)

    End Sub

    Private Sub DeleteVersionBT_Click(sender As Object, e As EventArgs) Handles DeleteVersionBT.Click

        delete_bt_Click(sender, e)

    End Sub

#End Region


#End Region


#Region "Utilities"

    Private Sub VersioningManagementUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Controller.positions_dictionary = TreeViewsUtilities.GeneratePositionsDictionary(VersionsTV)
        Controller.SendNewPositionsToModel()

    End Sub


#End Region

   
End Class