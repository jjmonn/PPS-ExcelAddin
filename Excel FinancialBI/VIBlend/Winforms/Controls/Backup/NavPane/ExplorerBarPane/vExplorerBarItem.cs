// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vExplorerBarItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vNavPane item</summary>
  [Designer("VIBlend.WinForms.Controls.Design.vExplorerBarItemDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(false)]
  public class vExplorerBarItem : Control
  {
    private int captionHeight = 30;
    private int expandedHeight = 100;
    private Timer timer = new Timer();
    private int duration = 10;
    private Stack<int> animValues = new Stack<int>();
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
    private string tooltipText = "";
    private TextImageRelation textImageRelation = TextImageRelation.ImageBeforeText;
    private ContentAlignment textAlign = ContentAlignment.MiddleLeft;
    private ContentAlignment imageAlign = ContentAlignment.MiddleLeft;
    private int imageIndex = -1;
    private vExplorerBarItemHeader header;
    private bool isExpanded;
    private int step;
    private Panel panel;
    private Font headerFont;
    private Image image;

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to use theme's background")]
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
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's text color
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use theme's text color")]
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
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>Gets or sets the  TextColor.</summary>
    /// <value>The  TextColor.</value>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the  TextColor.")]
    public Color TextColor
    {
      get
      {
        return this.backgroundTextColor;
      }
      set
      {
        this.backgroundTextColor = value;
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>Gets or sets the highlight TextColor.</summary>
    /// <value>The highlight TextColor.</value>
    [Category("Appearance")]
    [Description("Gets or sets the highlight TextColor.")]
    [DefaultValue(typeof (Color), "Black")]
    public Color HighlightTextColor
    {
      get
      {
        return this.highlightTextColor;
      }
      set
      {
        this.highlightTextColor = value;
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
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
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
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
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>Gets or sets the background border.</summary>
    /// <value>The background border.</value>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
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
    [Category("Appearance")]
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
      }
    }

    /// <summary>Gets or sets the selectedBackground border.</summary>
    /// <value>The selectedBackground border.</value>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
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
      }
    }

    /// <summary>Gets or sets the disabledBackgroundBorder border.</summary>
    /// <value>The disabledBackgroundBorder border.</value>
    [DefaultValue(typeof (Color), "Silver")]
    [Category("Appearance")]
    [Description("Gets or sets the DisabledBackgroundBorder border.")]
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
    [Description("Gets or sets the disabled background brush.")]
    [Category("Appearance")]
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
    [Browsable(false)]
    [Description("Gets or sets the background brush.")]
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
    [Category("Appearance")]
    [Browsable(false)]
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
    [Category("Appearance")]
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
      }
    }

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

    /// <summary>Gets or sets the tooltip text of the tab page.</summary>
    [Description("Gets or sets the tooltip text of the tab page.")]
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
        if (value == null)
          this.tooltipText = "";
        else
          this.tooltipText = value;
      }
    }

    /// <summary>Gets or sets the header text and image relation.</summary>
    /// <value>The header text and image relation.</value>
    [Description("Gets or sets the header text and image relation.")]
    [Category("Appearance")]
    public TextImageRelation HeaderTextImageRelation
    {
      get
      {
        return this.textImageRelation;
      }
      set
      {
        this.textImageRelation = value;
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
        if (this.HeaderControl == null)
          return;
        this.HeaderControl.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the text to display on the item's header area
    /// </summary>
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
    [Description("Gets or sets the index of the image to display on the item's header.")]
    [DefaultValue(-1)]
    [Category("Appearance")]
    public int ImageIndex
    {
      get
      {
        return this.imageIndex;
      }
      set
      {
        if (value == this.imageIndex)
          return;
        this.imageIndex = value;
      }
    }

    /// <summary>
    /// Gets or sets the image to display on the item's header
    /// </summary>
    [DefaultValue(null)]
    [Category("Appearance")]
    [Description("Gets or sets the image to display on the item's header.")]
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
      }
    }

    /// <summary>Gets or sets the height when the item is epxanded.</summary>
    [Description("Gets or sets the height when the item is epxanded.")]
    [Category("Appearance")]
    [DefaultValue(100)]
    public int ExpandedHeight
    {
      get
      {
        return this.expandedHeight;
      }
      set
      {
        this.expandedHeight = value;
        this.Height = value;
      }
    }

    /// <summary>
    /// Gets or sets the height of the header area of the item
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the height of the item's header.")]
    [DefaultValue(30)]
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
      }
    }

    /// <summary>Gets or sets the theme of the item's header.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ControlTheme HeaderTheme
    {
      get
      {
        return this.HeaderControl.Theme;
      }
      set
      {
        this.HeaderControl.Theme = value;
      }
    }

    /// <summary>
    /// Gets a reference to the NavigationPane control which hosts the item
    /// </summary>
    [Browsable(false)]
    public vExplorerBar NavPane
    {
      get
      {
        for (; this.Parent != null; this.Parent = this.Parent.Parent)
        {
          if (this.Parent is vExplorerBar)
            return (vExplorerBar) this.Parent;
        }
        return (vExplorerBar) null;
      }
    }

    /// <summary>
    /// Gets or sets if the item is currently in expanded state
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets if the item is currently in expanded state")]
    public bool Expanded
    {
      get
      {
        return this.isExpanded;
      }
      set
      {
        if (this.isExpanded == value)
          return;
        if (this.NavPane != null)
        {
          vExplorerBarCancelEventArgs args = new vExplorerBarCancelEventArgs(this);
          this.NavPane.OnExpandedChanging(args);
          if (args.Cancel)
            return;
        }
        CancelEventArgs e = new CancelEventArgs();
        this.OnExpandedChanging(e);
        if (e.Cancel)
          return;
        this.isExpanded = value;
        if (value)
        {
          if (this.DesignMode)
            this.Height = this.ExpandedHeight;
          else if (!this.NavPane.EnableToggleAnimation)
            this.Height = this.ExpandedHeight;
          else
            this.Animate();
        }
        else if (!this.NavPane.EnableToggleAnimation)
          this.Height = this.HeaderHeight;
        else
          this.Animate();
        this.OnExpandedChanged(EventArgs.Empty);
        if (this.NavPane == null)
          return;
        this.NavPane.OnExpandedChanged(new vExplorerBarEventArgs(this));
      }
    }

    /// <summary>Gets the header control.</summary>
    /// <value>The header control.</value>
    [Browsable(false)]
    public vExplorerBarItemHeader HeaderControl
    {
      get
      {
        return this.header;
      }
    }

    /// <summary>Occurs when expanded property is changed.</summary>
    [Category("Action")]
    public event EventHandler ExpandedChanged;

    /// <summary>Occurs when expanded property is changing.</summary>
    [Category("Action")]
    public event CancelEventHandler ExpandedChanging;

    /// <summary>Constructor</summary>
    public vExplorerBarItem()
    {
      this.ControlAdded += new ControlEventHandler(this.NavPaneItem_ControlAdded);
      this.ControlRemoved += new ControlEventHandler(this.NavPaneItem_ControlRemoved);
      this.header = new vExplorerBarItemHeader(this);
      this.Controls.Add((Control) this.header);
      this.panel = new Panel();
      this.panel.AutoScroll = true;
      this.Controls.Add((Control) this.panel);
      this.BackColor = Color.White;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.header.MouseMove += new MouseEventHandler(this.header_MouseMove);
      this.header.MouseDoubleClick += new MouseEventHandler(this.header_MouseDoubleClick);
      this.header.MouseClick += new MouseEventHandler(this.header_MouseClick);
      this.timer.Interval = 10;
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.Margin = new Padding();
    }

    private void Animate()
    {
      this.timer.Start();
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.timer.Dispose();
      base.Dispose(disposing);
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      double num = (double) this.captionHeight + (double) this.step * (double) this.step / (double) this.duration;
      this.step += 2;
      this.ItemPanel.AutoScroll = false;
      if (!this.Expanded)
      {
        if (this.animValues.Count > 0)
        {
          this.Size = new Size(this.Width, this.animValues.Pop());
        }
        else
        {
          this.timer.Stop();
          this.Size = new Size(this.Width, this.captionHeight);
          this.step = 0;
          this.ItemPanel.AutoScroll = true;
        }
      }
      else if (this.Height < this.expandedHeight)
      {
        int height = (int) num;
        this.Size = new Size(this.Width, height);
        this.animValues.Push(height);
      }
      else
      {
        this.ItemPanel.AutoScroll = true;
        this.Size = new Size(this.Width, this.expandedHeight);
        this.timer.Stop();
        this.step = 0;
      }
      this.Invalidate();
    }

    private void header_MouseClick(object sender, MouseEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      this.Expanded = !this.Expanded;
      this.CallOnClick();
    }

    private void header_MouseDoubleClick(object sender, MouseEventArgs e)
    {
    }

    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
      this.Expanded = !this.Expanded;
    }

    internal void CallOnClick()
    {
      this.OnClick(EventArgs.Empty);
    }

    protected virtual void OnExpandedChanging(CancelEventArgs e)
    {
      if (this.ExpandedChanging == null)
        return;
      this.ExpandedChanging((object) this, e);
    }

    protected virtual void OnExpandedChanged(EventArgs e)
    {
      if (this.ExpandedChanged == null)
        return;
      this.ExpandedChanged((object) this, e);
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

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      if (this.DesignMode || !this.Expanded || (this.timer.Enabled || this.Height == this.ExpandedHeight))
        return;
      this.Height = this.ExpandedHeight;
    }

    /// <summary>Raises the MouseMove event.</summary>
    /// <param name="e">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
    }

    private void header_MouseMove(object sender, MouseEventArgs e)
    {
    }

    /// <summary>Raises the Paint event.</summary>
    /// <param name="e">A PaintEventArgs that contains the event data. </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      Rectangle rect = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height);
      this.header.Bounds = new Rectangle(0, 0, this.Width, this.HeaderHeight);
      bool flag = true;
      if (this.Expanded)
        flag = false;
      if (this.NavPane.Items.IndexOf(this) == this.NavPane.Items.Count - 1)
        flag = true;
      if (flag)
        rect = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      using (Pen pen = new Pen(this.HeaderControl.backFill.BorderColor))
        e.Graphics.DrawRectangle(pen, rect);
    }

    private void NavPaneItem_ControlRemoved(object sender, ControlEventArgs e)
    {
    }

    private void NavPaneItem_ControlAdded(object sender, ControlEventArgs e)
    {
    }
  }
}
