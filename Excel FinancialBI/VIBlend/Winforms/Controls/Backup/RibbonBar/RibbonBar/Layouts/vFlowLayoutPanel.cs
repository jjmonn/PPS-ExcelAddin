// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vFlowLayoutPanel
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents a vFlowLayoutPanel control where the user can arrange multiple controls.
  /// </summary>
  [ToolboxBitmap(typeof (vFlowLayoutPanel), "ControlIcons.vFlowLayoutPanel.ico")]
  [Designer("VIBlend.WinForms.Controls.Design.vLayoutPanelDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [Description("Displays a flow layout panel where the user can arrange multiple controls.")]
  public class vFlowLayoutPanel : vPanel
  {
    internal List<int> rowHeights = new List<int>();
    private Timer timer = new Timer();
    private float animationCurrentValue = 0.5f;
    private float animationValue = 0.5f;
    private Stack<int> scrollValues = new Stack<int>();
    private bool autoHideControls = true;
    private int scrollOffset;
    private int animationStartValue;
    private int animationEndValue;
    private bool animateForward;
    private int scrollingIndex;
    private int desiredHeight;
    private bool canAnimate;

    /// <summary>
    /// Gets or sets a value indicating whether control can auto hide controls
    /// </summary>
    [Description("Gets or sets a value indicating whether control can auto hide controls")]
    [Category("Behavior")]
    public bool AutoHideControls
    {
      get
      {
        return this.autoHideControls;
      }
      set
      {
        this.autoHideControls = value;
      }
    }

    internal bool CanScrollUp
    {
      get
      {
        if (this.scrollingIndex == 0)
          return false;
        int num = 0;
        for (int index = 0; index < this.Content.Controls.Count; ++index)
        {
          if (this.Content.Controls[index].Visible)
            ++num;
        }
        return num != this.Content.Controls.Count || this.scrollingIndex != 0;
      }
    }

    internal bool CanScrollDown
    {
      get
      {
        if (this.scrollingIndex == this.rowHeights.Count)
          return false;
        int num = 0;
        for (int index = 0; index < this.Content.Controls.Count; ++index)
        {
          if (this.Content.Controls[index].Visible)
            ++num;
        }
        return num != this.Content.Controls.Count;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vFlowLayoutPanel" /> class.
    /// </summary>
    public vFlowLayoutPanel()
    {
      this.Content.Layout += new LayoutEventHandler(this.Content_Layout);
      this.SizeChanged += new EventHandler(this.vFlowLayoutPanel_SizeChanged);
      this.Content.AutoScroll = true;
      this.Content.ControlAdded += new ControlEventHandler(this.Content_ControlAdded);
      this.Content.ControlRemoved += new ControlEventHandler(this.Content_ControlRemoved);
      this.Resize += new EventHandler(this.vFlowLayoutPanel_Resize);
    }

    private void vFlowLayoutPanel_Resize(object sender, EventArgs e)
    {
    }

    private void Content_ControlRemoved(object sender, ControlEventArgs e)
    {
      this.CalculateLayout();
    }

    private void Content_ControlAdded(object sender, ControlEventArgs e)
    {
      this.CalculateLayout();
    }

    private void vFlowLayoutPanel_SizeChanged(object sender, EventArgs e)
    {
      this.CalculateLayout();
    }

    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      this.CalculateLayout();
    }

    private void Content_Layout(object sender, LayoutEventArgs e)
    {
      this.CalculateLayout();
    }

    /// <summary>Gets the desired height.</summary>
    /// <returns></returns>
    public int GetDesiredHeight()
    {
      return this.desiredHeight;
    }

    private void CalculateLayout()
    {
      this.rowHeights.Clear();
      int left = this.Margin.Left;
      int y = this.Margin.Top + this.scrollOffset;
      int val2 = 0;
      this.desiredHeight = 0;
      for (int index = 0; index < this.Content.Controls.Count; ++index)
      {
        Control control = this.Content.Controls[index];
        control.Visible = true;
        if (left + control.Width + control.Margin.Horizontal >= this.Width)
        {
          this.desiredHeight += val2 + this.Margin.Vertical;
          left = this.Margin.Left;
          y += val2 + this.Margin.Vertical;
          this.rowHeights.Add(val2 + this.Margin.Vertical);
          val2 = 0;
        }
        control.Location = new Point(left, y);
        if (control.Bounds.Bottom > this.Height && this.AutoHideControls)
          control.Visible = false;
        left += control.Width + control.Margin.Horizontal;
        val2 = Math.Max(control.Height, val2);
      }
      this.desiredHeight += val2 + this.Margin.Vertical;
    }

    protected internal virtual void ScrollDown()
    {
      if (this.timer.Enabled || this.rowHeights.Count <= this.scrollingIndex)
        return;
      int num = this.rowHeights[this.scrollingIndex];
      this.scrollValues.Push(num);
      int startValue = this.scrollOffset;
      int endValue = startValue - num;
      this.Animate(startValue, endValue, true);
      ++this.scrollingIndex;
    }

    protected internal virtual void ScrollUp()
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
        this.scrollingIndex = 0;
        if (this.scrollOffset == 0)
          return;
        this.Animate(this.scrollOffset, 0, false);
      }
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
        this.timer.Interval = 1;
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
      this.Content.PerformLayout();
    }
  }
}
