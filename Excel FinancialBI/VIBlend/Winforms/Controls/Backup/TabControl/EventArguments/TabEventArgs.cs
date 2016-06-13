// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTabEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represent a vTabControl event arguments for drag and drop operations.
  /// </summary>
  public class vTabEventArgs : EventArgs
  {
    private vTabPage draggingTab;

    /// <summary>Gets the drag source tab page.</summary>
    public vTabPage TabPage
    {
      get
      {
        return this.draggingTab;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="sourcePage">Drag source tab page.</param>
    public vTabEventArgs(vTabPage sourcePage)
    {
      this.draggingTab = sourcePage;
    }
  }
}
