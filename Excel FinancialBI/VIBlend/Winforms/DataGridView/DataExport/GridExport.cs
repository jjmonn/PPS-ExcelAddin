// Decompiled with JetBrains decompiler
// Type: VIBlend.WinForms.DataGridView.DataExport.GridExport
// Assembly: VIBlend.WinForms.DataGridView, Version=10.1.0.0, Culture=neutral, PublicKeyToken=84a1a092e92851e6
// MVID: F2A88F9F-F660-4105-8EFD-97C755F310EB
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\VIBlend.WinForms.DataGridView\10.1.0.0__84a1a092e92851e6\VIBlend.WinForms.DataGridView.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace VIBlend.WinForms.DataGridView.DataExport
{
  /// <summary>
  /// Represents a Data Export provider for VIBlend GridView
  /// </summary>
  public class GridExport
  {
    private vDataGridView grid;
    private List<HierarchyItem> leafLevelRowItems;
    private List<HierarchyItem> leafLevelColumnItems;

    private bool InitExport(vDataGridView grid)
    {
      this.grid = grid;
      grid.RowsHierarchy.GetVisibleLeafLevelItems(ref this.leafLevelRowItems);
      grid.ColumnsHierarchy.GetVisibleLeafLevelItems(ref this.leafLevelColumnItems);
      if (this.leafLevelRowItems != null && this.leafLevelColumnItems != null && this.leafLevelRowItems.Count > 0)
        return this.leafLevelColumnItems.Count > 0;
      return false;
    }

    private string GetHierarchyItemFullName(HierarchyItem item)
    {
      if (item.ParentItem == null)
        return item.Caption;
      StringBuilder stringBuilder = new StringBuilder();
      while (item != null)
      {
        stringBuilder.Insert(0, ']');
        stringBuilder.Insert(0, item.Caption);
        stringBuilder.Insert(0, '[');
        item = item.ParentItem;
        if (item != null)
          stringBuilder.Insert(0, '.');
      }
      return stringBuilder.ToString();
    }

    /// <summary>Exports the content of the data grid into a CSV file</summary>
    public bool ExportToCSV(vDataGridView grid, string fileName)
    {
      CSVExportWriter csvExportWriter = new CSVExportWriter();
      return this.Export(grid, fileName, (IExportWriter) csvExportWriter);
    }

    /// <summary>Exports the content of the data grid into a HTML file</summary>
    public bool ExportToHTML(vDataGridView grid, string fileName)
    {
      HTMLExportWriter htmlExportWriter = new HTMLExportWriter();
      return this.Export(grid, fileName, (IExportWriter) htmlExportWriter);
    }

    /// <summary>
    /// Exports the content of the data grid into a Microsoft Excel compatible XML file
    /// </summary>
    public bool ExportToExcelXML(vDataGridView grid, string fileName)
    {
      ExcelXMLExportWriter excelXmlExportWriter = new ExcelXMLExportWriter(grid);
      return this.Export(grid, fileName, (IExportWriter) excelXmlExportWriter);
    }

    /// <summary>Exports the content of the data grid into a XML file</summary>
    public bool ExportToXML(vDataGridView grid, string fileName)
    {
      XMLExportWriter xmlExportWriter = new XMLExportWriter();
      return this.Export(grid, fileName, (IExportWriter) xmlExportWriter);
    }

    /// <summary>
    /// Exports the content of the data grid using a custom export provider
    /// </summary>
    public bool Export(vDataGridView grid, string fileName, IExportWriter writer)
    {
      try
      {
        if (!this.InitExport(grid) || !writer.CreateFile(fileName))
          return false;
        writer.BeginRow();
        writer.AddColumn("");
        foreach (HierarchyItem leafLevelColumnItem in this.leafLevelColumnItems)
          writer.AddColumn(this.GetHierarchyItemFullName(leafLevelColumnItem));
        writer.EndRow();
        foreach (HierarchyItem leafLevelRowItem in this.leafLevelRowItems)
        {
          writer.BeginRow();
          writer.AddColumn(this.GetHierarchyItemFullName(leafLevelRowItem));
          foreach (HierarchyItem leafLevelColumnItem in this.leafLevelColumnItems)
          {
            object cellValue = grid.CellsArea.GetCellValue(leafLevelRowItem, leafLevelColumnItem);
            if (cellValue == null)
            {
              writer.AddColumn("");
            }
            else
            {
              if (cellValue.GetType() != typeof (string))
              {
                if (cellValue is DateTime)
                {
                  writer.AddColumn(cellValue);
                  continue;
                }
                if (cellValue is DateTime?)
                {
                  if (((DateTime?) cellValue).HasValue)
                  {
                    writer.AddColumn((object) ((DateTime?) cellValue).Value);
                    continue;
                  }
                  writer.AddColumn("");
                  continue;
                }
                double result = 0.0;
                if (double.TryParse(cellValue.ToString(), out result))
                {
                  writer.AddColumn(cellValue);
                  continue;
                }
              }
              string cellFormattedText = grid.CellsArea.GetCellFormattedText(leafLevelRowItem, leafLevelColumnItem, cellValue);
              writer.AddColumn(cellFormattedText);
            }
          }
          writer.EndRow();
        }
        writer.CloseFile();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return false;
      }
      return true;
    }
  }
}
