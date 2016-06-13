// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.BoundField
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a data binding relationship between a column in the data source, and a HierarchyItem
  /// </summary>
  public class BoundField
  {
    /// <summary>The text (caption) of the HierarchyItem</summary>
    public string Text { get; set; }

    /// <summary>The name of the column from the data source</summary>
    /// <remarks>
    /// When you bind to a database table, this property should be set to the name of the column that you want to bind.
    /// If you want to bind to a list of objects, the value of this property should match the name of an object's public property which you want to bind.
    /// Note that the value of this property is case sensitive
    /// </remarks>
    public string DataField { get; set; }

    /// <summary>
    /// Gets or sets the grid cells in-place editor of the HierarchyItems bound to this field
    /// </summary>
    public IEditor CellsEditor { get; set; }

    /// <summary>
    /// Gets or sets the grid cells style of the HierarchyItems bound to this field
    /// </summary>
    public GridCellStyle CellsStyle { get; set; }

    /// <summary>
    /// Gets or sets the grid cells display settings of the HierarchyItems bound to this field.
    /// </summary>
    public DisplaySettings CellsDisplaySettings { get; set; }

    /// <summary>
    /// Gets or sets the grid cells FormatProvider of the HierarchyItems bound to this field.
    /// </summary>
    public IFormatProvider CellsFormatProvider { get; set; }

    /// <summary>
    /// Gets or sets the grid cells Format string of the HierarchyItems bound to this field.
    /// </summary>
    public string CellsFormatString { get; set; }

    /// <summary>
    /// Gets or sets the grid cells image alignment of the HierarchyItems bound to this field.
    /// </summary>
    public ContentAlignment CellsImageAlignment { get; set; }

    /// <summary>
    /// Gets or sets the grid cells text alignment property of the HierarchyItems bound to this field.
    /// </summary>
    public ContentAlignment CellsTextAlignment { get; set; }

    /// <summary>
    /// Gets or sets the grid cells text and image relation property of the HierarchyItems bound to this field.
    /// </summary>
    public TextImageRelation CellsTextImageRelation { get; set; }

    /// <summary>
    /// Gets or sets the grid cells text wrap property of the HierarchyItems bound to this field.
    /// </summary>
    public bool CellsTextWrap { get; set; }

    /// <summary>
    /// Gets or sets if the HierarchyItems bound to this field are filterable.
    /// </summary>
    public bool AllowFiltering { get; set; }

    /// <summary>
    /// Gets or sets if the HierarchyItems bound to this field are sortable.
    /// </summary>
    public GridItemSortMode SortMode { get; set; }

    /// <summary>
    /// Gets or sets whether HierarchyItems bound to this field are resizable.
    /// </summary>
    public bool Resizable { get; set; }

    /// <summary>
    /// Gets or sets the TextWrap property of HierarchyItems bound to this field.
    /// </summary>
    public bool TextWrap { get; set; }

    /// <summary>
    /// Gets or sets the Width of the HierarchyItems bound to this field
    /// </summary>
    public int Width { get; set; }

    /// <summary>BoundField constructor.</summary>
    public BoundField()
    {
      this.Resizable = true;
    }

    /// <summary>BoundField constructor.</summary>
    /// <param name="text">The test to display in the HierarchyItem.</param>
    /// <param name="dataField">DataField to bind to.</param>
    public BoundField(string text, string dataField)
    {
      this.Text = text;
      this.DataField = dataField;
      this.Resizable = true;
      this.CellsTextAlignment = ContentAlignment.MiddleLeft;
      this.CellsImageAlignment = ContentAlignment.MiddleLeft;
    }
  }
}
