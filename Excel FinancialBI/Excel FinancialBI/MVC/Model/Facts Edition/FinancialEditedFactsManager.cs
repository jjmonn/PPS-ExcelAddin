using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Model.CRUD;
  using Network;
  using Utils;



  class FinancialEditedFactsManager : IEditedFactsManager
  {
    MultiIndexDictionary<Range, Tuple<AxisElem, Account, UInt32>, EditedFact> m_editedFacts = new MultiIndexDictionary<Range, Tuple<AxisElem, Account, UInt32>, EditedFact>();
    MultiIndexDictionary<Range, Tuple<AxisElem, Account, UInt32>, OutputFact> m_outputFacts = new MultiIndexDictionary<Range, Tuple<AxisElem, Account, UInt32>, OutputFact>();
    public event OnFactsDownloaded FactsDownloaded;
    List<EditedFact> m_factsToBeCommitted = new List<EditedFact>(); // ? to be confirmed

    public void RegisterEditedFacts()
    {

      // TO DO create and Fill the EditedFacts, RHEditedFacts and OutputsFacts
      // based on global orientation and dimensions



      // Once facts registered clean dimensions and put them into a safe dictionary ?
    }

    public void DownloadFacts(Version p_version, List<UInt32> p_periodsList)
    {



    }

    private void AfterFinancialInputDownloaded(bool p_success)
    {
      // TO DO

      // TO DO : request SOURCED COMPUTE
    }

    private void AfterFinancialOutputsComputed(bool p_success)
    {
      // TO DO : if success save values into facts

      FactsDownloaded(true);
    }

    private bool FillEditedFacts(List<Fact> p_factsList)
    {
    
      return true;
    }

    public void IdentifyDifferences()
    {
      foreach (EditedFact l_editedFact in m_editedFacts.Values)
      {
        l_editedFact.IsDifferent();
      }
    }

    public void UpdateWorksheetInputs()
    {
      foreach (EditedFact l_editedFact in m_editedFacts.Values)
      {
        l_editedFact.UpdateCellValue();
      }
    }

    public void UpdateWorkSheetOutputs()
    {
      foreach (OutputFact l_outputFact in m_editedFacts.Values)
      {
        l_outputFact.UpdateCellValue();
      }
    }

    public void CommitDifferences()
    {
      // TO DO
      // Loop through facts : if to be commited  add to update list
      // Send EditedFacts to the model  

      // associate updates to cells
      // ready to listen server answer
      //   -> update cells color on worksheet if success
    }








  }
}
