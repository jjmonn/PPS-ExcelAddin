Public Class DownloadUI

    Private EntitiesFlag As Boolean
    Private AccountsFlag As Boolean
    Private PeriodsFlag As Boolean
    

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        initGroups()
        initButtons()
        initTreeViews()

        AssetsSel.AllowDrop = True
        AccountsSel.AllowDrop = True
        PeriodsSel.AllowDrop = True

        Period_GB.Hide()
        Assets_GB.Hide()
        Accounts_GB.Hide()
        Panel1.Show()

    End Sub

    Private Sub initGroups()

        '-------------------------------------------------------------------
        ' Initialize the size of the groups
        '-------------------------------------------------------------------
        Dim Trees As New Collections.Generic.List(Of Windows.Forms.TreeView) From {Asset_TV, PeriodTV, AccountsTV}
        Dim Lists As New Collections.Generic.List(Of Windows.Forms.ListView) From {AssetsSel, PeriodsSel, AccountsSel}

        For Each Tree As Windows.Forms.TreeView In Trees
            With Tree
                .Top = TREEVIEWS_TOP
                .Left = TREEVIEWS_LEFT
                .Height = TREEVIEWS_HEIGHT
                .Width = TREEVIEWS_WIDTH
            End With
        Next

        For Each ListItem As Windows.Forms.ListView In Lists
            With ListItem
                .Top = LISTS_TOP
                .Left = LISTS_LEFT
                .Height = LISTS_HEIGHT
                .Width = LISTS_WIDTH
            End With
        Next
    End Sub

    Private Sub initButtons()

        '----------------------------------------------------------------
        ' Initialize the Main menu
        '----------------------------------------------------------------

        '--------------------------------------------------------------------------------------
        ' Entities
        '--------------------------------------------------------------------------------------
        Dim BT_Entities As New Windows.Forms.Button                             ' Entities Button 
        Dim LabelEntities As New Windows.Forms.Label                            ' Entities Label
        Dim LineEntities As New System.Windows.Forms.Label                      ' Entities Line

        With BT_Entities
            .Name = "BT_Entities"
            .Text = ""
            '.BackgroundImage = My.Resources.Down_blue
            .FlatAppearance.BorderColor = Panel1.BackColor
            .Top = BT_ASSETS_ORIGINAL_TOP
            .Left = CONTROLS_LEFT
            .Height = BUTTON_HEIGHT
            .Width = BUTTON_WIDTH
        End With
        AddHandler BT_Entities.Click, AddressOf EntitiesClick                   ' Add the control click.handler
        Panel1.Controls.Add(BT_Entities)

        With LabelEntities
            .Name = "LabelEntities"
            .Text = "Entities Selection"
            .Top = BT_ASSETS_ORIGINAL_TOP
            .Left = LABEL_LEFT
            .Width = LABEL_WIDTH
        End With
        AddHandler LabelEntities.Click, AddressOf EntitiesClick                ' Add the control click.handler
        Panel1.Controls.Add(LabelEntities)

        With LineEntities
            .Name = "LineEntities"
            .Location = New System.Drawing.Point(5, BT_ASSETS_LINE_ORIGINAL_TOP)
            .Size = New System.Drawing.Size(LINE_LENGHT, LINE_WIDTH)
            .BorderStyle = Windows.Forms.BorderStyle.None
            .BackColor = Drawing.Color.AliceBlue
            .Text = ""
        End With
        Panel1.Controls.Add(LineEntities)

        '--------------------------------------------------------------------------------------
        ' Accounts
        '--------------------------------------------------------------------------------------
        Dim BT_Accounts As New Windows.Forms.Button                             ' Account Button
        Dim LabelAccounts As New Windows.Forms.Label                            ' Accounts Label
        Dim LineAccounts As New System.Windows.Forms.Label                      ' Accounts Line

        With BT_Accounts
            .Name = "BT_Accounts"
            .Text = ""
            '.BackgroundImage = My.Resources.Down_blue
            .FlatAppearance.BorderColor = Panel1.BackColor
            .Top = BT_ACCOUNTS_ORIGINAL_TOP
            .Left = CONTROLS_LEFT
            .Height = BUTTON_HEIGHT
            .Width = BUTTON_WIDTH
        End With
        AddHandler BT_Accounts.Click, AddressOf AccountsClick                   ' Add the control click.handler
        Panel1.Controls.Add(BT_Accounts)

        With LabelAccounts
            .Name = "LabelAccounts"
            .Text = "Accounts Selection"
            .Top = BT_ACCOUNTS_ORIGINAL_TOP
            .Left = LABEL_LEFT
            .Width = LABEL_WIDTH
        End With
        AddHandler LabelAccounts.Click, AddressOf AccountsClick                   ' Add the control click.handler
        Panel1.Controls.Add(LabelAccounts)

        With LineAccounts
            .Name = "LineAccounts"
            .Location = New System.Drawing.Point(5, BT_ACCOUNTS_LINE_ORIGINAL_TOP)
            .Size = New System.Drawing.Size(LINE_LENGHT, LINE_WIDTH)
            .BorderStyle = Windows.Forms.BorderStyle.None
            .BackColor = Drawing.Color.AliceBlue
            .Text = ""
        End With
        Panel1.Controls.Add(LineAccounts)

        '--------------------------------------------------------------------------------------
        ' Periods
        '--------------------------------------------------------------------------------------
        Dim BT_periods As New Windows.Forms.Button                              ' Periods Button
        Dim Labelperiods As New Windows.Forms.Label                             ' periods Label
        Dim Lineperiods As New System.Windows.Forms.Label                       ' Periods Line

        With BT_periods
            .Name = "BT_periods"
            .Text = ""
            '.BackgroundImage = My.Resources.Down_blue
            .FlatAppearance.BorderColor = Panel1.BackColor
            .Top = BT_PERIODS_ORIGINAL_TOP
            .Left = CONTROLS_LEFT
            .Height = BUTTON_HEIGHT
            .Width = BUTTON_WIDTH
        End With
        AddHandler BT_periods.Click, AddressOf PeriodsClick                    ' Add the control click.handler
        Panel1.Controls.Add(BT_periods)

        With Labelperiods
            .Name = "Labelperiods"
            .Text = "Periods Selection"
            .Top = BT_PERIODS_ORIGINAL_TOP
            .Left = LABEL_LEFT
            .Width = LABEL_WIDTH
        End With
        AddHandler Labelperiods.Click, AddressOf PeriodsClick                    ' Add the control click.handler
        Panel1.Controls.Add(Labelperiods)

        With Lineperiods
            .Name = "Lineperiods"
            .Location = New System.Drawing.Point(5, BT_PERIODS_LINE_ORIGINAL_TOP)
            .Size = New System.Drawing.Size(LINE_LENGHT, LINE_WIDTH)
            .BorderStyle = Windows.Forms.BorderStyle.None
            .BackColor = Drawing.Color.AliceBlue
            .Text = ""
        End With
        Panel1.Controls.Add(Lineperiods)
    End Sub

    Private Sub initTreeViews()

        '---------------------------------------------------------------
        ' Initialize the Treeviews
        '---------------------------------------------------------------
        LoadAssetTree(Asset_TV)
        LoadAccountsTreeview(AccountsTV)


    End Sub

    Private Sub EntitiesClick(sender As Object, e As EventArgs)

        '-----------------------------------------------------
        ' Display or hide Entities selection
        '-----------------------------------------------------
        Panel1.AutoScrollPosition = New System.Drawing.Point(0)
        With Assets_GB                                          ' Set it location
            .Top = BT_ASSETS_LINE_ORIGINAL_TOP + V_MARGINS
            .Left = GROUPS_LEFT
            .Height = GROUPS_HEIGHT
            .Width = GROUPS_WIDTH
        End With

        Panel1.Controls("BT_Accounts").Top = BT_ACCOUNTS_ORIGINAL_TOP + GROUPS_HEIGHT + V_MARGINS * 2
        Panel1.Controls("LabelAccounts").Top = BT_ACCOUNTS_ORIGINAL_TOP + GROUPS_HEIGHT + V_MARGINS * 2
        Panel1.Controls("LineAccounts").Top = BT_ACCOUNTS_LINE_ORIGINAL_TOP + GROUPS_HEIGHT + V_MARGINS * 2

        Panel1.Controls("BT_periods").Top = BT_PERIODS_ORIGINAL_TOP + GROUPS_HEIGHT + V_MARGINS * 2
        Panel1.Controls("Labelperiods").Top = BT_PERIODS_ORIGINAL_TOP + GROUPS_HEIGHT + V_MARGINS * 2
        Panel1.Controls("Lineperiods").Top = BT_PERIODS_LINE_ORIGINAL_TOP + GROUPS_HEIGHT + V_MARGINS * 2

        Accounts_GB.Hide()
        Period_GB.Hide()
        Assets_GB.Show()                                        ' Show Asset Selection group
        
    End Sub

    Private Sub AccountsClick(sender As Object, e As EventArgs)

        '-----------------------------------------------------
        ' Display or hide Accounts selection
        '-----------------------------------------------------
        Panel1.AutoScrollPosition = New System.Drawing.Point(0)
        With Accounts_GB                                          ' Set it location
            .Top = BT_ACCOUNTS_LINE_ORIGINAL_TOP + V_MARGINS
            .Left = GROUPS_LEFT
            .Height = GROUPS_HEIGHT
            .Width = GROUPS_WIDTH
        End With

        Panel1.Controls("BT_Accounts").Top = BT_ACCOUNTS_ORIGINAL_TOP
        Panel1.Controls("LabelAccounts").Top = BT_ACCOUNTS_ORIGINAL_TOP
        Panel1.Controls("LineAccounts").Top = BT_ACCOUNTS_LINE_ORIGINAL_TOP

        Panel1.Controls("BT_periods").Top = BT_PERIODS_ORIGINAL_TOP + GROUPS_HEIGHT + V_MARGINS * 2
        Panel1.Controls("Labelperiods").Top = BT_PERIODS_ORIGINAL_TOP + GROUPS_HEIGHT + V_MARGINS * 2
        Panel1.Controls("Lineperiods").Top = BT_PERIODS_LINE_ORIGINAL_TOP + GROUPS_HEIGHT + V_MARGINS * 2

        Period_GB.Hide()
        Assets_GB.Hide()
        Accounts_GB.Show()
        
    End Sub

    Private Sub PeriodsClick(sender As Object, e As EventArgs)

        '-----------------------------------------------------
        ' Display or hide Periods selection
        '-----------------------------------------------------
        Panel1.AutoScrollPosition = New System.Drawing.Point(0)
        With Period_GB                                          ' Set it location
            .Top = BT_PERIODS_LINE_ORIGINAL_TOP + V_MARGINS
            .Left = GROUPS_LEFT
            .Height = GROUPS_HEIGHT
            .Width = GROUPS_WIDTH
        End With

        Panel1.Controls("BT_Accounts").Top = BT_ACCOUNTS_ORIGINAL_TOP
        Panel1.Controls("LabelAccounts").Top = BT_ACCOUNTS_ORIGINAL_TOP
        Panel1.Controls("LineAccounts").Top = BT_ACCOUNTS_LINE_ORIGINAL_TOP

        Panel1.Controls("BT_periods").Top = BT_PERIODS_ORIGINAL_TOP
        Panel1.Controls("Labelperiods").Top = BT_PERIODS_ORIGINAL_TOP
        Panel1.Controls("Lineperiods").Top = BT_PERIODS_LINE_ORIGINAL_TOP

        Period_GB.Show()
        Assets_GB.Hide()
        Accounts_GB.Hide()

    End Sub

    Private Sub Download_cmd_Click(sender As Object, e As EventArgs) Handles Download_cmd.Click

        '--------------------------------------------------------------------
        ' Launch the download
        '--------------------------------------------------------------------
        Dim affiliatesList() As String = {".1"}
        Dim MGD As New ModelGetData()

        MGD.computeMultipleAsset(affiliatesList)

        Dim DV As New DataViewUI
        DV.DataGridView1.DataSource = MGD.DataSource
        DV.TextBox1.Text = MGD.MAPP.getAssetName(affiliatesList(0))            ' ! ! to be filled
        Me.AddOwnedForm(DV)
        DGVsetUpDisplay(DV.DataGridView1)
        DV.Show()

        ' Me.Close()

        'Dim i As Integer

        'apps.Sheets(1).cells(1, 1) = DB.mapp.GetValue(ASSETS_TABLE, affiliateslist(0), ASSETS_NAME_VARIABLE, ASSETS_TREE_ID_VARIABLE)     ' Write Asset Name
        'For i = LBound(periodList) To UBound(periodList)     ' Write periods
        '    apps.Sheets(1).Cells(1, i + 2) = periodList(i)
        'Next i

        'i = 0
        'DB.mapp.accountsRST.Sort = ACCOUNT_POSITION_VARIABLE
        'DB.mapp.accountsRST.MoveFirst()
        'Do While DB.mapp.accountsRST.EOF = False
        '    apps.Sheets(1).cells(i + 3, 1) = DB.mapp.accountsRST.Fields(ACCOUNT_NAME_VARIABLE).Value
        '    Dim TempArray() As Double
        '    If DB.dataHT.ContainsKey(DB.mapp.accountsRST.Fields(ACCOUNT_TREE_ID_VARIABLE).Value) Then
        '        TempArray = DB.dataHT.Item(DB.mapp.accountsRST.Fields(ACCOUNT_TREE_ID_VARIABLE).Value)
        '        Dim Destination As Excel.Range = apps.Sheets(1).Cells(i + 3, 2)
        '        Destination = Destination.Resize(1, UBound(TempArray) + 1)
        '        Destination.Value = TempArray                                  ' 1 dim array write
        '    End If
        '        DB.mapp.accountsRST.MoveNext()
        '        i = i + 1
        'Loop


    End Sub


    '---------------------------------------------------------------------------------------
    ' Nodes Drag and Drop Procedure
    '---------------------------------------------------------------------------------------

    Private Sub TVs_ItemDrag(sender As Object, e As Windows.Forms.ItemDragEventArgs) _
                Handles AccountsTV.ItemDrag, Asset_TV.ItemDrag, PeriodTV.ItemDrag

        '----------------------------------------------------------------------------------
        ' Set the drag node and initiate the dragDrop
        '----------------------------------------------------------------------------------
        DoDragDrop(e.Item, Windows.Forms.DragDropEffects.Move)

    End Sub

    Private Sub AccountsTreeview_DragEnter(sender As Object, e As Windows.Forms.DragEventArgs) _
                Handles AccountsTV.DragEnter, Asset_TV.DragEnter, PeriodTV.DragEnter

        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) Then    'See if there is a TreeNode being dragged
            e.Effect = Windows.Forms.DragDropEffects.Move                       'TreeNode found allow move effect
        Else
            e.Effect = Windows.Forms.DragDropEffects.None                       'No TreeNode found, prevent move
        End If
    End Sub

    Private Sub ListView_DragOver(sender As Object, e As Windows.Forms.DragEventArgs) _
                Handles AccountsSel.DragOver, AssetsSel.DragOver, PeriodsSel.DragOver

        'Check that there is a TreeNode being dragged 
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedListView As Windows.Forms.ListView = CType(sender, Windows.Forms.ListView)

        'As the mouse moves over nodes, provide feedback to the user by highlighting the node that is the current drop target
        Dim targetItem As Windows.Forms.ListViewItem = selectedListView.GetItemAt(e.X, e.Y)

        'Currently selected node is a suitable target
        e.Effect = Windows.Forms.DragDropEffects.Move

    End Sub

    Private Sub AccountsTreeview_DragDrop(sender As Object, e As Windows.Forms.DragEventArgs) _
                Handles AccountsSel.DragDrop, PeriodsSel.DragDrop, AssetsSel.DragDrop

        'Check that there is a TreeNode being dragged
        If e.Data.GetDataPresent("System.Windows.Forms.TreeNode", True) = False Then Exit Sub

        'Get the TreeView raising the event (incase multiple on form)
        Dim selectedListView As Windows.Forms.ListView = CType(sender, Windows.Forms.ListView)

        'Get the TreeNode being dragged
        Dim dropNode As Windows.Forms.TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"),  _
                                                       Windows.Forms.TreeNode)

        ' Add the node and childs node to the selected list view
        selectedListView.Items.Add(dropNode.Text)
        'ListViewAdd(dropNode, selectedListView)
        
    End Sub

    Private Sub ListViewAdd(Node As Windows.Forms.TreeNode, ListV As Windows.Forms.ListView)

        ListV.Items.Add(Node.Text)
        'For Each child As Windows.Forms.TreeNode In Node.Nodes

        'Next

    End Sub


End Class