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
    bool Launch(bool p_updateCells, bool p_displayInitialDifferences, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId);
    void Close();
    void CommitFacts();
  }

  abstract class AFactEditionController<TModel> : IFactEditionController where TModel : AEditedFactsModel
  {
    public string Error { get; set; }
    public AddinModuleController AddinController { get; private set; }
    public TModel EditedFactModel { get; protected set; }
    public Account.AccountProcess Process { get; protected set; }
    public WorksheetAreaController AreaController { get; private set; }
    private UInt32 m_versionId;
    public abstract IFactEditionView View { get; }
    private WorksheetAnalyzer m_worksheetAnalyzer = new WorksheetAnalyzer();
    public List<Int32> m_periodsList;
    Worksheet m_worksheet;
    public delegate void OnWorksheetChangingHandler(Range p_cell);
    public event OnWorksheetChangingHandler WorksheetChanging;
    public delegate void OnWorksheetChanged();
    public event OnWorksheetChanged WorksheetChanged;

    protected UInt32 m_RHAccountId = 0;

    public AFactEditionController(AddinModuleController p_addinModuleController, Account.AccountProcess p_process, UInt32 p_versionId, Worksheet p_worksheet, List<Int32> p_periodsList = null)
    {
      m_worksheet = p_worksheet;
      AddinController = p_addinModuleController;
      m_versionId = p_versionId;
      AreaController = new WorksheetAreaController(p_process, m_versionId, p_worksheet, p_periodsList);
      Process = p_process;
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
      EditedFactModel.DownloadFacts(m_periodsList, p_updateCells, p_clientId, p_productId, p_adjustmentId);
    }

    public bool Launch(bool p_updateCells, bool p_displayInitialDifferences, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId)
    {
      if (m_worksheetAnalyzer.WorksheetScreenshot(m_worksheet.Cells) == true)
      {
        m_worksheetAnalyzer.Snapshot(AreaController);
        AreaController.DefineOrientation(Process);
        if (AreaController.IsValid() == false)
        {
          Error = Local.GetValue("upload.msg_error_upload");
          return false;
        }
        EditedFactModel.RegisterEditedFacts(AreaController, m_worksheet, m_versionId, p_displayInitialDifferences, m_RHAccountId);
        if (m_versionId != 0 && (m_periodsList == null || m_periodsList.Count > 0))
        {
          View.OpenFactsEdition(p_updateCells, p_clientId, p_productId, p_adjustmentId);
          return true;
        }
        else
        {
          Error = Local.GetValue("upload.msg_error_upload");
          return false;
        }
      }
      Error = m_worksheetAnalyzer.Error;
      return false;
    }

    public void CommitFacts()
    {
      EditedFactModel.Commit();
    }

    public void Close()
    {
      View.Close();
      EditedFactModel.FactsDownloaded -= OnFactsDownloaded;
      EditedFactModel.UnsubsribeEvents();
      AreaController.ClearDimensions();
    }

    private void OnCommitError(string p_address, ErrorMessage p_error)
    {
       // TO DO
      // write in commit log
    }

     #region Model callbacks

    protected void OnFactsDownloaded(bool p_success)
    {
      if (p_success == true)
      {
        AddinController.AssociateExcelWorksheetEvents(m_worksheet);
        EditedFactModel.FactsDownloaded -= OnFactsDownloaded;
      }
      else
      {
        // exit mode
      }
    }

    #endregion
   
    // Commit failed as whole method -> view message box

  }
}
