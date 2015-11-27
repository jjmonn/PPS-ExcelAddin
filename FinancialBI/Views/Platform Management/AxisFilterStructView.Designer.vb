<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AxisFilterStructView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AxisFilterStructView))
        Me.AddBT = New VIBlend.WinForms.Controls.vButton()
        Me.EditButtonsImagelist = New System.Windows.Forms.ImageList(Me.components)
        Me.DeleteBT = New VIBlend.WinForms.Controls.vButton()
        Me.VPanel1 = New VIBlend.WinForms.Controls.vPanel()
        Me.VPanel2 = New VIBlend.WinForms.Controls.vPanel()
        Me.m_structureTreeviewRightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.m_createCategoryUnderCurrentCategoryButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_renameButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.m_deleteButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.VPanel1.Content.SuspendLayout()
        Me.VPanel1.SuspendLayout()
        Me.VPanel2.SuspendLayout()
        Me.m_structureTreeviewRightClickMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'AddBT
        '
        Me.AddBT.AllowAnimations = True
        Me.AddBT.BackColor = System.Drawing.Color.Transparent
        Me.AddBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.AddBT.ImageKey = "1420498403_340208.ico"
        Me.AddBT.ImageList = Me.EditButtonsImagelist
        Me.AddBT.Location = New System.Drawing.Point(10, 7)
        Me.AddBT.Name = "AddBT"
        Me.AddBT.RoundedCornersMask = CType(15, Byte)
        Me.AddBT.Size = New System.Drawing.Size(93, 25)
        Me.AddBT.TabIndex = 0
        Me.AddBT.Text = "New"
        Me.AddBT.UseVisualStyleBackColor = False
        Me.AddBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'EditButtonsImagelist
        '
        Me.EditButtonsImagelist.ImageStream = CType(resources.GetObject("EditButtonsImagelist.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent
        Me.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico")
        Me.EditButtonsImagelist.Images.SetKeyName(1, "imageres_89.ico")
        '
        'DeleteBT
        '
        Me.DeleteBT.AllowAnimations = True
        Me.DeleteBT.BackColor = System.Drawing.Color.Transparent
        Me.DeleteBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteBT.ImageKey = "imageres_89.ico"
        Me.DeleteBT.ImageList = Me.EditButtonsImagelist
        Me.DeleteBT.Location = New System.Drawing.Point(110, 7)
        Me.DeleteBT.Name = "DeleteBT"
        Me.DeleteBT.RoundedCornersMask = CType(15, Byte)
        Me.DeleteBT.Size = New System.Drawing.Size(93, 25)
        Me.DeleteBT.TabIndex = 1
        Me.DeleteBT.Text = "Delete"
        Me.DeleteBT.UseVisualStyleBackColor = False
        Me.DeleteBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'VPanel1
        '
        Me.VPanel1.AllowAnimations = True
        Me.VPanel1.BorderRadius = 0
        '
        'VPanel1.Content
        '
        Me.VPanel1.Content.AutoScroll = True
        Me.VPanel1.Content.BackColor = System.Drawing.SystemColors.Control
        Me.VPanel1.Content.Controls.Add(Me.AddBT)
        Me.VPanel1.Content.Controls.Add(Me.DeleteBT)
        Me.VPanel1.Content.Location = New System.Drawing.Point(1, 1)
        Me.VPanel1.Content.Name = "Content"
        Me.VPanel1.Content.Size = New System.Drawing.Size(311, 37)
        Me.VPanel1.Content.TabIndex = 3
        Me.VPanel1.CustomScrollersIntersectionColor = System.Drawing.Color.Empty
        Me.VPanel1.Location = New System.Drawing.Point(0, 0)
        Me.VPanel1.Name = "VPanel1"
        Me.VPanel1.Opacity = 1.0!
        Me.VPanel1.PanelBorderColor = System.Drawing.Color.Transparent
        Me.VPanel1.Size = New System.Drawing.Size(313, 39)
        Me.VPanel1.TabIndex = 0
        Me.VPanel1.Text = "VPanel1"
        Me.VPanel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'VPanel2
        '
        Me.VPanel2.AllowAnimations = True
        Me.VPanel2.BorderRadius = 0
        '
        'VPanel2.Content
        '
        Me.VPanel2.Content.AutoScroll = True
        Me.VPanel2.Content.BackColor = System.Drawing.SystemColors.Control
        Me.VPanel2.Content.Location = New System.Drawing.Point(1, 1)
        Me.VPanel2.Content.Name = "Content"
        Me.VPanel2.Content.Size = New System.Drawing.Size(311, 422)
        Me.VPanel2.Content.TabIndex = 3
        Me.VPanel2.CustomScrollersIntersectionColor = System.Drawing.Color.Empty
        Me.VPanel2.Location = New System.Drawing.Point(0, 39)
        Me.VPanel2.Name = "VPanel2"
        Me.VPanel2.Opacity = 1.0!
        Me.VPanel2.PanelBorderColor = System.Drawing.Color.Transparent
        Me.VPanel2.Size = New System.Drawing.Size(313, 424)
        Me.VPanel2.TabIndex = 1
        Me.VPanel2.Text = "VPanel2"
        Me.VPanel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'm_structureTreeviewRightClickMenu
        '
        Me.m_structureTreeviewRightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.m_createCategoryUnderCurrentCategoryButton, Me.m_renameButton, Me.ToolStripSeparator1, Me.m_deleteButton})
        Me.m_structureTreeviewRightClickMenu.Name = "ContextMenuStripTV"
        Me.m_structureTreeviewRightClickMenu.Size = New System.Drawing.Size(307, 82)
        '
        'm_createCategoryUnderCurrentCategoryButton
        '
        Me.m_createCategoryUnderCurrentCategoryButton.Image = Global.FinancialBI.My.resources.add
        Me.m_createCategoryUnderCurrentCategoryButton.Name = "m_createCategoryUnderCurrentCategoryButton"
        Me.m_createCategoryUnderCurrentCategoryButton.Size = New System.Drawing.Size(306, 24)
        Me.m_createCategoryUnderCurrentCategoryButton.Text = "Create_category_under_category"
        '
        'm_renameButton
        '
        Me.m_renameButton.Name = "m_renameButton"
        Me.m_renameButton.Size = New System.Drawing.Size(306, 24)
        Me.m_renameButton.Text = "Renamme"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(264, 6)
        '
        'm_deleteButton
        '
        Me.m_deleteButton.Image = Global.FinancialBI.My.Resources.imageres_891
        Me.m_deleteButton.Name = "m_deleteButton"
        Me.m_deleteButton.Size = New System.Drawing.Size(306, 24)
        Me.m_deleteButton.Text = "Delete"
        '
        'AxisFilterStructView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(312, 463)
        Me.Controls.Add(Me.VPanel2)
        Me.Controls.Add(Me.VPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "AxisFilterStructView"
        Me.Text = "Categories"
        Me.VPanel1.Content.ResumeLayout(False)
        Me.VPanel1.ResumeLayout(False)
        Me.VPanel2.ResumeLayout(False)
        Me.m_structureTreeviewRightClickMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AddBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents DeleteBT As VIBlend.WinForms.Controls.vButton
    Friend WithEvents VPanel1 As VIBlend.WinForms.Controls.vPanel
    Friend WithEvents VPanel2 As VIBlend.WinForms.Controls.vPanel
    Friend WithEvents m_structureTreeviewRightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents m_createCategoryUnderCurrentCategoryButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents m_renameButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents m_deleteButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditButtonsImagelist As System.Windows.Forms.ImageList
End Class
