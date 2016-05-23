// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.IDraw
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=

// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

namespace VIBlend.Utilities
{
  /// <exclude />
  public interface IDraw : IFill, IBorder, IShape, IDrawText
  {
    string Owner { get; set; }

    double Opacity { get; set; }

    bool IsAnimated { get; set; }
  }
}
