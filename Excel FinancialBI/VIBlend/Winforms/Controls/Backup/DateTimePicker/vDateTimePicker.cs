// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vDateTimePicker
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  /// <summary>Represents a vDateTimePicker control</summary>
  /// <remarks>
  /// A vDateTimePicker displays an editable text box which allows the user to enter a time or date, or it select from a drop-down calendar.
  /// </remarks>
  [Description("Displays an editable text box which allows the user to enter a time or date, or it select from a drop-down calendar.")]
  [ToolboxBitmap(typeof (vDateTimePicker), "ControlIcons.DateTimePicker.ico")]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  [ToolboxItem(true)]
  public class vDateTimePicker : vDatePicker
  {
    private string nullValueText = "null value";
    private Timer timer = new Timer();

    /// <summary>Gets or sets the default date time format.</summary>
    /// <value>The default date time format.</value>
    [Description(" Gets or sets the default date time format.")]
    [Category("Behavior")]
    public DefaultDateTimePatterns DefaultDateTimeFormat
    {
      get
      {
        return this.dateTimeEditor.DefaultDateTimeFormat;
      }
      set
      {
        this.dateTimeEditor.DefaultDateTimeFormat = value;
      }
    }

    /// <summary>Gets the update timer.</summary>
    /// <value>The update timer.</value>
    [Browsable(false)]
    public Timer UpdateTimer
    {
      get
      {
        return this.timer;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.WinForms.Controls.vDateTimePicker" /> class.
    /// </summary>
    public vDateTimePicker()
    {
      this.Value = new DateTime?(DateTime.Now);
      this.dateTimeEditor.DefaultDateTimeFormat = DefaultDateTimePatterns.ShortDatePattern;
      this.dateTimeEditor.ValueChanged += new EventHandler(this.editor_ValueChanged);
      this.timer.Interval = 3500;
      this.timer.Tick += new EventHandler(this.OnTimer_Tick);
      this.dateTimeEditor.Readonly = false;
      this.dateTimeEditor.TextBox.TextChanged += new EventHandler(this.TextBox_TextChanged);
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.DesignMode || string.IsNullOrEmpty(this.Text) || !this.Text.Equals(this.Name))
        return;
      this.dateTimeEditor.UpdateText();
    }

    /// <summary>Clean up any resources being used.</summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.timer.Dispose();
      base.Dispose(disposing);
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (e.KeyCode != Keys.Delete)
        return;
      this.Value = new DateTime?(this.MinDate);
    }

    private void editor_ValueChanged(object sender, EventArgs e)
    {
      DateTime? nullable = this.dateTimeEditor.Value;
      DateTime minDate = this.MinDate;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() < minDate ? 1 : 0) : 0) == 0)
        return;
      this.timer.Stop();
      this.timer.Start();
    }

    private void OnTimer_Tick(object sender, EventArgs e)
    {
      DateTime? nullable = this.dateTimeEditor.Value;
      DateTime minDate = this.MinDate;
      if ((nullable.HasValue ? (nullable.GetValueOrDefault() < minDate ? 1 : 0) : 0) != 0)
        this.Value = new DateTime?(this.MinDate);
      this.timer.Stop();
    }
  }
}
