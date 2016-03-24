using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  using Microsoft.Office.Interop.Excel;
  using VIBlend.WinForms.Controls;
  using Forms;
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;

  class ExcelUtils
  {
    public const Int32 EXCEL_SHEET_NAME_MAX_LENGHT = 30;

    public static Range CreateReceptionWS(string p_name, string[] p_headerNamesArray, string[] p_headerValuesArray)
    {
      Workbook l_workbook =   AddinModule.CurrentInstance.ExcelApp.ActiveWorkbook;
      try
      {
        Worksheet l_worksheet = (Worksheet)AddinModule.CurrentInstance.ExcelApp.Worksheets.Add();
        RenammeWorksheet(l_worksheet, l_workbook, p_name);
        AddinModule.CurrentInstance.ExcelApp.ActiveWindow.DisplayGridlines = false;
        return FillHeader(l_worksheet, p_headerNamesArray, p_headerValuesArray);
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine("Excel error during worksheet insertion " + e.Message);
        return null;
      }
    }

    private static void RenammeWorksheet(Worksheet p_worksheet, Workbook p_workbook, string p_name)
    {
      try
      {
        if (p_name.Length < EXCEL_SHEET_NAME_MAX_LENGHT && CheckIfWorkbookContainsWorksheetName(p_workbook, p_name) == false)
          p_worksheet.Name = p_name;
        else
        {
          p_name = p_name.Substring(0, EXCEL_SHEET_NAME_MAX_LENGHT);
          if (CheckIfWorkbookContainsWorksheetName(p_workbook, p_name) == false)
            p_worksheet.Name = p_name;
        }
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine("Could not renamme worksheet " + e.Message);
      }
    }

    private static Range FillHeader(Worksheet p_worksheet, string[] p_headerNamesArray, string[] p_headerValuesArray)
    {
      Range l_range = p_worksheet.Cells[1, 1] as Range;
	    Int32 i = 0;
	    foreach (var item_loopVariable in p_headerNamesArray) {
		    l_range.Offset[i + 1, 0].Value = p_headerNamesArray[i];
		    l_range.Offset[i + 1, 1].Value = p_headerValuesArray[i];
		    i = i + 1;
	    }
	    l_range.Offset[i + 1, 0].Value = Local.GetValue("upload.report_as_of") + DateTime.Now.ToString("D");
      return l_range.Offset[i + 2, 0];
    }

    private static bool CheckIfWorkbookContainsWorksheetName(Workbook p_workbook, string p_pane)
    {
      foreach (Worksheet l_worksheet in p_workbook.Worksheets)
      {
        if (l_worksheet.Name == p_pane)
          return true;
      }
      return false;
    }

    public static void WriteAccountsFromTreeView(FbiTreeView<Account> p_accountsTreeview, Range p_range, List<Int32> p_periodsList, TimeConfig p_timeConfig)
    {
	    int IndentLevel = 0;
	    foreach (vTreeNode Node in p_accountsTreeview.Nodes) 
      {
		    IndentLevel = 0;
		    p_range = p_range.Offset[1, 0];
		    p_range.Value = Node.Text;

        WritePeriodsOnWorksheet(p_range, p_periodsList, p_timeConfig);
		    p_range = p_range.Offset[1, 0];

		    foreach (vTreeNode childNode in Node.Nodes) 
        {
			    WriteAccountOnWorksheet(childNode, ref p_range, IndentLevel);
		    }
	    }
	    p_range.Columns.AutoFit();
    }
    
    public static void WritePeriodsOnWorksheet(Range p_destinationCell, List<Int32> p_periodsList, TimeConfig p_timeConfig)
    {
      if (p_periodsList == null)
        return;

      Int32 i = 0;
      foreach (Int32 l_periodAsInt in p_periodsList)
      {
        string l_date =  DateTime.FromOADate(l_periodAsInt).ToString("MM/dd/yyyy");
        p_destinationCell.Offset[0, 1 + i].Value = l_date;  //PeriodModel.GetFormatedDate(l_periodAsInt, p_timeConfig);
        i = i + 1;
      }
    }
    
    private static void WriteAccountOnWorksheet(vTreeNode p_node, ref Range p_range, int p_indentLevel)
    {
      p_range.IndentLevel = p_indentLevel;
      p_range.Value = p_node.Text;
      p_range = p_range.Offset[1, 0];

      foreach (vTreeNode Child in p_node.Nodes)
      {
        p_indentLevel = p_indentLevel + 1;
        WriteAccountOnWorksheet(Child, ref p_range, p_indentLevel);
        p_indentLevel = p_indentLevel - 1;
      }

    }

    public static  void WriteListOnWorksheet(Range p_range, List<string> p_list)
    {
      foreach (string l_value in p_list)
      {
        p_range = p_range.Offset[1, 0];
        p_range.Value2 = l_value;
      }
    }

    public static Range GetRange(string p_address, Worksheet p_worksheet)
    {
      return p_worksheet.Cells[p_address] as Range;
    }

  }
}
