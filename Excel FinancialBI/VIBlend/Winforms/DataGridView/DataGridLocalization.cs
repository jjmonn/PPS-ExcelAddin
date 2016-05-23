// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.DataGridLocalization
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView
{
  public class DataGridLocalization : DataGridLocalizationBase
  {
    public override string GetString(LocalizationNames name)
    {
      switch (name)
      {
        case LocalizationNames.ContextMenuSortSmallestToLargest:
          return "Sort Smallest to Largest";
        case LocalizationNames.ContextMenuSortLargestToSmallest:
          return "Sort Largest to Smallest";
        case LocalizationNames.ContextMenuColumnChooser:
          return "Column Chooser";
        case LocalizationNames.ContextMenuGroupByThisColumn:
          return "Group by this column";
        case LocalizationNames.ContextMenuRemoveSort:
          return "Remove Sort";
        case LocalizationNames.ContextMenuFilter:
          return "Filter";
        case LocalizationNames.ContextMenuClearFilter:
          return "Clear Filter";
        case LocalizationNames.FilterWindowOrRadioButton:
          return "Or";
        case LocalizationNames.FilterWindowAndRadioButton:
          return "And";
        case LocalizationNames.FilterWindowOkButton:
          return "Ok";
        case LocalizationNames.FilterWindowCancelButton:
          return "Cancel";
        case LocalizationNames.FilterWindowFilterBy:
          return "Filter by: ";
        case LocalizationNames.FilterOperatorSelect:
          return "Select...";
        case LocalizationNames.FilterOperatorEqual:
          return "Equal";
        case LocalizationNames.FilterOperatorEqualCaseSensitive:
          return "Equal(Case Sensitive)";
        case LocalizationNames.FilterOperatorNotEqual:
          return "Not Equal";
        case LocalizationNames.FilterOperatorLessThan:
          return "Less Than";
        case LocalizationNames.FilterOperatorLessThanOrEqual:
          return "Less Than or Equal";
        case LocalizationNames.FilterOperatorGreaterThan:
          return "Greater Than";
        case LocalizationNames.FilterOperatorGreaterThanOrEqual:
          return "Greater Than or Equal";
        case LocalizationNames.FilterOperatorIsNull:
          return "Is NULL";
        case LocalizationNames.FilterOperatorIsNotNull:
          return "Is Not NULL";
        case LocalizationNames.FilterOperatorIsEmpty:
          return "Is Empty";
        case LocalizationNames.FilterOperatorIsNotEmpty:
          return "Is Not Empty";
        case LocalizationNames.FilterOperatorContains:
          return "Contains";
        case LocalizationNames.FilterOperatorContainsCaseSensitive:
          return "Contains(Case Sensitive)";
        case LocalizationNames.FilterOperatorDoesNotContain:
          return "Does Not Contain";
        case LocalizationNames.FilterOperatorDoesNotContainCaseSensitive:
          return "Does Not Contain(Case Sensitive)";
        case LocalizationNames.FilterOperatorStartsWith:
          return "Starts With";
        case LocalizationNames.FilterOperatorStartsWithCaseSensitive:
          return "Starts With(Case Sensitive)";
        case LocalizationNames.FilterOperatorEndsWith:
          return "Ends With";
        case LocalizationNames.FilterOperatorEndsWithCaseSensitive:
          return "Ends With(Case Sensitive)";
        case LocalizationNames.FilterOperatorRegularExpression:
          return "Regular Expression";
        case LocalizationNames.FilterValueText:
          return "Value:";
        case LocalizationNames.FilterShowRowsWhereText:
          return "Show rows where:";
        case LocalizationNames.FilterCriteriaDefinition:
          return "Filter Criteria Definition";
        case LocalizationNames.FilterButtonCustomFilter:
          return "Custom Filter";
        case LocalizationNames.FilterButtonBasicFilter:
          return "Basic Filter";
        case LocalizationNames.FilterItemsAllText:
          return "(Select All)";
        case LocalizationNames.FilterItemsBlanksText:
          return "(Blanks)";
        case LocalizationNames.FilterItemsNullsText:
          return "(Nulls)";
        case LocalizationNames.PivotTableSubTotalText:
          return "Sub Total";
        case LocalizationNames.PivotTableGrandTotalText:
          return "Grand Total";
        default:
          return (string) null;
      }
    }
  }
}
