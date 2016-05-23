// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.Properties.Resources
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace VIBlend.Utilities.Properties
{
  /// <summary>
  ///   A strongly-typed resource class, for looking up localized strings, etc.
  /// </summary>
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    /// <summary>
    ///   Returns the cached ResourceManager instance used by this class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) VIBlend.Utilities.Properties.Resources.resourceMan, (object) null))
          VIBlend.Utilities.Properties.Resources.resourceMan = new ResourceManager("VIBlend.Utilities.Properties.Resources", typeof (VIBlend.Utilities.Properties.Resources).Assembly);
        return VIBlend.Utilities.Properties.Resources.resourceMan;
      }
    }

    /// <summary>
    ///   Overrides the current thread's CurrentUICulture property for all
    ///   resource lookups using this strongly typed resource class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get
      {
        return VIBlend.Utilities.Properties.Resources.resourceCulture;
      }
      set
      {
        VIBlend.Utilities.Properties.Resources.resourceCulture = value;
      }
    }

    internal static Bitmap WinformsLogo21
    {
      get
      {
        return (Bitmap) VIBlend.Utilities.Properties.Resources.ResourceManager.GetObject("WinformsLogo21", VIBlend.Utilities.Properties.Resources.resourceCulture);
      }
    }

    internal Resources()
    {
    }
  }
}
