// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.AnimationColorCalculator
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System.Drawing;

namespace VIBlend.Utilities
{
  internal class AnimationColorCalculator
  {
    private int GetColorPartValue(int startValue, int endValue, int currentFrame, int framesCount)
    {
      if (startValue == endValue)
        return endValue;
      if (currentFrame <= 0)
        return startValue;
      if (currentFrame >= framesCount)
        return endValue;
      double num = (double) currentFrame / (double) framesCount;
      return (int) (byte) ((uint) startValue + (uint) (int) ((double) (endValue - startValue) * num));
    }

    public Color CalculateColor(Color startColor, Color endColor, int frameNumber, int framesCount)
    {
      return Color.FromArgb(this.GetColorPartValue((int) startColor.A, (int) endColor.A, frameNumber, framesCount), this.GetColorPartValue((int) startColor.R, (int) endColor.R, frameNumber, framesCount), this.GetColorPartValue((int) startColor.G, (int) endColor.G, frameNumber, framesCount), this.GetColorPartValue((int) startColor.B, (int) endColor.B, frameNumber, framesCount));
    }
  }
}
