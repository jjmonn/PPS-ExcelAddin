// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CellMouseEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.Windows.Forms;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a GridView grid cell mouse event argument</summary>
  public class CellMouseEventArgs : MouseEventArgs, ICell
  {
    private GridCell cell;

    /// <summary>The grid cell associated with the event</summary>
    public GridCell Cell
    {
      get
      {
        return this.cell;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="cell">GridCell</param>
    /// <param name="button">MouseButons</param>
    /// <param name="clicks">MouseEventArgs Clicks</param>
    /// <param name="x">MouseEventArgs X</param>
    /// <param name="y">MouseEventArgs Y</param>
    /// <param name="delta">MouseEventArgs Delta</param>
    public CellMouseEventArgs(GridCell cell, MouseButtons button, int clicks, int x, int y, int delta)
      : base(button, clicks, x, y, delta)
    {
      this.cell = cell;
    }
  }
}
