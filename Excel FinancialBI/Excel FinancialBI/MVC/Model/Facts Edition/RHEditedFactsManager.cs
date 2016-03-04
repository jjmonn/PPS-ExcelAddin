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

//  public delegate void FactsCommitError(string p_address, ErrorMessage p_error);

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
    List<string> m_legalHolidayTagList;
    List<string> m_clientsToBeCreated = new List<string>();
    private List<Int32> m_periodsList;
    FactsRHCommit m_factsCommit;


    public RHEditedFactsManager(List<Int32> p_periodsList)
    {
      m_factsTagList =  StringUtils.ToLowerStringList(Enum.GetNames(typeof(FactTag.TagType)));
      m_legalHolidayTagList = StringUtils.ToLowerStringList(Enum.GetNames(typeof(LegalHolidayTag)));
      m_periodsList = p_periodsList;
    }

    public void Dispose()
    {

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
      p_editedFact.EditedLegalHoliday.Tag = GetLegalHolidayTagFromCell(p_editedFact.Cell);

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

    public void DownloadFacts(List<Int32> p_periodsList, bool p_updateCells)
    {
      AddinModuleController.SetExcelInteractionState(false);
      m_updateCellsOnDownload = p_updateCells;
      DownloadLegalHolidays();

      AxisElem l_entity = m_dimensions.m_entities.UniqueValue as AxisElem;
      List<Account> l_accountsList = m_dimensions.GetAccountsList();
      List<AxisElem> l_employeesList = m_dimensions.GetAxisElemList(DimensionType.EMPLOYEE);
      Int32 l_startPeriod = PeriodModel.GetDayMinus3Weeks(p_periodsList.ElementAt(0));      // Download 3 weeks more for fact CP tag type purpose

      FactsModel.Instance.ReadEvent += AfterRHFactsDownloaded;
      m_requestIdList.Clear();
      m_requestIdList.Add(FactsModel.Instance.GetFactRH(m_RHAccountId, l_entity.Id, l_employeesList, m_versionId, (UInt32)l_startPeriod, (UInt32)p_periodsList.ElementAt(p_periodsList.Count - 1)));
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

    private void DownloadLegalHolidays()
    {
      AddinModuleController.SetExcelInteractionState(false);
      foreach (EditedRHFact l_editedFact in m_RHEditedFacts.Values)
      {
        LegalHoliday l_legalHoliday = LegalHolidayModel.Instance.GetValue(l_editedFact.EmployeeId, l_editedFact.Period);
        if (l_legalHoliday != null)
        {
          l_editedFact.ModelLegalHoliday.Id = l_legalHoliday.Id;
          l_editedFact.ModelLegalHoliday.Tag = LegalHolidayTag.FER;
          l_editedFact.ClientId = 0;
          l_editedFact.ModelFactTag.Tag = FactTag.TagType.NONE;
    
          if (m_updateCellsOnDownload == true)
          {
            l_editedFact.SetEditedLegalHoliday(LegalHolidayTag.FER);
            l_editedFact.Cell.Value2 = m_legalHolidayTagList[(Int32)LegalHolidayTag.FER].ToUpper();
          }
        }
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
      SafeDictionary<DimensionKey, Fact> l_previousWeeksFacts = new SafeDictionary<DimensionKey, Fact>();
      SafeDictionary<UInt32, EditedRHFact> l_IdEditedFactDict = new SafeDictionary<UInt32, EditedRHFact>();
  
      foreach (Fact l_fact in p_factsList)
      {
        DimensionKey l_dimensionKey = new DimensionKey(l_fact.EntityId, l_fact.AccountId, l_fact.EmployeeId, (Int32)l_fact.Period);
        if (l_fact.Period >= m_periodsList.ElementAt(0))
        {
          EditedRHFact l_RHEditedFact = m_RHEditedFacts[l_dimensionKey];
          if (l_RHEditedFact != null)
          {
            l_IdEditedFactDict.Add(l_fact.Id, l_RHEditedFact);
            FactTag l_factTag = FactTagModel.Instance.GetValue(l_fact.Id);
            LegalHoliday l_legalHoliday = LegalHolidayModel.Instance.GetValue(l_fact.EmployeeId, l_fact.Period);
            l_RHEditedFact.UpdateRHFactModels(l_fact, l_factTag, l_legalHoliday);
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
          l_previousWeeksFacts.Add(l_dimensionKey, l_fact);
      }
      m_factsCommit = new FactsRHCommit(m_RHEditedFacts, l_previousWeeksFacts, l_IdEditedFactDict, m_periodsList, m_rangeHighlighter, m_RHAccountId, m_versionId);
      return true;
    }

    public bool UpdateEditedValueAndTag(Range p_cell)
    {
      EditedRHFact l_editedFact = m_RHEditedFacts[p_cell.Address];
      if (l_editedFact == null)
        return false;
      
      UInt32 l_clientId = GetClientIdFromCell(p_cell);
      FactTag.TagType l_tagType = GetTagTypeFromCell(p_cell);
      LegalHolidayTag l_legalHolidayTag = GetLegalHolidayTagFromCell(p_cell);
      
      l_editedFact.SetEditedClient(l_clientId);
      l_editedFact.SetEditedFactType(l_tagType);
      l_editedFact.SetEditedLegalHoliday(l_legalHolidayTag);
      return true;
    }

    public void Commit()
    {
      if (m_factsCommit != null)
        m_factsCommit.Commit();
      else
      {
        // TO DO : report error
      }
    }

    // TO DO
    // Antiduplicate system

    // put before calling commit ?
    //if (m_clientsToBeCreated.Count > 0)
    //{
    //  LaunchClientsAutoCreation();
    //  return;
    //}
  
    #region Utils

    private string GetClientString(EditedRHFact p_editedFact, UInt32 p_clientId, FactTag p_factTag)
    {
      FactTag.TagType l_tag = FactTag.TagType.NONE;
      if (p_factTag != null)
        l_tag = p_factTag.Tag;
      if (l_tag != FactTag.TagType.NONE)
        return m_factsTagList.ElementAt((Int32)l_tag).ToUpper();
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
          string l_value = p_cell.Value2 as string;
          if (m_factsTagList.Contains(l_value.ToLower()) == false)
             m_clientsToBeCreated.Add(p_cell.Value2 as string);
        }
      }
      return 0;
    }

    private FactTag.TagType GetTagTypeFromCell(Range p_cell)
    {
      if (p_cell.Value2 == null)
        return FactTag.TagType.NONE;

      string l_value = p_cell.Value2 as string;
      if (m_factsTagList.Contains(l_value.ToLower()))
      {
        return (FactTag.TagType)m_factsTagList.FindIndex(x => x.StartsWith(l_value.ToLower()));
      }
      return FactTag.TagType.NONE;
    }

    private LegalHolidayTag GetLegalHolidayTagFromCell(Range p_cell)
    {
      if (p_cell.Value2 == null)
        return LegalHolidayTag.NONE;

      string l_value = p_cell.Value2 as string;
      if (m_legalHolidayTagList.Contains(l_value.ToLower()))
      {
        return (LegalHolidayTag)m_legalHolidayTagList.FindIndex(x => x.StartsWith(l_value.ToLower()));
      }

      return LegalHolidayTag.NONE;
    }
  
    #endregion

  }
}
