// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.Encrypt.Rsa
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=

// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;

namespace VIBlend.Utilities.Encrypt
{
  internal class Rsa
  {
    public byte[] Decrypt(byte[] data, byte[] exponent, byte[] modulus)
    {
      this.ValidKey(exponent, modulus);
      this.ValidData(data, modulus);
      try
      {
        byte[] bytes = Rsa.Modolus(LargeInteger.Parse(data), LargeInteger.Parse(exponent), LargeInteger.Parse(modulus)).GetBytes();
        this.ValidData(bytes, modulus);
        return bytes;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private static LargeInteger Modolus(LargeInteger x, LargeInteger y, LargeInteger n)
    {
      LargeInteger largeInteger1 = (LargeInteger) 1;
      LargeInteger largeInteger2 = x;
      LargeInteger largeInteger3 = y;
      while (largeInteger3 > 0)
      {
        if (largeInteger3 % 2 > 0)
          largeInteger1 = largeInteger1 * largeInteger2 % n;
        largeInteger3 >>= 1;
        largeInteger2 = largeInteger2 * largeInteger2 % n;
      }
      return largeInteger1;
    }

    private void ValidKey(byte[] exponent, byte[] modulus)
    {
      if (modulus == null || exponent == null)
        throw new Exception("Invalid Key.");
    }

    private void ValidData(byte[] data, byte[] modulus)
    {
      if (data == null || this.MoreOrIs(data, modulus))
        throw new Exception("Invalid Data");
    }

    public bool MoreOrIs(byte[] a, byte[] b)
    {
      int length1 = a.Length;
      int length2 = b.Length;
      for (int index = 0; index < a.Length && (int) a[index] == 0; ++index)
        --length1;
      for (int index = 0; index < b.Length && (int) b[index] == 0; ++index)
        --length2;
      if (length1 > length2)
        return true;
      if (length1 != length2)
        return false;
      for (int index = 0; index < a.Length && (int) a[index] <= (int) b[index]; ++index)
      {
        if ((int) a[index] < (int) b[index])
          return false;
      }
      return true;
    }
  }
}
