// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTreeViewDragEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents vTreeView drag and drop event arguments</summary>
  public class vTreeViewDragEventArgs : EventArgs
  {
    private vTreeNode targetNode;
    private vTreeNode dragNode;

    /// <summary>The drag vTreeNode.</summary>
    public vTreeNode DragNode
    {
      get
      {
        return this.dragNode;
      }
    }

    /// <summary>The target vTreeNode.</summary>
    public vTreeNode TargetNode
    {
      get
      {
        return this.targetNode;
      }
    }

    /// <summary>Constructors</summary>
    /// <param name="dragNode">The drag vTreeNode</param>
    /// <param name="targetNode">The target vTreeNode</param>
    public vTreeViewDragEventArgs(vTreeNode dragNode, vTreeNode targetNode)
    {
      this.dragNode = dragNode;
      this.targetNode = targetNode;
    }
  }
}
