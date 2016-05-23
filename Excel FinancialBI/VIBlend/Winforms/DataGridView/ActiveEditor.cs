// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.ActiveEditor
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  internal class ActiveEditor
  {
    private IEditor editor;
    private HierarchyItem columnItem;
    private HierarchyItem rowItem;
    private GridCell cell;

    public IEditor Editor
    {
      get
      {
        return this.editor;
      }
      set
      {
        this.editor = value;
      }
    }

    public GridCell Cell
    {
      get
      {
        return this.cell;
      }
    }

    public HierarchyItem ColumnItem
    {
      get
      {
        return this.columnItem;
      }
      set
      {
        this.columnItem = value;
        if (this.rowItem != null && this.columnItem != null)
          this.cell = new GridCell(this.rowItem, this.columnItem, this.rowItem.DataGridView.CellsArea);
        else
          this.cell = (GridCell) null;
      }
    }

    public HierarchyItem RowItem
    {
      get
      {
        return this.rowItem;
      }
      set
      {
        this.rowItem = value;
        if (this.rowItem != null && this.columnItem != null)
          this.cell = new GridCell(this.rowItem, this.columnItem, this.rowItem.DataGridView.CellsArea);
        else
          this.cell = (GridCell) null;
      }
    }
  }
}
