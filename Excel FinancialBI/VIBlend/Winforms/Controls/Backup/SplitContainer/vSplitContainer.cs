// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vSplitContainer
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
  /// Represents a control consisting of a movable bar that divides a container's display area into two resizable panels.
  /// </summary>
  [ToolboxBitmap(typeof (vSplitContainer), "ControlIcons.vSplitContainer.ico")]
  [ToolboxItem(true)]
  [Description("Represents a control consisting of a movable bar that divides a container's display area into two resizable panels.")]
  [Browsable(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vSplitContainerDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vSplitContainer : SplitContainerBase, IScrollableControlBase
  {
    private Size panel1MinSize = new Size(20, 20);
    private Size panel2MinSize = new Size(20, 20);
    private int splitterDistance = -1;
    private bool allowAnimations = true;
    private string styleKey = "Splitter";
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private vSplitterPanel panel1;
    private vSplitterPanel panel2;
    private AnimationManager manager;
    protected ControlTheme theme;
    private bool canRemove;

    /// <summary>Gets or sets the splitter distance.</summary>
    /// <value>The splitter distance.</value>
    [DefaultValue(-1)]
    public int SplitterDistance
    {
      get
      {
        return this.splitterDistance;
      }
      set
      {
        if (this.Orientation == Orientation.Horizontal && (value >= this.Width - this.Panel2MinSize.Width || value <= this.Panel1MinSize.Width) || this.Orientation == Orientation.Vertical && (value >= this.Height - this.Panel2MinSize.Height || value <= this.Panel1MinSize.Height) || value < -1)
          return;
        this.splitterDistance = value;
        this.ApplySplitterDistance((Control) this.Panel1, this.controlSize[(Control) this.Panel1]);
        this.ApplySplitterDistance((Control) this.Panel2, this.controlSize[(Control) this.Panel2]);
      }
    }

    /// <exclude />
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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
    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        this.allowAnimations = value;
      }
    }

    /// <summary>Gets or sets the style key.</summary>
    /// <value>The style key.</value>
    [DefaultValue("Button")]
    [Browsable(false)]
    public virtual string StyleKey
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

    /// <summary>Gets or sets the theme of the control</summary>
    [Category("Appearance")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Description("Gets or sets the theme of the control")]
    [Browsable(false)]
    public virtual ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null)
          return;
        this.theme = !(this.StyleKey != "Splitter") ? ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme) : ThemeCache.GetTheme(this.StyleKey, this.VIBlendTheme, value);
        this.RefreshSplitters();
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes.
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control using one of the built-in themes.")]
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

    /// <summary>Gets or sets the minimum size of the first panel.</summary>
    [Description("Gets or sets the minimum size of the first panel.")]
    public Size Panel1MinSize
    {
      get
      {
        return this.panel1MinSize;
      }
      set
      {
        this.panel1MinSize = value;
        this.RefreshSplitters();
      }
    }

    /// <summary>Gets or sets the minimum size of the second panel.</summary>
    /// <value>The size of the panel2 min.</value>
    [Description("Gets or sets the minimum size of the second panel.")]
    public Size Panel2MinSize
    {
      get
      {
        return this.panel2MinSize;
      }
      set
      {
        this.panel2MinSize = value;
        this.RefreshSplitters();
      }
    }

    /// <summary>Gets the panel1.</summary>
    /// <value>The panel1.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public vSplitterPanel Panel1
    {
      get
      {
        return this.panel1;
      }
    }

    /// <summary>Gets the panel2.</summary>
    /// <value>The panel2.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public vSplitterPanel Panel2
    {
      get
      {
        return this.panel2;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vSplitContainer" /> class.
    /// </summary>
    public vSplitContainer()
    {
      this.SetStyle(ControlStyles.ContainerControl | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
      this.panel1 = new vSplitterPanel();
      this.panel2 = new vSplitterPanel();
      this.panel1.Name = "Panel1";
      this.panel2.Name = "Panel2";
      this.InsertControl(0, (Control) this.panel2, new vSizeType(new Size(1, 1), SizeUnitType.Star));
      this.InsertControl(0, (Control) this.panel1, new vSizeType(new Size(1, 1), SizeUnitType.Star));
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.panel2);
      this.canRemove = true;
      this.PerformLayout();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.OnLayout(new LayoutEventArgs((Control) this.Panel2, "Size"));
    }

    private void ResetPanel1MinSize()
    {
      this.panel1MinSize = new Size(20, 20);
    }

    private bool ShouldSerializePanel1MinSize()
    {
      if (this.panel1MinSize.Width == 20)
        return this.panel1MinSize.Height != 20;
      return true;
    }

    private void ResetPanel2MinSize()
    {
      this.panel2MinSize = new Size(20, 20);
    }

    private bool ShouldSerializePanel2MinSize()
    {
      if (this.panel2MinSize.Width == 20)
        return this.panel2MinSize.Height != 20;
      return true;
    }

    /// <summary>Gets the size of the control.</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public override vSizeType GetControlSize(Control control)
    {
      if (this.controlSize.ContainsKey(control))
        return this.controlSize[control];
      return (vSizeType) null;
    }

    /// <summary>Sets the size of the control.</summary>
    /// <param name="control">The control.</param>
    /// <param name="sizeType">Type of the size.</param>
    public override void SetControlSize(Control control, vSizeType sizeType)
    {
      if (!this.controlSize.ContainsKey(control))
        return;
      this.controlSize.Remove(control);
      this.controlSize.Add(control, sizeType);
      this.PerformLayout();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
    protected override void OnControlAdded(ControlEventArgs e)
    {
      base.OnControlAdded(e);
      if (this.controlSize.ContainsKey(e.Control) || e.Control.GetType() == typeof (SplitterControl))
        return;
      this.layoutControls.Add(e.Control);
      this.controlSize.Add(e.Control, new vSizeType(new Size(1, 1), SizeUnitType.Star));
      this.RefreshSplitters();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
    protected override void OnControlRemoved(ControlEventArgs e)
    {
      base.OnControlRemoved(e);
      if (!this.canRemove || !this.controlSize.ContainsKey(e.Control) || e.Control.GetType() == typeof (SplitterControl))
        return;
      this.controlSize.Remove(e.Control);
      this.layoutControls.Remove(e.Control);
      this.RefreshSplitters();
    }

    /// <summary>Inserts the control.</summary>
    /// <param name="index">The index.</param>
    /// <param name="control">The control.</param>
    /// <param name="sizeType">Type of the size.</param>
    protected override void InsertControl(int index, Control control, vSizeType sizeType)
    {
      if (this.controlSize.ContainsKey(control))
        return;
      this.ApplySplitterDistance(control, sizeType);
      this.layoutControls.Insert(index, control);
      this.RefreshSplitters();
    }

    private void ApplySplitterDistance(Control control, vSizeType sizeType)
    {
      int num = this.SplitterDistance;
      if (num < 0)
        num = 1;
      if (num == 1)
      {
        sizeType.Size = new Size(1, 1);
        this.controlSize.Remove(control);
        this.controlSize.Add(control, sizeType);
      }
      else if (control == this.Panel1 && this.ShowSplitters)
      {
        if (num <= 0)
          return;
        sizeType.Size = this.Orientation != Orientation.Horizontal ? new Size(sizeType.Size.Width, num) : new Size(num, sizeType.Size.Height);
        this.controlSize.Remove(control);
        this.controlSize.Add(control, sizeType);
        this.PerformLayout();
      }
      else
      {
        if (control != this.Panel2 || !this.ShowSplitters || num <= 0)
          return;
        sizeType.Size = this.Orientation != Orientation.Horizontal ? new Size(sizeType.Size.Width, this.Height - num) : new Size(this.Width - num, sizeType.Size.Height);
        this.controlSize.Remove(control);
        this.controlSize.Add(control, sizeType);
        this.PerformLayout();
      }
    }

    /// <summary>Refreshes the splitters.</summary>
    protected override void RefreshSplitters()
    {
      foreach (SplitterControl splitter in this.splitters)
      {
        if (this.Controls.Contains((Control) splitter))
          this.Controls.Remove((Control) splitter);
      }
      this.splitters.Clear();
      if (!this.ShowSplitters || this.splitters.Count != 0)
        return;
      for (int index = 0; index < this.layoutControls.Count - 1; ++index)
      {
        SplitterControl splitterControl = new SplitterControl();
        splitterControl.VIBlendTheme = this.VIBlendTheme;
        splitterControl.SplitContainerBase = (SplitContainerBase) this;
        this.splitters.Add(splitterControl);
        Control control1 = this.layoutControls[index];
        Control control2 = this.layoutControls[index + 1];
        splitterControl.Control1 = control1;
        splitterControl.Control2 = control2;
        splitterControl.control1MinSize = this.Panel1MinSize;
        splitterControl.control2MinSize = this.panel2MinSize;
        splitterControl.orientation = this.Orientation;
      }
      this.Controls.AddRange((Control[]) this.splitters.ToArray());
    }

    /// <summary>Suspends the layout.</summary>
    /// <param name="suspendChildren">if set to <c>true</c> [suspend children].</param>
    public override void SuspendLayout(bool suspendChildren)
    {
      this.SuspendLayout();
      this.Panel1.SuspendLayout();
      this.Panel2.SuspendLayout();
    }

    /// <summary>Resumes the suspended layout.</summary>
    /// <param name="resumeChildren">if set to <c>true</c> [resume children].</param>
    public override void ResumeSuspendedLayout(bool resumeChildren)
    {
      this.Panel2.ResumeLayout(resumeChildren);
      this.Panel1.ResumeLayout(resumeChildren);
      this.ResumeLayout(resumeChildren);
    }

    /// <summary>Adds the control.</summary>
    /// <param name="control">The control.</param>
    /// <param name="sizeType">Type of the size.</param>
    protected override void AddControl(Control control, vSizeType sizeType)
    {
      if (this.controlSize.ContainsKey(control))
        return;
      this.controlSize.Add(control, sizeType);
      this.layoutControls.Add(control);
      this.Controls.Add(control);
      this.RefreshSplitters();
    }

    /// <summary>Removes the control.</summary>
    /// <param name="control">The control.</param>
    protected override void RemoveControl(Control control)
    {
      if (!this.Controls.Contains(control))
        return;
      if (this.controlSize.ContainsKey(control))
        this.controlSize.Remove(control);
      this.layoutControls.Remove(control);
      this.Controls.Remove(control);
      this.RefreshSplitters();
    }

    /// <summary>Removes the control at.</summary>
    /// <param name="index">The index.</param>
    protected override void RemoveControlAt(int index)
    {
      if (this.Controls.Count < index)
        return;
      this.Controls.RemoveAt(index);
      this.layoutControls.RemoveAt(index);
      this.RefreshSplitters();
    }

    /// <summary>
    /// </summary>
    /// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
    protected override void OnLayout(LayoutEventArgs levent)
    {
      base.OnLayout(levent);
      if (this.Controls.Count == 0)
        return;
      List<Control> visibleControls = new List<Control>();
      foreach (Control layoutControl in this.layoutControls)
      {
        if (layoutControl.Visible)
          visibleControls.Add(layoutControl);
      }
      if (visibleControls.Count == 1)
        visibleControls[0].Size = this.Size;
      else
        this.CalculateLayout(visibleControls);
    }

    private void CalculateLayout(List<Control> visibleControls)
    {
      int num1 = this.splitters.Count;
      if (!this.ShowSplitters)
        num1 = 0;
      int num2 = 0;
      switch (this.orientation)
      {
        case Orientation.Horizontal:
          int num3 = this.Width - num1 * this.SplitterSize;
          int num4 = 0;
          double num5 = 0.0;
          int x = 0;
          for (int index = 0; index < visibleControls.Count; ++index)
          {
            vSizeType vSizeType = this.controlSize[visibleControls[index]];
            if (vSizeType.UnitType == SizeUnitType.Pixel)
              num4 += vSizeType.Size.Width;
            else if (vSizeType.UnitType == SizeUnitType.Star)
              num2 += vSizeType.Size.Width;
          }
          if (num2 == 0)
            num2 = 1;
          int num6 = num3 - num4;
          Dictionary<Control, Rectangle> dictionary = new Dictionary<Control, Rectangle>();
          for (int index = 0; index < visibleControls.Count; ++index)
          {
            Control key = visibleControls[index];
            vSizeType vSizeType = this.controlSize[key];
            if (vSizeType.UnitType == SizeUnitType.Pixel)
              num5 = (double) vSizeType.Size.Width;
            else if (vSizeType.UnitType == SizeUnitType.Star)
              num5 = (double) vSizeType.Size.Width * (double) num6 / (double) num2;
            if (index == visibleControls.Count - 1)
              num5 = (double) (this.Width - x);
            dictionary.Add(key, new Rectangle(x, 0, (int) num5, this.Height));
            visibleControls[index].Bounds = dictionary[visibleControls[index]];
            if (this.ShowSplitters)
              x += (int) num5 + this.SplitterSize;
            else
              x += (int) num5 - 1;
          }
          if (!this.ShowSplitters)
            break;
          for (int index = 0; index < this.splitters.Count; ++index)
          {
            SplitterControl splitterControl = this.splitters[index];
            if (splitterControl.Control1 != null)
            {
              splitterControl.Location = new Point(splitterControl.Control1.Right, 0);
              splitterControl.Width = this.SplitterSize;
              splitterControl.Height = this.Height;
            }
          }
          break;
        case Orientation.Vertical:
          int num7 = this.Height - num1 * this.SplitterSize;
          int num8 = 0;
          double num9 = 0.0;
          int y = 0;
          for (int index = 0; index < visibleControls.Count; ++index)
          {
            vSizeType vSizeType = this.controlSize[visibleControls[index]];
            if (vSizeType.UnitType == SizeUnitType.Pixel)
              num8 += vSizeType.Size.Height;
            else if (vSizeType.UnitType == SizeUnitType.Star)
              num2 += vSizeType.Size.Height;
          }
          if (num2 == 0)
            num2 = 1;
          int num10 = num7 - num8;
          for (int index1 = 0; index1 < visibleControls.Count; ++index1)
          {
            Control index2 = visibleControls[index1];
            vSizeType vSizeType = this.controlSize[index2];
            if (vSizeType.UnitType == SizeUnitType.Pixel)
              num9 = (double) vSizeType.Size.Height;
            else if (vSizeType.UnitType == SizeUnitType.Star)
              num9 = (double) vSizeType.Size.Height * (double) num10 / (double) num2;
            if (index1 == visibleControls.Count - 1)
              num9 = (double) (this.Height - y);
            index2.Bounds = new Rectangle(0, y, this.Width, (int) num9);
            if (this.ShowSplitters)
              y += (int) num9 + this.SplitterSize;
            else
              y += (int) num9 - 1;
          }
          if (!this.ShowSplitters)
            break;
          for (int index = 0; index < this.splitters.Count; ++index)
          {
            SplitterControl splitterControl = this.splitters[index];
            if (splitterControl.Control1 != null)
            {
              splitterControl.Location = new Point(0, splitterControl.Control1.Bottom);
              splitterControl.Height = this.SplitterSize;
              splitterControl.Width = this.Width;
            }
          }
          break;
      }
    }

    /// <summary>Maximizes the specified control.</summary>
    /// <param name="control">The control.</param>
    public void Maximize(Control control)
    {
      for (int index = 0; index < this.Controls.Count; ++index)
      {
        if (!this.Controls[index].Equals((object) control))
          this.Controls[index].Visible = false;
      }
      this.PerformLayout();
    }

    /// <summary>Restores this instance.</summary>
    public void Restore()
    {
      for (int index = 0; index < this.Controls.Count; ++index)
        this.Controls[index].Visible = true;
    }
  }
}
