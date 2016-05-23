// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vLabelItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vLabelItem</summary>
  /// <remarks>
  /// vLabelItem represents a single label inside a vMultiLabel control
  /// </remarks>
  [ToolboxItem(false)]
  [Designer("VIBlend.WinForms.Controls.Design.vLabelItemDesigner, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vLabelItem : vLabel
  {
    private int row;
    private int offset;

    /// <summary>
    /// Gets or sets the row of the vLabelItem within the vMultiLabel control
    /// </summary>
    public int Row
    {
      get
      {
        return this.row;
      }
      set
      {
        this.row = value;
        if (this.Parent != null)
          this.Parent.PerformLayout();
        this.Refresh();
      }
    }

    /// <summary>Gets or sets the Horizontal offset of the vLabelItem</summary>
    [DefaultValue(0)]
    public int HorizontalOffset
    {
      get
      {
        return this.offset;
      }
      set
      {
        this.offset = value;
        if (this.Parent != null)
          this.Parent.PerformLayout();
        this.Refresh();
      }
    }
  }
}
