// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRibbonGroupFooter
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
  [ToolboxItem(false)]
  [Designer("VIBlend.WinForms.Controls.Design.RibbonGroupFooterDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vRibbonGroupFooter : Control, IScrollableControlBase
  {
    private ContentAlignment textAlignment = ContentAlignment.MiddleCenter;
    private vRibbonFooterButton footerButton = new vRibbonFooterButton();
    private bool useThemeTextColor = true;
    private bool footerButtonVisible = true;
    private bool allowAnimations = true;
    private string styleKey = "RibbonGroupFooter";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private PaintHelper helper = new PaintHelper();
    private vRibbonGroupContent item;
    internal BackgroundElement backFill;
    private AnimationManager manager;
    private ControlTheme theme;
    private ControlState state;

    /// <summary>
    /// Gets or sets a value indicating whether to use theme text color.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to use theme text color.")]
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

    /// <summary>Gets the footer button.</summary>
    /// <value>The footer button.</value>
    public vRibbonFooterButton FooterButton
    {
      get
      {
        return this.footerButton;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the footer button.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to show the footer button.")]
    public bool ShowFooterButton
    {
      get
      {
        return this.footerButtonVisible;
      }
      set
      {
        this.footerButtonVisible = value;
      }
    }

    /// <summary>Gets or sets the text alignment.</summary>
    /// <value>The text alignment.</value>
    [Category("Behavior")]
    [Description("Gets or sets the text alignment.")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.textAlignment;
      }
      set
      {
        this.textAlignment = value;
        this.Invalidate();
      }
    }

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

    [Browsable(false)]
    [DefaultValue(true)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        this.allowAnimations = value;
        this.backFill.IsAnimated = value;
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

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Appearance")]
    [Description("Gets or sets button's theme")]
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
        ControlTheme theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("RibbonGroupFooterNormal");
        if (fillStyle1 != null)
          theme.StyleNormal.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("RibbonGroupFooterHighlight");
        if (fillStyle2 != null)
          theme.StyleHighlight.FillStyle = fillStyle2;
        Color color1 = Color.Empty;
        Color color2 = this.theme.QueryColorSetter("RibbonFoooterTextColor");
        if (color2 != Color.Empty)
        {
          theme.StyleNormal.TextColor = color2;
          theme.StyleHighlight.TextColor = color2;
        }
        this.backFill.LoadTheme(theme);
        this.backFill.MaxFrameRate = 12;
        this.backFill.IsAnimated = true;
        this.Invalidate();
      }
    }

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
        this.footerButton.VIBlendTheme = this.defaultTheme;
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
    public override Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        this.backFill.Font = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the ribbon group.</summary>
    /// <value>The ribbon group.</value>
    public vRibbonGroupContent RibbonGroup
    {
      get
      {
        return this.item;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vRibbonGroupFooter" /> class.
    /// </summary>
    /// <param name="item">The item.</param>
    public vRibbonGroupFooter(vRibbonGroupContent item)
    {
      this.Text = "Ribbon Group";
      this.item = item;
      this.Size = new Size(0, item.FooterHeight);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.footerButton.Text = "";
      this.footerButton.Size = new Size(14, 14);
      this.Controls.Add((Control) this.footerButton);
      this.footerButton.Visible = this.ShowFooterButton;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      if (this.footerButton != null)
      {
        this.footerButton.Bounds = new Rectangle(this.Width - 17, this.Height / 2 - 7, 14, 14);
        this.footerButton.Visible = this.ShowFooterButton;
      }
      base.OnLayout(levent);
    }

    private void ResetTextAlignment()
    {
      this.TextAlignment = ContentAlignment.MiddleCenter;
    }

    private bool ShouldSerializeTextAlignment()
    {
      return this.TextAlignment != ContentAlignment.MiddleCenter;
    }

    /// <summary>
    /// Raises the <see cref="E:MouseMove" /> event.
    /// </summary>
    /// <param name="mevent">The <see cref="T:System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
    protected override void OnMouseMove(MouseEventArgs mevent)
    {
      if (this.ClientRectangle.Contains(mevent.Location) && !this.Capture)
      {
        this.state = ControlState.Hover;
        this.Invalidate();
      }
      base.OnMouseMove(mevent);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      this.state = ControlState.Normal;
      this.Invalidate();
      base.OnMouseLeave(e);
    }

    /// <summary>
    /// Raises the <see cref="E:MouseDown" /> event.
    /// </summary>
    /// <param name="mevent">The <see cref="T:System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
    protected override void OnMouseDown(MouseEventArgs mevent)
    {
      this.Capture = true;
      this.Invalidate();
      base.OnMouseDown(mevent);
    }

    /// <summary>
    /// Raises the <see cref="E:MouseUp" /> event.
    /// </summary>
    /// <param name="mevent">The <see cref="T:System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      this.Capture = false;
      this.state = !this.ClientRectangle.Contains(mevent.Location) ? ControlState.Normal : ControlState.Hover;
      this.Invalidate();
      base.OnMouseUp(mevent);
    }

    private bool IsMouseOver()
    {
      Point client = this.PointToClient(Cursor.Position);
      if (this.RibbonGroup.ContentPanel.RectangleToScreen(this.RibbonGroup.ContentPanel.ClientRectangle).Contains(Cursor.Position))
        return true;
      return this.ClientRectangle.Contains(client);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      this.state = this.IsMouseOver() ? ControlState.Hover : ControlState.Normal;
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      byte num1 = 15;
      byte num2 = 15;
      byte num3 = (byte) ((uint) num1 & 12U);
      byte num4 = (byte) ((uint) num2 & 3U);
      this.backFill.RoundedCornersBitmask = num3;
      this.backFill.Radius = this.RibbonGroup.Radius;
      this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      if (this.item.RibbonGroup.GroupStyle == RibbonStyle.Office2007)
      {
        this.backFill.DrawElementFill(e.Graphics, this.state);
        this.backFill.Bounds = new Rectangle(0, -1, this.ClientRectangle.Width - 1, this.ClientRectangle.Height);
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      }
      this.backFill.SetLastState(this.state);
      Rectangle rectangle = new Rectangle(this.backFill.Bounds.X + 5, this.backFill.Bounds.Y, this.backFill.Bounds.Width - 10, this.backFill.Bounds.Height);
      StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.TextAlignment, true, false);
      using (SolidBrush solidBrush = new SolidBrush(this.backFill.ForeColor))
      {
        if (!this.UseThemeTextColor)
          solidBrush.Color = this.ForeColor;
        e.Graphics.DrawString(this.Text, this.Font, (Brush) solidBrush, (RectangleF) rectangle, stringFormat);
      }
      e.Graphics.SmoothingMode = smoothingMode;
    }
  }
}
