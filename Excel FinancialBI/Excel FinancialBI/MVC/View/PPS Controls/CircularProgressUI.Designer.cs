using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class CircularProgressUI : System.Windows.Forms.Form
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CircularProgressUI));
		this.CP = new ProgressControls.ProgressIndicator();
		this.Label1 = new System.Windows.Forms.Label();
		this.SuspendLayout();
		//
		//CP
		//
		this.CP.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.CP.CircleColor = System.Drawing.Color.Purple;
		this.CP.CircleSize = 0.7f;
		this.CP.Location = new System.Drawing.Point(56, 28);
		this.CP.Name = "CP";
		this.CP.NumberOfCircles = 12;
		this.CP.Percentage = 0f;
		this.CP.Size = new System.Drawing.Size(79, 79);
		this.CP.TabIndex = 0;
		this.CP.Text = "ProgressIndicator1";
		//
		//Label1
		//
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(67, 128);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(62, 15);
		this.Label1.TabIndex = 1;
		this.Label1.Text = "Initializing";
		this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		//
		//CircularProgressUI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
		this.ClientSize = new System.Drawing.Size(190, 164);
		this.Controls.Add(this.Label1);
		this.Controls.Add(this.CP);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "CircularProgressUI";
		this.ResumeLayout(false);
		this.PerformLayout();

	}
	internal ProgressControls.ProgressIndicator CP;
	internal System.Windows.Forms.Label Label1;
}

}
