' EntitiesControl.vb
'
' to be reviewed for proper disctinction between DGV and control !!
'
'
'
' Known bugs:
'
'
'
' Last modified: 08/05/2014
' Author: Julien Monnereau


Imports System.Windows.Forms
Imports System.Collections
Imports System.Collections.Generic
Imports VIBlend.WinForms.DataGridView
Imports VIBlend.WinForms.DataGridView.Filters
Imports VIBlend.Utilities
Imports System.Drawing
Imports System.ComponentModel


Friend Class EntitiesControl


#Region "Instance Variables"

    ' Objects
    Private Controller As EntitiesController
    Friend EntitiesDGV As EntitiesDGV
    Private entitiesTV As TreeView
    Private CP As CircularProgressUI

    ' Variables
    Protected Friend entitiesNameKeyDic As Hashtable
    Private categoriesNameKeyDic As Hashtable
    Private tmpSplitterDistance As Double
    Private menuFlag As Boolean
    Private selectionFlag As Boolean


#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef input_controller As EntitiesController, _
                            ByRef input_entitiesTV As TreeView, _
                            ByRef input_entitiesNameKeyDic As Hashtable, _
                            ByRef input_categoriesNameKeyDic As Hashtable, _
                            ByRef categoriesTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        entitiesTV = input_entitiesTV
        entitiesNameKeyDic = input_entitiesNameKeyDic
        categoriesNameKeyDic = input_categoriesNameKeyDic
        EntitiesDGV = New EntitiesDGV(entitiesTV, categoriesTV, input_categoriesNameKeyDic)

        Me.TableLayoutPanel1.Controls.Add(EntitiesDGV.DGV, 0, 1)
        EntitiesDGV.DGV.Dock = DockStyle.Fill
        EntitiesDGV.DGV.ContextMenuStrip = RCM_TGV

        AddHandler EntitiesDGV.DGV.CellValueChanged, AddressOf dataGridView_CellValueChanged
        AddHandler EntitiesDGV.DGV.KeyDown, AddressOf entitiesDGV_KeyDown

    End Sub

    Protected Friend Sub closeControl()

        CP = New CircularProgressUI(Drawing.Color.Yellow, "Saving")
        CP.Show()
        BackgroundWorker1.RunWorkerAsync()

    End Sub

#End Region


#Region "Calls Back"

    Private Sub AddEntity_cmd_Click(sender As Object, e As EventArgs) Handles CreateEntityToolStripMenuItem.Click

        Me.Hide()
        Controller.ShowNewEntityUI()

    End Sub

    Private Sub DeleteEntity_cmd_Click(sender As Object, e As EventArgs) Handles DeleteEntityToolStripMenuItem.Click

        If Not EntitiesDGV.currentRowItem Is Nothing Then
            Dim confirm As Integer = MessageBox.Show("Careful, you are about to delete the entity " + Chr(13) + Chr(13) + _
                                                     EntitiesDGV.DGV.CellsArea.GetCellValue(EntitiesDGV.currentRowItem, EntitiesDGV.DGV.ColumnsHierarchy.Items(0)) + Chr(13) + Chr(13) + _
                                                     "This entity and all sub entities will be deleted, do you confirm?" + Chr(13) + Chr(13), _
                                                     "Entity deletion confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If confirm = DialogResult.Yes Then
                Dim entity_id As String = entitiesNameKeyDic(EntitiesDGV.DGV.CellsArea.GetCellValue(EntitiesDGV.currentRowItem, EntitiesDGV.DGV.ColumnsHierarchy.Items(0)))
                Controller.DeleteEntities(entitiesTV.Nodes.Find(entity_id, True)(0))
            End If
        Else
            MsgBox("An Entity must be selected in order to be deleted")
        End If
        EntitiesDGV.currentRowItem = Nothing

    End Sub

    Private Sub dropInExcel_cmd_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        EntitiesDGV.DropInExcel()

    End Sub

#End Region


#Region "DGV Right Click Menu"

    Private Sub cop_down_bt_Click(sender As Object, e As EventArgs) Handles copy_down_bt.Click

        EntitiesDGV.CopyValueDown()
        EntitiesDGV.DGV.Refresh()

    End Sub

    Private Sub drop_to_excel_bt_Click(sender As Object, e As EventArgs) Handles drop_to_excel_bt.Click

        EntitiesDGV.DropInExcel()

    End Sub

    Private Sub CreateEntityToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateEntityToolStripMenuItem.Click

        AddEntity_cmd_Click(sender, e)

    End Sub

    Private Sub DeleteEntityToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles DeleteEntityToolStripMenuItem2.Click

        DeleteEntity_cmd_Click(sender, e)

    End Sub

#End Region


#Region "DGV Events"

    Private Sub entitiesDGV_KeyDown(sender As Object, e As KeyEventArgs)

        Select Case e.KeyCode
            Case Keys.Delete : DeleteEntity_cmd_Click(sender, e)
        End Select

    End Sub

    Private Sub dataGridView_CellValueChanged(sender As Object, args As CellEventArgs)

        If EntitiesDGV.isFillingDGV = False Then
            Dim value As Object
            If args.Cell.ColumnItem.Caption <> EntitiesDGV.CURRENCY_COLUMN_NAME Then value = categoriesNameKeyDic(args.Cell.Value) Else value = args.Cell.Value
            Controller.UpdateEntity(entitiesNameKeyDic(EntitiesDGV.DGV.CellsArea.GetCellValue(args.Cell.RowItem, EntitiesDGV.DGV.ColumnsHierarchy.Items(0))), _
            EntitiesDGV.columnsCaptionID(args.Cell.ColumnItem.Caption), _
          value)
        End If

    End Sub

#End Region


#Region "Utilities"

    Protected Friend Function getDGVRowsIDItemsDict() As Dictionary(Of Int32, HierarchyItem)

        Return EntitiesDGV.rows_id_item_dic

    End Function

    Protected Friend Function getCurrentEntityName() As String

        Return EntitiesDGV.DGV.CellsArea.GetCellValue(EntitiesDGV.currentRowItem, EntitiesDGV.DGV.ColumnsHierarchy.Items(0))

    End Function

    Protected Friend Function getCurrentRowItem() As HierarchyItem

        Return EntitiesDGV.currentRowItem

    End Function

#End Region


#Region "Background Worker 1"

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

       Controller.SendNewPositionsToModel()

    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted

        AfterClosingAttemp_ThreadSafe()

    End Sub

    Delegate Sub AfterClosing_Delegate()

    Private Sub AfterClosingAttemp_ThreadSafe()

        If InvokeRequired Then
            Dim MyDelegate As New AfterClosing_Delegate(AddressOf AfterClosingAttemp_ThreadSafe)
            Me.Invoke(MyDelegate, New Object() {})
        Else
            CP.Dispose()
            Controller.sendCloseOrder()
        End If

    End Sub

#End Region


End Class
