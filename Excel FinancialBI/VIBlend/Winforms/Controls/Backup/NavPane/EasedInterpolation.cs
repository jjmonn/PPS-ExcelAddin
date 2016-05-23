// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.EasedInterpolation
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public abstract class EasedInterpolation : Interpolation
  {
    /// <summary>Gets or sets the edge behavior.</summary>
    /// <value>The edge behavior.</value>
    public EdgeBehaviorEnum EdgeBehavior { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.EasedInterpolation" /> class.
    /// </summary>
    public EasedInterpolation()
    {
      this.EdgeBehavior = EdgeBehaviorEnum.EaseOut;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.EasedInterpolation" /> class.
    /// </summary>
    /// <param name="edgeBehavior">The edge behavior.</param>
    public EasedInterpolation(EdgeBehaviorEnum edgeBehavior)
    {
      this.EdgeBehavior = edgeBehavior;
    }

    /// <summary>Gets the ease in alpha.</summary>
    /// <param name="progress">The progress.</param>
    /// <returns></returns>
    protected abstract double GetEaseInAlpha(double progress);

    /// <summary>Gets the ease out alpha.</summary>
    /// <param name="progress">The progress.</param>
    /// <returns></returns>
    protected abstract double GetEaseOutAlpha(double progress);

    /// <summary>Gets the ease in out alpha.</summary>
    /// <param name="timeFraction">The time fraction.</param>
    /// <returns></returns>
    protected virtual double GetEaseInOutAlpha(double timeFraction)
    {
      return timeFraction > 0.5 ? 0.5 + 0.5 * this.GetEaseOutAlpha((timeFraction - 0.5) * 2.0) : 0.5 * this.GetEaseInAlpha(timeFraction * 2.0);
    }

    /// <summary>Gets the alpha.</summary>
    /// <param name="progress">The progress.</param>
    /// <returns></returns>
    public override double GetAlpha(double progress)
    {
      switch (this.EdgeBehavior)
      {
        case EdgeBehaviorEnum.EaseIn:
          return this.GetEaseInAlpha(progress);
        case EdgeBehaviorEnum.EaseOut:
          return this.GetEaseOutAlpha(progress);
        default:
          return this.GetEaseInOutAlpha(progress);
      }
    }
  }
}
