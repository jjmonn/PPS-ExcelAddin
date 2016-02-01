using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class FBIFunctionUI : System.Windows.Forms.Form
{

	//Form overrides dispose to clean up the component list.
	[System.Diagnostics.DebuggerNonUserCode()]
	protected override void Dispose(bool disposing)
	{
		try {
			if (disposing && components != null) {
				components.Dispose();
			}
		} finally {
			base.Dispose(disposing);
		}
	}

	//Required by the Windows Form Designer

	private System.ComponentModel.IContainer components;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.  
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBIFunctionUI));
		this.ButtonsIL = new System.Windows.Forms.ImageList(this.components);
		this.validate_cmd = new VIBlend.WinForms.Controls.vButton();
		this.categoriesIL = new System.Windows.Forms.ImageList(this.components);
		this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.AdjustmentsTreeviewBox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.ProductsTreeviewBox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.CategoriesFiltersTreebox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.m_categoryFilterLabel = new VIBlend.WinForms.Controls.vLabel();
		this.CurrenciesComboBox = new VIBlend.WinForms.Controls.vComboBox();
		this.PeriodTreeBox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.m_productFilterLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_clientFilterLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_versionLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_currencyLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_entityLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_accountLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_periodLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_adjustmentFilterLabel = new VIBlend.WinForms.Controls.vLabel();
		this.EntityTreeBox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.AccountTreeBox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.VersionTreeBox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.ClientsTreeviewBox = new VIBlend.WinForms.Controls.vTreeViewBox();
		this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
		this.TableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		//
		//ButtonsIL
		//
		this.ButtonsIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonsIL.ImageStream");
		this.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent;
		this.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico");
		this.ButtonsIL.Images.SetKeyName(1, "imageres_89.ico");
		this.ButtonsIL.Images.SetKeyName(2, "plus.ico");
		//
		//validate_cmd
		//
		this.validate_cmd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.validate_cmd.FlatAppearance.BorderSize = 0;
		this.validate_cmd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.validate_cmd.ImageKey = "submit 1 ok.ico";
		this.validate_cmd.ImageList = this.ButtonsIL;
		this.validate_cmd.Location = new System.Drawing.Point(379, 373);
		this.validate_cmd.Name = "validate_cmd";
		this.validate_cmd.Size = new System.Drawing.Size(114, 27);
		this.validate_cmd.TabIndex = 10;
		this.validate_cmd.Text = "Insert formula";
		this.validate_cmd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.validate_cmd.UseVisualStyleBackColor = true;
		//
		//categoriesIL
		//
		this.categoriesIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("categoriesIL.ImageStream");
		this.categoriesIL.TransparentColor = System.Drawing.Color.Transparent;
		this.categoriesIL.Images.SetKeyName(0, "DB Grey.png");
		this.categoriesIL.Images.SetKeyName(1, "icons-blue.png");
		//
		//TableLayoutPanel1
		//
		this.TableLayoutPanel1.ColumnCount = 2;
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.48327f));
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.51673f));
		this.TableLayoutPanel1.Controls.Add(this.AdjustmentsTreeviewBox, 1, 7);
		this.TableLayoutPanel1.Controls.Add(this.ProductsTreeviewBox, 1, 6);
		this.TableLayoutPanel1.Controls.Add(this.CategoriesFiltersTreebox, 1, 8);
		this.TableLayoutPanel1.Controls.Add(this.m_categoryFilterLabel, 0, 8);
		this.TableLayoutPanel1.Controls.Add(this.CurrenciesComboBox, 1, 3);
		this.TableLayoutPanel1.Controls.Add(this.PeriodTreeBox, 1, 2);
		this.TableLayoutPanel1.Controls.Add(this.m_productFilterLabel, 0, 6);
		this.TableLayoutPanel1.Controls.Add(this.m_clientFilterLabel, 0, 5);
		this.TableLayoutPanel1.Controls.Add(this.m_versionLabel, 0, 4);
		this.TableLayoutPanel1.Controls.Add(this.m_currencyLabel, 0, 3);
		this.TableLayoutPanel1.Controls.Add(this.m_entityLabel, 0, 0);
		this.TableLayoutPanel1.Controls.Add(this.m_accountLabel, 0, 1);
		this.TableLayoutPanel1.Controls.Add(this.m_periodLabel, 0, 2);
		this.TableLayoutPanel1.Controls.Add(this.m_adjustmentFilterLabel, 0, 7);
		this.TableLayoutPanel1.Controls.Add(this.EntityTreeBox, 1, 0);
		this.TableLayoutPanel1.Controls.Add(this.AccountTreeBox, 1, 1);
		this.TableLayoutPanel1.Controls.Add(this.VersionTreeBox, 1, 4);
		this.TableLayoutPanel1.Controls.Add(this.ClientsTreeviewBox, 1, 5);
		this.TableLayoutPanel1.Location = new System.Drawing.Point(37, 38);
		this.TableLayoutPanel1.Name = "TableLayoutPanel1";
		this.TableLayoutPanel1.RowCount = 9;
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.Size = new System.Drawing.Size(459, 314);
		this.TableLayoutPanel1.TabIndex = 39;
		//
		//AdjustmentsTreeviewBox
		//
		this.AdjustmentsTreeviewBox.BackColor = System.Drawing.Color.White;
		this.AdjustmentsTreeviewBox.BorderColor = System.Drawing.Color.Black;
		this.AdjustmentsTreeviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.AdjustmentsTreeviewBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.AdjustmentsTreeviewBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.AdjustmentsTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.AdjustmentsTreeviewBox.Location = new System.Drawing.Point(142, 248);
		this.AdjustmentsTreeviewBox.Name = "AdjustmentsTreeviewBox";
		this.AdjustmentsTreeviewBox.Size = new System.Drawing.Size(314, 29);
		this.AdjustmentsTreeviewBox.TabIndex = 8;
		this.AdjustmentsTreeviewBox.UseThemeBackColor = false;
		this.AdjustmentsTreeviewBox.UseThemeDropDownArrowColor = true;
		this.AdjustmentsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//ProductsTreeviewBox
		//
		this.ProductsTreeviewBox.BackColor = System.Drawing.Color.White;
		this.ProductsTreeviewBox.BorderColor = System.Drawing.Color.Black;
		this.ProductsTreeviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ProductsTreeviewBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.ProductsTreeviewBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.ProductsTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.ProductsTreeviewBox.Location = new System.Drawing.Point(142, 213);
		this.ProductsTreeviewBox.Name = "ProductsTreeviewBox";
		this.ProductsTreeviewBox.Size = new System.Drawing.Size(314, 29);
		this.ProductsTreeviewBox.TabIndex = 7;
		this.ProductsTreeviewBox.UseThemeBackColor = false;
		this.ProductsTreeviewBox.UseThemeDropDownArrowColor = true;
		this.ProductsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//CategoriesFiltersTreebox
		//
		this.CategoriesFiltersTreebox.BackColor = System.Drawing.Color.White;
		this.CategoriesFiltersTreebox.BorderColor = System.Drawing.Color.Black;
		this.CategoriesFiltersTreebox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.CategoriesFiltersTreebox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.CategoriesFiltersTreebox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.CategoriesFiltersTreebox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.CategoriesFiltersTreebox.Location = new System.Drawing.Point(142, 283);
		this.CategoriesFiltersTreebox.Name = "CategoriesFiltersTreebox";
		this.CategoriesFiltersTreebox.Size = new System.Drawing.Size(314, 29);
		this.CategoriesFiltersTreebox.TabIndex = 9;
		this.CategoriesFiltersTreebox.UseThemeBackColor = false;
		this.CategoriesFiltersTreebox.UseThemeDropDownArrowColor = true;
		this.CategoriesFiltersTreebox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_categoryFilterLabel
		//
		this.m_categoryFilterLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_categoryFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_categoryFilterLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_categoryFilterLabel.Ellipsis = false;
		this.m_categoryFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_categoryFilterLabel.Location = new System.Drawing.Point(3, 283);
		this.m_categoryFilterLabel.Multiline = true;
		this.m_categoryFilterLabel.Name = "m_categoryFilterLabel";
		this.m_categoryFilterLabel.Size = new System.Drawing.Size(133, 29);
		this.m_categoryFilterLabel.TabIndex = 38;
		this.m_categoryFilterLabel.Text = "Categories filter";
		this.m_categoryFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_categoryFilterLabel.UseMnemonics = true;
		this.m_categoryFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//CurrenciesComboBox
		//
		this.CurrenciesComboBox.BackColor = System.Drawing.Color.White;
		this.CurrenciesComboBox.DisplayMember = "";
		this.CurrenciesComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.CurrenciesComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.CurrenciesComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.CurrenciesComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.CurrenciesComboBox.DropDownWidth = 314;
		this.CurrenciesComboBox.Location = new System.Drawing.Point(142, 108);
		this.CurrenciesComboBox.Name = "CurrenciesComboBox";
		this.CurrenciesComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.CurrenciesComboBox.Size = new System.Drawing.Size(314, 29);
		this.CurrenciesComboBox.TabIndex = 4;
		this.CurrenciesComboBox.UseThemeBackColor = false;
		this.CurrenciesComboBox.UseThemeDropDownArrowColor = true;
		this.CurrenciesComboBox.ValueMember = "";
		this.CurrenciesComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.CurrenciesComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//PeriodTreeBox
		//
		this.PeriodTreeBox.BackColor = System.Drawing.Color.White;
		this.PeriodTreeBox.BorderColor = System.Drawing.Color.Black;
		this.PeriodTreeBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PeriodTreeBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.PeriodTreeBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.PeriodTreeBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.PeriodTreeBox.Location = new System.Drawing.Point(142, 73);
		this.PeriodTreeBox.Name = "PeriodTreeBox";
		this.PeriodTreeBox.Size = new System.Drawing.Size(314, 29);
		this.PeriodTreeBox.TabIndex = 3;
		this.PeriodTreeBox.UseThemeBackColor = false;
		this.PeriodTreeBox.UseThemeDropDownArrowColor = true;
		this.PeriodTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_productFilterLabel
		//
		this.m_productFilterLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_productFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_productFilterLabel.Ellipsis = false;
		this.m_productFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_productFilterLabel.Location = new System.Drawing.Point(3, 213);
		this.m_productFilterLabel.Multiline = true;
		this.m_productFilterLabel.Name = "m_productFilterLabel";
		this.m_productFilterLabel.Size = new System.Drawing.Size(123, 24);
		this.m_productFilterLabel.TabIndex = 36;
		this.m_productFilterLabel.Text = "Products filter";
		this.m_productFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_productFilterLabel.UseMnemonics = true;
		this.m_productFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_clientFilterLabel
		//
		this.m_clientFilterLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_clientFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_clientFilterLabel.Ellipsis = false;
		this.m_clientFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_clientFilterLabel.Location = new System.Drawing.Point(3, 178);
		this.m_clientFilterLabel.Multiline = true;
		this.m_clientFilterLabel.Name = "m_clientFilterLabel";
		this.m_clientFilterLabel.Size = new System.Drawing.Size(123, 24);
		this.m_clientFilterLabel.TabIndex = 35;
		this.m_clientFilterLabel.Text = "Clients filter";
		this.m_clientFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_clientFilterLabel.UseMnemonics = true;
		this.m_clientFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_versionLabel
		//
		this.m_versionLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_versionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_versionLabel.Ellipsis = false;
		this.m_versionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_versionLabel.Location = new System.Drawing.Point(3, 143);
		this.m_versionLabel.Multiline = true;
		this.m_versionLabel.Name = "m_versionLabel";
		this.m_versionLabel.Size = new System.Drawing.Size(123, 24);
		this.m_versionLabel.TabIndex = 34;
		this.m_versionLabel.Text = "Version";
		this.m_versionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_versionLabel.UseMnemonics = true;
		this.m_versionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_currencyLabel
		//
		this.m_currencyLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_currencyLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_currencyLabel.Ellipsis = false;
		this.m_currencyLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_currencyLabel.Location = new System.Drawing.Point(3, 108);
		this.m_currencyLabel.Multiline = true;
		this.m_currencyLabel.Name = "m_currencyLabel";
		this.m_currencyLabel.Size = new System.Drawing.Size(123, 24);
		this.m_currencyLabel.TabIndex = 33;
		this.m_currencyLabel.Text = "Currency";
		this.m_currencyLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_currencyLabel.UseMnemonics = true;
		this.m_currencyLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_entityLabel
		//
		this.m_entityLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_entityLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_entityLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_entityLabel.Ellipsis = false;
		this.m_entityLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_entityLabel.Location = new System.Drawing.Point(3, 3);
		this.m_entityLabel.Multiline = true;
		this.m_entityLabel.Name = "m_entityLabel";
		this.m_entityLabel.Size = new System.Drawing.Size(133, 29);
		this.m_entityLabel.TabIndex = 30;
		this.m_entityLabel.Text = "Entity";
		this.m_entityLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_entityLabel.UseMnemonics = true;
		this.m_entityLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_accountLabel
		//
		this.m_accountLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_accountLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_accountLabel.Ellipsis = false;
		this.m_accountLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_accountLabel.Location = new System.Drawing.Point(3, 38);
		this.m_accountLabel.Multiline = true;
		this.m_accountLabel.Name = "m_accountLabel";
		this.m_accountLabel.Size = new System.Drawing.Size(123, 24);
		this.m_accountLabel.TabIndex = 31;
		this.m_accountLabel.Text = "Account";
		this.m_accountLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_accountLabel.UseMnemonics = true;
		this.m_accountLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_periodLabel
		//
		this.m_periodLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_periodLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_periodLabel.Ellipsis = false;
		this.m_periodLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_periodLabel.Location = new System.Drawing.Point(3, 73);
		this.m_periodLabel.Multiline = true;
		this.m_periodLabel.Name = "m_periodLabel";
		this.m_periodLabel.Size = new System.Drawing.Size(123, 24);
		this.m_periodLabel.TabIndex = 32;
		this.m_periodLabel.Text = "Period";
		this.m_periodLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_periodLabel.UseMnemonics = true;
		this.m_periodLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_adjustmentFilterLabel
		//
		this.m_adjustmentFilterLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_adjustmentFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_adjustmentFilterLabel.Ellipsis = false;
		this.m_adjustmentFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_adjustmentFilterLabel.Location = new System.Drawing.Point(3, 248);
		this.m_adjustmentFilterLabel.Multiline = true;
		this.m_adjustmentFilterLabel.Name = "m_adjustmentFilterLabel";
		this.m_adjustmentFilterLabel.Size = new System.Drawing.Size(123, 24);
		this.m_adjustmentFilterLabel.TabIndex = 37;
		this.m_adjustmentFilterLabel.Text = "Adjustments filter";
		this.m_adjustmentFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_adjustmentFilterLabel.UseMnemonics = true;
		this.m_adjustmentFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//EntityTreeBox
		//
		this.EntityTreeBox.BackColor = System.Drawing.Color.White;
		this.EntityTreeBox.BorderColor = System.Drawing.Color.Black;
		this.EntityTreeBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.EntityTreeBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.EntityTreeBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.EntityTreeBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.EntityTreeBox.Location = new System.Drawing.Point(142, 3);
		this.EntityTreeBox.Name = "EntityTreeBox";
		this.EntityTreeBox.Size = new System.Drawing.Size(314, 29);
		this.EntityTreeBox.TabIndex = 1;
		this.EntityTreeBox.UseThemeBackColor = false;
		this.EntityTreeBox.UseThemeDropDownArrowColor = true;
		this.EntityTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//AccountTreeBox
		//
		this.AccountTreeBox.BackColor = System.Drawing.Color.White;
		this.AccountTreeBox.BorderColor = System.Drawing.Color.Black;
		this.AccountTreeBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.AccountTreeBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.AccountTreeBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.AccountTreeBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.AccountTreeBox.Location = new System.Drawing.Point(142, 38);
		this.AccountTreeBox.Name = "AccountTreeBox";
		this.AccountTreeBox.Size = new System.Drawing.Size(314, 29);
		this.AccountTreeBox.TabIndex = 2;
		this.AccountTreeBox.UseThemeBackColor = false;
		this.AccountTreeBox.UseThemeDropDownArrowColor = true;
		this.AccountTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//VersionTreeBox
		//
		this.VersionTreeBox.BackColor = System.Drawing.Color.White;
		this.VersionTreeBox.BorderColor = System.Drawing.Color.Black;
		this.VersionTreeBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.VersionTreeBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.VersionTreeBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.VersionTreeBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.VersionTreeBox.Location = new System.Drawing.Point(142, 143);
		this.VersionTreeBox.Name = "VersionTreeBox";
		this.VersionTreeBox.Size = new System.Drawing.Size(314, 29);
		this.VersionTreeBox.TabIndex = 5;
		this.VersionTreeBox.UseThemeBackColor = false;
		this.VersionTreeBox.UseThemeDropDownArrowColor = true;
		this.VersionTreeBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//ClientsTreeviewBox
		//
		this.ClientsTreeviewBox.BackColor = System.Drawing.Color.White;
		this.ClientsTreeviewBox.BorderColor = System.Drawing.Color.Black;
		this.ClientsTreeviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ClientsTreeviewBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.ClientsTreeviewBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.ClientsTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.ClientsTreeviewBox.Location = new System.Drawing.Point(142, 178);
		this.ClientsTreeviewBox.Name = "ClientsTreeviewBox";
		this.ClientsTreeviewBox.Size = new System.Drawing.Size(314, 29);
		this.ClientsTreeviewBox.TabIndex = 6;
		this.ClientsTreeviewBox.UseThemeBackColor = false;
		this.ClientsTreeviewBox.UseThemeDropDownArrowColor = true;
		this.ClientsTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_versionsTreeviewImageList
		//
		this.m_versionsTreeviewImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("m_versionsTreeviewImageList.ImageStream");
		this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
		this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
		//
		//PPSBI_UI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		this.ClientSize = new System.Drawing.Size(529, 422);
		this.Controls.Add(this.TableLayoutPanel1);
		this.Controls.Add(this.validate_cmd);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "PPSBI_UI";
		this.Text = "Financial BI Excel Function";
		this.TableLayoutPanel1.ResumeLayout(false);
		this.ResumeLayout(false);

	}
  internal VIBlend.WinForms.Controls.vButton validate_cmd;
	internal System.Windows.Forms.ImageList ButtonsIL;
	internal System.Windows.Forms.ImageList categoriesIL;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	internal VIBlend.WinForms.Controls.vLabel m_categoryFilterLabel;
	internal VIBlend.WinForms.Controls.vLabel m_productFilterLabel;
	internal VIBlend.WinForms.Controls.vLabel m_clientFilterLabel;
	internal VIBlend.WinForms.Controls.vLabel m_versionLabel;
	internal VIBlend.WinForms.Controls.vLabel m_currencyLabel;
	internal VIBlend.WinForms.Controls.vLabel m_entityLabel;
	internal VIBlend.WinForms.Controls.vLabel m_accountLabel;
	internal VIBlend.WinForms.Controls.vLabel m_periodLabel;
	internal VIBlend.WinForms.Controls.vLabel m_adjustmentFilterLabel;
	internal VIBlend.WinForms.Controls.vTreeViewBox PeriodTreeBox;
	internal VIBlend.WinForms.Controls.vTreeViewBox EntityTreeBox;
	internal VIBlend.WinForms.Controls.vTreeViewBox AccountTreeBox;
	internal VIBlend.WinForms.Controls.vTreeViewBox VersionTreeBox;
	internal VIBlend.WinForms.Controls.vComboBox CurrenciesComboBox;
	internal VIBlend.WinForms.Controls.vTreeViewBox CategoriesFiltersTreebox;
	internal VIBlend.WinForms.Controls.vTreeViewBox AdjustmentsTreeviewBox;
	internal VIBlend.WinForms.Controls.vTreeViewBox ProductsTreeviewBox;
	internal VIBlend.WinForms.Controls.vTreeViewBox ClientsTreeviewBox;
	internal System.Windows.Forms.ImageList m_versionsTreeviewImageList;
}

}
