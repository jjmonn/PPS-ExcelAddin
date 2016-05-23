// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.SplitContainerBase
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  [Browsable(false)]
  public abstract class SplitContainerBase : ContainerControl
  {
    protected Dictionary<Control, vSizeType> controlSize = new Dictionary<Control, vSizeType>();
    internal List<Control> layoutControls = new List<Control>();
    internal List<SplitterControl> splitters = new List<SplitterControl>();
    private bool showSplitters = true;
    private int splitterSize = 7;
    protected Orientation orientation;
    internal bool isResizing;

    /// <summary>
    /// Gets or sets a value indicating whether to show splitters.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to show splitters.")]
    public bool ShowSplitters
    {
      get
      {
        return this.showSplitters;
      }
      set
      {
        this.showSplitters = value;
        this.RefreshSplitters();
        this.PerformLayout();
      }
    }

    /// <summary>Gets or sets the size of the splitter.</summary>
    /// <value>The size of the splitter.</value>
    [Description("Gets or sets the size of the splitter.")]
    [DefaultValue(7)]
    [Category("Layout")]
    public int SplitterSize
    {
      get
      {
        return this.splitterSize;
      }
      set
      {
        if (value <= 0 || value >= this.Width)
          return;
        this.splitterSize = value;
        this.RefreshSplitters();
        this.PerformLayout();
      }
    }

    /// <summary>Gets or sets the orientation.</summary>
    /// <value>The orientation.</value>
    [Description("Gets or sets the orientation.")]
    [Category("Behavior")]
    public Orientation Orientation
    {
      get
      {
        return this.orientation;
      }
      set
      {
        this.orientation = value;
        this.RefreshSplitters();
        this.PerformLayout();
      }
    }

    protected abstract void RefreshSplitters();

    protected abstract void InsertControl(int index, Control control, vSizeType sizeType);

    protected abstract void AddControl(Control control, vSizeType sizeType);

    protected abstract void RemoveControl(Control control);

    protected abstract void RemoveControlAt(int index);

    public abstract vSizeType GetControlSize(Control control);

    public abstract void SetControlSize(Control control, vSizeType sizeType);

    /// <summary>Suspends the layout.</summary>
    /// <param name="suspendChildren">if set to <c>true</c> [suspend children].</param>
    public virtual void SuspendLayout(bool suspendChildren)
    {
    }

    /// <summary>Resumes the suspended layout.</summary>
    /// <param name="resumeChildren">if set to <c>true</c> [resume children].</param>
    public virtual void ResumeSuspendedLayout(bool resumeChildren)
    {
    }
  }
}
