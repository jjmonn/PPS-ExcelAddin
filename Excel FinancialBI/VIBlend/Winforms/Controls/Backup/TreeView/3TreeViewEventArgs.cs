// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTreeViewEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents vTreeView event arguments</summary>
  public class vTreeViewEventArgs : EventArgs
  {
    private vTreeViewAction action;
    private vTreeNode node;

    /// <summary>Gets the action associated with the event.</summary>
    public vTreeViewAction Action
    {
      get
      {
        return this.action;
      }
    }

    /// <summary>Gets the tree node associated with the event.</summary>
    public vTreeNode Node
    {
      get
      {
        return this.node;
      }
    }

    /// <summary>Constructor.</summary>
    /// <param name="node">vTreeNode associated with the event.</param>
    /// <param name="action">Action associated with the event.</param>
    public vTreeViewEventArgs(vTreeNode node, vTreeViewAction action)
    {
      this.node = node;
      this.action = action;
    }
  }
}
