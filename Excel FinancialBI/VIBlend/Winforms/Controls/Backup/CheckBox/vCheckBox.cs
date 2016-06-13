// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vCheckBox
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
  /// <summary>
  /// Represents a vCheckBox control which allows the user to indicate whether an item is selected or not.
  /// </summary>
  [ToolboxBitmap(typeof (vCheckBox), "ControlIcons.vCheckBox.ico")]
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Represents a vCheckBox control which allows a user to indicate whether an item is selected or not.")]
  public class vCheckBox : CheckBox, IScrollableControlBase, INotifyPropertyChanged
  {
    private bool flat = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool useThemeTextColor = true;
    private Color checkMarkColor = Color.Black;
    private Color checkMarkColorIntermidiate = Color.Black;
    private bool useThemeCheckMarkColors = true;
    private bool showFocusRectangle = true;
    private Color focusColor = Color.Black;
    private PaintHelper helper = new PaintHelper();
    private bool isMouseOver;
    internal BackgroundElement checkBackGround;
    private ControlTheme theme;
    private bool textWrap;
    private bool useThemeFont;
    internal bool isPressed;
    private AnimationManager animManager;

    /// <summary>Gets or sets the theme of the control.</summary>
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        this.theme = value;
        this.theme.Radius = 0.0f;
        this.checkBackGround.LoadTheme(this.theme);
        this.Invalidate();
        this.OnPropertyChanged("Theme");
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control to one of the built-in themes.
    /// </summary>
    [Description("Gets or sets the theme of the control to one of the built-in themes.")]
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
        this.OnPropertyChanged("VIBlendTheme");
      }
    }

    /// <summary>
    /// Gets or sets whether the checkbutton's text is wrappable
    /// </summary>
    [Description("Gets or sets whether the checkbutton's text is wrappable")]
    [DefaultValue(false)]
    [Category("Appearance")]
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
        this.OnPropertyChanged("TextWrap");
      }
    }

    /// <summary>
    /// Determines whether to use the text color from the Theme or a custom value
    /// </summary>
    [DefaultValue(true)]
    [Description("Determines whether to use the text color from the Theme or a custom value")]
    [Category("Appearance")]
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
        this.OnPropertyChanged("UseThemeTextColor");
      }
    }

    /// <summary>
    /// Determines whether to use the Font from the Theme or a custom value
    /// </summary>
    [DefaultValue(false)]
    [Description("Determines whether to use the Font from the Theme or a custom value")]
    [Category("Appearance")]
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
        this.OnPropertyChanged("UseThemeFont");
      }
    }

    /// <summary>Gets or sets the color of the check mark</summary>
    /// <seealso cref="P:VIBlend.WinForms.Controls.vCheckBox.UseThemeCheckMarkColors" />
    [Description("CheckMark Color")]
    [DefaultValue(typeof (Color), "Black")]
    [Category("Appearance")]
    [Browsable(true)]
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
    /// Gets or sets the color of the check mark in intermediate state
    /// </summary>
    [DefaultValue(typeof (Color), "Black")]
    [Description("Gets or sets the color of the check mark in intermediate state")]
    [Browsable(true)]
    [Category("Appearance")]
    public Color CheckMarkColorIntermidiate
    {
      get
      {
        return this.checkMarkColorIntermidiate;
      }
      set
      {
        this.checkMarkColorIntermidiate = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the check mark will use the Theme colors or the colors defined through the control properties
    /// </summary>
    [DefaultValue(true)]
    [Description("Gets or sets whether the check mark will use the Theme colors or the colors defined through the control properties")]
    [Browsable(true)]
    [Category("Appearance")]
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
    /// Determines whether to show the focus rectangle when the checkbox is Focused
    /// </summary>
    [Description("Determines whether to show the focus rectangle when the checkbox is Focused")]
    [Category("Appearance")]
    [DefaultValue(true)]
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
    [Category("Appearance")]
    [Description("Gets or sets the focus color.")]
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

    /// <exclude />
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    /// <exclude />
    [EditorBrowsable(EditorBrowsableState.Never)]
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

    /// <summary>Occurs when a property is changed.</summary>
    [Category("Action")]
    [Description("Occurs when a property is changed.")]
    public event PropertyChangedEventHandler PropertyChanged;

    static vCheckBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vCheckBox()
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

    protected internal virtual void DrawCheck(Graphics graphics, int x, int y, ControlState controlState)
    {
      x -= 3;
      y -= 3;
      Point pt1 = new Point(x, y + 2);
      Point point = new Point(x + 2, y + 4);
      Point pt2 = new Point(x + 6, y);
      Color color1 = this.BorderColorFromState();
      Color color2 = this.theme.QueryColorSetter("CheckMarkColor");
      if (!color2.IsEmpty)
        color1 = color2;
      if (controlState == ControlState.Hover)
      {
        Color color3 = this.theme.QueryColorSetter("CheckMarkHighlightColor");
        if (!color3.IsEmpty)
          color1 = color3;
      }
      if (!this.useThemeCheckMarkColors)
      {
        switch (this.CheckState)
        {
          case CheckState.Unchecked:
            color1 = Color.FromArgb(0, (int) color1.R, (int) color1.G, (int) color1.B);
            break;
          case CheckState.Checked:
            color1 = this.CheckMarkColor;
            break;
          case CheckState.Indeterminate:
            color1 = this.CheckMarkColorIntermidiate;
            break;
        }
      }
      if (this.CheckState == CheckState.Indeterminate)
      {
        using (SolidBrush solidBrush = new SolidBrush(color1))
        {
          Rectangle rect = new Rectangle(x, y, 13, 13);
          rect.Width = rect.Height = 7;
          graphics.FillRectangle((Brush) solidBrush, rect);
        }
      }
      else
      {
        using (Pen pen = new Pen(color1))
        {
          graphics.DrawLine(pen, pt1, point);
          graphics.DrawLine(pen, point, pt2);
          ++pt1.Y;
          ++point.Y;
          ++pt2.Y;
          graphics.DrawLine(pen, pt1, point);
          graphics.DrawLine(pen, point, pt2);
          ++pt1.Y;
          ++point.Y;
          ++pt2.Y;
          graphics.DrawLine(pen, pt1, point);
          graphics.DrawLine(pen, point, pt2);
        }
      }
    }

    /// <summary>Draws the check box.</summary>
    /// <param name="g">The g.</param>
    /// <param name="rect">The rect.</param>
    /// <param name="controlState">State of the control.</param>
    public virtual void DrawCheckBox(Graphics g, Rectangle rect, ControlState controlState)
    {
      Rectangle rectangle1 = Rectangle.Empty;
      Rectangle rectangle2 = Rectangle.Empty;
      rectangle2 = new Rectangle(rect.X + 4, rect.Y, rect.Width - 4, rect.Height);
      rectangle1 = this.RightToLeft != RightToLeft.No ? new Rectangle(rect.Right - 15, rect.Y + (rect.Height - 12) / 2, 12, 12) : new Rectangle(rect.X, rect.Y + (rect.Height - 12) / 2, 12, 12);
      if (this.RightToLeft == RightToLeft.No)
      {
        rectangle2.X += 10;
        rectangle2.Width -= 10;
      }
      else
        rectangle2.Width -= 16;
      this.checkBackGround.Bounds = rectangle1;
      this.checkBackGround.DrawElementFill(g, controlState);
      this.checkBackGround.DrawElementBorder(g, controlState);
      if (this.Focused && this.ShowFocusRectangle)
      {
        using (Pen pen = new Pen(this.FocusColor, 1f))
        {
          pen.DashStyle = DashStyle.Dot;
          Rectangle rect1 = rectangle1;
          rect1.Inflate(-2, -2);
          g.DrawRectangle(pen, rect1);
        }
      }
      if (this.CheckState != CheckState.Unchecked)
        this.DrawCheck(g, rectangle1.X + 6, rectangle1.Y + 6, controlState);
      if (this.Appearance != Appearance.Normal)
        return;
      using (StringFormat format = new StringFormat())
      {
        using (Brush brush = (Brush) new SolidBrush(this.TextColorFromState()))
        {
          if (!this.TextWrap)
            format.FormatFlags = StringFormatFlags.NoWrap;
          format.LineAlignment = this.helper.GetVerticalAlignment(this.TextAlign);
          format.Alignment = this.helper.GetHorizontalAlignment(this.TextAlign);
          format.Trimming = StringTrimming.EllipsisCharacter;
          format.HotkeyPrefix = HotkeyPrefix.Show;
          if (this.RightToLeft == RightToLeft.Yes)
            format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
          g.DrawString(this.Text, this.FontFromState(), brush, (RectangleF) rectangle2, format);
        }
      }
    }

    /// <exclude />
    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      this.Refresh();
    }

    /// <exclude />
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      this.Refresh();
    }

    /// <exclude />
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
      this.isMouseOver = true;
      this.Refresh();
    }

    /// <exclude />
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.isMouseOver = false;
      this.Refresh();
    }

    /// <exclude />
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      base.OnMouseDown(mevent);
      this.isPressed = true;
    }

    /// <exclude />
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      base.OnMouseUp(mevent);
      this.isPressed = false;
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.flat)
      {
        base.OnPaint(e);
      }
      else
      {
        Graphics graphics = e.Graphics;
        Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
          graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
        ControlState controlState = ControlState.Normal;
        if (this.isMouseOver)
          controlState = ControlState.Hover;
        if (!this.Enabled)
          controlState = ControlState.Disabled;
        this.DrawCheckBox(graphics, rect, controlState);
      }
    }

    /// <exclude />
    protected override void OnSystemColorsChanged(EventArgs e)
    {
      base.OnSystemColorsChanged(e);
    }

    /// <summary>Called when a property is changed</summary>
    /// <param name="propertyName">Name of the property.</param>
    public void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
