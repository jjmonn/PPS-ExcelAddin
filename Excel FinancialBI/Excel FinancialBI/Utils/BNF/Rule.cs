using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  class Rule
  {
    private string m_name;
    private string m_event;

    public void Clear()
    {
      m_name = null;
      m_event = null;
    }

    public string Name
    {
      get { return (m_name); }
      set { m_name = value; }
    }

    public bool HasEvent
    {
      get { return (m_event != null); }
    }

    public string Event
    {
      get { return (m_event); }
      set { m_event = value; }
    }
  }
}
