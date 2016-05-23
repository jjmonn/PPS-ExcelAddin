// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vNavItemEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vNavPane event arguments</summary>
  public class vNavItemEventArgs : EventArgs
  {
    private vNavPaneItem item;

    /// <summary>NavigationPane item associated with the event.</summary>
    public vNavPaneItem Item
    {
      get
      {
        return this.item;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="item">NavigationPane item associated with the event.</param>
    public vNavItemEventArgs(vNavPaneItem item)
    {
      this.item = item;
    }
  }
}
