// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.ListItemComparer
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;

namespace VIBlend.WinForms.Controls
{
  public class ListItemComparer : IComparer<ListItem>
  {
    private bool ascending;

    public ListItemComparer(bool ascending)
    {
      this.ascending = ascending;
    }

    /// <summary>Compares the specified item1.</summary>
    /// <param name="item1">The item1.</param>
    /// <param name="item2">The item2.</param>
    /// <returns></returns>
    public int Compare(ListItem item1, ListItem item2)
    {
      if (string.IsNullOrEmpty(item1.Text) || string.IsNullOrEmpty(item2.Text))
        return item1.ToString().CompareTo(item2.ToString());
      DateTime result1;
      DateTime result2;
      if (DateTime.TryParse(item1.Text, out result1) && DateTime.TryParse(item2.Text, out result2))
      {
        if (!this.ascending)
          return result2.CompareTo(result1);
        return result1.CompareTo(result2);
      }
      double result3;
      double result4;
      if (double.TryParse(item1.Text, out result3) && double.TryParse(item2.Text, out result4))
      {
        if (!this.ascending)
          return result4.CompareTo(result3);
        return result3.CompareTo(result4);
      }
      if (!this.ascending)
        return item2.Text.CompareTo(item1.Text);
      return item1.Text.CompareTo(item2.Text);
    }
  }
}
