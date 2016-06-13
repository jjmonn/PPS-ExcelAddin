// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.FillStyle
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Drawing;
using System.Xml.Serialization;

namespace VIBlend.Utilities
{
  /// <summary>Represents a fill style</summary>
  [XmlInclude(typeof (FillStyleSolid))]
  [XmlInclude(typeof (FillStyleGradient))]
  [XmlInclude(typeof (FillStyleGradientEx))]
  [Serializable]
  public abstract class FillStyle
  {
    /// <summary>Gets the number of fill colors</summary>
    public abstract int ColorsNumber { get; }

    /// <summary>Gets the fill colors</summary>
    public abstract Color[] Colors { get; }

    /// <summary>Gets a brush for painting the fill area</summary>
    /// <param name="rect"></param>
    /// <returns></returns>
    public abstract Brush GetBrush(Rectangle rect);

    /// <exclude />
    protected abstract void CreateBrush();

    /// <summary>Creates a copy of the fill style object</summary>
    public abstract FillStyle Clone();
  }
}
