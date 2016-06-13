// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vExplorerBar
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents a ExplorerBar control which allows the user to select from a list of container controls that can host other controls.
  /// </summary>
  [ToolboxItem(true)]
  [Description("Represents a ExplorerBar control which allows the user to select from a list of container controls that can host other controls.")]
  [Designer("VIBlend.WinForms.Controls.Design.vExplorerBarDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [DefaultEvent("ExpandedChanged")]
  [ToolboxBitmap(typeof (vExplorerBar), "ControlIcons.vExplorerBar.ico")]
  public class vExplorerBar : vVerticalLayoutPanel, IScrollableControlBase
  {
    private bool enableToggleAnimation = true;
    internal Timer timerToolTip = new Timer();
    internal ToolTip paneToolTip = new ToolTip();
    private bool enableToolTips = true;
    private int toolTipShowDelay = 1500;
    private int toolTipHideDelay = 5000;
    private ToolTipEventArgs toolTipEventArgs = new ToolTipEventArgs();
    private bool allowAnimations = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private ExplorerBarItemsCollection navPaneItems;
    private Point showPoint;
    private bool allowPressedItemState;
    private vExplorerBarItem prevItem;
    private vExplorerBarItem newItem;
    private bool startWaiting;
    private AnimationManager manager;
    private ImageList imageList;
    private ControlTheme theme;

    /// <summary>
    /// Gets or sets a value indicating whether tool tips are enabled.
    /// </summary>
    [Description("Gets or sets a value indicating whether tool tips are enabled.")]
    [DefaultValue(true)]
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
    [Description("Gets or sets whether the control is enabled.")]
    [Category("Behavior")]
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
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether toggle animation is enabled
    /// </summary>
    /// <value>
    /// 	<c>true</c> if true the animation is enabled; otherwise, <c>false</c>.
    /// </value>
    [Description("Gets or sets a value indicating whether  toggle animation is enabled")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool EnableToggleAnimation
    {
      get
      {
        return this.enableToggleAnimation;
      }
      set
      {
        this.enableToggleAnimation = value;
        this.Invalidate();
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
        this.paneToolTip.InitialDelay = this.toolTipShowDelay;
        this.paneToolTip.ReshowDelay = this.toolTipShowDelay;
        this.paneToolTip.AutomaticDelay = this.toolTipShowDelay;
        this.paneToolTip.AutoPopDelay = this.toolTipShowDelay;
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

    /// <exclude />
    [Browsable(false)]
    public new AnimationManager AnimationManager
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
    [DefaultValue(true)]
    [Browsable(false)]
    public new bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        this.allowAnimations = value;
        this.Invalidate();
      }
    }

    [DefaultValue(true)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool AllowPressedItemState
    {
      get
      {
        return true;
      }
      set
      {
        this.allowPressedItemState = value;
      }
    }

    /// <summary>
    /// Gets or sets the list of images to display on the control's items.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the list of images to display on the control's items.")]
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

    /// <summary>Gets or sets the theme of the control.</summary>
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the theme of the control.")]
    public new ControlTheme Theme
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
        this.Invalidate();
        foreach (vExplorerBarItem vExplorerBarItem in this.Items)
        {
          vExplorerBarItem.HeaderControl.Theme = this.theme;
          vExplorerBarItem.HeaderControl.backFill.IsAnimated = this.theme.Animated;
        }
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Description("Gets or sets the theme of the control using one of the built-in themes.")]
    [Category("Appearance")]
    public new VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        base.VIBlendTheme = value;
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
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>
    /// Gets the collection of panes in this navigationpane control.
    /// </summary>
    [Category("Data")]
    [MergableProperty(false)]
    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the collection of panes in this navigationpane control.")]
    [Editor("System.ComponentModel.Design.CollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    public ExplorerBarItemsCollection Items
    {
      get
      {
        return this.navPaneItems;
      }
    }

    [Category("Action")]
    public event EventHandler<DrawExplorerBarEventArgs> DrawItemHeader;

    /// <summary>Occurs when tooltip shows.</summary>
    [Category("Action")]
    public event ToolTipShownHandler TooltipShow;

    /// <summary>Occurs when an item is expanded.</summary>
    [Description("Occurs when an item is expanded.")]
    [Category("Action")]
    public event EventHandler<vExplorerBarEventArgs> ExpandedChanged;

    /// <summary>Occurs when an item is expanding.</summary>
    [Description("Occurs when an item is expanding.")]
    [Category("Action")]
    public event EventHandler<vExplorerBarCancelEventArgs> ExpandedChanging;

    static vExplorerBar()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>vExplorerBar constructor</summary>
    public vExplorerBar()
    {
      this.ControlAdded += new ControlEventHandler(this.NavPane_ControlAdded);
      this.ControlRemoved += new ControlEventHandler(this.NavPane_ControlRemoved);
      this.navPaneItems = new ExplorerBarItemsCollection();
      this.navPaneItems.CollectionChanged += new EventHandler(this.navPaneItems_CollectionChanged);
      this.Margin = new Padding(0, 0, 0, 0);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.paneToolTip.InitialDelay = this.toolTipShowDelay;
      this.paneToolTip.ReshowDelay = this.toolTipShowDelay;
      this.timerToolTip.Interval = this.ToolTipShowDelay;
      this.timerToolTip.Tick += new EventHandler(this.timerToolTip_Tick);
    }

    /// <summary>
    /// Raises the <see cref="E:DrawItemHeader" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.DrawExplorerBarEventArgs" /> instance containing the event data.</param>
    protected internal virtual void OnDrawItemHeader(DrawExplorerBarEventArgs args)
    {
      if (this.DrawItemHeader == null)
        return;
      this.DrawItemHeader((object) this, args);
    }

    public void OnExpandedChanged(vExplorerBarEventArgs args)
    {
      if (this.ExpandedChanged == null)
        return;
      this.ExpandedChanged((object) this, args);
    }

    public void OnExpandedChanging(vExplorerBarCancelEventArgs args)
    {
      if (this.ExpandedChanging == null)
        return;
      this.ExpandedChanging((object) this, args);
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

    /// <summary>
    /// HitTest to find whether on the point position there is nav pane item
    /// </summary>
    /// <param name="pt">The pt.</param>
    /// <returns></returns>
    public vExplorerBarItem HitTest()
    {
      foreach (vExplorerBarItem vExplorerBarItem in this.Items)
      {
        if (vExplorerBarItem.HeaderControl.Bounds.Contains(vExplorerBarItem.HeaderControl.PointToClient(Cursor.Position)))
          return vExplorerBarItem;
      }
      return (vExplorerBarItem) null;
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

    private void navPaneItems_CollectionChanged(object sender, EventArgs e)
    {
      this.DoCollectionChange();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      foreach (vExplorerBarItem vExplorerBarItem in this.Items)
      {
        vExplorerBarItem.Width = this.Width;
        if (!vExplorerBarItem.Expanded)
          vExplorerBarItem.Height = vExplorerBarItem.HeaderHeight;
      }
    }

    private void DoCollectionChange()
    {
      foreach (vExplorerBarItem vExplorerBarItem in this.Items)
      {
        if (!this.Controls.Contains((Control) vExplorerBarItem))
          this.Controls.Add((Control) vExplorerBarItem);
        vExplorerBarItem.Height = vExplorerBarItem.HeaderHeight;
        vExplorerBarItem.Expanded = false;
        vExplorerBarItem.Width = this.Width;
        if (vExplorerBarItem.HeaderControl != null && vExplorerBarItem.HeaderControl.VIBlendTheme != this.VIBlendTheme)
          vExplorerBarItem.HeaderControl.VIBlendTheme = this.VIBlendTheme;
      }
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (this.Items != null && !this.DesignMode)
      {
        foreach (vExplorerBarItem vExplorerBarItem in this.Items)
        {
          if (vExplorerBarItem.ItemPanel != null)
          {
            vExplorerBarItem.ItemPanel.Invalidate();
            foreach (Control control in (ArrangedElementCollection) vExplorerBarItem.ItemPanel.Controls)
              control.Invalidate();
          }
        }
      }
      base.OnLayout(levent);
    }

    private void item_SizeChanged(object sender, EventArgs e)
    {
    }

    /// <summary>Expands the item.</summary>
    /// <param name="index">The index.</param>
    public void ExpandItem(int index)
    {
      if (index < 0 || index >= this.Items.Count)
        return;
      this.Items[index].Expanded = true;
    }

    /// <summary>Collapses the item.</summary>
    /// <param name="index">The index.</param>
    public void CollapseItem(int index)
    {
      if (index < 0 || index >= this.Items.Count)
        return;
      this.Items[index].Expanded = false;
    }

    /// <summary>Selects a specific navigation pane item</summary>
    /// <param name="item">The Item to select</param>
    public void SelectItem(vExplorerBarItem item)
    {
      if (item == null)
        return;
      this.CallHeaderClick(item.HeaderControl);
      item.CallOnClick();
    }

    internal void CallHeaderClick(vExplorerBarItemHeader item)
    {
      this.HeaderControl_Click((object) item, EventArgs.Empty);
    }

    private void HeaderControl_Click(object sender, EventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
    }

    private void NavPane_ControlRemoved(object sender, ControlEventArgs e)
    {
      this.Refresh();
    }

    private void NavPane_ControlAdded(object sender, ControlEventArgs e)
    {
      this.Refresh();
    }

    /// <summary>Raises the Paint event.</summary>
    /// <param name="e">A PaintEventArgs that contains the event data. </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.DesignMode)
        return;
      Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      ControlPaint.DrawFocusRectangle(e.Graphics, rectangle);
    }
  }
}
