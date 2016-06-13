// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.GalleryButton
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  public class GalleryButton : vRibbonButton
  {
    private RibbonBarGalleryGroup group;

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

    /// <summary>Gets or sets the gallery group.</summary>
    /// <value>The gallery group.</value>
    [Category("Behavior")]
    [DefaultValue(null)]
    [Description("Gets or sets the gallery group.")]
    public RibbonBarGalleryGroup GalleryGroup
    {
      get
      {
        return this.group;
      }
      set
      {
        this.group = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.GalleryButton" /> class.
    /// </summary>
    public GalleryButton()
    {
      this.PaintDefaultStateBorder = true;
      this.PaintDefaultStateFill = true;
    }
  }
}
