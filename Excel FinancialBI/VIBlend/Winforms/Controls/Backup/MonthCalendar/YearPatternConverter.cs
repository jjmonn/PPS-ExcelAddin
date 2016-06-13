// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.YearPatternConverter
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace VIBlend.WinForms.Controls
{
  public class YearPatternConverter : ExpandableObjectConverter
  {
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType != typeof (string))
        return base.CanConvertFrom(context, sourceType);
      return true;
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      string str1 = value as string;
      if (str1 == null)
        return base.ConvertFrom(context, culture, value);
      string str2 = str1.Trim();
      if (str2.Length == 0)
        return (object) null;
      if (culture == null)
        culture = CultureInfo.CurrentCulture;
      char ch = culture.TextInfo.ListSeparator[0];
      string[] strArray = str2.Split(ch);
      int[] numArray = new int[strArray.Length];
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = (int) converter.ConvertFromString(context, culture, strArray[index]);
      int length = numArray.Length;
      return (object) new YearPattern(numArray[1], numArray[0]);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == null)
        throw new ArgumentNullException("destinationType");
      if (!(value is YearPattern) || destinationType != typeof (string))
        return base.ConvertTo(context, culture, value, destinationType);
      YearPattern yearPattern = (YearPattern) value;
      if (culture == null)
        culture = CultureInfo.CurrentCulture;
      string separator = culture.TextInfo.ListSeparator + " ";
      TypeConverter converter = TypeDescriptor.GetConverter(typeof (int));
      string[] strArray1 = new string[2];
      int num1 = 0;
      string[] strArray2 = strArray1;
      int index1 = num1;
      int num2 = 1;
      int num3 = index1 + num2;
      string string1 = converter.ConvertToString(context, culture, (object) yearPattern.Month);
      strArray2[index1] = string1;
      string[] strArray3 = strArray1;
      int index2 = num3;
      int num4 = 1;
      int num5 = index2 + num4;
      string string2 = converter.ConvertToString(context, culture, (object) yearPattern.Day);
      strArray3[index2] = string2;
      return (object) string.Join(separator, strArray1);
    }

    public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
    {
      if (propertyValues == null)
        throw new ArgumentNullException("propertyValues");
      return (object) new YearPattern((int) propertyValues[(object) "Month"], (int) propertyValues[(object) "Day"]);
    }

    public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
    {
      return true;
    }

    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
    {
      return TypeDescriptor.GetProperties(typeof (YearPattern), attributes).Sort(new string[2]{ "Month", "Day" });
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      return true;
    }
  }
}
