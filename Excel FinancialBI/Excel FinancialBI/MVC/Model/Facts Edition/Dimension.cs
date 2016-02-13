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
    VERTICAL,
    HORIZONTAL,
  }

  public enum DimensionType
  {
    ACCOUNT = 0,
    ENTITY,
    PERIOD,
    EMPLOYEE
  }


  class Dimension<T> where T : CRUDEntity
  {
    public SafeDictionary<Range, T> m_values = new SafeDictionary<Range, T>();
    public Alignment m_alignment { get; set; }
    public SnapshotResult m_snapshotResult;
    public DimensionType m_dimensionType { get; private set; }
    
    public Dimension(DimensionType p_dimensionType)
    {
      m_dimensionType = p_dimensionType;
    }

    public bool AddValue(Range p_range, T p_dimensionObject)
    {
      if (m_values.Keys.Contains(p_range) || m_values.Values.Contains(p_dimensionObject))
        return (false);
      m_values.Add(p_range, p_dimensionObject);
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
      Range l_lastCell = m_values.ElementAt(m_values.Count - 1).Key;
      Int32 l_deltaRows = l_lastCell.Row -   m_values.Keys.ElementAt(0).Row;
      Int32 l_deltaColumns = l_lastCell.Column - m_values.Keys.ElementAt(0).Column;

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
          return m_values.ElementAt(0).Key.Column;
        return 0;
      }
    }

    public Int32 CellRowIndex
    {
      get
      {
        if (m_values.Count > 0)
          return m_values.ElementAt(0).Key.Row;
        return 0;
      }
    }

    public Range UniqueCell
    {     
      get
      {
        if (m_values.Count > 0)
          return m_values.ElementAt(0).Key;
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
