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
		this.columnsDisplayList = new VIBlend.WinForms.Controls.vListBox();
		this.m_rowsLabel = new System.Windows.Forms.Label();
		this.rowsDisplayList = new VIBlend.WinForms.Controls.vListBox();
		this.UpdateBT = new System.Windows.Forms.Button();
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.DimensionsTVPanel = new System.Windows.Forms.Panel();
		this.Panel1 = new System.Windows.Forms.Panel();
		this.CollapseRightPaneBT = new VIBlend.WinForms.Controls.vButton();
		this.m_fieldChoiceLabel = new System.Windows.Forms.Label();
		this.TableLayoutPanel1.SuspendLayout();
		this.TableLayoutPanel2.SuspendLayout();
		this.Panel1.SuspendLayout();
		this.SuspendLayout();
		//
		//TableLayoutPanel1
		//
		this.TableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
		this.TableLayoutPanel1.ColumnCount = 1;
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.Controls.Add(this.TableLayoutPanel2, 0, 2);
		this.TableLayoutPanel1.Controls.Add(this.DimensionsTVPanel, 0, 1);
		this.TableLayoutPanel1.Controls.Add(this.Panel1, 0, 0);
		this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.TableLayoutPanel1.Name = "TableLayoutPanel1";
		this.TableLayoutPanel1.RowCount = 3;
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.63511f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.36489f));
		this.TableLayoutPanel1.Size = new System.Drawing.Size(277, 616);
		this.TableLayoutPanel1.TabIndex = 1;
		//
		//TableLayoutPanel2
		//
		this.TableLayoutPanel2.ColumnCount = 2;
		this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.TableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.TableLayoutPanel2.Controls.Add(this.m_columnsLabel, 1, 0);
		this.TableLayoutPanel2.Controls.Add(this.columnsDisplayList, 1, 1);
		this.TableLayoutPanel2.Controls.Add(this.m_rowsLabel, 0, 0);
		this.TableLayoutPanel2.Controls.Add(this.rowsDisplayList, 0, 1);
		this.TableLayoutPanel2.Controls.Add(this.UpdateBT, 1, 2);
		this.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel2.Location = new System.Drawing.Point(3, 337);
		this.TableLayoutPanel2.Name = "TableLayoutPanel2";
		this.TableLayoutPanel2.RowCount = 3;
		this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.44884f));
		this.TableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.55116f));
		this.TableLayoutPanel2.Size = new System.Drawing.Size(271, 276);
		this.TableLayoutPanel2.TabIndex = 5;
		//
		//m_columnsLabel
		//
		this.m_columnsLabel.AutoSize = true;
		this.m_columnsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_columnsLabel.ForeColor = System.Drawing.Color.Black;
		this.m_columnsLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_columnsLabel.ImageKey = "table_selection_column.ico";
		this.m_columnsLabel.ImageList = this.ImageList2;
		this.m_columnsLabel.Location = new System.Drawing.Point(138, 0);
		this.m_columnsLabel.Name = "m_columnsLabel";
		this.m_columnsLabel.Size = new System.Drawing.Size(130, 20);
		this.m_columnsLabel.TabIndex = 3;
		this.m_columnsLabel.Text = FBI.Utils.Local.GetValue("CUI.columns_label");
		this.m_columnsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		//
		//ImageList2
		//
		this.ImageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList2.ImageStream");
		this.ImageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList2.Images.SetKeyName(0, "Close_Box_Red.png");
		this.ImageList2.Images.SetKeyName(1, "table_selection_column.ico");
		this.ImageList2.Images.SetKeyName(2, "table_selection_row.ico");
		//
		//columnsDisplayList
		//
		this.columnsDisplayList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.columnsDisplayList.Location = new System.Drawing.Point(138, 23);
		this.columnsDisplayList.Name = "columnsDisplayList";
		this.columnsDisplayList.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.columnsDisplayList.Size = new System.Drawing.Size(130, 220);
		this.columnsDisplayList.TabIndex = 5;
		this.columnsDisplayList.Text = "VListBox1";
		this.columnsDisplayList.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
		this.columnsDisplayList.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
		//
		//m_rowsLabel
		//
		this.m_rowsLabel.AutoSize = true;
		this.m_rowsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_rowsLabel.ForeColor = System.Drawing.Color.Black;
		this.m_rowsLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_rowsLabel.ImageKey = "table_selection_row.ico";
		this.m_rowsLabel.ImageList = this.ImageList2;
		this.m_rowsLabel.Location = new System.Drawing.Point(3, 0);
		this.m_rowsLabel.Name = "m_rowsLabel";
		this.m_rowsLabel.Size = new System.Drawing.Size(129, 20);
		this.m_rowsLabel.TabIndex = 4;
    this.m_rowsLabel.Text = FBI.Utils.Local.GetValue("CUI.rows_label");
		this.m_rowsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		//
		//rowsDisplayList
		//
		this.rowsDisplayList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rowsDisplayList.Location = new System.Drawing.Point(3, 23);
		this.rowsDisplayList.Name = "rowsDisplayList";
		this.rowsDisplayList.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.rowsDisplayList.Size = new System.Drawing.Size(129, 220);
		this.rowsDisplayList.TabIndex = 6;
		this.rowsDisplayList.Text = "VListBox1";
		this.rowsDisplayList.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
		this.rowsDisplayList.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
		//
		//UpdateBT
		//
		this.UpdateBT.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.UpdateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.UpdateBT.ImageKey = "Refresh DB 24.ico";
		this.UpdateBT.ImageList = this.ImageList1;
		this.UpdateBT.Location = new System.Drawing.Point(193, 249);
		this.UpdateBT.Name = "UpdateBT";
		this.UpdateBT.Size = new System.Drawing.Size(75, 24);
		this.UpdateBT.TabIndex = 0;
    this.UpdateBT.Text = FBI.Utils.Local.GetValue("CUI.refresh");
		this.UpdateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.UpdateBT.UseVisualStyleBackColor = true;
		//
		//ImageList1
		//
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "Close_Box_Red.png");
		this.ImageList1.Images.SetKeyName(1, "Refresh DB 24.ico");
		//
		//DimensionsTVPanel
		//
		this.DimensionsTVPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DimensionsTVPanel.Location = new System.Drawing.Point(3, 38);
		this.DimensionsTVPanel.Name = "DimensionsTVPanel";
		this.DimensionsTVPanel.Size = new System.Drawing.Size(271, 293);
		this.DimensionsTVPanel.TabIndex = 7;
		//
		//Panel1
		//
		this.Panel1.Controls.Add(this.CollapseRightPaneBT);
		this.Panel1.Controls.Add(this.m_fieldChoiceLabel);
		this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Panel1.Location = new System.Drawing.Point(3, 3);
		this.Panel1.Name = "Panel1";
		this.Panel1.Size = new System.Drawing.Size(271, 29);
		this.Panel1.TabIndex = 8;
		//
		//CollapseRightPaneBT
		//
		this.CollapseRightPaneBT.AllowAnimations = true;
		this.CollapseRightPaneBT.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.CollapseRightPaneBT.BackColor = System.Drawing.Color.Transparent;
		this.CollapseRightPaneBT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.CollapseRightPaneBT.Location = new System.Drawing.Point(249, 3);
		this.CollapseRightPaneBT.Name = "CollapseRightPaneBT";
		this.CollapseRightPaneBT.PaintBorder = false;
		this.CollapseRightPaneBT.RoundedCornersMask = Convert.ToByte(15);
		this.CollapseRightPaneBT.Size = new System.Drawing.Size(19, 19);
		this.CollapseRightPaneBT.TabIndex = 7;
		this.CollapseRightPaneBT.UseVisualStyleBackColor = false;
		this.CollapseRightPaneBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_fieldChoiceLabel
		//
		this.m_fieldChoiceLabel.AutoSize = true;
		this.m_fieldChoiceLabel.ForeColor = System.Drawing.Color.Black;
		this.m_fieldChoiceLabel.Location = new System.Drawing.Point(7, 11);
		this.m_fieldChoiceLabel.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
		this.m_fieldChoiceLabel.Name = "m_fieldChoiceLabel";
		this.m_fieldChoiceLabel.Size = new System.Drawing.Size(169, 15);
		this.m_fieldChoiceLabel.TabIndex = 6;
    this.m_fieldChoiceLabel.Text = FBI.Utils.Local.GetValue("CUI.fields_choice");
		//
		//CUI2RightPane
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(207)), Convert.ToInt32(Convert.ToByte(212)), Convert.ToInt32(Convert.ToByte(221)));
		this.Controls.Add(this.TableLayoutPanel1);
		this.Name = "CUI2RightPane";
		this.Size = new System.Drawing.Size(277, 616);
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
	public System.Windows.Forms.Panel DimensionsTVPanel;
	public System.Windows.Forms.Button UpdateBT;
	public System.Windows.Forms.ImageList ImageList1;
	public VIBlend.WinForms.Controls.vListBox columnsDisplayList;
	public VIBlend.WinForms.Controls.vListBox rowsDisplayList;
	public System.Windows.Forms.Panel Panel1;
	public System.Windows.Forms.ImageList ImageList2;
	public VIBlend.WinForms.Controls.vButton CollapseRightPaneBT;

	public System.Windows.Forms.Label m_rowsLabel;
}

}
