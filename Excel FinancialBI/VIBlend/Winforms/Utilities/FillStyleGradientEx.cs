// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.FillStyleGradientEx
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
  /// <summary>Represents a four color gradient fill style</summary>
  [Serializable]
  public class FillStyleGradientEx : FillStyle
  {
    [XmlIgnore]
    private Color[] colors = new Color[4];
    internal static int brushCreateCnt;
    private Rectangle rect;
    [NonSerialized]
    private LinearGradientBrush brush;
    private float gradientAngle;
    private float gradientOffset;
    private float gradientOffset2;

    /// <summary>Gets the number of colors</summary>
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

    /// <summary>Gradient Color1</summary>
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

    /// <summary>Gradient Color2</summary>
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
    [Browsable(false)]
    [XmlElement("Color2")]
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

    /// <summary>Gradient Color3</summary>
    [XmlIgnore]
    public Color Color3
    {
      get
      {
        return this.colors[2];
      }
      set
      {
        this.colors[2] = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [XmlElement("Color3")]
    public string XmlColor3
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.Color3);
      }
      set
      {
        this.Color3 = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <summary>Gradient Color4</summary>
    [XmlIgnore]
    public Color Color4
    {
      get
      {
        return this.colors[3];
      }
      set
      {
        this.colors[3] = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [XmlElement("Color4")]
    public string XmlColor4
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.Color4);
      }
      set
      {
        this.Color4 = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <summary>Gets or sets the Gradient Angle</summary>
    public float GradientAngle
    {
      get
      {
        return this.gradientAngle;
      }
      set
      {
        this.gradientAngle = value;
      }
    }

    /// <summary>Gets or sets the Gradient Percentage 1</summary>
    public float GradientOffset
    {
      get
      {
        return this.gradientOffset;
      }
      set
      {
        this.gradientOffset = value;
      }
    }

    /// <summary>Gets or sets the Gradient Percentage 2</summary>
    public float GradientOffset2
    {
      get
      {
        return this.gradientOffset2;
      }
      set
      {
        this.gradientOffset2 = value;
      }
    }

    internal FillStyleGradientEx()
    {
      this.rect = new Rectangle(0, 0, 10, 10);
    }

    /// <summary>Constructor</summary>
    /// <param name="color1">Color1</param>
    /// <param name="color2">Color2</param>
    /// <param name="color3">Color3</param>
    /// <param name="color4">Color4</param>
    /// <param name="gradientAngle">Gradient angle</param>
    /// <param name="GradientOffset">Gradient percentage 1</param>
    /// <param name="GradientOffset2">Gradient percentage 2</param>
    public FillStyleGradientEx(Color color1, Color color2, Color color3, Color color4, float gradientAngle, float gradientOffset, float gradientOffset2)
    {
      this.colors[0] = color1;
      this.colors[1] = color2;
      this.colors[2] = color3;
      this.colors[3] = color4;
      this.gradientAngle = gradientAngle;
      this.gradientOffset = gradientOffset;
      this.gradientOffset2 = gradientOffset2;
      this.rect = new Rectangle(0, 0, 10, 10);
      this.brush = (LinearGradientBrush) null;
    }

    /// <exclude />
    ~FillStyleGradientEx()
    {
      if (this.brush == null)
        return;
      this.brush.Dispose();
    }

    /// <exclude />
    public static implicit operator FillStyleGradientEx(FillStyleGradient fillStyle)
    {
      return new FillStyleGradientEx(fillStyle.Color1, fillStyle.Color2, fillStyle.Color2, fillStyle.Color2, 90f, 0.4f, 0.5f);
    }

    /// <exclude />
    public static implicit operator FillStyleGradientEx(FillStyleSolid fillStyle)
    {
      return new FillStyleGradientEx(fillStyle.Color, fillStyle.Color, fillStyle.Color, fillStyle.Color, 90f, 0.4f, 0.5f);
    }

    /// <exclude />
    protected override void CreateBrush()
    {
      float[] numArray = new float[4]{ 0.0f, this.GradientOffset, this.GradientOffset2, 1f };
      this.brush = new LinearGradientBrush(new Rectangle(this.rect.X, this.rect.Y, this.rect.Width, this.rect.Height), this.colors[0], this.colors[1], this.gradientAngle);
      this.brush.WrapMode = WrapMode.TileFlipXY;
      this.brush.InterpolationColors = new ColorBlend()
      {
        Colors = this.colors,
        Positions = numArray
      };
      ++FillStyleGradientEx.brushCreateCnt;
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

    /// <summary>Creates a copy of the fill style</summary>
    public override FillStyle Clone()
    {
      return (FillStyle) new FillStyleGradientEx(this.colors[0], this.colors[1], this.colors[2], this.colors[2], this.gradientAngle, this.gradientOffset, this.gradientOffset2);
    }
  }
}
