using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Model
{
  using CRUD;
  using Network;
  using Utils;


  class RHEditedFactsManager : IEditedFactsManager
  {
    MultiIndexDictionary<Range, DimensionKey, EditedFact> m_RHEditedFacts = new MultiIndexDictionary<Range, DimensionKey, EditedFact>();
    Dimensions m_dimensions = null;
    List<int> m_requestIdList = new List<int>();
    public event OnFactsDownloaded FactsDownloaded;
    Worksheet m_worksheet;
    public bool m_autoCommit { set; get; }
    private bool m_updateCellsOnDownload;
    UInt32 m_RHAccountId;
    UInt32 m_versionId;

    public void RegisterEditedFacts(Dimensions p_dimensions, Worksheet p_worksheet, UInt32 p_versionId, UInt32 p_RHAccountId)
    {
      m_worksheet = p_worksheet;
      m_dimensions = p_dimensions;
      m_versionId = p_versionId;
      m_RHAccountId = p_RHAccountId;
      Dimension<CRUDEntity> l_vertical = p_dimensions.m_dimensions[p_dimensions.m_orientation.Vertical];
      Dimension<CRUDEntity> l_horitontal = p_dimensions.m_dimensions[p_dimensions.m_orientation.Horizontal];

      CreateEditedFacts(l_vertical, l_horitontal, p_dimensions.m_entities);
  
      // TO DO : Once facts registered clean dimensions and put them into a safe dictionary ?
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
            m_RHEditedFacts.Set(l_factCell, new DimensionKey(l_editedFact.EntityId, l_editedFact.AccountId,l_editedFact.EmployeeId, (Int32)l_editedFact.Period), l_editedFact);
        }
      }
    }

    private EditedFact CreateEditedFact(Dimension<CRUDEntity> p_dimension1, CRUDEntity p_dimensionValue1,
                                        Dimension<CRUDEntity> p_dimension2, CRUDEntity p_dimensionValue2,
                                        Dimension<CRUDEntity> p_fixedDimension,
                                        Range p_cell)
    {
      UInt32 l_accountId = m_RHAccountId;
      UInt32 l_entityId = 0;
      UInt32 l_clientId = (UInt32)AxisType.Client;
      UInt32 l_productId = (UInt32)AxisType.Product;
      UInt32 l_adjustmentId = (UInt32)AxisType.Adjustment;
      UInt32 l_employeeId = 0;
      PeriodDimension l_period = null;

      Dimensions.SetDimensionValue(p_dimension1, p_dimensionValue1, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      Dimensions.SetDimensionValue(p_dimension2, p_dimensionValue2, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      Dimensions.SetDimensionValue(p_fixedDimension, p_fixedDimension.UniqueValue, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);

      if (l_employeeId == 0 || l_entityId == 0 || l_period == null)
        return null;
      return new EditedFact(l_accountId, l_entityId, l_clientId, l_productId, l_adjustmentId, l_employeeId, m_versionId, l_period, p_cell, Account.AccountProcess.RH);
    }

    public void DownloadFacts(UInt32 p_versionId, List<Int32> p_periodsList, bool p_updateCells)
    {
      // TO DO : Log and Check Download time
      m_updateCellsOnDownload = p_updateCells;
      AxisElem l_entity = m_dimensions.m_entities.UniqueValue as AxisElem;
      List<Account> l_accountsList = m_dimensions.GetAccountsList();
      List<AxisElem> l_employeesList = m_dimensions.GetAxisElemList(DimensionType.EMPLOYEE);
      Int32 l_startPeriod = p_periodsList.ElementAt(0);
      Int32 l_endPeriod = p_periodsList.ElementAt(p_periodsList.Count-1);

      FactsModel.Instance.ReadEvent += AfterRHFactsDownloaded;
      m_requestIdList.Clear();
      foreach (AxisElem l_employee in l_employeesList)
      {
        m_requestIdList.Add(FactsModel.Instance.GetFact(m_RHAccountId, l_entity.Id, l_employee.Id, p_versionId, (UInt32)l_startPeriod, (UInt32)l_endPeriod));
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
        DimensionKey l_dimensionKey = new DimensionKey(l_fact.EntityId, l_fact.AccountId, l_fact.EmployeeId, (Int32)l_fact.Period);
        EditedFact l_RHEditedFact = m_RHEditedFacts[l_dimensionKey];
        if (l_RHEditedFact != null)
        {
          l_RHEditedFact.UpdateFact(l_fact);
          if (m_updateCellsOnDownload)
            SetInputCellValue(l_RHEditedFact, l_fact.ClientId);
        }
      }
      return true;
    }

    private void SetInputCellValue(EditedFact p_RHEditedFact, UInt32 p_clientId)
    {
      AxisElem l_client = AxisElemModel.Instance.GetValue(AxisType.Client, p_clientId);
      if (l_client == null)
        return;
      p_RHEditedFact.Cell.Value2 = l_client.Name;
      p_RHEditedFact.EditedClientId = p_clientId;
    }

    //public void UpdateWorksheetInputs()
    //{
    //  foreach (EditedFact l_editedFact in m_RHEditedFacts.Values)
    //  {
    //    l_editedFact.UpdateCellValue();
    //  }
    //}

    public void CommitDifferences()
    {
      List<Fact> m_factsToBeCommitted = new List<Fact>();
      foreach (EditedFact l_RHEditedFact in m_RHEditedFacts.Values)
      {
        if (l_RHEditedFact.CellStatus == CellStatus.DifferentInput)
          m_factsToBeCommitted.Add(l_RHEditedFact);
      }

      // TO DO
      // RH Process : Antiduplicate system + Create New clients

      // associate updates to cells
      // ready to listen server answer
      //   -> update cells color on worksheet if success
    }

  }
}
