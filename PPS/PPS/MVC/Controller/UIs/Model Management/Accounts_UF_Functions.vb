Module Accounts_UF_Functions

    '---------------------------------------------------------------------------------
    ' Common procedure for Accounts Treeviews
    '---------------------------------------------------------------------------------

    Public Function LoadAccountsTreeview(TV As Windows.Forms.TreeView) As Collections.Hashtable

        '--------------------------------------------------------------------------------------
        ' Function: Load Accounts Treeview
        ' Loads the hierarchy saved in the account table into the destination tree
        ' Output : Array containing Accounts Formula Type (0), formula (1)
        '--------------------------------------------------------------------------------------

        ' Variables declaration
        Dim RST As ADODB.Recordset = New ADODB.Recordset
        Dim nodeX, ParentNode() As Windows.Forms.TreeNode
        Dim imageIndex As Integer
        Dim selected_ImageIndex As Integer
        Dim FormulaHT As Collections.Hashtable = New Collections.Hashtable
        Dim i As Integer
        '----------------------------------------

        On Error GoTo ErrorHandler
        TV.Nodes.Clear()
        RST.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        RST.Open(ACCOUNTS_TEMP_TABLE, _
                 ConnectioN, _
                 ADODB.CursorTypeEnum.adOpenDynamic, _
                 ADODB.LockTypeEnum.adLockOptimistic, _
                 ADODB.CommandTypeEnum.adCmdTable)

        RST.Sort = ACCOUNT_POSITION_VARIABLE
        If RST.RecordCount > 0 Then
            RST.MoveFirst()                                      ' Move the first element of the recordset

            Do While RST.EOF = False                             ' While / until all nodes are loaded
                ' Root node treatment
                If Trim(RST.Fields(ACCOUNT_PARENT_ID_VARIABLE).Value) = TV_ROOT_KEY Then

                    nodeX = TV.Nodes.Add(Trim(RST.Fields(ACCOUNT_TREE_ID_VARIABLE).Value), _
                                         Trim(RST.Fields(ACCOUNT_NAME_VARIABLE).Value))
                    nodeX.EnsureVisible()
                    ' Saving formula's data
                    Dim temparray(1) As String
                    TempArray(0) = RST.Fields(ACCOUNT_FORMULA_TYPE_VARIABLE).Value
                    TempArray(1) = RST.Fields(ACCOUNT_FORMULA_VARIABLE).Value
                    FormulaHT.Add(RST.Fields(ACCOUNT_TREE_ID_VARIABLE).Value, TempArray)

                Else   ' Other nodes treatement

                    ParentNode = TV.Nodes.Find(Trim(RST.Fields(ACCOUNT_PARENT_ID_VARIABLE).Value), True)
                    nodeX = ParentNode(0).Nodes.Add(Trim(RST.Fields(ACCOUNT_TREE_ID_VARIABLE).Value), _
                                           Trim(RST.Fields(ACCOUNT_NAME_VARIABLE).Value))
                    nodeX.EnsureVisible()

                    ' Saving formula's data
                    Dim temparray(1) As String
                    TempArray(0) = RST.Fields(ACCOUNT_FORMULA_TYPE_VARIABLE).Value
                    TempArray(1) = RST.Fields(ACCOUNT_FORMULA_VARIABLE).Value
                    FormulaHT.Add(RST.Fields(ACCOUNT_TREE_ID_VARIABLE).Value, TempArray)
                End If
                RST.MoveNext()
            Loop
        End If
        RST.Close()
        LoadAccountsTreeview = FormulaHT
        Exit Function

ErrorHandler:
        MsgBox(Err.Number & Err.Description)
        Exit Function

    End Function

    Public Sub updateAccTable(TV As Windows.Forms.TreeView, formulaHT As Collections.Hashtable)

        '-------------------------------------------------------------------
        ' Update Account Table
        ' Save the Treeview hierarchy and formulaHT content into the accounts
        ' table in the database
        ' Inputs: 1. Input node
        '         2. Table
        '         3. FormulaHT
        '-------------------------------------------------------------------

        Dim position As Integer
        Dim Formula, criteria, FormulaType As String
        Dim rst As ADODB.Recordset = New ADODB.Recordset
        rst.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        rst.Open(ACCOUNTS_TEMP_TABLE, _
                   ConnectioN, _
                   ADODB.CursorTypeEnum.adOpenDynamic, _
                   ADODB.LockTypeEnum.adLockBatchOptimistic, _
                   ADODB.CommandTypeEnum.adCmdTable)

        For Each TreeNode As Windows.Forms.TreeNode In TV.Nodes           ' Loop through TV.nodes
            FormulaType = formulaHT(TreeNode.Name)(0)                     ' Save Formula type   
            Formula = formulaHT(TreeNode.Name)(1)                         ' Save formula

            criteria = ACCOUNT_TREE_ID_VARIABLE & "=" & "'" & TreeNode.Name & "'"
            rst.Find(criteria, , , 1)                ' Look for the key in recordset

            If rst.EOF Or rst.BOF Then                                              ' Item NOT FOUND
                AddAccRecord(TreeNode, rst, Formula, FormulaType, position)         ' Add New record
            Else                                                                    ' Item FOUND
                UpdateAccRecord(TreeNode, rst, Formula, FormulaType, position)      ' Update record
            End If
            ' If children : upload children
            If TreeNode.Nodes.Count > 0 Then UpdateChild(TreeNode, rst, position, formulaHT)
        Next
        rst.UpdateBatch()
    End Sub

    Private Sub UpdateChild(TreeNode As Windows.Forms.TreeNode, _
                                 rst As ADODB.Recordset, ByRef position As Integer, _
                                 formulaHT As Collections.Hashtable)

        '-------------------------------------------------------------------
        ' Sub: Update Child -> To DB
        ' Sub called from the sub above (UpdateAccTable) if children need to
        ' be written into the database
        '-------------------------------------------------------------------
        Dim Formula, FormulaType, criteria As String

        For Each ChildNode As Windows.Forms.TreeNode In TreeNode.Nodes                ' Loop through TV.nodes
            FormulaType = formulaHT(ChildNode.Name)(0)                           ' Save Formula type   
            Formula = formulaHT(ChildNode.Name)(1)                               ' Save formula

            criteria = ACCOUNT_TREE_ID_VARIABLE & "=" & "'" & ChildNode.Name & "'"
            rst.Find(criteria, , , 1)                ' Look for the key in recordset

            If rst.EOF Or rst.BOF Then                                                ' Item NOT FOUND
                AddAccRecord(ChildNode, rst, Formula, FormulaType, position)          ' Add New record
            Else                                                                      ' Item FOUND
                UpdateAccRecord(ChildNode, rst, Formula, FormulaType, position)       ' Update record
            End If
            ' If children : upload children
            If ChildNode.Nodes.Count > 0 Then UpdateChild(ChildNode, rst, position, formulaHT)
        Next
    End Sub


    Private Sub AddAccRecord(inputNode As Windows.Forms.TreeNode, ByRef rst As ADODB.Recordset, _
                                  Formula As String, FormulaType As String, _
                                  position As Integer)

        '-------------------------------------------------------------------
        ' Sub: Save Account Reccord
        ' (Update table new record case) called by the two precedent functions
        '-------------------------------------------------------------------
        rst.AddNew()
            If Not inputNode.Parent Is Nothing Then
                rst(ACCOUNT_PARENT_ID_VARIABLE).Value = inputNode.Parent.Name
            Else
            rst(ACCOUNT_PARENT_ID_VARIABLE).Value = TV_ROOT_KEY
            End If
        rst(ACCOUNT_TREE_ID_VARIABLE).Value = inputNode.Text
        rst(ACCOUNT_NAME_VARIABLE).Value = inputNode.Text
        rst(ACCOUNT_POSITION_VARIABLE).Value = position
        rst(ACCOUNT_FORMULA_TYPE_VARIABLE).Value = FormulaType
        rst(ACCOUNT_FORMULA_VARIABLE).Value = Formula
        rst(ACCOUNT_TREE_VARIABLE).Value = "COA"
        position = position + 1

    End Sub


    Private Sub UpdateAccRecord(inputNode As Windows.Forms.TreeNode, ByRef rst As ADODB.Recordset, _
                                     Formula As String, FormulaType As String, ByRef position As Integer)

        '-------------------------------------------------------------------
        ' Sub: Update Account Reccord
        ' Case record is existing and need to be updated
        '-------------------------------------------------------------------

        On Error GoTo ErrorHandler
        If inputNode.name = rst.Fields(ACCOUNT_TREE_ID_VARIABLE).Value Then            ' Check key equality

            If rst.Fields(ACCOUNT_NAME_VARIABLE).Value <> inputNode.Text Then rst.Fields(ACCOUNT_NAME_VARIABLE).Value = inputNode.Text
            If rst.Fields(ACCOUNT_POSITION_VARIABLE).Value <> position Then rst.Fields(ACCOUNT_POSITION_VARIABLE).Value = position
            If Not inputNode.parent Is Nothing Then
                If rst.Fields(ACCOUNT_PARENT_ID_VARIABLE).Value <> inputNode.parent.name Then rst.Fields(ACCOUNT_PARENT_ID_VARIABLE).Value = inputNode.parent.name
            End If
            If rst.Fields(ACCOUNT_FORMULA_TYPE_VARIABLE).Value <> FormulaType Then rst.Fields(ACCOUNT_FORMULA_TYPE_VARIABLE).Value = FormulaType
            If rst.Fields(ACCOUNT_FORMULA_VARIABLE).Value <> Formula Then rst.Fields(ACCOUNT_FORMULA_VARIABLE).Value = Formula
            position = position + 1
            'rst.Update()
        Else
            'error message : should never happen
        End If
        Exit Sub

ErrorHandler:
        MsgBox(Err.Number & Err.Description)

    End Sub



End Module
