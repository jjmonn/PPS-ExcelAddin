<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DownloadUI
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
        Me.Assets_GB = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.AssetsSel = New System.Windows.Forms.ListView()
        Me.Asset_TV = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Period_GB = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PeriodsSel = New System.Windows.Forms.ListView()
        Me.PeriodTV = New System.Windows.Forms.TreeView()
        Me.Accounts_GB = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AccountsSel = New System.Windows.Forms.ListView()
        Me.AccountsTV = New System.Windows.Forms.TreeView()
        Me.Download_cmd = New System.Windows.Forms.Button()
        Me.Assets_GB.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Period_GB.SuspendLayout()
        Me.Accounts_GB.SuspendLayout()
        Me.SuspendLayout()
        '
        'Assets_GB
        '
        Me.Assets_GB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Assets_GB.Controls.Add(Me.Label2)
        Me.Assets_GB.Controls.Add(Me.AssetsSel)
        Me.Assets_GB.Controls.Add(Me.Asset_TV)
        Me.Assets_GB.Location = New System.Drawing.Point(45, 96)
        Me.Assets_GB.Name = "Assets_GB"
        Me.Assets_GB.Size = New System.Drawing.Size(331, 141)
        Me.Assets_GB.TabIndex = 0
        Me.Assets_GB.TabStop = False
        Me.Assets_GB.Text = "Entities Selection"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(34, 36)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(476, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Drag and drop the selected entities from the hierarchy to the list"
        '
        'AssetsSel
        '
        Me.AssetsSel.Location = New System.Drawing.Point(420, 66)
        Me.AssetsSel.Name = "AssetsSel"
        Me.AssetsSel.Size = New System.Drawing.Size(288, 317)
        Me.AssetsSel.TabIndex = 1
        Me.AssetsSel.UseCompatibleStateImageBehavior = False
        '
        'Asset_TV
        '
        Me.Asset_TV.Location = New System.Drawing.Point(29, 66)
        Me.Asset_TV.Name = "Asset_TV"
        Me.Asset_TV.Size = New System.Drawing.Size(358, 317)
        Me.Asset_TV.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(145, 18)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Set up the request"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel1.Controls.Add(Me.Period_GB)
        Me.Panel1.Controls.Add(Me.Assets_GB)
        Me.Panel1.Controls.Add(Me.Accounts_GB)
        Me.Panel1.Location = New System.Drawing.Point(12, 52)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(803, 481)
        Me.Panel1.TabIndex = 2
        '
        'Period_GB
        '
        Me.Period_GB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Period_GB.Controls.Add(Me.Label4)
        Me.Period_GB.Controls.Add(Me.PeriodsSel)
        Me.Period_GB.Controls.Add(Me.PeriodTV)
        Me.Period_GB.Location = New System.Drawing.Point(53, 243)
        Me.Period_GB.Name = "Period_GB"
        Me.Period_GB.Size = New System.Drawing.Size(307, 200)
        Me.Period_GB.TabIndex = 2
        Me.Period_GB.TabStop = False
        Me.Period_GB.Text = "Period Selection"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(34, 36)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(477, 17)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Drag and drop the selected periods from the hierarchy to the list"
        '
        'PeriodsSel
        '
        Me.PeriodsSel.Location = New System.Drawing.Point(157, 46)
        Me.PeriodsSel.Name = "PeriodsSel"
        Me.PeriodsSel.Size = New System.Drawing.Size(106, 122)
        Me.PeriodsSel.TabIndex = 1
        Me.PeriodsSel.UseCompatibleStateImageBehavior = False
        '
        'PeriodTV
        '
        Me.PeriodTV.Location = New System.Drawing.Point(20, 46)
        Me.PeriodTV.Name = "PeriodTV"
        Me.PeriodTV.Size = New System.Drawing.Size(121, 122)
        Me.PeriodTV.TabIndex = 0
        '
        'Accounts_GB
        '
        Me.Accounts_GB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Accounts_GB.Controls.Add(Me.Label3)
        Me.Accounts_GB.Controls.Add(Me.AccountsSel)
        Me.Accounts_GB.Controls.Add(Me.AccountsTV)
        Me.Accounts_GB.Location = New System.Drawing.Point(382, 225)
        Me.Accounts_GB.Name = "Accounts_GB"
        Me.Accounts_GB.Size = New System.Drawing.Size(360, 218)
        Me.Accounts_GB.TabIndex = 0
        Me.Accounts_GB.TabStop = False
        Me.Accounts_GB.Text = "Accounts Selection"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(34, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(488, 17)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Drag and drop the selected accounts from the hierarchy to the list"
        '
        'AccountsSel
        '
        Me.AccountsSel.Location = New System.Drawing.Point(191, 46)
        Me.AccountsSel.Name = "AccountsSel"
        Me.AccountsSel.Size = New System.Drawing.Size(144, 154)
        Me.AccountsSel.TabIndex = 1
        Me.AccountsSel.UseCompatibleStateImageBehavior = False
        '
        'AccountsTV
        '
        Me.AccountsTV.Location = New System.Drawing.Point(20, 46)
        Me.AccountsTV.Name = "AccountsTV"
        Me.AccountsTV.Size = New System.Drawing.Size(146, 135)
        Me.AccountsTV.TabIndex = 0
        '
        'Download_cmd
        '
        Me.Download_cmd.Location = New System.Drawing.Point(687, 21)
        Me.Download_cmd.Name = "Download_cmd"
        Me.Download_cmd.Size = New System.Drawing.Size(127, 25)
        Me.Download_cmd.TabIndex = 3
        Me.Download_cmd.Text = "Download Data"
        Me.Download_cmd.UseVisualStyleBackColor = True
        '
        'DownloadUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(827, 545)
        Me.Controls.Add(Me.Download_cmd)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "DownloadUI"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Data Access"
        Me.Assets_GB.ResumeLayout(False)
        Me.Assets_GB.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Period_GB.ResumeLayout(False)
        Me.Period_GB.PerformLayout()
        Me.Accounts_GB.ResumeLayout(False)
        Me.Accounts_GB.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Assets_GB As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AssetsSel As System.Windows.Forms.ListView
    Friend WithEvents Asset_TV As System.Windows.Forms.TreeView
    Friend WithEvents Accounts_GB As System.Windows.Forms.GroupBox
    Friend WithEvents AccountsSel As System.Windows.Forms.ListView
    Friend WithEvents AccountsTV As System.Windows.Forms.TreeView
    Friend WithEvents Period_GB As System.Windows.Forms.GroupBox
    Friend WithEvents PeriodsSel As System.Windows.Forms.ListView
    Friend WithEvents PeriodTV As System.Windows.Forms.TreeView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Download_cmd As System.Windows.Forms.Button
End Class
