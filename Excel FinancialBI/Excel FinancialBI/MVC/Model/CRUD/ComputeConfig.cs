using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.Model.CRUD
{
  class ComputeConfig
  {
    enum Period
    {
      DAY,
      WEEK,
      MONTH,
      YEAR
    }

    enum Dimension
    {
      ACCOUNT,
      VERSION,
      PERIOD,
      ENTITY,
      CLIENT,
      PRODUCT,
      EMPLOYEE
    }

    public ComputeRequest Request { get; set; }
    public List<Tuple<Dimension, object>> RowList { get; set; }
    public List<Tuple<Dimension, object>> ColumnList { get; set; }

    public ComputeConfig()
    {
      RowList = new List<Tuple<Dimension, object>>();
      ColumnList = new List<Tuple<Dimension, object>>();
    }
  }
}
