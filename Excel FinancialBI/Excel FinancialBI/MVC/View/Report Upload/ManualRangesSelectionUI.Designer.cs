using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class ManualRangesSelectionUI : System.Windows.Forms.Form
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualRangesSelectionUI));
		this.Label1 = new System.Windows.Forms.Label();
		this.PeriodsEditBT = new System.Windows.Forms.Button();
		this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
		this.PeriodsRefEdit = new System.Windows.Forms.TextBox();
		this.Label4 = new System.Windows.Forms.Label();
		this.AccountsEditBT = new System.Windows.Forms.Button();
		this.AccountsRefEdit = new System.Windows.Forms.TextBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.EntitiesEditBT = new System.Windows.Forms.Button();
		this.EntitiesRefEdit = new System.Windows.Forms.TextBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.Validate_Cmd = new System.Windows.Forms.Button();
		this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.TableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		//
		//Label1
		//
		this.Label1.AutoSize = true;
		this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
		this.Label1.ForeColor = System.Drawing.SystemColors.GrayText;
		this.Label1.Location = new System.Drawing.Point(11, 26);
		this.Label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(453, 15);
		this.Label1.TabIndex = 5;
		this.Label1.Text = "Select the ranges corresponding to the Entities, Accounts and Periods";
		//
		//PeriodsEditBT
		//
		this.PeriodsEditBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
		this.PeriodsEditBT.ImageIndex = 0;
		this.PeriodsEditBT.ImageList = this.ButtonsImageList;
		this.PeriodsEditBT.Location = new System.Drawing.Point(525, 112);
		this.PeriodsEditBT.Margin = new System.Windows.Forms.Padding(2);
		this.PeriodsEditBT.Name = "PeriodsEditBT";
		this.PeriodsEditBT.Size = new System.Drawing.Size(27, 27);
		this.PeriodsEditBT.TabIndex = 8;
		this.PeriodsEditBT.UseVisualStyleBackColor = true;
		//
		//ButtonsImageList
		//
		this.ButtonsImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonsImageList.ImageStream");
		this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
		this.ButtonsImageList.Images.SetKeyName(0, "favicon(161).ico");
		this.ButtonsImageList.Images.SetKeyName(1, "favicon(132).ico");
		this.ButtonsImageList.Images.SetKeyName(2, "favicon(76).ico");
		//
		//PeriodsRefEdit
		//
		this.PeriodsRefEdit.Location = new System.Drawing.Point(182, 112);
		this.PeriodsRefEdit.Margin = new System.Windows.Forms.Padding(2);
		this.PeriodsRefEdit.Name = "PeriodsRefEdit";
		this.PeriodsRefEdit.Size = new System.Drawing.Size(261, 20);
		this.PeriodsRefEdit.TabIndex = 7;
		//
		//Label4
		//
		this.Label4.AutoSize = true;
		this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label4.Location = new System.Drawing.Point(2, 110);
		this.Label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(116, 13);
		this.Label4.TabIndex = 4;
		this.Label4.Text = "Select Period(s) Range";
		//
		//AccountsEditBT
		//
		this.AccountsEditBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
		this.AccountsEditBT.ImageIndex = 0;
		this.AccountsEditBT.ImageList = this.ButtonsImageList;
		this.AccountsEditBT.Location = new System.Drawing.Point(525, 2);
		this.AccountsEditBT.Margin = new System.Windows.Forms.Padding(2);
		this.AccountsEditBT.Name = "AccountsEditBT";
		this.AccountsEditBT.Size = new System.Drawing.Size(27, 27);
		this.AccountsEditBT.TabIndex = 6;
		this.AccountsEditBT.UseVisualStyleBackColor = true;
		//
		//AccountsRefEdit
		//
		this.AccountsRefEdit.Location = new System.Drawing.Point(182, 2);
		this.AccountsRefEdit.Margin = new System.Windows.Forms.Padding(2);
		this.AccountsRefEdit.Name = "AccountsRefEdit";
		this.AccountsRefEdit.Size = new System.Drawing.Size(261, 20);
		this.AccountsRefEdit.TabIndex = 5;
		//
		//Label2
		//
		this.Label2.AutoSize = true;
		this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label2.Location = new System.Drawing.Point(2, 0);
		this.Label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(126, 13);
		this.Label2.TabIndex = 2;
		this.Label2.Text = "Select Account(s) Range";
		//
		//EntitiesEditBT
		//
		this.EntitiesEditBT.FlatAppearance.BorderColor = System.Drawing.Color.White;
		this.EntitiesEditBT.ImageIndex = 0;
		this.EntitiesEditBT.ImageList = this.ButtonsImageList;
		this.EntitiesEditBT.Location = new System.Drawing.Point(525, 57);
		this.EntitiesEditBT.Margin = new System.Windows.Forms.Padding(2);
		this.EntitiesEditBT.Name = "EntitiesEditBT";
		this.EntitiesEditBT.Size = new System.Drawing.Size(27, 27);
		this.EntitiesEditBT.TabIndex = 8;
		this.EntitiesEditBT.UseVisualStyleBackColor = true;
		//
		//EntitiesRefEdit
		//
		this.EntitiesRefEdit.Location = new System.Drawing.Point(182, 57);
		this.EntitiesRefEdit.Margin = new System.Windows.Forms.Padding(2);
		this.EntitiesRefEdit.Name = "EntitiesRefEdit";
		this.EntitiesRefEdit.Size = new System.Drawing.Size(261, 20);
		this.EntitiesRefEdit.TabIndex = 7;
		//
		//Label3
		//
		this.Label3.AutoSize = true;
		this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
		this.Label3.Location = new System.Drawing.Point(2, 55);
		this.Label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(114, 13);
		this.Label3.TabIndex = 4;
		this.Label3.Text = "Select Entitiy(s) Range";
		//
		//Validate_Cmd
		//
		this.Validate_Cmd.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
		this.Validate_Cmd.BackColor = System.Drawing.Color.SkyBlue;
		this.Validate_Cmd.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
		this.Validate_Cmd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.PaleTurquoise;
		this.Validate_Cmd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DodgerBlue;
		this.Validate_Cmd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Validate_Cmd.ImageKey = "favicon(76).ico";
		this.Validate_Cmd.ImageList = this.ButtonsImageList;
		this.Validate_Cmd.Location = new System.Drawing.Point(491, 266);
		this.Validate_Cmd.Margin = new System.Windows.Forms.Padding(2);
		this.Validate_Cmd.Name = "Validate_Cmd";
		this.Validate_Cmd.Size = new System.Drawing.Size(100, 25);
		this.Validate_Cmd.TabIndex = 3;
		this.Validate_Cmd.Text = "Validate";
		this.Validate_Cmd.UseVisualStyleBackColor = true;
		//
		//TableLayoutPanel1
		//
		this.TableLayoutPanel1.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
		this.TableLayoutPanel1.ColumnCount = 3;
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.45122f));
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.54878f));
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38f));
		this.TableLayoutPanel1.Controls.Add(this.AccountsRefEdit, 1, 0);
		this.TableLayoutPanel1.Controls.Add(this.Label2, 0, 0);
		this.TableLayoutPanel1.Controls.Add(this.AccountsEditBT, 2, 0);
		this.TableLayoutPanel1.Controls.Add(this.Label4, 0, 2);
		this.TableLayoutPanel1.Controls.Add(this.PeriodsRefEdit, 1, 2);
		this.TableLayoutPanel1.Controls.Add(this.PeriodsEditBT, 2, 2);
		this.TableLayoutPanel1.Controls.Add(this.EntitiesEditBT, 2, 1);
		this.TableLayoutPanel1.Controls.Add(this.EntitiesRefEdit, 1, 1);
		this.TableLayoutPanel1.Controls.Add(this.Label3, 0, 1);
		this.TableLayoutPanel1.Location = new System.Drawing.Point(35, 75);
		this.TableLayoutPanel1.Name = "TableLayoutPanel1";
		this.TableLayoutPanel1.RowCount = 3;
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel1.Size = new System.Drawing.Size(562, 166);
		this.TableLayoutPanel1.TabIndex = 7;
		//
		//ManualRangesSelectionUI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.Control;
		this.ClientSize = new System.Drawing.Size(629, 316);
		this.Controls.Add(this.TableLayoutPanel1);
		this.Controls.Add(this.Validate_Cmd);
		this.Controls.Add(this.Label1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Margin = new System.Windows.Forms.Padding(2);
		this.Name = "ManualRangesSelectionUI";
		this.Text = "Input Ranges Edition";
		this.TableLayoutPanel1.ResumeLayout(false);
		this.TableLayoutPanel1.PerformLayout();
		this.ResumeLayout(false);
		this.PerformLayout();

	}
	internal System.Windows.Forms.Label Label1;
	internal System.Windows.Forms.Button Validate_Cmd;
	internal System.Windows.Forms.Label Label4;
	internal System.Windows.Forms.Label Label2;
	internal System.Windows.Forms.Label Label3;
	internal System.Windows.Forms.Button PeriodsEditBT;
	internal System.Windows.Forms.TextBox PeriodsRefEdit;
	internal System.Windows.Forms.Button AccountsEditBT;
	internal System.Windows.Forms.TextBox AccountsRefEdit;
	internal System.Windows.Forms.Button EntitiesEditBT;
	internal System.Windows.Forms.TextBox EntitiesRefEdit;
	internal System.Windows.Forms.ImageList ButtonsImageList;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
}

}
