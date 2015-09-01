<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EntitySelectionForUsersMGT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EntitySelectionForUsersMGT))
        Me.entitiesTV = New System.Windows.Forms.TreeView()
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'entitiesTV
        '
        Me.entitiesTV.Dock = System.Windows.Forms.DockStyle.Fill
        Me.entitiesTV.Location = New System.Drawing.Point(0, 0)
        Me.entitiesTV.Name = "entitiesTV"
        Me.entitiesTV.Size = New System.Drawing.Size(328, 333)
        Me.entitiesTV.TabIndex = 0
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "report-icon.gif")
        '
        'EntitySelectionForUsersMGT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(328, 333)
        Me.Controls.Add(Me.entitiesTV)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "EntitySelectionForUsersMGT"
        Me.Text = "Select an Entity"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents entitiesTV As System.Windows.Forms.TreeView
    Friend WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
End Class
