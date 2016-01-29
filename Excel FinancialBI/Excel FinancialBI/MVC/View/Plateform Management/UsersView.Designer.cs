using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class UsersView : System.Windows.Forms.UserControl
  {
    //UserControl overrides dispose to clean up the component list.
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
      this.LayoutPanel = new System.Windows.Forms.TableLayoutPanel();
      this.EntitiesTV = new VIBlend.WinForms.Controls.vTreeView();
      this.LayoutPanel.SuspendLayout();
      this.SuspendLayout();
      //
      //LayoutPanel
      //
      this.LayoutPanel.BackColor = System.Drawing.SystemColors.ControlLight;
      this.LayoutPanel.ColumnCount = 2;
      this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
      this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 500f));
      this.LayoutPanel.Controls.Add(this.EntitiesTV, 1, 0);
      this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
      this.LayoutPanel.Name = "LayoutPanel";
      this.LayoutPanel.RowCount = 1;
      this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
      this.LayoutPanel.Size = new System.Drawing.Size(1156, 477);
      this.LayoutPanel.TabIndex = 0;
      //
      //EntitiesTV
      //
      this.EntitiesTV.AccessibleName = "TreeView";
      this.EntitiesTV.AccessibleRole = System.Windows.Forms.AccessibleRole.List;
      this.EntitiesTV.CheckBoxes = true;
      this.EntitiesTV.Dock = System.Windows.Forms.DockStyle.Fill;
      this.EntitiesTV.Location = new System.Drawing.Point(659, 3);
      this.EntitiesTV.Name = "EntitiesTV";
      this.EntitiesTV.ScrollPosition = new System.Drawing.Point(0, 0);
      this.EntitiesTV.SelectedNode = null;
      this.EntitiesTV.Size = new System.Drawing.Size(494, 471);
      this.EntitiesTV.TabIndex = 0;
      this.EntitiesTV.Text = "VTreeView1";
      this.EntitiesTV.VIBlendScrollBarsTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      this.EntitiesTV.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //UsersControl
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.LayoutPanel);
      this.Name = "UsersControl";
      this.Size = new System.Drawing.Size(1156, 477);
      this.LayoutPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal System.Windows.Forms.TableLayoutPanel LayoutPanel;

    internal VIBlend.WinForms.Controls.vTreeView EntitiesTV;
  }
}