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
      this.Label1 = new System.Windows.Forms.Label();
      this.vCircularProgressBar1 = new VIBlend.WinForms.Controls.vCircularProgressBar();
      this.SuspendLayout();
      // 
      // Label1
      // 
      this.Label1.AutoSize = true;
      this.Label1.Location = new System.Drawing.Point(67, 128);
      this.Label1.Name = "Label1";
      this.Label1.Size = new System.Drawing.Size(52, 13);
      this.Label1.TabIndex = 1;
      this.Label1.Text = "Initializing";
      this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // vCircularProgressBar1
      // 
      this.vCircularProgressBar1.AllowAnimations = true;
      this.vCircularProgressBar1.BackColor = System.Drawing.Color.Transparent;
      this.vCircularProgressBar1.IndicatorsCount = 8;
      this.vCircularProgressBar1.Location = new System.Drawing.Point(42, 26);
      this.vCircularProgressBar1.Maximum = 100;
      this.vCircularProgressBar1.Minimum = 0;
      this.vCircularProgressBar1.Name = "vCircularProgressBar1";
      this.vCircularProgressBar1.Size = new System.Drawing.Size(106, 99);
      this.vCircularProgressBar1.TabIndex = 2;
      this.vCircularProgressBar1.Text = "vCircularProgressBar1";
      this.vCircularProgressBar1.UseThemeBackground = false;
      this.vCircularProgressBar1.Value = 0;
      this.vCircularProgressBar1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // CircularProgressUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
      this.ClientSize = new System.Drawing.Size(190, 164);
      this.Controls.Add(this.vCircularProgressBar1);
      this.Controls.Add(this.Label1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "CircularProgressUI";
      this.ResumeLayout(false);
      this.PerformLayout();

  }
	public System.Windows.Forms.Label Label1;
  private VIBlend.WinForms.Controls.vCircularProgressBar vCircularProgressBar1;
}

}
