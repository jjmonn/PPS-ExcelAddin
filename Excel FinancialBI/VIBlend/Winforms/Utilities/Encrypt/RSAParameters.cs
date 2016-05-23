// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.Encrypt.RSAParameters
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System.Xml.Serialization;

namespace VIBlend.Utilities.Encrypt
{
  [XmlRoot("RSAKeyValue")]
  internal struct RSAParameters
  {
    public byte[] P;
    public byte[] Q;
    public byte[] D;
    public byte[] DP;
    public byte[] DQ;
    public byte[] Exponent;
    public byte[] InverseQ;
    public byte[] Modulus;
  }
}
