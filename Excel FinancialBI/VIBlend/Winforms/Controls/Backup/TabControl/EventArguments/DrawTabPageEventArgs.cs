// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DrawTabPageEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class DrawTabPageEventArgs : EventArgs
  {
    private vTabPage tabPage;
    private Graphics graphics;
    private Rectangle bounds;
    private bool handled;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.DrawTabPageEventArgs" /> is handled.
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

    /// <summary>Gets the tab page.</summary>
    /// <value>The tab page.</value>
    public vTabPage TabPage
    {
      get
      {
        return this.tabPage;
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
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DrawTabPageEventArgs" /> class.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="page">The page.</param>
    /// <param name="bounds">The bounds.</param>
    public DrawTabPageEventArgs(Graphics graphics, vTabPage page, Rectangle bounds)
    {
      this.tabPage = page;
      this.bounds = bounds;
      this.graphics = graphics;
    }
  }
}
