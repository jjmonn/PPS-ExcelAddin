using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
partial class ProcessSelectionTaskPane : AddinExpress.XL.ADXExcelTaskPane
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

	private System.ComponentModel.IContainer components;
	//NOTE: The following procedure is required by the Windows Form Designer
	//It can be modified using the Windows Form Designer.
	//Do not modify it using the code editor.
	[System.Diagnostics.DebuggerStepThrough()]
	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessSelectionTaskPane));
		this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
		this.m_processSelectionLabel = new VIBlend.WinForms.Controls.vLabel();
		this.m_validateButton = new VIBlend.WinForms.Controls.vButton();
		this.BTsIL = new System.Windows.Forms.ImageList(this.components);
		this.TableLayoutPanel1.SuspendLayout();
		this.SuspendLayout();
		//
		//TableLayoutPanel1
		//
		this.TableLayoutPanel1.ColumnCount = 1;
		this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
		this.TableLayoutPanel1.Controls.Add(this.m_processSelectionLabel, 0, 0);
		this.TableLayoutPanel1.Controls.Add(this.m_validateButton, 0, 2);
		this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
		this.TableLayoutPanel1.Name = "TableLayoutPanel1";
		this.TableLayoutPanel1.RowCount = 3;
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.882352f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.11765f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
		this.TableLayoutPanel1.Size = new System.Drawing.Size(296, 708);
		this.TableLayoutPanel1.TabIndex = 1;
		//
		//m_processSelectionLabel
		//
		this.m_processSelectionLabel.BackColor = System.Drawing.Color.Transparent;
		this.m_processSelectionLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
		this.m_processSelectionLabel.Ellipsis = false;
		this.m_processSelectionLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_processSelectionLabel.Location = new System.Drawing.Point(3, 4);
		this.m_processSelectionLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
		this.m_processSelectionLabel.Multiline = true;
		this.m_processSelectionLabel.Name = "m_processSelectionLabel";
		this.m_processSelectionLabel.Size = new System.Drawing.Size(98, 15);
		this.m_processSelectionLabel.TabIndex = 2;
		this.m_processSelectionLabel.Text = "Select a Process";
		this.m_processSelectionLabel.TextAlignment = System.Drawing.ContentAlignment.TopLeft;
		this.m_processSelectionLabel.UseMnemonics = true;
		this.m_processSelectionLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//m_validateButton
		//
		this.m_validateButton.AllowAnimations = true;
		this.m_validateButton.BackColor = System.Drawing.Color.Transparent;
		this.m_validateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.m_validateButton.ImageKey = "1420498403_340208.ico";
		this.m_validateButton.ImageList = this.BTsIL;
		this.m_validateButton.Location = new System.Drawing.Point(3, 625);
		this.m_validateButton.Name = "m_validateButton";
		this.m_validateButton.RoundedCornersMask = Convert.ToByte(15);
		this.m_validateButton.Size = new System.Drawing.Size(95, 23);
		this.m_validateButton.TabIndex = 3;
		this.m_validateButton.Text = "Validate";
		this.m_validateButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.m_validateButton.UseVisualStyleBackColor = true;
		this.m_validateButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
		//
		//BTsIL
		//
		this.BTsIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("BTsIL.ImageStream");
		this.BTsIL.TransparentColor = System.Drawing.Color.Transparent;
		this.BTsIL.Images.SetKeyName(0, "1420498403_340208.ico");
		//
		//ProcessSelectionTaskPane
		//
		this.ClientSize = new System.Drawing.Size(296, 708);
		this.Controls.Add(this.TableLayoutPanel1);
		this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		this.Location = new System.Drawing.Point(0, 0);
		this.Name = "ProcessSelectionTaskPane";
		this.Text = "Process Selection";
		this.TableLayoutPanel1.ResumeLayout(false);
		this.ResumeLayout(false);

	}
	internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
	internal VIBlend.WinForms.Controls.vLabel m_processSelectionLabel;
  internal VIBlend.WinForms.Controls.vButton m_validateButton;

	internal System.Windows.Forms.ImageList BTsIL;
}

}
