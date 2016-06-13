// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vNavPaneItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vNavPane item</summary>
  [ToolboxItem(false)]
  [Designer("VIBlend.WinForms.Controls.Design.NavPaneItemDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vNavPaneItem : Control, INotifyPropertyChanged
  {
    private int captionHeight = 30;
    private string tooltipText = "";
    private TextImageRelation textImageRelation = TextImageRelation.ImageBeforeText;
    private ContentAlignment textAlign = ContentAlignment.MiddleLeft;
    private ContentAlignment imageAlign = ContentAlignment.MiddleLeft;
    private int imageIndex = -1;
    private bool useThemeBackground = true;
    private bool useThemeTextColor = true;
    private Color backgroundTextColor = Color.Black;
    private Color highlightTextColor = Color.Black;
    private Color selectedTextColor = Color.Black;
    private Color disabledTextColor = Color.Silver;
    private Color backgroundBorder = Color.Black;
    private Color highlightBackgroundBorder = Color.Black;
    private Color selectedBackgroundBorder = Color.Black;
    private Color disabledBackgroundBorder = Color.Silver;
    private Brush disabledBackgroundBrush = (Brush) new SolidBrush(Color.Silver);
    private Brush backgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush selectedBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush highlightBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private vNavPaneHeader header;
    private bool isExpanded;
    internal int wantedY;
    internal int wantedHeight;
    private Panel panel;
    private Font headerFont;
    private Image image;

    /// <summary>
    /// Gets a reference to the panel which hosts the controls within the navigation pane item
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public Panel ItemPanel
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

    /// <summary>Gets or sets the tooltip text of the item.</summary>
    [Description("Gets or sets the tooltip text of the NavigationPane item.")]
    [Category("Appearance")]
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
        this.tooltipText = value != null ? value : "";
        this.OnPropertyChanged("TooltipText");
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
    [Category("Appearance")]
    [Description("Gets or sets the header's image alignment.")]
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

    /// <summary>
    /// Gets or sets the text to display on the item's header area
    /// </summary>
    [DefaultValue("HeaderText")]
    [Description("Gets or sets the text to display on the item's header.")]
    [Category("Appearance")]
    public string HeaderText
    {
      get
      {
        return this.header.Text;
      }
      set
      {
        this.header.Text = value;
        this.TooltipText = value;
        this.header.Invalidate();
        this.OnPropertyChanged("HeaderText");
      }
    }

    /// <summary>
    /// Gets or sets the font of the text to display in the item's header area
    /// </summary>
    [Description("Gets or sets the font of the text to display in the item's header area")]
    [Category("Appearance")]
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
    /// Gets or sets the index of the image to display on the item's header
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(-1)]
    [Description("Gets or sets the index of the image to display on the item's header.")]
    public int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        this.imageIndex = value;
        this.OnPropertyChanged("ImageIndex");
      }
    }

    /// <summary>
    /// Gets or sets the image to display on the item's header
    /// </summary>
    [Description("Gets or sets the image to display on the item's header.")]
    [Category("Appearance")]
    [DefaultValue(null)]
    public Image Image
    {
      get
      {
        if (this.image != null)
          return this.image;
        if (this.NavPane != null && this.NavPane.ImageList != null && (this.imageIndex >= 0 && this.imageIndex < this.NavPane.ImageList.Images.Count))
          return this.NavPane.ImageList.Images[this.imageIndex];
        return (Image) null;
      }
      set
      {
        this.image = value;
        this.imageIndex = -1;
        this.OnPropertyChanged("Image");
      }
    }

    /// <summary>
    /// Gets or sets the height of the header area of the item
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(30)]
    [Description("Gest or sets the height of the item's header.")]
    public int HeaderHeight
    {
      get
      {
        return this.captionHeight;
      }
      set
      {
        this.captionHeight = value;
        this.header.Height = value;
        this.OnPropertyChanged("HeaderHeight");
      }
    }

    /// <summary>Gets or sets the theme of the item's header.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public ControlTheme HeaderTheme
    {
      get
      {
        return this.HeaderControl.Theme;
      }
      set
      {
        this.HeaderControl.Theme = value;
        this.OnPropertyChanged("HeaderTheme");
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [Description("Gets or sets a value indicating whether to use theme's background")]
    [DefaultValue(true)]
    [Category("Behavior")]
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
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use theme's text color")]
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
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the  TextColor.")]
    [Category("Appearance")]
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
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
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
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
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
      }
    }

    /// <summary>Gets or sets the highlightBackground border.</summary>
    /// <value>The highlightBackground border.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the highlightBackground border.")]
    [Category("Appearance")]
    public Color HighlightBackgroundBorder
    {
      get
      {
        return this.highlightBackgroundBorder;
      }
      set
      {
        this.highlightBackgroundBorder = value;
      }
    }

    /// <summary>Gets or sets the selectedBackground border.</summary>
    /// <value>The selectedBackground border.</value>
    [Category("Appearance")]
    [Description("Gets or sets the selectedBackground border.")]
    [DefaultValue(typeof (Color), "Black")]
    public Color SelectedBackgroundBorder
    {
      get
      {
        return this.selectedBackgroundBorder;
      }
      set
      {
        this.selectedBackgroundBorder = value;
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
      }
    }

    /// <summary>Gets or sets the disabled background brush.</summary>
    /// <value>The disabled background brush.</value>
    [Category("Appearance")]
    [Browsable(false)]
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
      }
    }

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the selected background brush.")]
    public Brush SelectedBackgroundBrush
    {
      get
      {
        return this.selectedBackgroundBrush;
      }
      set
      {
        this.selectedBackgroundBrush = value;
      }
    }

    /// <summary>Gets or sets the HighlightBackground brush.</summary>
    /// <value>The HighlightBackground brush.</value>
    [Description("Gets or sets the HighlightBackground brush.")]
    [Browsable(false)]
    [Category("Appearance")]
    public Brush HighlightBackgroundBrush
    {
      get
      {
        return this.highlightBackgroundBrush;
      }
      set
      {
        this.highlightBackgroundBrush = value;
      }
    }

    /// <summary>
    /// Gets a reference to the NavigationPane control which hosts the item
    /// </summary>
    [Browsable(false)]
    public vNavPane NavPane
    {
      get
      {
        for (; this.Parent != null; this.Parent = this.Parent.Parent)
        {
          if (this.Parent is vNavPane)
            return (vNavPane) this.Parent;
        }
        return (vNavPane) null;
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
        this.OnPropertyChanged("IsExpanded");
      }
    }

    /// <summary>Gets the header control.</summary>
    /// <value>The header control.</value>
    [Browsable(false)]
    public vNavPaneHeader HeaderControl
    {
      get
      {
        return this.header;
      }
    }

    /// <summary>Occurs when a property value is changed.</summary>
    [Category("Action")]
    [Description("Occurs when a property value is changed.")]
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vNavPaneItem" /> class.
    /// </summary>
    public vNavPaneItem()
    {
      this.header = new vNavPaneHeader(this);
      this.Controls.Add((Control) this.header);
      this.panel = new Panel();
      this.panel.AutoScroll = true;
      this.Controls.Add((Control) this.panel);
      this.BackColor = Color.White;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
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
      if (this.panel == null)
        return;
      this.panel.Bounds = new Rectangle(1, this.HeaderHeight, this.Width - 2, this.Height - this.HeaderHeight - 1);
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
      base.OnPaint(e);
      Rectangle rect = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height);
      this.header.Bounds = new Rectangle(0, 0, this.Width, this.HeaderHeight);
      bool flag = false;
      if (this.NavPane.Items.IndexOf(this) == this.NavPane.Items.Count - 1)
        flag = true;
      if (flag)
        rect = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      using (Pen pen = new Pen(this.HeaderControl.backFill.BorderColor))
        e.Graphics.DrawRectangle(pen, rect);
    }

    /// <summary>Called when a property is changed.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Releases all resources used by the <see cref="T:System.ComponentModel.Component" />.
    /// </summary>
    public new void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      if (this.DesignMode)
        base.Dispose(disposing);
      else
        base.Dispose(disposing);
    }

    /// <summary>Clears this instance.</summary>
    public void Clear()
    {
      try
      {
        if (this.Controls.Count > 0)
          this.Controls.Clear();
        if (this.header != null)
          this.header = (vNavPaneHeader) null;
        if (this.panel == null)
          return;
        this.panel.Controls.Clear();
        this.panel = (Panel) null;
      }
      catch (Exception ex)
      {
      }
    }
  }
}
