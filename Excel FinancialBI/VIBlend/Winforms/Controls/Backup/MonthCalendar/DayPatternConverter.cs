// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.Controls.DayPatternConverter
// Assembly: VIBlend.WinForms.Controls, Version=10.1.0.0, Culture=neutral, PublicKeyToken=6feea38fef5ea0cf
// MVID: 52299622-F3FC-47B1-B1BC-FF00B0753A57
// Assembly location: C:\Program Files (x86)\VIBlend\Windows Forms Controls v.10.1.0.0\VIBlend.WinForms.Controls.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;
using System.Reflection;

namespace VIBlend.WinForms.Controls
{
  public class DayPatternConverter : ExpandableObjectConverter
  {
    public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
    {
      return (object) new DayPattern((int) propertyValues[(object) "DaysToNextOccurence"]);
    }

    public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
      return new TypeConverter.StandardValuesCollection((ICollection) new string[3]{ "1", "2", "3" });
    }

    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == typeof (string))
        return true;
      return base.CanConvertFrom(context, sourceType);
    }

    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (value is DayPattern)
      {
        if (destinationType == typeof (string))
          return (object) ((DayPattern) value).DaysToNextOccurence.ToString();
        if (destinationType == typeof (InstanceDescriptor))
        {
          DayPattern dayPattern = (DayPattern) value;
          ConstructorInfo constructor = typeof (Size).GetConstructor(new Type[1]{ typeof (int) });
          if (constructor != null)
            return (object) new InstanceDescriptor((MemberInfo) constructor, (ICollection) new object[1]{ (object) dayPattern.DaysToNextOccurence });
        }
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo info, object value)
    {
      if (value is string)
      {
        try
        {
          string s = value.ToString();
          if (string.IsNullOrEmpty(s))
            s = "1";
          return (object) new DayPattern(int.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture));
        }
        catch
        {
        }
        throw new ArgumentException("The arguments were not valid.");
      }
      return base.ConvertFrom(context, info, value);
    }
  }
}
