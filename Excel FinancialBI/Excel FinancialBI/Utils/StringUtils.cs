using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  static class StringUtils
  {
    public static bool ContainChars(string p_strToCheck, string p_charList)
    {
      foreach (char c in p_charList)
        if (p_strToCheck.Contains(c))
          return (true);
      return (false);
    }

    public static List<string> ToLowerStringList(string[] p_stringArray)
    {
      List<string> l_list = new List<string>();
      foreach (string l_string in p_stringArray)
      {
        l_list.Add(StringUtils.RemoveDiacritics(l_string).ToLower());
      }
      return l_list;
    }

    public static string RemoveDiacritics(string text)
    {
      if (text == null)
        return "";

      var normalizedString = text.Normalize(NormalizationForm.FormD);
      var stringBuilder = new StringBuilder();

      foreach (var c in normalizedString)
      {
        var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
        if (unicodeCategory != UnicodeCategory.NonSpacingMark)
        {
          stringBuilder.Append(c);
        }
      }

      return stringBuilder.ToString().Normalize(NormalizationForm.FormC).ToLower();
    }

    public static List<string> SplitToken(string p_str, char p_split, string p_tokens)
    {
      Int32 i = 0, j = 0;
      bool l_split = true;
      char l_char = '\0';
      List<string> l_list = new List<string>();

      for (; i < p_str.Length; ++i)
      {
        if (p_tokens.Contains(p_str[i]) && (p_str[i] == l_char || l_char == '\0'))
        {
          l_char = (l_split ? p_str[i] : '\0');
          l_split = !l_split;
        }
        if (p_str[i] == p_split && l_split)
        {
          l_list.Add(new String(p_str.ToCharArray(j, i - j)));
          j = i + 1;
        }
      }
      l_list.Add(new String(p_str.ToCharArray(j, i - j)));
      return (l_list);
    }

    public static string Join<T>(T[] p_list, string p_between, string p_sides = "")
    {
      string l_str = "";

      for (Int32 i = 0; i < p_list.Length; ++i)
      {
        if (i > 0)
          l_str += p_between;
        l_str += p_sides + p_list[i] + p_sides;
      }
      return (l_str);
    }
  }
}
