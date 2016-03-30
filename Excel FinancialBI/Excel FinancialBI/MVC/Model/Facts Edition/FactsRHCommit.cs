using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;


namespace FBI.MVC.Model
{
  using Network;
  using Utils;
  using CRUD;
  using FBI.MVC.Model;
  using FBI.MVC.Controller;
  using FBI.MVC.View;
  
  public delegate void FactsCommitError(string p_address, ErrorMessage p_error);


  class FactsRHCommit
  {
    MultiIndexDictionary<string, DimensionKey, EditedRHFact> m_RHEditedFacts;
    SafeDictionary<DimensionKey, Fact> m_previousWeeksFacts;
    SafeDictionary<UInt32, EditedRHFact> m_IdEditedFactDict;
    List<Int32> m_periodsList;
    RangeHighlighter m_rangeHighlighter;
    SafeDictionary<CRUDAction, SafeDictionary<string, Fact>> m_factsCommitDict = new SafeDictionary<CRUDAction,SafeDictionary<string,Fact>>();
    SafeDictionary<CRUDAction, List<FactTag>> m_factTagCommitDict = new SafeDictionary<CRUDAction, List<FactTag>>();
    SafeDictionary<CRUDAction, List<LegalHoliday>> m_LegalHolidayCommitDict = new SafeDictionary<CRUDAction, List<LegalHoliday>>();
    // SafeDictionary<UInt32, Fact> m_deleteFactsDict = new SafeDictionary<uint,Fact>();
    SafeDictionary<UInt32, EditedRHFact> m_legalHolidayDeleteDictIdEditedFact = new SafeDictionary<uint,EditedRHFact>();
    public event FactsCommitError OnCommitError;
    List<int> m_requestIdList = new List<int>();
    UInt32 m_RHAccountId;
    UInt32 m_versionId;
    UInt32 m_nbAwaitingAnswer = 0;

    public FactsRHCommit(MultiIndexDictionary<string, DimensionKey, EditedRHFact> p_RHEditedFacts,
                         SafeDictionary<DimensionKey, Fact> p_previousWeeksFacts,
                         SafeDictionary<UInt32, EditedRHFact> p_IdEditedFactDict,
                         List<Int32> p_periodsList,
                         RangeHighlighter p_rangeHighlighter,
                         UInt32 p_RHAccountId,
                         UInt32 p_versionId)
    {
      m_RHEditedFacts = p_RHEditedFacts;
      m_previousWeeksFacts = p_previousWeeksFacts;
      m_IdEditedFactDict = p_IdEditedFactDict;
      m_periodsList = p_periodsList;
      m_rangeHighlighter = p_rangeHighlighter;
      m_RHAccountId = p_RHAccountId;
      m_versionId = p_versionId;

      FactsModel.Instance.UpdateEvent += OnFactsUpdateList;
      FactTagModel.Instance.UpdateListEvent += OnFactTagsUpdate;
      LegalHolidayModel.Instance.UpdateListEvent += OnLegalHolidayUpdate;
    }

    public void UnSuscribeEvents()
    {
      FactsModel.Instance.UpdateEvent -= OnFactsUpdateList;
      FactTagModel.Instance.UpdateListEvent -= OnFactTagsUpdate;
      LegalHolidayModel.Instance.UpdateListEvent -= OnLegalHolidayUpdate;

      m_factTagCommitDict.Clear();
      m_LegalHolidayCommitDict.Clear();
      m_legalHolidayDeleteDictIdEditedFact.Clear();
    }

    public void Commit()
    {
      m_factsCommitDict[CRUDAction.UPDATE] = new SafeDictionary<string,Fact>();
      m_factsCommitDict[CRUDAction.DELETE] = new SafeDictionary<string, Fact>();
      
      m_factTagCommitDict[CRUDAction.CREATE] = new List<FactTag>();
      m_factTagCommitDict[CRUDAction.UPDATE] = new List<FactTag>();
      m_factTagCommitDict[CRUDAction.DELETE] = new List<FactTag>();
      
      m_LegalHolidayCommitDict[CRUDAction.CREATE] = new List<LegalHoliday>();
      m_LegalHolidayCommitDict[CRUDAction.DELETE] = new List<LegalHoliday>();
      m_legalHolidayDeleteDictIdEditedFact = new SafeDictionary<UInt32, EditedRHFact>();
      
      foreach (EditedRHFact l_RHEditedFact in m_RHEditedFacts.Values)
      {
        if (l_RHEditedFact.EditedLegalHoliday.Tag != l_RHEditedFact.ModelLegalHoliday.Tag)
          LegalHolidayCommitListFilling(l_RHEditedFact);

        if (l_RHEditedFact.EditedFactTag.Tag != FactTag.TagType.NONE) // else if ????
          ClientAllocationFromTagType(l_RHEditedFact);
        else
          RegularClientAllocation(l_RHEditedFact);

        if (l_RHEditedFact.EditedFactTag.Tag != l_RHEditedFact.ModelFactTag.Tag)
          FactTagCommitListFilling(l_RHEditedFact);
      }

      // AddinModuleController.SetExcelInteractionState(false);
      // TO DO : circular progress : self threaded UI with 
      FactsCommit();
      FactTagCommit();

      LegalHolidaysCommit();
    }

    #region Facts Commit

    private void ClientAllocationFromTagType(EditedRHFact p_editedFact)
    {
      switch (p_editedFact.EditedFactTag.Tag)
      {
        case FactTag.TagType.CP:
          ClientAllocation(GetLastAllocatedClient(p_editedFact), p_editedFact);
          break;

        case FactTag.TagType.RTT:
          ClientAllocation(GetLastAllocatedClient(p_editedFact), p_editedFact);
          break;

        case FactTag.TagType.Abs:
          ClientAllocation(GetLastAllocatedClient(p_editedFact), p_editedFact);
          break;

        case FactTag.TagType.E:
          ClientAllocation(GetNextAllocatedClient(p_editedFact), p_editedFact);
          break;

        case FactTag.TagType.S :
          ClientAllocation(GetLastAllocatedClient(p_editedFact), p_editedFact);
          break;

        default:
          if (p_editedFact.EditedFactTag.Tag != p_editedFact.ModelFactTag.Tag 
              || p_editedFact.Value != 0)
          {
            p_editedFact.ClientId = (UInt32)AxisType.Client;
            p_editedFact.Value = 0;
            m_factsCommitDict[CRUDAction.UPDATE][p_editedFact.Cell.Address] = p_editedFact;
          }
          break;
      }
    }

    private void ClientAllocation(UInt32 p_clientId, EditedRHFact p_editedFact)
    {
      if (p_clientId != p_editedFact.ClientId || p_editedFact.EditedValue != p_editedFact.Value)
      {
        p_editedFact.ClientId = p_clientId;
        p_editedFact.Value = p_editedFact.EditedValue;
        m_factsCommitDict[CRUDAction.UPDATE][p_editedFact.Cell.Address] = p_editedFact;
      }
    }

    private void RegularClientAllocation(EditedRHFact p_editedFact)
    {
      if (p_editedFact.EditedClientId != p_editedFact.ClientId)
      {
        p_editedFact.ClientId = (UInt32)p_editedFact.EditedClientId;
        p_editedFact.Value = 1;

        if (p_editedFact.EditedClientId == 0)
          m_factsCommitDict[CRUDAction.DELETE][p_editedFact.Cell.Address] = p_editedFact;
        else
          m_factsCommitDict[CRUDAction.UPDATE][p_editedFact.Cell.Address] = p_editedFact;
      }
    }

    private void FactsCommit()
    {
      foreach (KeyValuePair<CRUDAction, SafeDictionary<string, Fact>> l_CRUDActionList in m_factsCommitDict)
      {
        if (l_CRUDActionList.Value.Count > 0)
        {
          FactsModel.Instance.UpdateList(l_CRUDActionList.Value, l_CRUDActionList.Key);
          m_nbAwaitingAnswer++;
        }
        l_CRUDActionList.Value.Clear();
      }
    }

    private void OnFactsUpdateList(ErrorMessage p_status, CRUDAction p_CRUDAction, SafeDictionary<string, Tuple<UInt32, ErrorMessage>> p_resultsDict)
    {
      m_nbAwaitingAnswer--;
      if (p_status == ErrorMessage.SUCCESS)
      {
        lock (m_IdEditedFactDict)
        {
          AddinModuleController.SetExcelInteractionState(false);
          lock (m_RHEditedFacts)
          {
            foreach (KeyValuePair<string, Tuple<UInt32, ErrorMessage>> l_addressMessagePair in p_resultsDict)
            {
              if (p_CRUDAction == CRUDAction.UPDATE)
                OnFactUpdate(l_addressMessagePair.Key, l_addressMessagePair.Value.Item1, l_addressMessagePair.Value.Item2);
              else
                OnFactDelete(l_addressMessagePair.Key, l_addressMessagePair.Value.Item1, l_addressMessagePair.Value.Item2);
            }
            if (m_factTagCommitDict != null)
              FactTagCommit();
            else
              System.Diagnostics.Debug.WriteLine("On facts commit : m_factsTagCommitList or m_fatcTagsCreate list == null");
            AddinModuleController.SetExcelInteractionState(m_nbAwaitingAnswer == 0);
          }
        }
      }
      else
      {
        // Create after commit event intercepted by the view -> only if commit fails
        // set commit status in the ribbon as well
      }
      AddinModuleController.SetExcelInteractionState(true);
    }

    private void OnFactUpdate(string p_cellAddress, UInt32 p_factId, ErrorMessage p_errorMessage)
    {
      EditedRHFact l_editedFact = m_RHEditedFacts[p_cellAddress];
      if (l_editedFact != null && p_errorMessage == ErrorMessage.SUCCESS)
      {
        l_editedFact.SetId(p_factId);
        m_IdEditedFactDict[p_factId] = l_editedFact;
        m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
      }
      else
      {
        // Put back model value for ClientId, quid : information not available anymore ..
        // OnCommitError(l_addressMessagePair.Key, l_addressMessagePair.Value.Item2);
      }
    }

    private void OnFactDelete(string p_cellAddress, UInt32 p_factId, ErrorMessage p_errorMessage)
    {
      EditedRHFact l_editedFact = m_RHEditedFacts[p_cellAddress];
      if (l_editedFact != null && p_errorMessage == ErrorMessage.SUCCESS)
      {
        l_editedFact.ClientId = 0;
        m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
        if (m_IdEditedFactDict.ContainsKey(p_factId) == true)
          m_IdEditedFactDict.Remove(p_factId);
      }
      else
      {
        // put back edited client to fact Client Id value
        // Log commit error
      }
    }

    #endregion

    #region Facts Tags

    private void FactTagCommitListFilling(EditedRHFact p_editedFact)
    {
      p_editedFact.ModelFactTag.Tag = p_editedFact.EditedFactTag.Tag;
      FactTag l_factTag = FactTagModel.Instance.GetValue(p_editedFact.Id);

      if (l_factTag != null)
      {
        if (p_editedFact.EditedFactTag.Tag == FactTag.TagType.NONE)
        {
          p_editedFact.ClientId = (UInt32)p_editedFact.EditedClientId;
          m_factTagCommitDict[CRUDAction.DELETE].Add(p_editedFact.EditedFactTag);
        }
        else
          m_factTagCommitDict[CRUDAction.UPDATE].Add(p_editedFact.EditedFactTag);
      }
      else
        m_factTagCommitDict[CRUDAction.CREATE].Add(p_editedFact.EditedFactTag);
    }

    private void FactTagCommit()
    {
      lock (m_factTagCommitDict)
      {
        foreach (KeyValuePair<CRUDAction, List<FactTag>> l_CRUDActionList in m_factTagCommitDict)
        {
          if (l_CRUDActionList.Value.Count > 0)
          {
            m_nbAwaitingAnswer++;
            FactTagModel.Instance.UpdateList(l_CRUDActionList.Value, l_CRUDActionList.Key);
          }
          l_CRUDActionList.Value.Clear();
        }
      }
    }

    private void OnFactTagsUpdate(ErrorMessage p_status, SafeDictionary<CRUDAction, SafeDictionary<UInt32, ErrorMessage>> p_updateResults)
    {
      m_nbAwaitingAnswer--;
      AddinModuleController.SetExcelInteractionState(false);
      if (p_status == ErrorMessage.SUCCESS)
      {
        lock (m_IdEditedFactDict)
        {
        foreach (KeyValuePair<CRUDAction, SafeDictionary<UInt32, ErrorMessage>> l_result in p_updateResults)
          {
            foreach (KeyValuePair<UInt32, ErrorMessage> l_idErrorMessage in l_result.Value)
            {
              FactTagUpdate(l_idErrorMessage.Value, l_idErrorMessage.Key);
            }
          }
        }
      }
      else
      {
        // Create after commit event intercepted by the view -> only if commit fails
        // set commit status in the ribbon as well
      }
      AddinModuleController.SetExcelInteractionState(m_nbAwaitingAnswer == 0);
    }

    private void FactTagUpdate(ErrorMessage p_status, UInt32 p_id)
    {
      EditedRHFact l_editedFact = m_IdEditedFactDict[p_id];
      if (l_editedFact != null)
      {
        if (p_status == ErrorMessage.SUCCESS)
          m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
        else
        {
          FactTag l_modelFactTag = FactTagModel.Instance.GetValue(l_editedFact.Id);
          if (l_modelFactTag != null)
            l_editedFact.ModelFactTag.Tag = l_modelFactTag.Tag;

          if (OnCommitError != null)
            OnCommitError(l_editedFact.Cell.Address, ErrorMessage.SYSTEM); // TO DO : facts tags should be commited like facts
        }
      }
    }

    #endregion

    #region Legal holiday

    private void LegalHolidayCommitListFilling(EditedRHFact p_editedFact)
    {
      LegalHoliday l_legalHoliday = LegalHolidayModel.Instance.GetValue(p_editedFact.EmployeeId, p_editedFact.Period);

      if (p_editedFact.EditedLegalHoliday.Tag == LegalHolidayTag.FER)
      {
        if (p_editedFact.ClientId != 0)
          m_factsCommitDict[CRUDAction.DELETE][p_editedFact.Cell.Address] = p_editedFact;

        if (l_legalHoliday == null)
          m_LegalHolidayCommitDict[CRUDAction.CREATE].Add(p_editedFact.EditedLegalHoliday);
      }
      else
      {
        if (l_legalHoliday != null && m_legalHolidayDeleteDictIdEditedFact.ContainsKey(l_legalHoliday.Id) == false)
        {
          m_legalHolidayDeleteDictIdEditedFact[l_legalHoliday.Id] = p_editedFact;
          m_LegalHolidayCommitDict[CRUDAction.DELETE].Add(p_editedFact.EditedLegalHoliday);
        }
      }
    }

    private void LegalHolidaysCommit()
    {
      foreach (KeyValuePair<CRUDAction, List<LegalHoliday>> l_CRUDActionList in m_LegalHolidayCommitDict)
      {
        if (l_CRUDActionList.Value.Count > 0)
        {
          m_nbAwaitingAnswer++;
          LegalHolidayModel.Instance.UpdateList(l_CRUDActionList.Value, l_CRUDActionList.Key);
          l_CRUDActionList.Value.Clear();
        }
      }
    }

    private void OnLegalHolidayUpdate(ErrorMessage p_status, SafeDictionary<CRUDAction, SafeDictionary<UInt32, ErrorMessage>> p_updateResults)
    {
      m_nbAwaitingAnswer--;
      AddinModuleController.SetExcelInteractionState(false);
      if (p_status == ErrorMessage.SUCCESS)
      {
        if (p_updateResults[CRUDAction.CREATE] != null)
          OnLegalHolidayCreate(p_updateResults[CRUDAction.CREATE]);
        
        if (p_updateResults[CRUDAction.DELETE] != null)
          OnLegalHolidayDelete(p_updateResults[CRUDAction.DELETE]);
      }
      else
      {
        // Log commit error in view
      }
      AddinModuleController.SetExcelInteractionState(m_nbAwaitingAnswer == 0);
    }

    private void OnLegalHolidayCreate(SafeDictionary<UInt32, ErrorMessage> p_createResults)
    {
      foreach (KeyValuePair<UInt32, ErrorMessage> l_result in p_createResults)
      {
        if (l_result.Value == ErrorMessage.SUCCESS)
        {
          LegalHoliday l_legalHoliday = LegalHolidayModel.Instance.GetValue(l_result.Key);
          if (l_legalHoliday == null)
            return;

          EditedRHFact l_editedFact = GetEditedFact(l_legalHoliday.EmployeeId, (Int32)l_legalHoliday.Period);
          if (l_editedFact == null)
            return;

          m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
          l_editedFact.ModelLegalHoliday.Id = l_legalHoliday.Id;
          l_editedFact.ModelLegalHoliday.Tag = LegalHolidayTag.FER;
          l_editedFact.EditedLegalHoliday.Id = l_legalHoliday.Id;
          l_editedFact.EditedLegalHoliday.Tag = LegalHolidayTag.FER;
        }
        else
        {
          // TO DO: Log error
        }
      }
    }

    private void OnLegalHolidayDelete(SafeDictionary<UInt32, ErrorMessage> p_deleteResults)
    {
      if (m_legalHolidayDeleteDictIdEditedFact == null)
        return;

      lock (m_legalHolidayDeleteDictIdEditedFact)
      {
        foreach (KeyValuePair<UInt32, ErrorMessage> l_result in p_deleteResults)
        {
          if (l_result.Value == ErrorMessage.SUCCESS)
          {
            EditedRHFact l_editedFact = m_legalHolidayDeleteDictIdEditedFact[l_result.Key];
            if (l_editedFact == null)
              return;

            m_rangeHighlighter.FillCellColor(l_editedFact.Cell, EditedFactStatus.Committed);
            l_editedFact.ModelLegalHoliday.Tag = LegalHolidayTag.NONE;
          }
          else
          {
            // TO DO: Log commit error to view
          }
          m_legalHolidayDeleteDictIdEditedFact.Remove(l_result.Key);
        }
      }
    }

    #endregion

    #region Utils

    //public UInt32 GetLastAllocatedClient(EditedRHFact p_editedFact)
    //{
    //  Int32 l_periodMinus3Weeks = PeriodModel.GetDayMinus3Weeks(m_periodsList.ElementAt(0));
    //  for (Int32 l_period = (Int32)p_editedFact.Period; l_period >= l_periodMinus3Weeks; l_period -= 1)
    //  {
    //    l_period = (Int32)DateTime.FromOADate((double)l_period).AddDays(-1).ToOADate();  
    //    DimensionKey l_dimensionKey = new DimensionKey(p_editedFact.EntityId, p_editedFact.AccountId, p_editedFact.EmployeeId, l_period);
    //    EditedRHFact l_RHEditedFact = m_RHEditedFacts[l_dimensionKey];
    //    if (l_RHEditedFact != null && l_RHEditedFact.ClientId != 0)
    //    {
    //      p_editedFact.EditedValue = l_RHEditedFact.Value;
    //      return l_RHEditedFact.ClientId;
    //    }
    //    else
    //    {
    //      Fact l_fact = m_previousWeeksFacts[l_dimensionKey];
    //      if (l_fact != null && l_fact.ClientId != 0)
    //      {
    //        p_editedFact.EditedValue = l_fact.Value;
    //        return l_fact.ClientId;
    //      }
    //    }
    //  }
    //  // TO DO :Commit status could be orange when last allocated client is default 
    //  // TO DO : To be confirmed
    //  p_editedFact.EditedValue = 0;
    //  return (UInt32)AxisType.Client;
    //}

    public UInt32 GetLastAllocatedClient(EditedRHFact p_editedFact)
    {
      // TO DO : a priori on ne remonte pas dans les 3 semaines : on doit regarder uniquement le jour avant/ quid week ends

      Int32 l_periodMinus3Weeks = PeriodModel.GetDayMinus3Weeks(m_periodsList.ElementAt(0));
      for (Int32 l_period = (Int32)p_editedFact.Period; l_period >= l_periodMinus3Weeks; l_period -= 1)
      {
        l_period = (Int32)DateTime.FromOADate((double)l_period).AddDays(-1).ToOADate();  
        DimensionKey l_dimensionKey = new DimensionKey(p_editedFact.EntityId, p_editedFact.AccountId, p_editedFact.EmployeeId, l_period);
        EditedRHFact l_RHEditedFact = m_RHEditedFacts[l_dimensionKey];
        if (l_RHEditedFact != null)
        {
          if (l_RHEditedFact.EditedClientId != 0)
          {
            p_editedFact.EditedValue = l_RHEditedFact.Value;
            return (UInt32)l_RHEditedFact.EditedClientId;
          }
          if (l_RHEditedFact.ClientId != 0)
          {
            p_editedFact.EditedValue = l_RHEditedFact.Value;
            return l_RHEditedFact.ClientId;
          }
          }
        else
        {
          Fact l_fact = m_previousWeeksFacts[l_dimensionKey];
          if (l_fact != null && l_fact.ClientId != 0)
          {
            p_editedFact.EditedValue = l_fact.Value;
            return l_fact.ClientId;
          }
          }
      }

      // TO DO :Commit status could be orange when last allocated client is default 
      p_editedFact.EditedValue = 0;
      return (UInt32)AxisType.Client;
    }

    public UInt32 GetNextAllocatedClient(EditedRHFact p_editedFact)
    {
      DateTime l_date = DateTime.FromOADate((Int32)p_editedFact.Period);
      List<DayOfWeek> l_weekEndDays = PeriodModel.GetWeekEndDays();

      l_date = l_date.AddDays(1);
      while (l_weekEndDays.Contains(l_date.DayOfWeek) == true)
      {
        l_date = l_date.AddDays(1);
      }
      Int32 l_period = (Int32)l_date.ToOADate();
      DimensionKey l_dimensionKey = new DimensionKey(p_editedFact.EntityId, p_editedFact.AccountId, p_editedFact.EmployeeId, l_period);
      EditedRHFact l_RHEditedFact = m_RHEditedFacts[l_dimensionKey];

      if (l_RHEditedFact != null && l_RHEditedFact.EditedClientId != 0)
      {
        p_editedFact.EditedValue = l_RHEditedFact.Value;
        return (UInt32)l_RHEditedFact.EditedClientId;
      }
      // TO DO : Commit status could be orange when last allocated client is default 
      // TO DO  : set value to 0 ?
      return (UInt32)AxisType.Client;
    }

    private void SetSingleCellFillColor(Range p_cell, EditedFactStatus p_status)
    {
      AddinModuleController.SetExcelInteractionState(false);
      m_rangeHighlighter.FillCellColor(p_cell, p_status);
      AddinModuleController.SetExcelInteractionState(true);
    }

    private EditedRHFact GetEditedFact(UInt32 p_employeeId, Int32 p_period)
    {
      foreach (EditedRHFact l_editedFact in m_RHEditedFacts.Values)
      {
        if (l_editedFact.EmployeeId == p_employeeId && l_editedFact.Period == p_period)
          return l_editedFact;
      }
      return null;
    }

    #endregion

  }
}
