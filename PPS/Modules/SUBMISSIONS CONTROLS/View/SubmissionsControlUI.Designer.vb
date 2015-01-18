<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SubmissionsControlUI
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SubmissionsControlUI))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.EntitiesTVPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RefreshBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.ChartsTableLayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.ControlsDGVPanel = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.EntityTB = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.VersionTB = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.CurrencyTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TVRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DisplayEntityControlsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.ChartsTableLayoutPanel.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.TVRCM.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ChartsTableLayoutPanel)
        Me.SplitContainer1.Size = New System.Drawing.Size(1035, 613)
        Me.SplitContainer1.SplitterDistance = 184
        Me.SplitContainer1.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.EntitiesTVPanel, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(184, 613)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'EntitiesTVPanel
        '
        Me.EntitiesTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntitiesTVPanel.Location = New System.Drawing.Point(1, 26)
        Me.EntitiesTVPanel.Margin = New System.Windows.Forms.Padding(1)
        Me.EntitiesTVPanel.Name = "EntitiesTVPanel"
        Me.EntitiesTVPanel.Size = New System.Drawing.Size(182, 586)
        Me.EntitiesTVPanel.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RefreshBT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(184, 25)
        Me.Panel1.TabIndex = 1
        '
        'RefreshBT
        '
        Me.RefreshBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.RefreshBT.FlatAppearance.BorderSize = 0
        Me.RefreshBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RefreshBT.ImageKey = "Refresh2.png"
        Me.RefreshBT.ImageList = Me.ButtonsImageList
        Me.RefreshBT.Location = New System.Drawing.Point(6, 1)
        Me.RefreshBT.Name = "RefreshBT"
        Me.RefreshBT.Size = New System.Drawing.Size(22, 22)
        Me.RefreshBT.TabIndex = 3
        Me.RefreshBT.UseVisualStyleBackColor = True
        '
        'ButtonsImageList
        '
        Me.ButtonsImageList.ImageStream = CType(resources.GetObject("ButtonsImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsImageList.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonsImageList.Images.SetKeyName(1, "favicon(236).ico")
        Me.ButtonsImageList.Images.SetKeyName(2, "Refresh2.png")
        Me.ButtonsImageList.Images.SetKeyName(3, "Target zoomed.png")
        Me.ButtonsImageList.Images.SetKeyName(4, "Report.png")
        Me.ButtonsImageList.Images.SetKeyName(5, "favicon(187).ico")
        Me.ButtonsImageList.Images.SetKeyName(6, "favicon(196).ico")
        Me.ButtonsImageList.Images.SetKeyName(7, "add blue.jpg")
        Me.ButtonsImageList.Images.SetKeyName(8, "folder 2 ctrl bgd.png")
        '
        'ChartsTableLayoutPanel
        '
        Me.ChartsTableLayoutPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ChartsTableLayoutPanel.ColumnCount = 4
        Me.ChartsTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ChartsTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ChartsTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ChartsTableLayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.ChartsTableLayoutPanel.Controls.Add(Me.ControlsDGVPanel, 0, 1)
        Me.ChartsTableLayoutPanel.Controls.Add(Me.Panel3, 1, 0)
        Me.ChartsTableLayoutPanel.Controls.Add(Me.Panel2, 2, 0)
        Me.ChartsTableLayoutPanel.Controls.Add(Me.Panel4, 3, 0)
        Me.ChartsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChartsTableLayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.ChartsTableLayoutPanel.Name = "ChartsTableLayoutPanel"
        Me.ChartsTableLayoutPanel.RowCount = 4
        Me.ChartsTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.ChartsTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.ChartsTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.ChartsTableLayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.ChartsTableLayoutPanel.Size = New System.Drawing.Size(847, 613)
        Me.ChartsTableLayoutPanel.TabIndex = 0
        '
        'ControlsDGVPanel
        '
        Me.ControlsDGVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ControlsDGVPanel.Location = New System.Drawing.Point(1, 26)
        Me.ControlsDGVPanel.Margin = New System.Windows.Forms.Padding(1)
        Me.ControlsDGVPanel.Name = "ControlsDGVPanel"
        Me.ControlsDGVPanel.Size = New System.Drawing.Size(209, 194)
        Me.ControlsDGVPanel.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.EntityTB)
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Location = New System.Drawing.Point(211, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(211, 25)
        Me.Panel3.TabIndex = 3
        '
        'EntityTB
        '
        Me.EntityTB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EntityTB.Enabled = False
        Me.EntityTB.Location = New System.Drawing.Point(42, 3)
        Me.EntityTB.MaxLength = 100
        Me.EntityTB.Name = "EntityTB"
        Me.EntityTB.Size = New System.Drawing.Size(166, 20)
        Me.EntityTB.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 6)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(33, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Entity"
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.VersionTB)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(422, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(211, 25)
        Me.Panel2.TabIndex = 2
        '
        'VersionTB
        '
        Me.VersionTB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VersionTB.Enabled = False
        Me.VersionTB.Location = New System.Drawing.Point(50, 3)
        Me.VersionTB.MaxLength = 100
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.Size = New System.Drawing.Size(158, 20)
        Me.VersionTB.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "version"
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.CurrencyTB)
        Me.Panel4.Controls.Add(Me.Label3)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(633, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(214, 25)
        Me.Panel4.TabIndex = 4
        '
        'CurrencyTB
        '
        Me.CurrencyTB.Enabled = False
        Me.CurrencyTB.Location = New System.Drawing.Point(58, 3)
        Me.CurrencyTB.MaxLength = 100
        Me.CurrencyTB.Name = "CurrencyTB"
        Me.CurrencyTB.Size = New System.Drawing.Size(62, 20)
        Me.CurrencyTB.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 6)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Currency"
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "red")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "green")
        '
        'TVRCM
        '
        Me.TVRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayEntityControlsToolStripMenuItem})
        Me.TVRCM.Name = "TVRCM"
        Me.TVRCM.Size = New System.Drawing.Size(194, 26)
        '
        'DisplayEntityControlsToolStripMenuItem
        '
        Me.DisplayEntityControlsToolStripMenuItem.Name = "DisplayEntityControlsToolStripMenuItem"
        Me.DisplayEntityControlsToolStripMenuItem.Size = New System.Drawing.Size(193, 22)
        Me.DisplayEntityControlsToolStripMenuItem.Text = "Display Entity Controls"
        '
        'SubmissionsControlUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1035, 613)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "SubmissionsControlUI"
        Me.Text = "Submissions Controls"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ChartsTableLayoutPanel.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.TVRCM.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ChartsTableLayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents EntitiesTVPanel As System.Windows.Forms.Panel
    Friend WithEvents ControlsDGVPanel As System.Windows.Forms.Panel
    Friend WithEvents TVRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DisplayEntityControlsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RefreshBT As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents EntityTB As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents VersionTB As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents CurrencyTB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
