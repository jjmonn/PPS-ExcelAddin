<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CUI2LeftPane
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CUI2LeftPane))
        Me.MainTableLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainer = New System.Windows.Forms.SplitContainer()
        Me.SelectionTVTableLayout = New System.Windows.Forms.TableLayoutPanel()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.SelectionCB = New VIBlend.WinForms.Controls.vComboBox()
        Me.CollapseSelectionBT = New VIBlend.WinForms.Controls.vButton()
        Me.ExpansionImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PanelCollapseBT = New VIBlend.WinForms.Controls.vButton()
        Me.m_entitySelectionLabel = New System.Windows.Forms.Label()
        Me.CategoriesIL = New System.Windows.Forms.ImageList(Me.components)
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.m_versionsTreeviewImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.m_rightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SelectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MainTableLayout.SuspendLayout()
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer.Panel2.SuspendLayout()
        Me.SplitContainer.SuspendLayout()
        Me.SelectionTVTableLayout.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.m_rightClickMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainTableLayout
        '
        Me.MainTableLayout.ColumnCount = 1
        Me.MainTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.MainTableLayout.Controls.Add(Me.SplitContainer, 0, 1)
        Me.MainTableLayout.Controls.Add(Me.Panel1, 0, 0)
        Me.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MainTableLayout.Location = New System.Drawing.Point(0, 0)
        Me.MainTableLayout.Name = "MainTableLayout"
        Me.MainTableLayout.RowCount = 2
        Me.MainTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.MainTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.MainTableLayout.Size = New System.Drawing.Size(270, 671)
        Me.MainTableLayout.TabIndex = 3
        '
        'SplitContainer
        '
        Me.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer.Location = New System.Drawing.Point(3, 28)
        Me.SplitContainer.Name = "SplitContainer"
        Me.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer.Panel2
        '
        Me.SplitContainer.Panel2.Controls.Add(Me.SelectionTVTableLayout)
        Me.SplitContainer.Size = New System.Drawing.Size(264, 640)
        Me.SplitContainer.SplitterDistance = 297
        Me.SplitContainer.TabIndex = 1
        '
        'SelectionTVTableLayout
        '
        Me.SelectionTVTableLayout.ColumnCount = 1
        Me.SelectionTVTableLayout.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.SelectionTVTableLayout.Controls.Add(Me.TableLayoutPanel2, 0, 0)
        Me.SelectionTVTableLayout.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SelectionTVTableLayout.Location = New System.Drawing.Point(0, 0)
        Me.SelectionTVTableLayout.Name = "SelectionTVTableLayout"
        Me.SelectionTVTableLayout.RowCount = 2
        Me.SelectionTVTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.SelectionTVTableLayout.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.SelectionTVTableLayout.Size = New System.Drawing.Size(264, 339)
        Me.SelectionTVTableLayout.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.SelectionCB, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.CollapseSelectionBT, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(264, 25)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'SelectionCB
        '
        Me.SelectionCB.BackColor = System.Drawing.Color.White
        Me.SelectionCB.DisplayMember = ""
        Me.SelectionCB.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SelectionCB.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.SelectionCB.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.SelectionCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.SelectionCB.DropDownWidth = 233
        Me.SelectionCB.Location = New System.Drawing.Point(3, 3)
        Me.SelectionCB.Name = "SelectionCB"
        Me.SelectionCB.RoundedCornersMaskListItem = CType(15, Byte)
        Me.SelectionCB.Size = New System.Drawing.Size(233, 19)
        Me.SelectionCB.TabIndex = 0
        Me.SelectionCB.Text = "[CUI.selection]"
        Me.SelectionCB.UseThemeBackColor = False
        Me.SelectionCB.UseThemeDropDownArrowColor = True
        Me.SelectionCB.ValueMember = ""
        Me.SelectionCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        Me.SelectionCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'CollapseSelectionBT
        '
        Me.CollapseSelectionBT.AllowAnimations = True
        Me.CollapseSelectionBT.BackColor = System.Drawing.Color.Transparent
        Me.CollapseSelectionBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.CollapseSelectionBT.ImageKey = "minus"
        Me.CollapseSelectionBT.ImageList = Me.ExpansionImageList
        Me.CollapseSelectionBT.Location = New System.Drawing.Point(242, 3)
        Me.CollapseSelectionBT.Name = "CollapseSelectionBT"
        Me.CollapseSelectionBT.PaintBorder = False
        Me.CollapseSelectionBT.RoundedCornersMask = CType(15, Byte)
        Me.CollapseSelectionBT.Size = New System.Drawing.Size(19, 19)
        Me.CollapseSelectionBT.TabIndex = 1
        Me.CollapseSelectionBT.Text = " "
        Me.CollapseSelectionBT.UseVisualStyleBackColor = False
        Me.CollapseSelectionBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ExpansionImageList
        '
        Me.ExpansionImageList.ImageStream = CType(resources.GetObject("ExpansionImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ExpansionImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ExpansionImageList.Images.SetKeyName(0, "add.ico")
        Me.ExpansionImageList.Images.SetKeyName(1, "minus")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.PanelCollapseBT)
        Me.Panel1.Controls.Add(Me.m_entitySelectionLabel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(270, 25)
        Me.Panel1.TabIndex = 2
        '
        'PanelCollapseBT
        '
        Me.PanelCollapseBT.AllowAnimations = True
        Me.PanelCollapseBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelCollapseBT.BackColor = System.Drawing.Color.Transparent
        Me.PanelCollapseBT.BorderStyle = VIBlend.WinForms.Controls.ButtonBorderStyle.NONE
        Me.PanelCollapseBT.FlatAppearance.BorderSize = 0
        Me.PanelCollapseBT.ImageKey = "minus"
        Me.PanelCollapseBT.ImageList = Me.ExpansionImageList
        Me.PanelCollapseBT.Location = New System.Drawing.Point(247, 3)
        Me.PanelCollapseBT.Name = "PanelCollapseBT"
        Me.PanelCollapseBT.PaintBorder = False
        Me.PanelCollapseBT.RoundedCornersMask = CType(15, Byte)
        Me.PanelCollapseBT.Size = New System.Drawing.Size(19, 19)
        Me.PanelCollapseBT.TabIndex = 2
        Me.PanelCollapseBT.Text = " "
        Me.PanelCollapseBT.UseVisualStyleBackColor = False
        Me.PanelCollapseBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_entitySelectionLabel
        '
        Me.m_entitySelectionLabel.AutoSize = True
        Me.m_entitySelectionLabel.Location = New System.Drawing.Point(3, 5)
        Me.m_entitySelectionLabel.Name = "m_entitySelectionLabel"
        Me.m_entitySelectionLabel.Size = New System.Drawing.Size(115, 13)
        Me.m_entitySelectionLabel.TabIndex = 0
        Me.m_entitySelectionLabel.Text = "[CUI.entities_selection]"
        Me.m_entitySelectionLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft
        '
        'CategoriesIL
        '
        Me.CategoriesIL.ImageStream = CType(resources.GetObject("CategoriesIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.CategoriesIL.TransparentColor = System.Drawing.Color.Transparent
        Me.CategoriesIL.Images.SetKeyName(0, "elements.ico")
        Me.CategoriesIL.Images.SetKeyName(1, "favicon(81).ico")
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "elements_branch.ico")
        '
        'm_versionsTreeviewImageList
        '
        Me.m_versionsTreeviewImageList.ImageStream = CType(resources.GetObject("m_versionsTreeviewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico")
        Me.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico")
        '
        'm_rightClickMenu
        '
        Me.m_rightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectAllToolStripMenuItem, Me.UnselectAllToolStripMenuItem})
        Me.m_rightClickMenu.Name = "periodsRightClickMenu"
        Me.m_rightClickMenu.Size = New System.Drawing.Size(166, 70)
        '
        'SelectAllToolStripMenuItem
        '
        Me.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem"
        Me.SelectAllToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.SelectAllToolStripMenuItem.Text = "[CUI.select_all]"
        '
        'UnselectAllToolStripMenuItem
        '
        Me.UnselectAllToolStripMenuItem.Name = "UnselectAllToolStripMenuItem"
        Me.UnselectAllToolStripMenuItem.Size = New System.Drawing.Size(165, 22)
        Me.UnselectAllToolStripMenuItem.Text = "[CUI.unselect_all]"
        '
        'CUI2LeftPane
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(207, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(221, Byte), Integer))
        Me.Controls.Add(Me.MainTableLayout)
        Me.Name = "CUI2LeftPane"
        Me.Size = New System.Drawing.Size(270, 671)
        Me.MainTableLayout.ResumeLayout(False)
        Me.SplitContainer.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer.ResumeLayout(False)
        Me.SelectionTVTableLayout.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.m_rightClickMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MainTableLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_entitySelectionLabel As System.Windows.Forms.Label
    Friend WithEvents SplitContainer As System.Windows.Forms.SplitContainer
    Friend WithEvents SelectionTVTableLayout As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SelectionCB As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CollapseSelectionBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents CategoriesIL As System.Windows.Forms.ImageList
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents PanelCollapseBT As VIBlend.WinForms.Controls.vButton
    Public WithEvents ExpansionImageList As System.Windows.Forms.ImageList
    Friend WithEvents m_versionsTreeviewImageList As System.Windows.Forms.ImageList
    Friend WithEvents m_rightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SelectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
