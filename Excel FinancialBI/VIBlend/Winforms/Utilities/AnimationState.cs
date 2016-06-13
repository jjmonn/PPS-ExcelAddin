// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.AnimationState
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System.Drawing;

namespace VIBlend.Utilities
{
  /// <exclude />
  public class AnimationState
  {
    public bool backAnimating;
    public int currentFrame;
    public int MaxFrameRate;
    public Color startColor1;
    public Color startColor2;
    public Color startColor3;
    public Color startColor4;
    public Color endColor1;
    public Color endColor2;
    public Color endColor3;
    public Color endColor4;
    public Color animatingColor1;
    public Color animatingColor2;
    public Color animatingColor3;
    public Color animatingColor4;

    public AnimationState()
    {
      this.backAnimating = false;
      this.currentFrame = 0;
      this.MaxFrameRate = 6;
    }
  }
}
