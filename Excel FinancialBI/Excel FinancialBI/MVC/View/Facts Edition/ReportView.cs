
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.View
{
  using Controller;
  using Network;
  using Model.CRUD;

  class ReportView : AResultView<ReportController>
  {
    public ReportView()
    {
    }

    protected override void SuscribeEvents()
    {
      m_controller.ComputeCompleteEvent += OnComputeCompleteEvent;
    }

    void OnComputeCompleteEvent(ErrorMessage p_status, SourcedComputeRequest p_request, SafeDictionary<uint, ComputeResult> p_result)
    {
      FillDGV(p_result, true);
    }
  }
}
