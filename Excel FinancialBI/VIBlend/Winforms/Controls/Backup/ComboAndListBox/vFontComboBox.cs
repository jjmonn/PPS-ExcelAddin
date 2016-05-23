// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vFontComboBox
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vFontComboBox control</summary>
  [Designer("VIBlend.WinForms.Controls.Design.vListControlDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vFontComboBox), "ControlIcons.vFontComboBox.ico")]
  [Description("Displays an editable text box, and a drop-down list of fonts.")]
  public class vFontComboBox : vComboBox
  {
    private bool useDropDownPreferredSize = true;

    /// <summary>
    /// Gets or sets a value indicating whether to use the default size of the drop down
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use the default size of the drop down")]
    public bool UseDropDownDefaultSize
    {
      get
      {
        return this.useDropDownPreferredSize;
      }
      set
      {
        this.useDropDownPreferredSize = value;
      }
    }

    /// <summary>Gets or sets the selected font.</summary>
    /// <value>The selected font.</value>
    [Category("Behavior")]
    [Description("Gets or sets the selected font.")]
    public virtual Font SelectedFont
    {
      get
      {
        return new Font(this.Text, 8f);
      }
      set
      {
        this.Text = value.Name;
        this.Invalidate();
      }
    }

    static vFontComboBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vFontComboBox" /> class.
    /// </summary>
    public vFontComboBox()
    {
      this.ItemHeight = 22;
      this.SelectedIndexChanged += new EventHandler(this.vFontComboBox_SelectedIndexChanged);
      this.DropDown.DropDownOpen += new EventHandler(this.DropDown_DropDownOpen);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (!this.DesignMode && this.Items.Count == 0)
        this.FillFont();
      base.OnLayout(levent);
    }

    private void DropDown_DropDownOpen(object sender, EventArgs e)
    {
      if (!this.useDropDownPreferredSize)
        return;
      this.DropDown.Size = this.DropDown.PreferredSize;
    }

    private void vFontComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.EditBase.Font = this.SelectedFont;
    }

    private void FillFont()
    {
      try
      {
        List<ListItem> listItemList = new List<ListItem>();
        this.Items.Clear();
        int num1 = 0;
        int num2 = -1;
        foreach (FontFamily family in FontFamily.Families)
        {
          if (family.IsStyleAvailable(FontStyle.Regular))
          {
            this.Items.Add(new ListItem()
            {
              Font = new Font(family, 15f),
              Text = family.Name
            });
            if (this.Text == family.Name)
              num2 = num1;
            ++num1;
          }
        }
        this.SelectedIndex = num2;
      }
      catch
      {
      }
    }
  }
}
