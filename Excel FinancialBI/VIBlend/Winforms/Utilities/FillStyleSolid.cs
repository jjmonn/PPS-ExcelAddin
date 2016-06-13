// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.FillStyleSolid
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace VIBlend.Utilities
{
  /// <summary>Represents a solid fill style</summary>
  [Serializable]
  public class FillStyleSolid : FillStyle
  {
    /// <exclude />
    [NonSerialized]
    private Brush brush;
    [XmlIgnore]
    protected Color color;

    public override int ColorsNumber
    {
      get
      {
        return 1;
      }
    }

    public override Color[] Colors
    {
      get
      {
        return new Color[1]{ this.color };
      }
    }

    [XmlIgnore]
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

    /// <exclude />
    [Browsable(false)]
    [XmlElement("Color")]
    public string XmlColor
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.Color);
      }
      set
      {
        this.Color = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <exclude />
    internal FillStyleSolid()
    {
    }

    /// <exclude />
    public FillStyleSolid(Color color)
    {
      this.color = color;
    }

    /// <exclude />
    ~FillStyleSolid()
    {
      if (this.brush == null)
        return;
      this.brush.Dispose();
      this.brush = (Brush) null;
    }

    protected override void CreateBrush()
    {
      this.brush = (Brush) new SolidBrush(this.color);
    }

    public override Brush GetBrush(Rectangle rect)
    {
      if (this.brush == null)
        this.CreateBrush();
      return this.brush;
    }

    public override FillStyle Clone()
    {
      return (FillStyle) new FillStyleSolid(this.color);
    }
  }
}
