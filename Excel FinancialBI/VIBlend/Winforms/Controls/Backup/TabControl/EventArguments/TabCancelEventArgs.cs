// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTabCancelEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a tab control cancel event arguments</summary>
  public class vTabCancelEventArgs : vTabEventArgs
  {
    private bool cancel;

    /// <summary>Gets or sets whether to cancel the event.</summary>
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
    /// <param name="page">Tab page associated with the event.</param>
    public vTabCancelEventArgs(vTabPage page)
      : base(page)
    {
    }
  }
}
