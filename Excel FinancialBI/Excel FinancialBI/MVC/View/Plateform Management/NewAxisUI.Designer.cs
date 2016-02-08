using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace FBI.MVC.View
{
  partial class NewAxisUI : System.Windows.Forms.Form
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewAxisUI));
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.m_parentAxisLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_nameTextBox = new VIBlend.WinForms.Controls.vTextBox();
      this.m_nameLabel = new VIBlend.WinForms.Controls.vLabel();
      this.m_parentAxisElemTreeviewBox = new VIBlend.WinForms.Controls.vTreeViewBox();
      this.ButtonIcons = new System.Windows.Forms.ImageList(this.components);
      this.CancelBT = new System.Windows.Forms.Button();
      this.ButtonsIL = new System.Windows.Forms.ImageList(this.components);
      this.CreateAxisBT = new System.Windows.Forms.Button();
      this.TableLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
      this.TableLayoutPanel1.ColumnCount = 2;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.51376F));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.48624F));
      this.TableLayoutPanel1.Controls.Add(this.m_parentAxisLabel, 0, 1);
      this.TableLayoutPanel1.Controls.Add(this.m_nameTextBox, 1, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_nameLabel, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.m_parentAxisElemTreeviewBox, 1, 1);
      this.TableLayoutPanel1.Location = new System.Drawing.Point(22, 25);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(482, 90);
      this.TableLayoutPanel1.TabIndex = 18;
      // 
      // m_parentAxisLabel
      // 
      this.m_parentAxisLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_parentAxisLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_parentAxisLabel.Ellipsis = false;
      this.m_parentAxisLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_parentAxisLabel.Location = new System.Drawing.Point(3, 33);
      this.m_parentAxisLabel.Multiline = true;
      this.m_parentAxisLabel.Name = "m_parentAxisLabel";
      this.m_parentAxisLabel.Size = new System.Drawing.Size(169, 24);
      this.m_parentAxisLabel.TabIndex = 5;
      this.m_parentAxisLabel.Text = "parent_entity";
      this.m_parentAxisLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_parentAxisLabel.UseMnemonics = true;
      this.m_parentAxisLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_nameTextBox
      // 
      this.m_nameTextBox.BackColor = System.Drawing.Color.White;
      this.m_nameTextBox.BoundsOffset = new System.Drawing.Size(1, 1);
      this.m_nameTextBox.ControlBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
      this.m_nameTextBox.DefaultText = "Empty...";
      this.m_nameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_nameTextBox.Location = new System.Drawing.Point(178, 3);
      this.m_nameTextBox.MaxLength = 32767;
      this.m_nameTextBox.Name = "m_nameTextBox";
      this.m_nameTextBox.PasswordChar = '\0';
      this.m_nameTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.m_nameTextBox.SelectionLength = 0;
      this.m_nameTextBox.SelectionStart = 0;
      this.m_nameTextBox.Size = new System.Drawing.Size(301, 24);
      this.m_nameTextBox.TabIndex = 0;
      this.m_nameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
      this.m_nameTextBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_nameLabel
      // 
      this.m_nameLabel.BackColor = System.Drawing.Color.Transparent;
      this.m_nameLabel.DisplayStyle = VIBlend.WinForms.Controls.LabelItemStyle.TextOnly;
      this.m_nameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.m_nameLabel.Ellipsis = false;
      this.m_nameLabel.ImageAlignment = System.Drawing.ContentAlignment.TopLeft;
      this.m_nameLabel.Location = new System.Drawing.Point(3, 3);
      this.m_nameLabel.Multiline = true;
      this.m_nameLabel.Name = "m_nameLabel";
      this.m_nameLabel.Size = new System.Drawing.Size(169, 24);
      this.m_nameLabel.TabIndex = 1;
      this.m_nameLabel.Text = "Name";
      this.m_nameLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
      this.m_nameLabel.UseMnemonics = true;
      this.m_nameLabel.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // m_parentAxisElemTreeviewBox
      // 
      this.m_parentAxisElemTreeviewBox.BackColor = System.Drawing.Color.White;
      this.m_parentAxisElemTreeviewBox.BorderColor = System.Drawing.Color.Black;
      this.m_parentAxisElemTreeviewBox.DropDownMaximumSize = new System.Drawing.Size(1000, 1000);
      this.m_parentAxisElemTreeviewBox.DropDownMinimumSize = new System.Drawing.Size(10, 10);
      this.m_parentAxisElemTreeviewBox.DropDownResizeDirection = VIBlend.WinForms.Controls.SizingDirection.Both;
      this.m_parentAxisElemTreeviewBox.Location = new System.Drawing.Point(178, 33);
      this.m_parentAxisElemTreeviewBox.Name = "m_parentAxisElemTreeviewBox";
      this.m_parentAxisElemTreeviewBox.Size = new System.Drawing.Size(301, 23);
      this.m_parentAxisElemTreeviewBox.TabIndex = 4;
      this.m_parentAxisElemTreeviewBox.Text = "parent_entity_selection";
      this.m_parentAxisElemTreeviewBox.UseThemeBackColor = false;
      this.m_parentAxisElemTreeviewBox.UseThemeDropDownArrowColor = true;
      this.m_parentAxisElemTreeviewBox.VIBlendTheme = VIBlend.Utilities.VIBLEND_THEME.VISTABLUE;
      // 
      // ButtonIcons
      // 
      this.ButtonIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonIcons.ImageStream")));
      this.ButtonIcons.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonIcons.Images.SetKeyName(0, "imageres_89.ico");
      this.ButtonIcons.Images.SetKeyName(1, "favicon(95).ico");
      this.ButtonIcons.Images.SetKeyName(2, "submit 1 ok.ico");
      this.ButtonIcons.Images.SetKeyName(3, "favicon(97).ico");
      this.ButtonIcons.Images.SetKeyName(4, "imageres_99.ico");
      // 
      // CancelBT
      // 
      this.CancelBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CancelBT.ImageKey = "delete-2-xxl.png";
      this.CancelBT.ImageList = this.ButtonsIL;
      this.CancelBT.Location = new System.Drawing.Point(314, 136);
      this.CancelBT.Name = "CancelBT";
      this.CancelBT.Size = new System.Drawing.Size(92, 30);
      this.CancelBT.TabIndex = 21;
      this.CancelBT.Text = "Cancel";
      this.CancelBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.CancelBT.UseVisualStyleBackColor = true;
      this.CancelBT.Click += new System.EventHandler(this.CancelBT_Click);
      // 
      // ButtonsIL
      // 
      this.ButtonsIL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonsIL.ImageStream")));
      this.ButtonsIL.TransparentColor = System.Drawing.Color.Transparent;
      this.ButtonsIL.Images.SetKeyName(0, "submit 1 ok.ico");
      this.ButtonsIL.Images.SetKeyName(1, "delete-2-xxl.png");
      this.ButtonsIL.Images.SetKeyName(2, "favicon(97).ico");
      // 
      // CreateAxisBT
      // 
      this.CreateAxisBT.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.CreateAxisBT.ImageKey = "submit 1 ok.ico";
      this.CreateAxisBT.ImageList = this.ButtonsIL;
      this.CreateAxisBT.Location = new System.Drawing.Point(412, 136);
      this.CreateAxisBT.Name = "CreateAxisBT";
      this.CreateAxisBT.Size = new System.Drawing.Size(92, 30);
      this.CreateAxisBT.TabIndex = 20;
      this.CreateAxisBT.Text = "Create";
      this.CreateAxisBT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.CreateAxisBT.UseVisualStyleBackColor = true;
      this.CreateAxisBT.Click += new System.EventHandler(this.CreateAxisBT_Click);
      // 
      // NewAxisUI
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(523, 178);
      this.Controls.Add(this.TableLayoutPanel1);
      this.Controls.Add(this.CancelBT);
      this.Controls.Add(this.CreateAxisBT);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "NewAxisUI";
      this.Text = "New_entity";
      this.TableLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
    internal System.Windows.Forms.Button CancelBT;
    internal System.Windows.Forms.Button CreateAxisBT;
    internal System.Windows.Forms.ImageList ButtonsIL;
    internal System.Windows.Forms.ImageList ButtonIcons;
    internal VIBlend.WinForms.Controls.vTextBox m_nameTextBox;
    internal VIBlend.WinForms.Controls.vLabel m_nameLabel;
    internal VIBlend.WinForms.Controls.vLabel m_parentAxisLabel;
    internal VIBlend.WinForms.Controls.vTreeViewBox m_parentAxisElemTreeviewBox;
  }
}