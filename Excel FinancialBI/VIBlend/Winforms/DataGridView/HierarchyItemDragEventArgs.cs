// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.HierarchyItemDragEventArgs
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a grid HierarchyItem drag event argument</summary>
  public class HierarchyItemDragEventArgs
  {
    private HierarchyItem sourceItem;
    private HierarchyItem targetItem;

    /// <summary>Gets the DragSource HierarchyItem</summary>
    public HierarchyItem DragSourceHierarchyItem
    {
      get
      {
        return this.sourceItem;
      }
    }

    /// <summary>Gets the DropTarget HierarchyItem</summary>
    public HierarchyItem DropTargetHierarchyItem
    {
      get
      {
        return this.targetItem;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="sourceItem">Drag source HierarchyItem.</param>
    /// <param name="targetItem">Drop target HierarchyItem.</param>
    public HierarchyItemDragEventArgs(HierarchyItem sourceItem, HierarchyItem targetItem)
    {
      this.sourceItem = sourceItem;
      this.targetItem = targetItem;
    }
  }
}
