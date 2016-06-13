// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.Encrypt.RsaManaged
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Security.Cryptography;
using System.Xml;

namespace VIBlend.Utilities.Encrypt
{
  internal class RsaManaged
  {
    private Rsa rsa;
    private int keysize;
    private RSAParameters rsaParams;

    public RsaManaged()
    {
      this.keysize = 1024;
      this.rsa = new Rsa();
    }

    public RsaManaged(int keySize)
    {
      this.keysize = keySize;
      this.rsa = new Rsa();
    }

    public void ResetParameters()
    {
      this.ClearArray(this.rsaParams.Q);
      this.ClearArray(this.rsaParams.P);
      this.ClearArray(this.rsaParams.D);
      this.ClearArray(this.rsaParams.DQ);
      this.ClearArray(this.rsaParams.DP);
      this.ClearArray(this.rsaParams.InverseQ);
      this.ClearArray(this.rsaParams.Exponent);
      this.ClearArray(this.rsaParams.Modulus);
      this.rsaParams.Q = (byte[]) null;
      this.rsaParams.P = (byte[]) null;
      this.rsaParams.D = (byte[]) null;
      this.rsaParams.DQ = (byte[]) null;
      this.rsaParams.DP = (byte[]) null;
      this.rsaParams.InverseQ = (byte[]) null;
      this.rsaParams.Exponent = (byte[]) null;
      this.rsaParams.Modulus = (byte[]) null;
    }

    private void ClearArray(byte[] array)
    {
      if (array == null)
        return;
      Array.Clear((Array) array, 0, array.Length);
    }

    private byte[] SetArray(byte[] array)
    {
      if (array != null)
        return array.Clone() as byte[];
      return (byte[]) null;
    }

    private void SetParams(RSAParameters rsaParams)
    {
      if (rsaParams.Modulus == null || rsaParams.Exponent == null)
        throw new Exception("Invalid key.");
      this.ResetParameters();
      this.rsaParams.P = this.SetArray(rsaParams.P);
      this.rsaParams.Q = this.SetArray(rsaParams.Q);
      this.rsaParams.D = this.SetArray(rsaParams.D);
      this.rsaParams.DP = this.SetArray(rsaParams.DP);
      this.rsaParams.DQ = this.SetArray(rsaParams.DQ);
      this.rsaParams.InverseQ = this.SetArray(rsaParams.InverseQ);
      this.rsaParams.Exponent = this.SetArray(rsaParams.Exponent);
      this.rsaParams.Modulus = this.SetArray(rsaParams.Modulus);
      this.keysize = this.rsaParams.Modulus.Length * 8;
    }

    public void FromXmlString(string str)
    {
      RSAParameters rsaParams = new RSAParameters();
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.LoadXml(str);
      if (xmlDocument != null)
      {
        XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("RSAKeyValue");
        rsaParams.Modulus = this.ReadElement(elementsByTagName, "Modulus");
        rsaParams.Exponent = this.ReadElement(elementsByTagName, "Exponent");
        rsaParams.P = this.ReadElement(elementsByTagName, "P");
        rsaParams.Q = this.ReadElement(elementsByTagName, "Q");
        rsaParams.D = this.ReadElement(elementsByTagName, "D");
        rsaParams.DP = this.ReadElement(elementsByTagName, "DP");
        rsaParams.DQ = this.ReadElement(elementsByTagName, "DQ");
        rsaParams.InverseQ = this.ReadElement(elementsByTagName, "InverseQ");
      }
      this.SetParams(rsaParams);
    }

    private byte[] ReadElement(XmlNodeList xmlElement, string xmlChildElement)
    {
      byte[] numArray = (byte[]) null;
      XmlNode xmlNode1 = (XmlNode) null;
      for (int index = 0; index < xmlElement[0].ChildNodes.Count; ++index)
      {
        XmlNode xmlNode2 = xmlElement[0].ChildNodes[index];
        if (xmlNode2.Name.Equals(xmlChildElement))
        {
          xmlNode1 = xmlNode2;
          break;
        }
      }
      if (xmlNode1 != null)
      {
        try
        {
          numArray = Convert.FromBase64String(xmlNode1.ChildNodes[0].Value);
        }
        catch (FormatException ex)
        {
          numArray = (byte[]) null;
        }
      }
      return numArray;
    }

    public bool ValidateSignature(byte[] buffer, object hashobject, byte[] signature)
    {
      try
      {
        return this.ValidateHash((hashobject as HashAlgorithm).ComputeHash(buffer), signature);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    public bool ValidateHash(byte[] hash, byte[] signature)
    {
      try
      {
        byte[] signature1 = this.rsa.Decrypt(signature, this.rsaParams.Exponent, this.rsaParams.Modulus);
        byte[] numArray = new EMSA_PKCS1_v1_5_Decoder().Decode(hash, signature1, this.keysize);
        if (hash.Length != numArray.Length)
          return false;
        for (int index = 0; index < hash.Length; ++index)
        {
          if ((int) hash[index] != (int) numArray[index])
            return false;
        }
        return true;
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
