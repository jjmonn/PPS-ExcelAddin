<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManualRangeSel_UI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManualRangeSel_UI))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Validate_Cmd = New System.Windows.Forms.Button()
        Me.Period_GB = New System.Windows.Forms.GroupBox()
        Me.PeriodRefEdit = New AxRefEdit.AxRefEdit()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.PeriodInput = New System.Windows.Forms.TextBox()
        Me.Assets_GB = New System.Windows.Forms.GroupBox()
        Me.EntRefEdit = New AxRefEdit.AxRefEdit()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Entities_Input = New System.Windows.Forms.TextBox()
        Me.Accounts_GB = New System.Windows.Forms.GroupBox()
        Me.AccRefEdit = New AxRefEdit.AxRefEdit()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.AccountsInput = New System.Windows.Forms.TextBox()
        Me.Panel1.SuspendLayout()
        Me.Period_GB.SuspendLayout()
        CType(Me.PeriodRefEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Assets_GB.SuspendLayout()
        CType(Me.EntRefEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Accounts_GB.SuspendLayout()
        CType(Me.AccRefEdit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(536, 18)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Select the ranges corresponding to the Entities, Accounts and Periods"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Panel1.Controls.Add(Me.Validate_Cmd)
        Me.Panel1.Controls.Add(Me.Period_GB)
        Me.Panel1.Controls.Add(Me.Assets_GB)
        Me.Panel1.Controls.Add(Me.Accounts_GB)
        Me.Panel1.Location = New System.Drawing.Point(12, 53)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(552, 480)
        Me.Panel1.TabIndex = 6
        '
        'Validate_Cmd
        '
        Me.Validate_Cmd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Validate_Cmd.Location = New System.Drawing.Point(392, 442)
        Me.Validate_Cmd.Name = "Validate_Cmd"
        Me.Validate_Cmd.Size = New System.Drawing.Size(116, 24)
        Me.Validate_Cmd.TabIndex = 3
        Me.Validate_Cmd.Text = "Validate"
        Me.Validate_Cmd.UseVisualStyleBackColor = True
        '
        'Period_GB
        '
        Me.Period_GB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Period_GB.Controls.Add(Me.PeriodRefEdit)
        Me.Period_GB.Controls.Add(Me.Label7)
        Me.Period_GB.Controls.Add(Me.Label4)
        Me.Period_GB.Controls.Add(Me.PeriodInput)
        Me.Period_GB.Location = New System.Drawing.Point(31, 301)
        Me.Period_GB.Name = "Period_GB"
        Me.Period_GB.Size = New System.Drawing.Size(477, 120)
        Me.Period_GB.TabIndex = 2
        Me.Period_GB.TabStop = False
        Me.Period_GB.Text = "Period Selection"
        '
        'PeriodRefEdit
        '
        Me.PeriodRefEdit.Location = New System.Drawing.Point(231, 48)
        Me.PeriodRefEdit.Name = "PeriodRefEdit"
        Me.PeriodRefEdit.OcxState = CType(resources.GetObject("PeriodRefEdit.OcxState"), System.Windows.Forms.AxHost.State)
        Me.PeriodRefEdit.Size = New System.Drawing.Size(185, 26)
        Me.PeriodRefEdit.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(17, 80)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 17)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "Or enter a value"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(17, 37)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(100, 17)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Select a range"
        '
        'PeriodInput
        '
        Me.PeriodInput.AcceptsReturn = True
        Me.PeriodInput.AcceptsTab = True
        Me.PeriodInput.Location = New System.Drawing.Point(231, 80)
        Me.PeriodInput.Name = "PeriodInput"
        Me.PeriodInput.Size = New System.Drawing.Size(185, 22)
        Me.PeriodInput.TabIndex = 3
        '
        'Assets_GB
        '
        Me.Assets_GB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Assets_GB.Controls.Add(Me.EntRefEdit)
        Me.Assets_GB.Controls.Add(Me.Label5)
        Me.Assets_GB.Controls.Add(Me.Label2)
        Me.Assets_GB.Controls.Add(Me.Entities_Input)
        Me.Assets_GB.Location = New System.Drawing.Point(31, 26)
        Me.Assets_GB.Name = "Assets_GB"
        Me.Assets_GB.Size = New System.Drawing.Size(477, 120)
        Me.Assets_GB.TabIndex = 0
        Me.Assets_GB.TabStop = False
        Me.Assets_GB.Text = "Entities Selection"
        '
        'EntRefEdit
        '
        Me.EntRefEdit.Location = New System.Drawing.Point(231, 37)
        Me.EntRefEdit.Name = "EntRefEdit"
        Me.EntRefEdit.OcxState = CType(resources.GetObject("EntRefEdit.OcxState"), System.Windows.Forms.AxHost.State)
        Me.EntRefEdit.Size = New System.Drawing.Size(185, 26)
        Me.EntRefEdit.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(17, 71)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 17)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Or enter a value"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 32)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Select a range"
        '
        'Entities_Input
        '
        Me.Entities_Input.AcceptsReturn = True
        Me.Entities_Input.AcceptsTab = True
        Me.Entities_Input.Location = New System.Drawing.Point(231, 71)
        Me.Entities_Input.Name = "Entities_Input"
        Me.Entities_Input.Size = New System.Drawing.Size(185, 22)
        Me.Entities_Input.TabIndex = 0
        '
        'Accounts_GB
        '
        Me.Accounts_GB.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Accounts_GB.Controls.Add(Me.AccRefEdit)
        Me.Accounts_GB.Controls.Add(Me.Label6)
        Me.Accounts_GB.Controls.Add(Me.Label3)
        Me.Accounts_GB.Controls.Add(Me.AccountsInput)
        Me.Accounts_GB.Location = New System.Drawing.Point(31, 163)
        Me.Accounts_GB.Name = "Accounts_GB"
        Me.Accounts_GB.Size = New System.Drawing.Size(477, 120)
        Me.Accounts_GB.TabIndex = 0
        Me.Accounts_GB.TabStop = False
        Me.Accounts_GB.Text = "Accounts Selection"
        '
        'AccRefEdit
        '
        Me.AccRefEdit.Location = New System.Drawing.Point(231, 48)
        Me.AccRefEdit.Name = "AccRefEdit"
        Me.AccRefEdit.OcxState = CType(resources.GetObject("AccRefEdit.OcxState"), System.Windows.Forms.AxHost.State)
        Me.AccRefEdit.Size = New System.Drawing.Size(185, 26)
        Me.AccRefEdit.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(17, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(111, 17)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Or enter a value"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(17, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Select a range"
        '
        'AccountsInput
        '
        Me.AccountsInput.AcceptsReturn = True
        Me.AccountsInput.AcceptsTab = True
        Me.AccountsInput.Location = New System.Drawing.Point(231, 80)
        Me.AccountsInput.Name = "AccountsInput"
        Me.AccountsInput.Size = New System.Drawing.Size(185, 22)
        Me.AccountsInput.TabIndex = 3
        '
        'ManualRangeSel_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(574, 545)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ManualRangeSel_UI"
        Me.Text = "SnapshotManualSelectionUI"
        Me.Panel1.ResumeLayout(False)
        Me.Period_GB.ResumeLayout(False)
        Me.Period_GB.PerformLayout()
        CType(Me.PeriodRefEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Assets_GB.ResumeLayout(False)
        Me.Assets_GB.PerformLayout()
        CType(Me.EntRefEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Accounts_GB.ResumeLayout(False)
        Me.Accounts_GB.PerformLayout()
        CType(Me.AccRefEdit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Validate_Cmd As System.Windows.Forms.Button
    Friend WithEvents Period_GB As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents PeriodInput As System.Windows.Forms.TextBox
    Friend WithEvents Assets_GB As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Entities_Input As System.Windows.Forms.TextBox
    Friend WithEvents Accounts_GB As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AccountsInput As System.Windows.Forms.TextBox
    Friend WithEvents PeriodRefEdit As AxRefEdit.AxRefEdit
    Friend WithEvents EntRefEdit As AxRefEdit.AxRefEdit
    Friend WithEvents AccRefEdit As AxRefEdit.AxRefEdit
End Class
