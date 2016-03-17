using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;

  class DuplicateRHAllocation
  {
    Account m_account;
    AxisElem m_entity;
    AxisElem m_employee;
    Int32 m_period;
    public List<Fact> DuplicateFacts { get; private set; }

    public DuplicateRHAllocation(Account p_account, AxisElem p_entity, AxisElem p_employee, Int32 p_period)
    {
      DuplicateFacts = new List<Fact>();
      m_account = p_account;
      m_entity = p_entity;
      m_employee = p_employee;
      m_period = p_period;
    }

    public void AddFact(Fact p_fact)
    {
      DuplicateFacts.Add(p_fact);
    }

  }

}
