// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.vEditorControlBase
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  /// <exclude />
  [ToolboxItem(false)]
  public class vEditorControlBase : vControlBase
  {
    protected internal vTextBoxBase txtBox = new vTextBoxBase();
    private bool textBoxAutoLayout = true;
    private ControlTheme theme = ControlTheme.GetDefaultTheme(VIBLEND_THEME.VISTABLUE);
    private bool useThemeBorderColor = true;
    private bool useThemeForeColor = true;
    private bool useThemeFont = true;
    private Color overrideBorderColor = Color.Gray;
    private Color overrideForeColor = Color.Black;
    private Font overrideFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular);
    private const int borderWidth = 1;
    private bool useThemeBackColor;
    private bool enableBorderHighlight;

    [Browsable(false)]
    public vTextBoxBase TextBox
    {
      get
      {
        return this.txtBox;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether [text box auto layout].
    /// </summary>
    /// <value><c>true</c> if [text box auto layout]; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    [DefaultValue(true)]
    public bool TextBoxAutoLayout
    {
      get
      {
        return this.textBoxAutoLayout;
      }
      set
      {
        this.textBoxAutoLayout = value;
      }
    }

    /// <summary>Gets or sets the theme.</summary>
    /// <value>The theme.</value>
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
        if (this.theme == value)
          return;
        this.theme = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets whether to use the theme's border color.</summary>
    [Description("Gets or sets whether to use the theme's border color.")]
    [Category("Appearance")]
    [DefaultValue(true)]
    public bool UseThemeBorderColor
    {
      get
      {
        return this.useThemeBorderColor;
      }
      set
      {
        this.useThemeBorderColor = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the theme's background color.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to use the theme's background color.")]
    public bool UseThemeBackColor
    {
      get
      {
        return this.useThemeBackColor;
      }
      set
      {
        this.useThemeBackColor = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the theme's foreground color.
    /// </summary>
    [Description("Gets or sets a value indicating whether to use the theme's foreground color.")]
    [Category("Appearance")]
    public bool UseThemeForeColor
    {
      get
      {
        return this.useThemeForeColor;
      }
      set
      {
        this.useThemeForeColor = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the theme's font.
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to use the theme's font.")]
    [DefaultValue(true)]
    public bool UseThemeFont
    {
      get
      {
        return this.useThemeFont;
      }
      set
      {
        this.useThemeFont = value;
      }
    }

    /// <summary>Gets or sets the color of the override border.</summary>
    /// <value>The color of the override border.</value>
    [Description("Gets or sets the color of the override border.")]
    [Category("Appearance")]
    public Color OverrideBorderColor
    {
      get
      {
        return this.overrideBorderColor;
      }
      set
      {
        this.overrideBorderColor = value;
      }
    }

    /// <summary>Gets or sets the color of the override forecolor.</summary>
    /// <value>The color of the override fore.</value>
    [Description("Gets or sets the color of the override forecolor.")]
    [Category("Appearance")]
    public Color OverrideForeColor
    {
      get
      {
        return this.overrideForeColor;
      }
      set
      {
        this.overrideForeColor = value;
      }
    }

    /// <summary>Gets or sets the color of the override back color.</summary>
    /// <value>The color of the override back.</value>
    [Category("Appearance")]
    [Description("Gets or sets the color of the override back color.")]
    public Color OverrideBackColor
    {
      get
      {
        return this.BackColor;
      }
      set
      {
        this.BackColor = value;
      }
    }

    /// <summary>Gets or sets the override font.</summary>
    /// <value>The override font.</value>
    [Description("Gets or sets the color of the override Font.")]
    [Category("Appearance")]
    public Font OverrideFont
    {
      get
      {
        return this.overrideFont;
      }
      set
      {
        this.overrideFont = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the border will be highlighted when the user moves the mouse over the control and when the control has focus
    /// </summary>
    [Category("Behavior")]
    [Browsable(true)]
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public override bool Focused
    {
      get
      {
        if (!base.Focused)
          return this.txtBox.Focused;
        return true;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual bool HideSelection
    {
      get
      {
        return this.txtBox.HideSelection;
      }
      set
      {
        this.txtBox.HideSelection = value;
      }
    }

    [DefaultValue(16384)]
    [Category("Behavior")]
    public virtual int MaxLength
    {
      get
      {
        return this.txtBox.MaxLength;
      }
      set
      {
        this.txtBox.MaxLength = value;
      }
    }

    [Category("Behavior")]
    [DefaultValue(false)]
    public virtual bool ReadOnly
    {
      get
      {
        return this.txtBox.ReadOnly;
      }
      set
      {
        this.txtBox.ReadOnly = value;
        this.txtBox.BackColor = this.BackColor;
        this.Invalidate();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public virtual int SelectionLength
    {
      get
      {
        return this.txtBox.SelectionLength;
      }
      set
      {
        this.txtBox.SelectionLength = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual int SelectionStart
    {
      get
      {
        return this.txtBox.SelectionStart;
      }
      set
      {
        this.txtBox.SelectionStart = value;
      }
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual string SelectedText
    {
      get
      {
        return this.txtBox.SelectedText;
      }
      set
      {
        this.txtBox.SelectedText = value;
      }
    }

    [Category("Action")]
    public event PaintEventHandler PaintEditBox;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.Utilities.vEditorControlBase" /> class.
    /// </summary>
    public vEditorControlBase()
    {
      this.Width = 80;
      this.Height = 22;
      this.BackColor = Color.White;
      this.WireTextBoxEvents();
      this.SetStyle(ControlStyles.Selectable, false);
      this.Controls.Add((Control) this.txtBox);
      this.PerformEditLayout();
    }

    /// <summary>Sets the text box control.</summary>
    /// <param name="textBox">The text box.</param>
    public void SetTextBoxControl(vTextBoxBase textBox)
    {
      this.txtBox.Hide();
      this.Controls.Remove((Control) this.txtBox);
      this.txtBox = (vTextBoxBase) null;
      this.txtBox = textBox;
      this.Controls.Add((Control) this.txtBox);
      this.WireTextBoxEvents();
      this.PerformEditLayout();
    }

    internal void WireTextBoxEvents()
    {
      this.txtBox.BorderStyle = BorderStyle.None;
      this.txtBox.Click += new EventHandler(this.TextBox_Click);
      this.txtBox.DoubleClick += new EventHandler(this.TextBox_DoubleClick);
      this.txtBox.MouseUp += new MouseEventHandler(this.TextBox_MouseUp);
      this.txtBox.MouseDown += new MouseEventHandler(this.TextBox_MouseDown);
      this.txtBox.MouseHover += new EventHandler(this.TextBox_MouseHover);
      this.txtBox.MouseMove += new MouseEventHandler(this.TextBox_MouseMove);
      this.txtBox.MouseEnter += new EventHandler(this.TextBox_MouseEnter);
      this.txtBox.MouseLeave += new EventHandler(this.TextBox_MouseLeave);
      this.txtBox.GotFocus += new EventHandler(this.TextBox_GotFocus);
      this.txtBox.LostFocus += new EventHandler(this.TextBox_LostFocus);
      this.txtBox.SizeChanged += new EventHandler(this.TextBox_SizeChanged);
      this.txtBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
      this.txtBox.KeyDown += new KeyEventHandler(this.TextBox_KeyDown);
      this.txtBox.KeyPress += new KeyPressEventHandler(this.TextBox_KeyPress);
      this.txtBox.KeyUp += new KeyEventHandler(this.TextBox_KeyUp);
    }

    /// <summary>Inners the rect.</summary>
    /// <returns></returns>
    protected virtual Rectangle InnerRect()
    {
      return new Rectangle(2, 2, this.Width - 4, this.Height - 4);
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

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnBackColorChanged(EventArgs e)
    {
      base.OnBackColorChanged(e);
      this.txtBox.BackColor = this.BackColor;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnClick(EventArgs e)
    {
      base.OnClick(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnDoubleClick(EventArgs e)
    {
      base.OnDoubleClick(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      this.Invalidate();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnGotFocus(EventArgs e)
    {
      this.txtBox.Focus();
      base.OnGotFocus(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseEnter(EventArgs e)
    {
      base.OnMouseEnter(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseHover(EventArgs e)
    {
      base.OnMouseHover(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      if (this.ReadOnly)
        this.HoverState = false;
      VisualStyle visualStyle = this.theme.StyleNormal;
      if (this.HoverState || this.Focused)
        visualStyle = this.theme.StyleHighlight;
      if (!this.Enabled)
        visualStyle = this.theme.StyleDisabled;
      Color color1 = this.useThemeBackColor ? visualStyle.FillStyle.Colors[0] : this.BackColor;
      Color color2 = this.overrideBorderColor;
      if (this.useThemeBorderColor)
      {
        color2 = !this.HoverState && !this.Focused || !this.enableBorderHighlight ? this.theme.StyleNormal.BorderColor : this.theme.StyleHighlight.BorderColor;
        if (!this.Enabled)
          color2 = this.theme.StyleDisabled.BorderColor;
      }
      this.txtBox.Font = this.useThemeFont ? visualStyle.Font : this.overrideFont;
      this.txtBox.ForeColor = this.useThemeForeColor ? visualStyle.TextColor : this.overrideForeColor;
      if (this.UseThemeForeColor)
      {
        Color color3 = this.theme.QueryColorSetter("ControlBaseTextColor");
        if (color3 != Color.Empty)
          this.txtBox.ForeColor = color3;
      }
      this.txtBox.BackColor = color1;
      using (Brush brush = (Brush) new SolidBrush(color1))
        graphics.FillRectangle(brush, this.ClientRectangle);
      using (Pen pen = new Pen(color2))
        graphics.DrawRectangle(pen, rect);
      if (this.PaintEditBox == null)
        return;
      this.PaintEditBox((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnSizeChanged(EventArgs e)
    {
      this.PerformEditLayout();
      base.OnSizeChanged(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      this.txtBox.Text = this.Text;
    }

    /// <summary>Performs the edit layout.</summary>
    protected virtual void PerformEditLayout()
    {
      if (!this.textBoxAutoLayout)
        return;
      try
      {
        Rectangle rectangle = this.InnerRect();
        this.txtBox.Left = rectangle.Left + 1;
        this.txtBox.Width = rectangle.Width - 2;
        if (this.txtBox.AutoSize)
        {
          this.txtBox.Top = (this.Height - this.txtBox.Height) / 2;
          this.txtBox.Height = rectangle.Height;
        }
        else
          this.txtBox.Top = rectangle.Top + (rectangle.Height - this.txtBox.Height) / 2;
      }
      catch
      {
      }
    }

    private void TextBox_GotFocus(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    private void TextBox_LostFocus(object sender, EventArgs e)
    {
      this.Invalidate();
    }

    private void TextBox_MouseEnter(object sender, EventArgs e)
    {
      this.HoverState = true;
      this.Invalidate();
      this.OnMouseEnter(e);
    }

    private void TextBox_MouseLeave(object sender, EventArgs e)
    {
      this.HoverState = this.ClientRectangle.Contains(this.PointToClient(Cursor.Position));
      this.Invalidate();
      this.OnMouseLeave(e);
    }

    private void TextBox_SizeChanged(object sender, EventArgs e)
    {
      this.PerformEditLayout();
      this.Invalidate();
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
      this.Text = this.txtBox.Text;
    }
  }
}
