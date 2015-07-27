<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class NewAnalysisAxisUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewAnalysisAxisUI))
        Me.ButtonIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.CancelBT = New System.Windows.Forms.Button()
        Me.ValidateBT = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonIcons
        '
        Me.ButtonIcons.ImageStream = CType(resources.GetObject("ButtonIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico")
        Me.ButtonIcons.Images.SetKeyName(1, "1420498403_340208.ico")
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.SystemColors.Control
        Me.Panel2.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel2.Location = New System.Drawing.Point(12, 12)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(441, 220)
        Me.Panel2.TabIndex = 22
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
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(397, 175)
        Me.TableLayoutPanel1.TabIndex = 18
        '
        'CancelBT
        '
        Me.CancelBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CancelBT.ImageKey = "imageres_89.ico"
        Me.CancelBT.ImageList = Me.ButtonIcons
        Me.CancelBT.Location = New System.Drawing.Point(263, 238)
        Me.CancelBT.Name = "CancelBT"
        Me.CancelBT.Size = New System.Drawing.Size(92, 30)
        Me.CancelBT.TabIndex = 24
        Me.CancelBT.Text = "Cancel"
        Me.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CancelBT.UseVisualStyleBackColor = True
        '
        'ValidateBT
        '
        Me.ValidateBT.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ValidateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ValidateBT.ImageKey = "1420498403_340208.ico"
        Me.ValidateBT.ImageList = Me.ButtonIcons
        Me.ValidateBT.Location = New System.Drawing.Point(361, 238)
        Me.ValidateBT.Name = "ValidateBT"
        Me.ValidateBT.Size = New System.Drawing.Size(92, 30)
        Me.ValidateBT.TabIndex = 23
        Me.ValidateBT.Text = "Create"
        Me.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ValidateBT.UseVisualStyleBackColor = True
        '
        'NewAnalysisAxisUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 280)
        Me.Controls.Add(Me.CancelBT)
        Me.Controls.Add(Me.ValidateBT)
        Me.Controls.Add(Me.Panel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "NewAnalysisAxisUI"
        Me.Text = "NewAnalysisAxisUI"
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CancelBT As System.Windows.Forms.Button
    Friend WithEvents ValidateBT As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ButtonIcons As System.Windows.Forms.ImageList
End Class
