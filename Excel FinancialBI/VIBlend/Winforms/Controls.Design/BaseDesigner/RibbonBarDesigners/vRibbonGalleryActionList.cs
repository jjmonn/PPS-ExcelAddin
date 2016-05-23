// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vRibbonGalleryActionList
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace VIBlend.WinForms.Controls.Design
{
  public class vRibbonGalleryActionList : DesignerActionList
  {
    public vRibbonBarGallery ControlBase
    {
      get
      {
        return this.Component as vRibbonBarGallery;
      }
    }

    public vRibbonGalleryActionList(vRibbonBarGallery ctrl)
      : base((IComponent) ctrl)
    {
    }

    public override DesignerActionItemCollection GetSortedActionItems()
    {
      return new DesignerActionItemCollection() { (DesignerActionItem) new DesignerActionMethodItem((DesignerActionList) this, "AddGalleryButton", "Add Gallery Button") };
    }

    protected virtual void AddGalleryButton()
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Button");
      GalleryButton galleryButton = (GalleryButton) designerHost.CreateComponent(typeof (GalleryButton));
      galleryButton.Text = "";
      galleryButton.Size = new Size(32, 32);
      this.ControlBase.GalleryButtons.Add(galleryButton);
      transaction.Commit();
    }
  }
}
