// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vContextMenu
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vContextMenu control</summary>
  /// <remarks>
  /// Displays a shortcut menu when the user right-clicks on the associated control that supports context menu operations.
  /// </remarks>
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vContextMenu), "ControlIcons.vContextMenu.ico")]
  [Description("Displays a shortcut menu when the user right-clicks on the associated control that supports context menu operations.")]
  public class vContextMenu : ContextMenu
  {
    /// <summary>vContextMenu constructor</summary>
    public vContextMenu()
    {
    }

    /// <summary>vContextMenu constructor</summary>
    public vContextMenu(MenuItem[] items)
      : base(items)
    {
    }
  }
}
