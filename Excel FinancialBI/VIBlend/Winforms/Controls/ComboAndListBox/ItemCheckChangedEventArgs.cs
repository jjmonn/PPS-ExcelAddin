// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ItemCheckChangedEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class ItemCheckChangedEventArgs : EventArgs
  {
    private ListItem item;

    public ListItem Item
    {
      get
      {
        return this.item;
      }
    }

    public ItemCheckChangedEventArgs(ListItem item)
    {
      this.item = item;
    }
  }
}
