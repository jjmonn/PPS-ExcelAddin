// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.CellsArea
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents the data grid's cell area</summary>
  /// <summary>Represents the data grid's cell area</summary>
  public class CellsArea : INotifyPropertyChanged
  {
    private static CellColorMap defaultCFMap = new CellColorMap();
    internal Hashtable hashCellValues = new Hashtable();
    private Hashtable hashCellDataSource = new Hashtable();
    private Hashtable hashCellTextAlignment = new Hashtable();
    private Hashtable hashCellType = new Hashtable();
    private Hashtable hashCellImage = new Hashtable();
    private Hashtable hashCellImagePosition = new Hashtable();
    private Hashtable hashCellEditor = new Hashtable();
    private Hashtable hashCellDrawStyle = new Hashtable();
    private Hashtable hashCellTextWrap = new Hashtable();
    private Hashtable hashCellTextImageRelation = new Hashtable();
    private Hashtable hashCellImageAlignment = new Hashtable();
    private Hashtable hashCellDisplaySettings = new Hashtable();
    private Hashtable hashCellAutoSize = new Hashtable();
    private Hashtable hashCellSpan = new Hashtable();
    private Hashtable hashCellInSpan = new Hashtable();
    private Hashtable hashCellChildInSpan = new Hashtable();
    private Hashtable hashCellRowItem = new Hashtable();
    private Hashtable hashCellColumnItem = new Hashtable();
    private bool allowCellMerge = true;
    private Hashtable hashLockedCells = new Hashtable();
    private Hashtable hashCellCFGroups = new Hashtable();
    private Hashtable hashItemsCFGroups = new Hashtable();
    private Dictionary<string, CellsArea.ConditionalFormattingGroup> cfGroups = new Dictionary<string, CellsArea.ConditionalFormattingGroup>();
    private PropertyBagEx<long> cellFormatSettings = new PropertyBagEx<long>();
    private Dictionary<long, CellsArea.CellSelection> selectedCells = new Dictionary<long, CellsArea.CellSelection>();
    private List<HierarchyItem> cellsSelectionHighlightedItems = new List<HierarchyItem>();
    private bool isCellsSelectionHighlightedItemsDirty = true;
    private const int CachedCellValuesMaxCount = 4000000;
    private vDataGridView gridHost;
    private PaintHelper paintHelper;
    private Rectangle bounds;
    private bool isConditionalFormattingOn;

    internal PaintHelper PaintHelper
    {
      get
      {
        return this.paintHelper;
      }
    }

    /// <summary>
    /// Returns a reference to the Grid control hosting the CellsArea.
    /// </summary>
    public vDataGridView GridControl
    {
      get
      {
        return this.gridHost;
      }
    }

    /// <summary>
    /// Returns the number of cell span groups
    /// <remarks>
    /// Use this property to determine the number of cell merges in the data grid
    /// </remarks>
    /// </summary>
    public int CellSpanGroupsCount
    {
      get
      {
        return this.hashCellSpan.Count;
      }
    }

    /// <summary>
    /// Specifies whether cell merge operations are allowed or not
    /// </summary>
    public bool AllowCellMerge
    {
      get
      {
        return this.allowCellMerge;
      }
      set
      {
        this.allowCellMerge = value;
      }
    }

    /// <summary>
    /// Gets a reference to the grid cell that is currently being edited
    /// </summary>
    public GridCell EditCell
    {
      get
      {
        if (this.gridHost.activeEditor != null && this.gridHost.activeEditor.Editor != null)
          return this.gridHost.activeEditor.Cell;
        return (GridCell) null;
      }
    }

    /// <summary>Retrieves all non-empty grid cells</summary>
    /// <returns></returns>
    public GridCell[] NonEmptyCells
    {
      get
      {
        List<GridCell> cellsList = new List<GridCell>();
        this.GetCells(ref cellsList, true);
        return cellsList.ToArray();
      }
    }

    /// <summary>Retrieves all non-empty grid cells</summary>
    /// <returns></returns>
    public GridCell[] Cells
    {
      get
      {
        List<GridCell> cellsList = new List<GridCell>();
        this.GetCells(ref cellsList, false);
        return cellsList.ToArray();
      }
    }

    internal Rectangle Bounds
    {
      get
      {
        return this.bounds;
      }
      set
      {
        this.bounds = value;
      }
    }

    /// <summary>Conditional formatting groups collection</summary>
    public Dictionary<string, CellsArea.ConditionalFormattingGroup> ConditionalFormattingGroups
    {
      get
      {
        return this.cfGroups;
      }
    }

    /// <summary>
    /// Determines whether the conditional formatting is on or off
    /// </summary>
    public bool ConditionalFormattingEnabled
    {
      get
      {
        return this.isConditionalFormattingOn;
      }
      set
      {
        this.isConditionalFormattingOn = value;
      }
    }

    /// <summary>Returns the number of selected cells</summary>
    public int SelectedCellsCount
    {
      get
      {
        return this.selectedCells.Count;
      }
    }

    /// <summary>Returns a list of all selected cells</summary>
    public GridCell[] SelectedCells
    {
      get
      {
        GridCell[] gridCellArray = new GridCell[this.selectedCells.Count];
        Dictionary<long, CellsArea.CellSelection>.Enumerator enumerator = this.selectedCells.GetEnumerator();
        int num = 0;
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Value.IsSelected)
            gridCellArray[num++] = new GridCell(enumerator.Current.Value.RowItem, enumerator.Current.Value.ColumnItem, this);
        }
        return gridCellArray;
      }
    }

    /// <exclude />
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>Constructor</summary>
    public CellsArea(vDataGridView gridView)
    {
      this.gridHost = gridView;
      this.paintHelper = new PaintHelper();
      this.LoadDefaultCFColorScales();
      this.ClearCellFormatSettings();
    }

    /// <summary>Sets the image index of a grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="colItem">Cell's column</param>
    /// <param name="imageIndex">Image index</param>
    public void SetCellImage(HierarchyItem rowItem, HierarchyItem columnItem, int imageIndex)
    {
      if (rowItem == null || columnItem == null)
        return;
      this.hashCellImage[(object) vDataGridView.ComputeHash(columnItem, rowItem)] = (object) imageIndex;
      this.OnPropertyChanged("CellImage");
    }

    /// <summary>Gets the image index of a cell's image</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="colItem">Cell's column</param>
    /// <returns>The image index of the image associated with the grid cell. If there's no image associated with the cell the function returns -1.</returns>
    public int GetCellImageIndex(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (rowItem == null || columnItem == null)
        return -1;
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellImage.ContainsKey((object) hash))
        return (int) this.hashCellImage[(object) hash];
      return -1;
    }

    /// <summary>Clears all stored cell values and settings.</summary>
    public void Clear()
    {
      this.Reset();
    }

    internal void Reset()
    {
      this.hashCellValues.Clear();
      this.hashLockedCells.Clear();
      this.selectedCells.Clear();
      this.hashCellAutoSize.Clear();
      this.hashCellChildInSpan.Clear();
      this.hashCellColumnItem.Clear();
      this.hashCellDisplaySettings.Clear();
      this.hashCellDrawStyle.Clear();
      this.hashCellEditor.Clear();
      this.hashCellImage.Clear();
      this.hashCellImageAlignment.Clear();
      this.hashCellImagePosition.Clear();
      this.hashCellInSpan.Clear();
      this.hashCellRowItem.Clear();
      this.hashCellSpan.Clear();
      this.hashCellTextAlignment.Clear();
      this.hashCellTextImageRelation.Clear();
      this.hashCellTextWrap.Clear();
      this.hashCellType.Clear();
      this.hashCellDataSource.Clear();
      this.hashItemsCFGroups.Clear();
      this.hashCellCFGroups.Clear();
      this.ClearCellFormatSettings();
    }

    /// <summary>Sets the image positioning mode of a grid cell</summary>
    public void SetCellImagePosition(HierarchyItem rowItem, HierarchyItem colItem, ImagePositions position)
    {
      if (rowItem == null || colItem == null)
        return;
      this.hashCellImagePosition[(object) vDataGridView.ComputeHash(colItem, rowItem)] = (object) position;
      this.OnPropertyChanged("CellImagePosition");
    }

    /// <summary>Gets the image positioning mode of a grid cell</summary>
    public ImagePositions GetCellImagePosition(HierarchyItem rowItem, HierarchyItem colItem)
    {
      long hash = vDataGridView.ComputeHash(colItem, rowItem);
      if (this.hashCellImagePosition.ContainsKey((object) hash))
        return (ImagePositions) this.hashCellImagePosition[(object) hash];
      return ImagePositions.Center;
    }

    /// <summary>Sets the Value of a cell</summary>
    /// <remarks>
    /// The Value of a grid cell can be of any type. The default bahavior of the grid is to format and display the object as a String.
    /// If you want to override this behaviour, you need to specify a formatter for the cell or its corresponding row, or column.
    /// </remarks>
    public void SetCellValue(HierarchyItem rowItem, HierarchyItem columnItem, object value)
    {
      if (rowItem == null || columnItem == null)
        return;
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      GridCellDataSource cellDataSource = this.GetCellDataSource(rowItem, columnItem);
      if (cellDataSource == GridCellDataSource.DataBound)
      {
        if (this.gridHost.OnCellValueChanging(rowItem, columnItem, value))
          return;
        try
        {
          this.gridHost.SetCellValueFromDataSourceNonPivot(rowItem, columnItem, value);
        }
        catch (Exception ex)
        {
          return;
        }
        if (this.hashCellValues.ContainsKey((object) hash))
          this.hashCellValues.Remove((object) hash);
        this.GridControl.OnCellValueChanged(rowItem, columnItem);
      }
      else
      {
        if (cellDataSource == GridCellDataSource.DataBoundPivot)
          throw new Exception("The cell's value is derived from the data source and aggregated. It is not editable in this mode. Use the SetCellDataSource method to change the cell's data source first");
        if (this.gridHost.OnCellValueChanging(rowItem, columnItem, value))
          return;
        this.SetCellDataSource(rowItem, columnItem, GridCellDataSource.Static);
        this.hashCellValues[(object) hash] = value;
        this.OnPropertyChanged("CellValue");
        this.GridControl.OnCellValueChanged(rowItem, columnItem);
      }
    }

    /// <summary>Gets the Value of a grid cell</summary>
    public object GetCellValue(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      object CellValue = (object) null;
      int CellImageIndex = -1;
      if (this.gridHost.ColumnsHierarchy.IsGroupingColumn(columnItem))
        return (object) "";
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      GridCellDataSource gridCellDataSource = this.GetCellDataSource(rowItem, columnItem);
      if (rowItem.IsTotal || columnItem.IsTotal)
        gridCellDataSource = GridCellDataSource.Virtual;
      switch (gridCellDataSource)
      {
        case GridCellDataSource.Virtual:
          this.gridHost.OnCellValueNeeded(rowItem, columnItem, out CellValue, ref CellImageIndex);
          if (CellValue == null && this.gridHost.BindingState == GridBindingState.BoundPivot && (rowItem.IsTotal || columnItem.IsTotal))
          {
            CellValue = this.gridHost.GetCellValueFromDataSource(rowItem, columnItem);
            break;
          }
          break;
        case GridCellDataSource.DataBound:
          if (this.hashCellValues.ContainsKey((object) hash))
          {
            CellValue = this.hashCellValues[(object) hash];
            break;
          }
          try
          {
            if (this.GridControl.GroupingColumns.Count > 0 && this.GridControl.GroupingEnabled && rowItem.Items.Count > 0)
            {
              HierarchyItem rowItem1 = rowItem;
              do
              {
                rowItem1 = rowItem1.Items[0];
              }
              while (rowItem1.Items.Count > 0);
              CellValue = this.gridHost.GetCellValueFromDataSourceNonPivot(rowItem1, columnItem);
            }
            else
              CellValue = this.gridHost.GetCellValueFromDataSourceNonPivot(rowItem, columnItem);
          }
          catch (Exception ex)
          {
            CellValue = (object) "";
          }
          if (this.hashCellValues.Count < 4000000)
          {
            this.hashCellValues[(object) hash] = CellValue;
            break;
          }
          break;
        case GridCellDataSource.DataBoundPivot:
          if (this.hashCellValues.ContainsKey((object) hash))
          {
            CellValue = this.hashCellValues[(object) hash];
            break;
          }
          CellValue = this.gridHost.GetCellValueFromDataSource(rowItem, columnItem);
          if (this.hashCellValues.Count < 4000000)
          {
            this.hashCellValues[(object) hash] = CellValue;
            break;
          }
          break;
        default:
          if (this.hashCellValues.ContainsKey((object) hash))
          {
            CellValue = this.hashCellValues[(object) hash];
            break;
          }
          break;
      }
      return CellValue;
    }

    private bool IsSpanValid(HierarchyItem item, int span)
    {
      int itemIndex = item.ItemIndex;
      HierarchyItemsCollection hierarchyItemsCollection = item.ParentItem == null ? item.Hierarchy.Items : item.parentItem.Items;
      int num = 0;
      for (int index = itemIndex; index < hierarchyItemsCollection.ItemsRendered.Count && !hierarchyItemsCollection.ItemsRendered[index].Hidden; ++index)
      {
        if (++num == span)
          return true;
      }
      return false;
    }

    private bool IsSpanValidWithFixedItems(HierarchyItem item, int span)
    {
      int itemIndex = item.ItemIndex;
      HierarchyItemsCollection hierarchyItemsCollection = item.ParentItem == null ? item.Hierarchy.Items : item.parentItem.Items;
      int num1 = 0;
      int num2 = 0;
      for (int index = itemIndex; index < hierarchyItemsCollection.ItemsRendered.Count; ++index)
      {
        if (hierarchyItemsCollection.ItemsRendered[index].Fixed)
          ++num2;
        if (++num1 == span)
        {
          if (num2 > 0)
            return num1 == num2;
          return true;
        }
      }
      return false;
    }

    /// <summary>Sets the span of a data grid cell</summary>
    /// <param name="rowItem">The row of the grid cell.</param>
    /// <param name="columnItem">The column of the grid cell.</param>
    /// <param name="rowsCount">Number of rows to merge/span</param>
    /// <param name="columnsCount">Number of columns to merge/span</param>
    public void SetCellSpan(HierarchyItem rowItem, HierarchyItem columnItem, int rowsCount, int columnsCount)
    {
      if (rowItem == null || columnItem == null || !this.gridHost.AllowCellMerge)
        return;
      if (!this.IsSpanValid(rowItem, rowsCount))
        throw new Exception("The RowItem which you specified does not have enough syblings to apply the request vertical span");
      if (!this.IsSpanValidWithFixedItems(rowItem, rowsCount))
        throw new Exception("Invalid operation. The span cannot covers both Fixed and Non-Fixed Row items");
      if (!this.IsSpanValid(columnItem, columnsCount))
        throw new Exception("The ColumnItem which you specified does not have enough syblings to apply the request horizontal span");
      if (!this.IsSpanValidWithFixedItems(columnItem, columnsCount))
        throw new Exception("Invalid operation. The span cannot covers both Fixed and Non-Fixed Column items");
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (columnsCount <= 1 && rowsCount <= 1)
      {
        if (this.hashCellSpan.ContainsKey((object) hash))
          this.hashCellSpan.Remove((object) hash);
      }
      else
        this.hashCellSpan[(object) hash] = (object) new CellSpan(rowItem, columnItem, rowsCount, columnsCount);
      this.OnPropertyChanged("CellSpan");
    }

    /// <summary>Returns the cell span properties of a grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <returns></returns>
    public CellSpan GetCellSpan(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellSpan.ContainsKey((object) hash))
        return (CellSpan) this.hashCellSpan[(object) hash];
      CellSpan itemSpanGroup1 = this.GetItemSpanGroup(rowItem);
      CellSpan itemSpanGroup2 = this.GetItemSpanGroup(columnItem);
      if (itemSpanGroup1.ColumnItem == itemSpanGroup2.ColumnItem && itemSpanGroup1.RowItem == itemSpanGroup2.RowItem)
        return itemSpanGroup1;
      return new CellSpan(rowItem, columnItem, 1, 1);
    }

    /// <summary>Gets the List of cell span groups.</summary>
    /// <remarks>
    /// Use this method to retrieve information about the cell merges in the data grid
    /// </remarks>
    /// <returns>List of CellSpan objects containing information about cell merges</returns>
    public List<CellSpan> GetCellSpansList()
    {
      List<CellSpan> cellSpanList = new List<CellSpan>();
      IDictionaryEnumerator enumerator = this.hashCellSpan.GetEnumerator();
      while (enumerator.MoveNext())
      {
        CellSpan cellSpan = (CellSpan) enumerator.Value;
        cellSpanList.Add(cellSpan);
      }
      return cellSpanList;
    }

    internal CellSpan GetItemSpanGroup(HierarchyItem item)
    {
      IDictionaryEnumerator enumerator = this.hashCellSpan.GetEnumerator();
      while (enumerator.MoveNext())
      {
        CellSpan cellSpan = (CellSpan) enumerator.Value;
        HierarchyItem hierarchyItem = item.IsColumnsHierarchyItem ? cellSpan.ColumnItem : cellSpan.RowItem;
        int num = item.IsColumnsHierarchyItem ? cellSpan.ColumnsCount : cellSpan.RowsCount;
        if (hierarchyItem.ParentItem == item.ParentItem && item.ItemIndex >= hierarchyItem.ItemIndex && item.ItemIndex < hierarchyItem.ItemIndex + num)
          return cellSpan;
      }
      if (item.IsColumnsHierarchyItem)
        return new CellSpan((HierarchyItem) null, item, 1, 1);
      return new CellSpan(item, (HierarchyItem) null, 1, 1);
    }

    /// <summary>Determines whether the specific cell is merged.</summary>
    /// <param name="rowItem">The row item.</param>
    /// <param name="columnItem">The column item.</param>
    public bool IsCellMerged(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (!this.GridControl.AllowCellMerge)
        return false;
      return this.GetCellMergeInfo(rowItem, columnItem) != null;
    }

    private RowColItemPair GetCellMergeInfo(HierarchyItem rowItem, HierarchyItem colItem)
    {
      long hash = vDataGridView.ComputeHash(colItem, rowItem);
      if (this.hashCellInSpan.ContainsKey((object) hash))
        return (RowColItemPair) this.hashCellInSpan[(object) hash];
      return (RowColItemPair) null;
    }

    private Rectangle GetSpannedCellRect(HierarchyItem rowItem, HierarchyItem colItem)
    {
      if (!this.IsCellMerged(rowItem, colItem))
        return new Rectangle(colItem.X, rowItem.Y, colItem.Width, rowItem.Height);
      CellSpan cellSpan = this.GetCellSpan(rowItem, colItem);
      Size spannedCellSize = this.GetSpannedCellSize(cellSpan.RowItem, cellSpan.ColumnItem);
      return new Rectangle(cellSpan.ColumnItem.X, cellSpan.RowItem.Y, spannedCellSize.Width, spannedCellSize.Height);
    }

    internal Size GetSpannedCellSize(HierarchyItem rowItem, HierarchyItem colItem)
    {
      CellSpan cellSpan = this.GetCellSpan(rowItem, colItem);
      int num1 = cellSpan.ColumnsCount;
      int num2 = cellSpan.RowsCount;
      if (num1 <= 1 && num2 <= 1)
        return new Size(colItem.Width, rowItem.Height);
      List<HierarchyItem> pv1 = new List<HierarchyItem>();
      this.gridHost.RowsHierarchy.GetVisibleLeafLevelItems(ref pv1);
      List<HierarchyItem> pv2 = new List<HierarchyItem>();
      this.gridHost.ColumnsHierarchy.GetVisibleLeafLevelItems(ref pv2);
      int absoluteIndex1 = cellSpan.RowItem.GetAbsoluteIndex();
      int absoluteIndex2 = cellSpan.ColumnItem.GetAbsoluteIndex();
      int width = 0;
      int height = 0;
      for (int index = 0; index < num2; ++index)
      {
        HierarchyItem rowItem1 = pv1[index + absoluteIndex1];
        if (this.GetCellInMerge(rowItem1, colItem) != null || index == 0)
          height += rowItem1.Height;
      }
      for (int index = 0; index < num1; ++index)
      {
        HierarchyItem columnItem = pv2[index + absoluteIndex2];
        if (this.GetCellInMerge(rowItem, columnItem) != null || index == 0)
          width += columnItem.Width;
      }
      return new Size(width, height);
    }

    internal Size GetSpannedCellSize(HierarchyItem rowItem, HierarchyItem colItem, HierarchyItem offsetRowItem, HierarchyItem offsetColItem, ref Point offset)
    {
      List<HierarchyItem> pv1 = new List<HierarchyItem>();
      this.gridHost.RowsHierarchy.GetVisibleLeafLevelItems(ref pv1);
      List<HierarchyItem> pv2 = new List<HierarchyItem>();
      this.gridHost.ColumnsHierarchy.GetVisibleLeafLevelItems(ref pv2);
      int absoluteIndex1 = rowItem.GetAbsoluteIndex();
      int absoluteIndex2 = colItem.GetAbsoluteIndex();
      if (absoluteIndex1 < 0 || absoluteIndex2 < 0)
        return Size.Empty;
      int absoluteIndex3 = offsetRowItem.GetAbsoluteIndex();
      int absoluteIndex4 = offsetColItem.GetAbsoluteIndex();
      int num1 = this.GetCellSpan(rowItem, colItem).ColumnsCount;
      int num2 = this.GetCellSpan(rowItem, colItem).RowsCount;
      int width = 0;
      int height = 0;
      int num3 = Math.Abs(absoluteIndex2 - absoluteIndex4);
      int num4 = Math.Abs(absoluteIndex1 - absoluteIndex3);
      for (int index = 0; index < num2; ++index)
      {
        if (index + absoluteIndex1 < pv1.Count)
        {
          HierarchyItem rowItem1 = pv1[index + absoluteIndex1];
          if (this.GetCellInMerge(rowItem1, colItem) != null || index == 0)
          {
            height += rowItem1.Height;
            if (num4 > 0)
            {
              --num4;
              offset.Y = height;
            }
          }
        }
      }
      for (int index = 0; index < num1; ++index)
      {
        if (index + absoluteIndex2 < pv2.Count)
        {
          HierarchyItem columnItem = pv2[index + absoluteIndex2];
          if (this.GetCellInMerge(rowItem, columnItem) != null || index == 0)
          {
            width += columnItem.Width;
            if (num3 > 0)
            {
              --num3;
              offset.X = width;
            }
          }
        }
      }
      return new Size(width, height);
    }

    internal void SetCellInMerge(HierarchyItem rowItem, HierarchyItem columnItem, RowColItemPair key)
    {
      if (rowItem == null || columnItem == null)
        return;
      this.hashCellInSpan[(object) vDataGridView.ComputeHash(columnItem, rowItem)] = (object) key;
    }

    internal RowColItemPair GetCellInMerge(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (rowItem == null || columnItem == null)
        return (RowColItemPair) null;
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellInSpan.ContainsKey((object) hash))
        return (RowColItemPair) this.hashCellInSpan[(object) hash];
      return (RowColItemPair) null;
    }

    internal void SetCellMergeChildren(HierarchyItem rowItem, HierarchyItem columnItem, List<RowColItemPair> key)
    {
      if (rowItem == null || columnItem == null)
        return;
      this.hashCellChildInSpan[(object) vDataGridView.ComputeHash(columnItem, rowItem)] = (object) key;
    }

    internal List<RowColItemPair> GetCellMergeChildren(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellChildInSpan.ContainsKey((object) hash))
        return (List<RowColItemPair>) this.hashCellChildInSpan[(object) hash];
      return (List<RowColItemPair>) null;
    }

    /// <summary>Sets a custom draw style for a specific grid cell</summary>
    public void SetCellDrawStyle(HierarchyItem rowItem, HierarchyItem colItem, GridCellStyle cellStyle)
    {
      this.hashCellDrawStyle[(object) vDataGridView.ComputeHash(colItem, rowItem)] = (object) cellStyle;
      this.OnPropertyChanged("CellDrawStyle");
    }

    [Obsolete("Use HierarchyItem.CellsStyle")]
    public void SetCellDrawStyle(HierarchyItem item, GridCellStyle cellStyle)
    {
      item.CellsStyle = cellStyle;
      this.OnPropertyChanged("CellDrawStyle");
    }

    /// <summary>Gets the custom draw style of a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <returns>The custom draw style of the grid cell or null if a custom style is not set</returns>
    public GridCellStyle GetCellDrawStyle(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (rowItem == null || columnItem == null)
        return (GridCellStyle) null;
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellDrawStyle.ContainsKey((object) hash))
        return (GridCellStyle) this.hashCellDrawStyle[(object) hash];
      if (columnItem.CellsStyle != null)
        return columnItem.CellsStyle;
      if (rowItem.CellsStyle != null)
        return rowItem.CellsStyle;
      return (GridCellStyle) null;
    }

    /// <summary>Sets the display settings for a specific cell.</summary>
    /// <remarks>Use this method to specify whether you want the grid cell to be displayed as text, image or both</remarks>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <param name="displaySettings">Cell's displaySettings</param>
    public void SetCellDisplaySettings(HierarchyItem rowItem, HierarchyItem columnItem, DisplaySettings displaySettings)
    {
      this.hashCellDisplaySettings[(object) vDataGridView.ComputeHash(columnItem, rowItem)] = (object) displaySettings;
      this.OnPropertyChanged("CellDisplaySettings");
    }

    [Obsolete("Use HierarchyItem.CellsDisplaySettings")]
    public void SetCellDisplaySettings(HierarchyItem Item, DisplaySettings displaySettings)
    {
      Item.CellsDisplaySettings = displaySettings;
      this.OnPropertyChanged("CellDisplaySettings");
    }

    /// <summary>Gets the display settings for a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <returns>Cell's display settings</returns>
    public DisplaySettings GetCellDisplaySettings(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellDisplaySettings.ContainsKey((object) hash))
        return (DisplaySettings) this.hashCellDisplaySettings[(object) hash];
      if (columnItem.CellsDisplaySettings != DisplaySettings.TextOnly)
        return columnItem.CellsDisplaySettings;
      if (rowItem.CellsDisplaySettings != DisplaySettings.TextOnly)
        return rowItem.CellsDisplaySettings;
      return DisplaySettings.TextOnly;
    }

    /// <summary>
    /// Gets the text and image display relation of a grid cell
    /// </summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    public TextImageRelation GetCellTextImageRelation(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellTextImageRelation.ContainsKey((object) hash))
        return (TextImageRelation) this.hashCellTextImageRelation[(object) hash];
      if (columnItem.TextImageRelation != TextImageRelation.ImageBeforeText)
        return columnItem.TextImageRelation;
      if (rowItem.TextImageRelation != TextImageRelation.ImageBeforeText)
        return rowItem.TextImageRelation;
      return TextImageRelation.ImageBeforeText;
    }

    /// <summary>
    /// Sets the text and image display relation of a grid cell
    /// </summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <param name="relation">Cell's text and image relation</param>
    public void SetCellTextImageRelation(HierarchyItem rowItem, HierarchyItem columnItem, TextImageRelation relation)
    {
      this.hashCellTextImageRelation[(object) vDataGridView.ComputeHash(columnItem, rowItem)] = (object) relation;
      this.OnPropertyChanged("CellTextImageRelation");
    }

    /// <summary>Sets the size of the cell to auto.</summary>
    /// <param name="rowItem">The row item.</param>
    /// <param name="colItem">The col item.</param>
    /// <param name="autoSize">if set to <c>true</c> [auto size].</param>
    public void SetCellAutoSize(HierarchyItem rowItem, HierarchyItem colItem, bool autoSize)
    {
      this.hashCellAutoSize[(object) vDataGridView.ComputeHash(colItem, rowItem)] = (object) autoSize;
      if (autoSize)
      {
        Image cellImageInternal = this.GetCellImageInternal(rowItem, colItem);
        Font font = this.gridHost.Font;
        TextImageRelation textImageRelation = this.GetCellTextImageRelation(rowItem, colItem);
        object cellValue = this.GetCellValue(rowItem, colItem);
        string cellFormattedText = this.GetCellFormattedText(rowItem, colItem, cellValue);
        using (Graphics graphics = this.gridHost.CreateGraphics())
        {
          SizeF sizeF = graphics.MeasureString(cellFormattedText, font);
          if (cellImageInternal != null)
          {
            if (textImageRelation == TextImageRelation.ImageBeforeText || textImageRelation == TextImageRelation.TextBeforeImage)
            {
              sizeF.Width = sizeF.Width + (float) cellImageInternal.Width;
              sizeF.Height = Math.Max(sizeF.Height, (float) cellImageInternal.Height);
            }
            if (textImageRelation == TextImageRelation.ImageAboveText || textImageRelation == TextImageRelation.TextAboveImage)
            {
              sizeF.Height = sizeF.Height + (float) cellImageInternal.Height;
              sizeF.Width = Math.Max(sizeF.Width, (float) cellImageInternal.Width);
            }
          }
          rowItem.Width = Math.Max((int) sizeF.Height, rowItem.Width);
          colItem.Height = Math.Max((int) sizeF.Width + 25, colItem.hieararchyItemWidth);
        }
        this.gridHost.Invalidate();
      }
      this.OnPropertyChanged("CellAutoSize");
    }

    /// <exclude />
    public bool GetCellAutoSize(HierarchyItem rowItem, HierarchyItem colItem)
    {
      long hash = vDataGridView.ComputeHash(colItem, rowItem);
      if (this.hashCellAutoSize.ContainsKey((object) hash))
        return (bool) this.hashCellAutoSize[(object) hash];
      return false;
    }

    /// <summary>Sets the text alignment for a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <param name="textAlignment">Cell's new text alignment</param>
    public void SetCellTextAlignment(HierarchyItem rowItem, HierarchyItem columnItem, ContentAlignment textAlignment)
    {
      this.hashCellTextAlignment[(object) vDataGridView.ComputeHash(columnItem, rowItem)] = (object) textAlignment;
      this.OnPropertyChanged("CellTextAlignment");
    }

    [Obsolete("Use HierarchyItem.CellsTextAlignment")]
    public void SetCellTextAlignment(HierarchyItem Item, ContentAlignment textAlignment)
    {
      if (Item == null)
        return;
      Item.CellsTextAlignment = textAlignment;
      this.OnPropertyChanged("CellTextAlignment");
    }

    /// <summary>Gets the text alignment for a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <returns>Cell's text alignment. The default value is MiddleCenter</returns>
    public ContentAlignment GetCellTextAlignment(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellTextAlignment.ContainsKey((object) hash))
        return (ContentAlignment) this.hashCellTextAlignment[(object) hash];
      if (columnItem.CellsTextAlignment != ContentAlignment.MiddleLeft)
        return columnItem.CellsTextAlignment;
      if (rowItem.CellsTextAlignment != ContentAlignment.MiddleLeft)
        return rowItem.CellsTextAlignment;
      return ContentAlignment.MiddleLeft;
    }

    /// <summary>Sets the image alignment for a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <param name="imageAlignment">Cell's image alignment</param>
    public void SetCellImageAlignment(HierarchyItem rowItem, HierarchyItem columnItem, ContentAlignment imageAlignment)
    {
      this.hashCellImageAlignment[(object) vDataGridView.ComputeHash(columnItem, rowItem)] = (object) imageAlignment;
      this.OnPropertyChanged("CellImageAlignment");
    }

    [Obsolete("Use HierarchyItem.ImageAlignment")]
    public void SetCellImageAlignment(HierarchyItem Item, ContentAlignment imageAlignment)
    {
      if (Item == null)
        return;
      Item.CellsImageAlignment = imageAlignment;
      this.OnPropertyChanged("CellImageAlignment");
    }

    /// <summary>Gets the image alignment for a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <returns>The image alignment of the grid cell. The default value is MiddleCenter</returns>
    public ContentAlignment GetCellImageAlignment(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (rowItem == null || columnItem == null)
        return ContentAlignment.MiddleCenter;
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellImageAlignment.ContainsKey((object) hash))
        return (ContentAlignment) this.hashCellImageAlignment[(object) hash];
      if (columnItem.ImageAlignment != ContentAlignment.MiddleCenter)
        return columnItem.ImageAlignment;
      if (rowItem.ImageAlignment != ContentAlignment.MiddleCenter)
        return rowItem.ImageAlignment;
      return ContentAlignment.MiddleCenter;
    }

    /// <summary>Sets the text wrap mode for a specific grid cell</summary>
    public void SetCellTextWrap(HierarchyItem rowItem, HierarchyItem columnItem, bool textWrap)
    {
      this.hashCellTextWrap[(object) vDataGridView.ComputeHash(columnItem, rowItem)] = (object) textWrap;
      this.OnPropertyChanged("CellTextWrap");
    }

    [Obsolete("Use HierarchyItem.CellsTextWrap")]
    public void SetCellTextWrap(HierarchyItem item, bool textWrap)
    {
      item.CellsTextWrap = textWrap;
      this.OnPropertyChanged("CellTextWrap");
    }

    /// <summary>Gets the text wrap mode of a grid cell</summary>
    public bool GetCellTextWrap(HierarchyItem rowItem, HierarchyItem colItem)
    {
      long hash = vDataGridView.ComputeHash(colItem, rowItem);
      if (this.hashCellTextWrap.ContainsKey((object) hash))
        return (bool) this.hashCellTextWrap[(object) hash];
      return colItem.CellsTextWrap || rowItem.CellsTextWrap;
    }

    internal Rectangle GetCellBounds(HierarchyItem rowItem, HierarchyItem colItem)
    {
      if (rowItem == null || colItem == null)
        return Rectangle.Empty;
      Rectangle rectangle = new Rectangle(colItem.X, rowItem.Y, colItem.Width, rowItem.Height);
      if (this.allowCellMerge)
      {
        Size spannedCellSize = this.GetSpannedCellSize(rowItem, colItem);
        rectangle.Width = spannedCellSize.Width;
        rectangle.Height = spannedCellSize.Height;
      }
      return rectangle;
    }

    /// <summary>Sets the data source property of a grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <param name="cellDataSource">Cell's data source type</param>
    public void SetCellDataSource(HierarchyItem rowItem, HierarchyItem columnItem, GridCellDataSource cellDataSource)
    {
      this.hashCellDataSource[(object) vDataGridView.ComputeHash(columnItem, rowItem)] = (object) cellDataSource;
      this.OnPropertyChanged("CellDataSource");
    }

    [Obsolete("Use HierarchyItem.CellsDataSource property")]
    public void SetCellDataSource(HierarchyItem Item, GridCellDataSource cellDataSource)
    {
      Item.CellsDataSource = cellDataSource;
    }

    /// <summary>Gets the data source type of a grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <returns>The data source type of the grid cell. The default value is NotSet which means that the cell is unbound</returns>
    public GridCellDataSource GetCellDataSource(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellDataSource.ContainsKey((object) hash))
        return (GridCellDataSource) this.hashCellDataSource[(object) hash];
      if (columnItem.CellsDataSource != GridCellDataSource.NotSet)
        return columnItem.CellsDataSource;
      if (rowItem.CellsDataSource != GridCellDataSource.NotSet)
        return rowItem.CellsDataSource;
      if (this.gridHost.useVirtualCellsByDefault)
        return GridCellDataSource.Virtual;
      if (this.gridHost.BindingState == GridBindingState.Bound)
        return GridCellDataSource.DataBound;
      return this.gridHost.BindingState == GridBindingState.BoundPivot ? GridCellDataSource.DataBoundPivot : GridCellDataSource.NotSet;
    }

    /// <summary>Sets the data editor control for a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <param name="cellEditor">Cell's editor control</param>
    public void SetCellEditor(HierarchyItem rowItem, HierarchyItem columnItem, IEditor cellEditor)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (cellEditor == null)
      {
        this.hashCellEditor.Remove((object) hash);
      }
      else
      {
        this.hashCellEditor[(object) hash] = (object) cellEditor;
        this.OnPropertyChanged("CellEditor");
      }
    }

    /// <summary>Gets the cell editor of a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <returns>The function returns a reference to the cell's editor control or null if there is no editor assiciated with the grid cell</returns>
    public IEditor GetCellEditor(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (columnItem != null && rowItem != null)
      {
        long hash = vDataGridView.ComputeHash(columnItem, rowItem);
        if (this.hashCellEditor.ContainsKey((object) hash))
          return (IEditor) this.hashCellEditor[(object) hash];
      }
      if (columnItem != null && columnItem.CellsEditor != null)
        return columnItem.CellsEditor;
      if (rowItem != null && rowItem.CellsEditor != null)
        return rowItem.CellsEditor;
      return (IEditor) null;
    }

    /// <summary>
    /// Sets the grid cell editor for all cell in a row or column.
    /// </summary>
    /// <remarks>
    /// Use this method to specify a cell editor for all cell in row or column.
    /// </remarks>
    /// <param name="item">Row or a column item</param>
    /// <param name="editor">The editor for the entire row or column</param>
    [Obsolete("Please, use SetCellEditor")]
    public void SetItemEditor(HierarchyItem item, IEditor editor)
    {
      this.SetCellEditor(item, editor);
    }

    [Obsolete("Use HierarchyItem.CellsEditor")]
    public void SetCellEditor(HierarchyItem item, IEditor editor)
    {
      if (item == null)
        return;
      item.CellsEditor = editor;
      this.OnPropertyChanged("CellEditor");
    }

    [Obsolete("Use HierarchyItem.CellsEditor")]
    public IEditor GetCellEditor(HierarchyItem item)
    {
      if (item == null)
        return (IEditor) null;
      return item.CellsEditor;
    }

    internal void InvalidateHashes()
    {
      this.selectedCells.Clear();
    }

    internal void GetCells(ref List<GridCell> cellsList, HierarchyItem item, bool retrieveNonEmpty)
    {
      foreach (HierarchyItem hierarchyItem in item.IsColumnsHierarchyItem ? this.gridHost.RowsHierarchy.GetChildItemsRecursive() : this.gridHost.ColumnsHierarchy.GetChildItemsRecursive())
      {
        HierarchyItem rowItem;
        HierarchyItem columnItem;
        if (item.IsColumnsHierarchyItem)
        {
          rowItem = hierarchyItem;
          columnItem = item;
        }
        else
        {
          rowItem = item;
          columnItem = hierarchyItem;
        }
        if (this.GetCellValue(rowItem, columnItem) != null || retrieveNonEmpty)
          cellsList.Add(new GridCell(rowItem, columnItem, this));
      }
    }

    private void GetCells(ref List<GridCell> cellsList, bool retrieveNonEmpty)
    {
      List<HierarchyItem> childItemsRecursive = this.gridHost.RowsHierarchy.GetChildItemsRecursive();
      foreach (HierarchyItem columnItem in this.gridHost.ColumnsHierarchy.GetChildItemsRecursive())
      {
        foreach (HierarchyItem rowItem in childItemsRecursive)
        {
          if (this.GetCellValue(rowItem, columnItem) != null || retrieveNonEmpty)
            cellsList.Add(new GridCell(rowItem, columnItem, this));
        }
      }
    }

    internal long GetFirstSeletedCellHashKey()
    {
      if (this.SelectedCellsCount == 0)
        return -1;
      IDictionaryEnumerator dictionaryEnumerator = (IDictionaryEnumerator) this.selectedCells.GetEnumerator();
      dictionaryEnumerator.MoveNext();
      return (long) dictionaryEnumerator.Key;
    }

    internal Image GetCellImageInternal(HierarchyItem rowItem, HierarchyItem colItem)
    {
      if (rowItem == null || colItem == null)
        return (Image) null;
      int cellImageIndex = this.GetCellImageIndex(rowItem, colItem);
      Image image = (Image) null;
      if (cellImageIndex >= 0 && this.gridHost.ImageList != null && cellImageIndex < this.gridHost.ImageList.Images.Count)
        image = this.gridHost.ImageList.Images[cellImageIndex];
      return image;
    }

    private Color GetArrowColor(bool isHover)
    {
      if (!this.gridHost.Enabled)
        return this.gridHost.Theme.HierarchyItemStyleDisabled.BorderColor;
      Color color = Color.Empty;
      return !isHover ? this.gridHost.Theme.HierarchyItemStyleNormal.BorderColor : this.gridHost.Theme.HierarchyItemStyleNormal.BorderColorHighlighted;
    }

    private FillStyle GetArrowFillStyle(bool isHover)
    {
      if (!this.gridHost.Enabled)
        return this.gridHost.Theme.HierarchyItemStyleDisabled.FillStyle;
      FillStyle fillStyle = (!isHover ? this.gridHost.Theme.HierarchyItemStyleNormal.FillStyle : this.gridHost.Theme.HierarchyItemStyleNormal.FillStyleHighlight).Clone();
      for (int index = 0; index < fillStyle.ColorsNumber; ++index)
        fillStyle.Colors[index] = Color.FromArgb((int) byte.MaxValue, fillStyle.Colors[index]);
      return fillStyle;
    }

    private void DrawArrowIndicator(Graphics g, int x, int y, bool isExpanded, bool isHover)
    {
      GraphicsPath path = new GraphicsPath();
      int num1 = 6;
      if (isExpanded)
      {
        path.AddLine(x + num1, y + num1, x + num1, y);
        path.AddLine(x + num1, y, x, y + num1);
        path.AddLine(x, y + num1, x + num1, y + num1);
      }
      else
      {
        num1 = 8;
        int num2 = num1 / 2;
        path.AddLine(x, y, x, y + num1);
        path.AddLine(x, y + num1, x + num2, y + num2);
        path.AddLine(x + num2, y + num2, x, y);
      }
      using (Brush brush = this.GetArrowFillStyle(isHover).GetBrush(new Rectangle(x, y, num1, num1)))
        g.FillPath(brush, path);
      using (Pen pen = new Pen(this.GetArrowColor(isHover)))
        g.DrawPath(pen, path);
    }

    private void RenderRowGroupHeaders(Graphics g, HierarchyItem rowitem, string groupName, int x, int y)
    {
      if (rowitem == null && rowitem.itemLevel > this.gridHost.ColumnsHierarchy.Items.Count)
        return;
      Rectangle rect = new Rectangle(x, y, this.gridHost.ColumnsHierarchy.Width, rowitem.Height);
      int hiddenLeafItemsCount = rowitem.NonHiddenLeafItemsCount;
      GridCellStyle gridCellStyle = this.gridHost.Theme.GridCellStyle;
      Brush brush = (rowitem.Selected ? gridCellStyle.FillStyleSelected : gridCellStyle.FillStyle).GetBrush(rect);
      g.FillRectangle(brush, rect);
      using (Pen pen = new Pen(rowitem.Selected ? gridCellStyle.BorderColorSelected : gridCellStyle.BorderColor))
        g.DrawRectangle(pen, rect);
      bool isHover = false;
      if (this.gridHost.cellMouseMove != null && this.gridHost.cellMouseMove.RowItem == rowitem && (this.gridHost.ColumnsHierarchy.Items.Count > rowitem.itemLevel && this.gridHost.cellMouseMove.ColumnItem == this.gridHost.ColumnsHierarchy.Items[rowitem.itemLevel]))
        isHover = true;
      this.DrawArrowIndicator(g, x + 5 + this.gridHost.ColumnsHierarchy.Items[rowitem.itemLevel].X, y + 11, rowitem.Expanded, isHover);
      Font font = new Font(gridCellStyle.Font, FontStyle.Bold);
      Rectangle rectangle = rect;
      int num1 = 1 + this.gridHost.ColumnsHierarchy.Items[rowitem.itemLevel].X + this.gridHost.ColumnsHierarchy.Items[rowitem.itemLevel].Width;
      rectangle.X += num1;
      rectangle.Width -= num1;
      if (this.gridHost.GroupingDefaultHeaderTextVisible)
      {
        this.paintHelper.DrawText(g, rectangle, false, gridCellStyle.TextColor, groupName + " : ", font, ContentAlignment.MiddleLeft);
        int num2 = (int) g.MeasureString(groupName + " :", font, rectangle.Width).Width + 3;
        rectangle.X += num2;
        rectangle.Width -= num2;
        this.paintHelper.DrawText(g, rectangle, false, gridCellStyle.TextColor, rowitem.Caption, gridCellStyle.Font, ContentAlignment.MiddleLeft);
        int num3 = (int) g.MeasureString(rowitem.Caption, font, rectangle.Width).Width;
        rectangle.X += num3;
        rectangle.Width -= num3;
        string text = string.Format("({0})", (object) hiddenLeafItemsCount);
        this.paintHelper.DrawText(g, rectangle, false, Color.MediumBlue, text, gridCellStyle.Font, ContentAlignment.MiddleLeft);
        int num4 = (int) g.MeasureString(text, gridCellStyle.Font).Width + 4;
        rectangle.X += num4;
        rectangle.Width -= num4;
      }
      string customText = "";
      Color color = Color.Black;
      ContentAlignment alignment = ContentAlignment.MiddleRight;
      if (!this.gridHost.OnGroupHeaderCustomTextNeeded(rowitem, out customText, out font, out color, out alignment))
        return;
      Region clip = g.Clip;
      g.Clip = new Region(rectangle);
      this.paintHelper.DrawText(g, rectangle, false, color, customText, gridCellStyle.Font, alignment);
      g.Clip = clip;
    }

    internal void DrawMatrixData(Graphics g)
    {
      int x1 = this.bounds.X;
      int y1 = this.bounds.Y;
      List<HierarchyItem> pv1 = (List<HierarchyItem>) null;
      this.gridHost.ColumnsHierarchy.GetVisibleLeafLevelItems(ref pv1);
      List<HierarchyItem> pv2 = (List<HierarchyItem>) null;
      this.gridHost.RowsHierarchy.GetVisibleLeafLevelItems(ref pv2);
      if (this.isConditionalFormattingOn)
        this.UpdateCFGroupValues(pv2, pv1);
      int count = pv1.Count;
      if (pv2.Count == 0 || count == 0)
        return;
      int num1 = -1;
      int num2 = -1;
      Rectangle rectangle1 = new Rectangle(int.MaxValue, int.MaxValue, 0, 0);
      int index1 = pv2.Count - 1;
      int index2 = pv1.Count - 1;
      int index3 = 0;
      int index4 = 0;
      bool flag1 = false;
      int scrollViewStartRow = this.gridHost.RowsHierarchy.ScrollViewStartRow;
      int scrollViewEndRow = this.gridHost.RowsHierarchy.ScrollViewEndRow;
      bool flag2 = this.gridHost.GroupingEnabled && this.gridHost.GroupingColumns.Count > 0;
      int num3 = -1;
      int num4 = -1;
      Point point = new Point(0, 0);
      Rectangle drawBounds1;
      for (int index5 = 0; index5 < 3 && (num3 != -1 || num4 != -1 || index5 <= 0); ++index5)
      {
        for (int r = scrollViewStartRow; r <= scrollViewEndRow; ++r)
        {
          HierarchyItem hierarchyItem1 = pv2[r];
          if (index5 == 0 && hierarchyItem1.Fixed && r > num4)
            num4 = r;
          Rectangle drawBounds2 = hierarchyItem1.DrawBounds;
          drawBounds2.Height = hierarchyItem1.Height;
          if (drawBounds2.Bottom >= 0)
          {
            if (drawBounds2.Top <= this.gridHost.Bottom)
            {
              bool flag3 = this.gridHost.RowsHierarchy.IsItemSelected(hierarchyItem1);
              if (flag2 && hierarchyItem1.NonHiddenLeafItemsCount > 0 && hierarchyItem1.ItemsCount > 0)
              {
                if (pv1.Count != 0)
                {
                  int x2 = this.gridHost.ColumnsHierarchy.X;
                  int y2 = drawBounds2.Y;
                  string text = this.gridHost.GroupingColumns[hierarchyItem1.itemLevel].Text;
                  this.RenderRowGroupHeaders(g, hierarchyItem1, text, x2, y2);
                }
              }
              else
              {
                for (int index6 = 0; index6 < count; ++index6)
                {
                  HierarchyItem hierarchyItem2 = pv1[index6];
                  if (index5 == 0 && hierarchyItem2.Fixed && index6 > num3)
                    num3 = index6;
                  if (index5 > 0)
                  {
                    bool flag4 = hierarchyItem2.Fixed && hierarchyItem1.Fixed;
                    if (!flag4 && index5 == 1)
                      flag4 = hierarchyItem2.Fixed && r > num4 || hierarchyItem1.Fixed && index6 > num3;
                    if (hierarchyItem2.Fixed)
                    {
                      drawBounds1 = hierarchyItem2.DrawBounds;
                      if (drawBounds1.X + hierarchyItem2.Width > point.X)
                      {
                        drawBounds1 = hierarchyItem2.DrawBounds;
                        int num5 = drawBounds1.X + hierarchyItem2.Width;
                        point.X = num5;
                      }
                    }
                    if (hierarchyItem1.Fixed)
                    {
                      drawBounds1 = hierarchyItem1.DrawBounds;
                      if (drawBounds1.Y + hierarchyItem1.Height > point.Y)
                      {
                        drawBounds1 = hierarchyItem1.DrawBounds;
                        int num5 = drawBounds1.Y + hierarchyItem1.Height;
                        point.Y = num5;
                      }
                    }
                    if (!flag4)
                      break;
                  }
                  Rectangle drawBounds3 = hierarchyItem2.DrawBounds;
                  drawBounds3.Width = hierarchyItem2.Width;
                  int columnWidth = this.gridHost.ColumnsHierarchy.GetColumnWidth(index6);
                  Rectangle rectangle2 = new Rectangle(drawBounds3.X, drawBounds2.Y, drawBounds3.Width, drawBounds2.Height);
                  if (index6 == count - 1 && this.gridHost.vScroll != null && !this.gridHost.vScroll.Visible)
                  {
                    int num5 = columnWidth - 1;
                    rectangle2 = new Rectangle(drawBounds3.X, drawBounds2.Y, drawBounds3.Width - 1, drawBounds2.Height);
                  }
                  Rectangle rect1 = rectangle2;
                  if (rectangle2.Right >= 0)
                  {
                    if (rectangle2.Left <= this.gridHost.Width)
                    {
                      RowColItemPair cellInMerge = this.GetCellInMerge(hierarchyItem1, hierarchyItem2);
                      List<RowColItemPair> cellMergeChildren = this.GetCellMergeChildren(hierarchyItem1, hierarchyItem2);
                      int colSpan = this.GetCellSpan(hierarchyItem1, hierarchyItem2).ColumnsCount;
                      int rowSpan = this.GetCellSpan(hierarchyItem1, hierarchyItem2).RowsCount;
                      Rectangle spanRect = Rectangle.Empty;
                      HierarchyItem rowSpanItem = hierarchyItem1;
                      HierarchyItem colSpanItem = hierarchyItem2;
                      bool flag4 = this.gridHost.ColumnsHierarchy.IsItemSelected(hierarchyItem2);
                      if (this.allowCellMerge)
                      {
                        this.CalculateVisibleMergedCells(pv1, pv2, r, hierarchyItem1, index6, hierarchyItem2, cellInMerge, cellMergeChildren, colSpan, rowSpan);
                        this.GetMergedArea(hierarchyItem1, ref rect1, hierarchyItem2, cellInMerge, ref spanRect, ref rowSpanItem, ref colSpanItem);
                      }
                      RectangleF clipBounds = g.ClipBounds;
                      GridCellStyle cellDrawStyle = this.GetCellDrawStyle(rowSpanItem, colSpanItem);
                      GridCellStyle gridCellStyle = this.gridHost.Theme.GridCellStyle;
                      if (cellDrawStyle != null)
                        gridCellStyle = cellDrawStyle;
                      bool flag5 = false;
                      if (this.isConditionalFormattingOn)
                      {
                        Color cellCfColor = this.GetCellCFColor(rowSpanItem, colSpanItem);
                        if (cellCfColor != Color.Empty)
                        {
                          using (SolidBrush solidBrush = new SolidBrush(cellCfColor))
                          {
                            g.FillRectangle((Brush) solidBrush, rect1);
                            flag5 = true;
                          }
                        }
                      }
                      if (!flag5)
                        g.FillRectangle(gridCellStyle.FillStyle.GetBrush(rect1), rect1);
                      if (this.gridHost.GridLinesDisplayMode != GridLinesDisplayMode.DISPLAY_NONE)
                        this.DrawGridLines(g, gridCellStyle.BorderColor, rect1);
                      bool flag6 = false;
                      bool flag7 = false;
                      if (!flag4 && !flag3)
                      {
                        long hash = vDataGridView.ComputeHash(hierarchyItem2, hierarchyItem1);
                        if (cellInMerge != null)
                          hash = vDataGridView.ComputeHash(cellInMerge.ColItem, cellInMerge.RowItem);
                        if (this.selectedCells.ContainsKey(hash))
                        {
                          flag6 = true;
                          flag1 = true;
                          flag7 = true;
                          FillStyle fillStyleSelected = gridCellStyle.FillStyleSelected;
                          Color borderColorSelected = gridCellStyle.BorderColorSelected;
                          if (!flag5)
                            g.FillRectangle(fillStyleSelected.GetBrush(rect1), rect1);
                          if (this.gridHost.GridLinesDisplayMode != GridLinesDisplayMode.DISPLAY_NONE)
                            this.DrawGridLines(g, borderColorSelected, rect1);
                        }
                      }
                      else
                      {
                        flag6 = true;
                        flag1 = true;
                        if (cellInMerge == null && colSpan == 1 && rowSpan == 1)
                          flag7 = true;
                        if (!flag7)
                        {
                          CellSpan itemSpanGroup = this.GetItemSpanGroup(rowSpanItem);
                          if (itemSpanGroup.RowsCount > 1 || itemSpanGroup.ColumnsCount > 1)
                          {
                            flag7 = true;
                            int itemIndex1 = itemSpanGroup.RowItem.ItemIndex;
                            HierarchyItemsCollection hierarchyItemsCollection1 = itemSpanGroup.RowItem.parentItem != null ? itemSpanGroup.RowItem.parentItem.Items : itemSpanGroup.RowItem.HierarchyHost.Items;
                            for (int index7 = itemIndex1; flag7 && index7 < itemIndex1 + itemSpanGroup.RowsCount; ++index7)
                            {
                              if (!hierarchyItemsCollection1[index7].Selected)
                                flag7 = false;
                            }
                            if (!flag7)
                            {
                              flag7 = true;
                              int itemIndex2 = itemSpanGroup.ColumnItem.ItemIndex;
                              HierarchyItemsCollection hierarchyItemsCollection2 = itemSpanGroup.ColumnItem.parentItem != null ? itemSpanGroup.ColumnItem.parentItem.Items : itemSpanGroup.ColumnItem.HierarchyHost.Items;
                              for (int index7 = itemIndex2; flag7 && index7 < itemIndex2 + itemSpanGroup.ColumnsCount; ++index7)
                              {
                                if (!hierarchyItemsCollection2[index7].Selected)
                                  flag7 = false;
                              }
                            }
                          }
                        }
                        if (flag7)
                        {
                          if (!flag5)
                            g.FillRectangle(gridCellStyle.FillStyleSelected.GetBrush(rect1), rect1);
                          if (this.gridHost.GridLinesDisplayMode != GridLinesDisplayMode.DISPLAY_NONE)
                            this.DrawGridLines(g, gridCellStyle.BorderColorSelected, rect1);
                        }
                        if (num1 == -1 && num2 == -1)
                        {
                          num1 = r;
                          num2 = index6;
                        }
                      }
                      if (flag6)
                      {
                        rectangle1 = new Rectangle(Math.Min(rectangle2.X, rectangle1.X), Math.Min(rectangle2.Y, rectangle1.Y), rectangle1.Width, rectangle1.Height);
                        if (rectangle2.Right > rectangle1.Right)
                          rectangle1.Width += rectangle2.Width;
                        if (rectangle2.Bottom > rectangle1.Bottom)
                          rectangle1.Height += rectangle2.Height;
                        if (index2 > index6)
                          index2 = index6;
                        if (index1 > r)
                          index1 = r;
                        if (index4 < index6)
                          index4 = index6;
                        if (index3 < r)
                          index3 = r;
                      }
                      if (!this.gridHost.OnCellPaintBegin(rowSpanItem, colSpanItem, this, rect1, g))
                      {
                        bool flag8 = this.gridHost.OnCellCustomPaint(rowSpanItem, colSpanItem, this, rect1, g);
                        if (flag8 && this.gridHost.AllowClipDrawing)
                          g.Clip = new Region(clipBounds);
                        bool flag9 = !this.gridHost.IsScrolling || this.gridHost.BindingState != GridBindingState.BoundPivot || this.gridHost.RowsCount <= 50000;
                        if (!flag8 && flag9)
                        {
                          object CellValue = (object) null;
                          Image image = (Image) null;
                          if (this.GetCellDataSource(rowSpanItem, colSpanItem) == GridCellDataSource.Virtual)
                          {
                            int CellImageIndex = -1;
                            this.gridHost.OnCellValueNeeded(rowSpanItem, colSpanItem, out CellValue, ref CellImageIndex);
                            if (CellImageIndex >= 0 && this.gridHost.ImageList != null && CellImageIndex < this.gridHost.ImageList.Images.Count)
                              image = this.gridHost.ImageList.Images[CellImageIndex];
                          }
                          else
                          {
                            CellValue = this.GetCellValue(rowSpanItem, colSpanItem);
                            image = this.GetCellImageInternal(rowSpanItem, colSpanItem);
                          }
                          string cellFormattedText = this.GetCellFormattedText(rowSpanItem, colSpanItem, CellValue);
                          IEditor cellEditor = this.GetCellEditor(rowSpanItem, colSpanItem);
                          TextImageRelation textImageRelation = this.GetCellTextImageRelation(rowSpanItem, colSpanItem);
                          DisplaySettings cellDisplaySettings = this.GetCellDisplaySettings(rowSpanItem, colSpanItem);
                          ContentAlignment cellTextAlignment = this.GetCellTextAlignment(rowSpanItem, colSpanItem);
                          ContentAlignment cellImageAlignment = this.GetCellImageAlignment(rowSpanItem, colSpanItem);
                          bool cellTextWrap = this.GetCellTextWrap(rowSpanItem, colSpanItem);
                          bool isDrawHandled = false;
                          bool flag10 = false;
                          if (cellEditor != null)
                          {
                            flag10 = this.gridHost.activeEditor.Editor != null && this.gridHost.activeEditor.ColumnItem != null && (this.gridHost.activeEditor.RowItem != null && this.gridHost.activeEditor.RowItem == rowSpanItem) && this.gridHost.activeEditor.ColumnItem == colSpanItem;
                            if (this.gridHost.activeEditor.Editor == null || !flag10)
                              cellEditor.DrawEditorControl(g, new GridCell(rowSpanItem, colSpanItem, this), out isDrawHandled);
                          }
                          if (!isDrawHandled && !flag10)
                          {
                            if (cellDisplaySettings == DisplaySettings.ImageOnly)
                              this.DrawImage(g, new GridCell(hierarchyItem1, hierarchyItem2, this));
                            else if (image == null || cellDisplaySettings == DisplaySettings.TextOnly)
                            {
                              Rectangle Bounds = rect1;
                              Bounds.Inflate(-2, -2);
                              Bounds.Offset(1, 1);
                              this.paintHelper.DrawText(g, Bounds, cellTextWrap, flag7 ? gridCellStyle.TextColorSelected : gridCellStyle.TextColor, cellFormattedText, gridCellStyle.Font, cellTextAlignment);
                            }
                            else if (cellDisplaySettings == DisplaySettings.TextAndImage && image != null)
                            {
                              using (SolidBrush textBrush = new SolidBrush(flag7 ? gridCellStyle.TextColorSelected : gridCellStyle.TextColor))
                              {
                                Rectangle rectangle3 = rect1;
                                rectangle3.Inflate(-2, -2);
                                rectangle3.Offset(1, 1);
                                Rectangle rect2 = new Rectangle(rectangle3.X + 4, rectangle3.Y, rectangle3.Width - 8, rectangle3.Height);
                                this.paintHelper.DrawImageAndTextRectangle(g, cellTextWrap, 0, rect2, true, gridCellStyle.Font, textBrush, cellImageAlignment, cellTextAlignment, cellFormattedText, image, textImageRelation);
                              }
                            }
                          }
                          if (this.gridHost.AllowClipDrawing)
                            g.Clip = new Region(clipBounds);
                        }
                      }
                    }
                    else
                      break;
                  }
                }
              }
            }
            else
              break;
          }
        }
      }
      g.DrawRectangle(new Pen(this.gridHost.Theme.HierarchyItemStyleNormal.BorderColor, 1f), new Rectangle(x1, y1, this.gridHost.ColumnsHierarchy.Width, this.gridHost.RowsHierarchy.Height));
      if (!this.gridHost.SelectionBorderEnabled || !flag1 || this.gridHost.GroupingEnabled && this.gridHost.GroupingColumns.Count > 0 || this.EditCell != null && (this.EditCell.Editor.ActivationFlags & EditorActivationFlags.MOUSE_MOVE) != EditorActivationFlags.MOUSE_MOVE)
        return;
      bool flag11 = true;
      for (int index5 = index1; index5 <= index3 && flag11; ++index5)
      {
        for (int index6 = index2; index6 <= index4 && flag11; ++index6)
        {
          HierarchyItem rowItem = pv2[index5];
          HierarchyItem columnItem = pv1[index6];
          if (!this.gridHost.RowsHierarchy.IsItemSelected(rowItem) && !this.gridHost.ColumnsHierarchy.IsItemSelected(columnItem) && !this.IsCellSelected(rowItem, columnItem))
            flag11 = false;
        }
      }
      if (!flag11)
        return;
      RectangleF clipBounds1 = g.ClipBounds;
      if (point.X > 0 || point.Y > 0)
      {
        HierarchyItem hierarchyItem1 = pv2[index1];
        HierarchyItem hierarchyItem2 = pv2[index3];
        HierarchyItem hierarchyItem3 = pv1[index2];
        HierarchyItem hierarchyItem4 = pv1[index4];
        Rectangle rectangle2 = new Rectangle(0, 0, point.X, this.gridHost.Height);
        Rectangle rectangle3 = new Rectangle(0, 0, this.gridHost.Width, point.Y);
        // ISSUE: explicit reference operation
        // ISSUE: variable of a reference type
        drawBounds1 = hierarchyItem3.DrawBounds;
        int x2 = drawBounds1.X;
        drawBounds1 = hierarchyItem1.DrawBounds;
        int y2 = drawBounds1.Y;
        drawBounds1 = hierarchyItem4.DrawBounds;
        int num5 = drawBounds1.X + hierarchyItem4.Width;
        drawBounds1 = hierarchyItem3.DrawBounds;
        int x3 = drawBounds1.X;
        int width = num5 - x3;
        drawBounds1 = hierarchyItem2.DrawBounds;
        int num6 = drawBounds1.Y + hierarchyItem2.Height;
        drawBounds1 = hierarchyItem1.DrawBounds;
        int y3 = drawBounds1.Y;
        int height = num6 - y3;
        rectangle1 = new Rectangle(x2, y2, width, height);
        if (hierarchyItem1.Fixed && hierarchyItem2.Fixed && (hierarchyItem3.Fixed && hierarchyItem4.Fixed))
          g.Clip = new Region(new RectangleF(0.0f, 0.0f, (float) rectangle2.Right, (float) rectangle3.Bottom));
        if (hierarchyItem1.Fixed && !hierarchyItem2.Fixed)
        {
          drawBounds1 = hierarchyItem2.DrawBounds;
          if (drawBounds1.Top + hierarchyItem2.Height < point.Y)
          {
            int y4 = point.Y;
            drawBounds1 = hierarchyItem1.DrawBounds;
            int top = drawBounds1.Top;
            int num7 = y4 - top;
            rectangle1.Height = num7;
          }
        }
        if (hierarchyItem3.Fixed && !hierarchyItem4.Fixed)
        {
          drawBounds1 = hierarchyItem4.DrawBounds;
          if (drawBounds1.Right < point.X)
          {
            int x4 = point.X;
            drawBounds1 = hierarchyItem3.DrawBounds;
            int left = drawBounds1.Left;
            int num7 = x4 - left;
            rectangle1.Width = num7;
          }
        }
        if (!hierarchyItem3.Fixed && !hierarchyItem4.Fixed && (!hierarchyItem1.Fixed && !hierarchyItem2.Fixed))
          g.Clip = new Region(new RectangleF((float) point.X, (float) point.Y, (float) this.gridHost.Width, (float) this.gridHost.Height));
        else if (!hierarchyItem3.Fixed && !hierarchyItem4.Fixed)
          g.Clip = new Region(new RectangleF((float) point.X, 0.0f, (float) this.gridHost.Width, (float) this.gridHost.Height));
        else if (!hierarchyItem1.Fixed && !hierarchyItem2.Fixed)
          g.Clip = new Region(new RectangleF(0.0f, (float) point.Y, (float) this.gridHost.Width, (float) this.gridHost.Height));
      }
      int selectionBorderWidth = this.gridHost.SelectionBorderWidth;
      Rectangle rect = rectangle1;
      ++rect.X;
      ++rect.Y;
      rect.Width -= selectionBorderWidth == 2 ? 1 : 2;
      rect.Height -= selectionBorderWidth == 2 ? 1 : 2;
      using (Pen pen = new Pen(Color.Black, (float) selectionBorderWidth))
      {
        pen.DashStyle = this.gridHost.SelectionBorderDashStyle;
        pen.Alignment = PenAlignment.Inset;
        g.DrawRectangle(pen, rect);
      }
      g.Clip = new Region(clipBounds1);
    }

    private void DrawGridLines(Graphics g, Color penColor, Rectangle rect)
    {
      GridLinesDisplayMode linesDisplayMode = this.gridHost.GridLinesDisplayMode;
      if (linesDisplayMode == GridLinesDisplayMode.DISPLAY_NONE)
        return;
      if (this.gridHost.GroupingEnabled && this.gridHost.GroupingColumns.Count > 0)
        linesDisplayMode = GridLinesDisplayMode.DISPLAY_ROW_LINES;
      using (Pen pen = new Pen(penColor))
      {
        pen.DashStyle = this.gridHost.GridLinesDashStyle;
        if (linesDisplayMode == GridLinesDisplayMode.DISPLAY_ALL)
          g.DrawRectangle(pen, rect);
        else if (linesDisplayMode == GridLinesDisplayMode.DISPLAY_COLUMN_LINES)
        {
          g.DrawLine(pen, rect.Left, rect.Top, rect.Left, rect.Bottom);
          g.DrawLine(pen, rect.Right, rect.Top, rect.Right, rect.Bottom);
        }
        else
        {
          if (linesDisplayMode != GridLinesDisplayMode.DISPLAY_ROW_LINES)
            return;
          g.DrawLine(pen, rect.Left, rect.Top, rect.Right, rect.Top);
          g.DrawLine(pen, rect.Left, rect.Bottom, rect.Right, rect.Bottom);
        }
      }
    }

    /// <summary>Clears the merged cells.</summary>
    public void ClearCellsSpan()
    {
      this.hashCellInSpan.Clear();
      this.hashCellSpan.Clear();
      this.hashCellChildInSpan.Clear();
      this.GridControl.Invalidate();
    }

    /// <summary>Removes the cell merge.</summary>
    /// <param name="rowItem">The row item.</param>
    /// <param name="columnItem">The column item.</param>
    public void RemoveCellSpan(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (rowItem == null || columnItem == null)
        return;
      int num1 = this.GetCellSpan(rowItem, columnItem).ColumnsCount;
      int num2 = this.GetCellSpan(rowItem, columnItem).RowsCount;
      if (num1 > 1 && num2 == 1)
      {
        RowColItemPair rowColItemPair = new RowColItemPair(rowItem, columnItem);
        List<RowColItemPair> rowColItemPairList = new List<RowColItemPair>();
        int num3 = this.GridControl.ColumnsHierarchy.Items.IndexOf(columnItem);
        for (int index = 1; index < num1; ++index)
        {
          if (num3 + index < this.GridControl.ColumnsHierarchy.Items.Count)
          {
            HierarchyItem colItem = this.GridControl.ColumnsHierarchy.Items[num3 + index];
            if (colItem != null && colItem.ParentItem == columnItem.ParentItem)
            {
              long hash = vDataGridView.ComputeHash(colItem, rowItem);
              if (this.hashCellInSpan.Contains((object) hash))
              {
                this.hashCellInSpan.Remove((object) hash);
                rowColItemPairList.Add(new RowColItemPair(rowItem, colItem));
              }
            }
          }
        }
        if (rowColItemPairList.Count > 0)
        {
          long hash = vDataGridView.ComputeHash(columnItem, rowItem);
          if (this.hashCellChildInSpan.Contains((object) hash))
            this.hashCellChildInSpan.Remove((object) hash);
        }
      }
      else if (num2 > 1 && num1 == 1)
      {
        RowColItemPair rowColItemPair = new RowColItemPair(rowItem, columnItem);
        List<RowColItemPair> rowColItemPairList = new List<RowColItemPair>();
        int num3 = this.GridControl.RowsHierarchy.Items.IndexOf(rowItem);
        for (int index = 1; index < num2; ++index)
        {
          HierarchyItem rowItem1 = this.GridControl.RowsHierarchy.Items[num3 + index];
          if (rowItem != null && rowItem.ParentItem == rowItem1.ParentItem)
          {
            long hash = vDataGridView.ComputeHash(columnItem, rowItem1);
            if (this.hashCellInSpan.Contains((object) hash))
            {
              this.hashCellInSpan.Remove((object) hash);
              rowColItemPairList.Add(new RowColItemPair(rowItem1, columnItem));
            }
          }
        }
        if (rowColItemPairList.Count > 0)
        {
          long hash = vDataGridView.ComputeHash(columnItem, rowItem);
          if (this.hashCellChildInSpan.Contains((object) hash))
            this.hashCellChildInSpan.Remove((object) hash);
        }
      }
      else if (num1 > 1 && num2 > 1)
      {
        RowColItemPair rowColItemPair = new RowColItemPair(rowItem, columnItem);
        List<RowColItemPair> rowColItemPairList = new List<RowColItemPair>();
        int num3 = this.GridControl.RowsHierarchy.Items.IndexOf(rowItem);
        int num4 = this.GridControl.ColumnsHierarchy.Items.IndexOf(columnItem);
        for (int index1 = 0; index1 < num1; ++index1)
        {
          for (int index2 = 0; index2 < num2; ++index2)
          {
            if (index2 != 0 || index1 != 0)
            {
              HierarchyItem rowItem1 = this.GridControl.RowsHierarchy.Items.Count > num3 + index2 ? this.GridControl.RowsHierarchy.Items[num3 + index2] : (HierarchyItem) null;
              HierarchyItem colItem = this.GridControl.ColumnsHierarchy.Items.Count > num4 + index1 ? this.GridControl.ColumnsHierarchy.Items[num4 + index1] : (HierarchyItem) null;
              if (rowItem1 != null && colItem != null && (rowItem.ParentItem == rowItem1.ParentItem && columnItem.ParentItem == colItem.ParentItem))
              {
                long hash = vDataGridView.ComputeHash(colItem, rowItem1);
                if (this.hashCellInSpan.Contains((object) hash))
                {
                  this.hashCellInSpan.Remove((object) hash);
                  rowColItemPairList.Add(new RowColItemPair(rowItem1, colItem));
                }
              }
            }
          }
        }
        if (rowColItemPairList.Count > 0)
        {
          long hash = vDataGridView.ComputeHash(columnItem, rowItem);
          if (this.hashCellChildInSpan.Contains((object) hash))
            this.hashCellChildInSpan.Remove((object) hash);
        }
      }
      long hash1 = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashCellSpan.ContainsKey((object) hash1))
        this.hashCellSpan.Remove((object) hash1);
      this.GridControl.Invalidate();
    }

    private void CalculateVisibleMergedCells(List<HierarchyItem> visibleColItems, List<HierarchyItem> visibleRowItems, int r, HierarchyItem rowitem, int c, HierarchyItem colitem, RowColItemPair rowColItem, List<RowColItemPair> cellChildren, int colSpan, int rowSpan)
    {
      if (rowColItem != null)
        return;
      if (colSpan > 1 && rowSpan == 1)
      {
        RowColItemPair key1 = new RowColItemPair(rowitem, colitem);
        List<RowColItemPair> key2 = new List<RowColItemPair>();
        for (int index = 1; index < colSpan; ++index)
        {
          HierarchyItem hierarchyItem = visibleColItems.Count > c + index ? visibleColItems[c + index] : (HierarchyItem) null;
          if (hierarchyItem != null && hierarchyItem.ParentItem == colitem.ParentItem)
          {
            this.SetCellInMerge(rowitem, hierarchyItem, key1);
            key2.Add(new RowColItemPair(rowitem, hierarchyItem));
          }
        }
        if (key2.Count > 0)
          this.SetCellMergeChildren(rowitem, colitem, key2);
      }
      if (rowSpan > 1 && colSpan == 1)
      {
        RowColItemPair key1 = new RowColItemPair(rowitem, colitem);
        List<RowColItemPair> key2 = new List<RowColItemPair>();
        for (int index = 1; index < rowSpan; ++index)
        {
          HierarchyItem rowItem = visibleRowItems.Count > r + index ? visibleRowItems[r + index] : (HierarchyItem) null;
          if (rowItem != null && rowItem.ParentItem == rowitem.ParentItem)
          {
            this.SetCellInMerge(rowItem, colitem, key1);
            key2.Add(new RowColItemPair(rowItem, colitem));
          }
        }
        if (key2.Count > 0)
          this.SetCellMergeChildren(rowitem, colitem, key2);
      }
      if (colSpan <= 1 || rowSpan <= 1)
        return;
      RowColItemPair key3 = new RowColItemPair(rowitem, colitem);
      List<RowColItemPair> key4 = new List<RowColItemPair>();
      for (int index1 = 0; index1 < colSpan; ++index1)
      {
        for (int index2 = 0; index2 < rowSpan; ++index2)
        {
          if (index2 != 0 || index1 != 0)
          {
            HierarchyItem rowItem = visibleRowItems.Count > r + index2 ? visibleRowItems[r + index2] : (HierarchyItem) null;
            HierarchyItem hierarchyItem = visibleColItems.Count > c + index1 ? visibleColItems[c + index1] : (HierarchyItem) null;
            if (rowItem != null && hierarchyItem != null && (rowItem.ParentItem == rowitem.ParentItem && hierarchyItem.ParentItem == colitem.ParentItem))
            {
              this.SetCellInMerge(rowItem, hierarchyItem, key3);
              key4.Add(new RowColItemPair(rowItem, hierarchyItem));
            }
          }
        }
      }
      if (key4.Count <= 0)
        return;
      this.SetCellMergeChildren(rowitem, colitem, key4);
    }

    private void GetMergedArea(HierarchyItem rowitem, ref Rectangle rect, HierarchyItem colitem, RowColItemPair rowColItem, ref Rectangle spanRect, ref HierarchyItem rowSpanItem, ref HierarchyItem colSpanItem)
    {
      if (rowColItem == null)
        return;
      rowSpanItem = rowColItem == null ? rowitem : rowColItem.RowItem;
      colSpanItem = rowColItem == null ? colitem : rowColItem.ColItem;
      spanRect.Location = rect.Location;
      Point offset = Point.Empty;
      spanRect.Size = this.GetSpannedCellSize(rowSpanItem, colSpanItem, rowitem, colitem, ref offset);
      if (!(spanRect.Size != Size.Empty))
        return;
      rect = spanRect;
      rect.Location = new Point(rect.Location.X - offset.X, rect.Location.Y - offset.Y);
    }

    internal Image DrawImage(Graphics graphics, GridCell cell)
    {
      Image cellImageInternal = this.GetCellImageInternal(cell.RowItem, cell.ColumnItem);
      if (cellImageInternal == null)
        return (Image) null;
      Size size1 = cellImageInternal.Size;
      if (size1.Width > cell.Bounds.Width)
        size1.Width = cell.Bounds.Width;
      if (size1.Height > cell.Bounds.Height)
        size1.Height = cell.Bounds.Height;
      Size size2 = Size.Empty;
      size2.Width += cell.ColumnItem.X + this.gridHost.ColumnsHierarchy.X;
      size2.Height += cell.RowItem.Y + this.gridHost.RowsHierarchy.Y;
      switch (cell.ImagePosition)
      {
        case ImagePositions.Center:
          this.paintHelper.DrawBitmap(graphics, cellImageInternal, size2.Width + cell.ColumnItem.Width / 2 - size1.Width / 2, size2.Height + cell.RowItem.Height / 2 - size1.Height / 2);
          return cellImageInternal;
        case ImagePositions.Tile:
          int num1 = 1;
          while (num1 < cell.RowItem.Height - 2)
          {
            Size size3 = size1;
            if (num1 + size2.Height + size3.Height > cell.Bounds.Bottom + this.gridHost.RowsHierarchy.Y)
            {
              int num2 = size1.Height - (num1 + size2.Height + size3.Height - cell.Bounds.Bottom - this.gridHost.RowsHierarchy.Y);
              if (num2 < 0)
                num2 = 0;
              size3.Height = num2;
            }
            int num3 = 1;
            while (num3 < cell.ColumnItem.Width - 2)
            {
              if (num3 + size2.Width + size3.Width > cell.Bounds.Right + this.gridHost.ColumnsHierarchy.X)
              {
                int num2 = size1.Width - (num3 + size2.Width + size3.Width - cell.Bounds.Right - this.gridHost.ColumnsHierarchy.X);
                if (num2 < 0)
                  num2 = 0;
                size3.Width = num2;
              }
              this.paintHelper.DrawBitmap(graphics, cellImageInternal, num3 + size2.Width, num1 + size2.Height, size3.Width, size3.Height);
              num3 += size1.Width;
            }
            num1 += size1.Height;
          }
          return cellImageInternal;
        case ImagePositions.Stretch:
          if (cellImageInternal.Width == 0 || cellImageInternal.Height == 0)
            return cellImageInternal;
          float num4 = (float) cell.ColumnItem.Width / (float) cellImageInternal.Width;
          float num5 = (float) cell.RowItem.Height / (float) cellImageInternal.Height;
          if ((double) num4 > 0.0 && (double) num5 > 0.0)
          {
            int num2 = (int) Math.Round((double) cellImageInternal.Width * (double) num4);
            int num3 = (int) Math.Round((double) cellImageInternal.Height * (double) num5);
            int x = size2.Width + 1;
            int y = size2.Height + 1;
            this.paintHelper.DrawBitmap(graphics, cellImageInternal, x, y, num2 - 2, num3 - 2);
          }
          return cellImageInternal;
        default:
          return cellImageInternal;
      }
    }

    /// <summary>Returns the grid cell at a specific position</summary>
    /// <param name="point"></param>
    /// <returns>A reference to a grid cell or null if the position is outside of the cells area</returns>
    public GridCell HitTest(Point point)
    {
      HierarchyItem rowHierarchyItem = (HierarchyItem) null;
      HierarchyItem colHierarchyItem = (HierarchyItem) null;
      int refHitRow = -1;
      int refHitCol = -1;
      if (!this.HitTest(point, ref colHierarchyItem, ref rowHierarchyItem, ref refHitRow, ref refHitCol))
        return (GridCell) null;
      return new GridCell(rowHierarchyItem, colHierarchyItem, this);
    }

    internal bool HitTest(Point point, ref HierarchyItem colHierarchyItem, ref HierarchyItem rowHierarchyItem, ref int refHitRow, ref int refHitCol)
    {
      int itemIndex1 = -1;
      int itemIndex2 = -1;
      colHierarchyItem = this.gridHost.ColumnsHierarchy.PointToLeafItem(point, ref itemIndex1);
      rowHierarchyItem = this.gridHost.RowsHierarchy.PointToLeafItem(point, ref itemIndex2);
      refHitCol = colHierarchyItem == null ? -1 : colHierarchyItem.ItemIndex;
      refHitRow = rowHierarchyItem == null ? -1 : rowHierarchyItem.ItemIndex;
      if (colHierarchyItem != null)
        return rowHierarchyItem != null;
      return false;
    }

    public void UnlockAllCells()
    {
      this.hashLockedCells.Clear();
    }

    /// <summary>Locks a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    public void LockCell(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (!this.hashLockedCells.ContainsKey((object) hash))
        this.hashLockedCells.Add((object) hash, (object) 0);
      this.OnPropertyChanged("LockCell");
    }

    /// <summary>Unlocks a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    public void UnlockCell(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.hashLockedCells.ContainsKey((object) hash))
        this.hashLockedCells.Remove((object) hash);
      this.OnPropertyChanged("UnLockCell");
    }

    /// <summary>Determines if a grid cell is locked or not</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <returns></returns>
    public bool IsLockedCell(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      return this.hashLockedCells.ContainsKey((object) vDataGridView.ComputeHash(columnItem, rowItem));
    }

    /// <exclude />
    protected internal virtual void OnPropertyChanged(string propertyName)
    {
      PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
      string str;
      if ((str = propertyName) != null)
      {
        int num = str == "" ? 1 : 0;
      }
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    private void ClearCellsConditionalFormatting()
    {
      this.hashCellCFGroups.Clear();
      this.hashItemsCFGroups.Clear();
    }

    private Color GetCellCFColor(HierarchyItem rowItem, HierarchyItem colItem)
    {
      long hash = vDataGridView.ComputeHash(colItem, rowItem);
      string key = "";
      if (this.hashCellCFGroups.ContainsKey((object) hash))
        key = ((CellsArea.CellCFGroup) this.hashCellCFGroups[(object) hash]).groupName;
      else if (this.hashItemsCFGroups.ContainsKey((object) colItem))
        key = (string) this.hashItemsCFGroups[(object) colItem];
      else if (this.hashItemsCFGroups.ContainsKey((object) rowItem))
        key = (string) this.hashItemsCFGroups[(object) rowItem];
      if (string.IsNullOrEmpty(key))
        return Color.Empty;
      object cellValue = this.GetCellValue(rowItem, colItem);
      if (cellValue == null)
        return Color.Empty;
      double result = 0.0;
      if (!double.TryParse(cellValue.ToString(), out result))
        return Color.Empty;
      Color color = Color.Empty;
      if (this.cfGroups.ContainsKey(key))
        color = this.cfGroups[key].GetCFColor(result);
      return color;
    }

    private void UpdateCellCFGroup(HierarchyItem rowItem, HierarchyItem colItem, CellsArea.ConditionalFormattingGroup cfGroup)
    {
      object cellValue = this.GetCellValue(rowItem, colItem);
      if (cellValue == null)
        return;
      double result = 0.0;
      if (!double.TryParse(cellValue.ToString(), out result))
        return;
      cfGroup.UpdateMinMaxWithValue(result);
    }

    private void UpdateCFGroupValues(List<HierarchyItem> rowItems, List<HierarchyItem> colItems)
    {
      Dictionary<string, CellsArea.ConditionalFormattingGroup>.Enumerator enumerator1 = this.ConditionalFormattingGroups.GetEnumerator();
      while (enumerator1.MoveNext())
      {
        string key = enumerator1.Current.Key;
        CellsArea.ConditionalFormattingGroup cfGroup = enumerator1.Current.Value;
        cfGroup.ResetMinMax();
        IDictionaryEnumerator enumerator2 = this.hashCellCFGroups.GetEnumerator();
        while (enumerator2.MoveNext())
        {
          long num = (long) enumerator2.Key;
          CellsArea.CellCFGroup cellCfGroup = (CellsArea.CellCFGroup) enumerator2.Value;
          if (key == cellCfGroup.groupName)
            this.UpdateCellCFGroup(cellCfGroup.rowItem, cellCfGroup.colItem, cfGroup);
        }
        IDictionaryEnumerator enumerator3 = this.hashItemsCFGroups.GetEnumerator();
        while (enumerator3.MoveNext())
        {
          HierarchyItem hierarchyItem1 = (HierarchyItem) enumerator3.Key;
          if (!((string) enumerator3.Value != key))
          {
            foreach (HierarchyItem hierarchyItem2 in hierarchyItem1.IsColumnsHierarchyItem ? rowItems : colItems)
            {
              HierarchyItem colItem = hierarchyItem1.IsColumnsHierarchyItem ? hierarchyItem1 : hierarchyItem2;
              this.UpdateCellCFGroup(hierarchyItem1.IsColumnsHierarchyItem ? hierarchyItem2 : hierarchyItem1, colItem, cfGroup);
            }
          }
        }
      }
    }

    /// <summary>
    /// Specifies the conditional formatting group name for a specific row or column item
    /// </summary>
    /// <param name="item">Row or column item to include in the group</param>
    /// <param name="groupName">The name of the conditional formatting group</param>
    [Obsolete("Please, use SetCellConditionalFormattingGroup")]
    public void SetItemConditionalFormattingGroup(HierarchyItem item, string groupName)
    {
      this.SetCellConditionalFormattingGroup(item, groupName);
    }

    /// <summary>
    /// Specifies the conditional formatting group name for a specific row or column item
    /// </summary>
    /// <param name="item">Row or column item to include in the group</param>
    /// <param name="groupName">The name of the conditional formatting group</param>
    public void SetCellConditionalFormattingGroup(HierarchyItem item, string groupName)
    {
      if (this.hashItemsCFGroups.ContainsKey((object) item))
        this.hashItemsCFGroups.Remove((object) item);
      this.hashItemsCFGroups.Add((object) item, (object) groupName);
    }

    /// <summary>
    /// Specifies the conditional formatting group name for a specific grid cell
    /// </summary>
    /// <param name="cell">A reference to the grid cell</param>
    /// <param name="groupName">The name of the conditional formatting group</param>
    public void SetCellConditionalFormattingGroup(GridCell cell, string groupName)
    {
      this.SetCellConditionalFormattingGroup(cell.RowItem, cell.ColumnItem, groupName);
    }

    /// <summary>
    /// Specifies the conditional formatting group name for a specific grid cell
    /// </summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <param name="groupName">The name of the conditional formatting group</param>
    public void SetCellConditionalFormattingGroup(HierarchyItem rowItem, HierarchyItem columnItem, string groupName)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      CellsArea.CellCFGroup cellCfGroup;
      cellCfGroup.colItem = columnItem;
      cellCfGroup.rowItem = rowItem;
      cellCfGroup.groupName = groupName;
      if (this.hashCellCFGroups.ContainsKey((object) hash))
        this.hashCellCFGroups.Remove((object) hash);
      this.hashCellCFGroups.Add((object) hash, (object) cellCfGroup);
    }

    private void LoadDefaultCFColorScales()
    {
      Bitmap bitmap = new Bitmap(Assembly.GetAssembly(typeof (vDataGridView)).GetManifestResourceStream("VIBlend.WinForms.DataGridView.Resources.colormap.bmp"));
      CellsArea.defaultCFMap.LoadFromBitmap(bitmap);
    }

    private Point PointToParent(Point p)
    {
      return new Point(p.X + this.bounds.X, p.Y + this.bounds.Y);
    }

    internal GridCell GetCellAtDistance(GridCell cell, Point distance)
    {
      Rectangle spannedCellRect = this.GetSpannedCellRect(cell.RowItem, cell.ColumnItem);
      return this.HitTest(new Point(spannedCellRect.X + distance.X, spannedCellRect.Y + distance.Y - this.GridControl.VerticalScroll));
    }

    /// <summary>Returns the cell displayed above a specific cell</summary>
    /// <param name="cell">The grid cell used as a starting point.</param>
    /// <returns>The grid cell that appears above the specified cell. If such cell does not exist the method return null.</returns>
    public GridCell GetCellAbove(GridCell cell)
    {
      Rectangle rectangle = new Rectangle(cell.ColumnItem.DrawBounds.X, cell.RowItem.DrawBounds.Y, cell.ColumnItem.Width, cell.RowItem.Height);
      return this.HitTest(new Point(rectangle.Right - 1, rectangle.Y - 1));
    }

    /// <summary>Returns the cell displayed below a specific cell</summary>
    /// <param name="cell">The grid cell used as a starting point.</param>
    /// <returns>The grid cell that appears below the specified cell. If such cell does not exist the method return null.</returns>
    public GridCell GetCellBelow(GridCell cell)
    {
      Rectangle rectangle = new Rectangle(cell.ColumnItem.DrawBounds.X, cell.RowItem.DrawBounds.Y, cell.ColumnItem.Width, cell.RowItem.Height);
      return this.HitTest(new Point(rectangle.Right - 1, rectangle.Bottom + 1));
    }

    /// <summary>
    /// Returns the cell displayed on the left of a specific cell
    /// </summary>
    /// <param name="cell">The grid cell used as a starting point.</param>
    /// <returns>The grid cell that appears on the left side of the specified cell. If such cell does not exist the method return null.</returns>
    public GridCell GetCellLeft(GridCell cell)
    {
      Rectangle rectangle = new Rectangle(cell.ColumnItem.DrawBounds.X, cell.RowItem.DrawBounds.Y, cell.ColumnItem.Width, cell.RowItem.Height);
      return this.HitTest(new Point(rectangle.X - 1, rectangle.Bottom - 1));
    }

    /// <summary>
    /// Returns the cell displayed on the right of a specific cell
    /// </summary>
    /// <param name="cell">The grid cell used as a starting point.</param>
    /// <returns>The grid cell that appears on the right side of the specified cell. If such cell does not exist the method return null.</returns>
    public GridCell GetCellRight(GridCell cell)
    {
      Rectangle rectangle = new Rectangle(cell.ColumnItem.DrawBounds.X, cell.RowItem.DrawBounds.Y, cell.ColumnItem.Width, cell.RowItem.Height);
      return this.HitTest(new Point(rectangle.Right + 1, rectangle.Bottom - 1));
    }

    internal SizeF MeasureBestFitSize(HierarchyItem rowitem, HierarchyItem colitem)
    {
      SizeF sizeF1 = (SizeF) new Size(1, 1);
      object cellValue = this.GetCellValue(rowitem, colitem);
      if (cellValue == null)
        return sizeF1;
      GridCellStyle cellDrawStyle = this.GetCellDrawStyle(rowitem, colitem);
      GridCellStyle gridCellStyle = this.gridHost.Theme.GridCellStyle;
      if (cellDrawStyle != null)
        gridCellStyle = cellDrawStyle;
      if (vDataGridView.GraphicsMeasure == null)
      {
        if (this.gridHost == null)
          return sizeF1;
        vDataGridView.GraphicsMeasure = this.gridHost.CreateGraphics();
      }
      SizeF sizeF2 = vDataGridView.GraphicsMeasure.MeasureString(cellValue.ToString(), gridCellStyle.Font);
      sizeF2.Width += 5f;
      return sizeF2;
    }

    internal bool GetCellsMinMaxDate(HierarchyItem item, System.Type type, ref DateTime minValue, ref DateTime maxValue)
    {
      try
      {
        List<GridCell> cellsList = new List<GridCell>();
        this.GetCells(ref cellsList, item, false);
        if (type == typeof (DateTime))
        {
          minValue = DateTime.MaxValue;
          maxValue = DateTime.MinValue;
        }
        foreach (GridCell gridCell in cellsList)
        {
          if (!this.gridHost.GroupingEnabled || gridCell.RowItem.Depth >= this.gridHost.GroupingColumns.Count)
          {
            object cellValue = this.GetCellValue(gridCell.RowItem, gridCell.ColumnItem);
            if (cellValue == null || cellValue.GetType() != type)
              return false;
            if (type == typeof (DateTime))
            {
              if ((DateTime) cellValue > maxValue)
                maxValue = (DateTime) cellValue;
              if ((DateTime) cellValue < minValue)
                minValue = (DateTime) cellValue;
            }
          }
        }
      }
      catch (Exception ex)
      {
        return false;
      }
      return true;
    }

    /// <summary>Clears all format settings for the grid cells.</summary>
    public void ClearCellFormatSettings()
    {
      this.cellFormatSettings.Clear();
    }

    /// <summary>Sets the formatting for a grid cell.</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <param name="formatString">Format string for the grid cell's text.</param>
    public void SetCellFormatString(HierarchyItem rowItem, HierarchyItem columnItem, string formatString)
    {
      this.cellFormatSettings.SetPropertyValue("FormatString", vDataGridView.ComputeHash(columnItem, rowItem), (object) formatString);
    }

    /// <summary>Gets the formatting for a grid cell.</summary>
    /// <param name="rowItem">Cell's row</param>
    public string GetCellFormatString(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      object propertyValue = this.cellFormatSettings.GetPropertyValue("FormatString", vDataGridView.ComputeHash(columnItem, rowItem));
      if (propertyValue == null || propertyValue.GetType() != typeof (string))
        return "";
      return (string) propertyValue;
    }

    /// <summary>Sets the format provider for a grid cell.</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <param name="formatProvider">Format provider to use for the grid cell.</param>
    public void SetCellFormatProvider(HierarchyItem rowItem, HierarchyItem columnItem, IFormatProvider formatProvider)
    {
      this.cellFormatSettings.SetPropertyValue("FormatProvider", vDataGridView.ComputeHash(columnItem, rowItem), (object) formatProvider);
    }

    /// <summary>Gets the format provider for a grid cell.</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    public IFormatProvider GetCellFormatProvider(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      return (IFormatProvider) this.cellFormatSettings.GetPropertyValue("FormatProvider", vDataGridView.ComputeHash(columnItem, rowItem)) ?? (IFormatProvider) null;
    }

    /// <summary>
    /// Gets the cell's value as formatted text using the respective format provider and format expression
    /// </summary>
    public string GetCellFormattedText(GridCell cell)
    {
      return this.GetCellFormattedText(cell.RowItem, cell.ColumnItem, cell.Value);
    }

    internal string GetCellFormattedText(HierarchyItem rowItem, HierarchyItem columnItem, object cellValue)
    {
      if (cellValue == null)
        return "";
      vDataGridView.ComputeHash(columnItem, rowItem);
      string format = this.GetCellFormatString(rowItem, columnItem);
      if (string.IsNullOrEmpty(format))
        format = columnItem.CellsFormatString;
      if (string.IsNullOrEmpty(format))
        format = rowItem.CellsFormatString;
      if (string.IsNullOrEmpty(format))
      {
        if (cellValue == null)
          return "";
        return cellValue.ToString();
      }
      IFormatProvider formatProvider = (this.GetCellFormatProvider(rowItem, columnItem) ?? columnItem.CellsFormatProvider) ?? rowItem.CellsFormatProvider;
      return this.GetFormattedValueInternal(cellValue, formatProvider, format);
    }

    private string GetFormattedValueInternal(object value, IFormatProvider formatProvider, string format)
    {
      if (value == null)
        return "";
      value.ToString();
      string str;
      try
      {
        if (formatProvider == null)
          str = string.Format(format, value);
        else
          str = string.Format(formatProvider, format, new object[1]{ value });
      }
      catch (Exception ex)
      {
        str = value.ToString();
      }
      return str;
    }

    /// <summary>
    /// Begins selection update. Marks all selected cells for unselection.
    /// </summary>
    internal void BeginSelectionUpdate()
    {
      Dictionary<long, CellsArea.CellSelection>.Enumerator enumerator = this.selectedCells.GetEnumerator();
      while (enumerator.MoveNext())
        this.selectedCells[enumerator.Current.Key].IsSelected = false;
    }

    /// <summary>
    /// Ends selection update. Removes all cells which are marked as unselected.
    /// </summary>
    internal void EndSelectionUpdate()
    {
      List<long> longList = new List<long>();
      Dictionary<long, CellsArea.CellSelection>.Enumerator enumerator = this.selectedCells.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (!enumerator.Current.Value.IsSelected)
          longList.Add(enumerator.Current.Key);
      }
      foreach (long key in longList)
      {
        HierarchyItem rowItem = this.selectedCells[key].RowItem;
        HierarchyItem columnItem = this.selectedCells[key].ColumnItem;
        this.selectedCells.Remove(key);
        this.GridControl.OnCellSelectionChanged(rowItem, columnItem);
      }
      if (longList.Count <= 0)
        return;
      this.isCellsSelectionHighlightedItemsDirty = true;
    }

    internal bool HierarchyItemHasSelectedCells(HierarchyItem item)
    {
      if (this.isCellsSelectionHighlightedItemsDirty)
      {
        this.cellsSelectionHighlightedItems.Clear();
        Dictionary<long, CellsArea.CellSelection>.Enumerator enumerator = this.selectedCells.GetEnumerator();
        while (enumerator.MoveNext())
        {
          if (enumerator.Current.Value.IsSelected)
          {
            if (!this.cellsSelectionHighlightedItems.Contains(enumerator.Current.Value.ColumnItem))
              this.cellsSelectionHighlightedItems.Add(enumerator.Current.Value.ColumnItem);
            if (!this.cellsSelectionHighlightedItems.Contains(enumerator.Current.Value.RowItem))
              this.cellsSelectionHighlightedItems.Add(enumerator.Current.Value.RowItem);
          }
        }
        this.isCellsSelectionHighlightedItemsDirty = false;
      }
      return this.cellsSelectionHighlightedItems.Contains(item);
    }

    /// <summary>Clears the grid cells selection.</summary>
    public void ClearSelection()
    {
      Dictionary<long, CellsArea.CellSelection>.Enumerator enumerator = this.selectedCells.GetEnumerator();
      while (enumerator.MoveNext())
      {
        this.selectedCells[enumerator.Current.Key].IsSelected = false;
        this.GridControl.OnCellSelectionChanged(enumerator.Current.Value.RowItem, enumerator.Current.Value.ColumnItem);
      }
      this.selectedCells.Clear();
      this.isCellsSelectionHighlightedItemsDirty = true;
    }

    /// <summary>Selects a specific grid cell.</summary>
    public void SelectCell(GridCell cell)
    {
      this.SelectCell(cell.RowItem, cell.ColumnItem);
    }

    /// <summary>Selects a specific grid cell.</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    public void SelectCell(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      this.SelectCellInternal(rowItem, columnItem);
      GridCell gridCell = new GridCell(rowItem, columnItem, this);
      this.gridHost.cellKBRangeSelectionStart = gridCell;
      this.gridHost.cellKBRangeSelectionEnd = gridCell;
    }

    internal void SelectCellInternal(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (columnItem != null && this.gridHost.ColumnsHierarchy.IsGroupingColumn(columnItem))
        return;
      if (this.gridHost.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.NO_SELECT)
      {
        switch (this.gridHost.SelectionMode)
        {
          case vDataGridView.SELECTION_MODE.FULL_ROW_SELECT:
            this.gridHost.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.ROW_SELECT;
            break;
          case vDataGridView.SELECTION_MODE.FULL_COLUMN_SELECT:
            this.gridHost.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.COL_SELECT;
            break;
          case vDataGridView.SELECTION_MODE.CELL_SELECT:
            this.gridHost.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.CELLS_SELECT;
            break;
        }
      }
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      GridCell cell = new GridCell(rowItem, columnItem, this);
      this.SelectMergedCells(rowItem, columnItem, ref hash, ref cell);
      if (!this.selectedCells.ContainsKey(hash))
      {
        this.selectedCells.Add(hash, new CellsArea.CellSelection(rowItem, columnItem, true));
        this.GridControl.OnCellSelectionChanged(rowItem, columnItem);
      }
      else
        this.selectedCells[hash].IsSelected = true;
      this.isCellsSelectionHighlightedItemsDirty = true;
    }

    private void SelectMergedCells(HierarchyItem rowItem, HierarchyItem colItem, ref long hashKey, ref GridCell cell)
    {
      if (!this.allowCellMerge)
        return;
      List<RowColItemPair> rowColItemPairList = this.hashCellInSpan[(object) hashKey] == null ? this.GetCellMergeChildren(rowItem, colItem) : this.GetCellMergeChildren(((RowColItemPair) this.hashCellInSpan[(object) hashKey]).RowItem, ((RowColItemPair) this.hashCellInSpan[(object) hashKey]).ColItem);
      if (rowColItemPairList != null)
      {
        for (int index = 0; index < rowColItemPairList.Count; ++index)
        {
          hashKey = vDataGridView.ComputeHash(rowColItemPairList[index].ColItem, rowColItemPairList[index].RowItem);
          if (!this.selectedCells.ContainsKey(hashKey))
          {
            this.selectedCells.Add(hashKey, new CellsArea.CellSelection(rowColItemPairList[index].RowItem, rowColItemPairList[index].ColItem, true));
            this.GridControl.OnCellSelectionChanged(rowColItemPairList[index].RowItem, rowColItemPairList[index].ColItem);
          }
          else
            this.selectedCells[hashKey].IsSelected = true;
        }
      }
      hashKey = vDataGridView.ComputeHash(colItem, rowItem);
      if (!this.hashCellInSpan.Contains((object) hashKey))
        return;
      HierarchyItem hierarchyItem1 = ((RowColItemPair) this.hashCellInSpan[(object) hashKey]).RowItem;
      HierarchyItem hierarchyItem2 = ((RowColItemPair) this.hashCellInSpan[(object) hashKey]).ColItem;
      hashKey = vDataGridView.ComputeHash(hierarchyItem2, hierarchyItem1);
      if (!this.selectedCells.ContainsKey(hashKey))
      {
        this.selectedCells.Add(hashKey, new CellsArea.CellSelection(hierarchyItem1, hierarchyItem2, true));
        this.GridControl.OnCellSelectionChanged(hierarchyItem1, hierarchyItem2);
      }
      else
        this.selectedCells[hashKey].IsSelected = true;
    }

    /// <summary>Removes the selection for a specific grid cell</summary>
    public void UnSelectCell(GridCell cell)
    {
      this.UnSelectCell(cell.RowItem, cell.ColumnItem);
    }

    /// <summary>Removes the selection for a specific grid cell</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    public void UnSelectCell(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      GridCell cell = new GridCell(rowItem, columnItem, this);
      if (this.selectedCells.ContainsKey(hash))
      {
        this.selectedCells.Remove(hash);
        this.GridControl.OnCellSelectionChanged(rowItem, columnItem);
      }
      this.UnSelectMergedCells(rowItem, columnItem, ref hash, ref cell);
      this.isCellsSelectionHighlightedItemsDirty = true;
    }

    private void UnSelectMergedCells(HierarchyItem rowItem, HierarchyItem colItem, ref long hashKey, ref GridCell cell)
    {
      if (!this.allowCellMerge)
        return;
      List<RowColItemPair> rowColItemPairList = this.hashCellInSpan[(object) hashKey] == null ? this.GetCellMergeChildren(rowItem, colItem) : this.GetCellMergeChildren(((RowColItemPair) this.hashCellInSpan[(object) hashKey]).RowItem, ((RowColItemPair) this.hashCellInSpan[(object) hashKey]).ColItem);
      if (rowColItemPairList != null)
      {
        for (int index = 0; index < rowColItemPairList.Count; ++index)
        {
          hashKey = vDataGridView.ComputeHash(rowColItemPairList[index].ColItem, rowColItemPairList[index].RowItem);
          if (this.selectedCells.ContainsKey(hashKey))
          {
            this.selectedCells.Remove(hashKey);
            this.GridControl.OnCellSelectionChanged(rowColItemPairList[index].RowItem, rowColItemPairList[index].ColItem);
          }
        }
      }
      hashKey = vDataGridView.ComputeHash(colItem, rowItem);
      if (!this.hashCellInSpan.Contains((object) hashKey))
        return;
      GridCell gridCell = new GridCell(((RowColItemPair) this.hashCellInSpan[(object) hashKey]).RowItem, ((RowColItemPair) this.hashCellInSpan[(object) hashKey]).ColItem, this);
      hashKey = vDataGridView.ComputeHash(((RowColItemPair) this.hashCellInSpan[(object) hashKey]).ColItem, ((RowColItemPair) this.hashCellInSpan[(object) hashKey]).RowItem);
      this.selectedCells.Remove(hashKey);
      this.GridControl.OnCellSelectionChanged(((RowColItemPair) this.hashCellInSpan[(object) hashKey]).RowItem, ((RowColItemPair) this.hashCellInSpan[(object) hashKey]).ColItem);
    }

    /// <summary>Determines whether a grid cell is selected</summary>
    /// <param name="rowItem">Cell's row</param>
    /// <param name="columnItem">Cell's column</param>
    /// <returns>The method returns true when the cell is selected.</returns>
    public bool IsCellSelected(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      long hash = vDataGridView.ComputeHash(columnItem, rowItem);
      if (this.selectedCells.ContainsKey(hash))
        return this.selectedCells[hash].IsSelected;
      return false;
    }

    private struct CellCFGroup
    {
      public string groupName;
      public HierarchyItem rowItem;
      public HierarchyItem colItem;
    }

    /// <summary>Represents a conditional formatting group</summary>
    public class ConditionalFormattingGroup
    {
      private double minValue = double.MaxValue;
      private double maxValue = double.MinValue;
      private Color[] colorScaleCF = new Color[100];

      /// <summary>Constructor</summary>
      public ConditionalFormattingGroup()
      {
        this.SetConditionalFormattingColorScale(GridCellCFColorScale.RED_TO_GREEN);
      }

      internal void ResetMinMax()
      {
        this.minValue = double.MaxValue;
        this.maxValue = double.MinValue;
      }

      internal void UpdateMinMaxWithValue(double value)
      {
        if (value < this.minValue)
          this.minValue = value;
        if (value <= this.maxValue)
          return;
        this.maxValue = value;
      }

      /// <summary>
      /// Calculates the Color corresponding to a specific value
      /// </summary>
      /// <param name="value">The value for which the color has to be calculated</param>
      /// <returns>A Color corresponding to the value specified</returns>
      public Color GetCFColor(double value)
      {
        int cellCfIndex = this.CalculateCellCFIndex(value);
        if (cellCfIndex == -1)
          return Color.Empty;
        return this.colorScaleCF[cellCfIndex];
      }

      private int CalculateCellCFIndex(double value)
      {
        if (value >= this.maxValue)
          return this.colorScaleCF.Length - 1;
        if (value <= this.minValue)
          return 0;
        return (int) ((value - this.minValue) / (this.maxValue - this.minValue) * 100.0);
      }

      /// <summary>Sets the Color scale for the group</summary>
      /// <param name="colorScale">The color scale for the group</param>
      public void SetConditionalFormattingColorScale(GridCellCFColorScale colorScale)
      {
        try
        {
          int colorScale1 = (int) colorScale;
          Color[] colors = new Color[100];
          for (int index = 0; index < 100; ++index)
            colors[index] = CellsArea.defaultCFMap.GetColor(colorScale1, index);
          this.SetCustomConditionalFormattingColorScale(colors);
        }
        catch (Exception ex)
        {
        }
      }

      /// <summary>Specifies a custom conditional formatting color scale</summary>
      /// <param name="colors">You must pass an array of exactly 100 items of type Color</param>
      public void SetCustomConditionalFormattingColorScale(Color[] colors)
      {
        if (colors.Length != 100)
          throw new ArgumentOutOfRangeException("LoadColorScale: Invalid argument. colors must be an array of exactly 100 Color items");
        for (int index = 0; index < 100; ++index)
          this.colorScaleCF[index] = colors[index];
      }
    }

    internal class CellSelection
    {
      public HierarchyItem RowItem { get; set; }

      public HierarchyItem ColumnItem { get; set; }

      public bool IsSelected { get; set; }

      public CellSelection(HierarchyItem RowItem, HierarchyItem ColumnItem, bool IsSelected)
      {
        this.RowItem = RowItem;
        this.ColumnItem = ColumnItem;
        this.IsSelected = IsSelected;
      }
    }
  }
}
