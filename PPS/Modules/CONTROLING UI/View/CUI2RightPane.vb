Imports VIBlend.WinForms.Controls
Imports System.Collections.Generic
Imports System.Windows.Forms

' DisplayControl.vb
'
' User interface for configuring controllingUI2 display
' Allows user ot drag and drop nodes from analysis axis treeview to rows_display_tv
'
'
'
'
' Author: Julien Monnereau
' Last modified: 17/08/2015



Public Class CUI2RightPane


#Region "Instance Variables"

    ' Objects
    Private analysis_axis_tv As vTreeView
    Private dimensionsList As New List(Of String)
   
#End Region


#Region "Interface"

    Public Sub New(ByRef analysis_axis_tv As vTreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.analysis_axis_tv = analysis_axis_tv
        DimensionsTVPanel.Controls.Add(analysis_axis_tv)
        analysis_axis_tv.Dock = DockStyle.Fill

        ' Init listboxes
        rowsDisplayList.ItemHeight = 17
        columnsDisplayList.ItemHeight = 17
        rowsDisplayList.HotTrack = True
        columnsDisplayList.HotTrack = True
        rowsDisplayList.AllowDragDrop = True
        columnsDisplayList.AllowDragDrop = True
        rowsDisplayList.AllowDrop = True
        columnsDisplayList.AllowDrop = True

        ' By default add the fisrt node
        Dim accountsItem = rowsDisplayList.Items.Add(analysis_axis_tv.Nodes(0).Text)
        accountsItem.Value = analysis_axis_tv.Nodes(0).Value
        Dim entitiesItem = rowsDisplayList.Items.Add(analysis_axis_tv.Nodes(1).Text)
        entitiesItem.Value = analysis_axis_tv.Nodes(1).Value
        Dim yearsItem = columnsDisplayList.Items.Add(analysis_axis_tv.Nodes(2).Text)
        yearsItem.Value = analysis_axis_tv.Nodes(2).Value

        dimensionsList.Add(accountsItem.Value)
        dimensionsList.Add(entitiesItem.Value)
        dimensionsList.Add(yearsItem.Value)

        AddHandlers()

    End Sub

    Private Sub AddHandlers()

        'analysis_axis_tv.EnableIndicatorsAnimation = False
        'analysis_axis_tv.DefaultExpandCollapseIndicators = True
        '   analysis_axis_tv.ShowRootLines = true

        ' Tv Drag and drop
        '   AddHandler analysis_axis_tv.DragDrop, AddressOf vTreeView1_DragDrop
        AddHandler analysis_axis_tv.MouseDown, AddressOf vTreeView1_MouseDown
        AddHandler analysis_axis_tv.AfterSelect, AddressOf vTreeView_AfterSelect

        ' List Drag and Drop
        AddHandler rowsDisplayList.ItemDragging, AddressOf rowsDisplayList_ItemDragging
        AddHandler columnsDisplayList.ItemDragging, AddressOf columnsDisplayList_ItemDragging

        AddHandler rowsDisplayList.ItemDragEnding, AddressOf rowsDisplayList_ItemDragEnding
        AddHandler columnsDisplayList.ItemDragEnding, AddressOf columnsDisplayList_ItemDragEnding

        AddHandler rowsDisplayList.ItemDragStarting, AddressOf rowsDisplayList_ItemDragStarting
        AddHandler columnsDisplayList.ItemDragStarting, AddressOf columnsDisplayList_ItemDragStarting
        AddHandler rowsDisplayList.MouseLeave, AddressOf rowsDisplayList_MouseLeave
        AddHandler columnsDisplayList.MouseLeave, AddressOf columnsDisplayList_MouseLeave

    End Sub

    Public Function DimensionsListContainsItem(ByRef item As String) As Boolean

        Return dimensionsList.Contains(item)

    End Function

    Public Sub AddItemToColumnsHierarchy(ByRef text As String,
                                         ByRef id As String)

        Dim item = columnsDisplayList.Items.Add(text)
        item.Value = id
        dimensionsList.Add(id)

    End Sub

#End Region


#Region "TreeNode"

    'Private Sub analysisAxisTV_ItemDrag(sender As Object, e As ItemDragEventArgs)

    '    DoDragDrop(e.Item, Windows.Forms.DragDropEffects.Move)

    'End Sub

    'Private Sub analysisAxisTV_DragEnter(sender As Object, e As Windows.Forms.DragEventArgs)

    '    If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then
    '        e.Effect = DragDropEffects.Move
    '    Else
    '        e.Effect = DragDropEffects.None
    '    End If
    'End Sub

    Private Sub vTreeView_AfterSelect(ByVal sender As Object, ByVal e As vTreeViewEventArgs)
        Me.analysis_axis_tv.Capture = False
    End Sub

    Private Sub vTreeView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
        Dim point As Drawing.Point = e.Location
        Dim node As vTreeNode = Me.analysis_axis_tv.HitTest(e.Location)
        If node IsNot Nothing Then
            Me.analysis_axis_tv.DoDragDrop(node, DragDropEffects.All)
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

        Dim rowsDisplayListScreenRectangle As Drawing.Rectangle = Me.rowsDisplayList.RectangleToScreen(rowsDisplayList.ClientRectangle)
        If rowsDisplayListScreenRectangle.Contains(Cursor.Position) Then
            vListBox.DropItem(Me.columnsDisplayList, Me.rowsDisplayList, e.SourceItem)
            If dimensionsList.Contains(e.SourceItem.Value) = False Then
                dimensionsList.Add(e.SourceItem.Value)
            End If
        End If
        rowsDisplayList.StopDraggingTimer()
        columnsDisplayList.StopDraggingTimer()

    End Sub

    Private Sub rowsDisplayList_ItemDragEnding(ByVal sender As Object, ByVal e As ListItemDragCancelEventArgs)

        Dim columnsDisplayListScreenRectangle As Drawing.Rectangle = Me.columnsDisplayList.RectangleToScreen(columnsDisplayList.ClientRectangle)
        If columnsDisplayListScreenRectangle.Contains(Cursor.Position) Then
            vListBox.DropItem(Me.rowsDisplayList, Me.columnsDisplayList, e.SourceItem)
            If dimensionsList.Contains(e.SourceItem.Value) = False Then
                dimensionsList.Add(e.SourceItem.Value)
            End If
        End If
        rowsDisplayList.StopDraggingTimer()
        columnsDisplayList.StopDraggingTimer()

    End Sub

    Private rowsDisplayListGraphics As Drawing.Graphics
    Private columnsDisplayListGraphics As Drawing.Graphics

    Private Sub columnsDisplayList_ItemDragging(ByVal sender As Object, ByVal e As ListItemDragEventArgs)

        Dim rowsDisplayListScreenRectangle As Drawing.Rectangle = Me.rowsDisplayList.RectangleToScreen(rowsDisplayList.ClientRectangle)
        If rowsDisplayListScreenRectangle.Contains(Cursor.Position) Then
            If Me.rowsDisplayListGraphics Is Nothing Then
                Me.rowsDisplayListGraphics = Me.rowsDisplayList.CreateGraphics()
            End If

            Dim point As Drawing.Point = Me.rowsDisplayList.PointToClient(Cursor.Position)
            vListBox.DrawDragFeedback(Me.rowsDisplayListGraphics, Me.rowsDisplayList)
        Else
            Me.rowsDisplayList.Invalidate()
            Me.columnsDisplayList.Invalidate()
        End If

    End Sub

    Private Sub rowsDisplayList_ItemDragging(ByVal sender As Object, ByVal e As ListItemDragEventArgs)

        Dim columnsDisplayListScreenRectangle As Drawing.Rectangle = Me.columnsDisplayList.RectangleToScreen(columnsDisplayList.ClientRectangle)
        If columnsDisplayListScreenRectangle.Contains(Cursor.Position) Then
            If Me.columnsDisplayListGraphics Is Nothing Then
                Me.columnsDisplayListGraphics = Me.columnsDisplayList.CreateGraphics()
            End If

            Dim point As Drawing.Point = Me.columnsDisplayList.PointToClient(Cursor.Position)
            vListBox.DrawDragFeedback(Me.columnsDisplayListGraphics, Me.columnsDisplayList)
        Else
            Me.rowsDisplayList.Invalidate()
            Me.columnsDisplayList.Invalidate()
        End If

    End Sub

#End Region


#Region "Lists Node Drop"

    Private Sub Lists_DragOver(sender As Object, e As Windows.Forms.DragEventArgs) Handles rowsDisplayList.DragOver, columnsDisplayList.DragOver

        e.Effect = DragDropEffects.Move

    End Sub

    Private Sub List_DragDrop(sender As Object, e As DragEventArgs) Handles columnsDisplayList.DragDrop, rowsDisplayList.DragDrop

        Dim selectedListBox As vListBox = CType(sender, vListBox)
        Dim dropNode As vTreeNode = TryCast(e.Data.GetData(GetType(vTreeNode)), vTreeNode)
        If dropNode IsNot Nothing Then
            If dimensionsList.Contains(dropNode.Value) = False Then
                Dim item = selectedListBox.Items.Add(dropNode.Text)
                item.Value = dropNode.Value
                dimensionsList.Add(dropNode.Value)
            End If
        End If

    End Sub

#End Region


#Region "Move Up and Down"

    Private Sub DisplayLists_KeyDown(sender As Object, e As KeyEventArgs) Handles columnsDisplayList.KeyDown, rowsDisplayList.KeyDown

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



End Class
