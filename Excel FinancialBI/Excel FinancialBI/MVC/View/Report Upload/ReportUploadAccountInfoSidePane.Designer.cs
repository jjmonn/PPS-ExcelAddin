using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class ReportUploadAccountInfoSidePane : AddinExpress.XL.ADXExcelTaskPane
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

	private System.ComponentModel.IContainer components = null;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportUploadAccountInfoSidePane));
		this.m_accountTypeTextBox = new VIBlend.WinForms.Controls.vTextBox();
		this.m_accountTextBox = new VIBlend.WinForms.Controls.vTextBox();
		this.m_formulaTextBox = new System.Windows.Forms.TextBox();
		this.m_descriptionTextBox = new System.Windows.Forms.TextBox();
		this.VLabel1 = new VIBlend.WinForms.Controls.vLabel();
		this.VLabel2 = new VIBlend.WinForms.Controls.vLabel();
		this.VLabel3 = new VIBlend.WinForms.Controls.vLabel();
		this.VLabel4 = new VIBlend.WinForms.Controls.vLabel();
		this.SuspendLayout();
		//
		//m_accountTypeTextBox
		//
		this.m_accountTypeTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.m_accountTypeTextBox.BackColor = System.Drawing.Color.White;
		this.m_accountTypeTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
		this.m_accountTypeTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.m_accountTypeTextBox.DefaultText = "Empty...";
		this.m_accountTypeTextBox.Enabled = false;
		this.m_accountTypeTextBox.Location = new System.Drawing.Point(14, 609);
		this.m_accountTypeTextBox.MaxLength = 32767;
		this.m_accountTypeTextBox.Name = "m_accountTypeTextBox";
		this.m_accountTypeTextBox.PasswordChar = '\0';
		this.m_accountTypeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.m_accountTypeTextBox.SelectionLength = 0;
		this.m_accountTypeTextBox.SelectionStart = 0;
		this.m_accountTypeTextBox.Size = new System.Drawing.Size(219, 23);
		this.m_accountTypeTextBox.TabIndex = 15;
		this.m_accountTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.m_accountTypeTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_accountTextBox
		//
		this.m_accountTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.m_accountTextBox.BackColor = System.Drawing.Color.White;
		this.m_accountTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
		this.m_accountTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)), Convert.ToInt32(Convert.ToByte(39)));
		this.m_accountTextBox.DefaultText = "Empty...";
		this.m_accountTextBox.Enabled = false;
		this.m_accountTextBox.Location = new System.Drawing.Point(12, 42);
		this.m_accountTextBox.MaxLength = 32767;
		this.m_accountTextBox.Name = "m_accountTextBox";
		this.m_accountTextBox.PasswordChar = '\0';
		this.m_accountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
		this.m_accountTextBox.SelectionLength = 0;
		this.m_accountTextBox.SelectionStart = 0;
		this.m_accountTextBox.Size = new System.Drawing.Size(219, 23);
		this.m_accountTextBox.TabIndex = 9;
		this.m_accountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.m_accountTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_formulaTextBox
		//
		this.m_formulaTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.m_formulaTextBox.Enabled = false;
		this.m_formulaTextBox.Location = new System.Drawing.Point(12, 106);
		this.m_formulaTextBox.Multiline = true;
		this.m_formulaTextBox.Name = "m_formulaTextBox";
		this.m_formulaTextBox.Size = new System.Drawing.Size(221, 196);
		this.m_formulaTextBox.TabIndex = 16;
		//
		//m_descriptionTextBox
		//
		this.m_descriptionTextBox.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
		this.m_descriptionTextBox.Enabled = false;
		this.m_descriptionTextBox.Location = new System.Drawing.Point(14, 357);
		this.m_descriptionTextBox.Multiline = true;
		this.m_descriptionTextBox.Name = "m_descriptionTextBox";
		this.m_descriptionTextBox.Size = new System.Drawing.Size(217, 201);
		this.m_descriptionTextBox.TabIndex = 17;
		//
		//VLabel1
		//
		this.VLabel1.BackColor = System.Drawing.Color.Transparent;
		this.VLabel1.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.VLabel1.Ellipsis = false;
		this.VLabel1.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.VLabel1.Location = new System.Drawing.Point(12, 13);
		this.VLabel1.Multiline = true;
		this.VLabel1.Name = "VLabel1";
		this.VLabel1.Size = new System.Drawing.Size(92, 23);
		this.VLabel1.TabIndex = 8;
		this.VLabel1.Text = "Account";
		this.VLabel1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.VLabel1.UseMnemonics = true;
		this.VLabel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//VLabel2
		//
		this.VLabel2.BackColor = System.Drawing.Color.Transparent;
		this.VLabel2.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.VLabel2.Ellipsis = false;
		this.VLabel2.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.VLabel2.Location = new System.Drawing.Point(12, 77);
		this.VLabel2.Multiline = true;
		this.VLabel2.Name = "VLabel2";
		this.VLabel2.Size = new System.Drawing.Size(92, 23);
		this.VLabel2.TabIndex = 10;
		this.VLabel2.Text = "Formula";
		this.VLabel2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.VLabel2.UseMnemonics = true;
		this.VLabel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//VLabel3
		//
		this.VLabel3.BackColor = System.Drawing.Color.Transparent;
		this.VLabel3.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.VLabel3.Ellipsis = false;
		this.VLabel3.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.VLabel3.Location = new System.Drawing.Point(14, 328);
		this.VLabel3.Multiline = true;
		this.VLabel3.Name = "VLabel3";
		this.VLabel3.Size = new System.Drawing.Size(124, 23);
		this.VLabel3.TabIndex = 12;
		this.VLabel3.Text = "Description";
		this.VLabel3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.VLabel3.UseMnemonics = true;
		this.VLabel3.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//VLabel4
		//
		this.VLabel4.BackColor = System.Drawing.Color.Transparent;
		this.VLabel4.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.VLabel4.Ellipsis = false;
		this.VLabel4.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.VLabel4.Location = new System.Drawing.Point(14, 580);
		this.VLabel4.Multiline = true;
		this.VLabel4.Name = "VLabel4";
		this.VLabel4.Size = new System.Drawing.Size(136, 23);
		this.VLabel4.TabIndex = 14;
		this.VLabel4.Text = "Account's type";
		this.VLabel4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.VLabel4.UseMnemonics = true;
		this.VLabel4.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//ReportUploadSidePane
		//
		this.ClientSize = new System.Drawing.Size(280, 780);
		this.Controls.Add(this.m_descriptionTextBox);
		this.Controls.Add(this.m_formulaTextBox);
		this.Controls.Add(this.m_accountTypeTextBox);
		this.Controls.Add(this.VLabel4);
		this.Controls.Add(this.VLabel3);
		this.Controls.Add(this.VLabel2);
		this.Controls.Add(this.m_accountTextBox);
		this.Controls.Add(this.VLabel1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Location = new System.Drawing.Point(0, 0);
		this.Name = "ReportUploadSidePane";
		this.Text = "Account's details";
		this.ResumeLayout(false);
		this.PerformLayout();

	}
	internal VIBlend.WinForms.Controls.vTextBox m_accountTypeTextBox;
	internal VIBlend.WinForms.Controls.vTextBox m_accountTextBox;
	internal System.Windows.Forms.TextBox m_formulaTextBox;
  internal System.Windows.Forms.TextBox m_descriptionTextBox;
	internal VIBlend.WinForms.Controls.vLabel VLabel1;
	internal VIBlend.WinForms.Controls.vLabel VLabel2;
	internal VIBlend.WinForms.Controls.vLabel VLabel3;

	internal VIBlend.WinForms.Controls.vLabel VLabel4;
}

}
