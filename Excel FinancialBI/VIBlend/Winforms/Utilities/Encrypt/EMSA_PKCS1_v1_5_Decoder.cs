// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.Encrypt.EMSA_PKCS1_v1_5_Decoder
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;

namespace VIBlend.Utilities.Encrypt
{
  internal class EMSA_PKCS1_v1_5_Decoder
  {
    public byte[] Decode(byte[] hash, byte[] signature, int keybitsize)
    {
      if (signature.Length < keybitsize / 8)
      {
        byte[] numArray = new byte[keybitsize / 8];
        numArray[0] = (byte) 0;
        Array.Copy((Array) signature, 0, (Array) numArray, 1, signature.Length);
        signature = numArray;
      }
      if ((int) signature[0] != 0 || (int) signature[1] != 1)
        throw new Exception("Invalid signature.");
      int index = 2;
      while (index < signature.Length && (int) signature[index] == (int) byte.MaxValue)
        ++index;
      if (index < 10 || (int) signature[index] != 0)
        throw new Exception("Invalid signature.");
      int sourceIndex1 = index + 1;
      byte[] numArray1 = new byte[signature.Length - sourceIndex1 - hash.Length];
      Array.Copy((Array) signature, sourceIndex1, (Array) numArray1, 0, numArray1.Length);
      int sourceIndex2 = sourceIndex1 + numArray1.Length;
      byte[] numArray2 = new byte[signature.Length - sourceIndex2];
      Array.Copy((Array) signature, sourceIndex2, (Array) numArray2, 0, numArray2.Length);
      if (numArray2.Length != hash.Length)
        throw new Exception("Invalid signature.");
      return numArray2;
    }
  }
}
