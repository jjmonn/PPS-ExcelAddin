// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.ControlThemeDeserializer
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Drawing;

namespace VIBlend.Utilities
{
  /// <exclude />
  public class ControlThemeDeserializer
  {
    public static Color DeserializeColor(string color)
    {
      string[] strArray = color.Split(';');
      switch ((ColorFormat) Enum.Parse(typeof (ColorFormat), strArray[0], true))
      {
        case ColorFormat.NamedColor:
          return Color.FromName(strArray[1]);
        case ColorFormat.ARGBColor:
          return Color.FromArgb((int) byte.Parse(strArray[1]), (int) byte.Parse(strArray[2]), (int) byte.Parse(strArray[3]), (int) byte.Parse(strArray[4]));
        default:
          return Color.Empty;
      }
    }

    public static Font DeserializeFont(XmlFont font)
    {
      return font.CreateFontObject();
    }

    public static Shapes DeserializeShapeType(string value)
    {
      value = value.ToLower();
      Shapes shapes = Shapes.Default;
      if (value == "default")
        shapes = Shapes.Default;
      else if (value == "roundedrectangle")
        shapes = Shapes.RoundedRectangle;
      return shapes;
    }
  }
}
