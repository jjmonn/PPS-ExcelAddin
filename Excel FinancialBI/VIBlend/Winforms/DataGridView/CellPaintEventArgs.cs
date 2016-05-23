// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CellPaintEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Drawing;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a GridView grid cell paint event argument</summary>
  public class CellPaintEventArgs : EventArgs, ICell
  {
    private GridCell cell;
    private bool handled;
    private Rectangle bounds;
    private Graphics graphics;

    /// <summary>GridCell associated with the event</summary>
    public GridCell Cell
    {
      get
      {
        return this.cell;
      }
    }

    /// <summary>
    /// Flag indicating whether the painting was performed and handled by the even handler
    /// </summary>
    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    /// <summary>Gets the bounds of the grid cell paint area</summary>
    public Rectangle Bounds
    {
      get
      {
        return this.bounds;
      }
    }

    /// <summary>Gets a refrence to the GDI+ drawing surface</summary>
    public Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
    }

    public CellPaintEventArgs(GridCell cell, Rectangle bounds, Graphics g)
    {
      this.cell = cell;
      this.bounds = bounds;
      this.graphics = g;
    }
  }
}
