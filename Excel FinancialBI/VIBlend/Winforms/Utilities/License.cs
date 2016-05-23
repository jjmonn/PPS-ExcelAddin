// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.License
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Security.Cryptography;
using System.Text;
using VIBlend.Utilities.Encrypt;

namespace VIBlend.Utilities
{
  internal class License
  {
    public int ProductId { get; private set; }

    public int PackageId { get; private set; }

    public DateTime DateIssued { get; private set; }

    public string Key { get; private set; }

    public bool IsValid
    {
      get
      {
        return this.ProductId != -1;
      }
    }

    public void Load(string content, string publicKey)
    {
      this.ProductId = -1;
      this.PackageId = -1;
      this.DateIssued = DateTime.UtcNow;
      this.Key = "";
      try
      {
        string[] strArray1 = content.Split('|');
        string signature1 = strArray1[0];
        string s = strArray1[1];
        byte[] bytes = new UTF8Encoding().GetBytes(this.GenMask(signature1));
        byte[] signature2 = Convert.FromBase64String(s);
        SHA1Managed shA1Managed = new SHA1Managed();
        RsaManaged rsaManaged = new RsaManaged();
        rsaManaged.FromXmlString(publicKey);
        if (!rsaManaged.ValidateSignature(bytes, (object) shA1Managed, signature2))
          return;
        string[] strArray2 = signature1.Split('g');
        if (strArray2.Length != 4)
          return;
        int result1 = -1;
        if (!int.TryParse(strArray2[0], out result1))
          return;
        int result2 = -1;
        if (!int.TryParse(strArray2[1], out result2))
          return;
        long result3 = -1;
        if (!long.TryParse(strArray2[2], out result3))
          return;
        this.DateIssued = DateTime.MinValue.AddTicks(result3);
        this.Key = strArray2[3];
        this.ProductId = result1;
        this.PackageId = result2;
      }
      catch (Exception ex)
      {
      }
    }

    private string GenMask(string signature)
    {
      ulong num1 = 0;
      int num2 = 0;
      ulong num3 = 0;
      for (int index = 0; index < signature.Length; ++index)
      {
        ulong num4 = (ulong) (byte) signature[index];
        num3 ^= num4 << num2 * 8;
        if (++num2 == 8)
        {
          num1 ^= num3;
          num3 = 0UL;
          num2 = 0;
        }
      }
      return num1.ToString();
    }
  }
}
