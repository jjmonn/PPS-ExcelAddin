<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VersionSelectionPane
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VersionSelectionPane))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.m_versionSelectionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_validateButton = New VIBlend.WinForms.Controls.vButton()
        Me.BTsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.m_versionsTreeviewImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.m_versionSelectionLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_validateButton, 0, 2)
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
        'm_versionSelectionLabel
        '
        Me.m_versionSelectionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_versionSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_versionSelectionLabel.Ellipsis = False
        Me.m_versionSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_versionSelectionLabel.Location = New System.Drawing.Point(3, 4)
        Me.m_versionSelectionLabel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 0)
        Me.m_versionSelectionLabel.Multiline = True
        Me.m_versionSelectionLabel.Name = "m_versionSelectionLabel"
        Me.m_versionSelectionLabel.Size = New System.Drawing.Size(95, 15)
        Me.m_versionSelectionLabel.TabIndex = 2
        Me.m_versionSelectionLabel.Text = "Select a Version"
        Me.m_versionSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_versionSelectionLabel.UseMnemonics = True
        Me.m_versionSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_validateButton
        '
        Me.m_validateButton.AllowAnimations = True
        Me.m_validateButton.BackColor = System.Drawing.Color.Transparent
        Me.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.m_validateButton.ImageKey = "1420498403_340208.ico"
        Me.m_validateButton.ImageList = Me.BTsIL
        Me.m_validateButton.Location = New System.Drawing.Point(3, 618)
        Me.m_validateButton.Name = "m_validateButton"
        Me.m_validateButton.RoundedCornersMask = CType(15, Byte)
        Me.m_validateButton.Size = New System.Drawing.Size(95, 23)
        Me.m_validateButton.TabIndex = 3
        Me.m_validateButton.Text = "Validate"
        Me.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_validateButton.UseVisualStyleBackColor = True
        Me.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'BTsIL
        '
        Me.BTsIL.ImageStream = CType(resources.GetObject("BTsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.BTsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.BTsIL.Images.SetKeyName(0, "1420498403_340208.ico")
        '
        'm_versionsTreeviewImageList
        '
        Me.m_versionsTreeviewImageList.ImageStream = CType(resources.GetObject("m_versionsTreeviewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico")
        Me.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico")
        '
        'VersionSelectionPane
        '
        Me.ClientSize = New System.Drawing.Size(315, 701)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(0, 0)
        Me.Name = "VersionSelectionPane"
        Me.Text = "Versions Selection"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_versionsTreeviewImageList As System.Windows.Forms.ImageList
    Friend WithEvents m_versionSelectionLabel As viblend.winforms.controls.vLabel
    Friend WithEvents BTsIL As System.Windows.Forms.ImageList
    Friend WithEvents m_validateButton As viblend.winforms.controls.vButton

End Class
