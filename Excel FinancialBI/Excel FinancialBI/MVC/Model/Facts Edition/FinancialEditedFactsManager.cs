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
    public event OnFactsDownloaded FactsDownloaded;
    List<EditedFact> m_factsToBeCommitted = new List<EditedFact>(); // ? to be confirmed
    Worksheet m_worksheet;

    public void RegisterEditedFacts(Dimensions p_dimensions)
    {
      switch (p_dimensions.m_orientation)
      {
        case Dimensions.Orientation.ACCOUNTS_PERIODS :
          CreateRHEditedFacts(p_dimensions.m_accounts, p_dimensions.m_periods, p_dimensions.m_entities);
          break;

        case Dimensions.Orientation.PERIODS_ACCOUNTS :
          CreateRHEditedFacts(p_dimensions.m_periods, p_dimensions.m_accounts, p_dimensions.m_entities);
          break;

        case Dimensions.Orientation.ENTITIES_ACCOUNTS :
          CreateRHEditedFacts(p_dimensions.m_entities, p_dimensions.m_accounts, p_dimensions.m_periods);
          break;

        case Dimensions.Orientation.ACCOUNTS_ENTITIES :
          CreateRHEditedFacts(p_dimensions.m_accounts, p_dimensions.m_entities, p_dimensions.m_periods);
          break;

        case Dimensions.Orientation.PERIODS_ENTITIES :
          CreateRHEditedFacts(p_dimensions.m_periods, p_dimensions.m_entities, p_dimensions.m_accounts);
          break;

        case Dimensions.Orientation.ENTITIES_PERIODS :
          CreateRHEditedFacts(p_dimensions.m_entities, p_dimensions.m_periods, p_dimensions.m_accounts);
          break;
      }

      TO DO Once facts registered clean dimensions and put them into a safe dictionary ?
    }

    private void CreateRHEditedFacts(Dimension<CRUDEntity> p_rowsDimension, Dimension<CRUDEntity> p_columnsDimension, Dimension<CRUDEntity> p_fixedDimension)
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

   
    public void DownloadFacts(Version p_version, List<UInt32> p_periodsList)
    {

    // TO DO 


    }

    private void AfterFinancialInputDownloaded(bool p_success)
    {
      // TO DO

      // TO DO : request SOURCED COMPUTE
    }

    private void AfterFinancialOutputsComputed(bool p_success)
    {
      // TO DO : if success save values into facts

      FactsDownloaded(true);
    }

    private bool FillEditedFacts(List<Fact> p_factsList)
    {
    
      return true;
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

    public void UpdateWorkSheetOutputs()
    {
      foreach (OutputFact l_outputFact in m_editedFacts.Values)
      {
        l_outputFact.UpdateCellValue();
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

  }
}
