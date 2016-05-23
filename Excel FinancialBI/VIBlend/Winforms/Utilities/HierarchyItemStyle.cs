// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.HierarchyItemStyle
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace VIBlend.Utilities
{
  /// <summary>Represents a style of a HierarchyItem</summary>
  [Serializable]
  public class HierarchyItemStyle
  {
    [XmlIgnore]
    private Font font = new Font("MS San Serif", 10f);
    private FillStyle fillStyle;
    private FillStyle fillStyleHighlight;
    private Color borderColor;
    private Color borderColorHighlighted;
    private Color textColor;
    private Color textColorHighlighed;

    /// <summary>Fill style when the item is in normal state</summary>
    public FillStyle FillStyle
    {
      get
      {
        return this.fillStyle;
      }
      set
      {
        this.fillStyle = value;
      }
    }

    /// <summary>Fill style when the item is highlighted</summary>
    public FillStyle FillStyleHighlight
    {
      get
      {
        return this.fillStyleHighlight;
      }
      set
      {
        this.fillStyleHighlight = value;
      }
    }

    /// <summary>Border color when the item is in normal state</summary>
    [XmlIgnore]
    public Color BorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
      }
    }

    /// <exclude />
    [XmlElement("BorderColor")]
    [Browsable(false)]
    public string XmlBorderColor
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.BorderColor);
      }
      set
      {
        this.BorderColor = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <summary>Border color when the item is highlighted</summary>
    [XmlIgnore]
    public Color BorderColorHighlighted
    {
      get
      {
        return this.borderColorHighlighted;
      }
      set
      {
        this.borderColorHighlighted = value;
      }
    }

    /// <exclude />
    [XmlElement("BorderColorHighlighted")]
    [Browsable(false)]
    public string XmlBorderColorHighlighted
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.BorderColorHighlighted);
      }
      set
      {
        this.BorderColorHighlighted = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <summary>Text color when the item is in normal state</summary>
    [XmlIgnore]
    public Color TextColor
    {
      get
      {
        return this.textColor;
      }
      set
      {
        this.textColor = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [XmlElement("TextColor")]
    public string XmlTextColor
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.TextColor);
      }
      set
      {
        this.TextColor = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <summary>Text color when the item is highlighted</summary>
    [XmlIgnore]
    public Color TextColorHighlighed
    {
      get
      {
        return this.textColorHighlighed;
      }
      set
      {
        this.textColorHighlighed = value;
      }
    }

    /// <exclude />
    [XmlElement("TextColorHighlighted")]
    [Browsable(false)]
    public string XmlTextColorHighlighted
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.TextColorHighlighed);
      }
      set
      {
        this.TextColorHighlighed = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <summary>Gets or sets the text font of grid cell</summary>
    [XmlIgnore]
    public Font Font
    {
      get
      {
        return this.font;
      }
      set
      {
        this.font = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [XmlElement("Font")]
    public XmlFont XmlFont
    {
      get
      {
        return ControlThemeSerializer.SerializeFont(this.font);
      }
      set
      {
        this.font = ControlThemeDeserializer.DeserializeFont(value);
      }
    }

    private HierarchyItemStyle()
    {
    }

    public HierarchyItemStyle(FillStyle fillStyle, FillStyle fillStyleHighlighted, Color borderColor, Color borderColorHighlighted, Color textColor, Color textColorHighlighted)
    {
      this.fillStyle = fillStyle;
      this.fillStyleHighlight = fillStyleHighlighted;
      this.borderColor = borderColor;
      this.borderColorHighlighted = borderColorHighlighted;
      this.textColor = textColor;
      this.textColorHighlighed = textColorHighlighted;
    }

    internal bool Validate()
    {
      if (this.fillStyle != null)
        return this.fillStyleHighlight != null;
      return false;
    }

    /// <summary>
    /// Creates a new HierarchyItemStyle object with is an exact copy of this HierarchyItemStyle
    /// </summary>
    public HierarchyItemStyle Clone()
    {
      return new HierarchyItemStyle(this.fillStyle, this.fillStyleHighlight, this.borderColor, this.borderColorHighlighted, this.textColor, this.textColorHighlighed);
    }
  }
}
