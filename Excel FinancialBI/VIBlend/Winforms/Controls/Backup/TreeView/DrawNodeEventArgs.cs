// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DrawNodeEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  public class DrawNodeEventArgs : EventArgs
  {
    private Graphics g;
    private vTreeNode node;
    private bool isMouseOver;
    private bool isSelected;
    private bool handled;
    private TreeBackGroundElement background;

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:VIBlend.WinForms.Controls.DrawNodeEventArgs" /> is handled.
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

    public TreeBackGroundElement Background
    {
      get
      {
        return this.background;
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

    /// <summary>Gets the node.</summary>
    /// <value>The node.</value>
    public vTreeNode Node
    {
      get
      {
        return this.node;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is mouse over.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is mouse over; otherwise, <c>false</c>.
    /// </value>
    public bool IsMouseOver
    {
      get
      {
        return this.isMouseOver;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is selected.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
    /// </value>
    public bool IsSelected
    {
      get
      {
        return this.isSelected;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.DrawNodeEventArgs" /> class.
    /// </summary>
    /// <param name="g">The g.</param>
    /// <param name="node">The node.</param>
    /// <param name="isMouseOver">if set to <c>true</c> [is mouse over].</param>
    /// <param name="isSelected">if set to <c>true</c> [is selected].</param>
    /// <param name="background">The background.</param>
    public DrawNodeEventArgs(Graphics g, vTreeNode node, bool isMouseOver, bool isSelected, TreeBackGroundElement background)
    {
      this.g = g;
      this.node = node;
      this.isMouseOver = isMouseOver;
      this.isSelected = isSelected;
      this.background = background;
    }
  }
}
