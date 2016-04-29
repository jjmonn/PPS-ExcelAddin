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
      this.Label1 = new System.Windows.Forms.Label();
      this.m_progressBar = new VIBlend.WinForms.Controls.vProgressBar();
      this.ProgressBarControl1 = new FBI.MVC.View.ProgressBarControl();
      this.SuspendLayout();
      // 
      // Label1
      // 
      resources.ApplyResources(this.Label1, "Label1");
      this.Label1.Name = "Label1";
      // 
      // m_progressBar
      // 
      this.m_progressBar.BackColor = System.Drawing.Color.Transparent;
      resources.ApplyResources(this.m_progressBar, "m_progressBar");
      this.m_progressBar.Name = "m_progressBar";
      this.m_progressBar.RoundedCornersMask = ((byte)(15));
      this.m_progressBar.Value = 0;
      this.m_progressBar.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // ProgressBarControl1
      // 
      resources.ApplyResources(this.ProgressBarControl1, "ProgressBarControl1");
      this.ProgressBarControl1.Name = "ProgressBarControl1";
      // 
      // PBarUI
      // 
      resources.ApplyResources(this, "$this");
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.m_progressBar);
      this.Controls.Add(this.Label1);
      this.Controls.Add(this.ProgressBarControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.Name = "PBarUI";
      this.ShowInTaskbar = false;
      this.TopMost = true;
      this.ResumeLayout(false);
      this.PerformLayout();

	}
	public ProgressBarControl ProgressBarControl1;
	public System.Windows.Forms.Label Label1;
  private VIBlend.WinForms.Controls.vProgressBar m_progressBar;
}

}
