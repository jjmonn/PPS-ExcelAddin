// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.InputBase
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace VIBlend.WinForms.Controls
{
  [ToolboxItem(false)]
  public class InputBase : vTextBox, INotifyPropertyChanged
  {
    protected string undoValue = "0";
    protected string valueString = "0";
    protected string baseFormatString = "n";
    protected string formatString = "n";
    protected string emptyValue = "0";
    protected int decimalPossibleChars = 27;
    protected int decimalPlaces = 2;
    protected Decimal minimum = new Decimal(-469762049, -590869294, 5421010, true, (byte) 0);
    protected Decimal maximum = new Decimal(-469762049, -590869294, 5421010, false, (byte) 0);
    private SpinType spinMode = SpinType.SpinValueBeforeDigit;
    protected UndoRedoManager manager = new UndoRedoManager();
    private string errorString = "Value is out of range.";
    private bool useDefaultValidation = true;
    private ErrorProvider errorProvider = new ErrorProvider();
    private int maxValueLength = int.MaxValue;
    protected CultureInfo cultureInfo;
    protected Decimal value;
    protected bool isLessThanZero;
    private Decimal lastValue;

    /// <summary>Gets or sets the validation string.</summary>
    /// <value>The validation string.</value>
    [Description("Gets or sets the validation string.")]
    [DefaultValue("Value is out of range.")]
    [Category("Behavior")]
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
        if (!(this.Value < this.Minimum) && !(this.Value > this.Maximum) || !this.UseDefaultValidation)
          return;
        this.errorProvider.SetError((Control) this, this.ValidationString);
      }
    }

    /// <summary>Gets or sets editor's spin type.</summary>
    [Category("Behavior")]
    [Description("Gets or sets editor's spin type.")]
    public SpinType SpinType
    {
      get
      {
        return this.spinMode;
      }
      set
      {
        this.spinMode = value;
        this.OnPropertyChanged("SpinType");
      }
    }

    /// <summary>Gets or sets editor's decimal places.</summary>
    [Description("Gets or sets editor's decimal places.")]
    [Category("Behavior")]
    [DefaultValue(2)]
    public virtual int DecimalPlaces
    {
      get
      {
        return this.decimalPlaces;
      }
      set
      {
        if (this.decimalPlaces == value || value < 0 || value >= 20)
          return;
        this.decimalPlaces = value;
        this.Value = new Decimal(0);
        this.formatString = this.baseFormatString + (object) this.DecimalPlaces;
        this.ParseValue();
        this.ValueString = StringUtility.GetValueString(this.Text, this.SeparatorChar, this.HasSeparator);
        this.OnPropertyChanged("DecimalPlaces");
      }
    }

    internal string FormatString
    {
      get
      {
        return this.formatString;
      }
    }

    /// <summary>Gets or sets editor's culture information.</summary>
    [Description("Gets or sets editor's culture information.")]
    [Category("Behavior")]
    public CultureInfo CultureInfo
    {
      get
      {
        return this.cultureInfo;
      }
      set
      {
        this.cultureInfo = value;
        this.ParseValue();
        this.OnPropertyChanged("CultureInfo");
      }
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public new int MaxLength
    {
      get
      {
        return base.MaxLength;
      }
      set
      {
        base.MaxLength = value;
      }
    }

    /// <summary>Gets or sets the maximum length of the value string.</summary>
    /// <value>The length of the max value.</value>
    internal int MaxValueLength
    {
      get
      {
        return this.maxValueLength;
      }
      set
      {
        if (this.maxValueLength < 0)
          return;
        this.maxValueLength = value;
        this.OnPropertyChanged("MaxValueLength");
      }
    }

    /// <summary>Gets the current separator character.</summary>
    [Category("Behavior")]
    public virtual string SeparatorChar
    {
      get
      {
        return StringUtility.GetDecimalSeparator(this.CultureInfo);
      }
    }

    /// <summary>Gets or sets editor's Minimum value.</summary>
    [Category("Behavior")]
    [DefaultValue(typeof (Decimal), "-99999999999999999999999999")]
    [Description("Gets or sets editor's Minimum value.")]
    public Decimal Minimum
    {
      get
      {
        return this.minimum;
      }
      set
      {
        Decimal num = this.Value;
        if (!(value <= this.Maximum))
          return;
        this.minimum = value;
        if (value >= num)
          this.Value = value;
        this.OnPropertyChanged("Minimum");
      }
    }

    /// <summary>Gets or sets editor's Maximum value.</summary>
    [Description("Gets or sets editor's Maximum value.")]
    [DefaultValue(typeof (Decimal), "99999999999999999999999999")]
    [Category("Behavior")]
    public Decimal Maximum
    {
      get
      {
        return this.maximum;
      }
      set
      {
        Decimal num = this.Value;
        if (!(value >= this.Minimum))
          return;
        this.maximum = value;
        if (value <= num)
          this.Value = num;
        this.OnPropertyChanged("Maximum");
      }
    }

    /// <summary>Gets editor's last value.</summary>
    [Category("Behavior")]
    public virtual Decimal LastValue
    {
      get
      {
        return this.lastValue;
      }
      internal set
      {
        this.lastValue = value;
      }
    }

    protected internal virtual bool HasSeparator
    {
      get
      {
        return this.DecimalPlaces > 0;
      }
    }

    /// <summary>Gets or sets editor's value.</summary>
    [DefaultValue(typeof (Decimal), "0")]
    [Category("Behavior")]
    [Description("Gets or sets editor's value.")]
    public virtual Decimal Value
    {
      get
      {
        this.value = Decimal.Parse(this.ValueString, (IFormatProvider) CultureInfo.InvariantCulture);
        if (this.isLessThanZero)
          this.value = -this.value;
        return this.value;
      }
      set
      {
        if (!(this.value != value))
          return;
        this.lastValue = this.value;
        this.value = value;
        this.isLessThanZero = !(this.value >= new Decimal(0));
        int num = this.DecimalPlaces;
        if (!this.HasSeparator)
          num = 0;
        this.ValueString = StringUtility.GetValueString(value.ToString("n" + (object) num, (IFormatProvider) CultureInfo.InvariantCulture), ".", this.HasSeparator);
        ValueChangingEditorEventArgs args = new ValueChangingEditorEventArgs(this.Value);
        this.OnValueChanging(args);
        if (args.Cancel)
        {
          this.Undo();
        }
        else
        {
          this.OnValueChanged(new ValueChangedEditorEventArgs(this.Value));
          this.OnPropertyChanged("Value");
        }
      }
    }

    internal string ValueString
    {
      get
      {
        return this.valueString;
      }
      set
      {
        this.valueString = value;
        this.ParseValue();
      }
    }

    /// <summary>Represents the ValueChanged event.</summary>
    [Category("Action")]
    [Description("Occurs when a value has changed")]
    public event ValueChangedEditorEventHandler ValueChanged;

    /// <summary>Represents the ValueChanging event.</summary>
    [Description("Occurs when a value is changing")]
    [Category("Action")]
    public event ValueChangingEditorEventHandler ValueChanging;

    /// <summary>Represents the ValidationFailed event.</summary>
    [Description("Occurs when a value is out of the given Minimum - Maximmum range.")]
    [Category("Action")]
    public event EventHandler ValidationFailed;

    /// <summary>Occurs when a property has changed.</summary>
    [Category("Action")]
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>Initializes a new instance of InputBase.</summary>
    public InputBase()
    {
      this.cultureInfo = CultureInfo.CurrentCulture;
      this.TextAlign = HorizontalAlignment.Right;
      this.TextChanged += new EventHandler(this.InputBase_TextChanged);
    }

    /// <summary>Gets the default validation error provider.</summary>
    /// <returns></returns>
    public ErrorProvider GetDefaultValidationErrorProvider()
    {
      return this.errorProvider;
    }

    /// <summary>Calls the ValidationFailed event.</summary>
    protected internal virtual void OnValidationFailed()
    {
      if (this.ValidationFailed == null)
        return;
      this.ValidationFailed((object) this, EventArgs.Empty);
    }

    /// <summary>Calls the ValueChanged event.</summary>
    protected internal virtual void OnValueChanged(ValueChangedEditorEventArgs args)
    {
      if (this.ValueChanged != null)
        this.ValueChanged((object) this, args);
      if (this.Value < this.Minimum || this.Value > this.Maximum)
      {
        if (this.UseDefaultValidation)
          this.errorProvider.SetError((Control) this, this.ValidationString);
        this.OnValidationFailed();
      }
      else
        this.errorProvider.Clear();
    }

    /// <summary>Calls the ValueChanging event.</summary>
    protected internal virtual void OnValueChanging(ValueChangingEditorEventArgs args)
    {
      if (this.ValueChanging == null)
        return;
      this.ValueChanging((object) this, args);
    }

    private void InputBase_TextChanged(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(this.Text) || string.IsNullOrEmpty(this.Name) || !this.Text.Equals(this.Name))
        return;
      this.ParseValue();
    }

    /// <summary>Creates a handle for the control.</summary>
    /// <exception cref="T:System.ObjectDisposedException">
    /// The object is in a disposed state.
    /// </exception>
    protected override void CreateHandle()
    {
      base.CreateHandle();
      this.ParseValue();
    }

    private void ResetSpinType()
    {
      this.SpinType = SpinType.SpinValueBeforeDigit;
    }

    private bool ShouldSerializeSpinType()
    {
      return this.SpinType != SpinType.SpinValueBeforeDigit;
    }

    /// <summary>Executes the Ctrl + C command.</summary>
    protected virtual void CtrlC()
    {
    }

    /// <summary>Executes the Ctrl + V command.</summary>
    protected virtual void CtrlV()
    {
    }

    /// <summary>Executes the Ctrl + X command.</summary>
    protected virtual void CtrlX()
    {
    }

    public bool IsCtrlDown()
    {
      return Control.ModifierKeys == Keys.Control;
    }

    /// <summary>Determines whether the shift key is pressed.</summary>
    public bool IsShiftDown()
    {
      return Control.ModifierKeys == Keys.Shift;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
      e.Handled = true;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (this.Readonly)
        base.OnKeyDown(e);
      else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
        base.OnKeyDown(e);
      else if (this.IsShiftDown() && e.KeyCode == Keys.Tab || e.KeyCode == Keys.Tab)
        base.OnKeyDown(e);
      else if (this.IsShiftDown() && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
        base.OnKeyDown(e);
      else if (this.IsCtrlDown() && e.KeyCode == Keys.A)
      {
        base.OnKeyDown(e);
        this.SelectAll();
      }
      else if (this.IsCtrlDown() && e.KeyCode == Keys.V)
      {
        this.CtrlV();
        e.Handled = true;
      }
      else if (this.IsCtrlDown() && e.KeyCode == Keys.X)
      {
        this.CtrlX();
        e.Handled = true;
      }
      else if (this.IsCtrlDown() && e.KeyCode == Keys.Z)
      {
        this.Undo();
        e.Handled = true;
      }
      else if (this.IsCtrlDown() && e.KeyCode == Keys.Y)
      {
        this.Redo();
        e.Handled = true;
      }
      else if (this.IsCtrlDown() && e.KeyCode == Keys.C)
      {
        this.CtrlC();
        e.Handled = true;
      }
      else
      {
        switch (e.KeyCode)
        {
          case Keys.Delete:
            this.Delete();
            e.Handled = true;
            break;
          case Keys.F2:
            this.SelectAll();
            this.Delete();
            e.Handled = true;
            break;
          case Keys.Back:
            this.Backspace();
            e.Handled = true;
            break;
          case Keys.End:
            if (!this.IsShiftDown())
            {
              this.EndKey();
              e.Handled = true;
              break;
            }
            break;
          case Keys.Home:
            if (!this.IsShiftDown())
            {
              this.HomeKey();
              e.Handled = true;
              break;
            }
            break;
          case Keys.Up:
            this.UpKey();
            e.Handled = true;
            break;
          case Keys.Down:
            this.DownKey();
            e.Handled = true;
            break;
        }
        if (this.SeparatorChar.Equals(",") && e.KeyCode == Keys.Oemcomma || e.KeyValue == 110)
        {
          int separatorPositionInText = StringUtility.GetSeparatorPositionInText(this.SeparatorChar, this.Text);
          if (separatorPositionInText >= 0 && separatorPositionInText + 1 < this.Text.Length)
            this.SelectionStart = separatorPositionInText + 1;
        }
        else if (this.SeparatorChar.Equals(".") && e.KeyValue == 190)
        {
          int separatorPositionInText = StringUtility.GetSeparatorPositionInText(this.SeparatorChar, this.Text);
          if (separatorPositionInText >= 0 && separatorPositionInText + 1 < this.Text.Length)
            this.SelectionStart = separatorPositionInText + 1;
        }
        else if (!this.SeparatorChar.Equals(".") && e.KeyValue == 190)
        {
          int separatorPositionInText = StringUtility.GetSeparatorPositionInText(this.SeparatorChar, this.Text);
          if (separatorPositionInText >= 0 && separatorPositionInText + 1 < this.Text.Length)
            this.SelectionStart = separatorPositionInText + 1;
        }
        if (e.KeyValue == 189 || e.KeyValue == 109)
        {
          Decimal num = this.value;
          this.isLessThanZero = !this.isLessThanZero;
          if (num != this.Value)
          {
            ValueChangingEditorEventArgs args = new ValueChangingEditorEventArgs(this.Value);
            this.OnValueChanging(args);
            if (args.Cancel)
            {
              this.Undo();
              return;
            }
            this.OnValueChanged(new ValueChangedEditorEventArgs(this.Value));
            this.OnPropertyChanged("Value");
          }
          this.ParseValue();
          this.SelectionStart = 0;
          e.Handled = true;
        }
        if (e.KeyValue == 67 || e.KeyCode == Keys.Shift)
          return;
        this.InsertNewCharacter(e);
        if (e.KeyCode == Keys.Return || e.KeyCode == Keys.Escape)
          base.OnKeyDown(e);
        else
          e.Handled = true;
      }
    }

    protected override void OnKeyPress(KeyPressEventArgs e)
    {
      base.OnKeyPress(e);
      e.Handled = true;
    }

    protected override void OnTextChanged(EventArgs e)
    {
      base.OnTextChanged(e);
    }

    /// <summary>Deletes the selected value.</summary>
    public virtual void Delete()
    {
    }

    /// <summary>Simulates BackSpace.</summary>
    public virtual void Backspace()
    {
    }

    private void InsertNewCharacter(KeyEventArgs e)
    {
      char ch = '0';
      int num1 = e.KeyValue - (int) ch;
      bool flag = false;
      if (num1 < 10 && num1 >= 0)
      {
        string @string = num1.ToString();
        if (@string != null)
        {
          this.Insert(@string);
          flag = true;
        }
      }
      if (flag)
        return;
      int num2 = -1;
      switch (e.KeyCode)
      {
        case Keys.NumPad0:
          num2 = 0;
          break;
        case Keys.NumPad1:
          num2 = 1;
          break;
        case Keys.NumPad2:
          num2 = 2;
          break;
        case Keys.NumPad3:
          num2 = 3;
          break;
        case Keys.NumPad4:
          num2 = 4;
          break;
        case Keys.NumPad5:
          num2 = 5;
          break;
        case Keys.NumPad6:
          num2 = 6;
          break;
        case Keys.NumPad7:
          num2 = 7;
          break;
        case Keys.NumPad8:
          num2 = 8;
          break;
        case Keys.NumPad9:
          num2 = 9;
          break;
      }
      if (num2 < 0)
        return;
      this.Insert(num2.ToString());
    }

    /// <summary>Removes a range of characters.</summary>
    /// <param name="start"></param>
    /// <param name="length"></param>
    /// <param name="text"></param>
    /// <param name="separatorChar"></param>
    /// <param name="updateText"></param>
    /// <returns></returns>
    protected int RemoveRange(int start, int length, string text, string separatorChar, bool updateText)
    {
      int num1 = start;
      int num2 = length;
      int num3 = 0;
      Decimal num4 = this.Value;
      if (num2 == 0 && this.ValueString.Length < this.decimalPossibleChars - 1)
        return num3;
      int num5 = StringUtility.GetSeparatorPositionInText(separatorChar, text);
      if (!updateText)
        num5 = StringUtility.GetSeparatorPositionInText(".", text);
      if (num5 < 0 && !this.HasSeparator && text.Length > 1)
        num5 = text.Length;
      if (this.ValueString.Length == this.decimalPossibleChars - 1 && start + length < num5)
        ++num2;
      string str = "";
      for (int index = 0; index < text.Length; ++index)
      {
        if (index < num1 || index >= num1 + num2)
          str += (string) (object) text[index];
        else if (text[index].ToString().Equals(separatorChar))
          str += separatorChar;
        else if (index > num5)
          str += "0";
        else if (char.IsDigit(text[index]))
          ++num3;
      }
      if (str.Length == 0)
        str = "0";
      if (updateText)
        this.Text = str;
      else
        this.ValueString = str;
      return num3;
    }

    /// <summary>Gets editor's text.</summary>
    /// <returns></returns>
    protected virtual string GetPureText()
    {
      return this.Text;
    }

    /// <summary>Parses editor's value.</summary>
    /// <returns></returns>
    protected virtual bool ParseValue()
    {
      return false;
    }

    /// <summary>Formats editor's text.</summary>
    /// <returns></returns>
    protected virtual bool SetFormattedText()
    {
      return false;
    }

    /// <summary>Inserts a string into the editor.</summary>
    /// <param name="insertion"></param>
    public virtual void Insert(string insertion)
    {
    }

    /// <summary>Makes undo of the current value.</summary>
    public virtual void Undo()
    {
      this.valueString = this.undoValue;
      this.ParseValue();
    }

    public virtual void Redo()
    {
    }

    /// <summary>Moves the cursor one position left.</summary>
    public void LeftKey()
    {
      if (this.SelectionStart - 1 < 0)
        return;
      if (this.SelectionLength == 0)
        --this.SelectionStart;
      else if (this.SelectionStart - this.SelectionLength >= 0)
        this.SelectionStart -= this.SelectionLength;
      this.SelectionLength = 0;
    }

    protected override void OnMouseWheel(MouseEventArgs e)
    {
      base.OnMouseWheel(e);
      if (e.Delta > 0)
        this.SpinValue(true);
      else
        this.SpinValue(false);
    }

    /// <summary>Increases or decreases the current value.</summary>
    /// <param name="spinUp"></param>
    public void SpinValue(bool spinUp)
    {
      this.LastValue = this.Value;
      EditorState state = new EditorState();
      if (this.manager.GetState() == null)
      {
        state.SelectionLength = this.SelectionLength;
        state.SelectionStart = this.SelectionStart;
        state.StateType = EditorStateType.Insert;
        state.Text = this.Text;
        state.Value = this.Value;
        this.manager.SaveChanges(state);
      }
      switch (this.spinMode)
      {
        case SpinType.None:
          return;
        case SpinType.SpinDigit:
          this.SpinDigit(spinUp);
          break;
        case SpinType.SpinValueBeforeDigit:
          this.SpinValueBeforeDigit(spinUp);
          break;
        case SpinType.SpinDigitWithWrap:
          this.SpinDigitWithWrap(spinUp);
          break;
      }
      ValueChangingEditorEventArgs args = new ValueChangingEditorEventArgs(this.Value);
      this.OnValueChanging(args);
      if (args.Cancel)
      {
        this.Undo();
      }
      else
      {
        this.OnValueChanged(new ValueChangedEditorEventArgs(this.Value));
        this.manager.SaveChanges(new EditorState()
        {
          SelectionLength = this.SelectionLength,
          SelectionStart = this.SelectionStart,
          StateType = EditorStateType.Insert,
          Text = this.Text,
          Value = this.Value
        });
      }
    }

    protected virtual void SpinDigit(bool spinUp)
    {
      if (this.SelectionStart < 0 || this.SelectionStart > this.Text.Length)
        return;
      int selectionStart = this.SelectionStart;
      this.ValueString = StringUtility.GetValueString(this.Text, this.SeparatorChar, this.HasSeparator);
      int selectionInValue = StringUtility.GetSelectionInValue(selectionStart, this.Text, this.SeparatorChar, this.HasSeparator);
      if (selectionInValue > 0)
        --selectionInValue;
      char c = this.ValueString[selectionInValue];
      if (!char.IsDigit(c))
      {
        if (selectionInValue > 0)
        {
          c = this.ValueString[selectionInValue - 1];
          --selectionInValue;
        }
        if (!char.IsDigit(c) && this.ValueString.Length > 0 && this.ValueString[0].Equals('.'))
        {
          c = '0';
          this.ValueString = this.ValueString.Insert(0, "0");
        }
      }
      if (!char.IsDigit(c))
        return;
      int num1 = int.Parse(c.ToString(), (IFormatProvider) CultureInfo.InvariantCulture);
      int num2 = this.SpinIntDigit(spinUp, selectionInValue, num1);
      char[] charArray = this.ValueString.ToCharArray();
      char result;
      if (char.TryParse(num2.ToString(), out result))
        charArray[selectionInValue] = result;
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < charArray.Length; ++index)
        stringBuilder.Append(charArray[index]);
      this.ValueString = stringBuilder.ToString();
      if (this.Value > this.Maximum)
        this.Value = this.Maximum;
      if (this.Value < this.Minimum)
        this.Value = this.Minimum;
      this.ParseValue();
      this.SelectionStart = selectionStart;
    }

    private int SpinIntDigit(bool spinUp, int selectionInValue, int value)
    {
      if (this.spinMode == SpinType.SpinDigit)
      {
        if (spinUp)
        {
          if (value < 9)
            ++value;
        }
        else if (value > 0)
        {
          if (selectionInValue != 0 || value != 1)
            --value;
          else if (this.baseFormatString.Equals("d"))
            --value;
        }
      }
      else if (spinUp)
      {
        if (value < 9)
          ++value;
        else
          value = selectionInValue > 0 || selectionInValue == 0 && this.Value - new Decimal(9) <= new Decimal(99999999, 0, 0, false, (byte) 8) ? 0 : 1;
      }
      else if (value > 0)
      {
        if (selectionInValue != 0 || value != 1 || !(Decimal.op_Decrement(this.Value) > new Decimal(99999999, 0, 0, false, (byte) 8)))
          --value;
        else
          value = 9;
      }
      else
        value = 9;
      return value;
    }

    /// <summary>Spins the digit with wrap.</summary>
    /// <param name="spinUp">if set to <c>true</c> [spin up].</param>
    protected virtual void SpinDigitWithWrap(bool spinUp)
    {
      this.SpinDigit(spinUp);
    }

    /// <summary>Spins the value before digit.</summary>
    /// <param name="spinUp">if set to <c>true</c> [spin up].</param>
    protected virtual void SpinValueBeforeDigit(bool spinUp)
    {
      if (this.SelectionStart < 0 || this.SelectionStart > this.Text.Length)
        return;
      int selectionStart = this.SelectionStart;
      this.ValueString = StringUtility.GetValueString(this.Text, this.SeparatorChar, this.HasSeparator);
      if (this.ValueString.Length > 0 && this.ValueString[0].Equals('.'))
        this.ValueString = this.ValueString.Insert(0, "0");
      string valueString1 = this.ValueString;
      if (valueString1.Length <= 0)
        return;
      int length = Math.Max(1, StringUtility.GetSelectionInValue(selectionStart, this.GetPureText(), this.SeparatorChar, this.HasSeparator));
      if (length > valueString1.Length)
        length = valueString1.Length;
      string text = valueString1[0].ToString();
      if (length <= valueString1.Length)
        text = this.ValueString.Substring(0, length);
      string valueString2 = StringUtility.GetValueString(text, ".", this.HasSeparator);
      if (length > 0)
        --length;
      char c = this.ValueString[length];
      if (!char.IsDigit(c))
      {
        if (length > 0)
        {
          c = this.ValueString[length - 1];
          --length;
        }
        if (!char.IsDigit(c) && this.ValueString.Length > 0 && this.ValueString[0].Equals('.'))
          this.ValueString = this.ValueString.Insert(0, "0");
      }
      string valueString3 = StringUtility.GetValueString(this.ValueString.Substring(length + 1), ".", this.HasSeparator);
      Decimal num1 = new Decimal(0);
      if (!string.IsNullOrEmpty(valueString2))
        num1 = Decimal.Parse(valueString2, (IFormatProvider) CultureInfo.InvariantCulture);
      int num2 = this.ValueString.IndexOf('.');
      if (num2 < 0)
        num2 = this.ValueString.Length + 1;
      Decimal num3 = new Decimal(1);
      if (num2 < length)
        num3 = new Decimal(1) / (Decimal) Math.Pow(10.0, (double) (length - num2));
      if (spinUp)
        num1 += num3;
      else if (num1 > new Decimal(0) && (length != 0 || !(num1 == new Decimal(1))))
        num1 -= num3;
      this.ValueString = num1.ToString() + valueString3;
      if (this.Value > this.Maximum)
        this.Value = this.Maximum;
      if (this.Value < this.Minimum)
        this.Value = this.Minimum;
      if (this.baseFormatString.Equals("d"))
      {
        long valueInDecimalPlaces = StringUtility.GetMaximumValueInDecimalPlaces(this.DecimalPlaces);
        if (this.Value > (Decimal) valueInDecimalPlaces)
          this.Value = (Decimal) valueInDecimalPlaces;
      }
      this.ParseValue();
      this.SelectionStart = selectionStart;
    }

    /// <summary>Simulates pressing of up arrow key.</summary>
    public virtual void UpKey()
    {
      if (this.isLessThanZero)
        this.SpinValue(false);
      else
        this.SpinValue(true);
    }

    /// <summary>Simulates pressing of down arrow key.</summary>
    public virtual void DownKey()
    {
      if (this.isLessThanZero)
        this.SpinValue(true);
      else
        this.SpinValue(false);
    }

    /// <summary>Simulates pressing of down key.</summary>
    public void RightKey()
    {
      if (this.Text.Length <= this.SelectionStart)
        return;
      if (this.SelectionLength == 0)
        ++this.SelectionStart;
      else
        this.SelectionStart += this.SelectionLength;
      this.SelectionLength = 0;
    }

    /// <summary>Simulates pressing of Home key.</summary>
    public void HomeKey()
    {
      this.SelectionStart = 0;
      this.SelectionLength = 0;
    }

    /// <summary>Simulates pressing of End key.</summary>
    public void EndKey()
    {
      this.SelectionStart = this.Text.Length;
      this.SelectionLength = 0;
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
