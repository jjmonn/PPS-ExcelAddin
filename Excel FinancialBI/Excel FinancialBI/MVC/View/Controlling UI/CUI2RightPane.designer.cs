using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class CUI2RightPane : System.Windows.Forms.UserControl
{

	//UserControl1 overrides dispose to clean up the component list.
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CUI2RightPane));
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.TableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.m_columnsLabel = new System.Windows.Forms.Label();
      this.ImageList2 = new System.Windows.Forms.ImageList(this.components);
      this.m_columnsDisplayList = new VIBlend.WinForms.Controls.vListBox();
      this.m_rowsLabel = new System.Windows.Forms.Label();
      this.m_rowsDisplayList = new VIBlend.WinForms.Controls.vListBox();
      this.m_updateBT = new System.Windows.Forms.Button();
      this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
      this.m_dimensionsTVPanel = new System.Windows.Forms.Panel();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.CollapseRightPaneBT = new VIBlend.WinForms.Controls.vButton();
      this.m_fieldChoiceLabel = new System.Windows.Forms.Label();
      this.TableLayoutPanel1.SuspendLayout();
      this.TableLayoutPanel2.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
      this.TableLayoutPanel1.ColumnCount = 1;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Controls.Add(this.TableLayoutPanel2, 0, 2);
      this.TableLayoutPanel1.Controls.Add(this.m_dimensionsTVPanel, 0, 1);
      this.TableLayoutPanel1.Controls.Add(this.Panel1, 0, 0);
      this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.63511F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.36489F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(331, 777);
      this.TableLayoutPanel1.TabIndex = 1;
      // 
      // TableLayoutPanel2
      // 
      this.TableLayoutPanel2.ColumnCount = 2;
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.TableLayoutPanel2.Controls.Add(this.m_columnsLabel, 1, 0);
      this.TableLayoutPanel2.Controls.Add(this.m_columnsDisplayList, 1, 1);
      this.TableLayoutPanel2.Controls.Add(this.m_rowsLabel, 0, 0);
      this.TableLayoutPanel2.Controls.Add(this.m_rowsDisplayList, 0, 1);
      this.TableLayoutPanel2.Controls.Add(this.m_updateBT, 1, 2);
      this.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel2.Location = new System.Drawing.Point(3, 421);
      this.TableLayoutPanel2.Name = "TableLayoutPanel2";
      this.TableLayoutPanel2.RowCount = 3;
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.44884F));
      this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.55116F));
      this.TableLayoutPanel2.Size = new System.Drawing.Size(325, 353);
      this.TableLayoutPanel2.TabIndex = 5;
      // 
      // m_columnsLabel
      // 
      this.m_columnsLabel.AutoSize = true;
      this.m_columnsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_columnsLabel.ForeColor = System.Drawing.Color.Black;
      this.m_columnsLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_columnsLabel.ImageKey = "table_selection_column.ico";
      this.m_columnsLabel.ImageList = this.ImageList2;
      this.m_columnsLabel.Location = new System.Drawing.Point(165, 0);
      this.m_columnsLabel.Name = "m_columnsLabel";
      this.m_columnsLabel.Size = new System.Drawing.Size(157, 20);
      this.m_columnsLabel.TabIndex = 3;
      this.m_columnsLabel.Text = "[CUI.columns_label]";
      this.m_columnsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // ImageList2
      // 
      this.ImageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList2.ImageStream")));
      this.ImageList2.TransparentColor = System.Drawing.Color.Transparent;
      this.ImageList2.Images.SetKeyName(0, "Close_Box_Red.png");
      this.ImageList2.Images.SetKeyName(1, "table_selection_column.ico");
      this.ImageList2.Images.SetKeyName(2, "table_selection_row.ico");
      // 
      // m_columnsDisplayList
      // 
      this.m_columnsDisplayList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_columnsDisplayList.Location = new System.Drawing.Point(165, 23);
      this.m_columnsDisplayList.Name = "m_columnsDisplayList";
      this.m_columnsDisplayList.RoundedCornersMaskListItem = ((byte)(15));
      this.m_columnsDisplayList.Size = new System.Drawing.Size(157, 288);
      this.m_columnsDisplayList.TabIndex = 5;
      this.m_columnsDisplayList.Text = "VListBox1";
      this.m_columnsDisplayList.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_columnsDisplayList.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_rowsLabel
      // 
      this.m_rowsLabel.AutoSize = true;
      this.m_rowsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_rowsLabel.ForeColor = System.Drawing.Color.Black;
      this.m_rowsLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_rowsLabel.ImageKey = "table_selection_row.ico";
      this.m_rowsLabel.ImageList = this.ImageList2;
      this.m_rowsLabel.Location = new System.Drawing.Point(3, 0);
      this.m_rowsLabel.Name = "m_rowsLabel";
      this.m_rowsLabel.Size = new System.Drawing.Size(156, 20);
      this.m_rowsLabel.TabIndex = 4;
      this.m_rowsLabel.Text = "[CUI.rows_label]";
      this.m_rowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // m_rowsDisplayList
      // 
      this.m_rowsDisplayList.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_rowsDisplayList.Location = new System.Drawing.Point(3, 23);
      this.m_rowsDisplayList.Name = "m_rowsDisplayList";
      this.m_rowsDisplayList.RoundedCornersMaskListItem = ((byte)(15));
      this.m_rowsDisplayList.Size = new System.Drawing.Size(156, 288);
      this.m_rowsDisplayList.TabIndex = 6;
      this.m_rowsDisplayList.Text = "VListBox1";
      this.m_rowsDisplayList.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.m_rowsDisplayList.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_updateBT
      // 
      this.m_updateBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.m_updateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_updateBT.ImageKey = "Refresh DB 24.ico";
      this.m_updateBT.ImageList = this.ImageList1;
      this.m_updateBT.Location = new System.Drawing.Point(210, 317);
      this.m_updateBT.Name = "m_updateBT";
      this.m_updateBT.Size = new System.Drawing.Size(112, 24);
      this.m_updateBT.TabIndex = 0;
      this.m_updateBT.Text = "[CUI.refresh]";
      this.m_updateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_updateBT.UseVisualStyleBackColor = true;
      // 
      // ImageList1
      // 
      this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
      this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.ImageList1.Images.SetKeyName(0, "Close_Box_Red.png");
      this.ImageList1.Images.SetKeyName(1, "Refresh DB 24.ico");
      // 
      // m_dimensionsTVPanel
      // 
      this.m_dimensionsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_dimensionsTVPanel.Location = new System.Drawing.Point(3, 38);
      this.m_dimensionsTVPanel.Name = "m_dimensionsTVPanel";
      this.m_dimensionsTVPanel.Size = new System.Drawing.Size(325, 377);
      this.m_dimensionsTVPanel.TabIndex = 7;
      // 
      // Panel1
      // 
      this.Panel1.Controls.Add(this.CollapseRightPaneBT);
      this.Panel1.Controls.Add(this.m_fieldChoiceLabel);
      this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Panel1.Location = new System.Drawing.Point(3, 3);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(325, 29);
      this.Panel1.TabIndex = 8;
      // 
      // CollapseRightPaneBT
      // 
      this.CollapseRightPaneBT.AllowAnimations = true;
      this.CollapseRightPaneBT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.CollapseRightPaneBT.BackColor = System.Drawing.Color.Transparent;
      this.CollapseRightPaneBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.CollapseRightPaneBT.Location = new System.Drawing.Point(303, 3);
      this.CollapseRightPaneBT.Name = "CollapseRightPaneBT";
      this.CollapseRightPaneBT.PaintBorder = false;
      this.CollapseRightPaneBT.RoundedCornersMask = ((byte)(15));
      this.CollapseRightPaneBT.Size = new System.Drawing.Size(19, 19);
      this.CollapseRightPaneBT.TabIndex = 7;
      this.CollapseRightPaneBT.UseVisualStyleBackColor = false;
      this.CollapseRightPaneBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_fieldChoiceLabel
      // 
      this.m_fieldChoiceLabel.AutoSize = true;
      this.m_fieldChoiceLabel.ForeColor = System.Drawing.Color.Black;
      this.m_fieldChoiceLabel.Location = new System.Drawing.Point(7, 11);
      this.m_fieldChoiceLabel.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
      this.m_fieldChoiceLabel.Name = "m_fieldChoiceLabel";
      this.m_fieldChoiceLabel.Size = new System.Drawing.Size(108, 15);
      this.m_fieldChoiceLabel.TabIndex = 6;
      this.m_fieldChoiceLabel.Text = "[CUI.fields_choice]";
      // 
      // CUI2RightPane
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.Controls.Add(this.TableLayoutPanel1);
      this.Name = "CUI2RightPane";
      this.Size = new System.Drawing.Size(331, 777);
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel2.ResumeLayout(false);
      this.TableLayoutPanel2.PerformLayout();
      this.Panel1.ResumeLayout(false);
      this.Panel1.PerformLayout();
      this.ResumeLayout(false);

	}
	public System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	public System.Windows.Forms.TableLayoutPanel TableLayoutPanel2;
	public System.Windows.Forms.Label m_columnsLabel;
	public System.Windows.Forms.Label m_fieldChoiceLabel;
	public System.Windows.Forms.Panel m_dimensionsTVPanel;
	public System.Windows.Forms.Button m_updateBT;
	public System.Windows.Forms.ImageList ImageList1;
	public VIBlend.WinForms.Controls.vListBox m_columnsDisplayList;
	public VIBlend.WinForms.Controls.vListBox m_rowsDisplayList;
	public System.Windows.Forms.Panel Panel1;
	public System.Windows.Forms.ImageList ImageList2;
	public VIBlend.WinForms.Controls.vButton CollapseRightPaneBT;

	public System.Windows.Forms.Label m_rowsLabel;
}

}
