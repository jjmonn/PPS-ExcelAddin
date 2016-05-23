using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.Utils
{
  using MVC.Model.CRUD;
  using MVC.Model;

  class PeriodUtils
  {
    public static SafeDictionary<int, string> ToList(List<int> p_periods, TimeConfig p_time)
    {
      SafeDictionary<int, string> l_dic = new SafeDictionary<int, string>();

      foreach (int l_period in p_periods)
        l_dic[l_period] = PeriodModel.GetFormatedDate(l_period, p_time);
      return (l_dic);
    }
  }
}
