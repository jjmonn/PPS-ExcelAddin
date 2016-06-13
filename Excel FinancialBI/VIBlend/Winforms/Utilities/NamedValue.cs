// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.NamedValue
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=

// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;

namespace VIBlend.Utilities
{
  /// <summary>
  /// Represents a Name and Value pair. This class is used to define setter properties in VIBlend themes
  /// </summary>
  [Serializable]
  public class NamedValue
  {
    private string name;
    private string value;

    /// <summary>String represeting the Name of the NameValue pair</summary>
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

    /// <summary>String represeting the Value of the NameValue pair</summary>
    public string Value
    {
      get
      {
        return this.value;
      }
      set
      {
        this.value = value;
      }
    }
  }
}
