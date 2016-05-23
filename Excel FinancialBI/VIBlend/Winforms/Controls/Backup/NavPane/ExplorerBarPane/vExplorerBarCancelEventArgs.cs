// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vExplorerBarCancelEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vNavPane event arguments</summary>
  public class vExplorerBarCancelEventArgs : vExplorerBarEventArgs
  {
    private bool cancel;

    /// <summary>Gets or sets the cancel property.</summary>
    public bool Cancel
    {
      get
      {
        return this.cancel;
      }
      set
      {
        this.cancel = value;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="item">NavigationPane item associated with the event.</param>
    public vExplorerBarCancelEventArgs(vExplorerBarItem item)
      : base(item)
    {
    }
  }
}
