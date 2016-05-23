// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.CalendarPaintEventArgs
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Drawing;

namespace VIBlend.WinForms.Controls
{
  public class CalendarPaintEventArgs
  {
    private Graphics graphics;
    private DateTime currentDate;
    private Rectangle bounds;
    private vMonthCalendar calendar;

    public vMonthCalendar Calendar
    {
      get
      {
        return this.calendar;
      }
    }

    /// <summary>Gets the graphics.</summary>
    /// <value>The graphics.</value>
    public Graphics Graphics
    {
      get
      {
        return this.graphics;
      }
    }

    /// <summary>Gets the bounds.</summary>
    /// <value>The bounds.</value>
    public Rectangle Bounds
    {
      get
      {
        return this.bounds;
      }
    }

    /// <summary>Gets the current date.</summary>
    /// <value>The current date.</value>
    public DateTime CurrentDate
    {
      get
      {
        return this.currentDate;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.CalendarPaintEventArgs" /> class.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="currentDate">The current date.</param>
    /// <param name="bounds">The bounds.</param>
    public CalendarPaintEventArgs(vMonthCalendar calendar, Graphics graphics, DateTime currentDate, Rectangle bounds)
    {
      this.graphics = graphics;
      this.currentDate = currentDate;
      this.bounds = bounds;
      this.calendar = calendar;
    }
  }
}
