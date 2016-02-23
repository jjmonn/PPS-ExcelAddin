using System;
using System.Collections.Generic;
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
  }
}
