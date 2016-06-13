using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Model
{
  using Model.CRUD;
  using View;
  using Network;
  using Utils;
  using Model;
  using Controller;

  public delegate void OnComputeFailed();
  public delegate void ComputeProgressEvent(Int32 p_percentage);
  public delegate void DownloadProgressEvent(Int32 p_percentage);

  class FinancialEditedFactsModel : AEditedFactsModel
  {
    public MultiIndexDictionary<string, DimensionKey, EditedFinancialFact> EditedFacts { get; private set; }
    SafeDictionary<DimensionKey, Fact> m_facts = new SafeDictionary<DimensionKey, Fact>();
    public MultiIndexDictionary<string, DimensionKey, EditedFinancialFact> OutputFacts { get; private set; }
    public event OnComputeFailed ComputeFailed;
    public event ComputeProgressEvent ComputeProgress;
    public event ComputeProgressEvent DownloadProgress;
    WorksheetAreaController m_dimensions = null;
    private bool m_updateCellsOnDownload;
    UInt32 m_versionId;
    private List<Int32> m_periodsList;
    bool m_displayDiff = true;
    bool m_needRefresh = false;
    public int m_nbRequest = 0;
    public int m_nbCompute = 0;
    UInt32 m_computeId = 0;

    #region Initialize

    public FinancialEditedFactsModel(Worksheet p_worksheet) : base(p_worksheet)
    {
      EditedFacts = new MultiIndexDictionary<string, DimensionKey, EditedFinancialFact>();
      OutputFacts = new MultiIndexDictionary<string, DimensionKey, EditedFinancialFact>();

      FactsModel.Instance.ReadEvent += OnFinancialInputDownloaded;
      SourcedComputeModel.Instance.ComputeCompleteEvent += OnComputeCompleteEvent;
    }

    public override void Close()
    {
      FactsModel.Instance.ReadEvent -= OnFinancialInputDownloaded;
      SourcedComputeModel.Instance.ComputeCompleteEvent -= OnComputeCompleteEvent;
    }

    void OnComputeCompleteEvent(ErrorMessage p_status, SourcedComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result)
    {
      bool l_found = false;

      foreach (ComputeResult l_result in p_result.Values)
        if (RequestIdList.Contains(l_result.RequestId))
        {
          l_found = true;
          RequestIdList.Remove(l_result.RequestId);
        }
      if (l_found)
        OnFinancialOutputsComputed(p_status, p_request, p_result);
    }

    public override void RegisterEditedFacts(WorksheetAreaController p_dimensions, UInt32 p_versionId, bool p_displayInitialDifferences, UInt32 p_RHAccountId = 0)
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
        return;
      m_dimensions = p_dimensions;
      m_versionId = p_versionId;

      Version l_version = VersionModel.Instance.GetValue(m_versionId);

      if (l_version == null)
        m_periodsList = new List<int>();
      else
        m_periodsList = PeriodModel.GetPeriodList((int)l_version.StartPeriod, (int)l_version.NbPeriod, l_version.TimeConfiguration);
      Dimension<CRUDEntity> l_vertical = p_dimensions.Dimensions[p_dimensions.Orientation.Vertical];
      Dimension<CRUDEntity> l_horitontal = p_dimensions.Dimensions[p_dimensions.Orientation.Horizontal];
      Dimension<CRUDEntity> l_tabDimension = p_dimensions.Dimensions[p_dimensions.Orientation.TabDimension];

      m_displayDiff = p_displayInitialDifferences;
      CreateEditedFacts(l_vertical, l_horitontal, l_tabDimension);
    }

    private void CreateEditedFacts(Dimension<CRUDEntity> p_rowsDimension, Dimension<CRUDEntity> p_columnsDimension, Dimension<CRUDEntity> p_fixedDimension)
    {
      SafeDictionary<UInt32, UInt32> l_periodIndexDic = new SafeDictionary<uint,uint>();
      Version l_version = VersionModel.Instance.GetValue(m_versionId);

      if (l_version == null)
      {
        System.Diagnostics.Debug.WriteLine("FinancialEditedFactsModel.CreateEditedFact: Invalid version");
        return;
      }
      UInt32 l_periodIndex = 0;
      foreach (int l_period in m_periodsList)
        l_periodIndexDic[(UInt32)l_period] = l_periodIndex++;
      foreach (KeyValuePair<string, CRUDEntity> l_rowsKeyPair in p_rowsDimension.m_values)
      {
        foreach (KeyValuePair<string, CRUDEntity> l_columnsKeyPair in p_columnsDimension.m_values)
        {
          Range l_rowCell = m_worksheet.Range[l_rowsKeyPair.Key];
          Range l_columnCell = m_worksheet.Range[l_columnsKeyPair.Key];
          Range l_factCell = m_worksheet.Cells[l_rowCell.Row, l_columnCell.Column] as Range;
          EditedFinancialFact l_editedFact = CreateEditedFact(p_rowsDimension, l_rowsKeyPair.Value, p_columnsDimension, l_columnsKeyPair.Value, p_fixedDimension, l_factCell);
          if (l_editedFact != null)
          {
            // Set Edited Value           
            Account.FormulaTypes l_formulaType = l_editedFact.Account.FormulaType;
            if (l_formulaType == Account.FormulaTypes.HARD_VALUE_INPUT ||
              (l_formulaType == Account.FormulaTypes.FIRST_PERIOD_INPUT && 
              (l_periodIndexDic[l_editedFact.Period] < (Int32)l_version.FormulaPeriodIndex ||
              l_periodIndexDic[l_editedFact.Period] >= l_version.FormulaPeriodIndex + l_version.FormulaNbPeriod)))
              EditedFacts.Set(l_factCell.Address, new DimensionKey(l_editedFact.EntityId, l_editedFact.AccountId, l_editedFact.EmployeeId, (Int32)l_editedFact.Period), l_editedFact);
            else if (l_formulaType != Account.FormulaTypes.TITLE)
              OutputFacts.Set(l_factCell.Address, new DimensionKey(l_editedFact.EntityId, l_editedFact.AccountId, l_editedFact.EmployeeId, (Int32)l_editedFact.Period), l_editedFact);
          }
        }
      }
    }

    private EditedFinancialFact CreateEditedFact(Dimension<CRUDEntity> p_dimension1, CRUDEntity p_dimensionValue1,
                                        Dimension<CRUDEntity> p_dimension2, CRUDEntity p_dimensionValue2,
                                        Dimension<CRUDEntity> p_fixedDimension,
                                        Range p_cell)
    {
      UInt32 l_accountId = 0;
      UInt32 l_entityId = 0;
      UInt32 l_clientId = ClientId;
      UInt32 l_productId = ProductId;
      UInt32 l_adjustmentId = AdjustmentId;
      UInt32 l_employeeId = EmployeeId;
      PeriodDimension l_period = null;

      WorksheetAreaController.SetDimensionValue(p_dimension1, p_dimensionValue1, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      WorksheetAreaController.SetDimensionValue(p_dimension2, p_dimensionValue2, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);
      WorksheetAreaController.SetDimensionValue(p_fixedDimension, p_fixedDimension.UniqueValue, ref l_accountId, ref l_entityId, ref l_employeeId, ref l_period);

      if (l_accountId == 0 || l_entityId == 0 || l_period == null)
        return null;
      return new EditedFinancialFact(l_accountId, l_entityId, l_clientId, l_productId, l_adjustmentId, l_employeeId, m_versionId, l_period, p_cell);
    }

    #endregion

    public override void DownloadFacts(List<Int32> p_periodsList, bool p_updateCells, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      m_updateCellsOnDownload = p_updateCells;
      if (m_updateCellsOnDownload)
        foreach (EditedFactBase l_fact in EditedFacts.Values)
        {
          l_fact.Value = 0;
          l_fact.EditedValue = 0;
          l_fact.Cell.Value2 = 0;
        }
      List<AxisElem> l_entitiesList = m_dimensions.GetAxisElemList(DimensionType.ENTITY);

      RequestIdList.Clear();
      AddinModuleController.SetExcelInteractionState(false);
      RequestIdList.Add(FactsModel.Instance.GetFactFinancial(l_entitiesList, m_versionId, p_clientId, p_productId, p_adjustmentId));
      m_nbRequest++;
    }

    private void OnFinancialInputDownloaded(ErrorMessage p_status, Int32 p_requestId, List<Fact> p_factsList)
    {
      if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
        return;
      if (p_status != ErrorMessage.SUCCESS)
      {
        RaiseFactDownloaded(false);
        return;
      }
      SafeDictionary<DimensionKey, Fact> l_downloadedFactDic = new SafeDictionary<DimensionKey, Fact>();
      foreach (Fact l_fact in p_factsList)
        l_downloadedFactDic[new DimensionKey(l_fact.EntityId, l_fact.AccountId, (UInt32)AxisType.Employee, (Int32)l_fact.Period)] = l_fact;
      int l_count = 0;
      int l_nbFacts = EditedFacts.Count;

      foreach (DimensionKey l_key in EditedFacts.SecondaryKeys)
      {
        l_count++;
        EditedFinancialFact l_editedFact = EditedFacts[l_key];
        if (l_editedFact != null)
        {
          Fact l_fact = l_downloadedFactDic[l_key];
          l_downloadedFactDic.Remove(l_key);

          if (l_fact == null)
          {
            l_fact = l_editedFact.Clone();
            l_fact.Value = 0;
          }

          double l_editedValue = l_editedFact.EditedValue;
          l_editedFact.UpdateFinancialFact(l_fact);
          if (m_displayDiff)
            l_editedFact.EditedValue = l_editedValue;
          if (m_updateCellsOnDownload)
            l_editedFact.Cell.Value2 = l_editedFact.Value;

          if ((l_count % 50) == 0 && DownloadProgress != null)
            DownloadProgress((int)((l_count / (double)l_nbFacts) * 100));

        }
      }
      foreach (KeyValuePair<DimensionKey, Fact> l_pair in l_downloadedFactDic)
        m_facts[l_pair.Key] = l_pair.Value;
      RequestIdList.Remove(p_requestId);
      if (RequestIdList.Count == 0)
        ComputeOutputs();
    }

    public void ComputeOutputs()
    {
      SourcedComputeRequest l_sourcedComputeRequest = new SourcedComputeRequest();
      Version l_version = VersionModel.Instance.GetValue(m_versionId);

      if (l_version == null)
        return;
      l_sourcedComputeRequest.VersionId = m_versionId;
      l_sourcedComputeRequest.StartPeriod = (int)l_version.StartPeriod;
      l_sourcedComputeRequest.NbPeriods = l_version.NbPeriod;
      l_sourcedComputeRequest.GlobalFactVersionId = l_version.GlobalFactVersionId;
      l_sourcedComputeRequest.RateVersionId = l_version.RateVersionId;
      l_sourcedComputeRequest.ClientId = ClientId;
      l_sourcedComputeRequest.ProductId = ProductId;
      l_sourcedComputeRequest.AdjustmentId = AdjustmentId;
      List<Fact> l_factsList = new List<Fact>();
      foreach (EditedFinancialFact l_editedFact in EditedFacts.Values)
        if (l_editedFact.EditedValue != 0)
        {
          Fact l_tmp = l_editedFact.Clone();
          l_tmp.Value = l_editedFact.EditedValue;
          l_factsList.Add(l_tmp);
        }
      l_sourcedComputeRequest.FactList = l_factsList;
      l_sourcedComputeRequest.Process = Account.AccountProcess.FINANCIAL;

      List<UInt32> l_entitiesList = new List<UInt32>();
      foreach (AxisElem l_entity in m_dimensions.Entities.m_values.Values)
      {
        EntityCurrency l_currency = EntityCurrencyModel.Instance.GetValue(l_entity.Id);

        if (l_currency == null)
          l_sourcedComputeRequest.CurrencyId = Properties.Settings.Default.currentCurrency;
        else
          l_sourcedComputeRequest.CurrencyId = l_currency.CurrencyId;
        l_entitiesList.Add(l_entity.Id);
      }
      l_sourcedComputeRequest.EntityList = l_entitiesList;
      m_nbRequest++;
      m_nbCompute++;
      AddinModuleController.SetExcelInteractionState(false);
      if (SourcedComputeModel.Instance.Compute(l_sourcedComputeRequest, RequestIdList) == false)
        OnComputeFailed();
      else
      {
        UInt32 l_computeId = ++m_computeId;
        Task.Delay(5000).ContinueWith(t =>
        {
          if (l_computeId == m_computeId && m_nbCompute > 0)
            OnComputeFailed();
        });
      }
    }

    void OnComputeFailed()
    {
      RaiseFactDownloaded(false);
      m_nbRequest--;
      m_nbCompute--;
      AddinModuleController.SetExcelInteractionState(m_nbRequest <= 0);
      if (ComputeFailed != null)
        ComputeFailed();
    }

    private void OnFinancialOutputsComputed(ErrorMessage p_status, SourcedComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result)
    {
      try
      {
        m_nbRequest--;
        m_nbCompute--;
        if (ExcelUtils.IsWorksheetOpened(m_worksheet) == false)
          return;
        if (p_status == ErrorMessage.SUCCESS)
        {
          Version l_version = VersionModel.Instance.GetValue(m_versionId);

          if (l_version != null)
            foreach (ComputeResult l_result in p_result.Values)
            {
              Int32 l_count = 0;
              int l_nbValues = l_result.Values.Count;

              foreach (KeyValuePair<ResultKey, double> l_valuePair in l_result.Values) // value first set to correct value but reset to 0
              {
                l_count++;
                if (l_valuePair.Key.PeriodType != l_version.TimeConfiguration)
                  continue;
                DimensionKey l_key =
                  new DimensionKey(l_valuePair.Key.EntityId, l_valuePair.Key.AccountId, (UInt32)AxisType.Employee, l_valuePair.Key.Period);
                EditedFinancialFact l_fact = OutputFacts[l_key];

                if (l_fact == null)
                  continue;
                l_fact.Value = l_valuePair.Value;
                l_fact.EditedValue = l_valuePair.Value;
                if ((Double.IsNaN(l_valuePair.Value)))
                  l_fact.Cell.Value2 = "-";
                else if (Double.IsNegativeInfinity(l_valuePair.Value))
                  l_fact.Cell.Value2 = "-inf.";
                else if (Double.IsPositiveInfinity(l_valuePair.Value))
                  l_fact.Cell.Value2 = "+inf.";
                else
                  l_fact.Cell.Value = l_valuePair.Value;
                if ((l_count % 100) == 0 && ComputeProgress != null)
                  ComputeProgress((int)((l_count / (double)l_nbValues) * 100));
              }
            }
          if (ComputeProgress != null)
            ComputeProgress(100);
          RaiseFactDownloaded(true);
        }
        else
          RaiseFactDownloaded(false);
        AddinModuleController.SetExcelInteractionState(m_nbRequest <= 0);
      }
      catch (Exception e)
      {
        System.Diagnostics.Debug.WriteLine("FinancialEditedFactsModel.OnFinancialOutputsComputed: " + e.Message);
      }
    }

    public override void Refresh()
    {
      if (m_needRefresh)
        ComputeOutputs();
      m_needRefresh = false;
    }

    public override EditedFactBase UpdateEditedValueAndTag(Range p_cell)
    {
      if (EditedFacts.ContainsKey(p_cell.Address))
      {
        EditedFinancialFact l_fact = EditedFacts[p_cell.Address];

        if (p_cell.Value2 != null && (p_cell.Value2.GetType() != typeof(double) || (double)p_cell.Value2 == l_fact.EditedValue))
          return (null);

        if (p_cell.Value2 == null)
          l_fact.EditedValue = 0;
        else
          l_fact.EditedValue = (double)p_cell.Value2;
        
        m_needRefresh = true;
        return l_fact;
      }
      return null;
    }

    public override void Commit()
    {
      SafeDictionary<CRUDAction, SafeDictionary<string, Fact>> l_dic = new SafeDictionary<CRUDAction, SafeDictionary<string, Fact>>();

      l_dic[CRUDAction.UPDATE] = new SafeDictionary<string, Fact>();
      l_dic[CRUDAction.DELETE] = new SafeDictionary<string, Fact>();
      foreach (EditedFinancialFact l_editedFact in EditedFacts.Values)
      {
        if (l_editedFact.Value == l_editedFact.EditedValue)
          continue;
        l_editedFact.Value = l_editedFact.EditedValue;
        CRUDAction l_action = ((l_editedFact.Value == 0) ? CRUDAction.DELETE : CRUDAction.UPDATE);
        l_dic[l_action][l_editedFact.Cell.Address] = l_editedFact;
      }

      m_nbRequest++;
      if (l_dic[CRUDAction.UPDATE].Count != 0 || l_dic[CRUDAction.DELETE].Count != 0)
        AddinModuleController.SetExcelInteractionState(false);
      if (l_dic[CRUDAction.UPDATE].Count > 0)
        FactsModel.Instance.UpdateList(l_dic[CRUDAction.UPDATE], CRUDAction.UPDATE);
      if (l_dic[CRUDAction.DELETE].Count > 0)
        FactsModel.Instance.UpdateList(l_dic[CRUDAction.DELETE], CRUDAction.DELETE);
    }

    public override double? CellBelongToOutput(Range p_cell)
    {
      EditedFinancialFact l_fact = OutputFacts[p_cell.Address];

      if (l_fact == null)
        return (null);
      return (l_fact.Value);
    }

    public override double? CellBelongToInput(Range p_cell)
    {
      EditedFinancialFact l_fact = EditedFacts[p_cell.Address];

      if (l_fact == null)
        return (null);
      return (l_fact.EditedValue);
    }
  }
}
