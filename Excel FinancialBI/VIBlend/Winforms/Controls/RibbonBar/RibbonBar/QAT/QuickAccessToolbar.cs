// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.QuickAccessToolbar
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
  /// Represents a QuickAccessToolbar control which is often used in ribbon interfaces.
  /// </summary>
  [ToolboxBitmap(typeof (QuickAccessToolbar), "ControlIcons.QuickAccessToolbar.ico")]
  [Description("Represents a QuickAccessToolbar control which is often used in ribbon interfaces.")]
  [Designer("VIBlend.WinForms.Controls.Design.QuickAccessToolbarDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  public class QuickAccessToolbar : ScrollableControlMiniBase
  {
    private vHorizontalLayoutPanel layoutPanel = new vHorizontalLayoutPanel();
    private vApplicationMenuItem dropDownItem = new vApplicationMenuItem();
    internal bool drawBackground = true;
    private string styleKey = "QAT";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    protected BackgroundElement backFill;
    private RibbonStyle toolbarStyle;
    private ControlTheme theme;

    /// <summary>Gets or sets the toolbar style.</summary>
    /// <value>The toolbar style.</value>
    [Category("Behavior")]
    [Browsable(false)]
    public RibbonStyle ToolbarStyle
    {
      get
      {
        return this.toolbarStyle;
      }
      set
      {
        this.toolbarStyle = value;
        this.Invalidate();
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

    /// <summary>`
    /// Gets or sets the theme of the control.
    /// </summary>
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
        this.backFill.LoadTheme(this.theme);
        this.AllowAnimations = true;
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
        if (defaultTheme != null)
          this.Theme = defaultTheme;
        if (this.DropDown == null)
          return;
        this.DropDown.VIBlendTheme = value;
      }
    }

    /// <summary>Gets the content.</summary>
    /// <value>The content.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public vHorizontalLayoutPanel Content
    {
      get
      {
        return this.layoutPanel;
      }
    }

    /// <summary>Gets the drop down.</summary>
    /// <value>The drop down.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public vApplicationMenuItem DropDown
    {
      get
      {
        return this.dropDownItem;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.QuickAccessToolbar" /> class.
    /// </summary>
    public QuickAccessToolbar()
    {
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.dropDownItem.ItemType = vApplicationMenuItemType.DropDown;
      this.Controls.Add((Control) this.layoutPanel);
      this.Controls.Add((Control) this.dropDownItem);
      this.layoutPanel.Dock = DockStyle.Fill;
      this.dropDownItem.Dock = DockStyle.Right;
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.layoutPanel.Paint += new PaintEventHandler(this.layoutPanel_Paint);
      this.MaximumSize = new Size(500, 22);
      this.Size = new Size(100, 22);
      this.layoutPanel.StartOffset = 40;
      this.layoutPanel.ControlAdded += new ControlEventHandler(this.layoutPanel_ControlAdded);
      this.layoutPanel.Layout += new LayoutEventHandler(this.layoutPanel_Layout);
    }

    private void layoutPanel_Layout(object sender, LayoutEventArgs e)
    {
      if (this.drawBackground && this.ToolbarStyle == RibbonStyle.Office2007)
        this.layoutPanel.StartOffset = 40;
      else
        this.layoutPanel.StartOffset = 0;
    }

    private void layoutPanel_ControlAdded(object sender, ControlEventArgs e)
    {
      e.Control.Margin = new Padding(1, 6, 1, 3);
    }

    private void layoutPanel_Paint(object sender, PaintEventArgs e)
    {
      if (this.layoutPanel.Controls.Count <= 2 || !this.drawBackground || this.ToolbarStyle != RibbonStyle.Office2007)
        return;
      this.DrawQatBackground(e.Graphics, new Rectangle(0, 0, this.Width - 15, this.Height), new Point(10, this.Height));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      if (this.layoutPanel.Controls.Count <= 2 || !this.drawBackground || this.ToolbarStyle != RibbonStyle.Office2007)
        return;
      this.DrawQatBackground(e.Graphics, new Rectangle(0, 0, this.Width - 15, this.Height), new Point(10, this.Height));
    }

    protected internal virtual void DrawQatBackground(Graphics graphics, Rectangle bounds, Point roundPoint)
    {
      SmoothingMode smoothingMode = graphics.SmoothingMode;
      graphics.SmoothingMode = SmoothingMode.AntiAlias;
      int int32 = Convert.ToInt32(Math.Sqrt(Math.Pow((double) (bounds.X - roundPoint.X), 2.0) + Math.Pow((double) (bounds.Y - roundPoint.Y), 2.0)));
      using (GraphicsPath path = new GraphicsPath())
      {
        Rectangle rect = new Rectangle(roundPoint.X - int32, roundPoint.Y - int32, int32 * 2, int32 * 2);
        float num1 = (float) (Math.Acos((double) (roundPoint.Y - bounds.Bottom + 1) / (double) int32) * 180.0 / Math.PI);
        float num2 = (float) (Math.Acos((double) (roundPoint.Y - bounds.Y) / (double) int32) * 180.0 / Math.PI);
        path.AddArc(bounds.Right - bounds.Height, bounds.Y, bounds.Height, bounds.Height - 1, 270f, 180f);
        path.AddArc(rect, num1 - 90f, num2 - num1);
        path.CloseAllFigures();
        using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(this.ClientRectangle, this.backFill.Theme.StyleNormal.FillStyle.Colors[0], this.backFill.Theme.StyleNormal.FillStyle.Colors[1], 90f))
        {
          using (Pen pen = new Pen(this.backFill.BorderColor))
          {
            graphics.FillPath((Brush) linearGradientBrush, path);
            graphics.DrawPath(pen, path);
          }
        }
      }
      graphics.SmoothingMode = smoothingMode;
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.layoutPanel.Bounds = new Rectangle(0, 0, this.Width - 13, this.Height);
      this.dropDownItem.Bounds = new Rectangle(this.Width - 13, 0, 13, this.Height);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.layoutPanel.Bounds = new Rectangle(0, 0, this.Width - 13, this.Height);
      this.dropDownItem.Bounds = new Rectangle(this.Width - 13, 0, 13, this.Height);
    }
  }
}
