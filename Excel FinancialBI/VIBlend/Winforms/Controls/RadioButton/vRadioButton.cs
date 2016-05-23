// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRadioButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vRadioButton control.</summary>
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Represents a vRadioButton control, which provides multi-choice interface when grouped with other vRadioButton controls.")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vRadioButton), "ControlIcons.vRadioButton.ico")]
  public class vRadioButton : RadioButton, IScrollableControlBase
  {
    private bool flat = true;
    private bool useThemeTextColor = true;
    private Color checkMarkColor = Color.Black;
    private bool showFocusRectangle = true;
    private Color focusColor = Color.Black;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool isPressed;
    private bool isMouseOver;
    private BackgroundElement checkBackGround;
    private bool useThemeFont;
    private bool useThemeCheckMarkColors;
    private ControlState controlState;
    private AnimationManager animManager;
    private ControlTheme theme;

    /// <summary>
    /// Determines whether to use the text color from the Theme or ForeColor control property.
    /// </summary>
    [Description("Determines whether to use the text color from the Theme or ForeColor control property.")]
    [Category("Appearance")]
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
    /// Determines whether to use the Font from the Theme or the Font property of the control.
    /// </summary>
    [Category("Appearance")]
    [Description("Determines whether to use the Font from the Theme or the Font property of the control.")]
    [DefaultValue(false)]
    public bool UseThemeFont
    {
      get
      {
        return this.useThemeFont;
      }
      set
      {
        this.useThemeFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the check mark</summary>
    /// <seealso cref="P:VIBlend.WinForms.Controls.vRadioButton.UseThemeCheckMarkColors" />
    [Browsable(true)]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Black")]
    [Description("CheckMark Color")]
    public Color CheckMarkColor
    {
      get
      {
        return this.checkMarkColor;
      }
      set
      {
        this.checkMarkColor = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the check mark will use the Theme colors or the colors defined through the control properties
    /// </summary>
    [Description("Gets or sets whether the check mark will use the Theme colors or the colors defined through the control properties")]
    [Category("Appearance")]
    [Browsable(true)]
    [DefaultValue(false)]
    public bool UseThemeCheckMarkColors
    {
      get
      {
        return this.useThemeCheckMarkColors;
      }
      set
      {
        this.useThemeCheckMarkColors = value;
      }
    }

    /// <summary>
    /// Determines whether to show the focus rectangle when the radio button is Focused
    /// </summary>
    [DefaultValue(true)]
    [Description("Determines whether to show the focus rectangle when the radio button is Focused")]
    [Category("Appearance")]
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
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
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

    /// <exclude />
    [Browsable(false)]
    public new ContentAlignment CheckAlign
    {
      get
      {
        return base.CheckAlign;
      }
      set
      {
        base.CheckAlign = value;
      }
    }

    /// <exclude />
    [DefaultValue(false)]
    [Category("Behavior")]
    [Browsable(false)]
    public bool Flat
    {
      get
      {
        return this.flat;
      }
      set
      {
        this.flat = value;
        this.Invalidate();
      }
    }

    /// <exclude />
    [Browsable(false)]
    public new FlatStyle FlatStyle
    {
      get
      {
        return base.FlatStyle;
      }
      set
      {
        base.FlatStyle = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public new Image Image
    {
      get
      {
        return base.Image;
      }
      set
      {
        base.Image = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public new ContentAlignment ImageAlign
    {
      get
      {
        return base.ImageAlign;
      }
      set
      {
        base.ImageAlign = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public new int ImageIndex
    {
      get
      {
        return base.ImageIndex;
      }
      set
      {
        base.ImageIndex = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public new ImageList ImageList
    {
      get
      {
        return base.ImageList;
      }
      set
      {
        base.ImageList = value;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public override ContentAlignment TextAlign
    {
      get
      {
        return base.TextAlign;
      }
      set
      {
        base.TextAlign = value;
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

    /// <summary>Gets or sets the theme of the control.</summary>
    [Category("Appearance")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        this.theme = value;
        this.checkBackGround.LoadTheme(value);
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

    /// <summary>Gets or sets whether the control is animated</summary>
    [Browsable(false)]
    [DefaultValue(false)]
    public bool AllowAnimations
    {
      get
      {
        if (this.checkBackGround != null)
          return this.checkBackGround.IsAnimated;
        return true;
      }
      set
      {
        if (this.checkBackGround == null)
          return;
        this.checkBackGround.IsAnimated = value;
      }
    }

    static vRadioButton()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vRadioButton()
    {
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.checkBackGround = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
    }

    internal FillStyle FillStyleFromState()
    {
      if (!this.Enabled)
        return this.theme.StyleDisabled.FillStyle;
      if (this.isMouseOver)
        return this.theme.StyleHighlight.FillStyle;
      if (this.isPressed)
        return this.theme.StylePressed.FillStyle;
      return this.theme.StyleNormal.FillStyle;
    }

    internal Color BorderColorFromState()
    {
      if (!this.Enabled)
        return this.theme.StyleDisabled.BorderColor;
      if (this.isMouseOver)
        return this.theme.StyleHighlight.BorderColor;
      if (this.isPressed)
        return this.theme.StylePressed.BorderColor;
      return this.theme.StyleNormal.BorderColor;
    }

    internal Color TextColorFromState()
    {
      if (!this.useThemeTextColor)
        return this.ForeColor;
      if (!this.Enabled)
        return this.theme.StyleDisabled.TextColor;
      Color color = this.theme.QueryColorSetter("CheckBoxTextColor");
      if (!color.IsEmpty)
        return color;
      if (this.isMouseOver)
        return this.theme.StyleHighlight.TextColor;
      if (this.isPressed)
        return this.theme.StylePressed.TextColor;
      return this.theme.StyleNormal.TextColor;
    }

    internal Font FontFromState()
    {
      if (!this.useThemeFont)
        return this.Font;
      if (!this.Enabled)
        return this.theme.StyleDisabled.Font;
      if (this.isMouseOver)
        return this.theme.StyleHighlight.Font;
      if (this.isPressed)
        return this.theme.StylePressed.Font;
      return this.theme.StyleNormal.Font;
    }

    /// <summary>Draws the radio button.</summary>
    /// <param name="g">The g.</param>
    /// <param name="rect">The rect.</param>
    public virtual void DrawRadioButton(Graphics g, Rectangle rect)
    {
      Rectangle rect1 = Rectangle.Empty;
      Rectangle rectangle = Rectangle.Empty;
      rectangle = new Rectangle(rect.X + 4, rect.Y, rect.Width - 4, rect.Height);
      int num = 11;
      rect1 = this.RightToLeft != RightToLeft.No ? new Rectangle(rect.Right - 15, rect.Y + (rect.Height - num) / 2 - 1, num, num) : new Rectangle(rect.X, rect.Y + (rect.Height - num) / 2 - 1, num, num);
      if (this.RightToLeft == RightToLeft.No)
      {
        rectangle.X += 10;
        rectangle.Width -= 10;
      }
      else
        rectangle.Width -= 16;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      FillStyle fillStyle = this.FillStyleFromState();
      g.FillEllipse(fillStyle.GetBrush(rect1), rect1);
      Color baseColor = this.BorderColorFromState();
      Color color1 = this.theme.QueryColorSetter("CheckMarkColor");
      if (this.isMouseOver)
      {
        Color color2 = this.theme.QueryColorSetter("CheckMarkHighlightColor");
        if (!color2.IsEmpty)
          color1 = color2;
      }
      if (!color1.IsEmpty)
        baseColor = color1;
      if (!this.useThemeCheckMarkColors)
        baseColor = !this.Checked ? Color.FromArgb(0, (int) baseColor.R, (int) baseColor.G, (int) baseColor.B) : this.CheckMarkColor;
      g.DrawEllipse(new Pen(ControlPaint.Dark(baseColor)), rect1.X, rect1.Y, rect1.Width, rect1.Height);
      if (this.Focused && this.ShowFocusRectangle)
      {
        using (Pen pen = new Pen(this.FocusColor, 1f))
        {
          pen.DashStyle = DashStyle.Dot;
          Rectangle rect2 = rect1;
          rect2.Inflate(-2, -2);
          g.DrawEllipse(pen, rect2);
        }
      }
      if (this.Checked)
        g.FillEllipse((Brush) new SolidBrush(this.Enabled ? baseColor : this.theme.StyleDisabled.BorderColor), rect1.X + 2, rect1.Y + 2, 7, 7);
      g.SmoothingMode = smoothingMode;
      if (this.Appearance != Appearance.Normal)
        return;
      using (StringFormat format = new StringFormat())
      {
        format.FormatFlags = StringFormatFlags.NoWrap;
        format.LineAlignment = StringAlignment.Center;
        format.Trimming = StringTrimming.EllipsisCharacter;
        format.HotkeyPrefix = HotkeyPrefix.Show;
        if (this.RightToLeft == RightToLeft.Yes)
          format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        using (Brush brush = (Brush) new SolidBrush(this.TextColorFromState()))
          g.DrawString(this.Text, this.FontFromState(), brush, (RectangleF) rectangle, format);
      }
    }

    internal void DrawRadioButton(Graphics g, Rectangle rect, bool isMouseOver, bool isPressed)
    {
      this.isMouseOver = isMouseOver;
      this.isPressed = isPressed;
      Rectangle rect1 = Rectangle.Empty;
      Rectangle rectangle = Rectangle.Empty;
      rectangle = new Rectangle(rect.X + 4, rect.Y, rect.Width - 4, rect.Height);
      int num = 11;
      rect1 = this.RightToLeft != RightToLeft.No ? new Rectangle(rect.Right - 15, rect.Y + (rect.Height - num) / 2 - 1, num, num) : new Rectangle(rect.X, rect.Y + (rect.Height - num) / 2 - 1, num, num);
      if (this.RightToLeft == RightToLeft.No)
      {
        rectangle.X += 10;
        rectangle.Width -= 10;
      }
      else
        rectangle.Width -= 16;
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      FillStyle fillStyle = this.FillStyleFromState();
      g.FillEllipse(fillStyle.GetBrush(rect1), rect1);
      Color baseColor = this.BorderColorFromState();
      if (!this.useThemeCheckMarkColors)
        baseColor = !this.Checked ? Color.FromArgb(0, (int) baseColor.R, (int) baseColor.G, (int) baseColor.B) : this.CheckMarkColor;
      g.DrawEllipse(new Pen(ControlPaint.Dark(baseColor)), rect1.X, rect1.Y, rect1.Width, rect1.Height);
      if (this.Checked)
        g.FillEllipse((Brush) new SolidBrush(this.Enabled ? baseColor : this.theme.StyleDisabled.BorderColor), rect1.X + 2, rect1.Y + 2, 7, 7);
      g.SmoothingMode = smoothingMode;
      if (this.Appearance != Appearance.Normal)
        return;
      using (StringFormat format = new StringFormat())
      {
        format.FormatFlags = StringFormatFlags.NoWrap;
        format.LineAlignment = StringAlignment.Center;
        format.Trimming = StringTrimming.EllipsisCharacter;
        format.HotkeyPrefix = HotkeyPrefix.Show;
        if (this.RightToLeft == RightToLeft.Yes)
          format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
        using (Brush brush = (Brush) new SolidBrush(this.TextColorFromState()))
          g.DrawString(this.Text, this.FontFromState(), brush, (RectangleF) rectangle, format);
      }
    }

    /// <summary>Overrides OnGotFocus</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.Invalidate();
    }

    /// <summary>Overrides OnLostFocus</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Invalidate();
    }

    /// <summary>Overrides OnMouseEnter</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.isMouseOver = true;
      this.Invalidate();
    }

    /// <summary>Overrides OnMouseDown</summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      base.OnMouseDown(mevent);
      if (mevent.Button != MouseButtons.Left)
        return;
      this.isPressed = true;
    }

    /// <summary>Overrides OnMouseUp</summary>
    /// <param name="mevent">A MouseEventArgs that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      base.OnMouseUp(mevent);
      if (mevent.Button != MouseButtons.Left)
        return;
      this.isPressed = false;
    }

    /// <summary>Overrides OnMouseLeave</summary>
    /// <param name="e">A EventArgs that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.isMouseOver = false;
      this.Invalidate();
    }

    /// <summary>Overrides OnPaint</summary>
    /// <param name="e">A PaintEventArgs that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
      using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
      {
        graphics.FillRectangle((Brush) solidBrush, rect);
        this.DrawRadioButton(graphics, rect);
      }
    }
  }
}
