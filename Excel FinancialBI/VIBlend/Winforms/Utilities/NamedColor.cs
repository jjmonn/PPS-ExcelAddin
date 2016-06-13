// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.NamedColor
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace VIBlend.Utilities
{
  /// <summary>
  /// Represents a Named Color.  This class is used in the VIBlend themes definition
  /// </summary>
  [Serializable]
  public class NamedColor
  {
    private string name;
    private Color color;

    /// <summary>Name</summary>
    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    /// <summary>Color</summary>
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
    [XmlElement("Color")]
    [Browsable(false)]
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
  }
}
