using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBI.MVC.View
{
  using Controller;
  using Model;
  using Model.CRUD;
  using Utils;

  class AxisView : AxisBaseView<AxisController>
  {
    public AxisView()
    {

    }

    public override void LoadView()
    {
      MultiIndexDictionary<UInt32, Tuple<UInt32, UInt32>, AxisFilter> l_axisFilterDic = AxisFilterModel.Instance.GetDictionary(m_controller.AxisType);
      base.LoadView();

      if (l_axisFilterDic != null)
        LoadDGV(AxisElemModel.Instance.GetDictionary(m_controller.AxisType), l_axisFilterDic.SortedValues);
    }
  }
}
