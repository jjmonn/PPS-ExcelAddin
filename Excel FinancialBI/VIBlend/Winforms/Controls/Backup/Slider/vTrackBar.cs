// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTrackBar
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vTrackBar control.</summary>
  /// <remarks>
  /// vTrackBar displays a slider which allows the user to indicate the progress of an operation. TrackBar controls are often called sliders.
  /// </remarks>
  [Designer("VIBlend.WinForms.Controls.Design.vTrackControlsDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [DefaultEvent("ValueChanged")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vTrackBar), "ControlIcons.vTrackBar.ico")]
  [Description("Displays a slider which allows the user to indicate the progress of an operation.")]
  public class vTrackBar : Control, IScrollableControlBase
  {
    private Size thumbSize = new Size(10, 15);
    private int maxPos = 100;
    private int smallChange = 1;
    private int largeChange = 5;
    private byte roundedCornersMask = 15;
    private int roundedCornersRadius = 1;
    private byte roundedCornersMaskThumb = 15;
    private int roundedCornersRadiusThumb = 1;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private Brush thumbBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush thumbHighlightBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush thumbPressedBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private bool useThemeBackground = true;
    private Color thumbBackgroundBorder = Color.Black;
    private Color thumbHighlightBackgroundBorder = Color.Black;
    private Color thumbPressedBackgroundBorder = Color.Black;
    private Color backgroundBorder = Color.Black;
    private Color progressBackgroundBorder = Color.Black;
    private Brush backgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush progressBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private PaintHelper helper = new PaintHelper();
    private ControlState state;
    private Rectangle thumbRect;
    private Rectangle barRect;
    private Orientation trackBarOrientation;
    private int currentPos;
    private int minPos;
    private BackgroundElement backFill;
    private BackgroundElement progressFill;
    private BackgroundElement thumbFill;
    private ControlTheme theme;
    private AnimationManager animManager;

    /// <summary>Gets or sets the size of the track bar's thumb.</summary>
    [Description("Gets or sets the size of the track bar's thumb.")]
    [Category("TrackBar.Appearance")]
    public Size ThumbSize
    {
      get
      {
        return this.thumbSize;
      }
      set
      {
        this.thumbSize = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the orientation of the track bar.</summary>
    [DefaultValue(Orientation.Horizontal)]
    [Description("Gets or sets the orientation of the track bar.")]
    [Category("Behavior")]
    public Orientation Orientation
    {
      get
      {
        return this.trackBarOrientation;
      }
      set
      {
        if (this.trackBarOrientation == value)
          return;
        this.trackBarOrientation = value;
        int width = this.Width;
        this.Width = this.Height;
        this.Height = width;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the value of the track bar control.</summary>
    [Category("Behavior")]
    [Description("Gets or sets the value of the track bar control.")]
    public int Value
    {
      get
      {
        return this.currentPos;
      }
      set
      {
        if (value >= this.minPos && value <= this.maxPos)
        {
          this.currentPos = value;
          if (this.ValueChanged != null)
            this.ValueChanged((object) this, new EventArgs());
          this.Invalidate();
        }
        else if (!this.DesignMode)
          throw new ArgumentOutOfRangeException("The value property is out of range");
      }
    }

    /// <summary>
    /// Gets or sets the Minimum value for the track bar's position.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(0)]
    [Description("Gets or sets the Minimum value for the track bar's position.")]
    public int Minimum
    {
      get
      {
        return this.minPos;
      }
      set
      {
        if (value < this.maxPos)
        {
          this.minPos = value;
          if (this.currentPos < this.minPos)
          {
            this.currentPos = this.minPos;
            if (this.ValueChanged != null)
              this.ValueChanged((object) this, new EventArgs());
          }
          this.Invalidate();
        }
        else if (!this.DesignMode)
          throw new ArgumentOutOfRangeException("The Minimum position must be lower than the Maximum");
      }
    }

    /// <summary>
    /// Gets or sets the Maximum value for the track bar's position.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the Maximum value for the track bar's position.")]
    [DefaultValue(100)]
    public int Maximum
    {
      get
      {
        return this.maxPos;
      }
      set
      {
        if (value > this.minPos)
        {
          this.maxPos = value;
          if (this.currentPos > this.maxPos)
          {
            this.currentPos = this.maxPos;
            if (this.ValueChanged != null)
              this.ValueChanged((object) this, new EventArgs());
          }
          this.Invalidate();
        }
        else if (!this.DesignMode)
          throw new ArgumentOutOfRangeException("The Maximum position must be greater than the Minimum position");
      }
    }

    /// <summary>
    /// Gets or sets the value added to or subtracted from the Value property when the thumb is moved a small distance.
    /// </summary>
    [Description("Gets or sets the value added to or subtracted from the Value property when the thumb is moved a small distance.")]
    [DefaultValue(1)]
    [Category("Behavior")]
    public int SmallChange
    {
      get
      {
        return this.smallChange;
      }
      set
      {
        this.smallChange = value;
      }
    }

    /// <summary>
    /// Gets or sets a value to be added to or subtracted from the Value property when the thumb is moved a large distance.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(5)]
    [Description("Gets or sets a value to be added to or subtracted from the Value property when the thumb is moved a large distance.")]
    public int LargeChange
    {
      get
      {
        return this.largeChange;
      }
      set
      {
        this.largeChange = value;
      }
    }

    /// <summary>
    /// Gets or sets the mask that determines which corners are rounded
    /// </summary>
    [Browsable(false)]
    public byte RoundedCornersMask
    {
      get
      {
        return this.roundedCornersMask;
      }
      set
      {
        this.roundedCornersMask = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the rounded corners radius.</summary>
    [Description("Gets or sets the rounded corners radius.")]
    [Category("TrackBar.Appearance")]
    [DefaultValue(1)]
    public int RoundedCornersRadius
    {
      get
      {
        return this.roundedCornersRadius;
      }
      set
      {
        if (value < 0)
          return;
        this.roundedCornersRadius = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the mask that determines which corners for the TrackBar's Thumb are rounded
    /// </summary>
    [Browsable(false)]
    public byte RoundedCornersMaskThumb
    {
      get
      {
        return this.roundedCornersMaskThumb;
      }
      set
      {
        this.roundedCornersMaskThumb = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the rounded corners radius of the TrackBar's Thumb.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the rounded corners radius of the TrackBar's Thumb.")]
    [DefaultValue(1)]
    public int RoundedCornersRadiusThumb
    {
      get
      {
        return this.roundedCornersRadiusThumb;
      }
      set
      {
        if (value < 0)
          return;
        this.roundedCornersRadiusThumb = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Browsable(false)]
    [Description("Gets or sets the theme of the control.")]
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
        this.theme = value.CreateCopy();
        ControlTheme copy = this.theme.CreateCopy();
        FillStyle fillStyle = this.theme.QueryFillStyleSetter("ProgressFill");
        if (fillStyle != null)
        {
          copy.StyleNormal.FillStyle = fillStyle;
          copy.StyleHighlight.FillStyle = fillStyle;
          copy.StylePressed.FillStyle = fillStyle;
        }
        if (this.Orientation == Orientation.Vertical)
        {
          this.theme.SetFillStyleGradientAngle(0);
          copy.SetFillStyleGradientAngle(0);
        }
        this.backFill.LoadTheme(this.theme);
        this.thumbFill.LoadTheme(this.theme);
        this.progressFill.LoadTheme(copy);
        this.thumbFill.IsAnimated = this.AllowAnimations;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Description("Gets or sets the theme of the control using one of the built-in themes.")]
    [Category("TrackBar.Appearance")]
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
      }
    }

    /// <summary>Gets or sets the thumb background brush.</summary>
    /// <value>The background brush.</value>
    [Description("Gets or sets the thumb background brush.")]
    [Category("TrackBar.Appearance")]
    [Browsable(false)]
    public Brush ThumbBackgroundBrush
    {
      get
      {
        return this.thumbBackgroundBrush;
      }
      set
      {
        this.thumbBackgroundBrush = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the thumb Highlight background brush.</summary>
    /// <value>The background brush.</value>
    [Browsable(false)]
    [Category("TrackBar.Appearance")]
    [Description("Gets or sets the thumb Highlight background brush.")]
    public Brush ThumbHighlightBackgroundBrush
    {
      get
      {
        return this.thumbHighlightBackgroundBrush;
      }
      set
      {
        this.thumbHighlightBackgroundBrush = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the thumb Pressed background brush.</summary>
    /// <value>The background brush.</value>
    [Category("TrackBar.Appearance")]
    [Browsable(false)]
    [Description("Gets or sets the thumbPressed background brush.")]
    public Brush ThumbPressedBackgroundBrush
    {
      get
      {
        return this.thumbPressedBackgroundBrush;
      }
      set
      {
        this.thumbPressedBackgroundBrush = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to use theme's background")]
    [Category("TrackBar.Appearance")]
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
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the background thumb border.</summary>
    /// <value>The thumb background border.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the thumb background border.")]
    [Category("TrackBar.Appearance")]
    public Color ThumbBackgroundBorder
    {
      get
      {
        return this.thumbBackgroundBorder;
      }
      set
      {
        this.thumbBackgroundBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the background thumb border.</summary>
    /// <value>The thumb background border.</value>
    [Description("Gets or sets the thumb background border.")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("TrackBar.Appearance")]
    public Color ThumbHighlightBackgroundBorder
    {
      get
      {
        return this.thumbHighlightBackgroundBorder;
      }
      set
      {
        this.thumbHighlightBackgroundBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the background thumb border.</summary>
    /// <value>The thumb background border.</value>
    [Category("TrackBar.Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the thumb background border.")]
    public Color ThumbPressedBackgroundBorder
    {
      get
      {
        return this.thumbPressedBackgroundBorder;
      }
      set
      {
        this.thumbPressedBackgroundBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the background border.</summary>
    /// <value>The background border.</value>
    [Category("TrackBar.Appearance")]
    [Description("Gets or sets the background border.")]
    [DefaultValue(typeof (Color), "Black")]
    public Color BackgroundBorder
    {
      get
      {
        return this.backgroundBorder;
      }
      set
      {
        this.backgroundBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the selectedBackground border.</summary>
    /// <value>The selectedBackground border.</value>
    [DefaultValue(typeof (Color), "Black")]
    [Category("TrackBar.Appearance")]
    [Description("Gets or sets the selectedBackground border.")]
    public Color ProgressBackgroundBorder
    {
      get
      {
        return this.progressBackgroundBorder;
      }
      set
      {
        this.progressBackgroundBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Description("Gets or sets the background brush.")]
    [Category("TrackBar.Appearance")]
    [Browsable(false)]
    public Brush BackgroundBrush
    {
      get
      {
        return this.backgroundBrush;
      }
      set
      {
        this.backgroundBrush = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Browsable(false)]
    [Description("Gets or sets the progressBackgroundBrush background brush.")]
    [Category("TrackBar.Appearance")]
    public Brush ProgressBackgroundBrush
    {
      get
      {
        return this.progressBackgroundBrush;
      }
      set
      {
        this.progressBackgroundBrush = value;
        this.Invalidate();
      }
    }

    /// <exclude />
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.animManager == null)
          this.animManager = new AnimationManager((Control) this);
        return this.animManager;
      }
    }

    /// <summary>Gets or sets whether the control is animated</summary>
    [DefaultValue(false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool AllowAnimations
    {
      get
      {
        if (this.backFill != null)
          return this.backFill.IsAnimated;
        return true;
      }
      set
      {
        if (this.backFill == null)
          return;
        this.backFill.IsAnimated = value;
        this.progressFill.IsAnimated = value;
      }
    }

    /// <summary>
    /// Occurs when the Value property of a track bar changes, either by movement of the scroll box or programmatically.
    /// </summary>
    [Category("Action")]
    [Description("Occurs when the Value property of a track bar changes, either by movement of the scroll box or programmatically.")]
    public event EventHandler ValueChanged;

    /// <summary>
    /// Occurs when either a mouse or keyboard action moves the scroll box.
    /// </summary>
    [Category("Behavior")]
    [Description("Occurs when either a mouse or keyboard action moves the scroll box.")]
    public event ScrollEventHandler Scroll;

    static vTrackBar()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vTrackBar()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UserMouse | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.BackColor = Color.Transparent;
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.progressFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.thumbFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
    }

    private void ResetThumbSize()
    {
      this.ThumbSize = new Size(10, 15);
    }

    private bool ShouldSerializeThumbSize()
    {
      if (this.ThumbSize.Width != 10)
        return this.ThumbSize.Height != 15;
      return false;
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        e.Graphics.FillRectangle(brush, this.ClientRectangle);
      this.DrawTrackBar(e);
    }

    /// <summary>Draws the track bar.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected virtual void DrawTrackBar(PaintEventArgs e)
    {
      Rectangle clientRectangle = this.ClientRectangle;
      ++clientRectangle.X;
      ++clientRectangle.Y;
      clientRectangle.Width -= 2;
      clientRectangle.Height -= 2;
      double num = (double) (this.currentPos - this.minPos) / (double) (this.maxPos - this.minPos);
      this.thumbRect = this.trackBarOrientation != Orientation.Horizontal ? new Rectangle(0, (int) (num * (double) (clientRectangle.Height - this.thumbSize.Width)) + clientRectangle.Y, this.thumbSize.Height - 3 + 8, this.thumbSize.Width) : new Rectangle((int) (num * (double) (clientRectangle.Width - this.thumbSize.Width)) + clientRectangle.X, 0, this.thumbSize.Width, this.thumbSize.Height - 3 + 8);
      this.barRect = this.ClientRectangle;
      if (this.trackBarOrientation == Orientation.Horizontal)
      {
        this.barRect.Inflate(-1, -this.barRect.Height / 3);
        this.thumbRect.Height = this.ThumbSize.Height;
        this.thumbRect.Width = this.ThumbSize.Width;
        this.thumbRect.Y = clientRectangle.Y + clientRectangle.Height / 2 - this.ThumbSize.Height / 2;
        if (this.thumbRect.X + this.thumbRect.Width > clientRectangle.Right)
          this.thumbRect.X = clientRectangle.Right - this.thumbRect.Width;
      }
      else
      {
        this.barRect.Inflate(-this.barRect.Width / 3, -1);
        this.thumbRect.Height = this.ThumbSize.Width;
        this.thumbRect.Width = this.ThumbSize.Height;
        this.thumbRect.X = clientRectangle.X + clientRectangle.Width / 2 - this.ThumbSize.Height / 2;
        if (this.thumbRect.Y + this.thumbRect.Height > clientRectangle.Bottom)
          this.thumbRect.Y = clientRectangle.Bottom - this.thumbRect.Height;
      }
      if (this.RightToLeft == RightToLeft.Yes)
      {
        if (this.trackBarOrientation == Orientation.Horizontal)
          this.thumbRect.X = clientRectangle.Right - (this.thumbRect.X - clientRectangle.Left) - this.thumbRect.Width;
        else
          this.thumbRect.Y = clientRectangle.Bottom - (this.thumbRect.Y - clientRectangle.Top) - this.thumbRect.Height;
      }
      this.DrawBackFillAndProgress(e.Graphics, this.barRect);
      this.thumbFill.Radius = this.roundedCornersRadiusThumb;
      this.thumbFill.RoundedCornersBitmask = this.roundedCornersMaskThumb;
      this.thumbFill.Bounds = this.thumbRect;
      if (this.UseThemeBackground && this.Enabled)
      {
        this.thumbFill.DrawElementFill(e.Graphics, this.Enabled ? this.state : ControlState.Disabled);
        this.thumbFill.DrawElementBorder(e.Graphics, this.Enabled ? this.state : ControlState.Disabled);
      }
      else
      {
        Brush brush = this.ThumbBackgroundBrush;
        Color backgroundBorder = this.ThumbBackgroundBorder;
        if (this.state == ControlState.Normal || this.state == ControlState.Default)
        {
          brush = this.ThumbBackgroundBrush;
          backgroundBorder = this.ThumbBackgroundBorder;
        }
        else if (this.state == ControlState.Hover)
        {
          brush = this.ThumbHighlightBackgroundBrush;
          backgroundBorder = this.ThumbHighlightBackgroundBorder;
        }
        else if (this.state == ControlState.Pressed)
        {
          brush = this.ThumbPressedBackgroundBrush;
          backgroundBorder = this.ThumbPressedBackgroundBorder;
        }
        if (brush != null && this.Enabled)
        {
          GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(this.thumbFill.Bounds, this.RoundedCornersRadius, this.RoundedCornersMask);
          e.Graphics.FillPath(brush, partiallyRoundedPath);
          this.thumbFill.DrawElementBorder(e.Graphics, this.state, backgroundBorder);
        }
        else
        {
          this.thumbFill.DrawElementFill(e.Graphics, this.Enabled ? this.state : ControlState.Disabled);
          this.thumbFill.DrawElementBorder(e.Graphics, this.Enabled ? this.state : ControlState.Disabled);
        }
      }
    }

    /// <summary>Draws the back fill and progress.</summary>
    /// <param name="g">The g.</param>
    /// <param name="rect">The rect.</param>
    protected virtual void DrawBackFillAndProgress(Graphics g, Rectangle rect)
    {
      this.backFill.Radius = this.roundedCornersRadius;
      this.backFill.RoundedCornersBitmask = this.roundedCornersMask;
      this.progressFill.Radius = this.roundedCornersRadius;
      this.progressFill.RoundedCornersBitmask = this.roundedCornersMask;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.HighQuality;
      Rectangle bounds1 = rect;
      if (!this.UseThemeBackground)
      {
        GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(bounds1, this.RoundedCornersRadius, this.RoundedCornersMask);
        g.FillPath(this.BackgroundBrush, partiallyRoundedPath);
      }
      else
      {
        this.backFill.Bounds = bounds1;
        this.backFill.DrawElementFill(g, this.Enabled ? ControlState.Normal : ControlState.Disabled);
      }
      double num = (double) (this.Value - this.Minimum) / (double) (this.Maximum - this.Minimum);
      int width = (int) ((double) bounds1.Width * num);
      int height = (int) ((double) bounds1.Height * num);
      Rectangle bounds2 = new Rectangle(bounds1.X, bounds1.Y, width, bounds1.Height);
      if (this.RightToLeft == RightToLeft.Yes)
        bounds2 = new Rectangle(bounds1.Width - width, bounds1.Y, width, bounds1.Height);
      if (bounds2.Width != 0 && bounds2.Height != 0)
      {
        if (this.trackBarOrientation == Orientation.Vertical)
        {
          bounds2 = new Rectangle(bounds1.X, bounds1.Y, bounds1.Width, height);
          if (this.RightToLeft == RightToLeft.Yes)
            bounds2 = new Rectangle(bounds1.X, bounds1.Height - height, bounds1.Width, height);
        }
        if (!this.UseThemeBackground)
        {
          GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(bounds2, this.RoundedCornersRadius, this.RoundedCornersMask);
          g.FillPath(this.ProgressBackgroundBrush, partiallyRoundedPath);
          this.backFill.DrawElementBorder(g, ControlState.Normal, this.ProgressBackgroundBorder);
        }
        else
        {
          this.progressFill.Bounds = bounds2;
          this.progressFill.DrawElementFill(g, this.Enabled ? ControlState.Pressed : ControlState.DisabledPressed);
        }
      }
      this.backFill.Bounds = bounds1;
      if (!this.UseThemeBackground)
        this.backFill.DrawElementBorder(g, ControlState.Normal, this.BackgroundBorder);
      else
        this.backFill.DrawElementBorder(g, this.Enabled ? ControlState.Normal : ControlState.Disabled);
      g.SmoothingMode = smoothingMode;
    }

    /// <exclude />
    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      if (this.thumbRect.Contains(this.PointToClient(Cursor.Position)))
        this.state = ControlState.Hover;
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.state = ControlState.Normal;
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (e.Button != MouseButtons.Left)
        return;
      this.Capture = true;
      this.state = ControlState.Pressed;
      if (this.Scroll != null)
        this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.ThumbTrack, this.currentPos));
      if (this.ValueChanged != null)
        this.ValueChanged((object) this, new EventArgs());
      this.OnMouseMove(e);
    }

    /// <exclude />
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.state = !this.thumbRect.Contains(e.Location) ? ControlState.Normal : ControlState.Hover;
      if (this.Capture & e.Button == MouseButtons.Left)
      {
        ScrollEventType type = ScrollEventType.ThumbPosition;
        Point location = e.Location;
        int num1 = this.trackBarOrientation == Orientation.Horizontal ? location.X : location.Y;
        if (this.RightToLeft == RightToLeft.Yes)
          num1 = this.trackBarOrientation == Orientation.Horizontal ? this.barRect.Right - location.X : this.barRect.Bottom - location.Y;
        int num2 = this.thumbSize.Width >> 1;
        this.currentPos = (int) ((double) (num1 - num2) * (double) ((float) (this.maxPos - this.minPos) / (float) ((this.trackBarOrientation == Orientation.Horizontal ? this.ClientSize.Width : this.ClientSize.Height) - 2 * num2)) + (double) this.minPos);
        if (this.currentPos <= this.minPos)
        {
          this.currentPos = this.minPos;
          type = ScrollEventType.First;
        }
        else if (this.currentPos >= this.maxPos)
        {
          this.currentPos = this.maxPos;
          type = ScrollEventType.Last;
        }
        if (this.Scroll != null)
          this.Scroll((object) this, new ScrollEventArgs(type, this.currentPos));
        if (this.ValueChanged != null)
          this.ValueChanged((object) this, new EventArgs());
        this.state = ControlState.Pressed;
      }
      this.Refresh();
    }

    /// <exclude />
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      this.Capture = false;
      if (this.Scroll != null)
        this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.EndScroll, this.currentPos));
      if (this.ValueChanged != null)
        this.ValueChanged((object) this, new EventArgs());
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.SetValue(this.Value + -e.Delta / 120 * (this.maxPos - this.minPos) / this.largeChange);
    }

    /// <exclude />
    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Invalidate();
    }

    /// <exclude />
    protected override void OnKeyUp(KeyEventArgs e)
    {
      int num1 = this.RightToLeft == RightToLeft.Yes ? -this.LargeChange : this.LargeChange;
      int num2 = this.RightToLeft == RightToLeft.Yes ? -this.SmallChange : this.SmallChange;
      base.OnKeyUp(e);
      switch (e.KeyCode)
      {
        case Keys.Prior:
          this.SetValue(this.Value + num1);
          if (this.Scroll != null)
          {
            this.Scroll((object) this, new ScrollEventArgs(this.RightToLeft == RightToLeft.Yes ? ScrollEventType.LargeIncrement : ScrollEventType.LargeIncrement, this.Value));
            break;
          }
          break;
        case Keys.Next:
          this.SetValue(this.Value - num1);
          if (this.Scroll != null)
          {
            this.Scroll((object) this, new ScrollEventArgs(this.RightToLeft == RightToLeft.Yes ? ScrollEventType.LargeIncrement : ScrollEventType.LargeDecrement, this.Value));
            break;
          }
          break;
        case Keys.End:
          this.Value = this.maxPos;
          break;
        case Keys.Home:
          this.Value = this.minPos;
          break;
        case Keys.Left:
        case Keys.Down:
          this.SetValue(this.Value - num2);
          if (this.Scroll != null)
          {
            this.Scroll((object) this, new ScrollEventArgs(this.RightToLeft == RightToLeft.Yes ? ScrollEventType.SmallIncrement : ScrollEventType.SmallDecrement, this.Value));
            break;
          }
          break;
        case Keys.Up:
        case Keys.Right:
          this.SetValue(this.Value + num2);
          if (this.Scroll != null)
          {
            this.Scroll((object) this, new ScrollEventArgs(this.RightToLeft == RightToLeft.Yes ? ScrollEventType.SmallDecrement : ScrollEventType.SmallIncrement, this.Value));
            break;
          }
          break;
      }
      if (this.Scroll != null && this.Value == this.minPos)
        this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.First, this.Value));
      if (this.Scroll != null && this.Value == this.maxPos)
        this.Scroll((object) this, new ScrollEventArgs(ScrollEventType.Last, this.Value));
      Point client = this.PointToClient(Cursor.Position);
      this.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, client.X, client.Y, 0));
    }

    /// <exclude />
    protected override bool ProcessDialogKey(Keys keyData)
    {
      if (keyData == Keys.Tab | Control.ModifierKeys == Keys.Shift)
        return base.ProcessDialogKey(keyData);
      this.OnKeyDown(new KeyEventArgs(keyData));
      return true;
    }

    private void SetValue(int value)
    {
      if (value < this.minPos)
        this.Value = this.minPos;
      else if (value > this.maxPos)
        this.Value = this.maxPos;
      else
        this.Value = value;
    }
  }
}
