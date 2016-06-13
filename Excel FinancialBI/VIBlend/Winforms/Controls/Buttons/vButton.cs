// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vButton
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
  /// <summary>Represents a vButton control</summary>
  /// <remarks>
  /// A vButton control represents a standard button control raises an event when the user clicks it, and provides customizable visual appearance, and themes.
  /// </remarks>
  [Description("Displays a button that raises an event when the user clicks it.")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vButton), "ControlIcons.vButton.ico")]
  public class vButton : Button, IScrollableControlBase
  {
    private bool paintFill = true;
    private bool paintBorder = true;
    private byte roundedCornersMask = 15;
    private int roundedCornersRadius = 3;
    protected float opacity = 1f;
    private bool paintDefaultFill = true;
    private bool paintDefaultBorder = true;
    private bool allowAnimations = true;
    private string styleKey = "Button";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private Point imageAbsolutePosition = new Point(5, 5);
    private Point textAbsolutePosition = new Point(5, 5);
    private bool showFocusRectangle = true;
    private Color focusColor = Color.Black;
    protected PaintHelper paintHelper = new PaintHelper();
    private bool useThemeTextColor = true;
    private Color highlightTextColor = Color.Black;
    private Color pressedTextColor = Color.Black;
    private bool stretchImage;
    private bool hoverEffectsEnabled;
    private AnimationManager manager;
    protected ControlTheme theme;
    internal ControlState controlState;
    /// <exclude />
    protected BackgroundElement backFill;
    private bool textWrap;
    private bool useAbsoluteImagePositioning;
    private bool useAbsoluteTextPositioning;
    protected ButtonBorderStyle buttonBorderStyle;

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
    /// Gets or sets the rounded corners radius of the vButton control.
    /// </summary>
    [Description("Gets or sets the rounded corners radius of the vButton control.")]
    [Category("Behavior")]
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

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="!:BackGroundElement" /> has opacity.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if has opacity draw with opacity; otherwise not, <c>false</c>.
    /// </value>
    [DefaultValue(1f)]
    public float Opacity
    {
      get
      {
        return this.opacity;
      }
      set
      {
        this.opacity = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to stretch the associated Image
    /// </summary>
    /// <value><c>true</c> if StretchImage; otherwise, <c>false</c>.</value>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to stretch the associated Image")]
    [DefaultValue(false)]
    public bool StretchImage
    {
      get
      {
        return this.stretchImage;
      }
      set
      {
        this.stretchImage = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether mouse hover effects are enabled.
    /// </summary>
    [Description("Gets or sets a value indicating whether mouse hover effects are enabled.")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool HoverEffectsEnabled
    {
      get
      {
        return this.hoverEffectsEnabled;
      }
      set
      {
        this.hoverEffectsEnabled = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a background when the control state is default.
    /// </summary>
    /// <value><c>true</c> if [paint fill]; otherwise, <c>false</c>.</value>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to paint a background when the control state is default.")]
    public virtual bool PaintDefaultFill
    {
      get
      {
        return this.paintDefaultFill;
      }
      set
      {
        this.paintDefaultFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a border when the control state is default.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to paint a border when the control state is default.")]
    [DefaultValue(true)]
    public virtual bool PaintDefaultBorder
    {
      get
      {
        return this.paintDefaultBorder;
      }
      set
      {
        this.paintDefaultBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a background
    /// </summary>
    /// <value><c>true</c> if [paint fill]; otherwise, <c>false</c>.</value>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to paint a background")]
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
    /// Gets or sets a value indicating whether to paint a border
    /// </summary>
    /// <value><c>true</c> if [paint border]; otherwise, <c>false</c>.</value>
    [Description("Gets or sets a value indicating whether to paint a border")]
    [Category("Behavior")]
    [DefaultValue(true)]
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

    /// <exclude />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    /// <summary>Determines whether to use animations</summary>
    [Browsable(false)]
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        if (this.backFill != null)
          this.backFill.IsAnimated = value;
        this.allowAnimations = value;
      }
    }

    /// <summary>Gets or sets the style key.</summary>
    /// <value>The style key.</value>
    [Browsable(false)]
    [DefaultValue("Button")]
    public virtual string StyleKey
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

    /// <summary>Gets or sets the theme of the control</summary>
    [Browsable(false)]
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the theme of the control")]
    public virtual ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null)
          return;
        this.theme = !(this.StyleKey != "Button") ? ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme) : ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme, value);
        this.theme = value;
        this.backFill.LoadTheme(this.theme);
        this.backFill.IsAnimated = true;
        FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("ButtonNormal");
        if (fillStyle1 != null)
          this.theme.StyleNormal.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("ButtonHighlight");
        if (fillStyle2 != null)
          this.theme.StyleHighlight.FillStyle = fillStyle2;
        FillStyle fillStyle3 = this.theme.QueryFillStyleSetter("ButtonPressed");
        if (fillStyle2 != null)
          this.theme.StylePressed.FillStyle = fillStyle3;
        Color color1 = Color.Empty;
        Color color2 = this.theme.QueryColorSetter("ButtonBorder");
        if (color2 != Color.Empty)
          this.theme.StyleNormal.BorderColor = color2;
        Color color3 = Color.Empty;
        Color color4 = this.theme.QueryColorSetter("ButtonTextColor");
        if (color4 != Color.Empty)
          this.theme.StyleNormal.TextColor = color4;
        Color color5 = Color.Empty;
        Color color6 = this.theme.QueryColorSetter("ButtonHighlightTextColor");
        if (color6 != Color.Empty)
          this.theme.StyleHighlight.TextColor = color6;
        Color color7 = Color.Empty;
        Color color8 = this.theme.QueryColorSetter("ButtonPressedTextColor");
        if (color8 != Color.Empty)
          this.theme.StylePressed.TextColor = color8;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes
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

    /// <summary>Gets or sets the font of the vButton</summary>
    [Category("Appearance")]
    [Description("Gets or sets button's font")]
    public override Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        if (value == null)
          return;
        this.backFill.Font = value;
        this.backFill.HighLightFont = value;
        this.backFill.PressedFont = value;
        this.backFill.DisabledFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets whether the button's text is wrappable</summary>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Gets or sets whether the button's text is wrappable")]
    public bool TextWrap
    {
      get
      {
        return this.textWrap;
      }
      set
      {
        this.textWrap = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the positioning mode of the image on the button control.
    /// </summary>
    [Description("Gets or sets the positioning mode of the image on the button control.")]
    [Category("Appearance")]
    [DefaultValue(false)]
    public bool UseAbsoluteImagePositioning
    {
      get
      {
        return this.useAbsoluteImagePositioning;
      }
      set
      {
        this.useAbsoluteImagePositioning = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the position of the image on the button control.
    /// </summary>
    /// <remarks>
    /// This property has effect only when UseAbsoluteImagePositioning is set to true.
    /// </remarks>
    [Description("Gets or sets the positioning mode of the image on the button control.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Point), "5, 5")]
    public Point ImageAbsolutePosition
    {
      get
      {
        return this.imageAbsolutePosition;
      }
      set
      {
        this.imageAbsolutePosition = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the positioning mode of the text on the button control.
    /// </summary>
    [DefaultValue(false)]
    [Description("Gets or sets the positioning mode of the text on the button control.")]
    [Category("Appearance")]
    public bool UseAbsoluteTextPositioning
    {
      get
      {
        return this.useAbsoluteTextPositioning;
      }
      set
      {
        this.useAbsoluteTextPositioning = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the position of the text on the button control.
    /// </summary>
    /// <remarks>
    /// This property has effect only when UseAbsoluteTextPositioning is set to true.
    /// </remarks>
    [Category("Appearance")]
    [DefaultValue(typeof (Point), "5, 5")]
    [Description("Gets or sets the positioning mode of the image on the button control.")]
    public Point TextAbsolutePosition
    {
      get
      {
        return this.textAbsolutePosition;
      }
      set
      {
        this.textAbsolutePosition = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the border style of the button</summary>
    [Description("Gets or sets the border style of the button")]
    [Category("Appearance")]
    public virtual ButtonBorderStyle BorderStyle
    {
      get
      {
        return this.buttonBorderStyle;
      }
      set
      {
        this.buttonBorderStyle = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Determines whether to show the focus rectangle when the button is Focused
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Determines whether to show the focus rectangle when the button is Focused")]
    public bool ShowFocusRectangle
    {
      get
      {
        return this.showFocusRectangle;
      }
      set
      {
        this.showFocusRectangle = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the focus.</summary>
    /// <value>The color of the focus.</value>
    [Description("Gets or sets the focus color.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    public Color FocusColor
    {
      get
      {
        return this.focusColor;
      }
      set
      {
        if (!(value != this.focusColor))
          return;
        this.focusColor = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the theme text color.
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the theme text color.")]
    [DefaultValue(true)]
    [Category("Appearance")]
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
      }
    }

    /// <summary>
    /// Gets or sets the highlight state text color. The property is used when the UseThemeTextColor is true
    /// </summary>
    [Description("Gets or sets the highlight state text color. The property is used when the UseThemeTextColor is true.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    public Color HighlightTextColor
    {
      get
      {
        return this.highlightTextColor;
      }
      set
      {
        if (!(value != this.highlightTextColor))
          return;
        this.highlightTextColor = value;
      }
    }

    /// <summary>
    /// Gets or sets the pressed state text color. The property is used when the UseThemeTextColor is true
    /// </summary>
    [Description("Gets or sets the pressed state text color. The property is used when the UseThemeTextColor is true.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    public Color PressedTextColor
    {
      get
      {
        return this.pressedTextColor;
      }
      set
      {
        if (!(value != this.pressedTextColor))
          return;
        this.pressedTextColor = value;
      }
    }

    static vButton()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vButton()
    {
      this.Size = new Size(100, 30);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.defaultTheme);
      this.SizeChanged += new EventHandler(this.ButtonControl_SizeChanged);
    }

    /// <summary>Raises the EnabledChanged event.</summary>
    /// <param name="e">An EventArgs that contains the event data. </param>
    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      this.controlState = this.Enabled ? ControlState.Normal : ControlState.Disabled;
      this.Refresh();
    }

    /// <summary>Raises the GotFocus event.</summary>
    /// <param name="e">An EventArgs that contains the event data. </param>
    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.Refresh();
    }

    /// <summary>Raises the OnLostFocus event.</summary>
    /// <param name="e">An EventArgs that contains the event data. </param>
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Refresh();
    }

    /// <summary>Raises the OnMouseMove event.</summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data. </param>
    protected override void OnMouseMove(MouseEventArgs mevent)
    {
      if (this.ClientRectangle.Contains(mevent.Location) && !this.Capture)
      {
        this.controlState = ControlState.Hover;
        this.Refresh();
      }
      base.OnMouseMove(mevent);
    }

    /// <summary>Raises the OnMouseLeave event.</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      this.controlState = ControlState.Normal;
      this.Refresh();
      base.OnMouseLeave(e);
    }

    /// <summary>Raises the OnMouseDown event.</summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      this.Capture = true;
      this.controlState = ControlState.Pressed;
      this.Refresh();
      base.OnMouseDown(mevent);
    }

    /// <summary>Raises the OnMouseUp event.</summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      this.Capture = false;
      this.controlState = !this.ClientRectangle.Contains(mevent.Location) ? ControlState.Normal : ControlState.Hover;
      this.Refresh();
      base.OnMouseUp(mevent);
    }

    /// <exclude />
    protected override void OnPaintBackground(PaintEventArgs e)
    {
      base.OnPaintBackground(e);
    }

    /// <exclude />
    protected override void OnParentVisibleChanged(EventArgs e)
    {
      base.OnParentVisibleChanged(e);
    }

    private void ButtonControl_SizeChanged(object sender, EventArgs e)
    {
    }

    private void ResetBorderStyle()
    {
      this.BorderStyle = ButtonBorderStyle.SOLID;
    }

    private bool ShouldSerializeBorderStyle()
    {
      return this.BorderStyle != ButtonBorderStyle.SOLID;
    }

    /// <summary>Draws the control.</summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="bounds">The bounds.</param>
    /// <param name="controlState">State of the control.</param>
    /// <param name="isFocused">if set to <c>true</c> [is focused].</param>
    /// <param name="roundedCornersMask">The rounded corners mask.</param>
    protected virtual void DrawControl(Graphics graphics, Rectangle bounds, ControlState controlState, bool isFocused, byte roundedCornersMask)
    {
      Graphics graphics1 = graphics;
      if (bounds.Height <= 0 || bounds.Width <= 0)
        return;
      Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
      if ((double) this.opacity != 1.0)
        graphics1 = Graphics.FromImage((Image) bitmap);
      SmoothingMode smoothingMode = graphics1.SmoothingMode;
      graphics1.SmoothingMode = SmoothingMode.HighQuality;
      this.backFill.Opacity = 1f;
      Rectangle rectangle = bounds;
      --rectangle.Height;
      --rectangle.Width;
      this.backFill.Bounds = rectangle;
      byte num = this.backFill.RoundedCornersBitmask;
      int radius = this.backFill.Radius;
      this.backFill.RoundedCornersBitmask = roundedCornersMask;
      this.backFill.Radius = this.RoundedCornersRadius;
      if (this.PaintFill)
      {
        if (controlState == ControlState.Normal || controlState == ControlState.Default)
        {
          if (this.PaintDefaultFill)
            this.backFill.DrawElementFill(graphics1, controlState);
        }
        else
          this.backFill.DrawElementFill(graphics1, controlState);
        if (this.HoverEffectsEnabled && (this.controlState == ControlState.Hover || controlState == ControlState.Pressed))
        {
          Point client = this.PointToClient(Cursor.Position);
          if (rectangle.Contains(client))
          {
            int offsetY = 0;
            int offsetX = client.X - this.Bounds.Width / 2;
            GraphicsPath partiallyRoundedPath = this.paintHelper.CreatePartiallyRoundedPath(rectangle, this.roundedCornersRadius, this.roundedCornersMask);
            Region clip = graphics1.Clip;
            graphics1.Clip = new Region(partiallyRoundedPath);
            Color color = Color.Empty;
            Color glowColor = controlState != ControlState.Hover ? this.theme.QueryColorSetter("ButtonGlowCenterColorPressed") : this.theme.QueryColorSetter("ButtonGlowCenterColor");
            if (glowColor == Color.Empty)
            {
              FillStyle fillStyle = controlState == ControlState.Hover ? this.Theme.StyleHighlight.FillStyle : this.Theme.StylePressed.FillStyle;
              glowColor = ControlPaint.LightLight(fillStyle.Colors[fillStyle.ColorsNumber - 1]);
            }
            this.paintHelper.DrawWin7Glow(graphics1, rectangle, offsetX, offsetY, glowColor);
            graphics1.Clip = clip;
          }
        }
      }
      Rectangle imageAndTextRect = this.GetImageAndTextRect(ref bounds, controlState);
      this.DrawImageAndText(graphics1, controlState, imageAndTextRect);
      if (controlState == ControlState.Normal || controlState == ControlState.Default)
      {
        if (this.PaintDefaultFill)
          rectangle = this.DrawBorder(graphics1, controlState, rectangle);
      }
      else
        rectangle = this.DrawBorder(graphics1, controlState, rectangle);
      this.DrawFocusRectangle(graphics1, isFocused, rectangle, roundedCornersMask);
      graphics1.SmoothingMode = smoothingMode;
      if ((double) this.opacity != 1.0 && bitmap != null)
      {
        PaintHelper.SetOpacityToImage(bitmap, (byte) ((double) byte.MaxValue * (double) this.opacity));
        graphics.DrawImage((Image) bitmap, 0, 0);
        graphics1.Dispose();
        bitmap.Dispose();
      }
      this.backFill.RoundedCornersBitmask = num;
      this.backFill.Radius = radius;
    }

    protected virtual Rectangle DrawBorder(Graphics graphics, ControlState controlState, Rectangle drawRect)
    {
      if (this.BorderStyle == ButtonBorderStyle.SOLID && this.PaintBorder)
      {
        Rectangle rectangle = drawRect;
        this.backFill.DrawElementBorder(graphics, controlState);
        rectangle.Inflate(-1, -1);
        Color color = Color.Empty;
        if (controlState == ControlState.Normal)
          color = this.Theme.QueryColorSetter("ButtonNormalInnerBorderColor");
        else if (controlState == ControlState.Hover)
          color = this.Theme.QueryColorSetter("ButtonHighlightInnerBorderColor");
        else if (controlState == ControlState.Pressed)
          color = this.Theme.QueryColorSetter("ButtonPressedInnerBorderColor");
        if ((int) color.A > 0)
        {
          this.backFill.Bounds = rectangle;
          this.backFill.DrawElementBorder(graphics, controlState, color);
        }
      }
      return drawRect;
    }

    protected Rectangle GetImageAndTextRect(ref Rectangle bounds, ControlState controlState)
    {
      Rectangle rectangle = new Rectangle(bounds.X + 3, bounds.Y + 3, bounds.Width - 6, bounds.Height - 6);
      this.backFill.ImageAlignment = this.ImageAlign;
      this.backFill.TextAlignment = this.TextAlign;
      this.backFill.TextWrap = this.TextWrap;
      return rectangle;
    }

    protected virtual void DrawImageAndText(Graphics graphics, ControlState controlState, Rectangle imageTextRect)
    {
      this.backFill.Font = this.Font;
      this.backFill.HighLightFont = this.Font;
      this.backFill.PressedFont = this.Font;
      this.backFill.DisabledFont = this.Font;
      Color color = this.ForeColor;
      if (this.controlState == ControlState.Hover)
        color = this.HighlightTextColor;
      else if (this.controlState == ControlState.Pressed)
        color = this.PressedTextColor;
      if ((int) color.A == 0)
        color = this.ForeColor;
      if (!this.useAbsoluteImagePositioning && !this.useAbsoluteTextPositioning)
      {
        if (this.UseThemeTextColor)
          this.backFill.DrawElementTextAndImage(graphics, controlState, this.Text, this.Image, imageTextRect, this.TextImageRelation);
        else
          this.backFill.DrawElementTextAndImage(graphics, controlState, this.Text, this.Image, imageTextRect, this.TextImageRelation, color);
      }
      else
      {
        if (this.useAbsoluteImagePositioning)
        {
          Rectangle rectangle = new Rectangle(this.imageAbsolutePosition.X, this.imageAbsolutePosition.Y, imageTextRect.Right - this.imageAbsolutePosition.X, imageTextRect.Bottom - this.imageAbsolutePosition.Y);
        }
        if (this.Image != null)
        {
          if (this.StretchImage)
          {
            Bitmap bitmap = new Bitmap(this.Image, this.Width, this.Height);
            graphics.DrawImage((Image) bitmap, this.imageAbsolutePosition);
          }
          else
            graphics.DrawImage(this.Image, this.imageAbsolutePosition);
        }
        Rectangle textBounds = imageTextRect;
        if (this.useAbsoluteTextPositioning)
          textBounds = new Rectangle(this.textAbsolutePosition.X, this.textAbsolutePosition.Y, imageTextRect.Right - this.textAbsolutePosition.X, imageTextRect.Bottom - this.textAbsolutePosition.Y);
        if (this.UseThemeTextColor)
          this.backFill.DrawElementText(graphics, controlState, this.Text, textBounds);
        else
          this.backFill.DrawElementText(graphics, controlState, this.Text, textBounds, color);
      }
    }

    protected Rectangle DrawFocusRectangle(Graphics graphics, bool isFocused, Rectangle drawRect, byte roundedCornersMask)
    {
      if (isFocused && this.showFocusRectangle)
      {
        Rectangle bounds = drawRect;
        bounds.Inflate(-2, -2);
        int radius = this.RoundedCornersRadius - 2;
        if (radius < 0)
          radius = 0;
        GraphicsPath partiallyRoundedPath = this.paintHelper.CreatePartiallyRoundedPath(bounds, radius, roundedCornersMask);
        using (Pen pen = new Pen(this.FocusColor, 1f))
        {
          SmoothingMode smoothingMode = graphics.SmoothingMode;
          graphics.SmoothingMode = SmoothingMode.None;
          pen.DashStyle = DashStyle.Dot;
          graphics.DrawPath(pen, partiallyRoundedPath);
          graphics.SmoothingMode = smoothingMode;
        }
        partiallyRoundedPath.Dispose();
      }
      return drawRect;
    }

    /// <summary>Raises the OnPaint event.</summary>
    /// <param name="e">A PaintEventArgs that contains the event data. </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)) && this.controlState == ControlState.Hover)
        this.controlState = ControlState.Normal;
      if (!this.Enabled)
        this.controlState = ControlState.Disabled;
      this.DrawControl(e.Graphics, this.ClientRectangle, this.controlState, this.Focused, this.roundedCornersMask);
    }
  }
}
