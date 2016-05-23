// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.AnimationManager
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  /// <exclude />
  public class AnimationManager
  {
    public static Hashtable controls = new Hashtable();
    private AnimationColorCalculator colorAnimator = new AnimationColorCalculator();
    internal List<BackgroundElement> registeredElementsKeys = new List<BackgroundElement>();
    public List<AnimationState> registeredFillsKeys = new List<AnimationState>();
    private List<BackgroundElement> elementsToRemove = new List<BackgroundElement>();
    private List<AnimationState> fillsToRemove = new List<AnimationState>();
    private Timer animationTimer;
    private bool timerStarted;
    private Control hostingControl;

    public event EventHandler AnimationEnd;

    public AnimationManager(Control hostingControl)
    {
      this.animationTimer = new Timer();
      this.animationTimer.Interval = 20;
      this.animationTimer.Tick += new EventHandler(this.animationTimer_Tick);
      this.hostingControl = hostingControl;
    }

    protected virtual void OnAnimationFinished()
    {
      if (this.AnimationEnd == null)
        return;
      this.AnimationEnd((object) this, EventArgs.Empty);
    }

    public Color CalculateColor(Color startColor, Color endColor, int frameNumber, int framesCount)
    {
      return this.colorAnimator.CalculateColor(startColor, endColor, frameNumber, framesCount);
    }

    public bool Contains(AnimationState element)
    {
      return this.registeredFillsKeys.Contains(element);
    }

    internal void AddBackGroundElement(BackgroundElement element)
    {
      if (this.registeredElementsKeys.Contains(element))
        return;
      this.registeredElementsKeys.Add(element);
      if (this.registeredElementsKeys.Count != 1 || this.timerStarted)
        return;
      this.animationTimer.Start();
      this.timerStarted = true;
    }

    public void AddFillElement(AnimationState element)
    {
      if (!this.registeredFillsKeys.Contains(element))
        this.registeredFillsKeys.Add(element);
      if (this.registeredFillsKeys.Count != 1 || this.timerStarted)
        return;
      this.animationTimer.Start();
      this.timerStarted = true;
    }

    public void RemoveFillElement(AnimationState element)
    {
      if (!this.registeredFillsKeys.Contains(element))
        return;
      this.registeredFillsKeys.Remove(element);
    }

    internal void RemoveBackGroundElement(BackgroundElement element)
    {
      if (!this.registeredElementsKeys.Contains(element))
        return;
      element.currentFrame = 0;
      this.registeredElementsKeys.Remove(element);
      this.OnAnimationFinished();
    }

    private void animationTimer_Tick(object sender, EventArgs e)
    {
      if (this.registeredElementsKeys.Count == 0 && this.registeredFillsKeys.Count == 0)
      {
        this.animationTimer.Stop();
        this.timerStarted = false;
      }
      for (int index = 0; index < this.registeredElementsKeys.Count; ++index)
      {
        BackgroundElement backgroundElement = this.registeredElementsKeys[index];
        if (!backgroundElement.BackAnimating)
        {
          ++backgroundElement.currentFrame;
          if (backgroundElement.currentFrame < backgroundElement.MaxFrameRate)
          {
            Color color1 = this.colorAnimator.CalculateColor(backgroundElement.animatingStartColor1, backgroundElement.animatingEndColor1, backgroundElement.currentFrame, backgroundElement.MaxFrameRate);
            Color color2 = this.colorAnimator.CalculateColor(backgroundElement.animatingStartColor2, backgroundElement.animatingEndColor2, backgroundElement.currentFrame, backgroundElement.MaxFrameRate);
            Color color3 = this.colorAnimator.CalculateColor(backgroundElement.animatingStartColor3, backgroundElement.animatingEndColor3, backgroundElement.currentFrame, backgroundElement.MaxFrameRate);
            Color color4 = this.colorAnimator.CalculateColor(backgroundElement.animatingStartColor4, backgroundElement.animatingEndColor4, backgroundElement.currentFrame, backgroundElement.MaxFrameRate);
            backgroundElement.animatingColor1 = color1;
            backgroundElement.animatingColor2 = color2;
            backgroundElement.animatingColor3 = color3;
            backgroundElement.animatingColor4 = color4;
          }
          else
          {
            this.elementsToRemove.Add(backgroundElement);
            backgroundElement.animatingColor1 = backgroundElement.animatingEndColor1;
            backgroundElement.animatingColor2 = backgroundElement.animatingEndColor2;
            backgroundElement.animatingColor3 = backgroundElement.animatingEndColor3;
            backgroundElement.animatingColor4 = backgroundElement.animatingEndColor4;
          }
        }
        else
        {
          --backgroundElement.currentFrame;
          if (backgroundElement.currentFrame >= 0)
          {
            Color color1 = this.colorAnimator.CalculateColor(backgroundElement.animatingStartColor1, backgroundElement.animatingEndColor1, backgroundElement.currentFrame, backgroundElement.MaxFrameRate);
            Color color2 = this.colorAnimator.CalculateColor(backgroundElement.animatingStartColor2, backgroundElement.animatingEndColor2, backgroundElement.currentFrame, backgroundElement.MaxFrameRate);
            Color color3 = this.colorAnimator.CalculateColor(backgroundElement.animatingStartColor3, backgroundElement.animatingEndColor3, backgroundElement.currentFrame, backgroundElement.MaxFrameRate);
            Color color4 = this.colorAnimator.CalculateColor(backgroundElement.animatingStartColor4, backgroundElement.animatingEndColor4, backgroundElement.currentFrame, backgroundElement.MaxFrameRate);
            backgroundElement.animatingColor1 = color1;
            backgroundElement.animatingColor2 = color2;
            backgroundElement.animatingColor3 = color3;
            backgroundElement.animatingColor4 = color4;
          }
          else
          {
            this.elementsToRemove.Add(backgroundElement);
            backgroundElement.animatingColor1 = backgroundElement.animatingStartColor1;
            backgroundElement.animatingColor2 = backgroundElement.animatingStartColor2;
            backgroundElement.animatingColor3 = backgroundElement.animatingStartColor3;
            backgroundElement.animatingColor4 = backgroundElement.animatingStartColor4;
          }
        }
      }
      for (int index = 0; index < this.elementsToRemove.Count; ++index)
        this.RemoveBackGroundElement(this.elementsToRemove[index]);
      this.elementsToRemove.Clear();
      for (int index = 0; index < this.registeredFillsKeys.Count; ++index)
      {
        AnimationState animationState = this.registeredFillsKeys[index];
        if (index < this.registeredFillsKeys.Count && this.registeredFillsKeys.Count > 1)
        {
          if (!animationState.backAnimating)
          {
            if (animationState.currentFrame < animationState.MaxFrameRate - 2)
            {
              animationState.currentFrame = animationState.MaxFrameRate - 2;
              this.fillsToRemove.Add(animationState);
            }
          }
          else if (animationState.currentFrame > 1)
          {
            animationState.currentFrame = 1;
            this.fillsToRemove.Add(animationState);
          }
        }
        if (!animationState.backAnimating)
        {
          ++animationState.currentFrame;
          if (animationState.currentFrame < animationState.MaxFrameRate)
          {
            Color color1 = this.colorAnimator.CalculateColor(animationState.startColor1, animationState.endColor1, animationState.currentFrame, animationState.MaxFrameRate);
            Color color2 = this.colorAnimator.CalculateColor(animationState.startColor2, animationState.endColor2, animationState.currentFrame, animationState.MaxFrameRate);
            Color color3 = this.colorAnimator.CalculateColor(animationState.startColor3, animationState.endColor3, animationState.currentFrame, animationState.MaxFrameRate);
            Color color4 = this.colorAnimator.CalculateColor(animationState.startColor4, animationState.endColor4, animationState.currentFrame, animationState.MaxFrameRate);
            animationState.animatingColor1 = color1;
            animationState.animatingColor2 = color2;
            animationState.animatingColor3 = color3;
            animationState.animatingColor4 = color4;
          }
          else
          {
            this.fillsToRemove.Add(animationState);
            animationState.animatingColor1 = animationState.endColor1;
            animationState.animatingColor2 = animationState.endColor2;
            animationState.animatingColor3 = animationState.endColor3;
            animationState.animatingColor4 = animationState.endColor4;
            continue;
          }
        }
        else
        {
          --animationState.currentFrame;
          if (animationState.currentFrame >= 0)
          {
            Color color1 = this.colorAnimator.CalculateColor(animationState.startColor1, animationState.endColor1, animationState.currentFrame, animationState.MaxFrameRate);
            Color color2 = this.colorAnimator.CalculateColor(animationState.startColor2, animationState.endColor2, animationState.currentFrame, animationState.MaxFrameRate);
            Color color3 = this.colorAnimator.CalculateColor(animationState.startColor3, animationState.endColor3, animationState.currentFrame, animationState.MaxFrameRate);
            Color color4 = this.colorAnimator.CalculateColor(animationState.startColor4, animationState.endColor4, animationState.currentFrame, animationState.MaxFrameRate);
            animationState.animatingColor1 = color1;
            animationState.animatingColor2 = color2;
            animationState.animatingColor3 = color3;
            animationState.animatingColor4 = color4;
          }
          else
          {
            this.fillsToRemove.Add(animationState);
            animationState.animatingColor1 = animationState.startColor1;
            animationState.animatingColor2 = animationState.startColor2;
            animationState.animatingColor3 = animationState.startColor3;
            animationState.animatingColor4 = animationState.startColor4;
            continue;
          }
        }
        if (animationState.currentFrame <= 0)
          this.fillsToRemove.Add(animationState);
      }
      for (int index = 0; index < this.fillsToRemove.Count; ++index)
        this.RemoveFillElement(this.fillsToRemove[index]);
      this.fillsToRemove.Clear();
      this.hostingControl.Invalidate();
    }
  }
}
