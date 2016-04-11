using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  using MVC.Model.CRUD;

  class PeriodUtils
  {
    public static SafeDictionary<int, string> ToList(List<int> p_periods, TimeConfig p_time)
    {
      DateTime l_date;
      SafeDictionary<int, string> l_dic = new SafeDictionary<int, string>();

      foreach (int l_period in p_periods)
      {
        l_date = DateTime.FromOADate(l_period);
        switch (p_time)
        {
          case TimeConfig.YEARS:
            l_dic[l_period] = l_date.Year.ToString();
            break;
          case TimeConfig.MONTHS:
            l_dic[l_period] = l_date.Month + " " + l_date.Year;
            break;
          default:
          case TimeConfig.DAYS:
            l_dic[l_period] = l_date.Day + " " + l_date.Month + " " + l_date.Year;
            break;
        }
      }
      return (l_dic);
    }
  }
}
