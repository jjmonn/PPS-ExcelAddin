// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CellValueChangingEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a DataGridView grid cell value change event arguments.
  /// </summary>
  public class CellValueChangingEventArgs : CellCancelEventArgs
  {
    /// <summary>The new value of the grid cell.</summary>
    public object NewValue { get; set; }

    /// <summary>Constructor</summary>
    public CellValueChangingEventArgs(GridCell cell, object newValue)
      : base(cell)
    {
      this.NewValue = newValue;
    }
  }
}
