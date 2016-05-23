// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vRibbonGroupContent
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
  [Designer("VIBlend.WinForms.Controls.Design.RibbonGroupContentDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(false)]
  public class vRibbonGroupContent : Control, IScrollableControlBase
  {
    private int footerHeight = 16;
    private int radius = 3;
    private bool allowAnimations = true;
    private string styleKey = "RibbonGroupContent";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private PaintHelper helper = new PaintHelper();
    private vRibbonGroupFooter footer;
    internal BackgroundElement backFill;
    internal BackgroundElement panelFill;
    internal bool drawContentFill;
    private vRibbonGroup group;
    private AnimationManager manager;
    private ControlTheme theme;
    private RibbonGroupPanel panel;

    public vRibbonGroup RibbonGroup
    {
      get
      {
        return this.group;
      }
    }

    /// <summary>Gets or sets the text associated with this control.</summary>
    /// <value></value>
    /// <returns>The text associated with this control.</returns>
    [Description("Gets or sets the text associated with this control.")]
    [Category("Behavior")]
    public new string Text
    {
      get
      {
        return this.Footer.Text;
      }
      set
      {
        base.Text = value;
        this.Footer.Text = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the text alignment.</summary>
    /// <value>The text alignment.</value>
    [Description("Gets or sets the text alignment.")]
    [Category("Behavior")]
    public ContentAlignment TextAlignment
    {
      get
      {
        return this.Footer.TextAlignment;
      }
      set
      {
        this.Footer.TextAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the footer button is enabled
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to show the footer button.")]
    [DefaultValue(true)]
    public bool EnableFooterButton
    {
      get
      {
        return this.footer.FooterButton.Enabled;
      }
      set
      {
        this.footer.FooterButton.Enabled = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the footer button.
    /// </summary>
    [Description("Gets or sets a value indicating whether to show the footer button.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ShowFooterButton
    {
      get
      {
        return this.footer.ShowFooterButton;
      }
      set
      {
        this.footer.ShowFooterButton = value;
      }
    }

    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.manager == null)
        {
          this.manager = new AnimationManager((Control) this);
          this.manager.AnimationEnd += new EventHandler(this.manager_AnimationEnd);
        }
        return this.manager;
      }
      set
      {
        this.manager = value;
      }
    }

    [DefaultValue(true)]
    [Browsable(false)]
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

    [Category("Appearance")]
    [Description("Gets or sets button's theme")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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
        this.theme = value;
        ControlTheme theme = ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme);
        FillStyle fillStyle = this.theme.QueryFillStyleSetter("RibbonTabBackGround");
        if (fillStyle != null)
          theme.StyleNormal.FillStyle = fillStyle;
        this.backFill.LoadTheme(theme.CreateCopy());
        bool flag = true;
        if (this.group != null && this.group.GroupStyle == RibbonStyle.Office2010)
        {
          flag = false;
          theme.StyleHighlight.FillStyle.Colors[0] = Color.FromArgb(50, theme.StyleNormal.FillStyle.Colors[0]);
          theme.StyleHighlight.FillStyle.Colors[1] = Color.FromArgb(50, theme.StyleNormal.FillStyle.Colors[1]);
          if (theme.StyleNormal.FillStyle.ColorsNumber > 2)
          {
            theme.StyleHighlight.FillStyle.Colors[2] = Color.FromArgb(50, theme.StyleNormal.FillStyle.Colors[2]);
            theme.StyleHighlight.FillStyle.Colors[3] = Color.FromArgb(50, theme.StyleNormal.FillStyle.Colors[3]);
          }
        }
        else
        {
          theme.StyleHighlight.FillStyle.Colors[0] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[0]));
          theme.StyleHighlight.FillStyle.Colors[1] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[1]));
          if (theme.StyleNormal.FillStyle.ColorsNumber > 2)
          {
            theme.StyleHighlight.FillStyle.Colors[2] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[2]));
            theme.StyleHighlight.FillStyle.Colors[3] = ControlPaint.LightLight(Color.FromArgb(200, theme.StyleNormal.FillStyle.Colors[3]));
          }
        }
        theme.StyleNormal.FillStyle.Colors[0] = Color.Transparent;
        theme.StyleNormal.FillStyle.Colors[1] = Color.Transparent;
        if (theme.StyleNormal.FillStyle.ColorsNumber > 2)
        {
          theme.StyleNormal.FillStyle.Colors[2] = Color.Transparent;
          theme.StyleNormal.FillStyle.Colors[3] = Color.Transparent;
        }
        this.panelFill.LoadTheme(theme);
        this.panelFill.IsAnimated = flag;
        this.panelFill.MaxFrameRate = 12;
        this.backFill.IsAnimated = false;
        this.Invalidate();
        if (this.footer == null)
          return;
        this.Footer.Invalidate();
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
        this.footer.VIBlendTheme = value;
      }
    }

    /// <summary>
    /// Gets a reference to the panel which hosts the controls within the navigation pane item
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public vRibbonGroupFooter Footer
    {
      get
      {
        return this.footer;
      }
      internal set
      {
        this.footer = value;
      }
    }

    /// <summary>
    /// Gets a reference to the panel which hosts the controls within the navigation pane item
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public RibbonGroupPanel ContentPanel
    {
      get
      {
        return this.panel;
      }
      internal set
      {
        this.panel = value;
      }
    }

    /// <summary>
    /// Gets or sets the height of the header area of the item
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(16)]
    public int FooterHeight
    {
      get
      {
        return this.footerHeight;
      }
      set
      {
        this.footerHeight = value;
        this.footer.Height = value;
      }
    }

    /// <summary>Gets or sets the radius.</summary>
    /// <value>The radius.</value>
    [DefaultValue(3)]
    [Category("Appearance")]
    [Description("Gets or sets the radius.")]
    public int Radius
    {
      get
      {
        return this.radius;
      }
      set
      {
        this.radius = value;
        this.Invalidate();
      }
    }

    public vRibbonGroupContent(vRibbonGroup group)
    {
      this.group = group;
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.panelFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.footer = new vRibbonGroupFooter(this);
      this.Controls.Add((Control) this.footer);
      this.panel = new RibbonGroupPanel();
      this.panel.AutoScroll = true;
      this.Controls.Add((Control) this.panel);
      this.panel.BackColor = Color.Transparent;
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.SetStyle(ControlStyles.Opaque, false);
      this.BackColor = Color.Transparent;
      this.footer.MouseEnter += new EventHandler(this.footer_MouseEnter);
      this.panel.MouseEnter += new EventHandler(this.panel_MouseEnter);
      this.footer.MouseLeave += new EventHandler(this.footer_MouseLeave);
      this.panel.MouseLeave += new EventHandler(this.panel_MouseLeave);
      this.panel.Paint += new PaintEventHandler(this.panel_Paint);
      this.footer.FooterButton.MouseEnter += new EventHandler(this.FooterButton_MouseEnter);
      this.footer.FooterButton.MouseUp += new MouseEventHandler(this.FooterButton_MouseUp);
      this.footer.MouseUp += new MouseEventHandler(this.footer_MouseUp);
      this.panel.MouseUp += new MouseEventHandler(this.panel_MouseUp);
    }

    private void panel_Paint(object sender, PaintEventArgs e)
    {
    }

    private void panel_MouseLeave(object sender, EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.drawContentFill = false;
      this.panel.Invalidate();
      this.footer.Invalidate();
    }

    private void footer_MouseLeave(object sender, EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.drawContentFill = false;
      this.panel.Invalidate();
      this.footer.Invalidate();
    }

    private void FooterButton_MouseEnter(object sender, EventArgs e)
    {
      this.ResetStates();
      this.drawContentFill = true;
      this.panel.Invalidate();
      this.footer.Invalidate();
    }

    private void ResetStates()
    {
      if (this.RibbonGroup == null || !(this.RibbonGroup.Parent is vRibbonHorizontalLayoutPanel))
        return;
      for (int index = 0; index < (this.RibbonGroup.Parent as vRibbonHorizontalLayoutPanel).Controls.Count; ++index)
      {
        vRibbonGroup vRibbonGroup = (this.RibbonGroup.Parent as vRibbonHorizontalLayoutPanel).Controls[index] as vRibbonGroup;
        if (vRibbonGroup != null && vRibbonGroup != this.RibbonGroup)
        {
          vRibbonGroup.Content.drawContentFill = false;
          vRibbonGroup.Invalidate();
        }
      }
    }

    private void footer_MouseEnter(object sender, EventArgs e)
    {
      this.ResetStates();
      this.drawContentFill = true;
      this.panel.Invalidate();
      this.footer.Invalidate();
    }

    private void panel_MouseEnter(object sender, EventArgs e)
    {
      this.ResetStates();
      this.drawContentFill = true;
      this.panel.Invalidate();
      this.footer.Invalidate();
    }

    private void panel_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.drawContentFill = false;
      this.panel.Invalidate();
    }

    private void footer_MouseUp(object sender, MouseEventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.drawContentFill = false;
      this.panel.Invalidate();
    }

    private void FooterButton_MouseUp(object sender, MouseEventArgs e)
    {
      this.drawContentFill = false;
      this.panel.Invalidate();
    }

    /// <summary>Raises the Layout event.</summary>
    /// <param name="levent">A LayoutEventArgs that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      int x = 0;
      int y = 0;
      if (this.footer == null)
        return;
      this.footer.Bounds = new Rectangle(x, this.Height - this.FooterHeight - y, this.Width - 2 * x, this.FooterHeight);
      if (this.panel == null)
        return;
      this.panel.Bounds = new Rectangle(x, y, this.Width - 2 * x, this.Height - this.FooterHeight - 2 * y);
    }

    private void ResetTextAlignment()
    {
      this.TextAlignment = ContentAlignment.MiddleCenter;
    }

    private bool ShouldSerializeTextAlignment()
    {
      return this.TextAlignment != ContentAlignment.MiddleCenter;
    }

    private void manager_AnimationEnd(object sender, EventArgs e)
    {
      if (this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        return;
      this.drawContentFill = false;
      this.Invalidate();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (!this.RectangleToScreen(this.ClientRectangle).Contains(Cursor.Position))
        this.drawContentFill = false;
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      Rectangle rectangle = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      this.panelFill.Radius = this.Radius;
      if (this.drawContentFill)
      {
        if (this.Parent is ToolStripDropDown)
        {
          this.backFill.Bounds = rectangle;
          this.backFill.DrawElementFill(e.Graphics, ControlState.Normal);
        }
        if (this.group.GroupStyle == RibbonStyle.Office2007)
        {
          this.panelFill.Bounds = rectangle;
          this.panelFill.DrawElementFill(e.Graphics, ControlState.Hover);
        }
        else
        {
          this.panelFill.Bounds = rectangle;
          this.panelFill.DrawElementFill(e.Graphics, ControlState.Hover);
          this.helper.DrawGlow(e.Graphics, rectangle, Color.FromArgb(50, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue), Color.FromArgb(100, (int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue));
        }
      }
      else if (this.Parent is ToolStripDropDown)
      {
        this.backFill.Bounds = rectangle;
        this.backFill.DrawElementFill(e.Graphics, ControlState.Normal);
        this.panelFill.Bounds = rectangle;
        this.panelFill.DrawElementFill(e.Graphics, ControlState.Normal);
      }
      else
      {
        this.panelFill.Bounds = rectangle;
        this.panelFill.DrawElementFill(e.Graphics, ControlState.Normal);
      }
      if (this.group.GroupStyle == RibbonStyle.Office2007)
      {
        GraphicsPath roundedPathRect = this.helper.GetRoundedPathRect(rectangle, this.Radius);
        using (Pen pen = new Pen(this.backFill.BorderColor))
          e.Graphics.DrawPath(pen, roundedPathRect);
      }
      else
      {
        using (Pen pen = new Pen(this.backFill.BorderColor))
        {
          e.Graphics.DrawLine(pen, rectangle.Right - 1, rectangle.Top + 1, rectangle.Right - 1, rectangle.Bottom - 2);
          pen.Color = ControlPaint.LightLight(pen.Color);
          e.Graphics.DrawLine(pen, rectangle.Right, rectangle.Top + 1, rectangle.Right, rectangle.Bottom - 2);
        }
      }
      e.Graphics.SmoothingMode = smoothingMode;
    }
  }
}
