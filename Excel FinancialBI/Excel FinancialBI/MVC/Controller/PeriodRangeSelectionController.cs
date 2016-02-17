using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Controller
{
  using Model;
  using View;

  class PeriodRangeSelectionController : IController
  {
    public string Error { get; set; }
    PeriodRangeSelectionControl m_view;
    public IView View { get { return (m_view); } }
    public List<Int32> m_periodList { get; private set; }
    public bool m_isPeriodListValid = true;
    const UInt32 NB_WEEKS_FORWARD = 3;
    const UInt32 NB_WEEKS_BACKWARD = 1;

    public PeriodRangeSelectionController(UInt32 p_versionId)
    {
      m_view = new PeriodRangeSelectionControl(p_versionId);
      m_view.SetController(this);
      m_periodList = PeriodModel.GetPeriodsList(p_versionId);
      if (m_periodList == null || m_periodList.Count == 0)
        m_isPeriodListValid = false;
      LoadView();
    }

    void LoadView()
    {
      m_view.LoadView();
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

    public List<Int32> GetPeriodRange(DateTime p_startDate, DateTime p_endDate)
    {
      List<Int32> l_periodsRange = new List<Int32>();
      DateTime l_date = p_startDate;
      while (l_date <= p_endDate)
      {
        l_periodsRange.Add((Int32)l_date.ToOADate());
        l_date = l_date.AddDays(1);
      }
      return l_periodsRange;
    }

    public Int32 GetNbPeriods()
    {
      return (GetPeriodRange(GetStartDate(), GetEndDate()).Count);
    }

  }
}