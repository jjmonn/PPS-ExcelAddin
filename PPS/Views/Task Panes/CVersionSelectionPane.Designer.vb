<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CVersionSelectionPane
    Inherits AddinExpress.XL.ADXExcelTaskPane

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CVersionSelectionPane))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ValidateBT = New System.Windows.Forms.Button()
        Me.BTsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.VersioningTVIL = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ValidateBT, 0, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.882352!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.11765!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(315, 701)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 4)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Select a Version"
        '
        'ValidateBT
        '
        Me.ValidateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ValidateBT.ImageKey = "1420498403_340208.ico"
        Me.ValidateBT.ImageList = Me.BTsIL
        Me.ValidateBT.Location = New System.Drawing.Point(3, 618)
        Me.ValidateBT.Name = "ValidateBT"
        Me.ValidateBT.Size = New System.Drawing.Size(75, 23)
        Me.ValidateBT.TabIndex = 3
        Me.ValidateBT.Text = "Validate"
        Me.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ValidateBT.UseVisualStyleBackColor = True
        '
        'BTsIL
        '
        Me.BTsIL.ImageStream = CType(resources.GetObject("BTsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.BTsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.BTsIL.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'VersioningTVIL
        '
        Me.VersioningTVIL.ImageStream = CType(resources.GetObject("VersioningTVIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.VersioningTVIL.TransparentColor = System.Drawing.Color.Transparent
        Me.VersioningTVIL.Images.SetKeyName(0, "favicon(81).ico")
        Me.VersioningTVIL.Images.SetKeyName(1, "config circle orangev small.png")
        '
        'CVersionSelectionPane
        '
        Me.ClientSize = New System.Drawing.Size(315, 701)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "CVersionSelectionPane"
        Me.Text = "Versions Selection"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VersioningTVIL As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BTsIL As System.Windows.Forms.ImageList
    Friend WithEvents ValidateBT As System.Windows.Forms.Button

End Class
