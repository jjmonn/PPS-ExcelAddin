// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.ControlTheme
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace VIBlend.Utilities
{
  /// <summary>Represents a VIBlend control theme</summary>
  [Serializable]
  public class ControlTheme
  {
    private List<NamedColor> colorSetter = new List<NamedColor>();
    private List<NamedFillStyle> fillStyleSetter = new List<NamedFillStyle>();
    private List<NamedValue> namedValueSetter = new List<NamedValue>();
    private VisualStyle styleNormal;
    private VisualStyle styleHighlight;
    private VisualStyle stylePressed;
    private VisualStyle styleDisabled;
    private VisualStyle styleDisabledPressed;
    private float radius;
    [XmlIgnore]
    private Shapes shapeType;
    private double opacity;
    private bool isAnimated;

    /// <summary>Visual style for normal state</summary>
    public VisualStyle StyleNormal
    {
      get
      {
        return this.styleNormal;
      }
      set
      {
        this.styleNormal = value;
      }
    }

    /// <summary>Visual style for highlight state</summary>
    public VisualStyle StyleHighlight
    {
      get
      {
        return this.styleHighlight;
      }
      set
      {
        this.styleHighlight = value;
      }
    }

    /// <summary>Visual style for pressed state</summary>
    public VisualStyle StylePressed
    {
      get
      {
        return this.stylePressed;
      }
      set
      {
        this.stylePressed = value;
      }
    }

    /// <summary>Visual style for disabled state</summary>
    public VisualStyle StyleDisabled
    {
      get
      {
        return this.styleDisabled;
      }
      set
      {
        this.styleDisabled = value;
      }
    }

    /// <summary>Visual style for disabled state</summary>
    public VisualStyle StyleDisabledPressed
    {
      get
      {
        return this.styleDisabledPressed;
      }
      set
      {
        this.styleDisabledPressed = value;
      }
    }

    /// <exclude />
    public float Radius
    {
      get
      {
        return this.radius;
      }
      set
      {
        this.radius = value;
      }
    }

    /// <exclude />
    [XmlIgnore]
    public Shapes ShapeType
    {
      get
      {
        return this.shapeType;
      }
      set
      {
        this.shapeType = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [XmlElement("ShapeType")]
    public string XmlShapeType
    {
      get
      {
        return ControlThemeSerializer.SerializeShapeType(this.ShapeType);
      }
      set
      {
        this.ShapeType = ControlThemeDeserializer.DeserializeShapeType(value);
      }
    }

    /// <exclude />
    public double Opacity
    {
      get
      {
        return this.opacity;
      }
      set
      {
        this.opacity = value;
      }
    }

    /// <exclude />
    public bool Animated
    {
      get
      {
        return this.isAnimated;
      }
      set
      {
        this.isAnimated = value;
      }
    }

    /// <exclude />
    [XmlElement("ColorSetter")]
    public NamedColor[] ColorSetter
    {
      get
      {
        return this.colorSetter.ToArray();
      }
      set
      {
        this.colorSetter.Clear();
        foreach (NamedColor namedColor in value)
          this.colorSetter.Add(namedColor);
      }
    }

    /// <exclude />
    [XmlElement("FillStyleSetter")]
    public NamedFillStyle[] FillStyleSetter
    {
      get
      {
        return this.fillStyleSetter.ToArray();
      }
      set
      {
        this.fillStyleSetter.Clear();
        foreach (NamedFillStyle namedFillStyle in value)
          this.fillStyleSetter.Add(namedFillStyle);
      }
    }

    /// <exclude />
    [XmlElement("NamedValueSetter")]
    public NamedValue[] NamedValueSetter
    {
      get
      {
        return this.namedValueSetter.ToArray();
      }
      set
      {
        this.namedValueSetter.Clear();
        foreach (NamedValue namedValue in value)
          this.namedValueSetter.Add(namedValue);
      }
    }

    /// <summary>Constructor</summary>
    public ControlTheme()
    {
      this.InitDefaultTheme();
    }

    /// <summary>
    /// Creates and returns an instance of one of the default VIBlend themes
    /// </summary>
    public static ControlTheme GetDefaultTheme(VIBLEND_THEME theme)
    {
      string name;
      switch (theme)
      {
        case VIBLEND_THEME.OFFICEBLACK:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2007Black.xml";
          break;
        case VIBLEND_THEME.OFFICEBLUE:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2007Blue.xml";
          break;
        case VIBLEND_THEME.OFFICESILVER:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2007Silver.xml";
          break;
        case VIBLEND_THEME.OFFICEGREEN:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2007Green.xml";
          break;
        case VIBLEND_THEME.VISTABLUE:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeVistaBlue.xml";
          break;
        case VIBLEND_THEME.BLUEBLEND:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeBlueBlend.xml";
          break;
        case VIBLEND_THEME.ULTRABLUE:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeUltraBlue.xml";
          break;
        case VIBLEND_THEME.ORANGEFRESH:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOrange.xml";
          break;
        case VIBLEND_THEME.ECOGREEN:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeEcoGreen.xml";
          break;
        case VIBLEND_THEME.BLACKPEARL:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeBlack.xml";
          break;
        case VIBLEND_THEME.OFFICE2003BLUE:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2003Blue.xml";
          break;
        case VIBLEND_THEME.OFFICE2003OLIVE:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2003Olive.xml";
          break;
        case VIBLEND_THEME.OFFICE2003SILVER:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2003Silver.xml";
          break;
        case VIBLEND_THEME.RETRO:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeRetro.xml";
          break;
        case VIBLEND_THEME.RETROBLUE:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeRetroBlue.xml";
          break;
        case VIBLEND_THEME.OFFICE2010BLACK:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2010Black.xml";
          break;
        case VIBLEND_THEME.OFFICE2010BLUE:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2010Blue.xml";
          break;
        case VIBLEND_THEME.OFFICE2010SILVER:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeOffice2010Silver.xml";
          break;
        case VIBLEND_THEME.NERO:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeNero.xml";
          break;
        case VIBLEND_THEME.EXPRESSIONDARK:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeExpressionDark.xml";
          break;
        case VIBLEND_THEME.METROBLUE:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeMetroBlue.xml";
          break;
        case VIBLEND_THEME.METROGREEN:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeMetroGreen.xml";
          break;
        case VIBLEND_THEME.METROORANGE:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeMetroOrange.xml";
          break;
        case VIBLEND_THEME.STEEL:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeSteel.xml";
          break;
        default:
          name = "VIBlend.Utilities.ControlThemes.ControlsThemeVistaBlue.xml";
          break;
      }
      Stream manifestResourceStream = Assembly.GetAssembly(typeof (ControlTheme)).GetManifestResourceStream(name);
      ControlTheme controlTheme = new ControlTheme();
      if (!controlTheme.LoadFromXmlStream(manifestResourceStream) && !controlTheme.InitDefaultTheme())
        throw new Exception("Cannot Initialize Controls Default Theme");
      return controlTheme;
    }

    /// <summary>Saves the theme to a XML file</summary>
    /// <param name="fileName">Name of the XML file</param>
    public bool SaveToXml(string fileName)
    {
      StreamWriter streamWriter = (StreamWriter) null;
      try
      {
        streamWriter = new StreamWriter(fileName);
        new XmlSerializer(typeof (ControlTheme)).Serialize((TextWriter) streamWriter, (object) this);
        streamWriter.Close();
      }
      catch (Exception ex)
      {
        if (streamWriter != null)
          streamWriter.Close();
        return false;
      }
      return true;
    }

    /// <summary>
    /// Creates a new ControlTheme object which is a copy of the current one
    /// </summary>
    /// <returns></returns>
    public ControlTheme CreateCopy()
    {
      ControlTheme controlTheme = new ControlTheme();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new XmlSerializer(typeof (ControlTheme)).Serialize((Stream) memoryStream, (object) this);
        memoryStream.Position = 0L;
        controlTheme.LoadFromXmlStream((Stream) memoryStream);
      }
      return controlTheme;
    }

    /// <summary>Loads the theme from a XML stream</summary>
    public bool LoadFromXmlStream(Stream stream)
    {
      StreamReader streamReader = (StreamReader) null;
      try
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (ControlTheme));
        streamReader = new StreamReader(stream);
        ControlTheme controlTheme = (ControlTheme) null;
        try
        {
          controlTheme = (ControlTheme) xmlSerializer.Deserialize((TextReader) streamReader);
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.StackTrace);
        }
        streamReader.Close();
        streamReader = (StreamReader) null;
        this.styleNormal = controlTheme.StyleNormal;
        this.StylePressed = controlTheme.StylePressed;
        this.styleHighlight = controlTheme.StyleHighlight;
        this.styleDisabled = controlTheme.styleDisabled;
        this.styleDisabledPressed = controlTheme.styleDisabledPressed;
        this.shapeType = controlTheme.ShapeType;
        this.isAnimated = controlTheme.Animated;
        this.radius = controlTheme.Radius;
        this.opacity = controlTheme.Opacity;
        this.fillStyleSetter = controlTheme.fillStyleSetter;
        this.colorSetter = controlTheme.colorSetter;
        this.namedValueSetter = controlTheme.namedValueSetter;
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

    private bool InitDefaultTheme()
    {
      try
      {
        this.styleNormal = new VisualStyle();
        this.styleNormal.FillStyle = (FillStyle) new FillStyleGradient(Color.FromArgb((int) byte.MaxValue, 242, 191), Color.FromArgb((int) byte.MaxValue, 181, 130), LinearGradientMode.Vertical);
        this.StyleNormal.BorderColor = Color.FromArgb(61, 61, 9);
        this.styleNormal.TextAlignment = ContentAlignment.MiddleCenter;
        this.styleNormal.Font = new Font("Microsoft Sans Serif", 10f);
        this.styleNormal.TextColor = Color.FromArgb(0, 0, 0);
        this.styleNormal.TextWrap = true;
        this.styleHighlight = new VisualStyle();
        this.styleHighlight.FillStyle = (FillStyle) new FillStyleGradient(Color.FromArgb(250, 223, 137), Color.FromArgb(239, 156, 32), LinearGradientMode.Vertical);
        this.styleHighlight.BorderColor = Color.FromArgb(61, 61, 92);
        this.styleHighlight.TextAlignment = ContentAlignment.MiddleCenter;
        this.styleHighlight.Font = new Font("Microsoft Sans Serif", 10f);
        this.styleHighlight.TextColor = Color.FromArgb(0, 0, 0);
        this.styleHighlight.TextWrap = true;
        this.StylePressed = new VisualStyle();
        this.StylePressed.FillStyle = (FillStyle) new FillStyleGradient(Color.FromArgb((int) byte.MaxValue, 181, 130), Color.FromArgb((int) byte.MaxValue, 118, 107), LinearGradientMode.Vertical);
        this.StylePressed.BorderColor = Color.FromArgb(61, 61, 92);
        this.StylePressed.TextAlignment = ContentAlignment.MiddleCenter;
        this.StylePressed.Font = new Font("Microsoft Sans Serif", 10f);
        this.StylePressed.TextColor = Color.FromArgb(0, 0, 0);
        this.StylePressed.TextWrap = true;
        this.styleDisabled = new VisualStyle();
        this.styleDisabled.FillStyle = (FillStyle) new FillStyleGradient(Color.FromArgb(120, 96, 88), Color.FromArgb(245, 222, 179), LinearGradientMode.Vertical);
        this.styleDisabled.BorderColor = Color.FromArgb((int) byte.MaxValue, 128, 43);
        this.styleDisabled.TextAlignment = ContentAlignment.MiddleCenter;
        this.styleDisabled.Font = new Font("Microsoft Sans Serif", 10f);
        this.styleDisabled.TextColor = Color.FromArgb(0, 0, 0);
        this.styleDisabled.TextWrap = true;
        this.shapeType = Shapes.RoundedRectangle;
        this.isAnimated = true;
        this.radius = 0.0f;
        this.opacity = 0.0;
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        return false;
      }
      return true;
    }

    /// <exclude />
    public void SetFillStyleGradientAngle(int angle)
    {
      this.SetFillStyleGradientAngle(this.styleNormal.FillStyle, angle);
      this.SetFillStyleGradientAngle(this.styleHighlight.FillStyle, angle);
      this.SetFillStyleGradientAngle(this.stylePressed.FillStyle, angle);
      this.SetFillStyleGradientAngle(this.styleDisabled.FillStyle, angle);
    }

    private void SetFillStyleGradientAngle(FillStyle style, int angle)
    {
      if (style is FillStyleGradientEx)
      {
        ((FillStyleGradientEx) style).GradientAngle = (float) angle;
      }
      else
      {
        if (!(style is FillStyleGradient))
          return;
        if (angle / 90 % 2 == 0)
          ((FillStyleGradient) style).LinearGradientMode = LinearGradientMode.Horizontal;
        else
          ((FillStyleGradient) style).LinearGradientMode = LinearGradientMode.Vertical;
      }
    }

    /// <summary>Loads the Theme from a XML file</summary>
    public bool LoadFromXml(string fileName)
    {
      StreamReader streamReader = (StreamReader) null;
      try
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (ControlTheme));
        streamReader = new StreamReader(fileName);
        ControlTheme controlTheme = (ControlTheme) xmlSerializer.Deserialize((TextReader) streamReader);
        streamReader.Close();
        streamReader = (StreamReader) null;
        this.styleNormal = controlTheme.StyleNormal;
        this.StylePressed = controlTheme.StylePressed;
        this.styleHighlight = controlTheme.StyleHighlight;
        this.styleDisabled = controlTheme.styleDisabled;
        this.shapeType = controlTheme.ShapeType;
        this.isAnimated = controlTheme.Animated;
        this.radius = controlTheme.Radius;
        this.opacity = controlTheme.Opacity;
        this.fillStyleSetter = controlTheme.fillStyleSetter;
        this.colorSetter = controlTheme.colorSetter;
        this.namedValueSetter = controlTheme.namedValueSetter;
      }
      catch (Exception ex)
      {
        if (streamReader != null)
          streamReader.Close();
        return false;
      }
      return true;
    }

    /// <exclude />
    public string QueryNamedValueSetter(string name)
    {
      name = name.ToLower();
      foreach (NamedValue namedValue in this.namedValueSetter)
      {
        if (namedValue.Name.ToLower() == name)
          return namedValue.Value;
      }
      return string.Empty;
    }

    /// <exclude />
    public FillStyle QueryFillStyleSetter(string name)
    {
      name = name.ToLower();
      foreach (NamedFillStyle namedFillStyle in this.fillStyleSetter)
      {
        if (namedFillStyle.Name.ToLower() == name)
          return namedFillStyle.FillStyle;
      }
      return (FillStyle) null;
    }

    /// <exclude />
    public Color QueryColorSetter(string name)
    {
      name = name.ToLower();
      foreach (NamedColor namedColor in this.colorSetter)
      {
        if (namedColor.Name.ToLower() == name)
          return namedColor.Color;
      }
      return Color.Empty;
    }
  }
}
