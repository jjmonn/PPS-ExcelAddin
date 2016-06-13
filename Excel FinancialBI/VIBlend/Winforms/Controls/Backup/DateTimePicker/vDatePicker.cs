// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vDatePicker
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>
  /// Represents a vDatePicker control. The control contains a DropDown calendar, and allows you to select a date.
  /// </summary>
  [DefaultEvent("ValueChanged")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  [ToolboxBitmap(typeof (vDatePicker), "ControlIcons.DatePicker.ico")]
  [Description("Represents a vDatePicker control. The control contains a DropDown calendar, and allows you to select a date.")]
  public class vDatePicker : vControlBox
  {
    protected vDateTimeEditor dateTimeEditor = new vDateTimeEditor();
    private vMonthCalendar calendar;

    /// <summary>Gets or sets the validation string.</summary>
    /// <value>The validation string.</value>
    [Category("Behavior")]
    [DefaultValue("Value is out of range.")]
    [Description("Gets or sets the validation string.")]
    public string ValidationString
    {
      get
      {
        return this.dateTimeEditor.ValidationString;
      }
      set
      {
        this.dateTimeEditor.ValidationString = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the built-in validation.
    /// </summary>
    [Category("Behavior")]
    [DefaultValue(true)]
    [Description("Gets or sets a value indicating whether to use the built-in validation.")]
    public bool UseDefaultValidation
    {
      get
      {
        return this.dateTimeEditor.UseDefaultValidation;
      }
      set
      {
        this.dateTimeEditor.UseDefaultValidation = value;
      }
    }

    /// <summary>
    /// Gets or sets the font that is used to display the text in the editbox of the control
    /// </summary>
    /// <value></value>
    [Description("Gets or sets the font that is used to display the text in the editbox of the control")]
    [Category("Appearance")]
    public new Font Font
    {
      get
      {
        return base.Font;
      }
      set
      {
        base.Font = value;
        this.dateTimeEditor.Font = value;
        this.calendar.Font = value;
      }
    }

    /// <summary>
    /// Gets or sets the current culture settings of the control.
    /// </summary>
    [Description("Gets or sets the current culture settings of the control.")]
    [Category("Behavior")]
    public CultureInfo Culture
    {
      get
      {
        return this.dateTimeEditor.Culture;
      }
      set
      {
        this.dateTimeEditor.Culture = value;
      }
    }

    [Browsable(false)]
    public vDateTimeEditor DateTimeEditor
    {
      get
      {
        return this.dateTimeEditor;
      }
    }

    /// <summary>
    /// Gets or sets the text formatting applied to the text box of the DatePicker control
    /// </summary>
    [Description("Gets or sets the text formatting applied to the text box of the DatePicker control.")]
    [Browsable(true)]
    [Category("Behavior")]
    public string FormatValue
    {
      get
      {
        return this.dateTimeEditor.FormatValue;
      }
      set
      {
        this.dateTimeEditor.FormatValue = value;
        this.dateTimeEditor.Refresh();
      }
    }

    /// <summary>Gets or sets the minimum DateTime</summary>
    [DefaultValue(typeof (DateTime), "DateTime.MinDate")]
    [Category("Behavior")]
    [Description("Gets or sets the minimum DateTime")]
    public DateTime MinDate
    {
      get
      {
        this.CreateCalendar();
        return this.calendar.NavigationStartDate;
      }
      set
      {
        this.CreateCalendar();
        this.calendar.NavigationStartDate = value;
        this.dateTimeEditor.Minimum = value;
        DateTime navigationStartDate = this.calendar.NavigationStartDate;
        DateTime? nullable = this.Value;
        if ((nullable.HasValue ? (navigationStartDate < nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          this.Value = new DateTime?(this.calendar.NavigationStartDate);
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the maximum DateTime.</summary>
    [Description("Gets or sets the maximum DateTime.")]
    [DefaultValue(typeof (DateTime), "DateTime.MinDate")]
    [Category("Behavior")]
    public DateTime MaxDate
    {
      get
      {
        this.CreateCalendar();
        return this.calendar.NavigationEndDate;
      }
      set
      {
        this.CreateCalendar();
        this.calendar.NavigationEndDate = value;
        this.dateTimeEditor.Maximum = value;
        DateTime navigationEndDate = this.calendar.NavigationEndDate;
        DateTime? nullable = this.Value;
        if ((nullable.HasValue ? (navigationEndDate > nullable.GetValueOrDefault() ? 1 : 0) : 0) != 0)
          this.Value = new DateTime?(this.calendar.NavigationEndDate);
        this.Invalidate();
      }
    }

    /// <summary>Gets or sets the current DateTime for the control.</summary>
    [DefaultValue(typeof (DateTime), "DateTime.MinDate")]
    [Description("Gets or sets the current DateTime for the control.")]
    [Category("Behavior")]
    public DateTime? Value
    {
      get
      {
        this.CreateCalendar();
        return this.dateTimeEditor.Value;
      }
      set
      {
        DateTime? nullable1 = value;
        DateTime maxDate = this.MaxDate;
        if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() > maxDate ? 1 : 0) : 0) != 0)
          return;
        DateTime? nullable2 = value;
        DateTime minDate = this.MinDate;
        if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() < minDate ? 1 : 0) : 0) != 0)
          return;
        DateTime selectedDate = this.calendar.SelectedDate;
        DateTime? nullable3 = value;
        if ((!nullable3.HasValue ? 1 : (selectedDate != nullable3.GetValueOrDefault() ? 1 : 0)) != 0 && value.HasValue)
          this.calendar.SelectedDate = value.Value;
        DateTime? nullable4 = this.dateTimeEditor.Value;
        DateTime? nullable5 = value;
        if ((nullable4.HasValue != nullable5.HasValue ? 1 : (!nullable4.HasValue ? 0 : (nullable4.GetValueOrDefault() != nullable5.GetValueOrDefault() ? 1 : 0))) != 0)
          this.dateTimeEditor.Value = value;
        if (this.calendar == null)
          this.CreateCalendar();
        this.Invalidate();
        this.OnValueChanged();
      }
    }

    /// <summary>
    /// Returns the calendar control hosted by the vDataTimePicker control
    /// </summary>
    [Browsable(false)]
    public vMonthCalendar Calendar
    {
      get
      {
        return this.calendar;
      }
    }

    /// <summary>Occurs when the value has been changed</summary>
    [Description(" Occurs when the value has been changed")]
    [Category("Action")]
    public event EventHandler ValueChanged;

    static vDatePicker()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>vDatePicker Constructor</summary>
    public vDatePicker()
    {
      this.calendar = new vMonthCalendar();
      this.calendar.Dock = DockStyle.Fill;
      this.calendar.PaintBorder = true;
      this.ContentControl = (Control) this.calendar;
      this.dateTimeEditor.FormatValue = "yyyy-MMM-d";
      this.dateTimeEditor.DefaultDateTimeFormat = DefaultDateTimePatterns.Custom;
      this.EditBase.SetTextBoxControl(this.dateTimeEditor.TextBox);
      this.dateTimeEditor.ValueChanged += new EventHandler(this.dateTimeEditor_ValueChanged);
      this.Value = new DateTime?(DateTime.Now);
      this.ShowGrip = false;
      this.calendar.Size = new Size(250, 200);
      this.DropDownHeight = this.calendar.Height;
      this.DropDownWidth = this.calendar.Width;
      this.calendar.MouseLeave += new EventHandler(this.listBox_MouseLeave);
      this.calendar.MouseEnter += new EventHandler(this.listBox_MouseEnter);
      this.calendar.SelectionChanged += new EventHandler<SelectionEventArgs>(this.calendar_DateSelected);
      this.DropDown.DropDownOpen += new EventHandler(this.DropDown_DropDownOpen);
      this.DropDown.DropDownClose += new EventHandler(this.DropDown_DropDownClose);
      this.dateTimeEditor.Readonly = true;
      this.dateTimeEditor.UpdateText();
      this.dateTimeEditor.TextChanged += new EventHandler(this.dateTimeEditor_TextChanged);
      this.dateTimeEditor.TextBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
    }

    private void CreateCalendar()
    {
      if (this.calendar != null)
        return;
      this.calendar = new vMonthCalendar();
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.DesignMode || string.IsNullOrEmpty(this.Text) || !this.Text.Equals(this.Name))
        return;
      this.dateTimeEditor.UpdateText();
    }

    private void dateTimeEditor_TextChanged(object sender, EventArgs e)
    {
    }

    private void DropDown_DropDownClose(object sender, EventArgs e)
    {
    }

    private void DropDown_DropDownOpen(object sender, EventArgs e)
    {
      if (this.calendar.VIBlendTheme != this.VIBlendTheme)
        this.calendar.VIBlendTheme = this.VIBlendTheme;
      this.calendar.Refresh();
    }

    private void dateTimeEditor_ValueChanged(object sender, EventArgs e)
    {
      this.Value = this.dateTimeEditor.Value;
      if (this.UseDefaultValidation)
      {
        this.dateTimeEditor.GetDefaultValidationErrorProvider().Clear();
        DateTime? nullable1 = this.dateTimeEditor.Value;
        DateTime minDate = this.MinDate;
        if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() < minDate ? 1 : 0) : 0) == 0)
        {
          DateTime? nullable2 = this.dateTimeEditor.Value;
          DateTime maxDate = this.MaxDate;
          if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() > maxDate ? 1 : 0) : 0) == 0)
            goto label_4;
        }
        this.dateTimeEditor.GetDefaultValidationErrorProvider().SetError((Control) this, this.ValidationString);
      }
label_4:
      if (this.dateTimeEditor.Value.HasValue)
        this.calendar.CurrentDate = this.dateTimeEditor.Value.Value.Date;
      else
        this.calendar.CurrentDate = DateTime.Now.Date;
    }

    /// <summary>Called when the value has been changed</summary>
    protected virtual void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, EventArgs.Empty);
    }

    private void calendar_DateSelected(object sender, SelectionEventArgs e)
    {
      if (this.calendar.SelectedDates.Count > 0)
      {
        DateTime valueOrDefault = this.dateTimeEditor.Value.GetValueOrDefault();
        int minute = valueOrDefault.Minute;
        int hour = valueOrDefault.Hour;
        int second = valueOrDefault.Second;
        this.dateTimeEditor.Value = new DateTime?(new DateTime(this.calendar.SelectedDate.Year, this.calendar.SelectedDate.Month, this.calendar.SelectedDate.Day, hour, minute, second));
        this.calendar.SelectedDates.Clear();
        this.calendar.SelectedDate = this.dateTimeEditor.Value.Value.Date;
      }
      this.DropDown.Hide();
    }

    private void listBox_MouseEnter(object sender, EventArgs e)
    {
    }

    private void listBox_MouseLeave(object sender, EventArgs e)
    {
    }
  }
}
