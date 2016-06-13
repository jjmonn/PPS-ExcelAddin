// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.RECT
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  public struct RECT
  {
    internal int left;
    internal int top;
    internal int right;
    internal int bottom;

    internal Rectangle ToRect()
    {
      return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
    }
  }
}
