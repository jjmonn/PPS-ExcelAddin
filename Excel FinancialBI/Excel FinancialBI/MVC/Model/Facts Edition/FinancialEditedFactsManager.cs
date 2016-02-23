using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Model.CRUD;
  using Network;
  using Utils;

  class FinancialEditedFactsManager : IEditedFactsManager
  {
    MultiIndexDictionary<Range, Tuple<AxisElem, Account, PeriodDimension>, EditedFact> m_editedFacts = new MultiIndexDictionary<Range, Tuple<AxisElem, Account, PeriodDimension>, EditedFact>();
    MultiIndexDictionary<Range, Tuple<AxisElem, Account, PeriodDimension>, EditedFact> m_outputFacts = new MultiIndexDictionary<Range, Tuple<AxisElem, Account, PeriodDimension>, EditedFact>();
    Dimensions m_dimensions = null;
    List<int> m_inputsRequestIdList = new List<int>();
    public event OnFactsDownloaded FactsDownloaded;
    List<EditedFact> m_factsToBeCommitted = new List<EditedFact>(); // ? to be confirmed
    Worksheet m_worksheet;
    public bool m_autoCommit {set; get;}

    public void RegisterEditedFacts(Dimensions p_dimensions, Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;
      m_dimensions = p_dimensions;
      Dimension<CRUDEntity> l_vertical = p_dimensions.m_dimensions[p_dimensions.m_orientation.Vertical];
      Dimension<CRUDEntity> l_horitontal = p_dimensions.m_dimensions[p_dimensions.m_orientation.Horizontal];
      Dimension<CRUDEntity> l_tabDimension = p_dimensions.m_dimensions[p_dimensions.m_orientation.TabDimension];

      CreateEditedFacts(l_vertical, l_horitontal, l_tabDimension);

      // TO DO Once facts registered clean dimensions and put them into a safe dictionary ?
    }

    private void CreateEditedFacts(Dimension<CRUDEntity> p_rowsDimension, Dimension<CRUDEntity> p_columnsDimension, Dimension<CRUDEntity> p_fixedDimension)
    {
      foreach (KeyValuePair<Range, CRUDEntity> l_rowsKeyPair in p_rowsDimension.m_values)
      {
        foreach (KeyValuePair<Range, CRUDEntity> l_columnsKeyPair in p_columnsDimension.m_values)
        {
          Range l_factCell = m_worksheet.Cells[l_rowsKeyPair.Key.Row, l_columnsKeyPair.Key.Column] as Range;
          EditedFact l_editedFact = CreateEditedFact(p_rowsDimension, l_rowsKeyPair.Value, p_columnsDimension, l_columnsKeyPair.Value, p_fixedDimension, l_factCell);
          if (l_editedFact != null)
          {
            Tuple<AxisElem, Account, PeriodDimension> l_tuple = new Tuple<AxisElem, Account, PeriodDimension>(l_editedFact.m_entity, l_editedFact.m_account, l_editedFact.m_period);
            Account.FormulaTypes l_formulaType = l_editedFact.m_account.FormulaType;
            if (l_formulaType == Account.FormulaTypes.HARD_VALUE_INPUT || l_formulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT)
              m_editedFacts.Set(l_factCell, l_tuple, l_editedFact);
            else
              m_outputFacts.Set(l_factCell, l_tuple, l_editedFact);
          }
        }
      }
    }

    private EditedFact CreateEditedFact(Dimension<CRUDEntity> p_dimension1, CRUDEntity p_dimensionValue1,
                                        Dimension<CRUDEntity> p_dimension2, CRUDEntity p_dimensionValue2,
                                        Dimension<CRUDEntity> p_fixedDimension,
                                        Range p_cell)
    {
      Account l_account = null;
      AxisElem l_entity = null;
      PeriodDimension l_period = null;

      Dimensions.SetDimensionValue(p_dimension1, p_dimensionValue1, l_account, l_entity, null, l_period);
      Dimensions.SetDimensionValue(p_dimension2, p_dimensionValue2, l_account, l_entity, null, l_period);
      Dimensions.SetDimensionValue(p_fixedDimension, p_fixedDimension.UniqueValue, l_account, l_entity, null, l_period);

      if (l_account == null || l_entity == null || l_period == null)
        return null;
      return new EditedFact(l_account, l_entity, null, l_period, p_cell, Account.AccountProcess.FINANCIAL);
    }


    public void DownloadFacts(Version p_version, List<Int32> p_periodsList)
    {
      List<AxisElem> l_entitiesList = m_dimensions.GetAxisElemList(DimensionType.ENTITY);
      Int32 l_startPeriod = p_periodsList.ElementAt(0);
      Int32 l_endPeriod = p_periodsList.ElementAt(p_periodsList.Count);

      FactsModel.Instance.ReadEvent += AfterFinancialInputDownloaded;
      m_inputsRequestIdList.Clear();
      foreach (EditedFact l_editedFact in m_editedFacts.Values)
      {
        foreach (AxisElem l_entity in l_entitiesList)
        {
          //
          // TO DO : create method on server returning all facts specific to an entity :
          //    - all accounts
          //    - filter on client, product, adjustment and employee
          //
          m_inputsRequestIdList.Add(FactsModel.Instance.GetFact(l_editedFact.m_account.Id, l_entity.Id, (UInt32)AxisType.Employee, p_version.Id, (UInt32)l_startPeriod, (UInt32)l_endPeriod));
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
        if (FillEditedFacts(p_fact_list) != true)
        {
          FactsDownloaded(false);
          FactsModel.Instance.ReadEvent -= AfterFinancialInputDownloaded;
        }
        m_inputsRequestIdList.Remove(p_requestId);
        if (m_inputsRequestIdList.Count == 0)
        {
          //
          // TO DO : raise facts downloaded event or Outputs computation (SOURCED_COMPUTE)
          //
          FactsModel.Instance.ReadEvent -= AfterFinancialInputDownloaded;
        }
      }
      else
      {
        FactsDownloaded(false);
        FactsModel.Instance.ReadEvent -= AfterFinancialInputDownloaded;
      }
    }

    private bool FillEditedFacts(List<Fact> p_factsList)
    {
      foreach (Fact l_fact in p_factsList)
      {
        Account l_account = AccountModel.Instance.GetValue(l_fact.AccountId);
        AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, l_fact.EntityId);
        PeriodDimension l_period = new PeriodDimension(l_fact.Period);

        if (l_account == null || l_entity == null || l_period == null)
          return false;

        Tuple<AxisElem, Account, PeriodDimension> l_factTuple = new Tuple<AxisElem, Account, PeriodDimension>(l_entity, l_account, l_period);
        EditedFact l_EditedFact = m_editedFacts[l_factTuple];
        if (l_EditedFact != null)
          l_EditedFact.UpdateFactValue(l_fact.Value);
      }
      return true;
    }

    public void UpdateWorkSheetOutputs()
    {
      // Create Source_compute data set edited facts (object ?)
      // send SOURCED_COMPUTE REQUEST
      
    }

    private void AfterFinancialOutputsComputed(bool p_success)
    {
      //
      // TO DO : if success save values into facts
      //
      foreach (EditedFact l_outputFact in m_editedFacts.Values)
      {
        l_outputFact.UpdateCellValue();
      }
      FactsDownloaded(true);
    }

    public void IdentifyDifferences()
    {
      foreach (EditedFact l_editedFact in m_editedFacts.Values)
      {
        l_editedFact.IsDifferent();
      }
    }

    public void UpdateWorksheetInputs()
    {
      foreach (EditedFact l_editedFact in m_editedFacts.Values)
      {
        l_editedFact.UpdateCellValue();
      }
    }

    public void CommitDifferences()
    {
      // TO DO
      // Loop through facts : if to be commited  add to update list
      // Send EditedFacts to the model  

      // associate updates to cells
      // ready to listen server answer
      //   -> update cells color on worksheet if success
    }

    // TO DO : After commit event

  }
}
