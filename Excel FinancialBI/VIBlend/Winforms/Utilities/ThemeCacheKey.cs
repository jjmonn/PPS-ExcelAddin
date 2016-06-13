// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.ThemeCacheKey
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

namespace VIBlend.Utilities
{
  /// <exclude />
  public class ThemeCacheKey
  {
    private string styleKey;
    private VIBLEND_THEME theme;

    /// <summary>Gets or sets the style key.</summary>
    /// <value>The style key.</value>
    public string StyleKey
    {
      get
      {
        return this.styleKey;
      }
      set
      {
        this.styleKey = value;
      }
    }

    /// <summary>Gets or sets the theme.</summary>
    /// <value>The theme.</value>
    public VIBLEND_THEME Theme
    {
      get
      {
        return this.theme;
      }
      set
      {
        this.theme = value;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:VIBlend.Utilities.ThemeCacheKey" /> class.
    /// </summary>
    /// <param name="styleKey">The style key.</param>
    /// <param name="theme">The theme.</param>
    public ThemeCacheKey(string styleKey, VIBLEND_THEME theme)
    {
      this.styleKey = styleKey;
      this.theme = theme;
    }

    /// <summary>
    /// Determines whether the specified <see cref="T:System.Object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="T:System.Object" /> to compare with this instance.</param>
    /// <returns>
    /// 	<c>true</c> if the specified <see cref="T:System.Object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="T:System.NullReferenceException">
    /// The <paramref name="obj" /> parameter is null.
    /// </exception>
    public override bool Equals(object obj)
    {
      ThemeCacheKey themeCacheKey = (ThemeCacheKey) obj;
      return themeCacheKey.Theme == this.Theme && themeCacheKey.styleKey == this.styleKey;
    }
  }
}
