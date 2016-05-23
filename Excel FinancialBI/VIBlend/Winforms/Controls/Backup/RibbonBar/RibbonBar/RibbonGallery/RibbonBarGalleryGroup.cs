// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.RibbonBarGalleryGroup
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a RibbonBarGalleryGroup component</summary>
  [DesignTimeVisible(false)]
  [ToolboxItem(false)]
  public class RibbonBarGalleryGroup : Component
  {
    private int groupHeight = 50;
    private string groupName;

    /// <summary>Gets or sets the height of the group.</summary>
    /// <value>The height of the group.</value>
    [Category("Behavior")]
    [Description("Gets or sets the height of the group.")]
    public int GroupHeight
    {
      get
      {
        return this.groupHeight;
      }
      set
      {
        this.groupHeight = value;
      }
    }

    /// <summary>Gets or sets the name of the group.</summary>
    /// <value>The name of the group.</value>
    [Description("Gets or sets the name of the group.")]
    [Category("Behavior")]
    public string GroupName
    {
      get
      {
        return this.groupName;
      }
      set
      {
        this.groupName = value;
      }
    }
  }
}
