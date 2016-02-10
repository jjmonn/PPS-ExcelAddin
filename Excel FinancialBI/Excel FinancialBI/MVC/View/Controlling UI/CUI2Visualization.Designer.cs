using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class CUI2Visualization : System.Windows.Forms.UserControl
{

	//UserControl overrides dispose to clean up the component list.
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
		System.Windows.Forms.DataVisualization.Charting.ChartArea ChartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend Legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.ChartArea ChartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend Legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.ChartArea ChartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend Legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.Windows.Forms.DataVisualization.Charting.ChartArea ChartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
		System.Windows.Forms.DataVisualization.Charting.Legend Legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CUI2Visualization));
		this.VSplitContainer1 = new VIBlend.WinForms.Controls.vSplitContainer();
		this.VSplitContainer3 = new VIBlend.WinForms.Controls.vSplitContainer();
		this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.Chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.VSplitContainer2 = new VIBlend.WinForms.Controls.vSplitContainer();
		this.Chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.Chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
		this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.Panel1 = new System.Windows.Forms.Panel();
		this.m_refreshButton = new VIBlend.WinForms.Controls.vButton();
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.VersionTB = new VIBlend.WinForms.Controls.vTextBox();
		this.CurrencyTB = new VIBlend.WinForms.Controls.vTextBox();
		this.EntityTB = new VIBlend.WinForms.Controls.vTextBox();
		this.m_currencyLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_versionLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_entityLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_chartsRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.m_editChartButton = new System.Windows.Forms.ToolStripMenuItem();
		this.m_dropChartOnExcelButton = new System.Windows.Forms.ToolStripMenuItem();
		this.VSplitContainer1.Panel1.SuspendLayout();
		this.VSplitContainer1.Panel2.SuspendLayout();
		this.VSplitContainer1.SuspendLayout();
		this.VSplitContainer3.Panel1.SuspendLayout();
		this.VSplitContainer3.Panel2.SuspendLayout();
		this.VSplitContainer3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Chart1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Chart3).BeginInit();
		this.VSplitContainer2.Panel1.SuspendLayout();
		this.VSplitContainer2.Panel2.SuspendLayout();
		this.VSplitContainer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Chart2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.Chart4).BeginInit();
		this.TableLayoutPanel1.SuspendLayout();
		this.Panel1.SuspendLayout();
		this.m_chartsRightClickMenu.SuspendLayout();
		this.SuspendLayout();
		//
		//VSplitContainer1
		//
		this.VSplitContainer1.AllowAnimations = true;
		this.VSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.VSplitContainer1.Location = new System.Drawing.Point(3, 28);
		this.VSplitContainer1.Name = "VSplitContainer1";
		this.VSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		//
		//VSplitContainer1.Panel1
		//
		this.VSplitContainer1.Panel1.BackColor = System.Drawing.Color.White;
		this.VSplitContainer1.Panel1.BorderColor = System.Drawing.Color.Silver;
		this.VSplitContainer1.Panel1.Controls.Add(this.VSplitContainer3);
		this.VSplitContainer1.Panel1.Location = new System.Drawing.Point(0, 0);
		this.VSplitContainer1.Panel1.Name = "Panel1";
		this.VSplitContainer1.Panel1.Size = new System.Drawing.Size(346, 388);
		this.VSplitContainer1.Panel1.TabIndex = 1;
		//
		//VSplitContainer1.Panel2
		//
		this.VSplitContainer1.Panel2.BackColor = System.Drawing.Color.White;
		this.VSplitContainer1.Panel2.BorderColor = System.Drawing.Color.Silver;
		this.VSplitContainer1.Panel2.Controls.Add(this.VSplitContainer2);
		this.VSplitContainer1.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.VSplitContainer1.Panel2.Location = new System.Drawing.Point(351, 0);
		this.VSplitContainer1.Panel2.Name = "Panel2";
		this.VSplitContainer1.Panel2.Size = new System.Drawing.Size(347, 388);
		this.VSplitContainer1.Panel2.TabIndex = 2;
		this.VSplitContainer1.Size = new System.Drawing.Size(698, 388);
		this.VSplitContainer1.SplitterSize = 5;
		this.VSplitContainer1.StyleKey = "Splitter";
		this.VSplitContainer1.TabIndex = 0;
		this.VSplitContainer1.Text = "VSplitContainer1";
		this.VSplitContainer1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER;
		//
		//VSplitContainer3
		//
		this.VSplitContainer3.AllowAnimations = true;
		this.VSplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.VSplitContainer3.Location = new System.Drawing.Point(0, 0);
		this.VSplitContainer3.Name = "VSplitContainer3";
		this.VSplitContainer3.Orientation = System.Windows.Forms.Orientation.Vertical;
		//
		//VSplitContainer3.Panel1
		//
		this.VSplitContainer3.Panel1.BackColor = System.Drawing.Color.White;
		this.VSplitContainer3.Panel1.BorderColor = System.Drawing.Color.Silver;
		this.VSplitContainer3.Panel1.Controls.Add(this.Chart1);
		this.VSplitContainer3.Panel1.Location = new System.Drawing.Point(0, 0);
		this.VSplitContainer3.Panel1.Name = "Panel1";
		this.VSplitContainer3.Panel1.Size = new System.Drawing.Size(346, 191);
		this.VSplitContainer3.Panel1.TabIndex = 1;
		//
		//VSplitContainer3.Panel2
		//
		this.VSplitContainer3.Panel2.BackColor = System.Drawing.Color.White;
		this.VSplitContainer3.Panel2.BorderColor = System.Drawing.Color.Silver;
		this.VSplitContainer3.Panel2.Controls.Add(this.Chart3);
		this.VSplitContainer3.Panel2.Location = new System.Drawing.Point(0, 196);
		this.VSplitContainer3.Panel2.Name = "Panel2";
		this.VSplitContainer3.Panel2.Size = new System.Drawing.Size(346, 192);
		this.VSplitContainer3.Panel2.TabIndex = 2;
		this.VSplitContainer3.Size = new System.Drawing.Size(346, 388);
		this.VSplitContainer3.SplitterSize = 5;
		this.VSplitContainer3.StyleKey = "Splitter";
		this.VSplitContainer3.TabIndex = 0;
		this.VSplitContainer3.Text = "VSplitContainer3";
		this.VSplitContainer3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER;
		//
		//Chart1
		//
		ChartArea1.Name = "ChartArea1";
		this.Chart1.ChartAreas.Add(ChartArea1);
		this.Chart1.Dock = System.Windows.Forms.DockStyle.Fill;
		Legend1.Name = "Legend1";
		this.Chart1.Legends.Add(Legend1);
		this.Chart1.Location = new System.Drawing.Point(0, 0);
		this.Chart1.Name = "Chart1";
		this.Chart1.Size = new System.Drawing.Size(346, 191);
		this.Chart1.TabIndex = 0;
		this.Chart1.Text = "Chart1";
		//
		//Chart3
		//
		ChartArea2.Name = "ChartArea1";
		this.Chart3.ChartAreas.Add(ChartArea2);
		this.Chart3.Dock = System.Windows.Forms.DockStyle.Fill;
		Legend2.Name = "Legend1";
		this.Chart3.Legends.Add(Legend2);
		this.Chart3.Location = new System.Drawing.Point(0, 0);
		this.Chart3.Name = "Chart3";
		this.Chart3.Size = new System.Drawing.Size(346, 192);
		this.Chart3.TabIndex = 1;
		this.Chart3.Text = "Chart3";
		//
		//VSplitContainer2
		//
		this.VSplitContainer2.AllowAnimations = true;
		this.VSplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.VSplitContainer2.Location = new System.Drawing.Point(0, 0);
		this.VSplitContainer2.Name = "VSplitContainer2";
		this.VSplitContainer2.Orientation = System.Windows.Forms.Orientation.Vertical;
		//
		//VSplitContainer2.Panel1
		//
		this.VSplitContainer2.Panel1.BackColor = System.Drawing.Color.White;
		this.VSplitContainer2.Panel1.BorderColor = System.Drawing.Color.Silver;
		this.VSplitContainer2.Panel1.Controls.Add(this.Chart2);
		this.VSplitContainer2.Panel1.Location = new System.Drawing.Point(0, 0);
		this.VSplitContainer2.Panel1.Name = "Panel1";
		this.VSplitContainer2.Panel1.Size = new System.Drawing.Size(347, 191);
		this.VSplitContainer2.Panel1.TabIndex = 1;
		//
		//VSplitContainer2.Panel2
		//
		this.VSplitContainer2.Panel2.BackColor = System.Drawing.Color.White;
		this.VSplitContainer2.Panel2.BorderColor = System.Drawing.Color.Silver;
		this.VSplitContainer2.Panel2.Controls.Add(this.Chart4);
		this.VSplitContainer2.Panel2.Location = new System.Drawing.Point(0, 196);
		this.VSplitContainer2.Panel2.Name = "Panel2";
		this.VSplitContainer2.Panel2.Size = new System.Drawing.Size(347, 192);
		this.VSplitContainer2.Panel2.TabIndex = 2;
		this.VSplitContainer2.Size = new System.Drawing.Size(347, 388);
		this.VSplitContainer2.SplitterSize = 5;
		this.VSplitContainer2.StyleKey = "Splitter";
		this.VSplitContainer2.TabIndex = 0;
		this.VSplitContainer2.Text = "VSplitContainer2";
		this.VSplitContainer2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER;
		//
		//Chart2
		//
		ChartArea3.Name = "ChartArea1";
		this.Chart2.ChartAreas.Add(ChartArea3);
		this.Chart2.Dock = System.Windows.Forms.DockStyle.Fill;
		Legend3.Name = "Legend1";
		this.Chart2.Legends.Add(Legend3);
		this.Chart2.Location = new System.Drawing.Point(0, 0);
		this.Chart2.Name = "Chart2";
		this.Chart2.Size = new System.Drawing.Size(347, 191);
		this.Chart2.TabIndex = 1;
		this.Chart2.Text = "Chart2";
		//
		//Chart4
		//
		ChartArea4.Name = "ChartArea1";
		this.Chart4.ChartAreas.Add(ChartArea4);
		this.Chart4.Dock = System.Windows.Forms.DockStyle.Fill;
		Legend4.Name = "Legend1";
		this.Chart4.Legends.Add(Legend4);
		this.Chart4.Location = new System.Drawing.Point(0, 0);
		this.Chart4.Name = "Chart4";
		this.Chart4.Size = new System.Drawing.Size(347, 192);
		this.Chart4.TabIndex = 1;
		this.Chart4.Text = "Chart4";
		//
		//TableLayoutPanel1
		//
		this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.TableLayoutPanel1.ColumnCount = 1;
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.Controls.Add(this.VSplitContainer1, 0, 1);
		this.TableLayoutPanel1.Controls.Add(this.Panel1, 0, 0);
		this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.TableLayoutPanel1.Name = "TableLayoutPanel1";
		this.TableLayoutPanel1.RowCount = 2;
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel1.Size = new System.Drawing.Size(704, 419);
		this.TableLayoutPanel1.TabIndex = 1;
		//
		//Panel1
		//
		this.Panel1.Controls.Add(this.m_refreshButton);
		this.Panel1.Controls.Add(this.VersionTB);
		this.Panel1.Controls.Add(this.CurrencyTB);
		this.Panel1.Controls.Add(this.EntityTB);
		this.Panel1.Controls.Add(this.m_currencyLabel);
		this.Panel1.Controls.Add(this.m_versionLabel);
		this.Panel1.Controls.Add(this.m_entityLabel);
		this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Panel1.Location = new System.Drawing.Point(0, 0);
		this.Panel1.Margin = new System.Windows.Forms.Padding(0);
		this.Panel1.Name = "Panel1";
		this.Panel1.Size = new System.Drawing.Size(704, 25);
		this.Panel1.TabIndex = 1;
		//
		//m_refreshButton
		//
		this.m_refreshButton.AllowAnimations = true;
		this.m_refreshButton.BackColor = System.Drawing.Color.Transparent;
		this.m_refreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.m_refreshButton.ImageKey = "refresh classic green.ico";
		this.m_refreshButton.ImageList = this.ImageList1;
		this.m_refreshButton.Location = new System.Drawing.Point(3, 0);
		this.m_refreshButton.Name = "m_refreshButton";
		this.m_refreshButton.RoundedCornersMask = Convert.ToByte(15);
		this.m_refreshButton.Size = new System.Drawing.Size(73, 25);
		this.m_refreshButton.TabIndex = 13;
		this.m_refreshButton.Text = "Refresh";
		this.m_refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_refreshButton.UseVisualStyleBackColor = false;
		this.m_refreshButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//ImageList1
		//
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "Export classic green bigger.ico");
		this.ImageList1.Images.SetKeyName(1, "refresh classic green.ico");
		//
		//VersionTB
		//
		this.VersionTB.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.VersionTB.BackColor = System.Drawing.Color.White;
		this.VersionTB.BoundsOffset = new System.Drawing.Size(1, 1);
		this.VersionTB.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.VersionTB.DefaultText = "Empty...";
		this.VersionTB.Enabled = false;
		this.VersionTB.Location = new System.Drawing.Point(546, 1);
		this.VersionTB.MaxLength = 32767;
		this.VersionTB.Name = "VersionTB";
		this.VersionTB.PasswordChar = '\0';
		this.VersionTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.VersionTB.SelectionLength = 0;
		this.VersionTB.SelectionStart = 0;
		this.VersionTB.Size = new System.Drawing.Size(154, 23);
		this.VersionTB.TabIndex = 12;
		this.VersionTB.Text = " ";
		this.VersionTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.VersionTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//CurrencyTB
		//
		this.CurrencyTB.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.CurrencyTB.BackColor = System.Drawing.Color.White;
		this.CurrencyTB.BoundsOffset = new System.Drawing.Size(1, 1);
		this.CurrencyTB.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.CurrencyTB.DefaultText = "Empty...";
		this.CurrencyTB.Enabled = false;
		this.CurrencyTB.Location = new System.Drawing.Point(417, 1);
		this.CurrencyTB.MaxLength = 32767;
		this.CurrencyTB.Name = "CurrencyTB";
		this.CurrencyTB.PasswordChar = '\0';
		this.CurrencyTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.CurrencyTB.SelectionLength = 0;
		this.CurrencyTB.SelectionStart = 0;
		this.CurrencyTB.Size = new System.Drawing.Size(69, 23);
		this.CurrencyTB.TabIndex = 11;
		this.CurrencyTB.Text = " ";
		this.CurrencyTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.CurrencyTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//EntityTB
		//
		this.EntityTB.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.EntityTB.BackColor = System.Drawing.Color.White;
		this.EntityTB.BoundsOffset = new System.Drawing.Size(1, 1);
		this.EntityTB.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.EntityTB.DefaultText = "Empty...";
		this.EntityTB.Enabled = false;
		this.EntityTB.Location = new System.Drawing.Point(214, 1);
		this.EntityTB.MaxLength = 32767;
		this.EntityTB.Name = "EntityTB";
		this.EntityTB.PasswordChar = '\0';
		this.EntityTB.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.EntityTB.SelectionLength = 0;
		this.EntityTB.SelectionStart = 0;
		this.EntityTB.Size = new System.Drawing.Size(136, 23);
		this.EntityTB.TabIndex = 10;
		this.EntityTB.Text = " ";
		this.EntityTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.EntityTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_currencyLabel
		//
		this.m_currencyLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_currencyLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_currencyLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_currencyLabel.Ellipsis = false;
		this.m_currencyLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_currencyLabel.Location = new System.Drawing.Point(356, 5);
		this.m_currencyLabel.Multiline = true;
		this.m_currencyLabel.Name = "m_currencyLabel";
		this.m_currencyLabel.Size = new System.Drawing.Size(55, 16);
		this.m_currencyLabel.TabIndex = 9;
		this.m_currencyLabel.Text = "Currency";
		this.m_currencyLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_currencyLabel.UseMnemonics = true;
		this.m_currencyLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_versionLabel
		//
		this.m_versionLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_versionLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_versionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_versionLabel.Ellipsis = false;
		this.m_versionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_versionLabel.Location = new System.Drawing.Point(492, 5);
		this.m_versionLabel.Multiline = true;
		this.m_versionLabel.Name = "m_versionLabel";
		this.m_versionLabel.Size = new System.Drawing.Size(58, 16);
		this.m_versionLabel.TabIndex = 8;
		this.m_versionLabel.Text = "Version";
		this.m_versionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_versionLabel.UseMnemonics = true;
		this.m_versionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_entityLabel
		//
		this.m_entityLabel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_entityLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_entityLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_entityLabel.Ellipsis = false;
		this.m_entityLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_entityLabel.Location = new System.Drawing.Point(172, 5);
		this.m_entityLabel.Multiline = true;
		this.m_entityLabel.Name = "m_entityLabel";
		this.m_entityLabel.Size = new System.Drawing.Size(33, 13);
		this.m_entityLabel.TabIndex = 7;
		this.m_entityLabel.Text = "Entity";
		this.m_entityLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_entityLabel.UseMnemonics = true;
		this.m_entityLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_chartsRightClickMenu
		//
		this.m_chartsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.m_editChartButton,
			this.m_dropChartOnExcelButton
		});
		this.m_chartsRightClickMenu.Name = "m_chartsRightClickMenu";
		this.m_chartsRightClickMenu.Size = new System.Drawing.Size(147, 48);
		//
		//m_editChartButton
		//
		this.m_editChartButton.Image = global::FBI.Properties.Resources.chart_line;
		this.m_editChartButton.Name = "m_editChartButton";
		this.m_editChartButton.Size = new System.Drawing.Size(146, 22);
		this.m_editChartButton.Text = "Edit Chart";
		//
		//m_dropChartOnExcelButton
		//
		this.m_dropChartOnExcelButton.Image = global::FBI.Properties.Resources.excel_blue2;
		this.m_dropChartOnExcelButton.Name = "m_dropChartOnExcelButton";
		this.m_dropChartOnExcelButton.Size = new System.Drawing.Size(146, 22);
		this.m_dropChartOnExcelButton.Text = "Drop on Excel";
		//
		//CUI2Visualization
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.Controls.Add(this.TableLayoutPanel1);
		this.Name = "CUI2Visualization";
		this.Size = new System.Drawing.Size(704, 419);
		this.VSplitContainer1.Panel1.ResumeLayout(false);
		this.VSplitContainer1.Panel2.ResumeLayout(false);
		this.VSplitContainer1.ResumeLayout(false);
		this.VSplitContainer3.Panel1.ResumeLayout(false);
		this.VSplitContainer3.Panel2.ResumeLayout(false);
		this.VSplitContainer3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Chart1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Chart3).EndInit();
		this.VSplitContainer2.Panel1.ResumeLayout(false);
		this.VSplitContainer2.Panel2.ResumeLayout(false);
		this.VSplitContainer2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Chart2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.Chart4).EndInit();
		this.TableLayoutPanel1.ResumeLayout(false);
		this.Panel1.ResumeLayout(false);
		this.Panel1.PerformLayout();
		this.m_chartsRightClickMenu.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	public VIBlend.WinForms.Controls.vSplitContainer VSplitContainer1;
	public VIBlend.WinForms.Controls.vSplitContainer VSplitContainer3;
	public VIBlend.WinForms.Controls.vSplitContainer VSplitContainer2;
	public System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
	public System.Windows.Forms.DataVisualization.Charting.Chart Chart3;
	public System.Windows.Forms.DataVisualization.Charting.Chart Chart2;
	public System.Windows.Forms.DataVisualization.Charting.Chart Chart4;
	public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	public System.Windows.Forms.Panel Panel1;
	public VIBlend.WinForms.Controls.vTextBox VersionTB;
	public VIBlend.WinForms.Controls.vTextBox CurrencyTB;
	public VIBlend.WinForms.Controls.vTextBox EntityTB;
	public VIBlend.WinForms.Controls.vLabel m_currencyLabel;
	public VIBlend.WinForms.Controls.vLabel m_versionLabel;
	public VIBlend.WinForms.Controls.vLabel m_entityLabel;
	public System.Windows.Forms.ContextMenuStrip m_chartsRightClickMenu;
	public System.Windows.Forms.ToolStripMenuItem m_editChartButton;
	public System.Windows.Forms.ToolStripMenuItem m_dropChartOnExcelButton;
	public VIBlend.WinForms.Controls.vButton m_refreshButton;

	public System.Windows.Forms.ImageList ImageList1;
}

}
