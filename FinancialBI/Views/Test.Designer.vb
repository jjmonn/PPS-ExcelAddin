<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Test
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
        Me.ComputeBT = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.VTreeView1 = New VIBlend.WinForms.Controls.vTreeView()
        Me.VTreeNode1 = New VIBlend.WinForms.Controls.vTreeNode()
        Me.VTreeNode2 = New VIBlend.WinForms.Controls.vTreeNode()
        Me.SuspendLayout()
        '
        'ComputeBT
        '
        Me.ComputeBT.Location = New System.Drawing.Point(24, 32)
        Me.ComputeBT.Name = "ComputeBT"
        Me.ComputeBT.Size = New System.Drawing.Size(134, 25)
        Me.ComputeBT.TabIndex = 0
        Me.ComputeBT.Text = "Compute Test"
        Me.ComputeBT.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(29, 84)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(191, 26)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "TEST_SERVER CMSG"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'VTreeView1
        '
        Me.VTreeView1.AccessibleName = "TreeView"
        Me.VTreeView1.AccessibleRole = System.Windows.Forms.AccessibleRole.List
        Me.VTreeView1.Location = New System.Drawing.Point(277, 61)
        Me.VTreeView1.Name = "VTreeView1"
        Me.VTreeView1.Nodes.Add(Me.VTreeNode1)
        Me.VTreeView1.Nodes.Add(Me.VTreeNode2)
        Me.VTreeView1.ScrollPosition = New System.Drawing.Point(0, 0)
        Me.VTreeView1.SelectedNode = Nothing
        Me.VTreeView1.Size = New System.Drawing.Size(192, 245)
        Me.VTreeView1.TabIndex = 2
        Me.VTreeView1.Text = "VTreeView1"
        Me.VTreeView1.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
        Me.VTreeView1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK
        '
        'VTreeNode1
        '
        Me.VTreeNode1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VTreeNode1.HighlightForeColor = System.Drawing.SystemColors.ControlText
        Me.VTreeNode1.RoundedCornersMask = CType(15, Byte)
        Me.VTreeNode1.SelectedForeColor = System.Drawing.SystemColors.ControlText
        Me.VTreeNode1.Text = "Node"
        Me.VTreeNode1.TooltipText = "Node"
        '
        'VTreeNode2
        '
        Me.VTreeNode2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VTreeNode2.HighlightForeColor = System.Drawing.SystemColors.ControlText
        Me.VTreeNode2.RoundedCornersMask = CType(15, Byte)
        Me.VTreeNode2.SelectedForeColor = System.Drawing.SystemColors.ControlText
        Me.VTreeNode2.Text = "Node"
        Me.VTreeNode2.TooltipText = "Node"
        '
        'Test
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(516, 357)
        Me.Controls.Add(Me.VTreeView1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ComputeBT)
        Me.Name = "Test"
        Me.Text = "Test"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComputeBT As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents VTreeView1 As VIBlend.WinForms.Controls.vTreeView
    Friend WithEvents VTreeNode1 As VIBlend.WinForms.Controls.vTreeNode
    Friend WithEvents VTreeNode2 As VIBlend.WinForms.Controls.vTreeNode
End Class
