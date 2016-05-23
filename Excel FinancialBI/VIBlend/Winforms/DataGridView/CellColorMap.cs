// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CellColorMap
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections.Generic;
using System.Drawing;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a cell color map used for conditional formatting
  /// </summary>
  internal class CellColorMap
  {
    private List<List<Color>> colorMap = new List<List<Color>>();

    /// <summary>Gets the number of color scales</summary>
    public int ColorScalesCount
    {
      get
      {
        return this.colorMap.Count;
      }
    }

    /// <summary>Loads the color map from a bitmap file</summary>
    /// <param name="fileName">Name of the bitmap file</param>
    /// <returns>This method returns true if the map was loaded</returns>
    public bool LoadFromFile(string fileName)
    {
      try
      {
        if (!this.LoadFromBitmap(new Bitmap(fileName)))
          return false;
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    /// <summary>Loads the color map from a bitmap image</summary>
    /// <param name="bitmap">Bitmap image which represents the color map</param>
    /// <returns>This method returns true if the map was loaded</returns>
    public bool LoadFromBitmap(Bitmap bitmap)
    {
      try
      {
        if (bitmap.Height != 600 || bitmap.Width % 11 != 0)
          return false;
        int num = bitmap.Width / 11;
        this.colorMap.Clear();
        for (int index = 0; index < num; ++index)
        {
          List<Color> colorList = new List<Color>();
          this.colorMap.Add(colorList);
          int x = index * 11;
          int y = 0;
          while (y <= 599)
          {
            Color pixel = bitmap.GetPixel(x, y);
            colorList.Add(pixel);
            y += 6;
          }
        }
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    /// <summary>Gets the color at a specific index</summary>
    /// <param name="colorScale">The color scale to use. This parameter must be greater than 0 and less than ColorScalesCount</param>
    /// <param name="index">The index of the color. This parameter must be between 0 and 99.</param>
    /// <returns>The color at the requested index</returns>
    public Color GetColor(int colorScale, int index)
    {
      if (index < 0 || index > 99)
        throw new ArgumentOutOfRangeException("Index must be between 0 and 99");
      if (colorScale < 0 || colorScale >= this.ColorScalesCount)
        throw new ArgumentOutOfRangeException("colorScale must be greater than 0 and less than the number of Color scales");
      return this.colorMap[colorScale][index];
    }
  }
}
