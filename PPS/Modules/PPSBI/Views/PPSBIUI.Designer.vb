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
        Me.entitiesTB = New System.Windows.Forms.TextBox()
        Me.entityLabel = New System.Windows.Forms.Label()
        Me.accountLabel = New System.Windows.Forms.Label()
        Me.accountsTB = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PeriodCB = New System.Windows.Forms.ComboBox()
        Me.CurrencyCB = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.MainGroupBox = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.versionLabel = New System.Windows.Forms.Label()
        Me.selectEntity = New System.Windows.Forms.Button()
        Me.ButtonsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.accountSelect = New System.Windows.Forms.Button()
        Me.versionsTB = New System.Windows.Forms.TextBox()
        Me.VersionSelectionBT = New System.Windows.Forms.Button()
        Me.AdjustmentCB = New System.Windows.Forms.ComboBox()
        Me.validate_cmd = New System.Windows.Forms.Button()
        Me.MainTVsSelectionGB = New System.Windows.Forms.GroupBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SelectionValidateBT = New System.Windows.Forms.Button()
        Me.selectionCancelBT = New System.Windows.Forms.Button()
        Me.accountsIL = New System.Windows.Forms.ImageList(Me.components)
        Me.categoriesSelectionGroupBox = New System.Windows.Forms.GroupBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.CategoriesTVsTabControl = New System.Windows.Forms.CustomTabControl()
        Me.categoriesIL = New System.Windows.Forms.ImageList(Me.components)
        Me.rightSideEpxansionBT = New System.Windows.Forms.Button()
        Me.EntitiesTVImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.VersionsTVIcons = New System.Windows.Forms.ImageList(Me.components)
        Me.MainGroupBox.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MainTVsSelectionGB.SuspendLayout()
        Me.categoriesSelectionGroupBox.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'entitiesTB
        '
        Me.entitiesTB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.entitiesTB.Location = New System.Drawing.Point(164, 3)
        Me.entitiesTB.Name = "entitiesTB"
        Me.entitiesTB.Size = New System.Drawing.Size(289, 20)
        Me.entitiesTB.TabIndex = 0
        '
        'entityLabel
        '
        Me.entityLabel.AutoSize = True
        Me.entityLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.entityLabel.Location = New System.Drawing.Point(3, 0)
        Me.entityLabel.Name = "entityLabel"
        Me.entityLabel.Size = New System.Drawing.Size(33, 13)
        Me.entityLabel.TabIndex = 1
        Me.entityLabel.Text = "Entity"
        '
        'accountLabel
        '
        Me.accountLabel.AutoSize = True
        Me.accountLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.accountLabel.Location = New System.Drawing.Point(3, 45)
        Me.accountLabel.Name = "accountLabel"
        Me.accountLabel.Size = New System.Drawing.Size(47, 13)
        Me.accountLabel.TabIndex = 3
        Me.accountLabel.Text = "Account"
        '
        'accountsTB
        '
        Me.accountsTB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.accountsTB.Location = New System.Drawing.Point(164, 48)
        Me.accountsTB.Name = "accountsTB"
        Me.accountsTB.Size = New System.Drawing.Size(289, 20)
        Me.accountsTB.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 135)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Period"
        '
        'PeriodCB
        '
        Me.PeriodCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PeriodCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PeriodCB.FormattingEnabled = True
        Me.PeriodCB.Location = New System.Drawing.Point(164, 138)
        Me.PeriodCB.Name = "PeriodCB"
        Me.PeriodCB.Size = New System.Drawing.Size(288, 21)
        Me.PeriodCB.TabIndex = 3
        '
        'CurrencyCB
        '
        Me.CurrencyCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CurrencyCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CurrencyCB.FormattingEnabled = True
        Me.CurrencyCB.Location = New System.Drawing.Point(164, 183)
        Me.CurrencyCB.Name = "CurrencyCB"
        Me.CurrencyCB.Size = New System.Drawing.Size(288, 21)
        Me.CurrencyCB.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 180)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Currency"
        '
        'MainGroupBox
        '
        Me.MainGroupBox.BackColor = System.Drawing.SystemColors.Control
        Me.MainGroupBox.Controls.Add(Me.TableLayoutPanel1)
        Me.MainGroupBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainGroupBox.Location = New System.Drawing.Point(22, 42)
        Me.MainGroupBox.Name = "MainGroupBox"
        Me.MainGroupBox.Size = New System.Drawing.Size(537, 329)
        Me.MainGroupBox.TabIndex = 11
        Me.MainGroupBox.TabStop = False
        Me.MainGroupBox.Text = "Formula Inputs"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 46.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 342.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.versionLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.entitiesTB, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.entityLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CurrencyCB, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.selectEntity, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.accountLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodCB, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.accountSelect, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.accountsTB, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.versionsTB, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.VersionSelectionBT, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AdjustmentCB, 2, 5)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(19, 38)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(503, 273)
        Me.TableLayoutPanel1.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 225)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Adjustment"
        '
        'versionLabel
        '
        Me.versionLabel.AutoSize = True
        Me.versionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.versionLabel.Location = New System.Drawing.Point(3, 90)
        Me.versionLabel.Name = "versionLabel"
        Me.versionLabel.Size = New System.Drawing.Size(42, 13)
        Me.versionLabel.TabIndex = 13
        Me.versionLabel.Text = "Version"
        '
        'selectEntity
        '
        Me.selectEntity.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.selectEntity.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.selectEntity.FlatAppearance.BorderSize = 0
        Me.selectEntity.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.selectEntity.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.selectEntity.ImageKey = "config purple circle strl bckgd.png"
        Me.selectEntity.ImageList = Me.ButtonsIL
        Me.selectEntity.Location = New System.Drawing.Point(118, 3)
        Me.selectEntity.Name = "selectEntity"
        Me.selectEntity.Size = New System.Drawing.Size(30, 22)
        Me.selectEntity.TabIndex = 30
        Me.selectEntity.UseVisualStyleBackColor = True
        '
        'ButtonsIL
        '
        Me.ButtonsIL.ImageStream = CType(resources.GetObject("ButtonsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.ButtonsIL.Images.SetKeyName(0, "expand right.png")
        Me.ButtonsIL.Images.SetKeyName(1, "expandleft.png")
        Me.ButtonsIL.Images.SetKeyName(2, "submit 1 ok.ico")
        Me.ButtonsIL.Images.SetKeyName(3, "imageres_89.ico")
        Me.ButtonsIL.Images.SetKeyName(4, "favicon(10).ico")
        Me.ButtonsIL.Images.SetKeyName(5, "favicon(81).ico")
        Me.ButtonsIL.Images.SetKeyName(6, "Scenario.ico")
        Me.ButtonsIL.Images.SetKeyName(7, "db Purple big.ico")
        Me.ButtonsIL.Images.SetKeyName(8, "config purple circle strl bckgd.png")
        '
        'accountSelect
        '
        Me.accountSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.accountSelect.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.accountSelect.FlatAppearance.BorderSize = 0
        Me.accountSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.accountSelect.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.accountSelect.ImageKey = "favicon(81).ico"
        Me.accountSelect.ImageList = Me.ButtonsIL
        Me.accountSelect.Location = New System.Drawing.Point(118, 48)
        Me.accountSelect.Name = "accountSelect"
        Me.accountSelect.Size = New System.Drawing.Size(30, 20)
        Me.accountSelect.TabIndex = 31
        Me.accountSelect.UseVisualStyleBackColor = True
        '
        'versionsTB
        '
        Me.versionsTB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.versionsTB.Location = New System.Drawing.Point(164, 93)
        Me.versionsTB.Name = "versionsTB"
        Me.versionsTB.Size = New System.Drawing.Size(289, 20)
        Me.versionsTB.TabIndex = 2
        '
        'VersionSelectionBT
        '
        Me.VersionSelectionBT.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.VersionSelectionBT.FlatAppearance.BorderColor = System.Drawing.Color.White
        Me.VersionSelectionBT.FlatAppearance.BorderSize = 0
        Me.VersionSelectionBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.VersionSelectionBT.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.VersionSelectionBT.ImageKey = "db Purple big.ico"
        Me.VersionSelectionBT.ImageList = Me.ButtonsIL
        Me.VersionSelectionBT.Location = New System.Drawing.Point(118, 93)
        Me.VersionSelectionBT.Name = "VersionSelectionBT"
        Me.VersionSelectionBT.Size = New System.Drawing.Size(30, 24)
        Me.VersionSelectionBT.TabIndex = 32
        Me.VersionSelectionBT.UseVisualStyleBackColor = True
        '
        'AdjustmentCB
        '
        Me.AdjustmentCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.AdjustmentCB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AdjustmentCB.FormattingEnabled = True
        Me.AdjustmentCB.Location = New System.Drawing.Point(164, 228)
        Me.AdjustmentCB.Name = "AdjustmentCB"
        Me.AdjustmentCB.Size = New System.Drawing.Size(288, 21)
        Me.AdjustmentCB.TabIndex = 5
        '
        'validate_cmd
        '
        Me.validate_cmd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.validate_cmd.FlatAppearance.BorderSize = 0
        Me.validate_cmd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.validate_cmd.ImageKey = "submit 1 ok.ico"
        Me.validate_cmd.ImageList = Me.ButtonsIL
        Me.validate_cmd.Location = New System.Drawing.Point(445, 527)
        Me.validate_cmd.Name = "validate_cmd"
        Me.validate_cmd.Size = New System.Drawing.Size(114, 27)
        Me.validate_cmd.TabIndex = 6
        Me.validate_cmd.Text = "Insert Formula"
        Me.validate_cmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.validate_cmd.UseVisualStyleBackColor = True
        '
        'MainTVsSelectionGB
        '
        Me.MainTVsSelectionGB.Controls.Add(Me.Panel3)
        Me.MainTVsSelectionGB.Controls.Add(Me.Panel2)
        Me.MainTVsSelectionGB.Controls.Add(Me.Panel1)
        Me.MainTVsSelectionGB.Controls.Add(Me.SelectionValidateBT)
        Me.MainTVsSelectionGB.Controls.Add(Me.selectionCancelBT)
        Me.MainTVsSelectionGB.Location = New System.Drawing.Point(601, 32)
        Me.MainTVsSelectionGB.Name = "MainTVsSelectionGB"
        Me.MainTVsSelectionGB.Size = New System.Drawing.Size(379, 525)
        Me.MainTVsSelectionGB.TabIndex = 12
        Me.MainTVsSelectionGB.TabStop = False
        '
        'Panel3
        '
        Me.Panel3.Location = New System.Drawing.Point(25, 20)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(329, 460)
        Me.Panel3.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.Location = New System.Drawing.Point(25, 20)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(315, 387)
        Me.Panel2.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(25, 20)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(315, 387)
        Me.Panel1.TabIndex = 2
        '
        'SelectionValidateBT
        '
        Me.SelectionValidateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SelectionValidateBT.ImageKey = "submit 1 ok.ico"
        Me.SelectionValidateBT.ImageList = Me.ButtonsIL
        Me.SelectionValidateBT.Location = New System.Drawing.Point(213, 489)
        Me.SelectionValidateBT.Name = "SelectionValidateBT"
        Me.SelectionValidateBT.Size = New System.Drawing.Size(88, 25)
        Me.SelectionValidateBT.TabIndex = 1
        Me.SelectionValidateBT.Text = "Validate"
        Me.SelectionValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SelectionValidateBT.UseVisualStyleBackColor = True
        '
        'selectionCancelBT
        '
        Me.selectionCancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.selectionCancelBT.ImageKey = "imageres_89.ico"
        Me.selectionCancelBT.ImageList = Me.ButtonsIL
        Me.selectionCancelBT.Location = New System.Drawing.Point(92, 489)
        Me.selectionCancelBT.Name = "selectionCancelBT"
        Me.selectionCancelBT.Size = New System.Drawing.Size(88, 25)
        Me.selectionCancelBT.TabIndex = 0
        Me.selectionCancelBT.Text = "Cancel"
        Me.selectionCancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.selectionCancelBT.UseVisualStyleBackColor = True
        '
        'accountsIL
        '
        Me.accountsIL.ImageStream = CType(resources.GetObject("accountsIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.accountsIL.TransparentColor = System.Drawing.Color.Transparent
        Me.accountsIL.Images.SetKeyName(0, "imageres_1013.ico")
        Me.accountsIL.Images.SetKeyName(1, "favicon(81).ico")
        Me.accountsIL.Images.SetKeyName(2, "UIscon.png")
        Me.accountsIL.Images.SetKeyName(3, "favicon(247).ico")
        '
        'categoriesSelectionGroupBox
        '
        Me.categoriesSelectionGroupBox.Controls.Add(Me.Panel4)
        Me.categoriesSelectionGroupBox.Location = New System.Drawing.Point(600, 32)
        Me.categoriesSelectionGroupBox.Name = "categoriesSelectionGroupBox"
        Me.categoriesSelectionGroupBox.Size = New System.Drawing.Size(430, 525)
        Me.categoriesSelectionGroupBox.TabIndex = 13
        Me.categoriesSelectionGroupBox.TabStop = False
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.CategoriesTVsTabControl)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 16)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(424, 506)
        Me.Panel4.TabIndex = 4
        '
        'CategoriesTVsTabControl
        '
        Me.CategoriesTVsTabControl.DisplayStyle = System.Windows.Forms.TabStyle.Rounded
        '
        '
        '
        Me.CategoriesTVsTabControl.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark
        Me.CategoriesTVsTabControl.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark
        Me.CategoriesTVsTabControl.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(157, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.CategoriesTVsTabControl.DisplayStyleProvider.CloserColor = System.Drawing.Color.SlateBlue
        Me.CategoriesTVsTabControl.DisplayStyleProvider.FocusColor = System.Drawing.Color.Blue
        Me.CategoriesTVsTabControl.DisplayStyleProvider.FocusTrack = False
        Me.CategoriesTVsTabControl.DisplayStyleProvider.HotTrack = True
        Me.CategoriesTVsTabControl.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CategoriesTVsTabControl.DisplayStyleProvider.Opacity = 1.0!
        Me.CategoriesTVsTabControl.DisplayStyleProvider.Overlap = 0
        Me.CategoriesTVsTabControl.DisplayStyleProvider.Padding = New System.Drawing.Point(6, 3)
        Me.CategoriesTVsTabControl.DisplayStyleProvider.Radius = 10
        Me.CategoriesTVsTabControl.DisplayStyleProvider.ShowTabCloser = True
        Me.CategoriesTVsTabControl.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText
        Me.CategoriesTVsTabControl.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark
        Me.CategoriesTVsTabControl.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText
        Me.CategoriesTVsTabControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CategoriesTVsTabControl.HotTrack = True
        Me.CategoriesTVsTabControl.Location = New System.Drawing.Point(0, 0)
        Me.CategoriesTVsTabControl.Name = "CategoriesTVsTabControl"
        Me.CategoriesTVsTabControl.SelectedIndex = 0
        Me.CategoriesTVsTabControl.Size = New System.Drawing.Size(424, 506)
        Me.CategoriesTVsTabControl.TabIndex = 8
        '
        'categoriesIL
        '
        Me.categoriesIL.ImageStream = CType(resources.GetObject("categoriesIL.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.categoriesIL.TransparentColor = System.Drawing.Color.Transparent
        Me.categoriesIL.Images.SetKeyName(0, "DB Grey.png")
        Me.categoriesIL.Images.SetKeyName(1, "icons-blue.png")
        '
        'rightSideEpxansionBT
        '
        Me.rightSideEpxansionBT.ImageIndex = 0
        Me.rightSideEpxansionBT.ImageList = Me.ButtonsIL
        Me.rightSideEpxansionBT.Location = New System.Drawing.Point(537, 14)
        Me.rightSideEpxansionBT.Name = "rightSideEpxansionBT"
        Me.rightSideEpxansionBT.Size = New System.Drawing.Size(22, 22)
        Me.rightSideEpxansionBT.TabIndex = 14
        Me.rightSideEpxansionBT.UseVisualStyleBackColor = True
        '
        'EntitiesTVImageList
        '
        Me.EntitiesTVImageList.ImageStream = CType(resources.GetObject("EntitiesTVImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent
        Me.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico")
        Me.EntitiesTVImageList.Images.SetKeyName(1, "favicon(110).ico")
        '
        'VersionsTVIcons
        '
        Me.VersionsTVIcons.ImageStream = CType(resources.GetObject("VersionsTVIcons.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.VersionsTVIcons.TransparentColor = System.Drawing.Color.Transparent
        Me.VersionsTVIcons.Images.SetKeyName(0, "favicon.ico")
        Me.VersionsTVIcons.Images.SetKeyName(1, "favicon(81).ico")
        '
        'PPSBI_UI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(576, 569)
        Me.Controls.Add(Me.rightSideEpxansionBT)
        Me.Controls.Add(Me.categoriesSelectionGroupBox)
        Me.Controls.Add(Me.MainTVsSelectionGB)
        Me.Controls.Add(Me.MainGroupBox)
        Me.Controls.Add(Me.validate_cmd)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PPSBI_UI"
        Me.Text = "Function Designer"
        Me.MainGroupBox.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.MainTVsSelectionGB.ResumeLayout(False)
        Me.categoriesSelectionGroupBox.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents entitiesTB As System.Windows.Forms.TextBox
    Friend WithEvents entityLabel As System.Windows.Forms.Label
    Friend WithEvents selectEntity As System.Windows.Forms.Button
    Friend WithEvents accountLabel As System.Windows.Forms.Label
    Friend WithEvents accountSelect As System.Windows.Forms.Button
    Friend WithEvents accountsTB As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PeriodCB As System.Windows.Forms.ComboBox
    Friend WithEvents CurrencyCB As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents validate_cmd As System.Windows.Forms.Button
    Friend WithEvents MainGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents ButtonsIL As System.Windows.Forms.ImageList
    Friend WithEvents MainTVsSelectionGB As System.Windows.Forms.GroupBox
    Friend WithEvents SelectionValidateBT As System.Windows.Forms.Button
    Friend WithEvents selectionCancelBT As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents versionLabel As System.Windows.Forms.Label
    Friend WithEvents versionsTB As System.Windows.Forms.TextBox
    Friend WithEvents VersionSelectionBT As System.Windows.Forms.Button
    Friend WithEvents accountsIL As System.Windows.Forms.ImageList
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents categoriesSelectionGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents categoriesIL As System.Windows.Forms.ImageList
    Friend WithEvents rightSideEpxansionBT As System.Windows.Forms.Button
    Friend WithEvents CategoriesTVsTabControl As System.Windows.Forms.CustomTabControl
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Public WithEvents EntitiesTVImageList As System.Windows.Forms.ImageList
    Friend WithEvents VersionsTVIcons As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents AdjustmentCB As System.Windows.Forms.ComboBox
End Class
