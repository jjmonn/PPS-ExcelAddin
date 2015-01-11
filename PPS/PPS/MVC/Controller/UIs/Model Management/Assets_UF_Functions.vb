Module Assets_UF_Functions

    '---------------------------------------------------------------------------------
    ' Common procedure for Accounts Treeviews
    '---------------------------------------------------------------------------------

    Public Function LoadAssetTree(TV As Windows.Forms.TreeView) As Collections.Hashtable

        '-----------------------------------------------------------------
        ' Load Asset into the specified treeview
        ' Input: Treeview name
        ' Output: Hash table containing asset extra data
        '-----------------------------------------------------------------
        Dim RST As ADODB.Recordset = New ADODB.Recordset
        Dim Formula As String
        Dim nodeX, ParentNode() As Windows.Forms.TreeNode
        Dim assetdataHT As Collections.Hashtable = New Collections.Hashtable
        Dim i As Integer

        On Error GoTo ErrorHandler
        TV.Nodes.Clear()
        RST.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        RST.Open(ASSETS_TABLE, _
                     ConnectioN, _
                     ADODB.CursorTypeEnum.adOpenDynamic, _
                     ADODB.LockTypeEnum.adLockOptimistic, _
                     ADODB.CommandTypeEnum.adCmdTable)
        RST.Sort = ASSETS_POSITION_VARIABLE

        ' Tree loading
        If RST.RecordCount > 0 Then
            RST.MoveFirst()                                    ' Move the first element of the recordset
            Do While RST.EOF = False                           ' While / until all nodes are loaded

                ' Root node treatment
                If Trim(RST.Fields(ASSETS_PARENT_ID_VARIABLE).Value) = TV_ROOT_KEY Then

                    nodeX = TV.Nodes.Add(Trim(RST.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                         Trim(RST.Fields(ASSETS_NAME_VARIABLE).Value))
                Else   ' Other nodes treatement

                    ParentNode = TV.Nodes.Find(Trim(RST.Fields(ASSETS_PARENT_ID_VARIABLE).Value), True)
                    nodeX = ParentNode(0).Nodes.Add(Trim(RST.Fields(ASSETS_TREE_ID_VARIABLE).Value), _
                                           Trim(RST.Fields(ASSETS_NAME_VARIABLE).Value))
                End If
                nodeX.EnsureVisible()
                ' Saving extra data
                Dim AssetExtraData(3) As Object
                AssetExtraData(ASSET_EXTRA_ARRAY_AFFILIATE) = RST.Fields(ASSETS_AFFILIATE_ID_VARIABLE).Value
                AssetExtraData(ASSET_EXTRA_ARRAY_CATEGORY) = RST.Fields(ASSETS_CATEGORY_VARIABLE).Value
                AssetExtraData(ASSET_EXTRA_ARRAY_COUNTRY) = RST.Fields(ASSETS_COUNTRY_VARIABLE).Value
                AssetExtraData(ASSET_EXTRA_ARRAY_CURRENCY) = RST.Fields(ASSETS_CURRENCY_VARIABLE).Value
                assetdataHT.Add(RST.Fields(ASSETS_TREE_ID_VARIABLE).Value, AssetExtraData)
                RST.MoveNext()
            Loop
        End If
        RST.Close()
        LoadAssetTree = assetdataHT
        Exit Function

ErrorHandler:
        MsgBox(Err.Number & Err.Description)
        Exit Function

    End Function

    Public Sub updateAssTable(TV As Windows.Forms.TreeView, assetdataHT As Collections.Hashtable)

        '---------------------------------------------------------------------------------
        ' Update the Assets table (DataBase) from the treeview
        '---------------------------------------------------------------------------------
        Dim position As Integer
        Dim criteria As String
        Dim AssetExtraData() As Object
        Dim rst As ADODB.Recordset = New ADODB.Recordset
        rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient

        rst.Open(ASSETS_TABLE, ConnectioN, _
                 ADODB.CursorTypeEnum.adOpenDynamic, _
                 ADODB.LockTypeEnum.adLockBatchOptimistic, _
                 ADODB.CommandTypeEnum.adCmdTable)

        For Each TreeNode As Windows.Forms.TreeNode In TV.Nodes
            AssetExtraData = assetdataHT.Item(TreeNode.Name)                   ' Save asset extra data

            criteria = ASSETS_TREE_ID_VARIABLE & "=" & "'" & TreeNode.Name & "'"
            rst.Find(criteria, , , 1)                                          ' Look for the key in recordset

            If rst.EOF Or rst.BOF Then                                         ' Item NOT FOUND
                AddAssetRecord(TreeNode, rst, AssetExtraData, position)          ' Add New record
            Else                                                               ' Item FOUND
                UpdateAssetRecord(TreeNode, rst, AssetExtraData, position)       ' Update record
            End If
            ' If children : upload children
            If TreeNode.Nodes.Count > 0 Then UpdateAssetChild(TreeNode, rst, position, assetdataHT)
        Next
        rst.UpdateBatch()
    End Sub

    Private Sub UpdateAssetChild(TreeNode As Windows.Forms.TreeNode, rst As ADODB.Recordset, _
                                 ByRef position As Integer, assetdataHT As Collections.Hashtable)

        '-------------------------------------------------------------------
        ' Update Child nodes
        '-------------------------------------------------------------------
        Dim AssetExtraData() As Object
        Dim criteria As String

        For Each ChildNode As Windows.Forms.TreeNode In TreeNode.Nodes
            AssetExtraData = assetdataHT.Item(ChildNode.Name)                       ' Save asset extra data
            criteria = ASSETS_TREE_ID_VARIABLE & "=" & "'" & ChildNode.Name & "'"
            rst.Find(criteria, , , 1)                                               ' Look for the key in recordset

            If rst.EOF Or rst.BOF Then                                              ' Item NOT FOUND
                AddAssetRecord(ChildNode, rst, AssetExtraData, position)              ' Add New record
            Else                                                                    ' Item FOUND
                UpdateAssetRecord(ChildNode, rst, AssetExtraData, position)           ' Update record
            End If
            ' If child recursive call
            If ChildNode.Nodes.Count > 0 Then UpdateAssetChild(ChildNode, rst, position, assetdataHT)
        Next

    End Sub

    Private Sub AddAssetRecord(inputNode As Windows.Forms.TreeNode, ByRef rst As ADODB.Recordset, _
                                  AssetExtraData() As Object, ByRef position As Integer)

        '-------------------------------------------------------------------
        ' Add a new record in the table (DataBase)
        '-------------------------------------------------------------------
        rst.AddNew()
        If Not inputNode.Parent Is Nothing Then
            rst(ASSETS_PARENT_ID_VARIABLE).Value = inputNode.Parent.Name
        Else
            rst(ASSETS_PARENT_ID_VARIABLE).Value = TV_ROOT_KEY
        End If
        rst(ASSETS_TREE_ID_VARIABLE).Value = inputNode.Name
        rst(ASSETS_NAME_VARIABLE).Value = inputNode.Text
        rst(ASSETS_POSITION_VARIABLE).Value = position
        ' Extra Data
        rst(ASSETS_AFFILIATE_ID_VARIABLE).Value = AssetExtraData(ASSET_EXTRA_ARRAY_AFFILIATE)
        rst(ASSETS_CATEGORY_VARIABLE).Value = AssetExtraData(ASSET_EXTRA_ARRAY_CATEGORY)
        rst(ASSETS_COUNTRY_VARIABLE).Value = AssetExtraData(ASSET_EXTRA_ARRAY_COUNTRY)
        rst(ASSETS_CURRENCY_VARIABLE).Value = AssetExtraData(ASSET_EXTRA_ARRAY_CURRENCY)
        position = position + 1

    End Sub

    Private Sub UpdateAssetRecord(inputNode As Windows.Forms.TreeNode, ByRef rst As ADODB.Recordset, _
                                     AssetExtraData() As Object, ByRef position As Integer)

        '-------------------------------------------------------------------
        ' Update Account Reccord
        '-------------------------------------------------------------------
        On Error GoTo ErrorHandler
        If inputNode.Name = rst.Fields(ASSETS_TREE_ID_VARIABLE).Value Then            ' Check key equality
            If rst.Fields(ASSETS_NAME_VARIABLE).Value <> inputNode.Text Then rst.Fields(ASSETS_NAME_VARIABLE).Value = inputNode.Text
            If rst.Fields(ASSETS_POSITION_VARIABLE).Value <> position Then rst.Fields(ASSETS_POSITION_VARIABLE).Value = position
            If Not inputNode.Parent Is Nothing Then
                If rst.Fields(ASSETS_PARENT_ID_VARIABLE).Value <> inputNode.Parent.Name Then rst.Fields(ASSETS_PARENT_ID_VARIABLE).Value = inputNode.Parent.Name
            End If
            If rst.Fields(ASSETS_AFFILIATE_ID_VARIABLE).Value <> AssetExtraData(ASSET_EXTRA_ARRAY_AFFILIATE) Then rst.Fields(ASSETS_AFFILIATE_ID_VARIABLE).Value = AssetExtraData(ASSET_EXTRA_ARRAY_AFFILIATE)
            If rst.Fields(ASSETS_CATEGORY_VARIABLE).Value <> AssetExtraData(ASSET_EXTRA_ARRAY_CATEGORY) Then rst.Fields(ASSETS_CATEGORY_VARIABLE).Value = AssetExtraData(ASSET_EXTRA_ARRAY_CATEGORY)
            If rst.Fields(ASSETS_COUNTRY_VARIABLE).Value <> AssetExtraData(ASSET_EXTRA_ARRAY_COUNTRY) Then rst.Fields(ASSETS_COUNTRY_VARIABLE).Value = AssetExtraData(ASSET_EXTRA_ARRAY_COUNTRY)
            If rst.Fields(ASSETS_CURRENCY_VARIABLE).Value <> AssetExtraData(ASSET_EXTRA_ARRAY_CURRENCY) Then rst.Fields(ASSETS_CURRENCY_VARIABLE).Value = AssetExtraData(ASSET_EXTRA_ARRAY_CURRENCY)
            position = position + 1
        Else
            'error message : should never happen
        End If
        Exit Sub

ErrorHandler:
        MsgBox(Err.Number & Err.Description)
    End Sub

End Module
