using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.MVC.Model.CRUD;
  using Microsoft.Office.Interop.Excel;

  public delegate void OnFactsDownloaded(bool p_success);

  interface IEditedFactsManager
  {
   
    event OnFactsDownloaded FactsDownloaded;

    void RegisterEditedFacts(Dimensions p_dimensions);

     void DownloadFacts(Version p_version, List<UInt32> p_periodList);

     void IdentifyDifferences();

     void UpdateWorksheetInputs();

     void CommitDifferences();


  }
}
