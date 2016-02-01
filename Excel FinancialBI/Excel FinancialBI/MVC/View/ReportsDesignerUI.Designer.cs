using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class ReportDesignerUI : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportDesignerUI));
      this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
      this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.ReportsTVPanel = new System.Windows.Forms.Panel();
      this.Panel2 = new System.Windows.Forms.Panel();
      this.NewSerieBT = new System.Windows.Forms.Button();
      this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
      this.NewReportBT = new System.Windows.Forms.Button();
      this.DeleteReportBT = new System.Windows.Forms.Button();
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.GroupBox1 = new System.Windows.Forms.GroupBox();
      this.TableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
      this.Label6 = new System.Windows.Forms.Label();
      this.Label4 = new System.Windows.Forms.Label();
      this.ChartNameLabel = new System.Windows.Forms.Label();
      this.ReportTypeCB = new System.Windows.Forms.ComboBox();
      this.ReportNameTB = new System.Windows.Forms.TextBox();
      this.Label3 = new System.Windows.Forms.Label();
      this.Label5 = new System.Windows.Forms.Label();
      this.ReportPaletteCB = new System.Windows.Forms.ComboBox();
      this.Axis1TB = new System.Windows.Forms.TextBox();
      this.Axis2TB = new System.Windows.Forms.TextBox();
      this.GroupBox2 = new System.Windows.Forms.GroupBox();
      this.TableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
      this.Label1 = new System.Windows.Forms.Label();
      this.ItemCB = new System.Windows.Forms.ComboBox();
      this.NameTB = new System.Windows.Forms.TextBox();
      this.Label2 = new System.Windows.Forms.Label();
      this.GroupBox3 = new System.Windows.Forms.GroupBox();
      this.ChartPanel = new System.Windows.Forms.Panel();
      this.GroupBox4 = new System.Windows.Forms.GroupBox();
      this.TableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
      this.Label10 = new System.Windows.Forms.Label();
      this.TypeCB = new System.Windows.Forms.ComboBox();
      this.Label11 = new System.Windows.Forms.Label();
      this.Label13 = new System.Windows.Forms.Label();
      this.Label15 = new System.Windows.Forms.Label();
      this.Label16 = new System.Windows.Forms.Label();
      this.Label17 = new System.Windows.Forms.Label();
      this.AxisCB = new System.Windows.Forms.ComboBox();
      this.UnitTB = new System.Windows.Forms.TextBox();
      this.ColorBT = new System.Windows.Forms.Button();
      this.WidthNumericUpDown = new System.Windows.Forms.NumericUpDown();
      this.ValuesDisplayRB = new System.Windows.Forms.CheckBox();
      this.ReportsTVImageList = new System.Windows.Forms.ImageList(this.components);
      this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
      this.TVRCM = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.NewReportRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.NewSerieRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.RenameRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.DeleteRCM = new System.Windows.Forms.ToolStripMenuItem();
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
      this.SplitContainer1.Panel1.SuspendLayout();
      this.SplitContainer1.Panel2.SuspendLayout();
      this.SplitContainer1.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.Panel2.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      this.GroupBox1.SuspendLayout();
      this.TableLayoutPanel5.SuspendLayout();
      this.GroupBox2.SuspendLayout();
      this.TableLayoutPanel3.SuspendLayout();
      this.GroupBox3.SuspendLayout();
      this.GroupBox4.SuspendLayout();
      this.TableLayoutPanel4.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).BeginInit();
      this.TVRCM.SuspendLayout();
      this.SuspendLayout();
      // 
      // SplitContainer1
      // 
      this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
      this.SplitContainer1.Name = "SplitContainer1";
      // 
      // SplitContainer1.Panel1
      // 
      this.SplitContainer1.Panel1.Controls.Add(this.TableLayoutPanel2);
      // 
      // SplitContainer1.Panel2
      // 
      this.SplitContainer1.Panel2.Controls.Add(this.TableLayoutPanel1);
      this.SplitContainer1.Size = new System.Drawing.Size(1006, 558);
      this.SplitContainer1.SplitterDistance = 240;
      this.SplitContainer1.TabIndex = 0;
      // 
      // TableLayoutPanel2
      // 
      this.TableLayoutPanel2.ColumnCount = 1;
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel2.Controls.Add(this.ReportsTVPanel, 0, 1);
      this.TableLayoutPanel2.Controls.Add(this.Panel2, 0, 0);
      this.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 2;
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel2.Size = new System.Drawing.Size(240, 558);
      this.TableLayoutPanel2.TabIndex = 1;
      // 
      // ReportsTVPanel
      // 
      this.ReportsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.ReportsTVPanel.Location = new System.Drawing.Point(3, 28);
      this.ReportsTVPanel.Name = "ReportsTVPanel";
      this.ReportsTVPanel.Size = new System.Drawing.Size(234, 527);
      this.ReportsTVPanel.TabIndex = 0;
      // 
      // Panel2
      // 
      this.Panel2.Controls.Add(this.NewSerieBT);
      this.Panel2.Controls.Add(this.NewReportBT);
      this.Panel2.Controls.Add(this.DeleteReportBT);
      this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Panel2.Location = new System.Drawing.Point(1, 1);
      this.Panel2.Margin = new System.Windows.Forms.Padding(1);
      this.Panel2.Name = "Panel2";
      this.Panel2.Size = new System.Drawing.Size(238, 23);
      this.Panel2.TabIndex = 1;
      // 
      // NewSerieBT
      // 
      this.NewSerieBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this.NewSerieBT.FlatAppearance.BorderSize = 0;
      this.NewSerieBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.NewSerieBT.ImageKey = "favicon(188).ico";
      this.NewSerieBT.ImageList = this.ButtonsImageList;
      this.NewSerieBT.Location = new System.Drawing.Point(39, 1);
      this.NewSerieBT.Name = "NewSerieBT";
      this.NewSerieBT.Size = new System.Drawing.Size(22, 22);
      this.NewSerieBT.TabIndex = 14;
      this.NewSerieBT.UseVisualStyleBackColor = true;
      // 
      // ButtonsImageList
      // 
      this.ButtonsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonsImageList.ImageStream")));
      this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsImageList.Images.SetKeyName(0, "favicon(236).ico");
      this.ButtonsImageList.Images.SetKeyName(1, "Refresh2.png");
      this.ButtonsImageList.Images.SetKeyName(2, "Target zoomed.png");
      this.ButtonsImageList.Images.SetKeyName(3, "Report.png");
      this.ButtonsImageList.Images.SetKeyName(4, "favicon(187).ico");
      this.ButtonsImageList.Images.SetKeyName(5, "folder 2 ctrl bgd.png");
      this.ButtonsImageList.Images.SetKeyName(6, "favicon(239).ico");
      this.ButtonsImageList.Images.SetKeyName(7, "favicon(188).ico");
      this.ButtonsImageList.Images.SetKeyName(8, "add blue.jpg");
      this.ButtonsImageList.Images.SetKeyName(9, "imageres_89.ico");
      // 
      // NewReportBT
      // 
      this.NewReportBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
      this.NewReportBT.FlatAppearance.BorderSize = 0;
      this.NewReportBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.NewReportBT.ImageKey = "favicon(239).ico";
      this.NewReportBT.ImageList = this.ButtonsImageList;
      this.NewReportBT.Location = new System.Drawing.Point(11, 1);
      this.NewReportBT.Name = "NewReportBT";
      this.NewReportBT.Size = new System.Drawing.Size(22, 22);
      this.NewReportBT.TabIndex = 13;
      this.NewReportBT.UseVisualStyleBackColor = true;
      // 
      // DeleteReportBT
      // 
      this.DeleteReportBT.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
      this.DeleteReportBT.FlatAppearance.BorderSize = 0;
      this.DeleteReportBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.DeleteReportBT.ImageKey = "imageres_89.ico";
      this.DeleteReportBT.ImageList = this.ButtonsImageList;
      this.DeleteReportBT.Location = new System.Drawing.Point(69, 2);
      this.DeleteReportBT.Name = "DeleteReportBT";
      this.DeleteReportBT.Size = new System.Drawing.Size(22, 22);
      this.DeleteReportBT.TabIndex = 12;
      this.DeleteReportBT.UseVisualStyleBackColor = true;
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.TableLayoutPanel1.Controls.Add(this.GroupBox1, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.GroupBox2, 0, 1);
      this.TableLayoutPanel1.Controls.Add(this.GroupBox3, 1, 0);
      this.TableLayoutPanel1.Controls.Add(this.GroupBox4, 1, 1);
      this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.18868F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.81132F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(762, 558);
      this.TableLayoutPanel1.TabIndex = 0;
      // 
      // GroupBox1
      // 
      this.GroupBox1.Controls.Add(this.TableLayoutPanel5);
      this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GroupBox1.Location = new System.Drawing.Point(3, 3);
      this.GroupBox1.Name = "GroupBox1";
      this.GroupBox1.Size = new System.Drawing.Size(375, 218);
      this.GroupBox1.TabIndex = 0;
      this.GroupBox1.TabStop = false;
      this.GroupBox1.Text = "Report";
      // 
      // TableLayoutPanel5
      // 
      this.TableLayoutPanel5.ColumnCount = 2;
      this.TableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.59789F));
      this.TableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.40211F));
      this.TableLayoutPanel5.Controls.Add(this.Label6, 0, 4);
      this.TableLayoutPanel5.Controls.Add(this.Label4, 0, 3);
      this.TableLayoutPanel5.Controls.Add(this.ChartNameLabel, 0, 0);
      this.TableLayoutPanel5.Controls.Add(this.ReportTypeCB, 1, 2);
      this.TableLayoutPanel5.Controls.Add(this.ReportNameTB, 1, 0);
      this.TableLayoutPanel5.Controls.Add(this.Label3, 0, 2);
      this.TableLayoutPanel5.Controls.Add(this.Label5, 0, 1);
      this.TableLayoutPanel5.Controls.Add(this.ReportPaletteCB, 1, 1);
      this.TableLayoutPanel5.Controls.Add(this.Axis1TB, 1, 3);
      this.TableLayoutPanel5.Controls.Add(this.Axis2TB, 1, 4);
      this.TableLayoutPanel5.Location = new System.Drawing.Point(6, 53);
      this.TableLayoutPanel5.Name = "TableLayoutPanel5";
      this.TableLayoutPanel5.RowCount = 5;
      this.TableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.TableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.TableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.TableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.TableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
      this.TableLayoutPanel5.Size = new System.Drawing.Size(378, 123);
      this.TableLayoutPanel5.TabIndex = 0;
      // 
      // Label6
      // 
      this.Label6.AutoSize = true;
      this.Label6.Location = new System.Drawing.Point(3, 96);
      this.Label6.Name = "Label6";
      this.Label6.Size = new System.Drawing.Size(45, 13);
      this.Label6.TabIndex = 21;
      this.Label6.Text = "Axis Y 2";
      // 
      // Label4
      // 
      this.Label4.AutoSize = true;
      this.Label4.Location = new System.Drawing.Point(3, 72);
      this.Label4.Name = "Label4";
      this.Label4.Size = new System.Drawing.Size(45, 13);
      this.Label4.TabIndex = 19;
      this.Label4.Text = "Axis Y 1";
      // 
      // ChartNameLabel
      // 
      this.ChartNameLabel.AutoSize = true;
      this.ChartNameLabel.Location = new System.Drawing.Point(3, 0);
      this.ChartNameLabel.Name = "ChartNameLabel";
      this.ChartNameLabel.Size = new System.Drawing.Size(35, 13);
      this.ChartNameLabel.TabIndex = 14;
      this.ChartNameLabel.Text = "Name";
      // 
      // ReportTypeCB
      // 
      this.ReportTypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ReportTypeCB.FormattingEnabled = true;
      this.ReportTypeCB.Items.AddRange(new object[] {
            "Table",
            "Chart"});
      this.ReportTypeCB.Location = new System.Drawing.Point(130, 51);
      this.ReportTypeCB.Name = "ReportTypeCB";
      this.ReportTypeCB.Size = new System.Drawing.Size(187, 21);
      this.ReportTypeCB.TabIndex = 18;
      // 
      // ReportNameTB
      // 
      this.ReportNameTB.Enabled = false;
      this.ReportNameTB.Location = new System.Drawing.Point(130, 3);
      this.ReportNameTB.Name = "ReportNameTB";
      this.ReportNameTB.Size = new System.Drawing.Size(187, 20);
      this.ReportNameTB.TabIndex = 13;
      // 
      // Label3
      // 
      this.Label3.AutoSize = true;
      this.Label3.Location = new System.Drawing.Point(3, 48);
      this.Label3.Name = "Label3";
      this.Label3.Size = new System.Drawing.Size(70, 13);
      this.Label3.TabIndex = 17;
      this.Label3.Text = "Chart / Table";
      // 
      // Label5
      // 
      this.Label5.AutoSize = true;
      this.Label5.Location = new System.Drawing.Point(3, 24);
      this.Label5.Name = "Label5";
      this.Label5.Size = new System.Drawing.Size(40, 13);
      this.Label5.TabIndex = 16;
      this.Label5.Text = "Palette";
      // 
      // ReportPaletteCB
      // 
      this.ReportPaletteCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ReportPaletteCB.FormattingEnabled = true;
      this.ReportPaletteCB.Location = new System.Drawing.Point(130, 27);
      this.ReportPaletteCB.Name = "ReportPaletteCB";
      this.ReportPaletteCB.Size = new System.Drawing.Size(187, 21);
      this.ReportPaletteCB.TabIndex = 15;
      // 
      // Axis1TB
      // 
      this.Axis1TB.Location = new System.Drawing.Point(130, 75);
      this.Axis1TB.Name = "Axis1TB";
      this.Axis1TB.Size = new System.Drawing.Size(187, 20);
      this.Axis1TB.TabIndex = 22;
      // 
      // Axis2TB
      // 
      this.Axis2TB.Location = new System.Drawing.Point(130, 99);
      this.Axis2TB.Name = "Axis2TB";
      this.Axis2TB.Size = new System.Drawing.Size(187, 20);
      this.Axis2TB.TabIndex = 23;
      // 
      // GroupBox2
      // 
      this.GroupBox2.Controls.Add(this.TableLayoutPanel3);
      this.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GroupBox2.Location = new System.Drawing.Point(3, 227);
      this.GroupBox2.Name = "GroupBox2";
      this.GroupBox2.Size = new System.Drawing.Size(375, 328);
      this.GroupBox2.TabIndex = 1;
      this.GroupBox2.TabStop = false;
      this.GroupBox2.Text = "Serie";
      // 
      // TableLayoutPanel3
      // 
      this.TableLayoutPanel3.ColumnCount = 2;
      this.TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 186F));
      this.TableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel3.Controls.Add(this.Label1, 0, 0);
      this.TableLayoutPanel3.Controls.Add(this.ItemCB, 1, 1);
      this.TableLayoutPanel3.Controls.Add(this.NameTB, 1, 0);
      this.TableLayoutPanel3.Controls.Add(this.Label2, 0, 1);
      this.TableLayoutPanel3.Location = new System.Drawing.Point(6, 35);
      this.TableLayoutPanel3.Name = "TableLayoutPanel3";
      this.TableLayoutPanel3.RowCount = 4;
      this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel3.Size = new System.Drawing.Size(431, 284);
      this.TableLayoutPanel3.TabIndex = 3;
      // 
      // Label1
      // 
      this.Label1.AutoSize = true;
      this.Label1.Location = new System.Drawing.Point(3, 0);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(35, 13);
      this.Label1.TabIndex = 4;
      this.Label1.Text = "Name";
      // 
      // ItemCB
      // 
      this.ItemCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.ItemCB.FormattingEnabled = true;
      this.ItemCB.Location = new System.Drawing.Point(189, 28);
      this.ItemCB.Name = "ItemCB";
      this.ItemCB.Size = new System.Drawing.Size(239, 21);
      this.ItemCB.TabIndex = 0;
      // 
      // NameTB
      // 
      this.NameTB.Enabled = false;
      this.NameTB.Location = new System.Drawing.Point(189, 3);
      this.NameTB.Name = "NameTB";
      this.NameTB.Size = new System.Drawing.Size(239, 20);
      this.NameTB.TabIndex = 3;
      // 
      // Label2
      // 
      this.Label2.AutoSize = true;
      this.Label2.Location = new System.Drawing.Point(3, 25);
      this.Label2.Name = "Label2";
      this.Label2.Size = new System.Drawing.Size(141, 13);
      this.Label2.TabIndex = 5;
      this.Label2.Text = "Financial or Operational Item";
      // 
      // GroupBox3
      // 
      this.GroupBox3.Controls.Add(this.ChartPanel);
      this.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GroupBox3.Location = new System.Drawing.Point(384, 3);
      this.GroupBox3.Name = "GroupBox3";
      this.GroupBox3.Size = new System.Drawing.Size(375, 218);
      this.GroupBox3.TabIndex = 2;
      this.GroupBox3.TabStop = false;
      this.GroupBox3.Text = "Report Preview";
      // 
      // ChartPanel
      // 
      this.ChartPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.ChartPanel.Location = new System.Drawing.Point(24, 29);
      this.ChartPanel.Margin = new System.Windows.Forms.Padding(5);
      this.ChartPanel.Name = "ChartPanel";
      this.ChartPanel.Size = new System.Drawing.Size(328, 172);
      this.ChartPanel.TabIndex = 0;
      // 
      // GroupBox4
      // 
      this.GroupBox4.Controls.Add(this.TableLayoutPanel4);
      this.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GroupBox4.Location = new System.Drawing.Point(384, 227);
      this.GroupBox4.Name = "GroupBox4";
      this.GroupBox4.Size = new System.Drawing.Size(375, 328);
      this.GroupBox4.TabIndex = 3;
      this.GroupBox4.TabStop = false;
      this.GroupBox4.Text = "Serie Display";
      // 
      // TableLayoutPanel4
      // 
      this.TableLayoutPanel4.ColumnCount = 2;
      this.TableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
      this.TableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel4.Controls.Add(this.Label10, 0, 2);
      this.TableLayoutPanel4.Controls.Add(this.TypeCB, 1, 1);
      this.TableLayoutPanel4.Controls.Add(this.Label11, 0, 1);
      this.TableLayoutPanel4.Controls.Add(this.Label13, 0, 0);
      this.TableLayoutPanel4.Controls.Add(this.Label15, 0, 4);
      this.TableLayoutPanel4.Controls.Add(this.Label16, 0, 3);
      this.TableLayoutPanel4.Controls.Add(this.Label17, 0, 5);
      this.TableLayoutPanel4.Controls.Add(this.AxisCB, 1, 2);
      this.TableLayoutPanel4.Controls.Add(this.UnitTB, 1, 4);
      this.TableLayoutPanel4.Controls.Add(this.ColorBT, 1, 0);
      this.TableLayoutPanel4.Controls.Add(this.WidthNumericUpDown, 1, 3);
      this.TableLayoutPanel4.Controls.Add(this.ValuesDisplayRB, 1, 5);
      this.TableLayoutPanel4.Location = new System.Drawing.Point(24, 35);
      this.TableLayoutPanel4.Name = "TableLayoutPanel4";
      this.TableLayoutPanel4.RowCount = 8;
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
      this.TableLayoutPanel4.Size = new System.Drawing.Size(431, 284);
      this.TableLayoutPanel4.TabIndex = 4;
      // 
      // Label10
      // 
      this.Label10.AutoSize = true;
      this.Label10.Location = new System.Drawing.Point(3, 50);
      this.Label10.Name = "Label10";
      this.Label10.Size = new System.Drawing.Size(26, 13);
      this.Label10.TabIndex = 10;
      this.Label10.Text = "Axis";
      // 
      // TypeCB
      // 
      this.TypeCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.TypeCB.FormattingEnabled = true;
      this.TypeCB.Location = new System.Drawing.Point(119, 28);
      this.TypeCB.Name = "TypeCB";
      this.TypeCB.Size = new System.Drawing.Size(163, 21);
      this.TypeCB.TabIndex = 7;
      // 
      // Label11
      // 
      this.Label11.AutoSize = true;
      this.Label11.Location = new System.Drawing.Point(3, 25);
      this.Label11.Name = "Label11";
      this.Label11.Size = new System.Drawing.Size(31, 13);
      this.Label11.TabIndex = 9;
      this.Label11.Text = "Type";
      // 
      // Label13
      // 
      this.Label13.AutoSize = true;
      this.Label13.Location = new System.Drawing.Point(3, 0);
      this.Label13.Name = "Label13";
      this.Label13.Size = new System.Drawing.Size(31, 13);
      this.Label13.TabIndex = 8;
      this.Label13.Text = "Color";
      // 
      // Label15
      // 
      this.Label15.AutoSize = true;
      this.Label15.Location = new System.Drawing.Point(3, 100);
      this.Label15.Name = "Label15";
      this.Label15.Size = new System.Drawing.Size(26, 13);
      this.Label15.TabIndex = 11;
      this.Label15.Text = "Unit";
      // 
      // Label16
      // 
      this.Label16.AutoSize = true;
      this.Label16.Location = new System.Drawing.Point(3, 75);
      this.Label16.Name = "Label16";
      this.Label16.Size = new System.Drawing.Size(35, 13);
      this.Label16.TabIndex = 12;
      this.Label16.Text = "Width";
      // 
      // Label17
      // 
      this.Label17.AutoSize = true;
      this.Label17.Location = new System.Drawing.Point(3, 125);
      this.Label17.Name = "Label17";
      this.Label17.Size = new System.Drawing.Size(68, 13);
      this.Label17.TabIndex = 13;
      this.Label17.Text = "Values Label";
      // 
      // AxisCB
      // 
      this.AxisCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.AxisCB.FormattingEnabled = true;
      this.AxisCB.Items.AddRange(new object[] {
            "Primary",
            "Secondary"});
      this.AxisCB.Location = new System.Drawing.Point(119, 53);
      this.AxisCB.Name = "AxisCB";
      this.AxisCB.Size = new System.Drawing.Size(163, 21);
      this.AxisCB.TabIndex = 14;
      // 
      // UnitTB
      // 
      this.UnitTB.Location = new System.Drawing.Point(119, 103);
      this.UnitTB.Name = "UnitTB";
      this.UnitTB.Size = new System.Drawing.Size(163, 20);
      this.UnitTB.TabIndex = 16;
      // 
      // ColorBT
      // 
      this.ColorBT.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.ColorBT.FlatAppearance.BorderSize = 0;
      this.ColorBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.ColorBT.Location = new System.Drawing.Point(119, 3);
      this.ColorBT.Name = "ColorBT";
      this.ColorBT.Size = new System.Drawing.Size(27, 19);
      this.ColorBT.TabIndex = 18;
      this.ColorBT.UseVisualStyleBackColor = false;
      // 
      // WidthNumericUpDown
      // 
      this.WidthNumericUpDown.Location = new System.Drawing.Point(119, 78);
      this.WidthNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
      this.WidthNumericUpDown.Name = "WidthNumericUpDown";
      this.WidthNumericUpDown.Size = new System.Drawing.Size(161, 20);
      this.WidthNumericUpDown.TabIndex = 19;
      this.WidthNumericUpDown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
      // 
      // ValuesDisplayRB
      // 
      this.ValuesDisplayRB.AutoSize = true;
      this.ValuesDisplayRB.Location = new System.Drawing.Point(119, 128);
      this.ValuesDisplayRB.Name = "ValuesDisplayRB";
      this.ValuesDisplayRB.Size = new System.Drawing.Size(15, 14);
      this.ValuesDisplayRB.TabIndex = 20;
      this.ValuesDisplayRB.UseVisualStyleBackColor = true;
      // 
      // ReportsTVImageList
      // 
      this.ReportsTVImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ReportsTVImageList.ImageStream")));
      this.ReportsTVImageList.TransparentColor = System.Drawing.Color.Transparent;
      this.ReportsTVImageList.Images.SetKeyName(0, "favicon(239).ico");
      this.ReportsTVImageList.Images.SetKeyName(1, "favicon(176).ico");
      this.ReportsTVImageList.Images.SetKeyName(2, "favicon(2).ico");
      this.ReportsTVImageList.Images.SetKeyName(3, "PPS black and white small.ico");
      // 
      // ColorDialog1
      // 
      this.ColorDialog1.AnyColor = true;
      // 
      // TVRCM
      // 
      this.TVRCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewReportRCM,
            this.NewSerieRCM,
            this.ToolStripSeparator1,
            this.RenameRCM,
            this.ToolStripSeparator2,
            this.DeleteRCM});
      this.TVRCM.Name = "TVRCM";
      this.TVRCM.Size = new System.Drawing.Size(137, 104);
      // 
      // NewReportRCM
      // 
      this.NewReportRCM.Image = global::FBI.Properties.Resources.checked1;
      this.NewReportRCM.Name = "NewReportRCM";
      this.NewReportRCM.Size = new System.Drawing.Size(136, 22);
      this.NewReportRCM.Text = "New Report";
      // 
      // NewSerieRCM
      // 
      this.NewSerieRCM.Image = global::FBI.Properties.Resources.favicon_233_;
      this.NewSerieRCM.Name = "NewSerieRCM";
      this.NewSerieRCM.Size = new System.Drawing.Size(136, 22);
      this.NewSerieRCM.Text = "New Serie";
      // 
      // ToolStripSeparator1
      // 
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(133, 6);
      // 
      // RenameRCM
      // 
      this.RenameRCM.Name = "RenameRCM";
      this.RenameRCM.Size = new System.Drawing.Size(136, 22);
      this.RenameRCM.Text = "Rename";
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(133, 6);
      // 
      // DeleteRCM
      // 
      this.DeleteRCM.Image = global::FBI.Properties.Resources.imageres_89;
      this.DeleteRCM.Name = "DeleteRCM";
      this.DeleteRCM.Size = new System.Drawing.Size(136, 22);
      this.DeleteRCM.Text = "Delete";
      // 
      // ReportDesignerUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1006, 558);
      this.Controls.Add(this.SplitContainer1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "ReportDesignerUI";
      this.Text = "Reports Designer";
      this.SplitContainer1.Panel1.ResumeLayout(false);
      this.SplitContainer1.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
      this.SplitContainer1.ResumeLayout(false);
      this.TableLayoutPanel2.ResumeLayout(false);
      this.Panel2.ResumeLayout(false);
      this.TableLayoutPanel1.ResumeLayout(false);
      this.GroupBox1.ResumeLayout(false);
      this.TableLayoutPanel5.ResumeLayout(false);
      this.TableLayoutPanel5.PerformLayout();
      this.GroupBox2.ResumeLayout(false);
      this.TableLayoutPanel3.ResumeLayout(false);
      this.TableLayoutPanel3.PerformLayout();
      this.GroupBox3.ResumeLayout(false);
      this.GroupBox4.ResumeLayout(false);
      this.TableLayoutPanel4.ResumeLayout(false);
      this.TableLayoutPanel4.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.WidthNumericUpDown)).EndInit();
      this.TVRCM.ResumeLayout(false);
      this.ResumeLayout(false);

	}
	internal System.Windows.Forms.SplitContainer SplitContainer1;
	internal System.Windows.Forms.ImageList ReportsTVImageList;
	internal System.Windows.Forms.ImageList ButtonsImageList;
	internal System.Windows.Forms.ColorDialog ColorDialog1;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
	internal System.Windows.Forms.Panel ReportsTVPanel;
	internal System.Windows.Forms.Panel Panel2;
	internal System.Windows.Forms.Button NewSerieBT;
	internal System.Windows.Forms.Button NewReportBT;
	internal System.Windows.Forms.Button DeleteReportBT;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	internal System.Windows.Forms.GroupBox GroupBox1;
	internal System.Windows.Forms.Label Label5;
	internal System.Windows.Forms.ComboBox ReportPaletteCB;
	internal System.Windows.Forms.Label ChartNameLabel;
	internal System.Windows.Forms.TextBox ReportNameTB;
	internal System.Windows.Forms.GroupBox GroupBox2;
	internal System.Windows.Forms.GroupBox GroupBox3;
	internal System.Windows.Forms.Label Label1;
	internal System.Windows.Forms.TextBox NameTB;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel3;
	internal System.Windows.Forms.ComboBox ItemCB;
	internal System.Windows.Forms.Label Label2;
	internal System.Windows.Forms.Panel ChartPanel;
	internal System.Windows.Forms.GroupBox GroupBox4;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel4;
	internal System.Windows.Forms.Label Label10;
	internal System.Windows.Forms.ComboBox TypeCB;
	internal System.Windows.Forms.Label Label11;
	internal System.Windows.Forms.Label Label13;
	internal System.Windows.Forms.Label Label15;
	internal System.Windows.Forms.Label Label16;
	internal System.Windows.Forms.Label Label17;
	internal System.Windows.Forms.ComboBox AxisCB;
	internal System.Windows.Forms.TextBox UnitTB;
	internal System.Windows.Forms.Button ColorBT;
	internal System.Windows.Forms.NumericUpDown WidthNumericUpDown;
	internal System.Windows.Forms.ComboBox ReportTypeCB;
	internal System.Windows.Forms.Label Label3;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel5;
	internal System.Windows.Forms.Label Label4;
	internal System.Windows.Forms.Label Label6;
	internal System.Windows.Forms.TextBox Axis1TB;
	internal System.Windows.Forms.TextBox Axis2TB;
	internal System.Windows.Forms.ContextMenuStrip TVRCM;
	internal System.Windows.Forms.ToolStripMenuItem NewReportRCM;
	internal System.Windows.Forms.ToolStripMenuItem NewSerieRCM;
	internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
	internal System.Windows.Forms.ToolStripMenuItem DeleteRCM;
	internal System.Windows.Forms.ToolStripMenuItem RenameRCM;
	internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
	internal System.Windows.Forms.CheckBox ValuesDisplayRB;
}

}
