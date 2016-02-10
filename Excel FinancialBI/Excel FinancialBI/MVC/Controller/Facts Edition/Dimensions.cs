using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model
{
  using FBI.Utils;
  using Microsoft.Office.Interop.Excel;
  using FBI.MVC.Model.CRUD;


  class Dimensions
  {
    public enum Orientation
    {
      ACCOUNTS_PERIODS = 0,
      PERIODS_ACCOUNTS,
      ENTITIES_ACCOUNTS,
      ACCOUNTS_ENTITIES,
      PERIODS_ENTITIES,
      ENTITIES_PERIODS,
      EMPLOYEES_PERIODS,
      PERIODS_EMPLOYEES,
      FINANCIAL_ORIENTATION_ERROR,
      PDC_ORIENTATION_ERROR,
      ORIENTATION_ERROR
    }

    Orientation m_orientation;
    public Account.AccountProcess m_process { get; private set; }
    public Dimension<Account> m_accounts { get; set; }
    public Dimension<Account> m_outputsAccounts { get; set; }
    public Dimension<AxisElem> m_entities { get; set; }
    public Dimension<AxisElem> m_employees { get; set; }
    public Dimension<Period> m_periods { get; set; }

    public Dimensions()
    {
      m_accounts = new Dimension<Account>();
      m_outputsAccounts= new Dimension<Account>();
      m_entities  = new Dimension<AxisElem>();
      m_employees = new Dimension<AxisElem>();
      m_periods = new Dimension<Period>();
    }
                                                         

    public bool IsValid()
    {
      if (m_orientation == Orientation.ORIENTATION_ERROR)
        return false;
      return true;
    }

    public void DefineOrientation()
    {
      // TO DO: define global process and orientation based on dimensions values count and alignment 
    
      // TO DO : if orientations valid : highlight dimensions cells
    }

    private void DefineDimensionsAlignments()
    {
      m_accounts.DefineAlignment();
      m_outputsAccounts.DefineAlignment();
      m_entities.DefineAlignment();
      m_employees.DefineAlignment();
      m_periods.DefineAlignment();
    }


 
  }
}
