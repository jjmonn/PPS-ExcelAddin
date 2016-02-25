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
    public UInt32 EditedClientId {get; set;}
    public double EditedValue {get; set;}
    public Range Cell { get; private set;}
    public CellStatus CellStatus { get; private set;}
    public event CellValueChangedEventHandler OnCellValueChanged;

    public EditedFact(UInt32 p_accountId, UInt32 p_entityId, UInt32 p_clientId, UInt32 p_productId, UInt32 p_adjustmentId, UInt32 p_employeeId, UInt32 p_versionId, PeriodDimension p_period, Range p_cell, Account.AccountProcess p_process)
    {
      Cell = p_cell;
      m_process = p_process; 
      PeriodDimension = p_period;

      this.AccountId = p_accountId;
      this.EntityId = p_entityId;
      this.ClientId = p_clientId;
      this.ProductId = p_productId;
      this.AdjustmentId = p_adjustmentId;
      this.EmployeeId = p_employeeId;
      this.Period = PeriodDimension.Id;
      this.VersionId = p_versionId;

      if (m_process == CRUD.Account.AccountProcess.FINANCIAL)
        EditedValue = Convert.ToDouble(Cell.Value2);
      else
        EditedClientId = GetCellClientId(); 
    }
 
    public void UpdateFact(Fact p_fact)
    {
      Id = p_fact.Id;
      Value = p_fact.Value;
      ClientId = p_fact.ClientId;
    
      if (m_process == CRUD.Account.AccountProcess.FINANCIAL)
        SetCellStatusFinancial();
      else
        SetCellStatusRH();
    }

    public void SetCellStatusFinancial()
    {
      if (EditedValue != this.Value)
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

    private void SetCellStatusRH()
    {
      if (EditedClientId != this.ClientId)
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

    // Below : not here -> View
    public void UpdateCellValue()
    {
      Cell.Value2 = this.Value;
      // checker à quel moment c'est appelé
      // Event ->  event for highlighter - update cells color on worksheet 
    }

    // Below : attention valeur invalide traitée comme 0 ? + ce code doit-il être ici ?
    private double GetFactValue()
    {
      if (Cell.Value2 == null)
        return 0;
      double l_doubleValue;
      if (Cell.Value2.GetType() == typeof(string))
      {
        if (Double.TryParse(Cell.Value2 as string, out l_doubleValue))
          return l_doubleValue;
        else return 0;
      }
      return 0;
    }

    // Attention : traitement des valeurs invalides ? + ce code doit-il être ici ?
    private UInt32 GetCellClientId()
    {
      if (Cell.Value2 == null)
        return 0;
      if (Cell.Value2.GetType() == typeof(string))
      {
        AxisElem l_client = AxisElemModel.Instance.GetValue(AxisType.Client, Cell.Value2 as string);
        if (l_client == null)
          return 0;
        return l_client.Id;
      }
      return 0;
    }

    // BEFORE COMMIT : set clientId or Value to the editedvalue


  }
}
