// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vMaskedTextBox
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
  /// <summary>Represents a vMaskedTextBox control</summary>
  /// <remarks>
  /// Displays an editable text field, which contains a mask that allows the control to distinguish between proper and improper input.
  /// </remarks>
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vMaskBoxDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Displays an editable text field, which contains a mask that allows the control to distinguish between proper and improper input.")]
  [ToolboxBitmap(typeof (vMaskedTextBox), "ControlIcons.MaskedTextBox.ico")]
  public class vMaskedTextBox : Control, IScrollableControlBase
  {
    private MaskedTextBox textBox = new MaskedTextBox();
    private int gleamWidth = 1;
    private DefaultMasks defaultMask = DefaultMasks.Custom;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private int gleamRadius;
    private AnimationManager animManager;
    private bool hover;
    private BackgroundElement backGround;
    private ControlTheme theme;
    private bool syncBackColorWithParentBackColor;

    /// <summary>
    /// Gets or sets the default mask of the vMaskedTextBox control.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets the default mask of the vMaskedTextBox control.")]
    public DefaultMasks DefaultMask
    {
      get
      {
        return this.defaultMask;
      }
      set
      {
        this.defaultMask = value;
        this.SetMask();
      }
    }

    /// <summary>Gets or sets the radius of the gleam.</summary>
    [Category("Appearance")]
    [Description("Gets or sets the width of the gleam.")]
    public int GleamRadius
    {
      get
      {
        return this.gleamRadius;
      }
      set
      {
        this.gleamRadius = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the width of the gleam.</summary>
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

    /// <summary>
    /// Gets a reference to the TextBox of the vMaskBox control
    /// </summary>
    /// <value>The text box.</value>
    [Browsable(false)]
    public MaskedTextBox TextBox
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

    /// <summary>Gets or sets the prompt char.</summary>
    /// <value>The prompt char.</value>
    [Category("Behavior")]
    public char PromptChar
    {
      get
      {
        return this.textBox.PromptChar;
      }
      set
      {
        this.textBox.PromptChar = value;
      }
    }

    /// <summary>Gets or sets the mask.</summary>
    /// <value>The mask.</value>
    [Description("Gets or sets the control's mask string")]
    [Category("Behavior")]
    [Localizable(true)]
    [DefaultValue("")]
    [MergableProperty(false)]
    public string Mask
    {
      get
      {
        return this.textBox.Mask;
      }
      set
      {
        this.textBox.Mask = value;
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
    /// Gets or sets a value indicating whether to sync the back color with parent's back color.
    /// </summary>
    [Description("Gets or sets a value indicating whether to sync the back color with parent's back color.")]
    [Category("Appearance")]
    public bool SyncBackColorWithParentBackColor
    {
      get
      {
        return this.syncBackColorWithParentBackColor;
      }
      set
      {
        this.syncBackColorWithParentBackColor = value;
      }
    }

    static vMaskedTextBox()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>vMaskedTextBox Constructor</summary>
    public vMaskedTextBox()
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
      this.BackColor = Color.White;
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
    }

    private void SetMask()
    {
      switch (this.DefaultMask)
      {
        case DefaultMasks.Numeric:
          this.Mask = "00000";
          break;
        case DefaultMasks.PhoneNumber:
          this.Mask = "(999) 000-0000";
          break;
        case DefaultMasks.PhoneNumberNoAreaCode:
          this.Mask = "000-0000";
          break;
        case DefaultMasks.ZipCode:
          this.Mask = "00000-9999";
          break;
        case DefaultMasks.TimeUS:
          this.Mask = "90:00";
          break;
        case DefaultMasks.TimeEU:
          this.Mask = "00:00";
          break;
        case DefaultMasks.ShortDate:
          this.Mask = "00/00/0000";
          break;
        case DefaultMasks.ShortDateTime:
          this.Mask = "00/00/0000 90:00";
          break;
        case DefaultMasks.SSN:
          this.Mask = "000-00-0000";
          break;
        case DefaultMasks.Custom:
          this.Mask = "";
          break;
      }
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

    private void textBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.DesignMode || string.IsNullOrEmpty(this.Text) || !this.Text.Equals(this.Name))
        return;
      this.Text = "Set the Mask property.";
    }

    private void textBox_MouseDown(object sender, MouseEventArgs e)
    {
      this.hover = true;
      this.Invalidate();
    }

    private void TextBox_Layout(object sender, LayoutEventArgs e)
    {
      this.textBox.Location = new Point(this.gleamWidth, (this.Height - this.textBox.Height) / 2);
      this.textBox.Width = this.Width - 2 * this.gleamWidth;
    }

    /// <exclude />
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      if (this.Width == 0 || this.Height == 0)
        return;
      this.textBox.ForeColor = this.ForeColor;
      this.textBox.BackColor = this.BackColor;
      this.textBox.Font = this.Font;
      this.textBox.RightToLeft = this.RightToLeft;
      if (this.Parent != null && this.SyncBackColorWithParentBackColor)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.Parent.BackColor))
          e.Graphics.FillRectangle((Brush) solidBrush, this.ClientRectangle);
      }
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      Color borderColor;
      Color color = borderColor = this.theme.StyleNormal.BorderColor;
      if (this.hover)
        color = this.theme.StyleHighlight.BorderColor;
      if (this.textBox.Focused)
        color = this.theme.StylePressed.BorderColor;
      if (this.Readonly || !this.Enabled)
        color = this.theme.StyleDisabled.BorderColor;
      Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
      LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rectangle, color, color, LinearGradientMode.Vertical);
      PaintHelper paintHelper = new PaintHelper();
      --rectangle.Width;
      --rectangle.Height;
      GraphicsPath roundedPathRect = paintHelper.GetRoundedPathRect(rectangle, this.gleamRadius);
      e.Graphics.DrawPath(new Pen((Brush) linearGradientBrush, (float) this.gleamWidth), roundedPathRect);
      e.Graphics.SmoothingMode = smoothingMode;
    }
  }
}
