<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccountsMGT_UI
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
        Me.AccountsTreeview = New System.Windows.Forms.TreeView()
        Me.B_AddAccount = New System.Windows.Forms.Button()
        Me.B_AddSubAccount = New System.Windows.Forms.Button()
        Me.B_DeleteAccount = New System.Windows.Forms.Button()
        Me.B_AddFormula = New System.Windows.Forms.Button()
        Me.BDropToWS = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'AccountsTreeview
        '
        Me.AccountsTreeview.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.AccountsTreeview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.AccountsTreeview.Location = New System.Drawing.Point(2, 3)
        Me.AccountsTreeview.Name = "AccountsTreeview"
        Me.AccountsTreeview.Size = New System.Drawing.Size(504, 595)
        Me.AccountsTreeview.TabIndex = 0
        '
        'B_AddAccount
        '
        Me.B_AddAccount.Location = New System.Drawing.Point(528, 42)
        Me.B_AddAccount.Name = "B_AddAccount"
        Me.B_AddAccount.Size = New System.Drawing.Size(136, 31)
        Me.B_AddAccount.TabIndex = 1
        Me.B_AddAccount.Text = "Add Account"
        Me.B_AddAccount.UseVisualStyleBackColor = True
        '
        'B_AddSubAccount
        '
        Me.B_AddSubAccount.Location = New System.Drawing.Point(528, 79)
        Me.B_AddSubAccount.Name = "B_AddSubAccount"
        Me.B_AddSubAccount.Size = New System.Drawing.Size(136, 31)
        Me.B_AddSubAccount.TabIndex = 2
        Me.B_AddSubAccount.Text = "Add Sub Account"
        Me.B_AddSubAccount.UseVisualStyleBackColor = True
        '
        'B_DeleteAccount
        '
        Me.B_DeleteAccount.Location = New System.Drawing.Point(528, 116)
        Me.B_DeleteAccount.Name = "B_DeleteAccount"
        Me.B_DeleteAccount.Size = New System.Drawing.Size(136, 31)
        Me.B_DeleteAccount.TabIndex = 3
        Me.B_DeleteAccount.Text = "Delete Account"
        Me.B_DeleteAccount.UseVisualStyleBackColor = True
        '
        'B_AddFormula
        '
        Me.B_AddFormula.Location = New System.Drawing.Point(528, 153)
        Me.B_AddFormula.Name = "B_AddFormula"
        Me.B_AddFormula.Size = New System.Drawing.Size(136, 31)
        Me.B_AddFormula.TabIndex = 4
        Me.B_AddFormula.Text = "Add Formula"
        Me.B_AddFormula.UseVisualStyleBackColor = True
        '
        'BDropToWS
        '
        Me.BDropToWS.Location = New System.Drawing.Point(528, 218)
        Me.BDropToWS.Name = "BDropToWS"
        Me.BDropToWS.Size = New System.Drawing.Size(136, 51)
        Me.BDropToWS.TabIndex = 5
        Me.BDropToWS.Tag = "XC"
        Me.BDropToWS.Text = "Drop Accounts to Worksheet"
        Me.BDropToWS.UseVisualStyleBackColor = True
        '
        'AccountsMGT_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(680, 600)
        Me.Controls.Add(Me.BDropToWS)
        Me.Controls.Add(Me.B_AddFormula)
        Me.Controls.Add(Me.B_DeleteAccount)
        Me.Controls.Add(Me.B_AddSubAccount)
        Me.Controls.Add(Me.B_AddAccount)
        Me.Controls.Add(Me.AccountsTreeview)
        Me.Name = "AccountsMGT_UI"
        Me.Text = "Accounts Model Management"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents AccountsTreeview As System.Windows.Forms.TreeView
    Friend WithEvents B_AddAccount As System.Windows.Forms.Button
    Friend WithEvents B_AddSubAccount As System.Windows.Forms.Button
    Friend WithEvents B_DeleteAccount As System.Windows.Forms.Button
    Friend WithEvents B_AddFormula As System.Windows.Forms.Button
    Friend WithEvents BDropToWS As System.Windows.Forms.Button
End Class
