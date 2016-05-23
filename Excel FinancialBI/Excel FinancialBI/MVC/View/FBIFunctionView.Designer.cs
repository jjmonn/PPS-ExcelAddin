using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class FBIFunctionView : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FBIFunctionView));
      this.ButtonsIL = new System.Windows.Forms.ImageList(this.components);
      this.m_validateButton = new VIBlend.WinForms.Controls.vButton();
      this.categoriesIL = new System.Windows.Forms.ImageList(this.components);
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_periodCB = new VIBlend.WinForms.Controls.vComboBox();
      this.m_adjustmentTree = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_productTree = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_categoriesFilterTree = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_categoryFilterLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_currencyCB = new VIBlend.WinForms.Controls.vComboBox();
      this.m_productFilterLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_clientFilterLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_currencyLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_entityLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_accountLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_adjustmentFilterLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_entityTree = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_accountTree = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_clientTree = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_versionTree = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.m_periodLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_versionLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_versionsTreeviewImageList = new System.Windows.Forms.ImageList(this.components);
      this.m_aggregationLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_aggregationCB = new VIBlend.WinForms.Controls.vComboBox();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // ButtonsIL
      // 
      this.ButtonsIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonsIL.ImageStream")));
      this.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico");
      this.ButtonsIL.Images.SetKeyName(1, "imageres_89.ico");
      this.ButtonsIL.Images.SetKeyName(2, "plus.ico");
      // 
      // m_validateButton
      // 
      this.m_validateButton.AllowAnimations = true;
      this.m_validateButton.BackColor = System.Drawing.Color.Transparent;
      this.m_validateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.m_validateButton.FlatAppearance.BorderSize = 0;
      this.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_validateButton.ImageKey = "submit 1 ok.ico";
      this.m_validateButton.ImageList = this.ButtonsIL;
      this.m_validateButton.Location = new System.Drawing.Point(379, 411);
      this.m_validateButton.Name = "m_validateButton";
      this.m_validateButton.RoundedCornersMask = ((byte)(15));
      this.m_validateButton.Size = new System.Drawing.Size(114, 27);
      this.m_validateButton.TabIndex = 10;
      this.m_validateButton.Text = "Insert formula";
      this.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_validateButton.UseVisualStyleBackColor = true;
      this.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // categoriesIL
      // 
      this.categoriesIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("categoriesIL.ImageStream")));
      this.categoriesIL.TransparentColor = System.Drawing.Color.Transparent;
      this.categoriesIL.Images.SetKeyName(0, "DB Grey.png");
      this.categoriesIL.Images.SetKeyName(1, "icons-blue.png");
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.48327F));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.51673F));
      this.TableLayoutPanel1.Controls.Add(this.m_aggregationCB, 1, 4);
      this.TableLayoutPanel1.Controls.Add(this.m_periodCB, 1, 5);
      this.TableLayoutPanel1.Controls.Add(this.m_adjustmentTree, 1, 8);
      this.TableLayoutPanel1.Controls.Add(this.m_productTree, 1, 7);
      this.TableLayoutPanel1.Controls.Add(this.m_categoriesFilterTree, 1, 9);
      this.TableLayoutPanel1.Controls.Add(this.m_categoryFilterLabel, 0, 9);
      this.TableLayoutPanel1.Controls.Add(this.m_currencyCB, 1, 3);
      this.TableLayoutPanel1.Controls.Add(this.m_productFilterLabel, 0, 7);
      this.TableLayoutPanel1.Controls.Add(this.m_clientFilterLabel, 0, 6);
      this.TableLayoutPanel1.Controls.Add(this.m_currencyLabel, 0, 3);
      this.TableLayoutPanel1.Controls.Add(this.m_entityLabel, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_accountLabel, 0, 1);
      this.TableLayoutPanel1.Controls.Add(this.m_adjustmentFilterLabel, 0, 8);
      this.TableLayoutPanel1.Controls.Add(this.m_entityTree, 1, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_accountTree, 1, 1);
      this.TableLayoutPanel1.Controls.Add(this.m_clientTree, 1, 6);
      this.TableLayoutPanel1.Controls.Add(this.m_versionTree, 1, 2);
      this.TableLayoutPanel1.Controls.Add(this.m_periodLabel, 0, 5);
      this.TableLayoutPanel1.Controls.Add(this.m_versionLabel, 0, 2);
      this.TableLayoutPanel1.Controls.Add(this.m_aggregationLabel, 0, 4);
      this.TableLayoutPanel1.Location = new System.Drawing.Point(37, 38);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 10;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(459, 351);
      this.TableLayoutPanel1.TabIndex = 39;
      // 
      // m_periodCB
      // 
      this.m_periodCB.BackColor = System.Drawing.Color.White;
      this.m_periodCB.DisplayMember = "";
      this.m_periodCB.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_periodCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_periodCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_periodCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_periodCB.DropDownWidth = 314;
      this.m_periodCB.Location = new System.Drawing.Point(142, 178);
      this.m_periodCB.Name = "m_periodCB";
      this.m_periodCB.RoundedCornersMaskListItem = ((byte)(15));
      this.m_periodCB.Size = new System.Drawing.Size(314, 29);
      this.m_periodCB.TabIndex = 41;
      this.m_periodCB.UseThemeBackColor = false;
      this.m_periodCB.UseThemeDropDownArrowColor = true;
      this.m_periodCB.ValueMember = "";
      this.m_periodCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_periodCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_adjustmentTree
      // 
      this.m_adjustmentTree.BackColor = System.Drawing.Color.White;
      this.m_adjustmentTree.BorderColor = System.Drawing.Color.Black;
      this.m_adjustmentTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_adjustmentTree.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_adjustmentTree.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_adjustmentTree.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_adjustmentTree.Location = new System.Drawing.Point(142, 283);
      this.m_adjustmentTree.Name = "m_adjustmentTree";
      this.m_adjustmentTree.Size = new System.Drawing.Size(314, 29);
      this.m_adjustmentTree.TabIndex = 8;
      this.m_adjustmentTree.UseThemeBackColor = false;
      this.m_adjustmentTree.UseThemeDropDownArrowColor = true;
      this.m_adjustmentTree.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_productTree
      // 
      this.m_productTree.BackColor = System.Drawing.Color.White;
      this.m_productTree.BorderColor = System.Drawing.Color.Black;
      this.m_productTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_productTree.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_productTree.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_productTree.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_productTree.Location = new System.Drawing.Point(142, 248);
      this.m_productTree.Name = "m_productTree";
      this.m_productTree.Size = new System.Drawing.Size(314, 29);
      this.m_productTree.TabIndex = 7;
      this.m_productTree.UseThemeBackColor = false;
      this.m_productTree.UseThemeDropDownArrowColor = true;
      this.m_productTree.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_categoriesFilterTree
      // 
      this.m_categoriesFilterTree.BackColor = System.Drawing.Color.White;
      this.m_categoriesFilterTree.BorderColor = System.Drawing.Color.Black;
      this.m_categoriesFilterTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_categoriesFilterTree.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_categoriesFilterTree.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_categoriesFilterTree.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_categoriesFilterTree.Location = new System.Drawing.Point(142, 318);
      this.m_categoriesFilterTree.Name = "m_categoriesFilterTree";
      this.m_categoriesFilterTree.Size = new System.Drawing.Size(314, 30);
      this.m_categoriesFilterTree.TabIndex = 9;
      this.m_categoriesFilterTree.UseThemeBackColor = false;
      this.m_categoriesFilterTree.UseThemeDropDownArrowColor = true;
      this.m_categoriesFilterTree.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_categoryFilterLabel
      // 
      this.m_categoryFilterLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_categoryFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_categoryFilterLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_categoryFilterLabel.Ellipsis = false;
      this.m_categoryFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_categoryFilterLabel.Location = new System.Drawing.Point(3, 318);
      this.m_categoryFilterLabel.Multiline = true;
      this.m_categoryFilterLabel.Name = "m_categoryFilterLabel";
      this.m_categoryFilterLabel.Size = new System.Drawing.Size(133, 30);
      this.m_categoryFilterLabel.TabIndex = 38;
      this.m_categoryFilterLabel.Text = "Categories filter";
      this.m_categoryFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_categoryFilterLabel.UseMnemonics = true;
      this.m_categoryFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_currencyCB
      // 
      this.m_currencyCB.BackColor = System.Drawing.Color.White;
      this.m_currencyCB.DisplayMember = "";
      this.m_currencyCB.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_currencyCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_currencyCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_currencyCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_currencyCB.DropDownWidth = 314;
      this.m_currencyCB.Location = new System.Drawing.Point(142, 108);
      this.m_currencyCB.Name = "m_currencyCB";
      this.m_currencyCB.RoundedCornersMaskListItem = ((byte)(15));
      this.m_currencyCB.Size = new System.Drawing.Size(314, 29);
      this.m_currencyCB.TabIndex = 4;
      this.m_currencyCB.UseThemeBackColor = false;
      this.m_currencyCB.UseThemeDropDownArrowColor = true;
      this.m_currencyCB.ValueMember = "";
      this.m_currencyCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_currencyCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_productFilterLabel
      // 
      this.m_productFilterLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_productFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_productFilterLabel.Ellipsis = false;
      this.m_productFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_productFilterLabel.Location = new System.Drawing.Point(3, 248);
      this.m_productFilterLabel.Multiline = true;
      this.m_productFilterLabel.Name = "m_productFilterLabel";
      this.m_productFilterLabel.Size = new System.Drawing.Size(123, 24);
      this.m_productFilterLabel.TabIndex = 36;
      this.m_productFilterLabel.Text = "Products filter";
      this.m_productFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_productFilterLabel.UseMnemonics = true;
      this.m_productFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_clientFilterLabel
      // 
      this.m_clientFilterLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_clientFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_clientFilterLabel.Ellipsis = false;
      this.m_clientFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_clientFilterLabel.Location = new System.Drawing.Point(3, 213);
      this.m_clientFilterLabel.Multiline = true;
      this.m_clientFilterLabel.Name = "m_clientFilterLabel";
      this.m_clientFilterLabel.Size = new System.Drawing.Size(123, 24);
      this.m_clientFilterLabel.TabIndex = 35;
      this.m_clientFilterLabel.Text = "Clients filter";
      this.m_clientFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_clientFilterLabel.UseMnemonics = true;
      this.m_clientFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_currencyLabel
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
      // m_entityLabel
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
      // m_accountLabel
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
      // m_adjustmentFilterLabel
      // 
      this.m_adjustmentFilterLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_adjustmentFilterLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_adjustmentFilterLabel.Ellipsis = false;
      this.m_adjustmentFilterLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_adjustmentFilterLabel.Location = new System.Drawing.Point(3, 283);
      this.m_adjustmentFilterLabel.Multiline = true;
      this.m_adjustmentFilterLabel.Name = "m_adjustmentFilterLabel";
      this.m_adjustmentFilterLabel.Size = new System.Drawing.Size(123, 24);
      this.m_adjustmentFilterLabel.TabIndex = 37;
      this.m_adjustmentFilterLabel.Text = "Adjustments filter";
      this.m_adjustmentFilterLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_adjustmentFilterLabel.UseMnemonics = true;
      this.m_adjustmentFilterLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_entityTree
      // 
      this.m_entityTree.BackColor = System.Drawing.Color.White;
      this.m_entityTree.BorderColor = System.Drawing.Color.Black;
      this.m_entityTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_entityTree.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_entityTree.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_entityTree.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_entityTree.Location = new System.Drawing.Point(142, 3);
      this.m_entityTree.Name = "m_entityTree";
      this.m_entityTree.Size = new System.Drawing.Size(314, 29);
      this.m_entityTree.TabIndex = 1;
      this.m_entityTree.UseThemeBackColor = false;
      this.m_entityTree.UseThemeDropDownArrowColor = true;
      this.m_entityTree.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_accountTree
      // 
      this.m_accountTree.BackColor = System.Drawing.Color.White;
      this.m_accountTree.BorderColor = System.Drawing.Color.Black;
      this.m_accountTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_accountTree.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_accountTree.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_accountTree.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_accountTree.Location = new System.Drawing.Point(142, 38);
      this.m_accountTree.Name = "m_accountTree";
      this.m_accountTree.Size = new System.Drawing.Size(314, 29);
      this.m_accountTree.TabIndex = 2;
      this.m_accountTree.UseThemeBackColor = false;
      this.m_accountTree.UseThemeDropDownArrowColor = true;
      this.m_accountTree.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_clientTree
      // 
      this.m_clientTree.BackColor = System.Drawing.Color.White;
      this.m_clientTree.BorderColor = System.Drawing.Color.Black;
      this.m_clientTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_clientTree.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_clientTree.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_clientTree.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_clientTree.Location = new System.Drawing.Point(142, 213);
      this.m_clientTree.Name = "m_clientTree";
      this.m_clientTree.Size = new System.Drawing.Size(314, 29);
      this.m_clientTree.TabIndex = 6;
      this.m_clientTree.UseThemeBackColor = false;
      this.m_clientTree.UseThemeDropDownArrowColor = true;
      this.m_clientTree.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_versionTree
      // 
      this.m_versionTree.BackColor = System.Drawing.Color.White;
      this.m_versionTree.BorderColor = System.Drawing.Color.Black;
      this.m_versionTree.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_versionTree.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_versionTree.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_versionTree.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_versionTree.Location = new System.Drawing.Point(142, 73);
      this.m_versionTree.Name = "m_versionTree";
      this.m_versionTree.Size = new System.Drawing.Size(314, 29);
      this.m_versionTree.TabIndex = 5;
      this.m_versionTree.UseThemeBackColor = false;
      this.m_versionTree.UseThemeDropDownArrowColor = true;
      this.m_versionTree.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_periodLabel
      // 
      this.m_periodLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_periodLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_periodLabel.Ellipsis = false;
      this.m_periodLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_periodLabel.Location = new System.Drawing.Point(3, 178);
      this.m_periodLabel.Multiline = true;
      this.m_periodLabel.Name = "m_periodLabel";
      this.m_periodLabel.Size = new System.Drawing.Size(123, 24);
      this.m_periodLabel.TabIndex = 32;
      this.m_periodLabel.Text = "Period";
      this.m_periodLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_periodLabel.UseMnemonics = true;
      this.m_periodLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_versionLabel
      // 
      this.m_versionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_versionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_versionLabel.Ellipsis = false;
      this.m_versionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_versionLabel.Location = new System.Drawing.Point(3, 73);
      this.m_versionLabel.Multiline = true;
      this.m_versionLabel.Name = "m_versionLabel";
      this.m_versionLabel.Size = new System.Drawing.Size(123, 24);
      this.m_versionLabel.TabIndex = 40;
      this.m_versionLabel.Text = "Version";
      this.m_versionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_versionLabel.UseMnemonics = true;
      this.m_versionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_versionsTreeviewImageList
      // 
      this.m_versionsTreeviewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_versionsTreeviewImageList.ImageStream")));
      this.m_versionsTreeviewImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.m_versionsTreeviewImageList.Images.SetKeyName(0, "cloud_dark.ico");
      this.m_versionsTreeviewImageList.Images.SetKeyName(1, "favicon(81).ico");
      // 
      // m_aggregationLabel
      // 
      this.m_aggregationLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_aggregationLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_aggregationLabel.Ellipsis = false;
      this.m_aggregationLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_aggregationLabel.Location = new System.Drawing.Point(3, 143);
      this.m_aggregationLabel.Multiline = true;
      this.m_aggregationLabel.Name = "m_aggregationLabel";
      this.m_aggregationLabel.Size = new System.Drawing.Size(80, 25);
      this.m_aggregationLabel.TabIndex = 42;
      this.m_aggregationLabel.Text = "Aggregation";
      this.m_aggregationLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_aggregationLabel.UseMnemonics = true;
      this.m_aggregationLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_aggregationCB
      // 
      this.m_aggregationCB.BackColor = System.Drawing.Color.White;
      this.m_aggregationCB.DisplayMember = "";
      this.m_aggregationCB.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_aggregationCB.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_aggregationCB.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_aggregationCB.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_aggregationCB.DropDownWidth = 314;
      this.m_aggregationCB.Location = new System.Drawing.Point(142, 143);
      this.m_aggregationCB.Name = "m_aggregationCB";
      this.m_aggregationCB.RoundedCornersMaskListItem = ((byte)(15));
      this.m_aggregationCB.Size = new System.Drawing.Size(314, 29);
      this.m_aggregationCB.TabIndex = 43;
      this.m_aggregationCB.UseThemeBackColor = false;
      this.m_aggregationCB.UseThemeDropDownArrowColor = true;
      this.m_aggregationCB.ValueMember = "";
      this.m_aggregationCB.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_aggregationCB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // FBIFunctionView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.ClientSize = new System.Drawing.Size(529, 450);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Controls.Add(this.m_validateButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FBIFunctionView";
      this.Text = "Financial BI Excel Function";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

	}
  public VIBlend.WinForms.Controls.vButton m_validateButton;
	public System.Windows.Forms.ImageList ButtonsIL;
	public System.Windows.Forms.ImageList categoriesIL;
	public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	public VIBlend.WinForms.Controls.vLabel m_categoryFilterLabel;
	public VIBlend.WinForms.Controls.vLabel m_productFilterLabel;
  public VIBlend.WinForms.Controls.vLabel m_clientFilterLabel;
	public VIBlend.WinForms.Controls.vLabel m_currencyLabel;
	public VIBlend.WinForms.Controls.vLabel m_entityLabel;
	public VIBlend.WinForms.Controls.vLabel m_accountLabel;
	public VIBlend.WinForms.Controls.vLabel m_periodLabel;
  public VIBlend.WinForms.Controls.vLabel m_adjustmentFilterLabel;
	public VIBlend.WinForms.Controls.vTreeViewBox m_entityTree;
	public VIBlend.WinForms.Controls.vTreeViewBox m_accountTree;
	public VIBlend.WinForms.Controls.vTreeViewBox m_versionTree;
	public VIBlend.WinForms.Controls.vComboBox m_currencyCB;
	public VIBlend.WinForms.Controls.vTreeViewBox m_categoriesFilterTree;
	public VIBlend.WinForms.Controls.vTreeViewBox m_adjustmentTree;
	public VIBlend.WinForms.Controls.vTreeViewBox m_productTree;
	public VIBlend.WinForms.Controls.vTreeViewBox m_clientTree;
  public System.Windows.Forms.ImageList m_versionsTreeviewImageList;
  public VIBlend.WinForms.Controls.vLabel m_versionLabel;
  public VIBlend.WinForms.Controls.vComboBox m_periodCB;
  public VIBlend.WinForms.Controls.vComboBox m_aggregationCB;
  private VIBlend.WinForms.Controls.vLabel m_aggregationLabel;
}

}
