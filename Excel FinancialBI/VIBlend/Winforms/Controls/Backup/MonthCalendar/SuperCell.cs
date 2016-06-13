// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vCell
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  internal class vCell
  {
    public Rectangle bounds;
    public vMonthCalendar calendar;
    public DateTime date;
    public bool differentMonth;
    public bool today;
    public bool selected;
    public bool weekend;

    public vCell(vMonthCalendar calendar, Rectangle bounds, DateTime date, bool differentMonth, bool today, bool selected, bool weekend)
    {
      this.calendar = calendar;
      this.bounds = bounds;
      this.date = date;
    }
  }
}
