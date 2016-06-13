// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ExponentialInterpolation
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class ExponentialInterpolation : EasedInterpolation
  {
    /// <exclude />
    public double Power { get; set; }

    /// <exclude />
    public ExponentialInterpolation()
      : this(2.0, EdgeBehaviorEnum.EaseInOut)
    {
    }

    /// <exclude />
    public ExponentialInterpolation(double power, EdgeBehaviorEnum edgeBehavior)
      : base(edgeBehavior)
    {
      this.Power = power;
    }

    /// <exclude />
    protected override double GetEaseInAlpha(double timeFraction)
    {
      return Math.Pow(timeFraction, this.Power);
    }

    /// <exclude />
    protected override double GetEaseOutAlpha(double timeFraction)
    {
      return Math.Pow(timeFraction, 1.0 / this.Power);
    }
  }
}
