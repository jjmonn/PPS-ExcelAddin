// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.VisualStyle
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace VIBlend.Utilities
{
  /// <summary>Represents a visual style of a UI element</summary>
  [Serializable]
  public class VisualStyle
  {
    private Font font = new Font("MS San Serif", 10f);
    private FillStyle fillStyle;
    private Color borderColor;
    private Color textColor;
    private ContentAlignment textAlignment;
    private bool textWrap;

    /// <summary>Fill property of the visual style</summary>
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
    [Browsable(false)]
    [XmlElement("BorderColor")]
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

    /// <summary>Text alignment</summary>
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
      }
    }

    /// <summary>Text wrap mode</summary>
    public bool TextWrap
    {
      get
      {
        return this.textWrap;
      }
      set
      {
        this.textWrap = value;
      }
    }

    /// <summary>Text font</summary>
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

    /// <summary>
    /// Creates a new VisualStyle object with the exact same property values
    /// </summary>
    /// <returns></returns>
    public VisualStyle CreateCopy()
    {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof (VisualStyle));
      VisualStyle visualStyle = new VisualStyle();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        xmlSerializer.Serialize((Stream) memoryStream, (object) this);
        memoryStream.Position = 0L;
        visualStyle.LoadFromXmlStream((Stream) memoryStream);
      }
      return visualStyle;
    }

    /// <summary>Loads the VisualStyle from a XML stream</summary>
    /// <param name="stream">Input XML stream</param>
    /// <returns>The method returns true if the load is successful.</returns>
    public bool LoadFromXmlStream(Stream stream)
    {
      StreamReader streamReader = (StreamReader) null;
      try
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (VisualStyle));
        streamReader = new StreamReader(stream);
        VisualStyle visualStyle = (VisualStyle) null;
        try
        {
          visualStyle = (VisualStyle) xmlSerializer.Deserialize((TextReader) streamReader);
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.StackTrace);
        }
        streamReader.Close();
        streamReader = (StreamReader) null;
        this.borderColor = visualStyle.borderColor;
        this.fillStyle = visualStyle.fillStyle;
        this.font = visualStyle.font;
        this.textAlignment = visualStyle.textAlignment;
        this.textColor = visualStyle.textColor;
        this.textWrap = visualStyle.textWrap;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        if (streamReader != null)
          streamReader.Close();
        return false;
      }
      return true;
    }
  }
}
