using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using View;
  using Model.CRUD;
  using Model;
  using Network;

  class ReportController : AResultController<ReportView>
  {
    ReportViewContainer m_container;
    SafeDictionary<DimensionType, CUIDimensionConf> m_dims;
    Version m_version;
    public event SourcedComputeModel.ComputeCompleteEventHandler ComputeCompleteEvent;
    IFactEditionController m_parentController;

    public ReportController(Version p_version, IFactEditionController p_parentController)
    {
      m_version = p_version;
      m_container = new ReportViewContainer();
      m_container.Controls.Add(m_view);
      m_view.Dock = System.Windows.Forms.DockStyle.Fill;
      m_parentController = p_parentController;

      m_dims = new SafeDictionary<DimensionType, CUIDimensionConf>();
      m_dims[DimensionType.ACCOUNT] = new CUIDimensionConf(typeof(AccountModel));
      m_dims[DimensionType.EMPLOYEE] = new AxisElemConf(AxisType.Employee);
      m_dims[DimensionType.ENTITY] = new AxisElemConf(AxisType.Entities);
      m_dims[DimensionType.PERIOD] = new PeriodConf(p_version.TimeConfiguration);

      m_parentController.ComputeCompleteEvent += OnComputeCompleteEvent;
    }

    void OnComputeCompleteEvent(ErrorMessage p_status, SourcedComputeRequest p_request, SafeDictionary<uint, ComputeResult> p_result)
    {
      if (ComputeCompleteEvent != null)
        ComputeCompleteEvent(p_status, p_request, p_result);
    }

    public void ShowView(DimensionType p_column, DimensionType p_row, DimensionType p_thirdDim,
      SourcedComputeRequest p_request, SafeDictionary<UInt32, ComputeResult> p_result)
    {
      ComputeConfig l_config = new ComputeConfig();
      LegacyComputeRequest l_request = new LegacyComputeRequest();

      l_request.AccountList = p_request.AccountList;
      l_request.CurrencyId = p_request.CurrencyId;
      l_request.GlobalFactVersionId = p_request.GlobalFactVersionId;
      l_request.NbPeriods = p_request.NbPeriods;
      l_request.StartPeriod = p_request.StartPeriod;
      l_request.RateVersionId = p_request.RateVersionId;
      l_request.Versions.Add(p_request.VersionId);
      l_request.Process = p_request.Process;

      if (p_thirdDim == DimensionType.ENTITY)
        l_request.EntityId = p_request.EntityList.FirstOrDefault();
      else
      {
        AxisElem l_topEntity = AxisElemModel.Instance.GetTopEntity();
        if (l_topEntity != null)
          l_request.EntityId = l_topEntity.Id;
      }

      l_config.Rows = m_dims[p_row];
      l_config.Columns = m_dims[p_column];
      l_config.Request = l_request;
      m_view.PrepareDgv(l_config);
      if (p_result != null)
        m_view.FillDGV(p_result, true);
      m_container.Show();
    }
  }
}
