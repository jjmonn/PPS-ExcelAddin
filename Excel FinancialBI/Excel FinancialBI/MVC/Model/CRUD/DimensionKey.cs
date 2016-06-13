using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{

  using AccountKey = UInt32;
  using EntityKey = UInt32;
  using EmployeeKey = UInt32;
  using PeriodKey = Int32;


  class DimensionKey
  {
    Tuple<EntityKey, AccountKey, EmployeeKey, PeriodKey> m_key;
    public UInt32 EntityId { get { return (m_key.Item1); } }
    public UInt32 AccountId { get { return (m_key.Item2); } }
    public UInt32 EmployeeId { get { return (m_key.Item3); } }
    public Int32 Period { get { return (m_key.Item4); } }

    public DimensionKey(Tuple<EntityKey, AccountKey, EmployeeKey, PeriodKey> p_key)
    {
      m_key = p_key;
    }

    public DimensionKey(EntityKey p_entityId, AccountKey p_accountId, EmployeeKey p_employeeId, PeriodKey p_period)
    {
      m_key = new Tuple<EntityKey, AccountKey, EmployeeKey, PeriodKey>(p_entityId, p_accountId, p_employeeId, p_period);
    }

    public override bool Equals(object p_obj)
    {
      DimensionKey l_obj = (DimensionKey)p_obj;

      if (l_obj.GetHashCode() == GetHashCode())
        if (l_obj.m_key.Item1 == m_key.Item1)
          if (l_obj.m_key.Item2 == m_key.Item2)
            if (l_obj.m_key.Item3 == m_key.Item3)
              return (l_obj.m_key.Item4 == m_key.Item4);
      return (false);
    }

    public override int GetHashCode()
    {
      return m_key.GetHashCode();
    }

  }
}
