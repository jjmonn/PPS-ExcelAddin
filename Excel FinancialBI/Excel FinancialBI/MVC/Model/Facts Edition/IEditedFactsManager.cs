using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.MVC.Model.CRUD;
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.View;

  public delegate void OnFactsDownloaded(bool p_success);

  interface IEditedFactsManager
  {
    bool m_autoCommit { set; get; }
    event OnFactsDownloaded FactsDownloaded;

    void RegisterEditedFacts(Dimensions p_dimensions, Worksheet p_worksheet, UInt32 p_versionId, RangeHighlighter p_rangeHighlighter, UInt32 p_RHAccountId = 0);

    void DownloadFacts(UInt32 p_versionId, List<Int32> p_periodList, bool p_updateCells);

    bool UpdateEditedValues(Range p_cell);
    
    //void UpdateWorksheetInputs();

    void CommitDifferences();

  }
}
