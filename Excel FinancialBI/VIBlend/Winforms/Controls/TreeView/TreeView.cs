// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.TreeBackGroundElement
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.Drawing;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <exclude />
  public class TreeBackGroundElement : BackgroundElement
  {
    private vTreeNode node;

    /// <summary>Gets or sets the binded node.</summary>
    /// <value>The binded node.</value>
    public vTreeNode BindedNode
    {
      get
      {
        return this.node;
      }
      set
      {
        this.node = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.TreeBackGroundElement" /> class.
    /// </summary>
    /// <param name="rect">The rect.</param>
    /// <param name="host">The host.</param>
    public TreeBackGroundElement(Rectangle rect, IScrollableControlBase host)
      : base(rect, host)
    {
    }
  }
}
