// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.ScrollableControlMiniBase
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System.ComponentModel;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  /// <exclude />
  [ToolboxItem(false)]
  public class ScrollableControlMiniBase : Control, IScrollableControlBase
  {
    private AnimationManager animationManager;
    private bool allowAnimations;

    [DefaultValue(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
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
      }
    }

    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        return this.animationManager;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.Utilities.ScrollableControlMiniBase" /> class.
    /// </summary>
    public ScrollableControlMiniBase()
    {
      this.animationManager = new AnimationManager((Control) this);
    }
  }
}
