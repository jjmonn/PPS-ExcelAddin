// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CollectionChangedEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;

namespace VIBlend.WinForms.DataGridView
{
  public class CollectionChangedEventArgs : EventArgs
  {
    private CollectionChangedReason reason;

    /// <summary>Gets the reason.</summary>
    /// <value>The reason.</value>
    public CollectionChangedReason Reason
    {
      get
      {
        return this.reason;
      }
    }

    public CollectionChangedEventArgs(CollectionChangedReason reason)
    {
      this.reason = reason;
    }
  }
}
