using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class ControllingUI_2 : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControllingUI_2));
      this.EntitiesRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.RefreshRightClick = new System.Windows.Forms.ToolStripMenuItem();
      this.PeriodsRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.SelectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.UnselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AdjustmentsRCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.SelectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.UnselectAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
      this.m_DGVsControlTab = new VIBlend.WinForms.Controls.vTabControl();
      this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
      this.MenuImageList = new System.Windows.Forms.ImageList(this.components);
      this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.VersionTB = new VIBlend.WinForms.Controls.vTextBox();
      this.CurrencyTB = new VIBlend.WinForms.Controls.vTextBox();
      this.EntityTB = new VIBlend.WinForms.Controls.vTextBox();
      this.m_currencyLabel = new System.Windows.Forms.Label();
      this.m_versionLabel = new System.Windows.Forms.Label();
      this.m_entityLabel = new System.Windows.Forms.Label();
      this.MainMenu = new System.Windows.Forms.MenuStrip();
      this.ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.DropOnExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.BusinessControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_versionComparisonButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_versionSwitchButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_hideVersionButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_refreshButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ChartBT = new System.Windows.Forms.ToolStripMenuItem();
      this.ExpansionImageList = new System.Windows.Forms.ImageList(this.components);
      this.EntitiesRCMenu.SuspendLayout();
      this.PeriodsRCMenu.SuspendLayout();
      this.AdjustmentsRCMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
      this.SplitContainer1.Panel2.SuspendLayout();
      this.SplitContainer1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
      this.SplitContainer2.Panel1.SuspendLayout();
      this.SplitContainer2.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.MainMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // EntitiesRCMenu
      // 
      this.EntitiesRCMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.EntitiesRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RefreshRightClick});
      this.EntitiesRCMenu.Name = "ContextMenuStripEntitiesNodes";
      this.EntitiesRCMenu.Size = new System.Drawing.Size(166, 30);
      // 
      // RefreshRightClick
      // 
      this.RefreshRightClick.Image = ((System.Drawing.Image)(resources.GetObject("RefreshRightClick.Image")));
      this.RefreshRightClick.Name = "RefreshRightClick";
      this.RefreshRightClick.Size = new System.Drawing.Size(165, 26);
      this.RefreshRightClick.Text = "[CUI.refresh]";
      // 
      // PeriodsRCMenu
      // 
      this.PeriodsRCMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.PeriodsRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAllToolStripMenuItem,
            this.UnselectAllToolStripMenuItem});
      this.PeriodsRCMenu.Name = "periodsRightClickMenu";
      this.PeriodsRCMenu.Size = new System.Drawing.Size(197, 56);
      // 
      // SelectAllToolStripMenuItem
      // 
      this.SelectAllToolStripMenuItem.Name = "SelectAllToolStripMenuItem";
      this.SelectAllToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
      this.SelectAllToolStripMenuItem.Text = "[CUI.select_all]";
      // 
      // UnselectAllToolStripMenuItem
      // 
      this.UnselectAllToolStripMenuItem.Name = "UnselectAllToolStripMenuItem";
      this.UnselectAllToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
      this.UnselectAllToolStripMenuItem.Text = "[CUI.unselect_all]";
      // 
      // AdjustmentsRCMenu
      // 
      this.AdjustmentsRCMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.AdjustmentsRCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAllToolStripMenuItem1,
            this.UnselectAllToolStripMenuItem1});
      this.AdjustmentsRCMenu.Name = "AdjustmentsRCM";
      this.AdjustmentsRCMenu.Size = new System.Drawing.Size(197, 56);
      // 
      // SelectAllToolStripMenuItem1
      // 
      this.SelectAllToolStripMenuItem1.Name = "SelectAllToolStripMenuItem1";
      this.SelectAllToolStripMenuItem1.Size = new System.Drawing.Size(196, 26);
      this.SelectAllToolStripMenuItem1.Text = "[CUI.select_all]";
      // 
      // UnselectAllToolStripMenuItem1
      // 
      this.UnselectAllToolStripMenuItem1.Name = "UnselectAllToolStripMenuItem1";
      this.UnselectAllToolStripMenuItem1.Size = new System.Drawing.Size(196, 26);
      this.UnselectAllToolStripMenuItem1.Text = "[CUI.unselect_all]";
      // 
      // SplitContainer1
      // 
      this.SplitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SplitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(212)))), ((int)(((byte)(221)))));
      this.SplitContainer1.Location = new System.Drawing.Point(0, 4);
      this.SplitContainer1.Margin = new System.Windows.Forms.Padding(4);
      this.SplitContainer1.Name = "SplitContainer1";
      // 
      // SplitContainer1.Panel2
      // 
      this.SplitContainer1.Panel2.Controls.Add(this.m_DGVsControlTab);
      this.SplitContainer1.Size = new System.Drawing.Size(1148, 501);
      this.SplitContainer1.SplitterDistance = 212;
      this.SplitContainer1.SplitterWidth = 5;
      this.SplitContainer1.TabIndex = 8;
      // 
      // m_DGVsControlTab
      // 
      this.m_DGVsControlTab.AllowAnimations = true;
      this.m_DGVsControlTab.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_DGVsControlTab.Location = new System.Drawing.Point(0, 0);
      this.m_DGVsControlTab.Margin = new System.Windows.Forms.Padding(4);
      this.m_DGVsControlTab.Name = "m_DGVsControlTab";
      this.m_DGVsControlTab.Padding = new System.Windows.Forms.Padding(0, 25, 0, 0);
      this.m_DGVsControlTab.Size = new System.Drawing.Size(931, 501);
      this.m_DGVsControlTab.TabAlignment = VIBlend.WinForms.Controls.vTabPageAlignment.Top;
      this.m_DGVsControlTab.TabIndex = 0;
      this.m_DGVsControlTab.TabsAreaBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(212)))), ((int)(((byte)(221)))));
      this.m_DGVsControlTab.TabsInitialOffset = 5;
      this.m_DGVsControlTab.TitleHeight = 25;
      this.m_DGVsControlTab.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // ButtonsImageList
      // 
      this.ButtonsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonsImageList.ImageStream")));
      this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsImageList.Images.SetKeyName(0, "tablet_computer.ico");
      // 
      // MenuImageList
      // 
      this.MenuImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MenuImageList.ImageStream")));
      this.MenuImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.MenuImageList.Images.SetKeyName(0, "elements.ico");
      this.MenuImageList.Images.SetKeyName(1, "favicon(2).ico");
      this.MenuImageList.Images.SetKeyName(2, "element_branch2.ico");
      this.MenuImageList.Images.SetKeyName(3, "tablet_computer.ico");
      // 
      // SplitContainer2
      // 
      this.SplitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.SplitContainer2.BackColor = System.Drawing.Color.Transparent;
      this.SplitContainer2.Location = new System.Drawing.Point(0, 76);
      this.SplitContainer2.Margin = new System.Windows.Forms.Padding(4);
      this.SplitContainer2.Name = "SplitContainer2";
      // 
      // SplitContainer2.Panel1
      // 
      this.SplitContainer2.Panel1.Controls.Add(this.SplitContainer1);
      this.SplitContainer2.Size = new System.Drawing.Size(1448, 508);
      this.SplitContainer2.SplitterDistance = 1152;
      this.SplitContainer2.SplitterWidth = 5;
      this.SplitContainer2.TabIndex = 8;
      // 
      // Panel1
      // 
      this.Panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.Panel1.Controls.Add(this.VersionTB);
      this.Panel1.Controls.Add(this.CurrencyTB);
      this.Panel1.Controls.Add(this.EntityTB);
      this.Panel1.Controls.Add(this.m_currencyLabel);
      this.Panel1.Controls.Add(this.m_versionLabel);
      this.Panel1.Controls.Add(this.m_entityLabel);
      this.Panel1.Controls.Add(this.MainMenu);
      this.Panel1.Dock = System.Windows.Forms.DockStyle.Top;
      this.Panel1.Location = new System.Drawing.Point(0, 0);
      this.Panel1.Margin = new System.Windows.Forms.Padding(4);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(1448, 75);
      this.Panel1.TabIndex = 9;
      // 
      // VersionTB
      // 
      this.VersionTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.VersionTB.BackColor = System.Drawing.Color.White;
      this.VersionTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.VersionTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.VersionTB.DefaultText = "";
      this.VersionTB.Enabled = false;
      this.VersionTB.Location = new System.Drawing.Point(1239, 6);
      this.VersionTB.Margin = new System.Windows.Forms.Padding(4);
      this.VersionTB.MaxLength = 32767;
      this.VersionTB.Name = "VersionTB";
      this.VersionTB.PasswordChar = '\0';
      this.VersionTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.VersionTB.SelectionLength = 0;
      this.VersionTB.SelectionStart = 0;
      this.VersionTB.Size = new System.Drawing.Size(205, 28);
      this.VersionTB.TabIndex = 6;
      this.VersionTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.VersionTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // CurrencyTB
      // 
      this.CurrencyTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.CurrencyTB.BackColor = System.Drawing.Color.White;
      this.CurrencyTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.CurrencyTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.CurrencyTB.DefaultText = "";
      this.CurrencyTB.Enabled = false;
      this.CurrencyTB.Location = new System.Drawing.Point(1067, 6);
      this.CurrencyTB.Margin = new System.Windows.Forms.Padding(4);
      this.CurrencyTB.MaxLength = 32767;
      this.CurrencyTB.Name = "CurrencyTB";
      this.CurrencyTB.PasswordChar = '\0';
      this.CurrencyTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.CurrencyTB.SelectionLength = 0;
      this.CurrencyTB.SelectionStart = 0;
      this.CurrencyTB.Size = new System.Drawing.Size(92, 28);
      this.CurrencyTB.TabIndex = 5;
      this.CurrencyTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.CurrencyTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // EntityTB
      // 
      this.EntityTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.EntityTB.BackColor = System.Drawing.Color.White;
      this.EntityTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.EntityTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.EntityTB.DefaultText = "";
      this.EntityTB.Enabled = false;
      this.EntityTB.Location = new System.Drawing.Point(796, 6);
      this.EntityTB.Margin = new System.Windows.Forms.Padding(4);
      this.EntityTB.MaxLength = 32767;
      this.EntityTB.Name = "EntityTB";
      this.EntityTB.PasswordChar = '\0';
      this.EntityTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.EntityTB.SelectionLength = 0;
      this.EntityTB.SelectionStart = 0;
      this.EntityTB.Size = new System.Drawing.Size(181, 28);
      this.EntityTB.TabIndex = 4;
      this.EntityTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.EntityTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_currencyLabel
      // 
      this.m_currencyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_currencyLabel.AutoSize = true;
      this.m_currencyLabel.Location = new System.Drawing.Point(985, 11);
      this.m_currencyLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.m_currencyLabel.Name = "m_currencyLabel";
      this.m_currencyLabel.Size = new System.Drawing.Size(97, 17);
      this.m_currencyLabel.TabIndex = 3;
      this.m_currencyLabel.Text = "[CUI.currency]";
      // 
      // m_versionLabel
      // 
      this.m_versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_versionLabel.AutoSize = true;
      this.m_versionLabel.Location = new System.Drawing.Point(1167, 11);
      this.m_versionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.m_versionLabel.Name = "m_versionLabel";
      this.m_versionLabel.Size = new System.Drawing.Size(88, 17);
      this.m_versionLabel.TabIndex = 2;
      this.m_versionLabel.Text = "[CUI.version]";
      // 
      // m_entityLabel
      // 
      this.m_entityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_entityLabel.AutoSize = true;
      this.m_entityLabel.Location = new System.Drawing.Point(740, 11);
      this.m_entityLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.m_entityLabel.Name = "m_entityLabel";
      this.m_entityLabel.Size = new System.Drawing.Size(76, 17);
      this.m_entityLabel.TabIndex = 1;
      this.m_entityLabel.Text = "[CUI.entity]";
      // 
      // MainMenu
      // 
      this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
      this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
      this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExcelToolStripMenuItem,
            this.BusinessControlToolStripMenuItem,
            this.m_refreshButton,
            this.ChartBT});
      this.MainMenu.Location = new System.Drawing.Point(0, 0);
      this.MainMenu.Name = "MainMenu";
      this.MainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
      this.MainMenu.ShowItemToolTips = true;
      this.MainMenu.Size = new System.Drawing.Size(551, 60);
      this.MainMenu.TabIndex = 0;
      this.MainMenu.Text = "[CUI.main_menu]";
      // 
      // ExcelToolStripMenuItem
      // 
      this.ExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropOnExcelToolStripMenuItem,
            this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem});
      this.ExcelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ExcelToolStripMenuItem.Image")));
      this.ExcelToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem";
      this.ExcelToolStripMenuItem.Size = new System.Drawing.Size(152, 56);
      this.ExcelToolStripMenuItem.Text = "[CUI.drop_on_excel]";
      this.ExcelToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.ExcelToolStripMenuItem.ToolTipText = "[CUI.drop_on_excel_tooltip]";
      // 
      // DropOnExcelToolStripMenuItem
      // 
      this.DropOnExcelToolStripMenuItem.Name = "DropOnExcelToolStripMenuItem";
      this.DropOnExcelToolStripMenuItem.Size = new System.Drawing.Size(320, 26);
      this.DropOnExcelToolStripMenuItem.Text = "[CUI.drop_on_excel]";
      // 
      // DropOnlyTheVisibleItemsOnExcelToolStripMenuItem
      // 
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Name = "DropOnlyTheVisibleItemsOnExcelToolStripMenuItem";
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Size = new System.Drawing.Size(320, 26);
      this.DropOnlyTheVisibleItemsOnExcelToolStripMenuItem.Text = "Drop only the visible items on Excel";
      // 
      // BusinessControlToolStripMenuItem
      // 
      this.BusinessControlToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_versionComparisonButton,
            this.m_versionSwitchButton,
            this.m_hideVersionButton});
      this.BusinessControlToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("BusinessControlToolStripMenuItem.Image")));
      this.BusinessControlToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.BusinessControlToolStripMenuItem.Name = "BusinessControlToolStripMenuItem";
      this.BusinessControlToolStripMenuItem.Size = new System.Drawing.Size(191, 56);
      this.BusinessControlToolStripMenuItem.Text = "[CUI.performance_review]";
      this.BusinessControlToolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.BusinessControlToolStripMenuItem.ToolTipText = "[CUI.performance_review_tooltip]";
      // 
      // m_versionComparisonButton
      // 
      this.m_versionComparisonButton.Name = "m_versionComparisonButton";
      this.m_versionComparisonButton.Size = new System.Drawing.Size(310, 26);
      this.m_versionComparisonButton.Text = "[CUI.display_versions_comparison]";
      // 
      // m_versionSwitchButton
      // 
      this.m_versionSwitchButton.Name = "m_versionSwitchButton";
      this.m_versionSwitchButton.Size = new System.Drawing.Size(310, 26);
      this.m_versionSwitchButton.Text = "[CUI.switch_versions]";
      // 
      // m_hideVersionButton
      // 
      this.m_hideVersionButton.Name = "m_hideVersionButton";
      this.m_hideVersionButton.Size = new System.Drawing.Size(310, 26);
      this.m_hideVersionButton.Text = "[CUI.take_off_comparison]";
      // 
      // m_refreshButton
      // 
      this.m_refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("m_refreshButton.Image")));
      this.m_refreshButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.m_refreshButton.Name = "m_refreshButton";
      this.m_refreshButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Space)));
      this.m_refreshButton.Size = new System.Drawing.Size(102, 56);
      this.m_refreshButton.Text = "[CUI.refresh]";
      this.m_refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      this.m_refreshButton.ToolTipText = "[CUI.refresh_tooltip]";
      // 
      // ChartBT
      // 
      this.ChartBT.Image = ((System.Drawing.Image)(resources.GetObject("ChartBT.Image")));
      this.ChartBT.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
      this.ChartBT.Name = "ChartBT";
      this.ChartBT.Size = new System.Drawing.Size(96, 56);
      this.ChartBT.Text = "[CUI.charts]";
      this.ChartBT.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
      // 
      // ExpansionImageList
      // 
      this.ExpansionImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ExpansionImageList.ImageStream")));
      this.ExpansionImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ExpansionImageList.Images.SetKeyName(0, "add.ico");
      this.ExpansionImageList.Images.SetKeyName(1, "minus");
      // 
      // ControllingUI_2
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(212)))), ((int)(((byte)(221)))));
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.ClientSize = new System.Drawing.Size(1448, 585);
      this.Controls.Add(this.Panel1);
      this.Controls.Add(this.SplitContainer2);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
      this.Name = "ControllingUI_2";
      this.Text = "[CUI.financials]";
      this.EntitiesRCMenu.ResumeLayout(false);
      this.PeriodsRCMenu.ResumeLayout(false);
      this.AdjustmentsRCMenu.ResumeLayout(false);
      this.SplitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
      this.SplitContainer1.ResumeLayout(false);
      this.SplitContainer2.Panel1.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
      this.SplitContainer2.ResumeLayout(false);
      this.Panel1.ResumeLayout(false);
      this.Panel1.PerformLayout();
      this.MainMenu.ResumeLayout(false);
      this.MainMenu.PerformLayout();
      this.ResumeLayout(false);

	}
	public System.Windows.Forms.ContextMenuStrip EntitiesRCMenu;
	public System.Windows.Forms.ContextMenuStrip PeriodsRCMenu;
	public System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem;
	public System.Windows.Forms.ToolStripMenuItem UnselectAllToolStripMenuItem;
  public System.Windows.Forms.ToolStripMenuItem RefreshRightClick;
	public System.Windows.Forms.ContextMenuStrip AdjustmentsRCMenu;
	public System.Windows.Forms.ToolStripMenuItem SelectAllToolStripMenuItem1;
  public System.Windows.Forms.ToolStripMenuItem UnselectAllToolStripMenuItem1;
	public System.Windows.Forms.SplitContainer SplitContainer1;
	public System.Windows.Forms.ImageList MenuImageList;
	public System.Windows.Forms.SplitContainer SplitContainer2;
	public System.Windows.Forms.ImageList ButtonsImageList;
	public VIBlend.WinForms.Controls.vTabControl m_DGVsControlTab;
	public System.Windows.Forms.Panel Panel1;
	public System.Windows.Forms.MenuStrip MainMenu;
	public System.Windows.Forms.ToolStripMenuItem m_refreshButton;
	public System.Windows.Forms.ToolStripMenuItem BusinessControlToolStripMenuItem;
	public System.Windows.Forms.ToolStripMenuItem m_versionComparisonButton;
	public System.Windows.Forms.ToolStripMenuItem m_versionSwitchButton;
	public System.Windows.Forms.ToolStripMenuItem m_hideVersionButton;
	public System.Windows.Forms.ToolStripMenuItem ExcelToolStripMenuItem;
	public System.Windows.Forms.ToolStripMenuItem DropOnExcelToolStripMenuItem;
	public System.Windows.Forms.Label m_currencyLabel;
	public System.Windows.Forms.Label m_versionLabel;
	public System.Windows.Forms.Label m_entityLabel;
	public VIBlend.WinForms.Controls.vTextBox VersionTB;
	public VIBlend.WinForms.Controls.vTextBox CurrencyTB;
	public VIBlend.WinForms.Controls.vTextBox EntityTB;
  public System.Windows.Forms.ImageList ExpansionImageList;
	public System.Windows.Forms.ToolStripMenuItem ChartBT;
  public System.Windows.Forms.ToolStripMenuItem DropOnlyTheVisibleItemsOnExcelToolStripMenuItem;
}

}
