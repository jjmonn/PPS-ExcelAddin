// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.ScrollableControlBase
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System.ComponentModel;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  /// <exclude />
  [ToolboxItem(false)]
  public class ScrollableControlBase : ScrollableControl, IScrollableControlBase
  {
    private AnimationManager animationManager;
    private bool allowAnimations;

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

    public AnimationManager AnimationManager
    {
      get
      {
        return this.animationManager;
      }
    }

    public ScrollableControlBase()
    {
      this.animationManager = new AnimationManager((Control) this);
    }
  }
}
