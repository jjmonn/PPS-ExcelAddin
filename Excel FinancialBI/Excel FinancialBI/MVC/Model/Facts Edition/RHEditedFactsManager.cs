using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Model
{
  using CRUD;
  using FBI.MVC.Model;
  using FBI.MVC.Controller;
  using Network;
  using Utils;
  using FBI.MVC.View;

  class RHEditedFactsManager : IEditedFactsManager
  {
    MultiIndexDictionary<string, DimensionKey, EditedRHFact> m_RHEditedFacts = new MultiIndexDictionary<string, DimensionKey, EditedRHFact>();
    Dimensions m_dimensions = null;
    RangeHighlighter m_rangeHighlighter;
    List<int> m_requestIdList = new List<int>();
    public event OnFactsDownloaded FactsDownloaded;
    public event FactsCommitError OnCommitError;
    Worksheet m_worksheet;
    public bool m_autoCommit { set; get; }
    private bool m_updateCellsOnDownload;
    UInt32 m_RHAccountId;
    UInt32 m_versionId;
    List<string> m_factsTagList;
    List<string> m_clientsToBeCreated = new List<string>();
    private List<Int32> m_periodsList;
    SafeDictionary<DimensionKey, Fact> m_previousWeeksFacts = new SafeDictionary<DimensionKey, Fact>();
    SafeDictionary<Int32, EditedRHFact> m_requestIdDeletedFacts = new SafeDictionary<int, EditedRHFact>();

    public RHEditedFactsManager(List<Int32> p_periodsList)
    {
      m_factsTagList = Enum.GetNames(typeof(FactTag.TagType)).ToList();
      m_periodsList = p_periodsList;

      FactsModel.Instance.UpdateEvent += AfterFactsCommit;
      FactsModel.Instance.DeleteEvent += AfterFactDelete;
      FactTagModel.Instance.UpdateListEvent += AfterFactTagsCommit;
    }

    public void Dispose()
    {
      FactsModel.Instance.UpdateEvent -= AfterFactsCommit;
      FactsModel.Instance.DeleteEvent -= AfterFactDelete;
      FactTagModel.Instance.UpdateListEvent -= AfterFactTagsCommit;
      // dispose edited facts
      m_RHEditedFacts.Clear();
      // EMPTY dictionnaries etc.
    }

    public void RegisterEditedFacts(Dimensions p_dimensions, Worksheet p_worksheet, UInt32 p_versionId, RangeHighlighter p_rangeHighlighter, UInt32 p_RHAccountId)
    {
      m_worksheet = p_worksheet;
      m_dimensions = p_dimensions;
      m_rangeHighlighter = p_rangeHighlighter;
      m_versionId = p_versionId;
      m_RHAccountId = p_RHAccountId;
      Dimension<CRUDEntity> l_vertical = p_dimensions.m_dimensions[p_dimensions.m_orientation.Vertical];
      Dimension<CRUDEntity> l_horitontal = p_dimensions.m_dimensions[p_dimensions.m_orientation.Horizontal];

      CreateEditedFacts(l_vertical, l_horitontal, p_dimensions.m_entities, p_rangeHighlighter);

      // TO DO : Once facts registered clean dimensions and put them into a safe dictionary ?
    }

    private void CreateEditedFacts(Dimension<CRUDEntity> p_rowsDimension, Dimension<CRUDEntity> p_columnsDimension, Dimension<CRUDEntity> p_fixedDimension, RangeHighlighter p_rangeHighlighter)
    {
      foreach (KeyValuePair<Range, CRUDEntity> l_rowsKeyPair in p_rowsDimension.m_values)
      {
        foreach (KeyValuePair<Range, CRUDEntity> l_columnsKeyPair in p_columnsDimension.m_values)
        {
          Range l_factCell = m_worksheet.Cells[l_rowsKeyPair.Key.Row, l_columnsKeyPair.Key.Column] as Range;
          EditedRHFact l_editedFact = CreateEditedFact(p_rowsDimension, l_rowsKeyPair.Value, p_columnsDimension, l_columnsKeyPair.Value, p_fixedDimension, l_factCell);
          if (l_editedFact != null)
          {
            InitializeEditedFact(l_editedFact, p_rangeHighlighter);
          }
        }
      }
    }

    private void InitializeEditedFact(EditedRHFact p_editedFact, RangeHighlighter p_rangeHighlighter)
    {
      p_rangeHighlighter.FillInputsBaseColor(p_editedFact.Cell);
      p_editedFact.EditedClientId = GetClientIdFromCell(p_editedFact.Cell);
      p_editedFact.EditedFactTag.Tag = GetTagTypeFromCell(p_editedFact.Cell);
      p_editedFact.OnCellValueChanged += new CellValueChangedEventHandler(p_rangeHighlighter.FillCellColor);
      p_editedFact.SetCellStatusRH();
      m_RHEditedFacts.Set(p_editedFact.Cell.Address, new DimensionKey(p_editedFact.EntityId, p_editedFact.AccountId, p_editedFact.EmployeeId, (Int32)p_editedFact.Period), p_editedFact);
    }

    private EditedRHFact CreateEditedFact(Dimension<CRUDEntity> p_dimension1, CRUDEntity p_dimensionValue1,
                                          Dimension<CRUDEntity> p_dimension2, CRUDEntity p_dimensionValue2,
                                          Dimension<CRUDEntity> p_fixedDimension,
                                          Range p_cell)
    {
      UInt32 l_accountId = m_RHAccountId;
      UInt32 l_entityId = 0;
      UInt32 l_clientId = 0;
      UInt32 l_productId = (UInt32)AxisType.Product;
      UInt32 l_adjustmentId = (UInt32)AxisType.Adjustment;
      UInt32 l_employeeId = 0;
      PeriodDimension l_period = null;

      Dimensions.SetDimensionValue(p_dimension1, p_dimensionValue1, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      Dimensions.SetDimensionValue(p_dimension2, p_dimensionValue2, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      Dimensions.SetDimensionValue(p_fixedDimension, p_fixedDimension.UniqueValue, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);

      if (l_employeeId == 0 || l_entityId == 0 || l_period == null)
        return null;
      return new EditedRHFact(l_accountId, l_entityId, l_clientId, l_productId, l_adjustmentId, l_employeeId, m_versionId, l_period, p_cell);
    }

    public void DownloadFacts(UInt32 p_versionId, List<Int32> p_periodsList, bool p_updateCells)
    {
      AddinModuleController.SetExcelInteractionState(false);
      // TO DO : Log and Check Download time
      m_updateCellsOnDownload = p_updateCells;
      AxisElem l_entity = m_dimensions.m_entities.UniqueValue as AxisElem;
      List<Account> l_accountsList = m_dimensions.GetAccountsList();
      List<AxisElem> l_employeesList = m_dimensions.GetAxisElemList(DimensionType.EMPLOYEE);
      Int32 l_startPeriod = PeriodModel.GetDayMinus3Weeks(p_periodsList.ElementAt(0));      // Download 3 weeks more for fact CP tag type purpose

      FactsModel.Instance.ReadEvent += AfterRHFactsDownloaded;
      m_requestIdList.Clear();
      foreach (AxisElem l_employee in l_employeesList)
      {
        m_requestIdList.Add(FactsModel.Instance.GetFact(m_RHAccountId, l_entity.Id, l_employee.Id, p_versionId, (UInt32)l_startPeriod, (UInt32)p_periodsList.ElementAt(p_periodsList.Count - 1)));
      }
    }

    private void AfterRHFactsDownloaded(ErrorMessage p_status, Int32 p_requestId, List<Fact> p_fact_list)
    {
      // TO DO : time up to manage the case where the server stops answering or the connection is lost : 
      //          -> In this case notify user and exit fact edition 
      AddinModuleController.SetExcelInteractionState(false);
      if (p_status == ErrorMessage.SUCCESS)
      {
        if (FillEditedFacts(p_fact_list) != true)
        {
          SetEditedFactsStatus();
          AddinModuleController.SetExcelInteractionState(true);
          FactsDownloaded(false);
          FactsModel.Instance.ReadEvent -= AfterRHFactsDownloaded;
        }
        m_requestIdList.Remove(p_requestId);
        if (m_requestIdList.Count == 0)
        {
          SetEditedFactsStatus();
          AddinModuleController.SetExcelInteractionState(true);
          FactsDownloaded(true);
          FactsModel.Instance.ReadEvent -= AfterRHFactsDownloaded;
        }
      }
      else
      {
        SetEditedFactsStatus();
        AddinModuleController.SetExcelInteractionState(true);
        FactsDownloaded(false);
        FactsModel.Instance.ReadEvent -= AfterRHFactsDownloaded;
      }
    }

    private void SetEditedFactsStatus()
    {
      AddinModuleController.SetExcelInteractionState(false);
      foreach (EditedRHFact l_editedFact in m_RHEditedFacts.Values)
      {
        l_editedFact.SetCellStatusRH();
      }
      AddinModuleController.SetExcelInteractionState(true);
    }

    private bool FillEditedFacts(List<Fact> p_factsList)
    {
      foreach (Fact l_fact in p_factsList)
      {
        DimensionKey l_dimensionKey = new DimensionKey(l_fact.EntityId, l_fact.AccountId, l_fact.EmployeeId, (Int32)l_fact.Period);
        if (l_fact.Period >= m_periodsList.ElementAt(0))
        {
          EditedRHFact l_RHEditedFact = m_RHEditedFacts[l_dimensionKey];
          if (l_RHEditedFact != null)
          {
            FactTag l_factTag = FactTagModel.Instance.GetValue(l_fact.Id);
            l_RHEditedFact.UpdateRHFact(l_fact, l_factTag);
            if (m_updateCellsOnDownload == true)
            {
              l_RHEditedFact.SetEditedClient(l_fact.ClientId);
              if (l_factTag != null)
                l_RHEditedFact.SetEditedFactType(l_factTag.Tag);
              l_RHEditedFact.Cell.Value2 = GetClientString(l_RHEditedFact, l_fact.ClientId, l_factTag);
            }
          }
        }
        else
          m_previousWeeksFacts.Add(l_dimensionKey, l_fact);
      }
      return true;
    }

    public bool UpdateEditedValues(Range p_cell)
    {
      if (m_RHEditedFacts.ContainsKey(p_cell.Address))
      {
        EditedRHFact l_editedFact = m_RHEditedFacts[p_cell.Address];
        UInt32 l_clientId = GetClientIdFromCell(p_cell);
        FactTag.TagType l_tagType = GetTagTypeFromCell(p_cell);
        l_editedFact.SetEditedClient(l_clientId);
        l_editedFact.SetEditedFactType(l_tagType);
        return true;
      }
      else
        return false;
    }

    // Externalize Commit ?
    public void Commit()
    {
      // TO DO
      // Antiduplicate system

      // put before calling commit ?
      if (m_clientsToBeCreated.Count > 0)
      {
        LaunchClientsAutoCreation();
        return;
      }

      SafeDictionary<string, Fact> l_factsCommitDict = new SafeDictionary<string, Fact>();
      List<FactTag> l_factsTagCommitDict = new List<FactTag>();
      foreach (EditedRHFact l_RHEditedFact in m_RHEditedFacts.Values)
      {
        if (l_RHEditedFact.EditedFactTag.Tag != FactTag.TagType.NONE)
        {
          UInt32 l_lastAllocatedClient = GetLastAllocatedClient(l_RHEditedFact);
          if (l_lastAllocatedClient != l_RHEditedFact.ClientId)
          {
            l_RHEditedFact.ClientId = l_lastAllocatedClient;
            l_factsCommitDict.Add(l_RHEditedFact.Cell.Address, l_RHEditedFact);
          }
        }
        else
        {
          if (l_RHEditedFact.EditedFactStatus == EditedFactStatus.DifferentInput)
          {
            l_RHEditedFact.ClientId = l_RHEditedFact.EditedClientId;
            if (l_RHEditedFact.EditedClientId == 0)
              m_requestIdDeletedFacts.Add(FactsModel.Instance.Delete(l_RHEditedFact), l_RHEditedFact);
            else
              l_factsCommitDict.Add(l_RHEditedFact.Cell.Address, l_RHEditedFact);
          }
        }

        if (l_RHEditedFact.EditedFactTag.Tag != l_RHEditedFact.ModelFactTag.Tag)
        {
          l_RHEditedFact.ModelFactTag.Tag = l_RHEditedFact.EditedFactTag.Tag;
          l_factsTagCommitDict.Add(l_RHEditedFact.EditedFactTag);
        }
      }
      FactsModel.Instance.UpdateList(l_factsCommitDict);
      FactTagModel.Instance.UpdateList(l_factsTagCommitDict);
    }

    private void LaunchClientsAutoCreation()
    {
      // TO DO
      // launch clients autocreation controller
    }

    private UInt32 GetLastAllocatedClient(EditedRHFact p_editedFact)
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
      return (UInt32)AxisType.Client;
    }

    private void AfterFactsCommit(ErrorMessage p_status, Dictionary<string, ErrorMessage> p_resultsDict)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        AddinModuleController.SetExcelInteractionState(false);
        foreach (KeyValuePair<string, ErrorMessage> l_addressMessagePair in p_resultsDict)
        {
          EditedRHFact l_editedFact = m_RHEditedFacts[l_addressMessagePair.Key];
          if (l_addressMessagePair.Value == ErrorMessage.SUCCESS)
            m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
          else
          {
            // put back model value for ClientId
            OnCommitError(l_addressMessagePair.Key, l_addressMessagePair.Value);
          }
        }

        AddinModuleController.SetExcelInteractionState(true);
      }
      else
      {
        // Create after commit event intercepted by the view -> only if commit fails
        // set commit status in the ribbon as well
      }
    }

    private void AfterFactDelete(ErrorMessage p_status, Int32 p_requestId)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        EditedRHFact l_editedFact = m_requestIdDeletedFacts[p_requestId];
        if (l_editedFact == null)
          return;
        m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
      }
      else
      {
        // put back edited client to fact Client Id value
        // Log commit error
      }
      m_requestIdDeletedFacts.Remove(p_requestId);   
    }

    private void AfterFactTagsCommit(ErrorMessage p_status, Dictionary<UInt32, bool> p_updateResults)
    {

      if (p_status == ErrorMessage.SUCCESS)
      {
        foreach (EditedRHFact l_editedFact in m_RHEditedFacts.Values)
        {
          if (p_updateResults.ContainsKey(l_editedFact.Id))
          {
            if (p_updateResults[l_editedFact.Id] == true)
              m_rangeHighlighter.FillCellGreen(l_editedFact.Cell);
            else
            {
              l_editedFact.ModelFactTag.Tag = FactTagModel.Instance.GetValue(l_editedFact.Id).Tag;
              OnCommitError(l_editedFact.Cell.Address, ErrorMessage.SYSTEM); // TO DO : facts tags should be commited like facts
            }
          }
        }
      }
      else
      {
        // Create after commit event intercepted by the view -> only if commit fails
        // set commit status in the ribbon as well
      }
    }

    #region Utils

    private string GetClientString(EditedRHFact p_editedFact, UInt32 p_clientId, FactTag p_factTag)
    {
      FactTag.TagType l_tag = FactTag.TagType.NONE;
      if (p_factTag != null)
        l_tag = p_factTag.Tag;
      if (l_tag != FactTag.TagType.NONE)
        return m_factsTagList.ElementAt((Int32)l_tag);
      else
      {
        AxisElem l_client = AxisElemModel.Instance.GetValue(AxisType.Client, p_clientId);
        if (l_client != null)
          return l_client.Name;
      }
      return "";
    }

    private UInt32 GetClientIdFromCell(Range p_cell)
    {
      if (p_cell.Value2 == null)
        return 0;
      if (p_cell.Value2.GetType() == typeof(string))
      {
        AxisElem l_client = AxisElemModel.Instance.GetValue(AxisType.Client, p_cell.Value2 as string);
        if (l_client != null)
          return l_client.Id;
        else
        {
          if (m_factsTagList.Contains(p_cell.Value2 as string) == false)
            m_clientsToBeCreated.Add(p_cell.Value2 as string);
        }
      }
      return 0;
    }

    private FactTag.TagType GetTagTypeFromCell(Range p_cell)
    {
      if (p_cell.Value2 == null)
        return FactTag.TagType.NONE;
      if (m_factsTagList.Contains(p_cell.Value2 as string))
      {
        return (FactTag.TagType)m_factsTagList.FindIndex(x => x.StartsWith(p_cell.Value2 as string));
      }
      return FactTag.TagType.NONE;
    }

    #endregion

  }
}
