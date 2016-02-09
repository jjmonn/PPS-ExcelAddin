using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class FilterStructView : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterStructView));
      this.m_addFilter = new VIBlend.WinForms.Controls.vButton();
      this.EditButtonsImagelist = new System.Windows.Forms.ImageList(this.components);
      this.m_deleteFilter = new VIBlend.WinForms.Controls.vButton();
      this.VPanel1 = new VIBlend.WinForms.Controls.vPanel();
      this.m_filterPanel = new VIBlend.WinForms.Controls.vPanel();
      this.m_structureTreeviewRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.m_createSubCategory = new System.Windows.Forms.ToolStripMenuItem();
      this.m_renameButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.m_deleteButton = new System.Windows.Forms.ToolStripMenuItem();
      this.VPanel1.Content.SuspendLayout();
      this.VPanel1.SuspendLayout();
      this.m_filterPanel.SuspendLayout();
      this.m_structureTreeviewRightClickMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // m_addFilter
      // 
      this.m_addFilter.AllowAnimations = true;
      this.m_addFilter.BackColor = System.Drawing.Color.Transparent;
      this.m_addFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_addFilter.ImageKey = "1420498403_340208.ico";
      this.m_addFilter.ImageList = this.EditButtonsImagelist;
      this.m_addFilter.Location = new System.Drawing.Point(10, 7);
      this.m_addFilter.Name = "m_addFilter";
      this.m_addFilter.RoundedCornersMask = ((byte)(15));
      this.m_addFilter.Size = new System.Drawing.Size(93, 25);
      this.m_addFilter.TabIndex = 0;
      this.m_addFilter.Text = "New";
      this.m_addFilter.UseVisualStyleBackColor = false;
      this.m_addFilter.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // EditButtonsImagelist
      // 
      this.EditButtonsImagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("EditButtonsImagelist.ImageStream")));
      this.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent;
      this.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico");
      this.EditButtonsImagelist.Images.SetKeyName(1, "imageres_89.ico");
      // 
      // m_deleteFilter
      // 
      this.m_deleteFilter.AllowAnimations = true;
      this.m_deleteFilter.BackColor = System.Drawing.Color.Transparent;
      this.m_deleteFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.m_deleteFilter.ImageKey = "imageres_89.ico";
      this.m_deleteFilter.ImageList = this.EditButtonsImagelist;
      this.m_deleteFilter.Location = new System.Drawing.Point(110, 7);
      this.m_deleteFilter.Name = "m_deleteFilter";
      this.m_deleteFilter.RoundedCornersMask = ((byte)(15));
      this.m_deleteFilter.Size = new System.Drawing.Size(93, 25);
      this.m_deleteFilter.TabIndex = 1;
      this.m_deleteFilter.Text = "Delete";
      this.m_deleteFilter.UseVisualStyleBackColor = false;
      this.m_deleteFilter.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // VPanel1
      // 
      this.VPanel1.AllowAnimations = true;
      this.VPanel1.BorderRadius = 0;
      // 
      // VPanel1.Content
      // 
      this.VPanel1.Content.AutoScroll = true;
      this.VPanel1.Content.BackColor = System.Drawing.SystemColors.Control;
      this.VPanel1.Content.Controls.Add(this.m_addFilter);
      this.VPanel1.Content.Controls.Add(this.m_deleteFilter);
      this.VPanel1.Content.Location = new System.Drawing.Point(1, 1);
      this.VPanel1.Content.Name = "Content";
      this.VPanel1.Content.Size = new System.Drawing.Size(311, 37);
      this.VPanel1.Content.TabIndex = 3;
      this.VPanel1.CustomScrollersIntersectionColor = System.Drawing.Color.Empty;
      this.VPanel1.Location = new System.Drawing.Point(0, 0);
      this.VPanel1.Name = "VPanel1";
      this.VPanel1.Opacity = 1F;
      this.VPanel1.PanelBorderColor = System.Drawing.Color.Transparent;
      this.VPanel1.Size = new System.Drawing.Size(313, 39);
      this.VPanel1.TabIndex = 0;
      this.VPanel1.Text = "VPanel1";
      this.VPanel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_filterPanel
      // 
      this.m_filterPanel.AllowAnimations = true;
      this.m_filterPanel.BorderRadius = 0;
      // 
      // m_filterPanel.Content
      // 
      this.m_filterPanel.Content.AutoScroll = true;
      this.m_filterPanel.Content.BackColor = System.Drawing.SystemColors.Control;
      this.m_filterPanel.Content.Location = new System.Drawing.Point(1, 1);
      this.m_filterPanel.Content.Name = "Content";
      this.m_filterPanel.Content.Size = new System.Drawing.Size(311, 422);
      this.m_filterPanel.Content.TabIndex = 3;
      this.m_filterPanel.CustomScrollersIntersectionColor = System.Drawing.Color.Empty;
      this.m_filterPanel.Location = new System.Drawing.Point(0, 39);
      this.m_filterPanel.Name = "m_filterPanel";
      this.m_filterPanel.Opacity = 1F;
      this.m_filterPanel.PanelBorderColor = System.Drawing.Color.Transparent;
      this.m_filterPanel.Size = new System.Drawing.Size(313, 424);
      this.m_filterPanel.TabIndex = 1;
      this.m_filterPanel.Text = "VPanel2";
      this.m_filterPanel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      // 
      // m_structureTreeviewRightClickMenu
      // 
      this.m_structureTreeviewRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_createSubCategory,
            this.m_renameButton,
            this.ToolStripSeparator1,
            this.m_deleteButton});
      this.m_structureTreeviewRightClickMenu.Name = "ContextMenuStripTV";
      this.m_structureTreeviewRightClickMenu.Size = new System.Drawing.Size(247, 98);
      // 
      // m_createSubCategory
      // 
      this.m_createSubCategory.Image = global::FBI.Properties.Resources.add;
      this.m_createSubCategory.Name = "m_createSubCategory";
      this.m_createSubCategory.Size = new System.Drawing.Size(246, 22);
      this.m_createSubCategory.Text = "Create_category_under_category";
      // 
      // m_renameButton
      // 
      this.m_renameButton.Name = "m_renameButton";
      this.m_renameButton.Size = new System.Drawing.Size(246, 22);
      this.m_renameButton.Text = "Rename";
      // 
      // ToolStripSeparator1
      // 
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(243, 6);
      // 
      // m_deleteButton
      // 
      this.m_deleteButton.Image = global::FBI.Properties.Resources.imageres_891;
      this.m_deleteButton.Name = "m_deleteButton";
      this.m_deleteButton.Size = new System.Drawing.Size(246, 22);
      this.m_deleteButton.Text = "Delete";
      // 
      // FilterStructView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(312, 463);
      this.Controls.Add(this.m_filterPanel);
      this.Controls.Add(this.VPanel1);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "FilterStructView";
      this.Text = "Categories";
      this.VPanel1.Content.ResumeLayout(false);
      this.VPanel1.ResumeLayout(false);
      this.m_filterPanel.ResumeLayout(false);
      this.m_structureTreeviewRightClickMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal VIBlend.WinForms.Controls.vButton m_addFilter;
    internal VIBlend.WinForms.Controls.vButton m_deleteFilter;
    internal VIBlend.WinForms.Controls.vPanel VPanel1;
    internal VIBlend.WinForms.Controls.vPanel m_filterPanel;
    internal System.Windows.Forms.ContextMenuStrip m_structureTreeviewRightClickMenu;
    internal System.Windows.Forms.ToolStripMenuItem m_createSubCategory;
    internal System.Windows.Forms.ToolStripMenuItem m_renameButton;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    internal System.Windows.Forms.ToolStripMenuItem m_deleteButton;
    internal System.Windows.Forms.ImageList EditButtonsImagelist;
  }
}