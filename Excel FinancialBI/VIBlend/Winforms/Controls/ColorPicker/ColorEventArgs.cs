// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ColorEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  public class ColorEventArgs : EventArgs
  {
    private Color color;

    /// <summary>Gets or sets the color.</summary>
    /// <value>The color.</value>
    public Color Color
    {
      get
      {
        return this.color;
      }
      set
      {
        this.color = value;
      }
    }

    public ColorEventArgs(Color color)
    {
      this.color = color;
    }
  }
}
