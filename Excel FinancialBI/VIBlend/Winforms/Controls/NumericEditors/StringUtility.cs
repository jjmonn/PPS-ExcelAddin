// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.StringUtility
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Globalization;
using System.Text;

namespace VIBlend.WinForms.Controls
{
  public class StringUtility
  {
    public static bool ContainsDigits(string text)
    {
      for (int index = 0; index < text.Length; ++index)
      {
        if (char.IsDigit(text[index]))
          return true;
      }
      return false;
    }

    /// <summary>Gets a decimal separator specific to a given culture.</summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static string GetDecimalSeparator(CultureInfo info)
    {
      return info.NumberFormat.NumberDecimalSeparator;
    }

    /// <summary>
    /// Gets a currency separator specific to a given culture.
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static string GetCurrencySeparator(CultureInfo info)
    {
      return info.NumberFormat.CurrencyDecimalSeparator;
    }

    /// <summary>
    /// Gets a percentage separator specific to a given culture.
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static string GetPercentageSeparator(CultureInfo info)
    {
      return info.NumberFormat.PercentDecimalSeparator;
    }

    /// <summary>Gets the position of the separator in a string.</summary>
    /// <param name="separator"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static int GetSeparatorPositionInText(string separator, string text)
    {
      int num = -1;
      for (int index = 0; index < text.Length; ++index)
      {
        if (text[index].ToString().Equals(separator))
        {
          num = index;
          break;
        }
      }
      return num;
    }

    private static void BuildValueFromText(string str, string separator, StringBuilder builder, bool hasSeparator)
    {
      for (int i = 0; i < str.Length; ++i)
        StringUtility.BuildCorrectCharacter(str, separator, builder, i, hasSeparator);
    }

    private static void BuildCorrectCharacter(string str, string separator, StringBuilder builder, int i, bool hasSeparator)
    {
      if (char.IsDigit(str[i]))
      {
        builder.Append(str[i]);
      }
      else
      {
        if (!str[i].ToString().Equals(separator) || !hasSeparator)
          return;
        builder.Append(".");
      }
    }

    /// <summary>Gets selection position from the value string.</summary>
    /// <param name="valuePosition"></param>
    /// <param name="text"></param>
    /// <param name="separator"></param>
    /// <param name="hasSeparator"></param>
    /// <returns></returns>
    public static int GetSelectionPositionFromValuePosition(int valuePosition, string text, string separator, bool hasSeparator)
    {
      int num1 = 0;
      int num2 = 0;
      for (int index = 0; index <= text.Length; ++index)
      {
        if (num2 >= valuePosition)
        {
          num1 = index;
          break;
        }
        if (index < text.Length && (char.IsDigit(text[index]) || hasSeparator && text[index].ToString().Equals(separator)))
          ++num2;
        num1 = index;
      }
      return num1;
    }

    /// <summary>Gets the maximum value length.</summary>
    /// <param name="decimalPlaces"></param>
    /// <returns></returns>
    public static long GetMaximumValueInDecimalPlaces(int decimalPlaces)
    {
      string s = "";
      for (int index = 0; index < decimalPlaces; ++index)
        s += "9";
      return long.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    /// <summary>Gets the selection position in the value string.</summary>
    /// <param name="selectionPosition"></param>
    /// <param name="text"></param>
    /// <param name="separator"></param>
    /// <param name="hasSeparator"></param>
    /// <returns></returns>
    public static int GetSelectionInValue(int selectionPosition, string text, string separator, bool hasSeparator)
    {
      int num = 0;
      for (int index = 0; index < text.Length && index < selectionPosition; ++index)
      {
        if (char.IsDigit(text[index]) || hasSeparator && text[index].ToString().Equals(separator))
          ++num;
      }
      return num;
    }

    /// <summary>Gets the selection length in the value string.</summary>
    /// <param name="selectionPosition"></param>
    /// <param name="selectionLength"></param>
    /// <param name="text"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static int GetSelectionLengthInValue(int selectionPosition, int selectionLength, string text, string separator)
    {
      int num = 0;
      for (int index = 0; index < text.Length && index < selectionPosition + selectionLength; ++index)
      {
        if (selectionLength > 0 && index >= selectionPosition && char.IsDigit(text[index]) || index >= selectionPosition && text[index].ToString().Equals(separator))
          ++num;
      }
      return num;
    }

    /// <summary>Gets the substring before the separator character.</summary>
    /// <param name="text"></param>
    /// <param name="separator"></param>
    /// <param name="hasSeparator"></param>
    /// <returns></returns>
    public static string GetStringToSeparator(string text, string separator, bool hasSeparator)
    {
      int separatorPositionInText = StringUtility.GetSeparatorPositionInText(separator, text);
      return StringUtility.GetValueString(text.Substring(0, separatorPositionInText), separator, hasSeparator);
    }

    /// <summary>Gets the value string in a text.</summary>
    /// <param name="text"></param>
    /// <param name="separator"></param>
    /// <param name="hasSeparator"></param>
    /// <returns></returns>
    public static string GetValueString(string text, string separator, bool hasSeparator)
    {
      string separator1 = separator;
      StringBuilder builder = new StringBuilder();
      StringUtility.BuildValueFromText(text, separator1, builder, hasSeparator);
      return builder.ToString();
    }

    /// <summary>Gets the percentage value string in a text.</summary>
    /// <param name="text"></param>
    /// <param name="separator"></param>
    /// <param name="hasSeparator"></param>
    /// <returns></returns>
    public static string GetPercentageValueString(string text, string separator, bool hasSeparator)
    {
      string separator1 = separator;
      StringBuilder builder = new StringBuilder();
      StringUtility.BuildValueFromText(text, separator1, builder, hasSeparator);
      return StringUtility.MoveSeparatorTwoPositionsLeft(builder.ToString());
    }

    private static string MoveSeparatorTwoPositionsRight(string input)
    {
      return StringUtility.MoveSeparatorOnePositionRight(StringUtility.MoveSeparatorOnePositionRight(input));
    }

    private static string MoveSeparatorOnePositionRight(string input)
    {
      int separatorPosition;
      int afterDotDigitPos;
      input = StringUtility.GetNewSeparatorPosition(input, out separatorPosition, out afterDotDigitPos);
      if (afterDotDigitPos < 0)
        return input.Substring(0, separatorPosition) + (object) '0' + input.Substring(separatorPosition);
      return input.Substring(0, separatorPosition) + input.Substring(separatorPosition + 1, afterDotDigitPos - separatorPosition) + (object) '.' + input.Substring(afterDotDigitPos + 1);
    }

    private static string GetNewSeparatorPosition(string input, out int separatorPosition, out int afterDotDigitPos)
    {
      separatorPosition = input.IndexOf('.');
      if (separatorPosition < 0)
      {
        separatorPosition = input.Length;
        input += (string) (object) '.';
      }
      char[] anyOf = new char[10]{ '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
      afterDotDigitPos = input.IndexOfAny(anyOf, separatorPosition);
      return input;
    }

    private static string MoveSeparatorTwoPositionsLeft(string input)
    {
      if (input.IndexOf('.') < 0)
        input += (string) (object) '.';
      string input1 = string.Empty;
      foreach (int num in input)
        input1 = num.ToString() + input1;
      return StringUtility.GetResultInReversedOrder(StringUtility.MoveSeparatorTwoPositionsRight(input1), string.Empty);
    }

    private static string GetResultInReversedOrder(string reversedResult, string result)
    {
      foreach (int num in reversedResult)
        result = num.ToString() + result;
      return result;
    }

    /// <summary>
    /// Gets the number of digits after the separator character.
    /// </summary>
    /// <param name="separator"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static int GetDigitsCountAfterSeparator(string separator, string text)
    {
      int num = 0;
      for (int index = Math.Max(0, StringUtility.GetSeparatorPositionInText(separator, text)); index < text.Length; ++index)
      {
        if (char.IsDigit(text[index]))
          ++num;
      }
      return num;
    }

    /// <summary>Gets the insert type.</summary>
    /// <param name="positionInText"></param>
    /// <param name="separator"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    public static InsertType GetInsertType(int positionInText, string separator, string text)
    {
      InsertType insertType = InsertType.BeforeSeparator;
      int separatorPositionInText = StringUtility.GetSeparatorPositionInText(separator, text);
      if (separatorPositionInText != -1 && separatorPositionInText < positionInText)
        insertType = InsertType.AfterSeparator;
      return insertType;
    }

    /// <summary>Gets the insert type by position.</summary>
    /// <param name="positionInValue"></param>
    /// <param name="separator"></param>
    /// <param name="text"></param>
    /// <param name="hasSeparator"></param>
    /// <returns></returns>
    public static InsertType GetInsertTypeByPositionInValue(int positionInValue, string separator, string text, bool hasSeparator)
    {
      InsertType insertType = InsertType.BeforeSeparator;
      int digitsToSeparator = StringUtility.GetDigitsToSeparator(0, StringUtility.GetValueString(text, separator, hasSeparator));
      if (positionInValue > digitsToSeparator)
        insertType = InsertType.AfterSeparator;
      return insertType;
    }

    /// <summary>Gets the digits count before the separator character.</summary>
    /// <param name="digitsToSeparator"></param>
    /// <param name="valueString"></param>
    /// <returns></returns>
    public static int GetDigitsToSeparator(int digitsToSeparator, string valueString)
    {
      if (valueString.IndexOf('.') < 0)
        return valueString.Length;
      for (int index = 0; index < valueString.Length; ++index)
      {
        if (valueString[index].ToString().Equals("."))
        {
          digitsToSeparator = index;
          break;
        }
      }
      return digitsToSeparator;
    }
  }
}
