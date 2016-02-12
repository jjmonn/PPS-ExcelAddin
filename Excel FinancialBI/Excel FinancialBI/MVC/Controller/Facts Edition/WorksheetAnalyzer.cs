using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Microsoft.Office.Interop.Excel;
  using Utils;
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;


  class WorksheetAnalyzer
  {
    Range m_range;
    Dimensions m_dimensions;
    Range m_lastCell;
    public String Error { get; private set; }
    

    public bool WorksheetScreenshot(Range p_range)
    {
      m_range = p_range;
      m_lastCell = GetRealLastCell(m_range);
      Error = Local.GetValue("");
      if (m_lastCell != null)
        return (true);
      else
        return (false);
    }

    public void Snapshot(Dimensions p_dimensions, List<UInt32> p_periodsList = null)
    {
      m_dimensions = p_dimensions;
      DimensionsIdentificationProcess();

      m_dimensions.DefineOrientation();
    }

    private void DimensionsIdentificationProcess()
    {    
      Range l_cell; 
      for (UInt32 l_rowIndex = 1; l_rowIndex <= m_lastCell.Row; l_rowIndex++)
      {
        for (UInt32 l_columnIndex = 1; l_columnIndex <= m_lastCell.Column; l_columnIndex++)
        {
          l_cell = m_range.Cells[l_rowIndex, l_columnIndex] as Range;
          if (l_cell == null)
          {
            System.Diagnostics.Debug.WriteLine("Dataset: Snapshot method: DimensionsIdentificationProcess > error in cell identication process: address : ");            
            continue;
          }

          if (Convert.ToBoolean(l_cell.EntireRow.Hidden) == true  || Convert.ToBoolean(l_cell.EntireColumn.Hidden) == true)
            continue;
        
          DateTime l_isDate;
          if (DateTime.TryParse(l_cell.Value as string, out l_isDate))
          {
            if (m_dimensions.IsPeriod(l_cell) == false)
            {
              m_dimensions.DimensionsIdentify(l_cell);
            }
          }
          else
          {
            m_dimensions.DimensionsIdentify(l_cell);
          }
        }
      }

    }
  
    private Range GetRangeFromRowAndColumn(Int32 p_rowIndex, Int32 p_columnIndex)
    {
      return (Range)m_range.Cells[p_rowIndex, p_columnIndex];
    }

    static public Range GetRealLastCell(Range p_range)
    {
      long lRealLastRow = 0;
      long lRealLastColumn = 0;
      try
      {
        lRealLastRow = p_range.Cells.Find("*", p_range.Cells[1, 1], null, null, XlSearchOrder.xlByRows, XlSearchDirection.xlPrevious).Row;
        lRealLastColumn = p_range.Cells.Find("*", p_range.Cells[1, 1], null, null, XlSearchOrder.xlByColumns, XlSearchDirection.xlPrevious).Column;
        return p_range.Cells[lRealLastRow, lRealLastColumn] as Range;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine("Get real last cell error: " + ex.Message);
        return null;
      }
    }

  }
}
