<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewEntityUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewEntityUI))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.CreateEntityBT = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.51376!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.48624!))
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(9, 16)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(585, 420)
        Me.TableLayoutPanel1.TabIndex = 18
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(1, "favicon(95).ico")
        Me.ButtonIcons.Images.SetKeyName(2, "submit 1 ok.ico")
        Me.ButtonIcons.Images.SetKeyName(3, "favicon(97).ico")
        Me.ButtonIcons.Images.SetKeyName(4, "imageres_99.ico")
        '
        'CancelBT
        '
        Me.CancelBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "delete-2-xxl.png"
        Me.CancelBT.ImageList = Me.ButtonsIL
        Me.CancelBT.Location = New System.Drawing.Point(306, 501)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(92, 30)
        Me.CancelBT.TabIndex = 21
        Me.CancelBT.Text = "Cancel"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico")
        Me.ButtonsIL.Images.SetKeyName(1, "delete-2-xxl.png")
        Me.ButtonsIL.Images.SetKeyName(2, "favicon(97).ico")
        '
        'CreateEntityBT
        '
        Me.CreateEntityBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CreateEntityBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateEntityBT.ImageKey = "submit 1 ok.ico"
        Me.CreateEntityBT.ImageList = Me.ButtonsIL
        Me.CreateEntityBT.Location = New System.Drawing.Point(526, 501)
        Me.CreateEntityBT.Name = "CreateEntityBT"
        Me.CreateEntityBT.Size = New System.Drawing.Size(92, 30)
        Me.CreateEntityBT.TabIndex = 20
        Me.CreateEntityBT.Text = "Create"
        Me.CreateEntityBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateEntityBT.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel2.Location = New System.Drawing.Point(24, 34)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(639, 448)
        Me.Panel2.TabIndex = 19
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Location = New System.Drawing.Point(693, 34)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(438, 447)
        Me.Panel1.TabIndex = 22
        '
        'NewEntityUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 552)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.CreateEntityBT)
        Me.Controls.Add(Me.Panel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewEntityUI"
        Me.Text = "New Entity"
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents CreateEntityBT As System.Windows.Forms.Button
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
