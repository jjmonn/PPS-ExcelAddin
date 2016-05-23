// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vArrowButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents a Button control that show an arrow with configurable direction.
  /// </summary>
  [Description("Displays a Button control that show an arrow with configurable direction.")]
  [ToolboxBitmap(typeof (vArrowButton), "ControlIcons.vArrowButton.ico")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  public class vArrowButton : ScrollableControlMiniBase
  {
    private bool drawScrollButtonsBorder = true;
    private ArrowDirection direction = ArrowDirection.Right;
    private PaintHelper helper = new PaintHelper();
    private int maximumPosition = 100;
    private RibbonPaintHelper ribbonPaintHelper = new RibbonPaintHelper();
    private string styleKey = "ArrowButton";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    protected BackgroundElement scrollElement;
    protected BackgroundElement button;
    private int scrollButtonsRoundedCornersRadius;
    private int currentPosition;
    private int minimumPosition;
    private ControlTheme theme;
    private ControlTheme themeButton;
    private ControlTheme themeScrollElement;
    private ControlState buttonState;

    /// <exclude />
    protected bool MouseHover
    {
      get
      {
        return this.ClientRectangle.Contains(this.PointToClient(Cursor.Position));
      }
    }

    /// <summary>
    /// Gets or sets the current position of the vArrowButton control.
    /// </summary>
    /// <remarks>
    /// The minimum and maximum values of the Value property are specified by the Minimum and Maximum properties. This property enables you to increment or decrement the value of the vProgressBar.
    /// </remarks>
    [Description("Gets or sets the current position of the vArrowButton control.")]
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.All)]
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
    /// Gets or sets the Minimum value of the vArrowButton control.
    /// </summary>
    [Category("Behavior")]
    [RefreshProperties(RefreshProperties.All)]
    [Description("Gets or sets the Minimum value of the vArrowButton control.")]
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
    /// Gets or sets the Maximum value of the vArrowButton control.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the Maximum value of the vArrowButton control.")]
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

    /// <summary>Gets or sets the arrow direction.</summary>
    /// <value>The arrow direction.</value>
    [Description("Gets or sets the arrow direction.")]
    [Category("Behavior")]
    public ArrowDirection ArrowDirection
    {
      get
      {
        return this.direction;
      }
      set
      {
        this.direction = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control can respond to user interaction.
    /// </summary>
    /// <value></value>
    /// <returns>true if the control can respond to user interaction; otherwise, false. The default is true.
    /// </returns>
    /// <PermissionSet>
    /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public new bool Enabled
    {
      get
      {
        return base.Enabled;
      }
      set
      {
        base.Enabled = value;
        if (!value)
          return;
        this.buttonState = ControlState.Normal;
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
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control.")]
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
        this.theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        this.themeButton = ThemeCache.GetTheme(this.StyleKey + "1", this.VIBlendTheme);
        this.themeScrollElement = ThemeCache.GetTheme(this.StyleKey + "2", this.VIBlendTheme);
        this.button.LoadTheme(this.themeButton);
        this.AllowAnimations = true;
        this.button.IsAnimated = true;
        this.button.Radius = this.scrollButtonsRoundedCornersRadius;
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

    /// <summary>Occurs when value is changed</summary>
    [Category("Action")]
    public event EventHandler ValueChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vArrowButton" /> class.
    /// </summary>
    public vArrowButton()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.CreateScrollElements();
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.AllowAnimations = true;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.ClientRectangle.Contains(e.Location))
      {
        if (e.Button == MouseButtons.None)
          this.buttonState = ControlState.Hover;
      }
      else
        this.buttonState = ControlState.Normal;
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.buttonState = ControlState.Normal;
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.Capture = false;
      base.OnMouseUp(e);
      this.buttonState = !this.ClientRectangle.Contains(e.Location) ? ControlState.Normal : ControlState.Hover;
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.Capture = true;
      if (e.Button == MouseButtons.Left)
      {
        if (this.Value < this.Maximum)
          ++this.Value;
      }
      else if (this.Value > this.Minimum)
        --this.Value;
      if (this.buttonState == ControlState.Pressed)
        return;
      this.buttonState = ControlState.Pressed;
      this.Invalidate();
    }

    /// <summary>Called when [value changed].</summary>
    protected virtual void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, new EventArgs());
    }

    protected void CreateScrollElements()
    {
      if (this.button == null)
      {
        this.button = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        this.button.Owner = "ScrollButton";
      }
      this.button.HostingControl = (IScrollableControlBase) this;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.ClientRectangle.Width == 0 || (this.ClientRectangle.Height == 0 || this.button == null))
        return;
      if (!this.Enabled)
        this.buttonState = ControlState.Disabled;
      Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
      --rectangle.Width;
      --rectangle.Height;
      this.button.Bounds = rectangle;
      this.button.DrawElementFill(e.Graphics, this.buttonState);
      if (this.drawScrollButtonsBorder || this.buttonState != ControlState.Normal)
        this.button.DrawElementBorder(e.Graphics, this.buttonState);
      Color baseColor = this.button.BorderColor;
      if (this.buttonState == ControlState.Pressed)
        baseColor = this.button.PressedBorderColor;
      if (this.buttonState == ControlState.Hover)
        baseColor = this.button.HighlightBorderColor;
      if (this.buttonState == ControlState.Disabled)
        baseColor = this.button.DisabledBorderColor;
      Color color = ControlPaint.Dark(baseColor);
      Size size = this.ribbonPaintHelper.GetArrowSize();
      if (this.ArrowDirection == ArrowDirection.Left || this.ArrowDirection == ArrowDirection.Right)
      {
        int width = size.Width;
        size = new Size(size.Height, width);
      }
      Rectangle middleCenterRectangle = this.ribbonPaintHelper.GetMiddleCenterRectangle(new Rectangle(0, 0, this.Width, this.Height), new Rectangle(Point.Empty, size));
      this.helper.DrawArrowFigure(e.Graphics, color, middleCenterRectangle, this.ArrowDirection);
    }
  }
}
