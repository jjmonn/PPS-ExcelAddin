using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FBI.MVC.View
{
  using Model.CRUD;
  using Model;
  using Controller;
  using Forms;
  using Utils;

  public partial class EmployeeView : AxisView
  {
    FbiTreeView<AxisElem> m_employeeTreeView = null;
    public EmployeeView()
    {
      MultiIndexDictionary<uint, string, AxisElem> l_axisElemDic = AxisElemModel.Instance.GetDictionary(AxisType.Entities);
      m_employeeTreeView = new FbiTreeView<AxisElem>(l_axisElemDic);
      TableLayoutPanel1.Controls.Add(m_employeeTreeView);
    }
  }
}
