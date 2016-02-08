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
      this.m_createAxisElemMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_deleteAxisElemMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.m_renameAxisElemMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
      this.m_copyDownMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
      this.m_dropToExcelMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_autoResizeMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_expandAllMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.m_collapseAllMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
      this.m_axisEditionButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_createNewAxisElemMenuTop = new System.Windows.Forms.ToolStripMenuItem();
      this.m_deleteAxisElemMenuTop = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
      this.ExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.m_dropToExcelMenuTop = new System.Windows.Forms.ToolStripMenuItem();
      this.m_axisRightClickMenu.SuspendLayout();
      this.TableLayoutPanel1.SuspendLayout();
      this.MenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // EntitiesIL
      // 
      this.EntitiesIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("EntitiesIL.ImageStream")));
      this.EntitiesIL.TransparentColor = System.Drawing.Color.Transparent;
      this.EntitiesIL.Images.SetKeyName(0, "elements_branch.ico");
      this.EntitiesIL.Images.SetKeyName(1, "favicon(81).ico");
      // 
      // ButtonsIL
      // 
      this.ButtonsIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonsIL.ImageStream")));
      this.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico");
      // 
      // m_axisRightClickMenu
      // 
      this.m_axisRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_createAxisElemMenu,
            this.m_deleteAxisElemMenu,
            this.ToolStripSeparator1,
            this.m_renameAxisElemMenu,
            this.ToolStripSeparator5,
            this.m_copyDownMenu,
            this.ToolStripSeparator4,
            this.m_dropToExcelMenu,
            this.m_autoResizeMenu,
            this.m_expandAllMenu,
            this.m_collapseAllMenu});
      this.m_axisRightClickMenu.Name = "ContextMenuStripTGV";
      this.m_axisRightClickMenu.Size = new System.Drawing.Size(182, 220);
      // 
      // m_createAxisElemMenu
      // 
      this.m_createAxisElemMenu.Image = global::FBI.Properties.Resources.element_branch2_add;
      this.m_createAxisElemMenu.Name = "m_createAxisElemMenu";
      this.m_createAxisElemMenu.Size = new System.Drawing.Size(181, 22);
      this.m_createAxisElemMenu.Text = "Create";
      this.m_createAxisElemMenu.Click += new System.EventHandler(this.OnClickCreate);
      // 
      // m_deleteAxisElemMenu
      // 
      this.m_deleteAxisElemMenu.Image = global::FBI.Properties.Resources.element_branch2_delete;
      this.m_deleteAxisElemMenu.Name = "m_deleteAxisElemMenu";
      this.m_deleteAxisElemMenu.Size = new System.Drawing.Size(181, 22);
      this.m_deleteAxisElemMenu.Text = "Delete";
      this.m_deleteAxisElemMenu.Click += new System.EventHandler(this.OnClickDelete);
      // 
      // ToolStripSeparator1
      // 
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(178, 6);
      // 
      // m_renameAxisElemMenu
      // 
      this.m_renameAxisElemMenu.Name = "m_renameAxisElemMenu";
      this.m_renameAxisElemMenu.Size = new System.Drawing.Size(181, 22);
      this.m_renameAxisElemMenu.Text = "Rename";
      // 
      // ToolStripSeparator5
      // 
      this.ToolStripSeparator5.Name = "ToolStripSeparator5";
      this.ToolStripSeparator5.Size = new System.Drawing.Size(178, 6);
      // 
      // m_copyDownMenu
      // 
      this.m_copyDownMenu.Image = global::FBI.Properties.Resources.Download;
      this.m_copyDownMenu.Name = "m_copyDownMenu";
      this.m_copyDownMenu.Size = new System.Drawing.Size(181, 22);
      this.m_copyDownMenu.Text = "Copy down";
      // 
      // ToolStripSeparator4
      // 
      this.ToolStripSeparator4.Name = "ToolStripSeparator4";
      this.ToolStripSeparator4.Size = new System.Drawing.Size(178, 6);
      // 
      // m_dropToExcelMenu
      // 
      this.m_dropToExcelMenu.Image = global::FBI.Properties.Resources.excel_blue2;
      this.m_dropToExcelMenu.Name = "m_dropToExcelMenu";
      this.m_dropToExcelMenu.Size = new System.Drawing.Size(181, 22);
      this.m_dropToExcelMenu.Text = "Drop on excel";
      // 
      // m_autoResizeMenu
      // 
      this.m_autoResizeMenu.Name = "m_autoResizeMenu";
      this.m_autoResizeMenu.Size = new System.Drawing.Size(181, 22);
      this.m_autoResizeMenu.Text = "Auto resize columns";
      // 
      // m_expandAllMenu
      // 
      this.m_expandAllMenu.Name = "m_expandAllMenu";
      this.m_expandAllMenu.Size = new System.Drawing.Size(181, 22);
      this.m_expandAllMenu.Text = "Expand all";
      // 
      // m_collapseAllMenu
      // 
      this.m_collapseAllMenu.Name = "m_collapseAllMenu";
      this.m_collapseAllMenu.Size = new System.Drawing.Size(181, 22);
      this.m_collapseAllMenu.Text = "Collapse all";
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.ColumnCount = 1;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Controls.Add(this.MenuStrip1, 0, 0);
      this.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 2;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(516, 420);
      this.TableLayoutPanel1.TabIndex = 7;
      // 
      // MenuStrip1
      // 
      this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_axisEditionButton,
            this.ExcelToolStripMenuItem});
      this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
      this.MenuStrip1.Name = "MenuStrip1";
      this.MenuStrip1.Size = new System.Drawing.Size(516, 24);
      this.MenuStrip1.TabIndex = 7;
      this.MenuStrip1.Text = "MenuStrip1";
      // 
      // m_axisEditionButton
      // 
      this.m_axisEditionButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_createNewAxisElemMenuTop,
            this.m_deleteAxisElemMenuTop,
            this.ToolStripSeparator2});
      this.m_axisEditionButton.Image = global::FBI.Properties.Resources.element_branch23;
      this.m_axisEditionButton.Name = "m_axisEditionButton";
      this.m_axisEditionButton.Size = new System.Drawing.Size(73, 20);
      this.m_axisEditionButton.Text = "Entities";
      // 
      // m_createNewAxisElemMenuTop
      // 
      this.m_createNewAxisElemMenuTop.Image = global::FBI.Properties.Resources.plus;
      this.m_createNewAxisElemMenuTop.Name = "m_createNewAxisElemMenuTop";
      this.m_createNewAxisElemMenuTop.Size = new System.Drawing.Size(108, 22);
      this.m_createNewAxisElemMenuTop.Text = "Create";
      // 
      // m_deleteAxisElemMenuTop
      // 
      this.m_deleteAxisElemMenuTop.Image = global::FBI.Properties.Resources.imageres_89;
      this.m_deleteAxisElemMenuTop.Name = "m_deleteAxisElemMenuTop";
      this.m_deleteAxisElemMenuTop.Size = new System.Drawing.Size(108, 22);
      this.m_deleteAxisElemMenuTop.Text = "Delete";
      // 
      // ToolStripSeparator2
      // 
      this.ToolStripSeparator2.Name = "ToolStripSeparator2";
      this.ToolStripSeparator2.Size = new System.Drawing.Size(105, 6);
      // 
      // ExcelToolStripMenuItem
      // 
      this.ExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_dropToExcelMenuTop});
      this.ExcelToolStripMenuItem.Image = global::FBI.Properties.Resources.Excel_dark_24_24;
      this.ExcelToolStripMenuItem.Name = "ExcelToolStripMenuItem";
      this.ExcelToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
      this.ExcelToolStripMenuItem.Text = "Excel";
      // 
      // m_dropToExcelMenuTop
      // 
      this.m_dropToExcelMenuTop.Image = global::FBI.Properties.Resources.excel_blue2;
      this.m_dropToExcelMenuTop.Name = "m_dropToExcelMenuTop";
      this.m_dropToExcelMenuTop.Size = new System.Drawing.Size(146, 22);
      this.m_dropToExcelMenuTop.Text = "Drop on excel";
      // 
      // AxisView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
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
    internal System.Windows.Forms.ToolStripMenuItem m_copyDownMenu;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator5;
    internal System.Windows.Forms.ToolStripMenuItem m_dropToExcelMenu;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator4;
    internal System.Windows.Forms.ToolStripMenuItem m_createAxisElemMenu;
    internal System.Windows.Forms.ToolStripMenuItem m_deleteAxisElemMenu;
    internal System.Windows.Forms.MenuStrip MenuStrip1;
    internal System.Windows.Forms.ToolStripMenuItem m_axisEditionButton;
    internal System.Windows.Forms.ToolStripMenuItem m_createNewAxisElemMenuTop;
    internal System.Windows.Forms.ToolStripMenuItem m_deleteAxisElemMenuTop;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator2;
    internal System.Windows.Forms.ToolStripMenuItem ExcelToolStripMenuItem;
    internal System.Windows.Forms.ToolStripMenuItem m_dropToExcelMenuTop;
    internal System.Windows.Forms.ToolStripMenuItem m_renameAxisElemMenu;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    internal System.Windows.Forms.ToolStripMenuItem m_autoResizeMenu;
    internal System.Windows.Forms.ToolStripMenuItem m_expandAllMenu;
    internal System.Windows.Forms.ToolStripMenuItem m_collapseAllMenu;

    protected System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
  }
}