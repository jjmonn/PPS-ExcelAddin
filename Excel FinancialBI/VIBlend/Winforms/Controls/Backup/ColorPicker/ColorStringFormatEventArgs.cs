// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ColorStringFormatEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a color text formatting event arguments</summary>
  public class ColorStringFormatEventArgs
  {
    private Color color;
    private string formattedText;

    /// <summary>Gets the color being formatted</summary>
    public Color Color
    {
      get
      {
        return this.color;
      }
      internal set
      {
        this.color = value;
      }
    }

    /// <summary>
    /// Gets or sets the formatted text that represents the color
    /// </summary>
    public string FormattedText
    {
      get
      {
        return this.formattedText;
      }
      set
      {
        this.formattedText = value;
      }
    }

    public ColorStringFormatEventArgs(Color Color, string FormattedText)
    {
      this.color = Color;
      this.formattedText = FormattedText;
    }
  }
}
