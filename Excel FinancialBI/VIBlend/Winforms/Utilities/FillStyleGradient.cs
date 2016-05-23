// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.FillStyleGradient
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;

namespace VIBlend.Utilities
{
  /// <summary>Represents a two colors gredient fill style</summary>
  [Serializable]
  public class FillStyleGradient : FillStyle
  {
    [XmlIgnore]
    private Color[] colors = new Color[2];
    [NonSerialized]
    private LinearGradientBrush brush;
    [NonSerialized]
    private LinearGradientMode gradientMode;
    [XmlIgnore]
    private Rectangle rect;

    /// <summary>Gets the number of gradient colors</summary>
    public override int ColorsNumber
    {
      get
      {
        return this.colors.Length;
      }
    }

    /// <summary>Gets the gradient colors</summary>
    public override Color[] Colors
    {
      get
      {
        return this.colors;
      }
    }

    /// <summary>Start gradient color</summary>
    [XmlIgnore]
    public Color Color1
    {
      get
      {
        return this.colors[0];
      }
      set
      {
        this.colors[0] = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [XmlElement("Color1")]
    public string XmlColor1
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.Color1);
      }
      set
      {
        this.Color1 = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <summary>End gradient color</summary>
    [XmlIgnore]
    public Color Color2
    {
      get
      {
        return this.colors[1];
      }
      set
      {
        this.colors[1] = value;
      }
    }

    /// <exclude />
    [XmlElement("Color2")]
    [Browsable(false)]
    public string XmlColor2
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.Color2);
      }
      set
      {
        this.Color2 = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <summary>Gets or sets the Gradient mode</summary>
    [XmlIgnore]
    public LinearGradientMode LinearGradientMode
    {
      get
      {
        return this.gradientMode;
      }
      set
      {
        this.gradientMode = value;
      }
    }

    /// <summary>Default contrsuctor</summary>
    internal FillStyleGradient()
    {
      this.rect = new Rectangle(0, 0, 10, 10);
      this.gradientMode = LinearGradientMode.Vertical;
    }

    /// <summary>Constructor</summary>
    /// <param name="color1">Color1</param>
    /// <param name="color2">Color2</param>
    /// <param name="gradientMode">Gradient mode</param>
    public FillStyleGradient(Color color1, Color color2, LinearGradientMode gradientMode)
    {
      this.colors[0] = color1;
      this.colors[1] = color2;
      this.gradientMode = gradientMode;
      this.rect = new Rectangle(0, 0, 10, 10);
      this.brush = (LinearGradientBrush) null;
    }

    ~FillStyleGradient()
    {
      if (this.brush == null)
        return;
      this.brush.Dispose();
      this.brush = (LinearGradientBrush) null;
    }

    /// <exclude />
    public static implicit operator FillStyleGradient(FillStyleSolid fillStyle)
    {
      return new FillStyleGradient(fillStyle.Color, fillStyle.Color, LinearGradientMode.Vertical);
    }

    /// <exclude />
    protected override void CreateBrush()
    {
      if (this.rect.Width == 0)
        this.rect.Width = 1;
      if (this.rect.Height == 0)
        this.rect.Height = 1;
      this.brush = new LinearGradientBrush(this.rect, this.colors[0], this.colors[1], this.gradientMode);
    }

    /// <exclude />
    public override Brush GetBrush(Rectangle rect)
    {
      this.rect = rect;
      if (this.brush != null)
        this.brush.Dispose();
      this.CreateBrush();
      return (Brush) this.brush;
    }

    /// <summary>Creates a copy of the style</summary>
    public override FillStyle Clone()
    {
      return (FillStyle) new FillStyleGradient(this.colors[0], this.colors[1], this.gradientMode);
    }
  }
}
