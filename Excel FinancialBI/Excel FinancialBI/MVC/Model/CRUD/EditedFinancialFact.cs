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
      Value = p_fact.Value;
      EditedValue = p_fact.Value;
      ClientId = p_fact.ClientId;
    }

    public override EditedFactStatus SetFactValueStatus()
    {
      if (Account.FormulaType == CRUD.Account.FormulaTypes.HARD_VALUE_INPUT || Account.FormulaType == CRUD.Account.FormulaTypes.FIRST_PERIOD_INPUT)
      {
        if (EditedValue != this.Value)
          return EditedFactStatus.InputDifferent;
        else
          return EditedFactStatus.InputEqual;
      }
      else
      {
        if (EditedValue != this.Value)
          return EditedFactStatus.OutputDifferent;
        else
          return EditedFactStatus.OutputEqual;
      }
    }


  }
}
