' clientsControl.vb
'
' Drop to excel !!
'
'
'
' Author: Julien Monnereau
' Last modified: 26/04/2015


Imports VIBlend.WinForms.DataGridView
Imports System.Collections
Imports System.Collections.Generic
Imports System.Windows.Forms


Friend Class ClientsControl


#Region "Instance variables"

    ' Objects
    Private clientsDGV As AnalysisAxisDGV
    Private controller As clientsController
    Private newAnalysisAxisUI As NewAnalysisAxisUI
    Private categoriesTV As TreeView

    ' Variables
    Private categoriesNameKeyDic As Hashtable

#End Region


#Region "Initialize"

    Protected Friend Sub New(ByRef controller As ClientsController, _
                             ByRef categoriesTV As Windows.Forms.TreeView, _
                             ByRef values_dict As Hashtable, _
                             ByRef input_categoriesNameKeyDic As Hashtable)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.controller = controller
        Me.categoriesTV = categoriesTV
        Me.categoriesNameKeyDic = input_categoriesNameKeyDic
        newAnalysisAxisUI = New NewAnalysisAxisUI("client", categoriesTV)
        clientsDGV = New AnalysisAxisDGV(categoriesTV, values_dict)
        TableLayoutPanel1.Controls.Add(clientsDGV.DGV, 0, 1)
        clientsDGV.DGV.Dock = Windows.Forms.DockStyle.Fill
        clientsDGV.DGV.ContextMenuStrip = RCM_TGV

        AddHandler clientsDGV.DGV.CellValueChanging, AddressOf DGV_CellValueChanging
        AddHandler clientsDGV.DGV.KeyDown, AddressOf DGV_KeyDown
        AddHandler newAnalysisAxisUI.ValidateBT.Click, AddressOf NewAnalysisAxisValidate_Click

    End Sub

    Protected Friend Sub closeControl()


    End Sub

#End Region


#Region "Interface"

    Protected Friend Sub addclientRow(ByRef item_id As String, _
                                       ByRef itemHT As Hashtable)

        clientsDGV.isFillingDGV = True
        clientsDGV.addRow(item_id, itemHT)
        clientsDGV.isFillingDGV = False

    End Sub

#End Region


#Region "Events"

    Private Sub DGV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteclientMBT_Click(sender, e)
        End Select

    End Sub

    Private Sub DGV_CellValueChanging(sender As Object, args As CellCancelEventArgs)

        If clientsDGV.isFillingDGV = False Then
            Dim value As Object
            Select Case args.Cell.ColumnItem.ItemIndex
                Case 0 : If controller.updateclientName(args.Cell.RowItem.Caption, args.Cell.Editor.EditorValue) = False Then args.Cancel = True
                Case Else
                    If categoriesNameKeyDic.ContainsKey(args.Cell.Editor.EditorValue) = True Then
                        value = categoriesNameKeyDic(args.Cell.Editor.EditorValue)
                        controller.UpdateclientFilter(args.Cell.RowItem.Caption, _
                                                         clientsDGV.columnsCaptionIDDict(args.Cell.ColumnItem.Caption), _
                                                         value)
                    Else
                        args.Cancel = True
                    End If
            End Select
        End If

    End Sub

    Private Sub NewAnalysisAxisValidate_Click(sender As Object, e As EventArgs)

        Dim hash As New Hashtable
        hash.Add(NAME_VARIABLE, newAnalysisAxisUI.nameTextEditor.Text)
        For Each categoryNode As TreeNode In categoriesTV.Nodes
            Dim categoryValueText As String = newAnalysisAxisUI.Controls.Find(categoryNode.Name + "CB", True)(0).Text
            If categoryValueText <> "" Then
                hash.Add(categoryNode.Name, categoriesNameKeyDic(categoryValueText))
            Else
                hash.Add(categoryNode.Name, categoryNode.Name & NON_ATTRIBUTED_SUFIX)
            End If
        Next
        controller.createclient(hash)
        newAnalysisAxisUI.Hide()
        clientsDGV.DGV.Refresh()

    End Sub

#End Region


#Region "Call Backs"

    Private Sub NewClientMBT_Click(sender As Object, e As EventArgs) Handles NewClientMBT.Click, NewClientRCMBT.Click

        newAnalysisAxisUI.Show()

    End Sub

    Private Sub DeleteClientMBT_Click(sender As Object, e As EventArgs) Handles DeleteClientMBT.Click, DeleteClientRCMBT.Click

        If Not clientsDGV.current_row Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the client " + Chr(13) + Chr(13) + _
                                                clientsDGV.DGV.CellsArea.GetCellValue(clientsDGV.current_row, clientsDGV.DGV.ColumnsHierarchy.Items(0)),
                                                "Entity deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                controller.deleteclient(clientsDGV.current_row.Caption)
                clientsDGV.current_row.Delete()
                clientsDGV.current_row = Nothing
                clientsDGV.DGV.Refresh()
            End If
        Else
            MsgBox("A product must be selected first")
        End If

    End Sub

    Private Sub CopyDownRCMBT_Click(sender As Object, e As EventArgs) Handles CopyDownRCMBT.Click

    End Sub

    Private Sub DropToExcelRCMBT_Click(sender As Object, e As EventArgs) Handles DropToExcelRCMBT.Click, ExcelDropMBT.Click

    End Sub

#End Region


End Class
