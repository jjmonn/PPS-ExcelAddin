// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vVerticalLayoutPanel
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
  /// Represents a vVerticalLayoutPanel control where the user can arrange multiple controls.
  /// </summary>
  [Designer("VIBlend.WinForms.Controls.Design.vLayoutPanelDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vVerticalLayoutPanel), "ControlIcons.vVerticalLayoutPanel.ico")]
  [Description("Displays a vertical layout panel where the user can arrange multiple controls.")]
  public class vVerticalLayoutPanel : Panel, IScrollableControlBase
  {
    private Timer timer = new Timer();
    private float animationCurrentValue = 0.5f;
    private float animationValue = 0.5f;
    private int scrollingIndex = 2;
    private Stack<int> scrollValues = new Stack<int>();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private Color panelBackColor = Color.Transparent;
    private Color paintBorderColor = Color.Transparent;
    private bool allowAnimations = true;
    private vArrowButton upButton;
    private vArrowButton downButton;
    internal int scrollOffset;
    private int animationStartValue;
    private int animationEndValue;
    private bool animateForward;
    private BackgroundElement backFill;
    private ControlTheme theme;
    private bool usePanelBackColor;
    private bool usePanelBorderColor;
    private bool paintFill;
    private bool paintBorder;
    private AnimationManager manager;
    private bool canAnimate;

    /// <summary>Gets or sets the theme of the control.</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the theme of the control.")]
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
        this.downButton.VIBlendTheme = this.VIBlendTheme;
        this.upButton.VIBlendTheme = this.VIBlendTheme;
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
        this.usePanelBorderColor = value;
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the BorderColor of the Panel.</summary>
    [DefaultValue(typeof (Color), "Transparent")]
    [Category("Appearance")]
    [Description("Gets or sets the BorderColor of the Panel.")]
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

    /// <summary>
    /// Gets or sets a value indicating whether to paint a background
    /// </summary>
    /// <value><c>true</c> if [paint fill]; otherwise, <c>false</c>.</value>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to paint a background")]
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
    [Description("Gets or sets a value indicating whether to paint a border")]
    [Category("Behavior")]
    [DefaultValue(false)]
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

    /// <summary>vVerticalLayoutPanel constructor</summary>
    public vVerticalLayoutPanel()
    {
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.defaultTheme);
      this.AutoSize = false;
      this.AutoScroll = false;
      this.upButton = new vArrowButton();
      this.downButton = new vArrowButton();
      this.upButton.ArrowDirection = ArrowDirection.Up;
      this.downButton.ArrowDirection = ArrowDirection.Down;
      this.Controls.Add((Control) this.upButton);
      this.Controls.Add((Control) this.downButton);
      this.downButton.Click += new EventHandler(this.rightButton_Click);
      this.upButton.Click += new EventHandler(this.leftButton_Click);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (this.DesignMode)
        ControlPaint.DrawFocusRectangle(e.Graphics, this.ClientRectangle);
      if (this.backFill == null)
        return;
      if (this.PaintFill)
      {
        this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
        if (!this.UsePanelBackColor)
          this.backFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Linear);
        else
          this.backFill.DrawStandardFill(e.Graphics, this.PanelBackColor);
      }
      if (!this.PaintBorder)
        return;
      this.backFill.Bounds = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
      if (!this.UsePanelBorderColor)
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal);
      else
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal, this.PanelBorderColor);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Down)
      {
        this.ScrollDown();
        return true;
      }
      if (keyData != Keys.Up)
        return base.ProcessCmdKey(ref msg, keyData);
      this.ScrollUp();
      return true;
    }

    private void rightButton_Click(object sender, EventArgs e)
    {
      this.ScrollDown();
    }

    private void leftButton_Click(object sender, EventArgs e)
    {
      this.ScrollUp();
    }

    protected virtual void ScrollDown()
    {
      if (this.timer.Enabled || (this.Controls.Count <= this.scrollingIndex || !this.CanScrollDown()))
        return;
      Control control = this.Controls[this.scrollingIndex];
      int num = control.Height + control.Margin.Vertical;
      this.scrollValues.Push(num);
      int startValue = this.scrollOffset;
      int endValue = startValue - num;
      this.Animate(startValue, endValue, true);
      ++this.scrollingIndex;
    }

    protected virtual void ScrollUp()
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

    internal bool CanScrollDown()
    {
      int num = 0;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        Control control = this.Controls[index];
        if (!(control is vArrowButton) && control.Visible)
          num += control.Height + control.Margin.Vertical;
      }
      return num + this.scrollOffset > this.Height;
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

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.upButton == null || this.downButton == null)
        return;
      int height = 10;
      int y = this.scrollOffset + height;
      if (this.scrollOffset == 0)
        y = this.scrollOffset;
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        System.Type type = this.Controls[index].GetType();
        Control control = this.Controls[index];
        if (type == typeof (vArrowButton))
        {
          vArrowButton vArrowButton = this.Controls[index] as vArrowButton;
          if (vArrowButton == this.upButton)
            vArrowButton.Bounds = new Rectangle(0, 0, this.Width, height);
          else
            vArrowButton.Bounds = new Rectangle(0, this.Height - height, this.Width, height);
        }
        else if (control.Visible)
        {
          control.Bounds = new Rectangle(control.Margin.Left - control.Margin.Right, y, control.Width, control.Height);
          y += control.Height + control.Margin.Vertical;
        }
      }
      if (this.scrollOffset != 0)
      {
        this.upButton.Visible = true;
        this.downButton.Visible = true;
      }
      else if (y > this.Height)
      {
        this.downButton.Visible = true;
        this.upButton.Visible = false;
      }
      else
      {
        this.upButton.Visible = false;
        this.downButton.Visible = false;
      }
      if (this.CanScrollDown())
        return;
      this.downButton.Visible = false;
    }
  }
}
