using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.View
{
  using FBI.MVC.Controller;
  using FBI.MVC.Model;
  using Microsoft.Office.Interop.Excel;
  using System.Drawing;
  using Utils;

  class RangeHighlighter
  {
    FactsEditionController m_factsEditionController;
    Int32 DIMENSIONS_COLORS = Color.FromArgb(215, 239, 253).ToArgb();
    SafeDictionary<Range, Object> m_originalCellsColor = new SafeDictionary<Range, Object>();

    public RangeHighlighter(FactsEditionController p_factsEditionController)
    {
      m_factsEditionController = p_factsEditionController;
    }

    public void FillCellColor(Range p_cell, EditedFactStatus p_status)
    {
      if (p_cell == null)
        return;

      switch (p_status)
      {
        case EditedFactStatus.InputDifferent:
          FillInputCellRed(p_cell);
          break;

        case EditedFactStatus.OutputDifferent :
          FillOutputCellRed(p_cell);
          break;

        case EditedFactStatus.InputEqual :
          FillInputsBaseColor(p_cell);
          break;

        case EditedFactStatus.FactTagEqual :
          FillInputsBaseColor(p_cell);
          break;

        case EditedFactStatus.FactTagDifferent :
          FillInputCellRed(p_cell);
          break;

        case EditedFactStatus.LegalHolidayEqual:
          FillInputsBaseColor(p_cell);
          break;

        case EditedFactStatus.LegalHolidayDifferent:
          FillInputCellRed(p_cell);
          break;

        case EditedFactStatus.Committed:
          FillCellGreen(p_cell);
          break;

      }
    }

    public void FillDimensionColor(Range p_cell)
    {
      if (p_cell == null)
        return;
      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Properties.Settings.Default.FactsEditionDimensionsFill;
    }

    public void FillInputsBaseColor(Range p_cell)
    {
      if (p_cell == null)
        return;
      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Properties.Settings.Default.FactsEditionInputsFillColor;
    }

    public void FillInputCellRed(Range p_cell)
    {
      if (p_cell == null)
        return;

      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Properties.Settings.Default.FactsEditionInputsRedFill;
    }

    private void FillOutputCellRed(Range p_cell)
    {
      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Properties.Settings.Default.FactsEditionOutputsRedFill;
    }

    // equal input vs equal output
    private void ClearFillColor(Range p_cell)
    {
      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Color.Transparent;
    }

    public void FillCellGreen(Range p_cell)
    {
      if (p_cell == null)
        return;

      RegisterCellOriginalFill(p_cell);
      p_cell.Interior.Color = Properties.Settings.Default.FactsEditionInputCommitedFill;
    }

    private void RegisterCellOriginalFill(Range p_cell)
    {
      if (!m_originalCellsColor.ContainsKey(p_cell)) // only add if color has not been yet changed
        m_originalCellsColor.Add(p_cell, p_cell.Interior.Color); 
    }

    public void RevertToOriginalColors()
    {
      foreach (KeyValuePair<Range, Object> l_keyPair in m_originalCellsColor)
      {
        try
        {
          l_keyPair.Key.Interior.Color = l_keyPair.Value;
        }
        catch (Exception e)
        {
          System.Diagnostics.Debug.WriteLine("revert to original color: " + e.Message);
        }
      }
      m_originalCellsColor.Clear();
    }

  }
}
