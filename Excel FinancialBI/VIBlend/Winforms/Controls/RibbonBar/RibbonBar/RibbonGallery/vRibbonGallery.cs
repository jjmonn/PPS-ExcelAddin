// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRibbonBarGallery
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vRibbonBarGallery control</summary>
  [Description("Displays a ribbon bar gallery where the user can add other controls.")]
  [Designer("VIBlend.WinForms.Controls.Design.vRibbonGalleryDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vRibbonBarGallery), "ControlIcons.vRibbonBarGallery.ico")]
  [ToolboxItem(true)]
  public class vRibbonBarGallery : ScrollableControlMiniBase
  {
    private Color popupBackColor = Color.White;
    private PaintHelper helper = new PaintHelper();
    private List<RibbonBarGalleryGroup> groups = new List<RibbonBarGalleryGroup>();
    private List<vApplicationMenuItem> items = new List<vApplicationMenuItem>();
    private Size dropDownSize = Size.Empty;
    private Timer timer = new Timer();
    private Timer boundsTimer = new Timer();
    private string styleKey = "RibbonGallery";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private vRibbonDropDownButton dropDownButton;
    private vArrowButton upButton;
    private vArrowButton downButton;
    private vFlowLayoutPanel layoutPanel;
    private BackgroundElement backFill;
    private vDropDownBase dropDownBase;
    private vSizingControl ctrl;
    private GalleryButtonCollection galleryButtons;
    internal BackgroundElement panelFill;
    internal bool drawContentFill;
    private int updateCount;
    private ControlTheme theme;

    /// <summary>Gets or sets the size of the pop up.</summary>
    /// <value>The size of the pop up.</value>
    [Description("Gets or sets the size of the pop up.")]
    [Category("Behavior")]
    public Size DropDownSize
    {
      get
      {
        return this.dropDownSize;
      }
      set
      {
        this.dropDownSize = value;
      }
    }

    /// <summary>Gets or sets the color of the pop up.</summary>
    /// <value>The color of the pop up.</value>
    [Description("Gets or sets the color of the pop up.")]
    [DefaultValue(typeof (Color), "White")]
    [Category("Appearance")]
    public Color PopupBackColor
    {
      get
      {
        return this.popupBackColor;
      }
      set
      {
        this.popupBackColor = value;
      }
    }

    /// <summary>Gets the default size of the control.</summary>
    /// <value></value>
    /// <returns>
    /// The default <see cref="T:System.Drawing.Size" /> of the control.
    /// </returns>
    protected override Size DefaultSize
    {
      get
      {
        return new Size(150, 59);
      }
    }

    /// <summary>
    /// Gets the gallery buttons collection. Use this collection to add or remove buttons to the gallery.
    /// </summary>
    /// <value>The gallery buttons.</value>
    [Description("Gets the gallery buttons collection. Use this collection to add or remove buttons to the gallery.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public GalleryButtonCollection GalleryButtons
    {
      get
      {
        return this.galleryButtons;
      }
    }

    /// <summary>Gets the menu items.</summary>
    /// <value>The menu items.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the menu items.")]
    [Category("Behavior")]
    public List<vApplicationMenuItem> MenuItems
    {
      get
      {
        return this.items;
      }
    }

    /// <summary>Gets the groups.</summary>
    /// <value>The groups.</value>
    [Description("Gets the groups.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Behavior")]
    public List<RibbonBarGalleryGroup> Groups
    {
      get
      {
        return this.groups;
      }
    }

    /// <summary>Gets the drop down button.</summary>
    /// <value>The drop down button.</value>
    [Browsable(false)]
    public vRibbonDropDownButton DropDownButton
    {
      get
      {
        return this.dropDownButton;
      }
    }

    /// <summary>Gets up scroll button.</summary>
    /// <value>Up scroll button.</value>
    [Browsable(false)]
    public vArrowButton UpScrollButton
    {
      get
      {
        return this.upButton;
      }
    }

    /// <summary>Gets down scroll button.</summary>
    /// <value>Down scroll button.</value>
    [Browsable(false)]
    public vArrowButton DownScrollButton
    {
      get
      {
        return this.downButton;
      }
    }

    /// <summary>Gets the content.</summary>
    /// <value>The content.</value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public vFlowLayoutPanel Content
    {
      get
      {
        return this.layoutPanel;
      }
    }

    /// <summary>Gets or sets the style key.</summary>
    /// <value>The style key.</value>
    protected virtual string StyleKey
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
    [Browsable(false)]
    [Description("Gets or sets button's theme")]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        this.theme = value;
        ControlTheme theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        FillStyle fillStyle = this.theme.QueryFillStyleSetter("RibbonTabBackGround");
        if (fillStyle != null)
          theme.StyleNormal.FillStyle = fillStyle;
        this.backFill.LoadTheme(theme.CreateCopy());
        theme.StyleHighlight.FillStyle.Colors[0] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[0]));
        theme.StyleHighlight.FillStyle.Colors[1] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[1]));
        if (theme.StyleNormal.FillStyle.ColorsNumber > 2)
        {
          theme.StyleHighlight.FillStyle.Colors[2] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[2]));
          theme.StyleHighlight.FillStyle.Colors[3] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[3]));
        }
        this.panelFill.LoadTheme(theme);
        this.panelFill.IsAnimated = true;
        this.panelFill.MaxFrameRate = 16;
        this.backFill.MaxFrameRate = 16;
        this.backFill.IsAnimated = true;
        this.AllowAnimations = true;
        this.Invalidate();
      }
    }

    [Category("Appearance")]
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
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
        this.upButton.VIBlendTheme = this.defaultTheme;
        this.downButton.VIBlendTheme = this.defaultTheme;
        this.dropDownButton.VIBlendTheme = this.defaultTheme;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vRibbonBarGallery" /> class.
    /// </summary>
    public vRibbonBarGallery()
    {
      this.galleryButtons = new GalleryButtonCollection(this);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.upButton = new vArrowButton();
      this.downButton = new vArrowButton();
      this.layoutPanel = new vFlowLayoutPanel();
      this.dropDownButton = new vRibbonDropDownButton();
      this.Controls.Add((Control) this.upButton);
      this.Controls.Add((Control) this.downButton);
      this.Controls.Add((Control) this.layoutPanel);
      this.Controls.Add((Control) this.dropDownButton);
      this.upButton.ArrowDirection = ArrowDirection.Up;
      this.downButton.ArrowDirection = ArrowDirection.Down;
      this.layoutPanel.PaintBorder = false;
      this.layoutPanel.PaintFill = false;
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.panelFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.upButton.Click += new EventHandler(this.upButton_Click);
      this.downButton.Click += new EventHandler(this.downButton_Click);
      this.dropDownButton.Click += new EventHandler(this.dropDownButton_Click);
      this.layoutPanel.Content.AutoScroll = false;
      this.layoutPanel.Layout += new LayoutEventHandler(this.layoutPanel_Layout);
      this.layoutPanel.Content.Layout += new LayoutEventHandler(this.Content_Layout);
      this.upButton.Enabled = false;
      this.downButton.Enabled = false;
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.timer.Interval = 500;
      this.boundsTimer.Tick += new EventHandler(this.boundsTimer_Tick);
      this.galleryButtons.ButtonsChanged += new EventHandler(this.galleryButtons_ButtonsChanged);
      this.Content.Content.ControlAdded += new ControlEventHandler(this.Content_ControlAdded);
      this.Content.Content.ControlRemoved += new ControlEventHandler(this.Content_ControlRemoved);
      this.layoutPanel.MouseEnter += new EventHandler(this.layoutPanel_MouseEnter);
      this.layoutPanel.MouseLeave += new EventHandler(this.layoutPanel_MouseLeave);
      this.layoutPanel.Content.MouseEnter += new EventHandler(this.Content_MouseEnter);
      this.layoutPanel.Content.MouseLeave += new EventHandler(this.Content_MouseLeave);
      this.AllowAnimations = true;
      this.layoutPanel.Content.AutoScroll = false;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.drawContentFill = false;
      this.layoutPanel.Invalidate();
      this.Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.drawContentFill = true;
      this.layoutPanel.Invalidate();
      this.Invalidate();
    }

    private void Content_MouseLeave(object sender, EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.drawContentFill = false;
      this.layoutPanel.Invalidate();
      this.Invalidate();
    }

    private void Content_MouseEnter(object sender, EventArgs e)
    {
      this.drawContentFill = true;
      this.layoutPanel.Invalidate();
      this.Invalidate();
    }

    private void layoutPanel_MouseLeave(object sender, EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.drawContentFill = false;
      this.layoutPanel.Invalidate();
      this.Invalidate();
    }

    private void layoutPanel_MouseEnter(object sender, EventArgs e)
    {
      this.drawContentFill = true;
      this.layoutPanel.Invalidate();
      this.Invalidate();
    }

    private void Content_ControlRemoved(object sender, ControlEventArgs e)
    {
    }

    private void Content_ControlAdded(object sender, ControlEventArgs e)
    {
    }

    private void BeginUpdate()
    {
      ++this.updateCount;
    }

    private void EndUpdate()
    {
      --this.updateCount;
    }

    private bool Updating()
    {
      return this.updateCount > 0;
    }

    private void galleryButtons_ButtonsChanged(object sender, EventArgs e)
    {
      foreach (GalleryButton galleryButton in this.GalleryButtons)
      {
        galleryButton.Click -= new EventHandler(this.button_Click);
        galleryButton.Click += new EventHandler(this.button_Click);
      }
    }

    private void Content_Layout(object sender, LayoutEventArgs e)
    {
      this.SetUpButtonState();
      this.SetDownButtonState();
    }

    private void layoutPanel_Layout(object sender, LayoutEventArgs e)
    {
      this.SetUpButtonState();
      this.SetDownButtonState();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      Size size = new Size(15, 0);
      int height = this.Height / 3;
      int num = 1;
      int y1 = 0;
      this.upButton.SetBounds(this.Width - size.Width - num, y1, size.Width, height);
      int y2 = y1 + height;
      this.downButton.SetBounds(this.Width - size.Width - num, y2, size.Width, height);
      int y3 = y2 + (height - 1);
      this.dropDownButton.SetBounds(this.Width - size.Width - num, y3, size.Width, this.Height - y3);
      this.layoutPanel.SetBounds(num, num, this.Width - size.Width - 2 * num, this.Height);
      base.OnLayout(levent);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height);
      if (this.drawContentFill)
      {
        if (this.Parent is ToolStripDropDown)
        {
          this.backFill.Bounds = bounds;
          this.backFill.DrawElementFill(e.Graphics, ControlState.Normal);
        }
        this.panelFill.Bounds = bounds;
        this.panelFill.DrawElementFill(e.Graphics, ControlState.Hover);
      }
      else
      {
        this.panelFill.Bounds = bounds;
        this.panelFill.DrawElementFill(e.Graphics, ControlState.Normal);
      }
      GraphicsPath roundedPathRect = this.helper.GetRoundedPathRect(bounds, 1);
      e.Graphics.Clip = new Region(rect);
      using (Pen pen = new Pen(this.backFill.BorderColor))
        e.Graphics.DrawPath(pen, roundedPathRect);
      e.Graphics.ResetClip();
      e.Graphics.SmoothingMode = smoothingMode;
    }

    private int GetItemsHeight()
    {
      int num = 0;
      foreach (vApplicationMenuItem menuItem in this.MenuItems)
        num += menuItem.Height;
      return num;
    }

    private void AddMenuItems()
    {
      int itemsHeight = this.GetItemsHeight();
      int num = 0;
      foreach (vApplicationMenuItem menuItem in this.MenuItems)
      {
        menuItem.BackColor = this.PopupBackColor;
        this.dropDownBase.Controls.Add((Control) menuItem);
        menuItem.Bounds = new Rectangle(1, this.dropDownBase.Height - 9 - itemsHeight + num, this.dropDownBase.Width, menuItem.Height);
        num += menuItem.Height;
      }
    }

    private void dropDownButton_Click(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.BeginUpdate();
      vPanel vPanel = new vPanel();
      vPanel.PaintFill = false;
      vPanel.PaintBorder = false;
      vPanel.Content.AutoScroll = true;
      this.UnWireDropDownBase();
      this.dropDownBase = new vDropDownBase();
      this.WireDropDownBase();
      this.ctrl = new vSizingControl();
      this.dropDownBase.MinimumSize = new Size(this.Width, 3 * this.Height / 2);
      this.dropDownBase.PreferredSize = this.DropDownSize;
      List<Control> controlList = new List<Control>();
      foreach (Control control in (ArrangedElementCollection) this.Content.Content.Controls)
        controlList.Add(control);
      this.Content.Content.Controls.Clear();
      int y = 1;
      foreach (RibbonBarGalleryGroup group in this.Groups)
      {
        vApplicationMenuItem applicationMenuItem = new vApplicationMenuItem();
        applicationMenuItem.Text = group.GroupName;
        applicationMenuItem.Parent = (Control) vPanel.Content;
        applicationMenuItem.ItemType = vApplicationMenuItemType.Separator;
        applicationMenuItem.VIBlendTheme = this.VIBlendTheme;
        applicationMenuItem.Bounds = new Rectangle(1, y, this.dropDownBase.Width, applicationMenuItem.Height);
        y += applicationMenuItem.Height;
        vFlowLayoutPanel vFlowLayoutPanel = new vFlowLayoutPanel();
        vFlowLayoutPanel.PaintBorder = false;
        vFlowLayoutPanel.PaintFill = false;
        vFlowLayoutPanel.Parent = (Control) vPanel.Content;
        vFlowLayoutPanel.Bounds = new Rectangle(1, y, this.dropDownBase.Width + 1, group.GroupHeight);
        vFlowLayoutPanel.VIBlendTheme = this.VIBlendTheme;
        vFlowLayoutPanel.AutoHideControls = false;
        foreach (GalleryButton galleryButton in controlList)
        {
          if (galleryButton.GalleryGroup == group)
            vFlowLayoutPanel.Content.Controls.Add((Control) galleryButton);
        }
        vFlowLayoutPanel.Content.AutoScroll = false;
        if (vFlowLayoutPanel.Content.Controls.Count > 0)
          y += vFlowLayoutPanel.Height;
        else
          vFlowLayoutPanel.Height = 0;
      }
      vFlowLayoutPanel vFlowLayoutPanel1 = new vFlowLayoutPanel();
      vFlowLayoutPanel1.PaintBorder = false;
      vFlowLayoutPanel1.PaintFill = false;
      vFlowLayoutPanel1.Parent = (Control) vPanel.Content;
      vFlowLayoutPanel1.Content.HorizontalScroll.Maximum = vFlowLayoutPanel1.Content.HorizontalScroll.Minimum = 0;
      vFlowLayoutPanel1.Content.HorizontalScroll.Visible = false;
      int itemsHeight = this.GetItemsHeight();
      vFlowLayoutPanel1.VIBlendTheme = this.VIBlendTheme;
      vFlowLayoutPanel1.AutoHideControls = false;
      vFlowLayoutPanel1.Content.AutoScroll = false;
      foreach (GalleryButton galleryButton in controlList)
      {
        if (galleryButton.GalleryGroup == null)
          vFlowLayoutPanel1.Content.Controls.Add((Control) galleryButton);
      }
      vFlowLayoutPanel1.Bounds = new Rectangle(1, y, this.dropDownBase.Width - 5, 30);
      vFlowLayoutPanel1.Height = vFlowLayoutPanel1.GetDesiredHeight();
      if (vFlowLayoutPanel1.Content.Controls.Count == 0)
      {
        if (vPanel.Content.Controls.Count > 0)
          vPanel.Content.Controls[vPanel.Content.Controls.Count - 1].Height += vFlowLayoutPanel1.Bounds.Height;
        vFlowLayoutPanel1.Bounds = Rectangle.Empty;
      }
      this.dropDownBase.Controls.Add((Control) this.ctrl);
      this.dropDownBase.Controls.Add((Control) vPanel);
      this.ctrl.MinimumSize = new Size(0, 8);
      vPanel.BackColor = this.popupBackColor;
      this.dropDownBase.OpenControl(ArrowDirection.Down, new Point(this.PointToScreen(Point.Empty).X, this.PointToScreen(Point.Empty).Y));
      this.ctrl.BackGroundFill.LoadTheme(this.Theme);
      this.AddMenuItems();
      vPanel.Bounds = new Rectangle(1, 1, this.dropDownBase.Width - 2, this.dropDownBase.Height - 10 - itemsHeight);
      this.ctrl.Bounds = new Rectangle(1, vPanel.Bounds.Bottom + itemsHeight, vPanel.Bounds.Width, 8);
      this.boundsTimer.Start();
      vPanel.Content.HorizontalScroll.Visible = false;
      vPanel.Content.HorizontalScroll.Maximum = 0;
      vPanel.Content.HorizontalScroll.Minimum = 0;
    }

    private void button_Click(object sender, EventArgs e)
    {
      this.HideDropDown();
      GalleryButton galleryButton1 = sender as GalleryButton;
      foreach (GalleryButton galleryButton2 in this.GalleryButtons)
      {
        galleryButton2.isSelected = false;
        galleryButton2.Invalidate();
      }
      galleryButton1.isSelected = true;
      galleryButton1.Invalidate();
    }

    /// <summary>Hides the drop down.</summary>
    public void HideDropDown()
    {
      if (this.dropDownBase == null)
        return;
      this.dropDownBase.Hide();
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.timer.Dispose();
        this.boundsTimer.Dispose();
      }
      base.Dispose(disposing);
    }

    private void boundsTimer_Tick(object sender, EventArgs e)
    {
      this.dropDownBase.Location = new Point(this.PointToScreen(Point.Empty).X, this.PointToScreen(Point.Empty).Y);
    }

    private void WireDropDownBase()
    {
      if (this.dropDownBase == null)
        return;
      this.dropDownBase.Paint += new PaintEventHandler(this.dropDownBase_Paint);
      this.dropDownBase.DropDownClose += new EventHandler(this.dropDownBase_DropDownClose);
      this.dropDownBase.DropDownOpen += new EventHandler(this.dropDownBase_DropDownOpen);
      this.dropDownBase.SizeChanged += new EventHandler(this.dropDownBase_SizeChanged);
      this.dropDownBase.Layout += new LayoutEventHandler(this.dropDownBase_Layout);
      this.dropDownBase.DropDownClosing += new EventHandler(this.dropDownBase_DropDownClosing);
      this.dropDownBase.VisibleChanged += new EventHandler(this.dropDownBase_VisibleChanged);
    }

    private void UnWireDropDownBase()
    {
      if (this.dropDownBase == null)
        return;
      this.dropDownBase.Paint -= new PaintEventHandler(this.dropDownBase_Paint);
      this.dropDownBase.DropDownClose -= new EventHandler(this.dropDownBase_DropDownClose);
      this.dropDownBase.DropDownOpen -= new EventHandler(this.dropDownBase_DropDownOpen);
      this.dropDownBase.SizeChanged -= new EventHandler(this.dropDownBase_SizeChanged);
      this.dropDownBase.Layout -= new LayoutEventHandler(this.dropDownBase_Layout);
    }

    private void dropDownBase_DropDownClosing(object sender, EventArgs e)
    {
      foreach (Control galleryButton in this.GalleryButtons)
        galleryButton.Parent = (Control) this.Content.Content;
    }

    private void dropDownBase_VisibleChanged(object sender, EventArgs e)
    {
      if (this.dropDownBase.Visible)
        return;
      foreach (Control galleryButton in this.GalleryButtons)
        galleryButton.Parent = (Control) this.Content.Content;
    }

    private void dropDownBase_Layout(object sender, LayoutEventArgs e)
    {
      if (this.dropDownBase.Controls.Count <= 1)
        return;
      this.dropDownBase.Controls[1].PerformLayout();
    }

    private void dropDownBase_DropDownOpen(object sender, EventArgs e)
    {
      this.dropDownBase.Controls[0].Focus();
      this.dropDownBase.Capture = false;
      this.dropDownBase.Invalidate(true);
    }

    private void dropDownBase_SizeChanged(object sender, EventArgs e)
    {
      this.dropDownBase.Capture = false;
      if (this.dropDownBase.Controls.Count > 1)
      {
        vPanel vPanel = (vPanel) this.dropDownBase.Controls[1];
        vPanel.Content.AutoScroll = false;
        int itemsHeight = this.GetItemsHeight();
        vPanel.Bounds = new Rectangle(1, 1, this.dropDownBase.Width - 2, this.dropDownBase.Height - 10 - itemsHeight);
        int num1 = 5;
        if (vPanel.Content.VerticalScroll.Maximum > vPanel.Content.Height)
          num1 = 22;
        int num2 = 0;
        foreach (Control control in (ArrangedElementCollection) vPanel.Content.Controls)
        {
          control.Width = this.dropDownBase.Width - num1;
          if (control != vPanel.Content.Controls[vPanel.Content.Controls.Count - 1])
            num2 += control.Height;
        }
        vFlowLayoutPanel vFlowLayoutPanel = vPanel.Content.Controls[vPanel.Content.Controls.Count - 1] as vFlowLayoutPanel;
        if (vFlowLayoutPanel != null)
          vFlowLayoutPanel.Height = vFlowLayoutPanel.GetDesiredHeight();
        int num3 = 0;
        foreach (vApplicationMenuItem menuItem in this.MenuItems)
        {
          menuItem.Location = new Point(menuItem.Location.X, num3 + this.dropDownBase.Controls[1].Bounds.Bottom);
          menuItem.Width = this.dropDownBase.Width - 2;
          num3 += menuItem.Height;
        }
        this.ctrl.Bounds = new Rectangle(1, this.dropDownBase.Controls[1].Bounds.Bottom + itemsHeight, this.dropDownBase.Controls[1].Bounds.Width, 8);
        this.dropDownBase.Invalidate();
      }
      this.dropDownBase.Invalidate();
      this.timer.Stop();
      this.timer.Start();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      if (this.dropDownBase.Controls.Count > 1)
        ((vPanel) this.dropDownBase.Controls[1]).Content.AutoScroll = true;
      this.timer.Stop();
    }

    private void dropDownBase_DropDownClose(object sender, EventArgs e)
    {
      foreach (Control galleryButton in this.GalleryButtons)
        galleryButton.Parent = (Control) this.Content.Content;
      this.EndUpdate();
      this.boundsTimer.Stop();
    }

    private void dropDownBase_Paint(object sender, PaintEventArgs e)
    {
      using (Pen pen = new Pen(this.backFill.BorderColor))
      {
        Rectangle rect = new Rectangle(0, 0, this.dropDownBase.Width - 1, this.dropDownBase.Height - 1);
        e.Graphics.DrawRectangle(pen, rect);
      }
    }

    private void downButton_Click(object sender, EventArgs e)
    {
      this.Content.ScrollDown();
      this.SetDownButtonState();
    }

    private void SetDownButtonState()
    {
      this.downButton.Enabled = true;
      if (!this.Content.CanScrollDown)
        this.downButton.Enabled = false;
      this.upButton.Enabled = this.Content.CanScrollUp;
      this.downButton.Refresh();
      this.upButton.Refresh();
    }

    private void upButton_Click(object sender, EventArgs e)
    {
      this.Content.ScrollUp();
      this.SetUpButtonState();
    }

    private void SetUpButtonState()
    {
      this.upButton.Enabled = true;
      if (!this.Content.CanScrollUp)
        this.upButton.Enabled = false;
      this.downButton.Enabled = this.Content.CanScrollDown;
      this.downButton.Refresh();
      this.upButton.Refresh();
    }
  }
}
