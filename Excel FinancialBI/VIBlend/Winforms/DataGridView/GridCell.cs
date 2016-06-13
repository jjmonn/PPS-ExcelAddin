// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.GridCell
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Defines an grid cell inside the CellsArea</summary>
  public class GridCell
  {
    private object editValue;
    private CellsArea cellsArea;
    private HierarchyItem rowItem;
    private HierarchyItem colItem;

    /// <summary>
    /// Gets the uncommited value of the grid cell before the editor closes.
    /// </summary>
    public object EditValue
    {
      get
      {
        return this.editValue;
      }
      internal set
      {
        this.editValue = value;
      }
    }

    /// <summary>The CellsArea where cell is located</summary>
    [Browsable(false)]
    public CellsArea CellsArea
    {
      get
      {
        return this.cellsArea;
      }
      set
      {
        this.cellsArea = value;
      }
    }

    /// <summary>Cell's bounds</summary>
    [Browsable(false)]
    public Rectangle Bounds
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellBounds(this.rowItem, this.colItem);
      }
    }

    /// <summary>The Row HierarchyItem corresponding to the grid cell</summary>
    [Browsable(false)]
    public HierarchyItem RowItem
    {
      get
      {
        return this.rowItem;
      }
      internal set
      {
        this.rowItem = value;
      }
    }

    /// <summary>
    /// The Column HierarchyItem corresponding to the grid cell
    /// </summary>
    [Browsable(false)]
    public HierarchyItem ColumnItem
    {
      get
      {
        return this.colItem;
      }
      internal set
      {
        this.colItem = value;
      }
    }

    /// <summary>The CellSpan of the GridCell.</summary>
    /// <remarks>The CellSpan for a grid cell which does not participate in a cell merge contains exactly one row and one column</remarks>
    [Browsable(false)]
    public virtual CellSpan CellSpan
    {
      get
      {
        return this.cellsArea.GetCellSpan(this.rowItem, this.colItem);
      }
      set
      {
        if (value.RowItem != this.RowItem)
          throw new ArgumentException("This RowItem specified in the CellSpan does not match the RowItem of the GridCell");
        if (value.ColumnItem != this.ColumnItem)
          throw new ArgumentException("This ColumnItem specified in the CellSpan does not match the ColumnItem of the GridCell");
        this.cellsArea.SetCellSpan(this.rowItem, this.colItem, value.RowsCount, value.ColumnsCount);
      }
    }

    /// <summary>The editor control associated with the cell.</summary>
    [Browsable(false)]
    public virtual IEditor Editor
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellEditor(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellEditor(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>The custom draw style of the grid cell</summary>
    [Browsable(false)]
    public virtual GridCellStyle DrawStyle
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellDrawStyle(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellDrawStyle(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>The selection state of the grid cell</summary>
    [Browsable(false)]
    public virtual bool Selected
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.IsCellSelected(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        if (value)
          this.cellsArea.SelectCellInternal(this.rowItem, this.colItem);
        else
          this.cellsArea.UnSelectCell(this.rowItem, this.colItem);
      }
    }

    /// <summary>The locked state of the grid cell</summary>
    /// <remarks>
    /// Locked cells cannot be modified by the user via an editor
    /// </remarks>
    [Browsable(false)]
    [Description("Gets or sets whether the cell is locked")]
    [Category("Behavior")]
    public virtual bool Locked
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.IsLockedCell(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        if (value)
          this.cellsArea.LockCell(this.rowItem, this.colItem);
        else
          this.cellsArea.UnlockCell(this.rowItem, this.colItem);
      }
    }

    /// <summary>Gets or sets cell's image index</summary>
    [Description("Gets or sets cell's image index")]
    [Category("Appearance")]
    public virtual int ImageIndex
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellImageIndex(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellImage(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>Gets or sets the position of cell's image</summary>
    [Category("Behavior")]
    [Description("Gets or sets the position of cell's image")]
    public virtual ImagePositions ImagePosition
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellImagePosition(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellImagePosition(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>Gets or sets the alignment of cell's text</summary>
    [Description("Gets or sets the alignment of cell's text")]
    [Category("Behavior")]
    public virtual ContentAlignment TextAlignment
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellTextAlignment(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellTextAlignment(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>Gets or sets the wrap mode of cell's text</summary>
    [Description("Gets or sets the wrap mode of cell's text")]
    [Category("Appearance")]
    public virtual bool TextWrap
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellTextWrap(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellTextWrap(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>
    /// Gets or sets the relation between the text and image applied to this cell
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the relation between the text and image applied to this cell")]
    public virtual TextImageRelation TextImageRelation
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellTextImageRelation(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellTextImageRelation(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>Gets or sets cell's display settings</summary>
    /// <remarks>
    /// The DisplaySettings of a cell determine whether the the grid should render the cell's text, image or both
    /// </remarks>
    [Category("Behavior")]
    [Description("Gets or sets cell's display settings")]
    public virtual DisplaySettings DisplaySettings
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellDisplaySettings(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellDisplaySettings(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>Gets or sets cell's image alignment</summary>
    [Description("Gets or sets cell's image alignment")]
    [Category("Behavior")]
    public virtual ContentAlignment ImageAlignment
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellImageAlignment(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellImageAlignment(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>Gets or sets the value of the text</summary>
    /// <seealso cref="!:GridCellFormatting" />
    [Description("Gets or sets cell's value")]
    [Category("Appearance")]
    public virtual object Value
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellValue(this.rowItem, this.colItem);
      }
      set
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        this.cellsArea.SetCellValue(this.rowItem, this.colItem, value);
      }
    }

    /// <summary>Gets the cell's Value as a Formatted text</summary>
    /// <seealso cref="!:GridCellFormatting" />
    [Description("Gets the cell's formatted text")]
    [Category("Appearance")]
    public virtual string FormattedText
    {
      get
      {
        if (this.cellsArea == null)
          throw new Exception("CellsArea property is not set");
        return this.cellsArea.GetCellFormattedText(this);
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.DataGridView.GridCell" /> class.
    /// </summary>
    /// <param name="rowItem">The row item.</param>
    /// <param name="columnItem">The column item.</param>
    /// <param name="cellsArea">The cells area.</param>
    public GridCell(HierarchyItem rowItem, HierarchyItem columnItem, CellsArea cellsArea)
    {
      if (rowItem == null)
        throw new Exception("Invalid RowItem parameter");
      if (columnItem == null)
        throw new Exception("Invalid ColumnItem parameter");
      if (cellsArea == null)
        throw new Exception("Invalid CellsArea parameter");
      this.cellsArea = cellsArea;
      this.rowItem = rowItem;
      this.colItem = columnItem;
    }

    internal bool IsSameAs(GridCell cell)
    {
      if (cell == null || this.rowItem != cell.rowItem)
        return false;
      return this.colItem == cell.colItem;
    }
  }
}
