// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vProgressBar
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
  /// <summary>
  /// Represents a vProgressBar control. The control displays a bar that fills to indicate to the user the progress of an operation.
  /// </summary>
  [DefaultEvent("ValueChanged")]
  [Description("Represents a vProgressBar control. The control displays a bar that fills to indicate to the user the progress of an operation.")]
  [Designer("VIBlend.WinForms.Controls.Design.vTrackControlsDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vProgressBar), "ControlIcons.vProgressBar.ico")]
  public class vProgressBar : Control, IScrollableControlBase
  {
    private int maximumPosition = 100;
    private int stepValue = 1;
    private bool allowAnimations = true;
    private bool useThemeBackground = true;
    private Color backgroundBorder = Color.Black;
    private Color backgroundSeparatorColor = Color.Black;
    private Brush backgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private Brush progressBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    private byte roundedCornersMask = 15;
    private int roundedCornersRadius = 3;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private PaintHelper helper = new PaintHelper();
    private int currentPosition;
    private int minimumPosition;
    private BackgroundElement backFill;
    private BackgroundElement progressFill;
    private Orientation progressBarOrientation;
    private vProgressBar.vProgressBarFillStyle progressFillStyle;
    private AnimationManager manager;
    private ControlTheme theme;

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

    /// <summary>Gets or sets whether the control is animated</summary>
    [DefaultValue(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [Description("Gets or sets a value indicating whether to use theme's background")]
    [Category("ProgressBar.Appearance")]
    [DefaultValue(true)]
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

    /// <summary>Gets or sets the background border.</summary>
    /// <value>The background border.</value>
    [Category("ProgressBar.Appearance")]
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
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the background separator color.</summary>
    /// <value>The background separator color.</value>
    [Description("Gets or sets the background separator color.")]
    [Category("ProgressBar.Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    public Color BackgroundSeparatorColor
    {
      get
      {
        return this.backgroundSeparatorColor;
      }
      set
      {
        this.backgroundSeparatorColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the background brush.</summary>
    /// <value>The background brush.</value>
    [Browsable(false)]
    [Description("Gets or sets the background brush.")]
    [Category("ProgressBar.Appearance")]
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
    [Description("Gets or sets the progress background brush.")]
    [Category("ProgressBar.Appearance")]
    [Browsable(false)]
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

    /// <summary>
    /// Gets or sets the rounded corners radius of the progress bar.
    /// </summary>
    [Description("Gets or sets the rounded corners radius of the progress bar.")]
    [Category("ProgressBar.Appearance")]
    [DefaultValue(3)]
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

    /// <summary>Gets or sets the progress fill style of the control.</summary>
    [DefaultValue(vProgressBar.vProgressBarFillStyle.Solid)]
    [Browsable(true)]
    [Category("ProgressBar.Appearance")]
    public vProgressBar.vProgressBarFillStyle ProgressBarFillStyle
    {
      get
      {
        return this.progressFillStyle;
      }
      set
      {
        this.progressFillStyle = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the orientation of the control.</summary>
    [DefaultValue(Orientation.Horizontal)]
    [Category("Behavior")]
    [Description("Specifies the orientation of the control.")]
    public Orientation Orientation
    {
      get
      {
        return this.progressBarOrientation;
      }
      set
      {
        if (this.progressBarOrientation == value)
          return;
        this.progressBarOrientation = value;
        int width = this.Width;
        this.Width = this.Height;
        this.Height = width;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the current position of the vProgressBar control.
    /// </summary>
    /// <remarks>
    /// The minimum and maximum values of the Value property are specified by the Minimum and Maximum properties. This property enables you to increment or decrement the value of the vProgressBar.
    /// </remarks>
    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets the current position of the vProgressBar control.")]
    [Category("Behavior")]
    public int Value
    {
      get
      {
        return this.currentPosition;
      }
      set
      {
        if (value < this.minimumPosition)
          value = this.minimumPosition;
        if (value > this.maximumPosition)
          value = this.maximumPosition;
        if (this.currentPosition == value)
          return;
        this.currentPosition = value;
        this.OnValueChanged();
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the Minimum value of the vProgressBar control.
    /// </summary>
    [Description("Gets or sets the Minimum value of the vProgressBar control.")]
    [Category("Behavior")]
    [DefaultValue(0)]
    [RefreshProperties(RefreshProperties.All)]
    public int Minimum
    {
      get
      {
        return this.minimumPosition;
      }
      set
      {
        this.minimumPosition = value;
        if (this.minimumPosition > this.maximumPosition)
          this.maximumPosition = this.minimumPosition;
        if (this.minimumPosition > this.currentPosition)
          this.currentPosition = this.minimumPosition;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the Maximum value of the vProgressBar control.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the Maximum value of the vProgressBar control.")]
    [DefaultValue(100)]
    [RefreshProperties(RefreshProperties.All)]
    public int Maximum
    {
      get
      {
        return this.maximumPosition;
      }
      set
      {
        this.maximumPosition = value;
        if (this.maximumPosition < this.currentPosition)
          this.currentPosition = this.maximumPosition;
        if (this.maximumPosition < this.minimumPosition)
          this.minimumPosition = this.maximumPosition;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the amount by which a call to the PerformStep method increases the current position of the vProgressBar.
    /// </summary>
    [DefaultValue(1)]
    [Description("Gets or sets the amount by which a call to the PerformStep method increases the current position of the vProgressBar.")]
    [Category("Behavior")]
    public int Step
    {
      get
      {
        return this.stepValue;
      }
      set
      {
        this.stepValue = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Description("Gets or sets the theme of the control.")]
    [Category("Appearance")]
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
        this.progressFill.LoadTheme(copy);
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Description("Gets or sets the theme of the control.")]
    [Category("ProgressBar.Appearance")]
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
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>Occurs when value is changed</summary>
    [Category("Action")]
    public event EventHandler ValueChanged;

    static vProgressBar()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vProgressBar()
    {
      this.Size = new Size(150, 15);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.progressFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
    }

    /// <summary>Called when the value is changed.</summary>
    protected virtual void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, new EventArgs());
    }

    /// <summary>
    /// Advances the current position of the vProgressBar by the amount of the Step property.
    /// </summary>
    public void PerformStep()
    {
      if (this.currentPosition + this.stepValue < this.Maximum)
        this.currentPosition += this.stepValue;
      else
        this.currentPosition = this.Maximum;
      this.Invalidate();
    }

    /// <summary>
    /// Advances the current position of the vProgressBar bar by the specified amount.
    /// </summary>
    /// <param name="value">The amount by which to increment the vProgressBar's current position. </param>
    public void Increment(int value)
    {
      if (value < 0)
      {
        this.Decrement(-value);
      }
      else
      {
        if (this.currentPosition + value < this.Maximum)
          this.Value += value;
        else
          this.Value = this.Maximum;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Decrements the current position of the vProgressBar bar by the specified amount.
    /// </summary>
    /// <param name="value">The amount by which to decrement the vProgressBar's current position. </param>
    public void Decrement(int value)
    {
      if (value < 0)
      {
        this.Increment(-value);
      }
      else
      {
        if (this.currentPosition - value > this.Minimum)
          this.Value -= value;
        else
          this.Value = this.Minimum;
        this.Invalidate();
      }
    }

    /// <summary>Raises the Paint event.</summary>
    /// <param name="e">A PaintEventArgs that contains the event data. </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      this.RenderProgressBar(e.Graphics);
    }

    /// <summary>Renders the progress bar.</summary>
    /// <param name="g">The g.</param>
    /// <param name="bounds">The bounds.</param>
    public virtual void RenderProgressBar(Graphics g, Rectangle bounds)
    {
      using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        g.FillRectangle(brush, this.ClientRectangle);
      this.backFill.Radius = this.roundedCornersRadius;
      this.backFill.RoundedCornersBitmask = this.roundedCornersMask;
      this.progressFill.Radius = this.roundedCornersRadius;
      this.progressFill.RoundedCornersBitmask = this.roundedCornersMask;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.HighQuality;
      Rectangle bounds1 = new Rectangle(bounds.X, bounds.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      this.backFill.Bounds = bounds1;
      if (this.UseThemeBackground && this.Enabled)
      {
        this.backFill.DrawElementFill(g, this.Enabled ? ControlState.Normal : ControlState.Disabled);
      }
      else
      {
        GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(bounds1, this.RoundedCornersRadius, this.RoundedCornersMask);
        g.FillPath(this.BackgroundBrush, partiallyRoundedPath);
      }
      double num1 = (double) (this.currentPosition - this.minimumPosition) / (double) (this.maximumPosition - this.minimumPosition);
      int width = (int) ((double) bounds1.Width * num1);
      int height = (int) ((double) bounds1.Height * num1);
      Rectangle bounds2 = new Rectangle(bounds.X, bounds.Y, width, bounds1.Height);
      if (this.RightToLeft == RightToLeft.Yes)
        bounds2 = new Rectangle(bounds.Right - width, bounds.Y, width, bounds1.Height);
      if (bounds2.Width != 0 && bounds2.Height != 0)
      {
        if (this.Orientation == Orientation.Vertical)
        {
          bounds2 = new Rectangle(bounds.X, bounds.Y, bounds1.Width, height);
          if (this.RightToLeft == RightToLeft.Yes)
            bounds2 = new Rectangle(bounds.X, bounds.Bottom - height, bounds1.Width, height);
        }
        this.progressFill.Bounds = bounds2;
        if (this.UseThemeBackground)
        {
          this.progressFill.DrawElementFill(g, this.Enabled ? ControlState.Pressed : ControlState.DisabledPressed);
        }
        else
        {
          GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(bounds2, this.RoundedCornersRadius, this.RoundedCornersMask);
          g.FillPath(this.ProgressBackgroundBrush, partiallyRoundedPath);
        }
        int num2 = (int) ((double) this.Height * 0.67);
        int num3 = width / num2;
        Color white = Color.White;
        Color color = this.progressFill.Theme.StylePressed.FillStyle.Colors[0];
        if (this.Orientation == Orientation.Vertical)
        {
          num2 = (int) ((double) this.Width * 0.67);
          num3 = height / num2;
        }
        if (!this.UseThemeBackground)
          color = this.BackgroundSeparatorColor;
        switch (this.progressFillStyle)
        {
          case vProgressBar.vProgressBarFillStyle.Dashed:
            if (this.Orientation == Orientation.Horizontal)
            {
              for (int index = 1; index <= num3; ++index)
              {
                if (this.RightToLeft == RightToLeft.Yes)
                  g.DrawLine(new Pen(color, 1f), bounds.Right - num2 * index, 0, bounds.Right - num2 * index, this.Height);
                else
                  g.DrawLine(new Pen(color, 1f), num2 * index, 0, num2 * index, bounds.Bottom);
              }
            }
            if (this.Orientation == Orientation.Vertical)
            {
              for (int index = 1; index <= num3; ++index)
              {
                if (this.RightToLeft == RightToLeft.Yes)
                  g.DrawLine(new Pen(color, 1f), 0, bounds.Bottom - num2 * index, bounds.Right, bounds.Bottom - num2 * index);
                else
                  g.DrawLine(new Pen(color, 1f), 0, num2 * index, bounds.Right, num2 * index);
              }
              break;
            }
            break;
        }
      }
      this.backFill.Bounds = bounds1;
      if (this.UseThemeBackground && this.Enabled)
        this.backFill.DrawElementBorder(g, this.Enabled ? ControlState.Normal : ControlState.Disabled);
      else
        this.backFill.DrawElementBorder(g, ControlState.Normal, this.BackgroundBorder);
      g.SmoothingMode = smoothingMode;
    }

    /// <summary>Renders the progress bar.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    public virtual void RenderProgressBar(Graphics g)
    {
      using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        g.FillRectangle(brush, this.ClientRectangle);
      this.backFill.Radius = this.roundedCornersRadius;
      this.backFill.RoundedCornersBitmask = this.roundedCornersMask;
      this.progressFill.Radius = this.roundedCornersRadius;
      this.progressFill.RoundedCornersBitmask = this.roundedCornersMask;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.HighQuality;
      Rectangle bounds1 = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
      this.backFill.Bounds = bounds1;
      if (this.UseThemeBackground && this.Enabled)
      {
        this.backFill.DrawElementFill(g, this.Enabled ? ControlState.Normal : ControlState.Disabled);
      }
      else
      {
        GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(bounds1, this.RoundedCornersRadius, this.RoundedCornersMask);
        g.FillPath(this.BackgroundBrush, partiallyRoundedPath);
      }
      double num1 = (double) (this.currentPosition - this.minimumPosition) / (double) (this.maximumPosition - this.minimumPosition);
      int width = (int) ((double) bounds1.Width * num1);
      int height = (int) ((double) bounds1.Height * num1);
      Rectangle bounds2 = new Rectangle(0, 0, width, bounds1.Height);
      if (this.RightToLeft == RightToLeft.Yes)
        bounds2 = new Rectangle(this.Width - width, 0, width, bounds1.Height);
      if (bounds2.Width != 0 && bounds2.Height != 0)
      {
        if (this.Orientation == Orientation.Vertical)
        {
          bounds2 = new Rectangle(0, 0, bounds1.Width, height);
          if (this.RightToLeft == RightToLeft.Yes)
            bounds2 = new Rectangle(0, this.Height - height, bounds1.Width, height);
        }
        this.progressFill.Bounds = bounds2;
        if (this.UseThemeBackground)
        {
          this.progressFill.DrawElementFill(g, this.Enabled ? ControlState.Pressed : ControlState.DisabledPressed);
        }
        else
        {
          GraphicsPath partiallyRoundedPath = this.helper.CreatePartiallyRoundedPath(bounds2, this.RoundedCornersRadius, this.RoundedCornersMask);
          g.FillPath(this.ProgressBackgroundBrush, partiallyRoundedPath);
        }
        int num2 = (int) ((double) this.Height * 0.67);
        int num3 = width / num2;
        Color white = Color.White;
        Color color = this.progressFill.Theme.StylePressed.FillStyle.Colors[0];
        if (this.Orientation == Orientation.Vertical)
        {
          num2 = (int) ((double) this.Width * 0.67);
          num3 = height / num2;
        }
        if (!this.UseThemeBackground)
          color = this.BackgroundSeparatorColor;
        switch (this.progressFillStyle)
        {
          case vProgressBar.vProgressBarFillStyle.Dashed:
            if (this.Orientation == Orientation.Horizontal)
            {
              for (int index = 1; index <= num3; ++index)
              {
                if (this.RightToLeft == RightToLeft.Yes)
                  g.DrawLine(new Pen(color, 1f), this.Width - num2 * index, 0, this.Width - num2 * index, this.Height);
                else
                  g.DrawLine(new Pen(color, 1f), num2 * index, 0, num2 * index, this.Height);
              }
            }
            if (this.Orientation == Orientation.Vertical)
            {
              for (int index = 1; index <= num3; ++index)
              {
                if (this.RightToLeft == RightToLeft.Yes)
                  g.DrawLine(new Pen(color, 1f), 0, this.Height - num2 * index, this.Width, this.Height - num2 * index);
                else
                  g.DrawLine(new Pen(color, 1f), 0, num2 * index, this.Width, num2 * index);
              }
              break;
            }
            break;
        }
      }
      this.backFill.Bounds = bounds1;
      if (this.UseThemeBackground && this.Enabled)
        this.backFill.DrawElementBorder(g, this.Enabled ? ControlState.Normal : ControlState.Disabled);
      else
        this.backFill.DrawElementBorder(g, ControlState.Normal, this.BackgroundBorder);
      g.SmoothingMode = smoothingMode;
    }

    /// <summary>Enumeration of the vProgressBar fill styles</summary>
    public enum vProgressBarFillStyle
    {
      Solid,
      Dashed,
    }
  }
}
