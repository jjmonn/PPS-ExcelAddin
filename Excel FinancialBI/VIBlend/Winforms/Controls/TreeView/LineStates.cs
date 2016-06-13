// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.LineStates
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents a class which calculates the tree node lines.
  /// </summary>
  public class LineStates
  {
    private LineState[] states = new LineState[10];
    private int used;

    public int Depth
    {
      get
      {
        return this.used;
      }
    }

    public LineState this[int depth]
    {
      get
      {
        if (depth >= this.used)
          return LineState.None;
        return this.states[depth];
      }
    }

    public void Pop()
    {
      if (this.used <= 0)
        return;
      --this.used;
    }

    /// <summary>Pushes the specified state.</summary>
    /// <param name="state">The state.</param>
    public void Push(LineState state)
    {
      if (this.used > 0)
        state |= LineState.HasParent;
      if (this.states.Length == this.used)
      {
        LineState[] lineStateArray = new LineState[this.used * 2];
        Array.Copy((Array) this.states, (Array) lineStateArray, this.used);
        this.states = lineStateArray;
      }
      this.states[this.used++] = state;
    }
  }
}
