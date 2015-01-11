<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LogUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LogUI))
        Me.TabControl1 = New System.Windows.Forms.CustomTabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.EntitiesTVPanel = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RefreshBT = New System.Windows.Forms.Button()
        Me.ButtonsImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.DGVTabControl = New System.Windows.Forms.CustomTabControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.EntityTB = New System.Windows.Forms.TextBox()
        Me.CurrencyTB = New System.Windows.Forms.TextBox()
        Me.VersionTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.DGVsRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DisplayDataTrackingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntitiesTVRCM = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DisplayEntityFinancialsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.DGVsRCM.SuspendLayout()
        Me.EntitiesTVRCM.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.TabControl1.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.TabControl1.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.TabControl1.DisplayStyleProvider.FocusTrack = False
        Me.TabControl1.DisplayStyleProvider.HotTrack = True
        Me.TabControl1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.TabControl1.DisplayStyleProvider.Opacity = 1.0!
        Me.TabControl1.DisplayStyleProvider.Overlap = 0
        Me.TabControl1.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.TabControl1.DisplayStyleProvider.Radius = 10
        Me.TabControl1.DisplayStyleProvider.ShowTabCloser = False
        Me.TabControl1.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.TabControl1.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.TabControl1.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.HotTrack = True
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1002, 601)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 23)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(994, 574)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Financial Log"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableLayoutPanel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TableLayoutPanel2)
        Me.SplitContainer1.Size = New System.Drawing.Size(988, 568)
        Me.SplitContainer1.SplitterDistance = 161
        Me.SplitContainer1.TabIndex = 0
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.EntitiesTVPanel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(161, 568)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'EntitiesTVPanel
        '
        Me.EntitiesTVPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntitiesTVPanel.Location = New System.Drawing.Point(3, 28)
        Me.EntitiesTVPanel.Name = "EntitiesTVPanel"
        Me.EntitiesTVPanel.Size = New System.Drawing.Size(155, 537)
        Me.EntitiesTVPanel.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RefreshBT)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(161, 25)
        Me.Panel1.TabIndex = 1
        '
        'RefreshBT
        '
        Me.RefreshBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray
        Me.RefreshBT.FlatAppearance.BorderSize = 0
        Me.RefreshBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RefreshBT.ImageKey = "Refresh2.png"
        Me.RefreshBT.ImageList = Me.ButtonsImageList
        Me.RefreshBT.Location = New System.Drawing.Point(42, 1)
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
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 1
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.DGVTabControl, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.Panel2, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 2
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(823, 568)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'DGVTabControl
        '
        Me.DGVTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.DGVTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.DGVTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.GradientActiveCaption
        Me.DGVTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.SystemColors.ActiveCaption
        Me.DGVTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.Crimson
        Me.DGVTabControl.DisplayStyleProvider.CloserColorActive = System.Drawing.Color.Crimson
        Me.DGVTabControl.DisplayStyleProvider.FocusColor = System.Drawing.Color.Black
        Me.DGVTabControl.DisplayStyleProvider.FocusTrack = False
        Me.DGVTabControl.DisplayStyleProvider.HotTrack = True
        Me.DGVTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DGVTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.DGVTabControl.DisplayStyleProvider.Overlap = 0
        Me.DGVTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.DGVTabControl.DisplayStyleProvider.Radius = 10
        Me.DGVTabControl.DisplayStyleProvider.ShowTabCloser = True
        Me.DGVTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.DGVTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.DGVTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.DGVTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DGVTabControl.HotTrack = True
        Me.DGVTabControl.Location = New System.Drawing.Point(3, 28)
        Me.DGVTabControl.Name = "DGVTabControl"
        Me.DGVTabControl.SelectedIndex = 0
        Me.DGVTabControl.Size = New System.Drawing.Size(817, 537)
        Me.DGVTabControl.TabIndex = 8
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.EntityTB)
        Me.Panel2.Controls.Add(Me.CurrencyTB)
        Me.Panel2.Controls.Add(Me.VersionTB)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(823, 25)
        Me.Panel2.TabIndex = 2
        '
        'EntityTB
        '
        Me.EntityTB.Enabled = False
        Me.EntityTB.Location = New System.Drawing.Point(556, 3)
        Me.EntityTB.Name = "EntityTB"
        Me.EntityTB.Size = New System.Drawing.Size(155, 20)
        Me.EntityTB.TabIndex = 5
        '
        'CurrencyTB
        '
        Me.CurrencyTB.Enabled = False
        Me.CurrencyTB.Location = New System.Drawing.Point(318, 3)
        Me.CurrencyTB.Name = "CurrencyTB"
        Me.CurrencyTB.Size = New System.Drawing.Size(155, 20)
        Me.CurrencyTB.TabIndex = 4
        '
        'VersionTB
        '
        Me.VersionTB.Enabled = False
        Me.VersionTB.Location = New System.Drawing.Point(73, 3)
        Me.VersionTB.Name = "VersionTB"
        Me.VersionTB.Size = New System.Drawing.Size(155, 20)
        Me.VersionTB.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(506, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Entity"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(253, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Currency"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(42, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Version"
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage2.Controls.Add(Me.SplitContainer2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 23)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(994, 574)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Data Log"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label8)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label7)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label6)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label5)
        Me.SplitContainer2.Panel1.Controls.Add(Me.Label4)
        Me.SplitContainer2.Size = New System.Drawing.Size(988, 568)
        Me.SplitContainer2.SplitterDistance = 323
        Me.SplitContainer2.TabIndex = 0
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(147, 278)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(29, 13)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "User"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(47, 278)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "User"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(43, 172)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(47, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Account"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(43, 118)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(33, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Entity"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(43, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(42, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Version"
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "favicon(110).ico")
        '
        'DGVsRCM
        '
        Me.DGVsRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayDataTrackingToolStripMenuItem})
        Me.DGVsRCM.Name = "DGVsRCM"
        Me.DGVsRCM.Size = New System.Drawing.Size(189, 26)
        '
        'DisplayDataTrackingToolStripMenuItem
        '
        Me.DisplayDataTrackingToolStripMenuItem.Name = "DisplayDataTrackingToolStripMenuItem"
        Me.DisplayDataTrackingToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.DisplayDataTrackingToolStripMenuItem.Text = "Display Data Tracking"
        '
        'EntitiesTVRCM
        '
        Me.EntitiesTVRCM.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayEntityFinancialsToolStripMenuItem})
        Me.EntitiesTVRCM.Name = "EntitiesTVRCM"
        Me.EntitiesTVRCM.Size = New System.Drawing.Size(201, 26)
        '
        'DisplayEntityFinancialsToolStripMenuItem
        '
        Me.DisplayEntityFinancialsToolStripMenuItem.Name = "DisplayEntityFinancialsToolStripMenuItem"
        Me.DisplayEntityFinancialsToolStripMenuItem.Size = New System.Drawing.Size(200, 22)
        Me.DisplayEntityFinancialsToolStripMenuItem.Text = "Display Entity Financials"
        '
        'LogUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1002, 601)
        Me.Controls.Add(Me.TabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "LogUI"
        Me.Text = "Data Modification Tracking"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.DGVsRCM.ResumeLayout(False)
        Me.EntitiesTVRCM.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.CustomTabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents EntitiesTVPanel As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents EntityTB As System.Windows.Forms.TextBox
    Friend WithEvents CurrencyTB As System.Windows.Forms.TextBox
    Friend WithEvents VersionTB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DGVTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents DGVsRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DisplayDataTrackingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EntitiesTVRCM As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DisplayEntityFinancialsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshBT As System.Windows.Forms.Button
    Friend WithEvents ButtonsImageList As System.Windows.Forms.ImageList
End Class
