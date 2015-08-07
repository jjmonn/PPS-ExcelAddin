' DisplayControl.vb
'
' User interface for configuring controllingUI2 display
' Allows user ot drag and drop nodes from analysis axis treeview to rows_display_tv
'
'
'
'
' Author: Julien Monnereau
' Last modified: 03/08/2015



Public Class DisplayControl


#Region "Instance Variables"

    ' Objects
    Private analysis_axis_tv As TreeView



#End Region


#Region "Interface"

    Public Sub New(ByRef analysis_axis_tv As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.analysis_axis_tv = analysis_axis_tv
        TableLayoutPanel1.Controls.Add(analysis_axis_tv, 0, 0)
        TableLayoutPanel1.SetColumnSpan(analysis_axis_tv, 2)
        analysis_axis_tv.Dock = DockStyle.Fill
        rows_display_tv.AllowDrop = True
        columns_display_tv.AllowDrop = True

        ' By default add the fisrt node
        rows_display_tv.Nodes.Add(analysis_axis_tv.Nodes(0).Name, analysis_axis_tv.Nodes(0).Text)
        rows_display_tv.Nodes.Add(analysis_axis_tv.Nodes(1).Name, analysis_axis_tv.Nodes(1).Text)
        columns_display_tv.Nodes.Add(analysis_axis_tv.Nodes(2).Name, analysis_axis_tv.Nodes(2).Text)

        AddHandler analysis_axis_tv.ItemDrag, AddressOf analysisAxisTV_ItemDrag
        AddHandler analysis_axis_tv.DragEnter, AddressOf analysisAxisTV_DragEnter

    End Sub

#End Region


#Region "Drag and Drop Procedure"

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

    Private Sub TV_DragOver(sender As Object, e As Windows.Forms.DragEventArgs) Handles rows_display_tv.DragOver, columns_display_tv.DragOver

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub
        e.Effect = DragDropEffects.Move

    End Sub

    Private Sub TV_DragDrop(sender As Object, e As Windows.Forms.DragEventArgs) Handles rows_display_tv.DragDrop, columns_display_tv.DragDrop

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

        Dim selectedTreeview As TreeView = CType(sender, Windows.Forms.TreeView)

        'Get the TreeNode being dragged
        Dim dropNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        'The target node should be selected from the DragOver event
        Dim targetNode As TreeNode = selectedTreeview.SelectedNode

        If columns_display_tv.Nodes.Find(dropNode.Name, True).Count = 0 _
        AndAlso rows_display_tv.Nodes.Find(dropNode.Name, True).Count = 0 Then
            selectedTreeview.Nodes.Add(dropNode.Name, dropNode.Text)
        End If

    End Sub

#End Region


End Class
