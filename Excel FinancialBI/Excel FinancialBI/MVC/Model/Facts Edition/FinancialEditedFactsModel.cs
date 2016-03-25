using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Model
{
  using Model.CRUD;
  using View;
  using Network;
  using Utils;
  using Model;
  using Controller;
 
  class FinancialEditedFactsModel : AEditedFactsModel
  {
    public MultiIndexDictionary<string, DimensionKey, EditedFinancialFact> EditedFacts { get; private set; }
    SafeDictionary<DimensionKey, Fact> m_facts = new SafeDictionary<DimensionKey, Fact>();
    public MultiIndexDictionary<string, DimensionKey, EditedFinancialFact> OutputFacts { get; private set; }
    WorksheetAreaController m_dimensions = null;
    public event FactsCommitError OnCommitError;
    List<EditedFinancialFact> m_factsToBeCommitted = new List<EditedFinancialFact>(); // ? to be confirmed
    Worksheet m_worksheet;
    private bool m_updateCellsOnDownload;
    UInt32 m_versionId;
    private List<Int32> m_periodsList;
    bool m_needRefresh = false;

    public FinancialEditedFactsModel()
    {
      EditedFacts = new MultiIndexDictionary<string, DimensionKey, EditedFinancialFact>();
      OutputFacts = new MultiIndexDictionary<string, DimensionKey, EditedFinancialFact>();
    }

    public override void RegisterEditedFacts(WorksheetAreaController p_dimensions, Worksheet p_worksheet, UInt32 p_versionId, bool p_displayInitialDifferences, UInt32 p_RHAccountId = 0)
    {
      m_worksheet = p_worksheet;
      m_dimensions = p_dimensions;
      m_versionId = p_versionId;

      Version l_version = VersionModel.Instance.GetValue(m_versionId);

      if (l_version == null)
        m_periodsList = new List<int>();
      else
        m_periodsList = PeriodModel.GetPeriodList((int)l_version.StartPeriod, (int)l_version.NbPeriod, l_version.TimeConfiguration);
      Dimension<CRUDEntity> l_vertical = p_dimensions.Dimensions[p_dimensions.Orientation.Vertical];
      Dimension<CRUDEntity> l_horitontal = p_dimensions.Dimensions[p_dimensions.Orientation.Horizontal];
      Dimension<CRUDEntity> l_tabDimension = p_dimensions.Dimensions[p_dimensions.Orientation.TabDimension];

      CreateEditedFacts(l_vertical, l_horitontal, l_tabDimension);

      // TO DO Once facts registered clean dimensions and put them into a safe dictionary ?
    }

    ~FinancialEditedFactsModel()
    {
      //FactsModel.Instance.UpdateEvent -= AfterFactsCommit;
      // dispose edited facts
            EditedFacts.Clear();
      OutputFacts.Clear();
      // EMPTY dictionnaries etc.
    }

    public override void UnsubsribeEvents()
    {
      //  l_editedFact.OnCellValueChanged += new CellValueChangedEventHandler(p_rangeHighlighter.FillCellColor);
     

    }

    private void CreateEditedFacts(Dimension<CRUDEntity> p_rowsDimension, Dimension<CRUDEntity> p_columnsDimension, Dimension<CRUDEntity> p_fixedDimension)
    {
      foreach (KeyValuePair<string, CRUDEntity> l_rowsKeyPair in p_rowsDimension.m_values)
      {
        foreach (KeyValuePair<string, CRUDEntity> l_columnsKeyPair in p_columnsDimension.m_values)
        {
          Range l_rowCell = m_worksheet.Range[l_rowsKeyPair.Key];
          Range l_columnCell = m_worksheet.Range[l_columnsKeyPair.Key];
          Range l_factCell = m_worksheet.Cells[l_rowCell.Row, l_columnCell.Column] as Range;
          EditedFinancialFact l_editedFact = CreateEditedFact(p_rowsDimension, l_rowsKeyPair.Value, p_columnsDimension, l_columnsKeyPair.Value, p_fixedDimension, l_factCell);
          if (l_editedFact != null)
          {
            // Set Edited Value           
            Account.FormulaTypes l_formulaType = l_editedFact.Account.FormulaType;
            if (l_formulaType == Account.FormulaTypes.HARD_VALUE_INPUT || (l_formulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT && m_periodsList.FirstOrDefault() == l_editedFact.Period))
              EditedFacts.Set(l_factCell.Address, new DimensionKey(l_editedFact.EntityId, l_editedFact.AccountId, l_editedFact.EmployeeId, (Int32)l_editedFact.Period), l_editedFact);
            else
              OutputFacts.Set(l_factCell.Address, new DimensionKey(l_editedFact.EntityId, l_editedFact.AccountId, l_editedFact.EmployeeId, (Int32)l_editedFact.Period), l_editedFact);
          }
        }
      }
    }

    private EditedFinancialFact CreateEditedFact(Dimension<CRUDEntity> p_dimension1, CRUDEntity p_dimensionValue1,
                                        Dimension<CRUDEntity> p_dimension2, CRUDEntity p_dimensionValue2,
                                        Dimension<CRUDEntity> p_fixedDimension,
                                        Range p_cell)
    {
      UInt32 l_accountId = 0;
      UInt32 l_entityId = 0;
      UInt32 l_clientId = (UInt32)AxisType.Client;
      UInt32 l_productId = (UInt32)AxisType.Product;
      UInt32 l_adjustmentId = (UInt32)AxisType.Adjustment;
      UInt32 l_employeeId = (UInt32)AxisType.Employee;
      PeriodDimension l_period = null;

      WorksheetAreaController.SetDimensionValue(p_dimension1, p_dimensionValue1, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      WorksheetAreaController.SetDimensionValue(p_dimension2, p_dimensionValue2, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      WorksheetAreaController.SetDimensionValue(p_fixedDimension, p_fixedDimension.UniqueValue, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);

      if (l_accountId == 0 || l_entityId == 0 || l_period == null)
        return null;
      return new EditedFinancialFact(l_accountId, l_entityId, l_clientId, l_productId, l_adjustmentId, l_employeeId, m_versionId, l_period, p_cell);
    }

    public override void DownloadFacts(List<Int32> p_periodsList, bool p_updateCells, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      // flush m_editedFacts ?
      // flush m_facts ?
      // flush m_outputsFacts ?

      m_updateCellsOnDownload = p_updateCells;
      List<AxisElem> l_entitiesList = m_dimensions.GetAxisElemList(DimensionType.ENTITY);

      RequestIdList.Clear();
      foreach (AxisElem l_entity in l_entitiesList)
        RequestIdList.Add(FactsModel.Instance.GetFactFinancial(l_entity.Id, m_versionId, p_clientId, p_productId, p_adjustmentId));
    }


    // TO DO : must update worksheet flag
    //
    public bool FillFactsDictionnaries(List<Fact> p_factsList)
    {
      foreach (Fact l_fact in p_factsList)
      {
        DimensionKey l_dimensionKey = new DimensionKey(l_fact.EntityId, l_fact.AccountId, (UInt32)AxisType.Employee, (Int32)l_fact.Period);
        EditedFinancialFact l_EditedFact = EditedFacts[l_dimensionKey];
        if (l_EditedFact != null)
        {
          l_EditedFact.UpdateFinancialFact(l_fact);
          if (m_updateCellsOnDownload)
            l_EditedFact.Cell.Value2 = l_EditedFact.Value;
        }
        else
          m_facts[l_dimensionKey] = l_fact;  // Fact not on worksheet, saved as fact
      }
      return (true);
    }

    public void ComputeOutputs()
    {
      SourcedComputeRequest l_sourcedComputeRequest = new SourcedComputeRequest();
      Version l_version = VersionModel.Instance.GetValue(m_versionId);

      if (l_version == null)
        return;
      l_sourcedComputeRequest.VersionId = m_versionId;
      l_sourcedComputeRequest.StartPeriod = (int)l_version.StartPeriod;
      l_sourcedComputeRequest.NbPeriods = l_version.NbPeriod;
      l_sourcedComputeRequest.GlobalFactVersionId = l_version.GlobalFactVersionId;
      l_sourcedComputeRequest.RateVersionId = l_version.RateVersionId;
      List<Fact> l_factsList = new List<Fact>();
      foreach (EditedFinancialFact l_editedFact in EditedFacts.Values)
        if (l_editedFact.Value != 0)
          l_factsList.Add(l_editedFact);
      l_sourcedComputeRequest.FactList = l_factsList;
      l_sourcedComputeRequest.Process = Account.AccountProcess.FINANCIAL;

      List<UInt32> l_entitiesList = new List<UInt32>();
      foreach (AxisElem l_entity in m_dimensions.Entities.m_values.Values)
      {
        EntityCurrency l_currency = EntityCurrencyModel.Instance.GetValue(l_entity.Id);

        if (l_currency == null)
          l_sourcedComputeRequest.CurrencyId = Properties.Settings.Default.currentCurrency;
        else
          l_sourcedComputeRequest.CurrencyId = l_currency.CurrencyId;
        l_entitiesList.Add(l_entity.Id);
      }
      l_sourcedComputeRequest.EntityList = l_entitiesList;
      SourcedComputeModel.Instance.Compute(l_sourcedComputeRequest);
    }

    public override void Refresh()
    {
      if (m_needRefresh)
        ComputeOutputs();
      m_needRefresh = false;
    }

    public override bool UpdateEditedValueAndTag(Range p_cell)
    {
      if (EditedFacts.ContainsKey(p_cell.Address))
      {
        EditedFacts[p_cell.Address].Value = (double)p_cell.Value2;
        m_needRefresh = true;
        return true;
      }
      return false;
    }

    public void UpdateWorksheetInputs()
    {
      foreach (EditedFinancialFact l_editedFact in EditedFacts.Values)
      {
       // l_editedFact.UpdateCellValue();
      }
    }

    public override void Commit()
    {
      SafeDictionary<string, Fact> l_dic = new SafeDictionary<string, Fact>();

      foreach (EditedFinancialFact l_editedFact in EditedFacts.Values)
      {
        if (l_editedFact.Value == l_editedFact.EditedValue)
          continue;
        l_dic[l_editedFact.Cell.Address] = l_editedFact;
      }

      if (l_dic.Count > 0)
        FactsModel.Instance.UpdateList(l_dic, CRUDAction.UPDATE);
      // TO DO
      // Loop through facts : if to be commited  add to update list
      // Send EditedFacts to the model  

      // associate updates to cells
      // ready to listen server answer
      //   -> update cells color on worksheet if success
    }

    // TO DO : After commit event

    public override double? CellBelongToOutput(Range p_cell)
    {
      EditedFinancialFact l_fact = OutputFacts[p_cell.Address];

      if (l_fact == null)
        return (null);
      return (l_fact.Value);
    }

    #region Utils

    //private double GetFactValue() 
    //{
    //  // below : goes into controller
    //  if (Cell.Value2 == null)
    //    return 0;
    //  double l_doubleValue;
    //  if (Cell.Value2.GetType() == typeof(string))
    //  {
    //    if (Double.TryParse(Cell.Value2 as string, out l_doubleValue))
    //      return l_doubleValue;
    //    else return 0;
    //  }
    //  return 0;
    //}

    #endregion


  }
}
