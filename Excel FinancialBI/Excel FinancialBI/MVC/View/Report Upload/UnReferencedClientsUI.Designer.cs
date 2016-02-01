using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class UnReferencedClientsUI : System.Windows.Forms.Form
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

    private System.ComponentModel.IContainer components;
    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer.  
    //Do not modify it using the code editor.
    [System.Diagnostics.DebuggerStepThrough()]
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnReferencedClientsUI));
      this.m_clientsDGVPanel = new System.Windows.Forms.Panel();
      this.m_createAllButton = new VIBlend.WinForms.Controls.vButton();
      this.m_DGVContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.UnselectBothOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.SelectAllOnColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.UnselectAllOnColumnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_DGVContextMenuStrip.SuspendLayout();
      this.SuspendLayout();
      //
      //m_clientsDGVPanel
      //
      this.m_clientsDGVPanel.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
      this.m_clientsDGVPanel.Location = new System.Drawing.Point(-1, 0);
      this.m_clientsDGVPanel.Name = "m_clientsDGVPanel";
      this.m_clientsDGVPanel.Size = new System.Drawing.Size(430, 310);
      this.m_clientsDGVPanel.TabIndex = 0;
      //
      //m_createAllButton
      //
      this.m_createAllButton.AllowAnimations = true;
      this.m_createAllButton.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.m_createAllButton.BackColor = System.Drawing.Color.Transparent;
      this.m_createAllButton.Location = new System.Drawing.Point(314, 318);
      this.m_createAllButton.Name = "m_createAllButton";
      this.m_createAllButton.RoundedCornersMask = Convert.ToByte(15);
      this.m_createAllButton.Size = new System.Drawing.Size(104, 30);
      this.m_createAllButton.TabIndex = 1;
      this.m_createAllButton.Text = "Validate";
      this.m_createAllButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_createAllButton.UseVisualStyleBackColor = false;
      this.m_createAllButton.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      //
      //m_DGVContextMenuStrip
      //
      this.m_DGVContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.UnselectBothOptionsToolStripMenuItem,
			this.SelectAllOnColumnToolStripMenuItem,
			this.UnselectAllOnColumnToolStripMenuItem
		});
      this.m_DGVContextMenuStrip.Name = "m_DGVContextMenuStrip";
      this.m_DGVContextMenuStrip.Size = new System.Drawing.Size(196, 92);
      //
      //UnselectBothOptionsToolStripMenuItem
      //
      this.UnselectBothOptionsToolStripMenuItem.Name = "UnselectBothOptionsToolStripMenuItem";
      this.UnselectBothOptionsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.UnselectBothOptionsToolStripMenuItem.Text = "Unselect both options";
      //
      //SelectAllOnColumnToolStripMenuItem
      //
      this.SelectAllOnColumnToolStripMenuItem.Name = "SelectAllOnColumnToolStripMenuItem";
      this.SelectAllOnColumnToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.SelectAllOnColumnToolStripMenuItem.Text = "Select all on column";
      //
      //UnselectAllOnColumnToolStripMenuItem
      //
      this.UnselectAllOnColumnToolStripMenuItem.Name = "UnselectAllOnColumnToolStripMenuItem";
      this.UnselectAllOnColumnToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.UnselectAllOnColumnToolStripMenuItem.Text = "Unselect all on column";
      //
      //UnReferencedClientsUI
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(427, 358);
      this.Controls.Add(this.m_createAllButton);
      this.Controls.Add(this.m_clientsDGVPanel);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "UnReferencedClientsUI";
      this.Text = "UnReferenced Clients";
      this.m_DGVContextMenuStrip.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal System.Windows.Forms.Panel m_clientsDGVPanel;
    internal VIBlend.WinForms.Controls.vButton m_createAllButton;
    internal System.Windows.Forms.ContextMenuStrip m_DGVContextMenuStrip;
    internal System.Windows.Forms.ToolStripMenuItem UnselectBothOptionsToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem SelectAllOnColumnToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem UnselectAllOnColumnToolStripMenuItem;
  }
}