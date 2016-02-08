using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class PasswordBox : System.Windows.Forms.Form
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
      this.m_passwordTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.DescTB = new VIBlend.WinForms.Controls.vLabel();
      this.AcceptBT = new VIBlend.WinForms.Controls.vButton();
      this.CancelBT = new VIBlend.WinForms.Controls.vButton();
      this.SuspendLayout();
      // 
      // m_passwordTextBox
      // 
      this.m_passwordTextBox.BackColor = System.Drawing.Color.White;
      this.m_passwordTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_passwordTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_passwordTextBox.DefaultText = "";
      this.m_passwordTextBox.Location = new System.Drawing.Point(12, 92);
      this.m_passwordTextBox.MaxLength = 32767;
      this.m_passwordTextBox.Name = "m_passwordTextBox";
      this.m_passwordTextBox.PasswordChar = '*';
      this.m_passwordTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_passwordTextBox.SelectionLength = 0;
      this.m_passwordTextBox.SelectionStart = 0;
      this.m_passwordTextBox.ShowDefaultText = false;
      this.m_passwordTextBox.Size = new System.Drawing.Size(314, 23);
      this.m_passwordTextBox.TabIndex = 0;
      this.m_passwordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_passwordTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // DescTB
      // 
      this.DescTB.BackColor = System.Drawing.Color.Transparent;
      this.DescTB.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.DescTB.Ellipsis = false;
      this.DescTB.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.DescTB.Location = new System.Drawing.Point(12, 13);
      this.DescTB.Multiline = true;
      this.DescTB.Name = "DescTB";
      this.DescTB.Size = new System.Drawing.Size(220, 63);
      this.DescTB.TabIndex = 1;
      this.DescTB.Text = "Label";
      this.DescTB.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.DescTB.UseMnemonics = true;
      this.DescTB.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // AcceptBT
      // 
      this.AcceptBT.AllowAnimations = true;
      this.AcceptBT.BackColor = System.Drawing.Color.Transparent;
      this.AcceptBT.Location = new System.Drawing.Point(252, 13);
      this.AcceptBT.Name = "AcceptBT";
      this.AcceptBT.RoundedCornersMask = ((byte)(15));
      this.AcceptBT.Size = new System.Drawing.Size(74, 22);
      this.AcceptBT.TabIndex = 2;
      this.AcceptBT.Text = "OK";
      this.AcceptBT.UseVisualStyleBackColor = false;
      this.AcceptBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.AcceptBT.Click += new System.EventHandler(this.AcceptBT_Click);
      // 
      // CancelBT
      // 
      this.CancelBT.AllowAnimations = true;
      this.CancelBT.BackColor = System.Drawing.Color.Transparent;
      this.CancelBT.Location = new System.Drawing.Point(252, 41);
      this.CancelBT.Name = "CancelBT";
      this.CancelBT.RoundedCornersMask = ((byte)(15));
      this.CancelBT.Size = new System.Drawing.Size(74, 22);
      this.CancelBT.TabIndex = 3;
      this.CancelBT.Text = "Cancel";
      this.CancelBT.UseVisualStyleBackColor = false;
      this.CancelBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.CancelBT.Click += new System.EventHandler(this.CancelBT_Click);
      // 
      // PasswordBox
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(338, 127);
      this.Controls.Add(this.CancelBT);
      this.Controls.Add(this.AcceptBT);
      this.Controls.Add(this.DescTB);
      this.Controls.Add(this.m_passwordTextBox);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "PasswordBox";
      this.ShowIcon = false;
      this.Text = "PasswordBox";
      this.ResumeLayout(false);

	}
	internal VIBlend.WinForms.Controls.vTextBox m_passwordTextBox;
	internal VIBlend.WinForms.Controls.vLabel DescTB;
	internal VIBlend.WinForms.Controls.vButton AcceptBT;
	internal VIBlend.WinForms.Controls.vButton CancelBT;
}

}
