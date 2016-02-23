using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.MVC.Model.CRUD;
  using Microsoft.Office.Interop.Excel;
  

  class EditedFact
  {
    Account.AccountProcess m_process;
    Fact m_fact { get; set;}
    public Account m_account { get; private set;}
    public AxisElem m_entity { get; private set;}
    public AxisElem m_employee { get; private set;}
    public PeriodDimension m_period { get; private set;}
    double m_editedValue { get; set;}
    UInt32 m_editedClientId;
    Range m_cell { get; set;}
    bool m_mustCommit {get ; set;}

    public EditedFact(Account p_account, AxisElem p_entity, AxisElem p_employee, PeriodDimension p_period, Range p_cell, Account.AccountProcess p_process)
    {
      m_cell = p_cell;
      m_process = p_process;
      m_account = p_account;
      m_entity = p_entity;
      m_employee = p_employee;
      m_period = p_period;

      Fact l_fact = new Fact();
      l_fact.AccountId = m_account.Id;
      l_fact.EntityId = m_entity.Id;
      l_fact.Period = m_period.Id;
      l_fact.EmployeeId = m_employee.Id;
      m_fact = l_fact;
    }

    virtual public bool IsDifferent()
    {
      bool l_isDifferent;
      if (m_process == Account.AccountProcess.FINANCIAL)
         l_isDifferent = (m_editedValue == m_fact.Value);
      else
        // TO DO  : antiduplicate system
        l_isDifferent = (m_editedClientId == m_fact.ClientId);
        
      if (l_isDifferent)
      {
        // TO DO : highligh cell   -> Event range higlighter
        m_mustCommit = true;
      }
      return l_isDifferent;
    }

    public void UpdateFactValue(double p_value)
    {
      m_fact.Value = p_value;
    }

    public void UpdateCellValue()
    {
      m_cell.Value2 = m_fact.Value;
      //           -> update cells color on worksheet  -> event catch by highlighter
    }

    virtual public void SetEditedValue(double p_value)
    {
      if (IsDifferent() == true)
      {
          m_editedValue = p_value;
       }
    }

    virtual public void SetEditedValue(UInt32 p_clientId)
    {
      if (IsDifferent() == true)
      {
        m_editedClientId = p_clientId;
      }
    }
  }
}
