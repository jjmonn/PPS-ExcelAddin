// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DesignerAttributeTypeDescriptor
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace VIBlend.WinForms.Controls
{
  public sealed class DesignerAttributeTypeDescriptor : CustomTypeDescriptor
  {
    private IServiceProvider _provider;

    internal DesignerAttributeTypeDescriptor(ICustomTypeDescriptor parent, IComponent component)
      : base(parent)
    {
      if (component == null)
        return;
      this._provider = (IServiceProvider) component.Site;
    }

    public override AttributeCollection GetAttributes()
    {
      AttributeCollection attributes = base.GetAttributes();
      List<Attribute> attributeList = new List<Attribute>();
      foreach (Attribute attribute in attributes)
      {
        DesignerAttribute designerAttribute1 = attribute as DesignerAttribute;
        if (designerAttribute1 != null && designerAttribute1.DesignerBaseTypeName.StartsWith("System.ComponentModel.Design.IDesigner"))
        {
          ITypeResolutionService resolutionService = (ITypeResolutionService) null;
          if (this._provider != null)
            resolutionService = (ITypeResolutionService) this._provider.GetService(typeof (ITypeResolutionService));
          if (resolutionService != null && resolutionService.GetType(designerAttribute1.DesignerTypeName) == null)
          {
            DesignerAttribute designerAttribute2 = new DesignerAttribute("System.Windows.Forms.Design.ControlDesigner, System.Design");
            attributeList.Add((Attribute) designerAttribute2);
            continue;
          }
        }
        attributeList.Add(attribute);
      }
      return new AttributeCollection(attributeList.ToArray());
    }
  }
}
