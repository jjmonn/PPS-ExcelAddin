using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class FilterView : System.Windows.Forms.UserControl
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

    private System.ComponentModel.IContainer components;
    //NOTE: The following procedure is required by the Windows Form Designer
    //It can be modified using the Windows Form Designer.  
    //Do not modify it using the code editor.
    [System.Diagnostics.DebuggerStepThrough()]
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterView));
      this.m_valuePanel = new System.Windows.Forms.TableLayoutPanel();
      this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
      this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
      this.m_editStruct = new System.Windows.Forms.ToolStripMenuItem();
      this.m_contextRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.m_addValueRightClick = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.m_deleteRightClick = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.m_renameRightClick = new System.Windows.Forms.ToolStripMenuItem();
      this.m_expand = new System.Windows.Forms.ToolStripMenuItem();
      this.m_collapse = new System.Windows.Forms.ToolStripMenuItem();
      this.MenuStrip1.SuspendLayout();
      this.m_contextRightClick.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_valuePanel
      // 
      this.m_valuePanel.ColumnCount = 1;
      this.m_valuePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.m_valuePanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_valuePanel.Location = new System.Drawing.Point(0, 0);
      this.m_valuePanel.Name = "m_valuePanel";
      this.m_valuePanel.RowCount = 2;
      this.m_valuePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
      this.m_valuePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.m_valuePanel.Size = new System.Drawing.Size(525, 430);
      this.m_valuePanel.TabIndex = 0;
      // 
      // ImageList1
      // 
      this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
      this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.ImageList1.Images.SetKeyName(0, "filter_and_sort.ico");
      this.ImageList1.Images.SetKeyName(1, "config circle orangev small.png");
      this.ImageList1.Images.SetKeyName(2, "favicon(81).ico");
      // 
      // MenuStrip1
      // 
      this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_editStruct});
      this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
      this.MenuStrip1.Name = "MenuStrip1";
      this.MenuStrip1.Size = new System.Drawing.Size(525, 24);
      this.MenuStrip1.TabIndex = 2;
      this.MenuStrip1.Text = "MenuStrip1";
      // 
      // m_editStruct
      // 
      this.m_editStruct.Name = "m_editStruct";
      this.m_editStruct.Size = new System.Drawing.Size(89, 20);
      this.m_editStruct.Text = "Edit structure";
      // 
      // m_contextRightClick
      // 
      this.m_contextRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_addValueRightClick,
            this.ToolStripSeparator2,
            this.m_deleteRightClick,
            this.ToolStripSeparator1,
            this.m_renameRightClick,
            this.m_expand,
            this.m_collapse});
      this.m_contextRightClick.Name = "RCM_TV";
      this.m_contextRightClick.Size = new System.Drawing.Size(135, 126);
      // 
      // m_addValueRightClick
      // 
      this.m_addValueRightClick.Image = global::FBI.Properties.Resources.add;
      this.m_addValueRightClick.Name = "m_addValueRightClick";
      this.m_addValueRightClick.Size = new System.Drawing.Size(134, 22);
      this.m_addValueRightClick.Text = "New value";
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(131, 6);
      // 
      // m_deleteRightClick
      // 
      this.m_deleteRightClick.Image = global::FBI.Properties.Resources.imageres_89;
      this.m_deleteRightClick.Name = "m_deleteRightClick";
      this.m_deleteRightClick.Size = new System.Drawing.Size(134, 22);
      this.m_deleteRightClick.Text = "Delete";
      // 
      // ToolStripSeparator1
      // 
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(131, 6);
      // 
      // m_renameRightClick
      // 
      this.m_renameRightClick.Name = "m_renameRightClick";
      this.m_renameRightClick.Size = new System.Drawing.Size(134, 22);
      this.m_renameRightClick.Text = "Rename";
      // 
      // m_expand
      // 
      this.m_expand.Name = "m_expand";
      this.m_expand.Size = new System.Drawing.Size(134, 22);
      this.m_expand.Text = "Expand_all";
      // 
      // m_collapse
      // 
      this.m_collapse.Name = "m_collapse";
      this.m_collapse.Size = new System.Drawing.Size(134, 22);
      this.m_collapse.Text = "Collapse all";
      // 
      // FilterView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.MenuStrip1);
      this.Controls.Add(this.m_valuePanel);
      this.Name = "FilterView";
      this.Size = new System.Drawing.Size(525, 430);
      this.MenuStrip1.ResumeLayout(false);
      this.MenuStrip1.PerformLayout();
      this.m_contextRightClick.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    public System.Windows.Forms.TableLayoutPanel m_valuePanel;
    public System.Windows.Forms.ImageList ImageList1;
    public System.Windows.Forms.MenuStrip MenuStrip1;
    public System.Windows.Forms.ContextMenuStrip m_contextRightClick;
    public System.Windows.Forms.ToolStripMenuItem m_addValueRightClick;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    public System.Windows.Forms.ToolStripMenuItem m_renameRightClick;
    public System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    public System.Windows.Forms.ToolStripMenuItem m_deleteRightClick;
    public System.Windows.Forms.ToolStripMenuItem m_expand;
    public System.Windows.Forms.ToolStripMenuItem m_collapse;

    public System.Windows.Forms.ToolStripMenuItem m_editStruct;
  }
}