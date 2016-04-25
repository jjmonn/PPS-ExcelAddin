using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Globalization;
using Microsoft.VisualBasic;

namespace FBI.MVC.Model
{
  using Utils;
  using FBI.MVC.Model;
  using FBI.MVC.Model.CRUD;


  class PeriodModel
  {
    #region "Instance Variables"

    public const Int32 m_nbMonthInYear = 12;
    public const Int32 m_nbDaysInWeek = 7;
    public const Int32 m_nbWeekdsInYear = 52;

    public const Int32 m_nbDaysInYear = 365;

    static Int32[] m_monthList = new Int32[] {
			31,
			28,
			31,
			30,
			31,
			30,
			31,
			31,
			30,
			31,
			30,
			31
		};

    #endregion

    static public List<Int32> GetPeriodsList(UInt32 p_versionId)
    {
      Version l_version = VersionModel.Instance.GetValue(p_versionId);
      if (l_version == null)
        return null;

      switch (l_version.TimeConfiguration)
      {
        case CRUD.TimeConfig.YEARS:
          return GetYearsList((Int32)l_version.StartPeriod, (Int32)l_version.NbPeriod, l_version.TimeConfiguration);
        case CRUD.TimeConfig.MONTHS:
          return GetMonthsList((Int32)l_version.StartPeriod, (Int32)l_version.NbPeriod, l_version.TimeConfiguration);
        case CRUD.TimeConfig.WEEK:
          return GetWeeksList((Int32)l_version.StartPeriod, (Int32)l_version.NbPeriod, l_version.TimeConfiguration);
        case CRUD.TimeConfig.DAYS:
          return GetDaysList((Int32)l_version.StartPeriod, (Int32)l_version.NbPeriod);
        default:
          System.Diagnostics.Debug.WriteLine("Version get periods list: Unknown Time Configuration");
          return null;
      }
    }

    static public Int32 GetLastPeriod(Int32 p_startPeriod, Int32 p_nbPeriod, CRUD.TimeConfig p_timeConfig)
    {
      List<Int32> l_list = GetPeriodList(p_startPeriod, p_nbPeriod, p_timeConfig);

      if (l_list.Count > 0)
        return (l_list[l_list.Count - 1]);
      return (0);
    }

    static public List<Int32> GetPeriodList(Int32 p_startPeriod, Int32 p_nbPeriod, CRUD.TimeConfig p_timeConfig)
    {
      switch (p_timeConfig)
      {
        case CRUD.TimeConfig.YEARS:
          return GetYearsListFromYearlyConfiguration(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.MONTHS:
          return GetMonthsListFromMonthlyConfiguration(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.WEEK:
          return GetWeekIDListFromWeeklyConfig(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.DAYS:
          return GetDaysList(p_startPeriod, p_nbPeriod);
      }
      return new List<Int32>();
    }

    #region "Years interface"
  
    static public List<Int32> GetSubPeriods(CRUD.TimeConfig p_timeConfig, Int32 p_period)
    {
      switch (p_timeConfig)
      {
        case CRUD.TimeConfig.YEARS:
          return GetMonthsIdsInYear(p_period);
        case CRUD.TimeConfig.WEEK:
          return GetDaysIdListInWeek(p_period);
        default:
          return new List<int>();
      }
    }

    static public string GetFormatedDate(Int32 p_period, CRUD.TimeConfig p_timeConfig)
    {
      switch (p_timeConfig)
      {
        case CRUD.TimeConfig.YEARS:
          return DateTime.FromOADate(p_period).ToString("yyyy");
        case CRUD.TimeConfig.MONTHS:
          return DateTime.FromOADate(p_period).ToString("MMMM yyyy");
        case CRUD.TimeConfig.WEEK:
          return Utils.Local.GetValue("general.week") + " " + GetWeekNumberFromDateId(p_period) + ", " + DateTime.FromOADate(p_period).ToString("yyyy");
        case CRUD.TimeConfig.DAYS:
          return DateTime.FromOADate(p_period).ToString("d"); // format to be set in settings
          //return DateTime.FromOADate(p_period).ToString("MMMM dd, yyyy");
      }
      return (DateTime.FromOADate(p_period).ToShortDateString());
    }

    static public List<Int32> GetYearsList(Int32 p_startPeriod, Int32 p_nbPeriod, CRUD.TimeConfig p_timeConfig)
    {
      switch (p_timeConfig)
      {
        case CRUD.TimeConfig.YEARS:
          return GetYearsListFromYearlyConfiguration(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.MONTHS:
          return GetYearsListFromMonthlyConfiguration(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.WEEK:
          return GetYearsListFromWeeklyConfiguration(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.DAYS:
          return GetYearsListFromDailyConfiguration(p_startPeriod, p_nbPeriod);
        default:
          System.Diagnostics.Debug.WriteLine("Undefined time configuration");
          return null;
      }
    }

    private static List<Int32> GetYearsListFromYearlyConfiguration(Int32 p_startPeriod, Int32 p_nbPeriods)
    {

      List<Int32> l_yearsIdList = new List<Int32>();
      l_yearsIdList.Add(p_startPeriod);
      double l_year = p_startPeriod / 365.25 + 1900;

      for (Int32 i = 1; i <= p_nbPeriods - 1; i++)
      {
        if ((DateTime.IsLeapYear((Int32)l_year)))
        {
          p_startPeriod += 366;
        }
        else
        {
          p_startPeriod += 365;
        }
        l_yearsIdList.Add(p_startPeriod);
        l_year += 1;
      }
      return (l_yearsIdList);
    }

    private static List<Int32> GetYearsListFromMonthlyConfiguration(Int32 p_startPeriod, Int32 p_nbPeriods)
    {

      List<Int32> l_yearsIdList = new List<Int32>();
      Int32 l_yearId = 0;

      List<Int32> l_monthList = GetMonthsList(p_startPeriod, p_nbPeriods, CRUD.TimeConfig.MONTHS);
      foreach (Int32 l_monthId in l_monthList)
      {
        l_yearId = GetYearIdFromMonthID(l_monthId);
        if (l_yearsIdList.Contains(l_yearId) == false)
          l_yearsIdList.Add(l_yearId);
      }
      return l_yearsIdList;

    }

    private static List<Int32> GetYearsListFromWeeklyConfiguration(Int32 p_startPeriod, Int32 p_nbPeriods)
    {

      List<Int32> l_yearsIdList = new List<Int32>();
      Int32 yearId = 0;
      List<Int32> l_weekList = GetWeeksList(p_startPeriod, p_nbPeriods, CRUD.TimeConfig.WEEK);

      foreach (Int32 l_weekId in l_weekList)
      {
        yearId = (Int32)DateTime.FromOADate(l_weekId).Year;
        if (l_yearsIdList.Contains(yearId) == false)
          l_yearsIdList.Add(yearId);
      }
      return l_yearsIdList;

    }

    private static List<Int32> GetYearsListFromDailyConfiguration(Int32 p_startPeriod, Int32 p_nbPeriods)
    {

      List<Int32> l_yearsIdList = new List<Int32>();
      Int32 yearId = default(Int32);
      foreach (Int32 l_dayId in GetDaysList(p_startPeriod, p_nbPeriods))
      {
        yearId = (Int32) DateTime.FromOADate(l_dayId).Year;
        if (l_yearsIdList.Contains(yearId) == false)
          l_yearsIdList.Add(yearId);
      }
      return l_yearsIdList;

    }

    #endregion

    #region "Months interface"

    static public List<Int32> GetMonthsList(Int32 p_startPeriod, Int32 p_nbPeriod, CRUD.TimeConfig p_timeConfiguration)
    {

      switch (p_timeConfiguration)
      {
        case CRUD.TimeConfig.MONTHS:
          return GetMonthsListFromMonthlyConfiguration(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.WEEK:
          return GetMonthsListFromWeeklyConfiguration(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.DAYS:
          return GetMonthsListFromDailyConfiguration(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.YEARS:
          System.Diagnostics.Debug.WriteLine("Period.vb - Get GetMonthsList() - yearly time configuration asked (years must not be broke down in months because not values on months if yearly config).");
          return null;
        default:
          System.Diagnostics.Debug.WriteLine("Period.vb - Get GetMonthsList() - Undefined time configuration");
          return null;
      }

    }

    private static List<Int32> GetMonthsListFromMonthlyConfiguration(Int32 p_startPeriod, Int32 p_nbPeriod)
    {
      List<Int32> l_periodsList = new List<Int32>();
      l_periodsList.Add(p_startPeriod);
      if ((p_nbPeriod <= 1))
        return (l_periodsList);
      double l_currentYear = DateTime.FromOADate(p_startPeriod).Year;
      Int32 l_currentMonth =  (Int32) DateTime.FromOADate(p_startPeriod).Month;

      for (Int32 i = 1; i <= p_nbPeriod - 1; i++)
      {
        p_startPeriod += m_monthList[l_currentMonth % (12)];
        // february of a leap year
        if ((l_currentMonth == 1 && DateTime.IsLeapYear((Int32)l_currentYear)))
        {
          p_startPeriod += 1;
        }
        l_periodsList.Add(p_startPeriod);
        if ((l_currentMonth == 12))
        {
          l_currentYear += 1;
          l_currentMonth = 0;
        }
        l_currentMonth += 1;
      }
      return (l_periodsList);
    }

    private static List<Int32> GetMonthsListFromWeeklyConfiguration(Int32 p_startPeriod, Int32 p_nbPeriods)
    {

      List<Int32> l_monthsIdList = new List<Int32>();
      Int32 l_monthId = 0;
      List<Int32> l_weekList = GetWeeksList(p_startPeriod, p_nbPeriods, CRUD.TimeConfig.WEEK);

      foreach (Int32 l_weekId in l_weekList)
      {
        l_monthId = GetMonthIdFromPeriodId(l_weekId);
        if (l_monthsIdList.Contains(l_monthId) == false)
          l_monthsIdList.Add(l_monthId);
      }
      return (l_monthsIdList);
    }

    private static List<Int32> GetMonthsListFromDailyConfiguration(Int32 p_startPeriod, Int32 p_nbPeriods)
    {

      List<Int32> l_monthsIdList = new List<Int32>();
      Int32 l_monthId = default(Int32);
      foreach (Int32 l_dayId in GetDaysList(p_startPeriod, p_nbPeriods))
      {
        l_monthId = GetMonthIdFromPeriodId(l_dayId);
        if (l_monthsIdList.Contains(l_monthId) == false)
          l_monthsIdList.Add(l_monthId);
      }
      return (l_monthsIdList);
    }

    // yearId: "31/12/N" au format INT
    static public List<Int32> GetMonthsIdsInYear(Int32 p_yearId)
    {
      List<Int32> l_monthsIdList = new List<Int32>();
      Int32 l_monthId = 0;
      Int32 l_year = System.DateTime.FromOADate(p_yearId).Year;

      for (Int32 l_monthIndex = 0; l_monthIndex <= 11; l_monthIndex++)
      {
        l_monthId = (Int32)DateAndTime.DateSerial(l_year, l_monthIndex + 1, m_monthList[l_monthIndex]).ToOADate();
        // Add 1 day if february of a leap year
        if (l_monthIndex == 1 && DateTime.IsLeapYear(l_year))
          l_monthId++;
        l_monthsIdList.Add(l_monthId);
      }
      return (l_monthsIdList);
    }

    #endregion

    #region "Weeks interface"

    static public List<Int32> GetWeeksList(Int32 p_startPeriod, Int32 p_nbPeriod, CRUD.TimeConfig p_timeConfig)
    {
      switch (p_timeConfig)
      {
        case CRUD.TimeConfig.WEEK:
          return GetWeekIDListFromWeeklyConfig(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.DAYS:
          return GetWeekIDListFromDailyConfig(p_startPeriod, p_nbPeriod);
        case CRUD.TimeConfig.MONTHS:
          System.Diagnostics.Debug.WriteLine("Period.vb - GetWeeksList() - Monthly config version asked");
          return new List<Int32>();
        case CRUD.TimeConfig.YEARS:
          System.Diagnostics.Debug.WriteLine("Period.vb - GetWeeksList() - Yearly config version asked");
          return new List<Int32>();
      }
      return new List<Int32>();
    }

    private static List<Int32> GetWeekIDListFromWeeklyConfig(Int32 p_startPeriod, Int32 p_nbPeriod)
    {

      List<Int32> l_weeksList = new List<Int32>();
      Int32 l_periodId = GetFirstDayOfWeekId(p_startPeriod);

      // Check which day a week starts !!
      for (Int32 i = 0; i <= p_nbPeriod - 1; i++)
      {
        string l_period = DateTime.FromOADate(l_periodId).ToLongDateString();
        l_weeksList.Add(l_periodId);
        l_periodId += (Int32)m_nbDaysInWeek;
      }
      return l_weeksList;

    }

    private static List<Int32> GetWeekIDListFromDailyConfig(Int32 p_startPeriod, Int32 p_nbPeriod)
    {

      List<Int32> l_weeksList = new List<Int32>();
      Int32 l_periodId = p_startPeriod;
      Int32 l_currentWeekId = 0;
      List<Int32> l_dayList = GetDaysList(p_startPeriod, p_nbPeriod);

      foreach (Int32 l_dayId in l_dayList)
      {
        l_currentWeekId = GetWeekIdFromPeriodId(l_dayId);
        if (l_weeksList.Contains(l_currentWeekId) == false)
          l_weeksList.Add(l_currentWeekId);
      }
      return l_weeksList;

    }

    static public Int32 GetFirstDayOfWeekId(Int32 p_dayId)
    {
      if (p_dayId == 0)
        return (0);
      Int32 l_weekId = p_dayId - DateAndTime.Weekday(System.DateTime.FromOADate(p_dayId), Microsoft.VisualBasic.FirstDayOfWeek.Monday) + 1;
      return (l_weekId);
    }

    static public List<Int32> GetWeeksPeriodListFromPeriodsRange(System.DateTime p_startDate, System.DateTime p_endDate)
    {

      List<Int32> l_periods = new List<Int32>();
      while (p_startDate < p_endDate)
      {
        l_periods.Add(PeriodModel.GetWeekIdFromPeriodId((Int32)p_startDate.ToOADate()));
        p_startDate = p_startDate.AddDays(m_nbDaysInWeek);
      }
      return l_periods;

    }

    static public List<Int32> GetMonthPeriodListFromPeriodsRange(System.DateTime p_startDate, System.DateTime p_endDate)
    {
      List<Int32> l_periods = new List<Int32>();
      while (p_startDate < p_endDate)
      {
        l_periods.Add(PeriodModel.GetWeekIdFromPeriodId((Int32)p_startDate.ToOADate()));
        p_startDate = p_startDate.AddMonths(1);
      }
      return l_periods;
    }

    static public List<Int32> GetYearsPeriodListFromPeriodsRange(System.DateTime p_startDate, System.DateTime p_endDate)
    {
      List<Int32> l_periods = new List<Int32>();
      while (p_startDate < p_endDate)
      {
        l_periods.Add((Int32)p_startDate.ToOADate());
        p_startDate = p_startDate.AddYears(1);
      }
      l_periods.Add((Int32)p_startDate.ToOADate());
      return l_periods;
    }

    #endregion

    #region "Days interface"
    
    static public List<Int32> GetDaysList(Int32 p_startDayId, Int32 p_nbDays)
    {
      bool l_includeWeekEnds = Properties.Settings.Default.includeWeekEnds;
      List<DayOfWeek> l_weekEndDays = PeriodModel.GetWeekEndDays();
      List<Int32> l_daysList = new List<Int32>();

      for (Int32 l_dayId = p_startDayId; l_dayId <= p_startDayId + p_nbDays - 1; l_dayId++)
        l_daysList.Add(l_dayId);
      return (l_daysList);
    }

    static public bool IsWeekEnd(Int32 p_day)
    {
      List<DayOfWeek> l_weekEndDays = PeriodModel.GetWeekEndDays();

      return (l_weekEndDays.Contains(DateTime.FromOADate((double)p_day).DayOfWeek));
    }

    static public List<Int32> GetDaysPeriodsListFromWeeksId(List<Int32> p_weeksPeriodsList)
    {
      List<Int32> l_periods = new List<Int32>();
      foreach (Int32 l_weekId in p_weeksPeriodsList)
        foreach (Int32 l_day in GetDaysIdListInWeek(l_weekId))
          l_periods.Add(l_day);
      return (l_periods);
    }

    static public List<Int32> GetDaysIdListInWeek(Int32 p_weekId)
    {
      List<Int32> l_daysIdList = new List<Int32>();
      string l_date = DateTime.FromOADate(p_weekId).ToLongDateString();
      Int32 l_dayId = GetFirstDayOfWeekId(p_weekId);

      for (Int32 i = 1; i <= 7; i++)
      {
        l_daysIdList.Add(l_dayId);
        l_dayId += 1;
      }
      return (l_daysIdList);
    }

    #endregion

    #region "Utilities"

    static public Int32 GetYearIDFromYearValue(Int32 p_yearValue)
    {
      return ((Int32)(DateAndTime.DateSerial((Int32)p_yearValue, 12, 31).ToOADate()));
    }

    static public Int32 GetYearValueFromYearID(Int32 p_yearId)
    {
      return ((Int32)DateTime.FromOADate(Convert.ToDouble(p_yearId)).Year);
    }

    static public Int32 GetYearIdFromPeriodId(Int32 p_periodId)
    {
      return ((Int32)GetYearIdFromMonthID(GetMonthIdFromPeriodId(p_periodId)));
    }

    static public Int32 GetYearIdFromMonthID(Int32 p_monthId)
    {

      Int32 year_ = (Int32)DateTime.FromOADate(p_monthId).Year;
      return ((Int32)DateAndTime.DateSerial((Int32)year_, 12, 31).ToOADate());

    }

    static public Int32 GetWeekIdFromPeriodId(Int32 p_dayId)
    {

      // Uses localized settings for the first day of the week.
      if (p_dayId == 0)
        return 0;
      Int32 l_weekId = p_dayId - (Int32)DateAndTime.Weekday(System.DateTime.FromOADate(p_dayId), Microsoft.VisualBasic.FirstDayOfWeek.System) + 7;
      return l_weekId;

    }

    // Return the last day of month
    static public Int32 GetMonthIdFromPeriodId(Int32 p_periodId)
    {

      Int32 l_monthId = default(Int32);
      Int32 l_year = (Int32)DateTime.FromOADate(p_periodId).Year;

      Int32 l_monthnb = (Int32)DateTime.FromOADate((Int32)p_periodId).Month - 1;
      l_monthId = (Int32)DateAndTime.DateSerial((Int32)l_year, DateTime.FromOADate((Int32)p_periodId).Month, (Int32)m_monthList[l_monthnb]).ToOADate();

      // Add 1 day if february of a leap year
      if (l_monthnb == 1 && DateTime.IsLeapYear((Int32)l_year))
        l_monthId += 1;
      return (l_monthId);
    }

    // Compliant with ISO 860
    static public Int32 GetWeekNumberFromDateId(Int32 p_periodId)
    {

      // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
      // be the same week# as whatever Thursday, Friday or Saturday are,  and we always get those right
      DateTime l_time = System.DateTime.FromOADate(p_periodId);
      DayOfWeek l_day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(l_time);
      if (l_day >= DayOfWeek.Monday && l_day <= DayOfWeek.Wednesday)
        l_time = l_time.AddDays(3);

      // Return the week of our adjusted day
      return (Int32)CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(l_time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    static public List<Int32> FilterPeriodList(List<Int32> p_filteredPeriods, List<Int32> m_FilterReferencePeriodsList)
    {

      List<Int32> l_resultPeriods = new List<Int32>();
      foreach (Int32 l_periodId in p_filteredPeriods)
      {
        if (l_periodId >= m_FilterReferencePeriodsList[0])
        {
          if (l_periodId <= m_FilterReferencePeriodsList[m_FilterReferencePeriodsList.Count - 1])
            l_resultPeriods.Add(l_periodId);
          else
            return l_resultPeriods;
        }
      }
      return l_resultPeriods;
    }

    public static Int32 GetDayMinus3Weeks(Int32 p_dayId)
    {
      DateTime l_date = DateTime.FromOADate(p_dayId);
      l_date = l_date.AddDays(-3 * m_nbDaysInWeek);
      return (Int32)l_date.ToOADate();
    }

    public static string GetDateAsStringWeekFormat(DateTime p_date)
    {
      Int32 l_period = (Int32)p_date.ToOADate();
      string l_weekString = Local.GetValue("general.week");
      return l_weekString + PeriodModel.GetWeekNumberFromDateId(l_period) + ", " + DateTime.FromOADate(l_period).Year;
    }

    public static List<DayOfWeek> GetWeekEndDays()
    {
      List<DayOfWeek> l_weekEndDays = new List<DayOfWeek>();
      l_weekEndDays.Add(DayOfWeek.Saturday);
      l_weekEndDays.Add(DayOfWeek.Sunday);
      return l_weekEndDays;
    }

    public static List<Int32> FilterWeekEnds(List<Int32> p_periodList)
    {
    //  bool l_includeWeekEnds = Properties.Settings.Default.includeWeekEnds;
      List<DayOfWeek> l_weekEndDays = GetWeekEndDays();
      List<Int32> l_filteredPeriodsList = new List<Int32>();

      foreach (Int32 l_periodId in p_periodList)
      {
        if (l_weekEndDays.Contains(DateTime.FromOADate(l_periodId).DayOfWeek) == false)
          l_filteredPeriodsList.Add(l_periodId);
      }
        return l_filteredPeriodsList;
    }


    #endregion
  }
}