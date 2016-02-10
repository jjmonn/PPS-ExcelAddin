using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using AddinExpress.MSO;

namespace FBI
{
  using Microsoft.Office.Interop.Excel;

  //public delegate void OnWorksheetChange(Range p_range);
  //public delegate void OnSelectionChange(Range p_range);
  //public delegate void BeforeRightClick(Range p_range, ADXCancelEventArgs e);


  /// <summary>
  ///   Add-in Express Excel Worksheet Module
  /// </summary>
  [GuidAttribute("43EE4272-0525-47BA-93CF-DDFB4A423F19"), ProgId("FBI.ExcelSheetModule1")]
  public partial class ExcelSheetModule1 : AddinExpress.MSO.ADXExcelSheetModule
  {
    //public event OnWorksheetChange Worksheet_Changed;
    //public event OnSelectionChange Worksheet_SelectionChanged;
    //public event BeforeRightClick Worksheet_BeforeRightClick;

    public ExcelSheetModule1()
    {
      InitializeComponent();
      SubsribeEvents();
    }

    private void SubsribeEvents()
    {
      this.SelectionChange += new AddinExpress.MSO.ADXExcelSelectionChange_EventHandler(this.ExcelSheetModule1_SelectionChange);
      this.BeforeRightClick += new AddinExpress.MSO.ADXExcelBeforeRightClick_EventHandler(this.ExcelSheetModule1_BeforeRightClick);
      this.Change += new AddinExpress.MSO.ADXExcelChange_EventHandler(this.ExcelSheetModule1_Change);
    }

    #region Add-in Express automatic code

    // Required by Add-in Express - do not modify
    // the methods within this region
    public override System.ComponentModel.IContainer GetContainer()
    {
      if (components == null)
        components = new System.ComponentModel.Container();
      return components;
    }

    #endregion

    private void ExcelSheetModule1_Change(object sender, object target)
    {
      try
      {
        // TO DO : send action to the addin.cs or addinFactsEdition.cs

        Range l_range = target as Range;
        if (l_range == null)
          return;
        // Worksheet_Changed(target as Range);
      }
      catch
      {
        System.Diagnostics.Debug.WriteLine("Worksheet change : could not convert target object to excel range.");
      }
    }

    private void ExcelSheetModule1_SelectionChange(object sender, object target)
    {
      try
      {
       // Worksheet_SelectionChanged(target as Range);
      }
      catch
      {
        System.Diagnostics.Debug.WriteLine("Selection change : could not convert target object to excel range.");
      }
    }

    private void ExcelSheetModule1_BeforeRightClick(object sender, object target, ADXCancelEventArgs e)
    {
      try
      {
       // Worksheet_BeforeRightClick(target as Range, e);
      }
      catch
      {
        System.Diagnostics.Debug.WriteLine("before right click : could not convert target object to excel range.");
      }
    }

  }
}

