using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Model.CRUD;

  public enum EditedFactStatus
  {
    InputEqual = 0,
    OutputEqual,
    InputDifferent,
    OutputDifferent,
    FactTagEqual,
    FactTagDifferent,
    LegalHolidayEqual,
    LegalHolidayDifferent,
    Committed
  }

  public delegate void CellValueChangedEventHandler(Range p_cell, EditedFactStatus p_status);

  class EditedFactBase : Fact
  {
    public Range Cell { get; protected set; }
    public Account Account { get { return AccountModel.Instance.GetValue(this.AccountId); } }
    public AxisElem Entity { get { return AxisElemModel.Instance.GetValue(AxisType.Entities, this.EntityId); } }
    public AxisElem Employee { get { return AxisElemModel.Instance.GetValue(AxisType.Employee, this.EmployeeId); } }
    public PeriodDimension PeriodDimension { get; protected set; }

 //   public EditedFactStatus EditedFactStatus { get; protected set; }
    public event CellValueChangedEventHandler OnCellValueChanged;


    public EditedFactBase(UInt32 p_accountId, UInt32 p_entityId, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId, 
                          UInt32 p_employeeId, UInt32 p_versionId, PeriodDimension p_period, Range p_cell)
    {
      Cell = p_cell;
      PeriodDimension = p_period;
      this.AccountId = p_accountId;
      this.EntityId = p_entityId;
      this.ClientId = p_clientId;
      this.ProductId = p_productId;
      this.AdjustmentId = p_adjustmentId;
      this.EmployeeId = p_employeeId;
      this.Period = PeriodDimension.Id;
      this.VersionId = p_versionId;

  //    EditedFactStatus = EditedFactStatus.InputEqual;
    }

    protected void UpdateFactBase(Fact p_fact)
    {
      Id = p_fact.Id;
      ClientId = p_fact.ClientId;
      ProductId = p_fact.ProductId;
      AdjustmentId = p_fact.AdjustmentId;
    }

    protected EditedFactStatus RaiseStatusEvent(EditedFactStatus p_status)
    {
      if (OnCellValueChanged != null)
        OnCellValueChanged(Cell, p_status);
      
      return p_status;
    }

  }
}
