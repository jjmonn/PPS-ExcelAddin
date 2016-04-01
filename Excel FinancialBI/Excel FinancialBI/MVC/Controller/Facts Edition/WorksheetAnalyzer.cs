using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Microsoft.Office.Interop.Excel;
  using Utils;
  using FBI.MVC.Model;
  using FBI.MVC.Controller;
  using FBI.MVC.Model.CRUD;
  using System.Reflection;
  using System.ComponentModel;
  using FBI.MVC.View;

  class WorksheetAnalyzer : IProgressBarController
  {
    Range m_range;
    WorksheetAreaController m_dimensions;
    public string Error { get; set; }
    public IView View { get{ return m_progressBarView;} }
    Range m_lastCell;
    ProgressBarView m_progressBarView;
    BackgroundWorker m_snapshotBackgroundWorker;

    ~WorksheetAnalyzer()
    {
      m_range = null;
    }

    public bool WorksheetScreenshot(Range p_range)
    {
      m_range = p_range;
      m_progressBarView = new ProgressBarView(false);
      m_progressBarView.SetController(this);
      m_lastCell = GetRealLastCell(m_range);
      if (m_lastCell != null)
        return (true);
      else
      {
        Error = Local.GetValue("upload.msg_empty_worksheet");
        return (false);
      }
    }

    public void Snapshot(WorksheetAreaController p_dimensions, List<UInt32> p_periodsList = null)
    {
      m_dimensions = p_dimensions;

      //m_progressBarView.SetupProgressBar(m_lastCell.Row);
      //m_progressBarView.Show();

      //m_snapshotBackgroundWorker = new BackgroundWorker();
      //m_snapshotBackgroundWorker.DoWork += SnapshotBackgroundWorker_DoWork;
      //m_snapshotBackgroundWorker.RunWorkerCompleted += SnapshotBackgroundWorker_RunWorkerCompleted;
      //m_snapshotBackgroundWorker.ProgressChanged += SnapshotBackgroundWorker_ReportProgress;
      //m_snapshotBackgroundWorker.WorkerReportsProgress = true;
      //m_snapshotBackgroundWorker.WorkerSupportsCancellation = true;
      //m_snapshotBackgroundWorker.RunWorkerAsync();

      SnapshotBackgroundWorker_DoWork();
    }

    #region Snapshot backgroundworker

    private void SnapshotBackgroundWorker_DoWork()//object sender, DoWorkEventArgs e)
    {
      Range l_cell;
      for (UInt32 l_rowIndex = 1; l_rowIndex <= m_lastCell.Row; l_rowIndex++)
      {
        for (UInt32 l_columnIndex = 1; l_columnIndex <= m_lastCell.Column; l_columnIndex++)
        {
          l_cell = m_range.Cells[l_rowIndex, l_columnIndex] as Range;
          if (l_cell == null)
          {
            System.Diagnostics.Debug.WriteLine("Dataset: Snapshot method: DimensionsIdentificationProcess > error in cell identication process: address : ");
            continue;
          }

          if (Convert.ToBoolean(l_cell.EntireRow.Hidden) == true || Convert.ToBoolean(l_cell.EntireColumn.Hidden) == true)
            continue;

          if (l_cell.Value == null)
            continue;

          if (l_cell.Value.GetType() == typeof(DateTime))
            m_dimensions.RegisterPeriod(l_cell);
          else
          {
            if (l_cell.Value2.GetType() == typeof(string))
              m_dimensions.DimensionsIdentify(l_cell);
          }
        }
     //   m_snapshotBackgroundWorker.ReportProgress(1);
      }
    }

    private delegate void ReportProgress_Delegate(object sender, ProgressChangedEventArgs e);
    private void SnapshotBackgroundWorker_ReportProgress(object sender, ProgressChangedEventArgs e)
    {
       if (m_progressBarView.InvokeRequired)
      {
        ReportProgress_Delegate MyDelegate = new ReportProgress_Delegate(SnapshotBackgroundWorker_ReportProgress);
        m_progressBarView.Invoke(MyDelegate, new object[] { sender, e});
      }
      else
        m_progressBarView.AddProgress(1);
    }

    private delegate void RunWorkerCompleted_Delegate(object sender, RunWorkerCompletedEventArgs e);
    private void SnapshotBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (m_progressBarView.InvokeRequired)
      {
        RunWorkerCompleted_Delegate MyDelegate = new RunWorkerCompleted_Delegate(SnapshotBackgroundWorker_RunWorkerCompleted);
        m_progressBarView.Invoke(MyDelegate, new object[] { sender, e });
      }
      else
      {
        m_progressBarView.SetToMaxProgress();
        m_progressBarView.Hide();
      }
    }

    public void Cancel()
    {
      m_snapshotBackgroundWorker.CancelAsync();

    }

    #endregion

    private Range GetRangeFromRowAndColumn(Int32 p_rowIndex, Int32 p_columnIndex)
    {
      return (Range)m_range.Cells[p_rowIndex, p_columnIndex];
    }

    static public Range GetRealLastCell(Range p_range)
    {
      long lRealLastRow = 0;
      long lRealLastColumn = 0;
      try
      {
        p_range.Cells.Find("value", Type.Missing, XlFindLookIn.xlValues, XlLookAt.xlPart, XlSearchOrder.xlByRows, XlSearchDirection.xlNext, false, false, false);

        // Find the last real row
        lRealLastRow = p_range.Cells.Find("*", System.Reflection.Missing.Value,
        Missing.Value, Missing.Value, XlSearchOrder.xlByRows, XlSearchDirection.xlPrevious, false, Missing.Value, Missing.Value).Row;

        // Find the last real column
        lRealLastColumn = p_range.Cells.Find("*", System.Reflection.Missing.Value,
        Missing.Value, System.Reflection.Missing.Value, XlSearchOrder.xlByColumns, XlSearchDirection.xlPrevious, false, Missing.Value, Missing.Value).Column;

        //lRealLastRow = p_range.Cells.Find("*", p_range.Cells[1, 1], Type.Missing, XlFindLookIn.xlValues,XlLookAt.xlWhole, XlSearchOrder.xlByRows, XlSearchDirection.xlPrevious).Row;
        //lRealLastColumn = p_range.Cells.Find("*", p_range.Cells[1, 1], null, null, XlSearchOrder.xlByColumns, XlSearchDirection.xlPrevious).Column;
        return p_range.Cells[lRealLastRow, lRealLastColumn] as Range;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine("Get real last cell error: " + ex.Message);
        return null;
      }
    }

  }
}
