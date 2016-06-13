// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DrawNavPaneEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  public class DrawNavPaneEventArgs : EventArgs
  {
    private Graphics g;
    private ControlState state;
    private vNavPaneHeader header;
    private bool handled;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.DrawNavPaneEventArgs" /> is handled.
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
        return this.g;
      }
    }

    /// <summary>Gets the header.</summary>
    /// <value>The header.</value>
    public vNavPaneHeader Header
    {
      get
      {
        return this.header;
      }
    }

    /// <summary>Gets the state.</summary>
    /// <value>The state.</value>
    public ControlState State
    {
      get
      {
        return this.state;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DrawNavPaneEventArgs" /> class.
    /// </summary>
    /// <param name="g">The g.</param>
    /// <param name="state">The state.</param>
    /// <param name="header">The header.</param>
    public DrawNavPaneEventArgs(Graphics g, ControlState state, vNavPaneHeader header)
    {
      this.header = header;
      this.g = g;
      this.state = state;
    }
  }
}
