// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.Encrypt.LargeInteger
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Text;

namespace VIBlend.Utilities.Encrypt
{
  internal class LargeInteger
  {
    private uint[] data;
    private bool sign;

    internal int DataLength
    {
      get
      {
        return this.data.Length;
      }
    }

    private LargeInteger()
    {
      this.sign = true;
      this.data = new uint[1];
    }

    private LargeInteger(int value)
    {
      this.sign = value >= 0;
      if (this.sign)
        this.data = new uint[1]{ (uint) value };
      else
        this.data = new uint[1]{ (uint) -value };
    }

    private LargeInteger(uint value)
    {
      this.sign = true;
      this.data = new uint[1]{ value };
    }

    private LargeInteger(long value)
    {
      this.sign = value >= 0L;
      ulong num = (ulong) value;
      if (!this.sign)
        num = (ulong) -value;
      this.data = new uint[2]
      {
        (uint) num,
        (uint) (num >> 32)
      };
    }

    private LargeInteger(ulong value)
    {
      this.sign = true;
      this.data = new uint[2]
      {
        (uint) value,
        (uint) (value >> 32)
      };
    }

    private LargeInteger(LargeInteger value)
    {
      this.sign = value.sign;
      this.data = (uint[]) value.data.Clone();
    }

    private LargeInteger(bool sign, int length)
    {
      this.sign = sign;
      this.data = new uint[length];
    }

    private LargeInteger(bool sign, uint[] value)
    {
      this.sign = sign;
      this.data = value;
    }

    public static implicit operator LargeInteger(int value)
    {
      return new LargeInteger(value);
    }

    public static implicit operator LargeInteger(uint value)
    {
      return new LargeInteger(value);
    }

    public static implicit operator LargeInteger(long value)
    {
      return new LargeInteger(value);
    }

    public static implicit operator LargeInteger(ulong value)
    {
      return new LargeInteger(value);
    }

    public static LargeInteger operator +(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a + largeInteger;
    }

    public static LargeInteger operator +(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a + largeInteger;
    }

    public static LargeInteger operator +(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a + largeInteger;
    }

    public static LargeInteger operator +(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a + largeInteger;
    }

    public static LargeInteger operator +(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.Add(a, b);
    }

    public static LargeInteger operator -(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a - largeInteger;
    }

    public static LargeInteger operator -(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a - largeInteger;
    }

    public static LargeInteger operator -(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a - largeInteger;
    }

    public static LargeInteger operator -(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a - largeInteger;
    }

    public static LargeInteger operator -(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.Substract(a, b);
    }

    public static LargeInteger operator *(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a * largeInteger;
    }

    public static LargeInteger operator *(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a * largeInteger;
    }

    public static LargeInteger operator *(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a * largeInteger;
    }

    public static LargeInteger operator *(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a * largeInteger;
    }

    public static LargeInteger operator *(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.Multiply(a, b);
    }

    public static LargeInteger operator /(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a / largeInteger;
    }

    public static LargeInteger operator /(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a / largeInteger;
    }

    public static LargeInteger operator /(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a / largeInteger;
    }

    public static LargeInteger operator /(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a / largeInteger;
    }

    public static LargeInteger operator /(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.Divide(a, b)[0];
    }

    public static LargeInteger operator %(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a % largeInteger;
    }

    public static LargeInteger operator %(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a % largeInteger;
    }

    public static LargeInteger operator %(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a % largeInteger;
    }

    public static LargeInteger operator %(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a % largeInteger;
    }

    public static LargeInteger operator %(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.Divide(a, b)[1];
    }

    public static bool operator >(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a > largeInteger;
    }

    public static bool operator >(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a > largeInteger;
    }

    public static bool operator >(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a > largeInteger;
    }

    public static bool operator >(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a > largeInteger;
    }

    public static bool operator >(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.CompareSign(a, b) > 0;
    }

    public static bool operator <(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a < largeInteger;
    }

    public static bool operator <(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a < largeInteger;
    }

    public static bool operator <(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a < largeInteger;
    }

    public static bool operator <(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a < largeInteger;
    }

    public static bool operator <(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.CompareSign(a, b) < 0;
    }

    public static bool operator ==(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a == largeInteger;
    }

    public static bool operator ==(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a == largeInteger;
    }

    public static bool operator ==(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a == largeInteger;
    }

    public static bool operator ==(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a == largeInteger;
    }

    public static bool operator ==(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.CompareSign(a, b) == 0;
    }

    public static bool operator !=(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a != largeInteger;
    }

    public static bool operator !=(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a != largeInteger;
    }

    public static bool operator !=(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a != largeInteger;
    }

    public static bool operator !=(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a != largeInteger;
    }

    public static bool operator !=(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.CompareSign(a, b) != 0;
    }

    public static bool operator >=(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a >= largeInteger;
    }

    public static bool operator >=(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a >= largeInteger;
    }

    public static bool operator >=(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a >= largeInteger;
    }

    public static bool operator >=(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a >= largeInteger;
    }

    public static bool operator >=(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.CompareSign(a, b) >= 0;
    }

    public static bool operator <=(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a <= largeInteger;
    }

    public static bool operator <=(LargeInteger a, uint b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a <= largeInteger;
    }

    public static bool operator <=(LargeInteger a, long b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a <= largeInteger;
    }

    public static bool operator <=(LargeInteger a, ulong b)
    {
      LargeInteger largeInteger = new LargeInteger(b);
      return a <= largeInteger;
    }

    public static bool operator <=(LargeInteger a, LargeInteger b)
    {
      return LargeInteger.CompareSign(a, b) <= 0;
    }

    public static LargeInteger operator <<(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger();
      largeInteger.data = LargeInteger.LeftShift(a.data, b);
      largeInteger.Normalize();
      return largeInteger;
    }

    public static LargeInteger operator >>(LargeInteger a, int b)
    {
      LargeInteger largeInteger = new LargeInteger();
      largeInteger.data = LargeInteger.RightShift(a.data, b);
      largeInteger.Normalize();
      return largeInteger;
    }

    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string ToString()
    {
      return this.ToString("10");
    }

    public string ToString(string radix)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str = "0123456789ABCDEF";
      if (radix == "Bin" || radix == "bin")
      {
        for (int index1 = this.data.Length - 1; index1 > -1; --index1)
        {
          for (int index2 = 31; index2 > -1; --index2)
            stringBuilder.Append(this.data[index1] >> index2 & 1U);
        }
      }
      else if (radix == "X" || radix == "x")
      {
        for (int index = 0; index < this.data.Length; ++index)
        {
          uint num1 = this.data[index];
          int num2 = 0;
          while (num2 < 32)
          {
            uint num3 = num1 >> num2 & 15U;
            stringBuilder.Insert(0, str.Substring((int) num3, 1));
            num2 += 4;
          }
        }
      }
      else if (radix == "10")
      {
        uint[] numArray = this.data.Clone() as uint[];
        int length = numArray.Length;
        uint num1 = 10;
        ulong num2 = 0;
        ulong num3 = 0;
        if (length == 0)
          return "Math.IntBig";
        ulong num4;
        while (true)
        {
          num4 = 0UL;
          for (int index = length - 1; index > -1; --index)
          {
            ulong num5 = num4 + (ulong) numArray[index];
            num3 = num5 % (ulong) num1;
            numArray[index] = (uint) ((num5 - num3) / (ulong) num1);
            num4 = num3 << 32;
          }
          if ((int) numArray[length - 1] == 0)
          {
            if (length != 1)
              --length;
            else
              break;
          }
          stringBuilder.Insert(0, str.Substring((int) num3, 1));
        }
        stringBuilder.Insert(0, (num2 = num4 >> 32).ToString());
      }
      else
        stringBuilder.Append(radix);
      if (!this.sign)
        stringBuilder.Insert(0, "-");
      return stringBuilder.ToString();
    }

    public static LargeInteger Parse(string value)
    {
      return LargeInteger.Parse(value, 10);
    }

    public static LargeInteger Parse(string value, int radix)
    {
      int startIndex = 0;
      bool sign = true;
      if (value.Substring(startIndex, 1) == "-")
      {
        sign = false;
        ++startIndex;
      }
      uint[] array = new uint[1];
      for (; startIndex < value.Length; ++startIndex)
      {
        ulong num1 = ulong.Parse(value.Substring(startIndex, 1));
        for (uint index = 0; (long) index < (long) array.Length; ++index)
        {
          ulong num2 = num1 + (ulong) array[index] * (ulong) radix;
          array[index] = (uint) num2;
          num1 = num2 >> 32;
        }
        if ((long) num1 != 0L)
        {
          Array.Resize<uint>(ref array, array.Length + 1);
          array[array.Length - 1] = (uint) num1;
        }
      }
      return new LargeInteger(sign, array);
    }

    public static LargeInteger Parse(byte[] value)
    {
      return LargeInteger.Parse(value, true);
    }

    public static LargeInteger Parse(byte[] value, bool sign)
    {
      int num1 = 0;
      for (int index = 0; index < value.Length - 1 && (int) value[index] == 0; ++index)
        ++num1;
      int length = (value.Length - num1) / 4;
      if ((value.Length - num1) % 4 > 0)
        ++length;
      LargeInteger largeInteger = new LargeInteger(sign, length);
      int index1 = value.Length - 1;
      int num2 = 0;
      while (index1 > num1 - 1)
      {
        largeInteger.data[num2 / 4] += (uint) value[index1] << 8 * (num2 % 4);
        --index1;
        ++num2;
      }
      return largeInteger;
    }

    private static LargeInteger Add(LargeInteger a, LargeInteger b)
    {
      int num = LargeInteger.CompareArray(a, b);
      LargeInteger largeInteger;
      if (a.sign == b.sign)
      {
        if (num >= 0)
        {
          largeInteger = new LargeInteger(a);
          LargeInteger.AddArray(ref largeInteger.data, b.data);
        }
        else
        {
          largeInteger = new LargeInteger(b);
          LargeInteger.AddArray(ref largeInteger.data, a.data);
        }
      }
      else if (num > 0)
      {
        largeInteger = new LargeInteger(a);
        LargeInteger.SubstractArray(ref largeInteger.data, b.data);
      }
      else if (num < 0)
      {
        largeInteger = new LargeInteger(b);
        LargeInteger.SubstractArray(ref largeInteger.data, a.data);
      }
      else
        largeInteger = new LargeInteger(0);
      largeInteger.Normalize();
      return largeInteger;
    }

    private static void AddArray(ref uint[] a, uint[] b)
    {
      ulong num1 = 0;
      int index;
      for (index = 0; index < b.Length; ++index)
      {
        ulong num2 = num1 + (ulong) a[index] + (ulong) b[index];
        a[index] = (uint) num2;
        num1 = num2 >> 32;
      }
      while (num1 > 0UL)
      {
        if (a.Length == index)
          Array.Resize<uint>(ref a, a.Length + 1);
        ulong num2 = num1 + (ulong) a[index];
        a[index] = (uint) num2;
        num1 = num2 >> 32;
        ++index;
      }
    }

    private static LargeInteger Substract(LargeInteger a, LargeInteger b)
    {
      LargeInteger largeInteger = (LargeInteger) null;
      int num = LargeInteger.CompareArray(a, b);
      if (num == 0)
      {
        if (a.sign == b.sign)
        {
          largeInteger = new LargeInteger(0);
        }
        else
        {
          largeInteger = new LargeInteger(a);
          LargeInteger.AddArray(ref largeInteger.data, b.data);
        }
      }
      else if (num > 0)
      {
        largeInteger = new LargeInteger(a);
        if (a.sign == b.sign)
          LargeInteger.SubstractArray(ref largeInteger.data, b.data);
        else
          LargeInteger.AddArray(ref largeInteger.data, b.data);
      }
      else if (num < 0)
      {
        largeInteger = new LargeInteger(b);
        if (a.sign == b.sign)
        {
          LargeInteger.SubstractArray(ref largeInteger.data, a.data);
          largeInteger.sign = !largeInteger.sign;
        }
        else
        {
          LargeInteger.AddArray(ref largeInteger.data, a.data);
          largeInteger.sign = !largeInteger.sign;
        }
      }
      largeInteger.Normalize();
      return largeInteger;
    }

    private static void SubstractArray(ref uint[] a, uint[] b)
    {
      uint num1 = 0;
      int index;
      for (index = 0; index < b.Length; ++index)
      {
        if (a[index] >= b[index] + num1)
        {
          a[index] = a[index] - b[index] - num1;
          num1 = 0U;
        }
        else
        {
          uint num2 = (uint) (-1 - (int) b[index] - (int) num1 + 1);
          ulong num3 = (ulong) a[index] + (ulong) num2;
          a[index] = (uint) num3;
          num1 = 1U;
        }
      }
      while (num1 > 0U)
      {
        if (a[index] >= num1)
        {
          a[index] = a[index] - num1;
          num1 = 0U;
        }
        else
        {
          a[index] = 268435456U - num1;
          num1 = 1U;
        }
        ++index;
      }
    }

    private static LargeInteger Multiply(LargeInteger a, LargeInteger b)
    {
      LargeInteger largeInteger = new LargeInteger(a.sign == b.sign, a.data.Length + b.data.Length);
      LargeInteger.MultiplyArray(ref largeInteger.data, a.data, b.data);
      largeInteger.Normalize();
      return largeInteger;
    }

    private static void MultiplyArray(ref uint[] c, uint[] a, uint[] b)
    {
      for (int index1 = 0; index1 < b.Length; ++index1)
      {
        uint num1 = 0;
        for (int index2 = 0; index2 < a.Length; ++index2)
        {
          ulong num2 = (ulong) a[index2] * (ulong) b[index1] + (ulong) c[index2 + index1] + (ulong) num1;
          c[index2 + index1] = (uint) num2;
          num1 = (uint) (num2 >> 32);
        }
        c[index1 + a.Length] = num1;
      }
    }

    private static uint DivideUInt(ref uint[] tastnoe, uint[] delimoe, uint delitel)
    {
      ulong num1 = 0;
      for (int index = delimoe.Length - 1; index >= 0; --index)
      {
        ulong num2 = num1 * 4294967296UL + (ulong) delimoe[index];
        tastnoe[index] = (uint) (num2 / (ulong) delitel);
        num1 = num2 - (ulong) (tastnoe[index] * delitel);
      }
      return (uint) num1;
    }

    private static uint DivideModulUInt(uint[] delimoe, uint delitel)
    {
      ulong num = 0;
      for (int index = delimoe.Length - 1; index >= 0; --index)
        num = ((num << 32) + (ulong) delimoe[index]) % (ulong) delitel;
      return (uint) num;
    }

    private static LargeInteger[] Divide(LargeInteger a, LargeInteger b)
    {
      if (b == 0)
        throw new DivideByZeroException();
      int num = LargeInteger.CompareArray(a, b);
      bool sign = a.sign == b.sign;
      if (num > 0)
      {
        Array[] arrayArray = LargeInteger.DivideArray(a.data, b.data);
        LargeInteger largeInteger1 = new LargeInteger(sign, (uint[]) arrayArray[0]);
        LargeInteger largeInteger2 = new LargeInteger(sign, (uint[]) arrayArray[1]);
        largeInteger1.Normalize();
        largeInteger2.Normalize();
        return new LargeInteger[2]{ largeInteger1, largeInteger2 };
      }
      if (num < 0)
        return new LargeInteger[2]{ new LargeInteger(true, new uint[1]), new LargeInteger(sign, (uint[]) a.data.Clone()) };
      return new LargeInteger[2]{ new LargeInteger((sign ? 1 : 0) != 0, new uint[1]{ 1U }), new LargeInteger(true, new uint[1]) };
    }

    private static Array[] DivideArray(uint[] u, uint[] v)
    {
      int length1 = u.Length;
      int length2 = v.Length;
      uint[] tastnoe = new uint[length1 - length2 + 1];
      uint[] numArray1;
      if (length2 == 1)
      {
        int num = (int) LargeInteger.DivideUInt(ref tastnoe, u, v[0]);
        numArray1 = new uint[1]
        {
          LargeInteger.DivideModulUInt(u, v[0])
        };
      }
      else
      {
        int b = LargeInteger.Nlz(v[length2 - 1]);
        uint[] numArray2 = LargeInteger.LeftShift(v, b);
        uint[] array = LargeInteger.LeftShift(u, b);
        Array.Resize<uint>(ref array, u.Length + 1);
        for (int index1 = length1 - length2; index1 >= 0; --index1)
        {
          ulong num1 = (ulong) array[index1 + length2] * 4294967296UL + (ulong) array[index1 + length2 - 1];
          ulong num2 = num1 / (ulong) numArray2[length2 - 1];
          ulong num3 = num1 - num2 * (ulong) numArray2[length2 - 1];
          while (num2 >= 4294967296UL || num2 * (ulong) numArray2[length2 - 2] > num3 * 4294967296UL + (ulong) array[index1 + length2 - 2])
          {
            --num2;
            num3 += (ulong) numArray2[length2 - 1];
            if (num3 >= 4294967296UL)
              break;
          }
          uint num4 = 0;
          for (int index2 = 0; index2 < length2; ++index2)
          {
            ulong num5 = num2 * (ulong) numArray2[index2];
            ulong num6 = (ulong) ((long) array[index2 + index1] - (long) num4 - ((long) num5 & (long) uint.MaxValue));
            array[index1 + index2] = (uint) num6;
            num4 = (uint) ((num5 >> 32) - (num6 >> 32));
          }
          ulong num7 = (ulong) (array[index1 + length2] - num4);
          array[index1 + length2] = (uint) num7;
          tastnoe[index1] = (uint) num2;
        }
        numArray1 = LargeInteger.RightShift(array, b);
      }
      return new Array[2]{ (Array) tastnoe, (Array) numArray1 };
    }

    private static uint[] LeftShift(uint[] v, int b)
    {
      int num1 = b / 32;
      int num2 = b % 32;
      uint[] array = (uint[]) v.Clone();
      ulong num3 = 0;
      if (num2 > 0)
      {
        for (int index = 0; index < v.Length; ++index)
        {
          array[index] <<= num2;
          array[index] |= (uint) num3;
          num3 = (ulong) v[index] << num2 >> 32;
        }
        if (num3 > 0UL)
          ++num1;
      }
      if (num1 > 0)
      {
        int length = array.Length;
        Array.Resize<uint>(ref array, length + num1);
        if (num3 > 0UL)
        {
          array[array.Length - 1] = (uint) num3;
          --num1;
        }
        Array.Copy((Array) array, 0, (Array) array, num1, length);
        Array.Clear((Array) array, 0, num1);
      }
      return array;
    }

    private static uint[] RightShift(uint[] v, int value)
    {
      int sourceIndex = value / 32;
      value %= 32;
      if (sourceIndex >= v.Length)
        return new uint[1];
      uint[] numArray1 = (uint[]) v.Clone();
      if (value > 0)
      {
        numArray1[sourceIndex] >>= value;
        for (int index = sourceIndex + 1; index < numArray1.Length; ++index)
        {
          ulong num = (ulong) numArray1[index] << 32 - value;
          numArray1[index] >>= value;
          numArray1[index - 1] |= (uint) num;
        }
        if ((int) numArray1[numArray1.Length - 1] == 0)
          ++sourceIndex;
      }
      if (sourceIndex > 0)
      {
        uint[] numArray2 = new uint[numArray1.Length - sourceIndex];
        if ((int) numArray1[numArray1.Length - 1] == 0)
          --sourceIndex;
        Array.Copy((Array) numArray1, sourceIndex, (Array) numArray2, 0, numArray2.Length);
        numArray1 = numArray2;
      }
      return numArray1;
    }

    private static int CompareSign(LargeInteger a, LargeInteger b)
    {
      if (a.sign && !b.sign)
        return 1;
      if (!a.sign && b.sign)
        return -1;
      if (a.sign)
        return LargeInteger.CompareArray(a, b);
      return LargeInteger.CompareArray(b, a);
    }

    public byte[] GetBytes()
    {
      if (this.data.Length == 0)
        return new byte[0];
      int length = this.data.Length * 4;
      if (((int) this.data[this.data.Length - 1] & -16777216) == 0)
      {
        --length;
        if (((int) this.data[this.data.Length - 1] & 16711680) == 0)
        {
          --length;
          if (((int) this.data[this.data.Length - 1] & 65280) == 0)
            --length;
        }
      }
      byte[] numArray = new byte[length];
      int index = length - 1;
      int num = 0;
      while (index > -1)
      {
        numArray[index] = (byte) (this.data[num / 4] >> 8 * (num % 4));
        --index;
        ++num;
      }
      return numArray;
    }

    private static int CompareArray(LargeInteger a, LargeInteger b)
    {
      int length1 = a.data.Length;
      int length2 = b.data.Length;
      if (length1 > length2)
        return 1;
      if (length1 < length2)
        return -1;
      for (int index = length1 - 1; index > -1; --index)
      {
        if (a.data[index] > b.data[index])
          return 1;
        if (a.data[index] < b.data[index])
          return -1;
      }
      return 0;
    }

    private void Normalize()
    {
      int index = this.data.Length - 1;
      while (index > 0 && this.data[index] <= 0U)
        --index;
      Array.Resize<uint>(ref this.data, index + 1);
      if (this.data.Length != 1 || (int) this.data[0] != 0)
        return;
      this.sign = true;
    }

    private static int Nlz(uint x)
    {
      int num = 0;
      for (int index = 31; index >= 0 && (int) (x >> index) == 0; --index)
        ++num;
      return num;
    }
  }
}
