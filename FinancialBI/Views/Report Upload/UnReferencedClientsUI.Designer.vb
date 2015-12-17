<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UnReferencedClientsUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UnReferencedClientsUI))
        Me.m_clientsDGVPanel = New System.Windows.Forms.Panel()
        Me.m_createAllButton = New VIBlend.WinForms.Controls.vButton()
        Me.m_DGVContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.UnselectBothOptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectAllOnColumnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnselectAllOnColumnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.m_DGVContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'm_clientsDGVPanel
        '
        Me.m_clientsDGVPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_clientsDGVPanel.Location = New System.Drawing.Point(-1, 0)
        Me.m_clientsDGVPanel.Name = "m_clientsDGVPanel"
        Me.m_clientsDGVPanel.Size = New System.Drawing.Size(430, 310)
        Me.m_clientsDGVPanel.TabIndex = 0
        '
        'm_createAllButton
        '
        Me.m_createAllButton.AllowAnimations = True
        Me.m_createAllButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_createAllButton.BackColor = System.Drawing.Color.Transparent
        Me.m_createAllButton.Location = New System.Drawing.Point(314, 318)
        Me.m_createAllButton.Name = "m_createAllButton"
        Me.m_createAllButton.RoundedCornersMask = CType(15, Byte)
        Me.m_createAllButton.Size = New System.Drawing.Size(104, 30)
        Me.m_createAllButton.TabIndex = 1
        Me.m_createAllButton.Text = "Validate"
        Me.m_createAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_createAllButton.UseVisualStyleBackColor = False
        Me.m_createAllButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_DGVContextMenuStrip
        '
        Me.m_DGVContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.UnselectBothOptionsToolStripMenuItem, Me.SelectAllOnColumnToolStripMenuItem, Me.UnselectAllOnColumnToolStripMenuItem})
        Me.m_DGVContextMenuStrip.Name = "m_DGVContextMenuStrip"
        Me.m_DGVContextMenuStrip.Size = New System.Drawing.Size(196, 92)
        '
        'UnselectBothOptionsToolStripMenuItem
        '
        Me.UnselectBothOptionsToolStripMenuItem.Name = "UnselectBothOptionsToolStripMenuItem"
        Me.UnselectBothOptionsToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.UnselectBothOptionsToolStripMenuItem.Text = "Unselect both options"
        '
        'SelectAllOnColumnToolStripMenuItem
        '
        Me.SelectAllOnColumnToolStripMenuItem.Name = "SelectAllOnColumnToolStripMenuItem"
        Me.SelectAllOnColumnToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.SelectAllOnColumnToolStripMenuItem.Text = "Select all on column"
        '
        'UnselectAllOnColumnToolStripMenuItem
        '
        Me.UnselectAllOnColumnToolStripMenuItem.Name = "UnselectAllOnColumnToolStripMenuItem"
        Me.UnselectAllOnColumnToolStripMenuItem.Size = New System.Drawing.Size(195, 22)
        Me.UnselectAllOnColumnToolStripMenuItem.Text = "Unselect all on column"
        '
        'UnReferencedClientsUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 358)
        Me.Controls.Add(Me.m_createAllButton)
        Me.Controls.Add(Me.m_clientsDGVPanel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "UnReferencedClientsUI"
        Me.Text = "UnReferenced Clients"
        Me.m_DGVContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_clientsDGVPanel As System.Windows.Forms.Panel
    Friend WithEvents m_createAllButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents m_DGVContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents UnselectBothOptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectAllOnColumnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnselectAllOnColumnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
