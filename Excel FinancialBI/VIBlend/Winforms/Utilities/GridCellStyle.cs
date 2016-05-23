// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.GridCellStyle
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace VIBlend.Utilities
{
  /// <summary>Represents a style of a data grid cell</summary>
  [Serializable]
  public class GridCellStyle
  {
    [XmlIgnore]
    private Font font = new Font("MS San Serif", 10f);
    private FillStyle fillStyle;
    private FillStyle fillStyleSelected;
    private Color borderColor;
    private Color borderColorSelected;
    private Color textColor;
    private Color textColorSelected;

    /// <summary>FillStyle in normal state</summary>
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

    /// <summary>FillStyle in selected sate</summary>
    public FillStyle FillStyleSelected
    {
      get
      {
        return this.fillStyleSelected;
      }
      set
      {
        this.fillStyleSelected = value;
      }
    }

    /// <summary>Border color</summary>
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

    /// <summary>Border color in selected state</summary>
    [XmlIgnore]
    public Color BorderColorSelected
    {
      get
      {
        return this.borderColorSelected;
      }
      set
      {
        this.borderColorSelected = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [XmlElement("BorderColorSelected")]
    public string XmlBorderColorSelected
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.BorderColorSelected);
      }
      set
      {
        this.BorderColorSelected = ControlThemeDeserializer.DeserializeColor(value);
      }
    }

    /// <summary>Text color</summary>
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

    /// <summary>Text color in selected state</summary>
    [XmlIgnore]
    public Color TextColorSelected
    {
      get
      {
        return this.textColorSelected;
      }
      set
      {
        this.textColorSelected = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [XmlElement("TextColorSelected")]
    public string XmlTextColorSelected
    {
      get
      {
        return ControlThemeSerializer.SerializeColor(this.TextColorSelected);
      }
      set
      {
        this.TextColorSelected = ControlThemeDeserializer.DeserializeColor(value);
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
    [XmlElement("Font")]
    [Browsable(false)]
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

    /// <summary>Default constructor</summary>
    public GridCellStyle()
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="fillStyle">FillStyle in normal state</param>
    /// <param name="fillStyleSelected">FillStyle in selected state</param>
    /// <param name="borderColor">Border color in normal state</param>
    /// <param name="borderColorSelected">Border color in selected state</param>
    /// <param name="textColor">Text color in normal state</param>
    /// <param name="textColorSelected">Text color in selected state</param>
    public GridCellStyle(FillStyle fillStyle, FillStyle fillStyleSelected, Color borderColor, Color borderColorSelected, Color textColor, Color textColorSelected)
    {
      this.fillStyle = fillStyle;
      this.fillStyleSelected = fillStyleSelected;
      this.borderColor = borderColor;
      this.borderColorSelected = borderColorSelected;
      this.textColor = textColor;
      this.textColorSelected = textColorSelected;
    }

    /// <summary>Constructor</summary>
    /// <param name="fillStyle">FillStyle in normal state</param>
    /// <param name="fillStyleSelected">FillStyle in selected state</param>
    /// <param name="borderColor">Border color in normal state</param>
    /// <param name="borderColorSelected">Border color in selected state</param>
    /// <param name="textColor">Text color in normal state</param>
    /// <param name="textColorSelected">Text color in selected state</param>
    /// <param name="font">Text font</param>
    public GridCellStyle(FillStyle fillStyle, FillStyle fillStyleSelected, Color borderColor, Color borderColorSelected, Color textColor, Color textColorSelected, Font font)
    {
      this.fillStyle = fillStyle;
      this.fillStyleSelected = fillStyleSelected;
      this.borderColor = borderColor;
      this.borderColorSelected = borderColorSelected;
      this.textColor = textColor;
      this.textColorSelected = textColorSelected;
      this.font = font;
    }

    internal bool Validate()
    {
      if (this.fillStyle != null)
        return this.fillStyleSelected != null;
      return false;
    }
  }
}
