// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vToolTip
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
  /// <summary>Represents a vTooltip control.</summary>
  [ToolboxBitmap(typeof (vToolTip), "ControlIcons.vTooltip.ico")]
  [ToolboxItem(true)]
  [Description("Represents a vTooltip control.")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vToolTip : ScrollableControlMiniBase
  {
    private ContentAlignment headerTextAlignment = ContentAlignment.MiddleCenter;
    private ContentAlignment descriptionTextAlignment = ContentAlignment.MiddleCenter;
    private ContentAlignment footerTextAlignment = ContentAlignment.MiddleCenter;
    private ContentAlignment imageAlignment = ContentAlignment.MiddleCenter;
    private int autoPopDelay = 2500;
    private int initialDelay = 800;
    private SizeF footerRelativeSizeProportion = new SizeF(1f, 0.2f);
    private SizeF imageRelativeSizeProportion = new SizeF(0.3f, 0.6f);
    private SizeF headerRelativeSizeProportion = new SizeF(1f, 0.2f);
    private SizeF descriptionRelativeSizeProportion = new SizeF(0.7f, 0.6f);
    private bool drawImageBeforeText = true;
    private Padding contentMargin = new Padding(2, 2, 2, 2);
    private Timer initialDelayTimer = new Timer();
    private Timer autoPopTimer = new Timer();
    private PaintHelper helper = new PaintHelper();
    private bool canShow = true;
    private GradientStyles gradientStyle = GradientStyles.Linear;
    private bool useThemeBackColor = true;
    private ToolStripDropDownDirection dropDownShowDirection = ToolStripDropDownDirection.Default;
    private bool showBelowOwner = true;
    private string styleKey = "Tooltip";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private BackgroundElement backFill;
    private string headerText;
    private string descriptionText;
    private string footerText;
    private Image image;
    private bool drawFooterLine;
    private bool drawHeaderLine;
    private Icon footerIcon;
    private Font headerTextFont;
    private Font descriptionTextFont;
    private Font footerTextFont;
    private Color footerTextColor;
    private Color descriptionTextColor;
    private Color headerTextColor;
    private Control ownerControl;
    private Point absoluteShowPosition;
    private vToolStripDropDown dropDown;
    private ControlTheme theme;

    /// <summary>Gets or sets the gradient style.</summary>
    /// <value>The gradient style.</value>
    [Category("Appearance")]
    [Description("Gets or sets the gradient style.")]
    public GradientStyles GradientStyle
    {
      get
      {
        return this.gradientStyle;
      }
      set
      {
        this.gradientStyle = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the content margin.</summary>
    /// <value>The content margin.</value>
    [Category("Layout")]
    [Description("Gets or sets the content margin.")]
    public Padding ContentMargin
    {
      get
      {
        return this.contentMargin;
      }
      set
      {
        this.contentMargin = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the header relative size proportion.</summary>
    /// <value>The header relative size proportion.</value>
    [Category("Layout")]
    [Description("Gets or sets the header relative size proportion.")]
    public SizeF HeaderRelativeSizeProportion
    {
      get
      {
        return this.headerRelativeSizeProportion;
      }
      set
      {
        this.headerRelativeSizeProportion = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the Image relative size proportion.</summary>
    /// <value>The Image relative size proportion.</value>
    [Description("Gets or sets the decription relative size proportion.")]
    [Category("Layout")]
    public SizeF ImageRelativeSizeProportion
    {
      get
      {
        return this.imageRelativeSizeProportion;
      }
      set
      {
        this.imageRelativeSizeProportion = value;
        this.descriptionRelativeSizeProportion = new SizeF(1f - value.Width, this.descriptionRelativeSizeProportion.Height);
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the decription relative size proportion.</summary>
    /// <value>The decription relative size proportion.</value>
    [Category("Layout")]
    [Description("Gets or sets the decription relative size proportion.")]
    public SizeF DescriptionRelativeSizeProportion
    {
      get
      {
        return this.descriptionRelativeSizeProportion;
      }
      set
      {
        this.descriptionRelativeSizeProportion = value;
        this.imageRelativeSizeProportion = new SizeF(1f - value.Width, this.imageRelativeSizeProportion.Height);
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the footer relative size proportion.</summary>
    /// <value>The footer relative size proportion.</value>
    [Category("Layout")]
    [Description("Gets or sets the footer relative size proportion.")]
    public SizeF FooterRelativeSizeProportion
    {
      get
      {
        return this.footerRelativeSizeProportion;
      }
      set
      {
        this.footerRelativeSizeProportion = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the font of the text displayed by the control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.
    /// </returns>
    /// <PermissionSet>
    /// 	<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
    /// 	<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public new Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        this.headerTextFont = value;
        this.footerTextFont = value;
        this.descriptionTextFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the description text alignment.</summary>
    /// <value>The description text alignment.</value>
    [Category("Appearance")]
    [Description("Gets or sets the description text alignment.")]
    public ContentAlignment DescriptionTextAlignment
    {
      get
      {
        return this.descriptionTextAlignment;
      }
      set
      {
        this.descriptionTextAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the header text.</summary>
    /// <value>The color of the header text.</value>
    [Description("Gets or sets the color of the header text.")]
    [Category("Appearance")]
    public Color HeaderTextColor
    {
      get
      {
        return this.headerTextColor;
      }
      set
      {
        this.headerTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the description text.</summary>
    /// <value>The color of the description text.</value>
    [Description("Gets or sets the color of the description text.")]
    [Category("Appearance")]
    public Color DescriptionTextColor
    {
      get
      {
        return this.descriptionTextColor;
      }
      set
      {
        this.descriptionTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the footer text.</summary>
    /// <value>The color of the footer text.</value>
    [Description("Gets or sets the color of the header text.")]
    [Category("Appearance")]
    public Color FooterTextColor
    {
      get
      {
        return this.footerTextColor;
      }
      set
      {
        this.footerTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the footer text alignment.</summary>
    /// <value>The footer text alignment.</value>
    [Description("Gets or sets the footer text alignment.")]
    [Category("Appearance")]
    public ContentAlignment FooterTextAlignment
    {
      get
      {
        return this.footerTextAlignment;
      }
      set
      {
        this.footerTextAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the image alignment.</summary>
    /// <value>The image alignment.</value>
    [Description("Gets or sets the image alignment.")]
    [Category("Appearance")]
    public ContentAlignment ImageAlignment
    {
      get
      {
        return this.imageAlignment;
      }
      set
      {
        this.imageAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the header text alignment.</summary>
    /// <value>The header text alignment.</value>
    [Category("Appearance")]
    [Description("Gets or sets the header text alignment.")]
    public ContentAlignment HeaderTextAlignment
    {
      get
      {
        return this.headerTextAlignment;
      }
      set
      {
        this.headerTextAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the image.</summary>
    /// <value>The image.</value>
    [Description("Gets or sets the image.")]
    [Category("Behavior")]
    [DefaultValue(null)]
    public Image Image
    {
      get
      {
        return this.image;
      }
      set
      {
        this.image = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the backcolor from the theme
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to use the backcolor from the theme.")]
    [DefaultValue(true)]
    public bool UseThemeBackColor
    {
      get
      {
        return this.useThemeBackColor;
      }
      set
      {
        this.useThemeBackColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to draw the image before the text
    /// </summary>
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to draw the image before the text")]
    [Category("Appearance")]
    public bool DrawImageBeforeText
    {
      get
      {
        return this.drawImageBeforeText;
      }
      set
      {
        this.drawImageBeforeText = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to draw a Header line.
    /// </summary>
    [Description("Gets or sets a value indicating whether to draw a Header line.")]
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool DrawHeaderLine
    {
      get
      {
        return this.drawHeaderLine;
      }
      set
      {
        this.drawHeaderLine = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to draw a footer line.
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to draw a footer line.")]
    public bool DrawFooterLine
    {
      get
      {
        return this.drawFooterLine;
      }
      set
      {
        this.drawFooterLine = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the footer icon.</summary>
    /// <value>The footer icon.</value>
    [DefaultValue(null)]
    [Category("Behavior")]
    [Description("Gets or sets the footer icon.")]
    public Icon FooterIcon
    {
      get
      {
        return this.footerIcon;
      }
      set
      {
        this.footerIcon = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the header text.</summary>
    /// <value>The header text.</value>
    [Description("Gets or sets the header text.")]
    [Category("Behavior")]
    public string HeaderText
    {
      get
      {
        return this.headerText;
      }
      set
      {
        this.headerText = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the description text.</summary>
    /// <value>The description text.</value>
    [Category("Behavior")]
    [Description("Gets or sets the description text.")]
    public string DescriptionText
    {
      get
      {
        return this.descriptionText;
      }
      set
      {
        this.descriptionText = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the footer text.</summary>
    /// <value>The footer text.</value>
    [Category("Behavior")]
    [Description("Gets or sets the footer text.")]
    public string FooterText
    {
      get
      {
        return this.footerText;
      }
      set
      {
        this.footerText = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the header text font.</summary>
    /// <value>The header text font.</value>
    [Category("Behavior")]
    [Description("Gets or sets the header text font.")]
    public Font HeaderTextFont
    {
      get
      {
        return this.headerTextFont;
      }
      set
      {
        this.headerTextFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the description text font.</summary>
    /// <value>The description text font.</value>
    [Category("Behavior")]
    [Description("Gets or sets the description text font.")]
    public Font DescriptionTextFont
    {
      get
      {
        return this.descriptionTextFont;
      }
      set
      {
        this.descriptionTextFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the footer text font.</summary>
    /// <value>The footer text font.</value>
    [Description("Gets or sets the footer text font.")]
    [Category("Behavior")]
    public Font FooterTextFont
    {
      get
      {
        return this.footerTextFont;
      }
      set
      {
        this.footerTextFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the auto pop delay.</summary>
    /// <value>The auto pop delay.</value>
    [Category("Behavior")]
    [DefaultValue(0)]
    [Description("Gets or sets the auto pop delay.")]
    public int AutoPopDelay
    {
      get
      {
        return this.autoPopDelay;
      }
      set
      {
        this.autoPopDelay = value;
        if (value <= 0)
          return;
        this.autoPopTimer.Interval = value;
      }
    }

    /// <summary>Gets or sets the initial delay.</summary>
    /// <value>The initial delay.</value>
    [DefaultValue(0)]
    [Description("Gets or sets the initial delay.")]
    [Category("Behavior")]
    public int InitialDelay
    {
      get
      {
        return this.initialDelay;
      }
      set
      {
        this.initialDelay = value;
        if (value <= 0)
          return;
        this.initialDelayTimer.Interval = value;
      }
    }

    /// <summary>Gets or sets the absolute show position.</summary>
    /// <value>The absolute show position.</value>
    [Category("Behavior")]
    [Description("Gets or sets the absolute show position.")]
    public Point AbsoluteShowPosition
    {
      get
      {
        return this.absoluteShowPosition;
      }
      set
      {
        this.absoluteShowPosition = value;
      }
    }

    /// <summary>Gets or sets the owner control.</summary>
    /// <value>The owner control.</value>
    [DefaultValue(null)]
    [Category("Behavior")]
    [Description("Gets or sets the owner control.")]
    public Control OwnerControl
    {
      get
      {
        return this.ownerControl;
      }
      set
      {
        if (this.ownerControl != null)
        {
          this.ownerControl.MouseEnter -= new EventHandler(this.value_MouseEnter);
          this.ownerControl.MouseLeave -= new EventHandler(this.value_MouseLeave);
        }
        this.ownerControl = value;
        if (value == null)
          return;
        if (!this.DesignMode)
          this.Visible = false;
        value.MouseEnter += new EventHandler(this.value_MouseEnter);
        value.MouseLeave += new EventHandler(this.value_MouseLeave);
      }
    }

    /// <summary>Gets or sets the drop down direction.</summary>
    /// <value>The drop down show direction.</value>
    [Category("Behavior")]
    [Description("Gets or sets the drop down direction.")]
    public ToolStripDropDownDirection DropDownShowDirection
    {
      get
      {
        return this.dropDownShowDirection;
      }
      set
      {
        this.dropDownShowDirection = value;
      }
    }

    /// <summary>
    /// Gets or sets whether to show the drop down below the owner control.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets whether to show the drop down below the owner control.")]
    public bool ShowBelowOwner
    {
      get
      {
        return this.showBelowOwner;
      }
      set
      {
        this.showBelowOwner = value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is shown.
    /// </summary>
    /// <value><c>true</c> if this instance is shown; otherwise, <c>false</c>.</value>
    [Browsable(false)]
    public bool IsShown
    {
      get
      {
        if (this.dropDown != null)
          return this.dropDown.Visible;
        return false;
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
    [Description("Gets or sets the theme of the control.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    [Browsable(false)]
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
        FillStyle fillStyle = this.theme.QueryFillStyleSetter("ToolTipBackground");
        if (fillStyle != null)
          this.theme.StyleNormal.FillStyle = fillStyle;
        this.backFill.LoadTheme(this.theme);
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

    static vToolTip()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vToolTip" /> class.
    /// </summary>
    public vToolTip()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.backFill.Owner = "vToolTip";
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.AllowAnimations = true;
      this.FooterTextFont = this.Font;
      this.DescriptionTextFont = this.Font;
      this.HeaderTextFont = this.Font;
      this.descriptionText = "Description Text";
      this.footerText = "Footer Text";
      this.headerText = "Header Text";
      this.headerTextColor = this.ForeColor;
      this.footerTextColor = this.ForeColor;
      this.descriptionTextColor = this.ForeColor;
      this.initialDelayTimer.Tick += new EventHandler(this.initialDelayTimer_Tick);
      this.autoPopTimer.Tick += new EventHandler(this.autoPopTimer_Tick);
      this.initialDelayTimer.Interval = this.initialDelay;
      this.autoPopTimer.Interval = this.autoPopDelay;
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.ownerControl == null)
        return;
      if (this.OwnerControl.ClientRectangle.Contains(this.OwnerControl.PointToClient(Cursor.Position)))
        this.canShow = false;
      else
        this.canShow = true;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      if (this.OwnerControl == null || this.OwnerControl.ClientRectangle.Contains(this.OwnerControl.PointToClient(Cursor.Position)))
        return;
      this.canShow = true;
    }

    private void value_MouseLeave(object sender, EventArgs e)
    {
      this.canShow = true;
      this.HideTooltip();
      this.autoPopTimer.Stop();
    }

    private void value_MouseEnter(object sender, EventArgs e)
    {
      if (!this.canShow)
        return;
      this.canShow = false;
      this.initialDelayTimer.Stop();
      this.autoPopTimer.Stop();
      this.initialDelayTimer.Start();
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.autoPopTimer.Dispose();
        this.initialDelayTimer.Dispose();
      }
      base.Dispose(disposing);
    }

    private void autoPopTimer_Tick(object sender, EventArgs e)
    {
      this.HideTooltip();
      this.autoPopTimer.Stop();
    }

    private void initialDelayTimer_Tick(object sender, EventArgs e)
    {
      this.initialDelayTimer.Stop();
      this.ShowTooltip();
      this.autoPopTimer.Start();
    }

    /// <summary>Hides the tooltip.</summary>
    public void HideTooltip()
    {
      if (this.OwnerControl == null || this.dropDown == null || !this.dropDown.Visible)
        return;
      this.dropDown.StartForwardsPlusMinusAnimation();
      this.canShow = !this.OwnerControl.ClientRectangle.Contains(this.OwnerControl.PointToClient(Cursor.Position));
      this.ownerControl.Invalidate();
    }

    private void dropDown_HideAnimationFinished(object sender, EventArgs e)
    {
      if (this.ownerControl == null)
        return;
      this.ownerControl.Invalidate();
    }

    /// <summary>Shows the tooltip.</summary>
    public void ShowTooltip()
    {
      Point point = new Point(Cursor.Position.X + 5, Cursor.Position.Y + 5);
      if (this.AbsoluteShowPosition != Point.Empty)
        point = this.AbsoluteShowPosition;
      this.ShowTooltip(point);
    }

    public void ShowTooltip(Point point, Control owner)
    {
      if (owner == null)
        return;
      this.Visible = true;
      this.MaximumSize = this.Size;
      this.dropDown = new vToolStripDropDown((Control) this);
      this.dropDown.FocusOnPopUp = false;
      this.dropDown.Content.BackColor = Color.Transparent;
      this.dropDown.BackColor = Color.Transparent;
      this.dropDown.MaximumSize = this.Size;
      this.dropDown.CanResize = false;
      this.dropDown.TopLevel = true;
      this.dropDown.HideAnimationFinished += new EventHandler(this.dropDown_HideAnimationFinished);
      if (this.ShowBelowOwner)
        this.dropDown.Show(owner, new Point(5, owner.Height + 5));
      else if (this.dropDownShowDirection == ToolStripDropDownDirection.Default)
        this.dropDown.Show(point);
      else
        this.dropDown.Show(owner, point, this.DropDownShowDirection);
      this.initialDelayTimer.Stop();
      this.autoPopTimer.Start();
    }

    public void ShowTooltip(Point point)
    {
      if (!this.OwnerControl.ClientRectangle.Contains(this.OwnerControl.PointToClient(Cursor.Position)))
        return;
      this.Visible = true;
      this.MaximumSize = this.Size;
      this.dropDown = new vToolStripDropDown((Control) this);
      this.dropDown.FocusOnPopUp = false;
      this.dropDown.Content.BackColor = Color.Transparent;
      this.dropDown.BackColor = Color.Transparent;
      this.dropDown.MaximumSize = this.Size;
      this.dropDown.CanResize = false;
      this.dropDown.TopLevel = true;
      this.dropDown.HideAnimationFinished += new EventHandler(this.dropDown_HideAnimationFinished);
      if (this.ShowBelowOwner)
        this.dropDown.Show(this.OwnerControl, new Point(5, this.OwnerControl.Height + 5));
      else if (this.dropDownShowDirection == ToolStripDropDownDirection.Default)
        this.dropDown.Show(point);
      else
        this.dropDown.Show(this.OwnerControl, point, this.DropDownShowDirection);
    }

    private void ApplyContentMargin(ref RectangleF rectangle)
    {
      rectangle = new RectangleF(rectangle.X + (float) this.contentMargin.Left, rectangle.Y + (float) this.contentMargin.Top, rectangle.Width - (float) this.contentMargin.Horizontal, rectangle.Height - (float) this.contentMargin.Vertical);
    }

    protected void DrawImage(Graphics g, Image image, Rectangle r, ContentAlignment align)
    {
      Rectangle imageRectangle = ImageAndTextHelper.GetImageRectangle(image, r, align);
      if (!this.Enabled)
        ControlPaint.DrawImageDisabled(g, image, imageRectangle.X, imageRectangle.Y, this.BackColor);
      else
        g.DrawImage(image, imageRectangle.X, imageRectangle.Y, image.Width, image.Height);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      this.backFill.Radius = 0;
      this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      Color color = this.theme.StyleNormal.FillStyle.Colors[0];
      Color color2 = this.theme.StyleNormal.FillStyle.Colors[this.theme.StyleNormal.FillStyle.ColorsNumber - 1];
      if (!this.useThemeBackColor)
        color = color2 = this.BackColor;
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.ClientRectangle, color, color2, 90f))
      {
        if (this.GradientStyle == GradientStyles.Linear)
        {
          e.Graphics.FillRectangle((Brush) linearGradientBrush, this.backFill.Bounds);
        }
        else
        {
          using (SolidBrush solidBrush = new SolidBrush(color))
            e.Graphics.FillRectangle((Brush) solidBrush, this.backFill.Bounds);
        }
      }
      this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      SizeF sizeF1 = e.Graphics.MeasureString(this.headerText, this.HeaderTextFont);
      SizeF sizeF2 = e.Graphics.MeasureString(this.footerText, this.FooterTextFont);
      e.Graphics.MeasureString(this.descriptionText, this.DescriptionTextFont);
      RectangleF rectangle1 = new RectangleF(0.0f, 0.0f, (float) this.Width * this.HeaderRelativeSizeProportion.Width, Math.Max(sizeF1.Height, (float) this.Height * this.HeaderRelativeSizeProportion.Height));
      RectangleF rectangle2 = new RectangleF(0.0f, (float) this.Height - Math.Max(sizeF2.Height, (float) this.Height * this.FooterRelativeSizeProportion.Height), (float) this.Width * this.FooterRelativeSizeProportion.Width, Math.Max(sizeF2.Height, (float) this.Height * this.FooterRelativeSizeProportion.Height));
      RectangleF rectangle3 = new RectangleF(0.0f, rectangle1.Height, (float) this.Width * this.ImageRelativeSizeProportion.Width, (float) this.Height * this.ImageRelativeSizeProportion.Height);
      RectangleF rectangle4 = new RectangleF(rectangle3.Width, rectangle1.Height, (float) this.Width * this.DescriptionRelativeSizeProportion.Width, (float) this.Height * this.DescriptionRelativeSizeProportion.Height);
      if (!this.DrawImageBeforeText)
      {
        rectangle4 = new RectangleF(0.0f, rectangle1.Height, (float) this.Width * this.DescriptionRelativeSizeProportion.Width, (float) this.Height * this.DescriptionRelativeSizeProportion.Height);
        rectangle3 = new RectangleF(rectangle4.Width, rectangle1.Height, (float) this.Width * this.ImageRelativeSizeProportion.Width, (float) this.Height * this.ImageRelativeSizeProportion.Height);
      }
      if (this.Image == null)
        rectangle4 = new RectangleF(0.0f, rectangle1.Height, (float) this.Width, (float) this.Height * this.DescriptionRelativeSizeProportion.Height);
      this.ApplyContentMargin(ref rectangle1);
      this.ApplyContentMargin(ref rectangle4);
      this.ApplyContentMargin(ref rectangle3);
      this.ApplyContentMargin(ref rectangle2);
      if (this.Image != null)
        this.DrawImage(e.Graphics, this.Image, Rectangle.Ceiling(rectangle3), this.ImageAlignment);
      using (SolidBrush solidBrush = new SolidBrush(this.headerTextColor))
      {
        StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.HeaderTextAlignment, true, false);
        e.Graphics.DrawString(this.HeaderText, this.HeaderTextFont, (Brush) solidBrush, rectangle1, stringFormat);
      }
      using (SolidBrush solidBrush = new SolidBrush(this.descriptionTextColor))
      {
        StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.DescriptionTextAlignment, true, false);
        e.Graphics.DrawString(this.DescriptionText, this.DescriptionTextFont, (Brush) solidBrush, rectangle4, stringFormat);
      }
      using (SolidBrush solidBrush = new SolidBrush(this.footerTextColor))
      {
        if (this.FooterIcon != null)
        {
          RectangleF rectangleF = new RectangleF(rectangle2.X, rectangle2.Y + (float) this.contentMargin.Top, (float) this.footerIcon.Width, (float) this.footerIcon.Height);
          rectangle2 = new RectangleF(rectangle2.X + rectangleF.Right + (float) this.contentMargin.Left, rectangle2.Y, rectangle2.Width - rectangleF.Right - (float) this.contentMargin.Left, rectangle2.Height);
          e.Graphics.DrawIcon(this.FooterIcon, Rectangle.Ceiling(rectangleF));
        }
        StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.FooterTextAlignment, true, false);
        e.Graphics.DrawString(this.FooterText, this.FooterTextFont, (Brush) solidBrush, rectangle2, stringFormat);
      }
      using (Pen pen = new Pen(this.ForeColor))
      {
        if (this.DrawHeaderLine)
          e.Graphics.DrawLine(pen, rectangle1.Left, rectangle1.Bottom + (float) this.contentMargin.Top, rectangle1.Width, rectangle1.Bottom + (float) this.contentMargin.Top);
        if (!this.DrawFooterLine)
          return;
        e.Graphics.DrawLine(pen, (float) this.contentMargin.Left, rectangle4.Bottom, rectangle2.Width, rectangle4.Bottom);
      }
    }
  }
}
