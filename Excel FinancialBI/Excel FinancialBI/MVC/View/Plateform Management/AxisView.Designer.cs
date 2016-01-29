using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class AxisView : System.Windows.Forms.UserControl
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AxisView));
      this.EntitiesIL = new System.Windows.Forms.ImageList(this.components);
      this.ButtonsIL = new System.Windows.Forms.ImageList(this.components);
      this.m_axisRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.CreateAxisElemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.DeleteAxisElemToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.RenameAxisElemButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.copy_down_bt = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.drop_to_excel_bt = new System.Windows.Forms.ToolStripMenuItem();
      this.AutoResizeColumnsButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ExpandAllBT = new System.Windows.Forms.ToolStripMenuItem();
      this.CollapseAllBT = new System.Windows.Forms.ToolStripMenuItem();
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
      this.m_axisEditionButton = new System.Windows.Forms.ToolStripMenuItem();
      this.CreateANewAxisElemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.DeleteAxisElemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.SendEntitiesHierarchyToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_axisRightClickMenu.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      this.MenuStrip1.SuspendLayout();
      this.SuspendLayout();
      //
      //EntitiesIL
      //
      this.EntitiesIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("EntitiesIL.ImageStream");
      this.EntitiesIL.TransparentColor = System.Drawing.Color.Transparent;
      this.EntitiesIL.Images.SetKeyName(0, "elements_branch.ico");
      this.EntitiesIL.Images.SetKeyName(1, "favicon(81).ico");
      //
      //ButtonsIL
      //
      this.ButtonsIL.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ButtonsIL.ImageStream");
      this.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico");
      //
      //m_axisRightClickMenu
      //
      this.m_axisRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.CreateAxisElemToolStripMenuItem,
			this.DeleteAxisElemToolStripMenuItem2,
			this.ToolStripSeparator1,
			this.RenameAxisElemButton,
			this.ToolStripSeparator5,
			this.copy_down_bt,
			this.ToolStripSeparator4,
			this.drop_to_excel_bt,
			this.AutoResizeColumnsButton,
			this.ExpandAllBT,
			this.CollapseAllBT
		});
      this.m_axisRightClickMenu.Name = "ContextMenuStripTGV";
      this.m_axisRightClickMenu.Size = new System.Drawing.Size(202, 214);
      //
      //CreateAxisElemToolStripMenuItem
      //
      this.CreateAxisElemToolStripMenuItem.Image = global::FBI.Properties.Resources.element_branch2_add;
      this.CreateAxisElemToolStripMenuItem.Name = "CreateAxisElemToolStripMenuItem";
      this.CreateAxisElemToolStripMenuItem.Size = new System.Drawing.Size(201, 24);
      this.CreateAxisElemToolStripMenuItem.Text = "Create";
      //
      //DeleteAxisElemToolStripMenuItem2
      //
      this.DeleteAxisElemToolStripMenuItem2.Image = global::FBI.Properties.Resources.element_branch2_delete;
      this.DeleteAxisElemToolStripMenuItem2.Name = "DeleteAxisElemToolStripMenuItem2";
      this.DeleteAxisElemToolStripMenuItem2.Size = new System.Drawing.Size(201, 24);
      this.DeleteAxisElemToolStripMenuItem2.Text = "Delete";
      //
      //ToolStripSeparator1
      //
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(198, 6);
      //
      //RenameAxisElemButton
      //
      this.RenameAxisElemButton.Name = "RenameAxisElemButton";
      this.RenameAxisElemButton.Size = new System.Drawing.Size(201, 24);
      this.RenameAxisElemButton.Text = "Rename";
      //
      //ToolStripSeparator5
      //
      this.ToolStripSeparator5.Name = "ToolStripSeparator5";
      this.ToolStripSeparator5.Size = new System.Drawing.Size(198, 6);
      //
      //copy_down_bt
      //
      this.copy_down_bt.Image = global::FBI.Properties.Resources.Download;
      this.copy_down_bt.Name = "copy_down_bt";
      this.copy_down_bt.Size = new System.Drawing.Size(201, 24);
      this.copy_down_bt.Text = "Copy down";
      //
      //ToolStripSeparator4
      //
      this.ToolStripSeparator4.Name = "ToolStripSeparator4";
      this.ToolStripSeparator4.Size = new System.Drawing.Size(198, 6);
      //
      //drop_to_excel_bt
      //
      this.drop_to_excel_bt.Image = global::FBI.Properties.Resources.excel_blue2;
      this.drop_to_excel_bt.Name = "drop_to_excel_bt";
      this.drop_to_excel_bt.Size = new System.Drawing.Size(201, 24);
      this.drop_to_excel_bt.Text = "Drop on excel";
      //
      //AutoResizeColumnsButton
      //
      this.AutoResizeColumnsButton.Name = "AutoResizeColumnsButton";
      this.AutoResizeColumnsButton.Size = new System.Drawing.Size(201, 24);
      this.AutoResizeColumnsButton.Text = "Auto resize columns";
      //
      //ExpandAllBT
      //
      this.ExpandAllBT.Name = "ExpandAllBT";
      this.ExpandAllBT.Size = new System.Drawing.Size(201, 24);
      this.ExpandAllBT.Text = "Expand all";
      //
      //CollapseAllBT
      //
      this.CollapseAllBT.Name = "CollapseAllBT";
      this.CollapseAllBT.Size = new System.Drawing.Size(201, 24);
      this.CollapseAllBT.Text = "Collapse all";
      //
      //TableLayoutPanel1
      //
      this.TableLayoutPanel1.ColumnCount = 1;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
      this.TableLayoutPanel1.Controls.Add(this.MenuStrip1, 0, 0);
      this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24f));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(516, 420);
      this.TableLayoutPanel1.TabIndex = 7;
      //
      //MenuStrip1
      //
      this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.m_axisEditionButton,
			this.ExcelToolStripMenuItem
		});
      this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
      this.MenuStrip1.Name = "MenuStrip1";
      this.MenuStrip1.Size = new System.Drawing.Size(516, 24);
      this.MenuStrip1.TabIndex = 7;
      this.MenuStrip1.Text = "MenuStrip1";
      //
      //m_axisEditionButton
      //
      this.m_axisEditionButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.CreateANewAxisElemToolStripMenuItem,
			this.DeleteAxisElemToolStripMenuItem,
			this.ToolStripSeparator2
		});
      this.m_axisEditionButton.Image = global::FBI.Properties.Resources.element_branch23;
      this.m_axisEditionButton.Name = "m_axisEditionButton";
      this.m_axisEditionButton.Size = new System.Drawing.Size(81, 20);
      this.m_axisEditionButton.Text = "Entities";
      //
      //CreateANewAxisElemToolStripMenuItem
      //
      this.CreateANewAxisElemToolStripMenuItem.Image = global::FBI.Properties.Resources.plus;
      this.CreateANewAxisElemToolStripMenuItem.Name = "CreateANewAxisElemToolStripMenuItem";
      this.CreateANewAxisElemToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
      this.CreateANewAxisElemToolStripMenuItem.Text = "Create";
      //
      //DeleteAxisElemToolStripMenuItem
      //
      this.DeleteAxisElemToolStripMenuItem.Image = global::FBI.Properties.Resources.imageres_89;
      this.DeleteAxisElemToolStripMenuItem.Name = "DeleteAxisElemToolStripMenuItem";
      this.DeleteAxisElemToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
      this.DeleteAxisElemToolStripMenuItem.Text = "Delete";
      //
      //ToolStripSeparator2
      //
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(149, 6);
      //
      //ExcelToolStripMenuItem
      //
      this.ExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.SendEntitiesHierarchyToExcelToolStripMenuItem });
      this.ExcelToolStripMenuItem.Image = global::FBI.Properties.Resources.Excel_dark_24_24;
      this.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem";
      this.ExcelToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
      this.ExcelToolStripMenuItem.Text = "Excel";
      //
      //SendEntitiesHierarchyToExcelToolStripMenuItem
      //
      this.SendEntitiesHierarchyToExcelToolStripMenuItem.Image = global::FBI.Properties.Resources.excel_blue2;
      this.SendEntitiesHierarchyToExcelToolStripMenuItem.Name = "SendEntitiesHierarchyToExcelToolStripMenuItem";
      this.SendEntitiesHierarchyToExcelToolStripMenuItem.Size = new System.Drawing.Size(162, 24);
      this.SendEntitiesHierarchyToExcelToolStripMenuItem.Text = "Drop on excel";
      //
      //AxisView
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.TableLayoutPanel1);
      this.Name = "AxisView";
      this.Size = new System.Drawing.Size(516, 420);
      this.m_axisRightClickMenu.ResumeLayout(false);
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      this.MenuStrip1.ResumeLayout(false);
      this.MenuStrip1.PerformLayout();
      this.ResumeLayout(false);

    }
    public System.Windows.Forms.ImageList EntitiesIL;
    internal System.Windows.Forms.ImageList ButtonsIL;
    internal System.Windows.Forms.ContextMenuStrip m_axisRightClickMenu;
    internal System.Windows.Forms.ToolStripMenuItem copy_down_bt;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
    internal System.Windows.Forms.ToolStripMenuItem drop_to_excel_bt;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
    internal System.Windows.Forms.ToolStripMenuItem CreateAxisElemToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem DeleteAxisElemToolStripMenuItem2;
    internal System.Windows.Forms.MenuStrip MenuStrip1;
    internal System.Windows.Forms.ToolStripMenuItem m_axisEditionButton;
    internal System.Windows.Forms.ToolStripMenuItem CreateANewAxisElemToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem DeleteAxisElemToolStripMenuItem;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    internal System.Windows.Forms.ToolStripMenuItem ExcelToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem SendEntitiesHierarchyToExcelToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem RenameAxisElemButton;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    internal System.Windows.Forms.ToolStripMenuItem AutoResizeColumnsButton;
    internal System.Windows.Forms.ToolStripMenuItem ExpandAllBT;
    internal System.Windows.Forms.ToolStripMenuItem CollapseAllBT;

    protected System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
  }
}