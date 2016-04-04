using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace FBI.MVC.Controller
{
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;
  using FBI.MVC.View;
  using Utils;
  using Network;

  public interface IFactEditionController
  {
    void RaiseWorksheetChangingEvent(Range p_cell);
    void RaiseWorksheetChangedEvent();
    void RaiseWorksheetSelectionChangedEvent(Range p_range);
    bool Launch(bool p_updateCells, bool p_displayInitialDifferences, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId);
    void Close();
    void CommitFacts();
    void ShowStatusView();
    bool AutoCommit { get; set; }
  }

  abstract class AFactEditionController<TModel> : IFactEditionController where TModel : AEditedFactsModel
  {
    public string Error { get; set; }
    public AddinModuleController AddinController { get; private set; }
    public TModel EditedFactModel { get; protected set; }
    public Account.AccountProcess Process { get; protected set; }
    public UInt32 VersionId { get; set; }
    public abstract IFactEditionView View { get; }
    public List<Int32> PeriodsList { get; set; }
    public delegate void OnWorksheetChangingHandler(Range p_cell);
    public event OnWorksheetChangingHandler WorksheetChanging;
    public delegate void OnWorksheetChanged();
    public event OnWorksheetChanged WorksheetChanged;
    public delegate void OnWorksheetSelectionChangedHandler(Range p_range);
    public event OnWorksheetSelectionChangedHandler WorksheetSelectionChanged;
    public bool AutoCommit { get; set; }
    public UInt32 RHAccountId { get; set; }
    protected StatusReportInterfaceUI m_statusView;

    public AFactEditionController(AddinModuleController p_addinModuleController, Account.AccountProcess p_process, UInt32 p_versionId, Worksheet p_worksheet, List<Int32> p_periodsList = null)
    {
      AddinController = p_addinModuleController;
      VersionId = p_versionId;
      Process = p_process;
      RHAccountId = 0;
    }

    public void RaiseWorksheetSelectionChangedEvent(Range p_range)
    {
      if (WorksheetSelectionChanged != null)
        WorksheetSelectionChanged(p_range);
    }

    public void RaiseWorksheetChangingEvent(Range p_cell)
    {
      if (WorksheetChanging != null)
        WorksheetChanging(p_cell);
    }

    public void RaiseWorksheetChangedEvent()
    {
      if (WorksheetChanged != null)
        WorksheetChanged();
    }

    public void DownloadFacts(bool p_updateCells, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      AddinModuleController.SetExcelInteractionState(false);
      EditedFactModel.DownloadFacts(PeriodsList, p_updateCells, p_clientId, p_productId, p_adjustmentId);
    }

    public bool Launch(bool p_updateCells, bool p_displayInitialDifferences, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      Error = View.Launch(p_updateCells, p_displayInitialDifferences, p_clientId, p_productId, p_adjustmentId);
      return (Error == "");
    }

    public void CommitFacts()
    {
      EditedFactModel.Commit();
    }

    public virtual void Close()
    {
      View.Close();
      EditedFactModel.Close();
      if (m_statusView != null)
        m_statusView.Close();
    }

    public void ShowStatusView()
    {
      if (m_statusView != null)
        m_statusView.Show();
    }

    private void OnCommitError(string p_address, ErrorMessage p_error)
    {
       // TO DO
      // write in commit log
    }
  }
}
