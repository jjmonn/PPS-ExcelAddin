// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.GridTheme
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace VIBlend.Utilities
{
  /// <summary>Represents a GridView Theme</summary>
  [Serializable]
  public class GridTheme
  {
    private HierarchyItemStyle hierarchyItemStyleNormal;
    private HierarchyItemStyle hierarchyItemStyleSelected;
    private HierarchyItemStyle hierarchyItemStyleDisabled;
    private HierarchyItemStyle hierarchyAttributeItemStyleNormal;
    private HierarchyItemStyle hierarchyAttributeItemStyleSelected;
    private HierarchyItemStyle hierarchyAttributeItemStyleDisabled;
    private GridCellStyle gridCellStyle;

    /// <summary>The style of HierarchyItems in normal state</summary>
    public HierarchyItemStyle HierarchyItemStyleNormal
    {
      get
      {
        return this.hierarchyItemStyleNormal;
      }
      set
      {
        this.hierarchyItemStyleNormal = value;
      }
    }

    /// <summary>The style of HierarchyItems in selected state</summary>
    public HierarchyItemStyle HierarchyItemStyleSelected
    {
      get
      {
        return this.hierarchyItemStyleSelected;
      }
      set
      {
        this.hierarchyItemStyleSelected = value;
      }
    }

    /// <summary>The style of HierarchyItems in disabled state</summary>
    public HierarchyItemStyle HierarchyItemStyleDisabled
    {
      get
      {
        return this.hierarchyItemStyleDisabled;
      }
      set
      {
        this.hierarchyItemStyleDisabled = value;
      }
    }

    /// <summary>The style of summary HierarchyItems in normal state</summary>
    public HierarchyItemStyle HierarchyAttributeItemStyleNormal
    {
      get
      {
        return this.hierarchyAttributeItemStyleNormal;
      }
      set
      {
        this.hierarchyAttributeItemStyleNormal = value;
      }
    }

    /// <summary>The style of summary HierarchyItems in selected state</summary>
    public HierarchyItemStyle HierarchyAttributeItemStyleSelected
    {
      get
      {
        return this.hierarchyAttributeItemStyleSelected;
      }
      set
      {
        this.hierarchyAttributeItemStyleSelected = value;
      }
    }

    /// <summary>The style of summary HierarchyItems in disabled state</summary>
    public HierarchyItemStyle HierarchyAttributeItemStyleDisabled
    {
      get
      {
        return this.hierarchyAttributeItemStyleDisabled;
      }
      set
      {
        this.hierarchyAttributeItemStyleDisabled = value;
      }
    }

    /// <summary>The style of the data grid cells</summary>
    public GridCellStyle GridCellStyle
    {
      get
      {
        return this.gridCellStyle;
      }
      set
      {
        this.gridCellStyle = value;
      }
    }

    /// <summary>Default constructor</summary>
    public GridTheme()
    {
      this.InitDefaultTheme();
    }

    private void InitDefaultTheme()
    {
      FillStyleSolid fillStyleSolid1 = new FillStyleSolid(Color.FromArgb((int) byte.MaxValue, 239, 235, 222));
      FillStyleSolid fillStyleSolid2 = new FillStyleSolid(Color.FromArgb((int) byte.MaxValue, 239, 235, 222));
      FillStyleSolid fillStyleSolid3 = new FillStyleSolid(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192, 111));
      FillStyleSolid fillStyleSolid4 = new FillStyleSolid(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192, 111));
      FillStyleSolid fillStyleSolid5 = new FillStyleSolid(Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
      FillStyleSolid fillStyleSolid6 = new FillStyleSolid(Color.FromArgb((int) byte.MaxValue, 182, 202, 234));
      this.hierarchyItemStyleNormal = new HierarchyItemStyle((FillStyle) fillStyleSolid1, (FillStyle) fillStyleSolid2, Color.FromArgb((int) byte.MaxValue, 113, 111, 100), Color.FromArgb((int) byte.MaxValue, 0, 0, 128), Color.FromArgb((int) byte.MaxValue, 0, 0, 0), Color.FromArgb((int) byte.MaxValue, 0, 0, 0));
      this.hierarchyItemStyleDisabled = new HierarchyItemStyle((FillStyle) fillStyleSolid1, (FillStyle) fillStyleSolid2, Color.FromArgb((int) byte.MaxValue, 168, 178, 181), Color.FromArgb((int) byte.MaxValue, 203, 180, 253), Color.FromArgb((int) byte.MaxValue, 128, 84, 114), Color.FromArgb((int) byte.MaxValue, 128, 84, 114));
      this.hierarchyItemStyleSelected = new HierarchyItemStyle((FillStyle) fillStyleSolid3, (FillStyle) fillStyleSolid4, Color.FromArgb((int) byte.MaxValue, 0, 0, 128), Color.FromArgb((int) byte.MaxValue, 0, 0, 128), Color.FromArgb((int) byte.MaxValue, 0, 0, 0), Color.FromArgb((int) byte.MaxValue, 0, 0, 0));
      this.hierarchyAttributeItemStyleNormal = new HierarchyItemStyle((FillStyle) fillStyleSolid1, (FillStyle) fillStyleSolid2, Color.FromArgb((int) byte.MaxValue, 113, 111, 100), Color.FromArgb((int) byte.MaxValue, 0, 0, 128), Color.FromArgb((int) byte.MaxValue, 0, 0, 0), Color.FromArgb((int) byte.MaxValue, 0, 0, 0));
      this.hierarchyAttributeItemStyleDisabled = new HierarchyItemStyle((FillStyle) fillStyleSolid1, (FillStyle) fillStyleSolid2, Color.FromArgb((int) byte.MaxValue, 168, 178, 181), Color.FromArgb((int) byte.MaxValue, 203, 180, 253), Color.FromArgb((int) byte.MaxValue, 128, 84, 114), Color.FromArgb((int) byte.MaxValue, 128, 84, 114));
      this.hierarchyAttributeItemStyleSelected = new HierarchyItemStyle((FillStyle) fillStyleSolid3, (FillStyle) fillStyleSolid4, Color.FromArgb((int) byte.MaxValue, 0, 0, 128), Color.FromArgb((int) byte.MaxValue, 0, 0, 128), Color.FromArgb((int) byte.MaxValue, 0, 0, 0), Color.FromArgb((int) byte.MaxValue, 0, 0, 0));
      this.gridCellStyle = new GridCellStyle((FillStyle) fillStyleSolid5, (FillStyle) fillStyleSolid6, Color.FromArgb((int) byte.MaxValue, 192, 192, 192), Color.FromArgb((int) byte.MaxValue, 192, 192, 192), Color.FromArgb((int) byte.MaxValue, 0, 0, 0), Color.FromArgb((int) byte.MaxValue, 0, 0, 0));
    }

    private void InitTheme()
    {
    }

    /// <summary>Saves the theme in a XML file</summary>
    /// <param name="fileName">The output file name</param>
    /// <returns>This method returns true when the theme was successfully saved. Otherwise it returns false </returns>
    public bool SaveToXml(string fileName)
    {
      StreamWriter streamWriter = (StreamWriter) null;
      try
      {
        streamWriter = new StreamWriter(fileName);
        new XmlSerializer(typeof (GridTheme)).Serialize((TextWriter) streamWriter, (object) this);
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

    /// <summary>Loads the theme from a XML stream</summary>
    /// <param name="stream">The input XML stream which contains the theme definition</param>
    /// <returns>True if the load is successful</returns>
    public bool LoadFromXmlStream(Stream stream)
    {
      StreamReader streamReader = (StreamReader) null;
      try
      {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof (GridTheme));
        streamReader = new StreamReader(stream);
        GridTheme gridTheme = (GridTheme) xmlSerializer.Deserialize((TextReader) streamReader);
        streamReader.Close();
        streamReader = (StreamReader) null;
        this.hierarchyItemStyleNormal = gridTheme.hierarchyItemStyleNormal;
        this.hierarchyItemStyleDisabled = gridTheme.hierarchyItemStyleDisabled;
        this.hierarchyItemStyleSelected = gridTheme.hierarchyItemStyleSelected;
        this.hierarchyAttributeItemStyleNormal = gridTheme.hierarchyAttributeItemStyleNormal;
        this.hierarchyAttributeItemStyleDisabled = gridTheme.hierarchyAttributeItemStyleDisabled;
        this.hierarchyAttributeItemStyleSelected = gridTheme.hierarchyAttributeItemStyleSelected;
        this.gridCellStyle = gridTheme.gridCellStyle;
      }
      catch (Exception ex)
      {
        if (streamReader != null)
          streamReader.Close();
        return false;
      }
      return true;
    }

    /// <summary>Loads the theme from a XML file</summary>
    /// <param name="fileName">The name of the XML file which contains the theme definition</param>
    /// <returns>True if the load is successful</returns>
    public bool LoadFromXml(string fileName)
    {
      try
      {
        GridTheme gridTheme = (GridTheme) new XmlSerializer(typeof (GridTheme)).Deserialize((TextReader) new StreamReader(fileName));
        this.hierarchyItemStyleNormal = gridTheme.hierarchyItemStyleNormal;
        this.hierarchyItemStyleDisabled = gridTheme.hierarchyItemStyleDisabled;
        this.hierarchyItemStyleSelected = gridTheme.hierarchyItemStyleSelected;
        this.hierarchyAttributeItemStyleNormal = gridTheme.hierarchyAttributeItemStyleNormal;
        this.hierarchyAttributeItemStyleDisabled = gridTheme.hierarchyAttributeItemStyleDisabled;
        this.hierarchyAttributeItemStyleSelected = gridTheme.hierarchyAttributeItemStyleSelected;
        this.gridCellStyle = gridTheme.gridCellStyle;
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    /// <summary>
    /// Creates an instance of one of the default VIBlend themes
    /// </summary>
    public static GridTheme GetDefaultTheme(VIBLEND_THEME theme)
    {
      string name;
      switch (theme)
      {
        case VIBLEND_THEME.OFFICEBLACK:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2007Black.xml";
          break;
        case VIBLEND_THEME.OFFICEBLUE:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2007Blue.xml";
          break;
        case VIBLEND_THEME.OFFICESILVER:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2007Silver.xml";
          break;
        case VIBLEND_THEME.OFFICEGREEN:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2007Green.xml";
          break;
        case VIBLEND_THEME.VISTABLUE:
          name = "VIBlend.Utilities.GridThemes.GridThemeVistaBlue.xml";
          break;
        case VIBLEND_THEME.BLUEBLEND:
          name = "VIBlend.Utilities.GridThemes.GridThemeBlueBlend.xml";
          break;
        case VIBLEND_THEME.ULTRABLUE:
          name = "VIBlend.Utilities.GridThemes.GridThemeUltraBlue.xml";
          break;
        case VIBLEND_THEME.ORANGEFRESH:
          name = "VIBlend.Utilities.GridThemes.GridThemeOrangeFresh.xml";
          break;
        case VIBLEND_THEME.ECOGREEN:
          name = "VIBlend.Utilities.GridThemes.GridThemevGreen.xml";
          break;
        case VIBLEND_THEME.BLACKPEARL:
          name = "VIBlend.Utilities.GridThemes.GridThemeBlackPearl.xml";
          break;
        case VIBLEND_THEME.OFFICE2003BLUE:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2003Blue.xml";
          break;
        case VIBLEND_THEME.OFFICE2003OLIVE:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2003Olive.xml";
          break;
        case VIBLEND_THEME.OFFICE2003SILVER:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2003Silver.xml";
          break;
        case VIBLEND_THEME.RETRO:
          name = "VIBlend.Utilities.GridThemes.GridThemeRetro.xml";
          break;
        case VIBLEND_THEME.RETROBLUE:
          name = "VIBlend.Utilities.GridThemes.GridThemeRetroBlue.xml";
          break;
        case VIBLEND_THEME.OFFICE2010BLACK:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2010Black.xml";
          break;
        case VIBLEND_THEME.OFFICE2010BLUE:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2010Blue.xml";
          break;
        case VIBLEND_THEME.OFFICE2010SILVER:
          name = "VIBlend.Utilities.GridThemes.GridThemeOffice2010Silver.xml";
          break;
        case VIBLEND_THEME.NERO:
          name = "VIBlend.Utilities.GridThemes.GridThemeNero.xml";
          break;
        case VIBLEND_THEME.EXPRESSIONDARK:
          name = "VIBlend.Utilities.GridThemes.GridThemeExpressionDark.xml";
          break;
        case VIBLEND_THEME.METROBLUE:
          name = "VIBlend.Utilities.GridThemes.GridThemeMetroBlue.xml";
          break;
        case VIBLEND_THEME.METROGREEN:
          name = "VIBlend.Utilities.GridThemes.GridThemeMetroGreen.xml";
          break;
        case VIBLEND_THEME.METROORANGE:
          name = "VIBlend.Utilities.GridThemes.GridThemeMetroOrange.xml";
          break;
        case VIBLEND_THEME.STEEL:
          name = "VIBlend.Utilities.GridThemes.GridThemeSteel.xml";
          break;
        default:
          name = "VIBlend.Utilities.GridThemes.GridThemeVistaBlue.xml";
          break;
      }
      Stream manifestResourceStream = Assembly.GetAssembly(typeof (GridTheme)).GetManifestResourceStream(name);
      GridTheme gridTheme = new GridTheme();
      if (!gridTheme.LoadFromXmlStream(manifestResourceStream))
        gridTheme.InitDefaultTheme();
      return gridTheme;
    }
  }
}
