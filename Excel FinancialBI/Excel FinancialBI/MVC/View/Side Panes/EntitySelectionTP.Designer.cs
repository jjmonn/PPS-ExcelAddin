using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class EntitySelectionTP : AddinExpress.XL.ADXExcelTaskPane
{

	//Form overrides dispose to clean up the component list.
	[System.Diagnostics.DebuggerNonUserCode()]
	protected override void Dispose(bool disposing)
	{
		if (disposing) {
			if ((components != null)) {
				components.Dispose();
			}
		}
		base.Dispose(disposing);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntitySelectionTP));
		this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.ValidateBT = new System.Windows.Forms.Button();
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.EntitiesTVImageList = new System.Windows.Forms.ImageList(this.components);
		this.TableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		//
		//TableLayoutPanel1
		//
		this.TableLayoutPanel1.ColumnCount = 1;
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
		this.TableLayoutPanel1.Controls.Add(this.ValidateBT, 0, 2);
		this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.TableLayoutPanel1.Name = "TableLayoutPanel1";
		this.TableLayoutPanel1.RowCount = 3;
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.385735f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.61427f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60f));
		this.TableLayoutPanel1.Size = new System.Drawing.Size(271, 708);
		this.TableLayoutPanel1.TabIndex = 0;
		//
		//ValidateBT
		//
		this.ValidateBT.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.ValidateBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.ValidateBT.ImageKey = "1420498403_340208.ico";
		this.ValidateBT.ImageList = this.ImageList1;
		this.ValidateBT.Location = new System.Drawing.Point(184, 663);
		this.ValidateBT.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
		this.ValidateBT.Name = "ValidateBT";
		this.ValidateBT.Size = new System.Drawing.Size(84, 25);
		this.ValidateBT.TabIndex = 3;
		this.ValidateBT.Text = "Validate";
		this.ValidateBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.ValidateBT.UseVisualStyleBackColor = true;
		//
		//ImageList1
		//
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico");
		//
		//EntitiesTVImageList
		//
		this.EntitiesTVImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("EntitiesTVImageList.ImageStream");
		this.EntitiesTVImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.EntitiesTVImageList.Images.SetKeyName(0, "favicon(81).ico");
		this.EntitiesTVImageList.Images.SetKeyName(1, "config purple circle small.png");
		//
		//EntitySelectionTP
		//
		this.ClientSize = new System.Drawing.Size(271, 708);
		this.Controls.Add(this.TableLayoutPanel1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Location = new System.Drawing.Point(0, 0);
		this.Name = "EntitySelectionTP";
		this.Text = "Entity Selection";
		this.TableLayoutPanel1.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	internal System.Windows.Forms.Button ValidateBT;
	internal System.Windows.Forms.ImageList ImageList1;

	internal System.Windows.Forms.ImageList EntitiesTVImageList;
}

}
