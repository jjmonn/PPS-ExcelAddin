// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.Filters.StringFilterComparisonOperator
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView.Filters
{
  /// <summary>Enumeration of the string filter comparison operators</summary>
  public enum StringFilterComparisonOperator
  {
    EMPTY,
    NOT_EMPTY,
    NULL,
    NOT_NULL,
    CONTAINS,
    CONTAINS_CASE_SENSITIVE,
    DOES_NOT_CONTAIN,
    DOES_NOT_CONTAIN_CASE_SENSITIVE,
    STARTS_WITH,
    STARTS_WITH_CASE_SENSITIVE,
    ENDS_WITH,
    ENDS_WITH_CASE_SENSITIVE,
    EQUAL,
    EQUAL_CASE_SENSITIVE,
    REGEX,
  }
}
