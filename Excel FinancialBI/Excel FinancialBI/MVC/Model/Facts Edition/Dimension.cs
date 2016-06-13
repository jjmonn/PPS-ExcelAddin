using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.Utils;
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Model.CRUD;

  public enum SnapshotResult
  {
    EMPTY = 0,
    ONE,
    SEVERAL
  }

  public enum Alignment
  {
    UNDEFINED = 0,
    HORIZONTAL,
    VERTICAL,
  }

  public enum DimensionType
  {
    UNDEFINED = 0,
    ACCOUNT,
    ENTITY,
    PERIOD,
    EMPLOYEE
  }


  public class Dimension<T> where T : CRUDEntity
  {
    public SafeDictionary<string, T> m_values = new SafeDictionary<string, T>();
    public Alignment m_alignment { get; set; }
    public SnapshotResult m_snapshotResult;
    public DimensionType m_dimensionType { get; private set; }
    private Worksheet m_worksheet;
    
    public Dimension(DimensionType p_dimensionType, Worksheet p_worksheet)
    {
      m_dimensionType = p_dimensionType;
      m_alignment = Alignment.UNDEFINED;
      m_worksheet = p_worksheet;
    }

    public bool AddValue(Range p_range, T p_dimensionObject)
    {
      if (m_values.Keys.Contains(p_range.Address) || m_values.Values.Contains(p_dimensionObject))
        return (false);
      m_values.Add(p_range.Address, p_dimensionObject);
      return true;
    }

    public void SetAlignment()
    {
      m_alignment = GetAlignment();
    }

    private Alignment GetAlignment()
    {
      switch (m_values.Count)
      {
        case 0:
          m_snapshotResult = SnapshotResult.EMPTY;
          return Alignment.UNDEFINED;

        case 1:
          m_snapshotResult = SnapshotResult.ONE;
          return Alignment.UNDEFINED;
          
        default:
          m_snapshotResult = SnapshotResult.SEVERAL;
          break;
      }

      if (m_snapshotResult == SnapshotResult.EMPTY)
        return Alignment.UNDEFINED;

      // Based on differences between rows and column :
      Range l_lastCell = m_worksheet.Range[m_values.ElementAt(m_values.Count - 1).Key];
      Int32 l_deltaRows = l_lastCell.Row - m_worksheet.Range[m_values.Keys.ElementAt(0)].Row;
      Int32 l_deltaColumns = l_lastCell.Column - m_worksheet.Range[m_values.Keys.ElementAt(0)].Column;

      if (l_deltaRows > 0 && l_deltaColumns > 0)
        return Alignment.UNDEFINED;
        
      if (l_deltaRows > l_deltaColumns)
        return Alignment.VERTICAL;
      else 
        if (l_deltaColumns > l_deltaRows)
          return Alignment.HORIZONTAL;
        else
          return Alignment.UNDEFINED;
    }

    public Int32 CellColumnIndex
    {
      get
      {
        if (m_values.Count > 0)
          return m_worksheet.Range[m_values.ElementAt(0).Key].Column;
        return 0;
      }
    }

    public Int32 CellRowIndex
    {
      get
      {
        if (m_values.Count > 0)
          return m_worksheet.Range[m_values.ElementAt(0).Key].Row;
        return 0;
      }
    }

    public Range UniqueCell
    {     
      get
      {
        if (m_values.Count > 0)
          return m_worksheet.Range[m_values.ElementAt(0).Key];
        else
          return null;
      }
    }

    public CRUDEntity UniqueValue
    {
      get
      {
        if (m_values.Count > 0)
          return m_values.ElementAt(0).Value;
        else
          return null;
      }
    }

  }
}
