<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ASConfigurationPanel
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
        Me.ConfTabControl = New System.Windows.Forms.CustomTabControl()
        Me.EntitiesAttTab = New System.Windows.Forms.TabPage()
        Me.MappingTab = New System.Windows.Forms.TabPage()
        Me.UnitConvTab = New System.Windows.Forms.TabPage()
        Me.ConfTabControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'ConfTabControl
        '
        Me.ConfTabControl.Controls.Add(Me.EntitiesAttTab)
        Me.ConfTabControl.Controls.Add(Me.MappingTab)
        Me.ConfTabControl.Controls.Add(Me.UnitConvTab)
        Me.ConfTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.ConfTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.ConfTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.ConfTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.ConfTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray
        Me.ConfTabControl.DisplayStyleProvider.FocusTrack = False
        Me.ConfTabControl.DisplayStyleProvider.HotTrack = True
        Me.ConfTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.ConfTabControl.DisplayStyleProvider.Overlap = 0
        Me.ConfTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.ConfTabControl.DisplayStyleProvider.Radius = 10
        Me.ConfTabControl.DisplayStyleProvider.ShowTabCloser = False
        Me.ConfTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.ConfTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.ConfTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.ConfTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConfTabControl.HotTrack = True
        Me.ConfTabControl.Location = New System.Drawing.Point(0, 0)
        Me.ConfTabControl.Name = "ConfTabControl"
        Me.ConfTabControl.SelectedIndex = 0
        Me.ConfTabControl.Size = New System.Drawing.Size(750, 636)
        Me.ConfTabControl.TabIndex = 8
        '
        'EntitiesAttTab
        '
        Me.EntitiesAttTab.BackColor = System.Drawing.SystemColors.Control
        Me.EntitiesAttTab.Location = New System.Drawing.Point(4, 23)
        Me.EntitiesAttTab.Name = "EntitiesAttTab"
        Me.EntitiesAttTab.Padding = New System.Windows.Forms.Padding(3)
        Me.EntitiesAttTab.Size = New System.Drawing.Size(742, 609)
        Me.EntitiesAttTab.TabIndex = 0
        Me.EntitiesAttTab.Text = "Assets Formulas"
        '
        'MappingTab
        '
        Me.MappingTab.BackColor = System.Drawing.SystemColors.Control
        Me.MappingTab.Location = New System.Drawing.Point(4, 23)
        Me.MappingTab.Name = "MappingTab"
        Me.MappingTab.Padding = New System.Windows.Forms.Padding(3)
        Me.MappingTab.Size = New System.Drawing.Size(742, 609)
        Me.MappingTab.TabIndex = 1
        Me.MappingTab.Text = "Mapping"
        '
        'UnitConvTab
        '
        Me.UnitConvTab.BackColor = System.Drawing.SystemColors.Control
        Me.UnitConvTab.Location = New System.Drawing.Point(4, 23)
        Me.UnitConvTab.Name = "UnitConvTab"
        Me.UnitConvTab.Padding = New System.Windows.Forms.Padding(3)
        Me.UnitConvTab.Size = New System.Drawing.Size(742, 609)
        Me.UnitConvTab.TabIndex = 2
        Me.UnitConvTab.Text = "Unit Conversions"
        '
        'ASConfigurationPanel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ConfTabControl)
        Me.Name = "ASConfigurationPanel"
        Me.Size = New System.Drawing.Size(750, 636)
        Me.ConfTabControl.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ConfTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents EntitiesAttTab As System.Windows.Forms.TabPage
    Friend WithEvents MappingTab As System.Windows.Forms.TabPage
    Friend WithEvents UnitConvTab As System.Windows.Forms.TabPage

End Class
