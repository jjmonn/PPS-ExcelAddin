// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CellCancelEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a GridView grid cell cancel event argument</summary>
  public class CellCancelEventArgs : CellEventArgs
  {
    private bool cancel;

    /// <summary>Determines if the event is canceled.</summary>
    public bool Cancel
    {
      get
      {
        return this.cancel;
      }
      set
      {
        this.cancel = value;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="cell">GridCell associated with the event</param>
    public CellCancelEventArgs(GridCell cell)
      : base(cell)
    {
    }
  }
}
