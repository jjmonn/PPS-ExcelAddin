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
        l_list.Add(StringUtils.RemoveDiacritics(l_string));
      }
      return l_list;
    }

    public static string RemoveDiacritics(string text)
    {
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

  }
}
