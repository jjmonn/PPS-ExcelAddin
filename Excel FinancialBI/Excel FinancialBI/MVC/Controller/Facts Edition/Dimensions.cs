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


  class Dimensions
  {
    public enum Orientation
    {
      ACCOUNTS_PERIODS = 0,
      PERIODS_ACCOUNTS,
      ENTITIES_ACCOUNTS,
      ACCOUNTS_ENTITIES,
      PERIODS_ENTITIES,
      ENTITIES_PERIODS,
      EMPLOYEES_PERIODS,
      PERIODS_EMPLOYEES,
      FINANCIAL_ORIENTATION_ERROR,
      PDC_ORIENTATION_ERROR,
      ORIENTATION_ERROR
    }

    List<DateTime> m_periodsDatesList = new List<DateTime>();
    
    public Orientation m_orientation {private set; get;}
    public Account.AccountProcess m_process { get; private set; }
    public Dimension<CRUDEntity> m_accounts { get; set; }
    public Dimension<CRUDEntity> m_entities { get; set; }
    public Dimension<CRUDEntity> m_employees { get; set; }
    public Dimension<CRUDEntity> m_periods { get; set; }

    public Dimensions(UInt32 p_versionId, List<Int32> p_periodsList = null)
    {
      m_accounts = new Dimension<CRUDEntity>(DimensionType.ACCOUNT);
      m_entities = new Dimension<CRUDEntity>(DimensionType.ENTITY);
      m_employees = new Dimension<CRUDEntity>(DimensionType.EMPLOYEE);
      m_periods = new Dimension<CRUDEntity>(DimensionType.PERIOD);

      if (p_periodsList == null)
      {
        p_periodsList = PeriodModel.GetPeriodsList(p_versionId);
      }
      foreach (UInt32 periodId in p_periodsList)
      {
        m_periodsDatesList.Add(DateTime.FromOADate(periodId));
      }
    }

    #region Dimensions identification methods

    public void DimensionsIdentify(Range p_cell)
    {
      if (m_process == Account.AccountProcess.FINANCIAL)
        DimensionsIdentifyFinancial(p_cell);
      else
        DimensionsIdentifyRH(p_cell);
    }

    private void DimensionsIdentifyFinancial(Range p_cell)
    {
      if (p_cell.Value2.GetType() == typeof(string))
      {
        if (IsAccount(p_cell) == true)
          return;
        if (IsEntity(p_cell) == true)
          return;
      }
    }

    private void DimensionsIdentifyRH(Range p_cell)
    {
      if (p_cell.Value2.GetType() == typeof(string))
      {
        if (IsEmployee(p_cell) == true)
          return;
        if (IsEntity(p_cell) == true)
          return;
      }
    }

    public bool IsPeriod(Range p_cell)
    {
      UInt32 l_periodAsInt = Convert.ToUInt32(Convert.ToDateTime((p_cell).Value).ToOADate());
      PeriodDimension l_period = new PeriodDimension(l_periodAsInt);
      if (m_periodsDatesList.Contains(Convert.ToDateTime(p_cell.Value)) && !m_periods.m_values.Values.Contains(l_period))
      {
        m_periods.m_values.Add(p_cell, l_period);
        return true;
      }
      return false;

    }

    private bool IsAccount(Range p_cell)
    {
      Account l_account = AccountModel.Instance.GetValue(p_cell.Value2 as string);
      if (l_account == null)
        return (false);
      return RegisterAccount(p_cell, l_account);
    }

    private bool RegisterAccount(Range p_cell, Account p_account)
    {
      return m_accounts.AddValue(p_cell, p_account);
     }

    private bool IsEntity(Range p_cell)
    {
      AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, p_cell.Value2 as string);
      if (l_entity == null)
        return (false);

      return m_entities.AddValue(p_cell, l_entity);
    }

    public bool IsEmployee(Range p_cell)
    {
      AxisElem l_employee = AxisElemModel.Instance.GetValue(AxisType.Employee, p_cell.Value2 as string);
      if (l_employee == null)
        return (false);

      return m_employees.AddValue(p_cell, l_employee);
    }

    #endregion

    public bool IsValid()
    {
      if (m_orientation == Orientation.ORIENTATION_ERROR)
        return false;
      return true;
    }

    #region Orientation definition

    public bool DefineOrientation(Account.AccountProcess p_process)
    {
      DefineDimensionsAlignments();
   
     if (m_periods.m_snapshotResult == SnapshotResult.EMPTY || m_entities.m_snapshotResult == SnapshotResult.EMPTY)
      {
        m_orientation = Orientation.ORIENTATION_ERROR;
        return (false);
      }

      switch (p_process)
      {
        case Account.AccountProcess.FINANCIAL:
          if (m_accounts.m_snapshotResult == SnapshotResult.EMPTY)
          {
            m_orientation = Orientation.ORIENTATION_ERROR;
            return (false);
          }
         return SetFinancialDimensionsOrientation();

        case Account.AccountProcess.RH:
          if (m_employees.m_snapshotResult == SnapshotResult.EMPTY)
          {
            m_orientation = Orientation.ORIENTATION_ERROR;
            return (false);
          }
          else
            return SetRHDimensionsOrientation();
    
        default : 
          return (false);
      }
      // TO DO : if orientations valid : highlight dimensions cells -> maybe in controller according to the return value of this function
      //   Attention : TO DO -> RH Process account Id definition check
    }

    private void DefineDimensionsAlignments()
    {
      m_accounts.SetAlignment();
      m_entities.SetAlignment();
      m_employees.SetAlignment();
      m_periods.SetAlignment();
    }

    private bool SetFinancialDimensionsOrientation()
    {
      switch (String.Concat(m_periods.m_snapshotResult, m_accounts.m_snapshotResult, m_entities.m_snapshotResult))
      {
        case "111":
          SetDimensionsOrientationsCaseOneFact();
          break;

        case "112":
          SetDimensionsAlignmentCellsOtherCases(m_entities, m_accounts, m_periods);
          break;

        case "121":
          SetDimensionsAlignmentCellsOtherCases(m_accounts, m_entities, m_periods);
          break;

        case "211":
          SetDimensionsAlignmentCellsOtherCases(m_periods, m_entities, m_accounts);
          break;       
      }
     return DefineGlobalOrientation();
    }

    private bool SetRHDimensionsOrientation()
    {
      switch (String.Concat(m_periods.m_snapshotResult, m_employees.m_snapshotResult))
      {
        case "11":
          // One Period One Entity
          SetDimensionsOrientationsCaseOneFact();
          break;

        case "12":
          // One period Several Entities
          SetDimensionsAlignmentCellsOtherCases(m_entities, m_periods);
          break;

        case "21":
          // Several Periods One Entity
          SetDimensionsAlignmentCellsOtherCases(m_periods, m_entities);
          break;
      }
      return DefineGlobalOrientation();
    }

    private void SetDimensionsOrientationsCaseOneFact()
    {
      Range l_maxRightCell = null;
      Range l_maxBelowCell = null;
      SetCellsMaxsBelowAndRight(l_maxRightCell, l_maxBelowCell);

      // Entities Accounts
      if (l_maxBelowCell == m_entities.UniqueCell && l_maxRightCell == m_accounts.UniqueCell
          && m_entities.CellRowIndex != m_accounts.CellRowIndex && m_entities.CellColumnIndex != m_accounts.CellColumnIndex)
      {
        m_entities.m_alignment = Alignment.VERTICAL;
        m_accounts.m_alignment = Alignment.HORIZONTAL;
        return;
      }

      // Accounts Entities
      if (l_maxBelowCell == m_accounts.UniqueCell && l_maxRightCell == m_entities.UniqueCell
          && m_accounts.CellRowIndex != m_entities.CellRowIndex && m_accounts.CellColumnIndex != m_entities.CellColumnIndex)
      {
        m_accounts.m_alignment = Alignment.VERTICAL;
        m_entities.m_alignment = Alignment.HORIZONTAL;
        return;
      }

      // Entities Periods
      if (l_maxBelowCell == m_entities.UniqueCell && l_maxRightCell == m_periods.UniqueCell
          && m_entities.CellRowIndex != m_periods.CellRowIndex && m_entities.CellColumnIndex != m_periods.CellColumnIndex)
      {
        m_entities.m_alignment = Alignment.VERTICAL;
        m_periods.m_alignment = Alignment.HORIZONTAL;
        return;
      }

      // Periods Entities
      if (l_maxBelowCell == m_periods.UniqueCell && l_maxRightCell == m_entities.UniqueCell
          && m_periods.CellRowIndex != m_entities.CellRowIndex && m_periods.CellColumnIndex != m_entities.CellColumnIndex)
      {
        m_periods.m_alignment = Alignment.VERTICAL;
        m_entities.m_alignment = Alignment.HORIZONTAL;
        return;
      }

      // Accounts Periods
      if (l_maxBelowCell == m_accounts.UniqueCell && l_maxRightCell == m_periods.UniqueCell
          && m_accounts.CellRowIndex != m_periods.CellRowIndex && m_accounts.CellColumnIndex != m_periods.CellColumnIndex)
      {
        m_accounts.m_alignment = Alignment.VERTICAL;
        m_periods.m_alignment = Alignment.HORIZONTAL;
        return;
      }

      // Periods Accounts
      if (l_maxBelowCell == m_periods.UniqueCell && l_maxRightCell == m_accounts.UniqueCell
          && m_periods.CellRowIndex != m_accounts.CellRowIndex && m_periods.CellColumnIndex != m_accounts.CellColumnIndex)
      {
        m_periods.m_alignment = Alignment.VERTICAL;
        m_accounts.m_alignment = Alignment.HORIZONTAL;
        return;
      }

      // Periods Employees
      if (l_maxBelowCell == m_periods.UniqueCell && l_maxRightCell == m_employees.UniqueCell
          && m_periods.CellRowIndex != m_employees.CellRowIndex && m_periods.CellColumnIndex != m_employees.CellColumnIndex)
      {
        m_periods.m_alignment = Alignment.VERTICAL;
        m_employees.m_alignment = Alignment.HORIZONTAL;
        return;
      }

      // Employees Periods
      if (l_maxBelowCell == m_employees.UniqueCell && l_maxRightCell == m_periods.UniqueCell
          && m_employees.CellRowIndex != m_periods.CellRowIndex && m_employees.CellColumnIndex != m_periods.CellColumnIndex)
      {
        m_periods.m_alignment = Alignment.VERTICAL;
        m_employees.m_alignment = Alignment.HORIZONTAL;
        return;
      }

     m_periods.m_alignment = Alignment.UNDEFINED;
      m_accounts.m_alignment = Alignment.UNDEFINED;
      m_entities.m_alignment = Alignment.UNDEFINED;
      m_employees.m_alignment = Alignment.UNDEFINED;
    }

    private void SetDimensionsAlignmentCellsOtherCases(Dimension<CRUDEntity> p_definedDimension, Dimension<CRUDEntity> p_dimension1, Dimension<CRUDEntity> p_dimension2)
    {
      if (p_definedDimension.m_alignment == Alignment.HORIZONTAL)
      {
        if (p_dimension1.CellRowIndex > p_dimension2.CellRowIndex)
          SetDimensionAlignment(p_definedDimension, p_dimension1, Alignment.VERTICAL);
        else
          SetDimensionAlignment(p_definedDimension, p_dimension2, Alignment.VERTICAL);
      }
      else
      {
        if (p_dimension1.CellColumnIndex > p_dimension2.CellColumnIndex)
          SetDimensionAlignment(p_definedDimension, p_dimension1, Alignment.HORIZONTAL);
        else
          SetDimensionAlignment(p_definedDimension, p_dimension2, Alignment.HORIZONTAL);
      }
    }

    private void SetDimensionsAlignmentCellsOtherCases(Dimension<CRUDEntity> p_definedDimension, Dimension<CRUDEntity> p_dimensionToBeDefined)
    {
      if (p_definedDimension.m_alignment == Alignment.HORIZONTAL)
        SetDimensionAlignment(p_definedDimension, p_definedDimension, Alignment.VERTICAL);
      else
        SetDimensionAlignment(p_definedDimension, p_definedDimension, Alignment.HORIZONTAL);
    }

    private void SetDimensionAlignment(Dimension<CRUDEntity> p_definedDimension, Dimension<CRUDEntity> p_dimension, Alignment p_alignment)
    {
      if (p_dimension.CellColumnIndex != p_definedDimension.CellColumnIndex && p_dimension.CellRowIndex != p_definedDimension.CellRowIndex)
        p_dimension.m_alignment = p_alignment;
    }

    private bool DefineGlobalOrientation()
    {

      if (m_accounts.m_alignment == Alignment.VERTICAL && m_entities.m_alignment == Alignment.HORIZONTAL)
      {
        m_orientation = Orientation.ACCOUNTS_ENTITIES;
        return (true);
      }

      if (m_accounts.m_alignment == Alignment.VERTICAL && m_periods.m_alignment == Alignment.HORIZONTAL)
      {
        m_orientation = Orientation.ACCOUNTS_PERIODS;
        return (true);
      }

      if (m_periods.m_alignment == Alignment.VERTICAL && m_accounts.m_alignment == Alignment.HORIZONTAL)
      {
        m_orientation = Orientation.PERIODS_ACCOUNTS;
        return (true);
      }

      if (m_periods.m_alignment == Alignment.VERTICAL && m_entities.m_alignment == Alignment.HORIZONTAL)
      {
        m_orientation = Orientation.PERIODS_ENTITIES;
        return (true);
      }

      if (m_entities.m_alignment == Alignment.VERTICAL && m_accounts.m_alignment == Alignment.HORIZONTAL)
      {
        m_orientation = Orientation.ENTITIES_ACCOUNTS;
        return (true);
      }

      if (m_entities.m_alignment == Alignment.VERTICAL && m_periods.m_alignment == Alignment.HORIZONTAL)
      {
        m_orientation = Orientation.ENTITIES_PERIODS;
        return (true);
      }

      if (m_employees.m_alignment == Alignment.VERTICAL && m_periods.m_alignment == Alignment.HORIZONTAL)
      {
        m_orientation = Orientation.EMPLOYEES_PERIODS;
        return (true);
      }

      if (m_periods.m_alignment == Alignment.VERTICAL && m_employees.m_alignment == Alignment.HORIZONTAL)
      {
        m_orientation = Orientation.PERIODS_EMPLOYEES;
        return (true);
      }
      m_orientation = Orientation.ORIENTATION_ERROR;
      return (false);
    }

    private void SetCellsMaxsBelowAndRight(Range p_maxRightCell, Range p_maxBelowCell)
    {
      SafeDictionary<Range, Int32> l_rightCellsDict = new SafeDictionary<Range, Int32>();
      l_rightCellsDict.Add(m_periods.m_values.ElementAt(0).Key, m_periods.CellColumnIndex);
      l_rightCellsDict.Add(m_accounts.m_values.ElementAt(0).Key, m_accounts.CellColumnIndex);
      l_rightCellsDict.Add(m_entities.m_values.ElementAt(0).Key, m_entities.CellColumnIndex);
      l_rightCellsDict.Add(m_employees.m_values.ElementAt(0).Key, m_employees.CellColumnIndex);
      var l_columnsSortedDict = from entry in l_rightCellsDict orderby entry.Value ascending select entry;
      p_maxRightCell = l_columnsSortedDict.ElementAt(0).Key;

      SafeDictionary<Range, Int32> l_belowCellsDict = new SafeDictionary<Range, Int32>();
      l_belowCellsDict.Add(m_periods.m_values.ElementAt(0).Key, m_periods.CellColumnIndex);
      l_belowCellsDict.Add(m_accounts.m_values.ElementAt(0).Key, m_accounts.CellColumnIndex);
      l_belowCellsDict.Add(m_entities.m_values.ElementAt(0).Key, m_entities.CellColumnIndex);
      l_belowCellsDict.Add(m_employees.m_values.ElementAt(0).Key, m_employees.CellColumnIndex);
      var l_rowsSortedDict = from entry in l_belowCellsDict orderby entry.Value ascending select entry;
      p_maxBelowCell = l_rowsSortedDict.ElementAt(0).Key;
    }

    #endregion


    public static void SetDimensionValue(Dimension<CRUDEntity> p_dimension, CRUDEntity p_value, Account p_account, AxisElem p_entity, AxisElem p_employee, PeriodDimension p_period)
    {
      switch (p_dimension.m_dimensionType)
      {
        case DimensionType.ACCOUNT:
          p_account = p_value as Account;
          break;

        case DimensionType.ENTITY:
          p_entity = p_value as AxisElem;
          break;

        case DimensionType.EMPLOYEE:
          p_employee = p_value as AxisElem;
          break;

        case DimensionType.PERIOD:
          p_period = p_value as PeriodDimension;
          break;
      }
    }

    public List<Account> GetAccountsList()
    {
        List<Account> l_list = new List<Account>();
        foreach (CRUDEntity l_account in m_accounts.m_values.Values)
        {
            l_list.Add(l_account as Account);
        }
        return l_list;
    }

    public List<AxisElem> GetAxisElemList(DimensionType l_dimensionType)
    {
      List<AxisElem> l_list = new List<AxisElem>();
      Dimension<CRUDEntity> l_dimension;
      switch (l_dimensionType)
      {
        case DimensionType.EMPLOYEE:
          l_dimension = m_employees;
          break;
        case DimensionType.ENTITY:
          l_dimension = m_entities;
          break;
        default :
          l_dimension = null;
          break;
      }

      if (l_dimension == null)
        return l_list;

      foreach (CRUDEntity l_item in l_dimension.m_values.Values)
      {
        l_list.Add(l_item as AxisElem);
      }
      return l_list;
    } 

  }
}
