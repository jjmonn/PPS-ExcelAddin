using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Microsoft.Office.Interop.Excel;


  class WorksheetAnalyzer
  {
    Worksheet m_worksheet;
    Dimensions m_dimensions;


    public bool WorksheetScreenshot(Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;

      // TO DO : get real last cell
      // return false if worksheet empty
  
      return true;
    }

    public void Snapshot(Dimensions p_dimensions)
    {
      m_dimensions = p_dimensions;
      
      // TO DO : identify and register dimensions

      m_dimensions.DefineOrientation();
    }



    private Range GetRangeFromRowAndColumn(ref Int32 p_rowIndex, ref Int32 p_columnIndex)
    {
      return (Range) m_worksheet.Cells[p_rowIndex, p_columnIndex];
    }

  }
}
