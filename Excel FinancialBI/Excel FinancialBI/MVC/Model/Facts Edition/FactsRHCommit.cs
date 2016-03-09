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


  class FactsRHCommit
  {
    MultiIndexDictionary<string, DimensionKey, EditedRHFact> m_RHEditedFacts;
    SafeDictionary<DimensionKey, Fact> m_previousWeeksFacts;
    SafeDictionary<UInt32, EditedRHFact> m_IdEditedFactDict;
    List<Int32> m_periodsList;
    RangeHighlighter m_rangeHighlighter;
    List<EditedRHFact> m_factTagsCreateList;
    List<FactTag> m_factsTagCommitList;
    SafeDictionary<UInt32, EditedRHFact> m_legalHolidayDeleteDictIdEditedFact;
    public event FactsCommitError OnCommitError;
    List<int> m_requestIdList = new List<int>();
    UInt32 m_RHAccountId;
    UInt32 m_versionId;

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

      FactsModel.Instance.UpdateEvent += AfterFactsCommit;
      FactsModel.Instance.DeleteEvent += AfterFactDelete;
      FactTagModel.Instance.CreationEvent += AfterFactTagCreate;
      FactTagModel.Instance.UpdateListEvent += AfterFactTagsCommit;
      FactTagModel.Instance.DeleteEvent += AfterFactTagDelete;
      LegalHolidayModel.Instance.ReadEvent += AfterLegalHolidayRead;
      LegalHolidayModel.Instance.DeleteEvent += AfterLegalHolidayDelete;
    }

    public void Dispose()
    {
      FactsModel.Instance.UpdateEvent -= AfterFactsCommit;
      FactsModel.Instance.DeleteEvent -= AfterFactDelete;
      FactTagModel.Instance.CreationEvent -= AfterFactTagCreate;
      FactTagModel.Instance.UpdateListEvent -= AfterFactTagsCommit;
      FactTagModel.Instance.DeleteEvent -= AfterFactTagDelete;
      LegalHolidayModel.Instance.ReadEvent -= AfterLegalHolidayRead;
      LegalHolidayModel.Instance.DeleteEvent -= AfterLegalHolidayDelete;
    }

    public void Commit()
    {
      SafeDictionary<string, Fact> l_factsCommitDict = new SafeDictionary<string, Fact>();
      m_factsTagCommitList = new List<FactTag>();
      m_factTagsCreateList = new List<EditedRHFact>();
      m_legalHolidayDeleteDictIdEditedFact = new SafeDictionary<UInt32, EditedRHFact>();

      foreach (EditedRHFact l_RHEditedFact in m_RHEditedFacts.Values)
      {
        if (l_RHEditedFact.EditedLegalHoliday.Tag != l_RHEditedFact.ModelLegalHoliday.Tag)
          LegalHolidayCommit(l_RHEditedFact);

        if (l_RHEditedFact.EditedFactTag.Tag != FactTag.TagType.NONE)
          ClientCommitFromTagType(l_RHEditedFact, l_factsCommitDict);
        else
          RegularClientCommit(l_RHEditedFact, l_factsCommitDict);

        if (l_RHEditedFact.EditedFactTag.Tag != l_RHEditedFact.ModelFactTag.Tag)
          FactTagCommitListFilling(l_RHEditedFact);
      }

      // AddinModuleController.SetExcelInteractionState(false);
      // TO DO : circular progress : self threaded UI with 

      if (l_factsCommitDict.Count > 0)
        FactsModel.Instance.UpdateList(l_factsCommitDict);
      else
        FactTagCommitAndCreate();
     
      if (m_legalHolidayDeleteDictIdEditedFact.Count > 0)
        DeleteLegalHolidays();
    }

    #region Facts Commit

    private void ClientCommitFromTagType(EditedRHFact p_editedFact, SafeDictionary<string, Fact> p_factsCommitDict)
    {
      switch (p_editedFact.EditedFactTag.Tag)
      {
        case FactTag.TagType.CP:
          ClientCommitFromLastAllocatedClient(p_editedFact, p_factsCommitDict);
          break;

        case FactTag.TagType.RTT:
          ClientCommitFromLastAllocatedClient(p_editedFact, p_factsCommitDict);
          break;

        case FactTag.TagType.Abs:
          ClientCommitFromLastAllocatedClient(p_editedFact, p_factsCommitDict);
          break;

        default:
          p_editedFact.ClientId = (UInt32)AxisType.Client;
          p_editedFact.Value = 0;
          p_factsCommitDict[p_editedFact.Cell.Address] = p_editedFact;
          break;
      }
    }

    private void ClientCommitFromLastAllocatedClient(EditedRHFact p_editedFact, SafeDictionary<string, Fact> p_factsCommitDict)
    {
      UInt32 l_lastAllocatedClient = GetLastAllocatedClient(p_editedFact);
      if (l_lastAllocatedClient != p_editedFact.ClientId)
      {
        p_editedFact.ClientId = l_lastAllocatedClient;
        p_factsCommitDict[p_editedFact.Cell.Address] = p_editedFact;
      }
    }

    private void RegularClientCommit(EditedRHFact p_editedFact, SafeDictionary<string, Fact> p_factsCommitDict)
    {
      if (p_editedFact.EditedClientId != p_editedFact.ClientId)
      {
        p_editedFact.ClientId = (UInt32)p_editedFact.EditedClientId;
        p_editedFact.Value = 1;

        if (p_editedFact.EditedClientId == 0)
          FactsModel.Instance.Delete(p_editedFact);
        else
          p_factsCommitDict[p_editedFact.Cell.Address] = p_editedFact;
      }
    }

    private void AfterFactsCommit(ErrorMessage p_status, SafeDictionary<string, Tuple<UInt32, ErrorMessage>> p_resultsDict)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        AddinModuleController.SetExcelInteractionState(false);
        lock (m_RHEditedFacts)
        {
          foreach (KeyValuePair<string, Tuple<UInt32, ErrorMessage>> l_addressMessagePair in p_resultsDict)
          {
            UInt32 l_factId = l_addressMessagePair.Value.Item1;
            EditedRHFact l_editedFact = m_RHEditedFacts[l_addressMessagePair.Key];
            if (l_editedFact != null && l_addressMessagePair.Value.Item2 == ErrorMessage.SUCCESS)
            {
              l_editedFact.SetId(l_factId);
              m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
            }
            else
            {
              // put back model value for ClientId, quid : information not available anymore ..
            //  OnCommitError(l_addressMessagePair.Key, l_addressMessagePair.Value.Item2);
            }
          }
          FactTagCommitAndCreate();
          AddinModuleController.SetExcelInteractionState(true);
        }
      }
      else
      {
        // Create after commit event intercepted by the view -> only if commit fails
        // set commit status in the ribbon as well
      }
      AddinModuleController.SetExcelInteractionState(true);
    }

    private void AfterFactDelete(ErrorMessage p_status, UInt32 p_factId)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        lock (m_IdEditedFactDict)
        {
          EditedRHFact l_editedFact = m_IdEditedFactDict[p_factId];
          if (l_editedFact != null)
            m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
        }
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
          FactsModel.Instance.Delete(p_editedFact);
          FactTagModel.Instance.Delete(l_factTag.Id);
        }
        else
          m_factsTagCommitList.Add(p_editedFact.EditedFactTag);
      }
      else
      {
        m_factTagsCreateList.Add(p_editedFact);
      }
    }

    private void FactTagCommitAndCreate()
    {
      if (m_factsTagCommitList.Count > 0)
        FactTagModel.Instance.UpdateList(m_factsTagCommitList);

      if (m_factTagsCreateList.Count > 0)
        CreateFactTags();
    }

    private void CreateFactTags()
    {
      if (IsFactTagCreateListValid() == false)
      {
        System.Diagnostics.Debug.WriteLine("Could not create fact tags : corresponding facts not created/ updated on client.");
        return;
      }

      foreach (EditedRHFact l_editedFact in m_factTagsCreateList)
      {
        if (l_editedFact.EditedFactTag.Id != 0)
          FactTagModel.Instance.Create(l_editedFact.EditedFactTag);
        else
          SetSingleCellFillColor(l_editedFact.Cell, EditedFactStatus.InputDifferent);
      }
      m_factTagsCreateList.Clear();
    }

    private void AfterFactTagCreate(ErrorMessage status, UInt32 id)
    {
      // TO DO : update list
      // Excel interaction false nécessaire
      //  AddinModuleController.SetExcelInteractionState(false);
      EditedRHFact l_editedFact = m_IdEditedFactDict[id];
      if (l_editedFact != null)
      {
        if (status == ErrorMessage.SUCCESS)
          m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
        else
        {
          l_editedFact.ModelFactTag.Tag = FactTag.TagType.NONE;
          // log commit error to view
        }
      }
      //    AddinModuleController.SetExcelInteractionState(true);
    }

    // use single Fact Tag Update ?

    private void AfterFactTagsCommit(ErrorMessage p_status, Dictionary<UInt32, bool> p_updateResults)
    {
      AddinModuleController.SetExcelInteractionState(false);
      if (p_status == ErrorMessage.SUCCESS)
      {
        lock (m_IdEditedFactDict)
        {
          foreach (KeyValuePair<UInt32, bool> l_result in p_updateResults)
          {
            EditedRHFact l_editedFact = m_IdEditedFactDict[l_result.Key];
            if (l_editedFact != null)
            {
              if (l_result.Value == true)
                m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
              else
              {
                FactTag l_modelFactTag = FactTagModel.Instance.GetValue(l_editedFact.Id);
                if (l_modelFactTag != null)
                  l_editedFact.ModelFactTag.Tag = l_modelFactTag.Tag;
                OnCommitError(l_editedFact.Cell.Address, ErrorMessage.SYSTEM); // TO DO : facts tags should be commited like facts
              }
            }
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

    private void AfterFactTagDelete(ErrorMessage status, UInt32 id)
    {
      lock (m_IdEditedFactDict)
      {
        EditedRHFact l_editedFact = m_IdEditedFactDict[id];
        if (l_editedFact != null)
        {
          if (status == ErrorMessage.SUCCESS)
            SetSingleCellFillColor(l_editedFact.Cell, EditedFactStatus.InputEqual);
          else
          {
            FactTag l_factTag = FactTagModel.Instance.GetValue(id);
            if (l_factTag != null)
            {
              l_editedFact.ModelFactTag.Tag = l_factTag.Tag;
              SetSingleCellFillColor(l_editedFact.Cell, EditedFactStatus.InputDifferent);
            }
          }
        }
      }
    }

    private bool IsFactTagCreateListValid()
    {
      foreach (EditedRHFact l_editedFact in m_factTagsCreateList)
      {
        if (l_editedFact.EditedFactTag.Id == 0)
          return false;
      }
      return true;
    }

    #endregion

    #region Legal holiday

    private void LegalHolidayCommit(EditedRHFact p_editedFact)
    {
      if (p_editedFact.EditedLegalHoliday.Tag == LegalHolidayTag.FER)
      {
        if (p_editedFact.ClientId != 0)
          FactsModel.Instance.Delete(p_editedFact);

        CreateOrInsertLegalHoliday(p_editedFact);
      }
      else
        AddToLegalHolidayDeleteDictIfExists(p_editedFact);
    }

    private void CreateOrInsertLegalHoliday(EditedRHFact p_editedFact)
    {
      LegalHoliday l_legalHoliday = LegalHolidayModel.Instance.GetValue(p_editedFact.EmployeeId, p_editedFact.Period);
      if (l_legalHoliday == null)
      {
        LegalHolidayModel.Instance.Create(p_editedFact.EditedLegalHoliday);
      }
    }

    private void AddToLegalHolidayDeleteDictIfExists(EditedRHFact p_editedFact)
    {
      LegalHoliday l_legalHoliday = LegalHolidayModel.Instance.GetValue(p_editedFact.EmployeeId, p_editedFact.Period);
      if (l_legalHoliday != null)
      {
        if (m_legalHolidayDeleteDictIdEditedFact.ContainsKey(l_legalHoliday.Id) == false)
          m_legalHolidayDeleteDictIdEditedFact[l_legalHoliday.Id] = p_editedFact;
      }
    }

    private void DeleteLegalHolidays()
    {
      foreach (UInt32 l_legalHolidayId in m_legalHolidayDeleteDictIdEditedFact.Keys)
      {
        LegalHolidayModel.Instance.Delete(l_legalHolidayId);
      }
    }

    private void AfterLegalHolidayRead(ErrorMessage status, LegalHoliday p_legalHoliday)
    {

      if (status == ErrorMessage.SUCCESS)
      {
        LegalHoliday l_legalHoliday = LegalHolidayModel.Instance.GetValue(p_legalHoliday.Id);
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
        // Log commit error in view
      }

    }

    private void AfterLegalHolidayDelete(ErrorMessage status, UInt32 id)
    {
      if (status == ErrorMessage.SUCCESS)
      {
        EditedRHFact l_editedFact = m_legalHolidayDeleteDictIdEditedFact[id];
        if (l_editedFact == null)
          return;

        m_rangeHighlighter.FillCellColor(l_editedFact.Cell, EditedFactStatus.Committed);
        l_editedFact.ModelLegalHoliday.Tag = LegalHolidayTag.NONE;
      }
      else
      {
        // Log commit error to view
      }
      m_legalHolidayDeleteDictIdEditedFact.Remove(id);
    }

    #endregion

    #region Utils

    public UInt32 GetLastAllocatedClient(EditedRHFact p_editedFact)
    {
      for (Int32 l_period = (Int32)p_editedFact.Period; l_period > m_periodsList.ElementAt(0); l_period -= 1)
      {
        DimensionKey l_dimensionKey = new DimensionKey(p_editedFact.EntityId, p_editedFact.AccountId, p_editedFact.EmployeeId, l_period);
        EditedRHFact l_RHEditedFact = m_RHEditedFacts[l_dimensionKey];
        if (l_RHEditedFact != null && l_RHEditedFact.ClientId != 0)
          return l_RHEditedFact.ClientId;
        else
        {
          Fact l_fact = m_previousWeeksFacts[l_dimensionKey];
          if (l_fact != null && l_fact.ClientId != 0)
            return l_fact.ClientId;
        }
      }
      // Commit status could be orange when last allocated client is default 
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
