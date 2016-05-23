// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vPanel
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
  /// <summary>Represents a vPanel control</summary>
  /// <remarks>
  /// A vPanel is a scrollable container control that can host a group of other controls.
  /// </remarks>
  [Description("Represents a scrollable container control that can host a group of other controls.")]
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vPanelDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxBitmap(typeof (vPanel), "ControlIcons.Panel.ico")]
  public class vPanel : Control, IScrollableControlBase
  {
    private vContentPanel innerPanel = new vContentPanel();
    private vHScrollBar hScroll = new vHScrollBar();
    private vVScrollBar vScroll = new vVScrollBar();
    private vContentPanel crossScrollbarsControl = new vContentPanel();
    private Color customIntersectionColor = Color.Empty;
    private bool allowAnimations = true;
    private Color paintBorderColor = Color.Transparent;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private bool paintBorder = true;
    internal BackgroundElement backElement;
    private AnimationManager manager;
    private bool usePanelBorderColor;
    private int borderRadius;
    private ControlTheme theme;
    private bool paintFill;
    internal ControlState state;

    /// <summary>Gets or sets the color of the scrollers intersection.</summary>
    [Description(" Gets or sets the color of the scrollers intersection.")]
    [Category("Appearance")]
    public Color CustomScrollersIntersectionColor
    {
      get
      {
        return this.customIntersectionColor;
      }
      set
      {
        this.customIntersectionColor = value;
        this.SyncScrollBars();
      }
    }

    /// <exclude />
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

    /// <summary>Determines whether to use animations</summary>
    [Browsable(false)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        if (this.backElement != null)
          this.backElement.IsAnimated = value;
        this.allowAnimations = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the PanelBorderColor
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use the PanelBorderColor")]
    [DefaultValue(false)]
    public virtual bool UsePanelBorderColor
    {
      get
      {
        return this.usePanelBorderColor;
      }
      set
      {
        if (value == this.usePanelBorderColor)
          return;
        this.usePanelBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the BorderColor of the Panel.</summary>
    [Description("Gets or sets the BorderColor of the Panel.")]
    [Category("Appearance")]
    public virtual Color PanelBorderColor
    {
      get
      {
        return this.paintBorderColor;
      }
      set
      {
        this.paintBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the border radius.</summary>
    /// <value>The border radius.</value>
    [Category("Appearance")]
    [Description("Gets or sets the BorderRadius of the Panel. The Maximum value is 10.")]
    [Browsable(false)]
    public int BorderRadius
    {
      get
      {
        return this.borderRadius;
      }
      set
      {
        if (value == this.borderRadius || value < 0 || value > 10)
          return;
        this.borderRadius = value;
        if (this.backElement != null && this.theme != null)
        {
          this.theme.Radius = (float) this.borderRadius;
          this.backElement.Radius = this.borderRadius;
        }
        this.PerformLayout();
        this.Refresh();
      }
    }

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the vRadioButton Theme")]
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
        this.theme = value.CreateCopy();
        this.theme.Radius = (float) this.BorderRadius;
        this.backElement.LoadTheme(this.theme);
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
        if (defaultTheme != null)
          this.Theme = defaultTheme;
        this.hScroll.VIBlendTheme = value;
        this.vScroll.VIBlendTheme = value;
      }
    }

    /// <summary>
    /// Determines the opacity of the Panel control's background
    /// </summary>
    public float Opacity
    {
      get
      {
        return this.backElement.Opacity;
      }
      set
      {
        this.backElement.Opacity = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint a background
    /// </summary>
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to paint a background")]
    [Category("Behavior")]
    public bool PaintFill
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
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to paint a border")]
    public bool PaintBorder
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

    /// <summary>Gets the panel.</summary>
    /// <value>The panel.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Behavior")]
    [Description("Gets the content panel")]
    public vContentPanel Content
    {
      get
      {
        return this.innerPanel;
      }
    }

    static vPanel()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vPanel" /> class.
    /// </summary>
    public vPanel()
    {
      this.backElement = new BackgroundElement(this.Bounds, (IScrollableControlBase) this);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.Selectable, false);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.innerPanel.Layout += new LayoutEventHandler(this.innerPanel_Layout);
      this.Controls.Add((Control) this.hScroll);
      this.Controls.Add((Control) this.vScroll);
      this.Controls.Add((Control) this.crossScrollbarsControl);
      this.Controls.Add((Control) this.innerPanel);
      this.vScroll.Scroll += new ScrollEventHandler(this.vScroll_Scroll);
      this.hScroll.Scroll += new ScrollEventHandler(this.hScroll_Scroll);
      this.hScroll.Visible = false;
      this.vScroll.Visible = false;
      this.innerPanel.AutoScroll = true;
      this.innerPanel.Scroll += new ScrollEventHandler(this.innerPanel_Scroll);
      this.innerPanel.MouseWheel += new MouseEventHandler(this.innerPanel_MouseWheel);
      this.Theme = ControlTheme.GetDefaultTheme(this.VIBlendTheme);
      this.crossScrollbarsControl.Visible = false;
    }

    private void innerPanel_MouseWheel(object sender, MouseEventArgs e)
    {
      this.SyncScrollBars();
    }

    /// <exclude />
    protected override void OnRightToLeftChanged(EventArgs e)
    {
      base.OnRightToLeftChanged(e);
      this.innerPanel.RightToLeft = this.RightToLeft;
      this.SyncScrollBars();
    }

    /// <exclude />
    protected override void OnBackColorChanged(EventArgs e)
    {
      base.OnBackColorChanged(e);
    }

    private void innerPanel_Scroll(object sender, ScrollEventArgs e)
    {
      this.SyncScrollBars();
    }

    private void hScroll_Scroll(object sender, ScrollEventArgs e)
    {
      this.innerPanel.AutoScrollPosition = new Point(e.NewValue, Math.Abs(this.innerPanel.AutoScrollPosition.Y));
      this.innerPanel.Refresh();
    }

    private void vScroll_Scroll(object sender, ScrollEventArgs e)
    {
      this.innerPanel.AutoScrollPosition = new Point(Math.Abs(this.innerPanel.AutoScrollPosition.X), e.NewValue);
      this.innerPanel.Refresh();
    }

    private void innerPanel_Layout(object sender, LayoutEventArgs e)
    {
      this.PerformLayout();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      bool visible1 = this.innerPanel.HorizontalScroll.Visible;
      bool visible2 = this.innerPanel.VerticalScroll.Visible;
      bool flag = this.RightToLeft == RightToLeft.Yes;
      if (!visible2 && !visible1)
        this.innerPanel.Bounds = new Rectangle(1 + this.BorderRadius, 1 + this.BorderRadius, this.ClientRectangle.Width - 2 - 2 * this.BorderRadius, this.ClientRectangle.Height - 2 - 2 * this.BorderRadius);
      else if (!visible2 && visible1)
        this.innerPanel.Bounds = new Rectangle(1 + this.BorderRadius, 1 + this.BorderRadius, this.ClientRectangle.Width - 2 - 2 * this.BorderRadius, this.ClientRectangle.Height - 2 - this.BorderRadius);
      else if (visible2 && visible1)
      {
        if (!flag)
          this.innerPanel.Bounds = new Rectangle(1 + this.BorderRadius, 1 + this.BorderRadius, this.ClientRectangle.Width - 2 - this.BorderRadius, this.ClientRectangle.Height - 2 - this.BorderRadius);
        else
          this.innerPanel.Bounds = new Rectangle(1, 1 + this.BorderRadius, this.ClientRectangle.Width - 2 - this.BorderRadius, this.ClientRectangle.Height - 2 - this.BorderRadius);
      }
      else if (visible2 && !visible1)
      {
        if (!flag)
          this.innerPanel.Bounds = new Rectangle(1 + this.BorderRadius, 1 + this.BorderRadius, this.ClientRectangle.Width - 2 - this.BorderRadius, this.ClientRectangle.Height - 2 - 2 * this.BorderRadius);
        else
          this.innerPanel.Bounds = new Rectangle(1, 1 + this.BorderRadius, this.ClientRectangle.Width - 2 - this.BorderRadius, this.ClientRectangle.Height - 2 - 2 * this.BorderRadius);
      }
      base.OnLayout(levent);
      this.SyncScrollBars();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      if (this.PaintFill)
        this.BackColor = this.Enabled ? this.theme.StyleNormal.FillStyle.Colors[0] : this.theme.StyleDisabled.FillStyle.Colors[0];
      this.innerPanel.BackColor = this.BackColor;
      using (Brush brush = (Brush) new SolidBrush(this.BackColor))
        e.Graphics.FillRectangle(brush, this.ClientRectangle);
      if (this.Width <= 0 || this.Height <= 0)
        return;
      SmoothingMode smoothingMode = e.Graphics.SmoothingMode;
      e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      if (this.PaintBorder)
      {
        this.backElement.Bounds = new Rectangle(0, 0, this.Width, this.Height);
        this.backElement.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        if (!this.UsePanelBorderColor)
          this.backElement.DrawElementBorder(e.Graphics, this.state);
        else
          this.backElement.DrawElementBorder(e.Graphics, ControlState.Normal, this.PanelBorderColor);
      }
      e.Graphics.SmoothingMode = smoothingMode;
    }

    private void SyncScrollBars()
    {
      int num1 = 18;
      bool flag1 = false;
      bool flag2 = false;
      int num2 = 0;
      int x = 0;
      this.hScroll.Visible = false;
      this.vScroll.Visible = false;
      this.crossScrollbarsControl.Visible = false;
      if (this.innerPanel.VerticalScroll.Visible)
      {
        flag1 = true;
        this.vScroll.Visible = true;
      }
      if (this.innerPanel.HorizontalScroll.Visible)
      {
        flag2 = true;
        this.hScroll.Visible = true;
        this.hScroll.LargeChange = this.innerPanel.HorizontalScroll.LargeChange;
        this.hScroll.Maximum = this.innerPanel.HorizontalScroll.Maximum - this.hScroll.LargeChange + 1;
        this.hScroll.Minimum = this.innerPanel.HorizontalScroll.Minimum;
        this.hScroll.SmallChange = this.innerPanel.HorizontalScroll.SmallChange;
        this.hScroll.Value = this.innerPanel.HorizontalScroll.Value;
      }
      if (flag1)
      {
        if (flag2)
          num2 = num1;
        this.vScroll.LargeChange = this.innerPanel.VerticalScroll.LargeChange;
        this.vScroll.Maximum = this.innerPanel.VerticalScroll.Maximum - this.vScroll.LargeChange + 1;
        this.vScroll.Minimum = this.innerPanel.VerticalScroll.Minimum;
        this.vScroll.SmallChange = this.innerPanel.VerticalScroll.SmallChange;
        if (this.RightToLeft == RightToLeft.No)
          this.vScroll.Bounds = new Rectangle(this.Width - num1, 0, num1, this.Height - num2);
        else
          this.vScroll.Bounds = new Rectangle(0, 0, num1, this.Height - num2);
        this.vScroll.Value = this.innerPanel.VerticalScroll.Value;
      }
      if (flag2)
      {
        if (flag1)
          x = num1;
        if (this.RightToLeft == RightToLeft.No)
          this.hScroll.Bounds = new Rectangle(0, this.Height - num1, this.Width - x, num1);
        else
          this.hScroll.Bounds = new Rectangle(x, this.Height - num1, this.Width - x, num1);
      }
      if (flag2 && flag1)
      {
        this.crossScrollbarsControl.Visible = true;
        this.crossScrollbarsControl.BackColor = this.BackColor;
        if ((int) this.BackColor.A == 0)
          this.crossScrollbarsControl.BackColor = this.Theme.StyleNormal.FillStyle.Colors[0];
        if (this.CustomScrollersIntersectionColor != Color.Empty)
          this.crossScrollbarsControl.BackColor = this.CustomScrollersIntersectionColor;
        if (this.RightToLeft == RightToLeft.No)
          this.crossScrollbarsControl.Bounds = new Rectangle(this.Width - num1, this.Height - num1, num1, num1);
        else
          this.crossScrollbarsControl.Bounds = new Rectangle(0, this.Height - num1, num1, num1);
      }
      this.innerPanel.Invalidate();
    }
  }
}
