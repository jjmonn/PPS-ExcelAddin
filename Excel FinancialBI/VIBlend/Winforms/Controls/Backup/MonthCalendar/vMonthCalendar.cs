// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vMonthCalendar
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using VIBlend.Utilities;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a month calendar control.</summary>
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [DefaultEvent("SelectionChanged")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vMonthCalendar), "ControlIcons.vMonthCalendar.ico")]
  [Description("Represents a month calendar control.")]
  [DefaultProperty("CurrentDate")]
  public class vMonthCalendar : Control, IScrollableControlBase
  {
    private List<vAppointment> vCalendarDays = new List<vAppointment>();
    private bool paintCellDifferentMonthDatesStyle = true;
    private bool paintCellAppointmentsStyle = true;
    private List<vCell> cells = new List<vCell>();
    private PaintHelper helper = new PaintHelper();
    private Color differentMonthDaysColor = Color.LightGray;
    private Color weekendsColor = Color.LightGray;
    private bool showNavigationArrows = true;
    private bool paintColumnHeaderHorizontalLine = true;
    private bool paintRowHeaderHorizontalLine = true;
    private bool showColumnHeader = true;
    private bool enableSelection = true;
    private bool showTodayDate = true;
    private bool showDifferentMonthDates = true;
    private bool enableNavigation = true;
    private bool enableQuickNavigation = true;
    private bool showNavigation = true;
    private bool showFocusRectangle = true;
    private bool enableKeyboardNavigation = true;
    private List<DateTime> selectedDates = new List<DateTime>();
    private DateTime navigationStartDate = new DateTime(1900, 1, 1);
    private DateTime navigationEndDate = new DateTime(2100, 1, 1);
    private ContentAlignment cellAlignment = ContentAlignment.MiddleCenter;
    private ContentAlignment columnHeaderTextAlignment = ContentAlignment.TopCenter;
    private ContentAlignment rowHeaderTextAlignment = ContentAlignment.MiddleCenter;
    private Padding cellMargin = Padding.Empty;
    private bool useThemeTextColor = true;
    private bool paintCellDefaultBorder = true;
    private bool paintCellDefaultFill = true;
    private string titleFormatDayString = "MMMM, yyyy";
    private string titleFormatMonthString = "yyyy";
    private string titleFormatStartYearString = "yyyy";
    private string titleFormatStartCenturyString = "yyyy";
    private string titleFormatEndYearString = "yyyy";
    private string titleFormatEndCenturyString = "yyyy";
    private DateTime currentDate = DateTime.Now.Date;
    private DateTime minSupportedDate = DateTime.MinValue;
    private DateTime maxSupportedDate = DateTime.MaxValue;
    private int titleHeight = 20;
    private Padding cellTableMargin = new Padding(1, 0, 1, 0);
    private DayNameFormat dayNameFormat = DayNameFormat.FirstTwoLetters;
    private Timer timer = new Timer();
    private Timer htimer = new Timer();
    private int duration = 25;
    private Stack<int>[] animValues = new Stack<int>[3];
    private Stack<int> hanimValues = new Stack<int>();
    private DateTime baseDate = new DateTime(2000, 1, 1);
    private List<vAppointment> currentAppointments = new List<vAppointment>();
    private int rowHeaderSize = 20;
    private int columnHeaderSize = 20;
    private Color rowHeaderHorizontalLineColor = Color.LightGray;
    private Color columnHeaderHorizontalLineColor = Color.LightGray;
    private string[] years = new string[12];
    private string[] centuries = new string[12];
    private List<DateTime> ydates = new List<DateTime>();
    private List<DateTime> cdates = new List<DateTime>();
    private VIBLEND_THEME defaultTheme = VIBLEND_THEME.VISTABLUE;
    private DateTime visibleDate = DateTime.MinValue;
    private bool allowAnimations = true;
    protected BackgroundElement backFill;
    protected BackgroundElement titleFill;
    private bool paintCellWeekendsStyle;
    private bool mouseClicked;
    private ControlState state;
    private bool showRowHeader;
    private bool enableMultipleSelection;
    private CultureInfo cultureInfo;
    private vCalendarView calendarView;
    private vCalendarView tmpView;
    private Font cellFont;
    private Font navigationFont;
    private Font columnHeaderFont;
    private Font rowHeaderFont;
    private bool paintTitleBorder;
    private bool paintTitleFill;
    private bool paintBorder;
    private bool paintFill;
    private DayOfWeek dayOfWeek;
    private bool enableToggleSelection;
    private int step;
    private DateTime tmpCurrentDate;
    private int currentYValue;
    private bool upAnimated;
    private int currentXValue;
    private bool rightAnimated;
    private string[] DayNumbers;
    private Color? navigationArrowColor;
    private Color? navigationArrowHighlightColor;
    private Color? cellDefaultTextColor;
    private Point selectionStartPoint;
    private bool selectWithDragging;
    private Rectangle lastShownBounds;
    protected ControlTheme theme;
    private AnimationManager manager;

    /// <summary>Enables or disables toggle selection mode</summary>
    [Description("Enables or disables toggle selection mode")]
    [DefaultValue(false)]
    [Category("Behavior")]
    public bool EnableToggleSelection
    {
      get
      {
        return this.enableToggleSelection;
      }
      set
      {
        this.enableToggleSelection = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the day name format.</summary>
    /// <value>The day name format.</value>
    [Description("Gets or sets the day name format.")]
    [Category("Appearance")]
    public DayNameFormat DayNameFormat
    {
      get
      {
        return this.dayNameFormat;
      }
      set
      {
        this.dayNameFormat = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the font of the vCalendar</summary>
    [Category("Appearance")]
    [Description("Gets or sets calendar's font")]
    public override Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        this.backFill.Font = value;
        this.backFill.HighLightFont = value;
        this.backFill.PressedFont = value;
        this.backFill.DisabledFont = value;
        this.titleFill.Font = value;
        this.titleFill.HighLightFont = value;
        this.titleFill.PressedFont = value;
        this.titleFill.DisabledFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the list of calendar events</summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the list of calendar events.")]
    [Category("Behavior")]
    public List<vAppointment> Appointments
    {
      get
      {
        return this.vCalendarDays;
      }
      set
      {
        this.vCalendarDays = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the height of the title.</summary>
    /// <value>The height of the title.</value>
    [DefaultValue(20)]
    [Description("Gets or sets the height of the title.")]
    [Category("Behavior")]
    public int TitleHeight
    {
      get
      {
        return this.titleHeight;
      }
      set
      {
        this.titleHeight = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the start day of week.</summary>
    /// <value>The start day of week.</value>
    [Category("Behavior")]
    [Description("Gets or sets the first day of the week.")]
    public DayOfWeek StartDayOfWeek
    {
      get
      {
        return this.dayOfWeek;
      }
      set
      {
        this.dayOfWeek = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the title format century string.</summary>
    /// <value>The title format century string.</value>
    [DefaultValue("yyyy")]
    [Category("Behavior")]
    [Description("Gets or sets the start century format string")]
    public string TitleFormatStartCenturyString
    {
      get
      {
        return this.titleFormatStartCenturyString;
      }
      set
      {
        this.titleFormatStartCenturyString = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the title format century string.</summary>
    /// <value>The title format century string.</value>
    [DefaultValue("yyyy")]
    [Category("Behavior")]
    [Description("Gets or sets the end century format string")]
    public string TitleFormatEndCenturyString
    {
      get
      {
        return this.titleFormatEndCenturyString;
      }
      set
      {
        this.titleFormatEndCenturyString = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the title format year string.</summary>
    /// <value>The title format year string.</value>
    [DefaultValue("yyyy")]
    [Category("Behavior")]
    [Description("Gets or sets the title format year string.")]
    public string TitleFormatEndYearString
    {
      get
      {
        return this.titleFormatEndYearString;
      }
      set
      {
        this.titleFormatEndYearString = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the title format year string.</summary>
    /// <value>The title format year string.</value>
    [Category("Behavior")]
    [DefaultValue("yyyy")]
    [Description("Gets or sets the title format year string.")]
    public string TitleFormatStartYearString
    {
      get
      {
        return this.titleFormatStartYearString;
      }
      set
      {
        this.titleFormatStartYearString = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the title format month string.</summary>
    /// <value>The title format month string.</value>
    [DefaultValue("yyyy")]
    [Category("Behavior")]
    [Description("Gets or sets the title format month string.")]
    public string TitleFormatMonthString
    {
      get
      {
        return this.titleFormatMonthString;
      }
      set
      {
        this.titleFormatMonthString = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the title format day string.</summary>
    /// <value>The title format day string.</value>
    [DefaultValue("MMMM, yyyy")]
    [Category("Appearance")]
    [Description("Gets or sets the title format day string.")]
    public string TitleFormatDayString
    {
      get
      {
        return this.titleFormatDayString;
      }
      set
      {
        this.titleFormatDayString = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint cell default border.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to paint cell default border.")]
    public bool PaintCellBorder
    {
      get
      {
        return this.paintCellDefaultBorder;
      }
      set
      {
        this.paintCellDefaultBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint cell default fill.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to paint cell background")]
    public bool PaintCellFill
    {
      get
      {
        return this.paintCellDefaultFill;
      }
      set
      {
        this.paintCellDefaultFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint fill.
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to paint title's background")]
    public bool PaintTitleFill
    {
      get
      {
        return this.paintTitleFill;
      }
      set
      {
        this.paintTitleFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint border.
    /// </summary>
    [Description("Gets or sets a value indicating whether to paint title's border.")]
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool PaintTitleBorder
    {
      get
      {
        return this.paintTitleBorder;
      }
      set
      {
        this.paintTitleBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint fill.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(false)]
    [Description("Gets or sets a value indicating whether to paint calendar's background")]
    public bool PaintFill
    {
      get
      {
        return this.paintFill;
      }
      set
      {
        this.paintFill = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint cell's weeeknd style
    /// </summary>
    [DefaultValue(false)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to paint cell's weekend style")]
    public bool PaintCellWeekendsStyle
    {
      get
      {
        return this.paintCellWeekendsStyle;
      }
      set
      {
        this.paintCellWeekendsStyle = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint cell different month dates style.
    /// </summary>
    [Description("Gets or sets a value indicating whether to paint cell's different month style.")]
    [DefaultValue(true)]
    [Category("Appearance")]
    public bool PaintCellDifferentMonthDatesStyle
    {
      get
      {
        return this.paintCellDifferentMonthDatesStyle;
      }
      set
      {
        this.paintCellDifferentMonthDatesStyle = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value whether to paint cell's appointment style.
    /// </summary>
    [Description("Gets or sets a value indicating whether to paint cell's appointment style")]
    [Category("Appearance")]
    [DefaultValue(true)]
    public bool PaintCellAppointmentsStyle
    {
      get
      {
        return this.paintCellAppointmentsStyle;
      }
      set
      {
        this.paintCellAppointmentsStyle = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint border.
    /// </summary>
    [Description("Gets or sets a value indicating whether to paint calendar's border.")]
    [DefaultValue(false)]
    [Category("Appearance")]
    public bool PaintBorder
    {
      get
      {
        return this.paintBorder;
      }
      set
      {
        this.paintBorder = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use theme's text color
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("Gets or sets a value indicating whether to use theme's text color")]
    public bool UseThemeTextColor
    {
      get
      {
        return this.useThemeTextColor;
      }
      set
      {
        this.useThemeTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the row header font.</summary>
    /// <value>The row header font.</value>
    [Description("Gets or sets row's header font.")]
    [DefaultValue(null)]
    [Category("Appearance")]
    public Font RowHeaderFont
    {
      get
      {
        return this.rowHeaderFont;
      }
      set
      {
        this.rowHeaderFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the column header font.</summary>
    /// <value>The column header font.</value>
    [Description("Gets or sets column's header font.")]
    [DefaultValue(null)]
    [Category("Appearance")]
    public Font ColumnHeaderFont
    {
      get
      {
        return this.columnHeaderFont;
      }
      set
      {
        this.columnHeaderFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the navigation font.</summary>
    /// <value>The navigation font.</value>
    [Description("Gets or sets title's font")]
    [DefaultValue(null)]
    [Category("Appearance")]
    public Font NavigationFont
    {
      get
      {
        return this.navigationFont;
      }
      set
      {
        this.navigationFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the cell font.</summary>
    /// <value>The cell font.</value>
    [Description("Gets or sets cell's Font.")]
    [Category("Appearance")]
    [DefaultValue(null)]
    public Font CellFont
    {
      get
      {
        return this.cellFont;
      }
      set
      {
        this.cellFont = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the row header text alignment.</summary>
    /// <value>The row header text alignment.</value>
    [Description("Gets or sets row header text alignment.")]
    [Category("Appearance")]
    public ContentAlignment RowHeaderTextAlignment
    {
      get
      {
        return this.rowHeaderTextAlignment;
      }
      set
      {
        this.rowHeaderTextAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the column header text alignment.</summary>
    /// <value>The column header text alignment.</value>
    [Description("Gets or sets column header text alignment.")]
    [Category("Appearance")]
    public ContentAlignment ColumnHeaderTextAlignment
    {
      get
      {
        return this.columnHeaderTextAlignment;
      }
      set
      {
        this.columnHeaderTextAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the cell alignment.</summary>
    /// <value>The cell alignment.</value>
    [Category("Appearance")]
    [Description("Gets or sets cell's alignment.")]
    public ContentAlignment CellAlignment
    {
      get
      {
        return this.cellAlignment;
      }
      set
      {
        this.cellAlignment = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets the calendar.</summary>
    /// <value>The calendar.</value>
    [Description("Gets the default Calendar instance.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Behavior")]
    [NotifyParentProperty(true)]
    public Calendar Calendar
    {
      get
      {
        if (this.Culture.DateTimeFormat.Calendar != null)
          return this.Culture.DateTimeFormat.Calendar;
        return DateTimeFormatInfo.CurrentInfo.Calendar;
      }
    }

    /// <summary>Gets or sets the navigation end date.</summary>
    /// <value>The navigation end date.</value>
    [Description("Gets or sets calendar navigation end date.")]
    [Category("Behavior")]
    public DateTime NavigationEndDate
    {
      get
      {
        return this.navigationEndDate;
      }
      set
      {
        if (!(value > this.NavigationStartDate))
          return;
        this.navigationEndDate = value;
        this.CurrentDate = value.Date;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the navigation start date.</summary>
    /// <value>The navigation start date.</value>
    [Category("Behavior")]
    [Description("Gets or sets calendar navigation start date.")]
    public DateTime NavigationStartDate
    {
      get
      {
        return this.navigationStartDate;
      }
      set
      {
        if (!(value < this.NavigationEndDate))
          return;
        this.navigationStartDate = value;
        this.CurrentDate = value.Date;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the selected date.</summary>
    /// <value>The selected date.</value>
    [Browsable(false)]
    [Category("Behavior")]
    public DateTime SelectedDate
    {
      get
      {
        if (this.selectedDates.Count > 0)
          return this.selectedDates[0];
        return DateTime.MinValue;
      }
      set
      {
        if (!(value >= this.NavigationStartDate) || !(value <= this.NavigationEndDate))
          return;
        this.SelectedDates.Remove(this.SelectedDate);
        this.selectedDates.Insert(0, value);
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the selected dates.</summary>
    /// <value>The selected dates.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Description("Gets the selected dates collection")]
    [Category("Behavior")]
    public List<DateTime> SelectedDates
    {
      get
      {
        return this.selectedDates;
      }
    }

    /// <summary>Gets or sets the calendar view.</summary>
    /// <value>The calendar view.</value>
    [Browsable(false)]
    [Category("Behavior")]
    public vCalendarView CalendarView
    {
      get
      {
        return this.calendarView;
      }
      set
      {
        if (this.timer.Enabled)
          return;
        if (this.calendarView != value)
        {
          this.upAnimated = this.calendarView <= value;
          this.Animate();
          this.OnCalendarViewChanged(EventArgs.Empty);
        }
        this.tmpView = value;
      }
    }

    /// <summary>Gets or sets the current date.</summary>
    /// <value>The current date.</value>
    [Category("Behavior")]
    [Description("Gets or sets calendar's current date")]
    public DateTime CurrentDate
    {
      get
      {
        return this.currentDate;
      }
      set
      {
        if (value < this.NavigationStartDate)
        {
          if (this.currentDate == this.NavigationStartDate)
            return;
          value = this.NavigationStartDate;
        }
        if (value > this.NavigationEndDate)
        {
          if (this.currentDate == this.NavigationEndDate)
            return;
          value = this.NavigationEndDate;
        }
        if (!this.mouseClicked)
        {
          this.currentDate = value;
          this.currentAppointments = this.GetAppointmentsInMonth();
        }
        else
        {
          this.rightAnimated = value > this.currentDate;
          this.AnimateHorizontally();
          this.tmpCurrentDate = value;
        }
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to enable keyboard navigation
    /// </summary>
    [Category("Behavior")]
    [Description("Enables or disables keyboard navigation.")]
    [DefaultValue(true)]
    public bool EnableKeyboardNavigation
    {
      get
      {
        return this.enableKeyboardNavigation;
      }
      set
      {
        this.enableKeyboardNavigation = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show focus rectangle.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Shows or hides the cell's focus rectangle")]
    public bool ShowFocusRectangle
    {
      get
      {
        return this.showFocusRectangle;
      }
      set
      {
        this.showFocusRectangle = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show navigation arrows.
    /// </summary>
    [Description("Shows or hides navigation arrows.")]
    [Category("Behavior")]
    [DefaultValue(true)]
    public bool ShowNavigationArrows
    {
      get
      {
        return this.showNavigationArrows;
      }
      set
      {
        this.showNavigationArrows = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show navigation title.
    /// </summary>
    [DefaultValue(true)]
    [Description("Shows or hides navigation title.")]
    [Category("Behavior")]
    public bool ShowNavigation
    {
      get
      {
        return this.showNavigation;
      }
      set
      {
        this.showNavigation = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to enable navigation.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Enables or disables navigation.")]
    public bool EnableQuickNavigation
    {
      get
      {
        return this.enableQuickNavigation;
      }
      set
      {
        this.enableQuickNavigation = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to enable navigation.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Enables or disables navigation.")]
    public bool EnableNavigation
    {
      get
      {
        return this.enableNavigation;
      }
      set
      {
        this.enableNavigation = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show different month dates.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Shows or hides different month dates")]
    public bool ShowDifferentMonthDates
    {
      get
      {
        return this.showDifferentMonthDates;
      }
      set
      {
        this.showDifferentMonthDates = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show today's date.
    /// </summary>
    [DefaultValue(true)]
    [Category("Behavior")]
    [Description("Shows or hides today date cell state.")]
    public bool ShowTodayDate
    {
      get
      {
        return this.showTodayDate;
      }
      set
      {
        this.showTodayDate = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to enable multiple selection.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(false)]
    [Description("Enables or disables calendar's multiple selection.")]
    public bool EnableMultipleSelection
    {
      get
      {
        return this.enableMultipleSelection;
      }
      set
      {
        this.enableMultipleSelection = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to enable selection.
    /// </summary>
    [Category("Behavior")]
    [Description("Enables or disables calendar's selection")]
    [DefaultValue(true)]
    public bool EnableSelection
    {
      get
      {
        return this.enableSelection;
      }
      set
      {
        this.enableSelection = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show row header.
    /// </summary>
    [DefaultValue(false)]
    [Description("Shows or hides row header.")]
    [Category("Behavior")]
    public bool ShowRowHeader
    {
      get
      {
        return this.showRowHeader;
      }
      set
      {
        this.showRowHeader = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show column header.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Shows or hides column header")]
    public bool ShowColumnHeader
    {
      get
      {
        return this.showColumnHeader;
      }
      set
      {
        this.showColumnHeader = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the size of the header.</summary>
    [DefaultValue(20)]
    [Category("Layout")]
    [Description("Gets or sets the size of the header.")]
    public int RowHeaderSize
    {
      get
      {
        return this.rowHeaderSize;
      }
      set
      {
        this.rowHeaderSize = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the size of the header.</summary>
    [DefaultValue(20)]
    [Category("Layout")]
    [Description("Gets or sets the size of the header.")]
    public int ColumnHeaderSize
    {
      get
      {
        return this.columnHeaderSize;
      }
      set
      {
        this.columnHeaderSize = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint row header horizontal line.
    /// </summary>
    [DefaultValue(true)]
    [Category("Appearance")]
    [Description("If true a vertical line is drawn in row's header")]
    public bool PaintRowHeaderVerticalLine
    {
      get
      {
        return this.paintRowHeaderHorizontalLine;
      }
      set
      {
        this.paintRowHeaderHorizontalLine = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the navigation arrow.</summary>
    [Description("Gets or sets the color of the navigation arrow.")]
    [Category("Appearance")]
    public Color? NavigationArrowColor
    {
      get
      {
        return this.navigationArrowColor;
      }
      set
      {
        this.navigationArrowColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the color of the highlighted navigation arrow.
    /// </summary>
    [Category("Appearance")]
    [Description("Gets or sets the color of the highlighted navigation arrow.")]
    public Color? NavigationArrowHighlightColor
    {
      get
      {
        return this.navigationArrowHighlightColor;
      }
      set
      {
        this.navigationArrowHighlightColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the default cells text.</summary>
    [Description("Gets or sets the color of the default cells text.")]
    [Category("Appearance")]
    public Color? CellDefaultTextColor
    {
      get
      {
        return this.cellDefaultTextColor;
      }
      set
      {
        this.cellDefaultTextColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the color of the different month days text.
    /// </summary>
    /// <value>The color of the different month days text.</value>
    [Description("Gets or sets the color of the different month days text.")]
    [Category("Appearance")]
    public Color DifferentMonthDaysTextColor
    {
      get
      {
        return this.differentMonthDaysColor;
      }
      set
      {
        this.differentMonthDaysColor = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the color of the weekends text.</summary>
    /// <value>The color of the weekends text.</value>
    [Description("Gets or sets the text color of the weekends.")]
    [Category("Appearance")]
    public Color WeekendsTextColor
    {
      get
      {
        return this.weekendsColor;
      }
      set
      {
        this.weekendsColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the color of the row heared horizontal line.
    /// </summary>
    /// <value>The color of the row heared horizontal line.</value>
    [DefaultValue(typeof (Color), "LightGray")]
    [Description("Gets or sets the color of the vertical header line.")]
    [Category("Appearance")]
    public Color RowHeaderVerticalLineColor
    {
      get
      {
        return this.rowHeaderHorizontalLineColor;
      }
      set
      {
        this.rowHeaderHorizontalLineColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the color of the column header horizontal line.
    /// </summary>
    /// <value>The color of the column header horizontal line.</value>
    [Category("Appearance")]
    [Description("Gets or sets the color of the column header horizontal line.")]
    [DefaultValue(typeof (Color), "LightGray")]
    public Color ColumnHeaderHorizontalLineColor
    {
      get
      {
        return this.columnHeaderHorizontalLineColor;
      }
      set
      {
        this.columnHeaderHorizontalLineColor = value;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to paint column header horizontal line.
    /// </summary>
    [Category("Appearance")]
    [DefaultValue(true)]
    [Description("If true a horizontal line is drawn in column's header")]
    public bool PaintColumnHeaderHorizontalLine
    {
      get
      {
        return this.paintColumnHeaderHorizontalLine;
      }
      set
      {
        this.paintColumnHeaderHorizontalLine = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the culture.</summary>
    /// <value>The culture.</value>
    [DefaultValue(typeof (CultureInfo), "en-US")]
    [Category("Behavior")]
    [Description("Gets or sets the culture")]
    [TypeConverter(typeof (CultureInfoConverter))]
    public CultureInfo Culture
    {
      get
      {
        if (this.cultureInfo != null)
          return this.cultureInfo;
        return CultureInfo.CurrentCulture;
      }
      set
      {
        if (this.cultureInfo != value)
          this.cultureInfo = value;
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the theme of the control</summary>
    [Category("Appearance")]
    [Description("Gets or sets the theme of the control")]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual ControlTheme Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        if (value == null)
          return;
        this.theme = value.CreateCopy();
        FillStyle fillStyle1 = this.theme.QueryFillStyleSetter("CalendarFillStyle");
        if (fillStyle1 != null)
          this.theme.StylePressed.FillStyle = fillStyle1;
        FillStyle fillStyle2 = this.theme.QueryFillStyleSetter("CalendarNormalFillStyle");
        if (fillStyle2 != null)
          this.theme.StyleNormal.FillStyle = fillStyle2;
        Color color1 = this.theme.QueryColorSetter("CalendarBorder");
        if (color1 != Color.Empty)
          this.theme.StylePressed.BorderColor = color1;
        Color color2 = this.theme.QueryColorSetter("CalendarHighlightBorder");
        if (color2 != Color.Empty)
          this.theme.StyleHighlight.BorderColor = color2;
        Color color3 = this.theme.QueryColorSetter("CalendarHighlightForeColor");
        if (color3 != Color.Empty)
          this.theme.StyleHighlight.TextColor = color3;
        Color color4 = this.theme.QueryColorSetter("CalendarForeColor");
        if (color4 != Color.Empty)
          this.theme.StyleNormal.TextColor = color4;
        this.backFill.LoadTheme(this.theme);
        this.backFill.IsAnimated = true;
        this.titleFill.LoadTheme(this.theme.CreateCopy());
        this.titleFill.IsAnimated = true;
        this.backFill.IsAnimated = false;
        this.backFill.Radius = 2;
        this.titleFill.Radius = 2;
        this.Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the theme of the control using one of the built-in themes
    /// </summary>
    [Category("Appearance")]
    public VIBLEND_THEME VIBlendTheme
    {
      get
      {
        return this.defaultTheme;
      }
      set
      {
        this.defaultTheme = value;
        ControlTheme defaultTheme;
        try
        {
          defaultTheme = ControlTheme.GetDefaultTheme(this.defaultTheme);
        }
        catch (Exception ex)
        {
          Trace.WriteLine(ex.Message);
          return;
        }
        if (defaultTheme == null)
          return;
        this.Theme = defaultTheme;
      }
    }

    [Browsable(false)]
    public DateTime VisibleDate
    {
      get
      {
        return this.visibleDate;
      }
    }

    public DateTime TodaysDate
    {
      get
      {
        return DateTime.Today;
      }
    }

    /// <exclude />
    [Browsable(false)]
    public AnimationManager AnimationManager
    {
      get
      {
        if (this.manager == null)
          this.manager = new AnimationManager((Control) this);
        return this.manager;
      }
      set
      {
        this.manager = value;
      }
    }

    /// <summary>Determines whether to use animations</summary>
    [DefaultValue(true)]
    [Browsable(false)]
    public bool AllowAnimations
    {
      get
      {
        return this.allowAnimations;
      }
      set
      {
        if (this.backFill != null)
          this.backFill.IsAnimated = value;
        this.allowAnimations = value;
      }
    }

    /// <summary>Occurs when user has navigated</summary>
    [Description("Occurs when user has navigated")]
    [Category("Action")]
    public event EventHandler CalendarNavigated;

    /// <summary>Occurs when calendar view is changed.</summary>
    [Description("Occurs when calendar view is changed")]
    [Category("Action")]
    public event EventHandler CalendarViewChanged;

    /// <summary>Occurs when selection is changed.</summary>
    [Description("Occurs when selection is changed")]
    [Category("Action")]
    public event EventHandler<SelectionEventArgs> SelectionChanged;

    /// <summary>Occurs when selection is changing</summary>
    [Category("Action")]
    [Description("Occurs when selection is changing")]
    public event EventHandler<SelectionCancelEventArgs> SelectionChanging;

    /// <summary>Occurs when cell is painted.</summary>
    [Category("Action")]
    [Description("Occurs when cell is painted")]
    public event CalendarPaintHandler CellPaint;

    static vMonthCalendar()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:vCalendar" /> class.
    /// </summary>
    public vMonthCalendar()
    {
      this.cells.Clear();
      for (int index = 0; index < 42; ++index)
        this.cells.Add(new vCell(this, Rectangle.Empty, DateTime.MinValue, false, false, false, false));
      this.DayNumbers = new string[32]
      {
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10",
        "11",
        "12",
        "13",
        "14",
        "15",
        "16",
        "17",
        "18",
        "19",
        "20",
        "21",
        "22",
        "23",
        "24",
        "25",
        "26",
        "27",
        "28",
        "29",
        "30",
        "31"
      };
      this.Size = new Size(250, 200);
      this.SetStyle(ControlStyles.ResizeRedraw, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
      this.BackColor = Color.White;
      this.backFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.titleFill = new BackgroundElement(Rectangle.Empty, (IScrollableControlBase) this);
      this.Theme = ControlTheme.GetDefaultTheme(this.defaultTheme);
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.timer.Interval = 5;
      this.htimer.Tick += new EventHandler(this.htimer_Tick);
      this.htimer.Interval = 5;
      this.animValues[0] = new Stack<int>();
      this.animValues[1] = new Stack<int>();
      this.animValues[2] = new Stack<int>();
    }

    private void AnimateHorizontally()
    {
      this.htimer.Start();
    }

    private void Animate()
    {
      this.timer.Start();
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        this.timer.Dispose();
        this.htimer.Dispose();
      }
      base.Dispose(disposing);
    }

    private void htimer_Tick(object sender, EventArgs e)
    {
      double num = 0.0 + (double) this.step * (double) this.step / (double) this.duration;
      this.step += 2;
      if (this.rightAnimated)
      {
        if ((double) this.Width - num > 0.0)
        {
          this.currentXValue = this.Width - (int) num;
        }
        else
        {
          this.htimer.Stop();
          this.currentXValue = 0;
          this.step = 0;
          this.currentDate = this.tmpCurrentDate;
          this.currentAppointments = this.GetAppointmentsInMonth();
        }
      }
      else if (this.currentXValue < this.Width)
      {
        this.currentXValue = (int) num;
      }
      else
      {
        this.htimer.Stop();
        this.step = 0;
        this.currentXValue = 0;
        this.currentDate = this.tmpCurrentDate;
        this.currentAppointments = this.GetAppointmentsInMonth();
      }
      this.Invalidate();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      double num1 = 0.0 + (double) this.step * (double) this.step / (double) this.duration;
      this.step += 2;
      if (!this.upAnimated)
      {
        if (this.animValues[(int) this.tmpView].Count > 0)
        {
          this.currentYValue = this.animValues[(int) this.tmpView].Pop();
        }
        else
        {
          this.timer.Stop();
          this.currentYValue = 0;
          this.step = 0;
          this.calendarView = this.tmpView;
          this.currentAppointments = this.GetAppointmentsInMonth();
        }
      }
      else if (this.currentYValue < this.Height - this.TitleHeight)
      {
        int num2 = (int) num1;
        this.currentYValue = num2;
        this.animValues[(int) this.CalendarView].Push(num2);
      }
      else
      {
        this.timer.Stop();
        this.step = 0;
        this.currentYValue = 0;
        this.calendarView = this.tmpView;
        this.currentAppointments = this.GetAppointmentsInMonth();
      }
      this.Invalidate();
    }

    protected virtual void OnCalendarNavigated(EventArgs args)
    {
      if (this.CalendarNavigated == null)
        return;
      this.CalendarNavigated((object) this, args);
    }

    protected virtual void OnCalendarViewChanged(EventArgs args)
    {
      if (this.CalendarViewChanged == null)
        return;
      this.CalendarViewChanged((object) this, args);
    }

    protected virtual void OnCellPaint(CalendarPaintEventArgs args)
    {
      if (this.CellPaint == null)
        return;
      this.CellPaint((object) this, args);
    }

    protected virtual void OnSelectionChanged(SelectionEventArgs args)
    {
      if (this.SelectionChanged == null)
        return;
      this.SelectionChanged((object) this, args);
    }

    protected virtual void OnSelectionChanging(SelectionCancelEventArgs args)
    {
      if (this.SelectionChanging == null)
        return;
      this.SelectionChanging((object) this, args);
    }

    private void ResetCellAlignment()
    {
      this.CellAlignment = ContentAlignment.MiddleCenter;
    }

    private bool ShouldSerializeCellAlignment()
    {
      return this.CellAlignment != ContentAlignment.MiddleCenter;
    }

    private void ResetNavigationEndDate()
    {
      this.NavigationEndDate = new DateTime(2100, 1, 1);
    }

    private bool ShouldSerializeNavigationEndDate()
    {
      return this.NavigationEndDate != new DateTime(2100, 1, 1);
    }

    private void ResetNavigationStartDate()
    {
      this.NavigationStartDate = new DateTime(1900, 1, 1);
    }

    private bool ShouldSerializeNavigationStartDate()
    {
      return this.NavigationStartDate != new DateTime(1900, 1, 1);
    }

    /// <summary>Navigates to today.</summary>
    public void NavigateToToday()
    {
      this.CurrentDate = DateTime.Now.Date;
    }

    /// <summary>Navigates to.</summary>
    /// <param name="date">The date.</param>
    public void NavigateTo(DateTime date)
    {
      this.CurrentDate = date.Date;
    }

    /// <summary>Sets the calendar view.</summary>
    /// <param name="view">The view.</param>
    public void SetCalendarView(vCalendarView view)
    {
      this.CalendarView = view;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      if (!this.DesignMode)
        Licensing.LICheck((Control) this);
      base.OnPaint(e);
      int radius = this.backFill.Radius;
      this.backFill.Radius = 0;
      if (this.PaintFill)
      {
        this.backFill.Bounds = this.ClientRectangle;
        this.backFill.DrawStandardFill(e.Graphics, ControlState.Normal, GradientStyles.Solid);
      }
      if (this.PaintBorder)
      {
        this.backFill.Bounds = new Rectangle(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);
        this.backFill.DrawElementBorder(e.Graphics, ControlState.Normal, this.titleFill.BorderColor);
      }
      this.backFill.Radius = radius;
      e.Graphics.Clip = new Region(new Rectangle(0, this.TitleHeight, this.Width, this.Height));
      Rectangle bounds1 = new Rectangle(this.currentXValue, this.TitleHeight + this.currentYValue, this.Width, this.Height - this.TitleHeight);
      Rectangle bounds2 = new Rectangle(this.currentXValue, -this.Height + this.currentYValue, this.Width, this.Height);
      if (this.currentYValue == 0)
        bounds2 = new Rectangle(-this.Width + this.currentXValue, this.TitleHeight + this.currentYValue, this.Width, this.Height - this.TitleHeight);
      if (!this.upAnimated && this.tmpView != this.CalendarView)
      {
        Rectangle rectangle = bounds2;
        bounds2 = bounds1;
        bounds1 = rectangle;
      }
      if (this.currentDate != this.tmpCurrentDate && !this.rightAnimated && this.htimer.Enabled)
      {
        Rectangle rectangle = bounds2;
        bounds2 = bounds1;
        bounds1 = rectangle;
      }
      switch (this.CalendarView)
      {
        case vCalendarView.Day:
          this.RenderDaysView(e.Graphics, bounds1);
          break;
        case vCalendarView.Month:
          this.RenderMonthsView(e.Graphics, bounds1);
          break;
        case vCalendarView.Year:
          this.RenderYearsView(e.Graphics, bounds1);
          break;
        case vCalendarView.Century:
          this.RenderCenturiesView(e.Graphics, bounds1);
          break;
      }
      if ((this.htimer.Enabled || this.timer.Enabled) && (this.tmpView != this.CalendarView || this.currentDate != this.tmpCurrentDate))
      {
        switch (this.tmpView)
        {
          case vCalendarView.Day:
            this.RenderDaysView(e.Graphics, bounds2);
            break;
          case vCalendarView.Month:
            this.RenderMonthsView(e.Graphics, bounds2);
            break;
          case vCalendarView.Year:
            this.RenderYearsView(e.Graphics, bounds2);
            break;
          case vCalendarView.Century:
            this.RenderCenturiesView(e.Graphics, bounds2);
            break;
        }
      }
      e.Graphics.ResetClip();
      this.RenderNavigationTitle(e.Graphics);
    }

    /// <summary>Renders the years view.</summary>
    /// <param name="g">The g.</param>
    /// <param name="bounds">The bounds.</param>
    protected virtual void RenderYearsView(Graphics g, Rectangle bounds)
    {
      Rectangle rectangle = bounds;
      DateTime todaysDate = this.TodaysDate;
      rectangle = new Rectangle(rectangle.X + this.cellTableMargin.Left, rectangle.Y + this.cellTableMargin.Top, rectangle.Width - this.cellTableMargin.Horizontal, rectangle.Height - this.cellTableMargin.Vertical);
      this.years = new string[12];
      int num = this.NavigationEndDate.Year - this.NavigationStartDate.Year;
      DateTime navigationStartDate = this.NavigationStartDate;
      this.ydates.Clear();
      for (int index1 = 0; index1 < num; ++index1)
      {
        DateTime dateTime1 = this.NavigationStartDate.AddYears(index1 + 1);
        if (this.NavigationStartDate.Year <= this.CurrentDate.Year && this.CurrentDate.Year <= dateTime1.Year)
        {
          DateTime dateTime2 = dateTime1.AddYears(-1);
          dateTime1.AddYears(11);
          for (int index2 = 0; index2 < 12; ++index2)
          {
            int year = dateTime2.AddYears(index2).Year;
            if (this.NavigationStartDate.Year <= year && year <= this.NavigationEndDate.Year)
              this.years[index2] = year.ToString();
            this.ydates.Add(dateTime2.AddYears(index2));
          }
          break;
        }
      }
      StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.CellAlignment, false, false);
      Font font = this.CellFont ?? this.Font;
      int x1 = rectangle.X;
      int y = rectangle.Y;
      int width = rectangle.Width / 4;
      int height = rectangle.Height / 3;
      int index3 = 0;
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int x2 = rectangle.X;
        for (int index2 = 0; index2 < 4; ++index2)
        {
          Rectangle r = new Rectangle(x2, y, width, height);
          string s = this.years[index3];
          Rectangle screen = this.RectangleToScreen(r);
          if (!string.IsNullOrEmpty(s) && screen.Contains(Cursor.Position) && !this.timer.Enabled)
          {
            this.state = ControlState.Hover;
            this.backFill.Bounds = r;
            this.backFill.DrawStandardFillWithCustomGradientOffsets(g, this.state, GradientStyles.Linear, 90.0, 1.0, 0.5);
            this.backFill.Bounds = new Rectangle(r.X, r.Y, r.Width - 1, r.Height - 1);
            this.backFill.DrawElementBorder(g, this.state);
            if (Control.MouseButtons == MouseButtons.Left)
            {
              DateTime dateTime = new DateTime(this.ydates[index3].Year, 1, 1);
              if (this.NavigationStartDate <= dateTime && dateTime <= this.NavigationEndDate)
              {
                this.CalendarView = vCalendarView.Month;
                this.currentDate = dateTime;
                break;
              }
            }
          }
          this.state = ControlState.Normal;
          Color color = this.backFill.ForeColor;
          if (this.CellDefaultTextColor.HasValue)
            color = this.CellDefaultTextColor.Value;
          if (screen.Contains(Cursor.Position))
          {
            color = this.backFill.HighlightForeColor;
            this.state = ControlState.Hover;
          }
          using (SolidBrush solidBrush = new SolidBrush(color))
          {
            if (!string.IsNullOrEmpty(s))
            {
              if (index3 == 0 || index3 == 11)
                solidBrush.Color = this.DifferentMonthDaysTextColor;
              g.DrawString(s, font, (Brush) solidBrush, (RectangleF) r, stringFormat);
            }
          }
          x2 += width;
          ++index3;
        }
        y += height;
      }
    }

    protected virtual void RenderDaysView(Graphics g, Rectangle bounds)
    {
      this.minSupportedDate = this.Calendar.MinSupportedDateTime;
      this.maxSupportedDate = this.Calendar.MaxSupportedDateTime;
      DateTime visibleDate = this.GetVisibleDate();
      DateTime time1 = this.FirstCalendarDay(visibleDate);
      int num1 = this.RowHeaderSize;
      if (!this.ShowRowHeader)
        num1 = 0;
      int y = this.ColumnHeaderSize + bounds.Y;
      int num2 = this.ColumnHeaderSize + this.TitleHeight;
      if (!this.ShowColumnHeader)
      {
        y = bounds.Y;
        num2 = this.TitleHeight;
      }
      Rectangle cellTable = new Rectangle(bounds.X + num1, y, bounds.Width - num1, this.Height - num2);
      DateTime time2 = time1.Date;
      bool flag1 = !(this.Calendar is HebrewCalendar);
      DateTime todaysDate = this.TodaysDate;
      int month = this.Calendar.GetMonth(visibleDate);
      int days1 = time1.Subtract(this.baseDate).Days;
      int num3 = 0;
      if (this.IsMinSupportedYearMonth(visibleDate))
      {
        num3 = (int) (this.Calendar.GetDayOfWeek(time1) - this.GetFirstDayOfWeekInt());
        if (num3 < 0)
          num3 += 7;
      }
      DateTime date1 = this.Calendar.AddMonths(this.maxSupportedDate, -1);
      bool flag2 = this.IsMaxSupportedYearMonth(visibleDate) || this.IsTheSameYearMonth(date1, visibleDate);
      cellTable = new Rectangle(cellTable.X + this.cellTableMargin.Left, cellTable.Y + this.cellTableMargin.Top, cellTable.Width - this.cellTableMargin.Horizontal, cellTable.Height - this.cellTableMargin.Vertical);
      int left = cellTable.Left;
      int top1 = cellTable.Top;
      int x = cellTable.Left;
      int top2 = cellTable.Top;
      int width = (int) Math.Ceiling((double) ((float) (cellTable.Width % 7) / 7f)) + cellTable.Width / 7;
      int height = cellTable.Height / 6;
      int cellNumber = 0;
      for (int index1 = 0; index1 < 6; ++index1)
      {
        Rectangle cellBounds = new Rectangle(x, top2, width, height);
        int num4 = days1 * 100 + 7;
        int num5;
        if (num3 > 0)
          num5 = num4 - num3;
        else if (flag2)
        {
          int days2 = this.maxSupportedDate.Subtract(time2).Days;
          if (days2 < 6)
            num5 = num4 - (6 - days2);
        }
        for (int index2 = 0; index2 < 7; ++index2)
        {
          cellBounds = new Rectangle(x, top2, width, height);
          if (cellBounds.Right > cellTable.Right)
          {
            int num6 = cellBounds.Right - cellTable.Right;
            cellBounds = new Rectangle(x, top2, width - num6, height);
          }
          int num7 = (int) this.Calendar.GetDayOfWeek(time2);
          int dayOfMonth = this.Calendar.GetDayOfMonth(time2);
          string str = dayOfMonth > 31 || !flag1 ? time2.ToString("dd", (IFormatProvider) this.Culture) : this.DayNumbers[dayOfMonth];
          bool flag3 = time2.Equals(todaysDate);
          bool flag4 = num7 == 0 || num7 == 6;
          bool flag5 = this.Calendar.GetMonth(time2) != month;
          this.cells[cellNumber].date = time2;
          this.cells[cellNumber].today = flag3;
          this.cells[cellNumber].differentMonth = flag5;
          this.cells[cellNumber].weekend = flag4;
          this.cells[cellNumber].bounds = cellBounds;
          bool flag6 = true;
          if (flag5 && !this.ShowDifferentMonthDates)
            flag6 = false;
          StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.CellAlignment, false, false);
          Font font = this.CellFont ?? this.Font;
          if (flag6)
          {
            Rectangle screen = this.RectangleToScreen(cellBounds);
            ControlState stateType = ControlState.Normal;
            Color color1 = this.backFill.ForeColor;
            if (this.CellDefaultTextColor.HasValue)
              color1 = this.CellDefaultTextColor.Value;
            if (this.SelectedDates.Contains(time2))
            {
              color1 = this.backFill.PressedForeColor;
              Color color2 = this.theme.QueryColorSetter("CalendarCellSelectedForeColor");
              if (!color2.IsEmpty)
                color1 = color2;
              stateType = ControlState.Pressed;
            }
            if (flag3 && this.ShowTodayDate)
            {
              stateType = ControlState.Pressed;
              color1 = this.ForeColor;
              font = new Font(font, FontStyle.Bold);
            }
            else if (flag5 && this.PaintCellDifferentMonthDatesStyle)
              color1 = this.DifferentMonthDaysTextColor;
            else if (flag4 && this.PaintCellWeekendsStyle)
              color1 = this.WeekendsTextColor;
            if (screen.Contains(Cursor.Position))
            {
              color1 = this.backFill.HighlightForeColor;
              stateType = ControlState.Hover;
              time2 = this.DoCellSelection(time2, cellNumber, flag5 && this.PaintCellDifferentMonthDatesStyle);
              if (this.Capture && Control.MouseButtons == MouseButtons.Left && (flag5 && this.PaintCellDifferentMonthDatesStyle) && (this.EnableNavigation && !this.selectWithDragging))
                this.CurrentDate = time2;
            }
            if (!this.UseThemeTextColor)
              color1 = this.ForeColor;
            if (stateType != ControlState.Normal && this.PaintCellFill)
            {
              this.backFill.Bounds = new Rectangle(cellBounds.X + 1, cellBounds.Y + 1, cellBounds.Width - 2, cellBounds.Height - 2);
              this.backFill.DrawStandardFillWithCustomGradientOffsets(g, stateType, GradientStyles.Linear, 90.0, 1.0, 0.5);
            }
            SmoothingMode smoothingMode = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (stateType != ControlState.Normal && this.PaintCellBorder)
            {
              this.backFill.Bounds = new Rectangle(cellBounds.X + 1, cellBounds.Y + 1, cellBounds.Width - 2, cellBounds.Height - 2);
              this.backFill.DrawElementBorder(g, stateType);
            }
            g.SmoothingMode = smoothingMode;
            using (SolidBrush solidBrush = new SolidBrush(color1))
              g.DrawString(str, font, (Brush) solidBrush, (RectangleF) new Rectangle(cellBounds.X, cellBounds.Y + 1, cellBounds.Width, cellBounds.Height - 1), stringFormat);
            if (this.SelectedDate == time2 && this.ShowFocusRectangle)
              ControlPaint.DrawFocusRectangle(g, new Rectangle(cellBounds.X + 3, cellBounds.Y + 3, cellBounds.Width - 5, cellBounds.Height - 5));
          }
          this.RenderAppointments(g, ref time2, ref cellBounds, str, stringFormat, font);
          this.OnCellPaint(new CalendarPaintEventArgs(this, g, time2, cellBounds));
          if (!flag2 || time2.Month != this.maxSupportedDate.Month || time2.Day != this.maxSupportedDate.Day)
          {
            time2 = this.Calendar.AddDays(time2, 1);
            ++days1;
          }
          x += width;
          ++cellNumber;
        }
        top2 += height;
        x = left;
      }
      this.RenderColumnHeader(g, ref bounds, ref cellTable);
      this.RenderRowHeader(g, ref bounds, ref cellTable);
    }

    /// <summary>Renders the appointments.</summary>
    /// <param name="g">The g.</param>
    /// <param name="time">The time.</param>
    /// <param name="cellBounds">The cell bounds.</param>
    /// <param name="str4">The STR4.</param>
    /// <param name="format">The format.</param>
    /// <param name="cellFont">The cell font.</param>
    protected virtual void RenderAppointments(Graphics g, ref DateTime time, ref Rectangle cellBounds, string str4, StringFormat format, Font cellFont)
    {
      if (!this.paintCellAppointmentsStyle)
        return;
      foreach (vAppointment currentAppointment in this.currentAppointments)
      {
        if (currentAppointment.Date == time && (currentAppointment.Date.Month == this.CurrentDate.Month || this.ShowDifferentMonthDates))
        {
          currentAppointment.CellBounds = cellBounds;
          Font font = new Font(cellFont, FontStyle.Bold);
          using (SolidBrush solidBrush = new SolidBrush(this.ForeColor))
          {
            g.DrawString(str4, font, (Brush) solidBrush, (RectangleF) cellBounds, format);
            break;
          }
        }
      }
    }

    public List<vAppointment> GetAppointmentsInMonth()
    {
      DateTime dateTime1 = this.FirstCalendarDay(this.GetVisibleDate());
      DateTime dateTime2 = dateTime1;
      DateTime dateTime3 = dateTime1.AddDays(41.0);
      List<vAppointment> vAppointmentList = new List<vAppointment>();
      foreach (vAppointment appointment in this.Appointments)
      {
        bool flag = false;
        int numberOfOccurences = appointment.RecurrenceRange.NumberOfOccurences;
        if (appointment.RecurrenceRange.EndDate >= dateTime2 && appointment.RecurrenceRange.StartDate < dateTime3)
          flag = true;
        if (flag)
        {
          int num1 = 0;
          DateTime time;
          switch (appointment.RecurrenceRule)
          {
            case RecurrenceRule.Daily:
              int daysToNextOccurence = appointment.DayPattern.DaysToNextOccurence;
              for (time = appointment.Date; time <= dateTime3; time = time.AddDays((double) daysToNextOccurence))
              {
                ++num1;
                if (num1 <= numberOfOccurences)
                {
                  if (time >= dateTime2)
                  {
                    vAppointment vAppointment = appointment.Clone();
                    vAppointment.Date = time;
                    vAppointmentList.Add(vAppointment);
                  }
                }
                else
                  break;
              }
              continue;
            case RecurrenceRule.Weekly:
              int toNextOccurences = appointment.WeekPattern.WeeksToNextOccurences;
              time = appointment.Date;
              if (time >= dateTime2 && time <= dateTime3)
                vAppointmentList.Add(appointment);
              for (; time <= dateTime3; time = time.AddDays((double) (7 * toNextOccurences)))
              {
                ++num1;
                if (num1 <= numberOfOccurences)
                {
                  if (time >= dateTime2)
                  {
                    int num2 = (int) this.Calendar.GetDayOfWeek(time);
                    for (int index = 0; index < 6; ++index)
                    {
                      DayOfWeek dayOfWeek = (DayOfWeek) index;
                      DateTime dateTime4 = time.AddDays((double) (-num2 + index));
                      switch (dayOfWeek)
                      {
                        case DayOfWeek.Sunday:
                          if (appointment.WeekPattern.OccurOnSunday)
                          {
                            vAppointment vAppointment = appointment.Clone();
                            vAppointment.Date = dateTime4;
                            vAppointmentList.Add(vAppointment);
                            break;
                          }
                          break;
                        case DayOfWeek.Monday:
                          if (appointment.WeekPattern.OccurOnMonday)
                          {
                            vAppointment vAppointment = appointment.Clone();
                            vAppointment.Date = dateTime4;
                            vAppointmentList.Add(vAppointment);
                            break;
                          }
                          break;
                        case DayOfWeek.Tuesday:
                          if (appointment.WeekPattern.OccurOnTuesday)
                          {
                            vAppointment vAppointment = appointment.Clone();
                            vAppointment.Date = dateTime4;
                            vAppointmentList.Add(vAppointment);
                            break;
                          }
                          break;
                        case DayOfWeek.Wednesday:
                          if (appointment.WeekPattern.OccurOnWednesday)
                          {
                            vAppointment vAppointment = appointment.Clone();
                            vAppointment.Date = dateTime4;
                            vAppointmentList.Add(vAppointment);
                            break;
                          }
                          break;
                        case DayOfWeek.Thursday:
                          if (appointment.WeekPattern.OccurOnThursday)
                          {
                            vAppointment vAppointment = appointment.Clone();
                            vAppointment.Date = dateTime4;
                            vAppointmentList.Add(vAppointment);
                            break;
                          }
                          break;
                        case DayOfWeek.Friday:
                          if (appointment.WeekPattern.OccurOnFriday)
                          {
                            vAppointment vAppointment = appointment.Clone();
                            vAppointment.Date = dateTime4;
                            vAppointmentList.Add(vAppointment);
                            break;
                          }
                          break;
                        case DayOfWeek.Saturday:
                          if (appointment.WeekPattern.OccurOnSaturday)
                          {
                            vAppointment vAppointment = appointment.Clone();
                            vAppointment.Date = dateTime4;
                            vAppointmentList.Add(vAppointment);
                            break;
                          }
                          break;
                      }
                    }
                  }
                }
                else
                  break;
              }
              continue;
            case RecurrenceRule.Monthly:
              int monthsToNextOccurence = appointment.MonthPattern.MonthsToNextOccurence;
              int occurenceDay = appointment.MonthPattern.OccurenceDay;
              for (time = appointment.Date; time <= dateTime3; time = new DateTime(time.Year, time.Month, occurenceDay))
              {
                ++num1;
                if (num1 <= numberOfOccurences)
                {
                  if (time >= dateTime2)
                  {
                    vAppointment vAppointment = appointment.Clone();
                    vAppointment.Date = time;
                    vAppointmentList.Add(vAppointment);
                  }
                  time = time.AddMonths(monthsToNextOccurence);
                }
                else
                  break;
              }
              continue;
            case RecurrenceRule.Yearly:
              int month = appointment.YearPattern.Month;
              int day = appointment.YearPattern.Day;
              for (time = appointment.Date; time <= dateTime3; time = new DateTime(time.Year, time.Month, day))
              {
                ++num1;
                if (num1 <= numberOfOccurences)
                {
                  if (time >= dateTime2)
                  {
                    vAppointment vAppointment = appointment.Clone();
                    vAppointment.Date = time;
                    vAppointmentList.Add(vAppointment);
                  }
                  time = time.AddYears(1);
                }
                else
                  break;
              }
              continue;
            case RecurrenceRule.None:
              if (appointment.Date >= dateTime2 && appointment.Date <= dateTime3)
              {
                vAppointmentList.Add(appointment);
                continue;
              }
              continue;
            default:
              continue;
          }
        }
      }
      return vAppointmentList;
    }

    private DateTime DoCellSelection(DateTime time, int cellNumber, bool differentMonth)
    {
      if (this.NavigationStartDate <= time && time <= this.NavigationEndDate && (this.EnableSelection && Control.MouseButtons == MouseButtons.Left))
      {
        this.cells[cellNumber].selected = this.SelectedDates.Contains(time);
        if (!this.EnableToggleSelection && !this.EnableMultipleSelection)
        {
          if (this.SelectedDates.Contains(time))
          {
            this.OnSelectionChanged(new SelectionEventArgs(this.SelectedDate, time));
            return time;
          }
          SelectionCancelEventArgs args = new SelectionCancelEventArgs(this.SelectedDate, time);
          this.OnSelectionChanging(args);
          DateTime selectedDate = this.SelectedDate;
          this.SelectedDates.Clear();
          if (args.Cancel)
            return time;
          this.SelectedDates.Insert(0, time);
          this.OnSelectionChanged(new SelectionEventArgs(selectedDate, time));
          return time;
        }
        if (!this.cells[cellNumber].selected)
        {
          DateTime selectedDate = this.SelectedDate;
          SelectionCancelEventArgs args = new SelectionCancelEventArgs(this.SelectedDate, time);
          this.OnSelectionChanging(args);
          if (!this.EnableMultipleSelection)
            this.SelectedDates.Clear();
          if (args.Cancel)
            return time;
          this.SelectedDates.Insert(0, time);
          this.OnSelectionChanged(new SelectionEventArgs(selectedDate, time));
        }
        else
        {
          DateTime selectedDate = this.SelectedDate;
          SelectionCancelEventArgs args = new SelectionCancelEventArgs(this.SelectedDate, time);
          this.OnSelectionChanging(args);
          if (args.Cancel)
            return time;
          this.SelectedDates.Remove(time);
          this.OnSelectionChanged(new SelectionEventArgs(selectedDate, time));
        }
      }
      return time;
    }

    protected virtual void RenderRowHeader(Graphics g, ref Rectangle bounds, ref Rectangle cellTable)
    {
      if (!this.ShowRowHeader)
        return;
      DateTime time = this.FirstCalendarDay(this.GetVisibleDate());
      int num1 = (int) this.Calendar.GetDayOfWeek(time);
      Rectangle rectangle = new Rectangle(bounds.X, cellTable.Y, this.RowHeaderSize, cellTable.Height);
      int num2 = (int) this.StartDayOfWeek;
      int num3 = cellTable.Width / 7;
      int height = cellTable.Height / 6;
      StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.RowHeaderTextAlignment, false, false);
      Font font = this.ColumnHeaderFont ?? this.Font;
      for (int index = 0; index < 6; ++index)
      {
        string @string = this.Calendar.GetWeekOfYear(time, this.Culture.DateTimeFormat.CalendarWeekRule, this.StartDayOfWeek).ToString();
        Rectangle r = new Rectangle(rectangle.X, rectangle.Y + index * height, this.RowHeaderSize, height);
        stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
        Rectangle screen = this.RectangleToScreen(r);
        Color color = this.backFill.ForeColor;
        if (this.CellDefaultTextColor.HasValue)
          color = this.CellDefaultTextColor.Value;
        if (screen.Contains(Cursor.Position))
          color = this.backFill.HighlightForeColor;
        using (SolidBrush solidBrush = new SolidBrush(color))
          g.DrawString(@string, font, (Brush) solidBrush, (RectangleF) r, stringFormat);
        time = this.Calendar.AddWeeks(time, 1);
      }
      if (!this.PaintRowHeaderVerticalLine)
        return;
      int num4 = 0;
      int num5 = 2;
      if (this.ShowColumnHeader && this.PaintColumnHeaderHorizontalLine)
      {
        num4 = this.ColumnHeaderSize;
        num5 = 0;
      }
      using (Pen pen = new Pen(this.RowHeaderVerticalLineColor))
        g.DrawLine(pen, rectangle.X + this.RowHeaderSize - 1, bounds.Y + num5 + num4, rectangle.X + this.RowHeaderSize - 1, bounds.Bottom - 2 * num5);
    }

    protected virtual void RenderColumnHeader(Graphics g, ref Rectangle bounds, ref Rectangle cellTable)
    {
      if (!this.ShowColumnHeader)
        return;
      Rectangle rectangle = new Rectangle(cellTable.X, bounds.Y, cellTable.Width, this.ColumnHeaderSize);
      int firstDayOfWeekInt = this.GetFirstDayOfWeekInt();
      string[] dayNames = this.Culture.DateTimeFormat.DayNames;
      int width = (int) Math.Ceiling((double) ((float) (cellTable.Width % 7) / 7f)) + cellTable.Width / 7;
      int num1 = cellTable.Height / 6;
      StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.ColumnHeaderTextAlignment, false, false);
      Font font = this.ColumnHeaderFont ?? this.Font;
      int num2 = 0;
      for (int index1 = firstDayOfWeekInt; index1 < firstDayOfWeekInt + 7; ++index1)
      {
        Rectangle r = new Rectangle(rectangle.X + num2 * width, rectangle.Y, width, this.ColumnHeaderSize);
        if (r.Right > cellTable.Right)
        {
          int num3 = r.Right - cellTable.Right;
          r = new Rectangle(r.X, r.Y, r.Width - num3, r.Height);
        }
        int index2 = index1;
        if (index2 >= 7)
          index2 -= 7;
        string str = dayNames[index2];
        string s;
        switch (this.dayNameFormat)
        {
          case DayNameFormat.Full:
            s = this.Culture.DateTimeFormat.GetDayName((DayOfWeek) index2);
            break;
          case DayNameFormat.FirstLetter:
            s = this.Culture.DateTimeFormat.GetDayName((DayOfWeek) index2).Substring(0, 1);
            break;
          case DayNameFormat.FirstTwoLetters:
            s = this.Culture.DateTimeFormat.GetDayName((DayOfWeek) index2).Substring(0, 2);
            break;
          case DayNameFormat.Shortest:
            s = this.Culture.DateTimeFormat.GetShortestDayName((DayOfWeek) index2);
            break;
          default:
            s = this.Culture.DateTimeFormat.GetAbbreviatedDayName((DayOfWeek) index2);
            break;
        }
        stringFormat.FormatFlags |= StringFormatFlags.NoWrap;
        Rectangle screen = this.RectangleToScreen(r);
        Color color1 = this.backFill.ForeColor;
        if (this.CellDefaultTextColor.HasValue)
          color1 = this.CellDefaultTextColor.Value;
        if (screen.Contains(Cursor.Position))
        {
          color1 = this.backFill.HighlightForeColor;
          Color color2 = Color.Empty;
          Color color3 = this.theme.QueryColorSetter("CalendarHeaderHighlightForeColor");
          if (!color3.IsEmpty)
            color1 = color3;
        }
        using (SolidBrush solidBrush = new SolidBrush(color1))
          g.DrawString(s, font, (Brush) solidBrush, (RectangleF) r, stringFormat);
        ++num2;
      }
      if (!this.PaintColumnHeaderHorizontalLine)
        return;
      int num4 = 0;
      int num5 = 2;
      if (this.ShowRowHeader && this.PaintRowHeaderVerticalLine)
      {
        num4 = this.RowHeaderSize;
        num5 = 0;
      }
      using (Pen pen = new Pen(this.ColumnHeaderHorizontalLineColor))
        g.DrawLine(pen, bounds.X + num5 + num4, rectangle.Y + this.ColumnHeaderSize - 1, bounds.Right - 2 * num5, rectangle.Y + this.ColumnHeaderSize - 1);
    }

    protected virtual void RenderMonthsView(Graphics g, Rectangle bounds)
    {
      Rectangle rectangle = bounds;
      DateTime todaysDate = this.TodaysDate;
      rectangle = new Rectangle(rectangle.X + this.cellTableMargin.Left, rectangle.Y + this.cellTableMargin.Top, rectangle.Width - this.cellTableMargin.Horizontal, rectangle.Height - this.cellTableMargin.Vertical);
      string[] strArray = new string[12];
      for (int index = 0; index < 12; ++index)
        strArray[index] = this.Culture.DateTimeFormat.GetAbbreviatedMonthName(index + 1);
      StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.CellAlignment, false, false);
      Font font = this.CellFont ?? this.Font;
      int x1 = rectangle.X;
      int y = rectangle.Y;
      int width = rectangle.Width / 4;
      int height = rectangle.Height / 3;
      int index1 = 0;
      for (int index2 = 0; index2 < 3; ++index2)
      {
        int x2 = rectangle.X;
        for (int index3 = 0; index3 < 4; ++index3)
        {
          Rectangle r = new Rectangle(x2, y, width, height);
          string s = strArray[index1];
          Rectangle screen = this.RectangleToScreen(r);
          ControlState controlState = ControlState.Normal;
          Color color = this.backFill.ForeColor;
          if (this.CellDefaultTextColor.HasValue)
            color = this.CellDefaultTextColor.Value;
          if (!string.IsNullOrEmpty(s) && screen.Contains(Cursor.Position) && !this.timer.Enabled)
          {
            ControlState stateType = ControlState.Hover;
            this.backFill.Bounds = r;
            this.backFill.DrawStandardFillWithCustomGradientOffsets(g, stateType, GradientStyles.Linear, 90.0, 1.0, 0.5);
            this.backFill.Bounds = new Rectangle(r.X, r.Y, r.Width - 1, r.Height - 1);
            this.backFill.DrawElementBorder(g, stateType);
            if (Control.MouseButtons == MouseButtons.Left)
            {
              int num = index1;
              if (this.Calendar.GetDaysInMonth(this.CurrentDate.Year, num + 1) >= 1)
              {
                DateTime dateTime = new DateTime(this.CurrentDate.Year, num + 1, 1);
                if (this.NavigationStartDate <= dateTime && dateTime <= this.NavigationEndDate)
                {
                  this.CalendarView = vCalendarView.Day;
                  this.currentDate = dateTime;
                  break;
                }
              }
            }
          }
          if (screen.Contains(Cursor.Position))
          {
            color = this.backFill.HighlightForeColor;
            controlState = ControlState.Hover;
          }
          using (SolidBrush solidBrush = new SolidBrush(color))
          {
            if (!string.IsNullOrEmpty(s))
              g.DrawString(s, font, (Brush) solidBrush, (RectangleF) r, stringFormat);
          }
          x2 += width;
          ++index1;
        }
        y += height;
      }
    }

    protected virtual void RenderCenturiesView(Graphics g, Rectangle bounds)
    {
      Rectangle rectangle = bounds;
      DateTime todaysDate = this.TodaysDate;
      rectangle = new Rectangle(rectangle.X + this.cellTableMargin.Left, rectangle.Y + this.cellTableMargin.Top, rectangle.Width - this.cellTableMargin.Horizontal, rectangle.Height - this.cellTableMargin.Vertical);
      this.cdates.Clear();
      this.centuries = new string[12];
      int num1 = this.NavigationEndDate.Year - this.NavigationStartDate.Year;
      DateTime navigationStartDate = this.NavigationStartDate;
      this.cdates.Clear();
      int num2 = 0;
      while (num2 < num1)
      {
        DateTime dateTime1 = this.NavigationStartDate.AddYears(num2 + 120);
        if (this.NavigationStartDate <= this.CurrentDate && this.CurrentDate <= dateTime1)
        {
          DateTime dateTime2 = dateTime1.AddYears(-130);
          if (dateTime2 < this.NavigationStartDate)
            dateTime2 = this.NavigationStartDate;
          for (int index = 0; index < 12; ++index)
          {
            if (dateTime2.Year >= this.NavigationStartDate.Year && dateTime2.AddYears(index * 10).Year <= this.NavigationEndDate.Year)
              this.centuries[index] = dateTime2.AddYears(index * 10).Year.ToString() + "-" + dateTime2.AddYears(index * 10 + 9).Year.ToString();
            this.cdates.Add(dateTime2.AddYears(index * 10));
          }
          break;
        }
        num2 += 120;
      }
      StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, this.CellAlignment, false, false);
      Font font = this.CellFont ?? this.Font;
      int x1 = rectangle.X;
      int y = rectangle.Y;
      int width = rectangle.Width / 4;
      int height = rectangle.Height / 3;
      int index1 = 0;
      for (int index2 = 0; index2 < 3; ++index2)
      {
        int x2 = rectangle.X;
        for (int index3 = 0; index3 < 4; ++index3)
        {
          Rectangle r = new Rectangle(x2, y, width, height);
          string s = this.centuries[index1];
          Rectangle screen = this.RectangleToScreen(r);
          ControlState controlState = ControlState.Normal;
          Color color = this.backFill.ForeColor;
          if (this.CellDefaultTextColor.HasValue)
            color = this.CellDefaultTextColor.Value;
          if (!string.IsNullOrEmpty(s) && screen.Contains(Cursor.Position) && !this.timer.Enabled)
          {
            ControlState stateType = ControlState.Hover;
            this.backFill.Bounds = r;
            this.backFill.DrawStandardFillWithCustomGradientOffsets(g, stateType, GradientStyles.Linear, 90.0, 1.0, 0.5);
            this.backFill.Bounds = new Rectangle(r.X, r.Y, r.Width - 1, r.Height - 1);
            this.backFill.DrawElementBorder(g, stateType);
            if (Control.MouseButtons == MouseButtons.Left)
            {
              DateTime dateTime = new DateTime(this.cdates[index1].Year, 1, 1);
              if (this.NavigationStartDate.Year <= dateTime.Year && dateTime.Year <= this.NavigationEndDate.Year)
              {
                this.CalendarView = vCalendarView.Year;
                this.currentDate = dateTime;
                break;
              }
            }
          }
          if (screen.Contains(Cursor.Position))
          {
            color = this.backFill.HighlightForeColor;
            controlState = ControlState.Hover;
          }
          using (SolidBrush solidBrush = new SolidBrush(color))
          {
            if (!string.IsNullOrEmpty(s))
            {
              if (index1 == 0 || index1 == 11)
                solidBrush.Color = this.DifferentMonthDaysTextColor;
              g.DrawString(s, font, (Brush) solidBrush, (RectangleF) r, stringFormat);
            }
          }
          x2 += width;
          ++index1;
        }
        y += height;
      }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (!this.Focused || !this.EnableKeyboardNavigation || !this.EnableSelection)
        return base.ProcessCmdKey(ref msg, keyData);
      DateTime selectedDate = this.SelectedDate;
      if (this.SelectedDate.Month == this.CurrentDate.Month)
      {
        switch (keyData)
        {
          case Keys.Left:
            if (this.SelectedDate > this.NavigationStartDate && this.SelectedDate <= this.NavigationEndDate)
            {
              this.SelectedDate = this.SelectedDate.AddDays(-1.0);
              this.CurrentDate = this.SelectedDate;
              break;
            }
            break;
          case Keys.Up:
            if (this.SelectedDate >= this.NavigationStartDate && this.SelectedDate <= this.NavigationEndDate)
            {
              this.SelectedDate = this.SelectedDate.AddDays(-7.0);
              this.CurrentDate = this.SelectedDate;
              break;
            }
            break;
          case Keys.Right:
            if (this.SelectedDate > this.NavigationStartDate && this.SelectedDate < this.NavigationEndDate)
            {
              this.SelectedDate = this.SelectedDate.AddDays(1.0);
              this.CurrentDate = this.SelectedDate;
              break;
            }
            break;
          case Keys.Down:
            if (this.SelectedDate >= this.NavigationStartDate && this.SelectedDate <= this.NavigationEndDate)
            {
              this.SelectedDate = this.SelectedDate.AddDays(7.0);
              this.CurrentDate = this.SelectedDate;
              break;
            }
            break;
        }
      }
      this.Invalidate();
      return true;
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      base.OnMouseLeave(e);
      this.lastShownBounds = Rectangle.Empty;
      this.Invalidate();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.Focus();
      this.selectionStartPoint = e.Location;
      if (new Rectangle(this.ClientRectangle.X + 22, this.ClientRectangle.Y, this.ClientRectangle.Width - 44, this.TitleHeight).Contains(e.Location))
      {
        if (!this.EnableQuickNavigation || !this.EnableNavigation || this.timer.Enabled)
          return;
        this.SwitchViews(e);
      }
      else
      {
        if (e.Button != MouseButtons.Left)
          return;
        this.mouseClicked = true;
        int x = 10;
        Size size = new Size(3, 5);
        Rectangle rectangle1 = new Rectangle(x, this.TitleHeight / 2 - size.Height / 2, size.Width, size.Height);
        Rectangle rectangle2 = new Rectangle(this.Width - x - size.Width, this.TitleHeight / 2 - size.Height / 2, size.Width, size.Height);
        int num = 6;
        this.DoNavigation(this.RectangleToScreen(new Rectangle(rectangle1.X - num, rectangle1.Y - num, rectangle1.Width + 2 * num, rectangle1.Height + 2 * num)).Contains(Cursor.Position), this.RectangleToScreen(new Rectangle(rectangle2.X - num, rectangle2.Y - num, rectangle2.Width + 2 * num, rectangle2.Height + 2 * num)).Contains(Cursor.Position));
        this.Refresh();
      }
    }

    private void SwitchViews(MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left)
      {
        if (this.CalendarView == vCalendarView.Day)
          this.CalendarView = vCalendarView.Month;
        else if (this.CalendarView == vCalendarView.Month)
        {
          this.CalendarView = vCalendarView.Year;
        }
        else
        {
          if (this.CalendarView != vCalendarView.Year)
            return;
          this.CalendarView = vCalendarView.Century;
        }
      }
      else if (this.CalendarView == vCalendarView.Century)
        this.CalendarView = vCalendarView.Year;
      else if (this.CalendarView == vCalendarView.Year)
      {
        this.CalendarView = vCalendarView.Month;
      }
      else
      {
        if (this.CalendarView != vCalendarView.Month)
          return;
        this.CalendarView = vCalendarView.Day;
      }
    }

    private void DoNavigation(bool mouseOverLeftArrow, bool mouseOverRightArrow)
    {
      if (!this.EnableNavigation || this.htimer.Enabled)
        return;
      switch (this.CalendarView)
      {
        case vCalendarView.Day:
          if (mouseOverLeftArrow)
          {
            this.CurrentDate = this.CurrentDate.AddMonths(-1);
            break;
          }
          if (mouseOverRightArrow)
          {
            this.CurrentDate = this.CurrentDate.AddMonths(1);
            break;
          }
          break;
        case vCalendarView.Month:
          if (mouseOverLeftArrow)
          {
            this.CurrentDate = this.CurrentDate.AddMonths(-12);
            break;
          }
          if (mouseOverRightArrow)
          {
            this.CurrentDate = this.CurrentDate.AddMonths(12);
            break;
          }
          break;
        case vCalendarView.Year:
          if (mouseOverLeftArrow)
          {
            if (!this.ydates.Contains(this.NavigationStartDate))
            {
              this.CurrentDate = this.CurrentDate.AddYears(-10);
              break;
            }
            break;
          }
          if (mouseOverRightArrow && !this.ydates.Contains(this.NavigationEndDate))
          {
            this.CurrentDate = this.CurrentDate.AddYears(10);
            break;
          }
          break;
        case vCalendarView.Century:
          if (mouseOverLeftArrow)
          {
            if (this.CurrentDate == this.NavigationStartDate)
              return;
            if (!this.cdates.Contains(this.NavigationStartDate))
            {
              this.CurrentDate = this.CurrentDate.AddYears(-100);
              break;
            }
            break;
          }
          if (mouseOverRightArrow)
          {
            if (this.CurrentDate == this.NavigationEndDate)
              return;
            if (!this.cdates.Contains(this.NavigationEndDate))
            {
              this.CurrentDate = this.CurrentDate.AddYears(100);
              break;
            }
            break;
          }
          break;
      }
      this.OnCalendarNavigated(EventArgs.Empty);
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      this.selectWithDragging = false;
      this.Capture = false;
      this.state = ControlState.Normal;
      this.mouseClicked = false;
      base.OnMouseUp(e);
      this.Invalidate();
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      if (this.timer.Enabled || this.htimer.Enabled)
        return;
      if (e.Button == MouseButtons.None)
        this.Invalidate();
      if (!this.Capture && e.Button == MouseButtons.Left)
        this.Capture = true;
      if (e.Button == MouseButtons.Left && this.Capture && (this.EnableMultipleSelection || this.EnableSelection))
      {
        this.Invalidate();
        this.selectWithDragging = true;
        Point location = e.Location;
        if (this.CalendarView != vCalendarView.Day)
          return;
        vCell vCell1 = (vCell) null;
        vCell vCell2 = (vCell) null;
        for (int index = 0; index < this.cells.Count; ++index)
        {
          if (this.cells[index].bounds.Contains(this.selectionStartPoint))
            vCell1 = this.cells[index];
          else if (this.cells[index].bounds.Contains(location))
            vCell2 = this.cells[index];
        }
        if (vCell1 == null || vCell2 == null)
          return;
        DateTime dateTime1 = vCell1.date;
        DateTime dateTime2 = vCell2.date;
        if (dateTime1 > dateTime2)
        {
          DateTime dateTime3 = dateTime2;
          TimeSpan timeSpan = dateTime1 - dateTime3;
          if (timeSpan.TotalDays > 7.0)
          {
            int num = (int) timeSpan.TotalDays - 7;
            dateTime3 = dateTime3.AddDays((double) num);
          }
          DateTime dateTime4 = dateTime1;
          dateTime1 = dateTime3.AddDays(1.0);
          dateTime2 = dateTime4;
        }
        this.SelectedDates.Clear();
        int num1 = 0;
        while (dateTime1 <= dateTime2)
        {
          ++num1;
          this.SelectedDates.Add(dateTime1);
          dateTime1 = dateTime1.AddDays(1.0);
          if (num1 == 7)
            break;
        }
      }
      else
      {
        foreach (vAppointment currentAppointment in this.currentAppointments)
        {
          if (currentAppointment.vToolTip != null && (currentAppointment.CellBounds.Contains(e.Location) && currentAppointment.CellBounds != this.lastShownBounds) && (currentAppointment.Date.Month == this.CurrentDate.Month || this.ShowDifferentMonthDates))
          {
            this.lastShownBounds = currentAppointment.CellBounds;
            if (!currentAppointment.vToolTip.IsShown)
            {
              currentAppointment.vToolTip.ShowBelowOwner = false;
              Point screen = this.PointToScreen(Point.Empty);
              Point point = new Point(screen.X + currentAppointment.CellBounds.Right, screen.Y + currentAppointment.CellBounds.Bottom);
              currentAppointment.vToolTip.ShowTooltip(point, (Control) this);
            }
          }
        }
      }
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any. This method should not be overridden.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.
    /// </returns>
    public override string ToString()
    {
      return this.CurrentDate.ToShortDateString();
    }

    /// <summary>Renders the navigation title.</summary>
    /// <param name="g">Graphics</param>
    protected virtual void RenderNavigationTitle(Graphics g)
    {
      if (!this.ShowNavigation)
        return;
      Color foreColor = this.ForeColor;
      Rectangle rectangle = new Rectangle(0, 0, this.Width, this.TitleHeight);
      if (this.PaintTitleFill)
      {
        this.titleFill.Bounds = new Rectangle(-1, -1, this.Width + 2, this.TitleHeight + 1);
        this.titleFill.Radius = 0;
        this.titleFill.DrawStandardFill(g, ControlState.Normal, GradientStyles.Solid);
      }
      if (this.PaintTitleBorder)
      {
        this.titleFill.Radius = 0;
        this.titleFill.Bounds = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
        this.titleFill.DrawElementBorder(g, ControlState.Normal, this.titleFill.BorderColor);
      }
      int x = 10;
      Size size = new Size(3, 5);
      Rectangle bounds1 = new Rectangle(x, this.TitleHeight / 2 - size.Height / 2, size.Width, size.Height);
      Rectangle bounds2 = new Rectangle(this.Width - x - size.Width, this.TitleHeight / 2 - size.Height / 2, size.Width, size.Height);
      Rectangle r1 = new Rectangle(rectangle.X + x + size.Width + 6, rectangle.Y, rectangle.Width - 2 * x - 2 * size.Width - 12, rectangle.Height);
      int num = 6;
      Rectangle r2 = new Rectangle(bounds1.X - num, bounds1.Y - num, bounds1.Width + 2 * num, bounds1.Height + 2 * num);
      Rectangle r3 = new Rectangle(bounds2.X - num, bounds2.Y - num, bounds2.Width + 2 * num, bounds2.Height + 2 * num);
      bool flag1 = this.RectangleToScreen(r1).Contains(Cursor.Position);
      bool flag2 = this.RectangleToScreen(r2).Contains(Cursor.Position);
      bool flag3 = this.RectangleToScreen(r3).Contains(Cursor.Position);
      SmoothingMode smoothingMode = g.SmoothingMode;
      g.SmoothingMode = SmoothingMode.AntiAlias;
      if (this.ShowNavigationArrows)
      {
        Color color1 = this.titleFill.ForeColor;
        if (this.NavigationArrowColor.HasValue)
          color1 = this.NavigationArrowColor.Value;
        if (flag2)
        {
          color1 = this.titleFill.HighlightForeColor;
          Color color2 = this.theme.QueryColorSetter("CalendarHeaderHighlightForeColor");
          if (!color2.IsEmpty)
            color1 = color2;
          if (this.NavigationArrowHighlightColor.HasValue)
            color1 = this.NavigationArrowHighlightColor.Value;
          bounds1 = new Rectangle(x, this.TitleHeight / 2 - size.Height / 2, size.Width + 1, size.Height + 2);
        }
        this.helper.DrawArrowFigure(g, color1, bounds1, ArrowDirection.Left);
        Color color3 = this.titleFill.ForeColor;
        if (this.NavigationArrowColor.HasValue)
          color3 = this.NavigationArrowColor.Value;
        if (flag3)
        {
          color3 = this.titleFill.HighlightForeColor;
          Color color2 = this.theme.QueryColorSetter("CalendarHeaderHighlightForeColor");
          if (!color2.IsEmpty)
            color3 = color2;
          if (this.NavigationArrowHighlightColor.HasValue)
            color3 = this.NavigationArrowHighlightColor.Value;
          bounds2 = new Rectangle(this.Width - x - size.Width, this.TitleHeight / 2 - size.Height / 2, size.Width + 1, size.Height + 2);
        }
        this.helper.DrawArrowFigure(g, color3, bounds2, ArrowDirection.Right);
      }
      Color color4 = this.titleFill.ForeColor;
      if (this.CellDefaultTextColor.HasValue)
        color4 = this.CellDefaultTextColor.Value;
      StringFormat stringFormat = ImageAndTextHelper.GetStringFormat((Control) this, ContentAlignment.MiddleCenter, false, false);
      Font font = this.CellFont ?? this.Font;
      string s1 = this.CurrentDate.ToString(this.TitleFormatDayString, (IFormatProvider) this.Culture);
      switch (this.CalendarView)
      {
        case vCalendarView.Day:
          string titleFormatDayString = this.TitleFormatDayString;
          break;
        case vCalendarView.Month:
          s1 = this.CurrentDate.ToString(this.TitleFormatMonthString, (IFormatProvider) this.Culture);
          break;
        case vCalendarView.Year:
          string s2 = this.years[0];
          string s3 = this.years[11];
          if (string.IsNullOrEmpty(s2))
            s2 = this.years[1];
          if (string.IsNullOrEmpty(s3))
          {
            for (int index = 0; index < 10; ++index)
            {
              if (!string.IsNullOrEmpty(this.years[index]))
                s3 = this.years[index];
            }
          }
          if (s2 != null && s3 != null)
          {
            s1 = new DateTime(int.Parse(s2, (IFormatProvider) CultureInfo.InvariantCulture), 1, 1).ToString(this.TitleFormatStartYearString, (IFormatProvider) this.Culture) + " - " + new DateTime(int.Parse(s3, (IFormatProvider) CultureInfo.InvariantCulture), 1, 1).ToString(this.TitleFormatEndYearString, (IFormatProvider) this.Culture);
            break;
          }
          break;
        case vCalendarView.Century:
          string str1 = this.centuries[0];
          string str2 = this.centuries[11];
          if (string.IsNullOrEmpty(str1))
            str1 = this.centuries[1];
          if (string.IsNullOrEmpty(str2))
            str2 = this.centuries[10];
          if (!string.IsNullOrEmpty(str1))
          {
            if (!string.IsNullOrEmpty(str2))
            {
              string s4 = str1.Substring(0, 4);
              string s5 = str2.Substring(5);
              int result1;
              int result2;
              s1 = !int.TryParse(s4, out result1) || !int.TryParse(s4, out result2) ? this.CurrentDate.ToString(this.TitleFormatMonthString, (IFormatProvider) this.Culture) : new DateTime(int.Parse(s4, (IFormatProvider) CultureInfo.InvariantCulture), 1, 1).ToString(this.TitleFormatStartCenturyString, (IFormatProvider) this.Culture) + " - " + new DateTime(int.Parse(s5, (IFormatProvider) CultureInfo.InvariantCulture), 1, 1).ToString(this.TitleFormatEndCenturyString, (IFormatProvider) this.Culture);
              break;
            }
            s1 = this.CurrentDate.ToString(this.TitleFormatMonthString, (IFormatProvider) this.Culture);
            break;
          }
          break;
      }
      if (flag1 && !flag2 && !flag3)
      {
        color4 = this.titleFill.HighlightForeColor;
        Color color1 = this.theme.QueryColorSetter("CalendarHeaderHighlightForeColor");
        if (!color1.IsEmpty)
          color4 = color1;
      }
      if (!this.UseThemeTextColor)
        color4 = this.ForeColor;
      using (SolidBrush solidBrush = new SolidBrush(color4))
        g.DrawString(s1, font, (Brush) solidBrush, (RectangleF) r1, stringFormat);
      g.SmoothingMode = smoothingMode;
    }

    private Point[] GetTrianglePoints(ArrowDirection direction, Rectangle bounds)
    {
      Point[] points = new Point[3];
      int xtriangleOffset = vMonthCalendar.GetXTriangleOffset(ref bounds);
      int y1;
      int heightOffset;
      bounds = vMonthCalendar.GetYTriangleOffset(bounds, xtriangleOffset, out y1, out heightOffset);
      int y2 = (int) Math.Ceiling((double) (heightOffset / 2) * 2.5);
      switch (direction)
      {
        case ArrowDirection.Left:
          points[0] = new Point(heightOffset, 0);
          points[1] = new Point(heightOffset, y2);
          points[2] = new Point(0, y2 / 2);
          break;
        case ArrowDirection.Up:
          points[0] = new Point(0, y1);
          points[1] = new Point(xtriangleOffset, y1);
          points[2] = new Point(xtriangleOffset / 2, 0);
          break;
        case ArrowDirection.Right:
          points[0] = new Point(0, 0);
          points[1] = new Point(0, y2);
          points[2] = new Point(heightOffset, y2 / 2);
          break;
        case ArrowDirection.Down:
          points[0] = new Point(0, 0);
          points[1] = new Point(xtriangleOffset, 0);
          points[2] = new Point(xtriangleOffset / 2, y1);
          break;
      }
      switch (direction)
      {
        case ArrowDirection.Left:
        case ArrowDirection.Right:
          this.GetTrianglePointsOffset(points, bounds.X + (bounds.Width - heightOffset) / 2, bounds.Y + (bounds.Height - y2) / 2);
          return points;
        case ArrowDirection.Up:
        case ArrowDirection.Down:
          this.GetTrianglePointsOffset(points, bounds.X + (bounds.Width - y1) / 2, bounds.Y + (bounds.Height - xtriangleOffset) / 2);
          return points;
        default:
          return points;
      }
    }

    private static Rectangle GetYTriangleOffset(Rectangle bounds, int x, out int y, out int heightOffset)
    {
      y = (int) Math.Ceiling((double) (x / 2) * 2.5);
      heightOffset = (int) ((double) bounds.Height * 0.8);
      if (heightOffset % 2 == 0)
        ++heightOffset;
      return bounds;
    }

    private static int GetXTriangleOffset(ref Rectangle bounds)
    {
      int num = (int) ((double) bounds.Width * 0.8);
      if (num % 2 == 1)
        ++num;
      return num;
    }

    private void GetTrianglePointsOffset(Point[] points, int xOffset, int yOffset)
    {
      for (int index = 0; index < points.Length; ++index)
      {
        points[index].X += xOffset;
        points[index].Y += yOffset;
      }
    }

    private string GetMonthName(int m, bool bFull)
    {
      if (bFull)
        return DateTimeFormatInfo.CurrentInfo.GetMonthName(m);
      return DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(m);
    }

    private bool IsMaxSupportedYearMonth(DateTime date)
    {
      return this.IsTheSameYearMonth(this.maxSupportedDate, date);
    }

    private bool IsMinSupportedYearMonth(DateTime date)
    {
      return this.IsTheSameYearMonth(this.minSupportedDate, date);
    }

    private bool IsTheSameYearMonth(DateTime date1, DateTime date2)
    {
      if (DateTimeFormatInfo.CurrentInfo.Calendar.GetEra(date1) == DateTimeFormatInfo.CurrentInfo.Calendar.GetEra(date2) && DateTimeFormatInfo.CurrentInfo.Calendar.GetYear(date1) == DateTimeFormatInfo.CurrentInfo.Calendar.GetYear(date2))
        return DateTimeFormatInfo.CurrentInfo.Calendar.GetMonth(date1) == DateTimeFormatInfo.CurrentInfo.Calendar.GetMonth(date2);
      return false;
    }

    private int GetFirstDayOfWeekInt()
    {
      if (this.StartDayOfWeek != DayOfWeek.Sunday)
        return (int) this.StartDayOfWeek;
      return (int) DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek;
    }

    /// <summary>Specifies the day to display as the first day of the week on the <see cref="T:System.Web.UI.WebControls.Calendar"></see> control.</summary>
    public DateTime GetVisibleDate()
    {
      DateTime dateTime = this.CurrentDate;
      if (dateTime.Equals(DateTime.MinValue))
        dateTime = this.TodaysDate;
      if (this.IsMinSupportedYearMonth(dateTime))
        return this.minSupportedDate;
      return DateTimeFormatInfo.CurrentInfo.Calendar.AddDays(dateTime, -(DateTimeFormatInfo.CurrentInfo.Calendar.GetDayOfMonth(dateTime) - 1));
    }

    private DateTime FirstCalendarDay(DateTime visibleDate)
    {
      DateTime dateTime = visibleDate;
      if (this.IsMinSupportedYearMonth(dateTime))
        return dateTime;
      int num = (int) (DateTimeFormatInfo.CurrentInfo.Calendar.GetDayOfWeek(dateTime) - this.GetFirstDayOfWeekInt());
      if (num <= 0)
        num += 7;
      return DateTimeFormatInfo.CurrentInfo.Calendar.AddDays(dateTime, -num);
    }
  }
}
