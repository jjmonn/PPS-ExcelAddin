// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ListItemFormatEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class ListItemFormatEventArgs : EventArgs
  {
    private string itemString;
    private ListItem item;

    /// <summary>Gets or sets the item string.</summary>
    /// <value>The item string.</value>
    public string ItemString
    {
      get
      {
        return this.itemString;
      }
      set
      {
        this.itemString = value;
      }
    }

    /// <summary>Gets or sets the item.</summary>
    /// <value>The item.</value>
    public ListItem Item
    {
      get
      {
        return this.item;
      }
      set
      {
        this.item = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.ListItemFormatEventArgs" /> class.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="itemString">The item string.</param>
    public ListItemFormatEventArgs(ListItem item, string itemString)
    {
      this.itemString = itemString;
      this.item = item;
    }
  }
}
