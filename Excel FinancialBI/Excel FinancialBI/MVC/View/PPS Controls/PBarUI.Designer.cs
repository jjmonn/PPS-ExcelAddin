using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class PBarUI : System.Windows.Forms.Form
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PBarUI));
		this.ProgressBarControl1 = new ProgressBarControl();
		this.Label1 = new System.Windows.Forms.Label();
		this.SuspendLayout();
		//
		//ProgressBarControl1
		//
		this.ProgressBarControl1.Location = new System.Drawing.Point(42, 30);
		this.ProgressBarControl1.Name = "ProgressBarControl1";
		this.ProgressBarControl1.Size = new System.Drawing.Size(302, 11);
		this.ProgressBarControl1.TabIndex = 0;
		this.ProgressBarControl1.Visible = false;
		//
		//Label1
		//
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(96, 59);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(0, 13);
		this.Label1.TabIndex = 1;
		//
		//PBarUI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(384, 81);
		this.Controls.Add(this.Label1);
		this.Controls.Add(this.ProgressBarControl1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "PBarUI";
		this.Text = "Uploading...";
		this.ResumeLayout(false);
		this.PerformLayout();

	}
	internal ProgressBarControl ProgressBarControl1;
	internal System.Windows.Forms.Label Label1;
}

}
