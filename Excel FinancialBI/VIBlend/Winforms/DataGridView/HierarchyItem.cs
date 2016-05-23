// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.HierarchyItem
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>
  /// Represents a item in the rows or columns header area of the data grid
  /// </summary>
  public class HierarchyItem : INotifyPropertyChanged
  {
    internal int hieararchyItemWidth = HierarchyItem.MinimumWidth;
    internal int hierarchyItemHeight = HierarchyItem.MinimumHeight;
    private string text = "";
    internal int hashCode = -1;
    private int boundFieldIndex = -1;
    internal Point position = new Point(0, 0);
    private ushort collectionFlags;
    internal int widthWithChildrenCached;
    internal int heightWithChildren;
    [NonSerialized]
    private byte styleOverrideBitmask;
    private int itemIndex;
    [NonSerialized]
    private Hierarchy hierarchyHost;
    internal HierarchyItem parentItem;
    internal int itemLevel;
    internal int column;
    private HierarchyItemsCollection items;
    private HierarchyItemsCollection summaryItems;
    private object tag;

    internal static int MinimumHeight
    {
      get
      {
        return 19;
      }
    }

    internal static int MinimumWidth
    {
      get
      {
        return 15;
      }
    }

    private PropertyBagEx<HierarchyItem> PropertyBag
    {
      get
      {
        if (this.hierarchyHost == null)
          return vDataGridView.ItemsTempPropertyBag;
        return this.hierarchyHost.ItemProperties;
      }
    }

    /// <summary>Checks if the item is currently expanded</summary>
    public bool Expanded
    {
      get
      {
        return ((int) this.collectionFlags & 1) == 1;
      }
      set
      {
        this.ExpandInternal(value, true, false);
      }
    }

    /// <summary>Determines whether the items is enabled or disabled</summary>
    [Category("Behavior")]
    [Description("Gets or sets the enabled state")]
    [Browsable(true)]
    public bool Enabled
    {
      get
      {
        return ((int) this.collectionFlags & 2) == 2;
      }
      set
      {
        if (value)
          this.collectionFlags |= (ushort) 2;
        else
          this.collectionFlags &= (ushort) 65533;
        this.RaisePropertyChanged("Enabled");
      }
    }

    /// <summary>Checks if the item is filtered</summary>
    public bool Filtered
    {
      get
      {
        return ((int) this.collectionFlags & 4) == 4;
      }
      internal set
      {
        if (value)
          this.collectionFlags |= (ushort) 4;
        else
          this.collectionFlags &= (ushort) 65531;
        this.RaisePropertyChanged("Filtered");
      }
    }

    /// <summary>
    /// Gets or sets whether the grid's data can be filtered by this column
    /// </summary>
    [Category("Behavior")]
    public bool AllowFiltering
    {
      get
      {
        return ((int) this.collectionFlags & 8) == 8;
      }
      set
      {
        if (value)
          this.collectionFlags |= (ushort) 8;
        else
          this.collectionFlags &= (ushort) 65527;
        this.RaisePropertyChanged("AllowFiltering");
      }
    }

    /// <summary>Gets or sets whether the item is resizable</summary>
    [Category("Behavior")]
    public bool Resizable
    {
      get
      {
        return ((int) this.collectionFlags & 16) == 16;
      }
      set
      {
        if (value)
          this.collectionFlags |= (ushort) 16;
        else
          this.collectionFlags &= (ushort) 65519;
        this.RaisePropertyChanged("Resizable");
      }
    }

    /// <summary>Determines whether expand / collapse is enabled</summary>
    public bool ExpandCollapseEnabled
    {
      get
      {
        return ((int) this.collectionFlags & 64) == 64;
      }
      set
      {
        if (value)
          this.collectionFlags |= (ushort) 64;
        else
          this.collectionFlags &= (ushort) 65471;
        this.RaisePropertyChanged("ExpandCollapseEnabled");
      }
    }

    /// <summary>Gets or sets the text wrap mode</summary>
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets the text wrap mode")]
    public bool TextWrap
    {
      get
      {
        return ((int) this.collectionFlags & 128) == 128;
      }
      set
      {
        if (value)
          this.collectionFlags |= (ushort) 128;
        else
          this.collectionFlags &= (ushort) 65407;
        this.RaisePropertyChanged("TextWrap");
      }
    }

    /// <summary>Gets or sets whether the item is Fixed (Frozen)</summary>
    public bool Fixed
    {
      get
      {
        if (((int) this.collectionFlags & 256) == 256)
          return true;
        if (this.parentItem != null)
          return this.parentItem.Fixed;
        return false;
      }
      set
      {
        if (((int) this.collectionFlags & 256) == 256 == value)
          return;
        CellSpan itemSpanGroup = this.DataGridView.CellsArea.GetItemSpanGroup(this);
        if (itemSpanGroup.ColumnsCount > 1 && this.IsColumnsHierarchyItem || itemSpanGroup.RowsCount > 1 && !this.IsColumnsHierarchyItem)
          throw new ArgumentException("Invalid operation. The Fixed property cannot be modified for items participating in cells merge.");
        if (value)
          this.collectionFlags |= (ushort) 256;
        else
          this.collectionFlags &= (ushort) 65279;
        if (this.IsSummaryItem)
          this.hierarchyHost.SummaryItems.RePopulateRenderedItemsBasedOnFixedState();
        else
          this.hierarchyHost.Items.RePopulateRenderedItemsBasedOnFixedState();
        this.RaisePropertyChanged("Fixed");
      }
    }

    /// <summary>Determines whether the item is visible</summary>
    [Description("Determines whether the item is visible")]
    [DefaultValue(false)]
    [Category("Appearance")]
    [Browsable(true)]
    public bool Hidden
    {
      get
      {
        return ((int) this.collectionFlags & 512) == 512;
      }
      set
      {
        this.SetHidden(value, true);
        this.RaisePropertyChanged("Hidden");
      }
    }

    /// <summary>
    /// Checks if the HierarchyItem is part of the rows hierarchy
    /// </summary>
    public bool IsRowsHierarchyItem
    {
      get
      {
        return !this.Hierarchy.IsColumnsHierarchy;
      }
    }

    /// <summary>
    /// Checks if the HierarchyItem is part of the columns hierarchy
    /// </summary>
    [Description("Checks if the HierarchyItem is part of the columns hierarchy")]
    public bool IsColumnsHierarchyItem
    {
      get
      {
        return ((int) this.collectionFlags & 1024) == 1024;
      }
      internal set
      {
        if (value)
          this.collectionFlags |= (ushort) 1024;
        else
          this.collectionFlags &= (ushort) 64511;
      }
    }

    /// <summary>
    /// Checks if the HierarchyItem is a Total of other items in the same collection.
    /// </summary>
    [Description("Checks if the HierarchyItem is a Total of other items in the same collection.")]
    public bool IsTotal
    {
      get
      {
        return ((int) this.collectionFlags & 2048) == 2048;
      }
      internal set
      {
        if (value)
          this.collectionFlags |= (ushort) 2048;
        else
          this.collectionFlags &= (ushort) 63487;
      }
    }

    /// <summary>Gets or sets the sort mode for the item</summary>
    [Category("Behavior")]
    public GridItemSortMode SortMode
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("SortMode", this);
        if (propertyValue == null)
          return GridItemSortMode.NotSortable;
        return (GridItemSortMode) propertyValue;
      }
      set
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("SortMode", this);
        if (value == GridItemSortMode.NotSortable)
          this.PropertyBag.RemovePropertyValue("SortMode", this);
        else
          this.PropertyBag.SetPropertyValue("SortMode", this, (object) value);
        if (propertyValue == null || value == (GridItemSortMode) propertyValue)
          return;
        this.RaisePropertyChanged("SortMode");
      }
    }

    /// <summary>
    /// Gets the bound field which establishes a data binding relationship between the items and a data column in the data source
    /// </summary>
    public BoundField BoundField
    {
      get
      {
        return (BoundField) this.PropertyBag.GetPropertyValue("BoundField", this) ?? (BoundField) null;
      }
      internal set
      {
        this.PropertyBag.SetPropertyValue("BoundField", this, (object) value);
        this.RaisePropertyChanged("BoundField");
      }
    }

    /// <summary>Gets or sets the item's image index</summary>
    [Description("Gets or sets the item's image index")]
    [Category("Behavior")]
    [Browsable(true)]
    public int ImageIndex
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("ImageIndex", this);
        if (propertyValue == null)
          return -1;
        return (int) propertyValue;
      }
      set
      {
        if (value >= 0)
          this.PropertyBag.SetPropertyValue("ImageIndex", this, (object) value);
        else
          this.PropertyBag.SetPropertyValue("ImageIndex", this, (object) null);
        this.RaisePropertyChanged("ImageIndex");
      }
    }

    /// <summary>
    /// Gets a reference to the Hierarchy which hosts the items
    /// </summary>
    public Hierarchy Hierarchy
    {
      get
      {
        return this.HierarchyHost;
      }
      internal set
      {
        this.HierarchyHost = value;
      }
    }

    internal Hierarchy HierarchyHost
    {
      get
      {
        if (this.hierarchyHost == null && this.ParentItem != null)
          this.hierarchyHost = this.ParentItem.HierarchyHost;
        this.IsColumnsHierarchyItem = this.hierarchyHost is ColumnsHierarchy;
        return this.hierarchyHost;
      }
      set
      {
        this.SetHierarchyHost(this, value);
      }
    }

    /// <summary>Gets or sets the text alignment</summary>
    [Browsable(true)]
    [Category("Appearance")]
    [Description("Gets or sets the text alignment")]
    public ContentAlignment TextAlignment
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("TextAlignment", this);
        if (propertyValue == null)
          return ContentAlignment.MiddleLeft;
        return (ContentAlignment) propertyValue;
      }
      set
      {
        if (value != ContentAlignment.MiddleLeft)
          this.PropertyBag.SetPropertyValue("TextAlignment", this, (object) value);
        else
          this.PropertyBag.SetPropertyValue("TextAlignment", this, (object) null);
        this.RaisePropertyChanged("TextAlignment");
      }
    }

    /// <summary>
    /// Gets or sets the offset between the expand/collapse indicator and the text and image
    ///  </summary>
    [Description("Gets or sets the offset between the expand/collapse indicator and the text and image ")]
    [Category("Appearance")]
    [Browsable(true)]
    public int TextImageIndicatorOffset
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("TextImageIndicatorOffset", this);
        if (propertyValue == null)
          return 2;
        return (int) propertyValue;
      }
      set
      {
        if (value != 2)
          this.PropertyBag.SetPropertyValue("TextImageIndicatorOffset", this, (object) value);
        else
          this.PropertyBag.SetPropertyValue("TextImageIndicatorOffset", this, (object) null);
        this.RaisePropertyChanged("TextImageIndicatorOffset");
      }
    }

    /// <summary>Gets or sets the alignment of the image</summary>
    [Category("Behavior")]
    [Browsable(true)]
    [Description("Gets or sets the alignment of the image")]
    public ContentAlignment ImageAlignment
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("ImageAlignment", this);
        if (propertyValue == null)
          return ContentAlignment.MiddleLeft;
        return (ContentAlignment) propertyValue;
      }
      set
      {
        if (value != ContentAlignment.MiddleLeft)
          this.PropertyBag.SetPropertyValue("ImageAlignment", this, (object) value);
        else
          this.PropertyBag.SetPropertyValue("ImageAlignment", this, (object) null);
        this.RaisePropertyChanged("ImageAlignment");
      }
    }

    /// <summary>
    /// Gets or sets the offset between the text and the image
    ///  </summary>
    [Description("Gets or sets the offset between the text and the image")]
    [Browsable(true)]
    [Category("Appearance")]
    public int TextImageOffset
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("TextImageOffset", this);
        if (propertyValue == null)
          return 0;
        return (int) propertyValue;
      }
      set
      {
        if (value != 2)
          this.PropertyBag.SetPropertyValue("TextImageOffset", this, (object) value);
        else
          this.PropertyBag.SetPropertyValue("TextImageOffset", this, (object) null);
        this.RaisePropertyChanged("TextImageOffset");
      }
    }

    /// <summary>
    /// Gets or sets the relation between the text and the image
    ///  </summary>
    [Browsable(true)]
    [Description("Gets or sets the relation between the text and the image")]
    [Category("Appearance")]
    public TextImageRelation TextImageRelation
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("TextImageRelation", this);
        if (propertyValue == null)
          return TextImageRelation.ImageBeforeText;
        return (TextImageRelation) propertyValue;
      }
      set
      {
        if (value != TextImageRelation.ImageBeforeText)
          this.PropertyBag.SetPropertyValue("TextImageRelation", this, (object) value);
        else
          this.PropertyBag.SetPropertyValue("TextImageRelation", this, (object) null);
        this.RaisePropertyChanged("TextImageRelation");
      }
    }

    /// <summary>
    /// Returns the depth of the child items hierarchy under this item
    /// </summary>
    public int ChildItemsDepth
    {
      get
      {
        int num = 0;
        foreach (HierarchyItem hierarchyItem in this.items)
        {
          int childItemsDepth = hierarchyItem.ChildItemsDepth;
          if (childItemsDepth > num)
            num = childItemsDepth;
        }
        foreach (HierarchyItem summaryItem in this.summaryItems)
        {
          int childItemsDepth = summaryItem.ChildItemsDepth;
          if (childItemsDepth > num)
            num = childItemsDepth;
        }
        return num + 1;
      }
    }

    /// <summary>Gets the count of the child items</summary>
    [Description("Gets the children count")]
    [Browsable(false)]
    [Category("Behavior")]
    public int ItemsCount
    {
      get
      {
        return this.items.Count;
      }
    }

    internal static int DefaultRowWidth
    {
      get
      {
        return 70;
      }
    }

    internal static int DefaultColumnWidth
    {
      get
      {
        return 70;
      }
    }

    internal static int DefaultItemHeight
    {
      get
      {
        return 19;
      }
    }

    internal static int DefaultGroupRowItemHeight
    {
      get
      {
        return 30;
      }
    }

    /// <summary>
    /// Returns the width of the Item as it is rendered
    /// This width includes the child items if the item is expanded
    /// </summary>
    [Description("Returns the width of the Item as it is rendered")]
    [Category("Behavior")]
    [Browsable(false)]
    public int WidthWithChildren
    {
      get
      {
        if (this.Hierarchy.PreRenderRequired)
          this.CalculateWidthWithChildren();
        return this.widthWithChildrenCached;
      }
    }

    /// <summary>
    /// Returns the height of the Item as it is rendered.
    /// This height includes the child items if the item is expanded
    /// </summary>
    [Description("Returns the height of the Item as it is rendered")]
    [Category("Behavior")]
    [DefaultValue(20)]
    [Browsable(false)]
    public int HeightWithChildren
    {
      get
      {
        if (this.Hierarchy.PreRenderRequired)
          this.CalculateHeightWithChildren();
        return this.heightWithChildren;
      }
    }

    /// <summary>Gets or sets a custom item style for normal state.</summary>
    [Browsable(false)]
    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("Gets or sets a custom item style for normal state.")]
    public HierarchyItemStyle HierarchyItemStyleNormal
    {
      get
      {
        if (((int) this.styleOverrideBitmask & 1) != 1)
          return (HierarchyItemStyle) null;
        if (this.hierarchyHost == null)
          return (HierarchyItemStyle) null;
        return (HierarchyItemStyle) this.hierarchyHost.hashItemStyleNormal[(object) this];
      }
      set
      {
        if (this.hierarchyHost == null)
          throw new Exception("Item style cannot be changed for items that are not associated with a Hierarchy");
        this.hierarchyHost.hashItemStyleNormal[(object) this] = (object) value;
        if (value != null)
          this.styleOverrideBitmask |= (byte) 1;
        else
          this.styleOverrideBitmask ^= (byte) 1;
        this.RaisePropertyChanged("HierarchyItemStyleNormal");
      }
    }

    /// <summary>Gets or sets a custom item style for selected state.</summary>
    [DefaultValue(null)]
    [Description("Gets or sets a custom item style for selected state.")]
    [Browsable(false)]
    [Category("Behavior")]
    public HierarchyItemStyle HierarchyItemStyleSelected
    {
      get
      {
        if (((int) this.styleOverrideBitmask & 2) != 2)
          return (HierarchyItemStyle) null;
        if (this.hierarchyHost == null)
          return (HierarchyItemStyle) null;
        return (HierarchyItemStyle) this.hierarchyHost.hashItemStyleSelected[(object) this];
      }
      set
      {
        if (this.hierarchyHost == null)
          throw new Exception("Item style cannot be changed for items that are not associated with a Hierarchy");
        this.hierarchyHost.hashItemStyleSelected[(object) this] = (object) value;
        if (value != null)
          this.styleOverrideBitmask |= (byte) 2;
        else
          this.styleOverrideBitmask ^= (byte) 2;
        this.RaisePropertyChanged("HierarchyItemStyleSelected");
      }
    }

    /// <summary>Gets or sets a custom item style for disabled state.</summary>
    [Browsable(false)]
    [DefaultValue(null)]
    [Category("Behavior")]
    [Description("Gets or sets a custom item style for disabled state.")]
    public HierarchyItemStyle HierarchyItemStyleDisabled
    {
      get
      {
        if (((int) this.styleOverrideBitmask & 4) != 4)
          return (HierarchyItemStyle) null;
        if (this.hierarchyHost == null)
          return (HierarchyItemStyle) null;
        return (HierarchyItemStyle) this.hierarchyHost.hashItemStyleDisabled[(object) this];
      }
      set
      {
        if (this.hierarchyHost == null)
          throw new Exception("Item style cannot be changed for items that are not associated with a Hierarchy");
        this.hierarchyHost.hashItemStyleDisabled[(object) this] = (object) value;
        if (value != null)
          this.styleOverrideBitmask |= (byte) 4;
        else
          this.styleOverrideBitmask ^= (byte) 4;
        this.RaisePropertyChanged("HierarchyItemStyleDisabled");
      }
    }

    /// <summary>Gets or sets the Width of the item</summary>
    [Description("Gets or sets the Width of the item")]
    [Browsable(true)]
    [Category("Behavior")]
    public int Width
    {
      get
      {
        if (this.Hidden || this.Filtered || this.Hierarchy == null)
          return 0;
        int nIndex = this.column;
        if (!this.Hierarchy.IsColumnsHierarchy && ((RowsHierarchy) this.Hierarchy).CompactStyleRenderingEnabled)
          nIndex = 0;
        int minimumWidth = HierarchyItem.MinimumWidth;
        return !this.IsRowsHierarchyItem ? this.hieararchyItemWidth : this.Hierarchy.GetColumnWidth(nIndex);
      }
      set
      {
        if (value < 0)
          throw new Exception("Invalid HierarchyItem Width value. The value must greater than 0");
        if (this.Hierarchy == null)
          return;
        int columnIndex = this.column;
        if (!this.Hierarchy.IsColumnsHierarchy && ((RowsHierarchy) this.Hierarchy).CompactStyleRenderingEnabled)
          columnIndex = 0;
        if (this.hierarchyHost.ColumnsCount < columnIndex)
          this.hierarchyHost.UpdateColumnsCount();
        if (this.IsColumnsHierarchyItem)
          this.hieararchyItemWidth = value;
        else
          this.hierarchyHost.SetColumnWidth(columnIndex, value);
        this.Hierarchy.PreRenderRequired = true;
        this.RaisePropertyChanged("Width");
      }
    }

    /// <summary>Gets or sets the height of item</summary>
    [Browsable(true)]
    [Description("Gets or sets the Height of the item")]
    [Category("Behavior")]
    public int Height
    {
      get
      {
        if (!this.Hidden && !this.Filtered)
          return this.hierarchyItemHeight;
        return 0;
      }
      set
      {
        if (this.IsColumnsHierarchyItem)
          return;
        this.hierarchyItemHeight = value >= 0 ? (value <= 1000 ? value : 1000) : 0;
        if (this.hierarchyHost != null)
          this.hierarchyHost.PreRenderRequired = true;
        this.RaisePropertyChanged("Height");
      }
    }

    /// <summary>
    /// Gets the Item's horizontal offset relative to the parent hierarchy
    /// </summary>
    public int X
    {
      get
      {
        return this.position.X;
      }
      internal set
      {
        this.position.X = value;
      }
    }

    /// <summary>
    /// Gets the Item's vertical offset relative to the parent hierarchy
    /// </summary>
    public int Y
    {
      get
      {
        return this.position.Y;
      }
      internal set
      {
        this.position.Y = value;
      }
    }

    /// <summary>
    /// Gets whether the item is visible based on the current scroll position
    /// </summary>
    public bool Visible
    {
      get
      {
        if (this.Hidden)
          return false;
        Rectangle drawBounds = this.DrawBounds;
        int height = this.DataGridView.Height;
        int width = this.DataGridView.Width;
        int num1 = !this.IsColumnsHierarchyItem || !this.DataGridView.RowsHierarchy.Visible || !this.DataGridView.RowsHierarchy.Fixed ? 0 : this.DataGridView.RowsHierarchy.Width;
        int num2 = this.IsColumnsHierarchyItem || !this.DataGridView.ColumnsHierarchy.Visible || !this.DataGridView.ColumnsHierarchy.Fixed ? 0 : this.DataGridView.ColumnsHierarchy.Height;
        return drawBounds.Y >= num2 && drawBounds.Y + this.HeightWithChildren <= height && (drawBounds.X >= num1 && drawBounds.X + this.WidthWithChildren <= width);
      }
    }

    /// <summary>Determines whether the item is selected</summary>
    [Category("Behavior")]
    [Browsable(false)]
    [DefaultValue(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the selected state")]
    public bool Selected
    {
      get
      {
        return this.HierarchyHost.IsItemSelected(this);
      }
      set
      {
        if (value)
          this.HierarchyHost.SelectItem(this);
        else
          this.HierarchyHost.UnSelectItem(this);
        this.RaisePropertyChanged("Selected");
      }
    }

    /// <summary>
    /// Returns true when the item has at least on corresponding grid cell which is selected
    /// </summary>
    public bool IsItemCellSelected
    {
      get
      {
        if (this.DataGridView.CellsArea.HierarchyItemHasSelectedCells(this))
          return true;
        if (!this.IsSummaryItem)
        {
          CellSpan itemSpanGroup = this.DataGridView.CellsArea.GetItemSpanGroup(this);
          if (itemSpanGroup.ColumnItem != null && itemSpanGroup.RowItem != null && this.DataGridView.CellsArea.IsCellSelected(itemSpanGroup.RowItem, itemSpanGroup.ColumnItem))
            return true;
        }
        if (this.items.Count + this.summaryItems.Count == 0)
          return false;
        int num = 0;
        if (this.Expanded)
        {
          foreach (HierarchyItem hierarchyItem in this.items)
          {
            if (!hierarchyItem.Hidden && !hierarchyItem.Filtered)
            {
              if (!hierarchyItem.IsItemCellSelected)
                return false;
              ++num;
            }
          }
        }
        foreach (HierarchyItem summaryItem in this.summaryItems)
        {
          if (!summaryItem.Hidden && !summaryItem.Filtered)
          {
            if (!summaryItem.IsItemCellSelected)
              return false;
            ++num;
          }
        }
        return num > 0;
      }
    }

    internal bool IsSortItem
    {
      get
      {
        if (this.Hierarchy == null || this.Hierarchy.GridControl == null)
          return false;
        Hierarchy hierarchy = this.Hierarchy.IsColumnsHierarchy ? (Hierarchy) this.Hierarchy.GridControl.RowsHierarchy : (Hierarchy) this.Hierarchy.GridControl.ColumnsHierarchy;
        if (hierarchy == null)
          return false;
        return hierarchy.SortItem == this;
      }
    }

    internal bool HasFilterRule
    {
      get
      {
        foreach (HierarchyItemFilter filter in (this.IsColumnsHierarchyItem ? (Hierarchy) this.DataGridView.RowsHierarchy : (Hierarchy) this.DataGridView.ColumnsHierarchy).Filters)
        {
          if (filter.Item == this)
            return true;
        }
        return false;
      }
    }

    /// <summary>Gets the draw bounds.</summary>
    /// <value>The draw bounds.</value>
    public Rectangle DrawBounds
    {
      get
      {
        Hierarchy hierarchy = this.Hierarchy;
        int x = this.X + hierarchy.X;
        int y = this.Y + hierarchy.Y;
        if (this.Fixed)
        {
          if (this.IsColumnsHierarchyItem)
            x = this.X + (this.DataGridView.RowsHierarchy.Visible ? this.DataGridView.RowsHierarchy.Width + this.DataGridView.RowsHierarchy.X : 0);
          else
            y = this.Y + (this.DataGridView.ColumnsHierarchy.Visible ? this.DataGridView.ColumnsHierarchy.Height + this.DataGridView.ColumnsHierarchy.Y : 0);
        }
        Rectangle rectangle = new Rectangle(x, y, 0, 0);
        bool visibleSummaryItems = this.HasVisibleSummaryItems;
        if (this.IsColumnsHierarchyItem)
        {
          rectangle.Width = this.Expanded || visibleSummaryItems ? this.WidthWithChildren : this.Width;
          int num = this.Expanded || visibleSummaryItems ? this.Height : ((ColumnsHierarchy) hierarchy).Height - y + hierarchy.Y;
          rectangle.Height = num < HierarchyItem.MinimumHeight ? HierarchyItem.MinimumHeight : num;
        }
        else
        {
          int num1 = hierarchy.IsColumnsHierarchy || !((RowsHierarchy) hierarchy).CompactStyleRenderingEnabled ? this.Width : this.WidthWithChildren;
          int num2 = this.Expanded || visibleSummaryItems ? num1 : ((RowsHierarchy) hierarchy).Width - x + hierarchy.X;
          rectangle.Width = num2 < HierarchyItem.MinimumWidth ? HierarchyItem.MinimumWidth : num2;
          rectangle.Height = this.Expanded || visibleSummaryItems ? this.HeightWithChildren : this.Height;
        }
        return rectangle;
      }
    }

    internal bool HasVisibleSummaryItems
    {
      get
      {
        foreach (HierarchyItem hierarchyItem in this.summaryItems.ItemsRendered)
        {
          if (!hierarchyItem.Hidden && !hierarchyItem.Filtered)
            return true;
        }
        return false;
      }
    }

    /// <summary>Gets or sets the tooltip text of this item</summary>
    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue("")]
    [Description("Gets or sets the tooltip text of this item")]
    public string TooltipText
    {
      get
      {
        return (string) this.PropertyBag.GetPropertyValue("TooltipText", this) ?? this.Caption;
      }
      set
      {
        if (value != "")
          this.PropertyBag.SetPropertyValue("TooltipText", this, (object) value);
        else
          this.PropertyBag.SetPropertyValue("TooltipText", this, (object) null);
        this.RaisePropertyChanged("TooltipText");
      }
    }

    /// <summary>Gets or sets the text of this item</summary>
    [DefaultValue("Item")]
    [Category("Appearance")]
    [Description("Gets or sets the text of this item")]
    [Browsable(true)]
    public string Caption
    {
      get
      {
        return this.text;
      }
      set
      {
        this.text = value;
        this.RaisePropertyChanged("Caption");
      }
    }

    /// <summary>Gets or sets the cells content type for this item</summary>
    [Category("Data")]
    [Description("Gets or sets the cells content type for this item")]
    [Browsable(true)]
    public System.Type ContentType
    {
      get
      {
        return (System.Type) this.PropertyBag.GetPropertyValue("ContentType", this) ?? (System.Type) null;
      }
      set
      {
        this.PropertyBag.SetPropertyValue("ContentType", this, (object) value);
        this.RaisePropertyChanged("ContentType");
      }
    }

    /// <summary>
    /// Sets the data source property for all grid cells under this HierarchyItem (Row or Column).
    /// </summary>
    [Description("Sets the data source property for all grid cells under this HierarchyItem (Row or Column).")]
    [Browsable(true)]
    public GridCellDataSource CellsDataSource
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("CellsDataSource", this);
        if (propertyValue == null)
          return GridCellDataSource.NotSet;
        return (GridCellDataSource) propertyValue;
      }
      set
      {
        this.PropertyBag.SetPropertyValue("CellsDataSource", this, (object) value);
        this.RaisePropertyChanged("CellsDataSource");
      }
    }

    /// <summary>Gets or sets the cells in-place editor.</summary>
    [Description("Gets or sets the cells in-place editor.")]
    [Browsable(true)]
    public IEditor CellsEditor
    {
      get
      {
        return (IEditor) this.PropertyBag.GetPropertyValue("CellsEditor", this) ?? (IEditor) null;
      }
      set
      {
        this.PropertyBag.SetPropertyValue("CellsEditor", this, (object) value);
        this.RaisePropertyChanged("CellsEditor");
      }
    }

    /// <summary>
    /// Gets or sets the grid cells FormatProvider for this item.
    /// </summary>
    [Description("Gets or sets the grid cells FormatProvider for this item.")]
    [Browsable(true)]
    public IFormatProvider CellsFormatProvider
    {
      get
      {
        return (IFormatProvider) this.PropertyBag.GetPropertyValue("FormatProvider", this) ?? (IFormatProvider) null;
      }
      set
      {
        this.PropertyBag.SetPropertyValue("FormatProvider", this, (object) value);
        this.RaisePropertyChanged("FormatProvider");
      }
    }

    /// <summary>
    /// Gets or sets the grid cells Format string for this item.
    /// </summary>
    [Description("Gets or sets the grid cells Format string for this item.")]
    [Browsable(true)]
    public string CellsFormatString
    {
      get
      {
        return (string) this.PropertyBag.GetPropertyValue("FormatString", this) ?? (string) null;
      }
      set
      {
        this.PropertyBag.SetPropertyValue("FormatString", this, (object) value);
        this.RaisePropertyChanged("FormatString");
      }
    }

    /// <summary>Gets or sets the grid cells style.</summary>
    [Browsable(true)]
    [Description("Gets or sets the grid cells style.")]
    public GridCellStyle CellsStyle
    {
      get
      {
        return (GridCellStyle) this.PropertyBag.GetPropertyValue("CellsStyle", this) ?? (GridCellStyle) null;
      }
      set
      {
        this.PropertyBag.SetPropertyValue("CellsStyle", this, (object) value);
        this.RaisePropertyChanged("CellsStyle");
      }
    }

    /// <summary>Gets or sets the grid cells text wrap property.</summary>
    [Browsable(true)]
    [Description("Gets or sets the grid cells text wrap property.")]
    public bool CellsTextWrap
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("CellsTextWrap", this);
        if (propertyValue == null)
          return false;
        return (bool) propertyValue;
      }
      set
      {
        if (!value)
          this.PropertyBag.RemovePropertyValue("CellsTextWrap", this);
        else
          this.PropertyBag.SetPropertyValue("CellsTextWrap", this, (object) value);
        this.RaisePropertyChanged("CellsTextWrap");
      }
    }

    /// <summary>Gets or sets the grid cells text alignment property.</summary>
    [Description("Gets or sets the grid cells text alignment property.")]
    [Browsable(true)]
    public ContentAlignment CellsTextAlignment
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("CellsTextAlignment", this);
        if (propertyValue == null)
          return ContentAlignment.MiddleLeft;
        return (ContentAlignment) propertyValue;
      }
      set
      {
        if (value == ContentAlignment.MiddleLeft)
          this.PropertyBag.RemovePropertyValue("CellsTextAlignment", this);
        else
          this.PropertyBag.SetPropertyValue("CellsTextAlignment", this, (object) value);
        this.RaisePropertyChanged("CellsTextAlignment");
      }
    }

    /// <summary>
    /// Gets or sets the grid cells text and image relation property.
    /// </summary>
    [Browsable(true)]
    [Description("Gets or sets the grid cells text and image relation property.")]
    public TextImageRelation CellsTextImageRelation
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("TextImageRelation", this);
        if (propertyValue == null)
          return TextImageRelation.ImageBeforeText;
        return (TextImageRelation) propertyValue;
      }
      set
      {
        if (value == TextImageRelation.ImageBeforeText)
          this.PropertyBag.RemovePropertyValue("TextImageRelation", this);
        else
          this.PropertyBag.SetPropertyValue("TextImageRelation", this, (object) value);
        this.RaisePropertyChanged("TextImageRelation");
      }
    }

    /// <summary>Gets or sets the grid cells image alignment.</summary>
    [Description("Gets or sets the grid cells image alignment.")]
    [Browsable(true)]
    public ContentAlignment CellsImageAlignment
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("CellsImageAlignment", this);
        if (propertyValue == null)
          return ContentAlignment.MiddleCenter;
        return (ContentAlignment) propertyValue;
      }
      set
      {
        if (value == ContentAlignment.MiddleCenter)
          this.PropertyBag.RemovePropertyValue("CellsImageAlignment", this);
        else
          this.PropertyBag.SetPropertyValue("CellsImageAlignment", this, (object) value);
        this.RaisePropertyChanged("CellsImageAlignment");
      }
    }

    /// <summary>Gets or sets the grid cells display settings.</summary>
    [Browsable(true)]
    [Description("Gets or sets the grid cells display settings.")]
    public DisplaySettings CellsDisplaySettings
    {
      get
      {
        object propertyValue = this.PropertyBag.GetPropertyValue("CellsDisplaySettings", this);
        if (propertyValue == null)
          return DisplaySettings.TextOnly;
        return (DisplaySettings) propertyValue;
      }
      set
      {
        if (value == DisplaySettings.TextOnly)
          this.PropertyBag.RemovePropertyValue("CellsDisplaySettings", this);
        else
          this.PropertyBag.SetPropertyValue("CellsDisplaySettings", this, (object) value);
        this.RaisePropertyChanged("CellsDisplaySettings");
      }
    }

    /// <summary>
    /// Returns the number of child items and child summary items of the item
    /// </summary>
    public int TotalItemsCount
    {
      get
      {
        int num = 0;
        if (this.items.Count == 0 && this.summaryItems.Count == 0)
        {
          num = 1;
        }
        else
        {
          for (int index = 0; index < this.items.Count; ++index)
            num += this.items[index].TotalItemsCount;
          for (int index = 0; index < this.summaryItems.Count; ++index)
            num += this.summaryItems[index].TotalItemsCount;
        }
        return num;
      }
    }

    /// <summary>
    /// Returns the number child items and summary items which are currently visible
    /// </summary>
    public int VisibleItemsCount
    {
      get
      {
        if (this.Hidden || this.Filtered)
          return 0;
        int num = 0;
        if ((!this.Expanded || this.RenderedItems.Count == 0) && this.RenderedSummaryItems.Count == 0)
        {
          num = 1;
        }
        else
        {
          for (int index = 0; index < this.RenderedItems.Count; ++index)
            num += this.items[index].VisibleItemsCount;
          for (int index = 0; index < this.RenderedSummaryItems.Count; ++index)
            num += this.summaryItems[index].VisibleItemsCount;
        }
        return num;
      }
    }

    internal int NonHiddenLeafItemsCount
    {
      get
      {
        int num = 0;
        if (this.items.Count > 0)
        {
          for (int index = 0; index < this.items.Count; ++index)
          {
            if (!this.items[index].Hidden && !this.items[index].Filtered)
              num += this.items[index].NonHiddenLeafItemsCount;
          }
        }
        else
          num = 1;
        return num;
      }
    }

    internal int Column
    {
      get
      {
        return this.column;
      }
      set
      {
        this.column = value;
      }
    }

    /// <summary>Gets the depth of the item in the hierarchy</summary>
    [Description("Gets the depth of the item in the hierarchy")]
    [Browsable(false)]
    [Category("Behavior")]
    public int Depth
    {
      get
      {
        return this.itemLevel;
      }
    }

    /// <summary>Gets the parent DataGridView control.</summary>
    /// <value>The DataGridView that hosts this HierarchyItem.</value>
    public vDataGridView DataGridView
    {
      get
      {
        if (this.HierarchyHost != null)
          return this.HierarchyHost.GridControl;
        return (vDataGridView) null;
      }
    }

    /// <summary>
    /// Gets the index of corresponding row or column in the data source.
    /// </summary>
    /// <remarks>
    /// This property is valid only when the data grid is working in databound mode.
    /// If the item is one of the RowsHierarchy items you can use this propery to determine the
    /// row number in the data source. If the item is one of the ColumnsHierarchy items,
    /// you can use the property to retrieve the column index in the data source.
    /// Please note, that this propery is not applicable in Pivot bound mode because in these cases the data is aggregated.
    /// </remarks>
    public int BoundFieldIndex
    {
      get
      {
        return this.boundFieldIndex;
      }
      internal set
      {
        this.boundFieldIndex = value;
      }
    }

    /// <summary>
    /// Gets the index of the item within the items collection of the parent item or hierarchy
    /// </summary>
    public int ItemIndex
    {
      get
      {
        return this.itemIndex;
      }
    }

    private int HiddenChildItemsCount
    {
      get
      {
        int num = 0;
        foreach (HierarchyItem hierarchyItem in this.Items)
        {
          if (hierarchyItem.Hidden)
            ++num;
        }
        foreach (HierarchyItem summaryItem in this.SummaryItems)
        {
          if (summaryItem.Hidden)
            ++num;
        }
        return num;
      }
    }

    /// <summary>Gets the visible child items count.</summary>
    /// <value>The visible child items count.</value>
    public int VisibleChildItemsCount
    {
      get
      {
        int num = 0;
        foreach (HierarchyItem hierarchyItem in this.Items)
        {
          if (!hierarchyItem.Filtered && !hierarchyItem.Hidden)
            ++num;
        }
        foreach (HierarchyItem summaryItem in this.SummaryItems)
        {
          if (!summaryItem.Filtered && !summaryItem.Hidden)
            ++num;
        }
        return num;
      }
    }

    /// <summary>Gets the filtered child items.</summary>
    /// <value>The filtered child items.</value>
    public List<HierarchyItem> FilteredChildItems
    {
      get
      {
        List<HierarchyItem> hierarchyItemList = new List<HierarchyItem>();
        foreach (HierarchyItem hierarchyItem in this.Items)
        {
          if (hierarchyItem.Filtered)
            hierarchyItemList.Add(hierarchyItem);
        }
        foreach (HierarchyItem summaryItem in this.SummaryItems)
        {
          if (summaryItem.Filtered)
            hierarchyItemList.Add(summaryItem);
        }
        return hierarchyItemList;
      }
    }

    /// <summary>Gets the filtered child items count.</summary>
    /// <value>The filtered child items count.</value>
    public int FilteredChildItemsCount
    {
      get
      {
        int num = 0;
        foreach (HierarchyItem hierarchyItem in this.Items)
        {
          if (hierarchyItem.Filtered)
            ++num;
        }
        foreach (HierarchyItem summaryItem in this.SummaryItems)
        {
          if (summaryItem.Filtered)
            ++num;
        }
        return num;
      }
    }

    /// <summary>Gets or sets the data object of the item</summary>
    /// <remarks>
    /// Use this property to associate the item with a custom object
    /// </remarks>
    public object ItemData
    {
      get
      {
        return this.ItemValue;
      }
      set
      {
        this.ItemValue = value;
        this.RaisePropertyChanged("ItemValue");
      }
    }

    /// <summary>Collection representing the child items of this item</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public HierarchyItemsCollection Items
    {
      get
      {
        return this.items;
      }
    }

    /// <summary>
    /// Collection represeting the child summary items of this item
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public HierarchyItemsCollection SummaryItems
    {
      get
      {
        return this.summaryItems;
      }
    }

    internal List<HierarchyItem> RenderedItems
    {
      get
      {
        return this.items.ItemsRendered;
      }
    }

    internal List<HierarchyItem> RenderedSummaryItems
    {
      get
      {
        return this.summaryItems.ItemsRendered;
      }
    }

    /// <summary>Checks if this item is a summary item</summary>
    public bool IsSummaryItem
    {
      get
      {
        if (this.parentItem == null)
          return this.HierarchyHost.IsSummaryItem(this);
        foreach (HierarchyItem summaryItem in this.parentItem.SummaryItems)
        {
          if (summaryItem == this)
            return true;
        }
        return false;
      }
    }

    /// <summary>Gets the parent item if exist</summary>
    [Description("Gets the parent item if exists")]
    [Browsable(false)]
    [DefaultValue(null)]
    [Category("Behavior")]
    public HierarchyItem ParentItem
    {
      get
      {
        return this.parentItem;
      }
    }

    /// <summary>Returns the Item displayed above this item</summary>
    [Description("Returns the Item displayed above this item.")]
    public HierarchyItem ItemAbove
    {
      get
      {
        if (this.hierarchyHost == null)
          return (HierarchyItem) null;
        return this.hierarchyHost.HitTest(new Point(this.DrawBounds.Right - 2, this.DrawBounds.Top - 2), true);
      }
    }

    /// <summary>Returns the Item displayed below this item</summary>
    [Description("Returns the Item displayed below this item.")]
    public HierarchyItem ItemBelow
    {
      get
      {
        if (this.hierarchyHost == null)
          return (HierarchyItem) null;
        return this.hierarchyHost.HitTest(new Point(this.DrawBounds.Right - 2, this.DrawBounds.Bottom + 2), true);
      }
    }

    /// <summary>
    /// Returns the Item displayed on the left of the this item
    /// </summary>
    [Description("Returns the Item displayed on the left of the this item.")]
    public HierarchyItem ItemLeft
    {
      get
      {
        if (this.hierarchyHost == null)
          return (HierarchyItem) null;
        return this.hierarchyHost.HitTest(new Point(this.DrawBounds.Left - 2, this.DrawBounds.Bottom - 2));
      }
    }

    /// <summary>
    /// Returns the Item displayed on the right of the this item
    /// </summary>
    [Description("Returns the Item displayed on the right of the this item.")]
    public HierarchyItem ItemRight
    {
      get
      {
        if (this.hierarchyHost == null)
          return (HierarchyItem) null;
        return this.hierarchyHost.HitTest(new Point(this.DrawBounds.Right + 2, this.DrawBounds.Bottom - 2));
      }
    }

    /// <summary>
    /// Gets or sets the tag associated to the HierarchyItem instance.
    /// </summary>
    /// <value>The tag.</value>
    [Description("Gets or sets the tag associated to the HierarchyItem instance.")]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        this.tag = value;
      }
    }

    /// <summary>
    /// Gets or sets a user defined value associated with the HierarchyItem
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets a user defined value associated with the HierarchyItem.")]
    public object ItemValue
    {
      get
      {
        if (this.hierarchyHost == null)
          return (object) null;
        if (this.hierarchyHost.ItemValues.ContainsKey((object) this))
          return this.hierarchyHost.ItemValues[(object) this];
        return (object) null;
      }
      set
      {
        if (this.hierarchyHost == null)
          return;
        this.hierarchyHost.ItemValues[(object) this] = value;
        this.RaisePropertyChanged("ItemValue");
      }
    }

    /// <summary>Gets all grid cells for this hierarchy item</summary>
    [Description("Gets all grid cells for this hierarchy item.")]
    public GridCell[] Cells
    {
      get
      {
        List<GridCell> cellsList = new List<GridCell>();
        if (this.hierarchyHost != null && this.hierarchyHost.GridControl != null)
          this.hierarchyHost.GridControl.CellsArea.GetCells(ref cellsList, this, false);
        return cellsList.ToArray();
      }
    }

    /// <summary>Gets all non-empty grid cells for this hierarchy item</summary>
    [Description("Gets all non-empty grid cells for this hierarchy item.")]
    public GridCell[] NonEmptyCells
    {
      get
      {
        List<GridCell> cellsList = new List<GridCell>();
        if (this.hierarchyHost != null && this.hierarchyHost.GridControl != null)
          this.hierarchyHost.GridControl.CellsArea.GetCells(ref cellsList, this, true);
        return cellsList.ToArray();
      }
    }

    /// <summary>Occurs when a HierarchyItem property changed.</summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>Default constructor</summary>
    public HierarchyItem()
    {
      this.items = new HierarchyItemsCollection(this, this.HierarchyHost);
      this.summaryItems = new HierarchyItemsCollection(this, this.HierarchyHost);
      this.hierarchyItemHeight = 21;
      this.boundFieldIndex = -1;
      this.InitPropertyFlags();
    }

    internal HierarchyItem(bool isColumnItem, Hierarchy Parent, HierarchyItem ParentItem, int Index, string ItemText)
    {
      this.hierarchyItemHeight = 21;
      this.IsColumnsHierarchyItem = isColumnItem;
      this.hierarchyHost = Parent;
      this.parentItem = ParentItem;
      this.text = ItemText;
      this.itemLevel = 0;
      this.column = 0;
      this.itemIndex = Index;
      this.items = new HierarchyItemsCollection(this, this.hierarchyHost);
      this.summaryItems = new HierarchyItemsCollection(this, this.hierarchyHost);
      this.InitPropertyFlags();
    }

    /// <exclude />
    ~HierarchyItem()
    {
      if (this.items != null)
        this.items.Clear();
      if (this.summaryItems != null)
        this.summaryItems.Clear();
      if (this.PropertyBag == null)
        return;
      this.RemoveItemBagProperties(this.PropertyBag);
    }

    private void CopyItemBagProperties(PropertyBagEx<HierarchyItem> source, PropertyBagEx<HierarchyItem> destination)
    {
      foreach (string propertyTable in this.PropertyBag.PropertyTables)
      {
        object propertyValue = source.GetPropertyValue(propertyTable, this);
        destination.SetPropertyValue(propertyTable, this, propertyValue);
      }
    }

    private void RemoveItemBagProperties(PropertyBagEx<HierarchyItem> propertyBag)
    {
      try
      {
        int length = this.PropertyBag.PropertyTables.Length;
        for (int index = 0; index < length; ++index)
        {
          string name = this.PropertyBag.PropertyTables[index];
          propertyBag.RemovePropertyValue(name, this);
        }
      }
      catch (Exception ex)
      {
      }
    }

    private void InitPropertyFlags()
    {
      this.Expanded = false;
      this.Enabled = true;
      this.Filtered = false;
      this.AllowFiltering = false;
      this.Resizable = true;
      this.ExpandCollapseEnabled = true;
      this.TextWrap = false;
      this.collectionFlags &= (ushort) 65023;
      this.Fixed = false;
    }

    internal Image GetItemImageInternal()
    {
      Image image = (Image) null;
      if (this.DataGridView != null && this.DataGridView.ImageList != null && (this.ImageIndex >= 0 && this.ImageIndex < this.DataGridView.ImageList.Images.Count))
        image = this.DataGridView.ImageList.Images[this.ImageIndex];
      return image;
    }

    private void SetHierarchyHost(HierarchyItem item, Hierarchy host)
    {
      Hierarchy hierarchy = item.hierarchyHost;
      item.hierarchyHost = host;
      item.Items.parentHierarchy = host;
      item.summaryItems.parentHierarchy = host;
      if (hierarchy != host)
      {
        if (hierarchy != null)
        {
          this.CopyItemBagProperties(hierarchy.ItemProperties, host.ItemProperties);
          this.RemoveItemBagProperties(hierarchy.ItemProperties);
        }
        else
        {
          this.CopyItemBagProperties(vDataGridView.ItemsTempPropertyBag, host.ItemProperties);
          this.RemoveItemBagProperties(vDataGridView.ItemsTempPropertyBag);
        }
      }
      this.IsColumnsHierarchyItem = this.hierarchyHost is ColumnsHierarchy;
      foreach (HierarchyItem hierarchyItem in item.Items)
        this.SetHierarchyHost(hierarchyItem, host);
      foreach (HierarchyItem summaryItem in item.SummaryItems)
        this.SetHierarchyHost(summaryItem, host);
      if (host == null)
        return;
      host.PreRenderRequired = true;
    }

    internal void CalculateWidthWithChildren()
    {
      if (this.Hidden)
        this.widthWithChildrenCached = 0;
      else if (!this.IsColumnsHierarchyItem && ((RowsHierarchy) this.Hierarchy).CompactStyleRenderingEnabled)
      {
        this.widthWithChildrenCached = this.Hierarchy.ColumnWidths[0];
      }
      else
      {
        if (this.Items != null && this.SummaryItems != null)
        {
          List<HierarchyItem> itemsRendered1 = this.Items.ItemsRendered;
          List<HierarchyItem> itemsRendered2 = this.SummaryItems.ItemsRendered;
          if (this.Expanded && itemsRendered1.Count > 0 || (itemsRendered2.Count > 0 || !this.IsColumnsHierarchyItem))
          {
            int num = 0;
            if (!this.IsColumnsHierarchyItem)
            {
              int visibleLevelDepth = this.Hierarchy.MaxVisibleLevelDepth;
              for (int index = 0; index <= visibleLevelDepth; ++index)
                num += this.Hierarchy.ColumnWidths[index];
            }
            else
            {
              num = 0;
              if (this.Expanded)
              {
                for (int index = 0; index < itemsRendered1.Count; ++index)
                {
                  if (!itemsRendered1[index].Hidden)
                    num += this.items[index].WidthWithChildren;
                }
              }
              for (int index = 0; index < itemsRendered2.Count; ++index)
              {
                if (!itemsRendered2[index].Hidden)
                  num += itemsRendered2[index].WidthWithChildren;
              }
            }
            this.widthWithChildrenCached = num;
            return;
          }
        }
        this.widthWithChildrenCached = this.Width;
      }
    }

    internal void CalculateHeightWithChildren()
    {
      if (this.Hidden || this.Filtered)
      {
        this.heightWithChildren = 0;
      }
      else
      {
        bool flag = !this.IsColumnsHierarchyItem && ((RowsHierarchy) this.Hierarchy).CompactStyleRenderingEnabled;
        if (this.Expanded || this.HasVisibleSummaryItems)
        {
          int num1;
          if (this.IsColumnsHierarchyItem)
          {
            int num2 = this.hierarchyItemHeight;
            int num3 = 0;
            if (this.Expanded)
            {
              for (int index = 0; index < this.RenderedItems.Count; ++index)
              {
                int heightWithChildren = this.RenderedItems[index].HeightWithChildren;
                if (num3 < heightWithChildren)
                  num3 = heightWithChildren;
              }
            }
            for (int index = 0; index < this.RenderedSummaryItems.Count; ++index)
            {
              int heightWithChildren = this.RenderedSummaryItems[index].HeightWithChildren;
              if (num3 < heightWithChildren)
                num3 = heightWithChildren;
            }
            num1 = num2 + (num3 - 1);
          }
          else
          {
            num1 = flag ? this.Height : 0;
            if (this.Expanded)
            {
              for (int index = 0; index < this.RenderedItems.Count; ++index)
              {
                if (!this.RenderedItems[index].Hidden && !this.RenderedItems[index].Filtered)
                  num1 += this.RenderedItems[index].HeightWithChildren;
              }
            }
            for (int index = 0; index < this.RenderedSummaryItems.Count; ++index)
            {
              if (!this.RenderedSummaryItems[index].Hidden && !this.RenderedSummaryItems[index].Filtered)
                num1 += this.RenderedSummaryItems[index].HeightWithChildren;
            }
          }
          this.heightWithChildren = num1;
        }
        else
          this.heightWithChildren = this.hierarchyItemHeight;
      }
    }

    internal Bitmap GetItemImageForDrag()
    {
      int x1 = this.X;
      int x2 = this.HierarchyHost.X;
      int y1 = this.Y;
      int y2 = this.HierarchyHost.Y;
      Rectangle rectangle = new Rectangle(this.X, this.Y, 0, 0);
      using (Graphics graphics = this.DataGridView.CreateGraphics())
        rectangle.Size = this.CalculateOwnSize(graphics);
      bool flag = this.HierarchyHost.IsItemSelected(this);
      if (!flag)
      {
        flag = this.items.Count > 0;
        foreach (HierarchyItem hierarchyItem in this.items)
        {
          if (!this.HierarchyHost.IsItemSelected(hierarchyItem))
          {
            flag = false;
            break;
          }
        }
      }
      Rectangle rect1 = new Rectangle(1, 1, rectangle.Width - 2, rectangle.Height - 2);
      ControlState controlState = ControlState.Normal;
      if (flag)
        controlState = ControlState.Pressed;
      HierarchyItemStyle hierarchyItemStyle = this.DataGridView.Theme.HierarchyItemStyleNormal;
      if (!this.Enabled)
        hierarchyItemStyle = this.DataGridView.Theme.HierarchyItemStyleDisabled;
      if (this.HierarchyItemStyleSelected != null && this.HierarchyItemStyleDisabled != null && this.HierarchyItemStyleNormal != null)
      {
        hierarchyItemStyle = this.HierarchyItemStyleNormal;
        if (!this.Enabled)
          hierarchyItemStyle = this.HierarchyItemStyleDisabled;
      }
      Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
      using (Graphics g = Graphics.FromImage((Image) bitmap))
      {
        g.FillRectangle(controlState == ControlState.Hover ? hierarchyItemStyle.FillStyleHighlight.GetBrush(rect1) : hierarchyItemStyle.FillStyle.GetBrush(rect1), rect1);
        Pen pen = new Pen(controlState == ControlState.Hover ? hierarchyItemStyle.BorderColorHighlighted : hierarchyItemStyle.BorderColor, 1f);
        g.DrawRectangle(pen, rect1);
        Image itemImageInternal = this.GetItemImageInternal();
        if (itemImageInternal != null)
        {
          Rectangle rect2 = new Rectangle(this.TextImageIndicatorOffset, 0, rectangle.Width - 16 - this.TextImageIndicatorOffset, rectangle.Height);
          this.DataGridView.CellsArea.PaintHelper.DrawImageAndTextRectangle(g, this.TextWrap, this.TextImageOffset, rect2, true, hierarchyItemStyle.Font, new SolidBrush(Color.Black), this.ImageAlignment, this.TextAlignment, this.Caption != "" ? this.Caption : "DragItem", itemImageInternal, this.TextImageRelation);
        }
        else
          this.DataGridView.CellsArea.PaintHelper.DrawText(g, new Rectangle(0, 0, rectangle.Width, rectangle.Height), this.TextWrap, Color.Black, this.Caption != "" ? this.Caption : "DragItem", hierarchyItemStyle.Font, this.TextAlignment);
      }
      PaintHelper.SetOpacityToImage(bitmap, (byte) 64);
      return bitmap;
    }

    internal bool Draw(Graphics g)
    {
      if (this.Hidden || this.Filtered || (this.Width == 0 || this.Height == 0))
        return true;
      Hierarchy hierarchy = this.Hierarchy;
      vDataGridView dataGridView = this.DataGridView;
      Rectangle rectangle = this.DrawBounds;
      int num1 = this.HasVisibleSummaryItems ? 1 : 0;
      if (rectangle.Y > hierarchy.GetGridHeight() || rectangle.X > hierarchy.GetGridWidth() || (rectangle.X + this.WidthWithChildren < 0 || rectangle.Y + this.HeightWithChildren < 0) || (rectangle.Width <= 0 || rectangle.Height <= 0))
        return true;
      if (rectangle.Right == this.DataGridView.Bounds.Width && this.DataGridView.vScroll != null && !this.DataGridView.vScroll.Visible)
        rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height);
      int num2 = 0;
      if (!this.IsColumnsHierarchyItem && ((RowsHierarchy) this.Hierarchy).CompactStyleRenderingEnabled)
      {
        rectangle.Height = this.Height + 1;
        if (this.parentItem != null)
          num2 = ((RowsHierarchy) hierarchy).CompactStyleRenderingItemsIndent * this.itemLevel;
      }
      ControlState controlState = ControlState.Normal;
      bool flag1 = dataGridView.AllowHeaderItemHighlightOnCellSelection && this.IsItemCellSelected;
      bool bSelected = hierarchy.IsItemSelected(this) || flag1;
      if (bSelected)
        controlState = ControlState.Pressed;
      HierarchyItemStyle hierarchyItemStyle;
      if (this.IsSummaryItem)
      {
        hierarchyItemStyle = bSelected ? dataGridView.Theme.HierarchyAttributeItemStyleSelected : dataGridView.Theme.HierarchyAttributeItemStyleNormal;
        if (!this.Enabled)
          hierarchyItemStyle = dataGridView.Theme.HierarchyAttributeItemStyleDisabled;
      }
      else
      {
        hierarchyItemStyle = bSelected ? dataGridView.Theme.HierarchyItemStyleSelected : dataGridView.Theme.HierarchyItemStyleNormal;
        if (!this.Enabled)
          hierarchyItemStyle = dataGridView.Theme.HierarchyItemStyleDisabled;
      }
      if (this.HierarchyItemStyleSelected != null && bSelected)
        hierarchyItemStyle = this.HierarchyItemStyleSelected;
      if (this.HierarchyItemStyleDisabled != null && !this.Enabled)
        hierarchyItemStyle = this.HierarchyItemStyleDisabled;
      if (this.HierarchyItemStyleNormal != null && !bSelected)
        hierarchyItemStyle = this.HierarchyItemStyleNormal;
      if (this == dataGridView.FocusedItem)
      {
        controlState = ControlState.Hover;
        dataGridView.lastFocusedItem = this;
      }
      Brush brush = (controlState == ControlState.Hover ? hierarchyItemStyle.FillStyleHighlight : hierarchyItemStyle.FillStyle).GetBrush(rectangle);
      g.FillRectangle(brush, rectangle);
      Pen pen = new Pen(controlState == ControlState.Hover ? hierarchyItemStyle.BorderColorHighlighted : hierarchyItemStyle.BorderColor, 1f);
      g.DrawRectangle(pen, rectangle);
      int num3 = 0;
      for (int index = 0; index < this.RenderedItems.Count; ++index)
      {
        if (this.RenderedItems[index].Hidden || this.RenderedItems[index].Filtered)
          ++num3;
      }
      int num4 = 0;
      int x;
      int y;
      if (this.RenderedItems.Count > 0 && this.RenderedItems.Count > num3 && (this.ExpandCollapseEnabled && this.HierarchyHost.ShowExpandCollapseButtons))
      {
        this.DrawExpandCollapseIndicator(g, pen, bSelected, rectangle.Left + num2, rectangle.Top, 8, 8);
        x = rectangle.X + 14 + num2;
        y = rectangle.Y + 2;
        num4 += 15;
      }
      else
      {
        x = rectangle.Left + 3 + num2;
        y = rectangle.Top + 1;
      }
      bool hasFilterRule = this.HasFilterRule;
      if (hasFilterRule)
      {
        num4 += 17;
        if (rectangle.Right - 17 > x)
          this.DrawFilterIndicator(g, pen, rectangle.Right - 17, rectangle.Top + 6);
      }
      if (this.IsSortItem)
      {
        num4 += 17;
        int left = rectangle.Right - (hasFilterRule ? 34 : 17);
        if (left > x)
        {
          SortingDirection sortingDirection = this.IsColumnsHierarchyItem ? dataGridView.RowsHierarchy.SortingDirection : dataGridView.ColumnsHierarchy.SortingDirection;
          this.DrawSortingIndicator(g, pen, sortingDirection, left, rectangle.Top + 7);
        }
      }
      pen.Dispose();
      g.Clip = new Region(rectangle);
      bool flag2 = dataGridView.OnHierarchyItemCustomPaint(this, rectangle, g);
      Color color = controlState == ControlState.Hover ? hierarchyItemStyle.TextColorHighlighed : hierarchyItemStyle.TextColor;
      if (!flag2)
      {
        Font font = hierarchyItemStyle.Font;
        Image itemImageInternal = this.GetItemImageInternal();
        if (itemImageInternal != null)
        {
          Rectangle rect = new Rectangle(x + this.TextImageIndicatorOffset, y, rectangle.Width - num4 - this.TextImageIndicatorOffset, rectangle.Height);
          this.DataGridView.CellsArea.PaintHelper.DrawImageAndTextRectangle(g, this.TextWrap, this.TextImageOffset, rect, true, font, new SolidBrush(color), this.ImageAlignment, this.TextAlignment, this.Caption, itemImageInternal, this.TextImageRelation);
        }
        else
          this.DataGridView.CellsArea.PaintHelper.DrawText(g, new Rectangle(x, y, rectangle.Width - num4, rectangle.Height), this.TextWrap, color, this.Caption, font, this.TextAlignment);
      }
      g.Clip = new Region(dataGridView.ClientRectangle);
      if (this.Expanded)
      {
        foreach (HierarchyItem renderedItem in this.RenderedItems)
        {
          if (!renderedItem.Hidden && !renderedItem.Filtered)
          {
            if (!this.IsColumnsHierarchyItem)
            {
              if (!renderedItem.Draw(g))
                break;
            }
            else if (!renderedItem.Draw(g))
              break;
          }
        }
      }
      foreach (HierarchyItem renderedSummaryItem in this.RenderedSummaryItems)
      {
        if (!renderedSummaryItem.Hidden && !renderedSummaryItem.Filtered)
        {
          if (!this.IsColumnsHierarchyItem)
          {
            if (!renderedSummaryItem.Draw(g))
              break;
          }
          else if (!renderedSummaryItem.Draw(g))
            break;
        }
      }
      return true;
    }

    private void DrawExpandCollapseIndicator(Graphics g, Pen pen, bool bSelected, int left, int top, int width, int height)
    {
      g.DrawLine(pen, left + 5, top + 10, left + 9, top + 10);
      if (!this.Expanded)
        g.DrawLine(pen, left + 7, top + 8, left + 7, top + 6 + height - 2);
      g.DrawRectangle(pen, left + 3, top + 6, width, height);
    }

    private void DrawSortingIndicator(Graphics g, Pen pen, SortingDirection sortingDirection, int left, int top)
    {
      if (sortingDirection == SortingDirection.Descending)
      {
        g.DrawLine(pen, left, top, left + 10, top);
        g.DrawLine(pen, left, top, left + 5, top + 5);
        g.DrawLine(pen, left + 5, top + 5, left + 10, top);
      }
      else
      {
        g.DrawLine(pen, left, top + 5, left + 10, top + 5);
        g.DrawLine(pen, left + 5, top, left + 10, top + 5);
        g.DrawLine(pen, left + 5, top, left, top + 5);
      }
    }

    private void DrawFilterIndicator(Graphics g, Pen pen, int left, int top)
    {
      g.DrawLine(pen, left, top, left + 8, top);
      g.DrawLine(pen, left, top, left + 3, top + 3);
      g.DrawLine(pen, left + 8, top, left + 5, top + 3);
      g.DrawLine(pen, left + 3, top + 3, left + 3, top + 7);
      g.DrawLine(pen, left + 5, top + 3, left + 5, top + 7);
      g.DrawLine(pen, left + 3, top + 7, left + 5, top + 7);
    }

    /// <summary>Expands the item</summary>
    public void Expand()
    {
      this.ExpandInternal(true, false, false);
      this.hierarchyHost.UpdateVisibleLeaves();
      this.hierarchyHost.PreRenderRequired = true;
    }

    /// <summary>Collapses the item</summary>
    public void Collapse()
    {
      this.ExpandInternal(false, false, false);
      this.hierarchyHost.UpdateVisibleLeaves();
      this.hierarchyHost.PreRenderRequired = true;
    }

    internal void ExpandInternal(bool fExpand, bool fPerformHierarchyRefresh, bool fExpandAll)
    {
      if (!this.ExpandCollapseEnabled || this.Expanded == fExpand || this.items.Count == 0 && this.summaryItems.Count == 0)
        return;
      if (fExpandAll)
      {
        for (int index = 0; index < this.items.Count; ++index)
          this.items[index].ExpandInternal(fExpand, fPerformHierarchyRefresh, fExpandAll);
        for (int index = 0; index < this.summaryItems.Count; ++index)
          this.summaryItems[index].ExpandInternal(fExpand, fPerformHierarchyRefresh, fExpandAll);
      }
      if (fPerformHierarchyRefresh)
        this.HierarchyHost.UpdateVisibleLeaves();
      this.HierarchyHost.PreRenderRequired = true;
      if (fExpand)
        this.collectionFlags |= (ushort) 1;
      else
        this.collectionFlags &= (ushort) 65534;
      this.RaisePropertyChanged("Expanded");
    }

    /// <summary>
    /// Performs a HitTest at a specific point within the HierarchyItem
    /// </summary>
    /// <param name="point">Point representing the X, Y coordinate to test. The point must be relative to the top-left corner of the grid control</param>
    /// <returns>The method returns the HierarchyItem located at the position pointed by the point parameter. If there is no HierarchyItem at this position the method returns NULL.</returns>
    public HierarchyItem HitTest(Point point)
    {
      return this.HitTest(point, true);
    }

    internal HierarchyItem HitTest(Point pt, bool testChildItems)
    {
      if (this.Hidden || this.Filtered)
        return (HierarchyItem) null;
      Rectangle rectangle = new Rectangle(this.X, this.Y, this.IsColumnsHierarchyItem ? this.WidthWithChildren : this.Width, this.IsColumnsHierarchyItem ? this.Height : this.HeightWithChildren);
      if (!this.Expanded && this.RenderedSummaryItems.Count == 0)
      {
        if (this.IsColumnsHierarchyItem)
          rectangle.Height = this.DataGridView.ColumnsHierarchy.Height - this.Y;
        else
          rectangle.Width = this.DataGridView.RowsHierarchy.Width - this.X;
      }
      rectangle.Inflate(1, 1);
      if (rectangle.Contains(pt))
        return this;
      if (!this.Expanded && this.RenderedSummaryItems.Count == 0)
        return (HierarchyItem) null;
      if (!testChildItems && this.RenderedSummaryItems.Count == 0)
        return (HierarchyItem) null;
      for (int index = 0; index < this.RenderedItems.Count; ++index)
      {
        HierarchyItem hierarchyItem = this.RenderedItems[index].HitTest(pt, true);
        if (hierarchyItem != null)
          return hierarchyItem;
      }
      for (int index = 0; index < this.RenderedSummaryItems.Count; ++index)
      {
        HierarchyItem hierarchyItem = this.RenderedSummaryItems[index].HitTest(pt, true);
        if (hierarchyItem != null)
          return hierarchyItem;
      }
      return (HierarchyItem) null;
    }

    /// <exclude />
    public void Delete()
    {
      if (this.ParentItem == null)
      {
        if (this.IsSummaryItem)
          this.HierarchyHost.SummaryItems.RemoveAt(this.ItemIndex);
        else
          this.HierarchyHost.Items.RemoveAt(this.ItemIndex);
      }
      else if (this.IsSummaryItem)
        this.ParentItem.SummaryItems.RemoveAt(this.ItemIndex);
      else
        this.ParentItem.Items.RemoveAt(this.ItemIndex);
      this.HierarchyHost.PreRenderRequired = true;
    }

    /// <summary>Returns a string which uniquely identifies the item</summary>
    public string GetUniqueID()
    {
      HierarchyItem hierarchyItem = this;
      StringBuilder stringBuilder = new StringBuilder();
      for (; hierarchyItem != null; hierarchyItem = hierarchyItem.ParentItem)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append("$");
        stringBuilder.Append(hierarchyItem.ItemIndex.ToString());
      }
      return stringBuilder.ToString();
    }

    /// <summary>Returns a hash code for this instance.</summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode()
    {
      if (this.hashCode != -1)
        return this.hashCode;
      return base.GetHashCode();
    }

    internal void SetParentItem(HierarchyItem parent)
    {
      this.parentItem = parent;
    }

    /// <summary>Automatically resizes the HierarchyItem for best fit.</summary>
    /// <param name="autoResizeMode">AutoResize mode</param>
    public void AutoResize(AutoResizeMode autoResizeMode)
    {
      if (this.IsColumnsHierarchyItem)
        this.Width = HierarchyItem.MinimumWidth;
      if (this.IsRowsHierarchyItem)
        this.Height = HierarchyItem.MinimumHeight;
      if (autoResizeMode == AutoResizeMode.FIT_ALL || autoResizeMode == AutoResizeMode.FIT_ITEM_CONTENT)
        this.AutoResizeBestItemContent();
      if (autoResizeMode == AutoResizeMode.FIT_ALL || autoResizeMode == AutoResizeMode.FIT_CELL_CONTENT)
        this.AutoResizeBestCellFit();
      if (autoResizeMode != AutoResizeMode.FIT_ALL && autoResizeMode != AutoResizeMode.FIT_ITEM_CONTENT || (!this.IsColumnsHierarchyItem || this.parentItem == null) || this.Width >= this.parentItem.Width)
        return;
      this.Width = this.parentItem.Width;
    }

    private void AutoResizeBestItemContent()
    {
      if (this.DataGridView == null)
        return;
      if (this.hierarchyHost != null && this.hierarchyHost.PreRenderRequired)
        this.hierarchyHost.PreRender();
      if (vDataGridView.GraphicsMeasure == null)
      {
        if (this.DataGridView == null)
          return;
        vDataGridView.GraphicsMeasure = this.DataGridView.CreateGraphics();
      }
      Size size = this.CalculateSize(vDataGridView.GraphicsMeasure);
      if (this.Column >= this.hierarchyHost.ColumnsCount)
        this.Hierarchy.UpdateColumnsCount();
      if (this.Width < size.Width)
        this.Width = size.Width;
      if (this.IsColumnsHierarchyItem && this.ParentItem != null && this.ParentItem.Width > this.Width)
        this.Width = this.ParentItem.Width;
      if (this.IsRowsHierarchyItem)
      {
        if (this.Height < size.Height)
          this.Height = size.Height;
      }
      else if (this.DataGridView.ColumnsHierarchy.Height < size.Height)
        this.DataGridView.ColumnsHierarchy.SetRowHeight(this, size.Height);
      foreach (HierarchyItem hierarchyItem in this.items)
        hierarchyItem.AutoResizeBestItemContent();
      foreach (HierarchyItem summaryItem in this.summaryItems)
        summaryItem.AutoResizeBestItemContent();
      if (this.hierarchyHost == null)
        return;
      this.hierarchyHost.PreRenderRequired = true;
    }

    private void AutoResizeBestCellFit()
    {
      if (this.hierarchyHost != null && this.hierarchyHost.PreRenderRequired)
        this.hierarchyHost.PreRender();
      if (this.hierarchyHost != null && this.hierarchyHost.ColumnsCountRequresUpdate)
        this.hierarchyHost.UpdateColumnsCount();
      if (this.Items.Count == 0 && this.SummaryItems.Count == 0)
      {
        if (this.hierarchyHost != null && this.DataGridView != null && (this.DataGridView.CellsArea != null && this.DataGridView.RowsHierarchy != null) && this.DataGridView.ColumnsHierarchy != null)
        {
          Size size = new Size(1, 1);
          Hierarchy hierarchy = this.IsColumnsHierarchyItem ? (Hierarchy) this.DataGridView.RowsHierarchy : (Hierarchy) this.DataGridView.ColumnsHierarchy;
          List<HierarchyItem> pv = (List<HierarchyItem>) null;
          hierarchy.GetVisibleLeafLevelItems(ref pv);
          foreach (HierarchyItem hierarchyItem in pv)
          {
            SizeF sizeF = this.DataGridView.CellsArea.MeasureBestFitSize(hierarchyItem.IsRowsHierarchyItem ? hierarchyItem : this, hierarchyItem.IsColumnsHierarchyItem ? hierarchyItem : this);
            if ((double) size.Width < (double) sizeF.Width)
              size.Width = (int) sizeF.Width;
            if ((double) size.Height < (double) sizeF.Height)
              size.Height = (int) sizeF.Height;
          }
          if (this.Width < size.Width)
            this.Width = size.Width;
          if (this.IsRowsHierarchyItem && this.Height < size.Height)
            this.Height = size.Height;
        }
      }
      else
      {
        foreach (HierarchyItem hierarchyItem in this.items)
          hierarchyItem.AutoResizeBestCellFit();
        foreach (HierarchyItem summaryItem in this.summaryItems)
          summaryItem.AutoResizeBestCellFit();
      }
      if (this.hierarchyHost == null)
        return;
      this.hierarchyHost.PreRenderRequired = true;
    }

    internal Size CalculateSize(Graphics g)
    {
      bool flag = this.HierarchyHost.IsItemSelected(this);
      HierarchyItemStyle hierarchyItemStyle = flag ? this.DataGridView.Theme.HierarchyItemStyleSelected : this.DataGridView.Theme.HierarchyItemStyleNormal;
      if (!this.Enabled)
        hierarchyItemStyle = this.DataGridView.Theme.HierarchyItemStyleDisabled;
      if (this.HierarchyItemStyleSelected != null && this.HierarchyItemStyleDisabled != null && this.HierarchyItemStyleNormal != null)
      {
        hierarchyItemStyle = flag ? this.HierarchyItemStyleSelected : this.HierarchyItemStyleNormal;
        if (!this.Enabled)
          hierarchyItemStyle = this.HierarchyItemStyleDisabled;
      }
      Font font = hierarchyItemStyle.Font;
      SizeF sizeF = g.MeasureString(this.Caption, font);
      sizeF.Width += 10f;
      sizeF.Height += 5f;
      if (this.TextWrap)
      {
        StringFormat format = new StringFormat();
        sizeF = g.MeasureString(this.Caption, font, (int) sizeF.Width - 10, format);
        sizeF.Width += 10f;
        sizeF.Height += 5f;
      }
      Image itemImageInternal = this.GetItemImageInternal();
      if (itemImageInternal != null)
      {
        if (this.TextImageRelation == TextImageRelation.ImageBeforeText || this.TextImageRelation == TextImageRelation.TextBeforeImage)
        {
          sizeF.Width = sizeF.Width + (float) itemImageInternal.Width + (float) this.TextImageOffset + (float) this.TextImageIndicatorOffset;
          sizeF.Height = Math.Max(sizeF.Height, (float) itemImageInternal.Height);
        }
        if (this.TextImageRelation == TextImageRelation.ImageAboveText || this.TextImageRelation == TextImageRelation.TextAboveImage)
        {
          sizeF.Height = sizeF.Height + (float) itemImageInternal.Height + (float) this.TextImageOffset;
          sizeF.Width = Math.Max(sizeF.Width, (float) itemImageInternal.Width);
        }
      }
      sizeF.Width += (float) this.CalculateIndicatorsWidth();
      sizeF.Width = Math.Max((float) this.Width, sizeF.Width);
      int num = this.Items.Count <= 0 || !this.ExpandCollapseEnabled || !this.Hierarchy.ShowExpandCollapseButtons ? -8 : 0;
      if (!this.IsColumnsHierarchyItem && ((RowsHierarchy) this.Hierarchy).CompactStyleRenderingEnabled && this.itemLevel > 0)
        num = 0;
      if ((double) sizeF.Width > (double) num)
        return new Size((int) sizeF.Width + num, (int) sizeF.Height);
      return new Size((int) sizeF.Width, (int) sizeF.Height);
    }

    private int CalculateIndicatorsWidth()
    {
      int num = 0;
      if (this.Items.Count > 0)
        num += 16;
      if (this.IsSortItem)
        num += 17;
      if (this.HasFilterRule)
        num += 17;
      return num;
    }

    internal Size CalculateOwnSize(Graphics g)
    {
      bool flag = this.HierarchyHost.IsItemSelected(this);
      HierarchyItemStyle hierarchyItemStyle = flag ? this.DataGridView.Theme.HierarchyItemStyleSelected : this.DataGridView.Theme.HierarchyItemStyleNormal;
      if (!this.Enabled)
        hierarchyItemStyle = this.DataGridView.Theme.HierarchyItemStyleDisabled;
      if (this.HierarchyItemStyleSelected != null && this.HierarchyItemStyleDisabled != null && this.HierarchyItemStyleNormal != null)
      {
        hierarchyItemStyle = flag ? this.HierarchyItemStyleSelected : this.HierarchyItemStyleNormal;
        if (!this.Enabled)
          hierarchyItemStyle = this.HierarchyItemStyleDisabled;
      }
      SizeF sizeF = g.MeasureString(this.Caption, hierarchyItemStyle.Font);
      sizeF.Width += 10f;
      sizeF.Height += 5f;
      Image itemImageInternal = this.GetItemImageInternal();
      if (itemImageInternal != null)
      {
        if (this.TextImageRelation == TextImageRelation.ImageBeforeText || this.TextImageRelation == TextImageRelation.TextBeforeImage)
        {
          sizeF.Width = sizeF.Width + (float) itemImageInternal.Width + (float) this.TextImageOffset + (float) this.TextImageIndicatorOffset;
          sizeF.Height = Math.Max(sizeF.Height, (float) itemImageInternal.Height);
        }
        if (this.TextImageRelation == TextImageRelation.ImageAboveText || this.TextImageRelation == TextImageRelation.TextAboveImage)
        {
          sizeF.Height = sizeF.Height + (float) itemImageInternal.Height + (float) this.TextImageOffset;
          sizeF.Width = Math.Max(sizeF.Width, (float) itemImageInternal.Width);
        }
      }
      sizeF.Width += (float) this.CalculateIndicatorsWidth();
      return new Size((int) sizeF.Width, (int) sizeF.Height);
    }

    internal void SetHidden(bool value, bool bApplyToChildItems)
    {
      if (value)
        this.collectionFlags |= (ushort) 512;
      else
        this.collectionFlags &= (ushort) 65023;
      if (value)
      {
        if (!this.HierarchyHost.hashHiddenItems.ContainsKey((object) this))
          this.HierarchyHost.hashHiddenItems.Add((object) this, (object) 0);
      }
      else if (this.HierarchyHost.hashHiddenItems.ContainsKey((object) this))
        this.HierarchyHost.hashHiddenItems.Remove((object) this);
      foreach (HierarchyItem hierarchyItem in this.items)
        hierarchyItem.SetHidden(value, true);
      foreach (HierarchyItem summaryItem in this.summaryItems)
        summaryItem.SetHidden(value, true);
      this.HierarchyHost.PreRenderRequired = true;
      this.HierarchyHost.UpdateVisibleLeaves();
      this.DataGridView.Invalidate();
    }

    internal void SetItemIndex(int nIndex)
    {
      this.itemIndex = nIndex;
    }

    internal int GetAbsoluteIndex()
    {
      if (!this.IsColumnsHierarchyItem)
      {
        List<HierarchyItem> pv = new List<HierarchyItem>();
        this.DataGridView.RowsHierarchy.GetVisibleLeafLevelItems(ref pv);
        for (int index = 0; index < pv.Count; ++index)
        {
          if (pv[index] == this)
            return index;
        }
      }
      else
      {
        List<HierarchyItem> pv = new List<HierarchyItem>();
        this.DataGridView.ColumnsHierarchy.GetVisibleLeafLevelItems(ref pv);
        for (int index = 0; index < pv.Count; ++index)
        {
          if (pv[index] == this)
            return index;
        }
      }
      return -1;
    }

    internal bool PreRender(int x, int y, Graphics g)
    {
      if (this.Hidden || this.Filtered)
        return true;
      this.X = x;
      this.Y = y;
      bool flag1 = !this.IsColumnsHierarchyItem && ((RowsHierarchy) this.Hierarchy).CompactStyleRenderingEnabled;
      int count = this.RenderedSummaryItems.Count;
      if (!this.HierarchyHost.IsItemSelected(this))
      {
        bool flag2 = this.RenderedItems.Count > 0;
        foreach (HierarchyItem renderedItem in this.RenderedItems)
        {
          if (!this.HierarchyHost.IsItemSelected(renderedItem))
          {
            flag2 = false;
            break;
          }
        }
      }
      int num = flag1 ? this.Height : 0;
      if (this.Expanded)
      {
        foreach (HierarchyItem renderedItem in this.RenderedItems)
        {
          if (!renderedItem.Hidden && !renderedItem.Filtered)
          {
            if (!this.IsColumnsHierarchyItem)
            {
              int x1 = x + (flag1 ? 0 : this.Width);
              if (renderedItem.PreRender(x1, this.Y + num, g))
                num += renderedItem.HeightWithChildren;
              else
                break;
            }
            else if (renderedItem.PreRender(this.X + num, y + this.Height, g))
              num += renderedItem.WidthWithChildren;
            else
              break;
          }
        }
      }
      foreach (HierarchyItem renderedSummaryItem in this.RenderedSummaryItems)
      {
        if (!renderedSummaryItem.Hidden && !renderedSummaryItem.Filtered)
        {
          if (!this.IsColumnsHierarchyItem)
          {
            int x1 = x + (flag1 ? 0 : this.Width);
            if (renderedSummaryItem.PreRender(x1, this.Y + num, g))
              num += renderedSummaryItem.HeightWithChildren;
            else
              break;
          }
          else if (renderedSummaryItem.PreRender(this.X + num, y + this.Height, g))
            num += renderedSummaryItem.WidthWithChildren;
          else
            break;
        }
      }
      this.CalculateWidthWithChildren();
      this.CalculateHeightWithChildren();
      return true;
    }

    internal static HierarchyItem GetFirstVisibleLeaf(HierarchyItem item)
    {
      if (item.Expanded)
      {
        bool flag = false;
        for (int index = 0; index < item.RenderedItems.Count; ++index)
        {
          if (!item.RenderedItems[index].Hidden && !item.RenderedItems[index].Filtered)
          {
            item = item.RenderedItems[index];
            item = HierarchyItem.GetFirstVisibleLeaf(item);
            flag = true;
            break;
          }
        }
        if (flag)
          return item;
      }
      if (item.HasVisibleSummaryItems)
      {
        bool flag = false;
        for (int index = 0; index < item.RenderedSummaryItems.Count; ++index)
        {
          if (!item.RenderedSummaryItems[index].Hidden && !item.RenderedSummaryItems[index].Filtered)
          {
            item = item.RenderedSummaryItems[index];
            item = HierarchyItem.GetFirstVisibleLeaf(item);
            flag = true;
            break;
          }
        }
        if (flag)
          return item;
      }
      return item;
    }

    internal static HierarchyItem GetLastVisibleLeaf(HierarchyItem item)
    {
      bool flag = false;
      for (int index = item.RenderedSummaryItems.Count - 1; index >= 0; --index)
      {
        if (!item.RenderedSummaryItems[index].Hidden && !item.RenderedSummaryItems[index].Filtered)
        {
          item = item.RenderedSummaryItems[index];
          item = HierarchyItem.GetLastVisibleLeaf(item);
          flag = true;
          break;
        }
      }
      if (flag)
        return item;
      for (int index = item.RenderedItems.Count - 1; index >= 0 && item.Expanded; --index)
      {
        if (!item.RenderedItems[index].Hidden && !item.RenderedItems[index].Filtered)
        {
          item = item.RenderedItems[index];
          item = HierarchyItem.GetLastVisibleLeaf(item);
          break;
        }
      }
      return item;
    }

    private void RaisePropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.OnNotifyPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>Raises the PropertyChanged event</summary>
    /// <param name="e">PropertyChangedEventArgs that contains the event information.</param>
    protected virtual void OnNotifyPropertyChanged(PropertyChangedEventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, e);
    }

    /// <summary>Retrieves all child HierarchyItems at all levels</summary>
    /// <returns>List of HierarchyItems</returns>
    public List<HierarchyItem> GetChildItemsRecursive()
    {
      List<HierarchyItem> list = new List<HierarchyItem>();
      this.GetChildItems(ref list);
      return list;
    }

    internal void GetChildItems(ref List<HierarchyItem> list)
    {
      foreach (HierarchyItem hierarchyItem in this.Items)
      {
        list.Add(hierarchyItem);
        hierarchyItem.GetChildItems(ref list);
      }
      foreach (HierarchyItem summaryItem in this.SummaryItems)
      {
        list.Add(summaryItem);
        summaryItem.GetChildItems(ref list);
      }
    }
  }
}
