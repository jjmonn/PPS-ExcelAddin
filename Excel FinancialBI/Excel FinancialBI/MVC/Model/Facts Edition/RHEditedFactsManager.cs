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
    MultiIndexDictionary<Range, Tuple<AxisElem, Account, AxisElem, UInt32>, EditedFact> m_RHEditedFacts = new MultiIndexDictionary<Range, Tuple<AxisElem, Account, AxisElem, UInt32>, EditedFact>();
    Dimensions m_dimensions;
    List<int> m_requestIdList = new List<int>();
    public event OnFactsDownloaded FactsDownloaded;
    List<EditedFact> m_factsToBeCommitted = new List<EditedFact>(); // ? to be confirmed


    //public event OnRHFactsDownloaded RHFactsDownloaded;
    //public event OnOutputsComputed OutputsComputed;    

    public void RegisterEditedFacts()
    {

      // TO DO create and Fill the EditedFacts, RHEditedFacts and OutputsFacts
      // based on global orientation and dimensions



      // Once facts registered clean dimensions and put them into a safe dictionary ?
    }

    public void DownloadFacts(Version p_version, List<UInt32> p_periodsList)
    {
      AxisElem l_entity = m_dimensions.m_entities.m_values.Values.ElementAt(0);
      List<Account> l_accountsList = m_dimensions.m_accounts.m_values.Values.ToList();
      List<AxisElem> l_employeesList = m_dimensions.m_employees.m_values.Values.ToList();
      UInt32 l_startPeriod = p_periodsList.ElementAt(0);
      UInt32 l_endPeriod = p_periodsList.ElementAt(p_periodsList.Count);

      FactsModel.Instance.ReadEvent += AfterRHFactsDownloaded;
      m_requestIdList.Clear();
      foreach (Account l_account in l_accountsList)
      {
        foreach (AxisElem l_employee in l_employeesList)
        {
          m_requestIdList.Add(FactsModel.Instance.GetFact(l_account.Id, l_entity.Id, l_employee.Id, p_version.Id, l_startPeriod, l_endPeriod));
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
        if (l_account == null || l_entity == null || l_employee == null)
          return false;

        Tuple<AxisElem, Account, AxisElem, UInt32> l_factTuple = new Tuple<AxisElem, Account, AxisElem, UInt32>(l_entity, l_account, l_employee, l_fact.Period);
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
  }
}
