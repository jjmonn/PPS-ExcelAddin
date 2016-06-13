// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.GroupHeaderDragEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;

namespace VIBlend.WinForms.DataGridView
{
  public class GroupHeaderDragEventArgs : EventArgs
  {
    private DataGridGroupHeaderItem headerItem;

    /// <summary>Gets the header item.</summary>
    /// <value>The header item.</value>
    public DataGridGroupHeaderItem HeaderItem
    {
      get
      {
        return this.headerItem;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.DataGridView.GroupHeaderDragEventArgs" /> class.
    /// </summary>
    /// <param name="headerItem">The header item.</param>
    public GroupHeaderDragEventArgs(DataGridGroupHeaderItem headerItem)
    {
      this.headerItem = headerItem;
    }
  }
}
