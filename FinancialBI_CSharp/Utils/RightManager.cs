using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD;

public class RightManager
{
  private SafeDictionary<dynamic, Group.Permission> m_rights = new SafeDictionary<dynamic, Group.Permission>();

  public Group.Permission this[dynamic index]
  {
    get
    {
      return m_rights[index];
    }
    set
    {
      m_rights[index] = value;
    }
  }

  public void Enable(UInt64 p_permission)
  {
    foreach (KeyValuePair<dynamic, Group.Permission> pair in m_rights)
      pair.Key.Enabled = (((UInt64)pair.Value & p_permission) != 0);
  }
}