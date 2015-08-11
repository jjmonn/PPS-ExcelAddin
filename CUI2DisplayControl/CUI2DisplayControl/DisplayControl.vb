Imports VIBlend.WinForms.Controls

' DisplayControl.vb
'
' User interface for configuring controllingUI2 display
' Allows user ot drag and drop nodes from analysis axis treeview to rows_display_tv
'
'
'
'
' Author: Julien Monnereau
' Last modified: 11/08/2015



Public Class DisplayControl


#Region "Instance Variables"

    ' Objects
    Private analysis_axis_tv As TreeView
    Public Event RefreshOrder()
    Private dimensionsList As New List(Of String)

#End Region


#Region "Interface"

    Public Sub New(ByRef analysis_axis_tv As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.analysis_axis_tv = analysis_axis_tv
        DimensionsTVPanel.Controls.Add(analysis_axis_tv)
        analysis_axis_tv.Dock = DockStyle.Fill
        rowsDisplayList.AllowDrop = True
        columnsDisplayList.AllowDrop = True

        ' Init listboxes
        rowsDisplayList.ItemHeight = 20
        columnsDisplayList.ItemHeight = 20
        rowsDisplayList.HotTrack = True
        columnsDisplayList.HotTrack = True
        rowsDisplayList.AllowDragDrop = True
        columnsDisplayList.AllowDragDrop = True

        ' By default add the fisrt node
        Dim accountsItem = rowsDisplayList.Items.Add(analysis_axis_tv.Nodes(0).Text)
        accountsItem.Value = analysis_axis_tv.Nodes(0).Name
        Dim entitiesItem = rowsDisplayList.Items.Add(analysis_axis_tv.Nodes(1).Text)
        entitiesItem.Value = analysis_axis_tv.Nodes(1).Name
        Dim yearsItem = columnsDisplayList.Items.Add(analysis_axis_tv.Nodes(2).Text)
        yearsItem.Value = analysis_axis_tv.Nodes(2).Name

        dimensionsList.Add(accountsItem.Value)
        dimensionsList.Add(entitiesItem.Value)
        dimensionsList.Add(yearsItem.Value)

        AddHandlers()

    End Sub

    Private Sub AddHandlers()

        AddHandler analysis_axis_tv.ItemDrag, AddressOf analysisAxisTV_ItemDrag
        AddHandler analysis_axis_tv.DragEnter, AddressOf analysisAxisTV_DragEnter

        AddHandler rowsDisplayList.ItemDragging, AddressOf rowsDisplayList_ItemDragging
        AddHandler columnsDisplayList.ItemDragging, AddressOf columnsDisplayList_ItemDragging

        AddHandler rowsDisplayList.ItemDragEnding, AddressOf rowsDisplayList_ItemDragEnding
        AddHandler columnsDisplayList.ItemDragEnding, AddressOf columnsDisplayList_ItemDragEnding

        AddHandler rowsDisplayList.ItemDragStarting, AddressOf rowsDisplayList_ItemDragStarting
        AddHandler columnsDisplayList.ItemDragStarting, AddressOf columnsDisplayList_ItemDragStarting
        AddHandler rowsDisplayList.MouseLeave, AddressOf rowsDisplayList_MouseLeave
        AddHandler columnsDisplayList.MouseLeave, AddressOf columnsDisplayList_MouseLeave

    End Sub

#End Region


#Region "TreeNode"

    Private Sub analysisAxisTV_ItemDrag(sender As Object, e As ItemDragEventArgs)

        DoDragDrop(e.Item, Windows.Forms.DragDropEffects.Move)

    End Sub

    Private Sub analysisAxisTV_DragEnter(sender As Object, e As Windows.Forms.DragEventArgs)

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

#End Region


#Region "Lists Drag and drop"

    Private Sub columnsDisplayList_ItemDragStarting(ByVal sender As Object, ByVal e As ListItemDragCancelEventArgs)

        rowsDisplayList.StartDraggingTimer()
        columnsDisplayList.StartDraggingTimer()

    End Sub

    Private Sub rowsDisplayList_ItemDragStarting(ByVal sender As Object, ByVal e As ListItemDragCancelEventArgs)

        rowsDisplayList.StartDraggingTimer()
        columnsDisplayList.StartDraggingTimer()

    End Sub

    Private Sub rowsDisplayList_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)

        Me.rowsDisplayList.Invalidate()

    End Sub

    Private Sub columnsDisplayList_MouseLeave(ByVal sender As Object, ByVal e As EventArgs)

        Me.columnsDisplayList.Invalidate()

    End Sub

    Private Sub columnsDisplayList_ItemDragEnding(ByVal sender As Object, ByVal e As ListItemDragCancelEventArgs)

        Dim rowsDisplayListScreenRectangle As Rectangle = Me.rowsDisplayList.RectangleToScreen(rowsDisplayList.ClientRectangle)
        If rowsDisplayListScreenRectangle.Contains(Cursor.Position) Then
            vListBox.DropItem(Me.columnsDisplayList, Me.rowsDisplayList, e.SourceItem)
        End If
        rowsDisplayList.StopDraggingTimer()
        columnsDisplayList.StopDraggingTimer()

    End Sub

    Private Sub rowsDisplayList_ItemDragEnding(ByVal sender As Object, ByVal e As ListItemDragCancelEventArgs)

        Dim columnsDisplayListScreenRectangle As Rectangle = Me.columnsDisplayList.RectangleToScreen(columnsDisplayList.ClientRectangle)
        If columnsDisplayListScreenRectangle.Contains(Cursor.Position) Then
            vListBox.DropItem(Me.rowsDisplayList, Me.columnsDisplayList, e.SourceItem)
        End If
        rowsDisplayList.StopDraggingTimer()
        columnsDisplayList.StopDraggingTimer()

    End Sub

    Private rowsDisplayListGraphics As Graphics
    Private columnsDisplayListGraphics As Graphics

    Private Sub columnsDisplayList_ItemDragging(ByVal sender As Object, ByVal e As ListItemDragEventArgs)

        Dim rowsDisplayListScreenRectangle As Rectangle = Me.rowsDisplayList.RectangleToScreen(rowsDisplayList.ClientRectangle)
        If rowsDisplayListScreenRectangle.Contains(Cursor.Position) Then
            If Me.rowsDisplayListGraphics Is Nothing Then
                Me.rowsDisplayListGraphics = Me.rowsDisplayList.CreateGraphics()
            End If

            Dim point As Point = Me.rowsDisplayList.PointToClient(Cursor.Position)
            vListBox.DrawDragFeedback(Me.rowsDisplayListGraphics, Me.rowsDisplayList)
        Else
            Me.rowsDisplayList.Invalidate()
            Me.columnsDisplayList.Invalidate()
        End If

    End Sub

    Private Sub rowsDisplayList_ItemDragging(ByVal sender As Object, ByVal e As ListItemDragEventArgs)

        Dim columnsDisplayListScreenRectangle As Rectangle = Me.columnsDisplayList.RectangleToScreen(columnsDisplayList.ClientRectangle)
        If columnsDisplayListScreenRectangle.Contains(Cursor.Position) Then
            If Me.columnsDisplayListGraphics Is Nothing Then
                Me.columnsDisplayListGraphics = Me.columnsDisplayList.CreateGraphics()
            End If

            Dim point As Point = Me.columnsDisplayList.PointToClient(Cursor.Position)
            vListBox.DrawDragFeedback(Me.columnsDisplayListGraphics, Me.columnsDisplayList)
        Else
            Me.rowsDisplayList.Invalidate()
            Me.columnsDisplayList.Invalidate()
        End If

    End Sub

#End Region


#Region "Lists Node Drop"

    Private Sub TV_DragOver(sender As Object, e As Windows.Forms.DragEventArgs) Handles rowsDisplayList.DragOver, columnsDisplayList.DragOver

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then _
            Exit Sub
        e.Effect = DragDropEffects.Move

    End Sub

    Private Sub List_DragDrop(sender As Object, e As Windows.Forms.DragEventArgs) Handles columnsDisplayList.DragDrop, rowsDisplayList.DragDrop

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

        Dim selectedListBox As vListBox = CType(sender, vListBox)

        'Get the TreeNode being dragged
        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        'The target node should be selected from the DragOver event
        'Dim targetNode As TreeNode = selectedTreeview.SelectedNode

        If dimensionsList.Contains(dropNode.Name) = False Then
            Dim item = selectedListBox.Items.Add(dropNode.Text)
            item.Value = dropNode.Name
        End If

    End Sub

#End Region


#Region "Move Up and Down"

    Private Sub columnsDisplayList_KeyDown(sender As Object, e As KeyEventArgs) Handles columnsDisplayList.KeyDown, rowsDisplayList.KeyDown

        Select Case e.KeyCode
            Case Keys.Delete
                Dim selectedListBox As vListBox = CType(sender, vListBox)
                Dim itemValue = selectedListBox.SelectedItem.Value
                selectedListBox.Items.Remove(selectedListBox.SelectedItem)
                dimensionsList.Remove(itemValue)
            Case Keys.Up
                If e.Control Then
                    MoveItemUp(sender)
                End If
            Case Keys.Down
                If e.Control Then
                    MoveItemDown(sender)
                End If
        End Select

    End Sub

    'Move up
    Private Sub MoveItemUp(ByRef listBox As vListBox)

        'Make sure our item is not the first one on the list.
        If listBox.SelectedIndex > 0 Then
            Dim I = listBox.SelectedIndex - 1
            listBox.Items.Insert(listBox.SelectedItem, I)
            listBox.Items.RemoveAt(listBox.SelectedIndex)
            listBox.SelectedIndex = I
        End If

    End Sub

    'Move down
    Private Sub MoveItemDown(ByRef listBox As vListBox)

        'Make sure our item is not the last one on the list.
        If listBox.SelectedIndex < listBox.Items.Count - 1 Then
            'Insert places items above the index you supply, since we want
            'to move it down the list we have to do + 2
            Dim I = listBox.SelectedIndex + 2
            listBox.Items.Insert(listBox.SelectedItem, I)
            listBox.Items.RemoveAt(listBox.SelectedIndex)
            listBox.SelectedIndex = I - 1
        End If

    End Sub

#End Region



#Region "Call Backs"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RaiseEvent RefreshOrder()
    End Sub

#End Region



End Class
