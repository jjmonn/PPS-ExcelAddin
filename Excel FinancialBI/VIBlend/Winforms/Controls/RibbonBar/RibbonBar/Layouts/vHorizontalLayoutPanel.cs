// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vHorizontalLayoutPanel
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents a vHorizontalLayoutPanel control where the user can arrange multiple controls.
  /// </summary>
  [ToolboxBitmap(typeof (vHorizontalLayoutPanel), "ControlIcons.vHorizontalLayoutPanel.ico")]
  [ToolboxItem(true)]
  [Description("Displays a horizontal layout panel where the user can arrange multiple controls.")]
  [Designer("VIBlend.WinForms.Controls.Design.vLayoutPanelDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vHorizontalLayoutPanel : Panel, IScrollableControlBase
  {
    private Timer timer = new Timer();
    private float animationCurrentValue = 0.5f;
    private float animationValue = 0.5f;
    private int scrollingIndex = 2;
    private Stack<int> scrollValues = new Stack<int>();
    private bool allowAnimations = true;
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private Color panelBackColor = Color.Transparent;
    private Color paintBorderColor = Color.Transparent;
    private vArrowButton leftButton;
    private vArrowButton rightButton;
    private BackgroundElement backFill;
    internal int scrollOffset;
    private int animationStartValue;
    private int animationEndValue;
    private bool animateForward;
    private int startOffset;
    private bool paintFill;
    private bool paintBorder;
    private AnimationManager manager;
    private ControlTheme theme;
    private bool canAnimate;
    private bool usePanelBackColor;
    private bool usePanelBorderColor;

    /// <summary>
    /// Gets or sets a value indicating whether to paint a background
    /// </summary>
    /// <value><c>true</c> if [paint fill]; otherwise, <c>false</c>.</value>
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
    /// <value><c>true</c> if [paint border]; otherwise, <c>false</c>.</value>
    [Category("Behavior")]
    [DefaultValue(false)]
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
        if (this.backFill != null)
          this.backFill.IsAnimated = value;
        this.allowAnimations = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the container enables the user to scroll to any controls placed outside of its visible boundaries.
    /// </summary>
    /// <value></value>
    /// <returns>true if the container enables auto-scrolling; otherwise, false. The default value is false.
    /// </returns>
    [Browsable(false)]
    public override bool AutoScroll
    {
      get
      {
        return base.AutoScroll;
      }
      set
      {
        base.AutoScroll = value;
      }
    }

    /// <summary>Gets or sets the start offset.</summary>
    /// <value>The start offset.</value>
    [Category("Behavior")]
    [DefaultValue(0)]
    public int StartOffset
    {
      get
      {
        return this.startOffset;
      }
      set
      {
        this.startOffset = value;
        this.PerformLayout();
      }
    }

    /// <summary>
    /// </summary>
    /// <value></value>
    /// <returns>true if enabled; otherwise, false.</returns>
    [Browsable(false)]
    public override bool AutoSize
    {
      get
      {
        return base.AutoSize;
      }
      set
      {
        base.AutoSize = value;
      }
    }

    /// <summary>Gets or sets the theme of the control.</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
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
        this.theme = value.CreateCopy();
        ControlTheme theme = this.theme;
        FillStyle fillStyle = theme.QueryFillStyleSetter("RibbonGroup");
        if (fillStyle != null)
          theme.StyleNormal.FillStyle = fillStyle;
        this.backFill.LoadTheme(theme);
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
        this.leftButton.VIBlendTheme = this.VIBlendTheme;
        this.rightButton.VIBlendTheme = this.VIBlendTheme;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance can amiate.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance can amiate; otherwise, <c>false</c>.
    /// </value>
    public bool CanAmiate
    {
      get
      {
        return this.canAnimate;
      }
      set
      {
        this.canAnimate = value;
      }
    }

    /// <summary>Gets or sets the BackColor of the Panel.</summary>
    [Category("Appearance")]
    [Description("Gets or sets the BackColor of the Panel")]
    [DefaultValue(typeof (Color), "Transparent")]
    public virtual Color PanelBackColor
    {
      get
      {
        return this.panelBackColor;
      }
      set
      {
        this.panelBackColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the PanelBackColor
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to use the PanelBackColor")]
    public virtual bool UsePanelBackColor
    {
      get
      {
        return this.usePanelBackColor;
      }
      set
      {
        this.usePanelBackColor = value;
        this.Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the PanelBorderColor
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to use the PanelBorderColor")]
    public virtual bool UsePanelBorderColor
    {
      get
      {
        return this.usePanelBorderColor;
      }
      set
      {
        this.usePanelBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the BorderColor of the Panel.</summary>
    [Description("Gets or sets the BorderColor of the Panel.")]
    [Category("Appearance")]
    [DefaultValue(typeof (Color), "Transparent")]
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

    /// <summary>vHorizontalLayoutPanel constructor</summary>
    public vHorizontalLayoutPanel()
    {
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.defaultTheme);
      this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.AutoSize = false;
      this.AutoScroll = false;
      this.leftButton = new vArrowButton();
      this.rightButton = new vArrowButton();
      this.leftButton.ArrowDirection = ArrowDirection.Left;
      this.rightButton.ArrowDirection = ArrowDirection.Right;
      this.Controls.Add((Control) this.leftButton);
      this.Controls.Add((Control) this.rightButton);
      this.rightButton.Click += new EventHandler(this.rightButton_Click);
      this.leftButton.Click += new EventHandler(this.leftButton_Click);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Right)
      {
        this.ScrollRight();
        return true;
      }
      if (keyData != Keys.Left)
        return base.ProcessCmdKey(ref msg, keyData);
      this.ScrollLeft();
      return true;
    }

    private void rightButton_Click(object sender, EventArgs e)
    {
      this.ScrollRight();
    }

    private void leftButton_Click(object sender, EventArgs e)
    {
      this.ScrollLeft();
    }

    protected virtual void ScrollRight()
    {
      if (this.timer.Enabled || (this.Controls.Count <= this.scrollingIndex || !this.CanScrollRight()))
        return;
      Control control = this.Controls[this.scrollingIndex];
      int num = control.Width + control.Margin.Horizontal;
      this.scrollValues.Push(num);
      int startValue = this.scrollOffset;
      int endValue = startValue - num;
      this.Animate(startValue, endValue, true);
      ++this.scrollingIndex;
    }

    protected virtual void ScrollLeft()
    {
      if (this.timer.Enabled)
        return;
      if (this.scrollValues.Count > 0)
      {
        this.Animate(this.scrollOffset, this.scrollOffset + this.scrollValues.Pop(), false);
        --this.scrollingIndex;
      }
      else
      {
        this.scrollingIndex = 2;
        if (this.scrollOffset == 0)
          return;
        this.Animate(this.scrollOffset, 0, false);
      }
    }

    internal bool CanScrollRight()
    {
      return this.GetChildrenWidth() + this.scrollOffset + this.startOffset > this.Width;
    }

    internal int GetChildrenWidth()
    {
      int num = 0;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        Control control = this.Controls[index];
        if (!(control is vArrowButton) && control.Visible)
          num += control.Width + control.Margin.Horizontal;
      }
      return num;
    }

    private void Animate(int startValue, int endValue, bool animateForward)
    {
      if (!this.CanAmiate)
      {
        this.scrollOffset = endValue;
        this.PerformLayout();
      }
      else
      {
        this.timer.Tick -= new EventHandler(this.timer_Tick);
        this.timer.Tick += new EventHandler(this.timer_Tick);
        this.timer.Interval = 10;
        this.animationStartValue = startValue;
        this.animationEndValue = endValue;
        this.animationCurrentValue = (float) this.animationStartValue;
        this.timer.Start();
        this.animateForward = animateForward;
      }
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.timer.Dispose();
      base.Dispose(disposing);
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      this.animationValue *= 1.225f;
      if (this.animateForward)
      {
        this.animationCurrentValue -= this.animationValue;
        if ((double) this.animationCurrentValue > (double) this.animationEndValue)
        {
          this.scrollOffset -= (int) this.animationValue;
        }
        else
        {
          this.animationCurrentValue = (float) this.animationStartValue;
          this.timer.Stop();
          this.animationValue = 1f;
          this.scrollOffset = this.animationEndValue;
        }
      }
      else
      {
        this.animationCurrentValue += this.animationValue;
        if ((double) this.animationCurrentValue < (double) this.animationEndValue)
        {
          this.scrollOffset += (int) this.animationValue;
        }
        else
        {
          this.animationCurrentValue = (float) this.animationStartValue;
          this.timer.Stop();
          this.animationValue = 1f;
          this.scrollOffset = this.animationEndValue;
        }
      }
      this.PerformLayout();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.backFill != null)
      {
        if (this.PaintFill)
        {
          this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
          if (!this.UsePanelBackColor)
            this.backFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
          else
            this.backFill.DrawStandardFill(e.Graphics, this.PanelBackColor);
        }
        if (this.PaintBorder)
        {
          this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
          if (!this.UsePanelBorderColor)
            this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
          else
            this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal, this.PanelBorderColor);
        }
      }
      if (!this.DesignMode)
        return;
      ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.leftButton == null || this.rightButton == null)
        return;
      int width = 10;
      int x = this.scrollOffset + width + this.startOffset;
      if (this.scrollOffset == 0)
        x = this.scrollOffset + this.startOffset;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        System.Type type = this.Controls[index].GetType();
        Control control = this.Controls[index];
        if (type == typeof (vArrowButton))
        {
          vArrowButton vArrowButton = this.Controls[index] as vArrowButton;
          if (vArrowButton == this.leftButton)
            vArrowButton.Bounds = new Rectangle(0, 0, width, this.Height);
          else
            vArrowButton.Bounds = new Rectangle(this.Width - width, 0, width, this.Height);
        }
        else if (control.Visible)
        {
          control.Bounds = new Rectangle(x, control.Margin.Top - control.Margin.Bottom, control.Width, control.Height);
          x += control.Width + control.Margin.Horizontal;
        }
      }
      if (this.scrollOffset != 0)
      {
        this.leftButton.Visible = true;
        this.rightButton.Visible = true;
      }
      else if (x > this.Width)
      {
        this.rightButton.Visible = true;
        this.leftButton.Visible = false;
      }
      else
      {
        this.leftButton.Visible = false;
        this.rightButton.Visible = false;
      }
      if (this.CanScrollRight())
        return;
      this.rightButton.Visible = false;
    }
  }
}
