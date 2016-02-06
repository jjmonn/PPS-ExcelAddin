﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Globalization;
using Microsoft.VisualBasic;

namespace FBI.MVC.Model
{

  class Period
  {
    #region "Instance Variables"

    internal const Int32 m_nbMonthInYear = 12;
    internal const Int32 m_nbDaysInWeek = 7;
    internal const Int32 m_nbWeekdsInYear = 52;

    internal const Int32 m_nbDaysInYear = 365;

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

    #region "Years interface"

    static internal Int32[] GetYearsList(Int32 p_startPeriod, Int32 p_nbPeriod, CRUD.TimeConfig p_timeConfig)
    {

      switch (p_timeConfig)
      {
        case CRUD.TimeConfig.YEARS:
          return GetYearsListFromYearlyConfiguration(p_startPeriod, p_nbPeriod).ToArray();
        case CRUD.TimeConfig.MONTHS:
          return GetYearsListFromMonthlyConfiguration(p_startPeriod, p_nbPeriod).ToArray();
        case CRUD.TimeConfig.WEEK:
          return GetYearsListFromWeeklyConfiguration(p_startPeriod, p_nbPeriod).ToArray();
        case CRUD.TimeConfig.DAYS:
          return GetYearsListFromDailyConfiguration(p_startPeriod, p_nbPeriod).ToArray();
        default:
          System.Diagnostics.Debug.WriteLine("Period.vb - Get YearsList() - Undefined time configuration");
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

      Int32[] l_monthList = GetMonthsList(p_startPeriod, p_nbPeriods, CRUD.TimeConfig.MONTHS);
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
      Int32[] l_weekList = GetWeeksList(p_startPeriod, p_nbPeriods, CRUD.TimeConfig.WEEK);

      foreach (Int32 l_weekId in l_weekList)
      {
        yearId = System.DateTime.FromOADate(l_weekId).Year;
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
        yearId = System.DateTime.FromOADate(l_dayId).Year;
        if (l_yearsIdList.Contains(yearId) == false)
          l_yearsIdList.Add(yearId);
      }
      return l_yearsIdList;

    }

    #endregion

    #region "Months interface"

    static internal Int32[] GetMonthsList(Int32 p_startPeriod, Int32 p_nbPeriod, CRUD.TimeConfig p_timeConfiguration)
    {

      switch (p_timeConfiguration)
      {
        case CRUD.TimeConfig.MONTHS:
          return GetMonthsListFromMonthlyConfiguration(p_startPeriod, p_nbPeriod).ToArray();
        case CRUD.TimeConfig.WEEK:
          return GetMonthsListFromWeeklyConfiguration(p_startPeriod, p_nbPeriod).ToArray();
        case CRUD.TimeConfig.DAYS:
          return GetMonthsListFromDailyConfiguration(p_startPeriod, p_nbPeriod).ToArray();
        case CRUD.TimeConfig.YEARS:
          System.Diagnostics.Debug.WriteLine("Period.vb - Get GetMonthsList() - yearly time configuration asked (years must not be broke down in months because not values on months if yearly config).");
          return null;
        default:
          System.Diagnostics.Debug.WriteLine("Period.vb - Get GetMonthsList() - Undefined time configuration");
          return null;
      }

    }

    private static Int32[] GetMonthsListFromMonthlyConfiguration(Int32 p_startPeriod, Int32 p_nbPeriod)
    {

      List<Int32> l_periodsList = new List<Int32>();
      l_periodsList.Add(p_startPeriod);
      if ((p_nbPeriod <= 1))
        return (l_periodsList.ToArray());
      Int32[] m_monthList = {
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

      double l_currentYear = System.DateTime.FromOADate(p_startPeriod).Year;
      Int32 l_currentMonth = System.DateTime.FromOADate(p_startPeriod).Month;

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

      return (l_periodsList.ToArray());

    }

    private static List<Int32> GetMonthsListFromWeeklyConfiguration(Int32 p_startPeriod, Int32 p_nbPeriods)
    {

      List<Int32> l_monthsIdList = new List<Int32>();
      Int32 l_monthId = 0;
      Int32[] l_weekList = GetWeeksList(p_startPeriod, p_nbPeriods, CRUD.TimeConfig.WEEK);

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
    static internal Int32[] GetMonthsIdsInYear(Int32 p_yearId)
    {

      Int32[] l_monthsIdList = new Int32[12];
      Int32 l_monthId = 0;
      Int32 l_year = System.DateTime.FromOADate(p_yearId).Year;
      Int32[] m_monthList = {
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
      for (Int32 l_monthIndex = 0; l_monthIndex <= 11; l_monthIndex++)
      {
        l_monthId = (Int32)DateAndTime.DateSerial(l_year, l_monthIndex + 1, m_monthList[l_monthIndex]).ToOADate();
        // Add 1 day if february of a leap year
        if (l_monthIndex == 1 && DateTime.IsLeapYear(l_year))
          l_monthId++;
        l_monthsIdList[l_monthIndex] = l_monthId;
      }
      return (l_monthsIdList.ToArray());
    }

    #endregion

    #region "Weeks interface"

    static internal Int32[] GetWeeksList(Int32 p_startPeriod, Int32 p_nbPeriod, CRUD.TimeConfig p_timeConfig)
    {

      switch (p_timeConfig)
      {
        case CRUD.TimeConfig.WEEK:
          return GetWeekIDListFromWeeklyConfig(p_startPeriod, p_nbPeriod).ToArray();
        case CRUD.TimeConfig.DAYS:
          return GetWeekIDListFromDailyConfig(p_startPeriod, p_nbPeriod).ToArray();
        case CRUD.TimeConfig.MONTHS:
          System.Diagnostics.Debug.WriteLine("Period.vb - GetWeeksList() - Monthly config version asked");
          return new Int32[0];
        case CRUD.TimeConfig.YEARS:
          System.Diagnostics.Debug.WriteLine("Period.vb - GetWeeksList() - Yearly config version asked");
          return new Int32[0];
      }
      return new Int32[0];
    }

    private static List<Int32> GetWeekIDListFromWeeklyConfig(Int32 p_startPeriod, Int32 p_nbPeriod)
    {

      List<Int32> l_weeksList = new List<Int32>();
      Int32 l_periodId = p_startPeriod;

      // Check which day a week starts !!
      for (Int32 i = 0; i <= p_nbPeriod - 1; i++)
      {
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
      Int32[] l_dayList = GetDaysList(p_startPeriod, p_nbPeriod);


      foreach (Int32 l_dayId in l_dayList)
      {
        l_currentWeekId = GetWeekIdFromPeriodId(l_dayId);
        if (l_weeksList.Contains(l_currentWeekId) == false)
          l_weeksList.Add(l_currentWeekId);
      }
      return l_weeksList;

    }

    static internal Int32 GetFirstDayOfWeekId(Int32 p_dayId)
    {
      if (p_dayId == 0)
        return (0);

      Int32 l_weekId = p_dayId - DateAndTime.Weekday(System.DateTime.FromOADate(p_dayId), Microsoft.VisualBasic.FirstDayOfWeek.System);
      return (l_weekId);

    }

    static internal List<Int32> GetWeeksPeriodListFromPeriodsRange(System.DateTime p_startDate, System.DateTime p_endDate)
    {

      List<Int32> l_periods = new List<Int32>();
      while (p_startDate < p_endDate)
      {
        l_periods.Add(Period.GetWeekIdFromPeriodId((Int32)p_startDate.ToOADate()));
        p_startDate = p_startDate.AddDays(m_nbDaysInWeek);
      }
      return l_periods;

    }

    #endregion

    #region "Days interface"
    // To be checked 
    static internal Int32[] GetDaysList(Int32 p_startDayId, Int32 p_nbDays)
    {

      List<Int32> l_daysList = new List<Int32>();
      for (Int32 i = 0; i <= p_nbDays - 1; i++)
      {
        l_daysList.Add(p_startDayId);
        p_startDayId += 1;
      }
      return (l_daysList.ToArray());

    }

    static internal List<Int32> GetDaysPeriodsListFromWeeksId(List<Int32> p_weeksPeriodsList)
    {

      List<Int32> l_periods = new List<Int32>();
      foreach (Int32 l_weekId in p_weeksPeriodsList)
        foreach (Int32 l_day in GetDaysIdListInWeek(l_weekId))
          l_periods.Add(l_day);
      return (l_periods);
    }

    static internal List<Int32> GetDaysIdListInWeek(Int32 p_weekId)
    {

      List<Int32> l_daysIdList = new List<Int32>();
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

    static internal Int32 GetYearIDFromYearValue(Int32 p_yearValue)
    {
      return ((Int32)(DateAndTime.DateSerial(p_yearValue, 12, 31).ToOADate()));
    }

    static internal Int32 GetYearValueFromYearID(Int32 p_yearId)
    {
      return (DateTime.FromOADate(Convert.ToDouble(p_yearId)).Year);
    }

    static internal Int32 GetYearIdFromPeriodId(Int32 p_periodId)
    {
      return (GetYearIdFromMonthID(GetMonthIdFromPeriodId(p_periodId)));
    }

    static internal Int32 GetYearIdFromMonthID(Int32 p_monthId)
    {

      Int32 year_ = System.DateTime.FromOADate(p_monthId).Year;
      return ((Int32)DateAndTime.DateSerial(year_, 12, 31).ToOADate());

    }

    static internal Int32 GetWeekIdFromPeriodId(Int32 p_dayId)
    {

      // Uses localized settings for the first day of the week.
      if (p_dayId == 0)
        return 0;
      Int32 l_weekId = p_dayId - DateAndTime.Weekday(System.DateTime.FromOADate(p_dayId), Microsoft.VisualBasic.FirstDayOfWeek.System) + 7;
      return l_weekId;

    }

    // Return the last day of month
    static internal Int32 GetMonthIdFromPeriodId(Int32 p_periodId)
    {

      Int32 l_monthId = default(Int32);
      Int32 l_year = System.DateTime.FromOADate(p_periodId).Year;

      Int32 l_monthnb = DateTime.FromOADate(p_periodId).Month - 1;
      l_monthId = (Int32)DateAndTime.DateSerial(l_year, DateTime.FromOADate(p_periodId).Month, m_monthList[l_monthnb]).ToOADate();

      // Add 1 day if february of a leap year
      if (l_monthnb == 1 && DateTime.IsLeapYear(l_year))
        l_monthId += 1;
      return (l_monthId);
    }

    // Compliant with ISO 860
    static internal Int32 GetWeekNumberFromDateId(ref Int32 p_periodId)
    {

      // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
      // be the same week# as whatever Thursday, Friday or Saturday are,  and we always get those right
      DateTime l_time = System.DateTime.FromOADate(p_periodId);
      DayOfWeek l_day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(l_time);
      if (l_day >= DayOfWeek.Monday && l_day <= DayOfWeek.Wednesday)
        l_time = l_time.AddDays(3);

      // Return the week of our adjusted day
      return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(l_time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    static internal List<Int32> FilterPeriodList(Int32[] p_filteredPeriods, List<Int32> m_FilterReferencePeriodsList)
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

    #endregion
  }
}