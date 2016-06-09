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
    SafeDictionary<Type, SafeDictionary<string, object>> m_extracted;
    Worksheet m_worksheet;

    public SafeDictionary<Type, SafeDictionary<string, object>> Extracted { get { return (m_extracted); } }

    public WorksheetExtractor(Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;
      m_extracted = new SafeDictionary<Type, SafeDictionary<string, object>>();
    }

    public void Clear()
    {
      m_extracted.Clear();
    }

    public void Extract(Range p_range)
    {
      foreach (Range l_cell in p_range.Cells)
      {
        Tuple<object, string> l_value = GetValue(l_cell.Value2, (string)l_cell.Text);

        if (l_value == null)
          continue;

        Type l_type = l_value.Item1.GetType();
        if (l_type == typeof(double))
        {
          DateTime l_out;

          if (DateTime.TryParse(l_value.Item2, out l_out))
          {
            InsertValue(typeof(DateTime), l_out, l_cell.Address);
            continue;
          }
        }
        InsertValue(l_type, l_value.Item1, l_cell.Address);
      }
    }

    void InsertValue(Type p_type, object p_value, string p_address)
    {
      if (m_extracted[p_type] == null)
        m_extracted[p_type] = new SafeDictionary<string, object>();
      m_extracted[p_type][p_address] = p_value;
    }

    static Tuple<object, string> GetValue(object p_param, string p_text)
    {
      if (p_param == null)
        return (null);
      if (p_param.GetType() == typeof(ADXExcelRef))
      {
        ADXExcelRef l_ref = p_param as ADXExcelRef;
        string l_address = l_ref.ConvertToA1Style();

        Range l_range = AddinModule.CurrentInstance.ExcelApp.Range[l_address];

        if (l_range != null)
          return (GetValue(l_range.Value, (string)l_range.Text));
        else
          return (null);
      }
      else
        return (new Tuple<object, string>(p_param, p_text));
    }

    public List<object> GetExtractedValues<T>()
    {
      if (m_extracted[typeof(T)] == null)
        return (new List<object>());
      return (m_extracted[typeof(T)].Values.ToList());
    }
  }
}
