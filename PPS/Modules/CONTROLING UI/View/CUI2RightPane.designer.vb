﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CUI2RightPane
    Inherits System.Windows.Forms.UserControl

    'UserControl1 overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CUI2RightPane))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.columnsDisplayList = New VIBlend.WinForms.Controls.vListBox()
        Me.rowsDisplayList = New VIBlend.WinForms.Controls.vListBox()
        Me.UpdateBT = New System.Windows.Forms.Button()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DimensionsTVPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.CollapseRightPaneBT = New VIBlend.WinForms.Controls.vButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DimensionsTVPanel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.63511!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.36489!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(277, 616)
        Me.TableLayoutPanel1.TabIndex = 1
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.Label1, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.columnsDisplayList, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.rowsDisplayList, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.UpdateBT, 1, 2)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 338)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.44884!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.55116!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(271, 275)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label1.ImageKey = "table_selection_column.ico"
        Me.Label1.ImageList = Me.ImageList2
        Me.Label1.Location = New System.Drawing.Point(138, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Columns Labels"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ImageList2
        '
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList2.Images.SetKeyName(0, "Close_Box_Red.png")
        Me.ImageList2.Images.SetKeyName(1, "table_selection_column.ico")
        Me.ImageList2.Images.SetKeyName(2, "table_selection_row.ico")
        '
        'columnsDisplayList
        '
        Me.columnsDisplayList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.columnsDisplayList.Location = New System.Drawing.Point(138, 23)
        Me.columnsDisplayList.Name = "columnsDisplayList"
        Me.columnsDisplayList.RoundedCornersMaskListItem = CType(15, Byte)
        Me.columnsDisplayList.Size = New System.Drawing.Size(130, 219)
        Me.columnsDisplayList.TabIndex = 5
        Me.columnsDisplayList.Text = "VListBox1"
        Me.columnsDisplayList.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        Me.columnsDisplayList.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'rowsDisplayList
        '
        Me.rowsDisplayList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rowsDisplayList.Location = New System.Drawing.Point(3, 23)
        Me.rowsDisplayList.Name = "rowsDisplayList"
        Me.rowsDisplayList.RoundedCornersMaskListItem = CType(15, Byte)
        Me.rowsDisplayList.Size = New System.Drawing.Size(129, 219)
        Me.rowsDisplayList.TabIndex = 6
        Me.rowsDisplayList.Text = "VListBox1"
        Me.rowsDisplayList.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        Me.rowsDisplayList.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'UpdateBT
        '
        Me.UpdateBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UpdateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.UpdateBT.ImageKey = "Refresh DB 24.ico"
        Me.UpdateBT.ImageList = Me.ImageList1
        Me.UpdateBT.Location = New System.Drawing.Point(193, 248)
        Me.UpdateBT.Name = "UpdateBT"
        Me.UpdateBT.Size = New System.Drawing.Size(75, 24)
        Me.UpdateBT.TabIndex = 0
        Me.UpdateBT.Text = "Update"
        Me.UpdateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.UpdateBT.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Close_Box_Red.png")
        Me.ImageList1.Images.SetKeyName(1, "Refresh DB 24.ico")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label2.ImageKey = "table_selection_row.ico"
        Me.Label2.ImageList = Me.ImageList2
        Me.Label2.Location = New System.Drawing.Point(3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(129, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Rows Labels"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DimensionsTVPanel
        '
        Me.DimensionsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DimensionsTVPanel.Location = New System.Drawing.Point(3, 38)
        Me.DimensionsTVPanel.Name = "DimensionsTVPanel"
        Me.DimensionsTVPanel.Size = New System.Drawing.Size(271, 294)
        Me.DimensionsTVPanel.TabIndex = 7
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.CollapseRightPaneBT)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(271, 29)
        Me.Panel1.TabIndex = 8
        '
        'CollapseRightPaneBT
        '
        Me.CollapseRightPaneBT.AllowAnimations = True
        Me.CollapseRightPaneBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CollapseRightPaneBT.BackColor = System.Drawing.Color.Transparent
        Me.CollapseRightPaneBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CollapseRightPaneBT.Location = New System.Drawing.Point(249, 3)
        Me.CollapseRightPaneBT.Name = "CollapseRightPaneBT"
        Me.CollapseRightPaneBT.PaintBorder = False
        Me.CollapseRightPaneBT.RoundedCornersMask = CType(15, Byte)
        Me.CollapseRightPaneBT.Size = New System.Drawing.Size(19, 19)
        Me.CollapseRightPaneBT.TabIndex = 7
        Me.CollapseRightPaneBT.UseVisualStyleBackColor = False
        Me.CollapseRightPaneBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(7, 11)
        Me.Label3.Margin = New System.Windows.Forms.Padding(10, 3, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(169, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Choose fields to add to report:"
        '
        'CUI2RightPane
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CUI2RightPane"
        Me.Size = New System.Drawing.Size(277, 616)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DimensionsTVPanel As System.Windows.Forms.Panel
    Friend WithEvents UpdateBT As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Public WithEvents columnsDisplayList As VIBlend.WinForms.Controls.vListBox
    Public WithEvents rowsDisplayList As VIBlend.WinForms.Controls.vListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents CollapseRightPaneBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class