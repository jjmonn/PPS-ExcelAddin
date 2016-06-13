// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.BindingProgressEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a data binding progress event arguments</summary>
  public class BindingProgressEventArgs
  {
    public int RecordsProcessed { get; set; }

    public int RecordsCount { get; set; }

    public double ProgressPercentage
    {
      get
      {
        if (this.RecordsCount == 0)
          return 100.0;
        return (double) this.RecordsProcessed / (double) this.RecordsCount * 100.0;
      }
    }

    /// <summary>BindingProgressEventArgs constructor</summary>
    public BindingProgressEventArgs(int recordsProcessed, int recordsCount)
    {
      this.RecordsProcessed = recordsProcessed;
      this.RecordsCount = recordsCount;
    }
  }
}
