using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using AddinExpress.MSO;

namespace FBI.Excel
{
  class WorksheetExtractor
  {
    SafeDictionary<Type, List<KeyValuePair<string, object>>> m_extracted;
    Worksheet m_worksheet;

    public readonly SafeDictionary<Type, List<KeyValuePair<string, object>>> Extracted { get { return (m_extracted); } }

    public WorksheetExtractor(Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;
      m_extracted = new SafeDictionary<Type, List<KeyValuePair<string, object>>>();
    }

    public void Clear()
    {
      m_extracted.Clear();
    }

    public void Extract(Range p_range)
    { 
      foreach (Range l_cell in p_range.Cells)
      {
        object l_value = GetValue(l_cell.Value2);

        if (l_value == null)
          continue;
        m_extracted[l_value.GetType()].Add(new KeyValuePair<string, object>(l_cell.Address, l_value));
      }
    }

    static dynamic GetValue(object p_param)
    {
      if (p_param == null)
        return (null);
      if (p_param.GetType() == typeof(ADXExcelRef))
      {
        ADXExcelRef l_ref = p_param as ADXExcelRef;
        string l_address = l_ref.ConvertToA1Style();

        Range l_range = AddinModule.CurrentInstance.ExcelApp.Range[l_address];

        if (l_range != null)
          return (GetValue(l_range.Value));
        else
          return (null);
      }
      else
        return (p_param);
    }
  }
}
