// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.IFormatEditItem
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;

namespace VIBlend.WinForms.Controls
{
  internal interface IFormatEditItem
  {
    /// <summary>Sets the item editor.</summary>
    /// <param name="editedDateTime">The edited date time.</param>
    /// <returns></returns>
    IDateTimeEditor SetItemEditor(DateTime editedDateTime);

    /// <summary>Sets the item.</summary>
    /// <param name="offset">The offset.</param>
    /// <param name="editedDateTime">The edited date time.</param>
    /// <returns></returns>
    DateTime SetItem(int offset, DateTime editedDateTime);
  }
}
