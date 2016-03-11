using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class CUIVisualization : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CUIVisualization));
      System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
      System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
      System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
      this.panel2 = new System.Windows.Forms.Panel();
      this.m_chartsRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.m_editChartButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_dropChartOnExcelButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_panelRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.m_horizontalSplitBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_splitVerticalBT = new System.Windows.Forms.ToolStripMenuItem();
      this.m_chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
      this.TableLayoutPanel1.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.m_chartsRightClickMenu.SuspendLayout();
      this.m_panelRightClick.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.m_chart)).BeginInit();
      this.SuspendLayout();
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.TableLayoutPanel1.ColumnCount = 1;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Controls.Add(this.Panel1, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
      this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(688, 380);
      this.TableLayoutPanel1.TabIndex = 1;
      // 
      // Panel1
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
      this.Panel1.Size = new System.Drawing.Size(688, 25);
      this.Panel1.TabIndex = 1;
      // 
      // m_refreshButton
      // 
      this.m_refreshButton.AllowAnimations = true;
      this.m_refreshButton.BackColor = System.Drawing.Color.Transparent;
      this.m_refreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_refreshButton.ImageKey = "refresh classic green.ico";
      this.m_refreshButton.ImageList = this.ImageList1;
      this.m_refreshButton.Location = new System.Drawing.Point(3, 0);
      this.m_refreshButton.Name = "m_refreshButton";
      this.m_refreshButton.RoundedCornersMask = ((byte)(15));
      this.m_refreshButton.Size = new System.Drawing.Size(73, 25);
      this.m_refreshButton.TabIndex = 13;
      this.m_refreshButton.Text = "Refresh";
      this.m_refreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_refreshButton.UseVisualStyleBackColor = false;
      this.m_refreshButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // ImageList1
      // 
      this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
      this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.ImageList1.Images.SetKeyName(0, "Export classic green bigger.ico");
      this.ImageList1.Images.SetKeyName(1, "refresh classic green.ico");
      // 
      // VersionTB
      // 
      this.VersionTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.VersionTB.BackColor = System.Drawing.Color.White;
      this.VersionTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.VersionTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.VersionTB.DefaultText = "Empty...";
      this.VersionTB.Enabled = false;
      this.VersionTB.Location = new System.Drawing.Point(530, 1);
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
      // CurrencyTB
      // 
      this.CurrencyTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.CurrencyTB.BackColor = System.Drawing.Color.White;
      this.CurrencyTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.CurrencyTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.CurrencyTB.DefaultText = "Empty...";
      this.CurrencyTB.Enabled = false;
      this.CurrencyTB.Location = new System.Drawing.Point(401, 1);
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
      // EntityTB
      // 
      this.EntityTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.EntityTB.BackColor = System.Drawing.Color.White;
      this.EntityTB.BoundsOffset = new System.Drawing.Size(1, 1);
      this.EntityTB.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.EntityTB.DefaultText = "Empty...";
      this.EntityTB.Enabled = false;
      this.EntityTB.Location = new System.Drawing.Point(198, 1);
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
      // m_currencyLabel
      // 
      this.m_currencyLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_currencyLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_currencyLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_currencyLabel.Ellipsis = false;
      this.m_currencyLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_currencyLabel.Location = new System.Drawing.Point(340, 5);
      this.m_currencyLabel.Multiline = true;
      this.m_currencyLabel.Name = "m_currencyLabel";
      this.m_currencyLabel.Size = new System.Drawing.Size(55, 16);
      this.m_currencyLabel.TabIndex = 9;
      this.m_currencyLabel.Text = "Currency";
      this.m_currencyLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_currencyLabel.UseMnemonics = true;
      this.m_currencyLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_versionLabel
      // 
      this.m_versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_versionLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_versionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_versionLabel.Ellipsis = false;
      this.m_versionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_versionLabel.Location = new System.Drawing.Point(476, 5);
      this.m_versionLabel.Multiline = true;
      this.m_versionLabel.Name = "m_versionLabel";
      this.m_versionLabel.Size = new System.Drawing.Size(58, 16);
      this.m_versionLabel.TabIndex = 8;
      this.m_versionLabel.Text = "Version";
      this.m_versionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_versionLabel.UseMnemonics = true;
      this.m_versionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_entityLabel
      // 
      this.m_entityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_entityLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_entityLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_entityLabel.Ellipsis = false;
      this.m_entityLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_entityLabel.Location = new System.Drawing.Point(156, 5);
      this.m_entityLabel.Multiline = true;
      this.m_entityLabel.Name = "m_entityLabel";
      this.m_entityLabel.Size = new System.Drawing.Size(33, 13);
      this.m_entityLabel.TabIndex = 7;
      this.m_entityLabel.Text = "Entity";
      this.m_entityLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_entityLabel.UseMnemonics = true;
      this.m_entityLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // panel2
      // 
      this.panel2.Controls.Add(this.m_chart);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel2.Location = new System.Drawing.Point(3, 28);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(682, 349);
      this.panel2.TabIndex = 2;
      // 
      // m_chartsRightClickMenu
      // 
      this.m_chartsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_editChartButton,
            this.m_dropChartOnExcelButton});
      this.m_chartsRightClickMenu.Name = "m_chartsRightClickMenu";
      this.m_chartsRightClickMenu.Size = new System.Drawing.Size(147, 48);
      // 
      // m_editChartButton
      // 
      this.m_editChartButton.Image = global::FBI.Properties.Resources.chart_line;
      this.m_editChartButton.Name = "m_editChartButton";
      this.m_editChartButton.Size = new System.Drawing.Size(146, 22);
      this.m_editChartButton.Text = "Edit Chart";
      // 
      // m_dropChartOnExcelButton
      // 
      this.m_dropChartOnExcelButton.Image = global::FBI.Properties.Resources.excel_blue2;
      this.m_dropChartOnExcelButton.Name = "m_dropChartOnExcelButton";
      this.m_dropChartOnExcelButton.Size = new System.Drawing.Size(146, 22);
      this.m_dropChartOnExcelButton.Text = "Drop on Excel";
      // 
      // m_panelRightClick
      // 
      this.m_panelRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_horizontalSplitBT,
            this.m_splitVerticalBT});
      this.m_panelRightClick.Name = "m_chartsRightClickMenu";
      this.m_panelRightClick.Size = new System.Drawing.Size(156, 48);
      // 
      // m_horizontalSplitBT
      // 
      this.m_horizontalSplitBT.Name = "m_horizontalSplitBT";
      this.m_horizontalSplitBT.Size = new System.Drawing.Size(155, 22);
      this.m_horizontalSplitBT.Text = "Split Horizontal";
      // 
      // m_splitVerticalBT
      // 
      this.m_splitVerticalBT.Name = "m_splitVerticalBT";
      this.m_splitVerticalBT.Size = new System.Drawing.Size(155, 22);
      this.m_splitVerticalBT.Text = "Split Vertical";
      // 
      // m_chart
      // 
      chartArea1.Name = "ChartArea1";
      this.m_chart.ChartAreas.Add(chartArea1);
      this.m_chart.Dock = System.Windows.Forms.DockStyle.Fill;
      legend1.Name = "Legend1";
      this.m_chart.Legends.Add(legend1);
      this.m_chart.Location = new System.Drawing.Point(0, 0);
      this.m_chart.Name = "m_chart";
      this.m_chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
      series1.ChartArea = "ChartArea1";
      series1.Legend = "Legend1";
      series1.Name = "Series1";
      this.m_chart.Series.Add(series1);
      this.m_chart.Size = new System.Drawing.Size(682, 349);
      this.m_chart.TabIndex = 0;
      this.m_chart.Text = "chart1";
      // 
      // CUIVisualization
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(688, 380);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Name = "CUIVisualization";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.Panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.m_chartsRightClickMenu.ResumeLayout(false);
      this.m_panelRightClick.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.m_chart)).EndInit();
      this.ResumeLayout(false);

  }
  public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	public System.Windows.Forms.ContextMenuStrip m_chartsRightClickMenu;
	public System.Windows.Forms.ToolStripMenuItem m_editChartButton;
  public System.Windows.Forms.ToolStripMenuItem m_dropChartOnExcelButton;

  public System.Windows.Forms.ImageList ImageList1;
  public System.Windows.Forms.ContextMenuStrip m_panelRightClick;
  private System.Windows.Forms.ToolStripMenuItem m_horizontalSplitBT;
  public System.Windows.Forms.Panel Panel1;
  public VIBlend.WinForms.Controls.vButton m_refreshButton;
  public VIBlend.WinForms.Controls.vTextBox VersionTB;
  public VIBlend.WinForms.Controls.vTextBox CurrencyTB;
  public VIBlend.WinForms.Controls.vTextBox EntityTB;
  public VIBlend.WinForms.Controls.vLabel m_currencyLabel;
  public VIBlend.WinForms.Controls.vLabel m_versionLabel;
  public VIBlend.WinForms.Controls.vLabel m_entityLabel;
  private System.Windows.Forms.Panel panel2;
  private System.Windows.Forms.ToolStripMenuItem m_splitVerticalBT;
  private System.Windows.Forms.DataVisualization.Charting.Chart m_chart;
}

}
