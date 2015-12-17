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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(UnReferencedClientsUI))
        Me.m_clientsDGVPanel = New System.Windows.Forms.Panel()
        Me.m_createAllButton = New VIBlend.WinForms.Controls.vButton()
        Me.m_replaceSelectionButton = New VIBlend.WinForms.Controls.vButton()
        Me.m_resumeDataSubmissionButton = New VIBlend.WinForms.Controls.vButton()
        Me.SuspendLayout()
        '
        'm_clientsDGVPanel
        '
        Me.m_clientsDGVPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_clientsDGVPanel.Location = New System.Drawing.Point(-1, 0)
        Me.m_clientsDGVPanel.Name = "m_clientsDGVPanel"
        Me.m_clientsDGVPanel.Size = New System.Drawing.Size(430, 278)
        Me.m_clientsDGVPanel.TabIndex = 0
        '
        'm_createAllButton
        '
        Me.m_createAllButton.AllowAnimations = True
        Me.m_createAllButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_createAllButton.BackColor = System.Drawing.Color.Transparent
        Me.m_createAllButton.Location = New System.Drawing.Point(274, 284)
        Me.m_createAllButton.Name = "m_createAllButton"
        Me.m_createAllButton.RoundedCornersMask = CType(15, Byte)
        Me.m_createAllButton.Size = New System.Drawing.Size(147, 30)
        Me.m_createAllButton.TabIndex = 1
        Me.m_createAllButton.Text = "Create All"
        Me.m_createAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_createAllButton.UseVisualStyleBackColor = False
        Me.m_createAllButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_replaceSelectionButton
        '
        Me.m_replaceSelectionButton.AllowAnimations = True
        Me.m_replaceSelectionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_replaceSelectionButton.BackColor = System.Drawing.Color.Transparent
        Me.m_replaceSelectionButton.Location = New System.Drawing.Point(158, 284)
        Me.m_replaceSelectionButton.Name = "m_replaceSelectionButton"
        Me.m_replaceSelectionButton.RoundedCornersMask = CType(15, Byte)
        Me.m_replaceSelectionButton.Size = New System.Drawing.Size(110, 30)
        Me.m_replaceSelectionButton.TabIndex = 2
        Me.m_replaceSelectionButton.Text = "Replace selection"
        Me.m_replaceSelectionButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_replaceSelectionButton.UseVisualStyleBackColor = False
        Me.m_replaceSelectionButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_resumeDataSubmissionButton
        '
        Me.m_resumeDataSubmissionButton.AllowAnimations = True
        Me.m_resumeDataSubmissionButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.m_resumeDataSubmissionButton.BackColor = System.Drawing.Color.Transparent
        Me.m_resumeDataSubmissionButton.Location = New System.Drawing.Point(274, 320)
        Me.m_resumeDataSubmissionButton.Name = "m_resumeDataSubmissionButton"
        Me.m_resumeDataSubmissionButton.RoundedCornersMask = CType(15, Byte)
        Me.m_resumeDataSubmissionButton.Size = New System.Drawing.Size(147, 30)
        Me.m_resumeDataSubmissionButton.TabIndex = 3
        Me.m_resumeDataSubmissionButton.Text = "Resume data submission"
        Me.m_resumeDataSubmissionButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_resumeDataSubmissionButton.UseVisualStyleBackColor = False
        Me.m_resumeDataSubmissionButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'UnReferencedClientsUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(427, 358)
        Me.Controls.Add(Me.m_resumeDataSubmissionButton)
        Me.Controls.Add(Me.m_replaceSelectionButton)
        Me.Controls.Add(Me.m_createAllButton)
        Me.Controls.Add(Me.m_clientsDGVPanel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "UnReferencedClientsUI"
        Me.Text = "UnReferenced Clients"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_clientsDGVPanel As System.Windows.Forms.Panel
    Friend WithEvents m_createAllButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents m_replaceSelectionButton As VIBlend.WinForms.Controls.vButton
    Friend WithEvents m_resumeDataSubmissionButton As VIBlend.WinForms.Controls.vButton
End Class
