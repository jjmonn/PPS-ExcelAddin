// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.DataExport.IExportWriter
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

namespace VIBlend.WinForms.DataGridView.DataExport
{
  /// <summary>
  /// Data export provider for GridView interface definition
  /// </summary>
  public interface IExportWriter
  {
    /// <summary>Creates and opens an output file for writing</summary>
    bool CreateFile(string filePath);

    /// <summary>Closes the output file</summary>
    void CloseFile();

    /// <summary>Starts a grid row</summary>
    void BeginRow();

    /// <summary>Ends a grid row</summary>
    void EndRow();

    /// <summary>Adds a column</summary>
    void AddColumn(string text);

    /// <summary>Adds a column</summary>
    void AddColumn(object value);
  }
}
