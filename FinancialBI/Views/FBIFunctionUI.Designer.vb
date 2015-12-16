<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FBIFunctionUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FBIFunctionUI))
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.validate_cmd = New viblend.winforms.controls.vButton()
        Me.categoriesIL = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.AdjustmentsTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.ProductsTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.CategoriesFiltersTreebox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_categoryFilterLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.CurrenciesComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.PeriodTreeBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_productFilterLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_clientFilterLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_versionLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_currencyLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_entityLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_accountLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_periodLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.m_adjustmentFilterLabel = New VIBlend.WinForms.Controls.vLabel()
        Me.EntityTreeBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.AccountTreeBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.VersionTreeBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.ClientsTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.m_versionsTreeviewImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico")
        Me.ButtonsIL.Images.SetKeyName(1, "imageres_89.ico")
        Me.ButtonsIL.Images.SetKeyName(2, "plus.ico")
        '
        'validate_cmd
        '
        Me.validate_cmd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.validate_cmd.FlatAppearance.BorderSize = 0
        Me.validate_cmd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.validate_cmd.ImageKey = "submit 1 ok.ico"
        Me.validate_cmd.ImageList = Me.ButtonsIL
        Me.validate_cmd.Location = New System.Drawing.Point(379, 373)
        Me.validate_cmd.Name = "validate_cmd"
        Me.validate_cmd.Size = New System.Drawing.Size(114, 27)
        Me.validate_cmd.TabIndex = 10
        Me.validate_cmd.Text = "Insert formula"
        Me.validate_cmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.validate_cmd.UseVisualStyleBackColor = True
        '
        'categoriesIL
        '
        Me.categoriesIL.ImageStream = CType(resources.GetObject("categoriesIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.categoriesIL.TransparentColor = System.Drawing.Color.Transparent
        Me.categoriesIL.Images.SetKeyName(0, "DB Grey.png")
        Me.categoriesIL.Images.SetKeyName(1, "icons-blue.png")
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.48327!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.51673!))
        Me.TableLayoutPanel1.Controls.Add(Me.AdjustmentsTreeviewBox, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.ProductsTreeviewBox, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.CategoriesFiltersTreebox, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.m_categoryFilterLabel, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.CurrenciesComboBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodTreeBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.m_productFilterLabel, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.m_clientFilterLabel, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.m_versionLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.m_currencyLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.m_entityLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.m_accountLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.m_periodLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.m_adjustmentFilterLabel, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.EntityTreeBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AccountTreeBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionTreeBox, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ClientsTreeviewBox, 1, 5)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(37, 38)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 9
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(459, 314)
        Me.TableLayoutPanel1.TabIndex = 39
        '
        'AdjustmentsTreeviewBox
        '
        Me.AdjustmentsTreeviewBox.BackColor = System.Drawing.Color.White
        Me.AdjustmentsTreeviewBox.BorderColor = System.Drawing.Color.Black
        Me.AdjustmentsTreeviewBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdjustmentsTreeviewBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.AdjustmentsTreeviewBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.AdjustmentsTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.AdjustmentsTreeviewBox.Location = New System.Drawing.Point(142, 248)
        Me.AdjustmentsTreeviewBox.Name = "AdjustmentsTreeviewBox"
        Me.AdjustmentsTreeviewBox.Size = New System.Drawing.Size(314, 29)
        Me.AdjustmentsTreeviewBox.TabIndex = 8
        Me.AdjustmentsTreeviewBox.UseThemeBackColor = False
        Me.AdjustmentsTreeviewBox.UseThemeDropDownArrowColor = True
        Me.AdjustmentsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ProductsTreeviewBox
        '
        Me.ProductsTreeviewBox.BackColor = System.Drawing.Color.White
        Me.ProductsTreeviewBox.BorderColor = System.Drawing.Color.Black
        Me.ProductsTreeviewBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProductsTreeviewBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.ProductsTreeviewBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.ProductsTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.ProductsTreeviewBox.Location = New System.Drawing.Point(142, 213)
        Me.ProductsTreeviewBox.Name = "ProductsTreeviewBox"
        Me.ProductsTreeviewBox.Size = New System.Drawing.Size(314, 29)
        Me.ProductsTreeviewBox.TabIndex = 7
        Me.ProductsTreeviewBox.UseThemeBackColor = False
        Me.ProductsTreeviewBox.UseThemeDropDownArrowColor = True
        Me.ProductsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'CategoriesFiltersTreebox
        '
        Me.CategoriesFiltersTreebox.BackColor = System.Drawing.Color.White
        Me.CategoriesFiltersTreebox.BorderColor = System.Drawing.Color.Black
        Me.CategoriesFiltersTreebox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CategoriesFiltersTreebox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.CategoriesFiltersTreebox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.CategoriesFiltersTreebox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.CategoriesFiltersTreebox.Location = New System.Drawing.Point(142, 283)
        Me.CategoriesFiltersTreebox.Name = "CategoriesFiltersTreebox"
        Me.CategoriesFiltersTreebox.Size = New System.Drawing.Size(314, 29)
        Me.CategoriesFiltersTreebox.TabIndex = 9
        Me.CategoriesFiltersTreebox.UseThemeBackColor = False
        Me.CategoriesFiltersTreebox.UseThemeDropDownArrowColor = True
        Me.CategoriesFiltersTreebox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_categoryFilterLabel
        '
        Me.m_categoryFilterLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_categoryFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_categoryFilterLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_categoryFilterLabel.Ellipsis = False
        Me.m_categoryFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_categoryFilterLabel.Location = New System.Drawing.Point(3, 283)
        Me.m_categoryFilterLabel.Multiline = True
        Me.m_categoryFilterLabel.Name = "m_categoryFilterLabel"
        Me.m_categoryFilterLabel.Size = New System.Drawing.Size(133, 29)
        Me.m_categoryFilterLabel.TabIndex = 38
        Me.m_categoryFilterLabel.Text = "Categories filter"
        Me.m_categoryFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_categoryFilterLabel.UseMnemonics = True
        Me.m_categoryFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'CurrenciesComboBox
        '
        Me.CurrenciesComboBox.BackColor = System.Drawing.Color.White
        Me.CurrenciesComboBox.DisplayMember = ""
        Me.CurrenciesComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CurrenciesComboBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.CurrenciesComboBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.CurrenciesComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.CurrenciesComboBox.DropDownWidth = 314
        Me.CurrenciesComboBox.Location = New System.Drawing.Point(142, 108)
        Me.CurrenciesComboBox.Name = "CurrenciesComboBox"
        Me.CurrenciesComboBox.RoundedCornersMaskListItem = CType(15, Byte)
        Me.CurrenciesComboBox.Size = New System.Drawing.Size(314, 29)
        Me.CurrenciesComboBox.TabIndex = 4
        Me.CurrenciesComboBox.UseThemeBackColor = False
        Me.CurrenciesComboBox.UseThemeDropDownArrowColor = True
        Me.CurrenciesComboBox.ValueMember = ""
        Me.CurrenciesComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        Me.CurrenciesComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'PeriodTreeBox
        '
        Me.PeriodTreeBox.BackColor = System.Drawing.Color.White
        Me.PeriodTreeBox.BorderColor = System.Drawing.Color.Black
        Me.PeriodTreeBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PeriodTreeBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.PeriodTreeBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.PeriodTreeBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.PeriodTreeBox.Location = New System.Drawing.Point(142, 73)
        Me.PeriodTreeBox.Name = "PeriodTreeBox"
        Me.PeriodTreeBox.Size = New System.Drawing.Size(314, 29)
        Me.PeriodTreeBox.TabIndex = 3
        Me.PeriodTreeBox.UseThemeBackColor = False
        Me.PeriodTreeBox.UseThemeDropDownArrowColor = True
        Me.PeriodTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_productFilterLabel
        '
        Me.m_productFilterLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_productFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_productFilterLabel.Ellipsis = False
        Me.m_productFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_productFilterLabel.Location = New System.Drawing.Point(3, 213)
        Me.m_productFilterLabel.Multiline = True
        Me.m_productFilterLabel.Name = "m_productFilterLabel"
        Me.m_productFilterLabel.Size = New System.Drawing.Size(123, 24)
        Me.m_productFilterLabel.TabIndex = 36
        Me.m_productFilterLabel.Text = "Products filter"
        Me.m_productFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_productFilterLabel.UseMnemonics = True
        Me.m_productFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_clientFilterLabel
        '
        Me.m_clientFilterLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_clientFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_clientFilterLabel.Ellipsis = False
        Me.m_clientFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_clientFilterLabel.Location = New System.Drawing.Point(3, 178)
        Me.m_clientFilterLabel.Multiline = True
        Me.m_clientFilterLabel.Name = "m_clientFilterLabel"
        Me.m_clientFilterLabel.Size = New System.Drawing.Size(123, 24)
        Me.m_clientFilterLabel.TabIndex = 35
        Me.m_clientFilterLabel.Text = "Clients filter"
        Me.m_clientFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_clientFilterLabel.UseMnemonics = True
        Me.m_clientFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_versionLabel
        '
        Me.m_versionLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_versionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_versionLabel.Ellipsis = False
        Me.m_versionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_versionLabel.Location = New System.Drawing.Point(3, 143)
        Me.m_versionLabel.Multiline = True
        Me.m_versionLabel.Name = "m_versionLabel"
        Me.m_versionLabel.Size = New System.Drawing.Size(123, 24)
        Me.m_versionLabel.TabIndex = 34
        Me.m_versionLabel.Text = "Version"
        Me.m_versionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_versionLabel.UseMnemonics = True
        Me.m_versionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_currencyLabel
        '
        Me.m_currencyLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_currencyLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_currencyLabel.Ellipsis = False
        Me.m_currencyLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_currencyLabel.Location = New System.Drawing.Point(3, 108)
        Me.m_currencyLabel.Multiline = True
        Me.m_currencyLabel.Name = "m_currencyLabel"
        Me.m_currencyLabel.Size = New System.Drawing.Size(123, 24)
        Me.m_currencyLabel.TabIndex = 33
        Me.m_currencyLabel.Text = "Currency"
        Me.m_currencyLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_currencyLabel.UseMnemonics = True
        Me.m_currencyLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_entityLabel
        '
        Me.m_entityLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_entityLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_entityLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.m_entityLabel.Ellipsis = False
        Me.m_entityLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_entityLabel.Location = New System.Drawing.Point(3, 3)
        Me.m_entityLabel.Multiline = True
        Me.m_entityLabel.Name = "m_entityLabel"
        Me.m_entityLabel.Size = New System.Drawing.Size(133, 29)
        Me.m_entityLabel.TabIndex = 30
        Me.m_entityLabel.Text = "Entity"
        Me.m_entityLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_entityLabel.UseMnemonics = True
        Me.m_entityLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_accountLabel
        '
        Me.m_accountLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_accountLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_accountLabel.Ellipsis = False
        Me.m_accountLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_accountLabel.Location = New System.Drawing.Point(3, 38)
        Me.m_accountLabel.Multiline = True
        Me.m_accountLabel.Name = "m_accountLabel"
        Me.m_accountLabel.Size = New System.Drawing.Size(123, 24)
        Me.m_accountLabel.TabIndex = 31
        Me.m_accountLabel.Text = "Account"
        Me.m_accountLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_accountLabel.UseMnemonics = True
        Me.m_accountLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_periodLabel
        '
        Me.m_periodLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_periodLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_periodLabel.Ellipsis = False
        Me.m_periodLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_periodLabel.Location = New System.Drawing.Point(3, 73)
        Me.m_periodLabel.Multiline = True
        Me.m_periodLabel.Name = "m_periodLabel"
        Me.m_periodLabel.Size = New System.Drawing.Size(123, 24)
        Me.m_periodLabel.TabIndex = 32
        Me.m_periodLabel.Text = "Period"
        Me.m_periodLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_periodLabel.UseMnemonics = True
        Me.m_periodLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_adjustmentFilterLabel
        '
        Me.m_adjustmentFilterLabel.BackColor = System.Drawing.Color.Transparent
        Me.m_adjustmentFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.m_adjustmentFilterLabel.Ellipsis = False
        Me.m_adjustmentFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.m_adjustmentFilterLabel.Location = New System.Drawing.Point(3, 248)
        Me.m_adjustmentFilterLabel.Multiline = True
        Me.m_adjustmentFilterLabel.Name = "m_adjustmentFilterLabel"
        Me.m_adjustmentFilterLabel.Size = New System.Drawing.Size(123, 24)
        Me.m_adjustmentFilterLabel.TabIndex = 37
        Me.m_adjustmentFilterLabel.Text = "Adjustments filter"
        Me.m_adjustmentFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.m_adjustmentFilterLabel.UseMnemonics = True
        Me.m_adjustmentFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'EntityTreeBox
        '
        Me.EntityTreeBox.BackColor = System.Drawing.Color.White
        Me.EntityTreeBox.BorderColor = System.Drawing.Color.Black
        Me.EntityTreeBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.EntityTreeBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.EntityTreeBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.EntityTreeBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.EntityTreeBox.Location = New System.Drawing.Point(142, 3)
        Me.EntityTreeBox.Name = "EntityTreeBox"
        Me.EntityTreeBox.Size = New System.Drawing.Size(314, 29)
        Me.EntityTreeBox.TabIndex = 1
        Me.EntityTreeBox.UseThemeBackColor = False
        Me.EntityTreeBox.UseThemeDropDownArrowColor = True
        Me.EntityTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'AccountTreeBox
        '
        Me.AccountTreeBox.BackColor = System.Drawing.Color.White
        Me.AccountTreeBox.BorderColor = System.Drawing.Color.Black
        Me.AccountTreeBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountTreeBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.AccountTreeBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.AccountTreeBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.AccountTreeBox.Location = New System.Drawing.Point(142, 38)
        Me.AccountTreeBox.Name = "AccountTreeBox"
        Me.AccountTreeBox.Size = New System.Drawing.Size(314, 29)
        Me.AccountTreeBox.TabIndex = 2
        Me.AccountTreeBox.UseThemeBackColor = False
        Me.AccountTreeBox.UseThemeDropDownArrowColor = True
        Me.AccountTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VersionTreeBox
        '
        Me.VersionTreeBox.BackColor = System.Drawing.Color.White
        Me.VersionTreeBox.BorderColor = System.Drawing.Color.Black
        Me.VersionTreeBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionTreeBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.VersionTreeBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.VersionTreeBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.VersionTreeBox.Location = New System.Drawing.Point(142, 143)
        Me.VersionTreeBox.Name = "VersionTreeBox"
        Me.VersionTreeBox.Size = New System.Drawing.Size(314, 29)
        Me.VersionTreeBox.TabIndex = 5
        Me.VersionTreeBox.UseThemeBackColor = False
        Me.VersionTreeBox.UseThemeDropDownArrowColor = True
        Me.VersionTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'ClientsTreeviewBox
        '
        Me.ClientsTreeviewBox.BackColor = System.Drawing.Color.White
        Me.ClientsTreeviewBox.BorderColor = System.Drawing.Color.Black
        Me.ClientsTreeviewBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ClientsTreeviewBox.DropDownMaximumSize = New System.Drawing.Size(1000, 1000)
        Me.ClientsTreeviewBox.DropDownMinimumSize = New System.Drawing.Size(10, 10)
        Me.ClientsTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both
        Me.ClientsTreeviewBox.Location = New System.Drawing.Point(142, 178)
        Me.ClientsTreeviewBox.Name = "ClientsTreeviewBox"
        Me.ClientsTreeviewBox.Size = New System.Drawing.Size(314, 29)
        Me.ClientsTreeviewBox.TabIndex = 6
        Me.ClientsTreeviewBox.UseThemeBackColor = False
        Me.ClientsTreeviewBox.UseThemeDropDownArrowColor = True
        Me.ClientsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'm_versionsTreeviewImageList
        '
        Me.m_versionsTreeviewImageList.ImageStream = CType(resources.GetObject("m_versionsTreeviewImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico")
        Me.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico")
        '
        'PPSBI_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(529, 422)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.validate_cmd)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PPSBI_UI"
        Me.Text = "Financial BI Excel Function"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents validate_cmd As viblend.winforms.controls.vButton
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents categoriesIL As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents m_categoryFilterLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_productFilterLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_clientFilterLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_versionLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_currencyLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_entityLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_accountLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_periodLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents m_adjustmentFilterLabel As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents PeriodTreeBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents EntityTreeBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents AccountTreeBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents VersionTreeBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents CurrenciesComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents CategoriesFiltersTreebox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents AdjustmentsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents ProductsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents ClientsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents m_versionsTreeviewImageList As System.Windows.Forms.ImageList
End Class
