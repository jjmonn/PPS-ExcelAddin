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
' Last modified: 07/11/2015



Public Class CUI2RightPane


#Region "Instance Variables"

    ' Objects
    Private m_analysisAxisTreeview As New vTreeView
    Private m_dimensionsList As New List(Of String)
    Private m_process As CRUD.Account.AccountProcess


#End Region


#Region "Initialization"

    Friend Sub New(ByRef p_process As CRUD.Account.AccountProcess, _
                   ByRef p_entitiesFiltersNode As TreeNode, _
                   ByRef p_clientsFiltersNode As TreeNode, _
                   ByRef p_productsFiltersNode As TreeNode, _
                   ByRef p_adjustmentsFiltersNode As TreeNode)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        m_process = p_process
        DimensionsTVPanel.Controls.Add(m_analysisAxisTreeview)
        m_analysisAxisTreeview.Dock = DockStyle.Fill

        Select Case m_process
            Case CRUD.Account.AccountProcess.FINANCIAL : InitializeFinancialDimensions(p_entitiesFiltersNode, _
                                                                               p_clientsFiltersNode, _
                                                                               p_productsFiltersNode, _
                                                                               p_adjustmentsFiltersNode)

            Case CRUD.Account.AccountProcess.RH : InitializePDCDimensions(p_entitiesFiltersNode, _
                                                                   p_clientsFiltersNode, _
                                                                   p_productsFiltersNode, _
                                                                   p_adjustmentsFiltersNode)
        End Select

        ' Init listboxes
        rowsDisplayList.ItemHeight = 17
        columnsDisplayList.ItemHeight = 17
        rowsDisplayList.HotTrack = True
        columnsDisplayList.HotTrack = True
        rowsDisplayList.AllowDragDrop = True
        columnsDisplayList.AllowDragDrop = True
        rowsDisplayList.AllowDrop = True
        columnsDisplayList.AllowDrop = True

        AddHandlers()
        MultilangueSetup()

    End Sub

    Private Sub InitializeFinancialDimensions(ByRef p_entitiesFiltersNode As TreeNode, _
                                               ByRef p_clientsFiltersNode As TreeNode, _
                                               ByRef p_productsFiltersNode As TreeNode, _
                                               ByRef p_adjustmentsFiltersNode As TreeNode)

        FinancialProcessDimensionsDisplayPaneSetup(p_entitiesFiltersNode, _
                                                    p_clientsFiltersNode, _
                                                    p_productsFiltersNode, _
                                                    p_adjustmentsFiltersNode)
        ' By default add the fisrt node
        Dim accountsItem = rowsDisplayList.Items.Add(m_analysisAxisTreeview.Nodes(0).Text)
        accountsItem.Value = m_analysisAxisTreeview.Nodes(0).Value
        Dim entitiesItem = rowsDisplayList.Items.Add(m_analysisAxisTreeview.Nodes(1).Text)
        entitiesItem.Value = m_analysisAxisTreeview.Nodes(1).Value
        Dim yearsItem = columnsDisplayList.Items.Add(m_analysisAxisTreeview.Nodes(2).Text)
        yearsItem.Value = m_analysisAxisTreeview.Nodes(2).Value

        m_dimensionsList.Add(accountsItem.Value)
        m_dimensionsList.Add(entitiesItem.Value)
        m_dimensionsList.Add(yearsItem.Value)

    End Sub

    Private Sub InitializePDCDimensions(ByRef p_entitiesFiltersNode As TreeNode, _
                                               ByRef p_clientsFiltersNode As TreeNode, _
                                               ByRef p_productsFiltersNode As TreeNode, _
                                               ByRef p_adjustmentsFiltersNode As TreeNode)

        PDCProcessDimensionsDisplayPaneSetup(p_entitiesFiltersNode, _
                                            p_clientsFiltersNode, _
                                            p_productsFiltersNode, _
                                            p_adjustmentsFiltersNode)

        ' Default PDC report
        Dim accountsItem = rowsDisplayList.Items.Add(m_analysisAxisTreeview.Nodes(0).Text)
        accountsItem.Value = m_analysisAxisTreeview.Nodes(0).Value
        m_dimensionsList.Add(accountsItem.Value)

        Dim l_clientsItem = rowsDisplayList.Items.Add(m_analysisAxisTreeview.Nodes(5).Text)
        l_clientsItem.Value = m_analysisAxisTreeview.Nodes(5).Value
        m_dimensionsList.Add(l_clientsItem.Value)

        Dim l_weeksItem = columnsDisplayList.Items.Add(m_analysisAxisTreeview.Nodes(2).Text)
        l_weeksItem.Value = m_analysisAxisTreeview.Nodes(2).Value
        m_dimensionsList.Add(l_weeksItem.Value)
        
    End Sub

    Private Sub AddHandlers()

        'analysis_axis_tv.EnableIndicatorsAnimation = False
        'analysis_axis_tv.DefaultExpandCollapseIndicators = True
        '   analysis_axis_tv.ShowRootLines = true

        ' Tv Drag and drop
        '   AddHandler analysis_axis_tv.DragDrop, AddressOf vTreeView1_DragDrop
        AddHandler m_analysisAxisTreeview.MouseDown, AddressOf vTreeView1_MouseDown
        AddHandler m_analysisAxisTreeview.AfterSelect, AddressOf vTreeView_AfterSelect

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

    Private Sub FinancialProcessDimensionsDisplayPaneSetup(ByRef p_entitiesFiltersNode As TreeNode, _
                                                           ByRef p_clientsFiltersNode As TreeNode, _
                                                           ByRef p_productsFiltersNode As TreeNode, _
                                                           ByRef p_adjustmentsFiltersNode As TreeNode)

        VTreeViewUtil.InitTVFormat(m_analysisAxisTreeview)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS, ControllingUI_2.ACCOUNTS_CODE, m_analysisAxisTreeview)

        ' Entities Analysis Axis and Categories Nodes
        Dim entities_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES, _
                                                               ControllingUI_2.ENTITIES_CODE, _
                                                               m_analysisAxisTreeview)

        For Each entity_node As TreeNode In p_entitiesFiltersNode.Nodes
            FiltersNodeSubCategoriesInit(entity_node, entities_node)
        Next

        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.YEARS, ControllingUI_2.YEARS_CODE, m_analysisAxisTreeview)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.MONTHS, ControllingUI_2.MONTHS_CODE, m_analysisAxisTreeview)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS, ControllingUI_2.VERSIONS_CODE, m_analysisAxisTreeview)

        ' Clients Analysis Axis and Categories Nodes
        Dim clientsNode As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS, _
                                                             ControllingUI_2.CLIENTS_CODE, _
                                                             m_analysisAxisTreeview)
        For Each client_category_node As TreeNode In p_clientsFiltersNode.Nodes
            FiltersNodeSubCategoriesInit(client_category_node, clientsNode)
        Next

        ' Products Analysis Axis and Categories Nodes
        Dim products_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.PRODUCTS,
                                                               ControllingUI_2.PRODUCTS_CODE, _
                                                               m_analysisAxisTreeview)

        For Each product_category_node As TreeNode In p_productsFiltersNode.Nodes
            FiltersNodeSubCategoriesInit(product_category_node, products_node)
        Next

        Dim adjustment_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER _
                              & GlobalEnums.AnalysisAxis.ADJUSTMENTS, _
                              ControllingUI_2.ADJUSTMENT_CODE, m_analysisAxisTreeview)

        For Each adjustment_category_node As TreeNode In p_adjustmentsFiltersNode.Nodes
            FiltersNodeSubCategoriesInit(adjustment_category_node, adjustment_node)
        Next


    End Sub

    Private Sub PDCProcessDimensionsDisplayPaneSetup(ByRef p_entitiesFiltersNode As TreeNode, _
                                                     ByRef p_clientsFiltersNode As TreeNode, _
                                                     ByRef p_productsFiltersNode As TreeNode, _
                                                     ByRef p_adjustmentsFiltersNode As TreeNode)

        VTreeViewUtil.InitTVFormat(m_analysisAxisTreeview)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ACCOUNTS, _
                              ControllingUI_2.ACCOUNTS_CODE, m_analysisAxisTreeview)

        ' Entities Analysis Axis and Categories Nodes
        Dim entities_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.ENTITIES, _
                                                               ControllingUI_2.ENTITIES_CODE, _
                                                               m_analysisAxisTreeview)

        For Each entity_node As TreeNode In p_entitiesFiltersNode.Nodes
            FiltersNodeSubCategoriesInit(entity_node, entities_node)
        Next

        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.WEEKS, ControllingUI_2.WEEKS_CODE, m_analysisAxisTreeview)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.DAYS, ControllingUI_2.DAYS_CODE, m_analysisAxisTreeview)
        VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.VERSIONS, ControllingUI_2.VERSIONS_CODE, m_analysisAxisTreeview)

        ' Clients Analysis Axis and Categories Nodes
        Dim clientsNode As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.CLIENTS, _
                                                             ControllingUI_2.CLIENTS_CODE, _
                                                             m_analysisAxisTreeview)
        For Each client_category_node As TreeNode In p_clientsFiltersNode.Nodes
            FiltersNodeSubCategoriesInit(client_category_node, clientsNode)
        Next

        ' Products Analysis Axis and Categories Nodes
        Dim products_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER & GlobalEnums.AnalysisAxis.PRODUCTS,
                                                               ControllingUI_2.PRODUCTS_CODE, _
                                                               m_analysisAxisTreeview)

        For Each product_category_node As TreeNode In p_productsFiltersNode.Nodes
            FiltersNodeSubCategoriesInit(product_category_node, products_node)
        Next

        Dim adjustment_node As vTreeNode = VTreeViewUtil.AddNode(Computer.AXIS_DECOMPOSITION_IDENTIFIER _
                              & GlobalEnums.AnalysisAxis.ADJUSTMENTS, _
                              ControllingUI_2.ADJUSTMENT_CODE, m_analysisAxisTreeview)

        For Each adjustment_category_node As TreeNode In p_adjustmentsFiltersNode.Nodes
            FiltersNodeSubCategoriesInit(adjustment_category_node, adjustment_node)
        Next


    End Sub

    Private Sub FiltersNodeSubCategoriesInit(ByRef originNode As TreeNode, _
                                             ByRef destinationNode As vTreeNode)

        Dim destSubNode As vTreeNode = VTreeViewUtil.AddNode(Computer.FILTERS_DECOMPOSITION_IDENTIFIER & originNode.Name, _
                                                             originNode.Text, _
                                                             destinationNode)

        For Each originSubNode As TreeNode In originNode.Nodes
            FiltersNodeSubCategoriesInit(originSubNode, destSubNode)
        Next

    End Sub

    Private Sub MultilangueSetup()

        Me.m_columnsLabel.Text = Local.GetValue("CUI.columns_label")
        Me.m_rowsLabel.Text = Local.GetValue("CUI.rows_label")
        Me.UpdateBT.Text = Local.GetValue("CUI.refresh")
        Me.m_fieldChoiceLabel.Text = Local.GetValue("CUI.fields_choice")

    End Sub

#End Region


#Region "Interface"

    Public Function DimensionsListContainsItem(ByRef item As String) As Boolean

        Return m_dimensionsList.Contains(item)

    End Function

    Public Sub AddItemToColumnsHierarchy(ByRef text As String,
                                         ByRef id As String)

        Dim item = columnsDisplayList.Items.Add(text)
        item.Value = id
        m_dimensionsList.Add(id)

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
        Me.m_analysisAxisTreeview.Capture = False
    End Sub

    Private Sub vTreeView1_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)

        Dim node As vTreeNode = VTreeViewUtil.GetNodeAtPosition(Me.m_analysisAxisTreeview, e.Location)
        If node IsNot Nothing Then
            Me.m_analysisAxisTreeview.DoDragDrop(node, DragDropEffects.All)
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
            If m_dimensionsList.Contains(e.SourceItem.Value) = False Then
                m_dimensionsList.Add(e.SourceItem.Value)
            End If
        End If
        rowsDisplayList.StopDraggingTimer()
        columnsDisplayList.StopDraggingTimer()

    End Sub

    Private Sub rowsDisplayList_ItemDragEnding(ByVal sender As Object, ByVal e As ListItemDragCancelEventArgs)

        Dim columnsDisplayListScreenRectangle As Drawing.Rectangle = Me.columnsDisplayList.RectangleToScreen(columnsDisplayList.ClientRectangle)
        If columnsDisplayListScreenRectangle.Contains(Cursor.Position) Then
            vListBox.DropItem(Me.rowsDisplayList, Me.columnsDisplayList, e.SourceItem)
            If m_dimensionsList.Contains(e.SourceItem.Value) = False Then
                m_dimensionsList.Add(e.SourceItem.Value)
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
            If m_dimensionsList.Contains(dropNode.Value) = False Then
                If m_dimensionsList.Count >= 6 Then
                    MsgBox("Maximum allowed number of dimensions is 6.")
                Else
                    Dim item = selectedListBox.Items.Add(dropNode.Text)
                    item.Value = dropNode.Value
                    m_dimensionsList.Add(dropNode.Value)
                End If
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
                m_dimensionsList.Remove(itemValue)
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
