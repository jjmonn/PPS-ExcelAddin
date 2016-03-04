using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using VIBlend.WinForms.Controls;
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
  }
}