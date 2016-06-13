// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTabMouseEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class vTabMouseEventArgs : vTabEventArgs
  {
    private MouseEventArgs args;

    /// <summary>Gets the drag source tab page.</summary>
    public MouseEventArgs MouseEventArgs
    {
      get
      {
        return this.args;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="sourcePage">Drag source tab page.</param>
    public vTabMouseEventArgs(vTabPage page, MouseEventArgs args)
      : base(page)
    {
      this.args = args;
    }
  }
}
