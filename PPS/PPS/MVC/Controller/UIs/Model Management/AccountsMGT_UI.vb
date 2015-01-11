Imports Microsoft.Office.Interop

Public Class AccountsMGT_UI

    Private FormulaHT As Collections.Hashtable

    Private Sub AccountsMGT_UI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '-------------------------------------------------------------------------------
        ' Load the accounts from the DB
        ' Initializes details of the treeview
        '-------------------------------------------------------------------------------
        TVInit(AccountsTreeview)
        FormulaHT = LoadAccountsTreeview(Me.AccountsTreeview)
        AccountsTreeview.CollapseAll()

    End Sub


    Private Sub B_AddAccount_Click(sender As Object, e As EventArgs) Handles B_AddAccount.Click

        '---------------------------------------------------------------------------------------
        ' Sub: Add Account to the TreeView + triger update of the DataBase
        '      Maintain FormulaHT
        '---------------------------------------------------------------------------------------

        Dim TempArray(1) As String
        addNode(AccountsTreeview, "accounts")                                   ' Call UserForms_Functions addNode()
        TempArray(0) = "HV"
        TempArray(1) = ""
        FormulaHT.Add(AccountsTreeview.SelectedNode.Name, TempArray)            ' Add data to Formula hashTable
        updateAccTable(AccountsTreeview, FormulaHT)                             ' Save changes

    End Sub

    Private Sub B_AddSubAccount_Click(sender As Object, e As EventArgs) Handles B_AddSubAccount.Click

        '---------------------------------------------------------------------------------------
        ' Sub: Add SubAccount to the TreeView + triger update of the DataBase
        '      Maintain FormulaHT
        '---------------------------------------------------------------------------------------

        Dim TempArray(1) As String
        addChild(AccountsTreeview, "accounts")                                  ' Call UserForms_Functions addNode()
        TempArray(0) = "HV"
        TempArray(1) = ""
        FormulaHT.Add(AccountsTreeview.SelectedNode.Name, TempArray)            ' Add data to Formula hashTable
        updateAccTable(AccountsTreeview, FormulaHT)                             ' Save changes

    End Sub

    Private Sub B_DeleteAccount_Click(sender As Object, e As EventArgs) Handles B_DeleteAccount.Click

        '-------------------------------------------------------------------------------
        ' 
        '-------------------------------------------------------------------------------
        MsgBox("Account deletion is currently Managed at PurpleSun Solutions level. Please contact Julien Monnereau")

    End Sub

    Private Sub B_AddFormula_Click(sender As Object, e As EventArgs) Handles B_AddFormula.Click
        MsgBox("User Interface in development")
        ' -> Change Button name to "Manage Account" 
        ' Launch new UI
        ' -> This UI allows to see and change the details of the account
        ' -> Add or change the formula and etc. (check of the formula at editing time)

    End Sub

    Private Sub BDropToWS_Click(sender As Object, e As EventArgs) Handles BDropToWS.Click

        '--------------------------------------------------------------------------------
        ' Drop the account hierarchy to the Excel worksheet
        '--------------------------------------------------------------------------------
        Dim ActiveWS As Excel.Worksheet = apps.ActiveSheet
        Dim RNG As Excel.Range = apps.Application.ActiveCell
        Dim Response As MsgBoxResult

        If IsNothing(RNG) Then
            MsgBox("A destination cell must be selected in order to drop the Accounts on the Worksheet")
            Exit Sub
        Else
            Response = MsgBox("The Accounts will be dropped into cell" & RNG.Address, MsgBoxStyle.OkCancel)
            If Response = MsgBoxResult.Ok Then
                ' Launch Accounts Drop
                WriteAccountsFromTreeView(AccountsTreeview, FormulaHT, RNG)
            ElseIf Response = MsgBoxResult.Cancel Then
                Exit Sub
            End If
        End If
    End Sub


    '---------------------------------------------------------------------------------------
    ' Nodes Drag and Drop Procedure
    '---------------------------------------------------------------------------------------

    Private Sub AccountsTreeview_ItemDrag(sender As Object, e As Windows.Forms.ItemDragEventArgs) Handles AccountsTreeview.ItemDrag

        '----------------------------------------------------------------------------------
        ' Set the drag node and initiate the dragDrop
        '----------------------------------------------------------------------------------
        DoDragDrop(e.Item, Windows.Forms.DragDropEffects.Move)

    End Sub

    Private Sub AccountsTreeview_DragEnter(sender As Object, e As Windows.Forms.DragEventArgs) Handles AccountsTreeview.DragEnter
        'See if there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", _
            True) Then
            'TreeNode found allow move effect
            e.Effect = Windows.Forms.DragDropEffects.Move
        Else
            'No TreeNode found, prevent move
            e.Effect = Windows.Forms.DragDropEffects.None
        End If
    End Sub

    Private Sub AccountsTreeview_DragOver(sender As Object, e As Windows.Forms.DragEventArgs) Handles AccountsTreeview.DragOver

        'Check that there is a TreeNode being dragged 
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", _
               True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedTreeview As Windows.Forms.TreeView = CType(sender, Windows.Forms.TreeView)

        'As the mouse moves over nodes, provide feedback to 
        'the user by highlighting the node that is the 
        'current drop target
        Dim pt As Drawing.Point = _
            CType(sender, Windows.Forms.TreeView).PointToClient(New Drawing.Point(e.X, e.Y))
        Dim targetNode As Windows.Forms.TreeNode = selectedTreeview.GetNodeAt(pt)

        'See if the targetNode is currently selected, 
        'if so no need to validate again
        If Not (selectedTreeview.SelectedNode Is targetNode) Then
            'Select the    node currently under the cursor
            selectedTreeview.SelectedNode = targetNode

            'Check that the selected node is not the dropNode and
            'also that it is not a child of the dropNode and 
            'therefore an invalid target
            Dim dropNode As Windows.Forms.TreeNode = _
                CType(e.Data.GetData("System.Windows.Forms.TreeNode"),  _
                Windows.Forms.TreeNode)

            Do Until targetNode Is Nothing
                If targetNode Is dropNode Then
                    e.Effect = Windows.Forms.DragDropEffects.None
                    Exit Sub
                End If
                targetNode = targetNode.Parent
            Loop
        End If

        'Currently selected node is a suitable target
        e.Effect = Windows.Forms.DragDropEffects.Move


    End Sub

    Private Sub AccountsTreeview_DragDrop(sender As Object, e As Windows.Forms.DragEventArgs) Handles AccountsTreeview.DragDrop

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedTreeview As Windows.Forms.TreeView = CType(sender, Windows.Forms.TreeView)

        'Get the TreeNode being dragged
        Dim dropNode As Windows.Forms.TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"),  _
                                                       Windows.Forms.TreeNode)

        'The target node should be selected from the DragOver event
        Dim targetNode As Windows.Forms.TreeNode = selectedTreeview.SelectedNode

        dropNode.Remove()                                               'Remove the drop node from its current location

        If targetNode Is Nothing Then
            selectedTreeview.Nodes.Add(dropNode)
        Else
            targetNode.Nodes.Add(dropNode)
        End If

        dropNode.EnsureVisible()                                        'Ensure the newley created node is visible to the user and 
        selectedTreeview.SelectedNode = dropNode                        'select it
        updateAccTable(AccountsTreeview, FormulaHT)                     ' Save new hierarchy to DataBase

    End Sub

    '---------------------------------------------------------------------------------------
    ' 
    '---------------------------------------------------------------------------------------


    Private Sub AccountsTreeview_AfterLabelEdit(sender As Object, e As Windows.Forms.NodeLabelEditEventArgs) Handles AccountsTreeview.AfterLabelEdit

        '-------------------------------------------------------------------------
        ' After label edit Event : save the hierarchy to DataBase
        '-------------------------------------------------------------------------
        updateAccTable(AccountsTreeview, FormulaHT)                     ' Save new hierarchy to DataBase

    End Sub
End Class