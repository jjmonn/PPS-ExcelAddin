﻿'' AcquisitionUI.vb
''
'' Shows recognized items and corresponding data
'' Allows edition ?
'' Interface mainly managed by the had hoc ribbon
''
''
'' To do:
''       - cells select next cell by moving keys
''       - To be renewed completely !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
''          => priority normal
''          => simplify
''
'' Known bugs:
''       - 
''
''
'' 
'' Author: Julien Monnereau
'' Last modified: 21/08/2015


'Imports System.Collections.Generic
'Imports System.Windows.Forms
'Imports VIBlend.WinForms.DataGridView
'Imports System.Drawing
'Imports VIBlend.Utilities
'Imports Microsoft.Office.Interop


'Friend Class AcquisitionInterface


'#Region "Instance Variables"

'    ' Objects
'    Private Controller As AcquisitionInterfaceController
'    Private DATASET As ModelDataSet
'    Friend DGV As vDataGridView

'    ' Variables
'    Private normalStyleN As HierarchyItemStyle
'    Private normalStyleS As HierarchyItemStyle
'    Private greenStyleN As HierarchyItemStyle
'    Private greenStyleS As HierarchyItemStyle
'    Private redStyleN As HierarchyItemStyle
'    Private redStyleS As HierarchyItemStyle

'    ' Constants
'      Private Const FORMAT_CURRENCIES As String = "{0:C0}"
'    Private Const DGV_VI_BLEND_STYLE As VIBLEND_THEME = VIBLEND_THEME.VISTABLUE

'#End Region


'#Region "Initialize"

'    Friend Sub New(ByRef inputDataSet As ModelDataSet, ByRef inputController As AcquisitionInterfaceController)

'        ' This call is required by the designer.
'        InitializeComponent()

'        ' Add any initialization after the InitializeComponent() call.
'        DATASET = inputDataSet
'        Controller = inputController
'        InitConditionalFormatting()

'    End Sub

'    Private Sub AcquisitionUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'        Me.WindowState = FormWindowState.Maximized
'    End Sub

'    Friend Sub InitDGVDisplay()

'        Panel1.Controls.Add(DGV)
'        DataGridViewsUtil.FormatDGVRowsHierarchy(DGV)
'        DataGridViewsUtil.SetDGVCellsFontSize(DGV, My.Settings.dgvFontSize)
'        DGV.Dock = DockStyle.Fill
'        DGV.ColumnsHierarchy.AutoStretchColumns = True
'        DGV.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ALL)
'        DGV.RowsHierarchy.CompactStyleRenderingEnabled = True
'        DGV.RowsHierarchy.ExpandAllItems()
'        DGV.VIBlendTheme = DGV_VI_BLEND_STYLE
'        DGV.BackColor = SystemColors.Control
'        DGV.ShowBorder = False
'        AddHandler DGV.CellEndEdit, AddressOf DGV_CellEndEdit
'        'AddHandler DGV.CellBeginEdit, AddressOf DGV_CellBeginEdit

'    End Sub

'    Private Sub InitConditionalFormatting()

'        normalStyleN = GridTheme.GetDefaultTheme(DGV_VI_BLEND_STYLE).HierarchyItemStyleNormal
'        normalStyleS = GridTheme.GetDefaultTheme(DGV_VI_BLEND_STYLE).HierarchyItemStyleSelected
'        greenStyleN = GridTheme.GetDefaultTheme(VIBLEND_THEME.METROGREEN).HierarchyItemStyleNormal
'        greenStyleS = GridTheme.GetDefaultTheme(VIBLEND_THEME.METROGREEN).HierarchyItemStyleSelected
'        redStyleN = GridTheme.GetDefaultTheme(VIBLEND_THEME.METROORANGE).HierarchyItemStyleNormal
'        redStyleS = GridTheme.GetDefaultTheme(VIBLEND_THEME.METROORANGE).HierarchyItemStyleSelected

'        normalStyleN.Font = New Font(normalStyleN.Font.FontFamily, My.Settings.dgvFontSize)
'        normalStyleS.Font = New Font(normalStyleS.Font.FontFamily, My.Settings.dgvFontSize)
'        greenStyleN.Font = New Font(greenStyleN.Font.FontFamily, My.Settings.dgvFontSize)
'        greenStyleS.Font = New Font(greenStyleS.Font.FontFamily, My.Settings.dgvFontSize)
'        redStyleN.Font = New Font(redStyleN.Font.FontFamily, My.Settings.dgvFontSize)
'        redStyleS.Font = New Font(redStyleS.Font.FontFamily, My.Settings.dgvFontSize)

'    End Sub

'#End Region


'#Region "Interface"

'    Friend Sub LoadEntitiesToHierarchy(ByRef hierarchy As Hierarchy)

'        ClearHierarchyDictionary(hierarchy)
'        For Each entity As String In DATASET.dataSetDictionary.Keys
'            AddItemToHierarchyAndDictionaries(hierarchy, entity)
'        Next

'    End Sub

'    Friend Sub LoadAccountsToHierarchy(ByRef hierarchy As Hierarchy, _
'                                       ByRef currentEntity As String, _
'                                       ByRef accountsTV As TreeView)

'        ClearHierarchyDictionary(hierarchy)
'        Controller.editorKeyHierarchyDictionary.Clear()

'        For Each node As TreeNode In accountsTV.Nodes
'            Dim item As HierarchyItem = AddItemToHierarchyAndDictionaries(hierarchy, node.Text)
'            item.HierarchyItemStyleNormal = normalStyleN
'            item.HierarchyItemStyleSelected = normalStyleS
'            For Each childNode As TreeNode In node.Nodes
'                LoadChildAccountToHierarchy(childNode, item, currentEntity)
'            Next
'        Next

'    End Sub

'    Friend Sub LoadPeriodsToHierarchy(ByRef hierarchy As Hierarchy, _
'                                       ByRef currentEntity As String, _
'                                       ByRef currentAccount As String, _
'                                       ByRef currentPeriodList() As Int32, _
'                                       ByRef time_config As String)

'        ClearHierarchyDictionary(hierarchy)
'        Dim formatStr As String = ""

'        Select Case time_config
'            Case MONTHLY_TIME_CONFIGURATION : formatStr = "MMM yyyy"
'            Case YEARLY_TIME_CONFIGURATION : formatStr = "yyyy"
'        End Select

'        For Each period As String In DATASET.dataSetDictionary(currentEntity)(currentAccount).Keys
'            Dim item As HierarchyItem = AddItemToHierarchyAndDictionaries(hierarchy, period, Format(DateTime.FromOADate(period), formatStr))
'            Controller.periodsItemIDPeriodCodeDict.Add(item.GetUniqueID, period)
'        Next

'    End Sub


'#End Region


'#Region "DGV Hierarchies Initialization"

'    Private Sub LoadChildAccountToHierarchy(ByRef inputNode As TreeNode, _
'                                            ByRef inputItem As HierarchyItem, _
'                                            ByRef currentEntity As String)

'        Dim subItem As HierarchyItem = AddItemtoItemAndDictionaries(inputItem, inputNode.Text)
'        If DATASET.inputsAccountsList.Contains(inputNode.Text) Then
'            If DATASET.AccountsAddressValuesDictionary.ContainsValue(inputNode.Text) Then
'                subItem.HierarchyItemStyleNormal = greenStyleN
'                subItem.HierarchyItemStyleSelected = greenStyleS
'            Else
'                subItem.HierarchyItemStyleNormal = redStyleN
'                subItem.HierarchyItemStyleSelected = redStyleS
'            End If
'            AssociateTextBoxEditor(subItem)
'        Else
'            subItem.HierarchyItemStyleNormal = normalStyleN
'            subItem.HierarchyItemStyleSelected = normalStyleS
'        End If
'        For Each childNode As TreeNode In inputNode.Nodes
'            LoadChildAccountToHierarchy(childNode, subItem, currentEntity)
'        Next

'    End Sub

'    ' Adds param item to the param hierarchy and corresponding dictionary
'    Private Function AddItemToHierarchyAndDictionaries(ByRef hierarchy As Hierarchy, _
'                                                       ByRef key As String, _
'                                                       Optional ByRef caption As String = "") As HierarchyItem

'        If caption = "" Then caption = key
'        Dim subItem As HierarchyItem = hierarchy.Items.Add(caption)
'        subItem.CellsFormatString = FORMAT_CURRENCIES

'        If TypeOf (hierarchy) Is RowsHierarchy Then
'            Controller.rowsKeyItemDictionary.Add(key, subItem)
'        Else
'            Controller.columnsKeyItemDictionary.Add(key, subItem)
'        End If
'        Return subItem

'    End Function

'    ' Adds param item to the param hierarchy item and corresponding dictionary
'    Private Function AddItemtoItemAndDictionaries(ByRef item As HierarchyItem, ByRef key As String) As HierarchyItem

'        Dim subItem As HierarchyItem = item.Items.Add(key)
'        subItem.CellsFormatString = FORMAT_CURRENCIES
'        If TypeOf (item.Hierarchy) Is RowsHierarchy Then
'            Controller.rowsKeyItemDictionary.Add(key, subItem)
'        Else
'            Controller.columnsKeyItemDictionary.Add(key, subItem)
'        End If
'        Return subItem

'    End Function

'    ' Add a text box editor
'    Private Sub AssociateTextBoxEditor(ByRef item As HierarchyItem)

'        Dim textBoxEditor As New TextBoxEditor()
'        ' textBoxEditor.ActivationFlags = EditorActivationFlags.KEY_PRESS
'        ' And EditorActivationFlags.MOUSE_CLICK
'        item.CellsEditor = textBoxEditor
'        AddHandler textBoxEditor.TextBox.KeyDown, AddressOf celleditor_KeyDown
'        textBoxEditor.Name = item.Caption
'        Controller.editorKeyHierarchyDictionary.Add(textBoxEditor.Name, item)

'    End Sub


'#End Region


'#Region "Cells Color Filling"

'    Friend Sub FillCellInGreen(ByRef dgv As vDataGridView, _
'                               ByRef entity As String, _
'                               ByRef account As String, _
'                               ByRef period As String)

'        Dim cellStyle As GridCellStyle = dgv.Theme.GridCellStyle
'        Dim solidFillStyle As FillStyleSolid = New FillStyleSolid(Color.LightCyan)
'        cellStyle.TextColor = Color.Green
'        cellStyle.FillStyle = solidFillStyle
'        Dim cell As GridCell = GetDGVCell(entity, account, period)
'        dgv.CellsArea.SetCellDrawStyle(cell.RowItem, cell.ColumnItem, cellStyle)

'    End Sub

'    Friend Sub FillCellInRed(ByRef dgv As vDataGridView, _
'                             ByRef entity As String, _
'                             ByRef account As String, _
'                             ByRef period As String)

'        Dim cellStyle As GridCellStyle = dgv.Theme.GridCellStyle
'        Dim solidFillStyle As FillStyleSolid = New FillStyleSolid(Color.Salmon)
'        cellStyle.TextColor = Color.Red
'        cellStyle.FillStyle = solidFillStyle
'        Dim cell As GridCell = GetDGVCell(entity, account, period)
'        cell.DrawStyle = cellStyle
'        ' Dim rowIndex As Int32 = DGV.RowsHierarchy.Items.IndexOf(cell.RowItem)
'        ' Dim columnIndex As Int32 = DGV.ColumnsHierarchy.Items.IndexOf(cell.ColumnItem)
'        ' DGV.CellsArea.SetCellDrawStyle(DGV.RowsHierarchy.Items(1).Items(1), DGV.ColumnsHierarchy.Items(columnIndex), cellStyle)

'    End Sub

'#End Region


'#Region "DGV Events"

'    Private Sub celleditor_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

'        Select Case (e.KeyCode)
'            Case Keys.Enter : sender.CloseEditor(True)
'            Case Keys.Escape : sender.CloseEditor(False)
'                ' find a way to select next cell when clicking down or up
'        End Select

'    End Sub

'    Friend Sub DGV_CellEndEdit(sender As Object, args As VIBlend.WinForms.DataGridView.CellCancelEventArgs)

'        Try
'            Dim test = CDbl(args.Cell.EditValue) + 0.25

'            If TypeOf (args.Cell.Editor) Is TextBoxEditor Then
'                Dim textBox As TextBoxEditor = args.Cell.Editor
'                Dim entity, period As String
'                Dim accountHierarchy = Controller.editorKeyHierarchyDictionary(textBox.Name)
'                '  Dim secondHierarchy As Hierarchy
'                Dim account As String = accountHierarchy.Caption

'                Select Case Controller.currentDGVOrientationFlag
'                    Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR, ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR
'                        If accountHierarchy.IsRowsHierarchyItem Then
'                            period = Controller.periodsItemIDPeriodCodeDict(args.Cell.ColumnItem.GetUniqueID)
'                            '        secondHierarchy = args.Cell.ColumnItem.Hierarchy
'                        Else
'                            period = Controller.periodsItemIDPeriodCodeDict(args.Cell.RowItem.GetUniqueID)
'                            '       secondHierarchy = args.Cell.RowItem.Hierarchy
'                        End If
'                        entity = DGV.Name
'                    Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR, ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR
'                        If accountHierarchy.IsRowsHierarchyItem Then
'                            entity = args.Cell.ColumnItem.Caption
'                            '      secondHierarchy = args.Cell.ColumnItem.Hierarchy
'                        Else
'                            entity = args.Cell.RowItem.Caption
'                            '     secondHierarchy = args.Cell.RowItem.Hierarchy
'                        End If
'                        period = DGV.Name
'                End Select
'            End If

'        Catch ex As Exception
'            MsgBox("Value not valid")
'            args.Cancel = True
'        End Try

'    End Sub

'#End Region


'#Region "Utilities"

'    Private Sub ClearHierarchyDictionary(ByRef hierarchy As Hierarchy)

'        If TypeOf (hierarchy) Is RowsHierarchy Then
'            Controller.rowsKeyItemDictionary.Clear()
'        Else
'            Controller.columnsKeyItemDictionary.Clear()
'        End If

'    End Sub

'    Friend Function GetDGVCell(ByRef entity As String, _
'                               ByRef account As String, _
'                               ByRef period As String) As GridCell

'        Dim rowItem, columnItem As String
'        Select Case Controller.currentDGVOrientationFlag
'            Case ModelDataSet.DATASET_ACCOUNTS_PERIODS_OR
'                rowItem = account
'                columnItem = period

'            Case ModelDataSet.DATASET_PERIODS_ACCOUNTS_OR
'                rowItem = period
'                columnItem = account

'            Case ModelDataSet.DATASET_ACCOUNTS_ENTITIES_OR
'                rowItem = account
'                columnItem = entity

'            Case ModelDataSet.DATASET_ENTITIES_ACCOUNTS_OR
'                rowItem = entity
'                columnItem = account

'            Case ModelDataSet.DATASET_PERIODS_ENTITIES_OR
'                rowItem = period
'                columnItem = entity

'            Case ModelDataSet.DATASET_ENTITIES_PERIODS_OR
'                rowItem = entity
'                columnItem = period

'            Case Else
'                rowItem = Nothing
'                columnItem = Nothing

'        End Select

'        Try
'            DGV.CellsArea.SelectCell(Controller.rowsKeyItemDictionary(rowItem), Controller.columnsKeyItemDictionary(columnItem))
'            Return DGV.CellsArea.SelectedCells(0)
'        Catch ex As Exception
'            ' PPS Error tracking -> row item or column item not in dictionaries
'            Return Nothing
'        End Try

'    End Function

'#End Region


'#Region "Form Events"

'    Private Sub AcquisitionUI_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
'        e.Cancel = True
'        Me.Hide()
'    End Sub

'#End Region



'End Class