// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DragCancelEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a drag and drop cancel event argument</summary>
  public class DragCancelEventArgs : vTabEventArgs
  {
    private vTabPage targetTab;
    private bool cancel;

    /// <summary>Gets or sets whether the even is canceled</summary>
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

    /// <summary>The drop target tab page.</summary>
    public vTabPage TargetTabPage
    {
      get
      {
        return this.targetTab;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="sourcePage">Drag source tab page.</param>
    /// <param name="targetPage">Drop target tab page.</param>
    public DragCancelEventArgs(vTabPage sourcePage, vTabPage targetPage)
      : base(sourcePage)
    {
      this.targetTab = targetPage;
    }
  }
}
