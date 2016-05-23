// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vNavPane
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents a NavigationPane control, which allows the user to select from a list of container controls that can host other controls.
  /// </summary>
  [ToolboxBitmap(typeof (vNavPane), "ControlIcons.vNavPane.ico")]
  [ToolboxItem(true)]
  [Description("Represents a NavigationPane control, which allows the user to select from a list of container controls that can host other controls.")]
  [DefaultEvent("ExpandedChanged")]
  [Designer("VIBlend.WinForms.Controls.Design.vNavPaneDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vNavPane : Panel, IScrollableControlBase
  {
    private bool animate = true;
    internal Timer timerToolTip = new Timer();
    internal ToolTip paneToolTip = new ToolTip();
    private bool enableToolTips = true;
    private int toolTipShowDelay = 1500;
    private int toolTipHideDelay = 5000;
    private ToolTipEventArgs toolTipEventArgs = new ToolTipEventArgs();
    private bool allowAnimations = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private int animationPower = 1;
    private int animationDelay = 10;
    private NavPaneItemsCollection navPaneItems;
    private Timer animationTimer;
    private bool isAnimating;
    private Point showPoint;
    private vNavPaneItem lastSelectedItem;
    private vNavPaneItem selectedItem;
    private double step;
    private int currentIndex;
    private int newIndex;
    private bool allowPressedItemState;
    private vNavPaneItem prevItem;
    private vNavPaneItem newItem;
    private bool startWaiting;
    private AnimationManager manager;
    private ImageList imageList;
    private ControlTheme theme;

    /// <summary>
    /// Gets or sets whether the expand/collapse animation is enabled.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets whether the expand/collapse animation is enabled.")]
    public bool IsExpandCollapseAnimationEnabled
    {
      get
      {
        return this.animate;
      }
      set
      {
        this.animate = value;
        if (this.animate)
          return;
        this.LayoutControl(false);
        this.animationTimer.Stop();
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
    /// Gets or sets a value indicating whether tool tips are enabled.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether tool tips are enabled.")]
    [DefaultValue(true)]
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
    [DefaultValue(1500)]
    [Description("Time delay in milliseconds before the tooltip is shown")]
    [Category("Behavior")]
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
    [Category("Behavior")]
    [Description("Tooltip duration in milliseconds ")]
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
    [DefaultValue(true)]
    [Browsable(false)]
    public bool AllowAnimations
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
    [Description("Gets or sets the list of images to display on the control's items.")]
    [DefaultValue(null)]
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

    /// <summary>Gets or sets the theme of the control.</summary>
    [Description("Gets or sets the theme of the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        this.Invalidate();
        foreach (vNavPaneItem vNavPaneItem in this.Items)
        {
          vNavPaneItem.HeaderControl.Theme = this.theme;
          vNavPaneItem.HeaderControl.backFill.IsAnimated = this.theme.Animated;
        }
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control using one of the built-in themes.")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
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
        foreach (vNavPaneItem vNavPaneItem in this.Items)
          vNavPaneItem.HeaderControl.VIBlendTheme = value;
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>Gets or sets the animation power.</summary>
    /// <value>The animation power.</value>
    [DefaultValue(1)]
    [Description("Gets or sets the animation power.")]
    [Category("Behavior")]
    public int AnimationPower
    {
      get
      {
        return this.animationPower;
      }
      set
      {
        if (value < 1 || value >= 100)
          return;
        this.animationPower = value;
      }
    }

    /// <summary>Gets or sets the animation Delay.</summary>
    /// <value>The animation Delay.</value>
    [Category("Behavior")]
    [DefaultValue(10)]
    [Description("Gets or sets the animation Delay.")]
    public int AnimationDelay
    {
      get
      {
        return this.animationDelay;
      }
      set
      {
        if (value < 1)
          return;
        this.animationDelay = value;
        if (this.animationTimer == null)
          return;
        this.animationTimer.Interval = value;
      }
    }

    /// <summary>
    /// Gets the collection of panes in this navigationpane control.
    /// </summary>
    [Editor("System.ComponentModel.Design.CollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof (UITypeEditor))]
    [MergableProperty(false)]
    [Category("Data")]
    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the collection of panes in this navigationpane control.")]
    public NavPaneItemsCollection Items
    {
      get
      {
        return this.navPaneItems;
      }
    }

    /// <summary>Occurs when the item's header is rendered.</summary>
    [Description("Occurs when the item's header is rendered.")]
    [Category("Action")]
    public event EventHandler<DrawNavPaneEventArgs> DrawItemHeader;

    /// <summary>Occurs when tooltip shows.</summary>
    [Category("Action")]
    public event ToolTipShownHandler TooltipShow;

    /// <summary>
    /// Occurs when a the expanded item changes inside the navigation pane
    /// </summary>
    [Category("Action")]
    [Description("Occurs when an item is expanded.")]
    public event EventHandler<vNavItemEventArgs> ExpandedChanged;

    /// <summary>
    /// Occurs when a the expanded item changes inside the navigation pane
    /// </summary>
    [Description("Occurs when an item is expanding.")]
    [Category("Action")]
    public event EventHandler<vNavItemCancelEventArgs> ExpandedChanging;

    static vNavPane()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vNavPane" /> class.
    /// </summary>
    public vNavPane()
    {
      this.ControlAdded += new ControlEventHandler(this.NavPane_ControlAdded);
      this.ControlRemoved += new ControlEventHandler(this.NavPane_ControlRemoved);
      this.navPaneItems = new NavPaneItemsCollection();
      this.navPaneItems.CollectionChanged += new EventHandler(this.navPaneItems_CollectionChanged);
      this.animationTimer = new Timer();
      this.animationTimer.Tick += new EventHandler(this.animationTimer_Tick);
      this.animationTimer.Interval = 10;
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.paneToolTip.InitialDelay = this.toolTipShowDelay;
      this.paneToolTip.ReshowDelay = this.toolTipShowDelay;
      this.timerToolTip.Interval = this.ToolTipShowDelay;
      this.timerToolTip.Tick += new EventHandler(this.timerToolTip_Tick);
    }

    private double GetCalculatedValue(Interpolation interpolation, double from, double to, double progress)
    {
      double alpha = interpolation.GetAlpha(progress);
      return from + alpha * (to - from);
    }

    /// <summary>
    /// Raises the <see cref="E:DrawItemHeader" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.DrawNavPaneEventArgs" /> instance containing the event data.</param>
    protected internal virtual void OnDrawItemHeader(DrawNavPaneEventArgs args)
    {
      if (this.DrawItemHeader == null)
        return;
      this.DrawItemHeader((object) this, args);
    }

    /// <summary>Called when expanded is changed.</summary>
    /// <param name="item">The item.</param>
    public void OnExpandedChanged(vNavPaneItem item)
    {
      if (this.ExpandedChanged == null)
        return;
      this.ExpandedChanged((object) this, new vNavItemEventArgs(item));
    }

    /// <summary>
    /// Raises the <see cref="E:ExpandedChanging" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.vNavItemCancelEventArgs" /> instance containing the event data.</param>
    public void OnExpandedChanging(vNavItemCancelEventArgs args)
    {
      if (this.ExpandedChanging == null)
        return;
      this.ExpandedChanging((object) this, args);
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      this.ControlAdded -= new ControlEventHandler(this.NavPane_ControlAdded);
      this.ControlRemoved -= new ControlEventHandler(this.NavPane_ControlRemoved);
      this.navPaneItems.CollectionChanged -= new EventHandler(this.navPaneItems_CollectionChanged);
      this.animationTimer.Tick -= new EventHandler(this.animationTimer_Tick);
      this.timerToolTip.Tick -= new EventHandler(this.timerToolTip_Tick);
      if (disposing)
      {
        this.enableToolTips = false;
        this.timerToolTip.Dispose();
        this.timerToolTip = (Timer) null;
        this.animationTimer.Dispose();
        this.animationTimer = (Timer) null;
      }
      base.Dispose(disposing);
    }

    /// <summary>
    /// HitTest to find whether on the point position there is nav pane item
    /// </summary>
    /// <param name="pt">The pt.</param>
    /// <returns></returns>
    public vNavPaneItem HitTest()
    {
      foreach (vNavPaneItem vNavPaneItem in this.Items)
      {
        if (vNavPaneItem.HeaderControl.Bounds.Contains(vNavPaneItem.HeaderControl.PointToClient(Cursor.Position)))
          return vNavPaneItem;
      }
      return (vNavPaneItem) null;
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
      if (!this.enableToolTips)
        return;
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

    /// <summary>Raises the Layout event.</summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.LayoutControl(false);
    }

    private void navPaneItems_CollectionChanged(object sender, EventArgs e)
    {
      this.DoCollectionChange();
    }

    /// <summary>Clears this instance.</summary>
    public void Clear()
    {
      foreach (vNavPaneItem vNavPaneItem in this.Items)
      {
        if (vNavPaneItem.HeaderControl != null)
          vNavPaneItem.HeaderControl.Click -= new EventHandler(this.HeaderControl_Click);
        vNavPaneItem.SizeChanged -= new EventHandler(this.item_SizeChanged);
        if (vNavPaneItem.Parent != null)
          vNavPaneItem.Parent.Controls.Remove((Control) vNavPaneItem);
        vNavPaneItem.Dispose();
      }
      this.Items.Clear();
    }

    private void DoCollectionChange()
    {
      foreach (vNavPaneItem vNavPaneItem in this.Items)
      {
        vNavPaneItem.HeaderControl.Click -= new EventHandler(this.HeaderControl_Click);
        vNavPaneItem.SizeChanged -= new EventHandler(this.item_SizeChanged);
        if (vNavPaneItem.Parent != null)
          vNavPaneItem.Parent.Controls.Remove((Control) vNavPaneItem);
      }
      this.Controls.Clear();
      int num = 0;
      foreach (vNavPaneItem vNavPaneItem in this.Items)
      {
        vNavPaneItem.IsExpanded = false;
        if (num == 0)
          vNavPaneItem.IsExpanded = true;
        ++num;
        this.Controls.Add((Control) vNavPaneItem);
        vNavPaneItem.HeaderControl.Click += new EventHandler(this.HeaderControl_Click);
        vNavPaneItem.SizeChanged += new EventHandler(this.item_SizeChanged);
        if (vNavPaneItem.HeaderControl.VIBlendTheme != this.VIBlendTheme)
          vNavPaneItem.HeaderControl.VIBlendTheme = this.VIBlendTheme;
      }
      this.LayoutControl(true);
      this.Refresh();
    }

    private void item_SizeChanged(object sender, EventArgs e)
    {
      this.LayoutControl(false);
    }

    /// <summary>Selects a specific navigation pane item</summary>
    /// <param name="item">The Item to select</param>
    public void SelectItem(vNavPaneItem item)
    {
      if (item == null)
        return;
      this.CallHeaderClick(item.HeaderControl);
    }

    /// <summary>Expands an item.</summary>
    /// <param name="index">The index.</param>
    public void SelectItem(int index)
    {
      if (index < 0 || index >= this.Items.Count)
        return;
      this.CallHeaderClick(this.Items[index].HeaderControl);
    }

    internal void CallHeaderClick(vNavPaneHeader item)
    {
      this.HeaderControl_Click((object) item, EventArgs.Empty);
    }

    private void HeaderControl_Click(object sender, EventArgs e)
    {
      vNavPaneHeader vNavPaneHeader = (vNavPaneHeader) sender;
      vNavItemCancelEventArgs args = new vNavItemCancelEventArgs(vNavPaneHeader.NavPaneItem);
      this.OnExpandedChanging(args);
      if (args.Cancel)
        return;
      if (this.isAnimating)
      {
        this.isAnimating = false;
        this.LayoutControl(false);
        this.animationTimer.Stop();
      }
      else
      {
        foreach (vNavPaneItem vNavPaneItem in this.Items)
        {
          if (vNavPaneItem.IsExpanded)
            this.lastSelectedItem = vNavPaneItem;
          vNavPaneItem.IsExpanded = false;
        }
        vNavPaneHeader.NavPaneItem.IsExpanded = true;
        this.OnExpandedChanged(vNavPaneHeader.NavPaneItem);
        this.selectedItem = vNavPaneHeader.NavPaneItem;
        if (this.selectedItem != null)
          this.selectedItem.CallOnClick();
        if (this.lastSelectedItem == this.selectedItem)
          return;
        if (!this.animate)
        {
          this.LayoutControl(false);
        }
        else
        {
          int num = 0;
          foreach (vNavPaneItem navPaneItem in this.navPaneItems)
          {
            if (navPaneItem.Visible)
            {
              navPaneItem.wantedY = num;
              navPaneItem.Width = this.Width;
              navPaneItem.ItemPanel.AutoScroll = false;
              if (!navPaneItem.IsExpanded)
              {
                num += navPaneItem.HeaderControl.Height;
                navPaneItem.wantedHeight = navPaneItem.HeaderControl.Height;
              }
              else
              {
                navPaneItem.wantedHeight = this.Height - navPaneItem.HeaderControl.Height * (this.navPaneItems.Count - 1);
                num += navPaneItem.Height;
              }
            }
          }
          this.animationTimer.Start();
          this.isAnimating = true;
          this.currentIndex = this.Items.IndexOf(this.lastSelectedItem);
          this.newIndex = this.Items.IndexOf(this.selectedItem);
        }
      }
    }

    private int GetExpandedAvailableHeight()
    {
      int num = 0;
      for (int index = 0; index < 1; ++index)
        num = this.Items[index].HeaderHeight;
      return this.Height - num;
    }

    private void animationTimer_Tick(object sender, EventArgs e)
    {
      if (this.lastSelectedItem == null)
      {
        this.animationTimer.Stop();
        this.isAnimating = false;
        this.LayoutControl(false);
        this.step = 0.0;
        this.Refresh();
      }
      else
      {
        if (this.currentIndex < this.newIndex)
        {
          this.step += 0.02;
          if (this.step >= 1.0)
            this.step = 1.0;
          int num = (int) this.GetCalculatedValue((Interpolation) new ExponentialInterpolation((double) this.animationPower, EdgeBehaviorEnum.EaseInOut), 0.0, (double) this.GetExpandedAvailableHeight(), this.step);
          this.lastSelectedItem.Height -= num;
          for (int index = this.currentIndex + 1; index < this.newIndex; ++index)
            this.Items[index].Location = new Point(0, this.Items[index].Location.Y - num);
          this.selectedItem.Bounds = new Rectangle(new Point(0, this.selectedItem.Location.Y - num), new Size(this.selectedItem.Width, this.selectedItem.Height + num));
          if (this.lastSelectedItem.Height < this.lastSelectedItem.HeaderControl.Height)
          {
            this.lastSelectedItem.Height = this.lastSelectedItem.HeaderControl.Height;
            this.animationTimer.Stop();
            this.isAnimating = false;
            this.LayoutControl(false);
            this.step = 0.0;
          }
        }
        else
        {
          this.step += 0.02;
          int num = (int) this.GetCalculatedValue((Interpolation) new ExponentialInterpolation((double) this.animationPower, EdgeBehaviorEnum.EaseInOut), 0.0, (double) this.GetExpandedAvailableHeight(), this.step);
          this.selectedItem.Height += num;
          if (this.selectedItem.Height >= this.selectedItem.wantedHeight)
          {
            this.lastSelectedItem.Height = this.lastSelectedItem.HeaderControl.Height;
            this.animationTimer.Stop();
            this.isAnimating = false;
            this.LayoutControl(false);
            this.step = 0.0;
          }
          this.lastSelectedItem.Bounds = new Rectangle(new Point(0, this.lastSelectedItem.Location.Y + num), new Size(this.lastSelectedItem.Width, this.lastSelectedItem.Height - num));
          for (int index = this.newIndex + 1; index < this.currentIndex; ++index)
            this.Items[index].Location = new Point(0, this.Items[index].Location.Y + num);
        }
        if (this.selectedItem.Height == this.selectedItem.wantedHeight)
        {
          this.animationTimer.Stop();
          this.isAnimating = false;
          this.LayoutControl(false);
          this.step = 0.0;
        }
        this.Refresh();
      }
    }

    private void NavPane_ControlRemoved(object sender, ControlEventArgs e)
    {
      this.LayoutControl(false);
      this.Refresh();
    }

    private void NavPane_ControlAdded(object sender, ControlEventArgs e)
    {
      this.LayoutControl(false);
      this.Refresh();
    }

    /// <summary>Layouts the control and its items.</summary>
    protected virtual void LayoutControl(bool fromLayout)
    {
      if (this.isAnimating)
        return;
      int y = 0;
      if (fromLayout)
      {
        foreach (vNavPaneItem navPaneItem in this.navPaneItems)
        {
          if (navPaneItem.Parent != this)
          {
            navPaneItem.Parent.Controls.Remove((Control) navPaneItem);
            this.Controls.Add((Control) navPaneItem);
          }
        }
      }
      foreach (vNavPaneItem navPaneItem in this.navPaneItems)
      {
        if (navPaneItem.Visible)
        {
          if (navPaneItem.ItemPanel == null || navPaneItem.HeaderControl == null)
            return;
          navPaneItem.Location = new Point(0, y);
          navPaneItem.Width = this.Width;
          if (navPaneItem.ItemPanel != null)
            navPaneItem.ItemPanel.AutoScroll = true;
          if (!navPaneItem.IsExpanded)
          {
            y += navPaneItem.HeaderControl.Height;
            navPaneItem.Height = navPaneItem.HeaderControl.Height;
          }
          else
          {
            navPaneItem.Height = this.Height - navPaneItem.HeaderControl.Height * (this.navPaneItems.Count - 1);
            y += navPaneItem.Height;
          }
          navPaneItem.HeaderControl.Invalidate();
        }
      }
      this.Invalidate();
    }

    /// <summary>Raises the Paint event.</summary>
    /// <param name="e">A PaintEventArgs that contains the event data. </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.DesignMode)
        return;
      ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
    }
  }
}
