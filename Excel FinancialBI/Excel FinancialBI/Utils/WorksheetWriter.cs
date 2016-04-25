using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;
using VIBlend.WinForms.DataGridView;
using Microsoft.VisualBasic;

namespace FBI.Utils
{
  using MVC.Model;
  using MVC.Model.CRUD;

  static class WorksheetWriter
  {
    #region Generic

    public static void WriteArray(object[,] p_arrayToWrite, Range p_destination)
    {
      p_destination.Resize[Information.UBound(p_arrayToWrite, 1) + 1, Information.UBound(p_arrayToWrite, 2) + 1].Value2 = p_arrayToWrite;
    }

    static internal void WriteListOnExcel(Range p_range, List<string> p_list)
    {
      foreach (string l_value in p_list)
      {
        p_range = p_range.Offset[1, 0];
        p_range.Value2 = l_value;
      }
    }

    static internal void WritePeriodsOnWorksheet(Range p_range, Int32[] p_periods, TimeConfig p_timeConfig)
    {
      Int32 i = 0;
      foreach (UInt32 period in p_periods)
      {
        p_range.Offset[0, 1 + i].Value = Strings.Format(System.DateTime.FromOADate(period), "MM/dd/yyyy");
        i = i + 1;
      }
    }

    #endregion

    #region Account

    public static bool WriteAccountsFromTreeView(vTreeView p_treeview, Range p_destinationCell, List<Int32> p_periodDatesList = null)
    {
      try
      {
        int IndentLevel = 0;
        foreach (vTreeNode l_node in p_treeview.Nodes)
        {
          IndentLevel = 0;
          p_destinationCell = p_destinationCell.Offset[1, 0];
          p_destinationCell.Value = l_node.Text;

          if ((p_periodDatesList != null))
          {
            Int32 i = 0;
            foreach (Int32 period in p_periodDatesList)
            {
              p_destinationCell.Offset[0, 1 + i].Value = Strings.Format(System.DateTime.FromOADate(period), "Short Date");
              i++;
            }
          }
          p_destinationCell = p_destinationCell.Offset[1, 0];

          foreach (vTreeNode l_childNode in l_node.Nodes)
            if (WriteAccount(l_childNode, ref p_destinationCell, ref IndentLevel) == false)
              return (false);
        }
        p_destinationCell.Worksheet.Columns.AutoFit();
        return (true);
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine(e.Message);
        if (e.InnerException != null)
          System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
        return (false);
      }
    }

    public static bool WriteAccount(vTreeNode p_node, ref Range p_destinationCell, ref int p_indentLevel)
    {
      try
      {
        p_destinationCell.IndentLevel = p_indentLevel;
        p_destinationCell.Value = p_node.Text;
        p_destinationCell = p_destinationCell.Offset[1, 0];

        foreach (vTreeNode l_child in p_node.Nodes)
        {
          p_indentLevel = p_indentLevel + 1;
          if (WriteAccount(l_child, ref p_destinationCell, ref p_indentLevel) == false)
            return (false);
          p_indentLevel = p_indentLevel - 1;
        }
        return (true);
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine(e.Message);
        if (e.InnerException != null)
          System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
        return (false);
      }
    }

    #endregion

    #region Send to Excel Functions

    static internal bool CheckIfWorkbookContainsWorksheetName(Workbook WB, string WSName)
    {
      foreach (Worksheet WS in WB.Worksheets)
        if (WS.Name == WSName)
          return true;
      return false;
    }

    static internal Range CreateReceptionWS(string wsName, string[] header_names_array, string[] header_values_array)
    {
      try
      {
        Worksheet WS = (Worksheet)AddinModule.CurrentInstance.ExcelApp.Worksheets.Add();
        if (Strings.Len(wsName) < 30 && CheckIfWorkbookContainsWorksheetName(AddinModule.CurrentInstance.ExcelApp.ActiveWorkbook, wsName) == false)
        {
          WS.Name = wsName;
        }
        else
        {
          wsName = Strings.Left(wsName, 30);
          if (CheckIfWorkbookContainsWorksheetName(AddinModule.CurrentInstance.ExcelApp.ActiveWorkbook, wsName) == false)
          {
            WS.Name = wsName;
          }
        }

        AddinModule.CurrentInstance.ExcelApp.ActiveWindow.DisplayGridlines = false;
        Range destination = (Range)WS.Cells[1, 1];

        Int32 i = 0;
        foreach (string item in header_names_array)
        {
          destination.Offset[i + 1, 0].Value = header_names_array[i];
          destination.Offset[i + 1, 1].Value = header_values_array[i];
          i = i + 1;
        }
        destination.Offset[i + 1, 0].Value = Local.GetValue("upload.report_as_of") + Convert.ToString(Strings.Format(DateTime.Today, "D"));

        destination = destination.Offset[i + 2, 0];
        return destination;
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine("WorksheetWriter.CreateReceptionWS: " + e.Message);
        return (null);
      }
    }

    static internal void CopyDGVToExcelGeneric(vDataGridView DGV, Range dest_range, string p_currencySymbol, ref Int32 i, ref bool p_copyOnlyExpanded)
    {
      Int32 j = 1;
      Int32 nb_columns_floors = i;

      foreach (HierarchyItem column in DGV.ColumnsHierarchy.Items)
        SetupColumnsTitles(column, dest_range, ref i, ref j, ref nb_columns_floors, ref p_copyOnlyExpanded);

      j = 0;
      i = i + 1;
      foreach (HierarchyItem row in DGV.RowsHierarchy.Items)
        SetupRowsTitles(row, dest_range, ref i, ref j, ref p_copyOnlyExpanded);

      j = 1;
      i = nb_columns_floors + 1;
      foreach (HierarchyItem row in DGV.RowsHierarchy.Items)
        CopyRowHierarchy(row, dest_range, p_currencySymbol, ref i, ref j, ref p_copyOnlyExpanded);
      dest_range.Worksheet.Range[dest_range.Worksheet.Cells[1, 1], dest_range.Offset[i, j]].Columns.AutoFit();
    }

    static internal void SetupColumnsTitles(HierarchyItem column, Range range, ref Int32 i, ref Int32 j, ref Int32 parent_j, ref bool p_copyOnlyExpanded)
    {
      range.Offset[i, j].Value = column.Caption;
      range.Offset[i, j].Font.Bold = true;
      // FormatRangeFromHierarchyItem(range.Offset(i, j), column)
      j = j + 1;

      if (p_copyOnlyExpanded == false | p_copyOnlyExpanded == true & column.Expanded == true)
      {
        if (column.Items.Count > 0)
        {
          foreach (HierarchyItem sub_column in column.Items)
          {
            i = i + 1;
            SetupColumnsTitles(sub_column, range, ref i, ref j, ref parent_j, ref p_copyOnlyExpanded);
          }
          parent_j = parent_j + column.Items.Count;
        }
      }
    }


    static internal void SetupRowsTitles(HierarchyItem row, Range range, ref Int32 i, ref Int32 j, ref bool p_copyOnlyExpanded)
    {

      range.Offset[i, j].Value = row.Caption;
      FormatRangeFromHierarchyItem(range.Offset[i, j], row);
      i = i + 1;
      Int32 sub_rows_nb = 0;
      Int32 group_start_row = i + range.Row - 1;


      if (p_copyOnlyExpanded == false | p_copyOnlyExpanded == true & row.Expanded == true)
      {

        foreach (HierarchyItem sub_row in row.Items)
        {
          SetupRowsTitles(sub_row, range, ref i, ref j, ref p_copyOnlyExpanded);
          sub_rows_nb = sub_rows_nb + sub_row.Items.Count;
        }
        if (row.Items.Count > 0)
        {
          Worksheet ws = range.Worksheet;
          Int32 nb_sub_items = 0;
          CountSubItemsNb(row, ref nb_sub_items);
          Range grouped_range = ws.Range[ws.Cells[group_start_row + 1, 1], ws.Cells[group_start_row + nb_sub_items, 2]];
          grouped_range.Rows.Group(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        }
      }
    }

    static internal void CopyRowHierarchy(HierarchyItem row, Range range, string p_currencySymbol, ref Int32 i, ref Int32 j, ref bool p_copyOnlyExpanded)
    {
      j = 1;
      Worksheet ws = range.Worksheet;
      foreach (HierarchyItem column in row.DataGridView.ColumnsHierarchy.Items)
        CopyColumnHierarchy(row, column, range, ref i, ref j, ref p_copyOnlyExpanded);
      FormatRangeFromGridCell(ws.Range[range.Offset[i, 1], range.Offset[i, j]], row, p_currencySymbol);
      i = i + 1;


      if (p_copyOnlyExpanded == false | p_copyOnlyExpanded == false && row.Expanded == true)
        foreach (HierarchyItem sub_row in row.Items)
          CopyRowHierarchy(sub_row, range, p_currencySymbol, ref i, ref j, ref p_copyOnlyExpanded);
    }


    static internal void CopyColumnHierarchy(HierarchyItem row, HierarchyItem column, Range range, ref Int32 i, ref Int32 j, ref bool p_copyOnlyExpanded)
    {
      range.Offset[i, j].Value = row.DataGridView.CellsArea.GetCellValue(row, column);
      j = j + 1;

      if (p_copyOnlyExpanded == false | p_copyOnlyExpanded == true && column.Expanded == true)
        foreach (HierarchyItem sub_column in column.Items)
          CopyColumnHierarchy(row, sub_column, range, ref i, ref j, ref p_copyOnlyExpanded);
    }

    static internal void FormatRangeFromGridCell(Range xlRange, HierarchyItem item, string p_currencySymbol)
    {
      switch (item.CellsFormatString)
      {
        case "{0:C0}":
          xlRange.NumberFormat = "# ##0 €";
          break;
        case "{0:P}":
          xlRange.NumberFormat = "0,00%";
          break;
        case "{0:N}":
          xlRange.NumberFormat = "General";
          break;
        case "{0:N2}":
          xlRange.NumberFormat = "# ##0";
          break;
        default:
          xlRange.NumberFormat = "#,##0;(" + p_currencySymbol + "#,##0)";
          break;
      }

      if (item != null && item.CellsStyle != null)
      {
        xlRange.Font.Color = item.CellsStyle.TextColor.ToArgb();
        xlRange.Font.Bold = item.CellsStyle.Font.Bold;
        xlRange.Font.Italic = item.CellsStyle.Font.Italic;
      }
    }


    static internal void FormatRangeFromHierarchyItem(Range xlRange, HierarchyItem item)
    {
      if ((item.HierarchyItemStyleNormal != null))
      {
        xlRange.Interior.Color = item.HierarchyItemStyleNormal.FillStyle.Colors[0];
        xlRange.Font.Color = item.HierarchyItemStyleNormal.TextColor.ToArgb();
        xlRange.Font.Bold = item.HierarchyItemStyleNormal.Font.Bold;
        xlRange.Font.Italic = item.HierarchyItemStyleNormal.Font.Italic;
      }
    }

    static internal void CountSubItemsNb(HierarchyItem item, ref Int32 nb_items)
    {
      foreach (HierarchyItem sub_item in item.Items)
      {
        nb_items = nb_items + 1;
        CountSubItemsNb(sub_item, ref nb_items);
      }
    }

    #endregion
  }
}