﻿using System;
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

  class RHEditedFactsModel : AEditedFactsModel
  {
    public MultiIndexDictionary<string, DimensionKey, EditedRHFact> EditedFacts { get; private set; }
    WorksheetAreaController m_areaController = null;
    private bool m_updateCellsOnDownload;
    UInt32 m_RHAccountId;
    UInt32 m_versionId;
    List<string> m_factsTagList;
    List<string> m_legalHolidayTagList;
    SafeDictionary<string, EditedRHFact> m_clientsToBeCreated = new SafeDictionary<string, EditedRHFact>();
    private List<Int32> m_periodsList;
    public bool DisplayInitialDifference { get; private set; }
    public SafeDictionary<DimensionKey, Fact> PreviousWeeksFacts { get; set; }
    public SafeDictionary<UInt32, EditedRHFact> IdEditedFactDict { get; set; }

    #region Initialize

    public RHEditedFactsModel(List<Int32> p_periodsList, Worksheet p_worksheet)
      : base(p_worksheet)
    {
      EditedFacts = new MultiIndexDictionary<string, DimensionKey, EditedRHFact>();
      m_factsTagList = StringUtils.ToLowerStringList(Enum.GetNames(typeof(FactTag.TagType)));
      m_legalHolidayTagList = StringUtils.ToLowerStringList(Enum.GetNames(typeof(LegalHolidayTag)));
      m_periodsList = p_periodsList;
      PreviousWeeksFacts = new SafeDictionary<DimensionKey, Fact>();
      IdEditedFactDict = new SafeDictionary<UInt32, EditedRHFact>();
      FactsModel.Instance.ReadEvent += OnFactsDownloaded;
    }

    public override void Commit()
    {
      throw new NotImplementedException();
    }

    public override void Close()
    {
      FactsModel.Instance.ReadEvent -= OnFactsDownloaded;
    }

    public override void RegisterEditedFacts(WorksheetAreaController p_dimensions, UInt32 p_versionId, bool p_displayInitialDifferences, UInt32 p_RHAccountId = 0)
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
        return;
      DisplayInitialDifference = p_displayInitialDifferences;
      m_areaController = p_dimensions;
      m_versionId = p_versionId;
      m_RHAccountId = p_RHAccountId;

      Dimension<CRUDEntity> l_vertical = p_dimensions.Dimensions[p_dimensions.Orientation.Vertical];
      Dimension<CRUDEntity> l_horitontal = p_dimensions.Dimensions[p_dimensions.Orientation.Horizontal];

      CreateEditedFacts(l_vertical, l_horitontal, p_dimensions.Entities);
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
          EditedRHFact l_editedFact = CreateEditedFact(p_rowsDimension, l_rowsKeyPair.Value, p_columnsDimension, l_columnsKeyPair.Value, p_fixedDimension, l_factCell);
          if (l_editedFact != null)
            InitializeEditedFact(l_editedFact);
        }
      }
    }

    private void InitializeEditedFact(EditedRHFact p_editedFact)
    {
      EditedFacts.Set(p_editedFact.Cell.Address, new DimensionKey(p_editedFact.EntityId, p_editedFact.AccountId, p_editedFact.EmployeeId, (Int32)p_editedFact.Period), p_editedFact);
      p_editedFact.EditedClientId = GetClientIdFromCell(p_editedFact.Cell);
      p_editedFact.EditedFactTag.Tag = GetTagTypeFromCell(p_editedFact.Cell);
      p_editedFact.EditedLegalHoliday.Tag = GetLegalHolidayTagFromCell(p_editedFact.Cell);
    }

    private EditedRHFact CreateEditedFact(Dimension<CRUDEntity> p_dimension1, CRUDEntity p_dimensionValue1,
                                          Dimension<CRUDEntity> p_dimension2, CRUDEntity p_dimensionValue2,
                                          Dimension<CRUDEntity> p_fixedDimension,
                                          Range p_cell)
    {
      UInt32 l_accountId = m_RHAccountId;
      UInt32 l_entityId = 0;
      UInt32 l_clientId = 0;
      UInt32 l_productId = ProductId;
      UInt32 l_adjustmentId = AdjustmentId;
      UInt32 l_employeeId = 0;
      PeriodDimension l_period = null;

      WorksheetAreaController.SetDimensionValue(p_dimension1, p_dimensionValue1, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      WorksheetAreaController.SetDimensionValue(p_dimension2, p_dimensionValue2, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      WorksheetAreaController.SetDimensionValue(p_fixedDimension, p_fixedDimension.UniqueValue, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);

      if (l_employeeId == 0 || l_entityId == 0 || l_period == null)
        return null;
      return new EditedRHFact(l_accountId, l_entityId, l_clientId, l_productId, l_adjustmentId, l_employeeId, m_versionId, l_period, p_cell);
    }

    #endregion

    public override void DownloadFacts(List<Int32> p_periodsList, bool p_updateCells, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      AddinModuleController.SetExcelInteractionState(false);
      m_updateCellsOnDownload = p_updateCells;
      DownloadLegalHolidays();

      AxisElem l_entity = m_areaController.Entities.UniqueValue as AxisElem;
      List<Account> l_accountsList = m_areaController.GetAccountsList();
      List<AxisElem> l_employeesList = m_areaController.GetAxisElemList(DimensionType.EMPLOYEE);
      Int32 l_startPeriod = PeriodModel.GetDayMinus3Weeks(p_periodsList.ElementAt(0)); // Download 3 weeks more for fact CP tag type purpose

      RequestIdList.Clear();
      RequestIdList.Add(FactsModel.Instance.GetFactRH(m_RHAccountId, l_entity.Id, l_employeesList, m_versionId, (UInt32)l_startPeriod, (UInt32)p_periodsList.ElementAt(p_periodsList.Count - 1)));
    }

    private void OnFactsDownloaded(ErrorMessage p_status, Int32 p_requestId, List<Fact> p_fact_list)
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
        return;
      AddinModuleController.SetExcelInteractionState(false);
      if (p_status == ErrorMessage.SUCCESS)
      {
        if (FillEditedFacts(p_fact_list) == false)
        {
          AddinModuleController.SetExcelInteractionState(true);
          RaiseFactDownloaded(false);
        }
        RequestIdList.Remove(p_requestId);
        if (RequestIdList.Count == 0)
        {
          AddinModuleController.SetExcelInteractionState(true);
          RaiseFactDownloaded(true);
        }
      }
      else
      {
        AddinModuleController.SetExcelInteractionState(true);
        RaiseFactDownloaded(false);
      }
    }

    public bool FillEditedFacts(List<Fact> p_factsList)
    {
      PreviousWeeksFacts.Clear();
      IdEditedFactDict.Clear();

      foreach (Fact l_fact in p_factsList)
      {
        DimensionKey l_dimensionKey = new DimensionKey(l_fact.EntityId, l_fact.AccountId, l_fact.EmployeeId, (Int32)l_fact.Period);
        if (l_fact.Period >= m_periodsList.ElementAt(0))
        {
          EditedRHFact l_RHEditedFact = EditedFacts[l_dimensionKey];
          if (l_RHEditedFact != null)
          {
            IdEditedFactDict[l_fact.Id] = l_RHEditedFact;
            FactTag l_factTag = FactTagModel.Instance.GetValue(l_fact.Id);
            LegalHoliday l_legalHoliday = LegalHolidayModel.Instance.GetValue(l_fact.EmployeeId, l_fact.Period);
            l_RHEditedFact.UpdateRHFactModels(l_fact, l_factTag, l_legalHoliday);
            if (m_updateCellsOnDownload == true)
            {
              l_RHEditedFact.SetEditedClient(l_fact.ClientId, false);
              if (l_factTag != null)
                l_RHEditedFact.SetEditedFactType(l_factTag.Tag, false);
              l_RHEditedFact.Cell.Value2 = GetClientString(l_RHEditedFact, l_fact.ClientId, l_factTag);
            }
          }
        }
        else
          PreviousWeeksFacts[l_dimensionKey] = l_fact;
      }
      return true;
    }

    private void DownloadLegalHolidays()
    {
      AddinModuleController.SetExcelInteractionState(false);
      foreach (EditedRHFact l_editedFact in EditedFacts.Values)
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
            l_editedFact.SetEditedLegalHoliday(LegalHolidayTag.FER, false);
            l_editedFact.Cell.Value2 = m_legalHolidayTagList[(Int32)LegalHolidayTag.FER].ToUpper();
          }
        }
      }
    }

    public override EditedFactBase UpdateEditedValueAndTag(Range p_cell)
    {
      EditedRHFact l_editedFact = EditedFacts[p_cell.Address];
      if (l_editedFact == null)
        return null;
      
      UInt32 l_clientId = (UInt32)GetClientIdFromCell(p_cell);
      FactTag.TagType l_tagType = GetTagTypeFromCell(p_cell);
      LegalHolidayTag l_legalHolidayTag = GetLegalHolidayTagFromCell(p_cell);
           
      l_editedFact.SetEditedClient(l_clientId, false);
      l_editedFact.SetEditedFactType(l_tagType, false);
      l_editedFact.SetEditedLegalHoliday(l_legalHolidayTag, false);
      return l_editedFact;
    }

    public override double? CellBelongToOutput(Range p_cell)
    {
      return (null);
    }

    public override double? CellBelongToInput(Range p_cell)
    {
      return (null);
    }

    public override void Refresh()
    {

    }

    #region Utils

    private string GetClientString(EditedRHFact p_editedFact, UInt32 p_clientId, FactTag p_factTag)
    {
      FactTag.TagType l_tag = FactTag.TagType.NONE;
      if (p_factTag != null)
        l_tag = p_factTag.Tag;
      if (l_tag != FactTag.TagType.NONE && m_factsTagList.Count > (Int32)l_tag)
        return m_factsTagList.ElementAt((Int32)l_tag).ToUpper();
      else
      {
        AxisElem l_client = AxisElemModel.Instance.GetValue(AxisType.Client, p_clientId);
        if (l_client != null)
          return l_client.Name;
      }
      return "";
    }

    private Int32 GetClientIdFromCell(Range p_cell)
    {
      if (p_cell.Value2 == null)
        return 0;
      if (p_cell.Value2.GetType() == typeof(string))
      {
        AxisElem l_client = AxisElemModel.Instance.GetValue(AxisType.Client, p_cell.Value2 as string);
        if (l_client != null)
          return (Int32)l_client.Id;
        else
        {
          string l_value = StringUtils.RemoveDiacritics(p_cell.Value2 as string);
          if (l_value == "") 
            return 0;

          if (m_factsTagList.Contains(l_value) == false && m_legalHolidayTagList.Contains(l_value) == false)
          {
            EditedRHFact l_editedFact = EditedFacts[p_cell.Address];
            if (l_editedFact != null)
              m_clientsToBeCreated[p_cell.Value2 as string] = l_editedFact;
            return -1;
          }
        }
      }
      return 0;
    }

    private FactTag.TagType GetTagTypeFromCell(Range p_cell)
    {
      if (p_cell.Value2 == null)
        return FactTag.TagType.NONE;

      string l_value = StringUtils.RemoveDiacritics(p_cell.Value2 as string);
      if (l_value == "")
        return FactTag.TagType.NONE;
      if (m_factsTagList.Contains(l_value))
        return (FactTag.TagType)m_factsTagList.FindIndex(x => x.StartsWith(l_value));
      return FactTag.TagType.NONE;
    }

    private LegalHolidayTag GetLegalHolidayTagFromCell(Range p_cell)
    {
      if (p_cell.Value2 == null)
        return LegalHolidayTag.NONE;

      string l_value = StringUtils.RemoveDiacritics(p_cell.Value2 as string);
      if (l_value == "")
        return LegalHolidayTag.NONE;
      
      if (m_legalHolidayTagList.Contains(l_value))
        return (LegalHolidayTag)m_legalHolidayTagList.FindIndex(x => x.StartsWith(l_value));

      return LegalHolidayTag.NONE;
    }

    public List<string> GetClientsToBeCreatedList()
    {
      SafeDictionary<string, EditedRHFact> l_newClientsToBeCreated = new SafeDictionary<string, EditedRHFact>();

      for (UInt32 l_iterator = 0; l_iterator < m_clientsToBeCreated.Keys.Count; l_iterator += 1)
      {
        string l_undefinedClient = m_clientsToBeCreated.ElementAt((Int32)l_iterator).Key;
        AxisElem l_client = AxisElemModel.Instance.GetValue(AxisType.Client, l_undefinedClient);
        EditedRHFact l_editedFact = m_clientsToBeCreated.ElementAt((Int32)l_iterator).Value;

        if (l_client == null && l_editedFact.EditedClientId == -1)
          l_newClientsToBeCreated[l_undefinedClient] = l_editedFact; 
        }
      m_clientsToBeCreated = l_newClientsToBeCreated;
      return l_newClientsToBeCreated.Keys.ToList<string>();
    }

    #endregion

  }
}
