using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Controller;
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;
  using System.Drawing;

  class ExcelFormatting
  {

    // Identify the current range and 
    // Param: REPORT_FORMAT_CODE or INPUT_FORMAT_CODE
    static internal void FormatFinancialExcelRange(Range first_range_cell, UInt32 currency, System.DateTime startingDate)
    {
      Worksheet ws = first_range_cell.Worksheet;
      Range l_Range = default(Range);
      Range l_lastCell = WorksheetAnalyzer.GetRealLastCell(ws.Cells as Range);
      if (l_lastCell != null)
      {
        l_Range = ws.Range[first_range_cell, l_lastCell];
        FormatExcelRangeAs(l_Range,  currency,  startingDate);
      }
    
    }

    // Identify the current range and 
    // Param: REPORT_FORMAT_CODE or INPUT_FORMAT_CODE
    static internal void FormatRHExcelRange( Range p_firstCell)
    {
      Worksheet ws = p_firstCell.Worksheet;
      Range l_Range = default(Range);
      Range l_lastCell = WorksheetAnalyzer.GetRealLastCell(ws.Cells as Range);
      if (l_lastCell != null)
      {
        l_Range = ws.Range[p_firstCell, l_lastCell];
        //  to be reimplemented ?
        //  FormatExcelRangeAs(l_Range, Currency, startingDate)
      }
    
    }

    static internal void FormatExcelRangeAs(Range p_inputRange,  UInt32 p_currencyId, DateTime startingDate)
    {
      Currency l_currency = CurrencyModel.Instance.GetValue(p_currencyId);
      if (l_currency == null)
        return;
      
      FbiAccountFormat l_titleFormat = new  FbiAccountFormat("t");
      FbiAccountFormat l_importantFormat = new FbiAccountFormat("i");
      FbiAccountFormat l_normalFormat = new FbiAccountFormat("n");
      FbiAccountFormat l_detailFormat = new FbiAccountFormat("d");

      foreach (Range row in p_inputRange.Rows)
      {
        Range l_range =   row.Cells[1, 1] as Range;
        string accValue = l_range.Value2 as string;
        if ((accValue != null))
        {
          Account l_account = AccountModel.Instance.GetValue(accValue);
          if ((l_account != null))
          {
            FbiAccountFormat l_format = null;
            switch (l_account.FormatId)
            {
              case "t":
                l_format = l_titleFormat;
                break;
              case "i":
                l_format = l_importantFormat;
                break;

              case "n":
                l_format = l_normalFormat;
                break;

              case "d":
                l_format = l_detailFormat;
                break;
            }

            if (l_format == null)
              continue;

            // Colors
            row.Interior.Color = l_format.backColor;
            row.Font.Color = l_format.textColor;

            // Formats
            if (row.Cells.Count > 0)
            {
              Range l_cell = row.Cells[1, 1] as Range;
              l_cell.IndentLevel = l_format.indent;
            }

            if (l_format.isBold == true)
              row.Font.Bold = true;
            if (l_format.isItalic == true)
              row.Font.Italic = true;

            switch (l_account.Type)
            {
              case Account.AccountType.MONETARY:
                row.Cells.NumberFormat = "[$" + l_currency.Symbol + "]#,##0.00;([$" + l_currency.Symbol + "]#,##0.00)";
                break;
              case Account.AccountType.PERCENTAGE:
                row.Cells.NumberFormat = "0.00%";
                // put this in a table ?
                break;
              case Account.AccountType.NUMBER:
                row.Cells.NumberFormat = "#,##0.00";
                // further evolution set unit ?
                break;
              case Account.AccountType.DATE:
                row.Cells.NumberFormat = "d-mmm-yy";
                // d-mmm-yy
                break;
              default:
                row.Cells.NumberFormat = "#,##0.00";
                break;
            }

            // Borders
            if (l_format.bordersPresent == true)
            {
              row.Borders[XlBordersIndex.xlEdgeBottom].Color = l_format.bordersColor;
              row.Borders[XlBordersIndex.xlEdgeTop].Color = l_format.bordersColor;
            }
          }
        }
      }
      p_inputRange.Columns.AutoFit();

    }


    public static void FormatEntitiesReport( Range area)
    {
      area.Font.Color = Color.Black;
      Range subArea = default(Range);
      for (UInt32 j = 1; j <= area.Columns.Count - 1; j++)
      {
        Range cell1 = area.Cells[1, j] as Range;
        cell1 = cell1.Offset[-3, 0];
        Range cell2 = area.Cells[1, j] as Range;
        cell2 = cell2.Offset[area.Rows.Count - 4, 0];

        subArea = area.Range[cell1, cell2];
        subArea.Borders[XlBordersIndex.xlEdgeRight].Color = Color.Black;
        subArea.Borders[XlBordersIndex.xlEdgeBottom].Color = Color.Black;
        subArea.Borders[XlBordersIndex.xlEdgeLeft].Color = Color.Black;
        subArea.Borders[XlBordersIndex.xlEdgeTop].Color = Color.Black;
        Range l_cell = subArea.Cells[1, 1] as Range;
        l_cell.Borders[XlBordersIndex.xlEdgeBottom].Color = Color.Black;
      }
      area.Columns.AutoFit();
    }


  }
}
