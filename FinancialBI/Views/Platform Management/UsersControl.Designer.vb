<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class UsersControl
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
        Me.LayoutPanel = New System.Windows.Forms.TableLayoutPanel()
        Me.EntitiesTV = New VIBlend.WinForms.Controls.vTreeView()
        Me.LayoutPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'LayoutPanel
        '
        Me.LayoutPanel.BackColor = System.Drawing.SystemColors.ControlLight
        Me.LayoutPanel.ColumnCount = 2
        Me.LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.LayoutPanel.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500.0!))
        Me.LayoutPanel.Controls.Add(Me.EntitiesTV, 1, 0)
        Me.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LayoutPanel.Location = New System.Drawing.Point(0, 0)
        Me.LayoutPanel.Name = "LayoutPanel"
        Me.LayoutPanel.RowCount = 1
        Me.LayoutPanel.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.LayoutPanel.Size = New System.Drawing.Size(1156, 477)
        Me.LayoutPanel.TabIndex = 0
        '
        'EntitiesTV
        '
        Me.EntitiesTV.AccessibleName = "TreeView"
        Me.EntitiesTV.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.EntitiesTV.CheckBoxes = True
        Me.EntitiesTV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntitiesTV.Location = New System.Drawing.Point(659, 3)
        Me.EntitiesTV.Name = "EntitiesTV"
        Me.EntitiesTV.ScrollPosition = New System.Drawing.Point(0, 0)
        Me.EntitiesTV.SelectedNode = Nothing
        Me.EntitiesTV.Size = New System.Drawing.Size(494, 471)
        Me.EntitiesTV.TabIndex = 0
        Me.EntitiesTV.Text = "VTreeView1"
        Me.EntitiesTV.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        Me.EntitiesTV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER
        '
        'UsersControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LayoutPanel)
        Me.Name = "UsersControl"
        Me.Size = New System.Drawing.Size(1156, 477)
        Me.LayoutPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LayoutPanel As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents EntitiesTV As VIBlend.WinForms.Controls.vTreeView

End Class
