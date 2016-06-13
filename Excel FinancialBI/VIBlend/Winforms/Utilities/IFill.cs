// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.IFill
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System.Drawing;

namespace VIBlend.Utilities
{
  /// <exclude />
  public interface IFill
  {
    Color BackColor { get; set; }

    Color BackColor2 { get; set; }

    Color BackColor3 { get; set; }

    Color BackColor4 { get; set; }

    Color HighlightBackColor { get; set; }

    Color HighlightBackColor2 { get; set; }

    Color HighlightBackColor3 { get; set; }

    Color HighlightBackColor4 { get; set; }

    Color PressedBackColor { get; set; }

    Color PressedBackColor2 { get; set; }

    Color PressedBackColor3 { get; set; }

    Color PressedBackColor4 { get; set; }

    Color DisabledBackColor { get; set; }

    double GradientAngle { get; set; }

    double GradientOffset { get; set; }

    double GradientOffset2 { get; set; }

    GradientStyles GradientStyle { get; set; }

    int ColorsNumber { get; set; }

    double PressedGradientAngle { get; set; }

    double PressedGradientOffset { get; set; }

    double PressedGradientOffset2 { get; set; }

    GradientStyles PressedGradientStyle { get; set; }

    int PressedColorsNumber { get; set; }

    double HighlightGradientAngle { get; set; }

    double HighlightGradientOffset { get; set; }

    double HighlightGradientOffset2 { get; set; }

    GradientStyles HighlightGradientStyle { get; set; }

    int HighlightColorsNumber { get; set; }

    Size Size { get; set; }
  }
}
