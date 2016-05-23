// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.ThemeCache
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace VIBlend.Utilities
{
  public static class ThemeCache
  {
    public static int CacheLimit = 1000;
    private static Dictionary<ThemeCacheKey, ControlTheme> themesCache = new Dictionary<ThemeCacheKey, ControlTheme>();
    private static Dictionary<ThemeCacheKey, ControlTheme> themesCacheOffice2010Black = new Dictionary<ThemeCacheKey, ControlTheme>();
    private static Dictionary<ThemeCacheKey, ControlTheme> themesCacheOffice2010Silver = new Dictionary<ThemeCacheKey, ControlTheme>();
    private static Dictionary<ThemeCacheKey, ControlTheme> themesCacheOffice2010Blue = new Dictionary<ThemeCacheKey, ControlTheme>();
    private static Dictionary<ThemeCacheKey, ControlTheme> themesCacheOfficeBlack = new Dictionary<ThemeCacheKey, ControlTheme>();
    private static Dictionary<ThemeCacheKey, ControlTheme> themesCacheOfficeBlue = new Dictionary<ThemeCacheKey, ControlTheme>();
    private static Dictionary<ThemeCacheKey, ControlTheme> themesCacheOfficeSilver = new Dictionary<ThemeCacheKey, ControlTheme>();
    private static Dictionary<ThemeCacheKey, ControlTheme> themesCacheOfficeGreen = new Dictionary<ThemeCacheKey, ControlTheme>();
    private static Dictionary<ThemeCacheKey, ControlTheme> themesCacheVISTA = new Dictionary<ThemeCacheKey, ControlTheme>();

    /// <summary>Clears the cache.</summary>
    public static void ClearCache()
    {
      ThemeCache.themesCache.Clear();
      ThemeCache.themesCacheOffice2010Black.Clear();
      ThemeCache.themesCacheOffice2010Blue.Clear();
      ThemeCache.themesCacheOffice2010Silver.Clear();
      ThemeCache.themesCacheOfficeBlack.Clear();
      ThemeCache.themesCacheOfficeBlue.Clear();
      ThemeCache.themesCacheOfficeGreen.Clear();
      ThemeCache.themesCacheOfficeSilver.Clear();
    }

    /// <summary>Clears the cache by key.</summary>
    /// <param name="styleKey">The style key.</param>
    /// <param name="theme">The theme.</param>
    public static void ClearCacheByKey(string styleKey, VIBLEND_THEME theme)
    {
      ThemeCacheKey key = new ThemeCacheKey(styleKey, theme);
      switch (theme)
      {
        case VIBLEND_THEME.OFFICEBLACK:
          ThemeCacheKey cachedKey1 = ThemeCache.GetCachedKey(key, ThemeCache.themesCacheOfficeBlack);
          if (cachedKey1 == null)
            break;
          ThemeCache.themesCacheOfficeBlack.Remove(cachedKey1);
          break;
        case VIBLEND_THEME.OFFICEBLUE:
          ThemeCacheKey cachedKey2 = ThemeCache.GetCachedKey(key, ThemeCache.themesCacheOfficeBlue);
          if (cachedKey2 == null)
            break;
          ThemeCache.themesCacheOfficeBlue.Remove(cachedKey2);
          break;
        case VIBLEND_THEME.OFFICESILVER:
          ThemeCacheKey cachedKey3 = ThemeCache.GetCachedKey(key, ThemeCache.themesCacheOfficeSilver);
          if (cachedKey3 == null)
            break;
          ThemeCache.themesCacheOfficeSilver.Remove(cachedKey3);
          break;
        case VIBLEND_THEME.OFFICEGREEN:
          ThemeCacheKey cachedKey4 = ThemeCache.GetCachedKey(key, ThemeCache.themesCacheOfficeGreen);
          if (cachedKey4 == null)
            break;
          ThemeCache.themesCacheOfficeGreen.Remove(cachedKey4);
          break;
        case VIBLEND_THEME.VISTABLUE:
          ThemeCacheKey cachedKey5 = ThemeCache.GetCachedKey(key, ThemeCache.themesCacheVISTA);
          if (cachedKey5 == null)
            break;
          ThemeCache.themesCacheVISTA.Remove(cachedKey5);
          break;
        case VIBLEND_THEME.OFFICE2010BLACK:
          ThemeCacheKey cachedKey6 = ThemeCache.GetCachedKey(key, ThemeCache.themesCacheOffice2010Black);
          if (cachedKey6 == null)
            break;
          ThemeCache.themesCacheOffice2010Black.Remove(cachedKey6);
          break;
        case VIBLEND_THEME.OFFICE2010BLUE:
          ThemeCacheKey cachedKey7 = ThemeCache.GetCachedKey(key, ThemeCache.themesCacheOffice2010Blue);
          if (cachedKey7 == null)
            break;
          ThemeCache.themesCacheOffice2010Blue.Remove(cachedKey7);
          break;
        case VIBLEND_THEME.OFFICE2010SILVER:
          ThemeCacheKey cachedKey8 = ThemeCache.GetCachedKey(key, ThemeCache.themesCacheOffice2010Silver);
          if (cachedKey8 == null)
            break;
          ThemeCache.themesCacheOffice2010Silver.Remove(cachedKey8);
          break;
        default:
          ThemeCacheKey cachedKey9 = ThemeCache.GetCachedKey(key, ThemeCache.themesCache);
          if (cachedKey9 == null)
            break;
          ThemeCache.themesCache.Remove(cachedKey9);
          break;
      }
    }

    private static ThemeCacheKey GetCachedKey(ThemeCacheKey key, Dictionary<ThemeCacheKey, ControlTheme> cache)
    {
      foreach (ThemeCacheKey key1 in cache.Keys)
      {
        if (key1.Equals((object) key) && key.Theme == key.Theme)
          return key1;
      }
      return (ThemeCacheKey) null;
    }

    private static ControlTheme GetFromCache(ThemeCacheKey key, Dictionary<ThemeCacheKey, ControlTheme> cache)
    {
      foreach (ThemeCacheKey key1 in cache.Keys)
      {
        if (key1.Equals((object) key))
        {
          if (key.Theme == key.Theme)
            return cache[key1];
          cache.Remove(key1);
        }
      }
      return (ControlTheme) null;
    }

    /// <summary>Gets the theme.</summary>
    /// <param name="styleKey">The style key.</param>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static ControlTheme GetTheme(string styleKey, VIBLEND_THEME value)
    {
      return ThemeCache.GetTheme(styleKey, value, (ControlTheme) null);
    }

    /// <summary>Gets the theme.</summary>
    /// <param name="styleKey">The style key.</param>
    /// <param name="value">The value.</param>
    /// <param name="controlTheme">The control theme.</param>
    /// <returns></returns>
    public static ControlTheme GetTheme(string styleKey, VIBLEND_THEME value, ControlTheme controlTheme)
    {
      ThemeCacheKey key = new ThemeCacheKey(styleKey, value);
      if (ThemeCache.themesCache.Count > 0 || ThemeCache.themesCacheOffice2010Black.Count > 0 || (ThemeCache.themesCacheOfficeBlue.Count > 0 || ThemeCache.themesCacheOfficeGreen.Count > 0) || (ThemeCache.themesCacheOfficeSilver.Count > 0 || ThemeCache.themesCacheVISTA.Count > 0 || (ThemeCache.themesCacheOffice2010Blue.Count > 0 || ThemeCache.themesCacheOffice2010Silver.Count > 0)) || ThemeCache.themesCacheOfficeBlack.Count > 0)
      {
        ControlTheme fromCache;
        switch (value)
        {
          case VIBLEND_THEME.OFFICEBLACK:
            fromCache = ThemeCache.GetFromCache(key, ThemeCache.themesCacheOfficeBlack);
            break;
          case VIBLEND_THEME.OFFICEBLUE:
            fromCache = ThemeCache.GetFromCache(key, ThemeCache.themesCacheOfficeBlue);
            break;
          case VIBLEND_THEME.OFFICESILVER:
            fromCache = ThemeCache.GetFromCache(key, ThemeCache.themesCacheOfficeSilver);
            break;
          case VIBLEND_THEME.OFFICEGREEN:
            fromCache = ThemeCache.GetFromCache(key, ThemeCache.themesCacheOfficeGreen);
            break;
          case VIBLEND_THEME.VISTABLUE:
            fromCache = ThemeCache.GetFromCache(key, ThemeCache.themesCacheVISTA);
            break;
          case VIBLEND_THEME.OFFICE2010BLACK:
            fromCache = ThemeCache.GetFromCache(key, ThemeCache.themesCacheOffice2010Black);
            break;
          case VIBLEND_THEME.OFFICE2010BLUE:
            fromCache = ThemeCache.GetFromCache(key, ThemeCache.themesCacheOffice2010Blue);
            break;
          case VIBLEND_THEME.OFFICE2010SILVER:
            fromCache = ThemeCache.GetFromCache(key, ThemeCache.themesCacheOffice2010Silver);
            break;
          default:
            fromCache = ThemeCache.GetFromCache(key, ThemeCache.themesCache);
            break;
        }
        if (fromCache != null)
          return fromCache;
      }
      ControlTheme controlTheme1 = (ControlTheme) null;
      try
      {
        controlTheme1 = ControlTheme.GetDefaultTheme(value);
      }
      catch (Exception ex)
      {
        Trace.WriteLine(ex.Message);
      }
      ControlTheme controlTheme2 = controlTheme == null ? controlTheme1.CreateCopy() : controlTheme.CreateCopy();
      switch (value)
      {
        case VIBLEND_THEME.OFFICEBLACK:
          if (ThemeCache.themesCacheOfficeBlack.Count > ThemeCache.CacheLimit)
            ThemeCache.themesCacheOfficeBlack.Clear();
          ThemeCache.themesCacheOfficeBlack.Add(key, controlTheme2);
          break;
        case VIBLEND_THEME.OFFICEBLUE:
          if (ThemeCache.themesCacheOfficeBlue.Count > ThemeCache.CacheLimit)
            ThemeCache.themesCacheOfficeBlue.Clear();
          ThemeCache.themesCacheOfficeBlue.Add(key, controlTheme2);
          break;
        case VIBLEND_THEME.OFFICESILVER:
          if (ThemeCache.themesCacheOfficeSilver.Count > ThemeCache.CacheLimit)
            ThemeCache.themesCacheOfficeSilver.Clear();
          ThemeCache.themesCacheOfficeSilver.Add(key, controlTheme2);
          break;
        case VIBLEND_THEME.OFFICEGREEN:
          if (ThemeCache.themesCacheOfficeGreen.Count > ThemeCache.CacheLimit)
            ThemeCache.themesCacheOfficeGreen.Clear();
          ThemeCache.themesCacheOfficeGreen.Add(key, controlTheme2);
          break;
        case VIBLEND_THEME.VISTABLUE:
          if (ThemeCache.themesCacheVISTA.Count > ThemeCache.CacheLimit)
            ThemeCache.themesCacheVISTA.Clear();
          ThemeCache.themesCacheVISTA.Add(key, controlTheme2);
          break;
        case VIBLEND_THEME.OFFICE2010BLACK:
          if (ThemeCache.themesCacheOffice2010Black.Count > ThemeCache.CacheLimit)
            ThemeCache.themesCacheOffice2010Black.Clear();
          ThemeCache.themesCacheOffice2010Black.Add(key, controlTheme2);
          break;
        case VIBLEND_THEME.OFFICE2010BLUE:
          if (ThemeCache.themesCacheOffice2010Blue.Count > ThemeCache.CacheLimit)
            ThemeCache.themesCacheOffice2010Blue.Clear();
          ThemeCache.themesCacheOffice2010Blue.Add(key, controlTheme2);
          break;
        case VIBLEND_THEME.OFFICE2010SILVER:
          if (ThemeCache.themesCacheOffice2010Silver.Count > ThemeCache.CacheLimit)
            ThemeCache.themesCacheOffice2010Silver.Clear();
          ThemeCache.themesCacheOffice2010Silver.Add(key, controlTheme2);
          break;
        default:
          if (ThemeCache.themesCache.Count > ThemeCache.CacheLimit)
            ThemeCache.themesCache.Clear();
          ThemeCache.themesCache.Add(key, controlTheme2);
          break;
      }
      return controlTheme2;
    }
  }
}
