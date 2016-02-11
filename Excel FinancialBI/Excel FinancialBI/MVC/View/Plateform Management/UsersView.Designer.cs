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
      this.PanelEntities = new System.Windows.Forms.Panel();
      this.LayoutPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // LayoutPanel
      // 
      this.LayoutPanel.BackColor = System.Drawing.SystemColors.ControlLight;
      this.LayoutPanel.ColumnCount = 2;
      this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.LayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 750F));
      this.LayoutPanel.Controls.Add(this.PanelEntities, 1, 0);
      this.LayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.LayoutPanel.Location = new System.Drawing.Point(0, 0);
      this.LayoutPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.LayoutPanel.Name = "LayoutPanel";
      this.LayoutPanel.RowCount = 1;
      this.LayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.LayoutPanel.Size = new System.Drawing.Size(1734, 734);
      this.LayoutPanel.TabIndex = 0;
      // 
      // PanelEntities
      // 
      this.PanelEntities.Location = new System.Drawing.Point(987, 3);
      this.PanelEntities.Name = "PanelEntities";
      this.PanelEntities.Size = new System.Drawing.Size(744, 728);
      this.PanelEntities.TabIndex = 0;
      // 
      // UsersView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.LayoutPanel);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "UsersView";
      this.Size = new System.Drawing.Size(1734, 734);
      this.LayoutPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    public System.Windows.Forms.TableLayoutPanel LayoutPanel;
    private System.Windows.Forms.Panel PanelEntities;
  }
}