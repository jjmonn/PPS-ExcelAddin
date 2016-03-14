using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Model.CRUD;
  using FBI.MVC.View;
  using Network;
  using Utils;

 
  class FinancialEditedFactsModel : IEditedFactsModel
  {
    MultiIndexDictionary<Range, DimensionKey, EditedFinancialFact> m_editedFacts = new MultiIndexDictionary<Range, DimensionKey, EditedFinancialFact>();
    SafeDictionary<DimensionKey, Fact> m_facts = new SafeDictionary<DimensionKey, Fact>();
    MultiIndexDictionary<Range, DimensionKey, EditedFinancialFact> m_outputFacts = new MultiIndexDictionary<Range, DimensionKey, EditedFinancialFact>();
    WorksheetAreaController m_dimensions = null;
    List<int> m_inputsRequestIdList = new List<int>();
    public event OnFactsDownloaded FactsDownloaded;
    public event FactsCommitError OnCommitError;
    List<EditedFinancialFact> m_factsToBeCommitted = new List<EditedFinancialFact>(); // ? to be confirmed
    Worksheet m_worksheet;
    private bool m_updateCellsOnDownload;
    UInt32 m_versionId;
    private List<Int32> m_periodsList;

    public void RegisterEditedFacts(WorksheetAreaController p_dimensions, Worksheet p_worksheet, UInt32 p_versionId, RangeHighlighter p_rangeHighlighter, bool p_displayInitialDifferences, UInt32 p_RHAccountId = 0)
    {
      m_worksheet = p_worksheet;
      m_dimensions = p_dimensions;
      m_versionId = p_versionId;
      Dimension<CRUDEntity> l_vertical = p_dimensions.Dimensions[p_dimensions.Orientation.Vertical];
      Dimension<CRUDEntity> l_horitontal = p_dimensions.Dimensions[p_dimensions.Orientation.Horizontal];
      Dimension<CRUDEntity> l_tabDimension = p_dimensions.Dimensions[p_dimensions.Orientation.TabDimension];

      CreateEditedFacts(l_vertical, l_horitontal, l_tabDimension, p_rangeHighlighter);

      // TO DO Once facts registered clean dimensions and put them into a safe dictionary ?
    }

    public void Dispose()
    {
      //FactsModel.Instance.UpdateEvent -= AfterFactsCommit;
      // dispose edited facts
      m_editedFacts.Clear();
      m_outputFacts.Clear();
      // EMPTY dictionnaries etc.
    }

    private void CreateEditedFacts(Dimension<CRUDEntity> p_rowsDimension, Dimension<CRUDEntity> p_columnsDimension, Dimension<CRUDEntity> p_fixedDimension, RangeHighlighter p_rangeHighlighter)
    {
      foreach (KeyValuePair<Range, CRUDEntity> l_rowsKeyPair in p_rowsDimension.m_values)
      {
        foreach (KeyValuePair<Range, CRUDEntity> l_columnsKeyPair in p_columnsDimension.m_values)
        {
          Range l_factCell = m_worksheet.Cells[l_rowsKeyPair.Key.Row, l_columnsKeyPair.Key.Column] as Range;
          EditedFinancialFact l_editedFact = CreateEditedFact(p_rowsDimension, l_rowsKeyPair.Value, p_columnsDimension, l_columnsKeyPair.Value, p_fixedDimension, l_factCell);
          if (l_editedFact != null)
          {
            // Set Edited Value     
            l_editedFact.OnCellValueChanged += new CellValueChangedEventHandler(p_rangeHighlighter.FillCellColor);
      
            Account.FormulaTypes l_formulaType = l_editedFact.Account.FormulaType;
            if (l_formulaType == Account.FormulaTypes.HARD_VALUE_INPUT || l_formulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
              m_editedFacts.Set(l_factCell, new DimensionKey(l_editedFact.EntityId, l_editedFact.AccountId, l_editedFact.EmployeeId, (Int32)l_editedFact.Period), l_editedFact);
            else
              m_outputFacts.Set(l_factCell, new DimensionKey(l_editedFact.EntityId, l_editedFact.AccountId, l_editedFact.EmployeeId, (Int32)l_editedFact.Period), l_editedFact);
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

    public void DownloadFacts(List<Int32> p_periodsList, bool p_updateCells)
    {
      // flush m_editedFacts ?
      // flush m_facts ?
      // flush m_outputsFacts ?

      m_periodsList = p_periodsList;
      m_updateCellsOnDownload = p_updateCells;
      List<AxisElem> l_entitiesList = m_dimensions.GetAxisElemList(DimensionType.ENTITY);
      Int32 l_startPeriod = p_periodsList.ElementAt(0);
      Int32 l_endPeriod = p_periodsList.ElementAt(p_periodsList.Count);

      FactsModel.Instance.ReadEvent += AfterFinancialInputDownloaded;
      m_inputsRequestIdList.Clear();
      foreach (EditedFinancialFact l_editedFact in m_editedFacts.Values)
      {
        foreach (AxisElem l_entity in l_entitiesList)
        {
          //
          // TO DO : create method on server returning all facts specific to an entity :
          //    - all accounts
          //    - filter on client, product, adjustment and employee
          //
          //m_inputsRequestIdList.Add(FactsModel.Instance.GetFactFinancial());
        }
      }
    }

    private void AfterFinancialInputDownloaded(ErrorMessage p_status, Int32 p_requestId, List<Fact> p_fact_list)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        //
        // Attention : pour l'instant la méthode n'est pas adaptée: nécessité de filtrer sur les
        // clients, produits, ajustement et employé pour financial
        //
        if (FillFactsDictionnaries(p_fact_list) != true)
        {
          FactsDownloaded(false);
          FactsModel.Instance.ReadEvent -= AfterFinancialInputDownloaded;
        }
        m_inputsRequestIdList.Remove(p_requestId);
        if (m_inputsRequestIdList.Count == 0)
        {
          ComputeOutputs();
        }
      }
      else
      {
        FactsDownloaded(false);
        FactsModel.Instance.ReadEvent -= AfterFinancialInputDownloaded;
      }
    }


    // TO DO : must update worksheet flag
    //
    private bool FillFactsDictionnaries(List<Fact> p_factsList)
    {
      foreach (Fact l_fact in p_factsList)
      {
        DimensionKey l_dimensionKey = new DimensionKey(l_fact.EntityId, l_fact.AccountId, (UInt32)AxisType.Employee, (Int32)l_fact.Period);
        EditedFinancialFact l_EditedFact = m_editedFacts[l_dimensionKey];
        if (l_EditedFact != null)
          l_EditedFact.UpdateFinancialFact(l_fact);
        else
          m_facts[l_dimensionKey] = l_fact;  // Fact not on worksheet, saved as fact
      // if must update worksheet
        // update cell Value2
      
      }
      return true;
    }

    public void ComputeOutputs()
    {
      FactsModel.Instance.ReadEvent -= AfterFinancialInputDownloaded;
      SourcedComputeModel.Instance.ComputeCompleteEvent += AfterFinancialOutputsComputed;
      SourcedComputeRequest l_sourcedComputeRequest = new SourcedComputeRequest();
      l_sourcedComputeRequest.VersionId = m_versionId;

      List<Fact> l_factsList = new List<Fact>();
      foreach (EditedFinancialFact l_editedFact in m_editedFacts.Values)
      {
        l_factsList.Add(l_editedFact);
      }
      l_sourcedComputeRequest.FactList = l_factsList;

      List<UInt32> l_entitiesList = new List<UInt32>(); 
      foreach (AxisElem l_entity in m_dimensions.Entities.m_values.Values)
      {
        l_entitiesList.Add(l_entity.Id);
      }
      l_sourcedComputeRequest.EntityList = l_entitiesList;
     
      /////
      // must fill accounts list ?
      /////

      SourcedComputeModel.Instance.Compute(l_sourcedComputeRequest);
    }

    public void UpdateWorkSheetOutputs()
    {
      // Create Source_compute data set edited facts (object ?)
      // send SOURCED_COMPUTE REQUEST
      
    }

    private void AfterFinancialOutputsComputed(ErrorMessage p_status, SourcedComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {

      }
      else
      {
        // Exit financial edition mode ?
        FactsDownloaded(false);
      }
    }

    public bool UpdateEditedValueAndTag(Range p_cell)
    {
      if (m_editedFacts.ContainsKey(p_cell))
      {

        return true;
      }
      if (m_outputFacts.ContainsKey(p_cell))
      {

        return true;
      }
      return false;
    }

    public void UpdateWorksheetInputs()
    {
      foreach (EditedFinancialFact l_editedFact in m_editedFacts.Values)
      {
       // l_editedFact.UpdateCellValue();
      }
    }

    public void Commit()
    {
      // TO DO
      // Loop through facts : if to be commited  add to update list
      // Send EditedFacts to the model  

      // associate updates to cells
      // ready to listen server answer
      //   -> update cells color on worksheet if success
    }

    // TO DO : After commit event


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
