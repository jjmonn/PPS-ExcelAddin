using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{
  using Model;
  using Model.CRUD;
  using View;
  using Network;

  class FinancialFactEditionController : AFactEditionController<FinancialEditedFactsModel>
  {
    FinancialFactEditionView m_view;
    ReportUploadAccountInfoSidePane m_accountSP;

    public override IFactEditionView View { get { return (m_view); } }

    public FinancialFactEditionController(AddinModuleController p_addinController, UInt32 p_versionId, Worksheet p_worksheet) :
      base(p_addinController, Account.AccountProcess.FINANCIAL, p_versionId, p_worksheet)
    {
      EditedFactModel = new FinancialEditedFactsModel(p_worksheet);
      m_view = new FinancialFactEditionView(this, p_worksheet);
      m_view.LoadView();
      m_accountSP = AddinModule.CurrentInstance.ReportUploadAccountInfoSidePane;
      m_statusView = new StatusReportInterfaceUI(EditedFactModel);
      FactsModel.Instance.UpdateEvent += OnCommitResult;
      EditedFactModel.ComputeCompleteEvent += OnComputeCompleteEvent;
    }

    void OnComputeCompleteEvent(ErrorMessage p_status, SourcedComputeRequest p_request, SafeDictionary<uint, ComputeResult> p_result)
    {
      RaiseComputeCompleteEvent(p_status, p_request, p_result);
    }

    public override void Close()
    {
      base.Close();
      FactsModel.Instance.UpdateEvent -= OnCommitResult;
      m_accountSP.Hide();
    }

    public void DisplayAccountSP(Account p_account)
    {
      m_accountSP.SelectAccount(p_account);
      m_accountSP.Visible = true;
      m_accountSP.Show();
    }

    private void OnCommitResult(ErrorMessage p_status, CRUDAction p_action, SafeDictionary<string, Tuple<UInt32, ErrorMessage>> p_resultDic)
    {
      if (p_status == ErrorMessage.SUCCESS)
      {
        foreach (KeyValuePair<string, Tuple<UInt32, ErrorMessage>> l_pair in p_resultDic)
          if (l_pair.Value.Item2 != ErrorMessage.SUCCESS)
            EditedFactModel.RaiseOnCommitError(l_pair.Key, l_pair.Value.Item2);
      }
    }
  }
}
