// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ListItemChangingEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class ListItemChangingEventArgs : CancelEventArgs
  {
    private ListItem newItem;
    private ListItem oldItem;

    /// <summary>Gets the instance of the new selected item.</summary>
    [Description("Gets the instance of the new selected item.")]
    public ListItem NewItem
    {
      get
      {
        return this.newItem;
      }
    }

    /// <summary>Gets the instance of the old selected item.</summary>
    [Description("Gets the instance of the old selected item.")]
    public ListItem OldItem
    {
      get
      {
        return this.oldItem;
      }
    }

    public ListItemChangingEventArgs(ListItem newItem, ListItem oldItem)
    {
      this.newItem = newItem;
      this.oldItem = oldItem;
    }
  }
}
