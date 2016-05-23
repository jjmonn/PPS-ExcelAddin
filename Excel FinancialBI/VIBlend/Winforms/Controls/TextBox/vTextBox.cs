// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTextBox
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
  /// <summary>Represents a vTextBox control which</summary>
  /// <remarks>
  /// Provides an editable text field with cutomizable features.
  /// </remarks>
  [ToolboxItem(true)]
  [Description("Provides an editable text field with cutomizable features.")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vTextBox), "ControlIcons.vTextBox.ico")]
  public class vTextBox : Control, IScrollableControlBase
  {
    private vTextBoxBase textBox = new vTextBoxBase();
    private int gleamWidth = 1;
    private Size boundsOffset = new Size(1, 1);
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool paintFill = true;
    private bool paintBorder = true;
    private bool useThemeBorderColor = true;
    private Color controlBorderColor = Color.FromArgb(39, 39, 39);
    private Color controlHighlightBorderColor = Color.DimGray;
    private bool enableBorderHighlight;
    private AnimationManager animManager;
    private bool hover;
    private BackgroundElement backGround;
    private ControlTheme theme;

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

    /// <summary>
    /// Gets or sets a value indicating whether to draw default text
    /// </summary>
    [DefaultValue(true)]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Description("Gets or sets a value indicating whether to draw default text")]
    [Category("Appearance")]
    public bool ShowDefaultText
    {
      get
      {
        return this.textBox.ShowDefaultText;
      }
      set
      {
        this.textBox.ShowDefaultText = value;
      }
    }

    /// <summary>Gets or sets the default text.</summary>
    /// <value>The default text.</value>
    [Description("Gets or sets the default text.")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Appearance")]
    [Browsable(true)]
    public string DefaultText
    {
      get
      {
        return this.textBox.DefaultText;
      }
      set
      {
        this.textBox.DefaultText = value;
      }
    }

    /// <summary>Gets or sets the default color of the text.</summary>
    /// <value>The default color of the text.</value>
    [Description("Gets or sets the default color of the text.")]
    [Browsable(true)]
    [DefaultValue(typeof (Color), "Gray")]
    [EditorBrowsable(EditorBrowsableState.Always)]
    [Category("Appearance")]
    public Color DefaultTextColor
    {
      get
      {
        return this.textBox.DefaultTextColor;
      }
      set
      {
        this.textBox.DefaultTextColor = value;
      }
    }

    /// <summary>Gets or sets the default text font.</summary>
    /// <value>The default text font.</value>
    [Description("Gets or sets the default text font.")]
    [Category("Appearance")]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Always)]
    public Font DefaultTextFont
    {
      get
      {
        return this.textBox.DefaultTextFont;
      }
      set
      {
        this.textBox.DefaultTextFont = value;
      }
    }

    /// <summary>Gets or sets the width of the gleam.</summary>
    /// <value>The width of the gleam.</value>
    [DefaultValue(1)]
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
    [Browsable(false)]
    public vTextBoxBase TextBox
    {
      get
      {
        return this.textBox;
      }
    }

    /// <exclude />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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
    [EditorBrowsable(EditorBrowsableState.Never)]
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
    /// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
    /// </summary>
    /// <value></value>
    /// <returns>true if the user can give the focus to the control using the TAB key; otherwise, false. The default is true.
    /// Note:
    /// This property will always return
    /// true for an instance of the <see cref="T:System.Windows.Forms.Form" /> class.
    /// </returns>
    /// <PermissionSet>
    /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    [Category("Behavior")]
    public new bool TabStop
    {
      get
      {
        if (this.textBox == null)
          return false;
        return base.TabStop;
      }
      set
      {
        if (this.textBox == null)
          return;
        this.textBox.TabStop = value;
        base.TabStop = value;
      }
    }

    /// <summary>
    /// Gets or sets the tab order of the control within its container.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The index value of the control within the set of controls within its container. The controls in the container are included in the tab order.
    /// </returns>
    [Category("Behavior")]
    public new int TabIndex
    {
      get
      {
        if (this.textBox == null)
          return -1;
        return base.TabIndex;
      }
      set
      {
        if (this.textBox == null)
          return;
        this.textBox.TabIndex = value;
        base.TabIndex = value;
      }
    }

    /// <summary>Gets or sets whether the textbox has scrollbars.</summary>
    [Category("Appearance")]
    public ScrollBars ScrollBars
    {
      get
      {
        return this.textBox.ScrollBars;
      }
      set
      {
        this.textBox.ScrollBars = value;
      }
    }

    /// <summary>Gets or sets textbox's password character.</summary>
    [Category("Behavior")]
    public char PasswordChar
    {
      get
      {
        return this.textBox.PasswordChar;
      }
      set
      {
        this.textBox.PasswordChar = value;
        this.Invalidate();
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
    [Description("Gets or sets whether the vTextBox has multiple lines.")]
    [Category("Behavior")]
    [DefaultValue(false)]
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

    /// <summary>Gets or sets the text alignment.</summary>
    [Description("Gets or sets the text alignment.")]
    [Category("Behavior")]
    public HorizontalAlignment TextAlign
    {
      get
      {
        return this.textBox.TextAlign;
      }
      set
      {
        this.textBox.TextAlign = value;
      }
    }

    [Category("Behavior")]
    [Browsable(false)]
    public int SelectionStart
    {
      get
      {
        return this.textBox.SelectionStart;
      }
      set
      {
        this.textBox.SelectionStart = value;
      }
    }

    [Category("Behavior")]
    [Browsable(false)]
    public int SelectionLength
    {
      get
      {
        return this.textBox.SelectionLength;
      }
      set
      {
        this.textBox.SelectionLength = value;
      }
    }

    /// <summary>
    /// Gets or sets whether the vTextBox is in read only mode.
    /// </summary>
    [DefaultValue(false)]
    [Category("Behavior")]
    [Description("Gets or sets whether the vTextBox is in read only mode.")]
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

    /// <summary>Gets or sets the bounds offset.</summary>
    [Category("Appearance")]
    [Description("Gets or sets the bounds offset.")]
    public Size BoundsOffset
    {
      get
      {
        return this.boundsOffset;
      }
      set
      {
        this.boundsOffset = value;
        this.PerformLayout();
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
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to paint a border")]
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
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use the ControlBorderColor")]
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
    [Category("Appearance")]
    [Description("Gets or sets the BorderColor of the TextBox.")]
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
    [DefaultValue(typeof (Color), "DimGray")]
    [Description("Gets or sets the Highlight BorderColor of the TextBox.")]
    [Category("Appearance")]
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

    static vTextBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Constructor</summary>
    public vTextBox()
    {
      this.Controls.Add((Control) this.textBox);
      this.BackColor = Color.White;
      this.Height = this.textBox.Height = 22;
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
      this.textBox.MouseDown += new MouseEventHandler(this.textBoxBase_MouseDown);
      this.textBox.MouseUp += new MouseEventHandler(this.textBoxBase_MouseUp);
      this.textBox.MouseWheel += new MouseEventHandler(this.textBox_MouseWheel);
      this.textBox.KeyDown += new KeyEventHandler(this.textBox_KeyDown);
      this.textBox.KeyUp += new KeyEventHandler(this.textBox_KeyUp);
      this.textBox.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
      this.textBox.GotFocus += new EventHandler(this.textBox_GotFocus);
      this.textBox.LostFocus += new EventHandler(this.textBox_LostFocus);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
    }

    private void textBox_LostFocus(object sender, EventArgs e)
    {
      this.OnLostFocus(e);
    }

    private void textBox_GotFocus(object sender, EventArgs e)
    {
      this.OnGotFocus(e);
    }

    private void textBox_MouseWheel(object sender, MouseEventArgs e)
    {
      this.OnMouseWheel(e);
    }

    private void textBox_KeyPress(object sender, KeyPressEventArgs e)
    {
      this.OnKeyPress(e);
    }

    private void textBox_KeyUp(object sender, KeyEventArgs e)
    {
      this.OnKeyUp(e);
    }

    private void textBoxBase_MouseDown(object sender, MouseEventArgs e)
    {
      this.OnMouseDown(e);
    }

    private void textBoxBase_MouseUp(object sender, MouseEventArgs e)
    {
      this.OnMouseUp(e);
    }

    private void textBox_KeyDown(object sender, KeyEventArgs e)
    {
      this.OnKeyDown(e);
    }

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      this.OnTextChanged(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
      if (this.Enabled)
        return;
      this.Invalidate();
    }

    private void ResetDefaultTextFont()
    {
      this.DefaultTextFont = Control.DefaultFont;
    }

    private bool ShouldSerializeDefaultTextFont()
    {
      return this.DefaultTextFont != Control.DefaultFont;
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
      this.textBox.Invalidate();
    }

    private void vTextBox_MouseEnter(object sender, EventArgs e)
    {
      this.hover = true;
      this.Invalidate();
    }

    private void vTextBox_MouseLeave(object sender, EventArgs e)
    {
      this.hover = false;
      this.Invalidate();
    }

    /// <summary>selects the whole text.</summary>
    public void SelectAll()
    {
      this.textBox.SelectAll();
    }

    private void textBox_MouseDown(object sender, MouseEventArgs e)
    {
      this.hover = true;
      this.Invalidate();
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (!this.Visible)
        return;
      this.ApplyLayout();
    }

    private void TextBox_Layout(object sender, LayoutEventArgs e)
    {
      this.ApplyLayout();
    }

    private void ApplyLayout()
    {
      if (this.Multiline)
      {
        this.textBox.Height = this.Height - 2 * this.gleamWidth - 2 * this.boundsOffset.Height;
        this.textBox.Width = this.Width - 2 * this.gleamWidth - 2 * this.boundsOffset.Width;
        this.textBox.Location = new Point(this.gleamWidth + this.boundsOffset.Width, this.boundsOffset.Height + this.gleamWidth);
      }
      else
      {
        this.textBox.Location = new Point(this.gleamWidth + this.boundsOffset.Width, (this.Height - this.textBox.Height) / 2);
        this.textBox.Width = this.Width - 2 * this.gleamWidth - 2 * this.boundsOffset.Width;
      }
    }

    /// <summary>Draws the text.</summary>
    /// <param name="g">The g.</param>
    protected virtual void DrawText(Graphics g)
    {
      TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;
      Rectangle clientRectangle = this.ClientRectangle;
      TextFormatFlags flags = textFormatFlags;
      int num = this.Height / 2 - (int) g.MeasureString(this.Text, this.Font).Height / 2;
      clientRectangle.Offset(1, 1 + num);
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
      Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
      e.Graphics.FillRectangle((Brush) new SolidBrush(this.textBox.BackColor), rectangle);
      this.textBox.Visible = this.Enabled;
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
