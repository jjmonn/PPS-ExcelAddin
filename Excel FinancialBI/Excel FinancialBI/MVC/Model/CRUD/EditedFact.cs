using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.MVC.Model.CRUD;
  using Microsoft.Office.Interop.Excel;
  
  public enum CellStatus
  {
    Equal = 0,
    DifferentInput,
    DifferentOutput
  }

  public delegate void CellValueChangedEventHandler(Range p_cell, CellStatus p_status);


  class EditedFact : Fact
  {
    Account.AccountProcess m_process;
    public Account Account { get { return AccountModel.Instance.GetValue(this.AccountId); }}
    public AxisElem Entity { get { return AxisElemModel.Instance.GetValue(AxisType.Entities, this.EntityId); } }
    public AxisElem Employee { get { return AxisElemModel.Instance.GetValue(AxisType.Employee, this.EmployeeId); } }
    public PeriodDimension PeriodDimension { get; private set;}
    UInt32 m_editedClientId;
    double m_editedValue;
    public Range Cell { get; private set;}
    public CellStatus CellStatus { get; private set;}
    public event CellValueChangedEventHandler OnCellValueChanged;

    public EditedFact(Account p_account, AxisElem p_entity, AxisElem p_employee, PeriodDimension p_period, Range p_cell, Account.AccountProcess p_process)
    {
      Cell = p_cell;
      m_process = p_process; 
      PeriodDimension = p_period;

      this.AccountId = Account.Id;
      this.EntityId = Entity.Id;
      this.Period = PeriodDimension.Id;
      this.EmployeeId = Employee.Id;
    }

    public void SetEditedFinancialValue(double p_value)
    {
      m_editedValue = p_value;
      if (m_editedValue != this.Value)
      {
        if (Account.FormulaType == CRUD.Account.FormulaTypes.HARD_VALUE_INPUT || Account.FormulaType == CRUD.Account.FormulaTypes.FIRST_PERIOD_INPUT)
        {
          CellStatus = Model.CellStatus.DifferentInput;
          OnCellValueChanged(Cell, Model.CellStatus.DifferentInput);
        }
        else
        {  
          OnCellValueChanged(Cell, Model.CellStatus.DifferentOutput);
          CellStatus = Model.CellStatus.DifferentOutput;
        }
      }
      else
      {
        CellStatus = Model.CellStatus.Equal;
        OnCellValueChanged(Cell, Model.CellStatus.Equal);
      }
    }

    public void SetEditedRHValue(UInt32 p_clientId)
    {
      m_editedClientId = p_clientId;
      if (m_editedClientId != this.ClientId)
      {
        CellStatus = Model.CellStatus.DifferentInput;
        OnCellValueChanged(Cell, Model.CellStatus.DifferentInput);
      }
      else
      {
        CellStatus = Model.CellStatus.Equal;
        OnCellValueChanged(Cell, Model.CellStatus.Equal);
      }
    }

    public void UpdateFact(Fact p_fact)
    {
      Id = p_fact.Id;
      Value = p_fact.Value;
      EmployeeId = p_fact.EmployeeId;
      SetEditedFinancialValue(p_fact.Value);
      SetEditedRHValue(p_fact.EmployeeId);
    }

    // Below : not here -> View
    public void UpdateCellValue()
    {
      Cell.Value2 = this.Value;
      // checker à quel moment c'est appelé
      // Event ->  event for highlighter - update cells color on worksheet 
    }

    
  }
}
