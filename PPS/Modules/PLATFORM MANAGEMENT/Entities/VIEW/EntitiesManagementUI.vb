' AssetManagementUI.vb
'
' entities view
'
'
' To do: 
'       - Drag and drop -> Not implemented because the credentials levels must be updated when drag and drop
'       - Open as full window !!
'     
'
' Known bugs:
'
'
'
' Last modified: 12/03/2014
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.DataGridView.Filters
Imports VIBlend.Utilities
Imports System.Drawing


Friend Class EntitiesManagementUI


#Region "Instance Variables"

    ' Objects
    Private Controller As EntitiesController
    Private entitiesTV As TreeView
    Friend EntitiesDGVMGT As EntitiesDGV

    ' Variables
    Friend entitiesNameKeyDic As Hashtable
    Friend currentRowItem As HierarchyItem
    Private tmpSplitterDistance As Double
    Private menuFlag As Boolean
    Private selectionFlag As Boolean

  
#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef input_controller As EntitiesController, _
                   ByRef input_entitites_name_key_dic As Hashtable, _
                   ByRef input_entities_TV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        entitiesTV = input_entities_TV
        entitiesNameKeyDic = input_entitites_name_key_dic

        EntitiesDGVMGT = New EntitiesDGV(Me, entitiesDGV, Controller)
        Me.WindowState = FormWindowState.Maximized
        entitiesDGV.ImageList = EntitiesTVImageList
        entitiesTV.ImageList = EntitiesTVImageList
        entitiesTV.ContextMenuStrip = RCM_TV
      
        AddHandler entitiesDGV.CellMouseClick, AddressOf dataGridView_CellMouseClick
        'AddHandler entitiesDGV.Click, AddressOf dataGridView_CellMouseClick
        AddHandler entitiesDGV.HierarchyItemMouseClick, AddressOf dataGridView_HierarchyItemMouseClick
      
    End Sub

    Private Sub EntitiesManagementUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        entitiesDGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)

    End Sub

#End Region


#Region "Calls Back"

    Private Sub AddEntity_cmd_Click(sender As Object, e As EventArgs) Handles AddSubEntityToolStripMenuItem.Click, _
                                                                              CreateANewEntityToolStripMenuItem.Click
        Me.Hide()
        Controller.ShowNewEntityUI()

    End Sub

    Private Sub DeleteEntity_cmd_Click(sender As Object, e As EventArgs) Handles DeleteEntityToolStripMenuItem.Click, _
                                                                                 DeleteEntityToolStripMenuItem1.Click

        If Not currentRowItem Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the entity " + Chr(13) + Chr(13) + _
                                                     entitiesDGV.CellsArea.GetCellValue(currentRowItem, entitiesDGV.ColumnsHierarchy.Items(0)) + Chr(13) + Chr(13) + _
                                                     "This entity and all sub entities will be deleted, do you confirm?" + Chr(13) + Chr(13), _
                                                     "Entity deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Dim entity_id As String = entitiesNameKeyDic(entitiesDGV.CellsArea.GetCellValue(currentRowItem, entitiesDGV.ColumnsHierarchy.Items(0)))
                Controller.DeleteEntities(entitiesTV.Nodes.Find(entity_id, True)(0))
            End If
        Else
            MsgBox("An Entity must be selected in order to be deleted")
        End If
        currentRowItem = Nothing

    End Sub

    Private Sub CopyDownBT_Click(sender As Object, e As EventArgs)

        EntitiesDGVMGT.CopyValueDown()
        entitiesDGV.Refresh()

    End Sub

    Private Sub dropInExcel_cmd_Click(sender As Object, e As EventArgs) Handles DropHierarchyToExcelToolStripMenuItem.Click, _
                                                                                SendEntitiesHierarchyToExcelToolStripMenuItem.Click
        EntitiesDGVMGT.DropInExcel()

    End Sub

    Private Sub ExitBT_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click

        Me.Dispose()
        Me.Close()

    End Sub


#End Region


#Region "DGV Right Click Menu"

    Private Sub cop_down_bt_Click(sender As Object, e As EventArgs) Handles copy_down_bt.Click

        EntitiesDGVMGT.CopyValueDown()

    End Sub

    Private Sub drop_to_excel_bt_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        EntitiesDGVMGT.DropInExcel()

    End Sub

    Private Sub CreateEntityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateEntityToolStripMenuItem.Click

        AddEntity_cmd_Click(sender, e)

    End Sub

    Private Sub DeleteEntityToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteEntityToolStripMenuItem2.Click

        DeleteEntity_cmd_Click(sender, e)

    End Sub

#End Region


#Region "DGV Events"

    Private Sub dataGridView_CellMouseClick(ByVal sender As Object, ByVal args As CellMouseEventArgs)

        EntitiesDGVMGT.current_cell = args.Cell
        currentRowItem = args.Cell.RowItem

    End Sub

    Private Sub dataGridView_HierarchyItemMouseClick(sender As Object, args As HierarchyItemMouseEventArgs)

        currentRowItem = args.HierarchyItem
      
    End Sub

    Private Sub entitiesTGV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteEntity_cmd_Click(sender, e)
        End Select

    End Sub

#End Region


#Region "Form Events"

    Private Sub AssetsManagementUI_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Controller.SendNewPositionsToModel()

    End Sub


#End Region




End Class