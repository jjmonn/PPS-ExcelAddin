using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  using Microsoft.Office.Interop.Excel;


  class EditedFinancialFact : EditedFactBase
  {
    public double EditedValue { get; set; }

    public EditedFinancialFact(UInt32 p_accountId, UInt32 p_entityId, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId,
                               UInt32 p_employeeId, UInt32 p_versionId, PeriodDimension p_period, Range p_cell)
      : base(p_accountId, p_entityId, p_clientId, p_productId, p_adjustmentId, p_employeeId, p_versionId,
             p_period, p_cell)
    {
      try
      {
        EditedValue = Convert.ToDouble(Cell.Value2);
      }
      catch
      {
        EditedValue = 0;
      }
    }

    public void UpdateFinancialFact(Fact p_fact)
    {
      UpdateFactBase(p_fact);

      ClientId = p_fact.ClientId;
      SetFactValueStatus();
    }

    public void SetFactValueStatus()
    {
      if (Account.FormulaType == CRUD.Account.FormulaTypes.HARD_VALUE_INPUT || Account.FormulaType == CRUD.Account.FormulaTypes.FIRST_PERIOD_INPUT)
      {
        if (EditedValue != this.Value)
          SetFactStatus(EditedFactStatus.DifferentInput);
        else
          SetFactStatus(EditedFactStatus.InputEqual);
      }
      else
      {
        if (EditedValue != this.Value)
          SetFactStatus(EditedFactStatus.DifferentOutput);
        else
          SetFactStatus(EditedFactStatus.OutputEqual);
      }
    }


    //private double GetFactValue() 
    //{
    //  // below : goes into controller
    //  if (Cell.Value2 == null)
    //    return 0;
    //  double l_doubleValue;
    //  if (Cell.Value2.GetType() == typeof(string))
    //  {
    //    if (Double.TryParse(Cell.Value2 as string, out l_doubleValue))
    //      return l_doubleValue;
    //    else return 0;
    //  }
    //  return 0;
    //}

  }
}
