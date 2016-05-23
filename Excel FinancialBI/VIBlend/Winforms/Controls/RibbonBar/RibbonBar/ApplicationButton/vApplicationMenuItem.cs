// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vApplicationMenuItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vApplicationMenuItem control.</summary>
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vApplicationMenuItem), "ControlIcons.vApplicationMenuItem.ico")]
  [Designer("VIBlend.WinForms.Controls.Design.vApplicationMenuItemDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Display an application menu item.")]
  public class vApplicationMenuItem : ScrollableControlMiniBase
  {
    private RibbonPaintHelper ribbonPaintHelper = new RibbonPaintHelper();
    private Color tickColor = Color.Black;
    private int arrowAreaWidth = 23;
    private Timer timer = new Timer();
    private Color panelBackColor = Color.White;
    private int dropDownWidth = 200;
    private bool showArrow = true;
    private bool paintFill = true;
    private bool paintBorder = true;
    private bool useThemeTextColor = true;
    private bool autoOpen = true;
    private Size checkBoxSize = new Size(16, 16);
    private ContentAlignment imageAlignment = ContentAlignment.MiddleLeft;
    private int imageIndex = -1;
    private ContentAlignment textAlignment = ContentAlignment.MiddleCenter;
    private PaintHelper paintHelper = new PaintHelper();
    private ImageAndTextHelper textHelper = new ImageAndTextHelper();
    private string styleKey = "ApplicationMenuItem";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private ControlState controlState;
    private int imageAreaWidth;
    private string descriptionText;
    private ContentAlignment descriptionTextAlignment;
    private Font descriptionTextFont;
    private Color descriptionTextColor;
    private vApplicationMenuItemCollection menuItems;
    private ToolStripDropDown dropDown;
    internal bool opened;
    internal vApplicationMenuItem owner;
    private int dropDownHeight;
    private Point dropDownPosition;
    private vApplicationMenuItemType itemType;
    private bool showCheckBox;
    private bool paintImageAreaFill;
    private bool paintImageAreaBorder;
    private bool checkState;
    private Image image;
    private bool highlightArrow;
    private int radius;
    private vApplicationMenuItem selectedItem;
    private ImageList imageList;
    private BackgroundElement backFill;
    private BackgroundElement imageAreaFill;
    private ControlTheme theme;
    private ControlTheme theme2;

    /// <summary>Gets or sets the selected menu item.</summary>
    /// <value>The selected item.</value>
    [Browsable(false)]
    [Description("Gets or sets the selected menu item.")]
    public vApplicationMenuItem SelectedChildMenuItem
    {
      get
      {
        return this.selectedItem;
      }
      set
      {
        if (this.selectedItem == value)
          return;
        this.selectedItem = value;
        this.OnSelectionChanged();
      }
    }

    /// <summary>Gets or sets the radius.</summary>
    /// <value>The radius.</value>
    [Category("Behavior")]
    [DefaultValue(0)]
    public int Radius
    {
      get
      {
        return this.radius;
      }
      set
      {
        this.radius = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the text color coming from the theme
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use the text color coming from the theme")]
    [DefaultValue(true)]
    public bool UseThemeTextColor
    {
      get
      {
        return this.useThemeTextColor;
      }
      set
      {
        this.useThemeTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to highlight the arrow
    /// </summary>
    [Description("Gets or sets a value indicating whether to highlight the arrow")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool HighlightArrow
    {
      get
      {
        return this.highlightArrow;
      }
      set
      {
        this.highlightArrow = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show an arrow
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description(" Gets or sets a value indicating whether to show an arrow")]
    public bool ShowArrow
    {
      get
      {
        return this.showArrow;
      }
      set
      {
        this.showArrow = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.vApplicationMenuItem" /> is checked.
    /// </summary>
    /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether the item is checked.")]
    [Category("Behavior")]
    public bool Checked
    {
      get
      {
        return this.checkState;
      }
      set
      {
        this.checkState = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint item's border
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to paint item's border")]
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

    /// <summary>
    ///  Gets or sets a value indicating whether to paint item's background
    /// </summary>
    [Description("Gets or sets a value indicating whether to paint item's background")]
    [DefaultValue(true)]
    [Category("Behavior")]
    public bool PaintFill
    {
      get
      {
        return this.paintFill;
      }
      set
      {
        this.paintFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint image's area border
    /// </summary>
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to paint image's area border")]
    [Category("Behavior")]
    public bool PaintImageAreaBorder
    {
      get
      {
        return this.paintImageAreaBorder;
      }
      set
      {
        this.paintImageAreaBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint image's area background
    /// </summary>
    [Description("Gets or sets a value indicating whether to paint image's area background")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool PaintImageAreaFill
    {
      get
      {
        return this.paintImageAreaFill;
      }
      set
      {
        this.paintImageAreaFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show a checkbox.
    /// </summary>
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to show a checkbox.")]
    [Category("Behavior")]
    public bool ShowCheckBox
    {
      get
      {
        return this.showCheckBox;
      }
      set
      {
        this.showCheckBox = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dropdown is opened automatically
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description(" Gets or sets a value indicating whether the dropdown is opened automatically")]
    public bool AutoOpen
    {
      get
      {
        return this.autoOpen;
      }
      set
      {
        this.autoOpen = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the type of the item.</summary>
    /// <value>The type of the item.</value>
    [Category("Behavior")]
    [Description("Gets or sets the type of the item.")]
    public vApplicationMenuItemType ItemType
    {
      get
      {
        return this.itemType;
      }
      set
      {
        this.itemType = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the width of the arrow area.</summary>
    /// <value>The width of the arrow area.</value>
    [Category("Behavior")]
    [DefaultValue(23)]
    [Description("Gets or sets the width of the arrow area.")]
    public int ArrowAreaWidth
    {
      get
      {
        return this.arrowAreaWidth;
      }
      set
      {
        this.arrowAreaWidth = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the width of the image area.</summary>
    /// <value>The width of the image area.</value>
    [Description("Gets or sets the width of the image area.")]
    [DefaultValue(0)]
    [Category("Behavior")]
    public int ImageAreaWidth
    {
      get
      {
        return this.imageAreaWidth;
      }
      set
      {
        this.imageAreaWidth = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the content back.</summary>
    /// <value>The color of the content.</value>
    [Description("Gets or sets the color of the content back.")]
    [DefaultValue(typeof (Color), "White")]
    [Category("Appearance")]
    public Color ContentBackColor
    {
      get
      {
        return this.panelBackColor;
      }
      set
      {
        this.panelBackColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the width of the drop down.</summary>
    /// <value>The width of the drop down.</value>
    [Category("Behavior")]
    [Description("Gets or sets the width of the drop down.")]
    [DefaultValue(typeof (Point), "0,0")]
    public Point DropDownPosition
    {
      get
      {
        return this.dropDownPosition;
      }
      set
      {
        this.dropDownPosition = value;
      }
    }

    /// <summary>Gets or sets the width of the drop down.</summary>
    /// <value>The width of the drop down.</value>
    [Category("Behavior")]
    [Description("Gets or sets the width of the drop down.")]
    [DefaultValue(200)]
    public int DropDownWidth
    {
      get
      {
        return this.dropDownWidth;
      }
      set
      {
        this.dropDownWidth = value;
      }
    }

    /// <summary>Gets or sets the height of the drop down.</summary>
    /// <value>The height of the drop down.</value>
    [DefaultValue(0)]
    [Description("Gets or sets the height of the drop down.")]
    [Category("Behavior")]
    public int DropDownHeight
    {
      get
      {
        return this.dropDownHeight;
      }
      set
      {
        this.dropDownHeight = value;
      }
    }

    /// <summary>Gets a value indicating whether drop down is opened.</summary>
    /// <value>
    /// 	<c>true</c> if this instance the drop down is opened; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    public bool IsDropDownOpened
    {
      get
      {
        return this.opened;
      }
    }

    protected override Size DefaultSize
    {
      get
      {
        return new Size(base.DefaultSize.Width, 43);
      }
    }

    /// <summary>Gets the parent menu item.</summary>
    /// <value>The parent menu item.</value>
    [Browsable(false)]
    public vApplicationMenuItem ParentMenuItem
    {
      get
      {
        return this.owner;
      }
    }

    /// <summary>Gets the root menu item.</summary>
    /// <value>The root menu item.</value>
    [Browsable(false)]
    public vApplicationMenuItem RootMenuItem
    {
      get
      {
        for (vApplicationMenuItem parentMenuItem = this.ParentMenuItem; parentMenuItem != null; parentMenuItem = parentMenuItem.ParentMenuItem)
        {
          if (parentMenuItem.ParentMenuItem == null)
            return parentMenuItem;
        }
        return this;
      }
    }

    /// <summary>Gets the items.</summary>
    /// <value>The items.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Behavior")]
    [Description("Gets the children collection of menu items")]
    public vApplicationMenuItemCollection Items
    {
      get
      {
        return this.menuItems;
      }
    }

    /// <summary>Gets the drop down.</summary>
    /// <value>The drop down.</value>
    [Category("Behavior")]
    [Browsable(false)]
    public ToolStripDropDown DropDown
    {
      get
      {
        return this.dropDown;
      }
    }

    /// <summary>Gets or sets the color of the check box tick.</summary>
    /// <value>The color of the check box tick.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Category("Behavior")]
    [Description("Gets or sets the color of the check box tick.")]
    public Color CheckBoxTickColor
    {
      get
      {
        return this.tickColor;
      }
      set
      {
        this.tickColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the image alignment.</summary>
    /// <value>The image alignment.</value>
    [Category("Appearance")]
    [Description("Gets or sets the image alignment.")]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.imageAlignment;
      }
      set
      {
        this.imageAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the size of the check box.</summary>
    /// <value>The size of the check box.</value>
    [DefaultValue(typeof (Size), "16,16")]
    [Description("Gets or sets the size of the check box.")]
    [Category("Behavior")]
    public Size CheckBoxSize
    {
      get
      {
        return this.checkBoxSize;
      }
      set
      {
        this.checkBoxSize = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the image.</summary>
    /// <value>The image.</value>
    [DefaultValue(null)]
    [Description("Gets or sets the image.")]
    [Category("Behavior")]
    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the index of the image.</summary>
    /// <value>The index of the image.</value>
    [DefaultValue(-1)]
    [Description("Gets or sets the index of the image.")]
    [Category("Behavior")]
    public int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        this.imageIndex = value;
        if (this.ImageList == null || value < 0 || value >= this.ImageList.Images.Count)
          return;
        this.Image = this.ImageList.Images[value];
      }
    }

    /// <summary>Gets or sets the image list.</summary>
    /// <value>The image list.</value>
    [Category("Behavior")]
    [DefaultValue(null)]
    public ImageList ImageList
    {
      get
      {
        return this.imageList;
      }
      set
      {
        if (this.imageList == value)
          return;
        if (value != null)
          this.image = (Image) null;
        this.imageList = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the text alignment.</summary>
    /// <value>The text alignment.</value>
    [Description("Gets or sets the text alignment.")]
    [DefaultValue(typeof (ContentAlignment), "ContentAlignment.MiddleCenter")]
    [Category("Behavior")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the description text alignment.</summary>
    /// <value>The description text alignment.</value>
    [Description("Gets or sets the description text alignment.")]
    [Category("Appearance")]
    public ContentAlignment DescriptionTextAlignment
    {
      get
      {
        return this.descriptionTextAlignment;
      }
      set
      {
        this.descriptionTextAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the description text font.</summary>
    /// <value>The description text font.</value>
    [Description("Gets or sets the description text font.")]
    [Category("Appearance")]
    public Font DescriptionTextFont
    {
      get
      {
        return this.descriptionTextFont;
      }
      set
      {
        this.descriptionTextFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the description text.</summary>
    /// <value>The color of the description text.</value>
    [Category("Behavior")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the color of the description text.")]
    public Color DescriptionTextColor
    {
      get
      {
        return this.descriptionTextColor;
      }
      set
      {
        this.descriptionTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the description text.</summary>
    /// <value>The description text.</value>
    [Category("Behavior")]
    [DefaultValue("")]
    [Description("Gets or sets the description text.")]
    public string DescriptionText
    {
      get
      {
        return this.descriptionText;
      }
      set
      {
        this.descriptionText = value;
        this.Invalidate();
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

    /// <summary>Gets or sets the theme of the control.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [Browsable(false)]
    [Description("Gets or sets the theme of the control.")]
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
        this.theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        this.theme2 = ThemeCache.GetTheme(this.StyleKey + "1", this.VIBlendTheme);
        FillStyle fillStyle = this.theme.QueryFillStyleSetter("MenuImageArea");
        this.backFill.LoadTheme(this.theme);
        if (fillStyle != null)
          this.theme2.StyleNormal.FillStyle = fillStyle;
        this.imageAreaFill.LoadTheme(this.theme2);
        this.backFill.IsAnimated = false;
        this.imageAreaFill.IsAnimated = false;
        this.AllowAnimations = false;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
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
        if (defaultTheme != null)
          this.Theme = defaultTheme;
        foreach (vApplicationMenuItem applicationMenuItem in this.Items)
          applicationMenuItem.VIBlendTheme = this.VIBlendTheme;
      }
    }

    /// <summary>Occurs when a selection has been changed</summary>
    [Category("Action")]
    public event EventHandler SelectionChanged;

    /// <summary>Occurs when content button is clicked.</summary>
    [Category("Action")]
    public event EventHandler ContentButtonClicked;

    /// <summary>Occurs when arrow button is clicked.</summary>
    [Category("Action")]
    public event EventHandler ArrowButtonClicked;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vApplicationMenuItem" /> class.
    /// </summary>
    public vApplicationMenuItem()
    {
      this.dropDown = new ToolStripDropDown();
      this.dropDown.Padding = new Padding();
      this.dropDown.Margin = new Padding();
      this.Padding = new Padding();
      this.dropDown.AutoClose = false;
      this.menuItems = new vApplicationMenuItemCollection(this, (Control) this.dropDown);
      this.descriptionText = "";
      this.descriptionTextFont = this.Font;
      this.descriptionTextColor = Color.Black;
      this.descriptionTextAlignment = ContentAlignment.TopLeft;
      this.textAlignment = ContentAlignment.MiddleLeft;
      this.dropDown.Opened += new EventHandler(this.dropDown_Opened);
      this.dropDown.Opening += new CancelEventHandler(this.dropDown_Opening);
      this.dropDown.Closed += new ToolStripDropDownClosedEventHandler(this.dropDown_Closed);
      this.dropDown.MouseLeave += new EventHandler(this.dropDown_MouseLeave);
      this.timer.Interval = 150;
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.Size = new Size(180, 20);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.imageAreaFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.AllowAnimations = true;
      this.Items.MenuItemsChanged += new EventHandler(this.Items_ItemsChanged);
    }

    protected virtual void OnContentButtonClicked()
    {
      if (this.ContentButtonClicked == null)
        return;
      this.ContentButtonClicked((object) this, EventArgs.Empty);
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.timer.Dispose();
      base.Dispose(disposing);
    }

    protected virtual void OnArrowButtonClicked()
    {
      if (this.ArrowButtonClicked == null)
        return;
      this.ArrowButtonClicked((object) this, EventArgs.Empty);
    }

    protected virtual void OnSelectionChanged()
    {
      if (this.SelectionChanged == null)
        return;
      this.SelectionChanged((object) this, EventArgs.Empty);
    }

    private GraphicsPath GetCheckBoxTick(Rectangle rect)
    {
      int num = rect.X + rect.Width / 2;
      int y1 = rect.Y + rect.Height / 2;
      GraphicsPath graphicsPath = new GraphicsPath();
      graphicsPath.AddLine(num - 4, y1, num - 2, y1 + 4);
      graphicsPath.AddLine(num - 2, y1 + 4, num + 3, y1 - 5);
      return graphicsPath;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.DesignMode)
        return;
      this.RootMenuItem.SelectedChildMenuItem = this;
      this.CloseAllItems();
      if (this.owner == null)
      {
        if (this.opened)
          this.CloseDropDown();
        else
          this.ShowDropDown();
      }
      this.Checked = !this.Checked;
      if (new Rectangle(0, 0, this.Width - this.ArrowAreaWidth, this.Height).Contains(e.Location))
        this.OnContentButtonClicked();
      else if (this.ShowArrow)
        this.OnArrowButtonClicked();
      else
        this.OnContentButtonClicked();
    }

    private void CloseAllItems()
    {
      for (vApplicationMenuItem applicationMenuItem = this.owner; applicationMenuItem != null; applicationMenuItem = applicationMenuItem.owner)
      {
        if (applicationMenuItem.owner == null)
        {
          applicationMenuItem.CloseDropDown();
          break;
        }
      }
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      this.controlState = ControlState.Normal;
      this.Invalidate();
      if (!this.CheckMousePosition(this.Items))
      {
        this.timer.Stop();
        this.timer.Start();
      }
      base.OnMouseLeave(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.Invalidate();
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.controlState = ControlState.Hover;
      this.Invalidate();
      base.OnMouseEnter(e);
      if (this.opened || !this.AutoOpen)
        return;
      this.ShowDropDown();
    }

    protected override void OnMouseHover(EventArgs e)
    {
      if (this.DesignMode)
        return;
      base.OnMouseHover(e);
    }

    internal bool CheckMousePosition(vApplicationMenuItemCollection items)
    {
      using (IEnumerator<vApplicationMenuItem> enumerator = items.GetEnumerator())
      {
        if (enumerator.MoveNext())
        {
          vApplicationMenuItem current = enumerator.Current;
          if (current.RectangleToScreen(current.ClientRectangle).Contains(Cursor.Position) || current.DropDown.Bounds.Contains(Cursor.Position))
            return true;
          return this.CheckMousePosition(current.Items);
        }
      }
      return false;
    }

    /// <summary>Closes the drop down.</summary>
    public void CloseDropDown()
    {
      if (this.dropDown == null)
        return;
      this.dropDown.Close();
      this.CloseRecursive(this.Items);
      this.timer.Stop();
    }

    private void CloseRecursive(vApplicationMenuItemCollection items)
    {
      foreach (vApplicationMenuItem applicationMenuItem in items)
      {
        applicationMenuItem.CloseDropDown();
        this.CloseRecursive(applicationMenuItem.Items);
      }
    }

    /// <summary>Shows the drop down.</summary>
    public void ShowDropDown()
    {
      if (this.Items.Count <= 0 && !this.DesignMode || this.opened)
        return;
      this.PrepareItems();
      if (this.dropDownPosition != Point.Empty)
      {
        this.dropDown.Show((Control) this, this.DropDownPosition);
      }
      else
      {
        int width = this.Width;
        if (this.ItemType != vApplicationMenuItemType.DropDown)
          this.DropDown.Show((Control) this, new Point(width, 0));
        else
          this.DropDown.Show((Control) this, new Point(0, this.Height));
      }
    }

    private void PrepareItems()
    {
      this.dropDown.Items.Clear();
      Panel panel = new Panel();
      panel.BackColor = this.panelBackColor;
      panel.Size = Size.Empty;
      panel.Dock = DockStyle.Fill;
      this.dropDown.BackColor = this.panelBackColor;
      panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
      ToolStripControlHost stripControlHost = new ToolStripControlHost((Control) panel);
      this.dropDown.MinimumSize = new Size(this.DropDownWidth, 20);
      stripControlHost.Dock = DockStyle.Fill;
      panel.Margin = Padding.Empty;
      panel.MaximumSize = new Size(this.DropDownWidth, 0);
      for (int index = 0; index < this.Items.Count; ++index)
      {
        panel.Controls.Add((Control) this.Items[index]);
        if (this.Items[index].Size.Equals((object) Size.Empty))
        {
          this.CalculateBounds(this.Items[index]);
          panel.Size = new Size(this.DropDownWidth, panel.Size.Height + this.Items[index].Height);
        }
      }
      stripControlHost.Margin = new Padding(1, 1, 1, 1);
      int y = 0;
      foreach (Control control in (ArrangedElementCollection) panel.Controls)
      {
        control.Location = new Point(0, y);
        control.Width = this.DropDownWidth;
        y += control.Height;
      }
      this.dropDown.Items.Add((ToolStripItem) stripControlHost);
      if (this.DesignMode)
      {
        this.dropDown.Items.Add("Add MenuItem");
        this.dropDown.Items[this.dropDown.Items.Count - 1].Click += new EventHandler(this.addNew_Click);
        this.dropDown.Items.Add("Add SeparatorItem");
        this.dropDown.Items[this.dropDown.Items.Count - 1].Click += new EventHandler(this.addNewSeparator_Click);
        this.InitializeItems(1);
        this.InitializeItems(2);
        this.dropDown.PerformLayout();
        this.dropDown.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
      }
      else
      {
        if (this.DropDownHeight <= 0)
          return;
        this.DropDown.MinimumSize = new Size(this.dropDown.MinimumSize.Width, this.DropDownHeight);
      }
    }

    private void InitializeItems(int i)
    {
      this.dropDown.Items[this.dropDown.Items.Count - i].BackColor = Color.White;
      this.dropDown.Items[this.dropDown.Items.Count - i].ForeColor = Color.Black;
      this.dropDown.Items[this.dropDown.Items.Count - i].Alignment = ToolStripItemAlignment.Right;
      this.dropDown.Items[this.dropDown.Items.Count - i].Dock = DockStyle.Bottom;
      this.dropDown.Items[this.dropDown.Items.Count - i].Font = new Font(this.Font, FontStyle.Bold);
    }

    private void addNew_Click(object sender, EventArgs e)
    {
      IDesignerHost designerHost = this.GetService(typeof (IDesignerHost)) as IDesignerHost;
      if (designerHost == null)
        return;
      vApplicationMenuItem applicationMenuItem = designerHost.CreateComponent(typeof (vApplicationMenuItem)) as vApplicationMenuItem;
      applicationMenuItem.Text = "vApplicationMenuItem";
      this.Items.Add(applicationMenuItem);
      this.CloseDropDown();
      this.ShowDropDown();
    }

    private void addNewSeparator_Click(object sender, EventArgs e)
    {
      IDesignerHost designerHost = this.GetService(typeof (IDesignerHost)) as IDesignerHost;
      if (designerHost == null)
        return;
      vApplicationMenuItem applicationMenuItem = designerHost.CreateComponent(typeof (vApplicationMenuItem)) as vApplicationMenuItem;
      applicationMenuItem.Text = "vApplicationMenuItem";
      this.Items.Add(applicationMenuItem);
      this.CloseDropDown();
      this.ShowDropDown();
      applicationMenuItem.ItemType = vApplicationMenuItemType.Separator;
    }

    private void CalculateBounds(vApplicationMenuItem applicationMenuItem)
    {
      int num1 = 0;
      int num2 = 0;
      using (Graphics graphics = applicationMenuItem.CreateGraphics())
      {
        SizeF sizeF1 = graphics.MeasureString(applicationMenuItem.descriptionText, applicationMenuItem.descriptionTextFont);
        SizeF sizeF2 = graphics.MeasureString(applicationMenuItem.Text, applicationMenuItem.Font);
        SizeF sizeF3 = SizeF.Empty;
        if (applicationMenuItem.Image != null)
          sizeF3 = (SizeF) applicationMenuItem.Image.Size;
        num2 = applicationMenuItem.Height;
        num1 = (int) Math.Round((double) sizeF3.Width) + (int) Math.Max(Math.Round((double) sizeF1.Width), (double) (int) Math.Round((double) sizeF2.Width));
      }
      applicationMenuItem.Size = new Size(35 + num1 + applicationMenuItem.Margin.Horizontal, num2 + applicationMenuItem.Margin.Vertical);
    }

    private void ResetDescriptionTextAlignment()
    {
      this.DescriptionTextAlignment = ContentAlignment.TopLeft;
    }

    private bool ShouldSerializeDescriptionTextAlignment()
    {
      return this.DescriptionTextAlignment != ContentAlignment.TopLeft;
    }

    private void ResetDescriptionTextFont()
    {
      this.DescriptionTextFont = Control.DefaultFont;
    }

    private bool ShouldSerializeDescriptionTextFont()
    {
      return !this.DescriptionTextFont.Equals((object) Control.DefaultFont);
    }

    private void Items_ItemsChanged(object sender, EventArgs e)
    {
      foreach (vApplicationMenuItem applicationMenuItem in this.Items)
        applicationMenuItem.VIBlendTheme = this.RootMenuItem.VIBlendTheme;
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.ItemType == vApplicationMenuItemType.MenuItem)
      {
        Rectangle rectangle1 = new Rectangle(0, 0, this.ImageAreaWidth, this.Height);
        Rectangle rectangle2 = new Rectangle(0, 0, this.ImageAreaWidth - 1, this.Height - 1);
        Rectangle rectangle3 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        Rectangle fillRect = this.ClientRectangle;
        Rectangle arrowRect = new Rectangle(this.Width - this.ArrowAreaWidth, 0, this.ArrowAreaWidth, this.Height);
        bool mouseOverArrow = this.RectangleToScreen(arrowRect).Contains(Cursor.Position);
        if (this.ShowArrow && this.Items.Count > 0)
          fillRect = new Rectangle(0, 0, this.Width - this.ArrowAreaWidth, this.Height);
        this.backFill.Radius = this.Radius;
        if (this.opened)
          this.controlState = ControlState.Hover;
        if (this.controlState != ControlState.Normal)
        {
          if (this.PaintFill)
          {
            this.backFill.Bounds = fillRect;
            Color[] colors = this.Theme.StyleHighlight.FillStyle.Colors;
            if (this.Items.Count > 0)
              this.DrawHighlightArrowState(e, ref fillRect, ref arrowRect, mouseOverArrow, colors);
            if (!this.HighlightArrow || this.Items.Count == 0)
            {
              this.backFill.Bounds = this.ClientRectangle;
              this.backFill.DrawStandardFill(e.Graphics, this.controlState, GradientStyles.Linear);
            }
          }
          if (this.PaintBorder)
          {
            this.backFill.Bounds = rectangle3;
            this.backFill.DrawElementBorder(e.Graphics, this.controlState);
          }
        }
        if (this.controlState == ControlState.Normal)
        {
          if (this.PaintImageAreaFill)
          {
            this.imageAreaFill.Bounds = rectangle1;
            this.imageAreaFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
          }
          if (this.PaintImageAreaBorder)
          {
            this.imageAreaFill.Bounds = rectangle2;
            this.imageAreaFill.DrawElementBorder(e.Graphics, ControlState.Normal);
          }
        }
        this.DrawImageAndText(e);
        this.DrawArrow(e, arrowRect);
        this.DrawCheckBox(e);
      }
      else if (this.ItemType == vApplicationMenuItemType.Separator)
        this.DrawSeparator(e);
      else
        this.DrawDropDown(e);
    }

    private void DrawDropDown(PaintEventArgs e)
    {
      if (this.ClientRectangle.Width == 0 || this.ClientRectangle.Height == 0)
        return;
      if (this.dropDown.Visible)
        this.controlState = ControlState.Pressed;
      if (!this.Enabled)
        this.controlState = ControlState.Disabled;
      Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
      --rectangle.Width;
      --rectangle.Height;
      this.backFill.Bounds = rectangle;
      if (this.controlState != ControlState.Normal)
        this.backFill.DrawElementFill(e.Graphics, this.controlState);
      if (this.controlState != ControlState.Normal)
        this.backFill.DrawElementBorder(e.Graphics, this.controlState);
      Color arrowColor = this.backFill.BorderColor;
      if (this.controlState == ControlState.Pressed)
        arrowColor = this.backFill.PressedBorderColor;
      if (this.controlState == ControlState.Hover)
        arrowColor = this.backFill.HighlightBorderColor;
      this.ribbonPaintHelper.GetArrowSize();
      this.ribbonPaintHelper.DrawRibbonDropDownButtonArrow(e.Graphics, this.ClientRectangle, arrowColor);
    }

    private void DrawSeparator(PaintEventArgs e)
    {
      this.backFill.Radius = 0;
      Rectangle rectangle = new Rectangle(this.ImageAreaWidth, 0, this.Width - this.ImageAreaWidth, this.Height);
      if (this.PaintFill)
      {
        this.backFill.Bounds = rectangle;
        this.backFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Solid);
      }
      if (this.PaintBorder)
      {
        this.backFill.Bounds = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      }
      StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.TextAlignment, true, true);
      this.DrawTextWithoutImage(e, stringFormat);
    }

    private Rectangle DrawArrow(PaintEventArgs e, Rectangle arrowRect)
    {
      if (this.ShowArrow && this.Items.Count > 0)
      {
        Rectangle middleCenterRectangle = new RibbonPaintHelper().GetMiddleCenterRectangle(arrowRect, new Rectangle(0, 0, 3, 5));
        Color color = this.ForeColor;
        if (this.UseThemeTextColor)
          color = this.backFill.Theme.StyleNormal.TextColor;
        this.paintHelper.DrawArrowFigure(e.Graphics, color, middleCenterRectangle, ArrowDirection.Right);
      }
      return arrowRect;
    }

    private void DrawCheckBox(PaintEventArgs e)
    {
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      if (this.ShowCheckBox)
      {
        Rectangle rect = new Rectangle(3, 1 + this.Height / 2 - this.CheckBoxSize.Height / 2, this.CheckBoxSize.Width, this.CheckBoxSize.Height);
        using (GraphicsPath checkBoxTick = this.GetCheckBoxTick(rect))
        {
          using (Pen pen = new Pen(this.CheckBoxTickColor))
          {
            this.backFill.Bounds = rect;
            this.backFill.DrawStandardFill(e.Graphics, ControlState.Pressed, GradientStyles.Solid);
            this.backFill.DrawElementBorder(e.Graphics, ControlState.Pressed);
            if (this.Checked)
              e.Graphics.DrawPath(pen, checkBoxTick);
          }
        }
      }
      e.Graphics.SmoothingMode = smoothingMode;
    }

    private void DrawHighlightArrowState(PaintEventArgs e, ref Rectangle fillRect, ref Rectangle arrowRect, bool mouseOverArrow, Color[] colors)
    {
      if (!this.HighlightArrow)
        return;
      FillStyle fillStyle1 = this.Theme.CreateCopy().StyleHighlight.FillStyle;
      FillStyle fillStyle2 = this.Theme.CreateCopy().StyleHighlight.FillStyle;
      for (int index = 0; index < colors.Length; ++index)
        fillStyle2.Colors[index] = ControlPaint.LightLight(colors[index]);
      if (mouseOverArrow)
      {
        this.backFill.Theme.StyleHighlight.FillStyle = fillStyle2;
        this.backFill.DrawStandardFill(e.Graphics, this.controlState, GradientStyles.Linear);
        this.backFill.Theme.StyleHighlight.FillStyle = fillStyle1;
        if (!this.ShowArrow)
          return;
        this.backFill.Bounds = arrowRect;
        this.backFill.Radius = 0;
        this.backFill.DrawElementFill(e.Graphics, this.controlState);
        this.backFill.DrawElementBorder(e.Graphics, this.controlState);
        this.backFill.Radius = this.Radius;
      }
      else
      {
        this.backFill.Theme.StyleHighlight.FillStyle = fillStyle2;
        if (this.ShowArrow)
        {
          this.backFill.Radius = 0;
          this.backFill.Bounds = arrowRect;
          this.backFill.DrawElementFill(e.Graphics, this.controlState);
          this.backFill.DrawElementBorder(e.Graphics, this.controlState);
          this.backFill.Radius = this.Radius;
        }
        this.backFill.Bounds = fillRect;
        this.backFill.Theme.StyleHighlight.FillStyle = fillStyle1;
        this.backFill.DrawStandardFill(e.Graphics, this.controlState, GradientStyles.Linear);
      }
    }

    private void DrawImageAndText(PaintEventArgs e)
    {
      StringFormat stringFormat1 = ImageAndTextHelper.GetStringFormat((Control) this, this.TextAlignment, true, true);
      if (this.Image == null && this.ImageList != null && (this.ImageIndex >= 0 && this.ImageIndex < this.ImageList.Images.Count))
        this.Image = this.ImageList.Images[this.ImageIndex];
      if (this.Image != null)
      {
        Rectangle rectangle1 = this.ClientRectangle;
        rectangle1 = new Rectangle(this.ImageAreaWidth, 0, this.Width - this.ImageAreaWidth, this.Height);
        if (this.Items.Count > 0 && this.ShowArrow)
          rectangle1 = new Rectangle(this.ImageAreaWidth, 0, this.Width - this.ImageAreaWidth - this.ArrowAreaWidth, this.Height);
        int imageAreaWidth = this.ImageAreaWidth;
        if (this.ShowCheckBox)
          imageAreaWidth /= 2;
        Rectangle imageRectangle = ImageAndTextHelper.GetImageRectangle(this.Image, new Rectangle(0, 0, imageAreaWidth, this.Height), this.imageAlignment);
        e.Graphics.DrawImage(this.Image, imageRectangle);
        if (this.DescriptionText.Length > 0)
        {
          SizeF sizeF = e.Graphics.MeasureString(this.DescriptionText, this.DescriptionTextFont);
          Rectangle rectangle2 = new Rectangle(rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height / 2);
          StringFormat stringFormat2 = ImageAndTextHelper.GetStringFormat((Control) this, this.DescriptionTextAlignment, true, true);
          int num = 0;
          using (SolidBrush solidBrush = new SolidBrush(this.DescriptionTextColor))
          {
            switch (this.DescriptionTextAlignment)
            {
              case ContentAlignment.BottomCenter:
              case ContentAlignment.BottomRight:
              case ContentAlignment.BottomLeft:
                rectangle1 = new Rectangle(rectangle1.X, rectangle1.Y + rectangle1.Height / 2 + num, rectangle1.Width, rectangle1.Height / 2 - num);
                break;
              case ContentAlignment.MiddleRight:
              case ContentAlignment.MiddleLeft:
              case ContentAlignment.MiddleCenter:
                rectangle1 = new Rectangle(rectangle1.X, rectangle1.Y + rectangle1.Height / 4 + (int) sizeF.Height / 2 + num, rectangle1.Width, rectangle1.Height - num - rectangle1.Height / 4 - (int) sizeF.Height / 2);
                break;
              case ContentAlignment.TopLeft:
              case ContentAlignment.TopCenter:
              case ContentAlignment.TopRight:
                rectangle1 = new Rectangle(rectangle1.X, rectangle1.Y + (int) sizeF.Height + num, rectangle1.Width, rectangle1.Height - (int) sizeF.Height - num);
                break;
            }
            stringFormat2.FormatFlags |= StringFormatFlags.NoWrap;
            e.Graphics.DrawString(this.DescriptionText, this.DescriptionTextFont, (Brush) solidBrush, (RectangleF) rectangle2, stringFormat2);
          }
        }
        using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
        {
          if (this.UseThemeTextColor)
            solidBrush.Color = this.backFill.Theme.StyleNormal.TextColor;
          e.Graphics.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) rectangle1, stringFormat1);
        }
      }
      else
        this.DrawTextWithoutImage(e, stringFormat1);
    }

    private Rectangle DrawTextWithoutImage(PaintEventArgs e, StringFormat format)
    {
      Rectangle rectangle1 = this.ClientRectangle;
      rectangle1 = new Rectangle(this.ImageAreaWidth, 2, this.Width - this.ImageAreaWidth, this.Height - 2);
      if (this.Items.Count > 0 && this.ShowArrow)
        rectangle1 = new Rectangle(this.ImageAreaWidth, 2, this.Width - this.ImageAreaWidth - this.ArrowAreaWidth, this.Height - 2);
      if (this.DescriptionText.Length > 0)
      {
        SizeF sizeF = e.Graphics.MeasureString(this.DescriptionText, this.DescriptionTextFont);
        Rectangle rectangle2 = new Rectangle(rectangle1.X, rectangle1.Y, rectangle1.Width, rectangle1.Height / 2);
        StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.DescriptionTextAlignment, true, true);
        int num = 0;
        using (SolidBrush solidBrush = new SolidBrush(this.DescriptionTextColor))
        {
          switch (this.DescriptionTextAlignment)
          {
            case ContentAlignment.BottomCenter:
            case ContentAlignment.BottomRight:
            case ContentAlignment.BottomLeft:
              rectangle1 = new Rectangle(rectangle1.X, rectangle1.Y + rectangle1.Height / 2 + num, rectangle1.Width, rectangle1.Height / 2 - num);
              break;
            case ContentAlignment.MiddleRight:
            case ContentAlignment.MiddleLeft:
            case ContentAlignment.MiddleCenter:
              rectangle1 = new Rectangle(rectangle1.X, rectangle1.Y + rectangle1.Height / 4 + (int) sizeF.Height / 2 + num, rectangle1.Width, rectangle1.Height - num - rectangle1.Height / 4 - (int) sizeF.Height / 2);
              break;
            case ContentAlignment.TopLeft:
            case ContentAlignment.TopCenter:
            case ContentAlignment.TopRight:
              rectangle1 = new Rectangle(rectangle1.X, rectangle1.Y + (int) sizeF.Height + num, rectangle1.Width, rectangle1.Height - (int) sizeF.Height - num);
              break;
          }
          stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
          e.Graphics.DrawString(this.DescriptionText, this.DescriptionTextFont, (Brush) solidBrush, (RectangleF) rectangle2, stringFormat);
        }
      }
      using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
      {
        if (this.UseThemeTextColor)
          solidBrush.Color = this.backFill.Theme.StyleNormal.TextColor;
        e.Graphics.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) rectangle1, format);
      }
      return rectangle1;
    }

    private void dropDown_Opening(object sender, CancelEventArgs e)
    {
      this.opened = true;
    }

    private void dropDown_Opened(object sender, EventArgs e)
    {
      this.opened = true;
      this.controlState = ControlState.Pressed;
      this.Invalidate();
    }

    private void dropDown_Closed(object sender, ToolStripDropDownClosedEventArgs e)
    {
      this.opened = false;
      this.controlState = ControlState.Normal;
      this.Invalidate();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      if (this.dropDown.Bounds.Contains(Cursor.Position) || this.AnyOpenedChildMenu(this.Items))
        return;
      this.CloseDropDown();
    }

    private void dropDown_MouseLeave(object sender, EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.timer.Stop();
      this.timer.Start();
    }

    public bool AnyOpenedChildMenu(vApplicationMenuItemCollection items)
    {
      bool flag = false;
      foreach (vApplicationMenuItem applicationMenuItem in items)
      {
        if (applicationMenuItem.opened)
          return true;
        flag = this.AnyOpenedChildMenu(applicationMenuItem.Items);
      }
      return flag;
    }
  }
}
