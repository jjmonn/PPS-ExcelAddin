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

  struct Orientation
  {
    public Orientation(DimensionType p_vertical, DimensionType p_horizontal, DimensionType p_tabDimension)
    {
      Vertical = p_vertical;
      Horizontal = p_horizontal;
      TabDimension = p_tabDimension;
    }
    public DimensionType Vertical;
    public DimensionType Horizontal;
    public DimensionType TabDimension;
    public bool IsValid
    {
      get { return Vertical != DimensionType.UNDEFINED && Horizontal != DimensionType.UNDEFINED && TabDimension != DimensionType.UNDEFINED; }
    }
  };

  class Dimensions
  {
    List<Int32> m_periodsDatesList = new List<Int32>();

    public Orientation m_orientation { private set; get; }
    public SafeDictionary<DimensionType, Dimension<CRUDEntity>> m_dimensions = new SafeDictionary<DimensionType,Dimension<CRUDEntity>>();
    public Dimension<CRUDEntity> m_periods { get { return (m_dimensions[DimensionType.PERIOD]); } }
    public Dimension<CRUDEntity> m_accounts { get { return (m_dimensions[DimensionType.ACCOUNT]); } }
    public Dimension<CRUDEntity> m_entities { get { return (m_dimensions[DimensionType.ENTITY]); } }
    public Dimension<CRUDEntity> m_employees { get { return (m_dimensions[DimensionType.EMPLOYEE]); } }
    public Account.AccountProcess m_process { get; private set; }

    public Dimensions(UInt32 p_versionId, List<Int32> p_periodsList = null)
    {
      foreach (DimensionType l_dim in Enum.GetValues(typeof(DimensionType)))
        m_dimensions[l_dim] = new Dimension<CRUDEntity>(l_dim);

      if (p_periodsList == null)
        p_periodsList = PeriodModel.GetPeriodsList(p_versionId);

      m_periodsDatesList = p_periodsList;
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
      if (RegisterAccount(p_cell) == true)
        return;
      if (RegisterEntity(p_cell) == true)
        return;
    }

    private void DimensionsIdentifyRH(Range p_cell)
    {
      if (RegisterEmployee(p_cell) == true)
        return;
      if (m_entities.m_values.Count > 0) // only register the first entity found
        return;
      if (RegisterEntity(p_cell) == true)
      return;
  }

    public bool RegisterPeriod(Range p_cell)
    {
      UInt32 l_periodAsInt = Convert.ToUInt32(Convert.ToDateTime((p_cell).Value).ToOADate());
      PeriodDimension l_period = new PeriodDimension(l_periodAsInt);
      Int32 test = (Int32)Convert.ToDateTime(p_cell.Value).ToOADate();
      if (m_periodsDatesList.Contains((Int32)Convert.ToDateTime(p_cell.Value).ToOADate()))
        return m_periods.AddValue(p_cell, l_period); ;
      return false;
    }

    private bool RegisterAccount(Range p_cell)
    {
      Account l_account = AccountModel.Instance.GetValue(p_cell.Value2 as string);

      if (l_account == null)
        return (false);
      return m_accounts.AddValue(p_cell, l_account);
    }

    private bool RegisterEntity(Range p_cell)
    {
      AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, p_cell.Value2 as string);

      if (l_entity == null)
        return (false);
      return m_entities.AddValue(p_cell, l_entity);
    }

    public bool RegisterEmployee(Range p_cell)
    {
      AxisElem l_employee = AxisElemModel.Instance.GetValue(AxisType.Employee, p_cell.Value2 as string);

      if (l_employee == null)
        return (false);
      return m_employees.AddValue(p_cell, l_employee);
    }

    #endregion

    public bool IsValid()
    {
      return (m_orientation.IsValid);
    }

    #region Orientation definition

    public bool DefineOrientation(Account.AccountProcess p_process)
    {
      DefineDimensionsAlignments();
      m_orientation = new Orientation();

      if (m_periods.m_snapshotResult == SnapshotResult.EMPTY || m_entities.m_snapshotResult == SnapshotResult.EMPTY)
        return (false);

      switch (p_process)
      {
        case Account.AccountProcess.FINANCIAL:
          if (m_accounts.m_snapshotResult == SnapshotResult.EMPTY)
            return (false);
          return SetFinancialDimensionsOrientation();

        case Account.AccountProcess.RH:
          if (m_employees.m_snapshotResult == SnapshotResult.EMPTY)
            return (false);
          else
            return SetRHDimensionsOrientation();
        default:
          return (false);
      }
      // TO DO : if orientations valid : highlight dimensions cells -> maybe in controller according to the return value of this function
      //   Attention : TO DO -> RH Process account Id definition check
    }

    private void DefineDimensionsAlignments()
    {
      foreach (Dimension<CRUDEntity> l_dimension in m_dimensions.Values)
        l_dimension.SetAlignment();
    }

    private bool SetFinancialDimensionsOrientation()
    {
      switch (String.Concat(m_periods.m_snapshotResult, m_accounts.m_snapshotResult, m_entities.m_snapshotResult))
      {
        case "111":
          SetDimensionsAlignmentCaseOneFact();
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
          SetDimensionsAlignmentCaseOneFact();
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

    DimensionType FindDimension(Range p_cell)
    {
      foreach (KeyValuePair<DimensionType, Dimension<CRUDEntity>> l_pair in m_dimensions)
        if (p_cell == l_pair.Value.UniqueCell)
          return (l_pair.Key);
      return (DimensionType.UNDEFINED);
    }

    void InitDimensions()
    {
      foreach (Dimension<CRUDEntity> l_dimension in m_dimensions.Values)
        l_dimension.m_alignment = Alignment.UNDEFINED;
    }

    private void SetDimensionsAlignmentCaseOneFact()
    {
      Range l_maxRightCell = null;
      Range l_maxBelowCell = null;
      DimensionType l_dimensionVertical;
      DimensionType l_dimensionHorizontal;

      InitDimensions();
      SetCellsMaxsBelowAndRight(l_maxRightCell, l_maxBelowCell);
      l_dimensionVertical = FindDimension(l_maxBelowCell);
      l_dimensionHorizontal = FindDimension(l_maxRightCell);

      if (l_maxRightCell.Row == l_maxBelowCell.Row || l_maxRightCell.Column == l_maxBelowCell.Column ||
        l_dimensionHorizontal == DimensionType.UNDEFINED || l_dimensionVertical == DimensionType.UNDEFINED)
        return;
      m_dimensions[l_dimensionVertical].m_alignment = Alignment.VERTICAL;
      m_dimensions[l_dimensionHorizontal].m_alignment = Alignment.HORIZONTAL;
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

    DimensionType FindDimensionOriented(Alignment p_orientation)
    {
      foreach (KeyValuePair<DimensionType, Dimension<CRUDEntity>> l_pair in m_dimensions)
        if (l_pair.Value.m_alignment == p_orientation)
          return (l_pair.Key);
      return (DimensionType.UNDEFINED);
    }

    DimensionType FindTabDimension()
    {
      foreach (KeyValuePair<DimensionType, Dimension<CRUDEntity>> l_pair in m_dimensions)
        if (l_pair.Value.m_alignment == Alignment.UNDEFINED && l_pair.Value.m_values.Count == 1)
          return (l_pair.Key);
      return (DimensionType.UNDEFINED);
    }

    private bool DefineGlobalOrientation()
    {
      m_orientation = new Orientation(FindDimensionOriented(Alignment.VERTICAL), FindDimensionOriented(Alignment.HORIZONTAL), FindTabDimension());
      return (m_orientation.IsValid);
    }

    private void SetCellsMaxsBelowAndRight(Range p_maxRightCell, Range p_maxBelowCell)
    {
      SafeDictionary<Range, Int32> l_rightCellsDict = new SafeDictionary<Range, Int32>();
      l_rightCellsDict.Add(m_periods.m_values.ElementAt(0).Key, m_periods.CellColumnIndex);
      l_rightCellsDict.Add(m_accounts.m_values.ElementAt(0).Key, m_accounts.CellColumnIndex);
      l_rightCellsDict.Add(m_entities.m_values.ElementAt(0).Key, m_entities.CellColumnIndex);
      l_rightCellsDict.Add(m_employees.m_values.ElementAt(0).Key, m_employees.CellColumnIndex);
      IOrderedEnumerable<KeyValuePair<Range, Int32>> l_columnsSortedDict = from entry in l_rightCellsDict orderby entry.Value ascending select entry;
      p_maxRightCell = l_columnsSortedDict.ElementAt(0).Key;

      SafeDictionary<Range, Int32> l_belowCellsDict = new SafeDictionary<Range, Int32>();
      l_belowCellsDict.Add(m_periods.m_values.ElementAt(0).Key, m_periods.CellColumnIndex);
      l_belowCellsDict.Add(m_accounts.m_values.ElementAt(0).Key, m_accounts.CellColumnIndex);
      l_belowCellsDict.Add(m_entities.m_values.ElementAt(0).Key, m_entities.CellColumnIndex);
      l_belowCellsDict.Add(m_employees.m_values.ElementAt(0).Key, m_employees.CellColumnIndex);
      IOrderedEnumerable<KeyValuePair<Range, Int32>> l_rowsSortedDict = from entry in l_belowCellsDict orderby entry.Value ascending select entry;
      p_maxBelowCell = l_rowsSortedDict.ElementAt(0).Key;
    }

    #endregion

    public static void SetDimensionValue(Dimension<CRUDEntity> p_dimension, CRUDEntity p_value, ref UInt32 p_accountId, ref UInt32 p_entityId, ref UInt32 p_employeeId, ref PeriodDimension p_period)
    {
      switch (p_dimension.m_dimensionType)
      {
        case DimensionType.ACCOUNT:
          p_accountId = p_value.Id;
          break;

        case DimensionType.ENTITY:
          p_entityId = p_value.Id;
          break;

        case DimensionType.EMPLOYEE:
          p_employeeId = p_value.Id;
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
        l_list.Add(l_account as Account);
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
        default:
          l_dimension = null;
          break;
      }

      if (l_dimension == null)
        return l_list;

      foreach (CRUDEntity l_item in l_dimension.m_values.Values)
        l_list.Add(l_item as AxisElem);
      return l_list;
    }
  }
}
