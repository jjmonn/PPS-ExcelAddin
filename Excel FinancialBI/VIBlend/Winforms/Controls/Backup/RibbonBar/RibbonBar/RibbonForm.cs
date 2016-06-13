// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.RibbonForm
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a Form that supports VIBlend Themes.</summary>
  public class RibbonForm : vForm
  {
    private string styleKey = "RibbonForm";
    private bool hasRibbon;

    /// <summary>
    /// Gets or sets a value indicating whether this instance has ribbon.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has ribbon; otherwise, <c>false</c>.
    /// </value>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether this instance has ribbon bar")]
    public bool HasRibbon
    {
      get
      {
        return this.hasRibbon;
      }
      set
      {
        this.hasRibbon = value;
        if (value)
          this.TitleHeight = 1;
        else
          this.TitleHeight = 29;
        this.Controls.SetChildIndex((Control) this.minimizeButton, 0);
        this.Controls.SetChildIndex((Control) this.maximizeButton, 0);
        this.Controls.SetChildIndex((Control) this.closeButton, 0);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether glass effect is enabled
    /// </summary>
    /// <value></value>
    [Browsable(false)]
    internal override bool EnableGlassEffect
    {
      get
      {
        return false;
      }
      set
      {
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
    /// <value></value>
    public override ControlTheme Theme
    {
      get
      {
        return base.Theme;
      }
      set
      {
        base.Theme = value;
        ControlTheme theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        FillStyle fillStyle = theme.QueryFillStyleSetter("RibbonTitle");
        if (fillStyle != null)
          theme.StyleNormal.FillStyle = fillStyle;
        Color color = theme.QueryColorSetter("RibbonFormBorder");
        if (color != Color.Empty)
          theme.StyleNormal.BorderColor = color;
        this.titleFill.IsAnimated = false;
        this.titleFill.LoadTheme(theme);
        this.minimizeButton.VIBlendTheme = this.VIBlendTheme;
        this.maximizeButton.VIBlendTheme = this.VIBlendTheme;
        this.closeButton.VIBlendTheme = this.VIBlendTheme;
        this.maximizeButton.MinimumSize = new Size(17, 16);
        this.minimizeButton.MinimumSize = new Size(17, 16);
        this.closeButton.MinimumSize = new Size(17, 16);
        this.maximizeButton.MaximumSize = new Size(17, 16);
        this.minimizeButton.MaximumSize = new Size(17, 16);
        this.closeButton.MaximumSize = new Size(17, 16);
        if (!this.IsOffice2010Style())
          return;
        Size size = new Size(34, 19);
        this.maximizeButton.MinimumSize = size;
        this.minimizeButton.MinimumSize = size;
        this.closeButton.MinimumSize = size;
        this.maximizeButton.MaximumSize = size;
        this.minimizeButton.MaximumSize = size;
        this.closeButton.MaximumSize = size;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.RibbonForm" /> class.
    /// </summary>
    public RibbonForm()
    {
      this.maximizeButton.MinimumSize = new Size(17, 16);
      this.minimizeButton.MinimumSize = new Size(17, 16);
      this.closeButton.MinimumSize = new Size(17, 16);
      this.maximizeButton.MaximumSize = new Size(17, 16);
      this.minimizeButton.MaximumSize = new Size(17, 16);
      this.closeButton.MaximumSize = new Size(17, 16);
      this.minimizeButton.RibbonStyle = true;
      this.closeButton.RibbonStyle = true;
      this.maximizeButton.RibbonStyle = true;
      this.maximizeButton.BackColor = Color.Transparent;
      this.ShadowWidth = 0;
      this.BoundsHelper.MouseMoveArea = this.TitleRectangle;
      this.SizeChanged += new EventHandler(this.RibbonForm_SizeChanged);
      this.stopDefaultLayout = true;
    }

    protected override void InvalidateFrame()
    {
    }

    private void RibbonForm_SizeChanged(object sender, EventArgs e)
    {
      this.BoundsHelper.MouseMoveArea = this.TitleRectangle;
    }

    private bool IsOffice2010Style()
    {
      if (this.VIBlendTheme != VIBLEND_THEME.STEEL && this.VIBlendTheme != VIBLEND_THEME.OFFICE2010BLACK && this.VIBlendTheme != VIBLEND_THEME.OFFICE2010SILVER)
        return this.VIBlendTheme == VIBLEND_THEME.OFFICE2010BLUE;
      return true;
    }

    /// <summary>
    /// Raises the <see cref="E:Layout" /> event.
    /// </summary>
    /// <param name="levent">The <see cref="T:System.Windows.Forms.LayoutEventArgs" /> instance containing the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      int num1 = 2;
      int num2 = 2;
      int num3 = 62;
      if (this.IsOffice2010Style())
        num3 = 111;
      int num4 = num3;
      if (!this.MaximizeBox)
        num4 = num3 - this.maximizeButton.Width - num1 - num2;
      if (this.MinimizeBox)
      {
        this.minimizeButton.Location = new Point(this.Width - num4 - num2, num2 + 2);
        if (this.RightToLeft == RightToLeft.Yes)
          this.minimizeButton.Location = new Point(num4 + num2 - this.minimizeButton.Width, num2 + 2);
      }
      if (this.MaximizeBox)
      {
        this.maximizeButton.Location = new Point(this.Width - num4 - num2 + this.minimizeButton.Width + num1, num2 + 2);
        if (this.RightToLeft == RightToLeft.Yes)
          this.maximizeButton.Location = new Point(num4 + num2 - this.minimizeButton.Width - this.maximizeButton.Width, num2 + 2);
      }
      if (!this.CloseBox)
        return;
      if (!this.MaximizeBox)
        this.closeButton.Location = new Point(this.Width - num4 - num2 + this.minimizeButton.Width + 2 * num1, num2 + 2);
      else
        this.closeButton.Location = new Point(this.Width - num4 - num2 + this.minimizeButton.Width + this.maximizeButton.Width + 2 * num1, num2 + 2);
      if (this.RightToLeft != RightToLeft.Yes)
        return;
      this.closeButton.Location = new Point(num4 + num2 - this.minimizeButton.Width - this.maximizeButton.Width - this.closeButton.Width, num2 + 2);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (this.IsDisposed)
        return;
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      Rectangle rect1 = new Rectangle(this.Padding.Left - 1, this.Padding.Top - 1, this.Width - this.Padding.Horizontal + 1, this.Height - this.Padding.Vertical + 1);
      if (this.hasRibbon)
        rect1 = new Rectangle(this.Padding.Left - 1, 30, this.Width - this.Padding.Horizontal + 1, this.Height - this.Padding.Vertical + 1 - 30);
      int shadowOffset = this.ShadowWidth;
      int borderOffset = this.BorderWidth;
      if (this.WindowState != FormWindowState.Maximized)
      {
        RectangleF rectangleF = new RectangleF((PointF) this.PointToScreen(Point.Empty), (SizeF) this.Size);
        int shadowWidth = this.ShadowWidth;
        this.titleFill.Opacity = this.TitleOpacity;
        Rectangle rect2 = new Rectangle(this.ShadowWidth - 1, this.ShadowWidth - 1, this.Width - 2 * this.ShadowWidth + 1, this.Height - 2 * this.ShadowWidth + 1);
        double num1 = (double) this.ShadowWidth;
        using (Pen pen = new Pen(this.ShadowColor))
        {
          SolidBrush solidBrush = new SolidBrush(this.ShadowColor);
          for (int index = 0; index < this.ShadowWidth; ++index)
          {
            Color color = Color.FromArgb((int) byte.MaxValue + (int) (-255.0 * ((double) index / num1)), this.ShadowColor);
            pen.Color = color;
            solidBrush.Color = color;
            e.Graphics.DrawRectangle(pen, rect2);
            rect2.Inflate(1, 1);
          }
        }
        this.titleFill.Bounds = new Rectangle(this.TitleRectangle.X - 1, this.TitleRectangle.Y - 1, this.Width - 2 * this.ShadowWidth + 1, this.Height - 2 * this.ShadowWidth + 1);
        Rectangle rectangle1 = new Rectangle(0, 0, this.Width, this.Padding.Top);
        Rectangle rectangle2 = new Rectangle(0, this.Height - this.Padding.Bottom, this.Width, this.Padding.Bottom);
        Rectangle rectangle3 = new Rectangle(0, 0, this.Padding.Left + 1, this.Height);
        Rectangle rectangle4 = new Rectangle(this.Width - this.Padding.Right - 1, 0, this.Padding.Right + 1, this.Height);
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
          e.Graphics.FillRectangle((Brush) solidBrush, rect1);
        this.titleFill.Bounds = rectangle1;
        this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        this.titleFill.Bounds = rectangle2;
        this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        this.titleFill.Bounds = rectangle3;
        this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        this.titleFill.Bounds = rectangle4;
        this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        Rectangle rectangle5 = new Rectangle(this.TitleRectangle.X - 1, this.TitleRectangle.Y - 1, this.Width - 2 * this.ShadowWidth + 1, this.TitleHeight);
        this.titleFill.Bounds = rectangle5;
        if (this.hasRibbon)
        {
          rectangle5 = new Rectangle(this.TitleRectangle.X - 1, this.TitleRectangle.Y + 1, this.Width - 2 * this.ShadowWidth + 1, 29);
          this.titleFill.Bounds = rectangle5;
        }
        this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        using (Pen pen1 = new Pen(this.titleFill.BorderColor))
        {
          using (Pen pen2 = new Pen(ControlPaint.Light(Color.FromArgb(150, this.titleFill.BorderColor))))
          {
            Color color = this.Theme.QueryColorSetter("RibbonFormInnerBorder");
            if (!color.Equals((object) Color.Empty))
              pen2.Color = color;
            if (!this.hasRibbon)
            {
              e.Graphics.DrawRectangle(pen2, rect1);
            }
            else
            {
              Rectangle rect3 = new Rectangle(rect1.X, 138, rect1.Width, this.Height - 138 - this.BorderWidth - this.ShadowWidth);
              if (rect3.Width > 0)
              {
                if (rect3.Height > 0)
                  e.Graphics.DrawRectangle(pen2, rect3);
              }
            }
          }
          int num2 = 1;
          if (this.ShadowWidth > 0)
            num2 = 0;
          Rectangle bounds = new Rectangle(this.ShadowWidth - 2, this.ShadowWidth - 2, this.Width - 2 * this.ShadowWidth - num2 + 2, this.Height - 2 * this.ShadowWidth - num2 + 2);
          if (this.ShadowWidth < 2)
            bounds = new Rectangle(0, 0, this.Width - num2, this.Height - num2);
          GraphicsPath roundedPathRect1 = this.helper.GetRoundedPathRect(bounds, this.RoundRadius + 1);
          e.Graphics.DrawPath(pen1, roundedPathRect1);
          Color color1 = this.Theme.QueryColorSetter("RibbonFormBorder2");
          if (!color1.Equals((object) Color.Empty))
          {
            bounds.Inflate(-1, -1);
            GraphicsPath roundedPathRect2 = this.helper.GetRoundedPathRect(bounds, this.RoundRadius + 1);
            pen1.Color = color1;
            e.Graphics.DrawPath(pen1, roundedPathRect2);
          }
        }
      }
      else
      {
        this.titleFill.Bounds = new Rectangle(0, -1, this.Width, 30);
        this.titleFill.Opacity = 1f;
        this.titleFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        rect1 = new Rectangle(0, this.TitleHeight, this.Width, this.Height - this.TitleHeight);
        if (this.hasRibbon)
          rect1 = new Rectangle(0, 30, this.Width, this.Height - 30);
        using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
          e.Graphics.FillRectangle((Brush) solidBrush, rect1);
        shadowOffset = 0;
        borderOffset = 0;
      }
      if (this.HasRibbon)
        return;
      this.DrawText(e, shadowOffset, borderOffset);
    }
  }
}
