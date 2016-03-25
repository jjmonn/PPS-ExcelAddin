using System;
using System.Windows.Forms;
using AddinExpress.MSO;

namespace FBI
{
  using FBI.MVC.Controller;
  using Microsoft.Office.Interop.Excel;

  /// <summary>
  /// Add-in Express Excel Worksheet Events Class
  /// </summary>
  public class ExcelWorksheetEvents : AddinExpress.MSO.ADXExcelWorksheetEvents
  {
    IFactEditionController m_factsEditionController;
    private UInt16 MAX_NB_ROWS = 16384;

    public ExcelWorksheetEvents(AddinExpress.MSO.ADXAddinModule module)
      : base(module)
    {
    }

    public void SetController(IFactEditionController p_factsEditionController)
    {
      m_factsEditionController = p_factsEditionController;
    }

    public override void ProcessChange(object target)
    {
      try
      {
        Range l_range = target as Range;
        if (l_range != null && m_factsEditionController != null)
        {
          if (l_range.Count > MAX_NB_ROWS)
            return;
          foreach (Range l_cell in l_range.Cells)
            m_factsEditionController.RaiseWorksheetChangingEvent(l_cell);
        }
        m_factsEditionController.RaiseWorksheetChangedEvent();
      }
      catch(Exception e)
      {
        System.Diagnostics.Debug.WriteLine("Worksheet change event error: " + e.Message);
      }
    }


    public override void ProcessBeforeRightClick(object target, AddinExpress.MSO.ADXCancelEventArgs e)
    {
    }

    public override void ProcessSelectionChange(object target)
    {
    }

    #region Unused events

    public override void ProcessBeforeDoubleClick(object target, int elementID, int arg1, int arg2, AddinExpress.MSO.ADXCancelEventArgs e)
    {
      // TODO: Add some code
    }

    public override void ProcessActivate()
    {
      // TODO: Add some code
    }

    public override void ProcessDeactivate()
    {
      // TODO: Add some code
    }

    public override void ProcessCalculate()
    {
      // TODO: Add some code
    }

  
    public override void ProcessFollowHyperlink(object target)
    {
      // TODO: Add some code
    }

    public override void ProcessPivotTableUpdate(object target)
    {
      // TODO: Add some code
    }

    public override void ProcessResize()
    {
      // TODO: Add some code
    }

    public override void ProcessMouseDown(int button, int shift, int x, int y)
    {
      // TODO: Add some code
    }

    public override void ProcessMouseUp(int button, int shift, int x, int y)
    {
      // TODO: Add some code
    }

    public override void ProcessMouseMove(int button, int shift, int x, int y)
    {
      // TODO: Add some code
    }

    public override void ProcessDragPlot()
    {
      // TODO: Add some code
    }

    public override void ProcessDragOver()
    {
      // TODO: Add some code
    }

    public override void ProcessSelect(int elementID, int arg1, int arg2)
    {
      // TODO: Add some code
    }

    public override void ProcessSeriesChange(int seriesIndex, int pointIndex)
    {
      // TODO: Add some code
    }

    public override void ProcessPivotTableAfterValueChange(object targetPivotTable, object targetRange)
    {
      // TODO: Add some code
    }

    public override void ProcessPivotTableBeforeAllocateChanges(object targetPivotTable, int valueChangeStart, int valueChangeEnd, AddinExpress.MSO.ADXCancelEventArgs e)
    {
      // TODO: Add some code
    }

    public override void ProcessPivotTableBeforeCommitChanges(object targetPivotTable, int valueChangeStart, int valueChangeEnd, AddinExpress.MSO.ADXCancelEventArgs e)
    {
      // TODO: Add some code
    }

    public override void ProcessPivotTableBeforeDiscardChanges(object targetPivotTable, int valueChangeStart, int valueChangeEnd)
    {
      // TODO: Add some code
    }

    public override void ProcessPivotTableChangeSync(object target)
    {
      // TODO: Add some code
    }

    #endregion

  }
}


