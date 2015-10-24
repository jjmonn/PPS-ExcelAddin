<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PPSBI_UI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PPSBI_UI))
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.validate_cmd = New System.Windows.Forms.Button()
        Me.categoriesIL = New System.Windows.Forms.ImageList(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.AdjustmentsTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.ProductsTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.CategoriesFiltersTreebox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.VLabel9 = New VIBlend.WinForms.Controls.vLabel()
        Me.CurrenciesComboBox = New VIBlend.WinForms.Controls.vComboBox()
        Me.PeriodTreeBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.VLabel7 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel6 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel5 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel4 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel1 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel2 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel3 = New VIBlend.WinForms.Controls.vLabel()
        Me.VLabel8 = New VIBlend.WinForms.Controls.vLabel()
        Me.EntityTreeBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.AccountTreeBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.VersionTreeBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.ClientsTreeviewBox = New VIBlend.WinForms.Controls.vTreeViewBox()
        Me.VersionsTVIcons = New System.Windows.Forms.ImageList(Me.components)
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
        Me.validate_cmd.Text = Local.GetValue("ppsbi.insert_formula")
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
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel9, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.CurrenciesComboBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodTreeBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel7, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel6, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel5, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel4, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.VLabel8, 0, 7)
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
        Me.AdjustmentsTreeviewBox.Text = ""
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
        Me.ProductsTreeviewBox.Text = ""
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
        Me.CategoriesFiltersTreebox.Text = ""
        Me.CategoriesFiltersTreebox.UseThemeBackColor = False
        Me.CategoriesFiltersTreebox.UseThemeDropDownArrowColor = True
        Me.CategoriesFiltersTreebox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel9
        '
        Me.VLabel9.BackColor = System.Drawing.Color.Transparent
        Me.VLabel9.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel9.Ellipsis = False
        Me.VLabel9.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel9.Location = New System.Drawing.Point(3, 283)
        Me.VLabel9.Multiline = True
        Me.VLabel9.Name = "VLabel9"
        Me.VLabel9.Size = New System.Drawing.Size(133, 29)
        Me.VLabel9.TabIndex = 38
        Me.VLabel9.Text = Local.GetValue("ppsbi.categories_filter")
        Me.VLabel9.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel9.UseMnemonics = True
        Me.VLabel9.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        Me.PeriodTreeBox.Text = ""
        Me.PeriodTreeBox.UseThemeBackColor = False
        Me.PeriodTreeBox.UseThemeDropDownArrowColor = True
        Me.PeriodTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel7
        '
        Me.VLabel7.BackColor = System.Drawing.Color.Transparent
        Me.VLabel7.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel7.Ellipsis = False
        Me.VLabel7.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel7.Location = New System.Drawing.Point(3, 213)
        Me.VLabel7.Multiline = True
        Me.VLabel7.Name = "VLabel7"
        Me.VLabel7.Size = New System.Drawing.Size(123, 24)
        Me.VLabel7.TabIndex = 36
        Me.VLabel7.Text = Local.GetValue("ppsbi.products_filter")
        Me.VLabel7.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel7.UseMnemonics = True
        Me.VLabel7.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel6
        '
        Me.VLabel6.BackColor = System.Drawing.Color.Transparent
        Me.VLabel6.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel6.Ellipsis = False
        Me.VLabel6.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel6.Location = New System.Drawing.Point(3, 178)
        Me.VLabel6.Multiline = True
        Me.VLabel6.Name = "VLabel6"
        Me.VLabel6.Size = New System.Drawing.Size(123, 24)
        Me.VLabel6.TabIndex = 35
        Me.VLabel6.Text = Local.GetValue("ppsbi.clients_filter")
        Me.VLabel6.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel6.UseMnemonics = True
        Me.VLabel6.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel5
        '
        Me.VLabel5.BackColor = System.Drawing.Color.Transparent
        Me.VLabel5.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel5.Ellipsis = False
        Me.VLabel5.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel5.Location = New System.Drawing.Point(3, 143)
        Me.VLabel5.Multiline = True
        Me.VLabel5.Name = "VLabel5"
        Me.VLabel5.Size = New System.Drawing.Size(123, 24)
        Me.VLabel5.TabIndex = 34
        Me.VLabel5.Text = Local.GetValue("general.version")
        Me.VLabel5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel5.UseMnemonics = True
        Me.VLabel5.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel4
        '
        Me.VLabel4.BackColor = System.Drawing.Color.Transparent
        Me.VLabel4.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel4.Ellipsis = False
        Me.VLabel4.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel4.Location = New System.Drawing.Point(3, 108)
        Me.VLabel4.Multiline = True
        Me.VLabel4.Name = "VLabel4"
        Me.VLabel4.Size = New System.Drawing.Size(123, 24)
        Me.VLabel4.TabIndex = 33
        Me.VLabel4.Text = Local.GetValue("general.currency")
        Me.VLabel4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel4.UseMnemonics = True
        Me.VLabel4.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel1
        '
        Me.VLabel1.BackColor = System.Drawing.Color.Transparent
        Me.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VLabel1.Ellipsis = False
        Me.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel1.Location = New System.Drawing.Point(3, 3)
        Me.VLabel1.Multiline = True
        Me.VLabel1.Name = "VLabel1"
        Me.VLabel1.Size = New System.Drawing.Size(133, 29)
        Me.VLabel1.TabIndex = 30
        Me.VLabel1.Text = Local.GetValue("general.entity")
        Me.VLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel1.UseMnemonics = True
        Me.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel2
        '
        Me.VLabel2.BackColor = System.Drawing.Color.Transparent
        Me.VLabel2.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel2.Ellipsis = False
        Me.VLabel2.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel2.Location = New System.Drawing.Point(3, 38)
        Me.VLabel2.Multiline = True
        Me.VLabel2.Name = "VLabel2"
        Me.VLabel2.Size = New System.Drawing.Size(123, 24)
        Me.VLabel2.TabIndex = 31
        Me.VLabel2.Text = Local.GetValue("general.account")
        Me.VLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel2.UseMnemonics = True
        Me.VLabel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel3
        '
        Me.VLabel3.BackColor = System.Drawing.Color.Transparent
        Me.VLabel3.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel3.Ellipsis = False
        Me.VLabel3.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel3.Location = New System.Drawing.Point(3, 73)
        Me.VLabel3.Multiline = True
        Me.VLabel3.Name = "VLabel3"
        Me.VLabel3.Size = New System.Drawing.Size(123, 24)
        Me.VLabel3.TabIndex = 32
        Me.VLabel3.Text = Local.GetValue("general.period")
        Me.VLabel3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel3.UseMnemonics = True
        Me.VLabel3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VLabel8
        '
        Me.VLabel8.BackColor = System.Drawing.Color.Transparent
        Me.VLabel8.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly
        Me.VLabel8.Ellipsis = False
        Me.VLabel8.ImageAlignment = System.Drawing.ContentAlignment.TopLeft
        Me.VLabel8.Location = New System.Drawing.Point(3, 248)
        Me.VLabel8.Multiline = True
        Me.VLabel8.Name = "VLabel8"
        Me.VLabel8.Size = New System.Drawing.Size(123, 24)
        Me.VLabel8.TabIndex = 37
        Me.VLabel8.Text = Local.GetValue("ppsbi.adjustments_filter")
        Me.VLabel8.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Me.VLabel8.UseMnemonics = True
        Me.VLabel8.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
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
        Me.EntityTreeBox.Text = ""
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
        Me.AccountTreeBox.Text = ""
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
        Me.VersionTreeBox.Text = ""
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
        Me.ClientsTreeviewBox.Text = ""
        Me.ClientsTreeviewBox.UseThemeBackColor = False
        Me.ClientsTreeviewBox.UseThemeDropDownArrowColor = True
        Me.ClientsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE
        '
        'VersionsTVIcons
        '
        Me.VersionsTVIcons.ImageStream = CType(resources.GetObject("VersionsTVIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.VersionsTVIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.VersionsTVIcons.Images.SetKeyName(0, "breakpoint.png")
        Me.VersionsTVIcons.Images.SetKeyName(1, "favicon(81).ico")
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
        Me.Text = Local.GetValue("ppsbi.title")
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents validate_cmd As System.Windows.Forms.Button
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents categoriesIL As System.Windows.Forms.ImageList
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents VLabel9 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel7 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel6 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel5 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel4 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel1 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel2 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel3 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents VLabel8 As VIBlend.WinForms.Controls.vLabel
    Friend WithEvents PeriodTreeBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents EntityTreeBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents AccountTreeBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents VersionTreeBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents CurrenciesComboBox As VIBlend.WinForms.Controls.vComboBox
    Friend WithEvents CategoriesFiltersTreebox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents AdjustmentsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents ProductsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents ClientsTreeviewBox As VIBlend.WinForms.Controls.vTreeViewBox
    Friend WithEvents VersionsTVIcons As System.Windows.Forms.ImageList
End Class
