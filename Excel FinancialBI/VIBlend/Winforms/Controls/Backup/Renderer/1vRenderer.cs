// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.GradientColors
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  internal class GradientColors
  {
    public Color Color1;
    public Color Color2;
    public Color Color3;
    public Color Color4;
    public Color FillColor1;
    public Color FillColor2;
    public Color FillColor3;
    public Color FillColor4;
    public Color BorderColor1;
    public Color BorderColor2;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.GradientColors" /> class.
    /// </summary>
    public GradientColors()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.GradientColors" /> class.
    /// </summary>
    /// <param name="color1">The color1.</param>
    /// <param name="color2">The color2.</param>
    /// <param name="color3">The color3.</param>
    /// <param name="color4">The color4.</param>
    /// <param name="fillColor1">The fill color1.</param>
    /// <param name="fillColor2">The fill color2.</param>
    /// <param name="fillColor3">The fill color3.</param>
    /// <param name="fillColor4">The fill color4.</param>
    /// <param name="border1">The border1.</param>
    /// <param name="border2">The border2.</param>
    public GradientColors(Color color1, Color color2, Color color3, Color color4, Color fillColor1, Color fillColor2, Color fillColor3, Color fillColor4, Color border1, Color border2)
    {
      this.Color1 = color1;
      this.Color2 = color2;
      this.Color3 = color3;
      this.Color4 = color4;
      this.FillColor1 = fillColor1;
      this.FillColor2 = fillColor2;
      this.FillColor3 = fillColor3;
      this.FillColor4 = fillColor4;
      this.BorderColor1 = border1;
      this.BorderColor2 = border2;
    }
  }
}
