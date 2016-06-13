// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vAppointment
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  public class vAppointment
  {
    private Control tooltipContainer = new Control();
    private DayPattern dayPattern = new DayPattern(1);
    private MonthPattern monthPattern = new MonthPattern(1, 1);
    private WeekPattern weekPattern = new WeekPattern(1);
    private YearPattern yearPattern = new YearPattern(1, 1);
    private RecurrenceRule rule = RecurrenceRule.None;
    private RecurrenceRange range = new RecurrenceRange();
    private Rectangle cellBounds;
    private object tag;
    private vToolTip toolTip;

    internal Rectangle CellBounds
    {
      get
      {
        return this.cellBounds;
      }
      set
      {
        this.cellBounds = value;
      }
    }

    [Browsable(false)]
    public Control ToolTipContainer
    {
      get
      {
        this.tooltipContainer.Bounds = this.cellBounds;
        return this.tooltipContainer;
      }
    }

    /// <summary>Gets or sets the tag.</summary>
    /// <value>The tag.</value>
    [Description("Gets or sets the tag.")]
    [Category("Behavior")]
    [DefaultValue(null)]
    public object Tag
    {
      get
      {
        return this.tag;
      }
      set
      {
        this.tag = value;
      }
    }

    /// <summary>Gets or sets the v tool tip.</summary>
    /// <value>The v tool tip.</value>
    [Description("Gets or sets the vtooltip associated with the appointment.")]
    [Category("Behavior")]
    [DefaultValue(null)]
    public vToolTip vToolTip
    {
      get
      {
        return this.toolTip;
      }
      set
      {
        this.toolTip = value;
        if (this.toolTip == null)
          return;
        this.toolTip.OwnerControl = this.ToolTipContainer;
        this.toolTip.MouseMove += new MouseEventHandler(this.toolTip_MouseMove);
      }
    }

    /// <summary>Gets or sets the recurrence range.</summary>
    /// <value>The recurrence range.</value>
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the recurrence range.")]
    public RecurrenceRange RecurrenceRange
    {
      get
      {
        return this.range;
      }
      set
      {
        this.range = value;
      }
    }

    /// <summary>Gets or sets the date time.</summary>
    /// <value>The date time.</value>
    [Description("Gets or sets the date of the appointment.")]
    [Category("Behavior")]
    public DateTime Date
    {
      get
      {
        return this.RecurrenceRange.StartDate;
      }
      set
      {
        this.RecurrenceRange.StartDate = value;
      }
    }

    /// <summary>Gets or sets the day pattern.</summary>
    /// <value>The day pattern.</value>
    [Category("Behavior")]
    [Description("Gets or sets the day pattern.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public DayPattern DayPattern
    {
      get
      {
        return this.dayPattern;
      }
      set
      {
        this.dayPattern = value;
      }
    }

    /// <summary>Gets or sets the month pattern.</summary>
    /// <value>The month pattern.</value>
    [Category("Behavior")]
    [Description("Gets or sets the month pattern.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public MonthPattern MonthPattern
    {
      get
      {
        return this.monthPattern;
      }
      set
      {
        this.monthPattern = value;
      }
    }

    /// <summary>Gets or sets the week pattern.</summary>
    /// <value>The week pattern.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets or sets the week pattern.")]
    [Category("Behavior")]
    public WeekPattern WeekPattern
    {
      get
      {
        return this.weekPattern;
      }
      set
      {
        this.weekPattern = value;
      }
    }

    /// <summary>Gets or sets the year pattern.</summary>
    /// <value>The year pattern.</value>
    [Description("Gets or sets the year pattern.")]
    [Category("Behavior")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public YearPattern YearPattern
    {
      get
      {
        return this.yearPattern;
      }
      set
      {
        this.yearPattern = value;
      }
    }

    /// <summary>Gets or sets the recurrence rule.</summary>
    /// <value>The recurrence rule.</value>
    [Description("Gets or sets the recurrence rule.")]
    [Category("Behavior")]
    public RecurrenceRule RecurrenceRule
    {
      get
      {
        return this.rule;
      }
      set
      {
        this.rule = value;
      }
    }

    /// <summary>Occurs when cell is painted.</summary>
    [Category("Action")]
    public event CalendarPaintHandler CellPaint;

    private void toolTip_MouseMove(object sender, MouseEventArgs e)
    {
      this.toolTip.HideTooltip();
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString()
    {
      return this.Date.ToShortDateString();
    }

    /// <summary>
    /// Raises the <see cref="E:CellPaint" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:VIBlend.WinForms.Controls.CalendarPaintEventArgs" /> instance containing the event data.</param>
    protected internal virtual void OnCellPaint(CalendarPaintEventArgs args)
    {
      if (this.CellPaint == null)
        return;
      this.CellPaint((object) this, args);
    }

    /// <summary>Clones this instance.</summary>
    /// <returns></returns>
    public vAppointment Clone()
    {
      return new vAppointment() { RecurrenceRange = this.RecurrenceRange.Clone(), MonthPattern = this.MonthPattern.Clone(), DayPattern = this.DayPattern.Clone(), WeekPattern = this.WeekPattern.Clone(), YearPattern = this.YearPattern.Clone(), vToolTip = this.vToolTip };
    }
  }
}
