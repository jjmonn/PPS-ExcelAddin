using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using FBI.MVC.Model;

  class PeriodRangeSelectionController
  {
    UInt32 m_version;
    public List<UInt32> m_periodList { get; private set; }
    public bool m_isPeriodListValid = true;
    UInt32 NB_WEEKS_FORWARD = 3;
    UInt32 NB_WEEKS_BACKWARD = 1;

    public PeriodRangeSelectionController(UInt32 p_versionId)
    {
      m_periodList = PeriodModel.GetPeriodsList(p_versionId);
      if (m_periodList == null || m_periodList.Count == 0)
        m_isPeriodListValid = false;
    }

    public DateTime GetMinDate()
    {
      if (m_isPeriodListValid)
        return DateTime.FromOADate(m_periodList[0]);
      else
        return DateTime.Now;
    }

    public DateTime GetMaxDate()
    {
      if (m_isPeriodListValid)
        return DateTime.FromOADate(m_periodList[m_periodList.Count - 1]);
      else
        return DateTime.Now;
    }

    public DateTime GetStartDate()
    {
      DateTime l_startDate = DateTime.Now.AddDays(-PeriodModel.m_nbDaysInWeek * NB_WEEKS_BACKWARD);
      if (l_startDate >= GetMinDate() && l_startDate <= GetMaxDate())
        return l_startDate;
      else
        return GetMinDate();
    }

    public DateTime GetEndDate()
    {
      DateTime l_endDate = DateTime.Now.AddDays(PeriodModel.m_nbDaysInWeek * NB_WEEKS_FORWARD);
      if (l_endDate >= GetMinDate() && l_endDate <= GetMaxDate())
        return l_endDate;
      else
        return GetMaxDate();

    }

    public List<UInt32> GetPeriodRange(DateTime p_startDate, DateTime p_endDate)
    {
      List<UInt32> l_periodsRange = new List<UInt32>();
      DateTime l_date = p_startDate;
      while (l_date <= p_endDate)
      {
        l_periodsRange.Add((UInt32)l_date.ToOADate());
        l_date = l_date.AddDays(1);
      }
      return l_periodsRange;
    }

  }
}
