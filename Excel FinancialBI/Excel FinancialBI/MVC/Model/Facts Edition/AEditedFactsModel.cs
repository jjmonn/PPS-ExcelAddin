﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.MVC.Model.CRUD;
  using Network;
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.View;

  public delegate void OnFactsDownloaded(bool p_success);
  public delegate void FactsCommitError(string p_address, ErrorMessage p_error);

  abstract public class AEditedFactsModel
  {
    public List<int> RequestIdList { get; private set; }
    public event OnFactsDownloaded FactsDownloaded;
    protected Worksheet m_worksheet;
    public event FactsCommitError OnCommitError;

    protected AEditedFactsModel(Worksheet p_worksheet)
    {
      m_worksheet = p_worksheet;
      RequestIdList = new List<int>();
    }

    public abstract void Close();

    public abstract void RegisterEditedFacts(WorksheetAreaController p_dimensions, UInt32 p_versionId, bool p_displayInitialDifferences, UInt32 p_RHAccountId = 0);

    public abstract void DownloadFacts(List<Int32> p_periodList, bool p_updateCells, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId);

    public abstract EditedFactBase UpdateEditedValueAndTag(Range p_cell);

    public abstract void Refresh();

    public abstract double? CellBelongToOutput(Range p_cell);
    
    public abstract void Commit();

    public void RaiseFactDownloaded(bool p_success)
    {
      if (FactsDownloaded != null)
        FactsDownloaded(p_success);
    }

    public void RaiseOnCommitError(string p_address, ErrorMessage p_error)
    {
      if (OnCommitError != null)
        OnCommitError(p_address, p_error);
    }
  }
}
