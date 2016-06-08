// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.vDataGridView
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Xml;
using VIBlend.Utilities;
using VIBlend.WinForms.Controls;
using VIBlend.WinForms.DataGridView.Filters;

namespace VIBlend.WinForms.DataGridView
{
  /// <summary>Represents a GridView control</summary>
  /// <remarks>
  /// vDataGridView is highly customizable data grid control that allows the user to visualize hierarchical data on rows, and on columns.
  /// vDataGridView can work in both databound, and unbound modes. The data can be displayed in standard tabular format, or transformed to a pivot table view.
  /// </remarks>
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vDataGridViewDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Displays a highly customizable data grid that allows the user to visualize hierarchical data on rows, and on columns.")]
  public class vDataGridView : Control, IScrollableControlBase, INotifyPropertyChanged
  {
    private FilterForm filteringWindow = new FilterForm();
    private System.Type filterType = typeof (string);
    private bool enableResizeToolTip = true;
    private GridTheme theme = new GridTheme();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    internal ToolTip gridToolTip = new ToolTip();
    private System.Windows.Forms.Timer timerToolTip = new System.Windows.Forms.Timer();
    private bool enableToolTips = true;
    private int toolTipShowDelay = 1500;
    private int toolTipHideDelay = 5000;
    private bool allowClipDrawing = true;
    private bool allowHeaderItemHighlightOnCellSelection = true;
    private bool allowCellMerge = true;
    private bool allowDragDropIndication = true;
    internal vHScrollBar hScroll = new vHScrollBar();
    internal vVScrollBar vScroll = new vVScrollBar();
    private bool selectionBorderEnabled = true;
    private int selectionBorderWidth = 2;
    private vDataGridView.SELECTION_MODE selectionMode = vDataGridView.SELECTION_MODE.CELL_SELECT;
    private bool multipleSelectionEnabled = true;
    internal bool isSyncScrollRequired = true;
    private Color borderColor = Color.Empty;
    private int verticalScrollBarLargeChange = 20;
    private int verticalScrollBarSmallChange = 5;
    private int horizontalScrollBarLargeChange = 20;
    private int horizontalScrollBarSmallChange = 5;
    private bool scrollBarsEnabled = true;
    private bool isHorizontalResize = true;
    private Point scrollOffset = new Point(0, 0);
    private Size scrollableAreaSize = new Size(0, 0);
    private bool allowAnimations = true;
    internal ArrayList keysList = new ArrayList();
    internal ArrayList valuesList = new ArrayList();
    internal ActiveEditor activeEditor = new ActiveEditor();
    private HierarchyItem[] adjSelectedItemsSave = new HierarchyItem[4];
    internal Hashtable headerItemFillStylesHash = new Hashtable();
    private Rectangle mostLeftItemBounds = new Rectangle(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
    private bool allowContextMenuSorting = true;
    private bool allowDefaultContextMenu = true;
    private bool allowContextMenuFiltering = true;
    private bool allowContextMenuGrouping = true;
    private bool allowContextMenuColumnChooser = true;
    private ToolStripMenuItem menuItemSortAsc = new ToolStripMenuItem("&Sort Smallest to Largest");
    private ToolStripMenuItem menuItemSortDesc = new ToolStripMenuItem("&Sort Largest to Smallest");
    private ToolStripMenuItem menuItemSortClear = new ToolStripMenuItem("&Remove Sort");
    private ToolStripMenuItem menuItemFilter = new ToolStripMenuItem("&Filter");
    private ToolStripMenuItem menuItemFilterClear = new ToolStripMenuItem("&Clear Filter");
    private ToolStripMenuItem menuItemGroupBy = new ToolStripMenuItem("&Group by this Column");
    private ToolStripMenuItem menuItemColumnChooser = new ToolStripMenuItem("&Column Chooser");
    private ContextMenuStrip menu = new ContextMenuStrip();
    private int bindingProgressSampleRate = 20000;
    private bool groupingDefaultHeaderTextVisible = true;
    private Point initialMousePosition = Point.Empty;
    private Icon[] dragIcons = new Icon[4];
    private BindingSource bindingSource = new BindingSource();
    /// <summary>
    /// Collection of Bound filter fields that filter the grid data which will be displayed in a pivot table scenario
    /// </summary>
    public BoundItemsCollection<BoundFieldFilter> BoundFieldsFilters = new BoundItemsCollection<BoundFieldFilter>();
    /// <summary>
    /// Collection of Bound fields representing data binding relationships between data columns from the data source, and grid columns or rows
    /// </summary>
    public BoundItemsCollection<BoundField> BoundFields = new BoundItemsCollection<BoundField>();
    /// <summary>
    /// Collection of Bound fields for which the grid data will be displayed on rows in a pivot table scenario
    /// </summary>
    /// <seealso cref="F:VIBlend.WinForms.DataGridView.vDataGridView.BoundPivotColumns" />
    /// <seealso cref="F:VIBlend.WinForms.DataGridView.vDataGridView.BoundPivotValues" />
    public BoundItemsCollection<BoundField> BoundPivotRows = new BoundItemsCollection<BoundField>();
    /// <summary>
    /// Collection of Bound fields for which the grid data will be displayed on columns in a pivot table scenario
    /// </summary>
    /// <seealso cref="F:VIBlend.WinForms.DataGridView.vDataGridView.BoundPivotRows" />
    /// <seealso cref="F:VIBlend.WinForms.DataGridView.vDataGridView.BoundPivotValues" />
    public BoundItemsCollection<BoundField> BoundPivotColumns = new BoundItemsCollection<BoundField>();
    /// <summary>
    /// Collection of BoundValue fields which will form the Pivot table values. In a pivot table the values can be attached either next
    /// to the rows or to the columns.
    /// </summary>
    /// <seealso cref="F:VIBlend.WinForms.DataGridView.vDataGridView.PivotValuesOnRows" />
    /// <seealso cref="F:VIBlend.WinForms.DataGridView.vDataGridView.BoundPivotRows" />
    /// <seealso cref="F:VIBlend.WinForms.DataGridView.vDataGridView.BoundPivotColumns" />
    public BoundItemsCollection<BoundValueField> BoundPivotValues = new BoundItemsCollection<BoundValueField>();
    private PivotTotalsMode pivotRowsTotalsMode = PivotTotalsMode.DISPLAY_BOTH;
    private PivotTotalsMode pivotColumnsTotalsMode = PivotTotalsMode.DISPLAY_BOTH;
    /// <summary>
    /// Collection of Bound fields (columns) that the datagrid will be using to group rows
    /// </summary>
    /// <seealso cref="F:VIBlend.WinForms.DataGridView.vDataGridView.BoundPivotColumns" />
    /// <seealso cref="F:VIBlend.WinForms.DataGridView.vDataGridView.BoundPivotRows" />
    public BoundItemsCollection<BoundField> GroupingColumns = new BoundItemsCollection<BoundField>();
    internal Dictionary<HierarchyItem, string> hashGroupingRowHeaderItems = new Dictionary<HierarchyItem, string>();
    private BoundItemsCollection<BoundField> boundFields = new BoundItemsCollection<BoundField>();
    private BoundItemsCollection<BoundField> pivotRows = new BoundItemsCollection<BoundField>();
    private BoundItemsCollection<BoundField> pivotColumns = new BoundItemsCollection<BoundField>();
    private BoundItemsCollection<BoundValueField> pivotValueFields = new BoundItemsCollection<BoundValueField>();
    private BoundItemsCollection<BoundField> groupingColumns = new BoundItemsCollection<BoundField>();
    private BoundItemsCollection<BoundFieldFilter> boundFieldFilters = new BoundItemsCollection<BoundFieldFilter>();
    private object lockDataBinding = new object();
    private bool bindingComplete = true;
    private Dictionary<HierarchyItem, List<HierarchyItem>> hashRefItems = new Dictionary<HierarchyItem, List<HierarchyItem>>();
    private string[] excludedProperties = new string[20]{ "Hierarchy", "ImageIndex", "SortMode", "ImageAlignment", "TextAlignment", "TextImageRelation", "Column", "Filtered", "Selected", "ContentType", "CellsDataSource", "CellsTextAlignment", "CellsImageAlignment", "CellsDisplaySettings", "BoundField", "IsColumnHierarchyItem", "X", "Y", "BoundFieldIndex", "CellsTextImageRelation" };
    private Dictionary<string, PropertyInfo> hierarchyItemPropertiesDictionary = new Dictionary<string, PropertyInfo>();
    internal static Graphics GraphicsMeasure = (Graphics) null;
    internal static PropertyBagEx<HierarchyItem> ItemsTempPropertyBag = new PropertyBagEx<HierarchyItem>();
    private static bool isDisabled = false;
    private static DateTime initTime = DateTime.Now;
    internal const char LeftIndexerToken = '[';
    internal const char PropertyNameSeparator = '.';
    internal const char RightIndexerToken = ']';
    private BoundField filterBoundField;
    private bool isCheckUpdate;
    private HierarchyItem colItemRangeSelectionBeg;
    private HierarchyItem rowItemRangeSelectionBeg;
    private HierarchyItem colItemRangeSelectionEnd;
    private HierarchyItem rowItemRangeSelectionEnd;
    internal GridCell cellKBRangeSelectionStart;
    internal GridCell cellKBRangeSelectionEnd;
    private ToolTip resizeToolTip;
    private bool enableColumnChooser;
    private ColumnChooser columnChooser;
    private FilterDisplayMode filterDisplayMode;
    private System.Windows.Forms.Timer timerScroll;
    private Container components;
    private Rectangle rectNow;
    internal bool useVirtualCellsByDefault;
    private vDataGridView.CURSOR_TYPE currentCursorType;
    private Point currentPosition;
    private Point selectStartPosition;
    private DashStyle selectionBorderDashStyle;
    private GridLinesDisplayMode gridLinesDisplayMode;
    private DashStyle gridLinesDashStyle;
    private Graphics graphics;
    private int offsetX;
    private int offsetY;
    private vDataGridView.COL_RESIZE_STATES colResizeState;
    internal HierarchyItem resizingItem;
    internal vDataGridView.INTERNAL_SELECT_MODE internalSelectMode;
    private RowsHierarchy rowsHierarchy;
    private ColumnsHierarchy columnsHierarchy;
    private System.Windows.Forms.Timer scrollOnSelectionTimer;
    private bool isTooltipCursorChange;
    private ImageList imageList;
    private CellsArea cellsArea;
    private DataGridLocalizationBase localization;
    internal bool suspendGroupingColumnsCollectionChanged;
    private bool isPaintSuspended;
    private bool paintSuspend;
    private int deactivateState;
    private bool showBorder;
    private Cursor crossCursor;
    internal bool isCTRLPressed;
    internal bool isShiftPressed;
    private bool allowCopyPaste;
    private int selectedRowsCount;
    private int selectedColumnsCount;
    private bool cutOperation;
    private HierarchyItem focusedItem;
    internal HierarchyItem lastFocusedItem;
    private bool scrollTimerStarted;
    private Point lastMouseMovePt;
    internal GridCell cellMouseMove;
    private GridToolTipEventArgs toolTipEventArgs;
    private Point lastScrollPosition;
    internal bool scrollOnSelection;
    private vButton dragBtn;
    private AnimationManager animationManager;
    private Rectangle mostRightItemBounds;
    /// <summary>
    /// Represents a custom grid cell comparer
    /// <remarks>
    /// Use this property to override the default sorting behaviour of the grid
    /// </remarks>
    /// </summary>
    public IComparer<GridCell> GridCellSortComparer;
    private HierarchyItem itemContextMenuHit;
    private bool isBindingProgressEnabled;
    private bool isUpdatingTotals;
    private bool shouldDrag;
    private bool canDrag;
    internal HierarchyItem dragSourceItem;
    private DragPositionInTarget dragPositionInTarget;
    private Form dragDropForm;
    private GridBindingState bindingState;
    private BackgroundWorker backgroundWorker1;
    private string dataMember;
    private CurrencyManager dataManager;
    private bool isBindingSourceSetInProgress;
    private bool pivotRowsTotalsEnabled;
    private bool pivotColumnsTotalsEnabled;
    private bool isGroupingEnabled;
    private bool autoUpdateOnListChanged;
    /// <summary>
    /// Determines whether the pivot table values are displayed next to the pivot rows or next to pivot columns
    /// </summary>
    public bool PivotValuesOnRows;
    private Hashtable hashPivotItemsToTableRows;
    private PropertyInfo[] hierarchyItemProperties;

    /// <summary>
    /// Gets or sets a value indicating the filtering window display mode.
    /// </summary>
    [Description("Gets or sets a value indicating the filtering window display mode.")]
    public FilterDisplayMode FilterDisplayMode
    {
      get
      {
        return this.filterDisplayMode;
      }
      set
      {
        this.filterDisplayMode = value;
      }
    }

    /// <summary>Gets the column chooser.</summary>
    /// <value>The column chooser.</value>
    [Browsable(false)]
    public ColumnChooser ColumnChooser
    {
      get
      {
        if (this.columnChooser == null)
        {
          this.columnChooser = new ColumnChooser();
          this.columnChooser.DataGrid = this;
          this.columnChooser.StartPosition = FormStartPosition.Manual;
        }
        return this.columnChooser;
      }
    }

    /// <summary>
    /// "Enables/disables the column chooser which allows you to easily show/hide columns.
    /// </summary>
    [Category("Behavior")]
    [Description("Enables/disables the column chooser which allows you to easily show/hide columns.")]
    public bool EnableColumnChooser
    {
      get
      {
        return this.enableColumnChooser;
      }
      set
      {
        if (value == this.enableColumnChooser)
          return;
        this.enableColumnChooser = value;
        this.OnPropertyChanged("EnableColumnChooser");
      }
    }

    /// <summary>Gets the resize tool tip.</summary>
    /// <value>The resize tool tip.</value>
    [Description("Gets the resize tool tip.")]
    [Browsable(false)]
    public ToolTip ResizeToolTip
    {
      get
      {
        return this.resizeToolTip;
      }
    }

    /// <summary>
    /// Enables/disables the tooltips which are shown when you resize a column/row.
    /// </summary>
    [Description("Enables/disables the tooltips which are shown when you resize a column/row.")]
    [Category("Behavior")]
    public bool EnableResizeToolTip
    {
      get
      {
        return this.enableResizeToolTip;
      }
      set
      {
        if (value == this.enableResizeToolTip)
          return;
        this.enableResizeToolTip = value;
        this.OnPropertyChanged("EnableResizeToolTip");
      }
    }

    internal bool IsScrolling
    {
      get
      {
        return this.timerScroll.Enabled;
      }
    }

    /// <summary>
    /// Gets or sets whether the DataGrid will automatically bind to all columns available in the data source.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether the DataGrid will automatically bind to all columns available in the data source.")]
    [DefaultValue(true)]
    public bool AutoGenerateColumns { get; set; }

    /// <summary>Gets or sets the grid theme.</summary>
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the grid theme")]
    public GridTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == this.theme)
          return;
        this.theme = value;
        this.OnPropertyChanged("Theme");
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the DataGrid's theme.</summary>
    [Description("Gets or sets the DataGrid's theme.")]
    [Category("Appearance")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        try
        {
          this.Theme = GridTheme.GetDefaultTheme(value);
          this.hScroll.VIBlendTheme = this.vScroll.VIBlendTheme = value;
          this.defaultTheme = value;
          this.Invalidate();
          this.OnPropertyChanged("VIBlendTheme");
        }
        catch (Exception)
        {
        }
      }
    }

    /// <summary>
    /// Gets or sets whether the grid cells are in virtual mode by default
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether the grid cells are in virtual mode by default")]
    public virtual bool VirtualModeCellDefault
    {
      get
      {
        return this.useVirtualCellsByDefault;
      }
      set
      {
        if (value == this.useVirtualCellsByDefault)
          return;
        this.useVirtualCellsByDefault = value;
        this.OnPropertyChanged("VirtualModeCellDefault");
      }
    }

    /// <summary>
    /// Gets or sets whether the header item is highlighted on cell selection
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether the header items are highlighted on cell selection")]
    public virtual bool AllowHeaderItemHighlightOnCellSelection
    {
      get
      {
        return this.allowHeaderItemHighlightOnCellSelection;
      }
      set
      {
        if (value == this.allowHeaderItemHighlightOnCellSelection)
          return;
        this.allowHeaderItemHighlightOnCellSelection = value;
        this.OnPropertyChanged("AllowHeaderItemHighlightOnCellSelection");
      }
    }

    /// <summary>
    /// Gets or sets whether the dragging indicator is displayed
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether the indication when dragging is allowed")]
    public virtual bool AllowDragDropIndication
    {
      get
      {
        return this.allowDragDropIndication;
      }
      set
      {
        if (value == this.allowDragDropIndication)
          return;
        this.allowDragDropIndication = value;
        this.OnPropertyChanged("AllowDragDropIndication");
      }
    }

    /// <summary>
    /// Determines whether the grid should draw a border around the selected grid cells
    /// </summary>
    [Category("Appearance")]
    [Description("Determines whether the grid should draw a border around the selected grid cells")]
    public bool SelectionBorderEnabled
    {
      get
      {
        return this.selectionBorderEnabled;
      }
      set
      {
        this.selectionBorderEnabled = value;
        this.OnPropertyChanged("SelectionBorderEnabled");
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the width in pixel of the selection border
    /// </summary>
    /// <remarks>
    /// For estetic reasons the maximum value of this property is limited to 2 pixels
    /// </remarks>
    [Category("Appearance")]
    [Description("Gets or sets the width in pixel of the selection border")]
    public int SelectionBorderWidth
    {
      get
      {
        return this.selectionBorderWidth;
      }
      set
      {
        if (value < 0)
          this.selectionBorderWidth = 0;
        this.selectionBorderWidth = value <= 2 ? value : 2;
        this.Refresh();
        this.OnPropertyChanged("SelectionBorderWidth");
      }
    }

    /// <summary>
    /// Gets or sets the line dash style of the selection border
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the line dash style of the selection border")]
    public DashStyle SelectionBorderDashStyle
    {
      get
      {
        return this.selectionBorderDashStyle;
      }
      set
      {
        this.selectionBorderDashStyle = value;
        this.Refresh();
      }
    }

    /// <summary>Determines how the grid lines will be displayed.</summary>
    [Category("Appearance")]
    [Description("Determines how the grid lines will be displayed.")]
    public GridLinesDisplayMode GridLinesDisplayMode
    {
      get
      {
        return this.gridLinesDisplayMode;
      }
      set
      {
        if (value == this.gridLinesDisplayMode)
          return;
        this.gridLinesDisplayMode = value;
        this.OnPropertyChanged("GridLinesDisplayMode");
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the grid lines dash style.</summary>
    [Description("Gets or sets the grid lines dash style.")]
    [Category("Appearance")]
    public DashStyle GridLinesDashStyle
    {
      get
      {
        return this.gridLinesDashStyle;
      }
      set
      {
        if (value == this.gridLinesDashStyle)
          return;
        this.gridLinesDashStyle = value;
        this.OnPropertyChanged("GridLinesDashStyle");
        this.Refresh();
      }
    }

    /// <summary>Gets or sets whether the cells could be merged</summary>
    [Description("Gets or sets whether the cells could be merged")]
    [Category("Behavior")]
    public virtual bool AllowCellMerge
    {
      get
      {
        return this.allowCellMerge;
      }
      set
      {
        if (value == this.allowCellMerge)
          return;
        this.allowCellMerge = value;
        this.OnPropertyChanged("AllowCellMerge");
      }
    }

    /// <summary>Gets or sets whether the clip drawing is allowed</summary>
    [Category("Appearance")]
    [Description(" Gets or sets whether the clip drawing is allowed")]
    public virtual bool AllowClipDrawing
    {
      get
      {
        return this.allowClipDrawing;
      }
      set
      {
        if (value == this.allowClipDrawing)
          return;
        this.allowClipDrawing = value;
        this.OnPropertyChanged("AllowClipDrawing");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the tooltips are enabled.
    /// </summary>
    [Description("Controls whether to display tooltips in the headers and in the cells area")]
    [Category("Behavior")]
    public virtual bool EnableToolTips
    {
      get
      {
        return this.enableToolTips;
      }
      set
      {
        this.enableToolTips = value;
        this.OnPropertyChanged("EnableToolTips");
      }
    }

    /// <summary>Gets or sets the tool tip show delay.</summary>
    /// <value>The tool tip show delay.</value>
    [Category("Behavior")]
    [Description("Time delay in milliseconds before the tooltip is shown")]
    public int ToolTipShowDelay
    {
      get
      {
        return this.toolTipShowDelay;
      }
      set
      {
        this.toolTipShowDelay = value;
        this.OnPropertyChanged("ToolTipShowDelay");
      }
    }

    /// <summary>Gets or sets the duration of the tool tip.</summary>
    /// <value>The duration of the tool tip.</value>
    [Description("Tooltip duration in milliseconds ")]
    [Category("Behavior")]
    public int ToolTipDuration
    {
      get
      {
        return this.toolTipHideDelay;
      }
      set
      {
        this.toolTipHideDelay = value;
        this.OnPropertyChanged("ToolTipDuration");
      }
    }

    internal bool InDesignMode
    {
      get
      {
        return this.DesignMode;
      }
    }

    /// <summary>
    /// Gets or sets the image list associated to the grid control
    /// </summary>
    [Description("Gets or sets the image list associated to the grid control")]
    [Category("Behavior")]
    public ImageList ImageList
    {
      get
      {
        return this.imageList;
      }
      set
      {
        this.imageList = value;
        this.OnPropertyChanged("ImageList");
      }
    }

    /// <summary>Gets the cells area</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public CellsArea CellsArea
    {
      get
      {
        return this.cellsArea;
      }
    }

    /// <summary>Gets or sets the grid cells selection mode</summary>
    [Category("Behavior")]
    public vDataGridView.SELECTION_MODE SelectionMode
    {
      get
      {
        return this.selectionMode;
      }
      set
      {
        this.selectionMode = value;
        this.ClearSelection();
        this.Refresh();
      }
    }

    /// <summary>Gets or sets whether multiple selection is enabled.</summary>
    [Description("Gets or sets whether multiple selection is enabled.")]
    [Category("Behavior")]
    public bool MultipleSelectionEnabled
    {
      get
      {
        return this.multipleSelectionEnabled;
      }
      set
      {
        this.multipleSelectionEnabled = value;
      }
    }

    /// <summary>Gets the localization.</summary>
    /// <value>The localization.</value>
    [Browsable(false)]
    public DataGridLocalizationBase Localization
    {
      get
      {
        if (this.localization == null)
          this.localization = (DataGridLocalizationBase) new DataGridLocalization();
        return this.localization;
      }
      set
      {
        if (value == this.localization)
          return;
        this.localization = value;
      }
    }

    /// <summary>Gets the Rows hierarchy</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RowsHierarchy RowsHierarchy
    {
      get
      {
        return this.rowsHierarchy;
      }
    }

    /// <summary>Gets the Columns hierarchy</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ColumnsHierarchy ColumnsHierarchy
    {
      get
      {
        return this.columnsHierarchy;
      }
    }

    internal Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
      set
      {
        this.graphics = value;
      }
    }

    internal bool PaintSuspended
    {
      get
      {
        return this.isPaintSuspended;
      }
      set
      {
        this.isPaintSuspended = value;
      }
    }

    /// <summary>Gets or sets the color of the border.</summary>
    /// <value>The color of the border.</value>
    [Description("Gets or sets the Grid's Border color.")]
    [Category("Appearance")]
    public Color BorderColor
    {
      get
      {
        return this.borderColor;
      }
      set
      {
        this.borderColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show a border.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Shows or Hides the Grid's Border.")]
    public bool ShowBorder
    {
      get
      {
        return this.showBorder;
      }
      set
      {
        this.showBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the LargeChange of the Vertical ScrollBar.
    /// </summary>
    [Category("DataGrid.Scrolling")]
    [Description("Gets or sets the LargeChange of the Vertical ScrollBar.")]
    public int VerticalScrollBarLargeChange
    {
      get
      {
        return this.verticalScrollBarLargeChange;
      }
      set
      {
        this.verticalScrollBarLargeChange = value;
        this.SynchronizeScrollBars();
      }
    }

    /// <summary>
    /// Gets or sets the SmallChange of the Vertical ScrollBar.
    /// </summary>
    [Description("Gets or sets the SmallChange of the Vertical ScrollBar.")]
    [Category("DataGrid.Scrolling")]
    public int VerticalScrollBarSmallChange
    {
      get
      {
        return this.verticalScrollBarSmallChange;
      }
      set
      {
        this.verticalScrollBarSmallChange = value;
        this.SynchronizeScrollBars();
      }
    }

    /// <summary>
    /// Gets or sets the LargeChange of the Horizontal ScrollBar.
    /// </summary>
    [Category("DataGrid.Scrolling")]
    [Description("Gets or sets the LargeChange of the Horizontal ScrollBar.")]
    public int HorizontalScrollBarLargeChange
    {
      get
      {
        return this.horizontalScrollBarLargeChange;
      }
      set
      {
        this.horizontalScrollBarLargeChange = value;
        this.SynchronizeScrollBars();
      }
    }

    /// <summary>
    /// Gets or sets the SmallChange of the Horizontal ScrollBar.
    /// </summary>
    [Category("DataGrid.Scrolling")]
    [Description("Gets or sets the SmallChange of the Horizontal ScrollBar.")]
    public int HorizontalScrollBarSmallChange
    {
      get
      {
        return this.horizontalScrollBarSmallChange;
      }
      set
      {
        this.horizontalScrollBarSmallChange = value;
        this.SynchronizeScrollBars();
      }
    }

    /// <summary>Gets or sets the horizontal scroll position</summary>
    [Description("Gets or sets the horizontal scroll position.")]
    [Category("DataGrid.Scrolling")]
    public int HorizontalScroll
    {
      get
      {
        return this.hScroll.Value;
      }
      set
      {
        this.hScroll.Value = value;
        this.isSyncScrollRequired = true;
        this.SynchronizeScrollBars();
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the vertical scroll position</summary>
    [Description("Gets or sets the vertical scroll position.")]
    [Category("DataGrid.Scrolling")]
    public int VerticalScroll
    {
      get
      {
        return this.vScroll.Value;
      }
      set
      {
        this.vScroll.Value = value;
        this.isSyncScrollRequired = true;
        this.SynchronizeScrollBars();
        this.Refresh();
      }
    }

    /// <summary>
    /// Determines whether the Horizontal and Vertical ScrollBars will be displayed
    /// </summary>
    [Category("DataGrid.Scrolling")]
    [Description("Determines whether the Horizontal and Vertical ScrollBars will be displayed.")]
    public bool ScrollBarsEnabled
    {
      get
      {
        return this.scrollBarsEnabled;
      }
      set
      {
        this.scrollBarsEnabled = value;
        this.isSyncScrollRequired = true;
      }
    }

    /// <summary>
    /// Gets or sets whether to enable copy/paste operations within the grid cells
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether to enable copy/paste operations within the grid cells")]
    public bool AllowCopyPaste
    {
      get
      {
        return this.allowCopyPaste;
      }
      set
      {
        if (value == this.allowCopyPaste)
          return;
        this.allowCopyPaste = value;
        this.OnPropertyChanged("AllowCopyPaste");
      }
    }

    internal HierarchyItem FocusedItem
    {
      get
      {
        return this.focusedItem;
      }
    }

    internal Point ScrollOffset
    {
      get
      {
        return this.scrollOffset;
      }
      set
      {
        this.scrollOffset = value;
        int num1 = -this.scrollOffset.Y;
        if (num1 < this.vScroll.Minimum)
          num1 = this.vScroll.Minimum;
        if (num1 > this.vScroll.Maximum)
          num1 = this.vScroll.Maximum;
        int num2 = -this.scrollOffset.X;
        if (num2 < this.hScroll.Minimum)
          num2 = this.hScroll.Minimum;
        if (num2 > this.hScroll.Maximum)
          num2 = this.hScroll.Maximum;
        this.scrollOffset = new Point(-num2, -num1);
        this.hScroll.Value = num2;
        this.vScroll.Value = num1;
        this.hScroll.Refresh();
        this.vScroll.Refresh();
        this.Refresh();
        this.OnAutoScrollPositionChanged(EventArgs.Empty);
      }
    }

    internal Size ScrollableAreaSize
    {
      get
      {
        return this.scrollableAreaSize;
      }
      set
      {
        this.scrollableAreaSize = value;
      }
    }

    /// <summary>
    /// Gets or sets whether to use animations when visualizing the content of the grid
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether to use animations when visualizing the content of the grid")]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        this.allowAnimations = value;
      }
    }

    /// <exclude />
    [Description("Gets an instance of Animation Manager")]
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.animationManager == null)
          this.animationManager = new AnimationManager((Control) this);
        return this.animationManager;
      }
    }

    /// <summary>
    /// Determines whether to display the sorting options in the grid's context menu
    /// </summary>
    /// <remarks>
    /// When the user clicks the right mouse button on a HierarchyItem, the grid will display a context menu with multiple actions.
    /// The actions may include filtering, sorting, expand/collapse state, and others. The list may vary depending on the flags
    /// set at HierarchyItem level itself, and the context menu flags for each action type.
    /// </remarks>
    [Category("DataGrid.ContextMenu")]
    [Description("Determines whether to display the sorting options in the grid's context menu.")]
    public bool AllowContextMenuSorting
    {
      get
      {
        return this.allowContextMenuSorting;
      }
      set
      {
        if (value == this.allowContextMenuSorting)
          return;
        this.allowContextMenuSorting = value;
        this.OnPropertyChanged("AllowContextMenuSorting");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the built-in context menu can be displayed.
    /// </summary>
    [Description("Gets or sets a value indicating whether the built-in context menu can be displayed.")]
    [Category("DataGrid.ContextMenu")]
    public bool AllowDefaultContextMenu
    {
      get
      {
        return this.allowDefaultContextMenu;
      }
      set
      {
        if (value == this.allowDefaultContextMenu)
          return;
        this.allowDefaultContextMenu = value;
        this.OnPropertyChanged("AllowDefaultContextMenu");
      }
    }

    /// <summary>
    /// Determines whether to display the sorting options in the grid's context menu
    /// </summary>
    /// <remarks>
    /// When the user clicks the right mouse button on a HierarchyItem, the grid will display a context menu with multiple actions.
    /// The actions may include filtering, sorting, expand/collapse state, and others. The list may vary depending on the flags
    /// set at HierarchyItem level itself, and the context menu flags for each action type.
    /// </remarks>
    [Category("DataGrid.ContextMenu")]
    [Description("Determines whether to display the sorting options in the grid's context menu.")]
    public bool AllowContextMenuFiltering
    {
      get
      {
        return this.allowContextMenuFiltering;
      }
      set
      {
        if (value == this.allowContextMenuFiltering)
          return;
        this.allowContextMenuFiltering = value;
        this.OnPropertyChanged("AllowContextMenuFiltering");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the 'Group by this column' menu item is enabled.
    /// </summary>
    [Description("Gets or sets a value indicating whether the 'Group by this column' menu item is enabled.")]
    [Category("DataGrid.ContextMenu")]
    public bool AllowContextMenuGrouping
    {
      get
      {
        return this.allowContextMenuGrouping;
      }
      set
      {
        if (value == this.allowContextMenuGrouping)
          return;
        this.allowContextMenuGrouping = value;
        this.OnPropertyChanged("AllowContextMenuGrouping");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the 'Column Chooser' menu item is enabled.
    /// </summary>
    [Description("Gets or sets a value indicating whether the 'Column Chooser' menu item is enabled.")]
    [Category("DataGrid.ContextMenu")]
    public bool AllowContextMenuColumnChooser
    {
      get
      {
        return this.allowContextMenuColumnChooser;
      }
      set
      {
        if (value == this.allowContextMenuColumnChooser)
          return;
        this.allowContextMenuColumnChooser = value;
        this.OnPropertyChanged("AllowContextMenuColumnChooser");
      }
    }

    /// <summary>
    /// Gets or sets the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with this control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for this control, or null if there is no <see cref="T:System.Windows.Forms.ContextMenuStrip" />. The default is null.
    /// </returns>
    public override ContextMenuStrip ContextMenuStrip
    {
      get
      {
        return base.ContextMenuStrip;
      }
      set
      {
        if (base.ContextMenuStrip != null)
          base.ContextMenuStrip.Opening -= new CancelEventHandler(this.value_Opening);
        base.ContextMenuStrip = value;
        if (value == null)
          return;
        value.Opening += new CancelEventHandler(this.value_Opening);
      }
    }

    /// <summary>
    /// Gets or sets whether the data binding progress indicator is visible
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether the data binding progress indicator is visible.")]
    [Browsable(true)]
    public bool BindingProgressEnabled
    {
      get
      {
        return this.isBindingProgressEnabled;
      }
      set
      {
        this.isBindingProgressEnabled = value;
      }
    }

    /// <summary>
    /// Gets or sets the data binding progress sample rate. The value must be between 1,000 and 100,000.
    /// </summary>
    /// <remarks>
    /// Uset this value to control how often the binding progress is refreshed. For example setting this value to 1000 means that the progress will be updated approx. every 1000 records.
    /// </remarks>
    [Browsable(true)]
    [Description("Gets or sets the data binding progress sample rate. The value must be between 1,000 and 100,000.")]
    [Category("Behavior")]
    public int BindingProgressSampleRate
    {
      get
      {
        return this.bindingProgressSampleRate;
      }
      set
      {
        if (value < 1000)
          value = 1000;
        if (value > 100000)
          value = 100000;
        this.bindingProgressSampleRate = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the rows group name and rows count is visible in the rows grouping headers.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether the rows group name and rows count is visible in the rows grouping headers.")]
    [DefaultValue(true)]
    public bool GroupingDefaultHeaderTextVisible
    {
      get
      {
        return this.groupingDefaultHeaderTextVisible;
      }
      set
      {
        this.groupingDefaultHeaderTextVisible = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets the current data binding state of the grid control
    /// </summary>
    public GridBindingState BindingState
    {
      get
      {
        return this.bindingState;
      }
    }

    /// <summary>Gets or sets the DataSource of the grid control</summary>
    [TypeConverter("System.Windows.Forms.Design.DataSourceConverter, System.Design")]
    [AttributeProvider(typeof (IListSource))]
    [Category("Data")]
    [Description("DataSource for the Grid control")]
    [DefaultValue(null)]
    public object DataSource
    {
      get
      {
        return this.bindingSource.DataSource;
      }
      set
      {
        if (this.bindingSource.DataSource == value)
          return;
        this.isBindingSourceSetInProgress = true;
        if (value == null)
        {
          this.bindingSource.RemoveSort();
          this.bindingSource.DataSource = (object) null;
          this.isBindingSourceSetInProgress = false;
        }
        else
        {
          this.bindingSource.DataSource = value;
          this.DataBind();
          this.isBindingSourceSetInProgress = false;
        }
      }
    }

    /// <summary>Gets or sets the DataMember for the grid DataSource</summary>
    [Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", "System.Drawing.Design.UITypeEditor, System.Drawing")]
    [Category("Data")]
    [DefaultValue(null)]
    [Description("DataMember for the GridControl's DataSource")]
    public string DataMember
    {
      get
      {
        return this.dataMember;
      }
      set
      {
        if (!(this.dataMember != value))
          return;
        this.dataMember = value;
        this.DataBind();
      }
    }

    protected CurrencyManager DataManager
    {
      get
      {
        return this.dataManager;
      }
    }

    /// <summary>
    /// Gets or sets if the Pivot Table Rows Totals are enabled.
    /// </summary>
    public bool PivotRowsTotalsEnabled
    {
      get
      {
        return this.pivotRowsTotalsEnabled;
      }
      set
      {
        if (value != this.pivotRowsTotalsEnabled)
        {
          this.pivotRowsTotalsEnabled = value;
          this.OnPropertyChanged("PivotRowsTotalsEnabled");
        }
        this.UpdateTotals();
      }
    }

    /// <summary>
    /// Gets or sets the Pivot Table Rows totals display mode.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the Pivot Table Rows totals display mode.")]
    public PivotTotalsMode PivotRowsTotalsMode
    {
      get
      {
        return this.pivotRowsTotalsMode;
      }
      set
      {
        if (value != this.pivotRowsTotalsMode)
        {
          this.pivotRowsTotalsMode = value;
          this.OnPropertyChanged("PivotRowsTotalsMode");
        }
        this.UpdateTotals();
      }
    }

    /// <summary>
    /// Gets or sets if the Pivot Table Columns Totals are enabled.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets if the Pivot Table Columns Totals are enabled.")]
    public bool PivotColumnsTotalsEnabled
    {
      get
      {
        return this.pivotColumnsTotalsEnabled;
      }
      set
      {
        if (value != this.pivotColumnsTotalsEnabled)
        {
          this.pivotColumnsTotalsEnabled = value;
          this.OnPropertyChanged("PivotColumnsTotalsEnabled");
        }
        this.UpdateTotals();
      }
    }

    /// <summary>
    /// Gets or sets the Pivot Table Columns totals display mode.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the Pivot Table Columns totals display mode.")]
    public PivotTotalsMode PivotColumnsTotalsMode
    {
      get
      {
        return this.pivotColumnsTotalsMode;
      }
      set
      {
        if (value != this.pivotColumnsTotalsMode)
        {
          this.pivotColumnsTotalsMode = value;
          this.OnPropertyChanged("PivotColumnsTotalsMode");
        }
        this.UpdateTotals();
      }
    }

    /// <summary>Determines if grouping is enabled or not.</summary>
    [Description("Determines if grouping is enabled or not.")]
    [Category("Behavior")]
    public bool GroupingEnabled
    {
      get
      {
        return this.isGroupingEnabled;
      }
      set
      {
        this.isGroupingEnabled = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the grid's cells are updated when a list changed event occurs.
    /// </summary>
    [Description("Gets or sets a value indicating whether the grid's cells are updated when a list changed event occurs.")]
    [Browsable(false)]
    public bool AutoUpdateOnListChanged
    {
      get
      {
        return this.autoUpdateOnListChanged;
      }
      set
      {
        if (value == this.autoUpdateOnListChanged)
          return;
        this.autoUpdateOnListChanged = value;
        this.OnPropertyChanged("AutoUpdateOnListChanged");
      }
    }

    internal int RowsCount
    {
      get
      {
        if (this.dataManager != null)
          return this.dataManager.Count;
        return 0;
      }
    }

    internal System.Type DataType
    {
      get
      {
        object firstRecord = this.GetFirstRecord();
        if (firstRecord != null)
          return firstRecord.GetType();
        return (System.Type) null;
      }
    }

    /// <summary>
    /// Occurs when the built-in context menu is being displayed.
    /// </summary>
    [Description("Occurs when the built-in context menu is being displayed.")]
    [Category("Action")]
    public event CancelEventHandler ContextMenuShowing;

    /// <summary>Occurs when the built-in context menu is being closed</summary>
    [Description("Occurs when the built-in context menu is being closed.")]
    [Category("Action")]
    public event EventHandler ContextMenuClosed;

    /// <summary>
    /// Occurs before a tooltip is shown and provides a way to customize the tooltip text
    /// </summary>
    [Category("Action")]
    [Description("Occurs before a tooltip is shown and provides a way to customize the tooltip text")]
    public event vDataGridView.GridTooltipEventHandler TooltipShow;

    /// <summary>
    /// Occurs when a the grid binds to data column from the data source
    /// </summary>
    [Category("Action")]
    [Description("Occurs when a the grid binds to data column from the data source")]
    public event vDataGridView.GridBindField BindField;

    /// <summary>Occurs when a data binding operation is complete.</summary>
    [Description("Occurs when a data binding operation is complete.")]
    [Category("Action")]
    public event vDataGridView.DataBindingCompleteEventHandler BindingComplete;

    /// <summary>Occurs when a data binding operation is complete.</summary>
    [Category("Action")]
    [Description("Occurs when a data binding operation starts.")]
    public event vDataGridView.DataBindingStartEventHandler BindingStart;

    /// <summary>Occurs when the vertical scrollbar value is changed.</summary>
    [Category("Action")]
    [Description("Occurs when the vertical scrollbar value is changed.")]
    public event EventHandler VerticalScrollBarValueChanged;

    /// <summary>
    /// Occurs when the horizontal scrollbar value is changed.
    /// </summary>
    [Description("Occurs when the horizontal scrollbar value is changed.")]
    [Category("Action")]
    public event EventHandler HorizontalScrollBarValueChanged;

    /// <summary>Occurs when the scroll position has changed</summary>
    [Category("Action")]
    [Description("Occurs when the scroll position has changed")]
    public event EventHandler AutoScrollPositionChanged;

    /// <summary>Occurs when the data binding progress changes</summary>
    /// <remarks>
    /// Use this event to monitor the progress of binding and loading data.
    /// </remarks>
    public event vDataGridView.BindingProgressFormatEventHandler BindingProgressChanged;

    /// <summary>Occurs when a property is changed.</summary>
    [Description("Occurs when a property is changed.")]
    [Category("Action")]
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>Occurs when a HierarchyItem is expanded</summary>
    [Description("Occurs when a HierarchyItem is expanded")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemEventHandler HierarchyItemExpanded;

    /// <summary>Occurs when a HierarchyItem is clicked</summary>
    [Description("Occurs when a HierarchyItem is clicked")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemMouseEventHandler HierarchyItemMouseClick;

    /// <summary>
    /// Occurs when the user presses a mouse button over HierarchyItem.
    /// </summary>
    [Category("Action")]
    [Description("Occurs when the user presses a mouse button over HierarchyItem.")]
    public event vDataGridView.HierarchyItemMouseEventHandler HierarchyItemMouseDown;

    /// <summary>
    /// Occurs when the user releases a mouse button over HierarchyItem.
    /// </summary>
    [Description("Occurs when the user releases a mouse button over HierarchyItem.")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemMouseEventHandler HierarchyItemMouseUp;

    /// <summary>Occurs when a HierarchyItem is double clicked.</summary>
    [Description("Occurs when a HierarchyItem is double clicked")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemMouseEventHandler HierarchyItemMouseDoubleClick;

    /// <summary>Occurs when a HierarchyItem is painted</summary>
    [Description("Occurs when a HierarchyItem is painted")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemPaintEventHandler HierarchyItemCustomPaint;

    /// <summary>Occurs when a HierarchyItem is collapsed</summary>
    [Description("Occurs when a HierarchyItem is collapsed")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemEventHandler HierarchyItemCollapsed;

    /// <summary>Occurs when a HierarchyItem is expanding</summary>
    [Category("Action")]
    [Description("Occurs when a HierarchyItem is expanding")]
    public event vDataGridView.HierarchyItemCancelEventHandler HierarchyItemExpanding;

    /// <summary>Occurs when a HierarchyItem is collapsing</summary>
    [Description("Occurs when a HierarchyItem is collapsing")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemCancelEventHandler HierarchyItemCollapsing;

    /// <summary>
    /// Occurs when a drag operation with header item is started
    /// </summary>
    [Category("Action")]
    [Description("Occurs when a drag operation with header item is started")]
    public event vDataGridView.HierarchyItemDragEventHandler HierarchyItemDragStarted;

    /// <summary>
    /// Occurs when a drag operation with header item is ended
    /// </summary>
    [Description("Occurs when a drag operation with header item is ended")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemDragEventHandler HierarchyItemDragEnded;

    /// <summary>
    /// Occurs when a drag operation with header item is starting
    /// </summary>
    [Description("Occurs when a drag operation with header item is starting.")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemDragCancelEventHandler HierarchyItemDragStarting;

    /// <summary>
    /// Occurs when a drag operation with header item is ending
    /// </summary>
    [Category("Action")]
    [Description("Occurs when a drag operation with header item is ending.")]
    public event vDataGridView.HierarchyItemDragCancelEventHandler HierarchyItemDragEnding;

    /// <summary>Occurs when a header item is dragged.</summary>
    [Description("Occurs when a header item is dragged.")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemDragEventHandler HierarchyItemDrag;

    /// <summary>
    /// Occurs when the selection of a HierarchyItem has changed.
    /// </summary>
    [Description("Occurs when the selection of a HierarchyItem has changed.")]
    [Category("Action")]
    public event vDataGridView.HierarchyItemEventHandler HierarchyItemSelectionChanged;

    /// <summary>
    /// Occurs when the CurrencyManager ListChanged event is raised.
    /// </summary>
    [Description("Occurs when the CurrencyManager ListChanged event is raised.")]
    [Category("Action")]
    public event EventHandler<ListChangedEventArgs> ListChanged;

    /// <summary>Occurs when a key is down.</summary>
    [Category("Action")]
    public event EventHandler<vDataGridView.CellKeyEventArgs> CellKeyDown;

    /// <summary>Occurs when a key is up.</summary>
    [Category("Action")]
    public event EventHandler<vDataGridView.CellKeyEventArgs> CellKeyUp;

    /// <summary>Occurs when a cell editor is being activated</summary>
    [Description("Occurs when a cell editor is being activated")]
    [Category("Action")]
    public event vDataGridView.CellEditorActivateEventHandler CellEditorActivate;

    /// <summary>Occurs when a cell editor is being activated</summary>
    [Category("Action")]
    [Description("Occurs when a cell editor is being deactivated")]
    public event vDataGridView.CellEditorActivateEventHandler CellEditorDeActivate;

    /// <summary>Occurs when a cell is validating</summary>
    [Description("Occurs when a cell is validating")]
    [Category("Action")]
    public event vDataGridView.CellCancelEventHandler CellValidating;

    /// <summary>Occurs when a cell is validated</summary>
    [Description("Occurs when a cell is validated")]
    [Category("Action")]
    public event vDataGridView.CellEventHandler CellValidated;

    /// <summary>Occurs when a cell edit is ending</summary>
    [Description("Occurs when a cell edit is ending")]
    [Category("Action")]
    public event vDataGridView.CellCancelEventHandler CellEndEdit;

    /// <summary>Occurs when a cell edit is begining</summary>
    [Category("Action")]
    [Description("Occurs when a cell edit is begining")]
    public event vDataGridView.CellCancelEventHandler CellBeginEdit;

    /// <summary>Occurs when the mouse button is down in a cell bounds</summary>
    [Description("Occurs when the mouse button is down in a cell bounds")]
    [Category("Action")]
    public event vDataGridView.CellMouseEventHandler CellMouseDown;

    /// <summary>Occurs when the mouse button is released</summary>
    [Category("Action")]
    [Description("Occurs when the mouse button is released")]
    public event vDataGridView.CellMouseEventHandler CellMouseUp;

    /// <summary>
    /// Occurs when the mouse button is clicked in cell bounds
    /// </summary>
    [Category("Action")]
    [Description("Occurs when the mouse button is clicked in cell bounds")]
    public event vDataGridView.CellMouseEventHandler CellMouseClick;

    /// <summary>
    /// Occurs when the mouse button is double clicked inside the cell bounds.
    /// </summary>
    [Description("Occurs when the mouse button is double clicked in cell bounds")]
    [Category("Action")]
    public event vDataGridView.CellMouseEventHandler CellMouseDoubleClick;

    /// <summary>Occurs when a cell edit is ending</summary>
    [Description("Occurs when a cell edit is ending")]
    [Category("Action")]
    public event vDataGridView.CellMouseEventHandler CellMouseMove;

    /// <summary>Occurs when the mouse pointer has entered cell bounds</summary>
    [Description("Occurs when the mouse pointer has left cell bounds")]
    [Category("Action")]
    public event vDataGridView.CellEventHandler CellMouseEnter;

    /// <summary>Occurs when the mouse pointer has left cell bounds</summary>
    [Category("Action")]
    [Description("Occurs when the mouse pointer has left cell bounds")]
    public event vDataGridView.CellEventHandler CellMouseLeave;

    /// <summary>Occurs when a cell date is parsed</summary>
    [Category("Action")]
    [Description("Occurs when a cell date is parsed")]
    public event vDataGridView.CellParsingEventHandler CellParsing;

    /// <summary>Occurs when a cell is painted</summary>
    [Description("Occurs when a cell is painted")]
    [Category("Action")]
    public event vDataGridView.CellPaintEventHandler CellCustomPaint;

    /// <summary>
    /// Occurs when a the rows group header is rendered. Use this event customize the text of the rows group headers.
    /// </summary>
    [Description("Occurs when a the rows group header is rendered. Use this event customize the text of the rows group headers.")]
    [Category("Action")]
    public event vDataGridView.GridGroupHeaderCustomTextNeededEventHandler GroupHeaderCustomTextNeeded;

    /// <summary>
    /// Occurs before a grid cell painting. Use this event in virtual mode to specify the content of the grid cell
    /// </summary>
    public event vDataGridView.CellValueNeededEventHandler CellValueNeeded;

    /// <summary>Occurs before a grid cell value changes.</summary>
    public event vDataGridView.CellValueChangingEventHandler CellValueChanging;

    /// <summary>Occurs after the value of a grid cell changed.</summary>
    public event vDataGridView.CellEventHandler CellValueChanged;

    /// <summary>Occurs when the grid cells selection has changed</summary>
    public event vDataGridView.CellEventHandler CellSelectionChanged;

    /// <summary>Occurs when a cell is painted.</summary>
    [Description("Occurs just before the cell painting starts.")]
    [Category("Action")]
    public event vDataGridView.CellCancelEventHandler CellPaintBegin;

    static vDataGridView()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>GridView constructor</summary>
    public vDataGridView()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.EnableNotifyMessage | ControlStyles.OptimizedDoubleBuffer, true);
      this.InitializeComponent();
      this.LoadCrossCursor();
      this.Controls.Add((Control) this.vScroll);
      this.Controls.Add((Control) this.hScroll);
      this.LoadDragDropArrows();
      this.AutoGenerateColumns = true;
      this.rectNow = new Rectangle(0, 0, 0, 0);
      this.resizingItem = (HierarchyItem) null;
      this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.NO_SELECT;
      this.colResizeState = vDataGridView.COL_RESIZE_STATES.NO_RESIZE;
      this.currentCursorType = vDataGridView.CURSOR_TYPE.ARROW;
      this.offsetX = 0;
      this.offsetY = 0;
      this.isBindingProgressEnabled = false;
      this.columnsHierarchy = new ColumnsHierarchy(this);
      this.rowsHierarchy = new RowsHierarchy(this);
      this.cellsArea = new CellsArea(this);
      this.scrollOnSelectionTimer = new System.Windows.Forms.Timer();
      this.scrollOnSelectionTimer.Interval = 10;
      this.scrollOnSelectionTimer.Tick += new EventHandler(this.scrollOnSelectionTimer_Tick);
      this.timerScroll = new System.Windows.Forms.Timer();
      this.timerScroll.Interval = 10;
      this.timerScroll.Stop();
      this.timerScroll.Tick += new EventHandler(this.timerScroll_Tick);
      this.AllowAnimations = true;
      this.timerToolTip.Tick += new EventHandler(this.timerToolTip_Tick);
      this.CursorChanged += new EventHandler(this.OnCursorChanged);
      this.menuItemSortAsc.Click += new EventHandler(this.menuItemSortAsc_Click);
      this.menuItemSortDesc.Click += new EventHandler(this.menuItemSortDesc_Click);
      this.menuItemSortClear.Click += new EventHandler(this.menuItemSortClear_Click);
      this.menuItemFilter.Click += new EventHandler(this.menuItemFilter_Click);
      this.menuItemFilterClear.Click += new EventHandler(this.menuItemFilterClear_Click);
      this.menuItemGroupBy.Click += new EventHandler(this.menuItemGroupBy_Click);
      this.menuItemColumnChooser.Click += new EventHandler(this.menuItemColumnChooser_Click);
      this.gridToolTip.Disposed += new EventHandler(this.gridToolTip_Disposed);
      this.hScroll.ValueChanged += new EventHandler(this.hScroll_ValueChanged);
      this.vScroll.ValueChanged += new EventHandler(this.vScroll_ValueChanged);
      this.SizeChanged += new EventHandler(this.GridView_SizeChanged);
      this.resizeToolTip = new ToolTip();
      this.GroupingColumns.CollectionChanged += new EventHandler<CollectionChangedEventArgs>(this.GroupingColumns_CollectionChanged);
      this.BackColor = Color.FromArgb(171, 171, 171);
    }

    private System.Type GetCellsDataType(HierarchyItem item)
    {
      System.Type type = typeof (string);
      if (item.ContentType != null)
      {
        if (item.ContentType == typeof (double) || item.ContentType == typeof (float) || (item.ContentType == typeof (int) || item.ContentType == typeof (short)) || (item.ContentType == typeof (int) || item.ContentType == typeof (long) || (item.ContentType == typeof (uint) || item.ContentType == typeof (ushort))) || (item.ContentType == typeof (uint) || item.ContentType == typeof (ulong)))
          type = typeof (double);
        else if (item.ContentType == typeof (DateTime))
          type = typeof (DateTime);
        if (item.ContentType == typeof (double?) || item.ContentType == typeof (float?) || (item.ContentType == typeof (int?) || item.ContentType == typeof (short?)) || (item.ContentType == typeof (int?) || item.ContentType == typeof (long?) || (item.ContentType == typeof (uint?) || item.ContentType == typeof (ushort?))) || (item.ContentType == typeof (uint?) || item.ContentType == typeof (ulong?)))
          type = typeof (double?);
        else if (item.ContentType == typeof (DateTime?))
          type = typeof (DateTime?);
      }
      else
      {
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = true;
        Hierarchy hierarchy = item.IsColumnsHierarchyItem ? (Hierarchy) item.DataGridView.RowsHierarchy : (Hierarchy) item.DataGridView.ColumnsHierarchy;
        List<HierarchyItem> pv = (List<HierarchyItem>) null;
        hierarchy.GetVisibleLeafLevelItems(ref pv);
        foreach (HierarchyItem hierarchyItem in pv)
        {
          if (hierarchyItem.DataGridView == null || !hierarchyItem.IsRowsHierarchyItem || (!hierarchyItem.DataGridView.GroupingEnabled || hierarchyItem.Depth >= hierarchyItem.DataGridView.GroupingColumns.Count))
          {
            object obj = !item.IsColumnsHierarchyItem ? this.cellsArea.GetCellValue(hierarchyItem, item) : this.cellsArea.GetCellValue(item, hierarchyItem);
            if (obj == null)
            {
              flag3 = true;
            }
            else
            {
              if (obj is DateTime)
                flag1 = true;
              else if (flag1)
              {
                flag1 = false;
                flag2 = false;
                break;
              }
              if (obj is double || obj is float || (obj is int || obj is short) || (obj is int || obj is long || (obj is uint || obj is ushort)) || (obj is uint || obj is ulong))
                flag2 = true;
              else if (flag2)
              {
                flag1 = false;
                flag2 = false;
                break;
              }
            }
          }
        }
        type = !flag1 ? (!flag2 ? typeof (string) : (flag3 ? typeof (double?) : typeof (double))) : (flag3 ? typeof (DateTime?) : typeof (DateTime));
      }
      return type;
    }

    /// <summary>Shows the filter form.</summary>
    /// <param name="filterItem">The filter item.</param>
    public void ShowFilterForm(HierarchyItem filterItem)
    {
      if (filterItem == null)
        return;
      this.filterBoundField = (BoundField) null;
      this.filteringWindow = new FilterForm();
      this.filteringWindow.FilterItem = filterItem;
      this.DeSerializeFilter(filterItem);
      this.filteringWindow.filterCheckedListBox.CheckOnClick = true;
      this.filteringWindow.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.comboBoxFilters1.SelectedItemChanged += new EventHandler(this.comboBoxFilters1_SelectedItemChanged);
      this.filteringWindow.comboBoxFilters2.SelectedItemChanged += new EventHandler(this.comboBoxFilters2_SelectedItemChanged);
      this.filteringWindow.filterCheckedListBox.ItemChecked += new ItemCheckChangedEventHandler(this.filterCheckedListBox_ItemChecked);
      this.filteringWindow.btnFilterWindowOk.Click += new EventHandler(this.btnFilterWindowOk_Click);
      this.filteringWindow.btnFilterWindowCancel.Click += new EventHandler(this.btnFilterWindowCancel_Click);
      if (this.FilterDisplayMode == FilterDisplayMode.Default)
        this.filteringWindow.buttonCustomFilter.Visible = true;
      this.filteringWindow.Shown += new EventHandler(this.filteringWindow_Shown);
      int num = (int) this.filteringWindow.ShowDialog();
      this.Refresh();
    }

    private void filteringWindow_Shown(object sender, EventArgs e)
    {
      this.filteringWindow.BoundsHelper.EnableResize = false;
      if (this.filteringWindow.comboBoxFilters1.SelectedIndex > 0 || this.filteringWindow.comboBoxFilters2.SelectedIndex > 0)
        this.filteringWindow.buttonCustomFilter.Toggle = CheckState.Checked;
      else
        this.filteringWindow.UpdateControlsVisibility();
    }

    internal void ShowFilterForm(BoundField boundField)
    {
      this.filterBoundField = boundField;
      this.filteringWindow.VIBlendTheme = this.VIBlendTheme;
      this.DeserializeBoundFieldSimpleFilterAndLoadUI(boundField);
      this.filteringWindow.filterCheckedListBox.CheckOnClick = true;
      this.filteringWindow.comboBoxFilters1.SelectedItemChanged += new EventHandler(this.comboBoxFilters1_SelectedItemChanged);
      this.filteringWindow.comboBoxFilters2.SelectedItemChanged += new EventHandler(this.comboBoxFilters2_SelectedItemChanged);
      this.filteringWindow.filterCheckedListBox.ItemChecked += new ItemCheckChangedEventHandler(this.filterCheckedListBox_ItemChecked);
      this.filteringWindow.btnFilterWindowOk.Click += new EventHandler(this.btnFilterWindowOk_Click);
      this.filteringWindow.btnFilterWindowCancel.Click += new EventHandler(this.btnFilterWindowCancel_Click);
      this.filteringWindow.buttonCustomFilter.Visible = false;
      this.filteringWindow.textBlockFilterBy.Tag = (object) boundField;
      this.filteringWindow.textBlockFilterBy.Text = this.Localization.GetString(LocalizationNames.FilterWindowFilterBy) + boundField.Text;
      this.filteringWindow.Shown += new EventHandler(this.filteringWindow_Shown);
      int num = (int) this.filteringWindow.ShowDialog();
      this.Refresh();
    }

    internal void DeSerializeFilter(HierarchyItem filterItem)
    {
      Hierarchy filteredHierarchy = filterItem.IsColumnsHierarchyItem ? (Hierarchy) this.RowsHierarchy : (Hierarchy) this.ColumnsHierarchy;
      int filterIndex = -1;
      for (int index = 0; index < filteredHierarchy.Filters.Count; ++index)
      {
        if (filteredHierarchy.Filters[index].Item == filterItem)
        {
          filterIndex = index;
          break;
        }
      }
      if (filterIndex != -1)
      {
        IFilterBase filterBase = filteredHierarchy.Filters[filterIndex].Filter;
        if (filterBase is FilterGroup<DateTime?>)
          this.filterType = typeof (DateTime?);
        else if (filterBase is FilterGroup<double?>)
          this.filterType = typeof (double?);
        else if (filterBase is FilterGroup<string>)
          this.filterType = typeof (string);
        else if (filterBase is SetFilter<DateTime?>)
          this.filterType = typeof (DateTime?);
        else if (filterBase is SetFilter<double?>)
          this.filterType = typeof (double?);
        else if (filterBase is SetFilter<string>)
          this.filterType = typeof (string);
      }
      else
        this.filterType = this.GetCellsDataType(filterItem);
      this.filteringWindow.textBlockFilterBy.Tag = (object) filterItem;
      this.filteringWindow.textBlockFilterBy.Text = this.Localization.GetString(LocalizationNames.FilterWindowFilterBy) + filterItem.Caption;
      this.filteringWindow.Tag = (object) (filterItem.Caption + ";" + this.filterType.ToString());
      this.filteringWindow.Text = this.Localization.GetString(LocalizationNames.FilterCriteriaDefinition);
      if (this.FilterDisplayMode == FilterDisplayMode.Default)
      {
        this.DeserializeSimpleFilterAndLoadUI(filterItem, filteredHierarchy, filterIndex, this.filterType);
        this.DeserializeCustomFilterAndLoadUI(filterItem, filteredHierarchy, filterIndex, this.filterType);
      }
      else
      {
        this.filteringWindow.buttonCustomFilter.Visible = false;
        if (this.FilterDisplayMode == FilterDisplayMode.Custom)
        {
          this.filteringWindow.buttonCustomFilter.Toggle = CheckState.Checked;
          this.DeserializeCustomFilterAndLoadUI(filterItem, filteredHierarchy, filterIndex, this.filterType);
        }
        else
        {
          this.filteringWindow.buttonCustomFilter.Toggle = CheckState.Unchecked;
          this.DeserializeSimpleFilterAndLoadUI(filterItem, filteredHierarchy, filterIndex, this.filterType);
        }
        this.filteringWindow.UpdateControlsVisibility();
      }
    }

    internal void DeserializeSimpleFilterAndLoadUI(HierarchyItem filterItem, Hierarchy filteredHierarchy, int filterIndex, System.Type filterType)
    {
      List<HierarchyItem> childItemsRecursive = filteredHierarchy.GetChildItemsRecursive();
      Dictionary<string, bool> records = new Dictionary<string, bool>();
      for (int index = 0; index < childItemsRecursive.Count; ++index)
      {
        HierarchyItem hierarchyItem = childItemsRecursive[index];
        if (!hierarchyItem.IsRowsHierarchyItem || !this.isGroupingEnabled || (this.groupingColumns.Count <= 0 || hierarchyItem.Items.Count <= 0))
        {
          object obj = !filterItem.IsColumnsHierarchyItem ? this.cellsArea.GetCellValue(filterItem, hierarchyItem) : this.cellsArea.GetCellValue(hierarchyItem, filterItem);
          string str = string.Empty;
          string @string;
          if (obj != null)
          {
            @string = obj.ToString();
            if (@string.Length == 0)
              @string = this.Localization.GetString(LocalizationNames.FilterItemsBlanksText);
          }
          else
            @string = this.Localization.GetString(LocalizationNames.FilterItemsNullsText);
          bool flag = false;
          if (filterIndex != -1)
            flag = !filteredHierarchy.Filters[filterIndex].Filter.Evaluate(obj);
          if (!records.ContainsKey(@string))
            records.Add(@string, !flag);
        }
      }
      this.LoadFilterFormSimpleUI(filterItem.Caption, records, filterType);
    }

    internal void DeserializeBoundFieldSimpleFilterAndLoadUI(BoundField boundField)
    {
      Dictionary<object, bool> uniqueRowValues = (Dictionary<object, bool>) null;
      System.Type type = typeof (string);
      if (!this.GetUniqueRowValues(boundField, out uniqueRowValues, out type))
        return;
      IFilterBase filterBase = (IFilterBase) null;
      foreach (BoundFieldFilter boundFieldsFilter in this.BoundFieldsFilters)
      {
        if (boundFieldsFilter.DataField == boundField.DataField)
        {
          foreach (IFilterBase filter in boundFieldsFilter.Filters)
          {
            if (filter is SetFilter<double?> || filter is SetFilter<bool?> || (filter is SetFilter<string> || filter is SetFilter<DateTime?>))
            {
              filterBase = filter;
              break;
            }
          }
        }
      }
      Dictionary<string, bool> records = new Dictionary<string, bool>();
      Dictionary<object, bool>.Enumerator enumerator = uniqueRowValues.GetEnumerator();
      while (enumerator.MoveNext())
      {
        object key1 = enumerator.Current.Key;
        bool flag = false;
        if (filterBase != null)
          flag = !filterBase.Evaluate(key1);
        string str = string.Empty;
        string key2;
        if (key1 != null)
        {
          key2 = key1.ToString();
          if (key2.Length == 0)
            key2 = str = this.Localization.GetString(LocalizationNames.FilterItemsBlanksText);
        }
        else
          key2 = str = this.Localization.GetString(LocalizationNames.FilterItemsNullsText);
        if (!records.ContainsKey(key2))
          records.Add(key2, !flag);
      }
      this.LoadFilterFormSimpleUI(boundField.Text, records, this.filterType);
      this.filteringWindow.textBlockFilterBy.Tag = (object) boundField;
      this.filteringWindow.textBlockFilterBy.Text = this.Localization.GetString(LocalizationNames.FilterWindowFilterBy) + boundField.Text;
      this.filteringWindow.Tag = (object) (boundField.DataField + ";" + this.filterType.ToString());
      this.filteringWindow.Text = this.Localization.GetString(LocalizationNames.FilterCriteriaDefinition);
    }

    private void LoadFilterFormSimpleUI(string filterBy, Dictionary<string, bool> records, System.Type recordType)
    {
      this.filteringWindow.filterCheckedListBox.Items.Clear();
      VIBlend.WinForms.Controls.ListItem listItem1 = new VIBlend.WinForms.Controls.ListItem();
      listItem1.Text = this.Localization.GetString(LocalizationNames.FilterItemsAllText);
      listItem1.IsThreeState = true;
      this.filteringWindow.filterCheckedListBox.Items.Add(listItem1);
      this.filteringWindow.filterCheckedListBox.VIBlendTheme = this.VIBlendTheme;
      Dictionary<string, bool>.Enumerator enumerator = records.GetEnumerator();
      int num = 0;
      while (enumerator.MoveNext())
      {
        VIBlend.WinForms.Controls.ListItem listItem2 = new VIBlend.WinForms.Controls.ListItem();
        listItem2.IsChecked = new bool?(enumerator.Current.Value);
        if (enumerator.Current.Value)
          ++num;
        listItem2.Text = enumerator.Current.Key;
        this.filteringWindow.filterCheckedListBox.Items.Add(listItem2);
      }
      listItem1.IsChecked = new bool?(false);
      if (num == 0)
        return;
      if (num == records.Count)
        listItem1.IsChecked = new bool?(true);
      else
        listItem1.IsChecked = new bool?();
    }

    private void LoadComboBoxes(System.Type type)
    {
      List<string> stringList = new List<string>();
      int num1 = -1;
      int num2 = -1;
      if (type == typeof (DateTime))
      {
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorSelect));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorEqual));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorNotEqual));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorLessThan));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorLessThanOrEqual));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorGreaterThan));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorGreaterThanOrEqual));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorIsNull));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorIsNotNull));
        num1 = 6;
        num2 = 4;
      }
      else if (type == typeof (double))
      {
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorSelect));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorEqual));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorNotEqual));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorLessThan));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorLessThanOrEqual));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorGreaterThan));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorGreaterThanOrEqual));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorIsNull));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorIsNotNull));
        num1 = 0;
        num2 = 0;
      }
      else if (type == typeof (string))
      {
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorSelect));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorIsEmpty));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorIsNotEmpty));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorIsNull));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorIsNotNull));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorContains));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorContainsCaseSensitive));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorDoesNotContain));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorDoesNotContainCaseSensitive));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorStartsWith));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorStartsWithCaseSensitive));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorEndsWith));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorEndsWithCaseSensitive));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorEqual));
        stringList.Add(this.Localization.GetString(LocalizationNames.FilterOperatorEqualCaseSensitive));
        num1 = 0;
        num2 = 0;
      }
      try
      {
        this.filteringWindow.comboBoxFilters1.Items.Clear();
      }
      catch (Exception)
      {
      }
      try
      {
        this.filteringWindow.comboBoxFilters2.Items.Clear();
      }
      catch (Exception)
      {
      }
      foreach (string ItemText in stringList)
      {
        if (!string.IsNullOrEmpty(ItemText))
        {
          this.filteringWindow.comboBoxFilters1.Items.Add(ItemText);
          this.filteringWindow.comboBoxFilters2.Items.Add(ItemText);
        }
      }
      this.filteringWindow.comboBoxFilters1.SelectedIndex = num1;
      this.filteringWindow.comboBoxFilters2.SelectedIndex = num2;
    }

    private void LoadFilterFormCustomUI(string filterBy, System.Type recordType, object filter1Value, int filter1Operator, object filter2Value, int filter2Operator, object recordMinValue, object recordMaxValue, bool filterGroupOperator)
    {
      this.filteringWindow.radioButtonFilterAND.Text = this.Localization.GetString(LocalizationNames.FilterWindowAndRadioButton);
      this.filteringWindow.radioButtonFilterOR.Text = this.Localization.GetString(LocalizationNames.FilterWindowOrRadioButton);
      this.filteringWindow.btnFilterWindowOk.Text = this.Localization.GetString(LocalizationNames.FilterWindowOkButton);
      this.filteringWindow.btnFilterWindowCancel.Text = this.Localization.GetString(LocalizationNames.FilterWindowCancelButton);
      this.filteringWindow.textBoxValue1.Visible = false;
      this.filteringWindow.textBoxValue2.Visible = false;
      this.filteringWindow.dateTimeEditorValue1.Visible = false;
      this.filteringWindow.dateTimeEditorValue2.Visible = false;
      this.filteringWindow.textBoxValue1.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.textBoxValue2.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.dateTimeEditorValue1.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.dateTimeEditorValue2.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.btnFilterWindowOk.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.btnFilterWindowCancel.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.radioButtonFilterOR.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.radioButtonFilterAND.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.buttonCustomFilter.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.comboBoxFilters1.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.comboBoxFilters2.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.filterCheckedListBox.VIBlendTheme = this.VIBlendTheme;
      this.filteringWindow.dateTimeEditorValue1.Bounds = this.filteringWindow.textBoxValue1.Bounds;
      this.filteringWindow.dateTimeEditorValue2.Bounds = this.filteringWindow.textBoxValue2.Bounds;
      List<string> stringList = new List<string>();
      if (recordType == typeof (DateTime) || recordType == typeof (DateTime?))
      {
        DateTime dateTime1 = DateTime.MaxValue;
        DateTime dateTime2 = DateTime.MinValue;
        DateTime dateTime3 = (DateTime) recordMinValue;
        DateTime dateTime4 = (DateTime) recordMaxValue;
        this.filteringWindow.radioButtonFilterAND.Checked = filterGroupOperator;
        this.filteringWindow.dateTimeEditorValue1.Value = new DateTime?(dateTime3);
        this.filteringWindow.dateTimeEditorValue2.Value = new DateTime?(dateTime4);
        this.LoadComboBoxes(typeof (DateTime));
        if (filter1Operator != -1)
        {
          this.filteringWindow.comboBoxFilters1.SelectedIndex = filter1Operator + 1;
          if (filter1Value != null)
            this.filteringWindow.dateTimeEditorValue1.Value = new DateTime?((DateTime) filter1Value);
        }
        if (filter2Operator != -1)
        {
          this.filteringWindow.comboBoxFilters2.SelectedIndex = filter2Operator + 1;
          if (filter2Value != null)
            this.filteringWindow.dateTimeEditorValue2.Value = new DateTime?((DateTime) filter2Value);
        }
      }
      else if (recordType == typeof (double) || recordType == typeof (double?))
      {
        this.filteringWindow.radioButtonFilterAND.Checked = filterGroupOperator;
        this.filteringWindow.textBoxValue1.Text = "0";
        this.filteringWindow.textBoxValue2.Text = "0";
        this.LoadComboBoxes(typeof (double));
        if (filter1Operator != -1)
        {
          this.filteringWindow.comboBoxFilters1.SelectedIndex = filter1Operator + 1;
          if (filter1Value != null)
          {
            this.filteringWindow.textBoxValue1.Text = string.Format("{0:#.##}", filter1Value);
            this.filteringWindow.textBoxValue1.Visible = true;
          }
        }
        if (filter2Operator != -1)
        {
          this.filteringWindow.comboBoxFilters2.SelectedIndex = filter2Operator + 1;
          if (filter2Value != null)
          {
            this.filteringWindow.textBoxValue2.Text = string.Format("{0:#.##}", filter2Value);
            this.filteringWindow.textBoxValue2.Visible = true;
          }
        }
      }
      else
      {
        this.filteringWindow.radioButtonFilterAND.Checked = true;
        this.filteringWindow.textBoxValue1.Text = "";
        this.filteringWindow.textBoxValue2.Text = "";
        this.LoadComboBoxes(typeof (string));
        if (filter1Operator != -1)
        {
          this.filteringWindow.comboBoxFilters1.SelectedIndex = filter1Operator + 1;
          this.filteringWindow.textBoxValue1.Text = filter1Value == null ? "" : (string) filter1Value;
        }
        if (filter2Operator != -1)
        {
          this.filteringWindow.comboBoxFilters2.SelectedIndex = filter2Operator + 1;
          this.filteringWindow.textBoxValue2.Text = filter2Value == null ? "" : (string) filter2Value;
        }
      }
      this.filteringWindow.radioButtonFilterOR.Checked = !filterGroupOperator;
      this.UpdateValueInputVisibility();
    }

    private void DeserializeCustomFilterAndLoadUI(HierarchyItem filterItem, Hierarchy filteredHierarchy, int filterIndex, System.Type filterType)
    {
      object filter1Value = (object) null;
      int filter1Operator = -1;
      object filter2Value = (object) null;
      int filter2Operator = -1;
      object recordMinValue = (object) null;
      object recordMaxValue = (object) null;
      bool filterGroupOperator = true;
      if (filterType == typeof (DateTime) || filterType == typeof (DateTime?))
      {
        DateTime minValue = DateTime.MaxValue;
        DateTime maxValue = DateTime.MinValue;
        filterItem.DataGridView.cellsArea.GetCellsMinMaxDate(filterItem, typeof (DateTime), ref minValue, ref maxValue);
        recordMinValue = (object) minValue;
        recordMaxValue = (object) maxValue;
        if (filterIndex != -1 && filteredHierarchy.Filters[filterIndex].Filter is FilterGroup<DateTime?>)
        {
          FilterGroup<DateTime?> filterGroup = (FilterGroup<DateTime?>) filteredHierarchy.Filters[filterIndex].Filter;
          if (filterGroup.FiltersCount > 0)
          {
            DateTimeFilter dateTimeFilter = (DateTimeFilter) filterGroup.GetFilterAt(0);
            filter1Operator = (int) dateTimeFilter.ComparisonOperator;
            if (dateTimeFilter.Value.HasValue)
              filter1Value = (object) dateTimeFilter.Value.Value;
          }
          if (filterGroup.FiltersCount > 1)
          {
            filterGroupOperator = filterGroup.GetOperatorAt(1) == FilterOperator.AND;
            DateTimeFilter dateTimeFilter = (DateTimeFilter) filterGroup.GetFilterAt(1);
            filter2Operator = (int) dateTimeFilter.ComparisonOperator;
            if (dateTimeFilter.Value.HasValue)
              filter2Value = (object) dateTimeFilter.Value.Value;
          }
        }
      }
      else if (filterType == typeof (double) || filterType == typeof (double?))
      {
        if (filterIndex != -1 && filteredHierarchy.Filters[filterIndex].Filter is FilterGroup<double?>)
        {
          FilterGroup<double?> filterGroup = (FilterGroup<double?>) filteredHierarchy.Filters[filterIndex].Filter;
          if (filterGroup.FiltersCount > 0)
          {
            NumericFilter numericFilter = (NumericFilter) filterGroup.GetFilterAt(0);
            filter1Operator = (int) numericFilter.ComparisonOperator;
            if (numericFilter.Value.HasValue)
              filter1Value = (object) numericFilter.Value;
          }
          if (filterGroup.FiltersCount > 1)
          {
            filterGroupOperator = filterGroup.GetOperatorAt(1) == FilterOperator.AND;
            NumericFilter numericFilter = (NumericFilter) filterGroup.GetFilterAt(1);
            filter2Operator = (int) numericFilter.ComparisonOperator;
            if (numericFilter.Value.HasValue)
              filter2Value = (object) numericFilter.Value;
          }
        }
      }
      else if (filterIndex != -1 && filteredHierarchy.Filters[filterIndex].Filter is FilterGroup<string>)
      {
        FilterGroup<string> filterGroup = (FilterGroup<string>) filteredHierarchy.Filters[filterIndex].Filter;
        if (filterGroup.FiltersCount > 0)
        {
          StringFilter stringFilter = (StringFilter) filterGroup.GetFilterAt(0);
          filter1Operator = (int) stringFilter.ComparisonOperator;
          filter1Value = (object) stringFilter.Value;
        }
        if (filterGroup.FiltersCount > 1)
        {
          filterGroupOperator = filterGroup.GetOperatorAt(1) == FilterOperator.AND;
          StringFilter stringFilter = (StringFilter) filterGroup.GetFilterAt(1);
          filter2Operator = (int) stringFilter.ComparisonOperator;
          filter2Value = (object) stringFilter.Value;
        }
      }
      this.LoadFilterFormCustomUI(filterItem.Caption, filterType, filter1Value, filter1Operator, filter2Value, filter2Operator, recordMinValue, recordMaxValue, filterGroupOperator);
    }

    private void OnSwitchFilterUI(object sender, EventArgs e)
    {
      this.ToggleCustomFilterUI();
    }

    private void ToggleCustomFilterUI()
    {
      if (this.filteringWindow.buttonCustomFilter.Toggle != CheckState.Checked)
        this.filteringWindow.buttonCustomFilter.Text = "Custom Filter";
      else
        this.filteringWindow.buttonCustomFilter.Text = "Basic Filter";
    }

    private void btnFilterWindowCancel_Click(object sender, EventArgs e)
    {
      if (this.filteringWindow != null && this.filteringWindow.FilterItem != null)
        this.filteringWindow.FilterItem.DataGridView.Refresh();
      this.filteringWindow.Close();
    }

    private void filteringWindow_Close(object sender, EventArgs e)
    {
      if (this.filteringWindow == null || this.filteringWindow.FilterItem == null)
        return;
      this.filteringWindow.FilterItem.DataGridView.Refresh();
    }

    private void filterCheckedListBox_ItemChecked(object sender, ItemCheckChangedEventArgs args)
    {
      if (this.isCheckUpdate)
        return;
      string @string = this.Localization.GetString(LocalizationNames.FilterItemsAllText);
      this.isCheckUpdate = true;
      if (args.Item.Text == @string)
      {
        bool? isChecked = args.Item.IsChecked;
        if ((!isChecked.GetValueOrDefault() ? 0 : (isChecked.HasValue ? 1 : 0)) != 0)
          this.filteringWindow.filterCheckedListBox.CheckAllItems();
        else
          this.filteringWindow.filterCheckedListBox.UnCheckAllItems();
      }
      else
      {
        int num = 0;
        for (int index = 1; index < this.filteringWindow.filterCheckedListBox.Items.Count; ++index)
        {
          bool? isChecked = this.filteringWindow.filterCheckedListBox.Items[index].IsChecked;
          if ((!isChecked.GetValueOrDefault() ? 0 : (isChecked.HasValue ? 1 : 0)) != 0)
            ++num;
        }
        if (num == this.filteringWindow.filterCheckedListBox.Items.Count - 1)
          this.filteringWindow.filterCheckedListBox.Items[0].IsChecked = new bool?(true);
        else if (num == 0)
          this.filteringWindow.filterCheckedListBox.Items[0].IsChecked = new bool?(false);
        else
          this.filteringWindow.filterCheckedListBox.Items[0].IsChecked = new bool?();
      }
      this.isCheckUpdate = false;
    }

    private void comboBoxFilters1_SelectedItemChanged(object sender, EventArgs e)
    {
      this.UpdateValueInputVisibility();
    }

    private void comboBoxFilters2_SelectedItemChanged(object sender, EventArgs e)
    {
      this.UpdateValueInputVisibility();
    }

    internal void UpdateValueInputVisibility()
    {
      if (this.filteringWindow.comboBoxFilters1.SelectedIndex <= 0)
      {
        int selectedIndex = this.filteringWindow.comboBoxFilters2.SelectedIndex;
      }
      if (this.filteringWindow.comboBoxFilters1.SelectedItem != null)
      {
        bool flag = this.filteringWindow.comboBoxFilters1.SelectedIndex == 0;
        this.filteringWindow.labelValue1.Visible = !flag;
        this.filteringWindow.textBoxValue1.Visible = !flag;
        this.filteringWindow.dateTimeEditorValue1.Visible = !flag;
      }
      if (this.filteringWindow.comboBoxFilters2.SelectedItem != null)
      {
        bool flag = this.filteringWindow.comboBoxFilters2.SelectedIndex == 0;
        this.filteringWindow.vLabel1.Visible = !flag;
        this.filteringWindow.textBoxValue2.Visible = !flag;
        this.filteringWindow.dateTimeEditorValue2.Visible = !flag;
      }
      if (this.filterType == typeof (DateTime?) || this.filterType == typeof (DateTime))
      {
        if (this.filteringWindow.textBoxValue1.Visible)
          this.filteringWindow.textBoxValue1.Visible = false;
        if (this.filteringWindow.textBoxValue2.Visible)
          this.filteringWindow.textBoxValue2.Visible = false;
        this.filteringWindow.dateTimeEditorValue1.BringToFront();
        this.filteringWindow.dateTimeEditorValue2.BringToFront();
      }
      else
      {
        if (this.filteringWindow.dateTimeEditorValue1.Visible)
          this.filteringWindow.dateTimeEditorValue1.Visible = false;
        if (!this.filteringWindow.dateTimeEditorValue2.Visible)
          return;
        this.filteringWindow.dateTimeEditorValue2.Visible = false;
      }
    }

    private void btnFilterWindowOk_Click(object sender, EventArgs e)
    {
      IFilterBase filterBase = this.filteringWindow.buttonCustomFilter.Toggle != CheckState.Checked ? this.SerializeBasicFilter() : this.SerializeCustomFilter();
      if (this.filterBoundField == null)
      {
        if (this.filteringWindow.Tag == null)
          return;
        HierarchyItem hierarchyItem = (HierarchyItem) this.filteringWindow.textBlockFilterBy.Tag;
        Hierarchy hierarchy = hierarchyItem.IsColumnsHierarchyItem ? (Hierarchy) this.RowsHierarchy : (Hierarchy) this.ColumnsHierarchy;
        for (int index = 0; index < hierarchy.Filters.Count; ++index)
        {
          if (hierarchy.Filters[index].Item == hierarchyItem)
          {
            hierarchy.Filters.RemoveAt(index);
            break;
          }
        }
        hierarchy.Filters.Add(new HierarchyItemFilter()
        {
          Filter = filterBase,
          Item = hierarchyItem
        });
        this.filteringWindow.Close();
      }
      else
      {
        BoundFieldFilter boundFieldFilter = (BoundFieldFilter) null;
        for (int index = 0; index < this.BoundFieldsFilters.Count; ++index)
        {
          if (this.filterBoundField.DataField == this.BoundFieldsFilters[index].DataField)
          {
            boundFieldFilter = this.BoundFieldsFilters[index];
            break;
          }
        }
        if (boundFieldFilter == null)
          boundFieldFilter = new BoundFieldFilter();
        boundFieldFilter.DataField = this.filterBoundField.DataField;
        boundFieldFilter.Filters.Clear();
        boundFieldFilter.Filters.Add(filterBase);
        if (-1 == this.BoundFieldsFilters.IndexOf(boundFieldFilter))
          this.BoundFieldsFilters.Add(boundFieldFilter);
        this.filteringWindow.Close();
        this.DataBind();
      }
    }

    private IFilterBase SerializeBasicFilter()
    {
      string string1 = this.Localization.GetString(LocalizationNames.FilterItemsBlanksText);
      string string2 = this.Localization.GetString(LocalizationNames.FilterItemsNullsText);
      string[] strArray = ((string) this.filteringWindow.Tag).Split(';');
      int num = 0;
      if (strArray[1] == typeof (double).ToString() || strArray[1] == typeof (double?).ToString())
      {
        SetFilter<double?> setFilter = new SetFilter<double?>();
        foreach (VIBlend.WinForms.Controls.ListItem listItem in this.filteringWindow.filterCheckedListBox.Items)
        {
          if (num++ != 0 && !listItem.IsChecked.Value)
          {
            if (listItem.Text == string1 || listItem.Text == string2)
            {
              setFilter.AddItem(new double?());
              setFilter.SetItemState(new double?(), false);
            }
            else
            {
              double result = 0.0;
              double.TryParse(listItem.Text, out result);
              setFilter.AddItem(new double?(result));
              setFilter.SetItemState(new double?(result), false);
            }
          }
        }
        return (IFilterBase) setFilter;
      }
      if (strArray[1] == typeof (DateTime).ToString() || strArray[1] == typeof (DateTime?).ToString())
      {
        SetFilter<DateTime?> setFilter = new SetFilter<DateTime?>();
        foreach (VIBlend.WinForms.Controls.ListItem listItem in this.filteringWindow.filterCheckedListBox.Items)
        {
          if (num++ != 0 && !listItem.IsChecked.Value)
          {
            if (listItem.Text == string1 || listItem.Text == string2)
            {
              setFilter.AddItem(new DateTime?());
              setFilter.SetItemState(new DateTime?(), false);
            }
            else
            {
              DateTime result = DateTime.MinValue;
              DateTime.TryParse(listItem.Text, out result);
              setFilter.AddItem(new DateTime?(result));
              setFilter.SetItemState(new DateTime?(result), false);
            }
          }
        }
        return (IFilterBase) setFilter;
      }
      if (!(strArray[1] == typeof (string).ToString()))
        return (IFilterBase) null;
      SetFilter<string> setFilter1 = new SetFilter<string>();
      foreach (VIBlend.WinForms.Controls.ListItem listItem in this.filteringWindow.filterCheckedListBox.Items)
      {
        if (num++ != 0 && !listItem.IsChecked.Value)
        {
          if (listItem.Text == string1 || listItem.Text == string2)
          {
            setFilter1.AddItem("");
            setFilter1.SetItemState("", false);
          }
          else
          {
            setFilter1.AddItem(listItem.Text);
            setFilter1.SetItemState(listItem.Text, false);
          }
        }
      }
      return (IFilterBase) setFilter1;
    }

    private IFilterBase SerializeCustomFilter()
    {
      string[] strArray = ((string) this.filteringWindow.Tag).Split(';');
      if (strArray[1] == typeof (double).ToString() || strArray[1] == typeof (double?).ToString())
      {
        FilterGroup<double?> filterGroup = new FilterGroup<double?>();
        if (this.filteringWindow.comboBoxFilters1.SelectedIndex > 0)
        {
          NumericFilter numericFilter = new NumericFilter();
          numericFilter.ComparisonOperator = (NumericComparisonOperator) (this.filteringWindow.comboBoxFilters1.SelectedIndex - 1);
          if (this.filteringWindow.comboBoxFilters1.SelectedItem.ToString().ToLower().Contains("null"))
          {
            numericFilter.Value = new double?();
          }
          else
          {
            double result = 0.0;
            double.TryParse(this.filteringWindow.textBoxValue1.Text, out result);
            numericFilter.Value = new double?(result);
          }
          filterGroup.AddFilter(FilterOperator.AND, (IFilterBase) numericFilter);
        }
        if (this.filteringWindow.comboBoxFilters1.SelectedIndex > 0 && this.filteringWindow.comboBoxFilters2.SelectedIndex > 0)
        {
          NumericFilter numericFilter = new NumericFilter();
          numericFilter.ComparisonOperator = (NumericComparisonOperator) (this.filteringWindow.comboBoxFilters2.SelectedIndex - 1);
          if (this.filteringWindow.comboBoxFilters1.SelectedItem.ToString().ToLower().Contains("null"))
          {
            numericFilter.Value = new double?();
          }
          else
          {
            double result = 0.0;
            double.TryParse(this.filteringWindow.textBoxValue2.Text, out result);
            numericFilter.Value = new double?(result);
          }
          filterGroup.AddFilter(this.filteringWindow.radioButtonFilterAND.Checked ? FilterOperator.AND : FilterOperator.OR, (IFilterBase) numericFilter);
        }
        return (IFilterBase) filterGroup;
      }
      if (strArray[1] == typeof (DateTime).ToString() || strArray[1] == typeof (DateTime?).ToString())
      {
        FilterGroup<DateTime?> filterGroup = new FilterGroup<DateTime?>();
        if (this.filteringWindow.comboBoxFilters1.SelectedIndex > 0)
          filterGroup.AddFilter(FilterOperator.AND, (IFilterBase) new DateTimeFilter()
          {
            ComparisonOperator = (DateTimeComparisonOperator) (this.filteringWindow.comboBoxFilters1.SelectedIndex - 1),
            Value = (!this.filteringWindow.comboBoxFilters1.SelectedItem.ToString().ToLower().Contains("null") ? this.filteringWindow.dateTimeEditorValue1.Value : new DateTime?())
          });
        if (this.filteringWindow.comboBoxFilters1.SelectedIndex > 0 && this.filteringWindow.comboBoxFilters2.SelectedIndex > 0)
          filterGroup.AddFilter(this.filteringWindow.radioButtonFilterAND.Checked ? FilterOperator.AND : FilterOperator.OR, (IFilterBase) new DateTimeFilter()
          {
            ComparisonOperator = (DateTimeComparisonOperator) (this.filteringWindow.comboBoxFilters2.SelectedIndex - 1),
            Value = (!this.filteringWindow.comboBoxFilters2.SelectedItem.ToString().ToLower().Contains("null") ? this.filteringWindow.dateTimeEditorValue2.Value : new DateTime?())
          });
        return (IFilterBase) filterGroup;
      }
      FilterGroup<string> filterGroup1 = new FilterGroup<string>();
      if (this.filteringWindow.comboBoxFilters1.SelectedIndex > 0)
        filterGroup1.AddFilter(FilterOperator.AND, (IFilterBase) new StringFilter()
        {
          ComparisonOperator = (StringFilterComparisonOperator) (this.filteringWindow.comboBoxFilters1.SelectedIndex - 1),
          Value = this.filteringWindow.textBoxValue1.Text
        });
      if (this.filteringWindow.comboBoxFilters1.SelectedIndex > 0 && this.filteringWindow.comboBoxFilters2.SelectedIndex > 0)
        filterGroup1.AddFilter(this.filteringWindow.radioButtonFilterAND.Checked ? FilterOperator.AND : FilterOperator.OR, (IFilterBase) new StringFilter()
        {
          ComparisonOperator = (StringFilterComparisonOperator) (this.filteringWindow.comboBoxFilters2.SelectedIndex - 1),
          Value = this.filteringWindow.textBoxValue2.Text
        });
      return (IFilterBase) filterGroup1;
    }

    private bool HandleKeyboardNavigation(KeyEventArgs e)
    {
      if (this.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.CELLS_SELECT)
        return this.HandleCellsKeyboardNavigation(e);
      if (this.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.ROW_SELECT || this.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.COL_SELECT)
        return this.HandleItemsKeyboardNavigation(e);
      return false;
    }

    private bool HandleCellsKeyboardNavigation(KeyEventArgs e)
    {
      GridCell cell1 = this.cellKBRangeSelectionEnd;
      GridCell cell2 = (GridCell) null;
      if (cell1 == null)
        return false;
      if (e.KeyCode == Keys.Up)
        cell2 = this.CellsArea.GetCellAbove(cell1);
      else if (e.KeyCode == Keys.Down)
        cell2 = this.CellsArea.GetCellBelow(cell1);
      else if (e.KeyCode == Keys.Left)
        cell2 = this.CellsArea.GetCellLeft(cell1);
      else if (e.KeyCode == Keys.Right)
        cell2 = this.CellsArea.GetCellRight(cell1);
      else if (e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
      {
        List<HierarchyItem> pv = (List<HierarchyItem>) null;
        this.columnsHierarchy.GetVisibleLeafLevelItems(ref pv);
        if (pv.Count > 0 && cell1 != null)
        {
          HierarchyItem columnItem = e.KeyCode == Keys.Home ? pv[0] : pv[pv.Count - 1];
          cell2 = new GridCell(cell1.RowItem, columnItem, this.CellsArea);
        }
      }
      else if (e.KeyCode == Keys.Prior || e.KeyCode == Keys.Next)
      {
        List<HierarchyItem> pv = (List<HierarchyItem>) null;
        this.rowsHierarchy.GetVisibleLeafLevelItems(ref pv);
        if (pv.Count > 0 && cell1 != null)
        {
          HierarchyItem columnItem = cell1.ColumnItem;
          HierarchyItem rowItem = cell1.RowItem;
          int y = this.Bounds.Height;
          if (e.KeyCode == Keys.Prior)
            y = -y;
          cell2 = this.cellsArea.GetCellAtDistance(cell1, new Point(5, y));
          if (cell2 == null)
            cell2 = new GridCell(e.KeyCode == Keys.Prior ? pv[0] : pv[pv.Count - 1], columnItem, this.cellsArea);
          else if (!cell2.ColumnItem.Caption.Equals((object) cell1.ColumnItem))
            cell2 = new GridCell(cell2.RowItem, cell1.ColumnItem, this.cellsArea);
        }
      }
      if (cell2 == null)
        return false;
      if (e.Modifiers != Keys.Control)
        this.CellsArea.ClearSelection();
      if (e.Modifiers != Keys.Shift || !this.multipleSelectionEnabled)
      {
        this.cellKBRangeSelectionStart = cell2;
        this.cellKBRangeSelectionEnd = cell2;
      }
      else
      {
        if (this.cellKBRangeSelectionStart == null)
          this.cellKBRangeSelectionStart = cell2;
        this.cellKBRangeSelectionEnd = cell2;
      }
      foreach (GridCell gridCell in this.GetCellsInKBSelectionRange())
        this.CellsArea.SelectCellInternal(gridCell.RowItem, gridCell.ColumnItem);
      this.EnsureVisible(cell2);
      this.Refresh();
      return true;
    }

    private bool HandleItemsKeyboardNavigation(KeyEventArgs e)
    {
      Hierarchy hierarchy = this.selectionMode == vDataGridView.SELECTION_MODE.FULL_ROW_SELECT ? (Hierarchy) this.rowsHierarchy : (Hierarchy) this.columnsHierarchy;
      HierarchyItem rowItem = this.selectionMode == vDataGridView.SELECTION_MODE.FULL_ROW_SELECT ? this.rowItemRangeSelectionEnd : this.colItemRangeSelectionEnd;
      HierarchyItem hierarchyItem = (HierarchyItem) null;
      if (rowItem == null)
        return false;
      if (e.KeyCode == Keys.Up)
        hierarchyItem = rowItem.ItemAbove;
      else if (e.KeyCode == Keys.Down)
        hierarchyItem = rowItem.ItemBelow;
      else if (e.KeyCode == Keys.Left)
        hierarchyItem = rowItem.ItemLeft;
      else if (e.KeyCode == Keys.Right)
        hierarchyItem = rowItem.ItemRight;
      else if (e.KeyCode == Keys.Home || e.KeyCode == Keys.End)
      {
        if (rowItem != null)
        {
          HierarchyItem parentItem = rowItem.ParentItem;
          if (parentItem != null)
          {
            if (parentItem.Items.Count > 0)
              hierarchyItem = e.KeyCode == Keys.Home ? parentItem.Items[0] : parentItem.Items[parentItem.Items.Count - 1];
            else if (parentItem.SummaryItems.Items.Count > 0)
              hierarchyItem = e.KeyCode == Keys.Home ? parentItem.SummaryItems[0] : parentItem.SummaryItems[parentItem.Items.Count - 1];
          }
          else
            hierarchyItem = e.KeyCode == Keys.Home ? rowItem.Hierarchy.Items[0] : rowItem.Hierarchy.Items[rowItem.Hierarchy.Items.Count - 1];
        }
      }
      else if ((e.KeyCode == Keys.Prior || e.KeyCode == Keys.Next) && rowItem != null)
      {
        int y = this.Bounds.Height;
        if (e.KeyCode == Keys.Prior)
          y = -y;
        if (this.RowsHierarchy.Items.Count > 0 && this.ColumnsHierarchy.Items.Count > 0 && !rowItem.IsColumnsHierarchyItem)
        {
          GridCell cell = new GridCell(rowItem, this.ColumnsHierarchy.Items[0], this.CellsArea);
          GridCell cellAtDistance = this.cellsArea.GetCellAtDistance(cell, new Point(5, y));
          if (cellAtDistance != null)
            hierarchyItem = cellAtDistance.RowItem;
          if (cellAtDistance == null)
          {
            HierarchyItem parentItem = rowItem.ParentItem;
            if (parentItem != null)
            {
              if (parentItem.Items.Count > 0)
                hierarchyItem = e.KeyCode == Keys.Prior ? parentItem.Items[0] : parentItem.Items[parentItem.Items.Count - 1];
              else if (parentItem.SummaryItems.Items.Count > 0)
                hierarchyItem = e.KeyCode == Keys.Prior ? parentItem.SummaryItems[0] : parentItem.SummaryItems[parentItem.Items.Count - 1];
            }
            else
              hierarchyItem = e.KeyCode == Keys.Prior ? rowItem.Hierarchy.Items[0] : rowItem.Hierarchy.Items[rowItem.Hierarchy.Items.Count - 1];
          }
          else if (cellAtDistance.ColumnItem.Caption.Equals(cell.ColumnItem.Caption))
            hierarchyItem = cellAtDistance.RowItem;
        }
      }
      if (hierarchyItem == null)
        return false;
      if (e.Modifiers != Keys.Control)
        hierarchy.ClearSelection();
      if (e.Modifiers != Keys.Shift || !this.multipleSelectionEnabled)
      {
        if (this.selectionMode == vDataGridView.SELECTION_MODE.FULL_ROW_SELECT)
        {
          this.rowItemRangeSelectionBeg = hierarchyItem;
          this.rowItemRangeSelectionEnd = hierarchyItem;
        }
        else
        {
          this.colItemRangeSelectionBeg = hierarchyItem;
          this.colItemRangeSelectionEnd = hierarchyItem;
        }
      }
      else if (this.selectionMode == vDataGridView.SELECTION_MODE.FULL_ROW_SELECT)
      {
        if (this.rowItemRangeSelectionBeg == null)
          this.rowItemRangeSelectionBeg = hierarchyItem;
        this.rowItemRangeSelectionEnd = hierarchyItem;
      }
      else
      {
        if (this.colItemRangeSelectionBeg == null)
          this.colItemRangeSelectionBeg = hierarchyItem;
        this.colItemRangeSelectionEnd = hierarchyItem;
      }
      this.ApplyItemsMultiSelect();
      this.EnsureVisible(hierarchyItem);
      this.Refresh();
      return true;
    }

    private List<GridCell> GetCellsInKBSelectionRange()
    {
      List<GridCell> gridCellList = new List<GridCell>();
      if (this.cellKBRangeSelectionStart == null)
        return gridCellList;
      if (this.cellKBRangeSelectionEnd == null)
      {
        this.cellKBRangeSelectionEnd = this.cellKBRangeSelectionStart;
        gridCellList.Add(this.cellKBRangeSelectionEnd);
        return gridCellList;
      }
      GridCell gridCell1 = this.cellKBRangeSelectionStart.Bounds.X <= this.cellKBRangeSelectionEnd.Bounds.X ? this.cellKBRangeSelectionStart : this.cellKBRangeSelectionEnd;
      GridCell gridCell2 = gridCell1 == this.cellKBRangeSelectionStart ? this.cellKBRangeSelectionEnd : this.cellKBRangeSelectionStart;
      GridCell gridCell3 = this.cellKBRangeSelectionStart.Bounds.Y <= this.cellKBRangeSelectionEnd.Bounds.Y ? this.cellKBRangeSelectionStart : this.cellKBRangeSelectionEnd;
      GridCell gridCell4 = gridCell3 == this.cellKBRangeSelectionStart ? this.cellKBRangeSelectionEnd : this.cellKBRangeSelectionStart;
      GridCell cell1;
      GridCell cellAbove;
      for (cell1 = gridCell1; cell1.Bounds.Y > gridCell3.Bounds.Y; cell1 = cellAbove)
      {
        cellAbove = this.CellsArea.GetCellAbove(cell1);
        if (cellAbove == null || cellAbove == cell1)
          return gridCellList;
      }
      GridCell cellRight;
      for (GridCell cell2 = cell1; cell2 != null && cell2.Bounds.X <= gridCell2.Bounds.X; cell2 = cellRight)
      {
        cellRight = this.CellsArea.GetCellRight(cell2);
        if (cellRight == null || !cellRight.IsSameAs(cell2))
        {
          GridCell cellBelow;
          for (; cell2 != null && cell2.Bounds.Y <= gridCell4.Bounds.Y; cell2 = cellBelow)
          {
            gridCellList.Add(cell2);
            cellBelow = this.CellsArea.GetCellBelow(cell2);
            if (cellBelow == null || cellBelow.IsSameAs(cell2))
              break;
          }
          if (cellRight == null || cell2.IsSameAs(cellRight))
            break;
        }
        else
          break;
      }
      return gridCellList;
    }

    /// <summary>Clears the DataGrid data.</summary>
    public void Clear()
    {
      this.BoundFields.Clear();
      this.GroupingColumns.Clear();
      this.BoundPivotColumns.Clear();
      this.BoundPivotRows.Clear();
      this.DataSource = (object) null;
      this.columnsHierarchy.Items.Clear();
      this.rowsHierarchy.Items.Clear();
      this.cellsArea.Clear();
      this.columnsHierarchy = new ColumnsHierarchy(this);
      this.rowsHierarchy = new RowsHierarchy(this);
      this.cellsArea = new CellsArea(this);
      this.Refresh();
    }

    /// <summary>Called when the context menu is showing.</summary>
    protected virtual void OnContextMenuShowing(object sender, CancelEventArgs args)
    {
      if (this.ContextMenuShowing == null)
        return;
      this.ContextMenuShowing(sender, args);
    }

    /// <summary>Called when the context menu is closed.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="args">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnContextMenuClosed(object sender, EventArgs args)
    {
      if (this.ContextMenuClosed == null)
        return;
      this.ContextMenuClosed(sender, args);
    }

    /// <summary>Hides the column chooser.</summary>
    public void HideColumnChooser()
    {
      if (this.columnChooser == null)
        return;
      this.columnChooser.Close();
      this.columnChooser = (ColumnChooser) null;
    }

    /// <summary>Shows the column chooser.</summary>
    public void ShowColumnChooser()
    {
      this.ShowColumnChooser(this.ColumnChooser.Location);
    }

    /// <summary>Shows the column chooser at a specific location.</summary>
    public void ShowColumnChooser(Point location)
    {
      if (!this.EnableColumnChooser)
        return;
      this.Capture = false;
      if (this.columnChooser != null)
        this.columnChooser.Close();
      this.columnChooser = (ColumnChooser) null;
      Control control = (Control) this;
      this.ColumnChooser.Location = location;
      this.ColumnChooser.Show((IWin32Window) control);
      this.ColumnChooser.Location = location;
    }

    private void menuItemColumnChooser_Click(object sender, EventArgs e)
    {
      if (this.columnChooser != null)
      {
        this.columnChooser.Location = this.PointToScreen(this.currentPosition);
        this.columnChooser.Location.Offset(10, 10);
      }
      this.ShowColumnChooser();
    }

    private void timerScroll_Tick(object sender, EventArgs e)
    {
      this.timerScroll.Stop();
      this.Refresh();
    }

    private void GridView_SizeChanged(object sender, EventArgs e)
    {
      this.rowsHierarchy.PreRenderRequired = true;
      this.columnsHierarchy.PreRenderRequired = true;
      this.SynchronizeScrollBars();
      this.Refresh();
    }

    private void gridToolTip_Disposed(object sender, EventArgs e)
    {
      this.enableToolTips = false;
    }

    /// <exclude />
    protected internal virtual void OnToolTipShown(GridToolTipEventArgs args)
    {
      if (!this.enableToolTips || args == null || args.RowItem == null && args.ColumnItem == null)
        return;
      this.isTooltipCursorChange = true;
      if (this.TooltipShow != null)
        this.TooltipShow((object) this, args);
      this.gridToolTip.RemoveAll();
      Point client = this.PointToClient(Cursor.Position);
      if (this.activeEditor.Editor != null)
        return;
      if (args.ColumnItem != null && args.RowItem != null)
      {
        GridCell gridCell = this.CellsArea.HitTest(client);
        if (gridCell == null || this.ItemToSpanItem(gridCell.RowItem) != args.RowItem || this.ItemToSpanItem(gridCell.ColumnItem) != args.ColumnItem)
          return;
      }
      if (args.RowItem != null && args.ColumnItem == null && this.ItemToSpanItem(this.RowsHierarchy.HitTest(client)) != args.RowItem || args.ColumnItem != null && args.RowItem == null && this.ItemToSpanItem(this.ColumnsHierarchy.HitTest(client)) != args.ColumnItem)
        return;
      client.Y += 25;
      string text = "";
      if (args.Handled)
        text = args.ToolTipText;
      else if (args.ColumnItem != null && args.RowItem != null)
      {
        object cellValue = this.CellsArea.GetCellValue(args.RowItem, args.ColumnItem);
        string str = cellValue != null ? cellValue.ToString() : "";
        text = string.Format("Row: {0}\nColumn: {1}\nValue: {2}", (object) args.RowItem.Caption, (object) args.ColumnItem.Caption, (object) str);
      }
      else if (args.ColumnItem != null)
        text = args.ColumnItem.TooltipText;
      else if (args.RowItem != null)
        text = args.RowItem.TooltipText;
      if (string.IsNullOrEmpty(text))
        return;
      this.gridToolTip.Show(text, (IWin32Window) this, client, this.toolTipHideDelay);
    }

    private HierarchyItem ItemToSpanItem(HierarchyItem item)
    {
      if (item == null)
        return (HierarchyItem) null;
      CellSpan itemSpanGroup = this.cellsArea.GetItemSpanGroup(item);
      if (item.IsColumnsHierarchyItem)
        return itemSpanGroup.ColumnItem;
      return itemSpanGroup.RowItem;
    }

    private void OnCursorChanged(object sender, EventArgs e)
    {
      if (!this.isTooltipCursorChange)
        return;
      this.isTooltipCursorChange = false;
      this.UpdateCursor(this.currentCursorType);
    }

    /// <exclude />
    protected override void OnNotifyMessage(Message m)
    {
      base.OnNotifyMessage(m);
    }

    /// <summary>Gets the height of the gridview control</summary>
    public int GetHeight()
    {
      return this.rectNow.Height;
    }

    /// <summary>Gets the width of the gridview control</summary>
    public int GetWidth()
    {
      return this.rectNow.Width;
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.gridToolTip.Dispose();
        this.timerToolTip.Dispose();
        this.timerScroll.Dispose();
        if (this.resizeToolTip != null)
        {
          this.resizeToolTip.Dispose();
          this.resizeToolTip = (ToolTip) null;
        }
        this.GroupingColumns.CollectionChanged -= new EventHandler<CollectionChangedEventArgs>(this.GroupingColumns_CollectionChanged);
        if (this.components != null)
          this.components.Dispose();
        if (this.columnChooser != null)
        {
          this.columnChooser.Dispose();
          this.columnChooser = (ColumnChooser) null;
        }
      }
      base.Dispose(disposing);
    }

    private void GroupingColumns_CollectionChanged(object sender, CollectionChangedEventArgs e)
    {
      if (this.suspendGroupingColumnsCollectionChanged)
        return;
      this.Graphics = (Graphics) null;
      this.PaintSuspended = true;
      this.DataBind();
      this.ColumnsHierarchy.AutoResize();
      this.PaintSuspended = false;
      this.Refresh();
    }

    internal HierarchyItem HitTest(Point point, ref bool isOnItemButton)
    {
      isOnItemButton = false;
      HierarchyItem hierarchyItem = (HierarchyItem) null;
      if (this.RowsHierarchy.Visible)
        hierarchyItem = this.rowsHierarchy.HitTest(point);
      if (hierarchyItem == null && this.ColumnsHierarchy.Visible)
        hierarchyItem = this.columnsHierarchy.HitTest(point);
      if (hierarchyItem != null && hierarchyItem.ItemsCount != 0 && (hierarchyItem.ExpandCollapseEnabled && hierarchyItem.HierarchyHost.ShowExpandCollapseButtons))
      {
        int num1 = hierarchyItem.X + (hierarchyItem.IsColumnsHierarchyItem ? this.ColumnsHierarchy.X : this.RowsHierarchy.X);
        int num2 = hierarchyItem.Y + (hierarchyItem.IsColumnsHierarchyItem ? this.ColumnsHierarchy.Y : this.RowsHierarchy.Y);
        if (!hierarchyItem.IsColumnsHierarchyItem && this.RowsHierarchy.CompactStyleRenderingEnabled)
        {
          int num3 = 0;
          if (hierarchyItem.ParentItem != null)
            num3 = this.RowsHierarchy.CompactStyleRenderingItemsIndent * hierarchyItem.itemLevel;
          num1 += num3;
        }
        if (point.X >= num1 + 1 && point.X <= num1 + 11 && (point.Y >= num2 + 3 && point.Y <= num2 + 11))
          isOnItemButton = true;
      }
      return hierarchyItem;
    }

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new Container();
      this.Size = new Size(200, 200);
    }

    private void UpdateActiveEditorLayout()
    {
      if (this.activeEditor == null || this.activeEditor.Editor == null || (this.activeEditor.ColumnItem == null || this.activeEditor.RowItem == null))
        return;
      this.activeEditor.Editor.LayoutEditor(this.activeEditor.Cell);
    }

    private void SetupScrollArea()
    {
      int num = this.ColumnsHierarchy.Visible ? this.columnsHierarchy.Height : 0;
      if (this.ColumnsHierarchy.Visible)
      {
        int width = this.columnsHierarchy.Width;
      }
      if (this.RowsHierarchy.Visible)
      {
        int height = this.rowsHierarchy.Height;
      }
      Size size = new Size(this.offsetX + (this.columnsHierarchy.VisibleLeafItemsCount > 0 ? this.columnsHierarchy.Width : 0) + (this.RowsHierarchy.Visible ? this.rowsHierarchy.Width : 0), this.offsetY + (this.rowsHierarchy.VisibleLeafItemsCount > 0 ? this.rowsHierarchy.Height : 0) + num);
      if (this.rowsHierarchy.VisibleLeafItemsCount == 0 && !this.columnsHierarchy.Visible)
        size = new Size(0, 0);
      if (this.columnsHierarchy.VisibleLeafItemsCount == 0 && !this.rowsHierarchy.Visible)
        size = new Size(0, 0);
      if (vDataGridView.isDisabled && this.ScrollBarsEnabled)
        this.ScrollBarsEnabled = false;
      if (!(this.scrollableAreaSize != size) && !this.isSyncScrollRequired)
        return;
      this.isSyncScrollRequired = true;
      this.scrollableAreaSize = size;
      this.SynchronizeScrollBars();
    }

    /// <summary>Renders the content of the Data Grid Control</summary>
    public void Render()
    {
      this.PreRender();
    }

    private void PreRender()
    {
      if (this.ColumnsHierarchy.ColumnsCountRequresUpdate)
      {
        this.ColumnsHierarchy.UpdateColumnsCount();
        this.ColumnsHierarchy.UpdateColumnsIndexes();
      }
      if (this.RowsHierarchy.ColumnsCountRequresUpdate)
      {
        this.RowsHierarchy.UpdateColumnsCount();
        this.RowsHierarchy.UpdateColumnsIndexes();
      }
      Rectangle clientRectangle = this.ClientRectangle;
      Rectangle rectangle = clientRectangle;
      rectangle.Offset(this.offsetX, this.offsetY);
      this.rectNow = rectangle;
      if (this.columnsHierarchy.AutoStretchColumns)
        this.columnsHierarchy.Resize();
      if (this.columnsHierarchy.PreRenderRequired)
      {
        this.columnsHierarchy.PreRender();
        this.isSyncScrollRequired = true;
      }
      if (this.rowsHierarchy.PreRenderRequired)
      {
        this.rowsHierarchy.PreRender();
        this.isSyncScrollRequired = true;
      }
      int num1 = this.ColumnsHierarchy.Visible ? this.columnsHierarchy.Height : 0;
      if (this.ColumnsHierarchy.Visible)
      {
        int width = this.columnsHierarchy.Width;
      }
      if (this.RowsHierarchy.Visible)
      {
        int height = this.rowsHierarchy.Height;
      }
      int num2 = this.RowsHierarchy.Visible ? this.rowsHierarchy.Width : 0;
      this.SetupScrollArea();
      int num3 = -this.ScrollOffset.Y;
      int num4 = -this.ScrollOffset.X;
      this.RowsHierarchy.X = clientRectangle.Left - num4;
      this.RowsHierarchy.Y = clientRectangle.Top - num3 + num1;
      this.ColumnsHierarchy.X = clientRectangle.Left - num4 + num2;
      this.ColumnsHierarchy.Y = clientRectangle.Top - num3;
      this.cellsArea.Bounds = new Rectangle(this.ColumnsHierarchy.X, this.RowsHierarchy.Y, this.rectNow.Width, this.rectNow.Height);
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.paintSuspend)
        return;
      this.paintSuspend = true;
      base.OnPaint(e);
      Brush brush = (Brush) new SolidBrush(this.BackColor);
      e.Graphics.FillRectangle(brush, this.ClientRectangle);
      this.Graphics = e.Graphics;
      this.Graphics.SmoothingMode = SmoothingMode.Default;
      if (this.GroupingEnabled && this.groupingColumns.Count > 0)
      {
        this.rowsHierarchy.CompactStyleRenderingEnabled = true;
        this.rowsHierarchy.Visible = false;
      }
      this.PreRender();
      if (!vDataGridView.isDisabled)
      {
        this.cellsArea.DrawMatrixData(e.Graphics);
        if (this.RowsHierarchy.Visible)
        {
          this.RowsHierarchy.Graphics = e.Graphics;
          this.rowsHierarchy.Draw();
        }
        if (this.ColumnsHierarchy.Visible)
        {
          this.ColumnsHierarchy.Graphics = e.Graphics;
          this.columnsHierarchy.Draw();
        }
      }
      if (this.vScroll.Visible && this.hScroll.Visible)
        e.Graphics.FillRectangle(brush, new Rectangle(this.Width - this.vScroll.Width, this.Height - this.hScroll.Height, this.vScroll.Width, this.hScroll.Width));
      if (this.RowsHierarchy.Visible && this.ColumnsHierarchy.Visible && (this.RowsHierarchy.Fixed || this.ColumnsHierarchy.Fixed))
        e.Graphics.FillRectangle(brush, new Rectangle(this.RowsHierarchy.X, this.ColumnsHierarchy.Y, this.RowsHierarchy.Width, this.ColumnsHierarchy.Height));
      this.UpdateActiveEditorLayout();
      if (this.dragSourceItem != null && this.dragBtn != null)
      {
        e.Graphics.DrawImage(this.dragBtn.Image, this.PointToClient(Cursor.Position));
        this.DoDrag(this.lastMouseMovePt, e.Graphics);
      }
      this.paintSuspend = false;
      if (this.ShowBorder && this.Theme != null && this.Theme.HierarchyItemStyleNormal != null)
      {
        Color borderColor = this.Theme.HierarchyItemStyleNormal.BorderColor;
        if (this.BorderColor != Color.Empty)
          borderColor = this.BorderColor;
        using (Pen pen = new Pen(borderColor))
        {
          Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
          e.Graphics.DrawRectangle(pen, rect);
        }
      }
      brush.Dispose();
    }

    private void LicenseValidation()
    {
      if (this.DesignMode)
        return;
      Licensing.LICheck((Control) this);
    }

    private void RefreshMouseCursor()
    {
      if (this.IsDisposed)
        return;
      if (this.dragSourceItem != null)
      {
        this.UpdateCursor(vDataGridView.CURSOR_TYPE.HAND);
      }
      else
      {
        Point client = this.PointToClient(Cursor.Position);
        client.Offset(-this.offsetX, -this.offsetY);
        HierarchyItem hierarchyItem = this.RowsHierarchy.HitTest(client) ?? this.ColumnsHierarchy.HitTest(client);
        if (hierarchyItem != null)
          this.ItemMouseMove(client, hierarchyItem);
        else if (this.resizingItem != null)
          this.UpdateCursor(this.isHorizontalResize ? vDataGridView.CURSOR_TYPE.COLUMN_RESIZE : vDataGridView.CURSOR_TYPE.ROW_RESIZE);
        else
          this.UpdateCursor(vDataGridView.CURSOR_TYPE.ARROW);
      }
    }

    internal void SynchronizeScrollBars()
    {
      if (!this.isSyncScrollRequired)
        return;
      bool flag1 = false;
      bool flag2 = false;
      if (this.scrollableAreaSize.Height > this.Height)
        flag1 = true;
      if (this.scrollableAreaSize.Width > this.Width)
        flag2 = true;
      if (this.scrollableAreaSize.Height <= this.Height && this.scrollableAreaSize.Width <= this.Width)
      {
        flag1 = false;
        flag2 = false;
      }
      if (flag1)
        flag2 = flag2 || this.scrollableAreaSize.Width > this.Width - 15;
      else
        this.vScroll.Value = 0;
      if (flag2)
        flag1 = flag1 || this.scrollableAreaSize.Height > this.Height - 15;
      else
        this.hScroll.Value = 0;
      if (flag1)
      {
        this.vScroll.Location = new Point(this.Width - 15, 0);
        this.vScroll.Height = this.Height - (flag2 ? 15 : 0);
        this.vScroll.Width = 15;
        this.vScroll.Minimum = 0;
        int num = this.ScrollableAreaSize.Height - (this.Height - (flag2 ? 15 : 0));
        if (num >= 0)
          this.vScroll.Maximum = num;
      }
      else
        this.vScroll.Minimum = this.vScroll.Maximum = 0;
      if (flag2)
      {
        this.hScroll.Location = new Point(0, this.Height - 15);
        this.hScroll.Width = this.Width - (flag1 ? 15 : 0);
        this.hScroll.Height = 15;
        int num = this.ScrollableAreaSize.Width - (this.Width - (flag1 ? 15 : 0));
        this.hScroll.Minimum = 0;
        if (num >= 0)
          this.hScroll.Maximum = num;
      }
      else
        this.hScroll.Minimum = this.hScroll.Maximum = 0;
      if (this.hScroll.LargeChange != this.HorizontalScrollBarLargeChange)
        this.hScroll.LargeChange = this.HorizontalScrollBarLargeChange;
      if (this.hScroll.SmallChange != this.HorizontalScrollBarSmallChange)
        this.hScroll.SmallChange = this.HorizontalScrollBarSmallChange;
      if (this.vScroll.SmallChange != this.VerticalScrollBarSmallChange)
        this.vScroll.SmallChange = this.VerticalScrollBarSmallChange;
      if (this.vScroll.LargeChange != this.VerticalScrollBarLargeChange)
        this.vScroll.LargeChange = this.VerticalScrollBarLargeChange;
      bool flag3 = flag2 && this.scrollBarsEnabled;
      bool flag4 = flag1 && this.scrollBarsEnabled;
      if (flag3 != this.hScroll.Visible)
        this.hScroll.Visible = flag3;
      if (flag4 != this.vScroll.Visible)
        this.vScroll.Visible = flag4;
      if (this.hScroll.Visible)
        this.hScroll.Refresh();
      if (this.vScroll.Visible)
        this.vScroll.Refresh();
      this.isSyncScrollRequired = false;
    }

    /// <summary>
    /// Raises the <see cref="E:VerticalScrollBarValueChanged" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:System.Windows.Forms.ScrollEventArgs" /> instance containing the event data.</param>
    protected virtual void OnVerticalScrollBarValueChanged()
    {
      if (this.VerticalScrollBarValueChanged == null)
        return;
      this.VerticalScrollBarValueChanged((object) this, EventArgs.Empty);
    }

    /// <summary>
    /// Raises the <see cref="E:HorizontalScrollBarValueChanged" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:System.Windows.Forms.ScrollEventArgs" /> instance containing the event data.</param>
    protected virtual void OnHorizontalScrollBarValueChanged()
    {
      if (this.HorizontalScrollBarValueChanged == null)
        return;
      this.HorizontalScrollBarValueChanged((object) this, EventArgs.Empty);
    }

    private void vScroll_ValueChanged(object sender, EventArgs e)
    {
      if (this.scrollOffset.Y == -this.vScroll.Value)
        return;
      this.scrollOffset.Y = -this.vScroll.Value;
      this.timerScroll.Stop();
      this.timerScroll.Start();
      this.Invalidate();
      this.vScroll.Refresh();
      this.OnVerticalScrollBarValueChanged();
    }

    private void hScroll_ValueChanged(object sender, EventArgs e)
    {
      if (this.scrollOffset.X == -this.hScroll.Value)
        return;
      this.scrollOffset.X = -this.hScroll.Value;
      this.timerScroll.Stop();
      this.timerScroll.Start();
      this.hScroll.Refresh();
      this.Invalidate();
      this.OnHorizontalScrollBarValueChanged();
    }

    private void RefreshScrollBars()
    {
      this.hScroll.Refresh();
      this.vScroll.Refresh();
    }

    private bool LoadCrossCursor()
    {
      Assembly assembly = Assembly.GetAssembly(typeof (vDataGridView));
      foreach (string manifestResourceName in assembly.GetManifestResourceNames())
      {
        if (manifestResourceName.Contains("CROSSCURSOR.CUR"))
        {
          using (Stream manifestResourceStream = assembly.GetManifestResourceStream(manifestResourceName))
          {
            try
            {
              this.crossCursor = new Cursor(manifestResourceStream);
              break;
            }
            catch (ArgumentException)
            {
              manifestResourceStream.Position = 0L;
            }
          }
        }
      }
      return true;
    }

    /// <exclude />
    protected void UpdateCursor(vDataGridView.CURSOR_TYPE cursorType)
    {
      switch (cursorType)
      {
        case vDataGridView.CURSOR_TYPE.ARROW:
          if (Cursor.Current != Cursors.Arrow)
          {
            Cursor.Current = Cursors.Arrow;
            break;
          }
          break;
        case vDataGridView.CURSOR_TYPE.CROSS:
          Cursor.Current = this.crossCursor != (Cursor) null ? this.crossCursor : Cursors.Arrow;
          break;
        case vDataGridView.CURSOR_TYPE.COLUMN_RESIZE:
          if (Cursor.Current != Cursors.VSplit)
          {
            Cursor.Current = Cursors.VSplit;
            break;
          }
          break;
        case vDataGridView.CURSOR_TYPE.ROW_RESIZE:
          if (Cursor.Current != Cursors.HSplit)
          {
            Cursor.Current = Cursors.HSplit;
            break;
          }
          break;
        case vDataGridView.CURSOR_TYPE.HAND:
          if (Cursor.Current != Cursors.Hand)
          {
            Cursor.Current = Cursors.Hand;
            break;
          }
          break;
        default:
          if (Cursor.Current != Cursors.Arrow)
          {
            Cursor.Current = Cursors.Arrow;
            break;
          }
          break;
      }
      this.currentCursorType = cursorType;
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (this.activeEditor != null && this.activeEditor.Editor != null)
        return;
      if (this.vScroll.Maximum < this.vScroll.Value - e.Delta)
        this.vScroll.Value = this.vScroll.Maximum;
      else if (this.vScroll.Minimum > this.vScroll.Value - e.Delta)
        this.vScroll.Value = this.vScroll.Minimum;
      else
        this.vScroll.Value -= e.Delta;
      this.Invalidate();
    }

    private HierarchyItem HitTest(Point pt, bool searchRow, bool previous, HierarchyItem column, HierarchyItem row)
    {
      List<HierarchyItem> pv1 = new List<HierarchyItem>();
      this.rowsHierarchy.GetVisibleLeafLevelItems(ref pv1);
      List<HierarchyItem> pv2 = new List<HierarchyItem>();
      this.columnsHierarchy.GetVisibleLeafLevelItems(ref pv2);
      bool flag1 = false;
      HierarchyItem hierarchyItem1 = (HierarchyItem) null;
      foreach (HierarchyItem hierarchyItem2 in pv1)
      {
        bool flag2 = false;
        if (searchRow)
        {
          if (hierarchyItem2 != row && !flag1)
            hierarchyItem1 = hierarchyItem2;
          else if (!flag1)
            flag1 = true;
          else if (flag1)
          {
            if (!previous)
              return hierarchyItem2;
            return hierarchyItem1;
          }
        }
        else
        {
          foreach (HierarchyItem hierarchyItem3 in pv2)
          {
            if (hierarchyItem3 != column && !flag2)
              hierarchyItem1 = hierarchyItem3;
            else if (!flag2)
              flag2 = true;
            else if (flag2)
            {
              if (!previous)
                return hierarchyItem3;
              return hierarchyItem1;
            }
          }
        }
      }
      if (previous)
        return hierarchyItem1;
      return (HierarchyItem) null;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
    }

    /// <exclude />
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (!this.HandleKeyDown(new KeyEventArgs(keyData)) || this.activeEditor.Editor != null)
        return base.ProcessCmdKey(ref msg, keyData);
      this.OnKeyDown(new KeyEventArgs(keyData));
      return true;
    }

    private void GetSelectedShape()
    {
      ArrayList arrayList1 = new ArrayList();
      ArrayList arrayList2 = new ArrayList();
      foreach (GridCell selectedCell in this.cellsArea.SelectedCells)
      {
        if (!arrayList1.Contains((object) selectedCell.RowItem))
          arrayList1.Add((object) selectedCell.RowItem);
        if (!arrayList2.Contains((object) selectedCell.ColumnItem))
          arrayList2.Add((object) selectedCell.ColumnItem);
      }
      this.selectedRowsCount = arrayList1.Count;
      this.selectedColumnsCount = arrayList2.Count;
    }

    private bool HandleKeyDown(KeyEventArgs e)
    {
      if (e.Alt)
        return false;
      e.Handled = true;
      GridCell cell = (GridCell) null;
      if (this.CellsArea.SelectedCellsCount > 0)
        cell = this.CellsArea.SelectedCells[0];
      this.OnCellKeyDown(new vDataGridView.CellKeyEventArgs(cell, e));
      bool flag = true;
      if (e.Control)
        this.isCTRLPressed = true;
      if (e.Shift)
        this.isShiftPressed = true;
      if (e.KeyCode == Keys.Delete)
        return false;
      if (e.KeyData == (Keys.C | Keys.Control) && this.cellsArea.SelectedCells.Length > 0)
      {
        if (this.keysList.Count > 0)
        {
          int num = (int) MessageBox.Show("That command cannot be used on multiple selections", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          return true;
        }
        if (this.allowCopyPaste)
          this.CopySelectedDataToClipBoard();
      }
      if (e.KeyData == (Keys.X | Keys.Control) && this.cellsArea.SelectedCells.Length > 0)
      {
        if (this.keysList.Count > 0)
        {
          int num = (int) MessageBox.Show("That command cannot be used on multiple selections", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
          this.isCTRLPressed = false;
          this.isShiftPressed = false;
          return true;
        }
        if (this.allowCopyPaste)
        {
          this.cutOperation = true;
          this.CutSelectedDataToClipBoard();
          this.Invalidate();
        }
      }
      if (this.allowCopyPaste)
        this.PasteDataFromClipBoard(e);
      if (this.activeEditor.Editor == null)
      {
        if (this.CellsArea.SelectedCells.Length > 0 && this.cellsArea.GetCellEditor(this.CellsArea.SelectedCells[0].RowItem, this.CellsArea.SelectedCells[0].ColumnItem) is CheckBoxEditor)
        {
          bool closeEditor;
          bool closeEditorAndSaveChanges;
          this.CellsArea.SelectedCells[0].Editor.OnGridKeyDown(e, this.CellsArea.SelectedCells[0], out closeEditor, out closeEditorAndSaveChanges);
        }
        this.gridToolTip.Hide((IWin32Window) this);
        this.HandleKeyboardNavigation(e);
        Cursor.Show();
      }
      else
      {
        GridCell editCell = this.cellsArea.EditCell;
        bool closeEditor;
        bool closeEditorAndSaveChanges;
        editCell.Editor.OnGridKeyDown(e, editCell, out closeEditor, out closeEditorAndSaveChanges);
        if (e.KeyCode == Keys.Escape && this.HideEditor(EditorActivationFlags.KEY_PRESS_ESC, new KeyEventArgs(Keys.Escape), false))
        {
          this.cellsArea.SelectCell(editCell.RowItem, editCell.ColumnItem);
          this.Invalidate();
          return true;
        }
        if (e.KeyCode == Keys.Return && this.HideEditor(EditorActivationFlags.KEY_PRESS_ENTER, new KeyEventArgs(Keys.Return), false))
        {
          this.cellsArea.SelectCell(editCell.RowItem, editCell.ColumnItem);
          this.Invalidate();
          return true;
        }
        if (closeEditor)
        {
          if (!closeEditorAndSaveChanges)
          {
            this.CloseEditor(true);
            this.cellsArea.SelectCell(editCell.RowItem, editCell.ColumnItem);
            this.Invalidate();
            return true;
          }
          this.CloseEditor(false);
          this.cellsArea.SelectCell(editCell.RowItem, editCell.ColumnItem);
          this.Invalidate();
          return true;
        }
      }
      if (this.cellsArea.SelectedCells.Length == 1 && e.KeyValue != 0)
      {
        GridCell gridCell = this.cellsArea.SelectedCells[0];
        if (this.cellsArea.GetCellEditor(gridCell.RowItem, gridCell.ColumnItem) != null && this.activeEditor.Editor == null)
        {
          EditorActivationFlags activationFlag = EditorActivationFlags.KEY_PRESS;
          if (e.KeyCode == Keys.Return)
            activationFlag |= EditorActivationFlags.KEY_PRESS_ENTER;
          if (e.KeyCode == Keys.Escape)
            activationFlag |= EditorActivationFlags.KEY_PRESS_ESC;
          this.ShowEditor(gridCell.RowItem, gridCell.ColumnItem, activationFlag, e);
          if (this.activeEditor.Editor != null)
            this.cellsArea.ClearSelection();
          flag = true;
        }
      }
      return flag;
    }

    private void PasteDataFromClipBoard(KeyEventArgs e)
    {
      if ((!e.Shift || e.KeyCode != Keys.Insert) && (!e.Control || e.KeyCode != Keys.V) || this.cellsArea.SelectedCells.Length == 0)
        return;
      char[] separator = new char[2]{ '\r', '\n' };
      char[] chArray = new char[1]{ '\t' };
      string str = (string) Clipboard.GetDataObject().GetData(DataFormats.Text);
      if (str == null)
        return;
      string[] strArray1 = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
      int absoluteIndex1 = this.cellsArea.SelectedCells[0].RowItem.GetAbsoluteIndex();
      int absoluteIndex2 = this.cellsArea.SelectedCells[0].ColumnItem.GetAbsoluteIndex();
      this.ClearSelection();
      List<HierarchyItem> pv1 = new List<HierarchyItem>();
      this.rowsHierarchy.GetVisibleLeafLevelItems(ref pv1);
      List<HierarchyItem> pv2 = new List<HierarchyItem>();
      this.columnsHierarchy.GetVisibleLeafLevelItems(ref pv2);
      for (int index1 = 0; index1 < strArray1.Length; ++index1)
      {
        string[] strArray2 = strArray1[index1].Split(chArray);
        for (int index2 = 0; index2 < strArray2.Length; ++index2)
        {
          if (pv2.Count - 1 >= absoluteIndex2 + index2 && pv1.Count - 1 >= index1 + absoluteIndex1 && pv2.Count - 1 > -index2 + absoluteIndex2)
          {
            GridCell gridCell = new GridCell(pv1[index1 + absoluteIndex1], pv2[index2 + absoluteIndex2], this.cellsArea);
            this.cellsArea.SelectCellInternal(gridCell.RowItem, gridCell.ColumnItem);
            gridCell.Value = (object) strArray2[index2];
          }
        }
      }
      if (this.cutOperation)
      {
        Clipboard.Clear();
        this.cutOperation = false;
      }
      this.Invalidate();
    }

    private void DeleteSelectedData()
    {
      foreach (GridCell selectedCell in this.cellsArea.SelectedCells)
        selectedCell.Value = (object) "";
    }

    private void CutSelectedDataToClipBoard()
    {
      this.CopySelectedDataToClipBoard();
      this.DeleteSelectedData();
    }

    private void CopySelectedDataToClipBoard()
    {
      this.GetSelectedShape();
      DataObject dataObject = new DataObject();
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      int val2_1 = int.MaxValue;
      int val2_2 = int.MaxValue;
      int val2_3 = 0;
      foreach (GridCell selectedCell in this.cellsArea.SelectedCells)
      {
        val2_1 = Math.Min(selectedCell.RowItem.GetAbsoluteIndex(), val2_1);
        val2_2 = Math.Min(selectedCell.ColumnItem.GetAbsoluteIndex(), val2_2);
        val2_3 = Math.Max(selectedCell.ColumnItem.GetAbsoluteIndex(), val2_3);
      }
      ImageList imageList = new ImageList();
      for (int index1 = val2_1; index1 < this.selectedRowsCount + val2_1; ++index1)
      {
        for (int index2 = val2_2; index2 < this.selectedColumnsCount + val2_2; ++index2)
        {
          foreach (GridCell selectedCell in this.cellsArea.SelectedCells)
          {
            int absoluteIndex1 = selectedCell.RowItem.GetAbsoluteIndex();
            int absoluteIndex2 = selectedCell.ColumnItem.GetAbsoluteIndex();
            if (absoluteIndex1 == index1 && absoluteIndex2 == index2)
            {
              HierarchyItem spanItem1 = this.ItemToSpanItem(selectedCell.RowItem);
              HierarchyItem spanItem2 = this.ItemToSpanItem(selectedCell.ColumnItem);
              if (selectedCell.ImageIndex >= 0)
              {
                Image cellImageInternal = this.CellsArea.GetCellImageInternal(spanItem1, spanItem2);
                if (cellImageInternal != null)
                  imageList.Images.Add(cellImageInternal);
              }
              stringBuilder1.Append(this.CellsArea.GetCellFormattedText(spanItem1, spanItem2, this.CellsArea.GetCellValue(spanItem1, spanItem2)));
              if (val2_3 != absoluteIndex2)
                stringBuilder1.Append('\t');
            }
          }
        }
        stringBuilder1.Append("\r\n");
      }
      dataObject.SetData(DataFormats.Text, true, (object) stringBuilder1.ToString());
      dataObject.SetData(DataFormats.Bitmap, true, (object) imageList);
      Clipboard.SetDataObject((object) dataObject, true);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
      if (e.Modifiers != Keys.Control)
        this.isCTRLPressed = false;
      if (e.Modifiers != Keys.Shift)
        this.isShiftPressed = false;
      GridCell cell = (GridCell) null;
      if (this.CellsArea.SelectedCellsCount > 0)
        cell = this.CellsArea.SelectedCells[0];
      this.OnCellKeyUp(new vDataGridView.CellKeyEventArgs(cell, e));
    }

    private void GetCellDimension(ArrayList visibleRowItems, ArrayList visibleColItems, GridCell cell, out int rowNum, out int colNum)
    {
      colNum = -1;
      rowNum = -1;
      for (int index1 = 0; index1 < visibleRowItems.Count; ++index1)
      {
        if (visibleRowItems[index1] as HierarchyItem == cell.RowItem)
          rowNum = index1;
        for (int index2 = 0; index2 < visibleColItems.Count; ++index2)
        {
          if (visibleColItems[index2] as HierarchyItem == cell.ColumnItem)
            colNum = index2;
        }
      }
    }

    private void DoResize(Point point)
    {
      if (!this.resizingItem.Hierarchy.AllowResize || this.columnsHierarchy.IsGroupingColumn(this.resizingItem))
        return;
      if (this.isHorizontalResize)
        this.UpdateCursor(vDataGridView.CURSOR_TYPE.COLUMN_RESIZE);
      else
        this.UpdateCursor(vDataGridView.CURSOR_TYPE.ROW_RESIZE);
      this.focusedItem = this.resizingItem;
      int x = this.resizingItem.X;
      int y = this.resizingItem.Y;
      if (!this.resizingItem.Fixed)
      {
        if (this.resizingItem.IsColumnsHierarchyItem)
        {
          x += this.ColumnsHierarchy.Visible ? this.ColumnsHierarchy.X : 0;
          y += this.ColumnsHierarchy.Visible ? this.ColumnsHierarchy.Y : 0;
        }
        else
        {
          x += this.RowsHierarchy.Visible ? this.RowsHierarchy.X : 0;
          y += this.RowsHierarchy.Visible ? this.RowsHierarchy.Y : 0;
        }
      }
      else if (this.resizingItem.IsColumnsHierarchyItem)
        x += this.RowsHierarchy.Visible ? this.RowsHierarchy.X + this.RowsHierarchy.Width : 0;
      else
        y += this.ColumnsHierarchy.Visible ? this.ColumnsHierarchy.Y + this.ColumnsHierarchy.Height : 0;
      if (this.isHorizontalResize)
      {
        int num = Math.Abs(point.X - x);
        if (point.X < x || num < HierarchyItem.MinimumWidth)
          return;
        Hierarchy hierarchyHost = this.resizingItem.HierarchyHost;
        this.resizingItem.Width = num;
        if (this.EnableResizeToolTip)
        {
          string text = "Width: " + (object) num + " pixels";
          Point position = Cursor.Position;
          Point point1 = this.PointToClient(position);
          point1 = new Point(point1.X, this.resizingItem.DrawBounds.Bottom);
          position.Offset(5, 5);
          this.resizeToolTip.Show(text, (IWin32Window) this, point1);
        }
        if (this.resizingItem.IsColumnsHierarchyItem)
        {
          this.columnsHierarchy.Resize();
        }
        else
        {
          this.rowsHierarchy.PreRenderRequired = true;
          this.rowsHierarchy.Resize();
        }
      }
      else
      {
        int num = Math.Abs(point.Y - y);
        if (point.Y < y || num < HierarchyItem.MinimumHeight)
          return;
        Hierarchy hierarchyHost = this.resizingItem.HierarchyHost;
        this.resizingItem.Height = num;
        if (this.EnableResizeToolTip)
        {
          string text = "Height: " + (object) num + " pixels";
          Point position = Cursor.Position;
          Point point1 = this.PointToClient(position);
          point1 = new Point(this.resizingItem.DrawBounds.Right, this.resizingItem.DrawBounds.Top);
          position.Offset(5, 5);
          this.resizeToolTip.Show(text, (IWin32Window) this, point1);
        }
        if (this.resizingItem.IsColumnsHierarchyItem)
          this.columnsHierarchy.Resize();
        else
          this.rowsHierarchy.Resize();
      }
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position) && !this.Focused && this.activeEditor.Editor != null)
      {
        if (this.activeEditor.Editor.Control == null)
          this.Focus();
        else if (!this.activeEditor.Editor.Control.Focused)
          this.Focus();
      }
      base.OnLostFocus(e);
    }

    private void scrollOnSelectionTimer_Tick(object sender, EventArgs e)
    {
      Point client = this.PointToClient(Cursor.Position);
      this.OnMouseMove(new MouseEventArgs(MouseButtons.Left, 1, client.X, client.Y, 0));
    }

    /// <exclude />
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      GridCell cell1 = this.cellMouseMove;
      this.cellMouseMove = (GridCell) null;
      if (vDataGridView.isDisabled)
        return;
      if (this.lastMouseMovePt == e.Location)
      {
        this.RefreshMouseCursor();
      }
      else
      {
        this.gridToolTip.Hide((IWin32Window) this);
        this.lastMouseMovePt = e.Location;
        this.DoDrag(e.Location, (Graphics) null);
        if (this.dragSourceItem != null)
          return;
        Point point = new Point(e.X, e.Y);
        point.Offset(-this.offsetX, -this.offsetY);
        this.currentPosition = point;
        this.currentPosition = this.AdjustCurrentPosition(this.currentPosition);
        bool shouldRefresh = false;
        if (this.Capture)
        {
          if (this.colResizeState != vDataGridView.COL_RESIZE_STATES.RESIZING)
          {
            if (this.ScrollOnSelection(point, ref shouldRefresh))
            {
              if (!this.scrollTimerStarted)
              {
                this.scrollOnSelectionTimer.Start();
                this.scrollTimerStarted = true;
              }
              this.currentPosition = this.AdjustCurrentPosition(this.currentPosition);
              this.UpdateSelection();
              this.Invalidate();
              this.RefreshMouseCursor();
              return;
            }
            this.scrollTimerStarted = false;
            this.scrollOnSelectionTimer.Stop();
          }
        }
        else
        {
          this.scrollTimerStarted = false;
          this.scrollOnSelectionTimer.Stop();
        }
        if (this.colResizeState == vDataGridView.COL_RESIZE_STATES.RESIZING)
        {
          this.DoResize(point);
          this.HideEditor(EditorActivationFlags.MOUSE_MOVE, new KeyEventArgs(Keys.None), true);
          this.Invalidate();
          this.RefreshMouseCursor();
        }
        else
        {
          this.colResizeState = vDataGridView.COL_RESIZE_STATES.NO_RESIZE;
          HierarchyItem colHierarchyItem = this.columnsHierarchy.HitTest(point);
          HierarchyItem rowHierarchyItem = (HierarchyItem) null ?? this.rowsHierarchy.HitTest(point);
          if ((rowHierarchyItem != null || colHierarchyItem != null) && cell1 != null)
          {
            this.OnCellMouseLeave(new CellEventArgs(cell1));
            this.HideEditor(EditorActivationFlags.MOUSE_MOVE, new KeyEventArgs(Keys.None), true);
            cell1 = (GridCell) null;
          }
          this.resizingItem = (HierarchyItem) null;
          if (colHierarchyItem != null)
          {
            this.ItemMouseMove(point, colHierarchyItem);
            if (this.focusedItem != colHierarchyItem)
            {
              this.focusedItem = colHierarchyItem;
              if (e.Button == MouseButtons.Left)
                this.UpdateSelection();
              this.Invalidate();
            }
            if (this.enableToolTips)
              this.StartTooltipTimer(rowHierarchyItem, colHierarchyItem);
            this.RefreshMouseCursor();
          }
          else if (rowHierarchyItem != null)
          {
            this.ItemMouseMove(point, rowHierarchyItem);
            if (this.focusedItem != rowHierarchyItem)
            {
              this.focusedItem = rowHierarchyItem;
              if (e.Button == MouseButtons.Left)
                this.UpdateSelection();
              this.Invalidate();
            }
            if (this.enableToolTips)
              this.StartTooltipTimer(rowHierarchyItem, colHierarchyItem);
            this.RefreshMouseCursor();
          }
          else
          {
            this.resizingItem = (HierarchyItem) null;
            this.colResizeState = vDataGridView.COL_RESIZE_STATES.NO_RESIZE;
            this.UpdateCursor(vDataGridView.CURSOR_TYPE.ARROW);
            if (e.Button == MouseButtons.Left && this.Capture)
            {
              this.UpdateSelection();
              this.Invalidate();
            }
            if (this.focusedItem != null)
            {
              this.focusedItem = (HierarchyItem) null;
              this.Invalidate();
            }
            int refHitRow = -1;
            int refHitCol = -1;
            if (this.CellsArea.HitTest(new Point(e.X, e.Y), ref colHierarchyItem, ref rowHierarchyItem, ref refHitRow, ref refHitCol))
            {
              GridCell cell2 = new GridCell(rowHierarchyItem, colHierarchyItem, this.cellsArea);
              if (cell1 != null && cell1.Bounds != cell2.Bounds)
              {
                this.OnCellMouseLeave(new CellEventArgs(cell1));
                this.HideEditor(EditorActivationFlags.MOUSE_MOVE, new KeyEventArgs(Keys.None), true);
              }
              this.OnCellMouseEnter(new CellEventArgs(cell2));
              if (this.activeEditor.Editor == null && e.Button == MouseButtons.None)
                this.ShowEditor(rowHierarchyItem, colHierarchyItem, EditorActivationFlags.MOUSE_MOVE);
              this.OnCellMouseMove(new CellMouseEventArgs(cell2, e.Button, e.Clicks, e.X, e.Y, e.Delta));
              if (this.enableToolTips)
                this.StartTooltipTimer(rowHierarchyItem, colHierarchyItem);
              this.cellMouseMove = cell2;
            }
            else if (cell1 != null)
            {
              this.OnCellMouseLeave(new CellEventArgs(cell1));
              this.HideEditor(EditorActivationFlags.MOUSE_MOVE, new KeyEventArgs(Keys.None), true);
            }
            this.RefreshMouseCursor();
          }
        }
      }
    }

    private void StartTooltipTimer(HierarchyItem rowItem, HierarchyItem colItem)
    {
      rowItem = this.ItemToSpanItem(rowItem);
      colItem = this.ItemToSpanItem(colItem);
      this.toolTipEventArgs = new GridToolTipEventArgs(rowItem, colItem);
      this.timerToolTip.Stop();
      this.timerToolTip.Interval = this.toolTipShowDelay;
      this.timerToolTip.Start();
    }

    private void timerToolTip_Tick(object sender, EventArgs e)
    {
      this.timerToolTip.Stop();
      if (this.toolTipEventArgs == null)
        return;
      this.OnToolTipShown(this.toolTipEventArgs);
    }

    private Point AdjustCurrentPosition(Point point)
    {
      if (this.rowsHierarchy.Visible)
      {
        int num = this.rowsHierarchy.Width + this.ScrollOffset.X;
        if (point.X <= num)
          point.X = num;
      }
      else if (point.X < 0)
        point.X = 0;
      if (this.rowsHierarchy.Visible)
      {
        if (point.X > this.ClientRectangle.Right - 20)
          point.X = this.ClientRectangle.Right - 20;
      }
      else if (point.X > this.ClientRectangle.Right)
        point.X = this.ClientRectangle.Right - 20;
      if (this.columnsHierarchy.Visible)
      {
        if (point.Y < this.columnsHierarchy.Height)
          point.Y = this.columnsHierarchy.Height;
      }
      else if (point.Y < 0)
        point.Y = 0;
      if (point.Y > this.RowsHierarchy.Y + this.RowsHierarchy.Height)
        point.Y = this.RowsHierarchy.Y + this.RowsHierarchy.Height;
      return point;
    }

    protected virtual void OnAutoScrollPositionChanged(EventArgs args)
    {
      if (this.AutoScrollPositionChanged == null)
        return;
      this.AutoScrollPositionChanged((object) this, EventArgs.Empty);
    }

    /// <summary>
    /// Ensures that the specified item is visible within the control, scrolling the contents of the control if necessary.
    /// </summary>
    /// <param name="item">The HierarchyItem to scroll into view.</param>
    public void EnsureVisible(HierarchyItem item)
    {
      if (item == null || item.Hierarchy == null)
        return;
      this.PreRender();
      if (item.Visible)
        return;
      Rectangle rectangle = new Rectangle(item.DrawBounds.X, item.DrawBounds.Y, item.WidthWithChildren, item.HeightWithChildren);
      int num1 = !this.rowsHierarchy.Visible || !this.rowsHierarchy.Fixed ? 0 : this.rowsHierarchy.Width;
      int num2 = !this.columnsHierarchy.Visible || !this.columnsHierarchy.Fixed ? 0 : this.columnsHierarchy.Height;
      int num3 = this.Width - num1;
      int num4 = this.Height - num2;
      int num5 = 0;
      if (rectangle.X < num1)
        num5 = rectangle.X - num1;
      else if (rectangle.Right > num3 - (this.vScroll.Visible ? 0 : this.vScroll.Width))
      {
        num5 = rectangle.Right - num3 - num1;
        if (this.vScroll.Visible)
          num5 += this.vScroll.Width;
      }
      int num6 = 0;
      if (rectangle.Y < num2)
        num6 = rectangle.Y - num2;
      else if (rectangle.Bottom > num4 - (this.hScroll.Visible ? this.hScroll.Height : 0))
      {
        num6 = rectangle.Bottom - num4 - num2;
        if (this.hScroll.Visible)
          num6 += this.hScroll.Height;
      }
      Point point = new Point(this.ScrollOffset.X - num5, this.ScrollOffset.Y - num6);
      if (item.position.X == 0)
        point = new Point(0, point.Y);
      if (item.position.Y == 0)
        point = new Point(point.X, 0);
      this.ScrollOffset = new Point(point.X, point.Y);
    }

    /// <summary>
    /// Ensures that the specified grid cell is visible within the control, scrolling the contents of the control if necessary.
    /// </summary>
    /// <param name="cell">The GridCell to scroll into view.</param>
    public void EnsureVisible(GridCell cell)
    {
      if (cell == null || cell.RowItem == null || (cell.RowItem.Hierarchy == null || cell.ColumnItem == null) || cell.ColumnItem.Hierarchy == null)
        return;
      this.PreRender();
      if (cell.RowItem.Visible && cell.ColumnItem.Visible)
        return;
      int num1 = !this.rowsHierarchy.Visible || !this.rowsHierarchy.Fixed ? 0 : this.rowsHierarchy.Width;
      int num2 = !this.columnsHierarchy.Visible || !this.columnsHierarchy.Fixed ? 0 : this.columnsHierarchy.Height;
      int num3 = this.Width - num1;
      int num4 = this.Height - num2;
      Rectangle rectangle1 = new Rectangle(cell.RowItem.DrawBounds.X, cell.RowItem.DrawBounds.Y, cell.RowItem.WidthWithChildren, cell.RowItem.HeightWithChildren);
      Rectangle rectangle2 = new Rectangle(cell.ColumnItem.DrawBounds.X, cell.ColumnItem.DrawBounds.Y, cell.ColumnItem.WidthWithChildren, cell.ColumnItem.HeightWithChildren);
      int num5 = 0;
      if (!cell.ColumnItem.Visible)
      {
        if (rectangle2.X < num1)
          num5 = rectangle2.X - num1;
        else if (rectangle2.Right > num3 - (this.vScroll.Visible ? 0 : this.vScroll.Width))
        {
          num5 = rectangle2.Right - num3 - num1;
          if (this.vScroll.Visible)
            num5 += this.vScroll.Width;
        }
      }
      int num6 = 0;
      if (!cell.RowItem.Visible)
      {
        if (rectangle1.Y < num2)
          num6 = rectangle1.Y - num2;
        else if (rectangle1.Bottom > num4 - (this.hScroll.Visible ? this.hScroll.Height : 0))
        {
          num6 = rectangle1.Bottom - num4 - num2;
          if (this.hScroll.Visible)
            num6 += this.hScroll.Height;
        }
      }
      Point point = new Point(this.ScrollOffset.X - num5, this.ScrollOffset.Y - num6);
      if (cell.ColumnItem.position.X == 0)
        point = new Point(0, point.Y);
      if (cell.RowItem.position.Y == 0)
        point = new Point(point.X, 0);
      this.ScrollOffset = new Point(point.X, point.Y);
    }

    private void ScrollToOffset(Point pt)
    {
      int x = -pt.X + this.ClientRectangle.Right;
      if (this.vScroll.Visible)
        x -= this.vScroll.Width;
      int y = -pt.Y + this.ClientRectangle.Bottom;
      if (this.hScroll.Visible)
        y -= this.hScroll.Height;
      this.ScrollOffset = new Point(x, y);
    }

    private bool ScrollOnSelection(Point point, ref bool shouldRefresh)
    {
      this.lastScrollPosition = this.ScrollOffset;
      if (point.X > this.ClientRectangle.Width)
      {
        this.ScrollOffset = new Point(this.ScrollOffset.X - Math.Abs(point.X - this.ClientRectangle.Right), this.ScrollOffset.Y);
        shouldRefresh = true;
        this.selectStartPosition.X -= Math.Abs(this.lastScrollPosition.X - this.ScrollOffset.X);
      }
      if (point.X < this.ClientRectangle.X)
      {
        this.ScrollOffset = new Point(Math.Max(0, this.ScrollOffset.X + Math.Abs(point.X - this.ClientRectangle.X)), this.ScrollOffset.Y);
        this.selectStartPosition.X += Math.Abs(this.lastScrollPosition.X - this.ScrollOffset.X);
        shouldRefresh = true;
      }
      if (point.Y > this.ClientRectangle.Height)
      {
        this.ScrollOffset = new Point(this.ScrollOffset.X, this.ScrollOffset.Y - Math.Abs(point.Y - this.ClientRectangle.Bottom));
        shouldRefresh = true;
        this.selectStartPosition.Y -= Math.Abs(this.lastScrollPosition.Y - this.ScrollOffset.Y);
      }
      if (point.Y < this.ClientRectangle.Y)
      {
        this.ScrollOffset = new Point(this.ScrollOffset.X, this.ScrollOffset.Y + Math.Abs(point.Y - this.ClientRectangle.Y));
        shouldRefresh = true;
        this.selectStartPosition.Y += Math.Abs(this.lastScrollPosition.Y - this.ScrollOffset.Y);
      }
      if (this.lastScrollPosition == this.ScrollOffset || !shouldRefresh)
        return false;
      this.scrollOnSelection = true;
      this.Refresh();
      return true;
    }

    private void ItemMouseMove(Point point, HierarchyItem item)
    {
      if (this.resizingItem != null)
      {
        if (this.isHorizontalResize)
          this.UpdateCursor(vDataGridView.CURSOR_TYPE.COLUMN_RESIZE);
        else
          this.UpdateCursor(vDataGridView.CURSOR_TYPE.ROW_RESIZE);
      }
      else
      {
        this.UpdateCursor(vDataGridView.CURSOR_TYPE.CROSS);
        Rectangle drawBounds = item.DrawBounds;
        if (item.IsRowsHierarchyItem && this.rowsHierarchy.CompactStyleRenderingEnabled)
          drawBounds.Height = item.Height;
        int left = drawBounds.Left;
        int y = drawBounds.Y;
        int right = drawBounds.Right;
        int bottom = drawBounds.Bottom;
        if (Math.Abs(right - point.X) <= 3 && point.Y >= y && point.Y <= bottom)
        {
          this.isHorizontalResize = true;
          if (!item.Hierarchy.AllowResize || this.columnsHierarchy.IsGroupingColumn(item) || !item.Resizable)
            return;
          this.UpdateCursor(vDataGridView.CURSOR_TYPE.COLUMN_RESIZE);
          this.resizingItem = item.IsColumnsHierarchyItem ? HierarchyItem.GetLastVisibleLeaf(item) : item;
          if (!this.resizingItem.IsColumnsHierarchyItem && !this.resizingItem.Expanded)
          {
            List<HierarchyItem> pv = new List<HierarchyItem>();
            this.RowsHierarchy.GetVisibleLeafLevelItems(ref pv);
            foreach (HierarchyItem hierarchyItem in pv)
            {
              if (hierarchyItem.itemLevel > this.resizingItem.itemLevel)
                this.resizingItem = hierarchyItem;
            }
          }
          this.colResizeState = vDataGridView.COL_RESIZE_STATES.READY_RESIZE;
        }
        else if (!item.IsColumnsHierarchyItem && Math.Abs(bottom - point.Y) <= 3)
        {
          this.isHorizontalResize = false;
          if (!item.IsRowsHierarchyItem || !this.RowsHierarchy.CompactStyleRenderingEnabled)
            item = HierarchyItem.GetLastVisibleLeaf(item);
          if (!item.Hierarchy.AllowResize || !item.Resizable)
            return;
          this.UpdateCursor(vDataGridView.CURSOR_TYPE.ROW_RESIZE);
          this.resizingItem = item;
          this.colResizeState = vDataGridView.COL_RESIZE_STATES.READY_RESIZE;
        }
        else
        {
          bool isOnItemButton = false;
          if (this.HitTest(point, ref isOnItemButton) != item || !item.HierarchyHost.ShowExpandCollapseButtons || (!item.ExpandCollapseEnabled || !isOnItemButton) || item.ItemsCount <= 0)
            return;
          this.UpdateCursor(vDataGridView.CURSOR_TYPE.ARROW);
        }
      }
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      bool isOnItemButton = false;
      HierarchyItem hierarchyItem = this.HitTest(this.currentPosition, ref isOnItemButton);
      if (hierarchyItem == null || this.resizingItem == hierarchyItem)
        return;
      this.ToggleItemState(hierarchyItem);
      hierarchyItem.Hierarchy.SelectItem(hierarchyItem);
    }

    private void ToggleItemState(HierarchyItem item)
    {
      if (item.Items.Count == 0)
        return;
      HierarchyItemCancelEventArgs args = new HierarchyItemCancelEventArgs(item);
      if (item.Expanded)
        this.OnHierarchyItemCollapsing(args);
      else
        this.OnHierarchyItemExpanding(args);
      if (args.Cancel)
        return;
      if (item.Expanded)
        item.Collapse();
      else
        item.Expand();
      this.Invalidate();
      if (item.Expanded)
        this.OnItemExpanded((HierarchyItemEventArgs) args);
      else
        this.OnItemCollapsed((HierarchyItemEventArgs) args);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      if (vDataGridView.isDisabled)
        return;
      this.LicenseValidation();
      base.OnMouseDown(e);
      if (this.colResizeState == vDataGridView.COL_RESIZE_STATES.READY_RESIZE)
      {
        this.colResizeState = vDataGridView.COL_RESIZE_STATES.RESIZING;
        this.UpdateCursor(this.isHorizontalResize ? vDataGridView.CURSOR_TYPE.COLUMN_RESIZE : vDataGridView.CURSOR_TYPE.ROW_RESIZE);
        this.RefreshMouseCursor();
      }
      else
      {
        this.shouldDrag = false;
        this.initialMousePosition = e.Location;
        this.Capture = true;
        this.Focus();
        if (this.isCTRLPressed)
        {
          this.keysList.Clear();
          this.valuesList.Clear();
        }
        else
        {
          this.keysList.Clear();
          this.valuesList.Clear();
        }
        bool isOnItemButton = false;
        Point point = new Point(e.X, e.Y);
        point.Offset(-this.offsetX, -this.offsetY);
        this.currentPosition = point;
        HierarchyItem hierarchyItem = this.HitTest(this.currentPosition, ref isOnItemButton);
        if (hierarchyItem != null)
          this.OnHierarchyItemMouseDown(hierarchyItem, e);
        if (e.Button == MouseButtons.Right && hierarchyItem != null)
        {
          this.DisplayContextMenu(hierarchyItem);
          this.RefreshMouseCursor();
        }
        else
        {
          if (hierarchyItem != null && this.MultipleSelectionEnabled)
          {
            this.canDrag = true;
            if (isOnItemButton)
              this.UpdateCursor(vDataGridView.CURSOR_TYPE.ARROW);
            else
              this.UpdateCursor(vDataGridView.CURSOR_TYPE.CROSS);
            this.internalSelectMode = !hierarchyItem.IsColumnsHierarchyItem ? vDataGridView.INTERNAL_SELECT_MODE.ROW_SELECT : vDataGridView.INTERNAL_SELECT_MODE.COL_SELECT;
          }
          else
            this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.CELLS_SELECT;
          if (!this.isShiftPressed)
            this.selectStartPosition = this.currentPosition;
          HierarchyItem colHierarchyItem = (HierarchyItem) null;
          HierarchyItem rowHierarchyItem = (HierarchyItem) null;
          int refHitRow = -1;
          int refHitCol = -1;
          if (this.cellsArea.HitTest(this.currentPosition, ref colHierarchyItem, ref rowHierarchyItem, ref refHitRow, ref refHitCol))
          {
            this.OnCellMouseDown(new CellMouseEventArgs(new GridCell(rowHierarchyItem, colHierarchyItem, this.cellsArea), e.Button, e.Clicks, e.X, e.Y, e.Delta));
            if (e.Button == MouseButtons.Left)
            {
              EditorActivationFlags activationFlag = EditorActivationFlags.MOUSE_CLICK;
              if (this.CellsArea.IsCellSelected(rowHierarchyItem, colHierarchyItem) && this.CellsArea.SelectedCellsCount == 1)
                activationFlag |= EditorActivationFlags.MOUSE_CLICK_SELECTED_CELL;
              this.ShowEditor(rowHierarchyItem, colHierarchyItem, activationFlag, new KeyEventArgs(Keys.None));
            }
          }
          if (e.Button == MouseButtons.Left && this.activeEditor.Editor == null && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right))
            this.UpdateSelection();
          this.Invalidate();
          this.RefreshMouseCursor();
        }
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseClick(MouseEventArgs e)
    {
      if (vDataGridView.isDisabled)
        return;
      base.OnMouseClick(e);
      bool flag = false;
      if (this.activeEditor.Editor != null)
      {
        flag = true;
        HierarchyItem colHierarchyItem = (HierarchyItem) null;
        HierarchyItem rowHierarchyItem = (HierarchyItem) null;
        int refHitRow = -1;
        int refHitCol = -1;
        this.cellsArea.HitTest(this.currentPosition, ref colHierarchyItem, ref rowHierarchyItem, ref refHitRow, ref refHitCol);
        if (this.activeEditor.Cell.RowItem == rowHierarchyItem && this.activeEditor.Cell.ColumnItem == colHierarchyItem)
          this.UpdateSelection();
        else if (!this.HideEditor(EditorActivationFlags.MOUSE_CLICK, new KeyEventArgs(Keys.None), true))
        {
          this.activeEditor.Editor.Control.Focus();
          return;
        }
      }
      bool isOnItemButton = false;
      HierarchyItem hierarchyItem = this.HitTest(this.currentPosition, ref isOnItemButton);
      if (hierarchyItem != null)
      {
        this.OnHierarchyItemMouseClick(hierarchyItem, e);
        if (isOnItemButton)
          this.ToggleItemState(hierarchyItem);
        else if (e.Button == MouseButtons.Left && this.dragSourceItem == null && (this.resizingItem == null && hierarchyItem.SortMode == GridItemSortMode.Automatic) && hierarchyItem.SummaryItems.Count == 0)
          this.ToggleSort(hierarchyItem);
      }
      else
      {
        HierarchyItem colHierarchyItem = (HierarchyItem) null;
        HierarchyItem rowHierarchyItem = (HierarchyItem) null;
        int refHitRow1 = -1;
        int refHitCol = -1;
        if (!this.cellsArea.HitTest(this.currentPosition, ref colHierarchyItem, ref rowHierarchyItem, ref refHitRow1, ref refHitCol))
          return;
        if (this.isGroupingEnabled && this.GroupingColumns.Count > 0 && (this.ColumnsHierarchy.IsGroupingColumn(colHierarchyItem) && colHierarchyItem.ItemIndex == rowHierarchyItem.itemLevel))
        {
          if (rowHierarchyItem.Expanded)
          {
            rowHierarchyItem.Collapse();
            return;
          }
          rowHierarchyItem.Expand();
          return;
        }
        if (flag)
        {
          Point point = new Point(e.X, e.Y);
          point.Offset(-this.offsetX, -this.offsetY);
          this.selectStartPosition = this.currentPosition = point;
          int refHitRow2 = -1;
          refHitCol = -1;
          if (!this.cellsArea.HitTest(this.currentPosition, ref colHierarchyItem, ref rowHierarchyItem, ref refHitRow2, ref refHitCol))
            return;
          this.CellsArea.ClearSelection();
          this.CellsArea.SelectCell(rowHierarchyItem, colHierarchyItem);
          this.UpdateSelection();
        }
        this.OnCellMouseClick(new CellMouseEventArgs(new GridCell(rowHierarchyItem, colHierarchyItem, this.cellsArea), e.Button, e.Clicks, e.X, e.Y, e.Delta));
      }
      this.Refresh();
      this.RefreshMouseCursor();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDoubleClick" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (vDataGridView.isDisabled)
        return;
      base.OnMouseDoubleClick(e);
      bool flag = false;
      if (this.activeEditor.Editor != null)
      {
        flag = true;
        if (!this.HideEditor(EditorActivationFlags.MOUSE_DBLCLICK, new KeyEventArgs(Keys.None), true))
        {
          this.activeEditor.Editor.Control.Focus();
          return;
        }
      }
      bool isOnItemButton = false;
      HierarchyItem hierarchyItem = this.HitTest(this.currentPosition, ref isOnItemButton);
      if (hierarchyItem != null)
      {
        if (this.resizingItem == hierarchyItem && hierarchyItem.Resizable && hierarchyItem.Hierarchy.AllowResize)
          hierarchyItem.AutoResize(AutoResizeMode.FIT_ALL);
        this.OnHierarchyItemMouseDoubleClick(hierarchyItem, e);
      }
      else
      {
        HierarchyItem colHierarchyItem = (HierarchyItem) null;
        HierarchyItem rowHierarchyItem = (HierarchyItem) null;
        int refHitRow = -1;
        int refHitCol = -1;
        if (!this.cellsArea.HitTest(this.currentPosition, ref colHierarchyItem, ref rowHierarchyItem, ref refHitRow, ref refHitCol))
          return;
        if (this.isGroupingEnabled && this.groupingColumns.Count > 0 && rowHierarchyItem.ItemsCount > 0)
        {
          if (rowHierarchyItem.Expanded)
            rowHierarchyItem.Collapse();
          else
            rowHierarchyItem.Expand();
        }
        else
        {
          if (flag)
          {
            Point point = new Point(e.X, e.Y);
            point.Offset(-this.offsetX, -this.offsetY);
            this.selectStartPosition = this.currentPosition = point;
            refHitRow = -1;
            refHitCol = -1;
            if (!this.cellsArea.HitTest(this.currentPosition, ref colHierarchyItem, ref rowHierarchyItem, ref refHitRow, ref refHitCol))
              return;
            this.CellsArea.ClearSelection();
            this.CellsArea.SelectCell(rowHierarchyItem, colHierarchyItem);
            this.UpdateSelection();
          }
          this.OnCellMousedoubleClick(new CellMouseEventArgs(new GridCell(rowHierarchyItem, colHierarchyItem, this.cellsArea), e.Button, e.Clicks, e.X, e.Y, e.Delta));
          this.ShowEditor(rowHierarchyItem, colHierarchyItem, EditorActivationFlags.MOUSE_DBLCLICK);
          this.RefreshMouseCursor();
        }
      }
    }

    /// <summary>Toggles the sort of a hierarchy item.</summary>
    /// <param name="item">The item.</param>
    internal void ToggleSort(HierarchyItem item)
    {
      Cursor current = Cursor.Current;
      Cursor.Current = Cursors.WaitCursor;
      Hierarchy hierarchy = item.IsColumnsHierarchyItem ? (Hierarchy) this.RowsHierarchy : (Hierarchy) this.ColumnsHierarchy;
      if (hierarchy.SortItem != item)
        hierarchy.SortBy(item, SortingDirection.Ascending);
      else if (hierarchy.SortingDirection == SortingDirection.Ascending)
        hierarchy.SortBy(item, SortingDirection.Descending);
      else
        hierarchy.RemoveSort();
      Cursor.Current = current;
    }

    /// <summary>Submits an activation request to a grid cell's editor</summary>
    public void ShowEditor(GridCell cell)
    {
      if (cell == null)
        return;
      this.ShowEditor(cell.RowItem, cell.ColumnItem);
    }

    /// <summary>Submits an activation request to a grid cell's editor</summary>
    public void ShowEditor(HierarchyItem rowItem, HierarchyItem colItem)
    {
      if (rowItem == null || colItem == null || this.cellsArea.GetCellEditor(rowItem, colItem) == null)
        return;
      this.ShowEditor(rowItem, colItem, EditorActivationFlags.PROGRAMMATIC, new KeyEventArgs(Keys.None));
    }

    private void ShowEditor(HierarchyItem rowItem, HierarchyItem colItem, EditorActivationFlags activationFlag)
    {
      this.ShowEditor(rowItem, colItem, activationFlag, new KeyEventArgs(Keys.None));
    }

    private void ShowEditor(HierarchyItem rowItem, HierarchyItem colItem, EditorActivationFlags activationFlag, KeyEventArgs keyArgs)
    {
      CellSpan cellSpan = this.cellsArea.GetCellSpan(rowItem, colItem);
      rowItem = cellSpan.RowItem;
      colItem = cellSpan.ColumnItem;
      if (this.cellsArea.IsLockedCell(rowItem, colItem) || this.activeEditor.Editor != null)
        return;
      IEditor cellEditor = this.cellsArea.GetCellEditor(rowItem, colItem);
      if (cellEditor == null || this.activeEditor.Editor != null && rowItem == this.activeEditor.RowItem && colItem == this.activeEditor.ColumnItem || (cellEditor.ActivationFlags & activationFlag) == (EditorActivationFlags) 0)
        return;
      this.Capture = false;
      GridCell cell = new GridCell(rowItem, colItem, this.cellsArea);
      if (this.CellEditorActivate != null)
      {
        EditorActivationCancelEventArgs args = new EditorActivationCancelEventArgs(cell, activationFlag, keyArgs);
        this.CellEditorActivate((object) this, args);
        if (args.Cancel)
          return;
      }
      CellCancelEventArgs args1 = new CellCancelEventArgs(cell);
      this.OnCellBeginEdit(args1);
      if (args1.Cancel)
        return;
      this.activeEditor.Editor = cellEditor;
      this.activeEditor.RowItem = rowItem;
      this.activeEditor.ColumnItem = colItem;
      cellEditor.EditorValue = cell.Value;
      if (this.activeEditor.Editor.Control == null)
        return;
      this.activeEditor.Editor.Control.Visible = false;
      this.Controls.Add(this.activeEditor.Editor.Control);
      this.activeEditor.Editor.Control.Tag = this.cellsArea.GetCellValue(rowItem, colItem);
      this.UpdateActiveEditorLayout();
      cellEditor.OnOpenEditor(cell);
      this.activeEditor.Editor.Control.Visible = true;
      this.activeEditor.Editor.Control.Focus();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
    protected override void OnControlRemoved(ControlEventArgs e)
    {
      this.Focus();
      base.OnControlRemoved(e);
    }

    /// <summary>
    /// Submits a deactivation request to the currently opened editor
    /// </summary>
    /// <param name="CancelChanges">A Flag indicating whether the new value should be canceled or commited</param>
    public void CloseEditor(bool CancelChanges)
    {
      if (this.activeEditor.Editor == null || this.activeEditor.Editor.Control == null)
        return;
      this.HideEditor(EditorActivationFlags.PROGRAMMATIC, new KeyEventArgs(Keys.None), !CancelChanges);
    }

    private bool HideEditor(EditorActivationFlags deactivationFlag, KeyEventArgs keyArgs, bool commitChanges)
    {
      if (this.activeEditor.Editor == null || this.activeEditor.Editor.Control == null || (this.activeEditor.Editor.DeActivationFlags & deactivationFlag) == (EditorActivationFlags) 0)
        return false;
      this.activeEditor.Cell.EditValue = this.activeEditor.Editor.EditorValue;
      EditorActivationFlags deActivationFlags = this.activeEditor.Editor.DeActivationFlags;
      Point point1 = new Point(-100, -100);
      if (this.CellsArea.EditCell != null)
      {
        point1 = this.CellsArea.EditCell.Bounds.Location;
        point1 = new Point(point1.X + this.RowsHierarchy.Width + 2, point1.Y + this.ColumnsHierarchy.Height + 2);
      }
      try
      {
        System.Type conversionType = this.activeEditor.Cell.ColumnItem.ContentType;
        if (conversionType == System.Type.Missing || conversionType == null)
          conversionType = this.activeEditor.Cell.RowItem.ContentType;
        if ((conversionType == System.Type.Missing || conversionType == null) && this.activeEditor.Editor.EditorValue != null)
          conversionType = this.activeEditor.Editor.EditorValue.GetType();
        if (conversionType == System.Type.Missing || conversionType == null)
          conversionType = typeof (string);
        this.activeEditor.Cell.EditValue = Convert.ChangeType(this.activeEditor.Cell.EditValue, conversionType);
      }
      catch (Exception)
      {
      }
      if (this.CellEditorDeActivate != null)
      {
        EditorActivationCancelEventArgs args = new EditorActivationCancelEventArgs(this.activeEditor.Cell, deactivationFlag, keyArgs);
        this.CellEditorDeActivate((object) this, args);
        if (args.Cancel)
        {
          this.cellKBRangeSelectionEnd.EditValue = (object) null;
          return false;
        }
      }
      Point point2 = Point.Empty;
      if (this.activeEditor.Editor.Control != null)
        point2 = this.activeEditor.Editor.Control.Location;
      if (!commitChanges)
      {
        CellCancelEventArgs args = new CellCancelEventArgs(this.activeEditor.Cell);
        this.OnCellEndEdit(args);
        this.Capture = false;
        if (args.Cancel)
        {
          this.activeEditor.Cell.EditValue = (object) null;
          return false;
        }
        this.activeEditor.Cell.EditValue = (object) null;
        this.Controls.Remove(this.activeEditor.Editor.Control);
        this.activeEditor.Editor.Control.Hide();
        this.activeEditor.Editor = (IEditor) null;
        if (!(deActivationFlags & EditorActivationFlags.MOUSE_MOVE).Equals((object) EditorActivationFlags.MOUSE_MOVE))
        {
          this.ClearSelection();
          this.selectStartPosition = this.currentPosition = point1;
          this.UpdateSelection();
        }
        this.Refresh();
        return true;
      }
      CellCancelEventArgs args1 = new CellCancelEventArgs(this.activeEditor.Cell);
      CellParsingEventArgs args2 = new CellParsingEventArgs(this.activeEditor.Cell);
      this.OnCellValidating(args1);
      this.OnCellParsing(args2);
      if (args1.Cancel || args2.Cancel)
      {
        this.activeEditor.Cell.EditValue = (object) null;
        this.activeEditor.Editor.Control.Focus();
        this.Capture = false;
        return false;
      }
      this.OnCellValidated(new CellEventArgs(this.activeEditor.Cell));
      this.OnCellEndEdit(args1);
      if (args1.Cancel)
      {
        this.activeEditor.Cell.EditValue = (object) null;
        this.activeEditor.Editor.Control.Focus();
        this.Capture = false;
        return false;
      }
      object editValue = this.activeEditor.Cell.EditValue;
      this.activeEditor.Editor.OnCloseEditor();
      this.Controls.Remove(this.activeEditor.Editor.Control);
      this.activeEditor.Editor.Control.Hide();
      this.activeEditor.Editor = (IEditor) null;
      this.Capture = false;
      try
      {
        this.cellsArea.SetCellValue(this.activeEditor.RowItem, this.activeEditor.ColumnItem, editValue);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
        if (!(deActivationFlags & EditorActivationFlags.MOUSE_MOVE).Equals((object) EditorActivationFlags.MOUSE_MOVE))
        {
          Point point3 = point2;
          point3.Offset(-this.offsetX, -this.offsetY);
          this.currentPosition = point3;
          this.selectStartPosition = this.currentPosition;
        }
        this.Refresh();
        return true;
      }
      if (!(deActivationFlags & EditorActivationFlags.MOUSE_MOVE).Equals((object) EditorActivationFlags.MOUSE_MOVE))
      {
        Point point3 = point2;
        point3.Offset(-this.offsetX, -this.offsetY);
        this.currentPosition = point3;
        this.selectStartPosition = this.currentPosition;
      }
      this.Refresh();
      return true;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.focusedItem = (HierarchyItem) null;
      this.Invalidate();
      if (!this.EnableToolTips)
        return;
      this.gridToolTip.Hide((IWin32Window) this);
    }

    private void ClearSelection()
    {
      this.columnsHierarchy.ClearSelection();
      this.rowsHierarchy.ClearSelection();
      this.cellsArea.ClearSelection();
    }

    private void BeginSelectionUpdate()
    {
      if (this.isCTRLPressed && this.multipleSelectionEnabled)
        return;
      this.cellsArea.BeginSelectionUpdate();
      this.columnsHierarchy.BeginSelectionUpdate();
      this.rowsHierarchy.BeginSelectionUpdate();
    }

    private void EndSelectionUpdate()
    {
      this.cellsArea.EndSelectionUpdate();
      this.columnsHierarchy.EndSelectionUpdate();
      this.rowsHierarchy.EndSelectionUpdate();
    }

    private void UpdateSelection()
    {
      this.colItemRangeSelectionBeg = (HierarchyItem) null;
      this.rowItemRangeSelectionBeg = (HierarchyItem) null;
      this.BeginSelectionUpdate();
      int num1 = -1;
      int num2 = -1;
      bool isOnItemButton = false;
      Point point1 = this.selectStartPosition;
      point1.Y -= this.ScrollOffset.Y;
      point1.X -= this.ScrollOffset.X;
      HierarchyItem hierarchyItem1 = this.HitTest(this.selectStartPosition, ref isOnItemButton);
      if (hierarchyItem1 != null)
      {
        if (hierarchyItem1.IsColumnsHierarchyItem || !this.RowsHierarchy.CompactStyleRenderingEnabled)
          hierarchyItem1 = HierarchyItem.GetFirstVisibleLeaf(hierarchyItem1);
        if (hierarchyItem1.IsColumnsHierarchyItem)
        {
          if (this.selectionMode == vDataGridView.SELECTION_MODE.FULL_ROW_SELECT)
          {
            this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.NO_SELECT;
            this.EndSelectionUpdate();
            return;
          }
          this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.COL_SELECT;
          this.colItemRangeSelectionBeg = hierarchyItem1;
        }
        else
        {
          if (this.selectionMode == vDataGridView.SELECTION_MODE.FULL_COLUMN_SELECT)
          {
            this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.NO_SELECT;
            this.EndSelectionUpdate();
            return;
          }
          this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.ROW_SELECT;
          this.rowItemRangeSelectionBeg = hierarchyItem1;
        }
      }
      else if (this.cellsArea.HitTest(this.selectStartPosition, ref this.colItemRangeSelectionBeg, ref this.rowItemRangeSelectionBeg, ref num1, ref num2))
      {
        this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.CELLS_SELECT;
        if (this.selectionMode == vDataGridView.SELECTION_MODE.FULL_ROW_SELECT)
          this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.ROW_SELECT;
        else if (this.selectionMode == vDataGridView.SELECTION_MODE.FULL_COLUMN_SELECT)
          this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.COL_SELECT;
      }
      else
      {
        this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.NO_SELECT;
        this.EndSelectionUpdate();
        return;
      }
      Point point2 = this.currentPosition;
      if (this.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.COL_SELECT)
      {
        if (point2.Y >= this.columnsHierarchy.Y + this.columnsHierarchy.Height)
          point2.Y = this.columnsHierarchy.Y + this.columnsHierarchy.Height - 1;
        if (point2.Y <= this.columnsHierarchy.Y)
          point2.Y = this.selectStartPosition.Y;
        if (point2.X <= this.columnsHierarchy.X)
          point2.X = this.columnsHierarchy.X + 1;
        if (point2.X >= this.columnsHierarchy.X + this.columnsHierarchy.Width)
          point2.X = this.columnsHierarchy.X + this.columnsHierarchy.Width - 1;
      }
      else if (this.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.ROW_SELECT)
      {
        if (point2.Y >= this.rowsHierarchy.Y + this.rowsHierarchy.Height)
          point2.Y = this.rowsHierarchy.Y + this.rowsHierarchy.Height - 1;
        if (point2.Y <= this.rowsHierarchy.Y)
          point2.Y = this.rowsHierarchy.Y + 1;
        if (point2.X <= this.rowsHierarchy.X)
          point2.X = this.rowsHierarchy.X + this.rowsHierarchy.Width - 1;
        if (point2.X >= this.rowsHierarchy.X + this.rowsHierarchy.Width)
          point2.X = this.rowsHierarchy.X + this.rowsHierarchy.Width - 1;
      }
      this.colItemRangeSelectionEnd = (HierarchyItem) null;
      this.rowItemRangeSelectionEnd = (HierarchyItem) null;
      int num3 = -1;
      int num4 = -1;
      if (this.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.COL_SELECT || this.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.ROW_SELECT)
      {
        if (hierarchyItem1 != null)
        {
          HierarchyItem hierarchyItem2 = this.HitTest(point2, ref isOnItemButton);
          if (hierarchyItem2 == null)
          {
            this.HitTest(point2, ref isOnItemButton);
            this.EndSelectionUpdate();
            return;
          }
          if (hierarchyItem2.IsColumnsHierarchyItem || !this.RowsHierarchy.CompactStyleRenderingEnabled)
            hierarchyItem2 = HierarchyItem.GetLastVisibleLeaf(hierarchyItem2);
          if (hierarchyItem2.IsColumnsHierarchyItem)
            this.colItemRangeSelectionEnd = hierarchyItem2;
          else
            this.rowItemRangeSelectionEnd = hierarchyItem2;
        }
        else
        {
          GridCell gridCell = this.cellsArea.HitTest(this.currentPosition);
          if (gridCell != null)
          {
            this.colItemRangeSelectionEnd = gridCell.ColumnItem;
            this.rowItemRangeSelectionEnd = gridCell.RowItem;
          }
          else
          {
            this.colItemRangeSelectionEnd = (HierarchyItem) null;
            this.rowItemRangeSelectionEnd = (HierarchyItem) null;
          }
        }
      }
      point2.Y -= this.ScrollOffset.Y;
      point2.X -= this.ScrollOffset.X;
      if (this.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.CELLS_SELECT)
      {
        this.cellsArea.HitTest(this.selectStartPosition, ref this.colItemRangeSelectionBeg, ref this.rowItemRangeSelectionBeg, ref num1, ref num2);
        if (num1 == -1 || num2 == -1)
        {
          this.EndSelectionUpdate();
          return;
        }
        if (point1 != point2)
          this.cellsArea.HitTest(this.currentPosition, ref this.colItemRangeSelectionEnd, ref this.rowItemRangeSelectionEnd, ref num3, ref num4);
        if (num3 == -1 || num4 == -1)
        {
          this.internalSelectMode = vDataGridView.INTERNAL_SELECT_MODE.CELLS_SELECT;
          this.cellsArea.SelectCell(this.rowItemRangeSelectionBeg, this.colItemRangeSelectionBeg);
          this.cellKBRangeSelectionStart = this.cellKBRangeSelectionEnd = new GridCell(this.rowItemRangeSelectionBeg, this.colItemRangeSelectionBeg, this.cellsArea);
          this.EndSelectionUpdate();
          return;
        }
      }
      this.SaveSelectedItemsRangeOrder();
      this.AdjustSelectedItemsOrder();
      if (this.internalSelectMode == vDataGridView.INTERNAL_SELECT_MODE.CELLS_SELECT)
      {
        List<HierarchyItem> pv1 = new List<HierarchyItem>();
        this.rowsHierarchy.GetVisibleLeafLevelItems(ref pv1);
        List<HierarchyItem> pv2 = new List<HierarchyItem>();
        this.columnsHierarchy.GetVisibleLeafLevelItems(ref pv2);
        this.RowsHierarchy.PointToLeafItem(this.selectStartPosition, ref num2);
        this.RowsHierarchy.PointToLeafItem(this.currentPosition, ref num4);
        HierarchyItem leafItem1 = this.ColumnsHierarchy.PointToLeafItem(this.selectStartPosition, ref num1);
        HierarchyItem leafItem2 = this.ColumnsHierarchy.PointToLeafItem(this.currentPosition, ref num3);
        if (leafItem1 != null && leafItem1.Fixed)
          num1 = pv2.IndexOf(leafItem1);
        if (leafItem2 != null && leafItem2.Fixed)
          num3 = pv2.IndexOf(leafItem2);
        if (num3 < num1)
        {
          int num5 = num1;
          num1 = num3;
          num3 = num5;
        }
        if (num4 < num2)
        {
          int num5 = num2;
          num2 = num4;
          num4 = num5;
        }
        for (int index1 = num2; index1 <= num4 && index1 < pv1.Count; ++index1)
        {
          HierarchyItem rowItem = pv1[index1];
          for (int index2 = num1; index2 <= num3 && index2 < pv2.Count; ++index2)
          {
            HierarchyItem columnItem = pv2[index2];
            this.cellsArea.SelectCellInternal(rowItem, columnItem);
            if (columnItem == this.colItemRangeSelectionEnd)
              break;
          }
          if (rowItem == this.rowItemRangeSelectionEnd)
            break;
        }
        this.cellKBRangeSelectionStart = new GridCell(this.rowItemRangeSelectionBeg, this.colItemRangeSelectionBeg, this.cellsArea);
        this.cellKBRangeSelectionEnd = new GridCell(this.rowItemRangeSelectionEnd, this.colItemRangeSelectionEnd, this.cellsArea);
        this.EndSelectionUpdate();
        this.Invalidate();
      }
      else
      {
        this.RestoreSelectedItemsRangeOrder();
        this.ApplyItemsMultiSelect();
        this.EndSelectionUpdate();
      }
    }

    private void SaveSelectedItemsRangeOrder()
    {
      this.adjSelectedItemsSave[0] = this.colItemRangeSelectionBeg;
      this.adjSelectedItemsSave[1] = this.colItemRangeSelectionEnd;
      this.adjSelectedItemsSave[2] = this.rowItemRangeSelectionBeg;
      this.adjSelectedItemsSave[3] = this.rowItemRangeSelectionEnd;
    }

    private void RestoreSelectedItemsRangeOrder()
    {
      this.colItemRangeSelectionBeg = this.adjSelectedItemsSave[0];
      this.colItemRangeSelectionEnd = this.adjSelectedItemsSave[1];
      this.rowItemRangeSelectionBeg = this.adjSelectedItemsSave[2];
      this.rowItemRangeSelectionEnd = this.adjSelectedItemsSave[3];
    }

    private void AdjustSelectedItemsOrder()
    {
      if (this.colItemRangeSelectionBeg != null && this.colItemRangeSelectionEnd != null && this.colItemRangeSelectionBeg.X > this.colItemRangeSelectionEnd.X)
      {
        HierarchyItem hierarchyItem = this.colItemRangeSelectionBeg;
        this.colItemRangeSelectionBeg = this.colItemRangeSelectionEnd;
        this.colItemRangeSelectionEnd = hierarchyItem;
      }
      if (this.rowItemRangeSelectionBeg != null && this.rowItemRangeSelectionEnd != null && this.rowItemRangeSelectionBeg.Y > this.rowItemRangeSelectionEnd.Y)
      {
        HierarchyItem hierarchyItem = this.rowItemRangeSelectionBeg;
        this.rowItemRangeSelectionBeg = this.rowItemRangeSelectionEnd;
        this.rowItemRangeSelectionEnd = hierarchyItem;
      }
      if (this.multipleSelectionEnabled)
        return;
      this.rowItemRangeSelectionEnd = this.rowItemRangeSelectionBeg;
      this.colItemRangeSelectionEnd = this.colItemRangeSelectionBeg;
    }

    private void ApplyItemsMultiSelect()
    {
      this.SaveSelectedItemsRangeOrder();
      this.AdjustSelectedItemsOrder();
      for (int index = 0; index < 2; ++index)
      {
        if ((this.internalSelectMode != vDataGridView.INTERNAL_SELECT_MODE.COL_SELECT || index == 0) && (this.internalSelectMode != vDataGridView.INTERNAL_SELECT_MODE.ROW_SELECT || index == 1))
        {
          HierarchyItem hierarchyItem1 = index == 0 ? this.colItemRangeSelectionBeg : this.rowItemRangeSelectionBeg;
          HierarchyItem hierarchyItem2 = index == 0 ? this.colItemRangeSelectionEnd : this.rowItemRangeSelectionEnd;
          Hierarchy hierarchy = index == 0 ? (Hierarchy) this.columnsHierarchy : (Hierarchy) this.rowsHierarchy;
          List<HierarchyItem> pv = (List<HierarchyItem>) null;
          hierarchy.GetVisibleLeafLevelItems(ref pv);
          if (pv == null)
          {
            this.Invalidate();
            this.RestoreSelectedItemsRangeOrder();
            return;
          }
          bool flag = false;
          foreach (HierarchyItem hierarchyItem3 in pv)
          {
            if (hierarchyItem3 == hierarchyItem1 || flag)
            {
              if (hierarchyItem3.WidthWithChildren + hierarchyItem3.X > this.mostRightItemBounds.Right)
                this.mostRightItemBounds = new Rectangle(hierarchyItem3.X, hierarchyItem3.Y, hierarchyItem3.WidthWithChildren, hierarchyItem3.HeightWithChildren);
              if (hierarchyItem3.WidthWithChildren + hierarchyItem3.X < this.mostLeftItemBounds.Right)
                this.mostLeftItemBounds = new Rectangle(hierarchyItem3.X, hierarchyItem3.Y, hierarchyItem3.WidthWithChildren, hierarchyItem3.HeightWithChildren);
              flag = true;
              hierarchy.SelectItem(hierarchyItem3);
              if (hierarchyItem3 == hierarchyItem2)
                break;
            }
          }
          if (hierarchy.IsColumnsHierarchy || !((RowsHierarchy) hierarchy).CompactStyleRenderingEnabled)
            hierarchy.ApplySelectionToParentItems();
        }
      }
      this.RestoreSelectedItemsRangeOrder();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (this.IsDisposed)
        return;
      this.canDrag = false;
      this.Capture = false;
      this.scrollOnSelection = false;
      this.scrollOnSelectionTimer.Stop();
      base.OnMouseUp(e);
      if (this.EnableResizeToolTip && this.resizeToolTip != null)
        this.resizeToolTip.Hide((IWin32Window) this);
      int refHitRow = -1;
      int refHitCol = -1;
      Point point1 = new Point(e.X, e.Y);
      HierarchyItem rowHierarchyItem = (HierarchyItem) null;
      HierarchyItem colHierarchyItem = (HierarchyItem) null;
      if (this.CellsArea.HitTest(point1, ref colHierarchyItem, ref rowHierarchyItem, ref refHitRow, ref refHitCol))
        this.OnCellMouseUp(new CellMouseEventArgs(new GridCell(rowHierarchyItem, colHierarchyItem, this.cellsArea), e.Button, e.Clicks, e.X, e.Y, e.Delta));
      if (e.Button == MouseButtons.Left && this.colResizeState == vDataGridView.COL_RESIZE_STATES.RESIZING)
      {
        this.colResizeState = vDataGridView.COL_RESIZE_STATES.NO_RESIZE;
        this.UpdateCursor(vDataGridView.CURSOR_TYPE.ARROW);
        this.columnsHierarchy.PreRenderRequired = true;
        this.rowsHierarchy.PreRenderRequired = true;
        this.resizingItem = (HierarchyItem) null;
        this.Invalidate();
      }
      else
      {
        bool isOnItemButton = false;
        Point point2 = new Point(e.X, e.Y);
        point2.Offset(-this.offsetX, -this.offsetY);
        this.currentPosition = point2;
        HierarchyItem hierarchyItem = this.HitTest(this.currentPosition, ref isOnItemButton);
        if (hierarchyItem != null)
          this.OnHierarchyItemMouseUp(hierarchyItem, e);
        if (this.dragSourceItem != null && hierarchyItem != null && (hierarchyItem.parentItem == this.dragSourceItem.parentItem && hierarchyItem.HierarchyHost == this.dragSourceItem.HierarchyHost) && hierarchyItem.IsSummaryItem == this.dragSourceItem.IsSummaryItem)
          this.EndDrag(hierarchyItem);
        else if (this.dragBtn != null)
          this.EndDrag(this.dragSourceItem);
        this.Invalidate();
      }
    }

    internal static long ComputeHash(HierarchyItem colItem, HierarchyItem rowItem)
    {
      return (long) colItem.GetHashCode() << 32 | (long) rowItem.GetHashCode();
    }

    internal void PositionsRecompute()
    {
      this.columnsHierarchy.X = this.rowsHierarchy.Visible ? this.rowsHierarchy.Width : 0;
      this.rowsHierarchy.Y = this.columnsHierarchy.Visible ? this.columnsHierarchy.Height : 0;
      this.columnsHierarchy.X += this.ScrollOffset.X;
      this.columnsHierarchy.Y += this.ScrollOffset.Y;
      this.rowsHierarchy.Y += this.ScrollOffset.Y;
      this.rowsHierarchy.X += this.ScrollOffset.X;
      this.columnsHierarchy.PreRender();
      this.rowsHierarchy.PreRender();
    }

    [Obsolete("Use the RowsHierarchy.SortBy and ColumnsHierarchy.SortBy methods.")]
    public void SortBy(HierarchyItem item, SortingDirection sortingDirection)
    {
      this.InternalSortBy(item, sortingDirection);
    }

    internal void InternalSortBy(HierarchyItem item, SortingDirection sortingDirection)
    {
      (item.IsColumnsHierarchyItem ? (Hierarchy) this.RowsHierarchy : (Hierarchy) this.ColumnsHierarchy).SortBy(item, sortingDirection);
    }

    [Obsolete("Use the RowsHierarchy.RemoveSort and ColumnsHierarchy.RemoveSort methods.")]
    public void RemoveSort()
    {
      if (this.ColumnsHierarchy.IsSorted)
        this.ColumnsHierarchy.RemoveSort();
      if (!this.RowsHierarchy.IsSorted)
        return;
      this.RowsHierarchy.RemoveSort();
    }

    private Icon GetIconFromResource(string iconName)
    {
      Stream stream = (Stream) null;
      try
      {
        stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VIBlend.WinForms.DataGridView.Resources." + iconName);
        if (stream != null)
        {
          Icon icon = new Icon(stream);
          if (icon != null)
            return icon;
        }
      }
      catch (Exception)
      {
      }
      finally
      {
        if (stream != null)
          stream.Close();
      }
      return (Icon) null;
    }

    private void value_Opening(object sender, CancelEventArgs e)
    {
      if (!this.AllowDefaultContextMenu)
        return;
      Point client = this.PointToClient(Cursor.Position);
      if (this.RowsHierarchy.HitTest(client) == null && this.ColumnsHierarchy.HitTest(client) == null)
        return;
      e.Cancel = true;
    }

    /// <summary>Displays the context menu.</summary>
    /// <param name="item">The item.</param>
    protected virtual void DisplayContextMenu(HierarchyItem item)
    {
      if (!this.AllowDefaultContextMenu)
        return;
      this.itemContextMenuHit = item;
      this.menu = new ContextMenuStrip();
      if (item.SummaryItems.Count > 0)
        return;
      vStripsRenderer vStripsRenderer = new vStripsRenderer() { VIBlendTheme = this.VIBlendTheme, RenderedContextMenuStrip = this.menu };
      this.menu.ImageList = new ImageList()
      {
        TransparentColor = Color.Transparent,
        ColorDepth = ColorDepth.Depth32Bit,
        ImageSize = new Size(16, 16),
        Images = {
          this.GetIconFromResource("icon_sortASC.ico"),
          this.GetIconFromResource("icon_sortDESC.ico"),
          this.GetIconFromResource("icon_sortClear.ico"),
          this.GetIconFromResource("icon_filter.ico"),
          this.GetIconFromResource("icon_filterClear.ico")
        }
      };
      int num1 = 0;
      this.menuItemSortAsc.Text = this.Localization.GetString(LocalizationNames.ContextMenuSortSmallestToLargest);
      this.menuItemSortDesc.Text = this.Localization.GetString(LocalizationNames.ContextMenuSortLargestToSmallest);
      this.menuItemFilter.Text = this.Localization.GetString(LocalizationNames.ContextMenuFilter);
      this.menuItemColumnChooser.Text = this.Localization.GetString(LocalizationNames.ContextMenuColumnChooser);
      this.menuItemFilterClear.Text = this.Localization.GetString(LocalizationNames.ContextMenuClearFilter);
      this.menuItemGroupBy.Text = this.Localization.GetString(LocalizationNames.ContextMenuGroupByThisColumn);
      this.menuItemSortClear.Text = this.Localization.GetString(LocalizationNames.ContextMenuRemoveSort);
      if (item.SortMode != GridItemSortMode.NotSortable && this.allowContextMenuSorting)
      {
        this.menu.Items.Add((ToolStripItem) this.menuItemSortAsc);
        this.menu.Items.Add((ToolStripItem) this.menuItemSortDesc);
        this.menu.Items.Add((ToolStripItem) this.menuItemSortClear);
        ToolStripItemCollection items1 = this.menu.Items;
        int index1 = num1;
        int num2 = 1;
        int num3 = index1 + num2;
        items1[index1].ImageIndex = 0;
        ToolStripItemCollection items2 = this.menu.Items;
        int index2 = num3;
        int num4 = 1;
        int num5 = index2 + num4;
        items2[index2].ImageIndex = 1;
        ToolStripItemCollection items3 = this.menu.Items;
        int index3 = num5;
        int num6 = 1;
        num1 = index3 + num6;
        items3[index3].ImageIndex = 2;
        this.menuItemSortClear.Enabled = item.IsSortItem;
      }
      if (item.AllowFiltering && this.allowContextMenuFiltering)
      {
        if (this.menu.Items.Count > 0)
          this.menu.Items.Add("-");
        this.menu.Items.Add((ToolStripItem) this.menuItemFilter);
        this.menu.Items.Add((ToolStripItem) this.menuItemFilterClear);
        ToolStripItemCollection items1 = this.menu.Items;
        int index1 = num1;
        int num2 = 1;
        int num3 = index1 + num2;
        items1[index1].ImageIndex = 3;
        ToolStripItemCollection items2 = this.menu.Items;
        int index2 = num3;
        int num4 = 1;
        int num5 = index2 + num4;
        items2[index2].ImageIndex = 4;
        this.menuItemFilterClear.Enabled = item.HasFilterRule;
      }
      if (item.IsColumnsHierarchyItem)
      {
        if (this.GroupingEnabled && this.AllowContextMenuGrouping)
          this.menu.Items.Add((ToolStripItem) this.menuItemGroupBy);
        if (this.AllowContextMenuColumnChooser && this.EnableColumnChooser)
          this.menu.Items.Add((ToolStripItem) this.menuItemColumnChooser);
      }
      if (this.menu.Items.Count <= 0)
        return;
      CancelEventArgs args = new CancelEventArgs();
      this.OnContextMenuShowing((object) this.menu, args);
      if (!args.Cancel)
        this.menu.Show(Cursor.Position);
      this.menu.Closed += new ToolStripDropDownClosedEventHandler(this.menu_Closed);
    }

    private void menu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
      this.OnContextMenuClosed(sender, EventArgs.Empty);
      if (this.menu == null)
        return;
      this.menu.Closed -= new ToolStripDropDownClosedEventHandler(this.menu_Closed);
    }

    private void menuItemFilterClear_Click(object sender, EventArgs e)
    {
      Hierarchy hierarchy = this.itemContextMenuHit.IsColumnsHierarchyItem ? (Hierarchy) this.RowsHierarchy : (Hierarchy) this.ColumnsHierarchy;
      for (int index = 0; index < hierarchy.Filters.Count; ++index)
      {
        if (hierarchy.Filters[index].Item == this.itemContextMenuHit)
        {
          hierarchy.Filters.RemoveAt(index);
          --index;
        }
      }
      this.Refresh();
    }

    private void menuItemFilter_Click(object sender, EventArgs e)
    {
      this.ShowFilterForm(this.itemContextMenuHit);
    }

    private void menuItemSortClear_Click(object sender, EventArgs e)
    {
      Cursor current = Cursor.Current;
      Cursor.Current = Cursors.WaitCursor;
      if (this.itemContextMenuHit.IsColumnsHierarchyItem)
        this.RowsHierarchy.RemoveSort();
      else
        this.ColumnsHierarchy.RemoveSort();
      Cursor.Current = current;
      this.Refresh();
    }

    private void menuItemSortDesc_Click(object sender, EventArgs e)
    {
      Cursor current = Cursor.Current;
      Cursor.Current = Cursors.WaitCursor;
      if (this.itemContextMenuHit.IsColumnsHierarchyItem)
        this.RowsHierarchy.SortBy(this.itemContextMenuHit, SortingDirection.Descending);
      else
        this.ColumnsHierarchy.SortBy(this.itemContextMenuHit, SortingDirection.Descending);
      Cursor.Current = current;
      this.Refresh();
    }

    private void menuItemGroupBy_Click(object sender, EventArgs e)
    {
      HierarchyItem hierarchyItem = this.itemContextMenuHit;
      if (hierarchyItem == null || this.boundFields.Count == 0)
        return;
      string str = "";
      string dataField = "";
      foreach (BoundField boundField in this.BoundFields)
      {
        if (boundField.Text.Equals(hierarchyItem.Caption))
        {
          str = hierarchyItem.Caption;
          dataField = boundField.DataField;
        }
      }
      foreach (BoundField groupingColumn in this.GroupingColumns)
      {
        if (groupingColumn.Text.Equals(str))
          return;
      }
      this.GroupingColumns.Add(new BoundField(hierarchyItem.Caption, dataField));
    }

    private void menuItemSortAsc_Click(object sender, EventArgs e)
    {
      Cursor current = Cursor.Current;
      Cursor.Current = Cursors.WaitCursor;
      if (this.itemContextMenuHit.IsColumnsHierarchyItem)
        this.RowsHierarchy.SortBy(this.itemContextMenuHit, SortingDirection.Ascending);
      else
        this.ColumnsHierarchy.SortBy(this.itemContextMenuHit, SortingDirection.Ascending);
      Cursor.Current = current;
      this.Refresh();
    }

    private void UpdateBindingProgress(int recordsProcessed, int totalNumberOfRecords)
    {
      if (this.BindingProgressChanged == null)
        return;
      if (!this.BindingProgressEnabled)
        return;
      try
      {
        this.BindingProgressChanged((object) this, new BindingProgressEventArgs(recordsProcessed, totalNumberOfRecords));
      }
      catch (Exception)
      {
      }
    }

    /// <exclude />
    protected internal virtual bool OnGroupHeaderCustomTextNeeded(HierarchyItem rowItem, out string customText, out Font font, out Color color, out ContentAlignment alignment)
    {
      customText = "";
      font = this.Font;
      color = Color.Black;
      alignment = ContentAlignment.MiddleLeft;
      if (this.GroupHeaderCustomTextNeeded == null)
        return false;
      vDataGridView.GroupHeaderCustomTextNeededEventArgs args = new vDataGridView.GroupHeaderCustomTextNeededEventArgs(rowItem, rowItem.Items);
      args.HeaderTextAlignment = alignment;
      this.GroupHeaderCustomTextNeeded((object) this, args);
      font = args.HeaderFont;
      customText = args.HeaderText;
      color = args.HeaderTextColor;
      alignment = args.HeaderTextAlignment;
      return true;
    }

    /// <summary>Called when a property is changed.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>Begins the update totals.</summary>
    public void BeginUpdateTotals()
    {
      this.isUpdatingTotals = true;
    }

    /// <summary>Ends the update totals.</summary>
    public void EndUpdateTotals()
    {
      this.isUpdatingTotals = false;
      this.UpdateTotals();
    }

    private void UpdateTotals()
    {
      if (this.isUpdatingTotals || this.DataSource == null)
        return;
      this.DataBind();
      this.ColumnsHierarchy.AutoResize(AutoResizeMode.FIT_ITEM_CONTENT);
      this.RowsHierarchy.AutoResize(AutoResizeMode.FIT_ITEM_CONTENT);
    }

    private void BeginDrag(HierarchyItem dragItem)
    {
      if (dragItem == null || !dragItem.Hierarchy.AllowDragDrop || this.columnsHierarchy.IsGroupingColumn(dragItem))
        return;
      CellSpan itemSpanGroup = this.CellsArea.GetItemSpanGroup(dragItem);
      if (itemSpanGroup.ColumnItem != null && itemSpanGroup.RowItem != null || dragItem.parentItem != null && dragItem.parentItem.Items.Count + dragItem.parentItem.SummaryItems.Count <= 1)
        return;
      if (dragItem.ParentItem != null)
      {
        if (dragItem.ParentItem.Items.IsSorted || dragItem.ParentItem.SummaryItems.IsSorted)
          return;
      }
      else if (dragItem.Hierarchy.Items.IsSorted || dragItem.Hierarchy.SummaryItems.IsSorted)
        return;
      if (dragItem.Fixed)
        return;
      HierarchyItemDragCancelEventArgs args = new HierarchyItemDragCancelEventArgs(dragItem, (HierarchyItem) null);
      this.OnHierarchyItemDragStarting(args);
      if (args.Cancel)
      {
        this.dragSourceItem = (HierarchyItem) null;
      }
      else
      {
        this.dragSourceItem = dragItem;
        Bitmap itemImageForDrag = dragItem.GetItemImageForDrag();
        if (this.dragBtn == null)
          this.dragBtn = this.CreateDragDropControl(itemImageForDrag);
        else
          this.dragBtn.Image = (Image) itemImageForDrag;
        this.dragBtn.Location = this.PointToClient(Cursor.Position);
        this.dragBtn.Size = itemImageForDrag.Size;
        this.dragBtn.Opacity = 0.0f;
        this.dragBtn.Parent = (Control) this;
        this.dragBtn.Visible = false;
        this.dragBtn.LocationChanged += new EventHandler(this.dragBtn_LocationChanged);
        this.Controls.Add((Control) this.dragBtn);
        dragItem.HierarchyHost.UnSelectItem(dragItem);
        this.OnHierarchyItemDragStarted(new HierarchyItemDragEventArgs(dragItem, (HierarchyItem) null));
      }
    }

    private void EndDrag(HierarchyItem dragTargetItem)
    {
      if (this.GroupingEnabled)
      {
        this.DropOnGroupingHeader();
        this.OnHierarchyItemDragEnded(new HierarchyItemDragEventArgs(this.dragSourceItem, dragTargetItem));
        this.dragSourceItem = (HierarchyItem) null;
        this.Controls.Remove((Control) this.dragBtn);
        this.dragBtn = (vButton) null;
        this.dragDropForm.Dispose();
        this.dragDropForm = (Form) null;
      }
      else if (!this.CanDrop(dragTargetItem))
      {
        this.OnHierarchyItemDragEnded(new HierarchyItemDragEventArgs(this.dragSourceItem, dragTargetItem));
        this.dragSourceItem = (HierarchyItem) null;
        this.Controls.Remove((Control) this.dragBtn);
        this.dragBtn = (vButton) null;
        this.dragDropForm.Dispose();
        this.dragDropForm = (Form) null;
      }
      else
      {
        HierarchyItemDragCancelEventArgs args = new HierarchyItemDragCancelEventArgs(this.dragSourceItem, dragTargetItem);
        this.OnHierarchyItemDragEnding(args);
        if (args.Cancel)
          return;
        HierarchyItemsCollection hierarchyItemsCollection = !this.dragSourceItem.IsSummaryItem ? (this.dragSourceItem.ParentItem == null ? this.dragSourceItem.Hierarchy.Items : this.dragSourceItem.ParentItem.Items) : (this.dragSourceItem.ParentItem == null ? this.dragSourceItem.Hierarchy.SummaryItems : this.dragSourceItem.ParentItem.SummaryItems);
        int index1 = hierarchyItemsCollection.IndexOf(this.dragSourceItem);
        int index2 = hierarchyItemsCollection.IndexOf(dragTargetItem);
        hierarchyItemsCollection.RemoveAt(index1);
        if (index1 < index2)
          --index2;
        if (this.dragPositionInTarget == DragPositionInTarget.Right || this.dragPositionInTarget == DragPositionInTarget.Down)
          ++index2;
        if (index2 < hierarchyItemsCollection.Count)
          hierarchyItemsCollection.Insert(this.dragSourceItem, index2);
        else
          hierarchyItemsCollection.Add(this.dragSourceItem);
        hierarchyItemsCollection.OwnerHierarchy.UpdateColumnsIndexes();
        hierarchyItemsCollection.OwnerHierarchy.UpdateVisibleLeaves();
        this.OnHierarchyItemDragEnded(new HierarchyItemDragEventArgs(this.dragSourceItem, dragTargetItem));
        dragTargetItem.Hierarchy.PreRenderRequired = true;
        this.dragSourceItem = (HierarchyItem) null;
        this.Controls.Remove((Control) this.dragBtn);
        this.dragBtn = (vButton) null;
        this.dragDropForm.Dispose();
        this.dragDropForm = (Form) null;
      }
    }

    private void DropOnGroupingHeader()
    {
      Control parent = this.Parent;
      foreach (Control control in (ArrangedElementCollection) this.Parent.Controls)
      {
        if (control.GetType() == typeof (vDataGridViewGroupsHeader) && control.RectangleToScreen(control.ClientRectangle).Contains(Cursor.Position))
        {
          using (IEnumerator<BoundField> enumerator = this.BoundFields.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              BoundField current = enumerator.Current;
              if (current.Text.Equals(this.dragSourceItem.Caption))
              {
                BoundField boundField = new BoundField(current.Text, current.DataField);
                foreach (BoundField groupingColumn in this.GroupingColumns)
                {
                  if (boundField.DataField.Equals(groupingColumn.DataField))
                    return;
                }
                this.GroupingColumns.Add(boundField);
                break;
              }
            }
            break;
          }
        }
      }
    }

    private bool CanDrop(HierarchyItem targetItem)
    {
      if (targetItem == null || this.dragSourceItem == null || (targetItem.parentItem != this.dragSourceItem.parentItem || targetItem.Hierarchy != this.dragSourceItem.Hierarchy) || (targetItem.IsSummaryItem != this.dragSourceItem.IsSummaryItem || this.columnsHierarchy.IsGroupingColumn(targetItem) || targetItem.Fixed))
        return false;
      CellSpan itemSpanGroup = this.CellsArea.GetItemSpanGroup(targetItem);
      return itemSpanGroup.RowItem == null || itemSpanGroup.ColumnItem == null;
    }

    internal virtual bool ShouldDrag(Point mousePosition)
    {
      if (!this.shouldDrag)
        this.shouldDrag = Math.Abs(mousePosition.X - this.initialMousePosition.X) >= SystemInformation.DragSize.Width || Math.Abs(mousePosition.Y - this.initialMousePosition.Y) >= SystemInformation.DragSize.Height;
      return this.shouldDrag;
    }

    private vButton CreateDragDropControl(Bitmap image)
    {
      vButton vButton = new vButton();
      try
      {
        vButton.Size = new Size(0, 0);
      }
      catch
      {
        vButton = new vButton();
      }
      vButton.BorderStyle = VIBlend.WinForms.Controls.ButtonBorderStyle.NONE;
      vButton.Text = "";
      vButton.Image = (Image) image;
      return vButton;
    }

    private void dragDropForm_Paint(object sender, PaintEventArgs e)
    {
      int x = 0;
      int y = 0;
      HierarchyItemStyle hierarchyItemStyle = this.Theme.HierarchyItemStyleNormal;
      if (!this.Enabled)
        hierarchyItemStyle = this.Theme.HierarchyItemStyleDisabled;
      Size size = this.dragBtn.Size;
      Rectangle rect = new Rectangle(x, y, size.Width, size.Height);
      Brush brush = hierarchyItemStyle.FillStyle.GetBrush(rect);
      e.Graphics.FillRectangle(brush, rect);
      using (Pen pen = new Pen(hierarchyItemStyle.BorderColor, 1f))
      {
        e.Graphics.DrawRectangle(pen, rect);
        StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;
        using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
        {
          Rectangle rectangle = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
          e.Graphics.DrawString(this.dragSourceItem.Caption, this.Font, (Brush) solidBrush, (RectangleF) rectangle, format);
        }
      }
    }

    private Form GetDragDropForm(HierarchyItem headerItem)
    {
      if (headerItem == null)
        return (Form) null;
      if (this.dragDropForm == null)
      {
        this.dragDropForm = new Form();
        this.dragDropForm.ShowInTaskbar = false;
        this.dragDropForm.FormBorderStyle = FormBorderStyle.None;
        this.dragDropForm.Opacity = 0.01;
        this.dragDropForm.Shown += new EventHandler(this.dragDropForm_Shown);
        this.dragDropForm.Paint += new PaintEventHandler(this.dragDropForm_Paint);
        this.dragDropForm.Size = headerItem.DrawBounds.Size;
        this.dragDropForm.MinimumSize = Size.Empty;
      }
      return this.dragDropForm;
    }

    private void dragDropForm_Shown(object sender, EventArgs e)
    {
      this.dragDropForm.Opacity = 0.5;
      this.dragDropForm.Shown -= new EventHandler(this.dragDropForm_Shown);
    }

    private void DoDrag(Point point, Graphics g)
    {
      if (!this.canDrag || !this.Capture || !this.ShouldDrag(point))
        return;
      HierarchyItem hierarchyItem = (HierarchyItem) null;
      if (this.columnsHierarchy.AllowDragDrop)
        hierarchyItem = this.columnsHierarchy.HitTest(point);
      if (hierarchyItem == null && this.rowsHierarchy.AllowDragDrop)
        hierarchyItem = this.rowsHierarchy.HitTest(point);
      this.OnHierarchyItemDrag(new HierarchyItemDragEventArgs(this.dragSourceItem, hierarchyItem));
      if (this.dragBtn != null)
      {
        this.dragBtn.Location = this.PointToClient(Cursor.Position);
        Form dragDropForm = this.GetDragDropForm(this.dragSourceItem);
        if (this.dragBtn.Location.X < 0 || (this.dragBtn.Location.Y < 0 || this.dragBtn.Right > this.Width || this.dragBtn.Bottom > this.Height))
        {
          if (this.GroupingEnabled)
          {
            if (!dragDropForm.Visible)
            {
              dragDropForm.Show((IWin32Window) this);
              this.dragDropForm.Size = this.dragBtn.Size;
            }
            dragDropForm.Location = Cursor.Position;
          }
        }
        else if (dragDropForm.Visible)
          dragDropForm.Hide();
        if (g == null && !dragDropForm.Visible)
          this.ScrollOnDragging(point);
        bool flag = false;
        if (hierarchyItem != null)
        {
          CellSpan itemSpanGroup = this.CellsArea.GetItemSpanGroup(hierarchyItem);
          flag = itemSpanGroup.RowItem != null && itemSpanGroup.ColumnItem != null;
        }
        if (hierarchyItem == null || hierarchyItem == this.dragSourceItem || (!this.AllowDragDropIndication || flag) || (hierarchyItem.Fixed || !this.CanDrop(hierarchyItem)))
          return;
        this.DrawDragIndicator(hierarchyItem, g);
      }
      else
        this.BeginDrag(hierarchyItem);
    }

    private void LoadDragDropArrows()
    {
      string[] strArray = new string[4]{ "icon_arrowup.ico", "icon_arrowdown.ico", "icon_arrowleft.ico", "icon_arrowright.ico" };
      for (int index = 0; index < strArray.Length; ++index)
      {
        Stream manifestResourceStream = Assembly.GetAssembly(typeof (vDataGridView)).GetManifestResourceStream("VIBlend.WinForms.DataGridView.Resources." + strArray[index]);
        this.dragIcons[index] = new Icon(manifestResourceStream);
        manifestResourceStream.Close();
      }
    }

    private void DrawItemDragIndicator(Graphics g, HierarchyItem item)
    {
      if (g == null || item.Hidden || item.Filtered)
        return;
      int num1 = item.X + item.Hierarchy.X;
      int num2 = item.Y + item.Hierarchy.Y;
      Rectangle drawBounds = item.DrawBounds;
      if (num2 > this.Height || num1 > this.Width)
        return;
      if (this.dragPositionInTarget == DragPositionInTarget.Right)
      {
        g.DrawIcon(this.dragIcons[0], drawBounds.X + drawBounds.Width - 8, drawBounds.Y + 16);
        g.DrawIcon(this.dragIcons[1], drawBounds.X + drawBounds.Width - 8, drawBounds.Y - 16);
      }
      else if (this.dragPositionInTarget == DragPositionInTarget.Left)
      {
        g.DrawIcon(this.dragIcons[0], drawBounds.X - 8, drawBounds.Y + 16);
        g.DrawIcon(this.dragIcons[1], drawBounds.X - 8, drawBounds.Y - 16);
      }
      else if (this.dragPositionInTarget == DragPositionInTarget.Up)
      {
        g.DrawIcon(this.dragIcons[2], drawBounds.Right, drawBounds.Y - 8);
        g.DrawIcon(this.dragIcons[3], drawBounds.X - 16, drawBounds.Y - 8);
      }
      else
      {
        if (this.dragPositionInTarget != DragPositionInTarget.Down)
          return;
        g.DrawIcon(this.dragIcons[2], drawBounds.Right, drawBounds.Bottom - 8);
        g.DrawIcon(this.dragIcons[3], drawBounds.X - 16, drawBounds.Bottom - 8);
      }
    }

    private void DrawDragIndicator(HierarchyItem dragItem, Graphics g)
    {
      if (dragItem == null)
        return;
      Rectangle screen1 = this.RectangleToScreen(new Rectangle(dragItem.X + this.columnsHierarchy.X, dragItem.Y + this.ScrollOffset.Y, dragItem.WidthWithChildren, dragItem.HeightWithChildren));
      if (dragItem.IsColumnsHierarchyItem)
      {
        Rectangle rectangle1 = new Rectangle(screen1.X, screen1.Y, screen1.Width / 2, screen1.Height);
        Rectangle rectangle2 = new Rectangle(screen1.X + screen1.Width / 2, screen1.Y, screen1.Width / 2, screen1.Height);
        if (rectangle1.Contains(Cursor.Position))
        {
          this.dragPositionInTarget = DragPositionInTarget.Left;
          this.DrawItemDragIndicator(g, dragItem);
        }
        if (!rectangle2.Contains(Cursor.Position))
          return;
        this.dragPositionInTarget = DragPositionInTarget.Right;
        this.DrawItemDragIndicator(g, dragItem);
      }
      else
      {
        Rectangle screen2 = this.RectangleToScreen(new Rectangle(dragItem.X + this.ScrollOffset.X, dragItem.Y + this.rowsHierarchy.Y, dragItem.WidthWithChildren, dragItem.HeightWithChildren));
        if (dragItem.IsRowsHierarchyItem && this.RowsHierarchy.CompactStyleRenderingEnabled)
          screen2.Height = dragItem.Height;
        Rectangle rectangle1 = new Rectangle(screen2.X, screen2.Y, screen2.Width, screen2.Height / 2);
        Rectangle rectangle2 = new Rectangle(screen2.X, screen2.Y + screen2.Height / 2, screen2.Width, screen2.Height / 2);
        if (rectangle1.Contains(Cursor.Position))
        {
          this.dragPositionInTarget = DragPositionInTarget.Up;
          this.DrawItemDragIndicator(g, dragItem);
        }
        if (!rectangle2.Contains(Cursor.Position))
          return;
        this.dragPositionInTarget = DragPositionInTarget.Down;
        this.DrawItemDragIndicator(g, dragItem);
      }
    }

    private void dragBtn_LocationChanged(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    private void ScrollOnDragging(Point point)
    {
      bool flag = false;
      if (point.X <= 0)
      {
        this.ScrollOffset = new Point(Math.Max(0, -this.ScrollOffset.X - Math.Abs(point.X - this.ClientRectangle.X)), -this.ScrollOffset.Y);
        flag = true;
      }
      else if (point.X >= this.Bounds.Width - 20)
      {
        this.ScrollOffset = new Point(-this.ScrollOffset.X + Math.Abs(point.X - this.ClientRectangle.Right), -this.ScrollOffset.Y);
        flag = true;
      }
      if (point.Y <= 0)
      {
        this.ScrollOffset = new Point(-this.ScrollOffset.X, -this.ScrollOffset.Y - Math.Abs(point.Y - this.ClientRectangle.Y));
        flag = true;
      }
      if (point.Y >= this.Bounds.Height - 20)
      {
        this.ScrollOffset = new Point(-this.ScrollOffset.X, -this.ScrollOffset.Y + Math.Abs(point.Y - this.ClientRectangle.Bottom));
        flag = true;
      }
      if (flag)
      {
        if (!this.scrollTimerStarted)
        {
          this.scrollOnSelectionTimer.Start();
          this.scrollTimerStarted = true;
        }
        this.Invalidate();
        this.RefreshScrollBars();
      }
      else
      {
        this.scrollTimerStarted = false;
        this.scrollOnSelectionTimer.Stop();
      }
    }

    /// <exclude />
    protected internal virtual void OnHierarchyItemDragEnded(HierarchyItemDragEventArgs args)
    {
      if (this.HierarchyItemDragEnded == null)
        return;
      this.HierarchyItemDragEnded((object) this, args);
    }

    /// <exclude />
    protected internal virtual void OnHierarchyItemDragStarted(HierarchyItemDragEventArgs args)
    {
      if (this.HierarchyItemDragStarted == null)
        return;
      this.HierarchyItemDragStarted((object) this, args);
    }

    /// <exclude />
    protected internal virtual void OnHierarchyItemDragStarting(HierarchyItemDragCancelEventArgs args)
    {
      if (this.HierarchyItemDragStarting == null)
        return;
      this.HierarchyItemDragStarting((object) this, args);
    }

    /// <exclude />
    protected internal virtual void OnHierarchyItemDragEnding(HierarchyItemDragCancelEventArgs args)
    {
      if (this.HierarchyItemDragEnding == null)
        return;
      this.HierarchyItemDragEnding((object) this, args);
    }

    /// <exclude />
    protected internal virtual void OnHierarchyItemDrag(HierarchyItemDragEventArgs args)
    {
      if (this.HierarchyItemDrag == null)
        return;
      this.HierarchyItemDrag((object) this, args);
    }

    /// <exclude />
    protected internal virtual bool OnHierarchyItemCustomPaint(HierarchyItem HierarchyItem, Rectangle DrawBounds, Graphics g)
    {
      if (this.HierarchyItemCustomPaint == null)
        return false;
      HierarchyItemPaintEventArgs args = new HierarchyItemPaintEventArgs(HierarchyItem, DrawBounds, g);
      this.HierarchyItemCustomPaint((object) this, args);
      return args.Handled;
    }

    /// <exclude />
    protected internal virtual void OnHierarchyItemExpanding(HierarchyItemCancelEventArgs args)
    {
      if (this.HierarchyItemExpanding == null)
        return;
      this.HierarchyItemExpanding((object) this, args);
    }

    /// <exclude />
    protected internal virtual void OnHierarchyItemCollapsing(HierarchyItemCancelEventArgs args)
    {
      if (this.HierarchyItemCollapsing == null)
        return;
      this.HierarchyItemCollapsing((object) this, args);
    }

    /// <exclude />
    protected internal virtual void OnHierarchyItemSelectionChanged(HierarchyItem item)
    {
      if (this.HierarchyItemSelectionChanged == null)
        return;
      this.HierarchyItemSelectionChanged((object) this, new HierarchyItemEventArgs(item));
    }

    /// <summary>Raises the HierarchyItemMouseDown event.</summary>
    protected virtual void OnHierarchyItemMouseDown(HierarchyItem hierarchyItem, MouseEventArgs e)
    {
      if (this.HierarchyItemMouseDown == null)
        return;
      this.HierarchyItemMouseDown((object) this, new HierarchyItemMouseEventArgs(hierarchyItem, e));
    }

    /// <summary>Raises the HierarchyItemMouseUp event.</summary>
    protected virtual void OnHierarchyItemMouseUp(HierarchyItem hierarchyItem, MouseEventArgs e)
    {
      if (this.HierarchyItemMouseUp == null)
        return;
      this.HierarchyItemMouseUp((object) this, new HierarchyItemMouseEventArgs(hierarchyItem, e));
    }

    /// <summary>Raises the HierarchyItemMouseClick event.</summary>
    protected virtual void OnHierarchyItemMouseClick(HierarchyItem hierarchyItem, MouseEventArgs e)
    {
      if (this.HierarchyItemMouseClick == null)
        return;
      this.HierarchyItemMouseClick((object) this, new HierarchyItemMouseEventArgs(hierarchyItem, e));
    }

    /// <summary>Raises the HierarchyItemMouseDoubleClick event</summary>
    protected virtual void OnHierarchyItemMouseDoubleClick(HierarchyItem hierarchyItem, MouseEventArgs e)
    {
      if (this.HierarchyItemMouseDoubleClick == null)
        return;
      this.HierarchyItemMouseDoubleClick((object) this, new HierarchyItemMouseEventArgs(hierarchyItem, e));
    }

    /// <summary>Raises the ItemExpanded event</summary>
    protected virtual void OnItemExpanded(HierarchyItemEventArgs args)
    {
      if (this.HierarchyItemExpanded == null)
        return;
      this.HierarchyItemExpanded((object) this, args);
    }

    /// <summary>Raises the ItemCollapsed event</summary>
    protected virtual void OnItemCollapsed(HierarchyItemEventArgs args)
    {
      if (this.HierarchyItemCollapsed == null)
        return;
      this.HierarchyItemCollapsed((object) this, args);
    }

    internal bool GetUniqueRowValues(BoundField bf, out Dictionary<object, bool> uniqueRowValues, out System.Type type)
    {
      type = typeof (string);
      uniqueRowValues = new Dictionary<object, bool>();
      PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
      int propertyIndex = this.GetPropertyIndex(itemProperties, bf.DataField);
      if (propertyIndex == -1)
        return false;
      PropertyDescriptor propertyDescriptor = itemProperties[propertyIndex];
      type = propertyDescriptor.PropertyType;
      for (int index = 0; index < this.DataManager.List.Count; ++index)
      {
        object boundFieldValue = this.GetBoundFieldValue(this.DataManager.List[index], itemProperties, bf);
        uniqueRowValues[boundFieldValue] = true;
      }
      return true;
    }

    private int GetPropertyIndex(PropertyDescriptorCollection propCollection, string propertyName)
    {
      if (propCollection == null)
        return -1;
      int index = 0;
      while (index < propCollection.Count && !(propCollection[index].Name == propertyName))
        ++index;
      if (index == propCollection.Count)
        return -1;
      return index;
    }

    protected override void OnBindingContextChanged(EventArgs e)
    {
      if (!this.isBindingSourceSetInProgress)
      {
        this.isBindingSourceSetInProgress = true;
        this.isBindingSourceSetInProgress = false;
      }
      base.OnBindingContextChanged(e);
    }

    private PropertyInfo FindIndexerInMembers(MemberInfo[] members, string stringIndex, out object[] index)
    {
      index = (object[]) null;
      PropertyInfo propertyInfo = (PropertyInfo) null;
      foreach (PropertyInfo member in members)
      {
        if (member != null)
        {
          ParameterInfo[] indexParameters = member.GetIndexParameters();
          if (indexParameters.Length <= 1)
          {
            if (indexParameters[0].ParameterType == typeof (int))
            {
              int result = -1;
              if (int.TryParse(stringIndex.Trim(), NumberStyles.None, (IFormatProvider) CultureInfo.InvariantCulture, out result))
              {
                index = new object[1]
                {
                  (object) result
                };
                return member;
              }
            }
            if (indexParameters[0].ParameterType == typeof (string))
            {
              index = new object[1]
              {
                (object) stringIndex
              };
              propertyInfo = member;
            }
          }
        }
      }
      return propertyInfo;
    }

    internal PropertyInfo GetIndexer(System.Type type, string propertyPath, out object[] index)
    {
      index = (object[]) null;
      if (string.IsNullOrEmpty(propertyPath) || (int) propertyPath[0] != 91)
        return type.GetProperty(propertyPath);
      if (propertyPath.Length < 2 || (int) propertyPath[propertyPath.Length - 1] != 93)
        return (PropertyInfo) null;
      string stringIndex = propertyPath.Substring(1, propertyPath.Length - 2);
      PropertyInfo indexerInMembers = this.FindIndexerInMembers(type.GetDefaultMembers(), stringIndex, out index);
      if (indexerInMembers != null || !typeof (IList).IsAssignableFrom(type))
        return indexerInMembers;
      indexerInMembers = this.FindIndexerInMembers(typeof (IList).GetDefaultMembers(), stringIndex, out index);
      return indexerInMembers;
    }

    private void AutoGenerateBoundFields()
    {
      this.boundFields.Clear();
      this.BoundFields.Clear();
      try
      {
        foreach (PropertyDescriptor itemProperty in this.dataManager.GetItemProperties())
        {
          BoundField boundField = new BoundField(itemProperty.DisplayName, itemProperty.Name);
          this.boundFields.Add(boundField);
          this.BoundFields.Add(boundField);
        }
      }
      catch (Exception)
      {
        this.boundFields.Clear();
        this.BoundFields.Clear();
      }
    }

    private bool ContainsField(BoundItemsCollection<BoundValueField> collection, string dataField)
    {
      foreach (BoundField boundField in collection)
      {
        if (boundField.DataField == dataField)
          return true;
      }
      return false;
    }

    private bool ContainsField(BoundItemsCollection<BoundField> collection, string dataField)
    {
      foreach (BoundField boundField in collection)
      {
        if (boundField.DataField == dataField)
          return true;
      }
      return false;
    }

    /// <summary>Binds data from the data source to the control.</summary>
    public void DataBind()
    {
      if (!this.bindingComplete)
        return;
      bool flag = false;
      lock (this.lockDataBinding)
      {
        this.bindingState = GridBindingState.Unbound;
        if (this.DataSource == null || this.BindingContext == null)
          return;
        this.paintSuspend = true;
        if (this.BindingProgressEnabled)
          this.InitializeBackgroundWorker();
        CurrencyManager local_1;
        try
        {
          local_1 = (CurrencyManager) this.BindingContext[this.DataSource, this.DataMember];
          local_1.ItemChanged -= new ItemChangedEventHandler(this.mngr_ItemChanged);
          local_1.ListChanged -= new ListChangedEventHandler(this.mngr_ListChanged);
          local_1.ItemChanged += new ItemChangedEventHandler(this.mngr_ItemChanged);
          local_1.ListChanged += new ListChangedEventHandler(this.mngr_ListChanged);
        }
        catch (ArgumentException)
        {
          return;
        }
        if (this.dataManager != local_1)
        {
          if (this.dataManager != null)
          {
            this.dataManager.ItemChanged -= new ItemChangedEventHandler(this.mngr_ItemChanged);
            this.dataManager.ListChanged -= new ListChangedEventHandler(this.mngr_ListChanged);
          }
          this.dataManager = local_1;
        }
        this.boundFields.Clear();
        this.pivotRows.Clear();
        this.pivotColumns.Clear();
        this.pivotValueFields.Clear();
        this.groupingColumns.Clear();
        this.boundFieldFilters.Clear();
        foreach (BoundField item_0 in this.BoundFields)
        {
          if (!this.ContainsField(this.boundFields, item_0.DataField))
            this.boundFields.Add(item_0);
        }
        foreach (BoundField item_1 in this.BoundPivotRows)
        {
          if (!this.ContainsField(this.pivotColumns, item_1.DataField) && !this.ContainsField(this.pivotRows, item_1.DataField) && !this.ContainsField(this.pivotValueFields, item_1.DataField))
            this.pivotRows.Add(item_1);
        }
        foreach (BoundField item_2 in this.BoundPivotColumns)
        {
          if (!this.ContainsField(this.pivotColumns, item_2.DataField) && !this.ContainsField(this.pivotRows, item_2.DataField) && !this.ContainsField(this.pivotValueFields, item_2.DataField))
            this.pivotColumns.Add(item_2);
        }
        foreach (BoundValueField item_3 in this.BoundPivotValues)
        {
          if (!this.ContainsField(this.pivotColumns, item_3.DataField) && !this.ContainsField(this.pivotRows, item_3.DataField))
            this.pivotValueFields.Add(item_3);
        }
        foreach (BoundFieldFilter item_4 in this.BoundFieldsFilters)
          this.boundFieldFilters.Add(item_4);
        foreach (BoundField item_5 in this.GroupingColumns)
          this.groupingColumns.Add(item_5);
        if (this.boundFields.Count == 0)
          this.AutoGenerateBoundFields();
        this.bindingComplete = false;
        this.RowsHierarchy.Reset();
        this.ColumnsHierarchy.Reset();
        this.CellsArea.Reset();
        this.vScroll.Visible = false;
        this.hScroll.Visible = false;
        if (this.BindingProgressEnabled)
        {
          flag = this.Enabled;
          this.Enabled = false;
          this.backgroundWorker1.RunWorkerAsync();
        }
        else
          this.RefreshDataFromDataSource((BackgroundWorker) null);
      }
      if (this.BindingProgressEnabled)
      {
        while (!this.bindingComplete)
        {
          Application.DoEvents();
          Thread.Sleep(100);
        }
        this.Enabled = flag;
      }
      else
        this.FinishBinding();
    }

    private void mngr_ListChanged(object sender, ListChangedEventArgs e)
    {
      if (this.AutoUpdateOnListChanged)
        this.DataBind();
      this.OnListChanged(e);
    }

    private void mngr_ItemChanged(object sender, ItemChangedEventArgs e)
    {
    }

    protected virtual void OnListChanged(ListChangedEventArgs args)
    {
      if (this.ListChanged == null)
        return;
      this.ListChanged((object) this, args);
    }

    private void RefreshDataFromDataSource(BackgroundWorker worker)
    {
      this.PaintSuspended = true;
      this.hashPivotItemsToTableRows = new Hashtable();
      bool flag1 = this.pivotValueFields.Count > 0 && (this.pivotRows.Count > 0 || this.pivotColumns.Count > 0);
      if (this.bindingSource.DataSource == null || this.dataManager == null || (this.dataManager.Count == 0 || this.boundFields == null) || this.boundFields.Count == 0)
      {
        this.PaintSuspended = false;
        if (!this.BindingProgressEnabled)
          return;
        worker.ReportProgress(100);
      }
      else
      {
        if (this.BindingStart != null)
          this.BindingStart((object) this, new EventArgs());
        PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
        if (this.dataManager.Count > this.BindingProgressSampleRate && this.BindingProgressEnabled)
          worker.ReportProgress(0);
        Random random = new Random();
        int num1 = this.bindingProgressSampleRate + random.Next(0, 50);
        int[] numArray1 = new int[this.boundFieldFilters.Count];
        int num2 = 0;
        foreach (BoundFieldFilter boundFieldFilter in this.boundFieldFilters)
        {
          int propertyIndex = this.GetPropertyIndex(itemProperties, boundFieldFilter.DataField);
          numArray1[num2++] = propertyIndex;
        }
        if (flag1)
        {
          Hashtable hashPrefixes1 = new Hashtable();
          Hashtable hashPrefixes2 = new Hashtable();
          for (int tableRow = 0; tableRow < this.dataManager.Count; ++tableRow)
          {
            object gridRow = this.dataManager.List[tableRow];
            bool flag2 = false;
            for (int index1 = 0; !flag2 && index1 < numArray1.Length; ++index1)
            {
              int index2 = numArray1[index1];
              if (index2 != -1 && this.IsRowSkipped(gridRow, itemProperties[index2], this.boundFieldFilters[index1].Filters))
                flag2 = true;
            }
            if (!flag2)
            {
              this.AppendPivotHierarchy(tableRow, hashPrefixes1, (Hierarchy) this.RowsHierarchy, itemProperties, this.pivotRows, this.PivotValuesOnRows ? this.pivotValueFields : (BoundItemsCollection<BoundValueField>) null);
              this.AppendPivotHierarchy(tableRow, hashPrefixes2, (Hierarchy) this.ColumnsHierarchy, itemProperties, this.pivotColumns, !this.PivotValuesOnRows ? this.pivotValueFields : (BoundItemsCollection<BoundValueField>) null);
              if (this.BindingProgressEnabled && tableRow % num1 == 0)
              {
                num1 = this.bindingProgressSampleRate + random.Next(0, 50);
                int percentProgress = (int) ((double) tableRow / (double) this.dataManager.Count * 100.0) - 1;
                if (percentProgress < 0)
                  percentProgress = 0;
                worker.ReportProgress(percentProgress);
              }
            }
          }
          this.hashRefItems.Clear();
          if (this.PivotColumnsTotalsEnabled)
            this.SetupTotals((Hierarchy) this.ColumnsHierarchy, this.hashRefItems);
          if (this.PivotRowsTotalsEnabled)
            this.SetupTotals((Hierarchy) this.RowsHierarchy, this.hashRefItems);
          this.bindingState = GridBindingState.BoundPivot;
        }
        else if (this.isGroupingEnabled && this.groupingColumns.Count > 0)
        {
          foreach (BoundField groupingColumn in this.groupingColumns)
            this.ColumnsHierarchy.Items.Add("");
          this.ColumnsHierarchy.UpdateColumnsCount();
          foreach (HierarchyItem hierarchyItem in this.ColumnsHierarchy.Items)
            hierarchyItem.Width = this.RowsHierarchy.CompactStyleRenderingItemsIndent + 2;
          foreach (BoundField boundField in this.boundFields)
            this.CreateBoundColumns(itemProperties, boundField);
          this.hashGroupingRowHeaderItems.Clear();
          Dictionary<string, HierarchyItem> dictionary = new Dictionary<string, HierarchyItem>();
          List<string> stringList = new List<string>(20);
          int[] numArray2 = new int[this.groupingColumns.Count];
          for (int index = 0; index < this.groupingColumns.Count; ++index)
          {
            PropertyDescriptor propertyDescriptor = itemProperties.Find(this.groupingColumns[index].DataField, false);
            int num3 = itemProperties.IndexOf(propertyDescriptor);
            numArray2[index] = num3;
          }
          for (int index1 = 0; index1 < this.RowsCount; ++index1)
          {
            object gridRow = this.dataManager.List[index1];
            stringList.Clear();
            for (int index2 = 0; index2 < this.groupingColumns.Count; ++index2)
            {
              object boundFieldValue = this.GetBoundFieldValue(gridRow, itemProperties, this.groupingColumns[index2]);
              if (boundFieldValue != null)
              {
                string @string = boundFieldValue.ToString();
                stringList.Add(@string);
              }
            }
            if (stringList.Count == this.groupingColumns.Count)
            {
              HierarchyItem hierarchyItem = (HierarchyItem) null;
              int index2 = -1;
              string key = "";
              foreach (string ItemText in stringList)
              {
                ++index2;
                int hashCode = this.groupingColumns[index2].GetHashCode();
                key = string.Format("{0}_{1}_{2}", (object) key, (object) hashCode, (object) ItemText);
                if (dictionary.ContainsKey(key))
                {
                  hierarchyItem = dictionary[key];
                }
                else
                {
                  hierarchyItem = hierarchyItem != null ? hierarchyItem.Items.Add(ItemText) : this.rowsHierarchy.Items.Add(ItemText);
                  hierarchyItem.BoundFieldIndex = -1;
                  dictionary[key] = hierarchyItem;
                  hierarchyItem.Height = HierarchyItem.DefaultGroupRowItemHeight;
                }
              }
              if (hierarchyItem != null)
                hierarchyItem.Items.Add("").BoundFieldIndex = index1;
              if (this.BindingProgressEnabled && index1 % num1 == 0)
              {
                num1 = this.bindingProgressSampleRate + random.Next(0, 50);
                int percentProgress = (int) ((double) index1 / (double) this.dataManager.Count * 100.0) - 1;
                if (percentProgress < 0)
                  percentProgress = 0;
                worker.ReportProgress(percentProgress);
              }
            }
          }
          this.bindingState = GridBindingState.Bound;
        }
        else
        {
          foreach (BoundField boundField in this.boundFields)
            this.CreateBoundColumns(itemProperties, boundField);
          int count = this.dataManager.Count;
          HierarchyItemsCollection items = this.RowsHierarchy.Items;
          for (int index = 0; index < count; ++index)
          {
            HierarchyItem hierarchyItem = items.Add((index + 1).ToString());
            hierarchyItem.BoundFieldIndex = index;
            if (this.BindField != null)
              this.BindField((object) this, new BoundFieldEventArgs(hierarchyItem, (BoundField) null, this));
            if (this.BindingProgressEnabled && index % num1 == 0)
            {
              num1 = this.bindingProgressSampleRate + random.Next(0, 50);
              int percentProgress = (int) ((double) index / (double) this.dataManager.Count * 100.0) - 1;
              if (percentProgress < 0)
                percentProgress = 0;
              worker.ReportProgress(percentProgress);
            }
          }
          this.bindingState = GridBindingState.Bound;
        }
        if (!this.BindingProgressEnabled)
          return;
        worker.ReportProgress(100);
      }
    }

    private bool IsRowSkipped(object gridRow, PropertyDescriptor pi, List<IFilterBase> filters)
    {
      object obj = pi.GetValue(gridRow);
      foreach (IFilterBase filter in filters)
      {
        if (!filter.Evaluate(obj))
          return true;
      }
      return false;
    }

    private void SetupTotals(Hierarchy hierarchy, Dictionary<HierarchyItem, List<HierarchyItem>> hashItemTotals)
    {
      PivotTotalsMode totalsMode;
      if (hierarchy.IsColumnsHierarchy)
      {
        if (!this.PivotColumnsTotalsEnabled)
          return;
        totalsMode = this.pivotColumnsTotalsMode;
      }
      else
      {
        if (!this.PivotRowsTotalsEnabled)
          return;
        totalsMode = this.pivotRowsTotalsMode;
      }
      hierarchy.AddTotals(hierarchy.Items, hashItemTotals, totalsMode);
      if (hierarchy.IsColumnsHierarchy != !this.PivotValuesOnRows)
        return;
      this.AddSummaryItemsToTotals(hierarchy.Items);
    }

    private void AddSummaryItemsToTotals(HierarchyItemsCollection collection)
    {
      foreach (HierarchyItem key in collection)
      {
        if (key.IsTotal)
        {
          if (this.hashRefItems.ContainsKey(key))
          {
            List<HierarchyItem> hierarchyItemList = this.hashRefItems[key];
            for (int index1 = 0; index1 < hierarchyItemList.Count; ++index1)
            {
              if (index1 == 0)
              {
                foreach (HierarchyItem summaryItem in hierarchyItemList[index1].SummaryItems)
                {
                  HierarchyItem hierarchyItem = key.SummaryItems.Add(summaryItem.Caption);
                  hierarchyItem.BoundField = summaryItem.BoundField;
                  hierarchyItem.BoundFieldIndex = summaryItem.BoundFieldIndex;
                  hierarchyItem.IsTotal = true;
                  this.AddRefItem(hierarchyItem, summaryItem);
                }
              }
              else
              {
                for (int index2 = 0; index2 < hierarchyItemList[index1].SummaryItems.Count; ++index2)
                  this.AddRefItem(key.SummaryItems[index2], hierarchyItemList[index1].SummaryItems[index2]);
              }
            }
            this.hashRefItems.Remove(key);
          }
        }
        else
          this.AddSummaryItemsToTotals(key.Items);
      }
    }

    private void AddRefItem(HierarchyItem item, HierarchyItem refItem)
    {
      if (!this.hashRefItems.ContainsKey(item))
        this.hashRefItems.Add(item, new List<HierarchyItem>());
      this.hashRefItems[item].Add(refItem);
    }

    private void CreateBoundColumns(PropertyDescriptorCollection propCollection, BoundField columnBoundField)
    {
      HierarchyItem hierarchyItem = this.ColumnsHierarchy.Items.Add(columnBoundField.Text);
      hierarchyItem.BoundField = columnBoundField;
      this.ApplyBoundFieldProperties(columnBoundField, hierarchyItem);
      if (columnBoundField.DataField.Length > 0 && (int) columnBoundField.DataField[0] == 91)
      {
        object[] index;
        PropertyInfo indexer = this.GetIndexer(this.DataType, columnBoundField.DataField, out index);
        if (indexer != null)
        {
          hierarchyItem.BoundFieldIndex = 0;
          hierarchyItem.ContentType = indexer.PropertyType;
          if (this.BindField == null)
            return;
          this.BindField((object) this, new BoundFieldEventArgs(hierarchyItem, columnBoundField, this));
          return;
        }
      }
      PropertyDescriptor propertyDescriptor = propCollection.Find(columnBoundField.DataField, false);
      if (propertyDescriptor == null || hierarchyItem.BoundFieldIndex != -1)
        return;
      hierarchyItem.BoundFieldIndex = propCollection.IndexOf(propertyDescriptor);
      hierarchyItem.ContentType = propertyDescriptor.PropertyType;
      if (this.BindField == null)
        return;
      this.BindField((object) this, new BoundFieldEventArgs(hierarchyItem, columnBoundField, this));
    }

    private object GetBoundFieldValue(object gridRow, PropertyDescriptorCollection propCollection, BoundField boundField)
    {
      if (boundField == null || propCollection == null)
        return (object) null;
      object[] index = (object[]) null;
      if (boundField.DataField.Length > 0 && (int) boundField.DataField[0] == 91)
      {
        PropertyInfo indexer = this.GetIndexer(gridRow.GetType(), boundField.DataField, out index);
        if (indexer != null)
          return indexer.GetValue(gridRow, index);
      }
      PropertyDescriptor propertyDescriptor = propCollection.Find(boundField.DataField, false);
      if (propertyDescriptor == null)
        return (object) null;
      return propertyDescriptor.GetValue(gridRow);
    }

    private bool GetBoundFieldsValues(object gridRow, PropertyDescriptorCollection propCollection, BoundItemsCollection<BoundField> boundFields, ref ArrayList values)
    {
      foreach (BoundField boundField in boundFields)
      {
        object boundFieldValue = this.GetBoundFieldValue(gridRow, propCollection, boundField);
        if (boundFieldValue != null)
          values.Add(boundFieldValue);
      }
      return true;
    }

    private bool AppendPivotHierarchy(int tableRow, Hashtable hashPrefixes, Hierarchy hierarchy, PropertyDescriptorCollection propCollection, BoundItemsCollection<BoundField> boundFields, BoundItemsCollection<BoundValueField> boundValueFields)
    {
      ArrayList values = new ArrayList(boundFields.Count);
      StringBuilder stringBuilder = new StringBuilder(1024);
      if (!this.GetBoundFieldsValues(this.dataManager.List[tableRow], propCollection, boundFields, ref values))
        return false;
      stringBuilder.Remove(0, stringBuilder.Length);
      HierarchyItem hierarchyItem = (HierarchyItem) null;
      if (values.Count == 0 && boundValueFields != null)
      {
        this.AttachValueFieldAsSummaryItem(tableRow, hashPrefixes, hierarchyItem, stringBuilder.ToString(), boundValueFields);
        return true;
      }
      for (int index = 0; index < values.Count; ++index)
      {
        stringBuilder.Append("!_$%^&_");
        stringBuilder.Append(values[index].ToString());
        if (!hashPrefixes.ContainsKey((object) stringBuilder.ToString()))
        {
          if (hierarchyItem == null)
          {
            hierarchyItem = hierarchy.Items.Add(values[index].ToString());
          }
          else
          {
            this.AddSourceRecordToPivotItem(hierarchyItem, tableRow);
            hierarchyItem = hierarchyItem.Items.Add(values[index].ToString());
          }
          this.ApplyBoundFieldProperties(boundFields[index], hierarchyItem);
          hashPrefixes.Add((object) stringBuilder.ToString(), (object) hierarchyItem);
        }
        else
          hierarchyItem = hashPrefixes[(object) stringBuilder.ToString()] as HierarchyItem;
        if (boundValueFields == null || boundValueFields.Count == 0)
          this.AddSourceRecordToPivotItem(hierarchyItem, tableRow);
        else if (boundValueFields != null)
          this.AttachValueFieldAsSummaryItem(tableRow, hashPrefixes, hierarchyItem, stringBuilder.ToString(), boundValueFields);
      }
      return true;
    }

    private void AttachValueFieldAsSummaryItem(int tableRow, Hashtable hashPrefixes, HierarchyItem item, string strPrefix, BoundItemsCollection<BoundValueField> boundValueFields)
    {
      foreach (BoundValueField boundValueField in boundValueFields)
      {
        string str = strPrefix.ToString() + (object) boundValueField.GetHashCode();
        HierarchyItem hierarchyItem;
        if (hashPrefixes.ContainsKey((object) str))
        {
          hierarchyItem = hashPrefixes[(object) str] as HierarchyItem;
        }
        else
        {
          hierarchyItem = item == null ? (this.PivotValuesOnRows ? (Hierarchy) this.RowsHierarchy : (Hierarchy) this.ColumnsHierarchy).SummaryItems.Add(boundValueField.Text) : item.SummaryItems.Add(boundValueField.Text);
          hashPrefixes[(object) str] = (object) hierarchyItem;
        }
        hierarchyItem.BoundField = (BoundField) boundValueField;
        this.ApplyBoundFieldProperties((BoundField) boundValueField, hierarchyItem);
        this.AddSourceRecordToPivotItem(hierarchyItem, tableRow);
        if (this.BindField != null)
          this.BindField((object) this, new BoundFieldEventArgs(hierarchyItem, hierarchyItem.BoundField, this));
      }
    }

    private void AddSourceRecordToPivotItem(HierarchyItem item, int rowNumber)
    {
      List<int> intList;
      if (this.hashPivotItemsToTableRows.ContainsKey((object) item))
      {
        intList = (List<int>) this.hashPivotItemsToTableRows[(object) item];
      }
      else
      {
        intList = new List<int>();
        this.hashPivotItemsToTableRows.Add((object) item, (object) intList);
      }
      if (intList.Count > 0 && intList[intList.Count - 1] == rowNumber)
        return;
      intList.Add(rowNumber);
    }

    private int[] SortedArrayIntersectAndDedup(int[] arr1, int[] arr2)
    {
      List<int> intList = new List<int>();
      int index1 = 0;
      int index2 = 0;
      while (index1 < arr1.Length && index2 < arr2.Length)
      {
        if (arr1[index1] < arr2[index2])
          ++index1;
        else if (arr1[index1] > arr2[index2])
        {
          ++index2;
        }
        else
        {
          if (intList.Count == 0 || intList[intList.Count - 1] != arr1[index1])
            intList.Add(arr1[index1]);
          ++index1;
          ++index2;
        }
      }
      return intList.ToArray();
    }

    private void GetSortedItemTableRows(HierarchyItem item, ref List<int> arr, bool excludeFiltered)
    {
      if (excludeFiltered && item.Filtered)
        return;
      if (this.hashPivotItemsToTableRows.ContainsKey((object) item))
      {
        List<int> intList = (List<int>) this.hashPivotItemsToTableRows[(object) item];
        arr.AddRange((IEnumerable<int>) intList);
      }
      else
      {
        if (!item.IsTotal || item.ParentItem == null && item.Hierarchy == null || !this.hashRefItems.ContainsKey(item))
          return;
        foreach (HierarchyItem hierarchyItem in this.hashRefItems[item])
        {
          if (!hierarchyItem.Filtered && hierarchyItem != item && !hierarchyItem.IsTotal)
            this.GetSortedItemTableRows(hierarchyItem, ref arr, excludeFiltered);
        }
        arr.Sort();
      }
    }

    /// <summary>
    /// Gets the indexes of the rows set that is used to calculate the value of the grid cell in the pivot table view.
    /// </summary>
    /// <param name="rowItem">Row Hierarchy Item of the grid cell.</param>
    /// <param name="columnItem">Column Hierarchy Item of the grid cell.</param>
    /// <returns>Array of row indexes representing the rows set used to calculate the value of the pivot table cell.</returns>
    public int[] DrillThroughPivotCell(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      return this.InternalDrillThroughPivotCell(rowItem, columnItem, false);
    }

    private int[] InternalDrillThroughPivotCell(HierarchyItem rowItem, HierarchyItem columnItem, bool excludeFilteredItems)
    {
      List<int> arr1 = new List<int>();
      this.GetSortedItemTableRows(columnItem, ref arr1, excludeFilteredItems);
      List<int> arr2 = new List<int>();
      this.GetSortedItemTableRows(rowItem, ref arr2, excludeFilteredItems);
      return this.SortedArrayIntersectAndDedup(arr1.ToArray(), arr2.ToArray());
    }

    private object GetFirstRecord()
    {
      if (this.DataSource == null)
        return (object) null;
      if (!(this.DataSource is IEnumerable))
        return (object) null;
      IEnumerator enumerator = (this.DataSource as IEnumerable).GetEnumerator();
      enumerator.MoveNext();
      return enumerator.Current;
    }

    internal object GetCellValueFromDataSourceNonPivot(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (rowItem.BoundFieldIndex == -1 || columnItem.BoundFieldIndex == -1)
        throw new Exception("Invalid BoundField index.");
      PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
      object component = this.dataManager.List[rowItem.BoundFieldIndex];
      if (columnItem.BoundField.DataField.Length > 0 && (int) columnItem.BoundField.DataField[0] == 91)
      {
        object[] index;
        PropertyInfo indexer = this.GetIndexer(this.DataType, columnItem.BoundField.DataField, out index);
        if (indexer != null)
          return indexer.GetValue(component, index);
      }
      PropertyDescriptor propertyDescriptor = itemProperties[columnItem.BoundFieldIndex];
      if (propertyDescriptor == null)
        return (object) null;
      return propertyDescriptor.GetValue(component);
    }

    internal void SetCellValueFromDataSourceNonPivot(HierarchyItem rowItem, HierarchyItem columnItem, object value)
    {
      if (rowItem.BoundFieldIndex == -1 || columnItem.BoundFieldIndex == -1)
        throw new Exception("Invalid BoundField index.");
      PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
      object component = this.dataManager.List[rowItem.BoundFieldIndex];
      if (columnItem.BoundField.DataField.Length > 0 && (int) columnItem.BoundField.DataField[0] == 91)
      {
        object[] index;
        PropertyInfo indexer = this.GetIndexer(component.GetType(), columnItem.BoundField.DataField, out index);
        if (indexer != null && indexer.CanWrite)
        {
          indexer.SetValue(component, value, index);
          return;
        }
      }
      PropertyDescriptor propertyDescriptor = itemProperties[columnItem.BoundFieldIndex];
      if (propertyDescriptor == null)
        throw new Exception("Invalid grid column index");
      try
      {
        propertyDescriptor.SetValue(component, value);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    internal object GetCellValueFromDataSource(HierarchyItem rowItem, HierarchyItem colItem)
    {
      if (this.dataManager == null)
        return (object) "";
      PropertyDescriptorCollection itemProperties = this.dataManager.GetItemProperties();
      bool excludeFilteredItems = rowItem.IsTotal || colItem.IsTotal;
      int[] numArray = this.InternalDrillThroughPivotCell(rowItem, colItem, excludeFilteredItems);
      if (numArray.Length == 0)
        return (object) "";
      BoundField boundField = this.PivotValuesOnRows ? rowItem.BoundField : colItem.BoundField;
      if (boundField == null || !(boundField is BoundValueField))
        return (object) "";
      BoundValueField boundValueField = (BoundValueField) boundField;
      int num1 = 0;
      double num2 = 0.0;
      double num3 = 0.0;
      double num4 = double.MaxValue;
      double num5 = double.MinValue;
      try
      {
        foreach (int index in numArray)
        {
          object boundFieldValue = this.GetBoundFieldValue(this.dataManager.List[index], itemProperties, (BoundField) boundValueField);
          ++num1;
          switch (boundValueField.Function)
          {
            case PivotFieldFunction.Sum:
            case PivotFieldFunction.Average:
              num2 += Convert.ToDouble(boundFieldValue);
              break;
            case PivotFieldFunction.Max:
              if (num5 < Convert.ToDouble(boundFieldValue))
              {
                num5 = Convert.ToDouble(boundFieldValue);
                break;
              }
              break;
            case PivotFieldFunction.Min:
              if (num4 > Convert.ToDouble(boundFieldValue))
              {
                num4 = Convert.ToDouble(boundFieldValue);
                break;
              }
              break;
            case PivotFieldFunction.Product:
              if (num1 == 1)
              {
                num3 = Convert.ToDouble(boundFieldValue);
                break;
              }
              num3 *= Convert.ToDouble(boundFieldValue);
              break;
          }
        }
      }
      catch (Exception)
      {
        return (object) null;
      }
      if (boundValueField.Function == PivotFieldFunction.Count)
        return (object) num1;
      if (boundValueField.Function == PivotFieldFunction.Sum)
        return (object) num2;
      if (boundValueField.Function == PivotFieldFunction.Average)
        return (object) (num2 / (double) num1);
      if (boundValueField.Function == PivotFieldFunction.Max)
        return (object) num5;
      if (boundValueField.Function == PivotFieldFunction.Min)
        return (object) num4;
      if (boundValueField.Function == PivotFieldFunction.Product)
        return (object) num3;
      return (object) null;
    }

    private void InitializeBackgroundWorker()
    {
      if (this.backgroundWorker1 != null)
        return;
      this.backgroundWorker1 = new BackgroundWorker();
      this.backgroundWorker1.WorkerReportsProgress = true;
      this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.backgroundWorker1_DoWork);
      this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
      this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      this.RefreshDataFromDataSource(sender as BackgroundWorker);
      e.Result = (object) 0;
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      this.FinishBinding();
    }

    private void FinishBinding()
    {
      this.paintSuspend = false;
      this.RowsHierarchy.UpdateVisibleLeaves();
      this.ColumnsHierarchy.UpdateVisibleLeaves();
      if (this.Graphics != null)
      {
        if (!this.ColumnsHierarchy.SuspendWidthsReset)
          this.ColumnsHierarchy.AutoResize();
        if (!this.RowsHierarchy.SuspendWidthsReset)
          this.RowsHierarchy.AutoResize();
      }
      if (this.BindingComplete != null)
        this.BindingComplete((object) this, new EventArgs());
      this.Refresh();
      this.bindingComplete = true;
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      this.UpdateBindingProgress((int) ((double) e.ProgressPercentage * (double) this.dataManager.Count / 100.0), this.dataManager.Count);
    }

    private void ApplyBoundFieldProperties(BoundField bf, HierarchyItem item)
    {
      try
      {
        if (bf.CellsEditor != null)
          item.CellsEditor = bf.CellsEditor;
        if (bf.CellsStyle != null)
          item.CellsStyle = bf.CellsStyle;
        item.Resizable = bf.Resizable;
        item.TextWrap = bf.TextWrap;
        item.AllowFiltering = bf.AllowFiltering;
        item.SortMode = bf.SortMode;
        item.CellsDisplaySettings = bf.CellsDisplaySettings;
        if (bf.CellsFormatProvider != null)
          item.CellsFormatProvider = bf.CellsFormatProvider;
        if (!string.IsNullOrEmpty(bf.CellsFormatString))
          item.CellsFormatString = bf.CellsFormatString;
        item.CellsImageAlignment = bf.CellsImageAlignment;
        item.CellsTextAlignment = bf.CellsTextAlignment;
        item.CellsTextImageRelation = bf.CellsTextImageRelation;
        item.CellsTextWrap = bf.CellsTextWrap;
        if ((double) bf.Width == 0.0)
          return;
        item.Width = bf.Width;
      }
      catch (Exception)
      {
      }
    }

    private void LoadProperties(HierarchyItemsCollection items, Stream stream)
    {
      List<string> stringList = new List<string>();
      foreach (string excludedProperty in this.excludedProperties)
        stringList.Add(excludedProperty);
      XmlTextReader xmlTextReader = new XmlTextReader(stream);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load((XmlReader) xmlTextReader);
      this.TraverseNodes(xmlDocument.ChildNodes);
    }

    private HierarchyItem FindItemByIndex(int itemIndex, int depth, int parentItemIndex, HierarchyItemsCollection items)
    {
      HierarchyItem hierarchyItem1 = (HierarchyItem) null;
      for (int index = 0; index < items.Count; ++index)
      {
        HierarchyItem hierarchyItem2 = items[index];
        if (hierarchyItem2.Depth == depth && hierarchyItem2.ItemIndex == itemIndex)
        {
          HierarchyItem hierarchyItem3;
          if (parentItemIndex == -1 && hierarchyItem2.ParentItem == null)
          {
            hierarchyItem3 = hierarchyItem2;
            return hierarchyItem2;
          }
          if (hierarchyItem2.ParentItem.ItemIndex == parentItemIndex)
          {
            hierarchyItem3 = hierarchyItem2;
            return hierarchyItem2;
          }
        }
        if (hierarchyItem2.Items.Count > 0)
          return this.FindItemByIndex(itemIndex, depth, parentItemIndex, hierarchyItem2.Items);
        if (hierarchyItem2.SummaryItems.Items.Count > 0)
          return this.FindItemByIndex(itemIndex, depth, parentItemIndex, hierarchyItem2.SummaryItems);
      }
      return hierarchyItem1;
    }

    private PropertyInfo[] GetHierarchyItemProperties()
    {
      if (this.hierarchyItemProperties == null)
      {
        this.hierarchyItemProperties = typeof (HierarchyItem).GetProperties();
        foreach (PropertyInfo hierarchyItemProperty in this.hierarchyItemProperties)
          this.hierarchyItemPropertiesDictionary.Add(hierarchyItemProperty.Name, hierarchyItemProperty);
      }
      return this.hierarchyItemProperties;
    }

    private void TraverseNodes(XmlNodeList nodes)
    {
      foreach (XmlNode node in nodes)
      {
        if (node.Attributes != null)
        {
          int itemIndex = -1;
          int parentItemIndex = -1;
          int depth = -1;
          HierarchyItem hierarchyItem = (HierarchyItem) null;
          foreach (XmlAttribute attribute in (XmlNamedNodeMap) node.Attributes)
          {
            if (attribute.Name == "ItemIndex")
              itemIndex = int.Parse(attribute.Value);
            else if (attribute.Name == "ParentItemIndex")
              parentItemIndex = int.Parse(attribute.Value);
            else if (attribute.Name == "Depth")
              depth = int.Parse(attribute.Value);
            else if (itemIndex != -1 && depth != -1)
            {
              if (hierarchyItem == null)
                hierarchyItem = this.FindItemByIndex(itemIndex, depth, parentItemIndex, this.ColumnsHierarchy.Items);
              if (hierarchyItem != null)
              {
                if (this.GetHierarchyItemProperties() != null)
                {
                  PropertyInfo propertyInfo = this.hierarchyItemPropertiesDictionary[attribute.Name];
                  if (propertyInfo.PropertyType == typeof (int))
                  {
                    if (propertyInfo.Name == "Height")
                    {
                      int hieght = int.Parse(attribute.Value);
                      this.ColumnsHierarchy.SetRowHeight(hierarchyItem, hieght);
                    }
                    else
                      propertyInfo.SetValue((object) hierarchyItem, (object) int.Parse(attribute.Value), (object[]) null);
                  }
                  else if (propertyInfo.PropertyType == typeof (bool))
                    propertyInfo.SetValue((object) hierarchyItem, (object) bool.Parse(attribute.Value), (object[]) null);
                  else if (propertyInfo.PropertyType == typeof (string))
                    propertyInfo.SetValue((object) hierarchyItem, (object) attribute.Value, (object[]) null);
                }
              }
              else
                break;
            }
          }
        }
        if (node.HasChildNodes)
          this.TraverseNodes(node.ChildNodes);
      }
    }

    private void SaveProperties(HierarchyItemsCollection items, Stream stream)
    {
      List<string> excludedProperties = new List<string>();
      foreach (string excludedProperty in this.excludedProperties)
        excludedProperties.Add(excludedProperty);
      XmlWriter writer = XmlWriter.Create(stream, new XmlWriterSettings() { Indent = true, IndentChars = "  ", NewLineOnAttributes = true });
      writer.WriteStartDocument();
      writer.WriteStartElement("Settings");
      foreach (HierarchyItem hierarchyItem in this.ColumnsHierarchy.Items)
      {
        writer.WriteStartElement("HierarchyItem");
        this.WriteItemSettings(excludedProperties, writer, hierarchyItem);
        if (hierarchyItem.Items.Count > 0)
        {
          writer.WriteStartElement("HierarchyItem");
          this.SaveInnerItemProperties(excludedProperties, writer, hierarchyItem.Items);
          writer.WriteEndElement();
        }
        if (hierarchyItem.SummaryItems.Count > 0)
        {
          writer.WriteStartElement("HierarchyItem");
          this.SaveInnerItemProperties(excludedProperties, writer, hierarchyItem.SummaryItems);
          writer.WriteEndElement();
        }
        writer.WriteEndElement();
      }
      writer.WriteEndElement();
      writer.WriteEndDocument();
      writer.Flush();
    }

    private void SaveInnerItemProperties(List<string> excludedProperties, XmlWriter writer, HierarchyItemsCollection items)
    {
      foreach (HierarchyItem hierarchyItem in items)
      {
        writer.WriteStartElement("HierarchyItem");
        this.WriteItemSettings(excludedProperties, writer, hierarchyItem);
        if (hierarchyItem.Items.Count > 0)
        {
          writer.WriteStartElement("HierarchyItem");
          this.SaveInnerItemProperties(excludedProperties, writer, hierarchyItem.Items);
          writer.WriteEndElement();
        }
        if (hierarchyItem.SummaryItems.Count > 0)
        {
          writer.WriteStartElement("HierarchyItem");
          this.SaveInnerItemProperties(excludedProperties, writer, hierarchyItem.SummaryItems);
          writer.WriteEndElement();
        }
        writer.WriteEndElement();
      }
    }

    private void WriteItemSettings(List<string> excludedProperties, XmlWriter writer, HierarchyItem item)
    {
      writer.WriteAttributeString("ItemIndex", item.ItemIndex.ToString());
      if (item.ParentItem != null)
        writer.WriteAttributeString("ParentItemIndex", item.ParentItem.ItemIndex.ToString());
      else
        writer.WriteAttributeString("ParentItemIndex", "-1");
      writer.WriteAttributeString("Depth", item.Depth.ToString());
      foreach (PropertyInfo property in item.GetType().GetProperties())
      {
        if (property.CanWrite && property.CanWrite && !excludedProperties.Contains(property.Name))
        {
          object obj = property.GetValue((object) item, (object[]) null);
          if (obj != null)
            writer.WriteAttributeString(property.Name, obj.ToString());
        }
      }
    }

    /// <summary>Loads the columns layout.</summary>
    /// <param name="stream">The stream.</param>
    public void LoadColumnsLayout(Stream stream)
    {
      this.LoadProperties(this.ColumnsHierarchy.Items, stream);
    }

    /// <summary>Saves the layout.</summary>
    /// <returns></returns>
    public void SaveColumnsLayout(Stream stream)
    {
      this.SaveProperties(this.ColumnsHierarchy.Items, stream);
    }

    /// <summary>
    /// Raises the <see cref="E:CellKeyDown" /> event.
    /// </summary>
    protected virtual void OnCellKeyDown(vDataGridView.CellKeyEventArgs args)
    {
      if (this.CellKeyDown == null)
        return;
      this.CellKeyDown((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:CellKeyUp" /> event.
    /// </summary>
    /// <param name="args">The CellKeyEventArgs instance containing the event data.</param>
    protected virtual void OnCellKeyUp(vDataGridView.CellKeyEventArgs args)
    {
      if (this.CellKeyUp == null)
        return;
      this.CellKeyUp((object) this, args);
    }

    /// <exclude />
    protected internal virtual void OnCellValueNeeded(HierarchyItem rowItem, HierarchyItem colItem, out object CellValue, ref int CellImageIndex)
    {
      CellValue = (object) null;
      CellImageIndex = -1;
      if (this.CellValueNeeded == null)
        return;
      CellValueNeededEventArgs args = new CellValueNeededEventArgs(rowItem, colItem);
      this.CellValueNeeded((object) this, args);
      CellValue = args.CellValue != null ? args.CellValue : (object) "";
      CellImageIndex = args.CellImageIndex;
    }

    /// <summary>Raises the CellValueChanging event.</summary>
    /// <param name="rowItem">The row of the grid cell.</param>
    /// <param name="columnItem">The column of the grid cell.</param>
    /// <param name="newValue">The new value of the grid cell.</param>
    /// <returns>Returns True if the event was canceled by the user.</returns>
    protected internal bool OnCellValueChanging(HierarchyItem rowItem, HierarchyItem columnItem, object newValue)
    {
      if (this.CellValueChanging != null)
      {
        CellValueChangingEventArgs args = new CellValueChangingEventArgs(new GridCell(rowItem, columnItem, this.cellsArea), newValue);
        this.CellValueChanging((object) this, args);
        if (args.Cancel)
          return true;
      }
      return false;
    }

    /// <summary>Raises the CellValueChanged event.</summary>
    /// <param name="rowItem">The row of the grid cell.</param>
    /// <param name="columnItem">The column of the grid cell.</param>
    protected internal void OnCellValueChanged(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (this.CellValueChanged == null)
        return;
      this.CellValueChanged((object) this, new CellEventArgs(new GridCell(rowItem, columnItem, this.cellsArea)));
    }

    /// <summary>Raises the CellSelectionChanged event.</summary>
    /// <param name="rowItem">The row of the grid cell.</param>
    /// <param name="columnItem">The column of the grid cell.</param>
    protected internal void OnCellSelectionChanged(HierarchyItem rowItem, HierarchyItem columnItem)
    {
      if (this.CellSelectionChanged == null)
        return;
      this.CellSelectionChanged((object) this, new CellEventArgs(new GridCell(rowItem, columnItem, this.cellsArea)));
    }

    /// <exclude />
    protected internal virtual bool OnCellCustomPaint(HierarchyItem rowItem, HierarchyItem columnItem, CellsArea cellsArea, Rectangle bounds, Graphics g)
    {
      if (this.CellCustomPaint == null)
        return false;
      CellPaintEventArgs args = new CellPaintEventArgs(new GridCell(rowItem, columnItem, cellsArea), bounds, g);
      this.CellCustomPaint((object) this, args);
      return args.Handled;
    }

    /// <exclude />
    protected internal virtual bool OnCellPaintBegin(HierarchyItem rowItem, HierarchyItem columnItem, CellsArea cellsArea, Rectangle bounds, Graphics g)
    {
      if (this.CellPaintBegin == null)
        return false;
      CellCancelEventArgs args = new CellCancelEventArgs(new GridCell(rowItem, columnItem, cellsArea));
      this.CellPaintBegin((object) this, args);
      return args.Cancel;
    }

    /// <summary>Raises the CellParsing event</summary>
    protected virtual void OnCellParsing(CellParsingEventArgs args)
    {
      if (this.CellParsing == null)
        return;
      this.CellParsing((object) this, args);
    }

    /// <summary>Raises the CellMouseClick event</summary>
    protected virtual void OnCellMouseClick(CellMouseEventArgs args)
    {
      if (this.CellMouseClick == null)
        return;
      this.CellMouseClick((object) this, args);
    }

    /// <summary>Raises the CellMousedoubleClick event</summary>
    protected virtual void OnCellMousedoubleClick(CellMouseEventArgs args)
    {
      if (this.CellMouseDoubleClick == null)
        return;
      this.CellMouseDoubleClick((object) this, args);
    }

    /// <summary>Raises the CellValidating event</summary>
    protected virtual void OnCellValidating(CellCancelEventArgs args)
    {
      if (this.CellValidating == null)
        return;
      this.CellValidating((object) this, args);
    }

    /// <summary>Raises the CellValidated event</summary>
    protected virtual void OnCellValidated(CellEventArgs args)
    {
      if (this.CellValidated == null)
        return;
      this.CellValidated((object) this, args);
    }

    /// <summary>Raises the CellBedingEdit event</summary>
    protected virtual void OnCellBeginEdit(CellCancelEventArgs args)
    {
      if (this.CellBeginEdit == null)
        return;
      this.CellBeginEdit((object) this, args);
    }

    /// <summary>Raises the CellEndEdit event</summary>
    protected virtual void OnCellEndEdit(CellCancelEventArgs args)
    {
      if (this.CellEndEdit == null)
        return;
      this.CellEndEdit((object) this, args);
    }

    /// <summary>Raises the CellMouseEnter event</summary>
    protected virtual void OnCellMouseEnter(CellEventArgs args)
    {
      if (this.CellMouseEnter == null)
        return;
      this.CellMouseEnter((object) this, args);
    }

    /// <summary>Raises the CellMouseLeave event</summary>
    protected virtual void OnCellMouseLeave(CellEventArgs args)
    {
      if (this.CellMouseLeave == null)
        return;
      this.CellMouseLeave((object) this, args);
    }

    /// <summary>Raises the CellMouseDown event</summary>
    protected virtual void OnCellMouseDown(CellMouseEventArgs args)
    {
      if (this.CellMouseDown == null)
        return;
      this.CellMouseDown((object) this, args);
    }

    /// <summary>Raises the CellMouseUp event</summary>
    protected virtual void OnCellMouseUp(CellMouseEventArgs args)
    {
      if (this.CellMouseUp == null)
        return;
      this.CellMouseUp((object) this, args);
    }

    /// <summary>Raises the CellMouseMove event</summary>
    protected virtual void OnCellMouseMove(CellMouseEventArgs args)
    {
      if (this.CellMouseMove == null)
        return;
      this.CellMouseMove((object) this, args);
    }

    public delegate void GridGroupHeaderCustomTextNeededEventHandler(object sender, vDataGridView.GroupHeaderCustomTextNeededEventArgs args);

    public delegate void GridTooltipEventHandler(object sender, GridToolTipEventArgs args);

    public delegate void GridBindField(object sender, BoundFieldEventArgs args);

    public delegate void DataBindingCompleteEventHandler(object sender, EventArgs e);

    public delegate void DataBindingStartEventHandler(object sender, EventArgs e);

    private enum COL_RESIZE_STATES
    {
      NO_RESIZE,
      READY_RESIZE,
      RESIZING,
    }

    /// <summary>Enumeration of the grid cells selection mode</summary>
    public enum SELECTION_MODE
    {
      FULL_ROW_SELECT,
      FULL_COLUMN_SELECT,
      CELL_SELECT,
    }

    internal enum INTERNAL_SELECT_MODE
    {
      NO_SELECT = -1,
      COL_SELECT = 0,
      ROW_SELECT = 1,
      CELLS_SELECT = 2,
    }

    protected enum CURSOR_TYPE
    {
      ARROW,
      CROSS,
      COLUMN_RESIZE,
      ROW_RESIZE,
      HAND,
    }

    public delegate void BindingProgressFormatEventHandler(object sender, BindingProgressEventArgs e);

    /// <summary>
    /// Represents a group header custom text needed event arguments.
    /// </summary>
    public class GroupHeaderCustomTextNeededEventArgs : EventArgs
    {
      private HierarchyItem rowItem;
      private HierarchyItemsCollection rows;

      /// <summary>Gets the row.</summary>
      /// <value>The row.</value>
      public HierarchyItem Row
      {
        get
        {
          return this.rowItem;
        }
      }

      /// <summary>
      /// Collection of HierarchyItems that represents the rows in the group.
      /// </summary>
      public HierarchyItemsCollection GroupRows
      {
        get
        {
          return this.rows;
        }
      }

      /// <summary>Gets or sets the custom text of the rows header.</summary>
      public string HeaderText { get; set; }

      /// <summary>Gets or sets the font of the rows header.</summary>
      public Font HeaderFont { get; set; }

      /// <summary>Gets or sets the text color of the header text.</summary>
      public Color HeaderTextColor { get; set; }

      /// <summary>Gets or sets the alignment of the header text.</summary>
      public ContentAlignment HeaderTextAlignment { get; set; }

      /// <summary>Constructor</summary>
      public GroupHeaderCustomTextNeededEventArgs(HierarchyItem rowItem, HierarchyItemsCollection rows)
      {
        this.rows = rows;
        this.rowItem = rowItem;
      }
    }

    public delegate void HierarchyItemMouseEventHandler(object sender, HierarchyItemMouseEventArgs args);

    public delegate void HierarchyItemEventHandler(object sender, HierarchyItemEventArgs args);

    public delegate void HierarchyItemCancelEventHandler(object sender, HierarchyItemCancelEventArgs args);

    public delegate void HierarchyItemPaintEventHandler(object sender, HierarchyItemPaintEventArgs args);

    public delegate void HierarchyItemDragCancelEventHandler(object sender, HierarchyItemDragCancelEventArgs args);

    public delegate void HierarchyItemDragEventHandler(object sender, HierarchyItemDragEventArgs args);

    private sealed class PropertyDescriptorComparer : IComparer
    {
      public int Compare(object objectA, object objectB)
      {
        return string.Compare(((MemberDescriptor) objectA).Name, ((MemberDescriptor) objectB).Name);
      }
    }

    public delegate void CellEventHandler(object sender, CellEventArgs args);

    public delegate void CellCancelEventHandler(object sender, CellCancelEventArgs args);

    public delegate void CellValueChangingEventHandler(object sender, CellValueChangingEventArgs args);

    public delegate void CellMouseEventHandler(object sender, CellMouseEventArgs args);

    public delegate void CellParsingEventHandler(object sender, CellParsingEventArgs args);

    public delegate void CellPaintEventHandler(object sender, CellPaintEventArgs args);

    public delegate void CellValueNeededEventHandler(object sender, CellValueNeededEventArgs args);

    public delegate void CellEditorActivateEventHandler(object sender, EditorActivationCancelEventArgs args);

    public class CellKeyEventArgs : EventArgs
    {
      private GridCell cell;

      /// <summary>The grid cell associated with the event</summary>
      public GridCell Cell
      {
        get
        {
          return this.cell;
        }
      }

      /// <summary>Returns the MouseEventArgs.</summary>
      public KeyEventArgs KeyEventArgs { get; protected set; }

      /// <summary>Constructor</summary>
      /// <param name="cell">GridCell</param>
      /// <param name="args">KeyboardEventArgs</param>
      public CellKeyEventArgs(GridCell cell, KeyEventArgs args)
      {
        this.cell = cell;
        this.KeyEventArgs = args;
      }
    }
  }
}
