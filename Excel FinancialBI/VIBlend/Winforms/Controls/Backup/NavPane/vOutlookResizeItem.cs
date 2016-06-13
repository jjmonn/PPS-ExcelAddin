// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vOutlookResizeItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  public class vOutlookResizeItem : Control, IScrollableControlBase
  {
    private bool allowAnimations = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool useThemeBackground = true;
    private Color backgroundBorder = Color.Black;
    private Brush headerBackgroundBrush = (Brush) new SolidBrush(Color.WhiteSmoke);
    internal vOutlookNavPane navPane;
    internal BackgroundElement backFill;
    private AnimationManager manager;
    private ControlTheme theme;
    private ControlState state;
    private Point pt;

    /// <summary>Gets or sets the animation manager.</summary>
    /// <value>The animation manager.</value>
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

    /// <summary>
    /// Gets or sets a value indicating whether to enable animations
    /// </summary>
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

    [Description("Gets or sets button's theme")]
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
        Color color = this.theme.QueryColorSetter("NavBorderNormal");
        if (color != Color.Empty)
          this.theme.StyleNormal.BorderColor = color;
        FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("NavNormal");
        if (fillStyle1 != null)
          this.theme.StyleNormal.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("NavHover");
        if (fillStyle2 != null)
          this.theme.StyleHighlight.FillStyle = fillStyle2;
        FillStyle fillStyle3 = this.theme.QueryFillStyleSetter("NavPressed");
        if (fillStyle3 != null)
          this.theme.StylePressed.FillStyle = fillStyle3;
        this.backFill.LoadTheme(this.theme);
        this.backFill.IsAnimated = this.AllowAnimations;
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
      }
    }

    /// <summary>Gets or sets the hosted control.</summary>
    /// <value>The hosted control.</value>
    [Browsable(false)]
    public Control HostedControl
    {
      get
      {
        return (Control) this;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's background
    /// </summary>
    [Description("Gets or sets a value indicating whether to use theme's background")]
    [Category("Behavior")]
    public bool UseThemeBackground
    {
      get
      {
        return this.useThemeBackground;
      }
      set
      {
        if (value == this.useThemeBackground)
          return;
        this.useThemeBackground = value;
      }
    }

    /// <summary>Gets or sets the background border.</summary>
    /// <value>The background border.</value>
    [Description("Gets or sets the background border.")]
    [Category("Appearance")]
    public Color BackgroundBorder
    {
      get
      {
        return this.backgroundBorder;
      }
      set
      {
        this.backgroundBorder = value;
      }
    }

    /// <summary>Gets or sets the HighlightBackground brush.</summary>
    /// <value>The HighlightBackground brush.</value>
    [Category("Appearance")]
    [Browsable(false)]
    [Description("Gets or sets the HighlightBackground brush.")]
    public Brush BackgroundBrush
    {
      get
      {
        return this.headerBackgroundBrush;
      }
      set
      {
        this.headerBackgroundBrush = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vOutlookResizeItem" /> class.
    /// </summary>
    /// <param name="navpane">The navpane.</param>
    public vOutlookResizeItem(vOutlookNavPane navpane)
    {
      this.Height = 6;
      this.MouseDown += new MouseEventHandler(this.GripResizer_MouseDown);
      this.MouseMove += new MouseEventHandler(this.GripResizer_MouseMove);
      this.MouseUp += new MouseEventHandler(this.GripResizer_MouseUp);
      this.navPane = navpane;
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
    }

    private void GripResizer_MouseUp(object sender, MouseEventArgs e)
    {
      this.Capture = false;
    }

    private void GripResizer_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.ClientRectangle.Contains(e.Location) && !this.Capture && this.state != ControlState.Hover)
      {
        this.state = ControlState.Hover;
        this.Refresh();
      }
      if (!this.Capture)
        Cursor.Current = !this.ClientRectangle.Contains(e.Location) ? Cursors.Arrow : Cursors.SizeNS;
      else if (this.navPane.Items.Count > 0)
      {
        Cursor.Current = Cursors.SizeNS;
        vOutlookItem topVisibleItem = this.navPane.GetTopVisibleItem();
        if (topVisibleItem != null)
        {
          int y = e.Location.Y;
          if (y >= topVisibleItem.HeaderHeight)
          {
            this.navPane.HideFirstGroup();
            this.navPane.OnSplitterPositionChanged();
            this.pt.Y = e.Location.Y;
          }
          else if (y <= -topVisibleItem.HeaderHeight)
          {
            this.navPane.ShowFirstGroup();
            this.navPane.OnSplitterPositionChanged();
            this.pt.Y = e.Location.Y;
          }
        }
        else if (e.Location.Y <= -this.navPane.HeaderHeight)
        {
          this.navPane.ShowFirstGroup();
          this.navPane.OnSplitterPositionChanged();
          this.pt.Y = e.Location.Y;
        }
      }
      if (!this.Capture)
        return;
      this.Refresh();
      this.navPane.Refresh();
    }

    /// <summary>
    /// Raises the <see cref="E:MouseUp" /> event.
    /// </summary>
    /// <param name="mevent">The <see cref="T:System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      this.Capture = false;
      this.state = !this.ClientRectangle.Contains(mevent.Location) ? ControlState.Normal : ControlState.Hover;
      this.Refresh();
      base.OnMouseUp(mevent);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnMouseLeave(EventArgs e)
    {
      this.state = ControlState.Normal;
      this.Refresh();
      base.OnMouseLeave(e);
    }

    private bool IsOffice2010()
    {
      return this.navPane != null && (this.navPane.VIBlendTheme == VIBLEND_THEME.OFFICE2010BLACK || this.navPane.VIBlendTheme == VIBLEND_THEME.OFFICE2010SILVER || this.navPane.VIBlendTheme == VIBLEND_THEME.OFFICE2010BLUE);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      this.RenderResizeItem(e);
    }

    /// <summary>Renders the resize item.</summary>
    /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
    protected virtual void RenderResizeItem(PaintEventArgs e)
    {
      this.backFill.Bounds = this.ClientRectangle;
      if (!this.Enabled)
        this.state = ControlState.Disabled;
      if (this.navPane != null)
      {
        DrawOutlookNavPaneResizeItemEventArgs args = new DrawOutlookNavPaneResizeItemEventArgs(e.Graphics, this.state, this);
        this.navPane.OnDrawItemResizePart(args);
        if (args.Handled)
          return;
      }
      if (!this.UseThemeBackground)
      {
        using (Pen pen = new Pen(this.BackgroundBorder))
        {
          e.Graphics.FillRectangle(this.BackgroundBrush, this.backFill.Bounds);
          this.backFill.Bounds = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height);
          e.Graphics.DrawRectangle(pen, this.backFill.Bounds);
        }
      }
      else
      {
        this.backFill.DrawElementFill(e.Graphics, this.state);
        this.backFill.Radius = 0;
        Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height);
        Graphics graphics = e.Graphics;
        this.backFill.Bounds = rectangle;
        if (!this.IsOffice2010())
          this.backFill.DrawElementBorder(e.Graphics, this.state);
        else if (this.IsOffice2010())
        {
          rectangle = new Rectangle(1, 1, this.Width - 3, this.Height - 2);
          this.backFill.Bounds = rectangle;
          Color color = this.theme.QueryColorSetter("NavOuterHoverBorder");
          this.backFill.DrawElementBorder(e.Graphics, this.state, color);
          this.backFill.Bounds = this.ClientRectangle;
        }
        RectangleF rect1 = new RectangleF((float) (this.ClientRectangle.Width / 2 - 14), (float) (this.Size.Height / 2) - 1f, 2f, 2f);
        int num = 0;
        if (this.IsOffice2010())
          return;
        do
        {
          RectangleF rect2 = new RectangleF(rect1.X + 0.1f, rect1.Y + 0.1f, 2f, 2f);
          graphics.FillRectangle((Brush) new SolidBrush(Color.White), rect2);
          graphics.FillRectangle((Brush) new SolidBrush(this.backFill.ForeColor), rect1);
          rect1.X += 4f;
          if ((double) rect1.X + 6.0 > (double) (this.Size.Width - 3))
            break;
          ++num;
        }
        while (num <= 6);
      }
    }

    private void GripResizer_MouseDown(object sender, MouseEventArgs e)
    {
      this.Capture = true;
      Cursor.Current = Cursors.SizeNS;
      this.pt = e.Location;
    }
  }
}
