// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.vFixedPointEditor
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
  [Description("Provides an editable text field with cutomizable features.")]
  [ToolboxBitmap(typeof (vFixedPointEditor), "ControlIcons.vFixedPointEditor.ico")]
  [ToolboxItem(true)]
  [Designer("VIBlend.WinForms.Controls.Design.vDesignerBase, VIBlend.WinForms.Controls.Design, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf")]
  public class vFixedPointEditor : InputBase
  {
    private string savedValueString = "";
    private bool isBackSpace;

    public override string SeparatorChar
    {
      get
      {
        return this.CultureInfo.NumberFormat.NumberDecimalSeparator;
      }
    }

    static vFixedPointEditor()
    {
      TypeDescriptor.AddProvider((TypeDescriptionProvider) new DynamicDesignerProvider(TypeDescriptor.GetProvider(typeof (object))), typeof (object));
    }

    /// <summary>Initializes a new instance of fixed point editor.</summary>
    public vFixedPointEditor()
    {
      this.baseFormatString = "F";
      this.formatString = this.baseFormatString + (object) this.DecimalPlaces;
      this.InitialInitialization();
    }

    private void InitialInitialization()
    {
      this.SubsctractValueCharsUntilParseIsDone(false, "0");
    }

    private bool SubsctractValueCharsUntilParseIsDone(bool res, string NullValue)
    {
      while (!res)
      {
        res = this.ParseValue();
        bool shouldBreak = false;
        this.ParseEmptyValue(ref res, NullValue, ref shouldBreak);
        if (res)
          shouldBreak = true;
        if (!shouldBreak)
          this.valueString = this.valueString.Substring(0, this.valueString.Length - 1);
        else
          break;
      }
      return res;
    }

    private void ParseEmptyValue(ref bool res, string NullValue, ref bool shouldBreak)
    {
      if (this.valueString.Length != 0 && !(this.Value == new Decimal(0)))
        return;
      this.valueString = NullValue;
      res = this.ParseValue();
      shouldBreak = true;
    }

    private bool TryParse()
    {
      int digitsToSeparator = 0;
      bool tryParse = false;
      Decimal result;
      if (!Decimal.TryParse(this.valueString, NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        tryParse = false;
      this.IsValueParsed(ref digitsToSeparator, ref tryParse);
      return tryParse;
    }

    protected override bool ParseValue()
    {
      bool flag = false;
      if (this.TryParse())
      {
        if (string.IsNullOrEmpty(this.valueString))
          return flag;
        flag = this.SetFormattedText();
      }
      if (this.isLessThanZero)
      {
        if (this.Text.Length > 0 && this.Text[0].ToString() != "-")
          this.Text = this.Text.Insert(0, "-");
      }
      else if (this.Text.Length > 0 && this.Text[0].ToString() == "-")
        this.Text = this.Text.Substring(1);
      return flag;
    }

    private void IsValueParsed(ref int digitsToSeparator, ref bool tryParse)
    {
      digitsToSeparator = StringUtility.GetDigitsToSeparator(digitsToSeparator, this.valueString);
      if (digitsToSeparator >= this.decimalPossibleChars - this.valueString.Length + digitsToSeparator)
        return;
      tryParse = true;
    }

    protected override bool SetFormattedText()
    {
      try
      {
        string format = "{0:" + this.formatString + "}";
        Decimal num1 = Decimal.Parse(this.valueString, (IFormatProvider) CultureInfo.InvariantCulture);
        this.Text = string.Format((IFormatProvider) this.CultureInfo, format, new object[1]{ (object) num1 });
        Decimal num2 = num1;
        if (this.isLessThanZero)
        {
          Decimal num3 = -num2;
        }
      }
      catch (Exception ex)
      {
        this.Undo();
        return false;
      }
      return true;
    }

    public override void Undo()
    {
      EditorState editorState = this.manager.Undo();
      if (editorState == null)
        return;
      this.Value = editorState.Value;
      this.SelectionStart = editorState.SelectionStart;
      this.SelectionLength = editorState.SelectionLength;
    }

    public override void Redo()
    {
      EditorState editorState = this.manager.Redo();
      if (editorState == null)
        return;
      this.Value = editorState.Value;
      this.SelectionStart = editorState.SelectionStart;
      this.SelectionLength = editorState.SelectionLength;
    }

    private void DeleteDigit(int position)
    {
      this.ValueString = StringUtility.GetValueString(this.Text, this.SeparatorChar, this.HasSeparator);
      string valueString = this.ValueString;
      if (position >= valueString.Length || position < 0)
        return;
      char ch = valueString[position];
      bool flag = false;
      if (ch.Equals('.'))
        --position;
      InsertType byPositionInValue = StringUtility.GetInsertTypeByPositionInValue(position, this.SeparatorChar, this.Text, this.HasSeparator);
      Decimal result;
      if (Decimal.TryParse(valueString, out result))
      {
        Decimal num = new Decimal(1410065407, 2, 0, false, (byte) 10);
        if (result <= num && byPositionInValue == InsertType.BeforeSeparator)
          flag = true;
      }
      if (byPositionInValue == InsertType.AfterSeparator)
        flag = true;
      if (!flag)
        this.ValueString = valueString.Substring(0, position) + valueString.Substring(position + 1);
      else
        this.ValueString = valueString.Substring(0, position) + "0" + valueString.Substring(position + 1);
      this.ParseValue();
    }

    private void InsertDigit(string digit, int position)
    {
      this.ValueString = StringUtility.GetValueString(this.Text, this.SeparatorChar, this.HasSeparator);
      string valueString = this.ValueString;
      if (this.DecimalPlaces > 0 && position >= valueString.Length)
        position = valueString.Length - 1;
      char ch = char.MinValue;
      if (position < valueString.Length)
        ch = valueString[position];
      bool flag1 = false;
      bool flag2 = false;
      if (ch.Equals('.'))
      {
        --position;
        flag2 = true;
      }
      InsertType byPositionInValue = StringUtility.GetInsertTypeByPositionInValue(position, this.SeparatorChar, this.Text, this.HasSeparator);
      Decimal result;
      if (Decimal.TryParse(valueString, out result))
      {
        Decimal num = new Decimal(1410065407, 2, 0, false, (byte) 10);
        string format = "{0:" + this.formatString + "}";
        string.Format((IFormatProvider) this.CultureInfo, format, new object[1]
        {
          (object) result
        });
        string.Format((IFormatProvider) this.CultureInfo, format, new object[1]
        {
          (object) this.Maximum
        });
        if (result <= num && byPositionInValue == InsertType.BeforeSeparator)
          flag1 = true;
        else if (flag2)
          ++position;
      }
      if (byPositionInValue == InsertType.AfterSeparator)
        flag1 = true;
      if ((int) ch != 46 && this.ValueString.Length >= this.decimalPossibleChars - 1)
        flag1 = true;
      if (flag1 || this.ValueString.Length >= this.MaxValueLength)
      {
        if (position >= this.decimalPossibleChars - 1 && this.DecimalPlaces == 0)
          position = this.decimalPossibleChars - 2;
        if (position >= this.MaxValueLength)
          position = this.MaxValueLength;
        string str1 = valueString.Substring(0, position);
        string str2 = digit;
        string str3 = "";
        if (position + 1 < valueString.Length)
          str3 = valueString.Substring(position + 1);
        this.ValueString = str1 + str2 + str3;
      }
      else
        this.ValueString = valueString.Substring(0, position) + digit + valueString.Substring(position);
      this.ParseValue();
    }

    private void Validate()
    {
    }

    protected override void CtrlC()
    {
      int selectionStart = this.SelectionStart;
      this.savedValueString = StringUtility.GetValueString(this.Text.Substring(this.SelectionStart, this.SelectionLength), this.SeparatorChar, this.HasSeparator);
      if (this.savedValueString.Contains("."))
        this.savedValueString = this.savedValueString.Remove(this.savedValueString.IndexOf("."), 1);
      base.CtrlC();
    }

    protected override void CtrlV()
    {
      if (this.savedValueString.Length > 0)
      {
        int selectionStart = this.SelectionStart;
        StringUtility.GetSelectionInValue(selectionStart, this.Text, this.SeparatorChar, this.HasSeparator);
        StringUtility.GetSelectionLengthInValue(selectionStart, this.SelectionLength, this.Text, ".");
        this.isBackSpace = true;
        this.Delete(false);
        this.isBackSpace = false;
        this.Insert(this.savedValueString);
      }
      base.CtrlV();
    }

    protected override void CtrlX()
    {
      base.CtrlX();
      this.CtrlC();
      this.Delete();
    }

    public override void Insert(string insertion)
    {
      if (this.SelectionStart == this.Text.Length && this.DecimalPlaces > 0 || this.DecimalPlaces == 0 && this.SelectionStart == this.Text.Length && this.ValueString.Length == this.MaxValueLength)
        return;
      if (insertion.Equals("-"))
      {
        this.ParseValue();
      }
      else
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
        for (int index = 0; index < insertion.Length; ++index)
        {
          if (!char.IsDigit(insertion[index]))
            return;
        }
        Decimal num1 = this.Value;
        string decimalSeparator = StringUtility.GetDecimalSeparator(this.CultureInfo);
        int selectionStart = this.SelectionStart;
        int selectionLength = this.SelectionLength;
        this.ValueString = StringUtility.GetValueString(this.Text, decimalSeparator, this.HasSeparator);
        int selectionInValue = StringUtility.GetSelectionInValue(selectionStart, this.Text, this.SeparatorChar, this.HasSeparator);
        int selectionLengthInValue = StringUtility.GetSelectionLengthInValue(selectionStart, selectionLength, this.Text, this.SeparatorChar);
        StringUtility.GetDigitsToSeparator(0, this.ValueString);
        bool flag = false;
        if (this.DecimalPlaces > 0 && selectionInValue >= this.ValueString.Length)
          --selectionInValue;
        if (this.DecimalPlaces > 0 && (int) this.ValueString[selectionInValue] == 46)
        {
          InsertType byPositionInValue = StringUtility.GetInsertTypeByPositionInValue(selectionInValue, this.SeparatorChar, this.Text, this.HasSeparator);
          Decimal result;
          if (Decimal.TryParse(this.ValueString, out result))
          {
            Decimal num2 = new Decimal(1410065407, 2, 0, false, (byte) 10);
            if (result <= num2 && byPositionInValue == InsertType.BeforeSeparator)
              flag = true;
          }
        }
        this.RemoveRange(selectionInValue, selectionLengthInValue, this.ValueString, this.SeparatorChar, false);
        for (int index = 0; index < insertion.Length; ++index)
          this.InsertDigit(insertion[index].ToString(), selectionInValue + index);
        ValueChangingEditorEventArgs args = new ValueChangingEditorEventArgs(this.Value);
        this.OnValueChanging(args);
        if (args.Cancel)
        {
          this.Undo();
        }
        else
        {
          this.OnValueChanged(new ValueChangedEditorEventArgs(this.Value));
          if (flag)
            --selectionInValue;
          int fromValuePosition = StringUtility.GetSelectionPositionFromValuePosition(selectionInValue + 1, this.Text, this.SeparatorChar, this.HasSeparator);
          if (this.ValueString.Length == selectionInValue + 1)
            this.SelectionStart = this.Text.Length;
          else
            this.SelectionStart = fromValuePosition;
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
    }

    private void SetPosition(int position)
    {
      vFixedPointEditor fixedPointEditor;
      int num;
      for (; this.Text.Length - this.SelectionStart != position && this.SelectionStart + 1 <= this.Text.Length; fixedPointEditor.SelectionStart = num)
      {
        fixedPointEditor = this;
        num = fixedPointEditor.SelectionStart + 1;
      }
    }

    private void Delete(bool deleteWithoutSelection)
    {
      this.LastValue = this.Value;
      int selectionStart = this.SelectionStart;
      int selectionLength = this.SelectionLength;
      this.ValueString = StringUtility.GetValueString(this.Text, this.SeparatorChar, this.HasSeparator);
      int selectionInValue = StringUtility.GetSelectionInValue(selectionStart, this.Text, this.SeparatorChar, this.HasSeparator);
      int num = StringUtility.GetSelectionLengthInValue(selectionStart, selectionLength, this.Text, this.SeparatorChar);
      if (deleteWithoutSelection)
        num = Math.Max(1, num);
      StringUtility.GetDigitsToSeparator(0, this.ValueString);
      if (selectionInValue >= this.ValueString.Length)
        --selectionInValue;
      this.RemoveRange(selectionInValue, num, this.ValueString, ".", false);
      this.ParseValue();
      ValueChangingEditorEventArgs args = new ValueChangingEditorEventArgs(this.Value);
      this.OnValueChanging(args);
      if (args.Cancel)
      {
        this.Undo();
      }
      else
      {
        this.OnValueChanged(new ValueChangedEditorEventArgs(this.Value));
        int fromValuePosition = StringUtility.GetSelectionPositionFromValuePosition(selectionInValue, this.Text, this.SeparatorChar, this.HasSeparator);
        this.SelectionStart = fromValuePosition;
        if (fromValuePosition < this.valueString.IndexOf('.') || this.isBackSpace)
          return;
        ++this.SelectionStart;
      }
    }

    protected override void OnMouseUp(MouseEventArgs mevent)
    {
      base.OnMouseUp(mevent);
      this.manager.SaveChanges(new EditorState()
      {
        SelectionLength = this.SelectionLength,
        SelectionStart = this.SelectionStart,
        StateType = EditorStateType.Selection,
        Text = this.Text,
        Value = this.Value
      });
    }

    public override void Delete()
    {
      if (this.SelectionStart == this.TextBox.TextLength)
        return;
      this.manager.SaveChanges(new EditorState()
      {
        SelectionLength = this.SelectionLength,
        SelectionStart = this.SelectionStart,
        StateType = EditorStateType.Delete,
        Text = this.Text,
        Value = this.Value
      });
      this.Delete(true);
    }

    public override void Backspace()
    {
      if (this.SelectionStart == 0 && this.SelectionLength == 0)
        return;
      this.isBackSpace = true;
      if (this.SelectionStart > 0 && this.SelectionLength == 0)
        --this.SelectionStart;
      if (this.SelectionStart > 0 && this.SelectionLength == 0 && (this.Text.Length > this.SelectionStart && !char.IsDigit(this.Text[this.SelectionStart])) && !this.Text[this.SelectionStart].ToString().Equals(this.SeparatorChar))
        --this.SelectionStart;
      this.Delete();
      this.isBackSpace = false;
    }
  }
}
