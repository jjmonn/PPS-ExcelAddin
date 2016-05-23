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
      if (Licensing.DesignMode || (DateTime.Now - Licensing.dtLastCheck).TotalSeconds < 30.0)
        return true;
      Licensing.dtLastCheck = DateTime.Now;
      System.Type type = control.GetType();
      bool isInExcludedSet = false;
      if (Licensing.LICheck(type, out isInExcludedSet))
        return true;
      TimeSpan timeSpan = DateTime.Now - Licensing.dtLastDlgShow;
      if (!Licensing.isDemoDialogActive && timeSpan.TotalMinutes > 8.0)
      {
        Licensing.isDemoDialogActive = true;
        Licensing.DisplayTrialMsg(isInExcludedSet ? "Controls" : type.Name);
        Licensing.dtLastDlgShow = DateTime.Now;
        Licensing.isDemoDialogActive = false;
      }
      return false;
    }

    /// <summary>Checks if the license status of a control.</summary>
    /// <param name="control">Control to check.</param>
    /// <returns>True if the control passes the license validation.</returns>
    public static bool ValidateLicense(Control control)
    {
      if (control == null || ++Licensing.hitCount > 1000)
        return false;
      bool isInExcludedSet = false;
      return Licensing.LICheck(control.GetType(), out isInExcludedSet);
    }

    private static bool LICheck(System.Type type, out bool isInExcludedSet)
    {
      isInExcludedSet = false;
      if (!Licensing.isInitialized)
      {
        string publicKey = "";
        try
        {
          Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("VIBlend.Utilities.Resources.keys.public.xml");
          StreamReader streamReader = new StreamReader(manifestResourceStream);
          publicKey = streamReader.ReadToEnd();
          streamReader.Close();
          manifestResourceStream.Close();
        }
        catch (Exception ex)
        {
        }
        Licensing.license.Load(Licensing.licenseContent, publicKey);
        Licensing.isInitialized = true;
      }
      if (!Licensing.license.IsValid || Licensing.license.ProductId != 0 || (Licensing.license.PackageId < 0 || Licensing.license.PackageId >= Licensing.packageInfo.Length))
        return false;
      if (!Licensing.packageInfo[Licensing.license.PackageId].ToLower().Contains(type.Name.ToLower()))
        return true;
      isInExcludedSet = true;
      return false;
    }

    private static void DisplayTrialMsg(string controlName)
    {
      TrialForm trialForm = new TrialForm();
      trialForm.ControlName = controlName;
      Licensing.isDemoDialogActive = true;
      int num = (int) trialForm.ShowDialog();
      Licensing.dtLastDlgShow = DateTime.Now;
      Licensing.isDemoDialogActive = false;
    }
  }
}
