// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRichTextBox
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
  /// Represents a vRichTextBox control, that provides advanced text editing and formatting capabilities.
  /// </summary>
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Represents a vRichTextBox control, that provides advanced text editing and formatting capabilities.")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vRichTextBox), "ControlIcons.vTextBox.ico")]
  public class vRichTextBox : Control, IScrollableControlBase
  {
    private vRichTextBoxBase textBox = new vRichTextBoxBase();
    private int gleamWidth = 1;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool paintFill = true;
    private bool paintBorder = true;
    private bool useThemeBorderColor = true;
    private Color controlBorderColor = Color.FromArgb(39, 39, 39);
    private Color controlHighlightBorderColor = Color.DimGray;
    private AnimationManager animManager;
    private bool hover;
    private BackgroundElement backGround;
    private ControlTheme theme;
    private bool enableBorderHighlight;

    /// <summary>Gets or sets the width of the gleam.</summary>
    /// <value>The width of the gleam.</value>
    [Category("Appearance")]
    [Description("Gets or sets the width of the gleam.")]
    public int GleamWidth
    {
      get
      {
        return this.gleamWidth;
      }
      set
      {
        this.gleamWidth = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the text box.</summary>
    /// <value>The text box.</value>
    public vRichTextBoxBase TextBox
    {
      get
      {
        return this.textBox;
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
    [Browsable(false)]
    public bool AllowAnimations
    {
      get
      {
        if (this.backGround != null)
          return this.backGround.IsAnimated;
        return true;
      }
      set
      {
        if (this.backGround == null)
          return;
        this.backGround.IsAnimated = value;
      }
    }

    /// <summary>Gets or sets the text of the vTextBox control.</summary>
    [Category("Appearance")]
    public override string Text
    {
      get
      {
        if (this.textBox == null)
          return "";
        return this.textBox.Text;
      }
      set
      {
        if (this.textBox == null)
          return;
        this.textBox.Text = value;
      }
    }

    /// <summary>
    /// Gets or sets the maximum length of the text in the textbox.
    /// </summary>
    [Category("Behavior")]
    public int MaxLength
    {
      get
      {
        return this.textBox.MaxLength;
      }
      set
      {
        this.textBox.MaxLength = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets whether the vTextBox is in right-to-left mode.
    /// </summary>
    [Category("Behavior")]
    public override RightToLeft RightToLeft
    {
      get
      {
        return base.RightToLeft;
      }
      set
      {
        base.RightToLeft = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets whether the vTextBox has multiple lines.</summary>
    [Category("Behavior")]
    public bool Multiline
    {
      get
      {
        return this.textBox.Multiline;
      }
      set
      {
        this.textBox.Multiline = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets whether the vTextBox is in read only mode.
    /// </summary>
    [Category("Behavior")]
    public bool Readonly
    {
      get
      {
        return this.textBox.ReadOnly;
      }
      set
      {
        this.textBox.ReadOnly = value;
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control.")]
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
        this.theme = value;
        this.backGround = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
        this.backGround.LoadTheme(value);
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
    /// Gets or sets a value indicating whether to paint a background
    /// </summary>
    /// <value><c>true</c> if [paint fill]; otherwise, <c>false</c>.</value>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to paint a background")]
    internal bool PaintFill
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
    internal bool PaintBorder
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

    /// <summary>
    /// Gets or sets a value indicating whether to use the ControlBorderColor
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the ControlBorderColor")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public virtual bool UseThemeBorderColor
    {
      get
      {
        return this.useThemeBorderColor;
      }
      set
      {
        this.useThemeBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the BorderColor of the TextBox.</summary>
    [Description("Gets or sets the BorderColor of the TextBox.")]
    [Category("Appearance")]
    public virtual Color ControlBorderColor
    {
      get
      {
        return this.controlBorderColor;
      }
      set
      {
        this.controlBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the Highlight BorderColor of the TextBox.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "DimGray")]
    [Description("Gets or sets the Highlight BorderColor of the TextBox.")]
    public virtual Color ControlHighlightBorderColor
    {
      get
      {
        return this.controlHighlightBorderColor;
      }
      set
      {
        this.controlHighlightBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether [allow highlight].
    /// </summary>
    /// <value><c>true</c> if [allow highlight]; otherwise, <c>false</c>.</value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool AllowHighlight
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether [allow focused].
    /// </summary>
    /// <value><c>true</c> if [allow focused]; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool AllowFocused
    {
      get
      {
        return false;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets whether the border will be highlighted when the user moves the mouse over the control and when the control has focus
    /// </summary>
    [Category("Behavior")]
    [Browsable(true)]
    [DefaultValue(false)]
    [Description("Gets or sets whether the border will be highlighted when the user moves the mouse over the control and when the control has focus.")]
    public bool EnableBorderHighlight
    {
      get
      {
        return this.enableBorderHighlight;
      }
      set
      {
        this.enableBorderHighlight = value;
      }
    }

    static vRichTextBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vRichTextBox()
    {
      this.Controls.Add((Control) this.textBox);
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.Layout += new LayoutEventHandler(this.TextBox_Layout);
      this.MouseDown += new MouseEventHandler(this.textBox_MouseDown);
      this.MouseLeave += new EventHandler(this.vTextBox_MouseLeave);
      this.MouseEnter += new EventHandler(this.vTextBox_MouseEnter);
      this.textBox.BorderStyle = BorderStyle.None;
      this.textBox.Leave += new EventHandler(this.textBox_Leave);
      this.textBox.Enter += new EventHandler(this.textBox_Enter);
      this.textBox.MouseEnter += new EventHandler(this.vTextBox_MouseEnter);
      this.textBox.MouseLeave += new EventHandler(this.vTextBox_MouseLeave);
      this.textBox.TextChanged += new EventHandler(this.textBox_TextChanged);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.textBox.Click += new EventHandler(this.TextBox_Click);
      this.textBox.DoubleClick += new EventHandler(this.TextBox_DoubleClick);
      this.textBox.MouseUp += new MouseEventHandler(this.TextBox_MouseUp);
      this.textBox.MouseDown += new MouseEventHandler(this.TextBox_MouseDown);
      this.textBox.MouseHover += new EventHandler(this.TextBox_MouseHover);
      this.textBox.MouseMove += new MouseEventHandler(this.TextBox_MouseMove);
      this.textBox.GotFocus += new EventHandler(this.TextBox_GotFocus);
      this.textBox.LostFocus += new EventHandler(this.TextBox_LostFocus);
      this.textBox.SizeChanged += new EventHandler(this.TextBox_SizeChanged);
      this.textBox.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
      this.textBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
      this.textBox.KeyUp += new KeyEventHandler(this.TextBox_KeyUp);
      this.BackColor = Color.White;
    }

    private void TextBox_GotFocus(object sender, EventArgs e)
    {
    }

    private void TextBox_LostFocus(object sender, EventArgs e)
    {
    }

    private void TextBox_SizeChanged(object sender, EventArgs e)
    {
    }

    private void TextBox_Click(object sender, EventArgs e)
    {
      this.OnClick(e);
    }

    private void TextBox_DoubleClick(object sender, EventArgs e)
    {
      this.OnDoubleClick(e);
    }

    private void TextBox_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    private void TextBox_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void TextBox_MouseDown(object sender, MouseEventArgs e)
    {
      this.OnMouseDown(e);
    }

    private void TextBox_MouseHover(object sender, EventArgs e)
    {
      this.OnMouseHover(e);
    }

    private void TextBox_MouseMove(object sender, MouseEventArgs e)
    {
      this.OnMouseMove(e);
    }

    private void TextBox_MouseUp(object sender, MouseEventArgs e)
    {
      this.OnMouseUp(e);
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      if (this.Enabled)
        return;
      this.Invalidate();
    }

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    /// <exclude />
    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      if (!this.Focused)
        return;
      this.textBox.Focus();
    }

    private void textBox_Enter(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    private void textBox_Leave(object sender, EventArgs e)
    {
      this.Invalidate();
      this.hover = false;
      this.textBox.Invalidate();
    }

    private void vTextBox_MouseEnter(object sender, EventArgs e)
    {
      this.hover = true;
      this.Invalidate();
      this.textBox.Invalidate();
    }

    private void vTextBox_MouseLeave(object sender, EventArgs e)
    {
      this.hover = false;
      this.Invalidate();
      this.textBox.Invalidate();
    }

    private void textBox_MouseDown(object sender, MouseEventArgs e)
    {
      this.hover = true;
      this.Invalidate();
    }

    private void TextBox_Layout(object sender, LayoutEventArgs e)
    {
      this.textBox.Bounds = new Rectangle(this.gleamWidth, this.gleamWidth, this.Width - 2 * this.gleamWidth, this.Height - 2 * this.gleamWidth);
    }

    /// <summary>Draws the text.</summary>
    /// <param name="g">The g.</param>
    protected virtual void DrawText(Graphics g)
    {
      TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;
      Rectangle clientRectangle = this.ClientRectangle;
      TextFormatFlags flags = textFormatFlags;
      clientRectangle.Offset(1, 1);
      TextRenderer.DrawText((IDeviceContext) g, this.Text, this.Font, clientRectangle, ControlPaint.LightLight(this.ForeColor), this.BackColor, flags);
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.PaintBorder && !this.PaintFill || (this.Width == 0 || this.Height == 0))
        return;
      this.textBox.ForeColor = this.ForeColor;
      this.textBox.BackColor = this.BackColor;
      this.textBox.Font = this.Font;
      this.textBox.RightToLeft = this.RightToLeft;
      if (this.Parent != null)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.Parent.BackColor))
          e.Graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
      }
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      Color borderColor;
      Color color = borderColor = this.theme.StyleNormal.BorderColor;
      if (!this.UseThemeBorderColor)
        color = this.ControlBorderColor;
      if (this.hover && this.enableBorderHighlight)
        color = !this.UseThemeBorderColor ? this.ControlHighlightBorderColor : this.theme.StyleHighlight.BorderColor;
      if (this.textBox.Focused && this.enableBorderHighlight)
        color = !this.UseThemeBorderColor ? this.ControlHighlightBorderColor : this.theme.StylePressed.BorderColor;
      if (this.Readonly || !this.Enabled)
        color = this.theme.StyleDisabled.BorderColor;
      this.textBox.Visible = this.Enabled;
      Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
      e.Graphics.FillRectangle((Brush) new SolidBrush(this.textBox.BackColor), rectangle);
      if (!this.Enabled)
        this.DrawText(e.Graphics);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, color, color, LinearGradientMode.Vertical);
      PaintHelper paintHelper = new PaintHelper();
      --rectangle.Width;
      --rectangle.Height;
      GraphicsPath roundedPathRect = paintHelper.GetRoundedPathRect(rectangle, 0);
      e.Graphics.DrawPath(new Pen((Brush) linearGradientBrush, (float) this.gleamWidth), roundedPathRect);
      e.Graphics.SmoothingMode = smoothingMode;
    }
  }
}
