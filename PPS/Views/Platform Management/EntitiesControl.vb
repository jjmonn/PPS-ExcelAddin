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
' Last modified: 03/09/2015
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
    Private entitiesFiltersNameIdDict As Hashtable
    Private tmpSplitterDistance As Double
    Private menuFlag As Boolean
    Private selectionFlag As Boolean


#End Region


#Region "Initialization"

    Protected Friend Sub New(ByRef input_controller As EntitiesController, _
                            ByRef input_entitiesTV As TreeView, _
                            ByRef p_entitiesFilterValuesTV As TreeView, _
                            ByRef input_entitiesFiltersNameIdDict As Hashtable, _
                            ByRef entitiesFiltersTV As TreeView)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Controller = input_controller
        entitiesTV = input_entitiesTV
        entitiesFiltersNameIdDict = input_entitiesFiltersNameIdDict
        EntitiesDGV = New EntitiesDGV(entitiesTV, entitiesFiltersTV, p_entitiesFilterValuesTV, input_entitiesFiltersNameIdDict)

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


#Region "Interface"

    Friend Sub UpdateEntity(ByRef ht As Hashtable)

        EntitiesDGV.isFillingDGV = True
        EntitiesDGV.fillRow(ht(ID_VARIABLE), ht)
        EntitiesDGV.isFillingDGV = False
    End Sub

    Friend Sub DeleteEntity(ByRef id As Int32)

        EntitiesDGV.rows_id_item_dic(id).Delete()

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
                Dim entity_id As String = Controller.GetEntityId(EntitiesDGV.DGV.CellsArea.GetCellValue(EntitiesDGV.currentRowItem, EntitiesDGV.DGV.ColumnsHierarchy.Items(0)))
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

            If (Not args.Cell.Value Is Nothing) Then
                Select Case args.Cell.ColumnItem.ItemValue

                    Case ENTITIES_CURRENCY_VARIABLE
                        value = GlobalVariables.Currencies.GetCurrencyId(args.Cell.Value)
                        If (value = 0) Then
                            MsgBox("Currency " & args.Cell.Value & " not found.")
                            Exit Sub
                        End If

                    Case NAME_VARIABLE
                        value = args.Cell.Value

                    Case Else
                        value = entitiesFiltersNameIdDict(args.Cell.Value)

                End Select

                Controller.UpdateEntity(args.Cell.RowItem.ItemValue, _
                                        args.Cell.ColumnItem.ItemValue, _
                                        value)
            End If
        End If

    End Sub

#End Region


#Region "Utilities"

    Friend Function getDGVRowsIDItemsDict() As Dictionary(Of Int32, HierarchyItem)

        Return EntitiesDGV.rows_id_item_dic

    End Function

    Friend Function getCurrentEntityName() As String

        Return EntitiesDGV.DGV.CellsArea.GetCellValue(EntitiesDGV.currentRowItem, EntitiesDGV.DGV.ColumnsHierarchy.Items(0))

    End Function

    Friend Function getCurrentRowItem() As HierarchyItem

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
