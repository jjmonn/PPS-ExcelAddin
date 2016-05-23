// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.Design.vRibbonGalleryDesigner
// Assembly: VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 45AFAFFC-F25B-4966-A6F6-769F25D5920D
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.Design.dll

using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace VIBlend.WinForms.Controls.Design
{
  public class vRibbonGalleryDesigner : vDesignerBase
  {
    private vRibbonBarGallery gallery;

    public override DesignerActionListCollection ActionLists
    {
      get
      {
        DesignerActionListCollection actionListCollection = new DesignerActionListCollection();
        actionListCollection.AddRange(base.ActionLists);
        vRibbonGalleryActionList galleryActionList = new vRibbonGalleryActionList(this.gallery);
        actionListCollection.Add((DesignerActionList) galleryActionList);
        return actionListCollection;
      }
    }

    public override DesignerVerbCollection Verbs
    {
      get
      {
        return new DesignerVerbCollection(new DesignerVerb[1]{ new DesignerVerb("Add Gallery Button", new EventHandler(this.AddGalleryButton)) });
      }
    }

    protected virtual void AddGalleryButton(object sender, EventArgs e)
    {
      IDesignerHost designerHost = (IDesignerHost) this.GetService(typeof (IDesignerHost));
      DesignerTransaction transaction = designerHost.CreateTransaction("Add Button");
      GalleryButton galleryButton = (GalleryButton) designerHost.CreateComponent(typeof (GalleryButton));
      galleryButton.Text = "";
      galleryButton.Size = new Size(32, 32);
      this.gallery.GalleryButtons.Add(galleryButton);
      transaction.Commit();
    }

    public override void Initialize(IComponent component)
    {
      base.Initialize(component);
      this.gallery = component as vRibbonBarGallery;
    }

    protected override bool GetHitTest(Point point)
    {
      if (this.gallery == null)
        return base.GetHitTest(point);
      Rectangle screen = this.gallery.RectangleToScreen(this.gallery.ClientRectangle);
      if (point.X > screen.Right - 16)
        return true;
      return base.GetHitTest(point);
    }
  }
}
