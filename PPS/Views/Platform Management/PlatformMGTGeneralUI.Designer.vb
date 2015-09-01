<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PlatformMGTGeneralUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlatformMGTGeneralUI))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FinancialsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FinancialsAndOperationalItemsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdjustmentsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrganizationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrganizationToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.OrganizationCategoriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClientsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClientsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClientsCategoriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductsCategoriesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CurrenciesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ControlsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FinancialsToolStripMenuItem, Me.OrganizationToolStripMenuItem, Me.ClientsToolStripMenuItem, Me.ProductsToolStripMenuItem, Me.VersionsToolStripMenuItem, Me.CurrenciesToolStripMenuItem, Me.UsersToolStripMenuItem, Me.ControlsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(674, 55)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(674, 387)
        Me.Panel1.TabIndex = 1
        '
        'FinancialsToolStripMenuItem
        '
        Me.FinancialsToolStripMenuItem.CheckOnClick = True
        Me.FinancialsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FinancialsAndOperationalItemsToolStripMenuItem, Me.AdjustmentsToolStripMenuItem})
        Me.FinancialsToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.registry1
        Me.FinancialsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.FinancialsToolStripMenuItem.Name = "FinancialsToolStripMenuItem"
        Me.FinancialsToolStripMenuItem.Size = New System.Drawing.Size(71, 51)
        Me.FinancialsToolStripMenuItem.Text = "Financials"
        Me.FinancialsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'FinancialsAndOperationalItemsToolStripMenuItem
        '
        Me.FinancialsAndOperationalItemsToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.registry1
        Me.FinancialsAndOperationalItemsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.FinancialsAndOperationalItemsToolStripMenuItem.Name = "FinancialsAndOperationalItemsToolStripMenuItem"
        Me.FinancialsAndOperationalItemsToolStripMenuItem.Size = New System.Drawing.Size(262, 38)
        Me.FinancialsAndOperationalItemsToolStripMenuItem.Text = "Financials and Operational Items"
        '
        'AdjustmentsToolStripMenuItem
        '
        Me.AdjustmentsToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.elements1
        Me.AdjustmentsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.AdjustmentsToolStripMenuItem.Name = "AdjustmentsToolStripMenuItem"
        Me.AdjustmentsToolStripMenuItem.Size = New System.Drawing.Size(262, 38)
        Me.AdjustmentsToolStripMenuItem.Text = "Adjustments"
        '
        'OrganizationToolStripMenuItem
        '
        Me.OrganizationToolStripMenuItem.CheckOnClick = True
        Me.OrganizationToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OrganizationToolStripMenuItem1, Me.OrganizationCategoriesToolStripMenuItem})
        Me.OrganizationToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.element_branch21
        Me.OrganizationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.OrganizationToolStripMenuItem.Name = "OrganizationToolStripMenuItem"
        Me.OrganizationToolStripMenuItem.Size = New System.Drawing.Size(87, 51)
        Me.OrganizationToolStripMenuItem.Text = "Organization"
        Me.OrganizationToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'OrganizationToolStripMenuItem1
        '
        Me.OrganizationToolStripMenuItem1.Image = Global.PPS.My.Resources.Resources.element_branch21
        Me.OrganizationToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.OrganizationToolStripMenuItem1.Name = "OrganizationToolStripMenuItem1"
        Me.OrganizationToolStripMenuItem1.Size = New System.Drawing.Size(217, 38)
        Me.OrganizationToolStripMenuItem1.Text = "Organization"
        '
        'OrganizationCategoriesToolStripMenuItem
        '
        Me.OrganizationCategoriesToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.elements1
        Me.OrganizationCategoriesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.OrganizationCategoriesToolStripMenuItem.Name = "OrganizationCategoriesToolStripMenuItem"
        Me.OrganizationCategoriesToolStripMenuItem.Size = New System.Drawing.Size(217, 38)
        Me.OrganizationCategoriesToolStripMenuItem.Text = "Organization Categories"
        '
        'ClientsToolStripMenuItem
        '
        Me.ClientsToolStripMenuItem.CheckOnClick = True
        Me.ClientsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClientsToolStripMenuItem1, Me.ClientsCategoriesToolStripMenuItem})
        Me.ClientsToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.users_relation21
        Me.ClientsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ClientsToolStripMenuItem.Name = "ClientsToolStripMenuItem"
        Me.ClientsToolStripMenuItem.Size = New System.Drawing.Size(55, 51)
        Me.ClientsToolStripMenuItem.Text = "Clients"
        Me.ClientsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ClientsToolStripMenuItem1
        '
        Me.ClientsToolStripMenuItem1.Image = Global.PPS.My.Resources.Resources.users_relation21
        Me.ClientsToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ClientsToolStripMenuItem1.Name = "ClientsToolStripMenuItem1"
        Me.ClientsToolStripMenuItem1.Size = New System.Drawing.Size(185, 38)
        Me.ClientsToolStripMenuItem1.Text = "Clients"
        '
        'ClientsCategoriesToolStripMenuItem
        '
        Me.ClientsCategoriesToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.elements1
        Me.ClientsCategoriesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ClientsCategoriesToolStripMenuItem.Name = "ClientsCategoriesToolStripMenuItem"
        Me.ClientsCategoriesToolStripMenuItem.Size = New System.Drawing.Size(185, 38)
        Me.ClientsCategoriesToolStripMenuItem.Text = "Clients Categories"
        '
        'ProductsToolStripMenuItem
        '
        Me.ProductsToolStripMenuItem.CheckOnClick = True
        Me.ProductsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProductsToolStripMenuItem1, Me.ProductsCategoriesToolStripMenuItem})
        Me.ProductsToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.barcode
        Me.ProductsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ProductsToolStripMenuItem.Name = "ProductsToolStripMenuItem"
        Me.ProductsToolStripMenuItem.Size = New System.Drawing.Size(66, 51)
        Me.ProductsToolStripMenuItem.Text = "Products"
        Me.ProductsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ProductsToolStripMenuItem1
        '
        Me.ProductsToolStripMenuItem1.Image = Global.PPS.My.Resources.Resources.barcode
        Me.ProductsToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ProductsToolStripMenuItem1.Name = "ProductsToolStripMenuItem1"
        Me.ProductsToolStripMenuItem1.Size = New System.Drawing.Size(196, 38)
        Me.ProductsToolStripMenuItem1.Text = "Products"
        '
        'ProductsCategoriesToolStripMenuItem
        '
        Me.ProductsCategoriesToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.elements1
        Me.ProductsCategoriesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ProductsCategoriesToolStripMenuItem.Name = "ProductsCategoriesToolStripMenuItem"
        Me.ProductsCategoriesToolStripMenuItem.Size = New System.Drawing.Size(196, 38)
        Me.ProductsCategoriesToolStripMenuItem.Text = "Products Categories"
        '
        'VersionsToolStripMenuItem
        '
        Me.VersionsToolStripMenuItem.CheckOnClick = True
        Me.VersionsToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.elements31
        Me.VersionsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.VersionsToolStripMenuItem.Name = "VersionsToolStripMenuItem"
        Me.VersionsToolStripMenuItem.Size = New System.Drawing.Size(63, 51)
        Me.VersionsToolStripMenuItem.Text = "Versions"
        Me.VersionsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'CurrenciesToolStripMenuItem
        '
        Me.CurrenciesToolStripMenuItem.CheckOnClick = True
        Me.CurrenciesToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.symbol_dollar_euro
        Me.CurrenciesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.CurrenciesToolStripMenuItem.Name = "CurrenciesToolStripMenuItem"
        Me.CurrenciesToolStripMenuItem.Size = New System.Drawing.Size(75, 51)
        Me.CurrenciesToolStripMenuItem.Text = "Currencies"
        Me.CurrenciesToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'UsersToolStripMenuItem
        '
        Me.UsersToolStripMenuItem.CheckOnClick = True
        Me.UsersToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.user
        Me.UsersToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.UsersToolStripMenuItem.Name = "UsersToolStripMenuItem"
        Me.UsersToolStripMenuItem.Size = New System.Drawing.Size(47, 51)
        Me.UsersToolStripMenuItem.Text = "Users"
        Me.UsersToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ControlsToolStripMenuItem
        '
        Me.ControlsToolStripMenuItem.Image = Global.PPS.My.Resources.Resources.checkbox2
        Me.ControlsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ControlsToolStripMenuItem.Name = "ControlsToolStripMenuItem"
        Me.ControlsToolStripMenuItem.Size = New System.Drawing.Size(64, 51)
        Me.ControlsToolStripMenuItem.Text = "Controls"
        Me.ControlsToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'PlatformMGTGeneralUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(674, 470)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "PlatformMGTGeneralUI"
        Me.Text = "Configuration"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FinancialsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdjustmentsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrganizationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrganizationCategoriesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClientsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClientsCategoriesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductsCategoriesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CurrenciesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents FinancialsAndOperationalItemsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrganizationToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClientsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ControlsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
