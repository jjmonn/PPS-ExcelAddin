// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.PivotDesignPanelLocalization
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  public class PivotDesignPanelLocalization : PivotDesignPanelLocalizationBase
  {
    /// <summary>Gets the string.</summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public override string GetString(PivotDesignLocalizationNames name)
    {
      switch (name)
      {
        case PivotDesignLocalizationNames.MoveUpItem:
          return "Move Up";
        case PivotDesignLocalizationNames.MoveDownItem:
          return "Move Down";
        case PivotDesignLocalizationNames.MoveToRowAreaItem:
          return "Move to Row Area";
        case PivotDesignLocalizationNames.MoveToColumnAreaItem:
          return "Move to Column Area";
        case PivotDesignLocalizationNames.MoveToDataAreaItem:
          return "Move to Data Area";
        case PivotDesignLocalizationNames.RemoveFieldItem:
          return "Remove Field";
        case PivotDesignLocalizationNames.SelectItem:
          return "Select Items";
        case PivotDesignLocalizationNames.FunctionItem:
          return "Function";
        case PivotDesignLocalizationNames.AverageItem:
          return "Average";
        case PivotDesignLocalizationNames.CountItem:
          return "Count";
        case PivotDesignLocalizationNames.MaxItem:
          return "Max";
        case PivotDesignLocalizationNames.MinItem:
          return "Min";
        case PivotDesignLocalizationNames.ProductItem:
          return "Product";
        case PivotDesignLocalizationNames.SumItem:
          return "Sum";
        default:
          return (string) null;
      }
    }
  }
}
