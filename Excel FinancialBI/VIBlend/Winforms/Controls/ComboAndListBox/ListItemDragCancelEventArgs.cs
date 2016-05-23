// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ListItemDragCancelEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class ListItemDragCancelEventArgs : CancelEventArgs
  {
    private ListItem targetItem;
    private ListItem sourceItem;

    /// <summary>Gets the instance of the new selected item.</summary>
    [Description("Gets the instance of the new selected item.")]
    public ListItem TargetItem
    {
      get
      {
        return this.targetItem;
      }
    }

    /// <summary>Gets the instance of the old selected item.</summary>
    [Description("Gets the instance of the old selected item.")]
    public ListItem SourceItem
    {
      get
      {
        return this.sourceItem;
      }
    }

    /// <exclude />
    public ListItemDragCancelEventArgs(ListItem sourceItem, ListItem targetItem)
    {
      this.targetItem = targetItem;
      this.sourceItem = sourceItem;
    }
  }
}
