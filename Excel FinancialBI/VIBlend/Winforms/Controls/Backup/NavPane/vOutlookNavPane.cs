// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vOutlookNavPane
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents an Outlook style NavigationPane control, which allows the user to select from a list of container controls that can host other controls.
  /// </summary>
  [ToolboxBitmap(typeof (vOutlookNavPane), "ControlIcons.vOutlookNavPane.ico")]
  [Designer("VIBlend.WinForms.Controls.Design.OutlookNavPaneDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Represents an Outlook style NavigationPane control, which allows the user to select from a list of container controls that can host other controls.")]
  [ToolboxItem(true)]
  [DefaultEvent("SelectionChanged")]
  public class vOutlookNavPane : Control, IScrollableControlBase
  {
    private string showMoreButtonsItem = "Show More buttons";
    private string showFewerButtonsItem = "Show Fewer buttons";
    private string addOrRemoveButtons = "Add or Remove buttons";
    private Color paintBorderColor = Color.Transparent;
    private bool paintBorder = true;
    internal List<vOutlookItem> collapsedItem = new List<vOutlookItem>();
    private Panel activePanel = new Panel();
    internal ToolTip paneToolTip = new ToolTip();
    internal Timer timerToolTip = new Timer();
    private bool enableToolTips = true;
    private int toolTipShowDelay = 1500;
    private int toolTipHideDelay = 5000;
    private ToolTipEventArgs toolTipEventArgs = new ToolTipEventArgs();
    private bool allowAnimations = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    /// <summary>
    /// 
    /// 
    /// </summary>
    private string styleKey = "OutlookHeader";
    private Stack<vOutlookItem> hiddenItems = new Stack<vOutlookItem>();
    private int headerHeight = 30;
    private bool useThemeBackground = true;
    private bool useThemeTextColor = true;
    private ContentAlignment textAlign = ContentAlignment.MiddleLeft;
    private Color backgroundTextColor = Color.Black;
    private Color backgroundBorder = Color.Black;
    private Brush headerBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Image upArrowMenuImage;
    private Image downArrowMenuImage;
    private BackgroundElement backFill;
    private bool usePanelBorderColor;
    private OutlookItemsCollection navPaneItems;
    private vOutlookItem lastSelectedItem;
    private vOutlookItem selectedItem;
    private vOutlookStatusItem status;
    internal vOutlookHeader header;
    private vOutlookResizeItem resizer;
    private Point showPoint;
    private vOutlookItem prevItem;
    private vOutlookItem newItem;
    private bool startWaiting;
    private AnimationManager manager;
    private ControlTheme theme;
    private ControlTheme itemsTheme;
    private ImageList largeImageList;
    private ImageList smallImageList;

    /// <summary>Gets or sets up arrow menu image.</summary>
    /// <value>Up arrow menu image.</value>
    [DefaultValue(null)]
    [Description("Gets or sets up arrow menu image.")]
    [Category("Appearance")]
    public Image UpArrowMenuImage
    {
      get
      {
        return this.upArrowMenuImage;
      }
      set
      {
        this.upArrowMenuImage = value;
        if (this.status == null)
          return;
        this.status.InitializeMenu();
      }
    }

    /// <summary>Gets or sets down arrow menu image.</summary>
    /// <value>Down arrow menu image.</value>
    [Description("Gets or sets down arrow menu image.")]
    [DefaultValue(null)]
    [Category("Appearance")]
    public Image DownArrowMenuImage
    {
      get
      {
        return this.downArrowMenuImage;
      }
      set
      {
        this.downArrowMenuImage = value;
        if (this.status == null)
          return;
        this.status.InitializeMenu();
      }
    }

    /// <summary>Gets or sets the show more buttons item.</summary>
    /// <value>The show more buttons item.</value>
    [Category("Behavior")]
    [DefaultValue("Show More buttons")]
    [Description("Gets or sets the ShowFewerButtons item string.")]
    public string ShowMoreButtonsItemString
    {
      get
      {
        return this.showMoreButtonsItem;
      }
      set
      {
        this.showMoreButtonsItem = value;
      }
    }

    /// <summary>Gets or sets the show fewer buttons item.</summary>
    /// <value>The show fewer buttons item.</value>
    [Category("Behavior")]
    [Description("Gets or sets the ShowFewerButtonsItem item string.")]
    [DefaultValue("Show Fewer buttons")]
    public string ShowFewerButtonsItemString
    {
      get
      {
        return this.showFewerButtonsItem;
      }
      set
      {
        this.showFewerButtonsItem = value;
      }
    }

    /// <summary>Gets or sets the add or remove buttons.</summary>
    /// <value>The add or remove buttons.</value>
    [Description("Gets or sets the AddOrRemoveButtons item string.")]
    [DefaultValue("Add or Remove buttons")]
    [Category("Behavior")]
    public string AddOrRemoveButtonsString
    {
      get
      {
        return this.addOrRemoveButtons;
      }
      set
      {
        this.addOrRemoveButtons = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the ControlBorderColor
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the ControlBorderColor")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public virtual bool UseControlBorderColor
    {
      get
      {
        return this.usePanelBorderColor;
      }
      set
      {
        this.usePanelBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the BorderColor of the Panel.</summary>
    [DefaultValue(typeof (Color), "Transparent")]
    [Description("Gets or sets the BorderColor of the Control.")]
    [Category("Appearance")]
    public virtual Color ControlBorderColor
    {
      get
      {
        return this.paintBorderColor;
      }
      set
      {
        this.paintBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a border
    /// </summary>
    /// <value><c>true</c> if [paint border]; otherwise, <c>false</c>.</value>
    [Description("Gets or sets a value indicating whether to paint a border")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool PaintBorder
    {
      get
      {
        return this.paintBorder;
      }
      set
      {
        this.paintBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the status control.</summary>
    [Browsable(false)]
    public vOutlookStatusItem StatusItem
    {
      get
      {
        return this.status;
      }
    }

    /// <summary>Gets or sets the resize control.</summary>
    [Browsable(false)]
    public vOutlookResizeItem ResizeItem
    {
      get
      {
        return this.resizer;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether tool tips are enabled.
    /// </summary>
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether tool tips are enabled.")]
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
      }
    }

    /// <summary>Gets or sets whether the control is enabled.</summary>
    [Category("Behavior")]
    [Description("Gets or sets whether the control is enabled.")]
    public new bool Enabled
    {
      get
      {
        return base.Enabled;
      }
      set
      {
        base.Enabled = value;
        foreach (Control control in this.Items)
          control.Enabled = value;
        if (this.resizer != null)
          this.resizer.Enabled = value;
        if (this.status == null)
          return;
        this.status.Enabled = value;
      }
    }

    /// <summary>Gets or sets the tool tip show delay.</summary>
    /// <value>The tool tip show delay.</value>
    [DefaultValue(1500)]
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
        this.timerToolTip.Interval = value;
      }
    }

    /// <summary>Gets or sets the duration of the tool tip.</summary>
    /// <value>The duration of the tool tip.</value>
    [Description("Tooltip duration in milliseconds ")]
    [Category("Behavior")]
    [DefaultValue(5000)]
    public int ToolTipDuration
    {
      get
      {
        return this.toolTipHideDelay;
      }
      set
      {
        this.toolTipHideDelay = value;
      }
    }

    /// <summary>
    /// Gets a reference to the content panel of the active item.
    /// </summary>
    [Description("Gets a reference to the content panel of the active item.")]
    [Browsable(false)]
    public Panel ActivePanel
    {
      get
      {
        return this.activePanel;
      }
      internal set
      {
        this.activePanel = value;
      }
    }

    /// <summary>Gets the header.</summary>
    /// <value>The header.</value>
    [Browsable(false)]
    public vOutlookHeader Header
    {
      get
      {
        return this.header;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.manager == null)
          this.manager = new AnimationManager((Control) this);
        return this.manager;
      }
      set
      {
        this.manager = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [DefaultValue(true)]
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

    /// <summary>Gets or sets the theme of the control.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the theme of the control.")]
    [Category("Appearance")]
    [Browsable(false)]
    public ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null)
          return;
        this.theme = value.CreateCopy();
        this.ItemsTheme = this.theme;
        this.resizer.Theme = this.theme;
        this.status.Theme = this.theme;
        this.backFill.LoadTheme(this.theme);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Description("Gets or sets the theme of the control using one of the built-in themes.")]
    [Category("Appearance")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        if (value == this.defaultTheme)
          return;
        this.defaultTheme = value;
        ControlTheme defaultTheme;
        try
        {
          defaultTheme = ControlTheme.GetDefaultTheme(this.defaultTheme);
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.Message);
          return;
        }
        if (defaultTheme != null)
          this.Theme = defaultTheme;
        this.resizer.VIBlendTheme = value;
        this.status.VIBlendTheme = value;
      }
    }

    /// <summary>Gets or sets the style key.</summary>
    /// <value>The style key.</value>
    protected internal virtual string StyleKey
    {
      get
      {
        return this.styleKey;
      }
      set
      {
        this.styleKey = value;
      }
    }

    /// <summary>Gets or sets the theme.</summary>
    /// <value>The theme.</value>
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets button's theme")]
    [Browsable(false)]
    internal ControlTheme ItemsTheme
    {
      get
      {
        return this.itemsTheme;
      }
      set
      {
        if (value == null)
          return;
        this.itemsTheme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        this.backFill.IsAnimated = this.AllowAnimations;
        Color color1 = this.itemsTheme.QueryColorSetter("NavTextColorNormal");
        if (color1 != Color.Empty)
          this.itemsTheme.StyleNormal.TextColor = color1;
        Color color2 = this.itemsTheme.QueryColorSetter("NavTextColorPressed");
        if (color2 != Color.Empty)
          this.itemsTheme.StylePressed.TextColor = color2;
        Color color3 = this.itemsTheme.QueryColorSetter("NavTextColorHighlight");
        if (color3 != Color.Empty)
          this.itemsTheme.StyleHighlight.TextColor = color3;
        Color color4 = this.itemsTheme.QueryColorSetter("NavBorderNormal");
        if (color4 != Color.Empty)
          this.itemsTheme.StyleNormal.BorderColor = color4;
        FillStyle fillStyle1 = this.itemsTheme.QueryFillStyleSetter("NavNormal");
        if (fillStyle1 != null)
          this.itemsTheme.StyleNormal.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.itemsTheme.QueryFillStyleSetter("NavHover");
        if (fillStyle2 != null)
          this.itemsTheme.StyleHighlight.FillStyle = fillStyle2;
        FillStyle fillStyle3 = this.itemsTheme.QueryFillStyleSetter("NavPressed");
        if (fillStyle3 != null)
          this.itemsTheme.StylePressed.FillStyle = fillStyle3;
        if (this.header != null)
          this.header.backFill.LoadTheme(this.itemsTheme);
        foreach (vOutlookItem vOutlookItem in this.Items)
        {
          if (vOutlookItem.HeaderControl != null)
            vOutlookItem.HeaderControl.backFill.LoadTheme(this.itemsTheme);
        }
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the list of large images to display on the control's items.
    /// </summary>
    [DefaultValue(null)]
    [Description("Gets or sets the list of large images to display on the control's items.")]
    [Category("Behavior")]
    public virtual ImageList LargeImageList
    {
      get
      {
        return this.largeImageList;
      }
      set
      {
        this.largeImageList = value;
      }
    }

    /// <summary>
    /// Gets or sets the list of small images to display on the control's items when the items are not expanded.
    /// </summary>
    [DefaultValue(null)]
    [Category("Behavior")]
    [Description("Gets or sets the list of small images to display on the control's items when the items are not expanded.")]
    public virtual ImageList SmallImageList
    {
      get
      {
        return this.smallImageList;
      }
      set
      {
        this.smallImageList = value;
      }
    }

    /// <summary>Gets or sets the selected item</summary>
    [Description("Gets or sets the selected item")]
    [Category("Behavior")]
    public vOutlookItem SelectedItem
    {
      get
      {
        return this.selectedItem;
      }
      set
      {
        if (value == null || !this.Items.Contains(value))
          return;
        this.CallHeaderClick(value.HeaderControl);
      }
    }

    /// <summary>
    /// Gets the collection of panes in this navigationpane control.
    /// </summary>
    [Category("Data")]
    [Editor("System.ComponentModel.Design.CollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [Description("Gets the collection of panes in this navigationpane control.")]
    [MergableProperty(false)]
    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public OutlookItemsCollection Items
    {
      get
      {
        return this.navPaneItems;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use theme's background")]
    [DefaultValue(true)]
    public bool UseHeaderThemeBackground
    {
      get
      {
        return this.useThemeBackground;
      }
      set
      {
        if (value == this.useThemeBackground)
          return;
        this.useThemeBackground = value;
        if (this.Header == null)
          return;
        this.Header.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's text color
    /// </summary>
    [Description("Gets or sets a value indicating whether to use theme's text color")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool UseHeaderThemeTextColor
    {
      get
      {
        return this.useThemeTextColor;
      }
      set
      {
        if (value == this.useThemeTextColor)
          return;
        this.useThemeTextColor = value;
        if (this.Header == null)
          return;
        this.Header.Invalidate();
      }
    }

    /// <summary>Gets or sets the text alignment.</summary>
    /// <value>The text alignment.</value>
    [Description("Gets or sets the header's text alignment.")]
    [Category("Appearance")]
    public ContentAlignment HeaderTextAlignment
    {
      get
      {
        return this.textAlign;
      }
      set
      {
        this.textAlign = value;
        if (this.Header == null)
          return;
        this.Header.Invalidate();
      }
    }

    /// <summary>Gets or sets the  TextColor.</summary>
    /// <value>The  TextColor.</value>
    [Description("Gets or sets the header's TextColor.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    public Color HeaderTextColor
    {
      get
      {
        return this.backgroundTextColor;
      }
      set
      {
        this.backgroundTextColor = value;
        if (this.Header == null)
          return;
        this.Header.Invalidate();
      }
    }

    /// <summary>Gets or sets the background border.</summary>
    /// <value>The background border.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the background border.")]
    [Category("Appearance")]
    public Color HeaderBackgroundBorder
    {
      get
      {
        return this.backgroundBorder;
      }
      set
      {
        this.backgroundBorder = value;
        if (this.Header == null)
          return;
        this.Header.Invalidate();
      }
    }

    /// <summary>Gets or sets the HighlightBackground brush.</summary>
    /// <value>The HighlightBackground brush.</value>
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the HighlightBackground brush.")]
    public Brush HeaderBackgroundBrush
    {
      get
      {
        return this.headerBackgroundBrush;
      }
      set
      {
        this.headerBackgroundBrush = value;
        if (this.Header == null)
          return;
        this.Header.Invalidate();
      }
    }

    /// <summary>Gets or sets the header Font.</summary>
    [Description("Gets or sets the header Font.")]
    [Category("Appearance")]
    public Font HeaderFont
    {
      get
      {
        return this.header.Font;
      }
      set
      {
        this.header.Font = value;
      }
    }

    /// <summary>Gets or sets the header height</summary>
    [DefaultValue(30)]
    [Category("Appearance")]
    [Description("Gets or sets the header height")]
    public int HeaderHeight
    {
      get
      {
        return this.headerHeight;
      }
      set
      {
        this.headerHeight = value;
        this.Layout(false);
      }
    }

    /// <summary>Gets or sets whether the control is visible.</summary>
    public new bool Visible
    {
      get
      {
        return base.Visible;
      }
      set
      {
        base.Visible = value;
        this.Layout(false);
      }
    }

    /// <summary>Occurs when the splitter position has changed</summary>
    [Category("Action")]
    public event EventHandler SplitterPositionChanged;

    /// <summary>Occurs when the selected item has changed.</summary>
    [Description("Occurs when the selected item has changed.")]
    [Category("Action")]
    public event EventHandler<vOutlookNavItemEventArgs> SelectionChanged;

    /// <summary>Occurs when the selected item is changing.</summary>
    [Category("Action")]
    [Description("Occurs when the selected item is changing.")]
    public event EventHandler<vOutlookNavItemCancelEventArgs> SelectionChanging;

    [Category("Action")]
    public event EventHandler<DrawOutlookNavPaneEventArgs> DrawItemHeader;

    [Category("Action")]
    public event EventHandler<DrawOutlookNavPaneStatusEventArgs> DrawItemStatusPart;

    [Category("Action")]
    public event EventHandler<DrawOutlookNavPaneResizeItemEventArgs> DrawItemResizePart;

    /// <summary>Occurs when tooltip shows.</summary>
    [Category("Action")]
    public event ToolTipShownHandler TooltipShow;

    static vOutlookNavPane()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vOutlookNavPane()
    {
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      vOutlookItem vOutlookItem = new vOutlookItem();
      vOutlookItem.navPane = this;
      this.navPaneItems = new OutlookItemsCollection();
      this.status = new vOutlookStatusItem(this);
      this.header = new vOutlookHeader(vOutlookItem);
      this.header.enableStates = false;
      this.resizer = new vOutlookResizeItem(this);
      this.Controls.Add((Control) this.status);
      this.Controls.Add((Control) this.header);
      this.Controls.Add((Control) this.resizer);
      this.Controls.Add((Control) this.activePanel);
      this.ControlAdded += new ControlEventHandler(this.NavPane_ControlAdded);
      this.ControlRemoved += new ControlEventHandler(this.NavPane_ControlRemoved);
      this.navPaneItems.CollectionChanged += new EventHandler(this.navPaneItems_CollectionChanged);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.timerToolTip.Interval = this.ToolTipShowDelay;
      this.timerToolTip.Tick += new EventHandler(this.timerToolTip_Tick);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.activePanel.BackColor = Color.White;
    }

    /// <summary>
    /// Raises the <see cref="E:DrawItemHeader" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.DrawOutlookNavPaneEventArgs" /> instance containing the event data.</param>
    protected internal virtual void OnDrawItemHeader(DrawOutlookNavPaneEventArgs args)
    {
      if (this.DrawItemHeader == null)
        return;
      this.DrawItemHeader((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:DrawItemHeader" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.DrawOutlookNavPaneEventArgs" /> instance containing the event data.</param>
    protected internal virtual void OnDrawItemStatusPart(DrawOutlookNavPaneStatusEventArgs args)
    {
      if (this.DrawItemStatusPart == null)
        return;
      this.DrawItemStatusPart((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:DrawItemHeader" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.DrawOutlookNavPaneEventArgs" /> instance containing the event data.</param>
    protected internal virtual void OnDrawItemResizePart(DrawOutlookNavPaneResizeItemEventArgs args)
    {
      if (this.DrawItemResizePart == null)
        return;
      this.DrawItemResizePart((object) this, args);
    }

    /// <summary>Raises the SelectionChanged event</summary>
    protected internal virtual void OnSelectionChanged(vOutlookNavItemEventArgs args)
    {
      if (this.SelectionChanged == null)
        return;
      this.SelectionChanged((object) this, args);
    }

    /// <summary>Raises the SelectionChanging event</summary>
    protected internal virtual void OnSelectionChanging(vOutlookNavItemCancelEventArgs args)
    {
      if (this.SelectionChanging == null)
        return;
      this.SelectionChanging((object) this, args);
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.enableToolTips = false;
        this.timerToolTip.Dispose();
      }
      base.Dispose(disposing);
    }

    /// <summary>Raises the SplitterPositionChanged event</summary>
    protected internal virtual void OnSplitterPositionChanged()
    {
      if (this.SplitterPositionChanged == null)
        return;
      this.SplitterPositionChanged((object) this, EventArgs.Empty);
    }

    private void timerToolTip_Tick(object sender, EventArgs e)
    {
      if (!this.startWaiting)
        return;
      this.timerToolTip.Stop();
      if (this.toolTipEventArgs != null)
        this.OnToolTipShown(this.toolTipEventArgs);
      this.startWaiting = false;
    }

    protected internal virtual void OnToolTipShown(ToolTipEventArgs args)
    {
      if (this.TooltipShow != null)
        this.TooltipShow((object) this, args);
      string str = "";
      if (!string.IsNullOrEmpty(str))
        str = args.ToolTipText;
      if (this.newItem != null)
        str = this.newItem.TooltipText;
      this.paneToolTip.SetToolTip((Control) this.newItem.HeaderControl, str);
      if (this.newItem == null || string.IsNullOrEmpty(str))
        return;
      Rectangle bounds = this.newItem.HeaderControl.Bounds;
      this.showPoint = this.PointToClient(Cursor.Position);
      this.paneToolTip.Show(str, (IWin32Window) this.newItem.HeaderControl, new Point(this.newItem.PointToClient(Cursor.Position).X, this.newItem.HeaderControl.PointToClient(Cursor.Position).Y + this.newItem.HeaderHeight), this.toolTipHideDelay);
    }

    internal void StartTimer()
    {
      if (!this.EnableToolTips)
        return;
      this.newItem = this.HitTest();
      if (this.newItem != null && this.prevItem != this.newItem)
      {
        this.paneToolTip.RemoveAll();
        if (this.prevItem != null && this.prevItem.HeaderControl != null)
          this.paneToolTip.Hide((IWin32Window) this.prevItem.HeaderControl);
        this.startWaiting = true;
        this.timerToolTip.Start();
        this.showPoint = this.newItem.HeaderControl.PointToClient(Cursor.Position);
      }
      this.prevItem = this.newItem;
    }

    /// <summary>
    /// HitTest to find whether on the point position there is nav pane item
    /// </summary>
    /// <param name="pt">The pt.</param>
    /// <returns></returns>
    public vOutlookItem HitTest()
    {
      foreach (vOutlookItem vOutlookItem in this.Items)
      {
        if (vOutlookItem.HeaderControl.Bounds.Contains(vOutlookItem.HeaderControl.PointToClient(Cursor.Position)))
          return vOutlookItem;
      }
      return (vOutlookItem) null;
    }

    /// <summary>Raises the Layout event.</summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.Layout(false);
    }

    private void navPaneItems_CollectionChanged(object sender, EventArgs e)
    {
      this.DoCollectionChange();
    }

    private void DoCollectionChange()
    {
      foreach (vOutlookItem vOutlookItem in this.Items)
      {
        vOutlookItem.HeaderControl.Click -= new EventHandler(this.HeaderControl_Click);
        vOutlookItem.SizeChanged -= new EventHandler(this.item_SizeChanged);
        if (vOutlookItem.Parent != null)
          vOutlookItem.Parent.Controls.Remove((Control) vOutlookItem);
      }
      this.Controls.Clear();
      this.Controls.Add((Control) this.status);
      this.Controls.Add((Control) this.header);
      this.Controls.Add((Control) this.resizer);
      this.Controls.Add((Control) this.activePanel);
      int num = 0;
      foreach (vOutlookItem vOutlookItem in this.Items)
      {
        vOutlookItem.IsExpanded = false;
        vOutlookItem.Visible = true;
        if (num == 0)
          this.CallHeaderClick(vOutlookItem.HeaderControl);
        ++num;
        this.Controls.Add((Control) vOutlookItem);
        vOutlookItem.Visible = true;
        vOutlookItem.HeaderControl.Click += new EventHandler(this.HeaderControl_Click);
        vOutlookItem.SizeChanged += new EventHandler(this.item_SizeChanged);
        if (vOutlookItem.HeaderControl.backFill != null)
          vOutlookItem.HeaderControl.backFill.LoadTheme(this.ItemsTheme);
      }
      this.Layout(true);
    }

    private void item_SizeChanged(object sender, EventArgs e)
    {
      this.Layout(false);
    }

    private bool SetPanelToHostChildren(Control childControl, string controlName)
    {
      try
      {
        INestedContainer nestedContainer = this.GetService(typeof (INestedContainer)) as INestedContainer;
        if (nestedContainer == null)
          return false;
        for (int index = 0; index < nestedContainer.Components.Count; ++index)
        {
          if (nestedContainer.Components[index].Equals((object) childControl))
            return true;
        }
        nestedContainer.Add((IComponent) childControl, controlName);
        return true;
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
      }
      return false;
    }

    /// <summary>Selects a specific navigation pane item</summary>
    /// <param name="item">The Item to select.</param>
    public void SelectItem(vOutlookItem item)
    {
      this.CallHeaderClick(item.HeaderControl);
    }

    internal void CallHeaderClick(vOutlookHeader item)
    {
      this.HeaderControl_Click((object) item, EventArgs.Empty);
    }

    private void HeaderControl_Click(object sender, EventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      vOutlookHeader vOutlookHeader = (vOutlookHeader) sender;
      vOutlookNavItemCancelEventArgs args = new vOutlookNavItemCancelEventArgs(vOutlookHeader.NavPaneItem);
      this.OnSelectionChanging(args);
      if (args.Cancel)
        return;
      foreach (vOutlookItem vOutlookItem in this.Items)
      {
        if (vOutlookItem.IsExpanded)
          this.lastSelectedItem = vOutlookItem;
        vOutlookItem.IsExpanded = false;
        vOutlookItem.HeaderControl.Invalidate();
      }
      vOutlookHeader.NavPaneItem.IsExpanded = true;
      this.selectedItem = vOutlookHeader.NavPaneItem;
      this.header.Text = this.selectedItem.HeaderText;
      if (this.lastSelectedItem == this.selectedItem)
        return;
      this.header.Text = this.selectedItem.HeaderText;
      this.header.Invalidate();
      this.Controls.Remove((Control) this.ActivePanel);
      this.ActivePanel = (Panel) null;
      this.ActivePanel = this.selectedItem.Panel;
      if (this.DesignMode)
        this.SetPanelToHostChildren((Control) this.ActivePanel, "ActivePanel" + (object) this.Items.IndexOf(this.selectedItem));
      this.Controls.Add((Control) this.ActivePanel);
      this.OnSelectionChanged(new vOutlookNavItemEventArgs(this.selectedItem));
      this.selectedItem.CallOnClick();
    }

    private void NavPane_ControlRemoved(object sender, ControlEventArgs e)
    {
      this.Layout(false);
    }

    private void NavPane_ControlAdded(object sender, ControlEventArgs e)
    {
      this.Layout(false);
    }

    /// <summary>Hides the first visible item group</summary>
    public void HideFirstGroup()
    {
      foreach (vOutlookItem vOutlookItem in this.Items)
      {
        if (vOutlookItem.Visible && vOutlookItem.AutoHide)
        {
          this.hiddenItems.Push(vOutlookItem);
          vOutlookItem.Visible = false;
          break;
        }
      }
      this.Layout(false);
    }

    internal vOutlookItem GetTopVisibleItem()
    {
      foreach (vOutlookItem vOutlookItem in this.Items)
      {
        if (vOutlookItem.Visible || !this.Visible)
          return vOutlookItem;
      }
      return (vOutlookItem) null;
    }

    /// <summary>Shows the first hidden item group</summary>
    public void ShowFirstGroup()
    {
      if (this.hiddenItems.Count <= 0)
        return;
      vOutlookItem vOutlookItem = this.hiddenItems.Pop();
      if (this.collapsedItem.Contains(vOutlookItem))
        return;
      vOutlookItem.Visible = true;
      this.Layout(false);
    }

    private void ResetHeaderTextAlignment()
    {
      this.HeaderTextAlignment = ContentAlignment.MiddleLeft;
    }

    private bool ShouldSerializeHeaderTextAlignment()
    {
      return this.HeaderTextAlignment != ContentAlignment.MiddleLeft;
    }

    private void ResetHeaderFont()
    {
      this.HeaderFont = Control.DefaultFont;
    }

    private bool ShouldSerializeHeaderFont()
    {
      return !this.HeaderFont.Equals((object) Control.DefaultFont);
    }

    private void Layout(bool fromLayout)
    {
      if (fromLayout)
      {
        foreach (vOutlookItem navPaneItem in this.navPaneItems)
        {
          if (navPaneItem.Parent != this)
          {
            try
            {
              navPaneItem.Parent.Controls.Remove((Control) navPaneItem);
            }
            catch (Exception ex)
            {
              Trace.WriteLine(ex.Message);
            }
            this.Controls.Add((Control) navPaneItem);
          }
        }
      }
      if (this.StatusItem.Menu != null && this.StatusItem.Menu.Items.Count == 0)
        this.StatusItem.InitializeMenu();
      this.status.Location = new Point(0, this.Height - this.status.Height);
      this.status.Width = this.Width;
      int y = this.Height - this.status.Height;
      this.header.Width = this.Width;
      this.header.Location = new Point(0, 0);
      this.header.Height = this.HeaderHeight;
      if (!this.status.Visible)
        y = this.Height;
      for (int index = this.navPaneItems.Count - 1; index >= 0; --index)
      {
        vOutlookItem vOutlookItem = this.navPaneItems[index];
        if (vOutlookItem.Visible)
        {
          vOutlookItem.Height = vOutlookItem.HeaderHeight;
          vOutlookItem.Width = this.Width;
          y -= vOutlookItem.Height - 1;
          vOutlookItem.Location = new Point(0, y);
        }
      }
      this.resizer.Size = new Size(this.Width, this.resizer.Height);
      vOutlookItem topVisibleItem = this.GetTopVisibleItem();
      if (topVisibleItem != null)
        this.resizer.Location = new Point(0, topVisibleItem.Location.Y - this.resizer.Height);
      else if (this.status.Visible)
        this.resizer.Location = new Point(0, this.status.Location.Y - this.resizer.Height);
      else
        this.resizer.Location = new Point(0, this.Height - this.resizer.Height);
      if (this.ActivePanel == null || this.navPaneItems.Count <= 0)
        return;
      this.ActivePanel.Location = new Point(1, this.header.Height);
      this.ActivePanel.Size = new Size(this.Width - 2, this.resizer.Location.Y - (this.header.Location.Y + this.header.Height));
    }

    private void EnsureOneVisible()
    {
      foreach (Control navPaneItem in this.navPaneItems)
      {
        if (navPaneItem.Visible)
          return;
      }
      if (this.navPaneItems.Count <= 0)
        return;
      this.navPaneItems[0].Visible = true;
    }

    /// <summary>Raises the Paint event.</summary>
    /// <param name="e">A PaintEventArgs that contains the event data. </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      this.Layout(false);
      Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      if (this.backFill == null || !this.PaintBorder)
        return;
      this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      if (!this.UseControlBorderColor)
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      else
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal, this.ControlBorderColor);
    }
  }
}
