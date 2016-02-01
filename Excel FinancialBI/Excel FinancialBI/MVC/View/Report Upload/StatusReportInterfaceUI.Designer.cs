using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class StatusReportInterfaceUI : System.Windows.Forms.Form
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

	private System.ComponentModel.IContainer components = null;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.  
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatusReportInterfaceUI));
		this.m_errorsListBox = new VIBlend.WinForms.Controls.vListBox();
		this.SuspendLayout();
		//
		//m_errorsListBox
		//
		this.m_errorsListBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.m_errorsListBox.Location = new System.Drawing.Point(0, 0);
		this.m_errorsListBox.Name = "m_errorsListBox";
		this.m_errorsListBox.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.m_errorsListBox.Size = new System.Drawing.Size(423, 187);
		this.m_errorsListBox.TabIndex = 0;
		this.m_errorsListBox.Text = "VListBox1";
		this.m_errorsListBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.m_errorsListBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//StatusReportInterfaceUI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(423, 187);
		this.Controls.Add(this.m_errorsListBox);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "StatusReportInterfaceUI";
		this.Text = "Upload Error Messages";
		this.ResumeLayout(false);

	}
	internal VIBlend.WinForms.Controls.vListBox m_errorsListBox;
}

}
