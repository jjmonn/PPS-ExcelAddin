using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class SubmissionsControlUI : System.Windows.Forms.Form
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubmissionsControlUI));
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
		this.EntitiesTVPanel = new System.Windows.Forms.Panel();
		this.Panel1 = new System.Windows.Forms.Panel();
		this.RefreshBT = new System.Windows.Forms.Button();
		this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
		this.ChartsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
		this.ControlsDGVPanel = new System.Windows.Forms.Panel();
		this.Panel3 = new System.Windows.Forms.Panel();
		this.EntityTB = new System.Windows.Forms.TextBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.Panel2 = new System.Windows.Forms.Panel();
		this.VersionTB = new System.Windows.Forms.TextBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.Panel4 = new System.Windows.Forms.Panel();
		this.CurrencyTB = new System.Windows.Forms.TextBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.EntitiesTVImageList = new System.Windows.Forms.ImageList(this.components);
		this.TVRCM = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.DisplayEntityControlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		this.TableLayoutPanel2.SuspendLayout();
		this.Panel1.SuspendLayout();
		this.ChartsTableLayoutPanel.SuspendLayout();
		this.Panel3.SuspendLayout();
		this.Panel2.SuspendLayout();
		this.Panel4.SuspendLayout();
		this.TVRCM.SuspendLayout();
		this.SuspendLayout();
		//
		//SplitContainer1
		//
		this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer1.Name = "SplitContainer1";
		//
		//SplitContainer1.Panel1
		//
		this.SplitContainer1.Panel1.Controls.Add(this.TableLayoutPanel2);
		//
		//SplitContainer1.Panel2
		//
		this.SplitContainer1.Panel2.Controls.Add(this.ChartsTableLayoutPanel);
		this.SplitContainer1.Size = new System.Drawing.Size(1035, 613);
		this.SplitContainer1.SplitterDistance = 184;
		this.SplitContainer1.TabIndex = 0;
		//
		//TableLayoutPanel2
		//
		this.TableLayoutPanel2.ColumnCount = 1;
		this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel2.Controls.Add(this.EntitiesTVPanel, 0, 1);
		this.TableLayoutPanel2.Controls.Add(this.Panel1, 0, 0);
		this.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
		this.TableLayoutPanel2.Name = "TableLayoutPanel2";
		this.TableLayoutPanel2.RowCount = 2;
		this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f));
		this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel2.Size = new System.Drawing.Size(184, 613);
		this.TableLayoutPanel2.TabIndex = 0;
		//
		//EntitiesTVPanel
		//
		this.EntitiesTVPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.EntitiesTVPanel.Location = new System.Drawing.Point(1, 26);
		this.EntitiesTVPanel.Margin = new System.Windows.Forms.Padding(1);
		this.EntitiesTVPanel.Name = "EntitiesTVPanel";
		this.EntitiesTVPanel.Size = new System.Drawing.Size(182, 586);
		this.EntitiesTVPanel.TabIndex = 0;
		//
		//Panel1
		//
		this.Panel1.Controls.Add(this.RefreshBT);
		this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Panel1.Location = new System.Drawing.Point(0, 0);
		this.Panel1.Margin = new System.Windows.Forms.Padding(0);
		this.Panel1.Name = "Panel1";
		this.Panel1.Size = new System.Drawing.Size(184, 25);
		this.Panel1.TabIndex = 1;
		//
		//RefreshBT
		//
		this.RefreshBT.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
		this.RefreshBT.FlatAppearance.BorderSize = 0;
		this.RefreshBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.RefreshBT.ImageKey = "Refresh2.png";
		this.RefreshBT.ImageList = this.ButtonsImageList;
		this.RefreshBT.Location = new System.Drawing.Point(6, 1);
		this.RefreshBT.Name = "RefreshBT";
		this.RefreshBT.Size = new System.Drawing.Size(22, 22);
		this.RefreshBT.TabIndex = 3;
		this.RefreshBT.UseVisualStyleBackColor = true;
		//
		//ButtonsImageList
		//
		this.ButtonsImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonsImageList.ImageStream");
		this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.ButtonsImageList.Images.SetKeyName(0, "imageres_89.ico");
		this.ButtonsImageList.Images.SetKeyName(1, "favicon(236).ico");
		this.ButtonsImageList.Images.SetKeyName(2, "Refresh2.png");
		this.ButtonsImageList.Images.SetKeyName(3, "Target zoomed.png");
		this.ButtonsImageList.Images.SetKeyName(4, "Report.png");
		this.ButtonsImageList.Images.SetKeyName(5, "favicon(187).ico");
		this.ButtonsImageList.Images.SetKeyName(6, "favicon(196).ico");
		this.ButtonsImageList.Images.SetKeyName(7, "add blue.jpg");
		this.ButtonsImageList.Images.SetKeyName(8, "folder 2 ctrl bgd.png");
		//
		//ChartsTableLayoutPanel
		//
		this.ChartsTableLayoutPanel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.ChartsTableLayoutPanel.ColumnCount = 4;
		this.ChartsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.ChartsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.ChartsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.ChartsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25f));
		this.ChartsTableLayoutPanel.Controls.Add(this.ControlsDGVPanel, 0, 1);
		this.ChartsTableLayoutPanel.Controls.Add(this.Panel3, 1, 0);
		this.ChartsTableLayoutPanel.Controls.Add(this.Panel2, 2, 0);
		this.ChartsTableLayoutPanel.Controls.Add(this.Panel4, 3, 0);
		this.ChartsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ChartsTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
		this.ChartsTableLayoutPanel.Name = "ChartsTableLayoutPanel";
		this.ChartsTableLayoutPanel.RowCount = 4;
		this.ChartsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25f));
		this.ChartsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
		this.ChartsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
		this.ChartsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
		this.ChartsTableLayoutPanel.Size = new System.Drawing.Size(847, 613);
		this.ChartsTableLayoutPanel.TabIndex = 0;
		//
		//ControlsDGVPanel
		//
		this.ControlsDGVPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ControlsDGVPanel.Location = new System.Drawing.Point(1, 26);
		this.ControlsDGVPanel.Margin = new System.Windows.Forms.Padding(1);
		this.ControlsDGVPanel.Name = "ControlsDGVPanel";
		this.ControlsDGVPanel.Size = new System.Drawing.Size(209, 194);
		this.ControlsDGVPanel.TabIndex = 0;
		//
		//Panel3
		//
		this.Panel3.Controls.Add(this.EntityTB);
		this.Panel3.Controls.Add(this.Label2);
		this.Panel3.Location = new System.Drawing.Point(211, 0);
		this.Panel3.Margin = new System.Windows.Forms.Padding(0);
		this.Panel3.Name = "Panel3";
		this.Panel3.Size = new System.Drawing.Size(211, 25);
		this.Panel3.TabIndex = 3;
		//
		//EntityTB
		//
		this.EntityTB.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.EntityTB.Enabled = false;
		this.EntityTB.Location = new System.Drawing.Point(42, 3);
		this.EntityTB.MaxLength = 100;
		this.EntityTB.Name = "EntityTB";
		this.EntityTB.Size = new System.Drawing.Size(166, 20);
		this.EntityTB.TabIndex = 1;
		//
		//Label2
		//
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(3, 6);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(36, 15);
		this.Label2.TabIndex = 0;
		this.Label2.Text = "Entity";
		//
		//Panel2
		//
		this.Panel2.Controls.Add(this.VersionTB);
		this.Panel2.Controls.Add(this.Label1);
		this.Panel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Panel2.Location = new System.Drawing.Point(422, 0);
		this.Panel2.Margin = new System.Windows.Forms.Padding(0);
		this.Panel2.Name = "Panel2";
		this.Panel2.Size = new System.Drawing.Size(211, 25);
		this.Panel2.TabIndex = 2;
		//
		//VersionTB
		//
		this.VersionTB.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.VersionTB.Enabled = false;
		this.VersionTB.Location = new System.Drawing.Point(50, 3);
		this.VersionTB.MaxLength = 100;
		this.VersionTB.Name = "VersionTB";
		this.VersionTB.Size = new System.Drawing.Size(158, 20);
		this.VersionTB.TabIndex = 1;
		//
		//Label1
		//
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(3, 6);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(46, 15);
		this.Label1.TabIndex = 0;
		this.Label1.Text = "version";
		//
		//Panel4
		//
		this.Panel4.Controls.Add(this.CurrencyTB);
		this.Panel4.Controls.Add(this.Label3);
		this.Panel4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Panel4.Location = new System.Drawing.Point(633, 0);
		this.Panel4.Margin = new System.Windows.Forms.Padding(0);
		this.Panel4.Name = "Panel4";
		this.Panel4.Size = new System.Drawing.Size(214, 25);
		this.Panel4.TabIndex = 4;
		//
		//CurrencyTB
		//
		this.CurrencyTB.Enabled = false;
		this.CurrencyTB.Location = new System.Drawing.Point(58, 3);
		this.CurrencyTB.MaxLength = 100;
		this.CurrencyTB.Name = "CurrencyTB";
		this.CurrencyTB.Size = new System.Drawing.Size(62, 20);
		this.CurrencyTB.TabIndex = 1;
		//
		//Label3
		//
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(3, 6);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(55, 15);
		this.Label3.TabIndex = 0;
		this.Label3.Text = "Currency";
		//
		//EntitiesTVImageList
		//
		this.EntitiesTVImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("EntitiesTVImageList.ImageStream");
		this.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.EntitiesTVImageList.Images.SetKeyName(0, "red");
		this.EntitiesTVImageList.Images.SetKeyName(1, "green");
		//
		//TVRCM
		//
		this.TVRCM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.DisplayEntityControlsToolStripMenuItem });
		this.TVRCM.Name = "TVRCM";
		this.TVRCM.Size = new System.Drawing.Size(218, 28);
		//
		//DisplayEntityControlsToolStripMenuItem
		//
		this.DisplayEntityControlsToolStripMenuItem.Name = "DisplayEntityControlsToolStripMenuItem";
		this.DisplayEntityControlsToolStripMenuItem.Size = new System.Drawing.Size(217, 24);
		this.DisplayEntityControlsToolStripMenuItem.Text = "Display Entity Controls";
		//
		//SubmissionsControlUI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(1035, 613);
		this.Controls.Add(this.SplitContainer1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "SubmissionsControlUI";
		this.Text = "Submissions Controls";
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		this.TableLayoutPanel2.ResumeLayout(false);
		this.Panel1.ResumeLayout(false);
		this.ChartsTableLayoutPanel.ResumeLayout(false);
		this.Panel3.ResumeLayout(false);
		this.Panel3.PerformLayout();
		this.Panel2.ResumeLayout(false);
		this.Panel2.PerformLayout();
		this.Panel4.ResumeLayout(false);
		this.Panel4.PerformLayout();
		this.TVRCM.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	public System.Windows.Forms.SplitContainer SplitContainer1;
	public System.Windows.Forms.TableLayoutPanel ChartsTableLayoutPanel;
	public System.Windows.Forms.ImageList EntitiesTVImageList;
	public System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
	public System.Windows.Forms.Panel EntitiesTVPanel;
	public System.Windows.Forms.Panel ControlsDGVPanel;
	public System.Windows.Forms.ContextMenuStrip TVRCM;
	public System.Windows.Forms.ToolStripMenuItem DisplayEntityControlsToolStripMenuItem;
	public System.Windows.Forms.ImageList ButtonsImageList;
	public System.Windows.Forms.Panel Panel1;
	public System.Windows.Forms.Button RefreshBT;
	public System.Windows.Forms.Panel Panel3;
	public System.Windows.Forms.TextBox EntityTB;
	public System.Windows.Forms.Label Label2;
	public System.Windows.Forms.Panel Panel2;
	public System.Windows.Forms.TextBox VersionTB;
	public System.Windows.Forms.Label Label1;
	public System.Windows.Forms.Panel Panel4;
	public System.Windows.Forms.TextBox CurrencyTB;
	public System.Windows.Forms.Label Label3;
}

}
