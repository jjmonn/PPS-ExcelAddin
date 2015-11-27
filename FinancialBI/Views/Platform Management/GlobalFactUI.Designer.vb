<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GlobalFactUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GlobalFactUI))
        Me.MenuButtonIL = New System.Windows.Forms.ImageList(Me.components)
        Me.VersionsRCMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.select_version = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.AddRatesVersionRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.AddFolderRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.DeleteVersionRCM = New System.Windows.Forms.ToolStripMenuItem()
        Me.RenameVersionBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.dgvRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateNewFact = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CopyFactDownToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportFromExcelBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.m_circularProgress = New VIBlend.WinForms.Controls.vCircularProgressBar()
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.VersionLabel = New System.Windows.Forms.Label()
        Me.version_TB = New System.Windows.Forms.TextBox()
        Me.FactRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CreateNewFact2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.RenameBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.m_importFromExcelBT2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_versionsTreeviewImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.m_deleteBackgroundWorker = New System.ComponentModel.BackgroundWorker()
        Me.VersionsRCMenu.SuspendLayout()
        Me.dgvRCM.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.FactRightClickMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuButtonIL
        '
        Me.MenuButtonIL.ImageStream = CType(resources.GetObject("MenuButtonIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.MenuButtonIL.TransparentColor = System.Drawing.Color.Transparent
        Me.MenuButtonIL.Images.SetKeyName(0, "expand right.png")
        Me.MenuButtonIL.Images.SetKeyName(1, "expandleft.png")
        Me.MenuButtonIL.Images.SetKeyName(2, "favicon(120).ico")
        Me.MenuButtonIL.Images.SetKeyName(3, "favicon(125).ico")
        Me.MenuButtonIL.Images.SetKeyName(4, "favicon(217).ico")
        Me.MenuButtonIL.Images.SetKeyName(5, "favicon(126).ico")
        '
        'VersionsRCMenu
        '
        Me.VersionsRCMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.select_version, Me.ToolStripSeparator2, Me.AddRatesVersionRCM, Me.AddFolderRCM, Me.ToolStripSeparator6, Me.DeleteVersionRCM, Me.RenameVersionBT})
        Me.VersionsRCMenu.Name = "VersionsRCMenu"
        Me.VersionsRCMenu.Size = New System.Drawing.Size(150, 126)
        '
        'select_version
        '
        Me.select_version.Image = Global.FinancialBI.My.resources.config_circle_green
        Me.select_version.Name = "select_version"
        Me.select_version.Size = New System.Drawing.Size(149, 22)
        Me.select_version.Text = "Select version"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(146, 6)
        '
        'AddRatesVersionRCM
        '
        Me.AddRatesVersionRCM.Image = Global.FinancialBI.My.resources.elements3_add
        Me.AddRatesVersionRCM.Name = "AddRatesVersionRCM"
        Me.AddRatesVersionRCM.Size = New System.Drawing.Size(149, 22)
        Me.AddRatesVersionRCM.Text = "Create version"
        '
        'AddFolderRCM
        '
        Me.AddFolderRCM.Image = Global.FinancialBI.My.resources.folder_open_add
        Me.AddFolderRCM.Name = "AddFolderRCM"
        Me.AddFolderRCM.Size = New System.Drawing.Size(149, 22)
        Me.AddFolderRCM.Text = "Add folder"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(146, 6)
        '
        'DeleteVersionRCM
        '
        Me.DeleteVersionRCM.Image = Global.FinancialBI.My.resources.elements3_delete
        Me.DeleteVersionRCM.Name = "DeleteVersionRCM"
        Me.DeleteVersionRCM.Size = New System.Drawing.Size(149, 22)
        Me.DeleteVersionRCM.Text = "Delete"
        '
        'RenameVersionBT
        '
        Me.RenameVersionBT.Name = "RenameVersionBT"
        Me.RenameVersionBT.Size = New System.Drawing.Size(149, 22)
        Me.RenameVersionBT.Text = "Delete"
        '
        'dgvRCM
        '
        Me.dgvRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateNewFact, Me.ToolStripSeparator3, Me.CopyFactDownToolStripMenuItem, Me.ImportFromExcelBT})
        Me.dgvRCM.Name = "dgvRCM"
        Me.dgvRCM.Size = New System.Drawing.Size(136, 76)
        '
        'CreateNewFact
        '
        Me.CreateNewFact.Image = Global.FinancialBI.My.resources.elements_add
        Me.CreateNewFact.Name = "CreateNewFact"
        Me.CreateNewFact.Size = New System.Drawing.Size(135, 22)
        Me.CreateNewFact.Text = "New"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(132, 6)
        '
        'CopyFactDownToolStripMenuItem
        '
        Me.CopyFactDownToolStripMenuItem.Image = Global.FinancialBI.My.Resources.Download
        Me.CopyFactDownToolStripMenuItem.Name = "CopyFactDownToolStripMenuItem"
        Me.CopyFactDownToolStripMenuItem.Size = New System.Drawing.Size(135, 22)
        Me.CopyFactDownToolStripMenuItem.Text = "Copy down"
        '
        'ImportFromExcelBT
        '
        Me.ImportFromExcelBT.Image = Global.FinancialBI.My.resources.excel_blue2
        Me.ImportFromExcelBT.Name = "ImportFromExcelBT"
        Me.ImportFromExcelBT.Size = New System.Drawing.Size(135, 22)
        Me.ImportFromExcelBT.Text = "Import"
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel4.ColumnCount = 1
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.SplitContainer1, 0, 1)
        Me.TableLayoutPanel4.Controls.Add(Me.TableLayoutPanel5, 0, 0)
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(0, 20)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 2
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32.0!))
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(886, 585)
        Me.TableLayoutPanel4.TabIndex = 5
        '
        'SplitContainer1
        '
        Me.SplitContainer1.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 32)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.SplitContainer1.Panel2.Controls.Add(Me.m_circularProgress)
        Me.SplitContainer1.Size = New System.Drawing.Size(886, 553)
        Me.SplitContainer1.SplitterDistance = 191
        Me.SplitContainer1.SplitterWidth = 3
        Me.SplitContainer1.TabIndex = 7
        '
        'm_circularProgress
        '
        Me.m_circularProgress.AllowAnimations = True
        Me.m_circularProgress.BackColor = System.Drawing.Color.Transparent
        Me.m_circularProgress.IndicatorsCount = 8
        Me.m_circularProgress.Location = New System.Drawing.Point(260, 239)
        Me.m_circularProgress.Maximum = 100
        Me.m_circularProgress.Minimum = 0
        Me.m_circularProgress.Name = "m_circularProgress"
        Me.m_circularProgress.Size = New System.Drawing.Size(85, 85)
        Me.m_circularProgress.TabIndex = 1
        Me.m_circularProgress.Text = "VCircularProgressBar1"
        Me.m_circularProgress.UseThemeBackground = False
        Me.m_circularProgress.Value = 0
        Me.m_circularProgress.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLUE
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 3
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 97.02381!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.976191!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 249.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.Panel1, 2, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0, 0, 4, 0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(882, 32)
        Me.TableLayoutPanel5.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.VersionLabel)
        Me.Panel1.Controls.Add(Me.version_TB)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(632, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(250, 32)
        Me.Panel1.TabIndex = 6
        '
        'VersionLabel
        '
        Me.VersionLabel.AutoSize = True
        Me.VersionLabel.Location = New System.Drawing.Point(8, 7)
        Me.VersionLabel.Name = "VersionLabel"
        Me.VersionLabel.Size = New System.Drawing.Size(42, 13)
        Me.VersionLabel.TabIndex = 3
        Me.VersionLabel.Text = "Version"
        '
        'version_TB
        '
        Me.version_TB.Enabled = False
        Me.version_TB.Location = New System.Drawing.Point(60, 6)
        Me.version_TB.Margin = New System.Windows.Forms.Padding(1)
        Me.version_TB.Name = "version_TB"
        Me.version_TB.Size = New System.Drawing.Size(183, 20)
        Me.version_TB.TabIndex = 2
        '
        'FactRightClickMenu
        '
        Me.FactRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateNewFact2, Me.DeleteBT, Me.ToolStripSeparator1, Me.RenameBT, Me.ToolStripSeparator4, Me.m_importFromExcelBT2})
        Me.FactRightClickMenu.Name = "ContextMenuStrip1"
        Me.FactRightClickMenu.Size = New System.Drawing.Size(169, 104)
        '
        'CreateNewFact2
        '
        Me.CreateNewFact2.Image = Global.FinancialBI.My.resources.elements_add
        Me.CreateNewFact2.Name = "CreateNewFact2"
        Me.CreateNewFact2.Size = New System.Drawing.Size(168, 22)
        Me.CreateNewFact2.Text = "New"
        '
        'DeleteBT
        '
        Me.DeleteBT.Image = Global.FinancialBI.My.resources.elements_delete
        Me.DeleteBT.Name = "DeleteBT"
        Me.DeleteBT.Size = New System.Drawing.Size(168, 22)
        Me.DeleteBT.Text = "Delete"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(165, 6)
        '
        'RenameBT
        '
        Me.RenameBT.Name = "RenameBT"
        Me.RenameBT.Size = New System.Drawing.Size(168, 22)
        Me.RenameBT.Text = "Rename"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(165, 6)
        '
        'm_importFromExcelBT2
        '
        Me.m_importFromExcelBT2.Image = Global.FinancialBI.My.resources.excel_blue2
        Me.m_importFromExcelBT2.Name = "m_importFromExcelBT2"
        Me.m_importFromExcelBT2.Size = New System.Drawing.Size(168, 22)
        Me.m_importFromExcelBT2.Text = "Import from Excel"
        '
        'm_versionsTreeviewImageList
        '
        Me.m_versionsTreeviewImageList.ImageStream = CType(resources.GetObject("m_versionsTreeviewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico")
        Me.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico")
        '
        'GlobalFactUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Controls.Add(Me.TableLayoutPanel4)
        Me.Name = "GlobalFactUI"
        Me.Size = New System.Drawing.Size(886, 605)
        Me.VersionsRCMenu.ResumeLayout(False)
        Me.dgvRCM.ResumeLayout(False)
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.FactRightClickMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents MenuButtonIL As System.Windows.Forms.ImageList
    Friend WithEvents VersionsRCMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents select_version As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AddRatesVersionRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddFolderRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteVersionRCM As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CopyFactDownToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents VersionLabel As System.Windows.Forms.Label
    Friend WithEvents version_TB As System.Windows.Forms.TextBox
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FactRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RenameBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateNewFact As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CreateNewFact2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RenameVersionBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImportFromExcelBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_versionsTreeviewImageList As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents m_importFromExcelBT2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_circularProgress As VIBlend.WinForms.Controls.vCircularProgressBar
    Friend WithEvents m_deleteBackgroundWorker As System.ComponentModel.BackgroundWorker

End Class
