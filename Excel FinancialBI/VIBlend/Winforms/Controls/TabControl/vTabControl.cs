// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTabControl
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  [Description("Displays a collection of tabs that contain other controls and components, and allows the user to customize their look & feel.")]
  [ToolboxBitmap(typeof (vTabControl), "ControlIcons.vTabControl.ico")]
  [DefaultEvent("SelectedIndexChanged")]
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vTabControlDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vTabControl : Panel, IScrollableControlBase
  {
    private StringFormat sfTabs = new StringFormat();
    private int titleHeight = 45;
    private vTabPageTextOrientation textOrientation = vTabPageTextOrientation.Horizontal;
    private TabsShape tabsShape = TabsShape.Office2007;
    private int tabsSpacing = 5;
    private int tabsInitialOffset = 30;
    private int allPagesHeight = -1;
    private bool allowScrollingAnimation = true;
    protected int otherControls = 3;
    private vButton deleteButton = new vButton();
    internal ToolTip tabStripToolTip = new ToolTip();
    private Timer timerToolTip = new Timer();
    private int toolTipShowDelay = 1500;
    private int toolTipHideDelay = 5000;
    private ToolTipEventArgs toolTipEventArgs = new ToolTipEventArgs();
    private List<TabContext> tabContexts = new List<TabContext>();
    protected ToolStripDropDown dropDown = new ToolStripDropDown();
    protected Size dropDownSize = Size.Empty;
    private bool showCloseButtonOnSelectedTabOnly = true;
    private string styleKey = "TabControl";
    private Color pressedAndHightlightedBorder = Color.Empty;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private Stack<int> vScrollStep = new Stack<int>();
    private Timer timer = new Timer();
    private float animationCurrentValue = 0.5f;
    private float animationValue = 0.5f;
    private Stack<int> scrollStep = new Stack<int>();
    private PaintHelper helper = new PaintHelper();
    private int cornerRadius = 10;
    private Color tabsAreaBackColor = Color.Transparent;
    private Color tabsAreaBorderColor = Color.Transparent;
    protected PaintHelper paintHelper = new PaintHelper();
    protected bool allowAnimations = true;
    public static List<vTabControl> DropTabControlTargets = new List<vTabControl>();
    protected Point dropDownPosition;
    private ImageList imageList;
    private vTabPage selectedTab;
    private int[] widthPages;
    private vTabPageAlignment tabAlignment;
    private vHScrollBar hScrollBar;
    private vVScrollBar vScrollBar;
    protected BackgroundElement backGround;
    private AnimationManager animationManager;
    private bool allowDragAndDrop;
    private Timer scrollingTimer;
    private vTabCollection tabCollection;
    private bool fitsTabsToBounds;
    internal bool initializedPagesWidth;
    private bool enableToolTips;
    private int lastPageNullCounter;
    private bool enableDropDownStyle;
    private int contextTabsOffset;
    private vTabPage lastSelectedTab;
    private bool enableCloseButtons;
    private bool showFocusRectangle;
    private ControlTheme theme;
    private int oldScrollValue;
    private int animationStartValue;
    private int animationEndValue;
    private bool animateForward;
    private bool allowDeleteOnMouseWheelButton;
    private bool allowCloseButton;
    private vTabPage closeButtonPage;
    private int scrollOffset;
    private Point initialMousePosition;
    /// <exclude />
    private bool toggle;
    private Form dragForm;
    private bool dragging;
    private vTabPage draggingTab;
    private vTabPage lastPage;
    private Point showPoint;
    private bool useTabsAreaBackColor;
    private bool useTabsAreaBorderColor;
    internal bool validationCanceled;
    private bool allowAutoDrawImageListImages;

    /// <summary>Gets or sets the context tabs offset.</summary>
    /// <value>The context tabs offset.</value>
    [DefaultValue(0)]
    [Category("Behavior")]
    [Description("Gets or sets the context tabs offset..")]
    public int ContextTabsOffset
    {
      get
      {
        return this.contextTabsOffset;
      }
      set
      {
        this.contextTabsOffset = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether drop down style is enabled.
    /// </summary>
    [Description("Gets or sets a value indicating whether drop down style is enabled.")]
    [Category("Behavior")]
    [DefaultValue(false)]
    public virtual bool EnableDropDownStyle
    {
      get
      {
        return this.enableDropDownStyle;
      }
      set
      {
        if (value == this.enableDropDownStyle)
          return;
        if (value)
        {
          switch (this.TabAlignment)
          {
            case vTabPageAlignment.Top:
            case vTabPageAlignment.Bottom:
              this.dropDownSize = new Size(this.Width, this.Height - this.TitleHeight);
              this.Height = this.TitleHeight;
              break;
            case vTabPageAlignment.Left:
            case vTabPageAlignment.Right:
              this.dropDownSize = new Size(this.Width - this.TitleHeight, this.Height);
              this.Width = this.TitleHeight;
              break;
          }
        }
        else
        {
          this.Size = this.dropDownSize;
          this.CloseDropDown();
        }
        this.enableDropDownStyle = value;
        this.OnDropDownStyleChanged();
      }
    }

    /// <summary>Gets or sets the tabs shape.</summary>
    /// <value>The tabs shape.</value>
    [Category("Behavior")]
    [Description("Gets or sets the tabs shape.")]
    public TabsShape TabsShape
    {
      get
      {
        return this.tabsShape;
      }
      set
      {
        this.tabsShape = value;
        this.widthPages = (int[]) null;
        this.initializedPagesWidth = false;
        this.CalculatePagesWidth(this.CreateGraphics());
        this.SetLayout();
        this.Invalidate();
        foreach (Control tabPage in this.TabPages)
          tabPage.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display the close button only when the TabPage is selected.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to display the close button only when the TabPage is selected.")]
    [DefaultValue(true)]
    public bool ShowCloseButtonOnSelectedTabOnly
    {
      get
      {
        return this.showCloseButtonOnSelectedTabOnly;
      }
      set
      {
        if (value == this.showCloseButtonOnSelectedTabOnly)
          return;
        this.showCloseButtonOnSelectedTabOnly = value;
        this.widthPages = (int[]) null;
        this.initializedPagesWidth = false;
        this.CalculatePagesWidth(this.CreateGraphics());
        this.SetLayout();
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets whether the close buttons are enabled.</summary>
    [DefaultValue(false)]
    [Description("Gets or sets whether the close buttons are enabled.")]
    [Category("Behavior")]
    public bool EnableCloseButtons
    {
      get
      {
        if (this.TabPages.Count == 1)
          return false;
        return this.enableCloseButtons;
      }
      set
      {
        this.enableCloseButtons = value;
        this.widthPages = (int[]) null;
        this.initializedPagesWidth = false;
        this.CalculatePagesWidth(this.CreateGraphics());
        this.SetLayout();
        this.Invalidate();
      }
    }

    /// <summary>
    /// Determines whether to show the focus rectangle when a tab is focused.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Determines whether to show the focus rectangle when a tab is focused.")]
    public bool ShowFocusRectangle
    {
      get
      {
        return this.showFocusRectangle;
      }
      set
      {
        this.showFocusRectangle = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether tool tips are enabled.
    /// </summary>
    [DefaultValue(false)]
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

    /// <summary>Gets or sets the tool tip show delay.</summary>
    /// <value>The tool tip show delay.</value>
    [Category("Behavior")]
    [DefaultValue(1500)]
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
    [Category("Behavior")]
    [DefaultValue(5000)]
    [Description("Tooltip duration in milliseconds ")]
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

    /// <summary>Gets or sets the theme of the control.</summary>
    [Description("Gets or sets the theme of the control.")]
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null)
          return;
        this.theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        ControlTheme theme = ThemeCache.GetTheme(this.StyleKey + (object) 1, this.VIBlendTheme);
        FillStyle fillStyle = this.theme.QueryFillStyleSetter("TabControlBackGround");
        if (fillStyle != null)
          theme.StyleNormal.FillStyle = fillStyle;
        Color color1 = Color.Empty;
        Color color2 = this.theme.QueryColorSetter("TabControlBorder");
        if (!color2.Equals((object) Color.Empty))
          theme.StyleNormal.BorderColor = color2;
        if (!this.UseTabsAreaBackColor)
          this.tabsAreaBackColor = theme.StyleNormal.FillStyle.Colors[0];
        theme.StyleNormal.FillStyle = (FillStyle) new FillStyleSolid(this.tabsAreaBackColor);
        this.backGround.LoadTheme(theme);
        for (int index = 0; index < this.Controls.Count; ++index)
        {
          vTabPage vTabPage = this.Controls[index] as vTabPage;
          if (vTabPage != null && vTabPage.UseTabControlTheme)
          {
            vTabPage.VIBlendTheme = this.VIBlendTheme;
            vTabPage.tabsFill.IsAnimated = this.AllowAnimations;
            vTabPage.Invalidate();
          }
        }
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Description("Gets or sets the theme of the control using one of the built-in themes.")]
    [Category("Appearance")]
    public virtual VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        this.defaultTheme = value;
        if (this.vScrollBar != null)
          this.vScrollBar.VIBlendTheme = value;
        if (this.hScrollBar != null)
          this.hScrollBar.VIBlendTheme = value;
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
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>
    /// Gets or sets whether the tab is closed on mouse wheel button click
    ///  </summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets whether the tab is closed on mouse wheel button click")]
    public bool AllowDeleteOnMiddleMouseButton
    {
      get
      {
        return this.allowDeleteOnMouseWheelButton;
      }
      set
      {
        this.allowDeleteOnMouseWheelButton = value;
      }
    }

    /// <summary>Gets or sets whether a close button should be enabled</summary>
    [DefaultValue(false)]
    [Description("Gets or sets whether a close button should be enabled")]
    [Category("Behavior")]
    internal bool AllowCloseButton
    {
      get
      {
        return this.allowCloseButton;
      }
      set
      {
        if (this.allowCloseButton == value)
          return;
        this.allowCloseButton = value;
        this.deleteButton.Visible = value;
        this.deleteButton.Size = new Size(16, 16);
        Stream stream = (Stream) null;
        try
        {
          stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VIBlend.WinForms.Controls.TabControl.action_delete.png");
          if (stream == null)
            return;
          Bitmap bitmap = new Bitmap(stream);
          if (bitmap != null)
            this.deleteButton.Image = (Image) bitmap;
          this.SetLayout();
        }
        catch (Exception ex)
        {
        }
        finally
        {
          if (stream != null)
            stream.Close();
        }
      }
    }

    /// <summary>Gets or sets whether drag and drop is enabled.</summary>
    [Description("Gets or sets whether tab pages drag and drop is enabled.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public virtual bool AllowDragDrop
    {
      get
      {
        return this.allowDragAndDrop;
      }
      set
      {
        this.allowDragAndDrop = value;
        if (value)
        {
          vTabControl.DropTabControlTargets.Add(this);
        }
        else
        {
          if (vTabControl.DropTabControlTargets.IndexOf(this) == -1)
            return;
          vTabControl.DropTabControlTargets.Remove(this);
        }
      }
    }

    /// <summary>Gets or sets the tab contexts.</summary>
    /// <value>The tab context.</value>
    [Description("Gets or sets the tab contexts.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public virtual List<TabContext> TabContexts
    {
      get
      {
        return this.tabContexts;
      }
      set
      {
        this.tabContexts = value;
      }
    }

    /// <summary>
    /// Gets or sets whether tab pages should fit to control's bounds
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether tab pages should fit to control's bounds")]
    public virtual bool FitTabsToBounds
    {
      get
      {
        return this.fitsTabsToBounds;
      }
      set
      {
        if (this.fitsTabsToBounds == value)
          return;
        this.fitsTabsToBounds = value;
        this.scrollOffset = 0;
        this.SetLayout();
        this.InitializeScrollBar();
        this.Invalidate();
        this.hScrollBar.Value = 0;
        this.vScrollBar.Value = 0;
        this.hScrollBar.Visible = false;
        this.vScrollBar.Visible = false;
      }
    }

    /// <summary>Gets or sets text orientation of the tab pages.</summary>
    [Category("Behavior")]
    [Description("Gets or sets text orientation of the tab pages.")]
    public virtual vTabPageTextOrientation TextOrientation
    {
      get
      {
        return this.textOrientation;
      }
      set
      {
        if (this.textOrientation == value)
          return;
        this.textOrientation = value;
        this.EnsureTabVisible(this.SelectedTab);
        this.SetLayout();
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the height of all pages.</summary>
    /// <value>The height of all pages.</value>
    [DefaultValue(-1)]
    [Description("Gets or sets the height of all pages.")]
    [Category("Behavior")]
    public virtual int AllPagesHeight
    {
      get
      {
        return this.allPagesHeight;
      }
      set
      {
        if (this.allPagesHeight == value)
          return;
        this.allPagesHeight = value;
        this.SetLayout();
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the initial offset for the tab pages.</summary>
    [Description("Gets or sets the initial offset for the tab pages.")]
    [DefaultValue(30)]
    [Category("Behavior")]
    public virtual int TabsInitialOffset
    {
      get
      {
        return this.tabsInitialOffset;
      }
      set
      {
        if (this.tabsInitialOffset == value)
          return;
        this.tabsInitialOffset = value;
        this.SetLayout();
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the spacing between the tab pages.</summary>
    [DefaultValue(5)]
    [Description("Gets or sets the spacing between the tab pages.")]
    [Category("Behavior")]
    public int TabsSpacing
    {
      get
      {
        return this.tabsSpacing;
      }
      set
      {
        if (this.tabsSpacing == value)
          return;
        this.tabsSpacing = value;
        this.SetLayout();
        this.Invalidate();
        if (this.SelectedTab == null)
          return;
        this.SelectedTab.Invalidate();
      }
    }

    /// <summary>Gets the tab pages collection of the vTabControl.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [Description("Gets the tab pages collection of the vTabControl.")]
    public vTabCollection TabPages
    {
      get
      {
        if (this.tabCollection == null)
          this.tabCollection = new vTabCollection(this);
        return this.tabCollection;
      }
    }

    /// <summary>Gets or sets the tab pages alignment.</summary>
    [Description("Gets or sets the tab pages alignment.")]
    [Category("Behavior")]
    [Browsable(true)]
    public virtual vTabPageAlignment TabAlignment
    {
      get
      {
        return this.tabAlignment;
      }
      set
      {
        if (this.tabAlignment == value)
          return;
        for (int index = this.Controls.Count - 1; index >= 0; --index)
        {
          vTabPage vTabPage = this.Controls[index] as vTabPage;
          if (vTabPage != null && (!vTabPage.Invisible || this.DesignMode))
          {
            vTabPage.tabsFill.Animating = false;
            vTabPage.tabsFill.BackAnimating = false;
          }
        }
        this.tabAlignment = value;
        this.scrollOffset = 0;
        this.widthPages = (int[]) null;
        this.initializedPagesWidth = false;
        using (Graphics graphics = this.CreateGraphics())
          this.CalculatePagesWidth(graphics);
        this.SetLayout();
        this.Invalidate();
        this.Theme = this.Theme;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether scrolling animation is enabled
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool AllowScrollingAnimation
    {
      get
      {
        return this.allowScrollingAnimation;
      }
      set
      {
        this.allowScrollingAnimation = value;
        if (!value)
          this.StopScrollingAnimation();
        this.Invalidate();
      }
    }

    /// <summary>
    /// Determines the height of the title area which holds the tab pages
    /// </summary>
    [DefaultValue(45)]
    [Category("Behavior")]
    [Description("Determines the height of the title area which holds the tabs")]
    public virtual int TitleHeight
    {
      get
      {
        return this.titleHeight;
      }
      set
      {
        if (this.titleHeight == value)
          return;
        this.titleHeight = value;
        this.SetLayout();
        this.Invalidate();
      }
    }

    /// <summary>
    /// Get or sets the rounding radius when the TabsShape property is set to RoundedCorners.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(10)]
    [Description("Get or sets the rounding radius when the TabsShape property is set to RoundedCorners.")]
    public int CornerRadius
    {
      get
      {
        return this.cornerRadius;
      }
      set
      {
        this.cornerRadius = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the BackColor of vTabControl Tabs area.</summary>
    [DefaultValue(typeof (Color), "Transparent")]
    [Description("Gets or sets the BackColor of vTabControl Tabs area.")]
    [Category("Appearance")]
    public virtual Color TabsAreaBackColor
    {
      get
      {
        return this.tabsAreaBackColor;
      }
      set
      {
        this.tabsAreaBackColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the TabsAreaBackColor
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to use the TabsAreaBackColor")]
    public virtual bool UseTabsAreaBackColor
    {
      get
      {
        return this.useTabsAreaBackColor;
      }
      set
      {
        this.useTabsAreaBackColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the TabsAreaBorderColor
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the TabsAreaBorderColor")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public virtual bool UseTabsAreaBorderColor
    {
      get
      {
        return this.useTabsAreaBorderColor;
      }
      set
      {
        this.useTabsAreaBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the BackColor of vTabControl Tabs area.</summary>
    [DefaultValue(typeof (Color), "Transparent")]
    [Description("Gets or sets the BorderColor of vTabControl Tabs area.")]
    [Category("Appearance")]
    public virtual Color TabsAreaBorderColor
    {
      get
      {
        return this.tabsAreaBorderColor;
      }
      set
      {
        this.tabsAreaBorderColor = value;
        this.Refresh();
      }
    }

    [Browsable(false)]
    public new Color BackColor
    {
      get
      {
        return base.BackColor;
      }
      set
      {
        base.BackColor = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public new Image BackgroundImage
    {
      get
      {
        return base.BackgroundImage;
      }
      set
      {
        base.BackgroundImage = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public new BorderStyle BorderStyle
    {
      get
      {
        return base.BorderStyle;
      }
      set
      {
        base.BorderStyle = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new ScrollableControl.DockPaddingEdges DockPadding
    {
      get
      {
        return base.DockPadding;
      }
    }

    /// <summary>
    /// Gets or sets the image list associated to the tab control.
    /// </summary>
    [DefaultValue(null)]
    [Description("Gets or sets the image list associated to the tab control.")]
    [Category("Behavior")]
    public virtual ImageList ImageList
    {
      get
      {
        return this.imageList;
      }
      set
      {
        this.imageList = value;
      }
    }

    /// <summary>
    /// Gets or sets the index of the selected tab in the tab control.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public int SelectedIndex
    {
      get
      {
        return this.TabPages.IndexOf(this.SelectedTab);
      }
      set
      {
        if (value < 0 || value >= this.TabPages.Count)
          return;
        this.SelectedTab = this.TabPages[value];
      }
    }

    /// <summary>Gets or sets the selected tab in the tab control.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public vTabPage SelectedTab
    {
      get
      {
        return this.selectedTab;
      }
      set
      {
        if (value != null && !this.DesignMode && (!value.Enabled || value.Invisible))
          return;
        if (this.validationCanceled)
        {
          this.validationCanceled = false;
        }
        else
        {
          for (int index = 0; index < this.Controls.Count; ++index)
          {
            vTabPage vTabPage = this.Controls[index] as vTabPage;
            if (vTabPage != null && vTabPage == value)
            {
              vTabPage.Dock = this is vRibbonBar ? DockStyle.None : DockStyle.Fill;
              vTabPage.Show();
            }
          }
          for (int index = 0; index < this.Controls.Count; ++index)
          {
            vTabPage vTabPage = this.Controls[index] as vTabPage;
            if (vTabPage != null && vTabPage != value)
              vTabPage.Hide();
          }
          vTabCancelEventArgs e = new vTabCancelEventArgs(value);
          this.OnSelectedIndexChanging(e);
          if (e.Cancel)
            return;
          this.selectedTab = value;
          this.Invalidate();
          this.InvokeSelectedIndexChanged((object) this, EventArgs.Empty);
        }
      }
    }

    /// <exclude />
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        return this.animationManager;
      }
    }

    /// <summary>Gets or sets whether the control is animated</summary>
    [Browsable(false)]
    [DefaultValue(false)]
    public virtual bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        this.allowAnimations = value;
        foreach (vTabPage tabPage in this.TabPages)
          tabPage.tabsFill.IsAnimated = value;
      }
    }

    /// <summary>Occurs when tooltip shows.</summary>
    [Category("Action")]
    public event ToolTipShownHandler TooltipShow;

    /// <summary>Occurs when a TabPage is rendered.</summary>
    [Description("Occurs when a TabPage is rendered.")]
    [Category("Action")]
    public event EventHandler<DrawTabPageEventArgs> DrawTabPage;

    /// <summary>Occurs when the mouse button is clicked over a tab.</summary>
    [Description("Occurs when the mouse button is clicked over a tab.")]
    [Category("Action")]
    public event EventHandler<vTabMouseEventArgs> TabMouseDown;

    /// <summary>Occurs when the mouse button is clicked over a tab.</summary>
    [Category("Action")]
    [Description("Occurs when the mouse button is clicked over a tab.")]
    public event EventHandler<vTabMouseEventArgs> TabMouseUp;

    /// <summary>Occurs when a TabPage is rendered.</summary>
    [Category("Action")]
    [Description("Occurs when a TabPage is rendered.")]
    public event EventHandler<DrawTabPageEventArgs> DrawTabPageBackground;

    /// <summary>Occurs when the Title is rendered.</summary>
    [Description("Occurs when the Title is rendered.")]
    [Category("Action")]
    public event EventHandler<DrawTabControlTitleEventArgs> DrawTitleBackground;

    /// <summary>Occurs when popup panel is painted.</summary>
    [Category("Action")]
    public event PaintEventHandler PopupPaint;

    /// <summary>
    /// Occurs when the EnableDropDownStyle property is changed.
    /// </summary>
    [Category("Action")]
    public event EventHandler DropDownStyleChanged;

    /// <summary>Occurs when the HoveredTab has changed.</summary>
    [Category("Behavior")]
    public event vTabControl.DragEventHandler HoveredTabChanged;

    /// <summary>Occurs when the SelectedIndex property has changed.</summary>
    [Category("Behavior")]
    public event EventHandler SelectedIndexChanged;

    /// <summary>Occurs when the users clicks in the Title area</summary>
    [Category("Behavior")]
    public event EventHandler TitleClick;

    /// <summary>Occurs when the selected index is about to change</summary>
    [Category("Behavior")]
    public event vTabControl.vTabCancelEventHandler SelectedIndexChanging;

    /// <summary>Occurs when a tab page drag has started</summary>
    [Category("Behavior")]
    public event vTabControl.DragEventHandler TabPageDragStarted;

    /// <summary>Occurs when a tab page drag is about to start</summary>
    [Category("Behavior")]
    public event vTabControl.DragCancelEventHandler TabPageDragStarting;

    /// <summary>Occurs when a tab page drag has ended</summary>
    [Category("Behavior")]
    public event vTabControl.DragEventHandler TabPageDragEnded;

    /// <summary>Occurs when a tab page drag is about to end</summary>
    [Category("Behavior")]
    public event vTabControl.DragCancelEventHandler TabPageDragEnding;

    static vTabControl()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vTabControl()
    {
      this.sfTabs.HotkeyPrefix = HotkeyPrefix.Show;
      this.sfTabs.LineAlignment = StringAlignment.Center;
      this.sfTabs.Alignment = StringAlignment.Center;
      this.sfTabs.FormatFlags = StringFormatFlags.NoWrap;
      this.sfTabs.Trimming = StringTrimming.EllipsisCharacter;
      if (this.RightToLeft == RightToLeft.Yes)
        this.sfTabs.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.ControlAdded += new ControlEventHandler(this.PageAdded);
      this.ControlRemoved += new ControlEventHandler(this.PageRemoved);
      this.SetLayout();
      this.hScrollBar = new vHScrollBar();
      this.vScrollBar = new vVScrollBar();
      this.Controls.Add((Control) this.hScrollBar);
      this.Controls.Add((Control) this.vScrollBar);
      this.Controls.Add((Control) this.deleteButton);
      this.hScrollBar.Visible = false;
      this.vScrollBar.Visible = false;
      this.hScrollBar.ValueChanged += new EventHandler(this.hScrollBar_ValueChanged);
      this.vScrollBar.ValueChanged += new EventHandler(this.vScrollBar_ValueChanged);
      this.hScrollBar.Size = new Size(37, 17);
      this.vScrollBar.Size = new Size(17, 37);
      this.backGround = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.animationManager = new AnimationManager((Control) this);
      this.scrollingTimer = new Timer();
      this.scrollingTimer.Interval = 300;
      this.scrollingTimer.Tick += new EventHandler(this.scrollingTimer_Tick);
      this.deleteButton.Size = new Size(16, 16);
      this.timerToolTip.Tick += new EventHandler(this.timerToolTip_Tick);
      this.timerToolTip.Interval = 1500;
      this.deleteButton.PaintBorder = false;
      this.deleteButton.PaintFill = false;
      this.deleteButton.ShowFocusRectangle = false;
      this.deleteButton.Click += new EventHandler(this.deleteButton_Click);
      this.dropDown.Closed += new ToolStripDropDownClosedEventHandler(this.dropDown_Closed);
      this.dropDown.AutoClose = false;
    }

    /// <summary>
    /// Raises the <see cref="E:TabMouseDown" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.vTabMouseEventArgs" /> instance containing the event data.</param>
    protected virtual void OnTabMouseDown(vTabMouseEventArgs args)
    {
      if (this.TabMouseDown == null)
        return;
      this.TabMouseDown((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:TabMouseUp" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.vTabMouseEventArgs" /> instance containing the event data.</param>
    protected virtual void OnTabMouseUp(vTabMouseEventArgs args)
    {
      if (this.TabMouseUp == null)
        return;
      this.TabMouseUp((object) this, args);
    }

    protected virtual void OnPopupPaint(PaintEventArgs e)
    {
      if (this.PopupPaint == null)
        return;
      this.PopupPaint((object) this, e);
    }

    /// <summary>Raises the DropDownStyleChanged event.</summary>
    protected virtual void OnDropDownStyleChanged()
    {
      if (this.DropDownStyleChanged == null)
        return;
      this.DropDownStyleChanged((object) this, EventArgs.Empty);
    }

    private void ResetTabsShape()
    {
      this.TabsShape = TabsShape.Office2007;
    }

    private bool ShouldSerializeTabsShape()
    {
      return this.TabsShape != TabsShape.Office2007;
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      this.DeleteSelectedItem();
    }

    private void DeleteSelectedItem()
    {
      if (this.SelectedTab == null || this.TabPages.Count <= 1)
        return;
      this.deleteButton.Visible = false;
      this.Controls.IndexOf((Control) this.SelectedTab);
      this.Controls.Remove((Control) this.SelectedTab);
      this.TabPages.Remove(this.SelectedTab);
      this.SelectedTab = this.TabPages[0];
      this.SetLayout();
      this.Invalidate();
    }

    /// <summary>Deletes the tab page.</summary>
    /// <param name="page">The page.</param>
    public void DeleteTabPage(vTabPage page)
    {
      if (page == null || this.TabPages.Count <= 1)
        return;
      bool flag = page == this.SelectedTab;
      this.Controls.IndexOf((Control) page);
      int index = this.TabPages.IndexOf(page);
      this.Controls.Remove((Control) page);
      this.TabPages.Remove(page);
      this.SelectedTab = this.TabPages[0];
      this.widthPages = (int[]) null;
      this.CalculatePagesWidth(this.CreateGraphics());
      this.SetLayout();
      if (this.TabPages.Count > 0 && flag && index < this.TabPages.Count)
        this.SelectedTab = this.TabPages[index];
      this.Invalidate();
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      int clicks = e.Clicks;
    }

    private void timerToolTip_Tick(object sender, EventArgs e)
    {
      this.timerToolTip.Stop();
      if (this.toolTipEventArgs == null)
        return;
      this.OnToolTipShown(this.toolTipEventArgs);
    }

    protected internal virtual void OnToolTipShown(ToolTipEventArgs args)
    {
      if (this.TooltipShow != null)
        this.TooltipShow((object) this, args);
      vTabPage vTabPage = this.HitTest(this.showPoint);
      string text = "";
      if (!string.IsNullOrEmpty(text))
        text = args.ToolTipText;
      if (vTabPage != null)
        text = vTabPage.TooltipText;
      try
      {
        if (this.HitTest(this.PointToClient(Cursor.Position)) != vTabPage || vTabPage == null || string.IsNullOrEmpty(text))
          return;
        Rectangle rectangle = vTabPage.pageRectangle;
        this.showPoint = this.PointToClient(Cursor.Position);
        this.tabStripToolTip.Show(text, (IWin32Window) this, new Point(this.showPoint.X, rectangle.Bottom), this.toolTipHideDelay);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
    }

    /// <exclude />
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Invalidate();
    }

    /// <exclude />
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (this.EnableDropDownStyle && this.dropDown.Visible && keyData == Keys.Escape)
      {
        this.CloseDropDown();
        this.toggle = !this.toggle;
      }
      int num = this.TabPages.IndexOf(this.SelectedTab);
      if (this.Focused)
      {
        switch (keyData)
        {
          case Keys.Left:
          case Keys.Up:
            if (num > 0)
            {
              bool scrollingAnimation = this.AllowScrollingAnimation;
              this.AllowScrollingAnimation = false;
              for (int index = num - 1; index >= 0; --index)
              {
                vTabPage vTabPage = this.TabPages[index];
                if (!vTabPage.Invisible && vTabPage.Enabled)
                {
                  this.SelectedTab = this.TabPages[index];
                  this.EnsureTabVisible(this.SelectedTab);
                  this.Invalidate();
                  break;
                }
              }
              this.AllowScrollingAnimation = scrollingAnimation;
            }
            return true;
          case Keys.Right:
          case Keys.Down:
            if (num < this.TabPages.Count - 1)
            {
              bool scrollingAnimation = this.AllowScrollingAnimation;
              this.AllowScrollingAnimation = false;
              for (int index = num + 1; index < this.TabPages.Count; ++index)
              {
                vTabPage vTabPage = this.TabPages[index];
                if (!vTabPage.Invisible && vTabPage.Enabled)
                {
                  this.SelectedTab = this.TabPages[index];
                  this.EnsureTabVisible(this.SelectedTab);
                  this.Invalidate();
                  break;
                }
              }
              this.AllowScrollingAnimation = scrollingAnimation;
            }
            return true;
        }
      }
      return base.ProcessCmdKey(ref msg, keyData);
    }

    /// <summary>
    /// Call this function to ensure that a specific tab page is visible.
    /// </summary>
    /// <param name="tabPage">vTabPage to make visible.</param>
    public void EnsureTabVisible(vTabPage tabPage)
    {
      if (tabPage == null)
        return;
      this.vScrollStep.Clear();
      this.scrollStep.Clear();
      this.vScrollBar.Value = 0;
      this.hScrollBar.Value = 0;
      this.scrollOffset = 0;
      this.Invalidate();
      this.widthPages = (int[]) null;
      this.initializedPagesWidth = false;
      using (Graphics graphics = this.CreateGraphics())
        this.CalculatePagesWidth(graphics);
      this.InitializeScrollBar();
      using (Graphics graphics = this.CreateGraphics())
      {
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
        {
          while (this.GetPageRectangle(graphics, tabPage).Right > this.ClientRectangle.Width)
            ++this.hScrollBar.Value;
          while (this.GetPageRectangle(graphics, tabPage).Left < 0)
            --this.hScrollBar.Value;
        }
        else
        {
          while (this.GetPageRectangle(graphics, tabPage).Top > this.ClientRectangle.Height)
            ++this.vScrollBar.Value;
          while (this.GetPageRectangle(graphics, tabPage).Top < 0)
            --this.vScrollBar.Value;
        }
      }
    }

    private void vScrollBar_ValueChanged(object sender, EventArgs e)
    {
      using (Graphics graphics = this.CreateGraphics())
      {
        if (this.vScrollBar.Value > this.oldScrollValue)
        {
          int index1 = 0;
          for (int index2 = 0; index2 < this.Controls.Count; ++index2)
          {
            vTabPage page = this.Controls[index2] as vTabPage;
            if (page != null && !page.Invisible)
            {
              page.pageRectangle = Rectangle.Empty;
              if (this.GetPageRectangle(graphics, page).Top < 0)
                ++index1;
            }
          }
          if (index1 == 0)
          {
            if (this.AllowScrollingAnimation)
            {
              this.StopScrollingAnimation();
              this.Animate(this.scrollOffset, this.scrollOffset - this.widthPages[index1] - this.tabsSpacing - this.TabsInitialOffset, true);
            }
            else
              this.scrollOffset -= this.widthPages[index1] + this.tabsSpacing + this.TabsInitialOffset;
            this.vScrollStep.Push(this.widthPages[index1] + this.tabsSpacing + this.TabsInitialOffset);
          }
          else
          {
            Rectangle rectangle1 = Rectangle.Empty;
            Rectangle rectangle2 = Rectangle.Empty;
            int num = 0;
            for (int index2 = 0; index2 < this.TabPages.Count; ++index2)
            {
              vTabPage page = this.TabPages[index2];
              if (!page.Invisible || this.DesignMode)
              {
                Rectangle pageRectangle = this.GetPageRectangle(graphics, page);
                num += pageRectangle.Height;
              }
            }
            int vscrollPageHeight = this.GetVScrollPageHeight(graphics);
            if (num + this.TabPages.Count * this.TabsSpacing + this.TabsInitialOffset + this.scrollOffset + 40 >= this.Height)
            {
              if (this.AllowScrollingAnimation)
              {
                this.StopScrollingAnimation();
                this.Animate(this.scrollOffset, this.scrollOffset - vscrollPageHeight - this.tabsSpacing, true);
              }
              else
                this.scrollOffset -= vscrollPageHeight + this.tabsSpacing;
              this.vScrollStep.Push(vscrollPageHeight + this.tabsSpacing);
            }
          }
        }
        else if (this.vScrollStep.Count > 0)
        {
          if (this.AllowScrollingAnimation)
          {
            this.StopScrollingAnimation();
            this.Animate(this.scrollOffset, this.scrollOffset + this.vScrollStep.Pop(), false);
          }
          else
            this.scrollOffset += this.vScrollStep.Pop();
        }
        this.oldScrollValue = this.vScrollBar.Value;
      }
      this.Refresh();
    }

    private void StopScrollingAnimation()
    {
      if (!this.timer.Enabled)
        return;
      this.timer.Stop();
      this.animationCurrentValue = (float) this.animationEndValue;
      this.animationValue = 0.5f;
      this.scrollOffset = this.animationEndValue;
      this.animationStartValue = this.animationEndValue;
      this.Invalidate();
    }

    private void Animate(int startValue, int endValue, bool animateForward)
    {
      this.timer.Tick -= new EventHandler(this.timer_Tick);
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.timer.Interval = 10;
      this.animationStartValue = startValue;
      this.animationEndValue = endValue;
      this.animationCurrentValue = (float) this.animationStartValue;
      this.timer.Start();
      this.animateForward = animateForward;
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      this.animationValue *= 1.225f;
      if (this.animateForward)
      {
        this.animationCurrentValue -= this.animationValue;
        if ((double) this.animationCurrentValue > (double) this.animationEndValue)
        {
          this.scrollOffset -= (int) this.animationValue;
        }
        else
        {
          this.animationCurrentValue = (float) this.animationStartValue;
          this.timer.Stop();
          this.animationValue = 1f;
          this.scrollOffset = this.animationEndValue;
        }
      }
      else
      {
        this.animationCurrentValue += this.animationValue;
        if ((double) this.animationCurrentValue < (double) this.animationEndValue)
        {
          this.scrollOffset += (int) this.animationValue;
        }
        else
        {
          this.animationCurrentValue = (float) this.animationStartValue;
          this.timer.Stop();
          this.animationValue = 1f;
          this.scrollOffset = this.animationEndValue;
        }
      }
      this.Refresh();
    }

    private Point[] GetTabbedPath(Rectangle rc, vTabPageAlignment direction)
    {
      Point[] pointArray;
      switch (direction)
      {
        case vTabPageAlignment.Bottom:
          pointArray = new Point[8]
          {
            new Point(rc.X, rc.Top),
            new Point(rc.X + 2, rc.Top + 2),
            new Point(rc.X + 2, rc.Bottom - 2),
            new Point(rc.X + 4, rc.Bottom),
            new Point(rc.Right - 4, rc.Bottom),
            new Point(rc.Right - 2, rc.Bottom - 2),
            new Point(rc.Right - 2, rc.Top + 2),
            new Point(rc.Right, rc.Top)
          };
          break;
        case vTabPageAlignment.Left:
          pointArray = new Point[8]
          {
            new Point(rc.Right, rc.Top),
            new Point(rc.Right - 2, rc.Top + 2),
            new Point(rc.Left + 2, rc.Top + 2),
            new Point(rc.Left, rc.Top + 4),
            new Point(rc.Left, rc.Bottom - 4),
            new Point(rc.Left + 2, rc.Bottom - 2),
            new Point(rc.Right - 2, rc.Bottom - 2),
            new Point(rc.Right, rc.Bottom)
          };
          break;
        case vTabPageAlignment.Right:
          pointArray = new Point[8]
          {
            new Point(rc.Left, rc.Top),
            new Point(rc.Left + 2, rc.Top + 2),
            new Point(rc.Right - 2, rc.Top + 2),
            new Point(rc.Right, rc.Top + 4),
            new Point(rc.Right, rc.Bottom - 4),
            new Point(rc.Right - 2, rc.Bottom - 2),
            new Point(rc.Left + 2, rc.Bottom - 2),
            new Point(rc.Left, rc.Bottom)
          };
          break;
        default:
          pointArray = new Point[8]
          {
            new Point(rc.X, rc.Bottom),
            new Point(rc.X + 2, rc.Bottom - 2),
            new Point(rc.X + 2, rc.Y + 2),
            new Point(rc.X + 4, rc.Y),
            new Point(rc.Right - 4, rc.Y),
            new Point(rc.Right - 2, rc.Y + 2),
            new Point(rc.Right - 2, rc.Bottom - 2),
            new Point(rc.Right, rc.Bottom)
          };
          break;
      }
      return pointArray;
    }

    private int GetHScrollPageWidth(Graphics g)
    {
      int num = 0;
      foreach (vTabPage tabPage in this.TabPages)
      {
        if (tabPage != null && (!tabPage.Invisible || this.DesignMode))
        {
          tabPage.pageRectangle = Rectangle.Empty;
          Rectangle pageRectangle = this.GetPageRectangle(g, tabPage);
          if (pageRectangle.Left >= 0)
          {
            num = pageRectangle.Width;
            break;
          }
        }
      }
      return num;
    }

    private int GetVScrollPageHeight(Graphics g)
    {
      int num = 0;
      foreach (vTabPage tabPage in this.TabPages)
      {
        if (tabPage != null && (!tabPage.Invisible || this.DesignMode))
        {
          tabPage.pageRectangle = Rectangle.Empty;
          Rectangle pageRectangle = this.GetPageRectangle(g, tabPage);
          if (pageRectangle.Top >= 0)
          {
            num = pageRectangle.Height;
            break;
          }
        }
      }
      return num;
    }

    private void hScrollBar_ValueChanged(object sender, EventArgs e)
    {
      using (Graphics graphics = this.CreateGraphics())
      {
        if (this.hScrollBar.Value > this.oldScrollValue)
        {
          int index1 = 0;
          foreach (vTabPage tabPage in this.TabPages)
          {
            if (tabPage != null && !tabPage.Invisible)
            {
              tabPage.pageRectangle = Rectangle.Empty;
              if (this.GetPageRectangle(graphics, tabPage).Left < 0)
                ++index1;
            }
          }
          Rectangle rectangle1 = Rectangle.Empty;
          Rectangle rectangle2 = Rectangle.Empty;
          if (index1 == 0)
          {
            if (!this.AllowScrollingAnimation)
            {
              this.scrollOffset -= this.TabsInitialOffset;
              this.scrollOffset -= this.widthPages[index1] + this.tabsSpacing;
            }
            if (this.AllowScrollingAnimation)
            {
              this.StopScrollingAnimation();
              this.Animate(this.scrollOffset, this.scrollOffset - this.widthPages[index1] - this.tabsSpacing - this.TabsInitialOffset, true);
            }
            this.scrollStep.Push(this.TabsInitialOffset + this.widthPages[index1] + this.tabsSpacing);
          }
          else
          {
            int num = 0;
            for (int index2 = 0; index2 < this.TabPages.Count; ++index2)
            {
              vTabPage page = this.TabPages[index2];
              if (!page.Invisible || this.DesignMode)
              {
                Rectangle pageRectangle = this.GetPageRectangle(graphics, page);
                num += pageRectangle.Width;
              }
            }
            int hscrollPageWidth = this.GetHScrollPageWidth(graphics);
            if (num + this.TabPages.Count * this.TabsSpacing + this.TabsInitialOffset + this.scrollOffset + 40 >= this.Width)
            {
              if (this.AllowScrollingAnimation)
              {
                this.StopScrollingAnimation();
                this.Animate(this.scrollOffset, this.scrollOffset - hscrollPageWidth - this.tabsSpacing, true);
              }
              else
                this.scrollOffset -= hscrollPageWidth + this.tabsSpacing;
              this.scrollStep.Push(hscrollPageWidth + this.tabsSpacing);
            }
          }
        }
        else if (this.scrollStep.Count > 0)
        {
          if (this.AllowScrollingAnimation)
          {
            this.StopScrollingAnimation();
            this.Animate(this.scrollOffset, this.scrollOffset + this.scrollStep.Pop(), false);
          }
          else
            this.scrollOffset += this.scrollStep.Pop();
        }
        this.oldScrollValue = this.hScrollBar.Value;
      }
      this.Refresh();
    }

    /// <summary>Updates the scrollbars.</summary>
    public void UpdateScrollbars()
    {
      this.InitializeScrollBar();
    }

    internal void InitializeScrollBar()
    {
      if (this.vScrollBar == null || this.hScrollBar == null)
        return;
      this.deleteButton.Visible = false;
      if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
      {
        this.vScrollBar.Visible = false;
        this.hScrollBar.Visible = false;
        int num1 = 0;
        int y = this.titleHeight - this.hScrollBar.Height;
        if (this.TabAlignment == vTabPageAlignment.Bottom)
          y = this.Height - this.titleHeight;
        if (this.AllowCloseButton)
        {
          Point point = new Point(this.Width - 16, y);
          Size size = new Size(16, 16);
          this.deleteButton.SetBounds(point.X, point.Y, size.Width, size.Height);
          this.deleteButton.Visible = true;
        }
        if (this.FitTabsToBounds)
          return;
        using (Graphics graphics = this.CreateGraphics())
        {
          int num2 = this.TabsInitialOffset + this.TabPages.Count * this.TabsSpacing;
          for (int index = 0; index < this.Controls.Count; ++index)
          {
            vTabPage page = this.Controls[index] as vTabPage;
            if (page != null)
            {
              num2 += this.GetPageWidth(graphics, page);
              if (num2 > this.Width)
              {
                this.hScrollBar.Visible = true;
                this.hScrollBar.Location = new Point(this.Width - this.hScrollBar.Width - num1, y);
                this.deleteButton.Location = new Point(this.Width - this.deleteButton.Width - this.hScrollBar.Width, y);
              }
            }
          }
        }
        if (this.hScrollBar.Visible)
          return;
        this.scrollOffset = 0;
        this.vScrollStep.Clear();
        this.scrollStep.Clear();
        this.Invalidate();
      }
      else
      {
        int num1 = 0;
        int x = this.titleHeight - this.vScrollBar.Width;
        if (this.TabAlignment == vTabPageAlignment.Right)
          x = this.Width - this.titleHeight;
        if (this.AllowCloseButton)
        {
          Point point = new Point(x, this.Height - 16);
          Size size = new Size(16, 16);
          this.deleteButton.SetBounds(point.X, point.Y, size.Width, size.Height);
          this.deleteButton.Visible = true;
        }
        if (this.FitTabsToBounds)
          return;
        this.vScrollBar.Visible = false;
        this.hScrollBar.Visible = false;
        using (Graphics graphics = this.CreateGraphics())
        {
          int num2 = this.TabsInitialOffset + this.TabPages.Count * this.TabsSpacing;
          for (int index = 0; index < this.Controls.Count; ++index)
          {
            vTabPage page = this.Controls[index] as vTabPage;
            if (page != null)
            {
              num2 += this.GetPageRectangle(graphics, page).Height;
              if (num2 > this.Height)
              {
                this.vScrollBar.Visible = true;
                this.vScrollBar.Location = new Point(x, this.Height - this.vScrollBar.Height - num1);
                this.deleteButton.Location = new Point(x, this.Height - this.deleteButton.Height - this.vScrollBar.Height);
              }
            }
          }
        }
        if (this.vScrollBar.Visible)
          return;
        this.scrollOffset = 0;
        this.vScrollStep.Clear();
        this.scrollStep.Clear();
        this.Invalidate();
      }
    }

    private void ResetTextOrientation()
    {
      this.TextOrientation = vTabPageTextOrientation.Horizontal;
    }

    private bool ShouldSerializeTextOrientation()
    {
      return this.TextOrientation != vTabPageTextOrientation.Horizontal;
    }

    /// <exclude />
    protected virtual void SetLayout()
    {
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          this.DockPadding.Left = this.DockPadding.Right = 0;
          this.DockPadding.Top = this.titleHeight;
          this.DockPadding.Bottom = 0;
          break;
        case vTabPageAlignment.Bottom:
          this.DockPadding.Left = this.DockPadding.Right = 0;
          base.DockPadding.Top = 0;
          base.DockPadding.Bottom = this.titleHeight;
          break;
        case vTabPageAlignment.Left:
          this.DockPadding.Right = 0;
          this.DockPadding.Left = this.titleHeight;
          this.DockPadding.Top = 0;
          this.DockPadding.Bottom = 0;
          break;
        case vTabPageAlignment.Right:
          this.DockPadding.Right = this.titleHeight;
          this.DockPadding.Left = 0;
          this.DockPadding.Top = 0;
          this.DockPadding.Bottom = 0;
          break;
      }
      this.InitializeScrollBar();
      this.initializedPagesWidth = false;
    }

    /// <exclude />
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.InitializeScrollBar();
      this.initializedPagesWidth = false;
      if (!this.EnableDropDownStyle || this.DesignMode)
        return;
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
        case vTabPageAlignment.Bottom:
          if (this.Height <= this.TitleHeight)
            break;
          this.dropDownSize = new Size(this.Size.Width, this.Size.Height - this.TitleHeight);
          this.Height = this.TitleHeight;
          break;
        case vTabPageAlignment.Left:
        case vTabPageAlignment.Right:
          if (this.Width <= this.TitleHeight)
            break;
          this.dropDownSize = new Size(this.Size.Width - this.TitleHeight, this.Size.Height);
          this.Width = this.TitleHeight;
          break;
      }
    }

    /// <exclude />
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
    }

    /// <summary>Draws the text left rotated.</summary>
    /// <param name="g">The g.</param>
    /// <param name="f">The f.</param>
    /// <param name="r">The r.</param>
    /// <param name="text">The text.</param>
    /// <param name="brush">The brush.</param>
    /// <param name="format">The format.</param>
    protected virtual void DrawTextLeftRotated(Graphics g, Font f, Rectangle r, string text, SolidBrush brush, StringFormat format)
    {
      RectangleF layoutRectangle = new RectangleF((float) r.Left, (float) r.Bottom, (float) r.Height, (float) r.Width);
      Matrix transform = g.Transform;
      PointF point = new PointF((float) r.Left, (float) r.Bottom);
      Matrix matrix = new Matrix();
      matrix.RotateAt(-90f, point, MatrixOrder.Append);
      g.Transform = matrix;
      g.DrawString(text, f, (Brush) brush, layoutRectangle, format);
      g.Transform = transform;
    }

    internal void CalculatePagesWidth(Graphics g)
    {
      if (this.widthPages != null && this.widthPages.Length < this.TabPages.Count)
        this.initializedPagesWidth = false;
      if (this.initializedPagesWidth)
        return;
      this.initializedPagesWidth = true;
      int count = this.TabPages.Count;
      this.widthPages = new int[this.TabPages.Count];
      int num = 0;
      foreach (vTabPage tabPage in this.TabPages)
      {
        if (tabPage != null)
        {
          this.widthPages[num++] = this.GetPageWidth(g, tabPage);
          tabPage.pageRectangle = Rectangle.Empty;
        }
      }
    }

    /// <exclude />
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.timer.Dispose();
        this.timerToolTip.Dispose();
        this.scrollingTimer.Dispose();
        this.sfTabs.Dispose();
      }
      base.Dispose(disposing);
    }

    private StringFormat CreateFormat(vTabPage page)
    {
      StringFormat stringFormat = new StringFormat();
      stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
      stringFormat.Trimming = StringTrimming.EllipsisCharacter;
      if (this.RightToLeft == RightToLeft.Yes)
        stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
      stringFormat.LineAlignment = this.helper.GetVerticalAlignment(page.TextAlignment);
      stringFormat.Alignment = this.helper.GetHorizontalAlignment(page.TextAlignment);
      if (!page.MultilineText)
        stringFormat.FormatFlags = StringFormatFlags.NoWrap;
      stringFormat.Trimming = StringTrimming.EllipsisCharacter;
      return stringFormat;
    }

    private GraphicsPath GetCloseButtonPath(Rectangle closeRect)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddLine(closeRect.X, closeRect.Y, closeRect.Right, closeRect.Bottom);
      graphicsPath.CloseFigure();
      graphicsPath.AddLine(closeRect.Right, closeRect.Y, closeRect.X, closeRect.Bottom);
      graphicsPath.CloseFigure();
      return graphicsPath;
    }

    /// <summary>Draws the tab.</summary>
    /// <param name="g">The g.</param>
    /// <param name="page">The page.</param>
    protected virtual void DrawTab(Graphics g, vTabPage page)
    {
      if (page == null || page.Invisible && !this.DesignMode)
        return;
      Rectangle pageRectangle = this.GetPageRectangle(g, page);
      if (pageRectangle.Width <= 0 || pageRectangle.Height <= 0)
        return;
      DrawTabPageEventArgs e = new DrawTabPageEventArgs(g, page, pageRectangle);
      this.OnDrawTabPage(e);
      if (e.Handled)
        return;
      this.sfTabs = new StringFormat();
      this.sfTabs.HotkeyPrefix = HotkeyPrefix.Show;
      this.sfTabs.Trimming = StringTrimming.EllipsisCharacter;
      if (this.RightToLeft == RightToLeft.Yes)
        this.sfTabs.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
      this.sfTabs.LineAlignment = this.helper.GetVerticalAlignment(page.TextAlignment);
      this.sfTabs.Alignment = this.helper.GetHorizontalAlignment(page.TextAlignment);
      if (!page.MultilineText)
        this.sfTabs.FormatFlags = StringFormatFlags.NoWrap;
      this.sfTabs.Trimming = StringTrimming.EllipsisCharacter;
      byte num = 15;
      byte roundedCornersBitmaskPage = 15;
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          num &= (byte) 3;
          roundedCornersBitmaskPage &= (byte) 12;
          break;
        case vTabPageAlignment.Bottom:
          num &= (byte) 12;
          roundedCornersBitmaskPage &= (byte) 3;
          break;
        case vTabPageAlignment.Left:
          num &= (byte) 5;
          roundedCornersBitmaskPage &= (byte) 10;
          break;
        case vTabPageAlignment.Right:
          num &= (byte) 10;
          roundedCornersBitmaskPage &= (byte) 5;
          break;
      }
      page.tabsFill.RoundedCornersBitmask = num;
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          page.themeTab.SetFillStyleGradientAngle(90);
          break;
        case vTabPageAlignment.Bottom:
          page.themeTab.SetFillStyleGradientAngle(-90);
          break;
        case vTabPageAlignment.Left:
          page.themeTab.SetFillStyleGradientAngle(0);
          break;
        case vTabPageAlignment.Right:
          page.themeTab.SetFillStyleGradientAngle(180);
          break;
        default:
          throw new Exception("Invalid TabPage Alignment");
      }
      ControlState controlState;
      if (this.SelectedTab != null && this.SelectedTab == page && this.HitTest(this.PointToClient(Cursor.Position)) != page)
      {
        controlState = ControlState.Pressed;
        this.DrawSelectedTab(g, page, ref pageRectangle);
      }
      else if (this.HitTest(this.PointToClient(Cursor.Position)) == page && (page.Enabled || !page.Enabled && this.DesignMode))
      {
        if (this.hScrollBar.RectangleToScreen(this.hScrollBar.ClientRectangle).Contains(Cursor.Position) && this.hScrollBar.Visible || this.vScrollBar.RectangleToScreen(this.vScrollBar.ClientRectangle).Contains(Cursor.Position) && this.vScrollBar.Visible)
        {
          controlState = ControlState.Normal;
          page.tabsFill.Bounds = pageRectangle;
          page.tabsFill.DrawElementFill(g, controlState);
          page.tabsFill.DrawElementBorder(g, controlState);
          page.backGround.RoundedCornersBitmask = roundedCornersBitmaskPage;
        }
        else
        {
          page.tabsFill.Bounds = pageRectangle;
          if (this.SelectedTab == page)
          {
            controlState = ControlState.Pressed;
            FillStyle fillStyle = page.tabsFill.Theme.StylePressed.FillStyle;
            Color borderColor = page.tabsFill.Theme.StylePressed.BorderColor;
            pageRectangle = this.DrawTabShadow(g, pageRectangle);
            int radius = page.tabsFill.Radius;
            this.DrawSelectedAndHighlightedTab(g, page, ref pageRectangle, ref radius);
            page.tabsFill.Radius = radius;
            page.backGround.RoundedCornersBitmask = roundedCornersBitmaskPage;
            page.tabsFill.Theme.StylePressed.FillStyle = fillStyle;
            page.tabsFill.Theme.StylePressed.BorderColor = borderColor;
          }
          else
          {
            controlState = ControlState.Hover;
            this.DrawHighlightedTab(g, page, roundedCornersBitmaskPage, controlState);
            this.OnHoveredTabChanged(new vTabEventArgs(page));
          }
        }
      }
      else
      {
        controlState = ControlState.Normal;
        page.tabsFill.Bounds = pageRectangle;
        this.DrawDefaultTab(g, page);
        page.backGround.RoundedCornersBitmask = roundedCornersBitmaskPage;
      }
      pageRectangle = this.DrawFitToBoundsSeparators(g, page, pageRectangle);
      SizeF empty = SizeF.Empty;
      if (this.TabsShape == TabsShape.Chrome)
      {
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
        {
          pageRectangle.X = pageRectangle.X + 10;
          pageRectangle.Width = pageRectangle.Width - 20;
        }
        else
        {
          pageRectangle.Y = pageRectangle.Y + 10;
          pageRectangle.Height = pageRectangle.Height - 20;
        }
      }
      else if (this.TabsShape == TabsShape.VisualStudio)
      {
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
        {
          pageRectangle.X = pageRectangle.X + 20;
          pageRectangle.Width = pageRectangle.Width - 20;
        }
        else
        {
          pageRectangle.Y = pageRectangle.Y + 20;
          pageRectangle.Height = pageRectangle.Height - 20;
        }
      }
      else if (this.TabsShape == TabsShape.OneNote)
      {
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
          pageRectangle.Width = pageRectangle.Width - 20;
        else
          pageRectangle.Height = pageRectangle.Height - 20;
      }
      else if (this.TabsShape == TabsShape.RoundedCorners)
      {
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
        {
          pageRectangle.X += this.CornerRadius / 2;
          pageRectangle.Width = pageRectangle.Width - this.CornerRadius / 2;
        }
        else
        {
          pageRectangle.Y += this.CornerRadius / 2;
          pageRectangle.Height = pageRectangle.Height - this.CornerRadius / 2;
        }
      }
      if (this.EnableCloseButtons && page.EnableCloseButton)
      {
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
          pageRectangle.Width = pageRectangle.Width - 10;
        else
          pageRectangle.Height = pageRectangle.Height - 10;
      }
      if (this.ImageList != null || page.Image != null)
      {
        int index = this.Controls.IndexOf((Control) page) - 2;
        if (!this.allowAutoDrawImageListImages)
          index = page.ImageIndex;
        if (this.ImageList != null || page.Image != null)
        {
          if (page.Image != null)
            empty = (SizeF) page.Image.Size;
          else if (index != -1)
            empty = (SizeF) this.ImageList.ImageSize;
          Rectangle rectangle2 = new Rectangle(pageRectangle.X + 4, pageRectangle.Y + 2 + (int) ((double) (pageRectangle.Height - (int) empty.Height) / 2.0), (int) empty.Width, (int) empty.Height);
          switch (page.TextImageRelation)
          {
            case TextImageRelation.Overlay:
              rectangle2 = new Rectangle(pageRectangle.X + pageRectangle.Width / 2 - (int) empty.Width / 2, pageRectangle.Y + (int) ((double) (pageRectangle.Height - (int) empty.Height) / 2.0), (int) empty.Width, (int) empty.Height);
              break;
            case TextImageRelation.ImageAboveText:
              rectangle2 = new Rectangle(pageRectangle.X + pageRectangle.Width / 2 - (int) empty.Width / 2, pageRectangle.Y + 2, (int) empty.Width, (int) empty.Height);
              break;
            case TextImageRelation.TextAboveImage:
              rectangle2 = new Rectangle(pageRectangle.X + pageRectangle.Width / 2 - (int) empty.Width / 2, pageRectangle.Y + pageRectangle.Height - (int) empty.Height - 2, (int) empty.Width, (int) empty.Height);
              break;
            case TextImageRelation.ImageBeforeText:
              rectangle2 = new Rectangle(pageRectangle.X + 4, pageRectangle.Y + (int) (((double) pageRectangle.Height - (double) empty.Height) / 2.0), (int) empty.Width, (int) empty.Height);
              break;
            case TextImageRelation.TextBeforeImage:
              rectangle2 = new Rectangle(pageRectangle.X + pageRectangle.Width - (int) empty.Width - 4, pageRectangle.Y + pageRectangle.Height / 2 - (int) empty.Height / 2, (int) empty.Width, (int) empty.Height);
              break;
          }
          this.DrawTabImage(g, page, ref empty, index, ref rectangle2);
        }
      }
      Rectangle layoutRectangle = new Rectangle(pageRectangle.X + (int) empty.Width, pageRectangle.Y, pageRectangle.Width - (int) empty.Width, pageRectangle.Height);
      switch (page.TextImageRelation)
      {
        case TextImageRelation.Overlay:
          layoutRectangle = new Rectangle(pageRectangle.X, pageRectangle.Y, pageRectangle.Width, pageRectangle.Height);
          break;
        case TextImageRelation.ImageAboveText:
          layoutRectangle = new Rectangle(pageRectangle.X, pageRectangle.Y + (int) empty.Height, pageRectangle.Width, pageRectangle.Height - ((int) empty.Width - 2));
          break;
        case TextImageRelation.TextAboveImage:
          layoutRectangle = new Rectangle(pageRectangle.X, pageRectangle.Y, pageRectangle.Width, pageRectangle.Height - ((int) empty.Height - 2));
          break;
        case TextImageRelation.ImageBeforeText:
          layoutRectangle = new Rectangle(pageRectangle.X + (int) empty.Width, pageRectangle.Y, pageRectangle.Width - (int) empty.Width, pageRectangle.Height);
          break;
        case TextImageRelation.TextBeforeImage:
          layoutRectangle = new Rectangle(pageRectangle.X, pageRectangle.Y, pageRectangle.Width - (int) empty.Width - 4, pageRectangle.Height);
          break;
      }
      this.DrawTabText(g, page, controlState, layoutRectangle);
      if (this.ShowFocusRectangle && this.Focused && page == this.SelectedTab)
      {
        Rectangle rectangle = new Rectangle(pageRectangle.X + 3, pageRectangle.Y + 3, pageRectangle.Width - 6, pageRectangle.Height - 5);
        ControlPaint.DrawFocusRectangle(g, rectangle);
      }
      if (!this.EnableCloseButtons || !page.EnableCloseButton || !page.IsSelected && this.ShowCloseButtonOnSelectedTabOnly)
        return;
      Rectangle rectangle1 = new Rectangle(pageRectangle.Right - 5, pageRectangle.Y + pageRectangle.Height / 2 - 6, 13, 13);
      if (this.TabAlignment == vTabPageAlignment.Left || this.TabAlignment == vTabPageAlignment.Right)
        rectangle1 = new Rectangle(pageRectangle.X + pageRectangle.Width / 2 - 3, pageRectangle.Bottom - 5, 13, 13);
      Color darkGray = Color.DarkGray;
      if (this.RectangleToScreen(rectangle1).Contains(Cursor.Position))
        this.closeButtonPage = page;
      this.DrawCloseButton(g, rectangle1, darkGray, darkGray);
    }

    private Rectangle GetCloseButton(vTabPage tabPage)
    {
      Rectangle rectangle = new Rectangle(tabPage.pageRectangle.Right, tabPage.pageRectangle.Y + tabPage.pageRectangle.Height / 2 - 5, 13, 13);
      if (this.TabAlignment == vTabPageAlignment.Left || this.TabAlignment == vTabPageAlignment.Right)
        rectangle = new Rectangle(tabPage.pageRectangle.X + tabPage.pageRectangle.Width / 2 - 5, tabPage.pageRectangle.Bottom, 13, 13);
      return rectangle;
    }

    /// <summary>Draws the close button.</summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="bounds">The bounds.</param>
    /// <param name="color1">The color1.</param>
    /// <param name="color2">The color2.</param>
    protected virtual void DrawCloseButton(Graphics graphics, Rectangle bounds, Color color1, Color color2)
    {
      if (this.RectangleToScreen(bounds).Contains(Cursor.Position))
      {
        Color[] colors = this.theme.StylePressed.FillStyle.Colors;
        float[] colorOffsets = new float[4]{ 0.0f, 0.4f, 0.5f, 1f };
        if (colors.Length < 3)
          colorOffsets = new float[2]{ 0.0f, 1f };
        Rectangle rectangle = bounds;
        this.paintHelper.DrawGradientRectFigure(graphics, rectangle, colors, colorOffsets, GradientStyles.Linear, 90.0, 0.5, 0.5);
        this.paintHelper.DrawGradientRectBorder(graphics, rectangle, new Color[2]
        {
          this.theme.StyleHighlight.BorderColor,
          this.theme.StyleHighlight.BorderColor
        }, new float[2]{ 0.0f, 1f }, GradientStyles.Linear, 90.0, 0.5, 0.5);
      }
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, color1, color1, LinearGradientMode.Vertical))
      {
        Rectangle rectangle = bounds;
        using (Pen pen = new Pen((Brush) linearGradientBrush))
        {
          int num1 = rectangle.X + 3;
          int num2 = num1 + 5;
          int y1 = rectangle.Y + 4;
          int y2 = y1 + 5;
          for (int index = 0; index < 2; ++index)
          {
            graphics.DrawLine(pen, num1 + 1 + index, y1, num2 + index, y2);
            graphics.DrawLine(pen, num2 + index, y1, num1 + 1 + index, y2);
          }
        }
      }
    }

    public virtual void DrawDefaultTab(Graphics g, vTabPage page)
    {
      if (this.TabsShape == TabsShape.Chrome)
      {
        GraphicsPath chromePath = this.GetChromePath(page.tabsFill.Bounds);
        this.SelectedTab.tabsFill.Bounds = page.tabsFill.Bounds;
        page.tabsFill.DrawStandardFill(g, ControlState.Normal, GradientStyles.Linear, chromePath);
        page.tabsFill.DrawElementBorder(g, ControlState.Normal, chromePath);
      }
      else if (this.TabsShape == TabsShape.VisualStudio)
      {
        GraphicsPath vsPath = this.GetVSPath(page.tabsFill.Bounds);
        this.SelectedTab.tabsFill.Bounds = page.tabsFill.Bounds;
        page.tabsFill.DrawStandardFill(g, ControlState.Normal, GradientStyles.Linear, vsPath);
        page.tabsFill.DrawElementBorder(g, ControlState.Normal, vsPath);
      }
      else if (this.TabsShape == TabsShape.RoundedCorners)
      {
        GraphicsPath roundedCornersPath = this.GetRoundedCornersPath(page.tabsFill.Bounds);
        this.SelectedTab.tabsFill.Bounds = page.tabsFill.Bounds;
        page.tabsFill.DrawStandardFill(g, ControlState.Normal, GradientStyles.Linear, roundedCornersPath);
        page.tabsFill.DrawElementBorder(g, ControlState.Normal, roundedCornersPath);
      }
      else if (this.TabsShape == TabsShape.OneNote)
      {
        GraphicsPath oneNotePath = this.GetOneNotePath(page.tabsFill.Bounds);
        this.SelectedTab.tabsFill.Bounds = page.tabsFill.Bounds;
        page.tabsFill.DrawStandardFill(g, ControlState.Normal, GradientStyles.Linear, oneNotePath);
        page.tabsFill.DrawElementBorder(g, ControlState.Normal, oneNotePath);
      }
      else
      {
        page.tabsFill.DrawElementFill(g, ControlState.Normal);
        page.tabsFill.DrawElementBorder(g, ControlState.Normal);
      }
    }

    public virtual void DrawHighlightedTab(Graphics g, vTabPage page, byte roundedCornersBitmaskPage, ControlState tabState)
    {
      if (this.TabsShape == TabsShape.Chrome)
      {
        GraphicsPath chromePath = this.GetChromePath(page.tabsFill.Bounds);
        this.SelectedTab.tabsFill.Bounds = page.tabsFill.Bounds;
        page.tabsFill.DrawStandardFill(g, tabState, GradientStyles.Linear, chromePath);
        page.tabsFill.DrawElementBorder(g, tabState, chromePath);
      }
      else if (this.TabsShape == TabsShape.VisualStudio)
      {
        GraphicsPath vsPath = this.GetVSPath(page.tabsFill.Bounds);
        this.SelectedTab.tabsFill.Bounds = page.tabsFill.Bounds;
        page.tabsFill.DrawStandardFill(g, tabState, GradientStyles.Linear, vsPath);
        page.tabsFill.DrawElementBorder(g, tabState, vsPath);
      }
      else if (this.TabsShape == TabsShape.RoundedCorners)
      {
        GraphicsPath roundedCornersPath = this.GetRoundedCornersPath(page.tabsFill.Bounds);
        this.SelectedTab.tabsFill.Bounds = page.tabsFill.Bounds;
        page.tabsFill.DrawStandardFill(g, tabState, GradientStyles.Linear, roundedCornersPath);
        page.tabsFill.DrawElementBorder(g, tabState, roundedCornersPath);
      }
      else if (this.TabsShape == TabsShape.OneNote)
      {
        GraphicsPath oneNotePath = this.GetOneNotePath(page.tabsFill.Bounds);
        this.SelectedTab.tabsFill.Bounds = page.tabsFill.Bounds;
        page.tabsFill.DrawStandardFill(g, tabState, GradientStyles.Linear, oneNotePath);
        page.tabsFill.DrawElementBorder(g, tabState, oneNotePath);
      }
      else
      {
        page.tabsFill.DrawElementFill(g, tabState);
        page.tabsFill.DrawElementBorder(g, tabState);
        page.backGround.RoundedCornersBitmask = roundedCornersBitmaskPage;
        Color color = this.Theme.QueryColorSetter("TabControlHighlightInnerBorder");
        if (!(color != Color.Empty) || this.SelectedTab == page)
          return;
        Rectangle rectangle = page.tabsFill.Bounds;
        rectangle = new Rectangle(rectangle.X + 1, rectangle.Y + 1, rectangle.Width - 2, rectangle.Height);
        page.tabsFill.Bounds = rectangle;
        page.tabsFill.DrawElementBorder(g, ControlState.Hover, color);
      }
    }

    private GraphicsPath GetOneNotePath(Rectangle tabBounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.Right - tabBounds.Height + 4, tabBounds.Y + 2);
          graphicsPath.AddLine(tabBounds.Right - tabBounds.Height, tabBounds.Y, tabBounds.X + 3, tabBounds.Y);
          graphicsPath.AddArc(tabBounds.X, tabBounds.Y, 6, 6, 90f, 0.0f);
          graphicsPath.AddLine(tabBounds.X, tabBounds.Y + 3, tabBounds.X, tabBounds.Bottom);
          break;
        case vTabPageAlignment.Bottom:
          graphicsPath.AddLine(tabBounds.X, tabBounds.Y, tabBounds.X, tabBounds.Bottom - 3);
          graphicsPath.AddArc(tabBounds.X, tabBounds.Bottom - 6, 6, 6, 90f, 0.0f);
          graphicsPath.AddLine(tabBounds.X + 3, tabBounds.Bottom, tabBounds.Right - tabBounds.Height, tabBounds.Bottom);
          graphicsPath.AddLine(tabBounds.Right - tabBounds.Height + 4, tabBounds.Bottom - 2, tabBounds.Right, tabBounds.Y);
          break;
        case vTabPageAlignment.Left:
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.X + 3, tabBounds.Y);
          graphicsPath.AddArc(tabBounds.X, tabBounds.Y, 6, 6, 90f, 0.0f);
          graphicsPath.AddLine(tabBounds.X, tabBounds.Y + 3, tabBounds.X, tabBounds.Bottom - tabBounds.Width);
          graphicsPath.AddLine(tabBounds.X + 2, tabBounds.Bottom - tabBounds.Width + 4, tabBounds.Right, tabBounds.Bottom);
          break;
        case vTabPageAlignment.Right:
          graphicsPath.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.Right - 2, tabBounds.Bottom - tabBounds.Width + 4);
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Bottom - tabBounds.Width, tabBounds.Right, tabBounds.Y + 3);
          graphicsPath.AddArc(tabBounds.Right - 6, tabBounds.Y + 6, 6, 6, 90f, 0.0f);
          graphicsPath.AddLine(tabBounds.Right - 3, tabBounds.Y, tabBounds.X, tabBounds.Y);
          break;
      }
      return graphicsPath;
    }

    private GraphicsPath GetRoundedCornersPath(Rectangle tabBounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          graphicsPath.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X, tabBounds.Y + this.CornerRadius);
          graphicsPath.AddArc(tabBounds.X, tabBounds.Y, this.CornerRadius * 2, this.CornerRadius * 2, 180f, 90f);
          graphicsPath.AddLine(tabBounds.X + this.CornerRadius, tabBounds.Y, tabBounds.Right - this.CornerRadius, tabBounds.Y);
          graphicsPath.AddArc(tabBounds.Right - this.CornerRadius * 2, tabBounds.Y, this.CornerRadius * 2, this.CornerRadius * 2, 270f, 90f);
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Y + this.CornerRadius, tabBounds.Right, tabBounds.Bottom);
          break;
        case vTabPageAlignment.Bottom:
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom - this.CornerRadius);
          graphicsPath.AddArc(tabBounds.Right - this.CornerRadius * 2, tabBounds.Bottom - this.CornerRadius * 2, this.CornerRadius * 2, this.CornerRadius * 2, 0.0f, 90f);
          graphicsPath.AddLine(tabBounds.Right - this.CornerRadius, tabBounds.Bottom, tabBounds.X + this.CornerRadius, tabBounds.Bottom);
          graphicsPath.AddArc(tabBounds.X, tabBounds.Bottom - this.CornerRadius * 2, this.CornerRadius * 2, this.CornerRadius * 2, 90f, 90f);
          graphicsPath.AddLine(tabBounds.X, tabBounds.Bottom - this.CornerRadius, tabBounds.X, tabBounds.Y);
          break;
        case vTabPageAlignment.Left:
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X + this.CornerRadius, tabBounds.Bottom);
          graphicsPath.AddArc(tabBounds.X, tabBounds.Bottom - this.CornerRadius * 2, this.CornerRadius * 2, this.CornerRadius * 2, 90f, 90f);
          graphicsPath.AddLine(tabBounds.X, tabBounds.Bottom - this.CornerRadius, tabBounds.X, tabBounds.Y + this.CornerRadius);
          graphicsPath.AddArc(tabBounds.X, tabBounds.Y, this.CornerRadius * 2, this.CornerRadius * 2, 180f, 90f);
          graphicsPath.AddLine(tabBounds.X + this.CornerRadius, tabBounds.Y, tabBounds.Right, tabBounds.Y);
          break;
        case vTabPageAlignment.Right:
          graphicsPath.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right - this.CornerRadius, tabBounds.Y);
          graphicsPath.AddArc(tabBounds.Right - this.CornerRadius * 2, tabBounds.Y, this.CornerRadius * 2, this.CornerRadius * 2, 270f, 90f);
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Y + this.CornerRadius, tabBounds.Right, tabBounds.Bottom - this.CornerRadius);
          graphicsPath.AddArc(tabBounds.Right - this.CornerRadius * 2, tabBounds.Bottom - this.CornerRadius * 2, this.CornerRadius * 2, this.CornerRadius * 2, 0.0f, 90f);
          graphicsPath.AddLine(tabBounds.Right - this.CornerRadius, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
          break;
      }
      return graphicsPath;
    }

    private GraphicsPath GetVSPath(Rectangle tabBounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          graphicsPath.AddLine(tabBounds.X, tabBounds.Bottom, tabBounds.X + tabBounds.Height - 4, tabBounds.Y + 2);
          graphicsPath.AddLine(tabBounds.X + tabBounds.Height, tabBounds.Y, tabBounds.Right - 3, tabBounds.Y);
          graphicsPath.AddArc(tabBounds.Right - 6, tabBounds.Y, 6, 6, 270f, 90f);
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Y + 3, tabBounds.Right, tabBounds.Bottom);
          break;
        case vTabPageAlignment.Bottom:
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Y, tabBounds.Right, tabBounds.Bottom - 3);
          graphicsPath.AddArc(tabBounds.Right - 6, tabBounds.Bottom - 6, 6, 6, 0.0f, 90f);
          graphicsPath.AddLine(tabBounds.Right - 3, tabBounds.Bottom, tabBounds.X + tabBounds.Height, tabBounds.Bottom);
          graphicsPath.AddLine(tabBounds.X + tabBounds.Height - 4, tabBounds.Bottom - 2, tabBounds.X, tabBounds.Y);
          break;
        case vTabPageAlignment.Left:
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Bottom, tabBounds.X + 3, tabBounds.Bottom);
          graphicsPath.AddArc(tabBounds.X, tabBounds.Bottom - 6, 6, 6, 90f, 90f);
          graphicsPath.AddLine(tabBounds.X, tabBounds.Bottom - 3, tabBounds.X, tabBounds.Y + tabBounds.Width);
          graphicsPath.AddLine(tabBounds.X + 2, tabBounds.Y + tabBounds.Width - 4, tabBounds.Right, tabBounds.Y);
          break;
        case vTabPageAlignment.Right:
          graphicsPath.AddLine(tabBounds.X, tabBounds.Y, tabBounds.Right - 2, tabBounds.Y + tabBounds.Width - 4);
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Y + tabBounds.Width, tabBounds.Right, tabBounds.Bottom - 3);
          graphicsPath.AddArc(tabBounds.Right - 6, tabBounds.Bottom - 6, 6, 6, 0.0f, 90f);
          graphicsPath.AddLine(tabBounds.Right - 3, tabBounds.Bottom, tabBounds.X, tabBounds.Bottom);
          break;
      }
      return graphicsPath;
    }

    private GraphicsPath GetChromePath(Rectangle tabBounds)
    {
      GraphicsPath graphicsPath = new GraphicsPath();
      int num1;
      int num2;
      int num3;
      int num4;
      if (this.TabAlignment <= vTabPageAlignment.Bottom)
      {
        num1 = (int) Math.Floor((Decimal) tabBounds.Height * new Decimal(2) / new Decimal(3));
        num2 = (int) Math.Floor((Decimal) tabBounds.Height * new Decimal(1) / new Decimal(8));
        num3 = (int) Math.Floor((Decimal) tabBounds.Height * new Decimal(1) / new Decimal(6));
        num4 = (int) Math.Floor((Decimal) tabBounds.Height * new Decimal(1) / new Decimal(4));
      }
      else
      {
        num1 = (int) Math.Floor((Decimal) tabBounds.Width * new Decimal(2) / new Decimal(3));
        num2 = (int) Math.Floor((Decimal) tabBounds.Width * new Decimal(1) / new Decimal(8));
        num3 = (int) Math.Floor((Decimal) tabBounds.Width * new Decimal(1) / new Decimal(6));
        num4 = (int) Math.Floor((Decimal) tabBounds.Width * new Decimal(1) / new Decimal(4));
      }
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          graphicsPath.AddCurve(new Point[4]
          {
            new Point(tabBounds.X, tabBounds.Bottom),
            new Point(tabBounds.X + num3, tabBounds.Bottom - num2),
            new Point(tabBounds.X + num1 - num4, tabBounds.Y + num2),
            new Point(tabBounds.X + num1, tabBounds.Y)
          });
          graphicsPath.AddLine(tabBounds.X + num1, tabBounds.Y, tabBounds.Right - num1, tabBounds.Y);
          graphicsPath.AddCurve(new Point[4]
          {
            new Point(tabBounds.Right - num1, tabBounds.Y),
            new Point(tabBounds.Right - num1 + num4, tabBounds.Y + num2),
            new Point(tabBounds.Right - num3, tabBounds.Bottom - num2),
            new Point(tabBounds.Right, tabBounds.Bottom)
          });
          break;
        case vTabPageAlignment.Bottom:
          graphicsPath.AddCurve(new Point[4]
          {
            new Point(tabBounds.Right, tabBounds.Y),
            new Point(tabBounds.Right - num3, tabBounds.Y + num2),
            new Point(tabBounds.Right - num1 + num4, tabBounds.Bottom - num2),
            new Point(tabBounds.Right - num1, tabBounds.Bottom)
          });
          graphicsPath.AddLine(tabBounds.Right - num1, tabBounds.Bottom, tabBounds.X + num1, tabBounds.Bottom);
          graphicsPath.AddCurve(new Point[4]
          {
            new Point(tabBounds.X + num1, tabBounds.Bottom),
            new Point(tabBounds.X + num1 - num4, tabBounds.Bottom - num2),
            new Point(tabBounds.X + num3, tabBounds.Y + num2),
            new Point(tabBounds.X, tabBounds.Y)
          });
          break;
        case vTabPageAlignment.Left:
          graphicsPath.AddCurve(new Point[4]
          {
            new Point(tabBounds.Right, tabBounds.Bottom),
            new Point(tabBounds.Right - num2, tabBounds.Bottom - num3),
            new Point(tabBounds.X + num2, tabBounds.Bottom - num1 + num4),
            new Point(tabBounds.X, tabBounds.Bottom - num1)
          });
          graphicsPath.AddLine(tabBounds.X, tabBounds.Bottom - num1, tabBounds.X, tabBounds.Y + num1);
          graphicsPath.AddCurve(new Point[4]
          {
            new Point(tabBounds.X, tabBounds.Y + num1),
            new Point(tabBounds.X + num2, tabBounds.Y + num1 - num4),
            new Point(tabBounds.Right - num2, tabBounds.Y + num3),
            new Point(tabBounds.Right, tabBounds.Y)
          });
          break;
        case vTabPageAlignment.Right:
          graphicsPath.AddCurve(new Point[4]
          {
            new Point(tabBounds.X, tabBounds.Y),
            new Point(tabBounds.X + num2, tabBounds.Y + num3),
            new Point(tabBounds.Right - num2, tabBounds.Y + num1 - num4),
            new Point(tabBounds.Right, tabBounds.Y + num1)
          });
          graphicsPath.AddLine(tabBounds.Right, tabBounds.Y + num1, tabBounds.Right, tabBounds.Bottom - num1);
          graphicsPath.AddCurve(new Point[4]
          {
            new Point(tabBounds.Right, tabBounds.Bottom - num1),
            new Point(tabBounds.Right - num2, tabBounds.Bottom - num1 + num4),
            new Point(tabBounds.X + num2, tabBounds.Bottom - num3),
            new Point(tabBounds.X, tabBounds.Bottom)
          });
          break;
      }
      return graphicsPath;
    }

    public virtual void DrawSelectedAndHighlightedTab(Graphics g, vTabPage page, ref Rectangle pageRectangle, ref int pageRadius)
    {
      if (page.glossyColor != Color.Empty)
        this.DrawTabGlossyEffect(g, pageRectangle, page);
      int num = 0;
      if (this.TabsShape == TabsShape.Chrome)
      {
        GraphicsPath chromePath = this.GetChromePath(pageRectangle);
        this.SelectedTab.tabsFill.Bounds = pageRectangle;
        page.tabsFill.DrawStandardFill(g, ControlState.Pressed, GradientStyles.Linear, chromePath);
        page.tabsFill.DrawElementBorder(g, ControlState.Pressed, chromePath);
      }
      else if (this.TabsShape == TabsShape.VisualStudio)
      {
        GraphicsPath vsPath = this.GetVSPath(pageRectangle);
        this.SelectedTab.tabsFill.Bounds = pageRectangle;
        page.tabsFill.DrawStandardFill(g, ControlState.Pressed, GradientStyles.Linear, vsPath);
        page.tabsFill.DrawElementBorder(g, ControlState.Pressed, vsPath);
      }
      else if (this.TabsShape == TabsShape.OneNote)
      {
        GraphicsPath oneNotePath = this.GetOneNotePath(pageRectangle);
        this.SelectedTab.tabsFill.Bounds = pageRectangle;
        page.tabsFill.DrawStandardFill(g, ControlState.Pressed, GradientStyles.Linear, oneNotePath);
        page.tabsFill.DrawElementBorder(g, ControlState.Pressed, oneNotePath);
      }
      else if (this.TabsShape == TabsShape.RoundedCorners)
      {
        GraphicsPath roundedCornersPath = this.GetRoundedCornersPath(pageRectangle);
        this.SelectedTab.tabsFill.Bounds = pageRectangle;
        page.tabsFill.DrawStandardFill(g, ControlState.Pressed, GradientStyles.Linear, roundedCornersPath);
        page.tabsFill.DrawElementBorder(g, ControlState.Pressed, roundedCornersPath);
      }
      else if (this.TabsShape != TabsShape.Office2007)
      {
        page.tabsFill.DrawElementFill(g, ControlState.Pressed);
        page.tabsFill.DrawElementBorder(g, ControlState.Pressed);
      }
      else
      {
        num = page.tabsFill.Radius;
        page.tabsFill.Radius = 2;
        Rectangle rectangle1 = new Rectangle(pageRectangle.X + 1, pageRectangle.Y, pageRectangle.Width - 2, pageRectangle.Height);
        if (this.TabAlignment == vTabPageAlignment.Left || this.TabAlignment == vTabPageAlignment.Right)
          rectangle1 = new Rectangle(pageRectangle.X - 1, pageRectangle.Y + 1, pageRectangle.Width + 1, pageRectangle.Height - 2);
        page.tabsFill.DrawElementFill(g, ControlState.Pressed);
        this.SelectedTab.tabsFill.Bounds = rectangle1;
        Rectangle rectangle2 = new Rectangle(rectangle1.X - 3, rectangle1.Y, rectangle1.Width + 6, rectangle1.Height);
        if (this.TabAlignment == vTabPageAlignment.Left || this.TabAlignment == vTabPageAlignment.Right)
          rectangle2 = new Rectangle(rectangle1.X, rectangle1.Y - 3, rectangle1.Width, rectangle1.Height + 6);
        Point[] tabbedPath = this.GetTabbedPath(rectangle2, this.TabAlignment);
        if (page.glossyColor != Color.Empty)
          page.tabsFill.DrawElementBorder(g, ControlState.Pressed, tabbedPath, page.glossyColor);
        else
          page.tabsFill.DrawElementBorder(g, ControlState.Pressed, tabbedPath);
        Rectangle borderRectangle = this.DrawTab3DBorders(g, rectangle2, page, true);
        this.DrawGlossyBorder(g, page, borderRectangle);
      }
      pageRadius = num;
    }

    public virtual void DrawSelectedTab(Graphics g, vTabPage page, ref Rectangle pageRectangle)
    {
      pageRectangle = this.DrawTabShadow(g, pageRectangle);
      this.SelectedTab.tabsFill.Bounds = pageRectangle;
      if (this.TabsShape == TabsShape.Chrome)
      {
        GraphicsPath chromePath = this.GetChromePath(pageRectangle);
        this.SelectedTab.tabsFill.Bounds = pageRectangle;
        page.tabsFill.DrawStandardFill(g, ControlState.Pressed, GradientStyles.Linear, chromePath);
        page.tabsFill.DrawElementBorder(g, ControlState.Pressed, chromePath);
      }
      else if (this.TabsShape == TabsShape.VisualStudio)
      {
        GraphicsPath vsPath = this.GetVSPath(pageRectangle);
        this.SelectedTab.tabsFill.Bounds = pageRectangle;
        page.tabsFill.DrawStandardFill(g, ControlState.Pressed, GradientStyles.Linear, vsPath);
        page.tabsFill.DrawElementBorder(g, ControlState.Pressed, vsPath);
      }
      else if (this.TabsShape == TabsShape.RoundedCorners)
      {
        GraphicsPath roundedCornersPath = this.GetRoundedCornersPath(pageRectangle);
        this.SelectedTab.tabsFill.Bounds = pageRectangle;
        page.tabsFill.DrawStandardFill(g, ControlState.Pressed, GradientStyles.Linear, roundedCornersPath);
        page.tabsFill.DrawElementBorder(g, ControlState.Pressed, roundedCornersPath);
      }
      else if (this.TabsShape == TabsShape.OneNote)
      {
        GraphicsPath oneNotePath = this.GetOneNotePath(pageRectangle);
        this.SelectedTab.tabsFill.Bounds = pageRectangle;
        page.tabsFill.DrawStandardFill(g, ControlState.Pressed, GradientStyles.Linear, oneNotePath);
        page.tabsFill.DrawElementBorder(g, ControlState.Pressed, oneNotePath);
      }
      else if (this.TabsShape != TabsShape.Office2007)
      {
        this.SelectedTab.tabsFill.DrawElementFill(g, ControlState.Pressed);
        this.SelectedTab.tabsFill.DrawElementBorder(g, ControlState.Pressed);
      }
      else
      {
        int radius = this.SelectedTab.tabsFill.Radius;
        this.SelectedTab.tabsFill.Radius = 2;
        Rectangle rectangle1 = new Rectangle(pageRectangle.X + 1, pageRectangle.Y, pageRectangle.Width - 2, pageRectangle.Height);
        if (this.TabAlignment == vTabPageAlignment.Left || this.TabAlignment == vTabPageAlignment.Right)
          rectangle1 = new Rectangle(pageRectangle.X - 1, pageRectangle.Y + 1, pageRectangle.Width + 1, pageRectangle.Height - 2);
        this.SelectedTab.tabsFill.DrawElementFill(g, ControlState.Pressed);
        Rectangle rectangle2 = new Rectangle(rectangle1.X - 3, rectangle1.Y, rectangle1.Width + 6, rectangle1.Height);
        if (this.TabAlignment == vTabPageAlignment.Left)
          rectangle2 = new Rectangle(rectangle1.X + 1, rectangle1.Y - 3, rectangle1.Width - 1, rectangle1.Height + 6);
        else if (this.TabAlignment == vTabPageAlignment.Right)
          rectangle2 = new Rectangle(rectangle1.X, rectangle1.Y - 3, rectangle1.Width, rectangle1.Height + 6);
        Point[] tabbedPath = this.GetTabbedPath(rectangle2, this.TabAlignment);
        rectangle2 = this.DrawTab3DBorders(g, rectangle2, page, false);
        this.SelectedTab.tabsFill.DrawElementBorder(g, ControlState.Pressed, tabbedPath);
        this.SelectedTab.tabsFill.Radius = radius;
      }
    }

    private Rectangle DrawGlossyBorder(Graphics g, vTabPage page, Rectangle borderRectangle)
    {
      if (page.glossyColor != Color.Empty)
      {
        using (Pen pen = new Pen(Color.FromArgb(50, page.glossyColor)))
        {
          if (this.TabAlignment == vTabPageAlignment.Top)
          {
            g.DrawLine(pen, borderRectangle.Left + 3, borderRectangle.Top + 2, borderRectangle.Right - 3, borderRectangle.Top + 2);
            g.DrawLine(pen, borderRectangle.Left + 3, borderRectangle.Top + 1, borderRectangle.Right - 3, borderRectangle.Top + 1);
          }
          else if (this.TabAlignment == vTabPageAlignment.Bottom)
          {
            g.DrawLine(pen, borderRectangle.Left + 3, borderRectangle.Bottom - 2, borderRectangle.Right - 3, borderRectangle.Bottom - 2);
            g.DrawLine(pen, borderRectangle.Left + 3, borderRectangle.Bottom - 1, borderRectangle.Right - 3, borderRectangle.Bottom - 1);
          }
          else if (this.TabAlignment == vTabPageAlignment.Left)
          {
            g.DrawLine(pen, borderRectangle.Left + 1, borderRectangle.Top + 3, borderRectangle.Left + 1, borderRectangle.Bottom - 3);
            g.DrawLine(pen, borderRectangle.Left + 2, borderRectangle.Top + 3, borderRectangle.Left + 2, borderRectangle.Bottom - 3);
          }
          else if (this.TabAlignment == vTabPageAlignment.Right)
          {
            g.DrawLine(pen, borderRectangle.Right - 1, borderRectangle.Top + 3, borderRectangle.Right - 1, borderRectangle.Bottom - 3);
            g.DrawLine(pen, borderRectangle.Right - 2, borderRectangle.Top + 3, borderRectangle.Right - 2, borderRectangle.Bottom - 3);
          }
        }
      }
      return borderRectangle;
    }

    private Rectangle DrawFitToBoundsSeparators(Graphics g, vTabPage page, Rectangle pageRectangle)
    {
      if (this.FitTabsToBounds && page != this.SelectedTab)
      {
        int wantedPageWidth = this.GetWantedPageWidth(g, page);
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
        {
          if (page.TabContext == null && wantedPageWidth > pageRectangle.Width)
          {
            using (Pen pen = new Pen(Color.FromArgb((int) page.tabsFill.BorderColor.R, (int) page.tabsFill.BorderColor.G, (int) page.tabsFill.BorderColor.B)))
              g.DrawLine(pen, pageRectangle.Right, pageRectangle.Top, pageRectangle.Right, pageRectangle.Bottom);
          }
        }
        else if (wantedPageWidth > pageRectangle.Height)
        {
          using (Pen pen = new Pen(Color.FromArgb((int) page.tabsFill.BorderColor.R, (int) page.tabsFill.BorderColor.G, (int) page.tabsFill.BorderColor.B)))
            g.DrawLine(pen, pageRectangle.Left, pageRectangle.Bottom, pageRectangle.Right, pageRectangle.Bottom);
        }
      }
      return pageRectangle;
    }

    private Rectangle DrawTab3DBorders(Graphics g, Rectangle borderRectangle, vTabPage page, bool glossy)
    {
      using (Pen pen = new Pen(Color.FromArgb(50, this.selectedTab.tabsFill.PressedBorderColor)))
      {
        if (page.glossyColor != Color.Empty && glossy)
          pen.Color = Color.FromArgb(100, page.glossyColor);
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
        {
          g.DrawLine(pen, borderRectangle.Left + 3, borderRectangle.Top + 3, borderRectangle.Left + 3, borderRectangle.Bottom - 2);
          g.DrawLine(pen, borderRectangle.Right - 3, borderRectangle.Top + 3, borderRectangle.Right - 3, borderRectangle.Bottom - 2);
        }
        else
        {
          g.DrawLine(pen, borderRectangle.Left + 3, borderRectangle.Top + 3, borderRectangle.Right - 2, borderRectangle.Top + 3);
          g.DrawLine(pen, borderRectangle.Left + 3, borderRectangle.Bottom - 3, borderRectangle.Right - 2, borderRectangle.Bottom - 3);
        }
      }
      return borderRectangle;
    }

    private Rectangle DrawTabShadow(Graphics g, Rectangle pageRectangle)
    {
      if (this.TabsShape == TabsShape.Chrome || this.TabsShape == TabsShape.OneNote || this.TabsShape == TabsShape.VisualStudio)
        return pageRectangle;
      Rectangle bounds = pageRectangle;
      if (bounds.Width <= 0 || bounds.Height <= 0)
        return Rectangle.Empty;
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          bounds.Offset(3, 4);
          break;
        case vTabPageAlignment.Bottom:
          bounds.Offset(3, -4);
          break;
        case vTabPageAlignment.Left:
          bounds.Offset(4, 3);
          break;
        case vTabPageAlignment.Right:
          bounds.Offset(-4, 3);
          break;
      }
      using (GraphicsPath roundedPathRect = this.helper.GetRoundedPathRect(bounds, 6))
      {
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(roundedPathRect))
        {
          pathGradientBrush.WrapMode = WrapMode.Clamp;
          pathGradientBrush.InterpolationColors = new ColorBlend(3)
          {
            Colors = new Color[3]
            {
              Color.Transparent,
              Color.FromArgb(30, Color.FromArgb(70, 70, 70)),
              Color.FromArgb(60, Color.FromArgb(70, 70, 70))
            },
            Positions = new float[3]{ 0.0f, 0.1f, 1f }
          };
          g.FillPath((Brush) pathGradientBrush, roundedPathRect);
        }
      }
      return pageRectangle;
    }

    private Rectangle DrawTabGlossyEffect(Graphics g, Rectangle pageRectangle, vTabPage page)
    {
      Color color = page.glossyColor;
      Rectangle bounds = new Rectangle(pageRectangle.X - 3, pageRectangle.Y - 2, pageRectangle.Width + 6, pageRectangle.Height + 4);
      if (this.TabsShape != TabsShape.Office2007 || bounds.Width <= 0 || bounds.Height <= 0)
        return Rectangle.Empty;
      using (GraphicsPath roundedPathRect = this.helper.GetRoundedPathRect(bounds, 6))
      {
        using (PathGradientBrush pathGradientBrush = new PathGradientBrush(roundedPathRect))
        {
          pathGradientBrush.WrapMode = WrapMode.Clamp;
          pathGradientBrush.InterpolationColors = new ColorBlend(3)
          {
            Colors = new Color[3]
            {
              Color.Transparent,
              Color.FromArgb(200, page.glossyColor),
              Color.FromArgb((int) byte.MaxValue, page.glossyColor)
            },
            Positions = new float[3]{ 0.0f, 0.1f, 1f }
          };
          g.FillPath((Brush) pathGradientBrush, roundedPathRect);
        }
      }
      return pageRectangle;
    }

    protected virtual void DrawTabImage(Graphics g, vTabPage page, ref SizeF empty, int index, ref Rectangle rectangle2)
    {
      if ((double) empty.Width == 0.0)
        return;
      if (this.imageList != null && this.ImageList.Images.Count > page.ImageIndex)
      {
        if (index < 0)
          return;
        Image image = this.imageList.Images[index];
        g.DrawImage(image, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height);
      }
      else
      {
        if (page.Image == null)
          return;
        g.DrawImage(page.Image, rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height);
      }
    }

    public virtual Rectangle DrawTabText(Graphics g, vTabPage page, ControlState tabState, Rectangle layoutRectangle)
    {
      using (SolidBrush brush = new SolidBrush(page.tabsFill.ForeColor))
      {
        if (!page.UseThemeTextColor)
          brush.Color = page.TextColor;
        if (tabState == ControlState.Pressed)
        {
          brush.Color = page.tabsFill.PressedForeColor;
          if (!page.UseThemeTextColor)
            brush.Color = page.PressedTextColor;
        }
        else if (tabState == ControlState.Hover)
        {
          brush.Color = page.tabsFill.HighlightForeColor;
          if (!page.UseThemeTextColor)
            brush.Color = page.HighlightTextColor;
        }
        if (!page.Enabled)
          brush.Color = page.tabsFill.DisabledForeColor;
        SmoothingMode smoothingMode = g.SmoothingMode;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Font font = page.Font;
        if (!page.UseDefaultTextFont)
        {
          font = page.TextFont;
          if (page == this.SelectedTab)
            font = page.SelectedTextFont;
        }
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
        {
          if (this.TextOrientation == vTabPageTextOrientation.Vertical)
            this.DrawTextLeftRotated(g, font, layoutRectangle, page.Text, brush, this.sfTabs);
          else
            g.DrawString(page.Text, font, (Brush) brush, (RectangleF) layoutRectangle, this.sfTabs);
        }
        else if (this.TextOrientation != vTabPageTextOrientation.Vertical)
        {
          if (this.RightToLeft == RightToLeft.Yes)
            this.sfTabs.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
          if (this.TabAlignment == vTabPageAlignment.Right)
          {
            StringFormat format = this.CreateFormat(page);
            format.FormatFlags |= StringFormatFlags.DirectionVertical;
            g.DrawString(page.Text, font, (Brush) brush, (RectangleF) layoutRectangle, format);
          }
          else
            this.DrawTextLeftRotated(g, font, layoutRectangle, page.Text, brush, this.sfTabs);
        }
        else
          g.DrawString(page.Text, font, (Brush) brush, (RectangleF) layoutRectangle, this.sfTabs);
        g.SmoothingMode = smoothingMode;
      }
      return layoutRectangle;
    }

    public virtual Rectangle DrawTabText(Graphics g, vTabPage page, Color textColor, Rectangle layoutRectangle)
    {
      if (page == null)
        return layoutRectangle;
      using (SolidBrush brush = new SolidBrush(textColor))
      {
        SmoothingMode smoothingMode = g.SmoothingMode;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        Font font = page.Font;
        if (!page.UseDefaultTextFont)
        {
          font = page.TextFont;
          if (page == this.SelectedTab)
            font = page.SelectedTextFont;
        }
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
        {
          if (this.TextOrientation == vTabPageTextOrientation.Vertical)
            this.DrawTextLeftRotated(g, font, layoutRectangle, page.Text, brush, this.sfTabs);
          else
            g.DrawString(page.Text, font, (Brush) brush, (RectangleF) layoutRectangle, this.sfTabs);
        }
        else if (this.TextOrientation != vTabPageTextOrientation.Vertical)
        {
          if (this.RightToLeft == RightToLeft.Yes)
            this.sfTabs.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
          if (this.TabAlignment == vTabPageAlignment.Right)
          {
            StringFormat format = this.CreateFormat(page);
            format.FormatFlags |= StringFormatFlags.DirectionVertical;
            g.DrawString(page.Text, font, (Brush) brush, (RectangleF) layoutRectangle, format);
          }
          else
            this.DrawTextLeftRotated(g, font, layoutRectangle, page.Text, brush, this.sfTabs);
        }
        else
          g.DrawString(page.Text, font, (Brush) brush, (RectangleF) layoutRectangle, this.sfTabs);
        g.SmoothingMode = smoothingMode;
      }
      return layoutRectangle;
    }

    protected internal Rectangle GetPageRectangle(Graphics g, vTabPage page)
    {
      if (page == null)
        return Rectangle.Empty;
      if (this.widthPages == null || this.TabPages.Count > this.widthPages.Length)
        this.CalculatePagesWidth(g);
      int panelStartPos = this.GetPanelStartPos();
      int num1 = 10;
      double num2 = 1.0;
      int index = 0;
      if (page.TabContext == null)
      {
        foreach (vTabPage tabPage in this.TabPages)
        {
          if (tabPage != null)
          {
            if (tabPage.TabContext != null)
            {
              ++index;
            }
            else
            {
              if (!tabPage.Invisible || this.DesignMode)
              {
                if (tabPage != page)
                {
                  panelStartPos += (int) ((double) this.widthPages[index] * num2) + this.TabsSpacing;
                }
                else
                {
                  num1 = (int) ((double) this.widthPages[index] * num2);
                  break;
                }
              }
              ++index;
            }
          }
        }
      }
      else
      {
        foreach (vTabPage tabPage in this.TabPages)
        {
          if ((!tabPage.Invisible || this.DesignMode) && tabPage.TabContext == null)
            panelStartPos += (int) ((double) this.widthPages[this.TabPages.IndexOf(tabPage)] * num2) + this.TabsSpacing;
        }
        panelStartPos += this.ContextTabsOffset;
        int num3 = this.TabContexts.IndexOf(page.TabContext);
        foreach (vTabPage tabPage in this.TabPages)
        {
          if ((!tabPage.Invisible || this.DesignMode) && (tabPage.TabContext != null && this.TabContexts.IndexOf(tabPage.TabContext) < num3))
            panelStartPos += (int) ((double) this.widthPages[this.TabPages.IndexOf(tabPage)] * num2) + this.TabsSpacing;
        }
        foreach (vTabPage vTabPage in this.GetTabsWithSameContext(page))
        {
          if (vTabPage != page)
          {
            panelStartPos += (int) ((double) this.widthPages[this.TabPages.IndexOf(vTabPage)] * num2) + this.TabsSpacing;
          }
          else
          {
            num1 = (int) ((double) this.widthPages[this.TabPages.IndexOf(vTabPage)] * num2);
            break;
          }
        }
      }
      int pageHeight = this.GetPageHeight(g, page);
      Rectangle rectangle = Rectangle.Empty;
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          rectangle = new Rectangle(panelStartPos, this.titleHeight - pageHeight, num1, pageHeight);
          break;
        case vTabPageAlignment.Bottom:
          rectangle = new Rectangle(panelStartPos, this.Height - this.titleHeight, num1, pageHeight);
          break;
        case vTabPageAlignment.Left:
          rectangle = new Rectangle(this.titleHeight - pageHeight, panelStartPos, pageHeight, num1);
          break;
        case vTabPageAlignment.Right:
          rectangle = new Rectangle(this.Width - this.titleHeight - 1, panelStartPos, pageHeight, num1);
          break;
      }
      page.pageRectangle = rectangle;
      return rectangle;
    }

    private List<vTabPage> GetTabsWithSameContext(vTabPage page)
    {
      List<vTabPage> vTabPageList = new List<vTabPage>();
      foreach (vTabPage tabPage in this.TabPages)
      {
        if (tabPage.TabContext != null && tabPage.TabContext == page.TabContext)
          vTabPageList.Add(tabPage);
      }
      return vTabPageList;
    }

    private int GetPagesWidth(Graphics g)
    {
      int num = 0;
      int index = 0;
      this.CalculatePagesWidth(g);
      foreach (vTabPage tabPage in this.TabPages)
      {
        if (tabPage != null)
        {
          if (!tabPage.Invisible || this.DesignMode)
            num += this.widthPages[index];
          ++index;
        }
      }
      return num;
    }

    private int GetPageWidth(Graphics g, vTabPage page)
    {
      if (page == null)
        return -1;
      if (page.HeaderWidth.HasValue)
        return page.HeaderWidth.Value;
      int num1 = 0;
      if (this.ImageList != null && this.ImageList.Images.Count > page.ImageIndex && page.ImageIndex >= 0)
        num1 = this.ImageList.ImageSize.Width;
      if (page.Image != null)
        num1 = page.Image.Width;
      int num2 = -1;
      Font font = page.Font;
      if (!page.UseDefaultTextFont)
        font = page.TextFont;
      switch (this.textOrientation)
      {
        case vTabPageTextOrientation.Vertical:
          num2 = (int) g.MeasureString(page.Text, font, 1000, this.sfTabs).Height + 10 + num1 + page.Padding.Horizontal;
          break;
        case vTabPageTextOrientation.Horizontal:
          num2 = (int) g.MeasureString(page.Text, font, 1000, this.sfTabs).Width + 10 + num1 + page.Padding.Horizontal;
          break;
      }
      if (this.FitTabsToBounds)
      {
        if (this.TabPages.Count > 0)
        {
          int num3 = this.Width;
          if (this.TabAlignment == vTabPageAlignment.Left || this.TabAlignment == vTabPageAlignment.Right)
            num3 = this.Height;
          int widthOfAllTabs = this.GetWidthOfAllTabs();
          if (widthOfAllTabs > num3)
          {
            int num4 = widthOfAllTabs - num3;
            int pageWidthRank = this.GetPageWidthRank(page);
            int sum = this.GetSum();
            int num5 = num3 - this.TabsInitialOffset;
            Math.Abs(this.TabsSpacing);
            int count = this.TabPages.Count;
            int tabsInitialOffset = this.TabsInitialOffset;
            int num6 = num2 - (pageWidthRank + 1) * num4 / sum;
            int num7 = 1;
            foreach (vTabPage tabPage in this.TabPages)
            {
              num7 = this.GetWantedPageWidth(g, tabPage) - (this.GetPageWidthRank(tabPage) + 1) * num4 / sum;
              if (num7 <= 0)
                break;
            }
            if (num7 <= 0)
              num6 = num5 / this.TabPages.Count - this.TabsSpacing;
            if (this.TabsShape == TabsShape.Chrome || this.TabsShape == TabsShape.OneNote || this.TabsShape == TabsShape.VisualStudio)
              num6 += 20;
            if (this.EnableCloseButtons && page.EnableCloseButton)
              num6 += 15;
            return num6;
          }
        }
      }
      if (this.TabsShape == TabsShape.Chrome || this.TabsShape == TabsShape.OneNote || this.TabsShape == TabsShape.VisualStudio)
        num2 += 20;
      if (this.EnableCloseButtons && page.EnableCloseButton)
        num2 += 15;
      return num2;
    }

    private int GetWantedPageWidth(Graphics g, vTabPage page)
    {
      if (page == null)
        return -1;
      int num1 = 0;
      if (this.ImageList != null && this.ImageList.Images.Count > page.ImageIndex && page.ImageIndex >= 0)
        num1 = this.ImageList.ImageSize.Width;
      if (page.Image != null)
        num1 = page.Image.Width;
      int num2 = -1;
      Font font = page.Font;
      if (!page.UseDefaultTextFont)
        font = page.TextFont;
      switch (this.textOrientation)
      {
        case vTabPageTextOrientation.Vertical:
          num2 = (int) g.MeasureString(page.Text, font, 1000, this.sfTabs).Height + 10 + num1 + page.Padding.Horizontal;
          break;
        case vTabPageTextOrientation.Horizontal:
          num2 = (int) g.MeasureString(page.Text, font, 1000, this.sfTabs).Width + 10 + num1 + page.Padding.Horizontal;
          break;
      }
      return num2;
    }

    private int GetSum()
    {
      int num = 0;
      for (int index = 1; index <= this.TabPages.Count; ++index)
        num += index;
      return num;
    }

    private int GetPageWidthRank(vTabPage currentPage)
    {
      int num1 = 0;
      List<int> intList = new List<int>();
      using (Graphics graphics = this.CreateGraphics())
      {
        foreach (vTabPage tabPage in this.TabPages)
        {
          if (tabPage == null)
            return -1;
          int num2 = 0;
          if (this.ImageList != null && this.ImageList.Images.Count > tabPage.ImageIndex && tabPage.ImageIndex >= 0)
            num2 = this.ImageList.ImageSize.Width;
          if (tabPage.Image != null)
            num2 = tabPage.Image.Width;
          int num3 = -1;
          Font font = tabPage.Font;
          if (!tabPage.UseDefaultTextFont)
            font = tabPage.TextFont;
          switch (this.textOrientation)
          {
            case vTabPageTextOrientation.Vertical:
              num3 = (int) graphics.MeasureString(tabPage.Text, font, 1000, this.sfTabs).Height + 10 + num2 + tabPage.Padding.Horizontal;
              break;
            case vTabPageTextOrientation.Horizontal:
              num3 = (int) graphics.MeasureString(tabPage.Text, font, 1000, this.sfTabs).Width + 10 + num2 + tabPage.Padding.Horizontal;
              break;
          }
          intList.Add(num3);
          if (tabPage == currentPage)
            num1 = num3;
        }
      }
      int num4 = 0;
      this.GetWidthOfAllTabs();
      for (int index = 0; index < this.TabPages.Count; ++index)
      {
        if (intList[index] == num1)
        {
          if (this.TabPages[index] != currentPage)
            ++num4;
          else
            break;
        }
      }
      intList.Sort();
      return intList.IndexOf(num1) + num4;
    }

    private int GetPageHeightRank(vTabPage currentPage)
    {
      int num1 = 0;
      List<int> intList = new List<int>();
      using (Graphics graphics = this.CreateGraphics())
      {
        foreach (vTabPage tabPage in this.TabPages)
        {
          if (tabPage == null)
            return -1;
          int num2 = 0;
          if (this.ImageList != null && this.ImageList.Images.Count > tabPage.ImageIndex)
            num2 = this.ImageList.ImageSize.Width;
          if (tabPage.Image != null)
            num2 = tabPage.Image.Width;
          if (this.allPagesHeight > 0)
            return this.allPagesHeight;
          int num3 = -1;
          Font font = tabPage.Font;
          if (!tabPage.UseDefaultTextFont)
            font = tabPage.TextFont;
          switch (this.textOrientation)
          {
            case vTabPageTextOrientation.Vertical:
              num3 = Math.Max(num2 + tabPage.Padding.Vertical, (int) graphics.MeasureString(tabPage.Text, font, 1000, this.sfTabs).Width + 10 + tabPage.Padding.Vertical);
              break;
            case vTabPageTextOrientation.Horizontal:
              num3 = Math.Max(num2 + tabPage.Padding.Vertical, (int) graphics.MeasureString(tabPage.Text, font, 1000, this.sfTabs).Height + 10 + tabPage.Padding.Vertical);
              break;
          }
          intList.Add(num3);
          if (tabPage == currentPage)
            num1 = num3;
        }
      }
      int num4 = 0;
      for (int index = 0; index < this.TabPages.Count; ++index)
      {
        if (intList[index] == num1)
        {
          if (this.TabPages[index] != currentPage)
            ++num4;
          else
            break;
        }
      }
      intList.Sort();
      return intList.IndexOf(num1) + num4;
    }

    private int GetHeightOfAllTabs()
    {
      int tabsInitialOffset = this.TabsInitialOffset;
      using (Graphics graphics = this.CreateGraphics())
      {
        foreach (vTabPage tabPage in this.TabPages)
        {
          if (tabPage == null)
            return -1;
          int num1 = 0;
          if (this.ImageList != null && this.ImageList.Images.Count > tabPage.ImageIndex)
            num1 = this.ImageList.ImageSize.Width;
          if (tabPage.Image != null)
            num1 = tabPage.Image.Width;
          if (this.allPagesHeight > 0)
            return this.allPagesHeight;
          int num2 = -1;
          Font font = tabPage.Font;
          if (!tabPage.UseDefaultTextFont)
            font = tabPage.TextFont;
          switch (this.textOrientation)
          {
            case vTabPageTextOrientation.Vertical:
              num2 = Math.Max(num1 + tabPage.Padding.Vertical, (int) graphics.MeasureString(tabPage.Text, font, 1000, this.sfTabs).Width + 10 + tabPage.Padding.Vertical);
              break;
            case vTabPageTextOrientation.Horizontal:
              num2 = Math.Max(num1 + tabPage.Padding.Vertical, (int) graphics.MeasureString(tabPage.Text, font, 1000, this.sfTabs).Height + 10 + tabPage.Padding.Vertical);
              break;
          }
          tabsInitialOffset += num2 + this.TabsSpacing;
        }
      }
      return tabsInitialOffset;
    }

    private int GetWidthOfAllTabs()
    {
      int tabsInitialOffset = this.TabsInitialOffset;
      using (Graphics graphics = this.CreateGraphics())
      {
        foreach (vTabPage tabPage in this.TabPages)
        {
          if (tabPage == null)
            return -1;
          int num1 = 0;
          if (this.ImageList != null && this.ImageList.Images.Count > tabPage.ImageIndex && tabPage.ImageIndex >= 0)
            num1 = this.ImageList.ImageSize.Width;
          if (tabPage.Image != null)
            num1 = tabPage.Image.Width;
          int num2 = -1;
          Font font = tabPage.Font;
          if (!tabPage.UseDefaultTextFont)
            font = tabPage.TextFont;
          switch (this.textOrientation)
          {
            case vTabPageTextOrientation.Vertical:
              num2 = (int) graphics.MeasureString(tabPage.Text, font, 1000, this.sfTabs).Height + 10 + num1 + tabPage.Padding.Horizontal;
              break;
            case vTabPageTextOrientation.Horizontal:
              num2 = (int) graphics.MeasureString(tabPage.Text, font, 1000, this.sfTabs).Width + 10 + num1 + tabPage.Padding.Horizontal;
              break;
          }
          tabsInitialOffset += num2 + this.TabsSpacing;
        }
      }
      return tabsInitialOffset + 2 * this.TabsSpacing;
    }

    private int GetPageHeight(Graphics g, vTabPage page)
    {
      if (page == null)
        return -1;
      if (page.HeaderHeight.HasValue)
        return page.HeaderHeight.Value;
      int num1 = 0;
      if (this.ImageList != null && this.ImageList.Images.Count > page.ImageIndex)
        num1 = this.ImageList.ImageSize.Width;
      if (page.Image != null)
        num1 = page.Image.Width;
      if (this.allPagesHeight > 0)
        return this.allPagesHeight;
      int num2 = -1;
      Font font = page.Font;
      if (!page.UseDefaultTextFont)
        font = page.TextFont;
      switch (this.textOrientation)
      {
        case vTabPageTextOrientation.Vertical:
          num2 = Math.Max(num1 + page.Padding.Vertical, (int) g.MeasureString(page.Text, font, 1000, this.sfTabs).Width + 10 + page.Padding.Vertical);
          break;
        case vTabPageTextOrientation.Horizontal:
          num2 = Math.Max(num1 + page.Padding.Vertical, (int) g.MeasureString(page.Text, font, 1000, this.sfTabs).Height + 10 + page.Padding.Vertical);
          break;
      }
      return num2;
    }

    private int GetWantedPageHeight(Graphics g, vTabPage page)
    {
      if (page == null)
        return -1;
      int num = 0;
      if (this.ImageList != null && this.ImageList.Images.Count > page.ImageIndex)
        num = this.ImageList.ImageSize.Width;
      if (page.Image != null)
        num = page.Image.Width;
      if (this.allPagesHeight > 0)
        return this.allPagesHeight;
      Font font = page.Font;
      if (!page.UseDefaultTextFont)
        font = page.TextFont;
      switch (this.textOrientation)
      {
        case vTabPageTextOrientation.Vertical:
          return Math.Max(num + page.Padding.Vertical, (int) g.MeasureString(page.Text, font, 1000, this.sfTabs).Width + 10 + page.Padding.Vertical);
        case vTabPageTextOrientation.Horizontal:
          return Math.Max(num + page.Padding.Vertical, (int) g.MeasureString(page.Text, font, 1000, this.sfTabs).Height + 10 + page.Padding.Vertical);
        default:
          return -1;
      }
    }

    private int GetPanelStartPos()
    {
      return this.TabsInitialOffset + this.scrollOffset;
    }

    /// <summary>
    /// Performs a hit test and returns the tab page at a specific point.
    /// </summary>
    /// <param name="point">Point where to perform the hit test.</param>
    /// <returns>A vTabPage located at the hit test point. If there is no tab page at this location the method returns a null.</returns>
    public vTabPage HitTest(Point point)
    {
      if (this.IsDisposed)
        return (vTabPage) null;
      if (this.hScrollBar.Visible && this.hScrollBar.RectangleToScreen(this.hScrollBar.ClientRectangle).Contains(Cursor.Position))
        return (vTabPage) null;
      if (this.vScrollBar.Visible && this.vScrollBar.RectangleToScreen(this.vScrollBar.ClientRectangle).Contains(Cursor.Position))
        return (vTabPage) null;
      using (this.CreateGraphics())
      {
        foreach (vTabPage tabPage in this.TabPages)
        {
          if (tabPage != null && (!tabPage.Invisible || this.DesignMode) && tabPage.pageRectangle.Contains(point))
            return tabPage;
        }
      }
      return (vTabPage) null;
    }

    private void InvokeSelectedIndexChanged(object sender, EventArgs e)
    {
      try
      {
        this.OnSelectedIndexChanged(EventArgs.Empty);
      }
      catch
      {
      }
    }

    private void InvokeTitleClick(object sender, EventArgs e)
    {
      try
      {
        this.OnTitleClick(EventArgs.Empty);
        if (this.TitleClick == null)
          return;
        this.TitleClick((object) null, EventArgs.Empty);
      }
      catch
      {
      }
    }

    /// <exclude />
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.DesignMode)
        return;
      vTabPage vTabPage = this.HitTest(e.Location);
      if (vTabPage != null)
        this.OnTabMouseUp(new vTabMouseEventArgs(vTabPage, e));
      if (this.AllowDragDrop && this.draggingTab != null && vTabControl.DropTabControlTargets.Count > 0)
      {
        for (int index1 = 0; index1 < vTabControl.DropTabControlTargets.Count; ++index1)
        {
          Point client = vTabControl.DropTabControlTargets[index1].PointToClient(Cursor.Position);
          vTabPage targetPage = vTabControl.DropTabControlTargets[index1].HitTest(client);
          if (targetPage != null)
          {
            vTabControl l_vTabControl = vTabControl.DropTabControlTargets[index1];
            DragCancelEventArgs e1 = new DragCancelEventArgs(this.draggingTab, targetPage);
            this.OnTabPageDragEnding(e1);
            if (e1.Cancel)
              return;
            int index2 = l_vTabControl.TabPages.IndexOf(targetPage);
            int index3 = this.TabPages.IndexOf(this.draggingTab);
            this.TabPages.Remove(this.draggingTab);
            l_vTabControl.TabPages.Insert(index2, this.draggingTab);
            l_vTabControl.SelectedTab = this.draggingTab;
            if (this.SelectedTab == null)
            {
              for (; index3 >= 0; --index3)
              {
                if (index3 < this.TabPages.Count)
                {
                  this.SelectedTab = this.TabPages[index3];
                  break;
                }
              }
            }
            this.Refresh();
            l_vTabControl.Refresh();
            this.OnTabPageDragEnded(new vTabEventArgs(vTabPage));
            this.draggingTab = (vTabPage) null;
            break;
          }
        }
      }
      else if (vTabPage != null && this.draggingTab != null)
      {
        DragCancelEventArgs e1 = new DragCancelEventArgs(this.draggingTab, vTabPage);
        this.OnTabPageDragEnding(e1);
        if (e1.Cancel)
          return;
        int index = this.TabPages.IndexOf(vTabPage);
        this.TabPages.Remove(this.draggingTab);
        this.TabPages.Insert(index, this.draggingTab);
        this.SelectedTab = this.draggingTab;
        this.Refresh();
        this.OnTabPageDragEnded(new vTabEventArgs(vTabPage));
        this.draggingTab = (vTabPage) null;
      }
      this.Capture = false;
      if (this.dragForm != null)
        this.dragForm.Hide();
      this.dragging = false;
    }

    internal void InvokeMouseDown(MouseEventArgs e)
    {
      this.OnMouseDown(e);
    }

    internal void InvokeMouseMove(MouseEventArgs e)
    {
      this.OnMouseMove(e);
    }

    internal void InvokeMouseUp(MouseEventArgs e)
    {
      this.OnMouseUp(e);
    }

    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      base.OnMouseDoubleClick(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      if (this.EnableDropDownStyle)
      {
        this.CloseDropDown();
        this.toggle = !this.toggle;
      }
      this.Focus();
      this.lastSelectedTab = this.SelectedTab;
      this.initialMousePosition = e.Location;
      this.Capture = true;
      Point point = new Point(e.X, e.Y);
      using (Graphics graphics = this.CreateGraphics())
        this.CalculatePagesWidth(graphics);
      vTabPage page = this.HitTest(point);
      if (page != null)
        this.OnTabMouseDown(new vTabMouseEventArgs(page, e));
      if (e.Button == MouseButtons.Left && page != null && page.Enabled)
      {
        if (this.EnableCloseButtons && page.EnableCloseButton && this.closeButtonPage != null)
        {
          this.DeleteTabPage(page);
          return;
        }
        this.SelectedTab = page;
        this.InvokeTitleClick((object) this, (EventArgs) e);
      }
      if (e.Button == MouseButtons.Middle && this.AllowDeleteOnMiddleMouseButton)
        this.DeleteSelectedItem();
      this.HandleDropDown();
      this.Refresh();
    }

    /// <summary>Opens the TabControl's popup.</summary>
    public void OpenDropDown()
    {
      this.EnableDropDownStyle = true;
      this.HandleDropDown();
    }

    /// <summary>Closes the TabControl's DropDown.</summary>
    public void CloseDropDown()
    {
      if (this.dropDown == null)
        return;
      this.dropDown.Close();
    }

    private void HandleDropDown()
    {
      if (!this.EnableDropDownStyle || this.DesignMode || this.SelectedTab != null && !this.RectangleToScreen(this.SelectedTab.pageRectangle).Contains(Cursor.Position))
        return;
      if (this.toggle || this.lastSelectedTab != this.SelectedTab)
      {
        if (this.lastSelectedTab != this.SelectedTab)
          this.toggle = !this.toggle;
        if ((this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom) && this.Height > this.TitleHeight)
        {
          this.dropDownSize = new Size(this.Width, this.Height - this.TitleHeight);
          this.Height = this.TitleHeight;
        }
        if (this.dropDown.Items.Count > 0)
        {
          Panel panel = (this.dropDown.Items[0] as ToolStripControlHost).Control as Panel;
          if (this.lastSelectedTab != null)
          {
            while (panel.Controls.Count > 0 && panel.Controls.Count != 0)
              panel.Controls[0].Parent = (Control) this.lastSelectedTab;
          }
        }
        this.dropDown.Items.Clear();
        Panel panel1 = new Panel();
        panel1.Paint += new PaintEventHandler(this.hostPanel_Paint);
        panel1.Dock = DockStyle.Fill;
        panel1.BackColor = this.SelectedTab.ContentBackColor;
        panel1.Padding = new Padding(3, 3, 3, 3);
        int count = this.SelectedTab.Controls.Count;
        while (this.SelectedTab.Controls.Count > 0 && this.SelectedTab.Controls.Count != 0)
          this.SelectedTab.Controls[0].Parent = (Control) panel1;
        Form form = this.FindForm();
        if (form != null)
        {
          form.Deactivate -= new EventHandler(this.form_Deactivate);
          form.Deactivate += new EventHandler(this.form_Deactivate);
          form.LocationChanged -= new EventHandler(this.form_LocationChanged);
          form.LocationChanged += new EventHandler(this.form_LocationChanged);
          form.MouseDown -= new MouseEventHandler(this.form_MouseDown);
          form.MouseDown += new MouseEventHandler(this.form_MouseDown);
          if (form.ParentForm != null)
          {
            form.ParentForm.Deactivate -= new EventHandler(this.form_Deactivate);
            form.ParentForm.Deactivate += new EventHandler(this.form_Deactivate);
            form.ParentForm.LocationChanged -= new EventHandler(this.form_LocationChanged);
            form.ParentForm.LocationChanged += new EventHandler(this.form_LocationChanged);
            form.ParentForm.MouseDown -= new MouseEventHandler(this.form_MouseDown);
            form.ParentForm.MouseDown += new MouseEventHandler(this.form_MouseDown);
          }
        }
        ToolStripControlHost stripControlHost = new ToolStripControlHost((Control) panel1);
        stripControlHost.Margin = Padding.Empty;
        this.dropDown.Items.Add((ToolStripItem) stripControlHost);
        this.dropDown.MinimumSize = this.dropDownSize;
        stripControlHost.Dock = DockStyle.Fill;
        panel1.MinimumSize = new Size(this.dropDownSize.Width + 1, this.dropDownSize.Height + 1);
        panel1.MaximumSize = new Size(this.dropDownSize.Width + 1, this.dropDownSize.Height + 1);
        stripControlHost.Padding = Padding.Empty;
        this.dropDown.Padding = Padding.Empty;
        this.dropDown.Margin = Padding.Empty;
        panel1.BorderStyle = BorderStyle.None;
        panel1.BackColor = this.SelectedTab.ContentBackColor;
        using (Graphics graphics = this.CreateGraphics())
        {
          Rectangle pageRectangle = this.GetPageRectangle(graphics, this.SelectedTab);
          switch (this.TabAlignment)
          {
            case vTabPageAlignment.Top:
              this.dropDown.Show((Control) this, 0, pageRectangle.Bottom);
              break;
            case vTabPageAlignment.Bottom:
              this.dropDown.Show((Control) this, 0, -this.dropDownSize.Height - 1);
              break;
            case vTabPageAlignment.Left:
              this.dropDown.Show((Control) this, this.TitleHeight, 0);
              break;
            case vTabPageAlignment.Right:
              this.dropDown.Show((Control) this, -this.dropDownSize.Width - this.TitleHeight, 0);
              break;
          }
        }
      }
      this.dropDownPosition = this.dropDown.Bounds.Location;
    }

    private void form_MouseDown(object sender, MouseEventArgs e)
    {
      this.CloseDropDown();
      this.toggle = !this.toggle;
    }

    private void form_LocationChanged(object sender, EventArgs e)
    {
      this.CloseDropDown();
      this.toggle = !this.toggle;
    }

    private void form_Deactivate(object sender, EventArgs e)
    {
      if (this.dropDown.Bounds.Contains(Cursor.Position) || this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.CloseDropDown();
      this.toggle = !this.toggle;
    }

    private void hostPanel_Paint(object sender, PaintEventArgs e)
    {
      this.OnPopupPaint(e);
    }

    private void dropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
      foreach (Control control in (ArrangedElementCollection) ((ToolStripControlHost) this.dropDown.Items[0]).Control.Controls)
        control.Parent = (Control) this.SelectedTab;
    }

    private void InitializePageRectangles()
    {
      using (Graphics graphics = this.CreateGraphics())
      {
        foreach (vTabPage tabPage in this.TabPages)
        {
          if (tabPage != null && (!tabPage.Invisible || this.DesignMode))
            tabPage.pageRectangle = this.GetPageRectangle(graphics, tabPage);
        }
      }
    }

    /// <exclude />
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.Invalidate();
    }

    private Bitmap GetItemBitmap(vTabPage page, Bitmap image)
    {
      if (page == null)
        return (Bitmap) null;
      using (Graphics g = Graphics.FromImage((Image) image))
      {
        BackgroundElement backgroundElement = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        backgroundElement.LoadTheme(this.Theme);
        backgroundElement.Bounds = new Rectangle(0, 0, image.Width, image.Height);
        backgroundElement.DrawElementFill(g, ControlState.Normal);
        backgroundElement.Bounds = new Rectangle(0, 0, image.Width - 1, image.Height - 1);
        backgroundElement.DrawElementBorder(g, ControlState.Normal);
        g.DrawString(page.Text, page.Font, Brushes.Black, (RectangleF) backgroundElement.Bounds, this.sfTabs);
      }
      return image;
    }

    private void scrollingTimer_Tick(object sender, EventArgs e)
    {
      if (this.dragging)
      {
        Rectangle screen = this.RectangleToScreen(this.ClientRectangle);
        Rectangle rectangle1 = new Rectangle(screen.Right - 20, screen.Y, 20, screen.Height);
        Rectangle rectangle2 = new Rectangle(screen.Right - 40, screen.Y, 20, screen.Height);
        Rectangle rectangle3 = new Rectangle(screen.X, screen.Bottom - 40, screen.Width, 20);
        Rectangle rectangle4 = new Rectangle(screen.X, screen.Bottom - 20, screen.Width, 20);
        if (this.TabAlignment == vTabPageAlignment.Top || this.TabAlignment == vTabPageAlignment.Bottom)
        {
          if (!this.hScrollBar.Visible)
            return;
          if (rectangle1.Contains(this.dragForm.Location))
            ++this.hScrollBar.Value;
          if (!rectangle2.Contains(this.dragForm.Location))
            return;
          --this.hScrollBar.Value;
        }
        else
        {
          if (!this.vScrollBar.Visible)
            return;
          if (rectangle3.Contains(this.dragForm.Location))
            --this.vScrollBar.Value;
          if (!rectangle4.Contains(this.dragForm.Location))
            return;
          ++this.vScrollBar.Value;
        }
      }
      else
        this.scrollingTimer.Stop();
    }

    /// <exclude />
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.Capture && this.dragForm != null && (this.dragForm.Visible && !this.DesignMode))
      {
        this.dragForm.Location = Cursor.Position;
        if (!this.dragForm.TopMost)
          this.dragForm.TopMost = true;
      }
      if (!this.dragging && this.Capture && (this.ShouldDrag(e.Location) && this.AllowDragDrop) && (!this.DesignMode && this.TabPages.Count > 1))
      {
        this.dragging = true;
        using (Graphics graphics = this.CreateGraphics())
        {
          Size size = Size.Empty;
          vTabPage vTabPage = (vTabPage) null;
          for (int index = 0; index < this.Controls.Count; ++index)
          {
            vTabPage page = this.Controls[index] as vTabPage;
            if (page != null)
            {
              Rectangle pageRectangle = this.GetPageRectangle(graphics, page);
              if (pageRectangle.Contains(e.Location))
              {
                size = pageRectangle.Size;
                vTabPage = page;
              }
            }
          }
          DragCancelEventArgs e1 = new DragCancelEventArgs(vTabPage, (vTabPage) null);
          this.OnTabPageDragStarting(e1);
          if (e1.Cancel)
            return;
          Bitmap image = new Bitmap(size.Width + 5, size.Height + 5);
          Bitmap itemBitmap = this.GetItemBitmap(vTabPage, image);
          this.draggingTab = vTabPage;
          if (itemBitmap != null)
          {
            if (this.dragForm == null)
              this.dragForm = this.CreateDragDropForm(itemBitmap);
            this.dragForm.BackgroundImage = (Image) itemBitmap;
            this.dragForm.Location = Cursor.Position;
            this.dragForm.Show();
            this.dragForm.Size = itemBitmap.Size;
            this.scrollingTimer.Start();
          }
          this.OnTabPageDragStarted(new vTabEventArgs(this.draggingTab));
        }
      }
      if (this.GetType() == typeof (vRibbonBar) && !this.Capture && !new Rectangle(0, 0, this.Width, this.TitleHeight).Contains(e.Location))
        return;
      vTabPage vTabPage1 = (vTabPage) null;
      foreach (vTabPage tabPage in this.TabPages)
      {
        Rectangle rectangle = tabPage.pageRectangle;
        if (tabPage.pageRectangle.Contains(e.Location))
        {
          vTabPage1 = tabPage;
          break;
        }
      }
      this.Redraw(e);
      if (vTabPage1 != this.lastPage)
      {
        if (vTabPage1 != null && this.enableToolTips)
        {
          this.timerToolTip.Stop();
          this.timerToolTip.Start();
          this.tabStripToolTip.RemoveAll();
          this.showPoint = this.PointToClient(Cursor.Position);
        }
        if (vTabPage1 != null && vTabPage1 != this.lastPage)
        {
          if (this.GetType() == typeof (vRibbonBar))
            this.Invalidate(new Rectangle(0, 0, this.Width, this.TitleHeight));
          else
            this.Invalidate();
        }
        this.lastPage = vTabPage1;
        if (vTabPage1 == null && this.lastPageNullCounter == 0)
          this.Invalidate();
        else if (vTabPage1 != null)
          this.lastPageNullCounter = 0;
        if (vTabPage1 == null)
          ++this.lastPageNullCounter;
      }
      if (this.AllowAnimations || this.GetType() == typeof (vRibbonBar))
        return;
      this.Invalidate();
    }

    private void Redraw(MouseEventArgs e)
    {
      foreach (vTabPage tabPage in this.TabPages)
      {
        if (tabPage.pageRectangle.Contains(e.Location))
        {
          this.Invalidate();
          break;
        }
      }
    }

    internal Form CreateDragDropForm(Bitmap image)
    {
      Form form = new Form();
      try
      {
        form.Size = new Size(0, 0);
      }
      catch
      {
        form = new Form();
      }
      form.MinimizeBox = false;
      form.MaximizeBox = false;
      form.FormBorderStyle = FormBorderStyle.None;
      form.Opacity = 0.01;
      form.ShowInTaskbar = false;
      form.Text = "";
      form.Enabled = false;
      form.Visible = false;
      form.Shown += new EventHandler(this.dragDropForm_Shown);
      if (image != null)
        form.BackgroundImage = (Image) image;
      return form;
    }

    private void dragDropForm_Shown(object sender, EventArgs e)
    {
      if (this.dragForm == null)
        return;
      this.dragForm.Opacity = 0.7;
      this.dragForm.Shown -= new EventHandler(this.dragDropForm_Shown);
    }

    internal virtual bool ShouldDrag(Point mousePosition)
    {
      return Math.Abs(mousePosition.X - this.initialMousePosition.X) >= SystemInformation.DragSize.Width || Math.Abs(mousePosition.Y - this.initialMousePosition.Y) >= SystemInformation.DragSize.Height;
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs p)
    {
      base.OnPaint(p);
      if (this.Width == 0 || this.Height == 0)
        return;
      Graphics graphics = p.Graphics;
      if (!(this.Parent is OfficeRibbonForm) || this.DesignMode)
        this.DrawTitleBackGround(graphics);
      this.CalculatePagesWidth(graphics);
      int num = this.GetPageCount();
      if (this.DesignMode)
        num = this.Controls.Count;
      if (num <= 0)
        return;
      this.DrawTabs(graphics);
      this.DrawContexts(graphics);
    }

    protected List<vTabPage> GetPagesByContext(TabContext context)
    {
      List<vTabPage> vTabPageList = new List<vTabPage>();
      foreach (vTabPage tabPage in this.TabPages)
      {
        if (tabPage.TabContext == context)
          vTabPageList.Add(tabPage);
      }
      return vTabPageList;
    }

    protected virtual void DrawContexts(Graphics g)
    {
      if (this.TabAlignment != vTabPageAlignment.Top)
        return;
      foreach (TabContext tabContext in this.TabContexts)
      {
        if (tabContext.Visible)
        {
          int x = -1;
          int width = 0;
          int val1 = 0;
          List<vTabPage> pagesByContext = this.GetPagesByContext(tabContext);
          foreach (vTabPage page in pagesByContext)
          {
            Rectangle pageRectangle = this.GetPageRectangle(g, page);
            if (x == -1)
              x = pageRectangle.X;
            if (x > pageRectangle.X)
              x = pageRectangle.X;
            width += pageRectangle.Width + this.TabsSpacing;
            val1 = Math.Max(val1, pageRectangle.Height);
          }
          if (pagesByContext.Count > 0)
          {
            Rectangle rectangle = new Rectangle(x, 0, width, this.TitleHeight - val1);
            if (rectangle.Width > 0 && rectangle.Height > 0)
            {
              ControlTheme copy = this.theme.CreateCopy();
              copy.StyleNormal.FillStyle.Colors[0] = Color.FromArgb(0, tabContext.TabColor);
              copy.StyleNormal.FillStyle.Colors[1] = Color.FromArgb(20, tabContext.TabColor);
              if (copy.StyleNormal.FillStyle.ColorsNumber > 2)
                copy.StyleNormal.FillStyle.Colors[2] = Color.FromArgb(35, tabContext.TabColor);
              else
                copy.StyleNormal.FillStyle.Colors[1] = Color.FromArgb((int) byte.MaxValue, tabContext.TabColor);
              if (copy.StyleNormal.FillStyle.ColorsNumber > 3)
                copy.StyleNormal.FillStyle.Colors[3] = Color.FromArgb(155, tabContext.TabColor);
              BackgroundElement backgroundElement = new BackgroundElement(rectangle, (IScrollableControlBase) this);
              backgroundElement.LoadTheme(copy);
              backgroundElement.DrawStandardFill(g, ControlState.Normal, GradientStyles.Linear);
              using (Pen pen = new Pen(Color.FromArgb(15, tabContext.TabColor)))
              {
                g.DrawLine(pen, rectangle.Right, rectangle.Top, rectangle.Right, this.TitleHeight);
                g.DrawLine(pen, rectangle.Left, rectangle.Top, rectangle.Left, this.TitleHeight);
              }
              StringFormat format = new StringFormat();
              format.LineAlignment = StringAlignment.Center;
              format.Alignment = StringAlignment.Center;
              format.FormatFlags |= StringFormatFlags.NoWrap;
              format.Trimming = StringTrimming.EllipsisCharacter;
              using (SolidBrush solidBrush1 = new SolidBrush(this.ForeColor))
              {
                if (WindowsAPI.IsGlassEnabled)
                {
                  SmoothingMode smoothingMode = g.SmoothingMode;
                  g.SmoothingMode = SmoothingMode.HighQuality;
                  using (GraphicsPath path = new GraphicsPath())
                  {
                    if (rectangle.Width >= 0)
                    {
                      path.AddString(this.Text, this.Font.FontFamily, (int) this.Font.Style, this.Font.SizeInPoints + 3f, rectangle, format);
                      using (Pen pen = new Pen(Color.FromArgb(90, Color.White), 4f))
                        g.DrawPath(pen, path);
                      using (SolidBrush solidBrush2 = new SolidBrush(this.ForeColor))
                        g.FillPath((Brush) solidBrush2, path);
                    }
                  }
                  g.SmoothingMode = smoothingMode;
                }
                else
                  g.DrawString(tabContext.Text, this.Font, (Brush) solidBrush1, (RectangleF) rectangle, format);
              }
            }
          }
        }
      }
    }

    private void DrawTabs(Graphics g)
    {
      this.closeButtonPage = (vTabPage) null;
      for (int index = this.TabPages.Count - 1; index >= 0; --index)
      {
        vTabPage page = this.TabPages[index];
        if (page != null && (!page.Invisible || this.DesignMode))
        {
          if (page.UseTabControlTheme && page.VIBlendTheme != this.VIBlendTheme)
            page.VIBlendTheme = this.VIBlendTheme;
          if (this.SelectedTab != page)
            this.DrawTab(g, page);
        }
      }
      if (this.SelectedTab == null)
        return;
      this.DrawTab(g, this.SelectedTab);
    }

    private int GetPageCount()
    {
      int num = 0;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        vTabPage vTabPage = this.Controls[index] as vTabPage;
        if (vTabPage != null && !vTabPage.Invisible)
          ++num;
      }
      return num;
    }

    protected virtual void DrawTitleBackGround(Graphics g)
    {
      Rectangle bounds = Rectangle.Empty;
      int width = this.ClientRectangle.Width - 1;
      int height = this.ClientRectangle.Height - 1;
      byte roundedCornersBitmask = 15;
      switch (this.TabAlignment)
      {
        case vTabPageAlignment.Top:
          roundedCornersBitmask = (byte) 3;
          bounds = new Rectangle(0, 0, width, this.titleHeight);
          break;
        case vTabPageAlignment.Bottom:
          roundedCornersBitmask = (byte) 12;
          bounds = this.Height <= this.TitleHeight ? new Rectangle(0, height - this.titleHeight, width, this.titleHeight) : new Rectangle(0, this.titleHeight, width, height - this.titleHeight);
          break;
        case vTabPageAlignment.Left:
          roundedCornersBitmask = (byte) 5;
          bounds = new Rectangle(0, 0, this.titleHeight, height);
          break;
        case vTabPageAlignment.Right:
          roundedCornersBitmask = (byte) 10;
          bounds = new Rectangle(width - this.titleHeight, 0, this.titleHeight, height);
          break;
      }
      DrawTabControlTitleEventArgs e = new DrawTabControlTitleEventArgs(g, bounds);
      this.OnDrawTabControlTitle(e);
      if (e.Handled)
        return;
      this.backGround.Bounds = bounds;
      GraphicsPath partiallyRoundedPath = this.paintHelper.CreatePartiallyRoundedPath(bounds, this.backGround.Radius, roundedCornersBitmask);
      if (this.useTabsAreaBackColor)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.TabsAreaBackColor))
          g.FillPath((Brush) solidBrush, partiallyRoundedPath);
      }
      else
      {
        Brush brush = (this.Enabled ? this.backGround.Theme.StyleNormal : this.backGround.Theme.StyleDisabled).FillStyle.GetBrush(this.backGround.Bounds);
        g.FillPath(brush, partiallyRoundedPath);
        this.backGround.RoundedCornersBitmask = roundedCornersBitmask;
        this.backGround.Bounds = bounds;
        this.backGround.DrawElementFill(g, ControlState.None);
      }
      if (!this.UseTabsAreaBorderColor)
      {
        byte num = this.backGround.RoundedCornersBitmask;
        this.backGround.RoundedCornersBitmask = roundedCornersBitmask;
        this.backGround.DrawElementBorder(g, this.Enabled ? ControlState.Normal : ControlState.Disabled);
        this.backGround.RoundedCornersBitmask = num;
      }
      else
      {
        if ((int) this.TabsAreaBorderColor.A <= 0)
          return;
        byte num = this.backGround.RoundedCornersBitmask;
        this.backGround.RoundedCornersBitmask = roundedCornersBitmask;
        this.backGround.DrawElementBorder(g, this.Enabled ? ControlState.Normal : ControlState.Disabled, this.TabsAreaBorderColor);
        this.backGround.RoundedCornersBitmask = num;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:DrawTabControlTitle" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:VIBlend.WinForms.Controls.DrawTabControlTitleEventArgs" /> instance containing the event data.</param>
    protected virtual void OnDrawTabControlTitle(DrawTabControlTitleEventArgs e)
    {
      if (this.DrawTitleBackground == null)
        return;
      this.DrawTitleBackground((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:DrawTabPageBackground" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:VIBlend.WinForms.Controls.DrawTabPageEventArgs" /> instance containing the event data.</param>
    protected internal virtual void OnDrawTabPageBackground(DrawTabPageEventArgs e)
    {
      if (this.DrawTabPageBackground == null)
        return;
      this.DrawTabPageBackground((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:DrawTabPage" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:VIBlend.WinForms.Controls.DrawTabPageEventArgs" /> instance containing the event data.</param>
    protected virtual void OnDrawTabPage(DrawTabPageEventArgs e)
    {
      if (this.DrawTabPage == null)
        return;
      this.DrawTabPage((object) this, e);
    }

    /// <exclude />
    protected virtual void OnHoveredTabChanged(vTabEventArgs e)
    {
      if (this.HoveredTabChanged == null)
        return;
      this.HoveredTabChanged((object) this, e);
    }

    /// <exclude />
    protected virtual void OnSelectedIndexChanged(EventArgs e)
    {
      if (this.SelectedIndexChanged == null)
        return;
      this.SelectedIndexChanged((object) this, e);
    }

    /// <exclude />
    protected virtual void OnSelectedIndexChanging(vTabCancelEventArgs e)
    {
      if (this.SelectedIndexChanging == null)
        return;
      this.SelectedIndexChanging((object) this, e);
    }

    /// <exclude />
    protected virtual void OnTabPageDragStarted(vTabEventArgs e)
    {
      if (this.TabPageDragStarted == null)
        return;
      this.TabPageDragStarted((object) this, e);
    }

    /// <exclude />
    protected virtual void OnTabPageDragEnded(vTabEventArgs e)
    {
      if (this.TabPageDragEnded == null)
        return;
      this.TabPageDragEnded((object) this, e);
    }

    /// <exclude />
    protected virtual void OnTabPageDragStarting(DragCancelEventArgs e)
    {
      if (this.TabPageDragStarting == null)
        return;
      this.TabPageDragStarting((object) this, e);
    }

    /// <exclude />
    protected virtual void OnTabPageDragEnding(DragCancelEventArgs e)
    {
      if (this.TabPageDragEnding == null)
        return;
      this.TabPageDragEnding((object) this, e);
    }

    /// <exclude />
    protected override void OnSystemColorsChanged(EventArgs e)
    {
      base.OnSystemColorsChanged(e);
    }

    /// <exclude />
    protected virtual void OnTitleClick(EventArgs e)
    {
    }

    private void PageAdded(object sender, ControlEventArgs e)
    {
      this.initializedPagesWidth = false;
      if (e.Control is VIBlend.WinForms.Controls.vScrollBar || e.Control is vRibbonApplicationButton || e.Control is QuickAccessToolbar)
        return;
      e.Control.Dock = !(e.Control is vTabPage) ? DockStyle.Fill : (!(this is vRibbonBar) ? DockStyle.Fill : DockStyle.None);
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        vTabPage vTabPage = this.Controls[index] as vTabPage;
        if (vTabPage != null)
        {
          vTabPage.VIBlendTheme = this.VIBlendTheme;
          this.SelectedTab = vTabPage;
          break;
        }
      }
      this.Invalidate();
      this.CalculatePagesWidth(this.CreateGraphics());
      this.InitializeScrollBar();
      this.initializedPagesWidth = false;
    }

    private void PageRemoved(object sender, ControlEventArgs e)
    {
      if (e.Control is VIBlend.WinForms.Controls.vScrollBar || e.Control is vRibbonApplicationButton || e.Control is QuickAccessToolbar)
        return;
      this.initializedPagesWidth = false;
      vTabPage vTabPage = e.Control as vTabPage;
      if (vTabPage != null && vTabPage == this.SelectedTab && this.TabPages.Count > 0)
        this.SelectedTab = this.SelectedIndex <= 0 ? this.TabPages[0] : this.TabPages[this.SelectedIndex - 1];
      this.Invalidate();
      this.InitializeScrollBar();
      this.initializedPagesWidth = false;
    }

    /// <exclude />
    protected override bool ProcessMnemonic(char key)
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        vTabPage vTabPage = this.Controls[index] as vTabPage;
        if (vTabPage != null && Control.IsMnemonic(key, vTabPage.Text))
        {
          this.SelectedTab = vTabPage;
          return true;
        }
      }
      return false;
    }

    protected override void OnValidating(CancelEventArgs e)
    {
      base.OnValidating(e);
    }

    /// <exclude />
    public delegate void vTabCancelEventHandler(object sender, vTabCancelEventArgs e);

    /// <exclude />
    public delegate void DragEventHandler(object sender, vTabEventArgs e);

    /// <exclude />
    public delegate void DragCancelEventHandler(object sender, DragCancelEventArgs e);
  }
}
