// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vDateTimeEditor
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vDateTimeEditor control.</summary>
  /// <remarks>
  /// vDateTimeEditor provides an editable text field that allows the user to enter date, and time.
  /// </remarks>
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vDateTimeDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [Description("Provides an editable text field that allows the user to enter date, and time.")]
  [ToolboxBitmap(typeof (vDateTimeEditor), "ControlIcons.DateTimeEditor.ico")]
  [DefaultEvent("ValueChanged")]
  public class vDateTimeEditor : vTextBox
  {
    private DateTime? value = new DateTime?(DateTime.Now);
    private string formatValue = "d";
    private List<IDateTimeEditor> editors = new List<IDateTimeEditor>();
    private CultureInfo info = new CultureInfo("");
    private DateTime minimum = DateTime.MinValue;
    private DateTime maximum = DateTime.MaxValue;
    private Timer timer = new Timer();
    private bool autoAdvanceSelection = true;
    private string nullText = "(Null)";
    private string errorString = "Value is out of range.";
    private bool useDefaultValidation = true;
    private bool allowChangeValue = true;
    private ErrorProvider errorProvider = new ErrorProvider();
    private DefaultDateTimePatterns defaultPattern = DefaultDateTimePatterns.Custom;
    private List<Keys> pressedKeys = new List<Keys>();
    private string editorText = "";
    private const string ElementContentName = "Watermark";
    private IDateTimeEditor currentEditor;
    private List<DateTimeEditorFormatItem> items;
    private DateTime? lastValue;
    private bool IsWatermarked;
    private long lastValueOfCurrentEditor;

    /// <summary>
    /// Gets or sets a value indicating whether the auto advance selection is enabled.
    /// </summary>
    public bool AutoAdvanceSelection
    {
      get
      {
        return this.autoAdvanceSelection;
      }
      set
      {
        this.autoAdvanceSelection = value;
      }
    }

    /// <summary>Gets or sets the null text.</summary>
    /// <value>The null text.</value>
    public string NullText
    {
      get
      {
        return this.nullText;
      }
      set
      {
        this.nullText = value;
        this.OnPropertyChanged("NullText");
      }
    }

    /// <summary>Gets or sets the Minimum datetime value.</summary>
    [Description("Gets or sets the Minimum datetime value.")]
    [Category("Behavior")]
    public DateTime Minimum
    {
      get
      {
        return this.minimum;
      }
      set
      {
        if (!(value < this.Maximum))
          return;
        this.minimum = value;
        if (this.Value.HasValue)
        {
          DateTime? nullable = this.Value;
          DateTime minimum = this.Minimum;
          if ((nullable.HasValue ? (nullable.GetValueOrDefault() < minimum ? 1 : 0) : 0) != 0)
            this.Value = new DateTime?(this.Minimum);
        }
        this.OnPropertyChanged("Minimum");
      }
    }

    /// <summary>Gets or sets the Maximum datetime value.</summary>
    [Category("Behavior")]
    [Description("Gets or sets the Maximum datetime value.")]
    public DateTime Maximum
    {
      get
      {
        return this.maximum;
      }
      set
      {
        if (!(value > this.Minimum))
          return;
        this.maximum = value;
        if (this.Value.HasValue)
        {
          DateTime? nullable = this.Value;
          DateTime maximum = this.Maximum;
          if ((nullable.HasValue ? (nullable.GetValueOrDefault() > maximum ? 1 : 0) : 0) != 0)
            this.Value = new DateTime?(this.Maximum);
        }
        this.OnPropertyChanged("Maximum");
      }
    }

    /// <summary>Gets or sets the validation string.</summary>
    /// <value>The validation string.</value>
    [Category("Behavior")]
    [Description("Gets or sets the validation string.")]
    [DefaultValue("Value is out of range.")]
    public string ValidationString
    {
      get
      {
        return this.errorString;
      }
      set
      {
        this.errorString = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use the built-in validation.
    /// </summary>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to use the built-in validation.")]
    [DefaultValue(true)]
    public bool UseDefaultValidation
    {
      get
      {
        return this.useDefaultValidation;
      }
      set
      {
        this.useDefaultValidation = value;
        if (this.errorProvider == null)
          return;
        this.errorProvider.Clear();
        DateTime? nullable1 = this.Value;
        DateTime minimum = this.Minimum;
        if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() < minimum ? 1 : 0) : 0) == 0)
        {
          DateTime? nullable2 = this.Value;
          DateTime maximum = this.Maximum;
          if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() > maximum ? 1 : 0) : 0) == 0)
            return;
        }
        if (!this.UseDefaultValidation)
          return;
        this.errorProvider.SetError((Control) this, this.ValidationString);
      }
    }

    /// <summary>Gets or sets the value</summary>
    [Bindable(true)]
    [Description("Gets or sets the value")]
    [Category("Behavior")]
    [DefaultValue(typeof (DateTime), "DateTime.Now")]
    public DateTime? Value
    {
      get
      {
        return this.value;
      }
      set
      {
        if (this.Value.Equals((object) value))
          return;
        this.errorProvider.Clear();
        DateTime? nullable1 = value;
        DateTime minimum = this.Minimum;
        if ((nullable1.HasValue ? (nullable1.GetValueOrDefault() < minimum ? 1 : 0) : 0) != 0)
        {
          if (this.UseDefaultValidation)
            this.errorProvider.SetError((Control) this, this.ValidationString);
          this.OnValidationFailed();
        }
        DateTime? nullable2 = value;
        DateTime maximum = this.Maximum;
        if ((nullable2.HasValue ? (nullable2.GetValueOrDefault() > maximum ? 1 : 0) : 0) != 0)
        {
          if (this.UseDefaultValidation)
            this.errorProvider.SetError((Control) this, this.ValidationString);
          this.OnValidationFailed();
        }
        this.lastValue = this.value;
        this.value = value;
        this.allowChangeValue = false;
        this.allowChangeValue = true;
        CancelEventArgs args = new CancelEventArgs();
        this.OnValueChanging(args);
        if (args.Cancel)
          return;
        if (this.items == null)
          this.LoadItem(this.FormatValue, this.Culture.DateTimeFormat);
        this.SetEditorsValue(value);
        if (this.items != null)
          this.UpdateText();
        this.OnValueChanged();
      }
    }

    /// <summary>Gets or sets the last datetime value</summary>
    [Category("Behavior")]
    public DateTime? LastValue
    {
      get
      {
        return this.lastValue;
      }
    }

    /// <summary>Gets or sets a mask expression.</summary>
    [DefaultValue("d")]
    [Description("Gets or sets a mask expression.")]
    [Category("Behavior")]
    public string FormatValue
    {
      get
      {
        return this.formatValue;
      }
      set
      {
        this.formatValue = value;
        this.LoadItem(this.formatValue, this.Culture.DateTimeFormat);
        this.OnFormatValueChanged();
        this.OnPropertyChanged("FormatValue");
      }
    }

    /// <summary>Gets or sets the culture.</summary>
    /// <value>The culture.</value>
    [Category("Behavior")]
    [DefaultValue("")]
    public CultureInfo Culture
    {
      get
      {
        return this.info;
      }
      set
      {
        this.info = value;
        this.LoadItem(this.formatValue, this.Culture.DateTimeFormat);
        this.OnPropertyChanged("Culture");
      }
    }

    /// <summary>Gets or sets the default date time format.</summary>
    /// <value>The default date time format.</value>
    [Category("Behavior")]
    [Description(" Gets or sets the default date time format.")]
    public DefaultDateTimePatterns DefaultDateTimeFormat
    {
      get
      {
        return this.defaultPattern;
      }
      set
      {
        this.defaultPattern = value;
        this.SetDefaultDateTimeFormat();
        this.OnPropertyChanged("DefaultDateTimePatterns");
      }
    }

    /// <summary>Occurs when the value has been changed</summary>
    [Category("Action")]
    [Description(" Occurs when the value has been changed")]
    public event EventHandler ValueChanged;

    /// <summary>Occurs when the value has been changing</summary>
    [Description(" Occurs when the value has been changing")]
    [Category("Action")]
    public event EventHandler<CancelEventArgs> ValueChanging;

    /// <summary>Occurs when the format value has been changed</summary>
    [Category("Action")]
    [Description(" Occurs when the format value has been changed")]
    public event EventHandler FormatValueChanged;

    /// <summary>Represents the ValidationFailed event.</summary>
    [Description("Occurs when a value is out of the given Minimum - Maximmum range.")]
    [Category("Action")]
    public event EventHandler ValidationFailed;

    /// <summary>Occurs when a property has changed.</summary>
    [Category("Action")]
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>DateTimeEditor contructor</summary>
    public vDateTimeEditor()
    {
      this.timer.Interval = 500;
      this.timer.Tick += new EventHandler(this.timer_Tick);
      this.LoadItem(this.FormatValue, this.Culture.DateTimeFormat);
      this.UpdateText();
    }

    /// <summary>Gets the editors.</summary>
    /// <returns></returns>
    public List<IDateTimeEditor> GetEditors()
    {
      return this.editors;
    }

    /// <summary>Gets the items.</summary>
    /// <returns></returns>
    public List<DateTimeEditorFormatItem> GetItems()
    {
      return this.items;
    }

    /// <summary>Calls the ValidationFailed event.</summary>
    protected internal virtual void OnValidationFailed()
    {
      if (this.ValidationFailed == null)
        return;
      this.ValidationFailed((object) this, EventArgs.Empty);
    }

    /// <summary>Gets the default validation error provider.</summary>
    /// <returns></returns>
    public ErrorProvider GetDefaultValidationErrorProvider()
    {
      return this.errorProvider;
    }

    /// <summary>Called when the value has been changed</summary>
    protected virtual void OnValueChanging(CancelEventArgs args)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, args);
    }

    /// <summary>Called when the value has been changed</summary>
    protected virtual void OnValueChanged()
    {
      if (this.ValueChanged == null)
        return;
      this.ValueChanged((object) this, EventArgs.Empty);
    }

    /// <summary>Called when the format value has been changed</summary>
    protected virtual void OnFormatValueChanged()
    {
      if (this.FormatValueChanged == null)
        return;
      this.FormatValueChanged((object) this, EventArgs.Empty);
    }

    private void SetEditorsValue(DateTime? date)
    {
      if (!date.HasValue)
        return;
      DateTime dateTime = date.Value;
      long num1 = (long) dateTime.Year;
      long num2 = (long) dateTime.Day;
      long num3 = (long) dateTime.Hour;
      long num4 = (long) dateTime.Millisecond;
      long num5 = (long) dateTime.Second;
      long num6 = (long) dateTime.Minute;
      long num7 = (long) dateTime.Month;
      if (this.items == null)
        return;
      for (int index = 0; index < this.items.Count; ++index)
      {
        switch (this.items[index].ItemType)
        {
          case DateTimeItemType.Year:
            (this.editors[index] as DateTimeFormatBaseEditor).Value = num1;
            break;
          case DateTimeItemType.Month:
            (this.editors[index] as DateTimeFormatBaseEditor).Value = num7;
            break;
          case DateTimeItemType.Day:
            (this.editors[index] as DateTimeFormatBaseEditor).Value = num2;
            break;
          case DateTimeItemType.Minute:
            (this.editors[index] as DateTimeFormatBaseEditor).Value = num6;
            break;
          case DateTimeItemType.Second:
            (this.editors[index] as DateTimeFormatBaseEditor).Value = num5;
            break;
          case DateTimeItemType.Millisecond:
            (this.editors[index] as DateTimeFormatBaseEditor).Value = num4;
            break;
          case DateTimeItemType.FORMAT_hh:
            if (num3 > 12L)
              num3 -= 12L;
            (this.editors[index] as DateTimeFormatBaseEditor).Value = num3;
            break;
          case DateTimeItemType.FORMAT_HH:
            (this.editors[index] as DateTimeFormatBaseEditor).Value = num3;
            break;
        }
      }
    }

    /// <exclude />
    public void SetDefaultDateTimeFormat()
    {
      switch (this.DefaultDateTimeFormat)
      {
        case DefaultDateTimePatterns.RFC1123Pattern:
          this.FormatValue = "R";
          break;
        case DefaultDateTimePatterns.SortableDateTimePattern:
          this.FormatValue = "s";
          break;
        case DefaultDateTimePatterns.UniversalSortableDateTimePattern:
          this.FormatValue = "u";
          break;
        case DefaultDateTimePatterns.YearMonthPattern:
          this.FormatValue = "Y";
          break;
        case DefaultDateTimePatterns.ShortDatePattern:
          this.FormatValue = "d";
          break;
        case DefaultDateTimePatterns.LongDatePattern:
          this.FormatValue = "D";
          break;
        case DefaultDateTimePatterns.ShortTimePattern:
          this.FormatValue = "t";
          break;
        case DefaultDateTimePatterns.LongTimePattern:
          this.FormatValue = "T";
          break;
        case DefaultDateTimePatterns.FullDateTimePattern:
          this.FormatValue = "F";
          break;
        case DefaultDateTimePatterns.MonthDayPattern:
          this.FormatValue = "M";
          break;
        case DefaultDateTimePatterns.Custom:
          this.FormatValue = "";
          break;
      }
    }

    private string GetFormatValue(string format, DateTimeFormatInfo info)
    {
      if (format == null || format.Length == 0)
        format = "G";
      if (format.Length == 1)
      {
        switch (format[0])
        {
          case 'm':
          case 'M':
            return info.MonthDayPattern;
          case 'r':
          case 'R':
            return info.RFC1123Pattern;
          case 's':
            return info.SortableDateTimePattern;
          case 't':
            return info.ShortTimePattern;
          case 'u':
            return info.UniversalSortableDateTimePattern;
          case 'y':
          case 'Y':
            return info.YearMonthPattern;
          case 'd':
            return info.ShortDatePattern;
          case 'f':
            return info.LongDatePattern + (object) ' ' + info.ShortTimePattern;
          case 'g':
            return info.ShortDatePattern + (object) ' ' + info.ShortTimePattern;
          case 'D':
            return info.LongDatePattern;
          case 'F':
            return info.FullDateTimePattern;
          case 'G':
            return info.ShortDatePattern + (object) ' ' + info.LongTimePattern;
          case 'T':
            return info.LongTimePattern;
        }
      }
      if (format.Length == 2 && (int) format[0] == 37)
        format = format.Substring(1);
      return format;
    }

    /// <summary>Loads the item.</summary>
    /// <param name="mask">The mask.</param>
    /// <param name="dateTimeFormatInfo">The date time format info.</param>
    private void LoadItem(string mask, DateTimeFormatInfo dateTimeFormatInfo)
    {
      if (this.Value.HasValue)
      {
        this.items = this.ParseFormatValue(this.GetFormatValue(mask, dateTimeFormatInfo), dateTimeFormatInfo);
        this.editors.Clear();
        for (int index = 0; index < this.items.Count; ++index)
          this.editors.Add(this.items[index].GetDateTimeEditorByItemType(this.Value.Value));
      }
      this.UpdateText();
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (this.Readonly)
        return;
      this.SetSelection();
    }

    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      if (this.Readonly)
        return;
      this.SetSelection();
    }

    private void TextBox_MouseWheel(object sender, MouseEventArgs e)
    {
    }

    private void SetSelection()
    {
      string str1 = "";
      if (this.items == null)
        this.LoadItem(this.FormatValue, this.Culture.DateTimeFormat);
      for (int index = 0; index < this.items.Count; ++index)
      {
        string str2 = this.items[index].DateParser(this.Value);
        str1 += str2;
        if (str1.Length >= this.SelectionStart)
        {
          this.currentEditor = this.editors[index];
          if (!(this.currentEditor is DisabledEditor))
          {
            this.SelectionStart = str1.Length - str2.Length;
            this.SelectionLength = str2.Length;
            break;
          }
        }
      }
      if (this.currentEditor == null || !(this.currentEditor is DateTimeFormatBaseEditor))
        return;
      (this.currentEditor as DateTimeFormatBaseEditor).positions = 0L;
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      if (this.currentEditor == null || !(this.currentEditor is DateTimeFormatBaseEditor))
        return;
      (this.currentEditor as DateTimeFormatBaseEditor).positions = 0L;
    }

    private void SetSelection(int itemIndex)
    {
      string str1 = "";
      for (int index = 0; index < this.items.Count; ++index)
      {
        string str2 = this.items[index].DateParser(this.Value);
        str1 += str2;
        if (index == itemIndex)
        {
          this.SelectionStart = str1.Length - str2.Length;
          this.SelectionLength = str2.Length;
          break;
        }
      }
    }

    /// <summary>Updates the text.</summary>
    public virtual void UpdateText()
    {
      bool flag = false;
      if (!this.Value.HasValue)
      {
        this.IsWatermarked = true;
        this.Text = this.NullText;
      }
      else
        this.IsWatermarked = false;
      if (!this.Value.HasValue || flag)
        return;
      this.IsWatermarked = false;
      this.Text = this.Format(new DateTime?(this.Value.Value));
    }

    /// <summary>
    /// Called when <see cref="E:System.Windows.UIElement.KeyDown" /> event occurs.
    /// </summary>
    /// <param name="e">The data for the event.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.Readonly)
      {
        base.OnKeyDown(e);
      }
      else
      {
        this.DoKeyDown(e);
        e.Handled = true;
      }
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      e.Handled = true;
    }

    private bool IsValidShortcutKey(Keys key)
    {
      return key == Keys.L || key == Keys.N || (key == Keys.Y || key == Keys.W) || (key == Keys.T || key == Keys.X || (key == Keys.E || key == Keys.D)) || (key == Keys.M || key == Keys.H || (key == Keys.Home || key == Keys.End) || (key == Keys.Up || key == Keys.Down || (key == Keys.Next || key == Keys.Prior)));
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      if (!this.Value.HasValue)
      {
        this.timer.Stop();
      }
      else
      {
        if (this.pressedKeys.Count == 1)
        {
          DayOfWeek dayOfWeek = this.Value.Value.DayOfWeek;
          int num1 = (int) dayOfWeek;
          switch (this.pressedKeys[0])
          {
            case Keys.T:
              this.Value = new DateTime?(DateTime.Now.Date);
              break;
            case Keys.X:
              this.Value = new DateTime?(new DateTime(this.Value.Value.Year, 12, 25));
              break;
            case Keys.Y:
              this.Value = new DateTime?(this.Value.Value.AddDays(-1.0));
              break;
            case Keys.Prior:
              this.Value = new DateTime?(new DateTime(this.Value.Value.Year, this.Value.Value.Month, DateTime.DaysInMonth(this.Value.Value.Year, this.Value.Value.Month)));
              break;
            case Keys.Next:
              this.Value = new DateTime?(new DateTime(this.Value.Value.Year, this.Value.Value.Month, 1));
              break;
            case Keys.End:
              int num2 = (int) dayOfWeek;
              if (dayOfWeek == DayOfWeek.Sunday)
                num1 = 7;
              if (num1 != 7)
              {
                this.Value = new DateTime?(this.Value.Value.AddDays((double) -(num2 - 7)));
                break;
              }
              break;
            case Keys.Home:
              if (dayOfWeek == DayOfWeek.Sunday)
                num1 = 7;
              this.Value = new DateTime?(this.Value.Value.AddDays((double) -(num1 - 1)));
              break;
            case Keys.Up:
              this.Value = new DateTime?(this.Value.Value.AddDays(7.0));
              break;
            case Keys.Down:
              this.Value = new DateTime?(this.Value.Value.AddDays(-7.0));
              break;
            case Keys.N:
              this.Value = new DateTime?(DateTime.Now);
              break;
          }
        }
        int itemIndex = this.editors.IndexOf(this.currentEditor);
        if (itemIndex >= 0)
          this.SetSelection(itemIndex);
        this.pressedKeys.Clear();
        this.timer.Stop();
      }
    }

    protected virtual void DoKeyDown(KeyEventArgs e)
    {
      if (this.Readonly)
        return;
      if (e.KeyCode == Keys.Left && this.SelectionLength == this.Text.Length)
        e.Handled = true;
      else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
      {
        e.Handled = true;
        this.SelectAll();
      }
      else if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) && this.SelectionLength == this.Text.Length)
      {
        this.Value = new DateTime?();
        this.currentEditor = (IDateTimeEditor) null;
        this.UpdateText();
        e.Handled = true;
      }
      else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control && this.SelectionLength > 0)
      {
        Clipboard.SetText(this.Text.Substring(this.SelectionStart, this.SelectionLength));
        e.Handled = true;
      }
      else
      {
        int selectionStart = this.SelectionStart;
        int keyValue = e.KeyValue;
        if (this.currentEditor == null && this.editors.Count > 0)
          this.currentEditor = this.editors[0];
        if (e.Modifiers == Keys.Control)
        {
          if (e.KeyCode == Keys.Delete)
          {
            this.Value = new DateTime?();
            this.currentEditor = (IDateTimeEditor) null;
            this.UpdateText();
            e.Handled = true;
          }
          else
          {
            if (this.pressedKeys.Count >= 2)
              this.pressedKeys.Clear();
            this.timer.Stop();
            if (this.IsValidShortcutKey(e.KeyCode))
            {
              if (this.pressedKeys.Count > 0 && this.pressedKeys[0] != Keys.L && (this.pressedKeys[0] != Keys.N && this.pressedKeys[0] != Keys.X))
                this.pressedKeys.Clear();
              this.pressedKeys.Add(e.KeyCode);
            }
            else
              this.pressedKeys.Clear();
            if (this.pressedKeys.Count == 2)
            {
              Keys keys1 = this.pressedKeys[0];
              Keys keys2 = this.pressedKeys[1];
              if (keys1 == Keys.L)
              {
                switch (keys2)
                {
                  case Keys.M:
                    this.Value = new DateTime?(this.Value.Value.AddMonths(-1));
                    break;
                  case Keys.W:
                    this.Value = new DateTime?(this.Value.Value.AddDays(-7.0));
                    break;
                  case Keys.Y:
                    this.Value = new DateTime?(this.Value.Value.AddYears(-1));
                    break;
                  case Keys.D:
                    this.Value = new DateTime?(this.Value.Value.AddDays(-1.0));
                    break;
                  case Keys.H:
                    this.Value = new DateTime?(this.Value.Value.AddHours(-1.0));
                    break;
                }
              }
              else if (keys1 == Keys.N)
              {
                switch (keys2)
                {
                  case Keys.M:
                    this.Value = new DateTime?(this.Value.Value.AddMonths(1));
                    break;
                  case Keys.W:
                    this.Value = new DateTime?(this.Value.Value.AddDays(7.0));
                    break;
                  case Keys.Y:
                    this.Value = new DateTime?(this.Value.Value.AddYears(1));
                    break;
                  case Keys.D:
                    this.Value = new DateTime?(this.Value.Value.AddDays(1.0));
                    break;
                  case Keys.H:
                    this.Value = new DateTime?(this.Value.Value.AddHours(1.0));
                    break;
                }
              }
              else if (keys1 == Keys.X && keys2 == Keys.E)
                this.Value = new DateTime?(new DateTime(this.Value.Value.Year, 12, 24));
            }
            else if (this.pressedKeys.Count == 1)
              this.timer.Start();
            int itemIndex = this.editors.IndexOf(this.currentEditor);
            if (itemIndex < 0)
              return;
            this.SetSelection(itemIndex);
          }
        }
        else
        {
          switch (e.KeyCode)
          {
            case Keys.Space:
              this.DoSpaceKey();
              e.Handled = true;
              break;
            case Keys.End:
              this.DoEndKey();
              break;
            case Keys.Home:
              this.DoHomeKey();
              break;
            case Keys.Left:
              this.DoLeftKey();
              e.Handled = true;
              break;
            case Keys.Up:
              this.DoUpKey();
              e.Handled = true;
              break;
            case Keys.Right:
              this.DoRightKey();
              e.Handled = true;
              break;
            case Keys.Down:
              this.DoDownKey();
              e.Handled = true;
              break;
            case Keys.F2:
              this.DoF2Key();
              break;
            default:
              if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.LControlKey || e.KeyCode == Keys.Tab)
                break;
              this.UpdateValue();
              this.UpdateText();
              if (this.currentEditor == null)
                break;
              if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
              {
                this.currentEditor.InsertString("");
                if (this.currentEditor is DateTimeFormatBaseEditor)
                  (this.currentEditor as DateTimeFormatBaseEditor).Value = (this.currentEditor as DateTimeFormatBaseEditor).MinimumValue;
              }
              int num1 = 48;
              int num2 = keyValue;
              bool flag = false;
              int num3 = num2 - num1;
              if (num3 >= 0 && num3 <= 10)
              {
                int num4 = num2 - num1;
                DateTimeFormatBaseEditor formatBaseEditor = this.currentEditor as DateTimeFormatBaseEditor;
                if (formatBaseEditor != null && formatBaseEditor.positions == 0L)
                {
                  this.editorText = string.Empty;
                  formatBaseEditor.Value = 0L;
                }
                this.currentEditor.InsertString(num4.ToString());
                if (formatBaseEditor != null && (long) this.editorText.Length >= formatBaseEditor.MaxPositions)
                  this.editorText = "";
                this.editorText += num4.ToString();
                this.SelectWithAdvancePattern();
                flag = true;
              }
              if (!flag)
              {
                int num4 = -1;
                switch (e.KeyCode)
                {
                  case Keys.NumPad0:
                    num4 = 0;
                    break;
                  case Keys.NumPad1:
                    num4 = 1;
                    break;
                  case Keys.NumPad2:
                    num4 = 2;
                    break;
                  case Keys.NumPad3:
                    num4 = 3;
                    break;
                  case Keys.NumPad4:
                    num4 = 4;
                    break;
                  case Keys.NumPad5:
                    num4 = 5;
                    break;
                  case Keys.NumPad6:
                    num4 = 6;
                    break;
                  case Keys.NumPad7:
                    num4 = 7;
                    break;
                  case Keys.NumPad8:
                    num4 = 8;
                    break;
                  case Keys.NumPad9:
                    num4 = 9;
                    break;
                }
                if (num4 >= 0)
                {
                  DateTimeFormatBaseEditor formatBaseEditor = this.currentEditor as DateTimeFormatBaseEditor;
                  if (formatBaseEditor != null && formatBaseEditor.positions == 0L)
                  {
                    this.editorText = string.Empty;
                    formatBaseEditor.Value = 0L;
                  }
                  this.currentEditor.InsertString(num4.ToString());
                  this.editorText += num4.ToString();
                  this.SelectWithAdvancePattern();
                }
              }
              this.editors.IndexOf(this.currentEditor);
              this.UpdateValue();
              this.UpdateText();
              this.SetSelection(this.editors.IndexOf(this.currentEditor));
              e.Handled = true;
              break;
          }
        }
      }
    }

    private void SelectWithAdvancePattern()
    {
      if (!this.AutoAdvanceSelection)
        return;
      DateTimeFormatBaseEditor formatBaseEditor = this.currentEditor as DateTimeFormatBaseEditor;
      if (formatBaseEditor == null)
        return;
      if ((long) this.editorText.Length == formatBaseEditor.MaxPositions)
      {
        this.DoRightKey();
        if (this.currentEditor == formatBaseEditor)
          return;
        this.editorText = string.Empty;
      }
      else
      {
        bool flag = true;
        for (int index = 0; index < 10; ++index)
        {
          if (long.Parse(this.editorText + index.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture), (IFormatProvider) CultureInfo.InvariantCulture) <= formatBaseEditor.MaximumValue)
          {
            flag = false;
            break;
          }
        }
        if (!flag)
          return;
        this.DoRightKey();
        if (this.currentEditor == formatBaseEditor)
          return;
        this.editorText = string.Empty;
      }
    }

    /// <summary>Does down key.</summary>
    protected virtual void DoDownKey()
    {
      this.SpinDown();
    }

    /// <summary>
    /// Raises the <see cref="E:TextChanged" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected override void OnTextChanged(EventArgs e)
    {
      if (!this.DesignMode || string.IsNullOrEmpty(this.Text) || !this.Text.Equals(this.Name))
        return;
      this.UpdateText();
    }

    /// <summary>Does up key.</summary>
    protected virtual void DoUpKey()
    {
      this.SpinUp();
    }

    /// <summary>Does the f2 key.</summary>
    protected virtual void DoF2Key()
    {
      this.Value = new DateTime?(this.Minimum);
    }

    /// <summary>Does the space key.</summary>
    protected virtual void DoSpaceKey()
    {
      if (this.currentEditor == null || this.editors.Count <= 0)
        return;
      if (this.editors[this.editors.Count - 1].Equals((object) this.currentEditor))
        this.DoHomeKey();
      else
        this.DoRightKey();
    }

    /// <summary>Does the end key.</summary>
    protected virtual void DoEndKey()
    {
      if (this.currentEditor == null)
        return;
      for (int itemIndex = this.editors.Count - 1; itemIndex >= 0; --itemIndex)
      {
        if (!(this.editors[itemIndex] is DisabledEditor))
        {
          this.currentEditor = this.editors[itemIndex];
          this.SetSelection(itemIndex);
          break;
        }
      }
    }

    /// <summary>Does the home key.</summary>
    protected virtual void DoHomeKey()
    {
      if (this.currentEditor == null)
        return;
      for (int itemIndex = 0; itemIndex < this.editors.Count; ++itemIndex)
      {
        if (!(this.editors[itemIndex] is DisabledEditor))
        {
          this.currentEditor = this.editors[itemIndex];
          this.SetSelection(itemIndex);
          break;
        }
      }
    }

    /// <summary>Does the left key.</summary>
    protected virtual void DoLeftKey()
    {
      if (this.currentEditor == null)
        return;
      IDateTimeEditor dateTimeEditor = this.currentEditor;
      bool flag = false;
      int itemIndex = this.editors.IndexOf(this.currentEditor);
      int index = itemIndex;
      while (itemIndex > 0)
      {
        this.currentEditor = this.editors[--itemIndex];
        this.SetSelection(itemIndex);
        if (!(this.currentEditor is DisabledEditor))
        {
          flag = true;
          break;
        }
      }
      if (!flag && index >= 0)
        this.currentEditor = this.editors[index];
      if (this.currentEditor == null || dateTimeEditor == this.currentEditor || !(this.currentEditor is DateTimeFormatBaseEditor))
        return;
      (this.currentEditor as DateTimeFormatBaseEditor).positions = 0L;
    }

    /// <summary>Does the right key.</summary>
    protected virtual void DoRightKey()
    {
      if (this.currentEditor == null)
        return;
      IDateTimeEditor dateTimeEditor = this.currentEditor;
      bool flag = false;
      int itemIndex = this.editors.IndexOf(this.currentEditor);
      int index = itemIndex;
      while (itemIndex <= this.editors.Count - 2)
      {
        this.currentEditor = this.editors[++itemIndex];
        this.SetSelection(itemIndex);
        if (!(this.currentEditor is DisabledEditor))
        {
          flag = true;
          break;
        }
      }
      if (!flag && index >= 0)
        this.currentEditor = this.editors[index];
      if (this.currentEditor == null || this.currentEditor == dateTimeEditor || !(this.currentEditor is DateTimeFormatBaseEditor))
        return;
      (this.currentEditor as DateTimeFormatBaseEditor).positions = 0L;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      this.DoMouseWheel(e);
    }

    /// <summary>Does the mouse wheel.</summary>
    /// <param name="e">The <see cref="!:System.Windows.Input.MouseWheelEventArgs" /> instance containing the event data.</param>
    protected virtual void DoMouseWheel(MouseEventArgs e)
    {
      if (this.Readonly || !this.TextBox.Focused)
        return;
      if (e.Delta < 0)
        this.SpinDown();
      else
        this.SpinUp();
    }

    /// <summary>Performs the spin.</summary>
    /// <param name="spinUp">if set to <c>true</c> [spin up].</param>
    public virtual void PerformSpin(bool spinUp)
    {
      if (spinUp)
        this.SpinUp();
      else
        this.SpinDown();
    }

    /// <summary>Spins down.</summary>
    protected internal virtual void SpinDown()
    {
      if (this.currentEditor != null)
      {
        if (this.items[this.editors.IndexOf(this.currentEditor)].ItemType == DateTimeItemType.Day && this.Value.HasValue)
          (this.currentEditor as DateTimeFormatDayMonthYearEditor).MaximumValue = (long) DateTime.DaysInMonth(this.Value.Value.Year, this.Value.Value.Month);
        this.currentEditor.Down();
      }
      this.UpdateValue();
      this.SetSelection(this.editors.IndexOf(this.currentEditor));
    }

    /// <summary>Spins up.</summary>
    protected internal virtual void SpinUp()
    {
      if (this.currentEditor != null)
      {
        if (this.items[this.editors.IndexOf(this.currentEditor)].ItemType == DateTimeItemType.Day && this.Value.HasValue)
          (this.currentEditor as DateTimeFormatDayMonthYearEditor).MaximumValue = (long) DateTime.DaysInMonth(this.Value.Value.Year, this.Value.Value.Month);
        this.currentEditor.Up();
      }
      this.UpdateValue();
      int itemIndex = this.editors.IndexOf(this.currentEditor);
      if (itemIndex < 0)
        return;
      this.SetSelection(itemIndex);
    }

    private int GetAmPmOffset()
    {
      for (int index = 0; index < this.items.Count; ++index)
      {
        if (this.items[index].ItemType == DateTimeItemType.FORMAT_AMPM)
          return (int) this.editors[index].GetTextValue();
      }
      return 0;
    }

    private void UpdateValue()
    {
      DateTime dateTime = DateTime.MinValue;
      long num1 = 1;
      long num2 = 1;
      long num3 = 0;
      long num4 = 0;
      long num5 = 0;
      long num6 = 0;
      long num7 = 1;
      long num8 = 0;
      List<IDateTimeEditor> dateTimeEditorList = new List<IDateTimeEditor>();
      bool flag1 = false;
      bool flag2 = false;
      bool flag3 = false;
      for (int index = 0; index < this.items.Count; ++index)
      {
        switch (this.items[index].ItemType)
        {
          case DateTimeItemType.Year:
            num1 = this.editors[index].GetTextValue();
            flag1 = true;
            if (num1 == 0L)
            {
              num1 = 1L;
              break;
            }
            break;
          case DateTimeItemType.Month:
            num7 = this.editors[index].GetTextValue();
            flag2 = true;
            if (num7 == 0L)
            {
              num7 = 1L;
              break;
            }
            break;
          case DateTimeItemType.Day:
            num2 = this.editors[index].GetTextValue();
            flag3 = true;
            dateTimeEditorList.Add(this.editors[index]);
            if (num2 == 0L)
            {
              num2 = 1L;
              break;
            }
            break;
          case DateTimeItemType.Minute:
            num6 = this.editors[index].GetTextValue();
            break;
          case DateTimeItemType.Second:
            num5 = this.editors[index].GetTextValue();
            break;
          case DateTimeItemType.Millisecond:
            num4 = this.editors[index].GetTextValue();
            break;
          case DateTimeItemType.FORMAT_hh:
            num3 = (this.editors[index] as DateTimeFormatBaseEditor).Value;
            if (num3 < 12L && this.GetAmPmOffset() == 1)
            {
              num3 += 12L;
              break;
            }
            break;
          case DateTimeItemType.FORMAT_HH:
            num3 = this.editors[index].GetTextValue();
            break;
          case DateTimeItemType.FORMAT_AMPM:
            num8 = this.editors[index].GetTextValue();
            break;
        }
      }
      if (num1 > 0L && num7 > 0L && (num2 > 0L && num6 >= 0L) && (num3 >= 0L && num5 >= 0L && num4 >= 0L))
      {
        DateTime valueOrDefault = this.Value.GetValueOrDefault(this.Minimum);
        try
        {
          if (!flag3)
            num2 = (long) valueOrDefault.Day;
          if (!flag2)
            num7 = (long) valueOrDefault.Month;
          if (!flag1)
            num1 = (long) valueOrDefault.Year;
          if ((long) DateTime.DaysInMonth((int) num1, (int) num7) <= num2)
          {
            num2 = (long) DateTime.DaysInMonth((int) num1, (int) num7);
            if (dateTimeEditorList.Count > 0)
            {
              foreach (DateTimeFormatBaseEditor formatBaseEditor in dateTimeEditorList)
                formatBaseEditor.Value = num2;
            }
          }
          this.Value = new DateTime?(new DateTime((int) num1, (int) num7, (int) num2, (int) num3, (int) num6, (int) num5, (int) num4));
        }
        catch (Exception ex)
        {
          this.Value = new DateTime?(valueOrDefault);
        }
      }
      if (this.currentEditor == null || !(this.currentEditor is EditorAmPmItem))
        return;
      this.Value = new DateTime?(this.items[this.editors.IndexOf(this.currentEditor)].GetDateTimeWithOffset((int) num8, this.Value.Value));
    }

    /// <summary>
    /// Called before <see cref="E:System.Windows.UIElement.KeyUp" /> event occurs.
    /// </summary>
    /// <param name="e">The data for the event.</param>
    protected override void OnKeyUp(KeyEventArgs e)
    {
      if (this.Readonly)
        base.OnKeyUp(e);
      else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
      {
        e.Handled = true;
        this.SelectAll();
      }
      else if (e.KeyCode == Keys.C || e.KeyCode == Keys.A)
        e.Handled = true;
      else if (e.Modifiers == Keys.Control)
        e.Handled = true;
      else if (e.KeyCode == Keys.Control)
        e.Handled = true;
      else if (this.SelectionLength == this.Text.Length)
      {
        e.Handled = true;
      }
      else
      {
        if (this.currentEditor != null)
          e.Handled = true;
        e.Handled = true;
      }
    }

    /// <summary>Parses the value.</summary>
    /// <param name="text">The text.</param>
    public void ParseValue(string text)
    {
      DateTime result;
      if (!DateTime.TryParse(text, out result))
        return;
      this.value = new DateTime?(result);
    }

    /// <summary>Formats the specified formatted.</summary>
    /// <param name="formatted">The formatted.</param>
    /// <returns></returns>
    public string Format(DateTime? formatted)
    {
      if (this.items.Count >= 1)
        return this.Format(formatted, 0, this.items.Count - 1);
      return "";
    }

    /// <summary>
    /// return a formatted date time string based on a start and end indeces
    /// </summary>
    /// <param name="formatted">The formatted.</param>
    /// <param name="startFormatIndex">Start index of the format.</param>
    /// <param name="endFormatIndex">End index of the format.</param>
    /// <returns></returns>
    public string Format(DateTime? formatted, int startFormatIndex, int endFormatIndex)
    {
      string str = string.Empty;
      for (int index = startFormatIndex; index <= endFormatIndex; ++index)
        str += this.items[index].DateParser(formatted);
      return str;
    }

    private static int GetFormatValueGroupLength(string item)
    {
      for (int index = 1; index < item.Length; ++index)
      {
        if ((int) item[index] != (int) item[0])
          return index;
      }
      return item.Length;
    }

    private List<DateTimeEditorFormatItem> ParseFormatValue(string value, DateTimeFormatInfo dateTimeFormatInfo)
    {
      List<DateTimeEditorFormatItem> editorFormatItemList = new List<DateTimeEditorFormatItem>();
      int num1;
      for (string str = value; str.Length > 0; str = str.Substring(num1))
      {
        num1 = vDateTimeEditor.GetFormatValueGroupLength(str);
        DateTimeEditorFormatItem editorFormatItem = (DateTimeEditorFormatItem) null;
        switch (str[0])
        {
          case 's':
          case 'S':
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Second);
            break;
          case 't':
          case 'T':
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.FORMAT_AMPM);
            break;
          case 'y':
          case 'Y':
            if (num1 > 1)
            {
              editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Year);
              break;
            }
            num1 = 1;
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, 1), dateTimeFormatInfo, DateTimeItemType.Readonly);
            break;
          case 'z':
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Readonly);
            break;
          case 'd':
          case 'D':
            editorFormatItem = num1 <= 2 ? new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Day) : new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Readonly);
            break;
          case 'f':
          case 'F':
            if (num1 > 7)
              num1 = 7;
            editorFormatItem = num1 <= 3 ? new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Millisecond) : new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Readonly);
            break;
          case 'g':
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Readonly);
            break;
          case 'h':
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.FORMAT_hh);
            break;
          case 'm':
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Minute);
            break;
          case '\\':
            if (str.Length >= 2)
            {
              editorFormatItem = new DateTimeEditorFormatItem(str.Substring(1, 1), dateTimeFormatInfo, DateTimeItemType.Readonly);
              num1 = 2;
              break;
            }
            break;
          case ':':
          case '/':
            num1 = 1;
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, 1), dateTimeFormatInfo, DateTimeItemType.Readonly);
            break;
          case 'H':
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.FORMAT_HH);
            break;
          case 'M':
            if (num1 > 4)
              num1 = 4;
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, num1), dateTimeFormatInfo, DateTimeItemType.Month);
            break;
          case '"':
          case '\'':
            int num2 = str.IndexOf(str[0], 1);
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(1, Math.Max(1, num2 - 1)), dateTimeFormatInfo, DateTimeItemType.Readonly);
            num1 = Math.Max(1, num2 + 1);
            break;
          default:
            num1 = 1;
            editorFormatItem = new DateTimeEditorFormatItem(str.Substring(0, 1), dateTimeFormatInfo, DateTimeItemType.Readonly);
            break;
        }
        editorFormatItemList.Add(editorFormatItem);
      }
      return editorFormatItemList;
    }

    /// <summary>Called when property is changed.</summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
