// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.NamedFillStyle
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;

namespace VIBlend.Utilities
{
  /// <summary>
  /// Represents a Named fill style. This class is used in the VIBlend themes definition
  /// </summary>
  [Serializable]
  public class NamedFillStyle
  {
    private string name;
    private FillStyle fillStyle;

    /// <summary>Name of the NamedFillStyle</summary>
    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    /// <summary>FillStyle of the NamedFillStyle</summary>
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
  }
}
