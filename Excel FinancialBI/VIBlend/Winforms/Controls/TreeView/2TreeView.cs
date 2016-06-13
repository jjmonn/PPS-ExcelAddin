// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vTreeNodeLabelEditEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vTreeNode label edit event arguments</summary>
  public class vTreeNodeLabelEditEventArgs : EventArgs
  {
    private bool cancel;
    private string label;
    private vTreeNode node;

    /// <summary>Determines whether to cancel the edit or not</summary>
    public bool CancelEdit
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

    /// <summary>Gets the label for the vTreeNode being edited</summary>
    public string Label
    {
      get
      {
        return this.label;
      }
    }

    /// <summary>Gets the vTreeNode being edited</summary>
    public vTreeNode Node
    {
      get
      {
        return this.node;
      }
    }

    /// <summary>Constructor</summary>
    /// <param name="node">vTreeNode to edit</param>
    /// <param name="label">The vTreeNode's label</param>
    public vTreeNodeLabelEditEventArgs(vTreeNode node, string label)
    {
      this.node = node;
      this.label = label;
    }
  }
}
