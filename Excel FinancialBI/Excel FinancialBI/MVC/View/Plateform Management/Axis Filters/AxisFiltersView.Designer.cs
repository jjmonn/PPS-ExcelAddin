using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class AxisFiltersView : System.Windows.Forms.UserControl
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AxisFiltersView));
      this.m_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
      this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
      this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
      this.CategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.AddValueMenuBT = new System.Windows.Forms.ToolStripMenuItem();
      this.DeleteMenuBT = new System.Windows.Forms.ToolStripMenuItem();
      this.RenameMenuBT = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
      this.EditStructureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.RCM_TV = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.AddValueRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.DeleteRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.RenameRCM = new System.Windows.Forms.ToolStripMenuItem();
      this.ExpandAllBT = new System.Windows.Forms.ToolStripMenuItem();
      this.CollapseAllBT = new System.Windows.Forms.ToolStripMenuItem();
      this.MenuStrip1.SuspendLayout();
      this.RCM_TV.SuspendLayout();
      this.SuspendLayout();
      //
      //m_tableLayoutPanel
      //
      this.m_tableLayoutPanel.ColumnCount = 1;
      this.m_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
      this.m_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
      this.m_tableLayoutPanel.Name = "m_tableLayoutPanel";
      this.m_tableLayoutPanel.RowCount = 2;
      this.m_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
      this.m_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
      this.m_tableLayoutPanel.Size = new System.Drawing.Size(525, 430);
      this.m_tableLayoutPanel.TabIndex = 0;
      //
      //ImageList1
      //
      this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
      this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
      this.ImageList1.Images.SetKeyName(0, "filter_and_sort.ico");
      this.ImageList1.Images.SetKeyName(1, "config circle orangev small.png");
      this.ImageList1.Images.SetKeyName(2, "favicon(81).ico");
      //
      //MenuStrip1
      //
      this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.CategoriesToolStripMenuItem,
			this.EditStructureToolStripMenuItem
		});
      this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
      this.MenuStrip1.Name = "MenuStrip1";
      this.MenuStrip1.Size = new System.Drawing.Size(525, 24);
      this.MenuStrip1.TabIndex = 2;
      this.MenuStrip1.Text = "MenuStrip1";
      //
      //CategoriesToolStripMenuItem
      //
      this.CategoriesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.AddValueMenuBT,
			this.DeleteMenuBT,
			this.RenameMenuBT,
			this.ToolStripSeparator3
		});
      this.CategoriesToolStripMenuItem.Image = global::FBI.Properties.Resources.element_branch25;
      this.CategoriesToolStripMenuItem.Name = "CategoriesToolStripMenuItem";
      this.CategoriesToolStripMenuItem.Size = new System.Drawing.Size(129, 20);
      this.CategoriesToolStripMenuItem.Text = "Categories";
      //
      //AddValueMenuBT
      //
      this.AddValueMenuBT.Name = "AddValueMenuBT";
      this.AddValueMenuBT.Size = new System.Drawing.Size(169, 22);
      this.AddValueMenuBT.Text = "New value";
      //
      //DeleteMenuBT
      //
      this.DeleteMenuBT.Image = global::FBI.Properties.Resources.imageres_891;
      this.DeleteMenuBT.Name = "DeleteMenuBT";
      this.DeleteMenuBT.Size = new System.Drawing.Size(169, 22);
      this.DeleteMenuBT.Text = "Delete";
      //
      //RenameMenuBT
      //
      this.RenameMenuBT.Name = "RenameMenuBT";
      this.RenameMenuBT.Size = new System.Drawing.Size(169, 22);
      this.RenameMenuBT.Text = "Rename";
      //
      //ToolStripSeparator3
      //
      this.ToolStripSeparator3.Name = "ToolStripSeparator3";
      this.ToolStripSeparator3.Size = new System.Drawing.Size(166, 6);
      //
      //EditStructureToolStripMenuItem
      //
      this.EditStructureToolStripMenuItem.Name = "EditStructureToolStripMenuItem";
      this.EditStructureToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
      this.EditStructureToolStripMenuItem.Text = "Edit structure";
      //
      //RCM_TV
      //
      this.RCM_TV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.AddValueRCM,
			this.ToolStripSeparator2,
			this.DeleteRCM,
			this.ToolStripSeparator1,
			this.RenameRCM,
			this.ExpandAllBT,
			this.CollapseAllBT
		});
      this.RCM_TV.Name = "RCM_TV";
      this.RCM_TV.Size = new System.Drawing.Size(185, 126);
      //
      //AddValueRCM
      //
      this.AddValueRCM.Image = global::FBI.Properties.Resources.@add;
      this.AddValueRCM.Name = "AddValueRCM";
      this.AddValueRCM.Size = new System.Drawing.Size(184, 22);
      this.AddValueRCM.Text = "New value";
      //
      //ToolStripSeparator2
      //
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(181, 6);
      //
      //DeleteRCM
      //
      this.DeleteRCM.Image = global::FBI.Properties.Resources.imageres_89;
      this.DeleteRCM.Name = "DeleteRCM";
      this.DeleteRCM.Size = new System.Drawing.Size(184, 22);
      this.DeleteRCM.Text = "Delete";
      //
      //ToolStripSeparator1
      //
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(181, 6);
      //
      //RenameRCM
      //
      this.RenameRCM.Name = "RenameRCM";
      this.RenameRCM.Size = new System.Drawing.Size(184, 22);
      this.RenameRCM.Text = "Rename";
      //
      //ExpandAllBT
      //
      this.ExpandAllBT.Name = "ExpandAllBT";
      this.ExpandAllBT.Size = new System.Drawing.Size(184, 22);
      this.ExpandAllBT.Text = "Expand_all";
      //
      //CollapseAllBT
      //
      this.CollapseAllBT.Name = "CollapseAllBT";
      this.CollapseAllBT.Size = new System.Drawing.Size(184, 22);
      this.CollapseAllBT.Text = "Collapse all";
      //
      //AxisFiltersView
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.MenuStrip1);
      this.Controls.Add(this.m_tableLayoutPanel);
      this.Name = "AxisFiltersView";
      this.Size = new System.Drawing.Size(525, 430);
      this.MenuStrip1.ResumeLayout(false);
      this.MenuStrip1.PerformLayout();
      this.RCM_TV.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    internal System.Windows.Forms.TableLayoutPanel m_tableLayoutPanel;
    internal System.Windows.Forms.ImageList ImageList1;
    internal System.Windows.Forms.MenuStrip MenuStrip1;
    internal System.Windows.Forms.ToolStripMenuItem CategoriesToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem AddValueMenuBT;
    internal System.Windows.Forms.ToolStripMenuItem RenameMenuBT;
    internal System.Windows.Forms.ToolStripMenuItem DeleteMenuBT;
    internal System.Windows.Forms.ContextMenuStrip RCM_TV;
    internal System.Windows.Forms.ToolStripMenuItem AddValueRCM;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    internal System.Windows.Forms.ToolStripMenuItem RenameRCM;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    internal System.Windows.Forms.ToolStripMenuItem DeleteRCM;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator3;
    internal System.Windows.Forms.ToolStripMenuItem ExpandAllBT;
    internal System.Windows.Forms.ToolStripMenuItem CollapseAllBT;

    internal System.Windows.Forms.ToolStripMenuItem EditStructureToolStripMenuItem;
  }
}