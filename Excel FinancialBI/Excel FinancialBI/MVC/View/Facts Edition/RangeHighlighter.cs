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

  class RangeHighlighter
  {
    FactsEditionController m_factsEditionController;
    Color INPUT_RED = Color.Red;
    Color OUTPUT_RED = Color.LightSalmon;
    Color SUCCESS_COMMIT = Color.Green;
    SafeDictionary<Range, Color> m_originalCellsColor = new SafeDictionary<Range, Color>();

    public RangeHighlighter(FactsEditionController p_factsEditionController)
    {
      m_factsEditionController = p_factsEditionController;
    }

    public void FillCellColor(Range p_cell, CellStatus p_status)
    {
      switch (p_status)
      {
        case CellStatus.DifferentInput:
          FillInputCellRed(p_cell);
          break;

        case CellStatus.DifferentOutput :
          FillOutputCellRed(p_cell);
          break;

        case CellStatus.Equal :
          ClearFillColor(p_cell);
          break;
      }
    }

    private void FillInputCellRed(Range p_cell)
    {
      p_cell.Interior.Color = INPUT_RED;
      RegisterCellOriginalFill(p_cell);
    }

    private void FillOutputCellRed(Range p_cell)
    {
      p_cell.Interior.Color = OUTPUT_RED;
      RegisterCellOriginalFill(p_cell);
    }

    private void ClearFillColor(Range p_cell)
    {
      p_cell.Interior.Color = Color.Transparent;
      RegisterCellOriginalFill(p_cell);
    }

    public void FillCellGreen(Range p_cell)
    {
      p_cell.Interior.Color = SUCCESS_COMMIT;
      RegisterCellOriginalFill(p_cell);
    }

    private void RegisterCellOriginalFill(Range p_cell)
    {
      if (!m_originalCellsColor.ContainsKey(p_cell)) // only add if color has not been yet changed
      {
        m_originalCellsColor.Add(p_cell, (Color)p_cell.Interior.Color); 
      }
    }

    public void RevertToOriginalColors()
    {
      foreach (KeyValuePair<Range, Color> l_keyPair in m_originalCellsColor)
      {
        l_keyPair.Key.Interior.Color = l_keyPair.Value;
      }
      m_originalCellsColor.Clear();
    }

  }
}
