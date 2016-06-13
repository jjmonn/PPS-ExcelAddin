// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTreeViewDragCancelEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents vTreeView drag and drop cancel event arguments
  /// </summary>
  public class vTreeViewDragCancelEventArgs : vTreeViewDragEventArgs
  {
    private bool cancel;

    /// <summary>Determines whether the event is canceled.</summary>
    public bool Cancel
    {
      get
      {
        return this.cancel;
      }
      set
      {
        this.cancel = value;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="dragNode">The drag vTreeNode.</param>
    /// <param name="targetNode">The target vTreeNode.</param>
    public vTreeViewDragCancelEventArgs(vTreeNode dragNode, vTreeNode targetNode)
      : base(dragNode, targetNode)
    {
    }
  }
}
