// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vCircularProgressBar
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
  /// <summary>Represents a vCircularProgressBar control</summary>
  [ToolboxItem(true)]
  [Description("Represents a vCircularProgressBar control with animated progress indicator")]
  [Designer("VIBlend.WinForms.Controls.Design.vTrackControlsDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vCircularProgressBar), "ControlIcons.CircularProgressBar.ico")]
  public class vCircularProgressBar : Control, IScrollableControlBase
  {
    private int maximumPosition = 100;
    private bool allowAnimations = true;
    private int delay = 100;
    private float scaling = 1f;
    private Timer timer = new Timer();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private int indicatorsCount = 8;
    private int currentPosition;
    private int minimumPosition;
    private AnimationManager manager;
    private BackgroundElement backFill;
    private BackgroundElement progressFill;
    private ControlTheme theme;
    private bool useThemeBackground;

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

    /// <summary>Gets or sets the theme of the control.</summary>
    [Description("Gets or sets the theme of the control.")]
    [Category("Appearance")]
    [Browsable(false)]
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
        this.backFill.LoadTheme(this.theme);
        this.progressFill.LoadTheme(copy);
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
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    /// <summary>
    /// Gets the current position of the vCircularProgressBar control.
    /// </summary>
    /// <remarks>
    /// The minimum and maximum values of the Value property are specified by the Minimum and Maximum properties. This property enables you to increment or decrement the value of the vCircularProgressBar.
    /// </remarks>
    [RefreshProperties(RefreshProperties.All)]
    [Category("Behavior")]
    [Description("Gets or sets the current position of the vProgressBar control.")]
    public int Value
    {
      get
      {
        return this.currentPosition;
      }
      set
      {
        if (value < this.minimumPosition)
          throw new ArgumentException("Invalid Argument. Value must be between Minimum and Maximum");
        if (value > this.maximumPosition)
          throw new ArgumentException("Invalid Argument. Value must be between Minimum and Maximum");
        this.currentPosition = value;
        this.OnValueChanged();
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the Minimum value of the vCircularProgressBar control.
    /// </summary>
    [RefreshProperties(RefreshProperties.All)]
    [Category("Behavior")]
    [Description("Gets or sets the Minimum value of the vCircularProgressBar control.")]
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
    /// Gets or sets the Maximum value of the vCircularProgressBar control.
    /// </summary>
    [Description("Gets or sets the Maximum value of the vCircularProgressBar control.")]
    [Category("Behavior")]
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

    /// <summary>Gets or sets the scale range from 0.1 to 1.0.</summary>
    [DefaultValue(1f)]
    [Category("Appearance")]
    [Description("Gets or sets the scale raging from 0.1 to 1.0.")]
    public float ProgressIndicatorsScaleFactor
    {
      get
      {
        return this.scaling;
      }
      set
      {
        this.scaling = (double) value > 0.0 ? ((double) value > 1.0 ? 1f : value) : 0.1f;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the indication speed.</summary>
    [Description("Gets or sets the indication speed.")]
    [Category("Behavior")]
    [DefaultValue(75)]
    public int IndicationSpeed
    {
      get
      {
        return (-this.delay + 400) / 4;
      }
      set
      {
        int num = checked (400 - value * 4);
        this.delay = num >= 10 ? (num > 400 ? 400 : num) : 10;
        this.timer.Interval = this.delay;
      }
    }

    /// <summary>
    /// Gets or sets the number of indicators in the Circular ProgressBar control
    /// </summary>
    /// <value>Gets or sets the number of indicators in the Circular ProgressBar control</value>
    [Category("Behavior")]
    [Description("Gets or sets the indicators number.")]
    public int IndicatorsCount
    {
      get
      {
        return this.indicatorsCount;
      }
      set
      {
        this.indicatorsCount = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Determines whether the control's background is painted using the Theme colors
    /// </summary>
    public bool UseThemeBackground
    {
      get
      {
        return this.useThemeBackground;
      }
      set
      {
        this.useThemeBackground = value;
        this.Invalidate();
      }
    }

    /// <summary>Occurs when value is changed</summary>
    [Category("Action")]
    public event EventHandler ValueChanged;

    static vCircularProgressBar()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vCircularProgressBar" /> class.
    /// </summary>
    public vCircularProgressBar()
    {
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
      this.timer.Tick += new EventHandler(this.timer_Tick);
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.timer.Dispose();
      base.Dispose(disposing);
    }

    protected virtual void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, new EventArgs());
    }

    /// <summary>
    /// Starts the progress bar. When the value reaches the Maximum the progress indication is stopped.
    /// </summary>
    public void Start()
    {
      this.timer.Interval = this.delay;
      this.timer.Start();
    }

    /// <summary>Stops the progress bar.</summary>
    public void Stop()
    {
      this.timer.Stop();
      this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        e.Graphics.FillRectangle(brush, this.ClientRectangle);
      float num1 = (float) this.indicatorsCount;
      float num2 = (float) (((double) num1 + 1.0) / 2.0);
      float num3 = num1 + 1f;
      float angle = 360f / num1;
      GraphicsState gstate = e.Graphics.Save();
      if (this.useThemeBackground)
      {
        this.backFill.Bounds = this.ClientRectangle;
        this.backFill.Opacity = 0.1f;
        this.backFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
      }
      e.Graphics.TranslateTransform((float) this.Width / 2f, (float) this.Height / 2f);
      e.Graphics.RotateTransform(angle * (float) this.Value);
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      bool flag = this.Value == this.Minimum || this.Value == this.Maximum;
      for (int index = 1; index <= this.indicatorsCount; ++index)
      {
        using (new SolidBrush(Color.Red))
        {
          float num4 = (float) this.Width / (num2 / this.scaling);
          float num5 = (float) this.Width / num2 - num4;
          this.progressFill.Bounds = new Rectangle((int) ((float) this.Width / num3 + num5), (int) ((float) this.Height / num3 + num5), (int) num4, (int) num4);
          this.progressFill.Shape = Shapes.Circle;
          this.progressFill.Opacity = flag ? 1f / num1 : (float) index / num1;
          this.progressFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
          e.Graphics.RotateTransform(angle);
        }
      }
      e.Graphics.Restore(gstate);
      base.OnPaint(e);
    }

    /// <summary>
    /// Advances the current position of the vCircularProgressBar bar by the specified amount.
    /// </summary>
    /// <param name="value">The amount by which to increment the vCircularProgressBar's current position. </param>
    public void Increment(int value)
    {
      if (value < 0)
      {
        this.Decrement(-value);
      }
      else
      {
        if (this.currentPosition + value < this.Maximum)
        {
          this.Value += value;
        }
        else
        {
          this.Value = this.Maximum;
          this.Stop();
        }
        this.Invalidate();
      }
    }

    /// <summary>
    /// Decrements the current position of the vCircularProgressBar bar by the specified amount.
    /// </summary>
    /// <param name="value">The amount by which to decrement the vCircularProgressBar's current position. </param>
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

    private void timer_Tick(object sender, EventArgs e)
    {
      this.Increment(1);
      this.Invalidate();
    }
  }
}
