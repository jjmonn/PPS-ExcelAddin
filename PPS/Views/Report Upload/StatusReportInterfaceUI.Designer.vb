<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StatusReportInterfaceUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StatusReportInterfaceUI))
        Me.m_errorsListBox = New VIBlend.WinForms.Controls.vListBox()
        Me.SuspendLayout()
        '
        'm_errorsListBox
        '
        Me.m_errorsListBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_errorsListBox.Location = New System.Drawing.Point(0, 0)
        Me.m_errorsListBox.Name = "m_errorsListBox"
        Me.m_errorsListBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.m_errorsListBox.Size = New System.Drawing.Size(423, 187)
        Me.m_errorsListBox.TabIndex = 0
        Me.m_errorsListBox.Text = "VListBox1"
        Me.m_errorsListBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.m_errorsListBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'StatusReportInterfaceUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 187)
        Me.Controls.Add(Me.m_errorsListBox)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "StatusReportInterfaceUI"
        Me.Text = "Upload Error Messages"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents m_errorsListBox As VIBlend.WinForms.Controls.vListBox
End Class
