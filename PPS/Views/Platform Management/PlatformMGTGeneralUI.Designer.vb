﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
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
        Me.AccountsBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntitiesBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClientsBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductsBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdjustmentsBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.VersionsBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.CurrenciesBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ControlsBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.CategoriesBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntitiesFiltersBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClientsFiltersBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProductsFiltersBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdjustmentsFiltersBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsersBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupsBT = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExchangeRatesButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AccountsBT, Me.EntitiesBT, Me.ClientsBT, Me.ProductsBT, Me.AdjustmentsBT, Me.VersionsBT, Me.CurrenciesBT, Me.ExchangeRatesButton, Me.UsersBT, Me.GroupsBT, Me.ControlsBT, Me.CategoriesBT})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1009, 55)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'AccountsBT
        '
        Me.AccountsBT.CheckOnClick = True
        Me.AccountsBT.Image = Global.FinancialBI.My.Resources.Resources.favicon_15_
        Me.AccountsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.AccountsBT.Name = "AccountsBT"
        Me.AccountsBT.Size = New System.Drawing.Size(69, 51)
        Me.AccountsBT.Text = "Accounts"
        Me.AccountsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.AccountsBT.ToolTipText = "Financial and Operational Accounts Edition  (All changes applied will be availabl" & _
    "e for all users)"
        '
        'EntitiesBT
        '
        Me.EntitiesBT.CheckOnClick = True
        Me.EntitiesBT.Image = Global.FinancialBI.My.Resources.Resources.elements_hierarchy
        Me.EntitiesBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.EntitiesBT.Name = "EntitiesBT"
        Me.EntitiesBT.Size = New System.Drawing.Size(111, 51)
        Me.EntitiesBT.Text = "Entities Hierarchy"
        Me.EntitiesBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.EntitiesBT.ToolTipText = "Legal Organization Hierarchy Ediition  (All changes applied will be available for" & _
    " all users)"
        '
        'ClientsBT
        '
        Me.ClientsBT.CheckOnClick = True
        Me.ClientsBT.Image = Global.FinancialBI.My.Resources.Resources.users_relation
        Me.ClientsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ClientsBT.Name = "ClientsBT"
        Me.ClientsBT.Size = New System.Drawing.Size(55, 51)
        Me.ClientsBT.Text = "Clients"
        Me.ClientsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ClientsBT.ToolTipText = "Clients Edition  (All changes applied will be available for all users)"
        '
        'ProductsBT
        '
        Me.ProductsBT.CheckOnClick = True
        Me.ProductsBT.Image = Global.FinancialBI.My.Resources.Resources.barcode1
        Me.ProductsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ProductsBT.Name = "ProductsBT"
        Me.ProductsBT.Size = New System.Drawing.Size(66, 51)
        Me.ProductsBT.Text = "Products"
        Me.ProductsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ProductsBT.ToolTipText = "Products Edition  (All changes applied will be available for all users)"
        '
        'AdjustmentsBT
        '
        Me.AdjustmentsBT.Image = Global.FinancialBI.My.Resources.Resources.elements4
        Me.AdjustmentsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.AdjustmentsBT.Name = "AdjustmentsBT"
        Me.AdjustmentsBT.Size = New System.Drawing.Size(86, 51)
        Me.AdjustmentsBT.Text = "Adjustments"
        Me.AdjustmentsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.AdjustmentsBT.ToolTipText = "Adjustments Edition  (All changes applied will be available for all users)"
        '
        'VersionsBT
        '
        Me.VersionsBT.CheckOnClick = True
        Me.VersionsBT.Image = Global.FinancialBI.My.Resources.Resources.Excel_dark_24_24
        Me.VersionsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.VersionsBT.Name = "VersionsBT"
        Me.VersionsBT.Size = New System.Drawing.Size(62, 51)
        Me.VersionsBT.Text = "Versions"
        Me.VersionsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.VersionsBT.ToolTipText = "Versions Edition  (All changes applied will be available for all users)"
        '
        'CurrenciesBT
        '
        Me.CurrenciesBT.CheckOnClick = True
        Me.CurrenciesBT.Image = Global.FinancialBI.My.Resources.Resources.symbol_dollar_euro1
        Me.CurrenciesBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.CurrenciesBT.Name = "CurrenciesBT"
        Me.CurrenciesBT.Size = New System.Drawing.Size(75, 51)
        Me.CurrenciesBT.Text = "Currencies"
        Me.CurrenciesBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CurrenciesBT.ToolTipText = "Currencies Edition (All changes applied will be available for all users)"
        '
        'ControlsBT
        '
        Me.ControlsBT.Enabled = False
        Me.ControlsBT.Image = Global.FinancialBI.My.Resources.Resources.ok
        Me.ControlsBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ControlsBT.Name = "ControlsBT"
        Me.ControlsBT.Size = New System.Drawing.Size(64, 51)
        Me.ControlsBT.Text = "Controls"
        Me.ControlsBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ControlsBT.ToolTipText = "Controls Edition  (All changes applied will be available for all users)"
        '
        'CategoriesBT
        '
        Me.CategoriesBT.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EntitiesFiltersBT, Me.ClientsFiltersBT, Me.ProductsFiltersBT, Me.AdjustmentsFiltersBT})
        Me.CategoriesBT.Image = Global.FinancialBI.My.Resources.Resources.filter_and_sort
        Me.CategoriesBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.CategoriesBT.Name = "CategoriesBT"
        Me.CategoriesBT.Size = New System.Drawing.Size(75, 51)
        Me.CategoriesBT.Text = "Categories"
        Me.CategoriesBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.CategoriesBT.ToolTipText = "Entities, Clients, Products and Adjustments Categories Edition  (All changes appl" & _
    "ied will be available for all users)"
        '
        'EntitiesFiltersBT
        '
        Me.EntitiesFiltersBT.Image = Global.FinancialBI.My.Resources.Resources.elements_hierarchy
        Me.EntitiesFiltersBT.Name = "EntitiesFiltersBT"
        Me.EntitiesFiltersBT.Size = New System.Drawing.Size(141, 22)
        Me.EntitiesFiltersBT.Text = "Entities"
        '
        'ClientsFiltersBT
        '
        Me.ClientsFiltersBT.Image = Global.FinancialBI.My.Resources.Resources.users_relation
        Me.ClientsFiltersBT.Name = "ClientsFiltersBT"
        Me.ClientsFiltersBT.Size = New System.Drawing.Size(141, 22)
        Me.ClientsFiltersBT.Text = "Clients"
        '
        'ProductsFiltersBT
        '
        Me.ProductsFiltersBT.Image = Global.FinancialBI.My.Resources.Resources.barcode1
        Me.ProductsFiltersBT.Name = "ProductsFiltersBT"
        Me.ProductsFiltersBT.Size = New System.Drawing.Size(141, 22)
        Me.ProductsFiltersBT.Text = "Products"
        '
        'AdjustmentsFiltersBT
        '
        Me.AdjustmentsFiltersBT.Image = Global.FinancialBI.My.Resources.Resources.elements4
        Me.AdjustmentsFiltersBT.Name = "AdjustmentsFiltersBT"
        Me.AdjustmentsFiltersBT.Size = New System.Drawing.Size(141, 22)
        Me.AdjustmentsFiltersBT.Text = "Adjustments"
        '
        'UsersBT
        '
        Me.UsersBT.CheckOnClick = True
        Me.UsersBT.Image = Global.FinancialBI.My.Resources.Resources.user1
        Me.UsersBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.UsersBT.Name = "UsersBT"
        Me.UsersBT.Size = New System.Drawing.Size(47, 51)
        Me.UsersBT.Text = "Users"
        Me.UsersBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.UsersBT.ToolTipText = "Users Edition"
        '
        'GroupsBT
        '
        Me.GroupsBT.Name = "GroupsBT"
        Me.GroupsBT.Size = New System.Drawing.Size(57, 51)
        Me.GroupsBT.Text = "Groups"
        '
        'ExchangeRatesButton
        '
        Me.ExchangeRatesButton.Image = Global.FinancialBI.My.Resources.Resources.favicon_249_
        Me.ExchangeRatesButton.Name = "ExchangeRatesButton"
        Me.ExchangeRatesButton.Size = New System.Drawing.Size(100, 51)
        Me.ExchangeRatesButton.Text = "Exchange Rates"
        Me.ExchangeRatesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ExchangeRatesButton.ToolTipText = "Edit the currencies used in Financial BI"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Location = New System.Drawing.Point(0, 83)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1009, 432)
        Me.Panel1.TabIndex = 1
        '
        'PlatformMGTGeneralUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1009, 515)
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
    Friend WithEvents AccountsBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EntitiesBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClientsBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductsBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VersionsBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CurrenciesBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsersBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ControlsBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdjustmentsBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CategoriesBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClientsFiltersBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EntitiesFiltersBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProductsFiltersBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AdjustmentsFiltersBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupsBT As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExchangeRatesButton As System.Windows.Forms.ToolStripMenuItem
End Class
