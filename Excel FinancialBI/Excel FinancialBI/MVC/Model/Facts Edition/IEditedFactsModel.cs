using System;
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
  
  interface IEditedFactsModel
  {
    event OnFactsDownloaded FactsDownloaded;

    void UnsubsribeEvents();

    void RegisterEditedFacts(WorksheetAreaController p_dimensions, Worksheet p_worksheet, UInt32 p_versionId, RangeHighlighter p_rangeHighlighter, bool p_displayInitialDifferences, UInt32 p_RHAccountId = 0);

    void DownloadFacts(List<Int32> p_periodList, bool p_updateCells);

    bool UpdateEditedValueAndTag(Range p_cell);
    
    //void UpdateWorksheetInputs();

    void Commit();

  }
}
