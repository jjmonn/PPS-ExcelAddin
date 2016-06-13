// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTreeViewCancelEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vTreeView cancel event arguments</summary>
  public class vTreeViewCancelEventArgs : CancelEventArgs
  {
    private vTreeViewAction action;
    private vTreeNode node;

    /// <summary>Event's TreeViewAction</summary>
    public vTreeViewAction Action
    {
      get
      {
        return this.action;
      }
    }

    /// <summary>Event's vTreeNode.</summary>
    public vTreeNode Node
    {
      get
      {
        return this.node;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="node">vTreeView node.</param>
    /// <param name="cancel">Event's cancel state.</param>
    /// <param name="action">TreeViewAction for the event.</param>
    public vTreeViewCancelEventArgs(vTreeNode node, bool cancel, vTreeViewAction action)
    {
      this.node = node;
      this.action = action;
    }
  }
}
