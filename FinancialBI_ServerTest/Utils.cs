using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FinancialBI_ServerTest
{
  class Utils
  {
    public static string GetSHA1(string p_str)
    {
      SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
      Byte[] bytesToHash = Encoding.ASCII.GetBytes(p_str);

      bytesToHash = sha1.ComputeHash(bytesToHash);

      string result = "";

      foreach (Byte c in bytesToHash)
        result += c.ToString("x2");
      return (result);
    }
  }
}
