// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.HierarchyItemMouseEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System.Windows.Forms;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a HierarchyItem mouse event argument.</summary>
  public class HierarchyItemMouseEventArgs : MouseEventArgs
  {
    /// <summary>The HierarchyItem associated with the event.</summary>
    public HierarchyItem HierarchyItem { get; protected set; }

    /// <summary>Constructor</summary>
    /// <param name="hierarchyItem">HierarchyItem associated with the event.</param>
    /// <param name="MouseEventArgs">MouseEventArgs that contains data associated with the event.</param>
    public HierarchyItemMouseEventArgs(HierarchyItem hierarchyItem, MouseEventArgs args)
      : base(args.Button, args.Clicks, args.X, args.Y, args.Delta)
    {
      this.HierarchyItem = hierarchyItem;
    }
  }
}
