// Decompiled with JetBrains decompiler
// Type: VIBlend.Utilities.Licensing
// Assembly: VIBlend.WinForms.Utilities, Version=10.1.0.0, Culture=neutral, PublicKeyToken=bd9eb46da61c2531
// MVID: B364CB82-BDC8-49A3-B960-8586E1963D05
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.Utilities\10.1.0.0__bd9eb46da61c2531\VIBlend.WinForms.Utilities.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace VIBlend.Utilities
{
  public static class Licensing
  {
    private static bool isDemoDialogActive = false;
    private static DateTime dtLastCheck = DateTime.Now;
    private static DateTime dtLastDlgShow = DateTime.Now;
    private static string[] packageInfo = new string[4]{ "", "vOutlookNavPane, vTreeView, vExplorerBar, vRibbonBar, vRibbonBarGallery, vTabControl, QuickAccessToolbar,vRibbonGroup, vRatingControl, vMonthCalendar, vCircularProgressBar", "vDataGridView, vTreeView, vOutlookNavPane, vExplorerBar, vMonthCalendar, vMaskedTextBox, vOptionsFieldSet, vFieldSet", "vDataGridView, vTreeView, vRibbonBar, vRibbonBarGallery, QuickAccessToolbar,vRibbonGroup, vRatingControl, vMonthCalendar, vCircularProgressBar" };
    /// <summary>Gets or sets the license content</summary>
    private static string licenseContent = "";
    private static bool isInitialized = false;
    private static int hitCount = 0;
    private static License license = new License();

    public static string LicenseContent
    {
      get
      {
        return Licensing.licenseContent;
      }
      set
      {
        Licensing.licenseContent = value == null || value.Length <= 0 || value.Length >= 10000 ? "" : value;
        Licensing.isInitialized = false;
      }
    }

    private static bool DesignMode
    {
      get
      {
        return Process.GetCurrentProcess().ProcessName == "devenv";
      }
    }

    public static bool LICheck(Control control)
    {
      return true;
    }

    public static bool ValidateLicense(Control control)
    {
      return true;
    }

    private static bool LICheck(System.Type type, out bool isInExcludedSet)
    {
      isInExcludedSet = false;
      return true;
    }
  }
}
