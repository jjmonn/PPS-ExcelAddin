// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DrawTabControlTitleEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class DrawTabControlTitleEventArgs : EventArgs
  {
    private Graphics graphics;
    private Rectangle bounds;
    private bool handled;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.DrawTabControlTitleEventArgs" /> is handled.
    /// </summary>
    /// <value><c>true</c> if handled; otherwise, <c>false</c>.</value>
    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    /// <summary>Gets the graphics.</summary>
    /// <value>The graphics.</value>
    public Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
    }

    /// <summary>Gets the bounds.</summary>
    /// <value>The bounds.</value>
    public Rectangle Bounds
    {
      get
      {
        return this.bounds;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DrawTabControlTitleEventArgs" /> class.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="bounds">The bounds.</param>
    public DrawTabControlTitleEventArgs(Graphics graphics, Rectangle bounds)
    {
      this.bounds = bounds;
      this.graphics = graphics;
    }
  }
}
