using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class SnapshotPeriodRangeSelectionUI : System.Windows.Forms.Form
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SnapshotPeriodRangeSelectionUI));
		this.m_validateButton = new VIBlend.WinForms.Controls.vButton();
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.m_periodSelectionPanel = new System.Windows.Forms.Panel();
		this.m_accountSelectionComboBox = new VIBlend.WinForms.Controls.vComboBox();
		this.m_accountSelectionLabel = new VIBlend.WinForms.Controls.vLabel();
		this.SuspendLayout();
		//
		//m_validateButton
		//
		this.m_validateButton.AllowAnimations = true;
		this.m_validateButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
		this.m_validateButton.BackColor = System.Drawing.Color.Transparent;
		this.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.m_validateButton.ImageKey = "1420498403_340208.ico";
		this.m_validateButton.ImageList = this.ImageList1;
		this.m_validateButton.Location = new System.Drawing.Point(304, 187);
		this.m_validateButton.Name = "m_validateButton";
		this.m_validateButton.RoundedCornersMask = Convert.ToByte(15);
		this.m_validateButton.Size = new System.Drawing.Size(91, 24);
		this.m_validateButton.TabIndex = 3;
		this.m_validateButton.Text = "Validate";
		this.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_validateButton.UseVisualStyleBackColor = false;
		this.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//ImageList1
		//
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "1420498403_340208.ico");
		//
		//m_periodSelectionPanel
		//
		this.m_periodSelectionPanel.Location = new System.Drawing.Point(12, 13);
		this.m_periodSelectionPanel.Name = "m_periodSelectionPanel";
		this.m_periodSelectionPanel.Size = new System.Drawing.Size(383, 129);
		this.m_periodSelectionPanel.TabIndex = 4;
		//
		//m_accountSelectionComboBox
		//
		this.m_accountSelectionComboBox.BackColor = System.Drawing.Color.White;
		this.m_accountSelectionComboBox.DisplayMember = "";
		this.m_accountSelectionComboBox.DropDownList = true;
		this.m_accountSelectionComboBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
		this.m_accountSelectionComboBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
		this.m_accountSelectionComboBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
		this.m_accountSelectionComboBox.DropDownWidth = 243;
		this.m_accountSelectionComboBox.Location = new System.Drawing.Point(152, 148);
		this.m_accountSelectionComboBox.Name = "m_accountSelectionComboBox";
		this.m_accountSelectionComboBox.RoundedCornersMaskListItem = Convert.ToByte(15);
		this.m_accountSelectionComboBox.Size = new System.Drawing.Size(243, 23);
		this.m_accountSelectionComboBox.TabIndex = 5;
		this.m_accountSelectionComboBox.UseThemeBackColor = false;
		this.m_accountSelectionComboBox.UseThemeDropDownArrowColor = true;
		this.m_accountSelectionComboBox.ValueMember = "";
		this.m_accountSelectionComboBox.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		this.m_accountSelectionComboBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_accountSelectionLabel
		//
		this.m_accountSelectionLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_accountSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_accountSelectionLabel.Ellipsis = false;
		this.m_accountSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_accountSelectionLabel.Location = new System.Drawing.Point(12, 148);
		this.m_accountSelectionLabel.Multiline = true;
		this.m_accountSelectionLabel.Name = "m_accountSelectionLabel";
		this.m_accountSelectionLabel.Size = new System.Drawing.Size(134, 23);
		this.m_accountSelectionLabel.TabIndex = 6;
		this.m_accountSelectionLabel.Text = "Account selection";
		this.m_accountSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_accountSelectionLabel.UseMnemonics = true;
		this.m_accountSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//SnapshotPeriodRangeSelectionUI
		//
		this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.ClientSize = new System.Drawing.Size(407, 223);
		this.Controls.Add(this.m_accountSelectionLabel);
		this.Controls.Add(this.m_accountSelectionComboBox);
		this.Controls.Add(this.m_periodSelectionPanel);
		this.Controls.Add(this.m_validateButton);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Name = "SnapshotPeriodRangeSelectionUI";
		this.Text = "Period Range";
		this.ResumeLayout(false);

	}
	public VIBlend.WinForms.Controls.vButton m_validateButton;
	public System.Windows.Forms.ImageList ImageList1;
	public System.Windows.Forms.Panel m_periodSelectionPanel;
	public VIBlend.WinForms.Controls.vComboBox m_accountSelectionComboBox;
	public VIBlend.WinForms.Controls.vLabel m_accountSelectionLabel;
}

}
