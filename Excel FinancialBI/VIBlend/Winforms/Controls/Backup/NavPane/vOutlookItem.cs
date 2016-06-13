// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vOutlookItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vOutlookNavPane item</summary>
  [Designer("VIBlend.WinForms.Controls.Design.OutlookItemDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(false)]
  public class vOutlookItem : Control, INotifyPropertyChanged
  {
    private int headerHeight = 30;
    private bool menuItemVisible = true;
    private bool useThemeBackground = true;
    private bool useThemeTextColor = true;
    private Color backgroundTextColor = Color.Black;
    private Color highlightTextColor = Color.Black;
    private Color selectedTextColor = Color.Black;
    private Color disabledTextColor = Color.Silver;
    private bool autoHide = true;
    private Color backgroundBorder = Color.Black;
    private Color highlightBackgroundBorder = Color.Black;
    private Color selectedBackgroundBorder = Color.Black;
    private Color disabledBackgroundBorder = Color.Silver;
    private Brush disabledBackgroundBrush = (Brush) new SolidBrush(Color.Silver);
    private Brush backgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush selectedBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush highlightBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private string tooltipText = "";
    private int largeImageIndex = -1;
    private int smallImageIndex = -1;
    private TextImageRelation textImageRelation = TextImageRelation.ImageBeforeText;
    private ContentAlignment textAlign = ContentAlignment.MiddleLeft;
    private ContentAlignment imageAlign = ContentAlignment.MiddleLeft;
    private vOutlookHeader header;
    private bool isExpanded;
    private Panel panel;
    internal vOutlookNavPane navPane;
    private Image largeImage;
    private Image smallImage;
    private Font headerFont;

    /// <summary>
    /// Gets or sets a value indicating whether the auto-scroll is behavior.
    /// </summary>
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the auto-scroll is behavior.")]
    [Category("Behavior")]
    public bool AutoScroll
    {
      get
      {
        return this.panel.AutoScroll;
      }
      set
      {
        this.panel.AutoScroll = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the menu item associated to the item can be displayed.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the menu item associated to the item can be displayed.")]
    public bool MenuItemVisible
    {
      get
      {
        return this.menuItemVisible;
      }
      set
      {
        this.menuItemVisible = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use theme's background")]
    public bool UseThemeBackground
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
        this.OnPropertyChanged("UseThemeBackground");
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's text color
    /// </summary>
    [Description("Gets or sets a value indicating whether to use theme's text color")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool UseThemeTextColor
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
        this.OnPropertyChanged("UseThemeTextColor");
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>Gets or sets the  TextColor.</summary>
    /// <value>The  TextColor.</value>
    [Category("Appearance")]
    [Description("Gets or sets the  Text color.")]
    [DefaultValue(typeof (Color), "Black")]
    public Color TextColor
    {
      get
      {
        return this.backgroundTextColor;
      }
      set
      {
        this.backgroundTextColor = value;
        if (this.HeaderControl != null)
          this.HeaderControl.Invalidate();
        this.OnPropertyChanged("TextColor");
      }
    }

    /// <summary>Gets or sets the highlight TextColor.</summary>
    /// <value>The highlight TextColor.</value>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the highlight TextColor.")]
    public Color HighlightTextColor
    {
      get
      {
        return this.highlightTextColor;
      }
      set
      {
        this.highlightTextColor = value;
        if (this.HeaderControl != null)
          this.HeaderControl.Invalidate();
        this.OnPropertyChanged("HighlightTextColor");
      }
    }

    /// <summary>Gets or sets the selected TextColor.</summary>
    /// <value>The selected TextColor.</value>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the selected TextColor.")]
    public Color SelectedTextColor
    {
      get
      {
        return this.selectedTextColor;
      }
      set
      {
        this.selectedTextColor = value;
        if (this.HeaderControl != null)
          this.HeaderControl.Invalidate();
        this.OnPropertyChanged("SelectedTextColor");
      }
    }

    /// <summary>Gets or sets the disabledTextColor TextColor.</summary>
    /// <value>The disabledTextColor TextColor.</value>
    [DefaultValue(typeof (Color), "Silver")]
    [Category("Appearance")]
    [Description("Gets or sets the DisabledTextColor TextColor.")]
    public Color DisabledTextColor
    {
      get
      {
        return this.disabledTextColor;
      }
      set
      {
        this.disabledTextColor = value;
        if (this.HeaderControl != null)
          this.HeaderControl.Invalidate();
        this.OnPropertyChanged("DisabledTextColor");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the item can be hidden by resizing the resize splitter.
    /// </summary>
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether the item can be hidden by resizing the resize splitter.")]
    [Category("Behavior")]
    public bool AutoHide
    {
      get
      {
        return this.autoHide;
      }
      set
      {
        if (value == this.autoHide)
          return;
        this.autoHide = value;
        this.OnPropertyChanged("AutoHide");
      }
    }

    /// <summary>Gets or sets the background border.</summary>
    /// <value>The background border.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    [Description("Gets or sets the background border.")]
    public Color BackgroundBorder
    {
      get
      {
        return this.backgroundBorder;
      }
      set
      {
        this.backgroundBorder = value;
        this.OnPropertyChanged("BackgroundBorder");
      }
    }

    /// <summary>Gets or sets the highlightBackground border.</summary>
    /// <value>The highlightBackground border.</value>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the highlightBackground border.")]
    public Color HighlightBackgroundBorder
    {
      get
      {
        return this.highlightBackgroundBorder;
      }
      set
      {
        this.highlightBackgroundBorder = value;
        this.OnPropertyChanged("HighlightBackgroundBorder");
      }
    }

    /// <summary>Gets or sets the selectedBackground border.</summary>
    /// <value>The selectedBackground border.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    [Description("Gets or sets the selectedBackground border.")]
    public Color SelectedBackgroundBorder
    {
      get
      {
        return this.selectedBackgroundBorder;
      }
      set
      {
        this.selectedBackgroundBorder = value;
        this.OnPropertyChanged("SelectedBackgroundBorder");
      }
    }

    /// <summary>Gets or sets the disabledBackgroundBorder border.</summary>
    /// <value>The disabledBackgroundBorder border.</value>
    [Description("Gets or sets the DisabledBackgroundBorder border.")]
    [DefaultValue(typeof (Color), "Silver")]
    [Category("Appearance")]
    public Color DisabledBackgroundBorder
    {
      get
      {
        return this.disabledBackgroundBorder;
      }
      set
      {
        this.disabledBackgroundBorder = value;
        this.OnPropertyChanged("DisabledBackgroundBorder");
      }
    }

    /// <summary>Gets or sets the disabled background brush.</summary>
    /// <value>The disabled background brush.</value>
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the disabled background brush.")]
    public Brush DisabledBackgroundBrush
    {
      get
      {
        return this.disabledBackgroundBrush;
      }
      set
      {
        this.disabledBackgroundBrush = value;
        this.OnPropertyChanged("DisabledBackgroundBrush");
      }
    }

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Description("Gets or sets the background brush.")]
    [Browsable(false)]
    [Category("Appearance")]
    public Brush BackgroundBrush
    {
      get
      {
        return this.backgroundBrush;
      }
      set
      {
        this.backgroundBrush = value;
        this.OnPropertyChanged("BackgroundBrush");
      }
    }

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Description("Gets or sets the selected background brush.")]
    [Browsable(false)]
    [Category("Appearance")]
    public Brush SelectedBackgroundBrush
    {
      get
      {
        return this.selectedBackgroundBrush;
      }
      set
      {
        this.selectedBackgroundBrush = value;
        this.OnPropertyChanged("SelectedBackgroundBrush");
      }
    }

    /// <summary>Gets or sets the HighlightBackground brush.</summary>
    /// <value>The HighlightBackground brush.</value>
    [Category("Appearance")]
    [Description("Gets or sets the HighlightBackground brush.")]
    [Browsable(false)]
    public Brush HighlightBackgroundBrush
    {
      get
      {
        return this.highlightBackgroundBrush;
      }
      set
      {
        this.highlightBackgroundBrush = value;
        this.OnPropertyChanged("HighlightBackgroundBrush");
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new Color ForeColor
    {
      get
      {
        return base.ForeColor;
      }
      set
      {
        base.ForeColor = value;
      }
    }

    /// <summary>
    /// Gets a reference to the NavigationPane control which hosts the item
    /// </summary>
    [Browsable(false)]
    public vOutlookNavPane NavPane
    {
      get
      {
        if (this.navPane != null)
          return this.navPane;
        for (; this.Parent != null; this.Parent = this.Parent.Parent)
        {
          if (this.Parent is vOutlookNavPane)
            return (vOutlookNavPane) this.Parent;
        }
        return (vOutlookNavPane) null;
      }
    }

    /// <summary>Gets or sets the tooltip text of the item</summary>
    [Category("Behavior")]
    [Description("Gets or sets the tooltip text of the item.")]
    public string TooltipText
    {
      get
      {
        if (string.IsNullOrEmpty(this.tooltipText))
          this.tooltipText = this.Text;
        return this.tooltipText;
      }
      set
      {
        if (value == null)
          this.tooltipText = "";
        else
          this.tooltipText = value;
      }
    }

    /// <summary>
    /// Gets or sets the index of the Large image to display on the item's header
    /// </summary>
    [Description("Gets or sets the index of the Large image to display on the item's header.")]
    [Category("Appearance")]
    [DefaultValue(-1)]
    public virtual int LargeImageIndex
    {
      get
      {
        return this.largeImageIndex;
      }
      set
      {
        this.largeImageIndex = value;
        this.largeImage = (Image) null;
      }
    }

    /// <summary>
    /// Gets or sets the Large image to display on the item's header
    /// </summary>
    [DefaultValue(null)]
    [Description("Gets or sets the Large image to display on the item's header.")]
    [Category("Appearance")]
    public Image LargeImage
    {
      get
      {
        if (this.largeImage != null)
          return this.largeImage;
        if (this.NavPane != null && this.NavPane.LargeImageList != null && (this.largeImageIndex >= 0 && this.largeImageIndex < this.NavPane.LargeImageList.Images.Count))
          return this.NavPane.LargeImageList.Images[this.largeImageIndex];
        return (Image) null;
      }
      set
      {
        this.largeImage = value;
        this.largeImageIndex = -1;
      }
    }

    /// <summary>
    /// Gets or sets the index of the small image to display on the item's header
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the index of the small image to display on the item's header.")]
    [DefaultValue(-1)]
    public int SmallImageIndex
    {
      get
      {
        return this.smallImageIndex;
      }
      set
      {
        this.smallImageIndex = value;
        this.smallImage = (Image) null;
      }
    }

    /// <summary>
    /// Gets or sets the Large image to display on the item's header.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(null)]
    [Description("Gets or sets the Large image to display on the item's header.")]
    public Image SmallImage
    {
      get
      {
        if (this.smallImage != null)
          return this.smallImage;
        if (this.NavPane != null && this.NavPane.SmallImageList != null && (this.smallImageIndex >= 0 && this.smallImageIndex < this.NavPane.SmallImageList.Images.Count))
          return this.NavPane.SmallImageList.Images[this.smallImageIndex];
        return (Image) null;
      }
      set
      {
        this.smallImage = value;
        this.smallImageIndex = -1;
      }
    }

    /// <summary>Gets or sets the height of the item's header</summary>
    [Description("Gets or sets the height of the item's header")]
    [Category("Appearance")]
    [DefaultValue(30)]
    public int HeaderHeight
    {
      get
      {
        return this.headerHeight;
      }
      set
      {
        this.headerHeight = value;
        this.HeaderControl.Height = value;
        this.Height = value;
        this.OnPropertyChanged("HeaderHeight");
      }
    }

    /// <summary>Gets or sets the header text and image relation.</summary>
    /// <value>The header text and image relation.</value>
    [Category("Appearance")]
    [Description("Gets or sets the header text and image relation.")]
    public TextImageRelation HeaderTextImageRelation
    {
      get
      {
        return this.textImageRelation;
      }
      set
      {
        this.textImageRelation = value;
        this.OnPropertyChanged("HeaderTextImageRelation");
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>Gets or sets the text alignment.</summary>
    /// <value>The text alignment.</value>
    [Category("Appearance")]
    [Description("Gets or sets the header's text alignment.")]
    public ContentAlignment HeaderTextAlignment
    {
      get
      {
        return this.textAlign;
      }
      set
      {
        this.textAlign = value;
        this.OnPropertyChanged("HeaderTextAlignment");
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>Gets or sets the image alignment.</summary>
    /// <value>The image alignment.</value>
    [Description("Gets or sets the header's image alignment.")]
    [Category("Appearance")]
    public ContentAlignment HeaderImageAlignment
    {
      get
      {
        return this.imageAlign;
      }
      set
      {
        this.imageAlign = value;
        this.OnPropertyChanged("HeaderImageAlignment");
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>Gets or sets the text to display on the item's header</summary>
    [Category("Appearance")]
    [Description("Gets or sets the text to display on the item's header.")]
    [DefaultValue("HeaderText")]
    public string HeaderText
    {
      get
      {
        return this.HeaderControl.Text;
      }
      set
      {
        this.HeaderControl.Text = value;
        if (this.IsExpanded && this.NavPane != null)
          this.NavPane.header.Text = value;
        this.TooltipText = value;
        this.OnPropertyChanged("HeaderText");
      }
    }

    /// <summary>
    /// Gets or sets the font of the text to display in the item's header area
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the font of the text to display in the item's header area")]
    public Font HeaderFont
    {
      get
      {
        return this.headerFont;
      }
      set
      {
        this.headerFont = value;
      }
    }

    /// <summary>
    /// Gets a reference to the panel which hosts the controls within the navigation pane item
    /// </summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public Panel Panel
    {
      get
      {
        return this.panel;
      }
      internal set
      {
        this.panel = value;
      }
    }

    /// <summary>Checks if the item is currently in expanded state</summary>
    [Browsable(false)]
    public bool IsExpanded
    {
      get
      {
        return this.isExpanded;
      }
      internal set
      {
        this.isExpanded = value;
      }
    }

    /// <summary>Gets the header control.</summary>
    /// <value>The header control.</value>
    [Browsable(false)]
    public vOutlookHeader HeaderControl
    {
      get
      {
        return this.header;
      }
    }

    /// <summary>Occurs when a property of the item changes</summary>
    [Description("Occurs when a property is changed.")]
    [Category("Action")]
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vOutlookItem" /> class.
    /// </summary>
    public vOutlookItem()
    {
      this.panel = new Panel();
      this.panel.AutoScroll = true;
      this.header = new vOutlookHeader(this);
      this.Controls.Add((Control) this.header);
      this.header.Dock = DockStyle.Fill;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.header.MouseMove += new MouseEventHandler(this.header_MouseMove);
      this.Visible = true;
      this.panel.BackColor = Color.White;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (this.NavPane == null || this.NavPane.StatusItem == null)
        return;
      this.NavPane.StatusItem.RefreshStatus();
    }

    internal void CallOnClick()
    {
      this.OnClick(EventArgs.Empty);
    }

    /// <summary>Raises the Layout event.</summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.header == null)
        return;
      this.header.Bounds = new Rectangle(0, 0, this.Width, this.HeaderHeight);
    }

    private void ResetHeaderHeaderTextImageRelation()
    {
      this.HeaderTextImageRelation = TextImageRelation.ImageBeforeText;
    }

    private bool ShouldSerializeHeaderTextImageRelation()
    {
      return this.HeaderTextImageRelation != TextImageRelation.ImageBeforeText;
    }

    private void ResetHeaderTextAlignment()
    {
      this.HeaderTextAlignment = ContentAlignment.MiddleLeft;
    }

    private bool ShouldSerializeHeaderTextAlignment()
    {
      return this.HeaderTextAlignment != ContentAlignment.MiddleLeft;
    }

    private void ResetHeaderImageAlignment()
    {
      this.HeaderImageAlignment = ContentAlignment.MiddleLeft;
    }

    private bool ShouldSerializeHeaderImageAlignment()
    {
      return this.HeaderImageAlignment != ContentAlignment.MiddleLeft;
    }

    private void ResetHeaderFont()
    {
      this.HeaderFont = Control.DefaultFont;
    }

    private bool ShouldSerializeHeaderFont()
    {
      if (this.HeaderFont == null)
        return false;
      return !this.HeaderFont.Equals((object) Control.DefaultFont);
    }

    /// <summary>Raises the property changed event</summary>
    /// <param name="name">The name of the item property</param>
    protected internal virtual void OnPropertyChanged(string name)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(name));
    }

    /// <summary>Raises the MouseMove event.</summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
    }

    /// <summary>Raises the Paint event.</summary>
    /// <param name="e">A PaintEventArgs that contains the event data. </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      Rectangle rect = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height);
      this.HeaderControl.Width = this.ClientRectangle.Width - 1;
      using (Pen pen = new Pen(this.HeaderControl.backFill.BorderColor))
        e.Graphics.DrawRectangle(pen, rect);
    }

    private void header_MouseMove(object sender, MouseEventArgs e)
    {
    }
  }
}
