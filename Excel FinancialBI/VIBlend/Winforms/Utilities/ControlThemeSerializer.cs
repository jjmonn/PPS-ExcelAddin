// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.ControlThemeSerializer
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Drawing;

namespace VIBlend.Utilities
{
  /// <exclude />
  [Serializable]
  internal class ControlThemeSerializer
  {
    public static string SerializeColor(Color color)
    {
      return string.Format("{0};{1};{2};{3};{4}", (object) ColorFormat.ARGBColor, (object) color.A, (object) color.R, (object) color.G, (object) color.B);
    }

    public static XmlFont SerializeFont(Font font)
    {
      return new XmlFont(font);
    }

    public static string SerializeShapeType(Shapes shape)
    {
      return shape != Shapes.RoundedRectangle ? "default" : "RoundedRectangle";
    }
  }
}
