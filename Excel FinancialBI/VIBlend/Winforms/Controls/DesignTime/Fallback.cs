// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DynamicDesignerProvider
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;

namespace VIBlend.WinForms.Controls
{
  public sealed class DynamicDesignerProvider : TypeDescriptionProvider
  {
    public DynamicDesignerProvider(TypeDescriptionProvider parent)
      : base(parent)
    {
    }

    public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
    {
      if (objectType.Assembly == typeof (vButton).Assembly)
      {
        IComponent component = instance as IComponent;
        if (component != null && component.Site != null)
          return (ICustomTypeDescriptor) new DesignerAttributeTypeDescriptor(base.GetTypeDescriptor(objectType, instance), component);
      }
      return base.GetTypeDescriptor(objectType, instance);
    }
  }
}
