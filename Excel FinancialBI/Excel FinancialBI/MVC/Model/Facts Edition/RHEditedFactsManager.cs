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


  class RHEditedFactsManager : IEditedFactsManager
  {
    MultiIndexDictionary<Range, Tuple<AxisElem, Account, AxisElem, PeriodDimension>, EditedFact> m_RHEditedFacts = new MultiIndexDictionary<Range, Tuple<AxisElem, Account, AxisElem, PeriodDimension>, EditedFact>();
    Dimensions m_dimensions;
    List<int> m_requestIdList = new List<int>();
    public event OnFactsDownloaded FactsDownloaded;
    List<EditedFact> m_factsToBeCommitted = new List<EditedFact>(); // ? to be confirmed
    Worksheet m_worksheet;

    //public event OnRHFactsDownloaded RHFactsDownloaded;
    //public event OnOutputsComputed OutputsComputed;    


    public void RegisterEditedFacts(Dimensions p_dimensions, Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;
      switch (p_dimensions.m_orientation)
      {
        case Dimensions.Orientation.EMPLOYEES_PERIODS:
          CreateEditedFacts(p_dimensions.m_employees, p_dimensions.m_periods, p_dimensions.m_entities);
          break;

        case Dimensions.Orientation.PERIODS_EMPLOYEES:
          CreateEditedFacts(p_dimensions.m_periods, p_dimensions.m_employees, p_dimensions.m_entities);
          break;
      }
   
      // Once facts registered clean dimensions and put them into a safe dictionary ?
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
            Tuple<AxisElem, Account, AxisElem, PeriodDimension> l_tuple = new Tuple<AxisElem, Account, AxisElem, PeriodDimension>(l_editedFact.m_entity, l_editedFact.m_account,l_editedFact.m_employee, l_editedFact.m_period);
            m_RHEditedFacts.Set(l_factCell, l_tuple, l_editedFact);
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
      AxisElem l_employee = null;
      PeriodDimension l_period = null;

      Dimensions.SetDimensionValue(p_dimension1, p_dimensionValue1, l_account, l_entity, l_employee, l_period);
      Dimensions.SetDimensionValue(p_dimension2, p_dimensionValue2, l_account, l_entity, l_employee, l_period);
      Dimensions.SetDimensionValue(p_fixedDimension, p_fixedDimension.UniqueValue, l_account, l_entity, l_employee, l_period);

      if (l_account == null || l_entity == null || l_period == null)
        return null;
      return new EditedFact(l_account, l_entity, null, l_period, p_cell, Account.AccountProcess.RH);
    }


    public void DownloadFacts(Version p_version, List<Int32> p_periodsList)
    {
      // TO DO : Log and Check Download time
      AxisElem l_entity = m_dimensions.m_entities.UniqueValue as AxisElem;
      List<Account> l_accountsList = GetAccountsList(m_dimensions.m_accounts);
      List<AxisElem> l_employeesList = GetEmployeesList(m_dimensions.m_employees);
      Int32 l_startPeriod = p_periodsList.ElementAt(0);
      Int32 l_endPeriod = p_periodsList.ElementAt(p_periodsList.Count);

      FactsModel.Instance.ReadEvent += AfterRHFactsDownloaded;
      m_requestIdList.Clear();
      foreach (Account l_account in l_accountsList)
      {
        foreach (AxisElem l_employee in l_employeesList)
        {
            m_requestIdList.Add(FactsModel.Instance.GetFact(l_account.Id, l_entity.Id, l_employee.Id, p_version.Id, (UInt32)l_startPeriod, (UInt32)l_endPeriod));
        }
      }
    }

    private void AfterRHFactsDownloaded(ErrorMessage p_status, Int32 p_requestId, List<Fact> p_fact_list)
    {
      // TO DO : time up to manage the case where the server stops answering or the connection is lost : 
      //          -> In this case notify user and exit fact edition 

      if (p_status == ErrorMessage.SUCCESS)
      {
        if (FillEditedFacts(p_fact_list) != true)
        {
          FactsDownloaded(false);
          FactsModel.Instance.ReadEvent -= AfterRHFactsDownloaded;
        }
        m_requestIdList.Remove(p_requestId);
        if (m_requestIdList.Count == 0)
        {
          FactsDownloaded(true);
          FactsModel.Instance.ReadEvent -= AfterRHFactsDownloaded;
        }
      }
      else
      {
        FactsDownloaded(false);
        FactsModel.Instance.ReadEvent -= AfterRHFactsDownloaded;
      }
    }

    private bool FillEditedFacts(List<Fact> p_factsList)
    {
      foreach (Fact l_fact in p_factsList)
      {
        Account l_account = AccountModel.Instance.GetValue(l_fact.AccountId);
        AxisElem l_entity = AxisElemModel.Instance.GetValue(AxisType.Entities, l_fact.EntityId);
        AxisElem l_employee = AxisElemModel.Instance.GetValue(AxisType.Employee, l_fact.EmployeeId);
        PeriodDimension l_period = new PeriodDimension(l_fact.Period);

        if (l_account == null || l_entity == null || l_employee == null)
          return false;

        Tuple<AxisElem, Account, AxisElem, PeriodDimension> l_factTuple = new Tuple<AxisElem, Account, AxisElem, PeriodDimension>(l_entity, l_account, l_employee, l_period);
        EditedFact l_RHEditedFact = m_RHEditedFacts[l_factTuple];
        if (l_RHEditedFact != null)
          l_RHEditedFact.UpdateFactValue(l_fact.ClientId);
      }
      return true;
    }

    public void IdentifyDifferences()
    {
      foreach (EditedFact l_editedFact in m_RHEditedFacts.Values)
      {
        l_editedFact.IsDifferent();
      }

    }

    public void UpdateWorksheetInputs()
    {
      foreach (EditedFact l_editedFact in m_RHEditedFacts.Values)
      {
        l_editedFact.UpdateCellValue();
      }
    }

    public void CommitDifferences()
    {
      // TO DO
      // Loop through facts : if to be commited  add to update list
      // Send EditedFacts or RHEditedFacts to the model  

      // RH Process : Antiduplicate system

      // associate updates to cells
      // ready to listen server answer
      //   -> update cells color on worksheet if success
    }


    #region Utils

    private List<Account> GetAccountsList(Dimension<CRUDEntity> p_dimension)
    {
      List<Account> l_list = new List<Account>();
      foreach (CRUDEntity l_account in p_dimension.m_values.Values)
      {
        l_list.Add(l_account as Account);
      }
      return l_list;
    }

    private List<AxisElem> GetEmployeesList(Dimension<CRUDEntity> p_dimension)
    {
      List<AxisElem> l_list = new List<AxisElem>();
      foreach (CRUDEntity l_employee in p_dimension.m_values.Values)
      {
        l_list.Add(l_employee as AxisElem);
      }
      return l_list;
    }   
    
    #endregion

  }
}
