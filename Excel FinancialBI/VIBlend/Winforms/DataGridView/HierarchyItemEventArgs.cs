// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.HierarchyItemEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a HierarchyItem event argument</summary>
  public class HierarchyItemEventArgs : EventArgs
  {
    private HierarchyItem item;

    /// <summary>The HierarchyItem associated with the event.</summary>
    public HierarchyItem HierarchyItem
    {
      get
      {
        return this.item;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="item">A HierarchyItem associated with the event.</param>
    public HierarchyItemEventArgs(HierarchyItem item)
    {
      this.item = item;
    }
  }
}
