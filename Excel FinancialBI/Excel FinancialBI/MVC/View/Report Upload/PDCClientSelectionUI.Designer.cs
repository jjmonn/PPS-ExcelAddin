using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class PDCClientSelectionUI : System.Windows.Forms.Form
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDCClientSelectionUI));
		this.m_clientsTreeview = new VIBlend.WinForms.Controls.vTreeView();
		this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.m_validateButton = new VIBlend.WinForms.Controls.vButton();
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.TableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		//
		//m_clientsTreeview
		//
		this.m_clientsTreeview.AccessibleName = "TreeView";
		this.m_clientsTreeview.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
		this.m_clientsTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_clientsTreeview.Location = new System.Drawing.Point(3, 3);
		this.m_clientsTreeview.Name = "m_clientsTreeview";
		this.m_clientsTreeview.ScrollPosition = new System.Drawing.Point(0, 0);
		this.m_clientsTreeview.SelectedNode = null;
		this.m_clientsTreeview.Size = new System.Drawing.Size(331, 344);
		this.m_clientsTreeview.TabIndex = 0;
		this.m_clientsTreeview.Text = "VTreeView1";
		this.m_clientsTreeview.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK;
		this.m_clientsTreeview.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICEBLACK;
		//
		//TableLayoutPanel1
		//
		this.TableLayoutPanel1.ColumnCount = 1;
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.Controls.Add(this.m_clientsTreeview, 0, 0);
		this.TableLayoutPanel1.Controls.Add(this.m_validateButton, 0, 1);
		this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.TableLayoutPanel1.Name = "TableLayoutPanel1";
		this.TableLayoutPanel1.RowCount = 2;
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40f));
		this.TableLayoutPanel1.Size = new System.Drawing.Size(337, 390);
		this.TableLayoutPanel1.TabIndex = 1;
		//
		//m_validateButton
		//
		this.m_validateButton.AllowAnimations = true;
		this.m_validateButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
		this.m_validateButton.BackColor = System.Drawing.Color.Transparent;
		this.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_validateButton.ImageKey = "submit.png";
		this.m_validateButton.ImageList = this.ImageList1;
		this.m_validateButton.Location = new System.Drawing.Point(234, 353);
		this.m_validateButton.Name = "m_validateButton";
		this.m_validateButton.RoundedCornersMask = Convert.ToByte(15);
		this.m_validateButton.Size = new System.Drawing.Size(100, 30);
		this.m_validateButton.TabIndex = 1;
		this.m_validateButton.Text = "Validate";
		this.m_validateButton.UseVisualStyleBackColor = false;
		this.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//ImageList1
		//
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "submit.png");
		//
		//PDCClientSelectionUI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(337, 390);
		this.Controls.Add(this.TableLayoutPanel1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "PDCClientSelectionUI";
		this.Text = "Client selection";
		this.TableLayoutPanel1.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	internal VIBlend.WinForms.Controls.vTreeView m_clientsTreeview;
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	internal VIBlend.WinForms.Controls.vButton m_validateButton;
	internal System.Windows.Forms.ImageList ImageList1;
}

}
