// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DrawOutlookNavPaneStatusEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  public class DrawOutlookNavPaneStatusEventArgs : EventArgs
  {
    private Graphics g;
    private ControlState state;
    private vOutlookStatusItem statusItem;
    private bool handled;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.DrawOutlookNavPaneStatusEventArgs" /> is handled.
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
    public vOutlookStatusItem Status
    {
      get
      {
        return this.statusItem;
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
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DrawOutlookNavPaneStatusEventArgs" /> class.
    /// </summary>
    /// <param name="g">The g.</param>
    /// <param name="state">The state.</param>
    /// <param name="statusItem">The status item.</param>
    public DrawOutlookNavPaneStatusEventArgs(Graphics g, ControlState state, vOutlookStatusItem statusItem)
    {
      this.statusItem = statusItem;
      this.g = g;
      this.state = state;
    }
  }
}
