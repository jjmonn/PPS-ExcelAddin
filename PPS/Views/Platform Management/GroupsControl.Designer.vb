<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GroupsControl
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
        Me.GroupsPanel = New System.Windows.Forms.Panel()
        Me.EntitiesPanel = New System.Windows.Forms.Panel()
        Me.UsersPanel = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'GroupsPanel
        '
        Me.GroupsPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupsPanel.Location = New System.Drawing.Point(0, 0)
        Me.GroupsPanel.Name = "GroupsPanel"
        Me.GroupsPanel.Size = New System.Drawing.Size(248, 429)
        Me.GroupsPanel.TabIndex = 0
        '
        'EntitiesPanel
        '
        Me.EntitiesPanel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.EntitiesPanel.Location = New System.Drawing.Point(247, 0)
        Me.EntitiesPanel.Name = "EntitiesPanel"
        Me.EntitiesPanel.Size = New System.Drawing.Size(303, 429)
        Me.EntitiesPanel.TabIndex = 1
        '
        'UsersPanel
        '
        Me.UsersPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UsersPanel.Location = New System.Drawing.Point(548, 0)
        Me.UsersPanel.Name = "UsersPanel"
        Me.UsersPanel.Size = New System.Drawing.Size(320, 429)
        Me.UsersPanel.TabIndex = 2
        '
        'GroupsControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.UsersPanel)
        Me.Controls.Add(Me.EntitiesPanel)
        Me.Controls.Add(Me.GroupsPanel)
        Me.Name = "GroupsControl"
        Me.Size = New System.Drawing.Size(868, 429)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupsPanel As System.Windows.Forms.Panel
    Friend WithEvents EntitiesPanel As System.Windows.Forms.Panel
    Friend WithEvents UsersPanel As System.Windows.Forms.Panel

End Class
