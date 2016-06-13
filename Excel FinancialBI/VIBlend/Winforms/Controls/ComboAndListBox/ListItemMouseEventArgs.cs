// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ListItemMouseEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class ListItemMouseEventArgs : EventArgs
  {
    private ListItem item;
    private MouseEventArgs args;

    /// <summary>Gets the item.</summary>
    /// <value>The item.</value>
    public ListItem Item
    {
      get
      {
        return this.item;
      }
    }

    /// <summary>Gets the mouse event args.</summary>
    /// <value>The mouse event args.</value>
    public MouseEventArgs MouseEventArgs
    {
      get
      {
        return this.args;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.ListItemMouseEventArgs" /> class.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="args">The <see cref="T:System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
    public ListItemMouseEventArgs(ListItem item, MouseEventArgs args)
    {
      this.item = item;
      this.args = args;
    }
  }
}
