// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ListItemDragEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class ListItemDragEventArgs : EventArgs
  {
    private ListItem targetItem;
    private ListItem sourceItem;

    /// <summary>Gets the target item.</summary>
    /// <value>The target item.</value>
    [Description("Gets the target item.")]
    public ListItem TargetItem
    {
      get
      {
        return this.targetItem;
      }
    }

    /// <summary>Gets the source item.</summary>
    /// <value>The source item.</value>
    [Description("Gets the source item.")]
    public ListItem SourceItem
    {
      get
      {
        return this.sourceItem;
      }
    }

    /// <exclude />
    public ListItemDragEventArgs(ListItem sourceItem, ListItem targetItem)
    {
      this.targetItem = targetItem;
      this.sourceItem = sourceItem;
    }
  }
}
