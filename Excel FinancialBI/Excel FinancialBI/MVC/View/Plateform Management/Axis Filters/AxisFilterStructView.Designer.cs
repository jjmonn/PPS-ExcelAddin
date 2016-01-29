using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class AxisFilterStructView : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AxisFilterStructView));
      this.AddBT = new VIBlend.WinForms.Controls.vButton();
      this.EditButtonsImagelist = new System.Windows.Forms.ImageList(this.components);
      this.DeleteBT = new VIBlend.WinForms.Controls.vButton();
      this.VPanel1 = new VIBlend.WinForms.Controls.vPanel();
      this.VPanel2 = new VIBlend.WinForms.Controls.vPanel();
      this.m_structureTreeviewRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.m_createCategoryUnderCurrentCategoryButton = new System.Windows.Forms.ToolStripMenuItem();
      this.m_renameButton = new System.Windows.Forms.ToolStripMenuItem();
      this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
      this.m_deleteButton = new System.Windows.Forms.ToolStripMenuItem();
      this.VPanel1.Content.SuspendLayout();
      this.VPanel1.SuspendLayout();
      this.VPanel2.SuspendLayout();
      this.m_structureTreeviewRightClickMenu.SuspendLayout();
      this.SuspendLayout();
      //
      //AddBT
      //
      this.AddBT.AllowAnimations = true;
      this.AddBT.BackColor = System.Drawing.Color.Transparent;
      this.AddBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.AddBT.ImageKey = "1420498403_340208.ico";
      this.AddBT.ImageList = this.EditButtonsImagelist;
      this.AddBT.Location = new System.Drawing.Point(10, 7);
      this.AddBT.Name = "AddBT";
      this.AddBT.RoundedCornersMask = Convert.ToByte(15);
      this.AddBT.Size = new System.Drawing.Size(93, 25);
      this.AddBT.TabIndex = 0;
      this.AddBT.Text = "New";
      this.AddBT.UseVisualStyleBackColor = false;
      this.AddBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //EditButtonsImagelist
      //
      this.EditButtonsImagelist.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("EditButtonsImagelist.ImageStream");
      this.EditButtonsImagelist.TransparentColor = System.Drawing.Color.Transparent;
      this.EditButtonsImagelist.Images.SetKeyName(0, "1420498403_340208.ico");
      this.EditButtonsImagelist.Images.SetKeyName(1, "imageres_89.ico");
      //
      //DeleteBT
      //
      this.DeleteBT.AllowAnimations = true;
      this.DeleteBT.BackColor = System.Drawing.Color.Transparent;
      this.DeleteBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.DeleteBT.ImageKey = "imageres_89.ico";
      this.DeleteBT.ImageList = this.EditButtonsImagelist;
      this.DeleteBT.Location = new System.Drawing.Point(110, 7);
      this.DeleteBT.Name = "DeleteBT";
      this.DeleteBT.RoundedCornersMask = Convert.ToByte(15);
      this.DeleteBT.Size = new System.Drawing.Size(93, 25);
      this.DeleteBT.TabIndex = 1;
      this.DeleteBT.Text = "Delete";
      this.DeleteBT.UseVisualStyleBackColor = false;
      this.DeleteBT.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //VPanel1
      //
      this.VPanel1.AllowAnimations = true;
      this.VPanel1.BorderRadius = 0;
      //
      //VPanel1.Content
      //
      this.VPanel1.Content.AutoScroll = true;
      this.VPanel1.Content.BackColor = System.Drawing.SystemColors.Control;
      this.VPanel1.Content.Controls.Add(this.AddBT);
      this.VPanel1.Content.Controls.Add(this.DeleteBT);
      this.VPanel1.Content.Location = new System.Drawing.Point(1, 1);
      this.VPanel1.Content.Name = "Content";
      this.VPanel1.Content.Size = new System.Drawing.Size(311, 37);
      this.VPanel1.Content.TabIndex = 3;
      this.VPanel1.CustomScrollersIntersectionColor = System.Drawing.Color.Empty;
      this.VPanel1.Location = new System.Drawing.Point(0, 0);
      this.VPanel1.Name = "VPanel1";
      this.VPanel1.Opacity = 1f;
      this.VPanel1.PanelBorderColor = System.Drawing.Color.Transparent;
      this.VPanel1.Size = new System.Drawing.Size(313, 39);
      this.VPanel1.TabIndex = 0;
      this.VPanel1.Text = "VPanel1";
      this.VPanel1.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //VPanel2
      //
      this.VPanel2.AllowAnimations = true;
      this.VPanel2.BorderRadius = 0;
      //
      //VPanel2.Content
      //
      this.VPanel2.Content.AutoScroll = true;
      this.VPanel2.Content.BackColor = System.Drawing.SystemColors.Control;
      this.VPanel2.Content.Location = new System.Drawing.Point(1, 1);
      this.VPanel2.Content.Name = "Content";
      this.VPanel2.Content.Size = new System.Drawing.Size(311, 422);
      this.VPanel2.Content.TabIndex = 3;
      this.VPanel2.CustomScrollersIntersectionColor = System.Drawing.Color.Empty;
      this.VPanel2.Location = new System.Drawing.Point(0, 39);
      this.VPanel2.Name = "VPanel2";
      this.VPanel2.Opacity = 1f;
      this.VPanel2.PanelBorderColor = System.Drawing.Color.Transparent;
      this.VPanel2.Size = new System.Drawing.Size(313, 424);
      this.VPanel2.TabIndex = 1;
      this.VPanel2.Text = "VPanel2";
      this.VPanel2.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.OFFICESILVER;
      //
      //m_structureTreeviewRightClickMenu
      //
      this.m_structureTreeviewRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.m_createCategoryUnderCurrentCategoryButton,
			this.m_renameButton,
			this.ToolStripSeparator1,
			this.m_deleteButton
		});
      this.m_structureTreeviewRightClickMenu.Name = "ContextMenuStripTV";
      this.m_structureTreeviewRightClickMenu.Size = new System.Drawing.Size(307, 82);
      //
      //m_createCategoryUnderCurrentCategoryButton
      //
      this.m_createCategoryUnderCurrentCategoryButton.Image = global::FBI.Properties.Resources.@add;
      this.m_createCategoryUnderCurrentCategoryButton.Name = "m_createCategoryUnderCurrentCategoryButton";
      this.m_createCategoryUnderCurrentCategoryButton.Size = new System.Drawing.Size(306, 24);
      this.m_createCategoryUnderCurrentCategoryButton.Text = "Create_category_under_category";
      //
      //m_renameButton
      //
      this.m_renameButton.Name = "m_renameButton";
      this.m_renameButton.Size = new System.Drawing.Size(306, 24);
      this.m_renameButton.Text = "Renamme";
      //
      //ToolStripSeparator1
      //
      this.ToolStripSeparator1.Name = "ToolStripSeparator1";
      this.ToolStripSeparator1.Size = new System.Drawing.Size(264, 6);
      //
      //m_deleteButton
      //
      this.m_deleteButton.Image = global::FBI.Properties.Resources.imageres_891;
      this.m_deleteButton.Name = "m_deleteButton";
      this.m_deleteButton.Size = new System.Drawing.Size(306, 24);
      this.m_deleteButton.Text = "Delete";
      //
      //AxisFilterStructView
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(312, 463);
      this.Controls.Add(this.VPanel2);
      this.Controls.Add(this.VPanel1);
      this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
      this.Name = "AxisFilterStructView";
      this.Text = "Categories";
      this.VPanel1.Content.ResumeLayout(false);
      this.VPanel1.ResumeLayout(false);
      this.VPanel2.ResumeLayout(false);
      this.m_structureTreeviewRightClickMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal VIBlend.WinForms.Controls.vButton AddBT;
    internal VIBlend.WinForms.Controls.vButton DeleteBT;
    internal VIBlend.WinForms.Controls.vPanel VPanel1;
    internal VIBlend.WinForms.Controls.vPanel VPanel2;
    internal System.Windows.Forms.ContextMenuStrip m_structureTreeviewRightClickMenu;
    internal System.Windows.Forms.ToolStripMenuItem m_createCategoryUnderCurrentCategoryButton;
    internal System.Windows.Forms.ToolStripMenuItem m_renameButton;
    internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
    internal System.Windows.Forms.ToolStripMenuItem m_deleteButton;
    internal System.Windows.Forms.ImageList EditButtonsImagelist;
  }
}