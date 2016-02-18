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
    private DateTime m_minDate;
    private DateTime m_maxDate;

    public PeriodRangeSelectionController(UInt32 p_versionId)
    {
      m_view = new PeriodRangeSelectionControl(p_versionId);
      m_view.SetController(this);
      m_periodList = PeriodModel.GetPeriodsList(p_versionId);
      if (m_periodList == null || m_periodList.Count == 0)
        m_isPeriodListValid = false;
      MinDate = (m_isPeriodListValid) ? DateTime.FromOADate(m_periodList[0]) : DateTime.Now;
      MaxDate = (m_isPeriodListValid) ? DateTime.FromOADate(m_periodList[m_periodList.Count - 1]) : DateTime.Now;
      LoadView();
    }

    void LoadView()
    {
      m_view.LoadView();
    }

    public DateTime MinDate 
    {
      get { return (m_minDate); }
      set
      {
        m_minDate = value;
        if (m_view != null)
          m_view.MinDate = value;
      }
    }

    public DateTime MaxDate
    {
      get { return (m_maxDate); }
      set
      {
        m_maxDate = value;
        if (m_view != null)
          m_view.MaxDate = value;
      }
    }

    public DateTime GetDefaultStartDate()
    {
      DateTime l_startDate = DateTime.Now.AddDays(-PeriodModel.m_nbDaysInWeek * NB_WEEKS_BACKWARD);
      if (l_startDate >= MinDate && l_startDate <= MaxDate)
        return l_startDate;
      else
        return MinDate;
    }

    public DateTime GetDefaultEndDate()
    {
      DateTime l_endDate = DateTime.Now.AddDays(PeriodModel.m_nbDaysInWeek * NB_WEEKS_FORWARD);
      if (l_endDate >= MinDate && l_endDate <= MaxDate)
        return l_endDate;
      else
        return MaxDate;

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
      return (m_view.GetPeriodList().Count);
    }

    public List<Int32> GetPeriodList()
    {
      return (m_view.GetPeriodList());
    }

    public Int32 GetStartDate()
    {
      List<Int32> m_periods = m_view.GetPeriodList();

      if (m_periods == null || m_periods.Count <= 0)
        return (0);
      return (m_periods[0]);
    }
  }
}