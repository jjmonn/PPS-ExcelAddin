using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class AllocationKeysView : System.Windows.Forms.Form
  {

    //Form overrides dispose to clean up the component list.
    [System.Diagnostics.DebuggerNonUserCode()]
    protected override void Dispose(bool disposing)
    {
      try
      {
        if (disposing && components != null)
        {
          components.Dispose();
        }
      }
      finally
      {
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AllocationKeysView));
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.Panel1 = new System.Windows.Forms.Panel();
      this.m_accountTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_accountLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_DGVPanel = new System.Windows.Forms.Panel();
      this.TableLayoutPanel1.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.ColumnCount = 1;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Controls.Add(this.Panel1, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_DGVPanel, 0, 1);
      this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(891, 814);
      this.TableLayoutPanel1.TabIndex = 0;
      // 
      // Panel1
      // 
      this.Panel1.Controls.Add(this.m_accountTextBox);
      this.Panel1.Controls.Add(this.m_accountLabel);
      this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.Panel1.Location = new System.Drawing.Point(0, 0);
      this.Panel1.Margin = new System.Windows.Forms.Padding(0);
      this.Panel1.Name = "Panel1";
      this.Panel1.Size = new System.Drawing.Size(891, 38);
      this.Panel1.TabIndex = 1;
      // 
      // m_accountTextBox
      // 
      this.m_accountTextBox.BackColor = System.Drawing.Color.White;
      this.m_accountTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_accountTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_accountTextBox.DefaultText = "Empty...";
      this.m_accountTextBox.Enabled = false;
      this.m_accountTextBox.Location = new System.Drawing.Point(116, 5);
      this.m_accountTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountTextBox.MaxLength = 32767;
      this.m_accountTextBox.Name = "m_accountTextBox";
      this.m_accountTextBox.PasswordChar = '\0';
      this.m_accountTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_accountTextBox.SelectionLength = 0;
      this.m_accountTextBox.SelectionStart = 0;
      this.m_accountTextBox.Size = new System.Drawing.Size(771, 29);
      this.m_accountTextBox.TabIndex = 3;
      this.m_accountTextBox.Text = "Account";
      this.m_accountTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_accountTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICE2010SILVER;
      // 
      // m_accountLabel
      // 
      this.m_accountLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_accountLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_accountLabel.Ellipsis = false;
      this.m_accountLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_accountLabel.Location = new System.Drawing.Point(4, 5);
      this.m_accountLabel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.m_accountLabel.Multiline = true;
      this.m_accountLabel.Name = "m_accountLabel";
      this.m_accountLabel.Size = new System.Drawing.Size(102, 29);
      this.m_accountLabel.TabIndex = 2;
      this.m_accountLabel.Text = "Account";
      this.m_accountLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_accountLabel.UseMnemonics = true;
      this.m_accountLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_DGVPanel
      // 
      this.m_DGVPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_DGVPanel.Location = new System.Drawing.Point(3, 41);
      this.m_DGVPanel.Name = "m_DGVPanel";
      this.m_DGVPanel.Size = new System.Drawing.Size(885, 770);
      this.m_DGVPanel.TabIndex = 2;
      // 
      // AllocationKeysView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(891, 814);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "AllocationKeysView";
      this.Text = "Allocation Keys";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.Panel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
    internal System.Windows.Forms.Panel Panel1;
    internal VIBlend.WinForms.Controls.vTextBox m_accountTextBox;
    internal VIBlend.WinForms.Controls.vLabel m_accountLabel;
    private System.Windows.Forms.Panel m_DGVPanel;
  }
}