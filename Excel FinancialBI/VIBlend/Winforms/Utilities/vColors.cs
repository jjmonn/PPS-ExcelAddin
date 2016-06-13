// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.vColors
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System.Collections;
using System.Drawing;

namespace VIBlend.Utilities
{
  public sealed class vColors
  {
    private static string colorName = "Color";
    private static string customStr = "Custom";
    private static string otherStr = "Other...";
    private static string systemStr = "System";
    private static string webStr = "Web";
    private static Color[] colors;
    private static Color[] customColors;
    private static Hashtable localizableColors;
    private static Hashtable localizableSystemColors;
    private static Color[] systemColors;

    /// <summary>Gets or sets the name of the color.</summary>
    /// <value>The name of the color.</value>
    public static string ColorName
    {
      get
      {
        return vColors.colorName;
      }
      set
      {
        vColors.colorName = value;
      }
    }

    /// <summary>Gets the colors.</summary>
    /// <value>The colors.</value>
    public static Color[] Colors
    {
      get
      {
        return vColors.colors;
      }
    }

    /// <summary>Gets the custom colors.</summary>
    /// <value>The custom colors.</value>
    public static Color[] CustomColors
    {
      get
      {
        return vColors.customColors;
      }
    }

    /// <summary>Gets or sets the custom string.</summary>
    /// <value>The custom string.</value>
    public static string CustomString
    {
      get
      {
        return vColors.customStr;
      }
      set
      {
        vColors.customStr = value;
      }
    }

    /// <summary>Gets the localizable colors.</summary>
    /// <value>The localizable colors.</value>
    public static Hashtable LocalizableColors
    {
      get
      {
        return vColors.localizableColors;
      }
    }

    /// <summary>Gets the localizable system colors.</summary>
    /// <value>The localizable system colors.</value>
    public static Hashtable LocalizableSystemColors
    {
      get
      {
        return vColors.localizableSystemColors;
      }
    }

    /// <summary>Gets or sets the other string.</summary>
    /// <value>The other string.</value>
    public static string OtherString
    {
      get
      {
        return vColors.otherStr;
      }
      set
      {
        vColors.otherStr = value;
      }
    }

    /// <summary>Gets the system colors.</summary>
    /// <value>The system colors.</value>
    public static Color[] SystemColors
    {
      get
      {
        return vColors.systemColors;
      }
    }

    /// <summary>Gets or sets the system string.</summary>
    /// <value>The system string.</value>
    public static string SystemString
    {
      get
      {
        return vColors.systemStr;
      }
      set
      {
        vColors.systemStr = value;
      }
    }

    /// <summary>Gets or sets the web string.</summary>
    /// <value>The web string.</value>
    public static string WebString
    {
      get
      {
        return vColors.webStr;
      }
      set
      {
        vColors.webStr = value;
      }
    }

    /// <summary>
    /// Initializes the <see cref="T:VIBlend.Utilities.vColors" /> class.
    /// </summary>
    static vColors()
    {
      vColors.InitializeDefaultColors(vColors.InitializeSystemColors(vColors.InitializeCustomColors()));
      vColors.localizableColors = new Hashtable();
      vColors.localizableSystemColors = new Hashtable();
      vColors.InitializeLocalizibleColors();
    }

    private vColors()
    {
    }

    private static void InitializeLocalizibleColors()
    {
      foreach (Color systemColor in vColors.SystemColors)
        vColors.LocalizableSystemColors.Add((object) systemColor, (object) systemColor.Name);
      foreach (Color color in vColors.Colors)
        vColors.LocalizableColors.Add((object) color, (object) color.Name);
    }

    private static Color[] InitializeDefaultColors(Color[] colorArray)
    {
      colorArray = new Color[141]
      {
        Color.Transparent,
        Color.Black,
        Color.DimGray,
        Color.Gray,
        Color.DarkGray,
        Color.Silver,
        Color.LightGray,
        Color.Gainsboro,
        Color.WhiteSmoke,
        Color.White,
        Color.RosyBrown,
        Color.IndianRed,
        Color.Brown,
        Color.Firebrick,
        Color.LightCoral,
        Color.Maroon,
        Color.DarkRed,
        Color.Red,
        Color.Snow,
        Color.MistyRose,
        Color.Salmon,
        Color.Tomato,
        Color.DarkSalmon,
        Color.Coral,
        Color.OrangeRed,
        Color.LightSalmon,
        Color.Sienna,
        Color.SeaShell,
        Color.Chocolate,
        Color.SaddleBrown,
        Color.SandyBrown,
        Color.PeachPuff,
        Color.Peru,
        Color.Linen,
        Color.Bisque,
        Color.DarkOrange,
        Color.BurlyWood,
        Color.Tan,
        Color.AntiqueWhite,
        Color.NavajoWhite,
        Color.BlanchedAlmond,
        Color.PapayaWhip,
        Color.Moccasin,
        Color.Orange,
        Color.Wheat,
        Color.OldLace,
        Color.FloralWhite,
        Color.DarkGoldenrod,
        Color.Goldenrod,
        Color.Cornsilk,
        Color.Gold,
        Color.Khaki,
        Color.LemonChiffon,
        Color.PaleGoldenrod,
        Color.DarkKhaki,
        Color.Beige,
        Color.LightGoldenrodYellow,
        Color.Olive,
        Color.Yellow,
        Color.LightYellow,
        Color.Ivory,
        Color.OliveDrab,
        Color.YellowGreen,
        Color.DarkOliveGreen,
        Color.GreenYellow,
        Color.Chartreuse,
        Color.LawnGreen,
        Color.DarkSeaGreen,
        Color.ForestGreen,
        Color.LimeGreen,
        Color.LightGreen,
        Color.PaleGreen,
        Color.DarkGreen,
        Color.Green,
        Color.Lime,
        Color.Honeydew,
        Color.SeaGreen,
        Color.MediumSeaGreen,
        Color.SpringGreen,
        Color.MintCream,
        Color.MediumSpringGreen,
        Color.MediumAquamarine,
        Color.Aquamarine,
        Color.Turquoise,
        Color.LightSeaGreen,
        Color.MediumTurquoise,
        Color.DarkSlateGray,
        Color.PaleTurquoise,
        Color.Teal,
        Color.DarkCyan,
        Color.Aqua,
        Color.Cyan,
        Color.LightCyan,
        Color.Azure,
        Color.DarkTurquoise,
        Color.CadetBlue,
        Color.PowderBlue,
        Color.LightBlue,
        Color.DeepSkyBlue,
        Color.SkyBlue,
        Color.LightSkyBlue,
        Color.SteelBlue,
        Color.AliceBlue,
        Color.DodgerBlue,
        Color.SlateGray,
        Color.LightSlateGray,
        Color.LightSteelBlue,
        Color.CornflowerBlue,
        Color.RoyalBlue,
        Color.MidnightBlue,
        Color.Lavender,
        Color.Navy,
        Color.DarkBlue,
        Color.MediumBlue,
        Color.Blue,
        Color.GhostWhite,
        Color.SlateBlue,
        Color.DarkSlateBlue,
        Color.MediumSlateBlue,
        Color.MediumPurple,
        Color.BlueViolet,
        Color.Indigo,
        Color.DarkOrchid,
        Color.DarkViolet,
        Color.MediumOrchid,
        Color.Thistle,
        Color.Plum,
        Color.Violet,
        Color.Purple,
        Color.DarkMagenta,
        Color.Magenta,
        Color.Fuchsia,
        Color.Orchid,
        Color.MediumVioletRed,
        Color.DeepPink,
        Color.HotPink,
        Color.LavenderBlush,
        Color.PaleVioletRed,
        Color.Crimson,
        Color.Pink,
        Color.LightPink
      };
      vColors.colors = colorArray;
      return colorArray;
    }

    private static Color[] InitializeSystemColors(Color[] colorArray)
    {
      colorArray = new Color[26]
      {
        System.Drawing.SystemColors.ActiveBorder,
        System.Drawing.SystemColors.ActiveCaption,
        System.Drawing.SystemColors.ActiveCaptionText,
        System.Drawing.SystemColors.AppWorkspace,
        System.Drawing.SystemColors.Control,
        System.Drawing.SystemColors.ControlDark,
        System.Drawing.SystemColors.ControlDarkDark,
        System.Drawing.SystemColors.ControlLight,
        System.Drawing.SystemColors.ControlLightLight,
        System.Drawing.SystemColors.ControlText,
        System.Drawing.SystemColors.Desktop,
        System.Drawing.SystemColors.GrayText,
        System.Drawing.SystemColors.Highlight,
        System.Drawing.SystemColors.HighlightText,
        System.Drawing.SystemColors.HotTrack,
        System.Drawing.SystemColors.InactiveBorder,
        System.Drawing.SystemColors.InactiveCaption,
        System.Drawing.SystemColors.InactiveCaptionText,
        System.Drawing.SystemColors.Info,
        System.Drawing.SystemColors.InfoText,
        System.Drawing.SystemColors.Menu,
        System.Drawing.SystemColors.MenuText,
        System.Drawing.SystemColors.ScrollBar,
        System.Drawing.SystemColors.Window,
        System.Drawing.SystemColors.WindowFrame,
        System.Drawing.SystemColors.WindowText
      };
      vColors.systemColors = colorArray;
      return colorArray;
    }

    private static Color[] InitializeCustomColors()
    {
      Color[] colorArray = new Color[64]{ Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, 192, 192), Color.FromArgb((int) byte.MaxValue, 224, 192), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 192), Color.FromArgb(192, (int) byte.MaxValue, 192), Color.FromArgb(192, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb(192, 192, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, 192, (int) byte.MaxValue), Color.FromArgb(224, 224, 224), Color.FromArgb((int) byte.MaxValue, 128, 128), Color.FromArgb((int) byte.MaxValue, 192, 128), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 128), Color.FromArgb(128, (int) byte.MaxValue, 128), Color.FromArgb(128, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb(128, 128, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, 128, (int) byte.MaxValue), Color.FromArgb(192, 192, 192), Color.FromArgb((int) byte.MaxValue, 0, 0), Color.FromArgb((int) byte.MaxValue, 128, 0), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, 0), Color.FromArgb(0, (int) byte.MaxValue, 0), Color.FromArgb(0, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb(0, 0, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, 0, (int) byte.MaxValue), Color.FromArgb(128, 128, 128), Color.FromArgb(192, 0, 0), Color.FromArgb(192, 64, 0), Color.FromArgb(192, 192, 0), Color.FromArgb(0, 192, 0), Color.FromArgb(0, 192, 192), Color.FromArgb(0, 0, 192), Color.FromArgb(192, 0, 192), Color.FromArgb(64, 64, 64), Color.FromArgb(128, 0, 0), Color.FromArgb(128, 64, 0), Color.FromArgb(128, 128, 0), Color.FromArgb(0, 128, 0), Color.FromArgb(0, 128, 128), Color.FromArgb(0, 0, 128), Color.FromArgb(128, 0, 128), Color.FromArgb(0, 0, 0), Color.FromArgb(64, 0, 0), Color.FromArgb(128, 64, 64), Color.FromArgb(64, 64, 0), Color.FromArgb(0, 64, 0), Color.FromArgb(0, 64, 64), Color.FromArgb(0, 0, 64), Color.FromArgb(64, 0, 64), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue) };
      vColors.customColors = colorArray;
      return colorArray;
    }
  }
}
