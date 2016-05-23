// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vSizeType
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents an extension of the standard Size class.</summary>
  public class vSizeType
  {
    private Size size = new Size(1, 1);
    private SizeUnitType sizeType = SizeUnitType.Star;

    /// <summary>Gets or sets the type of the size.</summary>
    /// <value>The type of the size.</value>
    public SizeUnitType UnitType
    {
      get
      {
        return this.sizeType;
      }
      set
      {
        this.sizeType = value;
      }
    }

    /// <summary>Gets or sets the size.</summary>
    /// <value>The size.</value>
    public Size Size
    {
      get
      {
        return this.size;
      }
      set
      {
        this.size = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:SizeType" /> class.
    /// </summary>
    public vSizeType()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:SizeType" /> class.
    /// </summary>
    /// <param name="size">The size.</param>
    /// <param name="sizeType">Type of the size.</param>
    public vSizeType(Size size, SizeUnitType sizeType)
    {
      this.Size = size;
      this.UnitType = sizeType;
    }
  }
}
